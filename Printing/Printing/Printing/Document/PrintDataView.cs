using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using MControl.Data;
using MControl.Printing.Drawing;

namespace MControl.Printing
{
	/// <summary>
	/// Summary description for PrintDataView.
	/// </summary>


	public class DataViewDocument :IReportDocument
	{

		#region static methods


		public static HorizontalAlignment ConvertRtlToAlignment(System.Windows.Forms.RightToLeft rtl)
		{
		  return rtl==RightToLeft.Yes?HorizontalAlignment.Right:HorizontalAlignment.Left;
		}

        public static void PrintDataView(DataView dv, string header)
        {
            PrintDataView(dv, header, true);
        }

		public static void PrintDataView(DataView dv,string header,bool preview)
		{
			McPrintDocument rpt=new McPrintDocument();
			DataViewDocument rptList=new DataViewDocument(dv,header);
			rptList.CreateDocument(rpt);
			rpt.ReportSetting=rptList as IReportDocument;
            if (preview)
            {
                McPrintPreviewDialog dlg = new McPrintPreviewDialog();
                dlg.Document = rpt;
                dlg.Show();
            }
            else
            {
                rpt.Print();
            }
		}


		#endregion

		private string headerText="";
        public string HeaderText
        {
            get { return headerText; }
            set { headerText = value; }
        }

		private DataView dataView=null;
		private bool isResetStyle=false;
		private bool isColumnCreated=false;

        private McPrintDocument printDocument;

        public McPrintDocument PrintDocument
        {
            get 
            {
                if (printDocument == null)
                {
                    printDocument = new McPrintDocument();
                }
                return printDocument; 
            }
        }
		private MControl.Printing.ReportBuilder builder;

		private bool isCreated=false;

		public DataViewDocument(DataView dv,string headerText)
		{
			//printDocument=new McPrintDocument();
			dataView=dv;
			HeaderText=headerText;
			ResetStyles();
		}

		private void InitBuilder()
		{
			if(builder==null)
			{
				builder = new ReportBuilder(printDocument);
				builder.StartContainer(new MControl.Printing.Sections.LinearSections());
			}
		}

		public void ResetStyles()
		{  
			TextStyle.ResetStyles();
			isResetStyle=true;
		}

		public void SetCellMargins(float top,float left,float right,float bottom)
		{
			TextStyle.TableRow.MarginNear =left/10;// 0.1f;
			TextStyle.TableRow.MarginFar = right/10;//0.1f;
			TextStyle.TableRow.MarginTop =top/10;// 0.05f;
			TextStyle.TableRow.MarginBottom = bottom/10;//0.05f;
		}

		public void SetHeaderMargins(float top,float left,float right,float bottom)
		{
			TextStyle.TableHeader.MarginNear =left/10;// 0.1f;
			TextStyle.TableHeader.MarginFar = right/10;//0.1f;
			TextStyle.TableHeader.MarginTop =top/10;// 0.05f;
			TextStyle.TableHeader.MarginBottom = bottom/10;//0.05f;
		}

		public void SetHeader(Brush headerBack,Brush headerFore,Brush background)
		{
			TextStyle.Heading1.Size = 24;
			TextStyle.Heading1.Bold = false;
			TextStyle.TableHeader.BackgroundBrush =headerBack;//Brushes.Navy;
			TextStyle.TableHeader.Brush =headerFore;//Brushes.White;
			TextStyle.TableRow.BackgroundBrush =background;//Brushes.White;//
		}

		public void SetDefaultStyle()
		{
			if(!isResetStyle)
				ResetStyles();
			SetHeader(Brushes.Navy,Brushes.White,Brushes.Transparent);
			SetHeaderMargins(0.5f,1f,1f,0.5f);
			SetHeaderMargins(0.1f,0.1f,0.1f,0.1f);
		}

		public virtual void CreateDocument(McPrintDocument printDoc)
		{
			this.printDocument=printDoc;
			CreateDocument();
		}

		public virtual void CreateDocument()
		{
			CreateDocument(HorizontalAlignment.Left);
		}

		public virtual void CreateDocument(HorizontalAlignment alignment)
		{
			CreateDocument(alignment,80);
		}

		public virtual void CreateDocument(HorizontalAlignment alignment,float maxColumnWidth)
		{
			CreateDocument(alignment,maxColumnWidth,null);
		}

