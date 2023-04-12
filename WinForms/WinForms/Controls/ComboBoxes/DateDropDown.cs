using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;   
using System.Collections;
using System.Data; 
using System.Runtime.InteropServices;

using Nistec.Win32;


using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Globalization;
using Nistec.Win;  


namespace Nistec.WinForms
{

	public interface IDateDropDown
	{
		bool DroppedDown  {get;set;}
		void DoDropDown();
		DateTime SelectedDate{get;}
		string Format {get;set;}
		MonthCalendar Calendar{get;}
	}

	/// <summary>
	/// Summary description for DateDropDown.
	/// </summary>

	[System.ComponentModel.ToolboxItem(false)]
	public class DateDropDown : Nistec.WinForms.Controls.McPopUpBase,IDateDropDown
	{
 
		public static DateTime MaxValue{get{return new DateTime(2999,12,31);}}
		public static DateTime MinValue{get{return new DateTime(1900,1,1);}}

		#region Members
		protected MonthCalendar				m_Calendar;
		private Control						Mc;
		private  bool						m_DroppedDown;

		private System.Windows.Forms.Panel	pnlCalendar;
		private bool						SetStrtDate;
		private string						m_Format;
		private DateTime					CurrentValue;

		[Category("Behavior")]
		//public event DateSelectionChangedEventHandler SelectionChanged = null;
		public event EventHandler SelectionChanged;

        //public event EventHandler PopUpShow;
        //public event EventHandler PopUpClosed;

		#endregion 

		#region Constructors

		public static void DoDropDown(Control ctl) 
		{
			DateDropDown dlg=new DateDropDown(ctl);
			dlg.DoDropDown();
		}

		public static void DoDropDown(Control ctl,DateTime value)
		{
			DateDropDown dlg=new DateDropDown(ctl,value,MinValue,MaxValue);
			dlg.DoDropDown();
		}

		public static void DoDropDown(Control ctl,DateTime value,DateTime minDate,DateTime maxDate)
		{
			DateDropDown dlg=new DateDropDown(ctl,value,minDate,maxDate);
			dlg.DoDropDown();
		}

		public static void DoDropDown(Control ctl,string value)
		{
			DateDropDown dlg=new DateDropDown(ctl,value,MinValue,MaxValue);
			dlg.DoDropDown();
		}

		public static void DoDropDown(Control ctl,string value,DateTime minDate,DateTime maxDate)
		{
			DateDropDown dlg=new DateDropDown(ctl,value,minDate,maxDate);
			dlg.DoDropDown();
		}

		
		public DateDropDown(Control ctl):this(ctl,DateTime.Today,MinValue,MaxValue) 
		{
		}

		public DateDropDown(Control ctl,DateTime value):this(ctl,DateTime.Today,MinValue,MaxValue) 
		{
		}

		public DateDropDown(Control ctl,string value):this(ctl,value,MinValue,MaxValue)
		{
		}

		public DateDropDown(Control ctl,string value,DateTime minDate,DateTime maxDate)
		{
			DateTime dat=DateTime.Today;
			try
			{
				if(value!=null && value!="")
					dat= DateTime.Parse(value);
			}
			catch{}
			InitDateDropDown( ctl, dat, minDate, maxDate);

		}

		public DateDropDown(Control ctl,DateTime value,DateTime minDate,DateTime maxDate) : base(ctl)
		{
			InitDateDropDown( ctl, value, minDate, maxDate);
		}

		private void InitDateDropDown(Control ctl,DateTime value,DateTime minDate,DateTime maxDate) 
		{
			Mc = ctl;
			SetStrtDate=false;
			m_Format=  "dd/MM/yyyy";
			m_DroppedDown = false;
			CurrentValue=value ;

			try
			{
				InitializeComponent();
				m_Calendar.MinDate =minDate;
				m_Calendar.MaxDate =maxDate;
				m_Calendar.SetDate(value);
				SetStrtDate=true;
	
				this.pnlCalendar.BackColor  =m_Calendar.BackColor; 
				this.Size =m_Calendar.SingleMonthSize; 
				this.Height  =this.Height +4; 
				if(Mc is ILayout)
				{
					this.SetStyleLayout(((ILayout)Mc).LayoutManager.Layout);
				}

			}
			catch(ApplicationException ex)
			{
				MsgBox.ShowError (ex.Message);
			}
		}
		
		#endregion

		#region Dispose
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
	
