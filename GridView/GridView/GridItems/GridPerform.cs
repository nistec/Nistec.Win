using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

using Nistec.Data;
using System.Drawing;
using System.Collections;
using Nistec.Printing;
using Nistec.Data.Factory;
using Nistec.Data.Advanced;
using Nistec.Win;
using Nistec.Printing.Data;

namespace Nistec.GridView
{
    /// <summary>
    /// Represent Grid perform methods
    /// </summary>
    public class GridPerform
    {
        //Grid grid;
        //public GridPerform(Grid g)
        //{
        //    grid = g;
        //}
        #region Perform

        /// <summary>
        /// Perform SummarizeColumns
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="groupByColumn"></param>
        /// <param name="sumColumn"></param>
        /// <returns></returns>
        public static GridField[] SummarizeColumns(DataTable dt, string groupByColumn, string sumColumn)
        {
            if (dt == null || dt.Rows.Count == 0)
                return null;
            GridFieldCollection vcols = new GridFieldCollection();

            foreach (DataRow dr in dt.Rows)
            {
                vcols.Add(new GridField(dr[groupByColumn].ToString(), dr[sumColumn]));

            }

            if (vcols.Count == 0)
                return null;
            return vcols.GetFieldsArray();
        }


        /// <summary>
        /// Perform ChartAdd
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="caption"></param>
        /// <param name="tableName"></param>
        /// <param name="mode"></param>
        /// <param name="groupByField"></param>
        /// <param name="sumField"></param>
        /// <param name="aliasSumField"></param>
        /// <param name="filter"></param>
        public static void ChartAdd(Grid grid, string caption, string tableName, Aggregate mode, string groupByField, string sumField, string aliasSumField, string filter)
        {
            if (grid == null)
            {
                return;
            }
            DataTable dt = GroupByHelper.DoSelectGroupByInto(mode, grid.DataList.Table, tableName, groupByField, sumField, aliasSumField, filter);
            if (dt == null) return;
            if (dt.Rows.Count > 500)
            {
                MsgBox.ShowWarning("Too meny items.");
                return;
            }

            ChartAdd(grid, dt, caption, groupByField, aliasSumField);

        }
        /// <summary>
        /// Perform ChartAdd
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="dt"></param>
        /// <param name="caption"></param>
        /// <param name="groupByField"></param>
        /// <param name="aliasSumField"></param>
        public static void ChartAdd(Grid grid, DataTable dt, string caption, string groupByField, string aliasSumField)
        {
            if (dt == null) return;
            if (dt.Rows.Count > 500)
            {
                MsgBox.ShowWarning("Too meny items.");
                return;
            }
            GridChart ctlPieChart = new Nistec.GridView.GridChart(grid);

            //int ofst = ctlPieChart.CurrentOffset;
            //ofst = ofst == 30 ? 0 : 30;
            //ctlPieChart.SetOffset(ofst);
            ctlPieChart.Caption = caption;
            //ctlPieChart.ShowPanelColors = !ctlPieChart.ShowPanelColors;

            ctlPieChart.SetChart(dt, groupByField, aliasSumField);// "sumOf"+sumField);

            ctlPieChart.ShowPanelColors = true;
            //grid.PerformChartAdd(ctlPieChart);
            grid.Controls.Add(ctlPieChart);
            grid.Invalidate();

        }

