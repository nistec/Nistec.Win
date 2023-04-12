namespace MControl.Printing.Pdf.Core.Fonts
{
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Text;

    internal abstract class A261 : A252
    {
        internal static Encoding A17 = Encoding.GetEncoding(0x4e4);

        internal A261()
        {
            base._A253 = 900;
            base._A254 = 220;
            base._A255 = 30;
        }

        protected abstract int _A262(int b0);
        internal override float A256(char b1)
        {
            byte[] bytes = A17.GetBytes(new char[] { b1 });
            return (float) this._A262(bytes[0]);
        }

        internal override float A257(char b1, float b2)
        {
            return ((this.A256(b1) / 1000f) * b2);
        }

        internal override float A258(string b3, float b2)
        {
            float num = 0f;
            for (int i = 0; i < b3.Length; i++)
            {
                num += this.A257(b3[i], b2);
            }
            return num;
        }

        internal override void A54(ref A55 b4)
        {
            base._A92.A93.A94(b4.A2, 0);
            b4.A59(string.Format("{0} 0 obj", base.A95));
            b4.A59("<<");
            b4.A59("/Type /Font ");
            b4.A59(string.Format("/BaseFont /{0} ", base._A247));
            b4.A59("/Subtype /Type1 ");
            b4.A59(string.Format("/Name /{0} ", base._A259));
            b4.A59("/Encoding /WinAnsiEncoding ");
            b4.A59(">>");
            b4.A59("endobj");
        }
    }
}

