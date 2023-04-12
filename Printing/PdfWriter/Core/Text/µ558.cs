namespace MControl.Printing.Pdf.Core.Text
{
    using MControl.Printing.Pdf.Drawing;
    using System;

    internal class A558
    {
        private float _b0;
        private float _b1;
        private PdfColor _b2;

        internal A558(float b0, float b1, PdfColor b2)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
        }

        internal float A142
        {
            get
            {
                return this._b0;
            }
        }

        internal float A211
        {
            get
            {
                return this._b1;
            }
        }

        internal PdfColor A567
        {
            get
            {
                return this._b2;
            }
        }
    }
}

