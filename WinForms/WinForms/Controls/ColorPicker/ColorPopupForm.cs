using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

using Nistec.Win32;


using Nistec.Drawing;

using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Nistec.WinForms
{

	public class ColorPopupForm :  Nistec.WinForms.Controls.McPopUpBase
	{

		// Fields
		private McButton btOther;
		private Control colorBox;
		private IWindowsFormsEditorService edSrv;
		private ListBox lbSystem;
		private ListBox lbWeb;
		private Panel[] panels;
		public Color SelectedColor;
		public McTabControl TabControl;
		private McTabPage tbCustom;
		private McTabPage tbSystem;
		private McTabPage tbWeb;


		public ColorPopupForm(Control colorBox, Color selectedColor) : this(colorBox, selectedColor, null)
		{
		}

 
		public ColorPopupForm(Control colorBox, Color selectedColor, IWindowsFormsEditorService edSrv) : base(colorBox)
		{
			this.colorBox = null;
			this.TabControl = null;
			this.tbSystem = null;
			this.tbCustom = null;
			this.tbWeb = null;
			this.lbSystem = null;
			this.lbWeb = null;
			this.btOther = null;
			this.edSrv = null;
			try
			{
				base.SuspendLayout();
				this.TabControl = new McTabControl();
				this.TabControl.SuspendLayout();
				this.TabControl.ItemSize=new Size(60,20);
				this.TabControl.Dock = DockStyle.Fill;
				//this.TabControl.m_netFram=true;
				this.TabControl.Paint += new PaintEventHandler(this.TabControl_Paint);
				base.Controls.Add(this.TabControl);
				this.tbCustom = new McTabPage();
				this.tbCustom.SuspendLayout();
				this.tbCustom.Text = "Custom";
				this.tbCustom.DockPadding.All = 4;
				//this.TabControl.Controls.Add(this.tbCustom);
				this.TabControl.TabPages.Add(this.tbCustom);
				this.tbWeb = new McTabPage();
				this.tbWeb.SuspendLayout();
				this.tbWeb.Text = "Web";
				this.tbWeb.DockPadding.All = 4;
				//this.TabControl.Controls.Add(this.tbWeb);
				this.TabControl.TabPages.Add(this.tbWeb);
				this.tbSystem = new McTabPage();
				this.tbSystem.SuspendLayout();
				this.tbSystem.DockPadding.All = 4;
				this.tbSystem.Text = "System";
				//this.TabControl.Controls.Add(this.tbSystem);
				this.TabControl.TabPages.Add(this.tbSystem);
				this.lbWeb = new ListBox();
				this.lbWeb.DrawMode = DrawMode.OwnerDrawVariable;
				this.lbWeb.Dock = DockStyle.Fill;
				this.lbWeb.IntegralHeight = false;
				this.lbWeb.TabStop = false;
				this.lbWeb.MouseMove += new MouseEventHandler(this.lbWeb_MouseMove);
				this.lbWeb.DrawItem += new DrawItemEventHandler(this.lbWeb_DrawItem);
				this.tbWeb.Controls.Add(this.lbWeb);
				this.lbSystem = new ListBox();
				this.lbSystem.DrawMode = DrawMode.OwnerDrawVariable;
				this.lbSystem.Dock = DockStyle.Fill;
				this.lbSystem.IntegralHeight = false;
				this.lbSystem.TabStop = false;
				this.lbSystem.MouseMove += new MouseEventHandler(this.lbWeb_MouseMove);
				this.lbSystem.DrawItem += new DrawItemEventHandler(this.lbSystem_DrawItem);
				this.tbSystem.Controls.Add(this.lbSystem);
				this.btOther = new McButton();
				this.btOther.Location = new Point(0x3a,0xb4);// 0xd8);
				this.btOther.Name = "Color Editor";
				this.btOther.Text = "Color Editor";
				this.btOther.Size = new Size(100, 0x17);
				this.btOther.Click += new EventHandler(this.btOther_Click);
				this.tbCustom.Controls.Add(this.btOther);
				this.TabControl.ResumeLayout(false);
				this.tbCustom.ResumeLayout(false);
				this.tbWeb.ResumeLayout(false);
				this.tbSystem.ResumeLayout(false);
				base.ResumeLayout(false);
				this.edSrv = edSrv;
				this.colorBox = colorBox;
				this.SelectedColor = selectedColor;
				this.InitColors();
				this.lbSystem.SelectedIndex = -1;
				this.lbWeb.SelectedIndex = -1;
				bool flag1 = false;
				int num1 = 0;
				string text1 = selectedColor.ToString();
				foreach (Color color1 in McColors.McSystemColors)
				{
					if (color1.ToString() == text1)
					{
						this.TabControl.SelectedIndex = 2;
						this.lbSystem.SelectedIndex = num1;
						flag1 = true;
						break;
					}
					num1++;
				}
				if (!flag1)
				{
					num1 = 0;
					int num2 = selectedColor.ToArgb();
					foreach (Color color2 in McColors.Colors)
					{
						if (color2.ToArgb() == num2)
						{
							this.TabControl.SelectedIndex = 1;
							this.lbWeb.SelectedIndex = num1;
							flag1 = true;
							break;
						}
						num1++;
					}
				}
				if (!flag1)
				{
					this.TabControl.SelectedIndex = 0;
				}
				this.Localize();
			}
			catch
			{
			}
		}

		public override object SelectedItem
		{
			get {return this.SelectedColor as object;}
		}

//		public override int SelectedIndex
//		{
//			get {return 0;}
//		}
 		
		private void btOther_Click(object sender, EventArgs e)
		{
			base.LockClose = true;
			ColorDialog dialog1 = new ColorDialog();
			dialog1.FullOpen = true;
			if (dialog1.ShowDialog() == DialogResult.OK)
			{
				this.SelectedColor = dialog1.Color;
				if (this.colorBox is McColorPicker)
				{
					((McColorPicker) this.colorBox).SelectedColor = dialog1.Color;
				}
				this.ClosePopupForm();
				foreach (Color color1 in McColors.Colors)
				{
					if (color1.ToArgb() == this.SelectedColor.ToArgb())
					{
						this.SelectedColor = color1;
						break;
					}
				}
			}
			base.LockClose = false;
		}

		public override void ClosePopupForm()
		{
			base.ClosePopupForm();
			if (!base.LockClose && (this.edSrv != null))
			{
				this.edSrv.CloseDropDown();
			}
		}

		private void DrawItem(bool web, object sender, DrawItemEventArgs e)
		{
			if (e.Index != -1)
			{
				Color color1 = Color.Empty;
				string text1 = null;
				if (web)
				{
					color1 = McColors.Colors[e.Index];
					text1 = McColors.LocalizableColors[color1] as string;
				}
				else
				{
					color1 = McColors.McSystemColors[e.Index];
					text1 = McColors.LocalizableSystemColors[color1] as string;
				}
				Graphics graphics1 = e.Graphics;
				Rectangle rectangle1 = e.Bounds;
				if ((e.State & DrawItemState.Selected) > DrawItemState.None)
				{
					rectangle1.Width--;
				}
				Rectangle rectangle2 = new Rectangle(rectangle1.X + 2, rectangle1.Y + 2, 0x16, rectangle1.Height - 5);
				Rectangle rectangle3 = new Rectangle(rectangle2.Right + 2, rectangle1.Y, rectangle1.Width - rectangle2.X, rectangle1.Height);
				McPaint.DrawItem(graphics1, rectangle1, e.State, SystemColors.Window, SystemColors.ControlText);
				using (Brush brush1 = new SolidBrush(color1))
				{
					graphics1.FillRectangle(brush1, rectangle2);
				}
				graphics1.DrawRectangle(Pens.Black, rectangle2);
				using (StringFormat format1 = new StringFormat())
				{
					if (this.RightToLeft == RightToLeft.Yes)
					{
						format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					}
					graphics1.DrawString(text1, this.Font, Brushes.Black, (RectangleF) rectangle3, format1);
				}
			}
		}

 
		private void DrawPanel(object sender, bool isMouseOver)
		{
			Panel panel1 = sender as Panel;
			using (Graphics graphics1 = Graphics.FromHwnd(panel1.Handle))
			{
				Rectangle rectangle1 = panel1.ClientRectangle;
				if (isMouseOver)
				{
					graphics1.FillRectangle(McBrushes.Focus, rectangle1);
					graphics1.DrawRectangle(McPens.SelectedText, rectangle1.X, rectangle1.Y, rectangle1.Width - 1, rectangle1.Height - 1);
				}
				else
				{
					graphics1.FillRectangle(SystemBrushes.Control, rectangle1);
				}
				rectangle1.X += 2;
				rectangle1.Y += 2;
				rectangle1.Width -= 5;
				rectangle1.Height -= 5;
				graphics1.FillRectangle(new SolidBrush(panel1.BackColor), rectangle1);
				graphics1.DrawRectangle(SystemPens.ControlDark, rectangle1);
			}
		}

		private void InitColors()
		{
			this.panels = new Panel[McColors.CustomColors.Length];
			this.lbWeb.BeginUpdate();
			this.lbSystem.BeginUpdate();
			this.lbWeb.Items.Clear();
			foreach (Color color1 in McColors.Colors)
			{
				this.lbWeb.Items.Add(color1);
			}
			this.lbWeb.MouseUp += new MouseEventHandler(this.lbWeb_MouseUp);
			this.lbSystem.Items.Clear();
			foreach (Color color2 in McColors.McSystemColors)
			{
				this.lbSystem.Items.Add(color2);
			}
			this.lbWeb.EndUpdate();
			this.lbSystem.EndUpdate();
			this.lbSystem.MouseUp += new MouseEventHandler(this.lbSystem_MouseUp);
			int num1 = 6;
			int num2 = 6;
			int num3 = num1;
			int num4 = num2;
			int width=200;
			for (int num5 = 0; num5 < McColors.CustomColors.Length; num5++)
			{
				this.panels[num5] = new Panel();
				this.panels[num5].Left = num3;
				this.panels[num5].Top = num4;
				this.panels[num5].Height = 0x18;
				this.panels[num5].Width = 0x18;
				this.panels[num5].BackColor = McColors.CustomColors[num5];
				this.panels[num5].MouseEnter += new EventHandler(this.OnMouseEnterPanel);
				this.panels[num5].MouseLeave += new EventHandler(this.OnMouseLeavePanel);
				this.panels[num5].MouseDown += new MouseEventHandler(this.OnMouseDownPanel);
				this.panels[num5].MouseUp += new MouseEventHandler(this.OnMouseUpPanel);
				this.panels[num5].Paint += new PaintEventHandler(this.OnPanelPaint);
				this.tbCustom.Controls.Add(this.panels[num5]);
				num3 += 0x18;
				if (num3 > (width-5))
				{
					num3 = num1;
					num4 += 0x1a;
				}
			}
		}

 
		private void InitializeComponent()
		{
			this.AutoScaleBaseSize = new Size(5, 13);
			base.ClientSize = new Size(200,0x128);
			base.DockPadding.All = 2;
			base.Name = "ColorPopupForm";
			this.Text = "";
			base.Paint += new PaintEventHandler(this.McColorPickerForm_Paint);
		}

 
		private void lbSystem_DrawItem(object sender, DrawItemEventArgs e)
		{
			this.DrawItem(false, sender, e);
		}

		private void lbSystem_MouseUp(object sender, MouseEventArgs e)
		{
			if ((e == null) || ((e.Button & MouseButtons.Left) > MouseButtons.None))
			{
				if (this.lbSystem.SelectedItem != null)
				{
					this.SelectedColor = (Color) this.lbSystem.SelectedItem;
				}
				this.ClosePopupForm();
			}
		}

 
		private void lbWeb_DrawItem(object sender, DrawItemEventArgs e)
		{
			this.DrawItem(true, sender, e);
		}

 
		private void lbWeb_MouseMove(object sender, MouseEventArgs e)
		{
			ListBox box1 = sender as ListBox;
			Point point1 = box1.PointToClient(Cursor.Position);
			for (int num1 = 0; num1 < box1.Items.Count; num1++)
			{
				Rectangle rectangle1 = box1.GetItemRectangle(num1);
				if (rectangle1.Contains(point1))
				{
					box1.SelectedIndex = num1;
					return;
				}
			}
		}

		private void lbWeb_MouseUp(object sender, MouseEventArgs e)
		{
			if ((e == null) || ((e.Button & MouseButtons.Left) > MouseButtons.None))
			{
				if (this.lbWeb.SelectedItem != null)
				{
					this.SelectedColor = (Color) this.lbWeb.SelectedItem;
				}
				this.ClosePopupForm();
			}
		}

		private void Localize()
		{
			this.tbWeb.Text = McColors.WebStr;
			this.tbSystem.Text = McColors.SystemStr;
			this.tbCustom.Text = McColors.CustomStr;
			this.btOther.Text = "Color Editor";//McColors.OtherStr;
		}

 
		private void OnMouseDownPanel(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) > MouseButtons.None)
			{
				this.DrawPanel(sender, true);
			}
		}

 
		private void OnMouseEnterPanel(object sender, EventArgs e)
		{
			this.DrawPanel(sender, true);
		}

		private void OnMouseLeavePanel(object sender, EventArgs e)
		{
			this.DrawPanel(sender, false);
		}

 
		private void OnMouseUpPanel(object sender, MouseEventArgs e)
		{
			if ((e == null) || ((e.Button & MouseButtons.Left) > MouseButtons.None))
			{
				Panel panel1 = (Panel) sender;
				this.SelectedColor = panel1.BackColor;
				foreach (Color color1 in McColors.Colors)
				{
					if (color1.ToArgb() == this.SelectedColor.ToArgb())
					{
						this.SelectedColor = color1;
						break;
					}
				}
				this.ClosePopupForm();
			}
		}

		private void OnPanelPaint(object sender, PaintEventArgs e)
		{
			this.DrawPanel(sender, false);
		}

 
		private void McColorPickerForm_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, base.Width - 1, base.Height - 1);
		}

		private void TabControl_Paint(object sender, PaintEventArgs e)
		{
			base.OnPaint(e);
			if ((this.TabControl.SelectedIndex == 1) && (this.lbWeb.SelectedItem != null))
			{
				this.lbWeb.Focus();
			}
			else if ((this.TabControl.SelectedIndex == 2) && (this.lbSystem.SelectedItem != null))
			{
				this.lbSystem.Focus();
			}
		}

	

	}
}
