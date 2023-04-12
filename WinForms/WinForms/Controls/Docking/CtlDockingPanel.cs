using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Text;

using Nistec.Drawing;

namespace Nistec.WinForms
{
    //public enum DockingTabStyle
    //{
    //    Button,
    //    Polygon
    //}

	[Designer(typeof(Design.McDockingPanelDesigner)), ToolboxItem(false)]
	public class McDockingPanel : McPanel
	{

		private const int maxWidth=800;
        private const int autoHideInterval = 1000;

		// Fields
		private bool autoHide;
		internal const int AutoHideHeight = 0x17;
		private Timer autoHideTimer;
		internal const int buttonWidth = 20;
		private bool collapsed;
		private Size collapsedSize;
		private Rectangle dockingWindowRectanle;
		private McFloatingForm floatingForm;
		internal const int FloatingHeight = 300;
		internal const int FloatingWidth = 200;
		private Image imageAutoHideOff;
		private Image imageAutoHideOn;
		private Image imageClose;
		private bool isAutoHideButtonHover;
		private bool isAutoHideButtonPressed;
		private bool isCloseButtonHover;
		private bool isCloseButtonPressed;
		private bool isDockingControl;
		private bool isDockingWindowActive;
		private bool isFloatingMoving;
		private bool isMoving;
		internal bool IsResizing;
		private bool isSelected;
		internal DockStyle LastDock;
		internal Form LastDockForm;
		private Point lastPosition;
		private McDocking manager;
		private Rectangle panelBounds;
		internal const int ResizeBarSize = 4;
		private Rectangle resizeBounds;
		private McDockingTab selectedControl;
		private bool showCloseButton;
		private bool showHideButton;
		private Rectangle stopDockingRectanle;
		internal const int TabControlHeight = 20;
		internal const int TabHeight = 0x18;
		private Rectangle tabsBounds;
		private Rectangle titleBounds;
		internal const int TitleHeight = 20;
		//private DockingTabStyle dockingTabStyle=DockingTabStyle.Button;
        private bool isMouseDown;

        private BitArray DockingState;

        private enum DockState
        {
            Title,
            HideBar,
            Tabs
        }

		public McDockingPanel() : this(null)
		{
		}

