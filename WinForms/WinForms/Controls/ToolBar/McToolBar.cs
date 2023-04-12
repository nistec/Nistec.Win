using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

using Nistec.Drawing;

namespace Nistec.WinForms
{
  
    [Designer(typeof(Design.ToolBarDesigner)), ToolboxItem(true), ToolboxBitmap(typeof(McToolBar), "Toolbox.ToolBar.bmp")]
    public class McToolBar : McPanel,IToolBar
    {

        #region Members
        // Fields
        protected const int defaultHeight = 28;
        protected const int DockPaddingNormal = 3;
        protected const int DockPaddingTitle = 12;
        protected const int DotWidth = 12;

        protected bool ChangePos;
        protected bool IsDragging;
        private Point lastPos;
        private Point movePos;
        protected bool fixSize;
        private ImageList m_ImageList;
        protected bool allowMove;
        private int selectedGroup;
        //public event ButtonClickEventHandler ButtonClick;
        public event ToolButtonClickEventHandler ButtonClick;

        //internal protected bool UseDesigner=true;

        #endregion

        #region Constructor

        //internal McToolBar(bool net)
        //    : this()
        //{
        //    m_netFram = net;
        //}

        public McToolBar()
            : base(ControlLayout.Visual)
        {
            //base.gradiaentAngle=270f;//reversColor=true;
            //base.m_Style.XpDisable=true;
            this.selectedGroup = -1;
            this.IsDragging = false;
            this.ChangePos = true;
            this.allowMove = true;
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetLayoutTitle();
            base.Height = 0x1c;
        }

        #endregion

        #region Methods

        private Point CheckLocation(Point location)
        {
            location.X = Math.Max(1, location.X);
            location.Y = Math.Max(1, location.Y);
            int num1 = this.GetFreePos(location.X, location.Y);
            location.X = Math.Max(num1, location.X);
            location.Y = Math.Min(this.CorrectY(location.Y), this.GetMaxVerticalPos());
            if ((location.X + base.Width) > base.Parent.Width)
            {
                location.X = base.Parent.Width - base.Width;
            }
            return location;
        }

        public void CheckSeparators()
        {
            base.SuspendLayout();
            try
            {
                ArrayList list1 = new ArrayList();
                foreach (Control control1 in base.Controls)
                {
                    if ((control1 is McToolButton) && (((McToolButton)control1).ButtonStyle == ToolButtonStyle.Separator))
                    {
                        control1.Visible = true;
                    }
                    if (control1.Visible)
                    {
                        list1.Add(control1);
                    }
                }
                IEnumerator enumerator1 = list1.GetEnumerator();
                try
                {
                Label_0104:
                    while (enumerator1.MoveNext())
                    {
                        Control control2 = (Control)enumerator1.Current;
                        if (((control2 is McToolButton) && (((McToolButton)control2).ButtonStyle == ToolButtonStyle.Separator)) && control2.Visible)
                        {
                            int num1 = list1.IndexOf(control2) + 1;
                            for (int num2 = num1; num2 < list1.Count; num2++)
                            {
                                McToolButton button1 = list1[num2] as McToolButton;
                                Control control3 = list1[num2] as Control;
                                if (((button1 != null) && (button1.ButtonStyle != ToolButtonStyle.Separator)) || (button1 == null))
                                {
                                    goto Label_0104;
                                }
                                control3.Visible = false;
                            }
                        }
                    }
                }
                finally
                {
                    IDisposable disposable1 = enumerator1 as IDisposable;
                    if (disposable1 != null)
                    {
                        disposable1.Dispose();
                    }
                }
                for (int num3 = 0; num3 < base.Controls.Count; num3++)
                {
                    Control control4 = base.Controls[num3];
                    if (control4.Visible)
                    {
                        if ((control4 is McToolButton) && (((McToolButton)control4).ButtonStyle == ToolButtonStyle.Separator))
                        {
                            control4.Visible = false;
                        }
                        break;
                    }
                }
                for (int num4 = base.Controls.Count - 1; num4 >= 0; num4--)
                {
                    Control control5 = base.Controls[num4];
                    if (control5.Visible)
                    {
                        if ((control5 is McToolButton) && (((McToolButton)control5).ButtonStyle == ToolButtonStyle.Separator))
                        {
                            control5.Visible = false;
                        }
                        break;
                    }
                }
                if (base.Controls.Count > 0)
                {
                    for (int num5 = 0; num5 < base.Controls.Count; num5++)
                    {
                        Control control6 = base.Controls[num5];
                        if (!(control6 is McToolButton) || (((McToolButton)control6).ButtonStyle != ToolButtonStyle.Separator))
                        {
                            break;
                        }
                        control6.Visible = false;
                    }
                    for (int num6 = base.Controls.Count - 1; num6 >= 0; num6--)
                    {
                        Control control7 = base.Controls[num6];
                        if (!(control7 is McToolButton) || (((McToolButton)control7).ButtonStyle != ToolButtonStyle.Separator))
                        {
                            goto Label_027B;
                        }
                        control7.Visible = false;
                    }
                }
            }
            catch
            {
            }
        Label_027B:
            base.ResumeLayout();
        }

