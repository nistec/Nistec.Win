using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Resources;
using System.Data;

using Nistec.Printing.View;
using Nistec.Printing.View.Html;
using Nistec.Printing.View.Img;
using Nistec.Printing.View.Pdf;



namespace Nistec.Printing.View.Viewer
{
	public class PrintViewer : UserControl
	{


		#region Members
		// Fields
		private FindDlg _FindDlg;//_FindDlg;
		private FindInfo _FindInfo;//var1;
		private RectangleF[] _RectF;//var10;
		private Pager _pager;//var11
        private float _PageWidth;//var12;
        private float _PageHeight;//var13;
		private Font _Font;//var14;
		private int var15;
		private bool var16;
		private Document _Document;
		private RectangleF var18;
		private int var2;
		private InternalPanel _internalPanel1;//var21;
		private Panel _Panel;//var23;
		//private ComboBox _ComboBox;//var24;
		private TextBox _TextBox;//var25;
		private VScrollBar _VScrollBar;//var26;
		private HScrollBar _HScrollBar;//var27;
		private Label _Label1;//var28;
		private Label _Label2;//var29;
		private int var3;
		private InternalPanel _internalPanel2;//var30;
		private InternalPanel _internalPanel3;//var31;
		private ToolTip _ToolTip;//var32;
		private int var4;
		private int _CurrentPage;//var5;
		private float var6;
		private float var7;
		private float var8;
		private RectangleF var9;
		private IContainer components;
		internal PrintDialog PrintDialog;
		private PointF[] var55;
		private PointF[] var56;
		private PointF[] var57;
		private PointF[] var58;
		private PointF[] var59;
		private PointF[] var60;
		private System.Windows.Forms.ImageList ImagesDesigner;

		private ViewerBar _MenuBar;
		internal Form owner;
 
		#endregion

		#region Constructor

		public PrintViewer()
		{
			//netFramwork.//NetReflectedFram();

			PointF[] tfArray1 = new PointF[] { new PointF(24f, 4f), new PointF(24f, 22f) };
			this.var55 = tfArray1;
			tfArray1 = new PointF[] { new PointF(88f, 4f), new PointF(88f, 22f) };
			this.var56 = tfArray1;
			tfArray1 = new PointF[] { new PointF(116f, 4f), new PointF(116f, 22f) };
			this.var57 = tfArray1;
			tfArray1 = new PointF[] { new PointF(144f, 4f), new PointF(144f, 22f) };
			this.var58 = tfArray1;
			tfArray1 = new PointF[] { new PointF(268f, 4f), new PointF(268f, 22f) };
			this.var59 = tfArray1;
			tfArray1 = new PointF[] { new PointF(407f, 4f), new PointF(407f, 22f) };
			this.var60 = tfArray1;
			this.InitializeComponent();
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this.Init();
			this.InitCtls();
			SetContextMenu();
			this._internalPanel1.Focus();
		}

//		protected override void OnHandleCreated(EventArgs e)
//		{
//			base.OnHandleCreated (e);
//			netFramWinRpt.NetFram();
//		}


		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}


		#endregion

		#region InitializeComponent()

