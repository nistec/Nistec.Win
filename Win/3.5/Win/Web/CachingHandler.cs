using System;
using System.Web;
using MControl.Web.Configuration;
using System.Drawing;
using System.IO;

namespace MControl.Web
{
	/// <summary>
	/// Handler for sending files to the client
	/// </summary>
	public class CachingHandler : IHttpHandler
	{
		public bool IsReusable
		{
			get { return true; }
		}

		public void ProcessRequest(HttpContext context)
		{
			string file = context.Server.MapPath(context.Request.FilePath.Replace(".ashx", ""));
			string filename = file.Substring(file.LastIndexOf('\\') + 1);
			string extension = file.Substring(file.LastIndexOf('.') + 1);

            CachingSection config = (CachingSection)context.GetSection("MControl/Caching");
			if (config != null)
			{
				context.Response.Cache.SetExpires(DateTime.Now.Add(config.CachingTimeSpan));
				context.Response.Cache.SetCacheability(HttpCacheability.Public);
				context.Response.Cache.SetValidUntilExpires(false);
				
				FileExtension fileExtension = config.FileExtensions[extension];
				if (fileExtension != null)
				{
					context.Response.ContentType = fileExtension.ContentType;
				}
			}

			context.Response.AddHeader("content-disposition", "inline; filename=" + filename);
			context.Response.WriteFile(file);
		}
	}


    //public class CachingGraphicsHandler : IHttpHandler
    //    {
    //        public bool IsReusable
    //        {
    //            get { return true; }
    //        }

    //        public void ProcessRequest(HttpContext context)
    //        {
    //           Bitmap bitmap = new Bitmap(220, 50);
    //           Graphics graphics = Graphics.FromImage(bitmap);

    //            graphics.DrawString(
    //              DateTime.Now.ToString(),
    //              new Font("Verdana", 14),
    //              new SolidBrush(Color.Blue),
    //              new PointF(0, 0));

    //            graphics.DrawRectangle(
    //              new Pen(new SolidBrush(Color.Green)),
    //              new Rectangle(0, 0, 219, 49));

    //            MemoryStream stream = new MemoryStream();
    //            bitmap.Save(stream, ImageFormat.Png);
    //            graphics.Dispose();

    //            byte[] image = new byte[stream.Length];
    //            stream.Position = 0;
    //            stream.Read(image, 0, (int)stream.Length);
    //            stream.Close();

    //            context.Response.Cache.SetExpires(DateTime.Now.AddMinutes(1));
    //            context.Response.Cache.SetCacheability(HttpCacheability.Public);
    //            context.Response.Cache.SetValidUntilExpires(false);

    //            context.Response.AddHeader("content-disposition",
    //              "inline; filename=DateTime.png");
    //            context.Response.BinaryWrite(image);
    //        }
    //    }
  

}
