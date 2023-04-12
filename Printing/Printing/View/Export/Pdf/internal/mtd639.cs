namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd639 : mtd747
    {
        private int _var0;
        private float _var1;
        private float _var2;

        internal mtd639(float var3, float var4, int var0, PDFDocument var5)
        {
            base._mtd756 = var5;
            this._var1 = var3;
            this._var2 = var4;
            this._var0 = var0;
        }

        internal override void mtd710(ref mtd711 var6)
        {
            base._mtd756.mtd757.mtd758(var6.mtd32, 0);
            var6.mtd715(string.Format("{0} 0 obj", this.mtd759));
            var6.mtd715("<<");
            var6.mtd715("/Type /ExtGState ");
            var6.mtd715(string.Format("/CA {0} ", mtd620.mtd621(this.mtd764)));
            var6.mtd715(string.Format("/ca {0} ", mtd620.mtd621(this.mtd765)));
            var6.mtd715(">>");
            var6.mtd715("endobj");
        }

        internal static bool mtd766(mtd639 var7, mtd639 var8)
        {
            return ((var7 == var8) || (((var7 != null) && (var8 != null)) && ((var7.mtd764 == var8.mtd764) && (var7.mtd765 == var8.mtd765))));
        }

        internal override string mtd763
        {
            get
            {
                return Convert.ToString(this._var0);
            }
        }

        internal float mtd764
        {
            get
            {
                return this._var1;
            }
        }

        internal float mtd765
        {
            get
            {
                return this._var2;
            }
        }
    }
}

