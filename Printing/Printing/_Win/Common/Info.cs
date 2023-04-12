using System;
using System.Globalization;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using MControl.Win;

namespace MControl//.Util
{



	#region Info

	public sealed class Info
	{

		private static System.Globalization.CultureInfo GetCulture(System.Globalization.CultureInfo cultureInfo)
		{
			if (cultureInfo==null)
				return System.Globalization.CultureInfo.CurrentCulture;
			else
				return cultureInfo;
		}

		public static CultureInfo GetCultureInfo()
		{
			return Thread.CurrentThread.CurrentCulture;
		}

		public static DateTimeFormatInfo GetDateTimeFormatInfo()
		{
			return Thread.CurrentThread.CurrentCulture.DateTimeFormat;
		}

		internal static Encoding GetFileIOEncoding()
		{
			return Encoding.Default;
		}

		internal static int GetLocaleCodePage()
		{
			return Thread.CurrentThread.CurrentCulture.TextInfo.ANSICodePage;
		}

        public static bool IsGuid(string candidate, out Guid output)
        {
            Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

            bool isValid = false;

            output = Guid.Empty;

            if (candidate != null)
            {
                if (isGuid.IsMatch(candidate))
                {
                    output = new Guid(candidate);
                    isValid = true;
                }
            }
            return isValid;
        }

        public static bool IsGuid(string guid)
        {
            Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

            bool isValid = false;

            if (guid != null)
            {
                if (isGuid.IsMatch(guid))
                {
                    isValid = true;
                }
            }
            return isValid;
        }


        /// <summary>
        /// IsValidTime
        /// </summary>
        /// <param name="time">12:30</param>
        /// <returns></returns>
        public static bool IsValidTime(string time)
        {
            if (string.IsNullOrEmpty(time))
                return false;
            string[] args = time.Split(':');
            if (args == null || args.Length != 2)
                return false;
            int h = MControl.Types.ToInt(args[0], -1);
            int m = MControl.Types.ToInt(args[1], -1);
            if (h < 0 || m < 0)
                return false;
            return (h >= 0 && h <= 23 && m >= 0 && m <= 59);

        }
        public static bool IsValidTime(string time, ref int[] value)
        {
            if (string.IsNullOrEmpty(time))
                return false;
            string[] args = time.Split(':');
            if (args == null || args.Length != 2)
                return false;
            int h = MControl.Types.ToInt(args[0], -1);
            int m = MControl.Types.ToInt(args[1], -1);
            if (h < 0 || m < 0)
                return false;
            if (h >= 0 && h <= 23 && m >= 0 && m <= 59)
            {
                value = new int[] { h, m };
                return true;
            }
            return false;

        }
        /// <summary>
        /// IsValidMonthDay
        /// </summary>
        /// <param name="date">22/11</param>
        /// <returns></returns>
        public static bool IsValidMonthDay(string date, ref int[] value)
        {
            if (string.IsNullOrEmpty(date))
                return false;
            string[] args = date.Split('/');
            if (args == null || args.Length != 2)
                return false;
            int d = MControl.Types.ToInt(args[0], -1);
            int m = MControl.Types.ToInt(args[1], -1);
            if (m < 0 || d < 0)
                return false;
            if (m >= 0 && m <= 12 && d >= 0 && d <= 31)
            {
                value = new int[] { d, m };
                return true;
            }
            return false;
        }

