using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Collections;
using System.Drawing.Design;

using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;


using Nistec.Drawing;
using Nistec.Win32;

 
namespace Nistec.WinForms.Controls
{
	[System.ComponentModel.ToolboxItem(false)]
	public  class McBase : McControl, IMcBase
	{

		#region Members
        internal const int minHeight = 13;

		private System.ComponentModel.Container components = null;

		internal Image								m_Image;
        internal System.Windows.Forms.BorderStyle m_BorderStyle;
        internal System.Drawing.ContentAlignment m_TextAlign;
        internal System.Drawing.ContentAlignment m_ImageAlign;
        internal Rectangle bounds;
        internal int ButtonPedding;
        internal McState ctlState;

        private string toolTipText;
        private bool autoToolTip;
        /*toolTip*/
        private McToolTip toolTip;
        private bool showToolTip;


		private ControlLayout		m_ControlLayout;
		private int					m_ImageIndex;
		private ImageList			m_ImageList;

		//public const int DefHeight=20;
		//public const int MinHeight=15;

		private short prefHeightCache;
		private int requestedHeight;
		private bool integralHeightAdjust;
		//private bool wordWrap;
		//private bool hideSelection;
		private bool m_FixSize;

		[Description("OnFixedSizeChanged"), Category("PropertyChanged")]
		public event EventHandler FixedSizeChanged;

		#endregion

		#region Constructors

        internal McBase()
		{		
			m_BorderStyle =BorderStyle.FixedSingle;
			InitMcBase();
		}

        internal McBase(System.Windows.Forms.BorderStyle style)
		{		
			m_BorderStyle =style;
			InitMcBase();
		}		

		private void InitMcBase()//System.Windows.Forms.BorderStyle style)
		{
			SetStyle(ControlStyles.ResizeRedraw,true);
			SetStyle(ControlStyles.DoubleBuffer,true);
			SetStyle(ControlStyles.UserPaint,true);
			SetStyle(ControlStyles.AllPaintingInWmPaint,true);
			SetStyle(ControlStyles.SupportsTransparentBackColor ,true);
            
            this.toolTipText = null;
            this.autoToolTip = false;
            this.showToolTip = false;
     
			ctlState=McState.Default;
			ButtonPedding=2;
			m_FixSize=true;
			m_ImageIndex=-1;
			m_ImageList=null;
            m_ControlLayout = ControlLayout.Visual;
			InitializeComponent();
			m_TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
			m_ImageAlign=System.Drawing.ContentAlignment.MiddleCenter ;
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 

			this.prefHeightCache = -1;
			this.integralHeightAdjust = false;
			//this.wordWrap=true;
			//this.hideSelection=false;
			//base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.FixedHeight, m_FixSize);
			this.requestedHeight = this.DefaultSize.Height;
			this.requestedHeight = base.Height;

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
                if (this.m_ImageList != null)
                {
                    this.m_ImageList.Disposed -= new EventHandler(this.DetachImageList);
                }
                /*toolTip*/
                if (this.toolTip != null)
                {
                    this.toolTip.Dispose();
                    this.toolTip = null;
                }
                if (components != null)
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
			this.Name = "ControlBase";
			//this.Size = new System.Drawing.Size(112, 16);
		}
		#endregion

		#region Adsust

