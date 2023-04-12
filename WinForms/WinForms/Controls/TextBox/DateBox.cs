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
using Nistec.Win;


namespace Nistec.WinForms
{

    public enum DefaultDates
    {
        Default,
        Now
    }

    [ToolboxItem(false)]
    public class DateBox : Nistec.WinForms.Controls.McMaskedBase
    {

        #region Base Members

        private WinMethods.SYSTEMTIME systemtime;

        private DateFormats m_FormatType = DateFormats.ShortDate;
        private DateTime m_DefaultValue;
        //private RangeDate m_RangeValue = new RangeDate(RangeType.MinDate, RangeType.MaxDate);
        private DateTime m_Value;
        private DateTime m_MinValue;
        private DateTime m_MaxValue;

        internal bool ChangingText;
        internal bool UserEdit;

        public event EventHandler ValueChanged = null;
        //			public event KeyPressEventHandler KeyPressInternal;
        //			public event EventHandler ModifiedInternal;

        #endregion

        #region Constructor

        internal DateBox(bool net)
            : this()
        {
            base.m_netFram = net;
        }

        public DateBox()
            : base()
        {
            //dateSeparator=this.GetDateSeparator();
            base.Format = "d";//dd/MM/yyyy";
            base.InputMask = "";//"09/09/9900";
            this.UserEdit = false;

            m_MinValue = CalendarPicker.MinDate;
            m_MaxValue = CalendarPicker.MaxDate;

            //m_mskFormat="__/__/____";
            //m_inpChar='_';

            //SetupMask();
           // m_RangeValue = new RangeDate(RangeType.MinDate, RangeType.MaxDate);//RangeDate.Empty;

        }

 
        #endregion

        #region Validation events

        //		protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        //		{
        //			e.Cancel =IsValidating();
        //			if(!e.Cancel) 
        //			{
        //				if(Modified)
        //				  AppendMaskedText (base.Text);
        //				base.OnValidating(e);
        //			}
        //		}
        //
        //		protected override void OnValidated(EventArgs e)
        //		{
        //			//Errors.Dispose ();
        //			base.OnValidated(e);
        //		}

        //		protected void OnErrorOcurred(Nistec.WinForms.ErrorOcurredEventArgs e)
        //		{
        //			if (e.Message.Length > 0)
        //			{
        //				if (this.ErrorOcurred != null)
        //				{
        //					ErrorOcurredEventArgs oArg = new ErrorOcurredEventArgs(e.Message);
        //					ErrorOcurred(this, oArg); 
        //				}
        //			}
        ////			if (m_ShowErrorProvider) 
        ////				Errors.SetError(this, e.Message);
        //		}

        //		public void SetError(string Message)
        //		{
        //			if (this.m_ShowErrorProvider)
        //				this.Errors.SetError(this, Message);
        //		}

        #endregion

        #region override

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (this.UserEdit)
            {
                this.ValidateEditText();
            }

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (!IsHandleCreated)
                return;
            if (this.ChangingText)
            {
                this.ChangingText = false;
            }
            else
            {
                this.UserEdit = true;
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }

        internal protected virtual void ValidateEditText()
        {
            this.ParseEditText();
            this.UpdateEditText();
        }

        protected void ParseEditText()
        {
            try
            {
                string format = this.Format;
                System.Globalization.DateTimeFormatInfo dtf = new System.Globalization.DateTimeFormatInfo();
                if (format.Length > 1)
                {
                    dtf.ShortDatePattern = Format;
                }
                //m_

                Value = Types.ToDateTime(this.Text);
                    //Value = DateTime.Parse(this.Text, dtf);
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                this.UserEdit = false;
            }
        }
        protected virtual void UpdateEditText()
        {
            if (this.UserEdit)
            {
                this.ParseEditText();
            }
            this.ChangingText = true;
            //base.Text = this.Value.ToString(this.Format);
            AppendDateText(this.Value);
        }

        public void AppendDateText(string value)
        {
            try
            {
                AppendDateText(Types.ToDateTime(value,DateTime.Now));// this.Format));
            }
            catch (Exception)
            {
                throw new FormatException("Invalid Date Formt: " + value);
            }
        }

