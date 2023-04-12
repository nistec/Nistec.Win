namespace MControl.Printing.Pdf.Core.Text
{
    using MControl.Printing.Pdf.Drawing;
    using System;

    internal class A562
    {
        private float _b0;
        private bool _b1;
        private float _b2;
        private PdfColor _b3;

        internal A562(float b0, float b2, PdfColor b3, bool b1)
        {
            this._b0 = b0;
            this._b2 = b2;
            this._b1 = b1;
            this._b3 = b3;
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
                return this._b2;
            }
        }

        internal PdfColor A567
        {
            get
            {
                return this._b3;
            }
        }

        internal bool A586
        {
            get
            {
                return this._b1;
            }
        }
    }
}

