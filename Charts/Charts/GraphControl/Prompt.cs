namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Prompt : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private IContainer components=null;
        private float f;
        private System.Windows.Forms.Label label1;
        private int n = 1;
        private TextBox textBox1;
        private bool useInt = true;

        public Prompt(string message, bool useint)
        {
            this.InitializeComponent();
            this.Text = message;
            this.useInt = useint;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.useInt)
            {
                try
                {
                    this.n = int.Parse(this.textBox1.Text);
                    if (this.n <= 0)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid pozitive integer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                try
                {
                    this.f = float.Parse(this.textBox1.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid float!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter num:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(153, 20);
            this.textBox1.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(93, 51);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 51);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // Prompt
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(181, 88);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Prompt";
            this.Text = "Chart Control";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public float FloatNum
        {
            get
            {
                return this.f;
            }
            set
            {
                this.f = value;
            }
        }

        public int IntNum
        {
            get
            {
                return this.n;
            }
            set
            {
                this.n = value;
            }
        }
    }
}

