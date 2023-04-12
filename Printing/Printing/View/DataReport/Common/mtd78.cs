namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text.RegularExpressions;

    //mtd78
    internal class UISizeConverter  : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            float num;
            try
            {
                num = float.Parse(Regex.Replace((string)value, ReportUtil.Unit, "", RegexOptions.IgnoreCase), NumberFormatInfo.InvariantInfo) * ReportUtil.Dpi;

                //num = float.Parse(Regex.Replace((string)value, "cm", "", RegexOptions.IgnoreCase), NumberFormatInfo.CurrentInfo);
                //num *= 37.8f;

            }
            catch
            {
                throw new Exception("Value " + value + " Not in correct format");
            }
            return num;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            decimal num = decimal.Round((decimal)(((float)value) / ReportUtil.Dpi), 3);
            return string.Format(NumberFormatInfo.InvariantInfo, "{0} {1}", new object[] { num }, ReportUtil.Unit);
            //decimal num = decimal.Round((decimal)(((float)value) / 37.8f), 3);
            //return string.Format(NumberFormatInfo.CurrentInfo, "{0} cm", new object[] { num });
        }
    }
}

