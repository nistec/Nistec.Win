namespace Nistec.Printing.UI
{
    partial class ImportDlg
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
            this.mcImport1 = new Nistec.Printing.UI.McImport();
            this.SuspendLayout();
            // 
            // mcImport1
            // 
            this.mcImport1.BackColor = System.Drawing.Color.White;
            this.mcImport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcImport1.Location = new System.Drawing.Point(0, 0);
            this.mcImport1.Name = "mcImport1";
            this.mcImport1.Size = new System.Drawing.Size(375, 208);
            this.mcImport1.TabIndex = 0;
            // 
            // ImportDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 208);
            this.Controls.Add(this.mcImport1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImportDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nistec Import Dialog";
            this.ResumeLayout(false);

        }

        #endregion

        private McImport mcImport1;
    }
}