        //public static void ChartRemove(Grid grid)
        //{
        //    grid.PerformChartRemove();
        //}
        /// <summary>
        /// Perform Columns Header Settings
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="headers"></param>
        public static void ColumnsHeaderSettings(Grid grid, string[] headers)
        {
            GridColumnCollection cols = grid.GridColumns;

            if (cols == null || cols.Count == 0)
                return;
            int headerCount = headers.Length;

            for (int i = 0; i < cols.Count; i++)
            {
                GridColumnStyle style = cols[i];
                if (style.Visible && i < headerCount)
                    style.HeaderText = headers[i];
            }
        }
        /// <summary>
        /// Perform Add Columns range Settings
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="columns"></param>
        public static void ColumnsSettings(Grid grid, Dictionary<string, string> columns)
        {
            int colCount = columns.Count;
            grid.GridColumns.Clear();
            GridColumnStyle[] cols = new GridColumnStyle[colCount];
            int i = 0;
            foreach (KeyValuePair<string, string> col in columns)
            {
                cols[i] = ColumnCreate(col.Key, col.Value);
                i++;
            }
            grid.GridColumns.AddRange(cols);
        }
        /// <summary>
        /// Perform Column Create
        /// </summary>
        /// <param name="mappingName"></param>
        /// <param name="headerText"></param>
        /// <returns></returns>
        public static GridColumnStyle ColumnCreate(string mappingName, string headerText)
        {
            return ColumnCreate(GridColumnType.TextColumn, mappingName, headerText, false);
        }
        /// <summary>
        /// Perform Column Create
        /// </summary>
        /// <param name="type"></param>
        /// <param name="mappingName"></param>
        /// <param name="headerText"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public static GridColumnStyle ColumnCreate(GridColumnType type, string mappingName, string headerText, bool readOnly)
        {

            GridColumnStyle col = null;

            switch (type)
            {
                case GridColumnType.BoolColumn:
                    col = new GridBoolColumn();
                    break;
                case GridColumnType.ButtonColumn:
                    col = new GridButtonColumn();
                    break;
                case GridColumnType.ComboColumn:
                    col = new GridComboColumn();
                    break;
                case GridColumnType.DateTimeColumn:
                    col = new GridDateColumn();
                    break;
                case GridColumnType.GridColumn:
                    col = new GridControlColumn();
                    break;
                case GridColumnType.IconColumn:
                    col = new GridIconColumn();
                    break;
                case GridColumnType.LabelColumn:
                    col = new GridLabelColumn();
                    break;
                case GridColumnType.MemoColumn:
                    col = new GridMemoColumn();
                    break;
                case GridColumnType.MultiColumn:
                    col = new GridMultiColumn();
                    break;
                case GridColumnType.NumericColumn:
                    col = new GridNumericColumn();
                    break;
                case GridColumnType.ProgressColumn:
                    col = new GridProgressColumn();
                    break;
                case GridColumnType.TextColumn:
                    col = new GridTextColumn();
                    break;
                case GridColumnType.VGridColumn:
                    col = new VGridColumn();
                    break;
                default:
                    col = new GridTextColumn();
                    break;
            }
            col.MappingName = mappingName;
            col.HeaderText = headerText;
            col.ReadOnly = readOnly;
            return col;
        }

        /// <summary>
        /// Perform Add Columns range
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="columns"></param>
        public static void ColumnsSettings(Grid grid, GridColumnStyle[] columns)
        {
            grid.GridColumns.Clear();
            grid.GridColumns.AddRange(columns);

        }

        /// <summary>
        /// Perform Show Columns Filter
        /// </summary>
        /// <param name="grid"></param>
        public static void ColumnsFilter(Grid grid)
        {
            if (grid.RowCount > 0)
                GridColumnFilterDlg.ShowColumns(grid);
        }
        /// <summary>
        /// Perform Print
        /// </summary>
        /// <param name="grid"></param>
        public static void Print(Grid grid)
        {
            if (grid.RowCount > 0)
            {
                //PrintGridDataView.Print(grid);
                DataView dv = grid.DataList;
                McColumn[] cols = CreateColumns(grid);
                ReportBuilder.PrintDataView(dv, grid.CaptionText, ReportBuilder.ConvertRtlToAlignment(grid.RightToLeft), cols, true);

            }
        }

        internal static McColumn[] CreateColumns(Grid grid)
        {
            GridColumnStyle[] gcs = grid.GetBoundsColumns();
            McColumn[] ctl = new McColumn[gcs.Length];
            int i = 0;
            foreach (GridColumnStyle c in gcs)
            {
                McColumn col = new McColumn(c.MappingName, c.HeaderText, c.Width, c.DataType);
                col.Display = c.Visible;
                ctl[i] = col;
                i++;
            }
            return ctl;
        }

        internal static AdoField[] CreateExportColumns(Grid grid)
        {
            DataView dv = grid.DataList;
            if (dv == null)
                return null;

            DataTable dt = dv.Table;

            GridColumnStyle[] gcs = grid.GetVisibleColumns();
            AdoField[] ctl = new AdoField[gcs.Length];

            int i = 0;
            foreach (GridColumnStyle c in gcs)
            {
                //ExportField col = new ExportField();
                //col.Caption=c.HeaderText;
                //col.ColumnOrdinal=dt.Columns[c.MappingName].Ordinal;
                ctl[i] = new AdoField(c.MappingName, c.HeaderText, dt.Columns[c.MappingName].Ordinal);
                i++;
            }
            return ctl;
        }

