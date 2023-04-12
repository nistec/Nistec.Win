using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Nistec.Win32;
using System.Runtime.InteropServices;


using Nistec.WinForms.Controls;
using Nistec.Win;
namespace Nistec.WinForms
{
    

	[DesignTimeVisible(false),ToolboxItem(false)]
	public class McPopUp : Component,ILayout,IDropDown	
	{

		#region Base Members
		public const int ScrollWidth=20;
        public const int DefaultItemHeight = 16;
        public const int ImageRectWidth = 24;
        private const int SpaceWidth = 16;

		private McPopUp.ComboListBox				internalList;
		private McPopUp.ComboPopUp					m_ComboPopUp;
		protected PopUpItemsCollection				m_MenuItems;

        private int                                 m_ItemHeight;
        private int                                 m_MaxDropDownItems;
		private  int								m_SelectedIndex;
		private  PopUpItem							m_SelectedItem;

		protected bool								m_DroppedDown;
		private IntPtr								m_mouseHookHandle;
		private GCHandle							m_mouseProcHandle;
		private bool								IsHooked;
		protected int								m_DropDownWidth;

		private bool sorted;
		private ImageList imageList;
		private Color foreColor;
		private Color backColor;
        private Font font;
        private RightToLeft rightToLeft;
		private System.Windows.Forms.Border3DStyle borderStyle;
		private DrawItemStyle drawItemStyle; 
        private Form form;
        private Form mdiForm;
        private object m_Tag;
        private bool m_TextOnly;
        private bool m_UseOwnerWidth;
        protected bool m_UseAsync;

        internal bool recalcWidth;
        
        //internal int runTimeImageWidth;

		#endregion 

		#region Event Members
		// Events
		[Category("PropertyChanged"), Description("OnSelectedValueChanged")]
		public event EventHandler SelectedValueChanged;
		[Category("PropertyChanged"), Description("OnSelectedItemClicked")]
        public event SelectedPopUpItemEventHandler SelectedItemClick;
		[Category("PropertyChanged"), Description("OnDropDownOcurred")]
		public event EventHandler DropDownOcurred;
		[Category("PropertyChanged"), Description("OnDropDownClosed")]
		public event EventHandler DropDownClosed;


		#endregion

		#region Constructors

		private Control owner;
		public McPopUp(Control parent):this()
		{
          this.owner=parent;
          if (owner != null)
          {
              font = owner.Font;
          }
		  //SetForm();
		}

        private McPopUp()
		{
			this.drawItemStyle=DrawItemStyle.Default;
			m_SelectedIndex= -1;
			m_MaxDropDownItems  = 8;
            m_ItemHeight = DefaultItemHeight;
			m_MenuItems=new PopUpItemsCollection(owner,this);
			m_MenuItems.ctlPopUp=this;        
            m_TextOnly=false;
            m_UseOwnerWidth=false;
            m_UseAsync= true;

            recalcWidth = true;
 
			this.internalList = new McPopUp.ComboListBox(this);
			this.BackColor=Color.White;
			this.ForeColor=Color.Black;
			this.borderStyle=Border3DStyle.Flat;
			//window= Win32.WinAPI.GetParent( Win32.WinAPI.GetFocus());

            if (owner == null)
            {
                //owner = internalList.FindForm();
                this.font = internalList.Font;
                owner = Form.ActiveForm;
            }

		}

		protected override void Dispose( bool disposing )
		{
			if(disposing)
			{
				if(form!=null)
				{
					form.Deactivate-=new EventHandler(form_Deactivate);
					form.Move-=new EventHandler(form_Move);
                    if (mdiForm != null)
                    {
                        mdiForm.Deactivate -= new EventHandler(form_Deactivate);
                        mdiForm.Move -= new EventHandler(form_Move);
                    }
                }

				PopUpItemsCollection collection1 = this.m_MenuItems;
				if (collection1 != null)
				{
					for (int num1 = 0; num1 < collection1.Count; num1++)
					{
						collection1[num1].Dispose();
					}
				}
			}

			base.Dispose( disposing );
		}

		#endregion

		#region Virtual Events


        protected virtual void OnSelectedItemClick(SelectedPopUpItemEvent e)
		{
			if(SelectedItemClick!=null)
				SelectedItemClick(this,e);
		}

		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			if (this.SelectedValueChanged!=null)
				this.SelectedValueChanged(this, e);
			if(SelectedItemClick!=null)
                OnSelectedItemClick(new SelectedPopUpItemEvent(this.SelectedItem, this.SelectedIndex));

		}

