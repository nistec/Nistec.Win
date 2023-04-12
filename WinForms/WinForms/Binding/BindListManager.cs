using System;
using System.Collections;
using System.ComponentModel;


namespace Nistec.WinForms
{
	/// <summary>
	/// Summary description for BindListManager.
	/// </summary>
	[DefaultEvent("CollectionChanged")]
	internal class BindListManager : BindCollection
	{
		// Fields
		private BindManagerBase bindingManagerBase;

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
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				base[i].SetListManager(null);
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
	}

 
}
