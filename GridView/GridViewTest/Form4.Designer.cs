namespace GridViewTest
{
    partial class Form4
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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(213)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.Goldenrod;
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.StyleGuideBase.ButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.DarkGoldenrod;
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.PaleGoldenrod;
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.Ivory;
            this.StyleGuideBase.ColorBrushLower = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(206)))), ((int)(((byte)(103)))));
            this.StyleGuideBase.ColorBrushUpper = System.Drawing.Color.Ivory;
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.Ivory;
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.StyleGuideBase.FormColor = System.Drawing.Color.Ivory;
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Goldenrod;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 29);
            this.button1.TabIndex = 10000;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(331, 280);
            this.Controls.Add(this.button1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Controls.SetChildIndex(this.button1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
    }
}