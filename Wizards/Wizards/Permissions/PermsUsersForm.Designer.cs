namespace MControl.Wizards.Permissions
{
    partial class PermsUsersForm
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
            this.ColumnGroupID = new MControl.GridView.GridTextColumn();
            this.ColumnLvl = new MControl.GridView.GridComboColumn();
            this.ObjectID = new MControl.GridView.GridComboColumn();
            this.grid = new MControl.GridView.Grid();
            this.tbNew = new MControl.WinForms.McToolButton();
            this.tbRemove = new MControl.WinForms.McToolButton();
            this.tbCancel = new MControl.WinForms.McToolButton();
            this.tbSave = new MControl.WinForms.McToolButton();
            this.contextMenuStrip1 = new MControl.WinForms.McContextStrip();
            this.tbSetAll = new MControl.WinForms.McToolButton();
            this.ppNone = new MControl.WinForms.PopUpItem();
            this.ppDenyAll = new MControl.WinForms.PopUpItem();
            this.ppReadOnly = new MControl.WinForms.PopUpItem();
            this.ppEditOnly = new MControl.WinForms.PopUpItem();
            this.ppFullControl = new MControl.WinForms.PopUpItem();
            this.ctlPanel1.SuspendLayout();
            this.ctlToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlList
            // 
            this.ctlList.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlList, "");
            this.ctlList.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ctlList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlList.ForeColor = System.Drawing.Color.Black;
            this.ctlList.ItemHeight = 18;
            this.ctlList.Location = new System.Drawing.Point(2, 66);
            this.ctlList.Name = "ctlList";
            this.ctlList.ReadOnly = false;
            this.ctlList.Size = new System.Drawing.Size(164, 326);
            this.ctlList.StylePainter = this.StyleGuideBase;
            this.ctlList.TabIndex = 7;
            // 
            // ctlSplitter1
            // 
            this.ctlSplitter1.BackColor = System.Drawing.Color.White;
            this.ctlSplitter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlSplitter1.ForeColor = System.Drawing.Color.Black;
            this.ctlSplitter1.Location = new System.Drawing.Point(166, 66);
            this.ctlSplitter1.Name = "ctlSplitter1";
            this.ctlSplitter1.Size = new System.Drawing.Size(4, 326);
            this.ctlSplitter1.StylePainter = this.StyleGuideBase;
            this.ctlSplitter1.TabIndex = 8;
            this.ctlSplitter1.TabStop = false;
            // 
            // ctlPanel1
            // 
            this.ctlPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlPanel1.Controls.Add(this.grid);
            this.ctlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPanel1.Location = new System.Drawing.Point(170, 66);
            this.ctlPanel1.Name = "ctlPanel1";
            this.ctlPanel1.Size = new System.Drawing.Size(384, 326);
            this.ctlPanel1.StylePainter = this.StyleGuideBase;
            this.ctlPanel1.TabIndex = 9;
            // 
            // ctlToolBar
            // 
            this.ctlToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.ctlToolBar.Controls.Add(this.tbSetAll);
            this.ctlToolBar.Controls.Add(this.tbSave);
            this.ctlToolBar.Controls.Add(this.tbCancel);
            this.ctlToolBar.Controls.Add(this.tbRemove);
            this.ctlToolBar.Controls.Add(this.tbNew);
            this.ctlToolBar.Size = new System.Drawing.Size(552, 28);
            // 
            // ctlNavBar
            // 
            this.ctlNavBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlNavBar.Connection = null;
            this.ctlNavBar.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            this.ctlNavBar.DBProvider = MControl.Data.DBProvider.SqlServer;
            this.ctlNavBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlNavBar.EnableButtons = false;
            this.ctlNavBar.Location = new System.Drawing.Point(2, 275);
            this.ctlNavBar.ManagerDataAdapter = null;
            this.ctlNavBar.MessageDelete = "Delete";
            this.ctlNavBar.MessageSaveChanges = "SaveChanges";
            this.ctlNavBar.Name = "ctlNavBar";
            this.ctlNavBar.Padding = new System.Windows.Forms.Padding(2);
            this.ctlNavBar.ShowChangesButtons = true;
            this.ctlNavBar.ShowCounters = false;
            this.ctlNavBar.ShowNavigatore = false;
            this.ctlNavBar.Size = new System.Drawing.Size(453, 24);
            this.ctlNavBar.SizingGrip = false;
            this.ctlNavBar.StylePainter = this.StyleGuideBase;
            this.ctlNavBar.TabIndex = 0;
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
            // ColumnGroupID
            // 
            this.ColumnGroupID.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ColumnGroupID.Format = "";
            this.ColumnGroupID.HeaderText = "Group ID";
            this.ColumnGroupID.MappingName = "PermsID";
            this.ColumnGroupID.Visible = false;
            this.ColumnGroupID.Width = 0;
            // 
            // ColumnLvl
            // 
            this.ColumnLvl.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ColumnLvl.DisplayMember = "LvlName";
            this.ColumnLvl.Format = "";
            this.ColumnLvl.HeaderText = "Level";
            this.ColumnLvl.MappingName = "Lvl";
            this.ColumnLvl.ValueMember = "Lvl";
            this.ColumnLvl.Width = 127;
            // 
            // ObjectID
            // 
            this.ObjectID.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ObjectID.ButtonVisible = false;
            this.ObjectID.Format = "";
            this.ObjectID.HeaderText = "Object ";
            this.ObjectID.MappingName = "ObjectID";
            this.ObjectID.Width = 194;
            // 
            // grid
            // 
            this.grid.AllowAdd = false;
            this.grid.AllowColumnContextMenu = false;
            this.grid.AllowGridContextMenu = false;
            this.grid.AllowRemove = false;
            this.grid.BackColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid.CaptionVisible = false;
            this.grid.Columns.AddRange(new MControl.GridView.GridColumnStyle[] {
            this.ColumnGroupID,
            this.ObjectID,
            this.ColumnLvl});
            this.grid.DataMember = "";
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid.ForeColor = System.Drawing.Color.Black;
            this.grid.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(384, 326);
            this.grid.StylePainter = this.StyleGuideBase;
            this.grid.TabIndex = 1;
            this.grid.DirtyChanged += new System.EventHandler(this.grid_DirtyChanged);
            // 
            // tbNew
            // 
            this.tbNew.ButtonStyle = MControl.WinForms.ToolButtonStyle.Button;
            this.tbNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbNew.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbNew.Image = global::MControl.Wizards.Properties.Resources.newfile_wiz;
            this.tbNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbNew.Location = new System.Drawing.Point(12, 3);
            this.tbNew.Name = "tbNew";
            this.tbNew.ParentBar = this.ctlToolBar;
            this.tbNew.Size = new System.Drawing.Size(100, 22);
            this.tbNew.TabIndex = 0;
            this.tbNew.Tag = "New";
            this.tbNew.Text = "New Group";
            this.tbNew.ToolTipText = "New Group";
            // 
            // tbRemove
            // 
            this.tbRemove.ButtonStyle = MControl.WinForms.ToolButtonStyle.Button;
            this.tbRemove.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbRemove.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbRemove.Image = global::MControl.Wizards.Properties.Resources.delete;
            this.tbRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbRemove.Location = new System.Drawing.Point(112, 3);
            this.tbRemove.Name = "tbRemove";
            this.tbRemove.ParentBar = this.ctlToolBar;
            this.tbRemove.Size = new System.Drawing.Size(100, 22);
            this.tbRemove.TabIndex = 1;
            this.tbRemove.Tag = "Remove";
            this.tbRemove.Text = "Remove";
            this.tbRemove.ToolTipText = "Remove Group";
            // 
            // tbCancel
            // 
            this.tbCancel.ButtonStyle = MControl.WinForms.ToolButtonStyle.Button;
            this.tbCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbCancel.Image = global::MControl.Wizards.Properties.Resources.dbdele1;
            this.tbCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbCancel.Location = new System.Drawing.Point(212, 3);
            this.tbCancel.Name = "tbCancel";
            this.tbCancel.ParentBar = this.ctlToolBar;
            this.tbCancel.Size = new System.Drawing.Size(86, 22);
            this.tbCancel.TabIndex = 2;
            this.tbCancel.Tag = "Cancel";
            this.tbCancel.Text = "Cancel";
            this.tbCancel.ToolTipText = "Cancel Changes";
            // 
            // tbSave
            // 
            this.tbSave.ButtonStyle = MControl.WinForms.ToolButtonStyle.Button;
            this.tbSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbSave.Image = global::MControl.Wizards.Properties.Resources.saveHS;
            this.tbSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbSave.Location = new System.Drawing.Point(298, 3);
            this.tbSave.Name = "tbSave";
            this.tbSave.ParentBar = this.ctlToolBar;
            this.tbSave.Size = new System.Drawing.Size(94, 22);
            this.tbSave.TabIndex = 3;
            this.tbSave.Tag = "Save";
            this.tbSave.Text = "Save";
            this.tbSave.ToolTipText = "Save";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tbSetAll
            // 
            this.tbSetAll.ButtonStyle = MControl.WinForms.ToolButtonStyle.DropDownButton;
            this.tbSetAll.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbSetAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbSetAll.Location = new System.Drawing.Point(392, 3);
            this.tbSetAll.MenuItems.AddRange(new MControl.WinForms.PopUpItem[] {
            this.ppNone,
            this.ppDenyAll,
            this.ppReadOnly,
            this.ppEditOnly,
            this.ppFullControl});
            this.tbSetAll.Name = "tbSetAll";
            this.tbSetAll.ParentBar = this.ctlToolBar;
            this.tbSetAll.Size = new System.Drawing.Size(92, 22);
            this.tbSetAll.TabIndex = 4;
            this.tbSetAll.Tag = "SetAll";
            this.tbSetAll.Text = "Set All";
            this.tbSetAll.ToolTipText = "Set value for all list ";
            this.tbSetAll.SelectedItemClick += new MControl.WinForms.SelectedPopUpItemEventHandler(this.tbSetAll_SelectedItemClick);
            // 
            // ppNone
            // 
            this.ppNone.Text = "None";
            // 
            // ppDenyAll
            // 
            this.ppDenyAll.Text = "DenyAll";
            // 
            // ppReadOnly
            // 
            this.ppReadOnly.Text = "ReadOnly";
            // 
            // ppEditOnly
            // 
            this.ppEditOnly.Text = "EditOnly";
            // 
            // ppFullControl
            // 
            this.ppFullControl.Text = "FullControl";
            // 
            // PermsUsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(556, 394);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PermsUsersForm";
            this.ShowNavBar = false;
            this.Text = "Users Permission";
            this.UseSynchronize = false;
            this.Controls.SetChildIndex(this.ctlToolBar, 0);
            this.Controls.SetChildIndex(this.ctlNavBar, 0);
            this.Controls.SetChildIndex(this.ctlList, 0);
            this.Controls.SetChildIndex(this.ctlSplitter1, 0);
            this.Controls.SetChildIndex(this.ctlPanel1, 0);
            this.ctlPanel1.ResumeLayout(false);
            this.ctlToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.GridView.Grid grid;
        private MControl.GridView.GridTextColumn ColumnGroupID;
        private MControl.GridView.GridComboColumn ColumnLvl;
        private MControl.GridView.GridComboColumn ObjectID;
        private MControl.WinForms.McToolButton tbRemove;
        private MControl.WinForms.McToolButton tbNew;
        private MControl.WinForms.McToolButton tbSave;
        private MControl.WinForms.McToolButton tbCancel;
        private MControl.WinForms.McContextStrip contextMenuStrip1;
        private MControl.WinForms.McToolButton tbSetAll;
        private MControl.WinForms.PopUpItem ppNone;
        private MControl.WinForms.PopUpItem ppDenyAll;
        private MControl.WinForms.PopUpItem ppReadOnly;
        private MControl.WinForms.PopUpItem ppEditOnly;
        private MControl.WinForms.PopUpItem ppFullControl;

    }
}