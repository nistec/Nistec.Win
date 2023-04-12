using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;
using MControl.Win32;
using MControl.Util;


namespace MControl.WinForms
{

    public class ChildControl
    {

    }

	public  class WindowListBase : System.Windows.Forms.NativeWindow
	{
		#region Members
		private System.ComponentModel.IContainer components = null;
		private bool m_Start = false;
		
		protected Control m_ParentMc = null;
		protected Form m_ParentForm = null;
		protected Form m_MdiParent = null;


		internal protected bool m_LockClose=false;
	
		// Events
		[Category("Behavior")]
		public event EventHandler PopupClosed;
		[Category("Behavior")]
		public event EventHandler PopupShow;
//		[Category("Behavior")]
//		public event EventHandler SelectionChanged;


		#endregion

		#region Constructors
		
		public WindowListBase()
		{
			InitializeComponent();
		}

        public WindowListBase(Control parent)
		{
			InitializeComponent();

			m_ParentMc = parent;
 		    
			m_ParentForm = m_ParentMc.FindForm();
			
			if(Form.ActiveForm != null && Form.ActiveForm.IsMdiContainer)
			{
				m_MdiParent = Form.ActiveForm;

				m_MdiParent.LostFocus += new System.EventHandler(this.ParentLostFocus);
				m_MdiParent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
				m_MdiParent.Move      += new System.EventHandler(this.ParentFormMoved);
			}
			
			if(m_ParentForm!=null)
			{
				m_ParentForm.LostFocus += new System.EventHandler(this.ParentLostFocus);
				m_ParentForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
				m_ParentForm.Move      += new System.EventHandler(this.ParentFormMoved);
			}
				
			if(!object.ReferenceEquals(m_ParentMc.Parent,m_ParentForm))
			{
				m_ParentMc.Parent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
				//m_ParentMc.Parent.LostFocus += new System.EventHandler(this.ParentLostFocus);
				//m_ParentMc.Parent.Move      += new System.EventHandler(this.ParentFormMoved);

			}
		
		}

		#endregion

		#region Dispose

        public override void DestroyHandle()
        {
            base.DestroyHandle();

            //m_ParentMc.LostFocus -= new System.EventHandler(this.ParentControlLostFocus);
            if (m_ParentForm != null)
            {
                m_ParentForm.LostFocus -= new System.EventHandler(this.ParentLostFocus);
                m_ParentForm.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
                m_ParentForm.Move -= new System.EventHandler(this.ParentFormMoved);
                if (!object.ReferenceEquals(m_ParentMc.Parent, m_ParentForm) && m_ParentMc.Parent != null)
                {
                    m_ParentMc.Parent.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
                }
            }

            if (m_MdiParent != null)
            {
                m_MdiParent.LostFocus -= new System.EventHandler(this.ParentLostFocus);
                m_MdiParent.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
                m_MdiParent.Move -= new System.EventHandler(this.ParentFormMoved);
            }

            if (components != null)
            {
                components.Dispose();
            }

            this.OnPopupClosed(e);

        }

		#endregion

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			// 
			// McPopUpBase
			// 
            //this.AutoScaleMode = AutoScaleMode.;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(16, 19);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "McPopUpBase";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Deactivate += new System.EventHandler(this.McPopupForm_Deactivate);

		}

		#endregion

		#region Events handles