				if(this.m_Calendar!=null)
				{
					this.m_Calendar.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
			this.m_Calendar = new MonthCalendar();
			this.pnlCalendar = new System.Windows.Forms.Panel();
			this.pnlCalendar.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_Calendar
			// 
			this.m_Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_Calendar.Location = new System.Drawing.Point(0, 0);
			this.m_Calendar.MaxSelectionCount = 1;
			this.m_Calendar.Name = "m_Calendar";
			this.m_Calendar.TabIndex = 0;
			this.m_Calendar.DateSelected+=new DateRangeEventHandler(m_Calendar_DateSelected);
			//this.m_Calendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_Calendar_MouseUp);
			this.m_Calendar.KeyUp +=new KeyEventHandler(m_Calendar_KeyUp);
			// 
			// pnlCalendar
			// 
			this.pnlCalendar.BackColor = System.Drawing.Color.White;
			this.pnlCalendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlCalendar.Controls.Add(this.m_Calendar);
			this.pnlCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCalendar.Location = new System.Drawing.Point(0, 0);
			this.pnlCalendar.Name = "pnlCalendar";
			this.pnlCalendar.Size = new System.Drawing.Size(160, 160);
			this.pnlCalendar.TabIndex = 1;
			// 
			// DatePickerPopUp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(160, 160);
			this.Controls.Add(this.pnlCalendar);
			this.Name = "DateDropDown";
			this.Text = "DateDropDown";
			//this.DoubleClick += new System.EventHandler(this.m_Calendar_PopUpDoubleClick);
			//this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_Calendar_PopUpKeyUp);
			this.pnlCalendar.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Properties

		[DefaultValue("dd/MM/yyyy") ]
		public string Format
		{
			get{return m_Format ;}
			set{m_Format=value;}
		}

		public MonthCalendar Calendar
		{
			get{return m_Calendar;}
		}

		public DateTime SelectedDate
		{
			get {return this.m_Calendar.SelectionEnd;}
		}

		public override object SelectedItem
		{
			get
			{
				return this.SelectedDate as object;
			}
		}


		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("ComboBoxDroppedDown")]
		public bool DroppedDown
		{
			get
			{
				if (Mc.IsHandleCreated)
				{
					return m_DroppedDown;
				}
				return false;
			}
			set
			{
				if(m_DroppedDown)
				{
					this.Close();
					return;
				}	
				if(value)
					ShowPopUp();		
			}
		}
 
		public void SetStyleLayout(StyleLayout value)
		{
			m_Calendar.TitleBackColor=value.ColorBrush1Internal;
			m_Calendar.TitleForeColor=value.LightLightColor;
			m_Calendar.TrailingForeColor=value.LightColor;
			m_Calendar.BackColor=Color.White;
			m_Calendar.ForeColor=value.ForeColorInternal;
		}

		[Browsable(false) ]
		public MonthCalendar Calnedar
		{
			get {return m_Calendar;}
		}

//		[DefaultValue(typeof(System.Drawing.Color), "Navy") ]
//		internal protected Color CalendarTitleBackColor
//		{
//			get {return m_Calendar.TitleBackColor;}
//			set {m_Calendar.TitleBackColor = value;}
//		}
//
//		[DefaultValue(typeof(System.Drawing.Color), "White") ]
//		internal protected  Color CalendarTitleForeColor
//		{
//			get {return m_Calendar.TitleForeColor;}
//			set{m_Calendar.TitleForeColor = value;}
//		}
//
//		[DefaultValue(typeof(System.Drawing.Color), "Silver") ]
//		internal protected  Color CalendarTrailingForeColor
//		{
//			get {return m_Calendar.TrailingForeColor;}
//			set{m_Calendar.TrailingForeColor = value;}
//		}
//
//		[DefaultValue(typeof(System.Drawing.Color), "White") ]
//		internal protected  Color CalendarBackColor
//		{
//			get {return m_Calendar.BackColor;}
//			set {m_Calendar.BackColor = value;}
//		}
//
//		[DefaultValue(typeof(System.Drawing.Color), "Navy") ]
//		internal protected  Color CalendarForeColor
//		{
//			get {return m_Calendar.ForeColor;}
//			set {m_Calendar.ForeColor = value;}
//		}

		#endregion

		#region PopUp

		public void DoDropDown()
		{
			if(m_DroppedDown)
			{
				Close();
				return;
			}	
		
			ShowPopUp();			
		}

		protected void OnPopupClosed(object sender,System.EventArgs e)
		{
			m_DroppedDown = false;
			this.Dispose();
			m_Calendar.Visible =false;  
			Mc.Invalidate(false);
			if(SelectionChanged !=null) 
				this.SelectionChanged (this,e);
            //if(PopUpClosed!=null)
            //    PopUpClosed(Mc,e);
		}

	
		[UseApiElements("ShowWindow")]
		protected void ShowPopUp()
		{

			m_DroppedDown = true;


			if (!Mc.Enabled)
				return;

			if(!(CurrentValue <= this.m_Calendar.MaxDate && CurrentValue >= m_Calendar.MinDate))
			{
				string msg=RM.GetString(RM.ValueOutOfRange_v2,2,new object[]{m_Calendar.MaxDate,m_Calendar.MinDate});
				MsgBox.ShowWarning(msg)   ;
				return;
			}

			Point pt = new Point(Mc.Left,Mc.Bottom+1 );

			this.Location = Mc.Parent.PointToScreen (pt);
			this.Closed += new System.EventHandler(this.OnPopupClosed);
			//this.Show();
            WinAPI.ShowWindow(this.Handle, WindowShowStyle.ShowNormalNoActivate);
			this.m_Calendar.Focus();

			StartHook();
			this.Start = true;
			
            //if(PopUpShow!=null)
            //    PopUpShow(Mc,new EventArgs());
		}

