using System;
using System.Windows.Forms ;
using System.Data;
using Nistec;

namespace Nistec.GridView 
{
    
    /// <summary>
    /// Dimension Dialog for virtual source
    /// </summary>
	public class DimensionDlg : Nistec.WinForms.FormBase
	{
        
		// Required by the Windows Form Designer
		//private System.ComponentModel.IContainer components;
        
		// NOTE: The following procedure is required by the Windows Form Designer
		// It can be modified using the Windows Form Designer.  
		// Do not modify it using the code editor.
		internal System.Windows.Forms.GroupBox McGroupBox1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Button cmdOK;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox VirtualName;
		internal System.Windows.Forms.NumericUpDown Rows;
		internal System.Windows.Forms.NumericUpDown Cols;
        
        /// <summary>
        /// Dimension Dialog ctor
        /// </summary>
		public DimensionDlg() 
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			// Add any initialization after the InitializeComponent() call
		}
        
		
        /// <summary>
        /// Form overrides dispose to clean up the component list.
        /// </summary>
        /// <param name="disposing"></param>
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
            this.McGroupBox1 = new System.Windows.Forms.GroupBox();
            this.VirtualName = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Rows = new System.Windows.Forms.NumericUpDown();
            this.Cols = new System.Windows.Forms.NumericUpDown();
            this.cmdOK = new System.Windows.Forms.Button();
            this.McGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cols)).BeginInit();
            this.SuspendLayout();
            // 
            // McGroupBox1
            // 
            this.McGroupBox1.Controls.Add(this.VirtualName);
            this.McGroupBox1.Controls.Add(this.Label3);
            this.McGroupBox1.Controls.Add(this.Label2);
            this.McGroupBox1.Controls.Add(this.Label1);
            this.McGroupBox1.Controls.Add(this.Rows);
            this.McGroupBox1.Controls.Add(this.Cols);
            this.McGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.McGroupBox1.Location = new System.Drawing.Point(16, 8);
            this.McGroupBox1.Name = "McGroupBox1";
            this.McGroupBox1.Size = new System.Drawing.Size(296, 88);
            this.McGroupBox1.TabIndex = 3;
            this.McGroupBox1.TabStop = false;
            this.McGroupBox1.Text = "Data Source";
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
            this.Rows.Name = "Rows";
            this.Rows.Size = new System.Drawing.Size(64, 20);
            this.Rows.TabIndex = 1;
            // 
            // Cols
            // 
            this.Cols.Location = new System.Drawing.Point(208, 56);
            this.Cols.Name = "Cols";
            this.Cols.Size = new System.Drawing.Size(72, 20);
            this.Cols.TabIndex = 2;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(224, 104);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 10;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // DimensionDlg
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(322, 135);
            this.Controls.Add(this.McGroupBox1);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DimensionDlg";
            this.Text = "Virtual Grid Dimension";
            this.TopMost = true;
            this.McGroupBox1.ResumeLayout(false);
            this.McGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cols)).EndInit();
            this.ResumeLayout(false);

		}
        /// <summary>
        /// Open Dimension dialog
        /// </summary>
		public static void Open() 
		{
			DimensionDlg f = new DimensionDlg();
     		f.ShowDialog();
		}
        /// <summary>
        /// Open Dimension dialog
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public static DimensionDlg Open(int cols, int rows, string sourceName)
        {
            DimensionDlg f = new DimensionDlg();
            f.Rows.Value = rows;
            f.Cols.Value = cols;
            f.VirtualName.Text = sourceName;
            f.ShowDialog();
            return f;
        }
        
        
		private void cmdOK_Click(object sender, System.EventArgs e) 
		{
            this.DialogResult = DialogResult.Yes;
			this.Close();
		}
	}
}

