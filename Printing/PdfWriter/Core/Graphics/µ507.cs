namespace MControl.Printing.Pdf.Core.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A507 : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private float _b2;

        internal A507(float b0, float b1, float b2)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
        }

        internal override void A119(ref A120 b3, ref A112 b4)
        {
            GraphicsElement.A436(this._b0, this._b1, 0f, 0f, this._b2, ref b3, ref b4);
        }

        internal float A142
        {
            get
            {
                return this._b0;
            }
        }

        internal float A143
        {
            get
            {
                return this._b1;
            }
        }

        internal float A508
        {
            get
            {
                return this._b2;
            }
        }
    }
}

