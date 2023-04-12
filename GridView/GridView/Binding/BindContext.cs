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

namespace mControl.WinCtl.Controls
{

	#region BindContext

	[DefaultEvent("CollectionChanged")]
	public class BindContext :BindingContext// ICollection, IEnumerable
	{

		#region Ctor

		// Fields
		private Hashtable listManagers;
		private CollectionChangeEventHandler onCollectionChanged;

		// Events
		//[Description("collectionChangedEventDescr")]
		//public event CollectionChangeEventHandler CollectionChanged;

		// Methods
		public BindContext()
		{
			this.listManagers = new Hashtable();
		}

		#endregion

		#region Methods

		internal static void CheckPropertyBindCycles(BindContext newBindingContext, Binder propBinding)
		{
			if (((newBindingContext != null) && (propBinding != null)) && newBindingContext.Contains(propBinding.Control, ""))
			{
				BindManagerBase base1 = newBindingContext.EnsureBindListManager(propBinding.Control, "");
				for (int num1 = 0; num1 < base1.Bindings.Count; num1++)
				{
					Binder binding1 = base1.Bindings[num1];
					if (binding1.DataSource == propBinding.Control)
					{
						if (propBinding.BindToObject.BindingMemberInfo.BindingMember.Equals(binding1.PropertyName))
						{
							throw new ArgumentException("DataBindingCycle ", binding1.PropertyName);
						}
					}
					else if (propBinding.BindToObject.BindManagerBase is BindPropertyManager)
					{
						BindContext.CheckPropertyBindCycles(newBindingContext, binding1);
					}
				}
			}
		}

		internal BindManagerBase EnsureBindListManager(object dataSource, string dataMember)
		{
			if (dataMember == null)
			{
				dataMember = "";
			}
			BindContext.HashKey key1 = this.GetKey(dataSource, dataMember);
			BindManagerBase base1 = null;
			WeakReference reference1 = this.listManagers[key1] as WeakReference;
			if (reference1 != null)
			{
				base1 = (BindManagerBase) reference1.Target;
			}
			if (base1 == null)
			{
				if (dataMember.Length == 0)
				{
					if ((dataSource is IList) || (dataSource is IListSource))
					{
						base1 = new BindManager(dataSource);
					}
					else
					{
						base1 = new BindPropertyManager(dataSource);
					}
					if (reference1 == null)
					{
						this.listManagers.Add(key1, new WeakReference(base1, false));
						return base1;
					}
					reference1.Target = base1;
					return base1;
				}
				int num1 = dataMember.LastIndexOf(".");
				BindManagerBase base2 = this.EnsureBindListManager(dataSource, (num1 == -1) ? "" : dataMember.Substring(0, num1));
				PropertyDescriptor descriptor1 = base2.GetItemProperties().Find(dataMember.Substring(num1 + 1), true);
				if (descriptor1 == null)
				{
					throw new ArgumentException("RelatedListManagerChild", dataMember.Substring(num1 + 1) );
				}
				if (typeof(IList).IsAssignableFrom(descriptor1.PropertyType))
				{
					base1 = new RelatedBindManager(base2, dataMember.Substring(num1 + 1));
				}
				else
				{
					base1 = new RelatedBindPropertyManager(base2, dataMember.Substring(num1 + 1));
				}
				if (reference1 == null)
				{
					this.listManagers.Add(this.GetKey(dataSource, dataMember), new WeakReference(base1, false));
					return base1;
				}
				reference1.Target = base1;
			}
			return base1;
		}

		internal BindContext.HashKey GetKey(object dataSource, string dataMember)
		{
			return new BindContext.HashKey(dataSource, dataMember);
		}

 

		public BindManagerBase GetBindManager(object dataSource, string dataMember)
		{
			return this.EnsureBindListManager(dataSource, dataMember);
		}
 
		public BindManagerBase GetBindManager(object dataSource)
		{
			return GetBindManager(dataSource, "");
		}

