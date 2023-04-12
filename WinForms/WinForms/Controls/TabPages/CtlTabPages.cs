using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Collections;

using mControl.Drawing;
using mControl.Collections;

namespace mControl.WinForms
{

    public enum TabPosition
    {
        Top,
        Bottom,
        Hide
    }

    [ToolboxItem(false), ToolboxBitmap(typeof(McTabPages), "Toolbox.TabPages.bmp"), Designer(typeof(Design.TabPagesDesigner))]
    public class McTabPages : McPanel
    {

        #region members
        // Events
        [Category("Behavior")]
        public event EventHandler SelectedIndexChanged;
        //public event EventHandler SelectionChanged;
        [Category("Behavior")]
        public event EventHandler TitleClick;



        // Fields
        private ImageList imageList;
        private bool locked;
        //private bool positionAtBottom;
        private McPage selectedTab;
        private StringFormat sfTabs;
        //private int titleHeight;
        private int[] widthPages;

        protected McTabPages.TabPagesCollection _tabPages;	// collection of pages
        //private Color _BackgroundColor;
        private bool drawTop;
        private bool drawBackgroung;
        private Size itemSize;
        private McPage hoverPage;
        private TabPosition tabPosition;
        //internal Control owner;
        //private bool autoToolTip = false;
        /*toolTip*/
        private McToolTip toolTip;
        private bool showToolTip=true;

        const int DefaultTitleHeight = 0x16;
        private Size DefaultItemSize
        {
            get { return new Size(0, DefaultTitleHeight); }
        }

        #endregion

        #region Ctor

        internal McTabPages(bool net)
            : this()
        {
            m_netFram = net;
        }

