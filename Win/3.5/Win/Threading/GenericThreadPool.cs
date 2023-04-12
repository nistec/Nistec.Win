using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Messaging;

using System.Security;
using MControl.Win;

namespace MControl.Threading
{

    public class GenericThreadPool : IDisposable //,ICollection
    {

        public enum GenericThradState
        {
            Idle = 0,
            WorkBegin = 1,
            WorkEnd = 2
        }

        class GTState
        {
            public DateTime LastActivated;
            public GenericThradState State;
            public bool Started;

            public GTState()
            {
                LastActivated = DateTime.Now;
                State = GenericThradState.Idle;
                Started = false;

            }

            public void Suspend()
            {
                Started = false;
                State = GenericThradState.Idle;
            }
            public long GetIdlTime()
            {
                try
                {
                    return Convert.ToInt64(DateTime.Now.Subtract(LastActivated).TotalSeconds);
                }
                catch
                {
                    return 0;
                }
            }
            public bool CanResume()
            {
                return !Started && State == GenericThradState.Idle;
            }
            public bool CanSuspend(int idlTimeout)
            {
                long idleTime = GetIdlTime();
                return Started && State == GenericThradState.Idle && idleTime > idlTimeout;
            }
        }

        #region Members

        private bool initilized = false;
        /// <summary>
        /// Hashtable of all the threads in the thread pool.
        /// </summary>
        private Hashtable workerThreads = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// Hashtable of all the threads in the thread pool.
        /// </summary>
        private Dictionary<string, GTState> workItems = new Dictionary<string, GTState>();


        //private List<Thread> workerThreads;
        //private string m_GenericName;
        private int m_MaxThread = 1;
        private int m_MinThread = 1;
        //private int m_AvailableThread = 1;
        //private int m_IncrementFactor = 3;
        //private int m_DecrementFactor = 10;
        //private int m_IdleFactorState = 0;
        //private int m_IncrementFactorState = 0;
        //private int m_WorkFactor = 0;

        private bool m_FixedSize = false;
        private int m_Timeout = 1000;


        //private int m_SettingCouter = 0;

        /// <summary>
        /// Signaled when the thread pool is idle, i.e. no thread is busy
        /// and the work items queue is empty
        /// </summary>
        private ManualResetEvent m_IdleWaitHandle = new ManualResetEvent(true);

        /// <summary>
        /// An event to signal all the threads to quit immediately.
        /// </summary>
        private ManualResetEvent m_shuttingDownEvent = new ManualResetEvent(false);

        /// <summary>
        /// Indicate that the NetThreadPool has been disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// ErrorOcurred
        /// </summary>
        public event ErrorOcurredEventHandler ErrorOcurred;


        #endregion

        #region Constructor
        /// <summary>
        /// Ctor GenericThreadPool with fixed size
        /// </summary>
        /// <param name="maxThread"></param>
        public GenericThreadPool(int maxThread)
            : this(maxThread, maxThread, true)
        {
        }

        /// <summary>
        /// Ctor GenericThreadPool with dynamic size, using ManualReset and IdleTime
        /// </summary>
        /// <param name="minThread"></param>
        /// <param name="maxThread"></param>
        public GenericThreadPool(int minThread, int maxThread)//, int availableThreads)
            : this(minThread, maxThread, false)
        {
        }

        /// <summary>
        /// Ctor GenericThreadPool
        /// </summary>
        /// <param name="minThread"></param>
        /// <param name="maxThread"></param>
        /// <param name="fixedSize"></param>
        private GenericThreadPool(int minThread, int maxThread, /*int availableThreads,*/ bool fixedSize)
        {
            Console.WriteLine("Init GenericThreadPool ");
            //m_GenericName = genericName;
            if (maxThread <= 0)
            {
                throw new ArgumentException("maxThread required number > 0");
            }
            if (minThread <= 0 || minThread > maxThread)
            {
                throw new ArgumentException("minThread required number > 0 and less equal to max thread");
            }
            m_MaxThread = maxThread;
            m_MinThread = minThread;

            //set size settings
            m_FixedSize = fixedSize;
            if (m_MinThread == m_MaxThread)
            {
                m_FixedSize = true;
            }
            //m_AvailableThread = availableThreads;
            //if (m_FixedSize)
            //{
            //    //m_AvailableThread = m_MaxThread;
            //    m_FixedSize = fixedSize;
            //}
            //else if (availableThreads <= 0 || availableThreads > m_MaxThread || availableThreads < m_MinThread)
            //{
            //    throw new ArgumentException("availableThreads required number > 0 and less equal to max thread and bigger then min thread");
            //}
            //else
            //{
            //    //m_AvailableThread = availableThreads;
            //}

        }

