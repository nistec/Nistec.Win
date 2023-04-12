using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;



namespace Nistec.WinForms
{
	[ToolboxItem(false),Designer(typeof(Design.ProgressBarDesigner))]
	[ToolboxBitmap (typeof(McProgressXP),"Toolbox.ProgressXP.bmp")]
	public class McProgressXP : Nistec.WinForms.Controls.McContainer// System.Windows.Forms.Control,ILayout
	{
		#region Members
		private Timer m_timer;
		private float m_position;
		private float m_step;
		private bool m_start;

		private Color m_color1 = Color.White;
		private Color m_color2 = Color.Blue;

		#endregion

		#region Constructors

		public McProgressXP()
		{
			this.SetStyle(ControlStyles.DoubleBuffer,true);
			//m_color1 = Color.White;
			//m_color2 = Color.Blue;
			m_position = 0;
			m_step = 5;
			m_start=false;
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 
		}

		#endregion

		#region Overrides
		
		protected override void OnPaint(PaintEventArgs pe)
		{
			LinearGradientBrush b = new LinearGradientBrush(this.Bounds, m_color1, m_color2, 0, false);
			try
			{
				b.WrapMode = WrapMode.TileFlipX;
				b.TranslateTransform(Position, 0, MatrixOrder.Append);
				pe.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);      
			}
			finally
			{
				b.Dispose();
			}

			base.OnPaint(pe);
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			if (this.Visible) 
			{
				if (m_timer == null) 
				{
					m_timer = new System.Windows.Forms.Timer();
					m_timer.Interval = 20;
					m_timer.Tick += new EventHandler(OnTick);          
				}
                if(m_start)
					m_timer.Start();
			} 
			else 
			{
				if (m_timer != null) 
				{
					m_timer.Stop();
					m_start=false;
				}
			}

			base.OnVisibleChanged(e);
		}

		#endregion

		#region Virtuals

		protected virtual void OnTick(object sender, EventArgs args) 
		{    
			m_position += Step;
			if (m_position>this.Width) 
				m_position =- this.Width;

			this.Invalidate();
		}

		#endregion

		#region ILayout

//		[Browsable(false)]
//		public PainterTypes PainterType
//		{
//			get{return PainterTypes.Flat;}
//		}
//
//		protected IStyleGuide			m_StyleGuide;
//		protected IStyle			m_StylePainter;
//
//		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
//		public IStyle StylePainter
//		{
//			get {return m_StylePainter;}
//			set 
//			{
//				if(m_StylePainter!=value)
//				{
//					if (this.m_StylePainter != null)
//						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
//					m_StylePainter = value;
//					if (this.m_StylePainter != null)
//						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
//					OnStylePainterChanged(EventArgs.Empty);
//					this.Invalidate(true);
//				}
//			}
//		}
//
//		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
//		public IStyleGuide StyleGuide
//		{
//			get {return m_StyleGuide;}
//			set 
//			{
//				if(m_StyleGuide!=value)
//				{
//					if (this.m_StyleGuide != null)
//						this.m_StyleGuide.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
//					m_StyleGuide = value;
//					if (this.m_StyleGuide != null)
//						this.m_StyleGuide.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
//					OnStyleGuideChanged(EventArgs.Empty);
//					this.Invalidate(true);
//				}
//			}
//		}
//
//		[Browsable(false)]
//		public virtual IStyleLayout LayoutManager
//		{
//			get
//			{
//				if(this.m_StyleGuide!=null)
//					return this.m_StyleGuide.Layout as IStyleLayout;
//				else if(this.m_StylePainter!=null)
//					return this.m_StylePainter.Layout as IStyleLayout;
//				else
//					return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;
//			}
//		}
//
//		public virtual void SetStyleLayout(StyleLayout value)
//		{
//			if(this.m_StylePainter!=null)
//				this.m_StylePainter.Layout.SetStyleLayout(value); 
//		}
//
//		public virtual void SetStyleLayout(Styles value)
//		{
//			if(this.m_StylePainter!=null)
//				m_StylePainter.Layout.SetStyleLayout(value);
//		}

//		protected override void OnStyleGuideChanged(EventArgs e)
//		{
//			m_color2 = LayoutManager.Layout.CaptionColorInternal;
//			m_color1 = LayoutManager.Layout.LightLightColor;
//		}

		protected override void OnStylePainterChanged(EventArgs e)
		{
			m_color2 = LayoutManager.Layout.CaptionColorInternal;
			m_color1 = LayoutManager.Layout.LightLightColor;
		}

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			if((DesignMode || IsHandleCreated))
			{
				m_color2 = LayoutManager.Layout.CaptionColorInternal;
				m_color1 = LayoutManager.Layout.LightLightColor;
				this.Invalidate(true);
			}
		}

//		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
//		{
//
//			OnStylePropertyChanged(e);
//		}

		#endregion

		#region Properties

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoChildrenStyle
		{
			get{return base.AutoChildrenStyle;}
			set
			{
				base.AutoChildrenStyle=value;
			}
		}

//		[Category("Style")]
//        [DefaultValue(typeof(Color),"White")]
//		public Color StartColor
//		{
//			get{return m_color1;}
//			set{m_color1 = value;}
//		}
//
//		[Category("Style")]
//		[DefaultValue(typeof(Color),"Blue")]
//		public Color EndColor
//		{
//			get{return m_color2;}
//			set{m_color2 = value;}
//		}

		[Category("Behavior")]
		public float Position
		{
			get{return m_position;}
			set{m_position = value;}
		}

		[Category("Behavior")]
		public float Step
		{
			get{return m_step;}
			set{m_step = value;}
		}

		public bool Run
		{
			get{return m_start;}
			set
			{
				if(m_start != value && m_timer != null)
				{
					if(value && m_timer.Enabled )
					{
						m_timer.Start ();
						m_start = value;
					}
					else
						m_timer.Stop();
					Invalidate ();
				}
			}
		}

		#endregion

	}
}
