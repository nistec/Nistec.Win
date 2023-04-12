namespace mControl.Samples
{
    partial class CtlForm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlForm2));
            this.gridVirtual1 = new mControl.GridView.GridVirtual();
            ((System.ComponentModel.ISupportInitialize)(this.gridVirtual1)).BeginInit();
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
            this.caption.Text = "Caption Control";
            // 
            // gridVirtual1
            // 
            this.gridVirtual1.BackColor = System.Drawing.Color.White;
            this.gridVirtual1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridVirtual1.Dimension = new System.Drawing.Size(10, 20);
            this.gridVirtual1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVirtual1.ForeColor = System.Drawing.Color.Black;
            this.gridVirtual1.GridLineStyle = mControl.GridView.GridLineStyle.Solid;
            this.gridVirtual1.Location = new System.Drawing.Point(0, 56);
            this.gridVirtual1.MappingName = "VirtualGrid";
            this.gridVirtual1.Name = "gridVirtual1";
            this.gridVirtual1.PaintAlternating = false;
            this.gridVirtual1.Size = new System.Drawing.Size(503, 279);
            this.gridVirtual1.TabIndex = 2;
            // 
            // CtlForm2
            // 
            this.ClientSize = new System.Drawing.Size(503, 335);
            this.Controls.Add(this.gridVirtual1);
            this.Name = "CtlForm2";
            this.Text = "CtlForm2";
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.gridVirtual1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridVirtual1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private mControl.GridView.GridVirtual gridVirtual1;

    }
}