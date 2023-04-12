using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using MControl.Threading;

namespace MControl.Generic
{

    public class DynamicInvoker
    {
       //InvokeAsync(new InvokePublishDelegate(InvokeCampaignPublish), null);

        delegate void InvokePublishDelegate();

        static void Invoke(params object[] args)
        {
 
            DynamicInvoker invoker = new DynamicInvoker();
            invoker.InvokeAsync(new InvokePublishDelegate(InvokePublisher), null);
        }

        static void InvokePublisher()
        {

        }

        public DynamicInvoker()
        {
            wrapperInstance = new DelegateWrapper(InvokeWrappedDelegate);
        }
        #region Invoke async

        /// <summary>
        /// Delegate to wrap another delegate and its arguments
        /// </summary>
        delegate void DelegateWrapper(Delegate d, object[] args);

        /// <summary>
        /// An instance of DelegateWrapper which calls InvokeWrappedDelegate,
        /// which in turn calls the DynamicInvoke method of the wrapped
        /// delegate.
        /// </summary>
        DelegateWrapper wrapperInstance;// = new DelegateWrapper(InvokeWrappedDelegate);

        ///// <summary>
        ///// Callback used to call <code>EndInvoke</code> on the asynchronously
        ///// invoked DelegateWrapper.
        ///// </summary>
        //AsyncCallback callback = new AsyncCallback(EndWrapperInvoke);

        /// <summary>
        /// Executes the specified delegate with the specified arguments
        /// asynchronously on a thread pool thread.
        /// </summary>
        public void InvokeAsync(Delegate d, params object[] args)
        {
            AsyncCallback callback = new AsyncCallback(EndWrapperInvoke);


            // Invoke the wrapper asynchronously, which will then
            // execute the wrapped delegate synchronously (in the
            // thread pool thread)
            wrapperInstance.BeginInvoke(d, args, callback, null);
        }

        /// <summary>
        /// Invokes the wrapped delegate synchronously
        /// </summary>
        void InvokeWrappedDelegate(Delegate d, object[] args)
        {
            d.DynamicInvoke(args);
        }

        /// <summary>
        /// Calls EndInvoke on the wrapper and Close on the resulting WaitHandle
        /// to prevent resource leaks.
        /// </summary>
        void EndWrapperInvoke(IAsyncResult ar)
        {
            wrapperInstance.EndInvoke(ar);
            ar.AsyncWaitHandle.Close();
        }

        #endregion

    }

    public class WaitHandleEvent:IDisposable
    {
        #region ctor

        public WaitHandleEvent(bool initialState)
        {
            ResetEvent = new ManualResetEvent(initialState);
            EventState = -1;
        }

        public WaitHandleEvent(bool initialState, int timeout)
        {
            ResetEvent = new ManualResetEvent(initialState);
            Timeout = timeout;
            EventState = -1;
        }

        public void Dispose()
        {
            if (ResetEvent != null)
            {
                ((IDisposable)ResetEvent).Dispose();
                ResetEvent = null;
            }
            //if (action != null)
            //{
            //    action = null;
            //}
        }
        #endregion

        #region ManualResetEvent

        public ManualResetEvent ResetEvent { get; set; }
        int EventState;

        public bool IsCompleted
        {
            get { return EventState == 0; }
        }

        /// <summary>
        /// Sets the state of the event to nonsignaled, causing threads to block.
        /// </summary>
        /// <returns>true if the operation succeeds; otherwise, false.</returns>
        public bool Set()
        {
            ManualResetEvent resetEvent = (ManualResetEvent)ResetEvent;
            if (resetEvent != null)
            {
                EventState = 0;
                return resetEvent.Set();
            }
            return false;
        }

        /// <summary>
        /// Blocks the current thread until the current instance receives a signal, using
        ///     a System.TimeSpan to measure the time interval.
        /// </summary>
        /// <returns></returns>
        public bool WaitOne()
        {
            ManualResetEvent resetEvent = (ManualResetEvent)ResetEvent;
            if (resetEvent != null)
            {
                EventState = 1;
                return resetEvent.WaitOne(GetTimeout());
            }
            return false;
        }
        #endregion

        #region timeput

