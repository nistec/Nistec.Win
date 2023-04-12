using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nistec.Printing.UI
{
    public partial class ExportDlg : Form
    {
        public ExportDlg(DataTable dt)
        {
            InitializeComponent();
            mcExport1.Source = dt;
        }
    }
}