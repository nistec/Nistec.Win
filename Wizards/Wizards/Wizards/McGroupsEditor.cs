using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Nistec.WinForms;

namespace Nistec.Wizards.Controls
{
    [ToolboxItem(true), ToolboxBitmap(typeof(McGroupsEditor), "Toolbox.McGroupsEditor.bmp")]
    public partial class McGroupsEditor : Nistec.WinForms.Controls.McContainer
    {
        public McGroupsEditor()
        {
            InitializeComponent();
        }

        public event EventHandler ButtonRightClick;
        public event EventHandler ButtonAllRightToLeft_Click;
        public event EventHandler ButtonRightToLeft_Click;
        public event EventHandler ButtonLeftToRight_Click;
        public event EventHandler ButtonAllLeftToRight_Click;
        public event EventHandler ButtonLeft_Click;
        public event EventHandler ComboRight_SelectedIndexChanged;
        public event EventHandler ComboLeft_SelectedIndexChanged;
        public event EventHandler ListRight_SelectedIndexChanged;
        public event EventHandler ListLeft_SelectedIndexChanged;
  
     

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (ButtonRightClick != null)
                ButtonRightClick(this, e);
        }

        private void btnAllRightToLeft_Click(object sender, EventArgs e)
        {
            if (ButtonAllRightToLeft_Click != null)
                ButtonAllRightToLeft_Click(this, e);

        }

        private void btnRightToLeft_Click(object sender, EventArgs e)
        {
            if (ButtonRightToLeft_Click != null)
                ButtonRightToLeft_Click(this, e);

        }

        private void btnLeftToRight_Click(object sender, EventArgs e)
        {
            if (ButtonLeftToRight_Click != null)
                ButtonLeftToRight_Click(this, e);

        }

        private void btnAllLeftToRight_Click(object sender, EventArgs e)
        {
            if (ButtonAllLeftToRight_Click != null)
                ButtonAllLeftToRight_Click(this, e);

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (ButtonLeft_Click != null)
                ButtonLeft_Click(this, e);

        }

        private void ctlComboRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboRight_SelectedIndexChanged != null)
                ComboRight_SelectedIndexChanged(this, e);

        }

        private void ctlComboLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboLeft_SelectedIndexChanged != null)
                ComboLeft_SelectedIndexChanged(this, e);

        }

        private void ctlListBoxRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListRight_SelectedIndexChanged != null)
                ListRight_SelectedIndexChanged(this, e);

        }

        private void ctlListBoxLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListLeft_SelectedIndexChanged != null)
                ListLeft_SelectedIndexChanged(this, e);

        }

  
    }
}
