using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Data;

namespace Nistec.WinForms
{
	/// <summary>
	/// Summary description for BindManager.
	/// </summary>
	public class BindManager : BindManagerBase
	{
		// Fields
		private bool bound = false;
		private object dataSource;
		protected Type finalType;
		private bool inChangeRecordState = false;
		private int lastGoodKnownRow = -1;
		private IList list;
		protected int listposition = -1;
		private bool pullingData = false;
		private BindItemChangedEventArgs resetEvent = new BindItemChangedEventArgs(-1);
		private bool shouldBind = true;
		private bool suspendPushDataInCurrentChanged = false;

		// Events
		[Category("Data")]
		public event BindItemChangedEventHandler ItemChanged;

		[Category("Data")]
		public event EventHandler MetaDataChanged;

		// Methods
		internal BindManager(object dataSource)
		{
			this.SetDataSource(dataSource);
		}


		#region converts

//		public BindManagerBase GetBindingManagerBase()
//		{
//			return (BindManager)this.dataSource;
//		}

//		public static BindManager  GetBindManager(object dataSource,string dataMember, BindingManagerBase mngr)
//		{
//			BindContext bc=new BindContext();
//			BindManager bm=(BindManager) bc.EnsureListManager(dataSource,dataMember);
//			//BindManager bm=new BindManager(dataSource);
//			foreach(Binding b in mngr.Bindings)
//			{
//				bm.Bindings.Add(new Binder(b.PropertyName,b.DataSource,dataMember));	
//				//bm.Bindings.Add(new Binder(b.PropertyName,b.DataSource,b.BindingMemberInfo.BindingMember));	
//			}
//			return bm;
//		}
	
//		public static BindManagerBase  GetBindManager(object dataSource,string dataMember, Control c)
//		{
//			return ((McBind) c).BindContext[dataSource,dataMember];
//		}

		#endregion

		public override void AddNew()
		{
			if (!(this.list is IBindingList))
			{
				throw new NotSupportedException();
			}
			((IBindingList) this.list).AddNew();
			this.ChangeRecordState(this.list.Count - 1, true, this.Position != (this.list.Count - 1), true, true);
		}

		public override void CancelCurrentEdit()
		{
			if (this.Count > 0)
			{
				object obj2 = this.list[this.Position];
				if (obj2 is IEditableObject)
				{
					((IEditableObject) obj2).CancelEdit();
				}
				this.OnItemChanged(new BindItemChangedEventArgs(this.Position));
			}
		}

		private void ChangeRecordState(int newPosition, bool validating, bool endCurrentEdit, bool firePositionChange, bool pullData)
		{
			if ((newPosition == -1) && (this.list.Count == 0))
			{
				if (this.listposition != -1)
				{
					this.listposition = -1;
					this.OnPositionChanged(EventArgs.Empty);
				}
			}
			else
			{
				if (((newPosition < 0) || (newPosition >= this.Count)) && this.IsBinding)
				{
					throw new IndexOutOfRangeException("ListManagerBadPosition");
				}
				int listposition = this.listposition;
				if (endCurrentEdit)
				{
					this.inChangeRecordState = true;
					try
					{
						this.EndCurrentEdit();
					}
					finally
					{
						this.inChangeRecordState = false;
					}
				}
				if (validating && pullData)
				{
					this.CurrencyManager_PullData();
				}
				this.listposition = newPosition;
				if (validating)
				{
					this.OnCurrentChanged(EventArgs.Empty);
				}
				if ((listposition != this.listposition) && firePositionChange)
				{
					this.OnPositionChanged(EventArgs.Empty);
				}
			}
		}

		protected void CheckEmpty()
		{
			if (((this.dataSource == null) || (this.list == null)) || (this.list.Count == 0))
			{
				throw new InvalidOperationException("ListManagerEmptyList");
			}
		}

		private void CurrencyManager_PullData()
		{
			this.pullingData = true;
			try
			{
				base.PullData();
			}
			finally
			{
				this.pullingData = false;
			}
		}

