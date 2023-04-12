using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.Drawing.Design;
using System.Security;
using System.Reflection;
using System.Globalization;

namespace Nistec.WinForms
{
	/// <summary>
	/// Summary description for BindContext.
	/// </summary>
	[DefaultEvent("CollectionChanged")]
	public class BindContext : ICollection, IEnumerable
	{
		// Fields
		private Hashtable listManagers = new Hashtable();
		private CollectionChangeEventHandler onCollectionChanged;

		// Events
		[Description("collectionChangedEvent")]
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				throw new NotImplementedException();
			}
			remove
			{
				this.onCollectionChanged = (CollectionChangeEventHandler) Delegate.Remove(this.onCollectionChanged, value);
			}
		}

		// Methods
		protected internal void Add(object dataSource, BindManagerBase listManager)
		{
			this.AddCore(dataSource, listManager);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataSource));
		}

		protected virtual void AddCore(object dataSource, BindManagerBase listManager)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (listManager == null)
			{
				throw new ArgumentNullException("listManager");
			}
			this.listManagers[this.GetKey(dataSource, "")] = new WeakReference(listManager, false);
		}

		internal static void CheckPropertyBindingCycles(BindContext newBindingContext, Binder propBinding)
		{
			if (((newBindingContext != null) && (propBinding != null)) && newBindingContext.Contains(propBinding.Control, ""))
			{
				BindManagerBase base2 = newBindingContext.EnsureListManager(propBinding.Control, "");
				for (int i = 0; i < base2.Bindings.Count; i++)
				{
					Binder binding = base2.Bindings[i];
					if (binding.DataSource == propBinding.Control)
					{
						if (propBinding.BindToObject.BindingMemberInfo.BindingMember.Equals(binding.PropertyName))
						{
							throw new ArgumentException("DataBindingCycle", "propBinding:" + binding.PropertyName );
						}
					}
					else if (propBinding.BindToObject.BindManagerBase is BindPropertyManager)
					{
						CheckPropertyBindingCycles(newBindingContext, binding);
					}
				}
			}
		}

		protected internal void Clear()
		{
			this.ClearCore();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		protected virtual void ClearCore()
		{
			this.listManagers.Clear();
		}

		public bool Contains(object dataSource)
		{
			return this.Contains(dataSource, "");
		}

		public bool Contains(object dataSource, string dataMember)
		{
			return this.listManagers.ContainsKey(this.GetKey(dataSource, dataMember));
		}

		internal BindManagerBase EnsureListManager(object dataSource, string dataMember)
		{
			if (dataMember == null)
			{
				dataMember = "";
			}
			HashKey key = this.GetKey(dataSource, dataMember);
			BindManagerBase target = null;
			WeakReference reference = this.listManagers[key] as WeakReference;
			if (reference != null)
			{
				target = (BindManagerBase) reference.Target;
			}
			if (target == null)
			{
				if (dataMember.Length == 0)
				{
					if ((dataSource is IList) || (dataSource is IListSource))
					{
						target = new BindManager(dataSource);
					}
					else
					{
						target = new BindPropertyManager(dataSource);
					}
					if (reference == null)
					{
						this.listManagers.Add(key, new WeakReference(target, false));
						return target;
					}
					reference.Target = target;
					return target;
				}
				int length = dataMember.LastIndexOf(".");
				BindManagerBase parentManager = this.EnsureListManager(dataSource, (length == -1) ? "" : dataMember.Substring(0, length));
				PropertyDescriptor descriptor = parentManager.GetItemProperties().Find(dataMember.Substring(length + 1), true);
				if (descriptor == null)
				{
					throw new ArgumentException("RelatedListManagerChild", dataMember.Substring(length + 1) );
				}
				if (typeof(IList).IsAssignableFrom(descriptor.PropertyType))
				{
					target = new BindManagerRelated(parentManager, dataMember.Substring(length + 1));
				}
				else
				{
					target = new BindPropertyManagerRelated(parentManager, dataMember.Substring(length + 1));
				}
				if (reference == null)
				{
					this.listManagers.Add(this.GetKey(dataSource, dataMember), new WeakReference(target, false));
					return target;
				}
				reference.Target = target;
			}
			return target;
		}

		internal HashKey GetKey(object dataSource, string dataMember)
		{
			return new HashKey(dataSource, dataMember);
		}

		private void OnBadIndex(object index)
		{
			throw new IndexOutOfRangeException("BindingManagerBadIndex " + index.ToString() );
		}

		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, ccevent);
			}
		}

		protected internal void Remove(object dataSource)
		{
			this.RemoveCore(dataSource);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataSource));
		}

		protected virtual void RemoveCore(object dataSource)
		{
			this.listManagers.Remove(this.GetKey(dataSource, ""));
		}

		private void ScrubWeakRefs()
		{
			object[] array = new object[this.listManagers.Count];
			this.listManagers.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				DictionaryEntry entry = (DictionaryEntry) array[i];
				WeakReference reference = (WeakReference) entry.Value;
				if (reference.Target == null)
				{
					this.listManagers.Remove(entry.Key);
				}
			}
		}

		void ICollection.CopyTo(Array ar, int index)
		{
			//IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			this.listManagers.CopyTo(ar, index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			//IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			return this.listManagers.GetEnumerator();
		}

		internal static void UpdateBinding(BindContext newBindingContext, Binder binding)
		{
			BindManagerBase bindingManagerBase = binding.BindManagerBase;
			if (bindingManagerBase != null)
			{
				bindingManagerBase.Bindings.Remove(binding);
			}
			if (newBindingContext != null)
			{
				if (binding.BindToObject.BindManagerBase is BindPropertyManager)
				{
					CheckPropertyBindingCycles(newBindingContext, binding);
				}
				BindToObject bindToObject = binding.BindToObject;
				newBindingContext.EnsureListManager(bindToObject.DataSource, bindToObject.BindingMemberInfo.BindingPath).Bindings.Add(binding);
			}
		}

		// Properties
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public BindManagerBase this[object dataSource, string dataMember]
		{
			get
			{
				return this.EnsureListManager(dataSource, dataMember);
			}
		}

		public BindManagerBase this[object dataSource]
		{
			get
			{
				return this[dataSource, ""];
			}
		}

		int ICollection.Count
		{
			get
			{
				this.ScrubWeakRefs();
				return this.listManagers.Count;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Nested Types
		internal class HashKey
		{
			// Fields
			private string dataMember;
			private int dataSourceHashCode;
			private WeakReference wRef;

			// Methods
			internal HashKey(object dataSource, string dataMember)
			{
				if (dataSource == null)
				{
					throw new ArgumentNullException("dataSource");
				}
				if (dataMember == null)
				{
					dataMember = "";
				}
				this.wRef = new WeakReference(dataSource, false);
				this.dataSourceHashCode = dataSource.GetHashCode();
				this.dataMember = dataMember.ToLower(CultureInfo.InvariantCulture);
			}

			public override bool Equals(object target)
			{
				if (target is BindContext.HashKey)
				{
					BindContext.HashKey key = (BindContext.HashKey) target;
					if (this.wRef.Target == key.wRef.Target)
					{
						return (this.dataMember == key.dataMember);
					}
				}
				return false;
			}

			public override int GetHashCode()
			{
				return (this.dataSourceHashCode * this.dataMember.GetHashCode());
			}
		}
	}

 

}
