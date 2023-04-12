using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using mControl.Util;
using mControl.WinCtl.Controls;
//using mControl.WinCtl.BaseCtl;

namespace mControl.GridStyle.Controls
{
	/// <summary>
	/// Summary description for GridCaption.
	/// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class GridCaption : mControl.WinCtl.Controls.CtlBase,ILabel,IStyleCtl
	{

		#region members
		private System.ComponentModel.IContainer components;
		private int xMargin;
		private int yMargin;
		private HorizontalAlignment _Alignment;
		internal Grid owner;
		static Image image;
		private Rectangle imageRect;
		private System.Windows.Forms.ImageList GridImageList;
		private bool drawImage;
		private bool isStyleChanged=false;
		#endregion

		#region Constructors

		static GridCaption()
		{
			GridCaption.image =NativeMethods.LoadImage("mControl.GridStyle.Images.collapsed.gif");
		}

		public GridCaption(Grid grid):this()
		{
          this.owner=grid;
		}

		public GridCaption()
		{
			base.NetReflectedFram("ba7fa38f0b671cbc");
			xMargin=0;
		    yMargin=0;
			InitializeComponent();
             
			SetStyle(ControlStyles.Selectable,false);
			SetStyle(ControlStyles.StandardDoubleClick,false);
			this.TabStop =false;
			//this.BorderStyle=BorderStyle.None;
			//this.FixSize=false;
			//this.Height=24;
		}

		#endregion

		#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				//					if(components != null)
				//					{
				//						components.Dispose();
				//					}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GridCaption));
			this.GridImageList = new System.Windows.Forms.ImageList(this.components);
			// 
			// GridImageList
			// 
			this.GridImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.GridImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GridImageList.ImageStream")));
			this.GridImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// GridCaption
			// 
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Size = new System.Drawing.Size(150, 20);

		}
		#endregion

