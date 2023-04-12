using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using Nistec.Drawing;


namespace Nistec.WinForms
{
	[System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap (typeof(McGroupBox), "Toolbox.GroupBox.bmp")]
	[Designer(typeof(Design.GroupBoxDesigner)), DefaultProperty("Text"), DefaultEvent("Enter")]
	public class McGroupBox : Nistec.WinForms.Controls.McControl,ILayout,IBind
	{

		#region Members
		private System.ComponentModel.Container components = null;

		//private FlatStyle flatStyle;
		private int fontHeight;
 
		//private Single m_Radius = 4;
		private string m_Text = String.Empty;
		private int m_TextOffSet = 6; 
		
		protected int m_GroupIndex;
		internal bool autoChildrenStyle=false;

		[Category("PropertyChanged"), Description("GroupIndexChanged")]
		public event EventHandler SelectedIndexChanged;

		#endregion

		#region Constructors
		
		
		public McGroupBox()
		{
			//m_InheritanceStyle=true;
			this.fontHeight = -1;
			//this.flatStyle = FlatStyle.Standard;
 			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, this.OwnerDraw);
			base.SetStyle(ControlStyles.Selectable, false);
			this.TabStop = false;
			m_GroupIndex=-1;
			//SetStyle(ControlStyles.SupportsTransparentBackColor,true);
			InitializeComponent();
		}

