using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.ComponentModel;

using System.Collections;
using System.Security;
using System.Security.Permissions;
using System.Globalization;
using System.Data;
using System.Reflection;


using Nistec.Win32;
using Nistec.Data;
using Nistec.Collections;
  
namespace Nistec.WinForms
{
	[ToolboxItem(true),Designer(typeof(Design.McDesigner))]
	[ToolboxBitmap(typeof(McListBox), "Toolbox.ListBox.bmp")]
    public class McListBox : ListBox, ILayout, IMcList, IBind, IComboList
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
        //    //    Nistec.Util.Net.netWinMc.NetFram(this.Name, "Mc"); 
        //    //}
        //}

		#endregion

		#region Members
		// Fields
		private bool hotTrack;
		private ImageList imageList;
		//private bool useFirstImage;
		DrawItemStyle drawItemStyle;
        private string m_DefaultValue;

        private int defaultImageIndex;
        private ItemsCollection m_ListItems;
  
		#endregion

		#region Constructor

		public McListBox(): base()
		{
            defaultImageIndex = 0;

			base.BorderStyle=BorderStyle.FixedSingle; 
			this.drawItemStyle=DrawItemStyle.Default;
			//this.useFirstImage = false;
			this.hotTrack = false;
			this.imageList = null;
			//this.DrawMode = DrawMode.OwnerDrawVariable;
            //this.ResizeRedraw=true;
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 
		}

        //internal McListBox(bool net): this()
        //{
        //    m_netFram=net;
        //}

        protected override void Dispose(bool disposing)
        {
            if (m_ListItems != null)
            {
                m_ListItems.ItemAdded -= new ItemsCollectionChange(m_ListItems_ItemAdded);
                m_ListItems.ItemRemoved -= new ItemsCollectionChange(m_ListItems_ItemRemoved);
                m_ListItems.ItemsCleared -= new EventHandler(m_ListItems_ItemsCleared);
            }

            base.Dispose(disposing);
        }
		#endregion

		#region WndProc

		protected override void WndProc(ref Message m)
		{
            try
            {
                if (this.BorderStyle != BorderStyle.FixedSingle)
                {
                    base.WndProc(ref m);
                    return;
                }

                IntPtr hDC = IntPtr.Zero;
                Graphics gdc = null;
                switch (m.Msg)
                {
                    //				case WinMsgs.WM_MOUSEHOVER:	
                    //				case WinMsgs.WM_MOUSEMOVE:	
                    //					base.WndProc(ref m);
                    //					hDC = WinAPI.GetWindowDC(this.Handle);
                    //					gdc = Graphics.FromHdc(hDC);
                    //					PaintFlatControl(gdc,true);
                    //					WinAPI.ReleaseDC(m.HWnd, hDC);
                    //					gdc.Dispose();	
                    //					break;
                    //				case WinMsgs.WM_SETFOCUS :	
                    //				case WinMsgs.WM_KILLFOCUS :	
                    //				case WinMsgs.WM_MOUSELEAVE:	
                    case WinMsgs.WM_PAINT:
                        base.WndProc(ref m);
                        hDC = WinAPI.GetWindowDC(this.Handle);
                        gdc = Graphics.FromHdc(hDC);
                        PaintFlatControl(gdc, false);
                        WinAPI.ReleaseDC(m.HWnd, hDC);
                        gdc.Dispose();
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            catch { }
		}

		private void PaintFlatControl(Graphics g,bool hot)
		{
			Rectangle rect = new Rectangle(0, 0, this.Width-1, this.Height-1);
			LayoutManager.DrawBorder(g,rect,false,this.Enabled,this.Focused,hot);
			//LayoutManager.DrawMcEdit (g,rect,this,this.BorderStyle ,Enabled ,this.ContainsFocus,hot,false );


//			if (! this.Enabled  )
//				ControlPaint.DrawBorder(g, rect, m_Style.DisabledColor , ButtonBorderStyle.Solid  );
//			else if (Focused )
//				ControlPaint.DrawBorder(g, rect, m_Style.FocusedColor , ButtonBorderStyle.Solid  );
//			else if(hot)
//				ControlPaint.DrawBorder(g, rect, m_Style.BorderHotColor , ButtonBorderStyle.Solid  );
//			else 
//				ControlPaint.DrawBorder(g, rect, m_Style.BorderColor , ButtonBorderStyle.Solid  );
		}

//		[Browsable(false)]
//		public override Color BackColor
//		{
//			get
//			{
//				return base.BackColor;//LayoutManager.Layout.BackColorInternal;//base.BackColor;
//			}
//			set
//			{
//				base.BackColor =value;//LayoutManager.Layout.BackColorInternal;
//			}
//		}
//

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            //if (!DesignMode && m_ListItems != null && m_ListItems.Count > 0)
            //{
            //    this.Items.Clear();
            //    this.Items.AddRange(this.m_ListItems.ToStringArray());
            //}
        }


		#endregion

		#region DrawItems

		private DrawItemEventHandler GetDrawItemHandler()
		{
			FieldInfo info1 = typeof(System.Windows.Forms.ListBox).GetField("EVENT_DRAWITEM", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Static);
			return (DrawItemEventHandler) base.Events[info1.GetValue(null)];
		}

		protected virtual void OnDrawItemInternal(DrawItemEventArgs e)
		{
			DrawItemEventHandler handler1 = this.GetDrawItemHandler();
			if (handler1 == null)
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
					//int num1 = this.UseFirstImage ? 0 : e.Index;
					this.LayoutManager.DrawItem(graphics1, rectangle1,this, state1, this.GetItemText(this.Items[e.Index]));//,num1);
	
				}
			}
			else
			{
				handler1(this, e);
			}
		}
        protected virtual void OnDrawListItem(DrawItemEventArgs e)
        {
            Graphics graphics1 = e.Graphics;
            Rectangle rectangle1 = e.Bounds;
            if ((e.State & DrawItemState.Selected) > DrawItemState.None)
            {
                rectangle1.Width--;
            }
            DrawItemState state1 = e.State;
            if ((e.Index != -1))
            {
                ListItem item = this.ListItems[e.Index];
                this.LayoutManager.DrawItem(graphics1, rectangle1, this, state1, item.Text, item.ImageIndex);
            }
        }

		protected virtual void OnHotTrack(MouseEventArgs e)
		{
			Point point1 = base.PointToClient(Cursor.Position);
			for (int num1 = 0; num1 < this.Items.Count; num1++)
			{
				Rectangle rectangle1 = this.GetItemRectangle(num1);
				if (rectangle1.Contains(point1))
				{
					this.SelectedIndex = num1;
					return;
				}
			}
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if(isColumnView )
			{
				base.OnDrawItem(e); //OnDrawItemColumn(e);
			}
            else if (m_ListItems != null)
            {
                OnDrawListItem(e);
            }
            else if (this.DrawMode != DrawMode.Normal)//.OwnerDrawFixed || this.DrawMode == DrawMode.OwnerDrawVariable)
            {
                OnDrawItemInternal(e);
            }
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.HotTrack)
			{
				OnHotTrack(e);
			}
		}

 
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			//McColors.InitColors();
		}

		#endregion

		#region MultiColumns

