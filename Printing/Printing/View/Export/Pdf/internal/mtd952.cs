namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;

    internal class mtd952 : mtd942
    {
        internal mtd952()
        {
        }

        internal static void mtd23(float var0, float var1, float var2, float var3, Border var4, ref mtd944 var5, ref mtd742 var6)
        {
            if (var4 != null)
            {
                if (var7(var4))
                {
                    var8(var0, var1, var2, var3, var4, ref var5, ref var6);
                }
                else
                {
                    var9(var0, var1, var2, var3, var4, ref var5, ref var6);
                }
            }
        }

        private static void var10(float var11, float var0, float var1, float var2, float var3, BorderLineStyle var12, Color var13, ref mtd944 var5, ref mtd742 var6)
        {
            if ((var12 != BorderLineStyle.None) && (var13 != Color.Transparent))
            {
                float num;
                float num2;
                float num3;
                float num4;
                if (var11 == 1f)
                {
                    num = var0;
                    num2 = var5.mtd947(var1);
                    num3 = var0 + var2;
                    num4 = num2;
                }
                else if (var11 == 2f)
                {
                    num = var0;
                    num2 = var5.mtd947(var1);
                    num3 = var0;
                    num4 = var5.mtd947(var1 + var3);
                }
                else if (var11 == 3f)
                {
                    num = var0;
                    num2 = var5.mtd947(var1 + var3);
                    num3 = var0 + var2;
                    num4 = var5.mtd947(var1 + var3);
                }
                else
                {
                    num = var0 + var2;
                    num2 = var5.mtd947(var1 + var3);
                    num3 = var0 + var2;
                    num4 = var5.mtd947(var1);
                }
                if (var12 == BorderLineStyle.Solid)
                {
                    mtd942.mtd975(1f, var13, LineStyle.Solid, ref var5, ref var6);
                    mtd942.mtd977(var6, num, num2);
                    mtd942.mtd978(var6, num3, num4);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var12 == BorderLineStyle.Double)
                {
                    mtd942.mtd975(1f, var13, LineStyle.Solid, ref var5, ref var6);
                    if (var11 == 1f)
                    {
                        mtd942.mtd977(var6, num, num2);
                        mtd942.mtd978(var6, num3, num4);
                        mtd942.mtd977(var6, num - 2f, num2 + 2f);
                        mtd942.mtd978(var6, num3 + 2f, num4 + 2f);
                    }
                    else if (var11 == 2f)
                    {
                        mtd942.mtd977(var6, num, num2);
                        mtd942.mtd978(var6, num3, num4);
                        mtd942.mtd977(var6, num - 2f, num2 + 2f);
                        mtd942.mtd978(var6, num3 - 2f, num4 - 2f);
                    }
                    else if (var11 == 3f)
                    {
                        mtd942.mtd977(var6, num, num2);
                        mtd942.mtd978(var6, num3, num4);
                        mtd942.mtd977(var6, num - 2f, num2 - 2f);
                        mtd942.mtd978(var6, num3 + 2f, num4 - 2f);
                    }
                    else
                    {
                        mtd942.mtd977(var6, num, num2);
                        mtd942.mtd978(var6, num3, num4);
                        mtd942.mtd977(var6, num + 2f, num2 - 2f);
                        mtd942.mtd978(var6, num3 + 2f, num4 + 2f);
                    }
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var12 == BorderLineStyle.Dot)
                {
                    mtd942.mtd975(1f, var13, LineStyle.Dot, ref var5, ref var6);
                    mtd942.mtd977(var6, num, num2);
                    mtd942.mtd978(var6, num3, num4);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (((var12 == BorderLineStyle.Dash) || (var12 == BorderLineStyle.DashDot)) || (var12 == BorderLineStyle.DashDotDot))
                {
                    mtd942.mtd975(1f, var13, LineStyle.Dash, ref var5, ref var6);
                    mtd942.mtd977(var6, num, num2);
                    mtd942.mtd978(var6, num3, num4);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var12 == BorderLineStyle.ThickSolid)
                {
                    mtd942.mtd975(1.2f, var13, LineStyle.Solid, ref var5, ref var6);
                    mtd942.mtd977(var6, num, num2);
                    mtd942.mtd978(var6, num3, num4);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var12 == BorderLineStyle.ThickDouble)
                {
                    mtd942.mtd975(1.2f, var13, LineStyle.Solid, ref var5, ref var6);
                    if (var11 == 1f)
                    {
                        mtd942.mtd977(var6, num, num2);
                        mtd942.mtd978(var6, num3, num4);
                        mtd942.mtd977(var6, num - 2.4f, num2 + 2.4f);
                        mtd942.mtd978(var6, num3 + 2.4f, num4 + 2.4f);
                    }
                    else if (var11 == 2f)
                    {
                        mtd942.mtd977(var6, num, num2);
                        mtd942.mtd978(var6, num3, num4);
                        mtd942.mtd977(var6, num - 2.4f, num2 + 2.4f);
                        mtd942.mtd978(var6, num3 - 2.4f, num4 - 2.4f);
                    }
                    else if (var11 == 3f)
                    {
                        mtd942.mtd977(var6, num, num2);
                        mtd942.mtd978(var6, num3, num4);
                        mtd942.mtd977(var6, num - 2.4f, num2 - 2.4f);
                        mtd942.mtd978(var6, num3 + 2.4f, num4 - 2.4f);
                    }
                    else
                    {
                        mtd942.mtd977(var6, num, num2);
                        mtd942.mtd978(var6, num3, num4);
                        mtd942.mtd977(var6, num + 2.4f, num2 - 2.4f);
                        mtd942.mtd978(var6, num3 + 2.4f, num4 + 2.4f);
                    }
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var12 == BorderLineStyle.ThickDot)
                {
                    mtd942.mtd975(1.2f, var13, LineStyle.Dot, ref var5, ref var6);
                    mtd942.mtd977(var6, num, num2);
                    mtd942.mtd978(var6, num3, num4);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (((var12 == BorderLineStyle.ThickDash) || (var12 == BorderLineStyle.ThickDashDot)) || (var12 == BorderLineStyle.ThickDashDotDot))
                {
                    mtd942.mtd975(1.2f, var13, LineStyle.Dash, ref var5, ref var6);
                    mtd942.mtd977(var6, num, num2);
                    mtd942.mtd978(var6, num3, num4);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var12 == BorderLineStyle.ExtraThickSolid)
                {
                    mtd942.mtd975(1.4f, var13, LineStyle.Solid, ref var5, ref var6);
                    mtd942.mtd977(var6, num, num2);
                    mtd942.mtd978(var6, num3, num4);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
            }
        }

        private static bool var7(Border var4)
        {
            if (((var4.BorderTopStyle != var4.BorderLeftStyle) || (var4.BorderTopStyle != var4.BorderBottomStyle)) || (var4.BorderTopStyle != var4.BorderRightStyle))
            {
                return false;
            }
            return (((var4.BorderTopColor == var4.BorderLeftColor) && (var4.BorderTopColor == var4.BorderBottomColor)) && (var4.BorderTopColor == var4.BorderRightColor));
        }

        private static void var8(float var0, float var1, float var2, float var3, Border var4, ref mtd944 var5, ref mtd742 var6)
        {
            if (var4.BorderTopColor != Color.Transparent)
            {
                var6.mtd968(string.Format("{0} J", "0"));
                var6.mtd968(string.Format("{0} j", "0"));
                var6.mtd968(string.Format("{0} M", "10"));
                if (var4.BorderTopStyle == BorderLineStyle.Solid)
                {
                    mtd942.mtd975(1f, var4.BorderTopColor, LineStyle.Solid, ref var5, ref var6);
                    mtd942.mtd949(var6, var0, var5.mtd947(var1), var2, var3);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var4.BorderTopStyle == BorderLineStyle.Double)
                {
                    mtd942.mtd975(1f, var4.BorderTopColor, LineStyle.Solid, ref var5, ref var6);
                    mtd942.mtd949(var6, var0, var5.mtd947(var1), var2, var3);
                    mtd942.mtd949(var6, var0 - 2f, var5.mtd947(var1 - 2f), var2 + 4f, var3 + 4f);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var4.BorderTopStyle == BorderLineStyle.Dot)
                {
                    mtd942.mtd975(1f, var4.BorderTopColor, LineStyle.Dot, ref var5, ref var6);
                    mtd942.mtd949(var6, var0, var5.mtd947(var1), var2, var3);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (((var4.BorderTopStyle == BorderLineStyle.Dash) || (var4.BorderTopStyle == BorderLineStyle.DashDot)) || (var4.BorderTopStyle == BorderLineStyle.DashDotDot))
                {
                    mtd942.mtd975(1f, var4.BorderTopColor, LineStyle.Dash, ref var5, ref var6);
                    mtd942.mtd949(var6, var0, var5.mtd947(var1), var2, var3);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var4.BorderTopStyle == BorderLineStyle.ThickSolid)
                {
                    mtd942.mtd975(1.2f, var4.BorderTopColor, LineStyle.Solid, ref var5, ref var6);
                    mtd942.mtd949(var6, var0, var5.mtd947(var1), var2, var3);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var4.BorderTopStyle == BorderLineStyle.ThickDouble)
                {
                    mtd942.mtd975(1.2f, var4.BorderTopColor, LineStyle.Solid, ref var5, ref var6);
                    mtd942.mtd949(var6, var0, var5.mtd947(var1), var2, var3);
                    mtd942.mtd949(var6, var0 - 2f, var5.mtd947(var1 - 2f), var2 + 4f, var3 + 4f);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var4.BorderTopStyle == BorderLineStyle.ThickDot)
                {
                    mtd942.mtd975(1.2f, var4.BorderTopColor, LineStyle.Dot, ref var5, ref var6);
                    mtd942.mtd949(var6, var0, var5.mtd947(var1), var2, var3);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (((var4.BorderTopStyle == BorderLineStyle.ThickDash) || (var4.BorderTopStyle == BorderLineStyle.ThickDashDot)) || (var4.BorderTopStyle == BorderLineStyle.ThickDashDotDot))
                {
                    mtd942.mtd975(1.2f, var4.BorderTopColor, LineStyle.Dash, ref var5, ref var6);
                    mtd942.mtd949(var6, var0, var5.mtd947(var1), var2, var3);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
                else if (var4.BorderTopStyle == BorderLineStyle.ExtraThickSolid)
                {
                    mtd942.mtd975(1.4f, var4.BorderTopColor, LineStyle.Solid, ref var5, ref var6);
                    mtd942.mtd949(var6, var0, var5.mtd947(var1), var2, var3);
                    mtd942.mtd950(var6, mtd672.mtd674, false);
                }
            }
        }

        private static void var9(float var0, float var1, float var2, float var3, Border var4, ref mtd944 var5, ref mtd742 var6)
        {
            var10(1f, var0, var1, var2, var3, var4.BorderTopStyle, var4.BorderTopColor, ref var5, ref var6);
            var10(2f, var0, var1, var2, var3, var4.BorderLeftStyle, var4.BorderLeftColor, ref var5, ref var6);
            var10(3f, var0, var1, var2, var3, var4.BorderBottomStyle, var4.BorderBottomColor, ref var5, ref var6);
            var10(4f, var0, var1, var2, var3, var4.BorderRightStyle, var4.BorderRightColor, ref var5, ref var6);
        }
    }
}

