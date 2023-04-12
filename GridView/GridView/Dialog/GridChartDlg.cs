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
using System.Text;
using Nistec.Charts;
using Nistec.Data.Advanced;
using Nistec.Win;

namespace Nistec.GridView
{
	/// <summary>
	/// Summary description for DataChart.
	/// </summary>
	public class GridChartDlg : Nistec.WinForms.McForm
	{



        #region members


        // Add new oprrations according to the operation which support by the database you connect
        private Nistec.GridView.GridTextColumn ColumnType;
        private Nistec.GridView.GridComboColumn Aggrigate;
        private GridLabelColumn ColumnName;
        private GridBoolColumn GroupBy;
        private MenuItem mnPlay;
        private MenuItem mnPrint;
        private MenuItem mnOffset;
        private MenuItem mnColors;
        private Nistec.WinForms.McToolBar toolBar;
        private Nistec.WinForms.McTabControl tabControl1;
        private Nistec.WinForms.McTabPage ctlTabChart;
        private Grid GridChartData;
        private Nistec.WinForms.McTabPage ctlTabText;
        private GridChart ctlPieChart;
        private Nistec.WinForms.McToolButton tbClose;
        private Nistec.WinForms.McToolButton tbOk;
        private Nistec.WinForms.McToolButton tbEditor;
        private Nistec.WinForms.McToolButton tbChart;
        private Nistec.WinForms.McTextBox textChartCaption;
        private Nistec.WinForms.McToolButton tbSummarize;
        private Nistec.WinForms.McTabPage ctlTabGrid;
        private Grid GridSummarize;
		private System.ComponentModel.IContainer components=null;
        #endregion

        #region ctor

