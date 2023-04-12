using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace mControl.Threading
{
 
    #region CallBack


    [Synchronization()]
    public class SortSyncronized : ContextBoundObject
    {
        // A method that does some work - returns the square of the given number
        public string Sort(System.Data.DataView dv, string colView)
        {
            Console.Write("Syncronized.Sort called.  ");
            Console.WriteLine("The hash of the current thread is: {0}", Thread.CurrentThread.GetHashCode());
            return dv.Sort = colView;
        }
    }

    //
    // Async delegate used to call a method with this signature asynchronously
    //
    public delegate string SortSyncDelegate(System.Data.DataView dv, string colView);

    public class AsyncSort
    {
        public static void Start(System.Data.DataView dv, string colView)
        {
            string callResult = "";
            //Create an instance of a context-bound type SampleSynchronized
            //Because SampleSynchronized is context-bound, the object sampSyncObj 
            //is a transparent proxy
            SortSyncronized sortSyncObj = new SortSyncronized();


            ////call the method synchronously
            //Console.Write("Making a synchronous call on the object.  ");
            //Console.WriteLine("The hash of the current thread is: {0}", Thread.CurrentThread.GetHashCode());
            //callResult = sortSyncObj.Sort(dv, colView);
            ////Console.WriteLine("Result of calling sampSyncObj.Square with {0} is {1}.\n\n", callParameter, callResult);


            //call the method asynchronously
            Console.Write("Making an asynchronous call on the object.  ");
            Console.WriteLine("The hash of the current thread is: {0}", Thread.CurrentThread.GetHashCode());
            SortSyncDelegate sortDelegate = new SortSyncDelegate(sortSyncObj.Sort);
            //callParameter = 17;

            IAsyncResult aResult = sortDelegate.BeginInvoke(dv, colView, null, null);

            //Wait for the call to complete
            aResult.AsyncWaitHandle.WaitOne();

            callResult = sortDelegate.EndInvoke(aResult);
            //Console.WriteLine("Result of calling sampSyncObj.Square with {0} is {1}.", callParameter, callResult);
        }

    }

    #endregion

}
