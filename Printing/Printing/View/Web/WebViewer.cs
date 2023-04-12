using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using Nistec.Printing.View.Templates;
using Nistec.Printing.View.Pdf;
using Nistec.Printing.View.Html;
using Nistec.Printing.View.Img;
using Nistec.Printing.View;

namespace Nistec.Printing.View.Web
{
    public class WebViewer //: WebControl, INamingContainer, IPostBackDataHandler, IPostBackEventHandler
    {

 /*
        private TimeSpan _var11;
        private TimeSpan _var12;

        private int _var14;
        private int _var15;
        private string _var7;

        public WebViewer() : base(HtmlTextWriterTag.Div)
        {
            this._var11 = TimeSpan.FromMinutes(20.0);
            this._var12 = TimeSpan.FromSeconds(10.0);
        }

        public void ClearReportCache()
        {
            this._var7 = "";
            this._var14 = 1;
            this._var15 = 1;
        }

         protected override void OnLoad(EventArgs e)
        {
            this.Page.RegisterRequiresPostBack(this);
            //if (this.var22 || !this.var33())
            //{
            //    base.OnLoad(e);
            //}
            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.AddToCache();
            this.var24();
            base.OnPreRender(e);
        }

        public void RaisePostBackEvent(string args)
        {
   
        }

        public void RaisePostDataChangedEvent()
        {
        }

        private void AddToCache()
        {
            if (!this.GetCacheReport().Contains(this._var7))
            {
                this.mtd1105 = 1;
                this._var15 = 1;
                if (this._var6 != null)
                {
                    this._var7 = this.GetCacheReport().Add(this._var6);
                    try
                    {
                        this._var6.Generate();
                    }
                    catch
                    {
                        this._var2.InnerHtml = "Report execution failed.";
                    }
                    this._var15 = (int)this._var6.MaxPages;
                }
                if (this._var15 < 1)
                {
                    this._var15 = 1;
                }
            }
        }


        private McCacheReport GetCacheReport()
        {
            McCacheReport mtd = null;
            try
            {
                mtd = (McCacheReport)this.Context.Application[McCacheReport.mtd1111];
            }
            catch
            {
            }
            if (mtd == null)
            {
                McCacheReport.mtd1125(this.Context, this._var11, this._var12);
                try
                {
                    mtd = (McCacheReport)this.Context.Application[McCacheReport.mtd1111];
                }
                catch
                {
                }
            }
            if (mtd == null)
            {
                throw new ApplicationException("CouldNotLoadReportSource");
            }
            return mtd;
        }
*/

       
        public void ExHtml(HttpResponse response, Report report)//string var41)
        {
            //string[] strArray = var41.Split(new char[] { ':' });
            //Report report = null;
            //if (strArray.Length == 2)
            //{
            //    report = this.GetCacheReport().GetReport(strArray[0]);
            //}
            if ((report != null) && (report.MaxPages > 0L))
            {
                string name = report.GetType().Name;
                MemoryStream stream = new MemoryStream();
                try
                {
                    new HtmlWriter().RenderHtmlDocument(stream, report.Document, name, 1, (int)report.MaxPages);
                    stream.Flush();
                }
                catch
                {
                }
                this.RenderRequest(response, stream, "message/rfc822", name, "mht");
            }
        }


        public void ExPdf(HttpResponse response, Report report, PDFVersion version)//string var41)
        {
            //string[] strArray = var41.Split(new char[] { ':' });
            //Report report = null;
            //if (strArray.Length == 2)
            //{
            //    report = this.GetCacheReport().GetReport(strArray[0]);
            //}
            if ((report != null) && (report.MaxPages > 0L))
            {
                MemoryStream stream = new MemoryStream();
                PDFDocument document = new PDFDocument();
                document.EmbedFont = true;
                document.Version = version;
                try
                {
                    document.Export(report.Document, stream);
                    stream.Flush();
                }
                catch
                {
                }
                this.RenderRequest(response, stream, "application/pdf", report.GetType().Name, "pdf");
            }
        }

        private void RenderRequest(HttpResponse response, MemoryStream ms, string contentType, string filename, string ext)
        {
            if ((ms != null) && (ms.Length > 0L))
            {
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.Expires = 0;
                response.Cache.SetCacheability(HttpCacheability.Private);
                response.ContentType = contentType;
                response.AddHeader("Content-Type", response.ContentType);
                response.AddHeader("Content-Disposition", string.Format("inline;filename={0}.{1}", filename, ext));
                ms.Seek(0L, SeekOrigin.Begin);
                response.BinaryWrite(ms.ToArray());
                ms.Close();
                CompleteRequest(response);
            }
        }

        private static void CompleteRequest(HttpResponse response)
        {
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            try
            {
                response.End();
            }
            catch
            {
            }
        }
    }

  
}