		#endregion
		
		#region Internal helpers


		internal bool IsMatchHandle(IntPtr mh)
		{
			if(this.IsHandleCreated)
			{
				if(mh==this.Handle)
					return true;
			}

			if(m_Calendar.IsHandleCreated)
			{
				if (mh==m_Calendar.Handle)
					return true;

			}
			return false;
		}

		internal IntPtr GetComboPopUpHandle()
		{
			if(this.IsHandleCreated)
				return (IntPtr)this.Handle;
			return IntPtr.Zero;
		}

		internal IntPtr GetCalendarHandle()
		{
			if(m_Calendar.IsHandleCreated)
				return (IntPtr)m_Calendar.Handle;
			return IntPtr.Zero;
		}

		#endregion

		#region Events handlers

		private void m_Calendar_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
			if(SetStrtDate)
			{
				OnSelected();
			}
		}
		private void m_Calendar_KeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Escape)
			{
				this.Close();
			}

			if(e.KeyData == Keys.Enter)
			{
				OnSelected();
				this.Close();
			}
		}

		protected virtual void OnSelected()
		{
			this.CurrentValue=m_Calendar.SelectionStart;//  this.m_Calendar.SelectionEnd;
			Mc.Text=CurrentValue.ToString(this.m_Format);
				
			if(this.SelectionChanged != null)
			{
				this.SelectionChanged(this,new SelectionChangedEventArgs(CurrentValue.ToString(this.m_Format)));
			}

			this.Close();
		}

		protected override void OnClosed(System.EventArgs e)
		{
			EndHook();
			this.m_Calendar.Visible=false; 
			base.OnClosed(e);
		}

		#endregion

		#region Mouse Hook Methods

		private IntPtr					m_mouseHookHandle;
		private GCHandle				m_mouseProcHandle;
		private bool					IsHooked;

		protected virtual void StartHook()
		{ 
			if(IsHooked){EndHook();}
			// Mouse hook
			WinAPI.HookProc mouseHookProc = new WinAPI.HookProc(MouseHook);
			m_mouseProcHandle = GCHandle.Alloc(mouseHookProc);
			m_mouseHookHandle = WinAPI.SetWindowsHookEx((int)WindowsHookCodes.WH_MOUSE, 
				mouseHookProc, IntPtr.Zero, WinAPI.GetCurrentThreadId());
			if(m_mouseHookHandle ==IntPtr.Zero)
			{
				throw new ApplicationException(" Error Create handle");
			}
			IsHooked=true;
			m_DroppedDown=true;
		}

		protected virtual void EndHook()
		{
			// Unhook   
			if(!IsHooked){return;}
			WinAPI.UnhookWindowsHookEx( m_mouseHookHandle );
			m_mouseProcHandle.Free();
			m_mouseHookHandle = IntPtr.Zero;
			IsHooked=false;
			m_DroppedDown=false;
		}

		private IntPtr MouseHook(int code, IntPtr wparam, IntPtr lparam) 
		{
			MOUSEHOOKSTRUCT mh = (MOUSEHOOKSTRUCT )Marshal.PtrToStructure(lparam, typeof(MOUSEHOOKSTRUCT));
           
			bool res=GetMouseHook(mh.hwnd,wparam);
			if (res)
			{
				//WinAPI.SetFocus(IntPtr.Zero);
				return mh.hwnd;
			}
             
			return WinAPI.CallNextHookEx( m_mouseHookHandle, code, wparam, lparam );
		}

		protected virtual bool GetMouseHook(IntPtr mh,IntPtr wparam)
		{

			Msgs msg = (Msgs) wparam.ToInt32();
			if ((msg == Msgs.WM_LBUTTONDOWN) || (msg == Msgs.WM_RBUTTONDOWN)|| (msg == Msgs.WM_NCLBUTTONDOWN))
			{
				if ((mh == this.Handle) || (mh == this.m_Calendar.Handle))
				{
					//EndHook();
					//this.OnSelected();
					return false;
				}
				else
				{
					//EndHook();
					this.Close();
					return true;
				}
			}

			return false;
		}

		#endregion


	}

}
