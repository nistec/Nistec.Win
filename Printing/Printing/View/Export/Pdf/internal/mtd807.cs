namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Text;

    internal abstract class mtd807 : mtd811
    {
        internal static Encoding mtd645 = Encoding.GetEncoding(0x4e4);

        internal mtd807()
        {
            base._mtd812 = 900;
            base._mtd813 = 220;
            base._mtd814 = 30;
        }

        protected abstract int _mtd810(int var0);
        internal override void mtd710(ref mtd711 var4)
        {
            base._mtd756.mtd757.mtd758(var4.mtd32, 0);
            var4.mtd715(string.Format("{0} 0 obj", base.mtd759));
            var4.mtd715("<<");
            var4.mtd715("/Type /Font ");
            var4.mtd715(string.Format("/BaseFont /{0} ", base._mtd808));
            var4.mtd715("/Subtype /Type1 ");
            var4.mtd715(string.Format("/Name /{0} ", base._mtd818));
            var4.mtd715("/Encoding /WinAnsiEncoding ");
            var4.mtd715(">>");
            var4.mtd715("endobj");
        }

        internal override float mtd815(char var1)
        {
            byte[] bytes = mtd645.GetBytes(new char[] { var1 });
            return (float) this._mtd810(bytes[0]);
        }

        internal override float mtd816(char var1, float var2)
        {
            return ((this.mtd815(var1) / 1000f) * var2);
        }

        internal override float mtd817(string var3, float var2)
        {
            float num = 0f;
            for (int i = 0; i < var3.Length; i++)
            {
                num += this.mtd816(var3[i], var2);
            }
            return num;
        }
    }
}

