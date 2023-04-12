namespace MControl.Printing.Pdf.Core.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A517 : GraphicsElement
    {
        private float _b0;
        private float _b1;

        internal A517(float b0, float b1)
        {
            this._b0 = b0;
            this._b1 = b1;
        }

        internal override void A119(ref A120 b2, ref A112 b3)
        {
            GraphicsElement.A170(this._b0, -this._b1, ref b3);
        }

        internal float A518
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

        internal float A519
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
    }
}

