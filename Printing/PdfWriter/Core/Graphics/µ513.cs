namespace MControl.Printing.Pdf.Core.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A513 : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;

        internal A513(float b0, float b1, float b2, float b3)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
            this._b3 = b3;
        }

        internal override void A119(ref A120 b4, ref A112 b5)
        {
            GraphicsElement.A516(this._b0, this._b1, this._b2, this._b3, ref b4, ref b5);
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

        internal float A514
        {
            get
            {
                return this._b2;
            }
        }

        internal float A515
        {
            get
            {
                return this._b3;
            }
        }
    }
}

