using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nistec.WinForms
{
	#region Enumerations
	/// <summary>
	/// Defines the state of a <see cref="McTaskPanel">McTaskPanel</see>.
	/// </summary>
	public enum PanelState
	{
		/// <summary>
		/// The <see cref="McTaskPanel">McTaskPanel</see> is expanded.
		/// </summary>
		Expanded,
		/// <summary>
		/// The <see cref="McTaskPanel">McTaskPanel</see> is collapsed.
		/// </summary>
		Collapsed
	}
	#endregion

	#region Delegates
	/// <summary>
	/// A delegate type for hooking up panel state change notifications.
	/// </summary>
    public delegate void PanelStateChangedEventHandler(object sender, PanelStateChangedEventArgs e);
	#endregion

	#region PanelEventArgs class
	/// <summary>
	/// Provides data for the <see cref="McTaskPanel.PanelStateChanged">PanelStateChanged</see> event.
	/// </summary>
	public class PanelStateChangedEventArgs : System.EventArgs
	{
		#region Private Class data
		private McTaskPanel panel;
		#endregion

		#region Public Constructors
		/// <summary>
        /// Initialises a new <see cref="PanelStateChangedEventArgs">PanelEventArgs</see>.
		/// </summary>
		/// <param name="sender">The originating <see cref="McTaskPanel">McTaskPanel</see>.</param>
        public PanelStateChangedEventArgs(McTaskPanel sender)
		{
			this.panel = sender;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the <see cref="McTaskPanel">McTaskPanel</see> that triggered the event.
		/// </summary>
		public McTaskPanel McTaskPanel
		{
			get
			{
				return this.panel;
			}
		}

		/// <summary>
		/// Gets the <see cref="PanelState">PanelState</see> of the <see cref="McTaskPanel">McTaskPanel</see> that triggered the event.
		/// </summary>
		public PanelState PanelState
		{
			get
			{
				return this.panel.PanelState;
			}
		}
		#endregion
	}
	#endregion


    #region SlideShow

    public class Slide:IDisposable
    {
        private bool sliding;

        public bool Sliding
        {
            get { return sliding; }
        }

        private int height;

        public int Height
        {
            get { return height; }
        }


        private bool Expanded;
        private int slideValue;
        private int slideLimit;
        private int slideInterval;
        Nistec.Threading.ThreadTimer timer;
        delegate void SlideHeightCallBack(int value);

        Control ctl;
        public event EventHandler SlideShowStart;
        public event EventHandler SlideShowEnd;
        public event EventHandler SlideChanged;

        private Slide()
        {
           
        }
        public Slide(Control c)
        {
            ctl = c;
        }

        public virtual void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        public static Slide Instance
        {
            get { return new Slide(); }
        }

        protected virtual void OnSlideStart(EventArgs e)
        {
            if (SlideShowStart != null)
                SlideShowStart(this, e);
        }
        protected virtual void OnSlideEnd(EventArgs e)
        {
            if (SlideShowEnd != null)
                SlideShowEnd(this, e);
        }
        protected virtual void OnSlideChanged(EventArgs e)
        {
            if (SlideChanged != null)
                SlideChanged(this, e);
        }

        public void Show(int start, int end, bool expanded)
        {
            Show(ctl, start, end, expanded);
         }

        public void Show(Control c, int start, int end, bool expanded)
        {
            if (c == null)
            {
                throw new  NoNullAllowedException("Control can not be null");
            }
            ctl = c;
            Expanded = expanded;
            sliding = true;
            if (timer == null)
            {
                timer = new Nistec.Threading.ThreadTimer(10);
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            }
            if (expanded)
            {
                slideValue = start;
                slideLimit = end;
            }
            else
            {
                slideValue = end;
                slideLimit = start;
            }
            slideInterval = (end - start) / 10;
            timer.Start();
            OnSlideStart(EventArgs.Empty);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            switch (this.Expanded)
            {
                case false:
                    slideValue -= slideInterval;// 4;
                    if (slideValue < slideLimit)
                    {
                        timer.Stop();
                        SlideEnd(slideLimit);
                        return;
                    }
                    break;
                default://case PanelState.Expanded:
                    slideValue += slideInterval;// 4;
                    if (slideValue > slideLimit)
                    {
                        timer.Stop();
                        SlideEnd(slideLimit);
                        return;
                    }
                    break;
            }
            //this.Height = slideValue;
            SlideHeight(slideValue);
        }

        private void SlideHeight(int value)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new SlideHeightCallBack(SlideHeight), value);
            }
            else
            {
                height = value;
                ctl.Height = value;
                OnSlideChanged(EventArgs.Empty);
            }
        }

        private void SlideEnd(int value)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new SlideHeightCallBack(SlideEnd), value);
            }
            else
            {
                ctl.Height = value;
                //this.labelTitle.Invalidate();
                //OnPanelStateChanged(new PanelStateChangedEventArgs(this));
                sliding = false;
                OnSlideEnd(EventArgs.Empty);
            }
        }
    }
    #endregion
}