		public virtual void CreateDocument(HorizontalAlignment alignment,float maxColumnWidth,McColumn[] cols)
		{

            if (isCreated)
            {
                //PrintDocument.SectionData.rowIndex = 0;
                //PrintDocument.SectionData.startedPrinting = false;
                return;
            }
            if(!isResetStyle)
				ResetStyles();
			
            InitBuilder();

			//Pen innerPen = new Pen (Color.Silver, 0.01f);

			// Following line sets up the pen used for lins for tables
			builder.DefaultTablePen =PrintDocument.ThinPen;
		
			TextStyle.Heading1.StringAlignment = alignment==HorizontalAlignment.Left?StringAlignment.Near:alignment==HorizontalAlignment.Center?StringAlignment.Center:StringAlignment.Far;

			//builder.AddTextSection (HeaderText, TextStyle.Heading1);
			builder.DefaultTablePen = PrintDocument.ThinPen;
            //SetDefaultStyle();
            //if (HeaderText.Length > 0)
            //{
            //    builder.AddPageHeader(HeaderText, HorizontalAlignment.Center);
            //}
            //builder.AddPageFooter("Page %p", "Page %p", "Page %p", HorizontalAlignment.Center);
            
            MControl.Printing.Sections.ReportSectionData sectionData = builder.AddDataSection(dataView, true);
			PrintDocument.SectionData=sectionData;
		
			builder.CurrentSection.HorizontalAlignment =alignment;// HorizontalAlignment.Left;
			builder.CurrentSection.UseFullWidth = true;
			builder.CurrentSection.UseFullHeight = true;

			if(isColumnCreated)	goto Label_01;
            //this.PrintDocument.SectionData.ClearColumns();

			int docwidth=builder.CurrentDocument.DefaultPageSettings.PaperSize.Width;
			int colTotalWidth=0;
           // builder.CurrentDocument.DefaultPageSettings.Landscape = true;

			if(cols!=null)
			{
				colTotalWidth=GetColumnsTotalWidth(cols);
				if(docwidth<colTotalWidth)
				{
					builder.CurrentDocument.DefaultPageSettings.Landscape=true;
				}

				foreach(McColumn c in cols)
				{
					if(c.Display)
					{
						ReportDataColumn rdc=builder.AddColumn(c.ColumnName, c.Caption,(float )c.Width, true, true);
						rdc.ColumnAlignment=c.Alignment;
					}
				}
			}
			else
			{
				colTotalWidth=GetColumnsTotalWidth(maxColumnWidth);
				if(docwidth<colTotalWidth)
				{
					builder.CurrentDocument.DefaultPageSettings.Landscape=true;
				}
				builder.AddAllColumns(maxColumnWidth,true,true);
			}
			isColumnCreated=true;
	
			Label_01:
			isCreated=true;
		}

		public virtual void CreateColumns(McColumn[] cols)
		{
			if(isColumnCreated)
				return;

			InitBuilder();
			int docwidth=builder.CurrentDocument.DefaultPageSettings.PaperSize.Width;
			int colTotalWidth=0;

			if(cols!=null)
			{
				colTotalWidth=GetColumnsTotalWidth(cols);
				if(docwidth<colTotalWidth)
				{
					builder.CurrentDocument.DefaultPageSettings.Landscape=true;
				}

				foreach(McColumn c in cols)
				{
					if(c.Display)
					{
						ReportDataColumn rdc=builder.AddColumn(c.ColumnName, c.Caption,(float )c.Width, true, true);
						if(c.DataType==DataTypes.Number)
							rdc.ColumnAlignment=HorizontalAlignment.Right;
						else
							rdc.ColumnAlignment=c.Alignment;
					}
				}
				isColumnCreated=true;
			}
		}

		public virtual ReportDataColumn AddColumn(McColumn col)
		{
			InitBuilder();

			if(col==null)
			{
				throw new ArgumentException("Invalid Column control");
			}
			ReportDataColumn rdc=builder.AddColumn(col.ColumnName, col.Caption,(float )col.Width, true, true);
			rdc.ColumnAlignment=col.Alignment;
			return rdc;
		}

		public virtual ReportDataColumn AddColumn(string colName,string caption ,int width)
		{
			InitBuilder();

			if(colName!=null || caption==null || width <0)
			{
				throw new ArgumentException("Incorrect parameters");
			}
			ReportDataColumn rdc=builder.AddColumn(colName, caption,(float )width, true, true);
			return rdc;
		}

		public virtual ReportDataColumn AddColumn(string colName,string caption ,int width,HorizontalAlignment alignment)
		{
			InitBuilder();

			if(colName!=null || caption==null || width <0)
			{
				throw new ArgumentException("Incorrect parameters");
			}
			ReportDataColumn rdc=builder.AddColumn(colName, caption,(float )width, true, true);
			rdc.ColumnAlignment=alignment;
			return rdc;
		}

