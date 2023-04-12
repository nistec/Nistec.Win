namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    internal class DataItemConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(InstanceDescriptor)) && (value is DataItem))
            {
                DataItem item = value as DataItem;
                ConstructorInfo constructor = typeof(DataItem).GetConstructor(new Type[] { typeof(string) });
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, new object[] { item.Name });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

