using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Nistec.WinForms
{


	public class McFloatingForm : Form
	{
		// Fields
		private McDockingPanel Panel;


		public McFloatingForm(McDockingPanel panel)
		{
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this.InitializeComponent();
			base.DockPadding.All = 4;
			this.Panel = panel;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private McMousePosition GetNCHitTest(Point p)
		{
			Size size1 = SystemInformation.FrameBorderSize;
			p.Offset(-base.Left, -base.Top);
			if (((p.X <= 20) && (p.Y <= size1.Height)) || ((p.Y <= 20) && (p.X <= size1.Height)))
			{
				return McMousePosition.HTTOPLEFT;
			}
			if (((p.X >= (base.Width - 20)) && (p.Y <= size1.Height)) || ((p.Y <= 20) && (p.X >= (base.Width - size1.Height))))
			{
				return McMousePosition.HTTOPRIGHT;
			}
			if (((p.X <= 20) && (p.Y >= (base.Height - size1.Height))) || ((p.Y >= (base.Height - 20)) && (p.X <= size1.Height)))
			{
				return McMousePosition.HTBOTTOMLEFT;
			}
			if (((p.X >= (base.Width - 20)) && (p.Y >= (base.Height - size1.Height))) || ((p.Y >= (base.Height - 20)) && (p.X >= (base.Width - size1.Height))))
			{
				return McMousePosition.HTBOTTOMRIGHT;
			}
			if (p.X <= size1.Height)
			{
				return McMousePosition.HTLEFT;
			}
            //if (p.Y <= size1.Height)
            //{
            //    return McMousePosition.HTTOP;
            //}
			if (p.X >= (base.Width - size1.Height))
			{
				return McMousePosition.HTRIGHT;
			}
			if (p.Y >= (base.Height - size1.Height))
			{
				return McMousePosition.HTBOTTOM;
			}
			return McMousePosition.HTCLIENT;
		}

 
		private void InitializeComponent()
		{
			this.AutoScaleBaseSize = new Size(5, 13);
			base.ClientSize = new Size(0xe4, 0x126);
			base.FormBorderStyle = FormBorderStyle.None;
			base.MinimumSize = new Size(100, 0x37);
			base.Name = "McFloatingForm";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "Floating Form";
			base.SizeChanged += new EventHandler(this.McFloatingForm_SizeChanged);
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			this.Panel.InvalidateTitle();
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			e.Control.Dock = DockStyle.Fill;
			((McDockingPanel) e.Control).FloatingForm = this;
		}

		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);
			((McDockingPanel) e.Control).FloatingForm = null;
			if (base.Controls.Count == 0)
			{
				base.Close();
				base.Dispose();
			}
		}

		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			this.Panel.InvalidateTitle();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, base.Width, base.Height, Border3DStyle.Raised, Border3DSide.All);
		}

		private void McFloatingForm_SizeChanged(object sender, EventArgs e)
		{
			this.Panel.SetLayout();
			this.Panel.Invalidate();
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x84)
			{
				IntPtr ptr1 = m.LParam;
				m.Result = new IntPtr((int) this.GetNCHitTest(new Point(ptr1.ToInt32())));
			}
			else
			{
				base.WndProc(ref m);
			}
		}


	}
}