        //internal McGroupBox(bool net):this()
        //{
        //    this.m_netFram=net;
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
			components = new System.ComponentModel.Container();

		}

		#endregion

		#region Overrides

		private void WmEraseBkgnd(ref Message m)
		{
            Win32.RECT rect1 = new Win32.RECT();
            Win32.WinAPI.GetClientRect(base.Handle, ref rect1);
            Graphics graphics1 = Graphics.FromHdcInternal(m.WParam);
            Brush brush1 = LayoutManager.GetBrushFlat();// new SolidBrush(this.BackColor);
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


		protected override void OnFontChanged(EventArgs e)
		{
			this.fontHeight = -1;
			base.Invalidate();
			base.OnFontChanged(e);
			base.PerformLayout();
		}


		protected override void OnPaint(PaintEventArgs e)
		{

            //this.ForeColor=LayoutManager.Layout.ForeColorInternal;// m_Style.BackgroundColor;
            //this.BackColor=LayoutManager.Layout.BackgroundColorInternal;// m_Style.BackgroundColor;
			Graphics graphics1 = e.Graphics;
			Rectangle rectangle1 = base.ClientRectangle;
			int num1 = 8;
			rectangle1.X += num1;
			rectangle1.Width -= 2 * num1;
			Brush brush1 = new SolidBrush(this.ForeColor);
			StringFormat format1 = new StringFormat();
			if (base.ShowKeyboardCues)
			{
				format1.HotkeyPrefix = HotkeyPrefix.Show;
			}
			else
			{
				format1.HotkeyPrefix = HotkeyPrefix.Hide;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			Size size1 = Size.Ceiling(graphics1.MeasureString(this.Text, this.Font, rectangle1.Width, format1));
			Color color1 =LayoutManager.Layout.DisableColorInternal;// m_Style.DisabledColor;// base.DisabledColor;
			if (base.Enabled)
			{
				graphics1.DrawString(this.Text, this.Font, brush1, (RectangleF) rectangle1, format1);
			}
			else
			{
				ControlPaint.DrawStringDisabled(graphics1, this.Text, this.Font, color1, (RectangleF) rectangle1, format1);
			}
			format1.Dispose();
			brush1.Dispose();
			Color bordercolor =LayoutManager.Layout.BorderColorInternal;// m_Style.BorderColor;
	
			//Pen pen1 = new Pen(bordercolor);//ControlPaint.Light(color1, 1f));
			//Pen pen2 = new Pen(bordercolor);//ControlPaint.Dark(color1, 0f));
			Pen pen1 = new Pen(ControlPaint.Light(bordercolor, 1f));
			Pen pen2 = new Pen(ControlPaint.Dark(bordercolor, 0f));
			int num2 = num1;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				num2 = (num1 + rectangle1.Width) - size1.Width;
			}
			int num3 = base.FontHeight / 2;
			graphics1.DrawLine(pen1, 1, num3, 1, base.Height - 1);
			graphics1.DrawLine(pen2, 0, num3, 0, base.Height - 2);
			graphics1.DrawLine(pen1, 0, base.Height - 1, base.Width, base.Height - 1);
			graphics1.DrawLine(pen2, 0, base.Height - 2, base.Width - 1, base.Height - 2);
			graphics1.DrawLine(pen2, 0, num3 - 1, num2, num3 - 1);
			graphics1.DrawLine(pen1, 1, num3, num2, num3);
			graphics1.DrawLine(pen2, num2 + size1.Width, num3 - 1, base.Width - 2, num3 - 1);
			graphics1.DrawLine(pen1, num2 + size1.Width, num3, base.Width - 1, num3);
			graphics1.DrawLine(pen1, base.Width - 1, num3 - 1, base.Width - 1, base.Height - 1);
			graphics1.DrawLine(pen2, base.Width - 2, num3, base.Width - 2, base.Height - 2);
			base.OnPaint(e);
		}

 
		protected override bool ProcessMnemonic(char charCode)
		{
			if (!Control.IsMnemonic(charCode, this.Text))// || !base.CanProcessMnemonic())
			{
				return false;
			}
			//IntSecurity.ModifyFocus.Assert();
			try
			{
				base.SelectNextControl(null, true, true, true, false);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return true;
		}


//		protected override void OnControlAdded(ControlEventArgs e)
//		{
//			base.OnControlAdded(e);
//			if( e.Control is ILayout && InheritanceStyle)
//			{
//				if(this.StyleGuide!=null)
//					((ILayout)e.Control).StyleGuide=this.StyleGuide;
//				else
//				   ((ILayout)e.Control).SetStyleLayout(this.m_Style.Layout); 
//			}
//		}

//		protected override void OnControlRemoved(ControlEventArgs e)
//		{
//			base.OnControlRemoved(e);
//		}

		public override string ToString()
		{
			string text1 = base.ToString();
			return (text1 + ", Text: " + this.Text);
		}

		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if(this.SelectedIndexChanged!=null)
			{
              this.SelectedIndexChanged(this,e);
			}
		}
		#endregion

        #region StyleProperty

        //[Category("Style")]
        //public virtual ControlLayout ControlLayout
        //{
        //    get { return m_ControlLayout; }
        //    set
        //    {
        //        if (m_ControlLayout != value)
        //        {
        //            m_ControlLayout = value;
        //            bool flag1 = (m_ControlLayout == ControlLayout.XpLayout) || (value == ControlLayout.XpLayout);
        //            //this.flatStyle = value;
        //            base.SetStyle(ControlStyles.SupportsTransparentBackColor, this.OwnerDraw);
        //            base.SetStyle(ControlStyles.UserPaint, this.OwnerDraw);
        //            base.SetStyle(ControlStyles.UserMouse, this.OwnerDraw);
        //            base.SetStyle(ControlStyles.ResizeRedraw, this.OwnerDraw);
        //            if (flag1)
        //            {
        //                base.RecreateHandle();
        //            }
        //            else
        //            {
        //                this.Refresh();
        //            }

        //            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        //            //this.Invalidate();
        //        }
        //    }
        //}


        [Category("Style"), DefaultValue(typeof(Color), "WindowText")]
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

        [Category("Style"), DefaultValue(typeof(Color), "Control")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
               if (base.BackColor != value)
                {
                    SerializeBackColor(value, true);
                }
            }
        }

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("StylePlan") || e.PropertyName.Equals("ControlLayout"))
            {
                SerializeBackColor(Color.Empty, false);
                SerializeForeColor(Color.Empty, false);
                SerializeFont(Form.DefaultFont, false);
            }
            else
            {
                if ( e.PropertyName.Equals("ColorBrush1") || e.PropertyName.Equals("BackgroundColor"))
                    SerializeBackColor(Color.Empty, false);
                if (e.PropertyName.Equals("ForeColor"))
                    SerializeForeColor(Color.Empty, false);
                if (e.PropertyName.Equals("TextFont"))
                    SerializeFont(Form.DefaultFont, false);
            }
            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }
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
            if (IsHandleCreated && StylePainter != null)
                base.BackColor = LayoutManager.Layout.BackgroundColorInternal;
            else if (force)
                base.BackColor = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
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

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
            get
            {
                CreateParams params1 = base.CreateParams;
                if (!this.OwnerDraw)
                {
                    params1.ClassName = "BUTTON";
                    params1.Style |= 7;
                }
                params1.ExStyle |= 0x10000;
                return params1;
            }
        }

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}
 
		public override Rectangle DisplayRectangle
		{
			get
			{
				Size size1 = base.ClientSize;
				if (this.fontHeight == -1)
				{
					this.fontHeight = this.Font.Height;
				}
				return new Rectangle(3, this.fontHeight + 3, Math.Max(size1.Width - 6, 0), Math.Max((size1.Height - this.fontHeight) - 6, 0));
			}
		}

        //[DefaultValue(2), Description("ButtonFlatStyle"), Category("Appearance")]
        //public FlatStyle FlatStyle
        //{
        //    get
        //    {
        //        return this.flatStyle;
        //    }
        //    set
        //    {
        //        if (!Enum.IsDefined(typeof(FlatStyle), value))
        //        {
        //            throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
        //        }
        //        if (this.flatStyle != value)
        //        {
        //            bool flag1 = (this.flatStyle == FlatStyle.System) || (value == FlatStyle.System);
        //            this.flatStyle = value;
        //            base.SetStyle(ControlStyles.SupportsTransparentBackColor, this.OwnerDraw);
        //            base.SetStyle(ControlStyles.UserPaint, this.OwnerDraw);
        //            base.SetStyle(ControlStyles.UserMouse, this.OwnerDraw);
        //            base.SetStyle(ControlStyles.ResizeRedraw, this.OwnerDraw);
        //            if (flag1)
        //            {
        //                base.RecreateHandle();
        //            }
        //            else
        //            {
        //                this.Refresh();
        //            }
        //        }
        //    }
        //}
 
		private bool OwnerDraw
		{
			get
			{
                return true;// (this.m_ControlLayout != ControlLayout.XpLayout);//(this.FlatStyle != FlatStyle.System);
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
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
 
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				bool flag1 = base.Visible;
				try
				{
					if (flag1 && base.IsHandleCreated)
					{
						Win32.WinAPI.SendMessage(this.Handle, 11, 0, 0);
						//base.SendMessage(11, 0, 0);
					}
					base.Text = value;
				}
				finally
				{
					if (flag1 && base.IsHandleCreated)
					{
						Win32.WinAPI.SendMessage(this.Handle, 11, 1, 0);
						//base.SendMessage(11, 1, 0);
					}
				}
				base.Invalidate(true);
			}
		}

