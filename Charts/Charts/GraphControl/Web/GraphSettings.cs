namespace Nistec.Charts.Web
{
    //using Charts.Properties;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Nistec.Charts.Statistics;

    internal class GraphSettings : Form
    {
        private ToolStripMenuItem AddSimpleAverageToolStripMenuItem;
        private Button btnCancel;
        private Button BtnOK;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        internal ComboBox cbIDField;
        internal ComboBox cbLabels;
        internal ComboBox cbType;
        private McGraph chart;
        private ColorItemCollection col = new ColorItemCollection();
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ColorItemCollection culori;
        private DataItemCollection dataItems;
        private List<DataFieldsItem> dataFieldsItems = new List<DataFieldsItem>();
        private ChartDesigner designer;
        private ToolStripMenuItem exponentialToolStripMenuItem;
        private ToolStripMenuItem geometricAverageToolStripMenuItem;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox5;
        private ToolStripMenuItem harmonicAverageToolStripMenuItem;
        private ToolStripMenuItem hyperbolicToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private ListBox lbDataFields;
        private System.Windows.Forms.Label lblerror;
        private KeyItemCollection key;
        private ToolStripMenuItem linearToolStripMenuItem;
        private ToolStripMenuItem logarithmicToolStripMenuItem;
        private ToolStripMenuItem medianToolStripMenuItem;
        private ToolStripMenuItem modeToolStripMenuItem;
        private ToolStripMenuItem parabolicToolStripMenuItem;
        private ToolStripMenuItem polynomialToolStripMenuItem;
        private ToolStripMenuItem powerCurveToolStripMenuItem;
        private PropertyGrid propertyGrid1;
        private ToolStripMenuItem randomToolStripMenuItem;
        private ToolStripMenuItem recursionToolStripMenuItem;
        private ToolStripMenuItem runningAverageToolStripMenuItem;
        private ToolStripMenuItem simpleAverageToolStripMenuItem;
        private ToolStripStatusLabel StatusLabel;
        private StatusStrip statusStrip1;
        private TreeView tvAvailable;
        internal TextBox txtTitle;
        internal TextBox txtXAxisLabel;
        private ImageList imageList1;
        private Label label7;
        private Label label8;
        private Button btnRefresh;
        private Button btnColors;
        private Button btnValidate;
        private ToolTip toolTip1;
        internal TextBox txtYAxisLabel;

        internal GraphSettings(McGraph chart, ChartDesigner designer)
        {
            this.InitializeComponent();
            this.cbType.Items.AddRange(ChartMethods.ChartTypeList);
            this.chart = chart;
            this.designer = designer;
            this.col = chart.ColorItems;
            this.culori = chart.ColorItems;
            this.dataItems = chart.DataItems;
            this.key = chart.KeyItems;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphSettings));
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Available Fileds:");
            this.BtnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tvAvailable = new System.Windows.Forms.TreeView();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYAxisLabel = new System.Windows.Forms.TextBox();
            this.txtXAxisLabel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnColors = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddSimpleAverageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleAverageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geometricAverageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.harmonicAverageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runningAverageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recursionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exponentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logarithmicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerCurveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polynomialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parabolicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hyperbolicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbDataFields = new System.Windows.Forms.ListBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.cbLabels = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbIDField = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblerror = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(457, 326);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 0;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(538, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.tvAvailable);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtYAxisLabel);
            this.groupBox1.Controls.Add(this.txtXAxisLabel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 337);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Settings";
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 7;
            this.btnRefresh.ImageList = this.imageList1;
            this.btnRefresh.Location = new System.Drawing.Point(155, 303);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(24, 24);
            this.btnRefresh.TabIndex = 28;
            this.toolTip1.SetToolTip(this.btnRefresh, "Refresh Data Source");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "FillRightHS.png");
            this.imageList1.Images.SetKeyName(1, "FillLeftHS.png");
            this.imageList1.Images.SetKeyName(2, "FillUpHS.png");
            this.imageList1.Images.SetKeyName(3, "FillDownHS.png");
            this.imageList1.Images.SetKeyName(4, "add_att.gif");
            this.imageList1.Images.SetKeyName(5, "ColorHS.png");
            this.imageList1.Images.SetKeyName(6, "CheckGrammarHS.png");
            this.imageList1.Images.SetKeyName(7, "refresh_nav.gif");
            // 
            // tvAvailable
            // 
            this.tvAvailable.Location = new System.Drawing.Point(11, 153);
            this.tvAvailable.Name = "tvAvailable";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Available Fileds:";
            this.tvAvailable.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvAvailable.Size = new System.Drawing.Size(168, 149);
            this.tvAvailable.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Available Fileds:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Axis Y Name:";
            // 
            // txtYAxisLabel
            // 
            this.txtYAxisLabel.Location = new System.Drawing.Point(11, 113);
            this.txtYAxisLabel.Name = "txtYAxisLabel";
            this.txtYAxisLabel.Size = new System.Drawing.Size(168, 20);
            this.txtYAxisLabel.TabIndex = 15;
            // 
            // txtXAxisLabel
            // 
            this.txtXAxisLabel.Location = new System.Drawing.Point(11, 75);
            this.txtXAxisLabel.Name = "txtXAxisLabel";
            this.txtXAxisLabel.Size = new System.Drawing.Size(168, 20);
            this.txtXAxisLabel.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Axis X Name:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(11, 38);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(168, 20);
            this.txtTitle.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.ImageIndex = 1;
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(41, 303);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 24);
            this.button2.TabIndex = 17;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Chart Title:";
            // 
            // button1
            // 
            this.button1.ImageIndex = 0;
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(11, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 16;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnColors);
            this.groupBox2.Controls.Add(this.btnValidate);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.lbDataFields);
            this.groupBox2.Controls.Add(this.cbType);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.cbLabels);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cbIDField);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(209, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(188, 337);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Settings";
            // 
            // btnColors
            // 
            this.btnColors.ImageIndex = 5;
            this.btnColors.ImageList = this.imageList1;
            this.btnColors.Location = new System.Drawing.Point(127, 303);
            this.btnColors.Name = "btnColors";
            this.btnColors.Size = new System.Drawing.Size(24, 24);
            this.btnColors.TabIndex = 30;
            this.toolTip1.SetToolTip(this.btnColors, "Choose Colors");
            this.btnColors.UseVisualStyleBackColor = true;
            this.btnColors.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.ImageIndex = 6;
            this.btnValidate.ImageList = this.imageList1;
            this.btnValidate.Location = new System.Drawing.Point(157, 303);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(24, 24);
            this.btnValidate.TabIndex = 29;
            this.toolTip1.SetToolTip(this.btnValidate, "Validate Settings");
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Data Fileds:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Chart Type:";
            // 
            // button3
            // 
            this.button3.ImageIndex = 3;
            this.button3.ImageList = this.imageList1;
            this.button3.Location = new System.Drawing.Point(71, 303);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 24);
            this.button3.TabIndex = 20;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.ContextMenuStrip = this.contextMenuStrip1;
            this.button5.Enabled = false;
            this.button5.ImageIndex = 4;
            this.button5.ImageList = this.imageList1;
            this.button5.Location = new System.Drawing.Point(11, 303);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(24, 24);
            this.button5.TabIndex = 26;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button5_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSimpleAverageToolStripMenuItem,
            this.recursionToolStripMenuItem,
            this.randomToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(115, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // AddSimpleAverageToolStripMenuItem
            // 
            this.AddSimpleAverageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simpleAverageToolStripMenuItem,
            this.geometricAverageToolStripMenuItem,
            this.harmonicAverageToolStripMenuItem,
            this.runningAverageToolStripMenuItem,
            this.medianToolStripMenuItem,
            this.modeToolStripMenuItem});
            this.AddSimpleAverageToolStripMenuItem.Name = "AddSimpleAverageToolStripMenuItem";
            this.AddSimpleAverageToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.AddSimpleAverageToolStripMenuItem.Text = "Add Average";
            // 
            // simpleAverageToolStripMenuItem
            // 
            this.simpleAverageToolStripMenuItem.Name = "simpleAverageToolStripMenuItem";
            this.simpleAverageToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.simpleAverageToolStripMenuItem.Text = "Arithmetic Average";
            this.simpleAverageToolStripMenuItem.Click += new System.EventHandler(this.simpleAverageToolStripMenuItem_Click);
            // 
            // geometricAverageToolStripMenuItem
            // 
            this.geometricAverageToolStripMenuItem.Name = "geometricAverageToolStripMenuItem";
            this.geometricAverageToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.geometricAverageToolStripMenuItem.Text = "Geometric Average";
            this.geometricAverageToolStripMenuItem.Click += new System.EventHandler(this.geometricAverageToolStripMenuItem_Click);
            // 
            // harmonicAverageToolStripMenuItem
            // 
            this.harmonicAverageToolStripMenuItem.Name = "harmonicAverageToolStripMenuItem";
            this.harmonicAverageToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.harmonicAverageToolStripMenuItem.Text = "Harmonic Average";
            this.harmonicAverageToolStripMenuItem.Click += new System.EventHandler(this.harmonicAverageToolStripMenuItem_Click);
            // 
            // runningAverageToolStripMenuItem
            // 
            this.runningAverageToolStripMenuItem.Name = "runningAverageToolStripMenuItem";
            this.runningAverageToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.runningAverageToolStripMenuItem.Text = "Running Average";
            this.runningAverageToolStripMenuItem.Click += new System.EventHandler(this.runningAverageToolStripMenuItem_Click);
            // 
            // medianToolStripMenuItem
            // 
            this.medianToolStripMenuItem.Name = "medianToolStripMenuItem";
            this.medianToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.medianToolStripMenuItem.Text = "Median";
            this.medianToolStripMenuItem.Click += new System.EventHandler(this.medianToolStripMenuItem_Click);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.modeToolStripMenuItem.Text = "Mode";
            this.modeToolStripMenuItem.Click += new System.EventHandler(this.modeToolStripMenuItem_Click);
            // 
            // recursionToolStripMenuItem
            // 
            this.recursionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exponentialToolStripMenuItem,
            this.linearToolStripMenuItem,
            this.logarithmicToolStripMenuItem,
            this.powerCurveToolStripMenuItem,
            this.polynomialToolStripMenuItem,
            this.parabolicToolStripMenuItem,
            this.hyperbolicToolStripMenuItem});
            this.recursionToolStripMenuItem.Name = "recursionToolStripMenuItem";
            this.recursionToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.recursionToolStripMenuItem.Text = "Recursion";
            // 
            // exponentialToolStripMenuItem
            // 
            this.exponentialToolStripMenuItem.Name = "exponentialToolStripMenuItem";
            this.exponentialToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.exponentialToolStripMenuItem.Text = "Exponential";
            this.exponentialToolStripMenuItem.Click += new System.EventHandler(this.exponentialToolStripMenuItem_Click);
            // 
            // linearToolStripMenuItem
            // 
            this.linearToolStripMenuItem.Name = "linearToolStripMenuItem";
            this.linearToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.linearToolStripMenuItem.Text = "Linear";
            this.linearToolStripMenuItem.Click += new System.EventHandler(this.linearToolStripMenuItem_Click);
            // 
            // logarithmicToolStripMenuItem
            // 
            this.logarithmicToolStripMenuItem.Name = "logarithmicToolStripMenuItem";
            this.logarithmicToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.logarithmicToolStripMenuItem.Text = "Logarithmic";
            this.logarithmicToolStripMenuItem.Click += new System.EventHandler(this.logarithmicToolStripMenuItem_Click);
            // 
            // powerCurveToolStripMenuItem
            // 
            this.powerCurveToolStripMenuItem.Name = "powerCurveToolStripMenuItem";
            this.powerCurveToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.powerCurveToolStripMenuItem.Text = "Power Curve";
            this.powerCurveToolStripMenuItem.Click += new System.EventHandler(this.powerCurveToolStripMenuItem_Click);
            // 
            // polynomialToolStripMenuItem
            // 
            this.polynomialToolStripMenuItem.Name = "polynomialToolStripMenuItem";
            this.polynomialToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.polynomialToolStripMenuItem.Text = "Polynomic";
            this.polynomialToolStripMenuItem.Click += new System.EventHandler(this.polynomialToolStripMenuItem_Click);
            // 
            // parabolicToolStripMenuItem
            // 
            this.parabolicToolStripMenuItem.Name = "parabolicToolStripMenuItem";
            this.parabolicToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.parabolicToolStripMenuItem.Text = "Parabolic";
            this.parabolicToolStripMenuItem.Click += new System.EventHandler(this.parabolicToolStripMenuItem_Click);
            // 
            // hyperbolicToolStripMenuItem
            // 
            this.hyperbolicToolStripMenuItem.Name = "hyperbolicToolStripMenuItem";
            this.hyperbolicToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.hyperbolicToolStripMenuItem.Text = "Hyperbolic";
            this.hyperbolicToolStripMenuItem.Click += new System.EventHandler(this.hyperbolicToolStripMenuItem_Click);
            // 
            // randomToolStripMenuItem
            // 
            this.randomToolStripMenuItem.Name = "randomToolStripMenuItem";
            this.randomToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.randomToolStripMenuItem.Text = "Random Data";
            this.randomToolStripMenuItem.Click += new System.EventHandler(this.randomToolStripMenuItem_Click);
            // 
            // lbDataFields
            // 
            this.lbDataFields.FormattingEnabled = true;
            this.lbDataFields.Location = new System.Drawing.Point(11, 155);
            this.lbDataFields.Name = "lbDataFields";
            this.lbDataFields.Size = new System.Drawing.Size(170, 147);
            this.lbDataFields.TabIndex = 0;
            this.lbDataFields.SelectedIndexChanged += new System.EventHandler(this.lbDataFields_SelectedIndexChanged);
            this.lbDataFields.Click += new System.EventHandler(this.lbDataFields_Click);
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(11, 38);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(170, 21);
            this.cbType.TabIndex = 22;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.ImageIndex = 2;
            this.button4.ImageList = this.imageList1;
            this.button4.Location = new System.Drawing.Point(41, 303);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 24);
            this.button4.TabIndex = 19;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cbLabels
            // 
            this.cbLabels.FormattingEnabled = true;
            this.cbLabels.Location = new System.Drawing.Point(11, 112);
            this.cbLabels.Name = "cbLabels";
            this.cbLabels.Size = new System.Drawing.Size(170, 21);
            this.cbLabels.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Field Label:";
            // 
            // cbIDField
            // 
            this.cbIDField.FormattingEnabled = true;
            this.cbIDField.Location = new System.Drawing.Point(11, 74);
            this.cbIDField.Name = "cbIDField";
            this.cbIDField.Size = new System.Drawing.Size(170, 21);
            this.cbIDField.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Field ID:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.propertyGrid1);
            this.groupBox5.Location = new System.Drawing.Point(413, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 302);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Properties:";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(188, 277);
            this.propertyGrid1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 359);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // lblerror
            // 
            this.lblerror.AutoSize = true;
            this.lblerror.Location = new System.Drawing.Point(337, 351);
            this.lblerror.Name = "lblerror";
            this.lblerror.Size = new System.Drawing.Size(0, 13);
            this.lblerror.TabIndex = 18;
            // 
            // GraphSettings
            // 
            this.AcceptButton = this.BtnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(624, 381);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblerror);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GraphSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graph Settings";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.saveItems();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.tvAvailable.SelectedNode != null) && (this.tvAvailable.SelectedNode != this.tvAvailable.Nodes[0]))
                {
                    DataFieldsItem item = new DataFieldsItem(this.tvAvailable.SelectedNode.Text);
                    item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
                    this.dataFieldsItems.Add(item);
                    this.lbDataFields.Items.Add(this.tvAvailable.SelectedNode.Text);
                    this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
                }
                this.Validate();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //private void button1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (sender == this.button1)
        //    {
        //        this.button1.ImageIndex = 1;
        //    }
        //    if (sender == this.button2)
        //    {
        //        this.button2.ImageIndex = 3;
        //    }
        //    if (sender == this.button3)
        //    {
        //        this.button3.ImageIndex = 7;
        //    }
        //    if (sender == this.button4)
        //    {
        //        this.button4.ImageIndex = 5;
        //    }
        //    if (sender == this.button5)
        //    {
        //        this.button4.ImageIndex = 9;
        //    }
        //}

        //private void button1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (sender == this.button1)
        //    {
        //        this.button1.ImageIndex = 0;
        //    }
        //    if (sender == this.button2)
        //    {
        //        this.button2.ImageIndex = 2;
        //    }
        //    if (sender == this.button3)
        //    {
        //        this.button3.ImageIndex = 6;
        //    }
        //    if (sender == this.button4)
        //    {
        //        this.button4.ImageIndex = 4;
        //    }
        //    if (sender == this.button5)
        //    {
        //        this.button4.ImageIndex = 8;
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            int index = -1;
            try
            {
                index = this.lbDataFields.SelectedIndex;
                if (index >= 0)
                {
                    this.dataFieldsItems.RemoveAt(index);
                    try
                    {
                        this.lbDataFields.Items.RemoveAt(index);
                    }
                    catch
                    {
                    }
                }
                this.Validate();
                if (index > 0)
                {
                    this.lbDataFields.SelectedIndex = index - 1;
                }
                else if (this.lbDataFields.Items.Count > 0)
                {
                    this.lbDataFields.SelectedIndex = 0;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = this.lbDataFields.SelectedIndex;
                if (selectedIndex >= 0)
                {
                    if (selectedIndex < (this.lbDataFields.Items.Count - 1))
                    {
                        DataFieldsItem item = this.dataFieldsItems[selectedIndex];
                        this.dataFieldsItems.RemoveAt(selectedIndex);
                        this.dataFieldsItems.Insert(selectedIndex + 1, item);
                        string str = (string) this.lbDataFields.Items[selectedIndex];
                        try
                        {
                            this.lbDataFields.Items.RemoveAt(selectedIndex);
                        }
                        catch
                        {
                        }
                        this.lbDataFields.Items.Insert(selectedIndex + 1, str);
                        this.lbDataFields.SelectedIndex = selectedIndex + 1;
                    }
                    this.Validate();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = this.lbDataFields.SelectedIndex;
                if (selectedIndex >= 0)
                {
                    if (selectedIndex > 0)
                    {
                        DataFieldsItem item = this.dataFieldsItems[selectedIndex];
                        this.dataFieldsItems.RemoveAt(selectedIndex);
                        this.dataFieldsItems.Insert(selectedIndex - 1, item);
                        string str = (string) this.lbDataFields.Items[selectedIndex];
                        try
                        {
                            this.lbDataFields.Items.RemoveAt(selectedIndex);
                        }
                        catch
                        {
                        }
                        this.lbDataFields.Items.Insert(selectedIndex - 1, str);
                        this.lbDataFields.SelectedIndex = selectedIndex - 1;
                    }
                    this.Validate();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button5_MouseClick(object sender, MouseEventArgs e)
        {
            this.contextMenuStrip1.Show(this.button5.PointToScreen(e.Location));
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.cbType.SelectedIndex;
            this.txtXAxisLabel.Enabled = this.txtYAxisLabel.Enabled = true;
            this.btnColors.Visible = this.GetMultiColors(selectedIndex);
            if (this.lbDataFields.SelectedIndex < 0)
            {
                this.propertyGrid1.SelectedObject = null;
            }
            else
            {
                this.propertyGrid1.SelectedObject = this.dataFieldsItems[this.lbDataFields.SelectedIndex];
            }
            this.txtXAxisLabel.Enabled = this.txtYAxisLabel.Enabled = !this.GetNoLabels(selectedIndex);
            //--this.richTextBox1.Text = ChartTypeComments.GetComment(selectedIndex);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.lbDataFields.SelectedIndex < 0)
            {
                e.Cancel = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void exponentialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string field = RecursiveExponential.GetName(this.lbDataFields.SelectedItem.ToString());
            foreach (string str2 in this.lbDataFields.Items)
            {
                if (str2 == field)
                {
                    return;
                }
            }
            DataFieldsItem item = new DataFieldsItem(field);
            item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
            this.dataFieldsItems.Add(item);
            this.lbDataFields.Items.Add(field);
            this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
            this.Validate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtTitle.Text = this.chart.GraphTitle;
            this.txtXAxisLabel.Text = this.chart.AxisLabelX;
            this.txtYAxisLabel.Text = this.chart.AxisLabelY;
            this.cbType.SelectedIndex = (int)this.chart.ChartType;
            this.Populate();
            this.dataFieldsItems.Clear();
            this.lbDataFields.Items.Clear();
            foreach (DataItem item in this.dataItems)
            {
                DataFieldsItem item2 = new DataFieldsItem(item.Name);
                item2.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
                if (this.key.Count > this.dataFieldsItems.Count)
                {
                    item2.Key = this.key[this.lbDataFields.Items.Count].Name;
                }
                this.dataFieldsItems.Add(item2);
                this.lbDataFields.Items.Add(item.Name);
            }
            try
            {
                this.cbIDField.Text = this.chart.FieldID;
            }
            catch
            {
            }
            try
            {
                this.cbLabels.Text = this.chart.FieldLabel;
            }
            catch
            {
            }
            try
            {
                this.lbDataFields.SelectedIndex = 0;
                this.Validate();
            }
            catch
            {
            }
        }

        private void geometricAverageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string field = GeometricAverage.GetName(this.lbDataFields.SelectedItem.ToString());
            foreach (string str2 in this.lbDataFields.Items)
            {
                if (str2 == field)
                {
                    return;
                }
            }
            DataFieldsItem item = new DataFieldsItem(field);
            item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
            this.dataFieldsItems.Add(item);
            this.lbDataFields.Items.Add(field);
            this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
            this.Validate();
        }

        private bool GetMultiColors(int i)
        {
            int[] numArray = new int[] { 3, 30, 0x1d, 0x11, 0x1f, 1, 13 };
            for (int j = 0; j < numArray.Length; j++)
            {
                if (numArray[j] == i)
                {
                    return true;
                }
            }
            return false;
        }

        private bool GetNoLabels(int i)
        {
            int[] numArray = new int[] { 3, 30, 0x1d, 0x10, 0x1f, 0x15, 0x16, 1, 13, 0x12, 0x13, 0x17, 0x18 };
            for (int j = 0; j < numArray.Length; j++)
            {
                if (numArray[j] == i)
                {
                    return true;
                }
            }
            return false;
        }

        private bool GetOneFiledsOnly(int i)
        {
            int[] numArray = new int[] { 0, 2, 12, 3, 30, 0x1d, 0x11, 5, 0x10, 1, 13, 0x12, 0x13, 14, 20, 0x20 };
            for (int j = 0; j < numArray.Length; j++)
            {
                if (numArray[j] == i)
                {
                    return true;
                }
            }
            return false;
        }

        private void harmonicAverageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string field = HarmonicAverage.GetName(this.lbDataFields.SelectedItem.ToString());
            foreach (string str2 in this.lbDataFields.Items)
            {
                if (str2 == field)
                {
                    return;
                }
            }
            DataFieldsItem item = new DataFieldsItem(field);
            item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
            this.dataFieldsItems.Add(item);
            this.lbDataFields.Items.Add(field);
            this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
            this.Validate();
        }

        private void hyperbolicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string field = RecursiveHyperbolic.GetName(this.lbDataFields.SelectedItem.ToString());
            foreach (string str2 in this.lbDataFields.Items)
            {
                if (str2 == field)
                {
                    return;
                }
            }
            DataFieldsItem item = new DataFieldsItem(field);
            item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
            this.dataFieldsItems.Add(item);
            this.lbDataFields.Items.Add(field);
            this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
            this.Validate();
        }

 
        private void lbDataFields_Click(object sender, EventArgs e)
        {
            if (this.lbDataFields.SelectedIndex < 0)
            {
                this.propertyGrid1.SelectedObject = null;
            }
            else
            {
                this.propertyGrid1.SelectedObject = this.dataFieldsItems[this.lbDataFields.SelectedIndex];
            }
        }

        private void lbDataFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = this.dataFieldsItems[this.lbDataFields.SelectedIndex];
        }

        private void linearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string field = RecursiveLinear.GetName(this.lbDataFields.SelectedItem.ToString());
            foreach (string str2 in this.lbDataFields.Items)
            {
                if (str2 == field)
                {
                    return;
                }
            }
            DataFieldsItem item = new DataFieldsItem(field);
            item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
            this.dataFieldsItems.Add(item);
            this.lbDataFields.Items.Add(field);
            this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
            this.Validate();
        }

         private void btnColors_Click(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = this.col;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.designer.refreshSchema();
            this.Populate();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {

           this.Validate();
        }

        private void logarithmicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prompt prompt = new Prompt("Enter Base", true);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string field = RecursiveLogarithmic.GetName(this.lbDataFields.SelectedItem.ToString(), prompt.IntNum);
                foreach (string str2 in this.lbDataFields.Items)
                {
                    if (str2 == field)
                    {
                        return;
                    }
                }
                DataFieldsItem item = new DataFieldsItem(field);
                item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
                this.dataFieldsItems.Add(item);
                this.lbDataFields.Items.Add(field);
                this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
                this.Validate();
            }
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string field = Median.GetName(this.lbDataFields.SelectedItem.ToString());
            foreach (string str2 in this.lbDataFields.Items)
            {
                if (str2 == field)
                {
                    return;
                }
            }
            DataFieldsItem item = new DataFieldsItem(field);
            item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
            this.dataFieldsItems.Add(item);
            this.lbDataFields.Items.Add(field);
            this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
            this.Validate();
        }

        private void modeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string field = Mode.GetName(this.lbDataFields.SelectedItem.ToString());
            foreach (string str2 in this.lbDataFields.Items)
            {
                if (str2 == field)
                {
                    return;
                }
            }
            DataFieldsItem item = new DataFieldsItem(field);
            item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
            this.dataFieldsItems.Add(item);
            this.lbDataFields.Items.Add(field);
            this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
            this.Validate();
        }

        private void parabolicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string field = RecursiveParabolic.GetName(this.lbDataFields.SelectedItem.ToString());
            foreach (string str2 in this.lbDataFields.Items)
            {
                if (str2 == field)
                {
                    return;
                }
            }
            DataFieldsItem item = new DataFieldsItem(field);
            item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
            this.dataFieldsItems.Add(item);
            this.lbDataFields.Items.Add(field);
            this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
            this.Validate();
        }

        private void polynomialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prompt prompt = new Prompt("Ecuation Order:", true);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string field = RecursivePolynomic.GetName(this.lbDataFields.SelectedItem.ToString(), prompt.IntNum);
                foreach (string str2 in this.lbDataFields.Items)
                {
                    if (str2 == field)
                    {
                        return;
                    }
                }
                DataFieldsItem item = new DataFieldsItem(field);
                item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
                this.dataFieldsItems.Add(item);
                this.lbDataFields.Items.Add(field);
                this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
                this.Validate();
            }
        }

        private void Populate()
        {
            IEnumerator enumerator = this.designer.GetSampleDataSource().GetEnumerator();
            enumerator.MoveNext();
            DataRowView current = (DataRowView) enumerator.Current;
            DataTable table = current.Row.Table;
            this.cbIDField.Items.Clear();
            this.cbLabels.Items.Clear();
            this.tvAvailable.Nodes[0].Nodes.Clear();
            foreach (DataColumn column in table.Columns)
            {
                this.cbIDField.Items.Add(column.ColumnName);
                this.cbLabels.Items.Add(column.ColumnName);
                try
                {
                    decimal.Parse(current.Row[column].ToString());
                    this.tvAvailable.Nodes[0].Nodes.Add(column.ColumnName, column.ColumnName);
                    continue;
                }
                catch
                {
                    continue;
                }
            }
        }

        private void powerCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prompt prompt = new Prompt("Enter Base", true);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string field = RecursivePowerCurve.GetName(this.lbDataFields.SelectedItem.ToString(), (float)prompt.IntNum);
                foreach (string str2 in this.lbDataFields.Items)
                {
                    if (str2 == field)
                    {
                        return;
                    }
                }
                DataFieldsItem item = new DataFieldsItem(field);
                item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
                this.dataFieldsItems.Add(item);
                this.lbDataFields.Items.Add(field);
                this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
                this.Validate();
            }
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prompt prompt = new Prompt("Ecuation Order:", true);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string field = RandomData.GetName(prompt.IntNum);
                foreach (string str2 in this.lbDataFields.Items)
                {
                    if (str2 == field)
                    {
                        return;
                    }
                }
                DataFieldsItem item = new DataFieldsItem(field);
                item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
                this.dataFieldsItems.Add(item);
                this.lbDataFields.Items.Add(field);
                this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
                this.Validate();
            }
        }

        private void runningAverageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prompt prompt = new Prompt("Running Average Period:", true);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string field = RunningAverage.GetName(this.lbDataFields.SelectedItem.ToString(), prompt.IntNum);
                foreach (string str2 in this.lbDataFields.Items)
                {
                    if (str2 == field)
                    {
                        return;
                    }
                }
                DataFieldsItem item = new DataFieldsItem(field);
                item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
                this.dataFieldsItems.Add(item);
                this.lbDataFields.Items.Add(field);
                this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
                this.Validate();
            }
        }

        internal void saveItems()
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this.chart)["GraphTitle"];
            if (descriptor != null)
            {
                descriptor.SetValue(this.chart, this.txtTitle.Text);
            }
            descriptor = TypeDescriptor.GetProperties(this.chart)["AxisLabelX"];
            if (descriptor != null)
            {
                descriptor.SetValue(this.chart, this.txtXAxisLabel.Text);
            }
            descriptor = TypeDescriptor.GetProperties(this.chart)["AxisLabelY"];
            if (descriptor != null)
            {
                descriptor.SetValue(this.chart, this.txtYAxisLabel.Text);
            }
            descriptor = TypeDescriptor.GetProperties(this.chart)["Type"];
            if (descriptor != null)
            {
                descriptor.SetValue(this.chart, (ChartType) this.cbType.SelectedIndex);
            }
            descriptor = TypeDescriptor.GetProperties(this.chart)["FieldLabel"];
            if (descriptor != null)
            {
                descriptor.SetValue(this.chart, this.cbLabels.Text);
            }
            descriptor = TypeDescriptor.GetProperties(this.chart)["FieldID"];
            if (descriptor != null)
            {
                descriptor.SetValue(this.chart, this.cbIDField.Text);
            }
            string[] items = new string[this.dataFieldsItems.Count];
            for (int i = 0; i < this.dataFieldsItems.Count; i++)
            {
                items[i] = this.dataFieldsItems[i].Key;
            }
            this.chart.SetKey(items);
            string[] fields = new string[this.dataFieldsItems.Count];
            for (int j = 0; j < this.dataFieldsItems.Count; j++)
            {
                fields[j] = this.dataFieldsItems[j].Field;
            }
            this.chart.SetDataFields(fields);
            if (!this.GetMultiColors(this.cbType.SelectedIndex))
            {
                Color[] colors = new Color[this.dataFieldsItems.Count];
                for (int k = 0; k < this.dataFieldsItems.Count; k++)
                {
                    colors[k] = this.dataFieldsItems[k].ItemColor;
                }
                this.chart.SetColors(colors);
            }
            else
            {
                Color[] colorArray2 = new Color[this.col.Count];
                for (int m = 0; m < this.col.Count; m++)
                {
                    colorArray2[m] = this.col[m].color;
                }
                this.chart.SetColors(colorArray2);
            }
        }

        private void SetError(string eroare)
        {
            this.statusStrip1.Items[0].Text=(eroare);
            //this.errorProvider1.SetError(this.lblerror, eroare);
        }

        private void simpleAverageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string field = ArithmeticAverage.GetName(this.lbDataFields.SelectedItem.ToString());
            foreach (string str2 in this.lbDataFields.Items)
            {
                if (str2 == field)
                {
                    return;
                }
            }
            DataFieldsItem item = new DataFieldsItem(field);
            item.ItemColor = this.culori[this.lbDataFields.Items.Count % this.culori.Count];
            this.dataFieldsItems.Add(item);
            this.lbDataFields.Items.Add(field);
            this.lbDataFields.SelectedIndex = this.lbDataFields.Items.Count - 1;
            this.Validate();
        }

        private new void Validate()
        {
            try
            {
                if (this.lbDataFields.Items.Count < 1)
                {
                    this.SetError("No data fileds defined!");
                }
                else
                {
                    for (int i = 0; i < (this.lbDataFields.Items.Count - 1); i++)
                    {
                        for (int k = i + 1; k < this.lbDataFields.Items.Count; k++)
                        {
                            if (this.lbDataFields.Items[i].ToString() == this.lbDataFields.Items[k].ToString())
                            {
                                this.SetError("Duplicate data fileds are not allowed!");
                                return;
                            }
                        }
                    }
                    for (int j = 0; j < this.lbDataFields.Items.Count; j++)
                    {
                        bool flag = false;
                        for (int m = 0; m < this.tvAvailable.Nodes[0].Nodes.Count; m++)
                        {
                            if (this.lbDataFields.Items[j].ToString() == this.tvAvailable.Nodes[0].Nodes[m].Text)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            string str = Statistic.IsStatisticsField(this.lbDataFields.Items[j].ToString());
                            if (str == "RandomData")
                            {
                                flag = true;
                            }
                            else if (str != null)
                            {
                                for (int n = 0; n < this.lbDataFields.Items.Count; n++)
                                {
                                    if (str == this.lbDataFields.Items[n].ToString())
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!flag)
                        {
                            this.SetError("Field " + this.lbDataFields.Items[j].ToString() + " is not among the fields that can be used as data-fileds!");
                            return;
                        }
                    }
                    if ((this.chart.DataSource == null) && (this.chart.DataSourceID == null))
                    {
                        this.SetError("You should choose a datasource.");
                    }
                    else if (this.GetOneFiledsOnly(this.cbType.SelectedIndex) && (this.lbDataFields.Items.Count != 1))
                    {
                        this.SetError("This Chart Type suport only one data fileds.");
                    }
                    else if ((this.cbType.SelectedIndex == 0x1f) && (this.lbDataFields.Items.Count != 2))
                    {
                        this.SetError("This Chart Type expects 2 data fileds");//(latitude & longitude).");
                    }
                    //else if (((this.cbType.SelectedIndex == 0x10) || (this.cbType.SelectedIndex == 0x1f)) && ((this.chart.MapXML == null) || (this.chart.MapXML == string.Empty)))
                    //{
                    //    this.SetError("For the Map Mode you should set the MapXml Property.");
                    //}
                    //else if ((this.cbType.SelectedIndex == 0x10) && ((this.chart.CustomImage == null) || (this.chart.CustomImage == string.Empty)))
                    //{
                    //    this.SetError("For the Map Mode you should set the CustomImage property.");
                    //}
                    //else if ((this.cbType.SelectedIndex == 20) && ((this.chart.CustomImage == null) || (this.chart.CustomImage == string.Empty)))
                    //{
                    //    this.SetError("For the UserDrawnBars Mode you should set the CustomImage property.");
                    //}
                    else if (this.cbIDField.SelectedIndex < 0)
                    {
                        this.SetError("You should provide an FieldID (if you use the ChartClick event).");
                    }
                    else
                    {
                        this.SetError("");
                        this.statusStrip1.Items[0].Text=("Settings are valid!");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
 
    }
}

