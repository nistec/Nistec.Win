using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MControl.Wizards.Forms;
using MControl.WinForms;

namespace MControl.Wizards.Permissions
{
    public partial class PermsUsersForm : NavBarListForm
    {
        public PermsUsersForm()
        {
            InitializeComponent();
            this.grid.AllowGridContextMenu = false;
        }

        private void InitControls()
        {
            this.tbCancel.Enabled = false;
            this.tbSave.Enabled = false;

            MControl.Wizards.DataAccess.DalLibrary dal = new MControl.Wizards.DataAccess.DalLibrary(base.DalBase);
            this.ctlList.ValueMember = "PermsGroupID";
            this.ctlList.DisplayMember = "PermsGroupName";

            this.ctlList.DataSource = dal.Users_Group();

            this.ColumnLvl.ValueMember = "Lvl";
            this.ColumnLvl.DisplayMember = "LvlName";

            this.ColumnLvl.DataSource = dal.Users_Lvl();

            this.ObjectID.ValueMember = "ObjectID";
            this.ObjectID.DisplayMember = "ObjectName";

            this.ObjectID.DataSource = dal.Users_UIObjects();

        }

        public override void SetDataSource(DataTable dt, string mappingName)
        {
            base.SetDataSource(dt, mappingName);
            this.grid.Init(base.DataSource, "", base.MappingName);
        }

        protected override void OnAsyncExecuteBegin(AsyncCallback callBack)
        {
            base.OnAsyncExecuteBegin(callBack);
            InitControls();
            this.MappingName = "Users_Permissions";
            MControl.Wizards.DataAccess.DalAsync dal = new MControl.Wizards.DataAccess.DalAsync(base.DalBase);
            dal.Users_Permissions(callBack);
        }

        protected override void OnAsyncDalCompleted(EventArgs e)
        {
            base.OnAsyncDalCompleted(e);
            this.grid.Init(base.DataSource, "", base.MappingName);
            OnRowSelectedChanged(EventArgs.Empty);
        }

 
        protected override void OnRowSelectedChanged(EventArgs e)
        {
            base.OnRowSelectedChanged(e);
            this.grid.SetFilter("PermsID=" + this.ctlList.SelectedValue.ToString());
        }

        protected override void OnToolBarClick(MControl.WinForms.ToolButtonClickEventArgs e)
        {
            base.OnToolBarClick(e);

            switch(e.Button.Tag.ToString())
            {
                case "New":

                    break;
                case "Remove":

                    break;
                case "Cancel":
                    this.grid.RejectChanges();
                    break;
                case "Save":
                    this.grid.UpdateChanges(base.Connection, this.grid.MappingName);
                    break;

            }
        }

        private void grid_DirtyChanged(object sender, EventArgs e)
        {
            this.tbSave.Enabled = grid.Dirty;
            this.tbCancel.Enabled = grid.Dirty;

        }

        private void tbSetAll_SelectedItemClick(object sender, MControl.WinForms.SelectedPopUpItemEvent e)
        {
            int value = e.Index;
            if (value <= 0)
                return;
            for (int i = 0; i < this.grid.RowCount; i++)
            {
                grid[i, 2] = value-1;
            }
        
        }
    }
}