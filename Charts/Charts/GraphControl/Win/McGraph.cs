namespace Nistec.Charts.Win
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Design;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;
    using System.Windows.Forms;
    using Nistec.Charts.Statistics;

    [ToolboxBitmap(typeof(McGraph), "Nistec.Charts.Images.Graph.bmp")]
    [DefaultProperty("GraphTitle"), Designer(typeof(WinChartDesigner))]
    public class McGraph : UserControl, ISupportInitialize
    {
        private static int barEffectsNo = 12;
        private Charts.Chart chart;
        private IContainer components=null;
        private CurrencyManager dataManager;
        private string dataMember = "";
        private object dataSource;
        private bool Initializing;
        private ListChangedEventHandler listChangedHandler;
        private PictureBox pictureBox1;
        private static int pieEffectsNo = 6;

        public event ChartClickEventHandler ChartClick;

        public McGraph()
        {
            this.InitializeComponent();
            this.listChangedHandler = new ListChangedEventHandler(this.dataManager_ListChanged);
        }

        //public McGraph(ChartType type,DataView view,KeyItemCollection keys,DataItemCollection dataItems ):this()
        //{
        //    this.ChartType = type;
        //    this.DataSource = view;
        //    this.Keys = keys;
        //    this.DataItems = DataItemCollection;
        
        //}


        private void calculateColumns()
        {
        }

        private void ChartWinControl_Paint(object sender, PaintEventArgs e)
        {
            if (this.pictureBox1.Image == null)
            {
                this.DrawChart();
            }
            if (this.chart.BackColor != this.BackColor)
            {
                this.DrawChart();
            }
        }

        private void dataManager_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.DrawChart();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void DrawChart(DataView view, string fieldLabel, string[] dataItems, string[] keyItems)
        {
            this.DataSource = view;
            this.DataItems.Clear();
            this.DataItems.AddRange(dataItems);
            this.KeyItems.AddRange(keyItems);
            this.FieldLabel = fieldLabel;
            this.DrawChart();
        }
        public void DrawChart()
        {
            if (!this.Initializing)
            {
                try
                {
                    if (this.Chart.col.Count == 0)
                    {
                        Color[] colors = new Color[] { Color.Red, Color.FromArgb(0, 0xff, 0), Color.Blue, Color.Yellow, Color.Purple, Color.Fuchsia };
                        this.SetColors(colors);
                        this.ColorItems = this.ColorItems;
                    }
                    if (base.DesignMode)
                    {
                        if (this.Chart.dataItems.Count == 0)
                        {
                            this.SetDataFields("values1,values2,values3");
                            this.DataItems = this.DataItems;
                        }
                        DataTable table = new DataTable();
                        foreach (DataItem item in this.Chart.dataItems)
                        {
                            if (table.Columns[item.Name] == null)
                            {
                                table.Columns.Add(item.Name, typeof(int));
                            }
                        }
                        if (((this.Chart.XAxisLabels != null) && (this.Chart.XAxisLabels != string.Empty)) && (table.Columns[this.Chart.XAxisLabels] == null))
                        {
                            table.Columns.Add(this.Chart.XAxisLabels, typeof(string));
                        }
                        if (((this.Chart.FieldID != null) && (this.Chart.FieldID != string.Empty)) && (table.Columns[this.FieldID] == null))
                        {
                            table.Columns.Add(this.FieldID, typeof(string));
                        }
                        //if (((this.chart.FieldCategory != null) && (this.chart.FieldCategory != string.Empty)) && (table.Columns[this.FieldCategory] == null))
                        //{
                        //    table.Columns.Add(this.FieldCategory, typeof(string));
                        //}
                        Random random = new Random();

                        int num = 20;
                        for (int i = 0; i < num; i++)
                        {
                            DataRow row = table.NewRow();
                            foreach (DataItem item2 in this.Chart.dataItems)
                            {
                                row[item2.Name] = random.Next(0x29a);
                            }
                            if ((this.Chart.XAxisLabels != null) && (this.Chart.XAxisLabels != string.Empty))
                            {
                                if (table.Columns[this.Chart.XAxisLabels].DataType == typeof(string))
                                {
                                    row[this.chart.XAxisLabels] = "Label" + i.ToString();
                                    //if ((i % 2) == 0)
                                    //{
                                    //    row[this.Chart.XAxisLabels] = "Designer";
                                    //}
                                    //else
                                    //{
                                    //    row[this.Chart.XAxisLabels] = "Random";
                                    //}
                                }
                                else
                                {
                                    row[this.Chart.XAxisLabels] = random.Next(0x29a);
                                }
                            }
                            table.Rows.Add(row);
                        }
                    }
                    Statistic.Rezolve(this.chart.data, this.DataItems, this.KeyItems);
                    this.Chart.BackColor = this.BackColor;
                    this.Chart.ForeColor = this.ForeColor;
                    this.Chart.width = base.Width;
                    this.Chart.height = base.Height;
                    if (!base.DesignMode)
                    {
                        this.UpdateAllData();
                    }
                    if ((this.Chart.data != null) && (this.chart.data.Rows.Count != 0))
                    {
                        this.pictureBox1.Image = this.Chart.Draw();
                    }
                }
                catch
                {
                }
            }
        }

        private void DrawChartDesign()
        {
            if (!this.Initializing)
            {
                try
                {
                    if (this.Chart.dataItems.Count == 0)
                    {
                        //this.SetDataFields("values1,values2,values3");
                        //this.DataItems = this.DataItems;
                    }
                    DataTable table = new DataTable();
                    foreach (DataItem item in this.Chart.dataItems)
                    {
                        if (table.Columns[item.Name] == null)
                        {
                            table.Columns.Add(item.Name, typeof(int));
                        }
                    }
                    if (((this.Chart.XAxisLabels != null) && (this.Chart.XAxisLabels != string.Empty)) && (table.Columns[this.Chart.XAxisLabels] == null))
                    {
                        table.Columns.Add(this.Chart.XAxisLabels, typeof(string));
                    }
                    if (((this.Chart.FieldID != null) && (this.Chart.FieldID != string.Empty)) && (table.Columns[this.FieldID] == null))
                    {
                        table.Columns.Add(this.FieldID, typeof(string));
                    }
                    //if (((this.chart.FieldCategory != null) && (this.chart.FieldCategory != string.Empty)) && (table.Columns[this.FieldCategory] == null))
                    //{
                    //    table.Columns.Add(this.FieldCategory, typeof(string));
                    //}
                    Random random = new Random();

                    int num = 20;
                    for (int i = 0; i < num; i++)
                    {
                        DataRow row = table.NewRow();
                        foreach (DataItem item2 in this.Chart.dataItems)
                        {
                            row[item2.Name] = random.Next(0x29a);
                        }
                        if ((this.Chart.XAxisLabels != null) && (this.Chart.XAxisLabels != string.Empty))
                        {
                            if (table.Columns[this.Chart.XAxisLabels].DataType == typeof(string))
                            {
                                row[this.chart.XAxisLabels] = "Label" + i.ToString();
                                //if ((i % 2) == 0)
                                //{
                                //    row[this.Chart.XAxisLabels] = "Designer";
                                //}
                                //else
                                //{
                                //    row[this.Chart.XAxisLabels] = "Random";
                                //}
                            }
                            else
                            {
                                row[this.Chart.XAxisLabels] = random.Next(0x29a);
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                catch
                {
                }
            }
        }

        //public void DrawChart()
        //{
        //    if (!this.Initializing)
        //    {
        //        try
        //        {
        //            if (this.Chart.col.Count == 0)
        //            {
        //                Color[] colors = new Color[] { Color.Red, Color.FromArgb(0, 0xff, 0), Color.Blue, Color.Yellow, Color.Purple, Color.Fuchsia };
        //                this.SetColors(colors);
        //                this.ColorItems = this.ColorItems;
        //            }
        //            if (base.DesignMode)
        //            {
        //                if (this.Chart.dataItems.Count == 0)
        //                {
        //                    //this.SetDataFields("values1,values2,values3");
        //                    //this.DataItems = this.DataItems;
        //                }
        //                DataTable table = new DataTable();
        //                foreach (DataItem item in this.Chart.dataItems)
        //                {
        //                    if (table.Columns[item.Name] == null)
        //                    {
        //                        table.Columns.Add(item.Name, typeof(int));
        //                    }
        //                }
        //                if (((this.Chart.XAxisLabels != null) && (this.Chart.XAxisLabels != string.Empty)) && (table.Columns[this.Chart.XAxisLabels] == null))
        //                {
        //                    table.Columns.Add(this.Chart.XAxisLabels, typeof(string));
        //                }
        //                if (((this.Chart.FieldID != null) && (this.Chart.FieldID != string.Empty)) && (table.Columns[this.FieldID] == null))
        //                {
        //                    table.Columns.Add(this.FieldID, typeof(string));
        //                }
        //                if (((this.chart.FieldCategory != null) && (this.chart.FieldCategory != string.Empty)) && (table.Columns[this.FieldCategory] == null))
        //                {
        //                    table.Columns.Add(this.FieldCategory, typeof(string));
        //                }
        //                Random random = new Random();
        //                if ((this.ChartType != ChartType.CustomMap) && (this.ChartType != ChartType.CustomMapPoints))
        //                {
        //                    int num = 20;
        //                    for (int i = 0; i < num; i++)
        //                    {
        //                        DataRow row = table.NewRow();
        //                        foreach (DataItem item2 in this.Chart.dataItems)
        //                        {
        //                            row[item2.Name] = random.Next(0x29a);
        //                        }
        //                        if ((this.Chart.XAxisLabels != null) && (this.Chart.XAxisLabels != string.Empty))
        //                        {
        //                            if (table.Columns[this.Chart.XAxisLabels].DataType == typeof(string))
        //                            {
        //                                row[this.chart.XAxisLabels] = "Label" + i.ToString();
        //                                //if ((i % 2) == 0)
        //                                //{
        //                                //    row[this.Chart.XAxisLabels] = "Designer";
        //                                //}
        //                                //else
        //                                //{
        //                                //    row[this.Chart.XAxisLabels] = "Random";
        //                                //}
        //                            }
        //                            else
        //                            {
        //                                row[this.Chart.XAxisLabels] = random.Next(0x29a);
        //                            }
        //                        }
        //                        table.Rows.Add(row);
        //                    }
        //                }
        //                else if (this.ChartType == ChartType.CustomMapPoints)
        //                {
        //                    int num3 = 20;
        //                    string str = "abcdefghijklmnopqrstuvxyz";
        //                    for (int j = 0; j < num3; j++)
        //                    {
        //                        DataRow row2 = table.NewRow();
        //                        row2[this.chart.dataItems[0].Name] = random.Next(180) - 90;
        //                        row2[this.chart.dataItems[1].Name] = random.Next(360) - 180;
        //                        if ((this.chart.XAxisLabels != null) && (this.chart.XAxisLabels != string.Empty))
        //                        {
        //                            if (table.Columns[this.chart.XAxisLabels].DataType == typeof(string))
        //                            {
        //                                row2[this.chart.XAxisLabels] = str.Substring(random.Next(20), 4);
        //                            }
        //                            else
        //                            {
        //                                row2[this.chart.XAxisLabels] = random.Next(0x29a);
        //                            }
        //                        }
        //                        if ((this.chart.FieldCategory != null) && (this.chart.FieldCategory != string.Empty))
        //                        {
        //                            row2[this.chart.FieldCategory] = random.Next(3);
        //                        }
        //                        table.Rows.Add(row2);
        //                    }
        //                }
        //                else if (this.ChartType == ChartType.CustomMap)
        //                {
        //                    DataTable table2 = new DataTable("Element");
        //                    table2.Columns.Add("ID", typeof(string));
        //                    table2.Columns.Add("R", typeof(byte));
        //                    table2.Columns.Add("G", typeof(byte));
        //                    table2.Columns.Add("B", typeof(byte));
        //                    table2.Columns.Add("X", typeof(int));
        //                    table2.Columns.Add("Y", typeof(int));
        //                    table2.PrimaryKey = new DataColumn[] { table2.Columns["ID"] };
        //                    MemoryStream stream = new MemoryStream();
        //                    StreamWriter writer = new StreamWriter(stream);
        //                    writer.Write(this.Chart.mapXml);
        //                    writer.Flush();
        //                    StreamReader reader = new StreamReader(stream);
        //                    stream.Position = 0L;
        //                    table2.ReadXml(reader);
        //                    writer.Close();
        //                    writer.Dispose();
        //                    stream.Close();
        //                    stream.Dispose();
        //                    reader.Close();
        //                    reader.Dispose();
        //                    foreach (DataRow row3 in table2.Rows)
        //                    {
        //                        DataRow row4 = table.NewRow();
        //                        foreach (DataItem item3 in this.Chart.dataItems)
        //                        {
        //                            row4[item3.Name] = random.Next(0x14d);
        //                        }
        //                        if ((this.Chart.XAxisLabels != null) && (this.Chart.XAxisLabels != string.Empty))
        //                        {
        //                            row4[this.Chart.XAxisLabels] = row3["ID"].ToString();
        //                        }
        //                        if ((this.Chart.FieldID != null) && (this.Chart.FieldID != string.Empty))
        //                        {
        //                            row4[this.Chart.FieldID] = row3["ID"].ToString();
        //                        }
        //                        if ((this.chart.FieldCategory != null) && (this.chart.FieldCategory != string.Empty))
        //                        {
        //                            row4[this.chart.FieldCategory] = random.Next(3);
        //                        }
        //                        table.Rows.Add(row4);
        //                    }
        //                }
        //                this.Chart.data = table;
        //            }
        //            Statistics.rezolve(this.chart.data, this.DataItems, this.KeyItems);
        //            this.Chart.BackColor = this.BackColor;
        //            this.Chart.ForeColor = this.ForeColor;
        //            this.Chart.width = base.Width;
        //            this.Chart.height = base.Height;
        //            if (!base.DesignMode)
        //            {
        //                this.updateAllData();
        //            }
        //            if ((this.Chart.data != null) && (this.chart.data.Rows.Count != 0))
        //            {
        //                this.pictureBox1.Image = this.Chart.Draw();
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        public DataRowView GetDataRow(Point p)
        {
            if (this.Chart.colectieHotspoturi != null)
            {
                foreach (ChartHotSpot spot in (ChartHotSpotCollection) this.Chart.colectieHotspoturi)
                {
                    if (!(spot is ChartRectangleHotSpot))
                    {
                        continue;
                    }
                    ChartRectangleHotSpot spot2 = (ChartRectangleHotSpot) spot;
                    if (((spot2.Bottom >= p.Y) && (spot2.Top <= p.Y)) && ((spot2.Left <= p.X) && (spot2.Right >= p.X)))
                    {
                        try
                        {
                            string[] strArray = spot2.PostBackValue.Split(new char[] { '|' });
                            return (DataRowView) this.dataManager.List[int.Parse(strArray[2])];
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new PictureBox();
            //this.pictureBox1.BeginInit();
            base.SuspendLayout();
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(640, 480);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick+=(new MouseEventHandler(this.pictureBox1_MouseClick));
            this.pictureBox1.SizeChanged += new EventHandler(this.pictureBox1_SizeChanged);
            base.AutoScaleDimensions=(new SizeF(6f, 13f));
            base.AutoScaleMode= (AutoScaleMode)(1);
            base.Controls.Add(this.pictureBox1);
            base.Name = "McGraph";
            base.Size = new Size(640, 480);
            base.Paint += new PaintEventHandler(this.ChartWinControl_Paint);
            //this.pictureBox1.EndInit();
            base.ResumeLayout(false);
        }

        public void NormalizeMapLocation()
        {
            decimal num = this.chart.lat2 - this.chart.lat1;
            decimal num2 = this.chart.lon2 - this.chart.lon1;
            if ((base.Height / base.Width) > (num / num2))
            {
                num2 = num * (base.Width / base.Height);
                decimal num3 = (this.chart.lon1 + this.chart.lon2) / 2M;
                decimal num4 = num3 - (num2 / 2M);
                decimal num5 = num3 + (num2 / 2M);
                if ((num4 >= -180M) && (num5 <= 180M))
                {
                    this.chart.lon1 = num4;
                    this.chart.lon2 = num5;
                }
            }
            else if ((base.Height / base.Width) < (num / num2))
            {
                num = num2 * (base.Height / base.Width);
                decimal num6 = (this.chart.lat1 + this.chart.lat2) / 2M;
                decimal num7 = num6 - (num / 2M);
                decimal num8 = num6 + (num / 2M);
                if ((num7 >= -90M) && (num8 <= 90M))
                {
                    this.chart.lat1 = num7;
                    this.chart.lat2 = num8;
                }
            }
            this.DrawChart();
        }

        protected override void OnBindingContextChanged(EventArgs e)
        {
            base.OnBindingContextChanged(e);
            this.DrawChart();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Chart.colectieHotspoturi != null)
            {
                foreach (ChartHotSpot spot in (ChartHotSpotCollection) this.Chart.colectieHotspoturi)
                {
                    if (!(spot is ChartRectangleHotSpot))
                    {
                        continue;
                    }
                    ChartRectangleHotSpot spot2 = (ChartRectangleHotSpot) spot;
                    if (((spot2.Bottom >= e.Y) && (spot2.Top <= e.Y)) && ((spot2.Left <= e.X) && (spot2.Right >= e.X)))
                    {
                        try
                        {
                            string[] strArray = spot2.PostBackValue.Split(new char[] { '|' });
                            int position = int.Parse(strArray[2]);
                            if (this.AutoSelect)
                            {
                                this.dataManager.Position = position;
                            }
                            if (this.ChartClick.GetInvocationList().Length > 0)
                            {
                                if (strArray[1] != string.Empty)
                                {
                                    this.ChartClick(this, strArray[0], strArray[1], position);
                                }
                                else
                                {
                                    this.ChartClick(this, strArray[0], this.Chart.dataItems[0].Name, position);
                                }
                            }
                        }
                        catch
                        {
                        }
                        break;
                    }
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (base.Visible && (this.pictureBox1.Image == null))
            {
                this.DrawChart();
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if ((this.pictureBox1.Size.Width > 0) && (this.pictureBox1.Height > 0))
            {
                this.DrawChart();
            }
        }

        public void SetColors(Color[] colors)
        {
            this.Chart.col.Clear();
            foreach (Color color in colors)
            {
                this.Chart.col.Add(new ColorItem(color));
            }
            this.DrawChart();
        }

        public void SetDataFields(string comaSeparatedFields)
        {
            this.Chart.dataItems.Clear();
            string[] fields = comaSeparatedFields.Split(new char[] { ',' });
            this.SetDataFields(fields);
        }

        public void SetDataFields(string[] fields)
        {
            this.Chart.dataItems.Clear();
            foreach (string str in fields)
            {
                this.Chart.dataItems.Add(new DataItem(str));
            }
            this.DrawChart();
        }

        public void SetKey(string comaSeparatedItems)
        {
            string[] items = comaSeparatedItems.Split(new char[] { ',' });
            this.SetKey(items);
        }

        public void SetKey(string[] items)
        {
            this.Chart.KeyLabels.Clear();
            foreach (string str in items)
            {
                this.Chart.KeyLabels.Add(new KeyItem(str));
            }
            this.DrawChart();
        }

        public void SetMapLocation(double lat1, double lat2, double lon1, double lon2)
        {
            if (((lat1 < -90.0) || (lat2 < -90.0)) || ((lat1 > 90.0) || (lat2 > 90.0)))
            {
                throw new ChartException(new Exception("Latitude is out of range (-90;+90)"));
            }
            if (((lon1 < -180.0) || (lon2 < -180.0)) || ((lon1 > 180.0) || (lon2 > 180.0)))
            {
                throw new ChartException(new Exception("Longitude is out of range (-180;+180)"));
            }
            if (lat1 > lat2)
            {
                double num = lat1;
                lat1 = lat2;
                lat2 = num;
            }
            if (lon1 > lon2)
            {
                double num2 = lon1;
                lon1 = lon2;
                lon2 = num2;
            }
            this.chart.lat1 = (decimal) lat1;
            this.chart.lat2 = (decimal) lat2;
            this.chart.lon1 = (decimal) lon1;
            this.chart.lon2 = (decimal) lon2;
            this.DrawChart();
        }

        private bool ShouldSerializeAxisLabelFont()
        {
            return !this.AxisLabelFont.Equals(new Font("Arial", 12f));
        }

        private bool ShouldSerializeChartBackColor()
        {
            return !this.ChartBackColor.Equals(Color.White);
        }

        private bool ShouldSerializeChartTitleFont()
        {
            return !this.ChartTitleFont.Equals(new Font("Arial", 16f));
        }

        private bool ShouldSerializeLabelsFont()
        {
            return !this.FieldLabelFont.Equals(new Font("Arial", 8f));
        }

        private bool ShouldSerializeKeyFont()
        {
            return !this.KeyItemsFont.Equals(new Font("Arial", 8f));
        }

        private bool ShouldSerializeScaleBreakBottom()
        {
            return (this.ScaleBreakBottom != 0M);
        }

        private bool ShouldSerializeScaleBreakTop()
        {
            return (this.ScaleBreakTop != 0M);
        }

        private bool ShouldSerializeValueLabelsFont()
        {
            return !this.ValueLabelsFont.Equals(new Font("Arial", 8f));
        }

        void ISupportInitialize.BeginInit()
        {
            this.Initializing = true;
            this.DataItems.Clear();
            this.ColorItems.Clear();
            this.KeyItems.Clear();
        }

        void ISupportInitialize.EndInit()
        {
            this.Initializing = false;
        }

        private void tryDataBinding()
        {
            if (base.BindingContext != null)
            {
                CurrencyManager manager;
                try
                {
                    manager = (CurrencyManager) base.BindingContext[this.DataSource, this.DataMember];
                }
                catch (ArgumentException)
                {
                    return;
                }
                if (this.dataManager != manager)
                {
                    if (this.dataManager != null)
                    {
                        this.dataManager.ListChanged-=(this.listChangedHandler);
                    }
                    this.dataManager = manager;
                    if (this.dataManager != null)
                    {
                        this.dataManager.ListChanged+=(this.listChangedHandler);
                    }
                }
                this.DrawChart();
            }
        }

        private void UpdateAllData()
        {
            List<string> columns = new List<string>();
            foreach (DataItem item in this.DataItems)
            {
                columns.Add(item.Name);
            }
            //if ((this.FieldCategory != null) && (this.FieldCategory != ""))
            //{
            //    columns.Add(this.FieldCategory);
            //}
            if ((this.FieldID != null) && (this.FieldID != ""))
            {
                columns.Add(this.FieldID);
            }
            if ((this.FieldLabel != null) && (this.FieldLabel != ""))
            {
                columns.Add(this.FieldLabel);
            }
            this.chart.data = ExtractTable.Extract(this.dataManager, columns);
        }

        [Description("Whether a the clicked element is automatically selected."), Browsable(true), Bindable(true), Category("Chart"), DefaultValue(true)]
        public bool AutoSelect
        {
            get
            {
                return this.chart.AutoPostBack;
            }
            set
            {
                this.chart.AutoPostBack = value;
            }
        }

        [Bindable(true), Category("Chart"), Browsable(true), Description("The font used for X & Y Axis labels.")]
        public Font AxisLabelFont
        {
            get
            {
                return this.Chart.AxisLabelFont;
            }
            set
            {
                this.Chart.AxisLabelFont = value;
                this.DrawChart();
            }
        }

        [Browsable(true), Description("Background Color."), NotifyParentProperty(true), Bindable(true), Category("Appearance")]
        public new Color BackColor
        {
            get
            {
                return this.Chart.BackColor;
            }
            set
            {
                this.Chart.BackColor = value;
                this.DrawChart();
            }
        }

        [Browsable(true), Description("Chart's background Color."), NotifyParentProperty(true), Bindable(true), Category("Appearance")]
        public Color ChartBackColor
        {
            get
            {
                return this.Chart.ChartBackColor;
            }
            set
            {
                this.Chart.ChartBackColor = value;
                this.DrawChart();
            }
        }

        [Description("How much darkening is applied to alternative grid rows. (valid range 0-255)"), NotifyParentProperty(true), Browsable(true), Bindable(true), Category("Appearance"), DefaultValue(20)]
        public int BackgroundDarkening
        {
            get
            {
                return this.chart.BackgroundDarkening;
            }
            set
            {
                if ((value >= 0) && (value <= 0xff))
                {
                    this.chart.BackgroundDarkening = value;
                }
                this.DrawChart();
            }
        }

        internal Charts.Chart Chart
        {
            get
            {
                if (this.chart == null)
                {
                    this.chart = new Charts.Chart();
                    this.chart.windowsControl = true;
                }
                if (!base.DesignMode && (this.chart.colectieHotspoturi == null))
                {
                    this.chart.colectieHotspoturi = new HotSpotCollection();
                }
                return this.chart;
            }
            set
            {
                this.chart = value;
            }
        }

        [Browsable(true), Category("Chart"), Description("The font used for the Tile"), Bindable(true)]
        public Font ChartTitleFont
        {
            get
            {
                return this.Chart.TitleFont;
            }
            set
            {
                this.Chart.TitleFont = value;
                this.DrawChart();
            }
        }

        [NotifyParentProperty(true), Browsable(true), Bindable(true), Category("Chart"), Description("The Colors used for the graph Sets."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorItemCollection ColorItems
        {
            get
            {
                return this.Chart.col;
            }
            set
            {
                this.Chart.col = value;
            }
        }

        [Category("Data"), Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), DefaultValue("")]
        public string DataMember
        {
            get
            {
                return this.dataMember;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if (this.dataMember != value)
                {
                    this.dataMember = value;
                    this.tryDataBinding();
                }
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Category("Chart"), Description("The names of the columns used as data fileds (this will be y values)."), NotifyParentProperty(true)]
        public DataItemCollection DataItems
        {
            get
            {
                return this.Chart.dataItems;
            }
            set
            {
                this.Chart.dataItems = value;
                this.DrawChart();
            }
        }

        [DefaultValue((string) null), Category("Data"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if (this.dataSource != value)
                {
                    this.dataSource = value;
                    this.tryDataBinding();
                }
            }
        }

        //[Description("Used only with Exploded3Ddonut modes. The valid range is (0-1); It controls how 'thick' is the donut."), Browsable(true), Bindable(true), Category("Chart"), DefaultValue((double) 0.3)]
        //public double DonutHole
        //{
        //    get
        //    {
        //        return this.Chart.donaughtHole;
        //    }
        //    set
        //    {
        //        if ((value >= 0.0) && (value <= 1.0))
        //        {
        //            this.Chart.donaughtHole = value;
        //        }
        //        this.DrawChart();
        //    }
        //}

        [Category("Chart"), Browsable(true), Bindable(false), Description("Whether the grid will be Drawn or not."), DefaultValue(true)]
        public bool DrawGrid
        {
            get
            {
                return this.chart.DrawGrid;
            }
            set
            {
                this.chart.DrawGrid = value;
                this.DrawChart();
            }
        }

        [Category("Chart"), Browsable(true), DefaultValue(true), Description("Whether the shadow will be Drawn or not."), Bindable(false)]
        public bool DrawShadow
        {
            get
            {
                return this.Chart.ShadowIsDrawn;
            }
            set
            {
                this.Chart.ShadowIsDrawn = value;
                this.DrawChart();
            }
        }

        [DefaultValue(0x9b), Bindable(true), Category("Chart"), Description("The transparency/opacity of the elements of the graph. The valid range is (0-255) where 0 is completly transparent and 255 is completly opaque."), Browsable(true)]
        public int ElementOpacity
        {
            get
            {
                return this.Chart.alpha;
            }
            set
            {
                if ((value >= 0) && (value <= 0xff))
                {
                    this.chart.alpha = value;
                }
                this.DrawChart();
            }
        }

        [Description("The valid range is 0-1. This controls how much space is left between the ellemets (bars, cylinders, etc). This will also influence the width of the element. Small numbers will make the elements very thin, while 1 will make the ellemnts as 'fat' as possible."), NotifyParentProperty(true), Browsable(true), Bindable(true), Category("Chart"), DefaultValue((double) 0.8)]
        public double ElementSpacing
        {
            get
            {
                return this.Chart.PercentWidth;
            }
            set
            {
                if ((value >= 0.0) && (value <= 1.0))
                {
                    this.Chart.PercentWidth = value;
                }
                this.DrawChart();
            }
        }

        [Bindable(true), Category("Chart"), DefaultValue((double)0.05), Description("Used only with PieExpanded3D modes. The valid range is (0-1). It controls how far apart are the slices."), Browsable(true)]
        public double SlicesOffset
        {
            get
            {
                return this.Chart.slicesOffset;
            }
            set
            {
                if ((value >= 0.0) && (value <= 1.0))
                {
                    this.Chart.slicesOffset = value;
                }
                this.DrawChart();
            }
        }

        [DefaultValue("Chart Title"), NotifyParentProperty(true), Bindable(true), Localizable(true), Category("Chart"), Description("Chart Title"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string GraphTitle
        {
            get
            {
                return this.Chart.Title;
            }
            set
            {
                this.Chart.Title = value;
            }
        }

        //[Localizable(true),DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor)), Bindable(true), Browsable(true), Category("Chart"), Description("The CategoryID Column of the table. Used for MapPoints and Map chart-types only. Based on the category the points/regions are color coded.")]
        //public string FieldCategory
        //{
        //    get
        //    {
        //        return this.chart.FieldCategory;
        //    }
        //    set
        //    {
        //        this.chart.FieldCategory = value;
        //    }
        //}

        [DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor)), Browsable(true), Bindable(true), Localizable(true), Category("Chart"), Description("The IDColumn Name of the table. Used for Map mode.")]
        public string FieldID
        {
            get
            {
                return this.Chart.FieldID;
            }
            set
            {
                this.Chart.FieldID = value;
                this.DrawChart();
            }
        }

        public bool Initialize
        {
            get
            {
                return this.Initializing;
            }
            set
            {
                this.Initializing = value;
            }
        }

        [Bindable(true), Category("Chart"), DefaultValue("label"), Description("This column is used for the small labels under the x axis, or for generating the key in 'pie-graphs'"), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor)), NotifyParentProperty(true), Browsable(true)]
        public string FieldLabel
        {
            get
            {
                return this.Chart.XAxisLabels;
            }
            set
            {
                this.Chart.XAxisLabels = value;
                this.DrawChart();
            }
        }

        [Bindable(true), Description("The font used for the small labels(ticks) of the axes."), Browsable(true), Category("Chart")]
        public Font FieldLabelFont
        {
            get
            {
                return this.Chart.FieldLabelFont;
            }
            set
            {
                this.Chart.FieldLabelFont = value;
                this.DrawChart();
            }
        }

        [NotifyParentProperty(true), Category("Chart"), DefaultValue("{0}"), Description("This is the format string for the small labels that apear on the x axis. This will also format the key if this is created from a table column."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true), Bindable(false)]
        public string LabelsFormatString
        {
            get
            {
                return this.chart.LabelsFormatString;
            }
            set
            {
                this.chart.LabelsFormatString = value;
                this.DrawChart();
            }
        }

        [Category("Chart"), Bindable(true), Browsable(true), Description("List of values from which the key will be generated. This is only valid for graph types that show more than one fileds. Theese strings should be the 'user-friendly' names of the columns set with 'DataItems'."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), Localizable(true)]
        public KeyItemCollection KeyItems
        {
            get
            {
                return this.Chart.KeyLabels;
            }
            set
            {
                this.Chart.KeyLabels = value;
                this.DrawChart();
            }
        }

        [Category("Chart"), Description("The font used for the key."), Browsable(true), Bindable(true)]
        public Font KeyItemsFont
        {
            get
            {
                return this.Chart.KeyFont;
            }
            set
            {
                this.Chart.KeyFont = value;
                this.DrawChart();
            }
        }

        [DefaultValue(0), NotifyParentProperty(true), Bindable(true), Category("Chart"), Browsable(true), Description("The type of highlight that will be used for the ellemnts of the graph")]
        public int LightEffect
        {
            get
            {
                return this.Chart.effect;
            }
            set
            {
                if (((this.Chart.type == ChartType.Pie ) || (this.Chart.type == ChartType.Pie3D )))// || (this.Chart.type == 13))
                {
                    this.Chart.effect = value % pieEffectsNo;
                }
                else
                {
                    this.Chart.effect = value % barEffectsNo;
                }
                this.DrawChart();
            }
        }

        [Description("How 'strong' the efect is. The valid range is (0-1) where 0 is completly transparent and 1 is completly opaque."), Bindable(true), DefaultValue((double) 0.1), NotifyParentProperty(true), Category("Chart"), Browsable(true)]
        public double LightEffectOpacity
        {
            get
            {
                return this.Chart.effectopacity;
            }
            set
            {
                if ((value >= 0.0) && (value <= 1.0))
                {
                    this.Chart.effectopacity = value;
                }
                this.DrawChart();
            }
        }

        [Description("Size of the square blocks that indicate a datapoint on Line Charts and MultiLine Charts"), Bindable(true), Category("Appearance"), DefaultValue(5), Browsable(true), NotifyParentProperty(true)]
        public int LineGraphsDataPointSize
        {
            get
            {
                return this.chart.LineGraphsDataPointSize;
            }
            set
            {
                if ((value >= 0) && (value <= 10))
                {
                    this.chart.LineGraphsDataPointSize = value;
                }
            }
        }

        [Description("Size of the line on Line Charts and MultiLine Charts"), Browsable(true), Category("Appearance"), DefaultValue(4), Bindable(true), NotifyParentProperty(true)]
        public int LineGraphsLineSize
        {
            get
            {
                return this.chart.LineGraphsLineSize;
            }
            set
            {
                if ((value >= 1) && (value <= 10))
                {
                    this.chart.LineGraphsLineSize = value;
                }
            }
        }

        //[Category("Chart"),DefaultValue(""), Description("The xml text that contains the map data. Used for Map modes."), Editor(typeof(MyXmlEditor), typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), NotifyParentProperty(true), Bindable(true), Localizable(true), Browsable(true)]
        //public string MapXML
        //{
        //    get
        //    {
        //        return this.Chart.mapXml;
        //    }
        //    set
        //    {
        //        this.Chart.mapXml = value;
        //        this.DrawChart();
        //    }
        //}

        [Category("Chart"), DefaultValue(null), NotifyParentProperty(true), Bindable(true), Browsable(true), Description("The Images used as points for MapPoints Chart Type."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ImageList PointImages
        {
            get
            {
                if (this.chart.pointImageList == null)
                {
                    this.chart.pointImageList = new ImageList();
                }
                return (ImageList) this.chart.pointImageList;
            }
            set
            {
                this.chart.pointImageList = value;
            }
        }

        [Description("Use this to set a scale Break."), Category("ScaleBreak"), Browsable(true)]
        public decimal ScaleBreakBottom
        {
            get
            {
                return this.chart.ScaleBreakMinV;
            }
            set
            {
                this.chart.ScaleBreakMinV = value;
                this.DrawChart();
            }
        }

        [Category("ScaleBreak"), DefaultValue(5), Description("Use this to set the scale Break height."), Browsable(true)]
        public int ScaleBreakSize
        {
            get
            {
                return this.chart.ScaleBreakHeightV;
            }
            set
            {
                this.chart.ScaleBreakHeightV = value;
                this.DrawChart();
            }
        }

        [DefaultValue(0), Description("Use this to set a scale Break."), Category("ScaleBreak"), Browsable(true)]
        public decimal ScaleBreakTop
        {
            get
            {
                return this.chart.ScaleBreakMaxV;
            }
            set
            {
                this.chart.ScaleBreakMaxV = value;
                this.DrawChart();
            }
        }

        [Category("Chart"), DefaultValue(5/*15*/), Description("This is the offest of the shadow. For most graphs both X and Y should be positive. For some types theese values are ignored."), Browsable(true), Bindable(true)]
        public int ShadowOffsetX
        {
            get
            {
                return this.Chart.realShadowOffsetX;
            }
            set
            {
                this.Chart.realShadowOffsetX = value;
                this.DrawChart();
            }
        }

        [Category("Chart"), DefaultValue(-15/*-25*/), Description("This is the offest of the shadow. For most graphs both X and Y should be positive. For some types theese values are ignored."), Bindable(true), Browsable(true)]
        public int ShadowOffsetY
        {
            get
            {
                return this.Chart.realShadowOffsetY;
            }
            set
            {
                this.Chart.realShadowOffsetY = value;
                this.DrawChart();
            }
        }

        [Category("Chart"), Bindable(true), DefaultValue(0), Browsable(true), Description("The Graph Type(Bars, Pie, Surface, etc).")]
        public ChartType ChartType
        {
            get
            {
                return (ChartType) this.Chart.type;
            }
            set
            {
                this.Chart.type = value;// (int)value;
                this.DrawChart();
            }
        }

        //[Category("Chart"), Localizable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), NotifyParentProperty(true), Description("Custom Image. Used for userBars mode and Maps."), Browsable(true), Bindable(true)]
        //public System.Drawing.Image CustomImage
        //{
        //    get
        //    {
        //        return (System.Drawing.Image) this.Chart.WinCustomImage;
        //    }
        //    set
        //    {
        //        this.Chart.WinCustomImage = value;
        //        this.DrawChart();
        //    }
        //}

        [Browsable(true), Bindable(true), Category("Chart"), Description("The font used for the value labels of the elements.")]
        public Font ValueLabelsFont
        {
            get
            {
                return this.chart.ValueLabelsFont;
            }
            set
            {
                this.chart.ValueLabelsFont = value;
                this.DrawChart();
            }
        }

        [DefaultValue(""), Description("This is the format string for the value labels. Leave blank if you do not want any value labels."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), NotifyParentProperty(true), Bindable(false), Category("Chart"), Browsable(true)]
        public string ValueLabelsFormatString
        {
            get
            {
                return this.chart.ValueLabelsFormatString;
            }
            set
            {
                this.chart.ValueLabelsFormatString = value;
                this.DrawChart();
            }
        }

        [Browsable(true), NotifyParentProperty(true), Bindable(true), Category("Chart"), DefaultValue("Axis Label X"), Description("X axis name. This will apear as a label below the x axis."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AxisLabelX
        {
            get
            {
                return this.Chart.XName;
            }
            set
            {
                this.Chart.XName = value;
                this.DrawChart();
            }
        }

        [DefaultValue("Axis Label Y"), Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), NotifyParentProperty(true), Description("Y axis name. This will apear as a label before the y axis."), Browsable(true), Bindable(true)]
        public string AxisLabelY
        {
            get
            {
                return this.Chart.YName;
            }
            set
            {
                this.Chart.YName = value;
                this.DrawChart();
            }
        }

        [Browsable(true), Bindable(true), Category("Chart"), DefaultValue((double) 0.5), NotifyParentProperty(true), Description("For the 3D types, this controls how 'deep' the elements are. For most types the valid ranges are 0(flat)-1, but for others (like the 3D pie) larger values are also valid.")]
        public double ZValue
        {
            get
            {
                return this.Chart.Percent3D;
            }
            set
            {
                this.Chart.Percent3D = value;
                this.DrawChart();
            }
        }

        public delegate void ChartClickEventHandler(object source, string id, string field, int position);

        //public enum ChartType
        //{
        //    Bars,
        //    Pie,
        //    Bars3D,
        //    Donut,
        //    Multibar,
        //    Line,
        //    MultiLine,
        //    StackedBars,
        //    StackedBarsFull,
        //    StackedBars3D,
        //    StackedBars3DFull,
        //    Multibars3D,
        //    Cylinders,
        //    Pie3D,
        //    Surface,
        //    MultiSurface,
        //    Map,
        //    ExplodedPie,
        //    RadarLine,
        //    RadarSurface,
        //    UserDrawnBars,
        //    MultiRadarLine,
        //    MultiRadarSurface,
        //    StackedRadarLine,
        //    StackedRadarSurface,
        //    StackedSurface,
        //    FullStackedSurface,
        //    Multi3DLine,
        //    MultiCurve,
        //    Exploded3DPie,
        //    Exploded3DDonut,
        //    MapPoints,
        //    Bars3DWide,
        //    StackedCylinders,
        //    FullStackedCylinders,
        //    Surface3D,
        //    MultiSurface3D,
        //    StackedSurface3D
        //}
    }
}

