namespace Nistec.Printing.MSExcel
{
    using Nistec.Printing;
    using System;
    using System.Xml;
    using System.Text;
    using Nistec.Printing.Data;

    public class ExcelWriteProperties : AdoProperties
    {
        private ExcelWriterType _ExcelWriterType;

  
        public ExcelWriteProperties()
        {
            _DataSource = new AdoTable();
        }

        public override AdoProperties Clone()
        {
            ExcelWriteProperties properties = new ExcelWriteProperties();
            properties.ExcelWriterType = this._ExcelWriterType;
            properties.Filename = this._filename;
            //properties.Append = this._append;
            properties.FirstRowHeaders = this._firstRowHeaders;
            properties.DateFormat = this._dateFormat;
            properties.Encoding = this._encoding;
            properties.DataSource = this._DataSource.Clone();
            return properties;
        }

 
        public string GetColumnRange()
        {
            StringBuilder sb = new StringBuilder();
            foreach (AdoColumn col in this.DataSource.Columns)
            {
                sb.AppendFormat("{0} char(255),",col.ColumnName);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        public string GetFieldsRange()
        {
            StringBuilder sb = new StringBuilder();
            foreach (AdoColumn col in this.DataSource.Columns)
            {
                sb.AppendFormat("{0},", col.ColumnName);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }


        public ExcelWriterType ExcelWriterType
        {
            get
            {
                return this._ExcelWriterType;
            }
            set
            {
                if (this._ExcelWriterType != value)
                {
                    this._ExcelWriterType = value;
                    base.OnChanged();
                }
            }
        }
        
        //public bool Append
        //{
        //    get
        //    {
        //        return this._append;
        //    }
        //    set
        //    {
        //        if (this._append != value)
        //        {
        //            this._append = value;
        //            base.OnChanged();
        //        }
        //    }
        //}

        public string DateFormat
        {
            get
            {
                return this._dateFormat;
            }
            set
            {
                if (this._dateFormat != value)
                {
                    this._dateFormat = value;
                    base.OnChanged();
                }
            }
        }

        public string Encoding
        {
            get
            {
                return this._encoding;
            }
            set
            {
                if (this._encoding != value)
                {
                    this._encoding = value;
                    base.OnChanged();
                }
            }
        }

        public string Filename
        {
            get
            {
                return this._filename;
            }
            set
            {
                if (this._filename != value)
                {
                    this._filename = value;
                    base.OnChanged();
                }
            }
        }

        public bool FirstRowHeaders
        {
            get
            {
                return this._firstRowHeaders;
            }
            set
            {
                if (this._firstRowHeaders != value)
                {
                    this._firstRowHeaders = value;
                    base.OnChanged();
                }
            }
        }

        public AdoTable DataSource
        {
            get
            {
                return this._DataSource;
            }
            set
            {
                if (this._DataSource != value)
                {
                    this._DataSource = value;
                    base.OnChanged();
                }
            }
        }

   
    }
}

