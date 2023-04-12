

namespace mControl.Wizards.Dlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailDlg));
            this.ctlFrom = new mControl.WinCtl.Controls.CtlTextBox();
            this.btnOk = new mControl.WinCtl.Controls.CtlButton();
            this.btnExit = new mControl.WinCtl.Controls.CtlButton();
            this.ctlLabel6 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlLabel4 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlLabel1 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlSubject = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlTo = new mControl.WinCtl.Controls.CtlComboBox();
            this.ctlBody = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlLabel2 = new mControl.WinCtl.Controls.CtlLabel();
            this.txtFileName = new mControl.WinCtl.Controls.CtlTextBox();
            this.btnFile = new mControl.WinCtl.Controls.CtlButton();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.caption.ControlLayout = mControl.WinCtl.Controls.ControlLayout.XpLayout;
            this.caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.caption.Location = new System.Drawing.Point(2, 2);
            this.caption.Name = "caption";
            this.caption.ShowFormBox = true;
            this.caption.ShowMaximize = false;
            this.caption.ShowMinimize = false;
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.SubTitleColor = System.Drawing.SystemColors.ControlText;
            this.caption.Text = "Email Dialog";
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = mControl.WinCtl.Controls.Styles.Desktop;
            // 
            // ctlFrom
            // 
            this.ctlFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlFrom.Location = new System.Drawing.Point(88, 72);
            this.ctlFrom.Name = "ctlFrom";
            this.ctlFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlFrom.Size = new System.Drawing.Size(236, 20);
            this.ctlFrom.StylePainter = this.StyleGuideBase;
            this.ctlFrom.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.Location = new System.Drawing.Point(190, 222);
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
            this.btnExit.Location = new System.Drawing.Point(262, 222);
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
            this.ctlLabel6.Location = new System.Drawing.Point(16, 72);
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
            this.ctlLabel4.Location = new System.Drawing.Point(16, 96);
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
            this.ctlLabel1.Location = new System.Drawing.Point(16, 122);
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
            this.ctlSubject.Location = new System.Drawing.Point(88, 122);
            this.ctlSubject.Name = "ctlSubject";
            this.ctlSubject.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlSubject.Size = new System.Drawing.Size(238, 20);
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
            this.ctlTo.Location = new System.Drawing.Point(88, 96);
            this.ctlTo.Name = "ctlTo";
            this.ctlTo.Size = new System.Drawing.Size(236, 20);
            this.ctlTo.StylePainter = this.StyleGuideBase;
            this.ctlTo.TabIndex = 1;
            // 
            // ctlBody
            // 
            this.ctlBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlBody.Location = new System.Drawing.Point(16, 148);
            this.ctlBody.Multiline = true;
            this.ctlBody.Name = "ctlBody";
            this.ctlBody.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlBody.Size = new System.Drawing.Size(310, 68);
            this.ctlBody.StylePainter = this.StyleGuideBase;
            this.ctlBody.TabIndex = 3;
            // 
            // ctlLabel2
            // 
            this.ctlLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ctlLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel2.Location = new System.Drawing.Point(16, 222);
            this.ctlLabel2.Name = "ctlLabel2";
            this.ctlLabel2.Size = new System.Drawing.Size(52, 20);
            this.ctlLabel2.StylePainter = this.StyleGuideBase;
            this.ctlLabel2.Text = "Attach";
            this.ctlLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.BackColor = System.Drawing.Color.White;
            this.txtFileName.ForeColor = System.Drawing.Color.Black;
            this.txtFileName.Location = new System.Drawing.Point(67, 221);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFileName.Size = new System.Drawing.Size(84, 20);
            this.txtFileName.StylePainter = this.StyleGuideBase;
            this.txtFileName.TabIndex = 13;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFile.Location = new System.Drawing.Point(148, 222);
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
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(336, 258);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "EmailDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Dialog";
            this.Controls.SetChildIndex(this.caption, 0);
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

        private mControl.WinCtl.Controls.CtlButton btnExit;
        private mControl.WinCtl.Controls.CtlButton btnOk;
        private mControl.WinCtl.Controls.CtlLabel ctlLabel6;
        private mControl.WinCtl.Controls.CtlTextBox ctlFrom;
        private mControl.WinCtl.Controls.CtlLabel ctlLabel1;
        private mControl.WinCtl.Controls.CtlTextBox ctlSubject;
        private mControl.WinCtl.Controls.CtlComboBox ctlTo;
        private mControl.WinCtl.Controls.CtlTextBox ctlBody;
        private mControl.WinCtl.Controls.CtlLabel ctlLabel2;
        private mControl.WinCtl.Controls.CtlLabel ctlLabel4;
        private mControl.WinCtl.Controls.CtlTextBox txtFileName;
        private mControl.WinCtl.Controls.CtlButton btnFile;


    }
}