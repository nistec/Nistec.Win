namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class mtd164 : IEnumerable
    {
        private mtd126[] _var0;
        private int _var1;

        internal mtd164(int var2)
        {
            this._var0 = new mtd126[var2];
            this._var1 = 0;
        }

        internal void mtd2(mtd126 var5)
        {
            if (this._var1 >= this._var0.Length)
            {
                throw new IndexOutOfRangeException("Exceeds Capacity of fixed sized Array");
            }
            this._var0.SetValue(var5, this._var1);
            this._var1++;
        }

        internal mtd126 mtd204(string var4)
        {
            foreach (mtd126 mtd in this._var0)
            {
                if (string.Compare(var4, mtd.Name) == 0)
                {
                    return mtd;
                }
            }
            return null;
        }

        internal void mtd211(mtd126 var5)
        {
            if (this._var1 >= this._var0.Length)
            {
                throw new IndexOutOfRangeException("Exceeds Capacity of fixed sized Array");
            }
            this._var0.SetValue(var5, var5.RptControl.Index);
            this._var1++;
        }

        internal void mtd213()
        {
            Array.Sort(this._var0, 0, this._var1, new var6());
        }

        internal void mtd214()
        {
            mtd126[] sourceArray = this._var0;
            this._var0 = new mtd126[this._var1];
            Array.Copy(sourceArray, this._var0, this._var1);
        }

        public IEnumerator GetEnumerator()
        {
            return new Nistec.Printing.View.ArrayEnumerator(this._var0, this._var1);
        }

        internal int mtd166
        {
            get
            {
                return this._var1;
            }
        }

        internal int mtd210
        {
            get
            {
                return this._var0.Length;
            }
        }

        internal mtd126 this[int var3]
        {
            get
            {
                return this._var0[var3];
            }
        }

        private class var6 : IComparer
        {
            public int Compare(object x, object y)
            {
                int index = ((mtd126)x).RptControl.Index;
                int num2 = ((mtd126)y).RptControl.Index;
                int num3 = 0;
                if (index > num2)
                {
                    num3 = 1;
                }
                if (index < num2)
                {
                    num3 = -1;
                }
                return num3;
            }
        }
    }
}

