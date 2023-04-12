using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.WinForms;
using Nistec.GridView;

namespace GridViewTest
{
    public partial class Form4 : McForm
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VGridDlg dlg = new VGridDlg(this.StylePainter);
            dlg.Show();
        }
    }
}