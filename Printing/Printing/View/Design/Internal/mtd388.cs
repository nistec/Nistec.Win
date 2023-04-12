namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    using System.Collections;
    using System.Reflection;
    //mtd388
    internal class SectionListDesigner : IEnumerable
    {
        private Section[] _var0 = new Section[2];
        private int _count = 0;

        internal SectionListDesigner()
        {
        }

        internal void mtd2(Section s)
        {
            this._var0.SetValue(s, this._count);
            this._count++;
        }

        internal void mtd387()
        {
            this._count = 0;
            this._var0[0] = null;
            this._var0[1] = null;
        }

        public IEnumerator GetEnumerator()
        {
            return this._var0.GetEnumerator();
        }

        internal int Count //mtd166
        {
            get
            {
                return this._count;
            }
        }

        internal Section this[int idx]
        {
            get
            {
                return this._var0[idx];
            }
        }
    }
}

