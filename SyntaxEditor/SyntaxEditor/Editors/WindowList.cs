using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;   
using System.Collections;
using System.Data; 
using System.Runtime.InteropServices;
using System.Collections.Generic;

using MControl.Win32;
using MControl.Util;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Globalization;
using MControl.WinForms.Controls;
using MControl.WinForms;

namespace MControl.SyntaxEditor
{
	/// <summary>
	/// Summary description for WindowList.
	/// </summary>

	[System.ComponentModel.ToolboxItem(false)]
	public class IntelliWindow : McPopUpBase
	{
 
		#region Members
	
		private McListBox			internalList;
		private Control				Mc;
        private Form OwnerForm;

        //public event EventHandler PopUpShow;
        //public event EventHandler PopUpClosed;

		//private  ComboBoxStyle		m_DropDownStyle;
		private  int				m_DropDownWidth;
		private  bool				m_DroppedDown;
		private  int				m_MaxDropDownItems;
        private bool m_UpdateCaller;
        private bool m_ParentInitilaized;
        private bool m_SearchExactOnOpen;

  

 
        string m_SelectedText;
        private ImageList imageList1;
        private IContainer components;
        string m_ControlText;

 		#endregion 

		#region Event Members
		// Events
        [Category("PropertyChanged"), Description("ListControlOnSelectionChanged")]
        public event SelectionChangedEventHandler SelectionChanged;

		#endregion

		#region Constructor

        internal IntelliWindow()
        {
            InitComponents();
        }

        public IntelliWindow(Control parent)
		{
			Mc=parent;
			InitComponents();
		}

