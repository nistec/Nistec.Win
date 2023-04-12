using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;

namespace MControl.ReportView.Templates
{
	/// <summary>
    /// Simple Template Report with Page header and Footer and also pager label.
	/// </summary>
	public class ReportTemplate : MControl.ReportView.Design.Report
	{
		#region constructor

        private MControl.ReportView.GroupHeader GroupHeader;
        private MControl.ReportView.PageHeader PageHeader;
        private MControl.ReportView.PageFooter PageFooter;
		private MControl.ReportView.McLabel lblPageNo;
		private MControl.ReportView.ReportDetail ReportDetail;
        private MControl.ReportView.ReportHeader ReportHeader;
        private MControl.ReportView.ReportFooter ReportFooter;
        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ReportTemplate()
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
            this.ReportHeader = new MControl.ReportView.ReportHeader();
            this.ReportFooter = new MControl.ReportView.ReportFooter();
            this.ReportDetail = new MControl.ReportView.ReportDetail();
            this.GroupHeader = new MControl.ReportView.GroupHeader();
            this.PageHeader = new MControl.ReportView.PageHeader();
            this.PageFooter = new MControl.ReportView.PageFooter();
			this.lblPageNo = new MControl.ReportView.McLabel();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // ReportHeader
            // 
            this.ReportHeader.Height = 0F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.NewPage = NewPage.None;
            this.ReportHeader.Visible = false;
            // 
			// ReportDetail
			// 
			//this.ReportDetail.AlternatingColor = System.Drawing.Color.RosyBrown;
			this.ReportDetail.Height = 50F;
			this.ReportDetail.Name = "ReportDetail";
			// 
            // GroupHeader
			// 
            this.GroupHeader.Height = 47F;
            this.GroupHeader.Name = "GroupHeader";
            this.GroupHeader.NewPage = NewPage.None;
            this.GroupHeader.GroupKeepTogether = GroupKeepTogether.All;
            //this.PageHeader.Visible = false;
            // 
            // PageHeader
            // 
            this.PageHeader.Height = 0F;
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
            // ReportFooter
            // 
            this.ReportFooter.Height = 0F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.PrintAtBottom = true;
            this.ReportFooter.Visible = false;
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
																	 this.ReportHeader,
																	 this.PageHeader,
                                                                     this.GroupHeader,
																	 this.ReportDetail,
																	 this.PageFooter,
                                                                     this.ReportFooter});
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

        public void AddHeader(string s)//, System.Drawing.Font font)
        {
            //int index= this.Sections["ReportHeader"].Controls.Count;

            //McControlBuilder.CreateRptLable(index,s

            //McControlCollection controls=   this.Sections["PageHeader"].Controls;

            //foreach(McReportControl c in controls)
            //{
            //    c.Top
            //}
            if (string.IsNullOrEmpty(s))
                return;

            System.Drawing.Size size = MControl.Drawing.TextUtils.GetTextSize(this.Document.Graphics, s, McReportControl.DefaultFont);

            float ctlWidth = this.ReportWidth;
            float ctlHeight = (float)size.Height;

            McLabel lbl = McControlBuilder.CreateRptLable(0, s, 0, 0, ctlWidth, ctlHeight);
            //lbl.BackColor = System.Drawing.Color.Navy;
            //lbl.ForeColor = System.Drawing.Color.White;
            lbl.Parent = this.Sections["ReportHeader"];
            this.Sections["ReportHeader"].Controls.Add(lbl);
            this.Sections["ReportHeader"].Height = 50f;
            this.Sections["ReportHeader"].Visible = true;

            //McTextBox rt = new McTextBox();
            ////rt.MultiLine = true;
            //rt.CanGrow = true;
            //rt.Text = s;
            //rt.Parent = this.Sections["ReportHeader"];
            //this.Sections["ReportHeader"].Controls.Add(rt);

        }

        public void AddFooter(string s)//, System.Drawing.Font font)
        {
            //int index= this.Sections["ReportHeader"].Controls.Count;

            //McControlBuilder.CreateRptLable(index,s

            //McControlCollection controls=   this.Sections["PageHeader"].Controls;

            //foreach(McReportControl c in controls)
            //{
            //    c.Top
            //}

            if (string.IsNullOrEmpty(s))
                return;

            System.Drawing.Size size = MControl.Drawing.TextUtils.GetTextSize(this.Document.Graphics, s, McReportControl.DefaultFont);

            float ctlWidth = this.ReportWidth;
            float ctlHeight = (float)size.Height;

            McLabel lbl = McControlBuilder.CreateRptLable(0, s, 0, 0, ctlWidth, ctlHeight);
            //lbl.BackColor = System.Drawing.Color.Navy;
            //lbl.ForeColor = System.Drawing.Color.White;
            lbl.Parent = this.Sections["ReportFooter"];
            this.Sections["ReportFooter"].Controls.Add(lbl);
            this.Sections["ReportFooter"].Height = 50f;
            this.Sections["ReportFooter"].Visible = true;

            //McTextBox rt = new McTextBox();
            ////rt.MultiLine = true;
            //rt.CanGrow = true;
            //rt.Text = s;
            //rt.Parent = this.Sections["ReportHeader"];
            //this.Sections["ReportHeader"].Controls.Add(rt);

        }

	}
}
