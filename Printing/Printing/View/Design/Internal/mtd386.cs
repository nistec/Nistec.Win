namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    using System.Collections;
    using System.Reflection;

    internal class mtd386 : IEnumerable
    {
        private McReportControl[] _var0 = new McReportControl[10];
        private int _var1;

        internal mtd386()
        {
        }

        internal void mtd2(McReportControl c)
        {
            this.var2();
            this._var0.SetValue(c, this._var1);
            this._var1++;
        }

        internal void mtd387()
        {
            this._var1 = 0;
            this._var0 = new McReportControl[10];
        }

        public IEnumerator GetEnumerator()
        {
            return this._var0.GetEnumerator();
        }

        private void var2()
        {
            if (this._var1 >= this._var0.Length)
            {
                McReportControl[] destinationArray = new McReportControl[this._var0.Length + 10];
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

        internal McReportControl this[int idx]
        {
            get
            {
                return this._var0[idx];
            }
        }
    }
}

