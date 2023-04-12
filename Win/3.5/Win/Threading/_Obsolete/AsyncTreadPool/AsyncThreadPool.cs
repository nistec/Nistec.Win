

using System;
using System.Security;
using System.Threading;
using System.Collections;
using System.Diagnostics;

namespace MControl.Threading
{
	#region PostExecuteCall enumerator

    [Flags]
	public enum PostExecuteCall
	{
        Never                    = 0x00,
        WhenWorkItemCanceled     = 0x01,
		WhenWorkItemNotCanceled  = 0x02,
		Always                   = WhenWorkItemCanceled | WhenWorkItemNotCanceled,
	}

	#endregion

	#region AsyncThreadPool class
	/// <summary>
	/// thread pool class.
	/// </summary>
    public class AsyncThreadPool : IDisposable
    {
        #region Default Constants

        /// <summary>
        /// Default minimum number of threads the thread pool contains. (0)
        /// </summary>
        public const int DefaultMinWorkerThreads = 0;

        /// <summary>
        /// Default maximum number of threads the thread pool contains. (25)
        /// </summary>
        public const int DefaultMaxWorkerThreads = 25;

        /// <summary>
        /// Default idle timeout in milliseconds. (One minute)
        /// </summary>
        public const int DefaultIdleTimeout = 60*1000; // One minute

        /// <summary>
        /// Indicate to copy the security context of the caller and then use it in the call. (false)
        /// </summary>
        public const bool DefaultUseCallerContext = false; 

        /// <summary>
        /// Indicate to dispose of the state objects if they support the IDispose interface. (false)
        /// </summary>
        public const bool DefaultDisposeOfStateObjects = false; 

        /// <summary>
        /// The default option to run the post execute
        /// </summary>
        public const PostExecuteCall DefaultCallToPostExecute = PostExecuteCall.Always;

		/// <summary>
		/// The default post execute method to run. 
		/// When null it means not to call it.
		/// </summary>
		public static readonly PostExecuteWorkItemCallback DefaultPostExecuteWorkItemCallback = null;

        #endregion

        #region Member Variables

        /// <summary>
        /// Hashtable of all the threads in the thread pool.
        /// </summary>
        private Hashtable _workerThreads = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Queue of work items.
        /// </summary>
        private WorkItemsQueue _workItemsQueue = new WorkItemsQueue();

        /// <summary>
        /// Number of threads that currently work (not idle).
        /// </summary>
        private int _inUseWorkerThreads = 0;

        /// <summary>
        /// Start information to use. 
        /// It is simpler than providing many constructors.
        /// </summary>
        private ThreadPoolStateInfo _tpStateInfo = new ThreadPoolStateInfo();

        /// <summary>
        /// Total number of work items that are stored in the work items queue 
        /// plus the work items that the threads in the pool are working on.
        /// </summary>
        private int _currentWorkItemsCount = 0;

        /// <summary>
        /// Signaled when the thread pool is idle, i.e. no thread is busy
        /// and the work items queue is empty
        /// </summary>
        private ManualResetEvent _isIdleWaitHandle = new ManualResetEvent(true);

        /// <summary>
        /// An event to signal all the threads to quit immediately.
        /// </summary>
        private ManualResetEvent _shuttingDownEvent = new ManualResetEvent(false);

        /// <summary>
        /// A flag to indicate the threads to quit.
        /// </summary>
        private bool _shutdown = false;

        /// <summary>
        /// Counts the threads created in the pool.
        /// It is used to name the threads.
        /// </summary>
        private int _threadCounter = 0;

        /// <summary>
        /// Indicate that the AsyncThreadPool has been disposed
        /// </summary>
        private bool _isDisposed = false;

        #endregion

        #region Construction and Finalization

