using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;

using mControl.Drawing;

namespace mControl.WinCtl.Controls
{
	[ToolboxItem(false)]//,Designer(typeof(Design.CtlPageDesigner))]
	public class CtlPanelPage : CtlPanel
	{
		// Fields
		//private bool drawBorder;
		private Image image;
		protected int _imageIndex;
		protected ImageList _imageList;

		private bool pageVisible;// invisible;
		private ContextMenu titleContextMenu;

		public CtlPanelPage() 
		{
			base.BorderStyle=BorderStyle.FixedSingle;
			base.autoChildrenStyle=true;
			int num1;
			this.image = null;
			this._imageIndex=-1;
			//this.drawBorder = true;
//			this.pageVisible = true;
//			this.Text = text;
//			this.DockPadding.Right = num1 = 4;
//			this.DockPadding.Bottom = num1 = num1;
//			this.DockPadding.Left = num1 = num1;
//			this.DockPadding.Top = num1;
//			this.Visible = false;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
		}

 
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			//CtlColors.InitColors();
		}

//		[Browsable(false)]
//		public BorderStyle BorderStyle
//		{
//			get
//			{
//				return base.BorderStyle;
//			}
//			set
//			{
//				base.BorderStyle = value;
//			}
//		}

		[Browsable(false)]
		public new DockStyle Dock
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
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		[DefaultValue(null)]
		public ImageList ImageList
		{
			get { return _imageList; }
		
			set 
			{ 
				if (_imageList != value)
				{
					_imageList = value; 
				}
			}
		}


		[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		public int ImageIndex
		{
			get
			{

				if (((this._imageIndex != -1) && (_imageList != null)) && (this._imageIndex >= _imageList.Images.Count))
				{
					return (_imageList.Images.Count - 1);
				}
				return this._imageIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("InvalidLowBoundArgumentEx");
				}
				if (this._imageIndex != value)
				{
					if (value != -1 && _imageList!=null)
					{
						this.Image =_imageList.Images[value];// null;
					}
					this._imageIndex = value;
					base.Invalidate();
				}
			}
		}

		[Category("Appearance"), DefaultValue((string) null)]
		public virtual Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
				base.Invalidate();
				if (base.Parent != null)
				{
					base.Parent.Invalidate();
				}
			}
		}
		
		[Browsable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				base.Invalidate();
				if (base.Parent != null)
				{
					base.Parent.Invalidate();
				}
			}
		}
		[Category("Behavior")]
		public ContextMenu TitleContextMenu
		{
			get
			{
				return this.titleContextMenu;
			}
			set
			{
				this.titleContextMenu = value;
			}
		}
 
//		[Browsable(false)]
//		public new bool Visible
//		{
//			get
//			{
//				return base.Visible;
//			}
//			set
//			{
//				if ((base.Parent != null) && (this == ((CtlTabPages) base.Parent).SelectedTab))
//				{
//					base.Visible = true;
//					this.Dock = DockStyle.Fill;
//				}
//				else
//				{
//					base.Visible = value;
//				}
//			}
//		}

		

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged (e);
			this.Invalidate();
		}

		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged (e);
			this.Invalidate();
		}

//		[Category("Style"),Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//		public override bool AutoChildrenStyle
//		{
//			get{return base.AutoChildrenStyle;}
//			set{base.AutoChildrenStyle=value;}
//		}
//
//		[Category("Style"),Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//		public override GradientStyle GradientStyle
//		{
//			get{return base.GradientStyle;}
//			set{base.GradientStyle=value;}
//		}
//
//		[Category("Style"),Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//		public override ControlsLayout ControlLayout
//		{
//			get{return base.ControlLayout;}
//			set{base.ControlLayout=value;}
//		}
	}

}
