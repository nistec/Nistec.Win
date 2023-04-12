using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Nistec.Drawing;
using System.Security.Permissions;
using Nistec.Win32;
using Nistec.Win;

namespace Nistec.WinForms.Controls
{
	public enum McState
	{
		Default,
		Hot,
		Focused,
		Clicked
	}


	[System.ComponentModel.ToolboxItem(false)]
	public  class McEditBase : McControl,IMcEdit,ILayout,IBind	
	{

		#region Members

		internal const int minHeight=13;
        internal bool m_IsValid;
        internal int ForcesSize;
        internal McState ctlState;

        protected int ButtonPedding;

		private bool		m_ReadOnly;
		private Image		m_Image;
		private BorderStyle	m_BorderStyle;
		private string		m_DefaultValue;

		private short prefHeightCache;
		private int requestedHeight;
		private bool integralHeightAdjust;
		//private bool wordWrap;
		//private bool hideSelection;
		private bool m_FixSize;

        private ControlLayout m_ControlLayout;
        //protected ControlLayout defaultControlLayout = ControlLayout.Visual;


		[Description("OnFixedSizeChanged"), Category("PropertyChanged")]
		public event EventHandler FixedSizeChanged;
        [Category("Property Changed")]
        public event EventHandler ControlLayoutChanged;
		[Category("Behavior")]
		public  event ErrorOcurredEventHandler ErrorOcurred;

		#endregion

		#region Constructors

        internal McEditBase()
		{

             base.SetStyle(ControlStyles.ResizeRedraw, true);
			//			base.SetStyle(ControlStyles.DoubleBuffer,true);
			//			base.SetStyle(ControlStyles.UserPaint,true);
			//			base.SetStyle(ControlStyles.AllPaintingInWmPaint,true);

            base.BackColor = SystemColors.Window;
            base.ForeColor = SystemColors.WindowText;

			ctlState=McState.Default;
			ForcesSize=0;
			ButtonPedding=0;
			m_FixSize=true;
			m_ReadOnly=false;
			m_IsValid = true;
            m_BorderStyle = BorderStyle.FixedSingle;
			m_DefaultValue="";
            m_ControlLayout = ControlLayout.Visual;

            this.prefHeightCache = -1;
			this.integralHeightAdjust = false;
			//this.wordWrap=true;
			//this.hideSelection=false;
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.FixedHeight, m_FixSize);
			this.requestedHeight = this.DefaultSize.Height;
			this.requestedHeight = base.Height;

			this.Name = "McEditBase";
    
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
            //this.OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
            //this.BackColor=this.LayoutManager.Layout.BackColorInternal;
            //this.ForeColor=this.LayoutManager.Layout.ForeColorInternal;

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
			}
			base.Dispose( disposing );
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
					if(ForcesSize>0)
					{
						base.Height =this.ForcesSize; 
					}
					else if (m_FixSize)
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

        [Category("Appearance"), Description("Border"), DefaultValue(BorderStyle.FixedSingle), System.Runtime.InteropServices.DispId(-504)]
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

//		protected override void OnControlAdded(ControlEventArgs e)
//		{
//			base.OnControlAdded(e);
//			if( e.Control is ILayout)// && Parent is ILayout)
//			{
//				((ILayout)e.Control).StylePainter=this.StylePainter;//((ILayout)Parent).StyleGuide; 
//			}
//			//e.Control.MouseEnter += new System.EventHandler(this.ChildCtrlMouseEnter);
//			//e.Control.MouseLeave += new System.EventHandler(this.ChildCtrlMouseLeave);
//		}
//
//		protected void ChildCtrlMouseLeave(object sender,System.EventArgs e)
//		{
//			this.Invalidate();//DrawControl(false);
//		}
//
//		protected void ChildCtrlMouseEnter(object sender,System.EventArgs e)
//		{
//			this.Invalidate();//DrawControl(true);
//		}

		#endregion

		#region Overrides

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle Crect =ClientRectangle;
			Rectangle rect =new Rectangle(Crect.X,Crect.Y,Crect.Width-1,Crect.Height-1);
             
