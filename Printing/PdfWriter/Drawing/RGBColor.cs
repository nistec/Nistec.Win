namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class RGBColor : PdfColor
    {
        private float _b0;
        private float _b1;
        private float _b2;

        internal RGBColor()
        {
        }

        public RGBColor(Color color)
        {
            A237(this, color.R, color.G, color.B);
        }

        public RGBColor(byte red, byte green, byte blue)
        {
            A237(this, red, green, blue);
        }

        internal static string A122(RGBColor b6)
        {
            return string.Format("{0} {1} {2}", A15.A18(b6._b0), A15.A18(b6._b1), A15.A18(b6._b2));
        }

        internal override string A169(bool b5)
        {
            if (b5)
            {
                return string.Format("{0} RG", A122(this));
            }
            return string.Format("{0} rg", A122(this));
        }

        internal static void A237(RGBColor b7, byte b0, byte b1, byte b2)
        {
            b7._b0 = (float) Math.Round((double) (((float) b0) / 255f), 3);
            b7._b1 = (float) Math.Round((double) (((float) b1) / 255f), 3);
            b7._b2 = (float) Math.Round((double) (((float) b2) / 255f), 3);
        }

        internal override void A468(ref A112 b4, bool b5)
        {
            b4.A176(this.A169(b5));
        }

        internal override PdfColor DarkenColor(double percent)
        {
            float red = this._b0 - ((float) (this._b0 * percent));
            float green = this._b1 - ((float) (this._b1 * percent));
            float blue = this._b2 - ((float) (this._b2 * percent));
            return b3(red, green, blue);
        }

        internal override PdfColor LightenColor(double percent)
        {
            float red = this._b0 + ((float) (this._b0 * percent));
            float green = this._b1 + ((float) (this._b1 * percent));
            float blue = this._b2 + ((float) (this._b2 * percent));
            return b3(red, green, blue);
        }

        private static RGBColor b3(float red, float green, float blue)
        {
            if (red < 0.003f)
            {
                red = 0.003f;
            }
            if (red > 1f)
            {
                red = 1f;
            }
            if (green < 0.003f)
            {
                green = 0.003f;
            }
            if (green > 1f)
            {
                green = 1f;
            }
            if (blue < 0.003f)
            {
                blue = 0.003f;
            }
            if (blue > 1f)
            {
                blue = 1f;
            }
            RGBColor color = new RGBColor();
            color._b0 = red;
            color._b1 = green;
            color._b2 = blue;
            return color;
        }

        public float B
        {
            get
            {
                return this._b2;
            }
        }

        public float G
        {
            get
            {
                return this._b1;
            }
        }

        public float R
        {
            get
            {
                return this._b0;
            }
        }
    }
}

