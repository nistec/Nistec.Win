using System;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections;
using System.Reflection;


namespace MControl.Win
{

	#region Range

	[Serializable, StructLayout(LayoutKind.Sequential),TypeConverter(typeof(RangeConverter))]
	public sealed class RangeType //: IRangeNumber,IRangeDate
	{
		public const decimal MinNumber=-999999999;
		public const decimal MaxNumber=999999999;

		public static DateTime MinDate
		{
			get{return new DateTime(1900,1,1);}
		}

		public static DateTime MaxDate
		{
			get{return new DateTime(2999,12,31);}
		}

		#region User Defined Variables

		private RangeNumber mRangeNumber;
		private RangeDate mRangeDate;
		private bool IsNumber;

		public RangeType(decimal minValue ,decimal maxValue)
		{
			mRangeNumber=new RangeNumber (minValue,maxValue);
			IsNumber=true;
		}

        public RangeType(DateTime minValue, DateTime maxValue)
		{
			mRangeDate=new RangeDate (minValue,maxValue);
			IsNumber=false;
		}

		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public string MinValue
		{
			get 
			{
				if(IsNumber)
					return mRangeNumber.MinValue.ToString ();
				else
					return mRangeDate.MinValue.ToString () ;
			}
			set 
			{
				if(IsNumber)
					mRangeNumber.MinValue=System.Convert.ToDecimal  ( value);
				else
					mRangeDate.MinValue=System.Convert.ToDateTime (value);
			}
		}

		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public string MaxValue
		{
			get 
			{
				if(IsNumber)
					return mRangeNumber.MaxValue.ToString ();
				else
					return mRangeDate.MaxValue.ToString ();
			}
			set 
			{
				if(IsNumber)
					 mRangeNumber.MaxValue=System.Convert.ToDecimal  (value);
				 else
					 mRangeDate.MaxValue=System.Convert.ToDateTime (value);
			}
		}

		public RangeNumber RangeNumber()
		{
			return mRangeNumber;
		}

		public RangeDate RangeDate()
		{
			return mRangeDate;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public override string ToString()
		{
			if(IsNumber)
				return mRangeNumber.ToString ();
			else
				return mRangeDate.ToString ();
		}
		#endregion

		#region Methods
		public bool IsValid(object obj)
		{
			if(IsNumber)
			  return mRangeNumber.IsValid (obj); 
			else
		      return mRangeDate.IsValid (obj);
		}

		public bool IsValid(string s)
		{
			if(IsNumber)
				return mRangeNumber.IsValid (s); 
			else
				return mRangeDate.IsValid (s);
		}

		#endregion

	}

