using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mControl.WinCtl.Dlg
{
    public partial class MsgDialog : Form
    {
        public MsgDialog()
        {
            InitializeComponent();
        }

        public MsgDialog(string msg, string caption)
        {
            InitializeComponent();
            this.richText.Text=msg;
            this.Text = caption;
        }

        public static void Open(string msg,string caption)
        {
            MsgDialog td = new MsgDialog(msg,caption);
            td.Show();
        }

        public static void OpenDialog(string msg, string caption)
        {
            MsgDialog td = new MsgDialog(msg, caption);
            td.ShowDialog();
        }

    }
}