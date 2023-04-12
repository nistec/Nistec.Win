namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Grayscale : PdfColor
    {
        private float _b0;

        public Grayscale(float grayLevel)
        {
            if ((grayLevel < 0f) || (grayLevel > 1f))
            {
                throw new PdfWriterException("Grayscale value must be from 0.0 to 1.0.");
            }
            this._b0 = grayLevel;
        }

        internal override string A169(bool b3)
        {
            if (b3)
            {
                return string.Format("{0} G", this._b0);
            }
            return string.Format("{0} g", this._b0);
        }

        internal override void A468(ref A112 b2, bool b3)
        {
            b2.A176(this.A169(b3));
        }

        internal override PdfColor DarkenColor(double percent)
        {
            float num = this._b0 - ((float) (this._b0 * percent));
            return b1(num);
        }

        internal override PdfColor LightenColor(double percent)
        {
            float num = this._b0 + ((float) (this._b0 * percent));
            return b1(num);
        }

        private static Grayscale b1(float b0)
        {
            if (b0 < 0f)
            {
                b0 = 0f;
            }
            if (b0 > 1f)
            {
                b0 = 1f;
            }
            return new Grayscale(b0);
        }

        public float GrayLevel
        {
            get
            {
                return this._b0;
            }
        }
    }
}

