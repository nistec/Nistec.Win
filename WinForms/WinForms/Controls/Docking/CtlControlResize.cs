using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Nistec.WinForms
{
	[ToolboxItem(false)]
	internal class McControlResize : Control
	{
		// Fields
		private static McDockingPanel dockingPanel;
		private static Point lastResizePos;
		private static Rectangle lastResizeRectangle;
		private static int maxSize;
		private static int minSize;
		private static McControlResize ResizeControl;
		private static Rectangle startResizeRectangle;



		static McControlResize()
		{
			McControlResize.minSize = 0x42;
			McControlResize.maxSize = 800;//400;
		}

 
		public McControlResize()
		{
			base.SetStyle(ControlStyles.DoubleBuffer, true);
		}

 
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(SystemBrushes.ControlDark, McControlResize.ResizeControl.ClientRectangle);
		}

		public static void RefreshResize()
		{
			Rectangle rectangle1;
			switch (McControlResize.dockingPanel.Dock)
			{
				case DockStyle.Top:
				case DockStyle.Bottom:
					rectangle1 = McControlResize.lastResizeRectangle;
					rectangle1.Offset(0, Cursor.Position.Y - McControlResize.lastResizePos.Y);
					McControlResize.lastResizeRectangle = rectangle1;
					rectangle1.Y = Math.Max(McControlResize.minSize, rectangle1.Y);
					rectangle1.Y = Math.Min(McControlResize.maxSize, rectangle1.Y);
					McControlResize.ResizeControl.Bounds = rectangle1;
					break;

				case DockStyle.Left:
				case DockStyle.Right:
					rectangle1 = McControlResize.lastResizeRectangle;
					rectangle1.Offset(Cursor.Position.X - McControlResize.lastResizePos.X, 0);
					McControlResize.lastResizeRectangle = rectangle1;
					rectangle1.X = Math.Max(McControlResize.minSize, rectangle1.X);
					rectangle1.X = Math.Min(McControlResize.maxSize, rectangle1.X);
					McControlResize.ResizeControl.Bounds = rectangle1;
					break;
			}
			McControlResize.lastResizePos = Cursor.Position;
		}

		public static void StartResize(McDockingPanel panel, Rectangle rect, int minimumSize, int maximumSize)
		{
			McControlResize.minSize = minimumSize;
			McControlResize.maxSize = maximumSize;
			McControlResize.dockingPanel = panel;
			McControlResize.startResizeRectangle = McControlResize.lastResizeRectangle = rect;
			if (McControlResize.ResizeControl != null)
			{
				McControlResize.ResizeControl.Dispose();
				McControlResize.ResizeControl = null;
			}
			McControlResize.ResizeControl = new McControlResize();
			McControlResize.ResizeControl.Bounds = rect;
			McControlResize.dockingPanel.Parent.Controls.Add(McControlResize.ResizeControl);
			McControlResize.dockingPanel.Parent.Controls.SetChildIndex(McControlResize.ResizeControl, 0);
			McControlResize.dockingPanel.IsResizing = true;
			McControlResize.lastResizePos = Cursor.Position;
		}

		public static void StopResize()
		{
			McControlResize.StopResize(false);
		}

		public static void StopResize(bool cancel)
		{
			if (!cancel)
			{
				Rectangle rectangle1 = McControlResize.ResizeControl.Bounds;
				switch (McControlResize.dockingPanel.Dock)
				{
					case DockStyle.Top:
						McControlResize.dockingPanel.Height += rectangle1.Top - McControlResize.startResizeRectangle.Top;
						break;

					case DockStyle.Bottom:
						McControlResize.dockingPanel.Height -= rectangle1.Top - McControlResize.startResizeRectangle.Top;
						break;

					case DockStyle.Left:
						McControlResize.dockingPanel.Width += rectangle1.Left - McControlResize.startResizeRectangle.Left;
						break;

					case DockStyle.Right:
						McControlResize.dockingPanel.Width -= rectangle1.Left - McControlResize.startResizeRectangle.Left;
						break;
				}
			}
			McControlResize.dockingPanel.Parent.Controls.Remove(McControlResize.ResizeControl);
			McControlResize.ResizeControl.Dispose();
			McControlResize.ResizeControl = null;
			McControlResize.dockingPanel.IsResizing = false;
		}

 

	}
}
