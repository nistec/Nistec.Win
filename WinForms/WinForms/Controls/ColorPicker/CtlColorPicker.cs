using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Globalization;  


using Nistec.Win32;
using Nistec.Drawing;

namespace Nistec.WinForms
{
    [Designer(typeof(Design.McEditDesigner)), ToolboxItem(true), ToolboxBitmap(typeof(McColorPicker), "Toolbox.ColorPicker.bmp")]
	[DefaultEvent("SelectedIndexChanged")]
	public class McColorPicker : Nistec.WinForms.Controls.McButtonEdit
	{

		#region Members
		// Events
		[Category("Behavior")]
		public event EventHandler DropDown;
		[Category("Behavior")]
		public event EventHandler DropUp;
		[Category("Behavior")]
		public event EventHandler SelectedIndexChanged;

		// Fields
		internal ColorPopupForm ColorBoxPopupForm;
		private Color selectedColor;
		private bool showColorBox;
		private bool showColorName;

		#endregion

		#region Ctor

		public McColorPicker()
		{
			this.ColorBoxPopupForm = null;
			this.showColorBox = true;
			this.showColorName = true;
			this.selectedColor = Color.Transparent;
			//base.TextBox.Visible = false;
		}

		protected override void Dispose(bool disposing)
		{
//			if (disposing && (base.ButtonBitmap != null))
//			{
//				base.ButtonBitmap.Dispose();
//			}
			base.Dispose(disposing);
		}

		#endregion

		#region comb PopUp

		public override void DoDropDown()
		{
			if(DroppedDown)
			{
				ColorBoxPopupForm.Close();
				DroppedDown=false;
				return;
			}	
			ShowPopUp();
		}

		public override void CloseDropDown()
		{
			if(DroppedDown)
			{
				ColorBoxPopupForm.Close();
			}
		}

		public void ShowPopUp()
		{
	
			if (this.ColorBoxPopupForm == null)
			{
				this.InvokeDropDown(EventArgs.Empty);
				this.ColorBoxPopupForm = new ColorPopupForm(this, this.SelectedColor);
				Form form1 = base.FindForm();
				if (form1 != null)
				{
					form1.AddOwnedForm(this.ColorBoxPopupForm);
				}
				Rectangle rectangle1 = base.RectangleToScreen(base.ClientRectangle);
				this.ColorBoxPopupForm.Handle.ToString();
				this.ColorBoxPopupForm.Location = new Point(rectangle1.Left, (rectangle1.Top + base.Height) + 1);
				int num1 = 210;
				int num2 =232;// 270;
				this.ColorBoxPopupForm.Size = new Size(num1, num2);
				if (this.ColorBoxPopupForm.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
				{
					this.ColorBoxPopupForm.Top = (rectangle1.Top - 1) - this.ColorBoxPopupForm.Height;
				}
				if (Screen.PrimaryScreen.Bounds.Right < this.ColorBoxPopupForm.Right)
				{
					this.ColorBoxPopupForm.Left = rectangle1.Right - this.ColorBoxPopupForm.Width;
				}
				this.ColorBoxPopupForm.Font = this.Font;
				//this.ColorBoxPopupForm.LockClose = true;
				//this.ColorBoxPopupForm.ClosePopup += new EventHandler(this.OnClosePopup);
				this.ColorBoxPopupForm.Closed += new EventHandler(this.OnClosePopup);
				this.ColorBoxPopupForm.ShowPopupForm();
				this.ColorBoxPopupForm.TabControl.SelectedIndex = this.ColorBoxPopupForm.TabControl.SelectedIndex;
				//this.ColorBoxPopupForm.Focus();
				DroppedDown=true;
			}
			else
			{
				this.ColorBoxPopupForm.ClosePopupForm();
				this.ColorBoxPopupForm = null;
				DroppedDown=false;
			}
		}

		#endregion

		#region DropDown methods

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
				this.InvokeDropUp(e);
				Color color1 = this.ColorBoxPopupForm.SelectedColor;
				if (this.SelectedColor != color1)
				{
					this.SelectedColor = color1;
					this.InvokeSelectedIndexChanged(EventArgs.Empty);
				}
				this.ColorBoxPopupForm.Closed -= new EventHandler(this.OnClosePopup);

				this.ColorBoxPopupForm.Dispose();
				this.ColorBoxPopupForm = null;
				DroppedDown=false;
			}
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

