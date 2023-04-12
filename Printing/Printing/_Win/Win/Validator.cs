using System;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MControl.Win
{

	public sealed class Validator 
	{

		#region Compare

		/// <summary>
		/// Compare two values using provided operator and data type.
		/// </summary>
		/// <param name="strA"></param>
		/// <param name="strB"></param>
		/// <param name="op"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool CompareValues(string strA, string strB, ValidationOperator op, BaseDataType type)
		{
			return CompareValidator.Compare(strA, strB, op, type);// WebValidator.CompareValues(strA, strB, op, type);
		}
	
		public static bool CanConvert(string text, BaseDataType type)
		{
			if(type==BaseDataType.Currency)
				return Regx.IsCurrency(text);
            else
			   return CompareValidator.CanConvert(text, type);// WebValidator.CanConvert(text, type);
		}


		#endregion

		#region Regex

		/// <summary>
		/// Utility method validation regular expression.
		/// </summary>
		/// <param name="valueText"></param>
		/// <param name="patternText"></param>
		/// <returns></returns>
		public static bool ValidateRegex(string valueText, string patternText)
		{
			Match m = Regex.Match(valueText, patternText);
			return m.Success;
		}

		#endregion

		#region Validate numbers
	
		public static bool ValidateNumber(string Text,bool required,ref string message)
		{
			if(!required && Text.Length ==0)
				return true;
			else
				return ValidateNumber(Text,RangeNumber.Empty,ref message);
		}

		public static bool ValidateNumber(string Text,bool required,RangeNumber range,ref string message)
		{
			if(!required && Text.Length ==0)
				return true;
            else
             return ValidateNumber(Text,range,ref message);
		}

        public static bool ValidateNumber(string Text, bool required, RangeType range, ref string message)
		{
			if(!required && Text.Length ==0)
				return true;
			else
				return ValidateNumber(Text,range,ref message);
		}

		public static bool ValidateCurrency(string Text,bool required,RangeNumber range,ref string message)
		{
			if(!required && Text.Length ==0)
				return true;
			else
				return ValidateCurrency(Text,range,ref message);
		}

        public static bool ValidateCurrency(string Text, bool required, RangeType range, ref string message)
		{
			if(!required && Text.Length ==0)
				return true;
			else
				return ValidateCurrency(Text,range,ref message);
		}

		public static bool ValidateNumber(string Text,RangeNumber range,ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidNumber);
				return false;
			}
			else
			{
				if (!Info.IsDouble(Text))
				{
					message= RM.GetString(RM.InvalidNumber);
					return false;
				}
				else if(!range.IsEmpty)//TODO: !=null)
				{
					bool ok= range.IsValid (System.Convert.ToDouble (Text ));
					if(!ok)
						message= RM.GetString(RM.ValueOutOfRange_v2,new object[]{range.MaxValue,range.MinValue} );						
					return ok;
				}
			}
			return true;
		}

		public static bool ValidateNumber(string Text,RangeType range,ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidNumber);
				return false;
			}
			else
			{
				if (!Info.IsDouble(Text))
				{
					message= RM.GetString(RM.InvalidNumber);
					return false;
				}
				else if(range !=null)
				{
					bool ok= false;
					if(Info.IsDouble(Text))
						ok= range.IsValid (System.Convert.ToDouble (Text ));

					if(!ok)
						message= RM.GetString(RM.ValueOutOfRange_v2,new object[]{range.MaxValue,range.MinValue} );						
					return ok;
				}
			}
			return true;
		}

		public static bool ValidateCurrency(string Text,RangeNumber range,ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidNumber);
				return false;
			}
			else
			{
				if (!Regx.IsCurrency(Text))
				{
					message= RM.GetString(RM.InvalidNumber);
					return false;
				}
				else if(!range.IsEmpty)//TODO: !=null)
				{
                    Currency c = WinRegx.ParseCurrency(Text); 
					bool ok= range.IsValid (c.Value);
					if(!ok)
						message= RM.GetString(RM.ValueOutOfRange_v2,new object[]{range.MaxValue,range.MinValue} );						
					return ok;
				}
			}
			return true;
		}

        public static bool ValidateCurrency(string Text, RangeType range, ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidNumber);
				return false;
			}
			else
			{
				if (!Regx.IsCurrency(Text))
				{
					message= RM.GetString(RM.InvalidNumber);
					return false;
				}
				else if(range !=null)
				{
                    Currency c = WinRegx.ParseCurrency(Text); 
					bool ok= range.IsValid (c.Value);
					if(!ok)
						message= RM.GetString(RM.ValueOutOfRange_v2,new object[]{range.MaxValue,range.MinValue} );						
					return ok;
				}
			}
			return true;
		}

		#endregion

		#region Validate Date

		public static bool ValidateDate(string Text,bool required,ref string message)
		{
			if(!required && Text.Length ==0)
				return true;
			else
				return ValidateDate(Text,RangeDate.Empty,ref message);
		}

		public static bool ValidateDate(string Text,bool required,RangeDate range,ref string message)
		{
			if(!required && Text.Length ==0)
				return true;
			else
				return ValidateDate(Text,range,ref message);
		}

		public static bool ValidateDate(string Text,bool required,RangeType range,ref string message)
		{
			if(!required && Text.Length ==0)
				return true;
			else
				return ValidateDate(Text,range,ref message);
		}

		public static bool ValidateDate(string Text,ref string message)		
		{			
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidDateFormat_v,Text);						
				return false;
			}
			else if (!Info.IsDate (Text))
			{
				message= RM.GetString(RM.InvalidDateFormat_v,Text);						
				return false;
			}			
			return true;
		}

		public static bool ValidateDate(string Text,RangeDate range,ref string message)		
		{			
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidDateFormat_v,Text);						
				return false;
			}
			else
			{								
				if (!Info.IsDate (Text))
				{
					message= RM.GetString(RM.InvalidDateFormat_v,Text);						
					return false;
				}
				else if(!range.IsEmpty)//TODO: !=null)
				{
					bool ok= false;
					if(Info.IsDate(Text))
						ok= range.IsValid (System.Convert.ToDateTime (Text ));
					if(!ok)
						message= RM.GetString(RM.ValueOutOfRange_v2,new object[]{range.MaxValue,range.MinValue} );						
                    return ok;
				}
			}			
			return true;
		}


        public static bool ValidateDate(string Text, RangeType range, ref string message)		
		{			
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidDateFormat_v,Text);						
				return false;
			}
			else
			{								
				if (!Info.IsDate (Text))
				{
					message= RM.GetString(RM.InvalidDateFormat_v,Text);						
					return false;
				}
				else if(range !=null)
				{
					bool ok= false;
					if(Info.IsDate(Text))
						ok= range.IsValid (System.Convert.ToDateTime (Text ));
					if(!ok)
						message= RM.GetString(RM.ValueOutOfRange_v2,new object[]{range.MaxValue,range.MinValue} );						
					return ok;
				}
			}			
			return true;
		}

		public static bool ValidateTime(string Text,bool required,ref string message)
		{
			if(!required && Text.Length ==0)
				return true;
			else
				return ValidateTime(Text,ref message);
		}

		public static bool ValidateTime(string Text,ref string message)		
		{			
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidTime);						
				return false;
			}
			else
			{				
				try				
				{					
					DateTimeFormatInfo format = CultureInfo.CurrentCulture.DateTimeFormat;				
					DateTime.Parse(Text, format);					
					return true;//TypeUtil.IsDate(Text);					
				}				
				catch				
				{					
					message= RM.GetString(RM.InvalidTime);						
					return false;
				}			
			}			
		}

		#endregion

		#region Validate Bool 

		public static bool ValidateBool(int val,bool Required,ref string message)
		{
          return ValidateBool(val.ToString (),Required,ref message);
		}

		public static bool ValidateBool(string Text,ref string message)
		{
			return ValidateBool(Text,true,ref message);
		}

		public static bool ValidateBool(string Text,bool Required,ref string message)
		{
			bool ok=false;
			if (!Required && Text.Length ==0)
              return true;

			string T1=RM.GetString(RM.True );
			string F1=RM.GetString(RM.False);
			string T2=RM.GetString(RM.Yes  );
			string F2=RM.GetString(RM.No );
			string T3=RM.GetString(RM.On  );
			string F3=RM.GetString(RM.Off );

			if(Text.Equals (T1)||Text.Equals (T2)||Text.Equals (T3)||Text.Equals (F1)||Text.Equals (F2)||Text.Equals (F3))
				return true;
			else
			{
				switch(Text)
				{
					case "True":
					case "Yes":
					case "On":
					case "1":
					case "-1":
					case "False":
					case "No":
					case "Off":
					case "0":
						ok= true;
						break;
					default:
						ok= false;
						break;
				}
			}
			if(!ok)
				message= RM.GetString(RM.Error);						
 
			return ok;
		}

		#endregion

		#region Validate General 

