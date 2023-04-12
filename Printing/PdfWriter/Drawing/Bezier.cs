namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Bezier : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private PdfColor _b10;
        private PdfColor _b11;
        private MControl.Printing.Pdf.GraphicsMode _b12;
        private float _b2;
        private float _b3;
        private float _b4;
        private float _b5;
        private float _b6;
        private float _b7;
        private float _b8;
        private MControl.Printing.Pdf.LineStyle _b9;

        public Bezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, float linewidth, MControl.Printing.Pdf.LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, MControl.Printing.Pdf.GraphicsMode mode, string name)
        {
            base._A154 = name;
            this._b0 = x1;
            this._b1 = y1;
            this._b2 = x2;
            this._b3 = y2;
            this._b4 = x3;
            this._b5 = y3;
            this._b6 = x4;
            this._b7 = y4;
            this._b8 = linewidth;
            this._b9 = linestyle;
            this._b10 = linecolor;
            this._b10 = linecolor;
            this._b11 = fillcolor;
            this._b12 = mode;
        }

        internal override void A119(ref A120 b13, ref A112 b14)
        {
            if ((this._b12 & MControl.Printing.Pdf.GraphicsMode.stroke) == MControl.Printing.Pdf.GraphicsMode.stroke)
            {
                GraphicsElement.A177(this._b8, this._b10, this._b9, ref b13, ref b14);
            }
            if ((this._b12 & MControl.Printing.Pdf.GraphicsMode.fill) == MControl.Printing.Pdf.GraphicsMode.fill)
            {
                GraphicsElement.A160(this._b11, ref b13, ref b14);
            }
            float num = b13.A98(this._b1);
            float num2 = b13.A98(this._b3);
            float num3 = b13.A98(this._b5);
            float num4 = b13.A98(this._b7);
            GraphicsElement.A178(b14, this._b0, num);
            GraphicsElement.A472(b14, this._b2, num2, this._b4, num3, this._b6, num4);
            GraphicsElement.A180(b14, this._b12, false);
        }

        public PdfColor FillColor
        {
            get
            {
                return this._b11;
            }
            set
            {
                this._b11 = value;
            }
        }

        public MControl.Printing.Pdf.GraphicsMode GraphicsMode
        {
            get
            {
                return this._b12;
            }
            set
            {
                this._b12 = value;
            }
        }

        public PdfColor LineColor
        {
            get
            {
                return this._b10;
            }
            set
            {
                this._b10 = value;
            }
        }

        public MControl.Printing.Pdf.LineStyle LineStyle
        {
            get
            {
                return this._b9;
            }
            set
            {
                this._b9 = value;
            }
        }

        public float LineWidth
        {
            get
            {
                return this._b8;
            }
            set
            {
                this._b8 = value;
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

        public float X3
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

        public float X4
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

        public float Y3
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

        public float Y4
        {
            get
            {
                return this._b7;
            }
            set
            {
                this._b7 = value;
            }
        }
    }
}

