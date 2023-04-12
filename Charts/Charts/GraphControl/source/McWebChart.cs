namespace MControl.Charts.Web
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.Design;
    using System.Web.UI.WebControls;
    
    [ToolboxBitmap(typeof(McWebChart),"Resources.graph.bmp")]
    [ParseChildren(true), PersistChildren(false), Designer(typeof(ChartDesigner)), DefaultProperty("GraphTitle"), ToolboxData("<{0}:McWebChart runat=server Height=480px Width=640px BackColor='#DDDDDD'></{0}:McWebChart>")]
    public class McWebChart : DataBoundControl
    {
        private static int barEffectsNo = 12;
        private bool forcedSave;
        private ImageMap img;
        private static int pieEffectsNo = 6;

        public event ChartClickEventHandler ChartClick;

        public void AddBackgroundStripe(BackgroundStripe stripe)
        {
            this.chart.BackgroundStripes.Add(stripe);
        }

        public void AddBackgroundStripe(decimal minValue, decimal maxValue, Color brushColor)
        {
            this.AddBackgroundStripe(new BackgroundStripe(minValue, maxValue, brushColor));
        }

        internal string GetNavigateUrl(string data)
        {
            if (this.Page == null)
            {
                return string.Empty;
            }
            return ("javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, data));
        }

        protected override void LoadViewState(object savedState)
        {
            this.chart = (Chart) savedState;
        }

        private string MapPath(string name)
        {
            IWebApplication service = (IWebApplication) this.Page.Site.GetService(typeof(IWebApplication));
            if (service == null)
            {
                return "Can not get IWebApplication at DesignTime";
            }
            IProjectItem projectItemFromUrl = service.GetProjectItemFromUrl(name);
            if (projectItemFromUrl != null)
            {
                return projectItemFromUrl.PhysicalPath;
            }
            return "URL not found in project";
        }

        private string Normalize(string s)
        {
            return s.Replace("\r\n", "");
        }

        public void NormalizeMapLocation()
        {
            decimal num = this.chart.lat2 - this.chart.lat1;
            decimal num2 = this.chart.lon2 - this.chart.lon1;
            if ((((decimal) this.Height.Value) / ((decimal) this.Width.Value)) > (num / num2))
            {
                num2 = num * (((decimal) this.Width.Value) / ((decimal) this.Height.Value));
                decimal num3 = (this.chart.lon1 + this.chart.lon2) / 2M;
                decimal num4 = num3 - (num2 / 2M);
                decimal num5 = num3 + (num2 / 2M);
                if ((num4 >= -180M) && (num5 <= 180M))
                {
                    this.chart.lon1 = num4;
                    this.chart.lon2 = num5;
                }
            }
            else if ((((decimal) this.Height.Value) / ((decimal) this.Width.Value)) < (num / num2))
            {
                num = num2 * (((decimal) this.Height.Value) / ((decimal) this.Width.Value));
                decimal num6 = (this.chart.lat1 + this.chart.lat2) / 2M;
                decimal num7 = num6 - (num / 2M);
                decimal num8 = num6 + (num / 2M);
                if ((num7 >= -90M) && (num8 <= 90M))
                {
                    this.chart.lat1 = num7;
                    this.chart.lat2 = num8;
                }
            }
        }

        private void OnDataSourceViewSelectCallback(IEnumerable retrievedData)
        {
            if (base.IsBoundUsingDataSourceID)
            {
                this.OnDataBinding(EventArgs.Empty);
            }
            this.PerformDataBinding(retrievedData);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Page.IsPostBack)
            {
                if (!base.DesignMode && (this.ViewState[this.ID + "chart"] != null))
                {
                    this.chart = (Chart) this.ViewState[this.ID + "chart"];
                }
                string str = this.Page.Request["__EVENTARGUMENT"];
                if (str != null)
                {
                    try
                    {
                        string[] strArray = str.Split(new char[] { '|' });
                        if (strArray.Length > 1)
                        {
                            this.ChartClick(this, strArray[0], strArray[1]);
                        }
                        else
                        {
                            this.ChartClick(this, strArray[0], this.chart.x[0].str);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void PerformDataBinding(IEnumerable retrievedData)
        {
            base.PerformDataBinding(retrievedData);
            List<string> columns = new List<string>();
            foreach (DataItem item in this.DataSeries)
            {
                columns.Add(item.str);
            }
            if ((this.IDCategory != null) && (this.IDCategory != ""))
            {
                columns.Add(this.IDCategory);
            }
            if ((this.IDField != null) && (this.IDField != ""))
            {
                columns.Add(this.IDField);
            }
            if ((this.Labels != null) && (this.Labels != ""))
            {
                columns.Add(this.Labels);
            }
            if (!base.DesignMode)
            {
                this.chart.data = ExtractTable.Extract(retrievedData, columns);
            }
        }

        protected override void PerformSelect()
        {
            if (!base.IsBoundUsingDataSourceID)
            {
                this.OnDataBinding(EventArgs.Empty);
            }
            this.GetData().Select(this.CreateDataSourceSelectArguments(), new DataSourceViewSelectCallback(this.OnDataSourceViewSelectCallback));
            base.RequiresDataBinding = false;
            base.MarkAsDataBound();
            this.OnDataBound(EventArgs.Empty);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            base.RenderContents(output);
            this.renderImage(output);
        }

        private string renderImage(HtmlTextWriter output)
        {
            string fullPath = string.Empty;
            try
            {
                if (this.chart.col.Count == 0)
                {
                    Color[] colors = new Color[] { Color.Red, Color.FromArgb(0, 0xff, 0), Color.Blue, Color.Yellow, Color.Purple, Color.Fuchsia };
                    this.setColors(colors);
                }
                if (base.DesignMode)
                {
                    this.chart.radacina = this.MapPath(".");
                    if (this.chart.x.Count == 0)
                    {
                        this.setDataSeries("values1,values2,values3");
                    }
                    DataTable table = new DataTable();
                    foreach (DataItem item in this.chart.x)
                    {
                        if (table.Columns[item.str] == null)
                        {
                            table.Columns.Add(item.str, typeof(int));
                        }
                    }
                    if (((this.chart.XAxisLabels != null) && (this.chart.XAxisLabels != string.Empty)) && (table.Columns[this.chart.XAxisLabels] == null))
                    {
                        table.Columns.Add(this.chart.XAxisLabels, typeof(string));
                    }
                    if (((this.chart.IDField != null) && (this.chart.IDField != string.Empty)) && (table.Columns[this.IDField] == null))
                    {
                        table.Columns.Add(this.IDField, typeof(string));
                    }
                    if (((this.chart.IDCategory != null) && (this.chart.IDCategory != string.Empty)) && (table.Columns[this.IDCategory] == null))
                    {
                        table.Columns.Add(this.IDCategory, typeof(string));
                    }
                    Random random = new Random();
                    if ((this.Type != ChartType.Map) && (this.Type != ChartType.MapPoints))
                    {
                        int num = 20;
                        for (int i = 0; i < num; i++)
                        {
                            DataRow row = table.NewRow();
                            foreach (DataItem item2 in this.chart.x)
                            {
                                row[item2.str] = random.Next(0x29a);
                            }
                            if ((this.chart.XAxisLabels != null) && (this.chart.XAxisLabels != string.Empty))
                            {
                                if (table.Columns[this.chart.XAxisLabels].DataType == typeof(string))
                                {
                                    if ((i % 2) == 0)
                                    {
                                        row[this.chart.XAxisLabels] = "Designer";
                                    }
                                    else
                                    {
                                        row[this.chart.XAxisLabels] = "Random";
                                    }
                                }
                                else
                                {
                                    row[this.chart.XAxisLabels] = random.Next(0x29a);
                                }
                            }
                            table.Rows.Add(row);
                        }
                    }
                    else if (this.Type == ChartType.MapPoints)
                    {
                        int num3 = 20;
                        string str2 = "abcdefghijklmnopqrstuvxyz";
                        for (int j = 0; j < num3; j++)
                        {
                            DataRow row2 = table.NewRow();
                            row2[this.chart.x[0].str] = random.Next(180) - 90;
                            row2[this.chart.x[1].str] = random.Next(360) - 180;
                            if ((this.chart.XAxisLabels != null) && (this.chart.XAxisLabels != string.Empty))
                            {
                                if (table.Columns[this.chart.XAxisLabels].DataType == typeof(string))
                                {
                                    row2[this.chart.XAxisLabels] = str2.Substring(random.Next(20), 4);
                                }
                                else
                                {
                                    row2[this.chart.XAxisLabels] = random.Next(0x29a);
                                }
                            }
                            if ((this.chart.IDCategory != null) && (this.chart.IDCategory != string.Empty))
                            {
                                row2[this.chart.IDCategory] = random.Next(3);
                            }
                            table.Rows.Add(row2);
                        }
                    }
                    else if (this.Type == ChartType.Map)
                    {
                        DataTable table2 = new DataTable("Element");
                        table2.Columns.Add("ID", typeof(string));
                        table2.Columns.Add("R", typeof(byte));
                        table2.Columns.Add("G", typeof(byte));
                        table2.Columns.Add("B", typeof(byte));
                        table2.Columns.Add("X", typeof(int));
                        table2.Columns.Add("Y", typeof(int));
                        table2.PrimaryKey = new DataColumn[] { table2.Columns["ID"] };
                        try
                        {
                            table2.ReadXml(this.MapPath(this.chart.mapXml));
                        }
                        catch
                        {
                            throw new Exception("ERROR: No XML file specified for the Map chart!");
                        }
                        foreach (DataRow row3 in table2.Rows)
                        {
                            DataRow row4 = table.NewRow();
                            foreach (DataItem item3 in this.chart.x)
                            {
                                row4[item3.str] = random.Next(0x14d);
                            }
                            if ((this.chart.XAxisLabels != null) && (this.chart.XAxisLabels != string.Empty))
                            {
                                row4[this.chart.XAxisLabels] = row3["ID"].ToString();
                            }
                            if ((this.chart.IDField != null) && (this.chart.IDField != string.Empty))
                            {
                                row4[this.chart.IDField] = row3["ID"].ToString();
                            }
                            if ((this.chart.IDCategory != null) && (this.chart.IDCategory != string.Empty))
                            {
                                row4[this.chart.IDCategory] = random.Next(3);
                            }
                            table.Rows.Add(row4);
                        }
                    }
                    this.chart.data = table;
                }
                if (((this.Type == ChartType.UserDrawnBars) || (this.Type == ChartType.Map)) || (this.Type == ChartType.MapPoints))
                {
                    try
                    {
                        if (base.DesignMode)
                        {
                            this.chart.userImagePath = this.MapPath(this.chart.userImage);
                        }
                        else
                        {
                            this.chart.userImagePath = HttpContext.Current.Server.MapPath(this.chart.userImage);
                        }
                    }
                    catch
                    {
                        throw new Exception("ERROR: No Image File specified for this type of chart!");
                    }
                }
                if ((this.Type == ChartType.Map) || (this.Type == ChartType.MapPoints))
                {
                    try
                    {
                        if (base.DesignMode)
                        {
                            this.chart.mapXMlPath = this.MapPath(this.chart.mapXml);
                        }
                        else
                        {
                            this.chart.mapXMlPath = HttpContext.Current.Server.MapPath(this.chart.mapXml);
                        }
                    }
                    catch
                    {
                        throw new Exception("ERROR: No XML File specified for this type of chart!");
                    }
                }
                if ((this.Type == ChartType.MapPoints) || (this.Type == ChartType.Line))
                {
                    this.chart.pointImagesReal.Clear();
                    if (base.DesignMode)
                    {
                        foreach (ImageItem item4 in this.chart.pointImages)
                        {
                            this.chart.pointImagesReal.Add(new ImageItem(this.MapPath(item4.Image)));
                        }
                    }
                    else
                    {
                        foreach (ImageItem item5 in this.chart.pointImages)
                        {
                            this.chart.pointImagesReal.Add(new ImageItem(HttpContext.Current.Server.MapPath(item5.Image)));
                        }
                    }
                }
                if (this.chart.data == null)
                {
                    this.chart.data = (DataTable) this.ViewState["data"];
                }
                this.chart.BackColor = this.BackColor;
                this.chart.ForeColor = this.ForeColor;
                this.chart.chartDataControl = this;
                Statistics.rezolve(this.chart.data, this.DataSeries, this.Keys);
                this.img = new ImageMap();
                this.img.Width = this.chart.width = (int) this.Width.Value;
                this.img.Height = this.chart.height = (int) this.Height.Value;
                this.img.ID = this.ClientID;
                this.img.ImageUrl = this.chart.saveImage(out fullPath);
                this.chart.chartDataControl = null;
                this.img.HotSpotMode = HotSpotMode.Navigate;
                if (!base.DesignMode && (this.Page.Session[this.img.ImageUrl] != null))
                {
                    foreach (HotSpot spot in (HotSpotCollection) this.Page.Session[this.img.ImageUrl])
                    {
                        if (this.AutoPostBack)
                        {
                            spot.NavigateUrl = this.GetNavigateUrl(spot.PostBackValue);
                        }
                        else
                        {
                            spot.NavigateUrl = "javascript: ;";
                        }
                        this.img.HotSpots.Add(spot);
                    }
                    this.Page.Session[this.img.ImageUrl] = null;
                }
                if (output == null)
                {
                    return fullPath;
                }
                if (!this.UseFlash || (HttpContext.Current == null))
                {
                    this.img.RenderControl(output);
                    return fullPath;
                }
                output.Write(FlashChart.GetObject(Convert.ToInt32(this.Width.Value), Convert.ToInt32(this.Height.Value), this.ID, this.Page.ClientScript.GetWebResourceUrl(typeof(McWebChart), "Resources.TestChart.swf"), this.img.ImageUrl));
            }
            catch (Exception exception)
            {
                if (output != null)
                {
                    output.WriteLine(exception.Message);
                    output.WriteLine(exception.Source);
                    output.WriteLine(exception.StackTrace);
                }
            }
            return fullPath;
        }

        public string SaveChartImage()
        {
            this.forcedSave = true;
            return this.renderImage(null);
        }

        protected override object SaveViewState()
        {
            if (this.forcedSave)
            {
                return null;
            }
            return this.chart;
        }

        public void setColors(Color[] colors)
        {
            this.chart.col.Clear();
            foreach (Color color in colors)
            {
                this.chart.col.Add(new ColorItem(color));
            }
        }

        public void setDataSeries(string comaSeparatedFields)
        {
            this.chart.x.Clear();
            string[] fields = comaSeparatedFields.Split(new char[] { ',' });
            this.setDataSeries(fields);
        }

        public void setDataSeries(string[] fields)
        {
            this.chart.x.Clear();
            foreach (string str in fields)
            {
                this.chart.x.Add(new DataItem(str));
            }
        }

        public void setKey(string[] items)
        {
            this.chart.KeyLabels.Clear();
            foreach (string str in items)
            {
                this.chart.KeyLabels.Add(new KeyItem(str));
            }
        }

        public void setKey(string comaSeparatedItems)
        {
            string[] items = comaSeparatedItems.Split(new char[] { ',' });
            this.setKey(items);
        }

        public void setMapLocation(double lat1, double lat2, double lon1, double lon2)
        {
            if (((lat1 < -90.0) || (lat2 < -90.0)) || ((lat1 > 90.0) || (lat2 > 90.0)))
            {
                throw new ChartControlException(new Exception("Latitude is out of range (-90;+90)"));
            }
            if (((lon1 < -180.0) || (lon2 < -180.0)) || ((lon1 > 180.0) || (lon2 > 180.0)))
            {
                throw new ChartControlException(new Exception("Longitude is out of range (-180;+180)"));
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
        }

        public void setPointImages(string[] paths)
        {
            this.chart.pointImages.Clear();
            foreach (string str in paths)
            {
                this.chart.pointImages.Add(new ImageItem(str));
            }
        }

        private bool ShouldSerializeAxisLabelFont()
        {
            return !this.AxisLabelFont.Equals(new Font("Verdana", 14f));
        }

        private bool ShouldSerializeBackColor2()
        {
            return !this.BackColor2.Equals(Color.White);
        }

        private bool ShouldSerializeChartTitleFont()
        {
            return !this.ChartTitleFont.Equals(new Font("Verdana", 20f));
        }

        private bool ShouldSerializeLabelsFont()
        {
            return !this.LabelsFont.Equals(new Font("Verdana", 8f));
        }

        private bool ShouldSerializeKeyFont()
        {
            return !this.KeyFont.Equals(new Font("Verdana", 11f));
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
            return !this.ValueLabelsFont.Equals(new Font("Verdana", 8f));
        }

        [DefaultValue(true), Description("Whether a postback is raised when the user clicks on a chart element."), Browsable(true), Bindable(true), Category("Chart")]
        public bool AutoPostBack
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

        [Description("The font used for X & Y Axis labels."), Browsable(true), Bindable(true), Category("Chart")]
        public Font AxisLabelFont
        {
            get
            {
                return this.chart.AxisLabelFont;
            }
            set
            {
                this.chart.AxisLabelFont = value;
            }
        }

        [Browsable(true), NotifyParentProperty(true), Bindable(true), Category("Appearance"), Description("Chart's background Color.")]
        public Color BackColor2
        {
            get
            {
                return this.chart.BackColor2;
            }
            set
            {
                this.chart.BackColor2 = value;
            }
        }

        [DefaultValue(20), Category("Appearance"), Description("How much darkening is applied to alternative grid rows. (valid range 0-255)"), NotifyParentProperty(true), Browsable(true), Bindable(true)]
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
            }
        }

        [Description("How long will it take for the startup animation of the background to complete."), Browsable(true), Category("Flash"), DefaultValue(1)]
        public int BackgroundStartupAnimationDuration
        {
            get
            {
                return this.chart.flash.BackgroundStartupAnimationDuration;
            }
            set
            {
                this.chart.flash.BackgroundStartupAnimationDuration = value;
            }
        }

        [NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true), Bindable(true), Category("Chart"), DefaultValue("TempGraph"), Description("Cache directory for generated images. Gets deleted every minute."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CacheDirectory
        {
            get
            {
                return this.Normalize(this.chart.ImageFolder);
            }
            set
            {
                if ((value != null) && (value != string.Empty))
                {
                    this.chart.ImageFolder = this.Normalize(value);
                }
                else
                {
                    this.chart.ImageFolder = "TempGraph";
                }
            }
        }

        internal Chart chart
        {
            get
            {
                if (this.ViewState[this.ID + "chart"] == null)
                {
                    this.ViewState.Add(this.ID + "chart", new Chart());
                }
                return (Chart) this.ViewState[this.ID + "chart"];
            }
            set
            {
                this.ViewState[this.ID + "chart"] = value;
            }
        }

        [Description("The font used for the Tile"), Browsable(true), Bindable(true), Category("Chart")]
        public Font ChartTitleFont
        {
            get
            {
                return this.chart.TitleFont;
            }
            set
            {
                this.chart.TitleFont = value;
            }
        }

        //[PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), NotifyParentProperty(true), Description("The Colors used for the graph sets."), Category("Chart"), Browsable(true), Bindable(true)]
        public Colors Colors
        {
            get
            {
                return this.chart.col;
            }
        }

        [Description("The names of the columns used as data series (this will be y values)."), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), PersistenceMode(PersistenceMode.InnerProperty), NotifyParentProperty(true), Category("Chart"), Browsable(true)]
        public DataItems DataSeries
        {
            get
            {
                return this.chart.x;
            }
        }

        [Category("Chart"), Bindable(true), Description("Used only with Exploded3Ddonut modes. The valid range is (0-1); It controls how 'thick' is the donut."), DefaultValue((double) 0.3), Browsable(true)]
        public double DonutHole
        {
            get
            {
                return this.chart.donaughtHole;
            }
            set
            {
                if ((value >= 0.0) && (value <= 1.0))
                {
                    this.chart.donaughtHole = value;
                }
            }
        }

        [Category("Chart"), DefaultValue(true), Description("Whether the grid will be drawn or not."), Bindable(false), Browsable(true)]
        public bool DrawGrid
        {
            get
            {
                return this.chart.DrawGrid;
            }
            set
            {
                this.chart.DrawGrid = value;
            }
        }

        [Browsable(true), Category("Chart"), DefaultValue(true), Description("Whether the shadow will be drawn or not."), Bindable(true)]
        public bool DrawShadow
        {
            get
            {
                return this.chart.ShadowIsDrawn;
            }
            set
            {
                this.chart.ShadowIsDrawn = value;
            }
        }

        [DefaultValue(0x9b), Bindable(true), Description("The transparency/opacity of the elements of the graph. The valid range is (0-255) where 0 is completly transparent and 255 is completly opaque."), Category("Chart"), Browsable(true)]
        public int ElementOpacity
        {
            get
            {
                return this.chart.alpha;
            }
            set
            {
                if ((value >= 0) && (value <= 0xff))
                {
                    this.chart.alpha = value;
                }
            }
        }

        [Browsable(true), Description("How long will it take for the MouseOver animation of the elements to complete."), Category("Flash"), DefaultValue(1)]
        public int ElementsMouseOverAnimationDuration
        {
            get
            {
                return this.chart.flash.ElementsMouseOverAnimationDuration;
            }
            set
            {
                this.chart.flash.ElementsMouseOverAnimationDuration = value;
            }
        }

        [Category("Flash"), Browsable(true), Description("The Soft method for the animation of the elements ehen the mouse is over them (easein, easeout etc)."), DefaultValue(2)]
        public AnimationSoft ElementsMouseOverAnimationSoft
        {
            get
            {
                return this.chart.flash.ElementsMouseOverAnimationSoft;
            }
            set
            {
                this.chart.flash.ElementsMouseOverAnimationSoft = value;
            }
        }

        [Browsable(true), Description("The different modes in which an animation type for an element can be done (regular, ellastic, back, bounce etc)."), DefaultValue(4), Category("Flash")]
        public AnimationMode ElementsMouseOverAnimationMode
        {
            get
            {
                return this.chart.flash.ElementsMouseOverAnimationMode;
            }
            set
            {
                this.chart.flash.ElementsMouseOverAnimationMode = value;
            }
        }

        [Category("Flash"), Browsable(true), DefaultValue(5), Description("What kind of animation will elements have when the mouse is over them.")]
        public AnimationType ElementsMouseOverAnimationType
        {
            get
            {
                return this.chart.flash.ElementsMouseOverAnimationType;
            }
            set
            {
                this.chart.flash.ElementsMouseOverAnimationType = value;
            }
        }

        [Description("The Amplitude of the animation. If the animationType is a movement type how much the element will move; if it is a transparency change how much the Alpha of the color will change."), DefaultValue(-30), Browsable(true), Category("Flash")]
        public int ElementsMouseOverAnimationValueChange
        {
            get
            {
                return this.chart.flash.ElementsMouseOverAnimationValueChange;
            }
            set
            {
                this.chart.flash.ElementsMouseOverAnimationValueChange = value;
            }
        }

        [Browsable(true), Bindable(true), Category("Chart"), Description("The valid range is 0-1. This controls how much space is left between the ellemets (bars, cylinders, etc). This will also influence the width of the element. Small numbers will make the elements very thin, while 1 will make the ellemnts as 'fat' as possible."), NotifyParentProperty(true), DefaultValue((double) 0.8)]
        public double ElementSpacing
        {
            get
            {
                return this.chart.PercentWidth;
            }
            set
            {
                if ((value >= 0.0) && (value <= 1.0))
                {
                    this.chart.PercentWidth = value;
                }
            }
        }

        [Description("How long will it take for the startup animation of the elements to complete."), Browsable(true), DefaultValue(3), Category("Flash")]
        public int ElementsStartupAnimationDuration
        {
            get
            {
                return this.chart.flash.ElementsStartupAnimationDuration;
            }
            set
            {
                this.chart.flash.ElementsStartupAnimationDuration = value;
            }
        }

        [Browsable(true), DefaultValue(1), Description("The Soft method for the animation of the elements ehen they apppear on chart (easein, easeout etc)."), Category("Flash")]
        public AnimationSoft ElementsStartupAnimationSoft
        {
            get
            {
                return this.chart.flash.ElementsStartupAnimationSoft;
            }
            set
            {
                this.chart.flash.ElementsStartupAnimationSoft = value;
            }
        }

        [Browsable(true), Category("Flash"), DefaultValue(2), Description("The different modes in which an animation type for an element can be done (regular, ellastic, back, bounce etc).")]
        public AnimationMode ElementsStartupAnimationMode
        {
            get
            {
                return this.chart.flash.ElementsStartupAnimationMode;
            }
            set
            {
                this.chart.flash.ElementsStartupAnimationMode = value;
            }
        }

        [DefaultValue(1), Description("The Animation Order (all at once or one by one) of the elements when they first appear."), Category("Flash"), Browsable(true)]
        public AnimationOrder ElementsStartupAnimationOrder
        {
            get
            {
                return this.chart.flash.ElementsStartupAnimationOrder;
            }
            set
            {
                this.chart.flash.ElementsStartupAnimationOrder = value;
            }
        }

        [DefaultValue(5), Category("Flash"), Browsable(true), Description("What kind of animation will elements have when they appear on chart.")]
        public AnimationType ElementsStartupAnimationType
        {
            get
            {
                return this.chart.flash.ElementsStartupAnimationType;
            }
            set
            {
                this.chart.flash.ElementsStartupAnimationType = value;
            }
        }

        [Description("Used only with Exploded3DPie and Exploded3Ddonut modes. The valid range is (0-1). It controls how far apart are the slices."), DefaultValue((double) 0.2), Bindable(true), Browsable(true), Category("Chart")]
        public double Explosion
        {
            get
            {
                return this.chart.explosion;
            }
            set
            {
                if ((value >= 0.0) && (value <= 1.0))
                {
                    this.chart.explosion = value;
                }
            }
        }

        [NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true), Bindable(true), Localizable(true), Category("Chart"), Description("Chart Title"), DefaultValue("Chart Title"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string GraphTitle
        {
            get
            {
                return this.Normalize(this.chart.Title);
            }
            set
            {
                this.chart.Title = this.Normalize(value);
            }
        }

        [Description("The CategoryID Column of the table. Used for MapPoints and Map chart-types only. Based on the category the points/regions are color coded."), TypeConverter(typeof(DataFieldConverter)), Bindable(true), Localizable(true), Category("Chart"), Browsable(true)]
        public string IDCategory
        {
            get
            {
                return this.Normalize(this.chart.IDCategory);
            }
            set
            {
                this.chart.IDCategory = this.Normalize(value);
            }
        }

        [Browsable(true), Bindable(true), Localizable(true), Category("Chart"), Description("The IDColumn Name of the table. Used for Map mode."), TypeConverter(typeof(DataFieldConverter))]
        public string IDField
        {
            get
            {
                return this.Normalize(this.chart.IDField);
            }
            set
            {
                this.chart.IDField = this.Normalize(value);
            }
        }

        [Browsable(true), Category("Chart"), DefaultValue("labels"), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), PersistenceMode(PersistenceMode.InnerProperty), NotifyParentProperty(true), TypeConverter(typeof(DataFieldConverter)), Description("This column is used for the small labels under the x axis, or for generating the key in 'pie-graphs'")]
        public string Labels
        {
            get
            {
                return this.Normalize(this.chart.XAxisLabels);
            }
            set
            {
                this.chart.XAxisLabels = this.Normalize(value);
            }
        }

        [Description("The font used for the small labels(ticks) of the axes."), Category("Chart"), Browsable(true), Bindable(true)]
        public Font LabelsFont
        {
            get
            {
                return this.chart.LabelsFont;
            }
            set
            {
                this.chart.LabelsFont = value;
            }
        }

        [NotifyParentProperty(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true), Bindable(false), Category("Chart"), DefaultValue("{0}"), Description("This is the format string for the small labels that apear on the x axis. This will also format the key if this is created from a table column.")]
        public string LabelsFormatString
        {
            get
            {
                return this.Normalize(this.chart.LabelsFormatString);
            }
            set
            {
                this.chart.LabelsFormatString = this.Normalize(value);
            }
        }

       // [NotifyParentProperty(true), Browsable(true), Bindable(true), Localizable(true), Category("Chart"), Description("List of values from which the key will be generated. This is only valid for graph types that show more than one series. Theese strings should be the 'user-friendly' names of the columns set with 'DataSeries'."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), PersistenceMode(PersistenceMode.InnerProperty)]
        public KeyItems Keys
        {
            get
            {
                return this.chart.KeyLabels;
            }
        }

        [Category("Chart"), Bindable(true), Description("The font used for the key."), Browsable(true)]
        public Font KeyFont
        {
            get
            {
                return this.chart.KeyFont;
            }
            set
            {
                this.chart.KeyFont = value;
            }
        }

        [Description("What kind of animation will key elements have when they appear on chart."), Browsable(true), Category("Flash"), DefaultValue(5)]
        public AnimationType KeyStartAnimationType
        {
            get
            {
                return this.chart.flash.KeyStartupAnimationType;
            }
            set
            {
                this.chart.flash.KeyStartupAnimationType = value;
            }
        }

        [Browsable(true), Description("How long will it take for the startup animation of the key elements to complete. Measured in frames(1/30 seconds)."), Category("Flash"), DefaultValue(3)]
        public int KeyStartupAnimationDuration
        {
            get
            {
                return this.chart.flash.KeyStartupAnimationDuration;
            }
            set
            {
                this.chart.flash.KeyStartupAnimationDuration = value;
            }
        }

        [Browsable(true), Description("The Soft method for the animation of the key elements when they apppear on chart (easein, easeout etc)."), Category("Flash"), DefaultValue(1)]
        public AnimationSoft KeyStartupAnimationSoft
        {
            get
            {
                return this.chart.flash.KeyStartupAnimationSoft;
            }
            set
            {
                this.chart.flash.KeyStartupAnimationSoft = value;
            }
        }

        [Category("Flash"), Browsable(true), Description("The different modes in which an animation type for a key element can be done (regular, ellastic, back, bounce etc)."), DefaultValue(2)]
        public AnimationMode KeyStartupAnimationMode
        {
            get
            {
                return this.chart.flash.KeyStartupAnimationMode;
            }
            set
            {
                this.chart.flash.KeyStartupAnimationMode = value;
            }
        }

        [Description("The Animation Order (all at once or one by one) of the key items when they first appear."), Browsable(true), Category("Flash"), DefaultValue(1)]
        public AnimationOrder KeyStartupAnimationOrder
        {
            get
            {
                return this.chart.flash.KeyStartupAnimationOrder;
            }
            set
            {
                this.chart.flash.KeyStartupAnimationOrder = value;
            }
        }

        [Category("Chart"), Description("The type of highlight that will be used for the ellemnts of the graph"), Bindable(true), Browsable(true), DefaultValue(0), NotifyParentProperty(true)]
        public int LightEffect
        {
            get
            {
                return this.chart.effect;
            }
            set
            {
                if (((this.chart.type == 1) || (this.chart.type == 3)) || (this.chart.type == 13))
                {
                    this.chart.effect = value % pieEffectsNo;
                }
                else
                {
                    this.chart.effect = value % barEffectsNo;
                }
            }
        }

        [DefaultValue((double) 0.1), Description("How 'strong' the efect is. The valid range is (0-1) where 0 is completly transparent and 1 is completly opaque."), Bindable(true), Category("Chart"), Browsable(true), NotifyParentProperty(true)]
        public double LightEffectOpacity
        {
            get
            {
                return this.chart.effectopacity;
            }
            set
            {
                if ((value >= 0.0) && (value <= 1.0))
                {
                    this.chart.effectopacity = value;
                }
            }
        }

        [DefaultValue(5), Browsable(true), Bindable(true), Category("Appearance"), Description("Size of the square blocks that indicate a datapoint on Line Charts and MultiLine Charts"), NotifyParentProperty(true)]
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

        [Browsable(true), NotifyParentProperty(true), Bindable(true), Category("Appearance"), DefaultValue(4), Description("Size of the line on Line Charts and MultiLine Charts")]
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

        [Localizable(true), Bindable(true), Browsable(true), Category("Chart"), Description("The xml file that contains the map data. Used for Map mode."), Editor(typeof(UrlEditor), typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), PersistenceMode(PersistenceMode.InnerProperty), NotifyParentProperty(true)]
        public string MapXML
        {
            get
            {
                return this.Normalize(this.chart.mapXml);
            }
            set
            {
                this.chart.mapXml = this.Normalize(value);
            }
        }

        [Bindable(true), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true), NotifyParentProperty(true), Category("Chart"), Description("The Images used as points for MapPoints Chart Type."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ImageItems PointImages
        {
            get
            {
                return this.chart.pointImages;
            }
        }

        [Category("ScaleBreak"), Description("Use this to set a scale Break."), Browsable(true)]
        public decimal ScaleBreakBottom
        {
            get
            {
                return this.chart.ScaleBreakMinV;
            }
            set
            {
                this.chart.ScaleBreakMinV = value;
            }
        }

        [Description("Use this to set the scale Break height."), Browsable(true), Category("ScaleBreak"), DefaultValue(5)]
        public int ScaleBreakSize
        {
            get
            {
                return this.chart.ScaleBreakHeightV;
            }
            set
            {
                this.chart.ScaleBreakHeightV = value;
            }
        }

        [Description("Use this to set a scale Break."), Browsable(true), Category("ScaleBreak")]
        public decimal ScaleBreakTop
        {
            get
            {
                return this.chart.ScaleBreakMaxV;
            }
            set
            {
                this.chart.ScaleBreakMaxV = value;
            }
        }

        [Category("Chart"), Browsable(true), Description("This is the offest of the shadow. For most graphs both X and Y should be positive. For some types theese values are ignored."), Bindable(true), DefaultValue(15)]
        public int ShadowOffsetX
        {
            get
            {
                return this.chart.realShadowOffsetX;
            }
            set
            {
                this.chart.realShadowOffsetX = value;
            }
        }

        [Category("Chart"), DefaultValue(-25), Description("This is the offest of the shadow. For most graphs both X and Y should be positive. For some types theese values are ignored."), Bindable(true), Browsable(true)]
        public int ShadowOffsetY
        {
            get
            {
                return this.chart.realShadowOffsetY;
            }
            set
            {
                this.chart.realShadowOffsetY = value;
            }
        }

        [Description("If the recursion equations will be shown."), DefaultValue(false), Browsable(true), Category("Chart")]
        public bool ShowRecursionEquations
        {
            get
            {
                return this.chart.ShowRecursionEquations;
            }
            set
            {
                this.chart.ShowRecursionEquations = value;
            }
        }

        [Description("What kind of animation will title have when it appears on chart."), Browsable(true), Category("Flash"), DefaultValue(5)]
        public AnimationType TitleStartAnimationType
        {
            get
            {
                return this.chart.flash.TitleStartupAnimationType;
            }
            set
            {
                this.chart.flash.TitleStartupAnimationType = value;
            }
        }

        [DefaultValue(3), Description("How long will it take for the startup animation of the title to complete. Measured in frames(1/30 seconds)."), Category("Flash"), Browsable(true)]
        public int TitleStartupAnimationDuration
        {
            get
            {
                return this.chart.flash.TitleStartupAnimationDuration;
            }
            set
            {
                this.chart.flash.TitleStartupAnimationDuration = value;
            }
        }

        [Browsable(true), Description("The Soft method for the animation of the title when it apppears on chart (easein, easeout etc)."), Category("Flash"), DefaultValue(1)]
        public AnimationSoft TitleStartupAnimationSoft
        {
            get
            {
                return this.chart.flash.TitleStartupAnimationSoft;
            }
            set
            {
                this.chart.flash.TitleStartupAnimationSoft = value;
            }
        }

        [Category("Flash"), Browsable(true), Description("The different modes in which an animation type for the title can be done (regular, ellastic, back, bounce etc)."), DefaultValue(2)]
        public AnimationMode TitleStartupAnimationMode
        {
            get
            {
                return this.chart.flash.TitleStartupAnimationMode;
            }
            set
            {
                this.chart.flash.TitleStartupAnimationMode = value;
            }
        }

        [Category("Chart"), Browsable(true), Bindable(true), Description("The Graph Type(Bars, Pie, StackedBars, etc)."), DefaultValue(0)]
        public ChartType Type
        {
            get
            {
                return (ChartType) this.chart.type;
            }
            set
            {
                this.chart.type = (int) value;
                this.LightEffect = this.LightEffect;
            }
        }

        [Browsable(true), Description("Whether the chart will be created by using flash instead of a static image."), Category("Flash"), DefaultValue(false)]
        public bool UseFlash
        {
            get
            {
                return this.chart.flash.UseFlash;
            }
            set
            {
                this.chart.flash.UseFlash = value;
            }
        }

        [Description("User Image. Used for userDrawnBars mode."), PersistenceMode(PersistenceMode.InnerProperty), NotifyParentProperty(true), Bindable(true), Localizable(true), Category("Chart"), Browsable(true), Editor(typeof(UrlEditor), typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserImage
        {
            get
            {
                return this.Normalize(this.chart.userImage);
            }
            set
            {
                this.chart.userImage = this.Normalize(value);
            }
        }

        [Bindable(true), Description("The font used for the value labels of the elements."), Category("Chart"), Browsable(true)]
        public Font ValueLabelsFont
        {
            get
            {
                return this.chart.ValueLabelsFont;
            }
            set
            {
                this.chart.ValueLabelsFont = value;
            }
        }

        [NotifyParentProperty(true), Category("Chart"), Browsable(true), Bindable(false), DefaultValue(""), Description("This is the format string for the value labels. Leave blank if you do not want any value labels."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), PersistenceMode(PersistenceMode.InnerProperty)]
        public string ValueLabelsFormatString
        {
            get
            {
                return this.Normalize(this.chart.ValueLabelsFormatString);
            }
            set
            {
                this.chart.ValueLabelsFormatString = this.Normalize(value);
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), Bindable(true), Category("Chart"), DefaultValue("X Axis Label"), Description("X axis name. This will apear as a label below the x axis."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true), NotifyParentProperty(true)]
        public string XAxisLabel
        {
            get
            {
                return this.Normalize(this.chart.XName);
            }
            set
            {
                this.chart.XName = this.Normalize(value);
            }
        }

        [Category("Chart"), Bindable(true), Browsable(true), DefaultValue("Y Axis Label"), Description("Y axis name. This will apear as a label before the y axis."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), PersistenceMode(PersistenceMode.InnerProperty), NotifyParentProperty(true)]
        public string YAxisLabel
        {
            get
            {
                return this.Normalize(this.chart.YName);
            }
            set
            {
                this.chart.YName = this.Normalize(value);
            }
        }

        [Category("Chart"), Bindable(true), Browsable(true), Description("For the 3D types, this controls how 'deep' the elements are. For most types the valid ranges are 0(flat)-1, but for others (like the 3D pie) larger values are also valid."), DefaultValue((double) 0.5), NotifyParentProperty(true)]
        public double ZValue
        {
            get
            {
                return this.chart.Percent3D;
            }
            set
            {
                this.chart.Percent3D = value;
            }
        }

        public delegate void ChartClickEventHandler(object source, string id, string field);

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
        //    MultiCylinders,
        //    Surface3D,
        //    MultiSurface3D,
        //    StackedSurface3D
        //}
    }
}