		private void AdjustHeight()
		{
			if ((this.Anchor & (AnchorStyles.Bottom | AnchorStyles.Top)) != (AnchorStyles.Bottom | AnchorStyles.Top))
			{
				this.prefHeightCache = -1;
				base.FontHeight = -1;
				int num1 = this.requestedHeight;
				try
				{
					if (m_FixSize)
					{
						base.Height = this.PreferredHeight;
					}
					else
					{
						int num2 = base.Height;
						//if (this.ctlFlags[TextBoxBase.multiline])
						//{
						base.Height = Math.Max(num1, this.PreferredHeight + 2);
						//}
						this.integralHeightAdjust = true;
						try
						{
							base.Height = num1;
						}
						finally
						{
							this.integralHeightAdjust = false;
						}
					}
				}
				finally
				{
					this.requestedHeight = num1;
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("PreferredHeight"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Category("Layout")]
		public virtual int PreferredHeight
		{
			get
			{
				if (this.prefHeightCache > -1)
				{
					return this.prefHeightCache;
				}
				int num1 = base.FontHeight;
				if (this.BorderStyle != BorderStyle.None)
				{
					num1 += (SystemInformation.BorderSize.Height * 4) + 3;
				}
				this.prefHeightCache = (short) num1;
				return num1;
			}
		}
 
		protected virtual void OnFixedSizeChanged(EventArgs e)
		{
			if(FixedSizeChanged!=null)
				this.FixedSizeChanged(this,e);
		}

		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (!this.integralHeightAdjust && (height != base.Height))
			{
				this.requestedHeight = height;
			}
			if (m_FixSize)
			{
				height = this.PreferredHeight;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		[Localizable(true), Category("Behavior"), Description("FixSize"), DefaultValue(true), RefreshProperties(RefreshProperties.Repaint)]
		public virtual bool FixSize
		{
			get
			{
				return m_FixSize;
			}
			set
			{
				if (m_FixSize != value)
				{
					m_FixSize = value;
					if (this.m_FixSize)//!this.Multiline)
					{
						base.SetStyle(ControlStyles.FixedHeight, value);
						this.AdjustHeight();
					}
					this.OnFixedSizeChanged(EventArgs.Empty);
				}
			}
		}
 
		[Category("Appearance"), Description("BorderDescr"), DefaultValue(2),System.Runtime.InteropServices.DispId(-504)]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return this.m_BorderStyle;
			}
			set
			{
				if (this.m_BorderStyle != value)
				{
					this.m_BorderStyle = value;
					this.prefHeightCache = -1;
                    this.AdjustHeight();
					base.UpdateStyles();
					base.RecreateHandle();
					this.OnBorderStyleChanged(EventArgs.Empty);
				}
			}
		}
 
		#endregion
        
		#region Conteiner Events handlers

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            /*toolTip*/
            if (showToolTip && (this.toolTip == null))
            {
                this.toolTip = new McToolTip();
            }
        }

 
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			if( e.Control is ILayout)// && Parent is ILayout)
			{
				((ILayout)e.Control).StylePainter=this.StylePainter;//((ILayout)Parent).StyleGuide; 
			}
			e.Control.MouseEnter += new System.EventHandler(this.ChildCtrlMouseEnter);
			e.Control.MouseLeave += new System.EventHandler(this.ChildCtrlMouseLeave);
		}

		protected void ChildCtrlMouseLeave(object sender,System.EventArgs e)
		{
			this.Invalidate();// DrawControl(false);
		}

		protected void ChildCtrlMouseEnter(object sender,System.EventArgs e)
		{
			this.Invalidate();//DrawControl(true);
		}

		#endregion

