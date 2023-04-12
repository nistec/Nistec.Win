namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.IO;

    internal class mtd634
    {
        internal mtd634()
        {
        }

        internal static void mtd22(int var0, PropDoc var1, ref StreamWriter var2)
        {
            var2.Write("<span style=" + '"');
            mtd627.mtd22(var0, var1, ref var2);
            if (var1.BackColor != Color.Transparent)
            {
                mtd624.mtd22(var1.BackColor, ref var2);
            }
            if (var1.Border != null)
            {
                mtd625.mtd22(var1.Border, ref var2);
            }
            mtd626.mtd22(var1, ref var2);
            if (var1.Text != null)
            {
                var2.WriteLine('"' + string.Format(">{0}</span>", var1.Text));
            }
            else
            {
                var2.WriteLine('"' + "></span>");
            }
        }
    }
}

