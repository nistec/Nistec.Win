namespace Nistec.Printing.View
{
    using System;

    internal class McLocation//mtd119
    {
        internal float Left;//mtd128;
        internal float Top;//mtd129;
        internal bool mtd178;
        internal float Width;//mtd30;
        internal float Height;//mtd31;

         //mtd119(box.Left, box.Top, box.Width, box.Height, true);
        internal McLocation(float left, float top, float width, float height, bool var4)
        {
            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.Height = height;
            this.mtd178 = var4;
        }

        internal static void mtd243(ref McLocation var5, float height)
        {
            if (var5.Height != height)
            {
                if (!var5.mtd178)
                {
                    var5 = new McLocation(var5.Left, var5.Top, var5.Width, height, true);
                }
                else
                {
                    var5.Height = height;
                }
            }
        }

        internal static McLocation mtd253(ref McLocation var5)
        {
            return new McLocation(var5.Left, var5.Top, var5.Width, var5.Height, true);
        }

        internal static void mtd257(ref McLocation var5, float top)
        {
            if (var5.Top != top)
            {
                if (!var5.mtd178)
                {
                    var5 = new McLocation(var5.Left, top, var5.Width, var5.Height, true);
                }
                else
                {
                    var5.Top = top;
                }
            }
        }
    }
}

