namespace mControl.GridView
{
    partial class GridChartDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridChartDlg));
            this.ctlContextMenu1 = new mControl.WinCtl.Controls.CtlContextMenu();
            this.mnPlay = new System.Windows.Forms.MenuItem();
            this.mnPrint = new System.Windows.Forms.MenuItem();
            this.mnOffset = new System.Windows.Forms.MenuItem();
            this.ctlPieChart = new mControl.GridView.GridChart();
            this.mnColors = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.caption.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Visual;
            this.caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.caption.Location = new System.Drawing.Point(0, 0);
            this.caption.Name = "caption";
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.SubTitleColor = System.Drawing.SystemColors.ControlText;
            this.caption.Text = "Grid Chart";
            // 
            // ctlContextMenu1
            // 
            this.ctlContextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnPlay,
            this.mnPrint,
            this.mnOffset,
            this.mnColors});
            // 
            // mnPlay
            // 
            this.ctlContextMenu1.SetDraw(this.mnPlay, true);
            this.mnPlay.Index = 0;
            this.mnPlay.Text = "Play";
            this.mnPlay.Click += new System.EventHandler(this.mnPlay_Click);
            // 
            // mnPrint
            // 
            this.ctlContextMenu1.SetDraw(this.mnPrint, true);
            this.mnPrint.Index = 1;
            this.mnPrint.Text = "Print";
            this.mnPrint.Click += new System.EventHandler(this.mnPrint_Click);
            // 
            // mnOffset
            // 
            this.ctlContextMenu1.SetDraw(this.mnOffset, true);
            this.mnOffset.Index = 2;
            this.mnOffset.Text = "Show Offset";
            this.mnOffset.Click += new System.EventHandler(this.mnOffset_Click);
            // 
            // ctlPieChart
            // 
            this.ctlPieChart.BackColor = System.Drawing.Color.White;
            this.ctlPieChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ctlPieChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPieChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlPieChart.ForeColor = System.Drawing.Color.Black;
            this.ctlPieChart.Location = new System.Drawing.Point(0, 60);
            this.ctlPieChart.Name = "ctlPieChart";
            this.ctlPieChart.ShowPanelColors = true;
            this.ctlPieChart.Size = new System.Drawing.Size(346, 288);
            this.ctlPieChart.TabIndex = 10;
            // 
            // mnColors
            // 
            this.ctlContextMenu1.SetDraw(this.mnColors, true);
            this.mnColors.Index = 3;
            this.mnColors.Text = "Hide Colors";
            this.mnColors.Click += new System.EventHandler(this.mnColors_Click);
            // 
            // GridChartDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 348);
            this.ContextMenu = this.ctlContextMenu1;
            this.Controls.Add(this.ctlPieChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GridChartDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grid Chart";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.ctlPieChart, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private mControl.WinCtl.Controls.CtlContextMenu ctlContextMenu1;
        private System.Windows.Forms.MenuItem mnPlay;
        private System.Windows.Forms.MenuItem mnPrint;
        private mControl.GridView.GridChart ctlPieChart;
        private System.Windows.Forms.MenuItem mnOffset;
        private System.Windows.Forms.MenuItem mnColors;
    }
}