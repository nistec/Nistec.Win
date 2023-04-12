namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using System;
    using System.Drawing;

    public abstract class StyleBase
    {
        internal PdfFont _A171;
        internal float _A172;
        internal PdfColor _A504;
        internal bool _A505;

        internal StyleBase(PdfFont b0, float b1, PdfColor b2, bool b3)
        {
            if (b0 == null)
            {
                this._A171 = A428();
            }
            else
            {
                this._A171 = b0;
            }
            if (b1 < 1f)
            {
                this._A172 = 8f;
            }
            else
            {
                this._A172 = b1;
            }
            if (b2 == null)
            {
                this._A504 = PdfColor.Black;
            }
            else
            {
                this._A504 = b2;
            }
            this._A505 = b3;
        }

        internal virtual void A157(PdfFont b0)
        {
        }

        internal static PdfFont A428()
        {
            return new PdfFont(StandardFonts.Helvetica, FontStyle.Regular);
        }

        internal virtual bool A506
        {
            get
            {
                return false;
            }
        }

        public bool BestWordFit
        {
            get
            {
                return this._A505;
            }
        }

        public PdfFont Font
        {
            get
            {
                return this._A171;
            }
        }

        public float FontSize
        {
            get
            {
                return this._A172;
            }
        }

        public virtual PdfColor Highlight
        {
            get
            {
                return PdfColor.Transparent;
            }
        }

        public PdfColor TextColor
        {
            get
            {
                return this._A504;
            }
        }

        public virtual bool Underline
        {
            get
            {
                return false;
            }
        }
    }
}

