using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ChartsSample.Controls
{
    public partial class UsageControl : UserControl
    {
        public UsageControl()
        {
            InitializeComponent();
        }

        private void ctlButton3_Click(object sender, EventArgs e)
        {

            this.timer1.Enabled = true;
            this.timer1.Start();

        }

        private int value1 = 20;
        private int value2 = 25;
        private int value3 = 10;
        private int value4 = 15;
        private int stage = 0;
        private int dir = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {

            Random r = new Random();
            if (stage >= 25)
            {
                value1 = (int)r.Next(20, 80);
                value2 = (int)r.Next(25, 100);
                value3 = (int)r.Next(10, 70);
                value4 = (int)r.Next(15, 80);
                stage = 0;
                dir = dir * -1;
            }
            value1 += dir;
            value2 += dir;
            stage++;

            this.mcUsage1.Value1 = value1;
            this.mcUsage1.Value2 = value2;

            this.mcUsageHistory1.AddValues(value1, value2);

            this.mcUsage2.Value1 = value3;
            this.mcUsage2.Value2 = value4;

            this.mcUsageHistory2.AddValues(value3, value4);

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

    }
}
