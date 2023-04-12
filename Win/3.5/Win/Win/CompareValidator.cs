using System;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;


namespace MControl.Win
{

	public abstract class CompareValidator //: BaseValidator
	{

		private BaseDataType type;

		protected CompareValidator()
		{
		}
 
		public static bool CanConvert(string text, BaseDataType type)
		{
			object obj1 = null;
			return CompareValidator.Convert(text, type, out obj1);
		}

		public static bool Compare(string leftText, string rightText, ValidationOperator op, BaseDataType type)
		{
			object obj1;
			int num1;
			if (!CompareValidator.Convert(leftText, type, out obj1))
			{
				return false;
			}
			if (op != ValidationOperator.DataTypeCheck)
			{
				object obj2;
				if (!CompareValidator.Convert(rightText, type, out obj2))
				{
					return true;
				}
				switch (type)
				{
					case BaseDataType.String:
						num1 = string.Compare((string) obj1, (string) obj2, false, CultureInfo.CurrentCulture);
						goto Label_00B2;

					case BaseDataType.Integer:
						num1 = ((int) obj1).CompareTo(obj2);
						goto Label_00B2;

					case BaseDataType.Double:
						num1 = ((double) obj1).CompareTo(obj2);
						goto Label_00B2;

					case BaseDataType.Date:
						num1 = ((DateTime) obj1).CompareTo(obj2);
						goto Label_00B2;

					case BaseDataType.Currency:
						num1 = ((decimal) obj1).CompareTo(obj2);
						goto Label_00B2;
				}
			}
			return true;
			Label_00B2:
				switch (op)
				{
					case ValidationOperator.Equal:
						return (num1 == 0);

					case ValidationOperator.NotEqual:
						return (num1 != 0);

					case ValidationOperator.GreaterThan:
						return (num1 > 0);

					case ValidationOperator.GreaterThanEqual:
						return (num1 >= 0);

					case ValidationOperator.LessThan:
						return (num1 < 0);

					case ValidationOperator.LessThanEqual:
						return (num1 <= 0);
				}
			return true;
		}

 
		public static bool Convert(string text, BaseDataType type, out object value)
		{
			value = null;
			try
			{
				int num1;
				int num2;
				int num3;
				NumberFormatInfo info1;
				switch (type)
				{
					case BaseDataType.String:
						value = text;
						goto Label_04E7;

					case BaseDataType.Integer:
						value = int.Parse(text, CultureInfo.InvariantCulture);
						goto Label_04E7;

					case BaseDataType.Double:
					{
						string text1 = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
						string text2 = @"^\s*([-\+])?(\d+)?(\" + text1 + @"(\d+))?\s*$";
						Match match1 = Regex.Match(text, text2);
						if (match1.Success)
						{
							string text3 = match1.Groups[1].Value + (match1.Groups[2].Success ? match1.Groups[2].Value : "0") + (match1.Groups[4].Success ? ("." + match1.Groups[4].Value) : string.Empty);
							value = double.Parse(text3, CultureInfo.InvariantCulture);
						}
						goto Label_04E7;
					}
					case BaseDataType.Date:
						if (DateTimeFormatInfo.CurrentInfo.Calendar.GetType() == typeof(GregorianCalendar))
						{
							break;
						}
						value = DateTime.Parse(text);
						goto Label_04E7;

					case BaseDataType.Currency:
						goto Label_034C;

					default:
						goto Label_04E7;
				}
				string text4 = CompareValidator.GetDateElementOrder();
				string text5 = @"^\s*((\d{4})|(\d{2}))([-./])(\d{1,2})\4(\d{1,2})\s*$";
				Match match2 = Regex.Match(text, text5);
				if (match2.Success && (match2.Groups[2].Success || (text4 == "ymd")))
				{
					num1 = int.Parse(match2.Groups[6].Value, CultureInfo.InvariantCulture);
					num2 = int.Parse(match2.Groups[5].Value, CultureInfo.InvariantCulture);
					if (match2.Groups[2].Success)
					{
						num3 = int.Parse(match2.Groups[2].Value, CultureInfo.InvariantCulture);
					}
					else
					{
						num3 = CompareValidator.GetFullYear(int.Parse(match2.Groups[3].Value, CultureInfo.InvariantCulture));
					}
				}
				else
				{
					if (text4 == "ymd")
					{
						goto Label_04E7;
					}
					string text6 = @"^\s*(\d{1,2})([-./])(\d{1,2})\2((\d{4})|(\d{2}))\s*$";
					match2 = Regex.Match(text, text6);
					if (!match2.Success)
					{
						goto Label_04E7;
					}
					if (text4 == "mdy")
					{
						num1 = int.Parse(match2.Groups[3].Value, CultureInfo.InvariantCulture);
						num2 = int.Parse(match2.Groups[1].Value, CultureInfo.InvariantCulture);
					}
					else
					{
						num1 = int.Parse(match2.Groups[1].Value, CultureInfo.InvariantCulture);
						num2 = int.Parse(match2.Groups[3].Value, CultureInfo.InvariantCulture);
					}
					if (match2.Groups[5].Success)
					{
						num3 = int.Parse(match2.Groups[5].Value, CultureInfo.InvariantCulture);
					}
					else
					{
						num3 = CompareValidator.GetFullYear(int.Parse(match2.Groups[6].Value, CultureInfo.InvariantCulture));
					}
				}
				DateTime time1 = new DateTime(num3, num2, num1);
				if (time1 != DateTime.MinValue)
				{
					value = time1;
				}
				goto Label_04E7;
			Label_034C:
				info1 = NumberFormatInfo.CurrentInfo;
				string text7 = info1.CurrencyDecimalSeparator;
				string text8 = info1.CurrencyGroupSeparator;
				if (text8[0] == '\x00a0')
				{
					text8 = " ";
				}
				int num4 = info1.CurrencyDecimalDigits;
				string text9 = @"^\s*([-\+])?(((\d+)\" + text8 + @")*)(\d+)" + ((num4 > 0) ? (@"(\" + text7 + @"(\d{1," + num4.ToString(NumberFormatInfo.InvariantInfo) + "}))") : string.Empty) + @"?\s*$";
				Match match3 = Regex.Match(text, text9);
				if (match3.Success)
				{
					StringBuilder builder1 = new StringBuilder();
					builder1.Append(match3.Groups[1]);
					foreach (Capture capture1 in match3.Groups[4].Captures)
					{
						builder1.Append(capture1);
					}
					builder1.Append(match3.Groups[5]);
					if (num4 > 0)
					{
						builder1.Append(".");
						builder1.Append(match3.Groups[7]);
					}
					value = decimal.Parse(builder1.ToString(), CultureInfo.InvariantCulture);
				}
			}
			catch
			{
				value = null;
			}
			Label_04E7:
				return (value != null);
		}

		protected  bool DetermineRenderUplevel()
		{
			if ((this.Type == BaseDataType.Date) && (DateTimeFormatInfo.CurrentInfo.Calendar.GetType() != typeof(GregorianCalendar)))
			{
				return false;
			}
			return true;//base.DetermineRenderUplevel();
		}

		protected static string GetDateElementOrder()
		{
			string text1 = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
			if (text1.IndexOf('y') < text1.IndexOf('M'))
			{
				return "ymd";
			}
			if (text1.IndexOf('M') < text1.IndexOf('d'))
			{
				return "mdy";
			}
			return "dmy";
		}

 
		protected static int GetFullYear(int shortYear)
		{
			int num1 = DateTime.Today.Year;
			int num2 = num1 - (num1 % 100);
			if (shortYear < CompareValidator.CutoffYear)
			{
				return (shortYear + num2);
			}
			return ((shortYear + num2) - 100);
		}

 
		protected static int CutoffYear
		{
			get
			{
				return DateTimeFormatInfo.CurrentInfo.Calendar.TwoDigitYearMax;
			}
		}
 
		[Category("Behavior"), DefaultValue(0), Description("RangeValidator_Type")]
		public BaseDataType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				if ((value < BaseDataType.String) || (value > BaseDataType.Currency))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.type=value;
			}
		}
 

	}

}
