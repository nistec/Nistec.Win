using System;

namespace MControl.Threading
{
	/// <summary>
	/// Summary description for ThreadPoolStateInfo.
	/// </summary>
	public class ThreadPoolStateInfo
	{
        /// <summary>
        /// Idle timeout in milliseconds.
        /// If a thread is idle for _idleTimeout milliseconds then 
        /// it may quit.
        /// </summary>
        private int _idleTimeout;

        /// <summary>
        /// The lower limit of threads in the pool.
        /// </summary>
        private int _minWorkerThreads;

        /// <summary>
        /// The upper limit of threads in the pool.
        /// </summary>
        private int _maxWorkerThreads;

        /// <summary>
        /// Use the caller's security context
        /// </summary>
        private bool _useCallerContext;

        /// <summary>
        /// Dispose of the state object of a work item
        /// </summary>
        private bool _disposeOfStateObjects;

        /// <summary>
        /// The option to run the post execute
        /// </summary>
        private PostExecuteCall _postExecuteCalling;

		/// <summary>
		/// A post execute callback to call when none is provided in 
		/// the QueueWorkItem method.
		/// </summary>
		private PostExecuteWorkItemCallback _postExecuteWorkItemCallback;
        
		public ThreadPoolStateInfo()
		{
            _idleTimeout = AsyncThreadPool.DefaultIdleTimeout;
            _minWorkerThreads = AsyncThreadPool.DefaultMinWorkerThreads;
            _maxWorkerThreads = AsyncThreadPool.DefaultMaxWorkerThreads;
            _useCallerContext = AsyncThreadPool.DefaultUseCallerContext;
            _disposeOfStateObjects = AsyncThreadPool.DefaultDisposeOfStateObjects;
            _postExecuteCalling = AsyncThreadPool.DefaultCallToPostExecute;
			_postExecuteWorkItemCallback = AsyncThreadPool.DefaultPostExecuteWorkItemCallback;
		}

        public ThreadPoolStateInfo(ThreadPoolStateInfo tpStateInfo)
        {
            _idleTimeout = tpStateInfo._idleTimeout;
            _minWorkerThreads = tpStateInfo._minWorkerThreads;
            _maxWorkerThreads = tpStateInfo._maxWorkerThreads;
            _useCallerContext = tpStateInfo._useCallerContext;
            _disposeOfStateObjects = tpStateInfo._disposeOfStateObjects;
            _postExecuteCalling = tpStateInfo._postExecuteCalling;
			_postExecuteWorkItemCallback = tpStateInfo._postExecuteWorkItemCallback;
        }

        public int IdleTimeout
        {
            get { return _idleTimeout; }
            set { _idleTimeout = value; }
        }

        public int MinWorkerThreads
        {
            get { return _minWorkerThreads; }
            set { _minWorkerThreads = value; }
        }

        public int MaxWorkerThreads
        {
            get { return _maxWorkerThreads; }
            set { _maxWorkerThreads = value; }
        }

        public bool UseCallerContext
        {
            get { return _useCallerContext; }
            set { _useCallerContext = value; }
        }

        public bool DisposeOfStateObjects
        {
            get { return _disposeOfStateObjects; }
            set { _disposeOfStateObjects = value; }
        }

        public PostExecuteCall PostExecuteCall
        {
            get { return _postExecuteCalling; }
            set { _postExecuteCalling = value; }
        }

		public PostExecuteWorkItemCallback PostExecuteWorkItemCallback
		{
			get { return _postExecuteWorkItemCallback; }
			set { _postExecuteWorkItemCallback = value; }
		}
	}
}
