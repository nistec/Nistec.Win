namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Collections;
    using System.Drawing;

    internal class mtd944
    {
        private mtd1062 _var0;
        private mtd1062 _var1;
        private mtd1062 _var2;
        private mtd1061 _var3;
        private mtd984 _var4;
        private Stack _var5;
        private mtd1055 _var6;

        internal mtd944(mtd1055 var6, ref mtd1061 var7)
        {
            this._var3 = var7;
            this._var6 = var6;
            this._var0 = new mtd1062();
            this._var1 = new mtd1062();
            this._var2 = new mtd1062();
            this._var4 = new mtd984();
            this._var5 = new Stack();
        }

        internal mtd639 mtd1070(float var9, float var10)
        {
            mtd639 mtd = this._var3.mtd1070(var9, var10);
            for (int i = 0; i < this._var1.mtd32; i++)
            {
                if (this._var1[i] == mtd)
                {
                    return mtd;
                }
            }
            this._var1.mtd2(mtd);
            return mtd;
        }

        internal void mtd945()
        {
            this._var5.Push(this._var4.mtd253());
        }

        internal float mtd947(float var12)
        {
            return (this._var6.mtd31 - var12);
        }

        internal void mtd951()
        {
            this._var4 = (mtd984) this._var5.Pop();
        }

        internal mtd641 mtd953(mtd641 var8)
        {
            return this._var3.mtd1068(var8);
        }

        internal mtd641 mtd953(Font var8)
        {
            return this._var3.mtd1068(var8);
        }

        internal mtd643 mtd967(Image var11)
        {
            mtd643 mtd = this._var3.mtd967(var11);
            for (int i = 0; i < this._var2.mtd32; i++)
            {
                if (this._var2[i] == mtd)
                {
                    return mtd;
                }
            }
            this._var2.mtd2(mtd);
            return mtd;
        }

        internal mtd641 mtd986(mtd641 pdffont)
        {
            for (int i = 0; i < this._var0.mtd32; i++)
            {
                if (this._var0[i] == pdffont)
                {
                    return pdffont;
                }
            }
            this._var0.mtd2(pdffont);
            return pdffont;
        }

        internal PDFDocument mtd1064
        {
            get
            {
                return this._var6.mtd1064;
            }
        }

        internal mtd1062 mtd1066
        {
            get
            {
                return this._var0;
            }
        }

        internal float mtd30
        {
            get
            {
                return this._var6.mtd30;
            }
        }

        internal float mtd31
        {
            get
            {
                return this._var6.mtd31;
            }
        }

        internal mtd1062 mtd638
        {
            get
            {
                return this._var1;
            }
        }

        internal mtd1062 mtd642
        {
            get
            {
                return this._var2;
            }
        }

        internal mtd984 mtd985
        {
            get
            {
                return this._var4;
            }
        }

        internal Stack mtd993
        {
            get
            {
                return this._var5;
            }
        }
    }
}

