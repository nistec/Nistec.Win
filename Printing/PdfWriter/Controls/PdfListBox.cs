namespace MControl.Printing.Pdf.Controls
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Text;

    public class PdfListBox : PdfChoiceField
    {
        internal PdfListBox(Page page, string name, string[] items, RectangleF bound, PdfFont font, float fontsize) : base(page, name, items, bound, font, fontsize)
        {
        }

        internal override void A110()
        {
            base.A110();
            A93 A1 = base._A92.A93;
            A1.A95++;
        }

        internal override void A119(ref A120 b0, ref A112 b1)
        {
            if (base.Sorted)
            {
                Array.Sort(base.Items, Comparer.Default);
            }
            int length = base.Items.Length;
            string str = (length == 0) ? string.Empty : base.Items[0];
            base.Value = str;
            base.DefaultValue = str;
            base.A173 = this.b2(ref b0);
            for (int i = 0; i < length; i++)
            {
                str = base.Items[i];
                base._A171.A159(str);
            }
            PdfField.A156(ref b0, ref b1, this.Bounds, base.Backcolor, this.BorderColor, this.BorderStyle, false);
            base._A92 = b0.A97;
            b0.A164(this, true, true);
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
            b3.A59("/FT /Ch");
            b3.A54("/T ");
            if (A != null)
            {
                A26.A54(ref b3, base.Name, A);
            }
            else
            {
                A26.A54(ref b3, A15.A26(base.Name), A);
            }
            b3.A54("/V ");
            if (A != null)
            {
                A26.A54(ref b3, base.Value, A);
            }
            else
            {
                A26.A54(ref b3, A15.A26(base.Value), A);
            }
            b3.A54("/DV ");
            if (A != null)
            {
                A26.A54(ref b3, base.DefaultValue, A);
            }
            else
            {
                A26.A54(ref b3, A15.A26(base.DefaultValue), A);
            }
            int length = base.Items.Length;
            b3.A54("/Opt [ ");
            for (int i = 0; i < length; i++)
            {
                if (A != null)
                {
                    A26.A54(ref b3, base.Items[i], A, false);
                }
                else
                {
                    A26.A54(ref b3, A15.A26(base.Items[i]), A, false);
                }
            }
            b3.A59("]");
            b3.A59(string.Format("/Ff {0} ", A15.A18((float) ((long) base.A153))));
            b3.A54("/TU ");
            if (A != null)
            {
                A26.A54(ref b3, base.ToolTip, A);
            }
            else
            {
                A26.A54(ref b3, A15.A26(base.ToolTip), A);
            }
            b3.A54("/DA ");
            if (A != null)
            {
                A26.A54(ref b3, base.A173, A);
            }
            else
            {
                A26.A54(ref b3, A15.A26(base.A173), A);
            }
            b3.A59("/Type /Annot");
            b3.A59("/Subtype /Widget");
            string str = base.A165(num2);
            if ((str != null) && (str.Length > 0))
            {
                b3.A59(str);
            }
            float num5 = base.A141.A98(this.Bounds.Y);
            b3.A59(string.Format("/Rect {0}", A15.A21(this.Bounds.X, num5 - this.Bounds.Height, this.Bounds.Right, num5)));
            b3.A59(string.Format("/AP << /N {0} 0 R >>", num));
            b3.A59("/MK <<>>");
            b3.A59(string.Format("/P {0} 0 R", base.A141.A95));
            b3.A59("/F 4");
            b3.A59(">>");
            b3.A59("endobj");
            string s = string.Format("0 0 {0} {1} re W n", A15.A18(this.Bounds.Width), A15.A18(this.Bounds.Height));
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            A2.A94(b3.A2, 0);
            b3.A59(string.Format("{0} 0 obj", num));
            b3.A59("<<");
            b3.A59("/Type /XObject ");
            b3.A59("/Subtype /Form ");
            b3.A59(string.Format("/Name /AS{0} ", num));
            b3.A59(string.Format("/Length {0}", bytes.Length));
            b3.A59("/Resources << ");
            b3.A59("/ProcSet [/Pdf /Text /ImageC]");
            b3.A59(string.Format("/Font << /{0} {1} 0 R >>", base._A171.A168, base._A171.A95));
            b3.A59(">>");
            b3.A59(string.Format("/BBox [0 0 {0} {1}]", A15.A18(this.Bounds.Width), A15.A18(this.Bounds.Height)));
            b3.A59(">>");
            b3.A181("stream");
            if (A != null)
            {
                A.A100(num, 0);
                bytes = A.A57(bytes, bytes.Length);
                b3.A54(bytes, bytes.Length);
            }
            else
            {
                b3.A54(bytes, bytes.Length);
            }
            b3.A54((byte) 13);
            b3.A59("endstream");
            b3.A59("endobj");
            base.A166(num2, ref b3);
        }

        private string b2(ref A120 b0)
        {
            StringBuilder builder = new StringBuilder();
            base._A171 = GraphicsElement.A157(base._A171, base._A172, ref b0, ref builder);
            builder.Append("\r\n");
            builder.Append(base.Forecolor.A169(false));
            return builder.ToString();
        }
    }
}

