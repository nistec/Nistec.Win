namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Drawing;
    using MControl.Printing.Pdf.Core.Text;
    using System;
    using System.Drawing;

    public class Cell
    {
        private A477 _b0;
        private int _b1;
        private MControl.Printing.Pdf.Drawing.Row _b2;
        private int _b3;
        private TableStyle _b4;
        internal A478 A479;

        internal Cell(MControl.Printing.Pdf.Drawing.Row b2, int b5, int b1, A477 b0, TableStyle b4)
        {
            this._b2 = b2;
            this._b4 = b4;
            this._b3 = b5;
            this._b1 = b1;
            this._b0 = b0;
            this.A479 = null;
        }

        internal int A474()
        {
            return this._b3;
        }

        internal A477 A481()
        {
            return this._b0;
        }

        private static float b6(int b3, int b1, MControl.Printing.Pdf.Drawing.Row b2)
        {
            Columns columns = b2.Table.Columns;
            float num = 0f;
            int num2 = b3 + b1;
            for (int i = b3; i < num2; i++)
            {
                float w = columns[i].Width;
                if (w <= 0)

                    w = MControl.Types.ToFloat(b2.Cells[i].Value.ToString().Length * 3, 0);
                num += w;

            }
            return num;
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
                return this._b2.A482;
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

        public int ColSpan
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
                return this._b2.Height;
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

        public MControl.Printing.Pdf.Drawing.Row Row
        {
            get
            {
                return this._b2;
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

        public object Value
        {
            get
            {
                return this._b0.A480;
            }
        }

        public float Width
        {
            get
            {
                return b6(this._b3, this._b1, this._b2);
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

