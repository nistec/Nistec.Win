using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mControl.WinCtl.Controls;
using mControl.WinCtl.Forms;
using mControl.Util;

namespace mControl.Samples
{
    public partial class CtlForm1 : mControl.WinCtl.Forms.CtlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public CtlForm1()
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            // TODO: Add any constructor code after InitializeComponent call
        }

        protected override bool Initialize(object[] args)
        {
            this.vGrid1.SetDataBinding(args[0], args[1].ToString());
            return true;
        }
    }
}