		#region Overrides

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
			if(base.Width < base.DefaultSize.Width)
				base.Width=base.DefaultSize.Width;
		}

        
		protected override void OnParentChanged(EventArgs e)
		{
			if (Parent == null) return;
			//mPoint=GetPoints();
			base.OnParentChanged(e);
		}

		protected override void OnTextChanged(EventArgs e) 
		{
			//mPoint=GetPoints();
			base.OnTextChanged(e);
		}

		public override IStyleLayout CtlStyleLayout
		{
			get
			{
				if(this.owner!=null)
					return owner.CtlStyleLayout;
				else
				    return base.CtlStyleLayout;
			}
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			if(IsMouseHoverImage)
			{
				if(this.ContextMenu!=null)
                   this.ContextMenu.Show(this,imageRect.Location); 
			}
		}

		[Browsable(false)]
		private bool IsMouseHoverImage
		{
			get
			{
				try
				{
					if(!drawImage)
		              return false; 
					Point mPos  = Control.MousePosition;
					bool retVal = ImageRect.Contains(this.PointToClient(mPos));
					return retVal;
				}
				catch{return false;}
			}
		}

		[Browsable(false)]
		private Rectangle ImageRect
		{
			get
			{
				if(base.ImageAlign==ContentAlignment.MiddleLeft)
					imageRect=new Rectangle(1,(this.Height-GridCaption.image.Height)/2,GridCaption.image.Width,GridCaption.image.Height); 
				else
					imageRect=new Rectangle(this.Width-GridCaption.image.Width-2,(this.Height-GridCaption.image.Height)/2,GridCaption.image.Width,GridCaption.image.Height); 
				return imageRect;
			}
		}

		protected override void DrawControl(Graphics g)
		{
			Rectangle bounds = this.ClientRectangle;
			Rectangle rect=new Rectangle (bounds.X ,bounds.Y,bounds.Width-1 ,bounds.Height-1 );
				
			if(ControlLayout==ControlsLayout.Gradient )
			{
				using (Brush sb=CtlStyleLayout.GetBrushGradient(rect,90f,true))
				{
					g.FillRectangle (sb,rect);
				}
	
			}
			else if(ControlLayout==ControlsLayout.Flat)
			{
				using (Brush sb= CtlStyleLayout.GetBrushCaption())
				{
					g.FillRectangle (sb,rect);
				}
					
			}
			else
			{
				//this.BackColor = this.Parent.BackColor;
				ControlPaint.DrawButton (g,rect,ButtonState.Normal  ); 
			}

			if(m_BorderStyle==BorderStyle.FixedSingle) 
			{
				using (Pen pen=CtlStyleLayout.GetPenBorder())
				{
					g.DrawRectangle (pen,rect);
				}
			}
			else if(m_BorderStyle==BorderStyle.None) 
			{
				using(Pen pen=CtlStyleLayout.GetPenBorder())
				{
					g.DrawLine(pen,rect.X,rect.Bottom,rect.Right,rect.Bottom);
				}
			}

			bool flag1=false;
			if(this.Text.Length>0)
			{
				PaintText(g,rect);
				flag1=true;
			}
			//CtlStyleLayout.DrawString(g,rect,this.TextAlign,this.Text,this.Font);
 			drawImage= (GridCaption.image!=null && this.ContextMenu!=null);
			if(drawImage)
			{
				if(!flag1)
				{
					if (this.RightToLeft==RightToLeft.Yes)
						base.ImageAlign=ContentAlignment.MiddleLeft;
					else
						base.ImageAlign=ContentAlignment.MiddleRight;
				}
				CtlStyleLayout.DrawImage(g,rect,GridCaption.image,this.ImageAlign,true);
			}
		}

		private void PaintText(Graphics g, Rectangle textBounds)
		{
			Rectangle rectangle1 = textBounds;

			StringFormat format1 = new StringFormat();
			if (this.RightToLeft==RightToLeft.Yes)// alignToRight)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				base.ImageAlign=ContentAlignment.MiddleLeft;
			}
			else
				base.ImageAlign=ContentAlignment.MiddleRight;
	
			format1.Alignment = (this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far);
			format1.LineAlignment =StringAlignment.Center ;
			format1.FormatFlags |= StringFormatFlags.NoWrap;
			rectangle1.Offset(0, 2 * this.yMargin);
			rectangle1.Height -= 2 * this.yMargin;
			using (Brush foreBrush=CtlStyleLayout.GetBrushCaption())//.GetBrushFlat())
			{
				g.DrawString(this.Text, this.Font, foreBrush, (RectangleF) rectangle1, format1);
			}
			format1.Dispose();
		}

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged (e);
			isStyleChanged=true;
		}

		private void GridContextMenu_Popup(object sender, EventArgs e)
		{
			if(isStyleChanged)
			{
				//this.GridContextMenu.SetStyleLayout(this.CtlStyleLayout.Layout);
				this.GridContextMenu.CtlStyleLayout.StylePlan=this.CtlStyleLayout.StylePlan;
				isStyleChanged=false;
			}
		}
		#endregion

		#region Properties

		public int TopMargin
		{
			get{return yMargin;}
			set{yMargin=value;}
		}

		public int LeftMargin
		{
			get{return xMargin;}
			set{xMargin=value;}
		}

		public HorizontalAlignment Alignment
		{
			get{return _Alignment;}
			set{_Alignment=value;}
		}

		[Category("Appearance"),DefaultValue(BorderStyle.FixedSingle)]
		public override System.Windows.Forms.BorderStyle BorderStyle
		{
			get {return base.BorderStyle;}
			set 
			{
				if(base.BorderStyle != value)
				{
					base.BorderStyle = value;
					//SetSize ();

					if(value==BorderStyle.None)
					{
						this.BackColor=Color.Transparent;
					}
					else
					{
						this.BackColor = this.Parent.BackColor;
					}
					this.Invalidate ();
				}
			}
		}

		#endregion

		#region PopUpMenu

		internal mControl.WinCtl.Controls.CtlContextMenu GridContextMenu;
		private System.Windows.Forms.MenuItem mnFilter;
		private System.Windows.Forms.MenuItem mnRemoveFilter;
		private System.Windows.Forms.MenuItem mnColumns;
		private System.Windows.Forms.MenuItem mnFind;
		private System.Windows.Forms.MenuItem mnSepartor1;
		private System.Windows.Forms.MenuItem mnAdjust;
		private System.Windows.Forms.MenuItem mnStatusBar;
		private System.Windows.Forms.MenuItem mnSepartor2;
		private System.Windows.Forms.MenuItem mnSum;
		private System.Windows.Forms.MenuItem mnExport;
		private System.Windows.Forms.MenuItem mnPrint;
		//private System.Windows.Forms.ImageList GridImageList;

		private bool createMenu;

		internal void OnMenuHandel()
		{
			if(!createMenu)
				return;

			this.mnStatusBar.Checked=owner.IsStatusBarVisible;//.m_StatusBar.Visible;
		    
			bool isColumnOk=(owner.Columns.Count>0);// && this.DataList!=null);
	
			this.mnStatusBar.Enabled=isColumnOk;
			this.mnAdjust.Enabled=isColumnOk;
			this.mnSum.Enabled=isColumnOk;
			this.mnColumns.Enabled=isColumnOk;
	
		}

		private void InitMenu()
		{
			if(createMenu)
				return;

			//System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Grid));
			this.GridContextMenu = new mControl.WinCtl.Controls.CtlContextMenu();
			//this.GridImageList = new System.Windows.Forms.ImageList(this.components);
			this.mnFilter = new System.Windows.Forms.MenuItem();
			this.mnRemoveFilter = new System.Windows.Forms.MenuItem();
			this.mnColumns = new System.Windows.Forms.MenuItem();
			this.mnFind = new System.Windows.Forms.MenuItem();
			this.mnSepartor1 = new System.Windows.Forms.MenuItem();
			this.mnAdjust = new System.Windows.Forms.MenuItem();
			this.mnStatusBar = new System.Windows.Forms.MenuItem();
			this.mnSepartor2 = new System.Windows.Forms.MenuItem();
			this.mnSum = new System.Windows.Forms.MenuItem();
			this.mnExport = new System.Windows.Forms.MenuItem();
			this.mnPrint = new System.Windows.Forms.MenuItem();


			// 
			// GridContextMenu
			// 
			this.GridContextMenu.ImageList = this.GridImageList;
			this.GridContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] 
												{
													this.mnFilter,
													this.mnRemoveFilter,
													this.mnColumns,
													this.mnFind,
													this.mnSepartor1,
													this.mnAdjust,
													this.mnStatusBar,
													this.mnSepartor2,
													this.mnSum,
													this.mnExport,
													this.mnPrint});
			this.GridContextMenu.Popup+=new EventHandler(GridContextMenu_Popup);
			// 
			// GridImageList
			// 
