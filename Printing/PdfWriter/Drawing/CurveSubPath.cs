namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class CurveSubPath : SubPath
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;
        private float _b4;
        private float _b5;

        public CurveSubPath(float x, float y, float startControlPointX, float startControlPointY, float endControlPointX, float endControlPointY)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = startControlPointX;
            this._b3 = startControlPointY;
            this._b4 = endControlPointX;
            this._b5 = endControlPointY;
        }

        internal override void A119(ref A120 b6, ref A112 b7)
        {
            GraphicsElement.A472(b7, this._b2, b6.A98(this._b3), this._b4, b6.A98(this._b5), this._b0, b6.A98(this._b1));
        }

        public float EndControlPointX
        {
            get
            {
                return this._b4;
            }
            set
            {
                this._b4 = value;
            }
        }

        public float EndControlPointY
        {
            get
            {
                return this._b5;
            }
            set
            {
                this._b5 = value;
            }
        }

        public float StartControlPointX
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

        public float StartControlPointY
        {
            get
            {
                return this._b3;
            }
            set
            {
                this._b3 = value;
            }
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

