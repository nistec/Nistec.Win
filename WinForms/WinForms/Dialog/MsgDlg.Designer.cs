namespace Nistec.WinForms
{
    partial class MsgDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgDlg));
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblBottom = new System.Windows.Forms.Label();
            this.btnOk = new Nistec.WinForms.McButton();
            this.btnColse = new Nistec.WinForms.McButton();
            this.Input = new Nistec.WinForms.McTextBox();
            this.panelText = new Nistec.WinForms.McPanel();
            this.LblMsg = new Nistec.WinForms.McLabel();
            //this.timer1 = new System.Timers.Timer();
            this.panelFooter.SuspendLayout();
            this.panelText.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(126)))), ((int)(((byte)(177)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Desktop;
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.lblBottom);
            this.panelFooter.Controls.Add(this.btnOk);
            this.panelFooter.Controls.Add(this.btnColse);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(2, 69);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(243, 23);
            this.panelFooter.TabIndex = 9;
            // 
            // lblBottom
            // 
            this.lblBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBottom.Location = new System.Drawing.Point(49, 2);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(191, 20);
            this.lblBottom.TabIndex = 12;
            this.lblBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(1, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(20, 20);
            this.btnOk.TabIndex = 7;
            this.btnOk.ToolTipText = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnColse
            // 
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnColse.Image = ((System.Drawing.Image)(resources.GetObject("btnColse.Image")));
            this.btnColse.Location = new System.Drawing.Point(23, 2);
            this.btnColse.Name = "btnColse";
            this.btnColse.Size = new System.Drawing.Size(20, 20);
            this.btnColse.TabIndex = 6;
            this.btnColse.ToolTipText = "Close";
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // Input
            // 
            this.Input.AcceptsReturn = true;
            this.Input.BackColor = System.Drawing.Color.White;
            this.Input.Dock = System.Windows.Forms.DockStyle.Top;
            this.Input.Location = new System.Drawing.Point(0, 2);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(243, 20);
            this.Input.TabIndex = 11;
            // 
            // panelText
            // 
            this.panelText.Controls.Add(this.LblMsg);
            this.panelText.Controls.Add(this.Input);
            this.panelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelText.Location = new System.Drawing.Point(2, 38);
            this.panelText.Name = "panelText";
            this.panelText.Padding = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.panelText.Size = new System.Drawing.Size(243, 31);
            this.panelText.TabIndex = 12;
            // 
            // LblMsg
            // 
            this.LblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.LblMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LblMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblMsg.FixSize = false;
            this.LblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblMsg.Location = new System.Drawing.Point(0, 22);
            this.LblMsg.Name = "LblMsg";
            this.LblMsg.Size = new System.Drawing.Size(243, 5);
            //// 
            //// timer1
            //// 
            //this.timer1.Interval = 2000;
            //this.timer1.SynchronizingObject = this;
            //this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // MsgDlg
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnColse;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(247, 94);
            this.CloseOnEscape = true;
            this.ControlBox = false;
            this.ControlLayout = Nistec.WinForms.ControlLayout.System;
            this.Controls.Add(this.panelText);
            this.Controls.Add(this.panelFooter);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsgDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Mc Dialog";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.panelFooter, 0);
            this.Controls.SetChildIndex(this.panelText, 0);
            this.panelFooter.ResumeLayout(false);
            this.panelText.ResumeLayout(false);
            this.panelText.PerformLayout();
            //((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFooter;
        private Nistec.WinForms.McButton btnColse;
        private Nistec.WinForms.McButton btnOk;
        private Nistec.WinForms.McTextBox Input;
        private Nistec.WinForms.McPanel panelText;
        //private System.Timers.Timer timer1;
        private Nistec.WinForms.McLabel LblMsg;
        private System.Windows.Forms.Label lblBottom;
    }
}