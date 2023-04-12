using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace Nistec.Printing.View.Templates

{
	/// <summary>
    /// Summary description for InvoiceTemplate.
	/// </summary>
    public class InvoiceTemplate : Nistec.Printing.View.Design.ReportDesign
	{
		private Nistec.Printing.View.PageHeader PageHeader;
        private Nistec.Printing.View.ReportDetail ReportDetail;
		private Nistec.Printing.View.PageFooter PageFooter;
		private Nistec.Printing.View.GroupHeader CategoryNameHeader;
		private Nistec.Printing.View.GroupFooter CategoryNameFooter;
		private Nistec.Printing.View.McTextBox tbxCategoryName;
		private Nistec.Printing.View.McTextBox tbxDescription;
		private Nistec.Printing.View.McPicture picPicture;
		private Nistec.Printing.View.McTextBox tbxProductName;
		private Nistec.Printing.View.McLabel lblUnitPrice;
		private Nistec.Printing.View.McLine RptLine2;
		private Nistec.Printing.View.McTextBox tbxProductID;
		private Nistec.Printing.View.McLabel lblQuantityPerUnit;
		private Nistec.Printing.View.McTextBox tbxQuantityPerUnit;
		private Nistec.Printing.View.McTextBox tbxUnitPrice;
		private Nistec.Printing.View.McLabel lblProductName;
		private Nistec.Printing.View.McLabel lblPageFooter;
		private Nistec.Printing.View.McLabel lblPageNo;
		private Nistec.Printing.View.McLabel rptLabel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public InvoiceTemplate()
            : base()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.PageHeader = new Nistec.Printing.View.PageHeader();
            this.ReportDetail = new Nistec.Printing.View.ReportDetail();
            this.tbxUnitPrice = new Nistec.Printing.View.McTextBox();
            this.tbxQuantityPerUnit = new Nistec.Printing.View.McTextBox();
            this.tbxProductName = new Nistec.Printing.View.McTextBox();
            this.tbxProductID = new Nistec.Printing.View.McTextBox();
            this.PageFooter = new Nistec.Printing.View.PageFooter();
            this.lblPageFooter = new Nistec.Printing.View.McLabel();
            this.lblPageNo = new Nistec.Printing.View.McLabel();
            this.CategoryNameHeader = new Nistec.Printing.View.GroupHeader();
            this.rptLabel1 = new Nistec.Printing.View.McLabel();
            this.lblQuantityPerUnit = new Nistec.Printing.View.McLabel();
            this.lblProductName = new Nistec.Printing.View.McLabel();
            this.tbxCategoryName = new Nistec.Printing.View.McTextBox();
            this.tbxDescription = new Nistec.Printing.View.McTextBox();
            this.picPicture = new Nistec.Printing.View.McPicture();
            this.lblUnitPrice = new Nistec.Printing.View.McLabel();
            this.CategoryNameFooter = new Nistec.Printing.View.GroupFooter();
            this.RptLine2 = new Nistec.Printing.View.McLine();
            // 
            // PageHeader
            // 
            this.PageHeader.Height = 0F;
            this.PageHeader.Name = "PageHeader";
            // 
            // ReportDetail
            // 
            this.ReportDetail.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.tbxUnitPrice,
            this.tbxQuantityPerUnit,
            this.tbxProductName,
            this.tbxProductID});
            this.ReportDetail.Height = 35F;
            this.ReportDetail.Name = "ReportDetail";
            // 
            // tbxUnitPrice
            // 
            this.tbxUnitPrice.DataField = "UnitPrice";
            this.tbxUnitPrice.Height = 24F;
            this.tbxUnitPrice.Left = 468F;
            this.tbxUnitPrice.Name = "tbxUnitPrice";
            this.tbxUnitPrice.OutputFormat = "$#,##0.00";
            this.tbxUnitPrice.Parent = this.ReportDetail;
            this.tbxUnitPrice.Text = "UnitPrice";
            this.tbxUnitPrice.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.tbxUnitPrice.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUnitPrice.Top = 6F;
            this.tbxUnitPrice.Width = 78F;
            // 
            // tbxQuantityPerUnit
            // 
            this.tbxQuantityPerUnit.DataField = "QuantityPerUnit";
            this.tbxQuantityPerUnit.Height = 24F;
            this.tbxQuantityPerUnit.Left = 348F;
            this.tbxQuantityPerUnit.Name = "tbxQuantityPerUnit";
            this.tbxQuantityPerUnit.Parent = this.ReportDetail;
            this.tbxQuantityPerUnit.Text = "QuantityPerUnit";
            this.tbxQuantityPerUnit.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxQuantityPerUnit.Top = 6F;
            this.tbxQuantityPerUnit.Width = 114F;
            // 
            // tbxProductName
            // 
            this.tbxProductName.DataField = "ProductName";
            this.tbxProductName.Height = 24F;
            this.tbxProductName.Left = 18F;
            this.tbxProductName.Name = "tbxProductName";
            this.tbxProductName.Parent = this.ReportDetail;
            this.tbxProductName.Text = "ProductName";
            this.tbxProductName.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxProductName.Top = 6F;
            this.tbxProductName.Width = 240F;
            // 
            // tbxProductID
            // 
            this.tbxProductID.DataField = "ProductID";
            this.tbxProductID.Height = 24F;
            this.tbxProductID.Left = 258F;
            this.tbxProductID.Name = "tbxProductID";
            this.tbxProductID.Parent = this.ReportDetail;
            this.tbxProductID.Text = "ProductID:";
            this.tbxProductID.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tbxProductID.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxProductID.Top = 6F;
            this.tbxProductID.Width = 78F;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.lblPageFooter,
            this.lblPageNo});
            this.PageFooter.Height = 66F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.Initialize += new System.EventHandler(this.PageFooter_Initialize);
            // 
            // lblPageFooter
            // 
            this.lblPageFooter.BackColor = System.Drawing.Color.Navy;
            this.lblPageFooter.ForeColor = System.Drawing.Color.White;
            this.lblPageFooter.Height = 24F;
            this.lblPageFooter.Left = 6F;
            this.lblPageFooter.Name = "lblPageFooter";
            this.lblPageFooter.Parent = this.PageFooter;
            this.lblPageFooter.Text = "Northwind Traders  - Fall Catalog";
            this.lblPageFooter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblPageFooter.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageFooter.Top = 6F;
            this.lblPageFooter.Width = 564F;
            // 
            // lblPageNo
            // 
            this.lblPageNo.ForeColor = System.Drawing.Color.Navy;
            this.lblPageNo.Height = 24F;
            this.lblPageNo.Left = 198F;
            this.lblPageNo.Name = "lblPageNo";
            this.lblPageNo.Parent = this.PageFooter;
            this.lblPageNo.Text = "Page No :";
            this.lblPageNo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblPageNo.TextFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageNo.Top = 36F;
            this.lblPageNo.Width = 162F;
            // 
            // CategoryNameHeader
            // 
            this.CategoryNameHeader.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.rptLabel1,
            this.lblQuantityPerUnit,
            this.lblProductName,
            this.tbxCategoryName,
            this.tbxDescription,
            this.picPicture,
            this.lblUnitPrice});
            this.CategoryNameHeader.GroupKeepTogether = Nistec.Printing.View.GroupKeepTogether.All;
            this.CategoryNameHeader.Height = 184F;
            this.CategoryNameHeader.Index = 2;
            this.CategoryNameHeader.Name = "CategoryNameHeader";
            // 
            // rptLabel1
            // 
            this.rptLabel1.BackColor = System.Drawing.Color.Navy;
            this.rptLabel1.ForeColor = System.Drawing.Color.White;
            this.rptLabel1.Height = 24F;
            this.rptLabel1.Left = 264F;
            this.rptLabel1.Name = "rptLabel1";
            this.rptLabel1.Parent = this.CategoryNameHeader;
            this.rptLabel1.Text = "ProductID:";
            this.rptLabel1.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rptLabel1.Top = 156F;
            this.rptLabel1.Width = 78F;
            // 
            // lblQuantityPerUnit
            // 
            this.lblQuantityPerUnit.BackColor = System.Drawing.Color.Navy;
            this.lblQuantityPerUnit.ForeColor = System.Drawing.Color.White;
            this.lblQuantityPerUnit.Height = 24F;
            this.lblQuantityPerUnit.Left = 348F;
            this.lblQuantityPerUnit.Name = "lblQuantityPerUnit";
            this.lblQuantityPerUnit.Parent = this.CategoryNameHeader;
            this.lblQuantityPerUnit.Text = "QuantityPerUnit:";
            this.lblQuantityPerUnit.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantityPerUnit.Top = 156F;
            this.lblQuantityPerUnit.Width = 114F;
            // 
            // lblProductName
            // 
            this.lblProductName.BackColor = System.Drawing.Color.Navy;
            this.lblProductName.ForeColor = System.Drawing.Color.White;
            this.lblProductName.Height = 24F;
            this.lblProductName.Left = 18F;
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Parent = this.CategoryNameHeader;
            this.lblProductName.Text = "Product Name:";
            this.lblProductName.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Top = 156F;
            this.lblProductName.Width = 240F;
            // 
            // tbxCategoryName
            // 
            this.tbxCategoryName.DataField = "CategoryName";
            this.tbxCategoryName.ForeColor = System.Drawing.Color.Navy;
            this.tbxCategoryName.Height = 30F;
            this.tbxCategoryName.Left = 48F;
            this.tbxCategoryName.Name = "tbxCategoryName";
            this.tbxCategoryName.Parent = this.CategoryNameHeader;
            this.tbxCategoryName.Text = "CategoryName";
            this.tbxCategoryName.TextFont = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCategoryName.Top = 12F;
            this.tbxCategoryName.Width = 180F;
            // 
            // tbxDescription
            // 
            this.tbxDescription.DataField = "Description";
            this.tbxDescription.ForeColor = System.Drawing.Color.Navy;
            this.tbxDescription.Height = 96F;
            this.tbxDescription.Left = 48F;
            this.tbxDescription.Name = "tbxDescription";
            this.tbxDescription.Parent = this.CategoryNameHeader;
            this.tbxDescription.Text = "Description";
            this.tbxDescription.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDescription.Top = 48F;
            this.tbxDescription.Width = 180F;
            // 
            // picPicture
            // 
            this.picPicture.DataField = "Picture";
            this.picPicture.Height = 108F;
            this.picPicture.Left = 402F;
            this.picPicture.Name = "picPicture";
            this.picPicture.Parent = this.CategoryNameHeader;
            this.picPicture.SizeMode = Nistec.Printing.View.SizeMode.Stretch;
            this.picPicture.Top = 12F;
            this.picPicture.Width = 144F;
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.BackColor = System.Drawing.Color.Navy;
            this.lblUnitPrice.ForeColor = System.Drawing.Color.White;
            this.lblUnitPrice.Height = 24F;
            this.lblUnitPrice.Left = 468F;
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Parent = this.CategoryNameHeader;
            this.lblUnitPrice.Text = "Unit Price:";
            this.lblUnitPrice.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitPrice.Top = 156F;
            this.lblUnitPrice.Width = 78F;
            // 
            // CategoryNameFooter
            // 
            this.CategoryNameFooter.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.RptLine2});
            this.CategoryNameFooter.Height = 11F;
            this.CategoryNameFooter.Name = "CategoryNameFooter";
            // 
            // RptLine2
            // 
            this.RptLine2.BorderBottomColor = System.Drawing.Color.Transparent;
            this.RptLine2.BorderLeftColor = System.Drawing.Color.Transparent;
            this.RptLine2.BorderRightColor = System.Drawing.Color.Transparent;
            this.RptLine2.BorderTopColor = System.Drawing.Color.Transparent;
            this.RptLine2.LineWeight = 2F;
            this.RptLine2.Name = "RptLine2";
            this.RptLine2.Parent = this.CategoryNameFooter;
            this.RptLine2.X1 = 48F;
            this.RptLine2.X2 = 546F;
            this.RptLine2.Y1 = 6F;
            this.RptLine2.Y2 = 6F;
            // 
            // mcReport1
            // 
            this.ReportWidth = 578F;
            this.Sections.AddRange(new Nistec.Printing.View.Section[] {
            this.PageHeader,
            this.ReportDetail,
            this.PageFooter});
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		private int _pageno;
		private void Catalog_ReportStart(object sender, System.EventArgs e)
		{
			this._pageno = 1;
		}

		
		private void PageFooter_Initialize(object sender, System.EventArgs e)
		{
			this.lblPageNo.Text = "Page : " + this._pageno.ToString()+ " of " + this.MaxPages.ToString(); 
			this._pageno++;
		}


		
	}
}
