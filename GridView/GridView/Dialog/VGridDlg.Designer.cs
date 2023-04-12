using Nistec.Win;
namespace Nistec.GridView
{
    partial class VGridDlg
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
            this.ctlContextMenu1 = new Nistec.WinForms.McContextMenu();
            this.mnExport = new System.Windows.Forms.MenuItem();
            this.mnPrint = new System.Windows.Forms.MenuItem();
            this.lblDescription = new Nistec.WinForms.McLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.mcToolBar = new Nistec.WinForms.McToolBar();
            this.tbFit = new Nistec.WinForms.McToolButton();
            this.tbExport = new Nistec.WinForms.McToolButton();
            this.tbPrint = new Nistec.WinForms.McToolButton();
            this.tbSave = new Nistec.WinForms.McToolButton();
            this.propertyGrid1 = new Nistec.GridView.VGrid();
            this.mcToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGrid1)).BeginInit();
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
            // ctlContextMenu1
            // 
            this.ctlContextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnExport,
            this.mnPrint});
            // 
            // mnExport
            // 
            this.ctlContextMenu1.SetDraw(this.mnExport, true);
            this.mnExport.Index = 0;
            this.mnExport.Text = "Export";
            this.mnExport.Click += new System.EventHandler(this.mnExport_Click);
            // 
            // mnPrint
            // 
            this.ctlContextMenu1.SetDraw(this.mnPrint, true);
            this.mnPrint.Index = 1;
            this.mnPrint.Text = "Print";
            this.mnPrint.Click += new System.EventHandler(this.mnPrint_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.White;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDescription.FixSize = false;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point(2, 280);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(332, 36);
            this.lblDescription.StylePainter = this.StyleGuideBase;
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(2, 276);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(332, 4);
            this.splitter1.TabIndex = 10;
            this.splitter1.TabStop = false;
            // 
            // mcToolBar
            // 
            this.mcToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.mcToolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mcToolBar.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.mcToolBar.Controls.Add(this.tbFit);
            this.mcToolBar.Controls.Add(this.tbExport);
            this.mcToolBar.Controls.Add(this.tbPrint);
            this.mcToolBar.Controls.Add(this.tbSave);
            this.mcToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.mcToolBar.FixSize = false;
            this.mcToolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.mcToolBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mcToolBar.Location = new System.Drawing.Point(2, 38);
            this.mcToolBar.Name = "mcToolBar";
            this.mcToolBar.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.mcToolBar.SelectedGroup = -1;
            this.mcToolBar.Size = new System.Drawing.Size(332, 26);
            this.mcToolBar.TabIndex = 10002;
            this.mcToolBar.ButtonClick += new Nistec.WinForms.ToolButtonClickEventHandler(this.mcToolBar_ButtonClick);
            // 
            // tbFit
            // 
            this.tbFit.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbFit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbFit.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbFit.Image = global::Nistec.GridView.Properties.Resources.fit_to_size;
            this.tbFit.Location = new System.Drawing.Point(78, 3);
            this.tbFit.Name = "tbFit";
            this.tbFit.Size = new System.Drawing.Size(22, 20);
            this.tbFit.TabIndex = 3;
            this.tbFit.ToolTipText = "Fit Columns Size";
            // 
            // tbExport
            // 
            this.tbExport.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbExport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbExport.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbExport.Image = global::Nistec.GridView.Properties.Resources.Export;
            this.tbExport.Location = new System.Drawing.Point(56, 3);
            this.tbExport.Name = "tbExport";
            this.tbExport.Size = new System.Drawing.Size(22, 20);
            this.tbExport.TabIndex = 2;
            this.tbExport.ToolTipText = "Export";
            // 
            // tbPrint
            // 
            this.tbPrint.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbPrint.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbPrint.Image = global::Nistec.GridView.Properties.Resources.printp1;
            this.tbPrint.Location = new System.Drawing.Point(34, 3);
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Size = new System.Drawing.Size(22, 20);
            this.tbPrint.TabIndex = 1;
            this.tbPrint.ToolTipText = "Print preview";
            // 
            // tbSave
            // 
            this.tbSave.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbSave.Image = global::Nistec.GridView.Properties.Resources.save;
            this.tbSave.Location = new System.Drawing.Point(12, 3);
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(22, 20);
            this.tbSave.TabIndex = 0;
            this.tbSave.ToolTipText = "Save Changes";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.BackColor = System.Drawing.Color.White;
            this.propertyGrid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.propertyGrid1.ForeColor = System.Drawing.Color.Black;
            this.propertyGrid1.Location = new System.Drawing.Point(2, 64);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(332, 212);
            this.propertyGrid1.TabIndex = 10005;
            this.propertyGrid1.ReadOnlyChanged += new System.EventHandler(this.propertyGrid1_ReadOnlyChanged);
            this.propertyGrid1.DirtyChanged += new System.EventHandler(this.propertyGrid1_DirtyChanged);
            this.propertyGrid1.PropertyItemChanged += new PropertyItemChangedEventHandler(this.propertyGrid1_PropertyItemChanged);
            // 
            // VGridDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(336, 318);
            this.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.mcToolBar);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lblDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VGridDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VGrid Dlg";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.splitter1, 0);
            this.Controls.SetChildIndex(this.mcToolBar, 0);
            this.Controls.SetChildIndex(this.propertyGrid1, 0);
            this.mcToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGrid1)).EndInit();
            this.ResumeLayout(false);

        }

     
      

        
    
        #endregion

        private Nistec.WinForms.McContextMenu ctlContextMenu1;
        private System.Windows.Forms.MenuItem mnExport;
        private System.Windows.Forms.MenuItem mnPrint;
        private Nistec.WinForms.McLabel lblDescription;
        private System.Windows.Forms.Splitter splitter1;
        private Nistec.WinForms.McToolBar mcToolBar;
        private Nistec.WinForms.McToolButton tbFit;
        private Nistec.WinForms.McToolButton tbExport;
        private Nistec.WinForms.McToolButton tbPrint;
        private Nistec.WinForms.McToolButton tbSave;
        private VGrid propertyGrid1;
    }
}