        public void CheckState()
        {
            if (((this.Dock == DockStyle.None) && (base.Parent != null)) && (!(base.Parent is Form) && !base.DesignMode))
            {
                base.Parent.ClientSize = new Size(base.Parent.ClientSize.Width, this.GetHeight());
            }
        }


        private int CorrectY(int y)
        {
            int num1 = ((int)Math.Round((double)(((double)y) / ((double)base.Height)))) * base.Height;
            return Math.Max(1, num1);
        }

        public void DoAutoSize()
        {
            int num1 = 0;
            foreach (Control control1 in base.Controls)
            {
                num1 = Math.Max(control1.Right, num1);
            }
            base.Width = num1 + 4;
        }

        private void DrawDot(Graphics graphics, int x, int y)
        {
            int num1 = 2;
            new Rectangle(x, y, num1, num1);
            graphics.FillRectangle(SystemBrushes.ControlLightLight, x + 1, y + 1, num1, num1);
            graphics.FillRectangle(SystemBrushes.ControlDarkDark, x, y, num1, num1);
        }


        private int GetFreePos(int x, int y)
        {
            int num1 = x;
            if (base.Parent != null)
            {
                foreach (Control control1 in base.Parent.Controls)
                {
                    if (control1 == this)
                    {
                        continue;
                    }
                    McToolBar bar1 = control1 as McToolBar;
                    if ((((bar1 != null) && bar1.Visible) && ((bar1.Top == y) && (x >= bar1.Left))) && (num1 < bar1.Right))
                    {
                        num1 = bar1.Right;
                    }
                }
            }
            return num1;
        }


        private int GetHeight()
        {
            int num1 = 0;
            if (base.Parent != null)
            {
                foreach (Control control1 in base.Parent.Controls)
                {
                    McToolBar bar1 = control1 as McToolBar;
                    if (((bar1 != null) && bar1.Visible) && (bar1.Bottom > num1))
                    {
                        num1 = bar1.Bottom;
                    }
                }
            }
            return num1+1;
        }

        private int GetMaxVerticalPos()
        {
            int num1 = 0;
            foreach (Control control1 in base.Parent.Controls)
            {
                if (control1 != this)
                {
                    num1 = Math.Max(control1.Bottom, num1);
                }
            }
            return num1;
        }

        public void PlaceOnControl()
        {
            int num2;
            base.Top = 0;
            Control control1 = base.Parent;
            int num1 = 0;
        Label_0010:
            num2 = 0;
            foreach (Control control2 in control1.Controls)
            {
                if ((control2.Top == num1) && (control2 != this))
                {
                    num2 = Math.Max(control2.Right, num2);
                }
            }
            if ((control1.Width > (num2 + base.Width)) || (num2 == 0))
            {
                base.Left = num2;
                base.Top = num1;
            }
            else
            {
                num1 += base.Height;
                goto Label_0010;
            }
        }

