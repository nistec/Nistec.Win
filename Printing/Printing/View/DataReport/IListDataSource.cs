namespace Nistec.Printing.View
{
    using System;
    using System.Collections;

    public class IListDataSource
    {
        private IList _var0;
        private int _var1;
        private Nistec.Printing.View.DataFieldSchemaList _var2;

        public IListDataSource(IList data) : this(data, -1)
        {
        }

        internal IListDataSource(IList data, int level)
        {
            this._var0 = data;
            this._var1 = level;
            this._var2 = new Nistec.Printing.View.DataFieldSchemaList();
        }

        public void AddDataFieldSchema(string fieldname, TypeCode typecode)
        {
            this._var2.Add(new DataFieldSchema(fieldname, typecode));
        }

        internal int mtd182
        {
            get
            {
                return this._var1;
            }
        }

        public Nistec.Printing.View.DataFieldSchemaList DataFieldSchemaList
        {
            get
            {
                return this._var2;
            }
        }

        public IList List
        {
            get
            {
                return this._var0;
            }
        }
    }
}

