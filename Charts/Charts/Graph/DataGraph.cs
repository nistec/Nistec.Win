namespace MControl.Charts
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(McGraph),"Images.Graph.bmp"), Designer("MControl.Charts.Design.ControlDesignerBase")]
    public class McGraph : UserControl
    {

        #region members

        private DataTypes AxisLabelColumnType;
        private Container components = null;
        private DataTypes DataLabelColumnType;
        private System.Data.DataView dataview = null;
        private System.Data.DataTable dtSample;
        private System.Data.DataTable GraphDataTable = null;
        private System.Data.DataView GraphDataView = null;
        private ArrayList groups;
        private Color m_BackShadowColor /*m_BackShadingColor*/ = Color.Gainsboro;
        //private BackShadingOrigins m_BackShadingOrigin = BackShadingOrigins.Bottom;
        private System.Windows.Forms.BorderStyle m_BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        private ColorScheme m_ColorScheme = ColorScheme.Default;
        private GraphStyle m_GraphStyle = GraphStyle.Raised;
        private bool m_ConcatenateColumn = true;
        //private OleDbConnection m_Connection = null;
        //private string m_ConnectionString = "";
        private string m_AggregationColumn = "";
        private string m_DataGroupingColumn = "";
        //private string m_DataMember = "";
        private string m_DataNameColumn = "";
        private DataOutOfRangeBehaviors m_DataOutOfRangeBehavior = DataOutOfRangeBehaviors.ExtendRange;
        //private System.Data.DataSet m_DataSet = null;
        //private System.Data.DataTable m_DataTable = null;
        private float m_DataValueIndexInterval = 2000f;
        private System.Data.DataView m_DataView = null;
        private string m_FooterText ="";// "FooterText";
        private ColorCollection m_GraphColors;
        private GraphType m_GraphType = GraphType.Column;
        private string m_HeaderText ="";// "HeaderText";
        private float m_IndexLength = 5f;
        private LineFormat m_LineFormatAxis;//Abscissae;
        private LineFormat m_LineBorderBottom;
        private LineFormat m_LineBorderColumn;
        private LineFormat m_LineDataGroup;
        private LineFormat m_LineDataGroupIndexes;
        private LineFormat m_LineDataGroupIndexesOpposite;
        private LineFormat m_LineDataValue;
        private LineFormat m_LineDataValueIndexes;
        private LineFormat m_LineDataValueIndexesOpposite;
        private LineFormat m_LineBorderLeft;
        private LineFormat m_LineOrdinates;
        private LineFormat m_LineBorderRight;
        private LineFormat m_LineBorderTop;
        private LineFormat m_LineZero;
        private int m_LineGraphThickness = 1;
        private int m_LineGraphMarkerSize = 4;
        private LineGraphMarkerStyles m_LineGraphMarkerStyle = LineGraphMarkerStyles.Dot;
        private LineGraphXAxisTypes m_LineGraphXAxisType = LineGraphXAxisTypes.Ranges;
        private float m_MaxValue = 10000f;
        private float m_MinValue = 0f;
        private MControl.Charts.Spacings m_Spacing;
        private TextFormat m_TextColumnFormat;
        private TextFormat m_TextDataGroupAxisFormat;
        private TextFormat m_TextDataGroupIndexFormat;
        private TextFormat m_TextDataValueAxisFormat;
        private TextFormat m_TextDataValuesIndexFormat;
        private TextFormat m_TextFooterFormat;
        private TextFormat m_TextHeaderFormat;
        private ArrayList namespergroup;
        private bool SettingColorsInternally = false;
        #endregion

        #region ctor

        public McGraph()
        {
            this.InitializeComponent();
            this.BackColor = SystemColors.Window;
            this.m_GraphColors = new ColorCollection();
            this.m_GraphColors.ColorChanged += new EventHandler(this.OnColorChanged);
            this.m_Spacing = new MControl.Charts.Spacings(30f, 50f, 50f, 20f, 5f);
            this.m_Spacing.SpacingChanged += new EventHandler(this.OnSpacingChanged);
            this.m_LineBorderTop = new LineFormat(true, 1, Color.Black);
            this.m_LineBorderTop.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineBorderBottom = new LineFormat(true, 1, Color.Black);
            this.m_LineBorderBottom.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineBorderLeft = new LineFormat(true, 1, Color.Black);
            this.m_LineBorderLeft.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineBorderRight = new LineFormat(true, 1, Color.Black);
            this.m_LineBorderRight.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineDataGroup = new LineFormat(true, 1, Color.LightGray);
            this.m_LineDataGroup.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineDataValue = new LineFormat(true, 1, Color.LightGray);
            this.m_LineDataValue.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineFormatAxis = new LineFormat(false, 1, Color.Gray);
            this.m_LineFormatAxis.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineOrdinates = new LineFormat(false, 1, Color.Gray);
            this.m_LineOrdinates.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineDataGroupIndexesOpposite = new LineFormat(true, 1, Color.Black);
            this.m_LineDataGroupIndexesOpposite.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineDataGroupIndexes = new LineFormat(true, 1, Color.Black);
            this.m_LineDataGroupIndexes.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineDataValueIndexes = new LineFormat(true, 1, Color.Black);
            this.m_LineDataValueIndexes.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineDataValueIndexesOpposite = new LineFormat(true, 1, Color.Black);
            this.m_LineDataValueIndexesOpposite.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineBorderColumn = new LineFormat(true, 1, Color.Black);
            this.m_LineBorderColumn.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_LineZero = new LineFormat(true, 1, Color.Black);
            this.m_LineZero.LineChanged += new EventHandler(this.OnLineChanged);
            this.m_TextHeaderFormat = new TextFormat(Alignments.Center);
            this.m_TextHeaderFormat.TextFormatChanged += new EventHandler(this.OnTextFormatChanged);
            this.m_TextFooterFormat = new TextFormat(Alignments.Center);
            this.m_TextFooterFormat.TextFormatChanged += new EventHandler(this.OnTextFormatChanged);
            this.m_TextDataGroupAxisFormat = new TextFormat(Alignments.Center);
            this.m_TextDataGroupAxisFormat.TextFormatChanged += new EventHandler(this.OnTextFormatChanged);
            this.m_TextDataValueAxisFormat = new TextFormat(Alignments.Center);
            this.m_TextDataValueAxisFormat.TextFormatChanged += new EventHandler(this.OnTextFormatChanged);
            this.m_TextDataGroupIndexFormat = new TextFormat(Alignments.Center);
            this.m_TextDataGroupIndexFormat.TextFormatChanged += new EventHandler(this.OnTextFormatChanged);
            this.m_TextDataValuesIndexFormat = new TextFormat(Alignments.Right);
            this.m_TextDataValuesIndexFormat.TextFormatChanged += new EventHandler(this.OnTextFormatChanged);
            this.m_TextColumnFormat = new TextFormat(Alignments.Center);
            this.m_TextColumnFormat.TextFormatChanged += new EventHandler(this.OnTextFormatChanged);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.Name = "McGraph";
            base.Size = new Size(500, 300);
            base.Resize += new EventHandler(this.McGraph_Resize);
            base.Load += new EventHandler(this.McGraph_Load);
            base.Paint += new PaintEventHandler(this.McGraph_Paint);
        }

        #endregion

        #region private methods

        private void CheckDataInRange()
        {
            float num = 0f;
            float num2 = 0f;
            float num3 = 0f;
            for (int i = 0; i < this.GraphDataView.Count; i++)
            {
                num3 = Convert.ToSingle(this.GraphDataView[i]["Value"]);
                if (num3 < num)
                {
                    num = num3;
                }
                if (num3 > num2)
                {
                    num2 = num3;
                }
            }
            if ((num2 > this.m_MaxValue) || (num < this.m_MinValue))
            {
                if (this.m_DataOutOfRangeBehavior == DataOutOfRangeBehaviors.RaiseException)
                {
                    throw new Exception("Data is outside the range of MaxValue and MinValue");
                }
                if (num2 > this.m_MaxValue)
                {
                    this.m_MaxValue = num2;
                }
                if (num < this.m_MinValue)
                {
                    this.m_MinValue = num;
                }
            }
        }

        //private void ConnectionStringToDataView()
        //{
        //    OleDbConnection selectConnection = new OleDbConnection(this.m_ConnectionString);
        //    selectConnection.Open();
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM " + this.m_DataMember, selectConnection);
        //    System.Data.DataSet dataSet = new System.Data.DataSet();
        //    adapter.Fill(dataSet, this.m_DataMember);
        //    this.dataview = new System.Data.DataView(dataSet.Tables[this.m_DataMember]);
        //    selectConnection.Close();
        //}

        //private void ConnectionToDataView()
        //{
        //    ConnectionState state = this.m_Connection.State;
        //    if (state == ConnectionState.Closed)
        //    {
        //        this.m_Connection.Open();
        //    }
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM " + this.m_DataMember, this.m_Connection);
        //    System.Data.DataSet dataSet = new System.Data.DataSet();
        //    adapter.Fill(dataSet, this.m_DataMember);
        //    this.dataview = new System.Data.DataView(dataSet.Tables[this.m_DataMember]);
        //    if (state == ConnectionState.Closed)
        //    {
        //        this.m_Connection.Close();
        //    }
        //}

        private void CreateGraphDataView()
        {
            //if (((this.m_DataTable != null) && (this.m_DataGroupingColumn != "")) && ((this.m_DataNameColumn != "") && (this.m_AggregationColumn != "")))
            //{
            //    this.DataTableToDataView();
            //}
            if (((this.m_DataView != null) && (this.m_DataGroupingColumn != "")) && ((this.m_DataNameColumn != "") && (this.m_AggregationColumn != "")))
            {
                this.dataview = this.m_DataView;
            }
            //else if ((((this.m_DataSet != null) && (this.m_DataMember != "")) && ((this.m_DataGroupingColumn != "") && (this.m_DataNameColumn != ""))) && (this.m_AggregationColumn != ""))
            //{
            //    this.DataSetToDataView();
            //}
            //else if ((((this.m_Connection != null) && (this.m_DataMember != "")) && ((this.m_DataGroupingColumn != "") && (this.m_DataNameColumn != ""))) && (this.m_AggregationColumn != ""))
            //{
            //    this.ConnectionToDataView();
            //}
            //else
            //{
            //    if ((((this.m_ConnectionString == "") || (this.m_DataMember == "")) || ((this.m_DataGroupingColumn == "") || (this.m_DataNameColumn == ""))) || (this.m_AggregationColumn == ""))
            //    {
            //        throw new Exception("Data source properties not set");
            //    }
            //    this.ConnectionStringToDataView();
            //}
            if ((this.dataview != null) && (this.dataview.Count > 0))
            {
                this.SetDataTypes();
                this.groups = new ArrayList();
                this.namespergroup = new ArrayList();
                for (int i = 0; i < this.dataview.Count; i++)
                {
                    if (!this.groups.Contains(this.dataview[i][this.m_DataGroupingColumn]))
                    {
                        this.groups.Add(this.dataview[i][this.m_DataGroupingColumn]);
                    }
                    if (!this.namespergroup.Contains(this.dataview[i][this.m_DataNameColumn]))
                    {
                        this.namespergroup.Add(this.dataview[i][this.m_DataNameColumn]);
                    }
                }
                this.GraphDataTable = new System.Data.DataTable();
                this.GraphDataTable.Columns.Add("GroupNameObject", Type.GetType("System.Object"));
                this.GraphDataTable.Columns.Add("GroupNameString", Type.GetType("System.String"));
                this.GraphDataTable.Columns.Add("DataLabelObject", Type.GetType("System.Object"));
                this.GraphDataTable.Columns.Add("DataLabelString", Type.GetType("System.String"));
                this.GraphDataTable.Columns.Add("Value", Type.GetType("System.Double"));
                this.GraphDataTable.PrimaryKey = new System.Data.DataColumn[] { this.GraphDataTable.Columns["GroupNameString"], this.GraphDataTable.Columns["DataLabelString"] };
                for (int j = 0; j < this.dataview.Count; j++)
                {
                    DataRow row = this.GraphDataTable.NewRow();
                    row["GroupNameObject"] = this.dataview[j][this.m_DataGroupingColumn];
                    row["GroupNameString"] = this.GetAxisLabelAsString(this.dataview[j][this.m_DataGroupingColumn]);
                    row["DataLabelObject"] = this.dataview[j][this.m_DataNameColumn];
                    row["DataLabelString"] = this.GetDataLabelAsString(this.dataview[j][this.m_DataNameColumn]);
                    row["Value"] = this.dataview[j][this.m_AggregationColumn];
                    this.GraphDataTable.Rows.Add(row);
                }
                for (int k = 0; k < this.groups.Count; k++)
                {
                    for (int m = 0; m < this.namespergroup.Count; m++)
                    {
                        try
                        {
                            DataRow row2 = this.GraphDataTable.NewRow();
                            row2["GroupNameObject"] = this.groups[k];
                            row2["GroupNameString"] = this.GetAxisLabelAsString(this.groups[k]);
                            row2["DataLabelObject"] = this.namespergroup[m].ToString();
                            row2["DataLabelString"] = this.GetDataLabelAsString(this.namespergroup[m]);
                            row2["Value"] = 0;
                            this.GraphDataTable.Rows.Add(row2);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                this.GraphDataView = new System.Data.DataView(this.GraphDataTable, "", "GroupNameObject, DataLabelObject", DataViewRowState.CurrentRows);
            }
        }

        private void McGraph_Load(object sender, EventArgs e)
        {
        }

        private void McGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            if ((this.GraphDataView == null) && base.DesignMode)
            {
                this.GenerateSample();
                this.CreateGraphDataView();
            }
            if (this.GraphDataView != null)
            {
                Bitmap graphAsBitmap = this.GetGraphBitmap();
                graphics.DrawImage(graphAsBitmap, 0, 0);
                graphAsBitmap.Dispose();
            }
        }

        private void McGraph_Resize(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        //private void DataSetToDataView()
        //{
        //    this.dataview = new System.Data.DataView(this.m_DataSet.Tables[this.m_DataMember]);
        //}

        //private void DataTableToDataView()
        //{
        //    this.dataview = new System.Data.DataView(this.m_DataTable);
        //}
        private string GetAxisLabelAsString(object obj)
        {
            if (this.AxisLabelColumnType == DataTypes.DateTime)
            {
                DateTime time = (DateTime)obj;
                return time.ToString("MMMM");
            }
            return obj.ToString();
        }

        private string GetDataLabelAsString(object obj)
        {
            if (this.DataLabelColumnType == DataTypes.DateTime)
            {
                DateTime time = (DateTime)obj;
                return time.ToString("MMMM");
            }
            return obj.ToString();
        }

        private DataTypes GetDataType(string strType)
        {
            DataTypes notSupported = DataTypes.NotSupported;
            switch (strType)
            {
                case "System.DateTime":
                    return DataTypes.DateTime;

                case "System.Boolean":
                    return DataTypes.Boolean;

                case "System.String":
                    return DataTypes.Text;

                case "System.Byte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    return DataTypes.Integer;

                case "System.Decimal":
                case "System.Single":
                case "System.Double":
                    return DataTypes.Float;
            }
            return notSupported;
        }

        private void OnColorChanged(object sender, EventArgs e)
        {
            if (!this.SettingColorsInternally)
            {
                base.Invalidate();
            }
        }

        private void OnLineChanged(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        private void OnSpacingChanged(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        private void OnTextFormatChanged(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        private void SetDataTypes()
        {
            this.AxisLabelColumnType = this.GetDataType(this.dataview.Table.Columns[this.m_DataGroupingColumn].DataType.ToString());
            if (this.AxisLabelColumnType == DataTypes.NotSupported)
            {
                throw new Exception("AxisLabelColumn data type invalid");
            }
            this.DataLabelColumnType = this.GetDataType(this.dataview.Table.Columns[this.m_DataNameColumn].DataType.ToString());
            if (this.DataLabelColumnType == DataTypes.NotSupported)
            {
                throw new Exception("DataLabelColumn data type invalid");
            }
        }

        #endregion

        #region public methods

        public void DrawGraph(DataView view,GraphType graphType, GraphStyle graphStyle, ColorScheme scheme, string groupingColumnName,string dataColumnName,string aggregationColumn)
        {
            this.m_DataGroupingColumn = groupingColumnName;
            this.m_DataNameColumn = dataColumnName;
            this.m_AggregationColumn = aggregationColumn;
            this.GraphType = graphType;
            this.GraphStyle = graphStyle;
            this.ColorScheme = scheme;
            this.DataView = view;
            DrawGraph();
        }

        public void DrawGraph(DataView view, string groupingColumnName, string dataColumnName, string aggregationColumn)
        {
            this.m_DataGroupingColumn = groupingColumnName;
            this.m_DataNameColumn = dataColumnName;
            this.m_AggregationColumn = aggregationColumn;
            this.DataView = view;
            DrawGraph();
        }

        public void DrawGraph(DataView view, GraphType graphType, GraphStyle graphStyle, ColorScheme scheme)
        {
            this.GraphType = graphType;
            this.GraphStyle = graphStyle;
            this.ColorScheme = scheme;
            this.DataView = view;
            DrawGraph();
        }

        public void DrawGraph()
        {
            this.CreateGraphDataView();
            if ((this.dataview != null) && (this.dataview.Count > 0))
            {
                this.CheckDataInRange();
                base.Invalidate();
            }
        }

        #endregion

        #region Sample

        private void GenerateSample()
        {
            this.dtSample = new System.Data.DataTable();
            this.dtSample.Columns.Add("Department", typeof(string));
            this.dtSample.Columns.Add("Month", typeof(string));
            this.dtSample.Columns.Add("Sales", typeof(float));
            DataRow row = this.dtSample.NewRow();
            row[0] = "Home";
            row[1] = "Q1";
            row[2] = (int) (this.m_MaxValue * 0.3174f);
            this.dtSample.Rows.Add(row);
            DataRow row2 = this.dtSample.NewRow();
            row2[0] = "Business";
            row2[1] = "Q1";
            row2[2] = (int) (this.m_MaxValue * 0.8006f);
            this.dtSample.Rows.Add(row2);
            DataRow row3 = this.dtSample.NewRow();
            row3[0] = "Cars";
            row3[1] = "Q1";
            row3[2] = (int) (this.m_MaxValue * 0.6822f);
            this.dtSample.Rows.Add(row3);
            DataRow row4 = this.dtSample.NewRow();
            row4[0] = "Internet";
            row4[1] = "Q1";
            row4[2] = (int) (this.m_MaxValue * 0.5484f);
            this.dtSample.Rows.Add(row4);
            DataRow row5 = this.dtSample.NewRow();
            row5[0] = "Home";
            row5[1] = "Q2";
            row5[2] = (int) (this.m_MaxValue * 0.8902f);
            this.dtSample.Rows.Add(row5);
            DataRow row6 = this.dtSample.NewRow();
            row6[0] = "Business";
            row6[1] = "Q2";
            row6[2] = (int) (this.m_MaxValue * 0.7283f);
            this.dtSample.Rows.Add(row6);
            DataRow row7 = this.dtSample.NewRow();
            row7[0] = "Cars";
            row7[1] = "Q2";
            row7[2] = (int) (this.m_MaxValue * 0.3347f);
            this.dtSample.Rows.Add(row7);
            DataRow row8 = this.dtSample.NewRow();
            row8[0] = "Internet";
            row8[1] = "Q2";
            row8[2] = (int) (this.m_MaxValue * 0.7751f);
            this.dtSample.Rows.Add(row8);
            DataRow row9 = this.dtSample.NewRow();
            row9[0] = "Home";
            row9[1] = "Q3";
            row9[2] = (int) (this.m_MaxValue * 0.6491f);
            this.dtSample.Rows.Add(row9);
            DataRow row10 = this.dtSample.NewRow();
            row10[0] = "Business";
            row10[1] = "Q3";
            row10[2] = (int) (this.m_MaxValue * 0.488f);
            this.dtSample.Rows.Add(row10);
            DataRow row11 = this.dtSample.NewRow();
            row11[0] = "Cars";
            row11[1] = "Q3";
            row11[2] = (int) (this.m_MaxValue * 0.8954f);
            this.dtSample.Rows.Add(row11);
            DataRow row12 = this.dtSample.NewRow();
            row12[0] = "Internet";
            row12[1] = "Q3";
            row12[2] = (int) (this.m_MaxValue * 0.835f);
            this.dtSample.Rows.Add(row12);
            DataRow row13 = this.dtSample.NewRow();
            row13[0] = "Home";
            row13[1] = "Q4";
            row13[2] = (int) (this.m_MaxValue * 0.4258f);
            this.dtSample.Rows.Add(row13);
            DataRow row14 = this.dtSample.NewRow();
            row14[0] = "Business";
            row14[1] = "Q4";
            row14[2] = (int) (this.m_MaxValue * 0.8841f);
            this.dtSample.Rows.Add(row14);
            DataRow row15 = this.dtSample.NewRow();
            row15[0] = "Cars";
            row15[1] = "Q4";
            row15[2] = (int) (this.m_MaxValue * 0.5724f);
            this.dtSample.Rows.Add(row15);
            DataRow row16 = this.dtSample.NewRow();
            row16[0] = "Internet";
            row16[1] = "Q4";
            row16[2] = (int) (this.m_MaxValue * 0.6583f);
            this.dtSample.Rows.Add(row16);
            this.m_DataGroupingColumn = "Month";
            this.m_DataNameColumn = "Department";
            this.m_AggregationColumn = "Sales";
            //this.DataTable = this.dtSample;
            this.DataView = this.dtSample.DefaultView;
        }
        #endregion
 
        #region Graph Bitmap options

        public Bitmap GetGraphBitmap()
        {
            return this.GetGraphBitmap(base.Width, base.Height);
        }

        public Bitmap GetGraphBitmap(int Width, int Height)
        {
            if (this.m_GraphColors.Count == 0)
            {
                this.m_GraphColors.Add(Color.Lime);
                this.m_GraphColors.Add(Color.Red);
                this.m_GraphColors.Add(Color.Yellow);
                this.m_GraphColors.Add(Color.Blue);
            }
            Bitmap graphAsBitmapColumn = null;
            if (this.m_GraphType == GraphType.Column)
            {
                graphAsBitmapColumn = this.GetGraphAsBitmapColumn(Width, Height);
            }
            if (this.m_GraphType == GraphType.Bar)
            {
                graphAsBitmapColumn = this.GetGraphAsBitmapBar(Width, Height);
            }
            if (this.m_GraphType == GraphType.Line)
            {
                graphAsBitmapColumn = this.GetGraphAsBitmapLine(Width, Height);
            }
            return graphAsBitmapColumn;
        }

        private Bitmap GetGraphAsBitmapBar(int Width, int Height)
        {
            Color[] colorArray2;
            Bitmap image = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(image);
            Pen pen = new Pen(Color.Black);
            float num = (base.Width - this.m_Spacing.Left) - this.m_Spacing.Right;
            float num2 = (base.Height - this.m_Spacing.Top) - this.m_Spacing.Bottom;
            float left = this.m_Spacing.Left;
            float num4 = base.Height - this.m_Spacing.Bottom;
            float count = this.groups.Count;
            float num6 = this.namespergroup.Count;
            RectangleF rect = new RectangleF(0f, 0f, 0f, 0f);
            float num7 = num2 / count;
            float num8 = ((num7 / num6) - this.m_Spacing.ColumnsAndBars) - (this.m_Spacing.ColumnsAndBars / num6);
            float num9 = 0f;
            float num10 = 0f;
            int num11 = 0;
            if (this.BackgroundImage != null)
            {
                TextureBrush brush = new TextureBrush(this.BackgroundImage);
                graphics.FillRectangle(brush, base.ClientRectangle);
                brush.Dispose();
            }
            //else if (((this.m_BackShadingOrigin == BackShadingOrigins.TopLeft) || (this.m_BackShadingOrigin == BackShadingOrigins.TopRight)) || (((this.m_BackShadingOrigin == BackShadingOrigins.BottomLeft) || (this.m_BackShadingOrigin == BackShadingOrigins.BottomRight)) || (this.m_BackShadingOrigin == BackShadingOrigins.Center)))
            //{
            //    colorArray2 = new Color[] { this.BackColor };
            //    Color[] colorArray = colorArray2;
            //    GraphicsPath path = new GraphicsPath();
            //    path.AddRectangle(base.ClientRectangle);
            //    PathGradientBrush brush2 = new PathGradientBrush(path);
            //    brush2.CenterColor = this.m_BackShadingColor;
            //    brush2.SurroundColors = colorArray;
            //    if (this.m_BackShadingOrigin == BackShadingOrigins.TopLeft)
            //    {
            //        brush2.CenterPoint = new PointF(0f, 0f);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.TopRight)
            //    {
            //        brush2.CenterPoint = new PointF((float) base.Width, 0f);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.BottomRight)
            //    {
            //        brush2.CenterPoint = new PointF((float) base.Width, (float) base.Height);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.BottomLeft)
            //    {
            //        brush2.CenterPoint = new PointF(0f, (float) base.Height);
            //    }
            //    graphics.FillRectangle(brush2, base.ClientRectangle);
            //    brush2.Dispose();
            //    path.Dispose();
            //}
            else
            {
                LinearGradientBrush brush3 = null;
                //if (this.m_BackShadingOrigin == BackShadingOrigins.Top)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 270f);
                //}
                //else if (this.m_BackShadingOrigin == BackShadingOrigins.Right)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 0f);
                //}
                //else if (this.m_BackShadingOrigin == BackShadingOrigins.Bottom)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 90f);
                //}
                //else if (this.m_BackShadingOrigin == BackShadingOrigins.Left)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 180f);
                //}
                brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadowColor, 90f);
                
                graphics.FillRectangle(brush3, base.ClientRectangle);
                brush3.Dispose();
            }
            num9 = left;
            num10 = num4;
            string text = "";
            int num13 = 0;
            float num14 = num9;
            if (this.m_MinValue != 0f)
            {
                num14 = ((Math.Abs(this.m_MinValue) / (this.m_MaxValue - this.m_MinValue)) * num) + this.m_Spacing.Left;
            }
            else
            {
                num14 = this.m_Spacing.Left;
            }
            if (this.m_LineDataValue.Visible)
            {
                float num15 = (this.m_DataValueIndexInterval / (this.m_MaxValue - this.m_MinValue)) * num;
                pen.Color = this.m_LineDataValue.Color;
                pen.Width = this.m_LineDataValue.Thickness;
                float num16 = num14;
                while (num16 <= (base.Width - this.m_Spacing.Right))
                {
                    graphics.DrawLine(pen, num16, num4, num16, this.m_Spacing.Top);
                    num16 += num15;
                }
                if (num16 != (base.Width - this.m_Spacing.Right))
                {
                    graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, num4);
                }
                num16 = num14;
                while (num16 >= this.m_Spacing.Left)
                {
                    graphics.DrawLine(pen, num16, num4, num16, this.m_Spacing.Top);
                    num16 -= num15;
                }
                if (num16 != this.m_Spacing.Left)
                {
                    graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, num4);
                }
            }
            if (this.m_LineZero.Visible)
            {
                pen.Color = this.m_LineZero.Color;
                pen.Width = this.m_LineZero.Thickness;
                graphics.DrawLine(pen, num14, this.m_Spacing.Top, num14, num4);
            }
            for (num11 = 0; num11 < this.GraphDataView.Count; num11++)
            {
                float num17 = Convert.ToSingle(this.GraphDataView[num11]["Value"]);
                if (text != this.GraphDataView[num11]["GroupNameString"].ToString())
                {
                    if (this.m_LineDataGroupIndexes.Visible)
                    {
                        pen.Width = this.m_LineDataGroupIndexes.Thickness;
                        pen.Color = this.m_LineDataGroupIndexes.Color;
                        graphics.DrawLine(pen, num9, num10, num9 - this.m_IndexLength, num10);
                    }
                    if (this.m_LineDataGroupIndexesOpposite.Visible)
                    {
                        pen.Width = this.m_LineDataGroupIndexesOpposite.Thickness;
                        pen.Color = this.m_LineDataGroupIndexesOpposite.Color;
                        graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, num10, (base.Width - this.m_Spacing.Right) + this.m_IndexLength, num10);
                    }
                    if (this.m_LineDataGroup.Visible)
                    {
                        pen.Width = this.m_LineDataGroup.Thickness;
                        pen.Color = this.m_LineDataGroup.Color;
                        graphics.DrawLine(pen, num9, num10, base.Width - this.m_Spacing.Right, num10);
                    }
                    text = this.GraphDataView[num11]["GroupNameString"].ToString();
                    if (this.m_TextDataGroupIndexFormat.Visible)
                    {
                        float y = 0f;
                        if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Left)
                        {
                            y = num10 - graphics.MeasureString(text, this.m_TextDataGroupIndexFormat.Font).Height;
                        }
                        if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Center)
                        {
                            y = (num10 - (num7 / 2f)) - (graphics.MeasureString(text, this.m_TextDataGroupIndexFormat.Font).Height / 2f);
                        }
                        if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Right)
                        {
                            y = num10 - num7;
                        }
                        graphics.DrawString(text, this.m_TextDataGroupIndexFormat.Font, new SolidBrush(this.m_TextDataGroupIndexFormat.Color), num9 - graphics.MeasureString(text, this.m_TextDataGroupIndexFormat.Font).Width, y);
                    }
                    num10 -= this.m_Spacing.ColumnsAndBars;
                    num13 = 0;
                }
                float num12 = (num17 / (this.m_MaxValue - this.m_MinValue)) * num;
                if (num17 < 0f)
                {
                    rect.X = num14 - Math.Abs(num12);
                    rect.Y = num10 - num8;
                    rect.Width = Math.Abs(num12);
                    rect.Height = num8;
                }
                else if (num17 >= 0f)
                {
                    rect.X = num14;
                    rect.Y = num10 - num8;
                    rect.Width = num12;
                    rect.Height = num8;
                }
                if (rect.Width > 0f)
                {
                    if (this.m_GraphStyle == GraphStyle.Flat)
                    {
                        graphics.FillRectangle(new SolidBrush(this.m_GraphColors[num13]), rect);
                    }
                    else
                    {
                        LinearGradientBrush brush4 = new LinearGradientBrush(rect, this.m_GraphColors[num13], Color.White, 270f);
                        ColorBlend blend = new ColorBlend();
                        if (this.m_GraphStyle == GraphStyle.Raised)
                        {
                            colorArray2 = new Color[] { Color.DimGray, this.m_GraphColors[num13], this.m_GraphColors[num13], Color.White };
                            blend.Colors = colorArray2;
                            blend.Positions = new float[] { 0f, 0.2f, 0.8f, 1f };
                            brush4.InterpolationColors = blend;
                        }
                        else
                        {
                            colorArray2 = new Color[] { Color.DimGray, this.m_GraphColors[num13], Color.White };
                            blend.Colors = colorArray2;
                            float[] numArray = new float[3];
                            numArray[1] = 0.5f;
                            numArray[2] = 1f;
                            blend.Positions = numArray;
                            brush4.InterpolationColors = blend;
                        }
                        graphics.FillRectangle(brush4, rect);
                        brush4.Dispose();
                    }
                    if (this.m_LineBorderColumn.Visible)
                    {
                        pen.Color = this.m_LineBorderColumn.Color;
                        pen.Width = this.m_LineBorderColumn.Thickness;
                        graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                    }
                }
                if (this.m_TextColumnFormat.Visible)
                {
                    if (!this.m_ConcatenateColumn)
                    {
                        string s = this.GraphDataView[num11]["DataLabelString"].ToString();
                        string str3 = this.GraphDataView[num11]["Value"].ToString();
                        if (num17 >= 0f)
                        {
                            graphics.DrawString(s, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), rect.X, (rect.Y + (rect.Height / 2f)) - graphics.MeasureString(s, this.Font).Height);
                            graphics.DrawString(str3, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), rect.X, rect.Y + (rect.Height / 2f));
                        }
                        else
                        {
                            graphics.DrawString(s, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), (float) ((rect.X + rect.Width) - graphics.MeasureString(s, this.Font).Width), (float) ((rect.Y + (rect.Height / 2f)) - graphics.MeasureString(s, this.Font).Height));
                            graphics.DrawString(str3, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), (float) ((rect.X + rect.Width) - graphics.MeasureString(str3, this.Font).Width), (float) (rect.Y + (rect.Height / 2f)));
                        }
                    }
                    else
                    {
                        string str4 = this.GraphDataView[num11]["DataLabelString"].ToString() + " " + this.GraphDataView[num11]["Value"].ToString();
                        if (num17 >= 0f)
                        {
                            graphics.DrawString(str4, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), rect.X, (rect.Y + (rect.Height / 2f)) - (graphics.MeasureString(str4, this.Font).Height / 2f));
                        }
                        else
                        {
                            graphics.DrawString(str4, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), (float) ((rect.X + rect.Width) - graphics.MeasureString(str4, this.Font).Width), (float) ((rect.Y + (rect.Height / 2f)) - (graphics.MeasureString(str4, this.Font).Height / 2f)));
                        }
                    }
                }
                num10 -= this.m_Spacing.ColumnsAndBars + num8;
                if (num13 < (this.m_GraphColors.Count - 1))
                {
                    num13++;
                }
                else
                {
                    num13 = 0;
                }
            }
            if (this.m_LineDataGroup.Visible)
            {
                pen.Width = this.m_LineDataGroup.Thickness;
                pen.Color = this.m_LineDataGroup.Color;
                graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, this.m_Spacing.Top);
            }
            if (this.m_LineDataGroupIndexes.Visible)
            {
                pen.Width = this.m_LineDataGroupIndexes.Thickness;
                pen.Color = this.m_LineDataGroupIndexes.Color;
                graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, this.m_Spacing.Left - this.m_IndexLength, this.m_Spacing.Top);
            }
            if (this.m_LineDataGroupIndexesOpposite.Visible)
            {
                pen.Width = this.m_LineDataGroupIndexesOpposite.Thickness;
                pen.Color = this.m_LineDataGroupIndexesOpposite.Color;
                graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, (base.Width - this.m_Spacing.Right) + this.m_IndexLength, this.m_Spacing.Top);
            }
            if (this.m_LineDataValueIndexes.Visible || this.m_TextDataValuesIndexFormat.Visible)
            {
                float num19 = (this.m_DataValueIndexInterval / (this.m_MaxValue - this.m_MinValue)) * num;
                pen.Color = this.m_LineDataValueIndexes.Color;
                pen.Width = this.m_LineDataValueIndexes.Thickness;
                float num21 = 0f;
                float num20 = num14;
                while (num20 <= (base.Width - this.m_Spacing.Right))
                {
                    if (this.m_LineDataValueIndexes.Visible)
                    {
                        graphics.DrawLine(pen, num20, base.Height - this.m_Spacing.Bottom, num20, (base.Height - this.m_Spacing.Bottom) + this.m_IndexLength);
                    }
                    if (this.m_TextDataValuesIndexFormat.Visible)
                    {
                        float x = 0f;
                        if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                        {
                            x = num20 - graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width;
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                        {
                            x = num20 - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                        {
                            x = num20;
                        }
                        graphics.DrawString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), x, num4 + this.m_IndexLength);
                    }
                    num21 += this.m_DataValueIndexInterval;
                    num20 += num19;
                }
                if (this.m_LineDataValueIndexes.Visible && (num20 != (base.Width - this.m_Spacing.Right)))
                {
                    graphics.DrawLine(pen, (float) (base.Width - this.m_Spacing.Right), (float) (base.Height - this.m_Spacing.Bottom), (float) (base.Width - this.m_Spacing.Right), (float) ((base.Height - this.m_Spacing.Bottom) + this.m_IndexLength));
                }
                if (this.m_TextDataValuesIndexFormat.Visible && (num20 != (base.Width - this.m_Spacing.Right)))
                {
                    float num23 = 0f;
                    if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                    {
                        num23 = (base.Width - this.m_Spacing.Right) - graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width;
                    }
                    else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                    {
                        num23 = (base.Width - this.m_Spacing.Right) - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                    }
                    else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                    {
                        num23 = base.Width - this.m_Spacing.Right;
                    }
                    graphics.DrawString(this.m_MaxValue.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num23, num4 + this.m_IndexLength);
                }
                if (this.m_MinValue < 0f)
                {
                    num21 = 0f - this.m_DataValueIndexInterval;
                    num20 = num14 - num19;
                    while (num20 >= this.m_Spacing.Left)
                    {
                        if (this.m_LineDataValueIndexes.Visible)
                        {
                            graphics.DrawLine(pen, num20, base.Height - this.m_Spacing.Bottom, num20, (base.Height - this.m_Spacing.Bottom) + this.m_IndexLength);
                        }
                        if (this.m_TextDataValuesIndexFormat.Visible)
                        {
                            float num24 = 0f;
                            if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                            {
                                num24 = num20 - graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width;
                            }
                            else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                            {
                                num24 = num20 - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                            }
                            else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                            {
                                num24 = num20;
                            }
                            graphics.DrawString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num24, num4 + this.m_IndexLength);
                        }
                        num21 -= this.m_DataValueIndexInterval;
                        num20 -= num19;
                    }
                    if (this.m_LineDataValueIndexes.Visible && (num20 != this.m_Spacing.Left))
                    {
                        graphics.DrawLine(pen, this.m_Spacing.Left, base.Height - this.m_Spacing.Bottom, this.m_Spacing.Left, (base.Height - this.m_Spacing.Bottom) + this.m_IndexLength);
                    }
                    if (this.m_TextDataValuesIndexFormat.Visible && (num20 != this.m_Spacing.Left))
                    {
                        float num25 = 0f;
                        if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                        {
                            num25 = this.m_Spacing.Left - graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width;
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                        {
                            num25 = this.m_Spacing.Left - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                        {
                            num25 = this.m_Spacing.Left;
                        }
                        graphics.DrawString(this.m_MinValue.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num25, num4 + this.m_IndexLength);
                    }
                }
            }
            if (this.m_LineDataValueIndexesOpposite.Visible)
            {
                float num26 = (this.m_DataValueIndexInterval / (this.m_MaxValue - this.m_MinValue)) * num;
                pen.Color = this.m_LineDataValueIndexesOpposite.Color;
                pen.Width = this.m_LineDataValueIndexesOpposite.Thickness;
                float num28 = 0f;
                float num27 = num14;
                while (num27 <= (base.Width - this.m_Spacing.Right))
                {
                    graphics.DrawLine(pen, num27, this.m_Spacing.Top, num27, this.m_Spacing.Top - this.m_IndexLength);
                    num28 += this.m_DataValueIndexInterval;
                    num27 += num26;
                }
                if (num27 != (base.Width - this.m_Spacing.Right))
                {
                    graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, this.m_Spacing.Top - this.m_IndexLength);
                }
                num28 = 0f;
                num27 = num14;
                while (num27 >= this.m_Spacing.Left)
                {
                    graphics.DrawLine(pen, num27, this.m_Spacing.Top, num27, this.m_Spacing.Top - this.m_IndexLength);
                    num28 -= this.m_DataValueIndexInterval;
                    num27 -= num26;
                }
                if (num27 != this.m_Spacing.Left)
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, this.m_Spacing.Left, this.m_Spacing.Top - this.m_IndexLength);
                }
            }
            if (this.m_LineBorderTop.Visible)
            {
                pen.Color = this.m_LineBorderTop.Color;
                pen.Width = this.m_LineBorderTop.Thickness;
                graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, this.m_Spacing.Top);
            }
            if (this.m_LineBorderLeft.Visible)
            {
                pen.Color = this.m_LineBorderLeft.Color;
                pen.Width = this.m_LineBorderLeft.Thickness;
                graphics.DrawLine(pen, left, this.m_Spacing.Top, left, num4);
            }
            if (this.m_LineBorderBottom.Visible)
            {
                pen.Color = this.m_LineBorderBottom.Color;
                pen.Width = this.m_LineBorderBottom.Thickness;
                graphics.DrawLine(pen, this.m_Spacing.Left, num4, base.Width - this.m_Spacing.Right, num4);
            }
            if (this.m_LineBorderRight.Visible)
            {
                pen.Color = this.m_LineBorderRight.Color;
                pen.Width = this.m_LineBorderRight.Thickness;
                graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, num4);
            }
            if ((this.m_HeaderText != "") && this.m_TextHeaderFormat.Visible)
            {
                float num29 = 0f;
                if (this.m_TextHeaderFormat.Alignment == Alignments.Left)
                {
                    num29 = this.m_Spacing.Left;
                }
                else if (this.m_TextHeaderFormat.Alignment == Alignments.Center)
                {
                    num29 = (base.Width / 2) - (graphics.MeasureString(this.m_HeaderText, this.m_TextHeaderFormat.Font).Width / 2f);
                }
                else if (this.m_TextHeaderFormat.Alignment == Alignments.Right)
                {
                    num29 = (base.Width - graphics.MeasureString(this.m_HeaderText, this.m_TextHeaderFormat.Font).Width) - this.m_Spacing.Right;
                }
                graphics.DrawString(this.m_HeaderText, this.m_TextHeaderFormat.Font, new SolidBrush(this.m_TextHeaderFormat.Color), num29, (this.m_Spacing.Top / 2f) - (graphics.MeasureString(this.m_HeaderText, this.m_TextHeaderFormat.Font).Height / 2f));
            }
            if ((this.m_FooterText != "") && this.m_TextFooterFormat.Visible)
            {
                float num30 = 0f;
                if (this.m_TextFooterFormat.Alignment == Alignments.Left)
                {
                    num30 = this.m_Spacing.Left;
                }
                else if (this.m_TextFooterFormat.Alignment == Alignments.Center)
                {
                    num30 = (base.Width / 2) - (graphics.MeasureString(this.m_FooterText, this.m_TextFooterFormat.Font).Width / 2f);
                }
                else if (this.m_TextFooterFormat.Alignment == Alignments.Right)
                {
                    num30 = (base.Width - graphics.MeasureString(this.m_FooterText, this.m_TextFooterFormat.Font).Width) - this.m_Spacing.Right;
                }
                graphics.DrawString(this.m_FooterText, this.m_TextFooterFormat.Font, new SolidBrush(this.m_TextFooterFormat.Color), num30, (base.Height - graphics.MeasureString(this.m_FooterText, this.m_TextFooterFormat.Font).Height) - 4f);
            }
            if (this.m_TextDataGroupAxisFormat.Visible)
            {
                float bottom = 0f;
                if (this.m_TextDataGroupAxisFormat.Alignment == Alignments.Left)
                {
                    bottom = this.m_Spacing.Bottom;
                }
                else if (this.m_TextDataGroupAxisFormat.Alignment == Alignments.Center)
                {
                    bottom = ((num2 / 2f) + this.m_Spacing.Bottom) - (graphics.MeasureString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font).Width / 2f);
                }
                else if (this.m_TextDataGroupAxisFormat.Alignment == Alignments.Right)
                {
                    bottom = (this.m_Spacing.Bottom + num2) - graphics.MeasureString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font).Width;
                }
                graphics.TranslateTransform(0f, (float) base.Height);
                graphics.RotateTransform(270f);
                num9 = ((num2 / 2f) + this.m_Spacing.Bottom) - (graphics.MeasureString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font).Width / 2f);
                num10 = 0f;
                graphics.DrawString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font, new SolidBrush(this.m_TextDataGroupAxisFormat.Color), bottom, num10 + 4f);
                graphics.ResetTransform();
            }
            if (this.m_TextDataValueAxisFormat.Visible)
            {
                float num32 = 0f;
                if (this.m_TextDataValueAxisFormat.Alignment == Alignments.Left)
                {
                    num32 = this.m_Spacing.Left;
                }
                else if (this.m_TextDataValueAxisFormat.Alignment == Alignments.Center)
                {
                    num32 = ((num / 2f) + this.m_Spacing.Left) - (graphics.MeasureString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font).Width / 2f);
                }
                else if (this.m_TextDataValueAxisFormat.Alignment == Alignments.Right)
                {
                    num32 = (num + this.m_Spacing.Left) - graphics.MeasureString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font).Width;
                }
                num10 = (base.Height - (this.m_Spacing.Bottom / 2f)) - (graphics.MeasureString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font).Height / 2f);
                graphics.DrawString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font, new SolidBrush(this.m_TextDataValueAxisFormat.Color), num32, num10 - 4f);
            }
            if (this.m_BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle)
            {
                Pen pen2 = new Pen(SystemColors.WindowFrame, 1f);
                graphics.DrawLine(pen2, 0, 0, base.Width, 0);
                graphics.DrawLine(pen2, 0, 0, 0, base.Height);
                graphics.DrawLine(pen2, base.Width - 1, 0, base.Width - 1, base.Height);
                graphics.DrawLine(pen2, 0, base.Height - 1, base.Width, base.Height - 1);
                pen2.Dispose();
            }
            else if (this.m_BorderStyle == System.Windows.Forms.BorderStyle.Fixed3D)
            {
                Pen pen3 = new Pen(SystemColors.ControlDark, 1f);
                graphics.DrawLine(pen3, 0, 0, base.Width, 0);
                graphics.DrawLine(pen3, 0, 0, 0, base.Height);
                pen3.Color = SystemColors.WindowFrame;
                graphics.DrawLine(pen3, 1, 1, base.Width, 1);
                graphics.DrawLine(pen3, 1, 1, 1, base.Height);
                pen3.Color = SystemColors.ControlLight;
                graphics.DrawLine(pen3, base.Width - 2, 1, base.Width - 2, base.Height);
                graphics.DrawLine(pen3, 1, base.Height - 2, base.Width, base.Height - 2);
                pen3.Color = SystemColors.ControlLightLight;
                graphics.DrawLine(pen3, 0, base.Height - 1, base.Width, base.Height - 1);
                graphics.DrawLine(pen3, base.Width - 1, 0, base.Width - 1, base.Height);
                pen3.Dispose();
            }
            pen.Dispose();
            graphics.Dispose();
            return image;
        }

        private Bitmap GetGraphAsBitmapColumn(int Width, int Height)
        {
            Color[] colorArray2;
            Bitmap image = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(image);
            Pen pen = new Pen(Color.Black);
            float num = (base.Width - this.m_Spacing.Left) - this.m_Spacing.Right;
            float num2 = (base.Height - this.m_Spacing.Top) - this.m_Spacing.Bottom;
            float left = this.m_Spacing.Left;
            float num4 = base.Height - this.m_Spacing.Bottom;
            float count = this.groups.Count;
            float num6 = this.namespergroup.Count;
            RectangleF rect = new RectangleF(0f, 0f, 0f, 0f);
            float num7 = num / count;
            float num8 = ((num7 / num6) - this.m_Spacing.ColumnsAndBars) - (this.m_Spacing.ColumnsAndBars / num6);
            float num9 = 0f;
            float num10 = 0f;
            int num11 = 0;
            if (this.BackgroundImage != null)
            {
                TextureBrush brush = new TextureBrush(this.BackgroundImage);
                graphics.FillRectangle(brush, base.ClientRectangle);
                brush.Dispose();
            }
            //else if (((this.m_BackShadingOrigin == BackShadingOrigins.TopLeft) || (this.m_BackShadingOrigin == BackShadingOrigins.TopRight)) || (((this.m_BackShadingOrigin == BackShadingOrigins.BottomLeft) || (this.m_BackShadingOrigin == BackShadingOrigins.BottomRight)) || (this.m_BackShadingOrigin == BackShadingOrigins.Center)))
            //{
            //    colorArray2 = new Color[] { this.BackColor };
            //    Color[] colorArray = colorArray2;
            //    GraphicsPath path = new GraphicsPath();
            //    path.AddRectangle(base.ClientRectangle);
            //    PathGradientBrush brush2 = new PathGradientBrush(path);
            //    brush2.CenterColor = this.m_BackShadingColor;
            //    brush2.SurroundColors = colorArray;
            //    if (this.m_BackShadingOrigin == BackShadingOrigins.TopLeft)
            //    {
            //        brush2.CenterPoint = new PointF(0f, 0f);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.TopRight)
            //    {
            //        brush2.CenterPoint = new PointF((float) base.Width, 0f);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.BottomRight)
            //    {
            //        brush2.CenterPoint = new PointF((float) base.Width, (float) base.Height);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.BottomLeft)
            //    {
            //        brush2.CenterPoint = new PointF(0f, (float) base.Height);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.Center)
            //    {
            //        brush2.CenterPoint = new PointF((float) (base.Width / 2), (float) (base.Height / 2));
            //    }
            //    graphics.FillRectangle(brush2, base.ClientRectangle);
            //    brush2.Dispose();
            //    path.Dispose();
            //}
            else
            {
                LinearGradientBrush brush3 = null;
                //if (this.m_BackShadingOrigin == BackShadingOrigins.Top)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 270f);
                //}
                //else if (this.m_BackShadingOrigin == BackShadingOrigins.Right)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 0f);
                //}
                //else if (this.m_BackShadingOrigin == BackShadingOrigins.Bottom)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 90f);
                //}
                //else if (this.m_BackShadingOrigin == BackShadingOrigins.Left)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 180f);
                //}
                brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadowColor, 90f);
                
                graphics.FillRectangle(brush3, base.ClientRectangle);
                brush3.Dispose();
            }
            num9 = left;
            num10 = num4;
            string text = "";
            int num13 = 0;
            float num14 = num10;
            if (this.m_MinValue != 0f)
            {
                num14 = ((this.m_MaxValue / (this.m_MaxValue - this.m_MinValue)) * num2) + this.m_Spacing.Top;
            }
            else
            {
                num14 = base.Height - this.m_Spacing.Bottom;
            }
            if (this.m_LineDataValue.Visible)
            {
                float num15 = (this.m_DataValueIndexInterval / (this.m_MaxValue - this.m_MinValue)) * num2;
                pen.Color = this.m_LineDataValue.Color;
                pen.Width = this.m_LineDataValue.Thickness;
                float num16 = num14;
                while (num16 >= this.m_Spacing.Top)
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, num16, base.Width - this.m_Spacing.Right, num16);
                    num16 -= num15;
                }
                if (num16 != this.m_Spacing.Top)
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, this.m_Spacing.Top);
                }
                num16 = num14;
                while (num16 <= (base.Height - this.m_Spacing.Bottom))
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, num16, base.Width - this.m_Spacing.Right, num16);
                    num16 += num15;
                }
                if (num16 != (base.Height - this.m_Spacing.Bottom))
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, base.Height - this.m_Spacing.Bottom, base.Width - this.m_Spacing.Right, base.Height - this.m_Spacing.Bottom);
                }
            }
            if (this.m_LineZero.Visible)
            {
                pen.Color = this.m_LineZero.Color;
                pen.Width = this.m_LineZero.Thickness;
                graphics.DrawLine(pen, this.m_Spacing.Left, num14, base.Width - this.m_Spacing.Right, num14);
            }
            for (num11 = 0; num11 < this.GraphDataView.Count; num11++)
            {
                float num17 = Convert.ToSingle(this.GraphDataView[num11]["Value"]);
                if (text != this.GraphDataView[num11]["GroupNameString"].ToString())
                {
                    if (this.m_LineDataGroupIndexes.Visible)
                    {
                        pen.Width = this.m_LineDataGroupIndexes.Thickness;
                        pen.Color = this.m_LineDataGroupIndexes.Color;
                        graphics.DrawLine(pen, num9, num10, num9, num10 + this.m_IndexLength);
                    }
                    if (this.m_LineDataGroupIndexesOpposite.Visible)
                    {
                        pen.Width = this.m_LineDataGroupIndexesOpposite.Thickness;
                        pen.Color = this.m_LineDataGroupIndexesOpposite.Color;
                        graphics.DrawLine(pen, num9, this.m_Spacing.Top, num9, this.m_Spacing.Top - this.m_IndexLength);
                    }
                    if (this.m_LineDataGroup.Visible)
                    {
                        pen.Width = this.m_LineDataGroup.Thickness;
                        pen.Color = this.m_LineDataGroup.Color;
                        graphics.DrawLine(pen, num9, this.m_Spacing.Top, num9, base.Height - this.m_Spacing.Bottom);
                    }
                    text = this.GraphDataView[num11]["GroupNameString"].ToString();
                    if (this.m_TextDataGroupIndexFormat.Visible)
                    {
                        float x = 0f;
                        if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Left)
                        {
                            x = num9;
                        }
                        if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Center)
                        {
                            x = (num9 + (num7 / 2f)) - (graphics.MeasureString(text, this.m_TextDataGroupIndexFormat.Font).Width / 2f);
                        }
                        if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Right)
                        {
                            x = (num9 + num7) - graphics.MeasureString(text, this.m_TextDataGroupIndexFormat.Font).Width;
                        }
                        graphics.DrawString(text, this.m_TextDataGroupIndexFormat.Font, new SolidBrush(this.m_TextDataGroupIndexFormat.Color), x, num10);
                    }
                    num9 += this.m_Spacing.ColumnsAndBars;
                    num13 = 0;
                }
                float num12 = (num17 / (this.m_MaxValue - this.m_MinValue)) * num2;
                if (Convert.ToSingle(this.GraphDataView[num11]["Value"]) >= 0f)
                {
                    rect.X = num9;
                    rect.Y = num14 - num12;
                    rect.Width = num8;
                    rect.Height = num12;
                }
                else if (Convert.ToSingle(this.GraphDataView[num11]["Value"]) < 0f)
                {
                    rect.X = num9;
                    rect.Y = num14;
                    rect.Width = num8;
                    rect.Height = Math.Abs(num12);
                }
                if (rect.Height > 0f)
                {
                    if (this.m_GraphStyle == GraphStyle.Flat)
                    {
                        graphics.FillRectangle(new SolidBrush(this.m_GraphColors[num13]), rect);
                    }
                    else
                    {
                        LinearGradientBrush brush4 = new LinearGradientBrush(rect, this.m_GraphColors[num13], Color.White, 180f);
                        ColorBlend blend = new ColorBlend();
                        if (this.m_GraphStyle == GraphStyle.Raised)
                        {
                            colorArray2 = new Color[] { Color.DimGray, this.m_GraphColors[num13], this.m_GraphColors[num13], Color.White };
                            blend.Colors = colorArray2;
                            blend.Positions = new float[] { 0f, 0.2f, 0.8f, 1f };
                            brush4.InterpolationColors = blend;
                        }
                        else
                        {
                            colorArray2 = new Color[] { Color.DimGray, this.m_GraphColors[num13], Color.White };
                            blend.Colors = colorArray2;
                            float[] numArray = new float[3];
                            numArray[1] = 0.5f;
                            numArray[2] = 1f;
                            blend.Positions = numArray;
                            brush4.InterpolationColors = blend;
                        }
                        graphics.FillRectangle(brush4, rect);
                        brush4.Dispose();
                    }
                    if (this.m_LineBorderColumn.Visible)
                    {
                        pen.Color = this.m_LineBorderColumn.Color;
                        pen.Width = this.m_LineBorderColumn.Thickness;
                        graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                    }
                }
                if (this.m_TextColumnFormat.Visible)
                {
                    graphics.TranslateTransform(0f, (float) base.Height);
                    graphics.RotateTransform(270f);
                    if (this.m_ConcatenateColumn)
                    {
                        string s = this.GraphDataView[num11]["DataLabelString"].ToString() + " " + this.GraphDataView[num11]["Value"].ToString();
                        if (num17 >= 0f)
                        {
                            graphics.DrawString(s, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), (float) (((this.m_Spacing.Bottom + num2) - num14) + this.m_Spacing.Top), (float) ((num9 + (num8 / 2f)) - (graphics.MeasureString(s, this.Font).Height / 2f)));
                        }
                        else
                        {
                            graphics.DrawString(s, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), (float) ((((this.m_Spacing.Bottom + num2) - num14) + this.m_Spacing.Top) - graphics.MeasureString(s, this.m_TextColumnFormat.Font).Width), (float) ((num9 + (num8 / 2f)) - (graphics.MeasureString(s, this.Font).Height / 2f)));
                        }
                    }
                    else
                    {
                        string str3 = this.GraphDataView[num11]["DataLabelString"].ToString();
                        string str4 = this.GraphDataView[num11]["Value"].ToString();
                        if (num17 >= 0f)
                        {
                            graphics.DrawString(str3, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), (float) (((this.m_Spacing.Bottom + num2) - num14) + this.m_Spacing.Top), (float) ((num9 + (num8 / 2f)) - graphics.MeasureString(str3, this.Font).Height));
                            graphics.DrawString(str4, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), (float) (((this.m_Spacing.Bottom + num2) - num14) + this.m_Spacing.Top), (float) (num9 + (num8 / 2f)));
                        }
                        else
                        {
                            graphics.DrawString(str3, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), (float) ((((this.m_Spacing.Bottom + num2) - num14) + this.m_Spacing.Top) - graphics.MeasureString(str3, this.m_TextColumnFormat.Font).Width), (float) ((num9 + (num8 / 2f)) - graphics.MeasureString(str3, this.Font).Height));
                            graphics.DrawString(str4, this.m_TextColumnFormat.Font, new SolidBrush(this.m_TextColumnFormat.Color), (float) ((((this.m_Spacing.Bottom + num2) - num14) + this.m_Spacing.Top) - graphics.MeasureString(str4, this.m_TextColumnFormat.Font).Width), (float) (num9 + (num8 / 2f)));
                        }
                    }
                    graphics.ResetTransform();
                }
                num9 += this.m_Spacing.ColumnsAndBars + num8;
                if (num13 < (this.m_GraphColors.Count - 1))
                {
                    num13++;
                }
                else
                {
                    num13 = 0;
                }
            }
            if (this.m_LineDataGroup.Visible)
            {
                pen.Width = this.m_LineDataGroup.Thickness;
                pen.Color = this.m_LineDataGroup.Color;
                graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, base.Height - this.m_Spacing.Bottom);
            }
            if (this.m_LineDataGroupIndexes.Visible)
            {
                pen.Width = this.m_LineDataGroupIndexes.Thickness;
                pen.Color = this.m_LineDataGroupIndexes.Color;
                graphics.DrawLine(pen, (float) (base.Width - this.m_Spacing.Right), (float) (base.Height - this.m_Spacing.Bottom), (float) (base.Width - this.m_Spacing.Right), (float) ((base.Height - this.m_Spacing.Bottom) + this.m_IndexLength));
            }
            if (this.m_LineDataGroupIndexesOpposite.Visible)
            {
                pen.Width = this.m_LineDataGroupIndexesOpposite.Thickness;
                pen.Color = this.m_LineDataGroupIndexesOpposite.Color;
                graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, this.m_Spacing.Top - this.m_IndexLength);
            }
            if (this.m_LineDataValueIndexes.Visible || this.m_TextDataValuesIndexFormat.Visible)
            {
                float num19 = (this.m_DataValueIndexInterval / (this.m_MaxValue - this.m_MinValue)) * num2;
                pen.Color = this.m_LineDataValueIndexes.Color;
                pen.Width = this.m_LineDataValueIndexes.Thickness;
                float num21 = 0f;
                float num20 = num14;
                while (num20 >= this.m_Spacing.Top)
                {
                    if (this.m_LineDataValueIndexes.Visible)
                    {
                        graphics.DrawLine(pen, this.m_Spacing.Left, num20, this.m_Spacing.Left - this.m_IndexLength, num20);
                    }
                    if (this.m_TextDataValuesIndexFormat.Visible)
                    {
                        float num22 = 0f;
                        if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                        {
                            num22 = 4f;
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                        {
                            num22 = ((this.m_Spacing.Left - this.m_IndexLength) / 2f) - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                        {
                            num22 = (this.m_Spacing.Left - graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width) - this.m_IndexLength;
                        }
                        graphics.DrawString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num22, num20 - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Height / 2f));
                    }
                    num21 += this.m_DataValueIndexInterval;
                    num20 -= num19;
                }
                if (this.m_LineDataValueIndexes.Visible && (num20 != this.m_Spacing.Top))
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, this.m_Spacing.Left - this.m_IndexLength, this.m_Spacing.Top);
                }
                if (this.m_TextDataValuesIndexFormat.Visible && (num20 != this.m_Spacing.Top))
                {
                    float num23 = 0f;
                    if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                    {
                        num23 = 4f;
                    }
                    else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                    {
                        num23 = ((this.m_Spacing.Left - this.m_IndexLength) / 2f) - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                    }
                    else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                    {
                        num23 = (this.m_Spacing.Left - graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width) - this.m_IndexLength;
                    }
                    graphics.DrawString(this.m_MaxValue.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num23, this.m_Spacing.Top - (graphics.MeasureString(this.m_MaxValue.ToString(), this.m_TextDataValuesIndexFormat.Font).Height / 2f));
                }
                if (this.m_MinValue < 0f)
                {
                    num21 = 0f - this.m_DataValueIndexInterval;
                    num20 = num14 + num19;
                    while (num20 <= (base.Height - this.m_Spacing.Bottom))
                    {
                        if (this.m_LineDataValueIndexes.Visible)
                        {
                            graphics.DrawLine(pen, this.m_Spacing.Left, num20, this.m_Spacing.Left - this.m_IndexLength, num20);
                        }
                        if (this.m_TextDataValuesIndexFormat.Visible)
                        {
                            float num24 = 0f;
                            if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                            {
                                num24 = 4f;
                            }
                            else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                            {
                                num24 = ((this.m_Spacing.Left - this.m_IndexLength) / 2f) - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                            }
                            else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                            {
                                num24 = (this.m_Spacing.Left - graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width) - this.m_IndexLength;
                            }
                            graphics.DrawString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num24, num20 - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Height / 2f));
                        }
                        num21 -= this.m_DataValueIndexInterval;
                        num20 += num19;
                    }
                    if (this.m_LineDataValueIndexes.Visible && (num20 != (base.Height - this.m_Spacing.Bottom)))
                    {
                        graphics.DrawLine(pen, this.m_Spacing.Left, base.Height - this.m_Spacing.Bottom, this.m_Spacing.Left - this.m_IndexLength, base.Height - this.m_Spacing.Bottom);
                    }
                    if (this.m_TextDataValuesIndexFormat.Visible && (num20 != (base.Height - this.m_Spacing.Bottom)))
                    {
                        float num25 = 0f;
                        if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                        {
                            num25 = 4f;
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                        {
                            num25 = ((this.m_Spacing.Left - this.m_IndexLength) / 2f) - (graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                        {
                            num25 = (this.m_Spacing.Left - graphics.MeasureString(num21.ToString(), this.m_TextDataValuesIndexFormat.Font).Width) - this.m_IndexLength;
                        }
                        graphics.DrawString(this.m_MinValue.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num25, (base.Height - this.m_Spacing.Bottom) - (graphics.MeasureString(this.m_MaxValue.ToString(), this.m_TextDataValuesIndexFormat.Font).Height / 2f));
                    }
                }
            }
            if (this.m_LineDataValueIndexesOpposite.Visible)
            {
                float num26 = (this.m_DataValueIndexInterval / (this.m_MaxValue - this.m_MinValue)) * num2;
                pen.Color = this.m_LineDataValueIndexesOpposite.Color;
                pen.Width = this.m_LineDataValueIndexesOpposite.Thickness;
                float num27 = num14;
                while (num27 >= this.m_Spacing.Top)
                {
                    graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, num27, (base.Width - this.m_Spacing.Right) + this.m_IndexLength, num27);
                    num27 -= num26;
                }
                if (num27 != this.m_Spacing.Top)
                {
                    graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, (base.Width - this.m_Spacing.Right) + this.m_IndexLength, this.m_Spacing.Top);
                }
                num27 = num14;
                while (num27 <= (base.Height - this.m_Spacing.Bottom))
                {
                    graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, num27, (base.Width - this.m_Spacing.Right) + this.m_IndexLength, num27);
                    num27 += num26;
                }
                if (num27 != (base.Height - this.m_Spacing.Bottom))
                {
                    graphics.DrawLine(pen, (float) (base.Width - this.m_Spacing.Right), (float) (base.Height - this.m_Spacing.Bottom), (float) ((base.Width - this.m_Spacing.Right) + this.m_IndexLength), (float) (base.Height - this.m_Spacing.Bottom));
                }
            }
            if (this.m_LineBorderTop.Visible)
            {
                pen.Color = this.m_LineBorderTop.Color;
                pen.Width = this.m_LineBorderTop.Thickness;
                graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, this.m_Spacing.Top);
            }
            if (this.m_LineBorderLeft.Visible)
            {
                pen.Color = this.m_LineBorderLeft.Color;
                pen.Width = this.m_LineBorderLeft.Thickness;
                graphics.DrawLine(pen, left, this.m_Spacing.Top, left, num4);
            }
            if (this.m_LineBorderBottom.Visible)
            {
                pen.Color = this.m_LineBorderBottom.Color;
                pen.Width = this.m_LineBorderBottom.Thickness;
                graphics.DrawLine(pen, this.m_Spacing.Left, num4, base.Width - this.m_Spacing.Right, num4);
            }
            if (this.m_LineBorderRight.Visible)
            {
                pen.Color = this.m_LineBorderRight.Color;
                pen.Width = this.m_LineBorderRight.Thickness;
                graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, num4);
            }
            if ((this.m_HeaderText != "") && this.m_TextHeaderFormat.Visible)
            {
                float num28 = 0f;
                if (this.m_TextHeaderFormat.Alignment == Alignments.Left)
                {
                    num28 = this.m_Spacing.Left;
                }
                else if (this.m_TextHeaderFormat.Alignment == Alignments.Center)
                {
                    num28 = (base.Width / 2) - (graphics.MeasureString(this.m_HeaderText, this.m_TextHeaderFormat.Font).Width / 2f);
                }
                else if (this.m_TextHeaderFormat.Alignment == Alignments.Right)
                {
                    num28 = (base.Width - graphics.MeasureString(this.m_HeaderText, this.m_TextHeaderFormat.Font).Width) - this.m_Spacing.Right;
                }
                graphics.DrawString(this.m_HeaderText, this.m_TextHeaderFormat.Font, new SolidBrush(this.m_TextHeaderFormat.Color), num28, (this.m_Spacing.Top / 2f) - (graphics.MeasureString(this.m_HeaderText, this.m_TextHeaderFormat.Font).Height / 2f));
            }
            if ((this.m_FooterText != "") && this.m_TextFooterFormat.Visible)
            {
                float num29 = 0f;
                if (this.m_TextFooterFormat.Alignment == Alignments.Left)
                {
                    num29 = this.m_Spacing.Left;
                }
                else if (this.m_TextFooterFormat.Alignment == Alignments.Center)
                {
                    num29 = (base.Width / 2) - (graphics.MeasureString(this.m_FooterText, this.m_TextFooterFormat.Font).Width / 2f);
                }
                else if (this.m_TextFooterFormat.Alignment == Alignments.Right)
                {
                    num29 = (base.Width - graphics.MeasureString(this.m_FooterText, this.m_TextFooterFormat.Font).Width) - this.m_Spacing.Right;
                }
                graphics.DrawString(this.m_FooterText, this.m_TextFooterFormat.Font, new SolidBrush(this.m_TextFooterFormat.Color), num29, (base.Height - graphics.MeasureString(this.m_FooterText, this.m_TextFooterFormat.Font).Height) - 4f);
            }
            if (this.m_TextDataGroupAxisFormat.Visible)
            {
                float num30 = 0f;
                if (this.m_TextDataGroupAxisFormat.Alignment == Alignments.Left)
                {
                    num30 = this.m_Spacing.Left;
                }
                else if (this.m_TextDataGroupAxisFormat.Alignment == Alignments.Center)
                {
                    num30 = ((num / 2f) + this.m_Spacing.Left) - (graphics.MeasureString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font).Width / 2f);
                }
                else if (this.m_TextDataGroupAxisFormat.Alignment == Alignments.Right)
                {
                    num30 = (num + this.m_Spacing.Left) - graphics.MeasureString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font).Width;
                }
                num10 = (base.Height - (this.m_Spacing.Bottom / 2f)) - (graphics.MeasureString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font).Height / 2f);
                graphics.DrawString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font, new SolidBrush(this.m_TextDataGroupAxisFormat.Color), num30, num10 - 4f);
            }
            if (this.m_TextDataValueAxisFormat.Visible)
            {
                float bottom = 0f;
                if (this.m_TextDataValueAxisFormat.Alignment == Alignments.Left)
                {
                    bottom = this.m_Spacing.Bottom;
                }
                else if (this.m_TextDataValueAxisFormat.Alignment == Alignments.Center)
                {
                    bottom = ((num2 / 2f) + this.m_Spacing.Bottom) - (graphics.MeasureString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font).Width / 2f);
                }
                else if (this.m_TextDataValueAxisFormat.Alignment == Alignments.Right)
                {
                    bottom = (this.m_Spacing.Bottom + num2) - graphics.MeasureString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font).Width;
                }
                graphics.TranslateTransform(0f, (float) base.Height);
                graphics.RotateTransform(270f);
                num9 = ((num2 / 2f) + this.m_Spacing.Bottom) - (graphics.MeasureString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font).Width / 2f);
                num10 = 0f;
                graphics.DrawString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font, new SolidBrush(this.m_TextDataValueAxisFormat.Color), bottom, num10 + 4f);
                graphics.ResetTransform();
            }
            if (this.m_BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle)
            {
                Pen pen2 = new Pen(SystemColors.WindowFrame, 1f);
                graphics.DrawLine(pen2, 0, 0, base.Width, 0);
                graphics.DrawLine(pen2, 0, 0, 0, base.Height);
                graphics.DrawLine(pen2, base.Width - 1, 0, base.Width - 1, base.Height);
                graphics.DrawLine(pen2, 0, base.Height - 1, base.Width, base.Height - 1);
                pen2.Dispose();
            }
            else if (this.m_BorderStyle == System.Windows.Forms.BorderStyle.Fixed3D)
            {
                Pen pen3 = new Pen(SystemColors.ControlDark, 1f);
                graphics.DrawLine(pen3, 0, 0, base.Width, 0);
                graphics.DrawLine(pen3, 0, 0, 0, base.Height);
                pen3.Color = SystemColors.WindowFrame;
                graphics.DrawLine(pen3, 1, 1, base.Width, 1);
                graphics.DrawLine(pen3, 1, 1, 1, base.Height);
                pen3.Color = SystemColors.ControlLight;
                graphics.DrawLine(pen3, base.Width - 2, 1, base.Width - 2, base.Height);
                graphics.DrawLine(pen3, 1, base.Height - 2, base.Width, base.Height - 2);
                pen3.Color = SystemColors.ControlLightLight;
                graphics.DrawLine(pen3, 0, base.Height - 1, base.Width, base.Height - 1);
                graphics.DrawLine(pen3, base.Width - 1, 0, base.Width - 1, base.Height);
                pen3.Dispose();
            }
            pen.Dispose();
            graphics.Dispose();
            return image;
        }

        private Bitmap GetGraphAsBitmapLine(int Width, int Height)
        {
            Bitmap image = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pen = new Pen(Color.Black);
            float num = (base.Width - this.m_Spacing.Left) - this.m_Spacing.Right;
            float num2 = (base.Height - this.m_Spacing.Top) - this.m_Spacing.Bottom;
            float left = this.m_Spacing.Left;
            float num4 = base.Height - this.m_Spacing.Bottom;
            float num5 = 0f;
            float num6 = 0f;
            int num7 = 0;
            if (this.BackgroundImage != null)
            {
                TextureBrush brush = new TextureBrush(this.BackgroundImage);
                graphics.FillRectangle(brush, base.ClientRectangle);
                brush.Dispose();
            }
            //else if (((this.m_BackShadingOrigin == BackShadingOrigins.TopLeft) || (this.m_BackShadingOrigin == BackShadingOrigins.TopRight)) || (((this.m_BackShadingOrigin == BackShadingOrigins.BottomLeft) || (this.m_BackShadingOrigin == BackShadingOrigins.BottomRight)) || (this.m_BackShadingOrigin == BackShadingOrigins.Center)))
            //{
            //    Color[] colorArray = new Color[] { this.BackColor };
            //    GraphicsPath path = new GraphicsPath();
            //    path.AddRectangle(base.ClientRectangle);
            //    PathGradientBrush brush2 = new PathGradientBrush(path);
            //    brush2.CenterColor = this.m_BackShadingColor;
            //    brush2.SurroundColors = colorArray;
            //    if (this.m_BackShadingOrigin == BackShadingOrigins.TopLeft)
            //    {
            //        brush2.CenterPoint = new PointF(0f, 0f);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.TopRight)
            //    {
            //        brush2.CenterPoint = new PointF((float) base.Width, 0f);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.BottomRight)
            //    {
            //        brush2.CenterPoint = new PointF((float) base.Width, (float) base.Height);
            //    }
            //    else if (this.m_BackShadingOrigin == BackShadingOrigins.BottomLeft)
            //    {
            //        brush2.CenterPoint = new PointF(0f, (float) base.Height);
            //    }
            //    graphics.FillRectangle(brush2, base.ClientRectangle);
            //    brush2.Dispose();
            //    path.Dispose();
            //}
            else
            {
                LinearGradientBrush brush3 = null;
                //if (this.m_BackShadingOrigin == BackShadingOrigins.Top)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 270f);
                //}
                //else if (this.m_BackShadingOrigin == BackShadingOrigins.Right)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 0f);
                //}
                //else if (this.m_BackShadingOrigin == BackShadingOrigins.Bottom)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 90f);
                //}
                //else if (this.m_BackShadingOrigin == BackShadingOrigins.Left)
                //{
                //    brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadingColor, 180f);
                //}
                brush3 = new LinearGradientBrush(base.ClientRectangle, this.BackColor, this.m_BackShadowColor, 90f);
                
                graphics.FillRectangle(brush3, base.ClientRectangle);
                brush3.Dispose();
            }
            float num8 = num6;
            if (this.m_MinValue != 0f)
            {
                num8 = ((this.m_MaxValue / (this.m_MaxValue - this.m_MinValue)) * num2) + this.m_Spacing.Top;
            }
            else
            {
                num8 = base.Height - this.m_Spacing.Bottom;
            }
            if (this.m_LineDataValue.Visible)
            {
                float num9 = (this.m_DataValueIndexInterval / (this.m_MaxValue - this.m_MinValue)) * num2;
                pen.Color = this.m_LineDataValue.Color;
                pen.Width = this.m_LineDataValue.Thickness;
                float num10 = num8;
                while (num10 >= this.m_Spacing.Top)
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, num10, base.Width - this.m_Spacing.Right, num10);
                    num10 -= num9;
                }
                if (num10 != this.m_Spacing.Top)
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, this.m_Spacing.Top);
                }
                num10 = num8;
                while (num10 <= (base.Height - this.m_Spacing.Bottom))
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, num10, base.Width - this.m_Spacing.Right, num10);
                    num10 += num9;
                }
                if (num10 != (base.Height - this.m_Spacing.Bottom))
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, base.Height - this.m_Spacing.Bottom, base.Width - this.m_Spacing.Right, base.Height - this.m_Spacing.Bottom);
                }
            }
            if (this.m_LineZero.Visible)
            {
                pen.Color = this.m_LineZero.Color;
                pen.Width = this.m_LineZero.Thickness;
                graphics.DrawLine(pen, this.m_Spacing.Left, num8, base.Width - this.m_Spacing.Right, num8);
            }
            float count = this.groups.Count;
            float num12 = this.namespergroup.Count;
            float num13 = 0f;
            float num14 = 0f;
            float num15 = 0f;
            if (this.m_LineGraphXAxisType == LineGraphXAxisTypes.Ranges)
            {
                num15 = num / count;
            }
            else
            {
                num15 = num / (count - 1f);
            }
            float single1 = num15 / num12;
            float columnsAndBars = this.m_Spacing.ColumnsAndBars;
            float single3 = this.m_Spacing.ColumnsAndBars / num12;
            num5 = left;
            num6 = num4;
            string text = "";
            int num17 = 0;
            int index = 0;
            PointF[] tfArray = new PointF[(int) num12];
            for (num7 = 0; num7 < this.GraphDataView.Count; num7++)
            {
                if (text != this.GraphDataView[num7]["GroupNameString"].ToString())
                {
                    if (this.m_LineDataGroupIndexes.Visible)
                    {
                        pen.Width = this.m_LineDataGroupIndexes.Thickness;
                        pen.Color = this.m_LineDataGroupIndexes.Color;
                        graphics.DrawLine(pen, num5, num6, num5, num6 + this.m_IndexLength);
                    }
                    if (this.m_LineDataGroupIndexesOpposite.Visible)
                    {
                        pen.Width = this.m_LineDataGroupIndexesOpposite.Thickness;
                        pen.Color = this.m_LineDataGroupIndexesOpposite.Color;
                        graphics.DrawLine(pen, num5, this.m_Spacing.Top, num5, this.m_Spacing.Top - this.m_IndexLength);
                    }
                    if (this.m_LineDataGroup.Visible)
                    {
                        pen.Width = this.m_LineDataGroup.Thickness;
                        pen.Color = this.m_LineDataGroup.Color;
                        graphics.DrawLine(pen, num5, this.m_Spacing.Top, num5, base.Height - this.m_Spacing.Bottom);
                    }
                    text = this.GraphDataView[num7]["GroupNameString"].ToString();
                    if (this.m_TextDataGroupIndexFormat.Visible)
                    {
                        if (this.m_LineGraphXAxisType == LineGraphXAxisTypes.Ranges)
                        {
                            float x = 0f;
                            if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Left)
                            {
                                x = num5;
                            }
                            if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Center)
                            {
                                x = (num5 + (num15 / 2f)) - (graphics.MeasureString(text, this.m_TextDataGroupIndexFormat.Font).Width / 2f);
                            }
                            if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Right)
                            {
                                x = (num5 + num15) - graphics.MeasureString(text, this.m_TextDataGroupIndexFormat.Font).Width;
                            }
                            graphics.DrawString(text, this.m_TextDataGroupIndexFormat.Font, new SolidBrush(this.m_TextDataGroupIndexFormat.Color), x, num6);
                        }
                        else
                        {
                            float num20 = 0f;
                            if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Left)
                            {
                                num20 = num5 - graphics.MeasureString(text, this.m_TextDataGroupIndexFormat.Font).Width;
                            }
                            if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Center)
                            {
                                num20 = num5 - (graphics.MeasureString(text, this.m_TextDataGroupIndexFormat.Font).Width / 2f);
                            }
                            if (this.m_TextDataGroupIndexFormat.Alignment == Alignments.Right)
                            {
                                num20 = num5;
                            }
                            graphics.DrawString(text, this.m_TextDataGroupIndexFormat.Font, new SolidBrush(this.m_TextDataGroupIndexFormat.Color), num20, num6 + this.m_IndexLength);
                        }
                    }
                    num5 += num15;
                    num17 = 0;
                }
                float num16 = Math.Abs((float) ((Convert.ToSingle(this.GraphDataView[num7]["Value"]) / (this.m_MaxValue - this.m_MinValue)) * num2));
                if (this.m_LineGraphXAxisType == LineGraphXAxisTypes.Ranges)
                {
                    num13 = num5 - (num15 / 2f);
                }
                else
                {
                    num13 = num5 - num15;
                }
                if (Convert.ToSingle(this.GraphDataView[num7]["Value"]) >= 0f)
                {
                    num14 = num8 - num16;
                }
                else if (Convert.ToSingle(this.GraphDataView[num7]["Value"]) < 0f)
                {
                    num14 = num8 + num16;
                }
                if (this.m_LineFormatAxis.Visible)
                {
                    pen.Width = this.m_LineFormatAxis.Thickness;
                    pen.Color = this.m_LineFormatAxis.Color;
                    graphics.DrawLine(pen, left, num14, num13, num14);
                }
                if (this.m_LineOrdinates.Visible)
                {
                    pen.Width = this.m_LineOrdinates.Thickness;
                    pen.Color = this.m_LineOrdinates.Color;
                    graphics.DrawLine(pen, num13, num14, num13, num4);
                }
                int lineGraphMarkerSize = this.m_LineGraphMarkerSize;
                float num22 = ((float) this.m_LineGraphMarkerSize) / 2f;
                if (this.m_LineGraphMarkerStyle == LineGraphMarkerStyles.Dot)
                {
                    graphics.FillEllipse(new SolidBrush(this.m_GraphColors[num17]), num13 - num22, num14 - num22, (float) lineGraphMarkerSize, (float) lineGraphMarkerSize);
                }
                else
                {
                    pen.Width = 1f;
                    pen.Color = this.m_GraphColors[num17];
                    graphics.DrawLine(pen, (float) (num13 - num22), (float) (num14 - num22), (float) (num13 + num22), (float) (num14 + num22));
                    graphics.DrawLine(pen, (float) (num13 + num22), (float) (num14 - num22), (float) (num13 - num22), (float) (num14 + num22));
                }
                if (this.m_LineGraphThickness > 0)
                {
                    PointF tf = tfArray[index];
                    pen.Width = this.m_LineGraphThickness;
                    pen.Color = this.m_GraphColors[num17];
                    if (num7 >= num12)
                    {
                        graphics.DrawLine(pen, tf.X, tf.Y, num13, num14);
                    }
                    tfArray[index] = new PointF(num13, num14);
                }
                if (num17 < (this.m_GraphColors.Count - 1))
                {
                    num17++;
                }
                else
                {
                    num17 = 0;
                }
                if (index == (num12 - 1f))
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            if (this.m_LineDataGroup.Visible)
            {
                pen.Width = this.m_LineDataGroup.Thickness;
                pen.Color = this.m_LineDataGroup.Color;
                graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, base.Height - this.m_Spacing.Bottom);
            }
            if (this.m_LineDataGroupIndexes.Visible)
            {
                pen.Width = this.m_LineDataGroupIndexes.Thickness;
                pen.Color = this.m_LineDataGroupIndexes.Color;
                graphics.DrawLine(pen, (float) (base.Width - this.m_Spacing.Right), (float) (base.Height - this.m_Spacing.Bottom), (float) (base.Width - this.m_Spacing.Right), (float) ((base.Height - this.m_Spacing.Bottom) + this.m_IndexLength));
            }
            if (this.m_LineDataGroupIndexesOpposite.Visible)
            {
                pen.Width = this.m_LineDataGroupIndexesOpposite.Thickness;
                pen.Color = this.m_LineDataGroupIndexesOpposite.Color;
                graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, this.m_Spacing.Top - this.m_IndexLength);
            }
            if (this.m_LineDataValueIndexes.Visible || this.m_TextDataValuesIndexFormat.Visible)
            {
                float num23 = (this.m_DataValueIndexInterval / (this.m_MaxValue - this.m_MinValue)) * num2;
                pen.Color = this.m_LineDataValueIndexes.Color;
                pen.Width = this.m_LineDataValueIndexes.Thickness;
                float num25 = 0f;
                float num24 = num8;
                while (num24 >= this.m_Spacing.Top)
                {
                    if (this.m_LineDataValueIndexes.Visible)
                    {
                        graphics.DrawLine(pen, this.m_Spacing.Left, num24, this.m_Spacing.Left - this.m_IndexLength, num24);
                    }
                    if (this.m_TextDataValuesIndexFormat.Visible)
                    {
                        float num26 = 0f;
                        if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                        {
                            num26 = 4f;
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                        {
                            num26 = ((this.m_Spacing.Left - this.m_IndexLength) / 2f) - (graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                        {
                            num26 = (this.m_Spacing.Left - graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Width) - this.m_IndexLength;
                        }
                        graphics.DrawString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num26, num24 - (graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Height / 2f));
                    }
                    num25 += this.m_DataValueIndexInterval;
                    num24 -= num23;
                }
                if (this.m_LineDataValueIndexes.Visible && (num24 != this.m_Spacing.Top))
                {
                    graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, this.m_Spacing.Left - this.m_IndexLength, this.m_Spacing.Top);
                }
                if (this.m_TextDataValuesIndexFormat.Visible && (num24 != this.m_Spacing.Top))
                {
                    float num27 = 0f;
                    if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                    {
                        num27 = 4f;
                    }
                    else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                    {
                        num27 = ((this.m_Spacing.Left - this.m_IndexLength) / 2f) - (graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                    }
                    else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                    {
                        num27 = (this.m_Spacing.Left - graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Width) - this.m_IndexLength;
                    }
                    graphics.DrawString(this.m_MaxValue.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num27, this.m_Spacing.Top - (graphics.MeasureString(this.m_MaxValue.ToString(), this.m_TextDataValuesIndexFormat.Font).Height / 2f));
                }
                if (this.m_MinValue < 0f)
                {
                    num25 = 0f - this.m_DataValueIndexInterval;
                    num24 = num8 + num23;
                    while (num24 <= (base.Height - this.m_Spacing.Bottom))
                    {
                        if (this.m_LineDataValueIndexes.Visible)
                        {
                            graphics.DrawLine(pen, this.m_Spacing.Left, num24, this.m_Spacing.Left - this.m_IndexLength, num24);
                        }
                        if (this.m_TextDataValuesIndexFormat.Visible)
                        {
                            float num28 = 0f;
                            if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                            {
                                num28 = 4f;
                            }
                            else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                            {
                                num28 = ((this.m_Spacing.Left - this.m_IndexLength) / 2f) - (graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                            }
                            else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                            {
                                num28 = (this.m_Spacing.Left - graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Width) - this.m_IndexLength;
                            }
                            graphics.DrawString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num28, num24 - (graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Height / 2f));
                        }
                        num25 -= this.m_DataValueIndexInterval;
                        num24 += num23;
                    }
                    if (this.m_LineDataValueIndexes.Visible && (num24 != (base.Height - this.m_Spacing.Bottom)))
                    {
                        graphics.DrawLine(pen, this.m_Spacing.Left, base.Height - this.m_Spacing.Bottom, this.m_Spacing.Left - this.m_IndexLength, base.Height - this.m_Spacing.Bottom);
                    }
                    if (this.m_TextDataValuesIndexFormat.Visible && (num24 != (base.Height - this.m_Spacing.Bottom)))
                    {
                        float num29 = 0f;
                        if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Left)
                        {
                            num29 = 4f;
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Center)
                        {
                            num29 = ((this.m_Spacing.Left - this.m_IndexLength) / 2f) - (graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Width / 2f);
                        }
                        else if (this.m_TextDataValuesIndexFormat.Alignment == Alignments.Right)
                        {
                            num29 = (this.m_Spacing.Left - graphics.MeasureString(num25.ToString(), this.m_TextDataValuesIndexFormat.Font).Width) - this.m_IndexLength;
                        }
                        graphics.DrawString(this.m_MinValue.ToString(), this.m_TextDataValuesIndexFormat.Font, new SolidBrush(this.m_TextDataValuesIndexFormat.Color), num29, (base.Height - this.m_Spacing.Bottom) - (graphics.MeasureString(this.m_MaxValue.ToString(), this.m_TextDataValuesIndexFormat.Font).Height / 2f));
                    }
                }
            }
            if (this.m_LineDataValueIndexesOpposite.Visible)
            {
                float num30 = (this.m_DataValueIndexInterval / (this.m_MaxValue - this.m_MinValue)) * num2;
                pen.Color = this.m_LineDataValueIndexesOpposite.Color;
                pen.Width = this.m_LineDataValueIndexesOpposite.Thickness;
                float num31 = num8;
                while (num31 >= this.m_Spacing.Top)
                {
                    graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, num31, (base.Width - this.m_Spacing.Right) + this.m_IndexLength, num31);
                    num31 -= num30;
                }
                if (num31 != this.m_Spacing.Top)
                {
                    graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, (base.Width - this.m_Spacing.Right) + this.m_IndexLength, this.m_Spacing.Top);
                }
                num31 = num8;
                while (num31 <= (base.Height - this.m_Spacing.Bottom))
                {
                    graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, num31, (base.Width - this.m_Spacing.Right) + this.m_IndexLength, num31);
                    num31 += num30;
                }
                if (num31 != (base.Height - this.m_Spacing.Bottom))
                {
                    graphics.DrawLine(pen, (float) (base.Width - this.m_Spacing.Right), (float) (base.Height - this.m_Spacing.Bottom), (float) ((base.Width - this.m_Spacing.Right) + this.m_IndexLength), (float) (base.Height - this.m_Spacing.Bottom));
                }
            }
            if (this.m_LineBorderTop.Visible)
            {
                pen.Color = this.m_LineBorderTop.Color;
                pen.Width = this.m_LineBorderTop.Thickness;
                graphics.DrawLine(pen, this.m_Spacing.Left, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, this.m_Spacing.Top);
            }
            if (this.m_LineBorderLeft.Visible)
            {
                pen.Color = this.m_LineBorderLeft.Color;
                pen.Width = this.m_LineBorderLeft.Thickness;
                graphics.DrawLine(pen, left, this.m_Spacing.Top, left, num4);
            }
            if (this.m_LineBorderBottom.Visible)
            {
                pen.Color = this.m_LineBorderBottom.Color;
                pen.Width = this.m_LineBorderBottom.Thickness;
                graphics.DrawLine(pen, this.m_Spacing.Left, num4, base.Width - this.m_Spacing.Right, num4);
            }
            if (this.m_LineBorderRight.Visible)
            {
                pen.Color = this.m_LineBorderRight.Color;
                pen.Width = this.m_LineBorderRight.Thickness;
                graphics.DrawLine(pen, base.Width - this.m_Spacing.Right, this.m_Spacing.Top, base.Width - this.m_Spacing.Right, num4);
            }
            if ((this.m_HeaderText != "") && this.m_TextHeaderFormat.Visible)
            {
                float num32 = 0f;
                if (this.m_TextHeaderFormat.Alignment == Alignments.Left)
                {
                    num32 = this.m_Spacing.Left;
                }
                else if (this.m_TextHeaderFormat.Alignment == Alignments.Center)
                {
                    num32 = (base.Width / 2) - (graphics.MeasureString(this.m_HeaderText, this.m_TextHeaderFormat.Font).Width / 2f);
                }
                else if (this.m_TextHeaderFormat.Alignment == Alignments.Right)
                {
                    num32 = (base.Width - graphics.MeasureString(this.m_HeaderText, this.m_TextHeaderFormat.Font).Width) - this.m_Spacing.Right;
                }
                graphics.DrawString(this.m_HeaderText, this.m_TextHeaderFormat.Font, new SolidBrush(this.m_TextHeaderFormat.Color), num32, (this.m_Spacing.Top / 2f) - (graphics.MeasureString(this.m_HeaderText, this.m_TextHeaderFormat.Font).Height / 2f));
            }
            if ((this.m_FooterText != "") && this.m_TextFooterFormat.Visible)
            {
                float num33 = 0f;
                if (this.m_TextFooterFormat.Alignment == Alignments.Left)
                {
                    num33 = this.m_Spacing.Left;
                }
                else if (this.m_TextFooterFormat.Alignment == Alignments.Center)
                {
                    num33 = (base.Width / 2) - (graphics.MeasureString(this.m_FooterText, this.m_TextFooterFormat.Font).Width / 2f);
                }
                else if (this.m_TextFooterFormat.Alignment == Alignments.Right)
                {
                    num33 = (base.Width - graphics.MeasureString(this.m_FooterText, this.m_TextFooterFormat.Font).Width) - this.m_Spacing.Right;
                }
                graphics.DrawString(this.m_FooterText, this.m_TextFooterFormat.Font, new SolidBrush(this.m_TextFooterFormat.Color), num33, (base.Height - graphics.MeasureString(this.m_FooterText, this.m_TextFooterFormat.Font).Height) - 4f);
            }
            if (this.m_TextDataGroupAxisFormat.Visible)
            {
                float num34 = 0f;
                if (this.m_TextDataGroupAxisFormat.Alignment == Alignments.Left)
                {
                    num34 = this.m_Spacing.Left;
                }
                else if (this.m_TextDataGroupAxisFormat.Alignment == Alignments.Center)
                {
                    num34 = ((num / 2f) + this.m_Spacing.Left) - (graphics.MeasureString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font).Width / 2f);
                }
                else if (this.m_TextDataGroupAxisFormat.Alignment == Alignments.Right)
                {
                    num34 = (num + this.m_Spacing.Left) - graphics.MeasureString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font).Width;
                }
                num6 = (base.Height - (this.m_Spacing.Bottom / 2f)) - (graphics.MeasureString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font).Height / 2f);
                graphics.DrawString(this.m_DataGroupingColumn, this.m_TextDataGroupAxisFormat.Font, new SolidBrush(this.m_TextDataGroupAxisFormat.Color), num34, num6 - 4f);
            }
            if (this.m_TextDataValueAxisFormat.Visible)
            {
                float bottom = 0f;
                if (this.m_TextDataValueAxisFormat.Alignment == Alignments.Left)
                {
                    bottom = this.m_Spacing.Bottom;
                }
                else if (this.m_TextDataValueAxisFormat.Alignment == Alignments.Center)
                {
                    bottom = ((num2 / 2f) + this.m_Spacing.Bottom) - (graphics.MeasureString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font).Width / 2f);
                }
                else if (this.m_TextDataValueAxisFormat.Alignment == Alignments.Right)
                {
                    bottom = (this.m_Spacing.Bottom + num2) - graphics.MeasureString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font).Width;
                }
                graphics.TranslateTransform(0f, (float) base.Height);
                graphics.RotateTransform(270f);
                num5 = ((num2 / 2f) + this.m_Spacing.Bottom) - (graphics.MeasureString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font).Width / 2f);
                num6 = 0f;
                graphics.DrawString(this.m_AggregationColumn, this.m_TextDataValueAxisFormat.Font, new SolidBrush(this.m_TextDataValueAxisFormat.Color), bottom, num6 + 4f);
                graphics.ResetTransform();
            }
            if (this.m_BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle)
            {
                Pen pen2 = new Pen(SystemColors.WindowFrame, 1f);
                graphics.DrawLine(pen2, 0, 0, base.Width, 0);
                graphics.DrawLine(pen2, 0, 0, 0, base.Height);
                graphics.DrawLine(pen2, base.Width - 1, 0, base.Width - 1, base.Height);
                graphics.DrawLine(pen2, 0, base.Height - 1, base.Width, base.Height - 1);
                pen2.Dispose();
            }
            else if (this.m_BorderStyle == System.Windows.Forms.BorderStyle.Fixed3D)
            {
                Pen pen3 = new Pen(SystemColors.ControlDark, 1f);
                graphics.DrawLine(pen3, 0, 0, base.Width, 0);
                graphics.DrawLine(pen3, 0, 0, 0, base.Height);
                pen3.Color = SystemColors.WindowFrame;
                graphics.DrawLine(pen3, 1, 1, base.Width, 1);
                graphics.DrawLine(pen3, 1, 1, 1, base.Height);
                pen3.Color = SystemColors.ControlLight;
                graphics.DrawLine(pen3, base.Width - 2, 1, base.Width - 2, base.Height);
                graphics.DrawLine(pen3, 1, base.Height - 2, base.Width, base.Height - 2);
                pen3.Color = SystemColors.ControlLightLight;
                graphics.DrawLine(pen3, 0, base.Height - 1, base.Width, base.Height - 1);
                graphics.DrawLine(pen3, base.Width - 1, 0, base.Width - 1, base.Height);
                pen3.Dispose();
            }
            pen.Dispose();
            graphics.Dispose();
            return image;
        }
        #endregion

        #region properties

        [Category("Appearance"), Description("Shadow of background Color")]
        public Color BackShadowColor//BackShadingColor
        {
            get
            {
                return this.m_BackShadowColor;
            }
            set
            {
                this.m_BackShadowColor = value;
                base.Invalidate();
            }
        }

        //[DefaultValue(5), Category("Appearance"), Description("The origin of the graph's background shading")]
        //public BackShadingOrigins BackShadingOrigin
        //{
        //    get
        //    {
        //        return this.m_BackShadingOrigin;
        //    }
        //    set
        //    {
        //        this.m_BackShadingOrigin = value;
        //        base.Invalidate();
        //    }
        //}

        [Category("Appearance"), Description("Style of the control's border"), DefaultValue(2)]
        public new System.Windows.Forms.BorderStyle BorderStyle
        {
            get
            {
                return this.m_BorderStyle;
            }
            set
            {
                if (value != this.m_BorderStyle)
                {
                    this.m_BorderStyle = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(6), Description("One of the built-in color schemes"), Category("Appearance")]
        public ColorScheme ColorScheme
        {
            get
            {
                return this.m_ColorScheme;
            }
            set
            {
                this.m_ColorScheme = value;
                this.m_GraphColors.Clear();
                this.SettingColorsInternally = true;
                switch (this.m_ColorScheme)
                {
                    case ColorScheme.Blue:
                        this.m_GraphColors.Add(Color.LightSkyBlue);
                        this.m_GraphColors.Add(Color.Blue);
                        this.m_GraphColors.Add(Color.LightCyan);
                        this.m_GraphColors.Add(Color.Navy);//.DarkBlue);
                        break;

                    case ColorScheme.Gray://.BlueGray:
                        this.m_GraphColors.Add(Color.LightSteelBlue);
                        this.m_GraphColors.Add(Color.SteelBlue);
                        this.m_GraphColors.Add(Color.LightSkyBlue);//.SlateBlue);
                        this.m_GraphColors.Add(Color.DarkSlateBlue);
                        break;

                    //case ColorScheme.Citrus:
                    //    this.m_GraphColors.Add(Color.Lime);
                    //    this.m_GraphColors.Add(Color.Orange);
                    //    this.m_GraphColors.Add(Color.Yellow);
                    //    this.m_GraphColors.Add(Color.OrangeRed);
                    //    break;

                    case ColorScheme.Brown://.Desert:
                        this.m_GraphColors.Add(Color.Khaki);
                        this.m_GraphColors.Add(Color.DarkKhaki);
                        this.m_GraphColors.Add(Color.Cornsilk);
                        this.m_GraphColors.Add(Color.DarkGoldenrod);
                        break;

                    //case ColorScheme.Green:
                    //    this.m_GraphColors.Add(Color.PaleGreen);
                    //    this.m_GraphColors.Add(Color.Green);
                    //    this.m_GraphColors.Add(Color.MediumSeaGreen);
                    //    this.m_GraphColors.Add(Color.LightSeaGreen);//.Lime);
                    //    break;

                    case ColorScheme.Silver://.Monochrome:
                        this.m_GraphColors.Add(Color.LightGray);
                        this.m_GraphColors.Add(Color.DarkGray);
                        this.m_GraphColors.Add(Color.WhiteSmoke);
                        this.m_GraphColors.Add(Color.DimGray);
                        break;

                    case ColorScheme.Default://.MultiColor:
                        this.m_GraphColors.Add(Color.Red);
                        this.m_GraphColors.Add(Color.Yellow);
                        this.m_GraphColors.Add(Color.Lime);
                        this.m_GraphColors.Add(Color.Blue);
                        break;

                    case ColorScheme.Dark://.MultiColorDark:
                        this.m_GraphColors.Add(Color.DarkBlue);
                        this.m_GraphColors.Add(Color.DarkGreen);
                        this.m_GraphColors.Add(Color.DarkRed);
                        this.m_GraphColors.Add(Color.DarkGoldenrod);//.DarkMagenta);
                        break;

                    case ColorScheme.Light://.MultiColorLight:
                        this.m_GraphColors.Add(Color.SpringGreen);
                        this.m_GraphColors.Add(Color.Yellow);
                        this.m_GraphColors.Add(Color.LightSkyBlue);
                        this.m_GraphColors.Add(Color.Violet);
                        break;

                    case ColorScheme.Pink:
                        this.m_GraphColors.Add(Color.Pink);
                        this.m_GraphColors.Add(Color.Fuchsia);
                        this.m_GraphColors.Add(Color.LavenderBlush);
                        this.m_GraphColors.Add(Color.DeepPink);
                        break;

                    //case ColorScheme.Purple:
                    //    this.m_GraphColors.Add(Color.DarkViolet);
                    //    this.m_GraphColors.Add(Color.MediumOrchid);
                    //    this.m_GraphColors.Add(Color.Plum);
                    //    this.m_GraphColors.Add(Color.Purple);
                    //    break;

                    case ColorScheme.OrangeRed://.Wine:
                        this.m_GraphColors.Add(Color.LightCoral);//.Firebrick);
                        this.m_GraphColors.Add(Color.OrangeRed);
                        this.m_GraphColors.Add(Color.MediumVioletRed);
                        this.m_GraphColors.Add(Color.DarkRed);
                        break;
                }
                this.SettingColorsInternally = false;
                base.Invalidate();
            }
        }

        [Category("Appearance"), Description("The style of columns and bars"), DefaultValue(1)]
        public GraphStyle GraphStyle
        {
            get
            {
                return this.m_GraphStyle;
            }
            set
            {
                this.m_GraphStyle = value;
                base.Invalidate();
            }
        }

        [DefaultValue(true), Description("Specifies whether the name and value text of bars or columns are concatenated"), Category("Appearance")]
        public bool ConcatenateColumn
        {
            get
            {
                return this.m_ConcatenateColumn;
            }
            set
            {
                this.m_ConcatenateColumn = value;
                base.Invalidate();
            }
        }

        //[Description("A Connection object containing a table of data to be graphed"), Category("Appearance")]
        //public OleDbConnection Connection
        //{
        //    get
        //    {
        //        return this.m_Connection;
        //    }
        //    set
        //    {
        //        this.m_ConnectionString = "";
        //        this.m_DataTable = null;
        //        this.m_DataView = null;
        //        this.m_DataSet = null;
        //        this.m_Connection = value;
        //    }
        //}

        //[Description("A connection string for the database containing a table of data to be graphed"), Category("Appearance")]
        //public string ConnectionString
        //{
        //    get
        //    {
        //        return this.m_ConnectionString;
        //    }
        //    set
        //    {
        //        this.m_Connection = null;
        //        this.m_DataTable = null;
        //        this.m_DataView = null;
        //        this.m_DataSet = null;
        //        this.m_ConnectionString = value;
        //    }
        //}

        [DefaultValue(""), Category("Appearance"), Description("The column in the data source containing data")]
        public string AggregationColumn //DataColumn
        {
            get
            {
                return this.m_AggregationColumn;
            }
            set
            {
                this.m_AggregationColumn = value;
            }
        }

        [DefaultValue(""), Category("Appearance"), Description("The column in the data source by which data is grouped")]
        public string DataGroupingColumn
        {
            get
            {
                return this.m_DataGroupingColumn;
            }
            set
            {
                this.m_DataGroupingColumn = value;
            }
        }

        //[Description("The name of the table or view containing data to be graphed"), Category("Appearance")]
        //public string DataMember
        //{
        //    get
        //    {
        //        return this.m_DataMember;
        //    }
        //    set
        //    {
        //        this.m_DataMember = value;
        //    }
        //}

        [Category("Appearance"), DefaultValue(""), Description("The column in the data source containing names of items of data")]
        public string DataNameColumn
        {
            get
            {
                return this.m_DataNameColumn;
            }
            set
            {
                this.m_DataNameColumn = value;
            }
        }

        [DefaultValue(0), Description("Action taken if a value in the data source is outside of the MinValue and MaxValue properties"), Category("Data")]
        public DataOutOfRangeBehaviors DataOutOfRangeBehavior
        {
            get
            {
                return this.m_DataOutOfRangeBehavior;
            }
            set
            {
                this.m_DataOutOfRangeBehavior = value;
            }
        }

        //[Category("Appearance"), Description("A DataSet object containing a table of data to be graphed")]
        //public System.Data.DataSet DataSet
        //{
        //    get
        //    {
        //        return this.m_DataSet;
        //    }
        //    set
        //    {
        //        this.m_ConnectionString = "";
        //        this.m_Connection = null;
        //        this.m_DataTable = null;
        //        this.m_DataView = null;
        //        this.m_DataSet = value;
        //    }
        //}

        //[Description("A DataTable object containing data to be graphed"), Category("Appearance")]
        //public System.Data.DataTable DataTable
        //{
        //    get
        //    {
        //        return this.m_DataTable;
        //    }
        //    set
        //    {
        //        this.m_ConnectionString = "";
        //        this.m_Connection = null;
        //        this.m_DataSet = null;
        //        this.m_DataView = null;
        //        this.m_DataMember = "";
        //        this.m_DataTable = value;
        //    }
        //}

        [DefaultValue("2000F"), Description("The interval between indexes on the data value axis"), Category("Appearance")]
        public float DataValueIndexInterval
        {
            get
            {
                return this.m_DataValueIndexInterval;
            }
            set
            {
                this.m_DataValueIndexInterval = value;
                base.Invalidate();
            }
        }

        [Category("Data"), Description("A DataView object containing data to be graphed")]
        public System.Data.DataView DataView
        {
            get
            {
                return this.m_DataView;
            }
            set
            {
                //this.m_ConnectionString = "";
                //this.m_Connection = null;
                //this.m_DataSet = null;
                //this.m_DataTable = null;
                //this.m_DataMember = "";
                this.m_DataView = value;
            }
        }

        [Description("The text displayed as the control's footer"), Category("Appearance"), DefaultValue("FooterText")]
        public string FooterText
        {
            get
            {
                return this.m_FooterText;
            }
            set
            {
                this.m_FooterText = value;
                base.Invalidate();
            }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance"), Description("Collection of colors used to draw columns, bars or lines")]
        [Category("Appearance"), Description("Collection of colors used to draw columns, bars or lines")]
        public ColorCollection GraphColors
        {
            get
            {
                return this.m_GraphColors;
            }
        }

        [Category("Appearance"), DefaultValue(1), Description("The type of the graph")]
        public GraphType GraphType
        {
            get
            {
                return this.m_GraphType;
            }
            set
            {
                if (value != this.m_GraphType)
                {
                    this.m_GraphType = value;
                    base.Invalidate();
                }
            }
        }

        [Category("Appearance"), Description("The text displayed as the control's header"), DefaultValue("HeaderText")]
        public string HeaderText
        {
            get
            {
                return this.m_HeaderText;
            }
            set
            {
                this.m_HeaderText = value;
                base.Invalidate();
            }
        }

        [Category("Appearance"), Description("Length of indexes"), DefaultValue(5)]
        public int IndexLength
        {
            get
            {
                return (int) this.m_IndexLength;
            }
            set
            {
                if (value >= 0)
                {
                    this.m_IndexLength = value;
                }
                else
                {
                    this.m_IndexLength = 0f;
                }
                base.Invalidate();
            }
        }

        [Category("Appearance"), Description("The highest value the control can display"), DefaultValue((float)10000f)]
        public float MaxValue
        {
            get
            {
                return this.m_MaxValue;
            }
            set
            {
                this.m_MaxValue = value;
                if ((this.GraphDataView != null) && base.DesignMode)
                {
                    this.GraphDataView = null;
                }
                base.Invalidate();
            }
        }

        [DefaultValue((float)0f), Category("Appearance"), Description("The lowest value the control can display")]
        public float MinValue
        {
            get
            {
                return this.m_MinValue;
            }
            set
            {
                this.m_MinValue = value;
                base.Invalidate();
            }
        }

        [Description("Spacings of elements of the control"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance")]
        public MControl.Charts.Spacings Spacings
        {
            get
            {
                return this.m_Spacing;
            }
            set
            {
                this.m_Spacing = value;
            }
        }

        #endregion

        #region Line format

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("The format of the lines from the value axis to points on a line graph"), Category("LineFormat")]
        internal LineFormat LineFormatAxis//Abscissae
        {
            get
            {
                return this.m_LineFormatAxis;
            }
        }

        [Description("The format of the line bordering the bottom of the graph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("LineFormat")]
        internal LineFormat LineBorderBottom
        {
            get
            {
                return this.m_LineBorderBottom;
            }
        }

        [Category("LineFormat"), Description("The format of column or bar borders"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        internal LineFormat LineBorderColumn
        {
            get
            {
                return this.m_LineBorderColumn;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("LineFormat"), Description("The format of the lines dividing groups of data")]
        internal LineFormat LineDataGroup
        {
            get
            {
                return this.m_LineDataGroup;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("LineFormat"), Description("The format of data name index marks")]
        internal LineFormat LineDataGroupIndexes
        {
            get
            {
                return this.m_LineDataGroupIndexes;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("LineFormat"), Description("The format of data name index marks on the opposite axis")]
        internal LineFormat LineDataGroupIndexesOpposite
        {
            get
            {
                return this.m_LineDataGroupIndexesOpposite;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("The format of the lines dividing data values"), Category("LineFormat")]
        internal LineFormat LineDataValue
        {
            get
            {
                return this.m_LineDataValue;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("LineFormat"), Description("The format of data value index marks")]
        internal LineFormat LineDataValueIndexes
        {
            get
            {
                return this.m_LineDataValueIndexes;
            }
        }

        [Category("LineFormat"), Description("The format of data value index marks on the opposite axis"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        internal LineFormat LineDataValueIndexesOpposite
        {
            get
            {
                return this.m_LineDataValueIndexesOpposite;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("LineFormat"), Description("The format of the line bordering the left of the graph")]
        internal LineFormat LineBorderLeft
        {
            get
            {
                return this.m_LineBorderLeft;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("The format of the lines from the data name axis to points on a line graph"), Category("LineFormat")]
        internal LineFormat LineOrdinates
        {
            get
            {
                return this.m_LineOrdinates;
            }
        }

        [Category("LineFormat"), Description("The format of the line bordering the right of the graph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        internal LineFormat LineBorderRight
        {
            get
            {
                return this.m_LineBorderRight;
            }
        }

        [Category("LineFormat"), Description("The format of the line bordering the top of the graph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        internal LineFormat LineBorderTop
        {
            get
            {
                return this.m_LineBorderTop;
            }
        }

        [Description("The format of the zero line"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("LineFormat")]
        internal LineFormat LineZero
        {
            get
            {
                return this.m_LineZero;
            }
        }

        [DefaultValue(1), Category("Appearance"), Description("The thickness of line graph lines")]
        internal int LineGraphThickness
        {
            get
            {
                return this.m_LineGraphThickness;
            }
            set
            {
                if (value != this.m_LineGraphThickness)
                {
                    if (value >= 0)
                    {
                        this.m_LineGraphThickness = value;
                    }
                    else
                    {
                        this.m_LineGraphThickness = 0;
                    }
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(4), Category("Appearance"), Description("The size of marker points in a line graph")]
        internal int LineGraphMarkerSize
        {
            get
            {
                return this.m_LineGraphMarkerSize;
            }
            set
            {
                if (value != this.m_LineGraphMarkerSize)
                {
                    if (value >= 0)
                    {
                        this.m_LineGraphMarkerSize = value;
                    }
                    else
                    {
                        this.m_LineGraphMarkerSize = 0;
                    }
                    base.Invalidate();
                }
            }
        }

        [Category("Appearance"), Description("The appearance of marker points in a line graph"), DefaultValue(1)]
        internal LineGraphMarkerStyles LineGraphMarkerStyle
        {
            get
            {
                return this.m_LineGraphMarkerStyle;
            }
            set
            {
                if (value != this.m_LineGraphMarkerStyle)
                {
                    this.m_LineGraphMarkerStyle = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(0), Category("Appearance"), Description("Specifies whether values on the X axis of a line graph will be shown as ranges or points")]
        internal LineGraphXAxisTypes LineGraphXAxisType
        {
            get
            {
                return this.m_LineGraphXAxisType;
            }
            set
            {
                if (value != this.m_LineGraphXAxisType)
                {
                    this.m_LineGraphXAxisType = value;
                    base.Invalidate();
                }
            }
        }
        #endregion

        #region Text Format

        [Category("TextFormat"), Description("The format of text on columns or bars"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        internal TextFormat TextColumnFormat
        {
            get
            {
                return this.m_TextColumnFormat;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("The format of text on the data name axis"), Category("TextFormat")]
        internal TextFormat TextDataGroupAxisFormat
        {
            get
            {
                return this.m_TextDataGroupAxisFormat;
            }
        }

        [Category("TextFormat"), Description("The format of text on the data names index"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        internal TextFormat TextDataGroupIndexFormat
        {
            get
            {
                return this.m_TextDataGroupIndexFormat;
            }
        }

        [Description("The format of text on the data value axis"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("TextFormat")]
        internal TextFormat TextDataValueAxisFormat
        {
            get
            {
                return this.m_TextDataValueAxisFormat;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("TextFormat"), Description("The format of text on the data values axis")]
        internal TextFormat TextDataValuesIndexFormat
        {
            get
            {
                return this.m_TextDataValuesIndexFormat;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("The format of the footer text"), Category("TextFormat")]
        internal TextFormat TextFooterFormat
        {
            get
            {
                return this.m_TextFooterFormat;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("TextFormat"), Description("The format of the header text")]
        internal TextFormat TextHeaderFormat
        {
            get
            {
                return this.m_TextHeaderFormat;
            }
        }

        #endregion
    }
}

