using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Nistec.Drawing;
using Nistec.WinForms.Controls;


namespace Nistec.WinForms
{
	[Designer(typeof(Design.NavigatoreDesigner))]
	[ToolboxItem(true)]
	[ToolboxBitmap (typeof(McNavigatore),"Toolbox.Navigatore.bmp")]
	public class McNavigatore :McContainer
	{

		#region Members
		private System.ComponentModel.IContainer components = null;
	
		public enum Navigators
		{
			None=0,
			BtnLast=1,
			BtnNext=2,
			BtnPrev=3,
			BtnFirst=4,
			BtnAddNew=5,
			BtnDelete=6
		}

		private McButtonCombo btn1;
		private McButtonCombo btn2;
		private McButtonCombo btn3;
		private McButtonCombo btn4;
		private McButtonCombo btn5;
		private McButtonCombo btn6;

		private System.Windows.Forms.Timer countTimer;
		private bool					IsMouseDown;
		private Navigators				ActiveBtn;
		private int						TickCountr;
		private IntPtr m_ptr;

		private System.Windows.Forms.ToolTip toolTip;
		private bool m_EnableDelete;
		private bool m_EnableNew;
		private bool m_ViewDelete;
		private bool m_ViewNew;

		[Category("Action")]
		public event ButtonClickEventHandler ClickFirst;
		public event ButtonClickEventHandler ClickPrev;
		public event ButtonClickEventHandler ClickNext;
		public event ButtonClickEventHandler ClickLast;
		public event ButtonClickEventHandler ClickNew;
		public event ButtonClickEventHandler ClickDelete;


		#endregion

		#region Constructors

		public McNavigatore():base()
		{
			//base.ControlLayout=ControlLayout.System;
			m_ptr=IntPtr.Zero;
			IsMouseDown =false;
			ActiveBtn=Navigators.None;  
			m_EnableDelete=true;
			m_EnableNew=true;
			m_ViewDelete=true;
			m_ViewNew=true;
	
			InitializeComponent();
			SetStyle(ControlStyles.SupportsTransparentBackColor ,true);
			// this.BackColor =Color.Transparent; 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McNavigatore));
			this.countTimer = new System.Windows.Forms.Timer();
			this.btn1 = new McButtonCombo(this,"BtnLastPage.gif");
			this.btn2 = new McButtonCombo(this,"BtnNextPage.gif");
			this.btn3 = new McButtonCombo(this,"BtnPrevPage.gif");
			this.btn4 = new McButtonCombo(this,"BtnFirstPage.gif");
			this.btn5 = new McButtonCombo(this,"BtnAddNew.gif");
			this.btn6 = new McButtonCombo(this,"BtnDelete.gif");
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// countTimer
			// 
			this.countTimer.Enabled = false;
			this.countTimer.Tick   += new System.EventHandler(OnTimerTick);
			// 
			// btn1
			// 
			//this.btn1.Image = ((System.Drawing.Image)(resources.GetObject("btn1.Image")));
			this.btn1.Location = new System.Drawing.Point(62, 0);
			this.btn1.Name = "btn1";
			this.btn1.Size = new System.Drawing.Size(18, 18);
			this.btn1.TabIndex = 1;
			this.toolTip.SetToolTip(this.btn1, "Last");
			this.btn1.Click +=new EventHandler(this.btn1_ButtonClick);
			// 
			// btn2
			// 
			//this.btn2.Image = ((System.Drawing.Image)(resources.GetObject("btn2.Image")));
			this.btn2.Location = new System.Drawing.Point(42, 0);
			this.btn2.Name = "btn2";
			this.btn2.Size = new System.Drawing.Size(18, 18);
			this.btn2.TabIndex = 2;
			this.toolTip.SetToolTip(this.btn2, "Next");
			this.btn2.Click +=new EventHandler(this.btn2_ButtonClick);
			this.btn2.MouseDown +=new MouseEventHandler(btn2_MouseDown);
			this.btn2.MouseUp +=new MouseEventHandler(btn2_MouseUp);
			// 
			// btn3
			// 
			//this.btn3.Image = ((System.Drawing.Image)(resources.GetObject("btn3.Image")));
			this.btn3.Location = new System.Drawing.Point(22, 0);
			this.btn3.Name = "btn3";
			this.btn3.Size = new System.Drawing.Size(18, 18);
			this.btn3.TabIndex = 3;
			this.toolTip.SetToolTip(this.btn3, "Prev");
			this.btn3.Click +=new EventHandler(this.btn3_ButtonClick);
			this.btn3.MouseDown +=new MouseEventHandler(btn3_MouseDown);
			this.btn3.MouseUp +=new MouseEventHandler(btn3_MouseUp);
			// 
			// btn4
			// 
			//this.btn4.Image = ((System.Drawing.Image)(resources.GetObject("btn4.Image")));
			this.btn4.Location = new System.Drawing.Point(2, 0);
			this.btn4.Name = "btn4";
			this.btn4.Size = new System.Drawing.Size(18, 18);
			this.btn4.TabIndex = 4;
			this.toolTip.SetToolTip(this.btn4, "First");
			this.btn4.Click +=new EventHandler(this.btn4_ButtonClick);
			// 
			// btn5
			// 
			//this.btn5.Image = ((System.Drawing.Image)(resources.GetObject("btn5.Image")));
			this.btn5.Location = new System.Drawing.Point(82, 0);
			this.btn5.Name = "btn5";
			this.btn5.Size = new System.Drawing.Size(18, 18);
			this.btn5.TabIndex = 5;
			this.toolTip.SetToolTip(this.btn5, "New");
			this.btn5.Click +=new EventHandler(this.btn5_ButtonClick);
			// 
			// btn6
			// 
			//this.btn6.Image = ((System.Drawing.Image)(resources.GetObject("btn6.Image")));
			this.btn6.Location = new System.Drawing.Point(102, 0);
			this.btn6.Name = "btn6";
			this.btn6.Size = new System.Drawing.Size(18, 18);
			this.btn6.TabIndex = 6;
			this.toolTip.SetToolTip(this.btn6, "Delete");
			this.btn6.Click +=new EventHandler(this.btn6_ButtonClick);
			// 
			// McNavigatore
			// 
			this.Controls.Add(this.btn1);
			this.Controls.Add(this.btn2);
			this.Controls.Add(this.btn3);
			this.Controls.Add(this.btn4);
			this.Controls.Add(this.btn5);
			this.Controls.Add(this.btn6);
			this.EnabledChanged +=new EventHandler(panel1_EnabledChanged); 
			//this.BackColor = System.Drawing.SystemColors.Control;
			this.Name = "McNavigatore";
			this.Size = new System.Drawing.Size(122, 20);
			this.ResumeLayout(false);

		}
		#endregion

