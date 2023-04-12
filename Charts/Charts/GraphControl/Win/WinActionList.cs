namespace Nistec.Charts.Win
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Windows.Forms;

    internal class WinActionList : DesignerActionList
    {
        private McGraph _chart;
        private WinChartDesigner _chartDesigner;
        private DesignerActionItemCollection _items;

        public WinActionList(WinChartDesigner parent) : base(parent.Component)
        {
            this._chartDesigner = parent;
            this._chart = (McGraph)parent.Component;
        }

        private void DecreaseOpacity()
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this._chart)["ElementOpacity"];
            int num = (int) descriptor.GetValue(this._chart);
            if ((num - 0x19) > 0)
            {
                num -= 0x19;
            }
            else
            {
                num = 0;
            }
            descriptor.SetValue(this._chart, num);
            this._chart.DrawChart();
        }

        private void ChartBuilder()
        {
            new GraphSettings(this._chart, this._chartDesigner).ShowDialog();
            this._chart.DrawChart();
        }

        private void Dock()
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this._chart)["Dock"];
            DockStyle fill = DockStyle.Fill;
            descriptor.SetValue(this._chart, fill);
            this._chart.DrawChart();
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            if (this._items == null)
            {
                this._items = new DesignerActionItemCollection();
                this._items.Add(new DesignerActionHeaderItem("Chart", "chart"));
                //this._items.Add(new DesignerActionPropertyItem("DrawShadow", "Draw Shadow:", "chart", "Determines if the shadow is displayed on the chart."));
                this._items.Add(new DesignerActionPropertyItem("ChartTitle", "Chart Title:", "chart", "The Title that appears above the Chart."));
                this._items.Add(new DesignerActionPropertyItem("BackColor", "Background Color:", "chart", "The Background Color for the  main area of the Chart."));
                this._items.Add(new DesignerActionPropertyItem("ChartBackColor", "Chart BackColor:", "chart", "The Background Color for the Chart."));
                //this._items.Add(new DesignerActionMethodItem(this, "Dock", "Dock in Parent Container", true));
                //this._items.Add(new DesignerActionMethodItem(this, "DecreaseOpacity", "Decrease Opacity", true));
                //this._items.Add(new DesignerActionMethodItem(this, "IncreaseOpacity", "Increase Opacity", true));
                this._items.Add(new DesignerActionMethodItem(this, "DrawChart", "Draw Chart", true));
                this._items.Add(new DesignerActionMethodItem(this, "ChartBuilder", "Chart Builder", true));
            }
            return this._items;
        }

        private void IncreaseOpacity()
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this._chart)["ElementOpacity"];
            int num = (int) descriptor.GetValue(this._chart);
            if ((num + 0x19) < 0xff)
            {
                num += 0x19;
            }
            else
            {
                num = 0xff;
            }
            descriptor.SetValue(this._chart, num);
            this._chart.DrawChart();
        }

        private void DrawChart()
        {
            this._chart.DrawChart();
        }

        public Color BackColor
        {
            get
            {
                return this._chart.BackColor;
            }
            set
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this._chart)["BackColor"];
                if (descriptor != null)
                {
                    descriptor.SetValue(this._chart, value);
                }
                this._chart.DrawChart();
            }
        }

        public Color ChartBackColor
        {
            get
            {
                return this._chart.ChartBackColor;
            }
            set
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this._chart)["ChartBackColor"];
                if (descriptor != null)
                {
                    descriptor.SetValue(this._chart, value);
                }
                this._chart.DrawChart();
            }
        }

        public string ChartTitle
        {
            get
            {
                return this._chart.GraphTitle;
            }
            set
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this._chart)["GraphTitle"];
                if (descriptor != null)
                {
                    descriptor.SetValue(this._chart, value);
                }
                this._chart.DrawChart();
            }
        }

        public bool DrawShadow
        {
            get
            {
                return this._chart.DrawShadow;
            }
            set
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this._chart)["DrawShadow"];
                if (descriptor != null)
                {
                    descriptor.SetValue(this._chart, value);
                }
                this._chart.DrawChart();
            }
        }
    }
}

