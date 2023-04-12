using System;
using System.Windows.Forms ;
using System.Data;


 
namespace Nistec.WinForms
{
    
    
	public class FilterDlg : Nistec.WinForms.McForm 
	{
        
		#region Members
		// Required by the Windows Form Designer
		//private System.ComponentModel.IContainer components;
        
		protected const string STATUS_MESSAGE = "Number of records processed: ";
		protected const string NO_RECORDS_FOUND_MESSAGE = "No records were found that match the filter criteria.";
		protected const string CAPTION_TITLE = "Nistec Filter";
		protected const string NO_RECORDS_TO_SORT_MESSAGE = "There are no records to sort.";
		protected const string UN_KNOWN_DATA_TYPE = "unKnown Data type";


		internal Nistec.WinForms.McGroupBox McGroupBox1;
		internal Nistec.WinForms.McLabel Label2;
		internal Nistec.WinForms.McComboBox cboCompare;
		internal Nistec.WinForms.McLabel Label1;
		internal Nistec.WinForms.McTextBox txtFilter;
		internal Nistec.WinForms.McComboBox cboFilelds;
        
		internal Nistec.WinForms.McLabel LblFilter;
		private System.Windows.Forms.ImageList imageList1;
		private Nistec.WinForms.McButton btnOK;
		private Nistec.WinForms.McButton btnCancel;
		private Nistec.WinForms.McButton btnRemove;
		private System.ComponentModel.IContainer components;
		#endregion

		#region Ctor
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
		#endregion

