using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using MControl.WinForms;
using MControl.Wizards.Controls;
using MControl.Util;

namespace MControl.Wizards.Controls
{
	/// <summary>
    /// Summary description for McMMC.
	/// </summary>
    [ToolboxItem(false)]//, ToolboxBitmap(typeof(McWizardControl), "Toolbox.McWizardControl.bmp")]
    public class McMMC : McUserControl
	{
        private MControl.Wizards.McTabPanels wizTabPanels;
        private McTabPage ctlPageConfig;
        private MControl.GridView.Grid gridConfig;
   		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public McMMC()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.wizTabPanels = new MControl.Wizards.McTabPanels();
            this.ctlPageConfig = new MControl.WinForms.McTabPage();
            this.gridConfig = new MControl.GridView.Grid();
            this.ctlPageConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // wizTabPanels
            // 
            this.wizTabPanels.ButtonImageList = null;
            this.wizTabPanels.ButtonMenuStyle = MControl.WinForms.ButtonMenuStyles.Button;
            this.wizTabPanels.ButttonCancelText = "Cancel";
            this.wizTabPanels.ButttonHelpText = "Help";
            this.wizTabPanels.ButttonUpdateText = "OK";
            this.wizTabPanels.CaptionFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            //this.wizTabPanels.CaptionImage = null;
            this.wizTabPanels.CaptionSubText = null;
            this.wizTabPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizTabPanels.ListCaption = "";
            this.wizTabPanels.ListWidth = 150;
            this.wizTabPanels.Location = new System.Drawing.Point(0, 0);
            this.wizTabPanels.Name = "wizTabPanels";
            this.wizTabPanels.Padding = new System.Windows.Forms.Padding(2);
            this.wizTabPanels.SelectedIndex = 0;
            this.wizTabPanels.Size = new System.Drawing.Size(572, 350);
            this.wizTabPanels.TabIndex = 1;
            this.wizTabPanels.WizardPages.AddRange(new MControl.WinForms.McTabPage[] {
            this.ctlPageConfig});
            this.wizTabPanels.WizardType = MControl.Wizards.WizardPanelTypes.Controller;
            // 
            // ctlPageConfig
            // 
            this.ctlPageConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlPageConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlPageConfig.Controls.Add(this.gridConfig);
            this.ctlPageConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPageConfig.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPageConfig.Location = new System.Drawing.Point(0, 0);
            this.ctlPageConfig.Name = "ctlPageConfig";
            this.ctlPageConfig.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctlPageConfig.Size = new System.Drawing.Size(414, 234);
            this.ctlPageConfig.TabIndex = 0;
            this.ctlPageConfig.Text = "MMC";
            this.ctlPageConfig.ToolTipText = "";
            this.ctlPageConfig.Visible = false;
            // 
            // gridConfig
            // 
            this.gridConfig.BackColor = System.Drawing.Color.White;
            this.gridConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridConfig.DataMember = "";
            this.gridConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.gridConfig.ForeColor = System.Drawing.Color.Black;
            this.gridConfig.Location = new System.Drawing.Point(4, 4);
            this.gridConfig.Name = "gridConfig";
            this.gridConfig.Size = new System.Drawing.Size(406, 226);
            this.gridConfig.TabIndex = 0;
            // 
            // McMMC
            // 
            this.Controls.Add(this.wizTabPanels);
            this.Name = "McMMC";
            this.Size = new System.Drawing.Size(572, 350);
            this.ctlPageConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridConfig)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

   
        #region properties

  
        public GridView.Grid GridConfig
        {
            get { return this.gridConfig; }
        }

        public Wizards.McTabPanels TabPanels
        {
            get { return this.wizTabPanels; }
        }

       public string CaptionText
        {
            get { return this.ctlPageConfig.Text; }
            set { this.ctlPageConfig.Text = value; }
        }



        #endregion

   
    }
}

