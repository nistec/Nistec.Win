using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Nistec.WinForms;
using Nistec.Win;



namespace Nistec.GridView
{
	/// <summary>
	/// Summary description for McGridPreview.
	/// </summary>
	public class GridPreview : Nistec.WinForms.FormBase
	{

		#region NetFram

        //private void NetReflectedFram()
        //{
        //    //Nistec.Win.Net.nf_1.nf_2("ba7fa38f0b671cbc")
        //    //grpStyles.NetReflectedFram("ba7fa38f0b671cbc");
        //    chkAlternating.NetReflectedFram("ba7fa38f0b671cbc");
        //    chkFlat.NetReflectedFram("ba7fa38f0b671cbc");
        //    comboStyles.NetReflectedFram("ba7fa38f0b671cbc");
        //    chkCaption.NetReflectedFram("ba7fa38f0b671cbc");
        //    grpAdjust.NetReflectedFram("ba7fa38f0b671cbc");
        //    widthPixels.NetReflectedFram("ba7fa38f0b671cbc");
        //    label1.NetReflectedFram("ba7fa38f0b671cbc");
        //    //checkBox2.NetReflectedFram("ba7fa38f0b671cbc");
        //    chkScroll.NetReflectedFram("ba7fa38f0b671cbc");
        //    //comboTables.NetReflectedFram("ba7fa38f0b671cbc");
        //    //groupBox3.NetReflectedFram("ba7fa38f0b671cbc");
        //    chkRowHeaders.NetReflectedFram("ba7fa38f0b671cbc");
        //    chkColHeaders.NetReflectedFram("ba7fa38f0b671cbc");
        //    //btnSaveTableStyles.NetReflectedFram("ba7fa38f0b671cbc");

        //}

		#endregion

		#region Members
		//private Nistec.WinCtl.Wizards.WizDialog wizDialog1;

