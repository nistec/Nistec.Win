namespace MControl.Printing.Pdf
{
    using MControl.Printing.Pdf.Drawing;
    using System;

    public class Border
    {
        private float _b0;
        private PdfColor _b1;
        private MControl.Printing.Pdf.LineStyle _b2;

        public Border()
        {
            this._b0 = 1f;
            this._b1 = PdfColor.Black;
            this._b2 = MControl.Printing.Pdf.LineStyle.Solid;
        }

        public Border(float linewidth, PdfColor linecolor, MControl.Printing.Pdf.LineStyle linestyle)
        {
            this._b0 = linewidth;
            this._b1 = linecolor;
            this._b2 = linestyle;
        }

        public PdfColor LineColor
        {
            get
            {
                return this._b1;
            }
            set
            {
                this._b1 = value;
            }
        }

        public MControl.Printing.Pdf.LineStyle LineStyle
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

        public float LineWidth
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
    }
}

