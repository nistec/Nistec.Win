using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using Nistec.Win;

namespace Nistec.Printing
{
    [ToolboxBitmap(typeof(McPrintPreviewDialog), "Images.PreviewDlg.bmp"), ToolboxItem(true), DefaultProperty("Document"), Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DesignTimeVisible(true)]
	public class McPrintPreviewDialog : Form
	{

		public static void Preview(McPrintDocument document)
		{
			try
			{
				McPrintPreviewDialog dlg=new McPrintPreviewDialog();
				dlg.Document=document;
				dlg.Show();
			}
			catch(Exception ex)
			{
				MsgBox.ShowError(ex.Message);
			}
		}

		#region Events
		// Events
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		public event EventHandler BackColorChanged;
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		public event EventHandler BackgroundImageChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler CausesValidationChanged;
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		public event EventHandler ContextMenuChanged;
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		public event EventHandler CursorChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler DockChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler EnabledChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler FontChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler ForeColorChanged;
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		public event EventHandler ImeModeChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler LocationChanged;
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		public event EventHandler MaximumSizeChanged;
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		public event EventHandler MinimumSizeChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler RightToLeftChanged;
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		public event EventHandler SizeChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler TabStopChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler TextChanged;
//		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//		public event EventHandler VisibleChanged;

		#endregion

		#region Members

		private new static readonly Size DefaultMinimumSize;
		//private IReportDocument _RptDoc;
		private ContextMenu menu;
		private System.Windows.Forms.ContextMenu ZoomDropDown;
		private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;


		public event EventHandler PageChange;
		public event EventHandler DropDownChange;
		private int DropDownIndex;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBarButton tbPrint;
		private System.Windows.Forms.ToolBarButton tbExport;
		private System.Windows.Forms.ToolBarButton tbFind;
		private System.Windows.Forms.ToolBarButton tbGroup1;
		private System.Windows.Forms.ToolBarButton tbPageOne;
		private System.Windows.Forms.ToolBarButton tbPageTwo;
		private System.Windows.Forms.ToolBarButton tbPageThree;
		private System.Windows.Forms.ToolBarButton tbPageFour;
		private System.Windows.Forms.ToolBarButton tbPageSix;
		private System.Windows.Forms.ToolBarButton tbGroup2;
		private System.Windows.Forms.ToolBarButton tbZoomIn;
		private System.Windows.Forms.ToolBarButton tbZoomOut;
		private System.Windows.Forms.ToolBarButton tbZoom;
		private System.Windows.Forms.ToolBarButton tbGroup3;
		private System.Windows.Forms.ToolBarButton tbClose;
		private System.Windows.Forms.ToolBarButton tbGroup4;
		private System.Windows.Forms.ToolBarButton tbPrev;
		private System.Windows.Forms.ToolBarButton tbNext;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.Label pageLabel;
		private System.Windows.Forms.NumericUpDown pageCounter;
		private McPrintPreview previewControl;
		private System.Windows.Forms.ImageList ImagesDesigner;
		private System.Windows.Forms.ToolBarButton tbFirst;
		private System.Windows.Forms.ToolBarButton tbLast;
        private System.Windows.Forms.ContextMenuStrip ctlContextMenu1;
		private System.Windows.Forms.ToolStripMenuItem cmPrint;
		private System.Windows.Forms.ToolStripMenuItem cmZoomIn;
		private System.Windows.Forms.ToolStripMenuItem cmZoomOut;
		private System.Windows.Forms.ToolStripMenuItem cm150;
		private System.Windows.Forms.ToolStripMenuItem cm25;
		private System.Windows.Forms.ToolStripMenuItem cm50;
		private System.Windows.Forms.ToolStripMenuItem cm75;
		private System.Windows.Forms.ToolStripMenuItem cm100;
		private System.Windows.Forms.ToolStripMenuItem cm200;
		private System.Windows.Forms.ToolStripMenuItem cmFirst;
		private System.Windows.Forms.ToolStripMenuItem cmNext;
		private System.Windows.Forms.ToolStripMenuItem cmBack;
		private System.Windows.Forms.ToolStripMenuItem cmLast;
		private System.Windows.Forms.ToolStripMenuItem cmClose;
		private System.Windows.Forms.ToolStripSeparator menuItem24;
        private System.Windows.Forms.ToolStripSeparator menuItem25;
        private System.Windows.Forms.ToolStripSeparator menuItem26;
        private System.Windows.Forms.ToolStripSeparator menuItem27;
		private System.Windows.Forms.ToolStripMenuItem cmAuto;
		private System.Windows.Forms.ToolStripMenuItem cm10;
		private string DropDownText;

