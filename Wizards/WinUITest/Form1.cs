using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WizardsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.mcTabPanels1.AddPage("page123",2);
            this.mcTabPanels1.AddPage("page567", 4);
           
            //this.mcTabPanels1.WizardPages.AddRange(new MControl.WinForms.McTabPage[] { new MControl.WinForms.McTabPage("page1"), new MControl.WinForms.McTabPage("page2") });
        }
    }

   
}