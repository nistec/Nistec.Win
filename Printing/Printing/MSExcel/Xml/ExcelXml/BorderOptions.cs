namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class BorderOptions : IBorderOptions
    {
        [CompilerGenerated]
        private System.Drawing.Color _Color;
        [CompilerGenerated]
        private int _Weight;
        private Borderline lineStyle;
        private BorderSides sides;

        public BorderOptions()
        {
            this.sides = BorderSides.None;
            this.lineStyle = Borderline.Continuous;
            this.Weight = 1;
            this.Color = System.Drawing.Color.Black;
        }

        public BorderOptions(BorderOptions borderOptions)
        {
            this.sides = borderOptions.Sides;
            this.lineStyle = borderOptions.LineStyle;
            this.Weight = borderOptions.Weight;
            this.Color = borderOptions.Color;
        }

        internal bool CheckForMatch(BorderOptions other)
        {
            return ((((this.Sides == other.Sides) && (this.LineStyle == other.LineStyle)) && (this.Weight == other.Weight)) && (this.Color == other.Color));
        }

        internal void Write(XmlWriter writer)
        {
            if (this.Sides != BorderSides.None)
            {
                
                writer.WriteStartElement("Borders");

                string str = this.Sides.ToString("G");

                if (str.Contains("Left"))//(this.Sides.IsFlagSet(BorderSides.Left))
                {
                    this.WriteBorder(writer, "Left");
                }
                if (str.Contains("Top"))//(this.Sides.IsFlagSet(BorderSides.Top))
                {
                    this.WriteBorder(writer, "Top");
                }
                if (str.Contains("Right"))//(this.Sides.IsFlagSet(BorderSides.Right))
                {
                    this.WriteBorder(writer, "Right");
                }
                if (str.Contains("Bottom"))//(this.Sides.IsFlagSet(BorderSides.Bottom))
                {
                    this.WriteBorder(writer, "Bottom");
                }
                writer.WriteEndElement();
            }
        }

        private void WriteBorder(XmlWriter writer, string border)
        {
            writer.WriteStartElement("Border");
            writer.WriteAttributeString("ss", "Position", null, border);
            writer.WriteAttributeString("ss", "LineStyle", null, this.LineStyle.ToString());
            writer.WriteAttributeString("ss", "Weight", null, this.Weight.ToString(CultureInfo.InvariantCulture));
            if (this.Color != System.Drawing.Color.Black)
            {
                writer.WriteAttributeString("ss", "Color", null, XmlStyle.ColorToExcelFormat(this.Color));
            }
            writer.WriteEndElement();
        }

        internal void Read(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                while (reader.Read() && (!(reader.Name == "Borders") || (reader.NodeType != XmlNodeType.EndElement)))
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Border"))
                    {
                        //reader.MoveToFirstAttribute();

                        while (reader.MoveToNextAttribute()) //foreach (XmlReaderAttributeItem item in reader.GetAttributes())
                        {
                            int num;
                            string localName = reader.LocalName;
                            if (localName != null)
                            {
                                if (!(localName == "Position"))
                                {
                                    if (localName == "LineStyle")
                                    {
                                        goto Label_00B6;
                                    }
                                    if (localName == "Weight")
                                    {
                                        goto Label_00CB;
                                    }
                                }
                                else
                                {
                                    this.Sides |= (BorderSides)Enum.Parse(typeof(BorderSides), reader.Value, true);//ObjectExtensions.ParseEnum<BorderSides>(reader.Value);
                                   
                                }
                            }
                            goto Label_00EA;
                        Label_00B6:
                            this.LineStyle = (Borderline)Enum.Parse(typeof(Borderline), reader.Value, true);//ObjectExtensions.ParseEnum<Borderline>(reader.Value);
                            goto Label_00EA;
                        Label_00CB:
                            if (int.TryParse(reader.Value, out num))// item.Value.ParseToInt<int>(out num))
                            {
                                this.Weight = num;
                            }
                        Label_00EA:;
                        }
                    }
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

        public Borderline LineStyle
        {
            get
            {
                return this.lineStyle;
            }
            set
            {
                this.lineStyle = value;
                if (!Enum.IsDefined(lineStyle.GetType(),this.lineStyle))
                {
                    throw new ArgumentException("Invalid line style value encountered");
                }
            }
        }

        public BorderSides Sides
        {
            get
            {
                return this.sides;
            }
            set
            {
                this.sides = value;
                //if (!Enum.IsDefined(sides.GetType(), this.sides))
                //{
                //    throw new ArgumentException("Invalid Border side value encountered");
                //}
            }
        }

        public int Weight
        {
            [CompilerGenerated]
            get
            {
                return this._Weight;
            }
            [CompilerGenerated]
            set
            {
                this._Weight = value;
            }
        }
    }
}

