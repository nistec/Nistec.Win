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
using System.Globalization;


namespace Nistec.GridView
{
	//[ListBindable(false), Editor("GridView.Design.GridColumnCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	
    /// <summary>
    /// Represents a collection of GridColumnStyle objects in the Grid control
    /// </summary>
    [ListBindable(false), Editor("Nistec.GridView.Design.GridColumnCollectionEditor", typeof(UITypeEditor))]
	public class GridColumnCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		/// <summary>
        /// Collection Changed event
		/// </summary>
		public event CollectionChangeEventHandler CollectionChanged;

		#region Methods
		internal GridColumnCollection(GridTableStyle table)
		{
			this.items = new ArrayList();
			this.owner = null;
			this.isDefault = false;
			this.owner = table;
		}

		internal GridColumnCollection(GridTableStyle table, bool isDefault) : this(table)
		{
			this.isDefault = isDefault;
		}

 
        /// <summary>
        /// Add column to collection
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
		public virtual int Add(GridColumnStyle column)
		{
			if (this.isDefault)
			{
                 throw new ArgumentException("GridDefaultColumnCollectionChanged");
			}
			this.CheckForMappingNameDuplicates(column);
			column.SetGridTableInColumn(this.owner, true);
			column.MappingNameChanged += new EventHandler(this.ColumnStyleMappingNameChanged);
			column.PropertyDescriptorChanged += new EventHandler(this.ColumnStylePropDescChanged);
			if ((this.GridTableStyle != null) && (column.Width == -1))
			{
				column.width = this.GridTableStyle.dataGrid.PreferredColumnWidth;
			}
			int num1 = this.items.Add(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
			return num1;
		}

		internal void AddDefaultColumn(GridColumnStyle column)
		{
			column.SetGridTableInColumn(this.owner, true);
			this.items.Add(column);
		}
        /// <summary>
        /// Add columns range to collection
        /// </summary>
        /// <param name="columns"></param>
        public void AddRange(string[] columns)
        {
            if (columns == null)
            {
                throw new ArgumentNullException("columns");
            }
            for (int num1 = 0; num1 < columns.Length; num1++)
            {
                GridColumnStyle col=new GridTextColumn();
                col.MappingName=columns[num1];
                col.HeaderText =columns[num1];
                this.Add(col);
            }
        }

        /// <summary>
        /// Add columns range to collection
        /// </summary>
        /// <param name="columns"></param>
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
        /// <summary>
        /// Add columns range to collection
        /// </summary>
        /// <param name="columns"></param>
		public void AddRange(GridColumnCollection columns)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns");
			}
			for (int num1 = 0; num1 < columns.Count; num1++)
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
						throw new ArgumentException("GridColumnStyleDuplicateMappingName ", column.MappingName);
					}
				}
			}
		}
        /// <summary>
        /// Clear grid columns collection
        /// </summary>
		public void Clear()
		{
			for (int num1 = 0; num1 < this.Count; num1++)
			{
				this[num1].ReleaseHostedControl();
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
        /// <summary>
        /// Overloaded. Gets a value indicating whether the GridColumnCollection contains a specific GridColumnStyle.
        /// </summary>
        /// <param name="propDesc"></param>
        /// <returns></returns>
		public bool Contains(PropertyDescriptor propDesc)
		{
			return (this[propDesc] != null);
		}
        /// <summary>
        /// Overloaded. Gets a value indicating whether the GridColumnCollection contains a specific GridColumnStyle.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
		public bool Contains(string name)
		{
			foreach (GridColumnStyle style1 in this.items)
			{
				if (string.Compare(style1.MappingName, name, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

 
        /// <summary>
        /// Overloaded. Gets a value indicating whether the GridColumnCollection contains a specific GridColumnStyle.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
		public bool Contains(GridColumnStyle column)
		{
			int num1 = this.items.IndexOf(column);
			return (num1 != -1);
		}

 
        /// <summary>
        /// Gets the index of a specified GridColumnStyle
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the index of a specified GridColumnStyle
        /// </summary>
        /// <param name="mappingName"></param>
        /// <returns></returns>
		public int IndexOf(string mappingName)
		{
			int num1 = this.items.Count;
			for (int num2 = 0; num2 < num1; num2++)
			{
				GridColumnStyle style1 = (GridColumnStyle) this.items[num2];
				if (string.Compare(style1.MappingName, mappingName, true, CultureInfo.InvariantCulture) == 0)
				{
					return num2;
				}
			}
			return -1;
		}
        /// <summary>
        /// MapColumnStyleToPropertyName
        /// </summary>
        /// <param name="mappingName"></param>
        /// <param name="col"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        internal GridColumnStyle MapColumnStyleToPropertyName(string mappingName, GridColumnStyle col, int colIndex)//,ref bool isDescp)
		{
            /*bound*/
            if (!col.isMaped && !col.IsBound)
            {
                //col.MappingName = "UnBound" + colIndex.ToString();
                //col.isMaped = true;
                //isDescp = false;
                return col;
            }

            int cnt = this.items.Count;
            GridColumnStyle style1 = null;

            for (int i = 0; i < cnt; i++)
			{
				style1 = (GridColumnStyle) this.items[i];
                if (string.Compare(style1.MappingName, mappingName, true, CultureInfo.InvariantCulture) == 0)
				{
                    //style1.isMaped = true;
                    //isDescp = true;
					return style1;
				}
            }
			return null;
		}
        /// <summary>
        /// Raises the CollectionChanged event. 
        /// </summary>
        /// <param name="ccevent"></param>
		protected void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, ccevent);
			}
			Grid grid1 = this.owner.Grid;
			if (grid1 != null)
			{
				grid1.checkHierarchy = true;
			}
		}
        /// <summary>
        /// Removes the specified GridColumnStyle from the GridColumnCollection
        /// </summary>
        /// <param name="column"></param>
		public void Remove(GridColumnStyle column)
		{
			if (this.isDefault)
			{
				throw new ArgumentException("GridDefaultColumnCollectionChanged");
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
				throw new InvalidOperationException("GridColumnCollectionMissing");
			}
			this.RemoveAt(num1);
		}

 
        /// <summary>
        /// Removes the GridColumnStyle with the specified index from the GridColumnCollection. 
        /// </summary>
        /// <param name="index"></param>
		public void RemoveAt(int index)
		{
			if (this.isDefault)
			{
				throw new ArgumentException("GridDefaultColumnCollectionChanged");
			}
			GridColumnStyle style1 = (GridColumnStyle) this.items[index];
			style1.SetGridTableInColumn(null, true);
			style1.MappingNameChanged -= new EventHandler(this.ColumnStyleMappingNameChanged);
			style1.PropertyDescriptorChanged -= new EventHandler(this.ColumnStylePropDescChanged);
			this.items.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, style1));
		}

 
		internal void ResetDefaultColumnCollection()
		{
			for (int num1 = 0; num1 < this.Count; num1++)
			{
				this[num1].ReleaseHostedControl();
			}
			this.items.Clear();
		}
        /// <summary>
        /// Sets the PropertyDescriptor for each column style in the collection to a null
        /// </summary>
		public void ResetPropertyDescriptors()
		{
			for (int num1 = 0; num1 < this.Count; num1++)
			{
				this[num1].PropertyDescriptor = null;
			}
		}

		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

 
//		int ICollection.get_Count()
//		{
//			return this.items.Count;
//		}
//
// 
//		bool ICollection.get_IsSynchronized()
//		{
//			return false;
//		}
//
//		object ICollection.get_SyncRoot()
//		{
//			return this;
//		}

 
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

 
		int IList.Add(object value)
		{
			return this.Add((GridColumnStyle) value);
		}

 
		void IList.Clear()
		{
			this.Clear();
		}

 
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

//		bool IList.get_IsFixedSize()
//		{
//			return false;
//		}
//
// 
//		bool IList.get_IsReadOnly()
//		{
//			return false;
//		}
//
// 
//		object IList.get_Item(int index)
//		{
//			return this.items[index];
//		}

		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

 
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		void IList.Remove(object value)
		{
			this.Remove((GridColumnStyle) value);
		}

		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

 
//		void IList.set_Item(int index, object value)
//		{
//			throw new NotSupportedException();
//		}
//

		#endregion

		#region  Properties
		internal GridTableStyle GridTableStyle
		{
			get
			{
				return this.owner;
			}
		}
 
        /// <summary>
        /// Overloaded. Gets a specified GridColumnStyle in the GridColumnCollection.
        /// </summary>
        /// <param name="propDesc"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Overloaded. Gets a specified GridColumnStyle in the GridColumnCollection.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
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
 
//		public GridColumnStyle this[int index]
//		{
//			get
//			{
//				return (GridColumnStyle) this.items[index];
//			}
//		}

        /// <summary>
        /// Overloaded. Gets a specified GridColumnStyle in the GridColumnCollection.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public GridColumnStyle this[int index]
		{
			get
			{
				return (GridColumnStyle) this.items[index];
			}
//			set
//			{
//				this.items[index]=value;
//			}
		}

		object IList.this[int index]
		{
			get
			{
				return (GridColumnStyle) this.items[index];
			}
			set
			{
				this.items[index]=value;
			}
		}

        /// <summary>
        /// Overridden. Gets the list of items in the collection.
        /// </summary>
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		#endregion
        /// <summary>
        /// 
        /// get if Is Fixed Size
        /// </summary>
		public bool IsFixedSize { get{ return false;} }
		//public bool IsReadOnly { get{return false;} }

		// Fields
		private bool isDefault;
		private ArrayList items;
		//private CollectionChangeEventHandler onCollectionChanged;
		private GridTableStyle owner;
	}

}
