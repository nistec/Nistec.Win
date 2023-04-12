namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    internal class mtd741
    {
        internal mtd741()
        {
        }

        internal static mtd742 mtd743(Image var0)
        {
            if (var0 != null)
            {
                MemoryStream stream = new MemoryStream();
                new Bitmap(var0).Save(stream, ImageFormat.Jpeg);
                return new mtd742(stream.GetBuffer(), (int) stream.Length);
            }
            return null;
        }

        internal static mtd742 mtd744(Image var0)
        {
            Bitmap bitmap;
            int num;
            if (var0 == null)
            {
                return null;
            }
            mtd742 mtd = new mtd742();
            if (var0 is Bitmap)
            {
                bitmap = (Bitmap) var0;
            }
            else
            {
                if (var0 is Metafile)
                {
                    bitmap = new Bitmap(var0);
                    bitmap = new Bitmap(var0.Width, var0.Height);
                    bitmap.SetResolution(var0.HorizontalResolution, var0.VerticalResolution);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
                    try
                    {
                        graphics.DrawImage(var0, 0, 0);
                        goto Label_008E;
                    }
                    finally
                    {
                        graphics.Dispose();
                    }
                }
                bitmap = new Bitmap(var0);
            }
        Label_008E:
            num = 0;
            while (num < bitmap.Height)
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    Color pixel = bitmap.GetPixel(i, num);
                    mtd.mtd710(pixel.R);
                    mtd.mtd710(pixel.G);
                    mtd.mtd710(pixel.B);
                }
                num++;
            }
            mtd.mtd745();
            return mtd;
        }
    }
}

