namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class SectionCollection : IList, ICollection, IEnumerable
    {
        private ArrayList _var0 = new ArrayList();

        public event CollectionChange Inserted;

        public event CollectionChange Removed;

        public int Add(Section value)
        {
            int count = this._var0.Count;
            this._var0.Add(value);
            if (this.Inserted != null)
            {
                this.Inserted(count, value);
            }
            return count;
        }

        public void AddRange(Section[] s)
        {
            this._var0.AddRange(s);
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

        public Section GetItem(SectionType sType)
        {
            foreach (object obj2 in this._var0)
            {
                Section section = (Section) obj2;
                if (sType == section.Type)
                {
                    return section;
                }
            }
            return null;
        }

        public int IndexOf(Section value)
        {
            return this._var0.IndexOf(value);
        }

        public int IndexOf(string name)
        {
            int num = 0;
            foreach (object obj2 in this._var0)
            {
                if (string.Compare(((Section) obj2).Name, name, true) == 0)
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        public void Insert(int index, Section value)
        {
            this._var0.Insert(index, value);
            if (this.Inserted != null)
            {
                this.Inserted(index, value);
            }
        }

        public void Remove(Section value)
        {
            int index = this._var0.IndexOf(value);
            this._var0.RemoveAt(index);
            if (this.Removed != null)
            {
                this.Removed(index, value);
            }
        }

        public void RemoveAt(int index)
        {
            Section section = (Section) this._var0[index];
            this._var0.RemoveAt(index);
            if (this.Removed != null)
            {
                this.Removed(index, section);
            }
        }

        int IList.Add(object value)
        {
            if (!(value is Section))
            {
                throw new ArgumentException("value");
            }
            return this.Add((Section) value);
        }

        void IList.Clear()
        {
            this._var0.Clear();
        }

        bool IList.Contains(object value)
        {
            if (!(value is Section))
            {
                throw new ArgumentException("value");
            }
            return this._var0.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return this._var0.IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            if (!(value is Section))
            {
                throw new ArgumentException("value");
            }
            this.Insert(index, (Section) value);
        }

        void IList.Remove(object value)
        {
            if (!(value is Section))
            {
                throw new ArgumentException("value");
            }
            this.Remove((Section) value);
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

        public Section this[string name]
        {
            get
            {
                foreach (object obj2 in this._var0)
                {
                    Section section = (Section) obj2;
                    if (string.Compare(section.Name, name, true) == 0)
                    {
                        return section;
                    }
                }
                return null;
            }
        }

        public Section this[int index]
        {
            get
            {
                return (Section) this._var0[index];
            }
            set
            {
                this._var0[index] = value;
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
                if (!(value is Section))
                {
                    throw new ArgumentException("value");
                }
                this._var0[index] = value;
            }
        }
    }
}

