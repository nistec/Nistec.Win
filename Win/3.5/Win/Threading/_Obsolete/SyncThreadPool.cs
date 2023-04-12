using System;
using System.Threading;
using System.Collections;

namespace MControl.Threading
{


	#region SyncMonitor

		/// <summary>Implementation SyncMonitor based on the Monitor class.</summary>
		public class SyncMonitor
		{
			#region Member Variables
			/// <summary>The number of units alloted by this syncMonitor.</summary>
			private int _count;
			/// <summary>Lock for the syncMonitor.</summary>
			private object _semLock = new object();
			#endregion

			#region Construction
			/// <summary> Initialize the syncMonitor as a binary syncMonitor.</summary>
			public SyncMonitor() : this(1) 
			{
			}

			/// <summary> Initialize the syncMonitor as a counting syncMonitor.</summary>
			/// <param name="count">Initial number of threads that can take out units from this syncMonitor.</param>
			/// <exception cref="ArgumentException">Throws if the count argument is less than 0.</exception>
			public SyncMonitor(int count) 
			{
				if (count < 0) throw new ArgumentException("SyncMonitor must have a count of at least 0.", "count");
				_count = count;
			}
			#endregion

			#region Synchronization Operations

            /// <summary>WaitOne the syncMonitor (take out 1 unit from it).</summary>
            public void WaitOne() //P
			{
				// Lock so we can work in peace.  This works because lock is actually
				// built around Monitor.
				lock(_semLock) 
				{
					// Wait until a unit becomes available.  We need to wait
					// in a loop in case someone else wakes up before us.  This could
					// happen if the Monitor.Pulse statements were changed to Monitor.PulseAll
					// statements in order to introduce some randomness into the order
					// in which threads are woken.
					while(_count <= 0) Monitor.Wait(_semLock, Timeout.Infinite);
					_count--;
				}
			}

            /// <summary>AddOne the syncMonitor (add 1 unit to it).</summary>
            public void AddOne() //V
			{
				// Lock so we can work in peace.  This works because lock is actually
				// built around Monitor.
				lock(_semLock) 
				{
					// Release our hold on the unit of control.  Then tell everyone
					// waiting on this object that there is a unit available.
					_count++;
					Monitor.Pulse(_semLock);
				}
			}

			/// <summary>Resets the syncMonitor to the specified count.  Should be used cautiously.</summary>
			public void Reset(int count)
			{
				lock(_semLock) { _count = count; }
			}
			#endregion
		}


	#endregion

	#region SyncThreadPool
    public class SyncThreadPoolConfig
    {
        private static int maxThreadPool = 5;

        static SyncThreadPoolConfig()
        {

        }

        public static int MaxThreadPool
        {
            get { return maxThreadPool; }
            set
            {
                if (value < 2 || value > 25)
                {
                    throw new ArgumentException("required value between 2 and 25");
                }
                maxThreadPool = value;
            }
        }
    }
        /// <summary>SyncThreadPool.</summary>
		public class SyncThreadPool
		{
			#region Constants
			/// <summary>Maximum number of threads the thread pool has at its disposal.</summary>
			private static int _maxWorkerThreads = 5;

			#endregion

			#region Member Variables
			/// <summary>Queue of all the callbacks waiting to be executed.</summary>
			private static Queue _waitingCallbacks;
			/// <summary>
			/// Used to signal that a worker thread is needed for processing.  Note that multiple
			/// threads may be needed simultaneously and as such we use a syncMonitor instead of
			/// an auto reset event.
			/// </summary>
			private static SyncMonitor _workerThreadNeeded;
			/// <summary>List of all worker threads at the disposal of the thread pool.</summary>
			private static ArrayList _workerThreads;
            /// <summary>Number of threads currently active ThreadsInUse.</summary>
			private static int _ThreadsInUse;
			/// <summary>Lockable object for the ThreadPool.</summary>
			private static object _lockablePool = new object();
			#endregion

			#region Construction and Finalization
			/// <summary>Initialize the thread pool.</summary>
			static SyncThreadPool() { Initialize(); }

			/// <summary>Initializes the thread pool.</summary>
			private static void Initialize()
			{
				// Create our thread stores; we handle synchronization ourself
				// as we may run into situtations where multiple operations need to be atomic.
				// We keep track of the threads we've created just for good measure; not actually
				// needed for any core functionality.
				_waitingCallbacks = new Queue();
				_workerThreads = new ArrayList();
				_ThreadsInUse = 0;
                _maxWorkerThreads = SyncThreadPoolConfig.MaxThreadPool;

				// Create our "thread needed" event
				_workerThreadNeeded = new SyncMonitor(0);
			
				// Create all of the worker threads
				for(int i=0; i<_maxWorkerThreads; i++)
				{
					// Create a new thread and add it to the list of threads.
					Thread newThread = new Thread(new ThreadStart(ProcessQueuedItems));
					_workerThreads.Add(newThread);

					// Configure the new thread and start it
                    newThread.Name = "SyncThreadPool #" + i.ToString();
					newThread.IsBackground = true;
					newThread.Start();
				}
			}
			#endregion

