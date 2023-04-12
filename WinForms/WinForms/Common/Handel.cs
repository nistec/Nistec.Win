using System;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Nistec.Data;
   
  
namespace Nistec.WinForms
{

	public sealed class HandelKey
	{

		#region Handel key Event

//		public bool HandelKeyPress(Formats formatType,char KeyChar)
//		{
//			bool Handled=false;
//
//			switch(formatType)
//			{
//				case Formats.None:
//					Handled =false; 
//					break;
//				case Formats.GeneralNumber:
//				case Formats.FixNumber:
//				case Formats.StandadNumber:
//				case Formats.Money:
//				case Formats.Percent:
//					Handled = !HandleNumeric(KeyChar);
//					break;
//				case Formats.LongDate:
//					base.OnKeyPress(e);
//					break;
//				case Formats.GeneralDate:
//				case Formats.ShortDate:
//					Handled = HandleDate(KeyChar);
//					break;
//				case Formats.LongTime:
//				case Formats.ShortTime:
//					Handled = HandleTime(KeyChar);
//					break;
//				default:
//					Handled = false;
//					break;
//			}	
//			return Handel;
//		}

		#endregion

		#region Handle

		public static  bool HandleDate(IMcTextBox ctl,string SelectedText, char keyChar)		
		{			
			if (!ctl.ReadOnly) 			
			{	
				string text= ctl.Text;

				if (Char.IsDigit(keyChar) || 					
					keyChar == CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator[0] || 
					keyChar == 8 ||					
					keyChar == 22 || keyChar == 3 || 					
					keyChar == 26 || keyChar == 24 || keyChar == 27)				
				{ 					
					if (keyChar != 8 && keyChar != 22 && 						
						keyChar != 3 && keyChar != 26 && keyChar != 24 && keyChar != 27)					
					{						
						if (SelectedText == text) 
							ctl.Text="";//Clear();						
						//						
						if (keyChar != CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator[0] )						
						{							
							if (text.Length == 2 || text.Length == 5) 						
							{
								ctl.Text=(CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);						
								//Parent.AppendText(CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);						
							}
						}						
					}				
				}				
				else				
				{	
					return true;
				//					string m_InvalidMsg=  RM.GetString(WinMcResexKeys.OnlyDigitsBarAccepted,WinMcResexKeys.Root);						
				//					OnErrorOcurred(new ErrorOcurredEventArgs(m_InvalidMsg));
				}					
								
			}			
			return false;	    
		}
		
//		public static   bool HandleIp(char keyChar)
//		{
//			//m_InvalidMsg="";
//			if (!ReadOnly)
//			{
//				if(Char.IsDigit(keyChar) || 
//					keyChar == '.' || keyChar == 8 ||
//					keyChar == 22  || keyChar == 3 || 
//					keyChar == 26  || keyChar == 24)
//				{
//				}
//				else
//				{
//					//					string m_InvalidMsg= RM.GetString(WinMcResexKeys.OnlyDigitsPointsAccepted,WinMcResexKeys.Root);
//					//					OnErrorOcurred(new ErrorOcurredEventArgs(m_InvalidMsg));
//					return true;
//				}
//			}
//			return false;
//		}

		public static   bool HandleTime(IMcTextBox ctl,string SelectedText,char keyChar)		
		{			
			//m_InvalidMsg="";
			if (!ctl.ReadOnly) 			
			{				
				if (Char.IsDigit(keyChar) || 					
					keyChar == CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator[0] || 
					keyChar == 8 ||					
					keyChar == 22 || 
					keyChar == 3 || 					
					keyChar == 26 || 
					keyChar == 24)				
				{ 					
					if (keyChar != 8 && keyChar != 22 && 						
						keyChar != 3 && keyChar != 26 && keyChar != 24)					
					{						
						if (SelectedText == ctl.Text) 
							ctl.Text="";//.Clear();						
						//						
						if (keyChar != CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator[0] )						
						{							
							if (ctl.Text.Length == 2 || ctl.Text.Length == 5) 
							{
								ctl.Text=(CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator);
								//Parent.AppendText(CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator);
							}
						}						
						//						
						//if(this.Text.Length == 8)							
						//	return true;					
					}				
				}				
				else				
				{					
					//					string m_InvalidMsg= RM.GetString(WinMcResexKeys.OnlyDigitsTwoPointsAccepted,WinMcResexKeys.Root);						
					//					OnErrorOcurred(new ErrorOcurredEventArgs(m_InvalidMsg));
					return true;				
				}			
			}			
			return false;		
		}
		
		//		protected  bool HandleTextUnAcceptChars(char keyChar)
		//		{
		//			if (!ReadOnly) 
		//			{
		//				if ( this.m_UnAcceptChars.IndexOf(keyChar) > -1) 
		//				{
		//					ErrorMsg= RM.GetString(RM.IlegalChar);
		//				}
		//				return true;
		//			}
		//			return false;
		//		}

		#endregion

		#region Handel Numbers