		protected virtual void OnDropDown(EventArgs e)
		{
			//m_DroppedDown=true;
			if(DropDownOcurred!=null)
				this.DropDownOcurred(this,e);
		}
		
		protected virtual void OnDropDownClosed(EventArgs e)
		{
			//m_DroppedDown=false;
			if(DropDownClosed!=null)
				this.DropDownClosed(this,e);
		}

		#endregion

		#region Mouse Hook Methods

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
			if(!IsHooked){return;}
			// Unhook   
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

		protected bool GetMouseHook(IntPtr mh,IntPtr wparam)
		{
			if(!m_DroppedDown)
				return false;

			if(mh == m_ComboPopUp.Handle || mh == internalList.Handle )//|| mh == this.Handle)
			{
				return false;
			}
			if(this.owner!=null)
			{
				if(mh == owner.Handle)
				{
					if((wparam==(IntPtr)513 ||wparam==(IntPtr)516) && m_DroppedDown )
					{
						m_ComboPopUp.Close();
					}
					return false;
				}
			}

			if((wparam==(IntPtr)513 ||wparam==(IntPtr)516) && m_DroppedDown )
			{
				m_ComboPopUp.Close();
				return false;
			}
  
			return true;
		}

		#endregion

		#region DropDown

        public void DoDropDown()
        {
            if (DroppedDown)
            {
                m_ComboPopUp.Close();
                return;
            }
            else
            {
                ShowPopUp();
            }
        }

        public void CloseDropDown()
        {
            if (DroppedDown)
            {
                m_ComboPopUp.Close();
            }
        }

		private void OnPopupClosed(object sender,System.EventArgs e)
		{
			this.EndHook(); 
			m_ComboPopUp.Dispose();
			//Invalidate(false);
//			if(form!=null)
//			{
//				form.Deactivate-=new EventHandler(form_Deactivate);
//				form.Move-=new EventHandler(form_Move);
//			}
		}

        
		/// <summary>
		/// Show PopUp with cursor position
		/// </summary>
		public  void ShowPopUp()
		{
			Point pt =Cursor.Position;// new Point(this.Left,this.Bottom+1 );
			ShowPopUp(pt);
		}

		[UseApiElements("ShowWindow")]
		public  void ShowPopUp(Point pt)
		{
			if(m_DroppedDown)
			{
				m_ComboPopUp.Close();
				return;
			}
            //runTimeImageWidth = 0;
            int itemHeight = CalcItemHeight();

            this.internalList.ItemHeight = Math.Max(itemHeight + 2, DefaultItemHeight);

            if (recalcWidth)
            { 
                SetDropDownWidth();// this.CalcDropDownWidth();
                recalcWidth = false;
            }
			//m_DropDownWidth=internalList.ComputeMaxItemWidth(-1);//this.m_DropDownWidth);

			this.OnDropDown(new System.EventArgs());

			//Point pt =Cursor.Position;// new Point(this.Left,this.Bottom+1 );
			m_ComboPopUp = new McPopUp.ComboPopUp(this);// this.BackColor , m_MaxDropDownItems, this.Text, this.m_DropDownWidth, this.internalList);
			m_ComboPopUp.Location =pt;// this.Parent.PointToScreen(pt);
			m_ComboPopUp.Closed += new System.EventHandler(this.OnPopupClosed);
            this.internalList.frm=m_ComboPopUp; 
			this.internalList.RightToLeft=this.rightToLeft;
            //WinAPI.AnimateWindow(m_ComboPopUp.Handle, (uint)100, (uint)Win32.AnimateFlags.AW_VER_POSITIVE); 
            WinAPI.ShowWindow(m_ComboPopUp.Handle, WindowShowStyle.ShowNormalNoActivate);
			//m_ComboPopUp.Focus();
			m_ComboPopUp.Start = true;
			//m_DroppedDown = true;
			this.StartHook() ;
            this.DropDownHandle();

		}

		public void ClosePopUp()
		{
			if(m_DroppedDown)
			{
				m_ComboPopUp.Close();
				OnDropDownClosed(EventArgs.Empty);
				return;
			}	
		}