        /// <summary>
        /// grid Designer
        /// </summary>
		protected internal Nistec.GridView.Grid gridDesigner;
		private Nistec.WinForms.McCheckBox chkAlternating;
		private Nistec.WinForms.McCheckBox chkFlat;
		private Nistec.WinForms.McComboBox comboStyles;
		private Nistec.WinForms.McCheckBox chkCaption;
		private Nistec.WinForms.McGroupBox grpAdjust;
		private Nistec.WinForms.McSpinEdit widthPixels;
		private Nistec.WinForms.McLabel label1;
		private Nistec.WinForms.McCheckBox chkScroll;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ImageList imageList1;
		private Nistec.WinForms.McCheckBox chkRowHeaders;
		private Nistec.WinForms.McCheckBox chkColHeaders;
		private System.Windows.Forms.Panel ctlScrollableControl1;
		private Nistec.WinForms.McButton btnCancel;
		private Nistec.WinForms.McButton btnOk;
		private Nistec.WinForms.McButton btnAdjst;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor
        /// <summary>
        /// Initilaized GridPreview
        /// </summary>
		public GridPreview()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//NetReflectedFram();
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
            this.gridDesigner = new Nistec.GridView.Grid();
            this.chkRowHeaders = new Nistec.WinForms.McCheckBox();
            this.chkColHeaders = new Nistec.WinForms.McCheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.chkAlternating = new Nistec.WinForms.McCheckBox();
            this.chkFlat = new Nistec.WinForms.McCheckBox();
            this.comboStyles = new Nistec.WinForms.McComboBox();
            this.chkCaption = new Nistec.WinForms.McCheckBox();
            this.grpAdjust = new Nistec.WinForms.McGroupBox();
            this.label1 = new Nistec.WinForms.McLabel();
            this.chkScroll = new Nistec.WinForms.McCheckBox();
            this.widthPixels = new Nistec.WinForms.McSpinEdit();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAdjst = new Nistec.WinForms.McButton();
            this.btnCancel = new Nistec.WinForms.McButton();
            this.btnOk = new Nistec.WinForms.McButton();
            this.ctlScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDesigner)).BeginInit();
            this.grpAdjust.SuspendLayout();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Desktop;
            // 
            // ctlScrollableControl1
            // 
            this.ctlScrollableControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlScrollableControl1.AutoScroll = true;
            this.ctlScrollableControl1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ctlScrollableControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctlScrollableControl1.Controls.Add(this.gridDesigner);
            this.ctlScrollableControl1.Location = new System.Drawing.Point(160, 14);
            this.ctlScrollableControl1.Name = "ctlScrollableControl1";
            this.ctlScrollableControl1.Size = new System.Drawing.Size(449, 160);
            this.ctlScrollableControl1.TabIndex = 23;
            this.ctlScrollableControl1.Text = "ctlScrollableControl1";
            // 
            // gridDesigner
            // 
            this.gridDesigner.BackColor = System.Drawing.Color.White;
            this.gridDesigner.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridDesigner.DataMember = "";
            this.gridDesigner.ForeColor = System.Drawing.Color.Black;
            this.gridDesigner.GridLineStyle = Nistec.GridView.GridLineStyle.Solid;
            this.gridDesigner.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.gridDesigner.Location = new System.Drawing.Point(0, 0);
            this.gridDesigner.Name = "gridDesigner";
            this.gridDesigner.PaintAlternating = false;
            this.gridDesigner.Size = new System.Drawing.Size(442, 136);
            this.gridDesigner.StylePainter = this.StyleGuideBase;
            this.gridDesigner.TabIndex = 4;
            // 
            // chkRowHeaders
            // 
            this.chkRowHeaders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.chkRowHeaders.Checked = true;
            this.chkRowHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRowHeaders.FixSize = false;
            this.chkRowHeaders.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkRowHeaders.ImageIndex = 0;
            this.chkRowHeaders.Location = new System.Drawing.Point(8, 152);
            this.chkRowHeaders.Name = "chkRowHeaders";
            this.chkRowHeaders.Size = new System.Drawing.Size(128, 16);
            this.chkRowHeaders.StylePainter = this.StyleGuideBase;
            this.chkRowHeaders.TabIndex = 17;
            this.chkRowHeaders.Text = "Show Rows Header";
            this.chkRowHeaders.CheckedChanged += new System.EventHandler(this.chkRowHeaders_CheckedChanged);
            // 
            // chkColHeaders
            // 
            this.chkColHeaders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.chkColHeaders.Checked = true;
            this.chkColHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkColHeaders.FixSize = false;
            this.chkColHeaders.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkColHeaders.ImageIndex = 0;
            this.chkColHeaders.Location = new System.Drawing.Point(8, 136);
            this.chkColHeaders.Name = "chkColHeaders";
            this.chkColHeaders.Size = new System.Drawing.Size(128, 16);
            this.chkColHeaders.StylePainter = this.StyleGuideBase;
            this.chkColHeaders.TabIndex = 18;
            this.chkColHeaders.Text = "Show Columns Header";
            this.chkColHeaders.CheckedChanged += new System.EventHandler(this.chkColHeaders_CheckedChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // chkAlternating
            // 
            this.chkAlternating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.chkAlternating.Checked = true;
            this.chkAlternating.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlternating.FixSize = false;
            this.chkAlternating.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkAlternating.ImageIndex = 0;
            this.chkAlternating.Location = new System.Drawing.Point(8, 104);
            this.chkAlternating.Name = "chkAlternating";
            this.chkAlternating.Size = new System.Drawing.Size(112, 16);
            this.chkAlternating.StylePainter = this.StyleGuideBase;
            this.chkAlternating.TabIndex = 17;
            this.chkAlternating.Text = "Paint Alternating";
            this.chkAlternating.CheckedChanged += new System.EventHandler(this.chkAlternating_CheckedChanged);
            // 
            // chkFlat
            // 
            this.chkFlat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.chkFlat.Checked = true;
            this.chkFlat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlat.FixSize = false;
            this.chkFlat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkFlat.ImageIndex = 0;
            this.chkFlat.Location = new System.Drawing.Point(8, 120);
            this.chkFlat.Name = "chkFlat";
            this.chkFlat.Size = new System.Drawing.Size(104, 16);
            this.chkFlat.StylePainter = this.StyleGuideBase;
            this.chkFlat.TabIndex = 16;
            this.chkFlat.Text = "Flat Mode";
            this.chkFlat.CheckedChanged += new System.EventHandler(this.chkFlat_CheckedChanged);
            // 
            // comboStyles
            // 
            this.comboStyles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStyles.DropDownWidth = 128;
            this.comboStyles.FixSize = false;
            this.comboStyles.IntegralHeight = false;
            this.comboStyles.Location = new System.Drawing.Point(8, 56);
            this.comboStyles.Name = "comboStyles";
            this.comboStyles.ReadOnly = true;
            this.comboStyles.Size = new System.Drawing.Size(128, 21);
            this.comboStyles.StylePainter = this.StyleGuideBase;
            this.comboStyles.TabIndex = 14;
            this.comboStyles.SelectionChangeCommitted += new System.EventHandler(this.comboStyles_SelectionChangeCommitted);
            // 
            // chkCaption
            // 
            this.chkCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.chkCaption.Checked = true;
            this.chkCaption.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCaption.FixSize = false;
            this.chkCaption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkCaption.ImageIndex = 0;
            this.chkCaption.Location = new System.Drawing.Point(8, 88);
            this.chkCaption.Name = "chkCaption";
            this.chkCaption.Size = new System.Drawing.Size(104, 16);
            this.chkCaption.StylePainter = this.StyleGuideBase;
            this.chkCaption.TabIndex = 18;
            this.chkCaption.Text = "Show Caption";
            this.chkCaption.CheckedChanged += new System.EventHandler(this.chkCaption_CheckedChanged);
            // 
            // grpAdjust
            // 
            this.grpAdjust.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.grpAdjust.Controls.Add(this.label1);
            this.grpAdjust.Controls.Add(this.chkScroll);
            this.grpAdjust.Controls.Add(this.widthPixels);
            this.grpAdjust.Controls.Add(this.chkCaption);
            this.grpAdjust.Controls.Add(this.chkAlternating);
            this.grpAdjust.Controls.Add(this.chkFlat);
            this.grpAdjust.Controls.Add(this.chkRowHeaders);
            this.grpAdjust.Controls.Add(this.chkColHeaders);
            this.grpAdjust.Controls.Add(this.comboStyles);
            this.grpAdjust.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpAdjust.Location = new System.Drawing.Point(8, 8);
            this.grpAdjust.Name = "grpAdjust";
            this.grpAdjust.ReadOnly = false;
            this.grpAdjust.Size = new System.Drawing.Size(144, 200);
            this.grpAdjust.StylePainter = this.StyleGuideBase;
            this.grpAdjust.TabIndex = 20;
            this.grpAdjust.TabStop = false;
            this.grpAdjust.Text = "Design";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.ImageIndex = 0;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.StylePainter = this.StyleGuideBase;
            this.label1.Text = "Width Pixels";
            // 
            // chkScroll
            // 
            this.chkScroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.chkScroll.Checked = true;
            this.chkScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkScroll.FixSize = false;
            this.chkScroll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkScroll.ImageIndex = 0;
            this.chkScroll.Location = new System.Drawing.Point(8, 168);
            this.chkScroll.Name = "chkScroll";
            this.chkScroll.Size = new System.Drawing.Size(112, 16);
            this.chkScroll.StylePainter = this.StyleGuideBase;
            this.chkScroll.TabIndex = 6;
            this.chkScroll.Text = "Show Scroll bar";
            this.chkScroll.CheckedChanged += new System.EventHandler(this.chkScroll_CheckedChanged);
            // 
            // widthPixels
            // 
            this.widthPixels.ButtonAlign = Nistec.WinForms.ButtonAlign.Right;
            this.widthPixels.DecimalPlaces = 0;
            this.widthPixels.DefaultValue = "";
            this.widthPixels.FixSize = false;
            this.widthPixels.Format = "N";
            this.widthPixels.FormatType = NumberFormats.StandadNumber;
            this.widthPixels.Location = new System.Drawing.Point(80, 24);
            this.widthPixels.MaxValue = new decimal(new int[] {
            1280,
            0,
            0,
            0});
            this.widthPixels.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.widthPixels.Name = "widthPixels";
            this.widthPixels.Size = new System.Drawing.Size(56, 20);
            this.widthPixels.StylePainter = this.StyleGuideBase;
            this.widthPixels.TabIndex = 9;
            this.widthPixels.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.widthPixels.ValueChanged += new System.EventHandler(this.widthPixels_ValueChanged);
            // 
            // btnAdjst
            // 
            this.btnAdjst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdjst.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdjst.Location = new System.Drawing.Point(353, 184);
            this.btnAdjst.Name = "btnAdjst";
            this.btnAdjst.Size = new System.Drawing.Size(80, 24);
            this.btnAdjst.StylePainter = this.StyleGuideBase;
            this.btnAdjst.TabIndex = 26;
            this.btnAdjst.Text = "Adjst";
            this.btnAdjst.Click += new System.EventHandler(this.btnAdjst_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.Location = new System.Drawing.Point(537, 184);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.StylePainter = this.StyleGuideBase;
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.Location = new System.Drawing.Point(449, 184);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 24);
            this.btnOk.StylePainter = this.StyleGuideBase;
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "Save";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // GridPreview
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(617, 216);
            this.Controls.Add(this.btnAdjst);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.ctlScrollableControl1);
            this.Controls.Add(this.grpAdjust);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GridPreview";
            this.ctlScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDesigner)).EndInit();
            this.grpAdjust.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region GridPreview

		private bool created;
		//private const int gridHeight=112;
		private const int rowsSample=5;
		//private const int gridDefaultHeight=105;
	
		//private GridTableStyle[] tables;
		private int[] list=null;
		//private int[,] listCol=null;
		//private int[,] UpdatedCol=null;
	
		//private int TblCount;
		//private int ColCount;
		//private int[] ColsCount;

		private Grid gridParent;
		private bool readOnly=false;
	
       
		internal int[] List
		{
			get{return list;}
		}
        /// <summary>
        /// Get the grid width
        /// </summary>
		public int GridWidth
		{
			get{return this.gridDesigner.Width ;}
		}
        /// <summary>
        /// Preview grid designer
        /// </summary>
        /// <param name="grid"></param>
		public void Preview(Grid grid)
		{
			try
			{
				//MessageBox.Show("ok0");

				created=false;
				//tables=null;
				this.gridParent=grid;
				this.readOnly=grid.ReadOnly;
				this.gridDesigner.ReadOnly=true;
				this.gridDesigner.Width  =grid.Width ;
				//this.gridDesigner.StatusBarMode =grid.StatusBarMode ;
				this.gridDesigner.RightToLeft  =grid.RightToLeft  ;
				//this.ctlScrollableControl1.RightToLeft  =grid.RightToLeft  ;
				this.gridDesigner.CaptionVisible=grid.CaptionVisible;   
				this.gridDesigner.FlatMode=grid.FlatMode;  
				this.gridDesigner.BorderStyle  =grid.BorderStyle;  
				this.gridDesigner.CaptionText =grid.CaptionText;   
		
				this.chkFlat.Checked=grid.FlatMode; 
				this.chkAlternating.Checked=grid.PaintAlternating;  
				this.chkCaption.Checked=grid.CaptionVisible;  
				this.chkScroll.Checked=false;  
	
//				if(!this.gridDesigner.CaptionVisible)
//				{
//					this.gridDesigner.Height =this.gridDesigner.Height-Grid.DefaultCaptionHeight;
//				}
	
				this.comboStyles.Items.AddRange(Enum.GetNames(typeof(Styles)));
				this.widthPixels.Value=(decimal)gridDesigner.Width;
		
				//MessageBox.Show("ok1");
				if(grid.StylePainter==null)
				{
					this.StyleGuideBase.StylePlan=StyleLayout.DefaultStylePlan;
					this.comboStyles.Enabled=false; 
				}
				else
				{
					this.StyleGuideBase.SetStyleLayout(grid.LayoutManager.Layout);
				}
		
				string stylePlan=this.StyleGuideBase.StylePlan.ToString();
				this.comboStyles.SelectedItem=stylePlan;
					gridDesigner.Columns.AddRange(grid.Columns);
                    int i = 0;
                    foreach (GridColumnStyle c in gridDesigner.Columns)
                    {
                        if (!c.IsBound)
                        {
                            //c.IsBound = true;
                            c.MappingName = c.HeaderText + i.ToString();
                            i++;
                        }
                    }

                    //MessageBox.Show("ok2.1");
                    SetDataSource("GridPreview", gridDesigner.Columns);
				
//				}
				//MessageBox.Show(gridDesigner.Columns.Count.ToString());
				//MessageBox.Show("ok3");
		
				gridDesigner.ColumnHeadersVisible=grid.ColumnHeadersVisible;
				gridDesigner.RowHeadersVisible=grid.RowHeadersVisible;
				this.chkColHeaders.Checked=this.gridDesigner.ColumnHeadersVisible;
				this.chkRowHeaders.Checked=this.gridDesigner.RowHeadersVisible;

				//MessageBox.Show("ok4");


				//this.comboTables.Enabled=enableTables;
				//this.btnSaveTableStyles.Enabled=enableTables;
				created=true;
				int w=this.gridDesigner.Width;//+24;
				if(w<= Grid.MaxGridWidth)
				{
					this.Width=w;
				}
				//MessageBox.Show("ok5");
				SettingColumns();
			}
			catch
			{
				this.Close (); 
			}
		}

		#endregion

		#region Methods