		#region TimerTick

	
		private void OnTimerTick(object sender, EventArgs e)
		{

			if (IsMouseDown)
			{
				IntPtr ptr= Nistec.Win32.WinAPI.GetActiveWindow();
				if(m_ptr!=ptr  || m_ptr == IntPtr.Zero)
				{
					IsMouseDown=false;
					TickCountr=0;
					return;
				}

				if(TickCountr >6)
				{
					switch(ActiveBtn )
					{
						case Navigators.BtnNext  :
							OnButtonNextClick (EventArgs.Empty ); 
							break;
						case Navigators.BtnPrev  :
							OnButtonPrevClick (EventArgs.Empty ); 
							break;
					}
				}
				TickCountr++;
				ptr=IntPtr.Zero; 
			}
			else
				ReleaseTimer();

		}

		private void SetTimer(MouseEventArgs e)
		{
			if(this.Enabled && e.Button == MouseButtons.Left)
			{
				m_ptr=IntPtr.Zero; 
				m_ptr= Nistec.Win32.WinAPI.GetActiveWindow();
				//this.Focus ();
				this.Invalidate(false);
				this.OnTimerTick(this, e);
				this.countTimer.Interval =100;
				this.countTimer.Enabled = true;
			}
		}

		private void ReleaseTimer()
		{
			m_ptr=IntPtr.Zero; 
			TickCountr=0;
			this.countTimer.Stop ();
			this.countTimer.Enabled = false;
			this.Invalidate(false);
		}

		private void Internal_MouseDown(Navigators btn,  MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

			//if(ctl.IsMouseHover && ctl.Enabled)
			//{
			ActiveBtn =btn;
			IsMouseDown  =true;
			SetTimer(e);		
			//} 
			//else
			//{
			//	ActiveBtn =Navigators.None ;
			//	m_MouseDown =false;
			//}
	
			base.OnMouseDown(e);

		}

