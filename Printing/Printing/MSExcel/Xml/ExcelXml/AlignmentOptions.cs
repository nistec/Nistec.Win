namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class AlignmentOptions : IAlignmentOptions
    {
        [CompilerGenerated]
        private int _Indent;
        [CompilerGenerated]
        private int _Rotate;
        [CompilerGenerated]
        private bool _ShrinkToFit;
        [CompilerGenerated]
        private bool _WrapText;
        private HorizontalAlignment horizontal;
        private VerticalAlignment vertical;

        public AlignmentOptions()
        {
            this.horizontal = HorizontalAlignment.None;
            this.vertical = VerticalAlignment.None;
            this.Indent = 0;
            this.Rotate = 0;
            this.WrapText = false;
            this.ShrinkToFit = false;
        }

        public AlignmentOptions(AlignmentOptions ao)
        {
            this.horizontal = ao.Horizontal;
            this.vertical = ao.Vertical;
            this.Indent = ao.Indent;
            this.Rotate = ao.Rotate;
            this.WrapText = ao.WrapText;
            this.ShrinkToFit = ao.ShrinkToFit;
        }

        internal bool CheckForMatch(AlignmentOptions other)
        {
            return (((((this.Vertical == other.Vertical) && (this.Horizontal == other.Horizontal)) && ((this.Indent == other.Indent) && (this.Rotate == other.Rotate))) && (this.WrapText == other.WrapText)) && (this.ShrinkToFit == other.ShrinkToFit));
        }

        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("Alignment");
            if (this.Vertical != VerticalAlignment.None)
            {
                writer.WriteAttributeString("ss", "Vertical", null, this.Vertical.ToString());
            }
            if (this.Horizontal != HorizontalAlignment.None)
            {
                writer.WriteAttributeString("ss", "Horizontal", null, this.Horizontal.ToString());
            }
            if (this.WrapText)
            {
                writer.WriteAttributeString("ss", "WrapText", null, "1");
            }
            if (this.ShrinkToFit)
            {
                writer.WriteAttributeString("ss", "ShrinkToFit", null, "1");
            }
            if (this.Indent > 0)
            {
                writer.WriteAttributeString("ss", "Indent", null, this.Indent.ToString(CultureInfo.InvariantCulture));
            }
            if (this.Rotate > 0)
            {
                writer.WriteAttributeString("ss", "Rotate", null, this.Rotate.ToString(CultureInfo.InvariantCulture));
            }
            writer.WriteEndElement();
        }

        internal void Read(XmlReader reader)
        {
            //reader.MoveToFirstAttribute();

            while (reader.MoveToNextAttribute()) //foreach (XmlReaderAttributeItem item in reader.GetAttributes())
            {
                int num;
                string localName = reader.LocalName;
                if (localName != null)
                {
                    if (!(localName == "Vertical"))
                    {
                        if (localName == "Horizontal")
                        {
                            goto Label_0099;
                        }
                        if (localName == "WrapText")
                        {
                            goto Label_00B1;
                        }
                        if (localName == "ShrinkToFit")
                        {
                            goto Label_00D1;
                        }
                        if (localName == "Indent")
                        {
                            goto Label_00F1;
                        }
                        if (localName == "Rotate")
                        {
                            goto Label_0112;
                        }
                    }
                    else
                    {
                        this.Vertical = (VerticalAlignment)Enum.Parse(typeof(VerticalAlignment), reader.Value, true);//ObjectExtensions.ParseEnum<VerticalAlignment>(reader.Value);
                    }
                }
                goto Label_0133;
            Label_0099:
                this.Horizontal = (HorizontalAlignment)Enum.Parse(typeof(HorizontalAlignment), reader.Value, true);//ObjectExtensions.ParseEnum<HorizontalAlignment>(reader.Value);
                goto Label_0133;
            Label_00B1:
                this.WrapText = reader.Value == "1";
                goto Label_0133;
            Label_00D1:
                this.ShrinkToFit = reader.Value == "1";
                goto Label_0133;
            Label_00F1:
                if (int.TryParse(reader.Value, out num))// item.Value.ParseToInt<int>(out num))
                {
                    this.Indent = num;
                }
                goto Label_0133;
            Label_0112:
                if (int.TryParse(reader.Value, out num))// item.Value.ParseToInt<int>(out num))
                {
                    this.Rotate = num;
                }
            Label_0133:;
            }
        }

        public HorizontalAlignment Horizontal
        {
            get
            {
                return this.horizontal;
            }
            set
            {
                this.horizontal = value;
                if (! Enum.IsDefined(horizontal.GetType(), this.horizontal))
                {
                    throw new ArgumentException("Invalid horizontal alignment value encountered");
                }
            }
        }

        public int Indent
        {
            [CompilerGenerated]
            get
            {
                return this._Indent;
            }
            [CompilerGenerated]
            set
            {
                this._Indent = value;
            }
        }

        public int Rotate
        {
            [CompilerGenerated]
            get
            {
                return this._Rotate;
            }
            [CompilerGenerated]
            set
            {
                this._Rotate = value;
            }
        }

        public bool ShrinkToFit
        {
            [CompilerGenerated]
            get
            {
                return this._ShrinkToFit;
            }
            [CompilerGenerated]
            set
            {
                this._ShrinkToFit = value;
            }
        }

        public VerticalAlignment Vertical
        {
            get
            {
                return this.vertical;
            }
            set
            {
                this.vertical = value;
                if (!Enum.IsDefined(vertical.GetType(), this.vertical))
                {
                    throw new ArgumentException("Invalid vertical alignment value encountered");
                }
            }
        }

        public bool WrapText
        {
            [CompilerGenerated]
            get
            {
                return this._WrapText;
            }
            [CompilerGenerated]
            set
            {
                this._WrapText = value;
            }
        }
    }
}

