using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinCtlTest
{
    public partial class Form3 : Nistec.WinForms.McForm //Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}