using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;


namespace Nistec.WinForms
{
	[ToolboxBitmap(typeof(McScrollableControl), "Toolbox.ScrollableControl.bmp"), ToolboxItem(true)]
	public class McScrollableControl : ScrollableControl
	{
	// Fields
		private Panel edgePanel;
		private McHScrollBar hScrollBar;
		private int scrollHeight;
		private int scrollLeft;
		private int scrollTop;
		private int scrollWidth;
		private McVScrollBar vScrollBar;

		public McScrollableControl()
		{
			this.edgePanel = new Panel();
			this.scrollWidth = 100;
			this.scrollHeight = 100;
			this.scrollTop = 0;
			this.scrollLeft = 0;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.ContainerControl, false);
			this.VScrollBar = new McVScrollBar();
			this.VScrollBar.ValueChanged += new EventHandler(this.OnValueChanged);
			this.HScrollBar = new McHScrollBar();
			this.HScrollBar.ValueChanged += new EventHandler(this.OnValueChanged);
			base.Controls.Add(this.VScrollBar);
			base.Controls.Add(this.HScrollBar);
			base.Controls.Add(this.edgePanel);
			this.SetPos();
			base.Width = 100;
			base.Height = 100;
			base.ClientSize = new Size(40, 40);
		}

 
		private void Draw()
		{
			base.Invalidate(base.ClientRectangle);
		}

		private bool IsScrollBar(Control control)
		{
			if ((control != this.VScrollBar) && (control != this.HScrollBar))
			{
				return (control == this.edgePanel);
			}
			return true;
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.SetPos();
			base.SetDisplayRectLocation(10, 10);
		}

		private void OnValueChanged(object sender, EventArgs e)
		{
			this.SetPos();
			this.Draw();
		}

 
		protected void SetPos()
		{
			try
			{
				if (this.ClientWidth >= this.ScrollWidth)
				{
					this.HScrollBar.Visible = false;
				}
				else
				{
					this.HScrollBar.Visible = true;
				}
				if (this.ClientHeight >= this.ScrollHeight)
				{
					this.VScrollBar.Visible = false;
				}
				else
				{
					this.VScrollBar.Visible = true;
				}
				this.VScrollBar.Width = 0x10;
				if (this.HScrollBar.Visible)
				{
					this.VScrollBar.Height = base.Height - 0x10;
				}
				else
				{
					this.VScrollBar.Height = base.Height;
				}
				this.VScrollBar.Top = 0;
				this.VScrollBar.Left = base.Width - this.VScrollBar.Width;
				this.HScrollBar.Width = base.Width - 0x10;
				if (this.VScrollBar.Visible)
				{
					this.HScrollBar.Width = base.Width - 0x10;
				}
				else
				{
					this.HScrollBar.Width = base.Width;
				}
				this.HScrollBar.Height = 0x10;
				this.HScrollBar.Left = 0;
				this.HScrollBar.Top = base.Height - this.HScrollBar.Height;
				this.edgePanel.Left = this.HScrollBar.Right;
				this.edgePanel.Top = this.VScrollBar.Bottom;
				this.edgePanel.Width = this.VScrollBar.Width;
				this.edgePanel.Height = this.HScrollBar.Height;
				this.edgePanel.Visible = this.HScrollBar.Visible & this.VScrollBar.Visible;
				this.edgePanel.BackColor = SystemColors.Control;
				this.ScrollTop = -this.VScrollBar.Value;
				this.ScrollLeft = -this.HScrollBar.Value;
				this.VScrollBar.Minimum = 0;
				this.VScrollBar.Maximum = this.ScrollHeight;
				this.VScrollBar.LargeChange = Math.Max(this.ClientHeight, 0);
				this.VScrollBar.SmallChange = this.VScrollBar.LargeChange / 10;
				this.HScrollBar.Minimum = 0;
				this.HScrollBar.Maximum = this.ScrollWidth;
				this.HScrollBar.LargeChange = Math.Max(this.ClientWidth, 0);
				this.HScrollBar.SmallChange = this.HScrollBar.LargeChange / 10;
			}
			catch
			{
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
		public int ClientHeight
		{
			get
			{
				if (this.HScrollBar.Visible)
				{
					return (base.Height - 0x10);
				}
				return base.Height;
			}
		}
 
		[Browsable(false)]
		public int ClientWidth
		{
			get
			{
				if (this.VScrollBar.Visible)
				{
					return (base.Width - 0x10);
				}
				return base.Width;
			}
		}
		[Browsable(false)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}
		[Browsable(false)]
		public McHScrollBar HScrollBar
		{
			get
			{
				return this.hScrollBar;
			}
			set
			{
				this.hScrollBar = value;
			}
		}
 
		[Category("Behavior")]
		public virtual int ScrollHeight
		{
			get
			{
				return this.scrollHeight;
			}
			set
			{
				this.scrollHeight = value;
				this.SetPos();
				this.Draw();
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ScrollLeft
		{
			get
			{
				return this.scrollLeft;
			}
			set
			{
				int num1 = value - this.scrollLeft;
				if (num1 != 0)
				{
					foreach (Control control1 in base.Controls)
					{
						if (!this.IsScrollBar(control1))
						{
							control1.Left += num1;
						}
					}
					this.scrollLeft = value;
				}
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public int ScrollTop
		{
			get
			{
				return this.scrollTop;
			}
			set
			{
				int num1 = value - this.scrollTop;
				if (num1 != 0)
				{
					foreach (Control control1 in base.Controls)
					{
						if (!this.IsScrollBar(control1))
						{
							control1.Top += num1;
						}
					}
					this.scrollTop = value;
				}
			}
		}
		[Category("Behavior")]
		public virtual int ScrollWidth
		{
			get
			{
				return this.scrollWidth;
			}
			set
			{
				this.scrollWidth = value;
				this.SetPos();
				this.Draw();
			}
		}
 
		[Browsable(false)]
		public McVScrollBar VScrollBar
		{
			get
			{
				return this.vScrollBar;
			}
			set
			{
				this.vScrollBar = value;
			}
		}
 

	}

}
