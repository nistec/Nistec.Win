using System;
using System.Globalization;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using Nistec.Win;

namespace Nistec//.Util
{
    
	#region Info

	internal sealed class Info
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
            int h = Nistec.Types.ToInt(args[0], -1);
            int m = Nistec.Types.ToInt(args[1], -1);
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
            int h = Nistec.Types.ToInt(args[0], -1);
            int m = Nistec.Types.ToInt(args[1], -1);
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
            int d = Nistec.Types.ToInt(args[0], -1);
            int m = Nistec.Types.ToInt(args[1], -1);
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
					IsOK= WinRegx.IsDateTime(Txt,true);
					break;
				case Formats.ShortTime :
				case Formats.LongTime :
                    IsOK = WinRegx.IsTime(Txt);
					break;
				default:
					IsOK= true;
					break;
			} 
			return IsOK;
		}

	}


	#endregion
    
	#region Strings

    internal sealed class Strings
	{
		internal static readonly CompareInfo m_InvariantCompareInfo;

		static Strings()
		{
//			Strings.CurrencyPositiveFormatStrings = new string[] { "$n", "n$", "$ n", "n $" };
//			Strings.CurrencyNegativeFormatStrings = new string[] { "($n)", "-$n", "$-n", "$n-", "(n$)", "-n$", "n-$", "n$-", "-n $", "-$ n", "n $-", "$ n-", "$- n", "n- $", "($ n)", "(n $)" };
//			Strings.NumberNegativeFormatStrings = new string[] { "(n)", "-n", "- n", "n-", "n -" };
//			Strings.m_SyncObject = new object();
			Strings.m_InvariantCompareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		public static string StrReverse(string Expression)
		{
			if (Expression == null)
			{
				return "";
			}
			int num1 = Expression.Length;
			if (num1 == 0)
			{
				return "";
			}
			int num3 = num1 - 1;
			for (int num2 = 0; num2 <= num3; num2++)
			{
				char ch1 = Expression[num2];
				switch (char.GetUnicodeCategory(ch1))
				{
					case UnicodeCategory.Surrogate:
					case UnicodeCategory.NonSpacingMark:
					case UnicodeCategory.SpacingCombiningMark:
					case UnicodeCategory.EnclosingMark:
						return Strings.InternalStrReverse(Expression, num2, num1);
				}
			}
			char[] chArray1 = Expression.ToCharArray();
			Array.Reverse(chArray1);
			return new string(chArray1);
		}

		private static string InternalStrReverse(string Expression, int SrcIndex, int Length)
		{
			StringBuilder builder1 = new StringBuilder(Length);
			builder1.Length = Length;
			TextElementEnumerator enumerator1 = StringInfo.GetTextElementEnumerator(Expression, SrcIndex);
			if (!enumerator1.MoveNext())
			{
				return "";
			}
			int num2 = 0;
			int num1 = Length - 1;
			while (num2 < SrcIndex)
			{
				builder1[num1] = Expression[num2];
				num1--;
				num2++;
			}
			int num3 = enumerator1.ElementIndex;
			while (num1 >= 0)
			{
				SrcIndex = num3;
				if (enumerator1.MoveNext())
				{
					num3 = enumerator1.ElementIndex;
				}
				else
				{
					num3 = Length;
				}
				for (num2 = num3 - 1; num2 >= SrcIndex; num2--)
				{
					builder1[num1] = Expression[num2];
					num1--;
				}
			}
			return builder1.ToString();
		}

		public static int Asc(char String)
		{
			int num1;
			int num2 = Convert.ToInt32(String);
			if (num2 < 0x80)
			{
				return num2;
			}
			try
			{
				byte[] buffer1;
				int num3;
				Encoding encoding1 = Info.GetFileIOEncoding();
				char[] chArray1 = new char[] { String };
				if (encoding1.GetMaxByteCount(1) == 1)
				{
					buffer1 = new byte[1];
					num3 = encoding1.GetBytes(chArray1, 0, 1, buffer1, 0);
					return buffer1[0];
				}
				buffer1 = new byte[2];
				num3 = encoding1.GetBytes(chArray1, 0, 1, buffer1, 0);
				if (num3 == 1)
				{
					return buffer1[0];
				}
				if (BitConverter.IsLittleEndian)
				{
					byte num4 = buffer1[0];
					buffer1[0] = buffer1[1];
					buffer1[1] = num4;
				}
				num1 = BitConverter.ToInt16(buffer1, 0);
			}
			catch (Exception exception1)
			{
				throw exception1;
			}
			return num1;
		}

		public static int Asc(string String)
		{
			if ((String == null) || (String.Length == 0))
			{
				throw new ArgumentException("Argument_Length Zero", "String");
			}
			char ch1 = String[0];
			return Strings.Asc(ch1);
		}

		public static int AscW(string String)
		{
			if ((String == null) || (String.Length == 0))
			{
				throw new ArgumentException("Argument_Length Zero", "String");
			}
			return String[0];
		}

		public static int AscW(char String)
		{
			return String;
		}
 
		public static char Chr(int CharCode)
		{
			char ch1;
			if ((CharCode < -32768) || (CharCode > 0xffff))
			{
				throw new ArgumentException("Argument_Range Two Bytes ", "CharCode");
			}
			if ((CharCode >= 0) && (CharCode <= 0x7f))
			{
				return Convert.ToChar(CharCode);
			}
			try
			{
				int num1;
				Encoding encoding1 = Encoding.GetEncoding(Info.GetLocaleCodePage());
				if ((encoding1.GetMaxByteCount(1) == 1) && ((CharCode < 0) || (CharCode > 0xff)))
				{
					throw new Exception("Error Encoding");
				}
				char[] chArray1 = new char[2];
				byte[] buffer1 = new byte[2];
				Decoder decoder1 = encoding1.GetDecoder();
				if ((CharCode >= 0) && (CharCode <= 0xff))
				{
					buffer1[0] = (byte) (CharCode & 0xff);
					num1 = decoder1.GetChars(buffer1, 0, 1, chArray1, 0);
				}
				else
				{
					buffer1[0] = (byte) ((CharCode & 0xff00) / 0x100);
					buffer1[1] = (byte) (CharCode & 0xff);
					num1 = decoder1.GetChars(buffer1, 0, 2, chArray1, 0);
				}
				ch1 = chArray1[0];
			}
			catch (Exception exception1)
			{
				throw exception1;
			}
			return ch1;
		}

		public static char ChrW(int CharCode)
		{
			if ((CharCode < -32768) || (CharCode > 0xffff))
			{
				throw new ArgumentException("Argument_Range Two Bytes", "CharCode");
			}
			return Convert.ToChar((int) (CharCode & 0xffff));
		}

		public static int StrCmp(string sLeft, string sRight, bool TextCompare)
		{
			if (sLeft == null)
			{
				sLeft = "";
			}
			if (sRight == null)
			{
				sRight = "";
			}
			if (TextCompare)
			{
				return Info.GetCultureInfo().CompareInfo.Compare(sLeft, sRight, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase);
			}
			return string.CompareOrdinal(sLeft, sRight);
		}
#if(false)
		internal static bool IsHexOrOctValue(string Value, ref long i64Value)
		{
			int num1=0;
			int num2 = Value.Length;
			while (num1 < num2)
			{
				char ch1 = Value[num1];
				if (ch1 == '&')
				{
					ch1 = char.ToLower(Value[num1 + 1], CultureInfo.InvariantCulture);
					string text1 = Strings.ToHalfwidthNumbers(Value.Substring(num1 + 2));
					switch (ch1)
					{
						case 'h':
							i64Value = Convert.ToInt64(text1, 0x10);
							goto Label_007C;

						case 'o':
							i64Value = Convert.ToInt64(text1, 8);
							goto Label_007C;
					}
					throw new FormatException();
				}
				if ((ch1 != ' ') && (ch1 != '\u3000'))
				{
					return false;
				}
				num1++;
			}
			return false;
			Label_007C:
				return true;
		}

		internal static string ToHalfwidthNumbers(string s)
		{
			return Strings.ToHalfwidthNumbers(s, Thread.CurrentThread.CurrentCulture);
		}
 
		internal static string ToHalfwidthNumbers(string s, CultureInfo culture)
		{
			int num2 = culture.LCID & 0x3ff;
			if (((num2 != 4) && (num2 != 0x11)) && (num2 != 0x12))
			{
				return s;
			}
			return Strings.LCMapString(culture, 0x400000, s);
		}

		internal static string LCMapString(CultureInfo loc, int dwMapFlags, string sSrc)
		{
			int num3;
			int num4;
			if (sSrc == null)
			{
				num4 = 0;
			}
			else
			{
				num4 = sSrc.Length;
			}
			if (num4 == 0)
			{
				return "";
			}
			int num2 = loc.LCID;
			Encoding encoding1 = Encoding.GetEncoding(loc.TextInfo.ANSICodePage);
			if (encoding1.GetMaxByteCount(1) > 1)
			{
				string text3 = sSrc;
				byte[] buffer2 = encoding1.GetBytes(text3);
				num3 = Win32.WinAPI.LCMapStringA(num2, dwMapFlags, buffer2, buffer2.Length, null, 0);
				byte[] buffer1 = new byte[(num3 - 1) + 1];
				num3 = Win32.WinAPI.LCMapStringA(num2, dwMapFlags, buffer2, buffer2.Length, buffer1, num3);
				return encoding1.GetString(buffer1);
			}
			string text1 = new string(' ', num4);
			num3 = Win32.WinAPI.LCMapString(num2, dwMapFlags, ref sSrc, num4, ref text1, num4);
			return text1;
		}

		public static string[] Split(string Expression, string Delimiter /* = " " */, int Limit /* = -1 */, CompareMethod Compare)
		{
			string[] textArray1;
			try
			{
				int num2;
				if ((Expression == null) || (Expression.Length == 0))
				{
					return new string[] { "" };
				}
				if (Limit == -1)
				{
					Limit = Expression.Length + 1;
				}
				if (Delimiter == null)
				{
					num2 = 0;
				}
				else
				{
					num2 = Delimiter.Length;
				}
				if (num2 == 0)
				{
					return new string[] { Expression };
				}
				textArray1 = Strings.SplitHelper(Expression, Delimiter, Limit, (int) Compare);
			}
			catch (Exception exception1)
			{
				throw exception1;
			}
			return textArray1;
		}

		private static string[] SplitHelper(string sSrc, string sFind, int cMaxSubStrings, int Compare)
		{
			CompareInfo info1;
			int num2=0;
			CompareOptions options1=0;
			int num3=0;
			int num5=0;
			int num6=0;
			if (sFind == null)
			{
				num3 = 0;
			}
			else
			{
				num3 = sFind.Length;
			}
			if (sSrc == null)
			{
				num6 = 0;
			}
			else
			{
				num6 = sSrc.Length;
			}
			if (num3 == 0)
			{
				return new string[] { sSrc };
			}
			if (num6 == 0)
			{
				return new string[] { sSrc };
			}
			int num1 = 20;
			if (num1 > cMaxSubStrings)
			{
				num1 = cMaxSubStrings;
			}
			string[] textArray1 = new string[num1 + 1];
			if (Compare == 0)
			{
				options1 = CompareOptions.Ordinal;
				info1 = Strings.m_InvariantCompareInfo;
			}
			else
			{
				info1 = Info.GetCultureInfo().CompareInfo;
				options1 = CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase;
			}
			while (num5 < num6)
			{
				string text1;
				int num4 = info1.IndexOf(sSrc, sFind, num5, num6 - num5, options1);
				if ((num4 == -1) || ((num2 + 1) == cMaxSubStrings))
				{
					text1 = sSrc.Substring(num5);
					if (text1 == null)
					{
						text1 = "";
					}
					textArray1[num2] = text1;
					break;
				}
				text1 = sSrc.Substring(num5, num4 - num5);
				if (text1 == null)
				{
					text1 = "";
				}
				textArray1[num2] = text1;
				num5 = num4 + num3;
				num2++;
				if (num2 > num1)
				{
					num1 += 20;
					if (num1 > cMaxSubStrings)
					{
						num1 = cMaxSubStrings + 1;
					}
					textArray1 = (string[]) UtilHelper.CopyArray((Array) textArray1, new string[num1 + 1]);
				}
				textArray1[num2] = "";
				if (num2 == cMaxSubStrings)
				{
					text1 = sSrc.Substring(num5);
					if (text1 == null)
					{
						text1 = "";
					}
					textArray1[num2] = text1;
					break;
				}
			}
			if ((num2 + 1) == textArray1.Length)
			{
				return textArray1;
			}
			return (string[]) UtilHelper.CopyArray((Array) textArray1, new string[num2 + 1]);
		}
#endif

        /// <summary>
        /// Split string to segements by maxLength per segement
        /// </summary>
        /// <param name="s"></param>
        /// <param name="limt"></param>
        /// <param name="maxLengthPerSigment"></param>
        /// <returns></returns>
        public static string[] SplitString(string s, int limt, int maxLengthPerSigment)
        {
            return SplitString(s, limt, maxLengthPerSigment, null);
        }

        /// <summary>
        /// Split string to segements by sperator and maxLength per segement
        /// </summary>
        /// <param name="s"></param>
        /// <param name="limt"></param>
        /// <param name="maxLengthPerSigment"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] SplitString(string s, int limt, int maxLengthPerSigment, string separator)
        {
            string source = s;//"1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890";// 1234567890 1234567890 1234567890 ";
            //int sigment = 1;
            List<string> strResult = new List<string>();
            //int limt=70;
            //int maxCharPerSigment=65;
            int length = source.Length;
            if (length <= maxLengthPerSigment)
            {
                return new string[] {s};
            }

            if (length > limt)
            {
                //sigment = (int)Math.Ceiling((float)length / (float)maxCharPerSigment);
                source = source.Substring(0, limt);//
                length = source.Length;
            }

            //strResult = new string[sigment];
            int current = 0;
            int currentIndex = 0;
            int currentStartIndex = 0;
            int currentLength = 0;
            //int currentCharPerSigment = maxLengthPerSigment;

            do
            {
                currentStartIndex += maxLengthPerSigment;// currentCharPerSigment;
                if (currentStartIndex > length)
                {
                    currentStartIndex = length;
                    currentIndex = length;
                }
                else if (separator != null)
                {
                    currentIndex = source.LastIndexOf(separator, currentStartIndex);
                    if (currentIndex > -1) //currentIndex++;
                        currentStartIndex = currentIndex;
                }
                else
                {
                    currentIndex = currentStartIndex;
                }

                if (currentIndex == -1)
                {
                    currentLength = currentIndex - current;
                    strResult.Add(source.Substring(current, length - current));
                    break;
                }
                else
                {
                    currentLength = currentIndex - current;
                    strResult.Add(source.Substring(current, currentLength));
                }
                current += (currentLength);
                //currentCharPerSigment += currentIndex;
            } while (currentStartIndex < length);

            return strResult.ToArray();

        }
        //public static string[] SplitString(string s, int limt, int maxLengthPerSigment, string separator)
        //{
        //    string source = s;//"1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890";// 1234567890 1234567890 1234567890 ";
        //    //int sigment = 1;
        //    List<string> strResult=new List<string>();
        //    //int limt=70;
        //    //int maxCharPerSigment=65;
        //    int length = source.Length;

        //    //if (length > limt)
        //    //{
        //    //    sigment = (int)Math.Ceiling((float)length / (float)maxCharPerSigment);
        //    //}

        //    //strResult = new string[sigment];
        //    int current = 0;
        //    int currentIndex = 0;
        //    int currentStartIndex = 0;
        //    int currentLength = 0;
        //    int currentCharPerSigment = maxLengthPerSigment;

        //    do
        //    {
        //        currentStartIndex += currentCharPerSigment;
        //        if (currentStartIndex >= length)
        //        {
        //            currentStartIndex = length;
        //            currentIndex = length;
        //        }
        //        else if (separator != null)
        //        {
        //            currentIndex = source.LastIndexOf(separator, currentStartIndex);
        //            if (currentIndex > -1) currentIndex++;

        //        }
        //        else
        //        {
        //            currentIndex = currentStartIndex;
        //        }

        //        if (currentIndex == -1)
        //        {
        //            currentLength = currentIndex - current;
        //            strResult.Add(source.Substring(current, length - current));
        //            break;
        //        }
        //        else
        //        {
        //            currentLength = currentIndex - current;
        //            strResult.Add(source.Substring(current, currentLength));
        //        }
        //        current += currentLength;
        //        currentCharPerSigment += currentIndex;
        //    } while (currentStartIndex < length);

        //    //			for(int i=0;i<sigment;i++)
        //    //			{
        //    //				Console.WriteLine(strResult[i]);
        //    //			}
        //    return strResult.ToArray();

        //}


        public static List<string> SplitToList(string s, char spliter)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            List<string> list = new List<string>();
            string[] array = s.Split(spliter);
            foreach (string sa in array)
            {
                list.Add(sa);
            }
            return list;
        }
        public static string ArrayToString(string[] array, char spliter, bool trimEnd)
        {
            if (array == null || array.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach (string s in array)
            {
                sb.AppendFormat("{0}{1}", s, spliter);
            }
            if (trimEnd)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }

        public static string IntTimeToString(int time)
        {
            string t = time.ToString();
            t = t.Insert(t.Length - 2, ":");
            return t;
            //return string.Format("{0}:{1}", t.Substring(0, 2), t.Substring(2, 2));
        }
        public static int StringTimeToInt(string time)
        {
            string t = time.Replace(":", "").TrimStart('0');
            return Convert.ToInt32(t);
        }
        public static TimeSpan StringTimeToTimeSpan(string time)
        {
            string[] t = time.Split(':');
            return new TimeSpan(Convert.ToInt32(t[0]), Convert.ToInt32(t[1]), 0);
        }
	}


	#endregion

	#region Types

    internal static class Types //Types
	{
		//Types(){}  

        /// <summary>
        /// IsEmpty object|string|Guid or number==0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmpty(object value)
        {

            if (value == null || value == DBNull.Value)
                return true;
            Type type = value.GetType();

            if (type == typeof(string))
                return string.IsNullOrEmpty(value.ToString());
            if (type == typeof(Guid))
                return Guid.Empty == new Guid(value.ToString());
            if (type == typeof(int))
                return Types.ToInt(value) == 0;
            if (type == typeof(long))
                return Types.ToLong(value) == 0;
            if (type  == typeof(decimal))
                return Types.ToDecimal(value, 0) == 0;
            if (type == typeof(float))
                return Types.ToFloat(value, 0) == 0;
            if (type == typeof(double))
                return Types.ToDouble(value, 0) == 0;
            return false;
        }

        public static string NzOr(string a, string b)
        {
            if (string.IsNullOrEmpty(a))
                return b;
            return a;
        }

        public static string NzOr(object a, object b)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
                return b.ToString();
            if (b == null)
                return a.ToString();
            return NzOr(a.ToString(), b.ToString());
        }

		#region NZ

 
		public static object NZ(object value,object valueIfNull)
		{
			try
			{
				return (value ==null || value == DBNull.Value) ? valueIfNull:(object)value;
			}
			catch
			{
				return valueIfNull;
			}
		}

		public static int NZ(object value,int valueIfNull)
		{
			try
			{
				return (value ==null || value == DBNull.Value) ? valueIfNull:ToInt(value.ToString(), NumberStyles.Number,valueIfNull);
			}
			catch
			{
				return valueIfNull;
			}
		}

        
        public static float NZ(object value, float valueIfNull)
        {
            try
            {
                return (value == null || value == DBNull.Value) ? valueIfNull : ToFloat(value.ToString(), NumberStyles.Number, valueIfNull);
            }
            catch
            {
                return valueIfNull;
            }
        }

		public static double NZ(object value,double valueIfNull)
		{
			try
			{
                return (value == null || value == DBNull.Value) ? valueIfNull : ToDouble(value.ToString(), NumberStyles.Number, valueIfNull);
			}
			catch
			{
				return valueIfNull;
			}
		}

		public static decimal NZ(object value,decimal valueIfNull)
		{
			try
			{
				return (value ==null || value == DBNull.Value) ? valueIfNull: ToDecimal(value.ToString(), NumberStyles.Number,valueIfNull);
			}
			catch
			{
				return valueIfNull;
			}
		}
        
		
        public static DateTime NZ(object value,DateTime valueIfNull)
		{
			try
			{
				return (value ==null || value == DBNull.Value) ? valueIfNull:ToDateTime(value.ToString(),valueIfNull);
			}
			catch
			{
				return valueIfNull;
			}
		}

		public static string NZ(object value,string valueIfNull)
		{
			try
			{
				return (value ==null || value == DBNull.Value) ? valueIfNull:value.ToString();
			}
			catch
			{
				return valueIfNull;
			}
		}
		public static bool NZ(object value,bool valueIfNull)
		{
			try
			{
                return (value == null || value == DBNull.Value) ? valueIfNull : StringToBool(value.ToString(),valueIfNull);
			}
			catch
			{
				return valueIfNull;
			}
		}

		#endregion

		#region Types Number Methods

	
		public static double Fix(double Number)
		{
			if (Number >= 0)
			{
				return Math.Floor(Number);
			}
			return -Math.Floor(-Number);
		}

		public static string GetOnlyDigits(string sourceValue)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char localChar in sourceValue)
			{
				if (Char.IsDigit(localChar)) sb.Append(localChar);
			}
			return sb.ToString();
		}

		public static int GetNextDozen(int value)
		{
			int mod = 0, auxVal = value;
			do
			{
				mod = auxVal%10;
				if (mod != 0) auxVal++;
			}
			while(mod != 0);

			return auxVal;
		}

        public static Guid ToGuid(object value)
        {
            if (value == null)
                return Guid.Empty;
            try
            {
                if (WinRegx.IsGuid(value.ToString()))
                    return new Guid(value.ToString());
            }
            catch { }
            return Guid.Empty;
        }

        public static Guid ToGuid(object value, Guid defaultValue)
        {
            if (value == null)
                return defaultValue;
            try
            {
                if (WinRegx.IsGuid(value.ToString()))
                    return new Guid(value.ToString());
            }
            catch { }

            return defaultValue;
        }

        public static bool TryParseGuid(string s, out Guid value)
        {
            //ClsidFromString returns the empty guid for null strings   
            if ((s == null) || (s == ""))
            {
                value = Guid.Empty;
                return false;
            }

            int hresult = Win32.WinAPI.CLSIDFromString(s, out value);
            if (hresult >= 0)
            {
                return true;
            }
            else
            {
                value = Guid.Empty;
                return false;
            }
        }


        public static double ToDouble(object value, double defaultValue)
        {
            if (value == null)
                return defaultValue;
            //return StringToDouble(value.ToString(), NumberStyles.Number, defaultValue);
            double val;//0;
            if (Double.TryParse(value.ToString(), NumberStyles.Number, null, out val))
                return val;
            return defaultValue;
        }
        public static decimal ToDecimal(object value, decimal defaultValue)
        {
            if (value == null)
                return defaultValue;
            //return StringToDecimal(value.ToString(), NumberStyles.Number, defaultValue);
            decimal val;//0;
            if (Decimal.TryParse(value.ToString(), NumberStyles.Number, null, out val))
                return val;
            return defaultValue;
        }

        public static int ToInt(object value)
        {
            if (value == null)
                return 0;
            int val;//0;
            if (int.TryParse(value.ToString(), out val))
                return val;
            return 0;
        }
        public static int ToInt(object value, int defaultValue)
        {
            if (value == null)
                return defaultValue;
            int val;//0;
            if (int.TryParse(value.ToString(), out val))
                return val;
            return defaultValue;
        }

        public static long ToLong(object value)
        {
            if (value == null)
                return 0;
            long val;//0;
            if (long.TryParse(value.ToString(), out val))
                return val;
            return 0;
        }
        public static long ToLong(object value, long defaultValue)
        {
            if (value == null)
                return defaultValue;
            long val;//0;
            if (long.TryParse(value.ToString(), out val))
                return val;
            return defaultValue;
        }

        public static byte ToByte(object value, byte defaultValue)
        {
            if (value == null)
                return defaultValue;
            byte val;//0;
            if (byte.TryParse(value.ToString(),  out val))
                return val;
            return defaultValue;
        }

        public static float ToFloat(object value, float defaultValue)
        {
            if (value == null)
                return defaultValue;
            float val;//0;
            if (float.TryParse(value.ToString(), out val))
                return val;
            return defaultValue;
        }

        public static double ToDouble(object value, NumberStyles style, double defaultValue)
		{
			if(value==null)
				return defaultValue;
            double val;//0;
            if (Double.TryParse(value.ToString(), style, null, out val))
                return val;
            return defaultValue;
		}

        public static decimal ToDecimal(object value, NumberStyles style, decimal defaultValue)
		{
			if(value==null)
				return defaultValue;
            decimal val;//0;
            if (Decimal.TryParse(value.ToString(), style, null, out val))
                return val;
            return defaultValue;
		}
        public static int ToInt(object value, NumberStyles style, int defaultValue)
		{
			if(value==null)
				return defaultValue;
            int val;//0;
            if (int.TryParse(value.ToString(), style, null, out val))
                return val;
            return defaultValue;
    	}

		public static bool ToBool(object value,bool defaultValue)
		{
			if(value==null)
				return defaultValue;
			return StringToBool(value.ToString(),defaultValue);
		}

        public static byte ToByte(object value, NumberStyles style, byte defaultValue)
        {
            if (value == null)
                return defaultValue;
             byte val;//0;
            if (byte.TryParse(value.ToString(),style,null, out val))
                return val;
            return defaultValue;
        }

        public static float ToFloat(object value, NumberStyles style, float defaultValue)
        {
            if (value == null)
                return defaultValue;
            float val;// 0F;
            if (float.TryParse(value.ToString(), style, null, out val))
                return val;
            return defaultValue;
        }

