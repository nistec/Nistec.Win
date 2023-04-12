namespace mControl.GridView
{
    partial class PropertyDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyDlg));
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.BackColor = System.Drawing.Color.White;
            this.caption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.Name = "caption";
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.Text = "Property Grid";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CommandsVisibleIfAvailable = false;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 56);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(345, 329);
            this.propertyGrid1.TabIndex = 2;
            // 
            // PropertyDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 385);
            this.Controls.Add(this.propertyGrid1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PropertyDlg";
            this.Text = "Property Dlg";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.propertyGrid1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}