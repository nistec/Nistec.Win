namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd719
    {
        private int var0;
        private int var1;
        private int var2;
        private int var3;

        internal mtd719(int var3, int var2, int var0, int var1)
        {
            this.var3 = var3;
            this.var2 = var2;
            this.var0 = var0;
            this.var1 = var1;
        }

        internal bool mtd720(int var4)
        {
            return ((var4 >= this.var3) && (var4 <= this.var2));
        }

        internal void mtd721(int var4, mtd722 var5)
        {
            int num = mtd656.mtd723(this.var1 + (var4 - this.var3), this.var0);
            var5.mtd724(num, this.var0);
        }
    }
}

