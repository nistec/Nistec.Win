using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.Drawing.Design;
using System.Security;
using System.ComponentModel.Design;

using System.Drawing.Imaging;
using System.Globalization;

namespace Nistec.GridView
{
    /// <summary>
    /// Grid Preferred Column Width Type Converter
    /// </summary>
	public class GridPreferredColumnWidthTypeConverter : TypeConverter
	{
		/// <summary>
		/// 
		/// </summary>
		public GridPreferredColumnWidthTypeConverter()
		{
		}
        /// <summary>
        /// Get indicating the converter Can Convert From the given value object To specified type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if ((sourceType != typeof(string)) && (sourceType != typeof(int)))
			{
				return false;
			}
			return true;
		}

 
        /// <summary>
        /// Convert from the given value object To specified type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value.GetType() == typeof(string))
			{
				string text1 = value.ToString();
				if (text1.Equals("AutoColumnResize (-1)"))
				{
					return -1;
				}
				return int.Parse(text1);
			}
			if (value.GetType() != typeof(int))
			{
				throw base.GetConvertFromException(value);
			}
			return (int) value;
		}

        /// <summary>
        /// Convert the given value object To specified type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value.GetType() != typeof(int))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			int num1 = (int) value;
			if (num1 == -1)
			{
				return "AutoColumnResize (-1)";
			}
			return num1.ToString();
		}

	}
}
