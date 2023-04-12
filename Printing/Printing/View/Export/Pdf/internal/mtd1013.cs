namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd1013
    {
        private mtd1014[] _var0 = new mtd1014[10];
        private int _var1;

        internal mtd1013()
        {
        }

        internal void mtd2(mtd1014 var3)
        {
            this.var4();
            this._var0[this._var1] = var3;
            this._var1++;
        }

        private void var4()
        {
            if (this._var1 >= this._var0.Length)
            {
                mtd1014[] destinationArray = new mtd1014[2 * this._var0.Length];
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

        internal mtd1014 this[int var2]
        {
            get
            {
                return this._var0[var2];
            }
        }
    }
}

