using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;

using Nistec.Win32;


namespace Nistec.WinForms
{
	[Designer( typeof(Design.PanelDesigner)),DefaultProperty("BorderStyle"), DefaultEvent("Paint")]
	[System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap (typeof(McPanel),"Toolbox.Panel.bmp")]
	public class McPanel : System.Windows.Forms.ScrollableControl,ILayout,IPanel
	{

		#region NetReflectedFram
        //internal bool m_netFram=false;

        //public void NetReflectedFram(string pk)
        //{
        //    try
        //    {
        //        // this is done because this method can be called explicitly from code.
        //        System.Reflection.MethodBase method = (System.Reflection.MethodBase) (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
        //        m_netFram = Nistec.Util.Net.nf_1.nf_2(method, pk);
        //    }
        //    catch{}
        //}

        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated (e);
        //    //if(!DesignMode && !m_netFram)
        //    //{
        //    //    Nistec.Util.Net.netWinMc.NetFram(this.Name,"Mc"); 
        //    //}
        //}

		#endregion

		#region Members

		private System.ComponentModel.Container components = null;
		internal System.Windows.Forms.BorderStyle m_BorderStyle;
		internal ControlLayout m_ControlLayout;
		//private GradientStyle gardientStyle=GradientStyle.TopToBottom;
		internal bool autoChildrenStyle=false;
        //internal bool allowGradient = true;
        internal bool drowCustom = false;
        internal bool drowBack = false;

        public event PaintEventHandler CustomDrow;

		#endregion

		#region Contructors
		
		public McPanel()
		{
			this.m_ControlLayout=ControlLayout.System;
			this.m_BorderStyle = BorderStyle.None;
            InitializeComponent();
		}
		public McPanel(ControlLayout ctlLayout)
		{
            this.drowBack = true;
			this.m_ControlLayout=ctlLayout;
			this.m_BorderStyle = BorderStyle.FixedSingle;
            InitializeComponent();
		}

