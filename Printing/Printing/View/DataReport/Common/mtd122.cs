namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class McLineStyle//mtd122
    {
        internal Color _Color;//mtd41;
        internal LineStyle _LineStyle;//mtd42;
        internal float _LineWeight;//mtd43;

        internal McLineStyle(ref McLine e)
        {
            this._LineStyle = e.LineStyle;
            this._Color = e.LineColor;
            this._LineWeight = e.LineWeight;
        }
    }
}

