using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.IO;
using mControl.WinCtl.Controls;
using mControl.Printing;
using mControl.WinCtl.Printing;
using mControl.Data;

namespace mControl.WinCtl.Dlg
{
	public class PrintDB : mControl.WinCtl.Forms.CtlForm
	{
		#region Members
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel1;
		private mControl.WinCtl.Controls.CtlButton btnOpen;
		private mControl.WinCtl.Controls.CtlTextBox ctlConnectionString;
		private mControl.WinCtl.Controls.CtlListBox listTables;
		private mControl.WinCtl.Controls.CtlListBox listViews;
		private mControl.WinCtl.Controls.CtlTextBox txtScript;
		private mControl.WinCtl.Controls.CtlButton btnPreview;
		private mControl.WinCtl.Controls.CtlButton btnExit;
		private mControl.WinCtl.Controls.CtlComboBox ctlProviders;
		private mControl.WinCtl.Controls.CtlTabControl ctlTabControl;
		private mControl.WinCtl.Controls.CtlTabPage ctlTabTables;
		private mControl.WinCtl.Controls.CtlTabPage ctlTabViews;
		private mControl.WinCtl.Controls.CtlTabPage ctlTabScript;
		private mControl.WinCtl.Controls.CtlTabPage ctlTabSetting;
		private mControl.WinCtl.Controls.CtlGroupBox Alignment;
		private mControl.WinCtl.Controls.CtlRadioButton radioCenter;
		private mControl.WinCtl.Controls.CtlRadioButton radioLeft;
		private mControl.WinCtl.Controls.CtlRadioButton radioRight;
		private mControl.WinCtl.Controls.CtlSpinEdit ctlMaxColWidth;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel2;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel3;
		private mControl.WinCtl.Controls.CtlSpinEdit ctlTop;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel4;
		private mControl.WinCtl.Controls.CtlSpinEdit ctlBottom;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel5;
		private mControl.WinCtl.Controls.CtlSpinEdit ctlLeft;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel6;
		private mControl.WinCtl.Controls.CtlSpinEdit ctlRight;
		private mControl.WinCtl.Controls.CtlButton btnMinimized;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;

		#endregion

		#region Ctor

