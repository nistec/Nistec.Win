namespace MControl.Printing.Pdf.Core.Text
{
    using MControl.Printing.Pdf;
    using System;

    internal class A525 : A523
    {
        private float _b0;
        private TextAlignment _b1;

        internal A525(TextAlignment b2, float b0)
        {
            this._b0 = b0;
            this._b1 = b2;
        }

        internal override TextAlignment A570
        {
            get
            {
                return this._b1;
            }
        }

        internal override A37 A587
        {
            get
            {
                return A37.A40;
            }
        }

        internal override float A593
        {
            get
            {
                return this._b0;
            }
        }
    }
}