			if(this.m_BorderStyle == BorderStyle.Fixed3D  )
				ControlPaint.DrawBorder3D (e.Graphics,this.ClientRectangle,Border3DStyle.Sunken );
			else if(this.m_BorderStyle != BorderStyle.None )
			{
				//bool allowHot = (this.Enabled && !this.DesignMode) && !(this.IsMouseHover && Control.MouseButtons == MouseButtons.Left && !this.ContainsFocus);
				bool hot =ctlState==McState.Hot;// this.IsMouseHover  && allowHot ;//(this.IsMouseHover || this.ContainsFocus) && allowHot;
				bool focused=this.ContainsFocus;

				this.LayoutManager.DrawBorder(e.Graphics,rect,ReadOnly, Enabled,focused,hot);
			}
		}

		protected override void OnMouseEnter(System.EventArgs e)
		{
			base.OnMouseEnter(e);
			if(this.Enabled  && !this.ContainsFocus && ctlState!=McState.Hot)// && IsMouseHover())
			{
				ctlState=McState.Hot;	
				this.Invalidate();// DrawControl(true);
			}
		}

		protected override void OnMouseLeave(System.EventArgs e)
		{
			base.OnMouseLeave(e);
			if(this.Enabled && !this.ContainsFocus && !IsMouseHover())
			{
				ctlState=McState.Default;	
				this.Invalidate();//DrawControl(false);
			}
		}

		protected override void OnGotFocus(System.EventArgs e)
		{
			base.OnGotFocus(e);
			ctlState=McState.Focused;//ResetError();
			this.Invalidate();//DrawControl(false);
		}

		protected override void OnLostFocus(System.EventArgs e)
		{
			base.OnLostFocus(e);
			ctlState=McState.Default;
			this.Invalidate();//DrawControl(false);
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged (e);
			this.AdjustHeight();
		}

		protected override void OnEnabledChanged(System.EventArgs e)
		{
			base.OnEnabledChanged (e);
			this.Invalidate();//DrawControl(false);
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

		protected virtual void OnErrorOcurred(ErrorOcurredEventArgs e)
		{
			if (e.Message.Length > 0)
			{
				if (this.ErrorOcurred != null)
				{
					ErrorOcurredEventArgs oArg = new ErrorOcurredEventArgs(e.Message);
					ErrorOcurred(this, oArg); 
					string msg=e.Message;
					//ErrProvider.ShowError(this,ErrProviders.MsgBox,msg);
				}
			}
		}

        protected virtual void OnControlLayoutChanged(EventArgs e)
        {
            if (ControlLayoutChanged != null)
                ControlLayoutChanged(this, e);
        }
		#endregion

		#region ILayout

		protected IStyle	m_StylePainter;
  
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
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{

            //if (m_ControlLayout == ControlLayout.System)
            //    return;

            //if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackColor"))
            //    base.BackColor = LayoutManager.Layout.BackColorInternal;
            //if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
            //    base.ForeColor = LayoutManager.Layout.ForeColorInternal;

            //if ((DesignMode || IsHandleCreated))
            //    this.Invalidate(true);

            //if(e.PropertyName.Equals("BackColor"))
            //    this.BackColor=LayoutManager.Layout.BackColorInternal;//this.OnBackColorChanged(EventArgs.Empty);
            //else if(e.PropertyName.Equals("ForeColor"))
            //    this.ForeColor=LayoutManager.Layout.ForeColorInternal;//this.OnForeColorChanged(EventArgs.Empty);
            //else if ( e.PropertyName.Equals("StyleGuide"))
            //{
            //    this.BackColor = LayoutManager.Layout.BackColorInternal;//this.OnBackColorChanged(EventArgs.Empty);
            //    this.ForeColor = LayoutManager.Layout.ForeColorInternal;//this.OnForeColorChanged(EventArgs.Empty);
            //}
            //if((DesignMode || IsHandleCreated))
            //    this.Invalidate(true);
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnStylePropertyChanged(e);
		}

		#endregion

		#region Properties


        [Category("Style"),DefaultValue(ControlLayout.Visual)]    
        public virtual ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                if (m_ControlLayout != value)
                {
                    m_ControlLayout = value;
                    this.OnControlLayoutChanged(EventArgs.Empty);
                    OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
                    this.Invalidate();
                }
            }
        }

		[Category("Behavior"),DefaultValue("")]//,DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string DefaultValue
		{
			get {return m_DefaultValue;}
			set
			{
				if(m_DefaultValue != value)
				{
					m_DefaultValue = value;
                    //if(DesignMode)
                    //{
                    //    this.Text=m_DefaultValue;
                    //    this.Refresh();
                    //}
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

		public bool IsMouseHover()
		{
			try
			{
				Point mPos  = Control.MousePosition;
				bool retVal = this.ClientRectangle.Contains(this.PointToClient(mPos));
				return retVal;
			}
			catch{return false;}
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

		[Browsable(false)]
		public bool IsValid 
		{
			get{return m_IsValid;}
		}

		#endregion

 		#region Methods
		
		protected int GetHeightInternal()
		{
			if(ForcesSize>0)
			{
				return(int)this.ForcesSize; 
			}
			else if(FixSize)
			{
				switch(m_BorderStyle)
				{
					case BorderStyle.Fixed3D :
						return(int)this.Font.GetHeight ()+8; 
					case BorderStyle.FixedSingle  :
						return(int)this.Font.GetHeight ()+8; 
					case BorderStyle.None :
						return (int)this.Font.GetHeight()+1;//.GetHeight ()+1; 
				}
			}
			return Math.Max(minHeight+ButtonPedding, this.Height);
		}

	
		protected virtual bool AllowEdit()
		{
			return Enabled && ! m_ReadOnly; 
		}

		#endregion

		#region Internal Methods

		//		internal virtual bool FocusInternal()
		//		{
		//			if (this.CanFocus)
		//			{
		//				WinAPI.SetFocus(new HandleRef(this, this.Handle));
		//			}
		//			if (this.Focused && (this.ParentInternal != null))
		//			{
		//				IContainerControl control1 = this.ParentInternal.GetContainerControl();
		//				if (control1 != null)
		//				{
		//					if (control1 is ContainerControl)
		//					{
		//						((ContainerControl) control1).SetActiveControl(this);
		//					}
		//					else
		//					{
		//						control1.ActiveControl = this;
		//					}
		//				}
		//			}
		//			return this.Focused;
		//		}
		//	
		//		internal void SetActiveControlInternal(Control value)
		//		{
		//			if ((this.ActiveControl != value) || ((value != null) && !value.Focused))
		//			{
		//				bool flag1;
		//				if ((value != null) && !base.Contains(value))
		//				{
		//					throw new ArgumentException("CannotActivateControl");
		//				}
		//				ContainerControl control1 = this;
		//				if ((value != null) && (value.Parent != null))
		//				{
		//					control1 = value.Parent.GetContainerControl() as ContainerControl;
		//				}
		//				if (control1 != null)
		//				{
		//					flag1 = control1.ActiveControl==value;
		//					//flag1 = control1.ActivateControlInternal(value, false);
		//				}
		//				else
		//				{
		//                    control1 = value;
		//					flag1 = control1.ActiveControl==value;
		//					//flag1 = this.AssignActiveControlInternal(value);
		//				}
		//				if ((control1 != null) && flag1)
		//				{
		//					ContainerControl control2 = this;
		//					while ((control2.Parent != null) && (control2.Parent.GetContainerControl() is ContainerControl))
		//					{
		//						control2 = control2.Parent.GetContainerControl() as ContainerControl;
		//					}
		//					if (control2.ContainsFocus && (((value == null) || !(value is UserControl)) || ((value is UserControl) && !((UserControl) value).HasFocusableChild())))
		//					{
		//						control1.Focus();// FocusActiveControlInternal();
		//					}
		//				}
		//			}
		//		}
		//
		//		internal void FocusActiveControlInternal()
		//		{
		//			if ((this.ActiveControl != null) && this.ActiveControl.Visible)
		//			{
		//				IntPtr ptr1 = WinAPI.GetFocus();
		//				if ((ptr1 == IntPtr.Zero) || (Control.FromChildHandle(ptr1) != this.ActiveControl))
		//				{
		//					WinAPI.SetFocus(new HandleRef(this.ActiveControl, this.ActiveControl.Handle));
		//				}
		//			}
		//			else
		//			{
		//				ContainerControl control1 = this;
		//				while ((control1 != null) && !control1.Visible)
		//				{
		//					Control control2 = control1.Parent;
		//					if (control2 == null)
		//					{
		//						break;
		//					}
		//					control1 = control2.GetContainerControl() as ContainerControl;
		//				}
		//				if ((control1 != null) && control1.Visible)
		//				{
		//					WinAPI.SetFocus(new HandleRef(control1, control1.Handle));
		//				}
		//			}
		//		}
		//
		//
		//		internal bool ActivateControlInternal(Control control)//, bool originator)
		//		{
		//			bool flag1 = true;
		//			bool flag2 = false;
		//			ContainerControl control1 = null;
		//			Control control2 = this.ParentInternal;
		//			if (control2 != null)
		//			{
		//				control1 = control2.GetContainerControl() as ContainerControl;
		//				if (control1 != null)
		//				{
		//					flag2 = control1.ActiveControl != this;
		//				}
		//			}
		//			if ((control != this.activeControl) || flag2)
		//			{
		//				if (flag2 && !control1.ActivateControlInternal(this, false))
		//				{
		//					return false;
		//				}
		//				flag1 = this.AssignActiveControlInternal((control == this) ? null : control);
		//			}
		//			//if (originator)
		//			//{
		//			//	this.ScrollActiveControlIntoView();
		//			//}
		//			return flag1;
		//		}
		//
		//		private bool AssignActiveControlInternal(Control value)
		//		{
		//			if (this.ActiveControl != value)
		//			{
		//				this.ActiveControl = value;
		//                this.ActiveControl.Focus ();
		//				//this.UpdateFocusedControl();
		//				//if (this.ActiveControl == value)
		//				//{
		//				//	Form form1 = base.FindForm();
		//				//	if (form1 != null)
		//				//	{
		//				//		form1.UpdateDefaultButton();
		//				//	}
		//				//}
		//			}
		//			return (this.ActiveControl == value);
		//		}
		//
		// 
		//		internal bool HasFocusableChild()
		//		{
		//			Control control1 = null;
		//			do
		//			{
		//				control1 = base.GetNextControl(control1, true);
		//			}
		//			while ((((control1 == null) || !control1.CanSelect) || !control1.TabStop) && (control1 != null));
		//			return (control1 != null);
		//		}
		//
		//
		//		private Control parent;
		//
		//		internal virtual Control ParentInternal
		//		{
		//			get
		//			{
		//				return this.parent;
		//			}
		//			set
		//			{
		//				if (this.parent != value)
		//				{
		//					if (value != null)
		//					{
		//						value.Controls.Add(this);
		//					}
		//					else
		//					{
		//						this.parent.Controls.Remove(this);
		//					}
		//				}
		//			}
		//		}
		// 
		//

		#endregion

		#region IControlKeyAction Members


		protected override bool ProcessDialogKey(Keys keyData)
		{
			bool proc=false;

			if (keyData == Keys.Escape )
			{
				proc= OnEscapeAction();
			} 
			else if (keyData == Keys.Enter)
			{
				proc=OnEnterAction();
			} 
			else if (keyData == Keys.Insert )
			{
				proc=OnInsertAction();
			}

			if(!proc)
				proc= base.ProcessDialogKey(keyData);
			return proc;
		}

		protected virtual bool OnInsertAction()
		{
			return false;	
		}

		protected virtual bool OnEnterAction()
		{
			return false;	
		}

		protected virtual bool OnEscapeAction()
		{
			return false;	
		}

		public virtual void ActionTabNext()
		{
			ProcessDialogKey(Keys.Tab);
		}

		public virtual void ActionClick()
		{
			this.InvokeOnClick(this,new System.EventArgs ());  
		}

		public virtual bool IsValidating()
		{
			return true;
		}

		[Browsable(false)]
		protected virtual FieldType BaseFormat 
		{
			get{return FieldType.Text;}
		}
		#endregion

		#region IBind Members

		[Browsable(false)]
		public virtual BindingFormat BindFormat
		{
			get{return BindingFormat.String;}
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
	}

}
