namespace mControl.Samples
{
    partial class CtlForm1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlForm1));
            this.vGrid1 = new mControl.GridView.VGrid();
            ((System.ComponentModel.ISupportInitialize)(this.vGrid1)).BeginInit();
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
            // vGrid1
            // 
            this.vGrid1.AllowAdd = false;
            this.vGrid1.BackColor = System.Drawing.Color.White;
            this.vGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vGrid1.ColumnKeyBackColor = System.Drawing.Color.White;
            this.vGrid1.ColumnKeyForeColor = System.Drawing.Color.Black;
            this.vGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vGrid1.ForeColor = System.Drawing.Color.Black;
            this.vGrid1.GridLineStyle = mControl.GridView.GridLineStyle.Solid;
            this.vGrid1.Location = new System.Drawing.Point(0, 56);
            this.vGrid1.Name = "vGrid1";
            this.vGrid1.PaintAlternating = false;
            this.vGrid1.Size = new System.Drawing.Size(308, 282);
            this.vGrid1.TabIndex = 2;
            // 
            // CtlForm1
            // 
            this.ClientSize = new System.Drawing.Size(308, 338);
            this.Controls.Add(this.vGrid1);
            this.Name = "CtlForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CtlForm1";
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.vGrid1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.vGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private mControl.GridView.VGrid vGrid1;

    }
}