//		[Browsable(false),Category("Appearance"),DefaultValue((System.Single)(4))]
//		internal Single Radius
//		{
//			get
//			{ 
//				return m_Radius; 
//			}
//			set
//			{
//				if(value >=0 && value<=10)
//				{
//					m_Radius = value;
//					this.Invalidate(false);
//				}
//			}
//		}

		[Browsable(false),Category("Appearance"),DefaultValue(6)]
		internal int TextOffSet
		{
			get
			{ 
				return m_TextOffSet; 
			}
			set
			{
				m_TextOffSet = value;
				this.Invalidate(false);
			}
		}

		[Browsable(true),Bindable(true),Category("Appearance"),DefaultValue(-1)]
		public int GroupIndex
		{
			get
			{ 
				//if(m_GroupIndex > -1)
				//{
					return m_GroupIndex;
				//}
				//return GetRadioChecked(); 
			}
			set
			{
				if(m_GroupIndex !=value)
				{
					SetButtonIndex(value);
					this.Invalidate(false);
				}
			}
		}

		#endregion

		#region Methods

		internal bool SetButtonIndex(int value)
		{
			foreach(Control ctl in this.Controls)
			{
				if(ctl is McRadioButton)
				{
					if(((McRadioButton)ctl).GroupIndex==value)
					{
						((McRadioButton)ctl).CheckedInternal (true); 
						m_GroupIndex = value;
						OnSelectedIndexChanged(EventArgs.Empty);
						return true;
					}
				}
			}
           return false;
		}

		internal void SetGroupIndex(int value)
		{
			m_GroupIndex = value;
			OnSelectedIndexChanged(EventArgs.Empty);
		}


		private int GetRadioChecked()
		{
			foreach(Control c in this.Controls)
			{
				if(c is McRadioButton)
				{
					if(((McRadioButton)c).Checked)
						return ((McRadioButton)c).GroupIndex;
				}
			}
			return -1;
		}
		#endregion

		#region Events

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
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
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
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
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
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
		[EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				base.MouseDown += value;
			}
			remove
			{
				base.MouseDown -= value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
		public new event EventHandler MouseEnter
		{
			add
			{
				base.MouseEnter += value;
			}
			remove
			{
				base.MouseEnter -= value;
			}
		}
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler MouseLeave
		{
			add
			{
				base.MouseLeave += value;
			}
			remove
			{
				base.MouseLeave -= value;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				base.MouseUp += value;
			}
			remove
			{
				base.MouseUp -= value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
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

     	private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnStylePropertyChanged(e);
		}
		#endregion

		#region IBind Members

		private bool readOnly=false;

		[Browsable(false)]
		public virtual BindingFormat BindFormat
		{
			get{return BindingFormat.Int;}
		}

		public string BindPropertyName()
		{
			return "GroupIndex";
		}

		public bool ReadOnly
		{
			get{return this.readOnly;} 
			set
			{
				this.readOnly=value;
				if(value)
				{
					foreach(Control c in this.Controls)
					{
						if( c is IBind)
						{
							((IBind)c).ReadOnly=value;
						}
					}

				}
			}
		}
        public virtual void BindDefaultValue()
        {
            this.GroupIndex = 0;
        }
		#endregion
	}
}
