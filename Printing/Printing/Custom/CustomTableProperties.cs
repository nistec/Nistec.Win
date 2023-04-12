namespace Nistec.Printing.Custom
{
    using System;
    using System.Xml;
    using Nistec.Printing.Data;

    public class TableProperties : AdoProperties
    {
        private AdoTable _schema = new AdoTable();

        public override AdoProperties Clone()
        {
            TableProperties properties = new TableProperties();
            properties.DataSource = this._schema.Clone();
            return properties;
        }

        public AdoTable DataSource
        {
            get
            {
                return this._schema;
            }
            set
            {
                if (this._schema != value)
                {
                    this._schema = value;
                    base.OnChanged();
                }
            }
        }
    }
}

