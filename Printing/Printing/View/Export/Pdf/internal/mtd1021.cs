namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd1021
    {
        private mtd1022[] _var0;
        private int _var1;

        internal mtd1021()
        {
            this._var1 = 0;
            this._var0 = new mtd1022[10];
        }

        internal mtd1021(mtd1021 var2)
        {
            this._var1 = var2._var1;
            this._var0 = new mtd1022[this._var1];
            for (int i = 0; i < this._var1; i++)
            {
                this._var0[i] = var2[i];
            }
        }

        internal void mtd2(mtd1022 var4)
        {
            this.var5();
            this._var0[this._var1] = var4;
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

        internal void mtd394(int var3)
        {
            if ((var3 < 0) || (var3 >= this._var1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this._var1--;
            if (var3 < this._var1)
            {
                Array.Copy(this._var0, var3 + 1, this._var0, var3, this._var1 - var3);
            }
            this._var0[this._var1] = null;
        }

        private void var5()
        {
            if (this._var1 >= this._var0.Length)
            {
                mtd1022[] destinationArray = new mtd1022[2 * this._var0.Length];
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

        internal mtd1022 this[int var3]
        {
            get
            {
                return this._var0[var3];
            }
        }
    }
}

