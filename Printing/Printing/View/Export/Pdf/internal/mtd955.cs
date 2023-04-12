namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd955
    {
        private mtd1015[] _var0 = new mtd1015[10];
        private int _var1;

        internal mtd955()
        {
        }

        internal void mtd2(mtd1015 var3)
        {
            if (var3 != null)
            {
                this.var4();
                this._var0[this._var1] = var3;
                this._var1++;
            }
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
                mtd1015[] destinationArray = new mtd1015[2 * this._var0.Length];
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

        internal mtd1015 this[int var2]
        {
            get
            {
                return this._var0[var2];
            }
        }
    }
}