        /// <summary>
        /// Gets or sets the length of time, in seconds, before the request times out.
        /// </summary>
        public int Timeout { get; set; }

        public static readonly TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(30);
        public static readonly int DefaultTimeOutSeconds = 30;

        public TimeSpan GetTimeout()
        {
            if (Timeout <= 0)
                return DefaultTimeOut;
            return TimeSpan.FromSeconds(Timeout);
        }

        public static TimeSpan GetTimeout(int timeout)
        {
            if (timeout <= 0)
                return DefaultTimeOut;
            return TimeSpan.FromSeconds(timeout);
        }

        #endregion

        //internal Action action = null;
    }

#if(false)
    /// <summary>
    ///  Tasker Async base class.
    /// </summary>
    public abstract class GenericTaskerBase: IDisposable//:IGenericTasker
    {

        public void Dispose()
        {
            if (ResetEvent != null)
            {
                ((IDisposable)ResetEvent).Dispose();
                ResetEvent = null;
            }
        }


        #region ResetEvent

        internal ManualResetEvent ResetEvent{ get; set; }
        
        /// <summary>
        /// Sets the state of the event to nonsignaled, causing threads to block.
        /// </summary>
        /// <returns>true if the operation succeeds; otherwise, false.</returns>
        internal bool Set()
        {
            ManualResetEvent resetEvent = (ManualResetEvent)ResetEvent;
            if (resetEvent != null)
            {
                return resetEvent.Set();
            }
            return false;
        }

        /// <summary>
        /// Blocks the current thread until the current instance receives a signal, using
        ///     a System.TimeSpan to measure the time interval.
        /// </summary>
        /// <returns></returns>
        internal bool WaitOne()
        {
            ManualResetEvent resetEvent = (ManualResetEvent)ResetEvent;
            if (resetEvent != null)
            {
                return resetEvent.WaitOne(GetTimeout());
            }
            return false;
        }


        #endregion

        #region timeput

        /// <summary>
        /// Gets or sets the length of time, in seconds, before the request times out.
        /// </summary>
        public int Timeout { get; set; }

        public static readonly TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(30);
        public static readonly int DefaultTimeOutSeconds = 30;

        public TimeSpan GetTimeout()
        {
            if (Timeout <= 0)
                return DefaultTimeOut;
            return TimeSpan.FromSeconds(Timeout);
        }

        public TimeSpan GetTimeout(int timeout)
        {
            if (timeout <= 0)
                return DefaultTimeOut;
            return TimeSpan.FromSeconds(timeout);
        }
        #endregion

        #region Async


        private AsyncCallback onRequestCompleted;
        /// <summary>
        /// Task Completed event
        /// </summary>
        public event EventHandler TaskCompleted;
        
        protected AsyncCallback CreateCallBack()
        {
            if (this.onRequestCompleted == null)
            {
                this.onRequestCompleted = new AsyncCallback(this.OnRequestCompleted);
            }
            return this.onRequestCompleted;
        }

        void OnRequestCompleted(IAsyncResult asyncResult)
        {
 
            OnTaskCompleted(EventArgs.Empty);
        }

        /// <summary>
        /// OnTaskCompleted
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTaskCompleted(EventArgs e)
        {
            if (TaskCompleted != null)
                TaskCompleted(this, e);
        }
        #endregion

    }
#endif

    #region GenericTaskerSample
    /*
    public class GenericTaskerSample
    {
        #region GenericTasker<TResult> sample

        private void execTaskerWithResult()
        {
            GenericTasker<int> ta = new GenericTasker<int>();
            ta.TaskCompleted += new EventHandler(ta_TaskCompleted);
            int res = ta.Execute(() => execTargetWithResult(0, "nis"));

            Console.WriteLine("Result:{0}",res);
        }

        void ta_TaskCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Result:{0}", sender);
        }

        private int execTargetWithResult(int a, string b)
        {
            Console.WriteLine("execTarget int:{0}, str:{1} ", a, b);
            return 0;
        }

        #endregion

        #region GenericTasker sample

        private void execTasker()
        {
            GenericTasker ta = new GenericTasker();
            ta.TaskCompleted+=new EventHandler(ta_TaskCompleted);
            ta.Execute(() => execTarget(0, "nis"));
        }

        private void execTarget(int a, string b)
        {
            Console.WriteLine("execTarget int:{0}, str:{1} ", a, b);
        }

        #endregion

    }
    */
    #endregion

