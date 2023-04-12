using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinCtlTest
{
    public partial class FormMdi : Form
    {
        public FormMdi()
        {
            InitializeComponent();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.MdiParent = this;
            f.Show();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.MdiParent = this;
            f.Show();
      
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.MdiParent = this;
            f.Show();

        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.MdiParent = this;
            f.Show();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            Nistec.WinForms.InputBox.Open(this, "value", "enter", "mcontrol");
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            Nistec.WinForms.EmailDlg f = new Nistec.WinForms.EmailDlg();
            f.MdiParent = this;
            f.Show();
        }

        private void mcToolButton4_Click(object sender, EventArgs e)
        {

        }

        private void mcMultiBox1_ButtonClick(object sender, Nistec.WinForms.ButtonClickEventArgs e)
        {
            Nistec.WinForms.MsgDlg.ShowMsg(this.mcMemo1,"hello","");

            //StringInputDialog dlg = new StringInputDialog(this.Text);
            //dlg.Show();
        }
    }
}