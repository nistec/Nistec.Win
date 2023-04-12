namespace mControl.WinCtl.Dlg
{
    partial class MsgDialog
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
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.richText = new System.Windows.Forms.RichTextBox();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnOk);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 288);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(322, 29);
            this.panelBottom.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(250, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(67, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // richText
            // 
            this.richText.BackColor = System.Drawing.Color.White;
            this.richText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richText.Location = new System.Drawing.Point(0, 0);
            this.richText.Name = "richText";
            this.richText.ReadOnly = true;
            this.richText.Size = new System.Drawing.Size(322, 288);
            this.richText.TabIndex = 1;
            this.richText.Text = "";
            // 
            // MsgDialog
            // 
            this.AcceptButton = this.btnOk;
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(322, 317);
            this.ControlBox = false;
            this.Controls.Add(this.richText);
            this.Controls.Add(this.panelBottom);
            this.Name = "MsgDialog";
            this.Text = "Message Dialog";
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.RichTextBox richText;
        private System.Windows.Forms.Button btnOk;
    }
}