        private void DropDownHandle()
        {

            if (form != null)
            {
                form.Deactivate -= new EventHandler(form_Deactivate);
                form.Move -= new EventHandler(form_Move);

                if (mdiForm != null)
                {
                    mdiForm.Deactivate -= new EventHandler(form_Deactivate);
                    mdiForm.Move -= new EventHandler(form_Move);
                }

            }
            else //if (form == null)
            {
                if (this.owner != null)
                    form = this.owner.FindForm();
                else
                    form = Form.ActiveForm;

                if (form != null)
                {
                    mdiForm = form.MdiParent;
                }

            }
            if (form != null)
            {
                form.Deactivate += new EventHandler(form_Deactivate);
                form.Move += new EventHandler(form_Move);
                if (mdiForm != null)
                {
                    mdiForm.Deactivate += new EventHandler(form_Deactivate);
                    mdiForm.Move += new EventHandler(form_Move);
                }
            }

         }

       //private int deactiveCount;
        private void form_Deactivate(object sender, EventArgs e)
        {
            //form.Deactivate -= new EventHandler(form_Deactivate);

            if (IsHooked)
            {
                Point p = this.m_ComboPopUp.PointToClient(Cursor.Position);

                if (this.m_ComboPopUp.ClientRectangle.Contains(p))
                {
                    return;
                }
                EndHook();
                m_ComboPopUp.Close();
 
            }
            //ClosePopUp();
        }

		private void form_Move(object sender, EventArgs e)
		{
            //form.Move -= new EventHandler(form_Move);
			ClosePopUp();
		}

		#endregion

		#region Combo Properties

        public bool TextOnly
        {
            get { return this.m_TextOnly; }
            set { this.m_TextOnly = value; }
        }
        public bool UseOwnerWidth
        {
            get { return this.m_UseOwnerWidth; }
            set { this.m_UseOwnerWidth = value; }
        }
        public bool UseAsync
        {
            get { return this.m_UseAsync; }
            set { this.m_UseAsync = value; }
        }
		public object Tag
		{
			get{return this.m_Tag;}
			set{this.m_Tag=value;}
		}

		public DrawItemStyle DrawItemStyle
		{
			get{return this.drawItemStyle;}
			set{this.drawItemStyle=value;}
		}

		[Category("Items"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public virtual PopUpItemsCollection MenuItems
		{
			get{ return m_MenuItems;}
		}

		public Color BackColor
		{
			get{return backColor;}
			set{//if(useStyleGuide)
                //value = LayoutManager.Layout.BackColorInternal;
                backColor=value;}
		}
		public Color ForeColor
		{
			get{return foreColor;}
			set{//if(useStyleGuide)
                //value = LayoutManager.Layout.ForeColorInternal;
                foreColor=value;}
		}
		public RightToLeft RightToLeft
		{
			get{return rightToLeft;}
			set{rightToLeft=value;}
		}

		public System.Windows.Forms.Border3DStyle BorderStyle
		{
			get{return borderStyle;}
			set{borderStyle=value;}
		}

        [Category("Behavior"), DefaultValue(DefaultItemHeight)]
		public int ItemHeight
		{
            get { return m_ItemHeight;/*internalList.ItemHeight;*/ }
			set
            {
                m_ItemHeight = value;
                internalList.ItemHeight=value;
            }
		}

		[Category("Behavior"), DefaultValue((string) null)]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				this.imageList = value;
				//base.Invalidate();
			}
		}

		[Description("SelectedIndex"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get
			{
				return m_SelectedIndex;
			}
		}
 
		[Description("SelectedItem"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Bindable(true)]
		public PopUpItem SelectedItem
		{
			get
			{
				return  m_SelectedItem; 
			}
		}

		internal void SetItem(string text)
		{
			int num1 = -1;
			if (m_MenuItems != null)
			{

				m_SelectedItem=m_MenuItems[text];
	  
				if (m_SelectedItem != null)
				{
					num1 = m_MenuItems.IndexOf(m_SelectedItem);
					m_SelectedIndex = num1;
				}
				else
				{
					m_SelectedIndex = -1;
				}
				this.OnSelectedValueChanged(EventArgs.Empty);
			}
			if (num1 != -1)
			{
				m_SelectedItem=null;
				m_SelectedIndex = num1;
			}

		}

		[Description("ComboBoxDropDownWidth"), Category("Behavior")]
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
				return m_DroppedDown;
			}
            set { }
		}
 
 
//		[Localizable(true), Description("ComboBoxItemHeight"), Category("Behavior")]
//		public int ItemHeight
//		{
//			get{return itemHeight;}
//			set{itemHeight=value;}
//		}
 
 
		[DefaultValue(8), Description("ComboBoxMaxDropDownItems"), Category("Behavior"), Localizable(true)]
		public int MaxDropDownItems
		{
			get
			{
				return  m_MaxDropDownItems;
			}
			set
			{
				if ((value < 1) || (value > 100))
				{
					object[] objArray1 = new object[] { "value", value.ToString(), "1", "100" } ;
					throw new ArgumentOutOfRangeException("InvalidBoundArgument");//, objArray1));
				}
				m_MaxDropDownItems = (short) value;
			}
		}
 
