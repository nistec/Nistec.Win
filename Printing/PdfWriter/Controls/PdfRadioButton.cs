namespace MControl.Printing.Pdf.Controls
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class PdfRadioButton : PdfField
    {
        internal bool _A182;
        private PdfRadioGroup _b0;

        internal PdfRadioButton(PdfRadioGroup parent, string name, RectangleF bound) : base(parent.A141)
        {
            base._A154 = name;
            this.Bounds = bound;
            this._b0 = parent;
            this.BorderColor = PdfColor.DarkGray;
        }

        internal override void A110()
        {
            base.A110();
            A93 A1 = base._A92.A93;
            A1.A95++;
        }

        internal override void A119(ref A120 b1, ref A112 b2)
        {
            base._A92 = b1.A97;
            b1.A164(this, false, true);
            float x = this.Bounds.X;
            float num2 = b1.A98(this.Bounds.Y);
            float num3 = this.Bounds.Width / 2f;
            num2 -= num3;
            x += num3;
            b2.A59("q ");
            b2.A59(this.BorderColor.A169(true));
            b2.A59("0.50 w");
            GraphicsElement.A183(ref b2, x, num2, num3, num3, GraphicsMode.stroke);
            b2.A59("Q");
        }

        internal override void A54(ref A55 b3)
        {
            int num = this.A95 + 1;
            int num2 = this.A95 + 2;
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(this.A95, 0);
            }
            A93 A2 = base._A92.A93;
            A2.A94(b3.A2, 0);
            b3.A59(string.Format("{0} 0 obj", this.A95));
            b3.A59("<<");
            b3.A59("/Type /Annot");
            b3.A59("/Subtype /Widget");
            string str = base.A165(num2);
            if ((str != null) && (str.Length > 0))
            {
                b3.A59(str);
            }
            if (this._A182)
            {
                b3.A59(string.Format("/AS /{0} ", base.Name));
            }
            else
            {
                b3.A59("/AS /Off");
            }
            b3.A59("/Border [0 0 0]");
            float num3 = base.A141.A98(this.Bounds.Y);
            b3.A59(string.Format("/Rect {0}", A15.A21(this.Bounds.X, num3 - this.Bounds.Height, this.Bounds.Right, num3)));
            b3.A59(string.Format("/AP << /N << /{0} {1} 0 R >> >>", base.Name, num));
            b3.A59("/MK <<>>");
            b3.A59(string.Format("/Parent {0} 0 R", this._b0.A95));
            b3.A59(string.Format("/P {0} 0 R", base.A141.A95));
            b3.A59("/F 4");
            b3.A59(">>");
            b3.A59("endobj");
            A112 A3 = this.b4();
            A2.A94(b3.A2, 0);
            b3.A59(string.Format("{0} 0 obj", num));
            b3.A59("<<");
            b3.A59("/Type /XObject ");
            b3.A59("/Subtype /Form ");
            b3.A59(string.Format("/Name /AS{0} ", num));
            if (A3.Compressed)
            {
                b3.A59(string.Format("/Length {0} /Filter /FlateDecode ", A3.A2));
            }
            else
            {
                b3.A59(string.Format("/Length {0} ", A3.A2));
            }
            b3.A59("/Resources << ");
            b3.A59("/ProcSet [/Pdf /Text /ImageC]");
            b3.A59(">>");
            b3.A59(string.Format("/BBox [0 0 {0} {1}]", A15.A18(this.Bounds.Width), A15.A18(this.Bounds.Height)));
            b3.A59(">>");
            A3.A114(b3, num, A);
            b3.A59("endobj");
            base.A166(num2, ref b3);
        }

        private A112 b4()
        {
            A112 A = new A112();
            float num = this.Bounds.Width / 2f;
            float num2 = this.Bounds.Height / 2f;
            float num3 = (this.Bounds.Width / 2f) - 2f;
            A.A59(string.Format("0 0 {0} {1} re W n ", A15.A18(this.Bounds.Width), A15.A18(this.Bounds.Height)));
            A.A59("q ");
            A.A59("0 0 0 RG");
            A.A59("0 0 0 rg");
            A.A59("0.50 w");
            GraphicsElement.A183(ref A, num, num2, num3, num3, GraphicsMode.fill | GraphicsMode.stroke);
            A.A59("Q");
            if (base._A92.Compress)
            {
                A.A123();
            }
            return A;
        }

        internal override string A155
        {
            get
            {
                return string.Empty;
            }
        }

        public bool Checked
        {
            get
            {
                return this._A182;
            }
        }
    }
}

