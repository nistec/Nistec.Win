namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class McControlCollection : IList, ICollection, IEnumerable
    {
        private ArrayList _var0 = new ArrayList();

        public event CollectionChange Inserted;

        public event CollectionChange Removed;

        internal void mtd0()
        {
            int num = 0;
            int count = this._var0.Count;
            for (int i = 0; i < count; i++)
            {
                McReportControl control = (McReportControl) this._var0[i];
                if (control.Type != ControlType.PageBreak)
                {
                    control.Index = num;
                    num++;
                }
            }
        }

        internal void mtd1(out McReportControl[] controlList)
        {
            controlList = new McReportControl[this._var0.Count];
            int count = this._var0.Count;
            int num2 = 0;
            for (int i = 0; i < count; i++)
            {
                McReportControl control = (McReportControl) this._var0[i];
                controlList[i] = control;
                if (control.Type != ControlType.PageBreak)
                {
                    control.Index = num2;
                    num2++;
                }
            }
            Array.Sort(controlList, new var4());
        }

        public int Add(McReportControl value)
        {
            int count = this._var0.Count;
            value.Index = count;
            this._var0.Add(value);
            if (this.Inserted != null)
            {
                this.Inserted(count, value);
            }
            return count;
        }

        public void AddRange(McReportControl[] c)
        {
            this._var0.AddRange(c);
        }

        public void Clear()
        {
            this._var0.Clear();
        }

        public void CopyTo(Array array, int index)
        {
            this._var0.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return this._var0.GetEnumerator();
        }

        public int IndexOf(McReportControl value)
        {
            return this._var0.IndexOf(value);
        }

        public int IndexOf(string name)
        {
            int num = 0;
            foreach (object obj2 in this._var0)
            {
                if (string.Compare(((McReportControl) obj2).Name, name, true) == 0)
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        public void Insert(int index, McReportControl value)
        {
            value.Index = index;
            this._var0.Insert(index, value);
            this.var1(index);
            if (this.Inserted != null)
            {
                this.Inserted(index, value);
            }
        }

        public void Remove(McReportControl value)
        {
            int index = this._var0.IndexOf(value);
            this._var0.RemoveAt(index);
            this.var1(index);
            if (this.Removed != null)
            {
                this.Removed(index, value);
            }
        }

        public void RemoveAt(int index)
        {
            McReportControl control = (McReportControl) this._var0[index];
            this._var0.RemoveAt(index);
            this.var1(index);
            if (this.Removed != null)
            {
                this.Removed(index, control);
            }
        }

        public void SortByIndex()
        {
            this._var0.Sort(new var3());
        }

        private void var1(int var2)
        {
            for (int i = var2; i < this._var0.Count; i++)
            {
                ((McReportControl) this._var0[i]).Index = i;
            }
        }

        int IList.Add(object value)
        {
            if (!(value is McReportControl))
            {
                throw new ArgumentException("value");
            }
            return this.Add((McReportControl) value);
        }

        void IList.Clear()
        {
            this.Clear();
        }

        bool IList.Contains(object value)
        {
            return this._var0.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return this._var0.IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            if (!(value is McReportControl))
            {
                throw new ArgumentException("value");
            }
            this.Insert(index, (McReportControl) value);
        }

        void IList.Remove(object value)
        {
            if (!(value is McReportControl))
            {
                throw new ArgumentException("value");
            }
            this.Remove((McReportControl) value);
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public int Count
        {
            get
            {
                return this._var0.Count;
            }
        }

        public McReportControl this[string name]
        {
            get
            {
                foreach (object obj2 in this._var0)
                {
                    McReportControl control = (McReportControl) obj2;
                    if (string.Compare(control.Name, name, true) == 0)
                    {
                        return control;
                    }
                }
                return null;
            }
        }

        public McReportControl this[int index]
        {
            get
            {
                return (McReportControl) this._var0[index];
            }
            set
            {
                this._var0[index] = value;
                value.Index = index;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return this;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool IList.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this._var0[index];
            }
            set
            {
                if (!(value is McReportControl))
                {
                    throw new ArgumentException("value");
                }
                this._var0[index] = value;
                ((McReportControl) value).Index = index;
            }
        }

        private class var3 : IComparer
        {
            public int Compare(object x, object y)
            {
                float index = ((McReportControl) x).Index;
                float num2 = ((McReportControl) y).Index;
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

        private class var4 : IComparer
        {
            public int Compare(object x, object y)
            {
                float top = ((McReportControl) x).Top;
                float num2 = ((McReportControl) y).Top;
                int num3 = 0;
                if (top > num2)
                {
                    num3 = 1;
                }
                if (top < num2)
                {
                    num3 = -1;
                }
                return num3;
            }
        }
    }
}