        ~GenericThreadPool()
        {
            Dispose();
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            if (!disposed)
            {
                if (initilized)
                {
                    Abort();
                }

                if (null != m_shuttingDownEvent)
                {

                    m_shuttingDownEvent.Close();
                    m_shuttingDownEvent = null;
                }
                workerThreads.Clear();
                disposed = true;
                GC.SuppressFinalize(this);
            }
        }
        private void ValidateNotDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().ToString(), "The NetThreadPool has been shutdown");
            }
        }

        #endregion

        #region property

        ///// <summary>
        ///// Get GenericName
        ///// </summary>
        //public string GenericName
        //{
        //    get { return m_GenericName; }
        //}

        /// <summary>
        /// Get MaxThread
        /// </summary>
        public int MaxThread
        {
            get { return m_MaxThread; }
        }
        /// <summary>
        /// Get MinThread
        /// </summary>
        public int MinThread
        {
            get { return m_MinThread; }
        }

        /// <summary>
        /// Get CurrentWorkItems
        /// </summary>
        public int CurrentWorkItems
        {
            get { return /*GetThreadWorkerCount();*/ Thread.VolatileRead(ref m_CurrentWorkItems); }
        }
        /// <summary>
        /// Get if is a FixedSize
        /// </summary>
        public bool FixedSize
        {
            get { return m_FixedSize; }
        }
        ///// <summary>
        ///// Get AvailableThreads
        ///// </summary>
        //public int AvailableThreads
        //{
        //    get { return Thread.VolatileRead(ref m_AvailableThread); }
        //}
        /// <summary>
        /// Get or set SuspendTimeout in miliseconds
        /// </summary>
        public int SuspendTimeout
        {
            get { return m_Timeout; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("required value bigger then zero");
                }
                m_Timeout = value;
            }
        }
        /// <summary>
        /// Get or set Idle Time in seconds
        /// </summary>
        public int IdleSecondTime
        {
            get { return m_IdleSecondTime; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("required value bigger then zero");
                }
                m_IdleSecondTime = value;
            }
        }
        /*

        /// <summary>
        /// Get WorkFactorState
        /// </summary>
        public int WorkFactorState
        {
            get { return Thread.VolatileRead(ref m_WorkFactor); }
        }

        /// <summary>
        /// Get or set DecrementFactor between 5 and 100, when 5 is fast and 100 is slow, default=10
        /// </summary>
        public int DecrementFactor
        {
            get { return m_DecrementFactor; }
            set
            {
                if (value < 5)
                {
                    throw new ArgumentException("required value bigger or equal to 5");
                }
                m_DecrementFactor = value;
            }
        }
        /// <summary>
        /// Get or set IncrementFactor between 1 and 100, when 1 is fast and 100 is slow, default=5
        /// </summary>
        public int IncrementFactor
        {
            get { return m_IncrementFactor; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("required value bigger then 0");
                }
                m_IncrementFactor = value;
            }
        }

        /// <summary>
        /// Get Factor state
        /// </summary>
        public int IdleFactorState
        {
            get { return Thread.VolatileRead(ref m_IdleFactorState); }
        }
        /// <summary>
        /// Get Increment Factor state
        /// </summary>
        public int IncrementFactorState
        {
            get { return Thread.VolatileRead(ref m_IncrementFactorState); }
        }
         */
        #endregion

        #region MainThread

        ThreadStart m_threadStart;

        ///// <summary>
        ///// Start Thread Pool with Fixed size
        ///// </summary>
        ///// <param name="start"></param>
        //public virtual void StartThreadPool(ThreadStart start)
        //{
        //    StartThreadPool(start, m_MaxThread, true);
        //}
        ///// <summary>
        ///// Start Thread Pool with dynamic size, using ManualReset and Factor
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="availableThreads"></param>
        //public virtual void StartThreadPool(ThreadStart start, int availableThreads)
        //{
        //    StartThreadPool(start, availableThreads, false);

        //}

        /// <summary>
        /// Start Thread Pool
        /// </summary>
        /// <param name="start"></param>
        public void StartThreadPool(ThreadStart start)//, int availableThreads, bool fixedSize)
        {
            Console.WriteLine("Create GenericThreadPool GTP");

            try
            {
                if (initilized)
                {
                    return;
                }

                //if (fixedSize)
                //{
                //    m_AvailableThread = m_MaxThread;
                //    m_FixedSize = fixedSize;
                //}
                //else if (availableThreads <= 0 || availableThreads > m_MaxThread || availableThreads < m_MinThread)
                //{
                //    throw new ArgumentException("availableThreads required number > 0 and less equal to max thread and bigger then min thread");
                //}
                //else
                //{
                //    m_AvailableThread = availableThreads;
                //}
                m_threadStart = start;
                workItems.Clear();
                //workerThreads = new List<Thread>(m_MaxThread);
                for (int i = 0; i < MaxThread; i++)
                {
                    Thread workerThread = new Thread(start);
                    int num = i + 1;
                    string name = "GTP#" + num.ToString();
                    workerThread.Name = name;
                    workerThread.IsBackground = true;
                    workerThreads.Add(name, workerThread);
                    workItems[name] = new GTState();
                }
                //initilized = true;
                int counter = 0;
                foreach (DictionaryEntry o in workerThreads)
                {
                    if (m_FixedSize || counter < MinThread)//AvailableThreads)
                    {
                        Thread t = (Thread)o.Value;
                        ResumeThreadStart(t);
                        counter++;
                    }
                }

                //for (int i = 0; i < m_MaxThread; i++)
                //{
                //    if (m_FixedSize || i < AvailableThreads)
                //    {
                //        workerThreads[i].Start();
                //        IncrementWorkItemsCount();
                //    }
                //    Thread.Sleep(20);
                //}
                initilized = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("GTP Error:" + ex.Message);

            }
        }

        private void ResumeThreadStart(Thread t)
        {
            try
            {
                t.Start();
                workItems[t.Name].Started = true;
                Interlocked.Increment(ref m_CurrentWorkItems);
                //IncrementWorkItemsCount();
#if(debug)
                Console.WriteLine("ResumeThreadStart started:{0} ", t.Name);
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("GenericThreadPool ResumeThreadStart Error: " + ex.Message);
            }
        }
        private void ResumeThreadStart(Thread t, string name)
        {
            try
            {
                t = new Thread(m_threadStart);
                t.Name = name;
                t.IsBackground = true;
                t.Start();
                workItems[t.Name].Started = true;
                Interlocked.Increment(ref m_CurrentWorkItems);
                //IncrementWorkItemsCount();
#if(debug)
                Console.WriteLine("ResumeThreadStart started:{0} ", t.Name);
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("GenericThreadPool ResumeThreadStart Error: " + ex.Message);
            }
        }
        private void ResumeThreadStart(string name)
        {
            try
            {
                Thread t = (Thread)workerThreads[name];
                if (t == null)
                {
                    throw new Exception("ResumeThreadStart error: thread not exists in thread pool");
                }
                t = new Thread(m_threadStart);
                t.Name = name;
                t.IsBackground = true;
                t.Start();
                workItems[t.Name].Started = true;
                Interlocked.Increment(ref m_CurrentWorkItems);
                //IncrementWorkItemsCount();
#if(debug)
                Console.WriteLine("ResumeThreadStart started:{0} ", t.Name);
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("GenericThreadPool ResumeThreadStart Error: " + ex.Message);
            }
        }

        private void SuspendThread(Thread t)
        {
            try
            {

                if ((t.ThreadState & ThreadState.Unstarted) == 0 &&
                       (t.ThreadState & ThreadState.Background) != 0)
                {

                    t.Abort(t.Name);
                    t.Join(SuspendTimeout);
                }
                workItems[t.Name].Started = false;

                Interlocked.Decrement(ref m_CurrentWorkItems);

#if(debug)
                Console.WriteLine("SuspendThread :{0} ", t.Name);
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("GenericThreadPool SuspendThread Error: " + ex.Message);
            }
        }

        //private void ResumeThreadStart(int i)
        //{
        //    try
        //    {
        //        if (i >= MaxThread)
        //        {
        //            return;
        //        }
        //        Thread workerThread = new Thread(m_threadStart);
        //        workerThread.Name = GenericName + "#" + i.ToString();
        //        workerThread.IsBackground = true;
        //        workerThreads[i] = workerThread;
        //        workerThreads[i].Start();
        //        IncrementWorkItemsCount();

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("GenericThreadPool ResumeThreadStart Error: " + ex.Message);
        //    }
        //}
        /// <summary>
        /// StopThreadPool
        /// </summary>
        public virtual void StopThreadPool()
        {
            Console.WriteLine("Stop GenericThreadPool GTP");
            initilized = false;
            //RemoveThreads(m_MaxThread);

            Abort();

            //for (int i = 0; i < MaxThread; i++)
            //{
            //  //if( workerThreads[i].IsAlive)
            //  //{
            //  //    if(workerThreads[i].Join(m_Timeout))
            //  //      workerThreads[i].Abort();
            //  //}
            //  workerThreads[i].Abort();

            //}

            //workerThreads = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
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
            return m_IdleWaitHandle.WaitOne(millisecondsTimeout, false);
        }

        /// <summary>
        /// Force the NetThreadPool to shutdown
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
            Thread[] threads = null;
            lock (this.SyncRoot)
            {
                // Shutdown the work items queue
                //_workItemsQueue.Dispose();

                // Signal the threads to exit
                initilized = false;// _shutdown = true;
                m_shuttingDownEvent.Set();

                // Make a copy of the threads' references in the pool
                threads = new Thread[workerThreads.Count];
                workerThreads.Values.CopyTo(threads, 0);
            }

            int millisecondsLeft = millisecondsTimeout;
            DateTime start = DateTime.Now;
            bool waitInfinitely = (Timeout.Infinite == millisecondsTimeout);
            bool timeout = false;

            // Each iteration we update the time left for the timeout.
            foreach (Thread thread in threads)
            {
                // Join don't work with negative numbers
                if (!waitInfinitely && (millisecondsLeft < 0))
                {
                    timeout = true;
                    break;
                }
                bool success = false;

                if (!((thread.ThreadState & ThreadState.Stopped) != 0 ||
                        (thread.ThreadState & ThreadState.Unstarted) != 0))
                    success = thread.Join(millisecondsLeft);
                if (!success)
                {
                    timeout = true;
                    break;
                }

                if (!waitInfinitely)
                {
                    // Update the time left to wait
                    TimeSpan ts = DateTime.Now - start;
                    millisecondsLeft = millisecondsTimeout - (int)ts.TotalMilliseconds;
                }
            }

            if (timeout && forceAbort)
            {
                // Abort the threads in the pool
                foreach (Thread thread in threads)
                {
                    if ((thread != null) && thread.IsAlive)
                    {
                        try
                        {
                            thread.Abort("Shutdown");
                        }
                        catch (SecurityException)
                        {
                            //e = e;
                        }
                        catch (ThreadStateException)
                        {
                            //ex = ex;
                            // In case the thread has been terminated 
                            // after the check if it is alive.
                        }
                    }
                }
            }
        }

        #endregion

        #region ICollection

        public bool IsSynchronized
        {
            get
            {
                if (workerThreads != null)
                {
                    return ((ICollection)this.workerThreads).IsSynchronized;
                }
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                if (workerThreads != null)
                {
                    return ((ICollection)this.workerThreads).SyncRoot;
                }
                return null;
            }
        }

        //public void CopyTo(Thread[] array, int arrayIndex)
        //{
        //    Array.Copy(this.workerThreads.ToArray(), 0, array, arrayIndex, this.workerThreads.Count);
        //}

        //public IEnumerator<Thread> GetEnumerator()
        //{
        //    if (workerThreads != null)
        //    {
        //        return workerThreads.GetEnumerator();
        //    }
        //    return null;
        //}


        #endregion

        #region managed threadPool

        private int m_CurrentWorkItems = 0;
        private DateTime m_LastFull;
        private DateTime m_LastWorked;
        private int m_IsFull;
        private int m_IdleSecondTime = 60;
        private object m_lock = new object();

        private int SetPoolState(GenericThradState initialState)
        {
            if (m_FixedSize)
                return 0;

            //lock (m_lock)
            //{
            if (initialState != GenericThradState.Idle)
            {
                if (m_CurrentWorkItems /*>= m_AvailableThread && m_AvailableThread*/ < m_MaxThread)
                {

                    if (m_IsFull > 0)
                    {
                        if (DateTime.Now.Subtract(m_LastFull).TotalSeconds > m_IdleSecondTime)
                        {
                            Interlocked.Exchange(ref m_IsFull, 0);
                            return 1;
                        }
                    }
                    else
                    {
                        m_LastFull = DateTime.Now;
                        Interlocked.Exchange(ref m_IsFull, 1);
                    }
                }
                m_LastWorked = DateTime.Now;
            }
            else //if (initialState == GenericThradState.Idle)
            {
                if (m_CurrentWorkItems /*<= m_AvailableThread && m_AvailableThread*/ > m_MinThread)
                {

                    if (DateTime.Now.Subtract(m_LastWorked).TotalSeconds > m_IdleSecondTime)
                    {
                        Interlocked.Exchange(ref m_IsFull, 0);
                        return -1;
                    }
                }
            }

            return 0;
            // }
        }


        private int GetThreadWorkerCount()
        {
            int count = 0;
            lock (workerThreads.SyncRoot)
            {
                foreach (object o in workerThreads.Values)
                {
                    Thread t = (Thread)o;
                    if ((t.ThreadState & ThreadState.Stopped) != 0 ||
                        (t.ThreadState & ThreadState.Unstarted) != 0)
                        count++;
                    //Console.WriteLine("ThreadWorkerCount ThreadState:{0} " + t.ThreadState);

                }
            }
            return MaxThread - count;
        }

        private bool CanIncrement()
        {
            if (!initilized)
                return false;
            //int available = AvailableThreads;
            //int count = GetThreadWorkerCount();
            //return (count >= available && available < MaxThread);

            return m_CurrentWorkItems < m_MaxThread;
        }
        private bool CanDecrement()
        {
            if (!initilized)
                return false;
            //int available = AvailableThreads;
            //int count = GetThreadWorkerCount();
            //return (count <= available && available > MinThread);

            return m_CurrentWorkItems > m_MinThread;
        }


        //private bool TryIncrementAvailableThreads()
        //{
        //    if (!CanIncrement())
        //        return false;
        //    int available = AvailableThreads;
        //    Interlocked.Increment(ref m_AvailableThread);
        //    return AvailableThreads > available;

        //}
        //private bool TryDecrementAvailableThreads()
        //{
        //    if (!CanDecrement())
        //        return false;
        //    int available = AvailableThreads;
        //    Interlocked.Decrement(ref m_AvailableThread);
        //    return AvailableThreads < available;
        //}

        //public bool IncrementAvailableThreads()
        //{
        //    return TryIncrementAvailableThreads();
        //}

        //private bool DecrementAvailableThreads()
        //{
        //    return TryDecrementAvailableThreads();
        //}

        private void AddThreadWorker()
        {

            bool found = false;
            //we try to find stopped thread to resycle

            lock (((ICollection)workItems).SyncRoot)
            {
                foreach (KeyValuePair<string, GTState> gt in workItems)
                {

                    if (gt.Value.CanResume())
                    {
                        Thread t = (Thread)workerThreads[gt.Key];
                        ResumeThreadStart(t, gt.Key);
                        found = true;
                        break;
                    }
                }
            }

            //foreach (DictionaryEntry o in workerThreads)
            //{
            //    Thread t = (Thread)o.Value;
            //    if ((t.ThreadState & ThreadState.Stopped) != 0 ||
            //        (t.ThreadState & ThreadState.Aborted) != 0)
            //    {
            //        ResumeThreadStart(t, o.Key.ToString());
            //        found = true;
            //        break;
            //    }
            //}
            if (found)
                return;
            lock (workerThreads.SyncRoot)
            {
                foreach (DictionaryEntry o in workerThreads)
                {
                    Thread t = (Thread)o.Value;
                    if ((t.ThreadState & ThreadState.Unstarted) != 0)
                    {
                        ResumeThreadStart(t);
                        found = true;
                        break;
                    }
                }
            }
        }

        private void RemoveThreadWorker()
        {

            lock (((ICollection)workItems).SyncRoot)
            {
                foreach (KeyValuePair<string, GTState> gt in workItems)
                {

                    if (gt.Value.CanSuspend(m_IdleSecondTime))
                    {
                        Thread t = (Thread)workerThreads[gt.Key];
                        SuspendThread(t);
                        //found = true;
                        break;
                    }
                }
            }

            //            lock (workerThreads.SyncRoot)
            //            {
            //                foreach (DictionaryEntry o in workerThreads)
            //                {
            //                    Thread t = (Thread)o.Value;
            //                    if ((t.ThreadState & ThreadState.WaitSleepJoin) != 0 ||
            //                        (t.ThreadState & ThreadState.Background) != 0)
            //                    {

            //                        t.Abort(o.Key.ToString());
            //                        t.Join(SuspendTimeout);
            //                        workItems[t.Name].Suspend();
            //                        Interlocked.Decrement(ref m_CurrentWorkItems);
            //                        //DecrementWorkItemsCount();
            //#if(debug)
            //                        Console.WriteLine("RemoveThreadWorker removed:{0} " + t.Name);
            //#endif
            //                        break;
            //                    }
            //                }
            //            }
        }


        private void CheckTreadPoolState(GenericThradState initialState)
        {
            if (!initilized)
                return;

            int state = SetPoolState(initialState);
            //Console.WriteLine("PoolState: {0}",state);

            if (state < 0)
            {
                //RemoveThreads(1);
                if (m_CurrentWorkItems > MinThread)//TryDecrementAvailableThreads())
                {
                    RemoveThreadWorker();
                }
            }
            else if (state > 0)
            {
                //AddThreads(1);
                if (m_CurrentWorkItems < MaxThread)//TryIncrementAvailableThreads())
                {
                    AddThreadWorker();
                }
            }
        }

        /*
        private void CheckTreadPoolState(GenericThradState initialState)
        {
            if (!initilized)
                return;

            int available = AvailableThreads;

            if (initialState == GenericThradState.WorkBegin)
            {

                IncrementWorkFactorSate();

                if (available < m_MaxThread && WorkFactorState >= available)
                {
                    Interlocked.Increment(ref m_IncrementFactorState);

                    if (IncrementFactorState > IncrementFactor)
                    {
                        //AddThreads(1);
                        if (TryIncrementAvailableThreads())
                        {
                            AddThreadWorker();
                            Interlocked.Exchange(ref m_IncrementFactorState, 0);
                        }
                    }
                }

                Interlocked.Exchange(ref m_IdleFactorState, 0);
            }
            else if (initialState == GenericThradState.WorkEnd)
            {
                DecremenWorkFactorSate();
            }
            else if (initialState == GenericThradState.Idle)
            {
                //int workItems = GetThreadWorkerCount();// CurrentWorkItems;

                Interlocked.Exchange(ref m_IncrementFactorState, 0);

                Interlocked.Increment(ref m_IdleFactorState);

                //if (workItems > 1 && workItems < available)
                //{

                if (IdleFactorState > DecrementFactor)
                {
                    //RemoveThreads(1);
                    if (TryDecrementAvailableThreads())
                    {
                        RemoveThreadWorker();
                        Interlocked.Exchange(ref m_IdleFactorState, 0);
                    }
                }
            }
        }
        */
        /// <summary>
        /// Set ManualReset
        /// </summary>
        /// <param name="initialState">true= WorkBegin, false=WorkEnd </param>
        /// <param name="timeToSleep">time to sleep in milisconds</param>
        public void ManualReset(bool initialState, int timeToSleep)
        {
            ManualReset(initialState ? GenericThradState.WorkBegin : GenericThradState.WorkEnd);
            if (timeToSleep > 0)
            {
                Thread.Sleep(timeToSleep);
            }
        }
        /// <summary>
        /// Set ManualReset
        /// </summary>
        /// <param name="initialState"></param>
        public void ManualReset(bool initialState)
        {
            ManualReset(initialState ? GenericThradState.WorkBegin : GenericThradState.WorkEnd);
        }
        /// <summary>
        /// Set ManualIdle
        /// </summary>
        public void ManualIdle(int timeToSleep)
        {
            ManualReset(GenericThradState.Idle);
            if (timeToSleep > 0)
            {
                Thread.Sleep(timeToSleep);
            }
        }
        /// <summary>
        /// Set ManualIdle
        /// </summary>
        /// <param name="initialState"></param>
        public void ManualReset(GenericThradState initialState)
        {
            if (!m_FixedSize)
            {
                //if (initialState== GenericThradState.WorkBegin)
                //    IncrementWorkItemsCount();
                //else if(initialState == GenericThradState.WorkFinished)
                //    DecrementWorkItemsCount();

                //if (m_AutoThreadSettings)
                CheckTreadPoolState(initialState);
            }
        }


        //private void IncrementWorkFactorSate()
        //{
        //    Interlocked.Increment(ref m_WorkFactor);
        //    //Console.WriteLine("m_FactorState: " + m_FactorState.ToString());
        //}

        //private void DecremenWorkFactorSate()
        //{
        //    if (WorkFactorState <= 0)
        //        return;
        //    Interlocked.Decrement(ref m_WorkFactor);
        //    //Console.WriteLine("m_FactorState: " + m_FactorState.ToString());
        //}


        #endregion

        #region override

        /// <summary>
        /// OnErrorOcurred
        /// </summary>
        /// <param name="msg"></param>
        private void OnErrorOcurred(string msg)
        {
            OnErrorOcurred(new ErrorOcurredEventArgs(msg));
        }
        /// <summary>
        /// OnErrorOcurred
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnErrorOcurred(ErrorOcurredEventArgs e)
        {
            if (ErrorOcurred != null)
                ErrorOcurred(this, e);
        }

        #endregion

        #region _Absolete

        /*
        //private int m_CurrentWorkItems = 0;
         //private void IncrementWorkItemsCount()
        //{
        //    Interlocked.Increment(ref m_CurrentWorkItems);
        //    //Console.WriteLine("CurrentWorkItems: " + m_CurrentWorkItems.ToString());
        //}

        //private void DecrementWorkItemsCount()
        //{
        //    if (CurrentWorkItems <= 0)
        //        return;
        //    Interlocked.Decrement(ref m_CurrentWorkItems);
        //    //Console.WriteLine("CurrentWorkItems: " + m_CurrentWorkItems.ToString());
        //}

       [Obsolete("not used")]
        private int m_CurrentThreads = 0;
        [Obsolete("not used")]
        private byte m_SettingInterval = 3;
        [Obsolete("not used")]
        private bool m_AutoThreadSettings = false;
        
        /// <summary>
        /// WorkerProcess
        /// </summary>
        public event EventHandler WorkerProcess;
 
         
       /// <summary>
         /// AutoThreadSettings
         /// </summary>
         public bool AutoThreadSettings
         {
             get { return m_AutoThreadSettings; }
             set
             {
                 if (m_FixedSize)
                 {
                     throw new ArgumentException("AutoThreadSettings required not FixedSize property");
                 }
                 m_AutoThreadSettings = value;
             }
         }
        [Obsolete("not used")]
        public void AddThreads(byte threadsCount)
        {
            // Don't start threads on shut down
            if (!initilized || m_FixedSize)
            {
                return;
            }
            try
            {
                lock (((ICollection)workerThreads).SyncRoot)
                {
                    int threadNum = AvailableThreads;

                    for (byte i = 0; i < threadsCount; ++i)
                    {
                        // Don't create more threads then the upper limit
                        if (threadNum >= m_MaxThread)
                        {
                            return;
                        }


                        Console.WriteLine("AddThreads [{0}] ThreadState:{1}", workerThreads[threadNum].Name, workerThreads[threadNum].ThreadState);

                        Interlocked.Increment(ref m_AvailableThread);

                        if ((workerThreads[threadNum].ThreadState & ThreadState.Unstarted) != 0)
                        {
                            workerThreads[threadNum].Start();
                            IncrementWorkItemsCount();
                        }
                        else if ((workerThreads[threadNum].ThreadState & ThreadState.Stopped) != 0)
                        {
                            ResumeThreadStart(threadNum); break;
                        }
                        else if ((workerThreads[threadNum].ThreadState & ThreadState.WaitSleepJoin) != 0)
                        {
                            ResumeThreadStart(threadNum); break;
                        }
                        else if ((workerThreads[threadNum].ThreadState & ThreadState.Background) != 0)
                        {
                            ResumeThreadStart(threadNum); break;
                        }
                        else
                        {
                            return;
                        }

                        //m_AvailableThread++;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GenericThreadPool AddThreads Error: " + ex.Message);
            }
        }


        [Obsolete("not used")]
        public void RemoveThreads(byte threadsCount)
        {
            if (!initilized || m_FixedSize)
            {
                return;
            }
            try
            {
                lock (((ICollection)workerThreads).SyncRoot)
                {

                    for (byte i = threadsCount; i > 0; i--)
                    {
                        // Don't remove more threads then the lower limit
                        if (workerThreads.Count <= 0 || AvailableThreads <= m_MinThread)
                        {
                            return;
                        }

                        Thread workerThread = workerThreads[AvailableThreads - 1];
                        if (workerThread != null)
                        {
                            Console.WriteLine("RemoveThreads [{0}] ThreadState:{1}", workerThread.Name, workerThread.ThreadState);

                            if (workerThread.ThreadState == ThreadState.Running)
                            {
                                //Thread.Sleep(100);
                                return;
                            }

                            try
                            {

                                workerThread.Abort("reset");
                                workerThread.Join(SuspendTimeout);
                                DecrementWorkItemsCount();
                            }
                            catch (ThreadAbortException abortException)
                            {
                                Console.WriteLine((string)abortException.ExceptionState);
                            }
                            Console.WriteLine("ThreadState:{0}", workerThread.ThreadState);

                        }
                        Interlocked.Decrement(ref m_AvailableThread);
                        //m_AvailableThread--;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GenericThreadPool AddThreads Error: " + ex.Message);
            }
        }
 
        /// <summary>
        /// Get CurrentThreadsWorker
        /// </summary>
        [Obsolete("use AvailableThreads insted ")]
        public int CurrentThreadsWorker
        {
            get { return m_CurrentThreads; }
        }
        /// <summary>
        /// Get or Set the interval work items to increment or decrement the available thread count
        /// </summary>
        [Obsolete("use Factor insted ")]
        public byte AutoThreadInterval
        {
            get { return m_SettingInterval; }
            set
            {
                if (m_FixedSize)
                {
                    throw new ArgumentException("AutoThreadInterval required not FixedSize property");
                }
                m_SettingInterval = value;
            }
        }
        /// <summary>
        /// StartThreadPool
        /// </summary>
        /// <param name="availableThreads"></param>
        [Obsolete("use StartThreadPool(ThreadStart start) insted ")]
        public virtual void StartThreadPool(int availableThreads)
        {
            StartThreadPool(new ThreadStart(GenericWorkerInternal), availableThreads, false);
        }

        /// <summary>
        /// StartThreadPool
        /// </summary>
        /// <param name="fixedSize"></param>
        [Obsolete("use StartThreadPool(ThreadStart start) insted ")]
        public virtual void StartThreadPool(bool fixedSize)
        {

            StartThreadPool(new ThreadStart(GenericWorkerInternal), m_MaxThread, true);


        }

        /// <summary>
        /// GenericWorkerInternal
        /// </summary>
        [Obsolete("not used")]
        private void GenericWorkerInternal()
        {
            while (initilized)
            {
                try
                {
                    Interlocked.Increment(ref m_CurrentThreads);
                    //Console.WriteLine("CurrentThreadsWorker: " + m_CurrentThreads.ToString());
                    GenericThreadPoolWorker();
                    //if (!m_FixedSize)
                    //{
                    //    CheckTreadPoolState();
                    //}
                }
                finally
                {
                    Interlocked.Decrement(ref m_CurrentThreads);
                    Thread.Sleep(20);
                    //Console.WriteLine("CurrentThreadsWorker: " + m_CurrentThreads.ToString());
                }
            }
        }


        /// <summary>
        /// GenericThreadPoolWorker
        /// </summary>
        [Obsolete("not used")]
        protected virtual void GenericThreadPoolWorker()
        {
            if (WorkerProcess != null)
                WorkerProcess(this, EventArgs.Empty);
        }

        /// <summary>Async WorkItem to the thread pool.</summary>
        /// <param name="callback">
        /// A WaitCallback representing the delegate to invoke when the thread in the 
        /// thread pool picks up the work item.
        /// </param>
        [Obsolete("not used")]
        public void AsyncWorkItem(WaitCallback callback)
        {
            // Queue the delegate with no state
            AsyncWorkItem(callback, null);
        }

        /// <summary>AsyncWork Item to the thread pool.</summary>
        /// <param name="callback">
        /// A WaitCallback representing the delegate to invoke when the thread in the 
        /// thread pool picks up the work item.
        /// </param>
        /// <param name="state">
        /// The object that is passed to the delegate when serviced from the thread pool.
        /// </param>
        [Obsolete("not used")]
        public void AsyncWorkItem(WaitCallback callback, object state)
        {
            // Create a waiting callback that contains the delegate and its state.
            // At it to the processing queue, and signal that data is waiting.
            //WaitingCallback waiting = new WaitingCallback(callback, state);
            //lock (_lockablePool) { _waitingCallbacks.Enqueue(waiting); }
            //_workerThreadNeeded.AddOne();
        }


        [Obsolete("use ManualReset(GenericThradState initialState) insted ")]
        public void ManualReset(bool initialState)
        {
            if (!m_FixedSize)
            {
                if (initialState)
                    IncrementWorkItemsCount();
                else
                    DecrementWorkItemsCount();
                if (m_AutoThreadSettings)
                    CheckTreadPoolState(initialState);
            }
        }

        [Obsolete("use CheckTreadPoolState(GenericThradState initialState) insted ")]
        private void CheckTreadPoolState(bool initialState)
        {
            float interval = 0;

            int workItems = CurrentWorkItems;
            int available = AvailableThreads;

            if (initialState)
            {
                if (available < m_MaxThread && workItems >= available)
                {
                    interval = (float)((m_MaxThread - available) * .3);
                    //if (interval < (float)CurrentWorkItems)
                    //    AddThreads(1);
                }
            }
            else
            {
                if (workItems > 0 && workItems < available)
                {

                    interval = (float)((available) * .3);
                    //if (interval > (float)m_CurrentWorkItems)
                    //    RemoveThreads(1);
                }
            }
            //m_SettingCouter = 0;
            //    Interlocked.Exchange(ref m_SettingCouter, 0);
            //}
        }
      */
        #endregion

    }
}



    

