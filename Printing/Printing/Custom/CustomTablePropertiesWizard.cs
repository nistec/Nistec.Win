namespace MControl.Data.Ado.CustomTable
{
    using Baycastle.DataSlave;
    using Baycastle.DataSlave.UI;
    using Baycastle.DataSlave.UI.Controls;
    using Baycastle.DataSlave.UI.Wizard;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CustomTablePropertiesWizard : WizardDialog
    {
        private IContainer components;
        private Panel pnlPage1;
        private SchemaEditor2 sedEditor;

        public CustomTablePropertiesWizard(bool tableIsEmpty)
        {
            this.InitializeComponent();
            base.Properties = new CustomTableProperties();
            base.TotalPages = 1;
            base.NextButtonEnabled = false;
            if (Preferences.HelpEnabled)
            {
                base.helpWizard.HelpNamespace = Runtime.ApplicationFolder + @"\DS_User_Guide.chm";
                base.helpWizard.SetHelpKeyword(this, @"Using_DB\UD_Custom.htm");
                base.helpWizard.SetHelpNavigator(this, HelpNavigator.Topic);
            }
            this.sedEditor.AllowMove = tableIsEmpty;
        }

        protected override void DisplayPage()
        {
            CustomTableProperties properties = base.Properties as CustomTableProperties;
            if (base.PageNumber == 1)
            {
                this.pnlPage1.Visible = true;
                base.PageTitle = "Table Schema";
                base.PageDescription = "Specify the custom tables structure.";
                this.sedEditor.Schema = properties.Schema;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlPage1 = new Panel();
            this.sedEditor = new SchemaEditor2();
            this.pnlPage1.SuspendLayout();
            base.SuspendLayout();
            base.pnlHeader.Name = "pnlHeader";
            base.pnlHeader.Size = new Size(0x214, 0x44);
            base.pnlFooter.Location = new Point(-8, 0x152);
            base.pnlFooter.Name = "pnlFooter";
            base.pnlFooter.Size = new Size(0x214, 0x30);
            this.pnlPage1.Controls.Add(this.sedEditor);
            this.pnlPage1.Location = new Point(0, 0x48);
            this.pnlPage1.Name = "pnlPage1";
            this.pnlPage1.Size = new Size(0x228, 0x108);
            this.pnlPage1.TabIndex = 14;
            this.sedEditor.Location = new Point(0, 0);
            this.sedEditor.Name = "sedEditor";
            this.sedEditor.Size = new Size(520, 0x108);
            this.sedEditor.TabIndex = 0;
            this.sedEditor.SchemaChanged += new EventHandler(this.sedEditor_Changed);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x206, 0x182);
            base.Controls.Add(this.pnlPage1);
            base.Name = "CustomTablePropertiesWizard";
            base.Controls.SetChildIndex(this.pnlPage1, 0);
            base.Controls.SetChildIndex(base.pnlHeader, 0);
            base.Controls.SetChildIndex(base.pnlFooter, 0);
            this.pnlPage1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override void PageChanged()
        {
            base.PageChanged();
            CustomTableProperties properties = base.Properties as CustomTableProperties;
            base.NextButtonEnabled = properties.Schema.Columns.Count > 0;
        }

        private void sedEditor_Changed(object sender, EventArgs e)
        {
            this.PageChanged();
        }

        protected override void ShowHelp()
        {
            Help.ShowHelp(this, Runtime.ApplicationFolder + @"\DS_User_Guide.chm", HelpNavigator.Topic, @"Using_DB\UD_Custom.htm");
        }
    }
}

