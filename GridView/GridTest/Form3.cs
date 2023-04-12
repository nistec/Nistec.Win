using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GridTest
{
	/// <summary>
	/// Summary description for Form3.
	/// </summary>
	public class Form3 : System.Windows.Forms.Form
	{
		private mControl.GridView.Grid grid1;
		private mControl.GridView.GridTextColumn gridTextColumn1;
		private mControl.GridView.GridTextColumn gridTextColumn2;
		private mControl.GridView.GridTextColumn gridTextColumn3;
		private mControl.GridView.GridTextColumn gridTextColumn4;
		private mControl.GridView.GridTextColumn gridTextColumn5;
		private mControl.GridView.GridTextColumn gridTextColumn6;
        private NumericUpDown numericUpDown1;
        private WinFormsEdServiceDropDownExampleControl winFormsEdServiceDropDownExampleControl1;
        private WinFormsEdServiceDialogExampleControl winFormsEdServiceDialogExampleControl1;
        private mControl.WinCtl.Controls.CtlSpinEdit ctlSpinEdit2;
 		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form3()
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
				if(components != null)
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.winFormsEdServiceDropDownExampleControl1 = new GridTest.WinFormsEdServiceDropDownExampleControl();
            this.grid1 = new mControl.GridView.Grid();
            this.gridTextColumn1 = new mControl.GridView.GridTextColumn();
            this.gridTextColumn2 = new mControl.GridView.GridTextColumn();
            this.gridTextColumn3 = new mControl.GridView.GridTextColumn();
            this.gridTextColumn4 = new mControl.GridView.GridTextColumn();
            this.gridTextColumn5 = new mControl.GridView.GridTextColumn();
            this.gridTextColumn6 = new mControl.GridView.GridTextColumn();
            this.winFormsEdServiceDialogExampleControl1 = new GridTest.WinFormsEdServiceDialogExampleControl();
            this.ctlSpinEdit2 = new mControl.WinCtl.Controls.CtlSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(78, 277);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // winFormsEdServiceDropDownExampleControl1
            // 
            this.winFormsEdServiceDropDownExampleControl1.BackColor = System.Drawing.Color.Beige;
            this.winFormsEdServiceDropDownExampleControl1.Location = new System.Drawing.Point(50, 321);
            this.winFormsEdServiceDropDownExampleControl1.Name = "winFormsEdServiceDropDownExampleControl1";
            this.winFormsEdServiceDropDownExampleControl1.Size = new System.Drawing.Size(210, 74);
            this.winFormsEdServiceDropDownExampleControl1.TabIndex = 4;
            this.winFormsEdServiceDropDownExampleControl1.TestDropDownString = "Test String 3";
            // 
            // grid1
            // 
            this.grid1.BackColor = System.Drawing.Color.White;
            this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid1.CaptionText = "test";
            this.grid1.DataMember = "";
            this.grid1.ForeColor = System.Drawing.Color.Black;
            this.grid1.GridLineStyle = mControl.GridView.GridLineStyle.Solid;
            this.grid1.Location = new System.Drawing.Point(59, 12);
            this.grid1.Name = "grid1";
            this.grid1.PaintAlternating = false;
            this.grid1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grid1.Size = new System.Drawing.Size(417, 224);
            this.grid1.TabIndex = 2;
            // 
            // gridTextColumn1
            // 
            this.gridTextColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn1.AllowNull = false;
            this.gridTextColumn1.Format = "";
            this.gridTextColumn1.HeaderText = "Txt";
            this.gridTextColumn1.MappingName = "Txt";
            this.gridTextColumn1.Width = 75;
            // 
            // gridTextColumn2
            // 
            this.gridTextColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn2.AllowNull = false;
            this.gridTextColumn2.Format = "";
            this.gridTextColumn2.HeaderText = "Date";
            this.gridTextColumn2.MappingName = "Date";
            this.gridTextColumn2.Width = 75;
            // 
            // gridTextColumn3
            // 
            this.gridTextColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn3.AllowNull = false;
            this.gridTextColumn3.Format = "";
            this.gridTextColumn3.HeaderText = "Icon";
            this.gridTextColumn3.MappingName = "Icon";
            this.gridTextColumn3.Width = 75;
            // 
            // gridTextColumn4
            // 
            this.gridTextColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn4.AllowNull = false;
            this.gridTextColumn4.Format = "";
            this.gridTextColumn4.HeaderText = "Lbl";
            this.gridTextColumn4.MappingName = "Ibl";
            this.gridTextColumn4.Width = 75;
            // 
            // gridTextColumn5
            // 
            this.gridTextColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn5.AllowNull = false;
            this.gridTextColumn5.Format = "";
            this.gridTextColumn5.HeaderText = "Num";
            this.gridTextColumn5.MappingName = "Num";
            this.gridTextColumn5.Width = 75;
            // 
            // gridTextColumn6
            // 
            this.gridTextColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn6.AllowNull = false;
            this.gridTextColumn6.Format = "";
            this.gridTextColumn6.HeaderText = "Combo";
            this.gridTextColumn6.MappingName = "Combo";
            this.gridTextColumn6.Width = 75;
            // 
            // winFormsEdServiceDialogExampleControl1
            // 
            this.winFormsEdServiceDialogExampleControl1.BackColor = System.Drawing.Color.Beige;
            this.winFormsEdServiceDialogExampleControl1.Location = new System.Drawing.Point(302, 321);
            this.winFormsEdServiceDialogExampleControl1.Name = "winFormsEdServiceDialogExampleControl1";
            this.winFormsEdServiceDialogExampleControl1.Size = new System.Drawing.Size(210, 74);
            this.winFormsEdServiceDialogExampleControl1.TabIndex = 5;
            this.winFormsEdServiceDialogExampleControl1.TestDialogString = "Test String";
            // 
            // ctlSpinEdit2
            // 
            this.ctlSpinEdit2.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.ctlSpinEdit2.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Flat;
            this.ctlSpinEdit2.DecimalPlaces = 0;
            this.ctlSpinEdit2.Format = "N";
            this.ctlSpinEdit2.FormatType = mControl.Util.NumberFormats.StandadNumber;
            this.ctlSpinEdit2.Location = new System.Drawing.Point(364, 277);
            this.ctlSpinEdit2.Name = "ctlSpinEdit2";
            this.ctlSpinEdit2.Size = new System.Drawing.Size(112, 20);
            this.ctlSpinEdit2.TabIndex = 9;
            this.ctlSpinEdit2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // Form3
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(568, 429);
            this.Controls.Add(this.ctlSpinEdit2);
            this.Controls.Add(this.winFormsEdServiceDialogExampleControl1);
            this.Controls.Add(this.winFormsEdServiceDropDownExampleControl1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.grid1);
            this.Name = "Form3";
            this.Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	}
}
