namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Globalization;
    using System.Text;

    /// <summary>Represents a column of <see cref="T:MControl.GridView.GridComboBoxCell"></see> objects.</summary>
    /// <filterpriority>2</filterpriority>
    [ToolboxBitmap(typeof(GridComboBoxColumn), "GridComboBoxColumn.bmp"), Designer("System.Windows.Forms.Design.GridComboBoxColumnDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class GridComboBoxColumn : GridColumn
    {
        private static System.Type columnType = typeof(GridComboBoxColumn);

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridTextBoxColumn"></see> class to the default state.</summary>
        public GridComboBoxColumn() : base(new GridComboBoxCell())
        {
            ((GridComboBoxCell) base.CellTemplate).TemplateComboBoxColumn = this;
        }

        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridComboBoxColumn column;
            System.Type type = base.GetType();
            if (type == columnType)
            {
                column = new GridComboBoxColumn();
            }
            else
            {
                column = (GridComboBoxColumn) Activator.CreateInstance(type);
            }
            if (column != null)
            {
                base.CloneInternal(column);
                ((GridComboBoxCell) column.CellTemplate).TemplateComboBoxColumn = column;
            }
            return column;
        }

        internal void OnItemsCollectionChanged()
        {
            if (base.Grid != null)
            {
                GridRowCollection rows = base.Grid.Rows;
                int count = rows.Count;
                object[] items = ((GridComboBoxCell) this.CellTemplate).Items.InnerArray.ToArray();
                for (int i = 0; i < count; i++)
                {
                    GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                    if (cell != null)
                    {
                        cell.Items.ClearInternal();
                        cell.Items.AddRangeInternal(items);
                    }
                }
                base.Grid.OnColumnCommonChange(base.Index);
            }
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("GridComboBoxColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>Gets or sets a value indicating whether cells in the column will match the characters being entered in the cell with one from the possible selections. </summary>
        /// <returns>true if auto completion is activated; otherwise, false. The default is true.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(true), Browsable(true), Category("Behavior"), Description("Grid_ComboBoxColumnAutoComplete")]
        public bool AutoComplete
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.AutoComplete;
            }
            set
            {
                if (this.AutoComplete != value)
                {
                    this.ComboBoxCellTemplate.AutoComplete = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                            if (cell != null)
                            {
                                cell.AutoComplete = value;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Gets or sets the template used to create cells.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCell"></see> that all other cells in the column are modeled after. The default value is a new <see cref="T:MControl.GridView.GridComboBoxCell"></see>.</returns>
        /// <exception cref="T:System.InvalidCastException">When setting this property to a value that is not of type <see cref="T:MControl.GridView.GridComboBoxCell"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override GridCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                GridComboBoxCell cell = value as GridComboBoxCell;
                if ((value != null) && (cell == null))
                {
                    throw new InvalidCastException(MControl.GridView.RM.GetString("GridTypeColumn_WrongCellTemplateType", new object[] { "MControl.GridView.GridComboBoxCell" }));
                }
                base.CellTemplate = value;
                if (value != null)
                {
                    cell.TemplateComboBoxColumn = this;
                }
            }
        }

        private GridComboBoxCell ComboBoxCellTemplate
        {
            get
            {
                return (GridComboBoxCell) this.CellTemplate;
            }
        }

        /// <summary>Gets or sets the data source that populates the selections for the combo boxes.</summary>
        /// <returns>An object that represents a data source. The default is null.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.Repaint), AttributeProvider(typeof(IListSource)), Description("Grid_ComboBoxColumnDataSource")]
        public object DataSource
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.DataSource;
            }
            set
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                this.ComboBoxCellTemplate.DataSource = value;
                if (base.Grid != null)
                {
                    GridRowCollection rows = base.Grid.Rows;
                    int count = rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                        if (cell != null)
                        {
                            cell.DataSource = value;
                        }
                    }
                    base.Grid.OnColumnCommonChange(base.Index);
                }
            }
        }

        /// <summary>Gets or sets a string that specifies the property or column from which to retrieve strings for display in the combo boxes.</summary>
        /// <returns>A <see cref="T:System.String"></see> that specifies the name of a property or column in the data source specified in the <see cref="P:MControl.GridView.GridComboBoxColumn.DataSource"></see> property. The default is <see cref="F:System.String.Empty"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DefaultValue(""), Description("Grid_ComboBoxColumnDisplayMember"), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Data"), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string DisplayMember
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.DisplayMember;
            }
            set
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                this.ComboBoxCellTemplate.DisplayMember = value;
                if (base.Grid != null)
                {
                    GridRowCollection rows = base.Grid.Rows;
                    int count = rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                        if (cell != null)
                        {
                            cell.DisplayMember = value;
                        }
                    }
                    base.Grid.OnColumnCommonChange(base.Index);
                }
            }
        }

        /// <summary>Gets or sets a value that determines how the combo box is displayed when not editing.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridComboBoxDisplayStyle"></see> value indicating the combo box appearance. The default is <see cref="F:MControl.GridView.GridComboBoxDisplayStyle.DropDownButton"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null.</exception>
        [Category("Appearance"), Description("Grid_ComboBoxColumnDisplayStyle"), DefaultValue(1)]
        public GridComboBoxDisplayStyle DisplayStyle
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.DisplayStyle;
            }
            set
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                this.ComboBoxCellTemplate.DisplayStyle = value;
                if (base.Grid != null)
                {
                    GridRowCollection rows = base.Grid.Rows;
                    int count = rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                        if (cell != null)
                        {
                            cell.DisplayStyleInternal = value;
                        }
                    }
                    base.Grid.InvalidateColumn(base.Index);
                }
            }
        }

        /// <summary>Gets or sets a value that determines if the display style only applies to the current cell.</summary>
        /// <returns>true if the display style applies only to the current cell; otherwise false. The default is false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null.</exception>
        [Description("Grid_ComboBoxColumnDisplayStyleForCurrentCellOnly"), Category("Appearance"), DefaultValue(false)]
        public bool DisplayStyleForCurrentCellOnly
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.DisplayStyleForCurrentCellOnly;
            }
            set
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                this.ComboBoxCellTemplate.DisplayStyleForCurrentCellOnly = value;
                if (base.Grid != null)
                {
                    GridRowCollection rows = base.Grid.Rows;
                    int count = rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                        if (cell != null)
                        {
                            cell.DisplayStyleForCurrentCellOnlyInternal = value;
                        }
                    }
                    base.Grid.InvalidateColumn(base.Index);
                }
            }
        }

        /// <summary>Gets or sets the width of the drop-down lists of the combo boxes.</summary>
        /// <returns>The width, in pixels, of the drop-down lists. The default is 1.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Description("Grid_ComboBoxColumnDropDownWidth"), Category("Behavior"), DefaultValue(1)]
        public int DropDownWidth
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.DropDownWidth;
            }
            set
            {
                if (this.DropDownWidth != value)
                {
                    this.ComboBoxCellTemplate.DropDownWidth = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                            if (cell != null)
                            {
                                cell.DropDownWidth = value;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Gets or sets the flat style appearance of the column's cells.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.FlatStyle"></see> value indicating the cell appearance. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        [Category("Appearance"), Description("Grid_ComboBoxColumnFlatStyle"), DefaultValue(2)]
        public System.Windows.Forms.FlatStyle FlatStyle
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return ((GridComboBoxCell) this.CellTemplate).FlatStyle;
            }
            set
            {
                if (this.FlatStyle != value)
                {
                    ((GridComboBoxCell) this.CellTemplate).FlatStyle = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                            if (cell != null)
                            {
                                cell.FlatStyleInternal = value;
                            }
                        }
                        base.Grid.OnColumnCommonChange(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets the collection of objects used as selections in the combo boxes.</summary>
        /// <returns>An <see cref="T:MControl.GridView.GridComboBoxCell.ObjectCollection"></see> that represents the selections in the combo boxes. </returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Data"), Description("Grid_ComboBoxColumnItems")]
        public GridComboBoxCell.ObjectCollection Items
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.GetItems(base.Grid);
            }
        }

        /// <summary>Gets or sets the maximum number of items in the drop-down list of the cells in the column.</summary>
        /// <returns>The maximum number of drop-down list items, from 1 to 100. The default is 8.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        [Category("Behavior"), Description("Grid_ComboBoxColumnMaxDropDownItems"), DefaultValue(8)]
        public int MaxDropDownItems
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.MaxDropDownItems;
            }
            set
            {
                if (this.MaxDropDownItems != value)
                {
                    this.ComboBoxCellTemplate.MaxDropDownItems = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                            if (cell != null)
                            {
                                cell.MaxDropDownItems = value;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the items in the combo box are sorted.</summary>
        /// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DefaultValue(false), Category("Behavior"), Description("Grid_ComboBoxColumnSorted")]
        public bool Sorted
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.Sorted;
            }
            set
            {
                if (this.Sorted != value)
                {
                    this.ComboBoxCellTemplate.Sorted = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                            if (cell != null)
                            {
                                cell.Sorted = value;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Gets or sets a string that specifies the property or column from which to get values that correspond to the selections in the drop-down list.</summary>
        /// <returns>A <see cref="T:System.String"></see> that specifies the name of a property or column used in the <see cref="P:MControl.GridView.GridComboBoxColumn.DataSource"></see> property. The default is <see cref="F:System.String.Empty"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridComboBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DefaultValue(""), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Description("Grid_ComboBoxColumnValueMember"), Category("Data")]
        public string ValueMember
        {
            get
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ComboBoxCellTemplate.ValueMember;
            }
            set
            {
                if (this.ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                this.ComboBoxCellTemplate.ValueMember = value;
                if (base.Grid != null)
                {
                    GridRowCollection rows = base.Grid.Rows;
                    int count = rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        GridComboBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridComboBoxCell;
                        if (cell != null)
                        {
                            cell.ValueMember = value;
                        }
                    }
                    base.Grid.OnColumnCommonChange(base.Index);
                }
            }
        }
    }
}

