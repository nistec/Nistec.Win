namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;

    internal class mtd73
    {
        //public static void mtd62(ref Bitmap var6)
        //{
        //    var6 = new Bitmap(12, 12, PixelFormat.Format24bppRgb);
        //    var6.MakeTransparent();
        //    var6.SetPixel(8, 2, Color.Black);
        //    var6.SetPixel(9, 2, Color.Black);
        //    var6.SetPixel(8, 3, Color.Black);
        //    var6.SetPixel(9, 3, Color.Black);
        //    var6.SetPixel(7, 4, Color.Black);
        //    var6.SetPixel(8, 4, Color.Black);
        //    var6.SetPixel(7, 5, Color.Black);
        //    var6.SetPixel(8, 5, Color.Black);
        //    var6.SetPixel(3, 6, Color.Black);
        //    var6.SetPixel(4, 6, Color.Black);
        //    var6.SetPixel(6, 6, Color.Black);
        //    var6.SetPixel(7, 6, Color.Black);
        //    var6.SetPixel(3, 7, Color.Black);
        //    var6.SetPixel(4, 7, Color.Black);
        //    var6.SetPixel(6, 7, Color.Black);
        //    var6.SetPixel(7, 7, Color.Black);
        //    var6.SetPixel(4, 8, Color.Black);
        //    var6.SetPixel(5, 8, Color.Black);
        //    var6.SetPixel(6, 8, Color.Black);
        //    var6.SetPixel(4, 9, Color.Black);
        //    var6.SetPixel(5, 9, Color.Black);
        //    var6.SetPixel(6, 9, Color.Black);
        //    var6.SetPixel(5, 10, Color.Black);
        //}

        public static string mtd74(string var4)
        {
            return Encoding.Unicode.GetString(Convert.FromBase64String(var4));
        }

        public static string mtd75(string var5)
        {
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(var5));
        }
        //mtd81
        public static ImageList GetImageList(Assembly var0, string var1, Size var2, Point var3)
        {
            ImageList list = new ImageList();
            list.ImageSize = var2;
            Bitmap bitmap = new Bitmap(var0.GetManifestResourceStream(var1));
            Color pixel = bitmap.GetPixel(var3.X, var3.Y);
            bitmap.MakeTransparent(pixel);
            bitmap.MakeTransparent();
            list.Images.AddStrip(bitmap);
            return list;
        }
        //mtd82
        public static Bitmap[] GetControlsBitmap(Assembly var0)
        {
            return new Bitmap[] { new Bitmap(var0.GetManifestResourceStream("Nistec.Printing.View.Resources.Label.bmp")), new Bitmap(var0.GetManifestResourceStream("Nistec.Printing.View.Resources.TextBox.bmp")), new Bitmap(var0.GetManifestResourceStream("Nistec.Printing.View.Resources.CheckBox.bmp")), new Bitmap(var0.GetManifestResourceStream("Nistec.Printing.View.Resources.Picture.bmp")), new Bitmap(var0.GetManifestResourceStream("Nistec.Printing.View.Resources.Shape.bmp")), new Bitmap(var0.GetManifestResourceStream("Nistec.Printing.View.Resources.Line.bmp")), new Bitmap(var0.GetManifestResourceStream("Nistec.Printing.View.Resources.RichText.bmp")), new Bitmap(var0.GetManifestResourceStream("Nistec.Printing.View.Resources.SubReport.bmp")), new Bitmap(var0.GetManifestResourceStream("Nistec.Printing.View.Resources.PageBreak.bmp")) };
        }
    }
}

