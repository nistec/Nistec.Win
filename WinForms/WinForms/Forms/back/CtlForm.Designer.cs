namespace MControl.WinForms
{
    partial class McForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(McForm));
            this.caption = new MControl.WinForms.McCaption( ControlLayout.System,this);
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.BackColor = System.Drawing.Color.White;
            this.caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.caption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.caption.Location = new System.Drawing.Point(0, 0);
            this.caption.Name = "caption";
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.Text = "";
            // 
            // McForm
            // 
            //this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            //this.ClientSize = new System.Drawing.Size(292, 273);
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Controls.Add(this.caption);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "McForm";
            this.Text = "McForm";
            this.Controls.SetChildIndex(this.caption, 0);
            this.ResumeLayout(false);

        }

 
        #endregion
    }
}