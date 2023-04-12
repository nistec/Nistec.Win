namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.IO;

    internal class mtd633
    {
        internal mtd633()
        {
        }

        internal static void mtd22(int var0, PropDoc var1, ref StreamWriter var2)
        {
            if (var1.ShapeStyle == ShapeStyle.Rectangle)
            {
                string str = mtd620.mtd621(var1.mtd43);
                string str2 = mtd620.mtd622(var1.LineStyle);
                string str3 = ColorTranslator.ToHtml(var1.BorderColor);
                var2.Write("<span style=" + '"');
                mtd627.mtd22(var0, var1, ref var2);
                if (var1.BackColor != Color.Transparent)
                {
                    mtd624.mtd22(var1.BackColor, ref var2);
                }
                var2.Write(string.Format("border-top-style: {0}; ", str2));
                var2.Write(string.Format("border-top-width: {0}pt; ", str));
                var2.Write(string.Format("border-top-color: {0}; ", str3));
                var2.Write(string.Format("border-left-style: {0}; ", str2));
                var2.Write(string.Format("border-left-width: {0}pt; ", str));
                var2.Write(string.Format("border-left-color: {0}; ", str3));
                var2.Write(string.Format("border-bottom-style: {0}; ", str2));
                var2.Write(string.Format("border-bottom-width: {0}pt; ", str));
                var2.Write(string.Format("border-bottom-color: {0}; ", str3));
                var2.Write(string.Format("border-right-style: {0}; ", str2));
                var2.Write(string.Format("border-right-width: {0}pt; ", str));
                var2.Write(string.Format("border-right-color: {0}; ", str3));
                var2.WriteLine('"' + "></span>");
            }
        }
    }
}

