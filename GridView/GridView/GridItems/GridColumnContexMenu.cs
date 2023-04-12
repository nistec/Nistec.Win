using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.Win;


namespace Nistec.GridView
{
    /// <summary>
    /// Grid Column ContexMenu
    /// </summary>
    public class GridColumnContexMenu : System.Windows.Forms.ContextMenuStrip
    {
        private Grid grid;
        /// <summary>
        /// Initilaized GridColumnContexMenu
        /// </summary>
        /// <param name="g"></param>
        public GridColumnContexMenu(Grid g)
        {
            InitializeComponent();
            grid = g;
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridColumnContexMenu));
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
            this.SuspendLayout();
            // 
            // cmUndo
            // 
            this.cmUndo.Image = global::Nistec.GridView.Properties.Resources.undo;
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
            this.cmCut.Image = global::Nistec.GridView.Properties.Resources.cut;
            this.cmCut.Name = "cmCut";
            this.cmCut.Size = new System.Drawing.Size(160, 22);
            this.cmCut.Text = "Cut";
            this.cmCut.Click += new System.EventHandler(this.cmCut_Click);
            // 
            // cmCopy
            // 
            this.cmCopy.Image = global::Nistec.GridView.Properties.Resources.copy;
            this.cmCopy.Name = "cmCopy";
            this.cmCopy.Size = new System.Drawing.Size(160, 22);
            this.cmCopy.Text = "Copy";
            this.cmCopy.Click += new System.EventHandler(this.cmCopy_Click);
            // 
            // cmPaste
            // 
            this.cmPaste.Image = global::Nistec.GridView.Properties.Resources.paste;
            this.cmPaste.Name = "cmPaste";
            this.cmPaste.Size = new System.Drawing.Size(160, 22);
            this.cmPaste.Text = "Paste";
            this.cmPaste.Click += new System.EventHandler(this.cmPaste_Click);
            // 
            // cmDelete
            // 
            this.cmDelete.Image = global::Nistec.GridView.Properties.Resources.delete;
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
            this.cmSelectAll.Image = global::Nistec.GridView.Properties.Resources.select1;
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
            this.cmFilterBySelection.Image = global::Nistec.GridView.Properties.Resources.filter2;
            this.cmFilterBySelection.Name = "cmFilterBySelection";
            this.cmFilterBySelection.Size = new System.Drawing.Size(160, 22);
            this.cmFilterBySelection.Text = "Filter By Selection";
            this.cmFilterBySelection.Click += new System.EventHandler(this.cmFilterBySelection_Click);
            // 
            // cmFilterText
            // 
            //this.cmFilterText.Image = global::Nistec.GridView.Properties.Resources.filterSelection;
            this.cmFilterText.BorderStyle = BorderStyle.FixedSingle;
            this.cmFilterText.Name = "cmFilterText";
            this.cmFilterText.Size = new System.Drawing.Size(100, 21);
            this.cmFilterText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmFilterText_KeyUp);
            // 
            // cmRemoveFilter
            // 
            this.cmRemoveFilter.Image = global::Nistec.GridView.Properties.Resources.filterRemove;
            this.cmRemoveFilter.Name = "cmRemoveFilter";
            this.cmRemoveFilter.Size = new System.Drawing.Size(160, 22);
            this.cmRemoveFilter.Text = "Remove Filter";
            this.cmRemoveFilter.Click += new System.EventHandler(this.cmRemoveFilter_Click);
            // 
            // GridColumnContexMenu
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Size = new System.Drawing.Size(161, 243);
            this.Name = "GridColumnContexMenu";
            this.Text = "GridContexMenu";
            this.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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

        /// <summary>
        /// Raises the Opening event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnOpening(CancelEventArgs e)
        {
            base.OnOpening(e);
            IDataObject objPresumably = Clipboard.GetDataObject();
            bool isData = objPresumably != null;
            bool allowEdit = !grid.ReadOnly;
            bool canUndo=false;
            //Proceed if some copied data is present
            //if (objPresumably != null)
            //{
            //    //Next proceed only of the copied data is in the CSV format indicating Excel content
            //    if (objPresumably.GetDataPresent(DataFormats.CommaSeparatedValue))
            //    {
            //        return true;
            //    }
            //}
            Control c= grid.FindForm().ActiveControl;
            if (c != null)
            {
                if (c is GridTextBox)
                    canUndo = ((GridTextBox)c).CanUndo;
            }
            this.cmPaste.Enabled = isData && allowEdit;
            this.cmCut.Enabled = isData && allowEdit;
            this.cmDelete.Enabled = isData && allowEdit;
            this.cmUndo.Enabled = canUndo;

        }
        
        private void cmUndo_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
        }

        private void cmCut_Click(object sender, EventArgs e)
        {
            //SendKeys.Send("^X");

            Control c = grid.GetCurrentColumnControl(false);
            if (c != null)
            {
              Clipboard.SetText( c.Text);
              c.Text = "";
            }
        }

        private void cmCopy_Click(object sender, EventArgs e)
        {
            //SendKeys.Send("^C");
            Control c = grid.GetCurrentColumnControl(true);
            if (c != null)
            {
                Clipboard.SetText(c.Text);
            }
        }

        private void cmPaste_Click(object sender, EventArgs e)
        {
           // SendKeys.Send("^V");
            grid.SetCurrentColumnValue(false, Clipboard.GetText());
            //Control c = grid.GetCurrentColumnControl(false);
            //if (c != null)
            //{
            //    c.Text = Clipboard.GetText();
            //}
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{DELETE}");
        }

        private void cmSelectAll_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^A");
        }

        private void cmFilterBySelection_Click(object sender, EventArgs e)
        {
            try
            {
                GridColumnCollection coll = grid.GridColumns;
                GridColumnStyle style = coll[grid.currentCol];
                if (style == null)
                    return;
                string mappingName = style.MappingName;
                if (mappingName.Length == 0)
                    return;
                object val = this.grid[mappingName];
                if (val == null)
                    return;
                string filter = GridPerform.GetValidFilter("", mappingName, val.ToString(), "=");
                //string filter = mappingName + "=" + val.ToString();
                this.grid.SetFilter(filter);
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
            }
        }

        private void cmFilterText_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    string filter = this.cmFilterText.Text;
                    if (filter.Length == 0)
                        return;
                    GridColumnStyle col= grid.GridColumns[grid.currentCol];
                    if (col != null)
                    {
                        if ((filter.Contains("*")))
                        {
                            filter = col.MappingName + " LIKE '%" + filter.Replace("*", "") + "%'";
                        }
                        else if (!(filter.Contains("=") || filter.Contains(">") || filter.Contains("<") || filter.ToUpper().Contains("LIKE")))
                        {
                            filter = col.MappingName + "=" + filter;
                        }
                    }

                    this.grid.SetFilter(filter);
                    this.cmFilterText.Text = "";
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
            }
        }

        private void cmRemoveFilter_Click(object sender, EventArgs e)
        {
            this.grid.RemoveFilter();

        }
    }
}