		#endregion

		#region Constructor

		static McPrintPreviewDialog()
		{
			McPrintPreviewDialog.DefaultMinimumSize = new Size(0x177, 250);
		}

		public McPrintPreviewDialog()
		{
			base.SetStyle(ControlStyles.DoubleBuffer,true);

			this.menu = new ContextMenu();
			//base.AutoScaleBaseSize = new Size(5, 13);
			InitializeComponent();
			//Set Default Zoom
			this.menuItem9.Checked = true;
			this.cmAuto.Checked=true;
			this.CheckZoomMenu(this.menuItem9);
			this.previewControl.AutoZoom = true;
			DropDownIndex=8;
			this.MinimumSize = McPrintPreviewDialog.DefaultMinimumSize;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McPrintPreviewDialog));
			this.ZoomDropDown = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.tbPrint = new System.Windows.Forms.ToolBarButton();
			this.tbExport = new System.Windows.Forms.ToolBarButton();
			this.tbFind = new System.Windows.Forms.ToolBarButton();
			this.tbGroup1 = new System.Windows.Forms.ToolBarButton();
			this.tbPageOne = new System.Windows.Forms.ToolBarButton();
			this.tbPageTwo = new System.Windows.Forms.ToolBarButton();
			this.tbPageThree = new System.Windows.Forms.ToolBarButton();
			this.tbPageFour = new System.Windows.Forms.ToolBarButton();
			this.tbPageSix = new System.Windows.Forms.ToolBarButton();
			this.tbGroup2 = new System.Windows.Forms.ToolBarButton();
			this.tbZoomIn = new System.Windows.Forms.ToolBarButton();
			this.tbZoomOut = new System.Windows.Forms.ToolBarButton();
			this.tbZoom = new System.Windows.Forms.ToolBarButton();
			this.tbGroup3 = new System.Windows.Forms.ToolBarButton();
			this.tbClose = new System.Windows.Forms.ToolBarButton();
			this.tbGroup4 = new System.Windows.Forms.ToolBarButton();
			this.tbPrev = new System.Windows.Forms.ToolBarButton();
			this.tbNext = new System.Windows.Forms.ToolBarButton();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.tbFirst = new System.Windows.Forms.ToolBarButton();
			this.tbLast = new System.Windows.Forms.ToolBarButton();
			this.pageLabel = new System.Windows.Forms.Label();
			this.pageCounter = new System.Windows.Forms.NumericUpDown();
			this.ImagesDesigner = new System.Windows.Forms.ImageList(this.components);
			this.previewControl = new McPrintPreview();
            this.ctlContextMenu1 = new System.Windows.Forms.ContextMenuStrip();
			this.cmPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem24 = new System.Windows.Forms.ToolStripSeparator();
			this.cmZoomIn = new System.Windows.Forms.ToolStripMenuItem();
			this.cmZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem25 = new System.Windows.Forms.ToolStripSeparator();
			this.cmAuto = new System.Windows.Forms.ToolStripMenuItem();
			this.cm10 = new System.Windows.Forms.ToolStripMenuItem();
			this.cm25 = new System.Windows.Forms.ToolStripMenuItem();
			this.cm50 = new System.Windows.Forms.ToolStripMenuItem();
			this.cm75 = new System.Windows.Forms.ToolStripMenuItem();
			this.cm100 = new System.Windows.Forms.ToolStripMenuItem();
			this.cm150 = new System.Windows.Forms.ToolStripMenuItem();
			this.cm200 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem26 = new System.Windows.Forms.ToolStripSeparator();
			this.cmFirst = new System.Windows.Forms.ToolStripMenuItem();
			this.cmBack = new System.Windows.Forms.ToolStripMenuItem();
			this.cmNext = new System.Windows.Forms.ToolStripMenuItem();
			this.cmLast = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem27 = new System.Windows.Forms.ToolStripSeparator();
			this.cmClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pageCounter)).BeginInit();
			this.SuspendLayout();
			// 
			// ZoomDropDown
			// 
			this.ZoomDropDown.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem5,
																						 this.menuItem6,
																						 this.menuItem7,
																						 this.menuItem8,
																						 this.menuItem9});
			// 
			// menuItem1
			// 
			this.menuItem1.Text = "10%";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "25%";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Text = "50%";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Text = "75%";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Text = "100%";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Text = "150%";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Text = "200%";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Text = "400%";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Text = "Auto";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// tbPrint
			// 
			this.tbPrint.ImageIndex = 0;
			this.tbPrint.Tag = "Print";
			this.tbPrint.ToolTipText = "Print";
			// 
			// tbExport
			// 
			this.tbExport.ImageIndex = 1;
			this.tbExport.Tag = "Export";
			this.tbExport.ToolTipText = "Export";
			// 
			// tbFind
			// 
			this.tbFind.ImageIndex = 2;
			this.tbFind.Tag = "Find";
			this.tbFind.ToolTipText = "Find";
			this.tbFind.Visible = false;
			// 
			// tbGroup1
			// 
			this.tbGroup1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbPageOne
			// 
			this.tbPageOne.ImageIndex = 3;
			this.tbPageOne.Tag = "OnePage";
			this.tbPageOne.ToolTipText = "One Page";
			// 
			// tbPageTwo
			// 
			this.tbPageTwo.ImageIndex = 4;
			this.tbPageTwo.Tag = "TwoPages";
			this.tbPageTwo.ToolTipText = "Two Pages";
			// 
			// tbPageThree
			// 
			this.tbPageThree.ImageIndex = 5;
			this.tbPageThree.Tag = "ThreePages";
			this.tbPageThree.ToolTipText = "Three Pages";
			// 
			// tbPageFour
			// 
			this.tbPageFour.ImageIndex = 6;
			this.tbPageFour.Tag = "FourPages";
			this.tbPageFour.ToolTipText = "Four Pages";
			// 
			// tbPageSix
			// 
			this.tbPageSix.ImageIndex = 7;
			this.tbPageSix.Tag = "SixPages";
			this.tbPageSix.ToolTipText = "Six pages";
			// 
			// tbGroup2
			// 
			this.tbGroup2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbZoomIn
			// 
			this.tbZoomIn.ImageIndex = 8;
			this.tbZoomIn.Tag = "ZoomIn";
			this.tbZoomIn.ToolTipText = "Zoom In";
			// 
			// tbZoomOut
			// 
			this.tbZoomOut.ImageIndex = 9;
			this.tbZoomOut.Tag = "ZoomOut";
			this.tbZoomOut.ToolTipText = "Zoom Out";
			// 
			// tbZoom
			// 
			this.tbZoom.DropDownMenu = this.ZoomDropDown;
			this.tbZoom.ImageIndex = 10;
			this.tbZoom.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.tbZoom.Tag = "Zoom";
			// 
			// tbGroup3
			// 
			this.tbGroup3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbClose
			// 
			this.tbClose.ImageIndex = 11;
			this.tbClose.Tag = "Close";
			this.tbClose.ToolTipText = "Close";
			// 
			// tbGroup4
			// 
			this.tbGroup4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbPrev
			// 
			this.tbPrev.ImageIndex = 13;
			this.tbPrev.Tag = "PrevPage";
			this.tbPrev.ToolTipText = "Previous Page";
			// 
			// tbNext
			// 
			this.tbNext.ImageIndex = 14;
			this.tbNext.Tag = "NextPage";
			this.tbNext.ToolTipText = "Next Page";
			// 
			// toolBar1
			// 
			this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.tbPrint,
																						this.tbExport,
																						this.tbFind,
																						this.tbGroup1,
																						this.tbPageOne,
																						this.tbPageTwo,
																						this.tbPageThree,
																						this.tbPageFour,
																						this.tbPageSix,
																						this.tbGroup2,
																						this.tbZoomIn,
																						this.tbZoomOut,
																						this.tbZoom,
																						this.tbGroup3,
																						this.tbClose,
																						this.tbGroup4,
																						this.tbFirst,
																						this.tbPrev,
																						this.tbNext,
																						this.tbLast});
			this.toolBar1.ButtonSize = new System.Drawing.Size(23, 22);
			this.toolBar1.Controls.Add(this.pageLabel);
			this.toolBar1.Controls.Add(this.pageCounter);
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.ImagesDesigner;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(504, 28);
			this.toolBar1.TabIndex = 0;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// tbFirst
			// 
			this.tbFirst.ImageIndex = 12;
			this.tbFirst.Tag = "FirstPage";
			this.tbFirst.ToolTipText = "First Page";
			// 
			// tbLast
			// 
			this.tbLast.ImageIndex = 15;
			this.tbLast.Tag = "LastPage";
			this.tbLast.ToolTipText = "Last Page";
			// 
			// pageLabel
			// 
			this.pageLabel.Dock = System.Windows.Forms.DockStyle.Right;
			this.pageLabel.Location = new System.Drawing.Point(400, 0);
			this.pageLabel.Name = "pageLabel";
			this.pageLabel.Size = new System.Drawing.Size(40, 26);
			this.pageLabel.TabIndex = 0;
			this.pageLabel.Text = "Page";
			this.pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pageCounter
			// 
			this.pageCounter.Dock = System.Windows.Forms.DockStyle.Right;
			this.pageCounter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.pageCounter.Location = new System.Drawing.Point(440, 0);
			this.pageCounter.Maximum = new System.Decimal(new int[] {
																		1000,
																		0,
																		0,
																		0});
			this.pageCounter.Name = "pageCounter";
			this.pageCounter.Size = new System.Drawing.Size(64, 20);
			this.pageCounter.TabIndex = 1;
			this.pageCounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.pageCounter.Value = new System.Decimal(new int[] {
																	  1,
																	  0,
																	  0,
																	  0});
			this.pageCounter.ValueChanged += new System.EventHandler(this.UpdownMove);
			// 
			// ImagesDesigner
			// 
			this.ImagesDesigner.ImageSize = new System.Drawing.Size(16, 16);
			this.ImagesDesigner.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImagesDesigner.ImageStream")));
			this.ImagesDesigner.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// previewControl
			// 
			this.previewControl.AutoZoom = false;
			this.previewControl.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.previewControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.previewControl.ForeColor = System.Drawing.Color.White;
			this.previewControl.Location = new System.Drawing.Point(0, 28);
			this.previewControl.Name = "previewControl";
			this.previewControl.Size = new System.Drawing.Size(504, 297);
			this.previewControl.TabIndex = 1;
			this.previewControl.Zoom = 0.3;
			this.previewControl.StartPageChanged += new System.EventHandler(this.previewControl_StartPageChanged);
			// 
			// ctlContextMenu1
			// 
			this.ctlContextMenu1.ImageList = this.ImagesDesigner;
            this.ctlContextMenu1.RenderMode= ToolStripRenderMode.System;
            this.ctlContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
																							this.cmPrint,
																							this.menuItem24,
																							this.cmZoomIn,
																							this.cmZoomOut,
																							this.menuItem25,
																							this.cmAuto,
																							this.cm10,
																							this.cm25,
																							this.cm50,
																							this.cm75,
																							this.cm100,
																							this.cm150,
																							this.cm200,
																							this.menuItem26,
																							this.cmFirst,
																							this.cmBack,
																							this.cmNext,
																							this.cmLast,
																							this.menuItem27,
																							this.cmClose});
			// 
			// cmPrint
			// 
			this.cmPrint.ImageIndex = 0;
			this.cmPrint.Text = "Print";
			this.cmPrint.Click += new System.EventHandler(this.cmPrint_Click);
			// 
			// menuItem24
			// 
			this.menuItem24.Text = "-";
			// 
			// cmZoomIn
			// 
            this.cmZoomIn.ImageIndex = 8;
			this.cmZoomIn.Text = "ZoomIn";
			this.cmZoomIn.Click += new System.EventHandler(this.cmZoomIn_Click);
			// 
			// cmZoomOut
			// 
            this.cmZoomOut.ImageIndex = 9;
			this.cmZoomOut.Text = "ZoomOut";
			this.cmZoomOut.Click += new System.EventHandler(this.cmZoomOut_Click);
			// 
			// menuItem25
			// 
            //this.menuItem25.ImageIndex = 4;
			this.menuItem25.Text = "-";
			// 
			// cmAuto
			// 
			this.cmAuto.Text = "Auto";
			this.cmAuto.Click += new System.EventHandler(this.cmAuto_Click);
			// 
			// cm10
			// 
			this.cm10.Text = "10%";
			this.cm10.Click += new System.EventHandler(this.cm10_Click);
			// 
			// cm25
			// 
			this.cm25.Text = "25%";
			this.cm25.Click += new System.EventHandler(this.cm25_Click);
			// 
			// cm50
			// 
			this.cm50.Text = "50%";
			this.cm50.Click += new System.EventHandler(this.cm50_Click);
			// 
			// cm75
			// 
			this.cm75.Text = "75%";
			this.cm75.Click += new System.EventHandler(this.cm75_Click);
			// 
			// cm100
			// 
			this.cm100.Text = "100%";
			this.cm100.Click += new System.EventHandler(this.cm100_Click);
			// 
			// cm150
			// 
			this.cm150.Text = "150%";
			this.cm150.Click += new System.EventHandler(this.cm150_Click);
			// 
			// cm200
			// 
			this.cm200.Text = "200%";
			this.cm200.Click += new System.EventHandler(this.cm200_Click);
			// 
			// menuItem26
			// 
			this.menuItem26.Text = "-";
			// 
			// cmFirst
			// 
            this.cmFirst.ImageIndex = 12;
			this.cmFirst.Text = "First";
			this.cmFirst.Click += new System.EventHandler(this.cmFirst_Click);
			// 
			// cmBack
			// 
            this.cmBack.ImageIndex = 13;
			this.cmBack.Text = "Back";
			this.cmBack.Click += new System.EventHandler(this.cmBack_Click);
			// 
			// cmNext
			// 
            this.cmNext.ImageIndex = 14;
			this.cmNext.Text = "Next";
			this.cmNext.Click += new System.EventHandler(this.cmNext_Click);
			// 
			// cmLast
			// 
            this.cmLast.ImageIndex = 15;
			this.cmLast.Text = "Last ";
			this.cmLast.Click += new System.EventHandler(this.cmLast_Click);
			// 
			// menuItem27
			// 
			this.menuItem27.Text = "-";
			// 
			// cmClose
			// 
            this.cmClose.ImageIndex = 11;
			this.cmClose.Text = "Close";
			this.cmClose.Click += new System.EventHandler(this.cmClose_Click);
			// 
			// McPrintPreviewDialog
			// 
			this.TopLevel=true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 325);
			this.ContextMenuStrip = this.ctlContextMenu1;
			this.Controls.Add(this.previewControl);
			this.Controls.Add(this.toolBar1);
			this.Name = "McPrintPreviewDialog";
			this.Text = "Nistec Print Preview";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.toolBar1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pageCounter)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region ContextMenu Items

		private void cmPrint_Click(object sender, System.EventArgs e)
		{
			commands("Print");
		}

		private void cmZoomIn_Click(object sender, System.EventArgs e)
		{
			commands("ZoomIn");
		}

		private void cmZoomOut_Click(object sender, System.EventArgs e)
		{
			commands("ZoomOut");
		}

		private void cmAuto_Click(object sender, System.EventArgs e)
		{
			OnDropDownChange(e,8,"Auto"); 
			this.cmAuto.Checked = true;
			this.menuItem9.Checked = true;
			this.CheckZoomMenu(this.menuItem9);
			this.previewControl.AutoZoom = true;
		}

		private void cm10_Click(object sender, System.EventArgs e)
		{
			OnDropDownChange(e,0,"10%"); 
			this.cm10.Checked = true;
			this.menuItem1.Checked = true;
			this.previewControl.Zoom = 0.1;
		}

		private void cm25_Click(object sender, System.EventArgs e)
		{
			OnDropDownChange(e,1,"25%"); 
			this.cm25.Checked = true;
			this.menuItem2.Checked = true;
			this.previewControl.Zoom = 0.25;
		}

		private void cm50_Click(object sender, System.EventArgs e)
		{
			OnDropDownChange(e,2,"50%"); 
			this.cm50.Checked = true;
			this.menuItem3.Checked = true;
			this.previewControl.Zoom = .5;
		}

		private void cm75_Click(object sender, System.EventArgs e)
		{
			OnDropDownChange(e,3,"75%"); 
			this.cm75.Checked = true;
			this.menuItem4.Checked = true;
			this.previewControl.Zoom = 0.75;
		}

		private void cm100_Click(object sender, System.EventArgs e)
		{
			OnDropDownChange(e,4,"100%"); 
			this.cm100.Checked = true;
			this.menuItem5.Checked = true;
			this.previewControl.Zoom = 1;
		}

		private void cm150_Click(object sender, System.EventArgs e)
		{
			OnDropDownChange(e,5,"150%"); 
			this.cm150.Checked = true;
			this.menuItem6.Checked = true;
			this.previewControl.Zoom = 1.5;
		}

		private void cm200_Click(object sender, System.EventArgs e)
		{
			OnDropDownChange(e,6,"200%"); 
			this.cm200.Checked = true;
			this.menuItem7.Checked = true;
			this.previewControl.Zoom = 2;
		}

		private void cmFirst_Click(object sender, System.EventArgs e)
		{
			commands("FirstPage");
		}

		private void cmBack_Click(object sender, System.EventArgs e)
		{
			commands("PrevPage");
		}

		private void cmNext_Click(object sender, System.EventArgs e)
		{
			commands("NextPage");
		}

		private void cmLast_Click(object sender, System.EventArgs e)
		{
			commands("LastPage");
		}

		private void cmClose_Click(object sender, System.EventArgs e)
		{
			commands("Close");
		}

		#endregion

		#region ToolBar Items

		private void menuItem1_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,0,"10%"); 
			this.menuItem1.Checked = true;
			this.cm10.Checked = true;
			this.previewControl.Zoom = 0.1;
		}
		private void menuItem2_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,1,"25%"); 
			this.menuItem2.Checked = true;
			this.cm25.Checked = true;
			this.previewControl.Zoom = 0.25;
		}
		private void menuItem3_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,2,"50%"); 
			this.menuItem3.Checked = true;
			this.cm50.Checked = true;
			this.previewControl.Zoom = 0.5;
		}
		private void menuItem4_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,3,"75%"); 
			this.menuItem4.Checked = true;
			this.cm75.Checked = true;
			this.previewControl.Zoom = 0.75;
		}
		private void menuItem5_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,4,"100%"); 
			this.menuItem5.Checked = true;
			this.cm100.Checked = true;
			this.previewControl.Zoom = 1;
		}
		private void menuItem6_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,5,"150%"); 
			this.menuItem6.Checked = true;
			this.cm150.Checked = true;
			this.previewControl.Zoom = 1.5;
		}
		private void menuItem7_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,6,"200%"); 
			this.menuItem7.Checked = true;
			this.cm200.Checked = true;
			this.previewControl.Zoom = 2;
		}
		private void menuItem8_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,7,"400%"); 
			this.menuItem8.Checked = true;
			this.previewControl.Zoom = 4;

		}
		private void menuItem9_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,8,this.menuItem9.Text); 
			this.menuItem9.Checked = true;
			this.cmAuto.Checked = true;
			this.CheckZoomMenu(this.menuItem9);
			this.previewControl.AutoZoom = true;
		}

		protected virtual void OnDropDownChange(EventArgs e,int Index,string txt)
		{

			if(DropDownButton!=Index)
			{
				switch(DropDownIndex)
				{
					case 0:
						this.menuItem1.Checked = false;
						this.cm10.Checked=false;
						break;
					case 1:
						this.menuItem2.Checked = false;
						this.cm25.Checked=false;
						break;
					case 2:
						this.menuItem3.Checked = false;
						this.cm50.Checked=false;
						break;
					case 3:
						this.menuItem4.Checked = false;
						this.cm75.Checked=false;
						break;
					case 4:
						this.menuItem5.Checked = false;
						this.cm100.Checked=false;
						break;
					case 5:
						this.menuItem6.Checked = false;
						this.cm150.Checked=false;
						break;
					case 6:
						this.menuItem7.Checked = false;
						this.cm200.Checked=false;
						break;
					case 7:
						this.menuItem8.Checked = false;
						//this.cm400.Checked=false;
						break;
					case 8:
						this.menuItem9.Checked = false;
						this.cmAuto.Checked=false;
						break;
				}

				DropDownIndex=Index;
				DropDownText=txt;
				OnUpdownMove(e);
				if(DropDownChange!=null)
					DropDownChange(this,e);
			}
		}

		private void UpdownMove(object sender, EventArgs e)
		{
			OnUpdownMove(e);
		}

		protected virtual void OnUpdownMove(EventArgs e)
		{
			//this.p=(int)this.pageCounter.Value;
			previewControl.StartPage = ((int) this.pageCounter.Value) - 1;
			if(this.PageChange!=null) 
			{
				this.PageChange(this ,e);
			}
		}


		#endregion

		#region Commands

		private void commands(string cmd)
		{

			switch(cmd)
			{
				case "Print":
					if (this.previewControl.Document != null)
					{
						this.previewControl.Document.Print();
					}
					break;
				case "Export":
					ExportDocument();
					break;
				case "Find":
					//this._frmFind.Show();
					break;
				case "OnePage":
					this.previewControl.Rows = 1;
					this.previewControl.Columns = 1;
					break;
				case "TwoPages":
					this.previewControl.Rows = 1;
					this.previewControl.Columns = 2;
					break;
				case "ThreePages":
					this.previewControl.Rows = 1;
					this.previewControl.Columns = 3;
					break;
				case "FourPages":
					this.previewControl.Rows = 2;
					this.previewControl.Columns = 2;
					break;
				case "SixPages":
					this.previewControl.Rows = 2;
					this.previewControl.Columns = 3;
					break;
				case "ZoomIn":
					SetZoomIn();
					break;
				case "ZoomOut":
					SetZoomOut();
					break;
				case "Zoom":
					//this.ZoomAuto(null, EventArgs.Empty);
					break;
				case "NextPage":
					UpdownMovePage(1);
					//SetNextPage();
					break;
				case "PrevPage":
					UpdownMovePage(-1);
					//SetPrevPage();
					break;
				case "FirstPage":
					this.previewControl.StartPage = (int)0;
					break;
				case "LastPage":
					this.previewControl.StartPage = (int)this.previewControl.PagesCount-1;
					break;
				case "Close":
					Close();
					break;
				case "ZoomDropDown":
					break;
			}

		}

		#endregion

		#region ToolBar Properties

		public int DropDownButton
		{
			get{return this.DropDownIndex;}
		}

		public string DropDownSelected
		{
			get{return this.DropDownText;}
		}

