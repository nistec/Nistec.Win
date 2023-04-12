namespace Nistec.Charts.Web
{
    using System;
    using System.Collections;
    using System.ComponentModel.Design;
    using System.Web.UI.Design.WebControls;

    public class ChartDesigner :DataBoundControlDesigner //Nistec.Charts.Design.WebControlDesignerBase// 
    {
        private DesignerActionListCollection _actionLists;

        public new IEnumerable GetSampleDataSource()
        {
            return base.GetDesignTimeDataSource();
        }

        public void refreshSchema()
        {
            base.DataSourceDesigner.RefreshSchema(false);
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (this._actionLists == null)
                {
                    this._actionLists = new DesignerActionListCollection();
                    this._actionLists.AddRange(base.ActionLists);
                    this._actionLists.Add(new ActionList(this));
                }
                return this._actionLists;
            }
        }
    }
}