		public static void UpdateBinding(BindContext newBindingContext, Binder binding)
		{
			BindManagerBase base1 = binding.BindManagerBase;
			if (base1 != null)
			{
				base1.Bindings.Remove(binding);
			}
			if (newBindingContext != null)
			{
				if (binding.BindToObject.BindManagerBase is BindPropertyManager)
				{
					BindContext.CheckPropertyBindCycles(newBindingContext, binding);
				}
				BindToObject obj1 = binding.BindToObject;
				BindManagerBase base2 = newBindingContext.EnsureBindListManager(obj1.DataSource, obj1.BindingMemberInfo.BindingPath);
				base2.Bindings.Add(binding);
			}
		}


		#endregion

		#region Properties

		public new bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public new BindManagerBase this[object dataSource, string dataMember]
		{
			get
			{
				return this.EnsureBindListManager(dataSource, dataMember);
			}
		}
 
		public new BindManagerBase this[object dataSource]
		{
			get
			{
				return this[dataSource, ""];
			}
		}
 
		[Description("collectionChangedEventDescr")]
		public new event CollectionChangeEventHandler CollectionChanged
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
 
		#endregion

		#region base

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


		public new void Clear()
		{
			this.ClearCore();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

 
		protected override void ClearCore()
		{
			this.listManagers.Clear();
		}

 
		public new bool Contains(object dataSource)
		{
			return this.Contains(dataSource, "");
		}

 
		public new bool Contains(object dataSource, string dataMember)
		{
			return this.listManagers.ContainsKey(this.GetKey(dataSource, dataMember));
		}

//		internal BindingManagerBase EnsureListManager(object dataSource, string dataMember)
//		{
//			if (dataMember == null)
//			{
//				dataMember = "";
//			}
//			BindingContext.HashKey key1 = this.GetKey(dataSource, dataMember);
//			BindingManagerBase base1 = null;
//			WeakReference reference1 = this.listManagers[key1] as WeakReference;
//			if (reference1 != null)
//			{
//				base1 = (BindingManagerBase) reference1.Target;
//			}
//			if (base1 == null)
//			{
//				if (dataMember.Length == 0)
//				{
//					if ((dataSource is IList) || (dataSource is IListSource))
//					{
//						base1 = new CurrencyManager(dataSource);
//					}
//					else
//					{
//						base1 = new PropertyManager(dataSource);
//					}
//					if (reference1 == null)
//					{
//						this.listManagers.Add(key1, new WeakReference(base1, false));
//						return base1;
//					}
//					reference1.Target = base1;
//					return base1;
//				}
//				int num1 = dataMember.LastIndexOf(".");
//				BindingManagerBase base2 = this.EnsureListManager(dataSource, (num1 == -1) ? "" : dataMember.Substring(0, num1));
//				PropertyDescriptor descriptor1 = base2.GetItemProperties().Find(dataMember.Substring(num1 + 1), true);
//				if (descriptor1 == null)
//				{
//					throw new ArgumentException(SR.GetString("RelatedListManagerChild", new object[] { dataMember.Substring(num1 + 1) }));
//				}
//				if (typeof(IList).IsAssignableFrom(descriptor1.PropertyType))
//				{
//					base1 = new RelatedCurrencyManager(base2, dataMember.Substring(num1 + 1));
//				}
//				else
//				{
//					base1 = new RelatedPropertyManager(base2, dataMember.Substring(num1 + 1));
//				}
//				if (reference1 == null)
//				{
//					this.listManagers.Add(this.GetKey(dataSource, dataMember), new WeakReference(base1, false));
//					return base1;
//				}
//				reference1.Target = base1;
//			}
//			return base1;
//		}
//

//		internal BindingContext.HashKey GetKey(object dataSource, string dataMember)
//		{
//			return new BindingContext.HashKey(dataSource, dataMember);
//		}

 
		private void OnBadIndex(object index)
		{
			throw new IndexOutOfRangeException("BindingManagerBadIndex " + index.ToString() );
		}

		protected override void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, ccevent);
			}
		}

		public new void Remove(object dataSource)
		{
			this.RemoveCore(dataSource);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataSource));
		}

		protected override void RemoveCore(object dataSource)
		{
			this.listManagers.Remove(this.GetKey(dataSource, ""));
		}

		private void ScrubWeakRefs()
		{
			object[] objArray1 = new object[this.listManagers.Count];
			this.listManagers.CopyTo(objArray1, 0);
			for (int num1 = 0; num1 < objArray1.Length; num1++)
			{
				DictionaryEntry entry1 = (DictionaryEntry) objArray1[num1];
				WeakReference reference1 = (WeakReference) entry1.Value;
				if (reference1.Target == null)
				{
					this.listManagers.Remove(entry1.Key);
				}
			}
		}

 
		void CopyTo(Array ar, int index)//ICollection.CopyTo(Array ar, int index)
		{
			mControl.GridStyle.IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			this.listManagers.CopyTo(ar, index);
		}

 
		public int Count //ICollection.Count
		{
			get
			{
				this.ScrubWeakRefs();
				return this.listManagers.Count;
			}
		}

 
		public bool IsSynchronized //ICollection.get_IsSynchronized()
		{
			get{return false;}
		}

 
		object SyncRoot//ICollection.get_SyncRoot()
		{
			get{return null;}
		}

		IEnumerator GetEnumerator()//IEnumerable.GetEnumerator()
		{
			mControl.GridStyle.IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			return this.listManagers.GetEnumerator();
		}