        /// <summary>
        /// Constructor
        /// </summary>
        public AsyncThreadPool()
        {
            Start();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idleTimeout">Idle timeout in milliseconds</param>
        public AsyncThreadPool(int idleTimeout)
        {
            _tpStateInfo.IdleTimeout = idleTimeout;
            Start();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idleTimeout">Idle timeout in milliseconds</param>
        /// <param name="maxWorkerThreads">Upper limit of threads in the pool</param>
        public AsyncThreadPool(
            int idleTimeout,
            int maxWorkerThreads)
        {
            _tpStateInfo.IdleTimeout = idleTimeout;
            _tpStateInfo.MaxWorkerThreads = maxWorkerThreads;
            Start();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idleTimeout">Idle timeout in milliseconds</param>
        /// <param name="maxWorkerThreads">Upper limit of threads in the pool</param>
        /// <param name="minWorkerThreads">Lower limit of threads in the pool</param>
        public AsyncThreadPool(
            int idleTimeout,
            int maxWorkerThreads,
            int minWorkerThreads)
        {
            _tpStateInfo.IdleTimeout = idleTimeout;
            _tpStateInfo.MaxWorkerThreads = maxWorkerThreads;
            _tpStateInfo.MinWorkerThreads = minWorkerThreads;
            Start();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idleTimeout">Idle timeout in milliseconds</param>
        /// <param name="maxWorkerThreads">Upper limit of threads in the pool</param>
        /// <param name="minWorkerThreads">Lower limit of threads in the pool</param>
        /// <param name="useCallerContext">Indicate to copy the security context of the caller and then use it in the call</param>
        /// <param name="disposeOfStateObjects">Indicate to dispose of the state objects if they support the IDispose interface</param>
        public AsyncThreadPool(ThreadPoolStateInfo tpStateInfo)
        {
            _tpStateInfo = new ThreadPoolStateInfo(tpStateInfo);
            Start();
        }

        private void Start()
        {
            ValidateTPStateInfo();
            StartThreads(_tpStateInfo.MinWorkerThreads);
        }

        private void ValidateTPStateInfo()
        {
            if (_tpStateInfo.MinWorkerThreads < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "MinWorkerThreads", "MinWorkerThreads cannot be negative");
            }

            if (_tpStateInfo.MaxWorkerThreads <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "MaxWorkerThreads", "MaxWorkerThreads must be greater than zero");
            }

            if (_tpStateInfo.MinWorkerThreads > _tpStateInfo.MaxWorkerThreads)
            {
                throw new ArgumentOutOfRangeException(
                    "MinWorkerThreads, maxWorkerThreads", 
                    "MaxWorkerThreads must be greater or equal to MinWorkerThreads");
            }
        }

        #endregion

        #region Thread Processing

        /// <summary>
        /// Waits on the queue for a work item, shutdown, or timeout.
        /// </summary>
        /// <returns>
        /// Returns the WaitingCallback or null in case of timeout or shutdown.
        /// </returns>
        private WorkItem Dequeue()
        {
            WorkItem workItem = 
                _workItemsQueue.DequeueWorkItem(_tpStateInfo.IdleTimeout, _shuttingDownEvent);

            return workItem;
        }

        /// <summary>
        /// Put a new work item in the queue
        /// </summary>
        /// <param name="workItem">A work item to queue</param>
        private void Enqueue(WorkItem workItem)
        {
            // Make sure the workItem is not null
            Debug.Assert(null != workItem);

            IncrementWorkItemsCount();

            _workItemsQueue.EnqueueWorkItem(workItem);

            // If all the threads are busy then try to create a new one
            if ((ThreadsInUse + WaitingCallbacks) > _workerThreads.Count) 
            {
                StartThreads(1);
            }
        }

        private void IncrementWorkItemsCount()
        {
            int count = Interlocked.Increment(ref _currentWorkItemsCount);
            if (count == 1) 
            {
                _isIdleWaitHandle.Reset();
            }
        }

        private void DecrementWorkItemsCount()
        {
            int count = Interlocked.Decrement(ref _currentWorkItemsCount);
            if (count == 0) 
            {
                _isIdleWaitHandle.Set();
            }
        }

        /// <summary>
        /// Inform that the current thread is about to quit or quiting.
        /// The same thread may call this method more than once.
        /// </summary>
        private void InformCompleted()
        {
            // There is no need to lock the two methods together 
            // since only the current thread removes itself
            // and the _workerThreads is a synchronized hashtable
            if (_workerThreads.Contains(Thread.CurrentThread))
            {
                _workerThreads.Remove(Thread.CurrentThread);
            }
        }

        /// <summary>
        /// Starts new threads
        /// </summary>
        /// <param name="threadsCount">The number of threads to start</param>
        private void StartThreads(int threadsCount)
        {
            lock(_workerThreads.SyncRoot)
            {
                // Don't start threads on shut down
                if (_shutdown)
                {
                    return;
                }

                for(int i = 0; i < threadsCount; ++i)
                {
                    // Don't create more threads then the upper limit
                    if (_workerThreads.Count >= _tpStateInfo.MaxWorkerThreads)
                    {
                        return;
                    }

                    // Create a new thread
                    Thread workerThread = new Thread(new ThreadStart(ProcessQueuedItems));

                    // Configure the new thread and start it
                    workerThread.Name = "CTP Thread #" + _threadCounter;
                    workerThread.IsBackground = true;
                    workerThread.Start();
                    ++_threadCounter;

                    // Add the new thread to the hashtable and update its creation
                    // time.
                    _workerThreads[workerThread] = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// A worker thread method that processes work items from the work items queue.
        /// </summary>
        private void ProcessQueuedItems()
        {
            try
            {
                // Process until shutdown.
                while(!_shutdown)
                {
                    // Update the last time this thread was seen alive.
                    // It's good for debugging.
                    _workerThreads[Thread.CurrentThread] = DateTime.Now;

                    // Wait for a work item, shutdown, or timeout
                    WorkItem workItem = Dequeue();

                    // Update the last time this thread was seen alive.
                    // It's good for debugging.
                    _workerThreads[Thread.CurrentThread] = DateTime.Now;

                    // On timeout or shut down.
                    if (null == workItem)
                    {
                        // Double lock for quit.
                        if (_workerThreads.Count > _tpStateInfo.MinWorkerThreads)
                        {
                            lock(_workerThreads.SyncRoot)
                            {
                                if (_workerThreads.Count > _tpStateInfo.MinWorkerThreads)
                                {
                                    // Inform that the thread is quiting and then quit.
                                    // This method must be called within this lock or else
                                    // more threads will quit and the thread pool will go
                                    // below the lower limit.
                                    InformCompleted();
                                    break;
                                }
                            }
                        }
                    }

                    // If we didn't quit then skip to the next iteration.
                    if (null == workItem)
                    {
                        continue;
                    }

                    try 
                    {
                        // Change the state of the work item to 'in progress' if possible.
                        // We do it here so if the work item has been canceled we won't 
                        // increment the _inUseWorkerThreads.
                        // The cancel mechanism doesn't delete items from the queue,  
                        // it marks the work item as canceled, and when the work item
                        // is dequeued, we just skip it.
                        // If the post execute of work item is set to always or to
                        // call when the work item is canceled then the StartingWorkItem()
                        // will return true, so the post execute can run.
                        if (!workItem.StartingWorkItem())
                        {
                            continue;
                        }

                        // Execute the callback.  Make sure to accurately
                        // record how many callbacks are currently executing.
						Interlocked.Increment(ref _inUseWorkerThreads);

                        workItem.Execute();
                    }
                    finally
                    {
                        if (_tpStateInfo.DisposeOfStateObjects)
                        {
                            workItem.Dispose();
                        }

						Interlocked.Decrement(ref _inUseWorkerThreads);

                        DecrementWorkItemsCount();
                    }
                }
            } 
            catch(ThreadAbortException )
            {
                //tae = tae;
                // Handle the abort exception gracfully.
                Thread.ResetAbort();
            }
            catch(Exception e)
            {
                Debug.Assert(null != e);
            }
            finally
            {
                InformCompleted();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Queues a user work item to the thread pool.
        /// </summary>
        /// <param name="callback">
        /// A WaitCallback representing the delegate to invoke when the thread in the 
        /// thread pool picks up the work item.
        /// </param>
        public IWorkItemResult QueueWorkItem(WorkItemCallback callback)
        {
            // Queue the delegate with no state
            return QueueWorkItem(callback, null);
        }

        /// <summary>
        /// Queues a user work item to the thread pool.
        /// </summary>
        /// <param name="callback">
        /// A WaitCallback representing the delegate to invoke when the thread in the 
        /// thread pool picks up the work item.
        /// </param>
        /// <param name="state">
        /// The object that is passed to the delegate when serviced from the thread pool.
        /// </param>
        public IWorkItemResult QueueWorkItem(WorkItemCallback callback, object state)
        {
            ValidateNotDisposed();
            // Create a work item that contains the delegate and its state.
            WorkItem workItem = new WorkItem(
				callback, 
				state, 
				_tpStateInfo.UseCallerContext, 
				_tpStateInfo.PostExecuteWorkItemCallback, 
				_tpStateInfo.PostExecuteCall);
            Enqueue(workItem);
            return workItem.GetWorkItemResult();
        }

        /// <summary>
        /// Queues a user work item to the thread pool.
        /// </summary>
        /// <param name="callback">
        /// A WaitCallback representing the delegate to invoke when the thread in the 
        /// thread pool picks up the work item.
        /// </param>
        /// <param name="state">
        /// The object that is passed to the delegate when serviced from the thread pool.
        /// </param>
        /// <param name="postExecuteWorkItemCallback">
        /// A delegate to call after the callback completion
        /// </param>
        /// <returns>Work item result</returns>
        public IWorkItemResult QueueWorkItem(
            WorkItemCallback callback, 
            object state,
            PostExecuteWorkItemCallback postExecuteWorkItemCallback)
        {
            ValidateNotDisposed();

            // Create a work item that contains the delegate and its state.
            WorkItem workItem = new WorkItem(
                callback, 
                state, 
                _tpStateInfo.UseCallerContext, 
                postExecuteWorkItemCallback,
                _tpStateInfo.PostExecuteCall);
            Enqueue(workItem);
            return workItem.GetWorkItemResult();
        }

        /// <summary>
        /// Queues a user work item to the thread pool.
        /// </summary>
        /// <param name="callback">
        /// A WaitCallback representing the delegate to invoke when the thread in the 
        /// thread pool picks up the work item.
        /// </param>
        /// <param name="state">
        /// The object that is passed to the delegate when serviced from the thread pool.
        /// </param>
        /// <param name="postExecuteWorkItemCallback">
        /// A delegate to call after the callback completion
        /// </param>
        /// <param name="callToPostExecute">Indicates on which cases to call to the post execute callback</param>
        /// <returns>Work item result</returns>
        public IWorkItemResult QueueWorkItem(
            WorkItemCallback callback, 
            object state,
            PostExecuteWorkItemCallback postExecuteWorkItemCallback,
            PostExecuteCall callToPostExecute)
        {
            ValidateNotDisposed();

            // Create a work item that contains the delegate and its state.
            WorkItem workItem = new WorkItem(
                callback, 
                state, 
                _tpStateInfo.UseCallerContext, 
                postExecuteWorkItemCallback,
                callToPostExecute);
            Enqueue(workItem);
            return workItem.GetWorkItemResult();
        }

        /// <summary>
        /// Wait for the thread pool to be idle
        /// </summary>
        public void WaitForIdle()
        {
            WaitForIdle(Timeout.Infinite);
        }

        /// <summary>
        /// Wait for the thread pool to be idle
        /// </summary>
        public bool WaitForIdle(TimeSpan timeout)
        {
            return WaitForIdle((int)timeout.TotalMilliseconds);
        }

        /// <summary>
        /// Wait for the thread pool to be idle
        /// </summary>
        public bool WaitForIdle(int millisecondsTimeout)
        {
            return _isIdleWaitHandle.WaitOne(millisecondsTimeout, false);
        }

        /// <summary>
        /// Force the AsyncThreadPool to shutdown
        /// </summary>
        public void Abort()
        {
            Abort(true, 0);
        }

        public void Abort(bool forceAbort, TimeSpan timeout)
        {
            Abort(forceAbort, (int)timeout.TotalMilliseconds);
        }

        /// <summary>
        /// Empties the queue of work items and abort the threads in the pool.
        /// </summary>
        public void Abort(bool forceAbort, int millisecondsTimeout)
        {
            ValidateNotDisposed();
            Thread [] threads = null;
            lock(_workerThreads.SyncRoot)
            {
                // Shutdown the work items queue
                _workItemsQueue.Dispose();

                // Signal the threads to exit
                _shutdown = true;
                _shuttingDownEvent.Set();

                // Make a copy of the threads' references in the pool
                threads = new Thread [_workerThreads.Count];
                _workerThreads.Keys.CopyTo(threads, 0);
            }

            int millisecondsLeft = millisecondsTimeout;
            DateTime start = DateTime.Now;
            bool waitInfinitely = (Timeout.Infinite == millisecondsTimeout);
            bool timeout = false;

            // Each iteration we update the time left for the timeout.
            foreach(Thread thread in threads)
            {
                // Join don't work with negative numbers
                if (!waitInfinitely && (millisecondsLeft < 0))
                {
                    timeout = true;
                    break;
                }

                bool success = thread.Join(millisecondsLeft);
                if(!success)
                {
                    timeout = true;
                    break;
                }

                if(!waitInfinitely)
                {
                    // Update the time left to wait
                    TimeSpan ts = DateTime.Now - start;
                    millisecondsLeft = millisecondsTimeout - (int)ts.TotalMilliseconds;
                }
            }

            if (timeout && forceAbort)
            {
                // Abort the threads in the pool
                foreach(Thread thread in threads)
                {
                    if ((thread != null) && thread.IsAlive) 
                    {
						try 
						{
							thread.Abort("Shutdown");
						}
						catch(SecurityException )
						{
							//e = e;
						}
						catch(ThreadStateException)
						{
							//ex = ex;
							// In case the thread has been terminated 
							// after the check if it is alive.
						}
                    }
                }
            }
        }

        /// <summary>
        /// Wait for all work items to complete
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <returns>
        /// true when every work item in workItemResults has completed; otherwise false.
        /// </returns>
        public static bool WaitAll(
            IWorkItemResult [] workItemResults)
        {
            return WaitAll(workItemResults, Timeout.Infinite, true);
        }

        /// <summary>
        /// Wait for all work items to complete
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <param name="timeout">The number of milliseconds to wait, or a TimeSpan that represents -1 milliseconds to wait indefinitely. </param>
        /// <param name="exitContext">
        /// true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it; otherwise, false. 
        /// </param>
        /// <returns>
        /// true when every work item in workItemResults has completed; otherwise false.
        /// </returns>
        public static bool WaitAll(
            IWorkItemResult [] workItemResults,
            TimeSpan timeout,
            bool exitContext)
        {
            return WaitAll(workItemResults, (int)timeout.TotalMilliseconds, exitContext);
        }

        /// <summary>
        /// Wait for all work items to complete
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <param name="timeout">The number of milliseconds to wait, or a TimeSpan that represents -1 milliseconds to wait indefinitely. </param>
        /// <param name="exitContext">
        /// true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it; otherwise, false. 
        /// </param>
        /// <param name="cancelWaitHandle">A cancel wait handle to interrupt the wait if needed</param>
        /// <returns>
        /// true when every work item in workItemResults has completed; otherwise false.
        /// </returns>
        public static bool WaitAll(
            IWorkItemResult [] workItemResults,  
            TimeSpan timeout,
            bool exitContext,
            WaitHandle cancelWaitHandle)
        {
            return WaitAll(workItemResults, (int)timeout.TotalMilliseconds, exitContext, cancelWaitHandle);
        }

        /// <summary>
        /// Wait for all work items to complete
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Timeout.Infinite (-1) to wait indefinitely.</param>
        /// <param name="exitContext">
        /// true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it; otherwise, false. 
        /// </param>
        /// <returns>
        /// true when every work item in workItemResults has completed; otherwise false.
        /// </returns>
        public static bool WaitAll(
            IWorkItemResult [] workItemResults,  
            int millisecondsTimeout,
            bool exitContext)
        {
            return WorkItem.WaitAll(workItemResults, millisecondsTimeout, exitContext, null);
        }

        /// <summary>
        /// Wait for all work items to complete
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Timeout.Infinite (-1) to wait indefinitely.</param>
        /// <param name="exitContext">
        /// true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it; otherwise, false. 
        /// </param>
        /// <param name="cancelWaitHandle">A cancel wait handle to interrupt the wait if needed</param>
        /// <returns>
        /// true when every work item in workItemResults has completed; otherwise false.
        /// </returns>
        public static bool WaitAll(
            IWorkItemResult [] workItemResults,  
            int millisecondsTimeout,
            bool exitContext,
            WaitHandle cancelWaitHandle)
        {
            return WorkItem.WaitAll(workItemResults, millisecondsTimeout, exitContext, cancelWaitHandle);
        }


        /// <summary>
        /// Waits for any of the work items in the specified array to complete, cancel, or timeout
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <returns>
        /// The array index of the work item result that satisfied the wait, or WaitTimeout if any of the work items has been canceled.
        /// </returns>
        public static int WaitAny(
            IWorkItemResult [] workItemResults)
        {
            return WaitAny(workItemResults, Timeout.Infinite, true);
        }

        /// <summary>
        /// Waits for any of the work items in the specified array to complete, cancel, or timeout
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <param name="timeout">The number of milliseconds to wait, or a TimeSpan that represents -1 milliseconds to wait indefinitely. </param>
        /// <param name="exitContext">
        /// true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it; otherwise, false. 
        /// </param>
        /// <returns>
        /// The array index of the work item result that satisfied the wait, or WaitTimeout if no work item result satisfied the wait and a time interval equivalent to millisecondsTimeout has passed or the work item has been canceled.
        /// </returns>
        public static int WaitAny(
            IWorkItemResult [] workItemResults,
            TimeSpan timeout,
            bool exitContext)
        {
            return WaitAny(workItemResults, (int)timeout.TotalMilliseconds, exitContext);
        }

        /// <summary>
        /// Waits for any of the work items in the specified array to complete, cancel, or timeout
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <param name="timeout">The number of milliseconds to wait, or a TimeSpan that represents -1 milliseconds to wait indefinitely. </param>
        /// <param name="exitContext">
        /// true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it; otherwise, false. 
        /// </param>
        /// <param name="cancelWaitHandle">A cancel wait handle to interrupt the wait if needed</param>
        /// <returns>
        /// The array index of the work item result that satisfied the wait, or WaitTimeout if no work item result satisfied the wait and a time interval equivalent to millisecondsTimeout has passed or the work item has been canceled.
        /// </returns>
        public static int WaitAny(
            IWorkItemResult [] workItemResults,
            TimeSpan timeout,
            bool exitContext,
            WaitHandle cancelWaitHandle)
        {
            return WaitAny(workItemResults, (int)timeout.TotalMilliseconds, exitContext, cancelWaitHandle);
        }

        /// <summary>
        /// Waits for any of the work items in the specified array to complete, cancel, or timeout
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Timeout.Infinite (-1) to wait indefinitely.</param>
        /// <param name="exitContext">
        /// true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it; otherwise, false. 
        /// </param>
        /// <returns>
        /// The array index of the work item result that satisfied the wait, or WaitTimeout if no work item result satisfied the wait and a time interval equivalent to millisecondsTimeout has passed or the work item has been canceled.
        /// </returns>
        public static int WaitAny(
            IWorkItemResult [] workItemResults,  
            int millisecondsTimeout,
            bool exitContext)
        {
            return WorkItem.WaitAny(workItemResults, millisecondsTimeout, exitContext, null);
        }

        /// <summary>
        /// Waits for any of the work items in the specified array to complete, cancel, or timeout
        /// </summary>
        /// <param name="workItemResults">Array of work item result objects</param>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Timeout.Infinite (-1) to wait indefinitely.</param>
        /// <param name="exitContext">
        /// true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it; otherwise, false. 
        /// </param>
        /// <param name="cancelWaitHandle">A cancel wait handle to interrupt the wait if needed</param>
        /// <returns>
        /// The array index of the work item result that satisfied the wait, or WaitTimeout if no work item result satisfied the wait and a time interval equivalent to millisecondsTimeout has passed or the work item has been canceled.
        /// </returns>
        public static int WaitAny(
            IWorkItemResult [] workItemResults,  
            int millisecondsTimeout,
            bool exitContext,
            WaitHandle cancelWaitHandle)
        {
            return WorkItem.WaitAny(workItemResults, millisecondsTimeout, exitContext, cancelWaitHandle);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the lower limit of threads in the pool.
        /// </summary>
        public int MinThreads 
        { 
            get 
            {
                ValidateNotDisposed();
                return _tpStateInfo.MinWorkerThreads; 
            }
        }

        /// <summary>
        /// Get the upper limit of threads in the pool.
        /// </summary>
        public int MaxThreads 
        { 
            get 
            {
                ValidateNotDisposed();
                return _tpStateInfo.MaxWorkerThreads; 
            } 
        }
        /// <summary>
        /// Get the number of threads in the thread pool.
        /// Should be between the lower and the upper limits.
        /// </summary>
        public int ActiveThreads 
        { 
            get 
            {
                ValidateNotDisposed();
                return _workerThreads.Count; 
            } 
        }

        /// <summary>
        /// Get the number of busy (not idle) threads in the thread pool.
        /// </summary>
        public int ThreadsInUse 
        { 
            get 
            { 
                ValidateNotDisposed();
                return _inUseWorkerThreads; 
            } 
        }

        /// <summary>
        /// Get the number of work items in the queue.
        /// </summary>
        public int WaitingCallbacks 
        { 
            get 
            { 
                ValidateNotDisposed();
                return _workItemsQueue.Count;
            } 
        }

        #endregion

        #region IDisposable Members

        ~AsyncThreadPool()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                if (!_shutdown)
                {
                    Abort();
                }

                if (null != _shuttingDownEvent)
                {
                
                    _shuttingDownEvent.Close();
                    _shuttingDownEvent = null;
                }
                _workerThreads.Clear();
                _isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        private void ValidateNotDisposed()
        {
            if(_isDisposed)
            {
                throw new ObjectDisposedException(GetType().ToString(), "The AsyncThreadPool has been shutdown");
            }
        }
        #endregion
    }
	#endregion
}
