using System;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;


namespace Nistec.Printing.View
{
    public class OutputFormatDlg : System.Windows.Forms.Form//Nistec.WinForms.FormBase
	{

		#region NetFram

        //private void //NetReflectedFram()
        //{
        //    this.cbCancel.//NetReflectedFram("ba7fa38f0b671cbc");
        //    this.cbOK.//NetReflectedFram("ba7fa38f0b671cbc");
        //    this.gbSample.//NetReflectedFram("ba7fa38f0b671cbc");
        //    this.ctlGroupBox1.//NetReflectedFram("ba7fa38f0b671cbc");
        //}

		#endregion

		// Fields
		private System.ComponentModel.Container components = null;
		internal string mtd3;
		internal bool mtd4;
		private Button cbCancel;
		private Button cbOK;
		private ComboBox cbxSymbol;
		private CheckBox chkbSeperator;
		//private Container components;
		private GroupBox gbSample;
		private ListBox lbCategory;
		private ListBox lbFormat;
		private Label lblDecimalPlaces;
		private Label lblGeneral;
		private Label lblNegNumber;
		private Label lblSample;
		private Label lblSymbol;
		private ListBox lbNegNumber;
		private NumericUpDown nudDecimalPlaces;
		private double var17;
		private DateTime date;
		private GroupBox ctlGroupBox1;//var18;
		private TextBox txtFormat;

		public OutputFormatDlg()
		{
			//netFramwork.//NetReflectedFram();
			this.components = null;
			this.var17 = 100;
			this.date = DateTime.Now;
			this.InitializeComponent();
			//NetReflectedFram();
			this.SetCategories();
			this.Resetting();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region InitializeComponent
 
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OutputFormatDlg));
			this.lbCategory = new System.Windows.Forms.ListBox();
			this.gbSample = new GroupBox();
			this.lblSample = new System.Windows.Forms.Label();
			this.cbOK = new Button();
			this.cbCancel = new Button();
			this.ctlGroupBox1 = new GroupBox();
			this.gbSample.SuspendLayout();
			this.ctlGroupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbCategory
			// 
			this.lbCategory.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbCategory.ItemHeight = 14;
			this.lbCategory.Location = new System.Drawing.Point(8, 24);
			this.lbCategory.Name = "lbCategory";
			this.lbCategory.Size = new System.Drawing.Size(112, 144);
			this.lbCategory.TabIndex = 0;
			this.lbCategory.SelectedIndexChanged += new System.EventHandler(this.var2);
			// 
			// gbSample
			// 
			this.gbSample.Controls.Add(this.lblSample);
	    	this.gbSample.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gbSample.Location = new System.Drawing.Point(144, 48);
			this.gbSample.Name = "gbSample";
			this.gbSample.Size = new System.Drawing.Size(200, 56);
			this.gbSample.TabIndex = 2;
			this.gbSample.TabStop = false;
			this.gbSample.Text = "Sample";
			// 
			// lblSample
			// 
			this.lblSample.Location = new System.Drawing.Point(8, 24);
			this.lblSample.Name = "lblSample";
			this.lblSample.Size = new System.Drawing.Size(184, 24);
			this.lblSample.TabIndex = 0;
			this.lblSample.Text = "sample";
			// 
			// cbOK
			// 
			this.cbOK.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cbOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbOK.Location = new System.Drawing.Point(8, 240);
			this.cbOK.Name = "cbOK";
			this.cbOK.Size = new System.Drawing.Size(56, 24);
			this.cbOK.TabIndex = 3;
			this.cbOK.Text = "OK";
			this.cbOK.Click += new System.EventHandler(this.var3);
			// 
			// cbCancel
			// 
			this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cbCancel.Font = new System.Drawing.Font("Arial", 8.25F);
			this.cbCancel.Location = new System.Drawing.Point(80, 240);
			this.cbCancel.Name = "cbCancel";
			this.cbCancel.Size = new System.Drawing.Size(56, 24);
			this.cbCancel.TabIndex = 4;
			this.cbCancel.Text = "Cancel";
			this.cbCancel.Click += new System.EventHandler(this.var4);
			// 
			// ctlGroupBox1
			// 
			this.ctlGroupBox1.Controls.Add(this.lbCategory);
			this.ctlGroupBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ctlGroupBox1.Location = new System.Drawing.Point(8, 48);
			this.ctlGroupBox1.Name = "ctlGroupBox1";
			this.ctlGroupBox1.Size = new System.Drawing.Size(128, 184);
			this.ctlGroupBox1.TabIndex = 5;
			this.ctlGroupBox1.TabStop = false;
			this.ctlGroupBox1.Text = "Category";
			// 
			// OutputFormatDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			//this.CaptionHeight = 40;
			//this.CaptionVisible = true;
			this.ClientSize = new System.Drawing.Size(354, 279);
			this.Controls.Add(this.ctlGroupBox1);
			this.Controls.Add(this.cbCancel);
			this.Controls.Add(this.cbOK);
			this.Controls.Add(this.gbSample);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OutputFormatDlg";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.Load += new System.EventHandler(this.var5);
			this.Controls.SetChildIndex(this.gbSample, 0);
			this.Controls.SetChildIndex(this.cbOK, 0);
			this.Controls.SetChildIndex(this.cbCancel, 0);
			this.Controls.SetChildIndex(this.ctlGroupBox1, 0);
			this.gbSample.ResumeLayout(false);
			this.ctlGroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void SetCategories()//var0()
		{
			this.lbCategory.Items.Add("General");
			this.lbCategory.Items.Add("Number");
			this.lbCategory.Items.Add("Currency");
			this.lbCategory.Items.Add("Date");
			this.lbCategory.Items.Add("Time");
			this.lbCategory.Items.Add("Percentage");
			this.lbCategory.Items.Add("Custom");
		}

