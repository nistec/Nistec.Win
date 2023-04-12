namespace MControl.Printing.Pdf.Core.Fonts
{
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A246 : A252
    {
        internal A246()
        {
            base._A253 = 0x341;
            base._A254 = 300;
            base._A255 = 0x11;
        }

        internal override float A256(char b0)
        {
            return 600f;
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
            base._A92.A93.A94(b3.A2, 0);
            b3.A59(string.Format("{0} 0 obj", base.A95));
            b3.A59("<<");
            b3.A59("/Type /Font ");
            b3.A59(string.Format("/BaseFont /{0} ", base._A247));
            b3.A59("/Subtype /Type1 ");
            b3.A59(string.Format("/Name /{0} ", base._A259));
            b3.A59("/Encoding /WinAnsiEncoding ");
            b3.A59(">>");
            b3.A59("endobj");
        }
    }
}

