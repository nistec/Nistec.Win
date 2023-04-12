using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Nistec.Charts;

using Nistec.Data;
using Nistec.Printing;

namespace Nistec.GridView
{
    /// <summary>
    /// Grid Chart control .
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(GridChart), "Images.GridChart.bmp")]
    public partial class GridChart : UserControl
    {

        //private Color[] ChartColors;
        //private int colorCounter;
        private int decimalPoint=2;
        private decimal minValue = 0;
        private const int MaxColors = 15;
        private bool showPanelColors=true;
      
        private int i = 0;
        //private int offset = 30;
        //private int item = 0;
        private bool isRunning = false;
        private int currentOffset = 0;
        private Control owner;
        /// <summary>
        /// Initilazing a new Grid Chart control
        /// </summary>
        public GridChart()
        {
            InitializeComponent();
            this.cmRemove.Visible = false;
            currentLeading = "cmLeading30";
            currentDepth = "cmDepth50";

        }
        /// <summary>
        /// Initilazing a new Grid Chart control
        /// </summary>
        /// <param name="ctl"></param>
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
        /// <summary>
        /// Get or Set indicating whether to show  Panel Colors
        /// </summary>
        public bool ShowPanelColors
        {
            get { return this.showPanelColors; }
            set 
            {
               this.showPanelColors = value;
            }
        }
        /// <summary>
        /// Get or Set Caption text of chart
        /// </summary>
        public string Caption
        {
            get { return this.lblCaption.Text; }
            set { this.lblCaption.Text = value; }
        }
        /// <summary>
        /// Get or Set the Decimal Point of values
        /// </summary>
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
        /// <summary>
        /// Get or Set the Minimum Value
        /// </summary>
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
        /// <summary>
        /// Get the CurrentOffset
        /// </summary>
        [Browsable(false)]
        public int CurrentOffset
        {
            get { return this.currentOffset; }
        }
        /// <summary>
        /// Get the value indicating if the chart control is running
        /// </summary>
        [Browsable(false)]
        public bool IsRunning
        {
            get { return this.isRunning; }
        }
        /// <summary>
        /// Get the internal PieChart control
        /// </summary>
        [Browsable(false)]
        public McPieChart PiChart
        {
            get { return this.ctlPieChart; }
        }

        /// <summary>
        /// SetChart
        /// </summary>
        /// <param name="fields"></param>
        public void SetChart(GridField[] fields)
        {
            PieChartUtil.CreateChart(ctlPieChart,fields);
        }
        /// <summary>
        /// SetChart
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="groupByColumn"></param>
        /// <param name="sumColumn"></param>
        public void SetChart(DataTable dt, string groupByColumn, string sumColumn)
        {

             PieChartUtil.CreateChart(ctlPieChart,dt, groupByColumn, sumColumn);
 
        }
        /// <summary>
        /// SetChart
        /// </summary>
        /// <param name="grid"></param>
        public bool SetChart(Grid grid)
        {
            if (grid == null)
            {
                return false;
            }
            GridField[] fields = GridPerform.SummarizeColumns(grid);
            if (fields == null)
                return false;
            SetChart(fields);//McFieldCollection.ToDataField( fields));
            return true;
        }


        //private void ClearPanels()
        //{
        //    Color backColor=this.BackColor;
        //    foreach (Control c in this.Controls)
        //    {
        //        if (c is Label)
        //            c.Text = "";
        //        else if (c is Panel)
        //            c.BackColor = backColor;
        //    }
        //}

        //private void AddPanel(int index,int colorIndex, string text)
        //{
        //    //if (index > MaxColors)
        //    //    return;
        //    int top = 6 + index * 16;
        //    Panel p = new Panel();
        //    p.BorderStyle = BorderStyle.FixedSingle;
        //    p.BackColor = ChartColors[colorIndex];
        //    p.Location = new System.Drawing.Point(3, top);
        //    p.Name = "panelx" + index.ToString();
        //    p.Size = new System.Drawing.Size(13, 13);
        //    this.ctlPieChart.Controls.Add(p);

        //    Label l = new Label();
        //    l.AutoSize = true;
        //    l.BackColor = Color.Transparent;
        //    l.Location = new System.Drawing.Point(22, top);
        //    l.Name = "labelx" + index.ToString();
        //    l.Size = new System.Drawing.Size(0, 13);
        //    l.Text = text;
        //    this.ctlPieChart.Controls.Add(l);
     
        //}

        /// <summary>
        /// Occurs on Size changed
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// Set Offset
        /// </summary>
        /// <param name="value"></param>
        public void SetOffset(int value)
        {

            for (int i = 0; i < ctlPieChart.Items.Count; i++)
            {
                ctlPieChart.Items[i].Offset = value;
            }
            currentOffset = value;// currentOffset == 30 ? 0 : 30;

        }
        /// <summary>
        /// Play chart control
        /// </summary>
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
                case "cmPlay":
                    this.Play();
                    this.cmPlay.Text = this.IsRunning ? "Stop" : "Play";
                    break;
                case "cmPrint":
                    Charts.PieChartUtil.Print(ctlPieChart);
                    break;
                case "cmPreview":
                    McPrintPreviewDialog.Preview(PieChartUtil.GetPrintDocument(ctlPieChart));
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

        #region old

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
        //        double val = (double)Nistec.Win.Types.ToDouble(f.Value, 0);
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

        //public void SetChart(VGridField[] fields)
        //{
        //    if (fields == null || fields.Length == 0)
        //    {
        //        return;
        //    }
        //    //ClearPanels();
        //    ctlPieChart.Items.Clear();
        //    int index = 0;
        //    colorCounter = 0;
        //    ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red, Color.Olive, Color.Purple, Color.Fuchsia, Color.Silver, Color.Orange, Color.MediumSeaGreen, Color.Brown, Color.Turquoise, Color.Tomato, Color.CornflowerBlue, Color.White };
        //    foreach (VGridField f in fields)
        //    {
        //        decimal val = (decimal)Nistec.Win.Types.ToDecimal(f.Value, 0);
        //        if (val >= minValue)
        //        {
        //            val = Math.Round(val, decimalPoint);
        //            ctlPieChart.Items.Add(new PieChartItem((double)val, ChartColors[colorCounter], f.Key.ToString() + " \r\n[" + f.Text + "]", f.Key.ToString() + " [" + f.Text + "]", 0));
        //            AddPanel(index, colorCounter, f.Key.ToString() + " [" + val.ToString() + "]");
        //            index++;
        //            colorCounter++;
        //            if (colorCounter >= ChartColors.Length) colorCounter = 0;
        //        }
        //    }

        //    ctlPieChart.ItemStyle.SurfaceTransparency = 0.75F;
        //    ctlPieChart.FocusedItemStyle.SurfaceTransparency = 0.75F;
        //    ctlPieChart.FocusedItemStyle.SurfaceBrightness = 0.3F;
        //}

        //public void SetChart(DataTable dt, string groupByColumn, string sumColumn)
        //{
        //    if (dt == null || dt.Rows.Count == 0)
        //        return;

        //    if (dt.Rows.Count > 500)
        //    {
        //        MsgBox.ShowWarning("Too meny items.");
        //        return;
        //    }

        //    ClearPanels();
        //    ctlPieChart.Items.Clear();
        //    int index = 0;
        //    colorCounter = 0;
        //    ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red, Color.Olive, Color.Purple, Color.Fuchsia, Color.Silver, Color.Orange, Color.MediumSeaGreen, Color.Brown, Color.Turquoise, Color.Tomato, Color.CornflowerBlue, Color.White };
        //    decimal val = 0;
        //    string key = "";
        //    string[] groupFields = groupByColumn.Split(',');
        //    string[] sumFields = sumColumn.Split(',');

        //    List<string> panelDesc = new List<string>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        key = "";
        //        val = 0;
        //        foreach (string s in groupFields)
        //        {
        //            key +=string.Format("{0}:{1}, ", s,dr[s]);
        //        }
        //        key.TrimEnd(',');

        //        foreach (string s in sumFields)
        //        {
        //            val += (decimal) Nistec.Win.Types.ToDecimal(dr[s], 0);
        //        }

        //        //double val = (double)Nistec.Win.Types.ToDecimal(dr[sumColumn], 0);
        //        //string key = dr[groupByColumn].ToString();
        //        if (val >= minValue)
        //        {
        //            val = Math.Round(val, decimalPoint);
        //            ctlPieChart.Items.Add(new PieChartItem((double)val, ChartColors[colorCounter], key + " \r\n[" + val.ToString() + "]", key + " [" + val.ToString() + "]", 0));
        //            //AddPanel(index, colorCounter, key + " [" + val.ToString() + "]");
        //            panelDesc.Add(key + " [" + val.ToString() + "]");
        //            index++;
        //            colorCounter++;
        //            if (colorCounter >= ChartColors.Length) colorCounter = 0;
        //        }
        //    }
        //    ctlPieChart.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
        //    ctlPieChart.ItemStyle.SurfaceTransparency = 0.75F;
        //    ctlPieChart.FocusedItemStyle.SurfaceTransparency = 0.75F;
        //    ctlPieChart.FocusedItemStyle.SurfaceBrightness = 0.3F;
        //    ctlPieChart.AddPanels(panelDesc.ToArray());
        //    ctlPieChart.Invalidate();
        //}


        //public void SetChart(DataTable dt, string groupByColumn, string sumColumn)
        //{
        //    if (dt == null || dt.Rows.Count == 0)
        //        return;

        //    if (dt.Rows.Count > 500)
        //    {
        //        MsgBox.ShowWarning("Too meny items.");
        //        return;
        //    }

        //    ClearPanels();
        //    ctlPieChart.Items.Clear();
        //    int index = 0;
        //    colorCounter = 0;
        //    ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red, Color.Olive, Color.Purple, Color.Fuchsia, Color.Silver, Color.Orange, Color.MediumSeaGreen, Color.Brown, Color.Turquoise, Color.Tomato, Color.CornflowerBlue, Color.White };
        //    decimal val = 0;
        //    string key = "";
        //    string[] groupFields = groupByColumn.Split(',');
        //    string[] sumFields = sumColumn.Split(',');

        //    List<string> panelDesc = new List<string>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        key = "";
        //        val = 0;
        //        foreach (string s in groupFields)
        //        {
        //            key += string.Format("{0}:{1}, ", s, dr[s]);
        //        }
        //        key.TrimEnd(',');

        //        foreach (string s in sumFields)
        //        {
        //            val += (decimal)Nistec.Win.Types.ToDecimal(dr[s], 0);
        //        }

        //        //double val = (double)Nistec.Win.Types.ToDecimal(dr[sumColumn], 0);
        //        //string key = dr[groupByColumn].ToString();
        //        if (val >= minValue)
        //        {
        //            val = Math.Round(val, decimalPoint);
        //            ctlPieChart.Items.Add(new PieChartItem((double)val, ChartColors[colorCounter], key + " \r\n[" + val.ToString() + "]", key + " [" + val.ToString() + "]", 0));
        //            //AddPanel(index, colorCounter, key + " [" + val.ToString() + "]");
        //            panelDesc.Add(key + " [" + val.ToString() + "]");
        //            index++;
        //            colorCounter++;
        //            if (colorCounter >= ChartColors.Length) colorCounter = 0;
        //        }
        //    }
        //    ctlPieChart.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
        //    ctlPieChart.ItemStyle.SurfaceTransparency = 0.75F;
        //    ctlPieChart.FocusedItemStyle.SurfaceTransparency = 0.75F;
        //    ctlPieChart.FocusedItemStyle.SurfaceBrightness = 0.3F;
        //    ctlPieChart.AddPanels(panelDesc.ToArray());
        //    ctlPieChart.Invalidate();
        //}



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


        #endregion

    }
}
