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

    public class PdfCheckBox : PdfField
    {
        private bool _b0;
        private PdfFont _b1;
        private float _b2;

        internal PdfCheckBox(Page page, string name, RectangleF bound) : base(page)
        {
            base._A154 = name;
            this.Bounds = bound;
            this._b1 = new PdfFont(StandardFonts.ZapfDingbats, FontStyle.Regular);
            this._b2 = 7.2f;
        }

        internal override void A110()
        {
            base.A110();
            A93 A1 = base._A92.A93;
            A1.A95++;
        }

        internal override void A119(ref A120 b3, ref A112 b4)
        {
            this._b1 = b3.A167(this._b1);
            PdfField.A156(ref b3, ref b4, this.Bounds, base.Backcolor, this.BorderColor, this.BorderStyle, false);
            base._A92 = b3.A97;
            b3.A164(this, true, true);
        }

        internal override void A54(ref A55 b5)
        {
            int num = this.A95 + 1;
            int num2 = this.A95 + 2;
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(this.A95, 0);
            }
            A93 A2 = base._A92.A93;
            A2.A94(b5.A2, 0);
            b5.A59(string.Format("{0} 0 obj", this.A95));
            b5.A59("<<");
            b5.A59("/FT /Btn");
            b5.A54("/T ");
            if (A != null)
            {
                A26.A54(ref b5, base.Name, A);
            }
            else
            {
                A26.A54(ref b5, A15.A26(base.Name), A);
            }
            if (this._b0)
            {
                b5.A59("/V /On");
                b5.A59("/AS /On");
            }
            else
            {
                b5.A59("/V /Off");
                b5.A59("/AS /Off");
            }
            b5.A59(string.Format("/Ff {0} ", A15.A18((float) ((long) base.A153))));
            if (base.ToolTip != null)
            {
                b5.A54("/TU ");
                if (A != null)
                {
                    A26.A54(ref b5, base.ToolTip, A);
                }
                else
                {
                    A26.A54(ref b5, A15.A26(base.ToolTip), A);
                }
            }
            b5.A59("/Type /Annot");
            b5.A59("/Subtype /Widget");
            string str = base.A165(num2);
            if ((str != null) && (str.Length > 0))
            {
                b5.A59(str);
            }
            float num3 = base.A141.A98(this.Bounds.Y);
            b5.A59(string.Format("/Rect {0}", A15.A21(this.Bounds.X, num3 - this.Bounds.Height, this.Bounds.Right, num3)));
            b5.A59(string.Format("/AP << /N << /On {0} 0 R >> >>", num));
            b5.A59("/MK <<>>");
            b5.A59(string.Format("/P {0} 0 R", base.A141.A95));
            b5.A59("/F 4");
            b5.A59(">>");
            b5.A59("endobj");
            A112 A3 = this.b6();
            A2.A94(b5.A2, 0);
            b5.A59(string.Format("{0} 0 obj", num));
            b5.A59("<<");
            b5.A59("/Type /XObject ");
            b5.A59("/Subtype /Form ");
            b5.A59(string.Format("/Name /AS{0} ", num));
            if (A3.Compressed)
            {
                b5.A59(string.Format("/Length {0} /Filter /FlateDecode ", A3.A2));
            }
            else
            {
                b5.A59(string.Format("/Length {0} ", A3.A2));
            }
            b5.A59("/Resources << ");
            b5.A59("/ProcSet [/Pdf /Text /ImageC]");
            b5.A59(string.Format("/Font << /{0} {1} 0 R >>", this._b1.A168, this._b1.A95));
            b5.A59(">>");
            b5.A59(string.Format("/BBox [0 0 {0} {1}]", A15.A18(this.Bounds.Width), A15.A18(this.Bounds.Height)));
            b5.A59(">>");
            A3.A114(b5, num, A);
            b5.A59("endobj");
            base.A166(num2, ref b5);
        }

        private A112 b6()
        {
            A112 A = new A112();
            float textWidth = this._b1.GetTextWidth("4", this._b2);
            float num2 = this._b1.Height(this._b2);
            float num3 = (this.Bounds.Width - textWidth) / 2f;
            float num4 = num2 + 2f;
            A.A59(string.Format("0 0 {0} {1} re W n ", A15.A18(this.Bounds.Width), A15.A18(this.Bounds.Height)));
            A.A59("q ");
            A.A59("BT");
            A.A59(string.Format("/{0} {1} Tf ", this._b1.A168, A15.A18(this._b2)));
            A.A59(base.Forecolor.A169(false));
            GraphicsElement.A170(num3, num4, ref A);
            A.A59("(4) Tj");
            A.A59("ET");
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
                return "Btn";
            }
        }

        public bool Checked
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

        public float CheckSize
        {
            get
            {
                return this._b2;
            }
            set
            {
                if (value < 7.5f)
                {
                    value = 7.5f;
                }
                this._b2 = value;
            }
        }
    }
}

