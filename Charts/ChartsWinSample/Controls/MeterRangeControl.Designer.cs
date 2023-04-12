namespace ChartsSample.Controls
{
    partial class MeterRangeControl
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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.ctlButton3 = new MControl.WinForms.McButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ctlMeter2 = new MControl.Charts.McMeter();
            this.ctlMeter3 = new MControl.Charts.McMeter();
            this.ctlLed3 = new MControl.Charts.McLed();
            this.ctlMeter1 = new MControl.Charts.McMeter();
            this.btnStop = new MControl.WinForms.McButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(52, 272);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(237, 45);
            this.trackBar1.TabIndex = 8;
            // 
            // ctlButton3
            // 
            this.ctlButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton3.Location = new System.Drawing.Point(361, 284);
            this.ctlButton3.Name = "ctlButton3";
            this.ctlButton3.Size = new System.Drawing.Size(80, 20);
            this.ctlButton3.TabIndex = 11;
            this.ctlButton3.Text = "Run";
            this.ctlButton3.ToolTipText = "Run";
            this.ctlButton3.Click += new System.EventHandler(this.ctlButton3_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ctlMeter2
            // 
            this.ctlMeter2.Angle = 0;
            this.ctlMeter2.BackgoundColors.ColorAngle = 45;
            this.ctlMeter2.BackgoundColors.ColorEnd = System.Drawing.Color.AliceBlue;
            this.ctlMeter2.BackgoundColors.ColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ctlMeter2.BorderColors.ColorAngle = 45;
            this.ctlMeter2.BorderColors.ColorEnd = System.Drawing.Color.SlateGray;
            this.ctlMeter2.BorderColors.ColorStart = System.Drawing.Color.IndianRed;
            this.ctlMeter2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ctlMeter2.ForeColor = System.Drawing.Color.Blue;
            this.ctlMeter2.Location = new System.Drawing.Point(3, 132);
            this.ctlMeter2.MeterlineColor = System.Drawing.Color.Purple;
            this.ctlMeter2.MeterStyle = MControl.Charts.MeterStyle.LedBorder;
            this.ctlMeter2.Name = "ctlMeter2";
            this.ctlMeter2.ScaleInterval = 20;
            this.ctlMeter2.ScaleLedRed = 80;
            this.ctlMeter2.ScaleLedYellow = 70;
            this.ctlMeter2.ScaleMax = 100;
            this.ctlMeter2.ScaleMeterLineWidth = 30;
            this.ctlMeter2.ScaleMin = 0;
            this.ctlMeter2.ScalePieWidth = 24;
            this.ctlMeter2.ScaleValue = 0;
            this.ctlMeter2.Size = new System.Drawing.Size(191, 111);
            this.ctlMeter2.TabIndex = 5;
            // 
            // ctlMeter3
            // 
            this.ctlMeter3.Angle = 0;
            this.ctlMeter3.BackgoundColors.ColorAngle = 45;
            this.ctlMeter3.BackgoundColors.ColorEnd = System.Drawing.Color.AliceBlue;
            this.ctlMeter3.BackgoundColors.ColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ctlMeter3.BorderColors.ColorAngle = 45;
            this.ctlMeter3.BorderColors.ColorEnd = System.Drawing.Color.SlateGray;
            this.ctlMeter3.BorderColors.ColorStart = System.Drawing.Color.IndianRed;
            this.ctlMeter3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ctlMeter3.ForeColor = System.Drawing.Color.Blue;
            this.ctlMeter3.Location = new System.Drawing.Point(361, 132);
            this.ctlMeter3.MeterlineColor = System.Drawing.Color.Purple;
            this.ctlMeter3.MeterStyle = MControl.Charts.MeterStyle.LedBorder;
            this.ctlMeter3.Name = "ctlMeter3";
            this.ctlMeter3.ScaleInterval = 20;
            this.ctlMeter3.ScaleLedRed = 80;
            this.ctlMeter3.ScaleLedYellow = 50;
            this.ctlMeter3.ScaleMax = 100;
            this.ctlMeter3.ScaleMeterLineWidth = 30;
            this.ctlMeter3.ScaleMin = 0;
            this.ctlMeter3.ScalePieWidth = 24;
            this.ctlMeter3.ScaleValue = 0;
            this.ctlMeter3.Size = new System.Drawing.Size(191, 111);
            this.ctlMeter3.TabIndex = 9;
            // 
            // ctlLed3
            // 
            this.ctlLed3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlLed3.DrawBorder = true;
            this.ctlLed3.DrawText = true;
            this.ctlLed3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ctlLed3.Location = new System.Drawing.Point(240, 146);
            this.ctlLed3.Name = "ctlLed3";
            this.ctlLed3.ScaleHorizental = false;
            this.ctlLed3.ScaleLedCount = 10;
            this.ctlLed3.ScaleLedRed = 120;
            this.ctlLed3.ScaleLedYellow = 100;
            this.ctlLed3.ScaleMax = 150;
            this.ctlLed3.ScaleMin = 0;
            this.ctlLed3.ScaleValue = 0;
            this.ctlLed3.Size = new System.Drawing.Size(71, 97);
            this.ctlLed3.TabIndex = 7;
            // 
            // ctlMeter1
            // 
            this.ctlMeter1.Angle = 0;
            this.ctlMeter1.BackgoundColors.ColorAngle = 45;
            this.ctlMeter1.BackgoundColors.ColorEnd = System.Drawing.Color.Green;
            this.ctlMeter1.BackgoundColors.ColorStart = System.Drawing.Color.Blue;
            this.ctlMeter1.BorderColors.ColorAngle = 45;
            this.ctlMeter1.BorderColors.ColorEnd = System.Drawing.Color.SlateGray;
            this.ctlMeter1.BorderColors.ColorStart = System.Drawing.Color.IndianRed;
            this.ctlMeter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ctlMeter1.ForeColor = System.Drawing.Color.Blue;
            this.ctlMeter1.Location = new System.Drawing.Point(177, 29);
            this.ctlMeter1.MeterlineColor = System.Drawing.Color.Purple;
            this.ctlMeter1.MeterStyle = MControl.Charts.MeterStyle.LedFill;
            this.ctlMeter1.Name = "ctlMeter1";
            this.ctlMeter1.ScaleInterval = 20;
            this.ctlMeter1.ScaleLedRed = 80;
            this.ctlMeter1.ScaleLedYellow = 60;
            this.ctlMeter1.ScaleMax = 100;
            this.ctlMeter1.ScaleMeterLineWidth = 30;
            this.ctlMeter1.ScaleMin = 0;
            this.ctlMeter1.ScalePieWidth = 24;
            this.ctlMeter1.ScaleValue = 0;
            this.ctlMeter1.Size = new System.Drawing.Size(199, 111);
            this.ctlMeter1.TabIndex = 4;
            // 
            // btnStop
            // 
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnStop.Location = new System.Drawing.Point(447, 284);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 20);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "Stop";
            this.btnStop.ToolTipText = "Run";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // MeterRangeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.ctlMeter2);
            this.Controls.Add(this.ctlButton3);
            this.Controls.Add(this.ctlMeter3);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.ctlLed3);
            this.Controls.Add(this.ctlMeter1);
            this.Name = "MeterRangeControl";
            this.Size = new System.Drawing.Size(555, 388);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MControl.Charts.McMeter ctlMeter1;
        private MControl.Charts.McMeter ctlMeter2;
        private MControl.Charts.McLed ctlLed3;
        private System.Windows.Forms.TrackBar trackBar1;
        private MControl.Charts.McMeter ctlMeter3;
        private MControl.WinForms.McButton ctlButton3;
        private System.Windows.Forms.Timer timer1;
        private MControl.WinForms.McButton btnStop;
    }
}
