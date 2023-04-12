using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Printing;
using Nistec.Printing.Sections;
using Nistec.Printing.Drawing;

using System.IO;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using Nistec.Win;


namespace Nistec.Printing
{
	/// <summary>
	/// McPrintDocument extends from <see cref="System.Drawing.Printing.PrintDocument"/>
	/// and is customized for printing reports from one or more tables of data. 
	/// </summary>
//	/// <remarks>
//	/// <para>
//	/// A McPrintDocument is used just like <see cref="System.Drawing.Printing.PrintDocument"/>
//	/// when used with other printing framework classes, such as <see cref="System.Windows.Forms.PrintDialog"/>
//	/// or <see cref="System.Windows.Forms.PrintPreviewDialog"/>
//	/// </para>
//	/// <para>
//	/// A McPrintDocument object is the top level container for all the 
//	/// sections that make up the report.  (This consists of a header, body, and footer.)
//	/// </para>
//	/// <para>
//	/// The McPrintDocument's main job is printing, which occurs when the
//	/// Print() method is called of the base class.   The Print() method 
//	/// iterates through all the ReportSections making up the document, \
//	/// printing each one.
//	/// </para>
//	/// <para>
//	/// The strategy design pattern is employed for formatting the report.
//	/// An object implementing <see cref="Nistec.Printing.IReportDocument"/>
//	/// may be associated with the McPrintDocument. This IReportDocument 
//	/// object is application specific and knows how to create a report
//	/// based on application state and user settings. This object would be
//	/// responsible for creating sections, associating DataViews, 
//	/// and applying any required styles through use of the 
//	/// <see cref="Nistec.Printing.TextStyle"/> class.  It will generally
//	/// use the <see cref="Nistec.Printing.ReportBuilder"/> class to
//	/// assist with the complexity of building a report.
//	/// </para>
//	/// </remarks>

    [ToolboxBitmap(typeof(McPrintDocument), "Images.Document.bmp"), ToolboxItem(true)]//, DefaultProperty("Document")]
    public class McPrintDocument : System.Drawing.Printing.PrintDocument
    {


        //public void Save(string file)
        //{
        //    SerializablePrintDoc doc = new SerializablePrintDoc(this);
        //    //McPrintDocument doc = this;
        //    //Opens a file and serializes the object into it in binary format.
        //    Stream stream = File.Open(file, FileMode.Create);
        //    //SoapFormatter formatter = new SoapFormatter();
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    formatter.Serialize(stream, doc);
        //    stream.Close();
        //}

        //public McPrintDocument Load(string file)
        //{
        //    McPrintDocument doc = null;

        //    //Opens file ".xml" and deserializes the object from it.
        //    Stream stream = File.Open(file, FileMode.Open);
        //    //formatter = new SoapFormatter();
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    doc = (McPrintDocument)formatter.Deserialize(stream);
        //    stream.Close();
        //    return doc;
        //}


//		private System.Data.DataView dataSource;
//
//		public System.Data.DataView DataSource
//		{
//			get{return dataSource;}
//			set{dataSource=value;}
//		}
//
//		private Nistec.Data.ExportColumnType[] columnType;
//
//		public Nistec.Data.ExportColumnType[] ColumnType
//		{
//			get{return columnType;}
//			set{columnType=value;}
//		}

		public void ExportDocument()
		{
			if(sectionData==null)
			{
				MsgBox.ShowWarning("Invalid SectionData Definition");
			}
			sectionData.ExportDocument();
//			if(columnType!=null)
//				Data.Export.WinExport(dataSource.Table,columnType);
//			else
//				Data.Export.WinExport(dataSource.Table);
	
		}

        public McPrintDocument Copy()
        {
            McPrintDocument doc = new McPrintDocument();
            doc.body = this.body;
            doc.currentPage = this.currentPage;
            doc.DefaultPageSettings = this.DefaultPageSettings;
            doc.DocumentName = this.DocumentName;
            doc.documentUnit = this.documentUnit;
            doc.normalPen = this.normalPen;
            doc.OriginAtMargins = this.OriginAtMargins;
            doc.pageFooter = this.pageFooter;
            doc.pageFooterMaxHeight = this.pageFooterMaxHeight;
            doc.pageHeader = this.pageHeader;
            doc.pageHeaderMaxHeight = this.pageHeaderMaxHeight;
            doc.PrintController = this.PrintController;
            doc.PrinterSettings = this.PrinterSettings;
            doc.reportSetting = this.reportSetting;
            doc.resetAfterPrint = this.resetAfterPrint;
            doc.sectionData = this.sectionData;
            doc.thickPen = this.thickPen;
            doc.thinPen = this.thinPen;
            doc.totalPages = this.totalPages;
            return doc;

        }

