using System;
using System.Windows.Forms ;
using System.Data;
using mControl;
using mControl.Util;
 
namespace mControl.GridStyle 
{
    
    
	public class FilterDlg : mControl.WinCtl.Forms.CtlForm
	{
        
		// Required by the Windows Form Designer
		//private System.ComponentModel.IContainer components;
        
		protected const string STATUS_MESSAGE = "Number of records processed: ";
		protected const string NO_RECORDS_FOUND_MESSAGE = "No records were found that match the filter criteria.";
		protected const string CAPTION_TITLE = "mControl Filter";
		protected const string NO_RECORDS_TO_SORT_MESSAGE = "There are no records to sort.";
		protected const string UN_KNOWN_DATA_TYPE = "unKnown Data type";


		internal mControl.WinCtl.Controls.CtlGroupBox CtlGroupBox1;
		internal mControl.WinCtl.Controls.CtlLabel Label2;
		internal mControl.WinCtl.Controls.CtlComboBox cboCompare;
		internal mControl.WinCtl.Controls.CtlLabel Label1;
		internal mControl.WinCtl.Controls.CtlTextBox txtFilter;
		internal mControl.WinCtl.Controls.CtlComboBox cboFilelds;
        
		private DataTable dt;
		private DataView dtv;
		internal mControl.WinCtl.Controls.CtlLabel LblFilter;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;
        private mControl.WinCtl.Controls.CtlButton btnCancel;
        private mControl.WinCtl.Controls.CtlButton btnOk;
		private DataGrid grid;
		//private object CtlParent;
        
