using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Collections;

using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;


using Nistec.Drawing;

using Nistec.Win32;

namespace Nistec.WinForms.Controls
{
	[System.ComponentModel.ToolboxItem(false)]//,Designer(typeof(ContainerDesigner))]
	public  class McContainer : McControl,ILayout
	{

		#region Members
		private System.ComponentModel.Container components = null;

		//protected System.Windows.Forms.BorderStyle	m_BorderStyle;
		protected Rectangle							bounds;
		protected int								m_ImageIndex;
		protected ImageList							m_ImageList;
		private   bool								autoChildrenStyle;
        //private ControlLayout m_ControlLayout;
        internal bool shouldSetAutoStyle;
		#endregion

		#region Constructors

		public McContainer()
		{		
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.Selectable, false);
			this.TabStop = false;

			autoChildrenStyle=true;
			m_ImageIndex=-1;
			m_ImageList=null;
            shouldSetAutoStyle = true;
			InitializeComponent();
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 
		}

//		protected override void OnHandleCreated(EventArgs e)
//		{
//			base.OnHandleCreated (e);
//			Nistec.Util.Net.netWinMc.NetFram(this.Name); 	
//
//		}

		#endregion

		#region Dispose

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
		private void InitializeComponent()
		{
			// 
			// McBase
			// 
			this.Name = "McContainer";
			//this.Size = new System.Drawing.Size(112, 16);
		}
		#endregion