        /// <summary>
        /// Perform Show Grid Find
        /// </summary>
        /// <param name="grid"></param>
        public static void Find(Grid grid)
        {
            if (grid.RowCount > 0)
                FindDlg.Open(grid);
        }
        /// <summary>
        /// Perform Show Current Grid Row
        /// </summary>
        /// <param name="grid"></param>
        public static void CurrentRow(Grid grid)
        {
            DataRowView drv = grid.GetCurrentDataRow();
            if (drv != null)
            {
                VGridDlg gd = new VGridDlg(drv, grid.MappingName);
                gd.VGrid.SetColumnStylesToFields(grid.GridColumns);
                gd.SetStyleLayout(grid.LayoutManager.Layout);
                gd.VGrid.ControlLayout = grid.ControlLayout;
                gd.Show();
            }
        }
        /// <summary>
        /// Perform Show grid Chart dialog
        /// </summary>
        /// <param name="grid"></param>
        public static void Chart(Grid grid)
        {
            if (grid.RowCount > 0)
                GridChartDlg.Open(grid, grid.CaptionText);
        }

        /// <summary>
        /// Perform Show grid Summarize
        /// </summary>
        /// <param name="grid"></param>
        public static void Summarize(Grid grid)
        {
            if (!grid.StatusBarVisible)
            {
                grid.StatusBarVisible = true;
            }
            grid.StatusBar.InitilaizeColumns = true;
            grid.StatusBar.SummarizeColumns();
        }

        /// <summary>
        /// Perform Show grid Summarize
        /// </summary>
        /// <param name="grid"></param>
        public static void SummarizeDlg(Grid grid)
        {
            GridField[] fields = SummarizeColumns(grid);
            if (fields == null || fields.Length == 0)
            {
                MsgBox.ShowInfo("No columns with numeric data type was found");
                return;
            }
            VGridDlg gd = new VGridDlg();
            gd.Caption.Text = "Summarize Grid";
            gd.SelectObject(fields, grid.MappingName);
            gd.SetStyleLayout(grid.LayoutManager.Layout);
            gd.VGrid.ControlLayout = grid.ControlLayout;
            gd.VGrid.ReadOnly = true;
            gd.Show();
        }
        /// <summary>
        /// Perform Show Filter dialog
        /// </summary>
        /// <param name="grid"></param>
        public static void Filter(Grid grid)
        {
            if (grid.ListManager == null) return;
            GridFilterDlg.ShowFilter(grid);
        }
        /// <summary>
        /// Perform Remove filter
        /// </summary>
        /// <param name="grid"></param>
        public static void FilterRemove(Grid grid)
        {
            grid.RemoveFilter();
        }
        /// <summary>
        /// Perform Compute Grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="mode"></param>
        /// <param name="column"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public double Compute(Grid grid, AggregateMode mode, string column, double defaultValue)
        {
            object o = Compute(grid, mode.ToString() + "(" + column + ")", grid.RowFilter);
            return Types.ToDouble(o, defaultValue);
        }
        /// <summary>
        /// Perform compute grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="mode"></param>
        /// <param name="column"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public decimal Compute(Grid grid, AggregateMode mode, string column, decimal defaultValue)
        {
            object o = Compute(grid, mode.ToString() + "(" + column + ")", grid.RowFilter);
            return Types.ToDecimal(o, defaultValue);
        }
        /// <summary>
        /// Perform compute grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="mode"></param>
        /// <param name="column"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int Compute(Grid grid, AggregateMode mode, string column, int defaultValue)
        {
            object o = Compute(grid, mode.ToString() + "(" + column + ")", grid.RowFilter);
            return Types.ToInt(o, defaultValue);
        }