		public void Generate(Stream stream)
		{
			Report report1 = new Report();
			report1.LoadLayout(ref stream);
			report1.Generate();
			this.SetDocument(report1.Document);
		}

 
		public void Generate(string fileName)
		{
			Report report1 = new Report();
			report1.LoadLayout(fileName);
			report1.Generate();
			this.SetDocument(report1.Document);
		}

 
		public void Generate(ref Report report)
		{
			report.Generate();
			this.SetDocument(report.Document);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrintViewer));
			this.PrintDialog = new System.Windows.Forms.PrintDialog();
			this.ImagesDesigner = new System.Windows.Forms.ImageList(this.components);
			// 
			// ImagesDesigner
			// 
			this.ImagesDesigner.ImageSize = new System.Drawing.Size(16, 16);
			this.ImagesDesigner.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImagesDesigner.ImageStream")));
			this.ImagesDesigner.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// CtlPrintViewer
			// 
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.Name = "CtlPrintViewer";
			this.Size = new System.Drawing.Size(400, 400);
			this.Resize += new System.EventHandler(this.Viewer_Resize);

		}

		private void Init()//var19
		{
			base.SuspendLayout();
			this._FindInfo = new FindInfo();
			this._FindDlg = new FindDlg();
			this._FindDlg.printViewer = this;
			this._RectF = new RectangleF[6];
			this._pager = Pager.None;
			this._Font = new Font("Arial", 7f);
			this.SetDocument(null);
			this._Panel = new Panel();
			//this._ComboBox = new ComboBox();
			this._TextBox = new TextBox();
			this._VScrollBar = new VScrollBar();
			this._HScrollBar = new HScrollBar();
			this._Label1 = new Label();
			this._Label2 = new Label();
			this._internalPanel2 = new PrintViewer.InternalPanel();
			this._internalPanel3 = new PrintViewer.InternalPanel();
			this._internalPanel1 = new PrintViewer.InternalPanel();
			this._ToolTip = new ToolTip();
			this._ToolTip.AutoPopDelay = 0x7d0;
			this._ToolTip.InitialDelay = 500;
			this._ToolTip.ReshowDelay = 500;
			this._ToolTip.ShowAlways = true;
			//			this._ComboBox.Name = "PageZoom";
			//			this._ComboBox.Location = new Point(320, 4);
			//			this._ComboBox.Size = new Size(0x54, 0x18);
			//			this._ComboBox.Items.AddRange(new object[] { "10%", "25%", "50%", "75%", "100%", "200%", "400%", "800%", "Page Width", "Whole Page" });
			//			this._ComboBox.SelectedItem = "100%";
			//			this._ToolTip.SetToolTip(this._ComboBox, "Zoom");
			//			this._ComboBox.SelectedIndexChanged += new EventHandler(this.var34);
			//			this._ComboBox.KeyPress += new KeyPressEventHandler(this.var35);
			this._TextBox.Name = "Pages";
			this._TextBox.Text = "1/1";
			//this._TextBox.Location = new Point(0x1cb, 4);
			this._TextBox.Size = new Size(60, 0x18);
			this._ToolTip.SetToolTip(this._TextBox, "Current Page Number");
			this._TextBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);//var36
			this._TextBox.MouseWheel += new MouseEventHandler(this.TextBox_MouseWheel);//var37
			this._Panel.BackColor = SystemColors.Control;
			this._Panel.Dock = DockStyle.Top;
			this._Panel.Name = "ToolBar";
			this._Panel.Size = new Size(400, 0x20);
			this._TextBox.Location = new Point(0x1cb-40, 4);
			this._TextBox.Dock = System.Windows.Forms.DockStyle.None;
			this._Panel.Controls.AddRange(new Control[] {this._TextBox });
			//this._Panel.Controls.AddRange(new Control[] { this._ComboBox, this._TextBox });
			this._Panel.Paint += new PaintEventHandler(this.Panel_Paint);//var38
			this._VScrollBar.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
			this._VScrollBar.Location = new Point(0x180, 0x20);
			this._VScrollBar.Name = "VSBar";
			this._VScrollBar.Size = new Size(0x10, 0x160);
			this._VScrollBar.Scroll += new ScrollEventHandler(this.VScrollBar_Scroll);//var39
			this._HScrollBar.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
			this._HScrollBar.BackColor = SystemColors.Control;
			this._HScrollBar.Location = new Point(0, 0x180);
			this._HScrollBar.Name = "HSBar";
			this._HScrollBar.Size = new Size(0x180, 0x10);
			this._HScrollBar.Scroll += new ScrollEventHandler(this.HScrollBar_Scroll);//var40
			this._Label1.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			this._Label1.BackColor = SystemColors.Control;
			this._Label1.Name = "RepProperty";
			this._Label1.Size = new Size(0x18, 0x18);
			this._Label1.Location = new Point(0, 0x20);
			this._Label1.Paint += new PaintEventHandler(this.Label1_Paint);//var41
			this._Label2.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			this._Label2.BackColor = SystemColors.Control;
			this._Label2.Name = "FillerBottom";
			this._Label2.Size = new Size(0x10, 0x10);
			this._Label2.Location = new Point(0x180, 0x180);
			this._internalPanel2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			this._internalPanel2.BackColor = SystemColors.Control;
			this._internalPanel2.Name = "TopRuler";
			this._internalPanel2.Size = new Size(360, 0x18);
			this._internalPanel2.Location = new Point(0x18, 0x20);
			this._internalPanel2.Paint += new PaintEventHandler(this.internalPanel2_Paint);//var42
			this._internalPanel3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
			this._internalPanel3.BackColor = SystemColors.Control;
			this._internalPanel3.Location = new Point(0, 0x38);
			this._internalPanel3.Name = "SideRuler";
			this._internalPanel3.Size = new Size(0x18, 0x156);
			this._internalPanel3.Paint += new PaintEventHandler(this.internalPanel3_Paint);//var43
			this._internalPanel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
			this._internalPanel1.BackColor = SystemColors.ControlDark;
			this._internalPanel1.Location = new Point(0x18, 0x38);
			this._internalPanel1.Name = "gPanel";
			this._internalPanel1.Size = new Size(360, 0x148);
			this._internalPanel1.Paint += new PaintEventHandler(this.internalPanel_Paint);//var44
			this._internalPanel1.Click += new EventHandler(this.internalPanel_Click);//var45
			base.Controls.AddRange(new Control[] { this._Panel, this._VScrollBar, this._HScrollBar, this._Label1, this._Label2, this._internalPanel2, this._internalPanel3, this._internalPanel1 });
			base.ResumeLayout(false);
		}

		#endregion

		#region ToolBar
 
		public ToolBar MenuBar
		{
			get{return this._MenuBar;} 
		}

		private void InitCtls()
		{
			//ViewerMenuBar mnu=new ViewerMenuBar();
			//Bar mnu=new Bar();
			this._MenuBar=new ViewerBar();
			//this._MenuBar= mnu.ViewerMenu;
            this._MenuBar.ButtonClick+=new ToolBarButtonClickEventHandler(_MenuBar_ButtonClick);
			this._MenuBar.ButtonDropDown+=new ToolBarButtonClickEventHandler(_MenuBar_ButtonDropDown);
			this._MenuBar.DropDownChange+=new EventHandler(_MenuBar_DropDownChange);

//			this._TextBox.Name = "Pages";
//			this._TextBox.Text = "1/1";
//			this._TextBox.Location = new Point(0x1cb, 4);
//			//this._TextBox.Location = new Point(mnu.LastButton.X + 10, mnu.LastButton.Y);
//
//			//this._TextBox.Location = mnu.CurrentPager.Location;
//			//this._TextBox.Size = mnu.CurrentPager.Size;
//	
//			this._TextBox.Size = new Size(60, 0x18);
//			this._ToolTip.SetToolTip(this._TextBox, "Current Page Number");
//			this._TextBox.KeyPress += new KeyPressEventHandler(this.var36);
//			this._TextBox.MouseWheel += new MouseEventHandler(this.var37);
//			this._Panel.BackColor = SystemColors.Control;
//			this._Panel.Dock = DockStyle.Top;
//			this._Panel.Name = "ToolBar";
//			this._Panel.Size = new Size(400, 0x20);
			this._Panel.Controls.AddRange(new Control[] { this._MenuBar});


			//this._Panel.Controls.Add( mnu.ViewerMenu );


		}

		private void _MenuBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
          OnButtonClick(e);
		}

		private void _MenuBar_ButtonDropDown(object sender, ToolBarButtonClickEventArgs e)
		{
          OnButtonDropDown(e); 
		}

		private void _MenuBar_DropDownChange(object sender, EventArgs e)
		{
			SetZoom();

		}

		protected virtual void OnButtonClick(ToolBarButtonClickEventArgs e)
		{
			switch(e.Button.Tag.ToString())
			{
				case "Print":
					this.Print();
					break;
				case "Export":
					SetExport();
					break;
				case "Find":
					this._FindDlg.Show();
					break;
				case "OnePage":
					SetPageView("P1");
					break;
				case "TwoPages":
					SetPageView("P2");
					break;
				case "ThreePages":
					SetPageView("P3");
					break;
				case "FourPages":
					SetPageView("P4");
					break;
				case "SixPages":
					SetPageView("P6");
					break;
				case "ZoomIn":
					SetZoomIn();
					break;
				case "ZoomOut":
					SetZoomOut();
					break;
				case "Zoom":
					break;
				case "NextPage":
					SetNextPage();
					break;
				case "PrevPage":
					SetPrevPage();
					break;
				case "FirstPage":
					SetFirstPage();
					break;
				case "LastPage":
					SetLastPage();
					break;
				case "Close":
					this.FindForm().Close();
					break;
				case "ZoomDropDown":
					break;
			}
		}

		protected virtual void OnButtonDropDown(ToolBarButtonClickEventArgs e)
		{

		}

		private void InitCtlsOld()
		{
//			CtlInternal cls1 = new CtlInternal("Print", "Print", new Size(60, 0x18));
//			cls1.Location = new Point(0x1a, 2);
//			cls1.cls8 = FlatStyle.Flat;
//			cls1.Text = "Print..";
//			cls1.cls9 = this.ImageList1.Images[0];
//			cls1.MouseUp += new MouseEventHandler(this.var61);
//			CtlInternal cls2 = new CtlInternal("Export", "Export", new Size(0x18, 0x18));
//			cls2.Location = new Point(90, 2);
//			cls2.cls8 = FlatStyle.Flat;
//			cls2.cls9 = this.ImageList1.Images[1];
//			cls2.MouseUp += new MouseEventHandler(this.var62);
//			CtlInternal cls3 = new CtlInternal("Find", "Find", new Size(0x18, 0x18));
//			cls3.Location = new Point(0x76, 2);
//			cls3.cls8 = FlatStyle.Flat;
//			cls3.cls9 = this.ImageList1.Images[2];
//			cls3.MouseUp += new MouseEventHandler(this.var63);
//			CtlInternal cls4 = new CtlInternal("P1", "One Page", new Size(0x18, 0x18));
//			cls4.Location = new Point(0x92, 2);
//			cls4.cls8 = FlatStyle.Flat;
//			cls4.cls9 = this.ImageList1.Images[3];
//			cls4.MouseUp += new MouseEventHandler(this.var64);
//			CtlInternal CtlInternal = new CtlInternal("P2", "Two Pages", new Size(0x18, 0x18));
//			CtlInternal.Location = new Point(170, 2);
//			CtlInternal.cls8 = FlatStyle.Flat;
//			CtlInternal.cls9 = this.ImageList1.Images[4];
//			CtlInternal.MouseUp += new MouseEventHandler(this.var64);
//			CtlInternal cls6 = new CtlInternal("P3", "Three Pages", new Size(0x18, 0x18));
//			cls6.Location = new Point(0xc2, 2);
//			cls6.cls8 = FlatStyle.Flat;
//			cls6.cls9 = this.ImageList1.Images[5];
//			cls6.MouseUp += new MouseEventHandler(this.var64);
//			CtlInternal cls7 = new CtlInternal("P4", "Four Pages", new Size(0x18, 0x18));
//			cls7.Location = new Point(0xda, 2);
//			cls7.cls8 = FlatStyle.Flat;
//			cls7.cls9 = this.ImageList1.Images[6];
//			cls7.MouseUp += new MouseEventHandler(this.var64);
//			CtlInternal cls8 = new CtlInternal("P6", "Six Pages", new Size(0x18, 0x18));
//			cls8.Location = new Point(0xf2, 2);
//			cls8.cls8 = FlatStyle.Flat;
//			cls8.cls9 = this.ImageList1.Images[7];
//			cls8.MouseUp += new MouseEventHandler(this.var64);
//			CtlInternal cls9 = new CtlInternal("ZoomDown", "Zoom In", new Size(0x18, 0x18));
//			cls9.Location = new Point(270, 2);
//			cls9.cls8 = FlatStyle.Flat;
//			cls9.cls9 = this.ImageList1.Images[8];
//			cls9.MouseUp += new MouseEventHandler(this.var65);
//			CtlInternal cls10 = new CtlInternal("ZoomUp", "Zoom Out", new Size(0x18, 0x18));
//			cls10.Location = new Point(0x126, 2);
//			cls10.cls8 = FlatStyle.Flat;
//			cls10.cls9 = this.ImageList1.Images[9];
//			cls10.MouseUp += new MouseEventHandler(this.var66);
//			CtlInternal cls11 = new CtlInternal("PageUp", "Next Page", new Size(0x18, 0x18));
//			cls11.Location = new Point(0x199, 2);
//			cls11.cls8 = FlatStyle.Flat;
//			cls11.cls9 = this.ImageList1.Images[10];
//			cls11.MouseUp += new MouseEventHandler(this.var67);
//			CtlInternal cls12 = new CtlInternal("PageDown", "Previous Page", new Size(0x18, 0x18));
//			cls12.Location = new Point(0x1b1, 2);
//			cls12.cls8 = FlatStyle.Flat;
//			cls12.cls9 = this.ImageList1.Images[11];
//			Uregion12.MouseUp += new MouseEventHandler(this.var68);
//			this._Panel.Controls.AddRange(new Control[] { cls1, cls2, cls3, cls4, CtlInternal, cls6, cls7, cls8, cls10, cls9, cls11, cls12 });
		}

		#endregion

		#region override 

		private void Viewer_Resize(object sender, EventArgs e)
		{
			if ((this._pager == Pager.None) | (this._pager == Pager.One))
			{
				this.SetPrivewSize(this.var8);
			}
			else
			{
				this.Resetting();
			}
		}

		#endregion

		#region Event

		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.var82(0);
			}
			else if (((e.KeyChar != '/') && !char.IsControl(e.KeyChar)) && !char.IsNumber(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel (e);
			int num1 = 1;
			if (e.Delta < 0)
			{
				num1 = -1;
			}
			this.var82(num1);

		}

		private void TextBox_MouseWheel(object sender, MouseEventArgs e)
		{
			OnMouseWheel(e);
//			int num1 = 1;
//			if (e.Delta < 0)
//			{
//				num1 = -1;
//			}
//			this.var82(num1);
		}

		private void Panel_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics1 = e.Graphics;
			graphics1.DrawLine(new Pen(SystemColors.ControlDark, 1f), 0, 30, this._Panel.Width, 30);
			graphics1.DrawLine(new Pen(SystemColors.ControlLightLight, 1f), 0, 0x1f, this._Panel.Width, 0x1f);
			graphics1.DrawLines(new Pen(SystemColors.ControlDark, 1f), this.var55);
			graphics1.DrawLines(new Pen(SystemColors.ControlDark, 1f), this.var56);
			graphics1.DrawLines(new Pen(SystemColors.ControlDark, 1f), this.var57);
			graphics1.DrawLines(new Pen(SystemColors.ControlDark, 1f), this.var58);
			graphics1.DrawLines(new Pen(SystemColors.ControlDark, 1f), this.var59);
			graphics1.DrawLines(new Pen(SystemColors.ControlDark, 1f), this.var60);
		}

 
		private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			this.InvalidateRegions();
			this.var9.Y = (this.var9.Y + this.var7) - e.NewValue;
			this._RectF[0].Y = (this._RectF[0].Y + this.var7) - e.NewValue;
			this.InvalidateRegions();
			this._internalPanel2.Invalidate();
			this._internalPanel3.Invalidate();
			this.var7 = e.NewValue;
		}

		private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			this.InvalidateRegions();
			this.var9.X = (this.var9.X + this.var6) - e.NewValue;
			this._RectF[0].X = (this._RectF[0].X + this.var6) - e.NewValue;
			this.InvalidateRegions();
			this._internalPanel2.Invalidate();
			this._internalPanel3.Invalidate();
			this.var6 = e.NewValue;
		}

 
		private void Label1_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics1 = e.Graphics;
			graphics1.DrawRectangle(new Pen(SystemColors.ControlDark, 1f), 3, 3, 0x12, 0x12);
			graphics1.DrawRectangle(new Pen(SystemColors.ControlLightLight, 1f), 4, 4, 0x10, 0x10);
		}

 
		private void internalPanel2_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics1 = e.Graphics;
			GraphicsUnit unit1 = graphics1.PageUnit;
			graphics1.PageUnit = GraphicsUnit.Display;
			this.PaintFill(graphics1, sender);
			graphics1.PageUnit = unit1;
		}

 
		private void internalPanel3_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics1 = e.Graphics;
			GraphicsUnit unit1 = graphics1.PageUnit;
			graphics1.PageUnit = GraphicsUnit.Display;
			this.PaintFill2(graphics1, sender);
			graphics1.PageUnit = unit1;
		}

 
		private void internalPanel_Paint(object sender, PaintEventArgs e)
		{
			if (this._Document != null)
			{
				this._internalPanel1.SuspendLayout();
				Graphics graphics1 = e.Graphics;
				graphics1.PageUnit = GraphicsUnit.Display;
				if ((this._pager == Pager.None) | (this._pager == Pager.One))
				{
					graphics1.FillRectangle(SystemBrushes.Window, this.var9);
					graphics1.DrawRectangle(SystemPens.WindowFrame, this.var9.X, this.var9.Y, this.var9.Width, this.var9.Height);
					GraphicsContainer container1 = graphics1.BeginContainer();
					graphics1.TranslateTransform(this.var9.X, this.var9.Y);
					graphics1.ScaleTransform(this.var8, this.var8);
					this._Document.PrintPage(graphics1, this._CurrentPage - 1, 0f);
					this.DrawRectPage(graphics1, this._CurrentPage);//var71
					graphics1.EndContainer(container1);
				}
				else
				{
					int num1 = 0;
					for (int num2 = this.var3; num2 <= this.var4; num2++)
					{
						RectangleF ef1 = this._RectF[num1];
						graphics1.FillRectangle(SystemBrushes.Window, ef1);
						graphics1.DrawRectangle(SystemPens.WindowFrame, ef1.X, ef1.Y, ef1.Width, ef1.Height);
						GraphicsContainer container2 = graphics1.BeginContainer();
						graphics1.TranslateTransform(ef1.X, ef1.Y);
						graphics1.ScaleTransform(this.var8, this.var8);
						this._Document.PrintPage(graphics1, num2, 0f);
						this.DrawRectPage(graphics1, num2 + 1);
						graphics1.EndContainer(container2);
						num1++;
					}
				}
				this._internalPanel1.ResumeLayout(false);
			}
		}

 
		private void internalPanel_Click(object sender, EventArgs e)
		{
			this._internalPanel1.Focus();
		}

 
		private void PaintFill(Graphics g, object var48)//var46
		{
			float single1 = (this.var18.Left * ReportUtil.Dpi) * this.var8;
			float single2 = this.var9.X + ((this.var18.Right * ReportUtil.Dpi) * this.var8);
			g.FillRectangle(SystemBrushes.Window, this.var9.X, 5f, this.var9.Width, 14f);
			g.FillRectangle(SystemBrushes.ControlDark, this.var9.X, 5f, single1, 14f);
			g.FillRectangle(SystemBrushes.ControlDark, single2, 5f, this.var9.Right - single2, 14f);
			float single3 = this._internalPanel2.Height / 2;
			float single4 = single1 + this.var9.X;
			float single5 = this.var9.Right;
			float single6 = this.var2 * this.var8;
			this.DrawLines1(g, single4, single5, single6, single3);
			single5 = this.var9.Left;
			this.DrawLines1(g, single4, single5, -single6, single3);
		}

		private void PaintFill2(Graphics g, object var48)//var47
		{
			float single1 = this.var9.Y + ((this.var18.Top * ReportUtil.Dpi) * this.var8);
			float single2 = this.var9.Y + ((this.var18.Bottom * ReportUtil.Dpi) * this.var8);
			g.FillRectangle(SystemBrushes.Window, 5f, this.var9.Y, 14f, this.var9.Height);
			g.FillRectangle(SystemBrushes.ControlDark, 5f, this.var9.Y, 14f, single1 - this.var9.Y);
			g.FillRectangle(SystemBrushes.ControlDark, 5f, single2, 14f, this.var9.Bottom - single2);
			float single3 = this._internalPanel3.Width / 2;
			float single4 = single1;
			float single5 = this.var9.Bottom;
			float single6 = this.var2 * this.var8;
			this.DrawLines2(g, single4, single5, single6, single3);
			single5 = this.var9.Y;
			this.DrawLines2(g, single4, single5, -single6, single3);
		}

 
		private void DrawLines1(Graphics g, float singl4 , float singl5 , float singl6 , float singl3 )//var49
		{
			int num1 = 1;
			int num2 = 0;
			int num3 = 4;
			int num4 = 8;
			int num5 = 1;
			if (singl6 < 0f)
			{
				num5 = -1;
				num1 = -1;
				num3 = -4;
				num4 = -8;
			}
			for (float single1 = singl4 + singl6; single1 <= singl5; single1 += singl6)
			{
				num2 += num5;
				if ((num2 == num3) && (this.var8 >= 0.15f))
				{
					g.DrawLine(SystemPens.ControlText, single1, singl3 - 3f, single1, singl3 + 3f);
				}
				else if (num2 == num4)
				{
					int num6 = Math.Abs(num1);
					SizeF ef1 = g.MeasureString(num6.ToString(), this._Font);
					float single2 = ef1.Height;
					ef1 = g.MeasureString(num6.ToString(), this._Font);
					float single3 = ef1.Width;
					g.DrawString(num6.ToString(), this._Font, Brushes.Black, single1 - (single3 / 2f), singl3 - (single2 / 2f));
					num1 += num5;
					num2 = 0;
				}
				else if (this.var8 > 0.15)
				{
					g.DrawLine(SystemPens.ControlText, single1, singl3 - 1f, single1, singl3 + 1f);
				}
			}
		}

 
		private void DrawLines2(Graphics g, float singl4 , float singl5 , float singl6 , float singl3 )//var54
		{
			int num1 = 1;
			int num2 = 0;
			int num3 = 4;
			int num4 = 8;
			int num5 = 1;
			if (singl6 < 0f)
			{
				num5 = -1;
				num1 = -1;
				num3 = -4;
				num4 = -8;
			}
			for (float single1 = singl4 + singl6; single1 <= singl5; single1 += singl6)
			{
				num2 += num5;
				if ((num2 == num3) && (this.var8 >= 0.15f))
				{
					g.DrawLine(SystemPens.ControlText, singl3 - 3f, single1, singl3 + 3f, single1);
				}
				else if (num2 == num4)
				{
					int num6 = Math.Abs(num1);
					SizeF ef1 = g.MeasureString(num6.ToString(), this._Font);
					float single2 = ef1.Height;
					ef1 = g.MeasureString(num6.ToString(), this._Font);
					float single3 = ef1.Width;
					g.DrawString(num6.ToString(), this._Font, Brushes.Black, singl3 - (single3 / 2f), single1 - (single2 / 2f));
					num1 += num5;
					num2 = 0;
				}
				else if (this.var8 > 0.15f)
				{
					g.DrawLine(SystemPens.ControlText, singl3 - 1f, single1, singl3 + 1f, single1);
				}
			}
		}
