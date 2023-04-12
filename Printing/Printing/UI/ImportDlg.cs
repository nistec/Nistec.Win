using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nistec.Printing.UI
{
    public partial class ImportDlg : Form
    {
        public ImportDlg()
        {
            InitializeComponent();
        }

        public DataTable Source
        {
            get { return this.mcImport1.Source; }
        }

    }
}