//			this.GridImageList.ImageSize = new System.Drawing.Size(16, 16);
//			//this.GridImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GridImageList.ImageStream")));
//			this.GridImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// mnFilter
			// 
			this.GridContextMenu.SetDraw(this.mnFilter, true);
			this.GridContextMenu.SetImageIndex(this.mnFilter, 0);
			this.mnFilter.Index = 0;
			this.mnFilter.Text = "Filter";
			this.mnFilter.Click += new System.EventHandler(this.mnFilter_Click);
			// 
			// mnRemoveFilter
			// 
			this.GridContextMenu.SetDraw(this.mnRemoveFilter, true);
			this.GridContextMenu.SetImageIndex(this.mnRemoveFilter, 13);
			this.mnRemoveFilter.Index = 1;
			this.mnRemoveFilter.Text = "Remove Filter";
			this.mnRemoveFilter.Click += new System.EventHandler(this.mnRemoveFilter_Click);
			// 
			// mnColumns
			// 
			this.GridContextMenu.SetDraw(this.mnColumns, true);
			this.GridContextMenu.SetImageIndex(this.mnColumns, 12);
			this.mnColumns.Index = 2;
			this.mnColumns.Text = "Columns Filter";
			this.mnColumns.Click += new System.EventHandler(this.mnColumns_Click);
			// 
			// mnFind
			// 
			this.GridContextMenu.SetDraw(this.mnFind, true);
			this.GridContextMenu.SetImageIndex(this.mnFind, 1);
			this.mnFind.Index = 3;
			this.mnFind.Text = "Find";
			this.mnFind.Visible = false;
			this.mnFind.Click += new System.EventHandler(this.mnFind_Click);
			// 
			// mnSepartor1
			// 
			this.GridContextMenu.SetDraw(this.mnSepartor1, true);
			this.GridContextMenu.SetImageIndex(this.mnSepartor1, -1);
			this.mnSepartor1.Index = 4;
			this.mnSepartor1.Text = "-";
			// 
			// mnAdjust
			// 
			this.GridContextMenu.SetDraw(this.mnAdjust, true);
			this.GridContextMenu.SetImageIndex(this.mnAdjust, 3);
			this.mnAdjust.Index = 5;
			this.mnAdjust.Text = "Adjust";
			this.mnAdjust.Click += new System.EventHandler(this.mnAdjust_Click);
			// 
			// mnStatusBar
			// 
			this.GridContextMenu.SetDraw(this.mnStatusBar, true);
			this.GridContextMenu.SetImageIndex(this.mnStatusBar, -1);
			this.mnStatusBar.Index = 6;
			this.mnStatusBar.Text = "StatusBar";
			this.mnStatusBar.Click += new System.EventHandler(this.mnStatusBar_Click);
			// 
			// mnSepartor2
			// 
			this.GridContextMenu.SetDraw(this.mnSepartor2, true);
			this.GridContextMenu.SetImageIndex(this.mnSepartor2, -1);
			this.mnSepartor2.Index = 7;
			this.mnSepartor2.Text = "-";
			// 
			// mnSum
			// 
			this.GridContextMenu.SetDraw(this.mnSum, true);
			this.GridContextMenu.SetImageIndex(this.mnSum, 11);
			this.mnSum.Index = 8;
			this.mnSum.Text = "Sum";
			this.mnSum.Click += new System.EventHandler(this.mnSum_Click);
			// 
			// mnExport
			// 
			this.GridContextMenu.SetDraw(this.mnExport, true);
			this.GridContextMenu.SetImageIndex(this.mnExport, 9);
			this.mnExport.Index = 9;
			this.mnExport.Text = "Export";
			this.mnExport.Click += new System.EventHandler(this.mnExport_Click);
			// 
			// mnPrint
			// 
			this.GridContextMenu.SetDraw(this.mnPrint, true);
			this.GridContextMenu.SetImageIndex(this.mnPrint, 4);
			this.mnPrint.Index = 10;
			this.mnPrint.Text = "Print";
			this.mnPrint.Click += new System.EventHandler(this.mnPrint_Click);

			createMenu=true;
			OnMenuHandel();
		}

		#endregion

		#region CtlContextMenu

		//		[Category("Behavior")]//,DefaultValue(true)]
		//		public bool ShowCaptionMenu
		//		{
		//			get{return this.m_Caption.ShowPopUpMenu;} 
		//			set
		//			{
		//					this.m_Caption.ShowPopUpMenu=value; 
		//					this.Invalidate();
		//			}
		//		}

		private bool showPopUpMenu=false;
		
		[Category("Behavior"),DefaultValue(false)]//,RefreshProperties(RefreshProperties.All)]
		public bool ShowPopUpMenu
		{
			get{return this.showPopUpMenu;} 
			set
			{
				if(this.showPopUpMenu!=value)
				{
					if(value)
					{
						InitMenu();
						this.GridContextMenu.SetStyleLayout(this.CtlStyleLayout.StylePlan);
						this.GridContextMenu.StylePainter=this.StylePainter;
						//this.ContextMenu=this.GridContextMenu; 
						this.ContextMenu=this.GridContextMenu; 
					}
					else
					{
						this.ContextMenu=null; 
						//this.ContextMenu=null; 
					}
					//this.m_Caption.Invalidate();
					this.showPopUpMenu=value;
					this.Invalidate();
				}
			}
		}

		private void mnFilter_Click(object sender, System.EventArgs e)
		{
			owner.PerformFilter();
		}

		private void mnRemoveFilter_Click(object sender, System.EventArgs e)
		{
			owner.RemoveFilter();
		}

		private void mnFind_Click(object sender, System.EventArgs e)
		{

		}

		private void mnSum_Click(object sender, System.EventArgs e)
		{
			owner.PerformSum();
		}

		private void mnColumns_Click(object sender, System.EventArgs e)
		{
			owner.PerformColumnsFilter();
		}

		private void mnExport_Click(object sender, System.EventArgs e)
		{
			owner.PerformExport();
		}

		private void mnPrint_Click(object sender, System.EventArgs e)
		{
			owner.PerformPrint();
		}

		private void mnAdjust_Click(object sender, System.EventArgs e)
		{
			owner.PerformAdjustColumns();
		}
		private void mnStatusBar_Click(object sender, System.EventArgs e)
		{
			owner.PerformShowStatusBar();
			this.mnStatusBar.Checked=owner.StatusBar.Visible; 
		}

		#endregion

	}

}

