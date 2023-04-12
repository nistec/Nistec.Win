using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Data;

namespace mControl.WinCtl.Controls
{

	public class BindManager : BindManagerBase
	{

		#region Fields

		private bool bound;
		private object dataSource;
		protected Type finalType;
		private bool inChangeRecordState;
		private int lastGoodKnownRow;
		private IList list;
		protected int listposition;
		//private ItemChangedEventHandler onItemChanged;
		//private EventHandler onMetaDataChangedHandler;
		private bool pullingData;
		private BindItemChangedEventArgs resetEvent;
		private bool shouldBind;
		private bool suspendPushDataInCurrentChanged;

		// Events
		[Category("Data")]
		public event BindItemChangedEventHandler ItemChanged;
		[Category("Data")]
		public event EventHandler MetaDataChanged;

		#endregion

		#region converts

		public BindingManagerBase GetBindingManagerBase()
		{
			return (CurrencyManager)this.dataSource;
		}

		public static BindManager  GetBindManager(object dataSource,string dataMember, BindingManagerBase mngr)
		{
			BindManager bm=new BindManager(dataSource);
			foreach(Binding b in mngr.Bindings)
			{
				bm.Bindings.Add(new Binder(b.PropertyName,b.DataSource,dataMember));	
				//bm.Bindings.Add(new Binder(b.PropertyName,b.DataSource,b.BindingMemberInfo.BindingMember));	
			}
			return bm;
		}

		#endregion

		#region Methods
		public BindManager(object dataSource)
		{
			this.bound = false;
			this.shouldBind = true;
			this.listposition = -1;
			this.lastGoodKnownRow = -1;
			this.pullingData = false;
			this.inChangeRecordState = false;
			this.suspendPushDataInCurrentChanged = false;
			this.resetEvent = new BindItemChangedEventArgs(-1);
			this.SetDataSource(dataSource);
		}


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
				object obj1 = this.list[this.Position];
				if (obj1 is IEditableObject)
				{
					((IEditableObject) obj1).CancelEdit();
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
				int num1 = this.listposition;
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
				if ((num1 != this.listposition) && firePositionChange)
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
			int num1 = this.listposition;
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
			return (num1 != this.listposition);
		}

 
		public override void EndCurrentEdit()
		{
			if (this.Count > 0)
			{
				this.CurrencyManager_PullData();
				object obj1 = this.list[this.Position];
				if (obj1 is IEditableObject)
				{
					((IEditableObject) obj1).EndEdit();
				}
			}
		}

		public int Find(PropertyDescriptor property, object key, bool keepIndex)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (((property != null) && (this.list is IBindingList)) && ((IBindingList) this.list).SupportsSearching)
			{
				return ((IBindingList) this.list).Find(property, key);
			}
			for (int num1 = 0; num1 < this.list.Count; num1++)
			{
				object obj1 = property.GetValue(this.list[num1]);
				if (key.Equals(obj1))
				{
					return num1;
				}
			}
			return -1;
		}

