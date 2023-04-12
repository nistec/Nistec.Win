namespace Nistec.Printing.View
{
    using System;

    internal class LinePosition //mtd123
    {
        internal bool mtd178;
        internal float X1;//mtd44;
        internal float Y1;//mtd45;
        internal float X2;//mtd46;
        internal float Y2;//mtd47;

        internal LinePosition(ref McLine e, bool var1)
        {
            this.X1 = e.X1;
            this.Y1 = e.Y1;
            this.X2 = e.X2;
            this.Y2 = e.Y2;
            this.mtd178 = var1;
        }

        internal LinePosition(ref LinePosition e, float var0, bool var1)
        {
            this.X1 = e.X1;
            this.Y1 = e.Y1 + var0;
            this.X2 = e.X2;
            this.Y2 = e.Y2 + var0;
            this.mtd178 = var1;
        }

        internal static LinePosition mtd253(ref LinePosition var3)
        {
            return new LinePosition(ref var3, 0f, true);
        }

        internal static void mtd257(ref LinePosition e, float var2)
        {
            if (!e.mtd178)
            {
                e = new LinePosition(ref e, var2, true);
            }
            else
            {
                e.Y1 += var2;
                e.Y2 += var2;
            }
        }
    }
}

