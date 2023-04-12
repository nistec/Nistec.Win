namespace MControl.Printing.Pdf.Core.Fonts
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    internal class A278 : A252
    {
        internal A278()
        {
            base._A247 = "HYGoThic-Medium";
            base._A253 = 880;
            base._A254 = 120;
            base._A255 = 3;
            base._A248 = FontStyle.Regular;
        }

        internal override void A110()
        {
            int num;
            A93 A1 = base._A92.A93;
            A1.A95 = (num = A1.A95) + 1;
            base._A95 = num;
            base._A92.A93.A95 = base._A95 + 3;
        }

        internal override string A163(string b2)
        {
            return A15.A28(b2);
        }

        internal override float A256(char b0)
        {
            if (b0 > '_')
            {
                if (b0 < 'ᾞ')
                {
                    return 1000f;
                }
                if (b0 > '῾')
                {
                    return 1000f;
                }
            }
            return 500f;
        }

        internal override float A257(char b0, float b1)
        {
            return ((this.A256(b0) / 1000f) * b1);
        }

        internal override float A258(string b2, float b1)
        {
            float num = 0f;
            for (int i = 0; i < b2.Length; i++)
            {
                num += this.A257(b2[i], b1);
            }
            return num;
        }

        internal override void A54(ref A55 b3)
        {
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(base.A95 + 1, 0);
            }
            A93 A2 = base._A92.A93;
            A2.A94(b3.A2, 0);
            b3.A59(string.Format("{0} 0 obj", base.A95));
            b3.A59("<<");
            b3.A59("/Type/Font ");
            b3.A59("/Subtype/Type0");
            b3.A59(string.Format("/Name /{0} ", base._A259));
            b3.A59("/BaseFont/HYGoThic-Medium");
            b3.A59("/Encoding/UniKS-UCS2-H");
            b3.A59(string.Format("/DescendantFonts[{0} 0 R]", base.A95 + 1));
            b3.A59(">>");
            b3.A59("endobj");
            A2.A94(b3.A2, 0);
            b3.A59(string.Format("{0} 0 obj", base.A95 + 1));
            b3.A59("<<");
            b3.A59("/Type/Font ");
            b3.A59("/Subtype/CIDFontType2");
            b3.A59("/BaseFont/HYGoThic-Medium");
            b3.A54("/CIDSystemInfo << /Registry ");
            A26.A54(ref b3, "Adobe", A);
            b3.A54("/Ordering ");
            A26.A54(ref b3, "Korea1", A);
            b3.A59("/Supplement 1 >>");
            b3.A59(string.Format("/FontDescriptor {0} 0 R", base.A95 + 2));
            b3.A59("/W[1 95 500 8094 8190 500]");
            b3.A59("/DW 1000");
            b3.A59(">>");
            b3.A59("endobj");
            A2.A94(b3.A2, 0);
            b3.A59(string.Format("{0} 0 obj", base.A95 + 2));
            b3.A59("<<");
            b3.A59("/Type/FontDescriptor");
            b3.A59("/FontBBox[-6 -145 1003 880]");
            b3.A59(">>");
            b3.A59("endobj");
        }
    }
}

