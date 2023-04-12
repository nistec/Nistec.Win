using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using mControl.WinForms;
using mControl.WinUI.Controls;
using mControl.Util;

namespace mControl.WinUI.Controls
{
    public class McExplorer:mControl.WinUI.McExplorer
    {
        private IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(McExplorer));
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // topCaption
            // 
            this.topCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.topCaption.ControlLayout = mControl.WinForms.ControlLayout.Visual;
            this.topCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.topCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.topCaption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.topCaption.Image = ((System.Drawing.Image)(resources.GetObject("topCaption.Image")));
            this.topCaption.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.topCaption.Location = new System.Drawing.Point(2, 2);
            this.topCaption.Name = "topCaption";
            this.topCaption.SubText = "";
            this.topCaption.Text = "Tab Tree Wizard";
            // 
            // statusBar
            // 
            this.statusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBar.ControlLayout = mControl.WinForms.ControlLayout.Visual;
            this.statusBar.ForeColor = System.Drawing.Color.Black;
            //this.statusBar.HScrollPosition = 0;
            this.statusBar.Location = new System.Drawing.Point(2, 184);
            this.statusBar.Name = "statusBar";
            this.statusBar.ProgressValue = 0;
            this.statusBar.Size = new System.Drawing.Size(249, 24);
            this.statusBar.StartPanelPosition = 0;
            this.statusBar.TabIndex = 9;
            // 
            // lblList
            // 
            this.lblList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.lblList.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblList.ForeColor = System.Drawing.Color.Black;
            this.lblList.Location = new System.Drawing.Point(2, 2);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(116, 20);
            // 
            // McExplorer
            // 
            this.CaptionImage = ((System.Drawing.Image)(resources.GetObject("$this.CaptionImage")));
            this.Name = "McExplorer";
            this.toolBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
