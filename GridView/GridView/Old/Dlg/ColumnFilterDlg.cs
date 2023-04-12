using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using mControl.Util;

using mControl.WinCtl.Controls;



namespace mControl.GridStyle
{
	/// <summary>
	/// Summary description for ColumnFilterForm.
	/// </summary>
	public class ColumnFilterDlg : mControl.WinCtl.Forms.CtlForm
	{

		#region NetFram

		private void NetReflectedFram()
		{
			//mControl.Util.Net.NetFramReg.NetReflected("ba7fa38f0b671cbc")
			panel1.NetReflectedFram("ba7fa38f0b671cbc");
			ctlOK.NetReflectedFram("ba7fa38f0b671cbc");
			ctlCancel.NetReflectedFram("ba7fa38f0b671cbc");
		}

		#endregion

		private mControl.WinCtl.Controls.CtlPanel panel1;
		private mControl.WinCtl.Controls.CtlCheckedList ClbShowColumn;
		private mControl.WinCtl.Controls.CtlButton ctlOK;
		private mControl.WinCtl.Controls.CtlButton ctlCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ColumnFilterDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			NetReflectedFram();
//			if(netFramGrid.NetFram())
//			{
//				netFramwork.NetReflectedFram();
//			}
		}
		public ColumnFilterDlg(Grid g): this()
		{
          this.grid=g;
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
			this.panel1 = new mControl.WinCtl.Controls.CtlPanel();
			this.ctlOK = new mControl.WinCtl.Controls.CtlButton();
			this.ctlCancel = new mControl.WinCtl.Controls.CtlButton();
			this.ClbShowColumn = new mControl.WinCtl.Controls.CtlCheckedList();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// StyleGuideBase
			// 
			this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.StyleGuideBase.BorderColor = System.Drawing.Color.DimGray;
			this.StyleGuideBase.BorderHotColor = System.Drawing.Color.SlateGray;
			this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(74)));
			this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.Silver;
			this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.WhiteSmoke;
			this.StyleGuideBase.FlatColor = System.Drawing.Color.WhiteSmoke;
			this.StyleGuideBase.FocusedColor = System.Drawing.Color.FromArgb(((System.Byte)(63)), ((System.Byte)(63)), ((System.Byte)(63)));
			this.StyleGuideBase.FormColor = System.Drawing.Color.WhiteSmoke;
			this.StyleGuideBase.StylePlan = mControl.WinCtl.Controls.Styles.Silver;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.ctlOK);
			this.panel1.Controls.Add(this.ctlCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 255);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(210, 40);
			this.panel1.StylePainter = this.StyleGuideBase;
			this.panel1.TabIndex = 22;
			// 
			// ctlOK
			// 
			this.ctlOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ctlOK.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
			this.ctlOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ctlOK.FixSize = false;
			this.ctlOK.Location = new System.Drawing.Point(128, 8);
			this.ctlOK.Name = "ctlOK";
			this.ctlOK.Size = new System.Drawing.Size(72, 23);
			this.ctlOK.StylePainter = this.StyleGuideBase;
			this.ctlOK.TabIndex = 0;
			this.ctlOK.Text = "Ok";
			this.ctlOK.Click += new System.EventHandler(this.ctlOK_Click);
			// 
			// ctlCancel
			// 
			this.ctlCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ctlCancel.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
			this.ctlCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ctlCancel.FixSize = false;
			this.ctlCancel.Location = new System.Drawing.Point(48, 8);
			this.ctlCancel.Name = "ctlCancel";
			this.ctlCancel.Size = new System.Drawing.Size(72, 23);
			this.ctlCancel.StylePainter = this.StyleGuideBase;
			this.ctlCancel.TabIndex = 24;
			this.ctlCancel.Text = "Cancel";
			// 
			// ClbShowColumn
			// 
			this.ClbShowColumn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ClbShowColumn.CheckOnClick = true;
			this.ClbShowColumn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ClbShowColumn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.ClbShowColumn.Location = new System.Drawing.Point(0, 40);
			this.ClbShowColumn.Name = "ClbShowColumn";
			this.ClbShowColumn.Size = new System.Drawing.Size(210, 210);
			this.ClbShowColumn.StylePainter = this.StyleGuideBase;
			this.ClbShowColumn.TabIndex = 23;
			// 
			// ColumnFilterDlg
			// 
			this.AcceptButton = this.ctlOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.CancelButton = this.ctlCancel;
			this.CaptionHeight = 40;
			this.CaptionText = "Grid Columns";
			this.CaptionVisible = true;
			this.ClientSize = new System.Drawing.Size(210, 295);
			this.CloseOnEscape = true;
			this.Controls.Add(this.ClbShowColumn);
			this.Controls.Add(this.panel1);
			this.Name = "ColumnFilterDlg";
			this.Text = "Grid Columns";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.ClbShowColumn, 0);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private Grid grid;
		//private DialogResult dlgResult;

		public static void ShowColumns(Grid g)
		{
			ColumnFilterDlg f=new ColumnFilterDlg(g);
			f.SetStyleLayout(g.CtlStyleLayout.Layout);
			f.SetSourceColumns(g.Columns); 
			f.ShowDialog();
		}

		public void SetSourceColumns (GridColumnsCollection cols)
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
   		
			foreach(GridColumnStyle c in grid.Columns)
			{
				if(ClbShowColumn.CheckedItems.Contains(c.MappingName))
			      c.Visible=true;  
				else
				  c.Visible=false;  
          	}
			grid.ResetColumn(true);
		}

		private void ctlOK_Click(object sender, System.EventArgs e)
		{
		  UpdateGridColumns();
		}

		
	}
}
