using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using mControl.WinCtl.Controls;
using mControl.GridStyle.Columns;  
using mControl.Util;


namespace mControl.GridStyle
{
	/// <summary>
	/// Summary description for CtlGridPreview.
	/// </summary>
	public class GridPreview : mControl.WinCtl.Forms.CtlForm
	{

		#region NetFram

		private void NetReflectedFram()
		{
			//mControl.Util.Net.NetFramReg.NetReflected("ba7fa38f0b671cbc")
			groupBox2.NetReflectedFram("ba7fa38f0b671cbc");
			checkBox4.NetReflectedFram("ba7fa38f0b671cbc");
			checkBox3.NetReflectedFram("ba7fa38f0b671cbc");
			comboBox1.NetReflectedFram("ba7fa38f0b671cbc");
			checkBox5.NetReflectedFram("ba7fa38f0b671cbc");
			groupBox1.NetReflectedFram("ba7fa38f0b671cbc");
			widthPixels.NetReflectedFram("ba7fa38f0b671cbc");
			label1.NetReflectedFram("ba7fa38f0b671cbc");
			checkBox2.NetReflectedFram("ba7fa38f0b671cbc");
			checkBox1.NetReflectedFram("ba7fa38f0b671cbc");
			comboTables.NetReflectedFram("ba7fa38f0b671cbc");
			groupBox3.NetReflectedFram("ba7fa38f0b671cbc");
			checkBox7.NetReflectedFram("ba7fa38f0b671cbc");
			checkBox6.NetReflectedFram("ba7fa38f0b671cbc");
			button1.NetReflectedFram("ba7fa38f0b671cbc");

		}

		#endregion

		#region Members
		protected internal mControl.GridStyle.Grid grid1;
		private mControl.WinCtl.Controls.CtlGroupBox groupBox2;
		private mControl.WinCtl.Controls.CtlCheckBox checkBox4;
		private mControl.WinCtl.Controls.CtlCheckBox checkBox3;
		private mControl.WinCtl.Controls.CtlComboBox comboBox1;
		private mControl.WinCtl.Controls.CtlCheckBox checkBox5;
		private mControl.WinCtl.Controls.CtlGroupBox groupBox1;
		private mControl.WinCtl.Controls.CtlSpinEdit widthPixels;
		private mControl.WinCtl.Controls.CtlLabel label1;
		private mControl.WinCtl.Controls.CtlCheckBox checkBox2;
		private mControl.WinCtl.Controls.CtlCheckBox checkBox1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ImageList imageList1;
		private mControl.WinCtl.Controls.CtlComboBox comboTables;
		private mControl.WinCtl.Controls.CtlGroupBox groupBox3;
		private mControl.WinCtl.Controls.CtlCheckBox checkBox7;
		private mControl.WinCtl.Controls.CtlCheckBox checkBox6;
		private mControl.WinCtl.Controls.CtlButton button1;
        private System.Windows.Forms.Panel ctlScrollableControl1;
        private CtlButton btnOk;
        private CtlButton btnCancel;
        private CtlButton btnAdjust;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor

