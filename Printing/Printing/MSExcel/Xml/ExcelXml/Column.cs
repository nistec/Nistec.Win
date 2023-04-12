namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class Column
    {
        [CompilerGenerated]
        private bool _Hidden;
        [CompilerGenerated]
        private double _Width;
        private Workbook ParentBook;
        private string styleID;

        internal Column(Worksheet parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.ParentBook = parent.ParentBook;
        }

        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("Column");
            if (this.Width > 0.0)
            {
                writer.WriteAttributeString("ss", "Width", null, this.Width.ToString(CultureInfo.InvariantCulture));
            }
            if (this.Hidden)
            {
                writer.WriteAttributeString("ss", "Hidden", null, "1");
                writer.WriteAttributeString("ss", "AutoFitWidth", null, "0");
            }
            writer.WriteEndElement();
        }

        public bool Hidden
        {
            [CompilerGenerated]
            get
            {
                return this._Hidden;
            }
            [CompilerGenerated]
            set
            {
                this._Hidden = value;
            }
        }

        public XmlStyle Style
        {
            get
            {
                return this.ParentBook.GetStyleByID(this.styleID);
            }
            set
            {
                this.styleID = this.ParentBook.AddStyle(value);
            }
        }

        public double Width
        {
            [CompilerGenerated]
            get
            {
                return this._Width;
            }
            [CompilerGenerated]
            set
            {
                this._Width = value;
            }
        }
    }
}

