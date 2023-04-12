using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using Nistec.Printing.View;

namespace Nistec.Printing.View.Templates
{
	/// <summary>
	/// Summary description for Invoice.
	/// </summary>
    public class InvoiceIL : Nistec.Printing.View.Design.ReportDesign, IInvoice
	{
		private PageHeader secPageHeader;
        private ReportDetail secReportDetail;
		private PageFooter secPageFooter;
		private McTextBox tbxProductName;
		private McLabel lblUnitPrice;
		private McLine RptLine2;
		private McTextBox tbxProductID;
		private McLabel lblQuantity;
		private McTextBox tbxQuantity;
		private McTextBox tbxUnitPrice;
		private McLabel lblPageNo;
        private GroupHeader secGroupHeader;
        private McLabel lblProductID;
        private McLabel lblSumRow;
        private McLabel lblQty;
        private McLabel lblProductName;
        private GroupFooter secGroupFooter;
        private ReportHeader secReportHeader;
        private ReportFooter secReportFooter;
        private McLabel lblCustomerID;
        private McLabel lblAddress;
        private McLabel lblCustomer;
        private McLabel lblID;
        private McTextBox txtCustomer;
        private McTextBox txtCustomerId;
        private McTextBox txtAddress;
        private McTextBox txtDate;
        private McTextBox txtDetails;
        private McLabel lblDetails;
        private McLabel lblDate;
        private McTextBox txtSubTotal;
        private McTextBox tbxSumRow;
        private McLabel lblPrice;
        private McLabel lblSubTotal;
        private McLabel lblTitle;
        private McLabel lblPageFooter;
        private McLabel lblSubTitle;
        private McLine mcLine1;
        private McLabel lblVat;
        private McLabel lblTotal;
        private McTextBox txtTotal;
        private McTextBox txtVat;
        private McLine mcLine2;
        private McLine mcLine3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public InvoiceIL()
            : base()
		{
			/// <summary>
			/// Required for DataReports Designer support
			/// </summary>
			InitializeComponent();
            this.RightToLeft = true;
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
            this.secPageHeader = new Nistec.Printing.View.PageHeader();
            this.secReportDetail = new Nistec.Printing.View.ReportDetail();
            this.tbxSumRow = new Nistec.Printing.View.McTextBox();
            this.tbxUnitPrice = new Nistec.Printing.View.McTextBox();
            this.tbxQuantity = new Nistec.Printing.View.McTextBox();
            this.tbxProductName = new Nistec.Printing.View.McTextBox();
            this.tbxProductID = new Nistec.Printing.View.McTextBox();
            this.secPageFooter = new Nistec.Printing.View.PageFooter();
            this.lblPageNo = new Nistec.Printing.View.McLabel();
            this.secGroupHeader = new Nistec.Printing.View.GroupHeader();
            this.lblPrice = new Nistec.Printing.View.McLabel();
            this.lblProductID = new Nistec.Printing.View.McLabel();
            this.lblSumRow = new Nistec.Printing.View.McLabel();
            this.lblQty = new Nistec.Printing.View.McLabel();
            this.lblProductName = new Nistec.Printing.View.McLabel();
            this.lblQuantity = new Nistec.Printing.View.McLabel();
            this.lblUnitPrice = new Nistec.Printing.View.McLabel();
            this.RptLine2 = new Nistec.Printing.View.McLine();
            this.secGroupFooter = new Nistec.Printing.View.GroupFooter();
            this.txtTotal = new Nistec.Printing.View.McTextBox();
            this.txtVat = new Nistec.Printing.View.McTextBox();
            this.lblVat = new Nistec.Printing.View.McLabel();
            this.lblTotal = new Nistec.Printing.View.McLabel();
            this.lblSubTotal = new Nistec.Printing.View.McLabel();
            this.txtSubTotal = new Nistec.Printing.View.McTextBox();
            this.mcLine2 = new Nistec.Printing.View.McLine();
            this.mcLine3 = new Nistec.Printing.View.McLine();
            this.secReportHeader = new Nistec.Printing.View.ReportHeader();
            this.lblTitle = new Nistec.Printing.View.McLabel();
            this.txtCustomer = new Nistec.Printing.View.McTextBox();
            this.txtCustomerId = new Nistec.Printing.View.McTextBox();
            this.txtAddress = new Nistec.Printing.View.McTextBox();
            this.txtDate = new Nistec.Printing.View.McTextBox();
            this.txtDetails = new Nistec.Printing.View.McTextBox();
            this.lblDetails = new Nistec.Printing.View.McLabel();
            this.lblDate = new Nistec.Printing.View.McLabel();
            this.lblCustomer = new Nistec.Printing.View.McLabel();
            this.lblID = new Nistec.Printing.View.McLabel();
            this.lblAddress = new Nistec.Printing.View.McLabel();
            this.lblCustomerID = new Nistec.Printing.View.McLabel();
            this.lblSubTitle = new Nistec.Printing.View.McLabel();
            this.mcLine1 = new Nistec.Printing.View.McLine();
            this.secReportFooter = new Nistec.Printing.View.ReportFooter();
            this.lblPageFooter = new Nistec.Printing.View.McLabel();
            // 
            // secPageHeader
            // 
            this.secPageHeader.Height = 1F;
            this.secPageHeader.Name = "secPageHeader";
            // 
            // secReportDetail
            // 
            this.secReportDetail.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.tbxSumRow,
            this.tbxUnitPrice,
            this.tbxQuantity,
            this.tbxProductName,
            this.tbxProductID});
            this.secReportDetail.Height = 33F;
            this.secReportDetail.Name = "secReportDetail";
            // 
            // tbxSumRow
            // 
            this.tbxSumRow.DataField = "ExtendedPrice";
            this.tbxSumRow.Height = 24F;
            this.tbxSumRow.Left = 0F;
            this.tbxSumRow.Name = "tbxSumRow";
            this.tbxSumRow.OutputFormat = "₪#,##0.00";
            this.tbxSumRow.Parent = this.secReportDetail;
            this.tbxSumRow.Text = "";
            this.tbxSumRow.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.tbxSumRow.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSumRow.Top = 6F;
            this.tbxSumRow.Width = 102F;
            // 
            // tbxUnitPrice
            // 
            this.tbxUnitPrice.DataField = "UnitPrice";
            this.tbxUnitPrice.Height = 24F;
            this.tbxUnitPrice.Left = 108F;
            this.tbxUnitPrice.Name = "tbxUnitPrice";
            this.tbxUnitPrice.OutputFormat = "₪#,##0.00";
            this.tbxUnitPrice.Parent = this.secReportDetail;
            this.tbxUnitPrice.Text = "";
            this.tbxUnitPrice.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.tbxUnitPrice.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUnitPrice.Top = 6F;
            this.tbxUnitPrice.Width = 78F;
            // 
            // tbxQuantity
            // 
            this.tbxQuantity.DataField = "Quantity";
            this.tbxQuantity.Height = 24F;
            this.tbxQuantity.Left = 192F;
            this.tbxQuantity.Name = "tbxQuantity";
            this.tbxQuantity.Parent = this.secReportDetail;
            this.tbxQuantity.Text = "";
            this.tbxQuantity.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.tbxQuantity.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxQuantity.Top = 6F;
            this.tbxQuantity.Width = 72F;
            // 
            // tbxProductName
            // 
            this.tbxProductName.DataField = "ProductName";
            this.tbxProductName.Height = 24F;
            this.tbxProductName.Left = 270F;
            this.tbxProductName.Name = "tbxProductName";
            this.tbxProductName.Parent = this.secReportDetail;
            this.tbxProductName.RightToLeft = true;
            this.tbxProductName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.tbxProductName.Text = "";
            this.tbxProductName.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxProductName.Top = 6F;
            this.tbxProductName.Width = 240F;
            // 
            // tbxProductID
            // 
            this.tbxProductID.DataField = "ProductID";
            this.tbxProductID.Height = 24F;
            this.tbxProductID.Left = 516F;
            this.tbxProductID.Name = "tbxProductID";
            this.tbxProductID.Parent = this.secReportDetail;
            this.tbxProductID.RightToLeft = true;
            this.tbxProductID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.tbxProductID.Text = "";
            this.tbxProductID.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxProductID.Top = 6F;
            this.tbxProductID.Width = 78F;
            // 
            // secPageFooter
            // 
            this.secPageFooter.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.lblPageNo});
            this.secPageFooter.Height = 40F;
            this.secPageFooter.Name = "secPageFooter";
            this.secPageFooter.Initialize += new System.EventHandler(this.PageFooter_Initialize);
            // 
            // lblPageNo
            // 
            this.lblPageNo.ForeColor = System.Drawing.Color.Navy;
            this.lblPageNo.Height = 24F;
            this.lblPageNo.Left = 204F;
            this.lblPageNo.Name = "lblPageNo";
            this.lblPageNo.Parent = this.secPageFooter;
            this.lblPageNo.Text = "Page No :";
            this.lblPageNo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblPageNo.TextFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageNo.Top = 6F;
            this.lblPageNo.Width = 162F;
            // 
            // secGroupHeader
            // 
            this.secGroupHeader.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.lblPrice,
            this.lblProductID,
            this.lblSumRow,
            this.lblQty,
            this.lblProductName});
            this.secGroupHeader.Height = 36F;
            this.secGroupHeader.Index = 1;
            this.secGroupHeader.Name = "secGroupHeader";
            // 
            // lblPrice
            // 
            this.lblPrice.BackColor = System.Drawing.Color.Navy;
            this.lblPrice.ForeColor = System.Drawing.Color.White;
            this.lblPrice.Height = 24F;
            this.lblPrice.Left = 108F;
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Parent = this.secGroupHeader;
            this.lblPrice.RightToLeft = true;
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblPrice.Text = "מחיר";
            this.lblPrice.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Top = 6F;
            this.lblPrice.Width = 78F;
            // 
            // lblProductID
            // 
            this.lblProductID.BackColor = System.Drawing.Color.Navy;
            this.lblProductID.ForeColor = System.Drawing.Color.White;
            this.lblProductID.Height = 24F;
            this.lblProductID.Left = 516F;
            this.lblProductID.Name = "lblProductID";
            this.lblProductID.Parent = this.secGroupHeader;
            this.lblProductID.RightToLeft = true;
            this.lblProductID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblProductID.Text = "פריט";
            this.lblProductID.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductID.Top = 6F;
            this.lblProductID.Width = 78F;
            // 
            // lblSumRow
            // 
            this.lblSumRow.BackColor = System.Drawing.Color.Navy;
            this.lblSumRow.ForeColor = System.Drawing.Color.White;
            this.lblSumRow.Height = 24F;
            this.lblSumRow.Left = 0F;
            this.lblSumRow.Name = "lblSumRow";
            this.lblSumRow.Parent = this.secGroupHeader;
            this.lblSumRow.RightToLeft = true;
            this.lblSumRow.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblSumRow.Text = "סה\"כ";
            this.lblSumRow.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumRow.Top = 6F;
            this.lblSumRow.Width = 102F;
            // 
            // lblQty
            // 
            this.lblQty.BackColor = System.Drawing.Color.Navy;
            this.lblQty.ForeColor = System.Drawing.Color.White;
            this.lblQty.Height = 24F;
            this.lblQty.Left = 192F;
            this.lblQty.Name = "lblQty";
            this.lblQty.RightToLeft = true;
            this.lblQty.Parent = this.secGroupHeader;
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblQty.Text = "כמות";
            this.lblQty.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Top = 6F;
            this.lblQty.Width = 72F;
            // 
            // lblProductName
            // 
            this.lblProductName.BackColor = System.Drawing.Color.Navy;
            this.lblProductName.ForeColor = System.Drawing.Color.White;
            this.lblProductName.Height = 24F;
            this.lblProductName.Left = 270F;
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Parent = this.secGroupHeader;
            this.lblProductName.RightToLeft = true;
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblProductName.Text = "תאור";
            this.lblProductName.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Top = 6F;
            this.lblProductName.Width = 240F;
            // 
            // RptLine2
            // 
            this.RptLine2.BorderBottomColor = System.Drawing.Color.Transparent;
            this.RptLine2.BorderLeftColor = System.Drawing.Color.Transparent;
            this.RptLine2.BorderRightColor = System.Drawing.Color.Transparent;
            this.RptLine2.BorderTopColor = System.Drawing.Color.Transparent;
            this.RptLine2.LineWeight = 2F;
            this.RptLine2.Name = "RptLine2";
            this.RptLine2.Parent = this.secGroupFooter;
            this.RptLine2.X1 = 48F;
            this.RptLine2.X2 = 546F;
            this.RptLine2.Y1 = 6F;
            this.RptLine2.Y2 = 6F;
            // 
            // secGroupFooter
            // 
            this.secGroupFooter.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.txtTotal,
            this.txtVat,
            this.lblVat,
            this.lblTotal,
            this.lblSubTotal,
            this.txtSubTotal,
            this.mcLine2,
            this.mcLine3});
            this.secGroupFooter.Height = 91F;
            this.secGroupFooter.Name = "secGroupFooter";
            this.secGroupFooter.Initialize += new System.EventHandler(this.GroupFooter_Initialize);
            // 
            // txtTotal
            // 
            this.txtTotal.DataField = "";
            this.txtTotal.Height = 24F;
            this.txtTotal.Left = 0F;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.OutputFormat = "₪#,##0.00";
            this.txtTotal.Parent = this.secGroupFooter;
            this.txtTotal.Text = "";
            this.txtTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.txtTotal.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Top = 66F;
            this.txtTotal.Width = 102F;
            // 
            // txtVat
            // 
            this.txtVat.DataField = "";
            this.txtVat.Height = 24F;
            this.txtVat.Left = 0F;
            this.txtVat.Name = "txtVat";
            this.txtVat.OutputFormat = "₪#,##0.00";
            this.txtVat.Parent = this.secGroupFooter;
            this.txtVat.Text = "";
            this.txtVat.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.txtVat.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVat.Top = 36F;
            this.txtVat.Width = 102F;
            // 
            // lblVat
            // 
            this.lblVat.BackColor = System.Drawing.Color.White;
            this.lblVat.ForeColor = System.Drawing.Color.Navy;
            this.lblVat.Height = 24F;
            this.lblVat.Left = 114F;
            this.lblVat.Name = "lblVat";
            this.lblVat.Parent = this.secGroupFooter;
            this.lblVat.RightToLeft = true;
            this.lblVat.Text = "מעמ:";
            this.lblVat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVat.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVat.Top = 36F;
            this.lblVat.Width = 90F;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.White;
            this.lblTotal.ForeColor = System.Drawing.Color.Navy;
            this.lblTotal.Height = 24F;
            this.lblTotal.Left = 114F;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Parent = this.secGroupFooter;
            this.lblTotal.RightToLeft = true;
            this.lblTotal.Text = "סה\"כ:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotal.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Top = 66F;
            this.lblTotal.Width = 90F;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BackColor = System.Drawing.Color.White;
            this.lblSubTotal.ForeColor = System.Drawing.Color.Navy;
            this.lblSubTotal.Height = 24F;
            this.lblSubTotal.Left = 114F;
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Parent = this.secGroupFooter;
            this.lblSubTotal.RightToLeft = true;
            this.lblSubTotal.Text = "סיכום ביניים:";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSubTotal.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Top = 6F;
            this.lblSubTotal.Width = 90F;
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.DataField = "ExtendedPrice";
            this.txtSubTotal.Height = 24F;
            this.txtSubTotal.Left = 0F;
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.OutputFormat = "₪#,##0.00";
            this.txtSubTotal.Parent = this.secGroupFooter;
            this.txtSubTotal.SummaryFunc = Nistec.Printing.View.AggregateType.Sum;
            this.txtSubTotal.SummaryRunning = Nistec.Printing.View.SummaryRunning.Group;
            this.txtSubTotal.Text = "";
            this.txtSubTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.txtSubTotal.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal.Top = 6F;
            this.txtSubTotal.Width = 102F;
            // 
            // mcLine2
            // 
            this.mcLine2.BorderBottomColor = System.Drawing.Color.Transparent;
            this.mcLine2.BorderLeftColor = System.Drawing.Color.Transparent;
            this.mcLine2.BorderRightColor = System.Drawing.Color.Transparent;
            this.mcLine2.BorderTopColor = System.Drawing.Color.Transparent;
            this.mcLine2.LineWeight = 1F;
            this.mcLine2.Name = "mcLine2";
            this.mcLine2.Parent = this.secGroupFooter;
            this.mcLine2.X1 = 6F;
            this.mcLine2.X2 = 102F;
            this.mcLine2.Y1 = 60F;
            this.mcLine2.Y2 = 60F;
            // 
            // mcLine3
            // 
            this.mcLine3.BorderBottomColor = System.Drawing.Color.Transparent;
            this.mcLine3.BorderLeftColor = System.Drawing.Color.Transparent;
            this.mcLine3.BorderRightColor = System.Drawing.Color.Transparent;
            this.mcLine3.BorderTopColor = System.Drawing.Color.Transparent;
            this.mcLine3.LineWeight = 1F;
            this.mcLine3.Name = "mcLine3";
            this.mcLine3.Parent = this.secGroupFooter;
            this.mcLine3.X1 = 6F;
            this.mcLine3.X2 = 588F;
            this.mcLine3.Y1 = 6F;
            this.mcLine3.Y2 = 6F;
            // 
            // secReportHeader
            // 
            this.secReportHeader.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.lblTitle,
            this.txtCustomer,
            this.txtCustomerId,
            this.txtAddress,
            this.txtDate,
            this.txtDetails,
            this.lblDetails,
            this.lblDate,
            this.lblCustomer,
            this.lblID,
            this.lblAddress,
            this.lblCustomerID,
            this.lblSubTitle,
            this.mcLine1});
            this.secReportHeader.Height = 232F;
            this.secReportHeader.Name = "secReportHeader";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblTitle.Height = 36F;
            this.lblTitle.Left = 6F;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Parent = this.secReportHeader;
            this.lblTitle.RightToLeft = true;
            this.lblTitle.Text = "";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTitle.TextFont = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Top = 6F;
            this.lblTitle.Width = 588F;
            // 
            // txtCustomer
            // 
            this.txtCustomer.DataField = "";
            this.txtCustomer.Height = 24F;
            this.txtCustomer.Left = 6F;
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Parent = this.secReportHeader;
            this.txtCustomer.RightToLeft = true;
            this.txtCustomer.Text = "";
            this.txtCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtCustomer.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomer.Top = 72F;
            this.txtCustomer.Width = 480F;
            // 
            // txtCustomerId
            // 
            this.txtCustomerId.DataField = "";
            this.txtCustomerId.Height = 24F;
            this.txtCustomerId.Left = 6F;
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.Parent = this.secReportHeader;
            this.txtCustomerId.RightToLeft = true;
            this.txtCustomerId.Text = "";
            this.txtCustomerId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtCustomerId.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerId.Top = 102F;
            this.txtCustomerId.Width = 480F;
            // 
            // txtAddress
            // 
            this.txtAddress.DataField = "";
            this.txtAddress.Height = 24F;
            this.txtAddress.Left = 6F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Parent = this.secReportHeader;
            this.txtAddress.RightToLeft = true;
            this.txtAddress.Text = "";
            this.txtAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtAddress.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Top = 132F;
            this.txtAddress.Width = 480F;
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.ForeColor = System.Drawing.Color.Navy;
            this.lblCustomerID.Height = 24F;
            this.lblCustomerID.Left = 312F;
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.RightToLeft = true;
            this.lblCustomerID.Parent = this.secReportHeader;
            this.lblCustomerID.Text = "מספר לקוח:";
            this.lblCustomerID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCustomerID.TextFont = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerID.Top = 258F;
            this.lblCustomerID.Width = 114F;
            // 
            // txtDate
            // 
            this.txtDate.DataField = "";
            this.txtDate.Height = 24F;
            this.txtDate.Left = 6F;
            this.txtDate.Name = "txtDate";
            this.txtDate.OutputFormat = "";
            this.txtDate.RightToLeft = true;
            this.txtDate.Parent = this.secReportHeader;
            this.txtDate.Text = "";
            this.txtDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtDate.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Top = 162F;
            this.txtDate.Width = 480F;
            // 
            // txtDetails
            // 
            this.txtDetails.DataField = "";
            this.txtDetails.Height = 24F;
            this.txtDetails.Left = 6F;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.Parent = this.secReportHeader;
            this.txtDetails.RightToLeft = true;
            this.txtDetails.Text = "";
            this.txtDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtDetails.TextFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetails.Top = 192F;
            this.txtDetails.Width = 480F;
            // 
            // lblDetails
            // 
            this.lblDetails.ForeColor = System.Drawing.Color.Navy;
            this.lblDetails.Height = 24F;
            this.lblDetails.Left = 498F;
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Parent = this.secReportHeader;
            this.lblDetails.RightToLeft = true;
            this.lblDetails.Text = "פרטים :";
            this.lblDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDetails.TextFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetails.Top = 192F;
            this.lblDetails.Width = 96F;
            // 
            // lblDate
            // 
            this.lblDate.ForeColor = System.Drawing.Color.Navy;
            this.lblDate.Height = 24F;
            this.lblDate.Left = 498F;
            this.lblDate.Name = "lblDate";
            this.lblDate.Parent = this.secReportHeader;
            this.lblDate.RightToLeft = true;
            this.lblDate.Text = "תאריך :";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDate.TextFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Top = 162F;
            this.lblDate.Width = 96F;
            // 
            // lblCustomer
            // 
            this.lblCustomer.ForeColor = System.Drawing.Color.Navy;
            this.lblCustomer.Height = 24F;
            this.lblCustomer.Left = 498F;
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Parent = this.secReportHeader;
            this.lblCustomer.RightToLeft = true;
            this.lblCustomer.Text = "לקוח :";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCustomer.TextFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Top = 72F;
            this.lblCustomer.Width = 96F;
            // 
            // lblID
            // 
            this.lblID.ForeColor = System.Drawing.Color.Navy;
            this.lblID.Height = 24F;
            this.lblID.Left = 498F;
            this.lblID.Name = "lblID";
            this.lblID.Parent = this.secReportHeader;
            this.lblID.RightToLeft = true;
            this.lblID.Text = "מספר לקוח :";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblID.TextFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Top = 102F;
            this.lblID.Width = 96F;
            // 
            // lblAddress
            // 
            this.lblAddress.ForeColor = System.Drawing.Color.Navy;
            this.lblAddress.Height = 24F;
            this.lblAddress.Left = 498F;
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Parent = this.secReportHeader;
            this.lblAddress.RightToLeft = true;
            this.lblAddress.Text = "כתובת :";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAddress.TextFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Top = 132F;
            this.lblAddress.Width = 96F;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.BackColor = System.Drawing.Color.White;
            this.lblSubTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblSubTitle.Height = 18F;
            this.lblSubTitle.Left = 6F;
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Parent = this.secReportHeader;
            this.lblSubTitle.RightToLeft = true;
            this.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSubTitle.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.Top = 42F;
            this.lblSubTitle.Width = 588F;
            // 
            // mcLine1
            // 
            this.mcLine1.BorderBottomColor = System.Drawing.Color.Transparent;
            this.mcLine1.BorderLeftColor = System.Drawing.Color.Transparent;
            this.mcLine1.BorderRightColor = System.Drawing.Color.Transparent;
            this.mcLine1.BorderTopColor = System.Drawing.Color.Transparent;
            this.mcLine1.LineWeight = 1F;
            this.mcLine1.Name = "mcLine1";
            this.mcLine1.Parent = this.secReportHeader;
            this.mcLine1.X1 = 12F;
            this.mcLine1.X2 = 594F;
            this.mcLine1.Y1 = 66F;
            this.mcLine1.Y2 = 66F;
            // 
            // secReportFooter
            // 
            this.secReportFooter.Controls.AddRange(new Nistec.Printing.View.McReportControl[] {
            this.lblPageFooter});
            this.secReportFooter.Height = 36F;
            this.secReportFooter.Name = "secReportFooter";
            // 
            // lblPageFooter
            // 
            this.lblPageFooter.BackColor = System.Drawing.Color.White;
            this.lblPageFooter.ForeColor = System.Drawing.Color.Navy;
            this.lblPageFooter.Height = 24F;
            this.lblPageFooter.Left = 6F;
            this.lblPageFooter.Name = "lblPageFooter";
            this.lblPageFooter.Parent = this.secReportFooter;
            this.lblPageFooter.RightToLeft = true;
            this.lblPageFooter.Text = "";
            this.lblPageFooter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblPageFooter.TextFont = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageFooter.Top = 6F;
            this.lblPageFooter.Width = 588F;
            // 
            // mcReport2
            // 
            this.ReportWidth = 597F;
            this.Sections.AddRange(new Nistec.Printing.View.Section[] {
            this.secReportHeader,
            this.secPageHeader,
            this.secGroupHeader,
            this.secReportDetail,
            this.secGroupFooter,
            this.secPageFooter,
            this.secReportFooter});
            this.ReportStart += new EventHandler(Report_ReportStart);

        }

      
        private void InitComplete()
        {
            // 
            // mcReport2
            // 
            this.ReportWidth = 597F;
            this.Sections.AddRange(new Nistec.Printing.View.Section[] {
            this.secReportHeader,
            this.secPageHeader,
            this.secGroupHeader,
            this.secReportDetail,
            this.secGroupFooter,
            this.secPageFooter,
            this.secReportFooter});
            this.ReportStart += new EventHandler(Report_ReportStart);
            //((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
		#endregion

		private int _pageno;
        private float _vat;
		
        void Report_ReportStart(object sender, EventArgs e)
        {
            this._pageno = 1;
        }

        private void GroupFooter_Initialize(object sender, System.EventArgs e)
        {
            decimal subTotal = Types.ToDecimal(txtSubTotal.Value, 0);
            decimal vat = subTotal * (decimal)_vat;
            decimal total = subTotal + vat;
            txtVat.Value = vat;
            txtTotal.Value = total;
        }

		private void PageFooter_Initialize(object sender, System.EventArgs e)
		{
			this.lblPageNo.Text = "Page : " + this._pageno.ToString()+ " of " + this.MaxPages.ToString(); 
			this._pageno++;
		}

        public void AddTitle(string title, string subTitle)
        {
            lblTitle.Text = title;
            lblSubTitle.Text = subTitle;
        }
        public void AddHeader(string customerId, string customrName,string address,string date,string details)
        {
            txtAddress.Value = address;
            txtCustomer.Value = customrName;
            txtCustomerId.Value = customerId;
            txtDate.Value = date;
            txtDetails.Value = details;
        }
        public void AddFooter(string footer, float vat)
        {
            lblPageFooter.Text = footer;
            _vat = vat;
        }

        public InvoiceType InvoiceType
        {
            get { return InvoiceType.InvoiceIL; }
        }
        public Nistec.Printing.View.Design.ReportDesign IReport
        {
            get { return this; }
        }


		
	}
}