		public static bool IsDateTime(object Expression)
		{
			if (Expression != null)
			{
				if (Expression is DateTime)
				{
					return true;
				}
				if (Expression is string)
				{
					try
					{
						DateTime time1 = Types.DateFromString((string) Expression);
						return true;
					}
					catch (Exception)
					{
					}
				}
			}
			return false;
		}
        public static bool IsBool(object Expression)
        {
            if (Expression != null)
            {
                if (Expression is bool)
                {
                    return true;
                }
                if (Expression is string)
                {
                    string val=(string)Expression;
                    if(!(val.ToLower().Contains("false") || val.ToLower().Contains("true")))
                    {
                        return false;
                    }
                    try
                    {
                        bool res = bool.Parse((string)Expression);
                        return true;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return false;
        }
		public static bool IsDBNull(object Expression)
		{
			if ((Expression != null) && (Expression == DBNull.Value))
			{
				return true;
			}
			return false;
		}

        public static Boolean IsNumeric(object input)
        {
            if(input==null)
                return false;
            Double temp;
            return Double.TryParse(input.ToString(), out temp);
        }
        public static Boolean IsNumeric(String input)
        {
            Double temp;
            return Double.TryParse(input, out temp);
        }

        public static Boolean IsNumeric(String input, NumberStyles numberStyle)
        {
            Double temp;
            return Double.TryParse(input, numberStyle, CultureInfo.CurrentCulture, out temp);
        }


#if(false)
		public static bool IsNumeric(object Expression)
		{
			IConvertible convertible1 = null;
			double num1=0;
			if (Expression is IConvertible)
			{
				convertible1 = (IConvertible) Expression;
			}
			if (convertible1 == null)
			{
				if (Expression is char[])
				{
					Expression = new string((char[]) Expression);
				}
				else
				{
					return false;
				}
			}
			TypeCode code1 = convertible1.GetTypeCode();
			if ((code1 != TypeCode.String) && (code1 != TypeCode.Char))
			{
				return Info.IsNumericTypeCode(code1);
			}
			string text1 = convertible1.ToString(null);
			try
			{
				long num2=0;
				if (Strings.IsHexOrOctValue(text1, ref num2))
				{
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
			return Types.TryParse(text1, ref num1);
		}
#endif
		internal static bool IsNumericTypeCode(TypeCode TypCode)
		{
			switch (TypCode)
			{
				case TypeCode.Boolean:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
			}
			return false;
		}

		public static bool CanConvertTo(string  s,BaseDataType dataType)
		{
			return CompareValidator.CanConvert(s,dataType);
		}

		public static bool IsDate(string s)
		{
			return CompareValidator.CanConvert(s,BaseDataType.Date);
		}

		public static bool IsDouble(string s)
		{
			return CompareValidator.CanConvert(s,BaseDataType.Double);
		}

		public static bool IsInteger(string s)
		{
			return CompareValidator.CanConvert(s,BaseDataType.Integer);
		}

		public static bool IsCurrency(string s)
		{
			return CompareValidator.CanConvert(s,BaseDataType.Currency);
		}

		public static bool IsString(string s)
		{
			return CompareValidator.CanConvert(s,BaseDataType.String);
		}

		public static bool IsNumber(string s)
		{
			try
			{
				double result = 0;
				return Double.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out result);
			}
			catch
			{
				return false;
			}
		}
        public static bool IsNumber(string s,System.Globalization.NumberStyles style)
        {
            try
            {
                double result = 0;
                return Double.TryParse(s, style, System.Globalization.NumberFormatInfo.CurrentInfo, out result);
            }
            catch
            {
                return false;
            }
        }

		public static bool IsNumber(object obj)
		{
            if (obj == null)
                return false;
            return IsNumber(obj.ToString());
		}

		public static bool IsValidType(Formats format, string Txt)
		{
			bool IsOK;
			
			switch(format )
			{
				case Formats.FixNumber:
				case Formats.GeneralNumber:
				case Formats.Money:
				case Formats.Percent:
				case Formats.StandadNumber:
				case Formats.ShortDate :
					IsOK= IsDate(Txt);
					break;
				case Formats.LongDate :
                    IsOK = WinRegx.IsDateType(Txt);
					break;
				case Formats.GeneralDate  :
					IsOK= Regx.IsDateTime(Txt,true);
					break;
				case Formats.ShortTime :
				case Formats.LongTime :
					IsOK= Regx.IsTime(Txt) ;
					break;
				default:
					IsOK= true;
					break;
			} 
			return IsOK;
		}

	}


	#endregion

	
}
