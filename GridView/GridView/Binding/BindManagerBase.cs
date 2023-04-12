using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace mControl.WinCtl.Controls
{

	public abstract class BindManagerBase
	{

		#region Fields

		// Fields
		private BindCollection bindings;
		protected EventHandler onCurrentChangedHandler;
		protected EventHandler onPositionChangedHandler;
		private bool pullingData;

		// Events
		public event EventHandler CurrentChanged;
		public event EventHandler PositionChanged;

		#endregion

		#region virtual

		public abstract void AddNew();
		public abstract void CancelCurrentEdit();
		public abstract void EndCurrentEdit();
		public abstract PropertyDescriptorCollection GetItemProperties();

		public abstract string GetListName();
		protected internal abstract string GetListName(ArrayList listAccessors);
		//protected internal abstract void OnCurrentChanged(EventArgs e);

		public abstract void RemoveAt(int index);
		public abstract void ResumeBinding();
		public abstract void SetDataSource(object dataSource);
		public abstract void SuspendBinding();
		protected abstract void UpdateIsBinding();

		internal abstract Type BindType { get; }
 
		public abstract int Count { get; }
		public abstract object Current { get; }
 
		public abstract object DataSource { get; }
		public abstract bool IsBinding { get; }
		public abstract int Position { get; set; }
 
		#endregion

		#region Methods
		
		public BindManagerBase()
		{
			this.pullingData = false;

		}

		internal BindManagerBase(object dataSource)
		{
			this.pullingData = false;
			this.SetDataSource(dataSource);
		}

		protected internal virtual PropertyDescriptorCollection GetItemProperties(ArrayList dataSources, ArrayList listAccessors)
		{
			IList list1 = null;
			if (this is BindManager)
			{
				list1 = ((BindManager) this).List;
			}
			if (list1 is ITypedList)
			{
				PropertyDescriptor[] descriptorArray1 = new PropertyDescriptor[listAccessors.Count];
				listAccessors.CopyTo(descriptorArray1, 0);
				return ((ITypedList) list1).GetItemProperties(descriptorArray1);
			}
			return this.GetItemProperties(this.BindType, 0, dataSources, listAccessors);
		}

 
		protected virtual PropertyDescriptorCollection GetItemProperties(Type listType, int offset, ArrayList dataSources, ArrayList listAccessors)
		{
			if (listAccessors.Count >= offset)
			{
				if (listAccessors.Count == offset)
				{
					if (!typeof(IList).IsAssignableFrom(listType))
					{
						return TypeDescriptor.GetProperties(listType);
					}
					PropertyInfo[] infoArray1 = listType.GetProperties();
					for (int num1 = 0; num1 < infoArray1.Length; num1++)
					{
						if ("Item".Equals(infoArray1[num1].Name) && (infoArray1[num1].PropertyType != typeof(object)))
						{
							return TypeDescriptor.GetProperties(infoArray1[num1].PropertyType, new Attribute[] { new BrowsableAttribute(true) });
						}
					}
					IList list1 = dataSources[offset - 1] as IList;
					if ((list1 == null) || (list1.Count <= 0))
					{
						return null;
					}
					return TypeDescriptor.GetProperties(list1[0]);
				}
				PropertyInfo[] infoArray2 = listType.GetProperties();
				if (typeof(IList).IsAssignableFrom(listType))
				{
					PropertyDescriptorCollection collection1 = null;
					for (int num2 = 0; num2 < infoArray2.Length; num2++)
					{
						if ("Item".Equals(infoArray2[num2].Name) && (infoArray2[num2].PropertyType != typeof(object)))
						{
							collection1 = TypeDescriptor.GetProperties(infoArray2[num2].PropertyType, new Attribute[] { new BrowsableAttribute(true) });
						}
					}
					if (collection1 == null)
					{
						IList list2;
						if (offset == 0)
						{
							list2 = this.DataSource as IList;
						}
						else
						{
							list2 = dataSources[offset - 1] as IList;
						}
						if ((list2 != null) && (list2.Count > 0))
						{
							collection1 = TypeDescriptor.GetProperties(list2[0]);
						}
					}
					if (collection1 != null)
					{
						for (int num3 = 0; num3 < collection1.Count; num3++)
						{
							if (collection1[num3].Equals(listAccessors[offset]))
							{
								return this.GetItemProperties(collection1[num3].PropertyType, offset + 1, dataSources, listAccessors);
							}
						}
					}
				}
				else
				{
					for (int num4 = 0; num4 < infoArray2.Length; num4++)
					{
						if (infoArray2[num4].Name.Equals(((PropertyDescriptor) listAccessors[offset]).Name))
						{
							return this.GetItemProperties(infoArray2[num4].PropertyType, offset + 1, dataSources, listAccessors);
						}
					}
				}
			}
			return null;
		}

 
 
		protected void PullData()
		{
			this.pullingData = true;
			try
			{
				this.UpdateIsBinding();
				int num1 = this.Bindings.Count;
				for (int num2 = 0; num2 < num1; num2++)
				{
					this.Bindings[num2].PullData();
				}
			}
			finally
			{
				this.pullingData = false;
			}
		}

 
		protected void PushData()
		{
			if (!this.pullingData)
			{
				this.UpdateIsBinding();
				int num1 = this.Bindings.Count;
				for (int num2 = 0; num2 < num1; num2++)
				{
					this.Bindings[num2].PushData();
				}
			}
		}

		public BindCollection Bindings
		{
			get
			{
				if (this.bindings == null)
				{
					this.bindings = new BindListManager(this);// BindCollection(this);
				}
				return this.bindings;
			}
		}
 
 
		internal protected virtual void OnCurrentChanged(EventArgs e)
		{
			if(CurrentChanged!=null)
			{
				onCurrentChangedHandler=(EventHandler) Delegate.Combine(this.onCurrentChangedHandler, CurrentChanged);
				CurrentChanged(this,e);
			}
			
		}

		protected virtual void OnPositionChanged(EventArgs e)
		{
			if(PositionChanged!=null)
			{
				onPositionChangedHandler=(EventHandler) Delegate.Combine(this.onPositionChangedHandler, PositionChanged);
				PositionChanged(this,e);
			}
		}


//		public void add_PositionChanged(EventHandler value)
//		{
//			this.onPositionChangedHandler = (EventHandler) Delegate.Combine(this.onPositionChangedHandler, value);
//		}
//
//		public void remove_PositionChanged(EventHandler value)
//		{
//			this.onPositionChangedHandler = (EventHandler) Delegate.Remove(this.onPositionChangedHandler, value);
//		}
//	

		#endregion

	}

}