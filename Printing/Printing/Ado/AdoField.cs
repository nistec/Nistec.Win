using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nistec.Win;

namespace Nistec.Printing.Data
{
    public class AdoField:IDataField
    {
        //public readonly int Ordinal;
        //public readonly string Caption;
        //public readonly string ColumnName;
        //private Type _DataType;
        
        //public virtual Type DataType
        //{
        //    get { return _DataType; }
        //}

        protected Type _type;
        protected string _name;
        protected int _ordinal;
        protected string _caption;

        internal AdoField()
        {

        }

        public AdoField(string columnName, string caption)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException("ColumnName");
            }
            ColumnName = columnName;
            Caption = caption;
            _ordinal = -1;
            _type = typeof(string);
        }
        public AdoField(string columnName, string caption, int ordinal)
            : this(columnName, caption)
        {
            _ordinal = ordinal;
        }
        public AdoField(DataColumn c)
            : this(c.ColumnName, c.Caption, c.Ordinal)
        {
            _type = c.DataType;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Caption))
                return ColumnName;
            return Caption;
        }

        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(ColumnName); }
        }

        public virtual FieldType FieldType
        {
            get
            {
                return WinHelp.DataTypeOf(this._type);
            }
         }

        public virtual Type DataType
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        public virtual string ColumnName
        {
            get
            {
                return this._name;
            }
            set
            {
                if ((value == null) || (value == ""))
                {
                    throw new InvalidOperationException("Invalid value for Name property");
                }
                this._name = value;
            }
        }

        public int Ordinal
        {
            get
            {
                return this._ordinal;
            }
            set { this._ordinal = value; }
        }

        public string Caption
        {
            get
            {
                return this._caption;
            }
            set
            {
                if ((value == null) || (value == ""))
                {
                    _caption = _name;
                }
                this._caption = value;
            }
        }

        public static AdoField[] CreateFields(string[] fields)
        {
            AdoField[] ec = new AdoField[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                ec[i] = new AdoField(fields[i], fields[i]);
            }
            return ec;
        }

        public static AdoField[] CreateFields(DataTable dt)
        {
            AdoField[] ec = new AdoField[dt.Columns.Count];
            int i = 0;
            foreach (DataColumn c in dt.Columns)
            {
                ec[i] = new AdoField(c);
                i++;
            }
            return ec;
        }
        public static AdoField[] CreateFields(DataTable dt, string[] fields)
        {
            AdoField[] ec = new AdoField[fields.Length];
            int i = 0;
            foreach (string s in fields)
            {
                ec[i] = new AdoField(dt.Columns[s]);
                i++;
            }
            return ec;
        }

        public static object[] CreateRow(DataRow dr, AdoField[] fields)
        {
            object[] ec = new object[fields.Length];
            int i = 0;
            foreach (AdoField c in fields)
            {
                ec[i] = dr[c.ColumnName];
                i++;
            }
            return ec;
        }
    }

}