//		private void GetColumnsWidth(int indx)
//		{
//			for (int i=0;i<ColsCount[indx];i++)
//			{
//				this.gridDesigner.TableStyles[0].GridColumnStyles[i].Width=UpdatedCol[indx,i];
//			}
//		}
//
//		private void SetColumnsWidth(int indx)
//		{
//			for (int i=0;i<ColsCount[indx];i++)
//			{
//				UpdatedCol[indx,i]=this.gridDesigner.TableStyles[0].GridColumnStyles[i].Width;
//			}
//		}

//		private void SetMultiTables(Grid grid)
//		{
//
//			TblCount=grid.TableStyles.Count;
//			tables=new GridTableStyle[TblCount];
//			grid.TableStyles.CopyTo(tables,0);
//			gridDesigner.TableStyles.AddRange(tables);//.CopyTo(this.gridDesigner.TableStyles,0);
//			ColsCount=new int[TblCount];  
//
//			for(int i=0;i< TblCount;i++)
//			{
//				ColsCount.SetValue(grid.TableStyles[i].GridColumnStyles.Count,i);
//				GridTableStyle ts=grid.TableStyles[i];
//				tables.SetValue(ts,i);
//	
//				for(int j=0;j< grid.TableStyles[i].GridColumnStyles.Count;j++)
//					ColCount++;
//			}
//
//			listCol=new int[TblCount,ColCount];
//			UpdatedCol=new int[TblCount,ColCount];
//
//			for(int i=0;i< TblCount;i++)
//			{
//				for(int j=0;j< ColsCount[i];j++)
//				{
//					int w=grid.TableStyles[i].GridColumnStyles[j].Width;
//					listCol[i,j]=w;
//					UpdatedCol[i,j]=w;
//					gridDesigner.TableStyles[i].GridColumnStyles[j].Width=w;
//				}
//			}
//
////			foreach(GridTableStyle gd in grid.TableStyles)
////			{
////				this.comboTables.Items.Add(gd.MappingName);
////			}
//
//		}
//
//		private void SetDataSource(GridTableCollection tbls)
//		{
//			this.gridDesigner.DataMember="";
//			this.gridDesigner.DataSource=null;
////			this.gridDesigner.TableStyles.Clear();
////			this.gridDesigner.Columns.Clear();
//	
//			DataSet ds=new DataSet();
//
//			foreach(GridTableStyle t in tbls  )
//			{
//               DataTable dt=SetDataSource(t);
//              ds.Tables.Add(dt);//.MappingName,t.Columns);
//			}
//
//			this.gridDesigner.DataSource=ds;
//			this.gridDesigner.DataMember=tbls[0].MappingName;
//	
//			GetColumnsWidth(0);
//
////			this.chkColHeaders.Checked=gridDesigner.ColumnHeadersVisible;
////			this.chkRowHeaders.Checked=gridDesigner.RowHeadersVisible;
//		}
//
		private DataTable SetDataSource(GridTableStyle ts)
		{
			int cnt =ts.GridColumnStyles.Count;
			//list=new int[cnt];
			DataTable dmTable = new DataTable(ts.MappingName);
			System.Data.DataColumn dcol=null;
			//Build Schema Data source
			for (int i=0; i< cnt; i++)
			{
				//list.SetValue (ts.Columns[i].Width,i);

                if (ts.GridColumnStyles[i].ColumnType == GridColumnType.DateTimeColumn) 
				{
					dcol=new DataColumn(ts.GridColumnStyles[i].MappingName);
					dcol.DataType= typeof(DateTime);
					dcol.DefaultValue=DateTime.Today;
				}
                else if (ts.GridColumnStyles[i].ColumnType == GridColumnType.BoolColumn) 
				{
					dcol=new DataColumn(ts.GridColumnStyles[i].MappingName);
					dcol.DataType= typeof(bool);
					dcol.DefaultValue=true;
				}
				else
				{
					dcol=new DataColumn(ts.GridColumnStyles[i].MappingName , typeof(string));
					dcol.DefaultValue="";
				}
			
				dmTable.Columns.Add(dcol);
			}		
			
			FillDataTable(dmTable,cnt,rowsSample);
			dmTable.DefaultView.AllowNew =false;
			return dmTable;
		}

        private void SetDataSource(string mappingName, GridColumnCollection Columns)
        {
            //try
            //{
                int cnt = Columns.Count;
 
                list = new int[cnt];
                DataTable dmTable = new DataTable(mappingName);
                System.Data.DataColumn dcol = null;
      
                //Build Schema Data source
                for (int i = 0; i < cnt; i++)
                {
                    list.SetValue(Columns[i].Width, i);
 
                    if (Columns[i].ColumnType == GridColumnType.DateTimeColumn)
                    {
                        dcol = new DataColumn(Columns[i].MappingName, typeof(DateTime));
                        dcol.DefaultValue = DateTime.Today;
                    }
                    else if (Columns[i].ColumnType == GridColumnType.BoolColumn)
                    {
                        dcol = new DataColumn(Columns[i].MappingName, typeof(bool));
                        dcol.DefaultValue = true;
                    }
                    else
                    {
                        dcol = new DataColumn(Columns[i].MappingName, typeof(string));
                        dcol.DefaultValue = "";
                    }
                    dmTable.Columns.Add(dcol);
                }

                FillDataTable(dmTable, cnt, rowsSample);
                dmTable.DefaultView.AllowNew = false;
                this.gridDesigner.DataMember = "";
                this.gridDesigner.MappingName = mappingName;
                this.gridDesigner.SetDataBinding(dmTable,"");
                this.gridDesigner.DataSource = dmTable.DefaultView;
                //this.gridDesigner.Init(dmTable.DefaultView, "", mappingName);
                //MessageBox.Show("ok2.2.6");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
        /// <summary>
        /// Fill DataTable
        /// </summary>
        /// <param name="dmTable"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
		public  void FillDataTable(System.Data.DataTable dmTable ,int cols,int rows) 
		{
			
			DataRow dr =null;
			for (int i = 0; i < rows; i++) 
			{
				dr = dmTable.NewRow();

				for (int j = 0; j < cols; j++) 
				{
					try
					{
						System.Type type=dmTable.Columns[j].DataType;
						if(type== typeof(DateTime)) 
						{
							dr[j] =DateTime.Today;// "01/01/2000" ;
						}
						else if(type== typeof(int) || type== typeof(long) || type== typeof(byte) ) 
							dr[j] =10 ;
						else if(type== typeof(decimal) || type== typeof(double)|| type== typeof(short)) 
							dr[j] =10.00 ;
						else if(type== typeof(bool)) 
						{
							dr[j] =true ;
						}
						else
							dr[j] ="dr " + i.ToString() + ":" + j.ToString () ;
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
				dmTable.Rows.Add(dr);
			}
		}
		private void SettingColumns()
		{
			//MessageBox.Show(this.gridDesigner.Height.ToString());
		
			int rows=rowsSample;
			int rowHeight=gridDesigner.PreferredRowHeight;
			int headerAdd=gridDesigner.ColumnHeadersVisible  ? Grid.DefaultColumnHeaderHeight:0 ;
			int captionHeight=gridDesigner.CaptionVisible  ? Grid.DefaultCaptionHeight :0;
			int calcRowsHeight=rows*rowHeight;
			int scrollAdd= this.chkScroll.Checked ? (int)(rowHeight/2)*-1:(int)rowHeight ;
			this.gridDesigner.Height = calcRowsHeight+headerAdd+ captionHeight+scrollAdd;  
			//MessageBox.Show(this.gridDesigner.Height.ToString());
   
			this.Invalidate(); 
			//this.gridDesigner.AdjustColumns(this.chkScroll.Checked);// (this.gridDesigner);    
		}


		private void SetAdjustColumns()
		{
			int rows=rowsSample ;
			int rowHeight=gridDesigner.PreferredRowHeight;
			int headerAdd=gridDesigner.ColumnHeadersVisible  ? Grid.DefaultColumnHeaderHeight:0 ;
			int captionHeight=gridDesigner.CaptionVisible  ? Grid.DefaultCaptionHeight :0;
			int calcRowsHeight=rows*rowHeight; 
			int scrollAdd= this.chkScroll.Checked ? -rowHeight:rowHeight ;
		         
//			this.gridDesigner.Height =captionHeight+calcRowsHeight+scrollAdd; 
//			if(this.chkScroll.Checked )
//			{
//				this.gridDesigner.Height =gridDefaultHeight;//gridHeight-captionHeight;//(cnt-1)* gridDesigner.Tables[0].PreferredRowHeight + statusHeight;  
//			}
//			else
//			{
//				this.gridDesigner.Height =((2+rowsSample)* gridDesigner.PreferredRowHeight) + captionHeight;  
//			}

			this.Invalidate();
            //this.gridDesigner.OnDesignAdjustColumns(this.chkScroll.Checked);
            /*bound*/
			this.gridDesigner.AdjustColumns(this.chkScroll.Checked,false);// (this.gridDesigner);    
		}

		private void SetGridStyles()
		{
			if(!this.comboStyles.Enabled)
			{
				//this.BackColor=this.gridDesigner.GridLayout.ColorBrush2;
				goto Label_01;
			}
				object o=this.comboStyles.SelectedItem;
		
				if(o==null)
					o="Desktop";
				Styles style=(Styles)Enum.Parse(typeof(Styles),o.ToString());
				if(style==Styles.None)
					goto Label_01;    
            
	
			gridDesigner.SetStyleLayout(style);
			this.StyleGuideBase.StylePlan=style;

			Label_01:
	
			gridDesigner.FlatMode =this.chkFlat.Checked; 
			if(this.chkAlternating.Checked)
				gridDesigner.PaintAlternating=true;//.StatusBarMode=Nistec.GridView.Controls.StatusBarMode.Show;
			else
				gridDesigner.PaintAlternating=false;//StatusBarMode=Nistec.GridView.Controls.StatusBarMode.Hide;
		
			//gridDesigner.Grid.SetGridInColumnInternal (); 
		
		}

		#endregion

		#region ControlsEvents

		private void chkScroll_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!created)
				return;
			SettingColumns();

//			//int statusHeight=0;// this.gridDesigner.IsStatusBarVisible   ? this.gridDesigner.StatusBar.Height : 0;    
//			int captionHeight=this.gridDesigner.CaptionVisible ? Grid.DefaultCaptionHeight:0;
//			int colHeader=this.gridDesigner.ColumnHeadersVisible ? Grid.DefaultColumnHeaderHeight:0;
//			
//
//			if(this.chkScroll.Checked )
//			{
//				//this.gridDesigner.Height =gridDefaultHeight;//gridHeight-captionHeight;//(cnt-1)* gridDesigner.Tables[0].PreferredRowHeight + statusHeight;  
//				this.gridDesigner.Height =2+((rowsSample-1) * gridDesigner.PreferredRowHeight) + colHeader +captionHeight;  
//			}
//			else
//			{
//				this.gridDesigner.Height =2+((rowsSample)* gridDesigner.PreferredRowHeight) + colHeader +captionHeight;  
//			}
			//SetAdjustColumns();
			//this.Invalidate(); 
			//this.gridDesigner.AdjustColumns(this.chkScroll.Checked);    

			
		}

		private void widthPixels_ValueChanged(object sender, EventArgs e)
		{
			if(!created)
				return;  
			//if(this.gridDesigner.Width >= this.widthPixels.Value)
			//{
			this.gridDesigner.Width=(int)this.widthPixels.Value;
			//this.Width= this.gridDesigner.Width+40;
//			if(this.checkBox2.Checked )
//			{
//				//SetAdjustColumns();
//			}
			//}
		}

		private void comboStyles_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			if(created)
				SetGridStyles();  
		}

		private void chkFlat_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
				SetGridStyles();  
		}

		private void chkAlternating_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
				SetGridStyles();  
		}

		private void chkCaption_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
			{
				gridDesigner.CaptionVisible =this.chkCaption.Checked; 
				SettingColumns();
			}

		} 

