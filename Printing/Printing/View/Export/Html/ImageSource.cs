namespace Nistec.Printing.View.Html
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public abstract class ImageSource
    {
        protected ImageSource()
        {
        }

        internal static ImageFormat SetImageFormat(Image image)
        {
            ImageFormat rawFormat = image.RawFormat;
            if (image is Metafile)
            {
                return ImageFormat.Emf;
            }
            if (((rawFormat != ImageFormat.Bmp) && (rawFormat != ImageFormat.Gif)) && ((rawFormat != ImageFormat.Jpeg) && (rawFormat != ImageFormat.Tiff)))
            {
                return ImageFormat.Png;
            }
            return rawFormat;
        }
        //mtd636
        internal static void SetImageFormat(Image image, out ImageFormat imageFormat, out string ext)
        {
            ImageFormat rawFormat = image.RawFormat;
            if (image is Metafile)
            {
                imageFormat = ImageFormat.Emf;
                ext = ".emf";
            }
            else if (rawFormat == ImageFormat.Bmp)
            {
                imageFormat = ImageFormat.Bmp;
                ext = ".bmp";
            }
            else if (rawFormat == ImageFormat.Gif)
            {
                imageFormat = ImageFormat.Gif;
                ext = ".gif";
            }
            else if (rawFormat == ImageFormat.Jpeg)
            {
                imageFormat = ImageFormat.Jpeg;
                ext = ".jpg";
            }
            else if (rawFormat == ImageFormat.Tiff)
            {
                imageFormat = ImageFormat.Tiff;
                ext = ".tif";
            }
            else
            {
                imageFormat = ImageFormat.Png;
                ext = ".png";
            }
        }
        //mtd637
        internal static void Save(Stream stream, Image image, ImageFormat imageFormat)
        {
            ImageCodecInfo encoder = GetEncoder(imageFormat);
            if (encoder == null)
            {
                encoder = GetEncoder(ImageFormat.Png);
            }
            try
            {
                image.Save(stream, encoder, null);
            }
            catch
            {
            }
        }

        public abstract string GetSource(Image image);
        public static byte[] SaveToArray(Image image)
        {
            byte[] buffer = new byte[0];
            MemoryStream stream = new MemoryStream();
            try
            {
                Save(stream, image);
                buffer = stream.ToArray();
            }
            finally
            {
                stream.Close();
            }
            return buffer;
        }

        public static void SaveToFile(Image image, ImageFormat format, string filename)
        {
            image.Save(filename, format);
        }

        private static void Save(Stream stream, Image image)
        {
            Save(stream, image, SetImageFormat(image));
        }
        //var4
        private static ImageCodecInfo GetEncoder(ImageFormat imageFormat)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                if (imageEncoders[i].FormatID.Equals(imageFormat.Guid))
                {
                    return imageEncoders[i];
                }
            }
            return null;
        }
    }
}

