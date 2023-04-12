using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using mControl.Charts;

namespace mControl.GridView.GridItems
{
    public partial class GridChart : UserControl
    {

        private Color[] ChartColors;
        private int colorCounter;
        private const int MaxColors = 16;

        public GridChart()
        {
            InitializeComponent();
        }

        public bool ShowPanelColors
        {
            get{return this.panelColors.Visible;}
            set{this.panelColors.Visible=value;}
        }
        public int CurrentOffset
        {
            get { return this.currentOffset; }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
        }


        public void SetChart(Grid grid)
        {
            if (grid == null)
            {
                return;
            }
            VGridField[] fields = grid.SummarizeColumns();
            if (fields == null || fields.Length == 0)
            {
                return;
            }
            ClearPanels();
            colorCounter = 0;
            ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red, Color.Yellow, Color.Purple, Color.Pink, Color.Silver, Color.Orange, Color.Brown, Color.White };
            foreach (VGridField f in fields)
            {
                double val = (double)mControl.Util.Types.ToDouble(f.Value, 0);
                if (val >= 0)
                {
                    ctlPieChart.Items.Add(new PieChartItem(val, ChartColors[colorCounter], f.Key.ToString() + " \r\n[" + f.Text + "]", f.Key.ToString() + " [" + f.Text + "]", 0));
                    AddPanel(colorCounter ,f.Key.ToString() + " [" + f.Text + "]");
                    //SetPanel(colorCounter+1, ChartColors[colorCounter], f.Key.ToString() + " [" + f.Text + "]");
                    colorCounter++;
                     if (colorCounter >= ChartColors.Length) colorCounter = 0;
                }
            }

            ctlPieChart.ItemStyle.SurfaceAlphaTransparency = 0.75F;
            ctlPieChart.FocusedItemStyle.SurfaceAlphaTransparency = 0.75F;
            ctlPieChart.FocusedItemStyle.SurfaceBrightnessFactor = 0.3F;
        }

        private void ClearPanels()
        {
            Color backColor=this.panelColors.BackColor;
            foreach (Control c in this.panelColors.Controls)
            {
                if (c is Label)
                    c.Text = "";
                else if (c is Panel)
                    c.BackColor = backColor;
            }
        }

        private void SetPanel(int index, Color color,string text)
        {
            if (index > MaxColors)
                return;
            Control p= this.panelColors.Controls["panel" + index.ToString()];
            p.BackColor = color;
            Control l = this.panelColors.Controls["label" + index.ToString()];
            l.Text = text;
        }

        private void AddPanel(int index, string text)
        {
            if (index > MaxColors)
                return;
            int top = 4 + index * 16;

            Label p = new Label();
            p.BorderStyle = BorderStyle.FixedSingle;
            p.BackColor = ChartColors[index];
            p.Location = new System.Drawing.Point(3, top);
            p.Name = "panelx" + index.ToString();
            p.Size = new System.Drawing.Size(13, 13);
            p.Text = "p";
            //p.TabIndex = index;
            this.panelColors.Controls.Add(p);

            Label l = new Label();
            l.AutoSize = true;
            l.BackColor = ChartColors[index];
            l.Location = new System.Drawing.Point(22, top);
            l.Name = "labelx" + index.ToString();
            l.Size = new System.Drawing.Size(0, 13);
            //l.TabIndex = index;
            l.Text = text;
            this.panelColors.Controls.Add(l);

      
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.ctlPieChart != null)
            {
                float size = (float)Math.Min(this.ctlPieChart.Width, this.ctlPieChart.Height /*- this.caption.Height*/) ;
                size = size * 0.48F;
                if (size > 0)
                {
                    this.ctlPieChart.Radius = (float)size;
                }
            }
        }


        public void SetOffset(int value)
        {

            for (int i = 0; i < ctlPieChart.Items.Count; i++)
            {
                ctlPieChart.Items[item].Offset = value;
            }
            currentOffset = value;// currentOffset == 30 ? 0 : 30;

        }

        public void Play()
        {
            this.timer1.Enabled = !isRunning;
            isRunning = !isRunning;
            //this.mnPlay.Text = isRunning ? "Stop" : "Play";
        }

        private int i = 0;
        private int offset = 30;
        private int item = 0;
        private bool isRunning = false;
        private int currentOffset = 30;

        private void timer1_Tick(object sender, EventArgs e)
        {
            ctlPieChart.Rotation = (float)(i * Math.PI / 180);
            i++;

            if (i % 180 == 0)
            {
                ctlPieChart.Items[item].Offset = offset;
                item++;
                if (item >= ctlPieChart.Items.Count)
                {
                    item = 0;
                    offset = offset == 0 ? offset : 0;
                }
            }

            if (i == 360)
            {
                i = 0;
            }
        }

       }
}
