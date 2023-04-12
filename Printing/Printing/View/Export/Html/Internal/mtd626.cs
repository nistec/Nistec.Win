namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.IO;

    internal class mtd626
    {
        internal mtd626()
        {
        }

        internal static void mtd22(PropDoc var0, ref StreamWriter var1)
        {
            Font font = var0.Font;
            if (font != null)
            {
                var1.Write(string.Format("font-family: {0}; ", font.Name));
                var1.Write(string.Format("font-size: {0}pt; ", mtd620.mtd621(font.SizeInPoints)));
                if ((font.Style & FontStyle.Italic) == FontStyle.Italic)
                {
                    var1.Write("font-style: italic; ");
                }
                if ((font.Style & FontStyle.Bold) == FontStyle.Bold)
                {
                    var1.Write("font-weight: bold; ");
                }
                else
                {
                    var1.Write("font-weight: normal; ");
                }
                if ((font.Style & FontStyle.Underline) == FontStyle.Underline)
                {
                    var1.Write("text-decoration: underline; ");
                }
                var1.Write(string.Format("color: {0}; ", mtd620.mtd623(var0.ForeColor)));
                switch (var0.ContentAlignment)
                {
                    case ContentAlignment.TopLeft:
                        return;

                    case ContentAlignment.TopCenter:
                        var1.Write("text-align:center; ");
                        return;

                    case ContentAlignment.TopRight:
                        var1.Write("text-align:right; ");
                        return;

                    case ContentAlignment.MiddleLeft:
                        var1.Write("vertical-align:middle; ");
                        return;

                    case ContentAlignment.MiddleCenter:
                        var1.Write("text-align:center; vertical-align:middle; ");
                        return;

                    case ContentAlignment.MiddleRight:
                        var1.Write("text-align:right; vertical-align:middle; ");
                        return;

                    case ContentAlignment.BottomLeft:
                        var1.Write("vertical-align:bottom; ");
                        return;

                    case ContentAlignment.BottomCenter:
                        var1.Write("text-align:center; vertical-align:bottom; ");
                        return;

                    case ContentAlignment.BottomRight:
                        var1.Write("text-align:right; vertical-align:bottom; ");
                        break;
                }
            }
        }
    }
}

