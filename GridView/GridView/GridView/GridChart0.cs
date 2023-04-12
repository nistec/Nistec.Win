using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using mControl.Charts;
using mControl.Util;

namespace mControl.GridView
{
    /// <summary>
    /// GridChart.
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(GridChart), "Images.GridChart.bmp")]
    public partial class GridChart : UserControl
    {

        private Color[] ChartColors;
        private int colorCounter;
        private int decimalPoint=2;
        private decimal minValue = 0;
        private const int MaxColors = 15;
        
        private int i = 0;
        //private int offset = 30;
        //private int item = 0;
        private bool isRunning = false;
        private int currentOffset = 0;
        private Control owner;

        public GridChart()
        {
            InitializeComponent();
            this.cmRemove.Visible = false;

            currentLeading = "cmLeading30";
            currentDepth = "cmDepth50";

        }

        public GridChart(Control ctl):this()
        {
            owner = ctl;
            this.Size = ctl.Size;
            this.Location = new System.Drawing.Point(1, 1);
            owner.SizeChanged += new EventHandler(owner_SizeChanged);
            this.cmRemove.Visible = owner != null;

        }

        void owner_SizeChanged(object sender, EventArgs e)
        {
            this.Size = owner.Size;
        }

        public bool ShowPanelColors
        {
            get{return this.panelColors.Visible;}
            set{this.panelColors.Visible=value;}
        }

        public string Caption
        {
            get { return this.lblCaption.Text; }
            set { this.lblCaption.Text = value; }
        }

        public int PanelColorsWidth
        {
            get { return this.panelColors.Width; }
            set
            {
                if (value < 0)
                    return;
                this.panelColors.Width = value;
            }
        }
        public int DecimalPoint
        {
            get { return this.decimalPoint; }
            set 
            {
                if(value < 0 )
                    return;
                this.decimalPoint = value; 
            }
        }
        public decimal MinValue
        {
            get { return this.minValue; }
            set
            {
                if (value < 0)
                    return;
                this.minValue = value;
            }
        }

        [Browsable(false)]
        public int CurrentOffset
        {
            get { return this.currentOffset; }
        }

        [Browsable(false)]
        public bool IsRunning
        {
            get { return this.isRunning; }
        }
        [Browsable(false)]
        public CtlPieChart PieChart
        {
            get { return this.ctlPieChart; }
        }


        //public void SetChart(DataTable dt, string groupByColumn, string sumColumn)
        //{
        //    if (dt == null || dt.Rows.Count == 0)
        //        return ;
        //    VGridFieldCollection vcols = new VGridFieldCollection();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        vcols.Add(new VGridField(dr[groupByColumn].ToString(), dr[sumColumn]));
        //    }

        //    if (vcols.Count == 0)
        //        return ;
        //    SetChart( vcols.GetFieldsArray());
        //}

        public void SetChart(VGridField[] fields)
        {
            if (fields == null || fields.Length == 0)
            {
                return;
            }
            ClearPanels();
            ctlPieChart.Items.Clear();
            int index = 0;
            colorCounter = 0;
            ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red, Color.Olive, Color.Purple, Color.Fuchsia, Color.Silver, Color.Orange, Color.MediumSeaGreen, Color.Brown, Color.Turquoise, Color.Tomato, Color.CornflowerBlue, Color.White };
            foreach (VGridField f in fields)
            {
                decimal val = (decimal)mControl.Util.Types.ToDecimal(f.Value, 0);
                if (val >= minValue)
                {
                    val = Math.Round(val, decimalPoint);
                    ctlPieChart.Items.Add(new PieChartItem((double)val, ChartColors[colorCounter], f.Key.ToString() + " \r\n[" + f.Text + "]", f.Key.ToString() + " [" + f.Text + "]", 0));
                    AddPanel(index, colorCounter, f.Key.ToString() + " [" + val.ToString() + "]");
                    index++;
                    colorCounter++;
                    if (colorCounter >= ChartColors.Length) colorCounter = 0;
                }
            }

            ctlPieChart.ItemStyle.SurfaceTransparency = 0.75F;
            ctlPieChart.FocusedItemStyle.SurfaceTransparency = 0.75F;
            ctlPieChart.FocusedItemStyle.SurfaceBrightness = 0.3F;
        }

        public void SetChart(DataTable dt, string groupByColumn, string sumColumn)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            if (dt.Rows.Count > 500)
            {
                MsgBox.ShowWarning("Too meny items.");
                return;
            }

            ClearPanels();
            ctlPieChart.Items.Clear();
            int index = 0;
            colorCounter = 0;
            ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red, Color.Olive, Color.Purple, Color.Fuchsia, Color.Silver, Color.Orange, Color.MediumSeaGreen, Color.Brown, Color.Turquoise, Color.Tomato, Color.CornflowerBlue, Color.White };
            decimal val = 0;
            string key = "";
            string[] groupFields = groupByColumn.Split(',');
            string[] sumFields = sumColumn.Split(',');

            foreach (DataRow dr in dt.Rows)
            {
                key = "";
                val = 0;
                foreach (string s in groupFields)
                {
                    key +=string.Format("{0}:{1}, ", s,dr[s]);
                }
                key.TrimEnd(',');

                foreach (string s in sumFields)
                {
                    val += (decimal) mControl.Util.Types.ToDecimal(dr[s], 0);
                }

                //double val = (double)mControl.Util.Types.ToDecimal(dr[sumColumn], 0);
                //string key = dr[groupByColumn].ToString();
                if (val >= minValue)
                {
                    val = Math.Round(val, decimalPoint);
                    ctlPieChart.Items.Add(new PieChartItem((double)val, ChartColors[colorCounter], key + " \r\n[" + val.ToString() + "]", key + " [" + val.ToString() + "]", 0));
                    AddPanel(index, colorCounter, key + " [" + val.ToString() + "]");
                    index++;
                    colorCounter++;
                    if (colorCounter >= ChartColors.Length) colorCounter = 0;
                }
            }

            ctlPieChart.ItemStyle.SurfaceTransparency = 0.75F;
            ctlPieChart.FocusedItemStyle.SurfaceTransparency = 0.75F;
            ctlPieChart.FocusedItemStyle.SurfaceBrightness = 0.3F;
        }

        public void SetChart(Grid grid)
        {
            if (grid == null)
            {
                return;
            }
            VGridField[] fields = GridPerform.SummarizeColumns(grid);
            SetChart(fields);
 
        }


        //public void SetChart(Grid grid,string[] columns)
        //{
        //    if (grid == null)
        //    {
        //        return;
        //    }
        //    VGridField[] fields = GridPerform.SummarizeColumns(grid, columns);
        //    if (fields == null || fields.Length == 0)
        //    {
        //        return;
        //    }
        //    ClearPanels();
        //    int index = 0;
        //    colorCounter = 0;
        //    ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red, Color.Olive, Color.Purple, Color.Fuchsia, Color.Silver, Color.Orange, Color.MediumSeaGreen, Color.Brown, Color.Turquoise, Color.Tomato, Color.CornflowerBlue, Color.White };
        //    foreach (VGridField f in fields)
        //    {
        //        double val = (double)mControl.Util.Types.ToDouble(f.Value, 0);
        //        if (val >= minValue)
        //        {
        //            val = Math.Round(val, decimalPoint);
        //            ctlPieChart.Items.Add(new PieChartItem(val, ChartColors[colorCounter], f.Key.ToString() + " \r\n[" + f.Text + "]", f.Key.ToString() + " [" + f.Text + "]", 0));
        //            AddPanel(index, colorCounter, f.Key.ToString() + " [" + val.ToString() + "]");
        //            index++;
        //            colorCounter++;
        //            if (colorCounter >= ChartColors.Length) colorCounter = 0;
        //        }
        //    }

        //    ctlPieChart.ItemStyle.SurfaceAlphaTransparency = 0.75F;
        //    ctlPieChart.FocusedItemStyle.SurfaceAlphaTransparency = 0.75F;
        //    ctlPieChart.FocusedItemStyle.SurfaceBrightnessFactor = 0.3F;
        //}

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

        private void AddPanel(int index,int colorIndex, string text)
        {
            //if (index > MaxColors)
            //    return;
            int top = 4 + index * 16;
            Panel p = new Panel();
            p.BorderStyle = BorderStyle.FixedSingle;
            p.BackColor = ChartColors[colorIndex];
            p.Location = new System.Drawing.Point(3, top);
            p.Name = "panelx" + index.ToString();
            p.Size = new System.Drawing.Size(13, 13);
            this.panelColors.Controls.Add(p);

            Label l = new Label();
            l.AutoSize = true;
            l.Location = new System.Drawing.Point(22, top);
            l.Name = "labelx" + index.ToString();
            l.Size = new System.Drawing.Size(0, 13);
            l.Text = text;
            this.panelColors.Controls.Add(l);

      
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.ctlPieChart != null)
            {
                float size = (float)Math.Min(this.ctlPieChart.Width, this.ctlPieChart.Height /*- this.caption.Height*/) ;
                size = size * 0.44F;
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
                ctlPieChart.Items[i].Offset = value;
            }
            currentOffset = value;// currentOffset == 30 ? 0 : 30;

        }

        public void Play()
        {
            this.timer1.Enabled = !isRunning;
            isRunning = !isRunning;
            //this.mnPlay.Text = isRunning ? "Stop" : "Play";
        }

 
        private void timer1_Tick(object sender, EventArgs e)
        {
            ctlPieChart.Rotation = (float)(i * Math.PI / 180);
            i++;

            /*if (i % 180 == 0)
            {
                ctlPieChart.Items[item].Offset = offset;
                item++;
                if (item >= ctlPieChart.Items.Count)
                {
                    item = 0;
                    offset = offset == 0 ? offset : 0;
                }
            }*/

            if (i == 360)
            {
                i = 0;
            }
        }

        //private void cmOffset_Click(object sender, EventArgs e)
        //{
        //    int ofst = this.CurrentOffset;
        //    ofst = ofst == 30 ? 0 : 30;
        //    this.SetOffset(ofst);
        //    this.cmOffset.Text = ofst == 30 ? "Hide Offset" : "Show Offset";
        //}

        //private void cmColors_Click(object sender, EventArgs e)
        //{
        //    this.ShowPanelColors = !this.ShowPanelColors;
        //    this.cmColors.Text = this.ShowPanelColors ? "Hide Colors" : "Show Colors";
        //}

        //private void cmPlay_Click(object sender, EventArgs e)
        //{
        //    this.Play();
        //    this.cmPlay.Text = this.IsRunning ? "Stop" : "Play";

        //}

        //private void cmPrint_Click(object sender, EventArgs e)
        //{

        //}

        private string currentLeading;
        private string currentDepth;

        private void SetLeadingCheckedItem(ToolStripMenuItem item, int value)
        {

            switch (currentLeading)
            {
                case "cmLeading15":
                    cmLeading15.Checked=false;
                    break;
                case "cmLeading30":
                    cmLeading30.Checked = false;
                    break;
                case "cmLeading45":
                    cmLeading45.Checked = false;
                    break;
                case "cmLeading90":
                    cmLeading90.Checked = false;
                    break;
            }
            currentLeading = item.Name;
            ctlPieChart.Leaning = (float)(value * Math.PI / 180);
        }
  
        private void SetDepthCheckedItem(ToolStripMenuItem item, int value)
        {
            switch (currentDepth)
            {
                case "cmDepth0":
                    cmDepth0.Checked=false;
                    break;
                case "cmDepth25":
                    cmDepth25.Checked=false;
                    break;
                case "cmDepth50":
                    cmDepth50.Checked=false;
                    break;
                case "cmDepth75":
                    cmDepth75.Checked=false;
                    break;
                case "cmDepth100":
                    cmDepth100.Checked=false;
                    break;

            }
            currentDepth = item.Name;
            ctlPieChart.Depth = value;
        }
        private void contextMenuChart_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "cmOffset":
                    {
                        int ofst = this.CurrentOffset;
                        ofst = ofst == 30 ? 0 : 30;
                        this.SetOffset(ofst);
                        this.cmOffset.Text = ofst == 30 ? "Hide Offset" : "Show Offset";
                    }
                    break;
                case "cmColors":
                    {
                        this.ShowPanelColors = !this.ShowPanelColors;
                        this.cmColors.Text = this.ShowPanelColors ? "Hide Colors" : "Show Colors";
                    }
                    break;
                case "cmPlay":
                    this.Play();
                    this.cmPlay.Text = this.IsRunning ? "Stop" : "Play";
                    break;
                case "cmPrint":
                    Charts.PieChartUtil.Print(ctlPieChart);
                    break;
                case "cmPreview":
                    mControl.WinCtl.Controls.CtlPrintPreviewDialog.Preview(Charts.PieChartUtil.GetPrintDocument(ctlPieChart));
                    break;
                case "cmSave":
                    Charts.PieChartUtil.SaveAs(ctlPieChart);
                    break;
                case "cmRemove":
                    owner.Controls.Remove(this); ;
                    break;
            }
        }

        private void cmLeaning_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "cmLeading15":
                    SetLeadingCheckedItem(cmLeading15, 15);
                    break;
                case "cmLeading30":
                    SetLeadingCheckedItem(cmLeading30, 30);
                    break;
                case "cmLeading45":
                    SetLeadingCheckedItem(cmLeading45, 45);
                    cmLeading30.Checked = false;
                    //currentLeading.Checked = false;
                    //cmLeading45.Checked = true;
                    break;
                case "cmLeading90":
                    SetLeadingCheckedItem(cmLeading90, 88);
                    break;
             }
        }

        private void cmDepth_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmLeaning_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmDepth_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                 case "cmDepth0":
                    SetDepthCheckedItem(cmDepth0, 0);
                    break;
                case "cmDepth25":
                    SetDepthCheckedItem(cmDepth25, 25);
                    break;
                case "cmDepth50":
                    SetDepthCheckedItem(cmDepth50, 50);
                    break;
                case "cmDepth75":
                    SetDepthCheckedItem(cmDepth75, 75);
                    break;
                case "cmDepth100":
                    SetDepthCheckedItem(cmDepth100, 100);
                    break;

            }
          }

       }
}