		[Category("Behavior"), DefaultValue(false), Description("ComboBoxSorted")]
		public bool Sorted
		{
			get{return sorted;}
			set{sorted=value;}
		}

		internal int GetImageRectWidth()
		{
		  if(this.drawItemStyle==DrawItemStyle.CheckBox)
			  return 14;
          else if(this.imageList==null)
              return 0;
          else if(this.imageList.Images.Count==0)
              return 0;
          else
              return this.imageList.ImageSize.Width+2;
		}

        private int CalcItemHeight()
        {
            if (this.drawItemStyle == DrawItemStyle.CheckBox)
                return DefaultItemHeight;

            int imageHeight = DefaultItemHeight;

            if (this.imageList != null)
                imageHeight=  this.imageList.ImageSize.Height;

            foreach (PopUpItem p in this.MenuItems)
            {
                if (p.Image != null)
                {
                    imageHeight = Math.Max(imageHeight, p.Image.Height);
                }
            }

            return Math.Max(ItemHeight, Math.Max(imageHeight, DefaultItemHeight));
        }

//		public int GetVisibleItems()
//		{
//			int cnt=0;
//			foreach(PopUpItem p in this.MenuItems)
//			{
//				cnt+=p.Visible ?  1:0;
//			}
//				return cnt;
//		}
//
//		public int CalcDropDownHieght()
//		{
//			return this.ItemHeight *  GetVisibleItems();
//		}
        public void SetDropDownWidth()
        {
            int ownerWidth=0;
            if (UseOwnerWidth && this.owner != null)
                ownerWidth = owner.Width;

            this.DropDownWidth = Math.Max(ownerWidth, (int) CalcDropDownWidth());
        }

		public float CalcDropDownWidth()
		{
			float width=0f;
			int i=0;
            //int imageSize = 0;
            //if (this.imageList != null)
            //    imageSize = this.imageList.ImageSize.Width;

			foreach(PopUpItem p in this.MenuItems)
			{
				float w= MeasureStringMin(p.Text,this.font).Width;
				width=Math.Max(width,w);
                //if (p.Image != null)
                //{
                //    imageSize =Math.Max(imageSize, p.Image.Width);
                //}
                i++;
			}
            int imageWidth = m_TextOnly ? 0 : ImageRectWidth;
            int scrall = (i > this.MaxDropDownItems) ? McPopUp.ScrollWidth : 0;
            return width + imageWidth + scrall + SpaceWidth;

		}

		internal void CalcDropDownWidth(string text)
		{
			float width= MeasureStringMin(text,owner==null? Control.DefaultFont: this.font).Width;
            int imageWidth = m_TextOnly ? 0:ImageRectWidth ;
			int scrall= (this.MenuItems.Count > this.MaxDropDownItems)? McPopUp.ScrollWidth : 0;
            int TotalWidth = (int)width + imageWidth + scrall + SpaceWidth;
			this.DropDownWidth=Math.Max(TotalWidth,this.DropDownWidth);
		}

		internal void CalcDropDownWidth(PopUpItem item)
		{
			float width= MeasureStringMin(item.Text,owner==null? Control.DefaultFont: this.font).Width;
            //int imageSize = 0;
            //if (item.Image != null)
            //{
            //    imageSize=item.Image.Width;
            //}
            //else if(item.ImageList!=null && this.imageList==null)
            //{
            //    this.imageList=item.ImageList;
            //    imageSize = imageList.ImageSize.Width;
            //}

            int imageWidth = m_TextOnly ? 0 : ImageRectWidth;
            int scrall = (this.MenuItems.Count > this.MaxDropDownItems) ? McPopUp.ScrollWidth : 0;
            int TotalWidth = (int)width + imageWidth + scrall + SpaceWidth;
			this.DropDownWidth=Math.Max(TotalWidth,this.DropDownWidth);
		}


