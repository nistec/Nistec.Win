namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using System;

    public class TextStyle : StyleBase
    {
        private PdfColor _b0;
        private bool _b1;

        public TextStyle() : base(StyleBase.A428(), 8f, PdfColor.Black, false)
        {
            this._b0 = PdfColor.Transparent;
            this._b1 = false;
        }

        public TextStyle(PdfFont font, float fontsize) : base(font, fontsize, PdfColor.Black, false)
        {
            this._b0 = PdfColor.Transparent;
            this._b1 = false;
        }

        public TextStyle(PdfFont font, float fontsize, PdfColor textcolor) : base(font, fontsize, textcolor, false)
        {
            this._b0 = PdfColor.Transparent;
            this._b1 = false;
        }

        public TextStyle(PdfFont font, float fontsize, PdfColor textcolor, bool bestwordfit) : base(font, fontsize, textcolor, bestwordfit)
        {
            this._b0 = PdfColor.Transparent;
            this._b1 = false;
        }

        public TextStyle(PdfFont font, float fontsize, PdfColor textcolor, PdfColor highlight, bool underline) : this(font, fontsize, textcolor, highlight, underline, false)
        {
        }

        public TextStyle(PdfFont font, float fontsize, PdfColor textcolor, PdfColor highlight, bool underline, bool bestwordfit) : base(font, fontsize, textcolor, bestwordfit)
        {
            if (this._b0 == null)
            {
                this._b0 = PdfColor.Transparent;
            }
            else
            {
                this._b0 = highlight;
            }
            this._b1 = underline;
        }

        internal override void A157(PdfFont b2)
        {
            base._A171 = b2;
        }

        internal override bool A506
        {
            get
            {
                return (this._b1 | (this.Highlight != PdfColor.Transparent));
            }
        }

        public override PdfColor Highlight
        {
            get
            {
                return this._b0;
            }
        }

        public override bool Underline
        {
            get
            {
                return this._b1;
            }
        }
    }
}