		private void Resetting()//var1()
		{
			const int top=0x28;

			base.SuspendLayout();
			this.lblGeneral = new Label();
			this.lblGeneral.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblGeneral.Location = new Point(0x90, 140);
			this.lblGeneral.Name = "lblGeneral";
			this.lblGeneral.Size = new Size(200, 0x20);
			this.lblGeneral.TabIndex = 5;
			this.lblGeneral.Text = "General Format has not specific formating";
			this.lblNegNumber = new Label();
			this.lblNegNumber.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblNegNumber.Location = new Point(0x90, 0x9b+top);
			this.lblNegNumber.Name = "lblNegNumber";
			this.lblNegNumber.Size = new Size(0x98, 0x10);
			this.lblNegNumber.TabIndex = 10;
			this.lblNegNumber.Visible = false;
			this.lblNegNumber.Text = "Negative Numbers:";
			this.lbNegNumber = new ListBox();
			this.lbNegNumber.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbNegNumber.ItemHeight = 14;
			this.lbNegNumber.Location = new Point(0x90, 0xac+top);
			this.lbNegNumber.Name = "lbNegNumber";
			this.lbNegNumber.Size = new Size(200, 60);
			this.lbNegNumber.TabIndex = 9;
			this.lbNegNumber.Visible = false;
			this.lbNegNumber.Items.AddRange(new string[] { "-1,234.00", "(1,234.00)" });
			this.lbNegNumber.SelectedIndexChanged += new EventHandler(this.var6);
			this.chkbSeperator = new CheckBox();
			this.chkbSeperator.Checked = true;
			this.chkbSeperator.CheckState = CheckState.Checked;
			this.chkbSeperator.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chkbSeperator.Location = new Point(0x90, 0x68+top);
			this.chkbSeperator.Name = "chkbSeperator";
			this.chkbSeperator.Size = new Size(160, 0x18);
			this.chkbSeperator.TabIndex = 8;
			this.chkbSeperator.Visible = false;
			this.chkbSeperator.Text = "Use 1000 Seperator (,)";
			this.chkbSeperator.CheckedChanged += new EventHandler(this.var7);
			this.lblDecimalPlaces = new Label();
			this.lblDecimalPlaces.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblDecimalPlaces.Location = new Point(0x90, 0x48+top);
			this.lblDecimalPlaces.Name = "lblDecimalPlaces";
			this.lblDecimalPlaces.Size = new Size(0x60, 0x10);
			this.lblDecimalPlaces.TabIndex = 6;
			this.lblDecimalPlaces.Visible = false;
			this.lblDecimalPlaces.Text = "Decimal Places :";
			this.nudDecimalPlaces = new NumericUpDown();
			this.nudDecimalPlaces.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.nudDecimalPlaces.Location = new Point(0x90, 0x48+top);
			this.nudDecimalPlaces.Location = new Point(0xf8, 0x48+top);
			this.nudDecimalPlaces.Name = "nudDecimalPlaces";
			this.nudDecimalPlaces.Size = new Size(0x40, 20);
			this.nudDecimalPlaces.Visible = false;
			this.nudDecimalPlaces.Value = new decimal(2);
			this.nudDecimalPlaces.TabIndex = 7;
			this.nudDecimalPlaces.ValueChanged += new EventHandler(this.var8);
			this.cbxSymbol = new ComboBox();
			this.cbxSymbol.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.cbxSymbol.Location = new Point(0x90, 120+40);
			this.cbxSymbol.Name = "cbxSymbol";
			this.cbxSymbol.Size = new Size(200, 0x16);
			this.cbxSymbol.TabIndex = 5;
			this.cbxSymbol.Visible = false;
			this.cbxSymbol.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbxSymbol.DrawMode = DrawMode.OwnerDrawFixed;
			this.cbxSymbol.SelectedIndexChanged += new EventHandler(this.var9);
			this.cbxSymbol.DrawItem += new DrawItemEventHandler(this.var10);
			this.var11();
			this.lblSymbol = new Label();
			this.lblSymbol.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblSymbol.Location = new Point(0x90, 0x68+top);
			this.lblSymbol.Name = "lblSymbol";
			this.lblSymbol.Size = new Size(0x58, 0x10);
			this.lblSymbol.TabIndex = 6;
			this.lblSymbol.Visible = false;
			this.lblSymbol.Text = "Symbol :";
			this.lbFormat = new ListBox();
			this.lbFormat.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbFormat.Location = new Point(0x90, 0x6f+top);
			this.lbFormat.Name = "lbFormat";
			this.lbFormat.Size = new Size(200, 0x79);
			this.lbFormat.TabIndex = 5;
			this.lbFormat.Visible = false;
			this.lbFormat.SelectedIndexChanged += new EventHandler(this.var12);
			this.txtFormat = new TextBox();
			this.txtFormat.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.txtFormat.Location = new Point(0x90, 0x55+top);
			this.txtFormat.Name = "txtFormat";
			this.txtFormat.Size = new Size(200, 20);
			this.txtFormat.TabIndex = 6;
			this.txtFormat.Visible = false;
			this.txtFormat.Text = "";
			base.Controls.AddRange(new Control[] { this.lblGeneral, this.lblNegNumber, this.lbNegNumber, this.chkbSeperator, this.lblDecimalPlaces, this.nudDecimalPlaces, this.lblSymbol, this.cbxSymbol, this.lbFormat, this.txtFormat });
			base.ResumeLayout(false);
		}

 
		private void var10(object var15, DrawItemEventArgs var16)
		{
			Brush brush1;
			Rectangle rectangle1 = var16.Bounds;
			if ((var16.State & DrawItemState.Selected) > DrawItemState.None)
			{
				var16.Graphics.FillRectangle(SystemBrushes.Highlight, rectangle1);
				brush1 = Brushes.White;
			}
			else
			{
				var16.Graphics.FillRectangle(SystemBrushes.Window, rectangle1);
				brush1 = Brushes.Black;
			}
			CultureInfo info1 = (CultureInfo) this.cbxSymbol.Items[var16.Index];
			string text1 = info1.NumberFormat.CurrencySymbol.ToString();
			string text2 = info1.EnglishName;
			using (Font font1 = new Font("Arial", 8f, FontStyle.Bold))
			{
				var16.Graphics.DrawString(text1, font1, brush1, (float) (rectangle1.X + 1), (float) rectangle1.Y);
			}
			var16.Graphics.DrawString(text2, var16.Font, brush1, (float) (rectangle1.X + 30), (float) rectangle1.Y);
		}