		private void FindGoodRow()
		{
			int num1 = this.list.Count;
			for (int num2 = 0; num2 < num1; num2++)
			{
				this.listposition = num2;
				try
				{
					base.PushData();
				}
				catch (Exception)
				{
					goto Label_002A;
				}
				this.listposition = num2;
				return;
			Label_002A:;
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
			PropertyInfo[] infoArray1 = this.finalType.GetProperties();
			for (int num1 = 0; num1 < infoArray1.Length; num1++)
			{
				if ("Item".Equals(infoArray1[num1].Name) && (infoArray1[num1].PropertyType != typeof(object)))
				{
					return TypeDescriptor.GetProperties(infoArray1[num1].PropertyType, new Attribute[] { new BrowsableAttribute(true) });
				}
			}
			if (this.List.Count > 0)
			{
				return TypeDescriptor.GetProperties(this.List[0], new Attribute[] { new BrowsableAttribute(true) });
			}
			return new PropertyDescriptorCollection(null);
		}

 
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
				PropertyDescriptor[] descriptorArray1 = new PropertyDescriptor[listAccessors.Count];
				listAccessors.CopyTo(descriptorArray1, 0);
				return ((ITypedList) this.list).GetListName(descriptorArray1);
			}
			return "";
		}

 
		public ListSortDirection GetSortDirection()
		{
			if ((this.list is IBindingList) && ((IBindingList) this.list).SupportsSorting)
			{
				return ((IBindingList) this.list).SortDirection;
			}
			return ListSortDirection.Ascending;
		}

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
				this.UpdateLastGoodKnownRow(e);
				this.UpdateIsBinding();
				if (this.list.Count == 0)
				{
					this.listposition = -1;
					if ((e.ListChangedType == ListChangedType.Reset) && (e.NewIndex == -1))
					{
						this.OnItemChanged(this.resetEvent);
					}
					if (((e.ListChangedType == ListChangedType.PropertyDescriptorAdded) || (e.ListChangedType == ListChangedType.PropertyDescriptorDeleted)) || (e.ListChangedType == ListChangedType.PropertyDescriptorChanged))
					{
						this.OnMetaDataChanged(EventArgs.Empty);
					}
				}
				else
				{
					this.suspendPushDataInCurrentChanged = true;
					try
					{
						switch (e.ListChangedType)
						{
							case ListChangedType.Reset:
								if ((this.listposition != -1) || (this.list.Count <= 0))
								{
									break;
								}
								this.ChangeRecordState(0, true, false, true, false);
								goto Label_00ED;

							case ListChangedType.ItemAdded:
								if ((e.NewIndex <= this.listposition) && (this.listposition < (this.list.Count - 1)))
								{
									this.ChangeRecordState(this.listposition + 1, true, true, this.listposition != (this.list.Count - 2), false);
									this.UpdateIsBinding();
									this.OnItemChanged(this.resetEvent);
									if (this.listposition == (this.list.Count - 1))
									{
										this.OnPositionChanged(EventArgs.Empty);
									}
									return;
								}
								goto Label_018A;

							case ListChangedType.ItemDeleted:
								if (e.NewIndex == this.listposition)
								{
									this.ChangeRecordState(Math.Min(this.listposition, this.Count - 1), true, false, true, false);
									this.OnItemChanged(this.resetEvent);
									return;
								}
								goto Label_01F1;

							case ListChangedType.ItemMoved:
								if (e.OldIndex != this.listposition)
								{
									goto Label_0287;
								}
								this.ChangeRecordState(e.NewIndex, true, (this.Position > -1) && (this.Position < this.list.Count), true, false);
								goto Label_02D3;

							case ListChangedType.ItemChanged:
								this.OnItemChanged(new BindItemChangedEventArgs(e.NewIndex));
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
					Label_00ED:
						this.UpdateIsBinding();
						this.OnItemChanged(this.resetEvent);
						return;
					Label_018A:
						if (this.listposition == -1)
						{
							this.ChangeRecordState(0, false, false, true, false);
						}
						this.UpdateIsBinding();
						this.OnItemChanged(this.resetEvent);
						return;
					Label_01F1:
						if (e.NewIndex < this.listposition)
						{
							this.ChangeRecordState(this.listposition - 1, true, false, true, false);
							this.OnItemChanged(this.resetEvent);
							return;
						}
						this.OnItemChanged(this.resetEvent);
						return;
					Label_0287:
						if ((e.OldIndex > this.listposition) && (e.NewIndex <= this.listposition))
						{
							this.ChangeRecordState(this.listposition - 1, true, (this.Position > -1) && (this.Position < this.list.Count), true, false);
						}
					Label_02D3:
						this.OnItemChanged(this.resetEvent);
					}
					finally
					{
						this.suspendPushDataInCurrentChanged = false;
					}
				}
			}
		}

		internal protected  override void OnCurrentChanged(EventArgs e)
		{
			if (!this.inChangeRecordState)
			{
				int num1 = this.lastGoodKnownRow;
				bool flag1 = false;
				if (!this.suspendPushDataInCurrentChanged)
				{
					flag1 = this.CurrencyManager_PushData();
				}
				if (this.Count > 0)
				{
					object obj1 = this.list[this.Position];
					if (obj1 is IEditableObject)
					{
						((IEditableObject) obj1).BeginEdit();
					}
				}
				try
				{

					if (((base.onCurrentChangedHandler != null) && !flag1) || (flag1 && (num1 != -1)))
					{
						base.OnCurrentChanged(e);
						//base.onCurrentChangedHandler(this, e);
					}
				}
				catch (Exception)
				{
				}
			}
		}

		protected virtual void OnItemChanged(BindItemChangedEventArgs e)
		{
			bool flag1 = false;
			if ((e.Index == this.listposition) || (((e.Index == -1) && (this.Position < this.Count)) && !this.inChangeRecordState))
			{
				flag1 = this.CurrencyManager_PushData();
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
			if (flag1)
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

//		protected virtual  void OnPositionChanged(EventArgs e)
//		{
//			try
//			{
//				if (base.PositionChanged !=null)//.onPositionChangedHandler != null)
//				{
//					this.PositionChanged(this,e);
//					//base.OnPositionChanged(e);
//					//base.onPositionChangedHandler(this, e);
//				}
//			}
//			catch (Exception)
//			{
//			}
//		}

 
		public void Refresh()
		{
			this.List_ListChanged(this.list, new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		public void Release()
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
			catch (Exception exception1)
			{
				this.shouldBind = false;
				this.UpdateIsBinding();
				throw exception1;
			}
		}

		public override void SetDataSource(object dataSource)
		{
			if (this.dataSource != dataSource)
			{
				this.Release();
				this.dataSource = dataSource;
				this.list = null;
				this.finalType = null;
				object obj1 = dataSource;
				if (obj1 is Array)
				{
					this.finalType = obj1.GetType();
					obj1 = (Array) obj1;
				}
				if (obj1 is IListSource)
				{
					obj1 = ((IListSource) obj1).GetList();
				}
				if (obj1 is IList)
				{
					if (this.finalType == null)
					{
						this.finalType = obj1.GetType();
					}
					this.list = (IList) obj1;
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
					if (obj1 == null)
					{
						throw new ArgumentNullException("dataSource");
					}
					throw new ArgumentException("ListManagerSetDataSource", obj1.GetType().FullName );
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
			bool flag1 = (((this.list != null) && (this.list.Count > 0)) && this.shouldBind) && (this.listposition != -1);
			if ((this.list != null) && ((this.bound != flag1) || force))
			{
				this.bound = flag1;
				int num1 = flag1 ? 0 : -1;
				this.ChangeRecordState(num1, this.bound, this.Position != num1, true, false);
				int num2 = base.Bindings.Count;
				for (int num3 = 0; num3 < num2; num3++)
				{
					base.Bindings[num3].UpdateIsBinding();
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
					if (e.NewIndex <= this.lastGoodKnownRow)
					{
						this.lastGoodKnownRow++;
						return;
					}
					return;

				case ListChangedType.ItemDeleted:
					if (e.NewIndex == this.lastGoodKnownRow)
					{
						this.lastGoodKnownRow = -1;
						return;
					}
					return;

				case ListChangedType.ItemMoved:
					if (e.OldIndex == this.lastGoodKnownRow)
					{
						this.lastGoodKnownRow = e.NewIndex;
						return;
					}
					return;

				case ListChangedType.ItemChanged:
					if (e.NewIndex == this.lastGoodKnownRow)
					{
						this.lastGoodKnownRow = -1;
					}
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

 

		#endregion

		#region Properties
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
 
		internal override  Type BindType
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
		public override object DataSource
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
					throw new IndexOutOfRangeException("ListManagerNoValue " + index.ToString() );
				}
				return this.list[index];
			}
			set
			{
				if ((index < 0) || (index >= this.list.Count))
				{
					throw new IndexOutOfRangeException("ListManagerNoValue " +  index.ToString() );
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
					int num1 = this.list.Count;
					if (value >= num1)
					{
						value = num1 - 1;
					}
					this.ChangeRecordState(value, this.listposition != value, true, true, false);
				}
			}
		}

//		public DataView this[string dataMember]
//		{
//			get
//			{
//				try
//				{
//					if(this.dataSource==null)
//						return null;
//					if (this.list is ITypedList)
//					{
//						if(list is DataViewManager)
//						{
//							return (DataView)((DataViewManager)this.list).DataSet.Tables[dataMember].DefaultView;
//						}
//						else
//						{
//							return (DataView)this.list as DataView;
//						}
//					}
//					return null;
//				}
//				catch//(Exception ex)
//				{
//					return null;
//				}
//			}
//		}

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
		#endregion

	}
 
}