        #endregion

        #region override

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (this.Parent != null && this.Parent is McToolBarContainer)
            {
                ((McToolBarContainer)this.Parent).SelectedToolBar = this;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (((e.Button & MouseButtons.Left) > MouseButtons.None) && this.IsOverTitle)
            {
                this.lastPos = Cursor.Position;
                this.IsDragging = true;
                this.movePos = base.Location;
            }
            if (this.Parent != null && this.Parent is McToolBarContainer)
            {
                ((McToolBarContainer)this.Parent).SelectedToolBar = this;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (DesignMode)
            {
                base.OnMouseMove(e);
                return;
            }
            if (this.IsDragging)
            {
                if (this.Dock == DockStyle.None)
                {
                    this.movePos.Offset(Cursor.Position.X - this.lastPos.X, Cursor.Position.Y - this.lastPos.Y);
                    if (this.ChangePos)
                    {
                        base.Location = this.CheckLocation(new Point(this.movePos.X, this.CorrectY(this.movePos.Y)));
                    }
                    this.lastPos = Cursor.Position;
                }
                this.CheckState();
            }
            if (this.Dock == DockStyle.None)
            {
                if (this.IsOverTitle)
                {
                    Cursor.Current = Cursors.SizeAll;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.IsDragging = false;
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs p)
        {
            base.OnPaint(p);
            Graphics graphics1 = p.Graphics;
            Rectangle rectangle1 = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
            //using (Brush brush1 = McBrushes.GetControlBrush(rectangle1, 90f))
            //{
            //	graphics1.FillRectangle(brush1, rectangle1);
            //}
            using (Pen pen1 = new Pen(McPaint.Light(SystemColors.Control, 30)))
            {
                using (Pen pen2 = new Pen(McPaint.Dark(SystemColors.Control, 30)))
                {
                    graphics1.DrawLine(pen2, rectangle1.X, rectangle1.Bottom - 1, rectangle1.Right - 1, rectangle1.Bottom - 1);
                    graphics1.DrawLine(pen2, rectangle1.Right - 1, rectangle1.Bottom - 1, rectangle1.Right - 1, rectangle1.Y);
                }
            }
            if (this.allowMove)
            {
                int num1 = (base.Height - 0x10) / 2;
                this.DrawDot(graphics1, 4, num1);
                this.DrawDot(graphics1, 4, num1 + 4);
                this.DrawDot(graphics1, 4, num1 + 8);
                this.DrawDot(graphics1, 4, num1 + 12);
            }
        }


        protected override void OnPaintBackground(PaintEventArgs p)
        {
        }


        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            McColors.InitColors();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            this.CheckState();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.fixSize)
                this.Height = defaultHeight;
        }

        protected virtual void OnImageListChange(EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is McToolButton)
                {
                    McToolButton btn = c as McToolButton;
                    if (btn.ButtonStyle != ToolButtonStyle.Separator)
                    {
                        btn.ImageList = this.ImageList;
                        if (this.ImageList == null)
                        {
                            btn.ImageIndex = -1;
                        }
                    }
                }
            }
        }

        public void InvokeButtonClick(McToolButton button)
        {
            ToolButtonClickEventArgs e = new ToolButtonClickEventArgs(button);
            OnButtonClick(e);
            if (this.ButtonClick != null)
            {
                this.ButtonClick(this, e);
            }
        }

        protected virtual void OnButtonClick(ToolButtonClickEventArgs e)
        {

        }

        internal void InvokeButtonChecked(McToolButton button)
        {
            string groupName = button.OptionGroup;
            foreach (McToolButton tb in this.Controls)
            {
                if (tb.OptionGroup == groupName && tb != button && tb.Checked)
                {
                    tb.Checked = false;
                    break;
                }
            }

            InvokeButtonClick(button);
        }


        #endregion

        #region private Methods

        private void SetLayoutNormal()
        {
            base.DockPadding.Left = 3;
            base.DockPadding.Top = 3;
            base.DockPadding.Bottom = 3;
            base.DockPadding.Right = 3;
        }

