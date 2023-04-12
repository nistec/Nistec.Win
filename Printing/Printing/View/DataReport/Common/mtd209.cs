namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class mtd209 : IEnumerable
    {
        private float[] _var0 = new float[10];
        private int _var1 = 0;

        internal mtd209()
        {
        }

        internal void mtd2(float var3)
        {
            this.var4();
            this._var0.SetValue(var3, this._var1);
            this._var1++;
        }

        public IEnumerator GetEnumerator()
        {
            return new Nistec.Printing.View.ArrayEnumerator(this._var0, this._var1);
        }

        private void var4()
        {
            if (this._var1 >= this._var0.Length)
            {
                float[] destinationArray = new float[this._var0.Length + 10];
                Array.Copy(this._var0, destinationArray, this._var1);
                this._var0 = destinationArray;
            }
        }

        internal int mtd166
        {
            get
            {
                return this._var1;
            }
        }

        internal float this[int var2]
        {
            get
            {
                return this._var0[var2];
            }
        }
    }
}