		public SizeF MeasureStringMin(string s,Font stringFont)
		{
            System.Drawing.Graphics g = this.internalList.CreateGraphics();
			//System.Drawing.Graphics g=this.owner.CreateGraphics() ;
			// Measure string.
			SizeF stringSize = new SizeF();
			stringSize = g.MeasureString(s, stringFont);
			return stringSize;
			// Draw rectangle representing size of string.
			//g.DrawRectangle(new Pen(Color.Red, 1),	0.0F, 0.0F, stringSize.Width, stringSize.Height);
			// Draw string to screen.
			//g.DrawString(s,stringFont,	Brushes.Black,new PointF(0, 0));
		}

		#endregion

		#region class ComboListBox

		internal class ComboListBox : Nistec.WinForms.Controls.McListItems//Base
		{
			public ComboListBox(McPopUp ctl)
			{
                this.ItemHeight = DefaultItemHeight;
				this.parent=ctl;
				base.IntegralHeight=false;
				base.padding=26;
				base.DrawMode=DrawMode.OwnerDrawFixed;
				base.BackColor = ctl.BackColor;
				base.BorderStyle = System.Windows.Forms.BorderStyle.None;//.FixedSingle;
				base.RightToLeft  =ctl.RightToLeft ;
				base.Sorted=ctl.Sorted;
			}
			internal McPopUp parent;
			internal ComboPopUp frm;


			#region Overrides

			[UseApiElements("WM_LBUTTONUP, WM_LBUTTONDOWN, WM_MBUTTONDOWN")]
			protected override void WndProc(ref Message m)
			{
                base.WndProc(ref m);

				if(m.Msg == (int)Msgs.WM_LBUTTONUP)
				{

					if(frm.SelectedIndex==-1)return;
					object pitem = null;
                    if (parent.UseAsync)
                    {
                        frm.Close();
                    }
					if (frm.SelectedItem is string) 
					{
						pitem = (string)frm.SelectedItem.ToString();
						//int index = this.FindStringExact((string)pitem);
						//if (index > -1)owner.SelectedIndex = index;// ((ICombo)frm.Parent).SelectedIndex = index;
						parent.SetItem(pitem.ToString());
					}
                    if (!parent.UseAsync)
                    {
                        frm.Close();
                    }

					frm.Dispose();//.Close();
                    return;
				}

				if(m.Msg == (int)Msgs.WM_LBUTTONDOWN)
				{
                    return;
				}

				if(m.Msg == (int)Msgs.WM_MBUTTONDOWN)
				{
                    return;
				}
			}

			public void PostMessage(ref Message m)
			{
				base.WndProc(ref m);
			}

			protected override void OnDrawItem(DrawItemEventArgs e)
			{
				if(this.DrawMode!=DrawMode.Normal)
				{
					Graphics graphics1 = e.Graphics;
					Rectangle rectangle1 = e.Bounds;
					if ((e.State & DrawItemState.Selected) > DrawItemState.None)
					{
						rectangle1.Width--;
					}
					DrawItemState state1 = e.State;
					if ((e.Index != -1) && (this.Items.Count > 0))
					{
						PopUpItem pitem = parent.MenuItems[e.Index];
						//if(pitem.Visible)
						//{
							int num1=pitem.ImageIndex;  
							bool rtl= (this.RightToLeft==RightToLeft.Yes );
							DrawItemInternal(graphics1, rectangle1,pitem,this.parent.DrawItemStyle, state1,rtl,this.parent.TextOnly,pitem.Enabled);
						//}
					}
				}
			}

