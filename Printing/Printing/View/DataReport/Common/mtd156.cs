namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd156 : mtd126
    {
        internal mtd125 mtd157;
        internal mtd234 mtd242;

        internal mtd156()
        {
        }

        internal mtd156(ref McReportControl c) : base(ref c)
        {
            McShape s = (McShape) c;
            this.mtd242 = new mtd234(ref s);
            this.mtd157 = new mtd125(ref s);
        }

        internal static mtd156 mtd105(ref mtd156 var0, mtd248 pc)
        {
            McShape e = var0.mtd242.mtd63;
            mtd156 mtd = new mtd156();
            mtd.RptControl = var0.RptControl;
            mtd.mtd130 = e.Visible;
            mtd._Border = e.Border;
            mtd.mtd242 = var0.mtd242;
            if (pc.mtd57 != mtd249.mtd27)
            {
                mtd._Location = new McLocation(e.Left, e.Top, e.Width, e.Height, true);
            }
            else
            {
                mtd._Location = var0._Location;
            }
            if (pc.mtd63 != mtd261.mtd27)
            {
                mtd.mtd157 = new mtd125(ref e);
                return mtd;
            }
            mtd.mtd157 = var0.mtd157;
            return mtd;
        }

        internal static void mtd22(Graphics g, mtd156 var1, RectangleF var2)
        {
            mtd125 mtd = var1.mtd157;
            mtd10.mtd16(ref g, var2, mtd.mtd50, mtd.BackColor, mtd.BorderColor, mtd.mtd42, mtd.mtd43);
        }

        internal static mtd156 mtd253(ref mtd156 var0)
        {
            mtd156 mtd = new mtd156();
            mtd.RptControl = var0.RptControl;
            mtd._Location = var0._Location;
            mtd.mtd157 = var0.mtd157;
            mtd.mtd130 = var0.RptControl.Visible;
            mtd._Border = var0.RptControl.Border;
            mtd.mtd242 = var0.mtd242;
            return mtd;
        }
    }
}

