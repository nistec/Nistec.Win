using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Nistec.Win32;

namespace Nistec.WinForms
{
	internal class McDockingWindow : Form
	{

		// Fields
		private Container components;
		internal static Control ControlToDock;
		internal static McDockingWindow DockingWindow;
		internal static DockStyle DockType;
		internal static McDockingPanel panel;


		static McDockingWindow()
		{
			McDockingWindow.DockingWindow = null;
			McDockingWindow.panel = null;
			McDockingWindow.ControlToDock = null;
		}

 
		internal McDockingWindow()
		{
			this.components = null;
			this.InitializeComponent();
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.UserPaint, true);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

 
		internal static Rectangle GetDockRectangle(Rectangle rect)
		{
			Rectangle rectangle1 = McDockingWindow.GetPanelsDockRectangle(rect);
			if (!rectangle1.IsEmpty)
			{
				return rectangle1;
			}
			rectangle1 = McDockingWindow.GetWindowDockRectangle(rect);
			if (!rectangle1.IsEmpty)
			{
				return rectangle1;
			}
			return rect;
		}

		private static Rectangle GetDockRectangle(Form form)
		{
			Rectangle rectangle1 = form.ClientRectangle;
			for (int num1 = form.Controls.Count - 1; num1 >= 0; num1--)
			{
				Control control1 = form.Controls[num1];
				if (control1.Visible)
				{
					switch (control1.Dock)
					{
						case DockStyle.Top:
							rectangle1.Y += control1.Height;
							rectangle1.Height -= control1.Height;
							break;

						case DockStyle.Bottom:
							rectangle1.Height -= control1.Height;
							break;

						case DockStyle.Left:
							rectangle1.X += control1.Width;
							rectangle1.Width -= control1.Width;
							break;

						case DockStyle.Right:
							rectangle1.Width -= control1.Width;
							break;

						case DockStyle.Fill:
							return form.RectangleToScreen(rectangle1);
					}
				}
			}
			return form.RectangleToScreen(rectangle1);
		}

 
		private static Form GetForm()
		{
			if (McDockingWindow.panel.LastDockForm != null)
			{
				return McDockingWindow.panel.LastDockForm;
			}
			return McDockingWindow.panel.FindForm();
		}

		private static Rectangle GetPanelDockRectangle(McDockingPanel dockPanel, Rectangle rect)
		{
			Point point1 = Cursor.Position;
			McDockingWindow.ControlToDock = dockPanel;
			if (dockPanel.Collapsed)
			{
				Rectangle rectangle1 = dockPanel.RectangleToScreen(dockPanel.ClientRectangle);
				if (rectangle1.Contains(point1))
				{
					return rectangle1;
				}
			}
			Rectangle rectangle2 = dockPanel.RectangleToScreen(dockPanel.PanelBounds);
			if (rectangle2.Contains(point1))
			{
				if (dockPanel == McDockingWindow.panel)
				{
					return Rectangle.Empty;
				}
				int num3 = dockPanel.Width / 2;
				int num1 = dockPanel.Height - 20;
				int num2 = 0;
				if (dockPanel.Controls.Count > 1)
				{
					num1 -= 0x18;
					num2 = 0x18;
				}
				int num4 = rectangle2.Height;
				num1 /= 2;
				Rectangle rectangle3 = new Rectangle(rectangle2.Left, rectangle2.Top, rectangle2.Width, 20);
				if (rectangle3.Contains(point1))
				{
					McDockingWindow.DockType = DockStyle.Fill;
					return rectangle2;
				}
				if (num2 != 0)
				{
					rectangle3 = new Rectangle(rectangle2.Left, rectangle2.Bottom - 0x18, rectangle2.Width, 0x18);
					if (rectangle3.Contains(point1))
					{
						McDockingWindow.DockType = DockStyle.Fill;
						return rectangle2;
					}
				}
				switch (dockPanel.Dock)
				{
					case DockStyle.Bottom:
						rectangle3 = new Rectangle(rectangle2.Left, rectangle2.Bottom - 20, rectangle2.Width, 20);
						if (rectangle3.Contains(point1))
						{
							McDockingWindow.DockType = DockStyle.Fill;
							return rectangle2;
						}
						break;

					case DockStyle.Left:
						rectangle3 = new Rectangle(rectangle2.Left, rectangle2.Top, 20, rectangle2.Height);
						if (rectangle3.Contains(point1))
						{
							McDockingWindow.DockType = DockStyle.Fill;
							return rectangle2;
						}
						break;

					case DockStyle.Right:
						rectangle3 = new Rectangle(rectangle2.Right - 20, rectangle2.Top, 20, rectangle2.Height);
						if (rectangle3.Contains(point1))
						{
							McDockingWindow.DockType = DockStyle.Fill;
							return rectangle2;
						}
						break;
				}
			}
			return Rectangle.Empty;
		}

