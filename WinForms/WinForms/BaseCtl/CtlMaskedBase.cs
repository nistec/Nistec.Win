using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections;
  

using Nistec.Data;
using Nistec.Win;



namespace Nistec.WinForms.Controls
{


	[ToolboxItem(false)]
	public class McMaskedBase : McTextBoxBase
	{	

		#region Mask Members
		//protected bool useMask=true;

		//Mask
		private string m_mask="";			// input mask
		private string m_mskFormat="";		// display format (mask with input chars replaced by input char)
		private char m_inpChar='_';
		private char m_ReplaceChar='\0';
		//private bool m_maskChg=false;
		//private bool m_stdmaskChg=false;
		private Hashtable m_regexps;
		private Hashtable m_posNdx;			// hold position translation map
		private int m_location=0;
		private bool m_errInvalid=false;	// Error on invalid Text/Value input? -> true throws error, false ignore
		private int m_requiredCnt=0;		// required char count
		private int m_optionalCnt=0;		// optional char count
		protected bool useMask=true;
		// allowed mask chars
		private const char MASK_KEY = '@';

		public event EventHandler ModifiedInternal;

		#endregion

		#region Constructor

        internal McMaskedBase()
            : base()
		{
			
		}

		#endregion

		#region Appearance Property

		[Category("Appearance")]
		[Bindable(true)]
		[Browsable(true)]
		public override string Text
		{
			get{return base.Text;}
			set
			{
		
				if(value.Length ==0) 
				{
					base.Text =DefaultValue;
					return;
				}
                /*Value*/
                if (value == Text)
                    return;

				if(useMask && m_mskFormat.Length >0 )
				{
					//string v =Value;
					if(value.Length > m_mskFormat.Length)
						return;
					// if input string length doesn't match format length
					//	this is a problem! this means that Text input
					//	string MUST have optional missing chars
					if(value == "")
						base.Text = m_mskFormat;
					else if(IsValidString(value,false)&& value.Length == m_mskFormat.Length)
					{
						// must check optional input chars
//						bool ok = true;
//						int fpos = 0;
//						while(ok && fpos < m_mskFormat.Length)
//						{
//							if(m_mskFormat[fpos] == m_inpChar 
//								&& !IsValidChar(value[fpos], (int)m_posNdx[fpos]))
//								ok &= value[fpos] == m_inpChar;
//							fpos++;
//						}
//
//						if(ok)
							base.Text =value;//.Replace(this.m_inpChar,m_ReplaceChar);
					}
					else if(m_errInvalid)
						throw new ApplicationException("Input String Does Not Match Input Mask");
					else
						base.Text = value;
			
				}
				else
				{
                    base.Text = value;
				}
			}
		}

		protected virtual void AppendMaskedText(string s)
		{
			string value=s;

			if(value.Length ==0) 
			{
				base.Text =value;
				return;
				//value =m_DefaultValue;
			}
                
		
			if(this.useMask && m_mskFormat.Length >0 )
			{
				//string v =Value;
				if(value.Length > m_mskFormat.Length)
					return;
				// if input string length doesn't match format length
				//	this is a problem! this means that Text input
				//	string MUST have optional missing chars
				if(value == "")
					base.Text = m_mskFormat;
				else if(IsValidString(value,false))//Fix_Mask && value.Length == m_mskFormat.Length)
				{
					// must check optional input chars
					bool ok = true;
					int fpos = 0;                            //Fix_Mask
					while(ok && fpos < m_mskFormat.Length && fpos < value.Length)
					{
						if(m_mskFormat[fpos] == m_inpChar 
							&& !IsValidChar(value[fpos], (int)m_posNdx[fpos]))
							ok &= value[fpos] == m_inpChar;
						fpos++;
					}

					if(ok)
						base.Text =value;//.Replace(this.m_inpChar,m_ReplaceChar);
				}
				else //if(m_errInvalid)
					throw new ApplicationException("Input String Does Not Match Input Mask");
				//else
				//	base.Text = value;
			
			}
			else
			{
				base.Text = value;
			}
		}

		protected void SetInputMask(string value)
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

		#endregion

		#region Masked Public Property

		[Category("Masked")]
		[Description("define char to replace input char in value property")] 
		[DefaultValue('\0')]
		public char ReplaceChar
		{
			get{return m_ReplaceChar;}
			set{m_ReplaceChar=value;}
		}


		[Category("Masked")]
		[Description("Text value without mask char ")] 
		[DefaultValue("")]
		public string Value
		{
			get
			{
				if(m_mskFormat.Length  == 0)
					return base.Text;
				else
				{
					char ch;
					ch=m_ReplaceChar =='\0' ? ' ' : m_ReplaceChar;

					return base.Text.Replace(m_inpChar,ch);
				}
			}		
		}

