namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd638
    {
        private mtd639[] _var0 = new mtd639[10];
        private int _var1;

        internal mtd638()
        {
        }

        internal void mtd2(mtd639 var3)
        {
            this.var4();
            this._var0[this._var1] = var3;
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
                mtd639[] destinationArray = new mtd639[2 * this._var0.Length];
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

        internal mtd639 this[int var2]
        {
            get
            {
                return this._var0[var2];
            }
        }
    }
}

