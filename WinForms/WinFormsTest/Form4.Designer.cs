namespace WinCtlTest
{
    partial class formBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formBase));
            this.mcNavigatore1 = new Nistec.WinForms.McNavigatore();
            this.SuspendLayout();
            // 
            // mcNavigatore1
            // 
            this.mcNavigatore1.Location = new System.Drawing.Point(140, 206);
            this.mcNavigatore1.Name = "mcNavigatore1";
            this.mcNavigatore1.ShowDelete = true;
            this.mcNavigatore1.ShowNew = true;
            this.mcNavigatore1.Size = new System.Drawing.Size(122, 23);
            this.mcNavigatore1.TabIndex = 9;
            this.mcNavigatore1.TabStop = false;
            // 
            // formBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.mcNavigatore1);
            this.Name = "formBase";
            this.Text = "Form4";
            this.Controls.SetChildIndex(this.mcNavigatore1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Nistec.WinForms.McNavigatore mcNavigatore1;

    }
}