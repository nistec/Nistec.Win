using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


using Nistec.WinForms;
using Nistec.Win;



namespace Nistec.GridView
{
	/// <summary>
	/// Summary description for ColumnFilterForm.
	/// </summary>
	public class GridColumnFilterDlg : Nistec.WinForms.McForm
	{

		#region NetFram

        //private void NetReflectedFram()
        //{
        //    //Nistec.Win.Net.nf_1.nf_2("ba7fa38f0b671cbc")
        //    panel1.NetReflectedFram("ba7fa38f0b671cbc");
        //    ctlOK.NetReflectedFram("ba7fa38f0b671cbc");
        //    ctlCancel.NetReflectedFram("ba7fa38f0b671cbc");
        //    chkAdjust.NetReflectedFram("ba7fa38f0b671cbc");
        //}

		#endregion

		private Nistec.WinForms.McPanel panel1;
		private Nistec.WinForms.McCheckedList ClbShowColumn;
		private Nistec.WinForms.McButton ctlOK;
		private Nistec.WinForms.McButton ctlCancel;
        private Nistec.WinForms.McCheckBox chkAdjust;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary>
        /// Initilaized Grid Column Filter Dlg
        /// </summary>
		public GridColumnFilterDlg()
		{
			InitializeComponent();
		}

        /// <summary>
        /// Initilaized Grid Column Filter Dlg
        /// </summary>
        /// <param name="g"></param>
		public GridColumnFilterDlg(Grid g): this()
		{
          this.grid=g;
          base.SetStyleLayout(g.LayoutManager.Layout);
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
            this.panel1 = new Nistec.WinForms.McPanel();
            this.chkAdjust = new Nistec.WinForms.McCheckBox();
            this.ctlOK = new Nistec.WinForms.McButton();
            this.ctlCancel = new Nistec.WinForms.McButton();
            this.ClbShowColumn = new Nistec.WinForms.McCheckedList();
            this.panel1.SuspendLayout();
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkAdjust);
            this.panel1.Controls.Add(this.ctlOK);
            this.panel1.Controls.Add(this.ctlCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(2, 256);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 40);
            this.panel1.StylePainter = this.StyleGuideBase;
            this.panel1.TabIndex = 22;
            // 
            // chkAdjust
            // 
            this.chkAdjust.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chkAdjust.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkAdjust.Location = new System.Drawing.Point(12, 11);
            this.chkAdjust.Name = "chkAdjust";
            this.chkAdjust.Size = new System.Drawing.Size(55, 13);
            this.chkAdjust.StylePainter = this.StyleGuideBase;
            this.chkAdjust.TabIndex = 25;
            this.chkAdjust.Text = "Adjust";
            this.chkAdjust.ToolTipText = "Adjust columns width";
            // 
            // ctlOK
            // 
            this.ctlOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlOK.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.ctlOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctlOK.Location = new System.Drawing.Point(144, 8);
            this.ctlOK.Name = "ctlOK";
            this.ctlOK.Size = new System.Drawing.Size(55, 23);
            this.ctlOK.StylePainter = this.StyleGuideBase;
            this.ctlOK.TabIndex = 0;
            this.ctlOK.Text = "Ok";
            this.ctlOK.ToolTipText = "Ok";
            this.ctlOK.Click += new System.EventHandler(this.ctlOK_Click);
            // 
            // ctlCancel
            // 
            this.ctlCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlCancel.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.ctlCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlCancel.Location = new System.Drawing.Point(83, 8);
            this.ctlCancel.Name = "ctlCancel";
            this.ctlCancel.Size = new System.Drawing.Size(55, 23);
            this.ctlCancel.StylePainter = this.StyleGuideBase;
            this.ctlCancel.TabIndex = 24;
            this.ctlCancel.Text = "Cancel";
            this.ctlCancel.ToolTipText = "Cancel";
            // 
            // ClbShowColumn
            // 
            this.ClbShowColumn.CheckOnClick = true;
            this.ClbShowColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClbShowColumn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ClbShowColumn.Location = new System.Drawing.Point(2, 38);
            this.ClbShowColumn.Name = "ClbShowColumn";
            this.ClbShowColumn.Size = new System.Drawing.Size(209, 210);
            this.ClbShowColumn.StylePainter = this.StyleGuideBase;
            this.ClbShowColumn.TabIndex = 23;
            // 
            // GridColumnFilterDlg
            // 
            this.AcceptButton = this.ctlOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CancelButton = this.ctlCancel;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(213, 298);
            this.CloseOnEscape = true;
            this.Controls.Add(this.ClbShowColumn);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridColumnFilterDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grid Columns";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ClbShowColumn, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private Grid grid;
        private GridColumnCollection cols;
        //private DialogResult dlgResult;

        /// <summary>
        /// Show Columns
        /// </summary>
        /// <param name="g"></param>
		public static void ShowColumns(Grid g)
		{
			GridColumnFilterDlg f=new GridColumnFilterDlg(g);
			//f.SetStyleLayout(g.LayoutManager.Layout);
            f.cols = g.GridColumns;
            f.SetSourceColumns(); 
			f.ShowDialog();
		}
        /// <summary>
        /// SetSourceColumns
        /// </summary>
		public void SetSourceColumns ()
		{
			try
			{
				bool check=false;
				string caption="";
				foreach (GridColumnStyle col in cols)
				{
					check=col.Visible;
					caption=col.MappingName;//col.HeaderText.Length>0 ? col.HeaderText: col.MappingName;
					ClbShowColumn.Items.Add(caption,check);
				}
			
			}
			catch (System.Exception a_Ex)
			{
				MessageBox.Show(a_Ex.Message);
			}
		}
        /// <summary>
        /// SetSourceColumns
        /// </summary>
        /// <param name="Columns"></param>
		public void SetSourceColumns (DataColumnCollection Columns)
		{
			try
			{
				foreach (DataColumn col in Columns)
				{
					ClbShowColumn.Items.Add(col.ColumnName.ToString());
				}
			
			}
			catch (System.Exception a_Ex)
			{
				MessageBox.Show(a_Ex.Message);
			}
		}
        /// <summary>
        /// Get Selected Columns
        /// </summary>
        /// <returns></returns>
		public CheckedListBox GetSelectedColumns()
		{
			return ClbShowColumn;
		}

		private void UpdateGridColumns()
		{
			if(ClbShowColumn.CheckedItems.Count==0)
			{
              MsgBox.ShowWarning("No column visible"); 
			  return;
			}

            foreach (GridColumnStyle c in this.cols)
			{
				if(ClbShowColumn.CheckedItems.Contains(c.MappingName))
			      c.Visible=true;  
				else
				  c.Visible=false;  
          	}
			//grid.ResetColumn(true);
			grid.Invalidate();
		}

		private void ctlOK_Click(object sender, System.EventArgs e)
		{
		  UpdateGridColumns();
          if (this.chkAdjust.Checked)
          {
              grid.AdjustColumns();
          }
		}

		
	}
}