//		public ToolBar ViewerMenu
//		{
//			get{return this.toolBar1;}
//		}
//
//		public NumericUpDown ViewerPageNumber
//		{
//			get{return this.pageCounter;}
//		}

		internal void SetPageInternal(int Value)
		{
			this.pageCounter.Value=(decimal)Value;
		}

		public int MaxPage
		{
			get{return (int)this.pageCounter.Maximum;}
			set{this.pageCounter.Maximum=(decimal)value;}
		}


		//		public Rectangle LastButton
		//		{
		//			get{return   this.tbNext.Rectangle;}
		//		}
		//		public TextBox CurrentPager
		//		{
		//			get{return   this.txtCurrentPage;}
		//		}

		#endregion

		#region Methods
 
		private void CheckZoomMenu(MenuItem toChecked)
		{
			foreach (MenuItem item1 in this.menu.MenuItems)
			{
				item1.Checked = toChecked == item1;
			}
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		protected override void CreateHandle()
		{
			if ((this.Document != null) && !this.Document.PrinterSettings.IsValid)
			{
				throw new InvalidPrinterException(this.Document.PrinterSettings);
			}
			base.CreateHandle();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			this.previewControl.InvalidatePreview();
		}

		private void previewControl_StartPageChanged(object sender, EventArgs e)
		{
			this.pageCounter.Value=(decimal) (this.previewControl.StartPage + 1);
			//this.toolBar1.ViewerPageNumber.Maximum=(decimal) (this.previewControl + 1);
		}

		private void toolBar1_PageChange(object sender, EventArgs e)
		{
			if(CheckValidPage((int)this.pageCounter.Value))
			{
				this.previewControl.StartPage =(int)this.pageCounter.Value;
			}
			else
			{
				this.SetPageInternal(this.previewControl.StartPage);
			}
		}

		internal  bool ShouldSerializeAutoScaleBaseSize()
		{
			return false;
		}

		internal bool ShouldSerializeMinimumSize()
		{
			return !this.MinimumSize.Equals(McPrintPreviewDialog.DefaultMinimumSize);
		}

		internal  bool ShouldSerializeText()
		{
			return !this.Text.Equals("PrintPreview");
		}

		private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			commands(e.Button.Tag.ToString());
		}


		public bool CheckValidPage(int Value)
		{
			if(Value + this.previewControl.CurrentPage <= 0)
				return false;
			if((Value + this.previewControl.CurrentPage) > (this.previewControl.PagesCount))
				return false;
			return true;
		}

		internal void UpdownMovePage(int Value)
		{
			if(Value + this.previewControl.CurrentPage < 0)
				return;
			if((Value + this.previewControl.CurrentPage) > (this.previewControl.PagesCount))
				return;
			this.previewControl.StartPage += (int) Value;
		}

		private void SetZoomIn()
		{
			if(this.previewControl.Zoom > 0.20)
			{
				this.previewControl.Zoom -= 0.1;
			}
		}

		private void SetZoomOut()
		{
			if(this.previewControl.Zoom < 7.8)
			{
				this.previewControl.Zoom += 0.1;
			}

		}

		private void ExportDocument()
		{

			if(!(this.Document is McPrintDocument))
			{
				MsgBox.ShowWarning("Export method not supproted in This document ");
				return;
			}
			try
			{
				((McPrintDocument)this.Document).ExportDocument();
			}
			catch(Exception ex)
			{
				MsgBox.ShowWarning(ex.Message);
			}
		}