		private  void Internal_MouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			//if(ctl.Enabled && ctl.IsMouseHover  && 
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				ActiveBtn =Navigators.None ;
				IsMouseDown  =false;
				ReleaseTimer();
			}
			base.OnMouseUp(e);
		}


		private void btn2_MouseDown(object sender, MouseEventArgs e)
		{
			if(btn2.Enabled && btn2.IsMouseHover )
				Internal_MouseDown(Navigators.BtnNext   ,e);
		}

		private void btn2_MouseUp(object sender, MouseEventArgs e)
		{
			Internal_MouseUp(e);
		}

		private void btn3_MouseDown(object sender, MouseEventArgs e)
		{
			if(btn3.Enabled && btn3.IsMouseHover) 
				Internal_MouseDown(Navigators.BtnPrev  ,e);
		}

		private void btn3_MouseUp(object sender, MouseEventArgs e)
		{
			Internal_MouseUp(e);
		}

		public void PreformNextPress()
		{
			if(btn2.Enabled)
				Internal_MouseDown(Navigators.BtnNext ,new System.Windows.Forms.MouseEventArgs (MouseButtons.Left ,1,1,1,1));
		}
		public void PreformNextRelease()
		{
			Internal_MouseUp(new System.Windows.Forms.MouseEventArgs (MouseButtons.Left ,1,1,1,1));
		}
		public void PreformPrevPress()
		{
			if(btn3.Enabled)
				Internal_MouseDown(Navigators.BtnPrev  ,new System.Windows.Forms.MouseEventArgs (MouseButtons.Left ,1,1,1,1));
		}
		public void PreformPrevRelease()
		{
			Internal_MouseUp(new System.Windows.Forms.MouseEventArgs (MouseButtons.Left ,1,1,1,1));
		}

		#endregion

		#region Events handlers

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			base.BackColor = Color.Transparent ; 
			SetToolTips();
		}

		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged(e);
			SetSize();
		}

		protected void SetSize()
		{
	
			//this.Height =DefHeight;
			if(this.Height < Defaults.minHeight)
				this.Height = Defaults.minHeight;
		
			int h=this.Height;

			this.btn1.Height   =h ;
			this.btn2.Height   =h ;
			this.btn3.Height   =h ;
			this.btn4.Height   =h ;
			this.btn5.Height   =h ;
			this.btn6.Height   =h ;

			SetLocation();
		}

		private void SetToolTips()
		{
			if(this.RightToLeft==RightToLeft.Yes)
			{
				this.toolTip.SetToolTip(this.btn1, "First");
				this.toolTip.SetToolTip(this.btn2, "Prev");
				this.toolTip.SetToolTip(this.btn3, "Next");
				this.toolTip.SetToolTip(this.btn4, "Last");
			}
			else
			{
				this.toolTip.SetToolTip(this.btn1, "Last");
				this.toolTip.SetToolTip(this.btn2, "Next");
				this.toolTip.SetToolTip(this.btn3, "Prev");
				this.toolTip.SetToolTip(this.btn4, "First");
			}
		}

		internal void SetLocation(RightToLeft value)
		{
			this.RightToLeft =value;
			SetLocation();
		}

		internal void SetLocation()
		{
			int lft=0;	
	
			if(RightToLeft ==RightToLeft.Yes )
			{
				lft=2;
				if(m_ViewDelete)//this.btn6.Visible)
				{
					this.btn6.Location = new System.Drawing.Point(lft, 0);
					lft+=20; 
				}
				if(m_ViewNew)// this.btn5.Visible)
				{
					this.btn5.Location = new System.Drawing.Point(lft, 0);
					lft+=20; 
				}
				this.btn4.Location = new System.Drawing.Point(lft, 0);
				this.btn3.Location = new System.Drawing.Point(lft+=20, 0);
				this.btn2.Location = new System.Drawing.Point(lft+=20, 0);
				this.btn1.Location = new System.Drawing.Point(lft+=20, 0);
				lft+=20;
			}
			else
			{
				this.btn4.Location = new System.Drawing.Point(2, 0);
				this.btn3.Location = new System.Drawing.Point(22, 0);
				this.btn2.Location = new System.Drawing.Point(42, 0);
				this.btn1.Location = new System.Drawing.Point(62, 0);
				lft=btn1.Left+20; 
				if(m_ViewNew)//this.btn5.Visible)
				{
					this.btn5.Location = new System.Drawing.Point(lft, 0);
					lft+=20; 
				}
				if(m_ViewDelete)//this.btn6.Visible)
				{
					this.btn6.Location = new System.Drawing.Point(lft, 0);
					lft+=20; 
				}

			}
			this.Width=lft;
			//base.RecreateHandle();
			//if(this.DesignMode)
			//	this.Refresh();

		}

		#endregion

		#region Overrides

	
		protected override void OnMouseUp(MouseEventArgs e)
		{
			IsMouseDown=false;
			ActiveBtn=Navigators.None;  
			base.OnMouseUp (e);
		}

		protected override void OnEnabledChanged(System.EventArgs e)
		{
			base.OnEnabledChanged(e);
			EnableButtons=this.Enabled;
		}

		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged (e);
			SetLocation();
			SetToolTips();
		}

		#endregion

		#region Event Click

		private void panel1_EnabledChanged(object sender, EventArgs e)
		{
			IsMouseDown  =false;
			OnEnabledChanged (e);   
		}

		private void btn1_ButtonClick(object sender, System.EventArgs e)
		{
			IsMouseDown  =false;
			if(this.RightToLeft==RightToLeft.Yes)
				OnButtonFirstClick(e); 
			else
				OnButtonLastClick(e);
		}

		private void btn2_ButtonClick(object sender, System.EventArgs e)
		{
			IsMouseDown  =false;
			if(this.RightToLeft==RightToLeft.Yes)
				OnButtonPrevClick(e); 
			else
				OnButtonNextClick(e);
		}

		private void btn3_ButtonClick(object sender, System.EventArgs e)
		{
			IsMouseDown  =false;
			if(this.RightToLeft==RightToLeft.Yes)
				OnButtonNextClick(e); 
			else
				OnButtonPrevClick(e);
		}

		private void btn4_ButtonClick(object sender, System.EventArgs e)
		{
			IsMouseDown  =false;
			if(this.RightToLeft==RightToLeft.Yes)
				OnButtonLastClick(e); 
			else
				OnButtonFirstClick(e);
		}
		private void btn5_ButtonClick(object sender, System.EventArgs e)
		{
			IsMouseDown  =false;
			OnButtonNewClick(e);
		}
		private void btn6_ButtonClick(object sender, System.EventArgs e)
		{
			IsMouseDown  =false;
			OnButtonDeleteClick(e);
		}


		protected virtual void OnButtonFirstClick(EventArgs e) 
		{
			if(this.ClickFirst != null && this.Enabled  )
                this.ClickFirst(this, new ButtonClickEventArgs("First"));
	
		}

		protected virtual void OnButtonPrevClick(EventArgs e) 
		{
			if(this.ClickPrev != null && this.Enabled  )
                this.ClickPrev(this, new ButtonClickEventArgs("Prev"));
	
		}
		protected virtual void OnButtonNextClick(EventArgs e) 
		{
			if(this.ClickNext != null && this.Enabled  )
                this.ClickNext(this, new ButtonClickEventArgs("Next"));
	
		}
		protected virtual void OnButtonLastClick(EventArgs e) 
		{
			if(this.ClickLast != null && this.Enabled  )
                this.ClickLast(this, new ButtonClickEventArgs("Last"));
	
		}

		protected virtual void OnButtonNewClick(EventArgs e) 
		{
			if(this.ClickNew != null && this.Enabled  )
                this.ClickNew(this, new ButtonClickEventArgs("New"));
	
		}

		protected virtual void OnButtonDeleteClick(EventArgs e) 
		{
			if(this.ClickDelete != null && this.Enabled  )
                this.ClickDelete(this, new ButtonClickEventArgs("Delete"));
		}

		#endregion

		#region Methods

		protected bool IsMouseInButtonRect()
		{
			Rectangle rectButton = GetButtonRect;
			Point mPos = Control.MousePosition;
			if(rectButton.Contains(this.PointToClient(mPos)))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

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

		[Browsable(false)]
		public Rectangle GetButtonRect
		{
			get 
			{
				return new Rectangle(0 ,0,Width ,Height );
			}
		}

//		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
//		public override System.Windows.Forms.BorderStyle BorderStyle
//		{
//			get {return base.BorderStyle;}
//			set 
//			{
//				base.BorderStyle = value;
//			}
//		}

		[Category("Style"),DefaultValue(ControlLayout.Visual)]
		public override ControlLayout ControlLayout 
		{
			get {return base.ControlLayout;}
			set
			{
				if(base.ControlLayout!=value)
				{
					btn1.ControlLayout=value;
					btn2.ControlLayout=value;
					btn3.ControlLayout=value;
					btn4.ControlLayout=value;
					btn5.ControlLayout=value;
					btn6.ControlLayout=value;
			
					this.Invalidate ();
				}
			}
		}

		[Category("McButton"),DefaultValue(true)]
        [RefreshProperties(RefreshProperties.All )]  
		public bool EnableButtons
		{
			get {return btn1.Enabled ;}
			set 
			{
				btn1.Enabled = value;
				btn2.Enabled = value;
				btn3.Enabled = value;
				btn4.Enabled = value;
				if(!value) 
				{
					this.btn5.Enabled=value;
					this.btn6.Enabled=value;
				}
				else
				{
					this.btn5.Enabled=m_EnableNew ;
					this.btn6.Enabled=m_EnableDelete ;
				}
				this.Invalidate();
			}
		}

		[Category("McButton"),DefaultValue(true)]
		public bool EnableDelete
		{
			get {return btn6.Enabled ;}
			set 
			{
				m_EnableDelete=value;
				btn6.Enabled  = value;
				this.Invalidate();
			}
		}

		[Category("McButton"),DefaultValue(true)]
		public bool EnableNew
		{
			get {return btn5.Enabled ;}
			set 
			{
				m_EnableNew =value;
				btn5.Enabled  = value;
				this.Invalidate();
			}
		}

		[Category("McButton")]//,RefreshProperties(RefreshProperties.All)]
		public bool ShowDelete
		{
			get {return m_ViewDelete;}
			set 
			{
				if(m_ViewDelete!=value)
				{
					m_ViewDelete=value;
					//btn6.Visible  = value;
					SetLocation();
					this.Invalidate();
				}
			}
		}

		[Category("McButton")]//,RefreshProperties(RefreshProperties.All)]
		public bool ShowNew
		{
			get {return m_ViewNew ;}
			set 
			{
				if(m_ViewNew!=value)
				{
					m_ViewNew=value;
					//btn5.Visible  = value;
					SetLocation();
				}
				this.Invalidate();
			}
		}

		#endregion

		#region ILayout

//		protected IStyleGuide			m_StyleGuide;
//		protected IStyleButton		m_StylePainter;
//
//		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
//		public IStyleButton StylePainter
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
//					this.Invalidate();
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
//					this.Invalidate();
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
//
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
//
//		protected virtual void OnStyleGuideChanged(EventArgs e)
//		{
//			//
//		}
//
//		protected virtual void OnStylePainterChanged(EventArgs e)
//		{
//			//
//		}
//
//		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
//		{
//			if((DesignMode || IsHandleCreated))
//				this.Invalidate(true);
//		}
//
//		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
//		{
//
//			OnStylePropertyChanged(e);
//		}

		#endregion


//		#region ILayout
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
//					this.Invalidate();
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
//				else
//					return this.m_Style as IStyleLayout;
//
//			}
//		}
//
//		[Category("Style"),System.ComponentModel.RefreshProperties(RefreshProperties.Repaint),DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
//		public StyleButtonDesigner StyleMc
//		{
//			get {return m_Style;}
//			set {m_Style= value;}
//		}
//
//		public virtual void SetStyleLayout(Styles value)
//		{
//			m_Style.SetStyleLayout(value);//m_Style.StylePlan=value;
//		}
//
//		public virtual void SetStyleLayout(StyleLayout value)
//		{
//			m_Style.SetStyleLayout(value);
//			btn1.SetStyleLayout(value);
//			btn2.SetStyleLayout(value);
//			btn3.SetStyleLayout(value);
//			btn4.SetStyleLayout(value);
//			btn5.SetStyleLayout(value);
//			btn6.SetStyleLayout(value);
//			Invalidate();
//		}
//
//
//		protected virtual void OnStyleGuideChanged(EventArgs e)
//		{
//			btn1.StyleGuide =this.m_StyleGuide;
//			btn2.StyleGuide =this.m_StyleGuide;
//			btn3.StyleGuide =this.m_StyleGuide;
//			btn4.StyleGuide =this.m_StyleGuide;
//			btn5.StyleGuide =this.m_StyleGuide;
//			btn6.StyleGuide =this.m_StyleGuide;
//
//		}
//
//		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
//		{
//			if(!(DesignMode || IsHandleCreated))
//				return;
//
//			if(e.PropertyName.Equals("StyleLayout"))
//			{
//				foreach(Control ctl in this.Controls )
//				{
//					if(ctl is ILayout )
//					{
//						((ILayout)ctl).SetStyleLayout(m_Style.Layout); 
//					}
//				}
//			}
//			if(e.PropertyName.Equals("StylePlan"))
//			{
//				foreach(Control ctl in this.Controls )
//				{
//					if(ctl is ILayout )
//					{
//						((ILayout)ctl).SetStyleLayout(m_Style.StylePlan); 
//					}
//				}
//			}
//
//			this.Invalidate();
//		}
//
//		#endregion


	}
}
