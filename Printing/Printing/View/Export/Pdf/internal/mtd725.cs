namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd725
    {
        private int base_;
        private int var0;
        private int var1;
        private int var2;

        internal mtd725(int var2, int var0, int base_, int var1)
        {
            this.var2 = var2;
            this.var0 = var0;
            this.base_ = base_;
            this.var1 = var1;
        }

        internal bool mtd720(int var3)
        {
            return ((var3 >= this.var2) && (var3 <= this.var0));
        }

        internal void mtd733(int var3, mtd722 var4)
        {
            var4.mtd724(var3 - this.var2, this.var1);
        }

        internal int mtd732
        {
            get
            {
                return this.base_;
            }
        }

        internal int mtd738
        {
            get
            {
                return this.var0;
            }
        }

        internal int mtd739
        {
            get
            {
                return this.var1;
            }
        }

        internal int mtd740
        {
            get
            {
                return this.var2;
            }
        }
    }
}

