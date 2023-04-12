using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using Nistec.Printing.View.Templates;
using Nistec.Printing.View.Pdf;
using Nistec.Printing.View.Html;
using Nistec.Printing.View.Img;
using Nistec.Printing.View.Viewer;
using System.IO;
//using Nistec.Printing.View.Design;

namespace Nistec.Printing.View
{

    public enum InvoiceType
    {
        InvoiceEN,
        InvoiceIL
    }
    public interface IInvoice
    {
        void AddTitle(string title, string subTitle);
        void AddHeader(string customerId, string customrName, string address, string date, string details);
        void AddFooter(string footer, float vat);
        InvoiceType InvoiceType { get; }
        Nistec.Printing.View.Design.ReportDesign IReport { get; }

    }

	/// <summary>
	/// Summary description for InvoiceBuilder.
	/// </summary>
    /// <example>
    ///  private void CreateInvoice()
    ///     {
    ///         InvoiceBuilder builder = new InvoiceBuilder(Nistec.Printing.View.InvoiceType.InvoiceEN);
    ///         builder.AddTitle("Control.net", "Software");
    ///         builder.AddHeader("1234", "Abc interactive", "LA", DateTime.Now.ToString(), "Billing 3-2010");
    ///         builder.AddFooter("Control.net", 0.16f);
    ///         builder.AddRow("123", "Item blue", 1m, 20.50m);
    ///         builder.AddRow("456", "Item Green", 1m, 70.50m);
    ///         builder.AddRow("789", "Item Yellow", 1m, 86.52m);
    ///         builder.Generate();
    ///         builder.Preview();
    ///     }
    /// </example>
	public class InvoiceBuilder
    {
        #region static


        public static object GetDefaultProp(ExportType exType)
        {
            switch (exType)
            {
                case ExportType.Pdf:
                    return PDFprop.Default();
                case ExportType.Html:
                    return Htmlprop.Default();
                case ExportType.Image:
                    return TIFprop.Default();
                default:
                    throw new ArgumentException("ExportType not supported");
            }
        }

        #endregion

        #region ctor
        private DataTable dt;
        private IInvoice report;
		private bool rptCreated;
        //private PageOrientation pageOrientation;
        //private bool rightToLeft;

        public InvoiceBuilder(InvoiceType type)
        {
            switch (type)
            {
                case InvoiceType.InvoiceIL:
                    report = new InvoiceIL(); break;
                default:
                    report = new InvoiceEN(); break;
            }
            IReport.DefaultPaperSize = false;
            IReport.PaperKind = System.Drawing.Printing.PaperKind.A4;

            rptCreated = false;
        }

        public Nistec.Printing.View.Design.ReportDesign IReport
        {
            get { return report.IReport; }
        }

        public void AddTitle(string title, string subTitle)
        {
            report.AddTitle(title, subTitle);
        }
        public void AddHeader(string customerId, string customrName, string address, string date, string details)
        {
            report.AddHeader(customerId, customrName, address, date, details);
        }
        public void AddFooter(string footer, float vat)
        {
            report.AddFooter(footer, vat);
        }


        #endregion

        #region private methods

        private float GetPageWidth()
        {
            float pageWidth = IReport.ReportWidth;

            string unit = ReportUtil.Unit;
            System.Drawing.SizeF size = ReportUtil.GetPaperSize(IReport.PaperKind.ToString(), ref unit);
            if (size != System.Drawing.SizeF.Empty)
            {
                //if(pageOrientation==PageOrientation.Landscape)
                //    pageWidth = ReportUtil.ConvertToPoint(size.Height, unit);
                //else
                pageWidth = ReportUtil.ConvertToPoint(size.Width, unit);
            }
            return pageWidth;
        }

        //private void InitReport()
        //{

        //    //report.Sections["ReportDetail"].Controls.Clear();
        //    //report.Sections["GroupHeader"].Visible = true;

        //    IReport.DefaultPaperSize = false;
        //    IReport.PaperKind = System.Drawing.Printing.PaperKind.A4;
        //    //report.Orientation=pageOrientation;
        //}