        //internal McPanel(bool net):this()
        //{
        //    m_netFram=net;
        //}

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
            this.TabStop = false;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Selectable, false);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            
            components = new System.ComponentModel.Container();
			this.Name="McPanel";
//			m_Style=new StyleContainerDesigner (this);
//			m_Style.StylePlan=Styles.Custom;
//			m_Style.PropertyChanged+=new PropertyChangedEventHandler(m_Style_PropertyChanged);
		}
		#endregion

		#region override

		private bool OwnerDraw
		{
			get
			{
                return false;// (this.m_ControlLayout != ControlLayout.XpLayout && this.m_ControlLayout != ControlLayout.VistaLayout);
			}
		}

		private void WmEraseBkgnd(ref Message m)
		{
			Win32.RECT rect1 = new Win32.RECT();
			Win32.WinAPI.GetClientRect(base.Handle, ref rect1);
			Graphics graphics1 = Graphics.FromHdcInternal(m.WParam);
			Brush brush1 = new SolidBrush(this.BackColor);
			graphics1.FillRectangle(brush1, rect1.left, rect1.top, rect1.right - rect1.left, rect1.bottom - rect1.top);
			graphics1.Dispose();
			brush1.Dispose();
			m.Result = (IntPtr) 1;
		}

		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		protected override void WndProc(ref Message m)
		{
			if (this.OwnerDraw)
			{
				base.WndProc(ref m);
			}
			else
			{
				int num1 = m.Msg;
				if ((num1 == 20) || (num1 == 0x318))
				{
					this.WmEraseBkgnd(ref m);
				}
				else
				{
					base.WndProc(ref m);
				}
			}
		}

        protected virtual void OnCustomDrow(PaintEventArgs e)
        {
            if(CustomDrow!=null)
                CustomDrow(this,e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (drowCustom || CustomDrow != null)
            {
                OnCustomDrow(e);
            }
            else
            {
                DrawContainer(e.Graphics, ClientRectangle, StylePainter != null);
            }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //base.OnPaint(e);
        //    //WinAPI.ShowScrollBar(this.Handle, (int)ScrollBarTypes.SB_BOTH, 0);
        //    Rectangle rect = this.ClientRectangle;

        //    switch (m_ControlLayout)
        //    {
        //        case ControlLayout.Flat:
        //            DrawContainer(e.Graphics, rect, StylePainter != null);//true
        //            break;
        //        case ControlLayout.Visual:
        //        case ControlLayout.XpLayout:
        //        case ControlLayout.VistaLayout:
        //            if (allowGradient)
        //                DrawPanelXP(e.Graphics, rect);
        //            else
        //                DrawContainer(e.Graphics, rect, StylePainter != null);
        //            break;
        //        default:
        //            DrawContainer(e.Graphics, rect, StylePainter != null);//false
        //            break;
        //    }
        //    base.OnPaint(e);
        //}

        protected void DrawContainer(Graphics g, Rectangle bounds, bool fillBack)
        {
            Rectangle rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);

            //this.BackColor =LayoutManager.Layout.BackgroundColorInternal;//BackgroundColorInternal;

            if (fillBack || drowBack)
            {
                switch (ControlLayout)
                {
                    case ControlLayout.Visual:
                    case ControlLayout.XpLayout:
                    case ControlLayout.VistaLayout:
                        using (Brush sb = LayoutManager.GetBrushGradient(rect, 270f))
                        {
                            g.FillRectangle(sb, rect);
                        }
                        break;
                    default:
                        using (Brush b = LayoutManager.GetBrushFlat())
                        {
                            g.FillRectangle(b, rect);
                        }
                        break;
                }
            }

            if (m_BorderStyle == BorderStyle.FixedSingle)
            {
                using (Pen pen = LayoutManager.GetPenBorder())
                {
                    g.DrawRectangle(pen, rect);
                }
            }
            else if (m_BorderStyle == BorderStyle.Fixed3D)
            {
                ControlPaint.DrawBorder3D(g, bounds, System.Windows.Forms.Border3DStyle.Sunken);
            }
        }

        //private void DrawPanelXP(Graphics g,Rectangle bounds)
        //{
        //    float gradiaentAngle=(float)this.gardientStyle;
        //    Rectangle 	rect=new Rectangle (bounds.X ,bounds.Y,bounds.Width-1 ,bounds.Height-1);

        //    if(m_ControlLayout==ControlLayout.Visual )
        //    {
        //        using(Brush sb=LayoutManager.GetBrushGradient(rect,gradiaentAngle))
        //        {
        //            g.FillRectangle (sb,rect);
        //        }
				
        //        if(m_BorderStyle ==BorderStyle.FixedSingle)
        //        {
        //            using(Pen pen=LayoutManager.GetPenBorder())
        //            {
        //                g.DrawRectangle (pen,rect);
        //            }
        //        }
        //        else if(m_BorderStyle==BorderStyle.Fixed3D)
        //            ControlPaint.DrawBorder3D (g,rect,System.Windows.Forms.Border3DStyle.Sunken);
        //    }

        //    else if (m_ControlLayout == ControlLayout.XpLayout )
        //    {
        //        using (SolidBrush sb=new SolidBrush (this.Parent.BackColor))
        //        {
        //            g.FillRectangle(sb,bounds);
        //        }

        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //        System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, 4);

        //        using(Brush sb=LayoutManager.GetBrushGradient(rect,gradiaentAngle))
        //        {
        //            g.FillPath  (sb,path);
        //        }
		
        //        if(m_BorderStyle==BorderStyle.FixedSingle)
        //        {
        //            using(Pen pen=LayoutManager.GetPenBorder())
        //            {
        //                g.DrawPath (pen,path);
        //            }
        //        }
        //        else if(m_BorderStyle==BorderStyle.Fixed3D)
        //            ControlPaint.DrawBorder3D (g,bounds,System.Windows.Forms.Border3DStyle.Sunken);
        //    }
        //    else if (m_ControlLayout == ControlLayout.VistaLayout)
        //    {
        //        using (SolidBrush sb = new SolidBrush(this.Parent.BackColor))
        //        {
        //            g.FillRectangle(sb, bounds);
        //        }

        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //        System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, 4);

        //        using (Brush sb = LayoutManager.GetBrushVistaGradient(rect, gradiaentAngle))
        //        {
        //            g.FillPath(sb, path);
        //        }

        //        if (m_BorderStyle == BorderStyle.FixedSingle)
        //        {
        //            using (Pen pen = LayoutManager.GetPenBorder())
        //            {
        //                g.DrawPath(pen, path);
        //            }
        //        }
        //        else if (m_BorderStyle == BorderStyle.Fixed3D)
        //            ControlPaint.DrawBorder3D(g, bounds, System.Windows.Forms.Border3DStyle.Sunken);
        //    }
        //}

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeFont(Font value, bool force)
        {
            if (ShouldSerializeForeColor())
                this.Font = LayoutManager.Layout.TextFontInternal;
            else if (force)
                this.Font = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
            else if (force)
                base.ForeColor = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            switch (m_ControlLayout)
            {
                case ControlLayout.Visual:
                case ControlLayout.XpLayout:
                case ControlLayout.VistaLayout:
                    base.BackColor = LayoutManager.Layout.ColorBrush1Internal;
                    break;
                default:
                    if (IsHandleCreated && StylePainter != null)
                        base.BackColor = LayoutManager.Layout.BackgroundColorInternal;
                    else if(force)
                        base.BackColor = value;
                    break;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            if (!IsHandleCreated)
                return false;
            switch (m_ControlLayout)
            {
                case ControlLayout.Visual:
                case ControlLayout.XpLayout:
                    return true; 
                default:
                    return StylePainter != null;

            }
        }

       [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }


 

//		[UseApiElements("ShowScrollBar")]
//		protected override void OnPaint(PaintEventArgs e)
//		{
//			base.OnPaint(e);
//			WinAPI.ShowScrollBar(this.Handle, (int)ScrollBarTypes.SB_BOTH, 0);
//			Rectangle rect =this.ClientRectangle;
//			m_Style.DrawContainerColor(e.Graphics,rect,m_BorderStyle);
//		}

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
            if (this.autoChildrenStyle && this.StylePainter != null)
            {
                ((ILayout)e.Control).StylePainter = null;
            }

        }

		#endregion

		#region Properties

		[Category("Style"),Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool AutoChildrenStyle
		{
			get{return this.autoChildrenStyle;}
			set
			{
				if(autoChildrenStyle!=value)
				{
					autoChildrenStyle=value;
					SetChildrenStyle(!value);
				}
			}
		}

        //[Category("Style"),DefaultValue(GradientStyle.TopToBottom), Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public virtual GradientStyle GradientStyle
        //{
        //    get{return this.gardientStyle;}
        //    set
        //    {
        //        if(gardientStyle!=value)
        //        {
        //            gardientStyle=value;
        //            this.Invalidate();
        //        }
        //    }
        //}

		[Category("Style"),DefaultValue(ControlLayout.System)]
		public virtual ControlLayout ControlLayout 
		{
			get {return m_ControlLayout;}
			set
			{
                if (m_ControlLayout != value)
                {
                    m_ControlLayout = value;
                    //if(m_ControlLayout==ControlLayout.System && IsHandleCreated)
                    //{
                    //  this.BackColor=this.Parent.BackColor;  
                    //}
                    OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
                    OnControlLayoutChanged(EventArgs.Empty);
                    this.Invalidate();
                }
			}

		}
//		protected override CreateParams CreateParams
//		{
//			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
//			get
//			{
//				//return param BorederStyle none
//				CreateParams params1 = base.CreateParams;
//				params1.ExStyle |= 0x10000;
//				params1.ExStyle &= -513;
//				params1.Style &= -8388609;
//
//				switch (this.m_BorderStyle)
//				{
//					case BorderStyle.FixedSingle:
//					{
//						params1.Style |= 0x800000;
//						return params1;
//					}
//					case BorderStyle.Fixed3D:
//					{
//						params1.ExStyle |= 0x200;
//						return params1;
//					}
//				}
//				return params1;
//			}
//		}
 
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}
 
		[DefaultValue(false)]
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
 
		[Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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


        [Browsable(true), DefaultValue(typeof(System.Drawing.Color), "Black")]
         public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                if (base.ForeColor != value)
                {
                    SerializeForeColor(value, true);
                }
            }
        }

        [DefaultValue(typeof(System.Drawing.Color), "Control")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (base.BackColor != value)
                {
                    SerializeBackColor(value,true);
                }
            }
        }


		[DefaultValue(0), Category("Appearance"), DispId(-504), Description("PanelBorderStyle")]
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
					if (!Enum.IsDefined(typeof(BorderStyle), value))
					{
						throw new InvalidEnumArgumentException("value", (int) value, typeof(BorderStyle));
					}
					if(value== BorderStyle.Fixed3D)
					{
						if(this.m_ControlLayout==ControlLayout.XpLayout )
						{
							return;
						}
					}
					this.m_BorderStyle = value;
					base.UpdateStyles();
					this.Invalidate();
				}
			}
		}

		#endregion

		#region ILayout

		protected IStyle			m_StylePainter;
 
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
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
			if(autoChildrenStyle)
			{
				SetChildrenStyle(false);
			}
		}

		protected virtual void SetChildrenStyle(bool clear)
		{
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
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ColorBrush1") || e.PropertyName.Equals("BackgroundColor"))
                    SerializeBackColor(Color.Empty,false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);

            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnStylePropertyChanged(e);
		}

        protected virtual void OnControlLayoutChanged(EventArgs e)
        {

        }
		#endregion

		#region Methods

//		public void SetGradiaentAngle(float value)
//		{
//			if (!(value >=0 && value <=360))
//			{
//				throw new ArgumentException("Value must be between 0 and 360");
//			}
//			gradiaentAngle=value;
//		}


		public override string ToString()
		{
            string text1 = base.ToString();
            return (text1 + ControlLayout.ToString());
        }

		#endregion

		#region Events
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public  new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}
		#endregion

	}
}
