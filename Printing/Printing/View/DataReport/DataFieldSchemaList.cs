namespace Nistec.Printing.View
{
    using System;
    using System.Reflection;

    public class DataFieldSchemaList
    {
        private DataFieldSchema[] _var0 = new DataFieldSchema[10];
        private int _var1;

        internal DataFieldSchemaList()
        {
        }

        public void Add(DataFieldSchema fieldschema)
        {
            if (this.var2(fieldschema))
            {
                this.var3();
                this._var0[this._var1] = fieldschema;
                this._var1++;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < this._var1; i++)
            {
                this._var0[i] = null;
            }
            this._var1 = 0;
        }

        public int IndexOf(DataFieldSchema dataFieldSchema)
        {
            for (int i = 0; i < this._var1; i++)
            {
                if (dataFieldSchema == this._var0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(DataFieldSchema dataFieldSchema, int index)
        {
            if (this.var2(dataFieldSchema))
            {
                if ((index < 0) || (index > this._var1))
                {
                    throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
                }
                this.var3();
                if (index < this._var1)
                {
                    Array.Copy(this._var0, index, this._var0, index + 1, this._var1 - index);
                }
                this._var0[index] = dataFieldSchema;
                this._var1++;
            }
        }

        public void Remove(DataFieldSchema dataFieldSchema)
        {
            int index = this.IndexOf(dataFieldSchema);
            if (index != -1)
            {
                this.RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this._var1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this._var1--;
            if (index < this._var1)
            {
                Array.Copy(this._var0, index + 1, this._var0, index, this._var1 - index);
            }
            this._var0[this._var1] = null;
        }

        private bool var2(DataFieldSchema var4)
        {
            for (int i = 0; i < this._var1; i++)
            {
                DataFieldSchema schema = this._var0[i];
                if ((var4 == schema) || (var4.DataFieldName == schema.DataFieldName))
                {
                    return false;
                }
            }
            return true;
        }

        private void var3()
        {
            if (this._var1 >= this._var0.Length)
            {
                DataFieldSchema[] destinationArray = new DataFieldSchema[2 * this._var0.Length];
                Array.Copy(this._var0, destinationArray, this._var1);
                this._var0 = destinationArray;
            }
        }

        public int Count
        {
            get
            {
                return this._var1;
            }
        }

        public DataFieldSchema this[int index]
        {
            get
            {
                if ((index > -1) && (index < this._var1))
                {
                    return this._var0[index];
                }
                return null;
            }
        }

        public DataFieldSchema this[string datafieldname]
        {
            get
            {
                DataFieldSchema schema = null;
                for (int i = 0; i < this._var1; i++)
                {
                    schema = this._var0[i];
                    if (schema.DataFieldName == datafieldname)
                    {
                        return schema;
                    }
                }
                return null;
            }
        }
    }
}

