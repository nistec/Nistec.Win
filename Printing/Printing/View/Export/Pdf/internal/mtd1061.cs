namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;
    using System.IO;

    internal class mtd1061
    {
        private mtd640 _var0;
        private mtd638 _var1;
        private mtd642 _var2;
        private mtd1062 _var3;
        private PDFDocument _var4;

        internal mtd1061(PDFDocument var4)
        {
            this._var4 = var4;
            this._var0 = new mtd640();
            this._var1 = new mtd638();
            this._var2 = new mtd642();
            this._var3 = new mtd1062();
        }

        internal mtd641 mtd1068(mtd641 var6)
        {
            for (int i = 0; i < this._var0.mtd32; i++)
            {
                if (mtd641.mtd766(this._var0[i], var6))
                {
                    return this._var0[i];
                }
            }
            var6.mtd172(this._var4, this._var0.mtd32);
            var6.mtd780();
            this._var0.mtd2(var6);
            this._var3.mtd2(var6);
            return var6;
        }

        internal mtd641 mtd1068(Font var5)
        {
            mtd641 mtd = null;
            for (int i = 0; i < this._var0.mtd32; i++)
            {
                mtd = this._var0[i];
                if (mtd641.mtd766(mtd, var5))
                {
                    return mtd;
                }
            }
            mtd = null;
            if ((this._var4.UseLocalFont && (PDFDocument.mtd1060 != null)) && (this._var4.ResourcePath != null))
            {
                string path = PDFDocument.mtd1060.mtd1069(var5);
                if (path == null)
                {
                    return this._var4.mtd1003;
                }
                path = this._var4.ResourcePath + path;
                if (!File.Exists(path))
                {
                    return this._var4.mtd1003;
                }
                mtd = new mtd641(var5.Name, path, var5.Style, this._var4.EmbedFont);
            }
            else
            {
                mtd = new mtd641(var5, this._var4.EmbedFont);
            }
            mtd.mtd172(this._var4, this._var0.mtd32);
            mtd.mtd780();
            this._var0.mtd2(mtd);
            this._var3.mtd2(mtd);
            return mtd;
        }

        internal mtd639 mtd1070(float var7, float var8)
        {
            mtd639 mtd = new mtd639(var7, var8, this._var1.mtd32, this._var4);
            if (mtd != null)
            {
                for (int i = 0; i < this._var1.mtd32; i++)
                {
                    if (mtd639.mtd766(this._var1[i], mtd))
                    {
                        return this._var1[i];
                    }
                }
                mtd.mtd780();
                this._var1.mtd2(mtd);
                this._var3.mtd2(mtd);
            }
            return mtd;
        }

        internal void mtd387()
        {
            this._var0.mtd387();
            this._var1.mtd387();
            this._var2.mtd387();
            this._var3.mtd387();
        }

        internal mtd643 mtd967(Image var9)
        {
            if (var9 == null)
            {
                return null;
            }
            for (int i = 0; i < this._var2.mtd32; i++)
            {
                if (this._var2[i].mtd9 == var9)
                {
                    return this._var2[i];
                }
            }
            mtd643 mtd = new mtd643(var9, this._var2.mtd32, this._var4);
            mtd.mtd780();
            this._var2.mtd2(mtd);
            this._var3.mtd2(mtd);
            return mtd;
        }

        internal mtd1062 mtd1063
        {
            get
            {
                return this._var3;
            }
        }
    }
}