		#endregion

		#region Report Template

        public void Preview()
        {
            if (!rptCreated)
            {
                throw new Exception("Report not created");
            }
            ReportViewer.Preview(IReport);

        }
 
        public void Export(object prop, ExportType exType, string filepath, bool openAfter)
        {
            if (!rptCreated)
            {
                throw new Exception("Report not created");
            }
            Document doc = IReport.Document;
            
            if (doc == null)
            {
                throw new Exception("Invalid Document");
            }
            if (exType == ExportType.Pdf)
            {
                PDFprop cls1 = (PDFprop)prop;
                PDFDocument document1 = new PDFDocument();
                document1.Title = cls1.Title;
                document1.Author = cls1.Author;
                document1.Creator = cls1.Creator;
                document1.Compress = cls1.Compress;
                document1.EmbedFont = cls1.EmbedFont;
                
                if (cls1.Encrypt)
                {
                    SecurityManager manager1 = new SecurityManager();
                    manager1.OwnerPassword = cls1.OwnerPassword;
                    manager1.UserPassword = cls1.UserPassword;
                    manager1.Encryption = cls1.Encryption;
                    document1.SecurityManager = manager1;
                }

                document1.Export(doc, filepath);
            }
            else if (exType == ExportType.Html)
            {
                Htmlprop cls2 = (Htmlprop)prop;
                HTMLDocument document2 = new HTMLDocument();
                document2.Title = cls2.Title;
                document2.CharSet = cls2.CharSet;
                document2.Export(filepath, doc, 1, (int)doc.MaxPages, cls2.IsMultiPage);
            }
            else if (exType == ExportType.Image)
            {
                TIFprop cls3 = (TIFprop)prop;
                ImageDocument document3 = new ImageDocument();
                FileStream stream1 = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
                try
                {
                    document3.Export(stream1, TIFprop.cls1083(cls3.Format), doc, 1, (int)doc.MaxPages);
                }
                finally
                {
                    stream1.Flush();
                    stream1.Close();
                }
            }
            else if (exType == ExportType.Excel)
            {
                CSVprop cls3 = (CSVprop)prop;
                Nistec.Printing.ExcelXml.Workbook.Export((DataTable)doc.Report.DataSource, filepath, cls3.FirstRowHeader, null);
            }
            else if (exType == ExportType.Csv)
            {
                CSVprop cls3 = (CSVprop)prop;
                    Nistec.Printing.Csv.CsvWriter.Export((DataTable)doc.Report.DataSource, filepath, cls3.FirstRowHeader);
            }
            if (openAfter)
            {
                System.Diagnostics.Process.Start(filepath);
                //System.IO.FileStream fs= System.IO.File.Open(export1.filePath,System.IO.FileMode.Open);

            }
        }


        public  IInvoice Generate()
        {
            IReport.DataSource = InvoiceRows;
            IReport.Generate();
            rptCreated = true;
            return report;
        }

        //public void CreateInvoice()
        //{

        //    IReport.DataSource = InvoiceRows;
        //    //ReportViewer _frmviewer = new ReportViewer();
        //    //_frmviewer.printViewer.Document = _report.Document;
        //    //_frmviewer.ShowDialog();

        //}
       
 

		#endregion

        public  DataTable InvoiceSchema()
        {
            DataTable dt = new DataTable("Invoice");
            dt.Columns.Add("ProductID");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("ExtendedPrice", typeof(decimal));
            return dt.Clone();
        }

        private DataTable InvoiceRows
        {
            get
            {
                if (dt == null)
                {
                    dt = InvoiceSchema();
                }
                return dt;
            }
        }

        public void AddRow(string productId, string productName, decimal quantity, decimal price)
        {
            InvoiceRows.Rows.Add(productId, productName, quantity, price, price * quantity);
        }
        public void AddRows(params object [] ItemsArray)
        {
            InvoiceRows.Rows.Add(ItemsArray);
        }
        public void AddRows(DataTable dt)
        {
            InvoiceRows.Rows.Add(dt.Copy().Select());
        }

	}
}