		#region Paint background

//		protected override void OnPaint(PaintEventArgs e)
//		{
//			base.OnPaint (e);
//			if(this.m_BorderStyle==BorderStyle.FixedSingle)
//			{
//				Rectangle rect = new Rectangle(0, 0, this.Width-1, this.Height-1);
//				this.LayoutManager.DrawBorder(e.Graphics,rect,false,this.Enabled,this.Focused,false);
//			}
//			else if(this.m_BorderStyle==BorderStyle.Fixed3D)
//			{
//				ControlPaint.DrawBorder3D(e.Graphics,this.ClientRectangle,Border3DStyle.Sunken);
//			}
//		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground (pevent);
			PaintBackground(pevent,this.bounds);

		}

		internal void PaintButtonBackground(PaintEventArgs e, Rectangle bounds, Brush background)
		{
			if (background == null)
			{
				this.PaintBackground(e, bounds);
			}
			else
			{
				e.Graphics.FillRectangle(background, bounds);
			}
		}

		internal void PaintBackground(PaintEventArgs e, Rectangle rectangle)
		{
			if (this.RenderTransparent)
			{
				this.PaintTransparentBackground(e, rectangle);
			}
			if ((this.BackgroundImage != null) && !SystemInformation.HighContrast)
			{
				TextureBrush brush1 = new TextureBrush(this.BackgroundImage, WrapMode.Tile);
				try
				{
					Matrix matrix1 = brush1.Transform;
					matrix1.Translate((float) this.DisplayRectangle.X, (float) this.DisplayRectangle.Y);
					brush1.Transform = matrix1;
					e.Graphics.FillRectangle(brush1, rectangle);
					return;
				}
				finally
				{
					brush1.Dispose();
				}
			}
			Color color1 = this.BackColor;
			bool flag1 = false;
			if (((color1.A == 0xff)))// && (e.Graphics.GetHdc() != IntPtr.Zero))) //&& (this.BitsPerPixel > 8))
			{
				Win32.RECT rect1 = new Win32.RECT(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
				//Win32.WinAPI.FillRect (new HandleRef(e, e.HDC), ref rect1, new HandleRef(this, this.BackBrush));
				Win32.WinAPI.FillRect ((IntPtr)new HandleRef(e,this.Handle), ref rect1, (IntPtr)new HandleRef(this, this.BackBrush));
				flag1 = true;
			}
			if (!flag1 && (color1.A > 0))
			{
				if (color1.A == 0xff)
				{
					color1 = e.Graphics.GetNearestColor(color1);
				}
				using (Brush brush2 = new SolidBrush(color1))
				{
					e.Graphics.FillRectangle(brush2, rectangle);
				}
			}
		}

		internal void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle)
		{
			Graphics graphics1 = e.Graphics;
			Control control1 = this.Parent;
			if (control1 != null)
			{
				int num1;
				WinMethods.POINT point1 = new WinMethods.POINT();
				point1.y = num1 = 0;
				point1.x = num1;
				WinMethods.MapWindowPoints(new HandleRef(this, this.Handle), new HandleRef(control1, control1.Handle), point1, 1);
				rectangle.Offset(point1.x, point1.y);
				PaintEventArgs args1 = new PaintEventArgs(graphics1, rectangle);
				GraphicsState state1 = graphics1.Save();
				try
				{
					graphics1.TranslateTransform((float) -point1.x, (float) -point1.y);
					this.InvokePaintBackground(control1, args1);
					graphics1.Restore(state1);
					state1 = graphics1.Save();
					graphics1.TranslateTransform((float) -point1.x, (float) -point1.y);
					this.InvokePaint(control1, args1);
					return;
				}
				finally
				{
					graphics1.Restore(state1);
				}
			}
			graphics1.FillRectangle(SystemBrushes.Control, rectangle);
		}

		internal bool RenderTransparent
		{
			get
			{
				if (this.GetStyle(ControlStyles.SupportsTransparentBackColor))
				{
					return true;//(this.BackColor.A < 0xff);
				}
				return false;
			}
		}

		private IntPtr BackBrush
		{
			get
			{
				IntPtr ptr1;
				//				object obj1 = this.Properties.GetObject(Control.PropBackBrush);
				//				if (obj1 != null)
				//				{
				//					return (IntPtr) obj1;
				//				}
				Color color1 = this.BackColor;
				if ((this.Parent != null) && (this.Parent.BackColor == this.BackColor))
				{
					ptr1 = Win32.WinAPI.CreateSolidBrush(ColorTranslator.ToWin32(color1));
					//return this.Parent.BackBrush;
				}
				else if (ColorTranslator.ToOle(color1) < 0)
				{
					ptr1 = Win32.WinAPI.GetSysColorBrush(ColorTranslator.ToOle(color1) & 0xff);
					//this.SetState(0x200000, false);
				}
				else
				{
					ptr1 = Win32.WinAPI.CreateSolidBrush(ColorTranslator.ToWin32(color1));
					//this.SetState(0x200000, true);
				}
				//this.Properties.SetObject(Control.PropBackBrush, ptr1);
				return ptr1;
			}
		}
		#endregion

		#region Container Design
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			if( e.Control is ILayout)
			{
				if(this.autoChildrenStyle && this.StylePainter!=null)
				{
					if(this.StylePainter.StylePlan != (Styles.None))
					{
						((ILayout)e.Control).StylePainter=this.StylePainter;
						if(this.StylePainter.StylePlan == Styles.Custom)
							((ILayout)e.Control).SetStyleLayout(this.LayoutManager.Layout);
					}
				}
			}
		}

		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);
			if(this.autoChildrenStyle && this.StylePainter!=null)
			{
				((ILayout)e.Control).StylePainter=null;
			}

		}

		#endregion

		#region ILayout

        private ControlLayout m_ControlLayout = ControlLayout.Visual;
  
        [Category("Style"), DefaultValue(ControlLayout.Visual)]
        public virtual ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                m_ControlLayout = value;
            }
        }

		protected IStyle				m_StylePainter;

		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Flat;}
		}

		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
		public IStyle StylePainter
		{
			get {return m_StylePainter;}
			set 
			{
				if(m_StylePainter!=value)
				{
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					m_StylePainter = value;
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					OnStylePainterChanged(EventArgs.Empty);
					this.Invalidate(true);
				}
			}
		}

		[Browsable(false)]
		public virtual IStyleLayout LayoutManager
		{
			get
			{
				if(this.m_StylePainter!=null)
					return this.m_StylePainter.Layout as IStyleLayout;
				else
					return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;

			}
		}

		public virtual void SetStyleLayout(StyleLayout value)
		{
			if(this.m_StylePainter!=null)
				this.m_StylePainter.Layout.SetStyleLayout(value); 
		}

		public virtual void SetStyleLayout(Styles value)
		{
			if(this.m_StylePainter!=null)
				m_StylePainter.Layout.SetStyleLayout(value);
		}

		protected virtual void OnStylePainterChanged(EventArgs e)
		{
			if(autoChildrenStyle)
			{
				SetChildrenStyle(false);
			}
		}

		protected virtual void SetChildrenStyle(bool clear)
		{
            if (!shouldSetAutoStyle)
                return;

			foreach(Control c in this.Controls)
			{
				if( c is ILayout)
				{
					((ILayout)c).StylePainter=clear?null:this.StylePainter;
				}
			}
			this.Invalidate(true);
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			if((DesignMode || IsHandleCreated))
				this.Invalidate(true);
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnStylePropertyChanged(e);
		}

		#endregion

		#region Properties

        //[Category("Style")]
        //public virtual ControlLayout ControlLayout
        //{
        //    get { return m_ControlLayout; }
        //    set
        //    {
        //        m_ControlLayout = value;
        //        OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        //        this.Invalidate();
        //    }
        //}

		[Category("Style"),Browsable(true),DefaultValue(true),RefreshProperties( RefreshProperties.All)]//,EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool AutoChildrenStyle
		{
			get{return autoChildrenStyle;}
			set
			{
				if(autoChildrenStyle!=value)
				{
					autoChildrenStyle=value;
					SetChildrenStyle(!value);
				}
			}
		}

		[Browsable(false)]
		public bool IsMouseHover
		{
			get
			{
				try
				{
					Point mPos  = Control.MousePosition;
					bool retVal = this.ClientRectangle.Contains(this.PointToClient(mPos));
					return retVal;
				}
				catch{return false;}
			}
		}

