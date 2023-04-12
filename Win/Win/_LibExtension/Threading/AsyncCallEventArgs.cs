using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Extension.Nistec.Threading
{
    #region ExecutingResultEvent

    public enum AsyncProgressLevel
    {
        None = 0,
        Info = 1,
        Progress = 2,
        Error = 3,
        All = 6
    }
    public enum AsyncState
    {
        None,
        Started,
        Completed,
        Canceled
    }

    public delegate void AsyncResultEventHandler(object sender, AsyncResultEventArgs e);
    public delegate void AsyncProgressEventHandler(object sender, AsyncProgressEventArgs e);
    public delegate void AsyncCallEventHandler(object sender, AsyncCallEventArgs e);
    public delegate void AsyncDataResultEventHandler(object sender, AsyncDataResultEventArgs e);

    public class AsyncResultEventArgs : EventArgs
    {
        private IAsyncResult _Result;
        public AsyncResultEventArgs(IAsyncResult result)
        {
            _Result = result;
        }
        public IAsyncResult Result
        {
            get { return _Result; }
        }
    }

    public class AsyncCallEventArgs : EventArgs
    {
        private object _Result;
        public AsyncCallEventArgs(object result)
        {
            _Result = result;
        }
        public object Result
        {
            get { return _Result; }
        }
    }
    public class AsyncProgressEventArgs : EventArgs
    {
        public readonly string Message;
        public readonly AsyncProgressLevel Level;
        public AsyncProgressEventArgs(string s, AsyncProgressLevel lvl)
        {
            Message = s;
            Level = lvl;
        }
    }

    #endregion

    #region ExecutingResultEvent


    public class AsyncDataResultEventArgs : EventArgs
    {
        private DataTable _Table;
        public AsyncDataResultEventArgs(DataTable dt)
        {
            _Table = dt;
        }
        public DataTable Table
        {
            get { return _Table; }
        }
    }


    #endregion


}
