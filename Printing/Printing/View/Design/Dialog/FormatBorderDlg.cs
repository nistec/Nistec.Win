using System;
using System.Windows.Forms;
using System.Drawing;
using MControl.Printing.View;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Resources;

//using MControl.WinForms;

namespace MControl.Printing.View.Design
{
    public class FormatBorderDlg : System.Windows.Forms.Form//MControl.WinForms.FormBase
	{

		

		#region Constructors

		public FormatBorderDlg()
		{
			//netFramwork.//NetReflectedFram();
			this.InitializeComponent();
			//NetReflectedFram();
			this.SetLineStyle();
			this.SetPreset();
			this.ctlDropDown1.SetSelectedColor(Color.Black);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Initilaize

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormatBorderDlg));
            this.cbCancel = new Button();
            this.cbOK = new Button();
            this.gbPreview = new GroupBox();
            this.lblPreview = new System.Windows.Forms.Label();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.lblLineColor = new System.Windows.Forms.Label();
            this.lbLineStyle = new System.Windows.Forms.ListBox();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.ImageListPreset = new System.Windows.Forms.ImageList(this.components);
            this.ctlDropDown1 = new MultiPicker();
            this.ctlToolBar1 = new ToolBar();
            this.ctlToolButton12 = new ToolStripButton();
            this.ctlToolButton11 = new ToolStripButton();
            this.ctlToolButton10 = new ToolStripButton();
            this.ctlToolButton9 = new ToolStripButton();
            this.ctlToolButton8 = new ToolStripButton();
            this.ctlToolButton7 = new ToolStripButton();
            this.ctlToolButton6 = new ToolStripButton();
            this.ctlToolButton5 = new ToolStripButton();
            this.ctlToolButton4 = new ToolStripButton();
            this.ctlToolButton3 = new ToolStripButton();
            this.ctlToolButton2 = new ToolStripButton();
            this.ctlToolButton1 = new ToolStripButton();
            this.ctlGroupBox1 = new GroupBox();
            this.gbPreview.SuspendLayout();
            this.ctlToolBar1.SuspendLayout();
            this.ctlGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCancel
            // 
            this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cbCancel.Location = new System.Drawing.Point(133, 250);
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.Size = new System.Drawing.Size(64, 24);
            this.cbCancel.StylePainter = this.StyleGuideBase;
            this.cbCancel.TabIndex = 18;
            this.cbCancel.Text = "Cancel";
            this.cbCancel.ToolTipText = "Cancel";
            this.cbCancel.Click += new System.EventHandler(this.cbCancel_Click);
            // 
            // cbOK
            // 
            this.cbOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cbOK.Location = new System.Drawing.Point(205, 250);
            this.cbOK.Name = "cbOK";
            this.cbOK.Size = new System.Drawing.Size(64, 24);
            this.cbOK.StylePainter = this.StyleGuideBase;
            this.cbOK.TabIndex = 17;
            this.cbOK.Text = "OK";
            this.cbOK.ToolTipText = "OK";
            this.cbOK.Click += new System.EventHandler(this.cbOK_Click);
            // 
            // gbPreview
            // 
            this.gbPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.gbPreview.Controls.Add(this.lblPreview);
            this.gbPreview.Controls.Add(this.pnlPreview);
            this.gbPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.gbPreview.ForeColor = System.Drawing.Color.Black;
            this.gbPreview.Location = new System.Drawing.Point(133, 34);
            this.gbPreview.Name = "gbPreview";
            this.gbPreview.ReadOnly = false;
            this.gbPreview.Size = new System.Drawing.Size(136, 210);
            this.gbPreview.StylePainter = this.StyleGuideBase;
            this.gbPreview.TabIndex = 16;
            this.gbPreview.TabStop = false;
            this.gbPreview.Text = "Preview";
            // 
            // lblPreview
            // 
            this.lblPreview.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreview.Location = new System.Drawing.Point(8, 160);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(120, 40);
            this.lblPreview.TabIndex = 1;
            this.lblPreview.Text = "Click on diagram above or use tool bar to edit borders";
            // 
            // pnlPreview
            // 
            this.pnlPreview.BackColor = System.Drawing.Color.White;
            this.pnlPreview.Location = new System.Drawing.Point(8, 24);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(120, 120);
            this.pnlPreview.TabIndex = 0;
            this.pnlPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlPreview_MouseDown);
            this.pnlPreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlPreview_MouseMove);
            this.pnlPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPreview_Paint);
            // 
            // lblLineColor
            // 
            this.lblLineColor.Location = new System.Drawing.Point(8, 160);
            this.lblLineColor.Name = "lblLineColor";
            this.lblLineColor.Size = new System.Drawing.Size(56, 16);
            this.lblLineColor.TabIndex = 13;
            this.lblLineColor.Text = "LineColor";
            // 
            // lbLineStyle
            // 
            this.lbLineStyle.ColumnWidth = 50;
            this.lbLineStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbLineStyle.ItemHeight = 18;
            this.lbLineStyle.Location = new System.Drawing.Point(8, 24);
            this.lbLineStyle.MultiColumn = true;
            this.lbLineStyle.Name = "lbLineStyle";
            this.lbLineStyle.Size = new System.Drawing.Size(110, 130);
            this.lbLineStyle.TabIndex = 11;
            this.lbLineStyle.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.bLineStyle_DrawItem);
            this.lbLineStyle.SelectedIndexChanged += new System.EventHandler(this.lbLineStyle_SelectedIndexChanged);
            // 
            // ImageListPreset
            // 
            this.ImageListPreset.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListPreset.ImageStream")));
            this.ImageListPreset.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListPreset.Images.SetKeyName(0, "");
            this.ImageListPreset.Images.SetKeyName(1, "");
            this.ImageListPreset.Images.SetKeyName(2, "");
            this.ImageListPreset.Images.SetKeyName(3, "");
            this.ImageListPreset.Images.SetKeyName(4, "");
            this.ImageListPreset.Images.SetKeyName(5, "");
            this.ImageListPreset.Images.SetKeyName(6, "");
            this.ImageListPreset.Images.SetKeyName(7, "");
            this.ImageListPreset.Images.SetKeyName(8, "");
            this.ImageListPreset.Images.SetKeyName(9, "");
            this.ImageListPreset.Images.SetKeyName(10, "");
            this.ImageListPreset.Images.SetKeyName(11, "");
            // 
            // ctlDropDown1
            // 
            this.ctlDropDown1.ButtonToolTip = "Color list box";
            this.ctlDropDown1.DefaultValue = "Black";
            this.ctlDropDown1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ctlDropDown1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlDropDown1.FontSize = 8.25F;
            this.ctlDropDown1.IntegralHeight = false;
            this.ctlDropDown1.ItemHeight = 14;
            this.ctlDropDown1.Location = new System.Drawing.Point(8, 176);
            this.ctlDropDown1.Name = "ctlDropDown1";
            this.ctlDropDown1.PickerType = MControl.WinForms.PickerType.Colors;
            this.ctlDropDown1.Size = new System.Drawing.Size(112, 20);
            this.ctlDropDown1.StylePainter = this.StyleGuideBase;
            this.ctlDropDown1.TabIndex = 20;
            this.ctlDropDown1.Text = "Black";
            this.ctlDropDown1.SelectedIndexChanged += new System.EventHandler(this.ctlDropDown1_SelectedIndexChanged);
            // 
            // ctlToolBar1
            // 
            this.ctlToolBar1.BackColor = System.Drawing.Color.Silver;
            this.ctlToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlToolBar1.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            this.ctlToolBar1.Controls.Add(this.ctlToolButton12);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton11);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton10);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton9);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton8);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton7);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton6);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton5);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton4);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton3);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton2);
            this.ctlToolBar1.Controls.Add(this.ctlToolButton1);
            this.ctlToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlToolBar1.FixSize = false;
            this.ctlToolBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlToolBar1.ImageList = this.ImageListPreset;
            this.ctlToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlToolBar1.Name = "ctlToolBar1";
            this.ctlToolBar1.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.ctlToolBar1.Size = new System.Drawing.Size(276, 28);
            this.ctlToolBar1.StylePainter = this.StyleGuideBase;
            this.ctlToolBar1.TabIndex = 19;
            // 
            // ctlToolButton12
            // 
            this.ctlToolButton12.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton12.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton12.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton12.ImageIndex = 11;
            this.ctlToolButton12.ImageList = this.ImageListPreset;
            this.ctlToolButton12.Location = new System.Drawing.Point(254, 3);
            this.ctlToolButton12.Name = "ctlToolButton12";
            //this.ctlToolButton12.ParentBar = this.ctlToolBar1;
            this.ctlToolButton12.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton12.TabIndex = 11;
            this.ctlToolButton12.ToolTipText = "";
            this.ctlToolButton12.Click += new System.EventHandler(this.ctlToolButton12_Click);
            // 
            // ctlToolButton11
            // 
            this.ctlToolButton11.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton11.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton11.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton11.ImageIndex = 10;
            this.ctlToolButton11.ImageList = this.ImageListPreset;
            this.ctlToolButton11.Location = new System.Drawing.Point(232, 3);
            this.ctlToolButton11.Name = "ctlToolButton11";
            //this.ctlToolButton11.ParentBar = this.ctlToolBar1;
            this.ctlToolButton11.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton11.TabIndex = 10;
            this.ctlToolButton11.ToolTipText = "";
            this.ctlToolButton11.Click += new System.EventHandler(this.ctlToolButton11_Click);
            // 
            // ctlToolButton10
            // 
            this.ctlToolButton10.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton10.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton10.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton10.ImageIndex = 9;
            this.ctlToolButton10.ImageList = this.ImageListPreset;
            this.ctlToolButton10.Location = new System.Drawing.Point(210, 3);
            this.ctlToolButton10.Name = "ctlToolButton10";
            //this.ctlToolButton10.ParentBar = this.ctlToolBar1;
            this.ctlToolButton10.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton10.TabIndex = 9;
            this.ctlToolButton10.ToolTipText = "";
            this.ctlToolButton10.Click += new System.EventHandler(this.ctlToolButton10_Click);
            // 
            // ctlToolButton9
            // 
            this.ctlToolButton9.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton9.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton9.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton9.ImageIndex = 8;
            this.ctlToolButton9.ImageList = this.ImageListPreset;
            this.ctlToolButton9.Location = new System.Drawing.Point(188, 3);
            this.ctlToolButton9.Name = "ctlToolButton9";
            //this.ctlToolButton9.ParentBar = this.ctlToolBar1;
            this.ctlToolButton9.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton9.TabIndex = 8;
            this.ctlToolButton9.ToolTipText = "";
            this.ctlToolButton9.Click += new System.EventHandler(this.ctlToolButton9_Click);
            // 
            // ctlToolButton8
            // 
            this.ctlToolButton8.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton8.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton8.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton8.ImageIndex = 7;
            this.ctlToolButton8.ImageList = this.ImageListPreset;
            this.ctlToolButton8.Location = new System.Drawing.Point(166, 3);
            this.ctlToolButton8.Name = "ctlToolButton8";
            //this.ctlToolButton8.ParentBar = this.ctlToolBar1;
            this.ctlToolButton8.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton8.TabIndex = 7;
            this.ctlToolButton8.ToolTipText = "";
            this.ctlToolButton8.Click += new System.EventHandler(this.ctlToolButton8_Click);
            // 
            // ctlToolButton7
            // 
            this.ctlToolButton7.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton7.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton7.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton7.ImageIndex = 6;
            this.ctlToolButton7.ImageList = this.ImageListPreset;
            this.ctlToolButton7.Location = new System.Drawing.Point(144, 3);
            this.ctlToolButton7.Name = "ctlToolButton7";
            //this.ctlToolButton7.ParentBar = this.ctlToolBar1;
            this.ctlToolButton7.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton7.TabIndex = 6;
            this.ctlToolButton7.ToolTipText = "";
            this.ctlToolButton7.Click += new System.EventHandler(this.ctlToolButton7_Click);
            // 
            // ctlToolButton6
            // 
            this.ctlToolButton6.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton6.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton6.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton6.ImageIndex = 5;
            this.ctlToolButton6.ImageList = this.ImageListPreset;
            this.ctlToolButton6.Location = new System.Drawing.Point(122, 3);
            this.ctlToolButton6.Name = "ctlToolButton6";
            //this.ctlToolButton6.ParentBar = this.ctlToolBar1;
            this.ctlToolButton6.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton6.TabIndex = 5;
            this.ctlToolButton6.ToolTipText = "";
            this.ctlToolButton6.Click += new System.EventHandler(this.ctlToolButton6_Click);
            // 
            // ctlToolButton5
            // 
            this.ctlToolButton5.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton5.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton5.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton5.ImageIndex = 4;
            this.ctlToolButton5.ImageList = this.ImageListPreset;
            this.ctlToolButton5.Location = new System.Drawing.Point(100, 3);
            this.ctlToolButton5.Name = "ctlToolButton5";
            //this.ctlToolButton5.ParentBar = this.ctlToolBar1;
            this.ctlToolButton5.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton5.TabIndex = 4;
            this.ctlToolButton5.ToolTipText = "";
            this.ctlToolButton5.Click += new System.EventHandler(this.ctlToolButton5_Click);
            // 
            // ctlToolButton4
            // 
            this.ctlToolButton4.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton4.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton4.ImageIndex = 3;
            this.ctlToolButton4.ImageList = this.ImageListPreset;
            this.ctlToolButton4.Location = new System.Drawing.Point(78, 3);
            this.ctlToolButton4.Name = "ctlToolButton4";
            //this.ctlToolButton4.ParentBar = this.ctlToolBar1;
            this.ctlToolButton4.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton4.TabIndex = 3;
            this.ctlToolButton4.ToolTipText = "";
            this.ctlToolButton4.Click += new System.EventHandler(this.ctlToolButton4_Click);
            // 
            // ctlToolButton3
            // 
            this.ctlToolButton3.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton3.ImageIndex = 2;
            this.ctlToolButton3.ImageList = this.ImageListPreset;
            this.ctlToolButton3.Location = new System.Drawing.Point(56, 3);
            this.ctlToolButton3.Name = "ctlToolButton3";
            //this.ctlToolButton3.ParentBar = this.ctlToolBar1;
            this.ctlToolButton3.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton3.TabIndex = 2;
            this.ctlToolButton3.ToolTipText = "";
            this.ctlToolButton3.Click += new System.EventHandler(this.ctlToolButton3_Click);
            // 
            // ctlToolButton2
            // 
            this.ctlToolButton2.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton2.ImageIndex = 1;
            this.ctlToolButton2.ImageList = this.ImageListPreset;
            this.ctlToolButton2.Location = new System.Drawing.Point(34, 3);
            this.ctlToolButton2.Name = "ctlToolButton2";
            //this.ctlToolButton2.ParentBar = this.ctlToolBar1;
            this.ctlToolButton2.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton2.TabIndex = 1;
            this.ctlToolButton2.ToolTipText = "";
            this.ctlToolButton2.Click += new System.EventHandler(this.ctlToolButton2_Click);
            // 
            // ctlToolButton1
            // 
            this.ctlToolButton1.ButtonStyle = ToolButtonStyle.Button;
            this.ctlToolButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlToolButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlToolButton1.ImageIndex = 0;
            this.ctlToolButton1.ImageList = this.ImageListPreset;
            this.ctlToolButton1.Location = new System.Drawing.Point(12, 3);
            this.ctlToolButton1.Name = "ctlToolButton1";
            //this.ctlToolButton1.ParentBar = this.ctlToolBar1;
            this.ctlToolButton1.Size = new System.Drawing.Size(22, 22);
            this.ctlToolButton1.TabIndex = 0;
            this.ctlToolButton1.ToolTipText = "";
            this.ctlToolButton1.Click += new System.EventHandler(this.ctlToolButton1_Click);
            // 
            // ctlGroupBox1
            // 
            this.ctlGroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlGroupBox1.Controls.Add(this.lbLineStyle);
            this.ctlGroupBox1.Controls.Add(this.ctlDropDown1);
            this.ctlGroupBox1.Controls.Add(this.lblLineColor);
            this.ctlGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlGroupBox1.ForeColor = System.Drawing.Color.Black;
            this.ctlGroupBox1.Location = new System.Drawing.Point(5, 34);
            this.ctlGroupBox1.Name = "ctlGroupBox1";
            this.ctlGroupBox1.ReadOnly = false;
            this.ctlGroupBox1.Size = new System.Drawing.Size(128, 210);
            this.ctlGroupBox1.StylePainter = this.StyleGuideBase;
            this.ctlGroupBox1.TabIndex = 21;
            this.ctlGroupBox1.TabStop = false;
            this.ctlGroupBox1.Text = "Line Style";
            // 
            // FormatBorderDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(276, 285);
            this.Controls.Add(this.ctlGroupBox1);
            this.Controls.Add(this.ctlToolBar1);
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbOK);
            this.Controls.Add(this.gbPreview);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormatBorderDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.SetChildIndex(this.gbPreview, 0);
            this.Controls.SetChildIndex(this.cbOK, 0);
            this.Controls.SetChildIndex(this.cbCancel, 0);
            this.Controls.SetChildIndex(this.ctlToolBar1, 0);
            this.Controls.SetChildIndex(this.ctlGroupBox1, 0);
            this.gbPreview.ResumeLayout(false);
            this.ctlToolBar1.ResumeLayout(false);
            this.ctlGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
 
		private void SetLineStyle()//var0()
		{
		this.lbLineStyle.BeginUpdate();
			for (int num1 = 0; num1 <= 13; num1++)
			{
				this.lbLineStyle.Items.Add(num1);
			}
			this.lbLineStyle.SelectedIndex = 1;
			this.lbLineStyle.EndUpdate();
		}

		private void SetPreset()//var1()
		{
			this._border = new Border();
			this._borderLineStyle = BorderLineStyle.Solid;
			this.lineColor = Color.Black;
			this.topf = 6f;
			this.point1 = new Point(20, 20);
			this.point2 = new Point(20, 100);
			this.point3 = new Point(100, 100);
			this.point4 = new Point(100, 20);
			this.rectF1 = new RectangleF((float) (this.point1.X - 10), (float) this.point1.Y, 20f, 80f);
			this.rectF2 = new RectangleF((float) (this.point2.X - 10), (float) this.point2.Y, 80f, 20f);
			this.rectF3 = new RectangleF((float) (this.point4.X - 10), (float) this.point4.Y, 20f, 80f);
			this.rectF4 = new RectangleF((float) this.point1.X, (float) (this.point1.Y - 10), 80f, 20f);
			this.rectF5 = new RectangleF(20f, 20f, 80f, 80f);
//			int[,] numArray1 = new int[,] { { 8, 20 }, { 8, 60 }, { 8, 100 }, { 8, 140 }, { 8, 180 }, { 8, 220 }, { 0x30, 20 }, { 0x30, 60 }, { 0x30, 100 }, { 0x30, 140 }, { 0x30, 180 }, { 0x30, 220 } };
//			for (int num1 = 0; num1 <= 11; num1++)
//			{
//				CtlInternal ctl = new CtlInternal("Preset" + num1.ToString(), "", new Size(0x20, 0x20));
//				ctl.Location = new Point(numArray1[num1, 0], numArray1[num1, 1]);
//				ctl.Image = this.ImageListPreset.Images[num1];
//				ctl.FlatStyles = FlatStyle.Standard;
//				ctl.Click += new EventHandler(this.CtlInternal_Click);
//				this.gbPreset.Controls.Add(ctl);
//			}
		}

		private void cbCancel_Click(object sender, EventArgs e)
		{
			this.DlgResult = false;
			base.Close();
		}

 
		private void CtlInternal_Click(object sender, EventArgs e)
		{
			this.SetCtlPreset(((CtlInternal) sender).Name);
		}

 
		private void SetCtlPreset(string strPreset)//var23
		{
			this._border.BorderLeftStyle = BorderLineStyle.None;
			this._border.BorderBottomStyle = BorderLineStyle.None;
			this._border.BorderRightStyle = BorderLineStyle.None;
			this._border.BorderTopStyle = BorderLineStyle.None;
			this._border.BorderLeftColor = Color.Transparent;
			this._border.BorderBottomColor = Color.Black;
			this._border.BorderRightColor = Color.Black;
			this._border.BorderTopColor = Color.Black;
			switch (strPreset)
			{
				case "Preset1":
				{
					this._border.BorderBottomStyle = BorderLineStyle.Solid;
					this._border.BorderBottomColor = this.lineColor;
					break;
				}
				case "Preset2":
				{
					this._border.BorderLeftStyle = BorderLineStyle.Solid;
					this._border.BorderLeftColor = this.lineColor;
					break;
				}
				case "Preset3":
				{
					this._border.BorderRightStyle = BorderLineStyle.Solid;
					this._border.BorderRightColor = this.lineColor;
					break;
				}
				case "Preset4":
				{
					this._border.BorderTopStyle = BorderLineStyle.Solid;
					this._border.BorderTopColor = this.lineColor;
					break;
				}
				case "Preset5":
				{
					this._border.BorderLeftStyle = BorderLineStyle.Solid;
					this._border.BorderLeftColor = this.lineColor;
					this._border.BorderBottomStyle = BorderLineStyle.Solid;
					this._border.BorderBottomColor = this.lineColor;
					this._border.BorderRightStyle = BorderLineStyle.Solid;
					this._border.BorderRightColor = this.lineColor;
					this._border.BorderTopStyle = BorderLineStyle.Solid;
					this._border.BorderTopColor = this.lineColor;
					break;
				}
				case "Preset6":
				{
					this._border.BorderBottomStyle = BorderLineStyle.Double;
					this._border.BorderBottomColor = this.lineColor;
					break;
				}
				case "Preset7":
				{
					this._border.BorderBottomStyle = BorderLineStyle.ExtraThickSolid;
					this._border.BorderBottomColor = this.lineColor;
					break;
				}
				case "Preset8":
				{
					this._border.BorderBottomStyle = BorderLineStyle.Solid;
					this._border.BorderBottomColor = this.lineColor;
					this._border.BorderTopStyle = BorderLineStyle.Solid;
					this._border.BorderTopColor = this.lineColor;
					break;
				}
				case "Preset9":
				{
					this._border.BorderBottomStyle = BorderLineStyle.Double;
					this._border.BorderBottomColor = this.lineColor;
					this._border.BorderTopStyle = BorderLineStyle.Solid;
					this._border.BorderTopColor = this.lineColor;
					break;
				}
				case "Preset10":
				{
					this._border.BorderBottomStyle = BorderLineStyle.ExtraThickSolid;
					this._border.BorderBottomColor = this.lineColor;
					this._border.BorderTopStyle = BorderLineStyle.Solid;
					this._border.BorderTopColor = this.lineColor;
					break;
				}
				case "Preset11":
				{
					this._border.BorderLeftStyle = BorderLineStyle.ExtraThickSolid;
					this._border.BorderLeftColor = this.lineColor;
					this._border.BorderBottomStyle = BorderLineStyle.ExtraThickSolid;
					this._border.BorderBottomColor = this.lineColor;
					this._border.BorderRightStyle = BorderLineStyle.ExtraThickSolid;
					this._border.BorderRightColor = this.lineColor;
					this._border.BorderTopStyle = BorderLineStyle.ExtraThickSolid;
					this._border.BorderTopColor = this.lineColor;
					break;
				}
			}
			this.pnlPreview.Invalidate();
		}

		private bool IsRectVisible(Point p, RectangleF rectF)//var24
		{
			using (GraphicsPath path1 = new GraphicsPath())
			{
				path1.AddRectangle(rectF);
				if (path1.IsVisible(p))
				{
					return true;
				}
			}
			return false;
		}

		private void cbOK_Click(object sender, EventArgs e)
		{
			this.DlgResult = true;
			base.Close();
		}

		private void pnlPreview_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics1 = e.Graphics;
			graphics1.PageUnit = GraphicsUnit.Display;
			using (Pen pen1 = new Pen(Color.DarkGray, 1f))
			{
				graphics1.DrawLine(pen1, this.point1.X - this.topf, (float) this.point1.Y, (float) this.point1.X, (float) this.point1.Y);
				graphics1.DrawLine(pen1, (float) this.point1.X, this.point1.Y - this.topf, (float) this.point1.X, (float) this.point1.Y);
				graphics1.DrawLine(pen1, (float) this.point4.X, (float) this.point4.Y, this.point4.X + this.topf, (float) this.point4.Y);
				graphics1.DrawLine(pen1, (float) this.point4.X, this.point4.Y - this.topf, (float) this.point4.X, (float) this.point4.Y);
				graphics1.DrawLine(pen1, this.point2.X - this.topf, (float) this.point2.Y, (float) this.point2.X, (float) this.point2.Y);
				graphics1.DrawLine(pen1, (float) this.point2.X, this.point2.Y + this.topf, (float) this.point2.X, (float) this.point2.Y);
				graphics1.DrawLine(pen1, (float) this.point3.X, (float) this.point3.Y, this.point3.X + this.topf, (float) this.point3.Y);
				graphics1.DrawLine(pen1, (float) this.point3.X, (float) this.point3.Y, (float) this.point3.X, this.point3.Y + this.topf);
			}
			this._border.Render(graphics1, this.rectF5);
		}

		private void pnlPreview_MouseMove(object sender, MouseEventArgs e)
		{
			Point point1 = new Point(e.X, e.Y);
			if (((this.IsRectVisible(point1, this.rectF1) | this.IsRectVisible(point1, this.rectF2)) | this.IsRectVisible(point1, this.rectF3)) | this.IsRectVisible(point1, this.rectF4))
			{
				this.pnlPreview.Cursor = Cursors.Hand;
			}
			else
			{
				this.pnlPreview.Cursor = Cursors.Default;
			}
		}

		private void pnlPreview_MouseDown(object sender, MouseEventArgs e)
		{
			Point point1 = new Point(e.X, e.Y);
			if (this.IsRectVisible(point1, this.rectF1))
			{
				this._border.BorderLeftStyle = this._borderLineStyle;
				this._border.BorderLeftColor = this.lineColor;
			}
			else if (this.IsRectVisible(point1, this.rectF2))
			{
				this._border.BorderBottomStyle = this._borderLineStyle;
				this._border.BorderBottomColor = this.lineColor;
			}
			else if (this.IsRectVisible(point1, this.rectF3))
			{
				this._border.BorderRightStyle = this._borderLineStyle;
				this._border.BorderRightColor = this.lineColor;
			}
			else if (this.IsRectVisible(point1, this.rectF4))
			{
				this._border.BorderTopStyle = this._borderLineStyle;
				this._border.BorderTopColor = this.lineColor;
			}
			((Panel) sender).Invalidate();
		}

