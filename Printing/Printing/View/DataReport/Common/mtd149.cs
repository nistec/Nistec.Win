namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd149 : mtd126
    {
        internal McLineStyle mtd150;
        internal LinePosition mtd151;
        internal mtd230 mtd242;

        internal mtd149()
        {
        }

        internal mtd149(ref McReportControl c) : base(ref c)
        {
            McLine line = (McLine) c;
            this.mtd242 = new mtd230(ref line);
            this.mtd150 = new McLineStyle(ref line);
            this.mtd151 = new LinePosition(ref line, false);
        }

        internal static mtd149 mtd105(ref mtd149 var0, mtd248 pc)
        {
            McLine e = var0.mtd242.mtd60;
            mtd149 mtd = new mtd149();
            mtd.RptControl = var0.RptControl;
            mtd.mtd130 = e.Visible;
            mtd.mtd242 = var0.mtd242;
            if (pc.mtd60 != mtd255.mtd27)
            {
                mtd.mtd150 = new McLineStyle(ref e);
            }
            else
            {
                mtd.mtd150 = var0.mtd150;
            }
            if (pc.mtd61 != mtd256.mtd27)
            {
                mtd.mtd151 = new LinePosition(ref e, true);
                return mtd;
            }
            mtd.mtd151 = var0.mtd151;
            return mtd;
        }

        internal static void mtd22(Graphics g, mtd149 var3, float var4, float var5, float var6)
        {
            float num;
            float num2;
            float num3;
            float num4;
            float num5;
            McLineStyle mtd = var3.mtd150;
            LinePosition mtd2 = var3.mtd151;
            if (g.PageUnit == GraphicsUnit.Display)
            {
                num5 = var4 + (var6 * ReportUtil.Dpi);
                num = (mtd2.X1 * ReportUtil.Dpi) + var5;
                num2 = (mtd2.Y1 * ReportUtil.Dpi) + num5;
                num3 = (mtd2.X2 * ReportUtil.Dpi) + var5;
                num4 = (mtd2.Y2 * ReportUtil.Dpi) + num5;
            }
            else
            {
                num5 = var4 + var6;
                num = mtd2.X1 + var5;
                num2 = mtd2.Y1 + num5;
                num3 = mtd2.X2 + var5;
                num4 = mtd2.Y2 + num5;
            }
            mtd10.mtd15(ref g, num, num2, num3, num4, mtd._Color, mtd._LineStyle, mtd._LineWeight);
        }

        internal static mtd149 mtd253(ref mtd149 var0)
        {
            mtd149 mtd = new mtd149();
            mtd.RptControl = var0.RptControl;
            mtd._Location = var0._Location;
            mtd.mtd150 = var0.mtd150;
            mtd.mtd151 = LinePosition.mtd253(ref var0.mtd151);
            mtd.mtd130 = var0.mtd130;
            mtd.mtd254 = true;
            mtd.mtd242 = var0.mtd242;
            return mtd;
        }

        private static float var1(ref LinePosition var2)
        {
            if (var2.Y1 > var2.Y2)
            {
                return var2.Y1;
            }
            return var2.Y2;
        }

        internal override float mtd128
        {
            get
            {
                if (this.mtd151.X1 > this.mtd151.X2)
                {
                    return this.mtd151.X1;
                }
                return this.mtd151.X2;
            }
        }

        internal override float mtd129
        {
            get
            {
                return var1(ref this.mtd151);
            }
            set
            {
                LinePosition.mtd257(ref this.mtd151, value - var1(ref this.mtd151));
            }
        }

        internal override float Width//mtd30
        {
            get
            {
                if (this.mtd151.X1 > this.mtd151.X2)
                {
                    return (this.mtd151.X1 - this.mtd151.X2);
                }
                return (this.mtd151.X2 - this.mtd151.X1);
            }
        }

        internal override float Height//mtd31
        {
            get
            {
                if (this.mtd151.Y1 > this.mtd151.Y2)
                {
                    return (this.mtd151.Y1 - this.mtd151.Y2);
                }
                return (this.mtd151.Y2 - this.mtd151.Y1);
            }
        }
    }
}

