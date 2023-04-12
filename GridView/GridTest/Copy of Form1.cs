using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using mControl.GridStyle;

namespace GridTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private mControl.WinCtl.Controls.CtlDatePicker textBox1;
		private mControl.WinCtl.Controls.CtlComboBox textBox2;
		private mControl.WinCtl.Controls.CtlDropDown textBox3;
		private mControl.GridStyle.GridComboColumn gridComboColumn1;
		private mControl.GridStyle.GridButtonColumn gridButtonColumn1;
		private System.Windows.Forms.TextBox textMsg;
		private System.Windows.Forms.Panel panel1;
		private mControl.GridStyle.GridLabelColumn gridLabelColumn1;
		private mControl.GridStyle.GridButtonColumn gridButtonColumn2;
		private mControl.GridStyle.GridProgressColumn gridProgressColumn1;
		private mControl.GridStyle.GridComboColumn gridComboColumn2;
		private mControl.GridStyle.GridDateColumn gridDateColumn1;
		private mControl.GridStyle.GridIconColumn gridIconColumn1;
		private mControl.GridStyle.GridMultiColumn gridMultiColumn1;
		private mControl.GridStyle.GridControlColumn gridControlColumn1;
		private mControl.GridStyle.GridTextColumn gridTextBoxColumn1;
		private mControl.GridStyle.GridBoolColumn gridBoolColumn1;
		private System.Windows.Forms.ImageList imageList1;
		private mControl.WinCtl.Controls.PopUpItem popUpItem1;
		private mControl.WinCtl.Controls.PopUpItem popUpItem2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private mControl.WinCtl.Controls.StyleGrid styleGrid1;


		private Grid label1;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.label1 = new mControl.GridStyle.Grid();
			this.gridControlColumn1 = new mControl.GridStyle.GridControlColumn();
			this.gridLabelColumn1 = new mControl.GridStyle.GridLabelColumn();
			this.gridIconColumn1 = new mControl.GridStyle.GridIconColumn();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.gridButtonColumn2 = new mControl.GridStyle.GridButtonColumn();
			this.popUpItem1 = new mControl.WinCtl.Controls.PopUpItem();
			this.popUpItem2 = new mControl.WinCtl.Controls.PopUpItem();
			this.gridProgressColumn1 = new mControl.GridStyle.GridProgressColumn();
			this.gridComboColumn2 = new mControl.GridStyle.GridComboColumn();
			this.gridDateColumn1 = new mControl.GridStyle.GridDateColumn();
			this.gridMultiColumn1 = new mControl.GridStyle.GridMultiColumn();
			this.gridTextBoxColumn1 = new mControl.GridStyle.GridTextColumn();
			this.gridBoolColumn1 = new mControl.GridStyle.GridBoolColumn();
			this.gridComboColumn1 = new mControl.GridStyle.GridComboColumn();
			this.gridButtonColumn1 = new mControl.GridStyle.GridButtonColumn();
			this.textBox1 = new mControl.WinCtl.Controls.CtlDatePicker();
			this.textBox2 = new mControl.WinCtl.Controls.CtlComboBox();
			this.textBox3 = new mControl.WinCtl.Controls.CtlDropDown();
			this.textMsg = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.styleGrid1 = new mControl.WinCtl.Controls.StyleGrid(this.components);
			((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.White;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.CaptionText = "test";
			this.label1.Columns.AddRange(new mControl.GridStyle.GridColumnStyle[] {
																					  this.gridControlColumn1,
																					  this.gridLabelColumn1,
																					  this.gridIconColumn1,
																					  this.gridButtonColumn2,
																					  this.gridProgressColumn1,
																					  this.gridComboColumn2,
																					  this.gridDateColumn1,
																					  this.gridMultiColumn1,
																					  this.gridTextBoxColumn1,
																					  this.gridBoolColumn1});
			this.label1.DataMember = "";
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.GridLineStyle = mControl.GridStyle.GridLineStyle.Solid;
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.MappingName = "";
			this.label1.Name = "label1";
			this.label1.PaintAlternating = true;
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label1.Size = new System.Drawing.Size(512, 224);
			this.label1.StylePainter = this.styleGrid1;
			this.label1.TabIndex = 0;
			this.label1.ButtonClick += new mControl.GridStyle.ButtonClickEventHandler(this.label1_ButtonClick);
			// 
			// gridControlColumn1
			// 
			this.gridControlColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridControlColumn1.AllowNull = false;
			this.gridControlColumn1.DataMember = "";
			this.gridControlColumn1.FilterType = mControl.GridStyle.GridFilterType.Equal;
			this.gridControlColumn1.Format = "";
			this.gridControlColumn1.FormatInfo = null;
			this.gridControlColumn1.HeaderText = "Grid";
			this.gridControlColumn1.MappingName = "Num";
			this.gridControlColumn1.MaxWidth = 1280;
			this.gridControlColumn1.Width = 75;
			// 
			// gridLabelColumn1
			// 
			this.gridLabelColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridLabelColumn1.AllowNull = false;
			this.gridLabelColumn1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(128)));
			this.gridLabelColumn1.DrawLabel = true;
			this.gridLabelColumn1.Format = "";
			this.gridLabelColumn1.FormatInfo = null;
			this.gridLabelColumn1.HeaderText = "Label";
			this.gridLabelColumn1.MappingName = "Lbl";
			this.gridLabelColumn1.Width = 75;
			// 
			// gridIconColumn1
			// 
			this.gridIconColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridIconColumn1.AllowNull = false;
			this.gridIconColumn1.HeaderText = "Icon";
			this.gridIconColumn1.IconList = this.imageList1;
			this.gridIconColumn1.MappingName = "Icon";
			this.gridIconColumn1.Visible = false;
			this.gridIconColumn1.Width = 0;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// gridButtonColumn2
			// 
			this.gridButtonColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridButtonColumn2.AllowNull = false;
			this.gridButtonColumn2.ButtonStyle = mControl.GridStyle.GridButtonStyle.Link;
			this.gridButtonColumn2.CaptionText = "Push";
			this.gridButtonColumn2.HeaderText = "Button";
			this.gridButtonColumn2.ImageList = this.imageList1;
			this.gridButtonColumn2.MappingName = "Button";
			this.gridButtonColumn2.MenuItems.AddRange(new mControl.WinCtl.Controls.PopUpItem[] {
																								   this.popUpItem1,
																								   this.popUpItem2});
			this.gridButtonColumn2.Width = 75;
			// 
			// popUpItem1
			// 
			this.popUpItem1.ImageIndex = 1;
			this.popUpItem1.ImageList = this.imageList1;
			this.popUpItem1.Text = "aaaaaa";
			// 
			// popUpItem2
			// 
			this.popUpItem2.ImageIndex = 2;
			this.popUpItem2.ImageList = this.imageList1;
			this.popUpItem2.Text = "bbbbb";
			// 
			// gridProgressColumn1
			// 
			this.gridProgressColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridProgressColumn1.AllowNull = false;
			this.gridProgressColumn1.HeaderText = "Progress";
			this.gridProgressColumn1.MappingName = "Progress";
			this.gridProgressColumn1.NullText = "0";
			this.gridProgressColumn1.Width = 75;
			// 
			// gridComboColumn2
			// 
			this.gridComboColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridComboColumn2.AllowNull = false;
			this.gridComboColumn2.ComboStyle = mControl.WinCtl.Controls.ComboStyles.Label;
			this.gridComboColumn2.Format = "";
			this.gridComboColumn2.FormatInfo = null;
			this.gridComboColumn2.HeaderText = "Combo";
			this.gridComboColumn2.Items.AddRange(new object[] {
																  "asdfdf",
																  "sdfsafd",
																  "ertewtr",
																  "dfghdfgh"});
			this.gridComboColumn2.MappingName = "Combo";
			this.gridComboColumn2.Width = 75;
			// 
			// gridDateColumn1
			// 
			this.gridDateColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridDateColumn1.AllowNull = false;
			this.gridDateColumn1.Format = "dd/MM/yyyy";
			this.gridDateColumn1.FormatInfo = null;
			this.gridDateColumn1.HeaderText = "Date";
			this.gridDateColumn1.MappingName = "Date";
			this.gridDateColumn1.RangeValue = new mControl.Util.RangeDate(new System.DateTime(1900, 1, 1, 0, 0, 0, 0), new System.DateTime(2999, 12, 31, 0, 0, 0, 0));
			this.gridDateColumn1.Width = 75;
			// 
			// gridMultiColumn1
			// 
			this.gridMultiColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridMultiColumn1.AllowNull = false;
			this.gridMultiColumn1.ComboStyle = mControl.WinCtl.Controls.ComboStyles.Button;
			this.gridMultiColumn1.Format = "";
			this.gridMultiColumn1.FormatInfo = null;
			this.gridMultiColumn1.HeaderText = "Multi";
			this.gridMultiColumn1.Items.Add("asdfasdf");
			this.gridMultiColumn1.Items.Add("sdfasdfs");
			this.gridMultiColumn1.Items.Add("asdfasdf");
			this.gridMultiColumn1.Items.Add("sadfasdf");
			this.gridMultiColumn1.MappingName = "Multi";
			this.gridMultiColumn1.MultiType = mControl.WinCtl.Controls.MultiComboTypes.Combo;
			this.gridMultiColumn1.Width = 75;
			// 
			// gridTextBoxColumn1
			// 
			this.gridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridTextBoxColumn1.AllowNull = false;
			this.gridTextBoxColumn1.Format = "";
			this.gridTextBoxColumn1.FormatInfo = null;
			this.gridTextBoxColumn1.HeaderText = "Text";
			this.gridTextBoxColumn1.MappingName = "Txt";
			this.gridTextBoxColumn1.Width = 75;
			// 
			// gridBoolColumn1
			// 
			this.gridBoolColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridBoolColumn1.AllowNull = false;
			this.gridBoolColumn1.HeaderText = "Bool";
			this.gridBoolColumn1.MappingName = "Bool";
			this.gridBoolColumn1.NullValue = ((object)(resources.GetObject("gridBoolColumn1.NullValue")));
			this.gridBoolColumn1.Width = 75;
			// 
			// gridComboColumn1
			// 
			this.gridComboColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridComboColumn1.AllowNull = false;
			this.gridComboColumn1.ComboStyle = mControl.WinCtl.Controls.ComboStyles.Label;
			this.gridComboColumn1.Format = "";
			this.gridComboColumn1.FormatInfo = null;
			this.gridComboColumn1.HeaderText = "combo";
			this.gridComboColumn1.Items.AddRange(new object[] {
																  "Lable1",
																  "Lable2",
																  "Lable3"});
			this.gridComboColumn1.MappingName = "Lbl";
			this.gridComboColumn1.Visible = false;
			this.gridComboColumn1.Width = 0;
			// 
			// gridButtonColumn1
			// 
			this.gridButtonColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.gridButtonColumn1.AllowNull = false;
			this.gridButtonColumn1.ButtonStyle = mControl.GridStyle.GridButtonStyle.Link;
			this.gridButtonColumn1.CaptionText = "button";
			this.gridButtonColumn1.HeaderText = "Button";
			this.gridButtonColumn1.ImageList = null;
			this.gridButtonColumn1.MappingName = "Button";
			this.gridButtonColumn1.NullText = "...";
			this.gridButtonColumn1.Visible = false;
			this.gridButtonColumn1.Width = 0;
			this.gridButtonColumn1.ButtonClick += new mControl.GridStyle.ButtonClickEventHandler(this.gridButtonColumn1_ButtonClick);
			// 
			// textBox1
			// 
			this.textBox1.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
			this.textBox1.Format = "d";
			this.textBox1.InputMask = "00/00/0000";
			this.textBox1.Location = new System.Drawing.Point(344, 328);
			this.textBox1.Name = "textBox1";
			this.textBox1.RangeValue = new mControl.Util.RangeDate(new System.DateTime(1900, 1, 1, 0, 0, 0, 0), new System.DateTime(2999, 12, 31, 0, 0, 0, 0));
			this.textBox1.Size = new System.Drawing.Size(88, 20);
			this.textBox1.TabIndex = 7;
			this.textBox1.Value = new System.DateTime(2006, 11, 10, 0, 21, 45, 750);
			// 
			// textBox2
			// 
			this.textBox2.AcceptItems = false;
			this.textBox2.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
			this.textBox2.DrawMode = System.Windows.Forms.DrawMode.Normal;
			this.textBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.textBox2.DropDownWidth = 0;
			this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.textBox2.IntegralHeight = false;
			this.textBox2.ItemHeight = 13;
			this.textBox2.Location = new System.Drawing.Point(184, 328);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(150, 20);
			this.textBox2.TabIndex = 6;
			// 
			// textBox3
			// 
			this.textBox3.AcceptItems = false;
			this.textBox3.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
			this.textBox3.ButtonToolTip = "DropDown list box";
			this.textBox3.ComboType = mControl.WinCtl.Controls.ComboListType.Normal;
			this.textBox3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.textBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			this.textBox3.DropDownWidth = 0;
			this.textBox3.IntegralHeight = false;
			this.textBox3.ItemHeight = 13;
			this.textBox3.Location = new System.Drawing.Point(24, 328);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(150, 20);
			this.textBox3.TabIndex = 5;
			// 
			// textMsg
			// 
			this.textMsg.Location = new System.Drawing.Point(24, 296);
			this.textMsg.Name = "textMsg";
			this.textMsg.Size = new System.Drawing.Size(512, 20);
			this.textMsg.TabIndex = 4;
			this.textMsg.Text = "";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(235)), ((System.Byte)(234)), ((System.Byte)(219)));
			this.panel1.Location = new System.Drawing.Point(440, 328);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(88, 24);
			this.panel1.TabIndex = 8;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(24, 256);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 24);
			this.button1.TabIndex = 9;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(128, 256);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 24);
			this.button2.TabIndex = 10;
			this.button2.Text = "button2";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// styleGrid1
			// 
			this.styleGrid1.AlternatingColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(240)));
			this.styleGrid1.BorderColor = System.Drawing.SystemColors.Desktop;
			this.styleGrid1.BorderHotColor = System.Drawing.SystemColors.HotTrack;
			this.styleGrid1.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.styleGrid1.CaptionForeColor = System.Drawing.Color.White;
			this.styleGrid1.StylePlan = mControl.WinCtl.Controls.Styles.Desktop;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 373);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.textMsg);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			//this.gridControlColumn1.MappingName="tbl2";
			this.gridControlColumn1.DataSource=DataSource.CreateDataTable("tbl2",10);