		private static Rectangle GetPanelsDockRectangle(Rectangle rect)
		{
			Form form1 = McDockingWindow.GetForm();
			foreach (Control control1 in form1.Controls)
			{
				McDockingPanel panel1 = control1 as McDockingPanel;
				if (panel1 != null)
				{
					Rectangle rectangle1 = McDockingWindow.GetPanelDockRectangle(panel1, rect);
					if (!rectangle1.IsEmpty)
					{
						return rectangle1;
					}
				}
			}
            if(McDockingWindow.panel.Manager.UndockedPanels==null)
                return Rectangle.Empty;
			foreach (McDockingPanel panel2 in McDockingWindow.panel.Manager.UndockedPanels)
			{
				Rectangle rectangle2 = McDockingWindow.GetPanelDockRectangle(panel2, rect);
				if (!rectangle2.IsEmpty)
				{
					return rectangle2;
				}
			}
			return Rectangle.Empty;
		}

		private static Rectangle GetWindowDockRectangle(Rectangle rect)
		{
			Rectangle rectangle2;
			Form form1 = McDockingWindow.GetForm();
			Rectangle rectangle1 = McDockingWindow.GetDockRectangle(form1);
			Point point1 = Cursor.Position;
			bool flag1 = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			foreach (Control control1 in form1.Controls)
			{
				if (control1 is McDockingPanel)
				{
					switch (control1.Dock)
					{
						case DockStyle.Top:
						{
							flag3 = true;
							continue;
						}
						case DockStyle.Bottom:
							goto Label_0076;

						case DockStyle.Left:
						{
							flag1 = true;
							continue;
						}
						case DockStyle.Right:
						{
							flag2 = true;
							continue;
						}
					}
				}
				continue;
			Label_0076:
				flag4 = true;
			}
			McDockingWindow.ControlToDock = form1;
            if (!flag1)
            {
                rectangle2 = new Rectangle(rectangle1.Left, rectangle1.Top, 20, rectangle1.Height);
                if (rectangle2.Contains(point1))
                {
                    McDockingWindow.DockType = DockStyle.Left;
                    return new Rectangle(rectangle1.X, rectangle1.Y, 200, rectangle1.Height);
                }
            }
            if (!flag2)
            {
                rectangle2 = new Rectangle(rectangle1.Right - 20, rectangle1.Top, 20, rectangle1.Height);
                if (rectangle2.Contains(point1))
                {
                    McDockingWindow.DockType = DockStyle.Right;
                    return new Rectangle(rectangle1.Right - 200, rectangle1.Y, 200, rectangle1.Height);
                }
            }
			if (!flag3)
			{
				rectangle2 = new Rectangle(rectangle1.Left, rectangle1.Top, rectangle1.Width, 20);
				if (rectangle2.Contains(point1))
				{
					McDockingWindow.DockType = DockStyle.Top;
					return new Rectangle(rectangle1.X, rectangle1.Y, rectangle1.Width, 150);
				}
			}
			if (!flag4)
			{
				rectangle2 = new Rectangle(rectangle1.Left, rectangle1.Bottom - 20, rectangle1.Width, 20);
				if (rectangle2.Contains(point1))
				{
					McDockingWindow.DockType = DockStyle.Bottom;
					return new Rectangle(rectangle1.X, rectangle1.Bottom - 150, rectangle1.Width, 150);
				}
			}
	
			return Rectangle.Empty;
		}

		private void InitializeComponent()
		{
			this.AutoScaleBaseSize = new Size(5, 13);
			base.ClientSize = new Size(0x128, 240);
			base.FormBorderStyle = FormBorderStyle.None;
			base.Name = "McDockingWindow";
			base.Opacity = 0.5;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "McDockWindow";
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics1 = e.Graphics;
			Rectangle rectangle1 = e.ClipRectangle;
			rectangle1.Width--;
			rectangle1.Height--;
			graphics1.DrawRectangle(SystemPens.ControlDark, rectangle1);
			rectangle1.Inflate(-1, -1);
			graphics1.DrawRectangle(SystemPens.ControlDark, rectangle1);
			rectangle1.Inflate(-1, -1);
			rectangle1.Width++;
			rectangle1.Height++;
			graphics1.FillRectangle(SystemBrushes.ButtonShadow, rectangle1);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

 
		internal static void RefreshDockingWindow(Rectangle rect)
		{
			McDockingWindow.DockingWindow.Bounds = McDockingWindow.GetDockRectangle(rect);
		}

 
		internal static void StartDockingWindow(McDockingPanel pn, Rectangle rect)
		{
			McDockingWindow.panel = pn;
			McDockingWindow.DockingWindow = new McDockingWindow();
			WinAPI.SetWindowPos(McDockingWindow.DockingWindow.Handle, new IntPtr(-1), 0, 0, rect.Width, rect.Height, 0x53);
		}

 
		internal static void StopDockingWindow()
		{
			if (McDockingWindow.DockingWindow != null)
			{
				McDockingWindow.DockingWindow.Close();
				McDockingWindow.DockingWindow.Dispose();
				McDockingWindow.DockingWindow = null;
			}
		}



	}
}
