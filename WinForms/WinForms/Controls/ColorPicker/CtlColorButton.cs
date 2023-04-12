using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.ComponentModel;

using Nistec.Drawing;

namespace Nistec.WinForms
{

	[ToolboxItem(false)]//,ToolboxBitmap(typeof(McColorButton), "Toolbox.McColorButton.bmp")]
	public class McColorButton : McToolButton
	{
		// Events
		[Category("Behavior")]
		public event EventHandler DropDown;
		[Category("Behavior")]
		public event EventHandler DropUp;
		[Category("Behavior")]
		public event EventHandler SelectedIndexChanged;


		// Fields
		internal ColorPopupForm ColorBoxPopupForm;
		//private EventHandler DropDown;
		//private EventHandler DropUp;
		private Color selectedColor;
		//private EventHandler SelectedIndexChanged;

		public McColorButton()
		{
			this.ColorBoxPopupForm = null;
			this.selectedColor = Color.Transparent;
		}

		public void InvokeDropDown(EventArgs e)
		{
			this.OnDropDown(e);
		}

 
		public void InvokeDropUp(EventArgs e)
		{
			this.OnDropUp(e);
		}

		public void InvokeSelectedIndexChanged(EventArgs e)
		{
			this.OnSelectedIndexChanged(e);
		}

		private void OnClosePopup(object sender, EventArgs e)
		{
			if (this.ColorBoxPopupForm != null)
			{
				Color color1 = this.ColorBoxPopupForm.SelectedColor;
				if (this.SelectedColor != color1)
				{
					this.SelectedColor = color1;
					this.InvokeSelectedIndexChanged(EventArgs.Empty);
				}
				this.InvokeDropUp(EventArgs.Empty);
				this.ColorBoxPopupForm.Dispose();
				this.ColorBoxPopupForm = null;
			}
			base.Checked = false;
		}

 
		protected virtual void OnDropDown(EventArgs e)
		{
			if (this.DropDown != null)
			{
				this.DropDown(this, e);
			}
		}

		protected virtual void OnDropUp(EventArgs e)
		{
			if (this.DropUp != null)
			{
				this.DropUp(this, e);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if ((e.Button & MouseButtons.Left) > MouseButtons.None)
			{
				if (this.ColorBoxPopupForm == null)
				{
					this.ColorBoxPopupForm = new ColorPopupForm(this, this.SelectedColor);
					Form form1 = base.FindForm();
					if (form1 != null)
					{
						form1.AddOwnedForm(this.ColorBoxPopupForm);
					}
					Rectangle rectangle1 = base.RectangleToScreen(base.ClientRectangle);
					this.ColorBoxPopupForm.Location = new Point(rectangle1.Left, (rectangle1.Top + base.Height) + 1);
					int num1 = 210;
					int num2 = 270;
					this.ColorBoxPopupForm.Size = new Size(num1, num2);
					if (this.ColorBoxPopupForm.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
					{
						this.ColorBoxPopupForm.Top = (rectangle1.Top - 1) - num2;
					}
					if (Screen.PrimaryScreen.Bounds.Right < this.ColorBoxPopupForm.Right)
					{
						this.ColorBoxPopupForm.Left = rectangle1.Right - this.ColorBoxPopupForm.Width;
					}
					this.ColorBoxPopupForm.LockClose = true;
					this.ColorBoxPopupForm.Closed += new EventHandler(this.OnClosePopup);
					this.ColorBoxPopupForm.ShowPopupForm();
					this.ColorBoxPopupForm.Size = new Size(num1, num2);
					this.ColorBoxPopupForm.TabControl.SelectedIndex = this.ColorBoxPopupForm.TabControl.SelectedIndex;
					this.ColorBoxPopupForm.Focus();
					this.InvokeDropDown(EventArgs.Empty);
				}
				else
				{
					this.ColorBoxPopupForm.ClosePopupForm();
					this.ColorBoxPopupForm = null;
				}
			}
			else
			{
				if (this.ColorBoxPopupForm != null)
				{
					this.ColorBoxPopupForm.ClosePopupForm();
				}
				this.ColorBoxPopupForm = null;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.ColorBoxPopupForm != null)
			{
				this.ColorBoxPopupForm.LockClose = false;
			}
		}

 
		protected override void OnPaint(PaintEventArgs p)
		{
			base.OnPaint(p);
			Rectangle rectangle1 = base.ClientRectangle;
			Graphics graphics1 = p.Graphics;
			Rectangle rectangle2 = new Rectangle(rectangle1.X + 2, rectangle1.Bottom - 7, rectangle1.Width - 4, 5);
			if (base.Enabled)
			{
				using (Brush brush1 = new SolidBrush(this.SelectedColor))
				{
					graphics1.FillRectangle(brush1, rectangle2);
					if (this.SelectedColor == Color.Transparent)
					{
						graphics1.DrawRectangle(SystemPens.ControlDark, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
					}
				}
			}
		}

		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (this.SelectedIndexChanged != null)
			{
				this.SelectedIndexChanged(this, e);
			}
		}

		[Category("Behavior")]
		public Color SelectedColor
		{
			get
			{
				return this.selectedColor;
			}
			set
			{
				this.selectedColor = value;
				this.OnSelectedIndexChanged(EventArgs.Empty);
				base.Invalidate();
			}
		}
 
		[Browsable(false)]
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
 	}
 
}