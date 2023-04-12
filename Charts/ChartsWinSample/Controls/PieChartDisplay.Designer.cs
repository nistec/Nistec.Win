namespace ChartsSample.Controls
{
    partial class PieChartDisplay
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ctlPieChart1 = new MControl.Charts.McPieChart();
            this.btnStop = new MControl.WinForms.McButton();
            this.ctlButton3 = new MControl.WinForms.McButton();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ctlPieChart1
            // 
            this.ctlPieChart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ctlPieChart1.Depth = 50F;
            this.ctlPieChart1.Location = new System.Drawing.Point(12, 23);
            this.ctlPieChart1.Name = "ctlPieChart1";
            this.ctlPieChart1.Radius = 150F;
            this.ctlPieChart1.Size = new System.Drawing.Size(536, 354);
            this.ctlPieChart1.TabIndex = 0;
            this.ctlPieChart1.Text = "ctlPieChart1";
            // 
            // btnStop
            // 
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnStop.Location = new System.Drawing.Point(111, 395);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 20);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "Stop";
            this.btnStop.ToolTipText = "Run";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // ctlButton3
            // 
            this.ctlButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton3.Location = new System.Drawing.Point(25, 395);
            this.ctlButton3.Name = "ctlButton3";
            this.ctlButton3.Size = new System.Drawing.Size(80, 20);
            this.ctlButton3.TabIndex = 13;
            this.ctlButton3.Text = "Run";
            this.ctlButton3.ToolTipText = "Run";
            this.ctlButton3.Click += new System.EventHandler(this.ctlButton3_Click);
            // 
            // PieChartDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.ctlButton3);
            this.Controls.Add(this.ctlPieChart1);
            this.Name = "PieChartDisplay";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(555, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.Charts.McPieChart ctlPieChart1;
        private System.Windows.Forms.Timer timer1;
        private MControl.WinForms.McButton btnStop;
        private MControl.WinForms.McButton ctlButton3;
    }
}
