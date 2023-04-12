namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd159
    {
        internal object mtd137;
        internal McField _McField;//mtd204;
        internal int mtd235;
        internal int mtd236;
        internal float mtd237;
        internal McTextBox _McTextBox;//mtd238;
        private static int var0 = 2;
        private static Graphics var1 = Graphics.FromHwnd(new IntPtr(0));

        internal mtd159()
        {
        }

        internal mtd159(ref McTextBox var2)
        {
            this._McTextBox = var2;
            mtd159 mtd = this;
            var3(ref mtd);
        }

        internal static string mtd160(object var11, string var12)
        {
            if ((var11 == null) || (var11 == DBNull.Value))
            {
                return string.Empty;
            }
            if (var11 is string)
            {
                return Convert.ToString(var11);
            }
            return mtd245.mtd246(ref var11, ref var12);
        }

        internal static float mtd241(ref mtd158 var5, ref McLocation var6)
        {
            if (var5.mtd137 != null)
            {
                McLocation mtd = var5._Location;
                mtd159 mtd2 = var5.mtd242;
                McTextBox box = mtd2._McTextBox;
                if (box.CanGrow)
                {
                    float num;
                    string str = mtd160(var5.mtd137, mtd2.OutputFormat);
                    if ((((var6.Width < mtd.Width) | (var6.Height < mtd.Height)) | (str.Length > (mtd2.mtd235 * mtd2.mtd236))) | (mtd2.mtd237 > var5._Location.Height))
                    {
                        num = var7(ref var5.mtd145, mtd.Width, ref str);
                        if (num < var5._Location.Height)
                        {
                            if (box.CanShrink)
                            {
                                McLocation.mtd243(ref var5._Location, num);
                            }
                        }
                        else
                        {
                            McLocation.mtd243(ref var5._Location, num);
                        }
                    }
                    else if ((box.CanShrink && (str.Length <= mtd2.mtd235)) && (mtd2.mtd236 > 1))
                    {
                        num = var7(ref var5.mtd145, mtd.Width, ref str);
                        if (num < var5._Location.Height)
                        {
                            McLocation.mtd243(ref var5._Location, num);
                        }
                    }
                }
            }
            return (var5._Location.Top + var5._Location.Height);
        }

        private static void var3(ref mtd159 var4)
        {
            Font textFont = var4._McTextBox.TextFont;
            bool wordWrap = var4._McTextBox.WordWrap;
            float num = (var4._McTextBox.Width * 72f) - (2 * var0);
            float num2 = var4._McTextBox.Height - (2f * (((float)var0) / 72f));
            FontFamily fontFamily = textFont.FontFamily;
            float sizeInPoints = textFont.SizeInPoints;
            var4.mtd237 = (fontFamily.GetLineSpacing(textFont.Style) * sizeInPoints) / ((float) (fontFamily.GetEmHeight(textFont.Style) * 0x48));
            var4.mtd235 = (int) Math.Floor((double) (num / sizeInPoints));
            if (wordWrap)
            {
                var4.mtd236 = (int) Math.Floor((double) (num2 / var4.mtd237));
            }
            else
            {
                var4.mtd236 = 1;
            }
        }

        private static float var7(ref mtd120 var8, float var9, ref string var10)
        {
            int num = 2 * var0;
            GraphicsUnit pageUnit = var1.PageUnit;
            var1.PageUnit = GraphicsUnit.Point;
            float num2 = (var1.MeasureString(var10, var8._Font, (int) (((int) (var9 * 72f)) - num), var8.mtd244).Height + num) / 72f;
            var1.PageUnit = pageUnit;
            return num2;
        }

        internal object Value//mtd239
        {
            get
            {
                return this._McTextBox.Value;
            }
        }

        internal AggregateType AggregateType//mtd240
        {
            get
            {
                return this._McTextBox.SummaryFunc;
            }
        }

        internal string OutputFormat//mtd37
        {
            get
            {
                return this._McTextBox.OutputFormat;
            }
        }
    }
}

