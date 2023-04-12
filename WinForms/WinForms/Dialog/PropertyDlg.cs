using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nistec.WinForms
{
    public partial class PropertyDlg : Nistec.WinForms.McForm
    {
        public static bool IsOpen = false;

        public static void Open(object obj)
        {
            PropertyDlg pd = new PropertyDlg(obj);
            pd.Show();
        }

        public PropertyDlg(object obj)
            : this(obj, null, false)
        {
        }

        public PropertyDlg(object obj, string captionText)
            : this(obj, captionText,false)
        {
        }

        public PropertyDlg(object obj, string captionText, bool commandsVisible)
        {
            InitializeComponent();
            SelectObject(obj);
            if(string.IsNullOrEmpty(captionText))
                this.Caption.Text = captionText;
            this.propertyGrid1.CommandsVisibleIfAvailable = commandsVisible;
        }


        public void SelectObject(object obj)
        {
            propertyGrid1.SelectedObject = obj;
        }
 
        public void SelectObject(object obj, string captionSubText)
        {
            propertyGrid1.SelectedObject = obj;
            this.Caption.SubText = captionSubText;
        }

        public PropertyGrid PropertyGrid
        {
            get { return propertyGrid1; }
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