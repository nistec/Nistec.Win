namespace mControl.GridView.Controls
{
    partial class GridCtlContexMenuForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridCtlContexMenu));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmFilterBySelection = new System.Windows.Forms.ToolStripMenuItem();
            this.cmFilterText = new System.Windows.Forms.ToolStripTextBox();
            this.cmRemoveFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmUndo,
            this.toolStripSeparator2,
            this.cmCut,
            this.cmCopy,
            this.cmPaste,
            this.cmDelete,
            this.toolStripSeparator1,
            this.cmSelectAll,
            this.toolStripSeparator3,
            this.cmFilterBySelection,
            this.cmFilterText,
            this.cmRemoveFilter});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 243);
            // 
            // cmUndo
            // 
            this.cmUndo.Image = global::mControl.GridView.Properties.Resources.undo;
            this.cmUndo.Name = "cmUndo";
            this.cmUndo.Size = new System.Drawing.Size(160, 22);
            this.cmUndo.Text = "Undo";
            this.cmUndo.Click += new System.EventHandler(this.cmUndo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // cmCut
            // 
            this.cmCut.Image = global::mControl.GridView.Properties.Resources.cut;
            this.cmCut.Name = "cmCut";
            this.cmCut.Size = new System.Drawing.Size(160, 22);
            this.cmCut.Text = "Cut";
            this.cmCut.Click += new System.EventHandler(this.cmCut_Click);
            // 
            // cmCopy
            // 
            this.cmCopy.Image = global::mControl.GridView.Properties.Resources.copy;
            this.cmCopy.Name = "cmCopy";
            this.cmCopy.Size = new System.Drawing.Size(160, 22);
            this.cmCopy.Text = "Copy";
            this.cmCopy.Click += new System.EventHandler(this.cmCopy_Click);
            // 
            // cmPaste
            // 
            this.cmPaste.Image = global::mControl.GridView.Properties.Resources.paste;
            this.cmPaste.Name = "cmPaste";
            this.cmPaste.Size = new System.Drawing.Size(160, 22);
            this.cmPaste.Text = "Paste";
            this.cmPaste.Click += new System.EventHandler(this.cmPaste_Click);
            // 
            // cmDelete
            // 
            this.cmDelete.Image = global::mControl.GridView.Properties.Resources.delete;
            this.cmDelete.Name = "cmDelete";
            this.cmDelete.Size = new System.Drawing.Size(160, 22);
            this.cmDelete.Text = "Delete";
            this.cmDelete.Click += new System.EventHandler(this.cmDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // cmSelectAll
            // 
            this.cmSelectAll.Image = global::mControl.GridView.Properties.Resources.select1;
            this.cmSelectAll.Name = "cmSelectAll";
            this.cmSelectAll.Size = new System.Drawing.Size(160, 22);
            this.cmSelectAll.Text = "Select All";
            this.cmSelectAll.Click += new System.EventHandler(this.cmSelectAll_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // cmFilterBySelection
            // 
            this.cmFilterBySelection.Image = global::mControl.GridView.Properties.Resources.filter2;
            this.cmFilterBySelection.Name = "cmFilterBySelection";
            this.cmFilterBySelection.Size = new System.Drawing.Size(160, 22);
            this.cmFilterBySelection.Text = "Filter By Selection";
            this.cmFilterBySelection.Click += new System.EventHandler(this.cmFilterBySelection_Click);
            // 
            // cmFilterText
            // 
            this.cmFilterText.Name = "cmFilterText";
            this.cmFilterText.Size = new System.Drawing.Size(100, 21);
            this.cmFilterText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmFilterText_KeyUp);
            // 
            // cmRemoveFilter
            // 
            this.cmRemoveFilter.Image = global::mControl.GridView.Properties.Resources.filterRemove;
            this.cmRemoveFilter.Name = "cmRemoveFilter";
            this.cmRemoveFilter.Size = new System.Drawing.Size(160, 22);
            this.cmRemoveFilter.Text = "Remove Filter";
            this.cmRemoveFilter.Click += new System.EventHandler(this.cmRemoveFilter_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "undo.gif");
            this.imageList1.Images.SetKeyName(1, "cut.gif");
            this.imageList1.Images.SetKeyName(2, "copy.gif");
            this.imageList1.Images.SetKeyName(3, "paste.gif");
            this.imageList1.Images.SetKeyName(4, "delete.gif");
            this.imageList1.Images.SetKeyName(5, "select1.gif");
            this.imageList1.Images.SetKeyName(6, "filter2.gif");
            this.imageList1.Images.SetKeyName(7, "filter.gif");
            this.imageList1.Images.SetKeyName(8, "filterRemove.gif");
            this.imageList1.Images.SetKeyName(9, "Export.gif");
            this.imageList1.Images.SetKeyName(10, "fit_to_size.gif");
            this.imageList1.Images.SetKeyName(11, "printp1.gif");
            this.imageList1.Images.SetKeyName(12, "record.gif");
            this.imageList1.Images.SetKeyName(13, "space.gif");
            // 
            // GridCtlContexMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 335);
            this.MaximizeBox = false;
            this.Name = "GridCtlContexMenu";
            this.Text = "GridContexMenu";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmCopy;
        private System.Windows.Forms.ToolStripMenuItem cmPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox cmFilterText;
        private System.Windows.Forms.ToolStripMenuItem cmCut;
        private System.Windows.Forms.ToolStripMenuItem cmSelectAll;
        private System.Windows.Forms.ToolStripMenuItem cmDelete;
        private System.Windows.Forms.ToolStripMenuItem cmUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cmFilterBySelection;
        private System.Windows.Forms.ToolStripMenuItem cmRemoveFilter;
        private System.Windows.Forms.ImageList imageList1;
    }
}