            private void DrawItemInternal(Graphics g, Rectangle bounds, PopUpItem ctl, DrawItemStyle drawStyle, DrawItemState state, bool rightToLeft,bool textOnly,bool enabled)
            {

                Rectangle rect;
                Rectangle rectImageX=Rectangle.Empty;
                
                float angle = 180f;

                IStyleLayout ctlStyle = parent.LayoutManager;
                if (textOnly)
                {
                    rect = bounds;
                }
                else
                {
                    if (rightToLeft)
                    {
                        rectImageX = new Rectangle(bounds.Width - McPopUp.ImageRectWidth - 1, bounds.Y, McPopUp.ImageRectWidth, bounds.Height);
                        angle = 0f;
                        rect = new Rectangle(bounds.X , bounds.Y, bounds.Width - McPopUp.ImageRectWidth-2, bounds.Height);
                    }
                    else
                    {
                        rectImageX = new Rectangle(bounds.X + 1, bounds.Y, McPopUp.ImageRectWidth, bounds.Height);
                        angle = 180f;
                        rect = new Rectangle(bounds.X + McPopUp.ImageRectWidth+2, bounds.Y, bounds.Width - McPopUp.ImageRectWidth-2, bounds.Height);
                    }
                }

                using (Brush brush1 = ctlStyle.GetBrushBack())
                {
                    g.FillRectangle(brush1, bounds);
                }

 

                if (drawStyle != DrawItemStyle.LinkLabel)
                {

                    //draw image rect
                    if (!textOnly && drawStyle != DrawItemStyle.CheckBox)
                    {
                        ctlStyle.FillGradientRect(g, rectImageX, angle);
                    }

                    //DrawItemRect(g, bounds, state);
                    if ((state & DrawItemState.Focus) != DrawItemState.None)
                    {
                        using (Brush brush = ctlStyle.GetBrushSelected())
                            g.FillRectangle(brush, bounds);
                        using (Pen pen = ctlStyle.GetPenFocused())
                            g.DrawRectangle(pen, bounds.X, bounds.Y+1, bounds.Width, bounds.Height - 2);
                    }
                    else if ((state & DrawItemState.Selected) != DrawItemState.None)
                    {
                        using (Brush brush = ctlStyle.GetBrushSelected())
                            g.FillRectangle(brush, bounds);
                        using (Pen pen = ctlStyle.GetPenHot())
                            g.DrawRectangle(pen, bounds.X, bounds.Y+1, bounds.Width, bounds.Height - 2);
                    }
                    //else
                    //{
                    //    using (Brush brush1 = ctlStyle.GetBrushBack())
                    //    {
                    //        g.FillRectangle(brush1, rect);
                    //    }
                    //}
                }

                if (drawStyle == DrawItemStyle.CheckBox)
                {

                    Image image = null;
                    if (ctl.Checked)
                        image = ResourceUtil.ExtractImage(Global.ImagesPath + "checked1.gif");
                    else
                        image = ResourceUtil.ExtractImage(Global.ImagesPath + "checked2.gif");

                    Rectangle rectCheck;
                    if (rightToLeft)
                        rectCheck = new Rectangle(bounds.Width - image.Width - 1, bounds.Y + ((bounds.Height - image.Height) / 2), image.Width, image.Height);
                    else
                        rectCheck = new Rectangle(bounds.X + 1, bounds.Y + ((bounds.Height - image.Height) / 2), image.Width, image.Height);

                    if (image != null)
                        g.DrawImage(image, rectCheck.X, rectCheck.Y, rectCheck.Width, rectCheck.Height);
                    //num1 = image.Width + 2;
                    //imageRectWidth=image.Width + 2;
                }
                else if (!textOnly && ctl.Image != null)
                {
                    Size imageSize = ctl.Image.Size;
                    Rectangle rectImage = new Rectangle((rectImageX.Width - imageSize.Width) / 2, bounds.Y +( (rectImageX.Height - imageSize.Height) / 2), imageSize.Width, imageSize.Height);
                    ctlStyle.DrawImage(g, rectImage, ctl.Image, System.Drawing.ContentAlignment.MiddleCenter, enabled);
                }
                else if (!textOnly &&  ctl.ImageList != null)
                {
                    ImageList imageList = ctl.ImageList;
                    Size imageSize = imageList.ImageSize;
                    //if (imageRectWidth >= imageSize.Width)
                    //{
                    int imageIndex = ctl.ImageIndex;

                    if ((imageIndex >= 0) && (imageIndex < imageList.Images.Count))
                    {
                        Rectangle rectImage = new Rectangle((rectImageX.Width - imageSize.Width) / 2, bounds.Y +( (rectImageX.Height - imageSize.Height) / 2), imageSize.Width, imageSize.Height);
                        if (enabled)
                            imageList.Draw(g, rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height, imageIndex);
                        else
                            ctlStyle.DrawImage(g, rectImage, imageList.Images[imageIndex], System.Drawing.ContentAlignment.MiddleCenter, enabled);

                        //num1 = imageList.ImageSize.Width + 2;
                    }
                    //}
                }

                if (ctl.StartGroup)
                {
                    //using (Pen pen = ctlStyle.())
                        g.DrawLine(Pens.Gray, rect.X+6, rect.Y, rect.Width, rect.Y);
                }

                using (StringFormat format1 = new StringFormat())
                {
                    if (rightToLeft)
                    {
                        format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                        format1.Alignment = StringAlignment.Far;
                    }
                    else
                    {
                        format1.Alignment = StringAlignment.Near;
                        //bounds.X += imageRectWidth;// + textStartPos;
                    }
                    format1.LineAlignment = StringAlignment.Center;
                    format1.FormatFlags = StringFormatFlags.NoWrap;
                    format1.Trimming = StringTrimming.EllipsisCharacter;

                    //bounds.Width -= imageRectWidth;// + textStartPos;
                    if (ctl.Text == null)
                    {
                        return;
                    }
                    Font font = Control.DefaultFont;
                    if (drawStyle == DrawItemStyle.LinkLabel)
                    {
                        if (((state & DrawItemState.Focus) != DrawItemState.None) || ((state & DrawItemState.Selected) != DrawItemState.None))
                        {
                            font = new Font(font, FontStyle.Underline);
                        }
                    }

                    using (Brush brush1 = ctlStyle.GetBrushText(), brush2 = ctlStyle.GetBrushDisabled())
                    {
                        if (enabled)
                            g.DrawString(ctl.Text, font, brush1, (RectangleF)rect, format1);
                        else
                            g.DrawString(ctl.Text, font, brush2, (RectangleF)rect, format1);

                    }
                }
            }


