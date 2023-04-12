using System;
using System.Collections;
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

using mControl.Util;
using mControl.Collections;
using mControl.Drawing;
using mControl.WinCtl.Controls.Design;

namespace mControl.WinCtl.Controls
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	//[ToolboxBitmap(typeof(WizTabPanels),"Toolbox.WizPanels.bmp")]
	//[DefaultProperty("WizardType")]
	[Designer(typeof(Design.MultiPanelsDesigner))]
	public class CtlMultiPanels : mControl.WinCtl.Controls.CtlPanel
	{

		#region Members
		// Class wide constants
		protected const int _panelGap = 10;
		protected const int _buttonGap =4;// 10;
		
		// Instance fields
		private Image _image;
		private int _imageIndex;
		private ImageList _imageList;

		private GradientStyle gardientStyle;
		private ControlsLayout		m_ControlLayout;
  
	    
		// Instance designer fields
		protected mControl.WinCtl.Controls.CtlCaption _panelTop;
		protected mControl.WinCtl.Controls.CtlPanel _panelBottom;
	
		private mControl.WinCtl.Controls.CtlPanel panelBase;
		private mControl.WinCtl.Controls.CtlPanelPage panelTabs;
		private mControl.WinCtl.Controls.CtlSplitter splitter1;
		private mControl.WinCtl.Controls.CtlPanelPage panelList;

		private System.ComponentModel.IContainer components=null;

		#endregion
 
		#region Constructor

		public CtlMultiPanels()
		{
			InitializeComponent();

			m_ControlLayout=ControlsLayout.Gradient;

			gardientStyle=GradientStyle.BottomToTop;
			_imageIndex=-1;
			_imageList=null;
	
            
		}

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._panelBottom = new mControl.WinCtl.Controls.CtlPanel(true);
			this._panelTop = new mControl.WinCtl.Controls.CtlCaption();
			this.panelBase = new mControl.WinCtl.Controls.CtlPanel(true);
			this.panelTabs = new mControl.WinCtl.Controls.CtlPanelPage();
			this.splitter1 = new mControl.WinCtl.Controls.CtlSplitter();
			this.panelList = new mControl.WinCtl.Controls.CtlPanelPage();
			this.panelBase.SuspendLayout();
			this.panelTabs.SuspendLayout();
			this.panelList.SuspendLayout();
			this.SuspendLayout();
			// 
			// _panelBottom
			// 
			this._panelBottom.AutoChildrenStyle=true;
			this._panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._panelBottom.Dock = DockStyle.Bottom;
			this._panelBottom.Location = new System.Drawing.Point(2, 198);
			this._panelBottom.Name = "statusBar1";
			this._panelBottom.Size = new System.Drawing.Size(260, 48);
			this._panelBottom.TabIndex = 3;
			this._panelBottom.Text = "statusBar1";	
			// 
			// caption
			// 
			this._panelTop.AutoChildrenStyle=true;
			this._panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this._panelTop.Location = new System.Drawing.Point(2, 2);
			this._panelTop.Name = "caption";
			this._panelTop.Size = new System.Drawing.Size(260, 48);
			this._panelTop.TabIndex = 4;
			// 
			// panelBase
			// 
			this.panelBase.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.panelBase.Controls.Add(this.panelTabs);
			this.panelBase.Controls.Add(this.splitter1);
			this.panelBase.Controls.Add(this.panelList);
			this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBase.DockPadding.Top = 2;
			this.panelBase.DockPadding.Bottom = 2;
			this.panelBase.Location = new System.Drawing.Point(2, 50);
			this.panelBase.Name = "panelBase";
			this.panelBase.Size = new System.Drawing.Size(260, 148);
			this.panelBase.TabIndex = 5;
			// 
			// panelTabs
			// 
			this.panelTabs.AutoChildrenStyle=true;
			this.panelTabs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			//this.panelTabs.DockPadding.Bottom = 1;
			this.panelTabs.Location = new System.Drawing.Point(126, 2);
			this.panelTabs.Name = "panelTabs";
			this.panelTabs.Size = new System.Drawing.Size(132, 144);
			this.panelTabs.TabIndex = 2;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(122, 2);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(4, 144);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panelList
			// 
			this.panelList.AutoChildrenStyle=true;
			this.panelList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelList.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelList.DockPadding.All = 2;
			this.panelList.Location = new System.Drawing.Point(2, 2);
			this.panelList.Name = "panelList";
			this.panelList.Size = new System.Drawing.Size(120, 144);
			this.panelList.TabIndex = 0;
			// 
			// WizPanelTabs
			// 
			this.Controls.Add(this.panelBase);
			this.Controls.Add(this._panelTop);
			this.Controls.Add(this._panelBottom);
			this.Controls.SetChildIndex(this._panelTop,0);
			this.Controls.SetChildIndex(this._panelBottom,0);
			this.Controls.SetChildIndex(this.panelBase,0);
			this.DockPadding.All = 2;
			this.Name = "WizPanelTabs";
			this.Size = new System.Drawing.Size(264, 224);
			this.panelBase.ResumeLayout(false);
			this.panelTabs.ResumeLayout(false);
			this.panelList.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region internal

		#endregion

		#region Hide Properties
		
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new BorderStyle BorderStyle
		{
			get{return base.BorderStyle;}
			set{base.BorderStyle=value;}
		}

		#endregion

		#region Properties
		
		[Category("Style"),DefaultValue(GradientStyle.TopToBottom), Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GradientStyle GradientStyle
		{
			get{return this.gardientStyle;}
			set
			{
				if(gardientStyle!=value)
				{
					gardientStyle=value;
					this._panelTop.GradientStyle=value;
					this._panelBottom.GradientStyle=value;
					this.Invalidate();
				}
			}
		}

		[DefaultValue(ControlsLayout.Gradient)]    
		[Category("Style")]
		public virtual ControlsLayout ControlLayout 
		{
			get {return m_ControlLayout;}
			set
			{
				if(m_ControlLayout!=value)
				{
					m_ControlLayout=value;
					this._panelTop.ControlLayout=value;
					this._panelBottom.ControlLayout=value;
					this.Invalidate (true);
				}
			}

		}


		[DefaultValue(null)]
		public ImageList ImageList
		{
			get { return _imageList; }
		
			set 
			{ 
				_imageList = value; 
			}
		}

		[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				if (((this._imageIndex != -1) && (this._imageList != null)) && (this._imageIndex >= this._imageList.Images.Count))
				{
					return (this._imageList.Images.Count - 1);
				}
				return this._imageIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("InvalidLowBoundArgumentEx");
				}
				if (value != -1)
				{
					this._image = null;
				}
				this._imageIndex = value;
				base.Invalidate();
			}
		}

		[Category("Wizard")]
		[Description("Access to underlying header panel")]
		internal mControl.WinCtl.Controls.CtlPanel HeaderPanel
		{
			get { return _panelTop; }
		}

		[Category("Wizard")]
		[Description("Access to underlying trailer panel")]
		internal mControl.WinCtl.Controls.CtlPanel TrailerPanel
		{
			get { return _panelBottom; }
		}


	
		[Category("Wizard"),DefaultValue(true)]
		[Description("Show Main title Picture")]
		public bool ShowImage
		{
			get { return _panelTop.ShowImage; }
            
			set
			{
				_panelTop.ShowImage = value;
				_panelTop.Invalidate();
			}
		}

		[Category("Wizard"),DefaultValue(true)]
		[Description("Show Caption")]
		public bool ShowCaption
		{
			get { return _panelTop.Visible; }
            
			set
			{
				_panelTop.Visible = value;
				this.Invalidate();
			}
		}

		[Category("Wizard"),DefaultValue(true)]
		[Description("Show Bottom panel")]
		public bool ShowBottom
		{
			get { return _panelBottom.Visible; }
            
			set
			{
				_panelBottom.Visible = value;
				this.Invalidate();
			}
		}


		[Category("Wizard")]
		[Description("Main title Picture")]
		public Image CaptionImage
		{
			get { return _panelTop.Image;}
            
			set
			{
				_panelTop.Image=value;
				_panelTop.Invalidate();
			}
		}
    
		[Category("Wizard")]
		[Description("Font for drawing main title text")]
		public Font TitleFont
		{
			get 
			{ 
				
				return _panelTop.TitleFont; 
			}
		    
			set
			{
				if(value!=null)
				{
					_panelTop.TitleFont = value;
					_panelTop.Invalidate();
				}
			}
		}

		[Category("Wizard")]
		[Description("List width")]
		public int ListWidth
		{
			get { return this.panelList.Width;}
            
			set
			{
				this.panelList.Width=value;
			}
		}
		#endregion

		#region Override
        
		protected override void OnRightToLeftChanged(System.EventArgs e)
		{
			base.OnRightToLeftChanged (e);

			if(RightToLeft==RightToLeft.Yes)
			{
				this.panelList.Dock=DockStyle.Right;
				this.splitter1.Dock=DockStyle.Right;
				this.splitter1.Width=4;
		
			}
			else
			{
				this.panelList.Dock=DockStyle.Left;
				this.splitter1.Dock=DockStyle.Left;
				this.splitter1.Width=4;
			}
		}

   
		protected override void OnResize(EventArgs e)
		{
			this.PerformLayout();
		}

		protected void OnRepaintPanels(object sender, EventArgs e)
		{
			_panelTop.Invalidate();
			_panelBottom.Invalidate();
		}


		protected void OnPaintBottomPanel(object sender, PaintEventArgs pe)
		{
			using(Pen lightPen = CtlStyleLayout.GetPenBorder())
			{
				pe.Graphics.DrawRectangle(lightPen, 0, _panelBottom.Top, _panelBottom.Width-1, _panelBottom.Height - 1);
            
			}
		}

		#endregion

		#region IStyleCtl

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			if((DesignMode || IsHandleCreated))
			{
				this.Invalidate(true);
			}
		}

		protected override void SetChildrenStyle(bool clear)
		{
			base.SetChildrenStyle(clear);
			this.panelTabs.SetStyleLayout(this.CtlStyleLayout.Layout);
			this._panelBottom.SetStyleLayout(this.CtlStyleLayout.Layout);
			this._panelTop.SetStyleLayout(this.CtlStyleLayout.Layout);
			this.panelList.SetStyleLayout(this.CtlStyleLayout.Layout);

			this.Invalidate(true);
		}

		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged(e);
			this.panelTabs.StylePainter=this.m_StylePainter;
			this._panelTop.StylePainter=this.m_StylePainter;
			this._panelBottom.StylePainter=this.m_StylePainter;
			this.panelList.StylePainter=this.m_StylePainter;
			Invalidate();

		}

		#endregion

		private CtlPanel selectedPanel;
	
		public CtlPanel SelectedPanel
		{
			get{return selectedPanel;}
			set{selectedPanel=value;}
		}

		internal int ExternalMouseTest(IntPtr hWnd, Point mousePos)
		{
			if(ControlMouseTest(hWnd, mousePos, _panelTop))
				return 1;
			if(ControlMouseTest(hWnd, mousePos, _panelBottom))
				return 2;
			if(ControlMouseTest(hWnd, mousePos, panelTabs))
				return 3;
			if(ControlMouseTest(hWnd, mousePos, panelList))
				return 4;

			return -1;
			
		}

		protected virtual bool ControlMouseTest(IntPtr hWnd, Point mousePos, Control check)
		{
			// Is the mouse down for the left arrow window and is it valid to click?
			if ((hWnd == check.Handle) && check.Visible && check.Enabled)
			{
				// Check if the mouse click is over the left arrow
				if (check.ClientRectangle.Contains(mousePos))
				{
//					if (check == _leftArrow)
//						OnLeftArrow(null, EventArgs.Empty);
//	
//					if (check == _rightArrow)
//						OnRightArrow(null, EventArgs.Empty);

					return true;
				}
			}

			return false;
		}

		internal CtlPanel GetPanelAtPoint(Point p)
		{
			using (Graphics graphics1 = Graphics.FromHwnd(base.Handle))
			{
				Rectangle rectangle1 =(Rectangle) this._panelTop.ClientRectangle;
				if (rectangle1.Contains(p))
				{
					return this._panelTop;
				}
				rectangle1 =(Rectangle) this._panelBottom.ClientRectangle;
				if (rectangle1.Contains(p))
				{
					return this._panelBottom;
				}
				rectangle1 =(Rectangle) this.panelTabs.ClientRectangle;
				if (rectangle1.Contains(p))
				{
					return this.panelTabs;
				}
				rectangle1 =(Rectangle) this.panelList.ClientRectangle;
				if (rectangle1.Contains(p))
				{
					return this.panelList;
				}

			}
			return null;
		}

	}

}
