namespace Nistec.WinForms
{
    partial class MemoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoForm));
            this.ctlResize = new Nistec.WinForms.McResize();
            this.toolBar = new Nistec.WinForms.McToolBar();
            this.tbPrint = new Nistec.WinForms.McToolButton();
            this.tbSave = new Nistec.WinForms.McToolButton();
            this.tbClose = new Nistec.WinForms.McToolButton();
            this.txtMemo = new Nistec.WinForms.McTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ctlResize)).BeginInit();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctlResize
            // 
            this.ctlResize.BackColor = System.Drawing.Color.Transparent;
            this.ctlResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.ctlResize.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctlResize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlResize.Location = new System.Drawing.Point(169, 3);
            this.ctlResize.MinFormSize = new System.Drawing.Size(200, 80);
            this.ctlResize.Name = "ctlResize";
            this.ctlResize.ReadOnly = false;
            this.ctlResize.Size = new System.Drawing.Size(20, 20);
            this.ctlResize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ctlResize.TabIndex = 3;
            this.ctlResize.TabStop = false;
            // 
            // toolBar
            // 
            this.toolBar.AllowMove = false;
            this.toolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.toolBar.Controls.Add(this.tbPrint);
            this.toolBar.Controls.Add(this.tbSave);
            this.toolBar.Controls.Add(this.tbClose);
            this.toolBar.Controls.Add(this.ctlResize);
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolBar.FixSize = false;
            this.toolBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBar.Location = new System.Drawing.Point(0, 138);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(2, 3, 3, 3);
            this.toolBar.SelectedGroup = -1;
            this.toolBar.Size = new System.Drawing.Size(192, 28);
            this.toolBar.TabIndex = 0;
            this.toolBar.ButtonClick += new Nistec.WinForms.ToolButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // tbPrint
            // 
            this.tbPrint.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbPrint.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tbPrint.Image")));
            this.tbPrint.Location = new System.Drawing.Point(46, 3);
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Size = new System.Drawing.Size(22, 22);
            this.tbPrint.TabIndex = 2;
            this.tbPrint.ToolTipText = "Print";
            // 
            // tbSave
            // 
            this.tbSave.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbSave.Enabled = false;
            this.tbSave.Image = ((System.Drawing.Image)(resources.GetObject("tbSave.Image")));
            this.tbSave.Location = new System.Drawing.Point(24, 3);
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(22, 22);
            this.tbSave.TabIndex = 1;
            this.tbSave.ToolTipText = "Save";
            // 
            // tbClose
            // 
            this.tbClose.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbClose.Image = ((System.Drawing.Image)(resources.GetObject("tbClose.Image")));
            this.tbClose.Location = new System.Drawing.Point(2, 3);
            this.tbClose.Name = "tbClose";
            this.tbClose.Size = new System.Drawing.Size(22, 22);
            this.tbClose.TabIndex = 0;
            this.tbClose.ToolTipText = "Close";
            // 
            // txtMemo
            // 
            this.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMemo.Location = new System.Drawing.Point(0, 0);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(192, 138);
            this.txtMemo.TabIndex = 2;
            this.txtMemo.TextChanged += new System.EventHandler(this.txtMemo_TextChanged);
            // 
            // MemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 166);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.toolBar);
            this.Name = "MemoForm";
            this.Text = "MemoForm";
            ((System.ComponentModel.ISupportInitialize)(this.ctlResize)).EndInit();
            this.toolBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

   
        #endregion

        private McToolBar toolBar;
        private McToolButton tbPrint;
        private McToolButton tbSave;
        private McToolButton tbClose;
        private McTextBox txtMemo;
        private McResize ctlResize;
    }
}