		public McDockingPanel(McDocking manager)
		{
            this.DockingState = new BitArray(3);
            DockingState[0] = false;
            DockingState[1] = false;
            DockingState[2] = false;
            isMouseDown = false;
			this.manager = null;
			this.collapsed = false;
			this.isSelected = false;
			this.isAutoHideButtonHover = false;
			this.isCloseButtonHover = false;
			this.isAutoHideButtonPressed = false;
			this.isCloseButtonPressed = false;
			this.autoHide = false;
			this.showCloseButton = true;
			this.showHideButton = true;
			this.selectedControl = null;
			this.IsResizing = false;
			this.isDockingWindowActive = false;
			this.dockingWindowRectanle = Rectangle.Empty;
			this.isDockingControl = false;
			this.isFloatingMoving = false;
			this.isMoving = false;
			this.autoHideTimer = new Timer();
			this.LastDockForm = null;
			this.manager = manager;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			this.Dock = DockStyle.None;
			this.AllowDrop = true;
            this.autoHideTimer.Interval = autoHideInterval;
			this.autoHideTimer.Tick += new EventHandler(this.OnAutoHideTick);
			this.imageClose = McPaint.GetImage(typeof(McDockingPanel), "Nistec.WinForms.Controls.Docking.Close.bmp");
            this.imageAutoHideOn = McPaint.GetImage(typeof(McDockingPanel), "Nistec.WinForms.Controls.Docking.AutoHideOn.bmp");
            this.imageAutoHideOff = McPaint.GetImage(typeof(McDockingPanel), "Nistec.WinForms.Controls.Docking.AutoHideOff.bmp");

            //base.SetStyle(ControlStyles.ResizeRedraw, true);
            //base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //base.SetStyle(ControlStyles.DoubleBuffer, true);
            //base.SetStyle(ControlStyles.UserPaint, true);
			this.SetLayout();
		}

 
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.imageClose != null)
				{
					this.imageClose.Dispose();
				}
				if (this.imageAutoHideOn != null)
				{
					this.imageAutoHideOn.Dispose();
				}
				if (this.imageAutoHideOff != null)
				{
					this.imageAutoHideOff.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		[Category("Style"),Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoChildrenStyle
		{
			get{return base.AutoChildrenStyle;}
			set{base.AutoChildrenStyle=value;}
		}

        //[Category("Style"),Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override GradientStyle GradientStyle
        //{
        //    get{return base.GradientStyle;}
        //    set{base.GradientStyle=value;}
        //}

		public void DockControlToForm(McDockingTab control, Form form, DockStyle dockType)
		{
			McDockingPanel panel1 = this.UndockControl(control, Rectangle.Empty, false, null);
			panel1.DockPanelToForm(form, dockType);
			panel1.Focus();
		}

 
		public void DockControlToPanel(McDockingPanel panel)
		{
			this.UndockControl(this.SelectedTab, Rectangle.Empty, false, base.Parent).DockPanelToPanel(panel);
		}

 
		internal void DockPanel(Form form)
		{
			form.Controls.Add(this);
			this.Manager.RemoveUndockingPanels(this);
			this.SetLayout();
			base.Focus();
			this.LastDockForm = null;
		}

 
		public void DockPanelToForm(Form form, DockStyle dockType)
		{
			McDockingPanel panel1 = null;
            if (form == null || dockType== DockStyle.None)
                return;
			foreach (Control control1 in form.Controls)
			{
				if ((control1 is McDockingPanel) && (control1.Dock == dockType))
				{
					panel1 = control1 as McDockingPanel;
					break;
				}
			}
			if (panel1 == null)
			{
				panel1 = new McDockingPanel(this.Manager);
				panel1.Width = 200;
				panel1.Height = 150;
				panel1.Dock = dockType;
				panel1.LastDockForm = null;
				form.Controls.Add(panel1);
				form.Controls.SetChildIndex(panel1, 0);
				foreach (Control control2 in form.Controls)
				{
					if (control2.Dock == DockStyle.Fill)
					{
						form.Controls.SetChildIndex(control2, 0);
						break;
					}
				}
			}
			this.DockPanelToPanel(panel1);
		}

		public void DockPanelToPanel(McDockingPanel panel)
		{
			ArrayList list1 = new ArrayList();
			foreach (Control control1 in base.Controls)
			{
				list1.Add(control1);
			}
			panel.Controls.AddRange((Control[]) list1.ToArray(typeof(Control)));
			if (base.Parent != null)
			{
				base.Parent.Controls.Remove(this);
			}
			panel.SetLayout();
			if (this.FloatingForm != null)
			{
				panel.manager.RemoveUndockingPanels(this);
			}
			base.Focus();
		}

 
		public void DoClose(Control control)
		{
            if (control == null)
                return;
			CancelEventArgs args1 = new CancelEventArgs(false);
			((McDockingTab) control).InvokeClosing(args1);
			if (!args1.Cancel)
			{
				this.manager.ClosedControls.Add(control);
				if (this.floatingForm != null)
				{
					base.Controls.Remove(control);
					if (base.Controls.Count == 0)
					{
						this.manager.RemoveUndockingPanels(this);
						base.Parent.Controls.Remove(this);
					}
					((McDockingTab) control).InvokeClosed(EventArgs.Empty);
					this.SetLayoutAllPanels();
				}
				else if ((base.Controls.Count > 1) && (control != null))
				{
					base.Controls.Remove(control);
					((McDockingTab) control).InvokeClosed(EventArgs.Empty);
					base.Invalidate();
				}
				else
				{
					if (base.Parent != null)
					{
						base.Parent.Controls.Remove(this);
					}
					((McDockingTab) control).InvokeClosed(EventArgs.Empty);
				}
			}
		}

		private void DoDock()
		{
			if (this.isDockingControl && (base.Controls.Count > 1))
			{
				if (McDockingWindow.ControlToDock is Form)
				{
					this.DockControlToForm(this.SelectedTab, McDockingWindow.ControlToDock as Form, McDockingWindow.DockType);
				}
				else
				{
					this.DockControlToPanel(McDockingWindow.ControlToDock as McDockingPanel);
				}
			}
			else if (McDockingWindow.ControlToDock is Form)
			{
				this.DockPanelToForm(McDockingWindow.ControlToDock as Form, McDockingWindow.DockType);
			}
			else
			{
				this.DockPanelToPanel(McDockingWindow.ControlToDock as McDockingPanel);
			}
			this.SetLayoutAllPanels();
		}

 
        //private void DrawDockingControl(Graphics g, McDockingTab dockingControl)
        //{

        //    Color color1;
        //    Rectangle rectangle1 = this.GetTabRectangle(g, dockingControl);
        //    Point[] pointArray1 = new Point[] { new Point(rectangle1.X, rectangle1.Y), new Point(rectangle1.X, rectangle1.Bottom), new Point(rectangle1.Right - 5, rectangle1.Bottom), new Point(rectangle1.Right + 5, rectangle1.Y) };
        //    GraphicsPath path1 = new GraphicsPath(FillMode.Alternate);
        //    path1.AddPolygon(pointArray1);
        //    Point point1 = base.PointToClient(Cursor.Position);
        //    if (this.SelectedTab == dockingControl)
        //    {
        //        if (rectangle1.Contains(point1))
        //        {
        //            using (Brush brush1 = base.LayoutManager.GetBrushGradient(rectangle1, 90f))//McBrushes.GetControlLightBrush(rectangle1, 90f))
        //            {
        //                g.FillPath(brush1, path1);
        //                goto Label_014D;
        //            }
        //        }
        //        using (Brush brush2 = base.LayoutManager.GetBrushGradient(rectangle1, 90f))//McBrushes.GetControlBrush(rectangle1, 90f))
        //        {
        //            g.FillPath(brush2, path1);
        //            goto Label_014D;
        //        }
        //    }
        //    if (rectangle1.Contains(point1))
        //    {
        //        using (Brush brush3 =base.LayoutManager.GetBrushSelected())// new SolidBrush(McPaint.Light(McColors.Content, 15)))
        //        {
        //            g.FillPath(brush3, path1);
        //            goto Label_014D;
        //        }
        //    }
        //    g.FillPath(McBrushes.Content, path1);
        //    Label_014D:
        //        color1 = this.ForeColor;
        //    if (dockingControl != this.SelectedTab)
        //    {
        //        color1 = SystemColors.ControlDark;
        //    }
        //    int num1 = 0;
        //    if (dockingControl.Image != null)
        //    {
        //        num1 = dockingControl.Image.Width + 2;
        //        Rectangle rectangle2 = new Rectangle(rectangle1.X + 2, rectangle1.Y + ((rectangle1.Height - dockingControl.Image.Height) / 2), dockingControl.Image.Width, dockingControl.Image.Height);
        //        if (num1 != 0)
        //        {
        //            g.DrawImage(dockingControl.Image, rectangle2);
        //        }
        //    }
        //    Rectangle rectangle3 = new Rectangle(rectangle1.X + num1, rectangle1.Y, rectangle1.Width - num1, rectangle1.Height);
        //    using (Brush brush4 = new SolidBrush(color1))
        //    {
        //        using (StringFormat format1 = this.GetStringFormatTabs())
        //        {
        //            g.DrawString(dockingControl.Text, this.Font, brush4, (RectangleF) rectangle3, format1);
        //        }
        //    }
        //    if (this.SelectedTab == dockingControl)
        //    {
        //        g.DrawLine(SystemPens.ControlLightLight, pointArray1[0], pointArray1[1]);
        //        g.DrawLine(SystemPens.ControlDark, pointArray1[1], pointArray1[2]);
        //        g.DrawLine(SystemPens.ControlDark, pointArray1[2], pointArray1[3]);
        //        g.DrawLine(SystemPens.ControlDark, this.TabsBounds.X, pointArray1[0].Y, pointArray1[0].X, pointArray1[0].Y);
        //        g.DrawLine(SystemPens.ControlDark, pointArray1[3].X, pointArray1[0].Y, this.TabsBounds.Right - 1, pointArray1[0].Y);
        //    }
        //    else
        //    {
        //        using (Pen pen1 = new Pen(McPaint.Dark(SystemColors.Control, 40)))
        //        {
        //            g.DrawLine(pen1, pointArray1[0], pointArray1[1]);
        //            g.DrawLine(pen1, pointArray1[1], pointArray1[2]);
        //            g.DrawLine(pen1, pointArray1[2], pointArray1[3]);
        //        }
        //    }
        //}

 
		private void DrawDockingPanel(Graphics g, McDockingTab dockingControl)
		{
			Color color1;
			Rectangle rect = this.GetTabRectangle(g, dockingControl);
			Rectangle rectangle1=new Rectangle(rect.X+1,rect.Y+2,rect.Width-2,rect.Height);
			Point point1 = base.PointToClient(Cursor.Position);
			if (this.SelectedTab == dockingControl)
			{
				if (rectangle1.Contains(point1))
				{
					using (Brush brush1 = base.LayoutManager.GetBrushGradient(rectangle1, 90f))//  McBrushes.GetControlLightBrush(rectangle1, 90f))
					{
						g.FillRectangle(brush1,rectangle1);
						goto Label_014D;
					}
				}
				using (Brush brush2 = base.LayoutManager.GetBrushGradient(rectangle1, 90f))//McBrushes.GetControlBrush(rectangle1, 90f))
				{
					g.FillRectangle(brush2,rectangle1);
					goto Label_014D;
				}
			}
			if (rectangle1.Contains(point1))
			{
				using (Brush brush3 =base.LayoutManager.GetBrushSelected())
				{
					g.FillRectangle(brush3,rectangle1);
					goto Label_014D;
				}
			}
			g.FillRectangle(McBrushes.Content,rectangle1);
			Label_014D:
				color1 = this.ForeColor;
			if (dockingControl != this.SelectedTab)
			{
				color1 = SystemColors.ControlDark;
			}
			int num1 = 0;
			if (dockingControl.Image != null)
			{
				num1 = dockingControl.Image.Width + 2;
				Rectangle rectangle2 = new Rectangle(rectangle1.X + 2, rectangle1.Y + ((rectangle1.Height - dockingControl.Image.Height) / 2), dockingControl.Image.Width, dockingControl.Image.Height);
				if (num1 != 0)
				{
					g.DrawImage(dockingControl.Image, rectangle2);
				}
			}
			Rectangle rectangle3 = new Rectangle(rectangle1.X + num1, rectangle1.Y, rectangle1.Width - num1, rectangle1.Height);
			using (Brush brush4 = new SolidBrush(color1))
			{
				using (StringFormat format1 = this.GetStringFormatTabs())
				{
					g.DrawString(dockingControl.Text, this.Font, brush4, (RectangleF) rectangle3, format1);
				}
			}
			
			if (this.SelectedTab == dockingControl)
			{
				g.DrawRectangle(SystemPens.ControlDark,rectangle1);
				g.DrawLine(SystemPens.ControlDark, this.TabsBounds.X, rectangle1.Y-2, this.TabsBounds.Right-1, rectangle1.Y-2);

			}
			else
			{
				using (Pen pen1 = new Pen(ControlPaint.Dark(SystemColors.Control, 40)))
				{
					g.DrawRectangle(pen1,rectangle1);
				}
			}
		}

		internal Rectangle GetAutoHideControlBounds(McDockingTab dockingControl)
		{
			using (Graphics graphics1 = Graphics.FromHwnd(base.Handle))
			{
				int num1 = 0;
				int num2 = 0;
				switch (this.Dock)
				{
					case DockStyle.Top:
					case DockStyle.Bottom:
						break;

					case DockStyle.Left:
					case DockStyle.Right:
						num2 = this.AutoHideBarBounds.Height;
						goto Label_0055;

					default:
						goto Label_0055;
				}
				num2 = this.AutoHideBarBounds.Width;
			Label_0055:
				foreach (McDockingTab control1 in base.Controls)
				{
					Rectangle rectangle1 = this.GetTabRectangle(graphics1, control1, num2);
                    if (rectangle1 == Rectangle.Empty)
                    {
                        return Rectangle.Empty;
                    }
                    rectangle1.Width -= 10;
					if (control1 != this.SelectedTab)
					{
						rectangle1.Width = 0x10;
					}
					if (control1 == dockingControl)
					{
						switch (this.Dock)
						{
							case DockStyle.Top:
								return new Rectangle(num1, -1, rectangle1.Width + 5, 20);

							case DockStyle.Bottom:
								return new Rectangle(num1, base.Height - 20, rectangle1.Width + 5, 20);

							case DockStyle.Left:
								return new Rectangle(-1, num1, 20, rectangle1.Width + 5);

							case DockStyle.Right:
								return new Rectangle(base.Width - 20, num1, 20, rectangle1.Width + 5);
						}
					}
					num1 += rectangle1.Width + 8;
				}
				return Rectangle.Empty;
			}
		}

		private McDockingTab GetControlAt(int x, int y)
		{
			if (this.AutoHide)
			{
				foreach (McDockingTab control1 in base.Controls)
				{
					Rectangle rectangle1 = this.GetAutoHideControlBounds(control1);
					if (rectangle1.Contains(x, y))
					{
						return control1;
					}
				}
			}
			return null;
		}

 
		public McDockingTab GetDockingControlAt(int x, int y)
		{
			using (Graphics graphics1 = Graphics.FromHwnd(base.Handle))
			{
				foreach (McDockingTab control1 in base.Controls)
				{
					Rectangle rectangle1 = this.GetTabRectangle(graphics1, control1);
					if (rectangle1.Contains(x, y))
					{
						return control1;
					}
				}
				return null;
			}
		}
        internal bool IsTitleSelected()
        {
            return this.titleBounds.Contains(Cursor.Position);
        }
 
		private int GetMaximumSize()
		{
			switch (this.Dock)
			{
				case DockStyle.Bottom:
					if (!this.AutoHide)
					{
						return (base.Bounds.Bottom - 0x36);
					}
					return ((base.Bounds.Bottom - this.AutoHideBarSize) - 0x36);

				case DockStyle.Left:
					if (!this.AutoHide)
					{
						return (maxWidth + base.Left);
					}
					return ((this.AutoHideBarSize + maxWidth) + base.Left);

				case DockStyle.Right:
					if (!this.AutoHide)
					{
						return (base.Bounds.Right - 0x22);
					}
					return ((base.Bounds.Right - this.AutoHideBarSize) - 0x22);
			}
			if (!this.AutoHide)
			{
				return (maxWidth + base.Top);
			}
			return ((this.AutoHideBarSize + maxWidth) + base.Top);
		}

		private int GetMinimumSize()
		{
			switch (this.Dock)
			{
				case DockStyle.Bottom:
					if (!this.AutoHide)
					{
						return (base.Bounds.Bottom - maxWidth);
					}
					return ((base.Bounds.Bottom - this.AutoHideBarSize) - maxWidth);

				case DockStyle.Left:
					if (!this.AutoHide)
					{
						return (base.Left + 30);
					}
					return ((base.Left + this.AutoHideBarSize) + 30);

				case DockStyle.Right:
					if (!this.AutoHide)
					{
						return (base.Bounds.Right - maxWidth);
					}
					return ((base.Bounds.Right - this.AutoHideBarSize) - maxWidth);
			}
			if (!this.AutoHide)
			{
				return (base.Top + 50);
			}
			return ((base.Top + this.AutoHideBarSize) + 50);
		}

		internal Cursor GetResizeCursor()
		{
			if (!this.Collapsed)
			{
				switch (this.Dock)
				{
					case DockStyle.Top:
					case DockStyle.Bottom:
						return Cursors.HSplit;

					case DockStyle.Left:
					case DockStyle.Right:
						return Cursors.VSplit;
				}
			}
			return Cursors.Arrow;
		}

 
		private StringFormat GetStringFormatTabs()
		{
			StringFormat format1 = new StringFormat();
			format1.LineAlignment = StringAlignment.Center;
			format1.Alignment = StringAlignment.Center;
			format1.FormatFlags = StringFormatFlags.NoWrap;
			format1.Trimming = StringTrimming.EllipsisCharacter;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			return format1;
		}

 
		internal Rectangle GetTabRectangle(Graphics g, McDockingTab dockingControl)
		{
			return this.GetTabRectangle(g, dockingControl, 0);
		}

		internal Rectangle GetTabRectangle(Graphics g, McDockingTab dockingControl, int size)
		{
			int num1 = 5 + this.TabsBounds.X;
			int num2 = 10;
			int width = this.GetTotalWidth(g);
            if (width <= 0)
            {
                return Rectangle.Empty;
            }
            if (size == 0)
			{
				size = this.TabsBounds.Width;
			}
			double num4 = 1;
			if (((width + (num1 * 2)) + 5) > size)
			{
                num4 = ((double)size) / ((width + (num1 * 2)) + 5);
			}
			foreach (McDockingTab control1 in base.Controls)
			{
				if (control1 != dockingControl)
				{
					num1 += (int) (this.GetTabWidth(g, control1) * num4);
				}
				else
				{
					num2 = (int) (this.GetTabWidth(g, control1) * num4);
					break;
				}
			}
			base.Controls.IndexOf(this.SelectedTab);
			base.Controls.IndexOf(dockingControl);
			Rectangle rectangle1 = Rectangle.Empty;
			return new Rectangle(num1, this.TabsBounds.Y, num2, 20);
		}

		private int GetTabWidth(Graphics g, McDockingTab control)
		{
			int num1 = 0;
            if (!(control is McDockingTab))
            {
                return 0;
            }
            if (control.Image != null)
			{
				num1 = control.Image.Width;
			}
			using (StringFormat format1 = this.GetStringFormatTabs())
			{
				SizeF ef1 = g.MeasureString(control.Text, this.Font, 0x3e8, format1);
				return ((((int) ef1.Width) + 10) + num1);
			}
		}

		private int GetTotalWidth(Graphics g)
		{
			int num1 = 0;
            if (base.Controls.Count == 0)
            {
                return 0;
            }
			foreach (McDockingTab control1 in base.Controls)
			{
				num1 += this.GetTabWidth(g, control1);
			}
			return num1;
		}

 
		private void InvalidateAutoHideBar()
		{
			if ((this.AutoHideBarBounds.Width != 0) && (this.AutoHideBarBounds.Height != 0))
			{
				base.Invalidate(this.AutoHideBarBounds);
			}
		}

 
		internal void InvalidateTabs()
		{
			if ((this.TabsBounds.Width != 0) && (this.TabsBounds.Height != 0))
			{
				base.Invalidate(this.TabsBounds);
			}
		}

		internal void InvalidateTitle()
		{
			if ((this.TitleBounds.Width != 0) && (this.TitleBounds.Height != 0))
			{
				base.Invalidate(this.TitleBounds);
			}
		}

        [Browsable(false)]
        public bool IsMouseHover
        {
            get
            {
                try
                {
                    Point mPos = Control.MousePosition;
                    bool retVal = this.ClientRectangle.Contains(this.PointToClient(mPos));
                    return retVal;
                }
                catch { return false; }
            }
        }

		private void OnAutoHideTick(object sender, EventArgs e)
		{
            if ((((base.Parent != null) && this.AutoHide) && (!this.Collapsed /*&& !base.ContainsFocus*/)) && !IsMouseHover)//!base.ClientRectangle.Contains(base.PointToClient(Cursor.Position)))
			{
				this.autoHideTimer.Enabled = false;
				this.Collapsed = true;
				this.SetLayoutAllPanels();
			}
		}

 
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			if (this.SelectedTab == null)
			{
				this.SelectedTab = e.Control as McDockingTab;
			}
			e.Control.Dock = DockStyle.Fill;
			this.SetLayout();
			base.Invalidate();
		}

 
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);
			if (this.SelectedTab == e.Control)
			{
				if (base.Controls.Count == 0)
				{
					this.SelectedTab = null;
				}
				else
				{
					this.SelectedTab = base.Controls[0] as McDockingTab;
				}
			}
			this.SetLayout();
			base.Invalidate();
		}

 
		protected override void OnDockChanged(EventArgs e)
		{
			base.OnDockChanged(e);
			this.SetLayout();
		}

 
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			base.Invalidate();
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			base.Invalidate();
			if (this.AutoHide)
			{
				this.autoHideTimer.Enabled = true;
			}
		}

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if ((e.Button & MouseButtons.Left) > MouseButtons.None)
            {
                if (this.titleBounds.Contains(e.Location))
                {
                    if (isFloatingMoving)
                    {
                        DockPanelToForm(LastDockForm, LastDock);
                    }
                    else
                    {
                        this.UndockPanel(this.dockingWindowRectanle, true);
                    }

                }
            }
        }

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
            isMouseDown = true;
			if ((e.Button & MouseButtons.Left) > MouseButtons.None)
			{
				base.Focus();
                if (this.ShowCloseButton && this.CloseButtonBounds.Contains(e.X, e.Y) && (base.Width > 50))
				{
					this.IsCloseButtonPressed = true;
					//this.InvalidateTitle();
					return;
				}
				if ((this.ShowHideButton && (this.FloatingForm == null)) && (this.AutoHideButtonBounds.Contains(e.X, e.Y) && (base.Width > 50)))
				{
					this.IsAutoHideButtonPressed = true;
					//this.InvalidateTitle();
					return;
				}
				if (!this.AutoHide)
				{
					McDockingTab control1 = this.GetDockingControlAt(e.X, e.Y);
					if (control1 != null)
					{
						this.SelectedTab = control1;
						if ((this.Manager != null) && !this.Manager.LockDockingPanels)
						{
							this.isMoving = true;
						}
						this.stopDockingRectanle = this.TabsBounds;
						this.isDockingControl = true;
						this.isDockingWindowActive = false;
						this.dockingWindowRectanle.Location = base.PointToScreen(base.ClientRectangle.Location);
						this.dockingWindowRectanle.Width = 200;
						this.dockingWindowRectanle.Height = 300;
						this.dockingWindowRectanle.Y = Cursor.Position.Y - 5;
						if (!this.dockingWindowRectanle.Contains(Cursor.Position))
						{
							this.dockingWindowRectanle.X = Cursor.Position.X - (this.dockingWindowRectanle.Width / 2);
						}
						this.lastPosition = Cursor.Position;
						return;
					}
				}
				if (!this.AutoHide && this.TitleBounds.Contains(base.PointToClient(Cursor.Position)))
				{
					if (this.FloatingForm != null)
					{
						if ((this.Manager != null) && !this.Manager.LockDockingPanels)
						{
							this.isFloatingMoving = true;
						}
						this.isDockingWindowActive = false;
						this.isDockingControl = false;
					}
					else
					{
						if ((this.Manager != null) && !this.Manager.LockDockingPanels)
						{
							this.isMoving = true;
						}
						this.stopDockingRectanle = this.TitleBounds;
						this.isDockingControl = false;
						this.isDockingWindowActive = false;
						this.dockingWindowRectanle.Location = base.PointToScreen(base.ClientRectangle.Location);
						this.dockingWindowRectanle.Width = 200;
						this.dockingWindowRectanle.Height = 300;
						if (!this.dockingWindowRectanle.Contains(Cursor.Position))
						{
							this.dockingWindowRectanle.X = Cursor.Position.X - (this.dockingWindowRectanle.Width / 2);
						}
					}
					this.lastPosition = Cursor.Position;
				}
				if (this.ResizeBounds.Contains(e.X, e.Y) && !this.Collapsed)
				{
					McControlResize.StartResize(this, base.Parent.RectangleToClient(base.RectangleToScreen(this.ResizeBounds)), this.GetMinimumSize(), this.GetMaximumSize());
				}
				if (this.AutoHide)
				{
					this.SelectControl(e.X, e.Y);
				}
			}
			if ((e.Button & MouseButtons.Right) > MouseButtons.None)
			{
				if (this.IsResizing)
				{
					McControlResize.StopResize(true);
				}
				this.isMoving = false;
				this.isFloatingMoving = false;
				if (this.isDockingWindowActive)
				{
					this.isDockingWindowActive = false;
					McDockingWindow.StopDockingWindow();
				}
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
            DockingState[0] = false;
            DockingState[1] = false;
            DockingState[2] = false;

			if (this.AutoHide && !IsMouseHover)
			{
				this.autoHideTimer.Enabled = false;
				this.autoHideTimer.Enabled = true;
			}
			if (this.IsCloseButtonHover || this.IsAutoHideButtonHover)
			{
				this.IsCloseButtonHover = false;
				this.IsAutoHideButtonHover = false;
				this.Cursor = Cursors.Default;
			}
		}

        //protected override void OnMouseHover(EventArgs e)
        //{
        //    base.OnMouseHover(e);
        //    OnMouseHoverInternal(Cursor.Position);
        //}

        //protected void OnMouseHoverInternal(Point e)
        //{

        //    if (this.isFloatingMoving)
        //    {
        //        int num1 = this.lastPosition.X - Cursor.Position.X;
        //        int num2 = this.lastPosition.Y - Cursor.Position.Y;
        //        McFloatingForm form1 = this.FloatingForm;
        //        form1.Left -= num1;
        //        McFloatingForm form2 = this.FloatingForm;
        //        form2.Top -= num2;
        //        this.lastPosition = Cursor.Position;
        //        McDockingWindow.panel = this;
        //        Rectangle rectangle1 = McDockingWindow.GetDockRectangle(this.FloatingForm.RectangleToScreen(this.FloatingForm.ClientRectangle));
        //        if (rectangle1 != this.FloatingForm.RectangleToScreen(this.FloatingForm.ClientRectangle))
        //        {
        //            if (!this.isDockingWindowActive)
        //            {
        //                this.isDockingWindowActive = true;
        //                McDockingWindow.StartDockingWindow(this, rectangle1);
        //            }
        //            else
        //            {
        //                McDockingWindow.RefreshDockingWindow(rectangle1);
        //            }
        //        }
        //        else if (this.isDockingWindowActive)
        //        {
        //            this.isDockingWindowActive = false;
        //            McDockingWindow.StopDockingWindow();
        //        }
        //    }
        //    else if (this.isMoving && isMouseDown)
        //    {
        //        Point point1 = base.PointToClient(Cursor.Position);
        //        int num3 = this.lastPosition.X - Cursor.Position.X;
        //        int num4 = this.lastPosition.Y - Cursor.Position.Y;
        //        this.dockingWindowRectanle.X -= num3;
        //        this.dockingWindowRectanle.Y -= num4;
        //        if (this.isDockingControl)
        //        {
        //            McDockingTab control1 = this.GetDockingControlAt(e.X, e.Y);
        //            if (control1 != null)
        //            {
        //                int num5 = base.Controls.IndexOf(control1);
        //                int num6 = base.Controls.IndexOf(this.SelectedTab);
        //                base.Controls.SetChildIndex(control1, num6);
        //                base.Controls.SetChildIndex(this.SelectedTab, num5);
        //                base.Invalidate();
        //            }
        //        }
        //        if (this.stopDockingRectanle.Contains(point1))
        //        {
        //            if (this.isDockingWindowActive)
        //            {
        //                this.isDockingWindowActive = false;
        //                McDockingWindow.StopDockingWindow();
        //            }
        //        }
        //        else if (!this.isDockingWindowActive)
        //        {
        //            this.isDockingWindowActive = true;
        //            McDockingWindow.StartDockingWindow(this, this.dockingWindowRectanle);
        //        }
        //        else
        //        {
        //            McDockingWindow.RefreshDockingWindow(this.dockingWindowRectanle);
        //        }
        //        this.lastPosition = Cursor.Position;
        //    }
        //    else
        //    {
        //        this.IsCloseButtonHover = false;
        //        this.IsAutoHideButtonHover = false;
        //        this.IsCloseButtonHover = this.CloseButtonBounds.Contains(e.X, e.Y);
        //        this.IsAutoHideButtonHover = this.AutoHideButtonBounds.Contains(e.X, e.Y);

        //        if (this.TitleBounds.Contains(e.X, e.Y))
        //        {
        //            using (Graphics g = this.CreateGraphics())
        //            {
        //                PaintTitle(g);
        //            }
        //        }
        //        //if (DockingState[(int)DockState.Title] == false)
        //        //{
        //        //this.InvalidateTitle();
        //        //    DockingState[(int)DockState.Title] = true;
        //        //}
        //        //if (this.AutoHideBarBounds.Contains(e.X, e.Y))
        //        //{
        //        //if (DockingState[(int)DockState.HideBar] == false)
        //        //{
        //        //this.InvalidateAutoHideBar();
        //        //    DockingState[(int)DockState.HideBar] = true;
        //        //}
        //        //}
        //        //if (this.TabsBounds.Contains(e.X, e.Y))
        //        //{
        //        //if (DockingState[(int)DockState.Tabs] == false)
        //        //{
        //        //this.InvalidateTabs();
        //        //    DockingState[(int)DockState.Tabs] = true;
        //        //}

        //        //}
        //        if (this.IsResizing)
        //        {
        //            McControlResize.RefreshResize();
        //            Cursor.Current = this.GetResizeCursor();
        //        }
        //        else if (this.ResizeBounds.Contains(e.X, e.Y))
        //        {
        //            Cursor.Current = this.GetResizeCursor();
        //        }
        //        else
        //        {
        //            Cursor.Current = Cursors.Arrow;
        //        }
        //    }
        //}

		protected override void OnMouseMove(MouseEventArgs e)
		{
     
			base.OnMouseMove(e);

			if (this.isFloatingMoving)
			{
				int num1 = this.lastPosition.X - Cursor.Position.X;
				int num2 = this.lastPosition.Y - Cursor.Position.Y;
				McFloatingForm form1 = this.FloatingForm;
				form1.Left -= num1;
				McFloatingForm form2 = this.FloatingForm;
				form2.Top -= num2;
				this.lastPosition = Cursor.Position;
				McDockingWindow.panel = this;
				Rectangle rectangle1 = McDockingWindow.GetDockRectangle(this.FloatingForm.RectangleToScreen(this.FloatingForm.ClientRectangle));
				if (rectangle1 != this.FloatingForm.RectangleToScreen(this.FloatingForm.ClientRectangle))
				{
					if (!this.isDockingWindowActive)
					{
						this.isDockingWindowActive = true;
						McDockingWindow.StartDockingWindow(this, rectangle1);
					}
					else
					{
						McDockingWindow.RefreshDockingWindow(rectangle1);
					}
				}
				else if (this.isDockingWindowActive)
				{
					this.isDockingWindowActive = false;
					McDockingWindow.StopDockingWindow();
				}
			}
            else if (this.isMoving && isMouseDown)
			{
				Point point1 = base.PointToClient(Cursor.Position);
				int num3 = this.lastPosition.X - Cursor.Position.X;
				int num4 = this.lastPosition.Y - Cursor.Position.Y;
				this.dockingWindowRectanle.X -= num3;
				this.dockingWindowRectanle.Y -= num4;
				if (this.isDockingControl)
				{
					McDockingTab control1 = this.GetDockingControlAt(e.X, e.Y);
					if (control1 != null)
					{
						int num5 = base.Controls.IndexOf(control1);
						int num6 = base.Controls.IndexOf(this.SelectedTab);
						base.Controls.SetChildIndex(control1, num6);
						base.Controls.SetChildIndex(this.SelectedTab, num5);
						base.Invalidate();
					}
				}
				if (this.stopDockingRectanle.Contains(point1))
				{
					if (this.isDockingWindowActive)
					{
						this.isDockingWindowActive = false;
						McDockingWindow.StopDockingWindow();
					}
				}
				else if (!this.isDockingWindowActive)
				{
					this.isDockingWindowActive = true;
					McDockingWindow.StartDockingWindow(this, this.dockingWindowRectanle);
				}
				else
				{
					McDockingWindow.RefreshDockingWindow(this.dockingWindowRectanle);
				}
				this.lastPosition = Cursor.Position;
			}
			else
			{
				this.IsCloseButtonHover = false;
				this.IsAutoHideButtonHover = false;
				this.IsCloseButtonHover = this.CloseButtonBounds.Contains(e.X, e.Y);
				this.IsAutoHideButtonHover = this.AutoHideButtonBounds.Contains(e.X, e.Y);

                if (!collapsed && this.TitleBounds.Contains(e.X, e.Y))
                {
                    using (Graphics g = this.CreateGraphics())
                    {
                        PaintTitle(g);
                    }
                }
                //if (DockingState[(int)DockState.Title] == false)
                //{
                    //this.InvalidateTitle();
                //    DockingState[(int)DockState.Title] = true;
                //}
                //if (this.AutoHideBarBounds.Contains(e.X, e.Y))
                //{
                    //if (DockingState[(int)DockState.HideBar] == false)
                    //{
                        //this.InvalidateAutoHideBar();
                    //    DockingState[(int)DockState.HideBar] = true;
                    //}
                //}
                //if (this.TabsBounds.Contains(e.X, e.Y))
                //{
                    //if (DockingState[(int)DockState.Tabs] == false)
                    //{
                        //this.InvalidateTabs();
                    //    DockingState[(int)DockState.Tabs] = true;
                    //}
                   
                //}
				if (this.IsResizing)
				{
					McControlResize.RefreshResize();
					Cursor.Current = this.GetResizeCursor();
				}
				else if (this.ResizeBounds.Contains(e.X, e.Y))
				{
					Cursor.Current = this.GetResizeCursor();
				}
				else
				{
					Cursor.Current = Cursors.Arrow;
				}
			}
		}

 
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
            
			if ((e.Button & MouseButtons.Left) > MouseButtons.None)
			{
				if (this.isMoving && this.isDockingWindowActive)
				{
					this.isDockingWindowActive = false;
					McDockingWindow.StopDockingWindow();
					Rectangle rectangle1 = McDockingWindow.GetDockRectangle(this.dockingWindowRectanle);
					if (rectangle1 != this.dockingWindowRectanle)
					{
						this.DoDock();
					}
					else
					{
						if (this.isDockingControl && (base.Controls.Count > 1))
						{
							this.UndockControl(this.SelectedTab, this.dockingWindowRectanle, true, base.Parent);
						}
						else
						{
							this.UndockPanel(this.dockingWindowRectanle, true);
						}
						this.SetLayoutAllPanels();
					}
				}
				else if (this.isFloatingMoving && this.isDockingWindowActive)
				{
					this.isDockingWindowActive = false;
					McDockingWindow.StopDockingWindow();
					Rectangle rectangle2 = McDockingWindow.GetDockRectangle(this.dockingWindowRectanle);
					if (!rectangle2.IsEmpty)
					{
						this.DoDock();
					}
				}
				if (this.ShowCloseButton && this.IsCloseButtonPressed)
				{
					this.IsCloseButtonPressed = false;
					this.InvalidateTitle();
					if (this.IsCloseButtonHover && !this.Collapsed)
					{
						if (this.floatingForm != null)
						{
							while (base.Controls.Count > 0)
							{
								this.DoClose(this.SelectedTab);
							}
						}
						else
						{
							this.DoClose(this.SelectedTab);
						}
						this.SetLayoutAllPanels();
					}
				}
				if (this.IsAutoHideButtonPressed)
				{
					this.IsAutoHideButtonPressed = false;
					this.InvalidateTitle();
					if (this.IsAutoHideButtonHover)
					{
						this.AutoHide = !this.AutoHide;
						this.SetLayoutAllPanels();
					}
				}
				if (this.IsResizing)
				{
					McControlResize.StopResize();
					this.SetLayout();
				}
				this.isFloatingMoving = false;
                this.isMoving = false; 
                isMouseDown = false;
			}
		}


        protected void PaintTitle(Graphics graphics1)
        {
            //Graphics graphics1 = e.Graphics;

            if ((this.TitleBounds.IsEmpty || (this.TitleBounds.Width == 0)) || (this.TitleBounds.Height == 0))
            {
               return;
            }
            Color color1 = SystemColors.ControlText;
            if (base.ContainsFocus || this.IsSelected)
            {
                using (Brush brush1 = base.LayoutManager.GetBrushCaptionGradient(this.TitleBounds, 90f, true))// McBrushes.GetActiveCaptionBrush(this.TitleBounds, 90f))
                {
                    graphics1.FillRectangle(brush1, this.TitleBounds);
                    color1 = SystemColors.ActiveCaptionText;
                    goto Label_00E2;
                }
            }
            using (Brush brush2 = base.LayoutManager.GetBrushGradient(this.TitleBounds, 90f, true))// McBrushes.GetControlBrush(this.TitleBounds, 90f))
            {
                graphics1.FillRectangle(brush2, this.TitleBounds);
            }
        Label_00E2:
            graphics1.DrawRectangle(SystemPens.ControlDark, this.TitleBounds.X, this.TitleBounds.Y, this.TitleBounds.Width - 1, this.TitleBounds.Height - 1);
            int num1 = 0;
            if (((this.SelectedTab != null) && (this.SelectedTab.Image != null)) && (this.TitleBounds.Width > 0x37))
            {
                num1 = this.SelectedTab.Image.Width;
                Rectangle rectangle1 = new Rectangle(this.TitleBounds.X + 3, this.TitleBounds.Y + ((this.TitleBounds.Height - this.SelectedTab.Image.Height) / 2), num1, this.SelectedTab.Image.Height);
                if (num1 != 0)
                {
                    graphics1.DrawImage(this.SelectedTab.Image, rectangle1);
                }
            }
            if (this.SelectedTab != null)
            {
                using (StringFormat format1 = new StringFormat())
                {
                    format1.LineAlignment = StringAlignment.Center;
                    if (this.RightToLeft == RightToLeft.Yes)
                    {
                        format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                    }
                    format1.FormatFlags = StringFormatFlags.NoWrap;
                    format1.Trimming = StringTrimming.EllipsisCharacter;
                    format1.HotkeyPrefix = HotkeyPrefix.Hide;
                    if (this.RightToLeft == RightToLeft.Yes)
                    {
                        format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                    }
                    using (Brush brush3 = new SolidBrush(color1))
                    {
                        Rectangle rectangle2 = this.TextBounds;
                        if (!rectangle2.IsEmpty)
                        {
                            graphics1.DrawString(this.SelectedTab.Text, this.Font, brush3, (RectangleF)rectangle2, format1);
                        }
                    }
                }
            }
            if (((this.FloatingForm == null) && (base.Width > (0x30 + this.AutoHideBarSize))) && this.ShowHideButton)
            {
                this.PaintAutoHideButton(graphics1);
            }
            if (this.ShowCloseButton)
            {
                this.PaintCloseButton(graphics1);
            }
        }

 
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics1 = e.Graphics;

            if (!this.Collapsed && !this.ResizeBounds.IsEmpty)
            {
                using (Brush brush = LayoutManager.GetBrushContent())
                    graphics1.FillRectangle(brush, this.ResizeBounds);
                using (Pen pen = LayoutManager.GetPenInActive())
                    graphics1.DrawRectangle(pen, this.ResizeBounds);
            }
			if ((this.TitleBounds.IsEmpty || (this.TitleBounds.Width == 0)) || (this.TitleBounds.Height == 0))
			{
				goto Label_0325;
			}
			Color color1 = SystemColors.ControlText;
			if (base.ContainsFocus || this.IsSelected)
			{
				using (Brush brush1 =base.LayoutManager.GetBrushCaptionGradient(this.TitleBounds, 90f,true))// McBrushes.GetActiveCaptionBrush(this.TitleBounds, 90f))
				{
					graphics1.FillRectangle(brush1, this.TitleBounds);
					color1 = SystemColors.ActiveCaptionText;
					goto Label_00E2;
				}
			}
			using (Brush brush2 =base.LayoutManager.GetBrushGradient(this.TitleBounds, 90f,true))// McBrushes.GetControlBrush(this.TitleBounds, 90f))
			{
				graphics1.FillRectangle(brush2, this.TitleBounds);
			}
			Label_00E2:
                graphics1.DrawRectangle(SystemPens.ControlDark, this.PanelBounds.X, this.PanelBounds.Y, this.PanelBounds.Width - 1, this.PanelBounds.Height - 1);
                graphics1.DrawRectangle(SystemPens.ControlDark, this.TitleBounds.X, this.TitleBounds.Y, this.TitleBounds.Width - 1, this.TitleBounds.Height - 1);
			int num1 = 0;
			if (((this.SelectedTab != null) && (this.SelectedTab.Image != null)) && (this.TitleBounds.Width > 0x37))
			{
				num1 = this.SelectedTab.Image.Width;
				Rectangle rectangle1 = new Rectangle(this.TitleBounds.X + 3, this.TitleBounds.Y + ((this.TitleBounds.Height - this.SelectedTab.Image.Height) / 2), num1, this.SelectedTab.Image.Height);
				if (num1 != 0)
				{
					graphics1.DrawImage(this.SelectedTab.Image, rectangle1);
				}
			}
			if (this.SelectedTab != null)
			{
				using (StringFormat format1 = new StringFormat())
				{
					format1.LineAlignment = StringAlignment.Center;
					if (this.RightToLeft == RightToLeft.Yes)
					{
						format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					}
					format1.FormatFlags = StringFormatFlags.NoWrap;
					format1.Trimming = StringTrimming.EllipsisCharacter;
					format1.HotkeyPrefix = HotkeyPrefix.Hide;
					if (this.RightToLeft == RightToLeft.Yes)
					{
						format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					}
					using (Brush brush3 = new SolidBrush(color1))
					{
						Rectangle rectangle2 = this.TextBounds;
						if (!rectangle2.IsEmpty)
						{
							graphics1.DrawString(this.SelectedTab.Text, this.Font, brush3, (RectangleF) rectangle2, format1);
						}
					}
				}
			}
			if (((this.FloatingForm == null) && (base.Width > (0x30 + this.AutoHideBarSize))) && this.ShowHideButton)
			{
				this.PaintAutoHideButton(graphics1);
			}
			if (this.ShowCloseButton)
			{
				this.PaintCloseButton(graphics1);
			}
			Label_0325:
				if ((!this.AutoHide && ((base.Controls.Count > 1) || base.DesignMode)) && ((this.TabsBounds.Width != 0) && (this.TabsBounds.Height != 0)))
				{
                    using (Brush brushBack=LayoutManager.GetBrushFlat())
                        graphics1.FillRectangle(brushBack/*McBrushes.ContentDark*/, this.TabsBounds);
					for (int num2 = base.Controls.Count - 1; num2 >= 0; num2--)
					{
						McDockingTab control1 = base.Controls[num2] as McDockingTab;
                        //if (control1 != this.SelectedTab)
                        //{
                            //if (this.dockingTabStyle == DockingTabStyle.Button)
                                DrawDockingPanel(graphics1, control1);
                            //else
                             //   DrawDockingControl(graphics1, control1);
						//}
					}
                    //if (this.SelectedTab != null)
                    //{
                        //if (this.dockingTabStyle == DockingTabStyle.Button)
                        //    DrawDockingButton(graphics1, this.SelectedTab);
                        //else
                        //    DrawDockingControl(graphics1, this.SelectedTab);
					//}
				}
            Rectangle autoHideBarBounds=this.AutoHideBarBounds;
            if ((this.AutoHide && (autoHideBarBounds.Width != 0)) && (autoHideBarBounds.Height != 0))
			{
                using (Brush brushBack = LayoutManager.GetBrushFlat())
                    graphics1.FillRectangle(brushBack/*McBrushes.ContentDark*/, autoHideBarBounds);
				foreach (McDockingTab control2 in base.Controls)
				{
					Rectangle rectangle3 = this.GetAutoHideControlBounds(control2);
					if ((rectangle3.Width <= 0) || (rectangle3.Height <= 0))
					{
						continue;
					}
					if ((this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right))
					{
						if (rectangle3.Contains(base.PointToClient(Cursor.Position)) && !base.DesignMode)
						{
							using (Brush brush4 = McBrushes.GetControlLightBrush(rectangle3, 0f))
							{
								graphics1.FillRectangle(brush4, rectangle3);
								goto Label_0546;
							}
						}
						using (Brush brush5 = McBrushes.GetControlBrush(rectangle3, 0f))
						{
							graphics1.FillRectangle(brush5, rectangle3);
							goto Label_0546;
						}
					}
					if (rectangle3.Contains(base.PointToClient(Cursor.Position)) && !base.DesignMode)
					{
						using (Brush brush6 = McBrushes.GetControlLightBrush(rectangle3, 90f))
						{
							graphics1.FillRectangle(brush6, rectangle3);
							goto Label_0546;
						}
					}
					using (Brush brush7 = McBrushes.GetControlBrush(rectangle3, 90f))
					{
						graphics1.FillRectangle(brush7, rectangle3);
					}
				Label_0546:
					graphics1.DrawRectangle(SystemPens.ControlDark, rectangle3);
					int num3 = 0;
					if (control2.Image != null)
					{
						Rectangle rectangle4 = Rectangle.Empty;
						if ((this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right))
						{
							num3 = control2.Image.Height + 2;
							rectangle4 = new Rectangle(rectangle3.X + ((rectangle3.Width - control2.Image.Width) / 2), rectangle3.Y + 2, control2.Image.Width, control2.Image.Height);
						}
						else if ((this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom))
						{
							num3 = control2.Image.Width + 2;
							rectangle4 = new Rectangle(rectangle3.X + 2, rectangle3.Y + ((rectangle3.Height - control2.Image.Height) / 2), control2.Image.Width, control2.Image.Height);
						}
						graphics1.DrawImage(control2.Image, rectangle4);
					}
					if (control2 == this.SelectedTab)
					{
						using (StringFormat format2 = new StringFormat())
						{
							format2.Alignment = StringAlignment.Near;
							format2.LineAlignment = StringAlignment.Center;
							format2.FormatFlags = StringFormatFlags.NoWrap;
							if (this.RightToLeft == RightToLeft.Yes)
							{
								format2.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
							}
							if ((this.Dock == DockStyle.Right) || (this.Dock == DockStyle.Left))
							{
								rectangle3.Y += num3;
								rectangle3.Height -= num3;
								McPaint.DrawString(graphics1, control2.Text, this.Font, SystemBrushes.ControlDarkDark, rectangle3, format2, 90f);
								continue;
							}
							rectangle3.X += num3;
							rectangle3.Width -= num3;
							McPaint.DrawString(graphics1, control2.Text, this.Font, SystemBrushes.ControlDarkDark, rectangle3, format2, 0f);
							continue;
						}
					}
				}
			}
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.SetLayout();
			base.Invalidate();
		}

		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			McColors.InitColors();
		}
       
 
		private void PaintAutoHideButton(Graphics g)
		{
			if ((this.imageAutoHideOn != null) && (this.imageAutoHideOff != null))
			{
				if (this.AutoHide)
				{
					this.PaintButton(g, this.AutoHideButtonBounds, this.imageAutoHideOn, this.IsAutoHideButtonHover, this.IsAutoHideButtonPressed);
				}
				else
				{
					this.PaintButton(g, this.AutoHideButtonBounds, this.imageAutoHideOff, this.IsAutoHideButtonHover, this.IsAutoHideButtonPressed);
				}
			}
		}

		private void PaintButton(Graphics g, Rectangle rect, Image image, bool isHover, bool isPressed)
		{
			if (isHover || isPressed)
			{
				Color color1 = McColors.Focus;
				if (isPressed)
				{
					color1 = Color.FromArgb(100, color1);
				}
				g.FillRectangle(new SolidBrush(color1), rect);
				g.DrawRectangle(McPens.SelectedText, rect);
			}
			if (image != null)
			{
				if (base.ContainsFocus || this.IsSelected)
				{
					image = McPaint.ReplaceImageColor((Bitmap) image, SystemColors.ActiveCaptionText, Color.Black);
				}
				g.DrawImage(image, new Rectangle((rect.X + ((rect.Width - image.Width) / 2)) + 1, rect.Y + ((rect.Height - image.Height) / 2), image.Width, image.Height));
			}
		}

 
		private void PaintCloseButton(Graphics g)
		{
			if (this.imageClose != null)
			{
				this.PaintButton(g, this.CloseButtonBounds, this.imageClose, this.IsCloseButtonHover, this.IsCloseButtonPressed);
			}
		}

 
		private void SelectControl(int x, int y)
		{
			McDockingTab control1 = this.GetControlAt(x, y);
			if (control1 != null)
			{
				if (this.Collapsed)
				{
					this.Collapsed = false;
				}
				this.SelectedTab = control1;
				this.SetLayoutAllPanels();
			}
		}

		internal void SetLayout()
		{
			try
			{
				if ((this.Manager == null) || this.Manager.LockLayout)
				{
					return;
				}
				//--this.Refresh();
				switch (this.Dock)
				{
					case DockStyle.Top:
                        this.LastDock = DockStyle.Top;
						this.DockPadding.Left = 1;
						this.DockPadding.Right = 1;
						this.DockPadding.Top = this.AutoHideBarSize + 20;
						this.DockPadding.Bottom = 4 + this.TabsBarSize;
						this.resizeBounds = new Rectangle(0, base.Height - 4, base.Width, 4);
						this.titleBounds = new Rectangle(0, this.AutoHideBarSize, base.Width, 20);
						this.panelBounds = new Rectangle(0, this.AutoHideBarSize, base.Width, (base.Height - 4) - this.AutoHideBarSize);
						this.tabsBounds = new Rectangle(0, (base.Height - this.TabsBarSize) - 4, base.Width - this.AutoHideBarSize, this.TabsBarSize);
						goto Label_0406;

					case DockStyle.Bottom:
                        this.LastDock = DockStyle.Bottom;
                        this.DockPadding.Left = 1;
						this.DockPadding.Right = 1;
						this.DockPadding.Top = 0x18;
						this.DockPadding.Bottom = this.AutoHideBarSize + this.TabsBarSize;
						this.resizeBounds = new Rectangle(0, 0, base.Width, 4);
						this.titleBounds = new Rectangle(0, 4, base.Width, 20);
						this.panelBounds = new Rectangle(0, 4, base.Width, (base.Height - 4) - this.AutoHideBarSize);
						this.tabsBounds = new Rectangle(0, base.Height - this.TabsBarSize, base.Width - this.AutoHideBarSize, this.TabsBarSize);
						goto Label_0406;

					case DockStyle.Left:
                        this.LastDock = DockStyle.Left;
                        this.DockPadding.Left = this.AutoHideBarSize + 1;
						this.DockPadding.Right = 5;
						this.DockPadding.Top = 20;
						this.DockPadding.Bottom = this.TabsBarSize;
						this.resizeBounds = new Rectangle(base.Width - 4, 0, 4, base.Height);
						this.titleBounds = new Rectangle(this.AutoHideBarSize, 0, (base.Width - 4) - this.AutoHideBarSize, 20);
						this.panelBounds = new Rectangle(this.AutoHideBarSize, 0, (base.Width - 4) - this.AutoHideBarSize, base.Height);
						this.tabsBounds = new Rectangle(0, base.Height - this.TabsBarSize, (base.Width - 4) - this.AutoHideBarSize, this.TabsBarSize);
						goto Label_0406;

					case DockStyle.Right:
                        this.LastDock = DockStyle.Right;
                        this.DockPadding.Left = 5;
						this.DockPadding.Right = this.AutoHideBarSize + 1;
						this.DockPadding.Top = 20;
						this.DockPadding.Bottom = this.TabsBarSize;
						this.resizeBounds = new Rectangle(0, 0, 4, base.Height);
						this.titleBounds = new Rectangle(4, 0, (base.Width - 4) - this.AutoHideBarSize, 20);
						this.panelBounds = new Rectangle(4, 0, (base.Width - 4) - this.AutoHideBarSize, base.Height);
						this.tabsBounds = new Rectangle(4, base.Height - this.TabsBarSize, (base.Width - 4) - this.AutoHideBarSize, this.TabsBarSize);
						goto Label_0406;

					case DockStyle.Fill:
						break;

					default:
						goto Label_0406;
				}
				this.DockPadding.Left = 1;
				this.DockPadding.Right = 1;
				this.DockPadding.Top = 20;
				this.DockPadding.Bottom = this.TabsBarSize;
				this.resizeBounds = new Rectangle(0, 0, 0, base.Height);
				this.titleBounds = new Rectangle(0, 0, base.Width, 20);
				this.panelBounds = new Rectangle(0, 0, base.Width, base.Height);
				this.tabsBounds = new Rectangle(0, base.Height - this.TabsBarSize, base.Width, this.TabsBarSize);
			Label_0406:
				foreach (McDockingTab control1 in base.Controls)
				{
					if (control1.Visible && (this.SelectedTab != control1))
					{
						control1.Visible = false;
						this.selectedControl = this.SelectedTab;
						this.selectedControl.Visible = true;
						break;
					}
				}
				base.UpdateStyles();
			}
			catch
			{
			}
		}

 
		internal void SetLayoutAllPanels()
		{
            //--
            //if ((this.manager != null) && (this.manager.ParentForm != null))
            //{
            //    this.selectedControl.SetLayout();
            //    //foreach (Control control1 in this.manager.ParentForm.Controls)
            //    //{
            //    //    if (control1 is McDockingPanel)
            //    //    {
            //    //        if(control1.Dock==this.Dock)
            //    //        ((McDockingPanel)control1).SetLayout();
            //    //    }
            //    //}
            //    //TODO:add_fix
            //    ArrayList list = this.manager.UndockedPanels;
            //    if (list == null || list.Count==0)
            //        return;
            //    foreach (Control control2 in this.manager.UndockedPanels)
            //    {
            //        if (control2 is McDockingPanel)
            //        {
            //            if (control2.Dock == this.Dock)
            //                ((McDockingPanel)control2).SetLayout();
            //        }
            //    }
            //}
		}

 
		public McDockingPanel UndockControl(McDockingTab control, Rectangle rect, bool createForm, Control parent)
		{
			McDockingPanel panel1 = new McDockingPanel(this.Manager);
			panel1.LastDockForm = this.LastDockForm;
			panel1.Parent = parent;
			panel1.Controls.Add(control);
			panel1.UndockPanel(rect, createForm);
			if ((base.Controls.Count == 0) && (base.Parent != null))
			{
				base.Parent.Controls.Remove(this);
			}
			return panel1;
		}

 
		public void UndockPanel(Rectangle rect, bool createForm)
		{
			rect.Size = new Size(200, 300);
			if (createForm)
			{
				this.Manager.AddUndockingPanels(this);
				McFloatingForm form1 = new McFloatingForm(this);
				if (this.LastDockForm == null)
				{
					if ((base.Parent != null) && this.Manager.ShowingUndocked)
					{
						((Form) base.Parent).AddOwnedForm(form1);
					}
					this.LastDockForm = base.Parent as Form;
					this.LastDock = this.Dock;
				}
				else if (this.Manager.ShowingUndocked)
				{
					this.LastDockForm.AddOwnedForm(form1);
				}
				if (rect.Left == -1)
				{
					form1.StartPosition = FormStartPosition.CenterScreen;
					form1.ClientSize = new Size(rect.Width, rect.Height);
				}
				else
				{
					form1.Location = rect.Location;
					form1.ClientSize = new Size(rect.Width, rect.Height);
				}
				form1.Controls.Add(this);
				if (this.Manager.ShowingUndocked)
				{
					form1.Show();
				}
			}
			this.SetLayout();
			base.Focus();
		}

		public void CollapsedAutoHide()
		{
			this.AutoHide=true;
			this.autoHideTimer.Enabled = false;
			this.Collapsed = true;
			this.SetLayoutAllPanels();
		}

		protected override void WndProc(ref Message msg)
		{
            //if (msg.Msg == 0x2a3)
            //{
            //    this.InvalidateTitle();
            //    this.InvalidateTabs();
            //    this.InvalidateAutoHideBar();
            //}
			base.WndProc(ref msg);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public new bool AllowDrop
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
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AutoHide
		{
			get
			{
				return this.autoHide;
			}
			set
			{
				this.autoHide = value;
				switch (this.Dock)
				{
					case DockStyle.Left:
					case DockStyle.Right:
						base.Width += this.AutoHide ? 0x15 : -21;
						break;
				}
				this.SetLayout();
				base.Invalidate();
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public Rectangle AutoHideBarBounds
		{
			get
			{
				int num1 = this.AutoHideBarSize;
				if (!this.AutoHide)
				{
					num1 = 0;
				}
				switch (this.Dock)
				{
					case DockStyle.Top:
						return new Rectangle(0, 0, base.Width, num1);

					case DockStyle.Bottom:
						return new Rectangle(0, base.Height - num1, base.Width, num1);
					case DockStyle.Left:
						return new Rectangle(0, 0, num1, base.Height);
				}
				return new Rectangle(base.Width - num1, 0, num1, base.Height);
			}
		}
 
		internal int AutoHideBarSize
		{
			get
			{
				if (!this.AutoHide)
				{
					return 0;
				}
				return 0x17;
			}
		}
		private Rectangle AutoHideButtonBounds
		{
			get
			{
				Rectangle rectangle1 = this.CloseButtonBounds;
				rectangle1 = new Rectangle(rectangle1.X, rectangle1.Y, 20, rectangle1.Height);
				if (this.ShowCloseButton)
				{
					rectangle1.X -= 20;
				}
				return rectangle1;
			}
		}
 
        //[DefaultValue(DockingTabStyle.Button)]
        //public DockingTabStyle DockingTabStyle
        //{
        //    get
        //    {
        //        return this.dockingTabStyle;
        //    }
        //    set
        //    {
        //        this.dockingTabStyle = value;
        //    }
        //}
		[DefaultValue(0x3e8)]
		public int AutoHideTime
		{
			get
			{
				return this.autoHideTimer.Interval;
			}
			set
			{
				this.autoHideTimer.Interval = value;
			}
		}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override Color BackColor
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
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
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
 
		private Rectangle CloseButtonBounds
		{
			get
			{
				Rectangle rectangle1 = this.TitleBounds;
				rectangle1 = new Rectangle((rectangle1.Right - 20) - 3, rectangle1.Y + 2, 20, 15);
				if (!this.ShowCloseButton)
				{
					rectangle1.Width = 0;
				}
				return rectangle1;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Collapsed
		{
			get
			{
				return this.collapsed;
			}
			set
			{
				this.collapsed = value;
				if (value)
				{
					this.CollapsedSize = base.ClientSize;
					switch (this.Dock)
					{
						case DockStyle.Top:
						case DockStyle.Bottom:
							base.Height = this.AutoHideBarSize;
							return;

						case DockStyle.Left:
						case DockStyle.Right:
							base.Width = this.AutoHideBarSize;
							return;
					}
				}
				else
				{
					base.ClientSize = this.CollapsedSize;
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		internal Size CollapsedSize
		{
			get
			{
				return this.collapsedSize;
			}
			set
			{
				this.collapsedSize = value;
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
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

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal McFloatingForm FloatingForm
		{
			get
			{
				return this.floatingForm;
			}
			set
			{
				this.floatingForm = value;
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}
 
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsAutoHideButtonHover
		{
			get
			{
				return this.isAutoHideButtonHover;
			}
			set
			{
				this.isAutoHideButtonHover = value;
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		internal bool IsAutoHideButtonPressed
		{
			get
			{
				return this.isAutoHideButtonPressed;
			}
			set
			{
				this.isAutoHideButtonPressed = value;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsCloseButtonHover
		{
			get
			{
				return this.isCloseButtonHover;
			}
			set
			{
				this.isCloseButtonHover = value;
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		internal bool IsCloseButtonPressed
		{
			get
			{
				return this.isCloseButtonPressed;
			}
			set
			{
				this.isCloseButtonPressed = value;
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		internal bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
			set
			{
				this.isSelected = value;
			}
		}
 
		[Browsable(false)]
		public McDocking Manager
		{
			get
			{
				return this.manager;
			}
			set
			{
				this.manager = value;
			}
		}
 
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal Rectangle PanelBounds
		{
			get
			{
				return this.panelBounds;
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		internal Rectangle ResizeBounds
		{
			get
			{
				return this.resizeBounds;
			}
		}
 
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public McDockingTab SelectedTab
		{
			get
			{
				return this.selectedControl;
			}
			set
			{
				if (base.Parent != null)
				{
					foreach (Control control1 in base.Controls)
					{
						if (control1 != value)
						{
							control1.Visible = false;
						}
					}
				}
				this.selectedControl = value;
				if (value != null)
				{
					value.Visible = true;
					//base.Invalidate();
				}
				this.SetLayout();
			}
		}
		[DefaultValue(true), Category("Behavior"), Browsable(true)]
		public bool ShowCloseButton
		{
			get
			{
				return this.showCloseButton;
			}
			set
			{
				this.showCloseButton = value;
				base.Invalidate();
			}
		}
 
		[Browsable(true), DefaultValue(true), Category("Behavior")]
		public bool ShowHideButton
		{
			get
			{
				return this.showHideButton;
			}
			set
			{
				this.showHideButton = value;
				base.Invalidate();
			}
		}
 
		internal int TabsBarSize
		{
			get
			{
				if ((base.Controls.Count > 1) && !this.AutoHide)
				{
					return 0x18;
				}
				return 1;
			}
		}
		internal Rectangle TabsBounds
		{
			get
			{
				return this.tabsBounds;
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}
		internal Rectangle TextBounds
		{
			get
			{
				int num1 = 0;
				if ((this.SelectedTab != null) && (this.SelectedTab.Image != null))
				{
					num1 = this.SelectedTab.Image.Width;
				}
				Rectangle rectangle1 = this.TitleBounds;
				rectangle1.X += num1 + 4;
				rectangle1.Width -= (5 + num1) + 20;
				if (this.FloatingForm == null)
				{
					rectangle1.Width -= 20;
				}
				if (rectangle1.Width == 0)
				{
					return Rectangle.Empty;
				}
				return rectangle1;
			}
		}
		internal Rectangle TitleBounds
		{
			get
			{
				return this.titleBounds;
			}
		}


	}
}