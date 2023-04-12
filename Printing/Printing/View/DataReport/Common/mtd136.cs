namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd136 : mtd126
    {
        internal bool mtd135;
        internal mtd120 mtd145;
        internal mtd121 mtd146;
        internal mtd247 mtd147;
        internal mtd226 mtd242;

        internal mtd136()
        {
        }

        internal mtd136(ref McReportControl c) : base(ref c)
        {
            McCheckBox box = (McCheckBox) c;
            this.mtd147 = new mtd247(box.ForeColor, box.BackColor);
            this.mtd145 = new mtd120(box.TextFont, box.CheckAlignment, true, box.RightToLeft);
            this.mtd146 = new mtd121(box.mtd95, box.mtd96, box.Text);
            this.mtd242 = new mtd226(ref box);
        }

        internal static mtd136 mtd105(ref mtd136 var0, mtd248 pc)
        {
            McCheckBox box = var0.mtd242.mtd227;
            mtd136 mtd = new mtd136();
            mtd.RptControl = var0.RptControl;
            mtd.mtd242 = var0.mtd242;
            mtd.mtd130 = box.Visible;
            mtd._Border = box.Border;
            mtd.mtd135 = box.Checked;
            if (pc.mtd57 != mtd249.mtd27)
            {
                mtd._Location = new McLocation(box.Left, box.Top, box.Width, box.Height, true);
            }
            else
            {
                mtd._Location = var0._Location;
            }
            if (pc.mtd59 != mtd250.mtd27)
            {
                mtd.mtd147 = new mtd247(box.ForeColor, box.BackColor);
            }
            else
            {
                mtd.mtd147 = var0.mtd147;
            }
            if (pc.mtd51 != mtd251.mtd27)
            {
                mtd.mtd145 = new mtd120(box.TextFont, box.CheckAlignment, true, box.RightToLeft);
            }
            else
            {
                mtd.mtd145 = var0.mtd145;
            }
            if (pc.mtd62 != mtd252.mtd27)
            {
                mtd.mtd146 = new mtd121(box.mtd95, box.mtd96, box.Text);
                return mtd;
            }
            mtd.mtd146 = var0.mtd146;
            return mtd;
        }

        internal static void mtd22(Graphics g, mtd136 var1, RectangleF var2)
        {
            mtd10.mtd17(ref g, var2, ref var1.mtd145._Font, var1.mtd145.mtd244, var1.mtd147.BackColor, var1.mtd147.ForeColor, var1.mtd146.mtd51, var1.mtd135, var1.mtd146.mtd134, var1.mtd146.mtd133, ref var1._Border, false);
        }

        internal static mtd136 mtd253(ref mtd136 var0)
        {
            mtd136 mtd = new mtd136();
            mtd.RptControl = var0.RptControl;
            mtd._Location = var0._Location;
            mtd.mtd147 = var0.mtd147;
            mtd.mtd145 = var0.mtd145;
            mtd.mtd146 = var0.mtd146;
            mtd.mtd130 = var0.RptControl.Visible;
            mtd._Border = var0.RptControl.Border;
            mtd.mtd135 = var0.mtd135;
            mtd.mtd242 = var0.mtd242;
            return mtd;
        }
    }
}

