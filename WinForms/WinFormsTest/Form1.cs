using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.WinForms;
using Nistec.Data;
using Nistec;

namespace WinCtlTest
{
    public partial class Form1 : McForm
    {
        public Form1()
        {
            InitializeComponent();
          
            //Disable tpValidation By Permission control
            //this.ctlPermission1.SetPerms(this.tpValidation, PermsLevel.DenyAll);

            //InitPoUpMenu();
        }

    
        private PopUpItem selectedStyle;
        private PopUpItem selectedLayout;
        private PermsLevel currentLevel = PermsLevel.FullControl;

        Nistec.WinForms.McPopUp ctlPopUp;
        private void InitPoUpMenu()
        {
            bool editok = true;// ManagerUtil.Mngr.GetPerms(ObjectTypes.Design) > 1;
            ctlPopUp = new Nistec.WinForms.McPopUp(this);
            ctlPopUp.MaxDropDownItems = 9;
            ctlPopUp.ItemHeight = 32;
            //ctlPopUp.Tag = TreeName;//"Tables";
            ctlPopUp.ImageList = this.imageList1;// imagePopUp; ;
            //ctlPopUp.ItemHeight=18;
            ctlPopUp.MenuItems.AddItem("View All Rows", 0);
            ctlPopUp.MenuItems.AddItem("View Async Rows", 0);
            ctlPopUp.MenuItems.AddItem("View In New Form", 0);
            ctlPopUp.MenuItems.AddItem("View In Report", 0);
            ctlPopUp.MenuItems.AddItem("View Schema", null, 0, editok, true);
            ctlPopUp.MenuItems.AddItem("Select Statement", null, 0, editok, true);
            ctlPopUp.MenuItems.AddItem("Insert Statement", 0, editok);
            ctlPopUp.MenuItems.AddItem("Update Statement", 0, editok);
            ctlPopUp.MenuItems.AddItem("Create Statement", 0, editok);
        }

        private void toolBar_ButtonClick(object sender, ToolButtonClickEventArgs e)
        {
            
            PopUpItem item = e.Button.SelectedPopUpItem;
            bool styleChanged = false;
            bool layoutChanged = false;

            //ctlPopUp.ShowPopUp();
            
            switch (e.Button.Name)
            {
                case "tbProperty":
                    this.propertyGrid1.SelectedObject = this.ActiveControl;
                    break;
                case "tbResources":
                    break;
                case "tbPermission":
                    switch(currentLevel)
                    {
                        case PermsLevel.FullControl:
                            currentLevel = PermsLevel.ReadOnly;
                            break;
                        default:
                            currentLevel = PermsLevel.FullControl;
                            break;
                    }
                    //this.ctlPermission1.SetPerms(currentLevel);
                    //this.ctlPermission1.SetPerms(this.context1ToolStripMenuItem, PermsLevel.DenyAll); ;

                    NotifyWindow.ShowNotifyMsg(this.StylePainter, NotifyStyle.Msg, "Nistec","Now Permission Level Is:" + currentLevel.ToString());
                    if (currentLevel < PermsLevel.FullControl)
                    {
                        this.tbPermission.Enabled = true;
                    }

                    break;
                case "tbValidation":
                    break;
                case "tbStyles":
                    styleChanged = item != selectedStyle;
                    if (!styleChanged)
                        return;
                    ChangeStyle(item.Text);
                    if (selectedStyle != null)
                    {
                        selectedStyle.Checked = false; //tbStyles.MenuItems[3].Checked = false;
                    }
                    e.Button.SelectedPopUpItem.Checked = true;
                    selectedStyle = item;
                    break;
                case "tbControlLayout":
                    layoutChanged = item != selectedLayout;
                    if (!layoutChanged)
                        return;
                    ChangeLayout(item.Text);
                    if (selectedLayout != null)
                    {
                        selectedLayout.Checked = false;
                    }
                    e.Button.SelectedPopUpItem.Checked = true;
                    selectedLayout = item;
                    break;
            }
        }

        private void ChangeStyle(string s)
        {
            Styles style = (Styles)Enum.Parse(typeof(Styles), s, true);
            this.StyleGuideBase.StylePlan = style;

        }

        private void ChangeLayout(string s)
        {
            ControlLayout layout = (ControlLayout)Enum.Parse(typeof(ControlLayout), s, true);
            this.StyleGuideBase.SetControlLayout(this,layout);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            //MsgBox.ShowInfo("info");
            MsgDlg.ShowMsg("This is Info messgae");
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            //MsgBox.ShowError("error");
            MsgDlg.ShowDialog("This is a messgae dialog","mComtrol");
        }

        private void btnProgress1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                this.ctlProgressBar1.Value = i;
            }

        }

        private void btnProgress2_Click(object sender, EventArgs e)
        {
            this.ctlProgressBar2.DoLoop();

        }

        private void linkShowNotify_Click(object sender, EventArgs e)
        {
            NotifyWindow.ShowNotifyMsg(this.StylePainter, NotifyStyle.Msg, "Nistec", "This is a Notify message");
        }

        private void linkShowMsg_Click(object sender, EventArgs e)
        {
            MsgDlg.ShowDialog("This is a message Dialog.", "Nistec");
        }

        private void linkInputBox_Click(object sender, EventArgs e)
        {
            InputBox.Open(this, "Enter Ys/No", "This is a Input Box message Dialog.", "Nistec");
            //Nistec.WinCtl.Dlg.MsgDlg.OpenInputBox(this.linkInputBox, "Enter Ys/No", "This is a Input Box message Dialog.", "Nistec");
        }

        private void dockPanelLeft_Paint(object sender, PaintEventArgs e)
        {

        }

    
   
     

    
    }
}