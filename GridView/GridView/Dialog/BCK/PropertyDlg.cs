using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mControl.GridView
{
    public partial class PropertyDlg : mControl.WinCtl.Forms.CtlForm
    {
        public static bool IsOpen = false;

        public static void ColumnPropertyShow(GridColumnStyle column)
        {
            PropertyDlg pd = new PropertyDlg(column);
            pd.Show();
        }

        //public event EventHandler Closed;

        public PropertyDlg(GridColumnStyle column)
        {
            InitializeComponent();
            SelectObject(column);
         }

        public void SelectObject(GridColumnStyle column)
        {
            propertyGrid1.SelectedObject = column;
            this.CaptionSubText = column.HeaderText;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PropertyDlg.IsOpen = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            PropertyDlg.IsOpen = false;
        }
    }
}