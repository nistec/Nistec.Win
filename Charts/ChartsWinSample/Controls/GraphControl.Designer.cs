namespace ChartsSample.Controls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphControl));
            this.mcGraph1 = new MControl.Charts.Win.McGraph();
            ((System.ComponentModel.ISupportInitialize)(this.mcGraph1)).BeginInit();
            this.SuspendLayout();
            // 
            // mcGraph1
            // 
            this.mcGraph1.ChartType = MControl.Charts.ChartType.Bars;
            this.mcGraph1.ColorItems.AddRange(new MControl.Charts.ColorItem[] {
            ((MControl.Charts.ColorItem)(resources.GetObject("mcGraph1.ColorItems"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("mcGraph1.ColorItems1"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("mcGraph1.ColorItems2"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("mcGraph1.ColorItems3"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("mcGraph1.ColorItems4"))),
            ((MControl.Charts.ColorItem)(resources.GetObject("mcGraph1.ColorItems5")))});
            this.mcGraph1.DataItems.AddRange(new MControl.Charts.DataItem[] {
            ((MControl.Charts.DataItem)(resources.GetObject("mcGraph1.DataItems"))),
            ((MControl.Charts.DataItem)(resources.GetObject("mcGraph1.DataItems1"))),
            ((MControl.Charts.DataItem)(resources.GetObject("mcGraph1.DataItems2")))});
            this.mcGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcGraph1.FieldLabelFont = new System.Drawing.Font("Arial", 8F);
            this.mcGraph1.Initialize = false;
            this.mcGraph1.KeyItemsFont = new System.Drawing.Font("Arial", 8F);
            this.mcGraph1.Location = new System.Drawing.Point(4, 4);
            this.mcGraph1.Name = "mcGraph1";
            // 
            // 
            // 
            this.mcGraph1.PointImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.mcGraph1.PointImages.ImageSize = new System.Drawing.Size(16, 16);
            this.mcGraph1.PointImages.TransparentColor = System.Drawing.Color.Transparent;
            this.mcGraph1.ScaleBreakTop = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.mcGraph1.Size = new System.Drawing.Size(547, 442);
            this.mcGraph1.TabIndex = 0;
            // 
            // GraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.mcGraph1);
            this.Name = "GraphControl";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(555, 450);
            ((System.ComponentModel.ISupportInitialize)(this.mcGraph1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.Charts.Win.McGraph mcGraph1;
    }
}
