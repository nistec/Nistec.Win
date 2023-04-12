namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class LineSubPath : SubPath
    {
        private float _b0;
        private float _b1;

        public LineSubPath(float x, float y)
        {
            this._b0 = x;
            this._b1 = y;
        }

        internal override void A119(ref A120 b2, ref A112 b3)
        {
            GraphicsElement.A179(b3, this._b0, b2.A98(this._b1));
        }

        public float X
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

        public float Y
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

