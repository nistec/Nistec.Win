namespace ChartsSample.Controls
{
    partial class PieChart
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
            this.trkRadius = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trkThickness = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trkIncline = new System.Windows.Forms.TrackBar();
            this.trkRotation = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.ctlButton1 = new MControl.WinForms.McButton();
            this.ctlButton2 = new MControl.WinForms.McButton();
            this.trkSurfaceTransparency = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ctlPieChart1 = new MControl.Charts.McPieChart();
            ((System.ComponentModel.ISupportInitialize)(this.trkRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkIncline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRotation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSurfaceTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // trkRadius
            // 
            this.trkRadius.Location = new System.Drawing.Point(385, 212);
            this.trkRadius.Maximum = 1500;
            this.trkRadius.Minimum = 10;
            this.trkRadius.Name = "trkRadius";
            this.trkRadius.Size = new System.Drawing.Size(165, 45);
            this.trkRadius.TabIndex = 44;
            this.trkRadius.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkRadius.Value = 10;
            this.trkRadius.Scroll += new System.EventHandler(this.trkRadius_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(383, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Radius";
            // 
            // trkThickness
            // 
            this.trkThickness.Location = new System.Drawing.Point(386, 161);
            this.trkThickness.Maximum = 200;
            this.trkThickness.Name = "trkThickness";
            this.trkThickness.Size = new System.Drawing.Size(165, 45);
            this.trkThickness.TabIndex = 42;
            this.trkThickness.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkThickness.Value = 1;
            this.trkThickness.Scroll += new System.EventHandler(this.trkThickness_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(383, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Depth";
            // 
            // trkIncline
            // 
            this.trkIncline.Location = new System.Drawing.Point(386, 110);
            this.trkIncline.Maximum = 89;
            this.trkIncline.Minimum = 1;
            this.trkIncline.Name = "trkIncline";
            this.trkIncline.Size = new System.Drawing.Size(165, 45);
            this.trkIncline.TabIndex = 40;
            this.trkIncline.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkIncline.Value = 1;
            this.trkIncline.Scroll += new System.EventHandler(this.trkIncline_Scroll);
            // 
            // trkRotation
            // 
            this.trkRotation.Location = new System.Drawing.Point(386, 61);
            this.trkRotation.Maximum = 360;
            this.trkRotation.Name = "trkRotation";
            this.trkRotation.Size = new System.Drawing.Size(165, 45);
            this.trkRotation.TabIndex = 38;
            this.trkRotation.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkRotation.Scroll += new System.EventHandler(this.trkRotation_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Rotation";
            // 
            // ctlButton1
            // 
            this.ctlButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton1.Location = new System.Drawing.Point(384, 365);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(80, 20);
            this.ctlButton1.TabIndex = 45;
            this.ctlButton1.Text = "Save as";
            this.ctlButton1.ToolTipText = "Save as";
            this.ctlButton1.Click += new System.EventHandler(this.ctlButton1_Click);
            // 
            // ctlButton2
            // 
            this.ctlButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton2.Location = new System.Drawing.Point(471, 365);
            this.ctlButton2.Name = "ctlButton2";
            this.ctlButton2.Size = new System.Drawing.Size(80, 20);
            this.ctlButton2.TabIndex = 46;
            this.ctlButton2.Text = "Print";
            this.ctlButton2.ToolTipText = "Print";
            this.ctlButton2.Click += new System.EventHandler(this.ctlButton2_Click);
            // 
            // trkSurfaceTransparency
            // 
            this.trkSurfaceTransparency.Location = new System.Drawing.Point(388, 263);
            this.trkSurfaceTransparency.Maximum = 100;
            this.trkSurfaceTransparency.Name = "trkSurfaceTransparency";
            this.trkSurfaceTransparency.Size = new System.Drawing.Size(162, 45);
            this.trkSurfaceTransparency.TabIndex = 48;
            this.trkSurfaceTransparency.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkSurfaceTransparency.Value = 100;
            this.trkSurfaceTransparency.Scroll += new System.EventHandler(this.trkSurfaceTransparency_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(384, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Transparency";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(384, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "Leaning";
            // 
            // ctlPieChart1
            // 
            this.ctlPieChart1.Depth = 50F;
            this.ctlPieChart1.Location = new System.Drawing.Point(0, 48);
            this.ctlPieChart1.Name = "ctlPieChart1";
            this.ctlPieChart1.Radius = 150F;
            this.ctlPieChart1.Size = new System.Drawing.Size(377, 314);
            this.ctlPieChart1.TabIndex = 0;
            this.ctlPieChart1.Text = "ctlPieChart1";
            // 
            // PieChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ctlButton2);
            this.Controls.Add(this.trkSurfaceTransparency);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ctlButton1);
            this.Controls.Add(this.trkRotation);
            this.Controls.Add(this.trkRadius);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trkThickness);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trkIncline);
            this.Controls.Add(this.ctlPieChart1);
            this.Controls.Add(this.label1);
            this.Name = "PieChart";
            this.Size = new System.Drawing.Size(559, 450);
            ((System.ComponentModel.ISupportInitialize)(this.trkRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkIncline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRotation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSurfaceTransparency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MControl.Charts.McPieChart ctlPieChart1;
        private System.Windows.Forms.TrackBar trkRadius;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trkThickness;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trkIncline;
        private System.Windows.Forms.TrackBar trkRotation;
        private System.Windows.Forms.Label label1;
        private MControl.WinForms.McButton ctlButton1;
        private MControl.WinForms.McButton ctlButton2;
        private System.Windows.Forms.TrackBar trkSurfaceTransparency;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