        /// <summary>
        /// Initilaizing Grid Chart dialog
        /// </summary>
        public GridChartDlg()
		{
			InitializeComponent();
		}
        /// <summary>
        /// Initilaizing Grid Chart dialog
        /// </summary>
        /// <param name="g"></param>
        public GridChartDlg(Grid g)
            : this()
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
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.mnPlay = new System.Windows.Forms.MenuItem();
            this.mnPrint = new System.Windows.Forms.MenuItem();
            this.mnOffset = new System.Windows.Forms.MenuItem();
            this.mnColors = new System.Windows.Forms.MenuItem();
            this.toolBar = new Nistec.WinForms.McToolBar();
            this.textChartCaption = new Nistec.WinForms.McTextBox();
            this.tbOk = new Nistec.WinForms.McToolButton();
            this.tbSummarize = new Nistec.WinForms.McToolButton();
            this.tbChart = new Nistec.WinForms.McToolButton();
            this.tbEditor = new Nistec.WinForms.McToolButton();
            this.tbClose = new Nistec.WinForms.McToolButton();
            this.tabControl1 = new Nistec.WinForms.McTabControl();
            this.ctlTabChart = new Nistec.WinForms.McTabPage();
            this.GridChartData = new Nistec.GridView.Grid();
            this.ColumnName = new Nistec.GridView.GridLabelColumn();
            this.GroupBy = new Nistec.GridView.GridBoolColumn();
            this.Aggrigate = new Nistec.GridView.GridComboColumn();
            this.ColumnType = new Nistec.GridView.GridTextColumn();
            this.ctlTabText = new Nistec.WinForms.McTabPage();
            this.ctlPieChart = new Nistec.GridView.GridChart();
            this.ctlTabGrid = new Nistec.WinForms.McTabPage();
            this.GridSummarize = new Nistec.GridView.Grid();
            this.toolBar.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ctlTabChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridChartData)).BeginInit();
            this.ctlTabText.SuspendLayout();
            this.ctlTabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridSummarize)).BeginInit();
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
            // mnPlay
            // 
            this.mnPlay.Index = -1;
            this.mnPlay.Text = "";
            // 
            // mnPrint
            // 
            this.mnPrint.Index = -1;
            this.mnPrint.Text = "";
            // 
            // mnOffset
            // 
            this.mnOffset.Index = -1;
            this.mnOffset.Text = "";
            // 
            // mnColors
            // 
            this.mnColors.Index = -1;
            this.mnColors.Text = "";
            // 
            // toolBar
            // 
            this.toolBar.BackColor = System.Drawing.Color.Silver;
            this.toolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.toolBar.Controls.Add(this.textChartCaption);
            this.toolBar.Controls.Add(this.tbOk);
            this.toolBar.Controls.Add(this.tbSummarize);
            this.toolBar.Controls.Add(this.tbChart);
            this.toolBar.Controls.Add(this.tbEditor);
            this.toolBar.Controls.Add(this.tbClose);
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBar.FixSize = false;
            this.toolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.toolBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBar.Location = new System.Drawing.Point(2, 38);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.toolBar.Size = new System.Drawing.Size(395, 35);
            this.toolBar.StylePainter = this.StyleGuideBase;
            this.toolBar.TabIndex = 27;
            this.toolBar.ButtonClick += new Nistec.WinForms.ToolButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // textChartCaption
            // 
            this.textChartCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textChartCaption.Location = new System.Drawing.Point(128, 10);
            this.textChartCaption.Name = "textChartCaption";
            this.textChartCaption.Size = new System.Drawing.Size(261, 20);
            this.textChartCaption.StylePainter = this.StyleGuideBase;
            this.textChartCaption.TabIndex = 5;
            this.textChartCaption.Text = "Chart Caption";
            // 
            // tbOk
            // 
            this.tbOk.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbOk.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbOk.Image = global::Nistec.GridView.Properties.Resources.grd73c1;
            this.tbOk.Location = new System.Drawing.Point(100, 3);
            this.tbOk.Name = "tbOk";
            this.tbOk.Size = new System.Drawing.Size(22, 29);
            this.tbOk.TabIndex = 3;
            this.tbOk.Tag = "ok";
            this.tbOk.ToolTipText = "Show chart In Grid";
            // 
            // tbSummarize
            // 
            this.tbSummarize.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbSummarize.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbSummarize.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbSummarize.Image = global::Nistec.GridView.Properties.Resources.sum;
            this.tbSummarize.Location = new System.Drawing.Point(78, 3);
            this.tbSummarize.Name = "tbSummarize";
            this.tbSummarize.Size = new System.Drawing.Size(22, 29);
            this.tbSummarize.TabIndex = 6;
            this.tbSummarize.Tag = "summarize";
            this.tbSummarize.ToolTipText = "Show Grid Summarize";
            // 
            // tbChart
            // 
            this.tbChart.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbChart.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbChart.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbChart.Image = global::Nistec.GridView.Properties.Resources.piecha1;
            this.tbChart.Location = new System.Drawing.Point(56, 3);
            this.tbChart.Name = "tbChart";
            this.tbChart.Size = new System.Drawing.Size(22, 29);
            this.tbChart.TabIndex = 1;
            this.tbChart.Tag = "chart";
            this.tbChart.ToolTipText = "Show Chart";
            // 
            // tbEditor
            // 
            this.tbEditor.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbEditor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbEditor.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbEditor.Image = global::Nistec.GridView.Properties.Resources.expression_obj;
            this.tbEditor.Location = new System.Drawing.Point(34, 3);
            this.tbEditor.Name = "tbEditor";
            this.tbEditor.Size = new System.Drawing.Size(22, 29);
            this.tbEditor.TabIndex = 2;
            this.tbEditor.Tag = "editor";
            this.tbEditor.ToolTipText = "Show Chart Editor";
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
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.tabControl1.ControlLayout = Nistec.WinForms.ControlLayout.Flat;
            this.tabControl1.Controls.Add(this.ctlTabChart);
            this.tabControl1.Controls.Add(this.ctlTabText);
            this.tabControl1.Controls.Add(this.ctlTabGrid);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HideTabs = true;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 22);
            this.tabControl1.Location = new System.Drawing.Point(2, 73);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Size = new System.Drawing.Size(395, 311);
            this.tabControl1.StylePainter = this.StyleGuideBase;
            this.tabControl1.TabIndex = 28;
            this.tabControl1.TabPages.AddRange(new Nistec.WinForms.McTabPage[] {
            this.ctlTabChart,
            this.ctlTabText,
            this.ctlTabGrid});
            this.tabControl1.TabStop = false;
            // 
            // ctlTabChart
            // 
            this.ctlTabChart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ctlTabChart.Controls.Add(this.GridChartData);
            this.ctlTabChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlTabChart.Location = new System.Drawing.Point(4, 4);
            this.ctlTabChart.Name = "ctlTabChart";
            this.ctlTabChart.Size = new System.Drawing.Size(388, 304);
            this.ctlTabChart.StylePainter = this.StyleGuideBase;
            this.ctlTabChart.Text = "Grid Columns";
            // 
            // GridChartData
            // 
            this.GridChartData.AllowColumnContextMenu = false;
            this.GridChartData.AllowGridContextMenu = false;
            this.GridChartData.AutoAdjust = true;
            this.GridChartData.BackColor = System.Drawing.Color.White;
            this.GridChartData.BackgroundColor = System.Drawing.Color.White;
            this.GridChartData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridChartData.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.GridChartData.CaptionVisible = false;
            this.GridChartData.Columns.AddRange(new Nistec.GridView.GridColumnStyle[] {
            this.ColumnName,
            this.GroupBy,
            this.Aggrigate,
            this.ColumnType});
            this.GridChartData.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
            this.GridChartData.DataMember = "";
            this.GridChartData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridChartData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.GridChartData.ForeColor = System.Drawing.Color.Black;
            this.GridChartData.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.GridChartData.Location = new System.Drawing.Point(0, 0);
            this.GridChartData.MappingName = "ChartData";
            this.GridChartData.Name = "GridChartData";
            this.GridChartData.RowHeadersVisible = false;
            this.GridChartData.Size = new System.Drawing.Size(388, 304);
            this.GridChartData.StylePainter = this.StyleGuideBase;
            this.GridChartData.TabIndex = 24;
            // 
            // ColumnName
            // 
            this.ColumnName.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ColumnName.AutoAdjust = true;
            this.ColumnName.BackColor = System.Drawing.Color.DarkGray;
            this.ColumnName.Format = "";
            this.ColumnName.HeaderText = "ColumnName";
            this.ColumnName.MappingName = "ColumnName";
            this.ColumnName.Text = "";
            this.ColumnName.Width = 216;
            // 
            // GroupBy
            // 
            this.GroupBy.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.GroupBy.HeaderText = "GroupBy";
            this.GroupBy.MappingName = "GroupBy";
            this.GroupBy.NullText = "False";
            //this.GroupBy.NullValue = "False";
            this.GroupBy.Text = "";
            this.GroupBy.Width = 80;
            // 
            // Aggrigate
            // 
            this.Aggrigate.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.Aggrigate.AllowNull = false;
            this.Aggrigate.Format = "";
            this.Aggrigate.HeaderText = "Aggrigate";
            this.Aggrigate.MappingName = "Aggrigate";
            this.Aggrigate.Width = 80;
            this.Aggrigate.CellEdit += new System.EventHandler(this.Aggrigate_CellEdit);
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
            // ctlTabText
            // 
            this.ctlTabText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ctlTabText.Controls.Add(this.ctlPieChart);
            this.ctlTabText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlTabText.Location = new System.Drawing.Point(4, 4);
            this.ctlTabText.Name = "ctlTabText";
            this.ctlTabText.Size = new System.Drawing.Size(388, 304);
            this.ctlTabText.StylePainter = this.StyleGuideBase;
            this.ctlTabText.Text = "Chart";
            // 
            // ctlPieChart
            // 
            this.ctlPieChart.BackColor = System.Drawing.Color.White;
            this.ctlPieChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ctlPieChart.Caption = "Grid Chart";
            this.ctlPieChart.DecimalPoint = 2;
            this.ctlPieChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPieChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlPieChart.ForeColor = System.Drawing.Color.Black;
            this.ctlPieChart.Location = new System.Drawing.Point(0, 0);
            this.ctlPieChart.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctlPieChart.Name = "ctlPieChart";
            this.ctlPieChart.ShowPanelColors = false;
            this.ctlPieChart.Size = new System.Drawing.Size(388, 304);
            this.ctlPieChart.TabIndex = 11;
            // 
            // ctlTabGrid
            // 
            this.ctlTabGrid.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ctlTabGrid.Controls.Add(this.GridSummarize);
            this.ctlTabGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlTabGrid.Location = new System.Drawing.Point(4, 4);
            this.ctlTabGrid.Name = "ctlTabGrid";
            this.ctlTabGrid.Size = new System.Drawing.Size(388, 304);
            this.ctlTabGrid.StylePainter = this.StyleGuideBase;
            this.ctlTabGrid.Text = "ctlTabGrid";
            // 
            // GridSummarize
            // 
            this.GridSummarize.AllowAdd = false;
            this.GridSummarize.AllowGridContextMenu = false;
            this.GridSummarize.AllowRemove = false;
            this.GridSummarize.AutoAdjust = true;
            this.GridSummarize.BackColor = System.Drawing.Color.White;
            this.GridSummarize.BackgroundColor = System.Drawing.Color.White;
            this.GridSummarize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridSummarize.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.GridSummarize.CaptionVisible = false;
            this.GridSummarize.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
            this.GridSummarize.DataMember = "";
            this.GridSummarize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridSummarize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.GridSummarize.ForeColor = System.Drawing.Color.Black;
            this.GridSummarize.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.GridSummarize.Location = new System.Drawing.Point(0, 0);
            this.GridSummarize.MappingName = "SummarizeData";
            this.GridSummarize.Name = "GridSummarize";
            this.GridSummarize.ReadOnly = true;
            this.GridSummarize.Size = new System.Drawing.Size(388, 304);
            this.GridSummarize.StylePainter = this.StyleGuideBase;
            this.GridSummarize.TabIndex = 25;
            // 
            // GridChartDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(399, 386);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridChartDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grid Chart";
            this.Controls.SetChildIndex(this.toolBar, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ctlTabChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridChartData)).EndInit();
            this.ctlTabText.ResumeLayout(false);
            this.ctlTabGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridSummarize)).EndInit();
            this.ResumeLayout(false);

		}

 
 
 		#endregion

		private Grid grid;
        private DataTable DataSource;
        private DataView dataSource;
        private string fields;
        private string groupField ;
        private string aliasField;
        private DataTable ChartTable;
        private string chartCaption;
        private string tableName;
     

        /// <summary>
        /// Set Source
        /// </summary>
        /// <param name="Columns"></param>
        public void SetSource(DataColumnCollection Columns)
        {

            //DataSource = GroupByHelper.DoCreateTableSchema("ChartData", new string[] { "ColumnName", "GroupBy", "Aggrigate", "ColType" });
            
            Dictionary<string,Type> columns=new Dictionary<string,Type>();
            columns.Add("ColumnName",typeof(string));
            columns.Add("GroupBy",typeof(bool));
            columns.Add("Aggrigate",typeof(string));
            columns.Add("ColType", typeof(string));

            DataSource = DataUtil.CreateTableSchema("ChartData", columns);
            DataRow Row;
		
            foreach (DataColumn col in Columns)
            {
                Row = DataSource.NewRow();
                Row["ColumnName"] = col.ColumnName.ToString();
                Row["GroupBy"] = false;
                Row["Aggrigate"] = "None";
                Row["ColType"] = col.DataType.Name;
                DataSource.Rows.Add(Row);
            }

            dataSource = DataSource.DefaultView;
            dataSource.AllowDelete = false;
            dataSource.AllowNew = false;

        }

        /// <summary>
        /// Get Chart DataTable
        /// </summary>
        /// <returns></returns>
		public DataTable GetChartDataTable()
		{
			return DataSource;
		}


        private void RunChart(string current)
        {

            try
            {
                fields = string.Empty;
                groupField = string.Empty;
                aliasField = string.Empty;
                ChartTable = null;

                StringBuilder sbFileds = new StringBuilder();
                StringBuilder sbGroup = new StringBuilder();
                StringBuilder sbAlias = new StringBuilder();
                string mode = "none";
                string alias = null;
                string colName = null;
                string colType = null;

                // Build the RowChart statement according to the user restriction 
                foreach (DataRow ChartRow in DataSource.Rows)
                {
                    colName = ChartRow["ColumnName"].ToString();
                    colType = ChartRow["ColType"].ToString();

                    if (ChartRow["GroupBy"].ToString().ToLower().Equals("true"))
                    {
                        sbFileds.AppendFormat("{0},", colName);
                        sbGroup.AppendFormat("{0},", colName);
                    }

                    mode = ChartRow["Aggrigate"].ToString();

                    if (!mode.Equals("None"))
                    {
                        alias = mode + "Of_" + colName;
                        sbAlias.AppendFormat("{0},", alias);
                        sbFileds.AppendFormat("{0}({1}) {2},", mode, colName, alias);
                    }

                }
                fields = sbFileds.ToString();
                groupField = sbGroup.ToString();
                aliasField=sbAlias.ToString();
                fields = fields.TrimEnd(',');
                groupField = groupField.TrimEnd(',');
                aliasField = aliasField.TrimEnd(',');
                chartCaption = this.textChartCaption.Text;
                tableName = grid.MappingName;
                tableName = tableName.Length == 0 ? "ChartData" : tableName;

                if (fields == string.Empty)
                return ;
            if (current == "chart")
            {
                ChartTable = GroupByHelper.DoSelectGroupByInto(tableName, grid.DataList.Table, fields, grid.RowFilter, groupField);
                this.ctlPieChart.SetChart(ChartTable, groupField, aliasField);
                ctlPieChart.Caption = chartCaption;
            }
            else if (current == "summarize")
            {
                ChartTable = GroupByHelper.DoSelectGroupByInto(tableName, grid.DataList.Table, fields, grid.RowFilter, groupField);
                GridSummarize.SetDataBinding(ChartTable, "");
                //this.Close();
            }
            else
            {
                if (grid.RowCount > 10000)
                {
                    //this.spinner.ShowSpinner();
                    base.AsyncBeginInvoke(null);
                }
                else
                {
                    //this.spinner.ShowSpinner();

                    ChartTable = GroupByHelper.DoSelectGroupByInto(tableName, grid.DataList.Table, fields, grid.RowFilter, groupField);
                    
                    GridPerform.ChartAdd(grid, ChartTable, chartCaption, groupField, aliasField);
                    this.Close();
                }
            }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //private DataView ViewRecords;
        /// <summary>
        /// Occurs when Async Executing Worker started
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAsyncExecutingWorker(AsyncCallEventArgs e)
        {
            try
            {
                base.OnAsyncExecutingWorker(e);
                ChartTable = GroupByHelper.DoSelectGroupByInto(tableName, grid.DataList.Table, fields, grid.RowFilter, groupField);
 

            }
            catch(Exception ex)
            {
                base.AsyncCancelExecution();
                MsgBox.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Occurs when Async execute is completed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAsyncCompleted(AsyncCallEventArgs e)
        {
            base.OnAsyncCompleted(e);
            GridPerform.ChartAdd(grid, ChartTable, chartCaption, groupField, aliasField);
            this.Close();
        }

     
        void Aggrigate_CellEdit(object sender, EventArgs e)
        {


            Type type = Type.GetType("System." + ColumnType.Text);

            FieldType coltypes = WinHelp.DataTypeOf(type);

            Aggrigate.Enabled = coltypes == FieldType.Number;
  
        }

 
		private void ctlRemove_Click(object sender, System.EventArgs e)
		{
			//grid.RemoveChart();
			this.Close();
		}
        /// <summary>
        /// Occurs when Style Property Changed
        /// </summary>
        /// <param name="e"></param>
		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged (e);
			if(e.PropertyName=="StylePlan")
			{
				this.GridChartData.SetStyleLayout(this.StyleGuideBase.Layout);
			}
		}

 
        #region Chart Dialsg

        internal static bool IsOpen = false;

        /// <summary>
        /// Open a Grid chart dialog
        /// </summary>
        /// <param name="g"></param>
        /// <param name="caption"></param>
        public static void Open(Grid g, string caption)
        {
            GridChartDlg f = new GridChartDlg(g);
            try
            {
                //f.grid = g;
                //f.SetStyleLayout(g.LayoutManager.Layout);
                //f.GridChartData.SetStyleLayout(g.LayoutManager.Layout);

                if (g.DataList == null)
                {
                    //DataTable dt = Nistec.Data.DataSourceConvertor.GetDataTable(g.DataSource, g.DataMember);
                    //if (dt != null)
                    //{
                    //    f.SetSource(dt.Columns);
                    //}
                    throw new Exception("Invalid Grid DataList");
                }
                f.SetSource(g.DataList.Table.Columns);
                f.Caption.SubText = caption;
                f.ShowChart(g);
                //f.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Raise the form load
        /// </summary>
        /// <param name="e"></param>
         protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.tbOk.Enabled = this.grid != null;
            GridChartDlg.IsOpen = true;
            this.ctlPieChart.ShowPanelColors = true; 
            this.Aggrigate.Items.AddRange(Enum.GetNames(typeof(Aggregate)));
            this.GridChartData.Init(dataSource, "", "ChartData");
        }
        /// <summary>
        /// Occurs when from is closed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            GridChartDlg.IsOpen = false;
        }
        /// <summary>
        /// Get the Pie Chart control
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public McPieChart PieChart
        {
            get { return this.ctlPieChart.ctlPieChart; }
        }
        /// <summary>
        /// Set Chart
        /// </summary>
        /// <param name="grid"></param>
        public void ShowChart(Grid grid)
        {
            if (grid == null)
            {
                return;
            }
            if (this.ctlPieChart.SetChart(grid))
            {
                this.ShowDialog();
            }
        }


        /// <summary>
        /// Get or Set the chart panels colors
        /// </summary>
        public bool ShowPanelColors
        {
            get { return this.ctlPieChart.ShowPanelColors; }
            set { this.ctlPieChart.ShowPanelColors = value; }
        }

        #endregion

        private void toolBar_ButtonClick(object sender, Nistec.WinForms.ToolButtonClickEventArgs e)
        {
            switch (e.Button.Tag.ToString())
            {
                case "exit":
                    this.Close();
                    break;
                case "chart":
                    this.tabControl1.SelectedIndex = 1;
                    RunChart("chart");
                    break;
                case "editor":
                    this.tabControl1.SelectedIndex = 0;
                    break;
                case "ok":
                    RunChart("grid");
                    break;
                case "summarize":
                    this.tabControl1.SelectedIndex = 2;
                    RunChart("summarize");
                    break;
  
            }
        }

    }
}