		#region InitializeComponent

		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent() 
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterDlg));
            this.McGroupBox1 = new Nistec.WinForms.McGroupBox();
            this.Label2 = new Nistec.WinForms.McLabel();
            this.txtFilter = new Nistec.WinForms.McTextBox();
            this.cboCompare = new Nistec.WinForms.McComboBox();
            this.Label1 = new Nistec.WinForms.McLabel();
            this.cboFilelds = new Nistec.WinForms.McComboBox();
            this.LblFilter = new Nistec.WinForms.McLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new Nistec.WinForms.McButton();
            this.btnCancel = new Nistec.WinForms.McButton();
            this.btnRemove = new Nistec.WinForms.McButton();
            this.McGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(126)))), ((int)(((byte)(177)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Desktop;
            // 
            // McGroupBox1
            // 
            this.McGroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.McGroupBox1.Controls.Add(this.Label2);
            this.McGroupBox1.Controls.Add(this.txtFilter);
            this.McGroupBox1.Controls.Add(this.cboCompare);
            this.McGroupBox1.Controls.Add(this.Label1);
            this.McGroupBox1.Controls.Add(this.cboFilelds);
            this.McGroupBox1.Controls.Add(this.LblFilter);
            this.McGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.McGroupBox1.ForeColor = System.Drawing.Color.Black;
            this.McGroupBox1.Location = new System.Drawing.Point(8, 64);
            this.McGroupBox1.Name = "McGroupBox1";
            this.McGroupBox1.ReadOnly = false;
            this.McGroupBox1.Size = new System.Drawing.Size(296, 112);
            this.McGroupBox1.StylePainter = this.StyleGuideBase;
            this.McGroupBox1.TabIndex = 3;
            this.McGroupBox1.TabStop = false;
            this.McGroupBox1.Text = "Available Filters";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.White;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.ImageIndex = 0;
            this.Label2.Location = new System.Drawing.Point(8, 24);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(64, 20);
            this.Label2.StylePainter = this.StyleGuideBase;
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
            this.cboCompare.ButtonToolTip = "";
            this.cboCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompare.DropDownWidth = 64;
            this.cboCompare.FixSize = false;
            this.cboCompare.IntegralHeight = false;
            this.cboCompare.Location = new System.Drawing.Point(72, 56);
            this.cboCompare.Name = "cboCompare";
            this.cboCompare.Size = new System.Drawing.Size(64, 20);
            this.cboCompare.StylePainter = this.StyleGuideBase;
            this.cboCompare.TabIndex = 6;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.White;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.ImageIndex = 0;
            this.Label1.Location = new System.Drawing.Point(8, 56);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(64, 20);
            this.Label1.StylePainter = this.StyleGuideBase;
            this.Label1.Text = "Filter";
            // 
            // cboFilelds
            // 
            this.cboFilelds.ButtonToolTip = "";
            this.cboFilelds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilelds.DropDownWidth = 208;
            this.cboFilelds.FixSize = false;
            this.cboFilelds.IntegralHeight = false;
            this.cboFilelds.Location = new System.Drawing.Point(72, 24);
            this.cboFilelds.Name = "cboFilelds";
            this.cboFilelds.Size = new System.Drawing.Size(208, 20);
            this.cboFilelds.StylePainter = this.StyleGuideBase;
            this.cboFilelds.TabIndex = 4;
            // 
            // LblFilter
            // 
            this.LblFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.LblFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFilter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblFilter.ImageIndex = 0;
            this.LblFilter.Location = new System.Drawing.Point(16, 88);
            this.LblFilter.Name = "LblFilter";
            this.LblFilter.Size = new System.Drawing.Size(264, 13);
            this.LblFilter.StylePainter = this.StyleGuideBase;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // btnOK
            // 
            this.btnOK.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.Location = new System.Drawing.Point(168, 184);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 24);
            this.btnOK.StylePainter = this.StyleGuideBase;
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Ok";
            this.btnOK.ToolTipText = "Ok";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.Location = new System.Drawing.Point(240, 184);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 24);
            this.btnCancel.StylePainter = this.StyleGuideBase;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.btnRemove.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRemove.Location = new System.Drawing.Point(8, 184);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(80, 24);
            this.btnRemove.StylePainter = this.StyleGuideBase;
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Filter Remove";
            this.btnRemove.ToolTipText = "Filter Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // FilterDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(320, 221);
            this.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.McGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterDlg";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FilterDlg_Load);
            this.Controls.SetChildIndex(this.McGroupBox1, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnRemove, 0);
            this.McGroupBox1.ResumeLayout(false);
            this.McGroupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private DataTable dt;
		private DataView dtv;


		public static void Open(ILayout ctl,DataView dv) 
		{
			FilterDlg f = new FilterDlg();
			f.dtv = (DataView)dv;
			f.dt = f.dtv.Table;
			f.SetStyleLayout(ctl.LayoutManager.Layout);
			f.ShowDialog();
		}

        
		public static void Open(ILayout ctl,DataView dv, string ColName) 
		{
			FilterDlg f = new FilterDlg();
			f.dtv = (DataView)dv;
			f.dt = f.dtv.Table;
			f.cboFilelds.Text = ColName;
			f.cboFilelds.Enabled = false;
			f.SetStyleLayout(ctl.LayoutManager.Layout);
			f.ShowDialog();
		}
        
		public override void SetStyleLayout(Nistec.WinForms.StyleLayout value)
		{
			base.SetStyleLayout (value);
			base.SetChildrenStyle();
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
        
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//dtv.RowFilter = "";
				//grid.DataSource = dtv;
				this.Close();
				//base.OnNoClick (sender, e);
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message ,"Nistec");
			}
		
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			string strFilter="";
			if(this.cboFilelds.Text.Length ==0)
			{
				MessageBox.Show ("Choose field ","Nistec");
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
                else if (WinHelp.IsNumber(this.txtFilter.Text)) 
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
				//grid.DataSource = dtv;
				// //Display the number of rows in the view
				
				base.Text= (STATUS_MESSAGE + dtv.Count.ToString());
				LblFilter.Text = strFilter;
				if ((dtv.Count == 0)) 
				{
					MessageBox.Show (NO_RECORDS_FOUND_MESSAGE,CAPTION_TITLE,MessageBoxButtons.OK , MessageBoxIcon.Information );
				}
				this.Close();
				//base.OnYesClick (sender, e);
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message ,"Nistec");
			}
		
		}

			private void btnRemove_Click(object sender, System.EventArgs e)
			{
			try
			{
				dtv.RowFilter = "";
				//grid.DataSource = dtv;
				this.Close();
				//base.OnCancelClick (sender, e);
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message ,"Nistec");
			}
		}


	}
}

