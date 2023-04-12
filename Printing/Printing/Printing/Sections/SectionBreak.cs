
using System;
using System.Drawing;
using System.Diagnostics;
using Nistec.Printing.Drawing;

namespace Nistec.Printing.Sections
{
	/// <summary>
	/// SectionBreak is used to create page breaks, column breaks,
	/// and other breaks to flow like that.
	/// </summary>
	public class SectionBreak : ReportSection
	{

        /// <summary>
        /// Constructor for a section that breaks to next page
        /// </summary>
        public SectionBreak() : this (true)
        {
            requiresNonEmptyBounds = false;
        }


        /// <summary>
        /// Constructor for a section
        /// </summary>
        /// <param name="pageBreak">True to break for a page,
        /// false to just break for, say, a column or a line.</param>
        public SectionBreak(bool pageBreak)
        {
            this.PageBreak = pageBreak;
        }

        bool pageBreak;
        int pageNumber;
        bool firstTimeCalled;

        /// <summary>
        /// Gets or sets if this break will cause a new page,
        /// or simply fill up the rest of this section
        /// </summary>
        public bool PageBreak
        {
            get { return this.pageBreak; }
            set { this.pageBreak = value; }
        }


        /// <summary>
        /// This method is called after a size and before a print if
        /// the bounds have changed between the sizing and the printing.
        /// Override this function to update anything based on the new location
        /// </summary>
        /// <param name="originalBounds">Bounds originally passed for sizing</param>
        /// <param name="newBounds">New bounds for printing</param>
        /// <returns>SectionSizeValues for the new values of size, fits, continued</returns>
        protected override SectionSizeValues BoundsChanged (
            Bounds originalBounds,
            Bounds newBounds)
        {
            this.ResetSize();
            return base.BoundsChanged (originalBounds, newBounds);
        }

        /// <summary>
        /// This method is used to perform any required initialization.
        /// </summary>
        /// <param name="g">Graphics object to print on.</param>
        protected override void DoBeginPrint (
            Graphics g
            )
        {
            this.firstTimeCalled = true;
        }

        /// <summary>
        /// Called to calculate the size that this section requires on
        /// the next call to Print.  This method will be called once
        /// prior to each call to Print.  
        /// </summary>
        /// <param name="printDocument">The parent McPrintDocument that is printing.</param>
        /// <param name="g">Graphics object to print on.</param>
        /// <param name="bounds">Bounds of the area to print within.
        /// The bounds passed already takes the margins into account - so you cannot
        /// print or do anything within these margins.
        /// </param>
        /// <returns>The values for RequireSize, Fits and Continues for this section.</returns>
        protected override SectionSizeValues DoCalcSize (
            McPrintDocument printDocument,
            Graphics g,
            Bounds bounds
            )
        {
            SectionSizeValues retval = new SectionSizeValues();
            retval.Fits = true;
            int page = printDocument.GetCurrentPage();
//            Debug.WriteLine ("SectionBreak DoCalcSize");
//            Debug.WriteLine ("   PageBreak: " + this.pageBreak + ",  PageNumber: " + page);
//            Debug.WriteLine ("   Bounds: " + bounds);

            if (this.firstTimeCalled)
            {
                this.pageNumber = page;
                retval.Continued = this.pageBreak;
                retval.RequiredSize = bounds.GetSizeF();
                this.firstTimeCalled = false;
            }
            else
            {
                if (this.pageBreak && (page == this.pageNumber))
                {
                    retval.Continued = true;
                    retval.RequiredSize = bounds.GetSizeF();
                }
                else
                {
                    retval.Continued = false;
                    retval.RequiredSize = new SizeF (0, 0);
                }
            }
            return retval;
        }

        /// <summary>
        /// Called to actually print this section.  
        /// The DoCalcSize method will be called once prior to each
        /// call of DoPrint.
        /// DoPrint is not called if DoCalcSize sets fits to false.
        /// It should obey the values of Size and Continued as set by
        /// DoCalcSize().
        /// </summary>
        /// <param name="printDocument">The parent McPrintDocument that is printing.</param>
        /// <param name="g">Graphics object to print on.</param>
        /// <param name="bounds">Bounds of the area to print within.
        /// These bounds already take the margins into account.
        /// </param>
        protected override void DoPrint (
            McPrintDocument printDocument,
            Graphics g,
            Bounds bounds
            )
        {
            // nothing to print
        }



	}
}
