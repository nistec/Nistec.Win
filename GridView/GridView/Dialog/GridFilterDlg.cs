using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Threading;
using Extension.Nistec.Threading;
using Nistec.Data;
using System.Collections.Generic;
using Nistec.Win;

namespace Nistec.GridView
{
	/// <summary>
	/// Summary description for DataFilter.
	/// </summary>
	public class GridFilterDlg : Nistec.WinForms.McForm
	{


		private DataView  dataSource;
		private DataTable FilterDataTable;
		
		// Add new oprrations according to the operation which support by the database you connect
        private string[] Operations = { "None", "<", "<=", ">", ">=", "=", "<>", "LIKE", "*=", "=*" };
        private Nistec.GridView.Grid GridFilterData;
		private Nistec.GridView.GridTextColumn FilterText;
        private Nistec.GridView.GridTextColumn ColumnType;
        private System.Windows.Forms.ImageList imageList1;
        private Nistec.GridView.GridComboColumn FilterType;
        private Nistec.WinForms.McSpinner spinner;
        private GridLabelColumn ColumnName;
        private Nistec.WinForms.McToolBar toolBar;
        private Nistec.WinForms.McMultiBox textFilter;
        private Nistec.WinForms.McToolButton tbOk;
        private Nistec.WinForms.McToolButton tbRemoveFilter;
        private Nistec.WinForms.McToolButton tbClose;
		private System.ComponentModel.IContainer components;