		private void var11()
		{
			CultureInfo[] infoArray1 = CultureInfo.GetCultures(CultureTypes.AllCultures);
			this.cbxSymbol.BeginUpdate();
			CultureInfo[] infoArray2 = infoArray1;
			for (int num1 = 0; num1 < infoArray2.Length; num1++)
			{
				CultureInfo info1 = infoArray2[num1];
				if (!info1.IsNeutralCulture)
				{
					this.cbxSymbol.Items.Add(info1);
					if (info1.Name == "en-US")
					{
						this.cbxSymbol.SelectedIndex = this.cbxSymbol.Items.Count - 1;
					}
				}
			}
			this.cbxSymbol.EndUpdate();
		}

 
		private void var12(object var15, EventArgs var16)
		{
			this.mtd3 = (string) this.lbFormat.SelectedItem;
			this.txtFormat.Text = this.mtd3;
			if ((this.lbCategory.SelectedIndex == 3) || (this.lbCategory.SelectedIndex == 4))
			{
				this.lblSample.Text = this.date.ToString(this.mtd3);
			}
			else if (this.lbCategory.SelectedIndex == 6)
			{
				if (Regex.IsMatch(this.mtd3, "[dmyhs]"))
				{
					this.lblSample.Text = this.date.ToString(this.mtd3);
				}
				else
				{
					this.lblSample.Text = this.var17.ToString(this.mtd3);
				}
			}
		}

