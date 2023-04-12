namespace Nistec.Printing.Pdf
{
    using Nistec.Printing;
    using System;
    using System.Xml;
    using Nistec.Printing.Data;

    public class PdfWriteProperties : AdoProperties
    {
  
        public PdfWriteProperties()
        {
            _DataSource = new AdoTable();
        }

        public override AdoProperties Clone()
        {
            PdfWriteProperties properties = new PdfWriteProperties();
            properties.Filename = this._filename;
            properties.FirstRowHeaders = this._firstRowHeaders;
            properties.DateFormat = this._dateFormat;
            properties.Encoding = this._encoding;
            properties.DataSource = this._DataSource.Clone();
            return properties;
        }

  
  
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