//		private void var7(object sender, EventArgs e)
//		{
//			this.dlgColor.Color = Color.Black;
//			this.dlgColor.ShowDialog();
//			this.lineColor = this.dlgColor.Color;
//			this.lblColor.BackColor = this.lineColor;
//		}

		private void ctlDropDown1_SelectedIndexChanged(object sender, EventArgs e)
		{
			//this.dlgColor.Color = Color.Black;
			//this.dlgColor.ShowDialog();
			this.lineColor =this.ctlDropDown1.GetSelectedColor();
			//this.lblColor.BackColor = this.lineColor;

		}

		private void bLineStyle_DrawItem(object sender, DrawItemEventArgs e)
		{
			Brush brush1;
			Color color1;
			Rectangle rectangle1 = e.Bounds;
			if ((e.State & DrawItemState.Selected) > DrawItemState.None)
			{
				e.Graphics.FillRectangle(SystemBrushes.Highlight, rectangle1);
				brush1 = Brushes.White;
				color1 = Color.White;
			}
			else
			{
				e.Graphics.FillRectangle(SystemBrushes.Window, rectangle1);
				brush1 = Brushes.Black;
				color1 = Color.Black;
			}
			if (e.Index == 0)
			{
				using (Font font1 = new Font("Arial", 8f, FontStyle.Bold))
				{
					e.Graphics.DrawString("None", font1, brush1, (float) (rectangle1.X + 5), (float) rectangle1.Y);
					return;
				}
			}
			float single1 = rectangle1.Y + 9;
			if (e.Index == 1)
			{
				using (Pen pen1 = new Pen(color1, 1.5f))
				{
					pen1.DashStyle = DashStyle.Solid;
					e.Graphics.DrawLine(pen1, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 2)
			{
				using (Pen pen2 = new Pen(color1, 1.5f))
				{
					pen2.DashStyle = DashStyle.Dash;
					e.Graphics.DrawLine(pen2, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 3)
			{
				using (Pen pen3 = new Pen(color1, 1.5f))
				{
					pen3.DashStyle = DashStyle.Dot;
					e.Graphics.DrawLine(pen3, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 4)
			{
				using (Pen pen4 = new Pen(color1, 1.5f))
				{
					pen4.DashStyle = DashStyle.DashDot;
					e.Graphics.DrawLine(pen4, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 5)
			{
				using (Pen pen5 = new Pen(color1, 1.5f))
				{
					pen5.DashStyle = DashStyle.DashDotDot;
					e.Graphics.DrawLine(pen5, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 6)
			{
				single1 -= 2f;
				using (Pen pen6 = new Pen(color1, 1.5f))
				{
					pen6.DashStyle = DashStyle.Solid;
					e.Graphics.DrawLine(pen6, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					e.Graphics.DrawLine(pen6, (float) (rectangle1.X + 5), single1 + 2f, (float) (rectangle1.X + 40), single1 + 2f);
					return;
				}
			}
			if (e.Index == 7)
			{
				using (Pen pen7 = new Pen(color1, 2f))
				{
					pen7.DashStyle = DashStyle.Solid;
					e.Graphics.DrawLine(pen7, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 8)
			{
				using (Pen pen8 = new Pen(color1, 2f))
				{
					pen8.DashStyle = DashStyle.Dash;
					e.Graphics.DrawLine(pen8, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 9)
			{
				using (Pen pen9 = new Pen(color1, 2f))
				{
					pen9.DashStyle = DashStyle.Dot;
					e.Graphics.DrawLine(pen9, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 10)
			{
				using (Pen pen10 = new Pen(color1, 2f))
				{
					pen10.DashStyle = DashStyle.DashDot;
					e.Graphics.DrawLine(pen10, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 11)
			{
				using (Pen pen11 = new Pen(color1, 2f))
				{
					pen11.DashStyle = DashStyle.DashDotDot;
					e.Graphics.DrawLine(pen11, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					return;
				}
			}
			if (e.Index == 12)
			{
				single1 -= 2f;
				using (Pen pen12 = new Pen(color1, 2f))
				{
					pen12.DashStyle = DashStyle.Solid;
					e.Graphics.DrawLine(pen12, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
					e.Graphics.DrawLine(pen12, (float) (rectangle1.X + 5), single1 + 4f, (float) (rectangle1.X + 40), single1 + 4f);
					return;
				}
			}
			if (e.Index == 13)
			{
				using (Pen pen13 = new Pen(color1, 3f))
				{
					pen13.DashStyle = DashStyle.Solid;
					e.Graphics.DrawLine(pen13, (float) (rectangle1.X + 5), single1, (float) (rectangle1.X + 40), single1);
				}
			}
		}

		private void lbLineStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			this._borderLineStyle = (BorderLineStyle) this.lbLineStyle.SelectedIndex;
		}

		public Border BorderCtl //mtd99
		{
			get
			{
				return this._border;
			}
			set
			{
				this._border = value;
			}
		}

		#region Members

		// Fields
		internal Border _border;//mtd99;
		private BorderLineStyle _borderLineStyle;//var10;
		private Color lineColor;//var11;
		private float topf;//var12;
		private Point point1;//var13;
		private Point point2;//var14;
		private Point point3;//var15;
		private Point point4;//var16;
		private RectangleF rectF1;//var17;
		private RectangleF rectF2;//var18;
		private RectangleF rectF3;//var19;
		private RectangleF rectF4;//var20;
		private RectangleF rectF5;//var21;
		internal bool DlgResult;//mtd4;
		internal Button cbCancel;
		internal Button cbOK;
		private IContainer components;
		internal ColorDialog dlgColor;
		internal GroupBox gbPreview;
		internal ListBox lbLineStyle;
		internal Label lblLineColor;
		internal Label lblPreview;
		private System.Windows.Forms.ImageList ImageListPreset;
		private MultiPicker ctlDropDown1;
		private ToolBar ctlToolBar1;
		private ToolStripButton ctlToolButton12;
		private ToolStripButton ctlToolButton11;
		private ToolStripButton ctlToolButton10;
		private ToolStripButton ctlToolButton9;
		private ToolStripButton ctlToolButton8;
		private ToolStripButton ctlToolButton7;
		private ToolStripButton ctlToolButton6;
		private ToolStripButton ctlToolButton5;
		private ToolStripButton ctlToolButton4;
		private ToolStripButton ctlToolButton3;
		private ToolStripButton ctlToolButton2;
		private ToolStripButton ctlToolButton1;
		private GroupBox ctlGroupBox1;
		internal Panel pnlPreview;

		#endregion

		#region Pereset Buttons 
		private void ctlToolButton1_Click(object sender, System.EventArgs e)
		{
		  SetCtlPreset("Preset0");
		}

		private void ctlToolButton2_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset1");
		}

		private void ctlToolButton3_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset2");
		}

		private void ctlToolButton4_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset3");
		}

		private void ctlToolButton5_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset4");
		}

		private void ctlToolButton6_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset5");
		}

		private void ctlToolButton7_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset6");
		}

		private void ctlToolButton8_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset7");
		}

		private void ctlToolButton9_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset8");
		}

		private void ctlToolButton10_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset9");
		}

		private void ctlToolButton11_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset10");
		}

		private void ctlToolButton12_Click(object sender, System.EventArgs e)
		{
			SetCtlPreset("Preset11");
		}
		#endregion

	}

}
