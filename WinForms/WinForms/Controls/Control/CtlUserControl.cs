using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using MControl.Util;
using MControl.Data;

namespace MControl.WinForms
{
	[System.ComponentModel.ToolboxItem(false)]
	public  class McUserControl : System.Windows.Forms.UserControl,ILayout,IBind
	{

		#region NetReflectedFram
		internal bool m_netFram=false;

		public void NetReflectedFram(string pk)
		{
			try
			{
				// this is done because this method can be called explicitly from code.
				System.Reflection.MethodBase method = (System.Reflection.MethodBase) (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
                m_netFram = MControl.Net.License.nf_1.nf_2(method, pk);
			}
			catch{}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
            //if(!DesignMode && !m_netFram)
            //{
            //    MControl.Util.Net.netWinMc.NetFram(this.Name, "Mc"); 
            //}
		}

		#endregion

		#region Members
		private System.ComponentModel.Container components = null;
		private bool			m_ReadOnly;
		private Image			m_Image;
		private BorderStyle	m_BorderStyle;
		private string		m_DefaultValue;
		private bool			m_FixSize;
		private   bool			autoChildrenStyle;

		protected const int minHeight=13;

		#endregion

		#region Constructors

		public McUserControl()
		{	
			autoChildrenStyle=true;
			InitializeComponent();
		}

		internal McUserControl(bool net)
		{	
			InitializeComponent();
			m_netFram=net;
		}

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
			// McUserControl
			// 
			this.Name = "McUserControl";
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

		protected IStyle			m_StylePainter;
  
		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Edit;}
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
			//
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

		#endregion

		#region Properties

		[Category("Style"),Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		[Category("Behavior"),DefaultValue("")]
		public virtual string DefaultValue
		{
			get {return m_DefaultValue;}
			set
			{
				if(m_DefaultValue != value)
				{
					m_DefaultValue = value;
					this.Invalidate(false);
					if(DesignMode)
						this.Refresh();
				}
			}
		}

		[Category("Behavior"),DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get {return m_ReadOnly;}
			set
			{
				if(m_ReadOnly != value)
				{
					m_ReadOnly = value;
					this.Invalidate(false);
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

		[Category("Appearance")]
		[DefaultValue(null),
		System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
		public Image Image
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

		[Category("Appearance"),DefaultValue(true)]
		public virtual bool FixSize
		{
			get	{ return m_FixSize; }
			set
			{
				m_FixSize = value;
				if(m_FixSize != value)
				{
					SetSize();
				    this.Invalidate ();
				}
			}
		}

		[Category("Appearance")]//,DefaultValue(BorderStyle.FixedSingle)]
		public new System.Windows.Forms.BorderStyle BorderStyle
		{
			get {return m_BorderStyle;}
			set 
			{
				if(m_BorderStyle != value)
				{
					m_BorderStyle = value;
					SetSize();
					this.Invalidate ();
				}
			}
		}


		#endregion

		#region IBind Members
		private BindingFormat bindFormat=BindingFormat.String;

		public BindingFormat BindFormat
		{
			get{return bindFormat;}
			set{bindFormat=value;}
		}

		public virtual string BindPropertyName()
		{
			return "Text";
		}
        public virtual void BindDefaultValue()
        {
            //if (this.DefaultValue.Length > 0)
            //{
            this.Text = this.DefaultValue;
            if (base.IsHandleCreated)
            {
                this.OnTextChanged(EventArgs.Empty);
            }
            //}
        }

  		#endregion

		#region Methods
		
		protected virtual void SetSize()
		{
			if(FixSize)
			{
				switch(m_BorderStyle)
				{
					case BorderStyle.Fixed3D :
						this.Height =(int)this.Font.GetHeight ()+8; 
						//ButtonPedding = 2;
						break;
					case BorderStyle.FixedSingle  :
						this.Height =(int)this.Font.GetHeight ()+8; 
						//ButtonPedding = 2;
						break;
					case BorderStyle.None :
						this.Height =(int)this.Font.GetHeight ()+1; 
						//ButtonPedding = 0;
						break;
				}
			}
			else
			{
				if(this.Height < minHeight)
					this.Height=minHeight;
			}
		}

		protected virtual bool AllowEdit()
		{
			return Enabled && ! m_ReadOnly; 
		}

		#endregion

		#region Conteiner Events handlers

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

		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved (e);
			e.Control.MouseEnter -= new System.EventHandler(this.ChildCtrlMouseEnter);
			e.Control.MouseLeave -= new System.EventHandler(this.ChildCtrlMouseLeave);
		}

		protected void ChildCtrlMouseLeave(object sender,System.EventArgs e)
		{
			DrawControl(false);
		}

		protected void ChildCtrlMouseEnter(object sender,System.EventArgs e)
		{
			DrawControl(true);
		}

		#endregion

		#region Overrides

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);
			DrawControl(e.Graphics);
		}

		protected override void OnMouseEnter(System.EventArgs e)
		{
			base.OnMouseEnter(e);
			DrawControl(true);
		}

		protected override void OnMouseLeave(System.EventArgs e)
		{
			base.OnMouseLeave(e);
			DrawControl(false);
		}

		protected override void OnGotFocus(System.EventArgs e)
		{
			base.OnGotFocus(e);
			//ResetError();
			DrawControl(false);
		}

		protected override void OnLostFocus(System.EventArgs e)
		{
			base.OnLostFocus(e);
			DrawControl(false);
		}

		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged (e);
			SetSize();
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged (e);
			SetSize();
		}

		protected override void OnEnabledChanged(System.EventArgs e)
		{
			base.OnEnabledChanged (e);
			DrawControl(false);
		}

		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.OnParentBackColorChanged (e);
			this.Invalidate ();
		}

		#endregion

		#region virtual

		protected virtual void DrawControl(bool hot)
		{
			using(Graphics g = this.CreateGraphics())
			{
				DrawControl(g,hot);
			}
		}

		protected virtual void DrawControl(Graphics g)
		{
			bool allowHot = (this.Enabled && !this.DesignMode) && !(this.IsMouseHover && Control.MouseButtons == MouseButtons.Left && !this.ContainsFocus);
			bool hot = this.IsMouseHover  && allowHot ;//(this.IsMouseHover || this.ContainsFocus) && allowHot;

			DrawControl(g,hot);
		}

		protected virtual void DrawControl(Graphics g,bool hot)
		{
			bool focused=this.ContainsFocus;
			DrawControl(g,hot,focused);
		}

		protected virtual void DrawControl(Graphics g,bool hot,bool focused)
		{
			if(m_BorderStyle==BorderStyle.FixedSingle)
			{
				Rectangle rect = new Rectangle(0, 0, this.Width-1, this.Height-1);
				this.LayoutManager.DrawBorder(g,rect,this.ReadOnly,this.Enabled,focused,hot);
			}
			else if(m_BorderStyle==BorderStyle.Fixed3D)
			{
				Rectangle rect = this.ClientRectangle;
				ControlPaint.DrawBorder3D(g,rect,Border3DStyle.Sunken);
			}

		}

		#endregion

	}
}
