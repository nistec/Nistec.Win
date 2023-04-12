using System;
using System.Windows.Forms;
using System.Resources;
using System.Drawing;

using MControl.Printing.View;


namespace MControl.Printing.View.Design
{
    public class GroupOrderDlg : System.Windows.Forms.Form//MControl.WinForms.FormBase
	{
		#region NetFram


		#endregion

		public GroupOrderDlg()
		{
			//netFramwork.//NetReflectedFram();
			this.components = null;
			this.var0();
			this.var1();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void var0()
		{
			ResourceManager manager1 = new ResourceManager(typeof(GroupOrderDlg));
			this.cbCancel = new Button();
			this.cbOK = new Button();
			this.cbMoveDown = new Button();
			this.cbMoveUp = new Button();
			base.SuspendLayout();
			this.cbCancel.Location = new Point(0x70, 0xe0);
			this.cbCancel.Name = "cbCancel";
			this.cbCancel.Size = new Size(0x40, 0x18);
			this.cbCancel.TabIndex = 8;
			this.cbCancel.Text = "Cancel";
			this.cbCancel.Click += new EventHandler(this.var2);
			this.cbOK.Location = new Point(0x10, 0xe0);
			this.cbOK.Name = "cbOK";
			this.cbOK.Size = new Size(0x40, 0x18);
			this.cbOK.TabIndex = 7;
			this.cbOK.Text = "OK";
			this.cbOK.Click += new EventHandler(this.var3);
			this.cbMoveDown.Image = (Image) manager1.GetObject("cbMoveDown.Image");
			this.cbMoveDown.Location = new Point(0xb8, 0x70);
			this.cbMoveDown.Name = "cbMoveDown";
			this.cbMoveDown.Size = new Size(0x18, 0x20);
			this.cbMoveDown.TabIndex = 6;
			this.cbMoveDown.Click += new EventHandler(this.var4);
			this.cbMoveUp.Image = (Image) manager1.GetObject("cbMoveUp.Image");
			this.cbMoveUp.Location = new Point(0xb8, 0x48);
			this.cbMoveUp.Name = "cbMoveUp";
			this.cbMoveUp.Size = new Size(0x18, 0x20);
			this.cbMoveUp.TabIndex = 5;
			this.cbMoveUp.Click += new EventHandler(this.var5);
			this.AutoScaleBaseSize = new Size(5, 13);
			base.ClientSize = new Size(0xda, 0x107);
			base.Controls.Add(this.cbCancel);
			base.Controls.Add(this.cbOK);
			base.Controls.Add(this.cbMoveDown);
			base.Controls.Add(this.cbMoveUp);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GroupOrderDlg";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Set Group Order";
			base.TopMost = true;
			base.ResumeLayout(false);
		}

		private void var1()
		{
			this.var6 = new GroupOrderDlg.mtd393();
			this.var6.DisplayMember = "Name";
			this.var6.AllowDrop = true;
			this.var6.DrawMode = DrawMode.OwnerDrawFixed;
			this.var6.ItemHeight = 0x16;
			this.var6.Location = new Point(0x10, 8);
			this.var6.Name = "lbGroupOrder";
			this.var6.Size = new Size(160, 0xca);
			this.var6.DrawItem += new DrawItemEventHandler(this.var7);
			this.var6.MouseDown += new MouseEventHandler(this.var8);
			this.var6.DragEnter += new DragEventHandler(this.var9);
			this.var6.DragDrop += new DragEventHandler(this.var10);
			base.Controls.Add(this.var6);
		}

		private void var10(object sender, DragEventArgs e)
		{
			ListBox box1 = (ListBox) sender;
			string[] textArray1 = ((string) e.Data.GetData(DataFormats.Text)).Split(new char[] { '.' });
			Point point1 = box1.PointToClient(new Point(e.X, e.Y));
			this.var14 = box1.IndexFromPoint(point1);
			if ((this.var14 >= 0) && textArray1[0].Equals("dragGroup"))
			{
				this.var15();
				this.var12();
				this.var6.SelectedIndex = this.var14;
			}
		}

		private void var12()
		{
			this.var6.BeginUpdate();
			this.var6.Items.Clear();
			this.var6.Items.AddRange(this.var11);
			this.var6.EndUpdate();
		}

		private void var15()
		{
			Section section1;
			Section section2;
			if (this.var14 < this.var13)
			{
				section1 = this.var11[this.var14];
				this.var11.SetValue(this.var11[this.var13], this.var14);
				for (int num1 = this.var14 + 1; num1 <= this.var13; num1++)
				{
					section2 = this.var11[num1];
					this.var11[num1] = section1;
					section1 = section2;
				}
			}
			else if (this.var14 > this.var13)
			{
				section1 = this.var11[this.var14];
				this.var11.SetValue(this.var11[this.var13], this.var14);
				for (int num2 = this.var14 - 1; num2 >= this.var13; num2--)
				{
					section2 = this.var11[num2];
					this.var11[num2] = section1;
					section1 = section2;
				}
			}
		}

		private void var2(object sender, EventArgs e)
		{
			this.OK = false;
			base.Close();
		}

		private void var3(object sender, EventArgs e)
		{
			this.OK = true;
			base.Close();
		}

 
		private void var4(object sender, EventArgs e)
		{
			int num1 = this.var6.SelectedIndex;
			if (num1 < (this.var11.Length - 1))
			{
				object obj1 = this.var11.GetValue(num1);
				this.var11.SetValue(this.var11.GetValue((int) (num1 + 1)), num1);
				this.var11.SetValue(obj1, num1 + 1);
				this.var12();
				this.var6.SelectedIndex = num1 + 1;
			}
		}

 
		private void var5(object sender, EventArgs e)
		{
			int num1 = this.var6.SelectedIndex;
			if (num1 > 0)
			{
				object obj1 = this.var11.GetValue(num1);
				this.var11.SetValue(this.var11.GetValue((int) (num1 - 1)), num1);
				this.var11.SetValue(obj1, num1 - 1);
				this.var12();
				this.var6.SelectedIndex = num1 - 1;
			}
		}

 
		private void var7(object sender, DrawItemEventArgs e)
		{
			Brush brush1;
			Rectangle rectangle1 = e.Bounds;
			Rectangle rectangle2 = new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height);
			Rectangle rectangle3 = new Rectangle(e.Bounds.X, e.Bounds.Y, 20, e.Bounds.Height);
			Point[] pointArray3 = new Point[] { new Point(20, e.Bounds.Y), new Point(e.Bounds.X, e.Bounds.Y), new Point(e.Bounds.X, (e.Bounds.Y + e.Bounds.Height) - 1) };
			Point[] pointArray1 = pointArray3;
			pointArray3 = new Point[] { new Point(e.Bounds.X, (e.Bounds.Y + e.Bounds.Height) - 1), new Point(e.Bounds.X + 20, (e.Bounds.Y + e.Bounds.Height) - 1), new Point(20, e.Bounds.Y) };
			Point[] pointArray2 = pointArray3;
			if ((e.State & DrawItemState.Selected) > DrawItemState.None)
			{
				e.Graphics.FillRectangle(SystemBrushes.Highlight, rectangle2);
				brush1 = SystemBrushes.HighlightText;
			}
			else
			{
				e.Graphics.FillRectangle(SystemBrushes.Window, rectangle1);
				brush1 = SystemBrushes.ControlText;
			}
			e.Graphics.FillRectangle(SystemBrushes.Control, rectangle3);
			e.Graphics.DrawLines(SystemPens.ControlLightLight, pointArray1);
			e.Graphics.DrawLines(SystemPens.ControlDark, pointArray2);
			SizeF ef1 = e.Graphics.MeasureString(e.Index.ToString(), e.Font, 20);
			e.Graphics.DrawString(e.Index.ToString(), e.Font, SystemBrushes.ControlText, 10f - (ef1.Width / 2f), (float) (rectangle1.Y + 4));
			e.Graphics.DrawString(((GroupHeader) this.var6.Items[e.Index]).Name, e.Font, brush1, (float) (rectangle1.X + 0x19), (float) (rectangle1.Y + 4));
		}

 
		private void var8(object sender, MouseEventArgs e)
		{
			Point point1 = new Point(e.X, e.Y);
			ListBox box1 = (ListBox) sender;
			this.var13 = box1.IndexFromPoint(point1);
			if (this.var13 >= 0)
			{
				string text1 = "dragGroup." + box1.Items[this.var13].ToString();
				box1.DoDragDrop(text1, DragDropEffects.Move);
			}
		}

		private void var9(object sender, DragEventArgs e)
		{
			string[] textArray1 = ((string) e.Data.GetData(DataFormats.Text)).Split(new char[] { '.' });
			if (textArray1[0].Equals("dragGroup"))
			{
				e.Effect = DragDropEffects.Move;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

 
		public Section[] GroupList
		{
			get
			{
				return this.var11;
			}
			set
			{
				this.var11 = (Section[]) value.Clone();
				this.var12();
			}
		}
 

		// Fields
		private Section[] var11;
		private int var13;
		private int var14;
		internal Button cbCancel;
		internal Button cbMoveDown;
		internal Button cbMoveUp;
		internal Button cbOK;
		private System.ComponentModel.Container components;
		public bool OK;
		private mtd393 var6;

		// Nested Types
		internal class mtd393 : ListBox
		{
			internal mtd393()
			{
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.SetStyle(ControlStyles.ResizeRedraw, true);
			}

		}
	}

}