/*
        public static float StringToFloat(string value,NumberStyles style, float defaultValue)
        {
            float val;// 0F;
            if (float.TryParse(value, style, null, out val))
                return val;
            return defaultValue;
        }

        public static double StringToDouble(string value, NumberStyles style, double defaultValue)
		{
			//if(!Info.IsDouble(value))return defaultValue;
            double val;//0;
            if (Double.TryParse(value, style, null, out val))
                return val;
            return defaultValue;
       }

        public static decimal StringToDecimal(string value, NumberStyles style, decimal defaultValue)
		{
            decimal val;//0;
            if(Decimal.TryParse(value, style, null, out val))
                return val;
            return defaultValue;

        }

        public static int StringToInt(string value, NumberStyles style, int defaultValue)
        {
            int val;//0;
            if(int.TryParse(value, style, null, out val))
                return val;
            return defaultValue;
        }

        public static long StringToLong(string value, NumberStyles style, long defaultValue)
        {
            long val;//0;
            if(long.TryParse(value, style, null, out val))
                return val;
            return defaultValue;
        }

        public static byte StringToByte(string value, NumberStyles style, byte defaultValue)
        {
            byte val;//0;
            if(byte.TryParse(value, out val))
                return val;
            return defaultValue;
        }
*/
		public static string FormatDouble(string value,string format,int decimalPlaces,double defaultValue)
		{
			string f = format.ToUpper().Equals("G") ? format : format + decimalPlaces.ToString();
			if(!Info.IsDouble(value))return defaultValue.ToString(f,CultureInfo.CurrentCulture);
	
			try
			{
				return double.Parse (value).ToString (f,CultureInfo.CurrentCulture);
			}
			catch
			{
				return defaultValue.ToString(f,CultureInfo.CurrentCulture);
			}
		}

		public static string FormatDecimal(string value,string format,int decimalPlaces,decimal defaultValue)
		{
			string f = format.ToUpper().Equals("G") ? format : format + decimalPlaces.ToString();
			if(!Info.IsDouble(value))return defaultValue.ToString(f,CultureInfo.CurrentCulture);
	
			try
			{
				return decimal.Parse (value).ToString (f,CultureInfo.CurrentCulture);
			}
			catch
			{
				return defaultValue.ToString(f,CultureInfo.CurrentCulture);
			}
		}

		public static string FormatInt(string value,string format,int defaultValue)
		{
			if(!Info.IsDouble(value))return defaultValue.ToString(format ,CultureInfo.CurrentCulture);
	
			try
			{
				return int.Parse (value).ToString (format ,CultureInfo.CurrentCulture);
			}
			catch
			{
				return defaultValue.ToString(format ,CultureInfo.CurrentCulture);
			}
		}


		private static double DecimalToDouble(IConvertible ValueInterface)
		{
			return Convert.ToDouble(ValueInterface.ToDecimal(null));
		}

