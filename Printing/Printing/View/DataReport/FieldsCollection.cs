namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class FieldsCollection : IEnumerable
    {
        private ArrayList _var0 = new ArrayList();

        internal int mtd2(string var2, int var3, TypeCode var4, bool var5)
        {
            if (!this.var1(var2))
            {
                throw new Exception("Not valid Field name");
            }
            int index = this.IndexOf(var2);
            if (index != -1)
            {
                return index;
            }
            this.Add(new McField(var2, var3, var4, var5));
            return -1;
        }

        public int Add(McField value)
        {
            if (!this.var1(value.Name))
            {
                throw new Exception("Not valid Field name");
            }
            int index = this.IndexOf(value);
            if (index != -1)
            {
                return index;
            }
            this._var0.Add(value);
            return -1;
        }

        public int Add(string FieldName, TypeCode tc)
        {
            if (!this.var1(FieldName))
            {
                throw new Exception("Not valid Field name");
            }
            int index = this.IndexOf(FieldName);
            if (index != -1)
            {
                return index;
            }
            this.Add(new McField(FieldName, tc));
            return -1;
        }

        public void Clear()
        {
            this._var0.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            return this._var0.GetEnumerator();
        }

        public int IndexOf(McField value)
        {
            return this._var0.IndexOf(value);
        }

        public int IndexOf(string name)
        {
            int num = 0;
            foreach (object obj2 in this._var0)
            {
                if (string.Compare(((McField) obj2).Name, name, true) == 0)
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        public void Insert(int index, McField value)
        {
            if (!this.var1(value.Name))
            {
                throw new Exception("Not valid Field name");
            }
            if (this.IndexOf(value) == -1)
            {
                this._var0.Insert(index, value);
            }
        }

        public void Remove(McField value)
        {
            int index = this._var0.IndexOf(value);
            this._var0.RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            this._var0.RemoveAt(index);
        }

        private bool var1(string var6)
        {
            return ((var6 != null) && (var6.Length != 0));
        }

        public int Count
        {
            get
            {
                return this._var0.Count;
            }
        }

       
        public McField this[int index]
        {
            get
            {
                return (McField) this._var0[index];
            }
        }

        public McField this[string name]
        {
            get
            {
                foreach (object obj2 in this._var0)
                {
                    McField field = (McField) obj2;
                    if (string.Compare(field.Name, name, true) == 0)
                    {
                        return field;
                    }
                }
                return null;
            }
        }
    }
}