//		private void SaveToTif()
//		{
//			if(RptDocument==null)
//			{
//
//				return;
//			}
//			try
//			{
//				string filePath= Util.CommonDialog.SaveAs("Tif File (*.tif)|*.tif"); 
//				if(filePath!="")
//				{
//					RptDocument.PrintToTifFile(filePath);
//					//Commands.OpenFile(filePath);
//				}
//			}
//			catch(Exception ex)
//			{
//               MessageBox.Show(ex.ToString()); 
//			}
//		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel (e);

			//			if(this.previewControl.PagesCount<=10)
			//				return;
			//          
			//			int pagCount=this.previewControl.CurrentPage ;
			//			int CurrentInt = this.previewControl.CurrentPage ; 
			//			int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
			//		    
			//			if (CurrentInt+Delta >= pagCount )
			//				this.previewControl.StartPage =pagCount-1;
			//			else if (CurrentInt+Delta < 0 )
			//				this.previewControl.StartPage =0;
			//			else
			//				this.previewControl.StartPage = CurrentInt+Delta;
			
			//Parent.Text =Items[this.internalList.SelectedIndex].ToString (); 
		}



		#endregion

		#region Properties
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new IButtonControl AcceptButton
		{
			get
			{
				return base.AcceptButton;
			}
			set
			{
				base.AcceptButton = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new string AccessibleDescription
		{
			get
			{
				return base.AccessibleDescription;
			}
			set
			{
				base.AccessibleDescription = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new string AccessibleName
		{
			get
			{
				return base.AccessibleName;
			}
			set
			{
				base.AccessibleName = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new AccessibleRole AccessibleRole
		{
			get
			{
				return base.AccessibleRole;
			}
			set
			{
				base.AccessibleRole = value;
			}
		}
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}
 
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //public new bool AutoScale
        //{
        //    get
        //    {
        //        return base.AutoScale;
        //    }
        //    set
        //    {
        //        base.AutoScale = value;
        //    }
        //}

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new AutoScaleMode AutoScaleMode
        {
            get
            {
                return base.AutoScaleMode;
            }
            set
            {
                base.AutoScaleMode = value;
            }
        }
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override Size AutoScaleBaseSize
		{
			get
			{
				return base.AutoScaleBaseSize;
			}
			set
			{
			}
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new  Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new IButtonControl CancelButton
		{
			get
			{
				return base.CancelButton;
			}
			set
			{
				base.CancelButton = value;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool ControlBox
		{
			get
			{
				return base.ControlBox;
			}
			set
			{
				base.ControlBox = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new ControlBindingsCollection DataBindings
		{
			get
			{
				return base.DataBindings;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}
 
		[Description("PrintPreviewDocument"), DefaultValue((string) null), Category("Behavior")]
		public PrintDocument Document
		{
			get
			{
				return this.previewControl.Document;
			}
			set
			{
				this.previewControl.Document = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new FormBorderStyle FormBorderStyle
		{
			get
			{
				return base.FormBorderStyle;
			}
			set
			{
				base.FormBorderStyle = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new bool HelpButton
		{
			get
			{
				return base.HelpButton;
			}
			set
			{
				base.HelpButton = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Icon Icon
		{
			get
			{
				return base.Icon;
			}
			set
			{
				base.Icon = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool IsMdiContainer
		{
			get
			{
				return base.IsMdiContainer;
			}
			set
			{
				base.IsMdiContainer = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new bool KeyPreview
		{
			get
			{
				return base.KeyPreview;
			}
			set
			{
				base.KeyPreview = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool MaximizeBox
		{
			get
			{
				return base.MaximizeBox;
			}
			set
			{
				base.MaximizeBox = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new MainMenu Menu
		{
			get
			{
				return base.Menu;
			}
			set
			{
				base.Menu = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(false)]
		public new bool MinimizeBox
		{
			get
			{
				return base.MinimizeBox;
			}
			set
			{
				base.MinimizeBox = value;
			}
		}
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public new double Opacity
		{
			get
			{
				return base.Opacity;
			}
			set
			{
				base.Opacity = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Description("PrintPreviewPrintPreviewControl"), Category("Behavior")]
		public McPrintPreview PrintPreviewControl
		{
			get
			{
				return this.previewControl;
			}
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DefaultValue(false)]
		public new bool ShowInTaskbar
		{
			get
			{
				return base.ShowInTaskbar;
			}
			set
			{
				base.ShowInTaskbar = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}
		[Browsable(false), DefaultValue(2), EditorBrowsable(EditorBrowsableState.Never)]
		public new SizeGripStyle SizeGripStyle
		{
			get
			{
				return base.SizeGripStyle;
			}
			set
			{
				base.SizeGripStyle = value;
			}
		}
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new FormStartPosition StartPosition
		{
			get
			{
				return base.StartPosition;
			}
			set
			{
				base.StartPosition = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new object Tag
		{
			get
			{
				return base.Tag;
			}
			set
			{
				base.Tag = value;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new bool TopMost
		{
			get
			{
				return base.TopMost;
			}
			set
			{
				base.TopMost = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new Color TransparencyKey
		{
			get
			{
				return base.TransparencyKey;
			}
			set
			{
				base.TransparencyKey = value;
			}
		}
 
		[Category("Behavior"), DefaultValue(false), Description("PrintPreviewAntiAlias")]
		public bool UseAntiAlias
		{
			get
			{
				return this.PrintPreviewControl.UseAntiAlias;
			}
			set
			{
				this.PrintPreviewControl.UseAntiAlias = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new  bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new FormWindowState WindowState
		{
			get
			{
				return base.WindowState;
			}
			set
			{
				 base.WindowState = value;
			}
		}

//		public IReportDocument RptDocument
//		{
//			get{return _RptDoc;}
//			set{_RptDoc=value;}
//		}

		#endregion
	}
 
}