        /// <summary>
        /// Initilaized GridFilterDlg
        /// </summary>
		public GridFilterDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            this.KeyPreview = true;
		}
        /// <summary>
        /// Initilaized GridFilterDlg
        /// </summary>
        /// <param name="g"></param>
		public GridFilterDlg(Grid g):this()
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridFilterDlg));
            this.spinner = new Nistec.WinForms.McSpinner();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolBar = new Nistec.WinForms.McToolBar();
            this.textFilter = new Nistec.WinForms.McMultiBox();
            this.tbOk = new Nistec.WinForms.McToolButton();
            this.tbRemoveFilter = new Nistec.WinForms.McToolButton();
            this.tbClose = new Nistec.WinForms.McToolButton();
            this.GridFilterData = new Nistec.GridView.Grid();
            this.ColumnName = new Nistec.GridView.GridLabelColumn();
            this.FilterType = new Nistec.GridView.GridComboColumn();
            this.FilterText = new Nistec.GridView.GridTextColumn();
            this.ColumnType = new Nistec.GridView.GridTextColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spinner)).BeginInit();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridFilterData)).BeginInit();
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
            // spinner
            // 
            this.spinner.BackColor = System.Drawing.Color.Transparent;
            this.spinner.ForeColor = System.Drawing.SystemColors.ControlText;
            this.spinner.Image = ((System.Drawing.Image)(resources.GetObject("spinner.Image")));
            this.spinner.Location = new System.Drawing.Point(84, 10);
            this.spinner.Name = "spinner";
            this.spinner.ReadOnly = false;
            this.spinner.Size = new System.Drawing.Size(35, 18);
            this.spinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.spinner.TabIndex = 4;
            this.spinner.TabStop = false;
            this.spinner.Text = "label2";
            this.spinner.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // toolBar
            // 
            this.toolBar.BackColor = System.Drawing.Color.Silver;
            this.toolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.toolBar.Controls.Add(this.spinner);
            this.toolBar.Controls.Add(this.textFilter);
            this.toolBar.Controls.Add(this.tbOk);
            this.toolBar.Controls.Add(this.tbRemoveFilter);
            this.toolBar.Controls.Add(this.tbClose);
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBar.FixSize = false;
            this.toolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.toolBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBar.Location = new System.Drawing.Point(2, 38);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.toolBar.Size = new System.Drawing.Size(369, 35);
            this.toolBar.StylePainter = this.StyleGuideBase;
            this.toolBar.TabIndex = 28;
            this.toolBar.ButtonClick += new Nistec.WinForms.ToolButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // textFilter
            // 
            this.textFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textFilter.ButtonToolTip = "Custom";
            this.textFilter.Location = new System.Drawing.Point(125, 10);
            this.textFilter.MultiType = Nistec.WinForms.MultiType.Custom;
            this.textFilter.Name = "textFilter";
            this.textFilter.Size = new System.Drawing.Size(238, 20);
            this.textFilter.StylePainter = this.StyleGuideBase;
            this.textFilter.TabIndex = 5;
            this.textFilter.Text = "Filter Text";
            this.textFilter.ButtonClick += new Nistec.WinForms.ButtonClickEventHandler(this.textFilter_ButtonClick);
            // 
            // tbOk
            // 
            this.tbOk.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbOk.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbOk.Image = global::Nistec.GridView.Properties.Resources.filter2;
            this.tbOk.Location = new System.Drawing.Point(56, 3);
            this.tbOk.Name = "tbOk";
            this.tbOk.Size = new System.Drawing.Size(22, 29);
            this.tbOk.TabIndex = 3;
            this.tbOk.Tag = "ok";
            this.tbOk.ToolTipText = "Execute Filter";
            // 
            // tbRemoveFilter
            // 
            this.tbRemoveFilter.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbRemoveFilter.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbRemoveFilter.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbRemoveFilter.Image = global::Nistec.GridView.Properties.Resources.filterRemove;
            this.tbRemoveFilter.Location = new System.Drawing.Point(34, 3);
            this.tbRemoveFilter.Name = "tbRemoveFilter";
            this.tbRemoveFilter.Size = new System.Drawing.Size(22, 29);
            this.tbRemoveFilter.TabIndex = 2;
            this.tbRemoveFilter.Tag = "remove";
            this.tbRemoveFilter.ToolTipText = "Remove Filter";
            // 
            // tbClose
            // 
            this.tbClose.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbClose.Image = global::Nistec.GridView.Properties.Resources.logoff_small;
            this.tbClose.Location = new System.Drawing.Point(12, 3);
            this.tbClose.Name = "tbClose";
            this.tbClose.Size = new System.Drawing.Size(22, 29);
            this.tbClose.TabIndex = 0;
            this.tbClose.Tag = "exit";
            this.tbClose.ToolTipText = "Exit";
            // 
            // GridFilterData
            // 
            this.GridFilterData.AllowColumnContextMenu = false;
            this.GridFilterData.AllowGridContextMenu = false;
            this.GridFilterData.BackColor = System.Drawing.Color.White;
            this.GridFilterData.BackgroundColor = System.Drawing.Color.White;
            this.GridFilterData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridFilterData.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.GridFilterData.CaptionVisible = false;
            this.GridFilterData.Columns.AddRange(new Nistec.GridView.GridColumnStyle[] {
            this.ColumnName,
            this.FilterType,
            this.FilterText,
            this.ColumnType});
            this.GridFilterData.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
            this.GridFilterData.DataMember = "";
            this.GridFilterData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridFilterData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.GridFilterData.ForeColor = System.Drawing.Color.Black;
            this.GridFilterData.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.GridFilterData.Location = new System.Drawing.Point(2, 73);
            this.GridFilterData.MappingName = "FilterData";
            this.GridFilterData.Name = "GridFilterData";
            this.GridFilterData.RowHeadersVisible = false;
            this.GridFilterData.Size = new System.Drawing.Size(369, 235);
            this.GridFilterData.StylePainter = this.StyleGuideBase;
            this.GridFilterData.TabIndex = 24;
            this.GridFilterData.CurrentRowChanging += new System.ComponentModel.CancelEventHandler(this.GridFilterData_CurrentRowChanging);
            // 
            // ColumnName
            // 
            this.ColumnName.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ColumnName.BackColor = System.Drawing.Color.DarkGray;
            this.ColumnName.Format = "";
            this.ColumnName.HeaderText = "ColumnName";
            this.ColumnName.MappingName = "ColumnName";
            this.ColumnName.Text = "";
            this.ColumnName.Width = 145;
            // 
            // FilterType
            // 
            this.FilterType.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.FilterType.AllowNull = false;
            this.FilterType.Format = "";
            this.FilterType.HeaderText = "FilterType";
            this.FilterType.MappingName = "FilterType";
            this.FilterType.Width = 59;
            // 
            // FilterText
            // 
            this.FilterText.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.FilterText.Format = "";
            this.FilterText.HeaderText = "Filter";
            this.FilterText.MappingName = "FilterText";
            this.FilterText.Width = 140;
            this.FilterText.CellKeyPress += new System.Windows.Forms.KeyEventHandler(this.FilterText_CellKeyPress);
            // 
            // ColumnType
            // 
            this.ColumnType.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ColumnType.Format = "";
            this.ColumnType.HeaderText = "ColType";
            this.ColumnType.MappingName = "ColType";
            this.ColumnType.ReadOnly = true;
            this.ColumnType.Visible = false;
            this.ColumnType.Width = 0;
            // 
            // GridFilterDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(373, 310);
            this.Controls.Add(this.GridFilterData);
            this.Controls.Add(this.toolBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridFilterDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grid Filter";
            this.Load += new System.EventHandler(this.DataFilterForm_Load);
            this.Controls.SetChildIndex(this.toolBar, 0);
            this.Controls.SetChildIndex(this.GridFilterData, 0);
            ((System.ComponentModel.ISupportInitialize)(this.spinner)).EndInit();
            this.toolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridFilterData)).EndInit();
            this.ResumeLayout(false);

		}


   
 
 		#endregion

		private Grid grid;
		private string  currentFilter;
        /// <summary>
        /// Show Filter
        /// </summary>
        /// <param name="g"></param>
		public static void ShowFilter(Grid g)
		{
			GridFilterDlg f=new GridFilterDlg(g);
			//f.SetStyleLayout(g.LayoutManager.Layout);
			f.GridFilterData.SetStyleLayout(g.LayoutManager.Layout);
			f.SetSource(g.DataList.Table.Columns);
			f.ShowDialog();
		}

		private void DataFilterForm_Load(object sender, System.EventArgs e)
		{
			this.FilterType.Items.AddRange(this.Operations);
            this.Caption.SubText = "Current Filter: " + grid.RowFilter;
			this.GridFilterData.Init(dataSource,"","FilterData");
            //this.FilterType.EditBox.SelectedIndexChanged+=new EventHandler(EditBox_SelectedIndexChanged);

		}
        /// <summary>
        /// Set Source
        /// </summary>
        /// <param name="Columns"></param>
        public void SetSource(DataColumnCollection Columns)
        {

            FilterDataTable = DataUtil.CreateTableSchema("FilterData", new string[] { "ColumnName", "FilterType", "FilterText", "ColType" });
            //Dictionary<string, object>[] rows = new Dictionary<string, object>[Columns.Count];
            //int index = 0;
            DataRow Row;
		
            foreach (DataColumn col in Columns)
            {
                Row = FilterDataTable.NewRow();
                Row["ColumnName"] = col.ColumnName.ToString();
                Row["FilterType"] = "None";
                Row["FilterText"] = "";
                Row["ColType"] = col.DataType.Name;
                FilterDataTable.Rows.Add(Row);

             }

            //DataUtil.FillDataTable(dt, rows);

            dataSource = FilterDataTable.DefaultView;
            dataSource.AllowDelete = false;
            dataSource.AllowNew = false;

        }

        /// <summary>
        /// Get Filter DataTable
        /// </summary>
        /// <returns></returns>
		public DataTable GetFilterDataTable()
		{
			return FilterDataTable;
		}

        private void RemoveFilter()
        {
            grid.RemoveFilter();
            this.Close();

        }

        private void RunFilter()
        {
                SetTableByDataFilter();
        }

 		private void SetTableByDataFilter()
		{

            try
            {
                currentFilter = grid.RowFilter;
                string filter = currentFilter;// string.Empty;
                string prefix = string.Empty;
                string sufix = string.Empty;
                string filterType = string.Empty;
                string filterText = string.Empty;
                string colName = string.Empty;
                string colType = string.Empty;
                string op = string.Empty;

                // Build the RowFilter statement according to the user restriction 
                foreach (DataRow FilterRow in FilterDataTable.Rows)
                {
                     
                    //filter += GridPerform.GetValidFilter(filter, FilterRow["ColumnName"].ToString(), FilterRow["FilterText"].ToString(), FilterRow["FilterType"].ToString());
                    op = "AND";
                    filterType = FilterRow["FilterType"].ToString();
                    filterText = FilterRow["FilterText"].ToString();
                    colName = FilterRow["ColumnName"].ToString();
                    colType = FilterRow["ColType"].ToString();

                    Type type = Type.GetType("System." + colType);
                    FieldType coltypes = WinHelp.DataTypeOf(type);
             
           
                    if (filterText.StartsWith("|"))
                    {
                        filterText=filterText.Replace("|","");
                        op = "OR";
                    }
                    if (filterType != string.Empty && filterType != "None" && filterText != string.Empty)
                    {
                        // Add the "AND" operator only from the second filter condition 
                        // The RowFilter get statement which simallar to the Where condition in sql query
                        // For example "GroupID = '6' AND GroupName LIKE 'A%' 

                        prefix = string.Empty;
                        sufix = string.Empty;

                        if (filterType.ToUpper() == "LIKE")
                        {
                            prefix = "'%";
                            sufix = "%'";
                            filterType = "LIKE";
                        }
                        else if (filterType.ToUpper() == "*=")
                        {
                            prefix = "'%";
                            sufix = "'";
                            filterType = "LIKE";
                        }
                        else if (filterType.ToUpper() == "=*")
                        {
                            prefix = "'";
                            sufix = "%'";
                            filterType = "LIKE";
                        }
                        else if (coltypes == FieldType.Number || coltypes == FieldType.Bool)
                        {
                            //do nothing
                        }
                        else //if (!Info.IsNumber(filterText))//.Info.IsNumber(FilterRow["FilterText"].ToString()))
                        {
                            prefix = "'";
                            sufix = "'";
                        }
                         if (filter == string.Empty)
                        {
                            filter = string.Format("{0} {1} {2}{3}{4}", colName, filterType, prefix, filterText, sufix);
                            //filter = FilterRow["ColumnName"].ToString() + " " + FilterRow["FilterType"].ToString() + " '" + FilterRow["FilterText"].ToString()+"' ";
                        }

                        else
                        {
                            filter += string.Format(" {0} ({1} {2} {3}{4}{5})",op, colName, filterType, prefix, filterText, sufix);
                            //filter += " AND " + FilterRow["ColumnName"].ToString()+" " + FilterRow["FilterType"].ToString() +" '"+ FilterRow["FilterText"].ToString()+"'";
                        }

                    }
                }
                RunFilter(filter);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

        private void RunFilter(string filter)
        {
            if (filter != string.Empty)
            {

                currentFilter = filter;

                if (grid.RowCount > 10000)
                {
                    this.spinner.ShowSpinner();
                    base.AsyncBeginInvoke(filter);
                }
                else
                {
                    this.spinner.ShowSpinner();
                    grid.FilterChanged += new EventHandler(grid_FilterChanged);
                    grid.ErrorOcurred += new ErrorOcurredEventHandler(grid_ErrorOcurred); 
                    grid.SetFilter(filter);
                }
            }
        }

        void grid_ErrorOcurred(object sender, ErrorOcurredEventArgs e)
        {
            MsgBox.ShowError(e.Message);
            this.Close();
        }


        //private DataView ViewRecords;
        /// <summary>
        /// On Async Executing Worker started
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAsyncExecutingWorker(AsyncCallEventArgs e)
        {
            try
            {
                base.OnAsyncExecutingWorker(e);
                //ViewRecords = grid.DataList;
                //ViewRecords.RowFilter = (string)e.Result;
                grid.SetFilter((string)e.Result);
            }
            catch(Exception ex)
            {
                base.AsyncCancelExecution();
                MsgBox.ShowError(ex.Message);
            }
        }

        void grid_FilterChanged(object sender, EventArgs e)
        {
           this.Close();
        }
        /// <summary>
        /// On Async executing Completed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAsyncCompleted(AsyncCallEventArgs e)
        {
            base.OnAsyncCompleted(e);
            //grid.SetDataBinding(ViewRecords);
            this.Close();
        }

        void GridFilterData_CurrentRowChanging(object sender, CancelEventArgs e)
        {
          bool ok=  RowValidateing(GridFilterData.currentRow,FilterType.Text, FilterText.Text);
          e.Cancel = !ok;
        }

 
        void FilterText_CellKeyPress(object sender, KeyEventArgs e)
        {
            if (char.IsLetterOrDigit( Strings.Chr(e.KeyValue)) &&  this.FilterType.Text.Equals("None"))
            {
                this.GridFilterData["FilterType"] = "=";
            }
        }
 

         private bool RowValidateing(int row,string filterType, string value)
        {
            //this.Text="row:" +row.ToString()+" filterType:"+filterType+" value:"+value;
            if (string.IsNullOrEmpty(value) && filterType.Equals("None"))
            {
                return true;
            }

            if (string.IsNullOrEmpty(value) && !filterType.Equals("None"))
            {
                this.FilterType.Text = "None";
                return true;
            }
            else if (!string.IsNullOrEmpty(value) && filterType.Equals("None"))
            {
                this.FilterType.Text = "=";
            }

            object colType = this.GridFilterData[row, "ColType"];

            Type type = Type.GetType("System." + colType.ToString());
            FieldType coltypes = WinHelp.DataTypeOf(type);
            FieldType valtypes = WinHelp.DataTypeOf(value);

            if (coltypes != valtypes && coltypes != FieldType.Text)
            {
                this.FilterText.Text = "";
                this.FilterType.Text = "None";
                MsgBox.ShowWarning("Incorrect Data Type");
                return false;
            }
            return true;
        }

        void EditBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row = GridFilterData.CurrentRowIndex;
            if (FilterType.EditBox.Text.Equals("None"))
            {
                this.GridFilterData[row, "FilterText"] = "";
            }
            else
            {
                GridFilterData.GoToCell(GridFilterData.CurrentRowIndex, 2);
            }
        }

	
        /// <summary>
        /// On Style Property Changed
        /// </summary>
        /// <param name="e"></param>
		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged (e);
			if(e.PropertyName=="StylePlan")
			{
				this.GridFilterData.SetStyleLayout(this.StyleGuideBase.Layout);
			}
		}
        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                RunFilter();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// ProcessDialogKey
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                RunFilter();
            }
            return base.ProcessDialogKey(keyData);
        }

        void textFilter_ButtonClick(object sender, Nistec.WinForms.ButtonClickEventArgs e)
        {
            RunFilter(this.textFilter.Text);
        }

        private void toolBar_ButtonClick(object sender, Nistec.WinForms.ToolButtonClickEventArgs e)
        {
            switch (e.Button.Tag.ToString())
            {
                case "exit":
                    this.Close();
                    break;
                case "remove":
                    RemoveFilter();
                    break;
                case "ok":
                    RunFilter();
                    break;
 
            }
        }
	}
}
