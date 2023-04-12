namespace MControl.Charts
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Windows.Forms;
    using MControl.Charts.Web;

    [Serializable]
    public class Chart
    {
        public int alpha = 0x9b;
        public bool AutoPostBack = true;
        public Font AxisLabelFont = new Font("Verdana", 14f);
        public Color BackColor = Color.White;
        public Color BackColor2 = Color.White;
        public int BackgroundDarkening = 20;
        internal List<BackgroundStripe> BackgroundStripes = new List<BackgroundStripe>();
        internal object chartDataControl;
        public List<ColorItem> col = new List<ColorItem>();
        internal object colectieHotspoturi;
        public DataTable data;
        public double donaughtHole = 0.3;
        public bool DrawGrid = true;
        public int effect;
        public double effectopacity = 0.1;
        public double explosion = 0.2;
        internal FlashChart flash = new FlashChart();
        public Color ForeColor = Color.White;
        private int graphHeight;
        private int graphstartsY;
        private int graphWidth;
        public int height;
        public string IDCategory = string.Empty;
        public string IDField = string.Empty;
        public string ImageFolder = "TempGraph";
        public Font LabelsFont = new Font("Verdana", 8f);
        public string LabelsFormatString = "{0}";
        internal decimal lat1 = -90M;
        internal decimal lat2 = 90M;
        public Font KeyFont = new Font("Verdana", 11f);
        public List<KeyItem> KeyLabels = new List<KeyItem>();
        //private static string LicenseText = "Not for production environment";
        //internal static string licenta;
        public int LineGraphsDataPointSize = 5;
        public int LineGraphsLineSize = 4;
        internal decimal lon1 = -180M;
        internal decimal lon2 = 180M;
        public string mapXml = string.Empty;
        public string mapXMlPath = string.Empty;
        private int negativeOffset;
        private int offsetYBottom;
        public double Percent3D = 0.5;
        public double PercentWidth = 0.8;
        internal object pointImageList;
        public List<ImageItem> pointImages = new List<ImageItem>();
        internal List<ImageItem> pointImagesReal = new List<ImageItem>();
        public string radacina;
        public int realShadowOffsetX = 15;
        public int realShadowOffsetY = -25;
        private string rez;
        private int ScaleBreakHeight;
        public int ScaleBreakHeightV = 5;
        private decimal ScaleBreakMax;
        public decimal ScaleBreakMaxV;
        private decimal ScaleBreakMin;
        public decimal ScaleBreakMinV;
        private int ScaleBreakPos = 0x7fffffff;
        public bool ShadowIsDrawn = true;
        private Point ShadowOffset = new Point(15, -25);
        public bool ShowRecursionEquations;
        private SmoothingMode smoothingMode = SmoothingMode.HighQuality;
        public string Title = "Chart Title";
        public Font TitleFont = new Font("Verdana", 20f);
        public int type;
        public string userImage = string.Empty;
        public string userImagePath = string.Empty;
        public Font ValueLabelsFont = new Font("Verdana", 8f);
        public string ValueLabelsFormatString = string.Empty;
        private const int whiteTransparency = 200;
        public int width;
        internal bool windowsControl;
        internal object WinUserImage;
        public List<DataItem> x = new List<DataItem>();
        public string XAxisLabels = "labels";
        private XmlChart xmlChart;
        public string XName = "X Axis Label";
        public string YName = "Y Axis Label";

        //private bool checkLicense()
        //{
        //    if (HttpContext.Current != null)
        //    {
        //        return this.checkWebLicense();
        //    }
        //    return ((this.colectieHotspoturi == null) || this.checkWindowsLicense());
        //}

        //private bool checkUniversalKey(string s)
        //{
        //    int num;
        //    string[] strArray = s.Split(new char[] { '-' });
        //    if (strArray.Length != 3)
        //    {
        //        return false;
        //    }
        //    if (this.windowsControl)
        //    {
        //        num = 0xca;
        //    }
        //    else
        //    {
        //        num = 0xcf;
        //    }
        //    int num2 = 1;
        //    foreach (string str in strArray)
        //    {
        //        int num3 = int.Parse(str);
        //        if ((num3 % ((num2 * 0x29a) + num)) != 0)
        //        {
        //            return false;
        //        }
        //        if (num3.ToString().Length < 3)
        //        {
        //            return false;
        //        }
        //        num2++;
        //    }
        //    return true;
        //}

        //private bool checkWebLicense()
        //{
        //    string s = string.Empty;
        //    if (HttpContext.Current.Application["ChartControlLicense"] != null)
        //    {
        //        s = (string) HttpContext.Current.Application["ChartControlLicense"];
        //    }
        //    else
        //    {
        //        FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath("~/Charts.lic"));
        //        if (info.Exists)
        //        {
        //            s = File.ReadAllText(info.FullName);
        //            HttpContext.Current.Application["ChartControlLicense"] = s;
        //        }
        //    }
        //    return this.checkUniversalKey(s);
        //}

        //private bool checkWindowsLicense()
        //{
        //    string s = string.Empty;
        //    if (licenta != null)
        //    {
        //        s = licenta;
        //    }
        //    else
        //    {
        //        FileInfo info = new FileInfo("Charts.lic");
        //        if (info.Exists)
        //        {
        //            s = File.ReadAllText(info.FullName);
        //            licenta = s;
        //        }
        //    }
        //    return this.checkUniversalKey(s);
        //}

        private int ComputeXAxisSpace(Graphics g, int width)
        {
            float height = 0f;
            if ((this.XName != string.Empty) && (this.XName != null))
            {
                Font axisLabelFont = this.AxisLabelFont;
                height = g.MeasureString(this.XName, axisLabelFont).Height;
            }
            if ((this.XAxisLabels == string.Empty) || (this.XAxisLabels == null))
            {
                return Convert.ToInt32(height);
            }
            int length = 0;
            foreach (DataRow row in this.data.Rows)
            {
                string str = string.Format(this.LabelsFormatString, row[this.XAxisLabels]);
                if (str.Length > length)
                {
                    length = str.Length;
                }
            }
            Font labelsFont = this.LabelsFont;
            string str2 = "8888888888888888888888888888888888888888888888888";
            if (length > str2.Length)
            {
                length = str2.Length;
            }
            SizeF ef2 = g.MeasureString(str2.Substring(0, length), labelsFont);
            if (ef2.Width < (width * 0.8))
            {
                return Convert.ToInt32((float) ((ef2.Height + 20f) + height));
            }
            return (Convert.ToInt32((double) (Math.Sqrt((double) ((ef2.Width * ef2.Width) / 2f)) + height)) + 20);
        }

        private int ComputeYAxisSpace(Graphics g, decimal heighest)
        {
            Font labelsFont = this.LabelsFont;
            string str = "88888888888888888888888888888888";
            int length = Convert.ToInt32(heighest).ToString().Length;
            if (length > str.Length)
            {
                length = str.Length;
            }
            SizeF ef = g.MeasureString(str.Substring(0, length), labelsFont);
            float num2 = 0f;
            if ((this.YName != string.Empty) && (this.YName != null))
            {
                Font axisLabelFont = this.AxisLabelFont;
                num2 = g.MeasureString(this.YName, axisLabelFont).Height + 5f;
            }
            return Convert.ToInt32((float) ((ef.Width + 10f) + num2));
        }

        public void deleteImages(bool force)
        {
            try
            {
                if ((this.ImageFolder != string.Empty) && (this.ImageFolder != null))
                {
                    DirectoryInfo info = new DirectoryInfo(this.GetFullPath("."));
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                    object now = DateTime.Now;
                    if (HttpContext.Current != null)
                    {
                        now = HttpContext.Current.Application["LastChartCleanUp"];
                    }
                    if ((force || (now == null)) || (DateTime.Now.Subtract((DateTime) now) > new TimeSpan(0, 1, 0)))
                    {
                        foreach (FileInfo info2 in info.GetFiles())
                        {
                            if (info2.Extension == ".jpeg")
                            {
                                info2.Delete();
                            }
                            if (info2.Extension == ".xml")
                            {
                                info2.Delete();
                            }
                        }
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.Application["LastChartCleanUp"] = DateTime.Now;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private int determineYInterval(decimal Interval, decimal heightAxa, int maxSpace)
        {
            if (Interval == 0M)
            {
                return 0;
            }
            decimal[] numArray = new decimal[] { 2M, 2.5M, 2M };
            int num = 0;
            decimal num2 = 1M;
            decimal num3 = -1M;
            bool flag = false;
            do
            {
                num3 = heightAxa / (Interval / num2);
                if (num3 < maxSpace)
                {
                    num2 *= numArray[num % 3];
                    num++;
                }
                else
                {
                    flag = true;
                }
            }
            while (!flag);
            return Convert.ToInt32(num2);
        }

        public Bitmap draw()
        {
            Bitmap bitmap3;
            if (this.width == 0)
            {
                this.width = 600;
            }
            if (this.height == 0)
            {
                this.height = 500;
            }
            this.ShadowOffset.Y = this.realShadowOffsetY;
            this.ShadowOffset.X = this.realShadowOffsetX;
            if (ScaleBreaks.UsesScaleBreaks(this.type) && (this.ScaleBreakMaxV > this.ScaleBreakMinV))
            {
                this.ScaleBreakMax = this.ScaleBreakMaxV;
                this.ScaleBreakMin = this.ScaleBreakMinV;
                this.ScaleBreakHeight = this.ScaleBreakHeightV;
            }
            else
            {
                this.ScaleBreakMax = this.ScaleBreakMin = this.ScaleBreakHeight = 0;
            }
            Bitmap image = new Bitmap(this.width, this.height);
            this.xmlChart = new XmlChart(this.flash);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = this.smoothingMode;
            Brush brush = new SolidBrush(Color.Black);
            graphics.FillRectangle(new SolidBrush(this.BackColor2), -5, -5, this.width + 10, this.height + 10);
            this.xmlChart.AddBar(this.height + 10, this.width + 10, -5f, -5f, new ColorItem(this.BackColor2).ToString(), this.BackColor2.A, 0, 0, string.Empty, null);
            SizeF ef = graphics.MeasureString(this.Title, this.TitleFont);
            graphics.DrawString(this.Title, this.TitleFont, brush, (float) ((this.width - ef.Width) / 2f), (float) 10f);
            this.xmlChart.AddLabel(this.Title, (this.width - ef.Width) / 2f, 10f, 0, this.TitleFont, false);
            this.graphHeight = Convert.ToInt32((float) ((this.height - 20) - ef.Height));
            this.graphWidth = this.width;
            Bitmap bitmap2 = new Bitmap(1, 1);
            try
            {
                this.xmlChart.CurrentPhase = Phase.Key;
                this.xmlChart.OffsetY = Convert.ToInt32((float) (20f + ef.Height));
                if (((((this.type == 4) || (this.type == 6)) || ((this.type == 7) || (this.type == 8))) || (((this.type == 9) || (this.type == 10)) || ((this.type == 11) || (this.type == 15)))) || (((((this.type == 0x15) || (this.type == 0x16)) || ((this.type == 0x17) || (this.type == 0x18))) || (((this.type == 0x19) || (this.type == 0x1a)) || ((this.type == 0x1b) || (this.type == 0x1c)))) || (((this.type == 0x21) || (this.type == 0x22)) || (((this.type == 0x23) || (this.type == 0x25)) || (this.type == 0x26)))))
                {
                    bitmap2 = this.drawMultiKey();
                }
                if (((((this.type == 1) || (this.type == 3)) || ((this.type == 13) || (this.type == 0x11))) || ((this.type == 0x1d) || (this.type == 30))) && (this.XAxisLabels != string.Empty))
                {
                    bitmap2 = this.drawPieKey();
                }
                if (((this.type == 0x1f) || (this.type == 0x10)) && (this.IDCategory != string.Empty))
                {
                    bitmap2 = this.drawCategoryKey();
                }
            }
            catch
            {
            }
            this.graphWidth = (this.width - 10) - bitmap2.Width;
            int graphHeight = this.graphHeight;
            int graphWidth = this.graphWidth;
            this.graphstartsY = Convert.ToInt32((float) (20f + ef.Height));
            this.xmlChart.OffsetY = this.graphstartsY;
            this.xmlChart.OffsetX = 3;
            switch (this.type)
            {
                case 0:
                    bitmap3 = this.drawBars();
                    break;

                case 1:
                    bitmap3 = this.drawPie();
                    break;

                case 2:
                    bitmap3 = this.drawBars3D();
                    break;

                case 3:
                    bitmap3 = this.drawDonought();
                    break;

                case 4:
                    bitmap3 = this.drawMultiBars();
                    break;

                case 5:
                    bitmap3 = this.drawLine();
                    break;

                case 6:
                    bitmap3 = this.drawMultiLine();
                    break;

                case 7:
                    bitmap3 = this.drawStackedBars();
                    break;

                case 8:
                    bitmap3 = this.drawStackedBarsFull();
                    break;

                case 9:
                    bitmap3 = this.drawStackedBars3D();
                    break;

                case 10:
                    bitmap3 = this.drawStackedBars3DFull();
                    break;

                case 11:
                    bitmap3 = this.drawMultiBars3D();
                    break;

                case 12:
                    bitmap3 = this.drawCylinders();
                    break;

                case 13:
                    bitmap3 = this.draw3DExplodedPieOrDonauht(0.0, 0.0);
                    break;

                case 14:
                    bitmap3 = this.drawSurface();
                    break;

                case 15:
                    bitmap3 = this.drawMultiSurface();
                    break;

                case 0x10:
                    bitmap3 = this.DrawMap();
                    break;

                case 0x11:
                    bitmap3 = this.drawExplodedPie();
                    break;

                case 0x12:
                    bitmap3 = this.drawRadar(false);
                    break;

                case 0x13:
                    bitmap3 = this.drawRadar(true);
                    break;

                case 20:
                    bitmap3 = this.drawUserDrawnBars();
                    break;

                case 0x15:
                    bitmap3 = this.drawMultiRadar(false);
                    break;

                case 0x16:
                    bitmap3 = this.drawMultiRadar(true);
                    break;

                case 0x17:
                    bitmap3 = this.drawStackedRadar(false);
                    break;

                case 0x18:
                    bitmap3 = this.drawStackedRadar(true);
                    break;

                case 0x19:
                    bitmap3 = this.drawStackedSurface();
                    break;

                case 0x1a:
                    bitmap3 = this.drawFullStackedSurface();
                    break;

                case 0x1b:
                    bitmap3 = this.drawMulti3DLine();
                    break;

                case 0x1c:
                    bitmap3 = this.drawMultiCurve();
                    break;

                case 0x1d:
                    bitmap3 = this.draw3DExplodedPieOrDonauht(this.explosion, 0.0);
                    break;

                case 30:
                    bitmap3 = this.draw3DExplodedPieOrDonauht(this.explosion, this.donaughtHole);
                    break;

                case 0x1f:
                    bitmap3 = this.DrawMapPoints();
                    break;

                case 0x20:
                    bitmap3 = this.drawWideBars3D();
                    break;

                case 0x21:
                    bitmap3 = this.drawStackedCylinders();
                    break;

                case 0x22:
                    bitmap3 = this.drawFullStackedCylinders();
                    break;

                case 0x23:
                    bitmap3 = this.drawMultiCylinders();
                    break;

                case 0x24:
                    bitmap3 = this.drawSurface3D();
                    break;

                case 0x25:
                    bitmap3 = this.drawMultiSurface3D();
                    break;

                case 0x26:
                    bitmap3 = this.drawStackedSurface3D();
                    break;

                default:
                    bitmap3 = this.drawBars();
                    break;
            }
            graphics.DrawImage(bitmap3, new RectangleF(3f, (float) this.graphstartsY, (float) graphWidth, (float) graphHeight));
            this.xmlChart.OffsetY = 0;
            this.xmlChart.OffsetX = 0;
            graphics.DrawImage(bitmap2, new RectangleF((float) (10 + bitmap3.Width), (float) Convert.ToInt32((float) (20f + ef.Height)), (float) bitmap2.Width, (float) bitmap2.Height), new RectangleF(0f, 0f, (float) bitmap2.Width, (float) bitmap2.Height), GraphicsUnit.Pixel);
            //if (!this.checkLicense())
            //{
            //    Font font = new Font("Verdana", 15f);
            //    Brush brush2 = new SolidBrush(Color.FromArgb(200, Color.Red));
            //    SizeF ef2 = graphics.MeasureString(LicenseText, font);
            //    graphics.DrawString(LicenseText, font, brush2, (float) ((this.width - ef2.Width) / 2f), (float) ((this.height - ef2.Height) / 2f));
            //    this.xmlChart.AddCode((this.width - ef2.Width) / 2f, (this.height - ef2.Height) / 2f, font, false);
            //    return image;
            //}
            this.xmlChart.AddCode(0f, 0f, this.ValueLabelsFont, true);
            return image;
        }

        private Bitmap draw3DExplodedPieOrDonauht(double explosion, double donoughtHole)
        {
            double num = 0.0;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDouble(row[this.x[0]]) > 0.0)
                {
                    num += Convert.ToDouble(row[this.x[0]]);
                }
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            this.xmlChart.CurrentPhase = Phase.Background;
            g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.graphWidth, this.graphHeight);
            this.xmlChart.AddBar(this.graphHeight, this.graphWidth, 0f, 0f, new ColorItem(this.BackColor).ToString(), this.BackColor.A, 0, 0, string.Empty, string.Empty);
            this.xmlChart.CurrentPhase = Phase.Elements;
            Pen pen = new Pen(Color.FromArgb(this.alpha, this.ForeColor));
            Pen pen2 = new Pen(Color.FromArgb(this.alpha / 2, this.ForeColor));
            Brush brush = new SolidBrush(Color.FromArgb(100, Color.Black));
            double num3 = (Math.Min(this.graphWidth, this.graphHeight) - 10) * explosion;
            int width = Convert.ToInt32((double) ((Math.Min(this.graphWidth, this.graphHeight) - 10) - (2.0 * num3)));
            int height = width;
            int num6 = Convert.ToInt32((double) (width * donoughtHole));
            if (num6 == 0)
            {
                num6 = 1;
            }
            int num7 = Convert.ToInt32((double) (height * donoughtHole));
            if (num7 == 0)
            {
                num7 = 1;
            }
            int num8 = (this.graphWidth - width) / 2;
            int num9 = this.graphHeight - (height / 2);
            int num10 = Convert.ToInt32((double) ((this.Percent3D * height) / 2.0)) / 8;
            this.LoadImage(string.Format("pieLight{0}.png", this.effect));
            this.LoadImage(string.Format("pieShadow.png", new object[0]));
            g.ScaleTransform(1f, 0.5f);
            float item = -90f;
            List<float> list = new List<float>();
            List<float> list2 = new List<float>();
            List<int> list3 = new List<int>();
            for (int i = 0; i < count; i++)
            {
                float num13 = (float) ((Convert.ToDouble(this.data.Rows[i][this.x[0]]) / num) * 360.0);
                if (item < 90f)
                {
                    list.Add(item);
                    list2.Add(num13);
                    list3.Add(i);
                }
                else
                {
                    list.Insert(0, item);
                    list2.Insert(0, num13);
                    list3.Insert(0, i);
                }
                item += num13;
            }
            int num14 = num8 + (width / 2);
            int num15 = num9 + (height / 2);
            if (this.ShadowIsDrawn)
            {
                for (int k = 0; k < count; k++)
                {
                    float sweepAngle = list2[k];
                    item = list[k];
                    int local1 = list3[k];
                    double d = ((item + (sweepAngle / 2f)) / 180f) * 3.1415926535897931;
                    int num19 = Convert.ToInt32((double) (num3 * Math.Cos(d)));
                    int num20 = Convert.ToInt32((double) (num3 * Math.Sin(d)));
                    int num21 = Convert.ToInt32((double) ((width / 2) * Math.Cos(((item + sweepAngle) / 180f) * 3.1415926535897931))) + num19;
                    int num22 = Convert.ToInt32((double) ((height / 2) * Math.Sin(((item + sweepAngle) / 180f) * 3.1415926535897931))) + num20;
                    Convert.ToInt32((double) ((width / 2) * Math.Cos((item / 180f) * 3.1415926535897931)));
                    Convert.ToInt32((double) ((height / 2) * Math.Sin((item / 180f) * 3.1415926535897931)));
                    int num23 = Convert.ToInt32((double) ((num6 / 2) * Math.Cos(((item + sweepAngle) / 180f) * 3.1415926535897931))) + num19;
                    int num24 = Convert.ToInt32((double) ((num7 / 2) * Math.Sin(((item + sweepAngle) / 180f) * 3.1415926535897931))) + num20;
                    Convert.ToInt32((double) ((num6 / 2) * Math.Cos((item / 180f) * 3.1415926535897931)));
                    Convert.ToInt32((double) ((num7 / 2) * Math.Sin((item / 180f) * 3.1415926535897931)));
                    GraphicsPath path = new GraphicsPath();
                    path.AddArc((num8 + num19) + this.ShadowOffset.X, ((num9 + num10) + num20) + this.ShadowOffset.Y, width, height, item, sweepAngle);
                    path.AddLine((int) ((num14 + num21) + this.ShadowOffset.X), (int) (((num15 + num10) + num22) + this.ShadowOffset.Y), (int) ((num14 + num23) + this.ShadowOffset.X), (int) (((num15 + num10) + num24) + this.ShadowOffset.Y));
                    path.AddArc(((num8 + num19) + ((width - num6) / 2)) + this.ShadowOffset.X, (((num9 + num10) + num20) + ((height - num7) / 2)) + this.ShadowOffset.Y, num6, num7, item + sweepAngle, -sweepAngle);
                    path.CloseFigure();
                    g.FillPath(brush, path);
                }
            }
            List<Charts.Label> list4 = new List<Charts.Label>();
            for (int j = 0; j < count; j++)
            {
                float num26 = list2[j];
                item = list[j];
                int num27 = list3[j];
                double num28 = ((item + (num26 / 2f)) / 180f) * 3.1415926535897931;
                int num29 = Convert.ToInt32((double) (num3 * Math.Cos(num28)));
                int num30 = Convert.ToInt32((double) (num3 * Math.Sin(num28)));
                int num31 = Convert.ToInt32((double) ((width / 2) * Math.Cos(((item + num26) / 180f) * 3.1415926535897931))) + num29;
                int num32 = Convert.ToInt32((double) ((height / 2) * Math.Sin(((item + num26) / 180f) * 3.1415926535897931))) + num30;
                int num33 = Convert.ToInt32((double) ((width / 2) * Math.Cos((item / 180f) * 3.1415926535897931))) + num29;
                int num34 = Convert.ToInt32((double) ((height / 2) * Math.Sin((item / 180f) * 3.1415926535897931))) + num30;
                int num35 = Convert.ToInt32((double) ((num6 / 2) * Math.Cos(((item + num26) / 180f) * 3.1415926535897931))) + num29;
                int num36 = Convert.ToInt32((double) ((num7 / 2) * Math.Sin(((item + num26) / 180f) * 3.1415926535897931))) + num30;
                int num37 = Convert.ToInt32((double) ((num6 / 2) * Math.Cos((item / 180f) * 3.1415926535897931))) + num29;
                int num38 = Convert.ToInt32((double) ((num7 / 2) * Math.Sin((item / 180f) * 3.1415926535897931))) + num30;
                GraphicsPath path2 = new GraphicsPath();
                PointF[] points = new PointF[] { new PointF((float) (num14 + num35), (float) ((num15 - num10) + num36)), new PointF((float) (num14 + num35), (float) ((num15 + num10) + num36)), new PointF((float) (num14 + num31), (float) ((num15 + num10) + num32)), new PointF((float) (num14 + num31), (float) ((num15 - num10) + num32)) };
                path2.AddPolygon(points);
                Region region = new Region(path2);
                path2 = new GraphicsPath();
                points = new PointF[] { new PointF((float) (num14 + num37), (float) ((num15 - num10) + num38)), new PointF((float) (num14 + num37), (float) ((num15 + num10) + num38)), new PointF((float) (num14 + num33), (float) ((num15 + num10) + num34)), new PointF((float) (num14 + num33), (float) ((num15 - num10) + num34)) };
                path2.AddPolygon(points);
                region.Union(new Region(path2));
                path2 = new GraphicsPath();
                path2.AddArc(num8 + num29, (num9 + num10) + num30, width, height, item, num26);
                path2.AddLine((int) (num14 + num31), (int) ((num15 + num10) + num32), (int) (num14 + num31), (int) ((num15 + num32) - num10));
                path2.AddArc(num8 + num29, (num9 - num10) + num30, width, height, item + num26, -num26);
                path2.CloseFigure();
                region.Union(new Region(path2));
                path2 = new GraphicsPath();
                path2.AddArc((num8 + num29) + ((width - num6) / 2), ((num9 + num10) + num30) + ((height - num7) / 2), num6, num7, item, num26);
                path2.AddLine((int) (num14 + num35), (int) ((num15 + num10) + num36), (int) (num14 + num35), (int) ((num15 + num36) - num10));
                path2.AddArc((num8 + num29) + ((width - num6) / 2), ((num9 - num10) + num30) + ((height - num7) / 2), num6, num7, item + num26, -num26);
                path2.CloseFigure();
                region.Union(new Region(path2));
                if ((item < 0f) && ((item + num26) > 0f))
                {
                    int num39 = (width / 2) + num29;
                    int num40 = num30;
                    int num41 = (num6 / 2) + num29;
                    int num42 = num30;
                    path2 = new GraphicsPath();
                    points = new PointF[] { new PointF((float) (num14 + num41), (float) ((num15 - num10) + num42)), new PointF((float) (num14 + num41), (float) ((num15 + num10) + num42)), new PointF((float) (num14 + num39), (float) ((num15 + num10) + num40)), new PointF((float) (num14 + num39), (float) ((num15 - num10) + num40)) };
                    path2.AddPolygon(points);
                    region.Union(new Region(path2));
                    path2 = new GraphicsPath();
                    path2.AddArc(num8 + num29, (num9 + num10) + num30, width, height, item, 0f - item);
                    path2.AddLine((int) (num14 + num39), (int) ((num15 + num10) + num40), (int) (num14 + num39), (int) ((num15 + num40) - num10));
                    path2.AddArc(num8 + num29, (num9 - num10) + num30, width, height, 0f, item);
                    path2.CloseFigure();
                    region.Union(new Region(path2));
                    g.DrawLine(pen, (int) (num14 + num39), (int) ((num15 + num10) + num40), (int) (num14 + num39), (int) ((num15 - num10) + num40));
                }
                if ((item < 180f) && ((item + num26) > 180f))
                {
                    int num43 = (-width / 2) + num29;
                    int num44 = num30;
                    int num45 = (-num6 / 2) + num29;
                    int num46 = num30;
                    path2 = new GraphicsPath();
                    points = new PointF[] { new PointF((float) (num14 + num45), (float) ((num15 - num10) + num46)), new PointF((float) (num14 + num45), (float) ((num15 + num10) + num46)), new PointF((float) (num14 + num43), (float) ((num15 + num10) + num44)), new PointF((float) (num14 + num43), (float) ((num15 - num10) + num44)) };
                    path2.AddPolygon(points);
                    region.Union(new Region(path2));
                    g.DrawLine(pen, (int) (num14 + num43), (int) ((num15 + num10) + num44), (int) (num14 + num43), (int) ((num15 - num10) + num44));
                }
                path2 = new GraphicsPath();
                path2.AddArc(num8 + num29, (num9 - num10) + num30, width, height, item, num26);
                path2.AddLine((int) (num14 + num31), (int) ((num15 - num10) + num32), (int) (num14 + num35), (int) ((num15 - num10) + num36));
                path2.AddArc((num8 + num29) + ((width - num6) / 2), ((num9 - num10) + num30) + ((height - num7) / 2), num6, num7, item + num26, -num26);
                path2.CloseFigure();
                region.Union(new Region(path2));
                g.FillRegion(new SolidBrush(Color.FromArgb(this.alpha, this.col[num27 % this.col.Count])), region);
                g.DrawPath(pen, path2);
                g.DrawArc(pen2, (float) (num8 + num29), (float) ((num9 + num10) + num30), (float) width, (float) height, item, num26);
                g.DrawArc(pen2, (float) ((num8 + num29) + ((width - num6) / 2)), (float) (((num9 + num10) + num30) + ((height - num7) / 2)), (float) num6, (float) num7, item, num26);
                g.DrawLine(pen2, (int) (num14 + num31), (int) ((num15 + num10) + num32), (int) (num14 + num35), (int) ((num15 + num10) + num36));
                g.DrawLine(pen2, (int) (num14 + num33), (int) ((num15 + num10) + num34), (int) (num14 + num37), (int) ((num15 + num10) + num38));
                Pen pen3 = pen2;
                if (Math.Sin(((item + num26) / 180f) * 3.1415926535897931) < 0.0)
                {
                    pen3 = pen;
                }
                else
                {
                    pen3 = pen2;
                }
                g.DrawLine(pen3, (int) (num14 + num35), (int) ((num15 - num10) + num36), (int) (num14 + num35), (int) ((num15 + num10) + num36));
                if (Math.Sin((item / 180f) * 3.1415926535897931) < 0.0)
                {
                    pen3 = pen;
                }
                else
                {
                    pen3 = pen2;
                }
                g.DrawLine(pen3, (int) (num14 + num37), (int) ((num15 - num10) + num38), (int) (num14 + num37), (int) ((num15 + num10) + num38));
                if ((Math.Sin(((item + num26) / 180f) * 3.1415926535897931) >= 0.0) || (Math.Cos((item / 180f) * 3.1415926535897931) > 0.0))
                {
                    pen3 = pen;
                }
                else
                {
                    pen3 = pen2;
                }
                g.DrawLine(pen3, (int) (num14 + num31), (int) ((num15 + num32) - num10), (int) (num14 + num31), (int) ((num15 + num10) + num32));
                if ((Math.Sin((item / 180f) * 3.1415926535897931) >= 0.0) || (Math.Cos((item / 180f) * 3.1415926535897931) < 0.0))
                {
                    pen3 = pen;
                }
                else
                {
                    pen3 = pen2;
                }
                g.DrawLine(pen3, (int) (num14 + num33), (int) ((num15 + num34) + num10), (int) (num14 + num33), (int) ((num15 - num10) + num34));
                double num47 = item + 90f;
                double num48 = (item + 90f) + num26;
                double y = Math.Sin((num47 / 180.0) * 3.1415926535897931);
                double num50 = Math.Sin((num48 / 180.0) * 3.1415926535897931);
                double num51 = Math.Cos((num47 / 180.0) * 3.1415926535897931);
                double num52 = Math.Cos((num48 / 180.0) * 3.1415926535897931);
                double num53 = Math.Atan2(y, -num51);
                if (num53 == -3.1415926535897931)
                {
                    num53 += 6.2831853071795862;
                }
                double num54 = Math.Atan2(num50, -num52);
                if (num54 == -3.1415926535897931)
                {
                    num54 += 6.2831853071795862;
                }
                float num55 = Convert.ToSingle((double) ((Math.Min(num53, num54) * 180.0) / 3.1415926535897931));
                float num56 = Convert.ToSingle((double) ((Math.Max(num53, num54) * 180.0) / 3.1415926535897931));
                if (Math.Abs((float) ((num56 - num55) - num26)) > 1f)
                {
                    float num57 = num55;
                    num55 = num56;
                    num56 = num57;
                    num55 -= 360f;
                }
                this.xmlChart.AddDonut3D(num55, num56, width / 2, num6 / 2, 2 * num10, num14 + num29, num15 + num30, this.col[num27 % this.col.Count], this.alpha, this.GetNavigateUrl(this.GetID(j) + "||" + j), this.GetToolTip(j));
                if ((this.ValueLabelsFont != null) && (this.ValueLabelsFormatString != string.Empty))
                {
                    string text = string.Format(this.ValueLabelsFormatString, num26 / 360f);
                    SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                    int num58 = (num14 + Convert.ToInt32((double) (((width + num6) / 4) * Math.Cos(((item + (num26 / 2f)) / 180f) * 3.1415926535897931)))) + num29;
                    int num59 = ((num15 - num10) + Convert.ToInt32((double) (((height + num7) / 4) * Math.Sin(((item + (num26 / 2f)) / 180f) * 3.1415926535897931)))) + num30;
                    list4.Add(new Charts.Label(text, num58 - (size.Width / 2f), (num59 - (size.Height / 2f)) + 1f, size));
                }
            }
            Charts.Label.DrawList(list4, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight * 2, 0, 0);
            return image;
        }

        private Bitmap drawBars()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDecimal(row[this.x[0]]) < lowest)
                {
                    lowest = Convert.ToDecimal(row[this.x[0]]);
                }
                if (Convert.ToDecimal(row[this.x[0]]) > heighest)
                {
                    heighest = Convert.ToDecimal(row[this.x[0]]);
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num5 = ((float) this.graphWidth) / (count + 1f);
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num5));
            int offsetYTop = 10;
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            int width = Convert.ToInt32((double) (num5 * this.PercentWidth));
            int spacer = Convert.ToInt32((double) (num5 - width)) / 2;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num5, 0, this.graphHeight, offsetYTop, spacer, false);
            offsetYTop -= this.negativeOffset;
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            Brush brush = new SolidBrush(Color.FromArgb(this.alpha, this.col[0]));
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage("barShadow.png");
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                int height = 0;
                try
                {
                    height = Convert.ToInt32((decimal) ((ScaleBreaks.ValRec(this.data.Rows[i][this.x[0]], this.ScaleBreakMin, this.ScaleBreakMax) / (((heighest - lowest) + this.ScaleBreakMin) - this.ScaleBreakMax)) * (this.graphHeight - this.ScaleBreakHeight)));
                }
                catch
                {
                }
                if (height != 0)
                {
                    int x = Convert.ToInt32((double) ((i + 0.5) * num5)) + offsetX;
                    int y = (this.graphHeight - height) + offsetYTop;
                    if (y < this.ScaleBreakPos)
                    {
                        height += this.ScaleBreakHeight;
                        y -= this.ScaleBreakHeight;
                    }
                    if (this.ShadowIsDrawn)
                    {
                        if (height < image2.Height)
                        {
                            g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, height - this.ShadowOffset.Y), new Rectangle(0, 0, image2.Width, height - this.ShadowOffset.Y), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, height - this.ShadowOffset.Y));
                        }
                    }
                    if (height > 0)
                    {
                        g.FillRectangle(brush, x, y, width, height);
                        this.xmlChart.AddBar(height, width, (float) x, (float) y, this.col[0].ToString(), this.alpha, 0, 0, this.GetNavigateUrl(this.GetID(i) + "||" + i), this.GetToolTip(i));
                        g.DrawImage(image, new Rectangle(x, y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                    }
                    else
                    {
                        g.FillRectangle(brush, x, y + height, width, -height);
                        this.xmlChart.AddBar(-height, width, (float) x, (float) (y + height), this.col[0].ToString(), this.alpha, 0, 0, this.GetNavigateUrl(this.GetID(i) + "||" + i), this.GetToolTip(i));
                        g.DrawImage(image, new Rectangle(x, y + height, width, -height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                    }
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[i][this.x[0]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, x + ((width - size.Width) / 2f), (y - size.Height) + 1f, size));
                    }
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[i][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = this.GetID(i) + "||" + i;
                    spot.Left = x;
                    spot.Right = x + width;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = (y + height) + this.graphstartsY;
                    spots.Add(spot);
                }
            }
            if (this.ScaleBreakMax > this.ScaleBreakMin)
            {
                ScaleBreaks.drawScaleBreak(this.xmlChart, g, this.BackColor2, this.ScaleBreakPos, this.ScaleBreakHeight, offsetX - 1, this.graphWidth, 0);
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawBars3D()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDecimal(row[this.x[0]]) < lowest)
                {
                    lowest = Convert.ToDecimal(row[this.x[0]]);
                }
                if (Convert.ToDecimal(row[this.x[0]]) > heighest)
                {
                    heighest = Convert.ToDecimal(row[this.x[0]]);
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            double num4 = ((float) this.graphWidth) / (count + 1f);
            int depth = Convert.ToInt32((double) (Convert.ToInt32((double) (num4 * this.PercentWidth)) * this.Percent3D));
            int offsetX = this.ComputeYAxisSpace(g, heighest) + depth;
            int offsetYTop = depth + 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num4));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.graphWidth -= offsetX;
            num4 = ((float) this.graphWidth) / (count + 1f);
            int width = Convert.ToInt32((double) (num4 * this.PercentWidth));
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            depth = Convert.ToInt32((double) (width * this.Percent3D));
            width -= depth;
            int spacer = Convert.ToInt32((double) (num4 - width)) / 2;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num4, depth, this.graphHeight, offsetYTop, spacer, false);
            offsetYTop -= this.negativeOffset;
            Color baseColor = this.col[0];
            Brush brush = new SolidBrush(Color.FromArgb(this.alpha, baseColor));
            Brush brush2 = new SolidBrush(Color.FromArgb((this.alpha * 3) / 4, baseColor));
            Brush brush3 = new SolidBrush(Color.FromArgb(this.alpha / 2, baseColor));
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage(string.Format("barShadow.png", new object[0]));
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                int height = 0;
                try
                {
                    height = Convert.ToInt32((decimal) ((ScaleBreaks.ValRec(this.data.Rows[i][this.x[0]], this.ScaleBreakMin, this.ScaleBreakMax) / (((heighest - lowest) + this.ScaleBreakMin) - this.ScaleBreakMax)) * (this.graphHeight - this.ScaleBreakHeight)));
                }
                catch
                {
                }
                if (height != 0)
                {
                    int x = Convert.ToInt32((double) ((i + 0.5) * num4)) + offsetX;
                    int y = (this.graphHeight - height) + offsetYTop;
                    if (y < this.ScaleBreakPos)
                    {
                        height += this.ScaleBreakHeight;
                        y -= this.ScaleBreakHeight;
                    }
                    if (this.ShadowIsDrawn)
                    {
                        if (height < image2.Height)
                        {
                            g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num4), (height - this.ShadowOffset.Y) - depth), new Rectangle(0, 0, image2.Width, (height - this.ShadowOffset.Y) - depth), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num4), (height - this.ShadowOffset.Y) - depth));
                        }
                    }
                    if (height > 0)
                    {
                        g.FillRectangle(brush, x, y, width, height);
                        g.DrawImage(image, new Rectangle(x, y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                        Point[] points = new Point[] { new Point(x + width, y), new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point((x + width) + depth, y - depth) };
                        Point[] destPoints = new Point[] { points[0], points[3], points[1] };
                        g.FillPolygon(brush2, points);
                        g.DrawImage(image, destPoints, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                        Point[] pointArray3 = new Point[] { new Point(x + width, y), new Point((x + width) + depth, y - depth), new Point(x + depth, y - depth), new Point(x, y) };
                        Point[] pointArray4 = new Point[] { pointArray3[0], pointArray3[3], pointArray3[1] };
                        g.FillPolygon(brush3, pointArray3);
                        g.DrawImage(image, pointArray4, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                    }
                    else
                    {
                        g.FillRectangle(brush, x, y + height, width, -height);
                        g.DrawImage(image, new Rectangle(x, y + height, width, -height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                        Point[] pointArray5 = new Point[] { new Point(x + width, y), new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point((x + width) + depth, y - depth) };
                        Point[] pointArray6 = new Point[] { pointArray5[0], pointArray5[3], pointArray5[1] };
                        g.FillPolygon(brush2, pointArray5);
                        g.DrawImage(image, pointArray6, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                        Point[] pointArray7 = new Point[] { new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point(x + depth, (y + height) - depth), new Point(x, y + height) };
                        Point[] pointArray8 = new Point[] { pointArray7[0], pointArray7[3], pointArray7[1] };
                        g.FillPolygon(brush3, pointArray7);
                        g.DrawImage(image, pointArray8, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                    }
                    this.xmlChart.AddBar(height, width, (float) x, (float) y, new ColorItem((brush as SolidBrush).Color).ToString(), (brush as SolidBrush).Color.A, depth, 0, this.GetNavigateUrl(this.GetID(i) + "||" + i), this.GetToolTip(i));
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[i][this.x[0]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, x + ((width - size.Width) / 2f), (y - size.Height) + 0f, size));
                    }
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[i][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = this.GetID(i) + "||" + i;
                    spot.Left = x;
                    spot.Right = (x + width) + depth;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = (y + height) + this.graphstartsY;
                    spots.Add(spot);
                }
            }
            if (this.ScaleBreakMax > this.ScaleBreakMin)
            {
                ScaleBreaks.drawScaleBreak(this.xmlChart, g, this.BackColor2, this.ScaleBreakPos, this.ScaleBreakHeight, offsetX - 1, this.graphWidth, depth);
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawCategoryKey()
        {
            List<string> list = new List<string>();
            int count = this.data.Rows.Count;
            int length = 0;
            int num3 = 0;
            for (int i = 0; i < count; i++)
            {
                if (!list.Contains(this.data.Rows[i][this.IDCategory].ToString()))
                {
                    list.Add(this.data.Rows[i][this.IDCategory].ToString());
                    if (this.data.Rows[i][this.IDCategory].ToString().Length > length)
                    {
                        length = this.data.Rows[i][this.IDCategory].ToString().Length;
                    }
                    num3 = i;
                }
            }
            count = list.Count;
            Bitmap image = new Bitmap(10, 10);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = this.smoothingMode;
            Font keyFont = this.KeyFont;
            Pen pen = new Pen(Color.Black, 2f);
            SizeF ef = graphics.MeasureString(this.data.Rows[num3][this.IDCategory].ToString(), keyFont);
            int width = Convert.ToInt32(ef.Height);
            int num6 = Convert.ToInt32((int) ((this.graphHeight - 20) / (width + 5)));
            int num7 = Convert.ToInt32(Math.Ceiling((double) (((float) count) / (num6 + 0f))));
            int num8 = Convert.ToInt32((float) ((15 + width) + ef.Width));
            image = new Bitmap(num8 * num7, Convert.ToInt32((float) ((num6 * (ef.Height + 5f)) + 5f)));
            graphics = Graphics.FromImage(image);
            Brush brush = new SolidBrush(Color.Black);
            int num9 = 0;
            for (int j = 0; j < count; j++)
            {
                if (((j % num6) == 0) && (j > 0))
                {
                    num9++;
                }
                if ((this.pointImagesReal.Count == 0) || (this.type < 0x1f))
                {
                    graphics.FillRectangle(new SolidBrush(this.col[j % this.col.Count]), (float) (5 + (num9 * num8)), 5f + ((j % num6) * (ef.Height + 5f)), (float) width, (float) width);
                    graphics.DrawRectangle(pen, (float) (5 + (num9 * num8)), 5f + ((j % num6) * (ef.Height + 5f)), (float) width, (float) width);
                }
                else
                {
                    graphics.DrawImage(System.Drawing.Image.FromFile(this.pointImagesReal[j % this.pointImagesReal.Count]), new Rectangle(5 + (num9 * num8), 5 + ((j % num6) * (Convert.ToInt32(ef.Height) + 5)), width, width));
                }
                graphics.DrawString(list[j], keyFont, brush, (float) ((10 + width) + (num9 * num8)), 5f + ((j % num6) * (ef.Height + 5f)));
            }
            return image;
        }

        private Bitmap drawCylinders()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDecimal(row[this.x[0]]) < lowest)
                {
                    lowest = Convert.ToDecimal(row[this.x[0]]);
                }
                if (Convert.ToDecimal(row[this.x[0]]) > heighest)
                {
                    heighest = Convert.ToDecimal(row[this.x[0]]);
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num5 = ((float) this.graphWidth) / (count + 1f);
            int width = Convert.ToInt32((double) (num5 * this.PercentWidth));
            int depth = Convert.ToInt32((double) (width * this.Percent3D));
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            int spacer = Convert.ToInt32((double) (num5 - width)) / 2;
            int offsetYTop = 10 + width;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num5));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num5, depth, this.graphHeight, offsetYTop, spacer, false);
            offsetYTop -= this.negativeOffset;
            Brush brush = new SolidBrush(Color.FromArgb(this.alpha, this.col[0]));
            Pen pen = new Pen(Color.Black);
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage("barShadow.png");
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                int height = 0;
                try
                {
                    height = Convert.ToInt32((decimal) ((ScaleBreaks.ValRec(this.data.Rows[i][this.x[0]], this.ScaleBreakMin, this.ScaleBreakMax) / (((heighest - lowest) + this.ScaleBreakMin) - this.ScaleBreakMax)) * (this.graphHeight - this.ScaleBreakHeight)));
                }
                catch
                {
                }
                if (height != 0)
                {
                    int x = Convert.ToInt32((double) (((i + 0.5) * num5) + (0.5 * depth))) + offsetX;
                    int y = ((this.graphHeight - height) + offsetYTop) - (depth / 2);
                    if (y < (this.ScaleBreakPos - (0.5 * depth)))
                    {
                        height += this.ScaleBreakHeight;
                        y -= this.ScaleBreakHeight;
                    }
                    if (this.ShadowIsDrawn)
                    {
                        if (height < image2.Height)
                        {
                            g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, (height - this.ShadowOffset.Y) + (depth / 2)), new Rectangle(0, 0, image2.Width, height - this.ShadowOffset.Y), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, (height - this.ShadowOffset.Y) + (depth / 2)));
                        }
                    }
                    g.FillEllipse(brush, x, y - (depth / 2), width, depth);
                    g.DrawArc(pen, x, (y - (depth / 2)) + height, width, depth, 180, 0xe10);
                    GraphicsPath path = new GraphicsPath();
                    if (height > 0)
                    {
                        path.AddRectangle(new Rectangle(x, y, width, height));
                    }
                    else
                    {
                        path.AddRectangle(new Rectangle(x, y + height, width, -height));
                    }
                    path.AddPie((float) x, (y + height) - (((float) depth) / 2f), (float) width, (float) depth, 0f, 180f);
                    path.AddPie((float) x, y - (((float) depth) / 2f), (float) width, (float) depth, 0f, 180f);
                    g.Clip = new Region(path);
                    if (height > 0)
                    {
                        g.FillRectangle(brush, x, y, width, height + (depth / 2));
                    }
                    else
                    {
                        g.FillRectangle(brush, x, y + height, width, -height + (depth / 2));
                    }
                    g.DrawImage(image, new Rectangle(x, y, width, height + (depth / 2)), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                    g.Clip = new Region();
                    g.DrawArc(pen, x, (y - (depth / 2)) + height, width, depth, 0, 180);
                    g.DrawEllipse(pen, x, y - (depth / 2), width, depth);
                    this.xmlChart.AddCylinder(height, width, (float) x, (float) y, this.col[0].ToString(), this.alpha, depth, 1, this.GetNavigateUrl(this.GetID(i) + "||" + i), this.GetToolTip(i));
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[i][this.x[0]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, x + ((width - size.Width) / 2f), ((y - size.Height) - depth) + 1f, size));
                    }
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[i][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = this.GetID(i) + "||" + i;
                    spot.Left = x;
                    spot.Right = x + width;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = ((y + height) + (depth / 2)) + this.graphstartsY;
                    spots.Add(spot);
                }
            }
            if (this.ScaleBreakMax > this.ScaleBreakMin)
            {
                ScaleBreaks.drawScaleBreak(this.xmlChart, g, this.BackColor2, this.ScaleBreakPos, this.ScaleBreakHeight, offsetX - 1, this.graphWidth, depth);
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawDonought()
        {
            double num = 0.0;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDouble(row[this.x[0]]) > 0.0)
                {
                    num += Convert.ToDouble(row[this.x[0]]);
                }
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.graphWidth, this.graphHeight);
            this.xmlChart.CurrentPhase = Phase.Background;
            this.xmlChart.AddBar(this.graphHeight, this.graphWidth, 0f, 0f, new ColorItem(this.BackColor).ToString(), 0xff, 0, 0, string.Empty, string.Empty);
            this.xmlChart.CurrentPhase = Phase.Elements;
            Pen pen = new Pen(Color.Black);
            int width = Math.Min(this.graphWidth, this.graphHeight) - 10;
            int x = (this.graphWidth - width) / 2;
            int y = (this.graphHeight - width) / 2;
            float startAngle = 0f;
            System.Drawing.Image image = this.LoadImage(string.Format("pieLight{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage(string.Format("donoughtShadow.png", new object[0]));
            if (this.ShadowIsDrawn)
            {
                g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y - this.ShadowOffset.Y, width + 10, width + 10), new Rectangle(0, 0, image2.Width, image2.Height), GraphicsUnit.Pixel);
            }
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                float sweepAngle = (float) ((Convert.ToDouble(this.data.Rows[i][this.x[0]]) / num) * 360.0);
                g.FillPie(new SolidBrush(this.col[i % this.col.Count]), (float) x, (float) y, (float) width, (float) width, startAngle, sweepAngle);
                g.DrawPie(pen, (float) x, (float) y, (float) width, (float) width, startAngle, sweepAngle);
                this.xmlChart.AddDonut((-startAngle - sweepAngle) + 90f, -startAngle + 90f, width / 2, width / 4, x + (width / 2), y + (width / 2), this.col[i % this.col.Count], this.alpha, this.GetNavigateUrl(this.GetID(i) + "||" + i), 1, false, this.GetToolTip(i));
                if ((this.ValueLabelsFormatString != null) && (this.ValueLabelsFormatString != string.Empty))
                {
                    string text = string.Format(this.ValueLabelsFormatString, sweepAngle / 360f);
                    SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                    int num9 = Convert.ToInt32((double) ((x + (width / 2)) + (((3 * width) / 8) * Math.Cos(((startAngle + (sweepAngle / 2f)) * 3.1415926535897931) / 180.0))));
                    int num10 = Convert.ToInt32((double) ((y + (width / 2)) + (((3 * width) / 8) * Math.Sin(((startAngle + (sweepAngle / 2f)) * 3.1415926535897931) / 180.0))));
                    list.Add(new Charts.Label(text, num9 - (size.Width / 2f), (num10 - (size.Height / 2f)) + 1f, size));
                }
                startAngle += sweepAngle;
            }
            g.DrawImage(image, new Rectangle(x, y, width, width), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse((int) (x + (width / 4)), (int) (y + (width / 4)), (int) (width / 2), (int) (width / 2));
            g.Clip = new Region(path);
            g.FillEllipse(new SolidBrush(this.BackColor), (int) (x + (width / 4)), (int) (y + (width / 4)), (int) (width / 2), (int) (width / 2));
            if (this.ShadowIsDrawn)
            {
                g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y - this.ShadowOffset.Y, width + 10, width + 10), new Rectangle(0, 0, image2.Width, image2.Height), GraphicsUnit.Pixel);
            }
            g.Clip = new Region();
            g.DrawEllipse(pen, (int) (x + (width / 4)), (int) (y + (width / 4)), (int) (width / 2), (int) (width / 2));
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, 0, 0);
            return bitmap;
        }

        private Bitmap drawExplodedPie()
        {
            double num = 0.0;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDouble(row[this.x[0]]) > 0.0)
                {
                    num += Convert.ToDouble(row[this.x[0]]);
                }
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.graphWidth, this.graphHeight);
            this.xmlChart.CurrentPhase = Phase.Background;
            this.xmlChart.AddBar(this.graphHeight, this.graphWidth, 0f, 0f, new ColorItem(this.BackColor).ToString(), 0xff, 0, 0, string.Empty, string.Empty);
            this.xmlChart.CurrentPhase = Phase.Elements;
            Pen pen = new Pen(Color.Black);
            int num3 = Convert.ToInt32((double) (Math.Min(this.graphWidth, this.graphHeight) * this.explosion));
            int width = (Math.Min(this.graphWidth, this.graphHeight) - 10) - (2 * num3);
            int num5 = (this.graphWidth - width) / 2;
            int num6 = (this.graphHeight - width) / 2;
            float startAngle = 0f;
            System.Drawing.Image image = this.LoadImage(string.Format("pieLight{0}.png", this.effect));
            if (this.ShadowIsDrawn)
            {
                Brush brush = new SolidBrush(Color.FromArgb(70, Color.Black));
                for (int j = 0; j < count; j++)
                {
                    float sweepAngle = (float) ((Convert.ToDouble(this.data.Rows[j][this.x[0]]) / num) * 360.0);
                    double d = ((startAngle + (((double) sweepAngle) / 2.0)) * 3.1415926535897931) / 180.0;
                    int num11 = Convert.ToInt32((double) (num3 * Math.Cos(d)));
                    int num12 = Convert.ToInt32((double) (num3 * Math.Sin(d)));
                    g.FillPie(brush, (float) ((num5 + num11) + this.ShadowOffset.X), (float) ((num6 + num12) + this.ShadowOffset.Y), (float) width, (float) width, startAngle, sweepAngle);
                    startAngle += sweepAngle;
                }
            }
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                float num14 = (float) ((Convert.ToDouble(this.data.Rows[i][this.x[0]]) / num) * 360.0);
                double num15 = ((startAngle + (((double) num14) / 2.0)) * 3.1415926535897931) / 180.0;
                int num16 = Convert.ToInt32((double) (num3 * Math.Cos(num15)));
                int num17 = Convert.ToInt32((double) (num3 * Math.Sin(num15)));
                g.FillPie(new SolidBrush(this.col[i % this.col.Count]), (float) (num5 + num16), (float) (num6 + num17), (float) width, (float) width, startAngle, num14);
                g.DrawPie(pen, (float) (num5 + num16), (float) (num6 + num17), (float) width, (float) width, startAngle, num14);
                GraphicsPath path = new GraphicsPath();
                path.AddPie(num5 + num16, num6 + num17, width, width, startAngle, num14);
                this.xmlChart.AddDonut((-startAngle - num14) + 90f, -startAngle + 90f, width / 2, 0, (num5 + (width / 2)) + num16, (num6 + (width / 2)) + num17, this.col[i % this.col.Count], this.alpha, this.GetNavigateUrl(this.GetID(i) + "||" + i), 1, false, this.GetToolTip(i));
                g.Clip = new Region(path);
                g.DrawImage(image, new Rectangle(num5 + num16, num6 + num17, width, width), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                g.Clip = new Region();
                if ((this.ValueLabelsFormatString != null) && (this.ValueLabelsFormatString != string.Empty))
                {
                    string text = string.Format(this.ValueLabelsFormatString, num14);
                    SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                    int num18 = Convert.ToInt32((double) (((num5 + num16) + (width / 2)) + ((width / 3) * Math.Cos(((startAngle + (num14 / 2f)) * 3.1415926535897931) / 180.0))));
                    int num19 = Convert.ToInt32((double) (((num6 + num17) + (width / 2)) + ((width / 3) * Math.Sin(((startAngle + (num14 / 2f)) * 3.1415926535897931) / 180.0))));
                    list.Add(new Charts.Label(text, num18 - (size.Width / 2f), (num19 - (size.Height / 2f)) + 1f, size));
                }
                startAngle += num14;
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, 0, 0);
            return bitmap;
        }

        private Bitmap drawFullStackedCylinders()
        {
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            decimal heighest = 100M;
            int num3 = this.x.Count;
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num5 = ((float) this.graphWidth) / (count + 1f);
            int width = Convert.ToInt32((double) (num5 * this.PercentWidth));
            int depth = Convert.ToInt32((double) (width * this.Percent3D));
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            int spacer = Convert.ToInt32((double) (num5 - width)) / 2;
            int offsetYTop = 10 + width;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num5));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, 0M, 100M, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num5, depth, this.graphHeight, offsetYTop, spacer, false);
            Pen pen = new Pen(this.ForeColor);
            Brush[] brushArray = new Brush[num3];
            for (int i = 0; i < num3; i++)
            {
                Color baseColor = this.col[i % this.col.Count];
                brushArray[i] = new SolidBrush(Color.FromArgb(this.alpha, baseColor));
            }
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage("barShadow.png");
            List<Charts.Label> list = new List<Charts.Label>();
            for (int j = 0; j < count; j++)
            {
                decimal num12 = 0M;
                for (int k = 0; k < num3; k++)
                {
                    num12 += Convert.ToDecimal(this.data.Rows[j][this.x[k]]);
                }
                int x = Convert.ToInt32((double) (((j + 0.5) * num5) + (0.5 * depth))) + offsetX;
                int y = 0;
                int num16 = 0;
                for (int m = 0; m < num3; m++)
                {
                    int num18 = 0;
                    try
                    {
                        num18 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[m]]) / num12) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    y = ((this.graphHeight - num18) - num16) + offsetYTop;
                    num16 += num18;
                }
                if (this.ShadowIsDrawn)
                {
                    if (num16 < image.Height)
                    {
                        g.DrawImage(image, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num5), (num16 - this.ShadowOffset.Y) - (depth / 2)), new Rectangle(0, 0, image.Width, (num16 - this.ShadowOffset.Y) - (depth / 2)), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(image, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num5), (num16 - this.ShadowOffset.Y) - (depth / 2)));
                    }
                }
                num16 = 0;
                y = 0;
                for (int n = 0; n < num3; n++)
                {
                    int height = 0;
                    try
                    {
                        height = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[n]]) / num12) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    y = (((this.graphHeight - height) - num16) + offsetYTop) - (depth / 2);
                    g.FillEllipse(brushArray[n], x, y - (depth / 2), width, depth);
                    g.DrawArc(pen, x, (y - (depth / 2)) + height, width, depth, 180, 0xe10);
                    GraphicsPath path = new GraphicsPath();
                    path.AddRectangle(new Rectangle(x, y, width, height));
                    path.AddPie((float) x, (y + height) - (((float) depth) / 2f), (float) width, (float) depth, 0f, 180f);
                    path.AddPie((float) x, y - (((float) depth) / 2f), (float) width, (float) depth, 0f, 180f);
                    g.Clip = new Region(path);
                    offsetYTop -= this.negativeOffset;
                    g.FillRectangle(brushArray[n], x, y, width, height + (depth / 2));
                    g.Clip = new Region();
                    g.DrawArc(pen, x, (y - (depth / 2)) + height, width, depth, 0, 180);
                    g.DrawEllipse(pen, x, y - (depth / 2), width, depth);
                    this.xmlChart.AddCylinder(height, width, (float) x, (float) y, this.col[n % this.col.Count].ToString(), this.alpha, depth, 1, this.GetNavigateUrl(this.GetID(j) + "||" + j), this.GetToolTip(j));
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[j][this.x[n]]) / num12);
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, x + ((width - size.Width) / 2f), ((y - size.Height) - depth) + 1f, size));
                    }
                    num16 += height;
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = string.Concat(new object[] { this.GetID(j), "|", this.x[n].str, "|", j });
                    spot.Left = x;
                    spot.Right = x + width;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = (y + height) + this.graphstartsY;
                    spots.Add(spot);
                }
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawFullStackedSurface()
        {
            decimal heighest = 100M;
            decimal lowest = 0M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            int num4 = this.x.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num6 = ((float) this.graphWidth) / (count + 1f);
            int num7 = Convert.ToInt32((double) (num6 / 2.0));
            int offsetYTop = 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num6));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num6, 0, this.graphHeight, offsetYTop, Convert.ToInt32((double) (num6 / 2.0)), true);
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF((float) offsetX, (float) offsetYTop, (float) this.graphWidth, (float) this.graphHeight));
            g.Clip = new Region(path);
            offsetYTop -= this.negativeOffset;
            int[] numArray = new int[num4];
            int[] numArray2 = new int[num4];
            int[] numArray3 = new int[num4];
            numArray3[0] = -2147483648;
            int[] numArray4 = new int[num4];
            decimal[] numArray5 = new decimal[num4];
            decimal[] numArray6 = new decimal[num4];
            for (int i = 0; i < count; i++)
            {
                decimal num10 = 0M;
                for (int j = 0; j < num4; j++)
                {
                    num10 += Convert.ToDecimal(this.data.Rows[i][this.x[j]]);
                }
                decimal num12 = 0M;
                for (int k = 0; k < num4; k++)
                {
                    num12 += Convert.ToDecimal(this.data.Rows[i][this.x[k]]);
                    int num14 = 0;
                    try
                    {
                        num14 = Convert.ToInt32((decimal) ((num12 / num10) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    numArray[k] = Convert.ToInt32((double) (((i + 0.5) * num6) + num7)) + offsetX;
                    numArray2[k] = (this.graphHeight - num14) + offsetYTop;
                    numArray6[k] = num12 / num10;
                    if (numArray3[0] != -2147483648)
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(this.alpha, this.col[k % this.col.Count]));
                        if (k > 0)
                        {
                            Point[] points = new Point[] { new Point(numArray[k], numArray2[k]), new Point(numArray3[k], numArray4[k]), new Point(numArray3[k - 1], numArray4[k - 1]), new Point(numArray[k - 1], numArray2[k - 1]) };
                            g.FillPolygon(brush, points);
                            this.xmlChart.AddQuadrilater(numArray[k], numArray2[k], numArray3[k], numArray4[k], numArray3[k - 1], numArray4[k - 1], numArray[k - 1], numArray2[k - 1], 0, new ColorItem((brush as SolidBrush).Color).ToString(), this.alpha);
                        }
                        else
                        {
                            int y = this.graphHeight + offsetYTop;
                            Point[] pointArray2 = new Point[] { new Point(numArray[k], numArray2[k]), new Point(numArray3[k], numArray4[k]), new Point(numArray3[k], y), new Point(numArray[k], y) };
                            g.FillPolygon(brush, pointArray2);
                            this.xmlChart.AddQuadrilater(numArray[k], numArray2[k], numArray3[k], numArray4[k], numArray3[k], y, numArray[k], y, 0, new ColorItem((brush as SolidBrush).Color).ToString(), this.alpha);
                        }
                    }
                }
                for (int m = 0; m < num4; m++)
                {
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, numArray5[m]);
                        SizeF ef = g.MeasureString(text, this.ValueLabelsFont);
                        g.DrawString(text, this.ValueLabelsFont, Brushes.Black, (float) (numArray3[m] - (ef.Width / 2f)), (float) ((numArray4[m] - ef.Height) + 1f));
                    }
                    numArray3[m] = numArray[m];
                    numArray4[m] = numArray2[m];
                    numArray5[m] = numArray6[m];
                    if ((i == (count - 1)) && (this.ValueLabelsFormatString != string.Empty))
                    {
                        string str2 = string.Format(this.ValueLabelsFormatString, numArray5[m]);
                        SizeF ef2 = g.MeasureString(str2, this.ValueLabelsFont);
                        g.DrawString(str2, this.ValueLabelsFont, Brushes.Black, (float) (numArray3[m] - (ef2.Width / 2f)), (float) ((numArray4[m] - ef2.Height) + 1f));
                    }
                }
            }
            g.Clip = new Region();
            return image;
        }

        private Bitmap drawLine()
        {
            System.Drawing.Image[] imageArray;
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDecimal(row[this.x[0]]) < lowest)
                {
                    lowest = Convert.ToDecimal(row[this.x[0]]);
                }
                if (Convert.ToDecimal(row[this.x[0]]) > heighest)
                {
                    heighest = Convert.ToDecimal(row[this.x[0]]);
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num5 = ((float) this.graphWidth) / (count + 1f);
            int num6 = Convert.ToInt32((double) (num5 / 2.0));
            int offsetYTop = 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num5));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num5, 0, this.graphHeight, offsetYTop, Convert.ToInt32((double) (num5 / 2.0)), true);
            if (this.windowsControl)
            {
                imageArray = new System.Drawing.Image[(this.pointImageList as ImageList).Images.Count];
            }
            else
            {
                imageArray = new System.Drawing.Image[this.pointImagesReal.Count];
            }
            if (this.windowsControl)
            {
                for (int j = 0; j < imageArray.Length; j++)
                {
                    imageArray[j] = (this.pointImageList as ImageList).Images[j];
                }
            }
            else
            {
                for (int k = 0; k < this.pointImagesReal.Count; k++)
                {
                    imageArray[k] = System.Drawing.Image.FromFile(this.pointImagesReal[k].Image);
                }
            }
            Brush brush = new SolidBrush(this.col[0]);
            Pen pen = new Pen(this.col[0], (float) this.LineGraphsLineSize);
            Brush brush2 = new SolidBrush(Color.FromArgb(50, Color.Black));
            Pen pen2 = new Pen(brush2, (float) (this.LineGraphsLineSize + 1));
            Pen pen3 = new Pen(Color.Black, 1f);
            int num10 = -2147483648;
            int num11 = -2147483648;
            HotSpotCollection spots = new HotSpotCollection();
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(offsetX, 0, this.graphWidth, this.graphHeight + offsetYTop));
            g.Clip = new Region(path);
            offsetYTop -= this.negativeOffset;
            for (int i = 0; i < count; i++)
            {
                int num13 = 0;
                try
                {
                    num13 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[i][this.x[0]]) / (heighest - lowest)) * this.graphHeight));
                }
                catch
                {
                }
                int num14 = Convert.ToInt32((double) (((i + 0.5) * num5) + num6)) + offsetX;
                int num15 = (this.graphHeight - num13) + offsetYTop;
                if (this.ShadowIsDrawn && (imageArray.Length == 0))
                {
                    g.FillRectangle(brush2, (int) ((num14 - (this.LineGraphsDataPointSize + 1)) + this.ShadowOffset.X), (int) ((num15 - (this.LineGraphsDataPointSize + 1)) - this.ShadowOffset.Y), (int) (((this.LineGraphsDataPointSize + 1) * 2) + 1), (int) (((this.LineGraphsDataPointSize + 1) * 2) + 1));
                }
                if (num10 != -2147483648)
                {
                    if (this.ShadowIsDrawn && (imageArray.Length == 0))
                    {
                        g.DrawLine(pen2, (int) (num10 + this.ShadowOffset.X), (int) (num11 - this.ShadowOffset.Y), (int) (num14 + this.ShadowOffset.X), (int) (num15 - this.ShadowOffset.Y));
                    }
                    g.DrawLine(pen, num10, num11, num14, num15);
                    this.xmlChart.AddLine(num10, num11, num14, num15, 4, this.col[0].ToString(), this.col[0].color.A);
                    if (imageArray.Length == 0)
                    {
                        g.FillRectangle(brush, (int) (num10 - this.LineGraphsDataPointSize), (int) (num11 - this.LineGraphsDataPointSize), (int) ((this.LineGraphsDataPointSize * 2) + 1), (int) ((this.LineGraphsDataPointSize * 2) + 1));
                        if (this.LineGraphsDataPointSize > 0)
                        {
                            g.DrawRectangle(pen3, (int) (num10 - this.LineGraphsDataPointSize), (int) (num11 - this.LineGraphsDataPointSize), (int) ((this.LineGraphsDataPointSize * 2) + 1), (int) ((this.LineGraphsDataPointSize * 2) + 1));
                        }
                        this.xmlChart.AddBar((this.LineGraphsDataPointSize * 2) + 1, (this.LineGraphsDataPointSize * 2) + 1, (float) (num10 - this.LineGraphsDataPointSize), (float) (num11 - this.LineGraphsDataPointSize), this.col[0].ToString(), this.col[0].color.A, 0, 1, this.GetNavigateUrl(this.GetID(i) + "||" + i), this.GetToolTip(i));
                    }
                    else
                    {
                        g.DrawImage(imageArray[0], (int) (num10 - (imageArray[0].Width / 2)), (int) (num11 - (imageArray[0].Height / 2)));
                    }
                }
                RectangleHotSpot spot = new RectangleHotSpot();
                if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                {
                    spot.AlternateText = this.data.Rows[i][this.XAxisLabels].ToString();
                }
                spot.PostBackValue = this.GetID(i) + "||" + i;
                spot.Left = num14 - 5;
                spot.Right = num14 + 6;
                spot.Top = (num15 + this.graphstartsY) - 5;
                spot.Bottom = (num15 + 6) + this.graphstartsY;
                spots.Add(spot);
                num10 = num14;
                num11 = num15;
            }
            if (num10 != -2147483648)
            {
                if (imageArray.Length == 0)
                {
                    g.FillRectangle(brush, (int) (num10 - this.LineGraphsDataPointSize), (int) (num11 - this.LineGraphsDataPointSize), (int) ((this.LineGraphsDataPointSize * 2) + 1), (int) ((this.LineGraphsDataPointSize * 2) + 1));
                    if (this.LineGraphsDataPointSize > 0)
                    {
                        g.DrawRectangle(pen3, (int) (num10 - this.LineGraphsDataPointSize), (int) (num11 - this.LineGraphsDataPointSize), (int) ((this.LineGraphsDataPointSize * 2) + 1), (int) ((this.LineGraphsDataPointSize * 2) + 1));
                    }
                    this.xmlChart.AddBar((this.LineGraphsDataPointSize * 2) + 1, (this.LineGraphsDataPointSize * 2) + 1, (float) (num10 - this.LineGraphsDataPointSize), (float) (num11 - this.LineGraphsDataPointSize), this.col[0].ToString(), this.col[0].color.A, 0, 1, this.GetNavigateUrl(this.GetID(0) + "||" + 0), this.GetToolTip(0));
                }
                else
                {
                    g.DrawImage(imageArray[0], (int) (num10 - (imageArray[0].Width / 2)), (int) (num11 - (imageArray[0].Height / 2)));
                }
            }
            int num16 = 5;
            if (imageArray.Length > 0)
            {
                num16 = imageArray[0].Height / 2;
            }
            if (this.ValueLabelsFormatString != string.Empty)
            {
                for (int m = 0; m < count; m++)
                {
                    int num18 = 0;
                    try
                    {
                        num18 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[m][this.x[0]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    int num19 = Convert.ToInt32((double) (((m + 0.5) * num5) + num6)) + offsetX;
                    int num20 = (this.graphHeight - num18) + offsetYTop;
                    string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[m][this.x[0]]));
                    SizeF ef = g.MeasureString(text, this.ValueLabelsFont);
                    g.DrawString(text, this.ValueLabelsFont, Brushes.Black, (float) (num19 - (ef.Width / 2f)), (float) ((num20 - ef.Height) - num16));
                }
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return image;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return image;
        }

        private Bitmap DrawMap()
        {
            System.Drawing.Image winUserImage;
            DataTable table = new DataTable("Element");
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("R", typeof(byte));
            table.Columns.Add("G", typeof(byte));
            table.Columns.Add("B", typeof(byte));
            table.Columns.Add("X", typeof(int));
            table.Columns.Add("Y", typeof(int));
            table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };
            if (!this.windowsControl)
            {
                table.ReadXml(this.mapXMlPath);
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(this.mapXml);
                writer.Flush();
                StreamReader reader = new StreamReader(stream);
                stream.Position = 0L;
                table.ReadXml(reader);
                writer.Close();
                writer.Dispose();
                stream.Close();
                stream.Dispose();
                reader.Close();
                reader.Dispose();
            }
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            List<string> list = new List<string>();
            if (this.IDCategory != string.Empty)
            {
                for (int k = 0; k < count; k++)
                {
                    if (!list.Contains(this.data.Rows[k][this.IDCategory].ToString()))
                    {
                        list.Add(this.data.Rows[k][this.IDCategory].ToString());
                    }
                }
            }
            double minValue = double.MinValue;
            foreach (DataRow row in this.data.Rows)
            {
                if (Convert.ToDouble(row[this.x[0]]) > minValue)
                {
                    minValue = Convert.ToDouble(row[this.x[0]]);
                }
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = this.smoothingMode;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.graphWidth, this.graphHeight);
            List<ColorMap> list2 = new List<ColorMap>();
            for (int i = 0; i < count; i++)
            {
                string key = this.data.Rows[i][this.IDField].ToString();
                DataRow row2 = table.Rows.Find(key);
                if (row2 != null)
                {
                    int alpha = 0;
                    try
                    {
                        alpha = Convert.ToInt32((double) ((Convert.ToDouble(this.data.Rows[i][this.x[0]]) / minValue) * 255.0));
                    }
                    catch
                    {
                    }
                    ColorMap item = new ColorMap();
                    item.OldColor = Color.FromArgb((byte) row2["R"], (byte) row2["G"], (byte) row2["B"]);
                    if (this.IDCategory != string.Empty)
                    {
                        int index = list.IndexOf(this.data.Rows[i][this.IDCategory].ToString());
                        item.NewColor = Color.FromArgb(alpha, this.col[index % this.col.Count]);
                    }
                    else
                    {
                        item.NewColor = Color.FromArgb(alpha, this.col[0]);
                    }
                    list2.Add(item);
                    table.Rows.Remove(row2);
                }
            }
            foreach (DataRow row3 in table.Rows)
            {
                ColorMap map2 = new ColorMap();
                map2.OldColor = Color.FromArgb((byte) row3["R"], (byte) row3["G"], (byte) row3["B"]);
                map2.NewColor = Color.FromArgb(0, this.col[0]);
                list2.Add(map2);
            }
            if (this.WinUserImage != null)
            {
                winUserImage = this.WinUserImage as System.Drawing.Image;
            }
            else
            {
                winUserImage = System.Drawing.Image.FromFile(this.userImagePath);
            }
            ColorMap[] mapArray = new ColorMap[list2.Count];
            for (int j = 0; j < list2.Count; j++)
            {
                mapArray[j] = list2[j];
            }
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetRemapTable(mapArray);
            graphics.DrawImage(winUserImage, new Rectangle(0, 0, this.graphWidth, this.graphHeight), 0, 0, winUserImage.Width, winUserImage.Height, GraphicsUnit.Pixel, imageAttr);
            if (((this.XAxisLabels != null) && (this.XAxisLabels != string.Empty)) || ((this.ValueLabelsFormatString != null) && (this.ValueLabelsFormatString != string.Empty)))
            {
                Brush brush = new SolidBrush(Color.Black);
                table.Rows.Clear();
                if (!this.windowsControl)
                {
                    table.ReadXml(this.mapXMlPath);
                }
                else
                {
                    MemoryStream stream2 = new MemoryStream();
                    StreamWriter writer2 = new StreamWriter(stream2);
                    writer2.Write(this.mapXml);
                    writer2.Flush();
                    StreamReader reader2 = new StreamReader(stream2);
                    stream2.Position = 0L;
                    table.ReadXml(reader2);
                    writer2.Close();
                    writer2.Dispose();
                    stream2.Close();
                    stream2.Dispose();
                    reader2.Close();
                    reader2.Dispose();
                }
                for (int m = 0; m < count; m++)
                {
                    string str2 = this.data.Rows[m][this.IDField].ToString();
                    DataRow row4 = table.Rows.Find(str2);
                    if (row4 != null)
                    {
                        try
                        {
                            if ((this.XAxisLabels != null) && (this.XAxisLabels != string.Empty))
                            {
                                string text = this.data.Rows[m][this.XAxisLabels].ToString();
                                SizeF ef = graphics.MeasureString(text, this.LabelsFont);
                                int num9 = Convert.ToInt32((float) (((((int) row4["X"]) * this.graphWidth) / winUserImage.Width) - (ef.Width / 2f)));
                                int num10 = Convert.ToInt32((float) (((((int) row4["Y"]) * this.graphHeight) / winUserImage.Height) - ef.Height));
                                graphics.DrawString(text, this.LabelsFont, brush, new PointF((float) num9, (float) num10));
                            }
                            if ((this.ValueLabelsFormatString != null) && (this.ValueLabelsFormatString != string.Empty))
                            {
                                string str4 = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[m][this.x[0]]));
                                SizeF ef2 = graphics.MeasureString(str4, this.LabelsFont);
                                int num11 = Convert.ToInt32((float) (((((int) row4["X"]) * this.graphWidth) / winUserImage.Width) - (ef2.Width / 2f)));
                                int num12 = Convert.ToInt32((int) ((((int) row4["Y"]) * this.graphHeight) / winUserImage.Height));
                                graphics.DrawString(str4, this.LabelsFont, brush, new PointF((float) num11, (float) num12));
                            }
                            table.Rows.Remove(row4);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return bitmap;
        }

        private Bitmap DrawMapPoints()
        {
            Bitmap winUserImage;
            Graphics graphics;
            System.Drawing.Image[] imageArray;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            List<string> list = new List<string>();
            if (this.IDCategory != string.Empty)
            {
                for (int k = 0; k < count; k++)
                {
                    if (!list.Contains(this.data.Rows[k][this.IDCategory].ToString()))
                    {
                        list.Add(this.data.Rows[k][this.IDCategory].ToString());
                    }
                }
            }
            if ((this.WinUserImage == null) && ((this.userImage == string.Empty) || (this.userImage == null)))
            {
                try
                {
                    if (!this.windowsControl)
                    {
                        winUserImage = BitmapWriter.Download(this.mapXMlPath, this.graphWidth, this.graphHeight, this.lat1, this.lat2, this.lon1, this.lon2);
                    }
                    else
                    {
                        winUserImage = BitmapWriter.WinDownload(this.mapXml, this.graphWidth, this.graphHeight, this.lat1, this.lat2, this.lon1, this.lon2);
                    }
                }
                catch
                {
                    throw new Exception("ERROR: Could not download MAP from WMS Server.");
                }
                graphics = Graphics.FromImage(winUserImage);
                graphics.SmoothingMode = this.smoothingMode;
            }
            else if (this.WinUserImage != null)
            {
                winUserImage = this.WinUserImage as Bitmap;
                graphics = Graphics.FromImage(winUserImage);
                graphics.SmoothingMode = this.smoothingMode;
            }
            else
            {
                winUserImage = new Bitmap(this.graphWidth, this.graphHeight);
                graphics = Graphics.FromImage(winUserImage);
                graphics.SmoothingMode = this.smoothingMode;
                System.Drawing.Image image = System.Drawing.Image.FromFile(this.userImagePath);
                graphics.DrawImage(image, new Rectangle(0, 0, this.graphWidth, this.graphHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            }
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            Brush[] brushArray = new SolidBrush[this.col.Count];
            for (int i = 0; i < this.col.Count; i++)
            {
                brushArray[i] = new SolidBrush(this.col[i]);
            }
            if (this.windowsControl)
            {
                imageArray = new System.Drawing.Image[(this.pointImageList as ImageList).Images.Count];
            }
            else
            {
                imageArray = new System.Drawing.Image[this.pointImagesReal.Count];
            }
            if (this.windowsControl)
            {
                for (int m = 0; m < imageArray.Length; m++)
                {
                    imageArray[m] = (this.pointImageList as ImageList).Images[m];
                }
            }
            else
            {
                for (int n = 0; n < this.pointImagesReal.Count; n++)
                {
                    imageArray[n] = System.Drawing.Image.FromFile(this.pointImagesReal[n].Image);
                }
            }
            Brush brush = new SolidBrush(Color.FromArgb(100, Color.Black));
            Brush brush2 = new SolidBrush(Color.Black);
            Pen pen = new Pen(Color.Black, 1f);
            HotSpotCollection spots = new HotSpotCollection();
            for (int j = 0; j < count; j++)
            {
                decimal num7 = Convert.ToDecimal(this.data.Rows[j][this.x[0]]);
                decimal num8 = Convert.ToDecimal(this.data.Rows[j][this.x[1]]);
                if (num8 > 180M)
                {
                    num8 -= 360M;
                }
                int num9 = Convert.ToInt32((decimal) ((winUserImage.Width * (num8 - this.lon1)) / (this.lon2 - this.lon1)));
                int num10 = Convert.ToInt32((decimal) ((winUserImage.Height * (this.lat2 - num7)) / (this.lat2 - this.lat1)));
                if (this.ShadowIsDrawn && (imageArray.Length == 0))
                {
                    graphics.FillRectangle(brush, new Rectangle((num9 - 3) + this.ShadowOffset.X, (num10 - 3) - this.ShadowOffset.Y, 7, 7));
                }
                if (imageArray.Length == 0)
                {
                    if (this.IDCategory != string.Empty)
                    {
                        int index = list.IndexOf(this.data.Rows[j][this.IDCategory].ToString());
                        graphics.FillRectangle(brushArray[index % this.col.Count], new Rectangle(num9 - 3, num10 - 3, 7, 7));
                    }
                    else
                    {
                        graphics.FillRectangle(brushArray[0], new Rectangle(num9 - 3, num10 - 3, 7, 7));
                    }
                    graphics.DrawRectangle(pen, new Rectangle(num9 - 3, num10 - 3, 7, 7));
                    if ((this.XAxisLabels != null) && (this.XAxisLabels != string.Empty))
                    {
                        SizeF ef = graphics.MeasureString(this.data.Rows[j][this.XAxisLabels].ToString(), this.LabelsFont);
                        graphics.DrawString(this.data.Rows[j][this.XAxisLabels].ToString(), this.LabelsFont, brush2, num9 - (ef.Width / 2f), (float) (num10 + 5));
                    }
                }
                else
                {
                    System.Drawing.Image image2;
                    if (this.IDCategory != string.Empty)
                    {
                        int num12 = list.IndexOf(this.data.Rows[j][this.IDCategory].ToString());
                        image2 = imageArray[num12 % this.pointImagesReal.Count];
                    }
                    else
                    {
                        image2 = imageArray[0];
                    }
                    graphics.DrawImage(image2, (int) (num9 - (image2.Width / 2)), (int) (num10 - (image2.Height / 2)));
                    if ((this.XAxisLabels != null) && (this.XAxisLabels != string.Empty))
                    {
                        SizeF ef2 = graphics.MeasureString(this.data.Rows[j][this.XAxisLabels].ToString(), this.LabelsFont);
                        graphics.DrawString(this.data.Rows[j][this.XAxisLabels].ToString(), this.LabelsFont, brush2, num9 - (ef2.Width / 2f), (float) ((num10 + 5) + (image2.Height / 2)));
                    }
                }
                if ((this.IDField != null) && (this.IDField != string.Empty))
                {
                    string str = this.data.Rows[j][this.IDField].ToString();
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = str;
                    spot.Left = num9 - 3;
                    spot.Right = num9 + 3;
                    spot.Top = (num10 - 3) + this.graphstartsY;
                    spot.Bottom = (num10 + 3) + this.graphstartsY;
                    spots.Add(spot);
                }
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return winUserImage;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return winUserImage;
        }

        private Bitmap drawMulti3DLine()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            int num4 = this.x.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                for (int j = 0; j < num4; j++)
                {
                    if (row[this.x[j]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[j] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) < lowest)
                    {
                        lowest = Convert.ToDecimal(row[this.x[j]]);
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) > heighest)
                    {
                        heighest = Convert.ToDecimal(row[this.x[j]]);
                    }
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num7 = ((float) this.graphWidth) / ((count + 0f) + num4);
            int num8 = Convert.ToInt32((double) (num7 / 2.0));
            int num9 = Convert.ToInt32((double) (num7 * this.Percent3D));
            int offsetYTop = 10 + (num4 * num9);
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num7));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, num4 * num9);
            this.drawXAxis(g, offsetX, (float) num7, num4 * num9, this.graphHeight, offsetYTop, Convert.ToInt32((double) (num7 / 2.0)), true);
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(offsetX, 0, this.graphWidth, this.graphHeight + offsetYTop));
            g.Clip = new Region(path);
            offsetYTop -= this.negativeOffset;
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < num4; i++)
            {
                Brush brush2 = new SolidBrush(Color.FromArgb(this.alpha, this.col[i % this.col.Count]));
                int num12 = -2147483648;
                int num13 = -2147483648;
                for (int k = 0; k < count; k++)
                {
                    int num15 = 0;
                    try
                    {
                        num15 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[k][this.x[i]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    int num16 = Convert.ToInt32((double) (((k + 0.5) * num7) + num8)) + offsetX;
                    int num17 = (this.graphHeight - num15) + offsetYTop;
                    if (num12 != -2147483648)
                    {
                        if (this.ShadowIsDrawn)
                        {
                            Point[] pointArray = new Point[] { new Point((num12 + this.ShadowOffset.X) + (((num4 - i) - 1) * num9), (num13 + this.ShadowOffset.Y) - (((num4 - i) - 1) * num9)), new Point((num16 + this.ShadowOffset.X) + (((num4 - i) - 1) * num9), (num17 + this.ShadowOffset.Y) - (((num4 - i) - 1) * num9)), new Point((num16 + this.ShadowOffset.X) + ((num4 - i) * num9), (num17 + this.ShadowOffset.Y) - ((num4 - i) * num9)), new Point((num12 + this.ShadowOffset.X) + ((num4 - i) * num9), (num13 + this.ShadowOffset.Y) - ((num4 - i) * num9)) };
                            g.FillPolygon(brush, pointArray);
                        }
                        Point[] points = new Point[] { new Point(num12 + (((num4 - i) - 1) * num9), num13 - (((num4 - i) - 1) * num9)), new Point(num16 + (((num4 - i) - 1) * num9), num17 - (((num4 - i) - 1) * num9)), new Point(num16 + ((num4 - i) * num9), num17 - ((num4 - i) * num9)), new Point(num12 + ((num4 - i) * num9), num13 - ((num4 - i) * num9)) };
                        g.FillPolygon(brush2, points);
                        this.xmlChart.AddQuadrilater(num12 + (((num4 - i) - 1) * num9), num13 - (((num4 - i) - 1) * num9), num16 + (((num4 - i) - 1) * num9), num17 - (((num4 - i) - 1) * num9), num16 + ((num4 - i) * num9), num17 - ((num4 - i) * num9), num12 + ((num4 - i) * num9), num13 - ((num4 - i) * num9), 0, new ColorItem((brush2 as SolidBrush).Color).ToString(), this.alpha);
                    }
                    num12 = num16;
                    num13 = num17;
                }
                if (this.ValueLabelsFormatString != string.Empty)
                {
                    for (int m = 0; m < count; m++)
                    {
                        int num19 = 0;
                        try
                        {
                            num19 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[m][this.x[i]]) / (heighest - lowest)) * this.graphHeight));
                        }
                        catch
                        {
                        }
                        int num20 = Convert.ToInt32((double) (((m + 0.5) * num7) + num8)) + offsetX;
                        int num21 = (this.graphHeight - num19) + offsetYTop;
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[m][this.x[i]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, (num20 - (size.Width / 2f)) + (((num4 - i) - 1) * num9), ((num21 - size.Height) - (((num4 - i) - 1) * num9)) + 1f, size));
                    }
                    Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, 10);
                }
            }
            return image;
        }

        private Bitmap drawMultiBars()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num4 = this.x.Count;
            foreach (DataRow row in this.data.Rows)
            {
                for (int k = 0; k < num4; k++)
                {
                    if (row[this.x[k]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[k] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    if (Convert.ToDecimal(row[this.x[k]]) < lowest)
                    {
                        lowest = Convert.ToDecimal(row[this.x[k]]);
                    }
                    if (Convert.ToDecimal(row[this.x[k]]) > heighest)
                    {
                        heighest = Convert.ToDecimal(row[this.x[k]]);
                    }
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            int offsetYTop = 10;
            double num8 = ((float) this.graphWidth) / (count + 1f);
            int width = Convert.ToInt32((double) (num8 * this.PercentWidth)) / num4;
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            int spacer = Convert.ToInt32((double) (num8 - (num4 * width))) / 2;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num8));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num8, 0, this.graphHeight, offsetYTop, spacer, false);
            offsetYTop -= this.negativeOffset;
            Brush[] brushArray = new Brush[num4];
            for (int i = 0; i < num4; i++)
            {
                brushArray[i] = new SolidBrush(this.col[i % this.col.Count]);
            }
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage("barShadow.png");
            for (int j = 0; j < count; j++)
            {
                for (int m = 0; m < num4; m++)
                {
                    int height = 0;
                    try
                    {
                        height = Convert.ToInt32((decimal) ((ScaleBreaks.ValRec(this.data.Rows[j][this.x[m]], this.ScaleBreakMin, this.ScaleBreakMax) / (((heighest - lowest) + this.ScaleBreakMin) - this.ScaleBreakMax)) * (this.graphHeight - this.ScaleBreakHeight)));
                    }
                    catch
                    {
                    }
                    if (height != 0)
                    {
                        int x = Convert.ToInt32((double) (((j + 0.5) * num8) + (m * width))) + offsetX;
                        int y = (this.graphHeight - height) + offsetYTop;
                        if (y < this.ScaleBreakPos)
                        {
                            height += this.ScaleBreakHeight;
                            y -= this.ScaleBreakHeight;
                        }
                        if (height > 0)
                        {
                            if (this.ShadowIsDrawn)
                            {
                                if (height < image2.Height)
                                {
                                    g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, height - this.ShadowOffset.Y), new Rectangle(0, 0, image2.Width, height - this.ShadowOffset.Y), GraphicsUnit.Pixel);
                                }
                                else
                                {
                                    g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, height - this.ShadowOffset.Y));
                                }
                            }
                            g.FillRectangle(brushArray[m], x, y, width, height);
                            g.DrawImage(image, new Rectangle(x, y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                            this.xmlChart.AddBar(height, width, (float) x, (float) y, new ColorItem((brushArray[m] as SolidBrush).Color).ToString(), (brushArray[m] as SolidBrush).Color.A, 0, 0, this.GetNavigateUrl(string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j })), this.GetToolTip(j));
                        }
                        else
                        {
                            if (this.ShadowIsDrawn)
                            {
                                if (height < image2.Height)
                                {
                                    g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, (y + height) + this.ShadowOffset.Y, width, -height - this.ShadowOffset.Y), new Rectangle(0, height, image2.Width, -height - this.ShadowOffset.Y), GraphicsUnit.Pixel);
                                }
                                else
                                {
                                    g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, (y + height) + this.ShadowOffset.Y, width, -height - this.ShadowOffset.Y));
                                }
                            }
                            g.FillRectangle(brushArray[m], x, y + height, width, -height);
                            g.DrawImage(image, new Rectangle(x, y + height, width, -height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                            this.xmlChart.AddBar(-height, width, (float) x, (float) (y + height), new ColorItem((brushArray[m] as SolidBrush).Color).ToString(), (brushArray[m] as SolidBrush).Color.A, 0, 0, this.GetNavigateUrl(string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j })), this.GetToolTip(j));
                        }
                        RectangleHotSpot spot = new RectangleHotSpot();
                        if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                        {
                            spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                        }
                        spot.PostBackValue = string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j });
                        spot.Left = x;
                        spot.Right = x + width;
                        spot.Top = y + this.graphstartsY;
                        spot.Bottom = (y + height) + this.graphstartsY;
                        spots.Add(spot);
                    }
                }
            }
            if (this.ScaleBreakMax > this.ScaleBreakMin)
            {
                ScaleBreaks.drawScaleBreak(this.xmlChart, g, this.BackColor2, this.ScaleBreakPos, this.ScaleBreakHeight, offsetX - 1, this.graphWidth, 0);
            }
            if (this.ValueLabelsFormatString != string.Empty)
            {
                List<Charts.Label> list = new List<Charts.Label>();
                for (int n = 0; n < count; n++)
                {
                    for (int num18 = 0; num18 < num4; num18++)
                    {
                        int num19 = 0;
                        try
                        {
                            num19 = Convert.ToInt32((decimal) ((ScaleBreaks.ValRec(this.data.Rows[n][this.x[num18]], this.ScaleBreakMin, this.ScaleBreakMax) / (((heighest - lowest) + this.ScaleBreakMin) - this.ScaleBreakMax)) * (this.graphHeight - this.ScaleBreakHeight)));
                        }
                        catch
                        {
                        }
                        int num20 = Convert.ToInt32((double) (((n + 0.5) * num8) + (num18 * width))) + offsetX;
                        int num21 = (this.graphHeight - num19) + offsetYTop;
                        if (num21 < this.ScaleBreakPos)
                        {
                            num19 += this.ScaleBreakHeight;
                            num21 -= this.ScaleBreakHeight;
                        }
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[n][this.x[num18]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, num20 + ((width - size.Width) / 2f), (num21 - size.Height) + 1f, size));
                    }
                }
                Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawMultiBars3D()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num4 = this.x.Count;
            foreach (DataRow row in this.data.Rows)
            {
                for (int k = 0; k < num4; k++)
                {
                    if (row[this.x[k]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[k] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    if (Convert.ToDecimal(row[this.x[k]]) < lowest)
                    {
                        lowest = Convert.ToDecimal(row[this.x[k]]);
                    }
                    if (Convert.ToDecimal(row[this.x[k]]) > heighest)
                    {
                        heighest = Convert.ToDecimal(row[this.x[k]]);
                    }
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            double num6 = ((float) this.graphWidth) / (count + 1f);
            int depth = Convert.ToInt32((double) (Convert.ToInt32((double) (num6 * this.PercentWidth)) * this.Percent3D));
            int offsetX = this.ComputeYAxisSpace(g, heighest) + depth;
            this.graphWidth -= offsetX;
            int offsetYTop = depth + 10;
            num6 = ((float) this.graphWidth) / (count + 1f);
            int width = Convert.ToInt32((double) (num6 * this.PercentWidth));
            depth = Convert.ToInt32((double) (width * this.Percent3D));
            width = (width - depth) / num4;
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            int spacer = Convert.ToInt32((double) (num6 - (num4 * width))) / 2;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num6));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num6, depth, this.graphHeight, offsetYTop, spacer, false);
            offsetYTop -= this.negativeOffset;
            Brush[] brushArray = new Brush[num4];
            Brush[] brushArray2 = new Brush[num4];
            Brush[] brushArray3 = new Brush[num4];
            for (int i = 0; i < num4; i++)
            {
                Color baseColor = this.col[i % this.col.Count];
                brushArray[i] = new SolidBrush(Color.FromArgb(this.alpha, baseColor));
                brushArray2[i] = new SolidBrush(Color.FromArgb((this.alpha * 3) / 4, baseColor));
                brushArray3[i] = new SolidBrush(Color.FromArgb(this.alpha / 2, baseColor));
            }
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage("barShadow.png");
            List<Charts.Label> list = new List<Charts.Label>();
            for (int j = 0; j < count; j++)
            {
                for (int m = 0; m < num4; m++)
                {
                    int height = 0;
                    try
                    {
                        height = Convert.ToInt32((decimal) ((ScaleBreaks.ValRec(this.data.Rows[j][this.x[m]], this.ScaleBreakMin, this.ScaleBreakMax) / (((heighest - lowest) + this.ScaleBreakMin) - this.ScaleBreakMax)) * (this.graphHeight - this.ScaleBreakHeight)));
                    }
                    catch
                    {
                    }
                    if (height != 0)
                    {
                        int x = Convert.ToInt32((double) (((j + 0.5) * num6) + (m * width))) + offsetX;
                        int y = (this.graphHeight - height) + offsetYTop;
                        if (y < this.ScaleBreakPos)
                        {
                            height += this.ScaleBreakHeight;
                            y -= this.ScaleBreakHeight;
                        }
                        if (this.ShadowIsDrawn)
                        {
                            if (height < image2.Height)
                            {
                                g.DrawImage(image2, new Rectangle((x + this.ShadowOffset.X) + depth, y + this.ShadowOffset.Y, width + depth, (height - this.ShadowOffset.Y) - depth), new Rectangle(0, 0, image2.Width, (height - this.ShadowOffset.Y) - depth), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                g.DrawImage(image2, new Rectangle((x + this.ShadowOffset.X) + depth, y + this.ShadowOffset.Y, width + depth, (height - this.ShadowOffset.Y) - depth));
                            }
                        }
                        if (height > 0)
                        {
                            g.FillRectangle(brushArray[m], x, y, width, height);
                            g.DrawImage(image, new Rectangle(x, y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                            Point[] points = new Point[] { new Point(x + width, y), new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point((x + width) + depth, y - depth) };
                            Point[] destPoints = new Point[] { points[0], points[3], points[1] };
                            g.FillPolygon(brushArray2[m], points);
                            g.DrawImage(image, destPoints, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                            Point[] pointArray3 = new Point[] { new Point(x + width, y), new Point((x + width) + depth, y - depth), new Point(x + depth, y - depth), new Point(x, y) };
                            Point[] pointArray4 = new Point[] { pointArray3[0], pointArray3[3], pointArray3[1] };
                            g.FillPolygon(brushArray3[m], pointArray3);
                            g.DrawImage(image, pointArray4, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                        }
                        else
                        {
                            g.FillRectangle(brushArray[m], x, y + height, width, -height);
                            g.DrawImage(image, new Rectangle(x, y + height, width, -height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                            Point[] pointArray5 = new Point[] { new Point(x + width, y), new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point((x + width) + depth, y - depth) };
                            Point[] pointArray6 = new Point[] { pointArray5[0], pointArray5[3], pointArray5[1] };
                            g.FillPolygon(brushArray2[m], pointArray5);
                            g.DrawImage(image, pointArray6, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                            Point[] pointArray7 = new Point[] { new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point(x + depth, (y + height) - depth), new Point(x, y + height) };
                            Point[] pointArray8 = new Point[] { pointArray7[0], pointArray7[3], pointArray7[1] };
                            g.FillPolygon(brushArray3[m], pointArray7);
                            g.DrawImage(image, pointArray8, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                        }
                        this.xmlChart.AddBar(height, width, (float) x, (float) y, new ColorItem((brushArray[m] as SolidBrush).Color).ToString(), (brushArray[m] as SolidBrush).Color.A, depth, 0, this.GetNavigateUrl(string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j })), this.GetToolTip(j));
                        if (this.ValueLabelsFormatString != string.Empty)
                        {
                            string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[j][this.x[m]]));
                            SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                            list.Add(new Charts.Label(text, x + (((width - size.Width) + depth) / 2f), ((y - size.Height) + 1f) - (depth / 2), size));
                        }
                        RectangleHotSpot spot = new RectangleHotSpot();
                        if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                        {
                            spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                        }
                        spot.PostBackValue = string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j });
                        spot.Left = x;
                        spot.Right = x + width;
                        spot.Top = y + this.graphstartsY;
                        spot.Bottom = (y + height) + this.graphstartsY;
                        spots.Add(spot);
                    }
                }
            }
            if (this.ScaleBreakMax > this.ScaleBreakMin)
            {
                ScaleBreaks.drawScaleBreak(this.xmlChart, g, this.BackColor2, this.ScaleBreakPos, this.ScaleBreakHeight, offsetX - 1, this.graphWidth, depth);
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawMultiCurve()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            int num4 = this.x.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                for (int j = 0; j < num4; j++)
                {
                    if (row[this.x[j]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[j] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) < lowest)
                    {
                        lowest = Convert.ToDecimal(row[this.x[j]]);
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) > heighest)
                    {
                        heighest = Convert.ToDecimal(row[this.x[j]]);
                    }
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num7 = ((float) this.graphWidth) / (count + 1f);
            int num8 = Convert.ToInt32((double) (num7 / 2.0));
            int offsetYTop = 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num7));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num7, 0, this.graphHeight, offsetYTop, Convert.ToInt32((double) (num7 / 2.0)), true);
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
            Pen pen = new Pen(brush, (float) (this.LineGraphsLineSize + 1));
            Pen pen2 = new Pen(Color.Black, 1f);
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(offsetX, 0, this.graphWidth, this.graphHeight + offsetYTop));
            g.Clip = new Region(path);
            offsetYTop -= this.negativeOffset;
            Point[] points = new Point[count];
            Point[] pointArray2 = new Point[count];
            HotSpotCollection spots = new HotSpotCollection();
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < num4; i++)
            {
                Brush brush2 = new SolidBrush(this.col[i % this.col.Count]);
                Pen pen3 = new Pen(brush2, (float) this.LineGraphsLineSize);
                for (int k = 0; k < count; k++)
                {
                    int num12 = 0;
                    try
                    {
                        num12 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[k][this.x[i]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    int x = Convert.ToInt32((double) (((k + 0.5) * num7) + num8)) + offsetX;
                    int y = (this.graphHeight - num12) + offsetYTop;
                    points[k] = new Point(x, y);
                    pointArray2[k] = new Point(x + this.ShadowOffset.X, y + this.ShadowOffset.Y);
                }
                if (this.ShadowIsDrawn)
                {
                    g.DrawCurve(pen, pointArray2, 0.5f);
                    foreach (Point point in pointArray2)
                    {
                        g.FillRectangle(brush, new Rectangle(point.X - (this.LineGraphsDataPointSize + 1), point.Y - (this.LineGraphsDataPointSize + 1), ((this.LineGraphsDataPointSize + 1) * 2) + 1, ((this.LineGraphsDataPointSize + 1) * 2) + 1));
                    }
                }
                g.DrawCurve(pen3, points, 0.5f);
                int num15 = 0;
                foreach (Point point2 in points)
                {
                    g.FillRectangle(brush2, new Rectangle(point2.X - this.LineGraphsDataPointSize, point2.Y - this.LineGraphsDataPointSize, (this.LineGraphsDataPointSize * 2) + 1, (this.LineGraphsDataPointSize * 2) + 1));
                    if (this.LineGraphsDataPointSize > 0)
                    {
                        g.DrawRectangle(pen2, new Rectangle(point2.X - this.LineGraphsDataPointSize, point2.Y - this.LineGraphsDataPointSize, (this.LineGraphsDataPointSize * 2) + 1, (this.LineGraphsDataPointSize * 2) + 1));
                    }
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[num15][this.x[i]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, point2.X - (size.Width / 2f), ((point2.Y - size.Height) - 5f) + 1f, size));
                    }
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[num15][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = string.Concat(new object[] { this.GetID(num15), "|", this.x[i].str, "|", num15 });
                    spot.Left = point2.X - 5;
                    spot.Right = point2.X + 5;
                    spot.Top = (point2.Y + this.graphstartsY) - 5;
                    spot.Bottom = (point2.Y + 5) + this.graphstartsY;
                    spots.Add(spot);
                    num15++;
                }
                Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, 10);
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return image;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return image;
        }

        private Bitmap drawMultiCylinders()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num4 = this.x.Count;
            foreach (DataRow row in this.data.Rows)
            {
                for (int k = 0; k < num4; k++)
                {
                    if (Convert.ToDecimal(row[this.x[k]]) < lowest)
                    {
                        lowest = Convert.ToDecimal(row[this.x[k]]);
                    }
                    if (Convert.ToDecimal(row[this.x[k]]) > heighest)
                    {
                        heighest = Convert.ToDecimal(row[this.x[k]]);
                    }
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            double num6 = ((float) this.graphWidth) / (count + 1f);
            int depth = Convert.ToInt32((double) ((Convert.ToInt32((double) (num6 * this.PercentWidth)) / num4) * this.Percent3D));
            int offsetX = this.ComputeYAxisSpace(g, heighest) + depth;
            this.graphWidth -= offsetX;
            int offsetYTop = depth + 10;
            num6 = ((float) this.graphWidth) / (count + 1f);
            int width = Convert.ToInt32((double) (num6 * this.PercentWidth));
            depth = Convert.ToInt32((double) ((width / num4) * this.Percent3D));
            width = (width - depth) / num4;
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            int spacer = Convert.ToInt32((double) (num6 - (num4 * width))) / 2;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num6));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num6, depth, this.graphHeight, offsetYTop, spacer, false);
            offsetYTop -= this.negativeOffset;
            Pen pen = new Pen(this.ForeColor);
            Brush[] brushArray = new Brush[num4];
            for (int i = 0; i < num4; i++)
            {
                Color baseColor = this.col[i % this.col.Count];
                brushArray[i] = new SolidBrush(Color.FromArgb(this.alpha, baseColor));
            }
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage("barShadow.png");
            List<Charts.Label> list = new List<Charts.Label>();
            for (int j = 0; j < count; j++)
            {
                for (int m = 0; m < num4; m++)
                {
                    int height = 0;
                    try
                    {
                        height = Convert.ToInt32((decimal) ((ScaleBreaks.ValRec(this.data.Rows[j][this.x[m]], this.ScaleBreakMin, this.ScaleBreakMax) / (((heighest - lowest) + this.ScaleBreakMin) - this.ScaleBreakMax)) * (this.graphHeight - this.ScaleBreakHeight)));
                    }
                    catch
                    {
                    }
                    if (height != 0)
                    {
                        int x = Convert.ToInt32((double) (((j + 0.5) * num6) + (m * width))) + offsetX;
                        int y = ((this.graphHeight - height) + offsetYTop) - (depth / 2);
                        if (y < this.ScaleBreakPos)
                        {
                            height += this.ScaleBreakHeight;
                            y -= this.ScaleBreakHeight;
                        }
                        if (this.ShadowIsDrawn)
                        {
                            if (height < image.Height)
                            {
                                g.DrawImage(image, new Rectangle((x + this.ShadowOffset.X) + depth, y + this.ShadowOffset.Y, width + depth, (height - this.ShadowOffset.Y) - depth), new Rectangle(0, 0, image.Width, (height - this.ShadowOffset.Y) - depth), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                g.DrawImage(image, new Rectangle((x + this.ShadowOffset.X) + depth, y + this.ShadowOffset.Y, width + depth, (height - this.ShadowOffset.Y) - depth));
                            }
                        }
                        g.FillEllipse(brushArray[m], x, y - (depth / 2), width, depth);
                        g.DrawArc(pen, x, (y - (depth / 2)) + height, width, depth, 180, 0xe10);
                        GraphicsPath path = new GraphicsPath();
                        if (height > 0)
                        {
                            path.AddRectangle(new Rectangle(x, y, width, height));
                        }
                        else
                        {
                            path.AddRectangle(new Rectangle(x, y + height, width, -height));
                        }
                        path.AddPie((float) x, (y + height) - (((float) depth) / 2f), (float) width, (float) depth, 0f, 180f);
                        path.AddPie((float) x, y - (((float) depth) / 2f), (float) width, (float) depth, 0f, 180f);
                        g.Clip = new Region(path);
                        if (height > 0)
                        {
                            g.FillRectangle(brushArray[m], x, y, width, height + (depth / 2));
                        }
                        else
                        {
                            g.FillRectangle(brushArray[m], x, y + height, width, -height + (depth / 2));
                        }
                        g.Clip = new Region();
                        g.DrawArc(pen, x, (y - (depth / 2)) + height, width, depth, 0, 180);
                        g.DrawEllipse(pen, x, y - (depth / 2), width, depth);
                        this.xmlChart.AddCylinder(height, width, (float) x, (float) y, this.col[m % this.col.Count].ToString(), this.alpha, depth, 1, this.GetNavigateUrl(this.GetID(j) + "||" + j), this.GetToolTip(j));
                        if (this.ValueLabelsFormatString != string.Empty)
                        {
                            string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[j][this.x[m]]));
                            SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                            list.Add(new Charts.Label(text, x + (((width - size.Width) + depth) / 2f), ((y - size.Height) + 1f) - (depth / 2), size));
                        }
                        RectangleHotSpot spot = new RectangleHotSpot();
                        if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                        {
                            spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                        }
                        spot.PostBackValue = string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j });
                        spot.Left = x;
                        spot.Right = x + width;
                        spot.Top = y + this.graphstartsY;
                        spot.Bottom = (y + height) + this.graphstartsY;
                        spots.Add(spot);
                    }
                }
            }
            if (this.ScaleBreakMax > this.ScaleBreakMin)
            {
                ScaleBreaks.drawScaleBreak(this.xmlChart, g, this.BackColor2, this.ScaleBreakPos, this.ScaleBreakHeight, offsetX - 1, this.graphWidth, depth);
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawMultiKey()
        {
            List<KeyItem> keyLabels = this.KeyLabels;
            int count = keyLabels.Count;
            int length = 0;
            int num3 = 0;
            for (int i = 0; i < count; i++)
            {
                if (keyLabels[i].str.Length > length)
                {
                    length = keyLabels[i].str.Length;
                    num3 = i;
                }
            }
            Bitmap image = new Bitmap(10, 10);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = this.smoothingMode;
            Font keyFont = this.KeyFont;
            Pen pen = new Pen(Color.Black, 2f);
            SizeF ef = graphics.MeasureString(keyLabels[num3], keyFont);
            int height = Convert.ToInt32(ef.Height);
            image = new Bitmap((15 + height) + Convert.ToInt32(ef.Width), Convert.ToInt32((float) ((count * (ef.Height + 5f)) + 5f)));
            graphics = Graphics.FromImage(image);
            Brush brush = new SolidBrush(Color.Black);
            this.xmlChart.OffsetX = (this.graphWidth - image.Width) - 5;
            for (int j = 0; j < count; j++)
            {
                graphics.FillRectangle(new SolidBrush(this.col[j % this.col.Count]), 5f, 5f + (j * (ef.Height + 5f)), (float) height, (float) height);
                graphics.DrawRectangle(pen, 5f, 5f + (j * (ef.Height + 5f)), (float) height, (float) height);
                graphics.DrawString(keyLabels[j], keyFont, brush, (float) (10 + height), 5f + (j * (ef.Height + 5f)));
                this.xmlChart.AddBar(height, height, 5f, (float) ((int) (5f + (j * (ef.Height + 5f)))), new ColorItem(this.col[j % this.col.Count]).ToString(), 0xff, 0, 2, string.Empty, keyLabels[j].hint);
                this.xmlChart.AddLabel(keyLabels[j], (float) (10 + height), 5f + (j * (ef.Height + 5f)), 0, keyFont, false);
            }
            return image;
        }

        private Bitmap drawMultiLine()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            int num4 = this.x.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                for (int j = 0; j < num4; j++)
                {
                    if (row[this.x[j]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[j] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) < lowest)
                    {
                        lowest = Convert.ToDecimal(row[this.x[j]]);
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) > heighest)
                    {
                        heighest = Convert.ToDecimal(row[this.x[j]]);
                    }
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num7 = ((float) this.graphWidth) / (count + 1f);
            int num8 = Convert.ToInt32((double) (num7 / 2.0));
            int offsetYTop = 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num7));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num7, 0, this.graphHeight, offsetYTop, Convert.ToInt32((double) (num7 / 2.0)), true);
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
            Pen pen = new Pen(brush, (float) (this.LineGraphsLineSize + 1));
            Pen pen2 = new Pen(Color.Black, 1f);
            HotSpotCollection spots = new HotSpotCollection();
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(offsetX, 0, this.graphWidth, this.graphHeight + offsetYTop));
            g.Clip = new Region(path);
            offsetYTop -= this.negativeOffset;
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < num4; i++)
            {
                Brush brush2 = new SolidBrush(this.col[i % this.col.Count]);
                Pen pen3 = new Pen(brush2, (float) this.LineGraphsLineSize);
                int num11 = -2147483648;
                int num12 = -2147483648;
                for (int k = 0; k < count; k++)
                {
                    int num14 = 0;
                    try
                    {
                        num14 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[k][this.x[i]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    int num15 = Convert.ToInt32((double) (((k + 0.5) * num7) + num8)) + offsetX;
                    int num16 = (this.graphHeight - num14) + offsetYTop;
                    if (this.ShadowIsDrawn)
                    {
                        g.FillRectangle(brush, (int) ((num15 - (this.LineGraphsDataPointSize + 1)) + this.ShadowOffset.X), (int) ((num16 - (this.LineGraphsDataPointSize + 1)) - this.ShadowOffset.Y), (int) (((this.LineGraphsDataPointSize + 1) * 2) + 1), (int) (((this.LineGraphsDataPointSize + 1) * 2) + 1));
                    }
                    if (num11 != -2147483648)
                    {
                        if (this.ShadowIsDrawn)
                        {
                            g.DrawLine(pen, (int) (num11 + this.ShadowOffset.X), (int) (num12 - this.ShadowOffset.Y), (int) (num15 + this.ShadowOffset.X), (int) (num16 - this.ShadowOffset.Y));
                        }
                        g.DrawLine(pen3, num11, num12, num15, num16);
                        g.FillRectangle(brush2, (int) (num11 - this.LineGraphsDataPointSize), (int) (num12 - this.LineGraphsDataPointSize), (int) ((this.LineGraphsDataPointSize * 2) + 1), (int) ((this.LineGraphsDataPointSize * 2) + 1));
                        if (this.LineGraphsDataPointSize > 0)
                        {
                            g.DrawRectangle(pen2, (int) (num11 - this.LineGraphsDataPointSize), (int) (num12 - this.LineGraphsDataPointSize), (int) ((this.LineGraphsDataPointSize * 2) + 1), (int) ((this.LineGraphsDataPointSize * 2) + 1));
                        }
                        this.xmlChart.AddLine(num11, num12, num15, num16, Convert.ToInt32(pen3.Width), new ColorItem(pen3.Color).ToString(), this.alpha);
                        this.xmlChart.AddBar((this.LineGraphsDataPointSize * 2) + 1, (this.LineGraphsDataPointSize * 2) + 1, (float) (num11 - this.LineGraphsDataPointSize), (float) (num12 - this.LineGraphsDataPointSize), new ColorItem(pen3.Color).ToString(), this.alpha, 0, 1, this.GetNavigateUrl(string.Concat(new object[] { this.GetID(k), "|", this.x[i].str, "|", k })), this.GetToolTip(k));
                    }
                    if (k == (count - 1))
                    {
                        g.FillRectangle(brush2, (int) (num15 - this.LineGraphsDataPointSize), (int) (num16 - this.LineGraphsDataPointSize), (int) ((this.LineGraphsDataPointSize * 2) + 1), (int) ((this.LineGraphsDataPointSize * 2) + 1));
                        if (this.LineGraphsDataPointSize > 0)
                        {
                            g.DrawRectangle(pen2, (int) (num15 - this.LineGraphsDataPointSize), (int) (num16 - this.LineGraphsDataPointSize), (int) ((this.LineGraphsDataPointSize * 2) + 1), (int) ((this.LineGraphsDataPointSize * 2) + 1));
                        }
                        this.xmlChart.AddBar((this.LineGraphsDataPointSize * 2) + 1, (this.LineGraphsDataPointSize * 2) + 1, (float) (num15 - this.LineGraphsDataPointSize), (float) (num16 - this.LineGraphsDataPointSize), new ColorItem(pen3.Color).ToString(), this.alpha, 0, 1, this.GetNavigateUrl(string.Concat(new object[] { this.GetID(k), "|", this.x[i].str, "|", k })), this.GetToolTip(k));
                    }
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[k][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = string.Concat(new object[] { this.GetID(k), "|", this.x[i].str, "|", k });
                    spot.Left = num15 - 5;
                    spot.Right = num15 + 5;
                    spot.Top = (num16 + this.graphstartsY) - 5;
                    spot.Bottom = (num16 + this.graphstartsY) + 5;
                    spots.Add(spot);
                    num11 = num15;
                    num12 = num16;
                }
                if (this.ValueLabelsFormatString != string.Empty)
                {
                    for (int m = 0; m < count; m++)
                    {
                        int num18 = 0;
                        try
                        {
                            num18 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[m][this.x[i]]) / (heighest - lowest)) * this.graphHeight));
                        }
                        catch
                        {
                        }
                        int num19 = Convert.ToInt32((double) (((m + 0.5) * num7) + num8)) + offsetX;
                        int num20 = (this.graphHeight - num18) + offsetYTop;
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[m][this.x[i]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, num19 - (size.Width / 2f), ((num20 - size.Height) - 5f) + 1f, size));
                    }
                }
                Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, 10);
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return image;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return image;
        }

        private Bitmap drawMultiRadar(bool full)
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal num2 = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.x.Count;
            int n = this.data.Rows.Count;
            if (n == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                for (int j = 0; j < count; j++)
                {
                    if (row[this.x[j]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[j] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) < num2)
                    {
                        num2 = Convert.ToDecimal(row[this.x[j]]);
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) > heighest)
                    {
                        heighest = Convert.ToDecimal(row[this.x[j]]);
                    }
                }
            }
            if (num2 > 0M)
            {
                num2 = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int r = (Math.Min(this.graphHeight, this.graphWidth) - 80) / 2;
            int xc = this.graphWidth / 2;
            int yc = this.graphHeight / 2;
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
            Pen pen = new Pen(brush, 5f);
            Pen pen2 = new Pen(Color.Black, 1f);
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, this.graphWidth, this.graphHeight));
            g.Clip = new Region(path);
            this.drawRadarAxis(g, xc, yc, r, heighest, n);
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                int num10 = -2147483648;
                int num11 = -2147483648;
                Brush brush2 = new SolidBrush(Color.FromArgb(this.alpha, this.col[i % this.col.Count]));
                Pen pen3 = new Pen(Color.FromArgb(this.alpha, this.col[i % this.col.Count]), 5f);
                if (this.ShadowIsDrawn)
                {
                    for (int m = 0; m < (n + 1); m++)
                    {
                        int num13 = m % n;
                        int num14 = 0;
                        try
                        {
                            num14 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[num13][this.x[i]]) / (heighest - num2)) * r));
                        }
                        catch
                        {
                        }
                        double a = ((num13 * 2) * 3.1415926535897931) / ((double) n);
                        int num16 = (Convert.ToInt32((double) (Math.Sin(a) * num14)) + xc) + this.ShadowOffset.X;
                        int num17 = (Convert.ToInt32((double) (Math.Cos(a) * num14)) + yc) + this.ShadowOffset.Y;
                        if (num10 != -2147483648)
                        {
                            if (!full)
                            {
                                g.DrawLine(pen, num10, num11, num16, num17);
                            }
                            else
                            {
                                Point[] points = new Point[] { new Point(num10, num11), new Point(num16, num17), new Point(xc, yc) };
                                g.FillPolygon(brush, points);
                            }
                        }
                        num10 = num16;
                        num11 = num17;
                    }
                }
                num10 = -2147483648;
                num11 = -2147483648;
                for (int k = 0; k < (n + 1); k++)
                {
                    int num19 = k % n;
                    int num20 = 0;
                    try
                    {
                        num20 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[num19][this.x[i]]) / (heighest - num2)) * r));
                    }
                    catch
                    {
                    }
                    double num21 = ((num19 * 2) * 3.1415926535897931) / ((double) n);
                    int num22 = Convert.ToInt32((double) (Math.Sin(num21) * num20)) + xc;
                    int num23 = Convert.ToInt32((double) (Math.Cos(num21) * num20)) + yc;
                    if (num10 != -2147483648)
                    {
                        if (!full)
                        {
                            g.DrawLine(pen3, num10, num11, num22, num23);
                            this.xmlChart.AddLine(num10, num11, num22, num23, 5, this.col[i % this.col.Count].ToString(), this.alpha);
                        }
                        else
                        {
                            Point[] pointArray2 = new Point[] { new Point(num10, num11), new Point(num22, num23), new Point(xc, yc) };
                            g.FillPolygon(brush2, pointArray2);
                            this.xmlChart.AddTriangle(num10, num11, num22, num23, xc, yc, 0, this.col[i % this.col.Count].ToString(), this.alpha);
                        }
                        g.DrawLine(pen2, xc, yc, num22, num23);
                    }
                    if ((this.ValueLabelsFormatString != null) && (this.ValueLabelsFormatString != string.Empty))
                    {
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[num19][this.x[i]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        Charts.Label item = new Charts.Label(text, num22 - (size.Width / 2f), num23 - (size.Height / 2f), size);
                        list.Add(item);
                    }
                    num10 = num22;
                    num11 = num23;
                }
                Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, 0, 0);
            }
            return image;
        }

        private Bitmap drawMultiSurface()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            int num4 = this.x.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                for (int j = 0; j < num4; j++)
                {
                    if (row[this.x[j]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[j] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) < lowest)
                    {
                        lowest = Convert.ToDecimal(row[this.x[j]]);
                    }
                    if (Convert.ToDecimal(row[this.x[j]]) > heighest)
                    {
                        heighest = Convert.ToDecimal(row[this.x[j]]);
                    }
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num7 = ((float) this.graphWidth) / (count + 1f);
            int num8 = Convert.ToInt32((double) (num7 / 2.0));
            int offsetYTop = 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num7));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num7, 0, this.graphHeight, offsetYTop, Convert.ToInt32((double) (num7 / 2.0)), true);
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
            int y = offsetYTop + this.graphHeight;
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF((float) offsetX, 0f, (float) this.graphWidth, (float) (this.graphHeight + offsetYTop)));
            g.Clip = new Region(path);
            offsetYTop -= this.negativeOffset;
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < num4; i++)
            {
                Brush brush2 = new SolidBrush(Color.FromArgb(this.alpha, this.col[i % this.col.Count]));
                int x = -2147483648;
                int num13 = -2147483648;
                for (int k = 0; k < count; k++)
                {
                    int num15 = 0;
                    try
                    {
                        num15 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[k][this.x[i]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    int num16 = Convert.ToInt32((double) (((k + 0.5) * num7) + num8)) + offsetX;
                    int num17 = (this.graphHeight - num15) + offsetYTop;
                    if (x != -2147483648)
                    {
                        if (this.ShadowIsDrawn && this.ShadowIsDrawn)
                        {
                            int num18 = num13 - this.ShadowOffset.Y;
                            int num19 = num17 - this.ShadowOffset.Y;
                            Point[] pointArray = new Point[] { new Point(x + this.ShadowOffset.X, num18), new Point(num16 + this.ShadowOffset.X, num19), new Point(num16 + this.ShadowOffset.X, y), new Point(x + this.ShadowOffset.X, y) };
                            g.FillPolygon(brush, pointArray);
                        }
                        Point[] points = new Point[] { new Point(x, num13), new Point(num16, num17), new Point(num16, y), new Point(x, y) };
                        g.FillPolygon(brush2, points);
                        this.xmlChart.AddQuadrilater(x, num13, num16, num17, num16, y, x, y, 0, new ColorItem((brush2 as SolidBrush).Color).ToString(), this.alpha);
                    }
                    x = num16;
                    num13 = num17;
                }
                if (this.ValueLabelsFormatString != string.Empty)
                {
                    for (int m = 0; m < count; m++)
                    {
                        int num21 = 0;
                        try
                        {
                            num21 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[m][this.x[i]]) / (heighest - lowest)) * this.graphHeight));
                        }
                        catch
                        {
                        }
                        int num22 = Convert.ToInt32((double) (((m + 0.5) * num7) + num8)) + offsetX;
                        int num23 = (this.graphHeight - num21) + offsetYTop;
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[m][this.x[i]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, num22 - (size.Width / 2f), (num23 - size.Height) + 1f, size));
                    }
                    Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, 10);
                }
            }
            g.Clip = new Region();
            return image;
        }

        private Bitmap drawMultiSurface3D()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num4 = this.x.Count;
            foreach (DataRow row in this.data.Rows)
            {
                for (int k = 0; k < num4; k++)
                {
                    if (row[this.x[k]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[k] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    if (Convert.ToDecimal(row[this.x[k]]) < lowest)
                    {
                        lowest = Convert.ToDecimal(row[this.x[k]]);
                    }
                    if (Convert.ToDecimal(row[this.x[k]]) > heighest)
                    {
                        heighest = Convert.ToDecimal(row[this.x[k]]);
                    }
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            double num6 = ((double) this.graphWidth) / ((count + 1f) + this.Percent3D);
            int depth = Convert.ToInt32((double) (Convert.ToInt32(num6) * this.Percent3D));
            int offsetX = this.ComputeYAxisSpace(g, heighest) + depth;
            int offsetYTop = depth + 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num6));
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF((float) offsetX, (float) offsetYTop, (float) this.graphWidth, (float) this.graphHeight));
            g.Clip = new Region(path);
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.graphWidth -= offsetX;
            num6 = ((double) this.graphWidth) / ((count + 1) + this.Percent3D);
            int spacer = Convert.ToInt32(num6) / 2;
            depth = Convert.ToInt32((double) (num6 * this.Percent3D));
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num6, depth, this.graphHeight, offsetYTop, spacer, false);
            offsetYTop -= this.negativeOffset;
            depth /= num4;
            Brush[] brushArray = new SolidBrush[num4];
            Brush[] brushArray2 = new SolidBrush[num4];
            for (int i = 0; i < num4; i++)
            {
                int index = i % this.col.Count;
                Color color = this.col[index].color;
                brushArray[index] = new SolidBrush(Color.FromArgb(this.alpha, this.col[index]));
                Color baseColor = Color.FromArgb((color.R / 10) * 5, (color.G / 10) * 5, (color.B / 10) * 5);
                brushArray2[index] = new SolidBrush(Color.FromArgb(this.alpha, Color.FromArgb(this.alpha, baseColor)));
            }
            new Pen(this.ForeColor);
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
            List<Charts.Label> list = new List<Charts.Label>();
            for (int j = 0; j < num4; j++)
            {
                int x = -2147483648;
                int y = -2147483648;
                int num17 = (offsetYTop + this.graphHeight) - (((num4 - 1) - j) * depth);
                for (int m = 0; m < count; m++)
                {
                    int num19 = 0;
                    try
                    {
                        num19 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[m][this.x[j]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    int num20 = (Convert.ToInt32((double) (((m + 0.5) * num6) + spacer)) + offsetX) + (((num4 - 1) - j) * depth);
                    int num21 = ((this.graphHeight - num19) + offsetYTop) - (((num4 - 1) - j) * depth);
                    if (x != -2147483648)
                    {
                        if (this.ShadowIsDrawn)
                        {
                            int num22 = y - this.ShadowOffset.Y;
                            int num23 = num21 - this.ShadowOffset.Y;
                            Point[] pointArray = new Point[] { new Point((x + this.ShadowOffset.X) + depth, num22 - depth), new Point((num20 + this.ShadowOffset.X) + depth, num23 - depth), new Point((num20 + this.ShadowOffset.X) + depth, num17 - depth), new Point((x + this.ShadowOffset.X) + depth, num17 - depth) };
                            g.FillPolygon(brush, pointArray);
                        }
                        Point[] points = new Point[] { new Point(x + depth, y - depth), new Point(num20 + depth, num21 - depth), new Point(num20 + depth, num17 - depth), new Point(x + depth, num17 - depth) };
                        g.FillPolygon(brushArray[j], points);
                        Point[] pointArray3 = new Point[] { new Point(x + depth, num17 - depth), new Point(num20 + depth, num17 - depth), new Point(num20, num17), new Point(x, num17) };
                        g.FillPolygon(brushArray2[j], pointArray3);
                        if (m == 1)
                        {
                            Point[] pointArray4 = new Point[] { new Point(x + depth, y - depth), new Point(x, y), new Point(x, num17), new Point(x + depth, num17 - depth) };
                            g.FillPolygon(brushArray2[j], pointArray4);
                        }
                        Point[] pointArray5 = new Point[] { new Point(x + depth, y - depth), new Point(num20 + depth, num21 - depth), new Point(num20, num21), new Point(x, y) };
                        g.FillPolygon(brushArray2[j], pointArray5);
                        Point[] pointArray6 = new Point[] { new Point(num20 + depth, num21 - depth), new Point(num20, num21), new Point(num20, num17), new Point(num20 + depth, num17 - depth) };
                        g.FillPolygon(brushArray2[j], pointArray6);
                        Point[] pointArray7 = new Point[] { new Point(x, y), new Point(num20, num21), new Point(num20, num17), new Point(x, num17) };
                        g.FillPolygon(brushArray[j], pointArray7);
                        if (this.ValueLabelsFormatString != string.Empty)
                        {
                            string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[m][this.x[0]]));
                            SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                            list.Add(new Charts.Label(text, num20 - (size.Width / 2f), (float) num21, size));
                        }
                    }
                    x = num20;
                    y = num21;
                }
                Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            }
            g.Clip = new Region();
            return image;
        }

        private Bitmap drawPie()
        {
            double num = 0.0;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDouble(row[this.x[0]]) > 0.0)
                {
                    num += Convert.ToDouble(row[this.x[0]]);
                }
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.graphWidth, this.graphHeight);
            this.xmlChart.CurrentPhase = Phase.Background;
            this.xmlChart.AddBar(this.graphHeight, this.graphWidth, 0f, 0f, new ColorItem(this.BackColor).ToString(), 0xff, 0, 0, string.Empty, null);
            this.xmlChart.CurrentPhase = Phase.Elements;
            Pen pen = new Pen(Color.Black);
            int width = Math.Min(this.graphWidth, this.graphHeight) - 10;
            int x = (this.graphWidth - width) / 2;
            int y = (this.graphHeight - width) / 2;
            float startAngle = 0f;
            System.Drawing.Image image = this.LoadImage(string.Format("pieLight{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage(string.Format("pieShadow.png", new object[0]));
            if (this.ShadowIsDrawn)
            {
                g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y - this.ShadowOffset.Y, width + 10, width + 10), new Rectangle(0, 0, image2.Width, image2.Height), GraphicsUnit.Pixel);
            }
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                float sweepAngle = (float) ((Convert.ToDouble(this.data.Rows[i][this.x[0]]) / num) * 360.0);
                g.FillPie(new SolidBrush(this.col[i % this.col.Count]), (float) x, (float) y, (float) width, (float) width, startAngle, sweepAngle);
                g.DrawPie(pen, (float) x, (float) y, (float) width, (float) width, startAngle, sweepAngle);
                this.xmlChart.AddDonut((-startAngle - sweepAngle) + 90f, -startAngle + 90f, width / 2, 0, x + (width / 2), y + (width / 2), this.col[i % this.col.Count], this.alpha, this.GetNavigateUrl(this.GetID(i) + "||" + i), 1, false, this.GetToolTip(i));
                if ((this.ValueLabelsFormatString != null) && (this.ValueLabelsFormatString != string.Empty))
                {
                    string text = string.Format(this.ValueLabelsFormatString, sweepAngle / 360f);
                    SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                    int num9 = Convert.ToInt32((double) ((x + (width / 2)) + ((width / 3) * Math.Cos(((startAngle + (sweepAngle / 2f)) * 3.1415926535897931) / 180.0))));
                    int num10 = Convert.ToInt32((double) ((y + (width / 2)) + ((width / 3) * Math.Sin(((startAngle + (sweepAngle / 2f)) * 3.1415926535897931) / 180.0))));
                    list.Add(new Charts.Label(text, num9 - (size.Width / 2f), (num10 - (size.Height / 2f)) + 1f, size));
                }
                startAngle += sweepAngle;
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, 0, 0);
            g.DrawImage(image, new Rectangle(x, y, width, width), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            return bitmap;
        }

        private Bitmap drawPieKey()
        {
            int count = this.data.Rows.Count;
            int length = 0;
            int num3 = 0;
            for (int i = 0; i < count; i++)
            {
                if (string.Format(this.LabelsFormatString, this.data.Rows[i][this.XAxisLabels]).Length > length)
                {
                    length = this.data.Rows[i][this.XAxisLabels].ToString().Length;
                    num3 = i;
                }
            }
            Bitmap image = new Bitmap(10, 10);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = this.smoothingMode;
            Font keyFont = this.KeyFont;
            Pen pen = new Pen(Color.Black, 2f);
            string text = string.Format(this.LabelsFormatString, this.data.Rows[num3][this.XAxisLabels]);
            SizeF ef = graphics.MeasureString(text, keyFont);
            int height = Convert.ToInt32(ef.Height);
            int num6 = Convert.ToInt32((int) ((this.graphHeight - 20) / (height + 5)));
            int num7 = Convert.ToInt32(Math.Ceiling((double) (((float) count) / (num6 + 0f))));
            int num8 = Convert.ToInt32((float) ((15 + height) + ef.Width));
            image = new Bitmap(num8 * num7, Convert.ToInt32((float) ((num6 * (ef.Height + 5f)) + 5f)));
            graphics = Graphics.FromImage(image);
            this.xmlChart.OffsetX = (this.graphWidth - image.Width) - 5;
            Brush brush = new SolidBrush(Color.Black);
            int num9 = 0;
            for (int j = 0; j < count; j++)
            {
                if (((j % num6) == 0) && (j > 0))
                {
                    num9++;
                }
                graphics.FillRectangle(new SolidBrush(this.col[j % this.col.Count]), (float) (5 + (num9 * num8)), 5f + ((j % num6) * (ef.Height + 5f)), (float) height, (float) height);
                this.xmlChart.AddBar(height, height, (float) (5 + (num9 * num8)), (float) ((int) (5f + ((j % num6) * (ef.Height + 5f)))), this.col[j % this.col.Count].ToString(), this.col[j % this.col.Count].color.A, 0, 1, string.Empty, "");
                graphics.DrawRectangle(pen, (float) (5 + (num9 * num8)), 5f + ((j % num6) * (ef.Height + 5f)), (float) height, (float) height);
                text = string.Format(this.LabelsFormatString, this.data.Rows[j][this.XAxisLabels]);
                graphics.DrawString(text, keyFont, brush, (float) ((10 + height) + (num9 * num8)), 5f + ((j % num6) * (ef.Height + 5f)));
                this.xmlChart.AddLabel(text, (float) ((10 + height) + (num9 * num8)), 5f + ((j % num6) * (ef.Height + 5f)), 0, keyFont, false);
            }
            return image;
        }

        private Bitmap drawRadar(bool full)
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal num2 = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDecimal(row[this.x[0]]) < num2)
                {
                    num2 = Convert.ToDecimal(row[this.x[0]]);
                }
                if (Convert.ToDecimal(row[this.x[0]]) > heighest)
                {
                    heighest = Convert.ToDecimal(row[this.x[0]]);
                }
            }
            if (num2 > 0M)
            {
                num2 = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int r = (Math.Min(this.graphHeight, this.graphWidth) - 80) / 2;
            int xc = this.graphWidth / 2;
            int yc = this.graphHeight / 2;
            Brush brush = new SolidBrush(Color.FromArgb(this.alpha, this.col[0]));
            Pen pen = new Pen(this.col[0], 4f);
            Brush brush2 = new SolidBrush(Color.FromArgb(50, Color.Black));
            Pen pen2 = new Pen(brush2, 5f);
            Pen pen3 = new Pen(Color.Black, 1f);
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, this.graphWidth, this.graphHeight));
            g.Clip = new Region(path);
            this.drawRadarAxis(g, xc, yc, r, heighest, count);
            int num7 = -2147483648;
            int num8 = -2147483648;
            if (this.ShadowIsDrawn)
            {
                for (int j = 0; j < (count + 1); j++)
                {
                    int num10 = j % count;
                    int num11 = 0;
                    try
                    {
                        num11 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[num10][this.x[0]]) / (heighest - num2)) * r));
                    }
                    catch
                    {
                    }
                    double a = ((num10 * 2) * 3.1415926535897931) / ((double) count);
                    int num13 = (Convert.ToInt32((double) (Math.Sin(a) * num11)) + xc) + this.ShadowOffset.X;
                    int num14 = (Convert.ToInt32((double) (Math.Cos(a) * num11)) + yc) + this.ShadowOffset.Y;
                    if (num7 != -2147483648)
                    {
                        if (!full)
                        {
                            g.DrawLine(pen2, num7, num8, num13, num14);
                        }
                        else
                        {
                            Point[] points = new Point[] { new Point(num7, num8), new Point(num13, num14), new Point(xc, yc) };
                            g.FillPolygon(brush2, points);
                        }
                    }
                    num7 = num13;
                    num8 = num14;
                }
            }
            List<Charts.Label> list = new List<Charts.Label>();
            num7 = -2147483648;
            num8 = -2147483648;
            for (int i = 0; i < (count + 1); i++)
            {
                int num16 = i % count;
                int num17 = 0;
                try
                {
                    num17 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[num16][this.x[0]]) / (heighest - num2)) * r));
                }
                catch
                {
                }
                double num18 = ((num16 * 2) * 3.1415926535897931) / ((double) count);
                int num19 = Convert.ToInt32((double) (Math.Sin(num18) * num17)) + xc;
                int num20 = Convert.ToInt32((double) (Math.Cos(num18) * num17)) + yc;
                if (num7 != -2147483648)
                {
                    if (!full)
                    {
                        g.DrawLine(pen, num7, num8, num19, num20);
                        this.xmlChart.AddLine(num7, num8, num19, num20, 4, this.col[0].ToString(), this.col[0].color.A);
                    }
                    else
                    {
                        Point[] pointArray2 = new Point[] { new Point(num7, num8), new Point(num19, num20), new Point(xc, yc) };
                        g.FillPolygon(brush, pointArray2);
                        this.xmlChart.AddTriangle(num7, num8, num19, num20, xc, yc, 0, this.col[0].ToString(), this.alpha);
                    }
                    g.DrawLine(pen3, xc, yc, num19, num20);
                }
                if ((this.ValueLabelsFormatString != null) && (this.ValueLabelsFormatString != string.Empty))
                {
                    string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[num16][this.x[0]]));
                    SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                    Charts.Label item = new Charts.Label(text, num19 - (size.Width / 2f), num20 - (size.Height / 2f), size);
                    list.Add(item);
                }
                num7 = num19;
                num8 = num20;
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, 0, 0);
            return image;
        }

        private void drawRadarAxis(Graphics g, int xc, int yc, int r, decimal heighest, int n)
        {
            int num13;
            this.xmlChart.CurrentPhase = Phase.Background;
            Font labelsFont = this.LabelsFont;
            Brush brush = new SolidBrush(Color.Black);
            new SolidBrush(Color.FromArgb(200, Color.White));
            Brush brush2 = new SolidBrush(Color.FromArgb(this.BackgroundDarkening, Color.Black));
            Brush brush3 = new SolidBrush(this.BackColor);
            Pen pen = new Pen(brush);
            Pen pen2 = new Pen(brush3);
            decimal interval = heighest;
            int num2 = this.determineYInterval(interval, r, 15);
            decimal num3 = num2;
            //?int num4 = Convert.ToInt32((decimal)(((decimal.op_Increment(Math.Floor((decimal)(heighest / num2))) * num2) / interval) * r));

            int num4 = Convert.ToInt32((decimal) (((Math.Floor((decimal) (heighest / num2))) * num2) / interval) * r);
            for (int i = 0; i < n; i++)
            {
                double a = ((i * 3.1415926535897931) * 2.0) / ((double) n);
                double num7 = (((i + 1) * 3.1415926535897931) * 2.0) / ((double) n);
                Point[] points = new Point[] { new Point(Convert.ToInt32((double) (xc + (Math.Sin(a) * 0.0))), Convert.ToInt32((double) (yc + (Math.Cos(a) * 0.0)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(num7) * 0.0))), Convert.ToInt32((double) (yc + (Math.Cos(num7) * 0.0)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(num7) * num4))), Convert.ToInt32((double) (yc + (Math.Cos(num7) * num4)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(a) * num4))), Convert.ToInt32((double) (yc + (Math.Cos(a) * num4)))) };
                g.FillPolygon(brush3, points);
                this.xmlChart.AddQuadrilater(points[0].X, points[0].Y, points[1].X, points[1].Y, points[2].X, points[2].Y, points[3].X, points[3].Y, 1, new ColorItem(this.BackColor).ToString(), this.BackColor.A);
            }
            foreach (BackgroundStripe stripe in this.BackgroundStripes)
            {
                double num8 = Convert.ToDouble((decimal) ((stripe.MinValue / heighest) * r));
                double num9 = Convert.ToDouble((decimal) ((stripe.MaxValue / heighest) * r));
                if (num9 > r)
                {
                    num9 = num8 = r;
                }
                for (int j = 0; j < n; j++)
                {
                    double num11 = ((j * 3.1415926535897931) * 2.0) / ((double) n);
                    double num12 = (((j + 1) * 3.1415926535897931) * 2.0) / ((double) n);
                    if (num8 != num9)
                    {
                        Point[] pointArray2 = new Point[] { new Point(Convert.ToInt32((double) (xc + (Math.Sin(num11) * num8))), Convert.ToInt32((double) (yc + (Math.Cos(num11) * num8)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(num12) * num8))), Convert.ToInt32((double) (yc + (Math.Cos(num12) * num8)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(num12) * num9))), Convert.ToInt32((double) (yc + (Math.Cos(num12) * num9)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(num11) * num9))), Convert.ToInt32((double) (yc + (Math.Cos(num11) * num9)))) };
                        g.FillPolygon(new SolidBrush(stripe.BrushColor), pointArray2);
                        this.xmlChart.AddQuadrilater(pointArray2[0].X, pointArray2[0].Y, pointArray2[1].X, pointArray2[1].Y, pointArray2[2].X, pointArray2[2].Y, pointArray2[3].X, pointArray2[3].Y, 1, new ColorItem(stripe.BrushColor).ToString(), this.BackColor.A);
                    }
                    else
                    {
                        Pen pen3 = new Pen(stripe.BrushColor, 3f);
                        g.DrawLine(pen3, new Point(Convert.ToInt32((double) (xc + (Math.Sin(num11) * num8))), Convert.ToInt32((double) (yc + (Math.Cos(num11) * num8)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(num12) * num8))), Convert.ToInt32((double) (yc + (Math.Cos(num12) * num8)))));
                        this.xmlChart.AddLine(Convert.ToInt32((double) (xc + (Math.Sin(num11) * num8))), Convert.ToInt32((double) (yc + (Math.Cos(num11) * num8))), Convert.ToInt32((double) (xc + (Math.Sin(num12) * num8))), Convert.ToInt32((double) (yc + (Math.Cos(num12) * num8))), 3, new ColorItem(stripe.BrushColor).ToString(), stripe.BrushColor.A);
                    }
                }
            }
            for (num13 = 0; num3 < (heighest + num2); num13++)
            {
                int num14 = Convert.ToInt32((decimal) ((num3 / interval) * r));
                int num15 = Convert.ToInt32((decimal) (((num3 - num2) / interval) * r));
                for (int k = 0; k < n; k++)
                {
                    double num17 = ((k * 3.1415926535897931) * 2.0) / ((double) n);
                    double num18 = (((k + 1) * 3.1415926535897931) * 2.0) / ((double) n);
                    Point[] pointArray3 = new Point[] { new Point(Convert.ToInt32((double) (xc + (Math.Sin(num17) * num14))), Convert.ToInt32((double) (yc + (Math.Cos(num17) * num14)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(num18) * num14))), Convert.ToInt32((double) (yc + (Math.Cos(num18) * num14)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(num18) * num15))), Convert.ToInt32((double) (yc + (Math.Cos(num18) * num15)))), new Point(Convert.ToInt32((double) (xc + (Math.Sin(num17) * num15))), Convert.ToInt32((double) (yc + (Math.Cos(num17) * num15)))) };
                    if (this.DrawGrid)
                    {
                        g.DrawPolygon(pen, pointArray3);
                        if ((num13 % 2) == 1)
                        {
                            g.FillPolygon(brush2, pointArray3);
                            this.xmlChart.AddQuadrilater(pointArray3[0].X, pointArray3[0].Y, pointArray3[1].X, pointArray3[1].Y, pointArray3[2].X, pointArray3[2].Y, pointArray3[3].X, pointArray3[3].Y, 1, "0x0", this.BackgroundDarkening);
                        }
                    }
                    else
                    {
                        g.DrawPolygon(pen2, pointArray3);
                    }
                }
                num3 += num2;
            }
            num3 = num2;
            num13 = 0;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            while (num3 < (heighest + num2))
            {
                string s = Convert.ToInt32(num3).ToString();
                int num19 = Convert.ToInt32((decimal) ((num3 / interval) * r));
                if (num3 <= heighest)
                {
                    g.TranslateTransform((float) (xc + num19), (float) yc);
                    g.RotateTransform(90f);
                    g.DrawString(s, labelsFont, brush, (float) 0f, (float) 0f);
                    g.ResetTransform();
                    this.xmlChart.AddLabel(s, (float) (xc + num19), (float) yc, 90, labelsFont, false);
                }
                num3 += num2;
                num13++;
            }
            if ((this.XAxisLabels != null) && (this.XAxisLabels != string.Empty))
            {
                for (int m = 0; m < n; m++)
                {
                    string text = string.Format(this.LabelsFormatString, this.data.Rows[m][this.XAxisLabels]);
                    SizeF ef = g.MeasureString(text, labelsFont);
                    float num21 = r - ef.Width;
                    if (ef.Width <= (r / 2))
                    {
                        float num22 = (m * 360f) / ((float) n);
                        int num23 = Convert.ToInt32((double) (num21 * Math.Sin((num22 / 180f) * 3.1415926535897931)));
                        int num24 = Convert.ToInt32((double) (num21 * Math.Cos((num22 / 180f) * 3.1415926535897931)));
                        g.TranslateTransform((float) (xc + num23), (float) (yc + num24));
                        g.RotateTransform(-num22 + 90f);
                        g.DrawString(text, labelsFont, brush, (float) 0f, (float) -12f);
                        g.ResetTransform();
                        this.xmlChart.AddLabel(text, (float) (xc + num23), (float) (yc + num24), Convert.ToInt32((float) (-num22 + 90f)), labelsFont, false);
                    }
                }
            }
            this.xmlChart.CurrentPhase = Phase.Elements;
        }

        private Bitmap drawStackedBars()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num4 = this.x.Count;
            foreach (DataRow row in this.data.Rows)
            {
                decimal num5 = 0M;
                for (int k = 0; k < num4; k++)
                {
                    if (row[this.x[k]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[k] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    num5 += Convert.ToDecimal(row[this.x[k]]);
                }
                if (num5 < lowest)
                {
                    lowest = num5;
                }
                if (num5 > heighest)
                {
                    heighest = num5;
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num8 = ((float) this.graphWidth) / (count + 1f);
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num8));
            int offsetYTop = 10;
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            int width = Convert.ToInt32((double) (num8 * this.PercentWidth));
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            int spacer = Convert.ToInt32((double) (num8 - width)) / 2;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num8, 0, this.graphHeight, offsetYTop, spacer, false);
            Brush[] brushArray = new Brush[num4];
            for (int i = 0; i < num4; i++)
            {
                brushArray[i] = new SolidBrush(this.col[i % this.col.Count]);
            }
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage("barShadow.png");
            for (int j = 0; j < count; j++)
            {
                int height = 0;
                int y = 0;
                int x = 0;
                for (int m = 0; m < num4; m++)
                {
                    int num18 = 0;
                    try
                    {
                        num18 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[m]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    y = ((this.graphHeight - num18) - height) + offsetYTop;
                    height += num18;
                }
                x = Convert.ToInt32((double) ((j + 0.5) * num8)) + offsetX;
                if (this.ShadowIsDrawn)
                {
                    if (height < image2.Height)
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, height - this.ShadowOffset.Y), new Rectangle(0, 0, image2.Width, height - this.ShadowOffset.Y), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, height - this.ShadowOffset.Y));
                    }
                }
                height = 0;
                y = 0;
                for (int n = 0; n < num4; n++)
                {
                    int num20 = 0;
                    try
                    {
                        num20 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[n]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    y = ((this.graphHeight - num20) - height) + offsetYTop;
                    g.FillRectangle(brushArray[n], x, y, width, num20);
                    this.xmlChart.AddBar(num20, width, (float) x, (float) y, new ColorItem((brushArray[n] as SolidBrush).Color).ToString(), (brushArray[n] as SolidBrush).Color.A, 0, 0, this.GetNavigateUrl(string.Concat(new object[] { this.GetID(j), "|", this.x[n].str, "|", j })), this.GetToolTip(j));
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[j][this.x[n]]));
                        SizeF ef = g.MeasureString(text, this.ValueLabelsFont);
                        if ((ef.Height < num20) && (ef.Width < num8))
                        {
                            g.DrawString(text, this.ValueLabelsFont, Brushes.Black, x + ((width - ef.Width) / 2f), (float) (y + 1));
                        }
                    }
                    height += num20;
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = string.Concat(new object[] { this.GetID(j), "|", this.x[n].str, "|", j });
                    spot.Left = x;
                    spot.Right = x + width;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = (y + num20) + this.graphstartsY;
                    spots.Add(spot);
                }
                g.DrawImage(image, new Rectangle(x, y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawStackedBars3D()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num4 = this.x.Count;
            foreach (DataRow row in this.data.Rows)
            {
                decimal num5 = 0M;
                for (int k = 0; k < num4; k++)
                {
                    if (row[this.x[k]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[k] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    num5 += Convert.ToDecimal(row[this.x[k]]);
                }
                if (num5 < lowest)
                {
                    lowest = num5;
                }
                if (num5 > heighest)
                {
                    heighest = num5;
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            double num7 = ((float) this.graphWidth) / (count + 1f);
            int depth = Convert.ToInt32((double) (Convert.ToInt32((double) (num7 * this.PercentWidth)) * this.Percent3D));
            int offsetX = this.ComputeYAxisSpace(g, heighest) + depth;
            int offsetYTop = depth + 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num7));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.graphWidth -= offsetX;
            num7 = ((float) this.graphWidth) / (count + 1f);
            int width = Convert.ToInt32((double) (num7 * this.PercentWidth));
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            depth = Convert.ToInt32((double) (width * this.Percent3D));
            width -= depth;
            int spacer = Convert.ToInt32((double) (num7 - width)) / 2;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num7, depth, this.graphHeight, offsetYTop, spacer, false);
            Brush[] brushArray = new Brush[num4];
            Brush[] brushArray2 = new Brush[num4];
            Brush[] brushArray3 = new Brush[num4];
            for (int i = 0; i < num4; i++)
            {
                Color baseColor = this.col[i % this.col.Count];
                brushArray[i] = new SolidBrush(Color.FromArgb(this.alpha, baseColor));
                brushArray2[i] = new SolidBrush(Color.FromArgb((this.alpha * 3) / 4, baseColor));
                brushArray3[i] = new SolidBrush(Color.FromArgb(this.alpha / 2, baseColor));
            }
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage(string.Format("barShadow.png", new object[0]));
            List<Charts.Label> list = new List<Charts.Label>();
            for (int j = 0; j < count; j++)
            {
                int y = 0;
                int num16 = 0;
                for (int m = 0; m < num4; m++)
                {
                    int num18 = 0;
                    try
                    {
                        num18 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[m]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    y = ((this.graphHeight - num18) - num16) + offsetYTop;
                    num16 += num18;
                }
                int x = Convert.ToInt32((double) ((j + 0.5) * num7)) + offsetX;
                if (this.ShadowIsDrawn)
                {
                    if (num16 < image2.Height)
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num7), (num16 - this.ShadowOffset.Y) - depth), new Rectangle(0, 0, image2.Width, (num16 - this.ShadowOffset.Y) - depth), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num7), (num16 - this.ShadowOffset.Y) - depth));
                    }
                }
                decimal num20 = 0M;
                num16 = 0;
                y = 0;
                for (int n = 0; n < num4; n++)
                {
                    int height = 0;
                    try
                    {
                        height = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[n]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    y = ((this.graphHeight - height) - num16) + offsetYTop;
                    g.FillRectangle(brushArray[n], x, y, width, height);
                    g.DrawImage(image, new Rectangle(x, y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                    Point[] points = new Point[] { new Point(x + width, y), new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point((x + width) + depth, y - depth) };
                    Point[] destPoints = new Point[] { points[0], points[3], points[1] };
                    g.FillPolygon(brushArray2[n], points);
                    g.DrawImage(image, destPoints, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                    Point[] pointArray3 = new Point[] { new Point(x + width, y), new Point((x + width) + depth, y - depth), new Point(x + depth, y - depth), new Point(x, y) };
                    Point[] pointArray4 = new Point[] { pointArray3[0], pointArray3[3], pointArray3[1] };
                    g.FillPolygon(brushArray3[n], pointArray3);
                    g.DrawImage(image, pointArray4, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                    num16 += height;
                    this.xmlChart.AddBar(height, width, (float) x, (float) y, new ColorItem((brushArray[n] as SolidBrush).Color).ToString(), (brushArray[n] as SolidBrush).Color.A, depth, 0, this.GetNavigateUrl(string.Concat(new object[] { this.GetID(j), "|", this.x[n].str, "|", j })), this.GetToolTip(j));
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        num20 += Convert.ToDecimal(this.data.Rows[j][this.x[n]]);
                        string text = string.Format(this.ValueLabelsFormatString, num20);
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, x + ((width - size.Width) / 2f), (float) (y + 1), size));
                    }
                    Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = string.Concat(new object[] { this.GetID(j), "|", this.x[n].str, "|", j });
                    spot.Left = x;
                    spot.Right = (x + width) + depth;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = (y + height) + this.graphstartsY;
                    spots.Add(spot);
                }
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawStackedBars3DFull()
        {
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num2 = this.x.Count;
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            double num3 = ((float) this.graphWidth) / (count + 1f);
            int depth = Convert.ToInt32((double) (Convert.ToInt32((double) (num3 * this.PercentWidth)) * this.Percent3D));
            int offsetX = this.ComputeYAxisSpace(g, 100M) + depth;
            int offsetYTop = depth + 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num3));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.graphWidth -= offsetX;
            num3 = ((float) this.graphWidth) / (count + 1f);
            int width = Convert.ToInt32((double) (num3 * this.PercentWidth));
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            depth = Convert.ToInt32((double) (width * this.Percent3D));
            width -= depth;
            int spacer = Convert.ToInt32((double) (num3 - width)) / 2;
            this.drawYAxis(g, 0M, 100M, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num3, depth, this.graphHeight, offsetYTop, spacer, false);
            Brush[] brushArray = new Brush[num2];
            Brush[] brushArray2 = new Brush[num2];
            Brush[] brushArray3 = new Brush[num2];
            for (int i = 0; i < num2; i++)
            {
                Color baseColor = this.col[i % this.col.Count];
                brushArray[i] = new SolidBrush(Color.FromArgb(this.alpha, baseColor));
                brushArray2[i] = new SolidBrush(Color.FromArgb((this.alpha * 3) / 4, baseColor));
                brushArray3[i] = new SolidBrush(Color.FromArgb(this.alpha / 2, baseColor));
            }
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage(string.Format("barShadow.png", new object[0]));
            for (int j = 0; j < count; j++)
            {
                decimal num11 = 0M;
                for (int k = 0; k < num2; k++)
                {
                    num11 += Convert.ToDecimal(this.data.Rows[j][this.x[k]]);
                }
                int num13 = 0;
                int x = Convert.ToInt32((double) ((j + 0.5) * num3)) + offsetX;
                int graphHeight = this.graphHeight;
                int y = depth + offsetYTop;
                if (this.ShadowIsDrawn)
                {
                    if (this.graphHeight < image2.Height)
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num3), (graphHeight - this.ShadowOffset.Y) - depth), new Rectangle(0, 0, image2.Width, (graphHeight - this.ShadowOffset.Y) - depth), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num3), (graphHeight - this.ShadowOffset.Y) - depth));
                    }
                }
                decimal num17 = 0M;
                for (int m = 0; m < num2; m++)
                {
                    graphHeight = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[m]]) / num11) * this.graphHeight));
                    y = ((this.graphHeight - graphHeight) - num13) + offsetYTop;
                    g.FillRectangle(brushArray[m], x, y, width, graphHeight);
                    g.DrawImage(image, new Rectangle(x, y, width, graphHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                    Point[] points = new Point[] { new Point(x + width, y), new Point(x + width, y + graphHeight), new Point((x + width) + depth, (y + graphHeight) - depth), new Point((x + width) + depth, y - depth) };
                    Point[] destPoints = new Point[] { points[0], points[3], points[1] };
                    g.FillPolygon(brushArray2[m], points);
                    g.DrawImage(image, destPoints, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                    Point[] pointArray3 = new Point[] { new Point(x + width, y), new Point((x + width) + depth, y - depth), new Point(x + depth, y - depth), new Point(x, y) };
                    Point[] pointArray4 = new Point[] { pointArray3[0], pointArray3[3], pointArray3[1] };
                    g.FillPolygon(brushArray3[m], pointArray3);
                    g.DrawImage(image, pointArray4, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                    num13 += graphHeight;
                    this.xmlChart.AddBar(graphHeight, width, (float) x, (float) y, new ColorItem((brushArray[m] as SolidBrush).Color).ToString(), (brushArray[m] as SolidBrush).Color.A, depth, 0, this.GetNavigateUrl(string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j })), this.GetToolTip(j));
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        num17 = Convert.ToDecimal(this.data.Rows[j][this.x[m]]) / num11;
                        string text = string.Format(this.ValueLabelsFormatString, num17);
                        SizeF ef = g.MeasureString(text, this.ValueLabelsFont);
                        if ((ef.Width < num3) && (ef.Height < graphHeight))
                        {
                            g.DrawString(text, this.ValueLabelsFont, Brushes.Black, x + (((width - ef.Width) + depth) / 2f), (float) (y + 1));
                        }
                    }
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j });
                    spot.Left = x;
                    spot.Right = (x + width) + depth;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = (y + graphHeight) + this.graphstartsY;
                    spots.Add(spot);
                }
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawStackedBarsFull()
        {
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num2 = this.x.Count;
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, 100M);
            this.graphWidth -= offsetX;
            double num4 = ((float) this.graphWidth) / (count + 1f);
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num4));
            int offsetYTop = 10;
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            int width = Convert.ToInt32((double) (num4 * this.PercentWidth));
            int spacer = Convert.ToInt32((double) (num4 - width)) / 2;
            this.drawYAxis(g, 0M, 100M, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num4, 0, this.graphHeight, offsetYTop, spacer, false);
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            Brush[] brushArray = new Brush[num2];
            for (int i = 0; i < num2; i++)
            {
                brushArray[i] = new SolidBrush(this.col[i % this.col.Count]);
            }
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage("barShadow.png");
            for (int j = 0; j < count; j++)
            {
                int graphHeight = this.graphHeight;
                int x = Convert.ToInt32((double) ((j + 0.5) * num4)) + offsetX;
                int y = offsetYTop;
                if (this.ShadowIsDrawn)
                {
                    if (graphHeight < image2.Height)
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, graphHeight - this.ShadowOffset.Y), new Rectangle(0, 0, image2.Width, graphHeight - this.ShadowOffset.Y), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, graphHeight - this.ShadowOffset.Y));
                    }
                }
                decimal num13 = 0M;
                for (int k = 0; k < num2; k++)
                {
                    num13 += Convert.ToDecimal(this.data.Rows[j][this.x[k]]);
                }
                int height = 0;
                decimal num16 = 0M;
                for (int m = 0; m < num2; m++)
                {
                    graphHeight = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[m]]) / num13) * this.graphHeight));
                    y = ((this.graphHeight - graphHeight) - height) + offsetYTop;
                    g.FillRectangle(brushArray[m], x, y, width, graphHeight);
                    this.xmlChart.AddBar(graphHeight, width, (float) x, (float) y, new ColorItem((brushArray[m] as SolidBrush).Color).ToString(), (brushArray[m] as SolidBrush).Color.A, 0, 0, this.GetNavigateUrl(string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j })), this.GetToolTip(j));
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        num16 = Convert.ToDecimal(this.data.Rows[j][this.x[m]]) / num13;
                        string text = string.Format(this.ValueLabelsFormatString, num16);
                        SizeF ef = g.MeasureString(text, this.ValueLabelsFont);
                        if ((ef.Height < graphHeight) && (ef.Width < num4))
                        {
                            g.DrawString(text, this.ValueLabelsFont, Brushes.Black, x + ((width - ef.Width) / 2f), (float) (y + 1));
                        }
                    }
                    height += graphHeight;
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = string.Concat(new object[] { this.GetID(j), "|", this.x[m].str, "|", j });
                    spot.Left = x;
                    spot.Right = x + width;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = (y + graphHeight) + this.graphstartsY;
                    spots.Add(spot);
                }
                g.DrawImage(image, new Rectangle(x, y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawStackedCylinders()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num4 = this.x.Count;
            foreach (DataRow row in this.data.Rows)
            {
                decimal num5 = 0M;
                for (int k = 0; k < num4; k++)
                {
                    if (row[this.x[k]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[k] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    num5 += Convert.ToDecimal(row[this.x[k]]);
                }
                if (num5 < lowest)
                {
                    lowest = num5;
                }
                if (num5 > heighest)
                {
                    heighest = num5;
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num8 = ((float) this.graphWidth) / (count + 1f);
            int width = Convert.ToInt32((double) (num8 * this.PercentWidth));
            int depth = Convert.ToInt32((double) (width * this.Percent3D));
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            int spacer = Convert.ToInt32((double) (num8 - width)) / 2;
            int offsetYTop = 10 + width;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num8));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num8, depth, this.graphHeight, offsetYTop, spacer, false);
            Pen pen = new Pen(Color.Black);
            Brush[] brushArray = new Brush[num4];
            for (int i = 0; i < num4; i++)
            {
                Color baseColor = this.col[i % this.col.Count];
                brushArray[i] = new SolidBrush(Color.FromArgb(this.alpha, baseColor));
            }
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage("barShadow.png");
            List<Charts.Label> list = new List<Charts.Label>();
            for (int j = 0; j < count; j++)
            {
                int x = Convert.ToInt32((double) (((j + 0.5) * num8) + (0.5 * depth))) + offsetX;
                int y = 0;
                int num17 = 0;
                for (int m = 0; m < num4; m++)
                {
                    int num19 = 0;
                    try
                    {
                        num19 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[m]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    y = ((this.graphHeight - num19) - num17) + offsetYTop;
                    num17 += num19;
                }
                if (this.ShadowIsDrawn)
                {
                    if (num17 < image.Height)
                    {
                        g.DrawImage(image, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num8), (num17 - this.ShadowOffset.Y) - (depth / 2)), new Rectangle(0, 0, image.Width, (num17 - this.ShadowOffset.Y) - (depth / 2)), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(image, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num8), (num17 - this.ShadowOffset.Y) - (depth / 2)));
                    }
                }
                num17 = 0;
                y = 0;
                for (int n = 0; n < num4; n++)
                {
                    int height = 0;
                    try
                    {
                        height = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[n]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    y = (((this.graphHeight - height) - num17) + offsetYTop) - (depth / 2);
                    g.FillEllipse(brushArray[n], x, y - (depth / 2), width, depth);
                    g.DrawArc(pen, x, (y - (depth / 2)) + height, width, depth, 180, 0xe10);
                    GraphicsPath path = new GraphicsPath();
                    path.AddRectangle(new Rectangle(x, y, width, height));
                    path.AddPie((float) x, (y + height) - (((float) depth) / 2f), (float) width, (float) depth, 0f, 180f);
                    path.AddPie((float) x, y - (((float) depth) / 2f), (float) width, (float) depth, 0f, 180f);
                    g.Clip = new Region(path);
                    offsetYTop -= this.negativeOffset;
                    g.FillRectangle(brushArray[n], x, y, width, height + (depth / 2));
                    g.Clip = new Region();
                    g.DrawArc(pen, x, (y - (depth / 2)) + height, width, depth, 0, 180);
                    g.DrawEllipse(pen, x, y - (depth / 2), width, depth);
                    this.xmlChart.AddCylinder(height, width, (float) x, (float) y, this.col[n % this.col.Count].ToString(), this.alpha, depth, 1, this.GetNavigateUrl(this.GetID(j) + "||" + j), this.GetToolTip(j));
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[j][this.x[n]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, x + ((width - size.Width) / 2f), ((y - size.Height) - depth) + 1f, size));
                    }
                    num17 += height;
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[j][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = string.Concat(new object[] { this.GetID(j), "|", this.x[n].str, "|", j });
                    spot.Left = x;
                    spot.Right = x + width;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = (y + height) + this.graphstartsY;
                    spots.Add(spot);
                }
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawStackedRadar(bool full)
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal num2 = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.x.Count;
            int n = this.data.Rows.Count;
            if (n == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                decimal num5 = 0M;
                for (int k = 0; k < count; k++)
                {
                    if (row[this.x[k]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[k] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    num5 += Convert.ToDecimal(row[this.x[k]]);
                }
                if (num5 < num2)
                {
                    num2 = num5;
                }
                if (num5 > heighest)
                {
                    heighest = num5;
                }
            }
            if (num2 > 0M)
            {
                num2 = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int r = (Math.Min(this.graphHeight, this.graphWidth) - 80) / 2;
            int xc = this.graphWidth / 2;
            int yc = this.graphHeight / 2;
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
            Pen pen = new Pen(brush, 5f);
            Pen pen2 = new Pen(Color.Black, 1f);
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, this.graphWidth, this.graphHeight));
            g.Clip = new Region(path);
            this.drawRadarAxis(g, xc, yc, r, heighest, n);
            int num10 = -2147483648;
            int num11 = -2147483648;
            if (this.ShadowIsDrawn)
            {
                for (int m = 0; m < (n + 1); m++)
                {
                    int num13 = m % n;
                    decimal num14 = 0M;
                    for (int num15 = 0; num15 < count; num15++)
                    {
                        num14 += Convert.ToDecimal(this.data.Rows[num13][this.x[num15]]);
                    }
                    int num16 = 0;
                    try
                    {
                        num16 = Convert.ToInt32((decimal) ((num14 / (heighest - num2)) * r));
                    }
                    catch
                    {
                    }
                    double a = ((num13 * 2) * 3.1415926535897931) / ((double) n);
                    int num18 = (Convert.ToInt32((double) (Math.Sin(a) * num16)) + xc) + this.ShadowOffset.X;
                    int num19 = (Convert.ToInt32((double) (Math.Cos(a) * num16)) + yc) + this.ShadowOffset.Y;
                    if (num10 != -2147483648)
                    {
                        if (!full)
                        {
                            g.DrawLine(pen, num10, num11, num18, num19);
                        }
                        else
                        {
                            Point[] points = new Point[] { new Point(num10, num11), new Point(num18, num19), new Point(xc, yc) };
                            g.FillPolygon(brush, points);
                        }
                    }
                    num10 = num18;
                    num11 = num19;
                }
            }
            List<Charts.Label> list = new List<Charts.Label>();
            int[] numArray = new int[count];
            int[] numArray2 = new int[count];
            int[] numArray3 = new int[count];
            int[] numArray4 = new int[count];
            for (int i = 0; i < count; i++)
            {
                numArray[i] = -2147483648;
            }
            for (int j = 0; j < (n + 1); j++)
            {
                int num22 = j % n;
                decimal num23 = 0M;
                double num24 = ((num22 * 2) * 3.1415926535897931) / ((double) n);
                for (int num25 = 0; num25 < count; num25++)
                {
                    num23 += Convert.ToDecimal(this.data.Rows[num22][this.x[num25]]);
                    int num26 = 0;
                    try
                    {
                        num26 = Convert.ToInt32((decimal) ((num23 / (heighest - num2)) * r));
                    }
                    catch
                    {
                    }
                    numArray3[num25] = Convert.ToInt32((double) (Math.Sin(num24) * num26)) + xc;
                    numArray4[num25] = Convert.ToInt32((double) (Math.Cos(num24) * num26)) + yc;
                    if (numArray[num25] != -2147483648)
                    {
                        Brush brush2 = new SolidBrush(Color.FromArgb(this.alpha, this.col[num25 % this.col.Count]));
                        Pen pen3 = new Pen(Color.FromArgb(this.alpha, this.col[num25 % this.col.Count]), 5f);
                        if (!full)
                        {
                            g.DrawLine(pen3, numArray[num25], numArray2[num25], numArray3[num25], numArray4[num25]);
                            this.xmlChart.AddLine(numArray[num25], numArray2[num25], numArray3[num25], numArray4[num25], 5, this.col[num25 % this.col.Count].ToString(), this.alpha);
                        }
                        else if (num25 > 0)
                        {
                            Point[] pointArray2 = new Point[] { new Point(numArray[num25], numArray2[num25]), new Point(numArray3[num25], numArray4[num25]), new Point(numArray3[num25 - 1], numArray4[num25 - 1]), new Point(numArray[num25 - 1], numArray2[num25 - 1]) };
                            g.FillPolygon(brush2, pointArray2);
                            this.xmlChart.AddQuadrilater(numArray[num25], numArray2[num25], numArray3[num25], numArray4[num25], numArray3[num25 - 1], numArray4[num25 - 1], numArray[num25 - 1], numArray2[num25 - 1], 0, this.col[num25 % this.col.Count].ToString(), this.alpha);
                        }
                        else
                        {
                            Point[] pointArray3 = new Point[] { new Point(numArray[0], numArray2[0]), new Point(numArray3[0], numArray4[0]), new Point(xc, yc) };
                            g.FillPolygon(brush2, pointArray3);
                            this.xmlChart.AddTriangle(numArray[0], numArray2[0], numArray3[0], numArray4[0], xc, yc, 0, this.col[num25 % this.col.Count].ToString(), this.alpha);
                        }
                        g.DrawLine(pen2, xc, yc, numArray3[num25], numArray4[num25]);
                        if ((this.ValueLabelsFormatString != null) && (this.ValueLabelsFormatString != string.Empty))
                        {
                            string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[num22][this.x[num25]]));
                            SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                            Charts.Label item = new Charts.Label(text, numArray3[num25] - (size.Width / 2f), numArray4[num25] - (size.Height / 2f), size);
                            list.Add(item);
                        }
                    }
                }
                for (int num27 = 0; num27 < count; num27++)
                {
                    numArray[num27] = numArray3[num27];
                    numArray2[num27] = numArray4[num27];
                }
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, 0, 0);
            return image;
        }

        private Bitmap drawStackedSurface()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            int num4 = this.x.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                decimal num5 = 0M;
                for (int j = 0; j < num4; j++)
                {
                    if (row[this.x[j]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[j] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    num5 += Convert.ToDecimal(row[this.x[j]]);
                }
                if (num5 < lowest)
                {
                    lowest = num5;
                }
                if (num5 > heighest)
                {
                    heighest = num5;
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num8 = ((float) this.graphWidth) / (count + 1f);
            int num9 = Convert.ToInt32((double) (num8 / 2.0));
            int offsetYTop = 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num8));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num8, 0, this.graphHeight, offsetYTop, Convert.ToInt32((double) (num8 / 2.0)), true);
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF((float) offsetX, (float) offsetYTop, (float) this.graphWidth, (float) this.graphHeight));
            g.Clip = new Region(path);
            offsetYTop -= this.negativeOffset;
            int[] numArray = new int[num4];
            int[] numArray2 = new int[num4];
            int[] numArray3 = new int[num4];
            numArray3[0] = -2147483648;
            int[] numArray4 = new int[num4];
            decimal[] numArray5 = new decimal[num4];
            decimal[] numArray6 = new decimal[num4];
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                decimal num12 = 0M;
                for (int k = 0; k < num4; k++)
                {
                    num12 += Convert.ToDecimal(this.data.Rows[i][this.x[k]]);
                    int num14 = 0;
                    try
                    {
                        num14 = Convert.ToInt32((decimal) ((num12 / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    numArray[k] = Convert.ToInt32((double) (((i + 0.5) * num8) + num9)) + offsetX;
                    numArray2[k] = (this.graphHeight - num14) + offsetYTop;
                    numArray6[k] = num12;
                    if (numArray3[0] != -2147483648)
                    {
                        Brush brush2 = new SolidBrush(Color.FromArgb(this.alpha, this.col[k % this.col.Count]));
                        if (this.ShadowIsDrawn)
                        {
                            if (k > 0)
                            {
                                Point[] points = new Point[] { new Point(numArray[k] + this.ShadowOffset.X, numArray2[k] + this.ShadowOffset.Y), new Point(numArray3[k] + this.ShadowOffset.X, numArray4[k] + this.ShadowOffset.Y), new Point(numArray3[k - 1] + this.ShadowOffset.X, numArray4[k - 1] + this.ShadowOffset.Y), new Point(numArray[k - 1] + this.ShadowOffset.X, numArray2[k - 1] + this.ShadowOffset.Y) };
                                g.FillPolygon(brush, points);
                            }
                            else
                            {
                                int num15 = this.graphHeight + offsetYTop;
                                Point[] pointArray2 = new Point[] { new Point(numArray[k] + this.ShadowOffset.X, numArray2[k] + this.ShadowOffset.Y), new Point(numArray3[k] + this.ShadowOffset.X, numArray4[k] + this.ShadowOffset.Y), new Point(numArray3[k] + this.ShadowOffset.X, num15 + this.ShadowOffset.Y), new Point(numArray[k] + this.ShadowOffset.X, num15 + this.ShadowOffset.Y) };
                                g.FillPolygon(brush, pointArray2);
                            }
                        }
                        if (k > 0)
                        {
                            Point[] pointArray3 = new Point[] { new Point(numArray[k], numArray2[k]), new Point(numArray3[k], numArray4[k]), new Point(numArray3[k - 1], numArray4[k - 1]), new Point(numArray[k - 1], numArray2[k - 1]) };
                            g.FillPolygon(brush2, pointArray3);
                            this.xmlChart.AddQuadrilater(numArray[k], numArray2[k], numArray3[k], numArray4[k], numArray3[k - 1], numArray4[k - 1], numArray[k - 1], numArray2[k - 1], 0, new ColorItem((brush2 as SolidBrush).Color).ToString(), this.alpha);
                        }
                        else
                        {
                            int y = this.graphHeight + offsetYTop;
                            Point[] pointArray4 = new Point[] { new Point(numArray[k], numArray2[k]), new Point(numArray3[k], numArray4[k]), new Point(numArray3[k], y), new Point(numArray[k], y) };
                            g.FillPolygon(brush2, pointArray4);
                            this.xmlChart.AddQuadrilater(numArray[k], numArray2[k], numArray3[k], numArray4[k], numArray3[k], y, numArray[k], y, 0, new ColorItem((brush2 as SolidBrush).Color).ToString(), this.alpha);
                        }
                        if (this.ValueLabelsFormatString != string.Empty)
                        {
                            string text = string.Format(this.ValueLabelsFormatString, numArray5[k]);
                            SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                            list.Add(new Charts.Label(text, numArray3[k] - (size.Width / 2f), (float) (numArray4[k] + 1), size));
                        }
                        if ((k == (num4 - 1)) && (this.ValueLabelsFormatString != string.Empty))
                        {
                            string str2 = string.Format(this.ValueLabelsFormatString, numArray6[k]);
                            SizeF ef2 = g.MeasureString(str2, this.ValueLabelsFont);
                            list.Add(new Charts.Label(str2, numArray[k] - (ef2.Width / 2f), (float) (numArray2[k] + 1), ef2));
                        }
                    }
                }
                for (int m = 0; m < num4; m++)
                {
                    numArray3[m] = numArray[m];
                    numArray4[m] = numArray2[m];
                    numArray5[m] = numArray6[m];
                }
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, 10);
            g.Clip = new Region();
            return image;
        }

        private Bitmap drawStackedSurface3D()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 0M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            int num4 = this.x.Count;
            foreach (DataRow row in this.data.Rows)
            {
                decimal num5 = 0M;
                for (int k = 0; k < num4; k++)
                {
                    if (row[this.x[k]] == DBNull.Value)
                    {
                        throw new Exception("ERROR: DataItem '" + this.x[k] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                    }
                    num5 += Convert.ToDecimal(row[this.x[k]]);
                }
                if (num5 > heighest)
                {
                    heighest = num5;
                }
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            double num7 = ((double) this.graphWidth) / ((count + 1f) + this.Percent3D);
            int depth = Convert.ToInt32((double) (Convert.ToInt32(num7) * this.Percent3D));
            int offsetX = this.ComputeYAxisSpace(g, heighest) + depth;
            int offsetYTop = depth + 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num7));
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF((float) offsetX, (float) offsetYTop, (float) this.graphWidth, (float) this.graphHeight));
            g.Clip = new Region(path);
            offsetYTop -= this.negativeOffset;
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.graphWidth -= offsetX;
            num7 = ((double) this.graphWidth) / ((count + 1) + this.Percent3D);
            int spacer = Convert.ToInt32(num7) / 2;
            depth = Convert.ToInt32((double) (num7 * this.Percent3D));
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num7, depth, this.graphHeight, offsetYTop, spacer, false);
            this.ShadowOffset.X = Math.Abs(this.ShadowOffset.X);
            Brush[] brushArray = new SolidBrush[num4];
            Brush[] brushArray2 = new SolidBrush[num4];
            for (int i = 0; i < num4; i++)
            {
                int index = i % this.col.Count;
                Color color = this.col[index].color;
                brushArray[index] = new SolidBrush(Color.FromArgb(this.alpha, this.col[index]));
                Color baseColor = Color.FromArgb((color.R / 10) * 5, (color.G / 10) * 5, (color.B / 10) * 5);
                brushArray2[index] = new SolidBrush(Color.FromArgb(this.alpha, Color.FromArgb(this.alpha, baseColor)));
            }
            new Pen(this.ForeColor);
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black));
            int[] numArray = new int[num4];
            int[] numArray2 = new int[num4];
            int[] numArray3 = new int[num4];
            numArray3[0] = -2147483648;
            int[] numArray4 = new int[num4];
            decimal[] numArray5 = new decimal[num4];
            decimal[] numArray6 = new decimal[num4];
            List<Charts.Label> list = new List<Charts.Label>();
            for (int j = 0; j < count; j++)
            {
                decimal num16 = 0M;
                for (int m = 0; m < num4; m++)
                {
                    num16 += Convert.ToDecimal(this.data.Rows[j][this.x[m]]);
                    int num18 = 0;
                    try
                    {
                        num18 = Convert.ToInt32((decimal) ((num16 / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    numArray[m] = Convert.ToInt32((double) (((j + 0.5) * num7) + spacer)) + offsetX;
                    numArray2[m] = (this.graphHeight - num18) + offsetYTop;
                    numArray6[m] = num16;
                    if (numArray3[0] != -2147483648)
                    {
                        if (this.ShadowIsDrawn)
                        {
                            if (m > 0)
                            {
                                Point[] points = new Point[] { new Point((numArray[m] + this.ShadowOffset.X) + depth, (numArray2[m] + this.ShadowOffset.Y) - depth), new Point((numArray3[m] + this.ShadowOffset.X) + depth, (numArray4[m] + this.ShadowOffset.Y) - depth), new Point((numArray3[m - 1] + this.ShadowOffset.X) + depth, (numArray4[m - 1] + this.ShadowOffset.Y) - depth), new Point((numArray[m - 1] + this.ShadowOffset.X) + depth, (numArray2[m - 1] + this.ShadowOffset.Y) - depth) };
                                g.FillPolygon(brush, points);
                            }
                            else
                            {
                                int y = this.graphHeight + offsetYTop;
                                Point[] pointArray2 = new Point[] { new Point((numArray[m] + this.ShadowOffset.X) + depth, (numArray2[m] + this.ShadowOffset.Y) - depth), new Point((numArray3[m] + this.ShadowOffset.X) + depth, (numArray4[m] + this.ShadowOffset.Y) - depth), new Point((numArray3[m] + this.ShadowOffset.X) + depth, y), new Point((numArray[m] + this.ShadowOffset.X) + depth, y) };
                                g.FillPolygon(brush, pointArray2);
                            }
                        }
                        if (m > 0)
                        {
                            Point[] pointArray3 = new Point[] { new Point(numArray3[m] + depth, numArray4[m] - depth), new Point(numArray[m] + depth, numArray2[m] - depth), new Point(numArray[m], numArray2[m]), new Point(numArray3[m], numArray4[m]) };
                            g.FillPolygon(brushArray2[m], pointArray3);
                            Point[] pointArray4 = new Point[] { new Point(numArray[m] + depth, numArray2[m] - depth), new Point(numArray[m], numArray2[m]), new Point(numArray[m], numArray2[m - 1]), new Point(numArray[m] + depth, numArray2[m - 1] - depth) };
                            g.FillPolygon(brushArray2[m], pointArray4);
                            Point[] pointArray5 = new Point[] { new Point(numArray3[m], numArray4[m]), new Point(numArray[m], numArray2[m]), new Point(numArray[m], numArray2[m - 1]), new Point(numArray3[m], numArray4[m - 1]) };
                            g.FillPolygon(brushArray[m], pointArray5);
                            this.xmlChart.AddQuadrilater3D(numArray3[m - 1], numArray4[m - 1], numArray3[m], numArray4[m], numArray[m], numArray2[m], numArray[m - 1], numArray2[m - 1], 0, this.col[m % this.col.Count].ToString(), this.alpha, depth, new ColorItem((brushArray2[m] as SolidBrush).Color).ToString());
                        }
                        else
                        {
                            int num20 = this.graphHeight + offsetYTop;
                            Point[] pointArray6 = new Point[] { new Point(numArray3[m] + depth, numArray4[m] - depth), new Point(numArray[m] + depth, numArray2[m] - depth), new Point(numArray[m] + depth, num20 - depth), new Point(numArray3[m] + depth, num20 - depth) };
                            g.FillPolygon(brushArray[m], pointArray6);
                            Point[] pointArray7 = new Point[] { new Point(numArray3[m] + depth, num20 - depth), new Point(numArray[m] + depth, num20 - depth), new Point(numArray[m], num20), new Point(numArray3[m], num20) };
                            g.FillPolygon(brushArray2[m], pointArray7);
                            if (j == 1)
                            {
                                Point[] pointArray8 = new Point[] { new Point(numArray3[m] + depth, numArray4[m] - depth), new Point(numArray3[m], numArray4[m]), new Point(numArray3[m], num20), new Point(numArray3[m] + depth, num20 - depth) };
                                g.FillPolygon(brushArray2[m], pointArray8);
                            }
                            Point[] pointArray9 = new Point[] { new Point(numArray3[m] + depth, numArray4[m] - depth), new Point(numArray[m] + depth, numArray2[m] - depth), new Point(numArray[m], numArray2[m]), new Point(numArray3[m], numArray4[m]) };
                            g.FillPolygon(brushArray2[m], pointArray9);
                            Point[] pointArray10 = new Point[] { new Point(numArray[m] + depth, numArray2[m] - depth), new Point(numArray[m], numArray2[m]), new Point(numArray[m], num20), new Point(numArray[m] + depth, num20 - depth) };
                            g.FillPolygon(brushArray2[m], pointArray10);
                            Point[] pointArray11 = new Point[] { new Point(numArray3[m], numArray4[m]), new Point(numArray[m], numArray2[m]), new Point(numArray[m], num20), new Point(numArray3[m], num20) };
                            g.FillPolygon(brushArray[m], pointArray11);
                            this.xmlChart.AddQuadrilater3D(numArray3[m], num20, numArray3[m], numArray4[m], numArray[m], numArray2[m], numArray[m], num20, 0, this.col[m % this.col.Count].ToString(), this.alpha, depth, new ColorItem((brushArray2[m] as SolidBrush).Color).ToString());
                        }
                    }
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, numArray5[m]);
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, numArray3[m] - (size.Width / 2f), (float) (numArray4[m] + 1), size));
                    }
                    if ((m == (num4 - 1)) && (this.ValueLabelsFormatString != string.Empty))
                    {
                        string str2 = string.Format(this.ValueLabelsFormatString, numArray6[m]);
                        SizeF ef2 = g.MeasureString(str2, this.ValueLabelsFont);
                        list.Add(new Charts.Label(str2, numArray[m] - (ef2.Width / 2f), (float) (numArray2[m] + 1), ef2));
                    }
                }
                for (int n = 0; n < num4; n++)
                {
                    numArray3[n] = numArray[n];
                    numArray4[n] = numArray2[n];
                    numArray5[n] = numArray6[n];
                }
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, 10);
            g.Clip = new Region();
            return image;
        }

        private Bitmap drawSurface()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDecimal(row[this.x[0]]) < lowest)
                {
                    lowest = Convert.ToDecimal(row[this.x[0]]);
                }
                if (Convert.ToDecimal(row[this.x[0]]) > heighest)
                {
                    heighest = Convert.ToDecimal(row[this.x[0]]);
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num5 = ((float) this.graphWidth) / (count + 1f);
            int num6 = Convert.ToInt32((double) (num5 / 2.0));
            int offsetYTop = 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num5));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num5, 0, this.graphHeight, offsetYTop, Convert.ToInt32((double) (num5 / 2.0)), true);
            Brush brush = new SolidBrush(Color.FromArgb(this.alpha, this.col[0]));
            Brush brush2 = new SolidBrush(Color.FromArgb(50, Color.Black));
            int x = -2147483648;
            int y = -2147483648;
            int num10 = offsetYTop + this.graphHeight;
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF((float) offsetX, (float) offsetYTop, (float) this.graphWidth, (float) this.graphHeight));
            g.Clip = new Region(path);
            offsetYTop -= this.negativeOffset;
            for (int i = 0; i < count; i++)
            {
                int num12 = 0;
                try
                {
                    num12 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[i][this.x[0]]) / (heighest - lowest)) * this.graphHeight));
                }
                catch
                {
                }
                int num13 = Convert.ToInt32((double) (((i + 0.5) * num5) + num6)) + offsetX;
                int num14 = (this.graphHeight - num12) + offsetYTop;
                if (x != -2147483648)
                {
                    if (this.ShadowIsDrawn)
                    {
                        int num15 = y - this.ShadowOffset.Y;
                        int num16 = num14 - this.ShadowOffset.Y;
                        Point[] pointArray = new Point[] { new Point(x + this.ShadowOffset.X, num15), new Point(num13 + this.ShadowOffset.X, num16), new Point(num13 + this.ShadowOffset.X, num10), new Point(x + this.ShadowOffset.X, num10) };
                        g.FillPolygon(brush2, pointArray);
                    }
                    Point[] points = new Point[] { new Point(x, y), new Point(num13, num14), new Point(num13, num10), new Point(x, num10) };
                    g.FillPolygon(brush, points);
                    this.xmlChart.AddQuadrilater(x, y, num13, num14, num13, num10, x, num10, 0, this.col[0].ToString(), this.alpha);
                }
                x = num13;
                y = num14;
            }
            if (this.ValueLabelsFormatString != string.Empty)
            {
                for (int j = 0; j < count; j++)
                {
                    int num18 = 0;
                    try
                    {
                        num18 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[0]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    int num19 = Convert.ToInt32((double) (((j + 0.5) * num5) + num6)) + offsetX;
                    int num20 = (this.graphHeight - num18) + offsetYTop;
                    string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[j][this.x[0]]));
                    SizeF ef = g.MeasureString(text, this.ValueLabelsFont);
                    g.DrawString(text, this.ValueLabelsFont, Brushes.Black, (float) (num19 - (ef.Width / 2f)), (float) ((num20 - ef.Height) + 1f));
                }
            }
            g.Clip = new Region();
            return image;
        }

        private Bitmap drawSurface3D()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDecimal(row[this.x[0]]) < lowest)
                {
                    lowest = Convert.ToDecimal(row[this.x[0]]);
                }
                if (Convert.ToDecimal(row[this.x[0]]) > heighest)
                {
                    heighest = Convert.ToDecimal(row[this.x[0]]);
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap image = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = this.smoothingMode;
            double num4 = ((double) this.graphWidth) / ((count + 1f) + this.Percent3D);
            int depth = Convert.ToInt32((double) (Convert.ToInt32(num4) * this.Percent3D));
            int offsetX = this.ComputeYAxisSpace(g, heighest) + depth;
            int offsetYTop = depth + 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num4));
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF((float) offsetX, (float) offsetYTop, (float) this.graphWidth, (float) this.graphHeight));
            g.Clip = new Region(path);
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.graphWidth -= offsetX;
            num4 = ((double) this.graphWidth) / ((count + 1) + this.Percent3D);
            int spacer = Convert.ToInt32(num4) / 2;
            depth = Convert.ToInt32((double) (num4 * this.Percent3D));
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num4, depth, this.graphHeight, offsetYTop, spacer, false);
            offsetYTop -= this.negativeOffset;
            Brush brush = new SolidBrush(Color.FromArgb(this.alpha, this.col[0]));
            Color color = this.col[0].color;
            Color baseColor = Color.FromArgb((color.R / 10) * 5, (color.G / 10) * 5, (color.B / 10) * 5);
            Brush brush2 = new SolidBrush(Color.FromArgb(this.alpha, Color.FromArgb(this.alpha, baseColor)));
            new Pen(this.ForeColor);
            Brush brush3 = new SolidBrush(Color.FromArgb(50, Color.Black));
            int x = -2147483648;
            int y = -2147483648;
            int num12 = offsetYTop + this.graphHeight;
            for (int i = 0; i < count; i++)
            {
                int num14 = 0;
                try
                {
                    num14 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[i][this.x[0]]) / (heighest - lowest)) * this.graphHeight));
                }
                catch
                {
                }
                int num15 = Convert.ToInt32((double) ((i + 1) * num4)) + offsetX;
                int num16 = (this.graphHeight - num14) + offsetYTop;
                if (x != -2147483648)
                {
                    if (this.ShadowIsDrawn)
                    {
                        int num17 = y - this.ShadowOffset.Y;
                        int num18 = num16 - this.ShadowOffset.Y;
                        Point[] pointArray = new Point[] { new Point((x + this.ShadowOffset.X) + depth, num17 - depth), new Point((num15 + this.ShadowOffset.X) + depth, num18 - depth), new Point((num15 + this.ShadowOffset.X) + depth, num12 - depth), new Point((x + this.ShadowOffset.X) + depth, num12 - depth) };
                        g.FillPolygon(brush3, pointArray);
                    }
                    Point[] points = new Point[] { new Point(x + depth, y - depth), new Point(num15 + depth, num16 - depth), new Point(num15 + depth, num12 - depth), new Point(x + depth, num12 - depth) };
                    g.FillPolygon(brush, points);
                    Point[] pointArray3 = new Point[] { new Point(x + depth, num12 - depth), new Point(num15 + depth, num12 - depth), new Point(num15, num12), new Point(x, num12) };
                    g.FillPolygon(brush2, pointArray3);
                    if (i == 1)
                    {
                        Point[] pointArray4 = new Point[] { new Point(x + depth, y - depth), new Point(x, y), new Point(x, num12), new Point(x + depth, num12 - depth) };
                        g.FillPolygon(brush2, pointArray4);
                    }
                    Point[] pointArray5 = new Point[] { new Point(x + depth, y - depth), new Point(num15 + depth, num16 - depth), new Point(num15, num16), new Point(x, y) };
                    g.FillPolygon(brush2, pointArray5);
                    Point[] pointArray6 = new Point[] { new Point(num15 + depth, num16 - depth), new Point(num15, num16), new Point(num15, num12), new Point(num15 + depth, num12 - depth) };
                    g.FillPolygon(brush2, pointArray6);
                    Point[] pointArray7 = new Point[] { new Point(x, y), new Point(num15, num16), new Point(num15, num12), new Point(x, num12) };
                    g.FillPolygon(brush, pointArray7);
                }
                x = num15;
                y = num16;
            }
            if (this.ValueLabelsFormatString != string.Empty)
            {
                for (int j = 0; j < count; j++)
                {
                    int num20 = 0;
                    try
                    {
                        num20 = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[j][this.x[0]]) / (heighest - lowest)) * this.graphHeight));
                    }
                    catch
                    {
                    }
                    int num21 = Convert.ToInt32((double) (((j + 0.5) * num4) + spacer)) + offsetX;
                    int num22 = (this.graphHeight - num20) + offsetYTop;
                    string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[j][this.x[0]]));
                    SizeF ef = g.MeasureString(text, this.ValueLabelsFont);
                    g.DrawString(text, this.ValueLabelsFont, Brushes.Black, (float) (num21 - (ef.Width / 2f)), (float) ((num22 - ef.Height) + 1f));
                }
            }
            g.Clip = new Region();
            return image;
        }

        private Bitmap drawUserDrawnBars()
        {
            System.Drawing.Image winUserImage;
            System.Drawing.Image image2;
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDecimal(row[this.x[0]]) < lowest)
                {
                    lowest = Convert.ToDecimal(row[this.x[0]]);
                }
                if (Convert.ToDecimal(row[this.x[0]]) > heighest)
                {
                    heighest = Convert.ToDecimal(row[this.x[0]]);
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            int offsetX = this.ComputeYAxisSpace(g, heighest);
            this.graphWidth -= offsetX;
            double num5 = ((float) this.graphWidth) / (count + 1f);
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num5));
            int offsetYTop = 10;
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            int width = Convert.ToInt32((double) (num5 * this.PercentWidth));
            int spacer = Convert.ToInt32((double) (num5 - width)) / 2;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, 0);
            this.drawXAxis(g, offsetX, (float) num5, 0, this.graphHeight, offsetYTop, spacer, false);
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            new SolidBrush(Color.FromArgb(this.alpha, this.col[0]));
            HotSpotCollection spots = new HotSpotCollection();
            if (this.WinUserImage != null)
            {
                winUserImage = this.WinUserImage as System.Drawing.Image;
            }
            else
            {
                if (this.userImagePath != string.Empty)
                {
                    try
                    {
                        winUserImage = System.Drawing.Image.FromFile(this.userImagePath);
                        goto Label_02E5;
                    }
                    catch
                    {
                        throw new Exception("ERROR: No Image file specified for this type of chart!");
                    }
                }
                return bitmap;
            }
        Label_02E5:
            image2 = this.LoadImage("barShadow.png");
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                int height = 0;
                try
                {
                    height = Convert.ToInt32((decimal) ((Convert.ToDecimal(this.data.Rows[i][this.x[0]]) / (heighest - lowest)) * this.graphHeight));
                }
                catch
                {
                }
                int x = Convert.ToInt32((double) ((i + 0.5) * num5)) + offsetX;
                int y = (this.graphHeight - height) + offsetYTop;
                if (this.ShadowIsDrawn)
                {
                    if (height < image2.Height)
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, height - this.ShadowOffset.Y), new Rectangle(0, 0, image2.Width, height - this.ShadowOffset.Y), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, width, height - this.ShadowOffset.Y));
                    }
                }
                int num13 = Convert.ToInt32((float) ((((float) winUserImage.Width) / ((float) width)) * height));
                if (num13 > winUserImage.Height)
                {
                    num13 = winUserImage.Height;
                }
                g.DrawImage(winUserImage, new Rectangle(x, y, width, height), new Rectangle(0, 0, winUserImage.Width, num13), GraphicsUnit.Pixel);
                if (this.chartDataControl != null)
                {
                    this.xmlChart.AddUserBar(height, width, (float) x, (float) y, "0", 0xff, (this.chartDataControl as McWebChart).UserImage, 0, this.GetNavigateUrl(this.GetID(i) + "||" + i));
                }
                if (this.ValueLabelsFormatString != string.Empty)
                {
                    string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[i][this.x[0]]));
                    SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                    list.Add(new Charts.Label(text, x + ((width - size.Width) / 2f), (y - size.Height) + 1f, size));
                }
                RectangleHotSpot spot = new RectangleHotSpot();
                if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                {
                    spot.AlternateText = this.data.Rows[i][this.XAxisLabels].ToString();
                }
                spot.PostBackValue = this.GetID(i) + "||" + i;
                spot.Left = x;
                spot.Right = x + width;
                spot.Top = y + this.graphstartsY;
                spot.Bottom = (y + height) + this.graphstartsY;
                spots.Add(spot);
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private Bitmap drawWideBars3D()
        {
            decimal heighest = -79228162514264337593543950335M;
            decimal lowest = 79228162514264337593543950335M;
            if (this.data == null)
            {
                throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
            }
            if (this.x.Count == 0)
            {
                throw new Exception("ERROR: No Data Series set! Use either DataSeries property or setDataSeries method to set the series that will appear on the Y axis.");
            }
            int count = this.data.Rows.Count;
            if (count == 0)
            {
                throw new Exception("No Data Found!");
            }
            foreach (DataRow row in this.data.Rows)
            {
                if (row[this.x[0]] == DBNull.Value)
                {
                    throw new Exception("ERROR: DataItem '" + this.x[0] + "' not found! Check the DataSeries property elements or setDataSeries method to set the corect series that will appear on the Y axis.");
                }
                if (Convert.ToDecimal(row[this.x[0]]) < lowest)
                {
                    lowest = Convert.ToDecimal(row[this.x[0]]);
                }
                if (Convert.ToDecimal(row[this.x[0]]) > heighest)
                {
                    heighest = Convert.ToDecimal(row[this.x[0]]);
                }
            }
            if (lowest > 0M)
            {
                lowest = 0M;
            }
            Bitmap bitmap = new Bitmap(this.graphWidth, this.graphHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = this.smoothingMode;
            double num4 = ((double) this.graphWidth) / ((count + 1f) + this.Percent3D);
            int depth = Convert.ToInt32((double) (Convert.ToInt32((double) (num4 * this.PercentWidth)) * this.Percent3D));
            int offsetX = this.ComputeYAxisSpace(g, heighest) + depth;
            int offsetYTop = depth + 10;
            this.offsetYBottom = this.ComputeXAxisSpace(g, Convert.ToInt32(num4));
            this.graphHeight -= this.offsetYBottom + offsetYTop;
            this.graphWidth -= offsetX;
            num4 = ((double) this.graphWidth) / ((count + 1) + this.Percent3D);
            int width = Convert.ToInt32((double) (num4 * this.PercentWidth));
            this.ShadowOffset.X = Convert.ToInt32((double) (0.5 * width));
            depth = Convert.ToInt32((double) (width * this.Percent3D));
            int spacer = Convert.ToInt32((double) (num4 - width)) / 2;
            this.drawYAxis(g, lowest, heighest, offsetYTop, this.offsetYBottom, offsetX, depth);
            this.drawXAxis(g, offsetX, (float) num4, depth, this.graphHeight, offsetYTop, spacer, false);
            offsetYTop -= this.negativeOffset;
            Color baseColor = this.col[0];
            Brush brush = new SolidBrush(Color.FromArgb(this.alpha, baseColor));
            Brush brush2 = new SolidBrush(Color.FromArgb((this.alpha * 3) / 4, baseColor));
            Brush brush3 = new SolidBrush(Color.FromArgb(this.alpha / 2, baseColor));
            Pen pen = new Pen(this.ForeColor, 1f);
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = (float) this.effectopacity;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            HotSpotCollection spots = new HotSpotCollection();
            System.Drawing.Image image = this.LoadImage(string.Format("light{0}.png", this.effect));
            System.Drawing.Image image2 = this.LoadImage(string.Format("barShadow.png", new object[0]));
            List<Charts.Label> list = new List<Charts.Label>();
            for (int i = 0; i < count; i++)
            {
                int height = 0;
                try
                {
                    height = Convert.ToInt32((decimal) ((ScaleBreaks.ValRec(this.data.Rows[i][this.x[0]], this.ScaleBreakMin, this.ScaleBreakMax) / (((heighest - lowest) + this.ScaleBreakMin) - this.ScaleBreakMax)) * (this.graphHeight - this.ScaleBreakHeight)));
                }
                catch
                {
                }
                if (height != 0)
                {
                    int x = Convert.ToInt32((double) ((i + 0.5) * num4)) + offsetX;
                    int y = (this.graphHeight - height) + offsetYTop;
                    if (y < this.ScaleBreakPos)
                    {
                        height += this.ScaleBreakHeight;
                        y -= this.ScaleBreakHeight;
                    }
                    if (this.ShadowIsDrawn)
                    {
                        if (height < image2.Height)
                        {
                            g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num4), (height - this.ShadowOffset.Y) - depth), new Rectangle(0, 0, image2.Width, (height - this.ShadowOffset.Y) - depth), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            g.DrawImage(image2, new Rectangle(x + this.ShadowOffset.X, y + this.ShadowOffset.Y, Convert.ToInt32(num4), (height - this.ShadowOffset.Y) - depth));
                        }
                    }
                    if (height > 0)
                    {
                        g.FillRectangle(brush, x, y, width, height);
                        g.DrawImage(image, new Rectangle(x, y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                        g.DrawRectangle(pen, x, y, width, height);
                        Point[] points = new Point[] { new Point(x + width, y), new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point((x + width) + depth, y - depth) };
                        Point[] destPoints = new Point[] { points[0], points[3], points[1] };
                        g.FillPolygon(brush2, points);
                        g.DrawImage(image, destPoints, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                        g.DrawPolygon(pen, points);
                        Point[] pointArray3 = new Point[] { new Point(x + width, y), new Point((x + width) + depth, y - depth), new Point(x + depth, y - depth), new Point(x, y) };
                        Point[] pointArray4 = new Point[] { pointArray3[0], pointArray3[3], pointArray3[1] };
                        g.FillPolygon(brush3, pointArray3);
                        g.DrawImage(image, pointArray4, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                        g.DrawPolygon(pen, pointArray3);
                    }
                    else
                    {
                        g.FillRectangle(brush, x, y + height, width, -height);
                        g.DrawImage(image, new Rectangle(x, y + height, width, -height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                        g.DrawRectangle(pen, x, y + height, width, -height);
                        Point[] pointArray5 = new Point[] { new Point(x + width, y), new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point((x + width) + depth, y - depth) };
                        Point[] pointArray6 = new Point[] { pointArray5[0], pointArray5[3], pointArray5[1] };
                        g.FillPolygon(brush2, pointArray5);
                        g.DrawImage(image, pointArray6, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                        g.DrawPolygon(pen, pointArray5);
                        Point[] pointArray7 = new Point[] { new Point(x + width, y + height), new Point((x + width) + depth, (y + height) - depth), new Point(x + depth, (y + height) - depth), new Point(x, y + height) };
                        Point[] pointArray8 = new Point[] { pointArray7[0], pointArray7[3], pointArray7[1] };
                        g.FillPolygon(brush3, pointArray7);
                        g.DrawImage(image, pointArray8, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, imageAttr);
                        g.DrawPolygon(pen, pointArray7);
                    }
                    this.xmlChart.AddBar(height, width, (float) x, (float) y, new ColorItem((brush as SolidBrush).Color).ToString(), (brush as SolidBrush).Color.A, depth, 0, this.GetNavigateUrl(this.GetID(i) + "||" + i), this.GetToolTip(i));
                    if (this.ValueLabelsFormatString != string.Empty)
                    {
                        string text = string.Format(this.ValueLabelsFormatString, Convert.ToDecimal(this.data.Rows[i][this.x[0]]));
                        SizeF size = g.MeasureString(text, this.ValueLabelsFont);
                        list.Add(new Charts.Label(text, x + (((width - size.Width) + depth) / 2f), ((y - size.Height) + 1f) - (depth / 2), size));
                    }
                    RectangleHotSpot spot = new RectangleHotSpot();
                    if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
                    {
                        spot.AlternateText = this.data.Rows[i][this.XAxisLabels].ToString();
                    }
                    spot.PostBackValue = this.GetID(i) + "||" + i;
                    spot.Left = x;
                    spot.Right = (x + width) + depth;
                    spot.Top = y + this.graphstartsY;
                    spot.Bottom = (y + height) + this.graphstartsY;
                    spots.Add(spot);
                }
            }
            if (this.ScaleBreakMax > this.ScaleBreakMin)
            {
                ScaleBreaks.drawScaleBreak(this.xmlChart, g, this.BackColor2, this.ScaleBreakPos, this.ScaleBreakHeight, offsetX - 1, this.graphWidth, depth);
            }
            Charts.Label.DrawList(list, g, this.ValueLabelsFont, Brushes.Black, this.graphWidth, this.graphHeight, offsetX, offsetYTop);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.rez] = spots;
                return bitmap;
            }
            if (this.colectieHotspoturi != null)
            {
                this.colectieHotspoturi = spots;
            }
            return bitmap;
        }

        private void drawXAxis(Graphics g, int offsetX, float barWidth, int depth, int graphHeight, int offsetTop, int Spacer, bool fixLabel)
        {
            SizeF ef;
            Font labelsFont = this.LabelsFont;
            Brush brush = new SolidBrush(Color.Black);
            Brush brush2 = new SolidBrush(Color.FromArgb(200, Color.White));
            Pen pen = new Pen(brush2);
            Pen pen2 = new Pen(brush);
            if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
            {
                int length = 0;
                foreach (DataRow row in this.data.Rows)
                {
                    string str = string.Format(this.LabelsFormatString, row[this.XAxisLabels]);
                    if (str.Length > length)
                    {
                        length = str.Length;
                    }
                }
                string text = "8888888888888888888888888888888888888888888888888";
                if (length > text.Length)
                {
                    length = text.Length;
                }
                ef = g.MeasureString(text.Substring(0, length), labelsFont);
                int num2 = 0;
                if (ef.Width < barWidth)
                {
                    foreach (DataRow row2 in this.data.Rows)
                    {
                        string str3 = string.Format(this.LabelsFormatString, row2[this.XAxisLabels]);
                        text = (row2[this.XAxisLabels] != DBNull.Value) ? str3 : string.Empty;
                        ef = g.MeasureString(text, labelsFont);
                        float x = ((offsetX + (barWidth * (1f + num2))) - Spacer) - (ef.Width / 2f);
                        if (fixLabel)
                        {
                            x += barWidth / 2f;
                        }
                        g.DrawString(text, labelsFont, brush, x, (float) ((graphHeight + offsetTop) + 5));
                        this.xmlChart.AddLabel(text, x, (float) ((graphHeight + offsetTop) + 5), 0, labelsFont, false);
                        num2++;
                    }
                }
                else
                {
                    float num4 = (float) Math.Sqrt(2.0);
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    int num5 = Convert.ToInt32((double) ((ef.Height * 1.5) / ((double) barWidth)));
                    if (num5 < 1)
                    {
                        num5 = 1;
                    }
                    foreach (DataRow row3 in this.data.Rows)
                    {
                        if ((num2 % num5) != 0)
                        {
                            num2++;
                            continue;
                        }
                        string str4 = string.Format(this.LabelsFormatString, row3[this.XAxisLabels]);
                        text = (row3[this.XAxisLabels] != DBNull.Value) ? str4 : string.Empty;
                        ef = g.MeasureString(text, labelsFont);
                        float dx = (offsetX + (barWidth * (1 + num2))) - (ef.Width / num4);
                        float dy = (graphHeight + offsetTop) + (ef.Width / num4);
                        g.TranslateTransform(dx, dy);
                        g.RotateTransform(-45f);
                        g.DrawString(text, labelsFont, brush, (float) 0f, (float) 0f);
                        this.xmlChart.AddLabel(text, dx, dy, -45, labelsFont, false);
                        g.ResetTransform();
                        num2++;
                    }
                }
                if (this.DrawGrid)
                {
                    int num8 = Convert.ToInt32((double) ((ef.Height * 1.5) / ((double) barWidth)));
                    if (num8 < 1)
                    {
                        num8 = 1;
                    }
                    for (num2 = 0; num2 < (this.data.Rows.Count + 5); num2++)
                    {
                        if ((num2 % num8) == 0)
                        {
                            int num9 = Convert.ToInt32((float) (((offsetX + (barWidth * (0.5f + num2))) - Spacer) + depth));
                            if (num9 <= (this.graphWidth + offsetX))
                            {
                                g.DrawLine(pen2, num9, 0, num9, (graphHeight + offsetTop) - depth);
                                this.xmlChart.AddLine(num9, 0, num9, (graphHeight + offsetTop) - depth, 1, "0", 0xff);
                                g.DrawLine(pen2, num9, (graphHeight + offsetTop) - depth, num9 - depth, graphHeight + offsetTop);
                                this.xmlChart.AddLine(num9, (graphHeight + offsetTop) - depth, num9 - depth, graphHeight + offsetTop, 1, "0", 0xff);
                                num9++;
                                g.DrawLine(pen, num9, 0, num9, (graphHeight + offsetTop) - depth);
                                this.xmlChart.AddLine(num9, 0, num9, (graphHeight + offsetTop) - depth, 1, "0xFFFFFF", 200);
                                g.DrawLine(pen, num9, (graphHeight + offsetTop) - depth, num9 - depth, graphHeight + offsetTop);
                                this.xmlChart.AddLine(num9, (graphHeight + offsetTop) - depth, num9 - depth, graphHeight + offsetTop, 1, "0xFFFFFF", 200);
                            }
                        }
                    }
                }
            }
            if (this.ShowRecursionEquations)
            {
                int num10 = 10;
                foreach (KeyItem item in this.KeyLabels)
                {
                    if (((item.hint != null) && !(item.hint == "")) && ((item.str != null) && !(item.str == "")))
                    {
                        string str5 = item.str + item.hint;
                        SizeF ef2 = g.MeasureString(str5, this.ValueLabelsFont);
                        if ((num10 + ef2.Height) > ((graphHeight - offsetTop) - this.offsetYBottom))
                        {
                            break;
                        }
                        num10 += Convert.ToInt32(ef2.Height);
                        g.DrawString(str5, this.ValueLabelsFont, Brushes.Black, (float) ((offsetX + depth) + 10), (float) num10);
                        num10 += 5;
                    }
                }
            }
            if ((this.XName != string.Empty) && (this.XName != null))
            {
                labelsFont = this.AxisLabelFont;
                ef = g.MeasureString(this.XName, labelsFont);
                float num11 = offsetX + ((this.graphWidth - ef.Width) / 2f);
                float y = (((this.offsetYBottom + graphHeight) + offsetTop) - ef.Height) + 1f;
                g.DrawString(this.XName, labelsFont, brush, num11, y);
                this.xmlChart.AddLabel(this.XName, num11, y, 0, labelsFont, false);
            }
            this.xmlChart.EndMask();
            this.xmlChart.CurrentPhase = Phase.Elements;
        }

        private void drawYAxis(Graphics g, decimal lowest, decimal heighest, int offsetYTop, int offsetYBottom, int offsetX, int depth)
        {
            this.xmlChart.CurrentPhase = Phase.Background;
            decimal interval = heighest - lowest;
            if (this.ScaleBreakMax < heighest)
            {
                interval -= this.ScaleBreakMax - this.ScaleBreakMin;
            }
            else if (this.ScaleBreakMin < heighest)
            {
                interval -= heighest - this.ScaleBreakMin;
            }
            if (interval == 0M)
            {
                interval = 1M;
            }
            int num2 = this.determineYInterval(interval, this.graphHeight - this.ScaleBreakHeight, 30);
            this.negativeOffset = 0;
            if (lowest < 0M)
            {
                //?this.negativeOffset = (int)((decimal.op_UnaryNegation(lowest) * (this.graphHeight - this.ScaleBreakHeight)) / interval);
                this.negativeOffset = (int)Convert.ToInt32(((lowest) * (this.graphHeight - this.ScaleBreakHeight)) / interval);
            }
            decimal num3 = 0M;
            if (num2 > 0)
            {
                num3 = Math.Ceiling((decimal) (lowest / num2)) * num2;
            }
            Font labelsFont = this.LabelsFont;
            int num4 = offsetX - 5;
            Brush brush = new SolidBrush(Color.Black);
            Brush brush2 = new SolidBrush(Color.FromArgb(this.BackgroundDarkening, Color.Black));
            Pen pen = new Pen(brush, 1f);
            Point[] points = new Point[] { new Point(offsetX, depth), new Point(offsetX + depth, 0), new Point((offsetX + this.graphWidth) - 1, 0), new Point((offsetX + this.graphWidth) - 1, (this.graphHeight + offsetYTop) - depth), new Point((offsetX + this.graphWidth) - depth, this.graphHeight + offsetYTop), new Point(offsetX, this.graphHeight + offsetYTop), new Point(offsetX, depth) };
            Point[] pointArray2 = new Point[] { points[0], new Point(offsetX, 0), points[1] };
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(pointArray2);
            pointArray2 = new Point[] { new Point((offsetX + this.graphWidth) + 1, ((this.graphHeight + offsetYTop) - depth) + 1), new Point((offsetX + this.graphWidth) + 1, (this.graphHeight + offsetYTop) + 1), new Point(((offsetX + this.graphWidth) - depth) + 1, (this.graphHeight + offsetYTop) + 1) };
            path.AddPolygon(pointArray2);
            Region region = new Region();
            region.Xor(path);
            g.Clip = region;
            g.FillRectangle(new SolidBrush(this.BackColor), offsetX, 0, this.graphWidth, this.graphHeight + offsetYTop);
            g.DrawLines(pen, points);
            this.xmlChart.AddBackgroundMask(points);
            this.xmlChart.AddBar(this.graphHeight + offsetYTop, this.graphWidth, (float) offsetX, 0f, new ColorItem(this.BackColor).ToString(), 0xff, 0, 0, string.Empty, string.Empty);
            this.xmlChart.AddLine(num4 + 5, 0, num4 + 5, this.graphHeight + offsetYTop, 1, "0", 0xff);
            g.DrawLine(pen, (num4 + 5) + depth, -depth, (num4 + 5) + depth, (this.graphHeight + offsetYTop) - depth);
            this.xmlChart.AddLine((num4 + 5) + depth, -depth, (num4 + 5) + depth, (this.graphHeight + offsetYTop) - depth, 1, "0", 0xff);
            if (this.ScaleBreakMax > this.ScaleBreakMin)
            {
                this.ScaleBreakPos = (this.graphHeight - Convert.ToInt32((decimal) ((this.ScaleBreakMin / interval) * (this.graphHeight - this.ScaleBreakHeight)))) + offsetYTop;
            }
            foreach (BackgroundStripe stripe in this.BackgroundStripes)
            {
                decimal minValue = stripe.MinValue;
                decimal maxValue = stripe.MaxValue;
                if ((minValue > this.ScaleBreakMin) && (minValue < this.ScaleBreakMax))
                {
                    minValue += this.ScaleBreakMax;
                }
                if ((maxValue > this.ScaleBreakMin) && (maxValue < this.ScaleBreakMax))
                {
                    minValue += this.ScaleBreakMin;
                }
                if (minValue >= this.ScaleBreakMax)
                {
                    minValue -= this.ScaleBreakMax - this.ScaleBreakMin;
                }
                if (maxValue >= this.ScaleBreakMax)
                {
                    maxValue -= this.ScaleBreakMax - this.ScaleBreakMin;
                }
                int y = (this.graphHeight - Convert.ToInt32((decimal) ((minValue / interval) * (this.graphHeight - this.ScaleBreakHeight)))) + offsetYTop;
                int num8 = (this.graphHeight - Convert.ToInt32((decimal) ((maxValue / interval) * (this.graphHeight - this.ScaleBreakHeight)))) + offsetYTop;
                if (y < 0)
                {
                    y = 0;
                }
                if (num8 < 0)
                {
                    num8 = 0;
                }
                if ((y < this.ScaleBreakPos) && (this.ScaleBreakMax != this.ScaleBreakMin))
                {
                    y -= this.ScaleBreakHeight;
                }
                if ((num8 < this.ScaleBreakPos) && (this.ScaleBreakMax != this.ScaleBreakMin))
                {
                    num8 -= this.ScaleBreakHeight;
                }
                if (y != num8)
                {
                    Point[] pointArray3 = new Point[] { new Point(num4 + 5, y), new Point(num4 + 5, num8), new Point((num4 + 5) + depth, num8 - depth), new Point((num4 + 5) + depth, y - depth) };
                    g.FillPolygon(new SolidBrush(stripe.BrushColor), pointArray3);
                    g.FillRectangle(new SolidBrush(stripe.BrushColor), (int) ((num4 + 5) + depth), (int) (num8 - depth), (int) ((this.graphWidth + offsetX) - depth), (int) (y - num8));
                    this.xmlChart.AddBar(num8 - y, this.graphWidth - depth, (float) ((num4 + 5) + depth), (float) (y - depth), new ColorItem(stripe.BrushColor).ToString(), stripe.BrushColor.A, 0, 0, string.Empty, string.Empty);
                    this.xmlChart.AddQuadrilater(pointArray3[0].X, pointArray3[0].Y, pointArray3[1].X, pointArray3[1].Y, pointArray3[2].X, pointArray3[2].Y, pointArray3[3].X, pointArray3[3].Y, 0, new ColorItem(stripe.BrushColor).ToString(), stripe.BrushColor.A);
                    continue;
                }
                Pen pen2 = new Pen(stripe.BrushColor, 3f);
                g.DrawLine(pen2, new Point(num4 + 5, num8), new Point((num4 + 5) + depth, num8 - depth));
                this.xmlChart.AddLine(num4 + 5, num8, (num4 + 5) + depth, num8 - depth, 3, new ColorItem(stripe.BrushColor).ToString(), stripe.BrushColor.A);
                g.DrawLine(pen2, new Point((num4 + 5) + depth, num8 - depth), new Point((((num4 + 5) + this.graphWidth) + offsetX) - depth, num8 - depth));
                this.xmlChart.AddLine((num4 + 5) + depth, num8 - depth, (((num4 + 5) + this.graphWidth) + offsetX) - depth, num8 - depth, 3, new ColorItem(stripe.BrushColor).ToString(), stripe.BrushColor.A);
            }
            int num9 = 0;
            int num10 = -1;
            bool flag = true;
            while (num3 < (heighest + (2 * num2)))
            {
                flag = true;
                if ((num3 > this.ScaleBreakMin) && (num3 < this.ScaleBreakMax))
                {
                    //?num3 += Math.Ceiling(decimal.op_Increment((this.ScaleBreakMax - num3) / num2)) * num2;
                    num3 += Math.Ceiling((this.ScaleBreakMax - num3) / num2) * num2;
                }
                else
                {
                    string text = Convert.ToInt32(num3).ToString();
                    decimal num11 = num3;
                    if (num3 >= this.ScaleBreakMax)
                    {
                        num11 -= this.ScaleBreakMax - this.ScaleBreakMin;
                    }
                    int num12 = (this.graphHeight - Convert.ToInt32((decimal) ((num11 / interval) * (this.graphHeight - this.ScaleBreakHeight)))) + offsetYTop;
                    if (num12 < 0)
                    {
                        num12 = 0;
                    }
                    if ((num12 < this.ScaleBreakPos) && (this.ScaleBreakMax != this.ScaleBreakMin))
                    {
                        num12 -= this.ScaleBreakHeight;
                    }
                    if (num3 == this.ScaleBreakMin)
                    {
                        flag = false;
                    }
                    SizeF ef = g.MeasureString(text, labelsFont);
                    if ((num3 <= heighest) && flag)
                    {
                        g.DrawString(text, labelsFont, brush, (float) ((2 + num4) - ef.Width), (float) ((num12 - (ef.Height / 2f)) - this.negativeOffset));
                        this.xmlChart.AddLabel(text, (2 + num4) - ef.Width, (num12 - (ef.Height / 2f)) - this.negativeOffset, 0, labelsFont, false);
                    }
                    if (this.DrawGrid || (num3 == 0M))
                    {
                        if (((num9 % 2) == 0) && (num10 > 0))
                        {
                            Point[] pointArray4 = new Point[] { new Point(num4 + 5, (num12 + 1) - this.negativeOffset), new Point(num4 + 5, (((num12 + 1) + num10) - num12) - this.negativeOffset), new Point((num4 + 5) + depth, ((((num12 + 1) + num10) - num12) - depth) - this.negativeOffset), new Point((num4 + 5) + depth, ((num12 + 1) - depth) - this.negativeOffset) };
                            g.FillPolygon(brush2, pointArray4);
                            g.FillRectangle(brush2, (int) ((num4 + 5) + depth), (int) (((num12 + 1) - depth) - this.negativeOffset), (int) ((this.graphWidth + offsetX) - depth), (int) (num10 - num12));
                            this.xmlChart.AddBar(num10 - num12, this.graphWidth - depth, (float) ((num4 + 5) + depth), (float) (((num12 + 1) - depth) - this.negativeOffset), "0", this.BackgroundDarkening, 0, 0, string.Empty, string.Empty);
                            this.xmlChart.AddQuadrilater(pointArray4[0].X, pointArray4[0].Y, pointArray4[1].X, pointArray4[1].Y, pointArray4[2].X, pointArray4[2].Y, pointArray4[3].X, pointArray4[3].Y, 0, "0", this.BackgroundDarkening);
                        }
                        g.DrawLine(pen, (int) (num4 + 5), (int) ((num12 - 1) - this.negativeOffset), (int) ((num4 + 5) + depth), (int) (((num12 - 1) - depth) - this.negativeOffset));
                        this.xmlChart.AddLine(num4 + 5, (num12 - 1) - this.negativeOffset, (num4 + 5) + depth, ((num12 - 1) - depth) - this.negativeOffset, 1, "0", 0xff);
                        g.DrawLine(pen, (int) ((num4 + 5) + depth), (int) (((num12 - 1) - depth) - this.negativeOffset), (int) (this.graphWidth + offsetX), (int) (((num12 - 1) - depth) - this.negativeOffset));
                        this.xmlChart.AddLine((num4 + 5) + depth, ((num12 - 1) - depth) - this.negativeOffset, this.graphWidth + offsetX, ((num12 - 1) - depth) - this.negativeOffset, 1, "0", 0xff);
                    }
                    num10 = num12;
                    num3 += num2;
                    num9++;
                }
            }
            if ((this.YName != string.Empty) && (this.YName != null))
            {
                labelsFont = this.AxisLabelFont;
                SizeF ef2 = g.MeasureString(this.YName, labelsFont);
                float dx = 5f;
                float dy = (offsetYTop + ef2.Width) + ((this.graphHeight - ef2.Width) / 2f);
                g.TranslateTransform(dx, dy);
                g.RotateTransform(-90f);
                g.DrawString(this.YName, labelsFont, brush, (float) 0f, (float) 0f);
                this.xmlChart.AddLabel(this.YName, dx, dy, -90, labelsFont, false);
                g.ResetTransform();
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                if (imageEncoders[i].MimeType == mimeType)
                {
                    return imageEncoders[i];
                }
            }
            return null;
        }

        private string GetFullPath(string file)
        {
            if (HttpContext.Current == null)
            {
                return (this.radacina + "/" + this.ImageFolder + "/" + file);
            }
            return HttpContext.Current.Server.MapPath(this.ImageFolder + "/" + file);
        }

        private string GetID(int i)
        {
            if ((this.IDField != null) && !(this.IDField == string.Empty))
            {
                return this.data.Rows[i][this.IDField].ToString();
            }
            return string.Empty;
        }

        private string GetNavigateUrl(string data)
        {
            if (this.chartDataControl == null)
            {
                return string.Empty;
            }
            return (this.chartDataControl as McWebChart).GetNavigateUrl(data);
        }

        private string GetToolTip(int i)
        {
            if ((this.XAxisLabels != string.Empty) && (this.XAxisLabels != null))
            {
                return string.Format(this.LabelsFormatString, this.data.Rows[i][this.XAxisLabels]);
            }
            return string.Empty;
        }

        private System.Drawing.Image LoadImage(string file)
        {
            return new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("MControl.Charts.Resources." + file));
        }

        public string saveImage(out string fullPath)
        {
            fullPath = string.Empty;
            this.deleteImages(false);
            long ticks = DateTime.Now.Ticks;
            this.rez = string.Concat(new object[] { this.ImageFolder, "/", ticks, ".jpeg" });
            string s = this.GetFullPath(ticks + ".jpeg");
            Bitmap bitmap = this.draw();
            if ((!this.windowsControl && this.flash.UseFlash) && (HttpContext.Current != null))
            {
                this.rez = string.Concat(new object[] { this.ImageFolder, "/", ticks, ".xml" });
                s = this.GetFullPath(ticks + ".xml");
                this.xmlChart.SaveChart(s);
                this.xmlChart = null;
                return this.rez;
            }
            ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
            Encoder quality = Encoder.Quality;
            EncoderParameters encoderParams = new EncoderParameters(1);
            EncoderParameter parameter = new EncoderParameter(quality, 100L);
            encoderParams.Param[0] = parameter;
            try
            {
                string directoryName = Path.GetDirectoryName(s);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Cannot save chart image in temporary folder. " + exception.Message + " Make sure that the folder exists and that it has write permissions.");
            }
            bitmap.Save(s, encoderInfo, encoderParams);
            fullPath = s;
            return this.rez;
        }
    }
}

