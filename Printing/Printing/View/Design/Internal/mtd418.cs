namespace MControl.Printing.View.Design
{
    using System;
    using System.Windows.Forms;
    //mtd418
    internal class PaneBase : Panel
    {
        internal PaneBase()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
        }
    }
}

