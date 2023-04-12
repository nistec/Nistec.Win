namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;

    internal class mtd63 : mtd942
    {
        internal mtd63()
        {
        }

        internal static void mtd23(float var0, float var1, PropDoc var2, ref mtd944 var3, ref mtd742 var4)
        {
            float num = (var2.Left * 72f) + var0;
            float num2 = var3.mtd947((var2.Top * 72f) + var1);
            float num3 = var2.Width * 72f;
            float num4 = var2.Height * 72f;
            mtd672 mtd = mtd672.mtd674;
            mtd942.mtd975(var2.mtd43, var2.BorderColor, mtd942.mtd976(var2.LineStyle), ref var3, ref var4);
            if (var2.BackColor != Color.Transparent)
            {
                mtd |= mtd672.mtd675;
                mtd942.mtd964(var2.BackColor, ref var3, ref var4);
            }
            if (var2.ShapeStyle == ShapeStyle.Rectangle)
            {
                mtd942.mtd949(var4, num, num2, num3, num4);
            }
            else
            {
                var5(var4, num, num2, num3, num4);
            }
            mtd942.mtd950(var4, mtd, false);
        }

        private static void var5(mtd711 var6, float var7, float var8, float var9, float var10)
        {
            bool flag = true;
            float num = 3.141593f;
            float num2 = var9 / 2f;
            float num3 = var10 / 2f;
            float num4 = var7 + num2;
            float num5 = var8 - num3;
            float num6 = 0f;
            float num7 = 360f;
            flag = true;
            while (num6 < num7)
            {
                float num8;
                float num9;
                if ((num7 - num6) < 90f)
                {
                    num8 = num6;
                    num9 = num7;
                }
                else
                {
                    num8 = num6;
                    num9 = num8 + 90f;
                }
                num8 = (num8 * num) / 180f;
                num9 = (num9 * num) / 180f;
                float num10 = (float) Math.Sin((double) num8);
                float num11 = (float) Math.Cos((double) num8);
                float num12 = (float) Math.Sin((double) num9);
                float num13 = (float) Math.Cos((double) num9);
                float num14 = ((4f * ((float) (1.0 - Math.Cos((double) ((num9 - num8) / 2f))))) / ((float) Math.Sin((double) ((num9 - num8) / 2f)))) / 3f;
                float num15 = num2 * num11;
                float num16 = num3 * num10;
                float num17 = num2 * (num11 - (num14 * num10));
                float num18 = num3 * (num10 + (num14 * num11));
                float num19 = num2 * (num13 + (num14 * num12));
                float num20 = num3 * (num12 - (num14 * num13));
                float num21 = num2 * num13;
                float num22 = num3 * num12;
                float num23 = num15 + num4;
                float num24 = num16 + num5;
                float num25 = num17 + num4;
                float num26 = num18 + num5;
                float num27 = num19 + num4;
                float num28 = num20 + num5;
                float num29 = num21 + num4;
                float num30 = num22 + num5;
                num15 = num23;
                num16 = num24;
                num17 = num25;
                num18 = num26;
                num19 = num27;
                num20 = num28;
                num21 = num29;
                num22 = num30;
                if (flag)
                {
                    flag = false;
                    mtd942.mtd977(var6, num15, num16);
                }
                mtd942.mtd980(var6, num17, num18, num19, num20, num21, num22);
                num6 += 90f;
            }
        }
    }
}

