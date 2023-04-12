using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Resources;
using System.Drawing;
using System.Drawing.Printing;

//using MControl.WinForms;


namespace MControl.Printing.View.Design
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
    public class PageSetupDlg : System.Windows.Forms.Form//MControl.WinForms.FormBase
	{

		#region NetFram

        //private void //NetReflectedFram()
        //{
        //    this.cbCancel.//NetReflectedFram("ba7fa38f0b671cbc");
        //    this.cbOK.//NetReflectedFram("ba7fa38f0b671cbc");
        //    this.tcReportSetting.//NetReflectedFram("ba7fa38f0b671cbc");
        //}

		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PageSetupDlg()
		{

			//netFramwork.//NetReflectedFram();
			this.components = null;
			this._rect = new Rectangle();//var13
			this._rectF = new RectangleF();//var14
			this._printDocument = new PrintDocument();//var15
			int[] numArray1 = new int[4];
			numArray1[0] = 1;
			numArray1[3] = 0x10000;

			this.InitializeComponent();
			 //NetReflectedFram();
			this.SetGroupControls();
			this.PaperSourceItems();
			this.SetPaperSize();
			this.SetPrinterCollate();
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
            this.cbCancel = new Button();
            this.cbOK = new Button();
            this.tcReportSetting = new TabControl();
            this.tpPageSetup = new TabPage();
            this.gbPageSet = new System.Windows.Forms.GroupBox();
            this.lblGutter = new System.Windows.Forms.Label();
            this.lblRightMargin = new System.Windows.Forms.Label();
            this.lblLeftMargin = new System.Windows.Forms.Label();
            this.nudGutter = new System.Windows.Forms.NumericUpDown();
            this.nudRightMargin = new System.Windows.Forms.NumericUpDown();
            this.nudLeftMargin = new System.Windows.Forms.NumericUpDown();
            this.nudBottomMargin = new System.Windows.Forms.NumericUpDown();
            this.nudTopMargin = new System.Windows.Forms.NumericUpDown();
            this.lblBottomMargin = new System.Windows.Forms.Label();
            this.lblTopMargin = new System.Windows.Forms.Label();
            this.picPageSetup = new System.Windows.Forms.PictureBox();
            this.tpPrinterSetup = new TabPage();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblDuplex = new System.Windows.Forms.Label();
            this.lblCollate = new System.Windows.Forms.Label();
            this.cbxPaperSource = new System.Windows.Forms.ComboBox();
            this.cbxDuplex = new System.Windows.Forms.ComboBox();
            this.cbxCollate = new System.Windows.Forms.ComboBox();
            this.gbOrientation = new System.Windows.Forms.GroupBox();
            this.rbLandscape = new System.Windows.Forms.RadioButton();
            this.rbPortrait = new System.Windows.Forms.RadioButton();
            this.rbDefault = new System.Windows.Forms.RadioButton();
            this.picPrinter = new System.Windows.Forms.PictureBox();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.cbxPaperSize = new System.Windows.Forms.ComboBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.lblPaperSource = new System.Windows.Forms.Label();
            this.tcReportSetting.SuspendLayout();
            this.tpPageSetup.SuspendLayout();
            this.gbPageSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGutter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRightMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottomMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPageSetup)).BeginInit();
            this.tpPrinterSetup.SuspendLayout();
            this.gbOrientation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPrinter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = MControl.WinForms.Styles.Desktop;
            // 
            // cbCancel
            // 
            this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cbCancel.Location = new System.Drawing.Point(367, 317);
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.Size = new System.Drawing.Size(72, 24);
            this.cbCancel.StylePainter = this.StyleGuideBase;
            this.cbCancel.TabIndex = 16;
            this.cbCancel.Text = "Cancel";
            this.cbCancel.ToolTipText = "Cancel";
            this.cbCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // cbOK
            // 
            this.cbOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cbOK.Location = new System.Drawing.Point(279, 317);
            this.cbOK.Name = "cbOK";
            this.cbOK.Size = new System.Drawing.Size(72, 24);
            this.cbOK.StylePainter = this.StyleGuideBase;
            this.cbOK.TabIndex = 15;
            this.cbOK.Text = "OK";
            this.cbOK.ToolTipText = "OK";
            this.cbOK.Click += new System.EventHandler(this.OK_Click);
            // 
            // tcReportSetting
            // 
            this.tcReportSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcReportSetting.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tcReportSetting.Controls.Add(this.tpPageSetup);
            this.tcReportSetting.Controls.Add(this.tpPrinterSetup);
            this.tcReportSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tcReportSetting.ItemSize = new System.Drawing.Size(120, 24);
            this.tcReportSetting.Location = new System.Drawing.Point(0, 12);
            this.tcReportSetting.Name = "tcReportSetting";
            this.tcReportSetting.Size = new System.Drawing.Size(442, 299);
            this.tcReportSetting.StylePainter = this.StyleGuideBase;
            this.tcReportSetting.TabIndex = 14;
            this.tcReportSetting.TabPages.AddRange(new TabPage[] {
            this.tpPageSetup,
            this.tpPrinterSetup});
            this.tcReportSetting.TabStop = false;
            // 
            // tpPageSetup
            // 
            this.tpPageSetup.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tpPageSetup.Controls.Add(this.gbPageSet);
            this.tpPageSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tpPageSetup.ImageIndex = 0;
            this.tpPageSetup.Location = new System.Drawing.Point(4, 31);
            this.tpPageSetup.Name = "tpPageSetup";
            this.tpPageSetup.Size = new System.Drawing.Size(433, 263);
            this.tpPageSetup.StylePainter = this.StyleGuideBase;
            this.tpPageSetup.Text = "Page Setup";
            // 
            // gbPageSet
            // 
            this.gbPageSet.Controls.Add(this.lblGutter);
            this.gbPageSet.Controls.Add(this.lblRightMargin);
            this.gbPageSet.Controls.Add(this.lblLeftMargin);
            this.gbPageSet.Controls.Add(this.nudGutter);
            this.gbPageSet.Controls.Add(this.nudRightMargin);
            this.gbPageSet.Controls.Add(this.nudLeftMargin);
            this.gbPageSet.Controls.Add(this.nudBottomMargin);
            this.gbPageSet.Controls.Add(this.nudTopMargin);
            this.gbPageSet.Controls.Add(this.lblBottomMargin);
            this.gbPageSet.Controls.Add(this.lblTopMargin);
            this.gbPageSet.Controls.Add(this.picPageSetup);
            this.gbPageSet.Location = new System.Drawing.Point(8, 8);
            this.gbPageSet.Name = "gbPageSet";
            this.gbPageSet.Size = new System.Drawing.Size(288, 144);
            this.gbPageSet.TabIndex = 10;
            this.gbPageSet.TabStop = false;
            // 
            // lblGutter
            // 
            this.lblGutter.Location = new System.Drawing.Point(88, 112);
            this.lblGutter.Name = "lblGutter";
            this.lblGutter.Size = new System.Drawing.Size(88, 16);
            this.lblGutter.TabIndex = 18;
            this.lblGutter.Text = "Gutter :";
            // 
            // lblRightMargin
            // 
            this.lblRightMargin.Location = new System.Drawing.Point(88, 88);
            this.lblRightMargin.Name = "lblRightMargin";
            this.lblRightMargin.Size = new System.Drawing.Size(88, 16);
            this.lblRightMargin.TabIndex = 17;
            this.lblRightMargin.Text = "Right Margin :";
            // 
            // lblLeftMargin
            // 
            this.lblLeftMargin.Location = new System.Drawing.Point(88, 64);
            this.lblLeftMargin.Name = "lblLeftMargin";
            this.lblLeftMargin.Size = new System.Drawing.Size(80, 16);
            this.lblLeftMargin.TabIndex = 16;
            this.lblLeftMargin.Text = "Left Margin :";
            // 
            // nudGutter
            // 
            this.nudGutter.DecimalPlaces = 1;
            this.nudGutter.Location = new System.Drawing.Point(192, 112);
            this.nudGutter.Name = "nudGutter";
            this.nudGutter.Size = new System.Drawing.Size(80, 20);
            this.nudGutter.TabIndex = 15;
            this.nudGutter.ValueChanged += new System.EventHandler(this.Margin_ValueChanged);
            // 
            // nudRightMargin
            // 
            this.nudRightMargin.DecimalPlaces = 1;
            this.nudRightMargin.Location = new System.Drawing.Point(192, 88);
            this.nudRightMargin.Name = "nudRightMargin";
            this.nudRightMargin.Size = new System.Drawing.Size(80, 20);
            this.nudRightMargin.TabIndex = 14;
            this.nudRightMargin.ValueChanged += new System.EventHandler(this.Margin_ValueChanged);
            // 
            // nudLeftMargin
            // 
            this.nudLeftMargin.DecimalPlaces = 1;
            this.nudLeftMargin.Location = new System.Drawing.Point(192, 64);
            this.nudLeftMargin.Name = "nudLeftMargin";
            this.nudLeftMargin.Size = new System.Drawing.Size(80, 20);
            this.nudLeftMargin.TabIndex = 13;
            this.nudLeftMargin.ValueChanged += new System.EventHandler(this.Margin_ValueChanged);
            // 
            // nudBottomMargin
            // 
            this.nudBottomMargin.DecimalPlaces = 1;
            this.nudBottomMargin.Location = new System.Drawing.Point(192, 40);
            this.nudBottomMargin.Name = "nudBottomMargin";
            this.nudBottomMargin.Size = new System.Drawing.Size(80, 20);
            this.nudBottomMargin.TabIndex = 12;
            this.nudBottomMargin.ValueChanged += new System.EventHandler(this.Margin_ValueChanged);
            // 
            // nudTopMargin
            // 
            this.nudTopMargin.DecimalPlaces = 1;
            this.nudTopMargin.Location = new System.Drawing.Point(192, 16);
            this.nudTopMargin.Name = "nudTopMargin";
            this.nudTopMargin.Size = new System.Drawing.Size(80, 20);
            this.nudTopMargin.TabIndex = 11;
            this.nudTopMargin.ValueChanged += new System.EventHandler(this.Margin_ValueChanged);
            // 
            // lblBottomMargin
            // 
            this.lblBottomMargin.Location = new System.Drawing.Point(88, 40);
            this.lblBottomMargin.Name = "lblBottomMargin";
            this.lblBottomMargin.Size = new System.Drawing.Size(88, 16);
            this.lblBottomMargin.TabIndex = 10;
            this.lblBottomMargin.Text = "Bottom Margin :";
            // 
            // lblTopMargin
            // 
            this.lblTopMargin.Location = new System.Drawing.Point(88, 16);
            this.lblTopMargin.Name = "lblTopMargin";
            this.lblTopMargin.Size = new System.Drawing.Size(72, 16);
            this.lblTopMargin.TabIndex = 9;
            this.lblTopMargin.Text = "Top Margin :";
            // 
            // picPageSetup
            // 
            this.picPageSetup.Location = new System.Drawing.Point(16, 48);
            this.picPageSetup.Name = "picPageSetup";
            this.picPageSetup.Size = new System.Drawing.Size(48, 48);
            this.picPageSetup.TabIndex = 1;
            this.picPageSetup.TabStop = false;
            // 
            // tpPrinterSetup
            // 
            this.tpPrinterSetup.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tpPrinterSetup.Controls.Add(this.lblUnit);
            this.tpPrinterSetup.Controls.Add(this.lblDuplex);
            this.tpPrinterSetup.Controls.Add(this.lblCollate);
            this.tpPrinterSetup.Controls.Add(this.cbxPaperSource);
            this.tpPrinterSetup.Controls.Add(this.cbxDuplex);
            this.tpPrinterSetup.Controls.Add(this.cbxCollate);
            this.tpPrinterSetup.Controls.Add(this.gbOrientation);
            this.tpPrinterSetup.Controls.Add(this.nudHeight);
            this.tpPrinterSetup.Controls.Add(this.nudWidth);
            this.tpPrinterSetup.Controls.Add(this.lblHeight);
            this.tpPrinterSetup.Controls.Add(this.lblWidth);
            this.tpPrinterSetup.Controls.Add(this.cbxPaperSize);
            this.tpPrinterSetup.Controls.Add(this.lblPaperSize);
            this.tpPrinterSetup.Controls.Add(this.lblPaperSource);
            this.tpPrinterSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tpPrinterSetup.ImageIndex = 1;
            this.tpPrinterSetup.Location = new System.Drawing.Point(4, 31);
            this.tpPrinterSetup.Name = "tpPrinterSetup";
            this.tpPrinterSetup.Size = new System.Drawing.Size(433, 263);
            this.tpPrinterSetup.StylePainter = this.StyleGuideBase;
            this.tpPrinterSetup.Text = "Printer Setting";
            // 
            // lblUnit
            // 
            this.lblUnit.BackColor = System.Drawing.Color.Transparent;
            this.lblUnit.Location = new System.Drawing.Point(184, 56);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(40, 24);
            this.lblUnit.TabIndex = 27;
            this.lblUnit.Text = "inch";
            // 
            // lblDuplex
            // 
            this.lblDuplex.BackColor = System.Drawing.Color.Transparent;
            this.lblDuplex.Location = new System.Drawing.Point(16, 208);
            this.lblDuplex.Name = "lblDuplex";
            this.lblDuplex.Size = new System.Drawing.Size(72, 16);
            this.lblDuplex.TabIndex = 25;
            this.lblDuplex.Text = "Duplex :";
            // 
            // lblCollate
            // 
            this.lblCollate.BackColor = System.Drawing.Color.Transparent;
            this.lblCollate.Location = new System.Drawing.Point(16, 184);
            this.lblCollate.Name = "lblCollate";
            this.lblCollate.Size = new System.Drawing.Size(64, 16);
            this.lblCollate.TabIndex = 24;
            this.lblCollate.Text = "Collate :";
            // 
            // cbxPaperSource
            // 
            this.cbxPaperSource.Location = new System.Drawing.Point(96, 232);
            this.cbxPaperSource.Name = "cbxPaperSource";
            this.cbxPaperSource.Size = new System.Drawing.Size(168, 21);
            this.cbxPaperSource.TabIndex = 23;
            // 
            // cbxDuplex
            // 
            this.cbxDuplex.Location = new System.Drawing.Point(96, 208);
            this.cbxDuplex.Name = "cbxDuplex";
            this.cbxDuplex.Size = new System.Drawing.Size(168, 21);
            this.cbxDuplex.TabIndex = 22;
            // 
            // cbxCollate
            // 
            this.cbxCollate.Location = new System.Drawing.Point(96, 184);
            this.cbxCollate.Name = "cbxCollate";
            this.cbxCollate.Size = new System.Drawing.Size(168, 21);
            this.cbxCollate.TabIndex = 21;
            // 
            // gbOrientation
            // 
            this.gbOrientation.Controls.Add(this.rbLandscape);
            this.gbOrientation.Controls.Add(this.rbPortrait);
            this.gbOrientation.Controls.Add(this.rbDefault);
            this.gbOrientation.Controls.Add(this.picPrinter);
            this.gbOrientation.Location = new System.Drawing.Point(8, 88);
            this.gbOrientation.Name = "gbOrientation";
            this.gbOrientation.Size = new System.Drawing.Size(256, 88);
            this.gbOrientation.TabIndex = 20;
            this.gbOrientation.TabStop = false;
            this.gbOrientation.Text = "Orientation";
            // 
            // rbLandscape
            // 
            this.rbLandscape.Location = new System.Drawing.Point(112, 64);
            this.rbLandscape.Name = "rbLandscape";
            this.rbLandscape.Size = new System.Drawing.Size(120, 16);
            this.rbLandscape.TabIndex = 5;
            this.rbLandscape.Text = "Landscape";
            this.rbLandscape.CheckedChanged += new System.EventHandler(this._CheckedChanged);
            // 
            // rbPortrait
            // 
            this.rbPortrait.Location = new System.Drawing.Point(112, 40);
            this.rbPortrait.Name = "rbPortrait";
            this.rbPortrait.Size = new System.Drawing.Size(120, 16);
            this.rbPortrait.TabIndex = 4;
            this.rbPortrait.Text = "Portrait";
            this.rbPortrait.CheckedChanged += new System.EventHandler(this._CheckedChanged);
            // 
            // rbDefault
            // 
            this.rbDefault.Checked = true;
            this.rbDefault.Location = new System.Drawing.Point(112, 16);
            this.rbDefault.Name = "rbDefault";
            this.rbDefault.Size = new System.Drawing.Size(120, 16);
            this.rbDefault.TabIndex = 3;
            this.rbDefault.TabStop = true;
            this.rbDefault.Text = "Default";
            this.rbDefault.CheckedChanged += new System.EventHandler(this._CheckedChanged);
            // 
            // picPrinter
            // 
            this.picPrinter.Location = new System.Drawing.Point(16, 24);
            this.picPrinter.Name = "picPrinter";
            this.picPrinter.Size = new System.Drawing.Size(48, 48);
            this.picPrinter.TabIndex = 2;
            this.picPrinter.TabStop = false;
            // 
            // nudHeight
            // 
            this.nudHeight.DecimalPlaces = 1;
            this.nudHeight.Enabled = false;
            this.nudHeight.Location = new System.Drawing.Point(96, 64);
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(80, 20);
            this.nudHeight.TabIndex = 19;
            this.nudHeight.ValueChanged += new System.EventHandler(this._ValueChanged);
            this.nudHeight.Validating += new System.ComponentModel.CancelEventHandler(this._Validating);
            // 
            // nudWidth
            // 
            this.nudWidth.DecimalPlaces = 1;
            this.nudWidth.Enabled = false;
            this.nudWidth.Location = new System.Drawing.Point(96, 40);
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(80, 20);
            this.nudWidth.TabIndex = 18;
            this.nudWidth.ValueChanged += new System.EventHandler(this._ValueChanged);
            this.nudWidth.Validating += new System.ComponentModel.CancelEventHandler(this._Validating);
            // 
            // lblHeight
            // 
            this.lblHeight.BackColor = System.Drawing.Color.Transparent;
            this.lblHeight.Location = new System.Drawing.Point(16, 64);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(64, 16);
            this.lblHeight.TabIndex = 17;
            this.lblHeight.Text = "Height :";
            // 
            // lblWidth
            // 
            this.lblWidth.BackColor = System.Drawing.Color.Transparent;
            this.lblWidth.Location = new System.Drawing.Point(16, 40);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(72, 16);
            this.lblWidth.TabIndex = 16;
            this.lblWidth.Text = "Width :";
            // 
            // cbxPaperSize
            // 
            this.cbxPaperSize.Location = new System.Drawing.Point(96, 16);
            this.cbxPaperSize.Name = "cbxPaperSize";
            this.cbxPaperSize.Size = new System.Drawing.Size(168, 21);
            this.cbxPaperSize.TabIndex = 15;
            this.cbxPaperSize.SelectedIndexChanged += new System.EventHandler(this._SelectedIndexChanged);
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.BackColor = System.Drawing.Color.Transparent;
            this.lblPaperSize.Location = new System.Drawing.Point(16, 16);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(72, 16);
            this.lblPaperSize.TabIndex = 14;
            this.lblPaperSize.Text = "Paper Size :";
            // 
            // lblPaperSource
            // 
            this.lblPaperSource.BackColor = System.Drawing.Color.Transparent;
            this.lblPaperSource.Location = new System.Drawing.Point(16, 232);
            this.lblPaperSource.Name = "lblPaperSource";
            this.lblPaperSource.Size = new System.Drawing.Size(80, 16);
            this.lblPaperSource.TabIndex = 26;
            this.lblPaperSource.Text = "Paper Source :";
            // 
            // PageSetupDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(442, 350);
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbOK);
            this.Controls.Add(this.tcReportSetting);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PageSetupDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Page Setup";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.tcReportSetting, 0);
            this.Controls.SetChildIndex(this.cbOK, 0);
            this.Controls.SetChildIndex(this.cbCancel, 0);
            this.tcReportSetting.ResumeLayout(false);
            this.tpPageSetup.ResumeLayout(false);
            this.gbPageSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudGutter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRightMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottomMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPageSetup)).EndInit();
            this.tpPrinterSetup.ResumeLayout(false);
            this.gbOrientation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPrinter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion


		#region Members

		// Fields
		private float var11;
		private PageSettings _pageSettings;//_pageSettings;
		private Rectangle _rect;
		private RectangleF _rectF;
		private System.Drawing.Printing.PrintDocument _printDocument;
		private GroupControl _groupControl1;// var18;
		private GroupControl _groupControl2;//var19;
		private string var31;
		internal bool mtd4;
		internal Button cbCancel;
		internal Button cbOK;
		internal ComboBox cbxCollate;
		internal ComboBox cbxDuplex;
		internal ComboBox cbxPaperSize;
		internal ComboBox cbxPaperSource;
		//private Container components;
		internal GroupBox gbOrientation;
		internal GroupBox gbPageSet;
		internal Label lblBottomMargin;
		internal Label lblCollate;
		internal Label lblDuplex;
		internal Label lblGutter;
		internal Label lblHeight;
		internal Label lblLeftMargin;
		internal Label lblPaperSize;
		internal Label lblPaperSource;
		internal Label lblRightMargin;
		internal Label lblTopMargin;
		internal Label lblWidth;
		internal NumericUpDown nudBottomMargin;
		internal NumericUpDown nudGutter;
		internal NumericUpDown nudHeight;
		internal NumericUpDown nudLeftMargin;
		internal NumericUpDown nudRightMargin;
		internal NumericUpDown nudTopMargin;
		internal NumericUpDown nudWidth;
		internal PictureBox picPageSetup;
		internal PictureBox picPrinter;
		internal RadioButton rbDefault;
		internal RadioButton rbLandscape;
		internal RadioButton rbPortrait;
		internal TabControl tcReportSetting;
		internal TabPage tpPageSetup;
		private System.Windows.Forms.Label lblUnit;
		internal TabPage tpPrinterSetup;

		#endregion


		private void SetGroupControls()//var0()
		{
			this._groupControl1 = new PageSetupDlg.GroupControl();
			this._groupControl1.Location = new Point(0x138, 8);
			this._groupControl1.Name = "gbPreview";
			this._groupControl1.Size = new Size(120, 0x90);
			this._groupControl1.TabIndex = 1;
			this._groupControl1.TabStop = false;
			this._groupControl1.Text = "Preview";
			this._groupControl1.Paint += new PaintEventHandler(this._Paint);
			this._groupControl2 = new PageSetupDlg.GroupControl();
			this._groupControl2.Location = new Point(0x138, 8);
			this._groupControl2.Name = "gbPreview2";
			this._groupControl2.Size = new Size(120, 0x90);
			this._groupControl2.TabIndex = 2;
			this._groupControl2.TabStop = false;
			this._groupControl2.Text = "Preview";
			this._groupControl2.Paint += new PaintEventHandler(this._Paint);//var21
			this.tpPrinterSetup.Controls.Add(this._groupControl2);
			this.tpPageSetup.Controls.Add(this._groupControl1);
		}

		private void PaperSourceItems()//var1
		{
			this.cbxPaperSource.Items.Add("Printer Default");
			if (this.PrintersInstalled())
			{
				this.cbxPaperSource.DisplayMember = "SourceName";
				foreach (PaperSource source1 in this._printDocument.PrinterSettings.PaperSources)
				{
					this.cbxPaperSource.Items.Add(source1);
				}
			}
		}

 
		private void _SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cbxPaperSize.SelectedItem.ToString() == "Custom")
			{
				this.nudWidth.Enabled = true;
				this.nudHeight.Enabled = true;
			}
			else
			{
				string unit="";
                SizeF size = ReportUtil.GetPaperSize(this.cbxPaperSize.SelectedItem.ToString(), ref unit);
				if(size !=SizeF.Empty)
				{
					this.lblUnit.Text=unit; 
					this.nudWidth.Value =(decimal)size.Width;
					this.nudHeight.Value =(decimal)size.Height;
				}
				else
				{
					this.lblUnit.Text=""; 
					this.nudWidth.Value =(decimal)0;
					this.nudHeight.Value =(decimal)0;
				}
				//this.nudWidth.Value =(decimal)this.PageSettings.PaperWidth;// new decimal(0x55, 0, 0, false, 1);
				//this.nudHeight.Value =(decimal)this.PageSettings.PaperHeight;// new decimal(110, 0, 0, false, 1);
				this.nudWidth.Enabled = false;
				this.nudHeight.Enabled = false;
			}
			this.CalcSizes();
		}