        public void AppendDateText(DateTime value)
        {
            if (string.IsNullOrEmpty(Format))
                base.Text = value.ToString();
            else
                base.Text = value.ToString(Format);
        }

        public static DateTime SysTimeToDateTime(WinMethods.SYSTEMTIME s)
        {
            return new DateTime(s.wYear, s.wMonth, s.wDay, s.wHour, s.wMinute, s.wSecond);
        }

        public static WinMethods.SYSTEMTIME DateTimeToSysTime(DateTime time)
        {
            WinMethods.SYSTEMTIME systemtime1 = new WinMethods.SYSTEMTIME();
            systemtime1.wYear = (short)time.Year;
            systemtime1.wMonth = (short)time.Month;
            systemtime1.wDayOfWeek = (short)time.DayOfWeek;
            systemtime1.wDay = (short)time.Day;
            systemtime1.wHour = (short)time.Hour;
            systemtime1.wMinute = (short)time.Minute;
            systemtime1.wSecond = (short)time.Second;
            systemtime1.wMilliseconds = 0;
            return systemtime1;
        }

        #endregion

        #region Behavior Property

        //[Category("Behavior"), Description("RangeType of min and max value")]
        //public RangeDate RangeValue
        //{
        //    get { return m_RangeValue; }
        //    set
        //    {
        //        m_RangeValue = value;
        //    }
        //}

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

        [Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DateTime DefaultValue
        {
            get { return m_DefaultValue; }
            set
            {
                m_DefaultValue = value;
                if (this.DesignMode)
                {
                    this.m_Value = value;
                }
            }
        }


        #endregion

        #region Format Property

        [Browsable(false)]
        public bool UseMask
        {
            get { return base.useMask; }
            set { base.useMask = value; }
        }

        [Browsable(false)]
        public WinMethods.SYSTEMTIME SystemTimeValue
        {
            get { return this.systemtime; }
        }


        #region Date format

        public int GetIntDate()
        {
            string s = Types.FormatDate(this.Value.ToString(), "yyyyMMdd", "-1");
            if (s == "-1")
            {
                MsgBox.ShowError("Unable to format date to int");
                return -1;
            }
            return Types.ToInt(s, -1);
        }
        public string GetDateFormat()
        {
            return GetDateFormat(this.Format);
        }
        public string GetDateSqlFormat()
        {
            return GetDateFormat("yyyy-MM-dd");
        }
        public string GetDateFormat(string format)
        {
            try
            {
                string f = format;
                if (f.Length == 0)
                    f = "d";
                return Types.FormatDate(this.Value.ToString(), f, this.Text);
            }
            catch (Exception)
            {
                throw new FormatException("Invalid Date Formt: " + this.Value.ToString());
            }
        }

        #endregion

        //			[Bindable(true), Category("Behavior"), Description("DateTimePickerValue"), RefreshProperties(RefreshProperties.All)]
        //			public DateTime Value
        //			{
        //				get
        //				{
        //					return m_Value;
        //				}
        //				set
        //				{
        //
        //					bool flag1 = !DateTime.Equals(this.Value, value);
        //					if (flag1)
        //					{
        //						if (!RangeValue.IsValid (value))
        //						{
        //							return;
        //						}
        //						string text1 = this.Text;
        //						m_Value = value;
        //						//systemtime = DateTimeToSysTime(value);
        //						//WinMethods.SendMessage(new HandleRef(this, base.Handle), 0x1002, num1, systemtime1);
        //
        //						this.Text  =value.ToString (Format);
        //						if (flag1)
        //						{
        //							this.OnValueChanged(EventArgs.Empty);
        //						}
        //						if (!text1.Equals(this.Text))
        //						{
        //							this.OnTextChanged(EventArgs.Empty);
        //						}
        //					}
        //				}
        //			}

        [Bindable(true), Category("Behavior"), Description("DateTimeValue")]//, RefreshProperties(RefreshProperties.All)]
        public new DateTime Value
        {
            get
            {

                return this.m_Value;
            }
            set
            {
                if (value != m_Value)
                {
                    if (this.IsValid(value))
                    {
                        m_Value = value;
                        systemtime = DateTimeToSysTime(value);
                        this.OnValueChanged(EventArgs.Empty);
                        //this.UpdateEditText();
                        //base.Text=value.ToString (this.Format);
                        AppendDateText(value);
                    }
                }

            }
        }


        [Bindable(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.ResetValue();
                    return;
                }
                else if(value != Text)
                {
                    AppendMaskedText(value);
                    ParseEditText();
                }

            }
        }