		#region Overrides

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);
			//DrawControl(e.Graphics);
		}

		protected override void OnMouseEnter(System.EventArgs e)
		{
			if(this.Enabled  && !this.ContainsFocus)
			{
				ctlState=McState.Hot;	
				this.Invalidate();// DrawControl(true);
			}
            /*toolTip*/
            if ((!base.DesignMode ) && (this.showToolTip && (this.toolTip != null)))
            {
                this.toolTip.Show(ToolTipText, this);
                //McToolTip.Instance.Show(ToolTipText,this);
            }
            base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(System.EventArgs e)
		{
			if(this.Enabled && !this.ContainsFocus )
			{
				ctlState=McState.Default;	
				this.Invalidate();//DrawControl(false);
			}
            /*toolTip*/
            if (this.toolTip != null)
            {
                this.toolTip.Hide(this);
            }
            //McToolTip.Instance.Hide(this);
            base.OnMouseLeave(e);
        }

		protected override void OnGotFocus(System.EventArgs e)
		{
			base.OnGotFocus(e);
			ctlState=McState.Focused;
			this.Invalidate();//DrawControl(false);
		}

		protected override void OnLostFocus(System.EventArgs e)
		{
			base.OnLostFocus(e);
			ctlState=McState.Default;
			this.Invalidate();//DrawControl(false);
		}


		protected override void OnSizeChanged(System.EventArgs e)
		{
			bounds = new Rectangle(0, 0, this.Width-1, this.Height-1);
			base.OnSizeChanged (e);
			//SetSize();
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged (e);
			this.AdjustHeight();//this.SetSize ();
		}

		protected override void OnEnabledChanged(System.EventArgs e)
		{
			base.OnEnabledChanged (e);
			this.Invalidate ();//DrawControl(false);
		}

		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.OnParentBackColorChanged (e);
			this.Invalidate ();
		}

		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			//base.OnParentBackColorChanged (e);
			this.Invalidate ();
		}

		#endregion

		#region Paint background

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

		#region virtual

