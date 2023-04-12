namespace ChartsSample.Controls
{
    partial class LedControl
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
            this.ctlLed1 = new MControl.Charts.McLed();
            this.ctlLed3 = new MControl.Charts.McLed();
            this.ctlLed2 = new MControl.Charts.McLed();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.mcLed1 = new MControl.Charts.McLed();
            this.ctlLed4 = new MControl.Charts.McLed();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlLed1
            // 
            this.ctlLed1.BackColor = System.Drawing.Color.White;
            this.ctlLed1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlLed1.DrawBorder = false;
            this.ctlLed1.DrawText = false;
            this.ctlLed1.Location = new System.Drawing.Point(151, 164);
            this.ctlLed1.Name = "ctlLed1";
            this.ctlLed1.ScaleHorizental = false;
            this.ctlLed1.ScaleLedCount = 10;
            this.ctlLed1.ScaleLedRed = 90;
            this.ctlLed1.ScaleLedYellow = 70;
            this.ctlLed1.ScaleMax = 100;
            this.ctlLed1.ScaleMin = 0;
            this.ctlLed1.ScaleValue = 0;
            this.ctlLed1.Size = new System.Drawing.Size(20, 115);
            this.ctlLed1.TabIndex = 3;
            // 
            // ctlLed3
            // 
            this.ctlLed3.BackColor = System.Drawing.Color.White;
            this.ctlLed3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlLed3.DrawBorder = false;
            this.ctlLed3.DrawText = false;
            this.ctlLed3.Location = new System.Drawing.Point(99, 164);
            this.ctlLed3.Name = "ctlLed3";
            this.ctlLed3.ScaleHorizental = false;
            this.ctlLed3.ScaleLedCount = 10;
            this.ctlLed3.ScaleLedRed = 120;
            this.ctlLed3.ScaleLedYellow = 100;
            this.ctlLed3.ScaleMax = 150;
            this.ctlLed3.ScaleMin = 0;
            this.ctlLed3.ScaleValue = 0;
            this.ctlLed3.Size = new System.Drawing.Size(20, 115);
            this.ctlLed3.TabIndex = 5;
            // 
            // ctlLed2
            // 
            this.ctlLed2.BackColor = System.Drawing.Color.White;
            this.ctlLed2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlLed2.DrawBorder = false;
            this.ctlLed2.DrawText = false;
            this.ctlLed2.Location = new System.Drawing.Point(125, 164);
            this.ctlLed2.Name = "ctlLed2";
            this.ctlLed2.ScaleHorizental = false;
            this.ctlLed2.ScaleLedCount = 10;
            this.ctlLed2.ScaleLedRed = 100;
            this.ctlLed2.ScaleLedYellow = 80;
            this.ctlLed2.ScaleMax = 120;
            this.ctlLed2.ScaleMin = 0;
            this.ctlLed2.ScaleValue = 0;
            this.ctlLed2.Size = new System.Drawing.Size(20, 115);
            this.ctlLed2.TabIndex = 4;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(99, 309);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(237, 45);
            this.trackBar1.TabIndex = 17;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // mcLed1
            // 
            this.mcLed1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mcLed1.DrawBorder = true;
            this.mcLed1.DrawText = true;
            this.mcLed1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.mcLed1.Location = new System.Drawing.Point(283, 164);
            this.mcLed1.Name = "mcLed1";
            this.mcLed1.ScaleHorizental = false;
            this.mcLed1.ScaleLedCount = 10;
            this.mcLed1.ScaleLedRed = 120;
            this.mcLed1.ScaleLedYellow = 100;
            this.mcLed1.ScaleMax = 150;
            this.mcLed1.ScaleMin = 0;
            this.mcLed1.ScaleValue = 0;
            this.mcLed1.Size = new System.Drawing.Size(71, 115);
            this.mcLed1.TabIndex = 16;
            // 
            // ctlLed4
            // 
            this.ctlLed4.BackColor = System.Drawing.Color.Black;
            this.ctlLed4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctlLed4.DrawBorder = false;
            this.ctlLed4.DrawText = false;
            this.ctlLed4.Location = new System.Drawing.Point(99, 109);
            this.ctlLed4.Name = "ctlLed4";
            this.ctlLed4.ScaleHorizental = true;
            this.ctlLed4.ScaleLedCount = 10;
            this.ctlLed4.ScaleLedRed = 80;
            this.ctlLed4.ScaleLedYellow = 70;
            this.ctlLed4.ScaleMax = 100;
            this.ctlLed4.ScaleMin = 0;
            this.ctlLed4.ScaleValue = 0;
            this.ctlLed4.Size = new System.Drawing.Size(255, 22);
            this.ctlLed4.TabIndex = 18;
            // 
            // LedControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ctlLed4);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.mcLed1);
            this.Controls.Add(this.ctlLed1);
            this.Controls.Add(this.ctlLed3);
            this.Controls.Add(this.ctlLed2);
            this.Name = "LedControl";
            this.Size = new System.Drawing.Size(555, 455);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MControl.Charts.McLed ctlLed1;
        private MControl.Charts.McLed ctlLed3;
        private MControl.Charts.McLed ctlLed2;
        private System.Windows.Forms.TrackBar trackBar1;
        private MControl.Charts.McLed mcLed1;
        private MControl.Charts.McLed ctlLed4;
    }
}
