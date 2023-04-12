
using System;

namespace Nistec.Printing
{
	/// <summary>
	/// An interface to be used in the Strategy design pattern.
	/// McPrintDocument will call the single method CreateDocument()
	/// when it wants to be formatted and made into a complete document.
	/// </summary>
	public interface IReportDocument
	{
        /// <summary>
        /// This function is called prior to printing.
        /// The implementer of this function is passed a handle
        /// to a McPrintDocument object that needs to be setup.
        /// </summary>
        /// <param name="rpt">Handle to the McPrintDocument object</param>
        /// <remarks>
        /// It is desirable to call printDocument.ClearSections() before
        /// adding new sections to the printDocument.
        /// </remarks>
        void CreateDocument(McPrintDocument rpt);
        
	}
}
