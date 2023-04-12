namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd1020
    {
        private float _var0;
        private Nistec.Printing.View.Pdf.mtd1015 _var1;
        private mtd702 _var2;
        private mtd1000 _var3;
        private mtd1011 _var4;

        internal mtd1020(mtd702 var2, Nistec.Printing.View.Pdf.mtd1015 var1)
        {
            this._var0 = 0f;
            this._var2 = var2;
            this._var1 = var1;
            this._var3 = var1.mtd934;
            this._var4 = new mtd1011();
        }

        internal mtd1020(mtd702 var2, Nistec.Printing.View.Pdf.mtd1015 var1, mtd1012[] var5, float var0)
        {
            this._var0 = var0;
            this._var2 = var2;
            this._var1 = var1;
            this._var3 = var1.mtd934;
            this._var4 = new mtd1011(var5);
        }

        internal void mtd1042(int var6, char var7)
        {
            float num = this._var3.Font.mtd816(var7, this._var3.mtd997);
            this._var0 += num;
            this._var4.mtd2(new mtd1012(var6, var7, num));
        }

        internal bool mtd1005
        {
            get
            {
                if (!this._var3.mtd1005 && (this._var1.mtd1041 == null))
                {
                    return false;
                }
                return true;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd1015 mtd1015
        {
            get
            {
                return this._var1;
            }
        }

        internal bool mtd1030
        {
            get
            {
                return (this._var2 == mtd702.mtd703);
            }
        }

        internal float mtd1031
        {
            set
            {
                this._var0 += value;
            }
        }

        internal mtd1011 mtd1032
        {
            get
            {
                return this._var4;
            }
        }

        internal bool mtd1036
        {
            get
            {
                if (this._var4.mtd32 > 0)
                {
                    return false;
                }
                return true;
            }
        }

        internal float mtd30
        {
            get
            {
                return this._var0;
            }
        }

        internal mtd702 mtd66
        {
            get
            {
                return this._var2;
            }
        }

        internal float mtd939
        {
            get
            {
                return this._var3.Font.mtd1038(this._var3.mtd997);
            }
        }

        internal float mtd940
        {
            get
            {
                return Math.Abs(this._var3.Font.mtd1039(this._var3.mtd997));
            }
        }

        internal float mtd941
        {
            get
            {
                return this._var3.Font.mtd1040(this._var3.mtd997);
            }
        }
    }
}