//		public static SizeF GetPaperSize(string paper,ref string unit)
//		{
//			string s=MControl.Printing.View.ResexLibrary.GetPaperSize(paper);
//			if(s==null || s=="")
//			{
//              return SizeF.Empty;
//			}
//			
//			string[]res=s.Split(';');
//			unit=res[2];
//
//            return new SizeF(float.Parse(res[0]),float.Parse(res[1]));
//		}

		private void var16()
		{
			this.nudTopMargin.Value = (decimal) this.PageSettings.Margins.MarginTop;
			this.nudBottomMargin.Value = (decimal) this.PageSettings.Margins.MarginBottom;
			this.nudLeftMargin.Value = (decimal) this.PageSettings.Margins.MarginLeft;
			this.nudRightMargin.Value = (decimal) this.PageSettings.Margins.MarginRight;
			this.nudGutter.Value = (decimal) this.PageSettings.Gutter;
			this.cbxCollate.SelectedItem = (decimal) this.PageSettings.Collate;
			this.cbxDuplex.SelectedItem = (decimal) this.PageSettings.Duplex;
			if (this.PageSettings.DefaultPaperSource)
			{
				this.cbxPaperSource.SelectedIndex = 0;
			}
			else
			{
				this.cbxPaperSource.SelectedItem = this.var22(this.PageSettings);
				if (this.cbxPaperSource.SelectedItem == null)
				{
					this.cbxPaperSource.SelectedIndex = 0;
				}
			}
			if (this.PageSettings.DefaultPaperSize)
			{
				this.cbxPaperSize.SelectedIndex = 0;
			}
			else if (this.PageSettings.PaperKind == PaperKind.Custom)
			{
				this.cbxPaperSize.SelectedItem = PaperKind.Custom;
				this.nudWidth.Enabled = true;
				this.nudHeight.Enabled = true;
				this.nudWidth.Value = (decimal) (this.PageSettings.PaperWidth / 100f);
				this.nudHeight.Value = (decimal) (this.PageSettings.PaperHeight / 100f);
			}
			else
			{
				this.cbxPaperSize.SelectedIndex = this.GetSelectedIndex(this.PageSettings);
				if (this.cbxPaperSize.SelectedIndex == -1)
				{
					this.cbxPaperSize.SelectedItem = 0;
				}
			}
			if (this._pageSettings.Orientation == PageOrientation.Default)
			{
				this.rbDefault.Checked = true;
			}
			else if (this._pageSettings.Orientation == PageOrientation.Portrait)
			{
				this.rbPortrait.Checked = true;
			}
			else if (this._pageSettings.Orientation == PageOrientation.Landscape)
			{
				this.rbLandscape.Checked = true;
			}
		}

		private void SetPaperSize()//var2()
		{
			this.cbxPaperSize.Items.Add(PaperKind.A4);
			PaperInfos infos1 = PageSettings.PaperInfos;
			for (int num1 = 0; num1 < infos1.Size; num1++)
			{
				PaperInfo info1 = infos1[num1];
				if (info1.Kind != PaperKind.A4)
				{
					this.cbxPaperSize.Items.Add(info1.Kind);
				}
			}
			this.cbxPaperSize.Items.Add(PaperKind.Custom);
		}

		private bool PrintersInstalled()//var20()
		{
			bool flag1 = false;
			try
			{
				if (PrinterSettings.InstalledPrinters.Count > 0)
				{
					flag1 = true;
				}
			}
			catch
			{
			}
			return flag1;
		}

 
		private void _Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics1 = e.Graphics;
			graphics1.FillRectangle(SystemBrushes.Window, this._rectF);
			this.SetRegionLines(graphics1);
			this.SetRegion(graphics1);
			graphics1.DrawRectangle(SystemPens.ControlText, this._rectF.X, this._rectF.Y, this._rectF.Width, this._rectF.Height);
		}

		private System.Drawing.Printing.PaperSource var22(PageSettings var24)
		{
			if (!var24.DefaultPaperSource && this.PrintersInstalled())
			{
				for (int num1 = 1; num1 < this.cbxPaperSource.Items.Count; num1++)
				{
					PaperSource source1 = (PaperSource) this.cbxPaperSource.Items[num1];
					if (source1.Kind.Equals(var24.PaperSource))
					{
						return source1;
					}
				}
			}
			return null;
		}

		private int GetSelectedIndex(PageSettings p)//var23
		{
			for (int num1 = 0; num1 < this.cbxPaperSize.Items.Count; num1++)
			{
				if (this.cbxPaperSize.Items[num1].Equals(p.PaperKind))
				{
					return num1;
				}
			}
			return -1;
		}

		private void SetRegionLines(Graphics g)//var25
		{
			Region region1 = g.Clip;
			g.Clip = new Region(this._rectF);
			this.DrawPreviewLine(this._rect.Location, g);
			this.DrawPreviewLine(new Point(this._rect.Right, this._rect.Y), g);
			this.DrawPreviewLine(new Point(this._rect.X, this._rect.Bottom), g);
			this.DrawPreviewLine(new Point(this._rect.Right, this._rect.Bottom), g);
			g.Clip = region1;
		}

		private void SetRegion(Graphics g)//var26
		{
			Rectangle rectangle1 = this._rect;
			rectangle1.Inflate(-1, -1);
			Region region1 = g.Clip;
			g.Clip = new Region(rectangle1);
			for (int num1 = 0; num1 <= rectangle1.Height; num1 += 20)
			{
				Point[] pointArray2 = new Point[] { new Point(rectangle1.X, rectangle1.Y + num1), new Point(rectangle1.Right, rectangle1.Y + num1) };
				Point[] pointArray1 = pointArray2;
				g.DrawLines(SystemPens.WindowText, pointArray1);
				pointArray1[0].Offset(0, 4);
				pointArray1[1].Offset(0, 4);
				g.DrawLines(SystemPens.WindowText, pointArray1);
				pointArray1[0].Offset(0, 4);
				pointArray1[1].Offset(0, 4);
				g.DrawLines(SystemPens.WindowText, pointArray1);
				pointArray1[0].Offset(0, 4);
				pointArray1[1].Offset(0, 4);
				g.DrawLines(SystemPens.WindowText, pointArray1);
			}
			g.Clip = region1;
		}

 
		private void DrawPreviewLine(Point pt, Graphics g)//var27
		{
			PointF[] tfArray3 = new PointF[] { (PointF) new Point(pt.X - 4, pt.Y), (PointF) new Point(pt.X + 4, pt.Y) };
			PointF[] tfArray1 = tfArray3;
			tfArray3 = new PointF[] { (PointF) new Point(pt.X, pt.Y - 4), (PointF) new Point(pt.X, pt.Y + 4) };
			PointF[] tfArray2 = tfArray3;
			g.DrawLines(SystemPens.ControlDark, tfArray1);
			g.DrawLines(SystemPens.ControlDark, tfArray2);
		}

		private void CalcSizes()//var28()
		{
			float single1;
			float single2;
			Rectangle rectangle1 = new Rectangle(15, 20, this._groupControl1.Width - 30, this._groupControl1.Height - 0x23);
			if (this.cbxPaperSize.SelectedItem.ToString() == PaperKind.Custom.ToString())
			{
				single1 = (float) this.nudWidth.Value;
				single2 = (float) this.nudHeight.Value;
			}
			else
			{
				SizeF ef1 = PageSettings.PaperInfos[(PaperKind) this.cbxPaperSize.SelectedItem].SizeInInch;
				single2 = ef1.Height * 100f;
				single1 = ef1.Width * 100f;
			}
			if (this.rbLandscape.Checked)
			{
				float single3 = single2;
				single2 = single1;
				single1 = single3;
			}
			if (single2 >= single1)
			{
				this._rectF.Height = rectangle1.Height;
				this._rectF.Width = (this._rectF.Height * single1) / single2;
				this._rectF.Y = rectangle1.Y;
				this._rectF.X = rectangle1.Left + ((rectangle1.Width - this._rectF.Width) / 2f);
			}
			else
			{
				this._rectF.Width = rectangle1.Width;
				this._rectF.Height = (this._rectF.Width * single2) / single1;
				this._rectF.X = rectangle1.Left;
				this._rectF.Y = rectangle1.Top + ((rectangle1.Height - this._rectF.Height) / 2f);
			}
			this.var11 = this._rectF.Width / single1;
			this.SetRects();
			Rectangle rectangle2 = rectangle1;
			rectangle2.Inflate(1, 1);
			this._groupControl2.Invalidate(rectangle2);
		}

		private void SetRects()//var29()
		{
			this._rect.X = (int) (this._rectF.X + (((float) this.nudLeftMargin.Value) * this.var11));
			this._rect.Y = (int) (this._rectF.Y + (((float) this.nudTopMargin.Value) * this.var11));
			this._rect.Width = (int) (this._rectF.Width - (((float) (this.nudLeftMargin.Value + this.nudRightMargin.Value)) * this.var11));
			this._rect.Height = (int) (this._rectF.Height - (((float) (this.nudTopMargin.Value + this.nudBottomMargin.Value)) * this.var11));
		}

		private void SetPrinterCollate()//var3
		{
			this.cbxCollate.Items.Add(PrinterCollate.Default);
			this.cbxCollate.Items.Add(PrinterCollate.Collate);
			this.cbxCollate.Items.Add(PrinterCollate.DontCollate);
			this.cbxDuplex.Items.Add(PrinterDuplex.Default);
			this.cbxDuplex.Items.Add(PrinterDuplex.Horizontal);
			this.cbxDuplex.Items.Add(PrinterDuplex.Simplex);
			this.cbxDuplex.Items.Add(PrinterDuplex.Vertical);
		}

		private void SetMargins()//var30
		{
			this._pageSettings.Margins.MarginTop = (float) this.nudTopMargin.Value;
			this._pageSettings.Margins.MarginBottom = (float) this.nudBottomMargin.Value;
			this._pageSettings.Margins.MarginLeft = (float) this.nudLeftMargin.Value;
			this._pageSettings.Margins.MarginRight = (float) this.nudRightMargin.Value;
			this._pageSettings.Gutter = (float) this.nudGutter.Value;
			this._pageSettings.DefaultPaperSize = false;
			if (this.cbxPaperSize.SelectedItem.ToString() == PaperKind.Custom.ToString())
			{
				this._pageSettings.PaperKind = PaperKind.Custom;
				this._pageSettings.PaperWidth = ((float) this.nudWidth.Value) * 100f;
				this._pageSettings.PaperHeight = ((float) this.nudHeight.Value) * 100f;
			}
			else
			{
				this._pageSettings.PaperKind = (PaperKind) this.cbxPaperSize.SelectedItem;
			}
			if (this.cbxCollate.SelectedIndex > 0)
			{
				this._pageSettings.Collate = (PrinterCollate) this.cbxCollate.SelectedItem;
			}
			if (this.cbxDuplex.SelectedIndex > 0)
			{
				this._pageSettings.Duplex = (PrinterDuplex) this.cbxDuplex.SelectedItem;
			}
			if (this.cbxPaperSource.SelectedIndex > 0)
			{
				this._pageSettings.PaperSource = ((PaperSource) this.cbxPaperSource.SelectedItem).Kind;
				this._pageSettings.DefaultPaperSource = false;
			}
			if (this.rbPortrait.Checked)
			{
				this._pageSettings.Orientation = PageOrientation.Portrait;
			}
			else if (this.rbLandscape.Checked)
			{
				this._pageSettings.Orientation = PageOrientation.Landscape;
			}
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			this.mtd4 = false;
			base.Close();
		}

		private void OK_Click(object sender, EventArgs e)
		{
			this.SetMargins();
			this.mtd4 = true;
			base.Close();
		}

 
		private void Margin_ValueChanged(object sender, EventArgs e)
		{
			this.SetRects();
			this._groupControl1.Invalidate(new Region(this._rectF));
		}

		private void _CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton) sender).Name != this.var31)
			{
				this.CalcSizes();
				this.var31 = ((RadioButton) sender).Name;
			}
		}

 
		private void _Validating(object sender, CancelEventArgs e)
		{
			if (((NumericUpDown) sender).Value > new decimal(100))
			{
				((NumericUpDown) sender).Value = new decimal(100);
			}
			else if (((NumericUpDown) sender).Value < new decimal(1, 0, 0, false, 1))
			{
				((NumericUpDown) sender).Value = new decimal(1, 0, 0, false, 1);
			}
		}

		private void _ValueChanged(object sender, EventArgs e)
		{
			if (this.cbxPaperSize.SelectedIndex != -1)
			{
				this.CalcSizes();
			}
		}

		public PageSettings PageSettings//mtd398 PageSettings
		{
			get
			{
				return this._pageSettings;
			}
			set
			{
				this._pageSettings = value;
				this.var16();
			}
		}

		// Nested Types
		private class GroupControl : GroupBox//var17
		{
			public GroupControl()
			{
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.SetStyle(ControlStyles.UserPaint, true);
				base.SetStyle(ControlStyles.ResizeRedraw, true);
			}

		}

	}
}
