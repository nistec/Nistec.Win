using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Printing;
using MControl.Printing.Sections;
using MControl.Printing.Drawing;

using System.IO;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;


namespace MControl.Printing
{

    [Serializable]
    public class SerializablePrintDoc 
    {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SerializablePrintDoc(McPrintDocument document)
        {
            this.Body = document.Body;
            //this.currentPage = document.currentPage;
            this.DefaultPageSettings = document.DefaultPageSettings;
            this.DocumentName = document.DocumentName;
            this.DocumentUnit = document.DocumentUnit;
            //this.NormalPen = document.normalPen;
            this.OriginAtMargins = document.OriginAtMargins;
            this.PageFooter = document.PageFooter;
            this.PageFooterMaxHeight = document.PageFooterMaxHeight;
            this.PageHeader = document.PageHeader;
            this.PageHeaderMaxHeight = document.PageHeaderMaxHeight;
            //this.PrintController = document.PrintController;
            this.PrinterSettings = document.PrinterSettings;
            this.ReportSetting = document.ReportSetting;
            this.ResetAfterPrint = document.ResetAfterPrint;
            this.SectionData = document.SectionData;
            //this.ThickPen = document.thickPen;
            //this.ThinPen = document.thinPen;
            //this.TotalPages = document.totalPages;

        }

        public McPrintDocument Copy()
        {
            McPrintDocument doc = new McPrintDocument();
           
            doc.Body = this.body;
            //doc.currentPage = this.currentPage;
            doc.DefaultPageSettings = this.DefaultPageSettings;
            doc.DocumentName = this.DocumentName;
            doc.DocumentUnit = this.documentUnit;
            //doc.NormalPen = this.normalPen;
            doc.OriginAtMargins = this.OriginAtMargins;
            doc.PageFooter = this.pageFooter;
            doc.PageFooterMaxHeight = this.pageFooterMaxHeight;
            doc.PageHeader = this.pageHeader;
            doc.PageHeaderMaxHeight = this.pageHeaderMaxHeight;
            //doc.PrintController = this.PrintController;
            doc.PrinterSettings = this.PrinterSettings;
            doc.ReportSetting = this.reportSetting;
            doc.ResetAfterPrint = this.resetAfterPrint;
            doc.SectionData = this.sectionData;
            //doc.ThickPen = this.thickPen;
            //doc.ThinPen = this.thinPen;
            //doc.TotalPages = this.totalPages;
            return doc;

        }

        private PageSettings defaultPageSettings;

        public PageSettings DefaultPageSettings 
        {
            get {return defaultPageSettings ;}
            set { defaultPageSettings=value; } 
        }

        ////
        //// Summary:
        ////     Gets or sets the print controller that guides the printing process.
        ////
        //// Returns:
        ////     The System.Drawing.Printing.PrintController that guides the printing process.
        ////     The default is a new instance of the System.Windows.Forms.PrintControllerWithStatusDialog
        ////     class.
        //[DesignerSerializationVisibility(0)]
        //[Browsable(false)]
        //public PrintController PrintController
        //{
        //    get { return printController; }
        //    set { printController = value; }
        //}
        //private PrintController printController;
        //
        // Summary:
        //     Gets or sets the printer that prints the document.
        //
        // Returns:
        //     A System.Drawing.Printing.PrinterSettings that specifies where and how the
        //     document is printed. The default is a System.Drawing.Printing.PrinterSettings
        //     with its properties set to their default values.
        [DesignerSerializationVisibility(0)]
        [Browsable(false)]
        public PrinterSettings PrinterSettings
        {
            get { return printerSettings; }
            set { printerSettings = value; }
        }
        private PrinterSettings printerSettings;

        //
        // Summary:
        //     Gets or sets the document name to display (for example, in a print status
        //     dialog box or printer queue) while printing the document.
        //
        // Returns:
        //     The document name to display while printing the document. The default is
        //     "document".
        [DefaultValue("document")]
        public string DocumentName
        {
            get { return documentName; }
            set { documentName = value; }
        }
        private string documentName;
        //
        // Summary:
        //     Gets or sets a value indicating whether the position of a graphics object
        //     associated with a page is located just inside the user-specified margins
        //     or at the top-left corner of the printable area of the page.
        //
        // Returns:
        //     true if the graphics origin starts at the page margins; false if the graphics
        //     origin is at the top-left corner of the printable page. The default is false.
        [DefaultValue(false)]
        public bool OriginAtMargins
        {
            get { return originAtMargins; }
            set { originAtMargins = value; }
        }
        private bool originAtMargins;
    

		private ReportSectionData sectionData;

		public ReportSectionData SectionData
		{
			get{return sectionData;}
			set{sectionData=value;}
		}

    
  
 
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

 
        public virtual GraphicsUnit DocumentUnit
        {
            get { return this.documentUnit; }
            set { this.documentUnit = value; }
        }
        #endregion

    
	} // class
}
