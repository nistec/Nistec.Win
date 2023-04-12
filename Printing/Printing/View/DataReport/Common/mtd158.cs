namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd158 : mtd126
    {
        internal object mtd137;
        internal mtd120 mtd145;
        internal mtd247 mtd147;
        internal mtd159 mtd242;

        internal mtd158()
        {
        }

        internal mtd158(ref McReportControl c) : base(ref c)
        {
            McTextBox box = (McTextBox) c;
            this.mtd145 = new mtd120(box.TextFont, box.TextAlign, box.WordWrap, box.OutputFormat, box.RightToLeft);
            this.mtd147 = new mtd247(box.ForeColor, box.BackColor);
            this.mtd242 = new mtd159(ref box);
        }

        internal static mtd158 mtd105(ref mtd158 var0, mtd248 pc)
        {
            McTextBox box = var0.mtd242._McTextBox;
            mtd158 mtd = new mtd158();
            mtd.RptControl = var0.RptControl;
            mtd.mtd130 = box.Visible;
            mtd._Border = box.Border;
            mtd.mtd242 = var0.mtd242;
            mtd.mtd137 = box.Value;
            mtd.mtd242.mtd137 = mtd.mtd137;
            if (pc.mtd57 != mtd249.mtd27)
            {
                mtd._Location = new McLocation(box.Left, box.Top, box.Width, box.Height, true);
            }
            else
            {
                mtd._Location = var0._Location;
            }
            if (pc.mtd51 != mtd251.mtd27)
            {
                mtd.mtd145 = new mtd120(box.TextFont, box.TextAlign, box.WordWrap, box.OutputFormat, box.RightToLeft);
            }
            else
            {
                mtd.mtd145 = var0.mtd145;
            }
            if (pc.mtd59 != mtd250.mtd27)
            {
                mtd.mtd147 = new mtd247(box.ForeColor, box.BackColor);
                return mtd;
            }
            mtd.mtd147 = var0.mtd147;
            return mtd;
        }

        internal static void mtd22(Graphics g, mtd158 var1, RectangleF var2)
        {
            mtd120 mtd = var1.mtd145;
            mtd10.mtd12(ref g, var2, var1.mtd147.BackColor, mtd159.mtd160(var1.mtd137, mtd.OutputFormat), var1.mtd147.ForeColor, ref mtd._Font, mtd.mtd244, ref var1._Border);
        }

        internal static mtd158 mtd253(ref mtd158 var0)
        {
            mtd158 mtd = new mtd158();
            mtd.RptControl = var0.RptControl;
            mtd._Location = McLocation.mtd253(ref var0._Location);
            mtd.mtd145 = var0.mtd145;
            mtd.mtd147 = var0.mtd147;
            mtd.mtd130 = var0.mtd130;
            mtd._Border = var0._Border;
            mtd.mtd254 = true;
            mtd.mtd242 = var0.mtd242;
            mtd.mtd137 = var0.mtd137;
            return mtd;
        }
    }
}

