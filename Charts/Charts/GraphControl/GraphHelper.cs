using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Nistec.Charts;
using Nistec.Charts.Win;
using Nistec.Data;
using Nistec.Data.Advanced;
using Nistec.Win;

namespace Nistec.Charts
{
        //sample
        //Nistec.Charts.Win.McGraph grp = new Nistec.Charts.Win.McGraph();
        //this.panelControl.Controls.Add(grp);
        //Nistec.Charts.GraphHelper gu = new Nistec.Charts.GraphHelper(grp);
        //gu.CreateDataSource(Nistec.Data.Aggregate.Sum, DataSource.Select("Invoices"), "Invoices", new string[] { "CustomerID" }, "ExtendedPrice", "ExtendedPrice", "");
        //gu.CreateGraph(Nistec.Charts.ChartType.Bars, "CustomerID", "Invoices", "", "");

    /// <summary>
    /// GraphUtil
    /// </summary>
    public class GraphHelper
    {
        public readonly McGraph graph;

        public GraphHelper(McGraph g)
        {
            if (g == null)
            {
                g = new McGraph();
            }
            graph = g;
        }

        public void CreateDataSource(Aggregate mode, DataTable dataSource, string tableName, string[] groupByField, string sumField, string aliasSumField, string filter)
        {
            DataTable chartTable = GroupByHelper.DoSelectGroupByInto(mode, dataSource, tableName, groupByField, new string[] { sumField }, new string[] { aliasSumField }, filter);
            CreateDataSource(chartTable, tableName, sumField);
        }

        public void CreateDataSource(DataTable chartTable, string tableName, string sumField)
        {
            this.graph.DataItems.Clear();
            this.graph.DataSource = null;
            if (chartTable == null) return;
            if (chartTable.Rows.Count > 500)
            {
                MsgBox.ShowWarning("Too meny items.");
                return;
            }
            this.graph.DataItems.Add(new Nistec.Charts.DataItem(sumField));
            this.graph.DataSource = chartTable;
        }
        
        public void CreateGraph(ChartType type, string fieldLabel, string graphTitle, string axisLabelX, string axisLabelY)
        {
            if (this.graph.DataSource == null)
            {
                MsgBox.ShowWarning("Invalid data source.");
                return;
            }

            //McGraph graph = new McGraph();
            this.graph.ChartType = type;
            //this.graph.DataItems.Clear();
            //this.graph.DataSource = chartTable;
            //this.graph.DataItems.Add(new Nistec.Charts.DataItem(sumField));
            this.graph.FieldLabel = fieldLabel;
            this.graph.GraphTitle = graphTitle;
            this.graph.AxisLabelY = axisLabelX;
            this.graph.AxisLabelX = axisLabelY;
            graph.DrawChart();

        }

  
        //private void EmployeeSalesByQuarter()
        //{
        //    this.graph.DataItems.Clear();
        //    this.graph.DataSource = DataSource.Select("EmployeeSalesByQuarter");
        //    this.graph.DataItems.Add(new Nistec.Charts.DataItem("Quarter1"));
        //    this.graph.DataItems.Add(new Nistec.Charts.DataItem("Quarter2"));
        //    this.graph.DataItems.Add(new Nistec.Charts.DataItem("Quarter3"));
        //    this.graph.DataItems.Add(new Nistec.Charts.DataItem("Quarter4"));
        //    this.graph.KeyItems.Clear();
        //    this.graph.KeyItems.Add(new Nistec.Charts.KeyItem("Ouarter1"));
        //    this.graph.KeyItems.Add(new Nistec.Charts.KeyItem("Ouarter2"));
        //    this.graph.KeyItems.Add(new Nistec.Charts.KeyItem("Ouarter3"));
        //    this.graph.KeyItems.Add(new Nistec.Charts.KeyItem("Ouarter4"));

        //    this.graph.ColorItems.Clear();
        //    this.graph.ColorItems.Add(new Nistec.Charts.ColorItem(Color.Red));
        //    this.graph.ColorItems.Add(new Nistec.Charts.ColorItem(Color.Blue));
        //    this.graph.ColorItems.Add(new Nistec.Charts.ColorItem(Color.Gold));
        //    this.graph.ColorItems.Add(new Nistec.Charts.ColorItem(Color.Green));
        //    this.graph.ColorItems.Add(new Nistec.Charts.ColorItem(Color.Violet));

        //    this.graph.FieldID = "EmployeeID";
        //    this.graph.FieldLabel = "LastName";
        //    this.graph.GraphTitle = "Employee Sales By Quarter";
        //    this.graph.AxisLabelY = "Amount";
        //    this.graph.AxisLabelX = "Employee";
        //    graph.DrawChart();

        //}

        //private void SalesByYears()
        //{
        //    this.graph.DataItems.Clear();
        //    this.graph.DataSource = DataSource.Select("SalesByYears");
        //    this.graph.DataItems.Add(new Nistec.Charts.DataItem("Total1996"));
        //    this.graph.DataItems.Add(new Nistec.Charts.DataItem("Total1997"));
        //    this.graph.DataItems.Add(new Nistec.Charts.DataItem("Total1998"));
        //    this.graph.KeyItems.Clear();
        //    this.graph.KeyItems.Add(new Nistec.Charts.KeyItem("Total1996"));
        //    this.graph.KeyItems.Add(new Nistec.Charts.KeyItem("Total1997"));
        //    this.graph.KeyItems.Add(new Nistec.Charts.KeyItem("Total1998"));

        //    this.graph.ColorItems.Clear();
        //    this.graph.ColorItems.Add(new Nistec.Charts.ColorItem(Color.Red));
        //    this.graph.ColorItems.Add(new Nistec.Charts.ColorItem(Color.Blue));
        //    this.graph.ColorItems.Add(new Nistec.Charts.ColorItem(Color.Green));
        //    this.graph.ColorItems.Add(new Nistec.Charts.ColorItem(Color.Gold));

        //    this.graph.FieldID = "MonthNumber";
        //    this.graph.FieldLabel = "Month";
        //    this.graph.GraphTitle = "Sales By Years";
        //    this.graph.AxisLabelY = "Amount";
        //    this.graph.AxisLabelX = "Month";
        //    graph.DrawChart();

        //}

    }
}
