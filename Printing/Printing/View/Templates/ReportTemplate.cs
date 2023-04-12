using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;

namespace Nistec.Printing.View.Templates
{
	/// <summary>
    /// Simple Template Report with Page header and Footer and also pager label.
	/// </summary>
	public class ReportTemplate : BaseTemplate
	{
		#region constructor

 
        public ReportTemplate()
		{
			/// <summary>
			/// Required for DataReports Designer support
			/// </summary>
			//InitializeComponent();
            //base.ReportWidth = 577F;
            //base.Orientation = PageOrientation.Default;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
        public ReportTemplate(PageOrientation orientation)
        {
            /// <summary>
            /// Required for DataReports Designer support
            /// </summary>
            //InitializeComponent();
            //base.ReportWidth = 577F;
            base.Orientation = orientation;
            if (orientation == PageOrientation.Landscape)
            {
                base.ReportWidth = 948F;//948
            }
            else
            {
                base.ReportWidth = 610F;//610
            }
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#endregion
	
	}
}