			#endregion

		}

		#endregion

		#region class ComboPopUp

		internal class ComboPopUp : McPopUpBase
		{
			#region Members
			private System.ComponentModel.IContainer components = null;
			private new const int Padding=4;
	        private McPopUp.ComboListBox internalList;
			public event SelectionChangedEventHandler SelectionChanged = null;
            McPopUp Mc;
			#endregion
	
			#region Constructors

			public ComboPopUp(McPopUp ctl) //: base(parent)
			{
				Mc=ctl;
				this.SuspendLayout();
				InitializeComponent();
		
				internalList=Mc.internalList;

				if(internalList==null)
					internalList=new McPopUp.ComboListBox(Mc);
				else
					internalList.Items.Clear();
		
				Mc.MenuItems.CopyTo(internalList.Items);
	
				internalList.BackColor = Mc.backColor;
				internalList.Dock = System.Windows.Forms.DockStyle.Fill;
				internalList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.internalList_KeyUp);
//				internalList.SelectedIndexChanged  += new System.EventHandler (this.internalList_SelectedChanged);
//				internalList.SelectedValueChanged  += new System.EventHandler (this.internalList_SelectedChanged);

				internalList.Visible=true;

				int cnt=internalList.Items.Count;
				int visibleItems=Mc.MaxDropDownItems;
				
				string selectedText="";
				if(Mc.SelectedItem!=null)
					selectedText=Mc.SelectedItem.Text;
		  
				if(cnt==0)
					visibleItems=0;
				if(visibleItems > cnt)
				{
					visibleItems = cnt;
				}
                //if(Mc.ImageList!=null)
                //{
                //    //internalList.ImageList=Mc.ImageList;
                //    internalList.ItemHeight = Math.Max(Mc.ItemHeight, Mc.ImageList.ImageSize.Height);
                //}
                //else 
                //    internalList.ItemHeight = Mc.ItemHeight;

				this.Height =(internalList.ItemHeight * visibleItems)+4+Padding;
				this.BackColor = Mc.BackColor;
				int width=Mc.DropDownWidth;
				if(width>0)
					this.Width=width;
				else 
					this.Width=internalList.ComputeMaxItemWidth(width);
			    		       
				this.Controls.Add(internalList);
				this.ResumeLayout(false);
				int index = internalList.FindStringExact(selectedText);
				if(index > -1)
				{
					internalList.SelectedIndex = index;
				}
				internalList.Focus();
				if(Mc.drawItemStyle==DrawItemStyle.LinkLabel)
				{
			 		internalList.Cursor=System.Windows.Forms.Cursors.Hand;
				}
				
			}

			#endregion

			#region Dispose

