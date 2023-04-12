namespace WinCtlTest
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.ctlButton1 = new Nistec.WinForms.McButton();
            this.SuspendLayout();
            // 
            // ctlButton1
            // 
            this.ctlButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton1.Location = new System.Drawing.Point(27, 73);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(62, 27);
            this.ctlButton1.TabIndex = 0;
            this.ctlButton1.Text = "ctlButton1";
            this.ctlButton1.ToolTipText = "ctlButton1";
            this.ctlButton1.Click += new System.EventHandler(this.ctlButton1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 242);
            this.Controls.Add(this.ctlButton1);
            this.Name = "Form3";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Form3";
            this.Controls.SetChildIndex(this.ctlButton1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Nistec.WinForms.McButton ctlButton1;
    }
}