using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using mControl.Util;

namespace mControl.GridStyle
{
	/// <summary>
	/// Summary description for DataFilter.
	/// </summary>
	public class GridFilterDlg : mControl.WinCtl.Forms.CtlForm
	{

		#region NetFram

		private void NetReflectedFram()
		{
			//mControl.Util.Net.NetFramReg.NetReflected("ba7fa38f0b671cbc")
			ButtonSelect.NetReflectedFram("ba7fa38f0b671cbc");
			panel2.NetReflectedFram("ba7fa38f0b671cbc");
			ctlCancel.NetReflectedFram("ba7fa38f0b671cbc");
			ctlRemove.NetReflectedFram("ba7fa38f0b671cbc");
		}

		#endregion

		private DataView  dataSource;
		private DataTable FilterDataTable;
		
		// Add new oprrations according to the operation which support by the database you connect
		private string [] Operations = {"None","<","<=",">",">=","=","<>","LIKE"};
		private mControl.WinCtl.Controls.CtlButton ButtonSelect;
		private mControl.WinCtl.Controls.CtlPanel panel2;
		private mControl.GridStyle.Grid DataGridFilterData;
		private mControl.WinCtl.Controls.CtlButton ctlCancel;
		private mControl.GridStyle.Columns.GridTextColumn ColumnName;
		private mControl.GridStyle.Columns.GridTextColumn FilterText;
		private System.Windows.Forms.ImageList imageList1;
		private mControl.WinCtl.Controls.CtlButton ctlRemove;
		private mControl.GridStyle.Columns.GridComboColumn FilterType;
		private System.ComponentModel.IContainer components;

		public GridFilterDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			NetReflectedFram();
			//if(netFramGrid.NetFram())
			//{
			//	netFramwork.NetReflectedFram();
			//}
		}