        private void SetLayoutTitle()
        {
            base.DockPadding.Left = 12;
            base.DockPadding.Top = 3;
            base.DockPadding.Bottom = 3;
            base.DockPadding.Right = 3;
        }
        #endregion

        #region Properties

        [Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoChildrenStyle
        {
            get { return base.AutoChildrenStyle; }
            set { base.AutoChildrenStyle = value; }
        }

        //[Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override GradientStyle GradientStyle
        //{
        //    get { return base.GradientStyle; }
        //    set { base.GradientStyle = value; }
        //}

        [Description("ButtonImageList"), DefaultValue((string)null), Category("Appearance")]
        public ImageList ImageList
        {
            get
            {
                return this.m_ImageList;
            }
            set
            {
                if (this.m_ImageList != value)
                {
                    this.m_ImageList = value;
                    OnImageListChange(EventArgs.Empty);
                    base.Invalidate();
                }
            }
        }


        protected virtual bool IsOverTitle
        {
            get
            {
                if (!this.allowMove)
                {
                    return false;
                }
                Point point1 = base.PointToClient(Cursor.Position);
                if (((point1.X >= 0) && (point1.X <= 6)) && ((point1.Y >= 0) && (point1.Y <= base.Height)))
                {
                    return true;
                }
                return false;
            }
        }

        protected Point LastPos
        {
            get
            {
                return this.lastPos;
            }
            set
            {
                this.lastPos = value;
            }
        }

        protected Point MovePos
        {
            get
            {
                return this.movePos;
            }
            set
            {
                this.movePos = value;
            }
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool FixSize
        {
            get
            {
                return this.fixSize;
            }
            set
            {
                this.fixSize = value;
                if (this.fixSize)
                    this.Height = defaultHeight;

            }
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool AllowMove
        {
            get
            {
                return this.allowMove;
            }
            set
            {
                this.allowMove = value;
                if (this.allowMove)
                    base.DockPadding.Left = 12;
                else
                    base.DockPadding.Left = 2;
                this.Invalidate();
            }
        }


        #endregion



        //[Editor("Nistec.WinForms.ToolButtonCollectionEditor", typeof(System.Drawing.Design.UITypeEditor))]
        //public ControlCollection Items
        //{
        //    get { return this.Controls; }
        //}

        //protected override void OnControlAdded(ControlEventArgs e)
        //{
        //    base.OnControlAdded(e);

        //    McToolButton c = (McToolButton)e.Control;
        //    c.Dock = DockStyle.Left;
        //    //c.ParentBar = this;
        //    //c.Text = "";
        //    this.Controls.SetChildIndex(c, 0);
        //}

        //protected override void OnControlRemoved(ControlEventArgs e)
        //{
        //    this.Controls.Clear();
        //    base.OnControlRemoved(e);
        //}


        //public void AddToolButtons(McToolButton[] range)
        //{
        //    if (this.DesignMode)
        //        return;

        //    this.Controls.Clear();
        //    this.Controls.AddRange(range);

        //    //foreach (McToolButton tb in range)
        //    //{
        //    //    McToolButton c = (McToolButton)e.Control;
        //    //    c.Dock = DockStyle.Left;
        //    //    //c.ParentBar = this;
        //    //    //c.Text = "";
        //    //    this.Controls.SetChildIndex(c, 0);

        //    //}

        //}

   
        [Browsable(false)]
        public int SelectedGroup
        {
            get { return selectedGroup; }
            set 
            {
                if (selectedGroup != value)
                {
                    selectedGroup = value;

                    if (this.DesignMode)
                    {
                        return;
                    }
                    if (value < 0)
                    {
                        foreach (McToolButton tb in this.Controls)
                        {
                            tb.Visible = true;
                        }
                        return;
                    }
                    foreach (McToolButton tb in this.Controls)
                    {
                        tb.Visible = (tb.GroupIndex == 0 || tb.GroupIndex == value);
                    }

                }
            }
        }
    }

}
