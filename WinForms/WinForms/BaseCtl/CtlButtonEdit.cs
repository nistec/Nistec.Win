using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Nistec.Drawing;
using System.Collections;
using System.Runtime.InteropServices;

using Nistec.Win32;
using Nistec.WinForms.Design;

namespace Nistec.WinForms.Controls
{

	[Designer(typeof(McButtonEditDesigner))]
	[DefaultEvent("ButtonClick"),
	System.ComponentModel.ToolboxItem(false)]
	public class McButtonEdit : McEditBase
	{

		#region Members

		internal int				m_ButtonWidth = 16;
        internal McButtonCombo m_Button;
        internal bool m_AutoHide;
		
        
        private bool				m_DroppedDown;
		private HorizontalAlignment	m_TextAlign;
		private IntPtr				m_mouseHookHandle;
		private GCHandle			m_mouseProcHandle;
	    private bool				IsHooked;
		private ButtonAlign m_ButtonAlign;
        private bool m_ButtonVisible;
        internal bool IgnorSelection;

		[Category("Property Changed")]
		public new event EventHandler TextChanged;

		#endregion

		#region Constructors

        internal McButtonEdit()
            : this(true)
        {

        }

        internal McButtonEdit(bool isDefault)
            : base()
		{
			m_AutoHide=false;
			m_mouseHookHandle = IntPtr.Zero;
			ButtonPedding = 2;
			m_ButtonAlign=ButtonAlign.Right;
			m_DroppedDown = false;
            m_ButtonVisible = true;
			//m_TextRect =new Rectangle(1,1,this.Width - m_ButtonWidth ,this.Height );
			InitializeComponent();
			//m_Button.m_netFram=true;
			if(isDefault)
				this.m_Button.Image =ResourceUtil.LoadImage (Global.ImagesPath + "btnCombo.gif");
			else
				this.m_Button.Image=ResourceUtil.LoadImage (Global.ImagesPath + "btnBrows.gif");
	
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(IsHooked)
					EndHook();
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
			//System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McButtonEdit));
			this.m_Button = new McButtonCombo(this);
			this.SuspendLayout();
			// 
			// m_Button
			// 
			this.m_Button.ButtonComboType = ButtonComboType.Combo;
			this.m_Button.Location = new System.Drawing.Point(88, 2);
			this.m_Button.Name = "m_Button";
			this.m_Button.Size = new System.Drawing.Size(16, 16);
			this.m_Button.TabIndex = 1;
			this.m_Button.TabStop = false;
			this.m_Button.ToolTipText = "";
			this.m_Button.Click+=new EventHandler(m_Button_Click);
			// 
			// McButtonEdit
			// 
			this.Controls.Add(this.m_Button);
			this.Name = "McButtonEdit";
			this.Size = new System.Drawing.Size(104, 20);
			this.ResumeLayout(false);

		}
		#endregion

		#region Override

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
			OnButtonAlignChanged(e);
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if(IsMouseInButtonRect())
			{
				this.Invalidate(false);
			}
		}

		protected override void OnTextChanged(System.EventArgs e)
		{
			base.OnTextChanged(e);
			if(this.TextChanged !=null)
				TextChanged(this,new System.EventArgs ()); 
		}

		protected override void OnRightToLeftChanged(System.EventArgs e)
		{
			base.OnRightToLeftChanged (e);
			this.Invalidate ();
		}

		protected override void OnEnabledChanged(System.EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.m_Button.Enabled =this.Enabled;  
		}

		protected virtual void OnButtonAlignChanged(EventArgs e)
		{
            if (m_ButtonVisible)//  base.ControlLayout != ControlLayout.System)
			{
				Rectangle rect =this.ClientRectangle;  

				if(this.ButtonAlign==ButtonAlign.Left)
				{
				
					this.m_Button.Location  =new Point (rect.X +ButtonPedding,rect.Y  +ButtonPedding);
					this.m_Button.Size =new Size (m_ButtonWidth,rect.Height-(ButtonPedding*2));
				}
				else
				{
					this.m_Button.Size =new Size (m_ButtonWidth,this.Height-(ButtonPedding*2));
					this.m_Button.Location  =new Point (rect.X +this.Width - m_ButtonWidth - ButtonPedding,rect.Y  +ButtonPedding);
				}
			}
			this.Invalidate();
		}

