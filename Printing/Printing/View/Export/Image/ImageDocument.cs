namespace Nistec.Printing.View.Img
{
    using Nistec.Printing.View;
    using Nistec.Printing.View.Html;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class ImageDocument
    {
        private Document _Document;
        private float _pageWidth;
        private float _pageHeight;
        private float _totalHeight;
        private ImageFormat _ImageFormat;

        public void Export(Stream stream, ImageFormat format, Document document, int fromPageNo, int toPageNo)
        {
            int num = (toPageNo - fromPageNo) + 1;
            this._Document = document;
            this._ImageFormat = GetValidFormat(format);
            this._pageWidth = this._Document.PageWidth * ReportUtil.Dpi;
            this._pageHeight = this._Document.PageHeight * ReportUtil.Dpi;
            this._totalHeight = this._pageHeight * num;
            if (this.IsPrintable(format))
            {
                this.Print(stream, fromPageNo, toPageNo);
            }
            else
            {
                this.Save(stream, fromPageNo, toPageNo);
            }
        }

        public static ImageFormat GetValidFormat(ImageFormat format)
        {
            if (((format != ImageFormat.Exif) && (format != ImageFormat.MemoryBmp)) && (format != ImageFormat.Icon))
            {
                return format;
            }
            return ImageFormat.Png;
        }

        private static Metafile GetMetafile(Stream stream, EmfType emfType)
        {
            Metafile metafile;
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                IntPtr hdc = graphics.GetHdc();
                metafile = new Metafile(stream, hdc, emfType);
                graphics.ReleaseHdc(hdc);
            }
            return metafile;
        }

        private static Graphics CreateGraphics(Image image)
        {
            Graphics graphics = Graphics.FromImage(image);
            graphics.PageUnit = GraphicsUnit.Display;
            return graphics;
        }

        private void Print(Graphics g, int pageFrom, int pageTo)
        {
            g.FillRectangle(SystemBrushes.Window, 0f, 0f, this._pageWidth, this._totalHeight);
            float top = 0f;
            for (int i = pageFrom - 1; i < pageTo; i++)
            {
                this._Document.PrintPage(g, i, top);
                top += this._pageHeight;
            }
        }
        //var5
        private bool IsPrintable(ImageFormat imageFormat)
        {
            if (imageFormat != ImageFormat.Emf)
            {
                return (imageFormat == ImageFormat.Wmf);
            }
            return true;
        }

        private void Print(Stream stream, int pageFrom, int pageTo)
        {
            Image image = GetMetafile(stream, EmfType.EmfOnly);
            Graphics g = CreateGraphics(image);
            try
            {
                this.Print(g, pageFrom, pageTo);
            }
            finally
            {
                image.Dispose();
                g.Dispose();
            }
        }

        //var7
        private void Save(Stream stream, int pageFrom, int pageTo)
        {
            int width = (int) Math.Ceiling((double) this._pageWidth);
            int height = (int) Math.Ceiling((double) this._totalHeight);
            Image image = new Bitmap(width, height);
            Graphics g = CreateGraphics(image);
            this.Print(g, pageFrom, pageTo);
            try
            {
                ImageSource.Save(stream, image, this._ImageFormat);
            }
            finally
            {
                image.Dispose();
                g.Dispose();
            }
        }
    }
}

