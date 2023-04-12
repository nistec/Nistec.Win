using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MControl.Wizards.Forms;

namespace MControl.Wizards.Permissions
{
    public partial class UsersForm : NavBarListForm
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        private void InitControls()
        {
            MControl.Wizards.DataAccess.DalLibrary dal = new MControl.Wizards.DataAccess.DalLibrary(base.DalBase);
            this.ctlPermsGroup.ValueMember = "PermsGroupID";
            this.ctlPermsGroup.DisplayMember = "PermsGroupName";
         
            this.ctlPermsGroup.DataSource = dal.Users_Group();
        }

        protected override void OnAsyncExecuteBegin(AsyncCallback callBack)
        {
            base.OnAsyncExecuteBegin(callBack);
            InitControls();

            this.MappingName = "Users";
            MControl.Wizards.DataAccess.DalAsync dal = new MControl.Wizards.DataAccess.DalAsync(base.DalBase);
            dal.Users(callBack);
        }
        protected override void OnAsyncDalCompleted(EventArgs e)
        {
            base.OnAsyncDalCompleted(e);
            this.ctlList.ValueMember = "UserID";
            this.ctlList.DisplayMember = "UserName";
     
            this.ctlList.DataSource = DataSource;
            OnPositionChanged(EventArgs.Empty);
        }

        protected override void OnPositionChanged(EventArgs e)
        {
            base.OnPositionChanged(e);

            this.ctlNavBar.ShowDelete = this.ctlUserID.GetIntValue()>3;

        }

    }
}