        //			[Category("Appearance"),Bindable(true),Browsable(true)]
        //			public override string Text
        //			{
        //				get{return base.Text;}
        //				set
        //				{
        //					if(base.Text !=value)
        //					{
        //						if(!IsValid(value))
        //						{
        //							this.ResetValue();
        //						}
        //						else 
        //						{
        //							AppendMaskedText (value);
        //							//m_Value=DateTime.Parse (value);
        //							ParseEditText();
        //						}
        //					}
        //				}
        //			}

        [DefaultValue(""), Browsable(true)]
        public override string Format
        {
            get { return base.Format; }
            set 
            { 
                base.Format = value;

                switch (Format)
                {
                    case "d":
                        FormatType = DateFormats.ShortDate;
                        break;
                    case "s":
                    case "g":
                    case "f":
                    case "F":
                    case "r":
                    case "R":
                    case "u":
                    case "U":
                    case "D":
                    case "G":
                        FormatType = DateFormats.GeneralDate;
                        break;
                    default: //case DateFormats.CustomDate:
                        if(Format.Length <=10)
                            FormatType = DateFormats.CustomDate;
                        else //if (Format.Length == 19)
                            FormatType = DateFormats.GeneralDate;
                            //FormatType = DateFormats.CustomDate;
                        break;

                }
            
            }
        }

        [Category("Appearance"), DefaultValue(DateFormats.ShortDate), RefreshProperties(RefreshProperties.All)]
        public new DateFormats FormatType
        {
            get { return m_FormatType; }
            set
            {
                if (m_FormatType != value)
                {
                    m_FormatType = value;
                    if (useCustomFormat)
                    {
                        //m_RangeValue = new RangeDate(RangeType.MinDate, RangeType.MaxDate);
                        m_MinValue = CalendarPicker.MinDate;
                        m_MaxValue = CalendarPicker.MaxDate;
                        //return;
                    }
                    Format = WinHelp.GetFormat(value, Format);
                    base.FormatType = WinHelp.GetFormat(m_FormatType);
                   
                    Invalidate();
                }
            }
        }

        protected override void AppendMaskedText(string s)
        {

            string value = s;// GetDateFormat(s);

            if (value.Length == 0)
            {
                AppendDateText(m_DefaultValue);
                return;
                //value =m_DefaultValue.ToString (m_Format);
            }

            if (base.useMask && base.MaskFormat.Length > 0)
            {
                if (value.Length > base.MaskFormat.Length)
                {
                    //Fix_Value
                    AppendDateText(value);
                    return;
                }
                // if input string length doesn't match format length
                //	this is a problem! this means that Text input
                //	string MUST have optional missing chars

                //string v =value;

                if (value == "")
                    return;//value = m_mskFormat;   
                else if (IsValidString(value, false))//Fix_Mask&& value.Length == m_mskFormat.Length)
                {

                    base.Text = value;
                    //TODO: ValidString
                    // must check optional input chars
                    //						bool ok = true;
                    //						int fpos = 0;                         //Fix_Mask 
                    //						while(ok && fpos < m_mskFormat.Length && fpos < value.Length)
                    //						{
                    //							if(m_mskFormat[fpos] == m_inpChar 
                    //								&& !IsValidChar(value[fpos], (int)m_posNdx[fpos]))
                    //								ok &= value[fpos] == m_inpChar;
                    //							fpos++;
                    //						}
                    //
                    //						if(!ok)
                    //							return;
                    //v =value;//.Replace(this.m_inpChar,m_ReplaceChar);
                }
                else
                {
                    return;
                    //throw new ApplicationException(RM.GetString(RM.ValueNotMatchMask ));
                    //MessageBox.Show (RM.GetString(RM.ValueNotMatchMask));
                }
                //else if(m_errInvalid)
                //	throw new ApplicationException("Input String Does Not Match Input Mask");
                //else
                //	v = value;
            }
            else
            {
                base.Text = value;//.ToString (m_Format);
            }
        }


