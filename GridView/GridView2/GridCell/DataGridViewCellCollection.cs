namespace MControl.GridView
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>Represents a collection of cells in a <see cref="T:MControl.GridView.GridRow"></see>.</summary>
    /// <filterpriority>2</filterpriority>
    [ListBindable(false)]
    public class GridCellCollection : BaseCollection, IList, ICollection, IEnumerable
    {
        private ArrayList items = new ArrayList();
        private GridRow owner;

        /// <summary>Occurs when the collection is changed. </summary>
        /// <filterpriority>1</filterpriority>
        public event CollectionChangeEventHandler CollectionChanged;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellCollection"></see> class.</summary>
        /// <param name="gridRow">The <see cref="T:MControl.GridView.GridRow"></see> that owns the collection.</param>
        public GridCellCollection(GridRow gridRow)
        {
            this.owner = gridRow;
        }

        /// <summary>Adds a cell to the collection.</summary>
        /// <returns>The position in which to insert the new element.</returns>
        /// <param name="gridCell">A <see cref="T:MControl.GridView.GridCell"></see> to add to the collection.</param>
        /// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:MControl.GridView.GridCellCollection"></see> already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.-or-gridCell already belongs to a <see cref="T:MControl.GridView.GridRow"></see>.</exception>
        /// <filterpriority>1</filterpriority>
        public virtual int Add(GridCell gridCell)
        {
            if (this.owner.Grid != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_OwningRowAlreadyBelongsToGrid"));
            }
            if (gridCell.OwningRow != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_CellAlreadyBelongsToGridRow"));
            }
            return this.AddInternal(gridCell);
        }

        internal int AddInternal(GridCell gridCell)
        {
            int num = this.items.Add(gridCell);
            gridCell.OwningRowInternal = this.owner;
            Grid grid = this.owner.Grid;
            if ((grid != null) && (grid.Columns.Count > num))
            {
                gridCell.OwningColumnInternal = grid.Columns[num];
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, gridCell));
            return num;
        }

        /// <summary>Adds an array of cells to the collection.</summary>
        /// <param name="gridCells">The array of <see cref="T:MControl.GridView.GridCell"></see> objects to add to the collection.</param>
        /// <exception cref="T:System.ArgumentNullException">gridCells is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:MControl.GridView.GridCellCollection"></see> already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.-or-At least one value in gridCells is null.-or-At least one cell in gridCells already belongs to a <see cref="T:MControl.GridView.GridRow"></see>.-or-At least two values in gridCells are references to the same <see cref="T:MControl.GridView.GridCell"></see>.</exception>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual void AddRange(params GridCell[] gridCells)
        {
            if (gridCells == null)
            {
                throw new ArgumentNullException("gridCells");
            }
            if (this.owner.Grid != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_OwningRowAlreadyBelongsToGrid"));
            }
            foreach (GridCell cell in gridCells)
            {
                if (cell == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_AtLeastOneCellIsNull"));
                }
                if (cell.OwningRow != null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_CellAlreadyBelongsToGridRow"));
                }
            }
            int length = gridCells.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (gridCells[i] == gridCells[j])
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_CannotAddIdenticalCells"));
                    }
                }
            }
            this.items.AddRange(gridCells);
            foreach (GridCell cell2 in gridCells)
            {
                cell2.OwningRowInternal = this.owner;
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
        }

        /// <summary>Clears all cells from the collection.</summary>
        /// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:MControl.GridView.GridCellCollection"></see> already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Clear()
        {
            if (this.owner.Grid != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_OwningRowAlreadyBelongsToGrid"));
            }
            foreach (GridCell cell in this.items)
            {
                cell.OwningRowInternal = null;
            }
            this.items.Clear();
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
        }

        /// <summary>Determines whether the specified cell is contained in the collection.</summary>
        /// <returns>true if gridCell is in the collection; otherwise, false.</returns>
        /// <param name="gridCell">A <see cref="T:MControl.GridView.GridCell"></see> to locate in the collection.</param>
        /// <filterpriority>1</filterpriority>
        public virtual bool Contains(GridCell gridCell)
        {
            return (this.items.IndexOf(gridCell) != -1);
        }

        /// <summary>Copies the entire collection of cells into an array at a specified location within the array.</summary>
        /// <param name="array">The destination array to which the contents will be copied.</param>
        /// <param name="index">The index of the element in array at which to start copying.</param>
        /// <filterpriority>1</filterpriority>
        public void CopyTo(GridCell[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        /// <summary>Returns the index of the specified cell.</summary>
        /// <returns>The zero-based index of the value of gridCell parameter, if it is found in the collection; otherwise, -1.</returns>
        /// <param name="gridCell">The cell to locate in the collection.</param>
        /// <filterpriority>1</filterpriority>
        public int IndexOf(GridCell gridCell)
        {
            return this.items.IndexOf(gridCell);
        }

        /// <summary>Inserts a cell into the collection at the specified index. </summary>
        /// <param name="gridCell">The <see cref="T:MControl.GridView.GridCell"></see> to insert.</param>
        /// <param name="index">The zero-based index at which to place gridCell.</param>
        /// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:MControl.GridView.GridCellCollection"></see> already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.-or-gridCell already belongs to a <see cref="T:MControl.GridView.GridRow"></see>.</exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Insert(int index, GridCell gridCell)
        {
            if (this.owner.Grid != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_OwningRowAlreadyBelongsToGrid"));
            }
            if (gridCell.OwningRow != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_CellAlreadyBelongsToGridRow"));
            }
            this.items.Insert(index, gridCell);
            gridCell.OwningRowInternal = this.owner;
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, gridCell));
        }

        internal void InsertInternal(int index, GridCell gridCell)
        {
            this.items.Insert(index, gridCell);
            gridCell.OwningRowInternal = this.owner;
            Grid grid = this.owner.Grid;
            if ((grid != null) && (grid.Columns.Count > index))
            {
                gridCell.OwningColumnInternal = grid.Columns[index];
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, gridCell));
        }

        /// <summary>Raises the <see cref="E:MControl.GridView.GridCellCollection.CollectionChanged"></see> event.</summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs"></see> that contains the event data. </param>
        protected void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (this.onCollectionChanged != null)
            {
                this.onCollectionChanged(this, e);
            }
        }

        /// <summary>Removes the specified cell from the collection.</summary>
        /// <param name="cell">The <see cref="T:MControl.GridView.GridCell"></see> to remove from the collection.</param>
        /// <exception cref="T:System.ArgumentException">cell could not be found in the collection.</exception>
        /// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:MControl.GridView.GridCellCollection"></see> already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Remove(GridCell cell)
        {
            if (this.owner.Grid != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_OwningRowAlreadyBelongsToGrid"));
            }
            int index = -1;
            int count = this.items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.items[i] == cell)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("GridCellCollection_CellNotFound"));
            }
            this.RemoveAt(index);
        }

        /// <summary>Removes the cell at the specified index.</summary>
        /// <param name="index">The zero-based index of the <see cref="T:MControl.GridView.GridCell"></see> to be removed.</param>
        /// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:MControl.GridView.GridCellCollection"></see> already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void RemoveAt(int index)
        {
            if (this.owner.Grid != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_OwningRowAlreadyBelongsToGrid"));
            }
            this.RemoveAtInternal(index);
        }

        internal void RemoveAtInternal(int index)
        {
            GridCell element = (GridCell) this.items[index];
            this.items.RemoveAt(index);
            element.GridInternal = null;
            element.OwningRowInternal = null;
            if (element.ReadOnly)
            {
                element.ReadOnlyInternal = false;
            }
            if (element.Selected)
            {
                element.SelectedInternal = false;
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, element));
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.items.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        int IList.Add(object value)
        {
            return this.Add((GridCell) value);
        }

        void IList.Clear()
        {
            this.Clear();
        }

        bool IList.Contains(object value)
        {
            return this.items.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return this.items.IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (GridCell) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((GridCell) value);
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        /// <summary>Gets or sets the cell at the provided index location. In C#, this property is the indexer for the <see cref="T:MControl.GridView.GridCellCollection"></see> class.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCell"></see> stored at the given index.</returns>
        /// <param name="index">The zero-based index of the cell to get or set.</param>
        /// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.-or-The specified cell when setting this property already belongs to a <see cref="T:MControl.GridView.GridRow"></see>.</exception>
        /// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
        /// <filterpriority>1</filterpriority>
        public GridCell this[int index]
        {
            get
            {
                return (GridCell) this.items[index];
            }
            set
            {
                GridCell cell = value;
                if (cell == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (cell.Grid != null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_CellAlreadyBelongsToGrid"));
                }
                if (cell.OwningRow != null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellCollection_CellAlreadyBelongsToGridRow"));
                }
                if (this.owner.Grid != null)
                {
                    this.owner.Grid.OnReplacingCell(this.owner, index);
                }
                GridCell cell2 = (GridCell) this.items[index];
                this.items[index] = cell;
                cell.OwningRowInternal = this.owner;
                cell.StateInternal = cell2.State;
                if (this.owner.Grid != null)
                {
                    cell.GridInternal = this.owner.Grid;
                    cell.OwningColumnInternal = this.owner.Grid.Columns[index];
                    this.owner.Grid.OnReplacedCell(this.owner, index);
                }
                cell2.GridInternal = null;
                cell2.OwningRowInternal = null;
                cell2.OwningColumnInternal = null;
                if (cell2.ReadOnly)
                {
                    cell2.ReadOnlyInternal = false;
                }
                if (cell2.Selected)
                {
                    cell2.SelectedInternal = false;
                }
            }
        }

        /// <summary>Gets or sets the cell in the column with the provided name. In C#, this property is the indexer for the <see cref="T:MControl.GridView.GridCellCollection"></see> class.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCell"></see> stored in the column with the given name.</returns>
        /// <param name="columnName">The name of the column in which to get or set the cell.</param>
        /// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.-or-The specified cell when setting this property already belongs to a <see cref="T:MControl.GridView.GridRow"></see>.</exception>
        /// <exception cref="T:System.ArgumentException">columnName does not match the name of any columns in the control.</exception>
        /// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
        /// <filterpriority>1</filterpriority>
        public GridCell this[string columnName]
        {
            get
            {
                GridColumn column = null;
                if (this.owner.Grid != null)
                {
                    column = this.owner.Grid.Columns[columnName];
                }
                if (column == null)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("GridColumnCollection_ColumnNotFound", new object[] { columnName }), "columnName");
                }
                return (GridCell) this.items[column.Index];
            }
            set
            {
                GridColumn column = null;
                if (this.owner.Grid != null)
                {
                    column = this.owner.Grid.Columns[columnName];
                }
                if (column == null)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("GridColumnCollection_ColumnNotFound", new object[] { columnName }), "columnName");
                }
                this[column.Index] = value;
            }
        }

        /// <summary>Gets an <see cref="T:System.Collections.ArrayList"></see> containing <see cref="T:MControl.GridView.GridCellCollection"></see> objects.</summary>
        /// <returns><see cref="T:System.Collections.ArrayList"></see>.</returns>
        protected override ArrayList List
        {
            get
            {
                return this.items;
            }
        }

        int ICollection.Count
        {
            get
            {
                return this.items.Count;
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
                return this;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool IList.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (GridCell) value;
            }
        }
    }
}

