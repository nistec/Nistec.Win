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
	/// <summary>
	/// Summary description for ViewBuilder.
	/// </summary>
	public class ViewBuilder
    {
        #region static

        public static void Export(DataTable dataSource, object prop, string header, string footer, ExportType exType, string filepath, bool openAfter, PageOrientation orientation, bool RightToLeft)
        {

            ViewBuilder builder = new ViewBuilder(orientation, RightToLeft);
            builder.CreateReport(dataSource);
            builder.AddHeaderAndFooter(header, footer);
            builder.Generate();
            builder.Export(prop, exType, filepath, openAfter);
        }

        public static void Preview(DataTable dataSource, string header, string footer, PageOrientation orientation, bool RightToLeft)
        {
            ViewBuilder builder = new ViewBuilder(orientation, RightToLeft);
            builder.CreateReport(dataSource);
            builder.AddHeaderAndFooter(header, footer);
            builder.Generate();
            builder.Preview();
        }

        public static Report Create(DataTable dataSource, string header, string footer, PageOrientation orientation, bool RightToLeft)
        {
            ViewBuilder builder = new ViewBuilder(orientation, RightToLeft);
            builder.CreateReport(dataSource);
            builder.AddHeaderAndFooter(header, footer);
            builder.Generate();
            return builder.IReport;
        }

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

        private ReportTemplate report;
		private PageOrientation pageOrientation;
		private bool rptCreated;
        private bool rightToLeft;

        public ReportTemplate IReport
        {
            get { return report; }
        }

        public ViewBuilder()
		{
            report = new ReportTemplate();
			pageOrientation= PageOrientation.Default;
			rptCreated=false;
            rightToLeft = false;
        }
  

        public ViewBuilder(PageOrientation Orientation, bool RightToLeft)
        {
            report = new ReportTemplate();
            pageOrientation = Orientation;
            rptCreated = false;
            rightToLeft = RightToLeft;
        }
        public ViewBuilder(Nistec.Printing.View.Design.ReportDesign rpt)
        {
            report = Clone(rpt);
            rptCreated = false;
        }
        public ReportTemplate Clone(Nistec.Printing.View.Design.ReportDesign rpt)
        {
            ReportTemplate report = new ReportTemplate();
            report.DataFields = rpt.DataFields;
            report.DataSource = rpt.DataSource;
            report.DefaultPaperSize = rpt.DefaultPaperSize;
            report.DefaultPaperSource = rpt.DefaultPaperSource;
            //report.Document = rpt.Document;
            report.Duplex = rpt.Duplex;
            report.EOF = rpt.EOF;
            foreach (McField field in rpt.Fields)
            {
                report.Fields.Add(field);
            }
            report.MarginBottom = rpt.MarginBottom;
            report.MarginLeft = rpt.MarginLeft;
            report.MarginRight = rpt.MarginRight;
            report.MarginTop = rpt.MarginTop;
            report.MaxPages = rpt.MaxPages;
            report.Orientation = rpt.Orientation;
            report.PageSetting = rpt.PageSetting;
            report.PaperHeight = rpt.PaperHeight;
            report.PaperKind = rpt.PaperKind;
            report.PaperSource = rpt.PaperSource;
            report.PaperWidth = rpt.PaperWidth;
            report.ParentReport = rpt.ParentReport;
            report.ReportWidth = rpt.ReportWidth;
            report.RightToLeft = rpt.RightToLeft;
            report.Script = rpt.Script;
            report.ScriptLanguage = rpt.ScriptLanguage;
            foreach (Section sec in rpt.Sections)
            {
                if (report.Sections.IndexOf(sec.Name) < 0)
                    report.Sections.Add(sec);
            }
            report.Site = rpt.Site;
            //report.Version = rpt.Version;
            return report;
        }

        #endregion

        #region private methods

        private float GetPageWidth()
		{
			float pageWidth=report.ReportWidth;
	
			string unit=ReportUtil.Unit;
            System.Drawing.SizeF size = ReportUtil.GetPaperSize(report.PaperKind.ToString(), ref unit);
			if(size !=System.Drawing.SizeF.Empty)
			{
				if(pageOrientation==PageOrientation.Landscape)
                    pageWidth = ReportUtil.ConvertToPoint(size.Height, unit);
				else
                    pageWidth = ReportUtil.ConvertToPoint(size.Width, unit);
			}
           return pageWidth;
		}

		private void InitReport()
		{
            //PageHeader header = new PageHeader();
            //header.Height = 30F;

            //ReportDetail detail = new ReportDetail();
            //detail.KeepTogether = true;
            //detail.Height = 30F;

            //PageFooter footer = new PageFooter();
            //footer.Height = 30F;

            //report.ReportWidth = 578F;
            //report.Sections.AddRange(new Nistec.Printing.View.Section[] {
            //header,
            //detail,
            //footer});

            report.Sections["ReportDetail"].Controls.Clear();
            report.Sections["GroupHeader"].Visible = true;

			report.DefaultPaperSize = false;
			report.PaperKind = System.Drawing.Printing.PaperKind.A4;
			report.Orientation=pageOrientation;
		}


		#endregion

		#region Report Template

        public void Preview()
        {
            if (!rptCreated)
            {
                throw new Exception("Report not created");
            } 
            ReportViewer.Preview(report);
        }
 
        public void Export(object prop, ExportType exType, string filepath, bool openAfter)
        {
            if (!rptCreated)
            {
                throw new Exception("Report not created");
            }
            Document doc = report.Document;

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

        public void Export(Report report, object prop, ExportType exType, string filepath, bool openAfter)
        {
            if (!rptCreated)
            {
                throw new Exception("Report not created");
            }
            Document doc = report.Document;

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

        public ReportTemplate Generate()
        {
            report.Generate();
            rptCreated = true;
            return report;
        }

        public void AddHeaderAndFooter(string header, string footer)
        {
            if (!string.IsNullOrEmpty(header))
            {
                report.AddHeader(header);
            }
            if (!string.IsNullOrEmpty(footer))
            {
                report.AddFooter(footer);
            }
        }

        public void AddLabel(string section, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                report.AddLabel(section,text,rightToLeft);
            }
        }

        public void CreateReport(string dataSource, string header,bool rightToLeft)
		{
			InitReport();
			float pageWidth=GetPageWidth();
			report.ReportWidth=pageWidth;
			float margins=ReportUtil.ConvertToPoint(report.MarginLeft+report.MarginRight,ReportUtil.Unit);
			pageWidth-=margins;

			float ctlWidth=pageWidth;//McControl.DefaultWidth;
			float ctlHeight=McReportControl.DefaultHeight;

			report.Sections["ReportDetail"].Height=ctlHeight;

			System.Data.DataTable dt = new System.Data.DataTable("DataSource");
			dt.Columns.Add(new System.Data.DataColumn("Txt", typeof(string)));
			System.Data.DataRow dr = dt.NewRow();
			dr["Txt"] = dataSource;
			dt.Rows.Add(dr);

			McTextBox ctl=CreateRptText("Txt","Text",0,0,ctlWidth,ctlHeight,false,rightToLeft);
			ctl.Parent=report.Sections["ReportDetail"];
			report.Sections["ReportDetail"].Controls.Add(ctl);
			
			if(header !=null && header !="")
			{
				report.Sections["GroupHeader"].Height=ctlHeight+10F;

				McLabel lbl =CreateRptLable(0,header,0,0,ctlWidth,ctlHeight,rightToLeft);
				lbl.BackColor=System.Drawing.Color.Navy;
				lbl.ForeColor=System.Drawing.Color.White;
                lbl.Parent = report.Sections["GroupHeader"];
				report.Sections["GroupHeader"].Controls.Add(lbl);
			}
		
			report.DataSource=dt;
			//rptCreated=true;
        }

        public void CreateReport(System.Data.DataTable dataSource)
        {
            CreateReport(dataSource, PageOrientation.Default);
        }

        public void CreateReport(System.Data.DataTable dataSource, PageOrientation orientation)
		{
			pageOrientation=orientation;

            InitReport();

			float lft=0;
			int index=0;

            float pageWidth = GetPageWidth();
            report.ReportWidth = pageWidth;

			float margins=ReportUtil.ConvertToPoint(report.MarginLeft+report.MarginRight,ReportUtil.Unit);
			pageWidth-=margins;

			int cnt=dataSource.Columns.Count;
			float ctlWidth=McReportControl.DefaultWidth;
            float ctlHeight = McReportControl.DefaultHeight;
            float totalDiff = (cnt * McReportControl.DefaultWidth) - pageWidth;


			if(totalDiff>0)
			{
				float diff=totalDiff/cnt;
				ctlWidth-=diff;
				ctlHeight+=diff/2;
				report.Sections["ReportDetail"].Height=ctlHeight;
                report.Sections["GroupHeader"].Height = ctlHeight + 10F;
			}

			foreach(DataColumn c in dataSource.Columns)
			{
                bool isNumber = !(c.DataType == typeof(string));
				McTextBox ctl=CreateRptText(c.ColumnName,c.ColumnName,lft,0,ctlWidth,ctlHeight,isNumber,rightToLeft);
				McLabel lbl =CreateRptLable(index,c.ColumnName,lft,0,ctlWidth,ctlHeight,rightToLeft);
				lbl.BackColor=System.Drawing.Color.Navy;
				lbl.ForeColor=System.Drawing.Color.White;

				lft+=ctl.Width;
				report.Sections["ReportDetail"].Controls.Add(ctl);
                report.Sections["GroupHeader"].Controls.Add(lbl);
				ctl.Parent=report.Sections["ReportDetail"];
                lbl.Parent = report.Sections["GroupHeader"];//report.Sections["header1"];
				index++;
			}

			report.DataSource=dataSource;
            //rptCreated = true;
        }

        public void CreateReport(System.Data.DataTable dataSource, DataGridTableStyle tableStyle)
		{

			InitReport();

			string caption="";
			string mappingName="";
			float ctlWidth=McReportControl.DefaultWidth;
            float ctlHeight = McReportControl.DefaultHeight;
			Color headerBackColor=tableStyle.HeaderBackColor;
            Color headerforecolor=tableStyle.HeaderForeColor;
			float totalWidth=0;
			int count=tableStyle.GridColumnStyles.Count;


			for (int i = 0; i < count; i++)
			{
              totalWidth+=tableStyle.GridColumnStyles[i].Width;
			}
			float pageWidth=GetPageWidth();
			report.ReportWidth=pageWidth;
			float margins=ReportUtil.ConvertToPoint(report.MarginLeft+report.MarginRight,ReportUtil.Unit);
			pageWidth-=margins;
	
			float totalDiff=totalWidth-pageWidth;
			float lft=0;
			float diff=0;

			if(totalDiff>0)
			{
				diff=totalDiff/count;
				//ctlWidth-=diff;
				ctlHeight+=diff/2;
				report.Sections["ReportDetail"].Height=ctlHeight;
				report.Sections["GroupHeader"].Height=ctlHeight+10F;
			}

 
			for (int i = 0; i < count; i++)
			{
				mappingName = tableStyle.GridColumnStyles[i].MappingName;
				caption = tableStyle.GridColumnStyles[i].HeaderText;
				ctlWidth = tableStyle.GridColumnStyles[i].Width-diff;

				McTextBox ctl=CreateRptText(mappingName,caption,lft,0,ctlWidth,ctlHeight,false,rightToLeft);
				McLabel lbl =CreateRptLable(i,caption,lft,0,ctlWidth,ctlHeight,rightToLeft);
				lbl.BackColor=headerBackColor;
				lbl.ForeColor=headerforecolor;

				lft+=ctlWidth;
				report.Sections["ReportDetail"].Controls.Add(ctl);
				report.Sections["GroupHeader"].Controls.Add(lbl);
				ctl.Parent=report.Sections["ReportDetail"];
				lbl.Parent=report.Sections["GroupHeader"];
			}
			report.DataSource=dataSource;
            //rptCreated = true;
		}




		public void AddControls(string sectionName,Nistec.Printing.View.McReportControl[] controls)
		{
			int index=report.Sections.IndexOf(sectionName);
			if(index == -1)
			{
				throw new Exception("Section Not found ");
			}
			AddControls(index,controls);
		}

		public void AddControls(int sectionIndex,Nistec.Printing.View.McReportControl[] controls)
		{
			try
			{
				report.Sections[sectionIndex].Controls.AddRange(controls);
				foreach(McReportControl c in controls)
				{
					c.Parent=report.Sections[sectionIndex];
				}
			}
			catch(Exception ex)
			{
				throw ex; 
			}
		}

		public void AddControl(string sectionName,Nistec.Printing.View.McReportControl c)
		{
			int index=report.Sections.IndexOf(sectionName);
			if(index == -1)
			{
				throw new Exception("Section Not found ");
			}
			AddControl(index,c);
		}

        public void AddControl(int sectionIndex, Nistec.Printing.View.McReportControl c)
		{
			try
			{
				report.Sections[sectionIndex].Controls.Add(c);
				c.Parent=report.Sections[sectionIndex];
			}
			catch(Exception ex)
			{
				throw ex; 
			}
		}

		public void AddSection(SectionType sectionType,string sectionName,float height)
		{
			Nistec.Printing.View.Section section=null;
			switch(sectionType)
			{
				case SectionType.ReportDetail:
					return;
				case SectionType.GroupFooter:
					section= new Nistec.Printing.View.GroupFooter(sectionName);
					break;
				case SectionType.GroupHeader:
					section= new Nistec.Printing.View.GroupHeader(sectionName);
					break;
				case SectionType.PageFooter:
					section= new Nistec.Printing.View.PageFooter(sectionName);
					break;
				case SectionType.PageHeader:
					section= new Nistec.Printing.View.PageHeader(sectionName);
					break;
				case SectionType.ReportFooter:
					section= new Nistec.Printing.View.ReportFooter(sectionName);
					break;
				case SectionType.ReportHeader:
					section= new Nistec.Printing.View.ReportHeader(sectionName);
					break;
			}
			if(section==null)
			{
				return; 
			}
			section.Height=height;
		}

		#endregion

		#region Controls

        private McTextBox CreateRptText(string dataField, string caption, float x, float y, float width, float height, bool isNumber, bool isRtl)
        {
            ContentAlignment align = ContentAlignment.MiddleLeft;
            if (isNumber)
            {
                isRtl = false;
                align = ContentAlignment.MiddleRight;
            }
            else
            {
                align = isRtl ? ContentAlignment.TopRight : ContentAlignment.TopLeft;
            }
            return CreateRptText(dataField, caption, x, y, width, height, align, isRtl);
		
        }
		private McTextBox CreateRptText(string dataField,string caption,float x,float y,float width,float height, ContentAlignment align, bool isRtl)
		{
            McTextBox ctl = new McTextBox();

			ctl.DataField = dataField;
			ctl.Name = dataField;
			ctl.Text = caption;
			//ctl.TextFont =new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			ctl.Left = x;
			ctl.Top = y;
			ctl.Width = width;
			ctl.Height = height;
            ctl.TextAlign = align;
            ctl.RightToLeft = isRtl;
			return ctl;
		}

        private McLabel CreateRptLable(int index, string caption, float x, float y, float width, float height, bool isNumber, bool isRtl)
        {
            ContentAlignment align = ContentAlignment.MiddleLeft;
            if (isNumber)
            {
                isRtl = false;
                align = ContentAlignment.MiddleRight;
            }
            else
            {
                align = isRtl ? ContentAlignment.TopRight : ContentAlignment.TopLeft;
            }

            return CreateRptLable(index, caption, x, y, width, height, align, isRtl);

        }
        private McLabel CreateRptLable(int index, string caption, float x, float y, float width, float height,bool isRtl)
        {
            ContentAlignment align = isRtl ? ContentAlignment.TopRight : ContentAlignment.TopLeft;
            return CreateRptLable(index, caption, x, y, width, height, align, isRtl);

        }

        private McLabel CreateRptLable(int index, string caption, float x, float y, float width, float height, ContentAlignment align, bool isRtl)
		{
            McLabel ctl = new McLabel();

			ctl.Name = "Label" + index.ToString();
			ctl.Text = caption;
			//ctl.TextFont =new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			ctl.Left = x;
			ctl.Top = y;
			ctl.Width = width;
			ctl.Height = height;
            ctl.TextAlign = align;
            ctl.RightToLeft = isRtl;
            return ctl;
		}

		#endregion

	}
}
