namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using System;
    using System.Drawing;

    public class Row
    {
        private float _b0;
        private MControl.Printing.Pdf.Drawing.Cells _b1;
        private bool _b2;
        private MControl.Printing.Pdf.Drawing.Table _b3;
        private TableStyle _b4;
        internal int A474;

        public Row(MControl.Printing.Pdf.Drawing.Table table)
        {
            this._b3 = table;
            this._b4 = table.A405;
        }

        public Row(MControl.Printing.Pdf.Drawing.Table table, TableStyle style)
        {
            this._b3 = table;
            this._b4 = style;
            this.b5();
        }

        private void b5()
        {
            this._b0 = 0f;
            this._b2 = true;
            this.A474 = 0;
            this._b1 = new MControl.Printing.Pdf.Drawing.Cells(this);
        }

        internal TableStyle A405
        {
            get
            {
                return this._b4;
            }
        }

        internal bool A482
        {
            get
            {
                return this._b3.A482;
            }
        }

        public PdfColor BackColor
        {
            get
            {
                return this._b4.BackColor;
            }
            set
            {
                if (!this.A482 && (this._b4.BackColor != value))
                {
                    this._b4 = TableStyle.A483(this._b4, value, this);
                }
            }
        }

        public MControl.Printing.Pdf.Border Border
        {
            get
            {
                return this._b4.Border;
            }
            set
            {
                if (!this.A482 && (this._b4.Border != value))
                {
                    this._b4 = TableStyle.A484(this._b4, value, this);
                }
            }
        }

        public MControl.Printing.Pdf.Drawing.Cells Cells
        {
            get
            {
                return this._b1;
            }
        }

        public ContentAlignment ContentAlign
        {
            get
            {
                return this._b4.ContentAlign;
            }
            set
            {
                if (!this.A482 && (this._b4.ContentAlign != value))
                {
                    this._b4 = TableStyle.A490(this._b4, value, this);
                }
            }
        }

        public PdfFont Font
        {
            get
            {
                return this._b4.Font;
            }
            set
            {
                if (!this.A482 && (this._b4.Font != value))
                {
                    this._b4 = TableStyle.A157(this._b4, value, this);
                }
            }
        }

        public float FontSize
        {
            get
            {
                return this._b4.FontSize;
            }
            set
            {
                if (!this.A482 && (this._b4.FontSize != value))
                {
                    this._b4 = TableStyle.A486(this._b4, value, this);
                }
            }
        }

        public float Height
        {
            get
            {
                return this._b0;
            }
            set
            {
                this._b0 = value;
            }
        }

        public bool KeepTogether
        {
            get
            {
                return this._b2;
            }
            set
            {
                this._b2 = value;
            }
        }

        public float LineSpace
        {
            get
            {
                return this._b4.LineSpace;
            }
            set
            {
                if (!this.A482 && (this._b4.LineSpace != value))
                {
                    this._b4 = TableStyle.A487(this._b4, value, this);
                }
            }
        }

        public bool RightToLeft
        {
            get
            {
                return this._b4.RightToLeft;
            }
            set
            {
                if (!this.A482 && (this._b4.RightToLeft != value))
                {
                    this._b4 = TableStyle.A491(this._b4, value, this);
                }
            }
        }

        public MControl.Printing.Pdf.Drawing.Table Table
        {
            get
            {
                return this._b3;
            }
        }

        public TextAlignment TextAlign
        {
            get
            {
                return this._b4.TextAlign;
            }
            set
            {
                if (!this.A482 && (this._b4.TextAlign != value))
                {
                    this._b4 = TableStyle.A489(this._b4, value, this);
                }
            }
        }

        public PdfColor TextColor
        {
            get
            {
                return this._b4.TextColor;
            }
            set
            {
                if (!this.A482 && (this._b4.TextColor != value))
                {
                    this._b4 = TableStyle.A485(this._b4, value, this);
                }
            }
        }

        public PdfColor TextHighlight
        {
            get
            {
                return this._b4.Highlight;
            }
            set
            {
                if (!this.A482 && (this._b4.Highlight != value))
                {
                    this._b4 = TableStyle.A492(this._b4, value, this);
                }
            }
        }

        public bool TextUnderline
        {
            get
            {
                return this._b4.Underline;
            }
            set
            {
                if (!this.A482 && (this._b4.Underline != value))
                {
                    this._b4 = TableStyle.A493(this._b4, value, this);
                }
            }
        }

        public bool Wrap
        {
            get
            {
                return this._b4.Wrap;
            }
            set
            {
                if (!this.A482 && (this._b4.Wrap != value))
                {
                    this._b4 = TableStyle.A488(this._b4, value, this);
                }
            }
        }
    }
}

