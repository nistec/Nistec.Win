namespace MControl.GridView
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Globalization;
    using System.Reflection;

    /// <summary>A collection of <see cref="T:MControl.GridView.GridRow"></see> objects.</summary>
    /// <filterpriority>2</filterpriority>
    [ListBindable(false), DesignerSerializer("System.Windows.Forms.Design.GridRowCollectionCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class GridRowCollection : IList, ICollection, IEnumerable
    {
        private MControl.GridView.Grid grid;
        private RowArrayList items;
        private int rowCountsVisible;
        private int rowCountsVisibleFrozen;
        private int rowCountsVisibleSelected;
        private int rowsHeightVisible;
        private int rowsHeightVisibleFrozen;
        private List<GridElementStates> rowStates;

        /// <summary>Occurs when the contents of the collection change.</summary>
        /// <filterpriority>1</filterpriority>
        public event CollectionChangeEventHandler CollectionChanged;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowCollection"></see> class. </summary>
        /// <param name="grid">The <see cref="T:MControl.GridView.Grid"></see> that owns the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        public GridRowCollection(MControl.GridView.Grid grid)
        {
            this.InvalidateCachedRowCounts();
            this.InvalidateCachedRowsHeights();
            this.grid = grid;
            this.rowStates = new List<GridElementStates>();
            this.items = new RowArrayList(this);
        }

        /// <summary>Adds a new row to the collection.</summary>
        /// <returns>The index of the new row.</returns>
        /// <exception cref="T:System.ArgumentException">The row returned by the <see cref="P:MControl.GridView.Grid.RowTemplate"></see> property has more cells than there are columns in the control.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-The <see cref="T:MControl.GridView.Grid"></see> has no columns.-or-This operation would add a frozen row after unfrozen rows.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Add()
        {
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            return this.AddInternal(false, null);
        }

        /// <summary>Adds the specified number of new rows to the collection.</summary>
        /// <returns>The index of the last row that was added.</returns>
        /// <param name="count">The number of rows to add to the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">count is less than 1.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-The <see cref="T:MControl.GridView.Grid"></see> has no columns.-or-The row returned by the <see cref="P:MControl.GridView.Grid.RowTemplate"></see> property has more cells than there are columns in the control. -or-This operation would add frozen rows after unfrozen rows.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Add(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count", MControl.GridView.RM.GetString("GridRowCollection_CountOutOfRange"));
            }
            if (this.Grid.Columns.Count == 0)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoColumns"));
            }
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            if (this.Grid.RowTemplate.Cells.Count > this.Grid.Columns.Count)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_RowTemplateTooManyCells"));
            }
            GridRow rowTemplateClone = this.Grid.RowTemplateClone;
            GridElementStates state = rowTemplateClone.State;
            rowTemplateClone.GridInternal = this.grid;
            int num = 0;
            foreach (GridCell cell in rowTemplateClone.Cells)
            {
                cell.GridInternal = this.grid;
                cell.OwningColumnInternal = this.Grid.Columns[num];
                num++;
            }
            if (rowTemplateClone.HasHeaderCell)
            {
                rowTemplateClone.HeaderCell.GridInternal = this.grid;
                rowTemplateClone.HeaderCell.OwningRowInternal = rowTemplateClone;
            }
            if (this.Grid.NewRowIndex != -1)
            {
                int indexDestination = this.Count - 1;
                this.InsertCopiesPrivate(rowTemplateClone, state, indexDestination, count);
                return ((indexDestination + count) - 1);
            }
            return this.AddCopiesPrivate(rowTemplateClone, state, count);
        }

        /// <summary>Adds a new row to the collection, and populates the cells with the specified objects.</summary>
        /// <returns>The index of the new row.</returns>
        /// <param name="values">A variable number of objects that populate the cells of the new <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.VirtualMode"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is set to true.- or -The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-The <see cref="T:MControl.GridView.Grid"></see> has no columns. -or-The row returned by the <see cref="P:MControl.GridView.Grid.RowTemplate"></see> property has more cells than there are columns in the control.-or-This operation would add a frozen row after unfrozen rows.</exception>
        /// <exception cref="T:System.ArgumentNullException">values is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Add(params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (this.Grid.VirtualMode)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationInVirtualMode"));
            }
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            return this.AddInternal(false, values);
        }

        /// <summary>Adds the specified <see cref="T:MControl.GridView.GridRow"></see> to the collection.</summary>
        /// <returns>The index of the new <see cref="T:MControl.GridView.GridRow"></see>.</returns>
        /// <param name="gridRow">The <see cref="T:MControl.GridView.GridRow"></see> to add to the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <exception cref="T:System.ArgumentException">gridRow has more cells than there are columns in the control.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-The <see cref="T:MControl.GridView.Grid"></see> has no columns.-or-The <see cref="P:MControl.GridView.GridElement.Grid"></see> property of the gridRow is not null.-or-gridRow has a <see cref="P:MControl.GridView.GridRow.Selected"></see> property value of true. -or-This operation would add a frozen row after unfrozen rows.</exception>
        /// <exception cref="T:System.ArgumentNullException">gridRow is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual int Add(GridRow gridRow)
        {
            if (this.Grid.Columns.Count == 0)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoColumns"));
            }
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            return this.AddInternal(gridRow);
        }

        /// <summary>Adds the specified number of rows to the collection based on the row at the specified index.</summary>
        /// <returns>The index of the last row that was added.</returns>
        /// <param name="count">The number of rows to add to the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <param name="indexSource">The index of the row on which to base the new rows.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">indexSource is less than zero or greater than or equal to the number of rows in the control.-or-count is less than zero.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-This operation would add a frozen row after unfrozen rows.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual int AddCopies(int indexSource, int count)
        {
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            return this.AddCopiesInternal(indexSource, count);
        }

        internal int AddCopiesInternal(int indexSource, int count)
        {
            if (this.Grid.NewRowIndex != -1)
            {
                int indexDestination = this.Count - 1;
                this.InsertCopiesPrivate(indexSource, indexDestination, count);
                return ((indexDestination + count) - 1);
            }
            return this.AddCopiesInternal(indexSource, count, GridElementStates.None, GridElementStates.Selected | GridElementStates.Displayed);
        }

        internal int AddCopiesInternal(int indexSource, int count, GridElementStates dgvesAdd, GridElementStates dgvesRemove)
        {
            if ((indexSource < 0) || (this.Count <= indexSource))
            {
                throw new ArgumentOutOfRangeException("indexSource", MControl.GridView.RM.GetString("GridRowCollection_IndexSourceOutOfRange"));
            }
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count", MControl.GridView.RM.GetString("GridRowCollection_CountOutOfRange"));
            }
            GridElementStates rowTemplateState = ((GridElementStates) this.rowStates[indexSource]) & ~dgvesRemove;
            rowTemplateState |= dgvesAdd;
            return this.AddCopiesPrivate(this.SharedRow(indexSource), rowTemplateState, count);
        }

        private int AddCopiesPrivate(GridRow rowTemplate, GridElementStates rowTemplateState, int count)
        {
            int num;
            int rowIndex = this.items.Count;
            if (rowTemplate.Index == -1)
            {
                this.Grid.OnAddingRow(rowTemplate, rowTemplateState, true);
                for (int i = 0; i < (count - 1); i++)
                {
                    this.SharedList.Add(rowTemplate);
                    this.rowStates.Add(rowTemplateState);
                }
                num = this.SharedList.Add(rowTemplate);
                this.rowStates.Add(rowTemplateState);
                this.Grid.OnAddedRow_PreNotification(num);
                this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), rowIndex, count);
                for (int j = 0; j < count; j++)
                {
                    this.Grid.OnAddedRow_PostNotification((num - (count - 1)) + j);
                }
                return num;
            }
            num = this.AddDuplicateRow(rowTemplate, false);
            if (count > 1)
            {
                this.Grid.OnAddedRow_PreNotification(num);
                if (this.RowIsSharable(num))
                {
                    GridRow gridRow = this.SharedRow(num);
                    this.Grid.OnAddingRow(gridRow, rowTemplateState, true);
                    for (int m = 1; m < (count - 1); m++)
                    {
                        this.SharedList.Add(gridRow);
                        this.rowStates.Add(rowTemplateState);
                    }
                    num = this.SharedList.Add(gridRow);
                    this.rowStates.Add(rowTemplateState);
                    this.Grid.OnAddedRow_PreNotification(num);
                }
                else
                {
                    this.UnshareRow(num);
                    for (int n = 1; n < count; n++)
                    {
                        num = this.AddDuplicateRow(rowTemplate, false);
                        this.UnshareRow(num);
                        this.Grid.OnAddedRow_PreNotification(num);
                    }
                }
                this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), rowIndex, count);
                for (int k = 0; k < count; k++)
                {
                    this.Grid.OnAddedRow_PostNotification((num - (count - 1)) + k);
                }
                return num;
            }
            if (this.IsCollectionChangedListenedTo)
            {
                this.UnshareRow(num);
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(num)), num, 1);
            return num;
        }

        /// <summary>Adds a new row based on the row at the specified index.</summary>
        /// <returns>The index of the new row.</returns>
        /// <param name="indexSource">The index of the row on which to base the new row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">indexSource is less than zero or greater than or equal to the number of rows in the collection.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-This operation would add a frozen row after unfrozen rows.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual int AddCopy(int indexSource)
        {
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            return this.AddCopyInternal(indexSource, GridElementStates.None, GridElementStates.Selected | GridElementStates.Displayed, false);
        }

        internal int AddCopyInternal(int indexSource, GridElementStates dgvesAdd, GridElementStates dgvesRemove, bool newRow)
        {
            int num2;
            if (this.Grid.NewRowIndex != -1)
            {
                int indexDestination = this.Count - 1;
                this.InsertCopy(indexSource, indexDestination);
                return indexDestination;
            }
            if ((indexSource < 0) || (indexSource >= this.Count))
            {
                throw new ArgumentOutOfRangeException("indexSource", MControl.GridView.RM.GetString("GridRowCollection_IndexSourceOutOfRange"));
            }
            GridRow gridRow = this.SharedRow(indexSource);
            if (((gridRow.Index == -1) && !this.IsCollectionChangedListenedTo) && !newRow)
            {
                GridElementStates rowState = ((GridElementStates) this.rowStates[indexSource]) & ~dgvesRemove;
                rowState |= dgvesAdd;
                this.Grid.OnAddingRow(gridRow, rowState, true);
                num2 = this.SharedList.Add(gridRow);
                this.rowStates.Add(rowState);
                this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, gridRow), num2, 1);
                return num2;
            }
            num2 = this.AddDuplicateRow(gridRow, newRow);
            if ((!this.RowIsSharable(num2) || RowHasValueOrToolTipText(this.SharedRow(num2))) || this.IsCollectionChangedListenedTo)
            {
                this.UnshareRow(num2);
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(num2)), num2, 1);
            return num2;
        }

        private int AddDuplicateRow(GridRow rowTemplate, bool newRow)
        {
            GridRow gridRow = (GridRow) rowTemplate.Clone();
            gridRow.StateInternal = GridElementStates.None;
            gridRow.GridInternal = this.grid;
            GridCellCollection cells = gridRow.Cells;
            int num = 0;
            foreach (GridCell cell in cells)
            {
                if (newRow)
                {
                    cell.Value = cell.DefaultNewRowValue;
                }
                cell.GridInternal = this.grid;
                cell.OwningColumnInternal = this.Grid.Columns[num];
                num++;
            }
            GridElementStates rowState = rowTemplate.State & ~(GridElementStates.Selected | GridElementStates.Displayed);
            if (gridRow.HasHeaderCell)
            {
                gridRow.HeaderCell.GridInternal = this.grid;
                gridRow.HeaderCell.OwningRowInternal = gridRow;
            }
            this.Grid.OnAddingRow(gridRow, rowState, true);
            this.rowStates.Add(rowState);
            return this.SharedList.Add(gridRow);
        }

        internal int AddInternal(GridRow gridRow)
        {
            if (gridRow == null)
            {
                throw new ArgumentNullException("gridRow");
            }
            if (gridRow.Grid != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_RowAlreadyBelongsToGrid"));
            }
            if (this.Grid.Columns.Count == 0)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoColumns"));
            }
            if (gridRow.Cells.Count > this.Grid.Columns.Count)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("GridRowCollection_TooManyCells"), "gridRow");
            }
            if (gridRow.Selected)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_CannotAddOrInsertSelectedRow"));
            }
            if (this.Grid.NewRowIndex != -1)
            {
                int rowIndex = this.Count - 1;
                this.InsertInternal(rowIndex, gridRow);
                return rowIndex;
            }
            this.Grid.CompleteCellsCollection(gridRow);
            this.Grid.OnAddingRow(gridRow, gridRow.State, true);
            int num2 = 0;
            foreach (GridCell cell in gridRow.Cells)
            {
                cell.GridInternal = this.grid;
                if (cell.ColumnIndex == -1)
                {
                    cell.OwningColumnInternal = this.Grid.Columns[num2];
                }
                num2++;
            }
            if (gridRow.HasHeaderCell)
            {
                gridRow.HeaderCell.GridInternal = this.Grid;
                gridRow.HeaderCell.OwningRowInternal = gridRow;
            }
            int index = this.SharedList.Add(gridRow);
            this.rowStates.Add(gridRow.State);
            gridRow.GridInternal = this.grid;
            if ((!this.RowIsSharable(index) || RowHasValueOrToolTipText(gridRow)) || this.IsCollectionChangedListenedTo)
            {
                gridRow.IndexInternal = index;
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, gridRow), index, 1);
            return index;
        }

        internal int AddInternal(bool newRow, object[] values)
        {
            if (this.Grid.Columns.Count == 0)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoColumns"));
            }
            if (this.Grid.RowTemplate.Cells.Count > this.Grid.Columns.Count)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_RowTemplateTooManyCells"));
            }
            GridRow rowTemplateClone = this.Grid.RowTemplateClone;
            if (newRow)
            {
                rowTemplateClone.StateInternal = rowTemplateClone.State | GridElementStates.Visible;
                foreach (GridCell cell in rowTemplateClone.Cells)
                {
                    cell.Value = cell.DefaultNewRowValue;
                }
            }
            if (values != null)
            {
                rowTemplateClone.SetValuesInternal(values);
            }
            if (this.Grid.NewRowIndex != -1)
            {
                int rowIndex = this.Count - 1;
                this.Insert(rowIndex, rowTemplateClone);
                return rowIndex;
            }
            GridElementStates state = rowTemplateClone.State;
            this.Grid.OnAddingRow(rowTemplateClone, state, true);
            rowTemplateClone.GridInternal = this.grid;
            int num2 = 0;
            foreach (GridCell cell2 in rowTemplateClone.Cells)
            {
                cell2.GridInternal = this.grid;
                cell2.OwningColumnInternal = this.Grid.Columns[num2];
                num2++;
            }
            int index = this.SharedList.Add(rowTemplateClone);
            this.rowStates.Add(state);
            if (((values != null) || !this.RowIsSharable(index)) || (RowHasValueOrToolTipText(rowTemplateClone) || this.IsCollectionChangedListenedTo))
            {
                rowTemplateClone.IndexInternal = index;
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, rowTemplateClone), index, 1);
            return index;
        }

        /// <summary>Adds the specified <see cref="T:MControl.GridView.GridRow"></see> objects to the collection.</summary>
        /// <param name="gridRows">An array of <see cref="T:MControl.GridView.GridRow"></see> objects to be added to the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-At least one entry in the gridRows array is null.-or-The <see cref="T:MControl.GridView.Grid"></see> has no columns.-or-At least one row in the gridRows array has a <see cref="P:MControl.GridView.GridElement.Grid"></see> property value that is not null.-or-At least one row in the gridRows array has a <see cref="P:MControl.GridView.GridRow.Selected"></see> property value of true.-or-Two or more rows in the gridRows array are identical.-or-At least one row in the gridRows array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.-or-At least one row in the gridRows array contains more cells than there are columns in the control.-or-This operation would add frozen rows after unfrozen rows.</exception>
        /// <exception cref="T:System.ArgumentException">gridRows contains only one row, and the row it contains has more cells than there are columns in the control.</exception>
        /// <exception cref="T:System.ArgumentNullException">gridRows is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual void AddRange(params GridRow[] gridRows)
        {
            if (gridRows == null)
            {
                throw new ArgumentNullException("gridRows");
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if (this.Grid.NewRowIndex != -1)
            {
                this.InsertRange(this.Count - 1, gridRows);
            }
            else
            {
                if (this.Grid.Columns.Count == 0)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoColumns"));
                }
                int count = this.items.Count;
                this.Grid.OnAddingRows(gridRows, true);
                foreach (GridRow row in gridRows)
                {
                    int num2 = 0;
                    foreach (GridCell cell in row.Cells)
                    {
                        cell.GridInternal = this.grid;
                        cell.OwningColumnInternal = this.Grid.Columns[num2];
                        num2++;
                    }
                    if (row.HasHeaderCell)
                    {
                        row.HeaderCell.GridInternal = this.grid;
                        row.HeaderCell.OwningRowInternal = row;
                    }
                    int num3 = this.SharedList.Add(row);
                    this.rowStates.Add(row.State);
                    row.IndexInternal = num3;
                    row.GridInternal = this.grid;
                }
                this.Grid.OnAddedRows_PreNotification(gridRows);
                this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), count, gridRows.Length);
                this.Grid.OnAddedRows_PostNotification(gridRows);
            }
        }

        /// <summary>Clears the collection. </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection is data bound and the underlying data source does not support clearing the row data.-or-The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see></exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Clear()
        {
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            if (this.Grid.DataSource != null)
            {
                IBindingList list = this.Grid.DataConnection.List as IBindingList;
                if (((list == null) || !list.AllowRemove) || !list.SupportsChangeNotification)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_CantClearRowCollectionWithWrongSource"));
                }
                list.Clear();
            }
            else
            {
                this.ClearInternal(true);
            }
        }

        internal void ClearInternal(bool recreateNewRow)
        {
            int count = this.items.Count;
            if (count > 0)
            {
                this.Grid.OnClearingRows();
                for (int i = 0; i < count; i++)
                {
                    this.SharedRow(i).DetachFromGrid();
                }
                this.SharedList.Clear();
                this.rowStates.Clear();
                this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), 0, count, true, false, recreateNewRow, new Point(-1, -1));
            }
            else if ((recreateNewRow && (this.Grid.Columns.Count != 0)) && (this.Grid.AllowUserToAddRowsInternal && (this.items.Count == 0)))
            {
                this.Grid.AddNewRow(false);
            }
        }

        /// <summary>Determines whether the specified <see cref="T:MControl.GridView.GridRow"></see> is in the collection.</summary>
        /// <returns>true if the <see cref="T:MControl.GridView.GridRow"></see> is in the <see cref="T:MControl.GridView.GridRowCollection"></see>; otherwise, false.</returns>
        /// <param name="gridRow">The <see cref="T:MControl.GridView.GridRow"></see> to locate in the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <filterpriority>1</filterpriority>
        public virtual bool Contains(GridRow gridRow)
        {
            return (this.items.IndexOf(gridRow) != -1);
        }

        /// <summary>Copies the items from the collection into the specified <see cref="T:MControl.GridView.GridRow"></see> array, starting at the specified index.</summary>
        /// <param name="array">A <see cref="T:MControl.GridView.GridRow"></see> array that is the destination of the items copied from the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentException">array is multidimensional.-or- index is equal to or greater than the length of array.-or- The number of elements in the <see cref="T:MControl.GridView.GridRowCollection"></see> is greater than the available space from index to the end of array. </exception>
        /// <exception cref="T:System.ArgumentNullException">array is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero. </exception>
        /// <filterpriority>1</filterpriority>
        public void CopyTo(GridRow[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        internal int DisplayIndexToRowIndex(int visibleRowIndex)
        {
            int num = -1;
            for (int i = 0; i < this.Count; i++)
            {
                if ((this.GetRowState(i) & GridElementStates.Visible) == GridElementStates.Visible)
                {
                    num++;
                }
                if (num == visibleRowIndex)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>Returns the index of the first <see cref="T:MControl.GridView.GridRow"></see> that meets the specified criteria.</summary>
        /// <returns>The index of the first <see cref="T:MControl.GridView.GridRow"></see> that has the attributes specified by includeFilter; -1 if no row is found.</returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <exception cref="T:System.ArgumentException">includeFilter is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public int GetFirstRow(GridElementStates includeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            switch (includeFilter)
            {
                case GridElementStates.Visible:
                    if (this.rowCountsVisible != 0)
                    {
                        break;
                    }
                    return -1;

                case (GridElementStates.Visible | GridElementStates.Frozen):
                    if (this.rowCountsVisibleFrozen != 0)
                    {
                        break;
                    }
                    return -1;

                case (GridElementStates.Visible | GridElementStates.Selected):
                    if (this.rowCountsVisibleSelected == 0)
                    {
                        return -1;
                    }
                    break;
            }
            int rowIndex = 0;
            while ((rowIndex < this.items.Count) && ((this.GetRowState(rowIndex) & includeFilter) != includeFilter))
            {
                rowIndex++;
            }
            if (rowIndex >= this.items.Count)
            {
                return -1;
            }
            return rowIndex;
        }

        /// <summary>Returns the index of the first <see cref="T:MControl.GridView.GridRow"></see> that meets the specified inclusion and exclusion criteria.</summary>
        /// <returns>The index of the first <see cref="T:MControl.GridView.GridRow"></see> that has the attributes specified by includeFilter, and does not have the attributes specified by excludeFilter; -1 if no row is found.</returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <param name="excludeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public int GetFirstRow(GridElementStates includeFilter, GridElementStates excludeFilter)
        {
            if (excludeFilter == GridElementStates.None)
            {
                return this.GetFirstRow(includeFilter);
            }
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if ((excludeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "excludeFilter" }));
            }
            switch (includeFilter)
            {
                case GridElementStates.Visible:
                    if (this.rowCountsVisible != 0)
                    {
                        break;
                    }
                    return -1;

                case (GridElementStates.Visible | GridElementStates.Frozen):
                    if (this.rowCountsVisibleFrozen != 0)
                    {
                        break;
                    }
                    return -1;

                case (GridElementStates.Visible | GridElementStates.Selected):
                    if (this.rowCountsVisibleSelected == 0)
                    {
                        return -1;
                    }
                    break;
            }
            int rowIndex = 0;
            while ((rowIndex < this.items.Count) && (((this.GetRowState(rowIndex) & includeFilter) != includeFilter) || ((this.GetRowState(rowIndex) & excludeFilter) != GridElementStates.None)))
            {
                rowIndex++;
            }
            if (rowIndex >= this.items.Count)
            {
                return -1;
            }
            return rowIndex;
        }

        /// <summary>Returns the index of the last <see cref="T:MControl.GridView.GridRow"></see> that meets the specified criteria.</summary>
        /// <returns>The index of the last <see cref="T:MControl.GridView.GridRow"></see> that has the attributes specified by includeFilter; -1 if no row is found.</returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <exception cref="T:System.ArgumentException">includeFilter is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public int GetLastRow(GridElementStates includeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            switch (includeFilter)
            {
                case GridElementStates.Visible:
                    if (this.rowCountsVisible != 0)
                    {
                        break;
                    }
                    return -1;

                case (GridElementStates.Visible | GridElementStates.Frozen):
                    if (this.rowCountsVisibleFrozen != 0)
                    {
                        break;
                    }
                    return -1;

                case (GridElementStates.Visible | GridElementStates.Selected):
                    if (this.rowCountsVisibleSelected == 0)
                    {
                        return -1;
                    }
                    break;
            }
            int rowIndex = this.items.Count - 1;
            while ((rowIndex >= 0) && ((this.GetRowState(rowIndex) & includeFilter) != includeFilter))
            {
                rowIndex--;
            }
            if (rowIndex < 0)
            {
                return -1;
            }
            return rowIndex;
        }

        /// <summary>Returns the index of the next <see cref="T:MControl.GridView.GridRow"></see> that meets the specified criteria.</summary>
        /// <returns>The index of the first <see cref="T:MControl.GridView.GridRow"></see> after indexStart that has the attributes specified by includeFilter, or -1 if no row is found.</returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <exception cref="T:System.ArgumentException">includeFilter is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">indexStart is less than -1.</exception>
        public int GetNextRow(int indexStart, GridElementStates includeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if (indexStart < -1)
            {
                object[] args = new object[] { "indexStart", indexStart.ToString(CultureInfo.CurrentCulture), -1.ToString(CultureInfo.CurrentCulture) };
                throw new ArgumentOutOfRangeException("indexStart", MControl.GridView.RM.GetString("InvalidLowBoundArgumentEx", args));
            }
            int rowIndex = indexStart + 1;
            while ((rowIndex < this.items.Count) && ((this.GetRowState(rowIndex) & includeFilter) != includeFilter))
            {
                rowIndex++;
            }
            if (rowIndex >= this.items.Count)
            {
                return -1;
            }
            return rowIndex;
        }

        internal int GetNextRow(int indexStart, GridElementStates includeFilter, int skipRows)
        {
            int nextRow = indexStart;
            do
            {
                nextRow = this.GetNextRow(nextRow, includeFilter);
                skipRows--;
            }
            while ((skipRows >= 0) && (nextRow != -1));
            return nextRow;
        }

        /// <summary>Returns the index of the next <see cref="T:MControl.GridView.GridRow"></see> that meets the specified inclusion and exclusion criteria.</summary>
        /// <returns>The index of the next <see cref="T:MControl.GridView.GridRow"></see> that has the attributes specified by includeFilter, and does not have the attributes specified by excludeFilter; -1 if no row is found.</returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <param name="excludeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">indexStart is less than -1.</exception>
        public int GetNextRow(int indexStart, GridElementStates includeFilter, GridElementStates excludeFilter)
        {
            if (excludeFilter == GridElementStates.None)
            {
                return this.GetNextRow(indexStart, includeFilter);
            }
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if ((excludeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "excludeFilter" }));
            }
            if (indexStart < -1)
            {
                object[] args = new object[] { "indexStart", indexStart.ToString(CultureInfo.CurrentCulture), -1.ToString(CultureInfo.CurrentCulture) };
                throw new ArgumentOutOfRangeException("indexStart", MControl.GridView.RM.GetString("InvalidLowBoundArgumentEx", args));
            }
            int rowIndex = indexStart + 1;
            while ((rowIndex < this.items.Count) && (((this.GetRowState(rowIndex) & includeFilter) != includeFilter) || ((this.GetRowState(rowIndex) & excludeFilter) != GridElementStates.None)))
            {
                rowIndex++;
            }
            if (rowIndex >= this.items.Count)
            {
                return -1;
            }
            return rowIndex;
        }

        /// <summary>Returns the index of the previous <see cref="T:MControl.GridView.GridRow"></see> that meets the specified criteria.</summary>
        /// <returns>The index of the previous <see cref="T:MControl.GridView.GridRow"></see> that has the attributes specified by includeFilter; -1 if no row is found.</returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <exception cref="T:System.ArgumentException">includeFilter is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">indexStart is greater than the number of rows in the collection.</exception>
        public int GetPreviousRow(int indexStart, GridElementStates includeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if (indexStart > this.items.Count)
            {
                throw new ArgumentOutOfRangeException("indexStart", MControl.GridView.RM.GetString("InvalidHighBoundArgumentEx", new object[] { "indexStart", indexStart.ToString(CultureInfo.CurrentCulture), this.items.Count.ToString(CultureInfo.CurrentCulture) }));
            }
            int rowIndex = indexStart - 1;
            while ((rowIndex >= 0) && ((this.GetRowState(rowIndex) & includeFilter) != includeFilter))
            {
                rowIndex--;
            }
            if (rowIndex < 0)
            {
                return -1;
            }
            return rowIndex;
        }

        /// <summary>Returns the index of the previous <see cref="T:MControl.GridView.GridRow"></see> that meets the specified inclusion and exclusion criteria.</summary>
        /// <returns>The index of the previous <see cref="T:MControl.GridView.GridRow"></see> that has the attributes specified by includeFilter, and does not have the attributes specified by excludeFilter; -1 if no row is found.</returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <param name="excludeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">indexStart is greater than the number of rows in the collection.</exception>
        public int GetPreviousRow(int indexStart, GridElementStates includeFilter, GridElementStates excludeFilter)
        {
            if (excludeFilter == GridElementStates.None)
            {
                return this.GetPreviousRow(indexStart, includeFilter);
            }
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            if ((excludeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "excludeFilter" }));
            }
            if (indexStart > this.items.Count)
            {
                throw new ArgumentOutOfRangeException("indexStart", MControl.GridView.RM.GetString("InvalidHighBoundArgumentEx", new object[] { "indexStart", indexStart.ToString(CultureInfo.CurrentCulture), this.items.Count.ToString(CultureInfo.CurrentCulture) }));
            }
            int rowIndex = indexStart - 1;
            while ((rowIndex >= 0) && (((this.GetRowState(rowIndex) & includeFilter) != includeFilter) || ((this.GetRowState(rowIndex) & excludeFilter) != GridElementStates.None)))
            {
                rowIndex--;
            }
            if (rowIndex < 0)
            {
                return -1;
            }
            return rowIndex;
        }

        /// <summary>Returns the number of <see cref="T:MControl.GridView.GridRow"></see> objects in the collection that meet the specified criteria.</summary>
        /// <returns>The number of <see cref="T:MControl.GridView.GridRow"></see> objects in the <see cref="T:MControl.GridView.GridRowCollection"></see> that have the attributes specified by includeFilter.</returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <exception cref="T:System.ArgumentException">includeFilter is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public int GetRowCount(GridElementStates includeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            switch (includeFilter)
            {
                case GridElementStates.Visible:
                    if (this.rowCountsVisible == -1)
                    {
                        break;
                    }
                    return this.rowCountsVisible;

                case (GridElementStates.Visible | GridElementStates.Frozen):
                    if (this.rowCountsVisibleFrozen == -1)
                    {
                        break;
                    }
                    return this.rowCountsVisibleFrozen;

                case (GridElementStates.Visible | GridElementStates.Selected):
                    if (this.rowCountsVisibleSelected != -1)
                    {
                        return this.rowCountsVisibleSelected;
                    }
                    break;
            }
            int num = 0;
            for (int i = 0; i < this.items.Count; i++)
            {
                if ((this.GetRowState(i) & includeFilter) == includeFilter)
                {
                    num++;
                }
            }
            switch (includeFilter)
            {
                case GridElementStates.Visible:
                    this.rowCountsVisible = num;
                    return num;

                case (GridElementStates.Visible | GridElementStates.Displayed):
                    return num;

                case (GridElementStates.Visible | GridElementStates.Frozen):
                    this.rowCountsVisibleFrozen = num;
                    return num;

                case (GridElementStates.Visible | GridElementStates.Selected):
                    this.rowCountsVisibleSelected = num;
                    return num;
            }
            return num;
        }

        internal int GetRowCount(GridElementStates includeFilter, int fromRowIndex, int toRowIndex)
        {
            int num = 0;
            for (int i = fromRowIndex + 1; i <= toRowIndex; i++)
            {
                if ((this.GetRowState(i) & includeFilter) == includeFilter)
                {
                    num++;
                }
            }
            return num;
        }

        /// <summary>Returns the cumulative height of the <see cref="T:MControl.GridView.GridRow"></see> objects that meet the specified criteria.</summary>
        /// <returns>The cumulative height of <see cref="T:MControl.GridView.GridRow"></see> objects in the <see cref="T:MControl.GridView.GridRowCollection"></see> that have the attributes specified by includeFilter.</returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <exception cref="T:System.ArgumentException">includeFilter is not a valid bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values.</exception>
        public int GetRowsHeight(GridElementStates includeFilter)
        {
            if ((includeFilter & ~(GridElementStates.Visible | GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen | GridElementStates.Displayed)) != GridElementStates.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridElementStateCombination", new object[] { "includeFilter" }));
            }
            switch (includeFilter)
            {
                case GridElementStates.Visible:
                    if (this.rowsHeightVisible == -1)
                    {
                        break;
                    }
                    return this.rowsHeightVisible;

                case (GridElementStates.Visible | GridElementStates.Frozen):
                    if (this.rowsHeightVisibleFrozen == -1)
                    {
                        break;
                    }
                    return this.rowsHeightVisibleFrozen;
            }
            int num = 0;
            for (int i = 0; i < this.items.Count; i++)
            {
                if ((this.GetRowState(i) & includeFilter) == includeFilter)
                {
                    num += ((GridRow) this.items[i]).GetHeight(i);
                }
            }
            switch (includeFilter)
            {
                case GridElementStates.Visible:
                    this.rowsHeightVisible = num;
                    return num;

                case (GridElementStates.Visible | GridElementStates.Displayed):
                    return num;

                case (GridElementStates.Visible | GridElementStates.Frozen):
                    this.rowsHeightVisibleFrozen = num;
                    return num;
            }
            return num;
        }

        internal int GetRowsHeight(GridElementStates includeFilter, int fromRowIndex, int toRowIndex)
        {
            int num = 0;
            for (int i = fromRowIndex; i < toRowIndex; i++)
            {
                if ((this.GetRowState(i) & includeFilter) == includeFilter)
                {
                    num += ((GridRow) this.items[i]).GetHeight(i);
                }
            }
            return num;
        }

        private bool GetRowsHeightExceedLimit(GridElementStates includeFilter, int fromRowIndex, int toRowIndex, int heightLimit)
        {
            int num = 0;
            for (int i = fromRowIndex; i < toRowIndex; i++)
            {
                if ((this.GetRowState(i) & includeFilter) == includeFilter)
                {
                    num += ((GridRow) this.items[i]).GetHeight(i);
                    if (num > heightLimit)
                    {
                        return true;
                    }
                }
            }
            return (num > heightLimit);
        }

        /// <summary>Gets the state of the row with the specified index.</summary>
        /// <returns>A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values indicating the state of the specified row.</returns>
        /// <param name="rowIndex">The index of the row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than zero and greater than the number of rows in the collection minus one.</exception>
        public virtual GridElementStates GetRowState(int rowIndex)
        {
            if ((rowIndex < 0) || (rowIndex >= this.items.Count))
            {
                throw new ArgumentOutOfRangeException("rowIndex", MControl.GridView.RM.GetString("GridRowCollection_RowIndexOutOfRange"));
            }
            GridRow row = this.SharedRow(rowIndex);
            if (row.Index == -1)
            {
                return this.SharedRowState(rowIndex);
            }
            return row.GetState(rowIndex);
        }

        /// <summary>Returns the index of a specified item in the collection.</summary>
        /// <returns>The index of value if it is a <see cref="T:MControl.GridView.GridRow"></see> found in the <see cref="T:MControl.GridView.GridRowCollection"></see>; otherwise, -1.</returns>
        /// <param name="gridRow">The <see cref="T:MControl.GridView.GridRow"></see> to locate in the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <filterpriority>1</filterpriority>
        public int IndexOf(GridRow gridRow)
        {
            return this.items.IndexOf(gridRow);
        }

        /// <summary>Inserts the specified number of rows into the collection at the specified location.</summary>
        /// <param name="count">The number of rows to insert into the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <param name="rowIndex">The position at which to insert the rows.</param>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-The <see cref="T:MControl.GridView.Grid"></see> has no columns.-or-rowIndex is equal to the number of rows in the collection and the <see cref="P:MControl.GridView.Grid.AllowUserToAddRows"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is set to true.-or-The row returned by the <see cref="P:MControl.GridView.Grid.RowTemplate"></see> property has more cells than there are columns in the control. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than zero or greater than the number of rows in the collection. -or-count is less than 1.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Insert(int rowIndex, int count)
        {
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if ((rowIndex < 0) || (this.Count < rowIndex))
            {
                throw new ArgumentOutOfRangeException("rowIndex", MControl.GridView.RM.GetString("GridRowCollection_IndexDestinationOutOfRange"));
            }
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count", MControl.GridView.RM.GetString("GridRowCollection_CountOutOfRange"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            if (this.Grid.Columns.Count == 0)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoColumns"));
            }
            if (this.Grid.RowTemplate.Cells.Count > this.Grid.Columns.Count)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_RowTemplateTooManyCells"));
            }
            if ((this.Grid.NewRowIndex != -1) && (rowIndex == this.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoInsertionAfterNewRow"));
            }
            GridRow rowTemplateClone = this.Grid.RowTemplateClone;
            GridElementStates state = rowTemplateClone.State;
            rowTemplateClone.GridInternal = this.grid;
            int num = 0;
            foreach (GridCell cell in rowTemplateClone.Cells)
            {
                cell.GridInternal = this.grid;
                cell.OwningColumnInternal = this.Grid.Columns[num];
                num++;
            }
            if (rowTemplateClone.HasHeaderCell)
            {
                rowTemplateClone.HeaderCell.GridInternal = this.grid;
                rowTemplateClone.HeaderCell.OwningRowInternal = rowTemplateClone;
            }
            this.InsertCopiesPrivate(rowTemplateClone, state, rowIndex, count);
        }

        /// <summary>Inserts the specified <see cref="T:MControl.GridView.GridRow"></see> into the collection.</summary>
        /// <param name="gridRow">The <see cref="T:MControl.GridView.GridRow"></see> to insert into the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <param name="rowIndex">The position at which to insert the row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than zero or greater than the number of rows in the collection. </exception>
        /// <exception cref="T:System.ArgumentException">gridRow has more cells than there are columns in the control.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-rowIndex is equal to the number of rows in the collection and the <see cref="P:MControl.GridView.Grid.AllowUserToAddRows"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is set to true.-or-The <see cref="T:MControl.GridView.Grid"></see> has no columns.-or-The <see cref="P:MControl.GridView.GridElement.Grid"></see> property of gridRow is not null.-or-gridRow has a <see cref="P:MControl.GridView.GridRow.Selected"></see> property value of true. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
        /// <exception cref="T:System.ArgumentNullException">gridRow is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Insert(int rowIndex, GridRow gridRow)
        {
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            this.InsertInternal(rowIndex, gridRow);
        }

        /// <summary>Inserts a row into the collection at the specified position, and populates the cells with the specified objects.</summary>
        /// <param name="rowIndex">The position at which to insert the row.</param>
        /// <param name="values">A variable number of objects that populate the cells of the new row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than zero or greater than the number of rows in the collection. </exception>
        /// <exception cref="T:System.ArgumentException">The row returned by the control's <see cref="P:MControl.GridView.Grid.RowTemplate"></see> property has more cells than there are columns in the control.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-The <see cref="P:MControl.GridView.Grid.VirtualMode"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is set to true.-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-The <see cref="T:MControl.GridView.Grid"></see> has no columns.-or-rowIndex is equal to the number of rows in the collection and the <see cref="P:MControl.GridView.Grid.AllowUserToAddRows"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is set to true.-or-The <see cref="P:MControl.GridView.GridElement.Grid"></see> property of the row returned by the control's <see cref="P:MControl.GridView.Grid.RowTemplate"></see> property is not null. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
        /// <exception cref="T:System.ArgumentNullException">values is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Insert(int rowIndex, params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (this.Grid.VirtualMode)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationInVirtualMode"));
            }
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            GridRow rowTemplateClone = this.Grid.RowTemplateClone;
            rowTemplateClone.SetValuesInternal(values);
            this.Insert(rowIndex, rowTemplateClone);
        }

        /// <summary>Inserts rows into the collection at the specified position.</summary>
        /// <param name="indexDestination">The position at which to insert the rows.</param>
        /// <param name="count">The number of <see cref="T:MControl.GridView.GridRow"></see> objects to add to the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <param name="indexSource">The index of the <see cref="T:MControl.GridView.GridRow"></see> on which to base the new rows.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">indexSource is less than zero or greater than the number of rows in the collection minus one.-or-indexDestination is less than zero or greater than the number of rows in the collection.-or-count is less than 1.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-indexDestination is equal to the number of rows in the collection and <see cref="P:MControl.GridView.Grid.AllowUserToAddRows"></see> is true.-or-This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void InsertCopies(int indexSource, int indexDestination, int count)
        {
            if (this.Grid.DataSource != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            this.InsertCopiesPrivate(indexSource, indexDestination, count);
        }

        private void InsertCopiesPrivate(int indexSource, int indexDestination, int count)
        {
            if ((indexSource < 0) || (this.Count <= indexSource))
            {
                throw new ArgumentOutOfRangeException("indexSource", MControl.GridView.RM.GetString("GridRowCollection_IndexSourceOutOfRange"));
            }
            if ((indexDestination < 0) || (this.Count < indexDestination))
            {
                throw new ArgumentOutOfRangeException("indexDestination", MControl.GridView.RM.GetString("GridRowCollection_IndexDestinationOutOfRange"));
            }
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count", MControl.GridView.RM.GetString("GridRowCollection_CountOutOfRange"));
            }
            if ((this.Grid.NewRowIndex != -1) && (indexDestination == this.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoInsertionAfterNewRow"));
            }
            GridElementStates rowTemplateState = this.GetRowState(indexSource) & ~(GridElementStates.Selected | GridElementStates.Displayed);
            this.InsertCopiesPrivate(this.SharedRow(indexSource), rowTemplateState, indexDestination, count);
        }

        private void InsertCopiesPrivate(GridRow rowTemplate, GridElementStates rowTemplateState, int indexDestination, int count)
        {
            Point newCurrentCell = new Point(-1, -1);
            if (rowTemplate.Index == -1)
            {
                if (count > 1)
                {
                    this.Grid.OnInsertingRow(indexDestination, rowTemplate, rowTemplateState, ref newCurrentCell, true, count, false);
                    for (int i = 0; i < count; i++)
                    {
                        this.SharedList.Insert(indexDestination + i, rowTemplate);
                        this.rowStates.Insert(indexDestination + i, rowTemplateState);
                    }
                    this.Grid.OnInsertedRow_PreNotification(indexDestination, count);
                    this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), indexDestination, count, false, true, false, newCurrentCell);
                    for (int j = 0; j < count; j++)
                    {
                        this.Grid.OnInsertedRow_PostNotification(indexDestination + j, newCurrentCell, j == (count - 1));
                    }
                }
                else
                {
                    this.Grid.OnInsertingRow(indexDestination, rowTemplate, rowTemplateState, ref newCurrentCell, true, 1, false);
                    this.SharedList.Insert(indexDestination, rowTemplate);
                    this.rowStates.Insert(indexDestination, rowTemplateState);
                    this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(indexDestination)), indexDestination, count, false, true, false, newCurrentCell);
                }
            }
            else
            {
                this.InsertDuplicateRow(indexDestination, rowTemplate, true, ref newCurrentCell);
                if (count > 1)
                {
                    this.Grid.OnInsertedRow_PreNotification(indexDestination, 1);
                    if (this.RowIsSharable(indexDestination))
                    {
                        GridRow gridRow = this.SharedRow(indexDestination);
                        this.Grid.OnInsertingRow(indexDestination + 1, gridRow, rowTemplateState, ref newCurrentCell, false, count - 1, false);
                        for (int m = 1; m < count; m++)
                        {
                            this.SharedList.Insert(indexDestination + m, gridRow);
                            this.rowStates.Insert(indexDestination + m, rowTemplateState);
                        }
                        this.Grid.OnInsertedRow_PreNotification(indexDestination + 1, count - 1);
                        this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), indexDestination, count, false, true, false, newCurrentCell);
                    }
                    else
                    {
                        this.UnshareRow(indexDestination);
                        for (int n = 1; n < count; n++)
                        {
                            this.InsertDuplicateRow(indexDestination + n, rowTemplate, false, ref newCurrentCell);
                            this.UnshareRow(indexDestination + n);
                        }
                        this.Grid.OnInsertedRow_PreNotification(indexDestination + 1, count - 1);
                        this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), indexDestination, count, false, true, false, newCurrentCell);
                    }
                    for (int k = 0; k < count; k++)
                    {
                        this.Grid.OnInsertedRow_PostNotification(indexDestination + k, newCurrentCell, k == (count - 1));
                    }
                }
                else
                {
                    if (this.IsCollectionChangedListenedTo)
                    {
                        this.UnshareRow(indexDestination);
                    }
                    this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(indexDestination)), indexDestination, 1, false, true, false, newCurrentCell);
                }
            }
        }

        /// <summary>Inserts a row into the collection at the specified position, based on the row at specified position.</summary>
        /// <param name="indexDestination">The position at which to insert the row.</param>
        /// <param name="indexSource">The index of the row on which to base the new row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">indexSource is less than zero or greater than the number of rows in the collection minus one.-or-indexDestination is less than zero or greater than the number of rows in the collection.</exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-indexDestination is equal to the number of rows in the collection and <see cref="P:MControl.GridView.Grid.AllowUserToAddRows"></see> is true. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void InsertCopy(int indexSource, int indexDestination)
        {
            this.InsertCopies(indexSource, indexDestination, 1);
        }

        private void InsertDuplicateRow(int indexDestination, GridRow rowTemplate, bool firstInsertion, ref Point newCurrentCell)
        {
            GridRow gridRow = (GridRow) rowTemplate.Clone();
            gridRow.StateInternal = GridElementStates.None;
            gridRow.GridInternal = this.grid;
            GridCellCollection cells = gridRow.Cells;
            int num = 0;
            foreach (GridCell cell in cells)
            {
                cell.GridInternal = this.grid;
                cell.OwningColumnInternal = this.Grid.Columns[num];
                num++;
            }
            GridElementStates rowState = rowTemplate.State & ~(GridElementStates.Selected | GridElementStates.Displayed);
            if (gridRow.HasHeaderCell)
            {
                gridRow.HeaderCell.GridInternal = this.grid;
                gridRow.HeaderCell.OwningRowInternal = gridRow;
            }
            this.Grid.OnInsertingRow(indexDestination, gridRow, rowState, ref newCurrentCell, firstInsertion, 1, false);
            this.SharedList.Insert(indexDestination, gridRow);
            this.rowStates.Insert(indexDestination, rowState);
        }

        internal void InsertInternal(int rowIndex, GridRow gridRow)
        {
            if ((rowIndex < 0) || (this.Count < rowIndex))
            {
                throw new ArgumentOutOfRangeException("rowIndex", MControl.GridView.RM.GetString("GridRowCollection_RowIndexOutOfRange"));
            }
            if (gridRow == null)
            {
                throw new ArgumentNullException("gridRow");
            }
            if (gridRow.Grid != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_RowAlreadyBelongsToGrid"));
            }
            if ((this.Grid.NewRowIndex != -1) && (rowIndex == this.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoInsertionAfterNewRow"));
            }
            if (this.Grid.Columns.Count == 0)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoColumns"));
            }
            if (gridRow.Cells.Count > this.Grid.Columns.Count)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("GridRowCollection_TooManyCells"), "gridRow");
            }
            if (gridRow.Selected)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_CannotAddOrInsertSelectedRow"));
            }
            this.InsertInternal(rowIndex, gridRow, false);
        }

        internal void InsertInternal(int rowIndex, GridRow gridRow, bool force)
        {
            Point newCurrentCell = new Point(-1, -1);
            if (force)
            {
                if (this.Grid.Columns.Count == 0)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoColumns"));
                }
                if (gridRow.Cells.Count > this.Grid.Columns.Count)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("GridRowCollection_TooManyCells"), "gridRow");
                }
            }
            this.Grid.CompleteCellsCollection(gridRow);
            this.Grid.OnInsertingRow(rowIndex, gridRow, gridRow.State, ref newCurrentCell, true, 1, force);
            int num = 0;
            foreach (GridCell cell in gridRow.Cells)
            {
                cell.GridInternal = this.grid;
                if (cell.ColumnIndex == -1)
                {
                    cell.OwningColumnInternal = this.Grid.Columns[num];
                }
                num++;
            }
            if (gridRow.HasHeaderCell)
            {
                gridRow.HeaderCell.GridInternal = this.Grid;
                gridRow.HeaderCell.OwningRowInternal = gridRow;
            }
            this.SharedList.Insert(rowIndex, gridRow);
            this.rowStates.Insert(rowIndex, gridRow.State);
            gridRow.GridInternal = this.grid;
            if ((!this.RowIsSharable(rowIndex) || RowHasValueOrToolTipText(gridRow)) || this.IsCollectionChangedListenedTo)
            {
                gridRow.IndexInternal = rowIndex;
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, gridRow), rowIndex, 1, false, true, false, newCurrentCell);
        }

        /// <summary>Inserts the <see cref="T:MControl.GridView.GridRow"></see> objects into the collection at the specified position.</summary>
        /// <param name="rowIndex">The position at which to insert the rows.</param>
        /// <param name="gridRows">An array of <see cref="T:MControl.GridView.GridRow"></see> objects to add to the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-rowIndex is equal to the number of rows in the collection and <see cref="P:MControl.GridView.Grid.AllowUserToAddRows"></see> is true.-or-The <see cref="P:MControl.GridView.Grid.DataSource"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is not null.-or-At least one entry in the gridRows array is null.-or-The <see cref="T:MControl.GridView.Grid"></see> has no columns.-or-At least one row in the gridRows array has a <see cref="P:MControl.GridView.GridElement.Grid"></see> property value that is not null.-or-At least one row in the gridRows array has a <see cref="P:MControl.GridView.GridRow.Selected"></see> property value of true.-or-Two or more rows in the gridRows array are identical.-or-At least one row in the gridRows array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.-or-At least one row in the gridRows array contains more cells than there are columns in the control. -or-This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than zero or greater than the number of rows in the collection.</exception>
        /// <exception cref="T:System.ArgumentException">gridRows contains only one row, and the row it contains has more cells than there are columns in the control.</exception>
        /// <exception cref="T:System.ArgumentNullException">gridRows is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void InsertRange(int rowIndex, params GridRow[] gridRows)
        {
            if (gridRows == null)
            {
                throw new ArgumentNullException("gridRows");
            }
            if (gridRows.Length == 1)
            {
                this.Insert(rowIndex, gridRows[0]);
            }
            else
            {
                if ((rowIndex < 0) || (rowIndex > this.Count))
                {
                    throw new ArgumentOutOfRangeException("rowIndex", MControl.GridView.RM.GetString("GridRowCollection_IndexDestinationOutOfRange"));
                }
                if (this.Grid.NoDimensionChangeAllowed)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
                }
                if ((this.Grid.NewRowIndex != -1) && (rowIndex == this.Count))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoInsertionAfterNewRow"));
                }
                if (this.Grid.DataSource != null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_AddUnboundRow"));
                }
                if (this.Grid.Columns.Count == 0)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_NoColumns"));
                }
                Point newCurrentCell = new Point(-1, -1);
                this.Grid.OnInsertingRows(rowIndex, gridRows, ref newCurrentCell);
                int index = rowIndex;
                foreach (GridRow row in gridRows)
                {
                    int num2 = 0;
                    foreach (GridCell cell in row.Cells)
                    {
                        cell.GridInternal = this.grid;
                        if (cell.ColumnIndex == -1)
                        {
                            cell.OwningColumnInternal = this.Grid.Columns[num2];
                        }
                        num2++;
                    }
                    if (row.HasHeaderCell)
                    {
                        row.HeaderCell.GridInternal = this.Grid;
                        row.HeaderCell.OwningRowInternal = row;
                    }
                    this.SharedList.Insert(index, row);
                    this.rowStates.Insert(index, row.State);
                    row.IndexInternal = index;
                    row.GridInternal = this.grid;
                    index++;
                }
                this.Grid.OnInsertedRows_PreNotification(rowIndex, gridRows);
                this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), rowIndex, gridRows.Length, false, true, false, newCurrentCell);
                this.Grid.OnInsertedRows_PostNotification(gridRows, newCurrentCell);
            }
        }

        internal void InvalidateCachedRowCount(GridElementStates includeFilter)
        {
            if (includeFilter == GridElementStates.Visible)
            {
                this.InvalidateCachedRowCounts();
            }
            else if (includeFilter == GridElementStates.Frozen)
            {
                this.rowCountsVisibleFrozen = -1;
            }
            else if (includeFilter == GridElementStates.Selected)
            {
                this.rowCountsVisibleSelected = -1;
            }
        }

        internal void InvalidateCachedRowCounts()
        {
            this.rowCountsVisible = this.rowCountsVisibleFrozen = this.rowCountsVisibleSelected = -1;
        }

        internal void InvalidateCachedRowsHeight(GridElementStates includeFilter)
        {
            if (includeFilter == GridElementStates.Visible)
            {
                this.InvalidateCachedRowsHeights();
            }
            else if (includeFilter == GridElementStates.Frozen)
            {
                this.rowsHeightVisibleFrozen = -1;
            }
        }

        internal void InvalidateCachedRowsHeights()
        {
            this.rowsHeightVisible = this.rowsHeightVisibleFrozen = -1;
        }

        /// <summary>Raises the <see cref="E:MControl.GridView.GridRowCollection.CollectionChanged"></see> event.</summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs"></see> that contains the event data. </param>
        protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (this.onCollectionChanged != null)
            {
                this.onCollectionChanged(this, e);
            }
        }

        private void OnCollectionChanged(CollectionChangeEventArgs e, int rowIndex, int rowCount)
        {
            Point newCurrentCell = new Point(-1, -1);
            GridRow element = (GridRow) e.Element;
            int index = 0;
            if ((element != null) && (e.Action == CollectionChangeAction.Add))
            {
                index = this.SharedRow(rowIndex).Index;
            }
            this.OnCollectionChanged_PreNotification(e.Action, rowIndex, rowCount, ref element, false);
            if ((index == -1) && (this.SharedRow(rowIndex).Index != -1))
            {
                e = new CollectionChangeEventArgs(e.Action, element);
            }
            this.OnCollectionChanged(e);
            this.OnCollectionChanged_PostNotification(e.Action, rowIndex, rowCount, element, false, false, false, newCurrentCell);
        }

        private void OnCollectionChanged(CollectionChangeEventArgs e, int rowIndex, int rowCount, bool changeIsDeletion, bool changeIsInsertion, bool recreateNewRow, Point newCurrentCell)
        {
            GridRow element = (GridRow) e.Element;
            int index = 0;
            if ((element != null) && (e.Action == CollectionChangeAction.Add))
            {
                index = this.SharedRow(rowIndex).Index;
            }
            this.OnCollectionChanged_PreNotification(e.Action, rowIndex, rowCount, ref element, changeIsInsertion);
            if ((index == -1) && (this.SharedRow(rowIndex).Index != -1))
            {
                e = new CollectionChangeEventArgs(e.Action, element);
            }
            this.OnCollectionChanged(e);
            this.OnCollectionChanged_PostNotification(e.Action, rowIndex, rowCount, element, changeIsDeletion, changeIsInsertion, recreateNewRow, newCurrentCell);
        }

        private void OnCollectionChanged_PostNotification(CollectionChangeAction cca, int rowIndex, int rowCount, GridRow gridRow, bool changeIsDeletion, bool changeIsInsertion, bool recreateNewRow, Point newCurrentCell)
        {
            if (changeIsDeletion)
            {
                this.Grid.OnRowsRemovedInternal(rowIndex, rowCount);
            }
            else
            {
                this.Grid.OnRowsAddedInternal(rowIndex, rowCount);
            }
            switch (cca)
            {
                case CollectionChangeAction.Add:
                    if (!changeIsInsertion)
                    {
                        this.Grid.OnAddedRow_PostNotification(rowIndex);
                        break;
                    }
                    this.Grid.OnInsertedRow_PostNotification(rowIndex, newCurrentCell, true);
                    break;

                case CollectionChangeAction.Remove:
                    this.Grid.OnRemovedRow_PostNotification(gridRow, newCurrentCell);
                    break;

                case CollectionChangeAction.Refresh:
                    if (changeIsDeletion)
                    {
                        this.Grid.OnClearedRows();
                    }
                    break;
            }
            this.Grid.OnRowCollectionChanged_PostNotification(recreateNewRow, newCurrentCell.X == -1, cca, gridRow, rowIndex);
        }

        private void OnCollectionChanged_PreNotification(CollectionChangeAction cca, int rowIndex, int rowCount, ref GridRow gridRow, bool changeIsInsertion)
        {
            int height;
            bool useRowShortcut = false;
            bool computeVisibleRows = false;
            switch (cca)
            {
                case CollectionChangeAction.Add:
                    height = 0;
                    this.UpdateRowCaches(rowIndex, ref gridRow, true);
                    if ((this.GetRowState(rowIndex) & GridElementStates.Visible) != GridElementStates.None)
                    {
                        int firstDisplayedRowIndex = this.Grid.FirstDisplayedRowIndex;
                        if (firstDisplayedRowIndex != -1)
                        {
                            height = this.SharedRow(firstDisplayedRowIndex).GetHeight(firstDisplayedRowIndex);
                        }
                        break;
                    }
                    useRowShortcut = true;
                    computeVisibleRows = changeIsInsertion;
                    break;

                case CollectionChangeAction.Remove:
                {
                    GridElementStates rowState = this.GetRowState(rowIndex);
                    bool flag3 = (rowState & GridElementStates.Visible) != GridElementStates.None;
                    bool flag4 = (rowState & GridElementStates.Frozen) != GridElementStates.None;
                    this.rowStates.RemoveAt(rowIndex);
                    this.SharedList.RemoveAt(rowIndex);
                    this.Grid.OnRemovedRow_PreNotification(rowIndex);
                    if (!flag3)
                    {
                        useRowShortcut = true;
                    }
                    else if (!flag4)
                    {
                        if ((this.Grid.FirstDisplayedScrollingRowIndex != -1) && (rowIndex > this.Grid.FirstDisplayedScrollingRowIndex))
                        {
                            int num4 = 0;
                            int num5 = this.Grid.FirstDisplayedRowIndex;
                            if (num5 != -1)
                            {
                                num4 = this.SharedRow(num5).GetHeight(num5);
                            }
                            useRowShortcut = this.GetRowsHeightExceedLimit(GridElementStates.Visible, 0, rowIndex, (this.Grid.LayoutInfo.Data.Height + this.Grid.VerticalScrollingOffset) + SystemInformation.HorizontalScrollBarHeight) && (num4 <= this.Grid.LayoutInfo.Data.Height);
                        }
                    }
                    else
                    {
                        useRowShortcut = (this.Grid.FirstDisplayedScrollingRowIndex == -1) && this.GetRowsHeightExceedLimit(GridElementStates.Visible, 0, rowIndex, this.Grid.LayoutInfo.Data.Height + SystemInformation.HorizontalScrollBarHeight);
                    }
                    goto Label_02DF;
                }
                case CollectionChangeAction.Refresh:
                    this.InvalidateCachedRowCounts();
                    this.InvalidateCachedRowsHeights();
                    goto Label_02DF;

                default:
                    goto Label_02DF;
            }
            if (changeIsInsertion)
            {
                this.Grid.OnInsertedRow_PreNotification(rowIndex, 1);
                if (!useRowShortcut)
                {
                    if ((this.GetRowState(rowIndex) & GridElementStates.Frozen) != GridElementStates.None)
                    {
                        useRowShortcut = (this.Grid.FirstDisplayedScrollingRowIndex == -1) && this.GetRowsHeightExceedLimit(GridElementStates.Visible, 0, rowIndex, this.Grid.LayoutInfo.Data.Height);
                    }
                    else if ((this.Grid.FirstDisplayedScrollingRowIndex != -1) && (rowIndex > this.Grid.FirstDisplayedScrollingRowIndex))
                    {
                        useRowShortcut = this.GetRowsHeightExceedLimit(GridElementStates.Visible, 0, rowIndex, this.Grid.LayoutInfo.Data.Height + this.Grid.VerticalScrollingOffset) && (height <= this.Grid.LayoutInfo.Data.Height);
                    }
                }
            }
            else
            {
                this.Grid.OnAddedRow_PreNotification(rowIndex);
                if (!useRowShortcut)
                {
                    int num3 = (this.GetRowsHeight(GridElementStates.Visible) - this.Grid.VerticalScrollingOffset) - gridRow.GetHeight(rowIndex);
                    gridRow = this.SharedRow(rowIndex);
                    useRowShortcut = (this.Grid.LayoutInfo.Data.Height < num3) && (height <= this.Grid.LayoutInfo.Data.Height);
                }
            }
        Label_02DF:
            this.Grid.ResetUIState(useRowShortcut, computeVisibleRows);
        }

        /// <summary>Removes the row from the collection.</summary>
        /// <param name="gridRow">The row to remove from the <see cref="T:MControl.GridView.GridRowCollection"></see>.</param>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-gridRow is the row for new records.-or-The associated <see cref="T:MControl.GridView.Grid"></see> control is bound to an <see cref="T:System.ComponentModel.IBindingList"></see> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove"></see> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification"></see> property values that are not both true. </exception>
        /// <exception cref="T:System.ArgumentException">gridRow is not contained in this collection.-or-gridRow is a shared row.</exception>
        /// <exception cref="T:System.ArgumentNullException">gridRow is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void Remove(GridRow gridRow)
        {
            if (gridRow == null)
            {
                throw new ArgumentNullException("gridRow");
            }
            if (gridRow.Grid != this.Grid)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_RowDoesNotBelongToGrid"), "gridRow");
            }
            if (gridRow.Index == -1)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_RowMustBeUnshared"), "gridRow");
            }
            this.RemoveAt(gridRow.Index);
        }

        /// <summary>Removes the row at the specified position from the collection.</summary>
        /// <param name="index">The position of the row to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero and greater than the number of rows in the collection minus one. </exception>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:MControl.GridView.Grid"></see> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:MControl.GridView.Grid"></see> events:<see cref="E:MControl.GridView.Grid.CellEnter"></see><see cref="E:MControl.GridView.Grid.CellLeave"></see><see cref="E:MControl.GridView.Grid.CellValidating"></see><see cref="E:MControl.GridView.Grid.CellValidated"></see><see cref="E:MControl.GridView.Grid.RowEnter"></see><see cref="E:MControl.GridView.Grid.RowLeave"></see><see cref="E:MControl.GridView.Grid.RowValidated"></see><see cref="E:MControl.GridView.Grid.RowValidating"></see>-or-index is equal to the number of rows in the collection and the <see cref="P:MControl.GridView.Grid.AllowUserToAddRows"></see> property of the <see cref="T:MControl.GridView.Grid"></see> is set to true.-or-The associated <see cref="T:MControl.GridView.Grid"></see> control is bound to an <see cref="T:System.ComponentModel.IBindingList"></see> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove"></see> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification"></see> property values that are not both true.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index", MControl.GridView.RM.GetString("GridRowCollection_RowIndexOutOfRange"));
            }
            if (this.Grid.NewRowIndex == index)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_CannotDeleteNewRow"));
            }
            if (this.Grid.NoDimensionChangeAllowed)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ForbiddenOperationInEventHandler"));
            }
            if (this.Grid.DataSource != null)
            {
                IBindingList list = this.Grid.DataConnection.List as IBindingList;
                if (((list == null) || !list.AllowRemove) || !list.SupportsChangeNotification)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_CantRemoveRowsWithWrongSource"));
                }
                list.RemoveAt(index);
            }
            else
            {
                this.RemoveAtInternal(index, false);
            }
        }

        internal void RemoveAtInternal(int index, bool force)
        {
            GridRow gridRow = this.SharedRow(index);
            Point newCurrentCell = new Point(-1, -1);
            if (this.IsCollectionChangedListenedTo || gridRow.GetDisplayed(index))
            {
                gridRow = this[index];
            }
            gridRow = this.SharedRow(index);
            this.Grid.OnRemovingRow(index, out newCurrentCell, force);
            this.UpdateRowCaches(index, ref gridRow, false);
            if (gridRow.Index != -1)
            {
                this.rowStates[index] = gridRow.State;
                gridRow.DetachFromGrid();
            }
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, gridRow), index, 1, true, false, false, newCurrentCell);
        }

        private static bool RowHasValueOrToolTipText(GridRow gridRow)
        {
            foreach (GridCell cell in gridRow.Cells)
            {
                if (!cell.HasValue && !cell.HasToolTipText)
                {
                    continue;
                }
                return true;
            }
            return false;
        }

        internal bool RowIsSharable(int index)
        {
            GridRow row = this.SharedRow(index);
            if (row.Index != -1)
            {
                return false;
            }
            foreach (GridCell cell in row.Cells)
            {
                if ((cell.State & ~cell.CellStateFromColumnRowStates(this.rowStates[index])) != GridElementStates.None)
                {
                    return false;
                }
            }
            return true;
        }

        internal void SetRowState(int rowIndex, GridElementStates state, bool value)
        {
            GridRow row = this.SharedRow(rowIndex);
            if (row.Index == -1)
            {
                if (((((GridElementStates) this.rowStates[rowIndex]) & state) != GridElementStates.None) != value)
                {
                    if (((state == GridElementStates.Frozen) || (state == GridElementStates.Visible)) || (state == GridElementStates.ReadOnly))
                    {
                        row.OnSharedStateChanging(rowIndex, state);
                    }
                    if (value)
                    {
                        this.rowStates[rowIndex] = ((GridElementStates) this.rowStates[rowIndex]) | state;
                    }
                    else
                    {
                        this.rowStates[rowIndex] = ((GridElementStates) this.rowStates[rowIndex]) & ~state;
                    }
                    row.OnSharedStateChanged(rowIndex, state);
                }
            }
            else
            {
                GridElementStates states = state;
                if (states <= GridElementStates.Resizable)
                {
                    switch (states)
                    {
                        case GridElementStates.Displayed:
                            row.DisplayedInternal = value;
                            return;

                        case GridElementStates.Frozen:
                            row.Frozen = value;
                            return;

                        case (GridElementStates.Frozen | GridElementStates.Displayed):
                            return;

                        case GridElementStates.ReadOnly:
                            row.ReadOnlyInternal = value;
                            return;

                        case GridElementStates.Resizable:
                            row.Resizable = value ? GridTriState.True : GridTriState.False;
                            return;
                    }
                }
                else
                {
                    if (states != GridElementStates.Selected)
                    {
                        if (states != GridElementStates.Visible)
                        {
                            return;
                        }
                    }
                    else
                    {
                        row.SelectedInternal = value;
                        return;
                    }
                    row.Visible = value;
                }
            }
        }

        /// <summary>Returns the <see cref="T:MControl.GridView.GridRow"></see> at the specified index.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridRow"></see> positioned at the specified index.</returns>
        /// <param name="rowIndex">The index of the <see cref="T:MControl.GridView.GridRow"></see> to get.</param>
        /// <filterpriority>1</filterpriority>
        public GridRow SharedRow(int rowIndex)
        {
            return (GridRow) this.SharedList[rowIndex];
        }

        internal GridElementStates SharedRowState(int rowIndex)
        {
            return this.rowStates[rowIndex];
        }

        internal void Sort(IComparer customComparer, bool ascending)
        {
            if (this.items.Count > 0)
            {
                RowComparer rowComparer = new RowComparer(this, customComparer, ascending);
                this.items.CustomSort(rowComparer);
            }
        }

        internal void SwapSortedRows(int rowIndex1, int rowIndex2)
        {
            this.Grid.SwapSortedRows(rowIndex1, rowIndex2);
            GridRow row = this.SharedRow(rowIndex1);
            GridRow row2 = this.SharedRow(rowIndex2);
            if (row.Index != -1)
            {
                row.IndexInternal = rowIndex2;
            }
            if (row2.Index != -1)
            {
                row2.IndexInternal = rowIndex1;
            }
            if (this.Grid.VirtualMode)
            {
                int count = this.Grid.Columns.Count;
                for (int i = 0; i < count; i++)
                {
                    GridCell cell = row.Cells[i];
                    GridCell cell2 = row2.Cells[i];
                    object valueInternal = cell.GetValueInternal(rowIndex1);
                    object obj3 = cell2.GetValueInternal(rowIndex2);
                    cell.SetValueInternal(rowIndex1, obj3);
                    cell2.SetValueInternal(rowIndex2, valueInternal);
                }
            }
            object obj4 = this.items[rowIndex1];
            this.items[rowIndex1] = this.items[rowIndex2];
            this.items[rowIndex2] = obj4;
            GridElementStates states = this.rowStates[rowIndex1];
            this.rowStates[rowIndex1] = this.rowStates[rowIndex2];
            this.rowStates[rowIndex2] = states;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.items.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new UnsharingRowEnumerator(this);
        }

        int IList.Add(object value)
        {
            return this.Add((GridRow) value);
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
            this.Insert(index, (GridRow) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((GridRow) value);
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        private void UnshareRow(int rowIndex)
        {
            this.SharedRow(rowIndex).IndexInternal = rowIndex;
            this.SharedRow(rowIndex).StateInternal = this.SharedRowState(rowIndex);
        }

        private void UpdateRowCaches(int rowIndex, ref GridRow gridRow, bool adding)
        {
            if (((this.rowCountsVisible != -1) || (this.rowCountsVisibleFrozen != -1)) || (((this.rowCountsVisibleSelected != -1) || (this.rowsHeightVisible != -1)) || (this.rowsHeightVisibleFrozen != -1)))
            {
                GridElementStates rowState = this.GetRowState(rowIndex);
                if ((rowState & GridElementStates.Visible) != GridElementStates.None)
                {
                    int num = adding ? 1 : -1;
                    int num2 = 0;
                    if ((this.rowsHeightVisible != -1) || ((this.rowsHeightVisibleFrozen != -1) && ((rowState & (GridElementStates.Visible | GridElementStates.Frozen)) == (GridElementStates.Visible | GridElementStates.Frozen))))
                    {
                        num2 = adding ? gridRow.GetHeight(rowIndex) : -gridRow.GetHeight(rowIndex);
                        gridRow = this.SharedRow(rowIndex);
                    }
                    if (this.rowCountsVisible != -1)
                    {
                        this.rowCountsVisible += num;
                    }
                    if (this.rowsHeightVisible != -1)
                    {
                        this.rowsHeightVisible += num2;
                    }
                    if ((rowState & (GridElementStates.Visible | GridElementStates.Frozen)) == (GridElementStates.Visible | GridElementStates.Frozen))
                    {
                        if (this.rowCountsVisibleFrozen != -1)
                        {
                            this.rowCountsVisibleFrozen += num;
                        }
                        if (this.rowsHeightVisibleFrozen != -1)
                        {
                            this.rowsHeightVisibleFrozen += num2;
                        }
                    }
                    if (((rowState & (GridElementStates.Visible | GridElementStates.Selected)) == (GridElementStates.Visible | GridElementStates.Selected)) && (this.rowCountsVisibleSelected != -1))
                    {
                        this.rowCountsVisibleSelected += num;
                    }
                }
            }
        }

        /// <summary>Gets the number of rows in the collection.</summary>
        /// <returns>The number of rows in the <see cref="T:MControl.GridView.GridRowCollection"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>Gets the <see cref="T:MControl.GridView.Grid"></see> that owns the collection.</summary>
        /// <returns>The <see cref="T:MControl.GridView.Grid"></see> that owns the <see cref="T:MControl.GridView.GridRowCollection"></see>.</returns>
        protected MControl.GridView.Grid Grid
        {
            get
            {
                return this.grid;
            }
        }

        internal bool IsCollectionChangedListenedTo
        {
            get
            {
                return (this.onCollectionChanged != null);
            }
        }

        /// <summary>Gets the <see cref="T:MControl.GridView.GridRow"></see> at the specified index.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridRow"></see> at the specified index. Accessing a <see cref="T:MControl.GridView.GridRow"></see> with this indexer causes the row to become unshared. To keep the row shared, use the <see cref="M:MControl.GridView.GridRowCollection.SharedRow(System.Int32)"></see> method. For more information, see Best Practices for Scaling the Windows Forms Grid Control.</returns>
        /// <param name="index">The zero-based index of the <see cref="T:MControl.GridView.GridRow"></see> to get.</param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public GridRow this[int index]
        {
            get
            {
                GridRow row = this.SharedRow(index);
                if (row.Index != -1)
                {
                    return row;
                }
                GridRow gridRow = (GridRow) row.Clone();
                gridRow.IndexInternal = index;
                gridRow.GridInternal = row.Grid;
                gridRow.StateInternal = this.SharedRowState(index);
                this.SharedList[index] = gridRow;
                int num = 0;
                foreach (GridCell cell in gridRow.Cells)
                {
                    cell.GridInternal = row.Grid;
                    cell.OwningRowInternal = gridRow;
                    cell.OwningColumnInternal = this.Grid.Columns[num];
                    num++;
                }
                if (gridRow.HasHeaderCell)
                {
                    gridRow.HeaderCell.GridInternal = row.Grid;
                    gridRow.HeaderCell.OwningRowInternal = gridRow;
                }
                if (this.Grid != null)
                {
                    this.Grid.OnRowUnshared(gridRow);
                }
                return gridRow;
            }
        }

        /// <summary>Gets an array of <see cref="T:MControl.GridView.GridRow"></see> objects.</summary>
        /// <returns>An array of <see cref="T:MControl.GridView.GridRow"></see> objects.</returns>
        protected ArrayList List
        {
            get
            {
                int count = this.Count;
                for (int i = 0; i < count; i++)
                {
                    GridRow row1 = this[i];
                }
                return this.items;
            }
        }

        internal ArrayList SharedList
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
                return this.Count;
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

        private class RowArrayList : ArrayList
        {
            private GridRowCollection owner;
            private GridRowCollection.RowComparer rowComparer;

            public RowArrayList(GridRowCollection owner)
            {
                this.owner = owner;
            }

            private void CustomQuickSort(int left, int right)
            {
                if ((right - left) < 2)
                {
                    if (((right - left) > 0) && (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(left), this.rowComparer.GetComparedObject(right), left, right) > 0))
                    {
                        this.owner.SwapSortedRows(left, right);
                    }
                }
                else
                {
                    do
                    {
                        int center = (left + right) >> 1;
                        object obj2 = this.Pivot(left, center, right);
                        int num2 = left + 1;
                        int num3 = right - 1;
                        do
                        {
                            while ((center != num2) && (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(num2), obj2, num2, center) < 0))
                            {
                                num2++;
                            }
                            while ((center != num3) && (this.rowComparer.CompareObjects(obj2, this.rowComparer.GetComparedObject(num3), center, num3) < 0))
                            {
                                num3--;
                            }
                            if (num2 > num3)
                            {
                                break;
                            }
                            if (num2 < num3)
                            {
                                this.owner.SwapSortedRows(num2, num3);
                                if (num2 == center)
                                {
                                    center = num3;
                                }
                                else if (num3 == center)
                                {
                                    center = num2;
                                }
                            }
                            num2++;
                            num3--;
                        }
                        while (num2 <= num3);
                        if ((num3 - left) <= (right - num2))
                        {
                            if (left < num3)
                            {
                                this.CustomQuickSort(left, num3);
                            }
                            left = num2;
                        }
                        else
                        {
                            if (num2 < right)
                            {
                                this.CustomQuickSort(num2, right);
                            }
                            right = num3;
                        }
                    }
                    while (left < right);
                }
            }

            public void CustomSort(GridRowCollection.RowComparer rowComparer)
            {
                this.rowComparer = rowComparer;
                this.CustomQuickSort(0, this.Count - 1);
            }

            private object Pivot(int left, int center, int right)
            {
                if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(left), this.rowComparer.GetComparedObject(center), left, center) > 0)
                {
                    this.owner.SwapSortedRows(left, center);
                }
                if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(left), this.rowComparer.GetComparedObject(right), left, right) > 0)
                {
                    this.owner.SwapSortedRows(left, right);
                }
                if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(center), this.rowComparer.GetComparedObject(right), center, right) > 0)
                {
                    this.owner.SwapSortedRows(center, right);
                }
                return this.rowComparer.GetComparedObject(center);
            }
        }

        private class RowComparer
        {
            private bool ascending;
            private IComparer customComparer;
            private Grid grid;
            private GridRowCollection gridRows;
            private GridColumn gridSortedColumn;
            private static ComparedObjectMax max = new ComparedObjectMax();
            private int sortedColumnIndex;

            public RowComparer(GridRowCollection gridRows, IComparer customComparer, bool ascending)
            {
                this.grid = gridRows.Grid;
                this.gridRows = gridRows;
                this.gridSortedColumn = this.grid.SortedColumn;
                if (this.gridSortedColumn == null)
                {
                    this.sortedColumnIndex = -1;
                }
                else
                {
                    this.sortedColumnIndex = this.gridSortedColumn.Index;
                }
                this.customComparer = customComparer;
                this.ascending = ascending;
            }

            internal int CompareObjects(object value1, object value2, int rowIndex1, int rowIndex2)
            {
                if (value1 is ComparedObjectMax)
                {
                    return 1;
                }
                if (value2 is ComparedObjectMax)
                {
                    return -1;
                }
                int sortResult = 0;
                if (this.customComparer == null)
                {
                    if (!this.grid.OnSortCompare(this.gridSortedColumn, value1, value2, rowIndex1, rowIndex2, out sortResult))
                    {
                        if ((value1 is IComparable) || (value2 is IComparable))
                        {
                            sortResult = Comparer.Default.Compare(value1, value2);
                        }
                        else if (value1 == null)
                        {
                            if (value2 == null)
                            {
                                sortResult = 0;
                            }
                            else
                            {
                                sortResult = 1;
                            }
                        }
                        else if (value2 == null)
                        {
                            sortResult = -1;
                        }
                        else
                        {
                            sortResult = Comparer.Default.Compare(value1.ToString(), value2.ToString());
                        }
                        if (sortResult == 0)
                        {
                            if (this.ascending)
                            {
                                sortResult = rowIndex1 - rowIndex2;
                            }
                            else
                            {
                                sortResult = rowIndex2 - rowIndex1;
                            }
                        }
                    }
                }
                else
                {
                    sortResult = this.customComparer.Compare(value1, value2);
                }
                if (this.ascending)
                {
                    return sortResult;
                }
                return -sortResult;
            }

            internal object GetComparedObject(int rowIndex)
            {
                if ((this.grid.NewRowIndex != -1) && (rowIndex == this.grid.NewRowIndex))
                {
                    return max;
                }
                if (this.customComparer == null)
                {
                    return this.gridRows.SharedRow(rowIndex).Cells[this.sortedColumnIndex].GetValueInternal(rowIndex);
                }
                return this.gridRows[rowIndex];
            }

            private class ComparedObjectMax
            {
            }
        }

        private class UnsharingRowEnumerator : IEnumerator
        {
            private int current;
            private GridRowCollection owner;

            public UnsharingRowEnumerator(GridRowCollection owner)
            {
                this.owner = owner;
                this.current = -1;
            }

            bool IEnumerator.MoveNext()
            {
                if (this.current < (this.owner.Count - 1))
                {
                    this.current++;
                    return true;
                }
                this.current = this.owner.Count;
                return false;
            }

            void IEnumerator.Reset()
            {
                this.current = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    if (this.current == -1)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_EnumNotStarted"));
                    }
                    if (this.current == this.owner.Count)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowCollection_EnumFinished"));
                    }
                    return this.owner[this.current];
                }
            }
        }
    }
}

