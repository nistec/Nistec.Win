namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd148 : mtd126
    {
        internal object mtd137;
        internal mtd120 mtd145;
        internal mtd247 mtd147;
        internal mtd228 mtd242;

        internal mtd148()
        {
        }

        internal mtd148(ref McReportControl c) : base(ref c)
        {
            McLabel label = (McLabel) c;
            this.mtd145 = new mtd120(label.TextFont, label.TextAlign, label.WordWrap, label.RightToLeft);
            this.mtd147 = new mtd247(label.ForeColor, label.BackColor);
            this.mtd242 = new mtd228(ref label);
        }

        internal static mtd148 mtd105(ref mtd148 var0, mtd248 pc)
        {
            McLabel label = var0.mtd242._McLabel;
            mtd148 mtd = new mtd148();
            mtd.RptControl = var0.RptControl;
            mtd.mtd130 = label.Visible;
            mtd._Border = label.Border;
            mtd.mtd242 = var0.mtd242;
            McField field = var0.mtd242._McField;
            if (field != null)
            {
                mtd.mtd137 = field.Value;
            }
            else
            {
                mtd.mtd137 = label.Text;
            }
            if (pc.mtd57 != mtd249.mtd27)
            {
                mtd._Location = new McLocation(label.Left, label.Top, label.Width, label.Height, true);
            }
            else
            {
                mtd._Location = var0._Location;
            }
            if (pc.mtd51 != mtd251.mtd27)
            {
                mtd.mtd145 = new mtd120(label.TextFont, label.TextAlign, label.WordWrap, label.RightToLeft);
            }
            else
            {
                mtd.mtd145 = var0.mtd145;
            }
            if (pc.mtd59 != mtd250.mtd27)
            {
                mtd.mtd147 = new mtd247(label.ForeColor, label.BackColor);
                return mtd;
            }
            mtd.mtd147 = var0.mtd147;
            return mtd;
        }

        internal static void mtd22(Graphics g, mtd148 var0, RectangleF var1)
        {
            mtd10.mtd12(ref g, var1, var0.mtd147.BackColor, Convert.ToString(var0.mtd137), var0.mtd147.ForeColor, ref var0.mtd145._Font, var0.mtd145.mtd244, ref var0._Border);
        }

        internal static mtd148 mtd253(ref mtd148 var0)
        {
            mtd148 mtd = new mtd148();
            mtd.RptControl = var0.RptControl;
            mtd._Location = var0._Location;
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