        internal void SetFormatInputMask(string format)
        {
            DateTimeFormatInfo dti = new CultureInfo(CultureInfo.CurrentCulture.Name, false).DateTimeFormat;
            string sf = "";

            switch (format)
            {
                case "d":
                    sf = dti.ShortDatePattern;
                    sf = sf.Replace("MM", "00");
                    sf = sf.Replace("M", "09");
                    sf = sf.Replace("dd", "00");
                    sf = sf.Replace("d", "09");
                    sf = sf.Replace("y", "0");
                    break;
                //					case "g" :
                //					case "G" :
                //					case "f" :
                //					case "F" :
                //					case "r":
                //					case "R":
                //					case "u":
                //					case "U":
                //					case "D":
                //						sf="";
                //						break;
                case "G":
                    DateTime dt = new DateTime(2222, 11, 22, 22, 22, 22);
                    sf = dt.ToString("G");

                    sf = sf.Replace("2", "0");
                    sf = sf.Replace("1", "0");
                    sf = sf.Replace("AM", "AA");
                    sf = sf.Replace("PM", "AA");
                    break;
                default: //case DateFormats.CustomDate:
                    sf = this.Format;
                    sf = sf.Replace("MMMM", "aaaaaaaaaaaa");
                    sf = sf.Replace("MMM", "AAA");
                    sf = sf.Replace("MM", "00");
                    sf = sf.Replace("M", "09");
                    sf = sf.Replace("dddd", "aaaaaaaaaaaa");
                    sf = sf.Replace("ddd", "AAA");
                    sf = sf.Replace("dd", "00");
                    sf = sf.Replace("d", "09");
                    sf = sf.Replace("yyyy", "9900");
                    sf = sf.Replace("yy", "00");
                    sf = sf.Replace("y", "0");
                    sf = sf.Replace("HH", "00");
                    sf = sf.Replace("H", "09");
                    sf = sf.Replace("m", "0");
                    sf = sf.Replace("s", "0");

                    sf = sf.Replace("tt", "AA");
                    sf = sf.Replace("AM", "AA");
                    sf = sf.Replace("PM", "AA");

                    break;
            }
            SetInputMask(sf);

            //return sf;
        }

        #endregion

        #region internal Methods

        public System.Globalization.DateTimeFormatInfo GetDateFormatInfo()
        {
            return System.Globalization.DateTimeFormatInfo.GetInstance(null);
        }

        public string GetDateSeparator()
        {
            return System.Globalization.DateTimeFormatInfo.GetInstance(null).DateSeparator;
        }

        internal void ResetValue()
        {
            base.Text = "";
            this.m_Value = DateTime.Now;
            //this.userHasSetValue = false;
            //this.Checked = false;
            //this.OnValueChanged(EventArgs.Empty);
            //this.OnTextChanged(EventArgs.Empty);
        }

        //internal bool IsValid(string value)
        //{
        //    if ((value == null) || (value.Length == 0))
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        //DateTime.Parse(value);
        //        if (!Nistec.Util.Info.IsDateTime(value))
        //            return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }


        //    //if (!m_RangeValue.IsValid(value))
        //    //{
        //    //    return false;
        //    //}
        //    //return true;
        //}

        public bool IsValid(string s)
        {
            try
            {
                if (!WinHelp.IsDateTime(s))
                    return false;

                DateTime value = DateTime.Parse(s);
                return (value <= m_MaxValue && value >= m_MinValue);
            }
            catch
            {
                return false;
            }
        }

        public bool IsValid(object obj)
        {
            try
            {
                if (!WinHelp.IsDateTime(obj))
                    return false;
                DateTime value = System.Convert.ToDateTime(obj);
                return (value <= m_MaxValue && value >= m_MinValue);
            }
            catch
            {
                return false;
            }
        }

        public bool IsValid(DateTime value)
        {
            return (value <= m_MaxValue && value >= m_MinValue);
        }

        #endregion

        #region IControlKeyAction Members

        public override void ActionDefaultValue()
        {
            base.Text = m_DefaultValue.ToString(Format);
        }

        #endregion

    }

}

