namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class XmlStyle : IStyle
    {
        [CompilerGenerated]
        private AlignmentOptions __Alignment;
        [CompilerGenerated]
        private BorderOptions __Border;
        [CompilerGenerated]
        private FontOptions __Font;
        [CompilerGenerated]
        private InteriorOptions __Interior;
        [CompilerGenerated]
        private string _ID;
        private DisplayFormat displayFormat;

        public XmlStyle()
        {
            this.Initialize();
            this.SetDefaults();
        }

        public XmlStyle(XmlStyle style)
        {
            if (style == null)
            {
                this.Initialize();
                this.SetDefaults();
            }
            else
            {
                this.ID = "";
                this._Font = new FontOptions(style._Font);
                this._Interior = new InteriorOptions(style._Interior);
                this._Alignment = new AlignmentOptions(style._Alignment);
                this._Border = new BorderOptions(style._Border);
                this.DisplayFormat = style.DisplayFormat;
            }
        }

        internal bool CheckForMatch(XmlStyle style)
        {
            if (style == null)
            {
                return false;
            }
            return (((this._Font.CheckForMatch(style._Font) && this._Alignment.CheckForMatch(style._Alignment)) && (this._Interior.CheckForMatch(style._Interior) && this._Border.CheckForMatch(style._Border))) && (this.DisplayFormat == style.DisplayFormat));
        }

        internal static string ColorToExcelFormat(Color color)
        {
            return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", new object[] { color.R, color.G, color.B });
        }

        public override bool Equals(object obj)
        {
            return ((obj is XmlStyle) && this.CheckForMatch((XmlStyle) obj));
        }

        internal static Color ExcelFormatToColor(string str)
        {
            int num;
            int num2;
            int num3;
            if ((int.TryParse(str.Substring(1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num) && int.TryParse(str.Substring(3, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2)) && int.TryParse(str.Substring(5, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num3))
            {
                return Color.FromArgb(num, num2, num3);
            }
            return Color.Black;
        }

        internal static DisplayFormat StrintToDisplayFormat(string format)
        {
            switch (format.ToLower())
            {
                case "general date":
                    return DisplayFormat.GeneralDate;
                case "short date":
                    return DisplayFormat.ShortDate;
                case "@":
                    return DisplayFormat.Text;
                default:
                    if (format.Contains("am/pm"))
                        return DisplayFormat.Time;
                    else
                        return (DisplayFormat)Enum.Parse(typeof(DisplayFormat), format, true);
            }
        }

        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("Style");
            writer.WriteAttributeString("ss", "ID", null, this.ID);
            this._Font.Write(writer);
            this._Alignment.Write(writer);
            this._Border.Write(writer);
            this._Interior.Write(writer);
            if (this.DisplayFormat != DisplayFormat.None)
            {
                string pattern = "";
                
                switch (this.DisplayFormat)
                {
                    case DisplayFormat.Text:
                        pattern = "@";
                        break;

                    case DisplayFormat.Fixed:
                    case DisplayFormat.Standard:
                    case DisplayFormat.Percent:
                    case DisplayFormat.Scientific:
                    case DisplayFormat.Currency:
                        pattern = this.DisplayFormat.ToString();
                        break;

                    case DisplayFormat.GeneralDate:
                        pattern = "General Date";
                        break;

                    case DisplayFormat.ShortDate:
                        pattern = "Short Date";
                        break;

                    case DisplayFormat.LongDate:
                        pattern = DateTimeFormatInfo.CurrentInfo.LongDatePattern;
                        break;

                    case DisplayFormat.Time:
                        pattern = DateTimeFormatInfo.CurrentInfo.LongTimePattern;
                        if (pattern.Contains("t"))
                        {
                            pattern = pattern.Replace("t", "AM/PM");
                        }
                        if (pattern.Contains("tt"))
                        {
                            pattern = pattern.Replace("tt", "AM/PM");
                        }
                        break;
                }
                writer.WriteStartElement("NumberFormat");
                writer.WriteAttributeString("ss", "Format", null, pattern);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal static XmlStyle Read(XmlReader reader)
        {
            XmlStyle style = new XmlStyle();
            bool isEmptyElement = reader.IsEmptyElement;
            //XmlReaderAttributeItem singleAttribute = reader.GetSingleAttribute("ID");
            string singleAttribute = reader.GetAttribute("ss:ID");
            if (singleAttribute != null)
            {
                style.ID = singleAttribute;
            }
            if (isEmptyElement)
            {
                return ((singleAttribute == null) ? null : style);
            }
            while (reader.Read() && (!(reader.Name == "Style") || (reader.NodeType != XmlNodeType.EndElement)))
            {
                //XmlReaderAttributeItem item2;
                string item2;
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string name = reader.Name;

                    if (name != null)
                    {
                        switch (name)
                        {
                            case "Alignment":
                                style._Alignment.Read(reader);
                                break;
                            case "Interior":
                                style._Interior.Read(reader);
                                break;
                            case "Borders":
                                style._Border.Read(reader);
                                break;
                            case "NumberFormat":
                                item2 = reader.GetAttribute("ss:Format");
                                if (item2 != null)
                                {
                                    style.DisplayFormat = StrintToDisplayFormat(item2);// (DisplayFormat)Enum.Parse(typeof(DisplayFormat), item2, true);// ObjectExtensions.ParseEnum<DisplayFormat>(item2);
                                }
                                break;
                            case "Font":
                                style._Font.Read(reader);
                                break;

                        }
                    }
                }
             }
            return style;
        }

        private void Initialize()
        {
            this._Font = new FontOptions();
            this._Interior = new InteriorOptions();
            this._Alignment = new AlignmentOptions();
            this._Border = new BorderOptions();
        }

        //public static bool operator ==(XmlStyle cellOne, XmlStyle cellTwo)
        //{
        //    if (cellOne == null)
        //    {
        //        return (cellTwo == null);
        //    }
        //    return cellOne.Equals(cellTwo);
        //}

        //public static bool operator !=(XmlStyle cellOne, XmlStyle cellTwo)
        //{
        //    if (cellOne == null)
        //    {
        //        return (cellTwo != null);
        //    }
        //    return !cellOne.Equals(cellTwo);
        //}

        private void SetDefaults()
        {
            this.ID = "";
            this.DisplayFormat = DisplayFormat.None;
        }
        public Type GetFormatType()
        {
            switch (this.displayFormat)
            {
                case DisplayFormat.Fixed:
                    return typeof(int);
                case DisplayFormat.Standard:
                case DisplayFormat.Currency:
                    return typeof(decimal);
                case DisplayFormat.Percent:
                case DisplayFormat.Scientific:
                    return typeof(float);
                case DisplayFormat.GeneralDate:
                case DisplayFormat.LongDate:
                case DisplayFormat.ShortDate:
                case DisplayFormat.Time:
                    return typeof(DateTime);
                default:
                    return typeof(string);

            }
        }
        private AlignmentOptions _Alignment
        {
            [CompilerGenerated]
            get
            {
                return this.__Alignment;
            }
            [CompilerGenerated]
            set
            {
                this.__Alignment = value;
            }
        }

        private BorderOptions _Border
        {
            [CompilerGenerated]
            get
            {
                return this.__Border;
            }
            [CompilerGenerated]
            set
            {
                this.__Border = value;
            }
        }

        private FontOptions _Font
        {
            [CompilerGenerated]
            get
            {
                return this.__Font;
            }
            [CompilerGenerated]
            set
            {
                this.__Font = value;
            }
        }

        private InteriorOptions _Interior
        {
            [CompilerGenerated]
            get
            {
                return this.__Interior;
            }
            [CompilerGenerated]
            set
            {
                this.__Interior = value;
            }
        }

        public IAlignmentOptions Alignment
        {
            get
            {
                return this._Alignment;
            }
            set
            {
                this._Alignment = (AlignmentOptions) value;
            }
        }

        public IBorderOptions Border
        {
            get
            {
                return this._Border;
            }
            set
            {
                this._Border = (BorderOptions) value;
            }
        }

        public DisplayFormat DisplayFormat
        {
            get
            {
                return this.displayFormat;
            }
            set
            {
                this.displayFormat = value;
                if (!Enum.IsDefined(displayFormat.GetType(), this.displayFormat))
                {
                    throw new ArgumentException("Invalid display format value encountered");
                }
            }
        }

        public IFontOptions Font
        {
            get
            {
                return this._Font;
            }
            set
            {
                this._Font = (FontOptions) value;
            }
        }

        internal string ID
        {
            [CompilerGenerated]
            get
            {
                return this._ID;
            }
            [CompilerGenerated]
            set
            {
                this._ID = value;
            }
        }

        public IInteriorOptions Interior
        {
            get
            {
                return this._Interior;
            }
            set
            {
                this._Interior = (InteriorOptions) value;
            }
        }
    }
}

