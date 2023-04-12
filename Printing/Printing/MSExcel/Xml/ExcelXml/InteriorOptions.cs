namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class InteriorOptions : IInteriorOptions
    {
        [CompilerGenerated]
        private System.Drawing.Color _Color;

        public InteriorOptions()
        {
            this.Color = System.Drawing.Color.White;
        }

        public InteriorOptions(InteriorOptions io)
        {
            this.Color = io.Color;
        }

        internal bool CheckForMatch(InteriorOptions other)
        {
            return (this.Color == other.Color);
        }

        internal void Write(XmlWriter writer)
        {
            if (this.Color != System.Drawing.Color.White)
            {
                writer.WriteStartElement("Interior");
                writer.WriteAttributeString("ss", "Color", null, XmlStyle.ColorToExcelFormat(this.Color));
                writer.WriteAttributeString("ss", "Pattern", null, "Solid");
                writer.WriteEndElement();
            }
        }

        internal void Read(XmlReader reader)
        {
            //reader.MoveToFirstAttribute();

            while (reader.MoveToNextAttribute()) //foreach (XmlReaderAttributeItem item in reader.GetAttributes())
            {
                string localName = reader.LocalName;
                if ((localName != null) && (localName == "Color"))
                {
                    this.Color = XmlStyle.ExcelFormatToColor(reader.Value);
                }
            }
        }

        public System.Drawing.Color Color
        {
            [CompilerGenerated]
            get
            {
                return this._Color;
            }
            [CompilerGenerated]
            set
            {
                this._Color = value;
            }
        }
    }
}