	#region RangeConverter
	/// <summary>
	/// Summary description for RangeConverter.
	/// </summary>
	public class RangeConverter : TypeConverter
	{
		public RangeConverter()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// allows us to display the + symbol near the property name
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="value"></param>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(Range));
		}

	}
	#endregion

	#endregion

	#region RangeDate

	[Serializable, StructLayout(LayoutKind.Sequential), TypeConverter(typeof(RangeDateConverter))]//, ComVisible(true)]
	public struct RangeDate
	{
		static RangeDate()
		{
			RangeDate.Empty = new RangeDate();
		}
		public RangeDate(RangeDate r)
		{
			this.min = r.min;
			this.max = r.max;
		}

		public RangeDate(DateTime min, DateTime max)
		{
			this.min = min;
			this.max = max;
		}

		public override bool Equals(object obj)
		{
			if (obj is RangeDate)
			{
				RangeDate range1 = (RangeDate) obj;
				if (range1.min == this.min)
				{
					return (range1.max == this.max);
				}
			}
			return false;
		}

 
		public override int GetHashCode()
		{
			return base.GetHashCode();// (this.min ^ this.max);
		}

 
		//		public static RangeDate operator +(RangeDate sz1, RangeDate sz2)
		//		{
		//			return new RangeDate(sz1.min + sz2.min, sz1.max + sz2.max);
		//		}

		public static bool operator ==(RangeDate sz1, RangeDate sz2)
		{
			if (sz1.min == sz2.min)
			{
				return (sz1.max == sz2.max);
			}
			return false;
		}
 
		//		public static explicit operator RangeDate(RangeDate range)
		//		{
		//			return new RangeDate(range.min, range.max);
		//		}
		//
		//		public static implicit operator RangeF(RangeDate r)
		//		{
		//			return new RangeF((float) r.min, (float) r.max);
		//		}

 
		public static bool operator !=(RangeDate sz1, RangeDate sz2)
		{
			return !(sz1 == sz2);
		}

 
		//		public static RangeDate operator -(RangeDate sz1, RangeDate sz2)
		//		{
		//			return new RangeDate(sz1.min - sz2.min, sz1.max - sz2.max);
		//		}

 
		public override string ToString()
		{
			return ("{min=" + this.min.ToString() + ", max=" + this.max.ToString() + "}");
		}

		//		public static RangeDate Truncate(RangeF value)
		//		{
		//			return new RangeDate((int) value.min, (int) value.max);
		//		}


		public bool IsValid(string s)
		{
			try
			{
				DateTime value=DateTime.Parse (s);
				return (value <= max && value >= min); 
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
				DateTime value=System.Convert.ToDateTime (obj);
				return (value <= max && value >= min); 
			}
			catch
			{
				return false; 
			}
		}

		public bool IsValid(DateTime value)
		{
			return (value <= max && value >= min); 
		}

		public DateTime MaxValue
		{
			get
			{
				return this.max;
			}
			set
			{
				this.max = value;
			}
		}
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				if (this.min == DateTime.MinValue)
				{
					return (this.max == DateTime.MinValue);
				}
				return false;
			}
		}
		public DateTime MinValue
		{
			get
			{
				return this.min;
			}
			set
			{
				this.min = value;
			}
		}
 
		public static readonly RangeDate Empty;
		private DateTime max;
		private DateTime min;
 


	}

    public class RangeDateConverter : TypeConverter
    {
        public RangeDateConverter()
        {
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string text1 = ((string)value).Trim();
                if (text1.Length != 0)
                {
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    char ch1 = culture.TextInfo.ListSeparator[0];
                    string[] textArray1 = text1.Split(new char[] { ch1 });
                    DateTime[] dateArray1 = new DateTime[textArray1.Length];
                    TypeConverter converter1 = TypeDescriptor.GetConverter(typeof(DateTime));
                    for (int num1 = 0; num1 < dateArray1.Length; num1++)
                    {
                        DateTime.Parse(textArray1[num1]);
                        dateArray1[num1] = (DateTime)converter1.ConvertFromString(context, culture, textArray1[num1]);
                    }
                    if (dateArray1.Length != 2)
                    {
                        throw new ArgumentException("TextParseFailedFormat");
                    }
                    try
                    {
                        DateTime min = DateTime.Parse(textArray1[0]);
                        DateTime max = DateTime.Parse(textArray1[1]);
                        return new RangeDate(dateArray1[0], dateArray1[1]);
                    }
                    catch
                    {
                        throw new ArgumentException("TextParseFailedFormat");
                    }
                }
                return null;
            }
            return base.ConvertFrom(context, culture, value);
        }


        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if ((destinationType == typeof(string)) && (value is RangeDate))
            {
                RangeDate range1 = (RangeDate)value;
                if (culture == null)
                {
                    culture = CultureInfo.CurrentCulture;
                }
                string text1 = culture.TextInfo.ListSeparator + " ";
                TypeConverter converter1 = TypeDescriptor.GetConverter(typeof(DateTime));
                string[] textArray1 = new string[2];
                int num1 = 0;
                textArray1[num1++] = converter1.ConvertToString(context, culture, range1.MinValue);
                textArray1[num1++] = converter1.ConvertToString(context, culture, range1.MaxValue);
                return string.Join(text1, textArray1);
            }
            if ((destinationType == typeof(InstanceDescriptor)) && (value is RangeDate))
            {
                RangeDate range2 = (RangeDate)value;
                ConstructorInfo info1 = typeof(RangeDate).GetConstructor(new Type[] { typeof(DateTime), typeof(DateTime) });
                if (info1 != null)
                {
                    return new InstanceDescriptor(info1, new object[] { range2.MinValue, range2.MaxValue });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }


        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return new RangeDate((DateTime)propertyValues["MinValue"], (DateTime)propertyValues["MaxValue"]);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(RangeDate), attributes).Sort(new string[] { "MinValue", "MaxValue" });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }



    }
 

	#endregion

	#region RangeNumber
	[Serializable, StructLayout(LayoutKind.Sequential), TypeConverter(typeof(RangeNumberConverter))]//, ComVisible(true)]
	public struct RangeNumber
	{
		static RangeNumber()
		{
			RangeNumber.Empty = new RangeNumber();
		}
		public RangeNumber(RangeNumber r)
		{
			this.min = r.min;
			this.max = r.max;
		}

		public RangeNumber(decimal min, decimal max)
		{
			this.min = min;
			this.max = max;
		}

		public static RangeNumber Ceiling(RangeNumber value)
		{
			return new RangeNumber((decimal) Math.Ceiling((double) value.min), (decimal) Math.Ceiling((double) value.max));
		}

		public override bool Equals(object obj)
		{
			if (obj is RangeNumber)
			{
				RangeNumber range1 = (RangeNumber) obj;
				if (range1.min == this.min)
				{
					return (range1.max == this.max);
				}
			}
			return false;
		}

 
		public override int GetHashCode()
		{
			return base.GetHashCode();// (this.min ^ this.max);
		}

 
		public static RangeNumber operator +(RangeNumber sz1, RangeNumber sz2)
		{
			return new RangeNumber(sz1.min + sz2.min, sz1.max + sz2.max);
		}

		public static bool operator ==(RangeNumber sz1, RangeNumber sz2)
		{
			if (sz1.min == sz2.min)
			{
				return (sz1.max == sz2.max);
			}
			return false;
		}
 
		//		public static explicit operator RangeNumber(RangeNumber range)
		//		{
		//			return new RangeNumber(range.min, range.max);
		//		}
		//
		//		public static implicit operator RangeF(RangeNumber r)
		//		{
		//			return new RangeF((float) r.min, (float) r.max);
		//		}

 
		public static bool operator !=(RangeNumber sz1, RangeNumber sz2)
		{
			return !(sz1 == sz2);
		}

 
		public static RangeNumber operator -(RangeNumber sz1, RangeNumber sz2)
		{
			return new RangeNumber(sz1.min - sz2.min, sz1.max - sz2.max);
		}

 
		public static RangeNumber Round(RangeNumber value)
		{
			return new RangeNumber((decimal) Math.Round((decimal) value.min), (decimal) Math.Round((decimal) value.max));
		}

		public override string ToString()
		{
			return ("{min=" + this.min.ToString() + ", max=" + this.max.ToString() + "}");
		}

		//		public static RangeNumber Truncate(RangeF value)
		//		{
		//			return new RangeNumber((int) value.min, (int) value.max);
		//		}

		public bool IsValid(double value)
		{
			return ((decimal)value <= max && (decimal)value >= min); 
		}

		public bool IsValid(decimal value)
		{
			return ((decimal)value <= max && (decimal)value >= min); 
		}

		public bool IsValid(int value)
		{
			return ((decimal)value <= max && (decimal)value >= min); 
		}

		public bool IsValid(object obj)
		{
			decimal value= System.Convert.ToDecimal (obj);
			return (value <= max && value>= min); 
		}

		public bool IsValid(string s)
		{
			decimal value=decimal.Parse (s);
			return (value <= max && value>= min); 
		}

		public decimal MaxValue
		{
			get
			{
				return this.max;
			}
			set
			{
				this.max = value;
			}
		}
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				if (this.min == 0)
				{
					return (this.max == 0);
				}
				return false;
			}
		}
		public decimal MinValue
		{
			get
			{
				return this.min;
			}
			set
			{
				this.min = value;
			}
		}
 
		public static readonly RangeNumber Empty;
		private decimal max;
		private decimal min;
 


	}

    public class RangeNumberConverter : TypeConverter
    {
        // Methods
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string text = value as string;
            if (text == null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            string text2 = text.Trim();
            if (text2.Length == 0)
            {
                return null;
            }
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            char ch = culture.TextInfo.ListSeparator[0];
            string[] textArray = text2.Split(new char[] { ch });
            decimal[] numArray = new decimal[textArray.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(decimal));
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (decimal)converter.ConvertFromString(context, culture, textArray[i]);
            }
            if (numArray.Length != 2)
            {
                throw new ArgumentException("TextParseFailedFormat", text2);
            }
            return new RangeNumber(numArray[0], numArray[1]);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is RangeNumber)
            {
                if (destinationType == typeof(string))
                {
                    RangeNumber range = (RangeNumber)value;
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    string separator = culture.TextInfo.ListSeparator + " ";
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(decimal));
                    string[] textArray = new string[2];
                    int num = 0;
                    textArray[num++] = converter.ConvertToString(context, culture, range.MinValue);
                    textArray[num++] = converter.ConvertToString(context, culture, range.MaxValue);
                    return string.Join(separator, textArray);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    RangeNumber range = (RangeNumber)value;
                    ConstructorInfo member = typeof(RangeNumber).GetConstructor(new Type[] { typeof(decimal), typeof(decimal) });
                    if (member != null)
                    {
                        return new InstanceDescriptor(member, new object[] { range.MinValue, range.MaxValue });
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }
            object obj2 = propertyValues["MinValue"];
            object obj3 = propertyValues["MaxValue"];
            if (((obj2 == null) || (obj3 == null)) || (!(obj2 is decimal) || !(obj3 is decimal)))
            {
                throw new ArgumentException("PropertyValueInvalidEntry");
            }
            return new RangeNumber((decimal)obj2, (decimal)obj3);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(RangeNumber), attributes).Sort(new string[] { "MunValue", "MaxValue" });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }

 	#endregion


 

}
