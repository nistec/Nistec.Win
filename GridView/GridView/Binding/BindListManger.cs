using System;
using System.Collections;
using System.ComponentModel;


namespace mControl.WinCtl.Controls
{

	[DefaultEvent("CollectionChanged")]
	public class BindListManager : BindCollection
	{
		// Methods
		internal BindListManager(BindManagerBase bindingManagerBase)
		{
			this.bindingManagerBase = bindingManagerBase;
		}

		protected override void AddCore(Binder dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			if (dataBinding.BindManagerBase == this.bindingManagerBase)
			{
				throw new ArgumentException("BindingsCollectionAdd1", "dataBinding");
			}
			if (dataBinding.BindManagerBase != null)
			{
				throw new ArgumentException("BindingsCollectionAdd2", "dataBinding");
			}
			dataBinding.SetListManager(this.bindingManagerBase);
			base.AddCore(dataBinding);
		}

 
		protected override void ClearCore()
		{
			int num1 = this.Count;
			for (int num2 = 0; num2 < num1; num2++)
			{
				base[num2].SetListManager(null);
			}
			base.ClearCore();
		}

 
		protected override void RemoveCore(Binder dataBinding)
		{
			if (dataBinding.BindManagerBase != this.bindingManagerBase)
			{
				throw new ArgumentException("BindingsCollectionForeign");
			}
			dataBinding.SetListManager(null);
			base.RemoveCore(dataBinding);
		}

 

		// Fields
		private BindManagerBase bindingManagerBase;
	}
 
}