		[Category("Masked")]
		[Description("Mask format ")] 
		[RefreshProperties(RefreshProperties.All)]
		public string MaskFormat
		{
			get{return m_mskFormat;}
			//set{ mskFormat = value;}
		}


		[Category("Masked")]
		[Description("Sets the Input Char default '_'")] 
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue('_')]
		public char InputChar
		{
			// "_" default
			get{return m_inpChar;}
			set
			{
				if(value !='\0')
					m_inpChar = value;
				//InputMask = m_mask;
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

				//base.MaxLength= m_mskFormat.Length;
				//m_maskChg = false;
			}
		}

//		[Category("Masked")]
//		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool IsValid()
		{
			//get{
				   return IsValidString(base.Text,false);
			  // }
		}

//		[Category("Masked")]
//		[Description("Throw Error On Invalid Text/Value Property")]
//		[Browsable(false)]
//		[DefaultValue("")]
//		public bool ErrorInvalid
//		{
//			get{return m_errInvalid;}
//			set{m_errInvalid = value;}
//		}

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

					// IMPORTANT: MUST add and new mask chars to this regexp!
					m_regexps.Add('@', @"[09LlUuAaDdC]");	// used for input char testing
				}

				return m_regexps;
			}
		}

		#endregion

		#region Masked Handel key Event

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
            if (this.ReadOnly)
                return;
			if(this.useMask && m_mask.Length >0)
			{
		
				int strt =base.SelectionStart;
				int len =0;//base.SelectionLength;
				int p;

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
						base.SelectionStart = p;
						base.SelectionLength =0;// 1;
					
					}
					m_location = p;
					e.Handled = true;
                    goto Label_01;
				}
	
				if(strt >= m_mskFormat.Length )
				{
					e.Handled =true;
                    goto Label_01;
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

					// select next input char
					strt = Next(strt);
					base.SelectionStart = strt;
					m_location = strt;
					base.SelectionLength =0;// 1;
				}
				e.Handled = true;
                goto Label_01;

			}
            //else
            //{
            //    base.OnKeyPress(e);
            //}

            Label_01:
            if (!e.Handled)
            {
                base.OnKeyPress(e);
            }
            if (!e.Handled)
                OnModified();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			// return true to discontinue processing
            if (!this.useMask || m_mskFormat.Length == 0 || ReadOnly)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
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
						OnModified();
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
						OnModified();
					}
					else if(m_errInvalid)
						throw new ApplicationException("Input String Does Not Match Input Mask");

					return true;

				default:
					return base.ProcessCmdKey(ref msg, keyData);
			}
		}


		protected virtual void OnModified()
		{
			if(ModifiedInternal!=null)
				ModifiedInternal(this,EventArgs.Empty ); 
		}


		#endregion

		#region Masked functions

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
			if(!this.useMask || InputMask ==null) return;


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

//		protected bool IsValidString(string s,bool isMsg)
//		{
//			string m_InvalidMsg="";
//			bool ret = true;
//			int pos = 0;
//			// validate considering optional chars
//			while(ret && pos < m_mskFormat.Length) 
//			{
//				if(m_mskFormat[pos] == m_inpChar)
//				{
//					// check input is valid including "optional" -> space in regexp
//					if(pos >= s.Length)
//					{
//						// must be optional input
//						ret = ((string)Regxs[InputMask[(int)m_posNdx[pos]]]).IndexOf(' ') != -1;
//					}
//					else
//					{
//						// valid or optional
//						ret = IsValidChar(s[pos], (int)m_posNdx[pos]);
//						if(!ret)
//							ret |= ((string)Regxs[InputMask[(int)m_posNdx[pos]]]).IndexOf(' ') != -1
//								&& (s[pos] == ' ' || s[pos] == m_inpChar);
//					}
//				}
//				else
//				{
//					// check literal match
//					if(pos < s.Length)
//						ret = s[pos] == m_mskFormat[pos];
//				}
//				pos++;
//			}
//			if(!ret && isMsg)
//			{
//				if(Value.Length ==0)
//					ret=true;
//				else
//				{
//					m_InvalidMsg= RM.GetString(RM.IlegalChar);
//					OnValidatingError(m_InvalidMsg);//new Nistec.WinForms.ErrorOcurredEventArgs(m_InvalidMsg));
//				}
//			}
//			return ret;
//		}
//
		//TODO: ValidString
		protected bool IsValidString(string s,bool isMsg)
		{
			if(!this.useMask)return true;

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

		protected override bool IsValidating()
		{
			if(this.m_mskFormat.Length >0)
			{
				bool ok= IsValidString(Text,true);
				if(!ok)
				{
					string msg= RM.GetString(RM.ValueNotMatchMask_v,Text);   
					OnValidatingError(msg);//new Nistec.WinForms.ErrorOcurredEventArgs(msg));
					return false;
				}
			}

			return base.IsValidating() ;
		}

		#endregion

	}
}
