namespace Nistec.Charts
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Xml;

    internal class BitmapWriter //WMS
    {
        internal static Bitmap Download(string XMLFile, int width, int height, decimal lat1, decimal lat2, decimal lon1, decimal lon2)
        {
            Bitmap bitmap;
            XmlDocument document = new XmlDocument();
            document.Load(XMLFile);
            string requestUriString = document.DocumentElement.FirstChild.InnerText.Replace("{lat1}", lat1.ToString()).Replace("{lat2}", lat2.ToString()).Replace("{lon1}", lon1.ToString()).Replace("{lon2}", lon2.ToString()).Replace("{width}", width.ToString()).Replace("{height}", height.ToString());
            try
            {
                bitmap = (Bitmap) Image.FromStream(WebRequest.Create(requestUriString).GetResponse().GetResponseStream());
            }
            catch (Exception exception)
            {
                throw new ChartException(exception);
            }
            return bitmap;
        }

        internal static Bitmap WinDownload(string XMLText, int width, int height, decimal lat1, decimal lat2, decimal lon1, decimal lon2)
        {
            Bitmap bitmap;
            XmlDocument document = new XmlDocument();
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(XMLText);
            document.Load(stream);
            writer.Dispose();
            stream.Dispose();
            string requestUriString = document.DocumentElement.FirstChild.InnerText.Replace("{lat1}", lat1.ToString()).Replace("{lat2}", lat2.ToString()).Replace("{lon1}", lon1.ToString()).Replace("{lon2}", lon2.ToString()).Replace("{width}", width.ToString()).Replace("{height}", height.ToString());
            try
            {
                bitmap = (Bitmap) Image.FromStream(WebRequest.Create(requestUriString).GetResponse().GetResponseStream());
            }
            catch (Exception exception)
            {
                throw new ChartException(exception);
            }
            return bitmap;
        }
    }
}

