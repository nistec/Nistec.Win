using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;
using System.Drawing.Design;
using System.Globalization;

namespace mControl.GridStyle
{
	[ListBindable(false), Editor("mControl.GridStyle.GridColumnCollectionEditor", typeof(UITypeEditor))]
	public class GridColumnsCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		// Events
		public event CollectionChangeEventHandler CollectionChanged;

		// Fields
		private bool isDefault;
		private ArrayList items;
		//private CollectionChangeEventHandler onCollectionChanged;
		private DataGridTableStyle owner;
		private Grid grid;

//		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
//		{
//            //
//		}

		private GridColumnsCollection(){}

		internal GridColumnsCollection(Grid g)//: this(null)
		{
			grid=g;
			this.items = new ArrayList();
			this.owner = null;
			this.isDefault = false;
			//this.owner = table;
		}

		internal GridColumnsCollection(Grid g,DataGridTableStyle table)
		{
			this.items = new ArrayList();
			//this.owner = null;
			this.isDefault = false;
			this.owner = table;
			grid=g;
		}
//
//		internal GridColumnsCollection(DataGridTableStyle table, bool isDefault) : this(table)
//		{
//			this.isDefault = isDefault;
//		}

		public virtual int Add(GridColumnStyle column)
		{
			if (this.isDefault)
			{
				throw new ArgumentException("DataGridDefaultColumnCollectionChanged");
			}
			this.CheckForMappingNameDuplicates(column);
			//column.SetDataGridTableInColumnInternal(this.owner, true);
			column.SetGridInColumnInternal(grid);
			column.MappingNameChanged += new EventHandler(this.ColumnStyleMappingNameChanged);
			column.PropertyDescriptorChanged += new EventHandler(this.ColumnStylePropDescChanged);
			if ((this.GridDataTable != null) && (column.Width == -1))
			{
				column.Width = this.GridDataTable.PreferredColumnWidth;
			}
			int num1 = this.items.Add(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
			return num1;
		}

		internal void AddDefaultColumn(GridColumnStyle column)
		{
		
			//column.SetDataGridTableInColumnInternal(this.owner, true);
			column.SetGridInColumnInternal(grid);
			this.items.Add(column);
		}

 
		public void AddRange(GridColumnStyle[] columns)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns");
			}
			for (int num1 = 0; num1 < columns.Length; num1++)
			{
				this.Add(columns[num1]);
			}
		}

		internal void CheckForMappingNameDuplicates(GridColumnStyle column)
		{
			if (!column.MappingName.Equals(string.Empty))
			{
				for (int num1 = 0; num1 < this.items.Count; num1++)
				{
					if (((GridColumnStyle) this.items[num1]).MappingName.Equals(column.MappingName) && (column != this.items[num1]))
					{
						throw new ArgumentException("GridColumnStyleDuplicateMappingName", "column");
					}
				}
			}
		}

		public void Clear()
		{
			for (int num1 = 0; num1 < this.Count; num1++)
			{
				this[num1].ReleaseHostedControlInternal();
			}
			this.items.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

 
		private void ColumnStyleMappingNameChanged(object sender, EventArgs pcea)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

 
		private void ColumnStylePropDescChanged(object sender, EventArgs pcea)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, (GridColumnStyle) sender));
		}

		public bool Contains(PropertyDescriptor propDesc)
		{
			return (this[propDesc] != null);
		}

 
		public bool Contains(string name)
		{
			IEnumerator enumerator1 = this.items.GetEnumerator();
			while (enumerator1.MoveNext())
			{
				GridColumnStyle style1 = (GridColumnStyle) enumerator1.Current;
				if (string.Compare(style1.MappingName, name, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		public bool Contains(GridColumnStyle column)
		{
			int num1 = this.items.IndexOf(column);
			return (num1 != -1);
		}

		public int IndexOf(GridColumnStyle element)
		{
			int num1 = this.items.Count;
			for (int num2 = 0; num2 < num1; num2++)
			{
				GridColumnStyle style1 = (GridColumnStyle) this.items[num2];
				if (element == style1)
				{
					return num2;
				}
			}
			return -1;
		}

		internal GridColumnStyle MapColumnStyleToPropertyName(string mappingName)
		{
			int num1 = this.items.Count;
			for (int num2 = 0; num2 < num1; num2++)
			{
				GridColumnStyle style1 = (GridColumnStyle) this.items[num2];
				if (string.Compare(style1.MappingName, mappingName, true, CultureInfo.InvariantCulture) == 0)
				{
					return style1;
				}
			}
			return null;
		}

 
		protected void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, ccevent);
			}
			//DataGrid grid1 = this.owner.DataGrid;
			//if (grid1 != null)
			//{
			//	grid1.checkHierarchy = true;
			//}
		}

		public void Remove(GridColumnStyle column)
		{
			if (this.isDefault)
			{
				throw new ArgumentException("DataGridDefaultColumnCollectionChanged");
			}
			int num1 = -1;
			int num2 = this.items.Count;
			for (int num3 = 0; num3 < num2; num3++)
			{
				if (this.items[num3] == column)
				{
					num1 = num3;
					break;
				}
			}
			if (num1 == -1)
			{
				throw new InvalidOperationException("DataGridColumnCollectionMissing");
			}
			this.RemoveAt(num1);
		}

 
		public void RemoveAt(int index)
		{
			if (this.isDefault)
			{
				throw new ArgumentException("DataGridDefaultColumnCollectionChanged");
			}
			GridColumnStyle style1 = (GridColumnStyle) this.items[index];
			style1.SetGridInColumnInternal(null);
			//style1.SetDataGridInColumnInternal(null, true);
			style1.MappingNameChanged -= new EventHandler(this.ColumnStyleMappingNameChanged);
			style1.PropertyDescriptorChanged -= new EventHandler(this.ColumnStylePropDescChanged);
			this.items.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, style1));
		}

		internal void ResetDefaultColumnCollection()
		{
			for (int num1 = 0; num1 < this.Count; num1++)
			{
				this[num1].ReleaseHostedControlInternal();
			}
			this.items.Clear();
		}

		public void ResetPropertyDescriptors()
		{
			for (int num1 = 0; num1 < this.Count; num1++)
			{
				this[num1].PropertyDescriptor = null;
			}
		}

		void ICollection.CopyTo(Array array, int index)//
		{
			this.items.CopyTo(array, index);
		}

		int ICollection.Count//ICollection.get_
		{
			get{return this.items.Count;}
		}

 
		bool ICollection.IsSynchronized//()ICollection.get_
		{
			get{return false;}
		}

		object ICollection.SyncRoot//()ICollection.get_
		{
			get{return this;}
		}

 
		IEnumerator IEnumerable.GetEnumerator()//IEnumerable.
		{
			return this.items.GetEnumerator();
		}

		int IList.Add(object value)//IList.
		{
			return this.Add((GridColumnStyle) value);
		}

		void IList.Clear()//IList.
		{
			this.Clear();
		}

		bool IList.Contains(object value)//IList.
		{
			return this.items.Contains(value);
		}

		bool IList.IsFixedSize//
		{
			get{return false;}
		}

 
		bool IList.IsReadOnly//IList.
		{
			get{return false;}
		}

		object  IList.this[int index]//IList.Item
		{
			get{return this.items[index];}
			set{this.items[index]=value;}
		}

		int IList.IndexOf(object value)//IList.
		{
			return this.items.IndexOf(value);
		}

 
		void IList.Insert(int index, object value)//IList.
		{
			throw new NotSupportedException();
		}

 
		void IList.Remove(object value)//IList.
		{
			this.Remove((GridColumnStyle) value);
		}

		void IList.RemoveAt(int index)//IList.
		{
			this.RemoveAt(index);
		}

//		void Item(int index, object value)//IList.set_
//		{
//			throw new NotSupportedException();
//		}

 
		internal DataGridTableStyle GridDataTable
		{
			get
			{
				return this.owner;
			}
		}
		public GridColumnStyle this[PropertyDescriptor propDesc]
		{
			get
			{
				int num1 = this.items.Count;
				for (int num2 = 0; num2 < num1; num2++)
				{
					GridColumnStyle style1 = (GridColumnStyle) this.items[num2];
					if (propDesc.Equals(style1.PropertyDescriptor))
					{
						return style1;
					}
				}
				return null;
			}
		}
		public GridColumnStyle this[string columnName]
		{
			get
			{
				int num1 = this.items.Count;
				for (int num2 = 0; num2 < num1; num2++)
				{
					GridColumnStyle style1 = (GridColumnStyle) this.items[num2];
					if (string.Compare(style1.MappingName, columnName, true, CultureInfo.InvariantCulture) == 0)
					{
						return style1;
					}
				}
				return null;
			}
		}
		public GridColumnStyle this[int index]
		{
			get
			{
				return (GridColumnStyle) this.items[index];
			}
		}
 
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}



	}
 

}
