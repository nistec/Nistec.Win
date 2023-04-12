using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace Nistec.Printing.View.Web
{
    internal class ImageWriter//mtd1116
    {

        internal ImageWriter()
        {
        }

        internal static string mtd1117(Image var0)
        {
            ImageFormat rawFormat = var0.RawFormat;
            if (var0 is Metafile)
            {
                return "emf";
            }
            if (rawFormat == ImageFormat.Bmp)
            {
                return "bmp";
            }
            if (rawFormat == ImageFormat.Gif)
            {
                return "gif";
            }
            if (rawFormat == ImageFormat.Jpeg)
            {
                return "jpg";
            }
            if (rawFormat == ImageFormat.Tiff)
            {
                return "tif";
            }
            return "png";
        }

        internal static string mtd1131(Image var0, int var2)
        {
            return string.Format("img[{0}{1}{2}{3}{4}{5}{6}]", new object[] { mtd1117(var0), var0.Width, var0.Height, var0.HorizontalResolution, var0.VerticalResolution, var0.PixelFormat, var2 });
        }

        internal static string mtd1133(HttpRequest var1)
        {
            string[] strArray = var1.CurrentExecutionFilePath.Split(new char[] { '/' });
            return strArray[strArray.Length - 1];
        }
    }
    internal class mtd1129
    {
        private Image _var0;
        private string _var1;
        private byte[] _var2;

        internal mtd1129(Image var0, byte[] var2, string var1)
        {
            this._var0 = var0;
            this._var2 = var2;
            this._var1 = var1;
        }

        internal string mtd281
        {
            get
            {
                return this._var1;
            }
        }

        internal byte[] mtd784
        {
            get
            {
                return this._var2;
            }
        }

        internal Image mtd9
        {
            get
            {
                return this._var0;
            }
        }
    }

}
