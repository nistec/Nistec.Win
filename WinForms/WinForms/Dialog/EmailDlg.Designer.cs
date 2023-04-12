

namespace Nistec.WinForms
{
    partial class EmailDlg
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
            this.ctlFrom = new Nistec.WinForms.McTextBox();
            this.btnOk = new Nistec.WinForms.McButton();
            this.btnExit = new Nistec.WinForms.McButton();
            this.ctlLabel6 = new Nistec.WinForms.McLabel();
            this.ctlLabel4 = new Nistec.WinForms.McLabel();
            this.ctlLabel1 = new Nistec.WinForms.McLabel();
            this.ctlSubject = new Nistec.WinForms.McTextBox();
            this.ctlTo = new Nistec.WinForms.McComboBox();
            this.ctlBody = new Nistec.WinForms.McTextBox();
            this.ctlLabel2 = new Nistec.WinForms.McLabel();
            this.txtFileName = new Nistec.WinForms.McTextBox();
            this.btnFile = new Nistec.WinForms.McButton();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(126)))), ((int)(((byte)(177)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Desktop;
            // 
            // ctlFrom
            // 
            this.ctlFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlFrom.Location = new System.Drawing.Point(86, 55);
            this.ctlFrom.Name = "ctlFrom";
            this.ctlFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlFrom.Size = new System.Drawing.Size(327, 20);
            this.ctlFrom.StylePainter = this.StyleGuideBase;
            this.ctlFrom.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.Location = new System.Drawing.Point(277, 220);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 24);
            this.btnOk.StylePainter = this.StyleGuideBase;
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.ToolTipText = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(349, 220);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 24);
            this.btnExit.StylePainter = this.StyleGuideBase;
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.ToolTipText = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ctlLabel6
            // 
            this.ctlLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel6.Location = new System.Drawing.Point(14, 55);
            this.ctlLabel6.Name = "ctlLabel6";
            this.ctlLabel6.Size = new System.Drawing.Size(64, 20);
            this.ctlLabel6.StylePainter = this.StyleGuideBase;
            this.ctlLabel6.Text = "From";
            this.ctlLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel4
            // 
            this.ctlLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel4.Location = new System.Drawing.Point(14, 79);
            this.ctlLabel4.Name = "ctlLabel4";
            this.ctlLabel4.Size = new System.Drawing.Size(64, 20);
            this.ctlLabel4.StylePainter = this.StyleGuideBase;
            this.ctlLabel4.Text = "To";
            this.ctlLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel1
            // 
            this.ctlLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel1.Location = new System.Drawing.Point(14, 105);
            this.ctlLabel1.Name = "ctlLabel1";
            this.ctlLabel1.Size = new System.Drawing.Size(64, 20);
            this.ctlLabel1.StylePainter = this.StyleGuideBase;
            this.ctlLabel1.Text = "Subject";
            this.ctlLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlSubject
            // 
            this.ctlSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlSubject.Location = new System.Drawing.Point(86, 105);
            this.ctlSubject.Name = "ctlSubject";
            this.ctlSubject.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlSubject.Size = new System.Drawing.Size(329, 20);
            this.ctlSubject.StylePainter = this.StyleGuideBase;
            this.ctlSubject.TabIndex = 2;
            // 
            // ctlTo
            // 
            this.ctlTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlTo.ButtonToolTip = "";
            this.ctlTo.DropDownWidth = 200;
            this.ctlTo.IntegralHeight = false;
            this.ctlTo.Location = new System.Drawing.Point(86, 79);
            this.ctlTo.Name = "ctlTo";
            this.ctlTo.Size = new System.Drawing.Size(327, 20);
            this.ctlTo.StylePainter = this.StyleGuideBase;
            this.ctlTo.TabIndex = 1;
            // 
            // ctlBody
            // 
            this.ctlBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlBody.Location = new System.Drawing.Point(14, 131);
            this.ctlBody.Multiline = true;
            this.ctlBody.Name = "ctlBody";
            this.ctlBody.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlBody.Size = new System.Drawing.Size(399, 83);
            this.ctlBody.StylePainter = this.StyleGuideBase;
            this.ctlBody.TabIndex = 3;
            // 
            // ctlLabel2
            // 
            this.ctlLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ctlLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel2.Location = new System.Drawing.Point(14, 220);
            this.ctlLabel2.Name = "ctlLabel2";
            this.ctlLabel2.Size = new System.Drawing.Size(52, 20);
            this.ctlLabel2.StylePainter = this.StyleGuideBase;
            this.ctlLabel2.Text = "Attach";
            this.ctlLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFileName.BackColor = System.Drawing.Color.White;
            this.txtFileName.ForeColor = System.Drawing.Color.Black;
            this.txtFileName.Location = new System.Drawing.Point(65, 220);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFileName.Size = new System.Drawing.Size(158, 20);
            this.txtFileName.StylePainter = this.StyleGuideBase;
            this.txtFileName.TabIndex = 13;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFile.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFile.Location = new System.Drawing.Point(220, 220);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(21, 20);
            this.btnFile.StylePainter = this.StyleGuideBase;
            this.btnFile.TabIndex = 14;
            this.btnFile.Text = "...";
            this.btnFile.ToolTipText = "File";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // EmailDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(427, 289);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.ctlLabel2);
            this.Controls.Add(this.ctlBody);
            this.Controls.Add(this.ctlTo);
            this.Controls.Add(this.ctlLabel1);
            this.Controls.Add(this.ctlSubject);
            this.Controls.Add(this.ctlLabel4);
            this.Controls.Add(this.ctlLabel6);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.ctlFrom);
            this.MaximizeBox = false;
            this.Name = "EmailDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Dialog";
            this.Controls.SetChildIndex(this.ctlFrom, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.ctlLabel6, 0);
            this.Controls.SetChildIndex(this.ctlLabel4, 0);
            this.Controls.SetChildIndex(this.ctlSubject, 0);
            this.Controls.SetChildIndex(this.ctlLabel1, 0);
            this.Controls.SetChildIndex(this.ctlTo, 0);
            this.Controls.SetChildIndex(this.ctlBody, 0);
            this.Controls.SetChildIndex(this.ctlLabel2, 0);
            this.Controls.SetChildIndex(this.txtFileName, 0);
            this.Controls.SetChildIndex(this.btnFile, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Nistec.WinForms.McButton btnExit;
        private Nistec.WinForms.McButton btnOk;
        private Nistec.WinForms.McLabel ctlLabel6;
        private Nistec.WinForms.McTextBox ctlFrom;
        private Nistec.WinForms.McLabel ctlLabel1;
        private Nistec.WinForms.McTextBox ctlSubject;
        private Nistec.WinForms.McComboBox ctlTo;
        private Nistec.WinForms.McTextBox ctlBody;
        private Nistec.WinForms.McLabel ctlLabel2;
        private Nistec.WinForms.McLabel ctlLabel4;
        private Nistec.WinForms.McTextBox txtFileName;
        private Nistec.WinForms.McButton btnFile;


    }
}