namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.Globalization;

    internal class mtd620
    {
        internal mtd620()
        {
        }

        internal static string mtd621(double var0)
        {
            if (!var1(var0))
            {
                return var0.ToString("0.000", CultureInfo.InvariantCulture);
            }
            return var0.ToString("0", CultureInfo.InvariantCulture);
        }

        internal static string mtd621(int var0)
        {
            return var0.ToString(NumberFormatInfo.InvariantInfo);
        }

        internal static string mtd621(float var0)
        {
            if (!var1((double) var0))
            {
                return var0.ToString("0.000", CultureInfo.InvariantCulture);
            }
            return var0.ToString("0", CultureInfo.InvariantCulture);
        }

        internal static string mtd622(LineStyle var2)
        {
            if (var2 == LineStyle.Solid)
            {
                return "solid";
            }
            if (((var2 != LineStyle.Dash) && (var2 != LineStyle.Dot)) && ((var2 != LineStyle.DashDot) && (var2 != LineStyle.DashDotDot)))
            {
                return "dotted";
            }
            return "dashed";
        }

        internal static string mtd623(Color var3)
        {
            if ((var3 == Color.Transparent) || (var3 == Color.Empty))
            {
                var3 = Color.White;
            }
            return ColorTranslator.ToHtml(var3);
        }

        private static bool var1(double var0)
        {
            return ((Math.Round(var0, 0) - var0) == 0.0);
        }
    }
}

