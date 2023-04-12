namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Drawing;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Transparency : GraphicsElement
    {
        private float _b0;
        private float _b1;

        public Transparency(float value)
        {
            this.b2(value, value);
        }

        public Transparency(float stroke, float nonstroke)
        {
            this.b2(stroke, nonstroke);
        }

        public Transparency(float stroke, float nonstroke, string name)
        {
            base._A154 = name;
            this.b2(stroke, nonstroke);
        }

        internal override void A119(ref A120 b5, ref A112 b6)
        {
            A529 A = b5.A530;
            A6 A2 = b5.A531(this._b0, this._b1);
            if ((A.A196 != this._b0) || (A.A197 != this._b1))
            {
                b6.A176(string.Format("/GS{0} gs", A2.A168));
            }
        }

        private void b2(float b3, float b4)
        {
            if (b3 > 1f)
            {
                this._b0 = 1f;
            }
            else if (b3 < 0f)
            {
                this._b0 = 0f;
            }
            else
            {
                this._b0 = b3;
            }
            if (b4 > 1f)
            {
                this._b1 = 1f;
            }
            else if (b4 < 0f)
            {
                this._b1 = 0f;
            }
            else
            {
                this._b1 = b4;
            }
        }

        public float NonStrokeOpacity
        {
            get
            {
                return this._b1;
            }
            set
            {
                this.b2(this._b0, value);
            }
        }

        public float StrokeOpacity
        {
            get
            {
                return this._b0;
            }
            set
            {
                this.b2(value, this._b1);
            }
        }
    }
}

