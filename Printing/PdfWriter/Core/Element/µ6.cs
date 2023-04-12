namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A6 : A91
    {
        private int _b0;
        private float _b1;
        private float _b2;

        internal A6(float b3, float b4, int b0, Document b5)
        {
            base._A92 = b5;
            this._b1 = b3;
            this._b2 = b4;
            this._b0 = b0;
        }

        internal static bool A198(A6 b7, A6 b8)
        {
            return ((b7 == b8) || (((b7 != null) && (b8 != null)) && ((b7.A196 == b8.A196) && (b7.A197 == b8.A197))));
        }

        internal override void A54(ref A55 b6)
        {
            base._A92.A93.A94(b6.A2, 0);
            b6.A59(string.Format("{0} 0 obj", this.A95));
            b6.A59("<<");
            b6.A59("/Type /ExtGState ");
            b6.A59(string.Format("/CA {0} ", A15.A18(this.A196)));
            b6.A59(string.Format("/ca {0} ", A15.A18(this.A197)));
            b6.A59(">>");
            b6.A59("endobj");
        }

        internal override string A168
        {
            get
            {
                return Convert.ToString(this._b0);
            }
        }

        internal float A196
        {
            get
            {
                return this._b1;
            }
        }

        internal float A197
        {
            get
            {
                return this._b2;
            }
        }
    }
}

