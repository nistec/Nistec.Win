namespace Nistec.Charts.Web
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class XmlEditorForm : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private IContainer components=null;
        private System.Windows.Forms.Label label1;
        private LinkLabel linkLabel1;
        private OpenFileDialog openFileDialog1;
        internal RichTextBox richTextBox1;

        public XmlEditorForm()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.richTextBox1 = new RichTextBox();
            this.openFileDialog1 = new OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new LinkLabel();
            base.SuspendLayout();
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            //this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(480, 0x167);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor=(true);
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            //this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x231, 0x167);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor=(true);
            this.richTextBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.richTextBox1.Location = new Point(12, 0x19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new Size(0x270, 0x148);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.label1.AutoSize=(true);
            this.label1.Location = new Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Xml Text:";
            this.linkLabel1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.linkLabel1.AutoSize=(true);
            this.linkLabel1.Location = new Point(12, 0x171);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new Size(0x34, 13);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Open File";
            this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions=(new SizeF(6f, 13f));
            //base.AutoScaleMode = (AutoScaleMode)(1);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x288, 0x18a);
            base.Controls.Add(this.linkLabel1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.richTextBox1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.MinimizeBox = false;
            base.Name = "XmlEditorForm";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Xml Editor ";
            base.TopMost = true;
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.richTextBox1.Text = File.ReadAllText(this.openFileDialog1.FileName);
            }
        }
    }
}

