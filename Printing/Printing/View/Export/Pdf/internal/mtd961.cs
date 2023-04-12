namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd961
    {
        private mtd1016[] _var0;
        private int _var1;

        internal mtd961()
        {
            this._var1 = 0;
            this._var0 = new mtd1016[10];
        }

        internal mtd961(mtd1016[] var2)
        {
            this._var1 = var2.Length;
            this._var0 = var2;
        }

        internal void mtd2(mtd1016 var2)
        {
            this.var4();
            this._var0[this._var1] = var2;
            this._var1++;
        }

        internal void mtd387()
        {
            for (int i = 0; i < this._var1; i++)
            {
                this._var0[i] = null;
            }
            this._var1 = 0;
        }

        private void var4()
        {
            if (this._var1 >= this._var0.Length)
            {
                mtd1016[] destinationArray = new mtd1016[2 * this._var0.Length];
                Array.Copy(this._var0, destinationArray, this._var1);
                this._var0 = destinationArray;
            }
        }

        internal int mtd32
        {
            get
            {
                return this._var1;
            }
        }

        internal mtd1016 this[int var3]
        {
            get
            {
                return this._var0[var3];
            }
        }
    }
}