//		[Category("Appearance")]//,DefaultValue(BorderStyle.FixedSingle)]
//		public virtual System.Windows.Forms.BorderStyle BorderStyle
//		{
//			get {return m_BorderStyle;}
//			set 
//			{
//				if(m_BorderStyle != value)
//				{
//					m_BorderStyle = value;
//					//SetSize ();
//					this.Invalidate ();
//				}
//			}
//		}

		[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				if (((this.m_ImageIndex != -1) && (this.m_ImageList != null)) && (this.m_ImageIndex >= this.m_ImageList.Images.Count))
				{
					return (this.m_ImageList.Images.Count - 1);
				}
				return this.m_ImageIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("InvalidLowBoundArgumentEx");
				}
				if (this.m_ImageIndex != value)
				{
					this.m_ImageIndex = value;
					base.Invalidate();
                    OnImageIndexChanged(EventArgs.Empty);
				}
			}
		}
		[Description("ButtonImageList"), DefaultValue((string) null), Category("Appearance")]
		public ImageList ImageList
		{
			get
			{
				return this.m_ImageList;
			}
			set
			{
				if (this.m_ImageList != value)
				{
					EventHandler handler1 = new EventHandler(this.ImageListRecreateHandle);
					EventHandler handler2 = new EventHandler(this.DetachImageList);
					if (this.m_ImageList != null)
					{
						this.m_ImageList.RecreateHandle -= handler1;
						this.m_ImageList.Disposed -= handler2;
					}
					this.m_ImageList = value;
					if (value != null)
					{
						value.RecreateHandle += handler1;
						value.Disposed += handler2;
					}
					base.Invalidate();
				}
			}
		}

		#endregion

		#region Methods

        protected virtual void OnImageIndexChanged(EventArgs e)
        {

        }

		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

 
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.Invalidate();
			}
		}

		#endregion

	}

}