		public FilterDlg() 
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterDlg));
            this.CtlGroupBox1 = new mControl.WinCtl.Controls.CtlGroupBox();
            this.Label2 = new mControl.WinCtl.Controls.CtlLabel();
            this.txtFilter = new mControl.WinCtl.Controls.CtlTextBox();
            this.cboCompare = new mControl.WinCtl.Controls.CtlComboBox();
            this.Label1 = new mControl.WinCtl.Controls.CtlLabel();
            this.cboFilelds = new mControl.WinCtl.Controls.CtlComboBox();
            this.LblFilter = new mControl.WinCtl.Controls.CtlLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnCancel = new mControl.WinCtl.Controls.CtlButton();
            this.btnOk = new mControl.WinCtl.Controls.CtlButton();
            this.CtlGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.Name = "caption";
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.Text = "Grid Filter";
            // 
            // CtlGroupBox1
            // 
            this.CtlGroupBox1.Controls.Add(this.Label2);
            this.CtlGroupBox1.Controls.Add(this.txtFilter);
            this.CtlGroupBox1.Controls.Add(this.cboCompare);
            this.CtlGroupBox1.Controls.Add(this.Label1);
            this.CtlGroupBox1.Controls.Add(this.cboFilelds);
            this.CtlGroupBox1.Controls.Add(this.LblFilter);
            this.CtlGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.CtlGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CtlGroupBox1.Location = new System.Drawing.Point(8, 72);
            this.CtlGroupBox1.Name = "CtlGroupBox1";
            this.CtlGroupBox1.ReadOnly = false;
            this.CtlGroupBox1.Size = new System.Drawing.Size(296, 112);
            this.CtlGroupBox1.StylePainter = this.StyleGuideBase;
            this.CtlGroupBox1.TabIndex = 3;
            this.CtlGroupBox1.TabStop = false;
            this.CtlGroupBox1.Text = "Available Filters";
            // 
            // Label2
            // 
            this.Label2.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ImageIndex = 0;
            this.Label2.Location = new System.Drawing.Point(8, 24);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(64, 20);
            this.Label2.StylePainter = this.StyleGuideBase;
            this.Label2.TabIndex = 9;
            this.Label2.TabStop = false;
            this.Label2.Text = "Field";
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.White;
            this.txtFilter.ForeColor = System.Drawing.Color.Black;
            this.txtFilter.Location = new System.Drawing.Point(144, 56);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(136, 20);
            this.txtFilter.StylePainter = this.StyleGuideBase;
            this.txtFilter.TabIndex = 7;
            // 
            // cboCompare
            // 
            this.cboCompare.AcceptItems = false;
            this.cboCompare.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.cboCompare.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.cboCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompare.DropDownWidth = 64;
            this.cboCompare.FixSize = false;
            this.cboCompare.IntegralHeight = false;
            this.cboCompare.ItemHeight = 13;
            this.cboCompare.Location = new System.Drawing.Point(72, 56);
            this.cboCompare.Name = "cboCompare";
            this.cboCompare.Size = new System.Drawing.Size(64, 20);
            this.cboCompare.StylePainter = this.StyleGuideBase;
            this.cboCompare.TabIndex = 6;
            // 
            // Label1
            // 
            this.Label1.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ImageIndex = 0;
            this.Label1.Location = new System.Drawing.Point(8, 56);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(64, 20);
            this.Label1.StylePainter = this.StyleGuideBase;
            this.Label1.TabIndex = 5;
            this.Label1.TabStop = false;
            this.Label1.Text = "Filter";
            // 
            // cboFilelds
            // 
            this.cboFilelds.AcceptItems = false;
            this.cboFilelds.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.cboFilelds.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.cboFilelds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilelds.DropDownWidth = 208;
            this.cboFilelds.FixSize = false;
            this.cboFilelds.IntegralHeight = false;
            this.cboFilelds.ItemHeight = 13;
            this.cboFilelds.Location = new System.Drawing.Point(72, 24);
            this.cboFilelds.Name = "cboFilelds";
            this.cboFilelds.Size = new System.Drawing.Size(208, 20);
            this.cboFilelds.StylePainter = this.StyleGuideBase;
            this.cboFilelds.TabIndex = 4;
            // 
            // LblFilter
            // 
            this.LblFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LblFilter.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.LblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFilter.ImageIndex = 0;
            this.LblFilter.Location = new System.Drawing.Point(16, 88);
            this.LblFilter.Name = "LblFilter";
            this.LblFilter.Size = new System.Drawing.Size(264, 13);
            this.LblFilter.StylePainter = this.StyleGuideBase;
            this.LblFilter.TabIndex = 13;
            this.LblFilter.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // btnCancel
            // 
            this.btnCancel.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.FixSize = false;
            this.btnCancel.Location = new System.Drawing.Point(217, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 23);
            this.btnCancel.StylePainter = this.StyleGuideBase;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.FixSize = false;
            this.btnOk.Location = new System.Drawing.Point(125, 200);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 23);
            this.btnOk.StylePainter = this.StyleGuideBase;
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FilterDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(312, 237);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.CtlGroupBox1);
            this.Name = "FilterDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grid Filter";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FilterDlg_Load);
            this.Controls.SetChildIndex(this.CtlGroupBox1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.CtlGroupBox1.ResumeLayout(false);
            this.CtlGroupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		public static void Open(Grid g) 
		{
			FilterDlg f = new FilterDlg();
			f.grid = g.DataGrid ;
			f.dtv = ((DataView)(g.DataList));
			f.dt = f.dtv.Table;
			f.SetStyleLayout(g.CtlStyleLayout.Layout);
			f.Show();
		}

//		public static void Open(Grid g) 
//		{
//			FilterDlg f = new FilterDlg();
//			f.grid = g;
//			f.dtv = ((DataView)(g.DataSource));
//			f.dt = f.dtv.Table;
//			f.Show();
//		}
        
		public static void Open(Grid g, string ColName) 
		{
			FilterDlg f = new FilterDlg();
			f.grid = g.DataGrid;
			f.dtv = ((DataView)(g.DataSource));
			f.dt = f.dtv.Table;
			f.cboFilelds.Text = ColName;
			f.cboFilelds.Enabled = false;
			f.SetStyleLayout(g.CtlStyleLayout.Layout);
			f.Show();
		}
        
//		public static void Open(DataGridTableStyle t) 
//		{
//			FilterDlg f = new FilterDlg();
//			f.grid = t.DataGrid ;
//			f.dtv = ((DataView)(t.DataGrid.DataSource));
//			f.dt = f.dtv.Table;
//			f.Show();
//		}

		public override void SetStyleLayout(mControl.WinCtl.Controls.StyleLayout value)
		{
			base.SetStyleLayout (value);
			//this.SetStyleLayout(value);

		}
 
		private void FilterDlg_Load(object sender, System.EventArgs e)
		{
		
			SetFields();
			cboCompare.Items.AddRange(new object[] {
													   "<ALL>",
													   "<",
													   "<=",
													   "=",
													   ">=",
													   ">",
													   "like",
													   "<>"});
			this.cboCompare.Text = "<ALL>";
		}
        
		private void SetFields() 
		{
			foreach (DataColumn col in dt.Columns) 
			{
				this.cboFilelds.Items.Add(col.ColumnName);
			}
		}
        
		private string colType(string colName) 
		{
			return dt.Columns[colName].DataType.ToString();
		}
        
       private void btnCancel_Click(object sender, EventArgs e)
        {
			try
			{
				dtv.RowFilter = "";
				grid.DataSource = dtv;
				//this.Close();
				//base.OnNoClick (sender, e);
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message ,"mControl");
			}
		
		}

      private void btnOk_Click(object sender, EventArgs e)
        {
			string strFilter="";
			if(this.cboFilelds.Text.Length ==0)
			{
				MessageBox.Show ("Choose one field ","mControl");
				this.cboFilelds.DroppedDown =true;     
				return;
			}

			try
			{
				if ((this.cboFilelds.Text == "<ALL>")) 
				{
					strFilter = (this.cboFilelds.Text + (" " + this.txtFilter.Text));
				}
				else if (colType(this.cboFilelds.Text).EndsWith("String")) 
				{
					if ((this.cboCompare.Text == "like")) 
					{
						strFilter = (this.cboFilelds.Text + (" like \'%" 
							+ (this.txtFilter.Text + "%\'")));
					}
					else 
					{
						strFilter = (this.cboFilelds.Text + (" " 
							+ (this.cboCompare.Text + (" \'" 
							+ (this.txtFilter.Text + "\'")))));
					}
				}
				else if (colType(this.cboFilelds.Text).EndsWith("DateTime")) 
				{
					strFilter = (this.cboFilelds.Text + (" " 
						+ (this.cboCompare.Text + (" \'" 
						+ (this.txtFilter.Text + "\'")))));
				}
				else if (Regx.IsNumeric(this.txtFilter.Text)) 
				{
					strFilter = (this.cboFilelds.Text + (" " 
						+ (this.cboCompare.Text + (" " + this.txtFilter.Text))));
				}
				else 
				{
					MessageBox.Show(UN_KNOWN_DATA_TYPE,CAPTION_TITLE,MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
				}
				if ((txtFilter.Text.Trim().Length == 0)) 
				{
					MessageBox.Show (NO_RECORDS_FOUND_MESSAGE,CAPTION_TITLE,MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
					return;
				}
				dtv.RowFilter = strFilter;
				grid.DataSource = dtv;
				// //Display the number of rows in the view
				
				base.Text= (STATUS_MESSAGE + dtv.Count.ToString());
				LblFilter.Text = strFilter;
				if ((dtv.Count == 0)) 
				{
					MessageBox.Show (NO_RECORDS_FOUND_MESSAGE,CAPTION_TITLE,MessageBoxButtons.OK , MessageBoxIcon.Information );
				}
				//this.Close();
				//base.OnYesClick (sender, e);
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message ,"mControl");
			}
		
		}

  
 
	}
}

