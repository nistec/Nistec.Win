namespace MControl.Charts
{
    partial class GraphControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphControl));
            this.graph = new MControl.Charts.Win.McGraph();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.barsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bars3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pipesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surface3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pie3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barsMulti3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineMulti3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surfaceMulti3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pieExpanded3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.graph.ChartType = MControl.Charts.ChartType.Bars;
            this.graph.ColorItems.AddRange(new MControl.Charts.ColorItem[] {
            ((MControl.Charts.ColorItem)(resources.GetObject("graph.ColorItems"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("graph.ColorItems1"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("graph.ColorItems2"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("graph.ColorItems3"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("graph.ColorItems4"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("graph.ColorItems5")))});
            this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph.FieldLabelFont = new System.Drawing.Font("Arial", 8F);
            this.graph.Initialize = false;
            this.graph.KeyItemsFont = new System.Drawing.Font("Arial", 8F);
            this.graph.Location = new System.Drawing.Point(4, 4);
            this.graph.Name = "graph";
            // 
            // 
            // 
            this.graph.PointImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.graph.PointImages.ImageSize = new System.Drawing.Size(16, 16);
            this.graph.PointImages.TransparentColor = System.Drawing.Color.Transparent;
            this.graph.ScaleBreakTop = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.graph.Size = new System.Drawing.Size(547, 442);
            this.graph.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barsToolStripMenuItem,
            this.linesToolStripMenuItem,
            this.surfaceToolStripMenuItem,
            this.pieToolStripMenuItem,
            this.bars3DToolStripMenuItem,
            this.pipesToolStripMenuItem,
            this.surface3DToolStripMenuItem,
            this.pie3DToolStripMenuItem,
            this.barsMulti3DToolStripMenuItem,
            this.lineMulti3DToolStripMenuItem,
            this.surfaceMulti3DToolStripMenuItem,
            this.pieExpanded3DToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(150, 268);
            // 
            // barsToolStripMenuItem
            // 
            this.barsToolStripMenuItem.Name = "barsToolStripMenuItem";
            this.barsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.barsToolStripMenuItem.Text = "Bars";
            // 
            // linesToolStripMenuItem
            // 
            this.linesToolStripMenuItem.Name = "linesToolStripMenuItem";
            this.linesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.linesToolStripMenuItem.Text = "Lines";
            // 
            // surfaceToolStripMenuItem
            // 
            this.surfaceToolStripMenuItem.Name = "surfaceToolStripMenuItem";
            this.surfaceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.surfaceToolStripMenuItem.Text = "Surface";
            // 
            // pieToolStripMenuItem
            // 
            this.pieToolStripMenuItem.Name = "pieToolStripMenuItem";
            this.pieToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pieToolStripMenuItem.Text = "Pie";
            // 
            // bars3DToolStripMenuItem
            // 
            this.bars3DToolStripMenuItem.Name = "bars3DToolStripMenuItem";
            this.bars3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bars3DToolStripMenuItem.Text = "Bars3D";
            // 
            // pipesToolStripMenuItem
            // 
            this.pipesToolStripMenuItem.Name = "pipesToolStripMenuItem";
            this.pipesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pipesToolStripMenuItem.Text = "Pipes";
            // 
            // surface3DToolStripMenuItem
            // 
            this.surface3DToolStripMenuItem.Name = "surface3DToolStripMenuItem";
            this.surface3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.surface3DToolStripMenuItem.Text = "Surface3D";
            // 
            // pie3DToolStripMenuItem
            // 
            this.pie3DToolStripMenuItem.Name = "pie3DToolStripMenuItem";
            this.pie3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pie3DToolStripMenuItem.Text = "Pie3D";
            // 
            // barsMulti3DToolStripMenuItem
            // 
            this.barsMulti3DToolStripMenuItem.Name = "barsMulti3DToolStripMenuItem";
            this.barsMulti3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.barsMulti3DToolStripMenuItem.Text = "BarsMulti3D";
            // 
            // lineMulti3DToolStripMenuItem
            // 
            this.lineMulti3DToolStripMenuItem.Name = "lineMulti3DToolStripMenuItem";
            this.lineMulti3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lineMulti3DToolStripMenuItem.Text = "LineMulti3D";
            // 
            // surfaceMulti3DToolStripMenuItem
            // 
            this.surfaceMulti3DToolStripMenuItem.Name = "surfaceMulti3DToolStripMenuItem";
            this.surfaceMulti3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.surfaceMulti3DToolStripMenuItem.Text = "SurfaceMulti3D";
            // 
            // pieExpanded3DToolStripMenuItem
            // 
            this.pieExpanded3DToolStripMenuItem.Name = "pieExpanded3DToolStripMenuItem";
            this.pieExpanded3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pieExpanded3DToolStripMenuItem.Text = "PieExpanded3D";
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
            // GraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.graph);
            this.Name = "GraphControl";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(555, 450);
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.Charts.Win.McGraph graph;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem barsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surfaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bars3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pipesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surface3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pie3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barsMulti3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineMulti3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surfaceMulti3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pieExpanded3DToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
    }
}
