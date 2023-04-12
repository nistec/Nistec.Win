using System;
using System.Windows.Forms ;
using System.Data;
using mControl;

namespace mControl.GridStyle 
{
    
    
	public class DimensionDlg : mControl.WinCtl.Forms.FormBase
	{
        
		// Required by the Windows Form Designer
		//private System.ComponentModel.IContainer components;
        
		// NOTE: The following procedure is required by the Windows Form Designer
		// It can be modified using the Windows Form Designer.  
		// Do not modify it using the code editor.
		internal System.Windows.Forms.GroupBox CtlGroupBox1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Button cmdOK;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox VirtualName;
		internal System.Windows.Forms.NumericUpDown Rows;
		internal System.Windows.Forms.NumericUpDown Cols;
        
		public DimensionDlg() 
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			// Add any initialization after the InitializeComponent() call
		}
        
		// Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing) 
		{
			/*if (disposing) 
			{
				if (!(components == null)) 
				{
					components.Dispose();
				}
			}*/
			base.Dispose(disposing);
		}
        
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent() 
		{
			this.CtlGroupBox1 = new System.Windows.Forms.GroupBox();
			this.VirtualName = new System.Windows.Forms.TextBox();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Rows = new System.Windows.Forms.NumericUpDown();
			this.Cols = new System.Windows.Forms.NumericUpDown();
			this.cmdOK = new System.Windows.Forms.Button();
			this.CtlGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Rows)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Cols)).BeginInit();
			this.SuspendLayout();
			// 
			// CtlGroupBox1
			// 
			this.CtlGroupBox1.Controls.Add(this.VirtualName);
			this.CtlGroupBox1.Controls.Add(this.Label3);
			this.CtlGroupBox1.Controls.Add(this.Label2);
			this.CtlGroupBox1.Controls.Add(this.Label1);
			this.CtlGroupBox1.Controls.Add(this.Rows);
			this.CtlGroupBox1.Controls.Add(this.Cols);
			this.CtlGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CtlGroupBox1.Location = new System.Drawing.Point(16, 8);
			this.CtlGroupBox1.Name = "CtlGroupBox1";
			this.CtlGroupBox1.Size = new System.Drawing.Size(296, 88);
			this.CtlGroupBox1.TabIndex = 3;
			this.CtlGroupBox1.TabStop = false;
			this.CtlGroupBox1.Text = "Data Source";
			// 
			// VirtualName
			// 
			this.VirtualName.Location = new System.Drawing.Point(88, 24);
			this.VirtualName.Name = "VirtualName";
			this.VirtualName.Size = new System.Drawing.Size(192, 20);
			this.VirtualName.TabIndex = 0;
			this.VirtualName.Text = "VirtualGrid";
			// 
			// Label3
			// 
			this.Label3.Location = new System.Drawing.Point(152, 56);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(56, 23);
			this.Label3.TabIndex = 11;
			this.Label3.Text = "Cols";
			// 
			// Label2
			// 
			this.Label2.Location = new System.Drawing.Point(8, 24);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(72, 23);
			this.Label2.TabIndex = 9;
			this.Label2.Text = "Virtual Name";
			// 
			// Label1
			// 
			this.Label1.Location = new System.Drawing.Point(8, 56);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(64, 23);
			this.Label1.TabIndex = 5;
			this.Label1.Text = "Rows";
			// 
			// Rows
			// 
			this.Rows.Location = new System.Drawing.Point(88, 56);
			this.Rows.Minimum = new System.Decimal(new int[] {
																 1,
																 0,
																 0,
																 0});
			this.Rows.Name = "Rows";
			this.Rows.Size = new System.Drawing.Size(64, 20);
			this.Rows.TabIndex = 1;
			this.Rows.Value = new System.Decimal(new int[] {
															   1,
															   0,
															   0,
															   0});
			// 
			// Cols
			// 
			this.Cols.Location = new System.Drawing.Point(208, 56);
			this.Cols.Minimum = new System.Decimal(new int[] {
																 1,
																 0,
																 0,
																 0});
			this.Cols.Name = "Cols";
			this.Cols.Size = new System.Drawing.Size(72, 20);
			this.Cols.TabIndex = 2;
			this.Cols.Value = new System.Decimal(new int[] {
															   1,
															   0,
															   0,
															   0});
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(224, 104);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 10;
			this.cmdOK.Text = "OK";
			// 
			// DimensionDlg
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(322, 135);
			this.Controls.Add(this.CtlGroupBox1);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DimensionDlg";
			this.Text = "Virtual Grid Dimension";
			this.TopMost = true;
			this.CtlGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Rows)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Cols)).EndInit();
			this.ResumeLayout(false);

		}
        
		public static void Open() 
		{
			DimensionDlg f = new DimensionDlg();
			f.ShowDialog();
		}
        
		private void cmdOK_Click(object sender, System.EventArgs e) 
		{
			this.Close();
		}
	}
}

