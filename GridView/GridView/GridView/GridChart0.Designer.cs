namespace mControl.GridView
{
    partial class GridChart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelColors = new System.Windows.Forms.Panel();
            this.contextMenuChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmOffset = new System.Windows.Forms.ToolStripMenuItem();
            this.cmColors = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmLeaning = new System.Windows.Forms.ToolStripMenuItem();
            this.cmLeading15 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmLeading30 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmLeading45 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmLeading90 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDepth = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDepth0 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDepth25 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDepth50 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDepth75 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDepth100 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCaption = new System.Windows.Forms.Label();
            this.ctlPieChart = new mControl.Charts.CtlPieChart();
            this.cmRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelColors
            // 
            this.panelColors.BackColor = System.Drawing.Color.White;
            this.panelColors.ContextMenuStrip = this.contextMenuChart;
            this.panelColors.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelColors.Location = new System.Drawing.Point(0, 0);
            this.panelColors.Name = "panelColors";
            this.panelColors.Size = new System.Drawing.Size(120, 270);
            this.panelColors.TabIndex = 55;
            // 
            // contextMenuChart
            // 
            this.contextMenuChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmOffset,
            this.cmColors,
            this.cmPlay,
            this.toolStripSeparator1,
            this.cmLeaning,
            this.cmDepth,
            this.toolStripSeparator2,
            this.cmPrint,
            this.cmPreview,
            this.cmSave,
            this.cmRemove});
            this.contextMenuChart.Name = "contextMenuChart";
            this.contextMenuChart.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuChart.Size = new System.Drawing.Size(135, 214);
            this.contextMenuChart.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuChart_ItemClicked);
            // 
            // cmOffset
            // 
            this.cmOffset.Image = global::mControl.GridView.Properties.Resources.volume2;
            this.cmOffset.Name = "cmOffset";
            this.cmOffset.Size = new System.Drawing.Size(134, 22);
            this.cmOffset.Text = "Show Offset";
            // 
            // cmColors
            // 
            this.cmColors.Image = global::mControl.GridView.Properties.Resources.se2ae31;
            this.cmColors.Name = "cmColors";
            this.cmColors.Size = new System.Drawing.Size(134, 22);
            this.cmColors.Text = "Hide Colors";
            this.cmColors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmPlay
            // 
            this.cmPlay.Image = global::mControl.GridView.Properties.Resources.action;
            this.cmPlay.Name = "cmPlay";
            this.cmPlay.Size = new System.Drawing.Size(134, 22);
            this.cmPlay.Text = "Play";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // cmLeaning
            // 
            this.cmLeaning.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmLeading15,
            this.cmLeading30,
            this.cmLeading45,
            this.cmLeading90});
            this.cmLeaning.Image = global::mControl.GridView.Properties.Resources.rotate;
            this.cmLeaning.Name = "cmLeaning";
            this.cmLeaning.Size = new System.Drawing.Size(134, 22);
            this.cmLeaning.Text = "Leaning";
            this.cmLeaning.CheckedChanged += new System.EventHandler(this.cmLeaning_CheckedChanged);
            this.cmLeaning.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmLeaning_DropDownItemClicked);
            // 
            // cmLeading15
            // 
            this.cmLeading15.CheckOnClick = true;
            this.cmLeading15.Name = "cmLeading15";
            this.cmLeading15.Size = new System.Drawing.Size(86, 22);
            this.cmLeading15.Text = "15";
            // 
            // cmLeading30
            // 
            this.cmLeading30.Checked = true;
            this.cmLeading30.CheckOnClick = true;
            this.cmLeading30.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmLeading30.Name = "cmLeading30";
            this.cmLeading30.Size = new System.Drawing.Size(86, 22);
            this.cmLeading30.Text = "30";
            // 
            // cmLeading45
            // 
            this.cmLeading45.CheckOnClick = true;
            this.cmLeading45.Name = "cmLeading45";
            this.cmLeading45.Size = new System.Drawing.Size(86, 22);
            this.cmLeading45.Text = "45";
            // 
            // cmLeading90
            // 
            this.cmLeading90.CheckOnClick = true;
            this.cmLeading90.Name = "cmLeading90";
            this.cmLeading90.Size = new System.Drawing.Size(86, 22);
            this.cmLeading90.Text = "90";
            // 
            // cmDepth
            // 
            this.cmDepth.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmDepth0,
            this.cmDepth25,
            this.cmDepth50,
            this.cmDepth75,
            this.cmDepth100});
            this.cmDepth.Image = global::mControl.GridView.Properties.Resources.SameHeight;
            this.cmDepth.Name = "cmDepth";
            this.cmDepth.Size = new System.Drawing.Size(134, 22);
            this.cmDepth.Text = "Depth";
            this.cmDepth.CheckedChanged += new System.EventHandler(this.cmDepth_CheckedChanged);
            this.cmDepth.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmDepth_DropDownItemClicked);
            // 
            // cmDepth0
            // 
            this.cmDepth0.CheckOnClick = true;
            this.cmDepth0.Name = "cmDepth0";
            this.cmDepth0.Size = new System.Drawing.Size(152, 22);
            this.cmDepth0.Text = "0";
            // 
            // cmDepth25
            // 
            this.cmDepth25.CheckOnClick = true;
            this.cmDepth25.Name = "cmDepth25";
            this.cmDepth25.Size = new System.Drawing.Size(152, 22);
            this.cmDepth25.Text = "25";
            // 
            // cmDepth50
            // 
            this.cmDepth50.Checked = true;
            this.cmDepth50.CheckOnClick = true;
            this.cmDepth50.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmDepth50.Name = "cmDepth50";
            this.cmDepth50.Size = new System.Drawing.Size(152, 22);
            this.cmDepth50.Text = "50";
            // 
            // cmDepth75
            // 
            this.cmDepth75.CheckOnClick = true;
            this.cmDepth75.Name = "cmDepth75";
            this.cmDepth75.Size = new System.Drawing.Size(152, 22);
            this.cmDepth75.Text = "75";
            // 
            // cmDepth100
            // 
            this.cmDepth100.CheckOnClick = true;
            this.cmDepth100.Name = "cmDepth100";
            this.cmDepth100.Size = new System.Drawing.Size(152, 22);
            this.cmDepth100.Text = "100";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(131, 6);
            // 
            // cmPrint
            // 
            this.cmPrint.Image = global::mControl.GridView.Properties.Resources.printicon;
            this.cmPrint.Name = "cmPrint";
            this.cmPrint.Size = new System.Drawing.Size(134, 22);
            this.cmPrint.Text = "Print";
            // 
            // cmPreview
            // 
            this.cmPreview.Image = global::mControl.GridView.Properties.Resources.printp1;
            this.cmPreview.Name = "cmPreview";
            this.cmPreview.Size = new System.Drawing.Size(134, 22);
            this.cmPreview.Text = "Preview";
            // 
            // cmSave
            // 
            this.cmSave.Image = global::mControl.GridView.Properties.Resources.save;
            this.cmSave.Name = "cmSave";
            this.cmSave.Size = new System.Drawing.Size(134, 22);
            this.cmSave.Text = "Save";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCaption
            // 
            this.lblCaption.BackColor = System.Drawing.Color.White;
            this.lblCaption.ContextMenuStrip = this.contextMenuChart;
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblCaption.Location = new System.Drawing.Point(120, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(318, 22);
            this.lblCaption.TabIndex = 57;
            this.lblCaption.Text = "Grid Pie Chart";
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctlPieChart
            // 
            this.ctlPieChart.BackColor = System.Drawing.Color.White;
            this.ctlPieChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ctlPieChart.ContextMenuStrip = this.contextMenuChart;
            this.ctlPieChart.Depth = 40F;
            this.ctlPieChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPieChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlPieChart.ForeColor = System.Drawing.Color.Black;
            this.ctlPieChart.Location = new System.Drawing.Point(120, 0);
            this.ctlPieChart.Name = "ctlPieChart";
            this.ctlPieChart.Radius = 150F;
            this.ctlPieChart.Size = new System.Drawing.Size(318, 270);
            this.ctlPieChart.TabIndex = 56;
            // 
            // cmRemove
            // 
            this.cmRemove.Image = global::mControl.GridView.Properties.Resources.logoff_small;
            this.cmRemove.Name = "cmRemove";
            this.cmRemove.Size = new System.Drawing.Size(134, 22);
            this.cmRemove.Text = "Remove";
            // 
            // GridChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.ctlPieChart);
            this.Controls.Add(this.panelColors);
            this.Name = "GridChart";
            this.Size = new System.Drawing.Size(438, 270);
            this.contextMenuChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

         private System.Windows.Forms.Panel panelColors;
        private System.Windows.Forms.Timer timer1;
        internal mControl.Charts.CtlPieChart ctlPieChart;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.ContextMenuStrip contextMenuChart;
        private System.Windows.Forms.ToolStripMenuItem cmOffset;
        private System.Windows.Forms.ToolStripMenuItem cmColors;
        private System.Windows.Forms.ToolStripMenuItem cmPlay;
        private System.Windows.Forms.ToolStripMenuItem cmPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmLeaning;
        private System.Windows.Forms.ToolStripMenuItem cmDepth;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cmPreview;
        private System.Windows.Forms.ToolStripMenuItem cmSave;
        private System.Windows.Forms.ToolStripMenuItem cmLeading15;
        private System.Windows.Forms.ToolStripMenuItem cmLeading30;
        private System.Windows.Forms.ToolStripMenuItem cmLeading45;
        private System.Windows.Forms.ToolStripMenuItem cmLeading90;
        private System.Windows.Forms.ToolStripMenuItem cmDepth0;
        private System.Windows.Forms.ToolStripMenuItem cmDepth25;
        private System.Windows.Forms.ToolStripMenuItem cmDepth50;
        private System.Windows.Forms.ToolStripMenuItem cmDepth75;
        private System.Windows.Forms.ToolStripMenuItem cmDepth100;
        private System.Windows.Forms.ToolStripMenuItem cmRemove;
    }
}
