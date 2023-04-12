using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Messaging;
using mControl.Util;

namespace mControl.Threading
{

    /// <summary>
    /// AsyncChannelBase
    /// </summary>
    public abstract class  AsyncThreadPool:IDisposable 
    {
	
		#region Members

        static bool initilized;
        /// <summary>
        /// Hashtable of all the threads in the thread pool.
        /// </summary>
        private Hashtable mainThreads = Hashtable.Synchronized(new Hashtable());

        //private List<Thread> mainThread;
        //private List<int> mainThreadStatus;
        private string m_ChannelName;
        private int m_MaxThread = 1;
        private int m_MinThread = 1;
        private int m_CurrentThreads = 1;
        private int m_CurrentWorkItems = 0;
        
        private int m_SettingCouter = 0;
        private int m_SettingInterval = 10;
        //private bool m_Synchronization = false;
        private bool m_FixedSize = false;

        /// <summary>
        /// Signaled when the thread pool is idle, i.e. no thread is busy
        /// and the work items queue is empty
        /// </summary>
        private ManualResetEvent _IdleWaitHandle = new ManualResetEvent(true);


         /// <summary>
        /// ErrorOcurred
        /// </summary>
        public event ErrorOcurredEventHandler ErrorOcurred;
  
		#endregion

		#region Constructor
        /// <summary>
        /// AsyncThreadPool
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="maxThread"></param>
        public AsyncThreadPool(string channelName, int minThread, int maxThread)
		{
            Console.WriteLine("Init AsyncThreadPool ");
            m_ChannelName = channelName;
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
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            if (AsyncThreadPool.initilized)
            {
            }
     
        }

 		#endregion

        #region property

        /// <summary>
        /// ChannelName
        /// </summary>
        public string ChannelName
        {
            get{return m_ChannelName;}
        }
        /// <summary>
        /// MaxThread
        /// </summary>
        public int MaxThread
        {
            get { return m_MaxThread; }
        }
        /// <summary>
        /// MinThread
        /// </summary>
        public int MinThread
        {
            get { return m_MinThread; }
         }
        /// <summary>
         /// CurrentThreads
        /// </summary>
        public int CurrentThreads
        {
            get { return m_CurrentThreads; }
        }
        /// <summary>
        /// CurrentWorkItems
        /// </summary>
        public int CurrentWorkItems
        {
            get { return m_CurrentWorkItems; }
        }
        /// <summary>
        /// FixedSize
        /// </summary>
        public bool FixedSize
        {
            get { return m_FixedSize; }
        }
        #endregion

        #region MainThread
        /// <summary>
        /// StartAsyncThreadPool
        /// </summary>
        public virtual void StartAsyncThreadPool(bool fixedSize)
        {
            Console.WriteLine("Create AsyncThreadPool " + ChannelName);

            AsyncThreadPool.initilized = true;
            if (fixedSize)
                AddThreads(m_MaxThread);
            else
                AddThreads(m_MinThread);

            //try
            //{
            //    mainThreads = new List<Thread>(m_MinThread);
            //    mainThreadStatus = new List<int>(m_MinThread);
            //    for (int i = 0; i < MaxThread; i++)
            //    {
            //        mainThreads[i] = new Thread(new ThreadStart(AsyncThreadPoolWorker));
            //        mainThreads[i].Name = ChannelName+i.ToString();
            //        mainThreads[i].IsBackground = true;
            //        mainThreads[i].Start();

            //        Thread.Sleep(20);
            //    }
            //    AsyncThreadPool.initilized = true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ChannelName + " Error:" + ex.Message);

            //}
        }

        /// <summary>
        /// StopAsyncThreadPool
        /// </summary>
        public virtual void StopAsyncThreadPool()
        {
            Console.WriteLine("Stop AsyncThreadPool " + ChannelName);
            AsyncThreadPool.initilized = false;
            RemoveThreads(m_MaxThread);

            //for (int i = 0; i < MaxThread; i++)
            //{
            //    mainThreads[i].Join();
            //}

            mainThreads = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion

        #region managed threadPool

        private void CheckTreadPoolState()
        {
            //m_SettingCouter++;
            Interlocked.Increment(ref m_SettingCouter);
            if (m_SettingCouter > m_SettingInterval)
            {
                if (m_CurrentWorkItems < m_CurrentThreads)
                    RemoveThreads(1);
                else if (m_CurrentWorkItems == m_CurrentThreads)
                    AddThreads(1);

                //m_SettingCouter = 0;
                Interlocked.Exchange(ref m_SettingCouter,0);
            }
        }


        public void AddThreads(int threadsCount)
        {
            lock (mainThreads.SyncRoot)
            {
                // Don't start threads on shut down
                if (!AsyncThreadPool.initilized)
                {
                    return;
                }

                for (int i = 0; i < threadsCount; ++i)
                {
                    // Don't create more threads then the upper limit
                    if (mainThreads.Count >= m_MaxThread)
                    {
                        return;
                    }

                    // Create a new thread
                    Thread workerThread = new Thread(new ThreadStart(AsyncWorkerRunning));
                    int threadNum = m_CurrentThreads + 1;
                    // Configure the new thread and start it
                    workerThread.Name = m_ChannelName + threadNum.ToString();
                    workerThread.IsBackground = true;
                    workerThread.Start();
                    ++m_CurrentThreads;

                    // Add the new thread to the hashtable and update its creation
                    // time.
                    //mainThreads[workerThread] = DateTime.Now;
                    mainThreads[threadNum] = workerThread;
                }
            }
        }


        public void RemoveThreads(int threadsCount)
        {
            lock (mainThreads.SyncRoot)
            {
                // Don't start threads on shut down
                //if (!AsyncThreadPool.initilized)
                //{
                //    return;
                //}

                for (int i = threadsCount; i >0; i--)
                {
                    // Don't create more threads then the upper limit
                    if (mainThreads.Count <= 0 || i <= m_MinThread)
                    {
                        return;
                    }

                    Thread workerThread =(Thread)mainThreads[i];
                    workerThread.Join();
              
                    m_CurrentThreads--;
                }
            }
        }

        public void ManualReset(bool initialState)
        {
            if (!m_FixedSize)
            {
                if (initialState)
                    IncrementWorkItemsCount();
                else
                    DecrementWorkItemsCount();
            }
        }

        private void  IncrementWorkItemsCount()
        {
            int count = Interlocked.Increment(ref m_CurrentWorkItems);
            if (count == 1)
            {
                _IdleWaitHandle.Reset();
            }
        }

        private void DecrementWorkItemsCount()
        {
            int count = Interlocked.Decrement(ref m_CurrentWorkItems);
            if (count == 0)
            {
                _IdleWaitHandle.Set();
            }
        }

        #endregion

        #region override

        /// <summary>
        /// AsyncWorkerRunning
        /// </summary>
        private void AsyncWorkerRunning()
        {
            while (AsyncThreadPool.initilized)
            {
                    AsyncThreadPoolWorker();
                    //if (!m_FixedSize)
                    //{
                    //    CheckTreadPoolState();
                    //}
            }
        }

  
        /// <summary>
        /// AsyncThreadPoolWorker
        /// </summary>
        protected virtual void AsyncThreadPoolWorker()
        {
    
        }

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

	}
}


