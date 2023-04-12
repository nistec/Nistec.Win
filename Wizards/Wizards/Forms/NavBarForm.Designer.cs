namespace MControl.Wizards.Forms
{
    partial class NavBarForm
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
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlNavBar
            // 
            this.ctlNavBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlNavBar.Connection = null;
            this.ctlNavBar.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            this.ctlNavBar.DBProvider = MControl.Data.DBProvider.SqlServer;
            this.ctlNavBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlNavBar.EnableButtons = false;
            this.ctlNavBar.Location = new System.Drawing.Point(2, 220);
            this.ctlNavBar.ManagerDataAdapter = null;
            this.ctlNavBar.MessageDelete = "Delete";
            this.ctlNavBar.MessageSaveChanges = "SaveChanges";
            this.ctlNavBar.Name = "ctlNavBar";
            this.ctlNavBar.Padding = new System.Windows.Forms.Padding(2);
            this.ctlNavBar.ShowChangesButtons = true;
            this.ctlNavBar.Size = new System.Drawing.Size(271, 24);
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
            // NavBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(275, 246);
            this.Name = "NavBarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NavBar Form";
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}