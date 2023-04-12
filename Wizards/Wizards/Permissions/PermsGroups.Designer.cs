namespace MControl.Wizards.Permissions
{
    partial class PermsGroups
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
            this.ctlToolBar = new MControl.WinForms.McToolBar();
            this.tbUpdate = new MControl.WinForms.McToolButton();
            this.tbCancel = new MControl.WinForms.McToolButton();
            this.tbExit = new MControl.WinForms.McToolButton();
            this.grid = new MControl.GridView.Grid();
            this.PermsGroupID = new MControl.GridView.GridTextColumn();
            this.PermsGroupName = new MControl.GridView.GridTextColumn();
            this.IsBuiltIn = new MControl.GridView.GridBoolColumn();
            this.ctlToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
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
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = MControl.WinForms.Styles.Desktop;
            // 
            // ctlToolBar
            // 
            this.ctlToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.ctlToolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlToolBar.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            this.ctlToolBar.Controls.Add(this.tbUpdate);
            this.ctlToolBar.Controls.Add(this.tbCancel);
            this.ctlToolBar.Controls.Add(this.tbExit);
            this.ctlToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlToolBar.FixSize = false;
            this.ctlToolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlToolBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlToolBar.Location = new System.Drawing.Point(2, 38);
            this.ctlToolBar.Name = "ctlToolBar";
            this.ctlToolBar.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.ctlToolBar.Size = new System.Drawing.Size(421, 28);
            this.ctlToolBar.StylePainter = this.StyleGuideBase;
            this.ctlToolBar.TabIndex = 11;
            this.ctlToolBar.ButtonClick += new MControl.WinForms.ToolButtonClickEventHandler(this.ctlToolBar_ButtonClick);
            // 
            // tbUpdate
            // 
            this.tbUpdate.ButtonStyle = MControl.WinForms.ToolButtonStyle.Button; 
            this.tbUpdate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbUpdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbUpdate.Image = global::MControl.Wizards.Properties.Resources.saveHS;
            this.tbUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbUpdate.Location = new System.Drawing.Point(167, 3);
            this.tbUpdate.Name = "tbUpdate";
            this.tbUpdate.ParentBar = this.ctlToolBar;
            this.tbUpdate.Size = new System.Drawing.Size(80, 22);
            this.tbUpdate.TabIndex = 2;
            this.tbUpdate.Tag = "Update";
            this.tbUpdate.Text = "Update";
            this.tbUpdate.ToolTipText = "Update";
            // 
            // tbCancel
            // 
            this.tbCancel.AutoToolTip = false;
            this.tbCancel.ButtonStyle = MControl.WinForms.ToolButtonStyle.Button; 
            this.tbCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbCancel.Image = global::MControl.Wizards.Properties.Resources.dbdele1;
            this.tbCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbCancel.Location = new System.Drawing.Point(85, 3);
            this.tbCancel.Name = "tbCancel";
            this.tbCancel.ParentBar = this.ctlToolBar;
            this.tbCancel.Size = new System.Drawing.Size(82, 22);
            this.tbCancel.TabIndex = 1;
            this.tbCancel.Tag = "Cancel";
            this.tbCancel.Text = "Cancel ";
            this.tbCancel.ToolTipText = "Cancel Changes";
            // 
            // tbExit
            // 
            this.tbExit.ButtonStyle = MControl.WinForms.ToolButtonStyle.Button;
            this.tbExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbExit.Image = global::MControl.Wizards.Properties.Resources.dbcanc1;
            this.tbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbExit.Location = new System.Drawing.Point(12, 3);
            this.tbExit.Name = "tbExit";
            this.tbExit.ParentBar = this.ctlToolBar;
            this.tbExit.Size = new System.Drawing.Size(73, 22);
            this.tbExit.TabIndex = 0;
            this.tbExit.Tag = "Exit";
            this.tbExit.Text = "Exit";
            this.tbExit.ToolTipText = "Exit";
            // 
            // grid
            // 
            this.grid.AllowRemove = false;
            this.grid.BackColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid.CaptionVisible = false;
            this.grid.Columns.AddRange(new MControl.GridView.GridColumnStyle[] {
            this.PermsGroupID,
            this.PermsGroupName,
            this.IsBuiltIn});
            this.grid.DataMember = "";
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid.ForeColor = System.Drawing.Color.Black;
            this.grid.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid.Location = new System.Drawing.Point(2, 66);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(421, 319);
            this.grid.StylePainter = this.StyleGuideBase;
            this.grid.TabIndex = 12;
            this.grid.CurrentRowChanged += new System.EventHandler(this.grid_CurrentRowChanged);
            this.grid.DirtyChanged += new System.EventHandler(this.grid_DirtyChanged);
            // 
            // PermsGroupID
            // 
            this.PermsGroupID.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.PermsGroupID.Format = "G";
            this.PermsGroupID.FormatType = MControl.Formats.GeneralNumber;
            this.PermsGroupID.HeaderText = "Group ID";
            this.PermsGroupID.MappingName = "PermsGroupID";
            this.PermsGroupID.ReadOnly = true;
            this.PermsGroupID.Width = 82;
            // 
            // PermsGroupName
            // 
            this.PermsGroupName.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.PermsGroupName.Format = "";
            this.PermsGroupName.HeaderText = "Group Name";
            this.PermsGroupName.MappingName = "PermsGroupName";
            this.PermsGroupName.Width = 202;
            // 
            // IsBuiltIn
            // 
            this.IsBuiltIn.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.IsBuiltIn.HeaderText = "IsBuiltIn";
            this.IsBuiltIn.MappingName = "IsBuiltIn";
            this.IsBuiltIn.NullValue = "false";
            this.IsBuiltIn.ReadOnly = true;
            this.IsBuiltIn.Text = "";
            this.IsBuiltIn.Width = 77;
            // 
            // PermsGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(425, 409);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ctlToolBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PermsGroups";
            this.Text = "Perms Groups";
            this.Controls.SetChildIndex(this.ctlToolBar, 0);
            this.Controls.SetChildIndex(this.grid, 0);
            this.ctlToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected MControl.WinForms.McToolBar ctlToolBar;
        private MControl.GridView.Grid grid;
        private MControl.GridView.GridTextColumn PermsGroupID;
        private MControl.GridView.GridTextColumn PermsGroupName;
        private MControl.WinForms.McToolButton tbExit;
        private MControl.WinForms.McToolButton tbUpdate;
        private MControl.WinForms.McToolButton tbCancel;
        private MControl.GridView.GridBoolColumn IsBuiltIn;
    }
}