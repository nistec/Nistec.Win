namespace MControl.Charts
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    internal class ColorItemConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(InstanceDescriptor)) && (value is ColorItem))
            {
                ColorItem item = value as ColorItem;
                ConstructorInfo constructor = typeof(ColorItem).GetConstructor(new Type[] { typeof(string) });
                if (constructor != null)
                {
                    string str = string.Concat(new object[] { item.color.A, ",", item.color.R, ",", item.color.G, ",", item.color.B });
                    return new InstanceDescriptor(constructor, new object[] { str });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

