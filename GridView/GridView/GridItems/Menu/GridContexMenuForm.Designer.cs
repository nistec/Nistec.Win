namespace mControl.GridView.Controls
{
    partial class GridContexMenuForm
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmChart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmFit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmColumnFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.cmFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.cmFind = new System.Windows.Forms.ToolStripMenuItem();
            this.cmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSummarize = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmFit,
            this.toolStripSeparator1,
            this.cmColumnFilter,
            this.cmFilter,
            this.cmFind,
            this.toolStripSeparator2,
            this.cmExport,
            this.cmPreview,
            this.toolStripSeparator3,
            this.cmRecord,
            this.cmSummarize,
            this.cmChart});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 242);
            // 
            // cmChart
            // 
            this.cmChart.Image = global::mControl.GridView.Properties.Resources.barcha2;
            this.cmChart.Name = "cmChart";
            this.cmChart.Size = new System.Drawing.Size(152, 22);
            this.cmChart.Text = "Pie Chart";
            this.cmChart.Click += new System.EventHandler(this.cmChart_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // cmFit
            // 
            this.cmFit.Image = global::mControl.GridView.Properties.Resources.fit_to_size;
            this.cmFit.Name = "cmFit";
            this.cmFit.Size = new System.Drawing.Size(152, 22);
            this.cmFit.Text = "Fit To Width";
            this.cmFit.Click += new System.EventHandler(this.cmFit_Click);
            // 
            // cmColumnFilter
            // 
            this.cmColumnFilter.Image = global::mControl.GridView.Properties.Resources.column;
            this.cmColumnFilter.Name = "cmColumnFilter";
            this.cmColumnFilter.Size = new System.Drawing.Size(152, 22);
            this.cmColumnFilter.Text = "Columns Filter";
            this.cmColumnFilter.Click += new System.EventHandler(this.cmColumnFilter_Click);
            // 
            // cmFilter
            // 
            this.cmFilter.Image = global::mControl.GridView.Properties.Resources.filter;
            this.cmFilter.Name = "cmFilter";
            this.cmFilter.Size = new System.Drawing.Size(152, 22);
            this.cmFilter.Text = "Filter";
            this.cmFilter.Click += new System.EventHandler(this.cmFilter_Click);
            // 
            // cmFind
            // 
            this.cmFind.Image = global::mControl.GridView.Properties.Resources.search;
            this.cmFind.Name = "cmFind";
            this.cmFind.Size = new System.Drawing.Size(152, 22);
            this.cmFind.Text = "Find";
            this.cmFind.Click += new System.EventHandler(this.cmFind_Click);
            // 
            // cmExport
            // 
            this.cmExport.Image = global::mControl.GridView.Properties.Resources.Export;
            this.cmExport.Name = "cmExport";
            this.cmExport.Size = new System.Drawing.Size(152, 22);
            this.cmExport.Text = "Export";
            this.cmExport.Click += new System.EventHandler(this.cmExport_Click);
            // 
            // cmPreview
            // 
            this.cmPreview.Image = global::mControl.GridView.Properties.Resources.printp1;
            this.cmPreview.Name = "cmPreview";
            this.cmPreview.Size = new System.Drawing.Size(152, 22);
            this.cmPreview.Text = "Preview";
            this.cmPreview.Click += new System.EventHandler(this.cmPreview_Click);
            // 
            // cmRecord
            // 
            this.cmRecord.Image = global::mControl.GridView.Properties.Resources.record;
            this.cmRecord.Name = "cmRecord";
            this.cmRecord.Size = new System.Drawing.Size(152, 22);
            this.cmRecord.Text = "Active Record";
            this.cmRecord.Click += new System.EventHandler(this.cmRecord_Click);
            // 
            // cmSummarize
            // 
            this.cmSummarize.Image = global::mControl.GridView.Properties.Resources.sum;
            this.cmSummarize.Name = "cmSummarize";
            this.cmSummarize.Size = new System.Drawing.Size(152, 22);
            this.cmSummarize.Text = "Summarize Grid";
            this.cmSummarize.Click += new System.EventHandler(this.cmSummarize_Click);
            // 
            // GridContexMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 335);
            this.MaximizeBox = false;
            this.Name = "GridContexMenu";
            this.Text = "GridContexMenu";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmColumnFilter;
        private System.Windows.Forms.ToolStripMenuItem cmRecord;
        private System.Windows.Forms.ToolStripMenuItem cmExport;
        private System.Windows.Forms.ToolStripMenuItem cmPreview;
        private System.Windows.Forms.ToolStripMenuItem cmFilter;
        private System.Windows.Forms.ToolStripMenuItem cmFind;
        private System.Windows.Forms.ToolStripMenuItem cmSummarize;
        private System.Windows.Forms.ToolStripMenuItem cmFit;
        private System.Windows.Forms.ToolStripMenuItem cmChart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

    }
}