		public PrintDB()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitCombo();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintDB));
            this.btnOpen = new mControl.WinCtl.Controls.CtlButton();
            this.ctlConnectionString = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlTabControl = new mControl.WinCtl.Controls.CtlTabControl();
            this.ctlTabTables = new mControl.WinCtl.Controls.CtlTabPage();
            this.listTables = new mControl.WinCtl.Controls.CtlListBox();
            this.ctlTabViews = new mControl.WinCtl.Controls.CtlTabPage();
            this.listViews = new mControl.WinCtl.Controls.CtlListBox();
            this.ctlTabScript = new mControl.WinCtl.Controls.CtlTabPage();
            this.txtScript = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlTabSetting = new mControl.WinCtl.Controls.CtlTabPage();
            this.ctlLabel6 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlRight = new mControl.WinCtl.Controls.CtlSpinEdit();
            this.ctlLabel5 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlLeft = new mControl.WinCtl.Controls.CtlSpinEdit();
            this.ctlLabel4 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlBottom = new mControl.WinCtl.Controls.CtlSpinEdit();
            this.ctlLabel3 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlTop = new mControl.WinCtl.Controls.CtlSpinEdit();
            this.ctlLabel2 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlMaxColWidth = new mControl.WinCtl.Controls.CtlSpinEdit();
            this.Alignment = new mControl.WinCtl.Controls.CtlGroupBox();
            this.radioRight = new mControl.WinCtl.Controls.CtlRadioButton();
            this.radioCenter = new mControl.WinCtl.Controls.CtlRadioButton();
            this.radioLeft = new mControl.WinCtl.Controls.CtlRadioButton();
            this.ctlProviders = new mControl.WinCtl.Controls.CtlComboBox();
            this.btnPreview = new mControl.WinCtl.Controls.CtlButton();
            this.btnExit = new mControl.WinCtl.Controls.CtlButton();
            this.ctlLabel1 = new mControl.WinCtl.Controls.CtlLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnMinimized = new mControl.WinCtl.Controls.CtlButton();
            this.ctlTabControl.SuspendLayout();
            this.ctlTabTables.SuspendLayout();
            this.ctlTabViews.SuspendLayout();
            this.ctlTabScript.SuspendLayout();
            this.ctlTabSetting.SuspendLayout();
            this.Alignment.SuspendLayout();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.caption.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Visual;
            this.caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.caption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.caption.Location = new System.Drawing.Point(2, 2);
            this.caption.Name = "caption";
            this.caption.ShowFormBox = true;
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.Text = "PrintDB";
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = mControl.WinCtl.Controls.Styles.Desktop;
            // 
            // btnOpen
            // 
            this.btnOpen.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Visual;
            this.btnOpen.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOpen.Location = new System.Drawing.Point(16, 88);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(64, 20);
            this.btnOpen.StylePainter = this.StyleGuideBase;
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Open..";
            this.btnOpen.ToolTipText = "Open..";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click_1);
            // 
            // ctlConnectionString
            // 
            this.ctlConnectionString.BackColor = System.Drawing.Color.White;
            this.ctlConnectionString.ForeColor = System.Drawing.Color.Black;
            this.ctlConnectionString.Location = new System.Drawing.Point(88, 88);
            this.ctlConnectionString.Name = "ctlConnectionString";
            this.ctlConnectionString.ReadOnly = true;
            this.ctlConnectionString.Size = new System.Drawing.Size(248, 20);
            this.ctlConnectionString.StylePainter = this.StyleGuideBase;
            this.ctlConnectionString.TabIndex = 0;
            // 
            // ctlTabControl
            // 
            this.ctlTabControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlTabControl.ControlLayout = mControl.WinCtl.Controls.ControlLayout.XpLayout;
            this.ctlTabControl.Controls.Add(this.ctlTabTables);
            this.ctlTabControl.Controls.Add(this.ctlTabViews);
            this.ctlTabControl.Controls.Add(this.ctlTabScript);
            this.ctlTabControl.Controls.Add(this.ctlTabSetting);
            this.ctlTabControl.ItemSize = new System.Drawing.Size(78, 22);
            this.ctlTabControl.Location = new System.Drawing.Point(8, 120);
            this.ctlTabControl.Name = "ctlTabControl";
            this.ctlTabControl.Size = new System.Drawing.Size(328, 232);
            this.ctlTabControl.StylePainter = this.StyleGuideBase;
            this.ctlTabControl.TabIndex = 4;
            this.ctlTabControl.TabPages.AddRange(new mControl.WinCtl.Controls.CtlTabPage[] {
            this.ctlTabTables,
            this.ctlTabViews,
            this.ctlTabScript,
            this.ctlTabSetting});
            this.ctlTabControl.TabStop = false;
            this.ctlTabControl.Text = "ctlTabControl2";
            // 
            // ctlTabTables
            // 
            this.ctlTabTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlTabTables.Controls.Add(this.listTables);
            this.ctlTabTables.Location = new System.Drawing.Point(4, 29);
            this.ctlTabTables.Name = "ctlTabTables";
            this.ctlTabTables.Padding = new System.Windows.Forms.Padding(4);
            this.ctlTabTables.Size = new System.Drawing.Size(319, 198);
            this.ctlTabTables.StylePainter = this.StyleGuideBase;
            this.ctlTabTables.Text = "Tables";
            // 
            // listTables
            // 
            this.listTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTables.Location = new System.Drawing.Point(4, 4);
            this.listTables.Name = "listTables";
            this.listTables.ReadOnly = false;
            this.listTables.Size = new System.Drawing.Size(311, 184);
            this.listTables.StylePainter = this.StyleGuideBase;
            this.listTables.TabIndex = 0;
            this.listTables.DoubleClick += new System.EventHandler(this.listTables_DoubleClick);
            // 
            // ctlTabViews
            // 
            this.ctlTabViews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlTabViews.Controls.Add(this.listViews);
            this.ctlTabViews.Location = new System.Drawing.Point(4, 29);
            this.ctlTabViews.Name = "ctlTabViews";
            this.ctlTabViews.Padding = new System.Windows.Forms.Padding(4);
            this.ctlTabViews.Size = new System.Drawing.Size(319, 198);
            this.ctlTabViews.StylePainter = this.StyleGuideBase;
            this.ctlTabViews.Text = "Views";
            // 
            // listViews
            // 
            this.listViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViews.Location = new System.Drawing.Point(4, 4);
            this.listViews.Name = "listViews";
            this.listViews.ReadOnly = false;
            this.listViews.Size = new System.Drawing.Size(311, 184);
            this.listViews.StylePainter = this.StyleGuideBase;
            this.listViews.TabIndex = 0;
            this.listViews.DoubleClick += new System.EventHandler(this.listViews_DoubleClick);
            // 
            // ctlTabScript
            // 
            this.ctlTabScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlTabScript.Controls.Add(this.txtScript);
            this.ctlTabScript.Location = new System.Drawing.Point(4, 29);
            this.ctlTabScript.Name = "ctlTabScript";
            this.ctlTabScript.Padding = new System.Windows.Forms.Padding(4);
            this.ctlTabScript.Size = new System.Drawing.Size(319, 198);
            this.ctlTabScript.StylePainter = this.StyleGuideBase;
            this.ctlTabScript.Text = "Script";
            // 
            // txtScript
            // 
            this.txtScript.BackColor = System.Drawing.Color.White;
            this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScript.ForeColor = System.Drawing.Color.Black;
            this.txtScript.Location = new System.Drawing.Point(4, 4);
            this.txtScript.Multiline = true;
            this.txtScript.Name = "txtScript";
            this.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtScript.Size = new System.Drawing.Size(311, 190);
            this.txtScript.StylePainter = this.StyleGuideBase;
            this.txtScript.TabIndex = 0;
            // 
            // ctlTabSetting
            // 
            this.ctlTabSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlTabSetting.Controls.Add(this.ctlLabel6);
            this.ctlTabSetting.Controls.Add(this.ctlRight);
            this.ctlTabSetting.Controls.Add(this.ctlLabel5);
            this.ctlTabSetting.Controls.Add(this.ctlLeft);
            this.ctlTabSetting.Controls.Add(this.ctlLabel4);
            this.ctlTabSetting.Controls.Add(this.ctlBottom);
            this.ctlTabSetting.Controls.Add(this.ctlLabel3);
            this.ctlTabSetting.Controls.Add(this.ctlTop);
            this.ctlTabSetting.Controls.Add(this.ctlLabel2);
            this.ctlTabSetting.Controls.Add(this.ctlMaxColWidth);
            this.ctlTabSetting.Controls.Add(this.Alignment);
            this.ctlTabSetting.Location = new System.Drawing.Point(4, 29);
            this.ctlTabSetting.Name = "ctlTabSetting";
            this.ctlTabSetting.Size = new System.Drawing.Size(319, 198);
            this.ctlTabSetting.StylePainter = this.StyleGuideBase;
            this.ctlTabSetting.Text = "Setting";
            // 
            // ctlLabel6
            // 
            this.ctlLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel6.FixSize = false;
            this.ctlLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel6.Location = new System.Drawing.Point(32, 176);
            this.ctlLabel6.Name = "ctlLabel6";
            this.ctlLabel6.Size = new System.Drawing.Size(112, 20);
            this.ctlLabel6.StylePainter = this.StyleGuideBase;
            this.ctlLabel6.Text = "Margin Right";
            this.ctlLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctlLabel6.Visible = false;
            // 
            // ctlRight
            // 
            this.ctlRight.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.ctlRight.DecimalPlaces = 0;
            this.ctlRight.DefaultValue = "1";
            this.ctlRight.Format = "N";
            this.ctlRight.FormatType = mControl.Util.NumberFormats.StandadNumber;
            this.ctlRight.Location = new System.Drawing.Point(152, 176);
            this.ctlRight.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ctlRight.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctlRight.Name = "ctlRight";
            this.ctlRight.Size = new System.Drawing.Size(104, 20);
            this.ctlRight.StylePainter = this.StyleGuideBase;
            this.ctlRight.TabIndex = 34;
            this.ctlRight.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.ctlRight.Visible = false;
            // 
            // ctlLabel5
            // 
            this.ctlLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel5.FixSize = false;
            this.ctlLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel5.Location = new System.Drawing.Point(32, 152);
            this.ctlLabel5.Name = "ctlLabel5";
            this.ctlLabel5.Size = new System.Drawing.Size(112, 20);
            this.ctlLabel5.StylePainter = this.StyleGuideBase;
            this.ctlLabel5.Text = "Margin Left";
            this.ctlLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctlLabel5.Visible = false;
            // 
            // ctlLeft
            // 
            this.ctlLeft.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.ctlLeft.DecimalPlaces = 0;
            this.ctlLeft.DefaultValue = "1";
            this.ctlLeft.Format = "N";
            this.ctlLeft.FormatType = mControl.Util.NumberFormats.StandadNumber;
            this.ctlLeft.Location = new System.Drawing.Point(152, 152);
            this.ctlLeft.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ctlLeft.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctlLeft.Name = "ctlLeft";
            this.ctlLeft.Size = new System.Drawing.Size(104, 20);
            this.ctlLeft.StylePainter = this.StyleGuideBase;
            this.ctlLeft.TabIndex = 32;
            this.ctlLeft.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.ctlLeft.Visible = false;
            // 
            // ctlLabel4
            // 
            this.ctlLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel4.FixSize = false;
            this.ctlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel4.Location = new System.Drawing.Point(32, 128);
            this.ctlLabel4.Name = "ctlLabel4";
            this.ctlLabel4.Size = new System.Drawing.Size(112, 20);
            this.ctlLabel4.StylePainter = this.StyleGuideBase;
            this.ctlLabel4.Text = "Margin Bottom";
            this.ctlLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctlLabel4.Visible = false;
            // 
            // ctlBottom
            // 
            this.ctlBottom.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.ctlBottom.DecimalPlaces = 0;
            this.ctlBottom.DefaultValue = "0.5";
            this.ctlBottom.Format = "N";
            this.ctlBottom.FormatType = mControl.Util.NumberFormats.StandadNumber;
            this.ctlBottom.Location = new System.Drawing.Point(152, 128);
            this.ctlBottom.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ctlBottom.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctlBottom.Name = "ctlBottom";
            this.ctlBottom.Size = new System.Drawing.Size(104, 20);
            this.ctlBottom.StylePainter = this.StyleGuideBase;
            this.ctlBottom.TabIndex = 30;
            this.ctlBottom.Value = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.ctlBottom.Visible = false;
            // 
            // ctlLabel3
            // 
            this.ctlLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel3.FixSize = false;
            this.ctlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel3.Location = new System.Drawing.Point(32, 104);
            this.ctlLabel3.Name = "ctlLabel3";
            this.ctlLabel3.Size = new System.Drawing.Size(112, 20);
            this.ctlLabel3.StylePainter = this.StyleGuideBase;
            this.ctlLabel3.Text = "Margin Top";
            this.ctlLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctlLabel3.Visible = false;
            // 
            // ctlTop
            // 
            this.ctlTop.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.ctlTop.DecimalPlaces = 0;
            this.ctlTop.DefaultValue = "0.50";
            this.ctlTop.Format = "N";
            this.ctlTop.FormatType = mControl.Util.NumberFormats.StandadNumber;
            this.ctlTop.Location = new System.Drawing.Point(152, 104);
            this.ctlTop.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ctlTop.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctlTop.Name = "ctlTop";
            this.ctlTop.Size = new System.Drawing.Size(104, 20);
            this.ctlTop.StylePainter = this.StyleGuideBase;
            this.ctlTop.TabIndex = 28;
            this.ctlTop.Value = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.ctlTop.Visible = false;
            // 
            // ctlLabel2
            // 
            this.ctlLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel2.FixSize = false;
            this.ctlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel2.Location = new System.Drawing.Point(32, 72);
            this.ctlLabel2.Name = "ctlLabel2";
            this.ctlLabel2.Size = new System.Drawing.Size(112, 20);
            this.ctlLabel2.StylePainter = this.StyleGuideBase;
            this.ctlLabel2.Text = "Max Column Width";
            this.ctlLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlMaxColWidth
            // 
            this.ctlMaxColWidth.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.ctlMaxColWidth.DecimalPlaces = 0;
            this.ctlMaxColWidth.DefaultValue = "80";
            this.ctlMaxColWidth.Format = "N";
            this.ctlMaxColWidth.FormatType = mControl.Util.NumberFormats.StandadNumber;
            this.ctlMaxColWidth.Location = new System.Drawing.Point(152, 72);
            this.ctlMaxColWidth.MaxValue = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.ctlMaxColWidth.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctlMaxColWidth.Name = "ctlMaxColWidth";
            this.ctlMaxColWidth.Size = new System.Drawing.Size(104, 20);
            this.ctlMaxColWidth.StylePainter = this.StyleGuideBase;
            this.ctlMaxColWidth.TabIndex = 1;
            this.ctlMaxColWidth.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // Alignment
            // 
            this.Alignment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.Alignment.Controls.Add(this.radioRight);
            this.Alignment.Controls.Add(this.radioCenter);
            this.Alignment.Controls.Add(this.radioLeft);
            this.Alignment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Alignment.ForeColor = System.Drawing.Color.Black;
            this.Alignment.GroupIndex = 0;
            this.Alignment.Location = new System.Drawing.Point(32, 16);
            this.Alignment.Name = "Alignment";
            this.Alignment.ReadOnly = false;
            this.Alignment.Size = new System.Drawing.Size(224, 48);
            this.Alignment.StylePainter = this.StyleGuideBase;
            this.Alignment.TabIndex = 0;
            this.Alignment.TabStop = false;
            this.Alignment.Text = "Alignment";
            // 
            // radioRight
            // 
            this.radioRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.radioRight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioRight.GroupIndex = 2;
            this.radioRight.Location = new System.Drawing.Point(152, 24);
            this.radioRight.Name = "radioRight";
            this.radioRight.Size = new System.Drawing.Size(48, 13);
            this.radioRight.StylePainter = this.StyleGuideBase;
            this.radioRight.TabIndex = 2;
            this.radioRight.TabStop = false;
            this.radioRight.Text = "Right";
            // 
            // radioCenter
            // 
            this.radioCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.radioCenter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioCenter.GroupIndex = 1;
            this.radioCenter.Location = new System.Drawing.Point(80, 24);
            this.radioCenter.Name = "radioCenter";
            this.radioCenter.Size = new System.Drawing.Size(56, 13);
            this.radioCenter.StylePainter = this.StyleGuideBase;
            this.radioCenter.TabIndex = 1;
            this.radioCenter.TabStop = false;
            this.radioCenter.Text = "Center";
            // 
            // radioLeft
            // 
            this.radioLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.radioLeft.Checked = true;
            this.radioLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioLeft.GroupIndex = 0;
            this.radioLeft.Location = new System.Drawing.Point(24, 24);
            this.radioLeft.Name = "radioLeft";
            this.radioLeft.Size = new System.Drawing.Size(48, 13);
            this.radioLeft.StylePainter = this.StyleGuideBase;
            this.radioLeft.TabIndex = 0;
            this.radioLeft.TabStop = false;
            this.radioLeft.Text = "Left";
            // 
            // ctlProviders
            // 
            this.ctlProviders.ButtonToolTip = "";
            this.ctlProviders.DefaultValue = null;
            this.ctlProviders.DropDownWidth = 248;
            this.ctlProviders.IntegralHeight = false;
            this.ctlProviders.Location = new System.Drawing.Point(88, 64);
            this.ctlProviders.Name = "ctlProviders";
            this.ctlProviders.Size = new System.Drawing.Size(248, 20);
            this.ctlProviders.StylePainter = this.StyleGuideBase;
            this.ctlProviders.TabIndex = 23;
            this.ctlProviders.SelectedIndexChanged += new System.EventHandler(this.ctlProviders_SelectedIndexChanged);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPreview.Location = new System.Drawing.Point(203, 360);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(64, 24);
            this.btnPreview.StylePainter = this.StyleGuideBase;
            this.btnPreview.TabIndex = 24;
            this.btnPreview.Text = "Preview";
            this.btnPreview.ToolTipText = "Preview";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExit.Location = new System.Drawing.Point(275, 360);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 24);
            this.btnExit.StylePainter = this.StyleGuideBase;
            this.btnExit.TabIndex = 25;
            this.btnExit.Text = "Exit";
            this.btnExit.ToolTipText = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ctlLabel1
            // 
            this.ctlLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlLabel1.FixSize = false;
            this.ctlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel1.Location = new System.Drawing.Point(16, 64);
            this.ctlLabel1.Name = "ctlLabel1";
            this.ctlLabel1.Size = new System.Drawing.Size(64, 20);
            this.ctlLabel1.StylePainter = this.StyleGuideBase;
            this.ctlLabel1.Text = "Provider";
            // 
            // btnMinimized
            // 
            this.btnMinimized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimized.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMinimized.Location = new System.Drawing.Point(11, 360);
            this.btnMinimized.Name = "btnMinimized";
            this.btnMinimized.Size = new System.Drawing.Size(64, 24);
            this.btnMinimized.StylePainter = this.StyleGuideBase;
            this.btnMinimized.TabIndex = 27;
            this.btnMinimized.Text = "Minimized";
            this.btnMinimized.ToolTipText = "Minimized";
            this.btnMinimized.Click += new System.EventHandler(this.btnMinimized_Click);
            // 
            // PrintDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(347, 392);
            this.Controls.Add(this.btnMinimized);
            this.Controls.Add(this.ctlLabel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.ctlProviders);
            this.Controls.Add(this.ctlTabControl);
            this.Controls.Add(this.ctlConnectionString);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PrintDB";
            this.Text = "PrintDB";
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.btnOpen, 0);
            this.Controls.SetChildIndex(this.ctlConnectionString, 0);
            this.Controls.SetChildIndex(this.ctlTabControl, 0);
            this.Controls.SetChildIndex(this.ctlProviders, 0);
            this.Controls.SetChildIndex(this.btnPreview, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.ctlLabel1, 0);
            this.Controls.SetChildIndex(this.btnMinimized, 0);
            this.ctlTabControl.ResumeLayout(false);
            this.ctlTabTables.ResumeLayout(false);
            this.ctlTabViews.ResumeLayout(false);
            this.ctlTabScript.ResumeLayout(false);
            this.ctlTabScript.PerformLayout();
            this.ctlTabSetting.ResumeLayout(false);
            this.Alignment.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private object[] CallerArgs=null;
		private Control owner;

		protected override bool Initialize(object[] args)
		{
			defaultLayout=true;
			if(args.Length>1)
			{
				owner=(Control)args[1];
				if(owner is IStyleCtl)
				{
					base.SetStyleLayout(((IStyleCtl)owner).CtlStyleLayout.Layout);
					base.SetChildrenStyle();
					defaultLayout=false;
				}
			}
			if(defaultLayout)
			base.SetDefaultLayout();
			return base.Initialize (args);
		}

		public override void Open(object[] args)
		{
			if(args!=null)
				CallerArgs=args;
			base.Open (args);

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		/// <returns>arg[0]=ProviderIndex,arg[1]=ParentControl</returns>
		public override DialogResult OpenDialog(object[] args)
		{
			if(args!=null)
				CallerArgs=args;
	
			return base.OpenDialog (args);
		}

		private bool dbCreated=false;

		private void InitCombo()
		{
			this.ctlProviders.Items.Add("Microsoft.Jet.OLEDB.4.0");
			this.ctlProviders.Items.Add("Microsoft.SqlServer");
			this.ctlProviders.SelectedIndex=0;
		}

		private void ctlProviders_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ResetLists();
			this.ctlConnectionString.Text="";
			//this.btnOpen.Enabled=this.ctlProviders.SelectedIndex==0;
		}

		private void btnOpen_Click_1(object sender, System.EventArgs e)
		{
			this.ctlConnectionString.Text="";
			if(this.ctlProviders.SelectedIndex==0)//Access
			{
				ResetLists();
				this.ctlConnectionString.Text="";
				SetLists();
			}
			else if(this.ctlProviders.SelectedIndex==1)//Sql
			{
				ConnetionDlg dlg=new ConnetionDlg();
				dlg.SetStyleLayout(this.CtlStyleLayout.Layout);
				dlg.OpenDialog(new object[]{"1"});
				//dlg.ShowDialog();
				if(dlg.DialogResult==DialogResult.OK)
				{
					this.ctlConnectionString.Text=dlg.ConnectionString;
				}
			}

		}

		private bool DBdreated()
		{
			if(!dbCreated)
			{
				ResetLists();
			}
			return dbCreated;
		}

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			if(!DBdreated())return;
			if(this.ctlTabControl.SelectedIndex==0)
			{
				if(this.listTables.SelectedIndex!=-1)
					PrintTable( this.listTables.SelectedValue.ToString());
			}
			else if(this.ctlTabControl.SelectedIndex==1)
			{
				if(this.listViews.SelectedIndex!=-1)
					PrintTable(this.listViews.SelectedValue.ToString());
			}
			else if(this.ctlTabControl.SelectedIndex==2)
			{
				if(this.txtScript.TextLength>0)
					PrintScript(this.txtScript.Text);
			}
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void listTables_DoubleClick(object sender, System.EventArgs e)
		{
			if(!DBdreated())return;
			if(this.listTables.SelectedIndex!=-1)
			{
				PrintTable( this.listTables.SelectedValue.ToString());
			}
		}

		private void listViews_DoubleClick(object sender, System.EventArgs e)
		{
			if(!DBdreated())return;
			if(this.listViews.SelectedIndex!=-1)
			{
				PrintTable(this.listViews.SelectedValue.ToString());
			}
		} 

		private void ResetLists()
		{
			dbCreated=false;
	
			this.listTables.ClearSelected();
			this.listViews.ClearSelected();

			this.listTables.DataSource=null;
			this.listViews.DataSource=null;
		}

		private void SetLists()
		{
			//PrintDbUtil du=new PrintDbUtil();
			DataSet ds=null;
			
			if(this.ctlProviders.SelectedIndex==0)//Access
			{
				string path=Data.OleDb.DBCmd.OpenAccessDB();
				if(!File.Exists(path))
					return;
				this.ctlConnectionString.Text=Data.OleDb.DBCmd.GetProvider(path,"mdb");
                ds=Data.OleDb.DBCmd.GetOleSchemaList(path);
				this.listTables.ValueMember="TABLE_NAME";
				this.listTables.DisplayMember="TABLE_NAME";
				this.listTables.DataSource= ds.Tables["TABLES"];
				this.listViews.ValueMember="TABLE_NAME";
				this.listViews.DisplayMember="TABLE_NAME";
				this.listViews.DataSource= ds.Tables["VIEWS"];
			}
			else if(this.ctlProviders.SelectedIndex==1)//Sql
			{
				ConnetionDlg dlg=new ConnetionDlg();
				dlg.ShowDialog();
				if(dlg.DialogResult==DialogResult.No)
					return;
				this.ctlConnectionString.Text=dlg.ConnectionString;
                string serverName=dlg.ServerName;
				dlg.Dispose();
				ds=Data.SqlClient.DBCmd.GetSqlSchemaList(this.ctlConnectionString.Text);
				this.listTables.ValueMember="TABLE_NAME";
				this.listTables.DisplayMember="TABLE_NAME";
				this.listTables.DataSource= ds.Tables["TABLES"];
				this.listViews.ValueMember="TABLE_NAME";
				this.listViews.DisplayMember="TABLE_NAME";
				this.listViews.DataSource= ds.Tables["VIEWS"];
			}
			dbCreated=true;
		}

		private void PrintTable(string table)
		{
			DataTable dt=null;
			if(this.ctlProviders.SelectedIndex==0)//Access
			{
				dt= Data.OleDb.DBCmd.ReadOleDB(this.ctlConnectionString.Text,table);
			}
			else if(this.ctlProviders.SelectedIndex==1)//Sql
			{
				dt= Data.SqlClient.DBCmd.ReadSqlDB(this.ctlConnectionString.Text,table);
			}
			if(dt!=null)
			{
				PrintDBTable(dt.DefaultView,table);
			}

		}

		private void PrintScript(string script)
		{
			DataTable dt=null;
			if(this.ctlProviders.SelectedIndex==0)//Access
			{
				dt= Data.OleDb.DBCmd.ExecuteOleDB(this.ctlConnectionString.Text,"",this.txtScript.Text);
			}
			else if(this.ctlProviders.SelectedIndex==1)//Sql
			{
				dt= Data.SqlClient.DBCmd.ExecuteSqlDB(this.ctlConnectionString.Text,"",this.txtScript.Text);
			}
			if(dt!=null)
			{
				PrintDBTable(dt.DefaultView,"");
			}

		}

		private void PrintDBTable(DataView dv,string table)
		{
			if(CallerArgs!=null )
			{
				if(CallerArgs.Length==2)
				{
					UpdateCaller(CallerArgs[0],dv);
					return;
				}
				else if((string)CallerArgs[0]=="GetView")
				{
					dataList=dv;
					this.DialogResult=DialogResult.OK;
					return;
				}
			}
			DataViewDocument dataReport=new DataViewDocument(dv, table);
			
			//CtlPrintDocument rpt=new CtlPrintDocument();
			dataReport.SetDefaultStyle();
			HorizontalAlignment align=this.Alignment.GroupIndex==0? HorizontalAlignment.Left:this.Alignment.GroupIndex==1?HorizontalAlignment.Center:HorizontalAlignment.Right;
			dataReport.CreateDocument(align,(float)this.ctlMaxColWidth.Value);
			dataReport.Show();
			//rpt.ReportSetting=dataReport as IReportDocument;
			//CtlPrintPreviewDialog.Preview(rpt);
			//CtlPrintPreviewDialog dlg=new CtlPrintPreviewDialog();
			//dlg.Document=rpt;
			//dlg.Owner=this;
			//dlg.Show();
	
		}

		private DataView dataList=null;

		private void UpdateCaller(object obj,DataView dv)
		{
			if (obj ==null)return;
			
			if (obj is CtlLookUpList)
			{
				((CtlLookUpList)obj).DataSource=dv;
				((CtlLookUpList)obj).Invalidate(true);
			}
		}

		public DataView DataList
		{
			get{return dataList;}
		}

		public override void SetStyleLayout(StyleLayout value)
		{
			base.SetStyleLayout (value);
			this.ctlTabControl.AutoChildrenStyle=true;
		}

		private int restorHeight;
		private Point restorButton;
		private void btnMinimized_Click(object sender, System.EventArgs e)
		{   
			if(this.btnMinimized.Text=="Minimized")
			{
				restorHeight=this.Height;
				restorButton=this.btnMinimized.Location;
				this.Height=this.CaptionHeight+14;
				this.btnMinimized.Location=new Point(this.Width-this.btnMinimized.Width-50,15);
				this.btnMinimized.Text="Maximized";
			}
			else
			{
				this.Height=restorHeight;
				this.btnMinimized.Location=restorButton;
				this.btnMinimized.Text="Minimized";
			}


		}

	}
}
