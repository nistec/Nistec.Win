namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class RoundRectangle : GraphicsElement
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
        private float _b9;

        public RoundRectangle(float x, float y, float width, float height, float edgeRadius, float linewidth, MControl.Printing.Pdf.LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, MControl.Printing.Pdf.GraphicsMode mode, string name)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = width;
            this._b3 = height;
            this._b9 = edgeRadius;
            this._b7 = linewidth;
            this._b6 = linestyle;
            this._b4 = fillcolor;
            this._b5 = linecolor;
            this._b8 = mode;
            base._A154 = name;
        }

        internal override void A119(ref A120 b10, ref A112 b11)
        {
            float x = this._b0;
            float y = this._b1;
            float num3 = this._b2 - (2f * this._b9);
            y += this._b9;
            PathElement element = new PathElement(x, y);
            element.FillColor = this._b4;
            element.LineColor = this._b5;
            element.LineStyle = this._b6;
            element.LineWidth = this._b7;
            element.ClosePath = true;
            element.GraphicsMode = this._b8;
            y += num3;
            element.SubPaths.Add(new LineSubPath(x, y));
            y += this._b9;
            float endControlPointX = x;
            float endControlPointY = y;
            x += this._b9;
            element.SubPaths.Add(new CurveToSubPath(x, y, endControlPointX, endControlPointY));
            x += num3;
            element.SubPaths.Add(new LineSubPath(x, y));
            x += this._b9;
            endControlPointX = x;
            endControlPointY = y;
            y -= this._b9;
            element.SubPaths.Add(new CurveToSubPath(x, y, endControlPointX, endControlPointY));
            y -= num3;
            element.SubPaths.Add(new LineSubPath(x, y));
            y -= this._b9;
            endControlPointX = x;
            endControlPointY = y;
            x -= this._b9;
            element.SubPaths.Add(new CurveToSubPath(x, y, endControlPointX, endControlPointY));
            x -= num3;
            element.SubPaths.Add(new LineSubPath(x, y));
            x -= this._b9;
            endControlPointX = x;
            endControlPointY = y;
            y += this._b9;
            element.SubPaths.Add(new CurveToSubPath(x, y, endControlPointX, endControlPointY));
            element.A119(ref b10, ref b11);
        }

        public float EdgeRadius
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

        public float Height
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

        public float Width
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

