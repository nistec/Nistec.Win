namespace Nistec.Printing.UI
{
    partial class ExportDlg
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
            this.mcExport1 = new Nistec.Printing.UI.McExport();
            this.SuspendLayout();
            // 
            // mcExport1
            // 
            this.mcExport1.BackColor = System.Drawing.Color.White;
            this.mcExport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcExport1.Location = new System.Drawing.Point(0, 0);
            this.mcExport1.Name = "mcExport1";
            this.mcExport1.Size = new System.Drawing.Size(375, 208);
            this.mcExport1.Source = null;
            this.mcExport1.TabIndex = 0;
            // 
            // ExportDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 208);
            this.Controls.Add(this.mcExport1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nistec Export Dialog";
            this.ResumeLayout(false);

        }

        #endregion

        private McExport mcExport1;
    }
}