    #region GenericTasker<TResult>

    #region summary
    /// <summary>
    /// Tasker Async using <see cref="Func<TResult>"/> delegate that returns a value of the type
    /// specified by the TResult parameter.
    /// </summary>
    /// <example>
    /// <code>
    ///  public void execTaskerWithResult()
    ///{
    ///    GenericTasker<int> ta = new GenericTasker<int>();
    ///    int res = ta.Execute(() => execTargetWithResult(0, "nis"));
    ///    Console.WriteLine("Result:{0}", res);
    ///}
    ///private int execTargetWithResult(int a, string b)
    ///{
    ///    Console.WriteLine("execTarget int:{0}, str:{1} ", a, b);
    ///    return 2;
    ///}
    /// </code>
    /// </example>
    /// <typeparam name="TResult"></typeparam>
    #endregion
    public class GenericTasker<TResult> 
    {
        delegate TResult GenericTaskerCallback();


        /// <summary>
        /// Execute async task with <see cref="Func<TResult>"/> delegate and default timeout limit. 
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">if function is null</exception>
        /// <exception cref="TimeoutException">if the function does not finish in time </exception>
        public TResult Execute(Func<TResult> function)
        {
            return Execute(function, WaitHandleEvent.DefaultTimeOutSeconds);
        }

        /// <summary>
        /// Execute async task with <see cref="Func<TResult>"/> delegate and timeout limit. 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">if function is null</exception>
        /// <exception cref="TimeoutException">if the function does not finish in time </exception>
        public TResult Execute(Func<TResult> function, int timeout)
        {
            if (function == null) throw new ArgumentNullException("function");
            TResult tresult = default(TResult);
            Thread threadToKill = null;
            Func<TResult> wrappedAction = () =>
            {
                threadToKill = Thread.CurrentThread;
                return function();
            };
            AsyncCallback callBack = null;// CreateCallBack();
            IAsyncResult result = wrappedAction.BeginInvoke(callBack, null);
            if (result.AsyncWaitHandle.WaitOne(WaitHandleEvent.GetTimeout(timeout)))
            {
                tresult = wrappedAction.EndInvoke(result);
            }
            else
            {
                threadToKill.Abort();
                throw new TimeoutException();
            }
            return tresult;
        }

        /// <summary>
        /// Execute async task using <see cref="ThreadPoolEx"/> with <see cref="Func<TResult>"/> delegate and timeout limit. 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">if function is null</exception>
        /// <exception cref="TimeoutException">if the function does not finish in time </exception>
        public TResult ExecuteThread(Func<TResult> function, int timeout)
        {
            if (function == null) throw new ArgumentNullException("function");
            //Thread watchedThread = null;
            WorkItem wi = null;
            TResult tresult = default(TResult);
            ManualResetEvent handle = new ManualResetEvent(false);

            WaitCallback callBack = obj =>
            {
                //watchedThread = obj as Thread;
                tresult= function();
                handle.Set();
            };

            wi = ThreadPoolEx.QueueUserWorkItem(callBack, Thread.CurrentThread);
            if (handle.WaitOne(WaitHandleEvent.GetTimeout(timeout)))
            {
                handle.Set();
                return tresult;
            }
            else
            {
                handle.Set();
                if (wi != null)
                    ThreadPoolEx.Cancel(wi, true);
                throw new TimeoutException("Execute task operation has timed out");
            }
        }

 
        #region Execute thread

        ///// <summary>
        ///// Execute async task using <see cref="ThreadPool"/> with <see cref="Func<TResult>"/> delegate and timeout limit. 
        ///// </summary>
        ///// <param name="target"></param>
        ///// <param name="timeout"></param>
        //public TResult ExecuteThread(Func<TResult> target, int timeout)
        //{
        //    m_func = target;
        //    this.Timeout = timeout;
        //    result = default(TResult);

        //    ManualResetEvent mre = new ManualResetEvent(false);
        //    ThreadPool.QueueUserWorkItem(new WaitCallback(ExecuteWorker), mre);
        //    mre.WaitOne(GetTimeout());

