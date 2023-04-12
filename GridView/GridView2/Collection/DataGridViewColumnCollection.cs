namespace MControl.GridView
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>Represents a collection of <see cref="T:MControl.GridView.GridColumn"></see> objects in a <see cref="T:MControl.GridView.Grid"></see> control. </summary>
    /// <filterpriority>2</filterpriority>
    [ListBindable(false)]
    public class GridColumnCollection : BaseCollection, IList, ICollection, IEnumerable
    {
        private int columnCountsVisible;
        private int columnCountsVisibleSelected;
        private static ColumnOrderComparer columnOrderComparer = new ColumnOrderComparer();
        private int columnsWidthVisible;
        private int columnsWidthVisibleFrozen;
        private MControl.GridView.Grid grid;
        private ArrayList items = new ArrayList();
        private ArrayList itemsSorted;
        private int lastAccessedSortedIndex = -1;

        /// <summary>Occurs when the collection changes.</summary>
        /// <filterpriority>1</filterpriority>
        public event CollectionChangeEventHandler CollectionChanged;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridColumnCollection"></see> class for the given <see cref="T:MControl.GridView.Grid"></see>. </summary>
        /// <param name="grid">The <see cref="T:MControl.GridView.Grid"></see> that created this collection.</param>
        public GridColumnCollection(MControl.GridView.Grid grid)
        {
            this.InvalidateCachedColumnCounts();
            this.InvalidateCachedColumnsWidths();
            this.grid = grid;
        }

        internal int ActualDisplayIndexToColumnIndex(int actualDisplayIndex, GridElementStates includeFilter)
        {
            GridColumn firstColumn = this.GetFirstColumn(includeFilter);
            for (int i = 0; i < actualDisplayIndex; i++)
            {
                firstColumn = this.GetNextColumn(firstColumn, includeFilter, GridElementStates.None);
            }
            return firstColumn.Index;
        }

        /// <summary>Adds the given column to the collection.</summary>
        /// <returns>The index of the column.</returns>
        /// <param name="gridColumn">The <see cref="T:MControl.GridView.GridColumn"></see> to add.</param>
        /// <exception cref="T:System.ArgumentNullException">gridColumn is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> property values. -or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-gridColumn already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.-or-The gridColumn<see cref="P:MControl.GridView.GridColumn.SortMode"></see> property value is <see cref="F:MControl.GridView.GridColumnSortMode.Automatic"></see> and the <see cref="P:MControl.GridView.Grid.SelectionMode"></see> property value is <see cref="F:MControl.GridView.GridSelectionMode.FullColumnSelect"></see> or <see cref="F:MControl.GridView.GridSelectionMode.ColumnHeaderSelect"></see>. Use the control <see cref="M:MControl.GridView.Grid.System.ComponentModel.ISupportInitialize.BeginInit"></see> and <see cref="M:MControl.GridView.Grid.System.ComponentModel.ISupportInitialize.EndInit"></see> methods to temporarily set conflicting property values. -or-The gridColumn<see cref="P:MControl.GridView.GridColumn.InheritedAutoSizeMode"></see> property value is <see cref="F:MControl.GridView.GridAutoSizeColumnMode.ColumnHeader"></see> and the <see cref="P:MControl.GridView.Grid.ColumnHeadersVisible"></see> property value is false.-or-gridColumn has an <see cref="P:MControl.GridView.GridColumn.InheritedAutoSizeMode"></see> property value of <see cref="F:MControl.GridView.GridAutoSizeColumnMode.Fill"></see> and a <see cref="P:MControl.GridView.GridColumn.Frozen"></see> property value of true.-or-gridColumn has a <see cref="P:MControl.GridView.GridColumn.FillWeight"></see> property value that would cause the combined <see cref="P:MControl.GridView.GridColumn.FillWeight"></see> values of all columns in the control to exceed 65535.-or-gridColumn has <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> and <see cref="P:MControl.GridView.GridColumn.Frozen"></see> property values that would display it among a set of adjacent columns with the opposite <see cref="P:MControl.GridView.GridColumn.Frozen"></see> property value.-or-The <see cref="T:MControl.GridView.Grid"></see> control contains at least one row and gridColumn has a <see cref="P:MControl.GridView.GridColumn.CellType"></see> property value of null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual int Add(GridColumn gridColumn)
        {
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            if (this.Grid.InDisplayIndexAdjustments)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_CannotAlterDisplayIndexWithinAdjustments"));
            }
            this.Grid.OnAddingColumn(gridColumn);
            this.InvalidateCachedColumnsOrder();
            int num = this.items.Add(gridColumn);
            gridColumn.IndexInternal = num;
            gridColumn.GridInternal = this.grid;
            this.UpdateColumnCaches(gridColumn, true);
            this.Grid.OnAddedColumn(gridColumn);
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, gridColumn), false, new Point(-1, -1));
            return num;
        }

        /// <summary>Adds a <see cref="T:MControl.GridView.GridTextBoxColumn"></see> with the given column name and column header text to the collection.</summary>
        /// <returns>The index of the column.</returns>
        /// <param name="headerText">The text for the column's header.</param>
        /// <param name="columnName">The name by which the column will be referred.</param>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> property values. -or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.SelectionMode"></see> property value is <see cref="F:MControl.GridView.GridSelectionMode.FullColumnSelect"></see> or <see cref="F:MControl.GridView.GridSelectionMode.ColumnHeaderSelect"></see>, which conflicts with the default column <see cref="P:MControl.GridView.GridColumn.SortMode"></see> property value of <see cref="F:MControl.GridView.GridColumnSortMode.Automatic"></see>.-or-The default column <see cref="P:MControl.GridView.GridColumn.FillWeight"></see> property value of 100 would cause the combined <see cref="P:MControl.GridView.GridColumn.FillWeight"></see> values of all columns in the control to exceed 65535.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Add(string columnName, string headerText)
        {
            GridTextBoxColumn gridColumn = new GridTextBoxColumn();
            gridColumn.Name = columnName;
            gridColumn.HeaderText = headerText;
            return this.Add(gridColumn);
        }

        /// <summary>Adds a range of columns to the collection. </summary>
        /// <param name="gridColumns">An array of <see cref="T:MControl.GridView.GridColumn"></see> objects to add.</param>
        /// <exception cref="T:System.ArgumentNullException">gridColumns is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> property values. -or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-At least one of the values in gridColumns is null.-or-At least one of the columns in gridColumns already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.-or-At least one of the columns in gridColumns has a <see cref="P:MControl.GridView.GridColumn.CellType"></see> property value of null and the <see cref="T:MControl.GridView.Grid"></see> control contains at least one row.-or-At least one of the columns in gridColumns has a <see cref="P:MControl.GridView.GridColumn.SortMode"></see> property value of <see cref="F:MControl.GridView.GridColumnSortMode.Automatic"></see> and the <see cref="P:MControl.GridView.Grid.SelectionMode"></see> property value is <see cref="F:MControl.GridView.GridSelectionMode.FullColumnSelect"></see> or <see cref="F:MControl.GridView.GridSelectionMode.ColumnHeaderSelect"></see>. Use the control <see cref="M:MControl.GridView.Grid.System.ComponentModel.ISupportInitialize.BeginInit"></see> and <see cref="M:MControl.GridView.Grid.System.ComponentModel.ISupportInitialize.EndInit"></see> methods to temporarily set conflicting property values. -or-At least one of the columns in gridColumns has an <see cref="P:MControl.GridView.GridColumn.InheritedAutoSizeMode"></see> property value of <see cref="F:MControl.GridView.GridAutoSizeColumnMode.ColumnHeader"></see> and the <see cref="P:MControl.GridView.Grid.ColumnHeadersVisible"></see> property value is false.-or-At least one of the columns in gridColumns has an <see cref="P:MControl.GridView.GridColumn.InheritedAutoSizeMode"></see> property value of <see cref="F:MControl.GridView.GridAutoSizeColumnMode.Fill"></see> and a <see cref="P:MControl.GridView.GridColumn.Frozen"></see> property value of true.-or-The columns in gridColumns have <see cref="P:MControl.GridView.GridColumn.FillWeight"></see> property values that would cause the combined <see cref="P:MControl.GridView.GridColumn.FillWeight"></see> values of all columns in the control to exceed 65535.-or-At least two of the values in gridColumns are references to the same <see cref="T:MControl.GridView.GridColumn"></see>.-or-At least one of the columns in gridColumns has <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> and <see cref="P:MControl.GridView.GridColumn.Frozen"></see> property values that would display it among a set of adjacent columns with the opposite <see cref="P:MControl.GridView.GridColumn.Frozen"></see> property value.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void AddRange(params GridColumn[] gridColumns)
        {
            int num3;
            if (gridColumns == null)
            {
                throw new ArgumentNullException("gridColumns");
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            if (this.Grid.InDisplayIndexAdjustments)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_CannotAlterDisplayIndexWithinAdjustments"));
            }
            ArrayList list = new ArrayList(gridColumns.Length);
            ArrayList list2 = new ArrayList(gridColumns.Length);
            foreach (GridColumn column in gridColumns)
            {
                if (column.DisplayIndex != -1)
                {
                    list.Add(column);
                }
            }
            while (list.Count > 0)
            {
                int displayIndex = 0x7fffffff;
                int index = -1;
                for (num3 = 0; num3 < list.Count; num3++)
                {
                    GridColumn column2 = (GridColumn) list[num3];
                    if (column2.DisplayIndex < displayIndex)
                    {
                        displayIndex = column2.DisplayIndex;
                        index = num3;
                    }
                }
                list2.Add(list[index]);
                list.RemoveAt(index);
            }
            foreach (GridColumn column3 in gridColumns)
            {
                if (column3.DisplayIndex == -1)
                {
                    list2.Add(column3);
                }
            }
            num3 = 0;
            foreach (GridColumn column4 in list2)
            {
                gridColumns[num3] = column4;
                num3++;
            }
            this.Grid.OnAddingColumns(gridColumns);
            foreach (GridColumn column5 in gridColumns)
            {
                this.InvalidateCachedColumnsOrder();
                num3 = this.items.Add(column5);
                column5.IndexInternal = num3;
                column5.GridInternal = this.grid;
                this.UpdateColumnCaches(column5, true);
                this.Grid.OnAddedColumn(column5);
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), false, new Point(-1, -1));
        }

        /// <summary>Clears the collection. </summary>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> property values. -or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see></exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Clear()
        {
            if (this.Count > 0)
            {
                if (this.Grid.NoDimensionChangeAllowed)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
                }
                if (this.Grid.InDisplayIndexAdjustments)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_CannotAlterDisplayIndexWithinAdjustments"));
                }
                for (int i = 0; i < this.Count; i++)
                {
                    GridColumn column = this[i];
                    column.GridInternal = null;
                    if (column.HasHeaderCell)
                    {
                        column.HeaderCell.GridInternal = null;
                    }
                }
                GridColumn[] array = new GridColumn[this.items.Count];
                this.CopyTo(array, 0);
                this.Grid.OnClearingColumns();
                this.InvalidateCachedColumnsOrder();
                this.items.Clear();
                this.InvalidateCachedColumnCounts();
                this.InvalidateCachedColumnsWidths();
                foreach (GridColumn column2 in array)
                {
                    this.Grid.OnColumnRemoved(column2);
                    this.Grid.OnColumnHidden(column2);
                }
                this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), false, new Point(-1, -1));
            }
        }

        internal int ColumnIndexToActualDisplayIndex(int columnIndex, GridElementStates includeFilter)
        {
            GridColumn firstColumn = this.GetFirstColumn(includeFilter);
            int num = 0;
            while ((firstColumn != null) && (firstColumn.Index != columnIndex))
            {
                firstColumn = this.GetNextColumn(firstColumn, includeFilter, GridElementStates.None);
                num++;
            }
            return num;
        }

        /// <summary>Determines whether the collection contains the column referred to by the given name. </summary>
        /// <returns>true if the column is contained in the collection; otherwise, false.</returns>
        /// <param name="columnName">The name of the column to look for.</param>
        /// <exception cref="T:System.ArgumentNullException">columnName is null.</exception>
        /// <filterpriority>1</filterpriority>
        public virtual bool Contains(string columnName)
        {
            if (columnName == null)
            {
                throw new ArgumentNullException("columnName");
            }
            int count = this.items.Count;
            for (int i = 0; i < count; i++)
            {
                GridColumn column = (GridColumn) this.items[i];
                if (string.Compare(column.Name, columnName, true, CultureInfo.InvariantCulture) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Determines whether the collection contains the given column.</summary>
        /// <returns>true if the given column is in the collection; otherwise, false.</returns>
        /// <param name="gridColumn">The <see cref="T:MControl.GridView.GridColumn"></see> to look for.</param>
        /// <filterpriority>1</filterpriority>
        public virtual bool Contains(GridColumn gridColumn)
        {
            return (this.items.IndexOf(gridColumn) != -1);
        }

        /// <summary>Copies the items from the collection to the given array.</summary>
        /// <param name="array">The destination <see cref="T:MControl.GridView.GridColumn"></see> array.</param>
        /// <param name="index">The index of the destination array at which to start copying.</param>
        /// <filterpriority>1</filterpriority>
        public void CopyTo(GridColumn[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        internal bool DisplayInOrder(int columnIndex1, int columnIndex2)
        {
            int displayIndex = ((GridColumn) this.items[columnIndex1]).DisplayIndex;
            int num2 = ((GridColumn) this.items[columnIndex2]).DisplayIndex;
            return (displayIndex < num2);
        }

        internal GridColumn GetColumnAtDisplayIndex(int displayIndex)
        {
            if ((displayIndex >= 0) && (displayIndex < this.items.Count))
            {
                GridColumn column = (GridColumn) this.items[displayIndex];
                if (column.DisplayIndex == displayIndex)
                {
                    return column;
                }
                for (int i = 0; i < this.items.Count; i++)
                {
                    column = (GridColumn) this.items[i];
                    if (column.DisplayIndex == displayIndex)
                    {
                        return column;
                    }
                }
            }
            return null;
        }

        /// <summary>Returns the number of columns that meet the given filter requirements.</summary>
        /// <returns>The number of columns that meet the filter requirements.</returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter for inclusion.</param>
        /// <exception cref="T:System.ArgumentException">includeFilter is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public int GetColumnCount(GridElementStates includeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            GridElementStates states2 = includeFilter;
            if (states2 != GridElementStates.Visible)
            {
                if ((states2 == (GridElementStates.Visible | GridElementStates.Selected)) && (this.columnCountsVisibleSelected != -1))
                {
                    return this.columnCountsVisibleSelected;
                }
            }
            else if (this.columnCountsVisible != -1)
            {
                return this.columnCountsVisible;
            }
            int num = 0;
            if ((includeFilter & GridElementStates.Resizable) == GridElementStates.None)
            {
                for (int j = 0; j < this.items.Count; j++)
                {
                    if (((GridColumn) this.items[j]).StateIncludes(includeFilter))
                    {
                        num++;
                    }
                }
                GridElementStates states3 = includeFilter;
                if (states3 != GridElementStates.Visible)
                {
                    if (states3 != (GridElementStates.Visible | GridElementStates.Selected))
                    {
                        return num;
                    }
                }
                else
                {
                    this.columnCountsVisible = num;
                    return num;
                }
                this.columnCountsVisibleSelected = num;
                return num;
            }
            GridElementStates elementState = includeFilter & ~GridElementStates.Resizable;
            for (int i = 0; i < this.items.Count; i++)
            {
                if (((GridColumn) this.items[i]).StateIncludes(elementState) && (((GridColumn) this.items[i]).Resizable == GridTriState.True))
                {
                    num++;
                }
            }
            return num;
        }

        internal int GetColumnCount(GridElementStates includeFilter, int fromColumnIndex, int toColumnIndex)
        {
            int num = 0;
            GridColumn gridColumnStart = (GridColumn) this.items[fromColumnIndex];
            while (gridColumnStart != ((GridColumn) this.items[toColumnIndex]))
            {
                gridColumnStart = this.GetNextColumn(gridColumnStart, includeFilter, GridElementStates.None);
                if (gridColumnStart.StateIncludes(includeFilter))
                {
                    num++;
                }
            }
            return num;
        }

        internal float GetColumnsFillWeight(GridElementStates includeFilter)
        {
            float num = 0f;
            for (int i = 0; i < this.items.Count; i++)
            {
                if (((GridColumn) this.items[i]).StateIncludes(includeFilter))
                {
                    num += ((GridColumn) this.items[i]).FillWeight;
                }
            }
            return num;
        }

        private int GetColumnSortedIndex(GridColumn gridColumn)
        {
            if ((this.lastAccessedSortedIndex != -1) && (this.itemsSorted[this.lastAccessedSortedIndex] == gridColumn))
            {
                return this.lastAccessedSortedIndex;
            }
            for (int i = 0; i < this.itemsSorted.Count; i++)
            {
                if (gridColumn.Index == ((GridColumn) this.itemsSorted[i]).Index)
                {
                    this.lastAccessedSortedIndex = i;
                    return i;
                }
            }
            return -1;
        }

        /// <summary>Returns the width, in pixels, required to display all of the columns that meet the given filter requirements. </summary>
        /// <returns>The width, in pixels, that is necessary to display all of the columns that meet the filter requirements.</returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter for inclusion.</param>
        /// <exception cref="T:System.ArgumentException">includeFilter is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public int GetColumnsWidth(GridElementStates includeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            switch (includeFilter)
            {
                case GridElementStates.Visible:
                    if (this.columnsWidthVisible == -1)
                    {
                        break;
                    }
                    return this.columnsWidthVisible;

                case (GridElementStates.Visible | GridElementStates.Frozen):
                    if (this.columnsWidthVisibleFrozen == -1)
                    {
                        break;
                    }
                    return this.columnsWidthVisibleFrozen;
            }
            int num = 0;
            for (int i = 0; i < this.items.Count; i++)
            {
                if (((GridColumn) this.items[i]).StateIncludes(includeFilter))
                {
                    num += ((GridColumn) this.items[i]).Thickness;
                }
            }
            switch (includeFilter)
            {
                case GridElementStates.Visible:
                    this.columnsWidthVisible = num;
                    return num;

                case (GridElementStates.Visible | GridElementStates.Displayed):
                    return num;

                case (GridElementStates.Visible | GridElementStates.Frozen):
                    this.columnsWidthVisibleFrozen = num;
                    return num;
            }
            return num;
        }

        /// <summary>Returns the first column in display order that meets the given inclusion-filter requirements.</summary>
        /// <returns>The first column in display order that meets the given filter requirements, or null if no column is found.</returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represents the filter for inclusion.</param>
        /// <exception cref="T:System.ArgumentException">includeFilter is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public GridColumn GetFirstColumn(GridElementStates includeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if (this.itemsSorted == null)
            {
                this.UpdateColumnOrderCache();
            }
            for (int i = 0; i < this.itemsSorted.Count; i++)
            {
                GridColumn column = (GridColumn) this.itemsSorted[i];
                if (column.StateIncludes(includeFilter))
                {
                    this.lastAccessedSortedIndex = i;
                    return column;
                }
            }
            return null;
        }

        /// <summary>Returns the first column in display order that meets the given inclusion-filter and exclusion-filter requirements. </summary>
        /// <returns>The first column in display order that meets the given filter requirements, or null if no column is found.</returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter to apply for inclusion.</param>
        /// <param name="excludeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter to apply for exclusion.</param>
        /// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public GridColumn GetFirstColumn(GridElementStates includeFilter, GridElementStates excludeFilter)
        {
            if (excludeFilter == GridElementStates.None)
            {
                return this.GetFirstColumn(includeFilter);
            }
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if ((excludeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "excludeFilter" }));
            }
            if (this.itemsSorted == null)
            {
                this.UpdateColumnOrderCache();
            }
            for (int i = 0; i < this.itemsSorted.Count; i++)
            {
                GridColumn column = (GridColumn) this.itemsSorted[i];
                if (column.StateIncludes(includeFilter) && column.StateExcludes(excludeFilter))
                {
                    this.lastAccessedSortedIndex = i;
                    return column;
                }
            }
            return null;
        }

        /// <summary>Returns the last column in display order that meets the given filter requirements. </summary>
        /// <returns>The last displayed column in display order that meets the given filter requirements, or null if no column is found.</returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter to apply for inclusion.</param>
        /// <param name="excludeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter to apply for exclusion.</param>
        /// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public GridColumn GetLastColumn(GridElementStates includeFilter, GridElementStates excludeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if ((excludeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "excludeFilter" }));
            }
            if (this.itemsSorted == null)
            {
                this.UpdateColumnOrderCache();
            }
            for (int i = this.itemsSorted.Count - 1; i >= 0; i--)
            {
                GridColumn column = (GridColumn) this.itemsSorted[i];
                if (column.StateIncludes(includeFilter) && column.StateExcludes(excludeFilter))
                {
                    this.lastAccessedSortedIndex = i;
                    return column;
                }
            }
            return null;
        }

        /// <summary>Gets the first column after the given column in display order that meets the given filter requirements. </summary>
        /// <returns>The next column that meets the given filter requirements, or null if no column is found.</returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter to apply for inclusion.</param>
        /// <param name="gridColumnStart">The column from which to start searching for the next column.</param>
        /// <param name="excludeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter to apply for exclusion.</param>
        /// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        /// <exception cref="T:System.ArgumentNullException">gridColumnStart is null.</exception>
        public GridColumn GetNextColumn(GridColumn gridColumnStart, GridElementStates includeFilter, GridElementStates excludeFilter)
        {
            if (gridColumnStart == null)
            {
                throw new ArgumentNullException("gridColumnStart");
            }
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if ((excludeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "excludeFilter" }));
            }
            if (this.itemsSorted == null)
            {
                this.UpdateColumnOrderCache();
            }
            int columnSortedIndex = this.GetColumnSortedIndex(gridColumnStart);
            if (columnSortedIndex == -1)
            {
                bool flag = false;
                int num2 = 0x7fffffff;
                int displayIndex = 0x7fffffff;
                columnSortedIndex = 0;
                while (columnSortedIndex < this.items.Count)
                {
                    GridColumn column = (GridColumn) this.items[columnSortedIndex];
                    if (((column.StateIncludes(includeFilter) && column.StateExcludes(excludeFilter)) && ((column.DisplayIndex > gridColumnStart.DisplayIndex) || ((column.DisplayIndex == gridColumnStart.DisplayIndex) && (column.Index > gridColumnStart.Index)))) && ((column.DisplayIndex < displayIndex) || ((column.DisplayIndex == displayIndex) && (column.Index < num2))))
                    {
                        num2 = columnSortedIndex;
                        displayIndex = column.DisplayIndex;
                        flag = true;
                    }
                    columnSortedIndex++;
                }
                if (!flag)
                {
                    return null;
                }
                return (GridColumn) this.items[num2];
            }
            columnSortedIndex++;
            while (columnSortedIndex < this.itemsSorted.Count)
            {
                GridColumn column2 = (GridColumn) this.itemsSorted[columnSortedIndex];
                if (column2.StateIncludes(includeFilter) && column2.StateExcludes(excludeFilter))
                {
                    this.lastAccessedSortedIndex = columnSortedIndex;
                    return column2;
                }
                columnSortedIndex++;
            }
            return null;
        }

        /// <summary>Gets the last column prior to the given column in display order that meets the given filter requirements. </summary>
        /// <returns>The previous column that meets the given filter requirements, or null if no column is found.</returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter to apply for inclusion.</param>
        /// <param name="gridColumnStart">The column from which to start searching for the previous column.</param>
        /// <param name="excludeFilter">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that represent the filter to apply for exclusion.</param>
        /// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        /// <exception cref="T:System.ArgumentNullException">gridColumnStart is null.</exception>
        public GridColumn GetPreviousColumn(GridColumn gridColumnStart, GridElementStates includeFilter, GridElementStates excludeFilter)
        {
            if (gridColumnStart == null)
            {
                throw new ArgumentNullException("gridColumnStart");
            }
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if ((excludeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "excludeFilter" }));
            }
            if (this.itemsSorted == null)
            {
                this.UpdateColumnOrderCache();
            }
            int columnSortedIndex = this.GetColumnSortedIndex(gridColumnStart);
            if (columnSortedIndex == -1)
            {
                bool flag = false;
                int num2 = -1;
                int displayIndex = -1;
                columnSortedIndex = 0;
                while (columnSortedIndex < this.items.Count)
                {
                    GridColumn column = (GridColumn) this.items[columnSortedIndex];
                    if (((column.StateIncludes(includeFilter) && column.StateExcludes(excludeFilter)) && ((column.DisplayIndex < gridColumnStart.DisplayIndex) || ((column.DisplayIndex == gridColumnStart.DisplayIndex) && (column.Index < gridColumnStart.Index)))) && ((column.DisplayIndex > displayIndex) || ((column.DisplayIndex == displayIndex) && (column.Index > num2))))
                    {
                        num2 = columnSortedIndex;
                        displayIndex = column.DisplayIndex;
                        flag = true;
                    }
                    columnSortedIndex++;
                }
                if (!flag)
                {
                    return null;
                }
                return (GridColumn) this.items[num2];
            }
            columnSortedIndex--;
            while (columnSortedIndex >= 0)
            {
                GridColumn column2 = (GridColumn) this.itemsSorted[columnSortedIndex];
                if (column2.StateIncludes(includeFilter) && column2.StateExcludes(excludeFilter))
                {
                    this.lastAccessedSortedIndex = columnSortedIndex;
                    return column2;
                }
                columnSortedIndex--;
            }
            return null;
        }

        /// <summary>Gets the index of the given <see cref="T:MControl.GridView.GridColumn"></see> in the collection.</summary>
        /// <returns>The index of the given <see cref="T:MControl.GridView.GridColumn"></see>.</returns>
        /// <param name="gridColumn">The <see cref="T:MControl.GridView.GridColumn"></see> to return the index of.</param>
        /// <filterpriority>1</filterpriority>
        public int IndexOf(GridColumn gridColumn)
        {
            return this.items.IndexOf(gridColumn);
        }

        /// <summary>Inserts a column at the given index in the collection.</summary>
        /// <param name="columnIndex">The zero-based index at which to insert the given column.</param>
        /// <param name="gridColumn">The <see cref="T:MControl.GridView.GridColumn"></see> to insert.</param>
        /// <exception cref="T:System.ArgumentNullException">gridColumn is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> property values. -or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-gridColumn already belongs to a <see cref="T:MControl.GridView.Grid"></see> control.-or-The gridColumn<see cref="P:MControl.GridView.GridColumn.SortMode"></see> property value is <see cref="F:MControl.GridView.GridColumnSortMode.Automatic"></see> and the <see cref="P:MControl.GridView.Grid.SelectionMode"></see> property value is <see cref="F:MControl.GridView.GridSelectionMode.FullColumnSelect"></see> or <see cref="F:MControl.GridView.GridSelectionMode.ColumnHeaderSelect"></see>. Use the control <see cref="M:MControl.GridView.Grid.System.ComponentModel.ISupportInitialize.BeginInit"></see> and <see cref="M:MControl.GridView.Grid.System.ComponentModel.ISupportInitialize.EndInit"></see> methods to temporarily set conflicting property values. -or-The gridColumn<see cref="P:MControl.GridView.GridColumn.InheritedAutoSizeMode"></see> property value is <see cref="F:MControl.GridView.GridAutoSizeColumnMode.ColumnHeader"></see> and the <see cref="P:MControl.GridView.Grid.ColumnHeadersVisible"></see> property value is false.-or-gridColumn has an <see cref="P:MControl.GridView.GridColumn.InheritedAutoSizeMode"></see> property value of <see cref="F:MControl.GridView.GridAutoSizeColumnMode.Fill"></see> and a <see cref="P:MControl.GridView.GridColumn.Frozen"></see> property value of true.-or-gridColumn has <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> and <see cref="P:MControl.GridView.GridColumn.Frozen"></see> property values that would display it among a set of adjacent columns with the opposite <see cref="P:MControl.GridView.GridColumn.Frozen"></see> property value.-or-The <see cref="T:MControl.GridView.Grid"></see> control contains at least one row and gridColumn has a <see cref="P:MControl.GridView.GridColumn.CellType"></see> property value of null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Insert(int columnIndex, GridColumn gridColumn)
        {
            Point point;
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            if (this.Grid.InDisplayIndexAdjustments)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_CannotAlterDisplayIndexWithinAdjustments"));
            }
            if (gridColumn == null)
            {
                throw new ArgumentNullException("gridColumn");
            }
            int displayIndex = gridColumn.DisplayIndex;
            if (displayIndex == -1)
            {
                gridColumn.DisplayIndex = columnIndex;
            }
            try
            {
                this.Grid.OnInsertingColumn(columnIndex, gridColumn, out point);
            }
            finally
            {
                gridColumn.DisplayIndexInternal = displayIndex;
            }
            this.InvalidateCachedColumnsOrder();
            this.items.Insert(columnIndex, gridColumn);
            gridColumn.IndexInternal = columnIndex;
            gridColumn.GridInternal = this.grid;
            this.UpdateColumnCaches(gridColumn, true);
            this.Grid.OnInsertedColumn_PreNotification(gridColumn);
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, gridColumn), true, point);
        }

        internal void InvalidateCachedColumnCount(GridElementStates includeFilter)
        {
            if (includeFilter == GridElementStates.Visible)
            {
                this.InvalidateCachedColumnCounts();
            }
            else if (includeFilter == GridElementStates.Selected)
            {
                this.columnCountsVisibleSelected = -1;
            }
        }

        internal void InvalidateCachedColumnCounts()
        {
            this.columnCountsVisible = this.columnCountsVisibleSelected = -1;
        }

        internal void InvalidateCachedColumnsOrder()
        {
            this.itemsSorted = null;
        }

        internal void InvalidateCachedColumnsWidth(GridElementStates includeFilter)
        {
            if (includeFilter == GridElementStates.Visible)
            {
                this.InvalidateCachedColumnsWidths();
            }
            else if (includeFilter == GridElementStates.Frozen)
            {
                this.columnsWidthVisibleFrozen = -1;
            }
        }

        internal void InvalidateCachedColumnsWidths()
        {
            this.columnsWidthVisible = this.columnsWidthVisibleFrozen = -1;
        }

        /// <summary>Raises the <see cref="E:MControl.GridView.GridColumnCollection.CollectionChanged"></see> event.</summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs"></see> that contains the event data.</param>
        protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (this.onCollectionChanged != null)
            {
                this.onCollectionChanged(this, e);
            }
        }

        private void OnCollectionChanged(CollectionChangeEventArgs ccea, bool changeIsInsertion, Point newCurrentCell)
        {
            this.OnCollectionChanged_PreNotification(ccea);
            this.OnCollectionChanged(ccea);
            this.OnCollectionChanged_PostNotification(ccea, changeIsInsertion, newCurrentCell);
        }

        private void OnCollectionChanged_PostNotification(CollectionChangeEventArgs ccea, bool changeIsInsertion, Point newCurrentCell)
        {
            GridColumn element = (GridColumn) ccea.Element;
            if ((ccea.Action == CollectionChangeAction.Add) && changeIsInsertion)
            {
                this.Grid.OnInsertedColumn_PostNotification(newCurrentCell);
            }
            else if (ccea.Action == CollectionChangeAction.Remove)
            {
                this.Grid.OnRemovedColumn_PostNotification(element, newCurrentCell);
            }
            this.Grid.OnColumnCollectionChanged_PostNotification(element);
        }

        private void OnCollectionChanged_PreNotification(CollectionChangeEventArgs ccea)
        {
            this.Grid.OnColumnCollectionChanged_PreNotification(ccea);
        }

        /// <summary>Removes the column with the specified name from the collection.</summary>
        /// <param name="columnName">The name of the column to delete.</param>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> property values. -or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see></exception>
        /// <exception cref="T:System.ArgumentException">columnName does not match the name of any column in the collection.</exception>
        /// <exception cref="T:System.ArgumentNullException">columnName is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Remove(string columnName)
        {
            if (columnName == null)
            {
                throw new ArgumentNullException("columnName");
            }
            int count = this.items.Count;
            for (int i = 0; i < count; i++)
            {
                GridColumn column = (GridColumn) this.items[i];
                if (string.Compare(column.Name, columnName, true, CultureInfo.InvariantCulture) == 0)
                {
                    this.RemoveAt(i);
                    return;
                }
            }
            throw new ArgumentException(MControl.GridView.RM.GetString("GridColumnCollection_ColumnNotFound", new object[] { columnName }), "columnName");
        }

        /// <summary>Removes the specified column from the collection.</summary>
        /// <param name="gridColumn">The column to delete.</param>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> property values. -or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see></exception>
        /// <exception cref="T:System.ArgumentNullException">gridColumn is null.</exception>
        /// <exception cref="T:System.ArgumentException">gridColumn is not in the collection.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Remove(GridColumn gridColumn)
        {
            if (gridColumn == null)
            {
                throw new ArgumentNullException("gridColumn");
            }
            if (gridColumn.Grid != this.Grid)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_ColumnDoesNotBelongToGrid"), "gridColumn");
            }
            int count = this.items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.items[i] == gridColumn)
                {
                    this.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>Removes the column at the given index in the collection.</summary>
        /// <param name="index">The index of the column to delete.</param>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:MControl.GridView.GridColumn.DisplayIndex"></see> property values. -or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see></exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the number of columns in the control minus one. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index", MControl.GridView.RM.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            if (this.Grid.InDisplayIndexAdjustments)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_CannotAlterDisplayIndexWithinAdjustments"));
            }
            this.RemoveAtInternal(index, false);
        }

        internal void RemoveAtInternal(int index, bool force)
        {
            Point point;
            GridColumn gridColumn = (GridColumn) this.items[index];
            this.Grid.OnRemovingColumn(gridColumn, out point, force);
            this.InvalidateCachedColumnsOrder();
            this.items.RemoveAt(index);
            gridColumn.GridInternal = null;
            this.UpdateColumnCaches(gridColumn, false);
            this.Grid.OnRemovedColumn_PreNotification(gridColumn);
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, gridColumn), false, point);
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
            return this.Add((GridColumn) value);
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
            this.Insert(index, (GridColumn) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((GridColumn) value);
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        private void UpdateColumnCaches(GridColumn gridColumn, bool adding)
        {
            if (((this.columnCountsVisible != -1) || (this.columnCountsVisibleSelected != -1)) || ((this.columnsWidthVisible != -1) || (this.columnsWidthVisibleFrozen != -1)))
            {
                GridElementStates state = gridColumn.State;
                if ((state & GridElementStates.Visible) != GridElementStates.None)
                {
                    int num = adding ? 1 : -1;
                    int num2 = 0;
                    if ((this.columnsWidthVisible != -1) || ((this.columnsWidthVisibleFrozen != -1) && ((state & (GridElementStates.Visible | GridElementStates.Frozen)) == (GridElementStates.Visible | GridElementStates.Frozen))))
                    {
                        num2 = adding ? gridColumn.Width : -gridColumn.Width;
                    }
                    if (this.columnCountsVisible != -1)
                    {
                        this.columnCountsVisible += num;
                    }
                    if (this.columnsWidthVisible != -1)
                    {
                        this.columnsWidthVisible += num2;
                    }
                    if (((state & (GridElementStates.Visible | GridElementStates.Frozen)) == (GridElementStates.Visible | GridElementStates.Frozen)) && (this.columnsWidthVisibleFrozen != -1))
                    {
                        this.columnsWidthVisibleFrozen += num2;
                    }
                    if (((state & (GridElementStates.Visible | GridElementStates.Selected)) == (GridElementStates.Visible | GridElementStates.Selected)) && (this.columnCountsVisibleSelected != -1))
                    {
                        this.columnCountsVisibleSelected += num;
                    }
                }
            }
        }

        private void UpdateColumnOrderCache()
        {
            this.itemsSorted = (ArrayList) this.items.Clone();
            this.itemsSorted.Sort(columnOrderComparer);
            this.lastAccessedSortedIndex = -1;
        }

        internal static IComparer ColumnCollectionOrderComparer
        {
            get
            {
                return columnOrderComparer;
            }
        }

        /// <summary>Gets the <see cref="T:MControl.GridView.Grid"></see> upon which the collection performs column-related operations.</summary>
        /// <returns><see cref="T:MControl.GridView.Grid"></see>.</returns>
        protected MControl.GridView.Grid Grid
        {
            get
            {
                return this.grid;
            }
        }

        /// <summary>Gets or sets the column of the given name in the collection. </summary>
        /// <returns>The <see cref="T:MControl.GridView.GridColumn"></see> identified by the columnName parameter.</returns>
        /// <param name="columnName">The name of the column to get or set.</param>
        /// <exception cref="T:System.ArgumentNullException">columnName is null.</exception>
        /// <filterpriority>1</filterpriority>
        public GridColumn this[string columnName]
        {
            get
            {
                if (columnName == null)
                {
                    throw new ArgumentNullException("columnName");
                }
                int count = this.items.Count;
                for (int i = 0; i < count; i++)
                {
                    GridColumn column = (GridColumn) this.items[i];
                    if (string.Equals(column.Name, columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        return column;
                    }
                }
                return null;
            }
        }

        /// <summary>Gets or sets the column at the given index in the collection. </summary>
        /// <returns>The <see cref="T:MControl.GridView.GridColumn"></see> at the given index.</returns>
        /// <param name="index">The zero-based index of the column to get or set.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the number of columns in the collection minus one.</exception>
        /// <filterpriority>1</filterpriority>
        public GridColumn this[int index]
        {
            get
            {
                return (GridColumn) this.items[index];
            }
        }

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
                throw new NotSupportedException();
            }
        }

        private class ColumnOrderComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                GridColumn column = x as GridColumn;
                GridColumn column2 = y as GridColumn;
                return (column.DisplayIndex - column2.DisplayIndex);
            }
        }
    }
}

