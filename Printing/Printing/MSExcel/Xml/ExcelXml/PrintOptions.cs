namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Xml;

    public class PrintOptions
    {
        internal double BottomMargin;
        internal int BottomPrintRow;
        internal int FitHeight;
        internal bool FitToPage;
        internal int FitWidth;
        internal double FooterMargin;
        internal double HeaderMargin;
        private PageLayout layout;
        internal double LeftMargin;
        internal int LeftPrintCol;
        private PageOrientation orientation;
        internal bool PrintTitles;
        internal double RightMargin;
        internal int RightPrintCol;
        internal int Scale;
        internal double TopMargin;
        internal int TopPrintRow;

        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("PageSetup");
            if (this.Orientation != PageOrientation.None)
            {
                writer.WriteStartElement("Layout");
                writer.WriteAttributeString("", "Orientation", null, this.Orientation.ToString());
                writer.WriteEndElement();
            }
            writer.WriteStartElement("Header");
            writer.WriteAttributeString("", "Margin", null, this.HeaderMargin.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();
            writer.WriteStartElement("Footer");
            writer.WriteAttributeString("", "Margin", null, this.FooterMargin.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();
            writer.WriteStartElement("PageMargins");
            writer.WriteAttributeString("", "Bottom", null, this.BottomMargin.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("", "Left", null, this.LeftMargin.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("", "Right", null, this.RightMargin.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString("", "Top", null, this.TopMargin.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();
            writer.WriteEndElement();
            if (this.FitToPage)
            {
                writer.WriteStartElement("FitToPage");
                writer.WriteEndElement();
            }
            writer.WriteStartElement("Print");
            writer.WriteElementString("ValidPrinterInfo", "");
            writer.WriteElementString("FitHeight", this.FitHeight.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("FitWidth", this.FitWidth.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("Scale", this.Scale.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();
        }

        internal string GetPrintTitleRange(string workSheetName)
        {
            StringBuilder builder = new StringBuilder();
            if (this.PrintTitles)
            {
                if (this.LeftPrintCol != 0)
                {
                    builder.AppendFormat("'{0}'!C{1}", workSheetName, this.LeftPrintCol);
                    if (this.RightPrintCol != this.LeftPrintCol)
                    {
                        builder.AppendFormat(":C{0}", this.RightPrintCol);
                    }
                }
                if (this.TopPrintRow != 0)
                {
                    if (this.LeftPrintCol != 0)
                    {
                        builder.Append(',');
                    }
                    builder.AppendFormat("'{0}'!R{1}", workSheetName, this.TopPrintRow);
                    if (this.BottomPrintRow != this.TopPrintRow)
                    {
                        builder.AppendFormat(":R{0}", this.BottomPrintRow);
                    }
                }
            }
            return builder.ToString();
        }

        public void ResetHeaders()
        {
            this.TopPrintRow = 0;
            this.BottomPrintRow = 0;
            this.LeftPrintCol = 0;
            this.RightPrintCol = 0;
        }

        public void ResetMargins()
        {
            this.LeftMargin = 0.7;
            this.RightMargin = 0.7;
            this.TopMargin = 0.75;
            this.BottomMargin = 0.75;
            this.HeaderMargin = 0.3;
            this.FooterMargin = 0.3;
        }

        public void SetFitToPage(int width, int height)
        {
            this.FitWidth = width;
            this.FitHeight = height;
            this.FitToPage = true;
        }

        public void SetHeaderFooterMargins(double header, double footer)
        {
            this.HeaderMargin = Math.Max(0.0, header);
            this.FooterMargin = Math.Max(0.0, footer);
        }

        public void SetMargins(double left, double top, double right, double bottom)
        {
            this.LeftMargin = Math.Max(0.0, left);
            this.TopMargin = Math.Max(0.0, top);
            this.RightMargin = Math.Max(0.0, right);
            this.BottomMargin = Math.Max(0.0, bottom);
        }

        public void SetScaleToSize(int scale)
        {
            this.Scale = scale;
            this.FitToPage = false;
        }

        public void SetTitleColumns(int left, int right)
        {
            this.LeftPrintCol = Math.Max(1, left);
            this.RightPrintCol = Math.Max(1, Math.Max(left, right));
            this.PrintTitles = true;
        }

        public void SetTitleRows(int top, int bottom)
        {
            this.TopPrintRow = Math.Max(1, top);
            this.BottomPrintRow = Math.Max(1, Math.Max(top, bottom));
            this.PrintTitles = true;
        }

        public PageLayout Layout
        {
            get
            {
                return this.layout;
            }
            set
            {
                this.layout = value;
                if (!Enum.IsDefined(layout.GetType(), this.layout))
                {
                    throw new ArgumentException("Invalid page layout defined");
                }
            }
        }

        public PageOrientation Orientation
        {
            get
            {
                return this.orientation;
            }
            set
            {
                this.orientation = value;
                if (!Enum.IsDefined(orientation.GetType(), this.orientation))
                {
                    throw new ArgumentException("Invalid page layout defined");
                }
            }
        }
    }
}