		#endregion

		#region override

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			//base.TextBox.Visible = false;
			base.Invalidate();
		}

 
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if ((e.Button & MouseButtons.Left) > MouseButtons.None)
			{
				ShowPopUp();
			}
			else
			{
				if (this.ColorBoxPopupForm != null)
				{
					this.ColorBoxPopupForm.ClosePopupForm();
				}
				this.ColorBoxPopupForm = null;
				DroppedDown=false;
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
			Graphics graphics1 = p.Graphics;
			Rectangle rectangle1 = Rectangle.Empty;
			Rectangle rectangle2 = Rectangle.Empty;
			Rectangle rectangle3 = McPaint.GetButtonRect(base.ClientRectangle, true);
			Rectangle rectangle4 = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			if (this.showColorBox)
			{
				if (this.showColorName)
				{
					rectangle1 = new Rectangle(rectangle4.X + 4, rectangle4.Y + 4, 0x16, base.Height - 9);
				}
				else
				{
					rectangle1 = new Rectangle(rectangle4.X + 4, rectangle4.Y + 4, (base.Width - rectangle3.Width) - 10, base.Height - 9);
				}
			}
			if (this.showColorName)
			{
				if (this.showColorBox)
				{
					rectangle2 = new Rectangle(rectangle1.Right + 2, 1, ((base.Width - rectangle1.Width) - rectangle3.Width) - 4, base.Height - 2);
				}
				else
				{
					rectangle2 = new Rectangle(2, 1, (base.Width - rectangle3.Width) - 2, base.Height - 2);
				}
			}
			if (this.Focused)
			{
				Rectangle rectangle5 = this.GetContentRect();
				rectangle5.X--;
				rectangle5.Y++;
				rectangle5.Width--;
				rectangle5.Height -= 2;
				ControlPaint.DrawFocusRectangle(graphics1, rectangle5);
			}
			if (this.showColorBox)
			{
				using (Brush brush1 = new SolidBrush(this.SelectedColor))
				{
					graphics1.FillRectangle(brush1, rectangle1);
				}
				if (base.Enabled)
				{
					graphics1.DrawRectangle(Pens.Black, rectangle1);
				}
				else
				{
					graphics1.DrawRectangle(SystemPens.ControlDark, rectangle1);
				}
			}
			if (this.showColorName)
			{
				using (StringFormat format1 = new StringFormat())
				{
					format1.LineAlignment = StringAlignment.Center;
					format1.FormatFlags = StringFormatFlags.NoWrap;
					if (this.RightToLeft == RightToLeft.Yes)
					{
						format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					}
					string text1 = null;
					if (this.SelectedColor.IsSystemColor)
					{
						text1 = McColors.LocalizableSystemColors[this.SelectedColor] as string;
					}
					else if (this.SelectedColor.IsKnownColor)
					{
						text1 = McColors.LocalizableColors[this.SelectedColor] as string;
					}
					else
					{
						text1 = McColors.ColorName + "[A=" + this.SelectedColor.A.ToString() + ", R=" + this.SelectedColor.R.ToString() + ", G=" + this.SelectedColor.G.ToString() + ", B=" + this.SelectedColor.B.ToString() + "]";
					}
					if (base.Enabled)
					{
						using (Brush brush2 = new SolidBrush(this.ForeColor))
						{
							graphics1.DrawString(text1, this.Font, brush2, (RectangleF) rectangle2, format1);
							return;
						}
					}
					graphics1.DrawString(text1, this.Font, SystemBrushes.ControlDark, (RectangleF) rectangle2, format1);
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

		#endregion

		#region Properties

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

		[DefaultValue(true), Category("Behavior")]
		public bool ShowColorBox
		{
			get
			{
				return this.showColorBox;
			}
			set
			{
				this.showColorBox = value;
			}
		}
 

		[DefaultValue(true), Category("Behavior")]
		public bool ShowColorName
		{
			get
			{
				return this.showColorName;
			}
			set
			{
				this.showColorName = value;
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

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
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
                this.Font = LayoutManager.Layout.TextFont;
            else if (force)
                this.Font = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
            {
                base.ForeColor = LayoutManager.Layout.ForeColor;
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
                base.BackColor = LayoutManager.Layout.BackColor;
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

        #endregion

	}
}
 
