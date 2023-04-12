namespace MControl.Charts
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    internal class ImageItemConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(InstanceDescriptor)) && (value is ImageItem))
            {
                ImageItem item = value as ImageItem;
                ConstructorInfo constructor = typeof(ImageItem).GetConstructor(new Type[] { typeof(string) });
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, new object[] { item.Image });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

