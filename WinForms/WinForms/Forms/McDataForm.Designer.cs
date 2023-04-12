namespace Nistec.WinForms
{
    partial class McDataForm
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
            this.ctlNavBar = new Nistec.WinForms.McNavBar();
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlNavBar
            // 
            this.ctlNavBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlNavBar.Connection = null;
            this.ctlNavBar.DBProvider = Nistec.Data.DBProvider.SqlServer;
            this.ctlNavBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlNavBar.DockPadding.All = 2;
            this.ctlNavBar.EnableButtons = false;
            //this.ctlNavBar.Location = new System.Drawing.Point(0, 213);
            this.ctlNavBar.ManagerDataAdapter = null;
            this.ctlNavBar.MessageDelete = "Delete";
            this.ctlNavBar.MessageSaveChanges = "SaveChanges";
            this.ctlNavBar.Name = "ctlNavBar";
            this.ctlNavBar.StylePainter = this.StyleGuideBase;
            this.ctlNavBar.ShowChangesButtons = true;
            //this.ctlNavBar.Size = new System.Drawing.Size(240, 24);
            this.ctlNavBar.TabIndex = 0;
            this.ctlNavBar.NavBarUpdated += new Nistec.WinForms.NavBarUpdatedEventHandler(this.ctlNavBar_NavBarUpdated);
            this.ctlNavBar.BindPositionChanged += new System.EventHandler(this.ctlNavBar_BindPositionChanged);
            this.ctlNavBar.RowUpdated += new Nistec.WinForms.McNavBar.RowUpdatedEventHandler(this.ctlNavBar_RowUpdated);
            this.ctlNavBar.RowUpdating += new Nistec.WinForms.McNavBar.RowUpdatingEventHandler(this.ctlNavBar_RowUpdating);
            this.ctlNavBar.RowDeleted += new System.Data.DataRowChangeEventHandler(this.ctlNavBar_RowDeleted);
            this.ctlNavBar.RowDeleting += new System.Data.DataRowChangeEventHandler(this.ctlNavBar_RowDeleting);
            this.ctlNavBar.NavBarUpdating += new Nistec.WinForms.NavBarUpdatingEventHandler(this.ctlNavBar_NavBarUpdating);
            this.ctlNavBar.Dirty += new System.EventHandler(this.ctlNavBar_Dirty);
            this.ctlNavBar.BindCurrentChanged += new System.EventHandler(this.ctlNavBar_BindCurrentChanged);
            this.ctlNavBar.ItemChanged += new System.Windows.Forms.ItemChangedEventHandler(this.ctlNavBar_ItemChanged);
            this.ctlNavBar.RowNew += new System.Data.DataTableNewRowEventHandler(ctlNavBar_RowNew);
            // 
            // McDataForm
            // 
            //this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            //this.ClientSize = new System.Drawing.Size(240, 237);
            this.Controls.Add(this.ctlNavBar);
            this.Name = "McDataForm";
            this.Text = "McDataForm";
            this.Controls.SetChildIndex(this.ctlNavBar, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}