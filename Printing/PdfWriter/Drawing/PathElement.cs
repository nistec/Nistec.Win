namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class PathElement : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private MControl.Printing.Pdf.GraphicsMode _b10;
        private SubPathList _b11;
        private PdfColor _b2;
        private PdfColor _b3;
        private float _b4;
        private MControl.Printing.Pdf.LineStyle _b5;
        private MControl.Printing.Pdf.LineCap _b6;
        private MControl.Printing.Pdf.LineJoin _b7;
        private float _b8;
        private bool _b9;

        public PathElement(float x, float y)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = PdfColor.Transparent;
            this._b3 = PdfColor.Black;
            this._b4 = 1f;
            this._b5 = MControl.Printing.Pdf.LineStyle.Solid;
            this._b6 = MControl.Printing.Pdf.LineCap.Butt;
            this._b7 = MControl.Printing.Pdf.LineJoin.Miter;
            this._b8 = 10f;
            this._b9 = false;
            this._b10 = MControl.Printing.Pdf.GraphicsMode.stroke;
            this._b11 = new SubPathList();
        }

        internal override void A119(ref A120 b12, ref A112 b13)
        {
            b13.A176(string.Format("{0} J", A15.A18((int) this._b6)));
            b13.A176(string.Format("{0} j", A15.A18((int) this._b7)));
            b13.A176(string.Format("{0} M", A15.A18(this._b8)));
            GraphicsElement.A177(this._b4, this._b3, this._b5, ref b12, ref b13);
            GraphicsElement.A160(this._b2, ref b12, ref b13);
            GraphicsElement.A178(b13, this._b0, b12.A98(this._b1));
            for (int i = 0; i < this._b11.Count; i++)
            {
                this._b11[i].A119(ref b12, ref b13);
            }
            GraphicsElement.A180(b13, this._b10, this._b9);
        }

        public bool ClosePath
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
                return this._b2;
            }
            set
            {
                this._b2 = value;
            }
        }

        public MControl.Printing.Pdf.GraphicsMode GraphicsMode
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

        public MControl.Printing.Pdf.LineCap LineCap
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

        public PdfColor LineColor
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

        public MControl.Printing.Pdf.LineJoin LineJoin
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
                return this._b4;
            }
            set
            {
                this._b4 = value;
            }
        }

        public float MiterLimit
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

        public SubPathList SubPaths
        {
            get
            {
                return this._b11;
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

