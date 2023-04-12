namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class mtd389 : IEnumerable
    {
        private ArrayList _var0 = new ArrayList();

        public event CollectionChange mtd390;

        public event CollectionChange mtd391;

        internal void mtd2(SectionDesigner var4)
        {
            if (var4 == null)
            {
                throw new Exception("DesignerSection Is Null");
            }
            int count = this._var0.Count;
            this._var0.Add(var4);
            if (this.mtd390 != null)
            {
                this.mtd390(count, var4);
            }
        }

        internal int mtd215(SectionDesigner var4)
        {
            return this._var0.IndexOf(var4);
        }

        internal int mtd215(string var2)
        {
            int num = 0;
            foreach (object obj2 in this._var0)
            {
                if (string.Compare(((SectionDesigner) obj2).mtd91, var2, true) == 0)
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        internal void mtd216(int index, SectionDesigner var4)
        {
            if (var4 == null)
            {
                throw new Exception("DesignerSection Is Null");
            }
            this._var0.Insert(index, var4);
            if (this.mtd390 != null)
            {
                this.mtd390(index, var4);
            }
        }

        internal void mtd217(SectionDesigner var4)
        {
            int index = this.mtd215(var4);
            this._var0.RemoveAt(index);
            if (this.mtd391 != null)
            {
                this.mtd391(index, var4);
            }
        }

        internal void mtd387()
        {
            this._var0.Clear();
        }

        internal void mtd394(int var1)
        {
            SectionDesigner mtd = (SectionDesigner) this._var0[var1];
            this._var0.RemoveAt(var1);
            if (this.mtd391 != null)
            {
                this.mtd391(var1, mtd);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this._var0.GetEnumerator();
        }

        internal int mtd166
        {
            get
            {
                return this._var0.Count;
            }
        }

        internal SectionDesigner this[int var1]
        {
            get
            {
                return (SectionDesigner) this._var0[var1];
            }
        }

        internal SectionDesigner this[Section var3]
        {
            get
            {
                foreach (object obj2 in this._var0)
                {
                    SectionDesigner mtd = (SectionDesigner) obj2;
                    if (mtd.mtd393 == var3)
                    {
                        return mtd;
                    }
                }
                return null;
            }
        }

        internal SectionDesigner this[string var2]
        {
            get
            {
                foreach (object obj2 in this._var0)
                {
                    SectionDesigner mtd = (SectionDesigner) obj2;
                    if (string.Compare(mtd.mtd91, var2, true) == 0)
                    {
                        return mtd;
                    }
                }
                return null;
            }
        }
    }
}

