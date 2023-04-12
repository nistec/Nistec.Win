namespace Nistec.Printing.UI
{
    partial class McExport
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
            this.txtWorkbook = new System.Windows.Forms.TextBox();
            this.lblWorkbook = new System.Windows.Forms.Label();
            this.butBrowse = new System.Windows.Forms.Button();
            this.sfdSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.cboWriterType = new System.Windows.Forms.ComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkRowHeader = new System.Windows.Forms.CheckBox();
            this.cboTable = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.chkOpenAfter = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtWorkbook
            // 
            this.txtWorkbook.Location = new System.Drawing.Point(19, 68);
            this.txtWorkbook.Name = "txtWorkbook";
            this.txtWorkbook.Size = new System.Drawing.Size(316, 20);
            this.txtWorkbook.TabIndex = 17;
            // 
            // lblWorkbook
            // 
            this.lblWorkbook.AutoSize = true;
            this.lblWorkbook.Location = new System.Drawing.Point(19, 53);
            this.lblWorkbook.Name = "lblWorkbook";
            this.lblWorkbook.Size = new System.Drawing.Size(54, 13);
            this.lblWorkbook.TabIndex = 16;
            this.lblWorkbook.Text = "File Name";
            // 
            // butBrowse
            // 
            this.butBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.butBrowse.Location = new System.Drawing.Point(335, 69);
            this.butBrowse.Name = "butBrowse";
            this.butBrowse.Size = new System.Drawing.Size(20, 19);
            this.butBrowse.TabIndex = 18;
            this.butBrowse.Text = "...";
            this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
            // 
            // cboWriterType
            // 
            this.cboWriterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWriterType.Location = new System.Drawing.Point(19, 29);
            this.cboWriterType.Name = "cboWriterType";
            this.cboWriterType.Size = new System.Drawing.Size(336, 21);
            this.cboWriterType.TabIndex = 20;
            this.cboWriterType.SelectedIndexChanged += new System.EventHandler(this.cboWriterType_SelectedIndexChanged);
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(19, 14);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(64, 13);
            this.lblTable.TabIndex = 19;
            this.lblTable.Text = "Export Type";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(185, 168);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 26);
            this.btnOk.TabIndex = 21;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(273, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 26);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkRowHeader
            // 
            this.chkRowHeader.Location = new System.Drawing.Point(19, 133);
            this.chkRowHeader.Name = "chkRowHeader";
            this.chkRowHeader.Size = new System.Drawing.Size(126, 20);
            this.chkRowHeader.TabIndex = 25;
            this.chkRowHeader.Text = "Row First Headers";
            // 
            // cboTable
            // 
            this.cboTable.Location = new System.Drawing.Point(19, 106);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(336, 20);
            this.cboTable.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Worksheet";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(97, 168);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(82, 26);
            this.btnPreview.TabIndex = 26;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // chkOpenAfter
            // 
            this.chkOpenAfter.Location = new System.Drawing.Point(229, 133);
            this.chkOpenAfter.Name = "chkOpenAfter";
            this.chkOpenAfter.Size = new System.Drawing.Size(126, 20);
            this.chkOpenAfter.TabIndex = 27;
            this.chkOpenAfter.Text = "Open After Export";
            // 
            // McExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chkOpenAfter);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.chkRowHeader);
            this.Controls.Add(this.cboTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cboWriterType);
            this.Controls.Add(this.lblTable);
            this.Controls.Add(this.txtWorkbook);
            this.Controls.Add(this.lblWorkbook);
            this.Controls.Add(this.butBrowse);
            this.Name = "McExport";
            this.Size = new System.Drawing.Size(372, 211);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWorkbook;
        private System.Windows.Forms.Label lblWorkbook;
        private System.Windows.Forms.Button butBrowse;
        private System.Windows.Forms.SaveFileDialog sfdSaveAs;
        private System.Windows.Forms.ComboBox cboWriterType;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkRowHeader;
        private System.Windows.Forms.TextBox cboTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.CheckBox chkOpenAfter;
    }
}
