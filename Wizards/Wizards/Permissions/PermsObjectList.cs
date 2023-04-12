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
    public partial class PermsObjectList : AsyncDalForm
    {
        public PermsObjectList()
        {
            InitializeComponent();
        }

        private void InitControls()
        {
            this.tbUpdate.Enabled = false;
            this.tbCancel.Enabled = false;
            MControl.Wizards.DataAccess.DalLibrary dal = new MControl.Wizards.DataAccess.DalLibrary(base.DalBase);
            this.UItype.ValueMember = "UItypeID";
            this.UItype.DisplayMember = "UITypeName";

            this.UItype.DataSource = dal.Users_UITypes();

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

            this.MappingName = "Users_UIObjects";
            MControl.Wizards.DataAccess.DalAsync dal = new MControl.Wizards.DataAccess.DalAsync(base.DalBase);
            dal.Users_UIObjects(callBack);
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
       
    }
}