        protected override void OnControlLayoutChanged(EventArgs e)
		{
            if (!m_ButtonVisible)
                goto Label_01;

     
            if (base.ControlLayout == ControlLayout.Flat)
			{
                ButtonPedding = 0;
				this.m_Button.ControlLayout=ControlLayout.Visual;
			}
			else
			{
                ButtonPedding = 2;
				this.m_Button.ControlLayout=ControlLayout;
			}

			OnButtonAlignChanged(e);

            Label_01:
            base.OnControlLayoutChanged(e);
 			this.Invalidate();
		}

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F4)
            {
                DoDropDown();
            }
            return base.ProcessDialogKey(keyData);
        }
		#endregion

		#region Mouse Hook Methods

		protected virtual void StartHook()
		{
            IgnorSelection = false;

			if(IsHooked){EndHook();}
			// Mouse hook
			WinAPI.HookProc mouseHookProc = new WinAPI.HookProc(MouseHook);
			m_mouseProcHandle = GCHandle.Alloc(mouseHookProc);
			m_mouseHookHandle = WinAPI.SetWindowsHookEx((int)WindowsHookCodes.WH_MOUSE, 
				mouseHookProc, IntPtr.Zero, WinAPI.GetCurrentThreadId());
			if(m_mouseHookHandle ==IntPtr.Zero)
			{
				throw new ApplicationException(" Error Create handle");
			}
			IsHooked=true;
			m_DroppedDown=true;
		}

		protected virtual void EndHook()
		{
			// Unhook   
			if(!IsHooked){return;}
			WinAPI.UnhookWindowsHookEx( m_mouseHookHandle );
			m_mouseProcHandle.Free();
			m_mouseHookHandle = IntPtr.Zero;
			IsHooked=false;
			m_DroppedDown=false;
		}

		private IntPtr MouseHook(int code, IntPtr wparam, IntPtr lparam) 
		{
			MOUSEHOOKSTRUCT mh = (MOUSEHOOKSTRUCT )Marshal.PtrToStructure(lparam, typeof(MOUSEHOOKSTRUCT));
           
			bool res=GetMouseHook(mh.hwnd,wparam);
			if (res)
			{
				//WinAPI.SetFocus(IntPtr.Zero);
				return mh.hwnd;
			}
             
			return WinAPI.CallNextHookEx( m_mouseHookHandle, code, wparam, lparam );
		}

		protected virtual bool GetMouseHook(IntPtr mh,IntPtr wparam)
		{

			//			Msgs msg = (Msgs) wparam.ToInt32();
			//			if ((((mh != this.Handle) &&  m_DroppedDown) && ((msg == Msgs.WM_LBUTTONDOWN) || (msg == Msgs.WM_RBUTTONDOWN))) || (msg == Msgs.WM_NCLBUTTONDOWN))
			//			{
			//				EndHook();
			//				return true;
			//			}
			//			else if ((((mh != this.Handle) && m_DroppedDown) && ((msg == Msgs.WM_LBUTTONUP) || (msg == Msgs.WM_RBUTTONUP))) || (msg == Msgs.WM_NCLBUTTONUP))
			//			{
			//				EndHook();
			//				return true;
			//			}

			return false;
		}

		#endregion

		#region Virtual

		public virtual void DoDropDown()
		{
            //if (m_DroppedDown)
            //{
            //    IgnorSelection = true;
            //}
		}

		public virtual void CloseDropDown()
		{

		}

		protected virtual void OnButtonClick(EventArgs e) 
		{
			DoDropDown();
		}

		private void m_Button_Click(object sender, EventArgs e)
		{
			if(this.Enabled  )
			{
                OnButtonClick(e);
			}
		}


		#endregion
		 
		#region Methods

		public Rectangle GetContentRect()
		{
			if(!IsHandleCreated)
				return Rectangle.Empty;

			Rectangle rect=this.ClientRectangle;
			int textHeight = this.FontHeight+8;
			int yPos = (rect.Height  - textHeight) / 2;

			Size size;
			Point point;

            if (base.ControlLayout == ControlLayout.System)
			{
				size =new Size (Size.Width -6,Size.Height -4);
				point  =new Point (2,yPos);
			}
			else
			{
				size =new Size (this.Width - m_Button.Width -6,rect.Height -4);
	
				if(this.ButtonAlign==ButtonAlign.Left)// this.RightToLeft==System.Windows.Forms.RightToLeft.Yes)
				{
					point  =new Point (rect.X +m_Button.Left + m_Button.Width +2,rect.Y  +yPos);
				}
				else
				{
					point  =new Point (rect.X +2,rect.Y  +yPos);
				}
			}
			return new Rectangle(point,size);
		}


		[Browsable(false)]
		public Rectangle GetButtonRect
		{
			get{ return this.m_Button.ClientRectangle;  
				//return new Rectangle(this.Width - m_ButtonWidth,1,m_ButtonWidth - 1,this.Height - 2);
			}
		}

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

		#region ButtonProperties

		[Category("McButton"),Browsable(false)]
		internal McButtonCombo InternalButton
		{
			get{return m_Button;}
		}

		[Category("McButton")]//,DefaultValue("")]
		public String ButtonToolTip
		{
            get { return m_Button.ToolTipText; }
			set{
                //if (m_Button.ToolTipText != value)
                    m_Button.ToolTipText = value;
			   }
		}

		[Category("McButton"),Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal Image ButtonImage
		{
			get{return m_Button.Image;}
			set
			{
				if(m_Button.Image !=value)
				{
					m_Button.Image=value;
					Invalidate (m_Button.ClientRectangle);
				}
			}
		}

		[Category("McButton"),DefaultValue(ButtonAlign.Right)]
		public ButtonAlign ButtonAlign
		{
			get{ return m_ButtonAlign; }
			set
			{
				if(m_ButtonAlign != value)
				{
					m_ButtonAlign=value;
					OnButtonAlignChanged(EventArgs.Empty); //SetSize(); 
					this.Invalidate ();
				}
			}
		}

		#endregion

		#region Properties

        [Category("Appearance"), DefaultValue(true)]
        public virtual bool ButtonVisible
        {
            get { return m_ButtonVisible; }
            set
            {
                if (m_ButtonVisible != value)
                {
                    m_ButtonVisible = value;
                    m_Button.Visible = value;
                    this.OnControlLayoutChanged(EventArgs.Empty);
                    this.Invalidate();
                }
            }
        }

 		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool DroppedDown
		{
			get {return m_DroppedDown;}
			set {m_DroppedDown=value;}
		}

		[Category("Appearance"),DefaultValue(HorizontalAlignment.Left)]
		public virtual HorizontalAlignment TextAlign
		{
			get {return m_TextAlign;}
			set {
				if(m_TextAlign !=value)
				  m_TextAlign = value;
			    }
		}

		[Category("Behavior"),DefaultValue(false),Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsModified
		{
			get {return false;}
			set {;}
		}

        //[Category("McButton"),DefaultValue(Nistec.WinForms.ComboStyles.Button)]
        //public ComboStyles ComboStyle
        //{
        //    get {return m_ComboStyle;}
        //    set 
        //    {
        //      if(m_ComboStyle != value)
        //      {
        //          if(value != ComboStyles.Simple)
        //              defaultComboStyle=value;
        //          m_ComboStyle = value;
        //          this.OnComboStyleChanged(EventArgs.Empty);
        //          this.Invalidate ();
        //      }
        //    }
        //}

		#endregion

	}

}
