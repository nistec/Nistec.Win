using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MControl.Charts;

namespace ChartsSample.Controls
{
    public partial class GraphControl : UserControl
    {
        public GraphControl()
        {
            InitializeComponent();
        }

        public GraphControl(ChartType type):this()
        {
            this.mcGraph1.ChartType = type;
            switch(type)
            {
                case ChartType.Bars:
                case ChartType.Line:
                case ChartType.Surface:
                case ChartType.Pie:
                    Top10Orders();
                    break;
                case ChartType.Bars3D:
                case ChartType.Pipes:
                case ChartType.Surface3D:
                case ChartType.Pie3D:
                    SalesByCategory();
                    break;
                case ChartType.BarsMulti3D:
                case ChartType.LineMulti3D:
                case ChartType.SurfaceMulti3D:
                case ChartType.PieExpanded3D:
                    EmployeeSalesByQuarter();
                    break;
            }
        }

        private void Top10Orders()
        {
            this.mcGraph1.DataItems.Clear();
            this.mcGraph1.DataSource = DataSource.Select("Top10Orders");
            this.mcGraph1.DataItems.Add(new MControl.Charts.DataItem("SaleAmount"));
            this.mcGraph1.FieldLabel = "CompanyName";
            this.mcGraph1.GraphTitle = "Top 10 Orders";
            this.mcGraph1.AxisLabelY = "Amount";
            this.mcGraph1.AxisLabelX = "Company Name";
            mcGraph1.DrawChart();

        }
        private void SalesByCategory()
        {
            this.mcGraph1.DataItems.Clear();
            this.mcGraph1.DataSource = DataSource.Select("SalesByCategory");
            this.mcGraph1.DataItems.Add(new MControl.Charts.DataItem("ProductSales"));
            this.mcGraph1.FieldID = "CategoryID";
            this.mcGraph1.FieldLabel = "CategoryName";
            this.mcGraph1.GraphTitle = "Sales By Category";
            this.mcGraph1.AxisLabelY = "Product Sales";
            this.mcGraph1.AxisLabelX = "Category";
            mcGraph1.DrawChart();

        }

        private void EmployeeSalesByQuarter()
        {
            this.mcGraph1.DataItems.Clear();
            this.mcGraph1.DataSource = DataSource.Select("EmployeeSalesByQuarter");
            this.mcGraph1.DataItems.Add(new MControl.Charts.DataItem("Quarter1"));
            this.mcGraph1.DataItems.Add(new MControl.Charts.DataItem("Quarter2"));
            this.mcGraph1.DataItems.Add(new MControl.Charts.DataItem("Quarter3"));
            this.mcGraph1.DataItems.Add(new MControl.Charts.DataItem("Quarter4"));
            this.mcGraph1.KeyItems.Clear();
            this.mcGraph1.KeyItems.Add(new MControl.Charts.KeyItem("Ouarter1"));
            this.mcGraph1.KeyItems.Add(new MControl.Charts.KeyItem("Ouarter2"));
            this.mcGraph1.KeyItems.Add(new MControl.Charts.KeyItem("Ouarter3"));
            this.mcGraph1.KeyItems.Add(new MControl.Charts.KeyItem("Ouarter4"));

            this.mcGraph1.ColorItems.Clear();
            this.mcGraph1.ColorItems.Add(new MControl.Charts.ColorItem(Color.Red));
            this.mcGraph1.ColorItems.Add(new MControl.Charts.ColorItem(Color.Blue));
            this.mcGraph1.ColorItems.Add(new MControl.Charts.ColorItem(Color.Gold));
            this.mcGraph1.ColorItems.Add(new MControl.Charts.ColorItem(Color.Green));
            this.mcGraph1.ColorItems.Add(new MControl.Charts.ColorItem(Color.Violet));

            this.mcGraph1.FieldID = "EmployeeID";
            this.mcGraph1.FieldLabel = "LastName";
            this.mcGraph1.GraphTitle = "Employee Sales By Quarter";
            this.mcGraph1.AxisLabelY = "Amount";
            this.mcGraph1.AxisLabelX = "Employee";
            mcGraph1.DrawChart();

        }

        private void SalesByYears()
        {
            this.mcGraph1.DataItems.Clear();
            this.mcGraph1.DataSource = DataSource.Select("SalesByYears");
            this.mcGraph1.DataItems.Add(new MControl.Charts.DataItem("Total1996"));
            this.mcGraph1.DataItems.Add(new MControl.Charts.DataItem("Total1997"));
            this.mcGraph1.DataItems.Add(new MControl.Charts.DataItem("Total1998"));
            this.mcGraph1.KeyItems.Clear();
            this.mcGraph1.KeyItems.Add(new MControl.Charts.KeyItem("Total1996"));
            this.mcGraph1.KeyItems.Add(new MControl.Charts.KeyItem("Total1997"));
            this.mcGraph1.KeyItems.Add(new MControl.Charts.KeyItem("Total1998"));

            this.mcGraph1.ColorItems.Clear();
            this.mcGraph1.ColorItems.Add(new MControl.Charts.ColorItem(Color.Red));
            this.mcGraph1.ColorItems.Add(new MControl.Charts.ColorItem(Color.Blue));
            this.mcGraph1.ColorItems.Add(new MControl.Charts.ColorItem(Color.Green));
            this.mcGraph1.ColorItems.Add(new MControl.Charts.ColorItem(Color.Gold));

            this.mcGraph1.FieldID = "MonthNumber";
            this.mcGraph1.FieldLabel = "Month";
            this.mcGraph1.GraphTitle = "Sales By Years";
            this.mcGraph1.AxisLabelY = "Amount";
            this.mcGraph1.AxisLabelX = "Month";
            mcGraph1.DrawChart();

        }

    }
}
