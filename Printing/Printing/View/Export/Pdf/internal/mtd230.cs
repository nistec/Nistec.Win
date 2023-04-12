namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd230
    {
        private float _var0;
        private TextAlignment _var1;
        private float _var10;
        private int _var11;
        private bool _var12;
        private bool _var13;
        private float _var2;
        private float _var3;
        private float _var4;
        private float _var5;
        private float _var6;
        private Nistec.Printing.View.Pdf.mtd1021 _var7 = new Nistec.Printing.View.Pdf.mtd1021();
        private mtd1019 _var8 = new mtd1019();
        private float _var9;

        internal mtd230()
        {
        }

        internal static void mtd1009(mtd230 var14, bool var15, bool var16)
        {
            mtd1020 mtd;
            mtd1019 mtd2 = var14._var8;
            if (var14.mtd1024 == TextAlignment.mtd28)
            {
                if (!var16)
                {
                    if (!var14.mtd1025)
                    {
                        var17(var14);
                    }
                }
                else
                {
                    var18(var14);
                }
            }
            else if (var14.mtd1024 == TextAlignment.mtd683)
            {
                if (!var15)
                {
                    if (!var14.mtd1025)
                    {
                        var17(var14);
                    }
                    var18(var14);
                    float num = (var14.mtd30 - var14.mtd1028) / var14.mtd1029;
                    for (int j = 0; j < mtd2.mtd32; j++)
                    {
                        mtd = mtd2[j];
                        if (mtd.mtd1030)
                        {
                            mtd.mtd1031 = num;
                            var14._var10 += num;
                            var14._var7.mtd2(new mtd703(mtd.mtd1015, mtd.mtd30));
                        }
                        else
                        {
                            mtd1011 mtd3 = mtd.mtd1032;
                            mtd1033 mtd4 = new mtd1033(mtd3[0].mtd504, mtd.mtd1015);
                            mtd4.mtd30 = mtd.mtd30;
                            mtd4.mtd166 = (mtd3[mtd3.mtd32 - 1].mtd504 - mtd3[0].mtd504) + 1;
                            var14._var7.mtd2(mtd4);
                        }
                    }
                    return;
                }
                if (!var16)
                {
                    if (!var14.mtd1025)
                    {
                        var17(var14);
                    }
                }
                else
                {
                    var18(var14);
                    var14.mtd1023 += var14.mtd30 - var14.mtd1028;
                }
            }
            else if (var14.mtd1024 == TextAlignment.mtd408)
            {
                if (!var16)
                {
                    var18(var14);
                }
                else if (!var14.mtd1025)
                {
                    var17(var14);
                }
                var14._var0 += var14.mtd30 - var14.mtd1028;
            }
            else if (var14.mtd1024 == TextAlignment.mtd407)
            {
                if (!var14.mtd1025)
                {
                    var17(var14);
                }
                var18(var14);
                var14._var0 += (var14.mtd30 - var14.mtd1028) / 2f;
            }
            Nistec.Printing.View.Pdf.mtd1021 mtd5 = var14._var7;
            for (int i = 0; i < mtd2.mtd32; i++)
            {
                mtd = mtd2[i];
                if (mtd5.mtd32 > 0)
                {
                    mtd1022 mtd6 = mtd5[mtd5.mtd32 - 1];
                    if (mtd6.mtd1034 == mtd.mtd1015)
                    {
                        mtd1011 mtd7 = mtd.mtd1032;
                        mtd6.mtd30 += mtd.mtd30;
                        mtd6.mtd166 += (mtd7[mtd7.mtd32 - 1].mtd504 - mtd7[0].mtd504) + 1;
                    }
                    else
                    {
                        mtd1011 mtd8 = mtd.mtd1032;
                        mtd1033 mtd9 = new mtd1033(mtd8[0].mtd504, mtd.mtd1015);
                        mtd9.mtd30 = mtd.mtd30;
                        mtd9.mtd166 = (mtd8[mtd8.mtd32 - 1].mtd504 - mtd9.mtd504) + 1;
                        var14._var7.mtd2(mtd9);
                    }
                }
                else
                {
                    mtd1011 mtd10 = mtd.mtd1032;
                    mtd1033 mtd11 = new mtd1033(mtd10[0].mtd504, mtd.mtd1015);
                    mtd11.mtd30 = mtd.mtd30;
                    mtd11.mtd166 = (mtd10[mtd10.mtd32 - 1].mtd504 - mtd11.mtd504) + 1;
                    var14._var7.mtd2(mtd11);
                }
            }
        }

        internal void mtd1035(mtd1020 var19)
        {
            if ((var19 != null) && !var19.mtd1036)
            {
                if (var19.mtd1030)
                {
                    this._var11++;
                }
                this._var10 += var19.mtd30;
                if (this._var4 < var19.mtd939)
                {
                    this._var4 = var19.mtd939;
                }
                if (this._var5 < var19.mtd940)
                {
                    this._var5 = var19.mtd940;
                }
                if (this._var6 < var19.mtd941)
                {
                    this._var6 = var19.mtd941;
                }
                if (!this._var12 && var19.mtd1005)
                {
                    this._var12 = true;
                }
                this._var8.mtd2(var19);
            }
        }

        internal void mtd172(float var0, float var9, TextAlignment var1, float var2, float var3, bool var13)
        {
            this._var1 = var1;
            this._var4 = 0f;
            this._var2 = var2;
            this._var5 = 0f;
            this._var0 = var0;
            this._var6 = 0f;
            this._var9 = var9;
            this._var11 = 0;
            this._var3 = var3;
            this._var13 = var13;
            this._var8.mtd387();
            this._var7.mtd387();
            this._var10 = 0f;
            this._var12 = false;
        }

        private static void var17(mtd230 var14)
        {
            if ((var14._var8.mtd32 > 0) && var14._var8[0].mtd1030)
            {
                var14._var11--;
                var14._var10 -= var14._var8[0].mtd30;
                var14._var8.mtd394(0);
            }
        }

        private static void var18(mtd230 var14)
        {
            int num = var14._var8.mtd32;
            if ((num > 0) && var14._var8[num - 1].mtd1030)
            {
                int num2 = num - 1;
                var14._var11--;
                var14._var10 -= var14._var8[num2].mtd30;
                var14._var8.mtd394(num2);
            }
        }

        internal bool mtd1005
        {
            get
            {
                return this._var12;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd1021 mtd1021
        {
            get
            {
                return this._var7;
            }
        }

        internal float mtd1023
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }

        internal TextAlignment mtd1024
        {
            get
            {
                return this._var1;
            }
        }

        internal bool mtd1025
        {
            get
            {
                return this._var13;
            }
        }

        internal float mtd1026
        {
            get
            {
                return this._var3;
            }
        }

        internal float mtd1027
        {
            get
            {
                return this._var4;
            }
        }

        internal float mtd1028
        {
            get
            {
                return this._var10;
            }
        }

        internal float mtd1029
        {
            get
            {
                return (float) this._var11;
            }
        }

        internal float mtd237
        {
            get
            {
                return this._var2;
            }
        }

        internal float mtd30
        {
            get
            {
                return this._var9;
            }
        }

        internal float mtd31
        {
            get
            {
                return ((this._var4 + this._var5) + this._var6);
            }
        }

        internal int mtd32
        {
            get
            {
                return this._var8.mtd32;
            }
        }

        internal float mtd939
        {
            get
            {
                return this._var4;
            }
        }

        internal float mtd940
        {
            get
            {
                return this._var5;
            }
        }

        internal mtd1022 this[int index]
        {
            get
            {
                return this._var7[index];
            }
        }
    }
}

