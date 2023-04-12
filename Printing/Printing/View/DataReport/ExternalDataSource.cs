namespace Nistec.Printing.View
{
    using System;

    public class ExternalDataSource
    {
        private Nistec.Printing.View.DataFieldSchemaList _var0 = new Nistec.Printing.View.DataFieldSchemaList();

        public void AddDataFieldSchema(string fieldname, TypeCode typecode)
        {
            this._var0.Add(new DataFieldSchema(fieldname, typecode));
        }

        public Nistec.Printing.View.DataFieldSchemaList DataFieldSchemaList
        {
            get
            {
                return this._var0;
            }
        }
    }
}

