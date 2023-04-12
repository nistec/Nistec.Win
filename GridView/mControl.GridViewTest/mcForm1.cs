using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using mControl.WinCtl.Controls;
using mControl.GridView;

using mControl.NavBar.Data.Ole;

namespace mControl.Samples
{
	/// <summary>
	/// Summary description for Form3.
	/// </summary>
	public class mcForm1 : mControl.WinCtl.Forms.CtlForm
    {
		private mControl.GridView.GridComboColumn CustomerID;
		private mControl.GridView.GridDateColumn OrderDate;
		private mControl.GridView.GridDateColumn ShippedDate;
		private mControl.GridView.GridComboColumn ShipVia;
		private mControl.GridView.GridTextColumn Freight;
		private mControl.GridView.VGridColumn OrderID;

		private mControl.WinCtl.Controls.CtlButton ctlButton1;
		private mControl.WinCtl.Controls.CtlButton ctlButton2;
		private mControl.WinCtl.Controls.CtlButton ctlButton3;
		private mControl.WinCtl.Controls.CtlButton ctlButton4;
        private mControl.GridView.Grid grid1;
        private CtlButton ctlButton5;
        private mControl.GridView.GridButtonColumn gridBtn;
        private CtlButton btnCurrent;
        private CtlButton btnVirtual;
        private PopUpItem popUpItem1;
        private PopUpItem popUpItem2;
        private PopUpItem popUpItem3;
		//private mControl.GridView.GridTableStyle gridTableStyle1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public mcForm1()
		{
			//
			// Required for Windows Form Designer support
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mcForm1));
            this.ctlButton1 = new mControl.WinCtl.Controls.CtlButton();
            this.ctlButton2 = new mControl.WinCtl.Controls.CtlButton();
            this.ctlButton3 = new mControl.WinCtl.Controls.CtlButton();
            this.ctlButton4 = new mControl.WinCtl.Controls.CtlButton();
            this.ctlButton5 = new mControl.WinCtl.Controls.CtlButton();
            this.btnCurrent = new mControl.WinCtl.Controls.CtlButton();
            this.btnVirtual = new mControl.WinCtl.Controls.CtlButton();
            this.popUpItem1 = new mControl.WinCtl.Controls.PopUpItem();
            this.popUpItem2 = new mControl.WinCtl.Controls.PopUpItem();
            this.popUpItem3 = new mControl.WinCtl.Controls.PopUpItem();
            this.grid1 = new mControl.GridView.Grid();
            this.OrderID = new mControl.GridView.VGridColumn();
            this.CustomerID = new mControl.GridView.GridComboColumn();
            this.OrderDate = new mControl.GridView.GridDateColumn();
            this.ShippedDate = new mControl.GridView.GridDateColumn();
            this.ShipVia = new mControl.GridView.GridComboColumn();
            this.Freight = new mControl.GridView.GridTextColumn();
            this.gridBtn = new mControl.GridView.GridButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.caption.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Visual;
            this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.Name = "caption";
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.SubTitleColor = System.Drawing.SystemColors.ControlText;
            this.caption.Text = "Grid Sample";
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.StyleGuideBase.StylePlan = mControl.WinCtl.Controls.Styles.Desktop;
            // 
            // ctlButton1
            // 
            this.ctlButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton1.Location = new System.Drawing.Point(491, 304);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(71, 24);
            this.ctlButton1.StylePainter = this.StyleGuideBase;
            this.ctlButton1.TabIndex = 3;
            this.ctlButton1.Text = "Print";
            this.ctlButton1.Click += new System.EventHandler(this.ctlButton1_Click);
            // 
            // ctlButton2
            // 
            this.ctlButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton2.Location = new System.Drawing.Point(568, 304);
            this.ctlButton2.Name = "ctlButton2";
            this.ctlButton2.Size = new System.Drawing.Size(71, 24);
            this.ctlButton2.StylePainter = this.StyleGuideBase;
            this.ctlButton2.TabIndex = 4;
            this.ctlButton2.Text = "Export";
            this.ctlButton2.Click += new System.EventHandler(this.ctlButton2_Click);
            // 
            // ctlButton3
            // 
            this.ctlButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton3.Location = new System.Drawing.Point(645, 304);
            this.ctlButton3.Name = "ctlButton3";
            this.ctlButton3.Size = new System.Drawing.Size(71, 24);
            this.ctlButton3.StylePainter = this.StyleGuideBase;
            this.ctlButton3.TabIndex = 6;
            this.ctlButton3.Text = "Filter";
            this.ctlButton3.Click += new System.EventHandler(this.ctlButton3_Click);
            // 
            // ctlButton4
            // 
            this.ctlButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ctlButton4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton4.Location = new System.Drawing.Point(12, 304);
            this.ctlButton4.Name = "ctlButton4";
            this.ctlButton4.Size = new System.Drawing.Size(104, 24);
            this.ctlButton4.StylePainter = this.StyleGuideBase;
            this.ctlButton4.TabIndex = 7;
            this.ctlButton4.Text = "Adjust Columns";
            this.ctlButton4.Click += new System.EventHandler(this.ctlButton4_Click);
            // 
            // ctlButton5
            // 
            this.ctlButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlButton5.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton5.Location = new System.Drawing.Point(414, 304);
            this.ctlButton5.Name = "ctlButton5";
            this.ctlButton5.Size = new System.Drawing.Size(71, 24);
            this.ctlButton5.StylePainter = this.StyleGuideBase;
            this.ctlButton5.TabIndex = 9;
            this.ctlButton5.Text = "Find";
            this.ctlButton5.Click += new System.EventHandler(this.ctlButton5_Click);
            // 
            // btnCurrent
            // 
            this.btnCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCurrent.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCurrent.Location = new System.Drawing.Point(122, 304);
            this.btnCurrent.Name = "btnCurrent";
            this.btnCurrent.Size = new System.Drawing.Size(71, 24);
            this.btnCurrent.StylePainter = this.StyleGuideBase;
            this.btnCurrent.TabIndex = 10;
            this.btnCurrent.Text = "Current";
            this.btnCurrent.Click += new System.EventHandler(this.btnCurrent_Click);
            // 
            // btnVirtual
            // 
            this.btnVirtual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVirtual.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnVirtual.Location = new System.Drawing.Point(199, 304);
            this.btnVirtual.Name = "btnVirtual";
            this.btnVirtual.Size = new System.Drawing.Size(71, 24);
            this.btnVirtual.StylePainter = this.StyleGuideBase;
            this.btnVirtual.TabIndex = 11;
            this.btnVirtual.Text = "Virtual";
            this.btnVirtual.Click += new System.EventHandler(this.btnVirtual_Click);
            // 
            // popUpItem1
            // 
            this.popUpItem1.Text = "item1";
            // 
            // popUpItem2
            // 
            this.popUpItem2.Text = "item2";
            // 
            // popUpItem3
            // 
            this.popUpItem3.Text = "item3";
            // 
            // grid1
            // 
            this.grid1.AutoAdjust = true;
            this.grid1.BackColor = System.Drawing.Color.White;
            this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid1.CaptionText = "abc";
            this.grid1.Columns.AddRange(new mControl.GridView.GridColumnStyle[] {
            this.OrderID,
            this.CustomerID,
            this.OrderDate,
            this.ShippedDate,
            this.ShipVia,
            this.Freight,
            this.gridBtn});
            this.grid1.DataMember = "";
            this.grid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.grid1.ForeColor = System.Drawing.Color.Black;
            this.grid1.GridLineStyle = mControl.GridView.GridLineStyle.Solid;
            this.grid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid1.Location = new System.Drawing.Point(0, 56);
            this.grid1.Name = "grid1";
            this.grid1.PaintAlternating = false;
            this.grid1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grid1.Size = new System.Drawing.Size(728, 232);
            this.grid1.StylePainter = this.StyleGuideBase;
            this.grid1.TabIndex = 8;
            this.grid1.ButtonClick += new mControl.GridView.ButtonClickEventHandler(this.grid1_ButtonClick);
            // 
            // OrderID
            // 
            this.OrderID.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.OrderID.AllowNull = false;
            this.OrderID.Format = "";
            this.OrderID.HeaderText = "OrderID";
            this.OrderID.IsBound = false;
            this.OrderID.MappingName = "";
            this.OrderID.NullText = "";
            this.OrderID.Text = "Details";
            this.OrderID.Width = 94;
            // 
            // CustomerID
            // 
            this.CustomerID.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.CustomerID.AllowNull = false;
            this.CustomerID.AutoAdjust = true;
            this.CustomerID.DisplayMember = "CompanyName";
            this.CustomerID.Format = "";
            this.CustomerID.HeaderText = "CustomerID";
            this.CustomerID.MappingName = "CustomerID";
            this.CustomerID.ValueMember = "CustomerID";
            this.CustomerID.Width = 148;
            // 
            // OrderDate
            // 
            this.OrderDate.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.OrderDate.AllowNull = false;
            this.OrderDate.Format = "d";
            this.OrderDate.HeaderText = "OrderDate";
            this.OrderDate.MappingName = "OrderDate";
            this.OrderDate.MaxValue = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.OrderDate.MinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.OrderDate.UseMask = false;
            this.OrderDate.Width = 94;
            // 
            // ShippedDate
            // 
            this.ShippedDate.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ShippedDate.AllowNull = false;
            this.ShippedDate.Format = "d";
            this.ShippedDate.HeaderText = "ShippedDate";
            this.ShippedDate.MappingName = "ShippedDate";
            this.ShippedDate.MaxValue = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.ShippedDate.MinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.ShippedDate.UseMask = false;
            this.ShippedDate.Width = 94;
            // 
            // ShipVia
            // 
            this.ShipVia.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ShipVia.AllowNull = false;
            this.ShipVia.DisplayMember = "CompanyName";
            this.ShipVia.Format = "";
            this.ShipVia.HeaderText = "ShipVia";
            this.ShipVia.MappingName = "ShipVia";
            this.ShipVia.ValueMember = "ShipperID";
            this.ShipVia.Width = 94;
            // 
            // Freight
            // 
            this.Freight.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.Freight.AllowNull = false;
            this.Freight.Format = "N";
            this.Freight.FormatType = mControl.Util.Formats.StandadNumber;
            this.Freight.HeaderText = "Freight";
            this.Freight.MappingName = "Freight";
            this.Freight.Width = 81;
            // 
            // gridBtn
            // 
            this.gridBtn.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridBtn.ButtonStyle = mControl.GridView.GridButtonStyle.Link;
            this.gridBtn.CaptionText = "Click me";
            this.gridBtn.HeaderText = "button";
            this.gridBtn.ImageList = null;
            this.gridBtn.IsBound = false;
            this.gridBtn.MappingName = "";
            this.gridBtn.MenuItems.AddRange(new mControl.WinCtl.Controls.PopUpItem[] {
            this.popUpItem1,
            this.popUpItem2,
            this.popUpItem3});
            this.gridBtn.Width = 68;
            // 
            // mcForm1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(728, 340);
            this.Controls.Add(this.btnVirtual);
            this.Controls.Add(this.btnCurrent);
            this.Controls.Add(this.ctlButton5);
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.ctlButton4);
            this.Controls.Add(this.ctlButton3);
            this.Controls.Add(this.ctlButton2);
            this.Controls.Add(this.ctlButton1);
            this.Name = "mcForm1";
            this.Text = "Grid Sample";
            this.Controls.SetChildIndex(this.ctlButton1, 0);
            this.Controls.SetChildIndex(this.ctlButton2, 0);
            this.Controls.SetChildIndex(this.ctlButton3, 0);
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.ctlButton4, 0);
            this.Controls.SetChildIndex(this.grid1, 0);
            this.Controls.SetChildIndex(this.ctlButton5, 0);
            this.Controls.SetChildIndex(this.btnCurrent, 0);
            this.Controls.SetChildIndex(this.btnVirtual, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new mcForm1());
		}

		private void BindGrid()
		{
			DataTable dt= Dal.OleDB.DBApp.Orders();
			dt.TableName="Orders"; 

			DataTable dtDetails= Dal.OleDB.DBApp.OrdersDetails();
			dtDetails.TableName="OrdersDetails"; 
	
			this.CustomerID.DataSource=Dal.OleDB.DBApp.Customers().DefaultView;
			this.ShipVia.DataSource=Dal.OleDB.DBApp.Shippers().DefaultView;

            DataSet DS = new DataSet();
            DS.Tables.AddRange(new System.Data.DataTable[] { dt, dtDetails});

            DataRelation rel1 =
            DS.Relations.Add("rel1",
            DS.Tables["Orders"].Columns["OrderID"],
            DS.Tables["OrdersDetails"].Columns["OrderID"], false);

            this.grid1.Init(DS, "Orders", "Orders");
			this.OrderID.DataSource=dtDetails;

		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			BindGrid();
		}

		private void ctlButton1_Click(object sender, System.EventArgs e)
		{
			GridPerform.Print(this.grid1);
		}

		private void ctlButton2_Click(object sender, System.EventArgs e)
		{
            GridPerform.ExportFiltred(this.grid1);
		}

		private void ctlButton3_Click(object sender, System.EventArgs e)
		{
            GridPerform.Filter(this.grid1);
		}

		private void ctlButton4_Click(object sender, System.EventArgs e)
		{
            GridPerform.AdjustColumns(this.grid1);
		}

        private void ctlButton5_Click(object sender, EventArgs e)
        {
          GridPerform.Find(this.grid1);
        }

        private void btnCurrent_Click(object sender, EventArgs e)
        {
            if (this.grid1.CurrentRowIndex == -1)
                return;
            CtlForm1 f = new CtlForm1();
            f.Open(new object[] { this.grid1.GetCurrentDataRow(), this.grid1.MappingName });

        }

        private void btnVirtual_Click(object sender, EventArgs e)
        {
            CtlForm2 f = new CtlForm2();
            f.Open(null);

 
        }

        private void grid1_ButtonClick(object sender, mControl.GridView.ButtonClickEventArgs e)
        {

            MessageBox.Show(e.Value.ToString());
        }


	}
}
