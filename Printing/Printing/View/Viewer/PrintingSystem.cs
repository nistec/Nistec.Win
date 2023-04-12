using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.IO;

using MControl.ReportView;
using MControl.ReportView.ExportHTML;
using MControl.ReportView.ExportImage;
using MControl.ReportView.ExportPDF;

namespace MControl.ReportView.Viewer
{
	public class PrintingSystem
	{

		// Fields
        internal Report _RptReport;//cls176;

		public PrintingSystem()
		{
			this._RptReport = null;
		}

 
		public void ExportToHTML(FileStream filestream, string title)
		{
			if ((this._RptReport != null) && (this._RptReport.MaxPages > 0))
			{
				HTMLDocument document1 = new HTMLDocument();
				document1.Title = title;
				document1.Export(filestream, this._RptReport.Document);
			}
		}

		public void ExportToHTML(FileStream filestream, string title, HtmlCharacterSet charset)
		{
			if ((this._RptReport != null) && (this._RptReport.MaxPages > 0))
			{
				HTMLDocument document1 = new HTMLDocument();
				document1.Title = title;
				document1.CharSet = charset;
				document1.Export(filestream, this._RptReport.Document);
			}
		}

 
		public void ExportToHTML(string filename, string title, bool multipage)
		{
			if ((this._RptReport != null) && (this._RptReport.MaxPages > 0))
			{
				HTMLDocument document1 = new HTMLDocument();
				document1.Title = title;
				document1.Export(filename, this._RptReport.Document, multipage);
			}
		}

		public void ExportToHTML(string filename, string title, HtmlCharacterSet charset, bool multipage)
		{
			if ((this._RptReport != null) && (this._RptReport.MaxPages > 0))
			{
				HTMLDocument document1 = new HTMLDocument();
				document1.Title = title;
				document1.CharSet = charset;
				document1.Export(filename, this._RptReport.Document, multipage);
			}
		}

		public void ExportToImage(Stream stream, ImageType format)
		{
			ImageDocument document1 = new ImageDocument();
			Document document2 = this._RptReport.Document;
			try
			{
				document1.Export(stream, TIFprop.cls1083(format), document2, 1, (int) document2.MaxPages);
			}
			catch
			{
			}
		}

		public void ExportToImage(string filename, ImageType format)
		{
			ImageDocument document1 = new ImageDocument();
			Document document2 = this._RptReport.Document;
			FileStream stream1 = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);
			try
			{
				document1.Export(stream1, TIFprop.cls1083(format), document2, 1, (int) document2.MaxPages);
			}
			finally
			{
				stream1.Flush();
				stream1.Close();
			}
		}

		public void ExportToPDF(Stream stream, PDFVersion version, bool compress)
		{
			this.ExportToPDF(stream, version, compress, true);
		}

		public void ExportToPDF(string filename, PDFVersion version, bool compress)
		{
			this.ExportToPDF(filename, version, compress, true);
		}

 
		public void ExportToPDF(Stream stream, PDFVersion version, bool compress, bool embedfont)
		{
			if ((this._RptReport != null) && (this._RptReport.MaxPages > 0))
			{
				PDFDocument document1 = new PDFDocument();
				document1.EmbedFont = embedfont;
				document1.Compress = compress;
				document1.Export(this._RptReport.Document, stream);
			}
		}

 
		public void ExportToPDF(string filename, PDFVersion version, bool compress, bool embedfont)
		{
			if ((this._RptReport != null) && (this._RptReport.MaxPages > 0))
			{
				PDFDocument document1 = new PDFDocument();
				document1.EmbedFont = embedfont;
				document1.Compress = compress;
				document1.Export(this._RptReport.Document, filename);
			}
		}

 
		public void Generate(Stream stream)
		{
			this._RptReport = new Report();
			this._RptReport.LoadLayout(ref stream);
			this._RptReport.Generate();
		}

 
		public void Generate(string fileName)
		{
			this._RptReport = new Report();
			this._RptReport.LoadLayout(fileName);
			this._RptReport.Generate();
		}

        public void Generate(ref Report report)
		{
			this._RptReport = report;
			this._RptReport.Generate();
		}

 
		public void Print()
		{
			if ((this._RptReport != null) && (this._RptReport.MaxPages > 0))
			{
				this._RptReport.Document.Print();
			}
		}

 
		public Document Document
		{
			get
			{
				return this._RptReport.Document;
			}
		}
 
		public long MaxPages
		{
			get
			{
				return this._RptReport.MaxPages;
			}
		}

        public Report Report
		{
			get
			{
				return this._RptReport;
			}
		}
	}
 
}
