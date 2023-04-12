namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class CMYKColor : PdfColor
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;

        internal CMYKColor()
        {
        }

        public CMYKColor(byte cyan, byte magenta, byte yellow, byte black)
        {
            b4(this, cyan, magenta, yellow, black);
        }

        internal override void A468(ref A112 b6, bool b7)
        {
            if (b7)
            {
                b6.A176(string.Format("{0} K", b5(this)));
            }
            else
            {
                b6.A176(string.Format("{0} k", b5(this)));
            }
        }

        internal override PdfColor DarkenColor(double percent)
        {
            float cyan = this._b0 - ((float) (this._b0 * percent));
            float magenta = this._b1 - ((float) (this._b1 * percent));
            float yellow = this._b2 - ((float) (this._b2 * percent));
            float black = this._b3 - ((float) (this._b3 * percent));
            return b5(cyan, magenta, yellow, black);
        }

        internal override PdfColor LightenColor(double percent)
        {
            float cyan = this._b0 + ((float) (this._b0 * percent));
            float magenta = this._b1 + ((float) (this._b1 * percent));
            float yellow = this._b2 + ((float) (this._b2 * percent));
            float black = this._b3 + ((float) (this._b3 * percent));
            return b5(cyan, magenta, yellow, black);
        }

        private static void b4(CMYKColor b9, byte b0, byte b1, byte b2, byte b3)
        {
            b9._b0 = (float) Math.Round((double) (((float) b0) / 255f), 3);
            b9._b1 = (float) Math.Round((double) (((float) b1) / 255f), 3);
            b9._b2 = (float) Math.Round((double) (((float) b2) / 255f), 3);
            b9._b3 = (float) Math.Round((double) (((float) b3) / 255f), 3);
        }

        private static string b5(CMYKColor b8)
        {
            return string.Format("{0} {1} {2} {3}", new object[] { A15.A18(b8._b0), A15.A18(b8._b1), A15.A18(b8._b2), A15.A18(b8._b3) });
        }

        private static CMYKColor b5(float cyan, float magenta, float yellow, float black)
        {
            if (cyan < 0.003f)
            {
                cyan = 0.003f;
            }
            if (cyan > 1f)
            {
                cyan = 1f;
            }
            if (magenta < 0.003f)
            {
                magenta = 0.003f;
            }
            if (magenta > 1f)
            {
                magenta = 1f;
            }
            if (yellow < 0.003f)
            {
                yellow = 0.003f;
            }
            if (yellow > 1f)
            {
                yellow = 1f;
            }
            if (black < 0.003f)
            {
                black = 0.003f;
            }
            if (black > 1f)
            {
                black = 1f;
            }
            CMYKColor color = new CMYKColor();
            color._b0 = cyan;
            color._b1 = magenta;
            color._b2 = yellow;
            color._b3 = black;
            return color;
        }

        public float B
        {
            get
            {
                return this._b3;
            }
        }

        public float C
        {
            get
            {
                return this._b0;
            }
        }

        public float M
        {
            get
            {
                return this._b1;
            }
        }

        public float Y
        {
            get
            {
                return this._b2;
            }
        }
    }
}

