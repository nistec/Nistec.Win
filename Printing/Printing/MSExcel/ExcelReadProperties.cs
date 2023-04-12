namespace Nistec.Printing.MSExcel
{
    using Nistec.Printing;
    using System;
    using System.Xml;
    using Nistec.Printing.Data;

    public class ExcelReadProperties : AdoProperties, IAdoReadProperties
    {
        //private bool _firstRowHeaders = false;
        private bool _isRange = false;
        private byte _IMEX = 1;
        private string _namedRange = "";
        private string _query = "";
        private QueryType _queryType = QueryType.Worksheet;
        private string _range = "";
        private string _workbook = "";
        private string _worksheet = "";
        private ExcelReaderType _ExcelReaderType;

        public override AdoProperties Clone()
        {
            ExcelReadProperties properties = new ExcelReadProperties();
            properties.ExcelReaderType = this._ExcelReaderType;
            properties.Workbook = this._workbook;
            properties.Query = this._query;
            properties.FirstRowHeaders = this._firstRowHeaders;
            properties.Worksheet = this._worksheet;
            properties.IsRange = this._isRange;
            properties.IMEX = this._IMEX;
            properties.Range = this._range;
            properties.NamedRange = this._namedRange;
            properties.QueryType = this._queryType;
            return properties;
        }

        public ExcelReaderType ExcelReaderType
        {
            get
            {
                return this._ExcelReaderType;
            }
            set
            {
                if (this._ExcelReaderType != value)
                {
                    this._ExcelReaderType = value;
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

        public bool IsRange
        {
            get
            {
                return this._isRange;
            }
            set
            {
                if (this._isRange != value)
                {
                    this._isRange = value;
                    base.OnChanged();
                }
            }
        }

        public byte IMEX
        {
            get
            {
                return this._IMEX;
            }
            set
            {
                if (this._IMEX != value)
                {
                    this._IMEX = value;
                    base.OnChanged();
                }
            }
        }

        public string NamedRange
        {
            get
            {
                return this._namedRange;
            }
            set
            {
                if (this._namedRange != value)
                {
                    this._namedRange = value;
                    base.OnChanged();
                }
            }
        }

        public string Query
        {
            get
            {
                return this._query;
            }
            set
            {
                if (this._query != value)
                {
                    this._query = value;
                    base.OnChanged();
                }
            }
        }

        public QueryType QueryType
        {
            get
            {
                return this._queryType;
            }
            set
            {
                if (this._queryType != value)
                {
                    this._queryType = value;
                    base.OnChanged();
                }
            }
        }

        public string Range
        {
            get
            {
                return this._range;
            }
            set
            {
                if (this._range != value)
                {
                    this._range = value;
                    base.OnChanged();
                }
            }
        }

        public string Workbook
        {
            get
            {
                return this._workbook;
            }
            set
            {
                if (this._workbook != value)
                {
                    this._workbook = value;
                    base.OnChanged();
                }
            }
        }

        public string Worksheet
        {
            get
            {
                return this._worksheet;
            }
            set
            {
                if (this._worksheet != value)
                {
                    this._worksheet = value;
                    base.OnChanged();
                }
            }
        }
    }
}

