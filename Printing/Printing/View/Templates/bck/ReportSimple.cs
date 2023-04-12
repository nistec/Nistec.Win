using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;

namespace MControl.ReportView.Templates
{
	/// <summary>
    /// Simple Report with Page header and footer.
	/// </summary>
	public class ReportSimple : MControl.ReportView.Design.Report
	{
		#region constructor

		private MControl.ReportView.PageHeader PageHeader;
		private MControl.ReportView.PageFooter PageFooter;
		private MControl.ReportView.Detail Detail;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ReportSimple()
		{
			/// <summary>
			/// Required for DataReports Designer support
			/// </summary>
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#endregion

		#region DataReports Designer generated code
		/// <summary>
		/// Required method for DataReports Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Detail = new MControl.ReportView.Detail();
			this.PageHeader = new MControl.ReportView.PageHeader();
			this.PageFooter = new MControl.ReportView.PageFooter();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// Detail
			// 
			//this.ReportDetail.AlternatingColor = System.Drawing.Color.RosyBrown;
			this.Detail.Height = 50F;
			this.Detail.Name = "Detail";
			// 
			// PageHeader
			// 
			this.PageHeader.Height = 47F;
			this.PageHeader.Name = "PageHeader";
			this.PageHeader.Visible = false;
			// 
			// PageFooter
			// 
			this.PageFooter.Height = 44F;
			this.PageFooter.Name = "PageFooter";
			// 
            // ReportSimple
			// 
			this.DefaultPaperSize = false;
			this.PaperKind = System.Drawing.Printing.PaperKind.A4;
			this.ReportWidth = 577F;
			this.ScriptLanguage = MControl.ReportView.ScriptLanguage.CSharp;
			this.Sections.AddRange(new MControl.ReportView.Section[] {
																	 this.PageHeader,
																	 this.Detail,
																	 this.PageFooter});
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion


	}
}
