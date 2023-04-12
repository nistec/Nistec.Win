namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.IO;

    internal class mtd632
    {
        private ImageSource var0;

        internal mtd632(ImageSource var1)
        {
            this.var0 = var1;
        }

        internal void mtd22(int var2, PropDoc var3, ref StreamWriter var4)
        {
            var4.Write("<span style=" + '"');
            mtd627.mtd22(var2, var3, ref var4);
            if (var3.BackColor != Color.Transparent)
            {
                mtd624.mtd22(var3.BackColor, ref var4);
            }
            if (var3.Border != null)
            {
                mtd625.mtd22(var3.Border, ref var4);
            }
            var4.Write('"' + ">");
            Image image = var3.Image;
            if (image != null)
            {
                float num = ((float) image.Width) / image.HorizontalResolution;
                float num2 = ((float) image.Height) / image.VerticalResolution;
                var4.Write("<span style=" + '"');
                var4.Write(string.Format("position: absolute; top: in; left: 0in; width: {0}in; height: {1}in", mtd620.mtd621(num), mtd620.mtd621(num2)));
                var4.Write('"' + ">");
                string str = string.Format("border: 0; align: center; width: {0}in; height: {1}in", mtd620.mtd621(num), mtd620.mtd621(num2));
                string source = this.var0.GetSource(var3.Image);
                var4.WriteLine(string.Format("<img style={0} src={1}></img>", "'" + str + "'", "'" + source + "'"));
                var4.Write("</span>");
            }
            var4.WriteLine("</span>");
        }
    }
}

