namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class mtd625
    {
        internal mtd625()
        {
        }

        internal static void mtd22(Border var0, ref StreamWriter var1)
        {
            HTMLineStyle style;
            float num = 0f;
            if (var0.BorderTopStyle != BorderLineStyle.None)
            {
                var2(var0.BorderTopStyle, out num, out style);
                var1.Write(string.Format("border-top-style: {0}; ", style));
                var1.Write(string.Format("border-top-width: {0}pt; ", mtd620.mtd621(num)));
                var1.Write(string.Format("border-top-color: {0}; ", ColorTranslator.ToHtml(var0.BorderTopColor)));
            }
            if (var0.BorderLeftStyle != BorderLineStyle.None)
            {
                var2(var0.BorderLeftStyle, out num, out style);
                var1.Write(string.Format("border-left-style: {0}; ", style));
                var1.Write(string.Format("border-left-width: {0}pt; ", mtd620.mtd621(num)));
                var1.Write(string.Format("border-left-color: {0}; ", ColorTranslator.ToHtml(var0.BorderLeftColor)));
            }
            if (var0.BorderBottomStyle != BorderLineStyle.None)
            {
                var2(var0.BorderBottomStyle, out num, out style);
                var1.Write(string.Format("border-bottom-style: {0}; ", style));
                var1.Write(string.Format("border-bottom-width: {0}pt; ", mtd620.mtd621(num)));
                var1.Write(string.Format("border-bottom-color: {0}; ", ColorTranslator.ToHtml(var0.BorderBottomColor)));
            }
            if (var0.BorderRightStyle != BorderLineStyle.None)
            {
                var2(var0.BorderRightStyle, out num, out style);
                var1.Write(string.Format("border-right-style: {0}; ", style));
                var1.Write(string.Format("border-right-width: {0}pt; ", mtd620.mtd621(num)));
                var1.Write(string.Format("border-right-color: {0}; ", ColorTranslator.ToHtml(var0.BorderRightColor)));
            }
        }

        private static void var2(BorderLineStyle var3, out float var4, out HTMLineStyle var5)
        {
            if (var3 == BorderLineStyle.Solid)
            {
                var4 = 1f;
                var5 = HTMLineStyle.solid;
            }
            else if (var3 == BorderLineStyle.ThickSolid)
            {
                var4 = 1.5f;
                var5 = HTMLineStyle.solid;
            }
            else if (var3 == BorderLineStyle.ThickSolid)
            {
                var4 = 2f;
                var5 = HTMLineStyle.solid;
            }
            else if (((var3 == BorderLineStyle.Dash) || (var3 == BorderLineStyle.DashDot)) || ((var3 == BorderLineStyle.DashDotDot) || (var3 == BorderLineStyle.Dot)))
            {
                var4 = 1f;
                var5 = HTMLineStyle.dashed;
            }
            else if (((var3 == BorderLineStyle.ThickDash) || (var3 == BorderLineStyle.ThickDashDot)) || ((var3 == BorderLineStyle.ThickDashDotDot) || (var3 == BorderLineStyle.ThickDot)))
            {
                var4 = 1.5f;
                var5 = HTMLineStyle.dashed;
            }
            else if (var3 == BorderLineStyle.Double)
            {
                var4 = 1f;
                var5 = HTMLineStyle.Double;
            }
            else
            {
                var4 = 1.5f;
                var5 = HTMLineStyle.Double;
            }
        }
    }
}

