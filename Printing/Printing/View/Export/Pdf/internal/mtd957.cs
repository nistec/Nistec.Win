namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    internal class mtd957
    {
        private float _var0;
        private float _var1;
        private float _var10;
        private Nistec.Printing.View.Pdf.mtd230 _var11;
        private mtd961 _var12;
        private mtd1000 _var13;
        private float _var2;
        private float _var3;
        private TextAlignment _var4;
        private bool _var5;
        private mtd955 _var6;
        private bool _var7;
        private bool _var8;
        private float _var9;

        internal mtd957(mtd955 var6, float var0, float var1, float var2, float var3, TextAlignment var4, bool var5)
        {
            this._var0 = var0;
            this._var1 = var1;
            this._var2 = var2;
            this._var3 = var3;
            this._var4 = var4;
            this._var5 = var5;
            this._var6 = var6;
            this._var7 = false;
            this._var8 = true;
            this._var10 = 0f;
            this._var9 = 0f;
            this._var11 = new Nistec.Printing.View.Pdf.mtd230();
            this._var12 = new mtd961();
        }

        internal void mtd1050(Nistec.Printing.View.Pdf.mtd230 var11, bool var22)
        {
            if ((var11.mtd32 > 0) && !this._var8)
            {
                Nistec.Printing.View.Pdf.mtd230.mtd1009(var11, var22, this._var5);
                if (var11.mtd1026 != 0f)
                {
                    this._var12.mtd2(new mtd1043(var11));
                }
                else
                {
                    this._var12.mtd2(new mtd1016(var11));
                }
            }
            this._var10 += (var11.mtd1026 + var11.mtd31) + var11.mtd237;
            this.var15();
        }

        internal static void mtd1051(mtd1016 var38, out mtd1017 var39, out mtd1013 var40)
        {
            var39 = new mtd1017();
            var40 = new mtd1013();
            float num = 0f;
            float num2 = 0f;
            mtd1000 mtd = null;
            mtd1022 mtd2 = null;
            mtd1015 mtd3 = null;
            for (int i = 0; i < var38.mtd32; i++)
            {
                mtd2 = var38[i];
                mtd3 = mtd2.mtd1034;
                if (mtd != mtd3.mtd934)
                {
                    if (mtd == null)
                    {
                        mtd = mtd3.mtd934;
                        num2 += mtd2.mtd30;
                    }
                    else
                    {
                        if (mtd.mtd700 != Color.Transparent)
                        {
                            var40.mtd2(new mtd1014(num, num2, mtd.mtd700));
                        }
                        bool flag = false;
                        if ((mtd.Font.mtd934 & FontStyle.Bold) == FontStyle.Bold)
                        {
                            flag = true;
                        }
                        if (mtd.mtd701)
                        {
                            var39.mtd2(new mtd1018(num, num2, mtd.mtd1004, flag));
                        }
                        mtd = mtd3.mtd934;
                        num += num2;
                        num2 = mtd2.mtd30;
                    }
                }
                else
                {
                    num2 += mtd2.mtd30;
                }
            }
            if (mtd2 != null)
            {
                if (mtd.mtd700 != Color.Transparent)
                {
                    var40.mtd2(new mtd1014(num, num2, mtd.mtd700));
                }
                if (mtd.mtd701)
                {
                    bool flag2 = false;
                    if ((mtd.Font.mtd934 & FontStyle.Bold) == FontStyle.Bold)
                    {
                        flag2 = true;
                    }
                    var39.mtd2(new mtd1018(num, num2, mtd.mtd1004, flag2));
                }
            }
        }

        internal static void mtd1052(float var26, float var27, string var30, mtd1000 var31, ref mtd944 var14, ref mtd742 var23, bool var5)
        {
            mtd641 mtd = var31.Font;
            mtd.mtd936(var30);
            string str = var30;
            if (var5)
            {
                str = mtd942.mtd981(var30);
            }
            mtd942.mtd965(var23, var26, var14.mtd947(var27), 0f, mtd.mtd937(str), false);
        }

        internal static void mtd1053(float var32, float var33, float var34, float var35, Color var36, float var37, ref mtd944 var14, ref mtd742 var23)
        {
            var33 = var14.mtd947(var33);
            var35 = var14.mtd947(var35);
            mtd942.mtd975(var37, var36, LineStyle.Solid, ref var14, ref var23);
            mtd942.mtd977(var23, var32, var33);
            mtd942.mtd978(var23, var34, var35);
            mtd942.mtd950(var23, mtd672.mtd674, false);
        }

        internal static float mtd1054(mtd961 var12, int var24, int var25, float var28, float var41)
        {
            float num = var28 + var41;
            mtd1016 mtd = null;
            for (int i = var24; i < var25; i++)
            {
                mtd = var12[i];
                num += (mtd.mtd1026 + mtd.mtd31) + mtd.mtd237;
            }
            if (mtd != null)
            {
                num -= mtd.mtd237;
            }
            return num;
        }

        internal static void mtd23(ref mtd944 var14, ref mtd742 var23, mtd961 var12, int var24, int var25, float var26, float var27, float var28, float var29, bool var5)
        {
            mtd1017 mtd = null;
            mtd1013 mtd2 = null;
            var26 += var29;
            var27 += var28;
            for (int i = var24; i < var25; i++)
            {
                mtd1016 mtd3 = var12[i];
                if (mtd3.mtd1005)
                {
                    mtd1051(mtd3, out mtd, out mtd2);
                }
                else
                {
                    mtd = null;
                    mtd2 = null;
                }
                mtd1022 mtd4 = null;
                mtd1015 mtd5 = null;
                mtd1000 mtd6 = null;
                mtd641 mtd7 = null;
                float num2 = var26 + mtd3.mtd1023;
                float num3 = num2;
                var27 += mtd3.mtd1026;
                float num4 = var27 + mtd3.mtd1027;
                if (mtd2 != null)
                {
                    mtd1014 mtd8;
                    if (!var5)
                    {
                        for (int j = 0; j < mtd2.mtd32; j++)
                        {
                            mtd8 = mtd2[j];
                            mtd942.mtd948(num2 + mtd8.mtd128, var27, mtd8.mtd30, mtd3.mtd31, mtd8.mtd59, ref var14, ref var23);
                        }
                    }
                    else
                    {
                        for (int k = mtd2.mtd32 - 1; k > -1; k--)
                        {
                            mtd8 = mtd2[k];
                            mtd942.mtd948(var26 + (mtd3.mtd30 - ((mtd3.mtd1023 + mtd8.mtd128) + mtd8.mtd30)), var27, mtd8.mtd30, mtd3.mtd31, mtd8.mtd59, ref var14, ref var23);
                        }
                    }
                }
                if (!var5)
                {
                    for (int m = 0; m < mtd3.mtd32; m++)
                    {
                        mtd4 = mtd3[m];
                        mtd5 = mtd4.mtd1034;
                        if (mtd5.mtd934 != mtd6)
                        {
                            mtd6 = mtd5.mtd934;
                            mtd7 = mtd942.mtd963(mtd6.Font, mtd6.mtd997, ref var14, ref var23);
                            mtd6.mtd963(mtd7);
                            mtd942.mtd964(mtd6.mtd1004, ref var14, ref var23);
                        }
                        if (!mtd4.mtd1030)
                        {
                            mtd1052(num3, num4, mtd1033.mtd1044(mtd4), mtd6, ref var14, ref var23, false);
                        }
                        num3 += mtd4.mtd30;
                    }
                }
                else
                {
                    for (int n = mtd3.mtd32 - 1; n > -1; n--)
                    {
                        mtd4 = mtd3[n];
                        mtd5 = mtd4.mtd1034;
                        if (mtd5.mtd934 != mtd6)
                        {
                            mtd6 = mtd5.mtd934;
                            mtd7 = mtd942.mtd963(mtd6.Font, mtd6.mtd997, ref var14, ref var23);
                            mtd6.mtd963(mtd7);
                            mtd942.mtd964(mtd6.mtd1004, ref var14, ref var23);
                        }
                        if (!mtd4.mtd1030)
                        {
                            mtd1052(num3, num4, mtd1033.mtd1044(mtd4), mtd6, ref var14, ref var23, true);
                        }
                        num3 += mtd4.mtd30;
                    }
                }
                if (mtd != null)
                {
                    mtd1018 mtd9;
                    float num9;
                    float num10 = num4 + 2f;
                    float num11 = 1f;
                    if (!var5)
                    {
                        for (int num12 = 0; num12 < mtd.mtd32; num12++)
                        {
                            mtd9 = mtd[num12];
                            if (mtd9.mtd1037)
                            {
                                num11 = 1.25f;
                            }
                            else
                            {
                                num11 = 1f;
                            }
                            num9 = num2 + mtd9.mtd128;
                            mtd1053(num9, num10, num9 + mtd9.mtd30, num10, mtd9.mtd59, num11, ref var14, ref var23);
                        }
                    }
                    else
                    {
                        for (int num13 = mtd.mtd32 - 1; num13 > -1; num13--)
                        {
                            mtd9 = mtd[num13];
                            if (mtd9.mtd1037)
                            {
                                num11 = 1.5f;
                            }
                            else
                            {
                                num11 = 1f;
                            }
                            num9 = var26 + (mtd3.mtd30 - ((mtd3.mtd1023 + mtd9.mtd128) + mtd9.mtd30));
                            mtd1053(num9, num10, num9 + mtd9.mtd30, num10, mtd9.mtd59, num11, ref var14, ref var23);
                        }
                    }
                }
                var27 += mtd3.mtd31 + mtd3.mtd237;
            }
        }

        internal void mtd697(mtd1015 var14)
        {
            TextAlignment alignment = this._var4;
            float num = this._var2;
            this._var4 = var14.mtd1024;
            this._var2 = var14.mtd1046;
            this.mtd1050(this._var11, false);
            this._var4 = alignment;
            this._var2 = num;
        }

        internal void mtd698(mtd1015 var14)
        {
            this._var4 = var14.mtd1024;
            this._var3 = var14.mtd1046;
            this._var7 = true;
            this.mtd1050(this._var11, true);
        }

        internal void mtd959(float var9, mtd1000 var13, bool var16)
        {
            mtd1015 mtd = null;
            this._var9 = var9;
            this._var8 = var16;
            this._var10 = 0f;
            this._var7 = true;
            this._var12.mtd387();
            this._var13 = var13;
            for (int i = 0; i < this._var6.mtd32; i++)
            {
                mtd = this._var6[i];
                if (mtd.mtd66 == mtd696.mtd51)
                {
                    this.var17(mtd);
                }
                else if (mtd.mtd66 == mtd696.mtd698)
                {
                    this.mtd698(mtd);
                }
                else if (mtd.mtd66 == mtd696.mtd697)
                {
                    this.mtd697(mtd);
                }
            }
            this.mtd1050(this._var11, true);
        }

        private void var15()
        {
            float num;
            float num2 = 0f;
            float num3 = 0f;
            bool flag = false;
            if (this._var7)
            {
                num3 = this._var9 - this._var0;
                num = this._var0 + this._var1;
                this._var7 = false;
                flag = true;
                num2 = this._var3;
            }
            else
            {
                num3 = this._var9;
                num = this._var1;
            }
            this._var11.mtd172(num, num3, this._var4, this._var2, num2, flag);
        }

        private void var17(mtd1015 var18)
        {
            mtd1020 mtd = null;
            string str = var18.mtd51;
            mtd1000 mtd2 = var18.mtd934;
            if (str != null)
            {
                if (mtd2 == null)
                {
                    mtd2 = this._var13;
                }
                for (int i = 0; i < str.Length; i++)
                {
                    char ch = str[i];
                    switch (ch)
                    {
                        case ' ':
                            if (mtd == null)
                            {
                                mtd = new mtd1020(mtd702.mtd703, var18);
                                mtd.mtd1042(i, ch);
                            }
                            else if (mtd.mtd1030)
                            {
                                mtd.mtd1042(i, ch);
                            }
                            else
                            {
                                this.var19(mtd);
                                mtd = new mtd1020(mtd702.mtd703, var18);
                                mtd.mtd1042(i, ch);
                            }
                            break;

                        case '\t':
                            if (mtd == null)
                            {
                                mtd = new mtd1020(mtd702.mtd704, var18);
                                mtd.mtd1042(i, ch);
                            }
                            else if (mtd.mtd66 == mtd702.mtd704)
                            {
                                mtd.mtd1042(i, ch);
                            }
                            else
                            {
                                this.var19(mtd);
                                mtd = new mtd1020(mtd702.mtd704, var18);
                                mtd.mtd1042(i, ch);
                            }
                            break;

                        case '\r':
                        case '\n':
                            if (mtd != null)
                            {
                                this.var19(mtd);
                                mtd = null;
                            }
                            this.mtd1050(this._var11, false);
                            break;

                        default:
                            if (mtd == null)
                            {
                                mtd = new mtd1020(mtd702.mtd51, var18);
                                mtd.mtd1042(i, ch);
                            }
                            else if (mtd.mtd66 == mtd702.mtd51)
                            {
                                mtd.mtd1042(i, ch);
                            }
                            else
                            {
                                this.var19(mtd);
                                mtd = new mtd1020(mtd702.mtd51, var18);
                                mtd.mtd1042(i, ch);
                            }
                            break;
                    }
                }
                if (mtd != null)
                {
                    this.var19(mtd);
                }
            }
        }

        private void var19(mtd1020 var20)
        {
            float num = var20.mtd30 + this._var11.mtd1028;
            if (num < this._var11.mtd30)
            {
                this._var11.mtd1035(var20);
            }
            else if (num == this._var11.mtd30)
            {
                this._var11.mtd1035(var20);
                this.mtd1050(this._var11, false);
            }
            else if (num > this._var11.mtd30)
            {
                if (var20.mtd66 == mtd702.mtd51)
                {
                    if (var20.mtd30 > this._var11.mtd30)
                    {
                        this.var21(var20);
                    }
                    else
                    {
                        this.mtd1050(this._var11, false);
                        this._var11.mtd1035(var20);
                    }
                }
                else
                {
                    this.mtd1050(this._var11, false);
                }
            }
        }

        private void var21(mtd1020 var20)
        {
            float num = 0f;
            mtd1012[] mtdArray = null;
            mtd1012[] mtdArray2 = null;
            float num2 = 0f;
            float num3 = 0f;
            mtd1011 mtd2 = var20.mtd1032;
            float num4 = this._var11.mtd30 - this._var11.mtd1028;
            if (num4 > 0f)
            {
                for (int i = 0; i < mtd2.mtd32; i++)
                {
                    num += mtd2[i].mtd30;
                    if (num >= num4)
                    {
                        mtd1012 mtd;
                        if (num == num4)
                        {
                            if ((i + 1) < mtd2.mtd32)
                            {
                                mtdArray = new mtd1012[i + 1];
                                mtdArray2 = new mtd1012[(mtd2.mtd32 - 1) - i];
                                num2 = 0f;
                                num3 = 0f;
                                for (int j = 0; j < (i + 1); j++)
                                {
                                    mtd = mtd2[j];
                                    mtdArray[j] = mtd;
                                    num2 += mtd.mtd30;
                                }
                                int index = 0;
                                for (int k = i + 1; k < mtd2.mtd32; k++)
                                {
                                    mtd = mtd2[k];
                                    mtdArray2[index] = mtd;
                                    num3 += mtd.mtd30;
                                    index++;
                                }
                                this._var11.mtd1035(new mtd1020(var20.mtd66, var20.mtd1015, mtdArray, num2));
                                this.mtd1050(this._var11, false);
                                this.var19(new mtd1020(var20.mtd66, var20.mtd1015, mtdArray2, num3));
                                var20 = null;
                                return;
                            }
                            this._var11.mtd1035(var20);
                            this.mtd1050(this._var11, false);
                            return;
                        }
                        if (num > num4)
                        {
                            if ((i - 1) > -1)
                            {
                                mtdArray = new mtd1012[i];
                                mtdArray2 = new mtd1012[mtd2.mtd32 - i];
                                num2 = 0f;
                                num3 = 0f;
                                for (int m = 0; m < i; m++)
                                {
                                    mtd = mtd2[m];
                                    mtdArray[m] = mtd;
                                    num2 += mtd.mtd30;
                                }
                                int num10 = 0;
                                for (int n = i; n < mtd2.mtd32; n++)
                                {
                                    mtd = mtd2[n];
                                    mtdArray2[num10] = mtd;
                                    num3 += mtd.mtd30;
                                    num10++;
                                }
                                this._var11.mtd1035(new mtd1020(var20.mtd66, var20.mtd1015, mtdArray, num2));
                                this.mtd1050(this._var11, false);
                                this.var19(new mtd1020(var20.mtd66, var20.mtd1015, mtdArray2, num3));
                                var20 = null;
                                return;
                            }
                            if (this._var11.mtd32 == 0)
                            {
                                this._var11.mtd1035(var20);
                                this.mtd1050(this._var11, false);
                                return;
                            }
                            this.mtd1050(this._var11, false);
                            this.var19(var20);
                            return;
                        }
                    }
                }
            }
            else
            {
                this.mtd1050(this._var11, false);
                this.var19(var20);
            }
        }

        internal float mtd960
        {
            get
            {
                return this._var10;
            }
        }

        internal mtd961 mtd962
        {
            get
            {
                return this._var12;
            }
        }
    }
}