#if(false)
		public static double NumberFromObject(object Value)
		{
			return NumberFromObject(Value, null);
		}

 
		public static double NumberFromObject(object Value, NumberFormatInfo NumberFormat)
		{
			if (Value != null)
			{
				IConvertible convertible1 = null;
				try
				{
					convertible1 = (IConvertible) Value;
				}
				catch (Exception)
				{
				}
				if (convertible1 != null)
				{
					switch (convertible1.GetTypeCode())
					{
						case TypeCode.Boolean:
						{
							//return (double) - (convertible1.ToBoolean(null) > false);

							bool res=convertible1.ToBoolean(null);
							if(res)
								return (double)1;
							return (double)0;
						}
						case TypeCode.Byte:
							if (Value is byte)
							{
								return (double) ((byte) Value);
							}
							return (double) convertible1.ToByte(null);

						case TypeCode.Int16:
							if (Value is short)
							{
								return (double) ((short) Value);
							}
							return (double) convertible1.ToInt16(null);

						case TypeCode.Int32:
							if (Value is int)
							{
								return (double) ((int) Value);
							}
							return (double) convertible1.ToInt32(null);

						case TypeCode.Int64:
							if (Value is long)
							{
								return (double) ((long) Value);
							}
							return (double) convertible1.ToInt64(null);

						case TypeCode.Single:
							if (Value is float)
							{
								return (double) ((float) Value);
							}
							return (double) convertible1.ToSingle(null);

						case TypeCode.Double:
							if (Value is double)
							{
								return (double) Value;
							}
							return convertible1.ToDouble(null);

						case TypeCode.Decimal:
							return DecimalToDouble(convertible1);

						case TypeCode.String:
							return NumberFromString(convertible1.ToString(null), NumberFormat);
					}
				}
				throw new InvalidCastException("InvalidCast_FromTo " + Value.ToString());
			}
			return 0;
		}

 
		public static double NumberFromString(string Value)
		{
			return NumberFromString(Value, null);
		}

 
		public static double NumberFromString(string Value, NumberFormatInfo NumberFormat)
		{
			double num1;
			if (Value == null)
			{
				return 0;
			}
			try
			{
				long num2=0;
				if (Strings.IsHexOrOctValue(Value, ref num2))
				{
					return (double) num2;
				}
				num1 = Parse(Value, NumberFormat);
			}
			catch (FormatException exception1)
			{
				throw new InvalidCastException("InvalidCast_From StringTo " + Value.ToString(), exception1);
			}
			return num1;
		}

		public static double Parse(string Value, NumberFormatInfo NumberFormat)
		{
			double num1;
			CultureInfo info1 = Info.GetCultureInfo();
			if (NumberFormat == null)
			{
				NumberFormat = info1.NumberFormat;
			}
			NumberFormatInfo info2 = GetNormalizedNumberFormat(NumberFormat);
			Value = Strings.ToHalfwidthNumbers(Value, info1);
			try
			{
				num1 = double.Parse(Value, NumberStyles.Any, info2);
			}
				//			catch when (?)
				//				  {
				//					  num1 = double.Parse(Value, NumberStyles.Any, NumberFormat);
				//				  }
			catch (Exception exception2)
			{
				throw exception2;
			}
			return num1;
		}

		internal static bool TryParse(string Value, ref double Result)
		{
			CultureInfo info1 = Info.GetCultureInfo();
			NumberFormatInfo info3 = info1.NumberFormat;
			NumberFormatInfo info2 = GetNormalizedNumberFormat(info3);
			
			Value = Strings.ToHalfwidthNumbers(Value, info1);
			if (info3 == info2)
			{
				return double.TryParse(Value, NumberStyles.Any, info2, out Result);
			}
			try
			{
				Result = double.Parse(Value, NumberStyles.Any, info2);
				return true;
			}
			catch (FormatException)
			{
				return double.TryParse(Value, NumberStyles.Any, info3, out Result);
			}
			catch (Exception)
			{
				return false;
			}
		}