		private ReportSectionData sectionData;

		public ReportSectionData SectionData
		{
			get{return sectionData;}
			set{sectionData=value;}
		}

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

//		public McPrintDocument(System.Data.DataView dataSrc,Nistec.Data.ExportColumnType[] colType):this()
//		{
//			this.dataSource=dataSrc;
//			this.columnType=colType;
//		}
   
        /// <summary>
        /// Default Constructor
        /// </summary>
        public McPrintDocument()
        {
			
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            //this.resetAfterPrint = true;
            DocumentUnit = GraphicsUnit.Inch;
            ResetPens();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

		#region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
		#endregion


        IReportDocument reportSetting;
        float pageHeaderMaxHeight = 1F;
        float pageFooterMaxHeight = 1F;
        ReportSection pageHeader;
        ReportSection pageFooter;
        ReportSection body;
        private int currentPage;
        int totalPages;
        //bool pagesWereCounted;
        //bool countPages = true;
        bool resetAfterPrint;
        Pen normalPen;
        Pen thinPen;
        Pen thickPen;
        GraphicsUnit documentUnit;

        #region "Properties"

        /// <summary>
        /// An object that will setup this report before printing.
        /// (Strategy pattern).
        /// </summary>
        public IReportDocument ReportSetting
        {
            get { return this.reportSetting; }
            set { this.reportSetting = value; }
        }

        /// <summary>
        /// Used to define the size of the page header
        /// The header will stop at or before this distance from the top margin of the page
        /// </summary>
        public float PageHeaderMaxHeight
        {
            get { return this.pageHeaderMaxHeight; }
            set { this.pageHeaderMaxHeight = value; }
        }

        /// <summary>
        /// Used to define the size of the page footer
        /// The footer will start at this distance from the bottom margin of the page
        /// </summary>
        public float PageFooterMaxHeight
        {
            get { return this.pageFooterMaxHeight; }
            set { this.pageFooterMaxHeight = value; }
        }

        /// <summary>
        /// Returns the current page number
        /// </summary>
        /// <returns>Integer for the current page number</returns>
        public int GetCurrentPage()
        {
            return currentPage;
        }

        /// <summary>
        /// Gets the total number of pages in the document.
        /// This is only becomes valid during the printing of
        /// the first page.
        /// </summary>
        /// <returns>Total number of pages</returns>
        public int TotalPages
        {
            get { return this.totalPages; }
        }

        /// <summary>
        /// Gets or sets the flag
        /// indicating all sections are cleared after printing.
        /// This allows the next print of a different document to assume
        /// a clear document, and releas memory.
        /// </summary>
        public bool ResetAfterPrint
        {
            get { return this.resetAfterPrint; }
            set { this.resetAfterPrint = value; }
        }

        /// <summary>
        /// The ReportSection responsible for printing the page header.
        /// </summary>
        public ReportSection PageHeader
        {
            get { return this.pageHeader; }
            set { this.pageHeader = value; }
        }

        /// <summary>
        /// The ReportSection reponsible for printing the page footer.
        /// </summary>
        public ReportSection PageFooter
        {
            get { return this.pageFooter; }
            set { this.pageFooter = value; }
        }

        /// <summary>
        /// The ReportSection responsible for printing the page body.
        /// </summary>
        public ReportSection Body
        {
            get { return this.body; }
            set { this.body = value; }
        }

        /// <summary>
        /// Gets the pen for normal styled lines
        /// </summary>
        public Pen NormalPen
        {
            get { return this.normalPen; }
        }
        /// <summary>
        /// Gets the pen for thin lines
        /// </summary>
        public Pen ThinPen
        {
            get { return this.thinPen; }
        }
        /// <summary>
        /// Gets the pen for thick lines
        /// </summary>
        public Pen ThickPen
        {
            get { return this.thickPen; }
        }

        /// <summary>
        /// Gets or sets the units used for the entire document.  All
        /// margins, widths and heights should be expressed in this unit.
        /// Only inch has been tested at this point.  Millimeter should 
        /// work as well, with a few modifications.
        /// </summary>
        /// <remarks>
        /// Some notes on specifiying units other than inches.  Millimeter
        /// has been briefly tested, but a few things should be noted.
        /// <para>
        /// Every data section needs to have the following quantities set
        /// to something more realistic for units other than inches:
        /// <code>
        ///         MaxDetailRowHeight = 8F;
        ///         MaxHeaderRowHeight = 8F;
        /// </code>
        /// </para>
        /// <para>
        /// Default pen widths in McPrintDocument should probably be changed.
        /// </para>
        /// <para>
        /// Other units, besides inches and millimeters are not supported at all.
        /// To support these, first modify or override
        /// McPrintDocument.GetPageMarginScale()
        /// </para>
        /// <para>
        /// I (mike@mag37.com) would be interested in your results with this.
        /// </para>
        /// </remarks>
        public virtual GraphicsUnit DocumentUnit
        {
            get { return this.documentUnit; }
            set { this.documentUnit = value; }
        }
        #endregion

        /// <summary>
        /// Resets pens back to default values
        /// Black, and .08", .03" and .01" for thick, normal, and thin
        /// (assuming inches as the scale - these might be too thin
        /// if millimeters are assumed)
        /// </summary>
        public virtual void ResetPens()
        {
            // expressed in inches
            normalPen = new Pen (Color.Silver, 0.03f);
            thinPen = new Pen (Color.Silver, 0.01f);
            thickPen = new Pen (Color.Silver, 0.08f);
        }


        /// <summary>
        /// Reset the row and page count before printing
        /// </summary>
        /// <param name="e">PrintEventArgs</param>
        protected override void OnBeginPrint(System.Drawing.Printing.PrintEventArgs e)
        {
            //this.pagesWereCounted = false;
            this.totalPages = 0;
            reset();
            //this.currentPage = 0;

        }

        void reset()
        {
            if (this.ReportSetting != null)
            {
                this.ReportSetting.CreateDocument(this);
            }

            this.currentPage = 0;
            //this.body.BeginPrintCalled = false;
        }

        /// <summary>
        /// This is used to scale page margins (and other quantities)
        /// from inches to the GraphicsUnit specified.
        /// </summary>
        /// <returns>A floating point number which can be multiplied by
        /// a unit in inches to get a unit in the current DocumentUnit.
        /// </returns>
        protected virtual float GetPageMarginScale()
        {
            float scale = 1f;
            switch (DocumentUnit)
            {
                case GraphicsUnit.Inch:
                    scale = 1f;
                    break;
                case GraphicsUnit.Millimeter:
                    scale = 25.4f;
                    break;
            }
            return scale;
        }

        /// <summary>
        /// The actual method to print a page
        /// </summary>
        /// <param name="e">PrintPageEventArgs</param>
        /// <param name="sizeOnly">Indicates that only sizing is done</param>
        /// <returns>True if there are more pages</returns>
        protected virtual bool PrintAPage (PrintPageEventArgs e, bool sizeOnly)
        {
            Graphics g = e.Graphics;
            g.PageUnit = DocumentUnit;

            // Define page bounds (margins are always in inches)

            float scale = GetPageMarginScale();
            float leftMargin      = (e.MarginBounds.Left   / 100F) * scale;
            float rightMargin     = (e.MarginBounds.Right  / 100F) * scale;
            float topMargin       = (e.MarginBounds.Top    / 100F) * scale;
            float bottomMargin    = (e.MarginBounds.Bottom / 100F) * scale;
            float width           = (e.MarginBounds.Width  / 100F) * scale;
            float height          = (e.MarginBounds.Height / 100F) * scale;
            Bounds pageBounds = new Bounds(leftMargin, topMargin, rightMargin, bottomMargin);

            // Header
            if (this.PageHeader != null)
            {
                Bounds headerBounds = pageBounds;
                if (this.PageHeaderMaxHeight > 0)
                {
                    headerBounds.Limit.Y = headerBounds.Position.Y + this.PageHeaderMaxHeight;
                }
                this.PageHeader.Print(this, g, headerBounds);
                pageBounds.Position.Y += this.PageHeader.Size.Height;
            }
           
            // Footer
            if (this.PageFooter != null)
            {
                Bounds footerBounds = pageBounds;
                if (this.PageFooterMaxHeight > 0)
                {
                    footerBounds.Position.Y = footerBounds.Limit.Y - this.PageFooterMaxHeight;
                }
                this.PageFooter.CalcSize (this, g, footerBounds);
                footerBounds = footerBounds.GetBounds (this.PageFooter.Size,
                    this.PageFooter.HorizontalAlignment, this.PageFooter.VerticalAlignment);
                this.PageFooter.Print (this, g, footerBounds);
                pageBounds.Limit.Y -= this.PageFooter.Size.Height;
            }

            // Body
            if (this.Body != null)
            {
                this.Body.Print(this, g, pageBounds);
                e.HasMorePages = this.Body.Continued;
            }
            else
            {
                e.HasMorePages = false;
            }
            return e.HasMorePages;
        } // OnPrintPage

        /// <summary>
        /// Overrided OnPrintPage from PrintDocument. 
        /// This method, on first call, may count the pages.
        /// Then it will simple call PrintAPage on every
        /// call.
        /// </summary>
        /// <param name="e">PrintPageEventArgs</param>
        protected override void OnPrintPage (PrintPageEventArgs e)
        {
            // TODO: Eventually try to support counting pages ahead of time
//            if (this.countPages && !this.pagesWereCounted)
//            {
//                this.totalPages = 0;
//                while (PrintAPage(e, true))
//                {
//                    this.totalPages++;
//                }
//                this.pagesWereCounted = true;
//            }
//            this.reset();

            this.currentPage++; // preincrement, so the first page is page 1
            PrintAPage(e, false);
        }




		/// <summary>
		/// Called at the end of printing.  If ResetAfterPrint is set (default is true)
		/// then all text sections will be released after printing.
		/// </summary>
		/// <param name="e">PrintEventArgs</param>
        protected override void OnEndPrint(System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.ResetAfterPrint)
            {
                // reset stuff
                this.PageHeader = null;
                this.PageFooter = null;
                this.Body = null;
                this.PageHeaderMaxHeight = 0F;
                this.PageFooterMaxHeight = 0F;
            }
        }

