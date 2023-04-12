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
    public partial class PermsGroups : AsyncDalForm
    {
        public PermsGroups()
        {
            InitializeComponent();
        }

        private void InitControls()
        {
            this.tbUpdate.Enabled = false;
            this.tbCancel.Enabled = false;

        }

        public override void SetDataSource(DataTable dt, string mappingName)
        {
            base.SetDataSource(dt,mappingName);
            this.grid.Init(base.DataSource, "", base.MappingName);
        }

        protected override void OnAsyncExecuteBegin(AsyncCallback callBack)
        {
            base.OnAsyncExecuteBegin(callBack);
            InitControls();

            this.MappingName = "Users_Group";
            MControl.Wizards.DataAccess.DalAsync dal = new MControl.Wizards.DataAccess.DalAsync(base.DalBase);
            dal.Users_Group(callBack);
        }

        protected override void OnAsyncDalCompleted(EventArgs e)
        {
            base.OnAsyncDalCompleted(e);
            this.grid.Init(base.DataSource, "", base.MappingName);
        }
 
        private void ctlToolBar_ButtonClick(object sender, MControl.WinForms.ToolButtonClickEventArgs e)
        {
            OnToolBarClick(e);
        }

        protected virtual void OnToolBarClick(MControl.WinForms.ToolButtonClickEventArgs e)
        {
            switch (e.Button.Tag.ToString())
            {
                case "Exit":
                    this.Close();
                    break;
                case "Cancel":
                    this.grid.RejectChanges();
                    break;
                case "Update":
                    this.grid.UpdateChanges(base.Connection, this.grid.MappingName);
                    break;
            }
        }

        private void grid_DirtyChanged(object sender, EventArgs e)
        {
            this.tbUpdate.Enabled = grid.Dirty;
            this.tbCancel.Enabled = grid.Dirty;

        }

        private void grid_CurrentRowChanged(object sender, EventArgs e)
        {
            this.grid.AllowRemove = !Types.ToBool(this.grid[2], false);
        }
       
    }
}