        //    return result;
        //}

        //private TResult result;

        ///// <summary>
        ///// Execute worker Method
        ///// </summary>
        //void ExecuteWorker(object objState)
        //{
        //    ManualResetEvent mre = (ManualResetEvent)objState;

        //    try
        //    {
        //        lock (this.GetType())
        //        {
        //            result = m_func();
        //        }
        //    }
        //    finally
        //    {
        //        Set();
        //    }
        //}
        #endregion

        #region AsyncCallback
/*
        private AsyncCallback onRequestCompleted;
        /// <summary>
        /// Task Completed event
        /// </summary>
        public event EventHandler TaskCompleted;

        protected AsyncCallback CreateCallBack()
        {
            if (this.onRequestCompleted == null)
            {
                this.onRequestCompleted = new AsyncCallback(this.OnRequestCompleted);
            }
            return this.onRequestCompleted;
        }

        void OnRequestCompleted(IAsyncResult asyncResult)
        {
            //GenericTaskerCallback d = (GenericTaskerCallback)asyncResult.AsyncState;
            if (asyncResult.IsCompleted)
            {
                OnTaskCompleted(EventArgs.Empty);
            }
         }

        /// <summary>
        /// OnTaskCompleted
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTaskCompleted(EventArgs e)
        {
            if (TaskCompleted != null)
                TaskCompleted(this, e);
        }
*/
        #endregion


    }
    #endregion

    #region GenericTasker

    #region summary
    /// <summary>
    /// Tasker Async using <see cref="Action"/> delegate that does not return a value.
    /// </summary>
    /// <example>
    /// <code>
    /// public void execTasker()
    ///{
    ///    GenericTasker ta = new GenericTasker();
    ///    ta.TaskCompleted += new EventHandler(ta_TaskCompleted);
    ///    ta.Execute(() => execTarget(0, "nis"));
    ///    Console.WriteLine("TaskCompleted");
    ///}
    ///void ta_TaskCompleted(object sender, EventArgs e)
    ///{
    ///    Console.WriteLine("Result:{0}", sender);
    ///}
    ///private void execTarget(int a, string b)
    ///{
    ///    Console.WriteLine("execTarget int:{0}, str:{1} ", a, b);
    ///}
    /// </code>
    /// </example>
    #endregion
    public class GenericTasker 
    {

        delegate void GenericTaskerCallback(object state);

        /// <summary>
        /// Execute async task with <see cref="Action"/> delegate and default timeout limit. 
        /// </summary>
        /// <param name="function"></param>
        /// <exception cref="ArgumentNullException">if function is null</exception>
        /// <exception cref="TimeoutException">if the function does not finish in time </exception>
        public void Execute(Action function)
        {
            Execute(function, WaitHandleEvent.DefaultTimeOutSeconds);
        }

        /// <summary>
        /// Execute async task with <see cref="Action"/> delegate and timeout limit. 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="timeout"></param>
        /// <exception cref="ArgumentNullException">if function is null</exception>
        /// <exception cref="TimeoutException">if the function does not finish in time </exception>
        public void Execute(Action function, int timeout)
        {
            if (function == null) throw new ArgumentNullException("function");

            Thread threadToKill = null;
            Action wrappedAction = () =>
            {
                threadToKill = Thread.CurrentThread;
                function();
            };
            AsyncCallback callBack = CreateCallBack();
            IAsyncResult result = wrappedAction.BeginInvoke(callBack, null);
            if (result.AsyncWaitHandle.WaitOne(WaitHandleEvent.GetTimeout(timeout)))
            {
                wrappedAction.EndInvoke(result);
            }
            else
            {
                threadToKill.Abort();
                throw new TimeoutException();
            }
        }

