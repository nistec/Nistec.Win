namespace Nistec.Printing.View
{
    using System;
    using System.Collections;

    internal class mtd127 : CollectionBase
    {
        internal mtd163 mtd143(int var0)
        {
            return (mtd163) base.List[var0];
        }

        internal void mtd2(mtd163 var1)
        {
            base.List.Add(var1);
        }

        internal int mtd215(mtd163 var1)
        {
            return base.List.IndexOf(var1);
        }

        internal void mtd216(int var2, mtd163 var1)
        {
            base.List.Insert(var2, var1);
        }

        internal void mtd217(mtd163 var3)
        {
            base.List.Remove(var3);
        }

        internal void mtd217(int var0)
        {
            base.List.RemoveAt(var0);
        }
    }
}

