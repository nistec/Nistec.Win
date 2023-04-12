namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;

    internal class mtd641 : mtd747
    {
        private mtd811 _var0;

        internal mtd641(StandardFonts var1, FontStyle var5)
        {
            if (var1 == StandardFonts.mtd684)
            {
                this._var0 = new mtd684(FontStyle.Regular);
            }
            else if (var1 == StandardFonts.mtd688)
            {
                this._var0 = new mtd688(var5);
            }
        }

        internal mtd641(Font var1, bool var2)
        {
            this._var0 = new mtd931(var1, var2);
        }

        internal mtd641(string var3, string var4, FontStyle var5, bool var2)
        {
            this._var0 = new mtd931(var3, var4, var5, var2);
        }

        internal float mtd1038(float var8)
        {
            return ((this._var0.mtd939 * var8) / 1000f);
        }

        internal float mtd1039(float var8)
        {
            return ((this._var0.mtd940 * var8) / 1000f);
        }

        internal float mtd1040(float var8)
        {
            return ((this._var0.mtd941 * var8) / 1000f);
        }

        internal void mtd172(PDFDocument var6, int var7)
        {
            this._var0.mtd172(var6, var7);
        }

        internal float mtd31(float var8)
        {
            return ((((float) (this._var0.mtd939 + this._var0.mtd940)) / 1000f) * var8);
        }

        internal override void mtd710(ref mtd711 var12)
        {
            this._var0.mtd710(ref var12);
        }

        internal static bool mtd766(mtd641 var13, mtd641 var14)
        {
            if ((var13 == null) || (var14 == null))
            {
                return false;
            }
            return ((var13 == var14) || ((((var13.Font != null) && (var14.Font != null)) && (var13.Font == var14.Font)) || ((string.Compare(var13.mtd886, var14.mtd886) == 0) && (var13.mtd934 == var14.mtd934))));
        }

        internal static bool mtd766(mtd641 var13, Font var14)
        {
            if ((var13 == null) || (var14 == null))
            {
                return false;
            }
            return ((((var13.Font != null) && (var14 != null)) && (var13.Font == var14)) || ((string.Compare(var13.mtd886, var14.Name) == 0) && (var13.mtd934 == var14.Style)));
        }

        internal override void mtd780()
        {
            this._var0.mtd780();
        }

        internal float mtd815(char var9)
        {
            return this._var0.mtd815(var9);
        }

        internal float mtd815(ushort var10)
        {
            return this._var0.mtd815(var10);
        }

        internal float mtd816(char var9, float var8)
        {
            return this._var0.mtd816(var9, var8);
        }

        internal float mtd817(string var11, float var8)
        {
            return this._var0.mtd817(var11, var8);
        }

        internal void mtd935(char var9)
        {
            this._var0.mtd935(var9);
        }

        internal void mtd936(string var11)
        {
            this._var0.mtd936(var11);
        }

        internal string mtd937(string var11)
        {
            return this._var0.mtd937(var11);
        }

        internal Font Font//mtd132
        {
            get
            {
                return this._var0.Font;
            }
        }

        internal override int mtd759
        {
            get
            {
                return this._var0.mtd759;
            }
        }

        internal override string mtd763
        {
            get
            {
                return this._var0.mtd763;
            }
        }

        internal string mtd886
        {
            get
            {
                return this._var0.mtd886;
            }
        }

        internal FontStyle mtd934
        {
            get
            {
                return this._var0.mtd934;
            }
        }

        internal int mtd939
        {
            get
            {
                return this._var0.mtd939;
            }
        }

        internal int mtd940
        {
            get
            {
                return this._var0.mtd940;
            }
        }
    }
}

