namespace Nistec.Wizards.Controls
{
    partial class McGroupsEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctlListBoxLeft = new Nistec.WinForms.McListBox();
            this.ctlComboLeft = new Nistec.WinForms.McComboBox();
            this.ctlLabelLeft = new Nistec.WinForms.McLabel();
            this.ctlPanelLeft = new Nistec.WinForms.McPanel();
            this.btnLeft = new Nistec.WinForms.McButton();
            this.btnAllLeftToRight = new Nistec.WinForms.McButton();
            this.btnLeftToRight = new Nistec.WinForms.McButton();
            this.ctlListBoxRight = new Nistec.WinForms.McListBox();
            this.ctlComboRight = new Nistec.WinForms.McComboBox();
            this.ctlLabelRight = new Nistec.WinForms.McLabel();
            this.ctlPanelRight = new Nistec.WinForms.McPanel();
            this.btnRight = new Nistec.WinForms.McButton();
            this.btnAllRightToLeft = new Nistec.WinForms.McButton();
            this.btnRightToLeft = new Nistec.WinForms.McButton();
            this.ctlPanel = new Nistec.WinForms.McPanel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctlPanelLeft.SuspendLayout();
            this.ctlPanelRight.SuspendLayout();
            this.ctlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ctlListBoxLeft);
            this.splitContainer1.Panel1.Controls.Add(this.ctlComboLeft);
            this.splitContainer1.Panel1.Controls.Add(this.ctlLabelLeft);
            this.splitContainer1.Panel1.Controls.Add(this.ctlPanelLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ctlListBoxRight);
            this.splitContainer1.Panel2.Controls.Add(this.ctlComboRight);
            this.splitContainer1.Panel2.Controls.Add(this.ctlLabelRight);
            this.splitContainer1.Panel2.Controls.Add(this.ctlPanelRight);
            this.splitContainer1.Size = new System.Drawing.Size(292, 190);
            this.splitContainer1.SplitterDistance = 144;
            this.splitContainer1.TabIndex = 3;
            // 
            // ctlListBoxLeft
            // 
            this.ctlListBoxLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlListBoxLeft.Location = new System.Drawing.Point(0, 40);
            this.ctlListBoxLeft.Name = "ctlListBoxLeft";
            this.ctlListBoxLeft.ReadOnly = false;
            this.ctlListBoxLeft.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ctlListBoxLeft.Size = new System.Drawing.Size(144, 106);
            this.ctlListBoxLeft.TabIndex = 9;
            this.ctlListBoxLeft.SelectedIndexChanged += new System.EventHandler(this.ctlListBoxLeft_SelectedIndexChanged);
            // 
            // ctlComboLeft
            // 
            this.ctlComboLeft.ButtonToolTip = "";
            this.ctlComboLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlComboLeft.IntegralHeight = false;
            this.ctlComboLeft.Location = new System.Drawing.Point(0, 20);
            this.ctlComboLeft.Name = "ctlComboLeft";
            this.ctlComboLeft.Size = new System.Drawing.Size(144, 20);
            this.ctlComboLeft.TabIndex = 7;
            this.ctlComboLeft.SelectedIndexChanged += new System.EventHandler(this.ctlComboLeft_SelectedIndexChanged);
            // 
            // ctlLabelLeft
            // 
            this.ctlLabelLeft.BackColor = System.Drawing.SystemColors.Control;
            this.ctlLabelLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlLabelLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabelLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabelLeft.Location = new System.Drawing.Point(0, 0);
            this.ctlLabelLeft.Name = "ctlLabelLeft";
            this.ctlLabelLeft.Size = new System.Drawing.Size(144, 20);
            // 
            // ctlPanelLeft
            // 
            this.ctlPanelLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlPanelLeft.Controls.Add(this.btnLeft);
            this.ctlPanelLeft.Controls.Add(this.btnAllLeftToRight);
            this.ctlPanelLeft.Controls.Add(this.btnLeftToRight);
            this.ctlPanelLeft.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlPanelLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPanelLeft.Location = new System.Drawing.Point(0, 154);
            this.ctlPanelLeft.Name = "ctlPanelLeft";
            this.ctlPanelLeft.Size = new System.Drawing.Size(144, 36);
            this.ctlPanelLeft.TabIndex = 4;
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeft.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLeft.Location = new System.Drawing.Point(8, 9);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(40, 24);
            this.btnLeft.TabIndex = 7;
            this.btnLeft.Text = "-";
            this.btnLeft.ToolTipText = "-";
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnAllLeftToRight
            // 
            this.btnAllLeftToRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllLeftToRight.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAllLeftToRight.Location = new System.Drawing.Point(54, 9);
            this.btnAllLeftToRight.Name = "btnAllLeftToRight";
            this.btnAllLeftToRight.Size = new System.Drawing.Size(40, 24);
            this.btnAllLeftToRight.TabIndex = 6;
            this.btnAllLeftToRight.Text = ">>";
            this.btnAllLeftToRight.ToolTipText = ">>";
            this.btnAllLeftToRight.Click += new System.EventHandler(this.btnAllLeftToRight_Click);
            // 
            // btnLeftToRight
            // 
            this.btnLeftToRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeftToRight.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLeftToRight.Location = new System.Drawing.Point(100, 9);
            this.btnLeftToRight.Name = "btnLeftToRight";
            this.btnLeftToRight.Size = new System.Drawing.Size(40, 24);
            this.btnLeftToRight.TabIndex = 5;
            this.btnLeftToRight.Text = ">";
            this.btnLeftToRight.ToolTipText = ">";
            this.btnLeftToRight.Click += new System.EventHandler(this.btnLeftToRight_Click);
            // 
            // ctlListBoxRight
            // 
            this.ctlListBoxRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlListBoxRight.Location = new System.Drawing.Point(0, 40);
            this.ctlListBoxRight.Name = "ctlListBoxRight";
            this.ctlListBoxRight.ReadOnly = false;
            this.ctlListBoxRight.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ctlListBoxRight.Size = new System.Drawing.Size(144, 106);
            this.ctlListBoxRight.TabIndex = 8;
            this.ctlListBoxRight.SelectedIndexChanged += new System.EventHandler(this.ctlListBoxRight_SelectedIndexChanged);
            // 
            // ctlComboRight
            // 
            this.ctlComboRight.ButtonToolTip = "";
            this.ctlComboRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlComboRight.IntegralHeight = false;
            this.ctlComboRight.Location = new System.Drawing.Point(0, 20);
            this.ctlComboRight.Name = "ctlComboRight";
            this.ctlComboRight.Size = new System.Drawing.Size(144, 20);
            this.ctlComboRight.TabIndex = 6;
            this.ctlComboRight.SelectedIndexChanged += new System.EventHandler(this.ctlComboRight_SelectedIndexChanged);
            // 
            // ctlLabelRight
            // 
            this.ctlLabelRight.BackColor = System.Drawing.SystemColors.Control;
            this.ctlLabelRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlLabelRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabelRight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabelRight.Location = new System.Drawing.Point(0, 0);
            this.ctlLabelRight.Name = "ctlLabelRight";
            this.ctlLabelRight.Size = new System.Drawing.Size(144, 20);
            // 
            // ctlPanelRight
            // 
            this.ctlPanelRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlPanelRight.Controls.Add(this.btnRight);
            this.ctlPanelRight.Controls.Add(this.btnAllRightToLeft);
            this.ctlPanelRight.Controls.Add(this.btnRightToLeft);
            this.ctlPanelRight.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlPanelRight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPanelRight.Location = new System.Drawing.Point(0, 154);
            this.ctlPanelRight.Name = "ctlPanelRight";
            this.ctlPanelRight.Size = new System.Drawing.Size(144, 36);
            this.ctlPanelRight.TabIndex = 0;
            // 
            // btnRight
            // 
            this.btnRight.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRight.Location = new System.Drawing.Point(95, 9);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(40, 24);
            this.btnRight.TabIndex = 6;
            this.btnRight.Text = "-";
            this.btnRight.ToolTipText = "-";
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnAllRightToLeft
            // 
            this.btnAllRightToLeft.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAllRightToLeft.Location = new System.Drawing.Point(49, 9);
            this.btnAllRightToLeft.Name = "btnAllRightToLeft";
            this.btnAllRightToLeft.Size = new System.Drawing.Size(40, 24);
            this.btnAllRightToLeft.TabIndex = 5;
            this.btnAllRightToLeft.Text = "<<";
            this.btnAllRightToLeft.ToolTipText = "<<";
            this.btnAllRightToLeft.Click += new System.EventHandler(this.btnAllRightToLeft_Click);
            // 
            // btnRightToLeft
            // 
            this.btnRightToLeft.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRightToLeft.Location = new System.Drawing.Point(3, 9);
            this.btnRightToLeft.Name = "btnRightToLeft";
            this.btnRightToLeft.Size = new System.Drawing.Size(40, 24);
            this.btnRightToLeft.TabIndex = 4;
            this.btnRightToLeft.Text = "<";
            this.btnRightToLeft.ToolTipText = "<";
            this.btnRightToLeft.Click += new System.EventHandler(this.btnRightToLeft_Click);
            // 
            // ctlPanel
            // 
            this.ctlPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlPanel.Controls.Add(this.splitContainer1);
            this.ctlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPanel.Location = new System.Drawing.Point(0, 0);
            this.ctlPanel.Name = "ctlPanel";
            this.ctlPanel.Size = new System.Drawing.Size(292, 190);
            this.ctlPanel.TabIndex = 5;
            // 
            // McGroupsEditor
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctlPanel);
            this.Name = "McGroupsEditor";
            this.Size = new System.Drawing.Size(292, 190);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ctlPanelLeft.ResumeLayout(false);
            this.ctlPanelRight.ResumeLayout(false);
            this.ctlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Nistec.WinForms.McPanel ctlPanel;
        private Nistec.WinForms.McPanel ctlPanelRight;
        private Nistec.WinForms.McPanel ctlPanelLeft;
        public Nistec.WinForms.McButton btnRightToLeft;
        public Nistec.WinForms.McButton btnAllLeftToRight;
        public Nistec.WinForms.McButton btnLeftToRight;
        public Nistec.WinForms.McButton btnAllRightToLeft;
        public Nistec.WinForms.McButton btnLeft;
        public Nistec.WinForms.McButton btnRight;
        public Nistec.WinForms.McComboBox ctlComboLeft;
        public Nistec.WinForms.McLabel ctlLabelLeft;
        public Nistec.WinForms.McComboBox ctlComboRight;
        public Nistec.WinForms.McLabel ctlLabelRight;
        public Nistec.WinForms.McListBox ctlListBoxLeft;
        public Nistec.WinForms.McListBox ctlListBoxRight;
    }
}