			#region Public Methods
			/// <summary>Queues a user work item to the thread pool.</summary>
			/// <param name="callback">
			/// A WaitCallback representing the delegate to invoke when the thread in the 
			/// thread pool picks up the work item.
			/// </param>
			public static void SyncWorkItem(WaitCallback callback)
			{
				// Queue the delegate with no state
				SyncWorkItem(callback, null);
			}

			/// <summary>Queues a user work item to the thread pool.</summary>
			/// <param name="callback">
			/// A WaitCallback representing the delegate to invoke when the thread in the 
			/// thread pool picks up the work item.
			/// </param>
			/// <param name="state">
			/// The object that is passed to the delegate when serviced from the thread pool.
			/// </param>
			public static void SyncWorkItem(WaitCallback callback, object state)
			{
				// Create a waiting callback that contains the delegate and its state.
				// At it to the processing queue, and signal that data is waiting.
				SyncCallBack waiting = new SyncCallBack(callback, state);
				lock(_lockablePool) { _waitingCallbacks.Enqueue(waiting); }
				_workerThreadNeeded.AddOne();
			}

			/// <summary>Empties the work queue of any queued work items.  Resets all threads in the pool.</summary>
			public static void Reset()
			{
				lock(_lockablePool) 
				{ 
					// Cleanup any waiting callbacks
					try 
					{
						// Try to dispose of all remaining state
						foreach(object obj in _waitingCallbacks)
						{
							SyncCallBack callback = (SyncCallBack)obj;
							if (callback.State is IDisposable) ((IDisposable)callback.State).Dispose();
						}
					} 
					catch{}

					// Shutdown all existing threads
					try 
					{
						foreach(Thread thread in _workerThreads) 
						{
							if (thread != null) thread.Abort("reset");
						}
					}
					catch{}

					// Reinitialize the pool (create new threads, etc.)
					Initialize();
				}
			}
			#endregion

			#region Properties
			/// <summary>Gets the number of threads at the disposal of the thread pool.</summary>
			public static int MaxThreads { get { return _maxWorkerThreads; } }
			/// <summary>Gets the number of currently active threads in the thread pool.</summary>
			public static int ActiveThreads { get { return _ThreadsInUse; } }
			/// <summary>Gets the number of callback delegates currently waiting in the thread pool.</summary>
			public static int WaitingCallbacks { get { lock(_lockablePool) { return _waitingCallbacks.Count; } } }
			#endregion

			#region Thread Processing
			/// <summary>Event raised when there is an exception on a threadpool thread.</summary>
			public static event UnhandledExceptionEventHandler ThreadException;

			/// <summary>A thread worker function that processes items from the work queue.</summary>
			private static void ProcessQueuedItems()
			{
				// Process indefinitely
				while(true)
				{
					_workerThreadNeeded.WaitOne();

					// Get the next item in the queue.  If there is nothing there, go to sleep
					// for a while until we're woken up when a callback is waiting.
					SyncCallBack callback = null;

					// Try to get the next callback available.  We need to lock on the 
					// queue in order to make our count check and retrieval atomic.
					lock(_lockablePool)
					{
						if (_waitingCallbacks.Count > 0)
						{
							try { callback = (SyncCallBack)_waitingCallbacks.Dequeue(); } 
							catch{} // make sure not to fail here
						}
					}

					if (callback != null)
					{
						// We now have a callback.  Execute it.  Make sure to accurately
						// record how many callbacks are currently executing.
						try 
						{
							Interlocked.Increment(ref _ThreadsInUse);
							callback.Callback(callback.State);
						} 
						catch(Exception exc)
						{
							try
							{
                                UnhandledExceptionEventHandler handler = ThreadException;
								if (handler != null) handler(typeof(SyncThreadPool), new UnhandledExceptionEventArgs(exc, false));
							}
							catch{}
						}
						finally
						{
							Interlocked.Decrement(ref _ThreadsInUse);
						}
					}
				}
			}
			#endregion

			#region SyncCallBack

			/// <summary>Used to hold a callback delegate and the state for that delegate.</summary>
			private class SyncCallBack
			{
				#region Member Variables
				/// <summary>Callback delegate for the callback.</summary>
				private WaitCallback _callback;
				/// <summary>State with which to call the callback delegate.</summary>
				private object _state;
				#endregion

				#region Construction
				/// <summary>Initialize the callback holding object.</summary>
				/// <param name="callback">Callback delegate for the callback.</param>
				/// <param name="state">State with which to call the callback delegate.</param>
				public SyncCallBack(WaitCallback callback, object state)
				{
					_callback = callback;
					_state = state;
				}
				#endregion

				#region Properties
				/// <summary>Gets the callback delegate for the callback.</summary>
				public WaitCallback Callback { get { return _callback; } }
				/// <summary>Gets the state with which to call the callback delegate.</summary>
				public object State { get { return _state; } }
				#endregion
			}

			#endregion
		}

	#endregion

}
