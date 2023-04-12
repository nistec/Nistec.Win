namespace Nistec.Printing.UI
{
    partial class PrintDlg
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
            this.mcPrint1 = new Nistec.Printing.UI.McPrint();
            this.SuspendLayout();
            // 
            // mcPrint1
            // 
            this.mcPrint1.BackColor = System.Drawing.Color.White;
            this.mcPrint1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcPrint1.Document = null;
            this.mcPrint1.Location = new System.Drawing.Point(0, 0);
            this.mcPrint1.Name = "mcPrint1";
            this.mcPrint1.Size = new System.Drawing.Size(375, 208);
            this.mcPrint1.TabIndex = 0;
            // 
            // PrintDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 208);
            this.Controls.Add(this.mcPrint1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PrintDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nistec Print Dialog";
            this.ResumeLayout(false);

        }

        #endregion

        private McPrint mcPrint1;
    }
}