		public GridFilterDlg(Grid g):this()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GridFilterDlg));
			this.ButtonSelect = new mControl.WinCtl.Controls.CtlButton();
			this.panel2 = new mControl.WinCtl.Controls.CtlPanel();
			this.ctlRemove = new mControl.WinCtl.Controls.CtlButton();
			this.ctlCancel = new mControl.WinCtl.Controls.CtlButton();
			this.DataGridFilterData = new mControl.GridStyle.Grid();
			this.ColumnName = new mControl.GridStyle.Columns.GridTextColumn();
			this.FilterType = new mControl.GridStyle.Columns.GridComboColumn();
			this.FilterText = new mControl.GridStyle.Columns.GridTextColumn();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DataGridFilterData)).BeginInit();
			this.SuspendLayout();
			// 
			// ButtonSelect
			// 
			this.ButtonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonSelect.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
			this.ButtonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ButtonSelect.FixSize = false;
			this.ButtonSelect.Location = new System.Drawing.Point(288, 4);
			this.ButtonSelect.Name = "ButtonSelect";
			this.ButtonSelect.Size = new System.Drawing.Size(72, 23);
			this.ButtonSelect.StylePainter = this.StyleGuideBase;
			this.ButtonSelect.TabIndex = 1;
			this.ButtonSelect.Text = "Ok";
			this.ButtonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.ctlRemove);
			this.panel2.Controls.Add(this.ctlCancel);
			this.panel2.Controls.Add(this.ButtonSelect);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 202);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(362, 32);
			this.panel2.StylePainter = this.StyleGuideBase;
			this.panel2.TabIndex = 23;
			// 
			// ctlRemove
			// 
			this.ctlRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ctlRemove.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
			this.ctlRemove.DialogResult = System.Windows.Forms.DialogResult.None;
			this.ctlRemove.FixSize = false;
			this.ctlRemove.Location = new System.Drawing.Point(8, 4);
			this.ctlRemove.Name = "ctlRemove";
			this.ctlRemove.Size = new System.Drawing.Size(80, 23);
			this.ctlRemove.StylePainter = this.StyleGuideBase;
			this.ctlRemove.TabIndex = 3;
			this.ctlRemove.Text = "RemoveFilter";
			this.ctlRemove.Click += new System.EventHandler(this.ctlRemove_Click);
			// 
			// ctlCancel
			// 
			this.ctlCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ctlCancel.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
			this.ctlCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ctlCancel.FixSize = false;
			this.ctlCancel.Location = new System.Drawing.Point(200, 4);
			this.ctlCancel.Name = "ctlCancel";
			this.ctlCancel.Size = new System.Drawing.Size(80, 23);
			this.ctlCancel.StylePainter = this.StyleGuideBase;
			this.ctlCancel.TabIndex = 2;
			this.ctlCancel.Text = "Cancel";
			// 
			// DataGridFilterData
			// 
			this.DataGridFilterData.AllowChangeColumnMapping = false;
			this.DataGridFilterData.AutoAdjust = true;
			this.DataGridFilterData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DataGridFilterData.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.DataGridFilterData.CaptionVisible = false;
			this.DataGridFilterData.Columns.AddRange(new mControl.GridStyle.GridColumnStyle[] {
																								  this.ColumnName,
																								  this.FilterType,
																								  this.FilterText});
			this.DataGridFilterData.DataMember = "";
			this.DataGridFilterData.Dock = System.Windows.Forms.DockStyle.Fill;
			//-this.DataGridFilterData.DockPadding.All = 1;
			this.DataGridFilterData.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.DataGridFilterData.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.DataGridFilterData.Location = new System.Drawing.Point(0, 40);
			this.DataGridFilterData.MappingName = "FilterData";
			this.DataGridFilterData.Name = "DataGridFilterData";
			this.DataGridFilterData.RowHeadersVisible = false;
			this.DataGridFilterData.ShowPopUpMenu = false;
			this.DataGridFilterData.Size = new System.Drawing.Size(362, 162);
			this.DataGridFilterData.StylePainter = this.StyleGuideBase;
			this.DataGridFilterData.TabIndex = 24;
			// 
			// ColumnName
			// 
			this.ColumnName.FormatType = mControl.Util.Formats.None;
			this.ColumnName.HeaderText = "ColumnName";
			this.ColumnName.MappingName = "ColumnName";
			this.ColumnName.NullText = "(null)";
			this.ColumnName.ReadOnly = true;
			this.ColumnName.SumMode = mControl.GridStyle.AggregateMode.None;
			this.ColumnName.Width = 145;
			// 
			// FilterType
			// 
			this.FilterType.HeaderText = "FilterType";
			this.FilterType.MappingName = "FilterType";
			this.FilterType.NullText = "(null)";
			this.FilterType.Width = 55;
			// 
			// FilterText
			// 
			this.FilterText.FormatType = mControl.Util.Formats.None;
			this.FilterText.HeaderText = "Filter";
			this.FilterText.MappingName = "FilterText";
			this.FilterText.NullText = "(null)";
			this.FilterText.SumMode = mControl.GridStyle.AggregateMode.None;
			this.FilterText.Width = 144;
			this.FilterText.CellValidating += new mControl.GridStyle.CellValidatingEventHandler(this.FilterText_CellValidating);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// GridFilterDlg
			// 
			this.AcceptButton = this.ButtonSelect;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.ctlCancel;
			this.CaptionHeight = 40;
			this.CaptionText = "Grid Filter";
			this.ClientSize = new System.Drawing.Size(362, 234);
			this.Controls.Add(this.DataGridFilterData);
			this.Controls.Add(this.panel2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GridFilterDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Grid Filter";
			this.Load += new System.EventHandler(this.DataFilterForm_Load);
			this.Controls.SetChildIndex(this.panel2, 0);
			this.Controls.SetChildIndex(this.DataGridFilterData, 0);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DataGridFilterData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private Grid grid;
		//private DataColumnCollection  TableColumnCollection;


		public static void ShowFilter(Grid g)
		{
			GridFilterDlg f=new GridFilterDlg(g);
			f.SetStyleLayout(g.CtlStyleLayout.Layout);

			f.SetSourceColumns(g.DataList.Table.Columns);
			f.ShowDialog();
		}

		private void DataFilterForm_Load(object sender, System.EventArgs e)
		{
			this.FilterType.Items.AddRange(this.Operations);
			this.DataGridFilterData.Init(dataSource,"","FilterData");
		}


		public void SetSourceColumns (DataColumnCollection Columns)
		{

			DataRow FilterRow;
			//DataRow OperationRow;
			try
			{

//				OperationDataTable =new DataTable("OperationDataTable");
//				DataColumn CloumnOperation = new DataColumn("ColumnOperation",System.Type.GetType("System.String"));
//				OperationDataTable.Columns.Add(CloumnOperation);
//			
//                 
//
//				foreach (string Oper in Operations)
//				{
//                  OperationRow = OperationDataTable.NewRow();
//				  OperationRow["ColumnOperation"] = Oper;
//				  OperationDataTable.Rows.Add(OperationRow);
//				}
//				 

				FilterDataTable = new DataTable("FilterData");
			
				DataColumn CloumnName = new DataColumn("ColumnName",System.Type.GetType("System.String"));
				DataColumn ColumnOperation = new DataColumn("FilterType",System.Type.GetType("System.String"));
				DataColumn ColumnFilterData = new DataColumn("FilterText",System.Type.GetType("System.String"));
				
				FilterDataTable.Columns.Add(CloumnName);
				FilterDataTable.Columns.Add(ColumnOperation);				
				FilterDataTable.Columns.Add(ColumnFilterData);
				

				foreach (DataColumn col in Columns)
				{
					FilterRow = FilterDataTable.NewRow();
					FilterRow["ColumnName"] = col.ColumnName.ToString();
					FilterRow["FilterType"] = "None";
					FilterRow["FilterText"] = "";
					FilterDataTable.Rows.Add(FilterRow);

				}
			
				dataSource= FilterDataTable.DefaultView;
				dataSource.AllowDelete=false;
				dataSource.AllowNew=false;

			}

			catch (System.Exception a_Ex)
			{
				MessageBox.Show(a_Ex.Message);
			}
		}

		public DataTable GetFilterDataTable()
		{
			return FilterDataTable;
		}

		private void ButtonSelect_Click(object sender, System.EventArgs e)
		{
		SetTableByDataFilter();
		}

		private void SetTableByDataFilter()
		{
         

			try
			{
				string filter=string.Empty;
				// Build the RowFilter statement according to the user restriction 
				foreach (DataRow FilterRow in FilterDataTable.Rows)
				{
				
					if (FilterRow["FilterType"].ToString() != string.Empty && FilterRow["FilterType"].ToString() != "None" && FilterRow["FilterText"].ToString() != string.Empty)
					{
						// Add the "AND" operator only from the second filter condition 
						// The RowFilter get statement which simallar to the Where condition in sql query
						// For example "GroupID = '6' AND GroupName LIKE 'A%' 
						if (filter == string.Empty)
						{
							filter = FilterRow["ColumnName"].ToString() + " " + FilterRow["FilterType"].ToString() + " '" + FilterRow["FilterText"].ToString()+"' ";
						}
					
						else
						{
							filter += " AND " + FilterRow["ColumnName"].ToString()+" " + FilterRow["FilterType"].ToString() +" '"+ FilterRow["FilterText"].ToString()+"'";
						}
					
					}
				}

				if(filter!=string.Empty)
				{
//					DataView ViewRecords =  grid.DataList;
//					ViewRecords.RowFilter = filter;
//					grid.SetDataBinding(ViewRecords,"");
					grid.SetFilter(filter);
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void FilterText_CellValidating(object sender, CellValidatingEventArgs e)
		{
			if(e.Value!=null && this.DataGridFilterData[e.Row,"FilterType"].ToString().Equals("None"))
			{
                MsgBox.ShowError("Invalid Filter type"); 
				e.Cancel=true;
			}
		}

		private void FilterType_CellValidated(object sender, EventArgs e)
		{
			int row=DataGridFilterData.CurrentRowIndex;
			if(DataGridFilterData[row,"FilterType"].ToString().Equals("None"))
			{
				this.DataGridFilterData[row,"FilterText"]="";
			}
		}

		private void FilterType_CellValidating(object sender, CellValidatingEventArgs e)
		{
			if(e.Value.ToString().Equals("None"))
			{
				this.DataGridFilterData[e.Row,"FilterText"]="";
			}

		}

		private void ctlRemove_Click(object sender, System.EventArgs e)
		{
			grid.RemoveFilter();
			this.Close();
		}
	}
}
