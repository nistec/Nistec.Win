namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;

    internal class mtd77 : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (context != null)
            {
                return base.CanConvertFrom(context, sourceType);
            }
            return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (context != null)
            {
                return base.CanConvertFrom(context, destinationType);
            }
            return ((destinationType == typeof(Image)) || base.CanConvertFrom(context, destinationType));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if ((context == null) && (value is string))
            {
                try
                {
                    byte[] buffer = Convert.FromBase64String((string) value);
                    using (MemoryStream stream = new MemoryStream(buffer, 0, buffer.Length))
                    {
                        return Image.FromStream(stream);
                    }
                }
                catch
                {
                    throw new Exception("Can not DeSerilaized Image '" + value + "' - Please reset Image");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((context == null) && (value != null))
            {
                try
                {
                    Image image = (Image) value;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        image.Save(stream, ImageFormat.Png);
                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
                catch
                {
                    throw new Exception("Can not Serialize Image " + value);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

