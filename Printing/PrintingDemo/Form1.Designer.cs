namespace PrintingDemo
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.mcPrintDocument1 = new MControl.Printing.McPrintDocument();
            this.mcPrintPreviewDialog1 = new MControl.Printing.McPrintPreviewDialog();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(26, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Export Dlg";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(26, 105);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 30);
            this.button3.TabIndex = 2;
            this.button3.Text = "Import Dlg";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(26, 141);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 30);
            this.button4.TabIndex = 3;
            this.button4.Text = "Print Dlg";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(139, 33);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(94, 30);
            this.button5.TabIndex = 4;
            this.button5.Text = "Show";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // mcPrintDocument1
            // 
            this.mcPrintDocument1.Body = null;
            this.mcPrintDocument1.DocumentUnit = System.Drawing.GraphicsUnit.Inch;
            this.mcPrintDocument1.PageFooter = null;
            this.mcPrintDocument1.PageFooterMaxHeight = 1F;
            this.mcPrintDocument1.PageHeader = null;
            this.mcPrintDocument1.PageHeaderMaxHeight = 1F;
            this.mcPrintDocument1.ReportSetting = null;
            this.mcPrintDocument1.ResetAfterPrint = true;
            this.mcPrintDocument1.SectionData = null;
            // 
            // mcPrintPreviewDialog1
            // 
            this.mcPrintPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.mcPrintPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.mcPrintPreviewDialog1.ClientSize = new System.Drawing.Size(1280, 740);
            this.mcPrintPreviewDialog1.Document = this.mcPrintDocument1;
            this.mcPrintPreviewDialog1.Enabled = true;
            this.mcPrintPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("mcPrintPreviewDialog1.Icon")));
            this.mcPrintPreviewDialog1.Location = new System.Drawing.Point(-4, -4);
            this.mcPrintPreviewDialog1.MaxPage = 1000;
            this.mcPrintPreviewDialog1.MinimizeBox = true;
            this.mcPrintPreviewDialog1.Name = "McPrintPreviewDialog";
            this.mcPrintPreviewDialog1.ShowInTaskbar = true;
            this.mcPrintPreviewDialog1.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Auto;
            this.mcPrintPreviewDialog1.Text = "MControl Print Preview";
            this.mcPrintPreviewDialog1.Visible = false;
            this.mcPrintPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(26, 188);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(94, 30);
            this.button6.TabIndex = 5;
            this.button6.Text = "Show Invoice";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 309);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.Printing.McPrintDocument mcPrintDocument1;
        private MControl.Printing.McPrintPreviewDialog mcPrintPreviewDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

