namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;
    using System.Reflection;

    internal class mtd1059
    {
        private mtd1071[] _var0 = new mtd1071[10];
        private int _var1 = 0;
        //private mtd776 _var2;

        internal mtd1059()
        {
        }

        internal string mtd1069(Font var9)
        {
            mtd1071 mtd = this.var7(var9.Name);
            if (mtd == null)
            {
                if (var9.Style == FontStyle.Regular)
                {
                    return mtd.mtd1072;
                }
                if ((var9.Style & FontStyle.Bold) == FontStyle.Bold)
                {
                    if ((var9.Style & FontStyle.Italic) == FontStyle.Italic)
                    {
                        return mtd.mtd1075;
                    }
                    return mtd.mtd1073;
                }
                if (var9.Style == FontStyle.Italic)
                {
                    return mtd.mtd1074;
                }
            }
            return null;
        }

        internal void mtd2(string var4, FontStyle var5, string var6)
        {
            mtd1071 mtd = this.var7(var4);
            if (mtd == null)
            {
                mtd = new mtd1071(var4);
                this.var8();
                this._var0[this._var1] = mtd;
                this._var1++;
            }
            if (var5 == FontStyle.Regular)
            {
                mtd.mtd1072 = var6;
            }
            else if ((var5 & FontStyle.Bold) == FontStyle.Bold)
            {
                if ((var5 & FontStyle.Italic) == FontStyle.Italic)
                {
                    mtd.mtd1075 = var6;
                }
                else
                {
                    mtd.mtd1073 = var6;
                }
            }
            else if (var5 == FontStyle.Italic)
            {
                mtd.mtd1074 = var6;
            }
        }

        private mtd1071 var7(string var4)
        {
            for (int i = 0; i < this._var1; i++)
            {
                mtd1071 mtd = this._var0[i];
                if (mtd.mtd1076 == var4)
                {
                    return mtd;
                }
            }
            return null;
        }

        private void var8()
        {
            if (this._var1 >= this._var0.Length)
            {
                mtd1071[] sourceArray = this._var0;
                this._var0 = new mtd1071[2 * this._var0.Length];
                Array.Copy(sourceArray, this._var0, this._var1);
            }
        }

        internal int mtd166
        {
            get
            {
                return this._var1;
            }
        }

        internal mtd1071 this[int var3]
        {
            get
            {
                return this._var0[var3];
            }
        }
    }
}