		private void var13(int var14)
		{
			this.lbFormat.BeginUpdate();
			this.lbFormat.Items.Clear();
			if (var14 != 2)
			{
				if (var14 == 3)
				{
					this.lbFormat.Items.AddRange(new string[] { 
																  "M/d", "M/d/yy", "M/d/yyyy", "MM/dd/yy", "MM/dd/yyyy", "d-MMM", "d-MMM-yy", "d-MMM-yyyy", "dd-MMM-yy", "dd-MMM-yyyy", "MMM-yy", "MMM-yyyy", "MMMM-yy", "MMMM-yyyy", "MMMM d, yyyy", "M/d/yy h:mm AM/PM", 
																  "M/d/yyyy h:mm AM/PM", "M/d/yy h:mm", "M/d/yyyy h:mm"
															  });
				}
				else if (var14 == 4)
				{
					this.lbFormat.Items.AddRange(new string[] { "h:mm", "h:mm AM/PM", "h:mm:ss", "h:mm:ss AM/PM", "mm:ss.0", "[h]:mm:ss", "M/d/yy h:mm AM/PM", "M/d/yy h:mm", "M/d/yyyy h:mm AM/PM", "M/d/yyyy h:mm" });
				}
				else if (var14 == 6)
				{
					this.lbFormat.Items.AddRange(new string[] { 
																  "0", "0.00", "#,##0", "#,##0.00", "#,##0;(#,##0)", "#,##0.00;(#,##0.00)", "$#,##0;($#,##0)", "$#,##0.00;($#,##0.00)", "0%", "0.00%", "0.00E+00", "##0.0E+0", "# ?/?", "# ??/??", "M/d/yy", "d-MMM-yy", 
																  "d-MMMM", "MMM-yy", "h:mm AM/PM", "h:mm:ss AM/PM", "h:mm", "h:mm:ss", "M/d/yy h:mm", "mm:ss", "mm:ss.0"
															  });
				}
			}
			this.lbFormat.EndUpdate();
		}

 
		private void var19()
		{
			string text3;
			string text4;
			NumberFormatInfo info2 = (NumberFormatInfo) NumberFormatInfo.CurrentInfo.Clone();
			string text2 = "%";
			if (this.nudDecimalPlaces.Value > new decimal(0))
			{
				text3 = new string('0', (int) this.nudDecimalPlaces.Value);
				text3 = "." + text3;
			}
			else
			{
				text3 = "";
			}
			switch (this.lbCategory.SelectedIndex)
			{
				case 0:
				{
					this.mtd3 = "";
					return;
				}
				case 1:
				{
					if (!this.chkbSeperator.Checked)
					{
						text4 = "0";
						break;
					}
					text4 = "#,##0";
					break;
				}
				case 2:
				{
					CultureInfo info1 = (CultureInfo) this.cbxSymbol.SelectedItem;
					string text1 = info1.NumberFormat.CurrencySymbol;
					text4 = "#,##0";
					this.mtd3 = text1 + text4 + text3;
					if (this.lbNegNumber.SelectedIndex == 1)
					{
						this.mtd3 = this.mtd3 + ";(" + this.mtd3 + ")";
					}
					this.var17 = 12345;
					this.lblSample.Text = this.var17.ToString(this.mtd3);
					return;
				}
				case 3:
				case 4:
				{
					return;
				}
				case 5:
				{
					text4 = "0";
					this.mtd3 = text4 + text3 + text2;
					this.var17 = 100;
					this.lblSample.Text = this.var17.ToString(this.mtd3);
					return;
				}
				default:
				{
					return;
				}
			}
			this.mtd3 = text4 + text3;
			if (this.lbNegNumber.SelectedIndex == 1)
			{
				this.mtd3 = this.mtd3 + ";(" + this.mtd3 + ")";
			}
			this.var17 = 12345;
			this.lblSample.Text = this.var17.ToString(this.mtd3);
		}

 
		private void var2(object var15, EventArgs var16)
		{
			switch (this.lbCategory.SelectedIndex)
			{
				case 0:
				{
					this.lblDecimalPlaces.Visible = false;
					this.nudDecimalPlaces.Visible = false;
					this.lblNegNumber.Visible = false;
					this.lbNegNumber.Visible = false;
					this.chkbSeperator.Visible = false;
					this.lblSymbol.Visible = false;
					this.cbxSymbol.Visible = false;
					this.lblGeneral.Visible = true;
					this.lblSample.Text = "100";
					return;
				}
				case 1:
				{
					this.lblGeneral.Visible = false;
					this.lblSymbol.Visible = false;
					this.cbxSymbol.Visible = false;
					this.lbFormat.Visible = false;
					this.txtFormat.Visible = false;
					this.lblSample.Text = "";
					this.lblDecimalPlaces.Visible = true;
					this.nudDecimalPlaces.Visible = true;
					this.chkbSeperator.Visible = true;
					this.lblNegNumber.Visible = true;
					this.lbNegNumber.Visible = true;
					this.var19();
					return;
				}
				case 2:
				{
					this.lblGeneral.Visible = false;
					this.chkbSeperator.Visible = false;
					this.lbFormat.Visible = false;
					this.txtFormat.Visible = false;
					this.lblSample.Text = "";
					this.lblDecimalPlaces.Visible = true;
					this.nudDecimalPlaces.Visible = true;
					this.lblNegNumber.Visible = true;
					this.lbNegNumber.Visible = true;
					this.lblSymbol.Visible = true;
					this.cbxSymbol.Visible = true;
					this.var19();
					return;
				}
				case 3:
				{
					this.lblGeneral.Visible = false;
					this.lblSample.Text = "";
					this.chkbSeperator.Visible = false;
					this.lblNegNumber.Visible = false;
					this.lbNegNumber.Visible = false;
					this.lblSymbol.Visible = false;
					this.cbxSymbol.Visible = false;
					this.lblDecimalPlaces.Visible = false;
					this.nudDecimalPlaces.Visible = false;
					this.var13(3);
					this.lbFormat.SetSelected(3, true);
					this.lbFormat.Visible = true;
					this.txtFormat.Visible = true;
					return;
				}
				case 4:
				{
					this.lblGeneral.Visible = false;
					this.lblSample.Text = "";
					this.chkbSeperator.Visible = false;
					this.lblNegNumber.Visible = false;
					this.lbNegNumber.Visible = false;
					this.lblSymbol.Visible = false;
					this.cbxSymbol.Visible = false;
					this.lblDecimalPlaces.Visible = false;
					this.nudDecimalPlaces.Visible = false;
					this.var13(4);
					this.lbFormat.SetSelected(0, true);
					this.lbFormat.Visible = true;
					this.txtFormat.Visible = true;
					return;
				}
				case 5:
				{
					this.lblGeneral.Visible = false;
					this.lblSample.Text = "";
					this.chkbSeperator.Visible = false;
					this.lblNegNumber.Visible = false;
					this.lbNegNumber.Visible = false;
					this.lblSymbol.Visible = false;
					this.cbxSymbol.Visible = false;
					this.lblDecimalPlaces.Visible = false;
					this.nudDecimalPlaces.Visible = false;
					this.lbFormat.Visible = false;
					this.txtFormat.Visible = false;
					this.lblDecimalPlaces.Visible = true;
					this.nudDecimalPlaces.Visible = true;
					this.var19();
					return;
				}
				case 6:
				{
					this.lblGeneral.Visible = false;
					this.lblSample.Text = "";
					this.chkbSeperator.Visible = false;
					this.lblNegNumber.Visible = false;
					this.lbNegNumber.Visible = false;
					this.lblSymbol.Visible = false;
					this.cbxSymbol.Visible = false;
					this.lblDecimalPlaces.Visible = false;
					this.nudDecimalPlaces.Visible = false;
					this.var13(6);
					this.lbFormat.SetSelected(0, true);
					this.lbFormat.Visible = true;
					this.txtFormat.Visible = true;
					return;
				}
			}
		}

		private void var3(object var15, EventArgs var16)
		{
			this.mtd4 = true;
			this.var19();
			base.Close();
		}

 
		private void var4(object var15, EventArgs var16)
		{
			this.mtd4 = false;
			base.Close();
		}

		private void var5(object var15, EventArgs var16)
		{
			this.lbCategory.SetSelected(0, true);
			this.lbNegNumber.SetSelected(0, true);
		}

 
		private void var6(object var15, EventArgs var16)
		{
			this.var19();
		}

 
		private void var7(object var15, EventArgs var16)
		{
			this.var19();
		}

 
		private void var8(object var15, EventArgs var16)
		{
			this.var19();
		}

 
		private void var9(object var15, EventArgs var16)
		{
			this.var19();
		}


	}
 

}