		public static   bool HandleNumeric(IMcTextBox ctl,string SelectedText,ref int SelectionStart,int SelectionLength,int DecPlaces,char pressedChar)
		{
			//m_InvalidMsg="";
			if(ctl.ReadOnly)
			{
				return true;
			}

			char decSep = Convert.ToChar(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, CultureInfo.CurrentCulture);
			string val         = ctl.Text;
			int    length      = val.Length;
			int    curPos      = SelectionStart;
			bool   HasSeparator = val.IndexOf(decSep) > -1;
			int    sepPos      = val.IndexOf(decSep);

			if(SelectionLength > 0)
			{
				val = val.Remove(SelectionStart,SelectionLength);
				length = val.Length;
			}
			
			if(char.IsDigit(pressedChar))
			{
                
				if(val.IndexOf("-") > -1 && val.IndexOf("-") > curPos-1)
				{
					return false;
				}
								
				if(((val.StartsWith("0") && curPos == 1) || (val.StartsWith("-0") && curPos == 2)))
				{
					return false;
				}
					
				if(HasSeparator)
				{
					if(curPos > sepPos)
					{
						if((curPos - sepPos) >  DecPlaces )
						{
							return true;
						}

						if((length - sepPos) >  DecPlaces )
						{
							string newVal = val.Remove(curPos,1);
							newVal = newVal.Insert(curPos,pressedChar.ToString(CultureInfo.CurrentCulture));
							ctl.ResetText() ; 
							ctl.AppendText(newVal);//ctl.Text   = newVal;//Value
							SelectionStart = curPos + 1; 
							return true;
						}
					}
				}

				if(pressedChar == '0')
				{					
					if(val.StartsWith("-") && length > 1 && curPos == 1)
					{
						return false;					
					}

					if(length > 0 && curPos == 0)
					{
						return false;	
					}
				}

				return false;
			}
			else
			{
				if(pressedChar == decSep && !HasSeparator &&  DecPlaces  > 0)
				{
					if(length - curPos >  DecPlaces )
					{
						return true;
					}

					return false;
				}
				if(pressedChar == '-')
				{
					if(!val.StartsWith("-") && curPos == 0)
					{
						return false;
					}
				}
				if(pressedChar == '\b')
				{
					return false;
				}

				return true;
			}
		}

		#endregion

		#region Handel DateTime

		[Description("Handel key press by date mask ##/##/#### ,the DateSeparator from CultureInfo")]
		public static  bool HandleMaskDate(IMcTextBox ctl,string SelectedText,ref int SelectionStart,int SelectionLength,char pressedChar)
		{
			//m_InvalidMsg="";
			if(ctl.ReadOnly)
			{
				return true;
			}

			const int maxLen=10;
			char Sep = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator, CultureInfo.CurrentCulture);
			string val         = ctl.Text;
			int    length      = val.Length;
			int    curPos      = SelectionStart;

			if(SelectionLength > 0)
			{
				val = val.Remove(SelectionStart,SelectionLength);
				length = val.Length;
			}
			
			if(char.IsDigit(pressedChar))
			{
                if ((length - SelectionLength) >= maxLen)//length>=maxLen)
                    return true;
				if(curPos==1 || curPos==4)
				{
					val = val.Insert(curPos,pressedChar.ToString(CultureInfo.CurrentCulture));
                    if (!(val.Length > (curPos + 1) && val[curPos + 1] == Sep))
                    {
                        val = val.Insert(curPos + 1, Sep.ToString(CultureInfo.CurrentCulture));
                    }
					ctl.ResetText() ; 
					ctl.AppendText(val);// Text   = val;
					SelectionStart = curPos + 2; 
					return true;
				}
	
				return false;
			}
			else if(pressedChar == '\b')
			{
					if(curPos>2)
					{
						char ch=Convert.ToChar(val.Substring (curPos-1,1));
						if(ch==Sep)
						{
							val = val.Remove(curPos-2,2);
							ctl.ResetText() ; 
							ctl.AppendText(val);// Text   = val;

							SelectionStart = curPos -2; 
							return true;

						}
					}
					return false;
			}

			return true;
		}

