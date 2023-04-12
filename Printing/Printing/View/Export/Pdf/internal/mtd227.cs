namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;

    internal class mtd227 : mtd942
    {
        internal static float mtd943 = 1f;

        internal static void mtd23(float var0, float var1, PropDoc var2, ref mtd944 var3, ref mtd742 var4)
        {
            float num = (var2.Left * 72f) + var0;
            float num2 = (var2.Top * 72f) + var1;
            float num3 = var2.Width * 72f;
            float num4 = var2.Height * 72f;
            RectangleF ef = var2.mtd134;
            float num5 = num + (var2.mtd133.X * 72f);
            float num6 = num2 + (var2.mtd133.Y * 72f);
            mtd942.mtd945(ref var3, ref var4);
            mtd942.mtd946(var4, num, var3.mtd947(num2), num3, num4);
            if (var2.BackColor != Color.Transparent)
            {
                mtd942.mtd948(num, num2, num3, num4, var2.BackColor, ref var3, ref var4);
            }
            mtd942.mtd949(var4, num5, var3.mtd947(num6), 9f, 9f);
            mtd942.mtd950(var4, mtd672.mtd674, false);
            if (var2.IsChecked)
            {
                var5(num5 + 1f, var3.mtd947(num6 + 7f), ref var3, ref var4);
            }
            var6(num + (ef.X * 72f), num2 + (ef.Y * 72f), ef.Width * 72f, ef.Height * 72f, var2, ref var3, ref var4);
            mtd942.mtd951(ref var3, ref var4);
            mtd952.mtd23(num, num2, num3, num4, var2.Border, ref var3, ref var4);
        }

        private static void var5(float var7, float var8, ref mtd944 var3, ref mtd742 var4)
        {
            mtd641 mtd = var3.mtd953(new mtd641(StandardFonts.mtd688, FontStyle.Regular));
            mtd942.mtd963(mtd, 8f, ref var3, ref var4);
            mtd942.mtd964(Color.Black, ref var3, ref var4);
            char ch = '4';
            mtd942.mtd965(var4, var7, var8, 0f, mtd.mtd937(ch.ToString()), false);
        }

        private static void var6(float var7, float var8, float var9, float var10, PropDoc var2, ref mtd944 var3, ref mtd742 var4)
        {
            Font font = var2.Font;
            mtd954 mtd2 = new mtd954(var3.mtd953(font), font.SizeInPoints, var2.ForeColor);
            mtd955 mtd3 = new mtd955();
            mtd3.mtd2(new mtd956(var2.Text, mtd2));
            mtd957 mtd4 = new mtd957(mtd3, 0f, 0f, 0f, 0f, mtd942.mtd958(var2.ContentAlignment), var2.RightToLeft);
            mtd4.mtd959(var9 - (2f * mtd943), mtd2, false);
            float num = mtd4.mtd960;
            float num2 = 0f;
            if ((num + (2f * mtd943)) < var10)
            {
                if (((var2.ContentAlignment == ContentAlignment.MiddleLeft) || (var2.ContentAlignment == ContentAlignment.MiddleCenter)) || (var2.ContentAlignment == ContentAlignment.MiddleRight))
                {
                    num2 = (var10 - (num + (2f * mtd943))) / 2f;
                }
                else if (((var2.ContentAlignment == ContentAlignment.BottomLeft) || (var2.ContentAlignment == ContentAlignment.BottomCenter)) || (var2.ContentAlignment == ContentAlignment.BottomRight))
                {
                    num2 = var10 - (num + (2f * mtd943));
                }
            }
            mtd961 mtd5 = mtd4.mtd962;
            mtd957.mtd23(ref var3, ref var4, mtd5, 0, mtd5.mtd32, var7, var8 + num2, mtd943, mtd943, var2.RightToLeft);
        }
    }
}

