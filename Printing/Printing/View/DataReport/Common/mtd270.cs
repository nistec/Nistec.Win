namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class mtd270 : IEnumerable
    {
        private mtd272[] _var0;
        private int _var1;

        internal mtd270(int var1)
        {
            this._var0 = new mtd272[var1];
            this._var1 = 0;
        }

        internal void mtd2(mtd272 var3)
        {
            this._var0[this._var1] = var3;
            this._var1++;
        }

        internal void mtd214()
        {
            mtd272[] sourceArray = this._var0;
            this._var0 = new mtd272[this._var1];
            Array.Copy(sourceArray, this._var0, this._var1);
        }

        public IEnumerator GetEnumerator()
        {
            return this._var0.GetEnumerator();
        }

        internal int mtd166
        {
            get
            {
                return this._var1;
            }
        }

        internal mtd272 this[int var2]
        {
            get
            {
                return this._var0[var2];
            }
        }
    }
}