        /// <summary>
        /// Execute async task using <see cref="ThreadPoolEx"/> with <see cref="Action"/> delegate and timeout limit. 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="timeout"></param>
        /// <exception cref="ArgumentNullException">if function is null</exception>
        /// <exception cref="TimeoutException">if the function does not finish in time </exception>
        public void ExecuteThread(Action function, int timeout)
        {
            if (function == null) throw new ArgumentNullException("function");
            //Thread watchedThread = null;
            WorkItem wi=null;
            ManualResetEvent handle = new ManualResetEvent(false);

            WaitCallback callBack = obj =>
                {
                    //watchedThread = obj as Thread;
                    function();
                    handle.Set();
                };

            wi = ThreadPoolEx.QueueUserWorkItem(callBack, Thread.CurrentThread);
            if (handle.WaitOne(WaitHandleEvent.GetTimeout(timeout)))
            {
                handle.Set();
            }
            else
            {
                handle.Set();
                //watchedThread.Abort();
                if (wi != null)
                    ThreadPoolEx.Cancel(wi, true);
                throw new TimeoutException("Execute task operation has timed out");
            }
        }
 

        #region Execute thread
/*
        /// <summary>
        /// Execute async task using <see cref="ThreadPool"/> with <see cref="Action"/> delegate and timeout limit. 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="timeout"></param>
        /// <exception cref="TimeoutException"></exception>
        public void ExecuteThread(Action target, int timeout)
        {
            //m_action = target;

            using (WaitHandleEvent handle = new WaitHandleEvent(false, timeout))
            {
                handle.action = target;
                ThreadPool.QueueUserWorkItem(new WaitCallback(ExecuteWorker), handle);
                handle.WaitOne();
                if (handle.IsCompleted)
                {
                    OnTaskCompleted(EventArgs.Empty);
                }
                else //if (!handle.IsCompleted)
                {
                    handle.Set();
                    throw new TimeoutException("Execute task operation has timed out");
                }
            }
        }

        /// <summary>
        /// Execute worker Method
        /// </summary>
        void ExecuteWorker(object state)
        {
            WaitHandleEvent handle = (WaitHandleEvent)state;
            try
            {
                //lock (this.GetType())
                //{
                //    m_action();
                //}
                if (handle != null)
                    handle.action();
            }
            finally
            {
                if (handle != null)
                    handle.Set();
            }
        }
*/
        #endregion

        #region AsyncCallback

        private AsyncCallback onRequestCompleted;
        /// <summary>
        /// Task Completed event
        /// </summary>
        public event EventHandler TaskCompleted;

        protected AsyncCallback CreateCallBack()
        {
            if (this.onRequestCompleted == null)
            {
                this.onRequestCompleted = new AsyncCallback(this.OnRequestCompleted);
            }
            return this.onRequestCompleted;
        }

        void OnRequestCompleted(IAsyncResult asyncResult)
        {
            //GenericTaskerCallback d = (GenericTaskerCallback)asyncResult.AsyncState;
            if (asyncResult.IsCompleted)
            {
                OnTaskCompleted(EventArgs.Empty);
            }
         }

        /// <summary>
        /// OnTaskCompleted
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTaskCompleted(EventArgs e)
        {
            if (TaskCompleted != null)
                TaskCompleted(this, e);
        }
        #endregion


        #region sample


#if(false)


        static void CallWithTimeout(Action action, int timeoutMilliseconds)
    {
        Thread threadToKill = null;
        Action wrappedAction = () =>
        {
            threadToKill = Thread.CurrentThread;
            action();
        };

        IAsyncResult result = wrappedAction.BeginInvoke(null, null);
        if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
        {
            wrappedAction.EndInvoke(result);
        }
        else
        {
            threadToKill.Abort();
            throw new TimeoutException();
        }
    }

}


 /// <summary>
/// Helper class for invoking tasks with timeout. Overhead is 0,005 ms.
/// </summary>
/// <typeparam name="TResult">The type of the result.</typeparam>
[Immutable]
public sealed class WaitFor<TResult>
{
    readonly TimeSpan _timeout;

    /// <summary>
    /// Initializes a new instance of the <see cref="WaitFor{T}"/> class, 
    /// using the specified timeout for all operations.
    /// </summary>
    /// <param name="timeout">The timeout.</param>
    public WaitFor(TimeSpan timeout)
    {
        _timeout = timeout;
    }

