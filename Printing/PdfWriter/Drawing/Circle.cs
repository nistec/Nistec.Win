namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Circle : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;
        private PdfColor _b4;
        private PdfColor _b5;
        private MControl.Printing.Pdf.LineStyle _b6;
        private float _b7;
        private MControl.Printing.Pdf.GraphicsMode _b8;

        public Circle(float x, float y, float radiusX, float radiusY, float linewidth, MControl.Printing.Pdf.LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, MControl.Printing.Pdf.GraphicsMode mode, string name)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = radiusX;
            this._b3 = radiusY;
            this._b6 = linestyle;
            this._b7 = linewidth;
            this._b5 = linecolor;
            this._b4 = fillcolor;
            this._b8 = mode;
            base._A154 = name;
        }

        internal override void A119(ref A120 b9, ref A112 b10)
        {
            if ((this._b8 & MControl.Printing.Pdf.GraphicsMode.stroke) == MControl.Printing.Pdf.GraphicsMode.stroke)
            {
                GraphicsElement.A177(this._b7, this._b5, this._b6, ref b9, ref b10);
            }
            if ((this._b8 & MControl.Printing.Pdf.GraphicsMode.fill) == MControl.Printing.Pdf.GraphicsMode.fill)
            {
                GraphicsElement.A160(this._b4, ref b9, ref b10);
            }
            float num = b9.A98(this._b1);
            GraphicsElement.A183(ref b10, this._b0, num, this._b2, this._b3, this._b8);
        }

        public PdfColor FillColor
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

        public MControl.Printing.Pdf.GraphicsMode GraphicsMode
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

        public PdfColor LineColor
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

        public MControl.Printing.Pdf.LineStyle LineStyle
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

        public float LineWidth
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

        public float RadiusX
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

        public float RadiusY
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

