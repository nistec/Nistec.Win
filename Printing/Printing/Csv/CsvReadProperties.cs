namespace Nistec.Printing.Csv
{
    using Nistec.Printing.Data;
    using Nistec.Printing;
    using System;
    using System.Xml;

    public class CsvReadProperties : AdoProperties//, IAdoReadProperties
    {
        private bool _archiveAfterRead = false;
        private string _archiveFolder = "";
        private string _fieldDelimiter = ",";
        private FileTypes _fileType = FileTypes.Delimited;
        private bool _includeFilename = false;
        private bool _multipleFiles = false;
        private RecordSeparators _recordSeparator = RecordSeparators.Windows;
        private string _textQualifier = "\"";
        private bool _verticalRecords = false;

        public CsvReadProperties()
        {
            _DataSource = new AdoTable();
        }

        public override AdoProperties Clone()
        {
            CsvReadProperties properties = new CsvReadProperties();
            properties.Filename = this._filename;
            properties.FileType = this._fileType;
            properties.TextQualifier = this._textQualifier;
            properties.FieldDelimiter = this._fieldDelimiter;
            properties.RecordSeparator = this._recordSeparator;
            properties.FirstRowHeaders = this._firstRowHeaders;
            properties.Encoding = this._encoding;
            properties.MultipleFiles = this._multipleFiles;
            properties.VerticalRecords = this._verticalRecords;
            properties.IncludeFilename = this._includeFilename;
            properties.ArchiveAfterRead = this._archiveAfterRead;
            properties.ArchiveFolder = this._archiveFolder;
            properties.DataSource = this._DataSource.Clone();
            return properties;
        }

 
        public bool ArchiveAfterRead
        {
            get
            {
                return this._archiveAfterRead;
            }
            set
            {
                if (this._archiveAfterRead != value)
                {
                    this._archiveAfterRead = value;
                    base.OnChanged();
                }
            }
        }

        public string ArchiveFolder
        {
            get
            {
                return this._archiveFolder;
            }
            set
            {
                if (this._archiveFolder != value)
                {
                    this._archiveFolder = value;
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

        public bool IncludeFilename
        {
            get
            {
                return this._includeFilename;
            }
            set
            {
                if (this._includeFilename != value)
                {
                    this._includeFilename = value;
                    base.OnChanged();
                }
            }
        }

        public bool MultipleFiles
        {
            get
            {
                return this._multipleFiles;
            }
            set
            {
                if (this._multipleFiles != value)
                {
                    this._multipleFiles = value;
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

