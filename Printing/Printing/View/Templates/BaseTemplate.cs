using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Drawing;

namespace Nistec.Printing.View.Templates
{
	/// <summary>
    /// Simple Template Report with Page header and Footer and also pager label.
	/// </summary>
	public class BaseTemplate : Nistec.Printing.View.Design.ReportDesign
	{
		#region constructor

        private Nistec.Printing.View.GroupHeader GroupHeader;
        private Nistec.Printing.View.PageHeader PageHeader;
        private Nistec.Printing.View.PageFooter PageFooter;
		private Nistec.Printing.View.McLabel lblPageNo;
		private Nistec.Printing.View.ReportDetail ReportDetail;
        private Nistec.Printing.View.ReportHeader ReportHeader;
        private Nistec.Printing.View.ReportFooter ReportFooter;
        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public BaseTemplate()
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
            this.ReportHeader = new Nistec.Printing.View.ReportHeader();
            this.ReportFooter = new Nistec.Printing.View.ReportFooter();
            this.ReportDetail = new Nistec.Printing.View.ReportDetail();
            this.GroupHeader = new Nistec.Printing.View.GroupHeader();
            this.PageHeader = new Nistec.Printing.View.PageHeader();
            this.PageFooter = new Nistec.Printing.View.PageFooter();
			this.lblPageNo = new Nistec.Printing.View.McLabel();
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
            this.PageFooter.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
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
			//this.ReportWidth = 577F;
            this.Orientation = PageOrientation.Default;
			this.ScriptLanguage = Nistec.Printing.View.ScriptLanguage.CSharp;
			this.Sections.AddRange(new Nistec.Printing.View.Section[] {
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

        private System.Drawing.SizeF GetStringSize(string s, Font font)
        {
            Rectangle rec=new Rectangle(0,0,(int)ReportWidth,50);
            SizeF size = Nistec.Drawing.TextUtils.GetTextSize(this.Document.Graphics, s, font,ref rec, Nistec.Win32.DrawTextFormatFlags.DT_CALCRECT);

            //float height= McReportControl.DefaultFont.GetHeight(ReportUtil.Dpi);
            //string[] lines = s.Split(new string[]{"\r\n"},  StringSplitOptions.None);
            //int rows = lines.Length + 1;
            //size.Height+= rows * height;

            return size;
        }

        private float GetTop(Section sec, float space)
        {
            float top = 0;
            foreach (McReportControl c in sec.Controls)
            {
                top = Math.Max(top, c.Top + c.Height);
            }
            if (top <= 0)
                return 0;
            return top + space;
        }

        public void AddLabel(string sec, string s, bool RightToLeft)
        {
            ContentAlignment align = RightToLeft ? ContentAlignment.TopRight : ContentAlignment.TopLeft;
            AddLabel(this.Sections[sec], s, McReportControl.DefaultFont, Color.Black, align, RightToLeft);
        }

        public void AddLabel(string sec, string s, Font font, Color foreColor, ContentAlignment align, bool RightToLeft)
        {
            AddLabel(this.Sections[sec], s, McReportControl.DefaultFont, Color.Black, align, RightToLeft);
        }

        public void AddLabel(Section sec, string s, Font font,Color foreColor, ContentAlignment align, bool RightToLeft)
        {
            if (string.IsNullOrEmpty(s))
                return;

            SizeF size = GetStringSize(s, font);
            int index = sec.Controls.Count;
            
            //McReportControl control=  sec.Controls[index - 1];
            
            float y = GetTop(sec,0f);// control.Top + control.Height + 5f;

            McLabel ctl = new McLabel();
            ctl.Name = "Label" + index.ToString();
            ctl.Text = s;
            ctl.TextFont = font;// new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            ctl.TextAlign = align;
            ctl.ForeColor = foreColor;
            ctl.RightToLeft = RightToLeft;
            ctl.Left = 0;
            ctl.Top = y;
            ctl.Width = size.Width;
            ctl.Height = size.Height;
            ctl.Parent = sec;
            sec.Controls.Add(ctl);
            sec.Height += size.Height + 10f;
            sec.Visible = true;
        }

        public void AddHeader(string s)//, System.Drawing.Font font)
        {
            if (string.IsNullOrEmpty(s))
                return;

            ReportHeader.NewPage = NewPage.After;
            ReportHeader.Visible = true;
            AddLabel("ReportHeader", s, this.RightToLeft);

        }

        public void AddFooter(string s)//, System.Drawing.Font font)
        {

            if (string.IsNullOrEmpty(s))
                return;

            ReportHeader.NewPage = NewPage.After;
            ReportHeader.Visible = true;
            AddLabel("ReportFooter", s, this.RightToLeft);

        }

	}
}