#endif
		internal static NumberFormatInfo GetNormalizedNumberFormat(NumberFormatInfo InNumberFormat)
		{
			NumberFormatInfo info2;
			NumberFormatInfo info5 = InNumberFormat;
			if (((((info5.CurrencyDecimalSeparator != null) && (info5.NumberDecimalSeparator != null)) && ((info5.CurrencyGroupSeparator != null) && (info5.NumberGroupSeparator != null))) && (((info5.CurrencyDecimalSeparator.Length == 1) && (info5.NumberDecimalSeparator.Length == 1)) && ((info5.CurrencyGroupSeparator.Length == 1) && (info5.NumberGroupSeparator.Length == 1)))) && (((info5.CurrencyDecimalSeparator[0] == info5.NumberDecimalSeparator[0]) && (info5.CurrencyGroupSeparator[0] == info5.NumberGroupSeparator[0])) && (info5.CurrencyDecimalDigits == info5.NumberDecimalDigits)))
			{
				return InNumberFormat;
			}
			info5 = null;
			NumberFormatInfo info4 = InNumberFormat;
			if ((((info4.CurrencyDecimalSeparator != null) && (info4.NumberDecimalSeparator != null)) && ((info4.CurrencyDecimalSeparator.Length == info4.NumberDecimalSeparator.Length) && (info4.CurrencyGroupSeparator != null))) && ((info4.NumberGroupSeparator != null) && (info4.CurrencyGroupSeparator.Length == info4.NumberGroupSeparator.Length)))
			{
				int num3 = info4.CurrencyDecimalSeparator.Length - 1;
				int num1 = 0;
				while (num1 <= num3)
				{
					if (info4.CurrencyDecimalSeparator[num1] != info4.NumberDecimalSeparator[num1])
					{
						goto Label_019D;
					}
					num1++;
				}
				int num2 = info4.CurrencyGroupSeparator.Length - 1;
				for (num1 = 0; num1 <= num2; num1++)
				{
					if (info4.CurrencyGroupSeparator[num1] != info4.NumberGroupSeparator[num1])
					{
						goto Label_019D;
					}
				}
				return InNumberFormat;
			}
			info4 = null;
			Label_019D:
				info2 = (NumberFormatInfo) InNumberFormat.Clone();
			NumberFormatInfo info3 = info2;
			info3.CurrencyDecimalSeparator = info3.NumberDecimalSeparator;
			info3.CurrencyGroupSeparator = info3.NumberGroupSeparator;
			info3.CurrencyDecimalDigits = info3.NumberDecimalDigits;
			info3 = null;
			return info2;
		}

			#endregion

		#region Boolean

		public static bool StringToBool(string value,bool defaultValue)
		{
            bool val = defaultValue;
			try
			{
                if (value == "0")
                    return false;
                if (value == "1")
                    return true;
                if(Boolean.TryParse(value, out val))
                    return Boolean.Parse(value);
                return defaultValue;
                //return bool.Parse(value);
			}
			catch
			{
				return defaultValue;
			}
		}

        public static int BoolToInt(object value, bool defaultValue)
        {
            return BoolToInt(ToBool(value,defaultValue));
        }

        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }
		#endregion

		#region Types Percent Methods

			public static bool IsPercentString(string val, System.IFormatProvider provider )
			{
				System.Globalization.NumberFormatInfo l_Info;
				if (provider==null)
					l_Info = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
				else
					l_Info = (System.Globalization.NumberFormatInfo)provider.GetFormat(typeof(System.Globalization.NumberFormatInfo));

				if (val.IndexOf(l_Info.PercentSymbol) == -1)
					return false;
				else
					return true;
			}

			public static double StringToPercentDouble(string val,
				System.Globalization.NumberStyles style , 
				System.IFormatProvider provider,
				bool p_ConsiderAllStringAsPercent)
			{
				bool l_IsPercentString = IsPercentString(val,provider);
				if (l_IsPercentString)
				{
					return double.Parse(val.Replace("%",""),style,provider) / 100.0;
				}
				else
				{
					if (p_ConsiderAllStringAsPercent)
						return double.Parse(val,style,provider) / 100.0;
					else
						return double.Parse(val,style,provider);
				}
			}
			public static float StringToPercentFloat(string val,
				System.Globalization.NumberStyles style , 
				System.IFormatProvider provider ,
				bool p_ConsiderAllStringAsPercent)
			{
				bool l_IsPercentString = IsPercentString(val,provider);
				if (l_IsPercentString)
				{
					return float.Parse(val.Replace("%",""),style,provider) / 100;
				}
				else
				{
					if (p_ConsiderAllStringAsPercent)
						return float.Parse(val,style,provider) / 100;
					else
						return float.Parse(val,style,provider);
				}
			}
			public static decimal StringToPercentDecimal(string val,
				System.Globalization.NumberStyles style , 
				System.IFormatProvider provider,
				bool p_ConsiderAllStringAsPercent )
			{
				bool l_IsPercentString = IsPercentString(val,provider);
				if (l_IsPercentString)
				{
					return decimal.Parse(val.Replace("%",""),style,provider) / 100.0M;
				}
				else
				{
					if (p_ConsiderAllStringAsPercent)
						return decimal.Parse(val,style,provider) / 100.0M;
					else
						return decimal.Parse(val,style,provider);
				}
			}


			#endregion

		#region DateTime
		
		private static Calendar CurrentCalendar
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.Calendar;
			}
		}

		public static long DateDiff(DateInterval Interval, DateTime Date1, DateTime Date2)///* = 1 */, [Optional] FirstWeekOfYear WeekOfYear /* = 1 */)
		{
			return DateDiff(Interval, Date1, Date2,FirstDayOfWeek.System);// /* = 1 */, [Optional] FirstWeekOfYear WeekOfYear /* = 1 */)
		}

		public static long DateDiff(DateInterval Interval, DateTime Date1, DateTime Date2,FirstDayOfWeek DayOfWeek)// /* = 1 */, [Optional] FirstWeekOfYear WeekOfYear /* = 1 */)
		{
			Calendar calendar1;
			TimeSpan span1 = Date2.Subtract(Date1);
			switch (Interval)
			{
				case DateInterval.Year:
					calendar1 = CurrentCalendar;
					return (long) (calendar1.GetYear(Date2) - calendar1.GetYear(Date1));

				case DateInterval.Quarter:
					calendar1 = CurrentCalendar;
					return (long) ((((calendar1.GetYear(Date2) - calendar1.GetYear(Date1)) * 4) + ((calendar1.GetMonth(Date2) - 1) / 3)) - ((calendar1.GetMonth(Date1) - 1) / 3));

				case DateInterval.Month:
					calendar1 = CurrentCalendar;
					return (long) ((((calendar1.GetYear(Date2) - calendar1.GetYear(Date1)) * 12) + calendar1.GetMonth(Date2)) - calendar1.GetMonth(Date1));

				case DateInterval.DayOfYear:
				case DateInterval.Day:
					return (long) Math.Round(Types.Fix(span1.TotalDays));

				case DateInterval.WeekOfYear:
					Date1 = Date1.AddDays((double) (0 - GetDayOfWeek(Date1, DayOfWeek)));
					Date2 = Date2.AddDays((double) (0 - GetDayOfWeek(Date2, DayOfWeek)));
					span1 = Date2.Subtract(Date1);
					return (((long) Math.Round(Types.Fix(span1.TotalDays))) / 7);

				case DateInterval.Weekday:
					return (((long) Math.Round(Types.Fix(span1.TotalDays))) / 7);

				case DateInterval.Hour:
					return (long) Math.Round(Types.Fix(span1.TotalHours));

				case DateInterval.Minute:
					return (long) Math.Round(Types.Fix(span1.TotalMinutes));

				case DateInterval.Second:
					return (long) Math.Round(Types.Fix(span1.TotalSeconds));
			}
			throw new ArgumentException("Argument_InvalidValue1", "Interval");
		}

		public static long DateDiff(string Interval, object Date1, object Date2)//, [Optional] FirstDayOfWeek DayOfWeek /* = 1 */, [Optional] FirstWeekOfYear WeekOfYear /* = 1 */)
		{
			try
			{
                DateTime time1 = ToDateTime(Date1);
			}
			catch (Exception)
			{
				throw new InvalidCastException("Invalid Date Value ");
			}
			try
			{
                DateTime time2 = ToDateTime(Date2);
			}
			catch (Exception)
			{
				throw new InvalidCastException("Argument_InvalidDateValue " + Date2.ToString());
			}
            return DateDiff(DateIntervalFromString(Interval), ToDateTime(Date1), ToDateTime(Date2));//, DayOfWeek, WeekOfYear);
		}

		private static int GetDayOfWeek(DateTime dt, FirstDayOfWeek weekdayFirst)
		{
			if ((weekdayFirst < FirstDayOfWeek.System) || (weekdayFirst > FirstDayOfWeek.Saturday))
			{
				throw new Exception(" IllegalFuncCall ");
			}
			if (weekdayFirst == FirstDayOfWeek.System)
			{
				weekdayFirst = (FirstDayOfWeek) (Info.GetDateTimeFormatInfo().FirstDayOfWeek + (int)DayOfWeek.Monday);
			}
			return (((int) (((dt.DayOfWeek - ((DayOfWeek) ((int) weekdayFirst))) + ((int)(DayOfWeek) 8)) % ((int)(DayOfWeek.Saturday | DayOfWeek.Monday)) )) + 1);
		}


		private static DateInterval DateIntervalFromString(string Interval)
		{
			if (Interval != null)
			{
				Interval = Interval.ToLower(CultureInfo.InvariantCulture);
			}
			string text1 = Interval;
			if (Strings.StrCmp(text1, "yyyy", false) != 0)
			{
				if (Strings.StrCmp(text1, "y", false) != 0)
				{
					if (Strings.StrCmp(text1, "m", false) != 0)
					{
						if (Strings.StrCmp(text1, "d", false) != 0)
						{
							if (Strings.StrCmp(text1, "h", false) != 0)
							{
								if (Strings.StrCmp(text1, "n", false) != 0)
								{
									if (Strings.StrCmp(text1, "s", false) != 0)
									{
										if (Strings.StrCmp(text1, "ww", false) != 0)
										{
											if (Strings.StrCmp(text1, "w", false) != 0)
											{
												if (Strings.StrCmp(text1, "q", false) != 0)
												{
													throw new ArgumentException("Argument_Invalid Value", "Interval");
												}
												return DateInterval.Quarter;
											}
											return DateInterval.Weekday;
										}
										return DateInterval.WeekOfYear;
									}
									return DateInterval.Second;
								}
								return DateInterval.Minute;
							}
							return DateInterval.Hour;
						}
						return DateInterval.Day;
					}
					return DateInterval.Month;
				}
				return DateInterval.DayOfYear;
			}
			return DateInterval.Year;
		}


        //public static DateTime DateFromObject(object Value)
        //{
        //    DateTime time1=DateTime.MinValue;
        //    if (Value != null)
        //    {
        //        IConvertible convertible1 = null;
        //        try
        //        {
        //            convertible1 = (IConvertible) Value;
        //        }
        //        catch (Exception)
        //        {
        //        }
        //        if (convertible1 != null)
        //        {
        //            switch (convertible1.GetTypeCode())
        //            {
        //                case TypeCode.DateTime:
        //                    return convertible1.ToDateTime(null);

        //                case TypeCode.String:
        //                    return DateFromString(convertible1.ToString(null));
        //            }
        //        }
        //        throw new InvalidCastException("InvalidCast_FromTo "+ Value.ToString());
        //    }
        //    return time1;
        //}

		public static DateTime DateFromString(string Value)
		{
            return ToDateTime(Value, Info.GetCultureInfo());
		}

 
        //public static DateTime ParseDateLongFormat(string Value, string format)
        //{
        //    if(format.Length>1)
        //    {
        //        System.Globalization.DateTimeFormatInfo dtf=new System.Globalization.DateTimeFormatInfo();
        //        dtf.LongDatePattern=format;
        //        return ToDateTime(Value, dtf);
        //    }
        //    return ToDateTime(Value, "D");
        //}

        //public static DateTime ParseDateShortFormat(string Value, string format)
        //{
        //    if(format.Length>1)
        //    {
        //        System.Globalization.DateTimeFormatInfo dtf=new System.Globalization.DateTimeFormatInfo();
        //        dtf.ShortDatePattern=format;
        //        return ToDateTime(Value, dtf);
        //    }
        //    return ToDateTime(Value, "d");
        //}

        //public static DateTime ParseTimeShortFormat(string Value, string format)
        //{
        //    if(format.Length>1)
        //    {
        //        System.Globalization.DateTimeFormatInfo dtf=new System.Globalization.DateTimeFormatInfo();
        //        dtf.ShortTimePattern=format;
        //        return ToDateTime(Value, dtf);
        //    }
        //    return ToDateTime(Value, "t");
        //}

        //public static DateTime ParseTimeLongFormat(string Value, string format)
        //{
        //    if(format.Length>1)
        //    {
        //        System.Globalization.DateTimeFormatInfo dtf=new System.Globalization.DateTimeFormatInfo();
        //        dtf.LongTimePattern=format;
        //        return ToDateTime(Value, dtf);
        //    }
        //    return ToDateTime(Value, "T");
        //}

        //public static DateTime ToDateTime(string Value, string format)
        //{
        //    try
        //    {
        //        return DateTime.Parse(Value);
        //    }
        //    catch (Exception)
        //    {
        //        throw new InvalidCastException("InvalidCast_FromStringTo " +  Value);
        //    }
        //}

        public static DateTime ToDateTime(object Value)
        {
            return ToDateTime(Value, DateTime.Now);
        }

        public static DateTime ToDateTime(object Value, DateTime defaultValue)
        {
            if (Value == null)
                return defaultValue;
            return ToDateTime(Value.ToString(),defaultValue);

            //DateTime time1 = defaultValue;
            //if (Value != null)
            //{
            //    IConvertible convertible1 = null;
            //    try
            //    {
            //        convertible1 = (IConvertible)Value;
            //    }
            //    catch (Exception)
            //    {
            //    }
            //    if (convertible1 != null)
            //    {
            //        switch (convertible1.GetTypeCode())
            //        {
            //            case TypeCode.DateTime:
            //                return convertible1.ToDateTime(null);

            //            case TypeCode.String:
            //                return ToDateTime(convertible1.ToString(null), Info.GetCultureInfo());
            //            //return DateFromString(convertible1.ToString(null));
            //        }
            //    }
            //    //throw new InvalidCastException("InvalidCast_FromTo " + Value.ToString());
            //    return defaultValue;
            //}
            //return time1;
        }

        public static DateTime ToDateTime(string Value, DateTime defaultValue)
        {
            DateTime val;
            if (DateTime.TryParse(Value, out val))
                return val;
            return defaultValue;
        }

        public static DateTime ToDateTime(string Value,DateTimeFormatInfo dtf, DateTime defaultValue)
        {
            DateTime val;
            if (DateTime.TryParse(Value, dtf, DateTimeStyles.None, out val))
                return val;
            return defaultValue;
        }

        public static DateTime ToDateTime(string Value, CultureInfo culture, DateTime defaultValue)
        {
            DateTime val;
            if (DateTime.TryParse(Value, culture, DateTimeStyles.None, out val))
                return val;
            return defaultValue;
        }

        public static DateTime ToDateTime(string Value, DateTimeFormatInfo dtf)
		{

            return ToDateTime(Value, dtf, DateTime.Now);

            //DateTime date1;

            //try
            //{
            //    date1 = DateTime.Parse(Value, dtf);
            //}
            //catch (Exception)
            //{
            //    throw new InvalidCastException("InvalidCast_FromStringTo " + Value);
            //}
            //return date1;
		}

        public static DateTime ToDateTime(string Value, CultureInfo culture)
		{
            return ToDateTime(Value, culture, DateTime.Now);

            //DateTime date1;
            //try
            //{
            //    //time1 = DateTime.Parse(StringType.ToHalfwidthNumbers(Value), culture, DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AllowWhiteSpaces);
            //    date1 = DateTime.Parse(Value, culture, DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AllowWhiteSpaces);
            //}
            //catch (Exception)
            //{
            //    throw new InvalidCastException("InvalidCast_FromStringTo " +  Value);
            //}
            //return date1;
		}

		/// <summary>
		/// Get string date and return formated string date ,on exception return defult value
		/// </summary>
		/// <param name="s">string dateTime</param>
		/// <param name="format">format</param>
		/// <param name="defaultValue">value to return if invalid cast exception</param>
		/// <returns>String Date Time formated</returns>
		public static string  FormatDate(string s,string format, DateTime defaultValue)
		{
			
			try
			{
                return ToDateTime(s, defaultValue).ToString(format);
				//return DateTime.Parse(s).ToString(format);
			}
			catch(Exception)
			{
				return defaultValue.ToString(format);
			}
		}

		/// <summary>
		/// Get string date and return formated string date ,on exception return defult value
		/// </summary>
		/// <param name="s">string dateTime</param>
		/// <param name="format">format</param>
		/// <param name="defaultValue">string value to return if invalid cast exception</param>
		/// <returns>String Date Time formated</returns>
		public static string FormatDate(string s,string format, string defaultValue)
		{
            DateTime val;
            try
			{
                if (string.IsNullOrEmpty(s))
                    return defaultValue;
                if (DateTime.TryParse(s, out val))
                    return val.ToString(format);
                return defaultValue;

                //return ToDateTime(s).ToString(format);
			}
			catch(Exception)
			{
				return defaultValue;
			}
		}

        public static bool IsDateTime(string s)
        {
            DateTime val;
            try
            {
                if (string.IsNullOrEmpty(s) || s.Length < 10)
                    return false;
                if (DateTime.TryParse(s, out val))
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsDateTime(object o)
        {
            if (o == null)
                return false;
            if (o is DateTime)
                return true;
            return IsDateTime(o.ToString());
        }

        public static int DateTimeToInt(DateTime value)
        {
            //string s = FormatDate(value.ToString(), "yyyyMMdd", "19000101");
            //return int.Parse(s);
            return int.Parse(value.ToString("yyyyMMdd"));
        }

        public static int TodayInt()
        {
            return int.Parse(DateTime.Today.ToString("yyyyMMdd"));
        }

		public static string DateTimeFormatToString(DateTimeFormats dateTimeFormat)
		{
			string result = "d";
		
			if (dateTimeFormat == DateTimeFormats.ShortDatePattern) result = "d";
			else if	(dateTimeFormat == DateTimeFormats.LongDatePattern) result = "D";
			else if	(dateTimeFormat == DateTimeFormats.FullDateAndShortTimePattern) result = "f";
			else if	(dateTimeFormat == DateTimeFormats.FullDateAndLongTimePattern) result = "F";
			else if	(dateTimeFormat == DateTimeFormats.GeneralShortTime) result = "g";
			else if	(dateTimeFormat == DateTimeFormats.GeneralLongTime) result = "G";
			else if	(dateTimeFormat == DateTimeFormats.MonthDayPattern) result = "m";
			else if	(dateTimeFormat == DateTimeFormats.RFC1123Pattern) result = "r";
			else if	(dateTimeFormat == DateTimeFormats.SortableDateTimePattern) result = "s";
			else if	(dateTimeFormat == DateTimeFormats.ShortTimePattern) result = "t";
			else if	(dateTimeFormat == DateTimeFormats.LongTimePattern) result = "T";
			else if	(dateTimeFormat == DateTimeFormats.UniversalSortableDateTimePattern) result = "u";
			else if	(dateTimeFormat == DateTimeFormats.FullLongDateAndLongTime) result = "U";
			else if	(dateTimeFormat == DateTimeFormats.YearMonthPattern) result = "y";
		
			return result;
		}
	
		public static DateTimeFormats DateTimeFormat(string dateTimeFormat)
		{
			if (dateTimeFormat == "d")return DateTimeFormats.ShortDatePattern;
			else if	(dateTimeFormat == "D")return DateTimeFormats.LongDatePattern;
			else if	(dateTimeFormat == "f")return DateTimeFormats.FullDateAndShortTimePattern;
			else if	(dateTimeFormat == "F")return DateTimeFormats.FullDateAndLongTimePattern;
			else if	(dateTimeFormat == "g")return DateTimeFormats.GeneralShortTime;
			else if	(dateTimeFormat == "G")return DateTimeFormats.GeneralLongTime;
			else if	(dateTimeFormat == "m")return DateTimeFormats.MonthDayPattern;
			else if	(dateTimeFormat == "r")return DateTimeFormats.RFC1123Pattern;
			else if	(dateTimeFormat == "s")return DateTimeFormats.SortableDateTimePattern;
			else if	(dateTimeFormat == "t")return DateTimeFormats.ShortTimePattern;
			else if	(dateTimeFormat == "T")return DateTimeFormats.LongTimePattern;
			else if	(dateTimeFormat == "u")return DateTimeFormats.UniversalSortableDateTimePattern;
			else if	(dateTimeFormat == "U")return DateTimeFormats.FullLongDateAndLongTime;
			else if	(dateTimeFormat == "y")return DateTimeFormats.YearMonthPattern;
		
			return DateTimeFormats.ShortDatePattern;
		}
	

		public static string DateToString(DateTime value,string format)
		{
			return value.ToString(format, CultureInfo.CurrentCulture);
		}

		public static string DateToString(DateTime value)
		{
			string sep = System.Globalization.DateTimeFormatInfo.CurrentInfo.DateSeparator;
			string format = String.Empty;
		
			DateTime t = new DateTime(2000,2,1);
			string   tStr  = t.ToShortDateString();					
			if(tStr.IndexOf("1") > tStr.IndexOf("2"))
			{
				format = "MM" + sep + "dd" + sep + "yyyy";
			}
			else
			{
				format = "dd" + sep + "MM" + sep + "yyyy";
			}
		
			return value.ToString(format, CultureInfo.CurrentCulture);
		}
		
		public static DateTime DateValue(string StringDate)
		{
			DateTime time2 = DateFromString(StringDate);
			return time2.Date;
		}


 
        public static DateTime DateStart(DateTime d)
        {
            return new DateTime(d.Year,d.Month,d.Day,0,0,0);
        }
        public static DateTime DateEnd(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
        }

		#endregion

		#region Bytes

		public static string BytesToHexString(byte[] byteArray)
		{
			StringBuilder sb = new StringBuilder(40);
			foreach(byte bValue in byteArray)
			{
				sb.AppendFormat(CultureInfo.CurrentCulture, bValue.ToString("x2", CultureInfo.CurrentCulture).ToUpper(CultureInfo.CurrentCulture));
			}
			return sb.ToString();
		}

		public static byte[] BytesFromString(EncodingType encoding, string stringValue)
		{
			switch(encoding)
			{
				case EncodingType.ASCII:
					return (new ASCIIEncoding()).GetBytes(stringValue);
				case EncodingType.Unicode:
					return (new UnicodeEncoding()).GetBytes(stringValue);
				case EncodingType.UTF7:
					return (new UTF7Encoding()).GetBytes(stringValue);
				case EncodingType.UTF8:
					return (new UTF8Encoding()).GetBytes(stringValue);
				default:
					return (new UTF8Encoding()).GetBytes(stringValue);

			}
		}

		public static string BytesToString(EncodingType encoding,byte[] byteArray)
		{
			switch(encoding)
			{
				case EncodingType.ASCII:
					return (new ASCIIEncoding()).GetString(byteArray);
				case EncodingType.Unicode:
					return (new UnicodeEncoding()).GetString(byteArray);
				case EncodingType.UTF7:
					return (new UTF7Encoding()).GetString(byteArray);
				case EncodingType.UTF8:
					return (new UTF8Encoding()).GetString(byteArray);
				default:
					return (new UTF8Encoding()).GetString(byteArray);
			}
		}

		public static byte[] BytesFromString(string stringValue)
		{
			return (new UnicodeEncoding()).GetBytes(stringValue);
		}

		public static string BytesToString(byte[] byteArray)
		{
			return (new UnicodeEncoding()).GetString(byteArray);
		}
	
		#endregion

		#region Formats

        internal struct BoolRange
		{
			public object T;
			public object F;
		}

		public static string GetFormat(Formats formatType,ref RangeType range)
		{

			switch(formatType)
			{
				case Formats.None:
					return "";
				case Formats.GeneralNumber:
                    range = new RangeType(RangeType.MinNumber, RangeType.MaxNumber);
					return "G";
				case Formats.FixNumber:
					range =new  RangeType(RangeType.MinNumber ,RangeType.MaxNumber );
					return "F";
				case Formats.StandadNumber:
					range =new  RangeType(RangeType.MinNumber ,RangeType.MaxNumber );
					return "N";
				case Formats.Money:
					range =new  RangeType(RangeType.MinNumber ,RangeType.MaxNumber );
					return "C";
				case Formats.Percent:
					range =new  RangeType(RangeType.MinNumber ,RangeType.MaxNumber );
					return "P";
				case Formats.GeneralDate:
					range =new  RangeType(RangeType.MinDate,RangeType.MaxDate );						
					return "G";
				case Formats.LongDate:
					range =new  RangeType(RangeType.MinDate,RangeType.MaxDate );						
					return "D";
				case Formats.ShortDate:
					range =new  RangeType(RangeType.MinDate,RangeType.MaxDate );						
					return "d";
				case Formats.LongTime:
					range =new  RangeType(0,0);
					return "T";
				case Formats.ShortTime:
					range =new  RangeType(0,0);
					return "t";
				default:
					return "";
			}
			
		}
	
		public static string GetFormat(Formats formatType)
		{

			switch(formatType)
			{
				case Formats.None:
					return "";
				case Formats.GeneralNumber:
					return "G";
				case Formats.FixNumber:
					return "F";
				case Formats.StandadNumber:
					return "N";
				case Formats.Money:
					return "C";
				case Formats.Percent:
					return "P";
				case Formats.GeneralDate:
					return "G";
				case Formats.LongDate:
					return "D";
				case Formats.ShortDate:
					return "d";
				case Formats.LongTime:
					return "T";
				case Formats.ShortTime:
					return "t";
				default:
					return "";
			}
		}
		
		public static string GetFormat(DateFormats formatType,string custom)
		{

			switch(formatType)
			{
				case DateFormats.GeneralDate:
					return "G";
				case DateFormats.LongDate:
					return "D";
				case DateFormats.ShortDate:
					return "d";
				case DateFormats.ShortTime:
					return "t";
				case DateFormats.LongTime:
					return "T";
				case DateFormats.CustomDate:
					return custom;
				default:
					return "d";
			}
		}

		public static string GetFormat(NumberFormats formatType,string custom)
		{

			switch(formatType)
			{
				case NumberFormats.GeneralNumber:
					return "G";
				case NumberFormats.FixNumber:
					return "F";
				case NumberFormats.StandadNumber:
					return "N";
				case NumberFormats.Money:
					return "C";
				case NumberFormats.Percent:
					return "P";
				case NumberFormats.CustomNumber:
					return custom;
				default:
					return "N";
			}
		}

		public static FieldType GetBaseFormat(Formats format)
		{
			switch(format)
			{
				case Formats.None :
					return FieldType.Text;
				case Formats.FixNumber  :
				case Formats.GeneralNumber  :
				case Formats.Money  :
				case Formats.Percent  :
				case Formats.StandadNumber :
					return FieldType.Number;
				case Formats.GeneralDate  :
				case Formats.LongDate  :
				case Formats.LongTime  :
				case Formats.ShortDate :
				case Formats.ShortTime  :
					return FieldType.Date;
				default:
					return FieldType.Text;
			}
		}

		public static Formats GetFormat(DateFormats format)
		{
			switch(format)
			{
				case DateFormats.GeneralDate  :
					return Formats.GeneralDate  ;
				case DateFormats.LongDate  :
					return Formats.LongDate  ;
				case DateFormats.LongTime  :
					return Formats.LongTime  ;
				case DateFormats.ShortDate :
					return Formats.ShortDate ;
				case DateFormats.ShortTime  :
					return Formats.ShortTime  ;
				default:
					return Formats.ShortDate ;
			}
		}

		public static NumberFormats GetNumberFormat(Formats format)
		{
			switch(format)
			{
				case Formats.None :
					return NumberFormats.CustomNumber;
				case Formats.FixNumber  :
					return NumberFormats.FixNumber ;
				case Formats.GeneralNumber  :
					return NumberFormats.GeneralNumber ;
				case Formats.Money  :
					return NumberFormats.Money ;
				case Formats.Percent  :
					return NumberFormats.Percent ;
				case Formats.StandadNumber :
					return NumberFormats.StandadNumber;
				default:
					return NumberFormats.GeneralNumber;
			}
		}

		public static DateFormats GetDateFormat(Formats format)
		{
			switch(format)
			{
				case Formats.None :
					return DateFormats.CustomDate ;
				case Formats.GeneralDate  :
					return DateFormats.GeneralDate  ;
				case Formats.LongDate  :
					return DateFormats.LongDate  ;
				case Formats.LongTime  :
					return DateFormats.LongTime  ;
				case Formats.ShortDate :
					return DateFormats.ShortDate ;
				case Formats.ShortTime  :
					return DateFormats.ShortTime  ;
				default:
					return DateFormats.GeneralDate ;
			}
		}

		public static BoolRange GetBoolFormat(BoolFormats formatType)
		{
			BoolRange range=new BoolRange ();
			switch(formatType)
			{
				case BoolFormats.TrueFalse :
					range.T=RM.GetString(RM.True );
					range.F=RM.GetString(RM.False);
					break;
				case BoolFormats.YesNo :
					range.T=RM.GetString(RM.Yes);
					range.F=RM.GetString(RM.No );
					break;
				case BoolFormats.OnOff :
					range.T=RM.GetString(RM.On  );
					range.F=RM.GetString(RM.Off );
					break;
				default:
					range.T=RM.GetString(RM.True );
					range.F=RM.GetString(RM.False);
					break;
			}
			return range;

		}

		public static object[] GetBoolRange(BoolFormats formatType)
		{
			string T="";
			string F="";
			switch(formatType)
			{
				case BoolFormats.TrueFalse :
					T=RM.GetString(RM.True );
					F=RM.GetString(RM.False);
					break;
				case BoolFormats.YesNo :
					T=RM.GetString(RM.Yes);
					F=RM.GetString(RM.No );
					break;
				case BoolFormats.OnOff :
					T=RM.GetString(RM.On  );
					F=RM.GetString(RM.Off );
					break;
				default:
					T=RM.GetString(RM.True );
					F=RM.GetString(RM.False);
					break;
			}
			object[] obj=new object[]{T,F};
			return obj;

		}

		#endregion

		#region Compare

		public static int Compare(object objA , object objB )
		{
			try
			{	
				if (objA.ToString().Length == 0 && objB.ToString().Length > 0)
					return -1;
				else if (objA.ToString().Length > 0 && objB.ToString().Length == 0) 
					return 1;
				else if (objA.ToString().Length == 0 && objB.ToString().Length == 0) 
					return 0;
				else 
				{	
					int res=0;
					if( Info.IsDouble(objA.ToString ())&& Info.IsDouble(objB.ToString ()) )
						res= System.Convert.ToDecimal(objA).CompareTo (System.Convert.ToDecimal(objB));
					else if( Info.IsDate(objA.ToString ())&& Info.IsDate(objB.ToString ()))
						res= System.Convert.ToDateTime(objA).CompareTo (System.Convert.ToDateTime(objB));
					else
						res= objA.ToString().CompareTo(objB.ToString());
				
					return res;
				}    
			}
			catch(Exception ex)
			{
				throw ex; //MessageBox.Show (ex.Message.ToString ());
				//return 0;
			}
		}

		public static int Compare(FieldType mDataType,object objA , object objB )
		{
			try
			{	
				if (objA.ToString().Length == 0 && objB.ToString().Length > 0)
					return -1;
				else if (objA.ToString().Length > 0 && objB.ToString().Length == 0) 
					return 1;
				else if (objA.ToString().Length == 0 && objB.ToString().Length == 0) 
					return 0;
				else 
				{	
					int res=0;
					switch(mDataType)
					{
						case FieldType.Text :
							res= objA.ToString().CompareTo(objB.ToString());
							break;
						case FieldType.Number  :
							if(Info.IsDouble(objA.ToString())&& Info.IsDouble(objB.ToString()))
								res= System.Convert.ToDecimal(objA).CompareTo (System.Convert.ToDecimal(objB));
							break;
						case FieldType.Date  :
							if(Info.IsDate(objA.ToString())&& Info.IsDate(objB.ToString()))
								res= System.Convert.ToDateTime(objA).CompareTo (System.Convert.ToDateTime(objB));
							break;
						case FieldType.Bool  :
							res= System.Convert.ToBoolean (objA).CompareTo (System.Convert.ToBoolean (objB));
							break;
						default:
							res= objA.ToString().CompareTo(objB.ToString());
							break;
					}
					return res;
				}    
			}
			catch(Exception ex)
			{
				throw ex;//  MessageBox.Show (ex.Message.ToString ());
				//return 0;
			}
		}
		#endregion


        public static object ParseEnum(Type type, object value, object defaultValue)
        {
            try
            {
                if (value == null)
                    return defaultValue;
                if (!Enum.IsDefined(type, value))
                    return defaultValue;
                return Enum.Parse(type, value.ToString(), true);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int ParseEnum(Type type, object value, int defaultValue)
        {
            try
            {
                if (value == null)
                    return defaultValue;
                if (!Enum.IsDefined(type, value))
                    return defaultValue;
                return (int)Enum.Parse(type, value.ToString(), true);
            }
            catch
            {
                return defaultValue;
            }
        }


        public static FieldType DataTypeOf(Type type)
        {
            if (type.Equals(typeof(System.DateTime)))
            {
                return FieldType.Date;
            }
            if (type.Equals(typeof(System.Boolean)))
            {
                return FieldType.Bool;
            }
            if (((type.Equals(typeof(short)) || type.Equals(typeof(int))) || (type.Equals(typeof(long)) || type.Equals(typeof(ushort)))) || (((type.Equals(typeof(uint)) || type.Equals(typeof(ulong))) || (type.Equals(typeof(decimal)) || type.Equals(typeof(double)))) || ((type.Equals(typeof(float)) || type.Equals(typeof(byte))) || type.Equals(typeof(sbyte)))))
            {
                return FieldType.Number;
            }
            return FieldType.Text;
        }

        public static FieldType DataTypeOf(string text)
        {
            if (Info.IsNumeric(text))
                return FieldType.Number;
            if (Info.IsDateTime(text))
                return FieldType.Date;
            if (Info.IsBool(text))
                return FieldType.Bool;
            return FieldType.Text;
        }

	}
	#endregion
}