		public GridPreview()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			NetReflectedFram();
//			if(netFramGrid.NetFram())
//			{
//				netFramwork.NetReflectedFram();
//			}
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridPreview));
            this.ctlScrollableControl1 = new System.Windows.Forms.Panel();
            this.grid1 = new mControl.GridStyle.Grid();
            this.groupBox3 = new mControl.WinCtl.Controls.CtlGroupBox();
            this.button1 = new mControl.WinCtl.Controls.CtlButton();
            this.checkBox7 = new mControl.WinCtl.Controls.CtlCheckBox();
            this.comboTables = new mControl.WinCtl.Controls.CtlComboBox();
            this.checkBox6 = new mControl.WinCtl.Controls.CtlCheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new mControl.WinCtl.Controls.CtlGroupBox();
            this.checkBox4 = new mControl.WinCtl.Controls.CtlCheckBox();
            this.checkBox3 = new mControl.WinCtl.Controls.CtlCheckBox();
            this.comboBox1 = new mControl.WinCtl.Controls.CtlComboBox();
            this.checkBox5 = new mControl.WinCtl.Controls.CtlCheckBox();
            this.groupBox1 = new mControl.WinCtl.Controls.CtlGroupBox();
            this.label1 = new mControl.WinCtl.Controls.CtlLabel();
            this.checkBox2 = new mControl.WinCtl.Controls.CtlCheckBox();
            this.checkBox1 = new mControl.WinCtl.Controls.CtlCheckBox();
            this.widthPixels = new mControl.WinCtl.Controls.CtlSpinEdit();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnOk = new mControl.WinCtl.Controls.CtlButton();
            this.btnCancel = new mControl.WinCtl.Controls.CtlButton();
            this.btnAdjust = new mControl.WinCtl.Controls.CtlButton();
            this.ctlScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.Name = "caption";
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.Text = "Caption Control";
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.FocusedColor = System.Drawing.SystemColors.ActiveCaption;
            this.StyleGuideBase.StylePlan = mControl.WinCtl.Controls.Styles.Desktop;
            // 
            // ctlScrollableControl1
            // 
            this.ctlScrollableControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlScrollableControl1.AutoScroll = true;
            this.ctlScrollableControl1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ctlScrollableControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctlScrollableControl1.Controls.Add(this.grid1);
            this.ctlScrollableControl1.Location = new System.Drawing.Point(8, 62);
            this.ctlScrollableControl1.Name = "ctlScrollableControl1";
            this.ctlScrollableControl1.Size = new System.Drawing.Size(448, 138);
            this.ctlScrollableControl1.TabIndex = 23;
            this.ctlScrollableControl1.Text = "ctlScrollableControl1";
            // 
            // grid1
            // 
            this.grid1.AllowChangeColumnMapping = false;
            this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grid1.CaptionVisible = false;
            this.grid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid1.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.Name = "grid1";
            this.grid1.Padding = new System.Windows.Forms.Padding(1);
            this.grid1.ShowPopUpMenu = false;
            this.grid1.Size = new System.Drawing.Size(442, 120);
            this.grid1.StatusBarMode = mControl.GridStyle.Controls.StatusBarMode.Show;
            this.grid1.StylePainter = this.StyleGuideBase;
            this.grid1.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.checkBox7);
            this.groupBox3.Controls.Add(this.comboTables);
            this.groupBox3.Controls.Add(this.checkBox6);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.groupBox3.Location = new System.Drawing.Point(312, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.ReadOnly = false;
            this.groupBox3.Size = new System.Drawing.Size(144, 112);
            this.groupBox3.StylePainter = this.StyleGuideBase;
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tables";
            // 
            // button1
            // 
            this.button1.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.button1.FixSize = false;
            this.button1.Location = new System.Drawing.Point(8, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 18);
            this.button1.StylePainter = this.StyleGuideBase;
            this.button1.TabIndex = 19;
            this.button1.Text = "Save TableStyle";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox7
            // 
            this.checkBox7.Checked = true;
            this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox7.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.checkBox7.FixSize = false;
            this.checkBox7.ImageIndex = 0;
            this.checkBox7.Location = new System.Drawing.Point(8, 72);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(128, 16);
            this.checkBox7.StylePainter = this.StyleGuideBase;
            this.checkBox7.TabIndex = 17;
            this.checkBox7.Text = "Show Rows Header";
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // comboTables
            // 
            this.comboTables.AcceptItems = false;
            this.comboTables.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.comboTables.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTables.DropDownWidth = 128;
            this.comboTables.FixSize = false;
            this.comboTables.IntegralHeight = false;
            this.comboTables.ItemHeight = 13;
            this.comboTables.Location = new System.Drawing.Point(8, 32);
            this.comboTables.Name = "comboTables";
            this.comboTables.ReadOnly = true;
            this.comboTables.Size = new System.Drawing.Size(128, 21);
            this.comboTables.StylePainter = this.StyleGuideBase;
            this.comboTables.TabIndex = 14;
            this.comboTables.SelectedIndexChanged += new System.EventHandler(this.comboTables_SelectedIndexChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.Checked = true;
            this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox6.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.checkBox6.FixSize = false;
            this.checkBox6.ImageIndex = 0;
            this.checkBox6.Location = new System.Drawing.Point(8, 56);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(128, 16);
            this.checkBox6.StylePainter = this.StyleGuideBase;
            this.checkBox6.TabIndex = 18;
            this.checkBox6.Text = "Show Columns Header";
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox4);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.checkBox5);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.groupBox2.Location = new System.Drawing.Point(160, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.ReadOnly = false;
            this.groupBox2.Size = new System.Drawing.Size(144, 112);
            this.groupBox2.StylePainter = this.StyleGuideBase;
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Style";
            // 
            // checkBox4
            // 
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.checkBox4.FixSize = false;
            this.checkBox4.ImageIndex = 0;
            this.checkBox4.Location = new System.Drawing.Point(8, 72);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(112, 16);
            this.checkBox4.StylePainter = this.StyleGuideBase;
            this.checkBox4.TabIndex = 17;
            this.checkBox4.Text = "Show Status Bar";
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.checkBox3.FixSize = false;
            this.checkBox3.ImageIndex = 0;
            this.checkBox3.Location = new System.Drawing.Point(8, 88);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(104, 16);
            this.checkBox3.StylePainter = this.StyleGuideBase;
            this.checkBox3.TabIndex = 16;
            this.checkBox3.Text = "Flat Mode";
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.AcceptItems = false;
            this.comboBox1.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.comboBox1.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.DropDownWidth = 128;
            this.comboBox1.FixSize = false;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.ItemHeight = 13;
            this.comboBox1.Location = new System.Drawing.Point(8, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.ReadOnly = true;
            this.comboBox1.Size = new System.Drawing.Size(128, 21);
            this.comboBox1.StylePainter = this.StyleGuideBase;
            this.comboBox1.TabIndex = 14;
            this.comboBox1.Text = "Desktop";
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // checkBox5
            // 
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.checkBox5.FixSize = false;
            this.checkBox5.ImageIndex = 0;
            this.checkBox5.Location = new System.Drawing.Point(8, 56);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(104, 16);
            this.checkBox5.StylePainter = this.StyleGuideBase;
            this.checkBox5.TabIndex = 18;
            this.checkBox5.Text = "Show Caption";
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.widthPixels);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.groupBox1.Location = new System.Drawing.Point(8, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.ReadOnly = false;
            this.groupBox1.Size = new System.Drawing.Size(144, 112);
            this.groupBox1.StylePainter = this.StyleGuideBase;
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adjust";
            // 
            // label1
            // 
            this.label1.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageIndex = 0;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.StylePainter = this.StyleGuideBase;
            this.label1.TabIndex = 10;
            this.label1.TabStop = false;
            this.label1.Text = "Width Pixels";
            // 
            // checkBox2
            // 
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.checkBox2.FixSize = false;
            this.checkBox2.ImageIndex = 0;
            this.checkBox2.Location = new System.Drawing.Point(8, 64);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(112, 16);
            this.checkBox2.StylePainter = this.StyleGuideBase;
            this.checkBox2.TabIndex = 12;
            this.checkBox2.Text = "Auto adjust";
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.checkBox1.FixSize = false;
            this.checkBox1.ImageIndex = 0;
            this.checkBox1.Location = new System.Drawing.Point(8, 88);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 16);
            this.checkBox1.StylePainter = this.StyleGuideBase;
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Show Scroll bar";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // widthPixels
            // 
            this.widthPixels.ButtonsVisible = true;
            this.widthPixels.DecimalPlaces = 0;
            this.widthPixels.FixSize = false;
            this.widthPixels.Format = "N";
            this.widthPixels.FormatType = mControl.Util.NumberFormats.StandadNumber;
            this.widthPixels.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.widthPixels.Location = new System.Drawing.Point(80, 32);
            this.widthPixels.Name = "widthPixels";
            this.widthPixels.RangeValue = new mControl.Util.RangeNumber(new decimal(new int[] {
                0,
                0,
                0,
                0}), new decimal(new int[] {
                1280,
                0,
                0,
                0}));
            this.widthPixels.Size = new System.Drawing.Size(56, 20);
            this.widthPixels.StylePainter = this.StyleGuideBase;
            this.widthPixels.TabIndex = 9;
            this.widthPixels.Text = "300";
            this.widthPixels.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.widthPixels.ValueChanged += new System.EventHandler(this.widthPixels_ValueChanged);
            // 
            // btnOk
            // 
            this.btnOk.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.FixSize = false;
            this.btnOk.Location = new System.Drawing.Point(278, 319);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 23);
            this.btnOk.StylePainter = this.StyleGuideBase;
            this.btnOk.TabIndex = 25;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.FixSize = false;
            this.btnCancel.Location = new System.Drawing.Point(370, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 23);
            this.btnCancel.StylePainter = this.StyleGuideBase;
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdjust
            // 
            this.btnAdjust.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
            this.btnAdjust.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdjust.FixSize = false;
            this.btnAdjust.Location = new System.Drawing.Point(8, 319);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(86, 23);
            this.btnAdjust.StylePainter = this.StyleGuideBase;
            this.btnAdjust.TabIndex = 26;
            this.btnAdjust.Text = "Adjust";
            this.btnAdjust.Click += new System.EventHandler(this.btnAdjust_Click);
            // 
            // GridPreview
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(458, 354);
            this.Controls.Add(this.btnAdjust);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.ctlScrollableControl1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GridPreview";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.ctlScrollableControl1, 0);
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnAdjust, 0);
            this.ctlScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region GridPreview

		private bool created;
		private const int gridHeight=112;
		private const int rowsSample=4;
		private const int gridDefaultHeight=128;
	
		private GridTableStyle[] tables;
		private int[] list=null;
		private int[,] listCol=null;
		private int[,] UpdatedCol=null;
	
		private int TblCount;
		private int ColCount;
		private int[] ColsCount;

		private Grid owner;
	

		public int[] List
		{
			get{return list;}
		}

		public int GridWidth
		{
			get{return this.grid1.Width ;}
		}

		public void Preview(Grid grid)
		{
			try
			{
				created=false;
				tables=null;
				this.owner=grid;

				this.grid1.Width  =grid.Width ;
				//this.Width =grid.Width + 20;
	
				this.grid1.StatusBarMode =grid.StatusBarMode ;
				this.grid1.RightToLeft  =grid.RightToLeft  ;
				this.grid1.CaptionVisible=grid.CaptionVisible;   
				this.grid1.FlatMode=grid.FlatMode;  
				//-this.grid1.BorderColor =grid.BorderColor;  
				this.grid1.BorderStyle  =grid.BorderStyle;  
				this.grid1.CaptionText =grid.CaptionText;   
		
				this.checkBox3.Checked=grid.FlatMode; 
				this.checkBox4.Checked=grid.IsStatusBarVisible;  
				this.checkBox5.Checked=grid.CaptionVisible;  
		
				if(!this.grid1.CaptionVisible)
				{
					this.grid1.Height =this.grid1.Height-Grid.DefaultCaptionHeight;
				}
	
				this.comboBox1.Items.AddRange(Enum.GetNames(typeof(Styles)));
				this.widthPixels.Value=(decimal)grid1.Width;
		
				if(grid.StylePainter==null)
				{
					this.StyleGuideBase.StylePlan=StyleControl.StylePlan;
					this.comboBox1.Enabled=false; 
				}
				else
				{
					this.StyleGuideBase.SetStyleLayout(grid.StylePainter.Layout);
				}
		
				int indx=(int)(Styles)Enum.Parse(typeof(Styles),this.StyleGuideBase.StylePlan.ToString());
				this.comboBox1.SelectedIndex=indx;

				bool enableTables=false;
				//Tables/Columns
				if(grid.Tables.Count>0)
				{
					SetMultiTables(grid);
					//cnt=grid1.Tables[0].Columns.Count; 
					//SetDataSource(grid1.Tables[0].MappingName,grid1.Tables[0].Columns);
					SetDataSource(grid.Tables);
					this.comboTables.SelectedIndex=0;
					enableTables=true;
				}
				else
				{
					grid.CreateGridColumns();
					grid1.Columns.AddRange(grid.GridColumns);
					//grid1.Columns.AddRange(grid.CreateGridColumns());
					SetDataSource("GeidPreview",grid1.Columns);
								  
					grid1.ColumnHeadersVisible=grid.ColumnHeadersVisible;
					grid1.RowHeadersVisible=grid.RowHeadersVisible;
					this.checkBox6.Checked=this.grid1.ColumnHeadersVisible;
					this.checkBox7.Checked=this.grid1.RowHeadersVisible;

				}

				this.comboTables.Enabled=enableTables;
				this.button1.Enabled=enableTables;
				created=true;
				int w=this.grid1.Width+24;
				if(w<=1024)
				{
					this.Width=w;
				}
				//SetAdjustColumns();
			}
			catch
			{
				this.Close (); 
			}
		}

		#endregion

		#region Methods

		private void GetColumnsWidth(int indx)
		{
			for (int i=0;i<ColsCount[indx];i++)
			{
				this.grid1.Tables[0].Columns[i].Width=UpdatedCol[indx,i];
			}
		}

		private void SetColumnsWidth(int indx)
		{
			for (int i=0;i<ColsCount[indx];i++)
			{
				UpdatedCol[indx,i]=this.grid1.Tables[0].Columns[i].Width;
			}
		}

		private void SetMultiTables(Grid grid)
		{

			TblCount=grid.Tables.Count;
			tables=new GridTableStyle[TblCount];
			grid.Tables.CopyTo(tables,0);
			grid.Tables.CopyTo(this.grid1.Tables);
			ColsCount=new int[TblCount];  

			for(int i=0;i< TblCount;i++)
			{
				ColsCount.SetValue(grid.Tables[i].Columns.Count,i);
				GridTableStyle ts=grid.Tables[i];
				tables.SetValue(ts,i);
			
				for(int j=0;j< grid.Tables[i].Columns.Count;j++)
					ColCount++;
			}

			listCol=new int[TblCount,ColCount];
			UpdatedCol=new int[TblCount,ColCount];

			for(int i=0;i< TblCount;i++)
			{
				for(int j=0;j< ColsCount[i];j++)
				{
					int w=grid.Tables[i].Columns[j].Width;
					listCol[i,j]=w;
					UpdatedCol[i,j]=w;
					grid1.Tables[i].Columns[j].Width=w;
				}
			}

			foreach(GridTableStyle gd in grid.Tables)
			{
				this.comboTables.Items.Add(gd.MappingName);
			}

		}

		private void SetDataSource(TableCollection tbls)
		{
			this.grid1.DataMember="";
			this.grid1.DataSource=null;
			this.grid1.TableStyles.Clear();
			this.grid1.Columns.Clear();
	
			DataSet ds=new DataSet();

			foreach(GridTableStyle t in tbls  )
			{
               DataTable dt=SetDataSource(t);
               ds.Tables.Add(dt);//.MappingName,t.Columns);
			}

			this.grid1.DataMember=tbls[0].MappingName;
			this.grid1.DataSource=ds;
			//this.grid1.Init(ds,tbls[0].MappingName,mappingName);
	
			this.grid1.SetTableStyles(false);
			GetColumnsWidth(0);

			this.checkBox6.Checked=tbls[0].ColumnHeadersVisible;
			this.checkBox7.Checked=tbls[0].RowHeadersVisible;
		}

		private DataTable SetDataSource(GridTableStyle ts)
		{
			int cnt =ts.Columns.Count;

			//list=new int[cnt];
			DataTable dmTable = new DataTable(ts.MappingName);
			System.Data.DataColumn dcol=null;
			//Build Schema Data source
			for (int i=0; i< cnt; i++)
			{
				//list.SetValue (ts.Columns[i].Width,i);
		
				if(ts.Columns[i].ColumnType ==ColumnTypes.DateTimeColumn) 
				{
					dcol=new DataColumn(ts.Columns[i].MappingName);
					dcol.DataType= typeof(DateTime);
					dcol.DefaultValue=DateTime.Today;
				}
				else if(ts.Columns[i].ColumnType ==ColumnTypes.BoolColumn) 
				{
					dcol=new DataColumn(ts.Columns[i].MappingName);
					dcol.DataType= typeof(bool);
					dcol.DefaultValue=true;
				}
				else
				{
					dcol=new DataColumn(ts.Columns[i].MappingName , typeof(string));
					dcol.DefaultValue="";
				}
			
				dmTable.Columns.Add(dcol);
			}		
			
			FillDataTable(dmTable,cnt,rowsSample);
			dmTable.DefaultView.AllowNew =false;
			return dmTable;
		
		}

		private void SetDataSource(string mappingName,GridColumnsCollection Columns)
		{
			int cnt =Columns.Count;
            bool flagSetting=false; 

			if(tables!=null)
			{
				int indx=this.grid1.Tables.IndexOf(grid1.Tables.GetTable(mappingName));
				this.checkBox6.Checked=this.grid1.Tables[indx].ColumnHeadersVisible;
				this.checkBox7.Checked=this.grid1.Tables[indx].RowHeadersVisible;
				this.grid1.TableStyles.Clear();
				this.grid1.Columns.Clear();
				this.grid1.DataSource=null;
				flagSetting=true;
			}

			list=new int[cnt];
			DataTable dmTable = new DataTable(mappingName);
			System.Data.DataColumn dcol=null;

			//Build Schema Data source
			for (int i=0; i< cnt; i++)
			{
				list.SetValue (Columns[i].Width,i);
		
				if(Columns[i].ColumnType ==ColumnTypes.DateTimeColumn) 
				{
					dcol=new DataColumn(Columns[i].MappingName);
					dcol.DataType= typeof(DateTime);
					dcol.DefaultValue=DateTime.Today;
				}
				else if(Columns[i].ColumnType ==ColumnTypes.BoolColumn) 
				{
					dcol=new DataColumn(Columns[i].MappingName);
					dcol.DataType= typeof(bool);
					dcol.DefaultValue=true;
				}
				else
				{
					dcol=new DataColumn(Columns[i].MappingName , typeof(string));
					dcol.DefaultValue="";
				}
			
				dmTable.Columns.Add(dcol);
			}		
			
			FillDataTable(dmTable,cnt,rowsSample);
			dmTable.DefaultView.AllowNew =false;
		
			//this.grid1.MappingName=mappingName;
			//this.grid1.DataSource=dmTable.DefaultView;
			this.grid1.Init(dmTable.DefaultView,"",mappingName);
			this.grid1.SetTableStyles(flagSetting);

		}

		public  void FillDataTable(System.Data.DataTable dmTable ,int cols,int Rows) 
		{
			
			DataRow dr =null;
	
			for (int i = 0; (i < Rows); i++) 
			{
				dr = dmTable.NewRow();

				for (int j = 0; (j < cols); j++) 
				{
					try
					{
						System.Type type=dmTable.Columns[i].DataType.GetType();

						if(type== typeof(DateTime)) 
							dr[j] = DateTime.Today;// "01/01/2000" ;
						else if(type== typeof(int) || type== typeof(long) || type== typeof(byte) ) 
							dr[j] =10 ;
						else if(type== typeof(decimal) || type== typeof(double)|| type== typeof(short)) 
							dr[j] =10.00 ;
						if(type== typeof(bool)) 
							dr[j] =true ;
						else
							dr[j] ="dr " + i.ToString() + ":" + j.ToString () ;
					}
					catch{}
				}
				dmTable.Rows.Add(dr);
			}
		}

		private void SetAdjustColumns()
		{

			int rows=rowsSample ;// grid1.DataList.Count;
			int rowHeight=grid1.GridTableStyle.PreferredRowHeight;
			int rowsAdd=0;//grid1.DataList.AllowNew ? 1:0 ;
			//int gridHeight=grid1.DataGrid.Height;
			int HeaderAdd=grid1.GridTableStyle.ColumnHeadersVisible  ? Grid.DefaultColumnHeaderHeight:0 ;
			int CaptionHeight=grid1.CaptionVisible  ? Grid.DefaultCaptionHeight :0;
			int CalcRowsHeight=((rowsAdd+rows)*rowHeight)+HeaderAdd+ CaptionHeight; 
			//bool showVertScrall= (CalcRowsHeight > gridHeight);
			int statusBarHeight=grid1.IsStatusBarVisible ? this.grid1.StatusBar.Height :0; 
			int scrollAdd= this.checkBox1.Checked ?  -rowHeight:2 ;
		         
			this.grid1.Height =CalcRowsHeight+statusBarHeight+scrollAdd; 
			//AdjustColumns(grid1.Width,showVertScrall);


			this.Invalidate(); 
			this.grid1.AdjustColumns();// (this.grid1);    
			//this.grid1.GridTableStyle.AdjustColumns (this.grid1.Width ,this.checkBox1.Checked );    
		}

		private void SetGridStyles()
		{
			if(!this.comboBox1.Enabled)
			{
				//this.BackColor=this.grid1.GridLayout.ColorBrush2;
				goto Label_01;
			}
				object o=this.comboBox1.SelectedItem;
		
				if(o==null)
					o="Desktop";
				Styles style=(Styles)Enum.Parse(typeof(Styles),o.ToString());
				if(style==Styles.None)
					goto Label_01;    
            
	
			grid1.SetStyleLayout(style);
			this.StyleGuideBase.StylePlan=style;

			Label_01:
	
			grid1.FlatMode =this.checkBox3.Checked; 
			if(this.checkBox4.Checked)
				grid1.StatusBarMode=mControl.GridStyle.Controls.StatusBarMode.Show;
			else
				grid1.StatusBarMode=mControl.GridStyle.Controls.StatusBarMode.Hide;
		
			//grid1.DataGrid.SetDataGridInColumnInternal (); 
		
		}

		#endregion

		#region ControlsEvents

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!created)
				return;

			int statusHeight= this.grid1.IsStatusBarVisible   ? this.grid1.StatusBar.Height : 0;    
			int captionHeight=this.grid1.CaptionVisible ? Grid.DefaultCaptionHeight:0;
	
			if(this.checkBox1.Checked )
			{
				this.grid1.Height =gridDefaultHeight;//gridHeight-captionHeight;//(cnt-1)* grid1.Tables[0].PreferredRowHeight + statusHeight;  
			}
			else
			{
				this.grid1.Height =((1+rowsSample)* grid1.PreferredRowHeight) + statusHeight +captionHeight;  
			}
			SetAdjustColumns();
		}

		private void widthPixels_ValueChanged(object sender, EventArgs e)
		{
			if(!created)
				return;  
			//if(this.grid1.Width >= this.widthPixels.Value)
			//{
			this.grid1.Width=(int)this.widthPixels.Value;
			//this.Width= this.grid1.Width+40;
			if(this.checkBox2.Checked )
			{
				SetAdjustColumns();
			}
			//}
		}

		private void comboBox1_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			if(created)
				SetGridStyles();  
		}

		private void checkBox3_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
				SetGridStyles();  
		}

		private void checkBox4_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
				SetGridStyles();  
		}

		private void checkBox5_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
				grid1.CaptionVisible =this.checkBox5.Checked;  
		} 

		private void comboTables_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.comboTables.SelectedItem!=null)
			{
				string mapName=this.comboTables.SelectedItem.ToString();
				this.grid1.DataMember=mapName;
				GetColumnsWidth(this.comboTables.SelectedIndex);
	
				//this.grid1.SetTableStyles(true);

				//SetDataSource(mapName,this.grid1.Tables.GetTable(mapName).Columns);  
			}
		
		}

		private void checkBox6_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
				this.grid1.ColumnHeadersVisible=this.checkBox6.Checked;
		}

		private void checkBox7_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
				this.grid1.RowHeadersVisible=this.checkBox7.Checked;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{

			int indx=this.comboTables.SelectedIndex;
			this.grid1.Tables[indx].ColumnHeadersVisible=this.checkBox6.Checked;
			this.grid1.Tables[indx].RowHeadersVisible=this.checkBox7.Checked;
			for(int i=0;i<this.grid1.TableStyles[indx].GridColumnStyles.Count;i++)
			{
				grid1.Tables[indx].Columns[i].Width=grid1.TableStyles[indx].GridColumnStyles[i].Width;
			}
			SetColumnsWidth(indx);
	
			//this.grid1.Update();

		}

		#endregion

		#region WizClick

    private void btnOk_Click(object sender, EventArgs e)
        {
			try
			{

				if(this.tables!=null)
				{
					//grid1.TableStyles.Clear();
				
					for (int i=0; i< TblCount ; i++)
					{
						GridTableStyle ts=(GridTableStyle)grid1.Tables[i];
						owner.Tables[i].RowHeadersVisible=ts.RowHeadersVisible; 
						owner.Tables[i].ColumnHeadersVisible=ts.ColumnHeadersVisible; 
						for (int j=0; j< ColsCount[i]; j++)
						{
							owner.Tables[i].Columns[j].Width=ts.Columns[j].Width;
						}
					}
				}

				else if(grid1.Columns.Count >0)
				{
					for(int j=0;j< grid1.Columns.Count;j++)
					{
						owner.Columns[j].Width=grid1.Columns[j].Width;
					}
					owner.ColumnHeadersVisible=grid1.ColumnHeadersVisible;
					owner.RowHeadersVisible=this.checkBox7.Checked;// grid1.RowHeadersVisible;
				}

				owner.CaptionVisible=grid1.CaptionVisible;   
				owner.FlatMode =grid1.FlatMode;
				owner.StatusBarMode =grid1.StatusBarMode;
				owner.SetStyleLayout(grid1.GridLayout.Layout);//.StylePlan);
				//MessageBox.Show("ok");

				owner.Update();   
			
				this.DialogResult =DialogResult.OK ;
                this.Close();
				
			}
			catch(Exception ex)
			{
				MsgBox.ShowError (ex.Message );
			}

		}

        private void btnCancel_Click(object sender, EventArgs e)
        {
			this.DialogResult =DialogResult.No ;
			this.Close ();

		}

        private void btnAdjust_Click(object sender, EventArgs e)
        {
			SetAdjustColumns();    
			//base.OnHelpClick (sender, e);
		}

		#endregion

		#region Override

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			if(this.Width < 340)
			{
				this.Width=340; 
			}
    
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated (e);
			if(created)
				SetGridStyles(); 
		}


		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed (e);
			if(this.DialogResult ==DialogResult.OK )
				return;

			try
			{

				if(this.tables!=null)
				{
					//grid1.TableStyles.Clear();
				
					for (int i=0; i< tables.Length ; i++)
					{
						GridTableStyle ts=(GridTableStyle)tables.GetValue(i);
						grid1.Tables[i].RowHeadersVisible=ts.RowHeadersVisible; 
						grid1.Tables[i].ColumnHeadersVisible=ts.ColumnHeadersVisible; 
						for (int j=0; j< ColsCount[i]; j++)
						{
							grid1.Tables[i].Columns[j].Width=listCol[i,j]; 
						}
					}
				}
				else
				{
					grid1.GridTableStyle.GridColumnStyles.Clear ();
				
					for (int i=0; i< list.Length ; i++)
					{
						grid1.Columns[i].Width=list[i]; 
					}
				}
				//MessageBox.Show("ok");
			}
			catch(Exception  ex)
			{
				MessageBox.Show (ex.Message);
				//this.Close ();
			}
		
		}

		#endregion

    


	}
}