        /// <summary>
        /// Perform compute grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="mode"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static object Compute(Grid grid, AggregateMode mode, string column)
        {
            return Compute(grid, mode.ToString() + "(" + column + ")", grid.RowFilter);
        }
        /// <summary>
        /// Perform compute grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="mode"></param>
        /// <param name="column"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static object Compute(Grid grid, AggregateMode mode, string column, string filter)
        {
            return Compute(grid, mode.ToString() + "(" + column + ")", filter);
        }
        /// <summary>
        /// Perform compute grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="expression"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static object Compute(Grid grid, string expression, string filter)
        {
            try
            {
                DataView dv = grid.DataList;
                if (dv == null) return null;
                return dv.Table.Compute(expression, filter);
            }
            catch (Exception)
            {
                //OnErrorOccouerd("Compute: " + ex.Message);
                return "";
            }
        }
        /// <summary>
        /// Perform Adjust columns
        /// </summary>
        /// <param name="grid"></param>
        public static void AdjustColumns(Grid grid)
        {
            grid.AdjustColumns();
            grid.Invalidate();
            /*bound*/
            //if (!DesignMode && grid.myGridTable.GridColumnStyles.Count > 0)
            //{
            //    AdjustColumns(false, true);
            //    grid.Invalidate();
            //}
        }
        /// <summary>
        /// Perform Export data
        /// </summary>
        /// <param name="grid"></param>
        public static void Export(Grid grid)
        {

            DataView dv = grid.DataList;
            if (dv == null) return;

            System.Data.DataTable dtExport = dv.Table.Copy();
            try
            {
                AdoField[] exColumns = GridPerform.CreateExportColumns(grid);
                AdoExport.Export(dtExport,false, exColumns);


            }
            catch
            {
                AdoExport.Export(dtExport,false);
            }
        }
        /// <summary>
        /// Perform export data
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="state"></param>
        public static void Export(Grid grid, string filter, string sort, DataViewRowState state)
        {
            DataView dv = grid.DataList;
            if (dv == null) return;

            System.Data.DataTable dtExport = Nistec.Data.DataUtil.GetFilteredData(dv, filter, sort, state);
            try
            {
                AdoField[] exColumns = GridPerform.CreateExportColumns(grid);
                AdoExport.Export(dtExport,false, exColumns);
            }
            catch
            {
                AdoExport.Export(dtExport,false);
            }
        }
        /// <summary>
        /// Perform export filtred
        /// </summary>
        /// <param name="grid"></param>
        public static void ExportFiltred(Grid grid)
        {
            DataView dv = grid.DataList;
            if (dv == null) return;

            System.Data.DataTable dtExport = Nistec.Data.DataUtil.GetFilteredData(dv.Table, dv.RowFilter, dv.Sort, dv.RowStateFilter);
            try
            {
                AdoField[] exColumns = GridPerform.CreateExportColumns(grid);
                AdoExport.Export(dtExport,false, exColumns);
            }
            catch
            {
                AdoExport.Export(dtExport,false);
            }
        }
        /// <summary>
        /// Perform Import data to grid
        /// </summary>
        /// <param name="grid"></param>
        public static void Import(Grid grid)
        {
            try
            {
                grid.DataSource = AdoImport.Import();// ImportXml();
                //if (dt == null)// || ds.Tables.Count == 0)
                //    return;
                //if (ds.Tables.Count == 1)
                //{
                //    grid.DataSource = ds.Tables[0];
                //}
                //else
                //{
                //    grid.DataSource = ds;
                //    grid.DataMember = ds.Tables[0].TableName;
                //}
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
            }
        }


        #endregion

        #region Update changes
        /// <summary>
        /// Perform get indicating the grid has changes
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static bool HasChanges(Grid grid)
        {
            DataView dv = grid.DataList;
            if (dv == null) return false;
            return dv.Table.DataSet.HasChanges();
        }
        /// <summary>
        /// Perform Update changes
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="connectionString"></param>
        /// <param name="provider"></param>
        /// <param name="dbTableName"></param>
        /// <returns></returns>
        public static int UpdateChanges(Grid grid, string connectionString, Data.DBProvider provider, string dbTableName)
        {
            IDbCmd cmd = DbFactory.Create(connectionString, provider);
            return UpdateChanges(grid, cmd, dbTableName);
        }

