namespace Nistec.Printing.Clipboard
{
    using Nistec.Printing;
    using System;
    using System.Xml;
    using Nistec.Printing.Csv;
    using Nistec.Printing.Data;

    public class ClipboardReadProperties : AdoProperties
    {
        //private string _encoding = "UTF-8";
        private string _fieldDelimiter = ",";
        private FileTypes _fileType = FileTypes.Delimited;
        //private bool _firstRowHeaders = false;
        private RecordSeparators _recordSeparator = RecordSeparators.Windows;
        //private AdoTable _DataSource = new AdoTable();
        private string _textQualifier = "\"";
        private bool _verticalRecords = false;

        public ClipboardReadProperties()
        {
            _DataSource = new AdoTable();
        }

        public override AdoProperties Clone()
        {
            ClipboardReadProperties properties = new ClipboardReadProperties();
            properties.FileType = this._fileType;
            properties.TextQualifier = this._textQualifier;
            properties.FieldDelimiter = this._fieldDelimiter;
            properties.RecordSeparator = this._recordSeparator;
            properties.FirstRowHeaders = this._firstRowHeaders;
            properties.Encoding = this._encoding;
            properties.VerticalRecords = this._verticalRecords;
            properties.DataSource = this._DataSource.Clone();
            return properties;
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

        public bool VerticalRecords
        {
            get
            {
                return this._verticalRecords;
            }
            set
            {
                if (this._verticalRecords != value)
                {
                    this._verticalRecords = value;
                    base.OnChanged();
                }
            }
        }
    }
}