		private void InitComponents()
		{
			m_MaxDropDownItems  = 8;
            m_UpdateCaller = false;
            m_ParentInitilaized = false;
            m_SearchExactOnOpen = false;

			InitializeComponent();
            this.ImageList = this.imageList1;
            //base.ItemHeight = 15;

 		}

    
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.internalList != null)
                {
                    //this.internalList.DrawItem -= new DrawItemEventHandler(internalList_DrawItem);
                    this.internalList.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.mListBox_KeyUp);
                    this.internalList.DoubleClick -= new EventHandler(internalList_DoubleClick);
                    this.internalList.Dispose();
                }
                if (Mc != null)
                {
                    Mc.KeyDown -= new KeyEventHandler(Mc_KeyDown);
                    OwnerForm.Move -= new EventHandler(ParentForm_Move);
                }

            }
            base.Dispose(disposing);
        }

        private void InitParent()
        {
            if (Mc != null && !m_ParentInitilaized)
            {
                //Mc.KeyDown += new KeyEventHandler(Mc_KeyDown);
                //Mc.KeyPress += new KeyPressEventHandler(Mc_KeyPress);
                OwnerForm = Mc.FindForm();
                OwnerForm.Move += new EventHandler(ParentForm_Move);
                //OwnerForm.KeyDown += new KeyEventHandler(OwnerForm_KeyDown); 
                m_ParentInitilaized = true;
            }
        }

 

        void OwnerForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        void ParentForm_Move(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CommandKey(Keys key)
        {
            switch (key)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Enter:
                    OnSelected();
                    break;
                case Keys.Up:
                    MovePrev();
                    break;
                case  Keys.Down:
                    MoveNext();
                    break;
            }
        }

        private void MoveNext()
        {
            int index = this.internalList.SelectedIndex;
            if (index < this.internalList.Items.Count-1)
            {
                this.internalList.SelectedIndex = index + 1;
            }
        }
        private void MovePrev()
        {
            int index = this.internalList.SelectedIndex;
            if (index >0)
            {
                this.internalList.SelectedIndex = index - 1;
            }

        }

         private bool IsMouseOnScroll(MControl.Win32.POINT wpt)
        {
            int x = wpt.x;

            if (this.ListItems.Count <= MaxDropDownItems)
            {
                return false;
            }

            Point pt = new Point(Mc.Left, Mc.Bottom + 1);
            Point p = Mc.Parent.PointToScreen(pt);

            //Point pt = new Point(Mc.Left, Mc.Bottom + 1);
            //Point p = this.internalList.PointToScreen(pt);
            int right = p.X + this.Width;

            if (x >= (right - 30) && x <= right)
            {
                return true;
            }
            return false;
        }

		#endregion

		#region Designer generated code
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntelliWindow));
            this.internalList = new MControl.WinForms.McListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // internalList
            // 
            this.internalList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.internalList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.internalList.IntegralHeight = false;
            this.internalList.ItemHeight = 15;
            this.internalList.Location = new System.Drawing.Point(0, 0);
            this.internalList.Name = "internalList";
            this.internalList.ReadOnly = false;
            this.internalList.Size = new System.Drawing.Size(60, 19);
            this.internalList.TabIndex = 0;
            this.internalList.DoubleClick += new System.EventHandler(this.internalList_DoubleClick);
            this.internalList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mListBox_KeyUp);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "method.gif");
            this.imageList1.Images.SetKeyName(1, "Properties.png");
            this.imageList1.Images.SetKeyName(2, "field.gif");
            this.imageList1.Images.SetKeyName(3, "member.gif");
            this.imageList1.Images.SetKeyName(4, "class.gif");
            this.imageList1.Images.SetKeyName(5, "event.gif");
            this.imageList1.Images.SetKeyName(6, "reference.gif");
            this.imageList1.Images.SetKeyName(7, "field1.gif");
            this.imageList1.Images.SetKeyName(8, "methodBlue.gif");
            this.imageList1.Images.SetKeyName(9, "method2.gif");
            this.imageList1.Images.SetKeyName(10, "properties.gif");
            // 
            // IntelliWindowList
            // 
            this.ClientSize = new System.Drawing.Size(60, 19);
            this.Controls.Add(this.internalList);
            this.MinimumSize = new System.Drawing.Size(60, 10);
            this.Name = "IntelliWindowList";
            this.ResumeLayout(false);

		}
		#endregion

		#region Virtual Events
 
        void internalList_DoubleClick(object sender, EventArgs e)
        {
            this.OnSelected();
        }

		#endregion

		#region Data Properties

        public ListItems ListItems
        {
            get { return internalList.ListItems; }
        }

        //[Category("Data"),
        //Editor("System.Windows.Forms.Design.StringCollectionEditor,System.Design", 
        //    "System.Drawing.Design.UITypeEditor,System.Drawing"),
        //DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //internal McListBox.ObjectCollection Items
        //{
        //    get {return internalList.Items;}
        //}

		[Browsable(false)]
		internal McListBox InternalList
		{
			get {return internalList;}
		}

        [Browsable(false)]
        public new bool InvokeRequired
        {
            get { return internalList.InvokeRequired; }
        }

		#endregion

		#region Combo Properties

        [Description("SelectedText"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get
            {
                return m_SelectedText;
            }
        }
		[Description("ComboBoxSelectedIndex"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get
			{
				return internalList.SelectedIndex;
			}
			set
			{
                if (!DesignMode)
                {
                    internalList.SelectedIndex = value;
                }
			}
		}
 
  		[Description("ComboBoxDropDownWidth"),DefaultValue(0), Category("Behavior")]
		public int DropDownWidth
		{
			get
			{
				return m_DropDownWidth;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("InvalidArgument");//, objArray1));
				}
				if (m_DropDownWidth != value)
				{
					m_DropDownWidth=value;
				}
			}
		}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("ComboBoxDroppedDown")]
        public bool DroppedDown
        {
            get
            {
                if (Mc.IsHandleCreated)
                {
                    return m_DroppedDown;
                }
                return false;
            }
            set
            {
                if (m_DroppedDown)
                {
                    this.Close();
                    return;
                }
                if (value)
                    ShowPopUp();
            }
        }
 
		[Category("Behavior"), Description("ComboBoxIntegralHeight"), DefaultValue(true), Localizable(true)]
		public bool IntegralHeight
		{
			get
			{
				return internalList.IntegralHeight;
			}
			set
			{
				internalList.IntegralHeight=value;
			}
		}
 
		[Localizable(true),DefaultValue(13), Description("ComboBoxItemHeight"), Category("Behavior")]
		public int ItemHeight
		{
			get
			{
				return internalList.ItemHeight;  
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("InvalidArgument");//, objArray1));
				}
					internalList.ItemHeight=value;
			}
		}
  
		[DefaultValue(8), Description("ComboBoxMaxDropDownItems"), Category("Behavior"), Localizable(true)]
		public int MaxDropDownItems
		{
			get
			{
				return m_MaxDropDownItems;
			}
			set
			{
				if ((value < 1) || (value > 100))
				{
					throw new ArgumentOutOfRangeException("InvalidBoundArgument");//, objArray1));
				}
				m_MaxDropDownItems = (short) value;
			}
		}
 
        //[Category("Behavior"), Description("ComboBoxMaxLength"), Localizable(true), DefaultValue(0)]
        //public int MaxLength
        //{
        //    get
        //    {
        //        return m_MaxLength;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            value = 0;
        //        }
        //        if (m_MaxLength != value)
        //        {
        //            m_MaxLength= value;
        //        }
        //    }
        //}
 


        [Category("Behavior"), DefaultValue((string)null)]
        public ImageList ImageList
        {
            get
            {
                return this.internalList.ImageList;
            }
            set
            {
                this.internalList.ImageList = value;
                //if(value!=null)
                //{
                //    imageSize = m_ImageList.ImageSize;

                //}
            }
        }
        [Category("Behavior"), DefaultValue(false)]
        public bool HotTrack
        {
            get
            {
                return this.internalList.HotTrack;
            }
            set
            {
                this.internalList.HotTrack = value;
                base.Invalidate();
            }
        }

        public int DefaultImageIndex
        {
            get { return internalList.DefaultImageIndex; }
            set { internalList.DefaultImageIndex = value; }
        }

        public bool UpdateCaller
        {
            get { return m_UpdateCaller; }
            set { m_UpdateCaller = value; }
        }
        public bool SearchExactOnOpen
        {
            get { return m_SearchExactOnOpen; }
            set { m_SearchExactOnOpen = value; }
        }
		#endregion

		#region PopUp

        public void DoDropDown(string[] items, Point pt)
        {
            this.ListItems.AddRange(items);
            DoDropDown(pt);
        }
        public void DoDropDown(ListItems items, Point pt)
        {
            DoDropDown(items,"",pt);
        }
        public void DoDropDown(ListItems items, string contrlText, Point pt)
        {
            this.ListItems.AddRange(items);
            this.m_ControlText = contrlText;
            DoDropDown(pt);
        }

        public void DoDropDown(Point pt)
        {
            if (m_DroppedDown)
            {
                Close();
                return;
            }

            ShowPopUp(pt);
        }

        public void DoDropDown(string[] items)
        {
            this.ListItems.AddRange(items);
            DoDropDown();
        }
        public void DoDropDown(ListItems items)
        {
            DoDropDown(Mc, items, "");
        }
        public void DoDropDown(ListItems items, string contrlText)
        {
            DoDropDown(Mc, items, contrlText);
        }
        public void DoDropDown(Control parent, ListItems items)
        {
            DoDropDown(parent, items,"");
        }
        public void DoDropDown(Control parent, ListItems items, string contrlText)
        {
            if (parent != Mc)
            {
                Mc = parent;
                m_ParentInitilaized = false;
            }
            this.ListItems.AddRange(items);
            this.m_ControlText = contrlText;
            DoDropDown();
        }

		public void DoDropDown()
		{
			if(m_DroppedDown)
			{
				Close();
				return;
			}	
		
			ShowPopUp();			
		}

        //protected override void OnPopupClosed(System.EventArgs e)
        //{
        //    m_DroppedDown = false;
        //    //this.Dispose();
        //    internalList.Visible =false;  
        //    Mc.Invalidate(false);
        //    base.OnPopupClosed(e);
        //    //if(SelectionChanged !=null) 
        //    //    this.SelectionChanged (this,e);
        //    //if(PopUpClosed!=null)
        //    //    PopUpClosed(Mc,e);
        //}

        [UseApiElements("ShowWindow")]
        protected void ShowPopUp()
        {
            Point pt = new Point(Mc.Left, Mc.Bottom + 1);
            ShowPopUp(pt);
        }
	
		[UseApiElements("ShowWindow")]
        protected void ShowPopUp(Point pt)
		{
            InitParent();

			m_DroppedDown = true;

			int cnt=this.ListItems.Count;
			int visibleItems=this.MaxDropDownItems;

            string selectedText = "";
            if (SearchExactOnOpen)
            {
                selectedText = string.IsNullOrEmpty(m_ControlText) ? Mc.Text : m_ControlText;
            }

			if(cnt==0)
				visibleItems=0;
			if(visibleItems > cnt)
			{
				visibleItems = cnt;
			}

			this.Height =(this.internalList.ItemHeight * visibleItems)+4;

            if (this.DropDownWidth == 0)
               this.Width = this.ListItems.MaxWidth;// this.Width = Mc.Width;
            else
                this.Width = this.DropDownWidth;

 			//if(Items.Count <=0)
			//	return;
	
            //Point pt = new Point(Mc.Left,Mc.Bottom+1 );

            this.Location = Mc.Parent.PointToScreen (pt);
			
            //this.Closed += new System.EventHandler(this.OnPopupClosed);
            WinAPI.ShowWindow(this.Handle, 4);
			//this.internalList.Focus();
            StartHook();
            this.Start = true;
            if (selectedText.Length > 0)
            {
                int index = this.internalList.FindStringExact(selectedText);
                if (index > -1)
                {
                    this.internalList.SelectedIndex = index;
                }
            }
            else//if (selectedText.Length == 0)
            {
                this.internalList.SelectedIndex = 0;
            }
            base.OnPopupShow(EventArgs.Empty);
		}

   
		#endregion
		
		#region Internal helpers

		internal bool IsMatchHandle(IntPtr mh)
		{
			if(!m_DroppedDown)
				return false;
			
			if(this.IsHandleCreated)
			{
				if(mh==this.Handle)
					return true;
			}

			if(internalList.IsHandleCreated)
			{
				if (mh==internalList.Handle)
					return true;

			}
			return false;
		}

		internal IntPtr GetComboPopUpHandle()
		{
			if(this.IsHandleCreated)
				return (IntPtr)this.Handle;
			return IntPtr.Zero;
		}

		internal IntPtr GetListHandle()
		{
			if(internalList.IsHandleCreated)
				return (IntPtr)internalList.Handle;
			return IntPtr.Zero;
		}


		public int FindString(string s)
		{
            int index = internalList.FindString(s);
            if (index != -1)
            {
                internalList.SelectedIndex = index;
            }
            return index;
        }

		public int FindString(string s,int startIndex)
		{
            int index = internalList.FindString(s, startIndex);
            if (index != -1)
            {
                internalList.SelectedIndex = index;
            }
            return index;
        }

 
		#endregion

		#region Events handlers

		private void mListBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            switch (e.KeyData)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Enter:
                    OnSelected();
                    //this.Close();
                    break;
            }
		}

        protected virtual void OnSelected()
        {
            object item = this.internalList.SelectedItem;
            if (item != null)
            {
                m_SelectedText = (string)item;
                if (UpdateCaller)
                {
                    Mc.Text = m_SelectedText;
                }
                if (this.SelectionChanged != null)
                {
                    this.SelectionChanged(this, new SelectionChangedEventArgs(m_SelectedText));
                }
            }
            this.Close();
        }

		protected override void OnClosed(System.EventArgs e)
		{
			EndHook();
			this.internalList.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.mListBox_KeyUp);
			//this.internalList.Visible=false; 
		
			this.Controls.Remove(this.internalList);
			base.OnClosed(e);
		}

		#endregion

		#region Mouse Hook Methods

		private IntPtr					m_mouseHookHandle;
		private GCHandle				m_mouseProcHandle;
		private bool					IsHooked;

		protected virtual void StartHook()
		{ 
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
			int res=GetMouseHook(mh,wparam);
            if (res > 0)
            {
                if (res == 2)
                    this.OnSelected();

                this.Close();

                return mh.hwnd;
            }
             
			return WinAPI.CallNextHookEx( m_mouseHookHandle, code, wparam, lparam );
		}

        protected virtual int GetMouseHook(MOUSEHOOKSTRUCT mh, IntPtr wparam)
		{

			Msgs msg = (Msgs) wparam.ToInt32();
//			if ((msg == Msgs.WM_LBUTTONDOWN) || (msg == Msgs.WM_RBUTTONDOWN)|| (msg == Msgs.WM_NCLBUTTONDOWN))
            if (this.HotTrack)
            {

                if ((msg == Msgs.WM_LBUTTONUP) || (msg == Msgs.WM_RBUTTONUP) || (msg == Msgs.WM_NCLBUTTONUP))
                {

                    if ((mh.hwnd == this.Handle) || (mh.hwnd == this.internalList.Handle))
                    {
                        if (IsMouseOnScroll(mh.pt))
                            return 0;
                        //EndHook();
                        //this.OnSelected();
                        return 2;
                    }
                    else
                    {
                        //this.Close();
                        return 1;
                    }
                }
            }
            else
            {
                if (msg == Msgs.WM_MBUTTONDBLCLK)
                {

                    if ((mh.hwnd == this.Handle) || (mh.hwnd == this.internalList.Handle) || (mh.hwnd == this.Mc.Handle))
                    {
                        //if (IsMouseOnScroll(mh.pt))
                        //    return false;
                        //this.OnSelected();
                        return 0;
                    }
                 }
                 else if ((msg == Msgs.WM_LBUTTONUP) || (msg == Msgs.WM_RBUTTONUP) || (msg == Msgs.WM_NCLBUTTONUP))
                 {
                     if ((mh.hwnd == this.Handle) || (mh.hwnd == this.internalList.Handle) || (mh.hwnd == this.Mc.Handle))
                     {
                     }
                     else
                     {
                         //this.Close();
                         return 1;
                     }
                 }


            }
			return 0;
		}

		#endregion

	}

}