		[Description("Handel key press by datetime mask ##/##/#### ##:##:## ,the DateSeparator from CultureInfo")]
		public static  bool HandleMaskGeneralDateTime(IMcTextBox ctl,string SelectedText,ref int SelectionStart,int SelectionLength,char pressedChar)
		{
			//m_InvalidMsg="";
			if(ctl.ReadOnly)
			{
				return true;
			}

			const int maxLen=19;
			char Sep = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator, CultureInfo.CurrentCulture);
			char TimeSep = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator , CultureInfo.CurrentCulture);
			string val         = ctl.Text;
			int    length      = val.Length;
			int    curPos      = SelectionStart;

			if(SelectionLength > 0)
			{
				val = val.Remove(SelectionStart,SelectionLength);
				length = val.Length;
			}
			
			if(char.IsDigit(pressedChar))
			{
                if ((length - SelectionLength) >= maxLen)
                    return true;
				if(curPos==1 || curPos==4)
				{
 					val = val.Insert(curPos,pressedChar.ToString(CultureInfo.CurrentCulture));
                    if (!(val.Length >(curPos + 1) &&  val[curPos + 1] == Sep))
                    {
                        val = val.Insert(curPos + 1, Sep.ToString(CultureInfo.CurrentCulture));
                    }
                    ctl.ResetText() ; 
					ctl.AppendText(val);// Text   = val;
					SelectionStart = curPos + 2; 
					return true;
				}
				else if(curPos==10)
				{
                    if (!(val.Length > curPos && val[curPos ] ==' '))
                    {
                        val = val.Insert(curPos, " ");
                    }
					ctl.ResetText() ; 
					ctl.AppendText(val);
					SelectionStart = curPos + 1; 
					return true;
				}
				else if(curPos==12 || curPos==15)
				{
					val = val.Insert(curPos,pressedChar.ToString(CultureInfo.CurrentCulture));
                    if (!(val.Length > (curPos + 1) && val[curPos + 1] == TimeSep))
                    {
                        val = val.Insert(curPos + 1, TimeSep.ToString(CultureInfo.CurrentCulture));
                    }
					ctl.ResetText(); 
					ctl.AppendText(val);
					SelectionStart = curPos + 2; 
					return true;
				}
		
				return false;
			}
			else if(pressedChar == '\b')
			{
				if(curPos>2)
				{
					char ch=Convert.ToChar(val.Substring (curPos-1,1));
					if(ch==Sep || ch==' ' || ch==TimeSep)
					{
						val = val.Remove(curPos-2,2);
						ctl.ResetText() ; 
						ctl.AppendText(val);
						SelectionStart = curPos -2; 
						return true;

					}
				}
				return false;
			}

			return true;
		}

		#endregion

		#region Handel Time

		[Description("Handel key press by time mask ##:##:## ,the DateSeparator from CultureInfo")]
		public static  bool HandleMaskLongTime(IMcTextBox ctl,string SelectedText,ref int SelectionStart,int SelectionLength,char pressedChar)
		{
			//m_InvalidMsg="";
			if(ctl.ReadOnly)
			{
				return true;
			}

			const int maxLen=8;
			char Sep = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator , CultureInfo.CurrentCulture);
			string val         = ctl.Text;
			int    length      = val.Length;
			int    curPos      = SelectionStart;

			if(SelectionLength > 0)
			{
				val = val.Remove(SelectionStart,SelectionLength);
				length = val.Length;
			}
			
			if(char.IsDigit(pressedChar))
			{
                if ((length - SelectionLength) >= maxLen)
                    return true;
				if(curPos==1 || curPos==4)
				{
					val = val.Insert(curPos,pressedChar.ToString(CultureInfo.CurrentCulture));
                    if (!(val.Length > (curPos + 1) && val[curPos + 1] == Sep))
                    {
                        val = val.Insert(curPos + 1, Sep.ToString(CultureInfo.CurrentCulture));
                    }
					ctl.ResetText() ; 
					ctl.AppendText(val);
					SelectionStart = curPos + 2; 
					return true;
				}
	
				return false;
			}
			else if(pressedChar == '\b')
			{
				if(curPos>2)
				{
					char ch=Convert.ToChar(val.Substring (curPos-1,1));
					if(ch==Sep)
					{
						val = val.Remove(curPos-2,2);
						ctl.ResetText() ; 
						ctl.AppendText(val);
						SelectionStart = curPos -2; 
						return true;

					}
				}
				return false;
			}

			return true;
		}

		[Description("Handel key press by time mask ##:## ,the DateSeparator from CultureInfo")]
		public static  bool HandleMaskShortTime(IMcTextBox ctl,string SelectedText,ref int SelectionStart,int SelectionLength,char pressedChar)
		{
			//m_InvalidMsg="";
			if(ctl.ReadOnly)
			{
				return true;
			}

			const int maxLen=5;
			char Sep = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator , CultureInfo.CurrentCulture);
			string val         = ctl.Text;
			int    length      = val.Length;
			int    curPos      = SelectionStart;

			if(SelectionLength > 0)
			{
				val = val.Remove(SelectionStart,SelectionLength);
				length = val.Length;
			}
			
		    if(char.IsDigit(pressedChar))
			{
                if ((length - SelectionLength) >= maxLen)
                    return true;
				if(curPos==1)
				{
					val = val.Insert(curPos,pressedChar.ToString(CultureInfo.CurrentCulture));
                    if (!(val.Length > (curPos + 1) && val[curPos + 1] == Sep))
                    {
                        val = val.Insert(curPos + 1, Sep.ToString(CultureInfo.CurrentCulture));
                    }
					ctl.ResetText() ; 
					ctl.AppendText(val);
					SelectionStart = curPos + 2; 
					return true;

				}
	
				return false;
			}
			else if(pressedChar == '\b')
			{
				if(curPos>2)
				{
					char ch=Convert.ToChar(val.Substring (curPos-1,1));
					if(ch==Sep)
					{
						val = val.Remove(curPos-2,2);
						ctl.ResetText() ; 
						ctl.AppendText(val);
						SelectionStart = curPos -2; 
						return true;

					}
				}
				return false;
			}

			return true;
		}
		#endregion

	}

}
