using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions; 
 
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
 
using mControl.Util;
using mControl.Drawing;
using mControl.Win32;

 
namespace mControl.WinCtl.Controls
{

	public enum DefaultDates
	{
		Default,
		Now
	}

		[ToolboxItem(false)]
		public class DateBox: mControl.WinCtl.Controls.CtlTextBoxBase
		{	

			#region Base Members
			
			private WinMethods.SYSTEMTIME systemtime;

			private DateFormats m_FormatType = DateFormats.ShortDate;
			private DateTime m_DefaultValue;
			private RangeDate m_RangeValue=new RangeDate(Range.MinDate,Range.MaxDate);
			private DateTime m_Value;

			internal bool ChangingText;
			internal bool UserEdit;

			public event EventHandler ValueChanged = null;
			public event KeyPressEventHandler KeyPressInternal;
			public event EventHandler ModifiedInternal;
	
			#endregion

			#region Mask Members
			//Mask
			private string m_mask;			// input mask
			private string m_mskFormat;		// display format (mask with input chars replaced by input char)
			private char m_inpChar;
			//private char m_ReplaceChar;
			//private bool m_maskChg;
			//private bool m_stdmaskChg;
			private Hashtable m_regexps;
			private Hashtable m_posNdx;		// hold position translation map
			private int m_location;
			private int m_requiredCnt;		// required char count
			private int m_optionalCnt;		// optional char count

			// allowed mask chars
			private const char MASK_KEY = '@';

			#endregion

			#region Constructor

			internal DateBox(bool net):this()
			{
				base.m_netFram=net;
			}

			public DateBox(): base()
			{
				base.Format="dd/MM/yyyy";
				//dateSeparator=this.GetDateSeparator();

				m_mask="00/00/0000";
				m_mskFormat="__/__/____";
				m_inpChar='_';
				//m_ReplaceChar='\0';
				//m_maskChg=false;
				//m_stdmaskChg=false;
				m_location=0;
				m_requiredCnt=0;
				m_optionalCnt=0;

				SetupMask();
				m_RangeValue=new RangeDate(Range.MinDate,Range.MaxDate);//RangeDate.Empty;

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

			//		protected void OnErrorOcurred(mControl.WinCtl.Controls.ErrorOcurredEventArgs e)
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
				base.OnLostFocus (e);
				if (this.UserEdit)
				{
					this.ValidateEditText();
				}

			}

			protected override void OnTextChanged(EventArgs e)
			{
				base.OnTextChanged (e);
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
				if(ValueChanged!=null)
					ValueChanged(this,EventArgs.Empty);
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
					string format=this.Format;
					System.Globalization.DateTimeFormatInfo dtf=new System.Globalization.DateTimeFormatInfo();
					if(format.Length>1)
					{
						dtf.ShortDatePattern=Format;
					}
					m_Value =DateTime.Parse(this.Text, dtf);
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
					AppendDateText(Util.Types.DateFromString(value,this.Format));
				}
				catch(Exception)
				{
					throw new FormatException("Invalid Date Formt: " + value);
				}
			}

			public void AppendDateText(DateTime value)
			{
				base.Text = value.ToString(Format);
			}

			public string GetDateFormat(string value)
			{
				try
				{
					return  Util.Types.FormatDate(value,Format,value);
						//return DateTime.Parse(value).ToString(Format,CultureInfo.CurrentCulture);
				}
				catch(Exception)
				{
					throw new FormatException("Invalid Date Formt: " + value);
				}
			}

			public static DateTime SysTimeToDateTime(WinMethods.SYSTEMTIME s)
			{
				return new DateTime(s.wYear, s.wMonth, s.wDay, s.wHour, s.wMinute, s.wSecond);
			}

			public static WinMethods.SYSTEMTIME DateTimeToSysTime(DateTime time)
			{
				WinMethods.SYSTEMTIME systemtime1 = new WinMethods.SYSTEMTIME();
				systemtime1.wYear = (short) time.Year;
				systemtime1.wMonth = (short) time.Month;
				systemtime1.wDayOfWeek = (short) time.DayOfWeek;
				systemtime1.wDay = (short) time.Day;
				systemtime1.wHour = (short) time.Hour;
				systemtime1.wMinute = (short) time.Minute;
				systemtime1.wSecond = (short) time.Second;
				systemtime1.wMilliseconds = 0;
				return systemtime1;
			}

