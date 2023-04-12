using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ChartsSample.Controls
{
    public partial class LedControl : UserControl
    {
        public LedControl()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.ctlLed1.ScaleValue = (int)trackBar1.Value;
            this.ctlLed2.ScaleValue = (int)(trackBar1.Value);
            this.ctlLed3.ScaleValue = (int)(trackBar1.Value);
            this.ctlLed4.ScaleValue = (int)(trackBar1.Value);

        }
    }
}
