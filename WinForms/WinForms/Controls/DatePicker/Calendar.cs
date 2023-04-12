using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Data;
using Nistec.Win32;


using System.Runtime.InteropServices;
using Nistec.Win;

namespace Nistec.WinForms
{

    /// <summary>
    /// Summary description for CalendarPicker.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public class CalendarPicker : Component//,ICalendar
    {

        #region CalendarPicker

        public static DateTime MinDate
        {
            get { return new DateTime(1900, 1, 1); }
        }

        public static DateTime MaxDate
        {
            get { return new DateTime(2999, 12, 31); }
        }


        #region Members

        private Color m_CalendarTitleBackColor;
        private Color m_CalendarTitleForeColor;
        private Color m_CalendarTrailingForeColor;
        private Color m_CalendarBackColor;
        private Color m_CalendarForeColor;
        private StyleLayout m_StyleLayout;
        private string m_Format;
        private bool m_ExceptValidation;

        //private RangeDate m_RangeValue;
        protected DateTime m_DateValue;
        protected DateTime m_MaxValue;
        protected DateTime m_MinValue;

        private bool m_DroppedDown;
        private DateTime CurrentValue;

        private CalendarPicker.DatePickerPopUp m_ComboPopUp;

        private Control Parent;
        Form form;
        Form mdiForm;

        //internal Point				ParentPoint;


        public event EventHandler PopUpShow;
        public event EventHandler PopUpClosed;

        public event DateSelectionChangedEventHandler ValueChanged;
        public event DateSelectionChangedEventHandler SelectionChanged;

        #endregion

        #region Constructor
        public CalendarPicker(Control parent)
        {
            InitCalendar();
            Parent = parent;
           // m_RangeValue = new RangeDate(RangeType.MinDate, RangeType.MaxDate);
        }

        public CalendarPicker(Control parent, RangeDate rangeValue)
        {
            InitCalendar();
            Parent = parent;
           // m_RangeValue = rangeValue;
        }

        public CalendarPicker(Control parent, DateTime minValue, DateTime maxValue)
        {
            InitCalendar();
            Parent = parent;
           // m_RangeValue = new RangeDate(minValue, maxValue);
        }

        private void InitCalendar()
        {
            m_MinValue = MinDate;
            m_MaxValue = MaxDate;

            m_CalendarTitleBackColor = Color.Navy;
            m_CalendarTitleForeColor = Color.White;
            m_CalendarTrailingForeColor = Color.Silver;
            m_CalendarBackColor = Color.White;
            m_CalendarForeColor = Color.Navy;
            m_Format = "dd/MM/yyyy";//  "dd/MM/yyyy";//((IDatePicker)Parent).Format;
            m_ExceptValidation = true;
            //m_mouseHookHandle = IntPtr.Zero;
            //IsHooked=false;
            m_DateValue = DateTime.Today;
            m_DroppedDown = false;
            CurrentValue = DateTime.Today;

        }
        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (form != null)
                {
                    form.Deactivate += new EventHandler(form_Deactivate);
                    form.Move += new EventHandler(form_Move);
                    if (mdiForm != null)
                    {
                        mdiForm.Deactivate -= new EventHandler(form_Deactivate);
                        mdiForm.Move -= new EventHandler(form_Move);
                    }
                }

                if (this.m_ComboPopUp != null)
                {
                    this.m_ComboPopUp.Dispose();
                }

                //				if(components != null)
                //				{
                //					components.Dispose();
                //				}
            }
            base.Dispose(disposing);
        }

        #endregion

        #region PopUp

        public void DoDropDown(DateTime currentValue)
        {
            this.m_DateValue = currentValue;
            if (m_DroppedDown)
            {
                Close();
                return;
            }
            ShowPopUp();
        }

        public void Close()
        {
            if (!m_ComboPopUp.IsDisposed)
            {
                m_ComboPopUp.Close();
                m_ComboPopUp.Dispose();
            }
            m_DroppedDown = false;
            Parent.Invalidate(false);
        }

        protected void OnPopupClosed(object sender, System.EventArgs e)
        {
            m_DroppedDown = false;
            m_ComboPopUp.SelectionChanged -= new DateSelectionChangedEventHandler(this.OnPopUpSelectionChanged);
            m_ComboPopUp.Closed -= new System.EventHandler(this.OnPopupClosed);

            m_ComboPopUp.Dispose();
            Value = new DateTime(CurrentValue.Year, CurrentValue.Month, CurrentValue.Day, m_DateValue.Hour, m_DateValue.Minute, m_DateValue.Second);

            //Value = CurrentValue;
            if (PopUpClosed != null)
                PopUpClosed(Parent, e);
        }

        private void OnPopUpSelectionChanged(object sender, DateChangedEventArgs e)
        {
            CurrentValue = e.Date;
            if (SelectionChanged != null)
                SelectionChanged(this, e);

        }

        public bool IsValid(DateTime value)
        {
            return (value <= m_MaxValue && value >= m_MinValue);
        }
        public bool IsValid(string s)
        {
            try
            {
                if (!WinHelp.IsDateTime(s))
                    return false;

                return IsValid( DateTime.Parse(s));
            }
            catch
            {
                return false;
            }
        }

        [UseApiElements("ShowWindow")]
        protected void ShowPopUp()
        {

            if (!Parent.Enabled)
                return;

            CurrentValue = m_DateValue;// Types.DateFromString(Parent.Text,DateTime.Today);// Regx.ParseDate (Parent.Text,DateTime.Today);    

            if (!IsValid(CurrentValue))
            {
                string msg = RM.GetString(RM.ValueOutOfRange_v2, new object[] { MaxValue, MinValue });
                MsgBox.ShowWarning(msg);
                return;
            }

            Point pt = new Point(Parent.Left, Parent.Bottom);
            m_ComboPopUp = new DatePickerPopUp(this.Parent);//,CurrentValue,m_RangeValue.MinValue ,m_RangeValue.MaxValue);
            m_ComboPopUp.SelectionChanged += new DateSelectionChangedEventHandler(this.OnPopUpSelectionChanged);
            m_ComboPopUp.Closed += new System.EventHandler(this.OnPopupClosed);

            Rectangle screenRect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            if (screenRect.Bottom < pt.Y + m_ComboPopUp.Height)
            {
                pt.Y = pt.Y - m_ComboPopUp.Height - Parent.Height - 1;
            }

            if (screenRect.Right < pt.X + m_ComboPopUp.Width)
            {
                pt.X = screenRect.Right - m_ComboPopUp.Width - 2;
            }

            m_ComboPopUp.Location = Parent.Parent.PointToScreen(pt);
            m_ComboPopUp.ResetCalendar(this, CurrentValue, MinValue, MaxValue);
            WinAPI.ShowWindow(m_ComboPopUp.Handle, WindowShowStyle.ShowNormalNoActivate);

            m_ComboPopUp.Start = true;
            m_DroppedDown = true;
            if (PopUpShow != null)
                PopUpShow(Parent, new EventArgs());

            DropDownHandle();

        }


        private void DropDownHandle()
        {
            if (form != null)
            {
                form.Deactivate -= new EventHandler(form_Deactivate);
                form.Move -= new EventHandler(form_Move);
                if (mdiForm != null)
                {
                    mdiForm.Deactivate -= new EventHandler(form_Deactivate);
                    mdiForm.Move -= new EventHandler(form_Move);
                }
            }
            else //if (this.Parent!=null)
            {
                if (this.Parent != null)
                    form = this.Parent.FindForm();
                else
                    form = Form.ActiveForm;
                if (form != null)
                {
                    mdiForm = form.MdiParent;
                }
            }
            if (form != null)
            {
                form.Deactivate += new EventHandler(form_Deactivate);
                form.Move += new EventHandler(form_Move);
                if (mdiForm != null)
                {
                    mdiForm.Deactivate += new EventHandler(form_Deactivate);
                    mdiForm.Move += new EventHandler(form_Move);
                }
            }
        }

        private void form_Deactivate(object sender, EventArgs e)
        {
            //form.Deactivate -= new EventHandler(form_Deactivate);

            if (DroppedDown)
            {
                Point p = this.m_ComboPopUp.PointToClient(Cursor.Position);

                if (this.m_ComboPopUp.ClientRectangle.Contains(p))
                {
                    return;
                }
                //EndHook();
                m_ComboPopUp.Close();

            }
        }

        private void form_Move(object sender, EventArgs e)
        {
            //form.Move -= new EventHandler(form_Move);
            m_ComboPopUp.Close();
        }


        #endregion

        #region Properties

        internal bool IsMatchHandle(IntPtr mh)
        {
            if (m_ComboPopUp.IsHandleCreated)
            {
                if (mh == m_ComboPopUp.Handle)
                    return true;
            }

            if (m_ComboPopUp.Calendar.IsHandleCreated)
            {
                if (mh == m_ComboPopUp.Calendar.Handle)
                    return true;

            }
            return false;
        }

        internal IntPtr GetComboPopUpHandle()
        {
            if (m_ComboPopUp.IsHandleCreated)
                return (IntPtr)m_ComboPopUp.Handle;
            return IntPtr.Zero;
        }

        internal IntPtr GetCalendarHandle()
        {
            if (m_ComboPopUp.Calendar.IsHandleCreated)
                return (IntPtr)m_ComboPopUp.Calendar.Handle;
            return IntPtr.Zero;
        }

        public void SetStyleLayout(StyleLayout value)
        {
            m_StyleLayout = value;
            //			m_CalendarTitleBackColor=m_StyleLayout.ColorBrush1Internal;//DarkColor;
            //			m_CalendarTitleForeColor=m_StyleLayout.LightLightColor;
            //			m_CalendarTrailingForeColor=m_StyleLayout.LightColor;
            //			//m_CalendarBackColor=Color.White;// m_StyleLayout.BackgroundColorInternal;// LightLightColor;
            //			m_CalendarForeColor=m_StyleLayout.ForeColorInternal;// .DarkDarkColor;

            m_CalendarTitleBackColor = value.DarkColor;
            m_CalendarTitleForeColor = value.LightLightColor;
            m_CalendarTrailingForeColor = value.LightColor;
            //m_CalendarBackColor=Color.White;// m_StyleLayout.BackgroundColorInternal;// LightLightColor;
            m_CalendarForeColor = value.ForeColorInternal;// .DarkDarkColor;

        }

        [Browsable(false)]
        internal bool DroppedDown
        {
            get { return m_DroppedDown; }
        }

        [Browsable(false)]
        internal DatePickerPopUp ComboPopUp
        {
            get { return m_ComboPopUp; }
        }

        [Browsable(false)]
        internal MonthCalendar InternalCalnedar
        {
            get { return m_ComboPopUp.Calendar; }
        }

        [DefaultValue(typeof(System.Drawing.Color), "Navy")]
        internal protected Color CalendarTitleBackColor
        {
            get { return m_CalendarTitleBackColor; }
            set { m_CalendarTitleBackColor = value; }
        }

        [DefaultValue(typeof(System.Drawing.Color), "White")]
        internal protected Color CalendarTitleForeColor
        {
            get { return m_CalendarTitleForeColor; }
            set { m_CalendarTitleForeColor = value; }
        }

        [DefaultValue(typeof(System.Drawing.Color), "Silver")]
        internal protected Color CalendarTrailingForeColor
        {
            get { return m_CalendarTrailingForeColor; }
            set { m_CalendarTrailingForeColor = value; }
        }

        [DefaultValue(typeof(System.Drawing.Color), "White")]
        internal protected Color CalendarBackColor
        {
            get { return m_CalendarBackColor; }
            set { m_CalendarBackColor = value; }
        }

        [DefaultValue(typeof(System.Drawing.Color), "Navy")]
        internal protected Color CalendarForeColor
        {
            get { return m_CalendarForeColor; }
            set { m_CalendarForeColor = value; }
        }

        [Category("Behavior"), Description("min value")]
        public DateTime MinValue
        {
            get { return m_MinValue; }
            set
            {
                m_MinValue = value;
            }
        }
        [Category("Behavior"), Description("max value")]
        public DateTime MaxValue
        {
            get { return m_MaxValue; }
            set
            {
                m_MaxValue = value;
            }
        }

        internal DateTime Value
        {
            get { return m_DateValue; }
            set
            {
                if (value != m_DateValue)
                {
                    if (!IsValid(value))
                        return;
                    m_DateValue = value;
                    //((McDatePicker)Parent).SetInternalValue(value);//.ToString (m_Format));
                    //Parent.Text =value.ToString (m_Format);
                    if (ValueChanged != null)
                        ValueChanged(this, new DateChangedEventArgs(value));
                }
                //Parent.Text =value.ToString (m_Format);
                //OnDateChanged();
            }
        }

        [DefaultValue("dd/MM/yyyy")]
        public string Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }

        //		[ReadOnly(true)]
        //		public RangeDate RangeValue
        //		{
        //			get{return m_RangeValue;}
        //			set{m_RangeValue=value;}
        //		}

        //public RangeDate GetRangeValue()
        //{
        //    return m_RangeValue;
        //}
        //public void SetRangeValue(RangeDate value)
        //{
        //    m_RangeValue = value;
        //}
        public void SetRangeValue(DateTime min,DateTime max)
        {
            m_MinValue = min;
            m_MaxValue = max;
        }

        [DefaultValue(true)]
        public bool ExceptValidation
        {
            get { return m_ExceptValidation; }
            set { m_ExceptValidation = value; }
        }


        #endregion

        #region Validateion

        //		public void OnValidated()
        //		{
        //			if(_IsChange)
        //			{
        //				DateTimeFormatInfo format = CultureInfo.CurrentCulture.DateTimeFormat;				
        //				m_DateValue =DateTime.Parse(Parent.Text, format);				
        //			}
        //			_IsChange=false;
        //		}

        public bool IsValidateDate()
        {
            if (!m_ExceptValidation)
                return false;
            return IsValid(Parent.Text);
        }


        #endregion

        #region Public Methods

        public override string ToString()
        {
            return m_DateValue.ToString(m_Format);
        }

        public string GetDateFormat(string Text)
        {
            if (WinHelp.IsDate(Text))
            {
                return System.Convert.ToDateTime(Text).ToString(this.m_Format);
            }
            else
                return String.Empty;
        }

        public void OnLostFocus(object sender, System.EventArgs e)
        {
            if ((m_DroppedDown && m_ComboPopUp != null) && (Form.ActiveForm != m_ComboPopUp))
            {
                Close();
            }
        }

        public void OnKeyDown(KeyEventArgs e)
        {

            if (e.Shift && e.KeyCode == Keys.Down && m_DroppedDown)
            {
                ShowPopUp();
            }
            else if (((e.Shift && e.KeyCode == Keys.Up) || e.KeyCode == Keys.Escape) && (m_ComboPopUp != null))
            {
                Close();
            }

            else if (e.KeyCode == System.Windows.Forms.Keys.Down)
            {
                if (this.DroppedDown)
                    AddDays(7);
                else
                    AddDays(1);

            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Up)
            {
                if (this.DroppedDown)
                    AddDays(-7);
                else
                    AddDays(-1);

            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Left)
            {
                if (this.DroppedDown)
                    AddDays(-1);
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Right)
            {
                if (this.DroppedDown)
                    AddDays(1);
            }
            if (e.KeyCode == Keys.Enter && m_DroppedDown)
            {
                m_ComboPopUp.OnSelectionChanged();
            }

        }

        public void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            AddDays(Delta);
        }

        public void AddDays(int value)
        {
            try
            {
                if (!Parent.Enabled)
                    return;
                DateTime dt = m_DateValue;
                dt = dt.AddDays(value);

                if (dt <= MaxValue && dt >= MinValue)
                {
                    m_DateValue = dt;
                    Parent.Text = m_DateValue.ToString(m_Format); //Types.DateToString(m_DateValue,m_Format);//  m_DateValue.ToString (m_Format); 
                }
                if (this.DroppedDown)
                {
                    m_ComboPopUp.Calendar.SetDate(m_DateValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region Class PopUp

        internal class DatePickerPopUp : Nistec.WinForms.Controls.McPopUpBase
        {
            #region Members
            private System.ComponentModel.IContainer components = null;
            protected MonthCalendar m_Calendar;
            protected Control Mc;
            private System.Windows.Forms.Panel panel1;
            private bool SetStrtDate;


            [Category("Behavior")]
            public event DateSelectionChangedEventHandler SelectionChanged = null;
            #endregion

            #region Constructors


            public DatePickerPopUp(Control parent)
                : base(parent)
            {
                Mc = parent;
                SetStrtDate = false;

                try
                {
                    InitializeComponent();
                    //					m_Calendar.MinDate =minDate;
                    //					m_Calendar.MaxDate =maxDate;
                    //					m_Calendar.SetDate(date);
                    //					SetStrtDate=true;
                    //	
                    //					m_Calendar.SetCalendarDimensions (1,1); 
                    //					m_Calendar.TitleBackColor = parent.CalendarTitleBackColor;
                    //					m_Calendar.TitleForeColor = parent.CalendarTitleForeColor;
                    //					m_Calendar.TrailingForeColor = parent.CalendarTrailingForeColor;
                    //					m_Calendar.BackColor = parent.CalendarBackColor;
                    //					m_Calendar.ForeColor = parent.CalendarForeColor;

                    //					if (this.Mc is IDatePicker)
                    //					{
                    //						m_Calendar.SetCalendarDimensions (1,1); 
                    //						m_Calendar.TitleBackColor = ((IDatePicker)this.Mc).Calendar.CalendarTitleBackColor;
                    //						m_Calendar.TitleForeColor = ((IDatePicker)this.Mc).Calendar.CalendarTitleForeColor;
                    //						m_Calendar.TrailingForeColor = ((IDatePicker)this.Mc).Calendar.CalendarTrailingForeColor;
                    //						m_Calendar.BackColor = ((IDatePicker)this.Mc).Calendar.CalendarBackColor;
                    //						m_Calendar.ForeColor = ((IDatePicker)this.Mc).Calendar.CalendarForeColor;
                    //			
                    //					}
                    this.panel1.BackColor = m_Calendar.BackColor;
                    //this.Size = m_Calendar.SingleMonthSize;// .Size; 
                    //this.Height = this.Height + 4;

                    //Mc.Focus();//.Controls[0].Focus ();
                }
                catch (ApplicationException ex)
                {
                    MsgBox.ShowError(ex.Message);
                }
            }

            internal void ResetCalendar(CalendarPicker parent, DateTime date, DateTime minDate, DateTime maxDate)
            {
                m_Calendar.MinDate = minDate;
                m_Calendar.MaxDate = maxDate;
                m_Calendar.SetDate(date);
                SetStrtDate = true;

                m_Calendar.SetCalendarDimensions(1, 1);

                if (parent == null) return;
                m_Calendar.TitleBackColor = parent.CalendarTitleBackColor;
                m_Calendar.TitleForeColor = parent.CalendarTitleForeColor;
                m_Calendar.TrailingForeColor = parent.CalendarTrailingForeColor;
                m_Calendar.BackColor = parent.CalendarBackColor;
                m_Calendar.ForeColor = parent.CalendarForeColor;

            }

            #endregion

            #region Dispose
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {

                    if (components != null)
                    {
                        components.Dispose();
                    }
                }
                base.Dispose(disposing);
            }
            #endregion

            #region Windows Form Designer generated code
            private void InitializeComponent()
            {
                this.m_Calendar = new MonthCalendar();
                this.panel1 = new System.Windows.Forms.Panel();
                this.panel1.SuspendLayout();
                this.SuspendLayout();
                // 
                // m_Calendar
                // 
                this.m_Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
                this.m_Calendar.Location = new System.Drawing.Point(0, 0);
                this.m_Calendar.MaxSelectionCount = 1;
                this.m_Calendar.Name = "m_Calendar";
                this.m_Calendar.TabIndex = 0;
                this.m_Calendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_Calendar_MouseUp);
                this.m_Calendar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_Calendar_PopUpKeyUp);
                // 
                // panel1
                // 
                this.panel1.BackColor = System.Drawing.Color.White;
                this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.panel1.Controls.Add(this.m_Calendar);
                this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel1.Location = new System.Drawing.Point(0, 0);
                this.panel1.Name = "panel1";
                this.panel1.Size = new System.Drawing.Size(160, 160);
                this.panel1.TabIndex = 1;
                // 
                // DatePickerPopUp
                // 
                this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
                this.ClientSize = new System.Drawing.Size(160, 160);
                this.Controls.Add(this.panel1);
                this.Name = "DatePickerPopUp";
                this.Text = "DatePickerPopUp";
                this.DoubleClick += new System.EventHandler(this.m_Calendar_PopUpDoubleClick);
                this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_Calendar_PopUpKeyUp);
                this.panel1.ResumeLayout(false);
                this.ResumeLayout(false);

            }
            #endregion

            #region Events handlers

            private void m_Calendar_PopUpKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
            {
                if (e.KeyData == Keys.Escape)
                {
                    this.Close();
                }

                else if (e.KeyData == Keys.Enter)
                {
                    OnSelectionChanged();
                    //this.Close();
                }
            }

            private void m_Calendar_PopUpDoubleClick(object sender, System.EventArgs e)
            {
                System.Windows.Forms.MonthCalendar.HitTestInfo hTest = m_Calendar.HitTest(this.PointToClient(Control.MousePosition));
                System.Windows.Forms.MonthCalendar.HitArea hArea = hTest.HitArea;

                if (hArea == System.Windows.Forms.MonthCalendar.HitArea.Date)
                {
                    m_Calendar.SelectionStart = hTest.Time;
                    OnSelectionChanged();

                    //					this.Hide (); 
                    //					this.Close();
                }
            }

            private void m_Calendar_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                System.Windows.Forms.MonthCalendar.HitTestInfo hTest = m_Calendar.HitTest(this.PointToClient(Control.MousePosition));
                System.Windows.Forms.MonthCalendar.HitArea hArea = hTest.HitArea;

                if (hArea == System.Windows.Forms.MonthCalendar.HitArea.Date)
                {
                    m_Calendar.SelectionStart = hTest.Time;
                    OnSelectionChanged();
                    //					this.Hide (); 
                    //					this.Close();
                }
            }


            #endregion

            #region Virtuals

            private void m_Calendar_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
            {
                if (SetStrtDate)
                {
                    this.SelectionChanged(this, new DateChangedEventArgs(m_Calendar.SelectionStart));
                    //this.Close();
                }
            }

            internal void OnSelectionChanged()
            {
                if (this.SelectionChanged != null)
                {
                    this.SelectionChanged(this, new DateChangedEventArgs(m_Calendar.SelectionStart));
                }
                this.Hide();
                this.Close();

            }

            #endregion

            #region Overrides

            protected override void OnClosed(EventArgs e)
            {
                base.OnClosed(e);
                //this.panel1.Controls.Remove(this.m_Calendar);
            }

            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                base.OnPaint(e);

                if (this.Mc is IDatePicker)
                {
                    Rectangle rect = new Rectangle(this.ClientRectangle.Location, new Size(this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1));

                    using (Pen pen = (Pen)(((IDatePicker)this.Mc).LayoutManager.GetPenBorder()))
                    {
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                }
            }

            public override void PostMessage(ref Message m)
            {
                Message msg = new Message();
                msg.HWnd = m_Calendar.Handle;
                msg.LParam = m.LParam;
                msg.Msg = m.Msg;
                msg.Result = m.Result;
                msg.WParam = m.WParam;

                m_Calendar.PostMessage(ref msg);
            }

            #endregion

            #region Properties

            public MonthCalendar Calendar
            {
                get { return m_Calendar; }
            }

            public DateTime SelectedDate
            {
                get { return this.m_Calendar.SelectionEnd; }
            }

            //			public override int SelectedIndex
            //			{
            //				get
            //				{
            //					return 0;
            //				}
            //			}
            //
            public override object SelectedItem
            {
                get
                {
                    return this.SelectedDate as object;
                }
            }


            #endregion

        }
        #endregion

    }

    #region ICalendar

    public interface ICalendar
    {

        //		Color CalendarTitleBackColor {get ;set ;}
        //
        //		Color CalendarTitleForeColor {get ;set ;}
        //
        //		Color CalendarTrailingForeColor {get ;set ;}
        //
        //		Color CalendarBackColor {get ;set ;}
        //
        //		Color CalendarForeColor {get ;set ;}

        DateTime Value { get;set;}

        string Format { get;set;}

        //RangeDate RangeValue
        //{
        //	get ;//set ;
        //}

        RangeDate GetRangeValue();
        void SetRangeValue(RangeDate value);

        bool ExceptValidation { get;set;}


    }
    #endregion


}
