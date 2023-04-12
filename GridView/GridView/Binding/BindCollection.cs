using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace mControl.WinCtl.Controls
{

	[DefaultEvent("CollectionChanged")]
	public class BindCollection : BaseCollection
	{
		// Events
		[Description("collectionChangedEvent")]
		public event CollectionChangeEventHandler CollectionChanged;

		// Methods
		internal BindCollection()
		{
		}

 
		protected internal void Add(Binder binding)
		{
			this.AddCore(binding);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, binding));
		}

 
		protected virtual void AddCore(Binder dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			this.List.Add(dataBinding);
		}

 
		protected internal void Clear()
		{
			this.ClearCore();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		protected virtual void ClearCore()
		{
			this.List.Clear();
		}

		private void OnBadIndex(object index)
		{
			throw new IndexOutOfRangeException("BindingsCollectionBadIndex " + index.ToString() );
		}

 
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, ccevent);
			}
		}

		protected internal void Remove(Binder binding)
		{
			this.RemoveCore(binding);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, binding));
		}

		protected internal void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

 
		protected virtual void RemoveCore(Binder dataBinding)
		{
			this.List.Remove(dataBinding);
		}

 
		protected internal bool ShouldSerializeMyAll()
		{
			return (this.Count > 0);
		}

		public override int Count
		{
			get
			{
				if (this.list == null)
				{
					return 0;
				}
				return base.Count;
			}
		}
 
		public Binder this[int index]
		{
			get
			{
				return (Binder) this.List[index];
			}
		}
		protected override ArrayList List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}


		// Fields
		private ArrayList list;
		//private CollectionChangeEventHandler onCollectionChanged;
	}
}