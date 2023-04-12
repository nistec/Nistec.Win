namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    /// <summary>Converts <see cref="T:MControl.GridView.GridCellStyle"></see> objects to and from other data types.  </summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellStyleConverter : TypeConverter
    {
        /// <filterpriority>1</filterpriority>
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        /// <filterpriority>1</filterpriority>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if ((destinationType == typeof(InstanceDescriptor)) && (value is GridCellStyle))
            {
                return new InstanceDescriptor(value.GetType().GetConstructor(new System.Type[0]), new object[0], false);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

