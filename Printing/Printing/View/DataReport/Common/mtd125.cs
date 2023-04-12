namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd125
    {
        internal Color BackColor;
        internal Color BorderColor;
        internal LineStyle mtd42;
        internal float mtd43;
        internal ShapeStyle mtd50;

        internal mtd125(ref McShape e)
        {
            this.BackColor = e.BackColor;
            this.BorderColor = e.LineColor;
            this.mtd42 = e.LineStyle;
            this.mtd43 = e.LineWeight;
            this.mtd50 = e.Style;
        }
    }
}

