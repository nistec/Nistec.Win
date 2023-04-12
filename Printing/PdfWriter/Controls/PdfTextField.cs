namespace MControl.Printing.Pdf.Controls
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Controls;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;
    using System.Text;

    public class PdfTextField : PdfField
    {
        private PdfFont _b0;
        private float _b1;
        private int _b2;
        private TextAlignment _b3;

        internal PdfTextField(Page page, string name, string value, RectangleF bound, PdfFont font, float fontsize) : base(page)
        {
            base._A154 = name;
            base.Value = value;
            this.Bounds = bound;
            this._b0 = font;
            this._b1 = fontsize;
        }

        internal override void A119(ref A120 b5, ref A112 b6)
        {
            base.A173 = this.b7(ref b5);
            if (base.Value != null)
            {
                this._b0.A159(base.Value);
            }
            PdfField.A156(ref b5, ref b6, this.Bounds, base.Backcolor, this.BorderColor, this.BorderStyle, false);
            base._A92 = b5.A97;
            b5.A164(this, true, true);
        }

        internal static int A184(TextAlignment align)
        {
            if (align != TextAlignment.Left)
            {
                if (align == TextAlignment.Center)
                {
                    return 1;
                }
                if (align == TextAlignment.Right)
                {
                    return 2;
                }
            }
            return 0;
        }

        internal override void A54(ref A55 b4)
        {
            int num = this.A95 + 1;
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(this.A95, 0);
            }
            base._A92.A93.A94(b4.A2, 0);
            b4.A59(string.Format("{0} 0 obj", this.A95));
            b4.A59("<<");
            b4.A59("/FT /Tx");
            b4.A54("/T ");
            if (A != null)
            {
                A26.A54(ref b4, base.Name, A);
            }
            else
            {
                A26.A54(ref b4, A15.A26(base.Name), A);
            }
            b4.A54("/V ");
            if (A != null)
            {
                A26.A54(ref b4, base.Value, A);
            }
            else
            {
                A26.A54(ref b4, A15.A26(base.Value), A);
            }
            b4.A59(string.Format("/Ff {0} ", A15.A18((float) ((long) base.A153))));
            b4.A54("/TU ");
            if (A != null)
            {
                A26.A54(ref b4, base.ToolTip, A);
            }
            else
            {
                A26.A54(ref b4, A15.A26(base.ToolTip), A);
            }
            b4.A54("/DA ");
            if (A != null)
            {
                A26.A54(ref b4, base.A173, A);
            }
            else
            {
                A26.A54(ref b4, A15.A26(base.A173), A);
            }
            if (this._b2 != 0)
            {
                b4.A59(string.Format("/MaxLen {0}", this._b2));
            }
            b4.A59(string.Format("/Q {0}", A184(this._b3)));
            b4.A59("/Type /Annot");
            b4.A59("/Subtype /Widget");
            string str = base.A165(num);
            if ((str != null) && (str.Length > 0))
            {
                b4.A59(str);
            }
            b4.A59("/MK <<>>");
            float num2 = base.A141.A98(this.Bounds.Y);
            b4.A59(string.Format("/Rect {0}", A15.A21(this.Bounds.X, num2 - this.Bounds.Height, this.Bounds.Right, num2)));
            b4.A59(string.Format("/P {0} 0 R", base.A141.A95));
            b4.A59("/F 4");
            b4.A59(">>");
            b4.A59("endobj");
            base.A166(num, ref b4);
        }

        private string b7(ref A120 b5)
        {
            StringBuilder builder = new StringBuilder();
            this._b0 = GraphicsElement.A157(this._b0, this._b1, ref b5, ref builder);
            builder.Append("\r\n");
            builder.Append(base.Forecolor.A169(false));
            return builder.ToString();
        }

        internal override string A155
        {
            get
            {
                return "Tx";
            }
        }

        public PdfFont Font
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

        public float FontSize
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

        public int MaxLength
        {
            get
            {
                return this._b2;
            }
            set
            {
                if (value > 0)
                {
                    this._b2 = value;
                }
            }
        }

        public bool MultiLine
        {
            get
            {
                return ((base.A153 & A127.Multiline) != A127.Default);
            }
            set
            {
                if (value)
                {
                    base.A153 |= A127.Multiline;
                }
                else
                {
                    base.A153 &= ~A127.Multiline;
                }
            }
        }

        public bool Password
        {
            get
            {
                return ((base.A153 & A127.Password) != A127.Default);
            }
            set
            {
                if (value)
                {
                    base.A153 |= A127.Password;
                }
                else
                {
                    base.A153 &= ~A127.Password;
                }
            }
        }

        public TextAlignment TextAlign
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