        /// <summary>
        /// Perform Update changes
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="conn"></param>
        /// <param name="dbTableName"></param>
        /// <returns></returns>
        public static int UpdateChanges(Grid grid, System.Data.IDbConnection conn, string dbTableName)
        {
            IDbCmd cmd = DbFactory.Create(conn);
            return UpdateChanges(grid, cmd, dbTableName);
        }
        /// <summary>
        /// Perform Update changes
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="cmd"></param>
        /// <param name="dbTableName"></param>
        /// <returns></returns>
        public static int UpdateChanges(Grid grid, IDbCmd cmd, string dbTableName)
        {
            if (!grid.Dirty) return 0;
            try
            {
                DataView dv = grid.DataList;
                if (dv == null) return -1;
                grid.EndEdit();

                //Data.IDbCmd cmd = Nistec.Data.DBUtil.Create(conn);
                int res = cmd.Adapter.UpdateChanges(dv.Table, dbTableName);

                if (res > 0)
                {
                    dv.Table.AcceptChanges();
                }
                //dirty=false;
                grid.OnDirty(Grid.GridDirtyRowState.Completed);// (false);
                return res;
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// Perform summarize columns
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static GridField[] SummarizeColumns(Grid grid)
        {
            if (grid.RowCount == 0)
                return null;
            GridFieldCollection vcols = new GridFieldCollection();
            GridColumnCollection gridCols = grid.GridColumns;
            foreach (GridColumnStyle c in gridCols)
            {
                if (c.DataType == FieldType.Number)
                {
                    object o = Compute(grid, AggregateMode.Sum, c.MappingName, grid.RowFilter);
                    GridField field = new GridField(c.MappingName, o);
                    vcols.Add(field);
                }
            }
            if (vcols.Count == 0)
                return null;
            return vcols.GetFieldsArray();
        }


        /// <summary>
        /// Perform summarize columns
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static GridField[] SummarizeColumns(Grid grid, string[] fields)
        {
            if (grid.RowCount == 0)
                return null;
            GridFieldCollection vcols = new GridFieldCollection();
            GridColumnCollection gridCols = grid.GridColumns;
            int i = 0;
            int fieldsCount = fields.Length;

            foreach (GridColumnStyle c in gridCols)
            {
                if (i >= fieldsCount)
                    break;

                if (c.MappingName == fields[i])
                {
                    object o = Compute(grid, AggregateMode.Sum, c.MappingName, grid.RowFilter);
                    GridField field = new GridField(c.MappingName, o);
                    vcols.Add(field);
                    i++;
                }
            }
            if (vcols.Count == 0)
                return null;
            return vcols.GetFieldsArray();
        }

        #region Create columns

        ///// <summary>
        ///// Perform create columns
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="mappingNames"></param>
        //public static void CreateColumns(Grid grid,string[] mappingNames)
        //{
        //    CreateColumns(grid,mappingNames, mappingNames);
        //}
        ///// <summary>
        ///// Perform create columns
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="mappingNames"></param>
        ///// <param name="HeaderNames"></param>
        //public static void CreateColumns(Grid grid,string[] mappingNames, string[] HeaderNames)
        //{
        //    if (mappingNames == null || HeaderNames == null)
        //    {
        //        throw new ArgumentException("Invalid mappingNames or  HeaderNames ");
        //    }
        //    if (mappingNames.Length == 0 || HeaderNames.Length == 0)
        //    {
        //        throw new ArgumentException("Invalid mappingNames or  HeaderNames ");
        //    }
        //    grid.Columns.Clear();
        //    GridTextColumn gc = null;

        //    for (int i = 0; i < mappingNames.Length; i++)
        //    {
        //        gc = new GridTextColumn();
        //        gc.MappingName = mappingNames[i];
        //        gc.HeaderText = HeaderNames[i];
        //        gc.Width = Grid.DefaultColumnWidth;
        //        grid.Columns.Add(gc);
        //    }
        //}

        /// <summary>
        /// Perform create columns
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="types"></param>
        /// <param name="mappingNames"></param>
        /// <param name="headerNames"></param>
        /// <param name="width"></param>
        public static void CreateColumns(Grid grid, GridColumnType[] types, string[] mappingNames, string[] headerNames, int[] width)
        {
            if (mappingNames == null)
            {
                throw new ArgumentException("Invalid mappingNames");
            }

            int count = mappingNames.Length;

            if (types != null && types.Length != count)
            {
                throw new ArgumentException("Length of types not equals mappingNames Length");
            }
            if (headerNames != null && headerNames.Length != count || width.Length != count)
            {
                throw new ArgumentException("Length of headerNames not equals mappingNames Length");
            }
            if (width != null && width.Length != count)
            {
                throw new ArgumentException("Length of width not equals mappingNames Length");
            }

            GridColumnStyle[] columns = new GridColumnStyle[count];

            for (int i = 0; i < count; i++)
            {
                columns[i] = CreateColumns((types == null) ? GridColumnType.TextColumn : types[i], NativeMethods.ColumntTypeToDataType(types[i]), mappingNames[i], (headerNames == null) ? mappingNames[i] : headerNames[i], (width == null) ? Grid.DefaultColumnWidth : width[i]);
            }
            grid.Columns.Clear();
            grid.Columns.AddRange(columns);

        }
        /// <summary>
        /// Perform create columns
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="types"></param>
        /// <param name="mappingNames"></param>
        /// <param name="headerNames"></param>
        public static void CreateColumns(Grid grid, GridColumnType[] types, string[] mappingNames, string[] headerNames)
        {
            CreateColumns(grid, types, mappingNames, headerNames, null);
        }
        /// <summary>
        /// Perform create columns
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="mappingNames"></param>
        /// <param name="headerNames"></param>
        public static void CreateColumns(Grid grid, string[] mappingNames, string[] headerNames)
        {
            CreateColumns(grid, null, mappingNames, headerNames, null);
        }

        /// <summary>
        /// Perform create columns
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="types"></param>
        /// <param name="mappingNames"></param>
        public static void CreateColumns(Grid grid, GridColumnType[] types, string[] mappingNames)
        {
            CreateColumns(grid, types, mappingNames, null, null);
        }
        /// <summary>
        /// Perform create columns
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="mappingNames"></param>
        public static void CreateColumns(Grid grid, string[] mappingNames)
        {
            CreateColumns(grid, null, mappingNames, null, null);
        }
        /// <summary>
        /// Perform create column
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataType"></param>
        /// <param name="mappingName"></param>
        /// <param name="header"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static GridColumnStyle CreateColumns(GridColumnType type, FieldType dataType, string mappingName, string header, int width)
        {
            GridColumnStyle column = null;
            switch (type)
            {
                case GridColumnType.TextColumn:
                    column = new GridTextColumn();
                    break;
                case GridColumnType.ComboColumn:
                    column = new GridComboColumn();
                    break;
                case GridColumnType.DateTimeColumn:
                    column = new GridDateColumn();
                    break;
                case GridColumnType.LabelColumn:
                    column = new GridLabelColumn();
                    break;
                case GridColumnType.ButtonColumn:
                    column = new GridButtonColumn();
                    break;
                case GridColumnType.ProgressColumn:
                    column = new GridProgressColumn();
                    break;
                case GridColumnType.BoolColumn:
                    column = new GridBoolColumn();
                    break;
                case GridColumnType.IconColumn:
                    column = new GridIconColumn();
                    break;
                case GridColumnType.MultiColumn:
                    column = new GridMultiColumn();
                    break;
                case GridColumnType.NumericColumn:
                    column = new GridNumericColumn();
                    break;
                case GridColumnType.VGridColumn:
                    column = new VGridColumn();
                    break;
                case GridColumnType.GridColumn:
                    column = new GridControlColumn();
                    break;
                case GridColumnType.MemoColumn:
                    column = new GridMemoColumn();
                    break;

            }
            column.m_DataType = dataType;
            column.MappingName = mappingName;
            column.HeaderText = header;
            column.Width = width;
            return column;
        }
        #endregion

        /// <summary>
        /// Perform Get Valid Filter
        /// </summary>
        /// <param name="currentFilter"></param>
        /// <param name="column"></param>
        /// <param name="text"></param>
        /// <param name="Operator"></param>
        /// <returns></returns>
        public static string GetValidFilter(string currentFilter, string column, string text, string Operator)
        {
            if (text == string.Empty || Operator == String.Empty || column == String.Empty)
                return text;

            string filter = string.Empty;
            string prefix = string.Empty;
            string sufix = string.Empty;
            string filterType = Operator;

            if (Operator.ToUpper() == "LIKE")
            {
                prefix = "'%";
                sufix = "%'";
                filterType = "LIKE";
            }
            else if (Operator.ToUpper() == "*=")
            {
                prefix = "'%";
                sufix = "'";
                filterType = "LIKE";
            }
            else if (Operator.ToUpper() == "=*")
            {
                prefix = "'";
                sufix = "%'";
                filterType = "LIKE";
            }
            else if (!WinHelp.IsNumber(text))//.Info.IsNumber(text))
            {
                prefix = "'";
                sufix = "'";
            }

            if (currentFilter == string.Empty)
            {
                filter = string.Format("{0} {1} {2}{3}{4}", column, filterType, prefix, text, sufix);
            }
            else
            {
                filter = currentFilter + string.Format(" AND {0} {1} {2}{3}{4}", column, filterType, prefix, text, sufix);
            }
            return filter;
        }

        /// <summary>
        /// Auto Size Column Widths
        /// </summary>
        /// <param name="grid"></param>
        public static void AutoSizeColumnWidths(Grid grid)
        {
            AutoSizeColumnWidths(grid,50);
        }
        /// <summary>
        /// Auto Size Column Widths
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="minimumWidth"></param>
        public static void AutoSizeColumnWidths(Grid grid, int minimumWidth)
        {
            if ((grid != null) && (grid.DataList !=null))
            {
                Graphics graphics = grid.CreateGraphics();
                try
                {
                    DataTable dataSource = grid.DataList.Table;
                    int num = 0;
                    foreach (DataColumn column in dataSource.Columns)
                    {
                        string text = "";
                        foreach (DataRow row in dataSource.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted)
                            {
                                continue;
                            }
                            string str2 = row[column].ToString().Trim();
                            if (str2.IndexOf(Environment.NewLine) > -1)
                            {
                                string[] strArray = str2.Split(Environment.NewLine.ToCharArray());
                                int length = strArray.Length;
                                foreach (string str3 in strArray)
                                {
                                    str2 = str3.Trim();
                                    if (str2.Length > text.Length)
                                    {
                                        text = str2;
                                    }
                                }
                                continue;
                            }
                            if (str2.Length > text.Length)
                            {
                                text = str2;
                            }
                        }
                        Size size = graphics.MeasureString(text, grid.Font).ToSize();
                        Size size2 = graphics.MeasureString(column.ColumnName, grid.HeaderFont).ToSize();
                        Size size3 = (size.Width > size2.Width) ? size : size2;
                        if (size3.Width > minimumWidth)
                        {
                            grid.Columns[num].Width = size3.Width + 10;
                        }
                        else
                        {
                            grid.Columns[num].Width = minimumWidth;
                        }
                        num++;
                    }
                }
                catch
                {
                }
                finally
                {
                    graphics.Dispose();
                }
            }
        }
        /// <summary>
        /// Auto Size Row Heights
        /// </summary>
        /// <param name="grid"></param>
        public static void AutoSizeRowHeights(Grid grid)
        {
            int count = ((DataTable)grid.DataSource).Rows.Count;
            int num2 = ((DataTable)grid.DataSource).Columns.Count;
            Graphics graphics = Graphics.FromHwnd(grid.Handle);
            StringFormat format = new StringFormat(StringFormat.GenericTypographic);
            SizeF ef = graphics.MeasureString("ABC", grid.Font, 400, format);
            //Array array = (Array)grid.GetType().GetMethod("get_GridRows", BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreCase).Invoke(grid, null);
            
            ArrayList list = new ArrayList();
            foreach (GridRow gr in grid.GridRows)
            {
                if (gr is GridRelationshipRow)
                {
                    list.Add(gr);
                }
            }
            //foreach (object obj2 in array)
            //{
            //    if (obj2.ToString().EndsWith("GridRelationshipRow"))
            //    {
            //        list.Add(obj2);
            //    }
            //}
            for (int i = 0; i < count; i++)
            {
                int length = 1;
                for (int j = 0; j < num2; j++)
                {
                    string str = grid[i, j].ToString();
                    if (str.IndexOf(Environment.NewLine) > -1)
                    {
                        length = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Length;
                    }
                }
                int num6 = Convert.ToInt32(ef.Height) * length;
                num6 += 8;
                list[i].GetType().GetProperty("Height").SetValue(list[i], num6, null);
                grid.Invalidate();
            }
            graphics.Dispose();
        }

    }
}
