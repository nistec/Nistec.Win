namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using System;
    using System.Drawing;

    public class TableStyle : StyleBase
    {
        private PdfColor _b0;
        private MControl.Printing.Pdf.Border _b1;
        private bool _b2;
        private TextAlignment _b3;
        private ContentAlignment _b4;
        private bool _b5;
        private float _b6;
        private object _b7;
        private PdfColor _b8;
        private bool _b9;

        internal TableStyle(object b7) : base(null, 8f, PdfColor.Black, false)
        {
            this.b10(b7, TextAlignment.Left, ContentAlignment.TopLeft, true, PdfColor.Transparent, null, false, PdfColor.Transparent, false);
        }

        internal TableStyle(TableStyle b11, object b7) : base(b11._A171, b11._A172, b11._A504, b11._A505)
        {
            this.b10(b7, b11._b3, b11._b4, b11._b2, b11._b0, b11._b1, b11._b5, b11._b8, b11._b9);
        }

        public TableStyle(PdfFont font, float fontsize, MControl.Printing.Pdf.Border border) : base(font, fontsize, PdfColor.Black, false)
        {
            this.b10(null, TextAlignment.Left, ContentAlignment.TopLeft, true, PdfColor.Transparent, border, false, PdfColor.Transparent, false);
        }

        public TableStyle(PdfFont font, float fontsize, PdfColor textcolor) : base(font, fontsize, textcolor, false)
        {
            this.b10(null, TextAlignment.Left, ContentAlignment.TopLeft, true, PdfColor.Transparent, null, false, PdfColor.Transparent, false);
        }

        public TableStyle(PdfFont font, float fontsize, PdfColor textcolor, TextAlignment textalign) : base(font, fontsize, textcolor, false)
        {
            this.b10(null, textalign, ContentAlignment.TopLeft, true, PdfColor.Transparent, null, false, PdfColor.Transparent, false);
        }

        public TableStyle(PdfFont font, float fontsize, PdfColor textcolor, TextAlignment textalign, ContentAlignment calign, bool wrap, PdfColor texthighlight, MControl.Printing.Pdf.Border border) : base(font, fontsize, textcolor, false)
        {
            this.b10(null, textalign, calign, wrap, PdfColor.Transparent, border, false, texthighlight, false);
        }

        public TableStyle(PdfFont font, float fontsize, PdfColor textcolor, TextAlignment textalign, ContentAlignment calign, bool wrap, bool textunderline, MControl.Printing.Pdf.Border border) : base(font, fontsize, textcolor, false)
        {
            this.b10(null, textalign, calign, wrap, PdfColor.Transparent, border, false, PdfColor.Transparent, textunderline);
        }

        public TableStyle(PdfFont font, float fontsize, PdfColor textcolor, TextAlignment textalign, ContentAlignment calign, bool wrap, PdfColor backcolor, MControl.Printing.Pdf.Border border, bool rightToleft) : base(font, fontsize, textcolor, false)
        {
            this.b10(null, textalign, calign, wrap, backcolor, border, rightToleft, PdfColor.Transparent, false);
        }

        public TableStyle(PdfFont font, float fontsize, PdfColor textcolor, TextAlignment textalign, ContentAlignment calign, bool wrap, PdfColor texthighlight, bool textunderline, MControl.Printing.Pdf.Border border) : base(font, fontsize, textcolor, false)
        {
            this.b10(null, textalign, calign, wrap, PdfColor.Transparent, border, false, texthighlight, textunderline);
        }

        public TableStyle(PdfFont font, float fontsize, PdfColor textcolor, TextAlignment textalign, ContentAlignment calign, bool wrap, PdfColor backcolor, MControl.Printing.Pdf.Border border, bool rightToleft, bool bestwordfit) : base(font, fontsize, textcolor, bestwordfit)
        {
            this.b10(null, textalign, calign, wrap, backcolor, border, rightToleft, PdfColor.Transparent, false);
        }

        public TableStyle(PdfFont font, float fontsize, PdfColor textcolor, TextAlignment textalign, ContentAlignment calign, bool wrap, PdfColor texthighlight, bool textunderline, PdfColor backcolor, MControl.Printing.Pdf.Border border, bool rightToleft, bool bestwordfit) : base(font, fontsize, textcolor, bestwordfit)
        {
            this.b10(null, textalign, calign, wrap, backcolor, border, rightToleft, texthighlight, textunderline);
        }

        internal static TableStyle A157(TableStyle b11, PdfFont b14, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            if (b14 == null)
            {
                style._A171 = StyleBase.A428();
                return style;
            }
            style._A171 = b14;
            return style;
        }

        internal static TableStyle A483(TableStyle b11, PdfColor b0, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            if (b0 == null)
            {
                style._b0 = PdfColor.Transparent;
                return style;
            }
            style._b0 = b0;
            return style;
        }

        internal static TableStyle A484(TableStyle b11, MControl.Printing.Pdf.Border b1, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            style._b1 = b1;
            return style;
        }

        internal static TableStyle A485(TableStyle b11, PdfColor b16, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            if (b16 == null)
            {
                style._A504 = PdfColor.Black;
                return style;
            }
            style._A504 = b16;
            return style;
        }

        internal static TableStyle A486(TableStyle b11, float b15, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            style._A172 = b15;
            return style;
        }

        internal static TableStyle A487(TableStyle b11, float b6, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            style._b6 = b6;
            return style;
        }

        internal static TableStyle A488(TableStyle b11, bool b2, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            style._b2 = b2;
            return style;
        }

        internal static TableStyle A489(TableStyle b11, TextAlignment b3, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            style._b3 = b3;
            return style;
        }

        internal static TableStyle A490(TableStyle b11, ContentAlignment b4, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            style._b4 = b4;
            return style;
        }

        internal static TableStyle A491(TableStyle b11, bool b5, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            style._b5 = b5;
            return style;
        }

        internal static TableStyle A492(TableStyle b11, PdfColor b8, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            if (b8 == null)
            {
                style._b8 = PdfColor.Transparent;
                return style;
            }
            style._b8 = b8;
            return style;
        }

        internal static TableStyle A493(TableStyle b11, bool b9, object b7)
        {
            TableStyle style = b11;
            if (b11._b7 != b7)
            {
                style = new TableStyle(b11, b7);
            }
            style._b9 = b9;
            return style;
        }

        private void b10(object b7, TextAlignment b3, ContentAlignment b4, bool b2, PdfColor b0, MControl.Printing.Pdf.Border b1, bool b5, PdfColor b12, bool b13)
        {
            this._b7 = b7;
            this._b3 = b3;
            this._b4 = b4;
            this._b2 = b2;
            this._b1 = b1;
            this._b5 = b5;
            this._b9 = b13;
            if (b0 == null)
            {
                this._b0 = PdfColor.Transparent;
            }
            else
            {
                this._b0 = b0;
            }
            if (b12 == null)
            {
                this._b8 = PdfColor.Transparent;
            }
            else
            {
                this._b8 = b12;
            }
        }

        internal override bool A506
        {
            get
            {
                return (this._b9 | (this._b8 != PdfColor.Transparent));
            }
        }

        public PdfColor BackColor
        {
            get
            {
                return this._b0;
            }
        }

        public MControl.Printing.Pdf.Border Border
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
                return this._b4;
            }
        }

        public override PdfColor Highlight
        {
            get
            {
                return this._b8;
            }
        }

        public float LineSpace
        {
            get
            {
                return this._b6;
            }
        }

        public bool RightToLeft
        {
            get
            {
                return this._b5;
            }
        }

        public TextAlignment TextAlign
        {
            get
            {
                return this._b3;
            }
        }

        public override bool Underline
        {
            get
            {
                return this._b9;
            }
        }

        public bool Wrap
        {
            get
            {
                return this._b2;
            }
        }
    }
}

