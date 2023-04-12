using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nistec.Printing.UI
{
    public partial class PrintDlg : Form
    {
        internal PrintDlg()
        {
            InitializeComponent();
        }

        public PrintDlg(McPrintDocument doc)
        {
            InitializeComponent();
            this.mcPrint1.Document=doc;
        }

        public PrintDlg(DataTable dt)
        {
            InitializeComponent();
            this.mcPrint1.CreateDocument(dt);
        }
 
    }
}