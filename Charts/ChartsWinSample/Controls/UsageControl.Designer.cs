namespace ChartsSample.Controls
{
    partial class UsageControl
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
            this.mcUsage2 = new MControl.Charts.McUsage();
            this.mcUsageHistory2 = new MControl.Charts.McUsageHistory();
            this.ctlButton3 = new MControl.WinForms.McButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mcUsageHistory1 = new MControl.Charts.McUsageHistory();
            this.mcUsage1 = new MControl.Charts.McUsage();
            this.btnStop = new MControl.WinForms.McButton();
            this.SuspendLayout();
            // 
            // mcUsage2
            // 
            this.mcUsage2.BackColor = System.Drawing.Color.Black;
            this.mcUsage2.Location = new System.Drawing.Point(26, 12);
            this.mcUsage2.Maximum = 100;
            this.mcUsage2.Name = "mcUsage2";
            this.mcUsage2.Size = new System.Drawing.Size(41, 150);
            this.mcUsage2.TabIndex = 0;
            this.mcUsage2.Value1 = 100;
            this.mcUsage2.Value2 = 1;
            // 
            // mcUsageHistory2
            // 
            this.mcUsageHistory2.BackColor = System.Drawing.Color.Black;
            this.mcUsageHistory2.Location = new System.Drawing.Point(73, 12);
            this.mcUsageHistory2.Maximum = 100;
            this.mcUsageHistory2.Name = "mcUsageHistory2";
            this.mcUsageHistory2.Size = new System.Drawing.Size(449, 150);
            this.mcUsageHistory2.TabIndex = 1;
            // 
            // ctlButton3
            // 
            this.ctlButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton3.Location = new System.Drawing.Point(26, 390);
            this.ctlButton3.Name = "ctlButton3";
            this.ctlButton3.Size = new System.Drawing.Size(80, 20);
            this.ctlButton3.TabIndex = 12;
            this.ctlButton3.Text = "Run";
            this.ctlButton3.ToolTipText = "Run";
            this.ctlButton3.Click += new System.EventHandler(this.ctlButton3_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mcUsageHistory1
            // 
            this.mcUsageHistory1.BackColor = System.Drawing.Color.Black;
            this.mcUsageHistory1.Location = new System.Drawing.Point(73, 190);
            this.mcUsageHistory1.Maximum = 100;
            this.mcUsageHistory1.Name = "mcUsageHistory1";
            this.mcUsageHistory1.Size = new System.Drawing.Size(449, 150);
            this.mcUsageHistory1.TabIndex = 14;
            // 
            // mcUsage1
            // 
            this.mcUsage1.BackColor = System.Drawing.Color.Black;
            this.mcUsage1.Location = new System.Drawing.Point(26, 190);
            this.mcUsage1.Maximum = 100;
            this.mcUsage1.Name = "mcUsage1";
            this.mcUsage1.Size = new System.Drawing.Size(41, 150);
            this.mcUsage1.TabIndex = 13;
            this.mcUsage1.Value1 = 100;
            this.mcUsage1.Value2 = 1;
            // 
            // btnStop
            // 
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnStop.Location = new System.Drawing.Point(112, 390);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 20);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "Stop";
            this.btnStop.ToolTipText = "Run";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // UsageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.mcUsageHistory1);
            this.Controls.Add(this.mcUsage1);
            this.Controls.Add(this.ctlButton3);
            this.Controls.Add(this.mcUsageHistory2);
            this.Controls.Add(this.mcUsage2);
            this.Name = "UsageControl";
            this.Size = new System.Drawing.Size(555, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.Charts.McUsage mcUsage2;
        private MControl.Charts.McUsageHistory mcUsageHistory2;
        private MControl.WinForms.McButton ctlButton3;
        private System.Windows.Forms.Timer timer1;
        private MControl.Charts.McUsageHistory mcUsageHistory1;
        private MControl.Charts.McUsage mcUsage1;
        private MControl.WinForms.McButton btnStop;
    }
}
