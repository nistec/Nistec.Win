using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MControl.WinForms;

namespace ChartsSample
{
    public partial class FormMain : McForm
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void mcTaskPanel1_ItemClicked(object sender, LinkClickEvent e)
        {
            this.panelControl.Controls.Clear();
            switch (e.Name)
            {
                case "Bar":
                    //MControl.Charts.Win.McGraph grp = new MControl.Charts.Win.McGraph();
                    //this.panelControl.Controls.Add(grp);
                    //MControl.Charts.GraphHelper gu = new MControl.Charts.GraphHelper(grp);
                    //gu.CreateDataSource(MControl.Data.Aggregate.Sum, DataSource.Select("Invoices"), "Invoices", new string[] { "CustomerID" }, "ExtendedPrice", "ExtendedPrice", "");
                    //gu.CreateDataSource(DataSource.Select("SalesByCategory"), "SalesByCategory", "ProductSales");
                    //gu.CreateGraph(MControl.Charts.ChartType.Bars, "CategoryName", "SalesByCategory", "", "");
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.Bars));
                    break;
                case "Bar 3D":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.Bars3D));
                    break;
                case "Bar Multi3D":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.BarsMulti3D));
                    break;
                case "Line":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.Line));
                    break;
                case "Pipes":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.Pipes));
                    break;
                case "Line Multi3D":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.LineMulti3D));
                    break;
                case "Surface":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.Surface));
                    break;
                case "Surface 3D":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.Surface3D));
                    break;
                case "Surface Multi3D":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.SurfaceMulti3D));
                    break;
                case "Pie":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.Pie));
                    break;
                case "Pie 3D":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.Pie3D));
                    break;
                case "Pie Expanded":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.GraphControl(MControl.Charts.ChartType.PieExpanded3D));
                    break;
            }
        }

        private void mcTaskPanel2_ItemClicked(object sender, LinkClickEvent e)
        {
            this.panelControl.Controls.Clear();
            switch (e.Name)
            {
                case "Pie Chart":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.PieChart());
                    break;
                case "Pie Chart Display":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.PieChartDisplay());
                    break;
            }
        }

        private void mcTaskPanel3_ItemClicked(object sender, LinkClickEvent e)
        {
            this.panelControl.Controls.Clear();
            switch (e.Name)
            {
                case "Meter":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.MeterControl());
                    break;
                case "Meter with Range":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.MeterRangeControl());
                    break;
                case "Led":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.LedControl());
                    break;
                case "Usage":
                    this.panelControl.Controls.Add(new ChartsSample.Controls.UsageControl());
                    break;
            }
        }
    }
}