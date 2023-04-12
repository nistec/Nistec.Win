namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;

    internal class mtd76 : ReferenceConverter
    {
        private ReferenceConverter _var0;

        public mtd76() : base(typeof(IListSource))
        {
            this._var0 = new ReferenceConverter(typeof(IList));
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
            TypeConverter.StandardValuesCollection valuess2 = this._var0.GetStandardValues(context);
            ArrayList values = this.var1(standardValues);
            values.AddRange(this.var1(valuess2));
            values.Add(null);
            return new TypeConverter.StandardValuesCollection(values);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        private ArrayList var1(ICollection var2)
        {
            ArrayList list = new ArrayList();
            foreach (object obj2 in var2)
            {
                if ((obj2 == null) || (!(obj2 is DataSet) && !(obj2 is DataTable)))
                {
                    continue;
                }
                list.Add(obj2);
            }
            return list;
        }
    }
}

