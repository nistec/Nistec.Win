namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class CurveToSubPath : SubPath
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;

        public CurveToSubPath(float x, float y, float endControlPointX, float endControlPointY)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = endControlPointX;
            this._b3 = endControlPointY;
        }

        internal override void A119(ref A120 b4, ref A112 b5)
        {
            b5.A176(string.Format("{0} {1} {2} {3} v", new object[] { A15.A18(this._b2), A15.A18(b4.A98(this._b3)), A15.A18(this._b0), A15.A18(b4.A98(this._b1)) }));
        }

        public float EndControlPointX
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

        public float EndControlPointY
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

