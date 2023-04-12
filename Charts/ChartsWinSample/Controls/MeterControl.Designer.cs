namespace ChartsSample.Controls
{
    partial class MeterControl
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
            this.ctlLed4 = new MControl.Charts.McLed();
            this.ctlMeter4 = new MControl.Charts.McMeter();
            this.ctlMeter3 = new MControl.Charts.McMeter();
            this.ctlMeter2 = new MControl.Charts.McMeter();
            this.btnStop = new MControl.WinForms.McButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(14, 242);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(237, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // ctlButton3
            // 
            this.ctlButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton3.Location = new System.Drawing.Point(330, 255);
            this.ctlButton3.Name = "ctlButton3";
            this.ctlButton3.Size = new System.Drawing.Size(80, 20);
            this.ctlButton3.TabIndex = 10;
            this.ctlButton3.Text = "Run";
            this.ctlButton3.ToolTipText = "Run";
            this.ctlButton3.Click += new System.EventHandler(this.ctlButton3_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ctlLed4
            // 
            this.ctlLed4.BackColor = System.Drawing.Color.Black;
            this.ctlLed4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctlLed4.DrawBorder = false;
            this.ctlLed4.DrawText = false;
            this.ctlLed4.Location = new System.Drawing.Point(220, 68);
            this.ctlLed4.Name = "ctlLed4";
            this.ctlLed4.ScaleHorizental = true;
            this.ctlLed4.ScaleLedCount = 10;
            this.ctlLed4.ScaleLedRed = 80;
            this.ctlLed4.ScaleLedYellow = 70;
            this.ctlLed4.ScaleMax = 100;
            this.ctlLed4.ScaleMin = 0;
            this.ctlLed4.ScaleValue = 0;
            this.ctlLed4.Size = new System.Drawing.Size(119, 22);
            this.ctlLed4.TabIndex = 11;
            // 
            // ctlMeter4
            // 
            this.ctlMeter4.Angle = 0;
            this.ctlMeter4.BackgoundColors.ColorAngle = 45;
            this.ctlMeter4.BackgoundColors.ColorEnd = System.Drawing.Color.Green;
            this.ctlMeter4.BackgoundColors.ColorStart = System.Drawing.Color.Blue;
            this.ctlMeter4.BorderColors.ColorAngle = 45;
            this.ctlMeter4.BorderColors.ColorEnd = System.Drawing.Color.SlateGray;
            this.ctlMeter4.BorderColors.ColorStart = System.Drawing.Color.IndianRed;
            this.ctlMeter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ctlMeter4.ForeColor = System.Drawing.Color.Lavender;
            this.ctlMeter4.Location = new System.Drawing.Point(214, 109);
            this.ctlMeter4.MeterlineColor = System.Drawing.Color.Purple;
            this.ctlMeter4.MeterStyle = MControl.Charts.MeterStyle.Flat;
            this.ctlMeter4.Name = "ctlMeter4";
            this.ctlMeter4.ScaleInterval = 20;
            this.ctlMeter4.ScaleLedRed = 80;
            this.ctlMeter4.ScaleLedYellow = 70;
            this.ctlMeter4.ScaleMax = 60;
            this.ctlMeter4.ScaleMeterLineWidth = 30;
            this.ctlMeter4.ScaleMin = 0;
            this.ctlMeter4.ScalePieWidth = 20;
            this.ctlMeter4.ScaleValue = 0;
            this.ctlMeter4.Size = new System.Drawing.Size(125, 74);
            this.ctlMeter4.TabIndex = 7;
            // 
            // ctlMeter3
            // 
            this.ctlMeter3.Angle = 0;
            this.ctlMeter3.BackgoundColors.ColorAngle = 45;
            this.ctlMeter3.BackgoundColors.ColorEnd = System.Drawing.Color.Green;
            this.ctlMeter3.BackgoundColors.ColorStart = System.Drawing.Color.Blue;
            this.ctlMeter3.BorderColors.ColorAngle = 45;
            this.ctlMeter3.BorderColors.ColorEnd = System.Drawing.Color.SlateGray;
            this.ctlMeter3.BorderColors.ColorStart = System.Drawing.Color.IndianRed;
            this.ctlMeter3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ctlMeter3.ForeColor = System.Drawing.Color.Lavender;
            this.ctlMeter3.Location = new System.Drawing.Point(3, 68);
            this.ctlMeter3.MeterlineColor = System.Drawing.Color.Purple;
            this.ctlMeter3.MeterStyle = MControl.Charts.MeterStyle.Flat;
            this.ctlMeter3.Name = "ctlMeter3";
            this.ctlMeter3.ScaleInterval = 20;
            this.ctlMeter3.ScaleLedRed = 80;
            this.ctlMeter3.ScaleLedYellow = 70;
            this.ctlMeter3.ScaleMax = 100;
            this.ctlMeter3.ScaleMeterLineWidth = 6;
            this.ctlMeter3.ScaleMin = 0;
            this.ctlMeter3.ScalePieWidth = 24;
            this.ctlMeter3.ScaleValue = 0;
            this.ctlMeter3.Size = new System.Drawing.Size(202, 115);
            this.ctlMeter3.TabIndex = 6;
            // 
            // ctlMeter2
            // 
            this.ctlMeter2.Angle = 0;
            this.ctlMeter2.BackgoundColors.ColorAngle = 45;
            this.ctlMeter2.BackgoundColors.ColorEnd = System.Drawing.Color.Green;
            this.ctlMeter2.BackgoundColors.ColorStart = System.Drawing.Color.Blue;
            this.ctlMeter2.BorderColors.ColorAngle = 45;
            this.ctlMeter2.BorderColors.ColorEnd = System.Drawing.Color.SlateGray;
            this.ctlMeter2.BorderColors.ColorStart = System.Drawing.Color.IndianRed;
            this.ctlMeter2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ctlMeter2.ForeColor = System.Drawing.Color.Lavender;
            this.ctlMeter2.Location = new System.Drawing.Point(350, 68);
            this.ctlMeter2.MeterlineColor = System.Drawing.Color.Purple;
            this.ctlMeter2.MeterStyle = MControl.Charts.MeterStyle.Flat;
            this.ctlMeter2.Name = "ctlMeter2";
            this.ctlMeter2.ScaleInterval = 20;
            this.ctlMeter2.ScaleLedRed = 80;
            this.ctlMeter2.ScaleLedYellow = 70;
            this.ctlMeter2.ScaleMax = 160;
            this.ctlMeter2.ScaleMeterLineWidth = 30;
            this.ctlMeter2.ScaleMin = 40;
            this.ctlMeter2.ScalePieWidth = 24;
            this.ctlMeter2.ScaleValue = 0;
            this.ctlMeter2.Size = new System.Drawing.Size(202, 115);
            this.ctlMeter2.TabIndex = 5;
            // 
            // btnStop
            // 
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnStop.Location = new System.Drawing.Point(416, 255);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 20);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "Stop";
            this.btnStop.ToolTipText = "Run";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // MeterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.ctlLed4);
            this.Controls.Add(this.ctlMeter4);
            this.Controls.Add(this.ctlButton3);
            this.Controls.Add(this.ctlMeter3);
            this.Controls.Add(this.ctlMeter2);
            this.Controls.Add(this.trackBar1);
            this.Name = "MeterControl";
            this.Size = new System.Drawing.Size(555, 455);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private MControl.Charts.McMeter ctlMeter2;
        private MControl.Charts.McMeter ctlMeter3;
        private MControl.Charts.McMeter ctlMeter4;
        private MControl.WinForms.McButton ctlButton3;
        private System.Windows.Forms.Timer timer1;
        private MControl.Charts.McLed ctlLed4;
        private MControl.WinForms.McButton btnStop;
    }
}
