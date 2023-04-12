namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    internal class mtd72 : TypeConverter
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
            string str;
            try
            {
                str = mtd73.mtd74((string) value);
            }
            catch
            {
                throw new Exception("Error in Conversion FromBase64");
            }
            return str;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            try
            {
                value = mtd73.mtd75((string) value);
            }
            catch
            {
                throw new Exception("Error in Conversion ToBase64");
            }
            return value;
        }
    }
}

