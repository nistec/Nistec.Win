namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd314
    {
        internal mtd126 mtd199;
        internal mtd158 mtd309;
        internal ControlType _ControlType;//_ControlType;
        internal int mtd316;
        internal mtd148 mtd317;
        internal mtd149 mtd318;
        internal mtd136 mtd319;
        internal mtd152 mtd320;
        internal mtd156 mtd321;
        internal mtd126 mtd322;
        internal mtd155 mtd323;
        internal mtd161 mtd324;
        internal mtd248 mtd325 = new mtd248();

        internal mtd314()
        {
        }

        internal static void mtd326(ref mtd314 var0, ref McReportControl c)
        {
            var0 = new mtd314();
            var0.mtd309 = new mtd158(ref c);
            var0.mtd199 = var0.mtd309;
            var0._ControlType = ControlType.TextBox;
            c.mtd101 += new mtd24(var0.mtd327);
        }

        internal void mtd327(object var3, mtd25 e)
        {
            if (e.mtd66 == mtd56.mtd57)
            {
                var4(ref this.mtd325.mtd57, e, this.mtd309._Location);
            }
            else if (e.mtd66 == mtd56.mtd58)
            {
                var5(ref this.mtd325.mtd51, e, this.mtd309.mtd145);
            }
            else if (e.mtd66 == mtd56.mtd59)
            {
                var6(ref this.mtd325.mtd59, e, this.mtd309.mtd147);
            }
        }

        internal static void mtd328(ref mtd314 var0, ref McReportControl c)
        {
            var0 = new mtd314();
            var0.mtd317 = new mtd148(ref c);
            var0.mtd199 = var0.mtd317;
            var0._ControlType = ControlType.Label;
            c.mtd101 += new mtd24(var0.mtd329);
        }

        internal void mtd329(object var3, mtd25 e)
        {
            if (e.mtd66 == mtd56.mtd57)
            {
                var4(ref this.mtd325.mtd57, e, this.mtd317._Location);
            }
            else if (e.mtd66 == mtd56.mtd58)
            {
                var5(ref this.mtd325.mtd51, e, this.mtd317.mtd145);
            }
            else if (e.mtd66 == mtd56.mtd59)
            {
                var6(ref this.mtd325.mtd59, e, this.mtd317.mtd147);
            }
        }

        internal static void mtd330(ref mtd314 var0, ref McReportControl c)
        {
            var0 = new mtd314();
            var0.mtd318 = new mtd149(ref c);
            var0.mtd199 = var0.mtd318;
            var0._ControlType = ControlType.Line;
            c.mtd101 += new mtd24(var0.mtd331);
        }

        internal void mtd331(object var3, mtd25 e)
        {
            if (e.mtd66 == mtd56.mtd57)
            {
                var4(ref this.mtd325.mtd57, e, this.mtd318._Location);
            }
            else if (e.mtd66 == mtd56.mtd60)
            {
                var7(ref this.mtd325.mtd60, e, this.mtd318.mtd150);
            }
            else if (e.mtd66 == mtd56.mtd61)
            {
                var8(ref this.mtd325.mtd61, e, this.mtd318.mtd151);
            }
        }

        internal static void mtd332(ref mtd314 var0, ref McReportControl c)
        {
            var0 = new mtd314();
            var0.mtd320 = new mtd152(ref c);
            var0.mtd199 = var0.mtd320;
            var0._ControlType = ControlType.Picture;
            c.mtd101 += new mtd24(var0.mtd333);
        }

        internal void mtd333(object var3, mtd25 e)
        {
            if (e.mtd66 == mtd56.mtd57)
            {
                var4(ref this.mtd325.mtd57, e, this.mtd320._Location);
            }
            else if (e.mtd66 == mtd56.mtd9)
            {
                var9(ref this.mtd325.mtd9, e, this.mtd320.mtd153);
            }
        }

        internal static void mtd334(ref mtd314 var0, ref McReportControl c)
        {
            var0 = new mtd314();
            var0.mtd319 = new mtd136(ref c);
            var0.mtd199 = var0.mtd319;
            var0._ControlType = ControlType.CheckBox;
            c.mtd101 += new mtd24(var0.mtd335);
        }

        internal void mtd335(object var3, mtd25 e)
        {
            McCheckBox box = this.mtd319.mtd242.mtd227;
            if (e.mtd66 == mtd56.mtd57)
            {
                if (var4(ref this.mtd325.mtd57, e, this.mtd319._Location))
                {
                    var11(ref this.mtd325.mtd62, this.mtd319.mtd146, box.mtd95, box.mtd96);
                }
            }
            else if (e.mtd66 == mtd56.mtd59)
            {
                var6(ref this.mtd325.mtd59, e, this.mtd319.mtd147);
            }
            else if (e.mtd66 == mtd56.mtd62)
            {
                var11(ref this.mtd325.mtd62, this.mtd319.mtd146, box.mtd95, box.mtd96);
                var12(ref this.mtd325.mtd62, e, this.mtd319.mtd146);
            }
        }

        internal static void mtd336(ref mtd314 var0, ref McReportControl c)
        {
            var0 = new mtd314();
            var0.mtd321 = new mtd156(ref c);
            var0.mtd199 = var0.mtd321;
            var0._ControlType = ControlType.Shape;
            c.mtd101 += new mtd24(var0.mtd337);
        }

        internal void mtd337(object var3, mtd25 e)
        {
            if (e.mtd66 == mtd56.mtd57)
            {
                var4(ref this.mtd325.mtd57, e, this.mtd321._Location);
            }
            else if (e.mtd66 == mtd56.mtd63)
            {
                var10(ref this.mtd325.mtd63, e, this.mtd321.mtd157);
            }
        }

        internal static void mtd338(ref mtd314 var0, ref McReportControl c)
        {
            var0 = new mtd314();
            var0.mtd322 = new mtd126(ref c);
            var0.mtd199 = var0.mtd322;
            var0._ControlType = ControlType.PageBreak;
        }

        internal static void mtd339(ref mtd314 var0, ref mtd155 var1)
        {
            var0 = new mtd314();
            var0.mtd323 = var1;
            var0.mtd199 = var0.mtd323;
            var0._ControlType = ControlType.RichTextField;
            var1.RptControl.mtd101 += new mtd24(var0.mtd340);
        }

        internal void mtd340(object var3, mtd25 e)
        {
            if (e.mtd66 == mtd56.mtd57)
            {
                var4(ref this.mtd325.mtd57, e, this.mtd323._Location);
            }
            else if (e.mtd66 == mtd56.mtd59)
            {
                var6(ref this.mtd325.mtd59, e, this.mtd323.mtd147);
            }
        }

        internal static void mtd341(ref mtd314 var0, ref mtd161 var2)
        {
            var0 = new mtd314();
            var0.mtd324 = var2;
            var0.mtd199 = var0.mtd324;
            var0._ControlType = ControlType.SubReport;
            var2.RptControl.mtd101 += new mtd24(var0.mtd342);
        }

        internal void mtd342(object var3, mtd25 e)
        {
        }

        private static void var10(ref mtd261 var23, mtd25 e, mtd125 var24)
        {
            if (e.mtd65 == mtd26.mtd42)
            {
                if (var24.mtd42 != ((LineStyle) e.mtd68))
                {
                    var23 |= mtd261.mtd42;
                }
                else if ((var23 & mtd261.mtd42) == mtd261.mtd42)
                {
                    var23 ^= mtd261.mtd42;
                }
            }
            else if (e.mtd65 == mtd26.BorderColor)
            {
                if (var24.BorderColor != ((Color) e.mtd68))
                {
                    var23 |= mtd261.BorderColor;
                }
                else if ((var23 & mtd261.BorderColor) == mtd261.BorderColor)
                {
                    var23 ^= mtd261.BorderColor;
                }
            }
            else if (e.mtd65 == mtd26.mtd43)
            {
                if (var24.mtd43 != ((float) e.mtd68))
                {
                    var23 |= mtd261.mtd43;
                }
                else if ((var23 & mtd261.mtd43) == mtd261.mtd43)
                {
                    var23 ^= mtd261.mtd43;
                }
            }
            else if (e.mtd65 == mtd26.mtd50)
            {
                if (var24.mtd50 != ((ShapeStyle) e.mtd68))
                {
                    var23 |= mtd261.mtd50;
                }
                else if ((var23 & mtd261.mtd50) == mtd261.mtd50)
                {
                    var23 ^= mtd261.mtd50;
                }
            }
        }

        private static void var11(ref mtd252 var25, mtd121 var26, RectangleF var27, RectangleF var28)
        {
            if (var26.mtd133 != var27)
            {
                var25 |= mtd252.mtd133;
            }
            else if ((var25 & mtd252.mtd133) == mtd252.mtd133)
            {
                var25 ^= mtd252.mtd133;
            }
            if (var26.mtd134 != var28)
            {
                var25 |= mtd252.mtd134;
            }
            else if ((var25 & mtd252.mtd134) == mtd252.mtd134)
            {
                var25 ^= mtd252.mtd134;
            }
        }

        private static void var12(ref mtd252 var25, mtd25 e, mtd121 var26)
        {
            if (e.mtd65 == mtd26.mtd51)
            {
                if (var26.mtd51 != ((string) e.mtd68))
                {
                    var25 |= mtd252.mtd51;
                }
                else if ((var25 & mtd252.mtd51) == mtd252.mtd51)
                {
                    var25 ^= mtd252.mtd51;
                }
            }
        }

        private static bool var4(ref mtd249 boundchanged, mtd25 e, McLocation bounddata)
        {
            bool flag = false;
            if (e.mtd65 == mtd26.mtd29)
            {
                if (bounddata.Top != ((float)e.mtd68))
                {
                    boundchanged |= mtd249.mtd29;
                    return flag;
                }
                if ((boundchanged & mtd249.mtd29) == mtd249.mtd29)
                {
                    boundchanged ^= mtd249.mtd29;
                }
                return flag;
            }
            if (e.mtd65 == mtd26.mtd28)
            {
                if (bounddata.Left != ((float)e.mtd68))
                {
                    boundchanged |= mtd249.mtd28;
                    return flag;
                }
                if ((boundchanged & mtd249.mtd28) == mtd249.mtd28)
                {
                    boundchanged ^= mtd249.mtd28;
                }
                return flag;
            }
            if (e.mtd65 == mtd26.mtd30)
            {
                if (bounddata.Width != ((float)e.mtd68))
                {
                    boundchanged |= mtd249.mtd30;
                    return true;
                }
                if ((boundchanged & mtd249.mtd30) == mtd249.mtd30)
                {
                    boundchanged ^= mtd249.mtd30;
                }
                return flag;
            }
            if (e.mtd65 == mtd26.mtd31)
            {
                if (bounddata.Height != ((float)e.mtd68))
                {
                    boundchanged |= mtd249.mtd31;
                    return true;
                }
                if ((boundchanged & mtd249.mtd31) == mtd249.mtd31)
                {
                    boundchanged ^= mtd249.mtd31;
                }
                return flag;
            }
            if (e.mtd65 == mtd26.mtd33)
            {
                PointF tf = (PointF) e.mtd68;
                if (bounddata.Left != tf.X)
                {
                    boundchanged |= mtd249.mtd28;
                }
                else if ((boundchanged & mtd249.mtd28) == mtd249.mtd28)
                {
                    boundchanged ^= mtd249.mtd28;
                }
                if (bounddata.Top != tf.Y)
                {
                    boundchanged |= mtd249.mtd29;
                    return flag;
                }
                if ((boundchanged & mtd249.mtd29) == mtd249.mtd29)
                {
                    boundchanged ^= mtd249.mtd29;
                }
                return flag;
            }
            if (e.mtd65 == mtd26.mtd32)
            {
                SizeF ef = (SizeF) e.mtd68;
                if (bounddata.Width != ef.Width)
                {
                    boundchanged |= mtd249.mtd30;
                    flag = true;
                }
                else if ((boundchanged & mtd249.mtd30) == mtd249.mtd30)
                {
                    boundchanged ^= mtd249.mtd30;
                }
                if (bounddata.Height != ef.Height)
                {
                    boundchanged |= mtd249.mtd31;
                    return true;
                }
                if ((boundchanged & mtd249.mtd31) == mtd249.mtd31)
                {
                    boundchanged ^= mtd249.mtd31;
                }
            }
            return flag;
        }

        private static bool var5(ref mtd251 var13, mtd25 e, mtd120 var14)
        {
            if (e.mtd65 == mtd26.mtd34)
            {
                if (var14._Font != ((Font) e.mtd68))
                {
                    var13 |= mtd251.Font;
                }
                else if ((var13 & mtd251.Font) == mtd251.Font)
                {
                    var13 ^= mtd251.Font;
                }
            }
            else if (e.mtd65 == mtd26.OutputFormat)
            {
                if (var14.OutputFormat != ((string) e.mtd68))
                {
                    var13 |= mtd251.OutputFormat;
                }
                else if ((var13 & mtd251.OutputFormat) == mtd251.OutputFormat)
                {
                    var13 ^= mtd251.OutputFormat;
                }
            }
            else if (e.mtd65 == mtd26.ContentAlignment)
            {
                if (var14._ContentAlignment != ((ContentAlignment) e.mtd68))
                {
                    var13 |= mtd251.ContentAlignment;
                }
                else if ((var13 & mtd251.ContentAlignment) == mtd251.ContentAlignment)
                {
                    var13 ^= mtd251.ContentAlignment;
                }
            }
            else if (e.mtd65 == mtd26.LineLimit)
            {
                if (var14.LineLimit != ((bool) e.mtd68))
                {
                    var13 |= mtd251.LineLimit;
                }
                else if ((var13 & mtd251.LineLimit) == mtd251.LineLimit)
                {
                    var13 ^= mtd251.LineLimit;
                }
            }
            else if (e.mtd65 == mtd26.RightToLeft)
            {
                if (var14.RightToLeft != ((bool) e.mtd68))
                {
                    var13 |= mtd251.RightToLeft;
                }
                else if ((var13 & mtd251.RightToLeft) == mtd251.RightToLeft)
                {
                    var13 ^= mtd251.RightToLeft;
                }
            }
            return false;
        }

        private static void var6(ref mtd250 var15, mtd25 e, mtd247 var16)
        {
            if (e.mtd65 == mtd26.BackColor)
            {
                if (var16.BackColor != ((Color) e.mtd68))
                {
                    var15 |= mtd250.BackColor;
                }
                else if ((var15 & mtd250.BackColor) == mtd250.BackColor)
                {
                    var15 ^= mtd250.BackColor;
                }
            }
            else if (e.mtd65 == mtd26.ForeColor)
            {
                if (var16.ForeColor != ((Color) e.mtd68))
                {
                    var15 |= mtd250.ForeColor;
                }
                else if ((var15 & mtd250.ForeColor) == mtd250.ForeColor)
                {
                    var15 ^= mtd250.ForeColor;
                }
            }
        }

        private static void var7(ref mtd255 var17, mtd25 e, McLineStyle var18)
        {
            if (e.mtd65 == mtd26.mtd42)
            {
                if (var18._LineStyle != ((LineStyle)e.mtd68))
                {
                    var17 |= mtd255.mtd42;
                }
                else if ((var17 & mtd255.mtd42) == mtd255.mtd42)
                {
                    var17 ^= mtd255.mtd42;
                }
            }
            else if (e.mtd65 == mtd26.BorderColor)
            {
                if (var18._Color != ((Color)e.mtd68))
                {
                    var17 |= mtd255.BorderColor;
                }
                else if ((var17 & mtd255.BorderColor) == mtd255.BorderColor)
                {
                    var17 ^= mtd255.BorderColor;
                }
            }
            else if (e.mtd65 == mtd26.mtd43)
            {
                if (var18._LineWeight != ((float)e.mtd68))
                {
                    var17 |= mtd255.mtd43;
                }
                else if ((var17 & mtd255.mtd43) == mtd255.mtd43)
                {
                    var17 ^= mtd255.mtd43;
                }
            }
        }

        private static void var8(ref mtd256 var19, mtd25 e, LinePosition var20)
        {
            if (e.mtd65 == mtd26.mtd44)
            {
                if (var20.X1 != ((float)e.mtd68))
                {
                    var19 |= mtd256.mtd44;
                }
                else if ((var19 & mtd256.mtd44) == mtd256.mtd44)
                {
                    var19 ^= mtd256.mtd44;
                }
            }
            else if (e.mtd65 == mtd26.mtd45)
            {
                if (var20.Y1 != ((float)e.mtd68))
                {
                    var19 |= mtd256.mtd45;
                }
                else if ((var19 & mtd256.mtd45) == mtd256.mtd45)
                {
                    var19 ^= mtd256.mtd45;
                }
            }
            else if (e.mtd65 == mtd26.mtd46)
            {
                if (var20.X2 != ((float)e.mtd68))
                {
                    var19 |= mtd256.mtd46;
                }
                else if ((var19 & mtd256.mtd46) == mtd256.mtd46)
                {
                    var19 ^= mtd256.mtd46;
                }
            }
            else if (e.mtd65 == mtd26.mtd47)
            {
                if (var20.Y2 != ((float)e.mtd68))
                {
                    var19 |= mtd256.mtd47;
                }
                else if ((var19 & mtd256.mtd47) == mtd256.mtd47)
                {
                    var19 ^= mtd256.mtd47;
                }
            }
        }

        private static void var9(ref mtd258 var21, mtd25 e, mtd124 var22)
        {
            if (e.mtd65 == mtd26.BackColor)
            {
                if (var22.BackColor != ((Color) e.mtd68))
                {
                    var21 |= mtd258.BackColor;
                }
                else if ((var21 & mtd258.BackColor) == mtd258.BackColor)
                {
                    var21 ^= mtd258.BackColor;
                }
            }
            else if (e.mtd65 == mtd26.mtd48)
            {
                if (var22.mtd48 != ((PictureAlignment) e.mtd68))
                {
                    var21 |= mtd258.mtd48;
                }
                else if ((var21 & mtd258.mtd48) == mtd258.mtd48)
                {
                    var21 ^= mtd258.mtd48;
                }
            }
            else if (e.mtd65 == mtd26.mtd49)
            {
                if (var22.mtd49 != ((SizeMode) e.mtd68))
                {
                    var21 |= mtd258.mtd49;
                }
                else if ((var21 & mtd258.mtd49) == mtd258.mtd49)
                {
                    var21 ^= mtd258.mtd49;
                }
            }
        }
    }
}

