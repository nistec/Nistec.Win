namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class Beziers : GraphicsElement
    {
        private PointF[] _b0;
        private PdfColor _b1;
        private PdfColor _b2;
        private MControl.Printing.Pdf.LineStyle _b3;
        private float _b4;
        private MControl.Printing.Pdf.GraphicsMode _b5;

        public Beziers(PointF[] points, float linewidth, MControl.Printing.Pdf.LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, MControl.Printing.Pdf.GraphicsMode mode, string name)
        {
            base._A154 = name;
            this._b0 = points;
            this._b4 = linewidth;
            this._b3 = linestyle;
            this._b1 = fillcolor;
            this._b2 = linecolor;
            this._b5 = mode;
        }

        internal override void A119(ref A120 b6, ref A112 b7)
        {
            int length = this._b0.Length;
            if ((length > 0) && (((length - 1) - (((length - 1) / 3) * 3)) == 0))
            {
                if ((this._b5 & MControl.Printing.Pdf.GraphicsMode.stroke) == MControl.Printing.Pdf.GraphicsMode.stroke)
                {
                    GraphicsElement.A177(this._b4, this._b2, this._b3, ref b6, ref b7);
                }
                if ((this._b5 & MControl.Printing.Pdf.GraphicsMode.fill) == MControl.Printing.Pdf.GraphicsMode.fill)
                {
                    GraphicsElement.A160(this._b1, ref b6, ref b7);
                }
                float x = this._b0[0].X;
                float y = this._b0[0].Y;
                GraphicsElement.A178(b7, x, b6.A98(y));
                for (int i = 1; i < length; i += 3)
                {
                    x = this._b0[i].X;
                    y = this._b0[i].Y;
                    float num4 = this._b0[i + 1].X;
                    float num5 = this._b0[i + 1].Y;
                    float num6 = this._b0[i + 2].X;
                    float num7 = this._b0[i + 2].Y;
                    GraphicsElement.A472(b7, x, b6.A98(y), num4, b6.A98(num5), num6, b6.A98(num7));
                }
                GraphicsElement.A180(b7, this._b5, false);
            }
        }

        public PdfColor FillColor
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

        public MControl.Printing.Pdf.GraphicsMode GraphicsMode
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

        public PdfColor LineColor
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

        public MControl.Printing.Pdf.LineStyle LineStyle
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

        public float LineWidth
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

        public PointF[] Points
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
    }
}