		private bool CurrencyManager_PushData()
		{
			if (this.pullingData)
			{
				return false;
			}
			int listposition = this.listposition;
			if (this.lastGoodKnownRow == -1)
			{
				try
				{
					base.PushData();
				}
				catch (Exception)
				{
					this.FindGoodRow();
				}
				this.lastGoodKnownRow = this.listposition;
			}
			else
			{
				try
				{
					base.PushData();
				}
				catch (Exception)
				{
					this.listposition = this.lastGoodKnownRow;
					base.PushData();
				}
				this.lastGoodKnownRow = this.listposition;
			}
			return (listposition != this.listposition);
		}

		public override void EndCurrentEdit()
		{
			if (this.Count > 0)
			{
				this.CurrencyManager_PullData();
				object obj2 = this.list[this.Position];
				if (obj2 is IEditableObject)
				{
					((IEditableObject) obj2).EndEdit();
				}
			}
		}

		internal int Find(PropertyDescriptor property, object key, bool keepIndex)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (((property != null) && (this.list is IBindingList)) && ((IBindingList) this.list).SupportsSearching)
			{
				return ((IBindingList) this.list).Find(property, key);
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				object obj2 = property.GetValue(this.list[i]);
				if (key.Equals(obj2))
				{
					return i;
				}
			}
			return -1;
		}

		private void FindGoodRow()
		{
			int count = this.list.Count;
			for (int i = 0; i < count; i++)
			{
				this.listposition = i;
				try
				{
					base.PushData();
				}
				catch (Exception)
				{
					continue;
				}
				this.listposition = i;
				return;
			}
			this.SuspendBinding();
			throw new Exception("DataBindingPushDataException");
		}