//		private void comboTables_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			if(this.comboTables.SelectedItem!=null)
//			{
//				string mapName=this.comboTables.SelectedItem.ToString();
//				this.gridDesigner.DataMember=mapName;
//				GetColumnsWidth(this.comboTables.SelectedIndex);
//	
//				//this.gridDesigner.SetTableStyles(true);
//
//				//SetDataSource(mapName,this.gridDesigner.Tables.GetTable(mapName).Columns);  
//			}
//		
//		}

		private void chkColHeaders_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
			{
				this.gridDesigner.ColumnHeadersVisible=this.chkColHeaders.Checked;
				SettingColumns();
			}
		}

		private void chkRowHeaders_CheckedChanged(object sender, System.EventArgs e)
		{
			if(created)
			{
				this.gridDesigner.RowHeadersVisible=this.chkRowHeaders.Checked;
				SettingColumns();
			}
		}

//		private void btnSaveTableStyles_Click(object sender, System.EventArgs e)
//		{
//
//			int indx=this.comboTables.SelectedIndex;
//			this.gridDesigner.ColumnHeadersVisible=this.chkColHeaders.Checked;
//			this.gridDesigner.RowHeadersVisible=this.chkRowHeaders.Checked;
//			for(int i=0;i<this.gridDesigner.TableStyles[indx].GridColumnStyles.Count;i++)
//			{
//				gridDesigner.TableStyles[indx].GridColumnStyles[i].Width=gridDesigner.TableStyles[indx].GridColumnStyles[i].Width;
//			}
//			SetColumnsWidth(indx);
//	
//			//this.gridDesigner.Update();
//
//		}

		#endregion

		#region WizClick

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			try
			{

//				if(gridDesigner.TableStyles.Count>0)
//				{
//					//gridDesigner.TableStyles.Clear();
//				
//					for (int i=0; i< gridDesigner.TableStyles.Count ; i++)
//					{
//						GridTableStyle ts=(GridTableStyle)gridDesigner.TableStyles[i];
//						//gridParent.RowHeadersVisible=ts.RowHeadersVisible; 
//						//gridParent.ColumnHeadersVisible=ts.ColumnHeadersVisible; 
//						for (int j=0; j< ts.GridColumnStyles.Count ; j++)
//						{
//							gridParent.TableStyles[i].GridColumnStyles[j].Width=ts.GridColumnStyles[j].Width;
//						}
//						gridParent.ColumnHeadersVisible=gridDesigner.ColumnHeadersVisible;
//						gridParent.RowHeadersVisible=this.chkRowHeaders.Checked;// gridDesigner.RowHeadersVisible;
//					}
//				}

				if(gridDesigner.Columns.Count >0)
				{
					for(int j=0;j< gridDesigner.Columns.Count;j++)
					{
						gridParent.Columns[j].Width=gridDesigner.Columns[j].Width;
					}
					gridParent.ColumnHeadersVisible=gridDesigner.ColumnHeadersVisible;
					gridParent.RowHeadersVisible=this.chkRowHeaders.Checked;// gridDesigner.RowHeadersVisible;
				}

				gridParent.CaptionVisible=gridDesigner.CaptionVisible;   
				gridParent.FlatMode =gridDesigner.FlatMode;
				//gridParent.StatusBarMode =gridDesigner.StatusBarMode;
				gridParent.PaintAlternating=gridDesigner.PaintAlternating;
				gridParent.SetStyleLayout(gridDesigner.LayoutManager.Layout);
				//MessageBox.Show("ok");

				gridParent.Update();   
			
				this.DialogResult =DialogResult.OK ;
				this.Close();
				
			}
			catch(Exception ex)
			{
				MsgBox.ShowError (ex.Message );
			}
			finally
			{
				//base.OnYesClick (sender, e);
				//this.Close ();
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult =DialogResult.No ;
			this.Close ();

		}

		private void btnAdjst_Click(object sender, System.EventArgs e)
		{
            this.gridDesigner.OnResizeAdjustColumns();
            /*bound*/
			//this.gridDesigner.AdjustColumns(this.chkScroll.Checked,false);// (this.gridDesigner);    
		}

		#endregion

		#region Override
        /// <summary>
        /// On Resize
        /// </summary>
        /// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			if(this.Width < 340)
			{
				this.Width=340; 
			}
    
		}
        /// <summary>
        /// On Activated
        /// </summary>
        /// <param name="e"></param>
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated (e);
			if(created)
				SetGridStyles(); 
		}

        /// <summary>
        /// OnClosed
        /// </summary>
        /// <param name="e"></param>
		protected override void OnClosed(EventArgs e)
		{

			try
			{

				base.OnClosed (e);
				this.gridParent.ReadOnly=this.readOnly;

                foreach (GridColumnStyle c in gridDesigner.Columns)
                {
                    if (!c.IsBound)
                    {
                        c.MappingName = "";
                    }
                }

				if(this.DialogResult ==DialogResult.OK )
					return;
//				if(this.tables!=null)
//				{
//					//gridDesigner.TableStyles.Clear();
//				
//					for (int i=0; i< tables.Length ; i++)
//					{
//						GridTableStyle ts=(GridTableStyle)tables.GetValue(i);
//						//gridDesigner.RowHeadersVisible=ts.RowHeadersVisible; 
//						//gridDesigner.ColumnHeadersVisible=ts.ColumnHeadersVisible; 
//						for (int j=0; j< ColsCount[i]; j++)
//						{
//							gridDesigner.TableStyles[i].GridColumnStyles[j].Width=listCol[i,j]; 
//						}
//					}
//				}
//				else
//				{
					//gridDesigner.Columns.Clear ();
				
					for (int i=0; i< list.Length ; i++)
					{
						gridDesigner.Columns[i].Width=list[i]; 
					}
//				}
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