//		internal McColumnCollection m_Cols;
//		internal int ColumnSpacing=3;
//		internal int ColumnImage=-1;
		internal bool isColumnView=false;

//		internal void ColumnViewSetting()
//		{
//			isColumnView=true;
//			this.DrawMode=System.Windows.Forms.DrawMode.OwnerDrawFixed;
//			this.MultiColumn=false;
//			this.drawItemStyle=DrawItemStyle.Default;
//		}

//		internal void ColumnsSetting(McColumn[] cols)
//		{
//			if(m_Cols!=null)
//			{
//				m_Cols.Clear();
//			}
//			else
//			{
//              m_Cols=new McColumnCollection(this);
//			}
//			m_Cols.AddRange(cols);
//		}

	
		//Draw multiple columns
//		protected virtual void OnDrawItemColumn(DrawItemEventArgs e)
//		{
//			
//			try
//			{
//				if(m_Cols==null) 
//				{
//					throw new ArgumentException("Invalid Columns");
//				}
//				int iIndex = e.Index;
//				int colCount=m_Cols.Count;
//				if(iIndex > -1 && colCount>0)
//				{
//					int iXPos = 0;
//					int iYPos = 0;
//					int widthAdd=0;
//					bool drawImage=false;
//					Rectangle rectImage = Rectangle.Empty;
//		
//					DataRow dr =  ((DataView)this.DataSource)[iIndex].Row;//  m_dv[iIndex].Row;
//					//DataRow dr =  m_DataView[iIndex].Row;
//					int imageIndex=-1;
//			
//					if(ColumnImage>-1 && ColumnImage < colCount)
//					{
//						imageIndex=(int) Types.StringToInt(dr[ColumnImage].ToString(),-1);
//					}
//
//					e.DrawBackground();
//
//					if(this.ImageList!=null && imageIndex > -1)
//					{
//		
//						Rectangle bounds=e.Bounds;
//						if ((imageIndex >= 0) && (imageIndex < imageList.Images.Count))
//						{
//							//Rectangle rectImage;
//							if (this.RightToLeft == RightToLeft.Yes)
//								rectImage = new Rectangle(bounds.Width-imageList.ImageSize.Width-1, bounds.Y + ((bounds.Height - imageList.ImageSize.Height) / 2), imageList.ImageSize.Width, imageList.ImageSize.Height);
//							else
//								rectImage = new Rectangle(bounds.X , bounds.Y + ((bounds.Height - imageList.ImageSize.Height) / 2), imageList.ImageSize.Width, imageList.ImageSize.Height);
//		
//							drawImage=true;
//						}
//					}
//
//					int colWidth=0;
//			
//					for(int index = 0; index < colCount; index++) //Loop for drawing each column
//					{
//						colWidth=m_Cols[index].Width;
//						if(m_Cols[index].Display == false || colWidth <=ColumnSpacing)
//							continue;
//
//						if(drawImage)
//						{
//							widthAdd=imageList.ImageSize.Width + 1;
//							if(colWidth > ColumnSpacing+widthAdd)
//							{
//								imageList.Draw(e.Graphics, rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height, imageIndex);
//							}
//							else
//							{
//								widthAdd=0;
//							}
//							drawImage=false;
//						}
//						else
//						{
//							widthAdd=0;
//						}
//						e.Graphics.DrawString(dr[index].ToString(), Font, new SolidBrush(e.ForeColor), new RectangleF(iXPos+widthAdd, e.Bounds.Y, colWidth-widthAdd-ColumnSpacing, ItemHeight));
//						iXPos += colWidth;
//					}
//					iXPos = 0;
//					iYPos += ItemHeight;
//					e.DrawFocusRectangle();
//					//base.OnDrawItem(e);
//				}
//			}			
//			catch(Exception ex)
//			{
//				throw new Exception(ex.Message + "\r\nIn ColumnComboBox.OnDrawItem(DrawItemEventArgs).");
//			}
//		}

		#endregion

		#region Properties

        [Category("Behavior"), DefaultValue(null)]
        public virtual string DefaultValue
        {
            get { return m_DefaultValue; }
            set
            {
                if (m_DefaultValue != value)
                {
                    m_DefaultValue = value;
                }
            }
        }

		[Category("Behavior"), DefaultValue(false)]
		public bool HotTrack
		{
			get
			{
				return this.hotTrack;
			}
			set
			{
				this.hotTrack = value;
				base.Invalidate();
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
				base.Invalidate();
			}
		}
        //[DefaultValue(false), Category("Behavior")]
        //public bool UseFirstImage
        //{
        //    get
        //    {
        //        return this.useFirstImage;
        //    }
        //    set
        //    {
        //        this.useFirstImage = value;
        //        base.Invalidate();
        //    }
        //}


	    [DefaultValue(DrawItemStyle.Default), Category("Behavior")]
		public DrawItemStyle DrawItemStyle
		{
			get
			{
				return this.drawItemStyle;
			}
			set
			{
				this.drawItemStyle = value;
				base.Invalidate();
			}
		}

        [DefaultValue(0), Category("Behavior")]
        public int DefaultImageIndex
        {
            get { return defaultImageIndex; }
            set { defaultImageIndex = value; }
        }

        [DefaultValue(null), Category("Behavior")]
        public ItemsCollection ListItems
        {
            get 
            {
                if (m_ListItems == null)
                {
                    m_ListItems = new ItemsCollection();
                    m_ListItems.ItemAdded += new ItemsCollectionChange(m_ListItems_ItemAdded);
                    m_ListItems.ItemRemoved += new ItemsCollectionChange(m_ListItems_ItemRemoved);
                    m_ListItems.ItemsCleared += new EventHandler(m_ListItems_ItemsCleared);
                }
                return m_ListItems; 
            }
        }

        void m_ListItems_ItemsCleared(object sender, EventArgs e)
        {
            //if (!DesignMode)
                this.Items.Clear();
        }

        void m_ListItems_ItemRemoved(int index, ListItem value)
        {
            //if (!DesignMode)
                this.Items.RemoveAt(index);
        }

        void m_ListItems_ItemAdded(int index, ListItem value)
        {
            //if (!DesignMode)
            if(index > this.Items.Count-1)
                this.Items.Add(value);
            else
                this.Items.Insert(index, value.Text);
        }

     
		#endregion

        #region StyleProperty

        [Category("Style"), DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                SerializeForeColor(value, true);
            }
        }

        [Category("Style"), DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                SerializeBackColor(value, true);
            }
        }

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);
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
            {
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
            }
            else if (force)
            {
                base.ForeColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                base.BackColor = LayoutManager.Layout.BackColorInternal;
            }
            else if (force)
            {
                base.BackColor = value;
            }
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

        [Category("Apperarace"), DefaultValue(BorderStyle.FixedSingle)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        }

        #endregion


		#region ILayout

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Style"), DefaultValue(ControlLayout.Visual)]
        public virtual ControlLayout ControlLayout
        {
            get { return ControlLayout.Visual; }
            set
            {
            }
        }

		protected IStyle		m_StylePainter;

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

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}

		#endregion

		#region IBind Members

		private bool readOnly=false;

		[Browsable(false)]
		public BindingFormat BindFormat
		{
			get{return BindingFormat.String;}
		}

		public string BindPropertyName()
		{
			
			return  "SelectedValue";
		}

		public bool ReadOnly
		{
			get{return this.readOnly;} 
			set
			{
              this.readOnly=value;
			  //if(value)
                // base.SelectionMode=SelectionMode.None ;
			}
		}

         public virtual void BindDefaultValue()
        {
            //if (this.DefaultValue.Length > 0)
            //{
                this.SelectedValue = this.DefaultValue;
                if (base.IsHandleCreated)
                {
                    this.OnSelectedValueChanged(EventArgs.Empty);
                }
            //}
        }
		#endregion
	}
}
