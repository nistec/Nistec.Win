namespace Nistec.Printing.Xml
{
    using Nistec.Printing;
    using System;
    using System.Xml;
    using Nistec.Printing.Csv;
    using Nistec.Printing.Data;

    public class XmlReadProperties : AdoProperties
    {
        //private string _filename = "";
        //private string _encoding = "UTF-8";
        private string _tablename = "";
        //private AdoTable _DataSource = new AdoTable();

        public XmlReadProperties()
        {
            _DataSource = new AdoTable();
        }

        public override AdoProperties Clone()
        {
            XmlReadProperties properties = new XmlReadProperties();
            properties.Filename = this._filename;
            properties.TableName = this._tablename;
            properties.Encoding = this._encoding;
            properties.DataSource = this._DataSource.Clone();
            return properties;
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

        public string TableName
        {
            get
            {
                return this._tablename;
            }
            set
            {
                if (this._tablename != value)
                {
                    this._tablename = value;
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

