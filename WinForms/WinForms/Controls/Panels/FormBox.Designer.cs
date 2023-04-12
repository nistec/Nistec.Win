namespace Nistec.WinForms
{
    partial class FormBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBox));
            this.btnColse = new Nistec.WinForms.McButton();
            this.btnResore = new Nistec.WinForms.McButton();
            this.btnMinimize = new Nistec.WinForms.McButton();
            this.SuspendLayout();
            // 
            // btnColse
            // 
            this.btnColse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnColse.Image = ((System.Drawing.Image)(resources.GetObject("btnColse.Image")));
            this.btnColse.Location = new System.Drawing.Point(43, 3);
            this.btnColse.Name = "btnColse";
            this.btnColse.Size = new System.Drawing.Size(20, 20);
            this.btnColse.TabIndex = 5;
            this.btnColse.ToolTipText = "Close";
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // btnResore
            // 
            this.btnResore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResore.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnResore.Image = ((System.Drawing.Image)(resources.GetObject("btnResore.Image")));
            this.btnResore.Location = new System.Drawing.Point(23, 3);
            this.btnResore.Name = "btnResore";
            this.btnResore.Size = new System.Drawing.Size(20, 20);
            this.btnResore.TabIndex = 6;
            this.btnResore.ToolTipText = "Resore";
            this.btnResore.Click += new System.EventHandler(this.btnResore_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimize.Image")));
            this.btnMinimize.Location = new System.Drawing.Point(3, 3);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(20, 20);
            this.btnMinimize.TabIndex = 7;
            this.btnMinimize.ToolTipText = "Minimize";
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // FormBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnResore);
            this.Controls.Add(this.btnColse);
            this.Name = "FormBox";
            this.Size = new System.Drawing.Size(66, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private McButton btnColse;
        private McButton btnResore;
        private McButton btnMinimize;

    }
}
