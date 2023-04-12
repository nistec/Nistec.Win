using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;

using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;


using Nistec.Drawing;
using Nistec.Win32;
using Nistec.Data;
using Nistec.Win;


namespace Nistec.WinForms
{

    [DefaultEvent("ValueChanged")]
    [ToolboxItem(true), Designer(typeof(Design.McEditDesigner))]
    [ToolboxBitmap(typeof(McDatePicker), "Toolbox.DatePicker.bmp")]
    public class McDatePicker : Nistec.WinForms.Controls.McButtonEdit, IMcTextBox, IDatePicker
    {

        #region Members

        private System.ComponentModel.IContainer components = null;
        private DateBox m_TextBox;
        private CalendarPicker m_Calendar;
        //Form form;


        //private bool userHasSetValue;
        //private DateTime m_value;

        [Category("Action"), Description("DateTimePickerOnCloseUp")]
        public event EventHandler CloseUp;
        [Description("DateTimePickerOnDropDown"), Category("Action")]
        public event EventHandler DropDown;

        [Category("PropertyChanged"), Description("DateTimePickerOnFormatChanged")]
        public event EventHandler FormatChanged;
        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public new event EventHandler TextChanged;
        [Category("Action"), Description("valueChangedEvent")]
        public event EventHandler ValueChanged;
        [Category("Action"), Description("DateChangedEvent")]
        public event EventHandler DateChanged;

        #endregion

        #region Constructors

        public McDatePicker()
            : base()
        {

            //this.prefHeightCache = -1;
            this.m_IsValid = false;
            //this.userHasSetValue = false;

            InitializeComponent();
            //m_TextBox.m_netFram=true;

            m_TextBox.useCustomFormat = true;
            //this.m_Button.m_netFram = true;
            m_TextBox.DefaultValue = DateTime.Today;
            m_Calendar.Value = DateTime.Today;
            m_Calendar.SetRangeValue(MinValue,MaxValue);// (this.RangeValue);
            m_Calendar.SetStyleLayout(this.LayoutManager.Layout);
            //			this.Value = m_Calendar.Value;


        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.m_TextBox.Parent = this;
            //SetSize();
            SetStyleLayoutInternal();
            ActionDefaultValue();
            //if (this.defaultValue == DefaultDates.Now)
            //{
            //if (this.IsValid(DateTime.Now))
            //    this.Value = DateTime.Now;
            //}
        }

        public new bool IsValid(DateTime value)
        {
            return (value <= MaxValue && value >=MinValue);
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

        #region Component Designer generated code
        private void InitializeComponent()
        {
            m_Calendar = new CalendarPicker(this);
            this.m_TextBox = new Nistec.WinForms.DateBox(true);
            this.SuspendLayout();
            // 
            // m_TextBox
            // 
            this.m_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TextBox.BackColor = System.Drawing.Color.White;
            this.m_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_TextBox.Format = "dd/MM/yyyy";
            this.m_TextBox.FormatType = DateFormats.CustomDate;
            //this.m_TextBox.InputMask = "00/00/0000";
            this.m_TextBox.MaxLength = 10;
            this.m_TextBox.Name = "m_TextBox";
            this.m_TextBox.TabIndex = 0;
            //this.m_TextBox.Text = "";
            this.m_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
            this.m_TextBox.GotFocus += new System.EventHandler(this.m_TextBox_OnGotFocus);
            this.m_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBox_KeyPress);
            this.m_TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyUp);
            this.m_TextBox.TextChanged += new System.EventHandler(this.m_TextBox_TextChanged);
            this.m_TextBox.Leave += new System.EventHandler(this.m_TextBox_Leave);
            this.m_TextBox.LostFocus += new System.EventHandler(this.m_TextBox_OnLostFocus);
            this.m_TextBox.ModifiedInternal += new EventHandler(m_TextBox_ModifiedInternal);
            this.m_TextBox.LostFocus += new System.EventHandler(this.m_TextBox_OnLostFocus);
            this.m_TextBox.ValueChanged += new EventHandler(m_TextBox_ValueChanged);
            // 
            // m_Calendar
            // 
            this.m_Calendar.SelectionChanged += new DateSelectionChangedEventHandler(DatePopUp_SelectionChanged);
            this.m_Calendar.PopUpClosed += new EventHandler(m_Calendar_PopUpClosed);
            this.m_Calendar.PopUpShow += new EventHandler(m_Calendar_PopUpShow);
            this.m_Calendar.ValueChanged += new DateSelectionChangedEventHandler(m_Calendar_ValueChanged);
            // 
            // McDatePicker
            // 
            this.Controls.Add(this.m_TextBox);
            this.Name = "McDatePicker";
            this.Size = new System.Drawing.Size(88, 19);
            this.Controls.SetChildIndex(this.m_TextBox, 0);
            this.ResumeLayout(false);

        }
        #endregion

