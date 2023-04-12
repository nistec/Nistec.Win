namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.IO;

    internal class mtd631
    {
        internal mtd631()
        {
        }

        internal static void mtd22(int var0, PropDoc var1, ref StreamWriter var2)
        {
            float num = 0f;
            float num2 = 0f;
            float num3 = 0f;
            float num4 = 0f;
            bool flag = false;
            if (var1.LinePositionX1 == var1.LinePositionX2)
            {
                flag = true;
                num3 = var1.mtd43 / 72f;
                if (var1.LinePositionY2 > var1.LinePositionY1)
                {
                    num = var1.LinePositionX1;
                    num2 = var1.LinePositionY1;
                    num4 = var1.LinePositionY2 - var1.LinePositionY1;
                }
                else
                {
                    num = var1.LinePositionX2;
                    num2 = var1.LinePositionY2;
                    num4 = var1.LinePositionY1 - var1.LinePositionY2;
                }
            }
            else
            {
                if (var1.LinePositionY1 != var1.LinePositionY2)
                {
                    return;
                }
                if (var1.LinePositionX2 > var1.LinePositionX1)
                {
                    num = var1.LinePositionX1;
                    num2 = var1.LinePositionY1;
                    num3 = var1.LinePositionX2 - var1.LinePositionX1;
                }
                else
                {
                    num = var1.LinePositionX2;
                    num2 = var1.LinePositionY2;
                    num3 = var1.LinePositionX1 - var1.LinePositionX2;
                }
            }
            var2.Write("<span style=" + '"');
            mtd627.mtd22(var0, num, num2, num3, num4, var1.Visible, ref var2);
            if (flag)
            {
                var2.Write(string.Format("border-left-style: {0}; ", mtd620.mtd622(var1.LineStyle)));
                var2.Write(string.Format("border-left-width: {0}pt; ", mtd620.mtd621(var1.mtd43)));
                var2.Write(string.Format("border-left-color: {0}; ", ColorTranslator.ToHtml(var1.BorderColor)));
            }
            else
            {
                var2.Write(string.Format("border-top-style: {0}; ", mtd620.mtd622(var1.LineStyle)));
                var2.Write(string.Format("border-top-width: {0}pt; ", mtd620.mtd621(var1.mtd43)));
                var2.Write(string.Format("border-top-color: {0}; ", ColorTranslator.ToHtml(var1.BorderColor)));
            }
            var2.WriteLine('"' + "> </span>");
        }
    }
}

