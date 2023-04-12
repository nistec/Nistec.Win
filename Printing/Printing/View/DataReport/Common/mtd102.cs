namespace Nistec.Printing.View
{
    using System;

    internal abstract class mtd102
    {
        protected int _mtd218 = 0;

        internal mtd102()
        {
        }

        internal abstract void mtd103(object var0);
        internal static mtd102 mtd105(TypeCode var4)
        {
            if ((var4 == TypeCode.Int32) || (var4 == TypeCode.Int16))
            {
                return new mtd220();
            }
            if (var4 == TypeCode.Single)
            {
                return new mtd221();
            }
            if (var4 == TypeCode.Double)
            {
                return new mtd222();
            }
            if (var4 == TypeCode.Decimal)
            {
                return new mtd223();
            }
            return new mtd224();
        }

        internal abstract object mtd110(int var1, int var2, AggregateType var3);
        internal abstract void mtd111();
        internal abstract void mtd2(object var0);
        internal abstract void mtd219();

        internal int mtd32
        {
            get
            {
                return this._mtd218;
            }
        }
    }
}