		private int GetColumnsTotalWidth(McColumn[] cols)
		{
			int colTotalWidth=0;
			if(cols!=null)
			{
				foreach(McColumn c in cols)
				{
					if(c.Display)
					{
						colTotalWidth+=c.Width;
					}
				}
			}
				return colTotalWidth;			
		}

		private int GetColumnsTotalWidth(float maxColWidth)
		{
			int colTotalWidth=0;
			if(dataView!=null)
			{
				colTotalWidth=dataView.Table.Columns.Count*(int)maxColWidth;
			}
			return colTotalWidth;			
		}

		public  void Show()
		{
			if(!isCreated)
			{
				throw new ArgumentException("Invalid Document, You should CreateDocument method befor using Show methods");
			}
	
			PrintDocument.ReportSetting=this as IReportDocument;
			McPrintPreviewDialog dlg=new McPrintPreviewDialog();
			dlg.Document=PrintDocument;
			dlg.Show();
		}

        public void Print()
        {
            if (!isCreated)
            {
                throw new ArgumentException("Invalid Document, You should CreateDocument method befor using Show methods");
            }

            PrintDocument.ReportSetting = this as IReportDocument;
            this.PrintDocument.Print();
        }
	}

	public class PrintDocumentText : IReportDocument
	{


		private McPrintDocument printDocument;
		private MControl.Printing.ReportBuilder builder;

		private bool isCreated=false;

		public PrintDocumentText()
		{
			//printDocument=new McPrintDocument();
		}

        public McPrintDocument PrintDocument
        {
            get
            {
                if (printDocument == null)
                {
                    printDocument = new McPrintDocument();
                }
                return printDocument;
            }
        }
		private void InitBuilder()
		{
			if(builder==null)
			{
				builder = new ReportBuilder(PrintDocument);
				builder.StartLinearLayout(Direction.Vertical);
				TextStyle.ResetStyles();
			}
		}

		public virtual void CreateDocument(McPrintDocument printDoc)
		{
			this.printDocument=printDoc;
			CreateDocument();
		}

		public virtual void AddHeaderText(string text,bool bold,Brush brush)
		{
			if(isCreated)
			{
				throw new Exception("Document allready created");
			}
			InitBuilder();
			//Header
			TextStyle style = new TextStyle(TextStyle.PageHeader);
			style.Bold=bold;
			style.Brush=brush;
			builder.AddTextSection(text, style);
		}

		public virtual void AddFooterText(string text,bool bold,Brush brush)
		{
			if(isCreated)
			{
				throw new Exception("Document allready created");
			}
			InitBuilder();
			//Footer
			TextStyle style = new TextStyle(TextStyle.PageFooter);
			style.Bold=bold;
			style.Brush=brush;
			builder.AddTextSection(text, style);
		}

		public virtual void AddBodyText(string text,bool bold,Brush brush)
		{
			if(isCreated)
			{
				throw new Exception("Document allready created");
			}
			InitBuilder();
			//Body
			TextStyle style = new TextStyle(TextStyle.Normal);
			style.Bold=bold;
			style.Brush=brush;
			builder.AddTextSection(text, style);
		}

		public virtual void CreateDocument()
		{
			if(isCreated)
				return;
			InitBuilder();
			builder.FinishLinearLayout();
			isCreated=true;
		}

		public virtual void CreateDocument(string bodyText)
		{
			if(isCreated)
				return;
			InitBuilder();
			builder.AddTextSection(bodyText);
			builder.FinishLinearLayout();
			isCreated=true;
		}

		public virtual void CreateDocument(string bodyText, string headerText,string footerText)
		{

			if(isCreated)
				return;

			InitBuilder();

			//Header
			TextStyle header = new TextStyle(TextStyle.PageHeader);
			header.Bold=true;
			header.Brush=Brushes.Blue;
			builder.AddTextSection(headerText, header);
			
			//Body
			TextStyle body = new TextStyle(TextStyle.Normal);
			builder.AddTextSection(bodyText,body);

			//Footer
			builder.AddTextSection(footerText, TextStyle.PageFooter);
	
			builder.FinishLinearLayout();
			isCreated=true;

		}

		public  void Show()
		{
			if(!isCreated)
			{
				throw new ArgumentException("Invalid Document, You should CreateDocument method befor using Show methods");
			}
	
			PrintDocument.ReportSetting=this as IReportDocument;
			McPrintPreviewDialog dlg=new McPrintPreviewDialog();
			dlg.Document=PrintDocument;
			dlg.Show();
		}
	}
}