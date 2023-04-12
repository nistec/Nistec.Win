namespace MControl.Wizards.Forms
{
    partial class MMCForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MMCForm));
            this.ctlMMC = new MControl.Wizards.Controls.McMMC();
            this.SuspendLayout();
            // 
            // ctlMMC
            // 
            this.ctlMMC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlMMC.Location = new System.Drawing.Point(0, 0);
            this.ctlMMC.Name = "ctlMMC";
            this.ctlMMC.Padding = new System.Windows.Forms.Padding(2);
            this.ctlMMC.Size = new System.Drawing.Size(611, 378);
            this.ctlMMC.TabIndex = 0;
            // 
            // MMCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 378);
            this.Controls.Add(this.ctlMMC);
            this.Name = "MMCForm";
            this.Text = "MMC";
            this.Controls.SetChildIndex(this.ctlMMC, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.Wizards.Controls.McMMC ctlMMC;
    }
}