namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Line : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;
        private PdfColor _b4;
        private MControl.Printing.Pdf.LineStyle _b5;
        private float _b6;

        public Line(float x1, float y1, float x2, float y2, float linewidth, MControl.Printing.Pdf.LineStyle linestyle, PdfColor linecolor, string name)
        {
            this._b0 = x1;
            this._b1 = y1;
            this._b2 = x2;
            this._b3 = y2;
            this._b6 = linewidth;
            this._b5 = linestyle;
            this._b4 = linecolor;
            base._A154 = name;
        }

        internal override void A119(ref A120 b7, ref A112 b8)
        {
            GraphicsElement.A177(this._b6, this._b4, this._b5, ref b7, ref b8);
            float num = b7.A98(this._b1);
            float num2 = b7.A98(this._b3);
            GraphicsElement.A178(b8, this._b0, num);
            GraphicsElement.A179(b8, this._b2, num2);
            GraphicsElement.A180(b8, GraphicsMode.stroke, false);
        }

        public PdfColor LineColor
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

        public MControl.Printing.Pdf.LineStyle LineStyle
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

        public float LineWidth
        {
            get
            {
                return this._b6;
            }
            set
            {
                this._b6 = value;
            }
        }

        public float X1
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

        public float X2
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

        public float Y1
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

        public float Y2
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
    }
}

