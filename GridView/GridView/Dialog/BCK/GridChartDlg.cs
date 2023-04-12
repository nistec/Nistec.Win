using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mControl.Charts;

namespace mControl.GridView
{
    public partial class GridChartDlg : mControl.WinCtl.Forms.CtlForm
    {
        public static bool IsOpen = false;
        private Color[] ChartColors;
        private int colorCounter;

        public static void Open(Grid g, string caption)
        {
            GridChartDlg gch = new GridChartDlg();
            gch.CaptionSubText = caption;
            gch.SetChart(g);
            gch.Show();
        }
  
        public GridChartDlg()
        {
            InitializeComponent();
        }

  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VGridDlg.IsOpen = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            VGridDlg.IsOpen = false;
        }
        [Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced )]
        public CtlPieChart PieChart
        {
            get { return this.ctlPieChart; }
        }

        private void mnPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void mnPrint_Click(object sender, EventArgs e)
        {
            //this.propertyGrid1.PerformPrint();
        }

        private void mnOffset_Click(object sender, EventArgs e)
        {
            SetOffset(currentOffset);
            currentOffset = currentOffset == 30 ? 0 : 30;
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
            colorCounter = 0;
            ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red,Color.Yellow, Color.Purple,Color.Pink,Color.Silver,Color.Orange,Color.Brown,Color.White };
            foreach (VGridField f in fields)
            {
                double val = (double) mControl.Util.Types.ToDouble(f.Value,0);
                if (val >= 0)
                {
                    ctlPieChart.Items.Add(new PieChartItem(val, ChartColors[colorCounter], f.Key.ToString() + " \r\n[" + f.Text + "]", f.Key.ToString() + " [" + f.Text + "]", 0));
                    colorCounter++;
                    if (colorCounter >= ChartColors.Length) colorCounter = 0;
                }
            }

            ctlPieChart.ItemStyle.SurfaceAlphaTransparency = 0.75F;
            ctlPieChart.FocusedItemStyle.SurfaceAlphaTransparency = 0.75F;
            ctlPieChart.FocusedItemStyle.SurfaceBrightnessFactor = 0.3F;
         }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.ctlPieChart != null)
            {
                this.ctlPieChart.Radius = (float)Math.Min(this.ctlPieChart.Width, this.ctlPieChart.Height /*- this.caption.Height*/)* 0.48F;
            }
        }


        private void SetOffset(int value)
        {
  
            for (int i = 0; i < ctlPieChart.Items.Count;i++ )
            {
                ctlPieChart.Items[item].Offset = value;
            }
        }

        private void Play()
        {
            this.timer1.Enabled = !isRunning;
            isRunning = !isRunning;
            this.mnPlay.Text = isRunning ? "Stop" : "Play";
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