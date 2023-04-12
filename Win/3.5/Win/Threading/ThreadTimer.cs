using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Messaging;

using System.Runtime.InteropServices;
using System.ComponentModel;

namespace MControl.Threading
{
    /// <summary>
    /// ThreadTimer
    /// </summary>
    public class ThreadTimer : System.Timers.Timer
    {
 
        private DateTime timeStart;
        private bool stop;
        private TimeSpan timeSpan;
        private string currentTime;
        private int tickCount;
        private DateTime signalTime;

          
        /// <summary>
        /// ThreadTimer
        /// </summary>
        public ThreadTimer():this(100)
        {

        }

        /// <summary>
        /// ThreadTimer ctor
        /// </summary>
        /// <param name="interval"></param>
        public ThreadTimer(long interval)
            : base((double)interval)
        {
            stop = false;
            this.Elapsed += new System.Timers.ElapsedEventHandler(ThreadTimer_Elapsed);
            //// Keep the timer alive until the end of Main.
            //GC.KeepAlive(this);

        }

        void ThreadTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            signalTime = e.SignalTime;
            tickCount++;
            OnElapsed(e);
        }
  
        /// <summary>
        /// OnElapsed
        /// </summary>
        /// <param name="e"></param>
        protected void OnElapsed(System.Timers.ElapsedEventArgs e)
        {

        }

        private void SetTimeElapsed()
        {
            DateTime dtn = DateTime.Now;
            timeSpan = dtn.Subtract(timeStart);
            currentTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        /// <summary>
        /// Wait
        /// </summary>
        public void Wait(long interval)
        {
            tickCount = 0;
            Start();
            TimeSpan ts = DateTime.Now.Subtract(timeStart);
            while (!stop &&  (long)ts.TotalMilliseconds < interval)//(tickCount == 0)
            {
                ts = DateTime.Now.Subtract(timeStart);
                //tickCount = (ts.Ticks > Interval);
                Thread.Sleep(10);
            }
            Stop();
        }

        /// <summary>
        /// RestartTimer
        /// </summary>
        public void RestartTimer()
        {
            Stop();
            Start();
        }

        /// <summary>
        /// Start
        /// </summary>
        public new void Start()
        {
            base.Start();
            signalTime= timeStart=DateTime.Now;
        }


       /// <summary>
        /// CurrentTimeSpan
       /// </summary>
        public TimeSpan CurrentTimeSpan
        {
            get { return timeSpan; }
        }
        /// <summary>
        /// SignalTime
        /// </summary>
        public DateTime SignalTime
        {
            get { return signalTime; }
        }
        /// <summary>
        /// CurrentDisplayTime
        /// </summary>
        public string CurrentDisplayTime
        {
            get 
            {
                SetTimeElapsed();
                return currentTime; 
            }
        }
    }
}