        /// <summary>
        /// Add a header to the report document with one section of text.
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="hAlign">Alignment of the text within the header</param>
        public void AddPageHeader(
            string text,
            HorizontalAlignment hAlign
            )
        {
            RepeatableTextSection section = new RepeatableTextSection(text, TextStyle.PageHeader);
            section.TextFirstPage = text;
            section.TextEvenPage = text;
            section.TextOddPage = text;
            section.UseFullWidth = true;
            section.HorizontalAlignment = hAlign;
            this.PageHeader= section;
        }

        /// <summary>
        /// Adds a footer to the report document with one section of text.
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="addPageCounter">Page counter to be displayed</param>
        /// <param name="addCurrentDate">Current date and time to be displayed</param>
        /// <param name="hAlign">Alignment of the text within the footer</param>
        public void AddPageFooter(
            string text,
            bool addPageCounter,
            bool addCurrentDate,
            HorizontalAlignment hAlign
            )
        {
            string stext = string.Format("{0} | {1} | {2}", addCurrentDate ? DateTime.Now.ToString("d"): "", text, addPageCounter ? "Page %p" : "");

            RepeatableTextSection section = new RepeatableTextSection(stext, TextStyle.PageFooter);
            section.TextFirstPage = stext;
            section.TextEvenPage = stext;
            section.TextOddPage = stext;
            section.UseFullWidth = true;
            section.HorizontalAlignment = hAlign;
            this.PageFooter = section;
        }
  
        public void SetTableHeaderStyle(Brush headerBack, Brush headerFore, Brush background)
        {
            TextStyle.Heading1.Size = 24;
            TextStyle.Heading1.Bold = false;
            TextStyle.TableHeader.BackgroundBrush = headerBack;//Brushes.Navy;
            TextStyle.TableHeader.Brush = headerFore;//Brushes.White;
            TextStyle.TableRow.BackgroundBrush = background;//Brushes.White;//
        }

	} // class
}
