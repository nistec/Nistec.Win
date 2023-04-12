using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MControl.Threading
{

    #region WorkItem
    /// <summary>
    /// WorkItem
    /// </summary>
    public sealed class WorkItem
    {
        private WaitCallback _callback;
        private object _state;
        private ExecutionContext _ctx;

        internal WorkItem(WaitCallback wc, object state, ExecutionContext ctx)
        {
            _callback = wc; _state = state; _ctx = ctx;
        }

        internal WaitCallback Callback { get { return _callback; } }
        internal object State { get { return _state; } }
        internal ExecutionContext Context { get { return _ctx; } }
    }

    /// <summary>
    /// WorkItem Status
    /// </summary>
    public enum WorkItemStatus { Completed, Queued, Executing, Aborted }
    #endregion

    /// <summary>
    /// Extended <see cref="ThreadPool"/> 
    /// </summary>
    /// <example>
    /// <code>
    /// ThreadPoolEx.QueueUserWorkItem(delegate(object state));
    /// 
    /// ThreadPoolEx.Cancel(item, true);
    /// </code>
    /// </example>
    public static class ThreadPoolEx
    {
        private static LinkedList<WorkItem> _callbacks = new LinkedList<WorkItem>();
        private static Dictionary<WorkItem, Thread> _threads = new Dictionary<WorkItem, Thread>();

        /// <summary>
        /// Queues a method for execution. The method executes when a thread pool thread  becomes available.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static WorkItem QueueUserWorkItem(WaitCallback callback)
        {
            return QueueUserWorkItem(callback, null);
        }

        /// <summary>
        /// Queues a method for execution. The method executes when a thread pool thread  becomes available.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static WorkItem QueueUserWorkItem(WaitCallback callback, object state)
        {
            if (callback == null) throw new ArgumentNullException("callback");

            WorkItem item = new WorkItem(callback, state, ExecutionContext.Capture());
            lock (_callbacks) _callbacks.AddLast(item);
            ThreadPool.QueueUserWorkItem(new WaitCallback(HandleItem));
            return item;
        }

        private static void HandleItem(object ignored)
        {
            WorkItem item = null;
            try
            {
                lock (_callbacks)
                {
                    if (_callbacks.Count > 0)
                    {
                        item = _callbacks.First.Value;
                        _callbacks.RemoveFirst();
                    }
                    if (item == null) return;
                    _threads.Add(item, Thread.CurrentThread);

                } ExecutionContext.Run(item.Context,
                    delegate { item.Callback(item.State); }, null);
            }
            finally
            {
                lock (_callbacks)
                {
                    if (item != null) _threads.Remove(item);
                }
            }
        }

        /// <summary>
        /// Cancel WorkItem and abort thread.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="allowAbort"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="System.Threading.ThreadStateException"></exception>
        public static WorkItemStatus Cancel(WorkItem item, bool allowAbort)
        {
            if (item == null)  throw new ArgumentNullException("item");
            lock (_callbacks)
            {
                LinkedListNode<WorkItem> node = _callbacks.Find(item);
                if (node != null)
                {
                    _callbacks.Remove(node);
                    return WorkItemStatus.Queued;
                }
                else if (_threads.ContainsKey(item))
                {
                    if (allowAbort)
                    {
                        _threads[item].Abort();
                        _threads.Remove(item);
                        return WorkItemStatus.Aborted;
                    }
                    else return WorkItemStatus.Executing;
                }
                else return WorkItemStatus.Completed;
            }
        }
    }
}


