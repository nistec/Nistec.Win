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
    public partial class PieChartDisplay : UserControl
    {
        public PieChartDisplay()
        {
            InitializeComponent();

            ctlPieChart1.Items.Add(new PieChartItem(10, Color.Blue, "Blue", "Blue tool tip", 0));
            ctlPieChart1.Items.Add(new PieChartItem(10, Color.Gold, "Gold", "Gold tool tip", 0));
            ctlPieChart1.Items.Add(new PieChartItem(10, Color.Green, "Green", "Green tool tip", 0));
            ctlPieChart1.Items.Add(new PieChartItem(20, Color.Red, "Red", "Red tool tip", 0));


            ctlPieChart1.ItemStyle.SurfaceTransparency = 0.75F;
            ctlPieChart1.FocusedItemStyle.SurfaceTransparency = 0.75F;
            ctlPieChart1.FocusedItemStyle.SurfaceBrightness = 0.3F;

          }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           
            this.timer1.Interval = 10;

        }

        private int i = 0;
        private int offset = 30;
        private int item = 0;

          private void timer1_Tick(object sender, EventArgs e)
        {
  
            ctlPieChart1.Rotation = (float)(i * Math.PI / 180);
            i++;

            if (i % 180==0)
            {
                ctlPieChart1.Items[item].Offset = offset;
                item++;
                if (item > 3)
                {
                    item = 0;
                    offset = offset == 0 ? 30 : 0;
                }
            }

            if (i == 360)
            {
                i = 0;
            }
        }

        private void ctlButton3_Click(object sender, EventArgs e)
        {
            //this.timer1.Enabled = true;
            this.timer1.Start();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

        
    }
}