//		public static bool ValidateNull(Control[] ctls)
//		{
//			string errorMsg="Error Validation required";
//
//			foreach(Control c in ctls)
//			{ 
//				if (c is ICtlTextBox )
//				{
//					if(((ICtlTextBox)c).Required && ((ICtlTextBox)c).Text ==String.Empty )
//					{
//						errorMsg= ((ICtlTextBox)c).ErrorMessage =="" ? errorMsg :((ICtlTextBox)c).ErrorMessage; 
//						MsgBox.ShowError (errorMsg);
//						return false;
//					}
//				}
//			}
//			return true;
//		}


		public static  bool ValidateText(string Text,bool Required,ref string message)
		{
			if (Required && Text.Length ==0)
			{
				message= RM.GetString(RM.GetString(RM.RequiredField));						
				return false;
			}
			return true;
		}

		public static bool ValidateMail(string Text,ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidEmail);						
				return false;
			}
			else
			{
				bool ok= Regx.IsEmail(Text); 
		        if(!ok)
				message= RM.GetString(RM.InvalidEmail);						
				return ok;
			}
		}

		public static bool ValidateDiretory(string Text,ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidDirectory );						
				return false;
			}
			else
			{
				bool ok= Directory.Exists(Text);
				if(!ok)
					message= RM.GetString(RM.InvalidDirectory );						
				return ok;
			}
		}

		public static bool ValidateFileName(string Text,ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.FileNotFound_v, Text );						
				return false;
			}
			else
			{
				bool ok= File.Exists(Text);
				if(!ok)
					message= RM.GetString(RM.FileNotFound_v,Text );						
				return ok;
			}
		}

		public static bool ValidatePhone(string Text,ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidTelephoneFormat );						
				return false;
			}
			else
			{
				Regex regularExpr = new Regex(@"\s{1}\d{3}-\d{4}|\d{4}-\d{4}|\d{3}-\d{4}");
				bool ok= regularExpr.IsMatch(Text);
				if(!ok)
					message= RM.GetString(RM.InvalidTelephoneFormat );						
				return ok;
			}
		}

		public static bool ValidatePhoneWithArea(string Text,ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidTelephoneFormat );						
				return false;
			}
			else
			{
				Regex regularExpr = new Regex(@"[(]\d{3}[)]\d{4}-\d{4}|[(]\d{3}[)]\s{1}\d{3}-\d{4}|[(]\d{3}[)]\d{3}-\d{4}");
				bool ok= regularExpr.IsMatch(Text);
				if(!ok)
					message= RM.GetString(RM.InvalidTelephoneFormat );						
				return ok;
			}
		}

		public static bool ValidateRegex(string Text,string Regx,ref string message)
		{
			if (Text.Length ==0 || Regx.Length ==0)
			{
				message= RM.GetString(RM.InvalidRegx);						
				return false;
			}
			else
			{
				Regex regularExpr = new Regex(Regx);
				bool ok= regularExpr.IsMatch(Text);
				if(!ok)
					message= RM.GetString(RM.InvalidRegx);						
				return ok;
			}
		}

		public static bool ValidateIp(string Text,ref string message)
		{
			if (Text.Length ==0)
			{
				message= RM.GetString(RM.InvalidIp);						
				return false;
			}
			else
			{
				bool ok= Regx.IsIP (Text); 
				if(!ok)
					message= RM.GetString(RM.InvalidIp);						
				return ok;
			}
		}
		#endregion
	}

}
