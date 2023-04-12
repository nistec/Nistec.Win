namespace Nistec.Printing.UI
{
    partial class McPrint
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(McPrint));
            this.sfdSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.ofdBrowse = new System.Windows.Forms.OpenFileDialog();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.PrintDialog = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLandscape = new System.Windows.Forms.RadioButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.rbPortrait = new System.Windows.Forms.RadioButton();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.chkHeaderStyle = new System.Windows.Forms.CheckBox();
            this.chkShowHeader = new System.Windows.Forms.CheckBox();
            this.chkShowFooter = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.ImageIndex = 0;
            this.btnOk.ImageList = this.imageList1;
            this.btnOk.Location = new System.Drawing.Point(185, 168);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 26);
            this.btnOk.TabIndex = 21;
            this.btnOk.Text = "Print";
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
            // btnPreview
            // 
            this.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPreview.ImageIndex = 1;
            this.btnPreview.ImageList = this.imageList1;
            this.btnPreview.Location = new System.Drawing.Point(97, 168);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(82, 26);
            this.btnPreview.TabIndex = 26;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbLandscape);
            this.groupBox1.Controls.Add(this.rbPortrait);
            this.groupBox1.Location = new System.Drawing.Point(18, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 51);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Print Option";
            // 
            // rbLandscape
            // 
            this.rbLandscape.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbLandscape.ImageIndex = 3;
            this.rbLandscape.ImageList = this.imageList1;
            this.rbLandscape.Location = new System.Drawing.Point(122, 22);
            this.rbLandscape.Name = "rbLandscape";
            this.rbLandscape.Size = new System.Drawing.Size(99, 21);
            this.rbLandscape.TabIndex = 1;
            this.rbLandscape.Text = "Landscape";
            this.rbLandscape.UseVisualStyleBackColor = true;
            this.rbLandscape.CheckedChanged += new System.EventHandler(this.rbLandscape_CheckedChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "PrintHS.png");
            this.imageList1.Images.SetKeyName(1, "PrintPreviewHS.png");
            this.imageList1.Images.SetKeyName(2, "PortraitHS.png");
            this.imageList1.Images.SetKeyName(3, "PortraitLandscapeHS.png");
            this.imageList1.Images.SetKeyName(4, "PrintSetupHS.png");
            this.imageList1.Images.SetKeyName(5, "PropertiesHS.png");
            // 
            // rbPortrait
            // 
            this.rbPortrait.Checked = true;
            this.rbPortrait.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbPortrait.ImageIndex = 2;
            this.rbPortrait.ImageList = this.imageList1;
            this.rbPortrait.Location = new System.Drawing.Point(15, 19);
            this.rbPortrait.Name = "rbPortrait";
            this.rbPortrait.Size = new System.Drawing.Size(84, 26);
            this.rbPortrait.TabIndex = 0;
            this.rbPortrait.TabStop = true;
            this.rbPortrait.Text = "Portrait";
            this.rbPortrait.UseVisualStyleBackColor = true;
            this.rbPortrait.CheckedChanged += new System.EventHandler(this.rbPortrait_CheckedChanged);
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(18, 32);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(337, 20);
            this.txtHeader.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Title";
            // 
            // btnSettings
            // 
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSettings.ImageIndex = 4;
            this.btnSettings.ImageList = this.imageList1;
            this.btnSettings.Location = new System.Drawing.Point(273, 83);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(82, 26);
            this.btnSettings.TabIndex = 30;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // chkHeaderStyle
            // 
            this.chkHeaderStyle.AutoSize = true;
            this.chkHeaderStyle.Location = new System.Drawing.Point(156, 115);
            this.chkHeaderStyle.Name = "chkHeaderStyle";
            this.chkHeaderStyle.Size = new System.Drawing.Size(91, 17);
            this.chkHeaderStyle.TabIndex = 31;
            this.chkHeaderStyle.Text = "Invert Header";
            this.chkHeaderStyle.UseVisualStyleBackColor = true;
            // 
            // chkShowHeader
            // 
            this.chkShowHeader.AutoSize = true;
            this.chkShowHeader.Location = new System.Drawing.Point(18, 115);
            this.chkShowHeader.Name = "chkShowHeader";
            this.chkShowHeader.Size = new System.Drawing.Size(91, 17);
            this.chkShowHeader.TabIndex = 32;
            this.chkShowHeader.Text = "Show Header";
            this.chkShowHeader.UseVisualStyleBackColor = true;
            // 
            // chkShowFooter
            // 
            this.chkShowFooter.AutoSize = true;
            this.chkShowFooter.Location = new System.Drawing.Point(18, 138);
            this.chkShowFooter.Name = "chkShowFooter";
            this.chkShowFooter.Size = new System.Drawing.Size(86, 17);
            this.chkShowFooter.TabIndex = 33;
            this.chkShowFooter.Text = "Show Footer";
            this.chkShowFooter.UseVisualStyleBackColor = true;
            // 
            // McPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chkShowFooter);
            this.Controls.Add(this.chkShowHeader);
            this.Controls.Add(this.chkHeaderStyle);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "McPrint";
            this.Size = new System.Drawing.Size(372, 206);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog sfdSaveAs;
        private System.Windows.Forms.OpenFileDialog ofdBrowse;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPreview;
        internal System.Windows.Forms.PrintDialog PrintDialog;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLandscape;
        private System.Windows.Forms.RadioButton rbPortrait;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.CheckBox chkHeaderStyle;
        private System.Windows.Forms.CheckBox chkShowHeader;
        private System.Windows.Forms.CheckBox chkShowFooter;
    }
}
