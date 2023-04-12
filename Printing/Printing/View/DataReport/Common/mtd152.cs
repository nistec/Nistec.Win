namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd152 : mtd126
    {
        internal mtd124 mtd153;
        internal Image mtd154;
        internal mtd231 mtd242;

        internal mtd152()
        {
        }

        internal mtd152(ref McReportControl c) : base(ref c)
        {
            McPicture picture = (McPicture) c;
            this.mtd242 = new mtd231(ref picture);
            this.mtd153 = new mtd124(picture.PictureAlignment, picture.SizeMode, picture.BackColor);
            this.mtd154 = picture.Image;
        }

        internal static mtd152 mtd105(ref mtd152 var0, mtd248 pc)
        {
            McPicture picture = var0.mtd242.mtd232;
            McField field1 = var0.mtd242.mtd204;
            mtd152 mtd = new mtd152();
            mtd.RptControl = var0.RptControl;
            mtd.mtd130 = picture.Visible;
            mtd._Border = picture.Border;
            mtd.mtd154 = picture.Image;
            mtd.mtd242 = var0.mtd242;
            if (pc.mtd57 != mtd249.mtd27)
            {
                mtd._Location = new McLocation(picture.Left, picture.Top, picture.Width, picture.Height, true);
            }
            else
            {
                mtd._Location = var0._Location;
            }
            if (pc.mtd9 != mtd258.mtd27)
            {
                mtd.mtd153 = new mtd124(picture.PictureAlignment, picture.SizeMode, picture.BackColor);
                return mtd;
            }
            mtd.mtd153 = var0.mtd153;
            return mtd;
        }

        internal static void mtd22(Graphics g, mtd152 var0, RectangleF var1)
        {
            mtd124 mtd = var0.mtd153;
            var0.mtd242.mtd232.PaintCtl(g, var1, ref var0.mtd154, mtd.mtd48, mtd.mtd49, mtd.BackColor);
        }

        internal static mtd152 mtd253(ref mtd152 var0)
        {
            mtd152 mtd = new mtd152();
            mtd.RptControl = var0.RptControl;
            mtd._Location = var0._Location;
            mtd.mtd153 = var0.mtd153;
            mtd.mtd130 = var0.mtd130;
            mtd._Border = var0._Border;
            mtd.mtd254 = true;
            mtd.mtd242 = var0.mtd242;
            mtd.mtd154 = var0.mtd154;
            return mtd;
        }
    }
}

