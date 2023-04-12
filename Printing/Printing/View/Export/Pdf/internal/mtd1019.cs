namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd1019
    {
        private mtd1020[] _var0 = new mtd1020[10];
        private int _var1;

        internal mtd1019()
        {
        }

        internal void mtd2(mtd1020 var3)
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

        internal void mtd394(int var2)
        {
            if ((var2 < 0) || (var2 >= this._var1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this._var1--;
            if (var2 < this._var1)
            {
                Array.Copy(this._var0, var2 + 1, this._var0, var2, this._var1 - var2);
            }
            this._var0[this._var1] = null;
        }

        private void var4()
        {
            if (this._var1 >= this._var0.Length)
            {
                mtd1020[] destinationArray = new mtd1020[2 * this._var0.Length];
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

        internal mtd1020 this[int var2]
        {
            get
            {
                return this._var0[var2];
            }
        }
    }
}