			#endregion

			#region Behavior Property
	
			[Category("Behavior"),Description ("Range of min and max value")]
			public RangeDate RangeValue
			{
				get	{return m_RangeValue;}
				set	
				{
					m_RangeValue =value;
				}
			}

			[Category("Behavior"),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public new DateTime DefaultValue
			{
				get {return m_DefaultValue;}
				set
				{
					m_DefaultValue = value;
					if(this.DesignMode)
					{
						this.m_Value=value;
					}
				}
			}


			#endregion

			#region Masked Public Property

			[Category("Masked")]
			[Description("Mask format ")] 
			[RefreshProperties(RefreshProperties.All)]
			public string MaskFormat
			{
				get{return m_mskFormat;}
				//set{ mskFormat = value;}
			}


			[Category("Masked"),DefaultValue('_'),Description("Sets the Input Char default '_'"),RefreshProperties(RefreshProperties.All)]
			public char InputChar
			{
				// "_" default
				get{return m_inpChar;}
				set
				{ 
					if(value !=m_inpChar && value !='\0')
					{
						m_inpChar = value;
						SetInputMask(m_mask);
						this.Invalidate(true);
						//InputMask = m_mask;
					}
				}
			}

	
			[Category("Masked")]
			[Description("Sets the Input Mask "
				 + "(digit 0 = required  9 = optional)"
				 + "(letter A= required a = optional)"
				 + "(letter or digit D = required d = optional)")]
			[RefreshProperties(RefreshProperties.All)]
			[DefaultValue("")]
			public string InputMask
			{
				get{return m_mask;}
				set
				{
					if(value!=m_mask)
					{
						SetInputMask(value);
						this.Invalidate(true);
					}
				}
			}

			[Category("Masked")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
			public bool IsValidMask
			{
				get{return IsValidString(base.Text,false);}
			}

			#endregion

			#region Masked Private Property

			private Hashtable Regxs
			{
				get
				{
					if(m_regexps == null)
					{
						m_regexps = new Hashtable();

						// build regexps
						m_regexps.Add('0', @"[0-9]");		// digit required
						m_regexps.Add('9', @"[0-9 ]");		// digit/space not required
					
						m_regexps.Add('L', @"[a-z]");		// letter a-z required
						m_regexps.Add('l', @"[a-z ]");		// letter a-z not required

						m_regexps.Add('U', @"[A-Z]");		// letter A-Z required
						m_regexps.Add('u', @"[A-Z ]");		// letter A-Z not required

						m_regexps.Add('A', @"[a-zA-Z]");	// letter required
						m_regexps.Add('a', @"[a-zA-Z ]");	// letter not required

						m_regexps.Add('D', @"[a-zA-Z0-9]");		// letter or digit required
						m_regexps.Add('d', @"[a-zA-Z0-9 ]");	// letter or digit not required

						m_regexps.Add('C', @".");		// any char

						//9luad optionals
						// IMPORTANT: MUST add and new mask chars to this regexp!
						m_regexps.Add('@', @"[09LlUuAaDdC]");	// used for input char testing
					}

					return m_regexps;
				}
			}

			#endregion

			#region Masked Event

			protected override void OnKeyPress(KeyPressEventArgs e)
			{

				if(m_mask.Length >0)
				{
					if(base.Text.Length > m_mask.Length)
					{
						//throw new FormatException("Incorrect format value , Text Longer then Input Mask");
						MsgBox.ShowError("Incorrect format value , Text Longer then Input Mask");
						return;
					}

		
					int strt =base.SelectionStart;
					int len =0;//base.SelectionLength;
					int p;

					try
					{
						// Handle Backspace -> replace previous char with inpchar and select
						if(e.KeyChar == 0x08)
						{
							string s =base.Text;
							p = Prev(strt);
							if(p <= 0)
								base.Text = "";
							else if(p != strt)
							{

								if(len==0)
									len=1;
								s=s.Remove(p,len);
								s=s.Insert(p,m_mskFormat.Substring(p, len));

								base.Text = s;
								//this.Modified =true;
								base.SelectionStart = p;
								base.SelectionLength =0;// 1;
					
							}
							m_location = p;
							e.Handled = true;
							return;
						}
	
						if(strt >= m_mskFormat.Length )
						{
							e.Handled =true;
							return;
						}
	
						// handle startup, runs once
						if(m_mskFormat[strt] != m_inpChar)
						{
							strt = Next(strt-1);//-1);
							len = 1;
						}

						// update display if valid char entered
						if(IsValidChar(e.KeyChar, (int)m_posNdx[strt]))
						{
							// assemble new text
							string t = "";
							t =base.Text.Substring(0,this.SelectionStart);//  strt);
							t += e.KeyChar.ToString();

							if((strt + len) !=base.MaxLength)// && (strt + len) >0 )
							{
								if(len>0)
									t += m_mskFormat.Substring(strt + 1, len - 1);
								else if((strt+1)  < m_mskFormat.Length )
									t += m_mskFormat.Substring(strt + 1);
				
								if((strt + 1 )  < base.Text.Length) 
									t=t.Replace(t.Substring (strt+1),Text.Substring (strt+1));
		
								//t +=base.Text.Substring(strt + 1 );

							}
							else
								t += m_mskFormat.Substring(strt + 1);

							base.Text = t;
							//this.Modified =true;

							// select next input char
							strt = Next(strt);
							base.SelectionStart = strt;
							m_location = strt;
							base.SelectionLength =0;// 1;
						}
						//e.Handled = true;
						e.Handled = HandelKey.HandleDate(this,SelectedText,e.KeyChar);
					}
					catch(Exception ex)
					{
						MsgBox.ShowError(ex.Message);
					}
				}
				else
				//e.Handled = true;
				if(KeyPressInternal!=null)
					KeyPressInternal(this,e); 
				
			}

			protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
			{
				// return true to discontinue processing
				if(m_mskFormat.Length ==0 )
					return base.ProcessCmdKey(ref msg, keyData);

				// NOTES: 
				//	1) break; causes warnings below
				//	2) m_location tracks caret location, always the start of selected char
				int strt =base.SelectionStart;
				int len =base.SelectionLength;//0
				int end = +base.SelectionLength - 1;//strt
				string s =base.Text;
				int p;
		
				if(strt >= m_mskFormat.Length )
					return false;

				// handle startup, runs once
				if(m_mskFormat[strt] != m_inpChar)
				{
					strt = Next(-1);
					len = 1;
				}


				switch(keyData)
				{
					case Keys.Delete:
						// delete selection, replace with input format

						p=strt;
						if(p <= s.Length )
						{

							if(len==0)
								len=1;
				
							s=s.Remove(p,len);
							s=s.Insert(p,m_mskFormat.Substring(p, len));
							base.Text=s;
					
							//base.Text = s.Substring(0, strt) + m_mskFormat.Substring(strt, len) + s.Substring(strt + len);
							base.SelectionStart = strt;
							base.SelectionLength =0;// 1;
							m_location = strt;
							if(ModifiedInternal!=null)
								ModifiedInternal(this,EventArgs.Empty ); 
						}
						return true;

					case Keys.V | Keys.Control:
					case Keys.Insert | Keys.Shift:
						// attempt paste
						// NOTES:
						//	1) Paste is likely to have literals since it must be copied from somewhere
						IDataObject iData = Clipboard.GetDataObject();

						// assemble new text
						string t = s.Substring(0, strt)
							+ (string)iData.GetData(DataFormats.Text)
							+ s.Substring(strt + len);

						// check if data to be pasted is convertable to inputType
						if(IsValidString(t,false ))
						{
							base.Text = t;
							if(ModifiedInternal!=null)
								ModifiedInternal(this,EventArgs.Empty ); 
							//base.Modified =true;
						}
						else
						{
							//MessageBox.Show (RM.GetString(RM.ValueNotMatchMask ));
							//throw new ApplicationException(RM.GetString(RM.ValueNotMatchMask ));

							//else if(m_errInvalid)
							//	throw new ApplicationException("Input String Does Not Match Input Mask");
						}
						return true;

					default:
						return base.ProcessCmdKey(ref msg, keyData);
				}
			}

			#endregion

			#region Mask functions

			private void SetInputMask(string value)
			{
				//m_maskChg = true;
				m_mask = value;
				SetupMask();

				// runtime handling, reset text if current text is not valid
				if(DesignMode == true ||base.Text.Length == 0 || !IsValidString(base.Text,false))
					base.Text = m_mskFormat;
				//else
				//{
				// reformat current text with new mask
				//	this.Value = this.Value;
				//}
				
				base.MaxLength= m_mskFormat.Length ==0 ? System.Int16.MaxValue : m_mskFormat.Length;
				//m_maskChg = false;

			}

			public void AppendMaskedText(string s)
			{

				string value=s;// GetDateFormat(s);

				if(value.Length ==0) 
				{
					AppendDateText(m_DefaultValue);
					return;
					//value =m_DefaultValue.ToString (m_Format);
				}

				if(m_mskFormat.Length >0 )
				{
					if(value.Length > m_mskFormat.Length)
					{
						//Fix_Value
						AppendDateText(value);
						return;
					}
					// if input string length doesn't match format length
					//	this is a problem! this means that Text input
					//	string MUST have optional missing chars

					//string v =value;

					if(value == "")
						return;//value = m_mskFormat;   
					else if(IsValidString(value,false))//Fix_Mask&& value.Length == m_mskFormat.Length)
					{

						base.Text =value;
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
				//base.Text =value;//.ToString (m_Format);

			}

			private bool IsValidChar(char input, int pos)
			{
				// validate input char against mask
				return Regex.IsMatch(input.ToString(), (string)Regxs[InputMask[pos]]);
			}

			private bool IsMaskChar(char input)
			{
				// check char
				return  Regex.IsMatch(input.ToString(), (string)Regxs[MASK_KEY]);
			}

			private void SetupMask()
			{
				// used to build position translation map from mask string
				//	and input format
				if(InputMask ==null) return;


				string s = InputMask;
				m_mskFormat = "";

				// reset index
				if(m_posNdx == null)
					m_posNdx = new Hashtable();
				else
					m_posNdx.Clear();

				int cnt = 0;
				m_requiredCnt = 0;
				m_optionalCnt = 0;

				for(int i = 0; i < s.Length; i++)
				{
					if(IsMaskChar(s[i]))
					{
						m_posNdx.Add(cnt, i);
						m_mskFormat += m_inpChar;
						// update optional/required char counts
						if(((string)Regxs[InputMask[i]]).IndexOf(' ') != -1)
							m_optionalCnt++;
						else
							m_requiredCnt++;
					}
					else if(s[i] == '\\')
					{
						// escape char
						i++;
						m_mskFormat += s[i].ToString();
					}
					else
						m_mskFormat += s[i].ToString();

					cnt++;
				}
			}

			private int Prev(int startPos)
			{
				// return previous input char position
				// returns current position if no input chars to the left
				// caller must decide what to do with this
				int strt = startPos;
				int ret = strt;

				if(strt==0)
					return 0;
				else
				{
					while(strt > 0)
					{
						strt--;
						if(m_mskFormat[strt] == m_inpChar)
							return strt;
					}
					return ret-1;			
				}
			}

			private int Next(int startPos)
			{
				// return next input char position
				// returns current position if no input chars to the left
				// caller must decide what to do with this
				int strt = startPos;
				int ret = strt;
			
				while(strt <base.MaxLength-1 )
				{
					strt++;
					if(m_mskFormat[strt] == m_inpChar)
						return strt;
				}

				return ret+1;			
			}
			#endregion

			#region Format Property

			[Browsable(false)]
			public WinMethods.SYSTEMTIME SystemTimeValue
			{
				get{return this.systemtime;}
			}

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
			public DateTime Value
			{
				get
				{
					return this.m_Value;
				}
				set
				{
					if (value != m_Value)
					{
						if (this.RangeValue.IsValid(value))
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


			[Bindable(true),Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
					else
					{
						AppendMaskedText (value);
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

			[DefaultValue(""),Browsable(true)] 
			public override string Format
			{
				get {return base.Format;}
				set {base.Format = value;}
			}

			[Category("Appearance"),DefaultValue(DateFormats.ShortDate),RefreshProperties(RefreshProperties.All)]   
			public new DateFormats FormatType
			{
				get{return m_FormatType;}
				set
				{
					if(m_FormatType!=value)
					{
						m_FormatType=value;
						if(useCustomFormat)
						{
							m_RangeValue =new  RangeDate(Range.MinDate,Range.MaxDate );	
							return;	
						}
						Format=Util.Types.GetFormat(value,Format);
						base.FormatType=Util.Types.GetFormat(m_FormatType);

						Invalidate ();
					}
				}
			}


			internal void SetFormatInputMask(string format)
			{
				DateTimeFormatInfo dti = new CultureInfo( CultureInfo.CurrentCulture.Name, false ).DateTimeFormat;
				string sf ="";
    
				switch(format)
				{
					case "d":
						sf =dti.ShortDatePattern;
						sf=sf.Replace("MM","00");
						sf=sf.Replace("M","09");
						sf=sf.Replace("dd","00");
						sf=sf.Replace("d","09");
						sf=sf.Replace("y","0");
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
						DateTime dt=new DateTime(2222,11,22,22,22,22);
						sf=dt.ToString("G");

						sf=sf.Replace("2","0");
						sf=sf.Replace("1","0");
						sf=sf.Replace("AM","AA");
						sf=sf.Replace("PM","AA");
						break;
					default : //case DateFormats.CustomDate:
						sf=this.Format;
						sf=sf.Replace("MMMM","aaaaaaaaaaaa");
						sf=sf.Replace("MMM","AAA");
						sf=sf.Replace("MM","00");
						sf=sf.Replace("M","09");
						sf=sf.Replace("dddd","aaaaaaaaaaaa");
						sf=sf.Replace("ddd","AAA");
						sf=sf.Replace("dd","00");
						sf=sf.Replace("d","09");
						sf=sf.Replace("y","0");
						sf=sf.Replace("HH","00");
						sf=sf.Replace("H","09");
						sf=sf.Replace("m","0");
						sf=sf.Replace("s","0");
				
						sf=sf.Replace("tt","AA");
						sf=sf.Replace("AM","AA");
						sf=sf.Replace("PM","AA");
		
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
				base.Text =""; 
				this.m_Value = DateTime.Now;
				//this.userHasSetValue = false;
				//this.Checked = false;
				//this.OnValueChanged(EventArgs.Empty);
				//this.OnTextChanged(EventArgs.Empty);
			}

			internal  bool IsValid(string value)
			{
				if ((value == null) || (value.Length == 0))
				{
					return false;
				}
				try
				{
					//DateTime.Parse(value);
					if(! mControl.Util.Info.IsDateTime(value))
						return false;
				}
				catch
				{
                  return false;
				}
//				if(!Regx.IsDateTime(value,true))//TypeUtils.IsDate (value)) 
//				{
//					return false;
//				}
//				if(!Regx.IsValidDate(this.m_FormatType,value))//TypeUtils.IsDate (value)) 
//				{
//					return false;
//				}
				if (!m_RangeValue.IsValid (value))
				{
					return false;
				}
				return true;			
			}

			#endregion

			#region Validation function

			//TODO: ValidString
			private bool IsValidString(string s,bool isMsg)
			{
				//string m_InvalidMsg="";
				bool ret = true;
				int posMsk = 0;
				int posStr = 0;
				int opCnt=0;
				int pos=0;
				// validate considering optional chars
				while(ret && posMsk < m_mskFormat.Length) 
				{
					if(m_mskFormat[posMsk] == m_inpChar)
					{
						// check input is valid including "optional" -> space in regexp
						if(posMsk >= s.Length)
						{
							// must be optional input
							ret =opCnt+pos== s.Length || ((string)Regxs[InputMask[(int)m_posNdx[posMsk]]]).IndexOf(' ') != -1;
						}
						else
						{
							// valid or optional
							ret = IsValidChar(s[posStr], (int)m_posNdx[posMsk]);
							if(!ret)
								ret |= ((string)Regxs[InputMask[(int)m_posNdx[posMsk]]]).IndexOf(' ') != -1
									&& (s[posStr] == ' ' || s[posStr] == m_inpChar);
							if(!ret)
							{
								ret="9luad".IndexOf(InputMask[posMsk],0)!=-1;
								if(ret)
								{
									posMsk++;
									opCnt++;
								}
							}
							//9luad
						}
					}
					else
					{
						// check literal match
						if(posMsk < s.Length)
							ret = s[posStr] == m_mskFormat[posMsk];
					}
					pos++;
					posMsk++;
					posStr++;
				}
				if(!ret && isMsg)
				{
					if(s.Length ==0)
						ret=true;
					else
					{
						//m_InvalidMsg= RM.GetString(RM.IlegalChar);
						//OnErrorOcurred(new ErrorOcurredEventArgs(m_InvalidMsg));
					}
				}
				return ret;
			}

			#endregion

			#region IControlKeyAction Members
		
			public override  void ActionDefaultValue()
			{
				base.Text =m_DefaultValue.ToString (Format);  
			}

			#endregion

		}

	}

