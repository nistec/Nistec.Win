namespace Nistec.Printing.Csv
{
    using Nistec.Printing;
    using System;
    using System.Xml;
    using Nistec.Printing.Data;

    public class CsvWriteProperties : AdoProperties
    {
        private bool _append = false;
        private string _fieldDelimiter = ",";
        private FileTypes _fileType = FileTypes.Delimited;
        private RecordSeparators _recordSeparator = RecordSeparators.Windows;
        private string _textQualifier = "\"";

        public CsvWriteProperties()
        {
            _DataSource = new AdoTable();
        }

        public override AdoProperties Clone()
        {
            CsvWriteProperties properties = new CsvWriteProperties();
            properties.Filename = this._filename;
            properties.Append = this._append;
            properties.FileType = this._fileType;
            properties.TextQualifier = this._textQualifier;
            properties.FieldDelimiter = this._fieldDelimiter;
            properties.RecordSeparator = this._recordSeparator;
            properties.FirstRowHeaders = this._firstRowHeaders;
            properties.DateFormat = this._dateFormat;
            properties.Encoding = this._encoding;
            properties.DataSource = this._DataSource.Clone();
            return properties;
        }

  
        public bool Append
        {
            get
            {
                return this._append;
            }
            set
            {
                if (this._append != value)
                {
                    this._append = value;
                    base.OnChanged();
                }
            }
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

        public string FieldDelimiter
        {
            get
            {
                return this._fieldDelimiter;
            }
            set
            {
                if (this._fieldDelimiter != value)
                {
                    this._fieldDelimiter = value;
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

        public FileTypes FileType
        {
            get
            {
                return this._fileType;
            }
            set
            {
                if (this._fileType != value)
                {
                    this._fileType = value;
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

        public RecordSeparators RecordSeparator
        {
            get
            {
                return this._recordSeparator;
            }
            set
            {
                if (this._recordSeparator != value)
                {
                    this._recordSeparator = value;
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

        public string TextQualifier
        {
            get
            {
                return this._textQualifier;
            }
            set
            {
                if (this._textQualifier != value)
                {
                    this._textQualifier = value;
                    base.OnChanged();
                }
            }
        }
    }
}