        public McTabPages()
        {
            this.tabPosition = TabPosition.Top;

            this.drawBackgroung = true;
            this.drawTop = false;
            //this._BackgroundColor=SystemColors.Control;
            this.widthPages = null;
            //this.titleHeight = 0x16;
            this.itemSize = new Size(0, DefaultTitleHeight);

            this.locked = false;
            //this.positionAtBottom = false;
            this.imageList = null;
            this.selectedTab = null;
            this.sfTabs = new StringFormat();
            this.sfTabs.HotkeyPrefix = HotkeyPrefix.Show;
            this.sfTabs.LineAlignment = StringAlignment.Center;
            this.sfTabs.Alignment = StringAlignment.Center;
            this.sfTabs.FormatFlags = StringFormatFlags.NoWrap;
            this.sfTabs.Trimming = StringTrimming.EllipsisCharacter;
            if (this.RightToLeft == RightToLeft.Yes)
            {
                this.sfTabs.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.ControlAdded += new ControlEventHandler(this.PageAdded);
            base.ControlRemoved += new ControlEventHandler(this.PageRemoved);
            this.SetLayout();


            // Create collections
            _tabPages = new McTabPages.TabPagesCollection();

            _tabPages.Inserted += new CollectionChange(_tabPages_Inserted);
            _tabPages.Removed += new CollectionChange(_tabPages_Removed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.sfTabs.Dispose();
                //if (this.imageList != null)
                //{
                //    this.imageList.Disposed -= new EventHandler(this.DetachImageList);
                //}
                /*toolTip*/
                if (this.toolTip != null)
                {
                    this.toolTip.Dispose();
                    this.toolTip = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region  override

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            /*toolTip*/
            if (showToolTip && (this.toolTip == null))
            {
                this.toolTip = new McToolTip();
            }
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            this.Invalidate();
            //			if(this.drawBackgroung)
            //			{
            //				if(Parent.BackColor!=Color.Transparent)
            //				{
            //					this._BackgroundColor=Parent.BackColor;
            //					this.Invalidate();
            //				}
            //			}
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point point1 = new Point(e.X, e.Y);
            using (Graphics graphics1 = Graphics.FromHwnd(base.Handle))
            {
                this.CalculatePagesWidth(graphics1);
            }
            McPage page1 = this.GetTabPageAtPoint(point1);
            if (page1 != null)
            {
                this.SelectedTab = page1;
                this.InvokeTitleClick(this, e);
                if (((e.Button & MouseButtons.Right) > MouseButtons.None) && (this.SelectedTab.TitleContextMenu != null))
                {
                    this.SelectedTab.TitleContextMenu.Show(page1, page1.PointToClient(Cursor.Position));
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.Invalidate();
            base.OnMouseEnter(e);

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.Invalidate();
            base.OnMouseMove(e);
        }

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            McColors.InitColors();
        }


        protected virtual void OnTitleClick(EventArgs e)
        {
            if (this.TitleClick != null)
            {
                this.TitleClick(null, EventArgs.Empty);
            }

        }

        //		protected virtual void OnSelectedIndexChanged(EventArgs e)
        //		{
        //		}

        //		public virtual void OnSelectionChanging(EventArgs e)
        //		{
        //			// Has anyone registered for the event?
        //			if (SelectionChanging != null)
        //				SelectionChanging(this, e);
        //		}

        public virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }

        //		public virtual void OnClosePressed(EventArgs e)
        //		{
        //			// Has anyone registered for the event?
        //			if (ClosePressed != null)
        //				ClosePressed(this, e);
        //		}

        //		public virtual void OnPageGotFocus(EventArgs e)
        //		{
        //			// Has anyone registered for the event?
        //			if (PageGotFocus != null)
        //				PageGotFocus(this, e);
        //		}
        //		
        //		public virtual void OnPageLostFocus(EventArgs e)
        //		{
        //			// Has anyone registered for the event?
        //			if (PageLostFocus != null)
        //				PageLostFocus(this, e);
        //		}

        #endregion

        #region Collection

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public McTabPages.TabPagesCollection TabPages
        {
            get { return _tabPages; }
        }

        #endregion

        #region methods

        private void CalculatePagesWidth(Graphics g)
        {
            this.widthPages = new int[_tabPages.Count];
            int num1 = 0;
            foreach (McPage page1 in _tabPages)
            {
                this.widthPages[num1++] = this.GetPageWidth(g, page1);
            }
        }

        #endregion

        #region Draw

        protected override void OnPaint(PaintEventArgs p)
        {
            base.OnPaint(p);
            if ((!this.locked && (base.Width != 0)) && (base.Height != 0))
            {
                Graphics graphics1 = p.Graphics;
                Rectangle rectangle3 = Rectangle.Empty;
                Rectangle rectangle1 = Rectangle.Empty;
                int titleHeight = this.itemSize.Height;
                if (tabPosition == TabPosition.Hide)
                {
                    return;
                }

                if (tabPosition == TabPosition.Bottom)// this.PositionAtBottom)
                {
                    new Rectangle(0, 0, base.Width, base.Height - titleHeight);
                    rectangle1 = new Rectangle(0, base.Height - titleHeight, base.Width, titleHeight);
                }
                else
                {
                    new Rectangle(0, titleHeight, base.Width, base.Height - titleHeight);
                    rectangle1 = new Rectangle(0, 0, base.Width, titleHeight);
                }
                this.CalculatePagesWidth(graphics1);

                //graphics1.FillRectangle(new SolidBrush(_BackgroundColor), rectangle1);// McBrushes.ContentDark, rectangle1);
                //Draw Background
                if (this.drawBackgroung)
                {
                    if (Parent.BackColor != Color.Transparent)
                    {
                        using (Brush sbBack = new SolidBrush(Parent.BackColor))
                        {
                            graphics1.FillRectangle(sbBack, rectangle1);// new SolidBrush(_BackgroundColor), rectangle1);// McBrushes.ContentDark, rectangle1);
                        }
                    }
                    else
                    {
                        using (Brush sbBack = McStyleLayout.GetBrushFlat())
                        {
                            graphics1.FillRectangle(sbBack, rectangle1);// new SolidBrush(_BackgroundColor), rectangle1);// McBrushes.ContentDark, rectangle1);
                        }
                    }
                }
                else
                {
                    using (Brush sbBack = McStyleLayout.GetBrushFlat())
                    {
                        graphics1.FillRectangle(sbBack, rectangle1);// new SolidBrush(_BackgroundColor), rectangle1);// McBrushes.ContentDark, rectangle1);
                    }
                }

                if (tabPosition == TabPosition.Hide)
                {
                    return;
                }

                int num1 = 0;
                foreach (McPage page1 in _tabPages)
                {
                    if (page1.PageVisible)
                    {
                        num1++;
                    }
                }
                if (base.DesignMode)
                {
                    num1 = _tabPages.Count;
                }
                if (num1 > 0)
                {
                    if (this.SelectedTab == null)
                    {
                        this.SelectedTab = _tabPages[0] as McPage;
                    }
                    int num2 = 0;
                    _tabPages.IndexOf(this.SelectedTab);
                    for (int num3 = _tabPages.Count - 1; num3 >= 0; num3--)
                    {
                        McPage page2 = _tabPages[num3] as McPage;
                        if ((page2 != this.SelectedTab) && (page2.PageVisible || base.DesignMode))
                        {
                            this.DrawPage(graphics1, page2);
                        }
                        num2++;
                    }
                    this.DrawPage(graphics1, this.SelectedTab);

                    //					Rectangle rectangle2 = this.GetPageRectangle(graphics1, this.SelectedTab);
                    //					if (this.PositionAtBottom)
                    //					{
                    //						graphics1.DrawLine(SystemPens.ControlDark, 0, base.Height - this.titleHeight, rectangle2.X - 1, base.Height - this.titleHeight);
                    //						graphics1.DrawLine(SystemPens.ControlDark, rectangle2.Right + 5, base.Height - this.titleHeight, base.Width + 1, base.Height - this.titleHeight);
                    //					}
                    //					else
                    //					{
                    //						graphics1.DrawLine(SystemPens.ControlLightLight, 0, this.titleHeight - 1, rectangle2.X - 1, this.titleHeight - 1);
                    //						graphics1.DrawLine(SystemPens.ControlDark, rectangle2.Right + 5, this.titleHeight - 1, base.Width + 1, this.titleHeight - 1);
                    //					}
                }
                //graphics1.DrawLine(McStyleBase.GetPenBorder(), 0, 0, rectangle1.Right, 0);

                if (this.drawTop)
                {
                    if (tabPosition == TabPosition.Bottom)//this.PositionAtBottom)
                        graphics1.DrawLine(McStyleLayout.GetPenBorder(), 0, base.Height - 1, rectangle1.Right, base.Height - 1);
                    else
                        graphics1.DrawLine(McStyleLayout.GetPenBorder(), 0, 0, rectangle1.Right, 0);
                }

            }
        }


        private void DrawPage(Graphics g, McPage page)
        {
            if (/*toolTip*/ toolTip != null && this.showToolTip && !string.IsNullOrEmpty(page.ToolTipText))
            {
                McPage pg = this.GetTabPageAtPoint(base.PointToClient(Cursor.Position));
                if (pg != hoverPage)
                {
                    //McToolTip.Instance.Hide(this);
                    toolTip.Hide(this);
                }

                if (pg == page && page != hoverPage)
                {
                    //McToolTip.Instance.Show(page.ToolTipText, this);
                    /*toolTip*/
                    toolTip.Show(page.ToolTipText, this);//, base.PointToClient(Cursor.Position));
                    hoverPage = page;
                }
            }
  
            Point[] pointArray1;
            Color color1;
            Point[] pointArray2;
            if (!page.PageVisible && !base.DesignMode)
            {
                return;
            }
            Rectangle rectangle1 = this.GetPageRectangle(g, page);
            //if (tabPosition==TabPosition.Hide)
            if ((rectangle1.Width == 0) || (rectangle1.Height == 0))
            {
                return;
            }
            if (tabPosition == TabPosition.Top)//this.PositionAtBottom)
            {
                pointArray2 = new Point[] { new Point(rectangle1.X, rectangle1.Y), new Point(rectangle1.X, rectangle1.Bottom), new Point(rectangle1.Right + 5, rectangle1.Bottom), new Point(rectangle1.Right - 5, rectangle1.Y) };
                pointArray1 = pointArray2;
            }
            else
            {
                pointArray2 = new Point[] { new Point(rectangle1.X, rectangle1.Y), new Point(rectangle1.X, rectangle1.Bottom), new Point(rectangle1.Right - 5, rectangle1.Bottom), new Point(rectangle1.Right + 5, rectangle1.Y) };
                pointArray1 = pointArray2;
            }
            using (GraphicsPath path1 = new GraphicsPath(FillMode.Alternate))
            {
                path1.AddPolygon(pointArray1);
                if (this.SelectedTab == page)
                {
                    McPage page1 = this.GetTabPageAtPoint(base.PointToClient(Cursor.Position));
                    if (page1 == page)
                    {
                          using (Brush brush1 = base.McStyleLayout.GetBrushGradient(rectangle1, 90f, false))// McBrushes.GetControlLightBrush(rectangle1, 90f))
                        {
                            g.FillPath(brush1, path1);
                            goto Label_0231;
                        }
                    }
                    using (Brush brush2 = base.McStyleLayout.GetBrushGradient(rectangle1, 90f, true))// McBrushes.GetControlBrush(rectangle1, 90f))
                    {
                        g.FillPath(brush2, path1);
                        goto Label_0231;
                    }
                }
                McPage page2 = this.GetTabPageAtPoint(base.PointToClient(Cursor.Position));
                if (page2 == page)
                {
                     using (Brush brush3 = new SolidBrush(McPaint.Light(McColors.Content, 15)))
                    {
                        g.FillPath(brush3, path1);
                        goto Label_0231;
                    }
                }
                g.FillPath(McBrushes.Content, path1);
            }
        Label_0231:
            color1 = this.ForeColor;
            if (page != this.SelectedTab)
            {
                color1 = SystemColors.ControlDark;
            }
            SizeF ef1 = SizeF.Empty;
            int indx = page.ImageIndex;// _tabPages.IndexOf(page);

            if ((this.ImageList != null && indx > -1) || (page.Image != null))
            {
                if (page.Image != null)
                {
                    ef1 = (SizeF)page.Image.Size;
                }
                else
                {
                    ef1 = (SizeF)this.ImageList.ImageSize;
                }
                Rectangle rectangle2 = new Rectangle(rectangle1.X + 2, rectangle1.Y + ((int)((rectangle1.Height - ef1.Height) / 2f)), (int)ef1.Width, (int)ef1.Height);
                if (ef1.Width != 0f)
                {
                    if ((this.imageList != null && indx > -1) && (this.ImageList.Images.Count > indx))
                    {
                        this.ImageList.Draw(g, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height, indx);
                    }
                    else if (page.Image != null)
                    {
                        g.DrawImage(page.Image, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height);
                    }
                }

            }
            Rectangle rectangle3 = new Rectangle(rectangle1.X + ((int)ef1.Width), rectangle1.Y, rectangle1.Width - ((int)ef1.Width), rectangle1.Height);
            using (Brush brush4 = new SolidBrush(color1))
            {
                g.DrawString(page.Text, this.Font, brush4, (RectangleF)rectangle3, this.sfTabs);
            }
            if (this.SelectedTab == page)
            {
                g.DrawLine(SystemPens.ControlLightLight, pointArray1[0], pointArray1[1]);
                if (tabPosition == TabPosition.Top)//!this.PositionAtBottom)
                {
                    using (Brush brush5 = new LinearGradientBrush(new Rectangle(pointArray1[3].X, pointArray1[3].Y, pointArray1[2].X - pointArray1[3].X, (pointArray1[2].Y - pointArray1[3].Y) - 5), SystemColors.Control, SystemColors.ControlDark, (LinearGradientMode)((int)0)))
                    {
                        using (Pen pen1 = new Pen(brush5))
                        {
                            g.DrawLine(pen1, pointArray1[2], pointArray1[3]);
                        }
                    }
                    g.DrawLine(SystemPens.ControlLightLight, pointArray1[0], pointArray1[3]);
                }
                else
                {
                    g.DrawLine(SystemPens.ControlDark, pointArray1[1], pointArray1[2]);
                    g.DrawLine(SystemPens.ControlDark, pointArray1[2], pointArray1[3]);
                }

            }
            else
            {
                using (Pen pen2 = new Pen(McPaint.Dark(SystemColors.Control, 40)))
                {
                    if (tabPosition == TabPosition.Bottom)//this.PositionAtBottom)
                    {
                        g.DrawLine(pen2, pointArray1[0], pointArray1[1]);
                        g.DrawLine(pen2, pointArray1[1], pointArray1[2]);
                        g.DrawLine(pen2, pointArray1[2], pointArray1[3]);
                    }
                    else
                    {
                        g.DrawLine(pen2, pointArray1[0], pointArray1[1]);
                        g.DrawLine(pen2, pointArray1[0], pointArray1[3]);
                        g.DrawLine(pen2, pointArray1[2], pointArray1[3]);
                    }
                }
            }
        }

        private Rectangle GetPageRectangle(Graphics g, McPage page)
        {
            int num1 = this.GetPanelStartPos();
            int num2 = 10;
            int num3 = this.GetPagesWidth(g);
            double num4 = 1;
            if (((num3 + (num1 * 2)) + 5) > base.Width)
            {
                num4 = ((double)base.Width) / ((num3 + (num1 * 2)) + 5);
            }
            int num5 = 0;
            foreach (McPage page1 in _tabPages)
            {
                if (page1.PageVisible || base.DesignMode)
                {
                    if (page1 != page)
                    {
                        num1 += (int)(this.widthPages[num5] * num4);
                    }
                    else
                    {
                        num2 = (int)(this.widthPages[num5] * num4);
                        break;
                    }
                }
                num5++;
            }
            _tabPages.IndexOf(this.SelectedTab);
            _tabPages.IndexOf(page);
            Rectangle rectangle1 = Rectangle.Empty;
            if (tabPosition == TabPosition.Hide)
            {
                return new Rectangle(0, 0, 0, 0);

            }
            else if (tabPosition == TabPosition.Bottom)//this.PositionAtBottom)
            {
                return new Rectangle(num1, (base.Height - this.itemSize.Height) - 1, num2, this.itemSize.Height - 2);
            }
            return new Rectangle(num1, 2, num2, this.itemSize.Height - 2);
        }

        private int GetPagesWidth(Graphics g)
        {
            int num1 = 0;
            int num2 = 0;
            if (this.widthPages == null)
            {
                CalculatePagesWidth(g);
            }
            foreach (McPage page1 in _tabPages)
            {
                if (page1.PageVisible || base.DesignMode)
                {
                    num1 += this.widthPages[num2];
                }
                num2++;
            }
            return num1;
        }

        private int GetPageWidth(Graphics g, McPage page)
        {
            int num1 = 0;
            if ((this.ImageList != null) && (this.ImageList.Images.Count > _tabPages.IndexOf(page)))
            {
                num1 = this.ImageList.ImageSize.Width;
            }
            if (page.Image != null)
            {
                num1 = page.Image.Width;
            }

            if (this.itemSize.Width > 0)
            {
                return this.itemSize.Width;
            }

            SizeF ef1 = g.MeasureString(page.Text, this.Font, 0x3e8, this.sfTabs);
            return ((((int)ef1.Width) + 10) + num1);
        }

        private int GetPanelStartPos()
        {
            return 5;
        }

        #endregion

        #region public methods

        public McPage GetTabPageAtPoint(Point p)
        {
            using (Graphics graphics1 = Graphics.FromHwnd(base.Handle))
            {
                foreach (McPage page1 in _tabPages)
                {
                    if (!page1.PageVisible && !base.DesignMode)
                    {
                        continue;
                    }
                    Rectangle rectangle1 = this.GetPageRectangle(graphics1, page1);
                    if (rectangle1.Contains(p))
                    {
                        return page1;
                    }
                }
            }
            return null;
        }

        public virtual void InvokeSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.OnSelectedIndexChanged(EventArgs.Empty);
                //if (this.SelectedIndexChanged != null)
                //{
                //	this.SelectedIndexChanged(sender, e);
                //}
            }
            catch
            {
            }
        }

        public virtual void InvokeTitleClick(object sender, EventArgs e)
        {
            try
            {
                this.OnTitleClick(EventArgs.Empty);
            }
            catch
            {
            }
        }


        public void LockRefresh()
        {
            this.locked = true;
        }

        public void UnlockRefresh()
        {
            this.locked = false;
        }

        #endregion

        #region private methods


        private void _tabPages_Inserted(int index, object value)
        {
            //			if(this.owner!=null)
            //				this.owner.Controls.Add(value as McPage);
            //			else
            this.Controls.Add(value as McPage);
        }

        private void _tabPages_Removed(int index, object value)
        {
            //			if(this.owner!=null)
            //				this.owner.Controls.Remove(value as McPage);
            //			else
            this.Controls.Remove(value as McPage);
        }


        internal void PageAdded(object sender, ControlEventArgs e)
        {
            if (!(e.Control is McPage))
            {
                return;
            }
            e.Control.Dock = DockStyle.Fill;
            e.Control.CreateGraphics();
            McPage pag = e.Control as McPage;

            pag.StylePainter = this.StylePainter;
            if (_tabPages.Count == 1)
            {
                this.SelectedTab = _tabPages[0] as McPage;
            }
            base.Invalidate();
        }

        private void PageRemoved(object sender, ControlEventArgs e)
        {
            foreach (McPage page1 in _tabPages)
            {
                if (page1 == this.SelectedTab)
                {
                    _tabPages.Remove(page1);
                    //base.Invalidate();
                    break;
                }
            }

            if (_tabPages.Count > 0)
            {
                this.SelectedTab = _tabPages[0] as McPage;
                base.Invalidate();
                return;
            }
            this.SelectedTab = null;
            base.Invalidate();
        }

        protected override bool ProcessMnemonic(char key)
        {
            foreach (McPage page1 in _tabPages)
            {
                if (Control.IsMnemonic(key, page1.Text))
                {
                    this.SelectedTab = page1;
                    return true;
                }
            }
            return false;
        }

        protected virtual void SetLayout()
        {
            int num1;
            this.DockPadding.Right = num1 = 0;
            this.DockPadding.Left = num1;
            if (tabPosition == TabPosition.Hide)
            {
                this.DockPadding.Top = 0;
                this.DockPadding.Bottom = 0;
            }
            else if (tabPosition == TabPosition.Bottom)//this.PositionAtBottom)
            {
                this.DockPadding.Top = 0;
                this.DockPadding.Bottom = this.itemSize.Height;
            }
            else
            {
                this.DockPadding.Top = this.itemSize.Height;
                this.DockPadding.Bottom = 0;
            }
        }
        #endregion

        #region properties

        //[Category("Behavior"), DefaultValue(false)]
        //public bool AutoToolTip
        //{
        //    get { return autoToolTip; }
        //    set
        //    {
        //        if (autoToolTip != value)
        //        {
        //            autoToolTip = value;
        //            foreach (McPage tp in this.TabPages)
        //            {
        //                tp.AutoToolTip = value;
        //             }
        //        }
        //    }
        //}

        [Category("Behavior"), DefaultValue(true)]
        public virtual bool ShowToolTip
        {
            get { return showToolTip; }
            set
            {
                if (showToolTip != value)
                {
                    showToolTip = value;
                    /*toolTip*/
                    if (value && (this.toolTip == null))
                    {
                        this.toolTip = new McToolTip();
                    }
                    base.Invalidate();
                }
            }
        }
        [Category("Style"), Browsable(true), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoChildrenStyle
        {
            get { return base.AutoChildrenStyle; }
            set
            {
                if (base.AutoChildrenStyle != value)
                {
                    base.AutoChildrenStyle = value;
                    foreach (McPage tp in this.TabPages)
                    {
                        tp.autoChildrenStyle = value;
                        tp.Invalidate(true);
                    }
                }
            }
        }

        [Category("Style"), Browsable(true), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ControlLayout ControlLayout
        {
            get { return base.ControlLayout; }
            set
            {
                if (base.ControlLayout != value)
                {
                    base.ControlLayout = value;
                    foreach (McPage tp in this.TabPages)
                    {
                        tp.ControlLayout = value;
                        tp.Invalidate(true);
                    }
                }
            }
        }

        //[Category("Style"), Browsable(true), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override GradientStyle GradientStyle
        //{
        //    get { return base.GradientStyle; }
        //    set
        //    {
        //        if (base.GradientStyle != value)
        //        {
        //            base.GradientStyle = value;
        //            foreach (McPage tp in this.TabPages)
        //            {
        //                tp.GradientStyle = value;
        //                tp.Invalidate(true);
        //            }
        //        }
        //    }
        //}


        [Browsable(false)]
        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false)]
        public new Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }
        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get
            {
                return base.BorderStyle;
            }
            set
            {
                base.BorderStyle = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ScrollableControl.DockPaddingEdges DockPadding
        {
            get
            {
                return base.DockPadding;
            }
        }
        [DefaultValue((string)null), Category("Behavior")]
        public virtual ImageList ImageList
        {
            get
            {
                return this.imageList;
            }
            set
            {
                this.imageList = value;
            }
        }

        //		[DefaultValue(0x16), Category("Behavior")]
        //		public virtual int TitleHeight
        //		{
        //			get
        //			{
        //				return this.titleHeight;
        //			}
        //			set
        //			{
        //				if(value>=0 && value < 50)
        //				{
        //					this.titleHeight = value;
        //					this.SetLayout();
        //					base.Invalidate();
        //				}
        //			}
        //		}

        [Category("Behavior"), Localizable(true), Description("TabBaseItemSize")]
        public Size ItemSize
        {
            get
            {
                if (!this.itemSize.IsEmpty)
                {
                    return this.itemSize;
                }
                if (base.IsHandleCreated)
                {
                    //this.getTabRectfromItemSize = true;
                    //Rectangle rectangle1 = this.GetTabRect(0);
                    //return rectangle1.Size;
                }
                return DefaultItemSize;
            }
            set
            {
                if ((value.Width < 0) || (value.Height < 0))
                {
                    throw new ArgumentException("InvalidArgument", "ItemSize " + value.ToString());
                }
                this.itemSize = value;
                this.SetLayout();

                //this.ApplyItemSize();
                //this.UpdateSize();
                base.Invalidate();
            }
        }

        //		internal void ApplyItemSize()
        //		{
        //			if (base.IsHandleCreated && this.ShouldSerializeItemSize())
        //			{
        //                 this.Invalidate();
        //				//this.SendMessage(0x1329, 0, (int) WinMethods.MAKELPARAM(this.itemSize.Width, this.itemSize.Height));
        //			}
        //			//this.cachedDisplayRect = Rectangle.Empty;
        //		}

        private bool ShouldSerializeItemSize()
        {
            return !this.itemSize.Equals(DefaultItemSize);
        }

        //		internal void UpdateSize()
        //		{
        //			//this.BeginUpdate();
        //			Size size1 = base.Size;
        //			base.Size = new Size(size1.Width + 1, size1.Height);
        //			base.Size = size1;
        //			//this.EndUpdate();
        //		}


        [DefaultValue(TabPosition.Top), Category("Behavior")]
        public virtual TabPosition TabPosition
        {
            get
            {
                return this.tabPosition;
            }
            set
            {
                this.tabPosition = value;
                this.SetLayout();
                base.Invalidate();
            }
        }

        //		[DefaultValue(false), Category("Behavior")]
        //		public virtual bool PositionAtBottom
        //		{
        //			get
        //			{
        //				return this.positionAtBottom;
        //			}
        //			set
        //			{
        //				this.positionAtBottom = value;
        //				this.SetLayout();
        //				base.Invalidate();
        //			}
        //		}

        [DefaultValue(true), Category("Style"), RefreshProperties(RefreshProperties.Repaint)]
        public bool DrawBackground
        {
            get
            {
                return this.drawBackgroung;
            }
            set
            {
                if (this.drawBackgroung != value)
                {
                    this.drawBackgroung = value;
                    //this.OnParentBackColorChanged(EventArgs.Empty);
                    this.Invalidate();
                }
            }
        }

        //		[DefaultValue(typeof(Color),"Control"),Category("Style")]
        //		public Color BackgroundColor
        //		{
        //			get
        //			{
        //				return _BackgroundColor;
        //			}
        //			set
        //			{
        //				_BackgroundColor=value;
        //				this.Invalidate();
        //			}
        //		}

        [DefaultValue(false), Category("Style")]
        public bool DrawTopLine
        {
            get
            {
                return this.drawTop;
            }
            set
            {
                this.drawTop = value;
                this.Invalidate();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get
            {
                return _tabPages.IndexOf(this.SelectedTab);
            }
            set
            {
                if ((value >= 0) && (value < _tabPages.Count))
                {
                    this.SelectedTab = _tabPages[value] as McPage;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public McPage SelectedTab
        {
            get
            {
                return this.selectedTab;
            }
            set
            {
                foreach (McPage page1 in _tabPages)
                {
                    if (page1 == value)
                    {
                        page1.Dock = DockStyle.Fill;
                        page1.Show();
                    }
                }
                foreach (McPage page2 in _tabPages)
                {
                    if (page2 != value)
                    {
                        page2.Hide();
                    }
                }
                this.selectedTab = value;
                base.Invalidate();
                this.InvokeSelectedIndexChanged(this, EventArgs.Empty);
            }
        }


        //		protected mControl.Collections.TabPageCollection _tabPages=new mControl.Collections.TabPageCollection();
        //
        //
        //		[Category("Appearance")]
        //		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //		public virtual mControl.Collections.TabPageCollection TabPages
        //		{
        //			get { return _tabPages; }
        //		}
        #endregion

        #region TabPagCollection

        public class TabPagesCollection : CollectionWithEvents
        {
            public McPage Add(McPage value)
            {
                // Use base class to process actual collection operation
                base.List.Add(value as object);

                return value;
            }

            public void AddRange(McPage[] values)
            {
                // Use existing method to add each array entry
                foreach (McPage page in values)
                    Add(page);
            }

            public void Remove(McPage value)
            {
                // Use base class to process actual collection operation
                base.List.Remove(value as object);
            }

            public void Insert(int index, McPage value)
            {
                // Use base class to process actual collection operation
                base.List.Insert(index, value as object);
            }

            public void MoveTo(int index, McPage value)
            {
                // Use base class to process actual collection operation
                base.List.Remove(value as object);
                base.List.Insert(index, value as object);
            }

            public bool Contains(McPage value)
            {
                // Use base class to process actual collection operation
                return base.List.Contains(value as object);
            }

            public McPage this[int index]
            {
                // Use base class to process actual collection operation
                get { return (base.List[index] as McPage); }
            }

            public McPage this[string title]
            {
                get
                {
                    // Search for a Page with a matching title
                    foreach (McPage page in base.List)
                        if (page.Text == title)
                            return page;

                    return null;
                }
            }

            public int IndexOf(McPage value)
            {
                // Find the 0 based index of the requested entry
                return base.List.IndexOf(value);
            }

            public McPage Add(string title)
            {
                McPage item = new McPage(title);
                return Add(item);
            }

            public McPage Add(string title, StyleGuide style)
            {
                McPage item = new McPage(title);
                //item.StylePlan = style;
                return Add(item);
            }

            public void CopyTo(TabPagesCollection array, System.Int32 index)
            {
                foreach (McPage obj in base.List)
                    array.Add(obj);
            }

        }
        #endregion


    }


}