		public override PropertyDescriptorCollection GetItemProperties()
		{
			if (typeof(Array).IsAssignableFrom(this.finalType))
			{
				return TypeDescriptor.GetProperties(this.finalType.GetElementType());
			}
			if (this.list is ITypedList)
			{
				return ((ITypedList) this.list).GetItemProperties(null);
			}
			PropertyInfo[] properties = this.finalType.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				if ("Item".Equals(properties[i].Name) && (properties[i].PropertyType != typeof(object)))
				{
					return TypeDescriptor.GetProperties(properties[i].PropertyType, new Attribute[] { new BrowsableAttribute(true) });
				}
			}
			if (this.List.Count > 0)
			{
				return TypeDescriptor.GetProperties(this.List[0], new Attribute[] { new BrowsableAttribute(true) });
			}
			return new PropertyDescriptorCollection(null);
		}

		//internal
		public override string GetListName()
		{
			if (this.list is ITypedList)
			{
				return ((ITypedList) this.list).GetListName(null);
			}
			return this.finalType.Name;
		}

		protected internal override string GetListName(ArrayList listAccessors)
		{
			if (this.list is ITypedList)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[listAccessors.Count];
				listAccessors.CopyTo(array, 0);
				return ((ITypedList) this.list).GetListName(array);
			}
			return "";
		}

		//internal
		public ListSortDirection GetSortDirection()
		{
			if ((this.list is IBindingList) && ((IBindingList) this.list).SupportsSorting)
			{
				return ((IBindingList) this.list).SortDirection;
			}
			return ListSortDirection.Ascending;
		}

		//internal
		public PropertyDescriptor GetSortProperty()
		{
			if ((this.list is IBindingList) && ((IBindingList) this.list).SupportsSorting)
			{
				return ((IBindingList) this.list).SortProperty;
			}
			return null;
		}

		private void List_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (!this.inChangeRecordState)
			{
				ListChangedEventArgs args;
				if ((e.ListChangedType == ListChangedType.ItemMoved) && (e.OldIndex < 0))
				{
					args = new ListChangedEventArgs(ListChangedType.ItemAdded, e.NewIndex, e.OldIndex);
				}
				else if ((e.ListChangedType == ListChangedType.ItemMoved) && (e.NewIndex < 0))
				{
					args = new ListChangedEventArgs(ListChangedType.ItemDeleted, e.OldIndex, e.NewIndex);
				}
				else
				{
					args = e;
				}
				this.UpdateLastGoodKnownRow(args);
				this.UpdateIsBinding();
				if (this.list.Count == 0)
				{
					this.listposition = -1;
					if ((args.ListChangedType == ListChangedType.Reset) && (args.NewIndex == -1))
					{
						this.OnItemChanged(this.resetEvent);
					}
					if (args.ListChangedType == ListChangedType.ItemDeleted)
					{
						this.OnItemChanged(this.resetEvent);
					}
					if (((args.ListChangedType == ListChangedType.PropertyDescriptorAdded) || (args.ListChangedType == ListChangedType.PropertyDescriptorDeleted)) || (args.ListChangedType == ListChangedType.PropertyDescriptorChanged))
					{
						this.OnMetaDataChanged(EventArgs.Empty);
					}
				}
				else
				{
					this.suspendPushDataInCurrentChanged = true;
					try
					{
						switch (args.ListChangedType)
						{
							case ListChangedType.Reset:
								if ((this.listposition != -1) || (this.list.Count <= 0))
								{
									break;
								}
								this.ChangeRecordState(0, true, false, true, false);
								goto Label_0152;

							case ListChangedType.ItemAdded:
								if ((args.NewIndex > this.listposition) || (this.listposition >= (this.list.Count - 1)))
								{
									goto Label_01EF;
								}
								this.ChangeRecordState(this.listposition + 1, true, true, this.listposition != (this.list.Count - 2), false);
								this.UpdateIsBinding();
								this.OnItemChanged(this.resetEvent);
								if (this.listposition == (this.list.Count - 1))
								{
									this.OnPositionChanged(EventArgs.Empty);
								}
								return;

							case ListChangedType.ItemDeleted:
								if (args.NewIndex != this.listposition)
								{
									goto Label_0256;
								}
								this.ChangeRecordState(Math.Min(this.listposition, this.Count - 1), true, false, true, false);
								this.OnItemChanged(this.resetEvent);
								return;

							case ListChangedType.ItemMoved:
								if (args.OldIndex != this.listposition)
								{
									goto Label_02EC;
								}
								this.ChangeRecordState(args.NewIndex, true, (this.Position > -1) && (this.Position < this.list.Count), true, false);
								goto Label_0328;

							case ListChangedType.ItemChanged:
								this.OnItemChanged(new BindItemChangedEventArgs(args.NewIndex));
								return;

							case ListChangedType.PropertyDescriptorAdded:
							case ListChangedType.PropertyDescriptorDeleted:
							case ListChangedType.PropertyDescriptorChanged:
								this.OnMetaDataChanged(EventArgs.Empty);
								return;

							default:
								return;
						}
						this.ChangeRecordState(Math.Min(this.listposition, this.list.Count - 1), true, false, true, false);
					Label_0152:
						this.UpdateIsBinding();
						this.OnItemChanged(this.resetEvent);
						return;
					Label_01EF:
						if (this.listposition == -1)
						{
							this.ChangeRecordState(0, false, false, true, false);
						}
						this.UpdateIsBinding();
						this.OnItemChanged(this.resetEvent);
						return;
					Label_0256:
						if (args.NewIndex < this.listposition)
						{
							this.ChangeRecordState(this.listposition - 1, true, false, true, false);
							this.OnItemChanged(this.resetEvent);
						}
						else
						{
							this.OnItemChanged(this.resetEvent);
						}
						return;
					Label_02EC:
						if (args.NewIndex == this.listposition)
						{
							this.ChangeRecordState(args.OldIndex, true, (this.Position > -1) && (this.Position < this.list.Count), true, false);
						}
					Label_0328:
						this.OnItemChanged(this.resetEvent);
					}
					finally
					{
						this.suspendPushDataInCurrentChanged = false;
					}
				}
			}
		}

		protected internal override void OnCurrentChanged(EventArgs e)
		{
			if (!this.inChangeRecordState)
			{
				int lastGoodKnownRow = this.lastGoodKnownRow;
				bool flag = false;
				if (!this.suspendPushDataInCurrentChanged)
				{
					flag = this.CurrencyManager_PushData();
				}
				if (this.Count > 0)
				{
					object obj2 = this.list[this.Position];
					if (obj2 is IEditableObject)
					{
						((IEditableObject) obj2).BeginEdit();
					}
				}
				try
				{
					if ((!flag) || (flag && (lastGoodKnownRow != -1)))
					{
						base.OnCurrentChanged(e);
					}
//					if (((this.CurrentChanged != null) && !flag) || (flag && (lastGoodKnownRow != -1)))
//					{
//						this.CurrentChanged(this, e);
//					}
				}
				catch (Exception)
				{
				}
			}
		}

		protected virtual void OnItemChanged(BindItemChangedEventArgs e)
		{
			bool flag = false;
			if ((e.Index == this.listposition) || (((e.Index == -1) && (this.Position < this.Count)) && !this.inChangeRecordState))
			{
				flag = this.CurrencyManager_PushData();
			}
			try
			{
				if (this.ItemChanged != null)
				{
					this.ItemChanged(this, e);
				}
			}
			catch (Exception)
			{
			}
			if (flag)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
		}

		private void OnMetaDataChanged(EventArgs e)
		{
			if (this.MetaDataChanged != null)
			{
				this.MetaDataChanged(this, e);
			}
		}

		protected override void OnPositionChanged(EventArgs e)
		{
			try
			{
				base.OnPositionChanged(e);
//				if (base.PositionChanged != null)
//				{
//					base.PositionChanged(this, e);
//				}
			}
			catch (Exception)
			{
			}
		}

		public void Refresh()
		{
			this.List_ListChanged(this.list, new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		internal void Release()
		{
			this.UnwireEvents(this.list);
		}

		public override void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		public override void ResumeBinding()
		{
			this.lastGoodKnownRow = -1;
			try
			{
				if (!this.shouldBind)
				{
					this.shouldBind = true;
					this.listposition = ((this.list != null) && (this.list.Count != 0)) ? 0 : -1;
					this.UpdateIsBinding();
				}
			}
			catch (Exception exception)
			{
				this.shouldBind = false;
				this.UpdateIsBinding();
				throw exception;
			}
		}

		internal override void SetDataSource(object dataSource)
		{
			if (this.dataSource != dataSource)
			{
				this.Release();
				this.dataSource = dataSource;
				this.list = null;
				this.finalType = null;
				object list = dataSource;
				if (list is Array)
				{
					this.finalType = list.GetType();
					list = (Array) list;
				}
				if (list is IListSource)
				{
					list = ((IListSource) list).GetList();
				}
				if (list is IList)
				{
					if (this.finalType == null)
					{
						this.finalType = list.GetType();
					}
					this.list = (IList) list;
					this.WireEvents(this.list);
					if (this.list.Count > 0)
					{
						this.listposition = 0;
					}
					else
					{
						this.listposition = -1;
					}
					this.OnItemChanged(this.resetEvent);
					this.UpdateIsBinding();
				}
				else
				{
					if (list == null)
					{
						throw new ArgumentNullException("dataSource");
					}
					throw new ArgumentException("ListManagerSetDataSource", list.GetType().FullName);
				}
			}
		}

		public void SetSort(PropertyDescriptor property, ListSortDirection sortDirection)
		{
			if ((this.list is IBindingList) && ((IBindingList) this.list).SupportsSorting)
			{
				((IBindingList) this.list).ApplySort(property, sortDirection);
			}
		}

		public override void SuspendBinding()
		{
			this.lastGoodKnownRow = -1;
			if (this.shouldBind)
			{
				this.shouldBind = false;
				this.UpdateIsBinding();
			}
		}

		internal void UnwireEvents(IList list)
		{
			if ((list is IBindingList) && ((IBindingList) list).SupportsChangeNotification)
			{
				((IBindingList) list).ListChanged -= new ListChangedEventHandler(this.List_ListChanged);
			}
		}

		protected override void UpdateIsBinding()
		{
			this.UpdateIsBinding(false);
		}

		private void UpdateIsBinding(bool force)
		{
			bool flag = (((this.list != null) && (this.list.Count > 0)) && this.shouldBind) && (this.listposition != -1);
			if ((this.list != null) && ((this.bound != flag) || force))
			{
				this.bound = flag;
				int newPosition = flag ? 0 : -1;
				this.ChangeRecordState(newPosition, this.bound, this.Position != newPosition, true, false);
				int count = base.Bindings.Count;
				for (int i = 0; i < count; i++)
				{
					base.Bindings[i].UpdateIsBinding();
				}
				this.OnItemChanged(this.resetEvent);
			}
		}

		private void UpdateLastGoodKnownRow(ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
				case ListChangedType.Reset:
					this.lastGoodKnownRow = -1;
					return;

				case ListChangedType.ItemAdded:
					if (e.NewIndex > this.lastGoodKnownRow)
					{
						break;
					}
					this.lastGoodKnownRow++;
					return;

				case ListChangedType.ItemDeleted:
					if (e.NewIndex != this.lastGoodKnownRow)
					{
						break;
					}
					this.lastGoodKnownRow = -1;
					return;

				case ListChangedType.ItemMoved:
					if (e.OldIndex != this.lastGoodKnownRow)
					{
						break;
					}
					this.lastGoodKnownRow = e.NewIndex;
					return;

				case ListChangedType.ItemChanged:
					if (e.NewIndex == this.lastGoodKnownRow)
					{
						this.lastGoodKnownRow = -1;
					}
					break;

				default:
					return;
			}
		}

		internal void WireEvents(IList list)
		{
			if ((list is IBindingList) && ((IBindingList) list).SupportsChangeNotification)
			{
				((IBindingList) list).ListChanged += new ListChangedEventHandler(this.List_ListChanged);
			}
		}

		// Properties
		public bool AllowAdd
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList) this.list).AllowNew;
				}
				if ((this.list != null) && !this.list.IsReadOnly)
				{
					return !this.list.IsFixedSize;
				}
				return false;
			}
		}

		public bool AllowEdit
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList) this.list).AllowEdit;
				}
				if (this.list == null)
				{
					return false;
				}
				return !this.list.IsReadOnly;
			}
		}

		public bool AllowRemove
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList) this.list).AllowRemove;
				}
				if ((this.list != null) && !this.list.IsReadOnly)
				{
					return !this.list.IsFixedSize;
				}
				return false;
			}
		}

		internal override Type BindType
		{
			get
			{
				return this.finalType;
			}
		}

		public override int Count
		{
			get
			{
				if (this.list == null)
				{
					return 0;
				}
				return this.list.Count;
			}
		}

		public override object Current
		{
			get
			{
				return this[this.Position];
			}
		}

		internal override object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		public override bool IsBinding
		{
			get
			{
				return this.bound;
			}
		}

		public object this[int index]
		{
			get
			{
				if ((index < 0) || (index >= this.list.Count))
				{
					throw new IndexOutOfRangeException("ListManagerNoValue"+ index.ToString());
				}
				return this.list[index];
			}
			set
			{
				if ((index < 0) || (index >= this.list.Count))
				{
					throw new IndexOutOfRangeException("ListManagerNoValue "+ index.ToString());
				}
				this.list[index] = value;
			}
		}

		public IList List
		{
			get
			{
				return this.list;
			}
		}

		public override int Position
		{
			get
			{
				return this.listposition;
			}
			set
			{
				if (this.listposition != -1)
				{
					if (value < 0)
					{
						value = 0;
					}
					int count = this.list.Count;
					if (value >= count)
					{
						value = count - 1;
					}
					this.ChangeRecordState(value, this.listposition != value, true, true, false);
				}
			}
		}

		public DataView DataList(string dataMember)
		{
			try
			{
				if(this.dataSource==null)
					return null;
				if (this.list is ITypedList)
				{
					if(list is DataViewManager)
					{
						return (DataView)((DataViewManager)this.list).DataSet.Tables[dataMember].DefaultView;
					}
					else
					{
						return (DataView)this.list as DataView;
					}
				}
				return null;
			}
			catch//(Exception ex)
			{
				return null;
			}
		}

	}

 
}
