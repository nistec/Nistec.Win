namespace Nistec.Printing.View
{
    using System;

    public class DataFieldSchema
    {
        private string _var0;
        private System.TypeCode _var1;

        public DataFieldSchema(string datafieldname, System.TypeCode typecode)
        {
            this._var0 = datafieldname;
            this._var1 = typecode;
        }

        public string DataFieldName
        {
            get
            {
                return this._var0;
            }
        }

        public System.TypeCode TypeCode
        {
            get
            {
                return this._var1;
            }
        }
    }
}

