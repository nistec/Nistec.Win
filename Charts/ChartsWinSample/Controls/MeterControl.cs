using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ChartsSample.Controls
{
    public partial class MeterControl : UserControl
    {
        public MeterControl()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //this.ctlMeter1.ScaleValue = trackBar1.Value;
            this.ctlMeter2.ScaleValue = trackBar1.Value;
            this.ctlMeter3.ScaleValue = trackBar1.Value;

        }

 
        private void ctlButton3_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 100;
            this.timer1.Enabled = !this.timer1.Enabled;
            if (this.timer1.Enabled)
                this.timer1.Start();
            else
                this.timer1.Stop();
        }

        private int i = 0;
        private int dir = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ctlMeter2.ScaleValue = i;
            this.ctlMeter3.ScaleValue = i;
            this.ctlMeter4.ScaleValue = i / 3;
            this.ctlLed4.ScaleValue = i;
            i += dir;
            //this.Invalidate(true);
            if (i == 100 || i == 0)
            {
                dir = dir * -1;
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

    }
}