    /// <summary>
    /// Executes the spcified function within the current thread, aborting it
    /// if it does not complete within the specified timeout interval. 
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>result of the function</returns>
    /// <remarks>
    /// The performance trick is that we do not interrupt the current
    /// running thread. Instead, we just create a watcher that will sleep
    /// until the originating thread terminates or until the timeout is
    /// elapsed.
    /// </remarks>
    /// <exception cref="ArgumentNullException">if function is null</exception>
    /// <exception cref="TimeoutException">if the function does not finish in time </exception>
    public TResult Run(Func<TResult> function)
    {
        if (function == null) throw new ArgumentNullException("function");

        var sync = new object();
        var isCompleted = false;

        WaitCallback watcher = obj =>
            {
                var watchedThread = obj as Thread;

                lock (sync)
                {
                    if (!isCompleted)
                    {
                        Monitor.Wait(sync, _timeout);
                    }
                }
                   // CAUTION: the call to Abort() can be blocking in rare situations
                    // http://msdn.microsoft.com/en-us/library/ty8d3wta.aspx
                    // Hence, it should not be called with the 'lock' as it could deadlock
                    // with the 'finally' block below.

                    if (!isCompleted)
                    {
                        watchedThread.Abort();
                    }
        };

        try
        {
            ThreadPool.QueueUserWorkItem(watcher, Thread.CurrentThread);
            return function();
        }
        catch (ThreadAbortException)
        {
            // This is our own exception.
            Thread.ResetAbort();

            throw new TimeoutException(string.Format("The operation has timed out after {0}.", _timeout));
        }
        finally
        {
            lock (sync)
            {
                isCompleted = true;
                Monitor.Pulse(sync);
            }
        }
    }

    /// <summary>
    /// Executes the spcified function within the current thread, aborting it
    /// if it does not complete within the specified timeout interval.
    /// </summary>
    /// <param name="timeout">The timeout.</param>
    /// <param name="function">The function.</param>
    /// <returns>result of the function</returns>
    /// <remarks>
    /// The performance trick is that we do not interrupt the current
    /// running thread. Instead, we just create a watcher that will sleep
    /// until the originating thread terminates or until the timeout is
    /// elapsed.
    /// </remarks>
    /// <exception cref="ArgumentNullException">if function is null</exception>
    /// <exception cref="TimeoutException">if the function does not finish in time </exception>
    public static TResult Run(TimeSpan timeout, Func<TResult> function)
    {
        return new WaitFor<TResult>(timeout).Run(function);
    }
}

               /// <summary>
        /// Executes the spcified function within the current thread, aborting it
        /// if it does not complete within the specified timeout interval. 
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>result of the function</returns>
        /// <remarks>
        /// The performance trick is that we do not interrupt the current
        /// running thread. Instead, we just create a watcher that will sleep
        /// until the originating thread terminates or until the timeout is
        /// elapsed.
        /// </remarks>
        /// <exception cref="ArgumentNullException">if function is null</exception>
        /// <exception cref="TimeoutException">if the function does not finish in time </exception>
        //[Immutable]
        public void ExecuteThread(Action function, int timeout)
        {
            if (function == null) throw new ArgumentNullException("function");

            var sync = new object();
            var isCompleted = false;
            //bool taskCompleted = false;
            WaitCallback watcher = obj =>
            {
                var watchedThread = obj as Thread;

                lock (sync)
                {
                    if (!isCompleted)
                    {
                        Monitor.Wait(sync, WaitHandleEvent.GetTimeout(timeout));
                    }
                }
                // CAUTION: the call to Abort() can be blocking in rare situations
                // http://msdn.microsoft.com/en-us/library/ty8d3wta.aspx
                // Hence, it should not be called with the 'lock' as it could deadlock
                // with the 'finally' block below.

                if (!isCompleted)
                {
                    watchedThread.Abort();
                }
            };

            try
            {
                ThreadPool.QueueUserWorkItem(watcher, Thread.CurrentThread);
                function();
                //taskCompleted = true;
                
            }
            catch (ThreadAbortException)
            {
                // This is our own exception.
                Thread.ResetAbort();

                throw new TimeoutException(string.Format("The operation has timed out after {0}.", timeout));
            }
            finally
            {
                lock (sync)
                {
                    isCompleted = true;
                    Monitor.Pulse(sync);
                }
            }

            //if (taskCompleted)
            //{
            //    OnTaskCompleted(EventArgs.Empty);
            //}

        }

#endif
        #endregion

    }

    #endregion
}
