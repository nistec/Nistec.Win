namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.IO;

    internal class mtd630
    {
        private ImageSource var0;

        internal mtd630(ImageSource var1)
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
                RectangleF ef = var5(var3, num, num2);
                var4.Write("<span style=" + '"');
                var4.Write(string.Format("position: absolute; top: {0}in; left: {1}in; width: {2}in; height: {3}in", new object[] { mtd620.mtd621(ef.Top), mtd620.mtd621(ef.Left), mtd620.mtd621(ef.Width), mtd620.mtd621(ef.Height) }));
                var4.Write('"' + ">");
                string str = string.Format("border: 0; align: center; width: {0}in; height: {1}in", mtd620.mtd621(ef.Width), mtd620.mtd621(ef.Height));
                string source = this.var0.GetSource(var3.Image);
                var4.WriteLine(string.Format("<img style={0} src={1}></img>", "'" + str + "'", "'" + source + "'"));
                var4.Write("</span>");
            }
            var4.WriteLine("</span>");
        }

        private static RectangleF var5(PropDoc var3, float var6, float var7)
        {
            RectangleF ef = new RectangleF(0f, 0f, var6, var7);
            float single1 = var3.Left;
            float single2 = var3.Top;
            float num = var3.Width;
            float num2 = var3.Height;
            SizeMode mode = var3.SizeMode;
            PictureAlignment alignment = var3.PictureAlignment;
            switch (mode)
            {
                case SizeMode.Clip:
                    switch (alignment)
                    {
                        case PictureAlignment.TopLeft:
                            ef.X = 0f;
                            ef.Y = 0f;
                            return ef;

                        case PictureAlignment.TopRight:
                            ef.Y = 0f;
                            ef.X = num - ef.Width;
                            return ef;

                        case PictureAlignment.Center:
                            ef.Y = (num2 - ef.Height) / 2f;
                            ef.X = (num - ef.Width) / 2f;
                            return ef;

                        case PictureAlignment.BottomLeft:
                            ef.X = 0f;
                            ef.Y = num2 - ef.Height;
                            return ef;
                    }
                    ef.X = num - ef.Width;
                    ef.Y = num2 - ef.Height;
                    return ef;

                case SizeMode.Stretch:
                    ef.X = 0f;
                    ef.Y = 0f;
                    ef.Width = num;
                    ef.Height = num2;
                    return ef;
            }
            if (mode == SizeMode.Zoom)
            {
                ef.Height = num2;
                ef.Width = (num2 * var6) / var7;
                if (ef.Width > num)
                {
                    ef.Width = num;
                    ef.Height = (num * var7) / var6;
                    ef.X = 0f;
                    ef.Y = (num2 - ef.Height) / 2f;
                    return ef;
                }
                ef.X = (num - ef.Width) / 2f;
                ef.Y = 0f;
            }
            return ef;
        }
    }
}

