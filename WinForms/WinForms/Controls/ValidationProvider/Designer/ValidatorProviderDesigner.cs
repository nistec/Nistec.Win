
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Nistec.WinForms
{
	/// <summary>
	/// ValidatorProviderDesigner add verbs for Validation setup on the PropertyGrid.
	/// </summary>
	internal class ValidatorProviderDesigner : System.ComponentModel.Design.ComponentDesigner
	{
		private IComponentChangeService			_ComponentChangeService = null;
		private IDesignerHost					_DesignerHost			= null;

		/// <summary>
		/// Default Ctor.
		/// </summary>
		public ValidatorProviderDesigner()
		{
			this.Verbs.Add(new DesignerVerb("Edit ValidationRules...", new EventHandler(this.EditValidationRulesHandler)));
		}

		/// <summary>
		/// Handle "Edit ValidationRules..." verb event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditValidationRulesHandler(object sender, EventArgs e)
        {
			this.CustomInitialize();
			ValidatorEditor vrdf = new ValidatorEditor(this._DesignerHost, this.Component as McValidator, null);
			vrdf.ShowDialog();

		}

		/// <summary>
		/// Makes sure local variables are valid.
		/// </summary>
		private void CustomInitialize()
		{
            //MessageBox.Show("vld1.0");
			if (this._DesignerHost == null)
				this._DesignerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            //MessageBox.Show("vld1.1");
			if (this._ComponentChangeService == null)
				this._ComponentChangeService = this._DesignerHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            //MessageBox.Show("vld1.2");
		}
	}
}
