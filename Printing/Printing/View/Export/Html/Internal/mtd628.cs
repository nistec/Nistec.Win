namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    internal class mtd628
    {
        private Image _var0;
        private Image _var1;
        internal ImageSource mtd629;

        internal mtd628(ImageSource var2)
        {
            this.mtd629 = var2;
            this._var0 = var3(true);
            this._var1 = var3(false);
        }

        internal void mtd22(int var4, PropDoc var5, ref StreamWriter var6)
        {
            string source;
            RectangleF ef = var5.mtd133;
            RectangleF ef2 = var5.mtd134;
            var6.Write("<span style=" + '"');
            mtd627.mtd22(var4, var5, ref var6);
            if (var5.BackColor != Color.Transparent)
            {
                mtd624.mtd22(var5.BackColor, ref var6);
            }
            mtd626.mtd22(var5, ref var6);
            if (var5.Border != null)
            {
                mtd625.mtd22(var5.Border, ref var6);
            }
            var6.WriteLine('"' + ">");
            var6.Write("<span style=" + '"');
            mtd627.mtd22(var4, ef.Left, ef.Top, ef.Width, ef.Height, var5.Visible, ref var6);
            var6.WriteLine('"' + ">");
            if (var5.IsChecked)
            {
                source = this.mtd629.GetSource(this._var0);
            }
            else
            {
                source = this.mtd629.GetSource(this._var1);
            }
            var6.WriteLine(string.Format("<img src={0}></img></span>", "'" + source + "'"));
            var6.Write("<span style=" + '"');
            mtd627.mtd22(var4, ef2.Left, ef2.Top, ef2.Width, ef2.Height, var5.Visible, ref var6);
            if (var5.Text != null)
            {
                var6.WriteLine('"' + string.Format(">{0}</span>", var5.Text));
            }
            else
            {
                var6.WriteLine('"' + "></span>");
            }
            var6.WriteLine("</span>");
        }

        private static Image var3(bool var7)
        {
            Bitmap image = new Bitmap(12, 12, PixelFormat.Format24bppRgb);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(SystemBrushes.Window, 0, 0, 12, 12);
            graphics.DrawRectangle(SystemPens.WindowFrame, 0, 0, 11, 11);
            graphics.Dispose();
            if (var7)
            {
                image.SetPixel(8, 2, Color.Black);
                image.SetPixel(9, 2, Color.Black);
                image.SetPixel(8, 3, Color.Black);
                image.SetPixel(9, 3, Color.Black);
                image.SetPixel(7, 4, Color.Black);
                image.SetPixel(8, 4, Color.Black);
                image.SetPixel(7, 5, Color.Black);
                image.SetPixel(8, 5, Color.Black);
                image.SetPixel(3, 6, Color.Black);
                image.SetPixel(4, 6, Color.Black);
                image.SetPixel(6, 6, Color.Black);
                image.SetPixel(7, 6, Color.Black);
                image.SetPixel(3, 7, Color.Black);
                image.SetPixel(4, 7, Color.Black);
                image.SetPixel(6, 7, Color.Black);
                image.SetPixel(7, 7, Color.Black);
                image.SetPixel(4, 8, Color.Black);
                image.SetPixel(5, 8, Color.Black);
                image.SetPixel(6, 8, Color.Black);
                image.SetPixel(4, 9, Color.Black);
                image.SetPixel(5, 9, Color.Black);
                image.SetPixel(6, 9, Color.Black);
                image.SetPixel(5, 10, Color.Black);
            }
            return image;
        }
    }
}

