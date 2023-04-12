using System;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Nistec.Data;
using Nistec.Win;

namespace Nistec.WinForms
{
	[ToolboxItem(false),
	ToolboxBitmap (typeof(McDateTimeLabel), "Toolbox.DateTimeLabel.bmp")]
	public class McDateTimeLabel : System.Windows.Forms.Label
	{
		#region Members
		private bool enableTimer = false;
		private DateTimeFormats dateTimeFormat = DateTimeFormats.LongDatePattern;
		private int timerInterval = 1000;
		private Timer timer = new Timer();
		
		private System.ComponentModel.IContainer components = null;

		#endregion

		#region Constructor
		
		public McDateTimeLabel()
		{
			InitializeComponent();
			this.timer.Tick += new EventHandler(this.Timer_Tick);
		}

		#endregion

		#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				this.timer.Tick -= new EventHandler(this.Timer_Tick);

				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Designer generated code
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Event handlers

		private void Timer_Tick(object sender, EventArgs e)
		{
			this.Invalidate();
		}

		#endregion

		#region Overrides

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
            this.Text = DateTime.Now.ToString(WinHelp.DateTimeFormatToString(this.dateTimeFormat), CultureInfo.CurrentCulture);
		}

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);

			if (!this.Enabled)
				this.enableTimer = false;
		}
		
		#endregion

		#region Properties

		[Category("Behavior"),DefaultValue(false)]
		public bool EnableTimer
		{
			get{return this.enableTimer;}
			set
			{
				this.enableTimer = value;
				this.timer.Enabled = value;
			}
		}
		
		[Category("Behavior")]
		public DateTimeFormats DateTimeFormat
		{
			get{return this.dateTimeFormat;}
			set
			{
				this.dateTimeFormat = value;
				this.Invalidate();
			}
		}

		[Category("Behavior"),DefaultValue(1000)]
		public int TimerInterval
		{
			get{return this.timerInterval;}
			set
			{
				this.timerInterval = value;
				this.timer.Interval = value;
			}
		}

		#endregion
	}
}