//		protected virtual void DrawControl(bool hot)
//		{
//			using(Graphics g = this.CreateGraphics())
//			{
//				DrawControl(g,hot);
//			}
//		}
//
//		protected virtual void DrawControl(Graphics g)
//		{
//			bool allowHot = (this.Enabled && !this.DesignMode) && !(this.IsMouseHover && Control.MouseButtons == MouseButtons.Left && !this.ContainsFocus);
//			bool hot = this.IsMouseHover  && allowHot ;//(this.IsMouseHover || this.ContainsFocus) && allowHot;
//
//			DrawControl(g,hot);
//		}
//
//		protected virtual void DrawControl(Graphics g,bool hot)
//		{
//			bool focused=this.ContainsFocus;
//			DrawControl(g,hot,focused);
//		}
//		
//		protected virtual void DrawControl(Graphics g,bool hot,bool focused)
//		{
//			//
//		}

		protected virtual Image GetCurrentImage()
		{
			int indx = m_ImageIndex;

			if((m_ImageList != null) && (indx != -1))
			{
				if ( (indx < m_ImageList.Images.Count))
				{
					return m_ImageList.Images[indx];
				}
			}
			return this.Image;
		}

		#endregion

		#region ILayout

		protected IStyle	m_StylePainter;

		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Button;}
		}

		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
		public IStyle StylePainter
		{
			get {return m_StylePainter;}
			set 
			{
				if(m_StylePainter!=value)
				{
//					if(value!=null)
//					{
//                      if (!(value is IStyleButton))
//						  return;
//					}
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
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{

            //if(e.PropertyName.Equals("BackColor"))
            //    this.BackColor=LayoutManager.Layout.BackColorInternal;//this.OnBackColorChanged(EventArgs.Empty);
            //else if(e.PropertyName.Equals("ForeColor"))
            //    this.ForeColor=LayoutManager.Layout.ForeColorInternal;//this.OnForeColorChanged(EventArgs.Empty);

            //if((DesignMode || IsHandleCreated))
            //    this.Invalidate(true);
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnStylePropertyChanged(e);
		}

		#endregion

		#region Properties

        //[Category("Appearance")]
        //[Browsable(true),
        //DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        //Bindable(true)]
        [Category("Appearance"),Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), SettingsBindable(true)]

		public override string Text
		{
			get {return base.Text;}

			set
			{ 
				base.Text = value; 
				this.Invalidate();
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

		[Category("Appearance")]
		[DefaultValue(null)]
		//System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
		public virtual Image Image
		{
			get	{ return m_Image; }
			set
			{
				if(m_Image != value)
				{
					m_Image = value;
					this.Invalidate();
				}
			}
		}

//		[Category("Appearance"),DefaultValue(true)]
//		public virtual bool FixSize
//		{
//			get	{ return m_FixSize; }
//			set
//			{
//				m_FixSize = value;
//				if(m_FixSize != value)
//				{
//					SetSize();
//					this.Invalidate ();
//				}
//			}
//		}

//		[Category("Appearance"),DefaultValue(BorderStyle.FixedSingle)]
//		public virtual System.Windows.Forms.BorderStyle BorderStyle
//		{
//			get {return m_BorderStyle;}
//			set 
//			{
//				if(m_BorderStyle != value)
//				{
//					m_BorderStyle = value;
//					SetSize ();
//					this.Invalidate ();
//				}
//			}
//		}


        [Category("Behavior"),DefaultValue(false)]
        public virtual bool AutoToolTip
        {
            get { return autoToolTip; }
            set
            {
                autoToolTip=value;
            }
        }
 
        [Category("Behavior"), DefaultValue(false)]
        public virtual bool ShowToolTip
        {
            get { return showToolTip; }
            set
            {
                if (showToolTip != value)
                {
                    showToolTip = value;
                    /*toolTip*/
                    if (value && (this.toolTip == null))
                    {
                        this.toolTip = new McToolTip();
                    }
                    base.Invalidate();
                }
            }
        }
        [Category("Behavior"), DefaultValue(null), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Localizable(true), Description("ToolTipText")]
        public string ToolTipText
        {
            get
            {

                if (!this.AutoToolTip || !string.IsNullOrEmpty(this.toolTipText))
                {
                    return this.toolTipText;
                }
                string text = this.Text;
                if (McToolTip.ContainsMnemonic(text))
                {
                    text = string.Join("", text.Split(new char[] { '&' }));
                }
                return text;
            }
            set
            {
                this.toolTipText = value;
            }
        }

		[Category("Appearance")]
		[DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
		public virtual System.Drawing.ContentAlignment TextAlign
		{
			get{ return m_TextAlign; }

			set
			{
				if(m_TextAlign != value)
				{
					m_TextAlign = value;
					this.Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
		public virtual System.Drawing.ContentAlignment ImageAlign
		{
			get{ return m_ImageAlign; }

			set
			{
				if(m_ImageAlign != value)
				{
					m_ImageAlign = value;
					this.Invalidate();
				}
			}
		}

        [Category("Style"), DefaultValue(ControlLayout.Visual)]
		public virtual ControlLayout ControlLayout 
		{
			get {return m_ControlLayout;}
			set
			{
				m_ControlLayout=value;
                OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
				this.Invalidate ();
			}

		}

		[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		public virtual int ImageIndex
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
					if (value != -1)
					{
						this.m_Image = null;
					}
					this.m_ImageIndex = value;
					base.Invalidate();
				}
			}
		}
		[Description("ButtonImageList"), DefaultValue((string) null), Category("Appearance")]
		public virtual ImageList ImageList
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
					if (value != null)
					{
						this.m_Image = null;
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

		protected virtual void SetSize()
		{
			if(FixSize)
			{
				switch(m_BorderStyle)
				{
					case BorderStyle.Fixed3D :
						this.Height =(int)this.Font.GetHeight ()+8; 
						ButtonPedding = 2;
						break;
					case BorderStyle.FixedSingle  :
						this.Height =(int)this.Font.GetHeight ()+8; 
						ButtonPedding = 2;
						break;
					case BorderStyle.None :
						this.Height =(int)this.Font.GetHeight ()+1; 
						ButtonPedding = 0;
						break;
				}

			}
			else
			{
				if(this.Height < minHeight)
					this.Height=minHeight;

			}
		}


//		protected int GetHeight()
//		{
//			if(m_BorderStyle==BorderStyle.None )
//				return (int)this.Font.GetHeight ()+1; 
//			else
//				return (int)this.Font.GetHeight ()+8; 
//		 
//		}
		#endregion

	}

}
