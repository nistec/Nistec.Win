using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;

namespace MControl.ReportView.Templates
{
	/// <summary>
    /// Simple Report with Page header and Footer and also pager label.
	/// </summary>
	public class ReportPager : MControl.Printing.View.Design.Report
	{
		#region constructor

		private MControl.ReportView.PageHeader PageHeader;
		private MControl.ReportView.PageFooter PageFooter;
		private MControl.ReportView.McLabel lblPageNo;
		private MControl.ReportView.Detail Detail;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ReportPager()
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
			this.lblPageNo = new MControl.ReportView.McLabel();
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
            this.PageFooter.Controls.AddRange(new MControl.ReportView.McReportControl[] {
																				   this.lblPageNo});
			this.PageFooter.Height = 44F;
			this.PageFooter.Name = "PageFooter";
			this.PageFooter.Initialize += new System.EventHandler(this.PageFooter_Initialize);
			// 
			// lblPageNo
			// 
			this.lblPageNo.ForeColor = System.Drawing.Color.Navy;
			this.lblPageNo.Height = 25.8F;
			this.lblPageNo.Left = 6F;
			this.lblPageNo.Name = "lblPageNo";
			this.lblPageNo.Parent = this.PageFooter;
			this.lblPageNo.Text = "Page No :";
			this.lblPageNo.TextFont = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPageNo.Top = 6F;
			this.lblPageNo.Width = 126F;
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
			this.ReportStart += new System.EventHandler(this.DataReport_ReportStart);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		#region Report Events

		private int pageno;

		private void DataReport_ReportStart(object sender, EventArgs e)
		{
			this.pageno = 1;
		}

		private void PageFooter_Initialize(object sender, EventArgs e)
		{
			this.lblPageNo.Text = "Page : " + this.pageno.ToString()+ " of " + this.MaxPages.ToString(); 
			this.pageno++;

		}

		#endregion

	}
}