//		private void var61(object sender, MouseEventArgs e)
//		{
//			this.Print();
//		}

 
//		private void var62(object sender, MouseEventArgs e)
//		{
//			SetExport();
//		}


//		private void var63(object sender, MouseEventArgs e)
//		{
//			this._FindDlg.Show();
//		}

 
//		private void var64(object sender, MouseEventArgs e)
//		{
//			string text1;
//			if ((text1 = ((CtlInternal) sender).Name) != null)
//			{
//				text1 = string.IsInterned(text1);
//				if (text1 != "P1")
//				{
//					if (text1 == "P2")
//					{
//						if (this._pager != Pager.Two)
//						{
//							this._pager = Pager.Two;
//							this.var70();
//						}
//					}
//					else if (text1 == "P3")
//					{
//						if (this._pager != Pager.Three)
//						{
//							this._pager = Pager.Three;
//							this.var70();
//						}
//					}
//					else if (text1 == "P4")
//					{
//						if (this._pager != Pager.Four)
//						{
//							this._pager = Pager.Four;
//							this.var70();
//						}
//					}
//					else if ((text1 == "P6") && (this._pager != Pager.Six))
//					{
//						this._pager = Pager.Six;
//						this.var70();
//					}
//				}
//				else
//				{
//					if (this._pager == Pager.One)
//					{
//						return;
//					}
//					this._pager = Pager.One;
//					this.var69(1f);
//					this._ComboBox.Text = "100%";
//				}
//			}
//		}

		#endregion

		#region Methods

		internal void FindText()//cls1089
		{
			if ((this._FindInfo.CurrentPage == null) || (this._FindInfo.CurrentPage.PageIndex != this._CurrentPage))
			{
				this._FindInfo.CurrentPage = this._Document.Pages.GetPage(this._CurrentPage - 1);
			}
			if (this._Document.Find(this._FindInfo))
			{
				if (this._FindInfo.CurrentPage.PageIndex != this._CurrentPage)
				{
					this._CurrentPage = this._FindInfo.CurrentPage.PageIndex;
					this._TextBox.Text = this._CurrentPage + "/" + this._Document.MaxPages;
					this.var81();
				}
				this.ResettingRegions();
			}
			else
			{
				if (this._FindInfo.Step == 1)
				{
					this._FindDlg.lblErrorText.Text = "End of Page";
				}
				else
				{
					this._FindDlg.lblErrorText.Text = "Beginning of Page";
				}
				this._FindDlg.ErrorProvider1.SetError(this._FindDlg.lblErrorText, this._FindDlg.lblErrorText.Text);
			}
			this._internalPanel1.Invalidate();
		}

 
		internal void InvalidateRegions()//cls1090
		{
			for (int num1 = 0; num1 < 6; num1++)
			{
				if (this._RectF[num1].IsEmpty)
				{
					return;
				}
				RectangleF ef1 = this._RectF[num1];
				ef1.Inflate(1f, 1f);
				this._internalPanel1.Invalidate(new Region(ef1));
			}
		}

		public void Print()
		{
			if (!this.var16)
			{
				MessageBox.Show("Report Not Generated", "Nistec", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (this._Document.MaxPages > 0)
			{
				this._Document.Print(this.PrintDialog);
			}
			else
			{
				MessageBox.Show("No Pages to print", "Nistec", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void SetExport()
		{
			ExportDlg export1 = new ExportDlg();
			export1.ShowDialog();
            if (export1.CanView && (this._Document != null))
			{
				if (export1.ExportType == ExportType.Pdf)
				{
					PDFprop cls1 = export1.PDFprop;
					PDFDocument document1 = new PDFDocument();
					document1.Title = cls1.Title;
					document1.Compress = cls1.Compress;
					document1.EmbedFont = cls1.EmbedFont;
					if (cls1.Encrypt)
					{
						SecurityManager manager1 = new SecurityManager();
						manager1.OwnerPassword = cls1.OwnerPassword;
						manager1.UserPassword = cls1.UserPassword;
						manager1.Encryption = cls1.Encryption;
						document1.SecurityManager = manager1;
					}
					document1.Export(this._Document, export1.filePath);
				}
				else if (export1.ExportType == ExportType.Html)
				{
					Htmlprop cls2 = export1.Htmlprop;
					HTMLDocument document2 = new HTMLDocument();
					document2.Title = cls2.Title;
					document2.CharSet = cls2.CharSet;
					document2.Export(export1.filePath, this._Document, 1, (int) this._Document.MaxPages, cls2.IsMultiPage);
				}
				else if (export1.ExportType == ExportType.Image)
				{
					TIFprop cls3 = export1.TIFprop;
					ImageDocument document3 = new ImageDocument();
					FileStream stream1 = new FileStream(export1.filePath, FileMode.Create, FileAccess.ReadWrite);
					try
					{
						document3.Export(stream1, TIFprop.cls1083(cls3.Format), this._Document, 1, (int) this._Document.MaxPages);
					}
					finally
					{
						stream1.Flush();
						stream1.Close();
					}
				}
                else if (export1.ExportType == ExportType.Csv)
                {
                    CSVprop csvprop = export1.Csvprop;
                    Nistec.Printing.Csv.CsvWriter.Export((DataTable)this._Document.Report.DataSource, export1.filePath, csvprop.FirstRowHeader);
                    //DataView dv = Nistec.Data.DataSourceConvertor.GetDataView(this._Document.Report.DataSource, "");
                    //Nistec.Printing.ReportBuilder.PrintDataView(dv, "");
                    ////cls3.GetExport().ExportData(this._Document.Report.DataSource,"",cls3.Format,export1.filePath);
                    ////cls3.GetExport().ExportData(this._Document.Report.GetDataSet(),"",cls3.Format,export1.filePath);
                }
                else if (export1.ExportType == ExportType.Excel)
                {
                    CSVprop csvprop = export1.Csvprop;
                    Nistec.Printing.ExcelXml.Workbook.Export((DataTable)this._Document.Report.DataSource, export1.filePath, csvprop.FirstRowHeader, null);
                }
				if(export1.openAfter)
				{
					System.Diagnostics.Process.Start(export1.filePath);
                  //System.IO.FileStream fs= System.IO.File.Open(export1.filePath,System.IO.FileMode.Open);
					
				}
			}
		}

		private void SetDocument(Document doc)//var33
		{
			this.var16 = false;
			this._Document = doc;
			if (doc != null)
			{
				this._internalPanel1.SuspendLayout();
				this._Document.Graphics = base.CreateGraphics();
                this._PageWidth = this._Document.PageWidth *ReportUtil.Dpi;
                this._PageHeight = this._Document.PageHeight *ReportUtil.Dpi;
				this.var18 = this._Document.PrintBound;
				this._TextBox.Text = string.Format("{0}/{1}", this._CurrentPage, this._Document.MaxPages);
				this.var16 = true;
				this._internalPanel1.ResumeLayout(true);
				this.InvalidateRegions();
			}
			else
			{
				this.var2 = 12;
				this._CurrentPage = 1;
				this.var8 = 1f;
				this.var18 = new RectangleF(1f, 1f, 6.267f, 9.692f);
                this._PageWidth =  793.632f;
                this._PageHeight = 1122.432f;
			}
		}

 
		//		//SelectedIndexChanged
		//		private void var34(object sender, EventArgs e)
		//		{
		//			this._pager = Pager.None;
		//			if (this.var15 != this._ComboBox.SelectedIndex)
		//			{
		//				this.var15 = this._ComboBox.SelectedIndex;
		//				if (this._ComboBox.SelectedIndex < 8)
		//				{
		//					this.SetPrivewSize(this.var77((string) this._ComboBox.SelectedItem));
		//				}
		//				else
		//				{
		//					this.SetPrivewSize(1f);
		//				}
		//			}
		//		}

		private void SetZoom()
		{
			this._pager = Pager.None;
			if (this.var15 != this._MenuBar.DropDownButton)
			{
				this.var15 = this._MenuBar.DropDownButton;
				if (this._MenuBar.DropDownButton < 8)
				{
					this.SetPrivewSize(this.StrToFloat((string) this._MenuBar.DropDownSelected));
				}
				else
				{
					this.SetPrivewSize(1f);
				}
			}
		}

 
		//ComboKeyPress
		//		private void var35(object sender, KeyPressEventArgs e)
		//		{
		//			if (e.KeyChar == '\r')
		//			{
		//				float single1 = this.var77(this._ComboBox.Text);
		//				if (single1 == -1f)
		//				{
		//					e.Handled = true;
		//				}
		//				else
		//				{
		//					if (single1 < 0.1f)
		//					{
		//						single1 = 0.1f;
		//					}
		//					else if (single1 > 8f)
		//					{
		//						single1 = 8f;
		//					}
		//					float single2 = single1 * 100f;
		//					this._ComboBox.Text = single2.ToString() + '%';
		//					this._pager = Pager.None;
		//					this.SetPrivewSize(single1);
		//					e.Handled = true;
		//				}
		//			}
		//		}

		private void SetPageView(string page)
		{
			string text1=page;
			if (text1 != "P1")
			{
				if (text1 == "P2")
				{
					if (this._pager != Pager.Two)
					{
						this._pager = Pager.Two;
						this.Resetting();
					}
				}
				else if (text1 == "P3")
				{
					if (this._pager != Pager.Three)
					{
						this._pager = Pager.Three;
						this.Resetting();
					}
				}
				else if (text1 == "P4")
				{
					if (this._pager != Pager.Four)
					{
						this._pager = Pager.Four;
						this.Resetting();
					}
				}
				else if ((text1 == "P6") && (this._pager != Pager.Six))
				{
					this._pager = Pager.Six;
					this.Resetting();
				}
			}	
			else
			{
				if (this._pager == Pager.One)
				{
					return;
				}

					this._pager = Pager.One;
					this.SetPrivewSize(1f);
				//this._ComboBox.Text = "100%";
			}
		}

//		private void var65(object sender, MouseEventArgs e)
//		{
//			this._pager = Pager.None;
//			if (!(((this.var8 == 0.1f) | (this._ComboBox.SelectedIndex == 8)) | (this._ComboBox.SelectedIndex == 9)))
//			{
//				if (this.var8 <= 1f)
//				{
//					this.var8 -= 0.1f;
//				}
//				else if (this.var8 <= 8f)
//				{
//					this.var8 -= 0.5f;
//				}
//				if (this.var8 < 0.1f)
//				{
//					this.var8 = 0.1f;
//				}
//				double num1 = Math.Round((double) (this.var8 * 100f));
//				this._ComboBox.Text = num1.ToString() + '%';
//				if (this.var3 != (this._CurrentPage - 1))
//				{
//					this.var3 = this._CurrentPage - 1;
//				}
//				this.var69(this.var8);
//			}
//		}

		private void SetZoomIn()
		{
			this._pager = Pager.None;
			if (!(((this.var8 == 0.1f) | (this._MenuBar.DropDownButton == 8)) | (this._MenuBar.DropDownButton == 9)))
			{
				if (this.var8 <= 1f)
				{
					this.var8 -= 0.1f;
				}
				else if (this.var8 <= 8f)
				{
					this.var8 -= 0.5f;
				}
				if (this.var8 < 0.1f)
				{
					this.var8 = 0.1f;
				}
				double num1 = Math.Round((double) (this.var8 * 100f));
				//this._ComboBox.Text = num1.ToString() + '%';
				if (this.var3 != (this._CurrentPage - 1))
				{
					this.var3 = this._CurrentPage - 1;
				}
				this.SetPrivewSize(this.var8);
			}
		}
 
//		private void var66(object sender, MouseEventArgs e)
//		{
//			this._pager = Pager.None;
//			if (!(((this.var8 == 8f) | (this._ComboBox.SelectedIndex == 8)) | (this._ComboBox.SelectedIndex == 9)))
//			{
//				if (this.var8 < 1f)
//				{
//					this.var8 += 0.1f;
//				}
//				else if (this.var8 < 8f)
//				{
//					this.var8 += 0.5f;
//				}
//				if (this.var8 > 8f)
//				{
//					this.var8 = 8f;
//				}
//				double num1 = Math.Round((double) (this.var8 * 100f));
//				this._ComboBox.Text = num1.ToString() + '%';
//				if (this.var3 != (this._CurrentPage - 1))
//				{
//					this.var3 = this._CurrentPage - 1;
//				}
//				this.var69(this.var8);
//			}
//		}

		private void SetZoomOut()
		{
			this._pager = Pager.None;
			if (!(((this.var8 == 8f) | (this._MenuBar.DropDownButton == 9)) | (this._MenuBar.DropDownButton == 9)))
			{
				if (this.var8 < 1f)
				{
					this.var8 += 0.1f;
				}
				else if (this.var8 < 8f)
				{
					this.var8 += 0.5f;
				}
				if (this.var8 > 8f)
				{
					this.var8 = 8f;
				}
				double num1 = Math.Round((double) (this.var8 * 100f));
				//this._ComboBox.Text = num1.ToString() + '%';
				if (this.var3 != (this._CurrentPage - 1))
				{
					this.var3 = this._CurrentPage - 1;
				}
				this.SetPrivewSize(this.var8);
			}
		}


//		private void var67(object sender, MouseEventArgs e)
//		{
//			if ((this._CurrentPage + 1) <= this._Document.MaxPages)
//			{
//				this._CurrentPage++;
//				this._TextBox.Text = this._CurrentPage.ToString() + '/' + this._Document.MaxPages.ToString();
//				this.var81();
//			}
//		}
//
//		private void var68(object sender, MouseEventArgs e)
//		{
//			if ((this._CurrentPage - 1) > 0)
//			{
//				this._CurrentPage--;
//				this._TextBox.Text = this._CurrentPage.ToString() + '/' + this._Document.MaxPages.ToString();
//				this.var81();
//			}
//		}

		private void SetNextPage()
		{
			if ((this._CurrentPage + 1) <= this._Document.MaxPages)
			{
				this._CurrentPage++;
				this._TextBox.Text = this._CurrentPage.ToString() + '/' + this._Document.MaxPages.ToString();
				this.var81();
			}
		}

		private void SetPrevPage()
		{
			if ((this._CurrentPage - 1) > 0)
			{
				this._CurrentPage--;
				this._TextBox.Text = this._CurrentPage.ToString() + '/' + this._Document.MaxPages.ToString();
				this.var81();
			}
		}

		private void SetLastPage()
		{
			if (this._Document.MaxPages>0)
			{
				this._CurrentPage= (int)this._Document.MaxPages;
				this._TextBox.Text = this._CurrentPage.ToString() + '/' + this._Document.MaxPages.ToString();
				this.var81();
			}
		}

		private void SetFirstPage()
		{
			if (this._Document.MaxPages>0)
			{
				this._CurrentPage=1;
				this._TextBox.Text = this._CurrentPage.ToString() + '/' + this._Document.MaxPages.ToString();
				this.var81();
			}
		}

		private void SetPrivewSize(float var73)//var69
		{
			float single1 = this._internalPanel1.Width;
			float single2 = this._internalPanel1.Height;
			RectangleF ef1 = new RectangleF(10f, 10f, single1 - 20f, single2 - 20f);
			this.InvalidateRegions();
			if (((string) this._MenuBar.DropDownSelected)=="Page Width")//this._ComboBox.SelectedItem) == "Page Width")
			{
				this.var9.Width = ef1.Width;
                this.var9.Height = (this.var9.Width * this._PageHeight) / this._PageWidth;
				this.var9.X = ef1.Left;
				this.var9.Y = ef1.Top;
                this.var8 = this.var9.Width / this._PageWidth;
				this._VScrollBar.Enabled = true;
				this._HScrollBar.Enabled = false;
				this.var6 = 0f;
				this.SetVScrollBar();
			}
			else if (((string) this._MenuBar.DropDownSelected)=="Full Page")//._ComboBox.SelectedItem) == "Whole Page")
			{
				this.var9 = this.SetRectF(ef1, true);
                this.var8 = this.var9.Width / this._PageWidth;
				this._HScrollBar.Enabled = false;
				this._VScrollBar.Enabled = false;
				this.var6 = 0f;
				this.var7 = 0f;
			}
			else
			{
				this.var8 = var73;
                float single3 = this._PageWidth * this.var8;
                float single4 = this._PageHeight * this.var8;
				if (single1 >= (single3 + 10f))
				{
					this.var9 = new RectangleF((single1 - single3) / 2f, 10f, single3, single4);
				}
				else
				{
                    //this.var9 = new RectangleF(10f, 10f, single1, single2);
                    this.var9 = new RectangleF(10f, 10f, single3, single4);
				}
				if ((this.var9.Width + 20f) > single1)
				{
					this._HScrollBar.Enabled = true;
					this.SetHScrollBar();
				}
				else
				{
					this._HScrollBar.Enabled = false;
				}
				if ((this.var9.Height + 20f) > single2)
				{
					this._VScrollBar.Enabled = true;
					this.SetVScrollBar();
				}
				else
				{
					this._VScrollBar.Enabled = false;
				}
			}
			Array.Clear(this._RectF, 0, 5);
			this._RectF.SetValue(this.var9, 0);
			this.InvalidateRegions();
			this._internalPanel2.Invalidate();
			this._internalPanel3.Invalidate();
		}

		private void Resetting()//var70
		{
			RectangleF ef1;
			float single1 = this._internalPanel1.Width;
			float single2 = this._internalPanel1.Height;
			this.InvalidateRegions();
			Array.Clear(this._RectF, 0, 5);
			if (this._pager == Pager.Two)
			{
				ef1 = new RectangleF(10f, 10f, (single1 - 25f) / 2f, single2 - 20f);
				this.var9 = this.SetRectF(ef1, false);
                this.var8 = this.var9.Width / this._PageWidth;
				this._RectF.SetValue(this.var9, 0);
				this._RectF.SetValue(new RectangleF(this.var9.Right + 5f, this.var9.Top, this.var9.Width, this.var9.Height), 1);
				this.var3 = ((int) (Math.Ceiling(((double) this._CurrentPage) / 2) - 1)) * 2;
				this.var4 = this.var3 + 1;
			}
			else if (this._pager == Pager.Three)
			{
				ef1 = new RectangleF(10f, 10f, (single1 - 30f) / 3f, single2 - 20f);
				this.var9 = this.SetRectF(ef1, false);
                this.var8 = this.var9.Width / this._PageWidth;
				this.var9.X += (ef1.Width - this.var9.Width) / 2f;
				this._RectF.SetValue(this.var9, 0);
				this._RectF.SetValue(new RectangleF(this.var9.Right + 5f, this.var9.Top, this.var9.Width, this.var9.Height), 1);
				this._RectF.SetValue(new RectangleF(this._RectF[1].Right + 5f, this.var9.Top, this.var9.Width, this.var9.Height), 2);
				this.var3 = ((int) (Math.Ceiling(((double) this._CurrentPage) / 3) - 1)) * 3;
				this.var4 = this.var3 + 2;
			}
			else if (this._pager == Pager.Four)
			{
				ef1 = new RectangleF(10f, 10f, (single1 - 25f) / 2f, (single2 - 25f) / 2f);
				this.var9 = this.SetRectF(ef1, false);
                this.var8 = this.var9.Width / this._PageWidth;
				this._RectF.SetValue(this.var9, 0);
				this._RectF.SetValue(new RectangleF(this.var9.Right + 5f, this.var9.Top, this.var9.Width, this.var9.Height), 1);
				this._RectF.SetValue(new RectangleF(this.var9.Left, this.var9.Bottom + 5f, this.var9.Width, this.var9.Height), 2);
				this._RectF.SetValue(new RectangleF(this.var9.Right + 5f, this.var9.Bottom + 5f, this.var9.Width, this.var9.Height), 3);
				this.var3 = ((int) (Math.Ceiling(((double) this._CurrentPage) / 4) - 1)) * 4;
				this.var4 = this.var3 + 3;
			}
			else if (this._pager == Pager.Six)
			{
				ef1 = new RectangleF(10f, 10f, (single1 - 30f) / 3f, (single2 - 25f) / 2f);
				this.var9 = this.SetRectF(ef1, false);
                this.var8 = this.var9.Width / this._PageWidth;
				this.var9.X += (ef1.Width - this.var9.Width) / 2f;
				this._RectF.SetValue(this.var9, 0);
				this._RectF.SetValue(new RectangleF(this.var9.Right + 5f, this.var9.Top, this.var9.Width, this.var9.Height), 1);
				this._RectF.SetValue(new RectangleF(this._RectF[1].Right + 5f, this.var9.Top, this.var9.Width, this.var9.Height), 2);
				this._RectF.SetValue(new RectangleF(this.var9.Left, this.var9.Bottom + 5f, this.var9.Width, this.var9.Height), 3);
				this._RectF.SetValue(new RectangleF(this._RectF[1].Left, this._RectF[1].Bottom + 5f, this.var9.Width, this.var9.Height), 4);
				this._RectF.SetValue(new RectangleF(this._RectF[2].Left, this._RectF[2].Bottom + 5f, this.var9.Width, this.var9.Height), 5);
				this.var3 = ((int) (Math.Ceiling(((double) this._CurrentPage) / 6) - 1)) * 6;
				this.var4 = this.var3 + 5;
			}
			if (this.var4 > (this._Document.MaxPages - 1))
			{
				this.var4 = ((int) this._Document.MaxPages) - 1;
			}
			double num1 = Math.Round((double) (this.var8 * 100f));
			//this._ComboBox.Text = num1.ToString() + '%';
			if ((this._CurrentPage - 1) > this.var3)
			{
				this.var9 = this._RectF[(this._CurrentPage - this.var3) - 1];
			}
			this._HScrollBar.Enabled = false;
			this._VScrollBar.Enabled = false;
			this.var6 = 0f;
			this.var7 = 0f;
			this.InvalidateRegions();
			this._internalPanel2.Invalidate();
			this._internalPanel3.Invalidate();
		}

		private void DrawRectPage(Graphics g, int var72)
		{
			if ((this._FindInfo.CurrentPage != null) && (this._FindInfo.CurrentPage.PageIndex == var72))
			{
				using (Pen pen1 = new Pen(Color.Red))
				{
					RectangleF ef1 = this._FindInfo.PointerBound;
					g.DrawRectangle(pen1, ef1.X, ef1.Y, ef1.Width, ef1.Height);
				}
			}
		}

 
		private void SetVScrollBar()//var74
		{
			this._VScrollBar.Maximum = ((int) this.var9.Height) + 20;
			this._VScrollBar.SmallChange = this.var2;
			this._VScrollBar.LargeChange = this._internalPanel1.Height;
			this._VScrollBar.Value = 0;
			this.var7 = 0f;
		}

 
		private RectangleF SetRectF(RectangleF var79, bool var80)//var75
		{
			RectangleF ef1 = new RectangleF();
			ef1.Height = var79.Height;
            ef1.Width = (ef1.Height * this._PageWidth) / this._PageHeight;
			ef1.Y = var79.Top;
			if (var80)
			{
				ef1.X = var79.Left + ((var79.Width - ef1.Width) / 2f);
			}
			else
			{
				ef1.X = (var79.Left + var79.Width) - ef1.Width;
			}
			if (var79.Left >= ef1.X)
			{
				ef1.Width = var79.Width;
                ef1.Height = (ef1.Width * this._PageHeight) / this._PageWidth;
				ef1.X = var79.Left;
				ef1.Y = var79.Top;
			}
			return ef1;
		}

		private void SetHScrollBar()//var76
		{
			this._HScrollBar.Maximum = ((int) this.var9.Width) + 20;
			this._HScrollBar.SmallChange = this.var2;
			this._HScrollBar.LargeChange = this._internalPanel1.Width;
			this._HScrollBar.Value = 0;
			this.var6 = 0f;
		}

 
		private float StrToFloat(string var78)//var77
		{
			float single1;
			var78 = var78.Replace('%', ' ');
			try
			{
				double num1 = double.Parse(var78);
				num1 = Math.Round(num1) / 100;
				single1 = (float) num1;
			}
			catch (Exception exception1)
			{
				MessageBox.Show(exception1.Message, "Nistec", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				single1 = -1f;
			}
			return single1;
		}

 
		private void var81()
		{
			if ((this._pager == Pager.One) | (this._pager == Pager.None))
			{
				if (this.var3 != (this._CurrentPage - 1))
				{
					this.var3 = this._CurrentPage - 1;
					this.SetPrivewSize(this.var8);
				}
			}
			else if (((this._CurrentPage - 1) <= this.var4) && ((this._CurrentPage - 1) >= this.var3))
			{
				this.var9 = this._RectF[(this._CurrentPage - this.var3) - 1];
				this._internalPanel2.Invalidate();
				this._internalPanel3.Invalidate();
			}
			else
			{
				this.Resetting();
			}
		}

 
		private void var82(int offset)
		{
			if (this._TextBox.Text.Length > 0)
			{
				try
				{
					string[] textArray1 = Regex.Split(this._TextBox.Text, "/");
					if (PrintViewer.var83(textArray1[0]))
					{
						int num1 = int.Parse(textArray1[0]);
						num1 += offset;
						if ((num1 >= 1) & (num1 <= this._Document.MaxPages))
						{
							this._TextBox.Text = string.Format("{0}/{1}", num1, this._Document.MaxPages);
							this._CurrentPage = num1;
						}
						else if (num1 > this._Document.MaxPages)
						{
							this._TextBox.Text = string.Format("{0}/{1}", this._Document.MaxPages, this._Document.MaxPages);
							this._CurrentPage = (int) this._Document.MaxPages;
						}
						else if (num1 < 1)
						{
							this._TextBox.Text = string.Format("1/{0}", this._Document.MaxPages);
							this._CurrentPage = 1;
						}
						else
						{
							this._TextBox.Text = this._CurrentPage.ToString() + '/' + this._Document.MaxPages.ToString();
						}
					}
					else
					{
						this._TextBox.Text = this._CurrentPage.ToString() + '/' + this._Document.MaxPages.ToString();
					}
				}
				catch
				{
				}
			}
			else
			{
				this._TextBox.Text = this._CurrentPage.ToString() + '/' + this._Document.MaxPages.ToString();
			}
			this.var81();
		}

		private static bool var83(string var84)
		{
			string text1 = var84;
			for (int num1 = 0; num1 < text1.Length; num1++)
			{
				char ch1 = text1[num1];
				if (!char.IsNumber(ch1))
				{
					return false;
				}
			}
			return true;
		}

 
		private void ResettingRegions()//var85
		{
			float single1 = 0f;
			float single2 = 0f;
			bool flag1 = false;
			if (this._VScrollBar.Enabled)
			{
				float single3 = this.var9.Y + (this._FindInfo.PointerBound.Y * this.var8);
				float single4 = single3 + ((this._FindInfo.PointerBound.Height - 5f) * this.var8);
				if (this._internalPanel1.Height < single4)
				{
					single2 = ((float) Math.Ceiling((double) ((single4 - this._internalPanel1.Height) / ((float) this._VScrollBar.LargeChange)))) * this._VScrollBar.LargeChange;
					if ((this.var7 + single2) > (this._VScrollBar.Maximum - this._VScrollBar.LargeChange))
					{
						single2 = (this._VScrollBar.Maximum - this._VScrollBar.LargeChange) - this.var7;
					}
					this.var7 += single2;
					this._VScrollBar.Value = (int) this.var7;
					flag1 = true;
				}
				else if (single3 < 0f)
				{
					single2 = -((float) Math.Ceiling((double) (Math.Abs(single3) / ((float) this._VScrollBar.LargeChange)))) * this._VScrollBar.LargeChange;
					if ((this.var7 + single2) < 0f)
					{
						single2 = -this.var7;
					}
					this.var7 += single2;
					this._VScrollBar.Value = (int) this.var7;
					flag1 = true;
				}
			}
			if (this._HScrollBar.Enabled)
			{
				float single5 = this.var9.X + (this._FindInfo.PointerBound.X * this.var8);
				float single6 = single5 + ((this._FindInfo.PointerBound.Width - 5f) * this.var8);
				if (this._internalPanel1.Width < single6)
				{
					single1 = ((float) Math.Ceiling((double) ((single6 - this._internalPanel1.Width) / ((float) this._HScrollBar.LargeChange)))) * this._HScrollBar.LargeChange;
					if ((this.var6 + single1) > (this._HScrollBar.Maximum - this._HScrollBar.LargeChange))
					{
						single1 = (this._HScrollBar.Maximum - this._HScrollBar.LargeChange) - this.var6;
					}
					this.var6 += single1;
					this._HScrollBar.Value = (int) this.var6;
					flag1 = true;
				}
				else if (single5 < 0f)
				{
					single1 = -((float) Math.Ceiling((double) (Math.Abs(single5) / ((float) this._HScrollBar.LargeChange)))) * this._HScrollBar.LargeChange;
					if ((this.var6 + single1) < 0f)
					{
						single1 = -this.var6;
					}
					this.var6 += single1;
					this._HScrollBar.Value = (int) this.var6;
					flag1 = true;
				}
			}
			if (flag1)
			{
				this.InvalidateRegions();
				this.var9.Y -= single2;
				this._RectF[0].Y -= single2;
				this.var9.X -= single1;
				this._RectF[0].X -= single1;
				this.InvalidateRegions();
				this._internalPanel2.Invalidate();
				this._internalPanel3.Invalidate();
			}
		}

		#endregion

		#region Properties

		internal FindInfo findInfo
		{
			get
			{
				return this._FindInfo;
			}
		}

		public Document Document//var17
		{
			get
			{
				return this._Document;
			}
			set
			{
				this.SetDocument(value);
			}
		}

		#endregion

		#region Internal Panel
		// Nested Types cls414
		private class InternalPanel : Panel
		{

			internal InternalPanel()
			{
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.SetStyle(ControlStyles.UserPaint, true);
				base.SetStyle(ControlStyles.ResizeRedraw, true);
			}

		}
		#endregion

		#region Context Menu

        private ContextMenuStrip ctlContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem cmPrint;
        private System.Windows.Forms.ToolStripMenuItem cmZoomIn;
        private System.Windows.Forms.ToolStripMenuItem cmZoomOut;
        private System.Windows.Forms.ToolStripMenuItem cmExport;
        private System.Windows.Forms.ToolStripMenuItem cmFind;
        private System.Windows.Forms.ToolStripMenuItem cmFirst;
        private System.Windows.Forms.ToolStripMenuItem cmNext;
        private System.Windows.Forms.ToolStripMenuItem cmBack;
        private System.Windows.Forms.ToolStripMenuItem cmLast;
        private System.Windows.Forms.ToolStripMenuItem cmClose;
        private System.Windows.Forms.ToolStripMenuItem cm1;
        private System.Windows.Forms.ToolStripMenuItem cm2;
        private System.Windows.Forms.ToolStripMenuItem cm3;
		//private System.Windows.Forms.MenuItem cmAuto;


		private void SetContextMenu()
		{

			this.ctlContextMenu1 = new ContextMenuStrip();
			this.cmPrint = new System.Windows.Forms.ToolStripMenuItem();
			this.cmZoomIn = new System.Windows.Forms.ToolStripMenuItem();
			this.cmZoomOut = new System.Windows.Forms.ToolStripMenuItem();
			//this.cmAuto = new System.Windows.Forms.MenuItem();
			this.cmFirst = new System.Windows.Forms.ToolStripMenuItem();
			this.cmBack = new System.Windows.Forms.ToolStripMenuItem();
			this.cmNext = new System.Windows.Forms.ToolStripMenuItem();
			this.cmLast = new System.Windows.Forms.ToolStripMenuItem();
			this.cmClose = new System.Windows.Forms.ToolStripMenuItem();
			this.cmExport = new System.Windows.Forms.ToolStripMenuItem();
			this.cmFind = new System.Windows.Forms.ToolStripMenuItem();
			this.cm1 = new System.Windows.Forms.ToolStripMenuItem();
			this.cm2 = new System.Windows.Forms.ToolStripMenuItem();
			this.cm3 = new System.Windows.Forms.ToolStripMenuItem();

			// 
			// ctlContextMenu1
			// 
			this.ctlContextMenu1.ImageList = this.ImagesDesigner;
			this.ctlContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
											
												this.cmPrint,
												this.cmExport,
												this.cmFind,
												this.cm1,
												this.cmZoomIn,
												this.cmZoomOut,
												this.cm2,
												this.cmFirst,
												this.cmBack,
												this.cmNext,
												this.cmLast,
												this.cm3,
												this.cmClose});
			// 
			// cmPrint
			// 
            //this.ctlContextMenu1.SetDraw(this.cmPrint, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmPrint, 0);
			this.cmPrint.ImageIndex = 0;
			this.cmPrint.Text = "Print";
			this.cmPrint.ShortcutKeys= Keys.F2;
			this.cmPrint.Click += new System.EventHandler(this.cmPrint_Click);
			// 
			// cmExport
			// 
            //this.ctlContextMenu1.SetDraw(this.cmExport, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmExport, 1);
			this.cmExport.ImageIndex = 1;
			this.cmExport.Text = "Export";
            this.cmExport.ShortcutKeys = Keys.F9;
			this.cmExport.Click += new System.EventHandler(this.cmExport_Click);
			// 
			// cmFind
			// 
            //this.ctlContextMenu1.SetDraw(this.cmFind, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmFind, 2);
			this.cmFind.ImageIndex = 2;
			this.cmFind.Text = "Find";
            this.cmFind.ShortcutKeys = Keys.F7;
			this.cmFind.Click += new System.EventHandler(this.cmFind_Click);
			// 
			// cm1
			// 
            //this.ctlContextMenu1.SetDraw(this.cm1, true);
            //this.ctlContextMenu1.SetImageIndex(this.cm1, -1);
			this.cm1.ImageIndex = 3;
			this.cm1.Text = "-";
			// 
			// cmZoomIn
			// 
            //this.ctlContextMenu1.SetDraw(this.cmZoomIn, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmZoomIn, 8);//8
			this.cmZoomIn.ImageIndex = 4;
			this.cmZoomIn.Text = "ZoomIn";
            this.cmZoomIn.ShortcutKeys = Keys.F3;
			this.cmZoomIn.Click += new System.EventHandler(this.cmZoomIn_Click);
			// 
			// cmZoomOut
			// 
            //this.ctlContextMenu1.SetDraw(this.cmZoomOut, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmZoomOut, 9);//9
			this.cmZoomOut.ImageIndex = 5;
			this.cmZoomOut.Text = "ZoomOut";
            this.cmZoomOut.ShortcutKeys = Keys.F4;
			this.cmZoomOut.Click += new System.EventHandler(this.cmZoomOut_Click);
			// 
			// cm2
			// 
            //this.ctlContextMenu1.SetDraw(this.cm2, true);
            //this.ctlContextMenu1.SetImageIndex(this.cm2, -1);
			this.cm2.ImageIndex = 6;
			this.cm2.Text = "-";
			// 
			// cmFirst
			// 
            //this.ctlContextMenu1.SetDraw(this.cmFirst, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmFirst, 12);
			this.cmFirst.ImageIndex = 7;
			this.cmFirst.Text = "First";
			this.cmFirst.Click += new System.EventHandler(this.cmFirst_Click);
			// 
			// cmBack
			// 
            //this.ctlContextMenu1.SetDraw(this.cmBack, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmBack, 13);

            this.cmBack.ImageIndex = 8;
			this.cmBack.Text = "Back";
			this.cmBack.Click += new System.EventHandler(this.cmBack_Click);
			// 
			// cmNext
			// 
            //this.ctlContextMenu1.SetDraw(this.cmNext, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmNext, 14);
            this.cmNext.ImageIndex = 9;
			this.cmNext.Text = "Next";
			this.cmNext.Click += new System.EventHandler(this.cmNext_Click);
			// 
			// cmLast
			// 
            //this.ctlContextMenu1.SetDraw(this.cmLast, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmLast, 15);
            this.cmLast.ImageIndex = 10;
			this.cmLast.Text = "Last ";
			this.cmLast.Click += new System.EventHandler(this.cmLast_Click);
			// 
			// cm3
			// 
            //this.ctlContextMenu1.SetDraw(this.cm3, true);
            //this.ctlContextMenu1.SetImageIndex(this.cm3, -1);
            this.cm3.ImageIndex = 11;
			this.cm3.Text = "-";
			// 
			// cmClose
			// 
            //this.ctlContextMenu1.SetDraw(this.cmClose, true);
            //this.ctlContextMenu1.SetImageIndex(this.cmClose, 11);
            this.cmClose.ImageIndex = 12;
			this.cmClose.Text = "Close";
			this.cmClose.Click += new System.EventHandler(this.cmClose_Click);


			this.ContextMenuStrip = this.ctlContextMenu1;
		}

		#endregion

		#region ContextMenu Items

		private void cmPrint_Click(object sender, System.EventArgs e)
		{
			this.Print();
		}
		private void cmExport_Click(object sender, System.EventArgs e)
		{
			SetExport();
		}
		private void cmFind_Click(object sender, System.EventArgs e)
		{
			this._FindDlg.Show();
		}

		private void cmZoomIn_Click(object sender, System.EventArgs e)
		{
			SetZoomIn();
		}

		private void cmZoomOut_Click(object sender, System.EventArgs e)
		{
			SetZoomOut();
		}

//		private void cmAuto_Click(object sender, System.EventArgs e)
//		{
//			OnDropDownChange(e,8,"Auto"); 
//			this.cmAuto.Checked = true;
//			this.menuItem9.Checked = true;
//			this.CheckZoomMenu(this.menuItem9);
//			this.previewControl.AutoZoom = true;
//		}

		private void cmFirst_Click(object sender, System.EventArgs e)
		{
			SetFirstPage();
		}

		private void cmBack_Click(object sender, System.EventArgs e)
		{
			SetPrevPage();
		}

		private void cmNext_Click(object sender, System.EventArgs e)
		{
			SetNextPage();
		}

		private void cmLast_Click(object sender, System.EventArgs e)
		{
			SetLastPage();
		}

		private void cmClose_Click(object sender, System.EventArgs e)
		{
			this.FindForm().Close();
		}

		#endregion

		#region Key Events

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			//return base.ProcessCmdKey (ref msg, keyData);

			switch(keyData)
			{
				case Keys.PageDown:
					SetNextPage();
					break;
				case Keys.PageUp:
					SetPrevPage();
					break;
				case Keys.Home:
					SetFirstPage();
					break;
				case Keys.End:
					SetLastPage();
					break;
				case Keys.Escape:
					this.FindForm().Close();
					break;
				case Keys.F9://export
					SetExport();
					break;
				case Keys.F2://print
					this.Print();
					break;
				case Keys.F7://Find
					this._FindDlg.Show();
					break;
				case Keys.F3://zoomIn
					SetZoomIn();
					break;
				case Keys.F4://zommOut
					SetZoomOut();
					break;
//				case Keys.F6://filter
//					string val=Nistec.WinCtl.Dlg.InputBox.Open(this.FindForm(),"","","").ToString();
//	
//					//System.Data.DataTable dt= Nistec.Data.DataSource.GetDataTable(this.Document.Report.DataSource,"");
//                    //System.Data.DataTable dv=dt.Copy();
//					System.Data.DataView dv= Nistec.Data.DataSource.GetDataView(this.Document.Report.DataSource,"");
//	    			System.Data.DataTable dt=dv.Table.Copy();
//
//					//int i=dv.DefaultView.Count;
//		            dt.DefaultView.Sort="ProductID" ;
//					dt.DefaultView.RowFilter="ProductID='" + val + "'";
//					//i=dv.DefaultView.Count;
//					//System.Data.DataView dv= this.Document.Report.Filter("ProductID=" + val);
//					
////					Report rpt = new Report();
////                    rpt.DataSource=dv;
////					//rpt.Generate();
////					this.Document=null;
////					this.SetDocument(rpt.Document);
//
//					//this.Document.Report.DataSource = dv;  
//					Report rpt =new Report();
//					rpt=this.Document.Report;  
//					rpt.DataSource = dt.DefaultView;
//					rpt.Generate(); 
//                    		
//					
//					PrintViewer.Preview(rpt);
//					break;
			}
			return false;
		}


		#endregion


	}
 
}
