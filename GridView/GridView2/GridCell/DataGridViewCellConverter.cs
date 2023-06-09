namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    internal class GridCellConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            GridCell cell = value as GridCell;
            if ((destinationType == typeof(InstanceDescriptor)) && (cell != null))
            {
                ConstructorInfo constructor = cell.GetType().GetConstructor(new System.Type[0]);
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, new object[0], false);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

