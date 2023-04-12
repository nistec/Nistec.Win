namespace ChartsSample
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            MControl.WinForms.LinkLabelItem linkPieExpanded;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mcTaskBar1 = new MControl.WinForms.McTaskBar();
            this.mcTaskPanel1 = new MControl.WinForms.McTaskPanel();
            this.linkBar = new MControl.WinForms.LinkLabelItem();
            this.linkBar3d = new MControl.WinForms.LinkLabelItem();
            this.linkBarMulti3d = new MControl.WinForms.LinkLabelItem();
            this.linkLine = new MControl.WinForms.LinkLabelItem();
            this.linkLineMulti3d = new MControl.WinForms.LinkLabelItem();
            this.linkPipes = new MControl.WinForms.LinkLabelItem();
            this.linkSurface = new MControl.WinForms.LinkLabelItem();
            this.linkSurface3d = new MControl.WinForms.LinkLabelItem();
            this.linksurfaceMulti3d = new MControl.WinForms.LinkLabelItem();
            this.linkPie = new MControl.WinForms.LinkLabelItem();
            this.linkPie3d = new MControl.WinForms.LinkLabelItem();
            this.mcTaskPanel2 = new MControl.WinForms.McTaskPanel();
            this.linkPieChart = new MControl.WinForms.LinkLabelItem();
            this.linkPieChartDisplay = new MControl.WinForms.LinkLabelItem();
            this.mcTaskPanel3 = new MControl.WinForms.McTaskPanel();
            this.linkMeter = new MControl.WinForms.LinkLabelItem();
            this.linkMeterRange = new MControl.WinForms.LinkLabelItem();
            this.linkLed = new MControl.WinForms.LinkLabelItem();
            this.linkUsage = new MControl.WinForms.LinkLabelItem();
            this.panelControl = new System.Windows.Forms.Panel();
            linkPieExpanded = new MControl.WinForms.LinkLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.mcTaskBar1)).BeginInit();
            this.mcTaskBar1.SuspendLayout();
            this.mcTaskPanel1.SuspendLayout();
            this.mcTaskPanel2.SuspendLayout();
            this.mcTaskPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkPieExpanded
            // 
            linkPieExpanded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            linkPieExpanded.Cursor = System.Windows.Forms.Cursors.Hand;
            linkPieExpanded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            linkPieExpanded.ImageIndex = 1;
            linkPieExpanded.ImageList = this.imageList1;
            linkPieExpanded.Location = new System.Drawing.Point(0, 248);
            linkPieExpanded.Name = "linkPieExpanded";
            linkPieExpanded.Size = new System.Drawing.Size(176, 20);
            linkPieExpanded.TabIndex = 12;
            linkPieExpanded.Text = "Pie Expanded";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "graphhs.png");
            this.imageList1.Images.SetKeyName(1, "mc_10.jpeg");
            this.imageList1.Images.SetKeyName(2, "mc_1.jpeg");
            this.imageList1.Images.SetKeyName(3, "mc_2.jpeg");
            this.imageList1.Images.SetKeyName(4, "mc_3.jpeg");
            this.imageList1.Images.SetKeyName(5, "mc_4.jpeg");
            this.imageList1.Images.SetKeyName(6, "mc_5.jpeg");
            this.imageList1.Images.SetKeyName(7, "mc_6.jpeg");
            this.imageList1.Images.SetKeyName(8, "mc_7.jpeg");
            this.imageList1.Images.SetKeyName(9, "mc_8.jpeg");
            this.imageList1.Images.SetKeyName(10, "mc_9.jpeg");
            this.imageList1.Images.SetKeyName(11, "piecha1.gif");
            this.imageList1.Images.SetKeyName(12, "UsageHistory.bmp");
            this.imageList1.Images.SetKeyName(13, "Led.bmp");
            this.imageList1.Images.SetKeyName(14, "Meter.bmp");
            this.imageList1.Images.SetKeyName(15, "Usage.bmp");
            // 
            // mcTaskBar1
            // 
            this.mcTaskBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mcTaskBar1.Controls.Add(this.mcTaskPanel1);
            this.mcTaskBar1.Controls.Add(this.mcTaskPanel2);
            this.mcTaskBar1.Controls.Add(this.mcTaskPanel3);
            this.mcTaskBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.mcTaskBar1.ImageList = this.imageList1;
            this.mcTaskBar1.Location = new System.Drawing.Point(2, 38);
            this.mcTaskBar1.Name = "mcTaskBar1";
            this.mcTaskBar1.Panels.AddRange(new MControl.WinForms.McTaskPanel[] {
            this.mcTaskPanel1,
            this.mcTaskPanel2,
            this.mcTaskPanel3});
            this.mcTaskBar1.SingleActive = false;
            this.mcTaskBar1.Size = new System.Drawing.Size(176, 476);
            this.mcTaskBar1.TabIndex = 0;
            // 
            // mcTaskPanel1
            // 
            this.mcTaskPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcTaskPanel1.Controls.Add(this.linkBar);
            this.mcTaskPanel1.Controls.Add(this.linkBar3d);
            this.mcTaskPanel1.Controls.Add(this.linkBarMulti3d);
            this.mcTaskPanel1.Controls.Add(this.linkLine);
            this.mcTaskPanel1.Controls.Add(this.linkLineMulti3d);
            this.mcTaskPanel1.Controls.Add(this.linkPipes);
            this.mcTaskPanel1.Controls.Add(this.linkSurface);
            this.mcTaskPanel1.Controls.Add(this.linkSurface3d);
            this.mcTaskPanel1.Controls.Add(this.linksurfaceMulti3d);
            this.mcTaskPanel1.Controls.Add(this.linkPie);
            this.mcTaskPanel1.Controls.Add(this.linkPie3d);
            this.mcTaskPanel1.Controls.Add(linkPieExpanded);
            this.mcTaskPanel1.ControlSpace = 0;
            this.mcTaskPanel1.Image = ((System.Drawing.Image)(resources.GetObject("mcTaskPanel1.Image")));
            this.mcTaskPanel1.Items.AddRange(new MControl.WinForms.LinkLabelItem[] {
            this.linkBar,
            this.linkBar3d,
            this.linkBarMulti3d,
            this.linkLine,
            this.linkLineMulti3d,
            this.linkPipes,
            this.linkSurface,
            this.linkSurface3d,
            this.linksurfaceMulti3d,
            this.linkPie,
            this.linkPie3d,
            linkPieExpanded});
            this.mcTaskPanel1.Location = new System.Drawing.Point(0, 0);
            this.mcTaskPanel1.Name = "mcTaskPanel1";
            this.mcTaskPanel1.Size = new System.Drawing.Size(176, 25);
            this.mcTaskPanel1.TabIndex = 0;
            this.mcTaskPanel1.Text = "Graph Control";
            this.mcTaskPanel1.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcTaskPanel1.ItemClicked += new MControl.WinForms.LinkClickEventHandler(this.mcTaskPanel1_ItemClicked);
            // 
            // linkBar
            // 
            this.linkBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkBar.ImageIndex = 2;
            this.linkBar.ImageList = this.imageList1;
            this.linkBar.Location = new System.Drawing.Point(0, 28);
            this.linkBar.Name = "linkBar";
            this.linkBar.Size = new System.Drawing.Size(176, 20);
            this.linkBar.TabIndex = 1;
            this.linkBar.Text = "Bar";
            // 
            // linkBar3d
            // 
            this.linkBar3d.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkBar3d.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkBar3d.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkBar3d.ImageIndex = 7;
            this.linkBar3d.ImageList = this.imageList1;
            this.linkBar3d.Location = new System.Drawing.Point(0, 48);
            this.linkBar3d.Name = "linkBar3d";
            this.linkBar3d.Size = new System.Drawing.Size(176, 20);
            this.linkBar3d.TabIndex = 2;
            this.linkBar3d.Text = "Bar 3D";
            // 
            // linkBarMulti3d
            // 
            this.linkBarMulti3d.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkBarMulti3d.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkBarMulti3d.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkBarMulti3d.ImageIndex = 3;
            this.linkBarMulti3d.ImageList = this.imageList1;
            this.linkBarMulti3d.Location = new System.Drawing.Point(0, 68);
            this.linkBarMulti3d.Name = "linkBarMulti3d";
            this.linkBarMulti3d.Size = new System.Drawing.Size(176, 20);
            this.linkBarMulti3d.TabIndex = 3;
            this.linkBarMulti3d.Text = "Bar Multi3D";
            // 
            // linkLine
            // 
            this.linkLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLine.ImageIndex = 5;
            this.linkLine.ImageList = this.imageList1;
            this.linkLine.Location = new System.Drawing.Point(0, 88);
            this.linkLine.Name = "linkLine";
            this.linkLine.Size = new System.Drawing.Size(176, 20);
            this.linkLine.TabIndex = 4;
            this.linkLine.Text = "Line";
            // 
            // linkLineMulti3d
            // 
            this.linkLineMulti3d.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLineMulti3d.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLineMulti3d.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLineMulti3d.ImageIndex = 6;
            this.linkLineMulti3d.ImageList = this.imageList1;
            this.linkLineMulti3d.Location = new System.Drawing.Point(0, 108);
            this.linkLineMulti3d.Name = "linkLineMulti3d";
            this.linkLineMulti3d.Size = new System.Drawing.Size(176, 20);
            this.linkLineMulti3d.TabIndex = 6;
            this.linkLineMulti3d.Text = "Line Multi3D";
            // 
            // linkPipes
            // 
            this.linkPipes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkPipes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkPipes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkPipes.ImageIndex = 7;
            this.linkPipes.ImageList = this.imageList1;
            this.linkPipes.Location = new System.Drawing.Point(0, 128);
            this.linkPipes.Name = "linkPipes";
            this.linkPipes.Size = new System.Drawing.Size(176, 20);
            this.linkPipes.TabIndex = 5;
            this.linkPipes.Text = "Pipes";
            // 
            // linkSurface
            // 
            this.linkSurface.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSurface.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkSurface.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkSurface.ImageIndex = 8;
            this.linkSurface.ImageList = this.imageList1;
            this.linkSurface.Location = new System.Drawing.Point(0, 148);
            this.linkSurface.Name = "linkSurface";
            this.linkSurface.Size = new System.Drawing.Size(176, 20);
            this.linkSurface.TabIndex = 7;
            this.linkSurface.Text = "Surface";
            // 
            // linkSurface3d
            // 
            this.linkSurface3d.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSurface3d.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkSurface3d.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkSurface3d.ImageIndex = 9;
            this.linkSurface3d.ImageList = this.imageList1;
            this.linkSurface3d.Location = new System.Drawing.Point(0, 168);
            this.linkSurface3d.Name = "linkSurface3d";
            this.linkSurface3d.Size = new System.Drawing.Size(176, 20);
            this.linkSurface3d.TabIndex = 8;
            this.linkSurface3d.Text = "Surface 3D";
            // 
            // linksurfaceMulti3d
            // 
            this.linksurfaceMulti3d.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linksurfaceMulti3d.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linksurfaceMulti3d.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linksurfaceMulti3d.ImageIndex = 10;
            this.linksurfaceMulti3d.ImageList = this.imageList1;
            this.linksurfaceMulti3d.Location = new System.Drawing.Point(0, 188);
            this.linksurfaceMulti3d.Name = "linksurfaceMulti3d";
            this.linksurfaceMulti3d.Size = new System.Drawing.Size(176, 20);
            this.linksurfaceMulti3d.TabIndex = 9;
            this.linksurfaceMulti3d.Text = "Surface Multi3D";
            // 
            // linkPie
            // 
            this.linkPie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkPie.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkPie.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkPie.ImageIndex = 1;
            this.linkPie.ImageList = this.imageList1;
            this.linkPie.Location = new System.Drawing.Point(0, 208);
            this.linkPie.Name = "linkPie";
            this.linkPie.Size = new System.Drawing.Size(176, 20);
            this.linkPie.TabIndex = 10;
            this.linkPie.Text = "Pie";
            // 
            // linkPie3d
            // 
            this.linkPie3d.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkPie3d.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkPie3d.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkPie3d.ImageIndex = 1;
            this.linkPie3d.ImageList = this.imageList1;
            this.linkPie3d.Location = new System.Drawing.Point(0, 228);
            this.linkPie3d.Name = "linkPie3d";
            this.linkPie3d.Size = new System.Drawing.Size(176, 20);
            this.linkPie3d.TabIndex = 11;
            this.linkPie3d.Text = "Pie 3D";
            // 
            // mcTaskPanel2
            // 
            this.mcTaskPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcTaskPanel2.Controls.Add(this.linkPieChart);
            this.mcTaskPanel2.Controls.Add(this.linkPieChartDisplay);
            this.mcTaskPanel2.ControlSpace = 0;
            this.mcTaskPanel2.Image = ((System.Drawing.Image)(resources.GetObject("mcTaskPanel2.Image")));
            this.mcTaskPanel2.Items.AddRange(new MControl.WinForms.LinkLabelItem[] {
            this.linkPieChart,
            this.linkPieChartDisplay});
            this.mcTaskPanel2.Location = new System.Drawing.Point(0, 25);
            this.mcTaskPanel2.Name = "mcTaskPanel2";
            this.mcTaskPanel2.Size = new System.Drawing.Size(176, 25);
            this.mcTaskPanel2.TabIndex = 1;
            this.mcTaskPanel2.Text = "Pie Chart";
            this.mcTaskPanel2.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcTaskPanel2.ItemClicked += new MControl.WinForms.LinkClickEventHandler(this.mcTaskPanel2_ItemClicked);
            // 
            // linkPieChart
            // 
            this.linkPieChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkPieChart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkPieChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkPieChart.ImageIndex = 11;
            this.linkPieChart.ImageList = this.imageList1;
            this.linkPieChart.Location = new System.Drawing.Point(0, 28);
            this.linkPieChart.Name = "linkPieChart";
            this.linkPieChart.Size = new System.Drawing.Size(176, 20);
            this.linkPieChart.TabIndex = 1;
            this.linkPieChart.Text = "Pie Chart";
            // 
            // linkPieChartDisplay
            // 
            this.linkPieChartDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkPieChartDisplay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkPieChartDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkPieChartDisplay.ImageIndex = 11;
            this.linkPieChartDisplay.ImageList = this.imageList1;
            this.linkPieChartDisplay.Location = new System.Drawing.Point(0, 48);
            this.linkPieChartDisplay.Name = "linkPieChartDisplay";
            this.linkPieChartDisplay.Size = new System.Drawing.Size(176, 20);
            this.linkPieChartDisplay.TabIndex = 2;
            this.linkPieChartDisplay.Text = "Pie Chart Display";
            // 
            // mcTaskPanel3
            // 
            this.mcTaskPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcTaskPanel3.Controls.Add(this.linkMeter);
            this.mcTaskPanel3.Controls.Add(this.linkMeterRange);
            this.mcTaskPanel3.Controls.Add(this.linkLed);
            this.mcTaskPanel3.Controls.Add(this.linkUsage);
            this.mcTaskPanel3.ControlSpace = 0;
            this.mcTaskPanel3.Image = ((System.Drawing.Image)(resources.GetObject("mcTaskPanel3.Image")));
            this.mcTaskPanel3.Items.AddRange(new MControl.WinForms.LinkLabelItem[] {
            this.linkMeter,
            this.linkMeterRange,
            this.linkLed,
            this.linkUsage});
            this.mcTaskPanel3.Location = new System.Drawing.Point(0, 50);
            this.mcTaskPanel3.Name = "mcTaskPanel3";
            this.mcTaskPanel3.Size = new System.Drawing.Size(176, 25);
            this.mcTaskPanel3.TabIndex = 2;
            this.mcTaskPanel3.Text = "Meter Controls";
            this.mcTaskPanel3.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcTaskPanel3.ItemClicked += new MControl.WinForms.LinkClickEventHandler(this.mcTaskPanel3_ItemClicked);
            // 
            // linkMeter
            // 
            this.linkMeter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkMeter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkMeter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkMeter.ImageIndex = 14;
            this.linkMeter.ImageList = this.imageList1;
            this.linkMeter.Location = new System.Drawing.Point(0, 28);
            this.linkMeter.Name = "linkMeter";
            this.linkMeter.Size = new System.Drawing.Size(176, 20);
            this.linkMeter.TabIndex = 1;
            this.linkMeter.Text = "Meter";
            // 
            // linkMeterRange
            // 
            this.linkMeterRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkMeterRange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkMeterRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkMeterRange.ImageIndex = 14;
            this.linkMeterRange.ImageList = this.imageList1;
            this.linkMeterRange.Location = new System.Drawing.Point(0, 48);
            this.linkMeterRange.Name = "linkMeterRange";
            this.linkMeterRange.Size = new System.Drawing.Size(176, 20);
            this.linkMeterRange.TabIndex = 2;
            this.linkMeterRange.Text = "Meter with Range";
            // 
            // linkLed
            // 
            this.linkLed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLed.ImageIndex = 13;
            this.linkLed.ImageList = this.imageList1;
            this.linkLed.Location = new System.Drawing.Point(0, 68);
            this.linkLed.Name = "linkLed";
            this.linkLed.Size = new System.Drawing.Size(176, 20);
            this.linkLed.TabIndex = 3;
            this.linkLed.Text = "Led";
            // 
            // linkUsage
            // 
            this.linkUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkUsage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkUsage.ImageIndex = 12;
            this.linkUsage.ImageList = this.imageList1;
            this.linkUsage.Location = new System.Drawing.Point(0, 88);
            this.linkUsage.Name = "linkUsage";
            this.linkUsage.Size = new System.Drawing.Size(176, 20);
            this.linkUsage.TabIndex = 4;
            this.linkUsage.Text = "Usage";
            // 
            // panelControl
            // 
            this.panelControl.BackColor = System.Drawing.Color.White;
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(178, 38);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(596, 476);
            this.panelControl.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(776, 516);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.mcTaskBar1);
            this.Name = "FormMain";
            this.Text = "MControl Charts";
            this.Controls.SetChildIndex(this.mcTaskBar1, 0);
            this.Controls.SetChildIndex(this.panelControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.mcTaskBar1)).EndInit();
            this.mcTaskBar1.ResumeLayout(false);
            this.mcTaskPanel1.ResumeLayout(false);
            this.mcTaskPanel2.ResumeLayout(false);
            this.mcTaskPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.WinForms.McTaskBar mcTaskBar1;
        private MControl.WinForms.McTaskPanel mcTaskPanel1;
        private MControl.WinForms.LinkLabelItem linkBar;
        private MControl.WinForms.LinkLabelItem linkBar3d;
        private MControl.WinForms.LinkLabelItem linkBarMulti3d;
        private MControl.WinForms.LinkLabelItem linkLine;
        private MControl.WinForms.LinkLabelItem linkPipes;
        private MControl.WinForms.LinkLabelItem linkLineMulti3d;
        private MControl.WinForms.McTaskPanel mcTaskPanel2;
        private MControl.WinForms.McTaskPanel mcTaskPanel3;
        private MControl.WinForms.LinkLabelItem linkSurface;
        private MControl.WinForms.LinkLabelItem linkSurface3d;
        private MControl.WinForms.LinkLabelItem linksurfaceMulti3d;
        private MControl.WinForms.LinkLabelItem linkPie;
        private MControl.WinForms.LinkLabelItem linkPie3d;
        private MControl.WinForms.LinkLabelItem linkPieChart;
        private MControl.WinForms.LinkLabelItem linkMeter;
        private MControl.WinForms.LinkLabelItem linkMeterRange;
        private MControl.WinForms.LinkLabelItem linkLed;
        private MControl.WinForms.LinkLabelItem linkUsage;
        private System.Windows.Forms.Panel panelControl;
        private MControl.WinForms.LinkLabelItem linkPieChartDisplay;
        private System.Windows.Forms.ImageList imageList1;

    }
}