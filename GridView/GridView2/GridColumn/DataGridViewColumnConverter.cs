namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    internal class GridColumnConverter : ExpandableObjectConverter
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
            GridColumn column = value as GridColumn;
            if ((destinationType == typeof(InstanceDescriptor)) && (column != null))
            {
                ConstructorInfo constructor;
                if (column.CellType != null)
                {
                    constructor = column.GetType().GetConstructor(new System.Type[] { typeof(System.Type) });
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[] { column.CellType }, false);
                    }
                }
                constructor = column.GetType().GetConstructor(new System.Type[0]);
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, new object[0], false);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

