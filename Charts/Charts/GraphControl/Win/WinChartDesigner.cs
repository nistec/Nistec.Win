namespace Nistec.Charts.Win
{
    using System.ComponentModel.Design;
    using System.Windows.Forms.Design;

    internal class WinChartDesigner :ControlDesigner// Nistec.Charts.Design.ControlDesignerBase//
    {
        private DesignerActionListCollection _actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (this._actionLists == null)
                {
                    this._actionLists = new DesignerActionListCollection();
                    this._actionLists.AddRange(base.ActionLists);
                    this._actionLists.Add(new WinActionList(this));
                }
                return this._actionLists;
            }
        }
    }
}

