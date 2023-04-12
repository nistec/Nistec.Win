namespace MControl.Printing.Pdf
{
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Runtime.InteropServices;

    public class Page
    {
        private float _b0;
        private float _b1;
        private MControl.Printing.Pdf.PageRotate _b2;
        private Document _b4;
        private int _b5;
        private PdfGraphics b3;

        public Page(PageSize pagesize)
        {
            b6(pagesize, out this._b0, out this._b1);
            this._b2 = MControl.Printing.Pdf.PageRotate.Angle0;
            this.b7();
        }

        public Page(PageSize pagesize, PageOrientation orientation)
        {
            if (orientation == PageOrientation.Portrait)
            {
                b6(pagesize, out this._b0, out this._b1);
            }
            else
            {
                b6(pagesize, out this._b1, out this._b0);
            }
            this._b2 = MControl.Printing.Pdf.PageRotate.Angle0;
            this.b7();
        }

        public Page(float width, float height)
        {
            this._b0 = width;
            this._b1 = height;
            this._b2 = MControl.Printing.Pdf.PageRotate.Angle0;
            this.b7();
        }

        internal void A110()
        {
            int num;
            A93 A = this._b4.A93;
            A.A95 = (num = A.A95) + 1;
            this._b5 = num;
            if (this.b3.A293 > 0)
            {
                A.A95++;
            }
        }

        internal void A54(ref A598 b9, ref A55 b10)
        {
            A91 A;
            A112 A3 = new A112();
            A120 A4 = new A120(this, ref b9);
            this.b3.A547(ref A4, ref A3);
            A93 A5 = this._b4.A93;
            A5.A94(b10.A2, 0);
            b10.A59(string.Format("{0} 0 obj", this.A95));
            b10.A59("<<");
            b10.A59("/Type /Page ");
            b10.A59(string.Format("/Parent {0} 0 R ", this._b4.Pages.A95));
            b10.A59(string.Format("/MediaBox {0} ", A15.A21(0f, 0f, this._b0, this._b1)));
            if (this._b2 == MControl.Printing.Pdf.PageRotate.Angle0)
            {
                b10.A59(string.Format("/Rotate {0}", 0));
            }
            else if (this._b2 == MControl.Printing.Pdf.PageRotate.Angle90)
            {
                b10.A59(string.Format("/Rotate {0}", 90));
            }
            else if (this._b2 == MControl.Printing.Pdf.PageRotate.Angle180)
            {
                b10.A59(string.Format("/Rotate {0}", 180));
            }
            else if (this._b2 == MControl.Printing.Pdf.PageRotate.Angle270)
            {
                b10.A59(string.Format("/Rotate {0}", 270));
            }
            int num = this._b5;
            if (this.b3.A293 > 0)
            {
                num++;
                b10.A59(string.Format("/Contents {0} 0 R ", num));
            }
            b10.A59("/Resources <<");
            b10.A59(string.Format("/ProcSet {0} 0 R ", this._b4.A222.A95));
            A599 A2 = A4.A5;
            if (A2.A2 > 0)
            {
                b10.A59("/ExtGState << ");
                for (int i = 0; i < A2.A2; i++)
                {
                    A = A2[i];
                    b10.A59(string.Format("/GS{0} {1} 0 R ", A.A168, A.A95));
                }
                b10.A59(">>");
            }
            A2 = A4.A13;
            if (A2.A2 > 0)
            {
                b10.A59("/XObject << ");
                for (int j = 0; j < A2.A2; j++)
                {
                    A = A2[j];
                    b10.A59(string.Format("/{0} {1} 0 R ", A.A168, A.A95));
                }
                b10.A59(">>");
            }
            A2 = A4.A601;
            if (A2.A2 > 0)
            {
                b10.A59("/Font << ");
                for (int k = 0; k < A2.A2; k++)
                {
                    A = A2[k];
                    b10.A59(string.Format("/{0} {1} 0 R ", A.A168, A.A95));
                }
                b10.A59(">>");
            }
            b10.A59(">>");
            A2 = A4.A602;
            if (A2.A2 > 0)
            {
                b10.A54("/Annots [ ");
                for (int m = 0; m < A2.A2; m++)
                {
                    b10.A176(string.Format("{0} 0 R ", A2[m].A95));
                }
                b10.A176("] ");
            }
            b10.A59(">>");
            b10.A59("endobj");
            if (this.b3.A293 > 0)
            {
                if (this._b4.Compress)
                {
                    A3.A123();
                }
                A5.A94(b10.A2, 0);
                A3.A54(b10, num, false, this._b4.A56);
            }
        }

        internal float A98(float b8)
        {
            return (this._b1 - b8);
        }

        private static void b6(PageSize b11, out float b0, out float b1)
        {
            if (b11 == PageSize.A0)
            {
                b0 = 2380f;
                b1 = 3368f;
            }
            else if (b11 == PageSize.A1)
            {
                b0 = 1684f;
                b1 = 2380f;
            }
            else if (b11 == PageSize.A2)
            {
                b0 = 1190f;
                b1 = 1684f;
            }
            else if (b11 == PageSize.A3)
            {
                b0 = 842f;
                b1 = 1190f;
            }
            else if (b11 == PageSize.A4)
            {
                b0 = 595f;
                b1 = 842f;
            }
            else if (b11 == PageSize.A5)
            {
                b0 = 420f;
                b1 = 595f;
            }
            else if (b11 == PageSize.B4)
            {
                b0 = 729f;
                b1 = 1032f;
            }
            else if (b11 == PageSize.B5)
            {
                b0 = 516f;
                b1 = 729f;
            }
            else if (b11 == PageSize.Letter)
            {
                b0 = 612f;
                b1 = 792f;
            }
            else if (b11 == PageSize.Legal)
            {
                b0 = 612f;
                b1 = 1008f;
            }
            else if (b11 == PageSize.Tabloid)
            {
                b0 = 792f;
                b1 = 1224f;
            }
            else if (b11 == PageSize.Ledger)
            {
                b0 = 1224f;
                b1 = 792f;
            }
            else if (b11 == PageSize.Statement)
            {
                b0 = 396f;
                b1 = 612f;
            }
            else if (b11 == PageSize.Executive)
            {
                b0 = 540f;
                b1 = 720f;
            }
            else if (b11 == PageSize.Folio)
            {
                b0 = 612f;
                b1 = 936f;
            }
            else if (b11 == PageSize.Quarto)
            {
                b0 = 610f;
                b1 = 780f;
            }
            else
            {
                b0 = 720f;
                b1 = 1008f;
            }
        }

        private void b7()
        {
            this.b3 = new PdfGraphics(this);
        }

        internal int A95
        {
            get
            {
                return this._b5;
            }
        }

        internal Document A97
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

        public PdfGraphics Graphics
        {
            get
            {
                return this.b3;
            }
        }

        public float Height
        {
            get
            {
                return this._b1;
            }
        }

        public MControl.Printing.Pdf.PageRotate PageRotate
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

        public float Width
        {
            get
            {
                return this._b0;
            }
        }
    }
}