//this.label1.MappingName="tbl";
			this.label1.DataSource=DataSource.CreateDataTable("tbl",10);
		}

		private void gridButtonColumn1_Click(object sender, EventArgs e)
		{
			this.textMsg.Text= this.label1.CurrentRowIndex.ToString();
		}

		private void gridButtonColumn1_ButtonClick(object sender, ButtonClickEventArgs e)
		{
			this.textMsg.Text= e.Value.ToString();

		}

		private void label1_ButtonClick(object sender, mControl.GridStyle.ButtonClickEventArgs e)
		{
			if(sender is GridButtonColumn)
			{
				int row=this.label1.CurrentRowIndex;
				this.gridProgressColumn1.ResetProgress(row);
				//this.label1["Progress"]=0;
				//this.label1.Invalidate(this.label1.GetCellBounds(row,3));
				//this.label1.Focus();

				for(int i=0;i<= 100;i++)
				{
					this.gridProgressColumn1.SetProgressValue(i,row);
					//this.label1["Progress"]=i;
					//this.label1.Invalidate(this.label1.GetCellBounds(row,3));
					//System.Threading.Thread.Sleep(200);
					//Application.DoEvents();
					//this.label1.Refresh();//.Invalidate(this.label1.GetCellBounds(row,3));
				}
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Form2 f=new Form2();
			f.Show();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			Form3 f=new Form3();
			f.Show();
	
		}
	}
}