		private void ParentFormMouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.Close();
		}

		private void ParentLostFocus(object sender, System.EventArgs e)
		{
			if(m_Start)
			{
				//if (! (this.Focused || this.m_ParentMc.Focused)) 
				this.Close();
			}
		}

		private void ParentFormMoved(object sender, System.EventArgs e)
		{
			if(m_Start){
				this.Close();
			}
		}

		#endregion

		#region Overrides

		[SecurityPermission(SecurityAction.LinkDemand)]
		[UseApiElements("Msgs.WM_MOUSEACTIVATE, MouseActivateFlags.MA_NOACTIVATE")]
		protected override void WndProc(ref Message m)
		{				
			if(m.Msg == (int)Msgs.WM_MOUSEACTIVATE)
			{
				m.Result = (IntPtr)MouseActivateFlags.MA_NOACTIVATE ;
				return;
			}

			base.WndProc(ref m);
		}

		#endregion

		#region Virtuals

		public virtual void PostMessage(ref Message m)
		{
		}

		public virtual void ClosePopupForm()
		{
			if (!this.LockClose)
			{
				if (this.m_ParentMc != null)
				{
					if (m_ParentForm != null)
					{
						m_ParentForm.Activate();
					}
				}
				base.Close();
			}
		}

  
		protected virtual void OnPopupClosed(EventArgs e)
		{
			if (this.PopupClosed != null)
			{
				this.PopupClosed(this, e);
			}
		}
 
		protected virtual void OnPopupShow(EventArgs e)
		{
			if (this.PopupShow != null)
			{
				this.PopupShow(this, e);
			}
		}

        private void DefChildWndProc(ref Message m)
        {
            if (this.childEdit != null)
            {
                NativeWindow window = (m.HWnd == this.childEdit.Handle) ? this.childEdit : this.childListBox;
                if (window != null)
                {
                    window.DefWndProc(ref m);
                }
            }
        }

		public virtual void ShowPopupForm()
		{
            Win32.RECT rect = new Win32.RECT();
            Win32.WinMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
            Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
            int x = Win32.WinAPI.SignedLOWORD(m.LParam);
            int y = Win32.WinAPI.SignedLOWORD(m.LParam);
            Point p = new Point(x, y);
            p = base.PointToScreen(p);
            //if (this.mouseEvents && !base.ValidationCancelled)
            //{
            //    this.mouseEvents = false;
            //    if (this.mousePressed)
            //    {
            //        if (!rectangle.Contains(p))
            //        {
            //            this.mousePressed = false;
            //            this.mouseInEdit = false;
            //            this.OnMouseLeave(EventArgs.Empty);
            //        }
            //        else
            //        {
            //            this.mousePressed = false;
            //            this.OnClick(new MouseEventArgs(MouseButtons.Left, 1, System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam), 0));
            //            this.OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, System.Windows.Forms.NativeMethods.Util.SignedLOWORD(m.LParam), System.Windows.Forms.NativeMethods.Util.SignedHIWORD(m.LParam), 0));
            //        }
            //    }
            //}
            this.DefChildWndProc(ref m);
            base.CaptureInternal = false;
            p = this.EditToComboboxMapping(m);
            this.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, p.X, p.Y, 0));

            if (!base.IsHandleCreated)
            {
                this.CreateHandle();
            }
            base.SendMessage(0x14f, value ? -1 : 0, 0);

			OnPopupShow(EventArgs.Empty);
		}

		public virtual void ShowPopUp(System.IntPtr hwnd)
		{
			ShowPopUp(hwnd,4);
		}

		[UseApiElements("ShowWindow")]
		public virtual void ShowPopUp(System.IntPtr hwnd,short state )
		{
			WinAPI.ShowWindow(hwnd,state);
			OnPopupShow(EventArgs.Empty);
		}
 
		[UseApiElements("ShowWindow")]
		public virtual void ShowPopUp(System.IntPtr hwnd,Win32.AnimateFlags flag)
		{
			WinAPI.AnimateWindow(hwnd, (uint)100,(uint) flag); 
			//WinAPI.AnimateWindow(m_ComboPopUp.Handle, (uint)100,(uint) Win32.AnimateFlags.AW_VER_POSITIVE); 
			OnPopupShow(EventArgs.Empty);
		}

		private void McPopupForm_Deactivate(object sender, EventArgs e)
		{
			this.ClosePopupForm();
		}
		#endregion		

		#region Properties

		public bool Start
		{
			get{return m_Start;}
			set{m_Start = value;}
		}

        public bool LockClose
        {
            get { return m_LockClose; }
            set { m_LockClose = value; }
        }

//		protected virtual void OnSelectionChanged(EventArgs e)
//		{
//			if(this.SelectionChanged != null)
//			{
//				this.SelectionChanged(this,e);
//			}
//		}

		//public abstract void Selection();
	
		//public abstract object SelectedItem{get;}

		public virtual object SelectedItem
		{
			get {return null;}
		}

		//public abstract int SelectedIndex{get;}

        //public abstract int SearchItem(string value);
        #endregion

    }
}
