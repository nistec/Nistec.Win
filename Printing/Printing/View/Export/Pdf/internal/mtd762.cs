namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd762
    {
        private mtd776[] _var0;
        private int _var1 = 0;
        private mtd776 _var2;

        internal mtd762(mtd776 var2)
        {
            this._var2 = var2;
            this._var0 = new mtd776[10];
        }

        internal void mtd2(mtd776 var4)
        {
            this.var5();
            var4.mtd208 = this._var2;
            this._var0[this._var1] = var4;
            this._var1++;
        }

        internal int mtd215(mtd776 var4)
        {
            for (int i = 0; i < this._var1; i++)
            {
                if (var4 == this._var0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        internal void mtd217(mtd776 var4)
        {
            int num = this.mtd215(var4);
            if (num != -1)
            {
                this.mtd394(num);
            }
        }

        internal void mtd394(int var3)
        {
            if ((var3 < 0) || (var3 >= this._var1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this._var1--;
            this._var0[var3].mtd208 = null;
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
                mtd776[] sourceArray = this._var0;
                this._var0 = new mtd776[2 * this._var0.Length];
                Array.Copy(sourceArray, this._var0, this._var1);
            }
        }

        internal int mtd166
        {
            get
            {
                return this._var1;
            }
        }

        internal mtd776 this[int var3]
        {
            get
            {
                return this._var0[var3];
            }
        }
    }
}

