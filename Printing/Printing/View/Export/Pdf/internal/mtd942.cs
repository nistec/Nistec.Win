namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;

    internal abstract class mtd942
    {
        //protected string _mtd982;

        internal mtd942()
        {
        }

        internal static void mtd945(ref mtd944 var17, ref mtd742 var18)
        {
            var18.mtd968("q ");
            var17.mtd945();
        }

        internal static void mtd946(mtd711 var0, float var1, float var2, float var3, float var4)
        {
            var0.mtd968(mtd620.mtd648(var1, var2, var3, var4) + " re W n");
        }

        internal static void mtd948(float var1, float var2, float var3, float var4, Color var19, ref mtd944 var17, ref mtd742 var18)
        {
            if (var19 != Color.Transparent)
            {
                mtd964(var19, ref var17, ref var18);
                mtd949(var18, var1, var17.mtd947(var2), var3, var4);
                mtd950(var18, mtd672.mtd675, false);
            }
        }

        internal static void mtd949(mtd711 var0, RectangleF var11)
        {
            var0.mtd968(mtd620.mtd648(var11) + " re");
        }

        internal static void mtd949(mtd711 var0, float var1, float var2, float var3, float var4)
        {
            var0.mtd968(mtd620.mtd648(var1, var2, var3, var4) + " re");
        }

        internal static void mtd950(mtd711 var0, mtd672 var27, bool var28)
        {
            bool flag = false;
            bool flag2 = false;
            if ((var27 & mtd672.mtd674) == mtd672.mtd674)
            {
                flag2 = true;
            }
            if ((var27 & mtd672.mtd675) == mtd672.mtd675)
            {
                flag = true;
            }
            if ((var27 & mtd672.mtd676) == mtd672.mtd676)
            {
                if ((var27 & mtd672.mtd673) == mtd672.mtd673)
                {
                    var0.mtd968("W*");
                }
                if (flag && flag2)
                {
                    if (!var28)
                    {
                        var0.mtd968("B*");
                    }
                    else
                    {
                        var0.mtd968("b*");
                    }
                }
                else if (!flag && flag2)
                {
                    if (!var28)
                    {
                        var0.mtd968("S");
                    }
                    else
                    {
                        var0.mtd968("s");
                    }
                }
                else if (flag && !flag2)
                {
                    var0.mtd968("f*");
                }
                else if (!flag && !flag2)
                {
                    var0.mtd968("n*");
                }
            }
            else
            {
                if ((var27 & mtd672.mtd673) == mtd672.mtd673)
                {
                    var0.mtd968("W");
                }
                if (flag && flag2)
                {
                    if (!var28)
                    {
                        var0.mtd968("B");
                    }
                    else
                    {
                        var0.mtd968("b");
                    }
                }
                else if (!flag && flag2)
                {
                    if (!var28)
                    {
                        var0.mtd968("S");
                    }
                    else
                    {
                        var0.mtd968("s");
                    }
                }
                else if (flag && !flag2)
                {
                    var0.mtd968("f");
                }
                else if (!flag && !flag2)
                {
                    var0.mtd968("n");
                }
            }
        }

        internal static void mtd951(ref mtd944 var17, ref mtd742 var18)
        {
            if (var17.mtd993.Count > 0)
            {
                var18.mtd968("Q ");
                var17.mtd951();
            }
        }

        internal static TextAlignment mtd958(ContentAlignment var30)
        {
            if (((var30 == ContentAlignment.TopLeft) || (var30 == ContentAlignment.MiddleLeft)) || (var30 == ContentAlignment.BottomLeft))
            {
                return TextAlignment.mtd28;
            }
            if (((var30 != ContentAlignment.TopCenter) && (var30 != ContentAlignment.MiddleCenter)) && (var30 != ContentAlignment.BottomCenter))
            {
                return TextAlignment.mtd408;
            }
            return TextAlignment.mtd407;
        }

        internal static mtd641 mtd963(mtd641 var15, float var16, ref mtd944 var17, ref mtd742 var18)
        {
            bool flag = false;
            mtd984 mtd = var17.mtd985;
            mtd641 mtd2 = var17.mtd986(var15);
            if (!mtd641.mtd766(mtd.mtd987, mtd2))
            {
                mtd.mtd987 = mtd2;
                flag = true;
            }
            if (mtd.mtd988 != var16)
            {
                mtd.mtd988 = var16;
                flag = true;
            }
            if (flag)
            {
                var18.mtd968(string.Format("/{0} {1} Tf ", mtd2.mtd763, mtd620.mtd621(var16)));
            }
            return mtd2;
        }

        internal static void mtd964(Color var19, ref mtd944 var17, ref mtd742 var18)
        {
            mtd984 mtd = var17.mtd985;
            if (mtd.mtd989 != var19)
            {
                mtd.mtd989 = var19;
                var20(var19, ref var18, false);
            }
        }

        internal static void mtd965(mtd711 var0, float var1, float var2, float var12, string var13, bool var14)
        {
            var0.mtd968("BT");
            var0.mtd968(string.Format("{0} {1} Td", mtd620.mtd621(var1), mtd620.mtd621(var2)));
            var0.mtd968(string.Format("{0} Tj", var13));
            var0.mtd968("ET ");
            if (var14)
            {
                mtd983(var0, var1, var2 - 1.4f, var1 + var12, var2 - 1.4f);
            }
        }

        internal static void mtd975(float var21, Color var22, LineStyle var23, ref mtd944 var17, ref mtd742 var18)
        {
            mtd984 mtd = var17.mtd985;
            if (mtd.mtd990 != var23)
            {
                mtd.mtd990 = var23;
                if (var23 == LineStyle.Solid)
                {
                    var18.mtd968("[] 0 d");
                }
                else if (var23 == LineStyle.Dash)
                {
                    var18.mtd968("[6 2] 0 d");
                }
                else if (var23 == LineStyle.Dot)
                {
                    var18.mtd968("[2 2] 0 d");
                }
            }
            if (mtd.mtd991 != var21)
            {
                if (var21 <= 0f)
                {
                    var21 = 1f;
                }
                mtd.mtd991 = var21;
                var18.mtd968(string.Format("{0} w", mtd620.mtd621(var21)));
            }
            if (mtd.mtd992 != var22)
            {
                mtd.mtd992 = var22;
                var20(var22, ref var18, true);
            }
        }

        internal static LineStyle mtd976(LineStyle var23)
        {
            if (var23 == LineStyle.Solid)
            {
                return LineStyle.Solid;
            }
            if (var23 == LineStyle.Dot)
            {
                return LineStyle.Dot;
            }
            return LineStyle.Dash;
        }

        internal static void mtd977(mtd711 var0, float var1, float var2)
        {
            var0.mtd968(string.Format("{0} {1} m", mtd620.mtd621(var1), mtd620.mtd621(var2)));
        }

        internal static void mtd978(mtd711 var0, float var1, float var2)
        {
            var0.mtd968(mtd620.mtd649(var1, var2) + " l");
        }

        internal static void mtd980(mtd711 var0, float var5, float var6, float var7, float var8, float var9, float var10)
        {
            var0.mtd968(string.Format("{0} {1} {2} {3} {4} {5} c", new object[] { mtd620.mtd621(var5), mtd620.mtd621(var6), mtd620.mtd621(var7), mtd620.mtd621(var8), mtd620.mtd621(var9), mtd620.mtd621(var10) }));
        }

        internal static string mtd981(string var29)
        {
            char[] chArray = new char[var29.Length];
            int index = 0;
            for (int i = var29.Length - 1; i > -1; i--)
            {
                chArray[index] = var29[i];
                index++;
            }
            return new string(chArray);
        }

        internal static void mtd983(mtd711 var0, float var5, float var6, float var7, float var8)
        {
            var0.mtd968(mtd620.mtd649(var5, var6) + " m");
            var0.mtd968(mtd620.mtd649(var7, var8) + " l");
            var0.mtd968("S");
        }

        private static void var20(Color var24, ref mtd742 var18, bool var25)
        {
            if (var25)
            {
                var18.mtd968(string.Format("{0} RG", var26(var24)));
            }
            else
            {
                var18.mtd968(string.Format("{0} rg", var26(var24)));
            }
        }

        private static string var26(Color var24)
        {
            float num = (float) Math.Round((double) (((float) var24.R) / 255f), 3);
            float num2 = (float) Math.Round((double) (((float) var24.G) / 255f), 3);
            float num3 = (float) Math.Round((double) (((float) var24.B) / 255f), 3);
            return string.Format("{0} {1} {2}", mtd620.mtd621(num), mtd620.mtd621(num2), mtd620.mtd621(num3));
        }
    }
}

