namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class FontOptions : IFontOptions
    {
        [CompilerGenerated]
        private bool _Bold;
        [CompilerGenerated]
        private System.Drawing.Color _Color;
        [CompilerGenerated]
        private bool _Italic;
        [CompilerGenerated]
        private string _Name;
        [CompilerGenerated]
        private int _Size;
        [CompilerGenerated]
        private bool _Strikeout;
        [CompilerGenerated]
        private bool _Underline;

        public FontOptions()
        {
            this.Name = "Tahoma";
            this.Size = 0;
            this.Bold = false;
            this.Underline = false;
            this.Italic = false;
            this.Strikeout = false;
            this.Color = System.Drawing.Color.Black;
        }

        public FontOptions(FontOptions fo)
        {
            this.Name = fo.Name;
            this.Size = fo.Size;
            this.Bold = fo.Bold;
            this.Underline = fo.Underline;
            this.Italic = fo.Italic;
            this.Strikeout = fo.Strikeout;
            this.Color = fo.Color;
        }

        internal bool CheckForMatch(FontOptions other)
        {
            return (((((this.Name == other.Name) && (this.Size == other.Size)) && ((this.Bold == other.Bold) && (this.Underline == other.Underline))) && ((this.Italic == other.Italic) && (this.Strikeout == other.Strikeout))) && (this.Color == other.Color));
        }

        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("Font");
            writer.WriteAttributeString("ss", "FontName", null, this.Name);
            if (this.Size != 0)
            {
                writer.WriteAttributeString("ss", "Size", null, this.Size.ToString(CultureInfo.InvariantCulture));
            }
            writer.WriteAttributeString("ss", "Color", null, XmlStyle.ColorToExcelFormat(this.Color));
            if (this.Bold)
            {
                writer.WriteAttributeString("ss", "Bold", null, "1");
            }
            if (this.Italic)
            {
                writer.WriteAttributeString("ss", "Italic", null, "1");
            }
            if (this.Underline)
            {
                writer.WriteAttributeString("ss", "Underline", null, "Single");
            }
            if (this.Strikeout)
            {
                writer.WriteAttributeString("ss", "Strikeout", null, "1");
            }
            writer.WriteEndElement();
        }

        internal void Read(XmlReader reader)
        {
            //reader.MoveToFirstAttribute();

            while (reader.MoveToNextAttribute()) //foreach (XmlReaderAttributeItem item in reader.GetAttributes())
            {
                switch (reader.LocalName)
                {
                    case "FontName":
                        this.Name = reader.Value;
                        break;

                    case "Size":
                        int num;
                        if (int.TryParse(reader.Value, out num))// item.Value.ParseToInt<int>(out num))
                        {
                            this.Size = num;
                        }
                        break;

                    case "Color":
                        this.Color = XmlStyle.ExcelFormatToColor(reader.Value);
                        break;

                    case "Bold":
                        this.Bold = reader.Value == "1";
                        break;

                    case "Italic":
                        this.Italic = reader.Value == "1";
                        break;

                    case "Underline":
                        this.Underline = reader.Value == "Single";
                        break;

                    case "Strikeout":
                        this.Strikeout = reader.Value == "1";
                        break;
                }
            }
        }

        public bool Bold
        {
            [CompilerGenerated]
            get
            {
                return this._Bold;
            }
            [CompilerGenerated]
            set
            {
                this._Bold = value;
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

        public bool Italic
        {
            [CompilerGenerated]
            get
            {
                return this._Italic;
            }
            [CompilerGenerated]
            set
            {
                this._Italic = value;
            }
        }

        public string Name
        {
            [CompilerGenerated]
            get
            {
                return this._Name;
            }
            [CompilerGenerated]
            set
            {
                this._Name = value;
            }
        }

        public int Size
        {
            [CompilerGenerated]
            get
            {
                return this._Size;
            }
            [CompilerGenerated]
            set
            {
                this._Size = value;
            }
        }

        public bool Strikeout
        {
            [CompilerGenerated]
            get
            {
                return this._Strikeout;
            }
            [CompilerGenerated]
            set
            {
                this._Strikeout = value;
            }
        }

        public bool Underline
        {
            [CompilerGenerated]
            get
            {
                return this._Underline;
            }
            [CompilerGenerated]
            set
            {
                this._Underline = value;
            }
        }
    }
}