//		internal static void UpdateBinding(BindContext newBindingContext, Binder binding)
//		{
//			BindManagerBase base1 = binding.BindManagerBase;
//			if (base1 != null)
//			{
//				base1.Bindings.Remove(binding);
//			}
//			if (newBindingContext != null)
//			{
//				if (binding.BindToObject.BindingManagerBase is PropertyManager)
//				{
//					BindContext.CheckPropertyBindingCycles(newBindingContext, binding);
//				}
//				BindToObject obj1 = binding.BindToObject;
//				BindingManagerBase base2 = newBindingContext.EnsureListManager(obj1.DataSource, obj1.BindingMemberInfo.BindingPath);
//				base2.Bindings.Add(binding);
//			}
//		}

		#endregion

		#region Nested Types
		internal class HashKey
		{
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
					BindContext.HashKey key1 = (BindContext.HashKey) target;
					if (this.wRef.Target == key1.wRef.Target)
					{
						return (this.dataMember == key1.dataMember);
					}
				}
				return false;
			}

			public override int GetHashCode()
			{
				return (this.dataSourceHashCode * this.dataMember.GetHashCode());
			}

 

			// Fields
			private string dataMember;
			private int dataSourceHashCode;
			private WeakReference wRef;
		}
		#endregion
	}
 
	#endregion

	#region RelatedBindManager

	internal class RelatedBindManager : BindManager
	{
		// Methods
		internal RelatedBindManager(BindManagerBase parentManager, string dataField) : base(null)
		{
			this.parentManager = parentManager;
			this.dataField = dataField;
			this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
			if ((this.fieldInfo == null) || !typeof(IList).IsAssignableFrom(this.fieldInfo.PropertyType))
			{
				throw new ArgumentException("RelatedListManagerChild", dataField);
			}
			base.finalType = this.fieldInfo.PropertyType;
			parentManager.CurrentChanged += new EventHandler(this.ParentManager_CurrentChanged);
			this.ParentManager_CurrentChanged(parentManager, EventArgs.Empty);
		}

		public override PropertyDescriptorCollection GetItemProperties()
		{
			PropertyDescriptorCollection collection1 = this.GetItemProperties(new ArrayList(), new ArrayList());
			if (collection1 != null)
			{
				return collection1;
			}
			return new PropertyDescriptorCollection(null);
		}

		protected internal override PropertyDescriptorCollection GetItemProperties(ArrayList dataSources, ArrayList listAccessors)
		{
			listAccessors.Insert(0, this.fieldInfo);
			dataSources.Insert(0, this.DataSource);
			return this.parentManager.GetItemProperties(dataSources, listAccessors);
		}

		public override string GetListName()
		{
			string text1 = this.GetListName(new ArrayList());
			if (text1.Length > 0)
			{
				return text1;
			}
			return base.GetListName();
		}

		protected internal override string GetListName(ArrayList listAccessors)
		{
			listAccessors.Insert(0, this.fieldInfo);
			return this.parentManager.GetListName(listAccessors);
		}

		private void ParentManager_CurrentChanged(object sender, EventArgs e)
		{
			int num1 = base.listposition;
			try
			{
				base.PullData();
			}
			catch
			{
			}
			if (this.parentManager is BindManager)
			{
				BindManager manager1 = (BindManager) this.parentManager;
				if (manager1.Count > 0)
				{
					this.SetDataSource(this.fieldInfo.GetValue(manager1.Current));
					base.listposition = (this.Count > 0) ? 0 : -1;
				}
				else
				{
					manager1.AddNew();
					manager1.CancelCurrentEdit();
				}
			}
			else
			{
				this.SetDataSource(this.fieldInfo.GetValue(this.parentManager.Current));
				base.listposition = (this.Count > 0) ? 0 : -1;
			}
			if (num1 != base.listposition)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
			this.OnCurrentChanged(EventArgs.Empty);
		}

 
		internal BindManager ParentManager
		{
			get
			{
				return (this.parentManager as BindManager);
			}
		}


		// Fields
		private string dataField;
		private PropertyDescriptor fieldInfo;
		private BindManagerBase parentManager;
	}

	#endregion

	#region RelatedBindPropertyManager

	internal class RelatedBindPropertyManager : BindPropertyManager
	{
		// Methods
		internal RelatedBindPropertyManager(BindManagerBase parentManager, string dataField) : base(parentManager.Current, dataField)
		{
			this.parentManager = parentManager;
			this.dataField = dataField;
			this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
			if (this.fieldInfo == null)
			{
				throw new ArgumentException("RelatedListManagerChild" ,dataField );
			}
			parentManager.CurrentChanged += new EventHandler(this.ParentManager_CurrentChanged);
			this.ParentManager_CurrentChanged(parentManager, EventArgs.Empty);
		}

		public override PropertyDescriptorCollection GetItemProperties()
		{
			PropertyDescriptorCollection collection1 = this.GetItemProperties(new ArrayList(), new ArrayList());
			if (collection1 != null)
			{
				return collection1;
			}
			return new PropertyDescriptorCollection(null);
		}

 
		public new  PropertyDescriptorCollection GetItemProperties(ArrayList dataSources, ArrayList listAccessors)
		{
			dataSources.Insert(0, this.DataSource);
			listAccessors.Insert(0, this.fieldInfo);
			return this.parentManager.GetItemProperties(dataSources, listAccessors);
		}

 
		public override  string GetListName()
		{
			string text1 = this.GetListName(new ArrayList());
			if (text1.Length > 0)
			{
				return text1;
			}
			return base.GetListName();
		}

 
		internal protected  override string GetListName(ArrayList listAccessors)
		{
			listAccessors.Insert(0, this.fieldInfo);
			return this.parentManager.GetListName(listAccessors);
		}

		internal protected  override void OnCurrentChanged(EventArgs e)
		{
			this.SetDataSource(this.fieldInfo.GetValue(this.parentManager.Current));
			base.OnCurrentChanged(e);
		}

		private void ParentManager_CurrentChanged(object sender, EventArgs e)
		{
			this.EndCurrentEdit();
			this.SetDataSource(this.fieldInfo.GetValue(this.parentManager.Current));
			this.OnCurrentChanged(EventArgs.Empty);
		}

		public new  Type BindType
		{
			get
			{
				return this.fieldInfo.PropertyType;
			}
		}



		// Fields
		private string dataField;
		private PropertyDescriptor fieldInfo;
		private BindManagerBase parentManager;
	}
	#endregion

}