        #region McTextBox Events handlers

        private void m_TextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }

        private void m_TextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void m_TextBox_OnGotFocus(object sender, System.EventArgs e)
        {
            this.OnGotFocus(e);
        }

        private void m_TextBox_OnLostFocus(object sender, System.EventArgs e)
        {
            this.OnLostFocus(e);
        }

        private void m_TextBox_Leave(object sender, System.EventArgs e)
        {
            base.OnLostFocus(e);
        }

        private void m_TextBox_TextChanged(object sender, System.EventArgs e)
        {
            this.OnTextChanged(e);
        }

        private void m_TextBox_ValueChanged(object sender, System.EventArgs e)
        {
            this.OnValueChanged(e);
        }

        #endregion

        #region Virtual Events

        private void m_TextBox_ModifiedInternal(object sender, EventArgs e)
        {
            OnModifiedInternal(e);
        }

        protected virtual void OnModifiedInternal(EventArgs e)
        {
            //
        }

        //		private void m_TextBox_KeyPressInternal(object sender, KeyPressEventArgs e)
        //		{
        //          OnKeyPressInternal(e); 
        //		}
        //
        //		protected virtual void OnKeyPressInternal(KeyPressEventArgs e)
        //		{
        //	      //
        //		}

        protected virtual void OnCloseUp(EventArgs e)
        {
            if (this.CloseUp != null)
            {
                this.CloseUp(this, e);
            }
        }


        protected virtual void OnDropDown(EventArgs e)
        {
            if (this.DropDown != null)
            {
                this.DropDown(this, e);
            }
        }


        protected virtual void OnFormatChanged(EventArgs e)
        {
            if (this.FormatChanged != null)
            {
                this.FormatChanged(this, e);
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {

            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        //private void ResetFormat()
        //{
        //    m_TextBox.FormatType = DateFormats.ShortDate;
        //}

        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            if (this.TextChanged != null)
                TextChanged(this, new System.EventArgs());
        }

        protected override void OnRightToLeftChanged(System.EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            //SetSize();
            this.Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            OnControlLayoutChanged(e);
            //			if(this.Height < (minHeight+ButtonPedding))
            //				this.Height=minHeight+ButtonPedding;
            //
            //			if(!this.internaleResize) SetSize();
        }

        protected override void OnControlLayoutChanged(EventArgs e)
        {
            base.OnControlLayoutChanged(e);

            if (this.m_TextBox == null)
                return;

            int yPos = (this.Height - m_TextBox.Height) / 2;

            if (!ButtonVisible)// ControlLayout ==ControlLayout.System)
            {
                this.m_TextBox.Size = new Size(Size.Width - 6, Size.Height - 4);
                yPos = (this.Height - m_TextBox.Height) / 2;
                this.m_TextBox.Location = new Point(2, yPos);
            }
            else
            {
                Rectangle rect = this.ClientRectangle;

                //if(this.RightToLeft==System.Windows.Forms.RightToLeft.Yes)
                if (this.ButtonAlign == ButtonAlign.Left)
                {
                    this.m_TextBox.Size = new Size(this.Width - m_Button.Width - 6, rect.Height - 4);
                    yPos = ((rect.Height - m_TextBox.Height) / 2);
                    this.m_TextBox.Location = new Point(rect.X + m_Button.Left + m_Button.Width + 2, rect.Y + yPos);
                }
                else
                {
                    this.m_TextBox.Size = new Size(rect.Width - m_Button.Width - 6, rect.Height - 6);
                    yPos = ((rect.Height - m_TextBox.Height) / 2);
                    this.m_TextBox.Location = new Point(rect.X + 2, rect.Y + yPos);
                }
            }

            base.OnButtonAlignChanged(e);

            this.Invalidate();
        }

        [Browsable(false)]
        public override bool Focused
        {
            get { return m_TextBox.Focused; }
        }

        public new bool Focus()
        {
            return m_TextBox.Focus();
        }

        public virtual void SelectAll()
        {
            if (!m_TextBox.IsDisposed)
                m_TextBox.SelectAll();
        }

        #endregion

        #region McTextBox Property

        //[Category("Behavior")]
        //[Description("RangeType of min and max value")]
        ////[System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public RangeDate RangeValue
        //{
        //    get { return m_TextBox.RangeValue; }
        //    set
        //    {
        //        m_TextBox.RangeValue = value;
        //        m_Calendar.SetRangeValue(value);
        //    }
        //}

        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
                this.m_TextBox.ContextMenuStrip = value;
            }
        }

        [Category("Behavior"), Description("min value")]
        public DateTime MinValue
        {
            get { return m_TextBox.MinValue; }
            set
            {
                m_TextBox.MinValue = value;
            }
        }
        [Category("Behavior"), Description("max value")]
        public DateTime MaxValue
        {
            get { return m_TextBox.MaxValue; }
            set
            {
                m_TextBox.MaxValue = value;
            }
        }

        [Category("Behavior")]
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsModified
        {
            get { return m_TextBox.Modified; }
            set { m_TextBox.Modified = value; }
        }

        [Category("Behavior"), DefaultValue(true)]
        public new bool CausesValidation
        {
            get { return m_TextBox.CausesValidation; }
            set { m_TextBox.CausesValidation = value; }
        }

        private void ResetValue()
        {
            this.Value = m_TextBox.DefaultValue;// DateTime.Now;
            //this.userHasSetValue = false;
            this.m_IsValid = false;
            //this.OnValueChanged(EventArgs.Empty);
            //this.OnTextChanged(EventArgs.Empty);
        }

        [Browsable(false)]
        public WinMethods.SYSTEMTIME SystemTimeValue
        {
            get { return this.m_TextBox.SystemTimeValue; }
        }

        //this.ctlDatePicker1.DefaultValue = new System.DateTime(((long)(0)));
            

        //		[Bindable(true), Category("Behavior"), Description("DateTimePickerValue"), RefreshProperties(RefreshProperties.All)]
        //		public DateTime Value
        //		{
        //			get
        //			{
        //				//if (!this.userHasSetValue && this.m_IsValid)//.validTime)
        //				//{
        //				//	return this.DefaultValue;// DateTime.Now;
        //				//}
        //				return m_TextBox.Value;//this.m_value;
        //			}
        //			set
        //			{
        //
        //				bool flag1 = !DateTime.Equals(this.Value, value);
        //				if (!this.userHasSetValue || flag1)
        //				{
        //					if (!m_TextBox.RangeValue.IsValid (value))
        //					{
        //						m_IsValid=false;
        //						//throw new ArgumentException("InvalidBoundArgument " + value.ToString());
        //
        //						this.userHasSetValue = true;
        //						m_TextBox.Text  =m_TextBox.Value.ToString (Format);
        //						m_IsValid=true;
        //	
        //						OnErrorOcurred( new ErrorOcurredEventArgs("Value out of range"));
        //
        //						return;
        //					}
        //					string text1 = this.Text;
        //					m_TextBox.Value = value;
        //					this.userHasSetValue = true;
        //					m_TextBox.Text  =value.ToString (Format);
        //					m_IsValid=true;
        //					//if (base.IsHandleCreated)
        //					//{
        //						//int num1 = 0;
        //						//WinMethods.SYSTEMTIME systemtime1 = WinMethods.DateTimeToSysTime(value);
        //						//WinMethods.SendMessage(new HandleRef(this, base.Handle), 0x1002, num1, systemtime1);
        //						//m_TextBox.Text  =value.ToString (m_Calendar.Format);
        //						//m_IsValid=true;
        //					//}
        //					if (flag1)
        //					{
        //						this.OnValueChanged(EventArgs.Empty);
        //					}
        //					if (!text1.Equals(this.Text))
        //					{
        //						this.OnTextChanged(EventArgs.Empty);
        //					}
        //				}
        //			}
        //		}

        [Bindable(true), Category("Behavior"), Description("DateTimePickerValue")]//, RefreshProperties(RefreshProperties.All)]
        public DateTime Value
        {
            get
            {
                return m_TextBox.Value;
            }
            set
            {
                m_TextBox.Value = value;
            }
        }

        [Bindable(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return m_TextBox.Text;
            }
            set
            {
                m_TextBox.Text = value;
            }
        }

        //		[Bindable(true),Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //		public override string Text
        //		{
        //			get
        //			{
        //				return m_TextBox.Text;
        //			}
        //			set
        //			{
        //				//m_TextBox.Text=value;
        //				if ((value == null) || (value.Length == 0))
        //				{
        //					this.ResetValue();
        //					//m_TextBox.Text="";
        //					return;
        //				}
        //				try
        //				{
        //					//s=System.Convert.ToDateTime(value).ToString(Format); 
        //					this.Value = DateTime.Parse(value);
        //				}
        //				catch
        //				{
        //					throw new ArgumentException("InvalidDateTime Format " + value.ToString());
        //				}
        //			}
        //		}

        [Category("Appearance")]
        [DefaultValue(System.Windows.Forms.HorizontalAlignment.Left)]
        public override HorizontalAlignment TextAlign
        {
            get { return this.m_TextBox.TextAlign; }
            set
            {//if(m_TextBox.TextAlign != value)
                m_TextBox.TextAlign = value;
            }
        }

        [Description("ComboBoxSelectionLength"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionLength
        {
            get
            {
                return this.m_TextBox.SelectionLength;
                //				int[] numArray3 = new int[1];
                //				int[] numArray1 = numArray3;
                //				numArray3 = new int[1];
                //				int[] numArray2 = numArray3;
                //				WinMethods.SendMessage(new HandleRef(this, base.Handle), 320, numArray2, numArray1);
                //				return (numArray1[0] - numArray2[0]);
            }
            set
            {
                this.m_TextBox.SelectionLength = value;
                //				if (value < 0)
                //				{
                //					throw new ArgumentException("InvalidArgument", value.ToString() );
                //				}
                //				this.Select(this.SelectionStart, value);
            }
        }

        [Description("ComboBoxSelectionStart"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get
            {
                //				int[] numArray1 = new int[1];
                //				WinMethods.SendMessage(new HandleRef(this, base.Handle), 320, numArray1, null);
                //				return numArray1[0];
                return this.m_TextBox.SelectionStart;
            }
            set
            {
                this.m_TextBox.SelectionStart = value;
                //				if (value < 0)
                //				{
                //					throw new ArgumentException("InvalidArgument", value.ToString() );
                //				}
                //				this.Select(value, this.SelectionLength);
            }
        }

        public void Select(int start, int length)
        {
            this.m_TextBox.Select(start<0 ? 0:start, length);
        }

        protected virtual void SetFormatInputMask()
        {
            m_TextBox.SetFormatInputMask(this.Format);
        }

        #endregion

        #region StyleProperty

        [Category("Style"), DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                SerializeForeColor(value, true);
            }
        }

        [Category("Style"), DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                SerializeBackColor(value, true);
            }
        }

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);
            if ((DesignMode || IsHandleCreated))
            {
                SetStyleLayoutInternal();
                this.Invalidate(true);
            }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeFont(Font value, bool force)
        {
            if (ShouldSerializeForeColor())
                this.Font = LayoutManager.Layout.TextFontInternal;
            else if (force)
                this.Font = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
            {
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
                this.m_TextBox.ForeColor = base.ForeColor;
            }
            else if (force)
            {
                base.ForeColor = value;
                this.m_TextBox.ForeColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                base.BackColor = LayoutManager.Layout.BackColorInternal;
                this.m_TextBox.BackColor = base.BackColor;
            }
            else if (force)
            {
                base.BackColor = value;
                this.m_TextBox.BackColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

 
        private void SetStyleLayoutInternal()
        {
            m_Calendar.SetStyleLayout(this.LayoutManager.Layout);
        }

        #endregion

        #region IMcTextBox Members

        protected virtual void AppendInternal(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            this.m_TextBox.AppendDateText(value);
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool UseMask
        {
            get { return m_TextBox.UseMask; }
            set { m_TextBox.UseMask = value; }
        }

        [Category("Appearance")]//,DefaultValue("dd/MM/yyyy"),RefreshProperties(RefreshProperties.All)]
        [
        Description(
        "d     1/3/2002,	M/d/yyyy \r\n" +
        "D     Thursday, January 03, 2002	dddd, MMMM dd, yyyy \r\n" +
        "f     Thursday, January 03, 2002 12:00 AM \r\n" +
        "F     Thursday, January 03, 2002 12:00:00 AM dddd, MMMM dd, yyyy h:mm:ss tt \r\n" +
        "g     1/3/2002 12:00 AM \r\n" +
        "G     1/3/2002 12:00:00 AM \r\n" +
        "m     January 03 MMMM dd \r\n" +
        "M     January 03 MMMM dd \r\n" +
        "r     Thu, 03 Jan 2002 00:00:00 GMT ddd, dd MMM yyyy HH':'mm':'ss 'GMT' \r\n" +
        "R     Thu, 03 Jan 2002 00:00:00 GMT ddd, dd MMM yyyy HH':'mm':'ss 'GMT' \r\n" +
        "s     2002-01-03T00:00:00 yyyy'-'MM'-'dd'T'HH':'mm':'ss \r\n" +
        "t     12:00 AM h:mm tt \r\n" +
        "T     12:00:00 AM h:mm:ss tt \r\n" +
        "u     2002-01-03 00:00:00Z yyyy'-'MM'-'dd HH':'mm':'ss'Z' \r\n" +
        "U     Thursday, January 03, 2002 8:00:00 AM \r\n" +
        "y     January, 2002 MMMM, yyyy \r\n" +
        "Y     January, 2002 MMMM, yyyy ")
            ]
        public string Format
        {
            get { return m_TextBox.Format; }
            set
            {
                //if(m_TextBox.Format != value)
                //{
                //if (value.ToUpper() == "G" || value.ToUpper() == "F" || value.ToUpper() == "R" || value.ToUpper() == "U" || value == "D")
                //{
                //    value = "d";//throw new FormatException("UnSupported format");
                //}
                if (value == null) value = "d";
                m_TextBox.Format = value;
                this.m_Calendar.Format = value;
                if (value != "")
                {
                    m_TextBox.SetFormatInputMask(value);
                }
                //}
            }
        }

        [Category("Behavior"), DefaultValue(false)]
        public override bool ReadOnly
        {
            get { return m_TextBox.ReadOnly; }
            set
            {//if(m_TextBox.ReadOnly != value)
                m_TextBox.ReadOnly = value;
            }
        }

        public void AppendText(string value)
        {
            m_TextBox.AppendText(value);
        }

        public override void ResetText()
        {
            m_TextBox.ResetText();
        }

        public void AppendFormatText(string value)
        {
            m_TextBox.AppendFormatText(value);
        }

        [Browsable(false)]
        protected override FieldType BaseFormat
        {
            get { return m_TextBox.BaseFormat; }
        }

        #endregion

        #region Calendar
        //private DefaultDates defaultValue = DefaultDates.Default;

        //[DefaultValue(DefaultDates.Default)]// [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new DefaultDates DefaultValue
        //{
        //    get { return defaultValue; }
        //    set
        //    {
        //        defaultValue = value;
        //        if (this.DesignMode && value == DefaultDates.Now)
        //            this.Value = DateTime.Now;
        //    }
        //}

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DateTime DefaultValue
        {
            get { return m_TextBox.DefaultValue; }
            set
            {
                m_TextBox.DefaultValue = value;
            }
        }

        [Category("Appearance")]
        [Description("Sets the Input Mask "
             + "(digit 0 = required  9 = optional)"
             + "(letter A= required a = optional)"
             + "(letter or digit D = required d = optional)")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue("")]
        public string InputMask
        {
            get { return m_TextBox.InputMask; }
            set
            {
                m_TextBox.InputMask = value;
            }
        }

        //		[Category("Appearance")]
        //		public DateFormats FormatType
        //		{
        //			get {return m_TextBox.FormatType ;}
        //			set {
        //				 m_TextBox.FormatType = value;
        //				 this.Invalidate ();
        //				 m_TextBox.SetFormatInputMask();
        //				 OnFormatChanged (EventArgs.Empty ); 
        //			   }
        //		}

        [Category("Property"), Browsable(false)]
        [System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CalendarPicker Calendar
        {
            get { return m_Calendar; }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!this.ReadOnly)
            {
                //?mMask.OnKeyDown  (e);
                m_Calendar.OnKeyDown(e);
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            e.Handled = true;
        }

        protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (!this.ReadOnly)
            {
                m_Calendar.OnMouseWheel(e);
            }
        }

        private void m_Calendar_PopUpShow(object sender, EventArgs e)
        {
            OnDropDown(e);
        }

        private void m_Calendar_PopUpClosed(object sender, EventArgs e)
        {
            ClosePopUp();
        }

        protected override bool GetMouseHook(IntPtr mh, IntPtr wparam)
        {
            if (!DroppedDown)
                return false;
            if (mh == InternalButton.Handle)
                return false;
            if (m_Calendar.IsMatchHandle(mh))
                return false;

            //			if(mh == Handle || mh ==m_TextBox.Handle)
            //			{
            //				if(wparam==(IntPtr)513 && DroppedDown )
            //				{
            //					m_Calendar.Close ();
            //					return false;
            //				}
            //				return true;
            //			}
            if (wparam == (IntPtr)513 && DroppedDown)
            {
                //if(!m_Calendar.IsMatchHandle(mh))
                //{
                m_Calendar.Close();
                //}
                return false;
            }
            return true;
        }

        private void ShowPopUp()
        {
            m_Calendar.DoDropDown(this.Value);
            if (m_Calendar.DroppedDown)
            {
                base.StartHook();
            }
            OnDropDown(EventArgs.Empty);
            //this.SetForm();
        }

    
        private void ClosePopUp()
        {
            if (!m_Calendar.DroppedDown)
            {
                base.EndHook();
            }
            OnCloseUp(EventArgs.Empty);
        }

        public override void DoDropDown()
        {
            if (this.ReadOnly)
            {
                return;
            }
            if (DroppedDown)
            {
                m_Calendar.Close();
                return;
            }

            ShowPopUp();
            base.DoDropDown();
        }

        public override void CloseDropDown()
        {
            if (DroppedDown)
            {
                m_Calendar.Close();
            }
        }

        private void m_Calendar_ValueChanged(object sender, DateChangedEventArgs e)
        {
            m_TextBox.UserEdit = true;
            this.Value = e.Date;
            //this.OnValueChanged(e);
        }

        private void DatePopUp_SelectionChanged(object sender, DateChangedEventArgs e)
        {
            OnDateChanged(e);
        }

        protected void OnDateChanged(DateChangedEventArgs e)
        {
            if (this.DateChanged != null)
            {
                this.DateChanged(this, new System.EventArgs());
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("DroppedDown")]
        public override bool DroppedDown
        {
            get
            {
                if (base.IsHandleCreated)
                {
                    return base.DroppedDown;
                }
                return false;
            }
            set
            {
                DoDropDown();
            }
        }

        public int GetIntDate()
        {
            return m_TextBox.GetIntDate();
        }
        public string GetDateFormat()
        {
            return m_TextBox.GetDateFormat();
        }
        public string GetDateFormat(string format)
        {
            return m_TextBox.GetDateFormat(format);
        }
        public string GetDateSqlFormat()
        {
            return m_TextBox.GetDateSqlFormat();
        }


        //This code produces the following output.
        //
        //FORMAT  en-US EXAMPLE
        //CHAR    VALUE OF ASSOCIATED PROPERTY, IF ANY
        //
        //d     1/3/2002
        //M/d/yyyy (ShortDatePattern)
        //
        //D     Thursday, January 03, 2002
        //dddd, MMMM dd, yyyy (LongDatePattern)
        //
        //f     Thursday, January 03, 2002 12:00 AM
        //
        //F     Thursday, January 03, 2002 12:00:00 AM
        //dddd, MMMM dd, yyyy h:mm:ss tt (FullDateTimePattern)
        //
        //g     1/3/2002 12:00 AM
        //
        //G     1/3/2002 12:00:00 AM
        //
        //	m     January 03
        //MMMM dd (MonthDayPattern)
        //
        //M     January 03
        //MMMM dd (MonthDayPattern)
        //
        //r     Thu, 03 Jan 2002 00:00:00 GMT
        //ddd, dd MMM yyyy HH':'mm':'ss 'GMT' (RFC1123Pattern)
        //
        //R     Thu, 03 Jan 2002 00:00:00 GMT
        //ddd, dd MMM yyyy HH':'mm':'ss 'GMT' (RFC1123Pattern)
        //
        //s     2002-01-03T00:00:00
        //yyyy'-'MM'-'dd'T'HH':'mm':'ss (SortableDateTimePattern)
        //
        //t     12:00 AM
        //h:mm tt (ShortTimePattern)
        //
        //T     12:00:00 AM
        //h:mm:ss tt (LongTimePattern)
        //
        //u     2002-01-03 00:00:00Z
        //yyyy'-'MM'-'dd HH':'mm':'ss'Z' (UniversalSortableDateTimePattern)
        //
        //U     Thursday, January 03, 2002 8:00:00 AM
        //
        //y     January, 2002
        //MMMM, yyyy (YearMonthPattern)
        //
        //Y     January, 2002
        //MMMM, yyyy (YearMonthPattern)

        #endregion

        #region IControlKeyAction Members

        public void ActionUndo()
        {
            m_TextBox.ActionUndo();
        }

        public void ActionDefaultValue()
        {
            DateTime dv = DateTime.Now;

            //if (DefaultValue > DateTime.MinValue)
            //{
            //    dv = DefaultValue;
            //}
            if (this.IsValid(dv))
            {
                this.Value = dv;
            }

            //if (this.defaultValue == DefaultDates.Now)
            //{
            //    if (this.IsValid(DateTime.Now))
            //    {
            //        this.Value = DateTime.Now;
            //        //if (base.IsHandleCreated)
            //        //{
            //        //    this.OnValueChanged(EventArgs.Empty);
            //        //}
            //    }
            //}
            //else
            //{
            //    m_TextBox.Text = base.DefaultValue;// ActionDefaultValue(); 
            //}
        }

        protected override bool OnEnterAction()//EnterAction action)
        {
            if (m_Calendar != null)
                m_Calendar.OnKeyDown(new KeyEventArgs(Keys.Enter));
            //OnButtonClick(new System.EventArgs());
            //if(action==EnterAction.MoveNext)
            //	ActionTabNext();
            return true;
        }

        protected override bool OnInsertAction()//InsertAction action)
        {
            if (this.IsValid(DateTime.Now))
            {
                this.Value = DateTime.Now;
            }
            //DoDropDown();
            return false;
        }

        protected override bool OnEscapeAction()//EscapeAction action)
        {
            ClosePopUp();
            return false;
        }

        //		public override bool IsValidating()
        //		{ 
        //			if(! this.m_TextBox.CausesValidation )
        //			{
        //				m_IsValid=true;
        //				return true;
        //			}
        //			string msg="";
        //			bool ok= m_TextBox.IsValidating (ref msg);
        //	
        //			if(!ok)
        //			{
        //				m_IsValid=false;
        //				OnErrorOcurred( new ErrorOcurredEventArgs(msg));
        //				this.AppendFormatText(this.Value.ToString(m_Calendar.Format));
        //			}
        //			else
        //			{
        //				if(this.userHasSetValue && this.m_IsValid)// m_TextBox.Modified)
        //				{
        // 					this.Value=Regx.ParseDate(Text,this.Value);
        //				    this.AppendFormatText(this.Value.ToString(m_Calendar.Format));
        //					this.userHasSetValue=false;
        //				}
        //				m_IsValid=true;
        //			}
        //			return ok;
        // 		}

        #endregion

        #region IBind Members
        
        [Browsable(false)]
        public override BindingFormat BindFormat
        {
            get
            {
                string format = this.Format;
                if (format == "d" || (Format.Length >1 && Format.Length <= 10))
                        return BindingFormat.Date; 
                else
                        return BindingFormat.DateTime;
               

            }
        }

        public override string BindPropertyName()
        {
            return "Value";
        }
        public override void BindDefaultValue()
        {
            ActionDefaultValue();
            if (base.IsHandleCreated)
            {
                this.OnValueChanged(EventArgs.Empty);
            }
        }
        #endregion

    }
}
