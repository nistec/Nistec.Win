using System;
using System.Threading;

namespace mControl.Threading
{
	/// <summary>
	/// Summary description for ThreadPoolHandle.
	/// </summary>
	public class ThreadPoolHandle
	{
//		private int maxCount =25;
//		private int minCount =5;
//		private int thread =0;
//		private int Cport;

		public ThreadPoolHandle()
		{
						

		}

		// TaskInfo contains data that will be passed to the callback
		// method.
		public class TaskInfo 
		{
			public RegisteredWaitHandle Handle = null;
			public string Info = "default";
		}


		public void StartWaitForSingleObject (string info) 
		{
			// The main thread uses AutoResetEvent to signal the
			// registered wait handle, which executes the callback
			// method.
			AutoResetEvent ev = new AutoResetEvent(false);
	
			TaskInfo ti = new TaskInfo();
			ti.Info = info;
			// The TaskInfo for the task includes the registered wait
			// handle returned by RegisterWaitForSingleObject.  This
			// allows the wait to be terminated when the object has
			// been signaled once (see WaitProc).
			ti.Handle = ThreadPool.RegisterWaitForSingleObject(
				ev,
				new WaitOrTimerCallback(WaitProc),
				ti,
				1000,
				false
				);

			// The main thread waits three seconds, to demonstrate the
			// time-outs on the queued thread, and then signals.
			Thread.Sleep(3100);
			Console.WriteLine("Main thread signals.");
			ev.Set();

			// The main thread sleeps, which should give the callback
			// method time to execute.  If you comment out this line, the
			// program usually ends before the ThreadPool thread can execute.
			Thread.Sleep(1000);
			// If you start a thread yourself, you can wait for it to end
			// by calling Thread.Join.  This option is not available with 
			// thread pool threads.
		}
   
		// The callback method executes when the registered wait times out,
		// or when the WaitHandle (in this case AutoResetEvent) is signaled.
		// WaitProc unregisters the WaitHandle the first time the event is 
		// signaled.
		protected virtual void WaitProc(object state, bool timedOut) 
		{
			// The state object must be cast to the correct type, because the
			// signature of the WaitOrTimerCallback delegate specifies type
			// Object.
			TaskInfo ti = (TaskInfo) state;

			//string cause = "TIMED OUT";
			if (!timedOut) 
			{
				//cause = "SIGNALED";
				// If the callback method executes because the WaitHandle is
				// signaled, stop future execution of the callback method
				// by unregistering the WaitHandle.
				if (ti.Handle != null)
					ti.Handle.Unregister(null);
			} 

//			QueueHandle qh=new QueueHandle();
//			qh.QListner(null);

			//			Console.WriteLine("WaitProc( {0} ) executes on thread {1}; cause = {2}.",
			//				ti.OtherInfo, 
			//				Thread.CurrentThread.GetHashCode().ToString(), 
			//				cause
			//				);
		}

		// Add a queue request to Threadpool with a callback to RunPooledThread (which calls
//		public void StartWorkItem()
//		{
//			// Create a callback to subroutine RunPooledThread
//			WaitCallback callback = new WaitCallback(DoWork2);
//			// && put in a request to ThreadPool to run the process.
//			ThreadPool.QueueUserWorkItem(callback, null);
//		}


	}
}