            //delegate void DisposeCallBack(bool disposing);

			protected override void Dispose( bool disposing )
			{
				if( disposing )
				{
                    //if (internalList.InvokeRequired)
                    //{
                    //    internalList.Invoke(new DisposeCallBack(Dispose), disposing);
                    //}
                    //else
                    //{
                    //    internalList.Dispose();
                    //}

					if (components != null) 
					{
						components.Dispose();
					}
				}
				base.Dispose( disposing );
			}

			#endregion

			#region Designer generated code
			private void InitializeComponent()
			{
				this.MinimumSize = new System.Drawing.Size(1, 1);
				this.DockPadding.All=Padding;
				this.Name = "ComboPopUp";
			}
			#endregion

			#region Overrides

 
			protected override void OnClosed(System.EventArgs e)
			{

				internalList.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.internalList_KeyUp);
				//internalList.SelectedIndexChanged  -= new System.EventHandler (this.internalList_SelectedChanged);
				//internalList.SelectedValueChanged  -= new System.EventHandler (this.internalList_SelectedChanged);
				internalList.Visible=false; 
		
				this.Controls.Remove(internalList);

                //Nistec.Win32.WinAPI.AnimateWindow(this.Handle, (uint)100, (uint)Win32.AnimateFlags.AW_HIDE ); 

				base.OnClosed(e);
			}

			protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
			{
				base.OnPaint(e);
				Rectangle rect=this.ClientRectangle;
				ControlPaint.DrawBorder3D(e.Graphics,rect,Mc.borderStyle);
			}

			public override void PostMessage(ref Message m)
			{
				Message msg = new Message();
				msg.HWnd    = internalList.Handle;
				msg.LParam  = m.LParam;
				msg.Msg     = m.Msg;
				msg.Result  = m.Result;
				msg.WParam  = m.WParam;

				internalList.PostMessage(ref msg);
			}

			#endregion

			#region Methods
        
			public int SelectedIndex
			{
				get 
				{
					if(internalList.SelectedIndex == -1)
						return -1;
					if(!Mc.MenuItems[internalList.SelectedIndex].Enabled)
						return -1;

					return internalList.SelectedIndex;
				}
			}

			public override object SelectedItem
			{
				get {return internalList.SelectedItem;}
			}

			public int SearchItem(string value)
			{
				int index = internalList.FindString(value);
				if (index > -1) internalList.SelectedIndex = index;

				return index;
			}

			private void internalList_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
			{
				if(e.KeyData == Keys.Escape)
				{
					this.Close();
				}

				if(e.KeyData == Keys.Enter)
				{
					OnSelectionChanged();
					this.Close();
				}
			}

			private void internalList_SelectedChanged(object sender ,System.EventArgs e)
			{
				OnSelectionChanged();
			}

			private void OnSelectionChanged()
			{
				if(internalList.SelectedIndex == -1)
					return;
				if(!Mc.MenuItems[internalList.SelectedIndex].Enabled)
					return;
				if(internalList.SelectedItem != null)
				{
					if(this.SelectionChanged != null)
					{
						this.SelectionChanged(this,new SelectionChangedEventArgs(internalList.Text));
					}
				}
			}

			#endregion

		}
		#endregion

		#region ILayout Members

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Style"), DefaultValue(ControlLayout.Visual)]
        public virtual ControlLayout ControlLayout
        {
            get { return ControlLayout.Visual; }
            set
            {
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
					//if (this.m_StylePainter != null)
						//this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					m_StylePainter = value;
					//if (this.m_StylePainter != null)
					//	this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					//OnStylePainterChanged(EventArgs.Empty);
				}
			}
		}

		[Browsable(false)]
		public virtual IStyleLayout LayoutManager
		{
			get
			{
				if(this.owner is ILayout)
					return ((ILayout)this.owner).LayoutManager;
				if(this.m_StylePainter!=null)
					return this.m_StylePainter.Layout as IStyleLayout;
				else
					return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;
			}
		}

		public void SetStyleLayout(Nistec.WinForms.Styles value)
		{
			// TODO:  Add McPopUp.SetStyleLayout implementation
		}

		void Nistec.WinForms.ILayout.SetStyleLayout(StyleLayout value)
		{
			// TODO:  Add McPopUp.Nistec.WinForms.ILayout.SetStyleLayout implementation
		}

		#endregion
	}
	

}