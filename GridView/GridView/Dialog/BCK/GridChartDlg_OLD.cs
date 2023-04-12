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
            get { return this.ctlPieChart.ctlPieChart; }
        }

        private void mnPlay_Click(object sender, EventArgs e)
        {
            this.ctlPieChart.Play();
            this.mnPlay.Text = this.ctlPieChart.IsRunning ? "Stop" : "Play";
        }

        private void mnPrint_Click(object sender, EventArgs e)
        {
            //this.propertyGrid1.PerformPrint();
        }

        private void mnOffset_Click(object sender, EventArgs e)
        {
            int ofst = this.ctlPieChart.CurrentOffset;
            ofst = ofst == 30 ? 0 : 30;
            this.ctlPieChart.SetOffset(ofst);
            this.mnOffset.Text = ofst == 30 ? "Hide Offset" : "Show Offset";
        }

        private void mnColors_Click(object sender, EventArgs e)
        {
            this.ctlPieChart.ShowPanelColors = !this.ctlPieChart.ShowPanelColors;
            this.mnColors.Text = this.ctlPieChart.ShowPanelColors ? "Hide Colors" : "Show Colors";

        }

        public void SetChart(Grid grid)
        {
            if (grid == null)
            {
                return;
            }
            this.ctlPieChart.SetChart(grid);
         }

        public bool ShowPanelColors
        {
            get { return this.ctlPieChart.ShowPanelColors; }
            set { this.ctlPieChart.ShowPanelColors = value; }
        }

   
         

  
    }
}