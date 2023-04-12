namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd1011
    {
        private mtd1012[] _var0;
        private int _var1;

        internal mtd1011()
        {
            this._var0 = new mtd1012[10];
        }

        internal mtd1011(mtd1012[] var0)
        {
            this._var0 = var0;
            this._var1 = var0.Length;
        }

        internal void mtd2(mtd1012 var3)
        {
            this.var4();
            this._var0[this._var1] = var3;
            this._var1++;
        }

        private void var4()
        {
            if (this._var1 >= this._var0.Length)
            {
                mtd1012[] destinationArray = new mtd1012[2 * this._var0.Length];
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

        internal mtd1012 this[int var2]
        {
            get
            {
                return this._var0[var2];
            }
        }
    }
}

