namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class Lines : GraphicsElement
    {
        private PointF[] _b0;
        private PdfColor _b1;
        private PdfColor _b2;
        private MControl.Printing.Pdf.LineStyle _b3;
        private float _b4;
        private MControl.Printing.Pdf.GraphicsMode _b5;
        private bool _b6;

        public Lines(PointF[] points, float linewidth, MControl.Printing.Pdf.LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, MControl.Printing.Pdf.GraphicsMode mode, bool closepath, string name)
        {
            this._b0 = points;
            this._b4 = linewidth;
            this._b3 = linestyle;
            this._b1 = fillcolor;
            this._b2 = linecolor;
            this._b5 = mode;
            this._b6 = closepath;
            base._A154 = name;
        }

        internal override void A119(ref A120 b7, ref A112 b8)
        {
            if ((this._b5 & MControl.Printing.Pdf.GraphicsMode.stroke) == MControl.Printing.Pdf.GraphicsMode.stroke)
            {
                GraphicsElement.A177(this._b4, this._b2, this._b3, ref b7, ref b8);
            }
            if ((this._b5 & MControl.Printing.Pdf.GraphicsMode.fill) == MControl.Printing.Pdf.GraphicsMode.fill)
            {
                GraphicsElement.A160(this._b1, ref b7, ref b8);
            }
            PointF tf = this._b0[0];
            GraphicsElement.A178(b8, tf.X, b7.A98(tf.Y));
            for (int i = 1; i < this._b0.Length; i++)
            {
                tf = this._b0[i];
                GraphicsElement.A179(b8, tf.X, b7.A98(tf.Y));
            }
            GraphicsElement.A180(b8, this._b5, this._b6);
        }

        public bool ClosePath
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

