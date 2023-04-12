namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class mtd875
    {
        private ArrayList _var0 = new ArrayList();

        internal mtd875()
        {
        }

        internal bool mtd2(ushort var2)
        {
            if (!this._var0.Contains(var2))
            {
                this._var0.Add(var2);
                return true;
            }
            return false;
        }

        internal void mtd387()
        {
            this._var0.Clear();
        }

        internal int mtd166
        {
            get
            {
                return this._var0.Count;
            }
        }

        internal ushort[] mtd880
        {
            get
            {
                this._var0.Sort();
                ushort[] numArray = new ushort[this.mtd166];
                for (int i = 0; i < this.mtd166; i++)
                {
                    numArray[i] = this[i];
                }
                return numArray;
            }
        }

        internal ushort this[int var1]
        {
            get
            {
                return (ushort) this._var0[var1];
            }
        }
    }
}

