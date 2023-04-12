namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Globalization;
    using System.Text;

    /// <summary>Represents a column in a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    [ToolboxItem(false), DesignTimeVisible(false), TypeConverter(typeof(GridColumnConverter)), Designer("System.Windows.Forms.Design.GridColumnDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class GridColumn : GridBand, IComponent, IDisposable
    {
        private GridAutoSizeColumnMode autoSizeMode;
        private TypeConverter boundColumnConverter;
        private int boundColumnIndex;
        private GridCell cellTemplate;
        private const byte gridCOLUMN_automaticSort = 1;
        private const float gridCOLUMN_defaultFillWeight = 100f;
        private const int gridCOLUMN_defaultMinColumnThickness = 5;
        private const int gridCOLUMN_defaultWidth = 100;
        private const byte gridCOLUMN_displayIndexHasChangedInternal = 0x10;
        private const byte gridCOLUMN_isBrowsableInternal = 8;
        private const byte gridCOLUMN_isDataBound = 4;
        private const byte gridCOLUMN_programmaticSort = 2;
        private string dataPropertyName;
        private int desiredFillWidth;
        private int desiredMinimumWidth;
        private int displayIndex;
        private float fillWeight;
        private byte flags;
        private string name;
        private static readonly int PropGridColumnValueType = PropertyStore.CreateKey();
        private ISite site;
        private float usedFillWeight;

        /// <summary>Occurs when the <see cref="T:MControl.GridView.GridColumn"></see> is disposed.</summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler Disposed;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridColumn"></see> class to the default state.</summary>
        public GridColumn() : this(null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridColumn"></see> class using an existing <see cref="T:MControl.GridView.GridCell"></see> as a template.</summary>
        /// <param name="cellTemplate">An existing <see cref="T:MControl.GridView.GridCell"></see> to use as a template. </param>
        public GridColumn(GridCell cellTemplate)
        {
            this.boundColumnIndex = -1;
            this.dataPropertyName = string.Empty;
            this.fillWeight = 100f;
            this.usedFillWeight = 100f;
            base.Thickness = 100;
            base.MinimumThickness = 5;
            this.name = string.Empty;
            base.bandIsRow = false;
            this.displayIndex = -1;
            this.cellTemplate = cellTemplate;
            this.autoSizeMode = GridAutoSizeColumnMode.NotSet;
        }

        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridColumn gridColumn = (GridColumn) Activator.CreateInstance(base.GetType());
            if (gridColumn != null)
            {
                this.CloneInternal(gridColumn);
            }
            return gridColumn;
        }

        internal void CloneInternal(GridColumn gridColumn)
        {
            base.CloneInternal(gridColumn);
            gridColumn.name = this.Name;
            gridColumn.displayIndex = -1;
            gridColumn.HeaderText = this.HeaderText;
            gridColumn.DataPropertyName = this.DataPropertyName;
            if (gridColumn.CellTemplate != null)
            {
                gridColumn.cellTemplate = (GridCell) this.CellTemplate.Clone();
            }
            else
            {
                gridColumn.cellTemplate = null;
            }
            if (base.HasHeaderCell)
            {
                gridColumn.HeaderCell = (GridColumnHeaderCell) this.HeaderCell.Clone();
            }
            gridColumn.AutoSizeMode = this.AutoSizeMode;
            gridColumn.SortMode = this.SortMode;
            gridColumn.FillWeightInternal = this.FillWeight;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    lock (this)
                    {
                        if ((this.site != null) && (this.site.Container != null))
                        {
                            this.site.Container.Remove(this);
                        }
                        if (this.disposed != null)
                        {
                            this.disposed(this, EventArgs.Empty);
                        }
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        internal GridAutoSizeColumnMode GetInheritedAutoSizeMode(Grid grid)
        {
            if ((grid == null) || (this.autoSizeMode != GridAutoSizeColumnMode.NotSet))
            {
                return this.autoSizeMode;
            }
            switch (grid.AutoSizeColumnsMode)
            {
                case GridAutoSizeColumnsMode.ColumnHeader:
                    return GridAutoSizeColumnMode.ColumnHeader;

                case GridAutoSizeColumnsMode.AllCellsExceptHeader:
                    return GridAutoSizeColumnMode.AllCellsExceptHeader;

                case GridAutoSizeColumnsMode.AllCells:
                    return GridAutoSizeColumnMode.AllCells;

                case GridAutoSizeColumnsMode.DisplayedCellsExceptHeader:
                    return GridAutoSizeColumnMode.DisplayedCellsExceptHeader;

                case GridAutoSizeColumnsMode.DisplayedCells:
                    return GridAutoSizeColumnMode.DisplayedCells;

                case GridAutoSizeColumnsMode.Fill:
                    return GridAutoSizeColumnMode.Fill;
            }
            return GridAutoSizeColumnMode.None;
        }

        /// <summary>Calculates the ideal width of the column based on the specified criteria.</summary>
        /// <returns>The ideal width, in pixels, of the column.</returns>
        /// <param name="autoSizeColumnMode">A <see cref="T:MControl.GridView.GridAutoSizeColumnMode"></see> value that specifies an automatic sizing mode. </param>
        /// <param name="fixedHeight">true to calculate the width of the column based on the current row heights; false to calculate the width with the expectation that the row heights will be adjusted.</param>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">autoSizeColumnMode is not a valid <see cref="T:MControl.GridView.GridAutoSizeColumnMode"></see> value. </exception>
        /// <exception cref="T:System.ArgumentException">autoSizeColumnMode is <see cref="F:MControl.GridView.GridAutoSizeColumnMode.NotSet"></see>, <see cref="F:MControl.GridView.GridAutoSizeColumnMode.None"></see>, or <see cref="F:MControl.GridView.GridAutoSizeColumnMode.Fill"></see>. </exception>
        public virtual int GetPreferredWidth(GridAutoSizeColumnMode autoSizeColumnMode, bool fixedHeight)
        {
            if (((autoSizeColumnMode == GridAutoSizeColumnMode.NotSet) || (autoSizeColumnMode == GridAutoSizeColumnMode.None)) || (autoSizeColumnMode == GridAutoSizeColumnMode.Fill))
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_NeedColumnAutoSizingCriteria", new object[] { "autoSizeColumnMode" }));
            }
            switch (autoSizeColumnMode)
            {
                case GridAutoSizeColumnMode.NotSet:
                case GridAutoSizeColumnMode.None:
                case GridAutoSizeColumnMode.ColumnHeader:
                case GridAutoSizeColumnMode.AllCellsExceptHeader:
                case GridAutoSizeColumnMode.AllCells:
                case GridAutoSizeColumnMode.DisplayedCellsExceptHeader:
                case GridAutoSizeColumnMode.DisplayedCells:
                case GridAutoSizeColumnMode.Fill:
                {
                    int preferredWidth;
                    int num3;
                    GridRow row;
                    Grid grid = base.Grid;
                    if (grid == null)
                    {
                        return -1;
                    }
                    GridAutoSizeColumnCriteriaInternal internal2 = (GridAutoSizeColumnCriteriaInternal) autoSizeColumnMode;
                    int num = 0;
                    if (grid.ColumnHeadersVisible && ((internal2 & GridAutoSizeColumnCriteriaInternal.Header) != GridAutoSizeColumnCriteriaInternal.NotSet))
                    {
                        if (fixedHeight)
                        {
                            preferredWidth = this.HeaderCell.GetPreferredWidth(-1, grid.ColumnHeadersHeight);
                        }
                        else
                        {
                            preferredWidth = this.HeaderCell.GetPreferredSize(-1).Width;
                        }
                        if (num < preferredWidth)
                        {
                            num = preferredWidth;
                        }
                    }
                    if ((internal2 & GridAutoSizeColumnCriteriaInternal.AllRows) != GridAutoSizeColumnCriteriaInternal.NotSet)
                    {
                        for (num3 = grid.Rows.GetFirstRow(GridElementStates.Visible); num3 != -1; num3 = grid.Rows.GetNextRow(num3, GridElementStates.Visible))
                        {
                            row = grid.Rows.SharedRow(num3);
                            if (fixedHeight)
                            {
                                preferredWidth = row.Cells[base.Index].GetPreferredWidth(num3, row.Thickness);
                            }
                            else
                            {
                                preferredWidth = row.Cells[base.Index].GetPreferredSize(num3).Width;
                            }
                            if (num < preferredWidth)
                            {
                                num = preferredWidth;
                            }
                        }
                        return num;
                    }
                    if ((internal2 & GridAutoSizeColumnCriteriaInternal.DisplayedRows) != GridAutoSizeColumnCriteriaInternal.NotSet)
                    {
                        int height = grid.LayoutInfo.Data.Height;
                        int num5 = 0;
                        for (num3 = grid.Rows.GetFirstRow(GridElementStates.Visible | GridElementStates.Frozen); (num3 != -1) && (num5 < height); num3 = grid.Rows.GetNextRow(num3, GridElementStates.Visible | GridElementStates.Frozen))
                        {
                            row = grid.Rows.SharedRow(num3);
                            if (fixedHeight)
                            {
                                preferredWidth = row.Cells[base.Index].GetPreferredWidth(num3, row.Thickness);
                            }
                            else
                            {
                                preferredWidth = row.Cells[base.Index].GetPreferredSize(num3).Width;
                            }
                            if (num < preferredWidth)
                            {
                                num = preferredWidth;
                            }
                            num5 += row.Thickness;
                        }
                        if (num5 >= height)
                        {
                            return num;
                        }
                        for (num3 = grid.DisplayedBandsInfo.FirstDisplayedScrollingRow; (num3 != -1) && (num5 < height); num3 = grid.Rows.GetNextRow(num3, GridElementStates.Visible))
                        {
                            row = grid.Rows.SharedRow(num3);
                            if (fixedHeight)
                            {
                                preferredWidth = row.Cells[base.Index].GetPreferredWidth(num3, row.Thickness);
                            }
                            else
                            {
                                preferredWidth = row.Cells[base.Index].GetPreferredSize(num3).Width;
                            }
                            if (num < preferredWidth)
                            {
                                num = preferredWidth;
                            }
                            num5 += row.Thickness;
                        }
                    }
                    return num;
                }
            }
            throw new InvalidEnumArgumentException("value", (int) autoSizeColumnMode, typeof(GridAutoSizeColumnMode));
        }

        private bool ShouldSerializeDefaultCellStyle()
        {
            if (!base.HasDefaultCellStyle)
            {
                return false;
            }
            GridCellStyle defaultCellStyle = this.DefaultCellStyle;
            if ((((defaultCellStyle.BackColor.IsEmpty && defaultCellStyle.ForeColor.IsEmpty) && (defaultCellStyle.SelectionBackColor.IsEmpty && defaultCellStyle.SelectionForeColor.IsEmpty)) && (((defaultCellStyle.Font == null) && defaultCellStyle.IsNullValueDefault) && (defaultCellStyle.IsDataSourceNullValueDefault && string.IsNullOrEmpty(defaultCellStyle.Format)))) && ((defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) && (defaultCellStyle.Alignment == GridContentAlignment.NotSet)) && ((defaultCellStyle.WrapMode == GridTriState.NotSet) && (defaultCellStyle.Tag == null))))
            {
                return !defaultCellStyle.Padding.Equals(Padding.Empty);
            }
            return true;
        }

        private bool ShouldSerializeHeaderText()
        {
            return (base.HasHeaderCell && this.HeaderCell.ContainsLocalValue);
        }

        /// <summary>Gets a string that describes the column.</summary>
        /// <returns>A <see cref="T:System.String"></see> that describes the column.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("GridColumn { Name=");
            builder.Append(this.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>Gets or sets the mode by which the column automatically adjusts its width.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridAutoSizeColumnMode"></see> value that determines whether the column will automatically adjust its width and how it will determine its preferred width. The default is <see cref="F:MControl.GridView.GridAutoSizeColumnMode.NotSet"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The specified value when setting this property results in an <see cref="P:MControl.GridView.GridColumn.InheritedAutoSizeMode"></see> value of <see cref="F:MControl.GridView.GridAutoSizeColumnMode.ColumnHeader"></see> for a visible column when column headers are hidden.-or-The specified value when setting this property results in an <see cref="P:MControl.GridView.GridColumn.InheritedAutoSizeMode"></see> value of <see cref="F:MControl.GridView.GridAutoSizeColumnMode.Fill"></see> for a visible column that is frozen.</exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is a <see cref="T:MControl.GridView.GridAutoSizeColumnMode"></see> that is not valid. </exception>
        [Category("Layout"), DefaultValue(0), Description("GridColumn_AutoSizeMode"), RefreshProperties(RefreshProperties.Repaint)]
        public GridAutoSizeColumnMode AutoSizeMode
        {
            get
            {
                return this.autoSizeMode;
            }
            set
            {
                switch (value)
                {
                    case GridAutoSizeColumnMode.NotSet:
                    case GridAutoSizeColumnMode.None:
                    case GridAutoSizeColumnMode.ColumnHeader:
                    case GridAutoSizeColumnMode.AllCellsExceptHeader:
                    case GridAutoSizeColumnMode.AllCells:
                    case GridAutoSizeColumnMode.DisplayedCellsExceptHeader:
                    case GridAutoSizeColumnMode.DisplayedCells:
                    case GridAutoSizeColumnMode.Fill:
                        if (this.autoSizeMode != value)
                        {
                            if (this.Visible && (base.Grid != null))
                            {
                                if (!base.Grid.ColumnHeadersVisible && ((value == GridAutoSizeColumnMode.ColumnHeader) || ((value == GridAutoSizeColumnMode.NotSet) && (base.Grid.AutoSizeColumnsMode == GridAutoSizeColumnsMode.ColumnHeader))))
                                {
                                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_AutoSizeCriteriaCannotUseInvisibleHeaders"));
                                }
                                if (this.Frozen && ((value == GridAutoSizeColumnMode.Fill) || ((value == GridAutoSizeColumnMode.NotSet) && (base.Grid.AutoSizeColumnsMode == GridAutoSizeColumnsMode.Fill))))
                                {
                                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_FrozenColumnCannotAutoFill"));
                                }
                            }
                            GridAutoSizeColumnMode inheritedAutoSizeMode = this.InheritedAutoSizeMode;
                            bool flag = ((inheritedAutoSizeMode != GridAutoSizeColumnMode.Fill) && (inheritedAutoSizeMode != GridAutoSizeColumnMode.None)) && (inheritedAutoSizeMode != GridAutoSizeColumnMode.NotSet);
                            this.autoSizeMode = value;
                            if (base.Grid == null)
                            {
                                if (((this.InheritedAutoSizeMode == GridAutoSizeColumnMode.Fill) || (this.InheritedAutoSizeMode == GridAutoSizeColumnMode.None)) || (this.InheritedAutoSizeMode == GridAutoSizeColumnMode.NotSet))
                                {
                                    if ((base.Thickness != base.CachedThickness) && flag)
                                    {
                                        base.ThicknessInternal = base.CachedThickness;
                                        return;
                                    }
                                }
                                else if (!flag)
                                {
                                    base.CachedThickness = base.Thickness;
                                    return;
                                }
                            }
                            else
                            {
                                base.Grid.OnAutoSizeColumnModeChanged(this, inheritedAutoSizeMode);
                            }
                        }
                        return;
                }
                throw new InvalidEnumArgumentException("value", (int) value, typeof(GridAutoSizeColumnMode));
            }
        }

        internal TypeConverter BoundColumnConverter
        {
            get
            {
                return this.boundColumnConverter;
            }
            set
            {
                this.boundColumnConverter = value;
            }
        }

        internal int BoundColumnIndex
        {
            get
            {
                return this.boundColumnIndex;
            }
            set
            {
                this.boundColumnIndex = value;
            }
        }

        /// <summary>Gets or sets the template used to create new cells.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCell"></see> that all other cells in the column are modeled after. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public virtual GridCell CellTemplate
        {
            get
            {
                return this.cellTemplate;
            }
            set
            {
                this.cellTemplate = value;
            }
        }

        /// <summary>Gets the run-time type of the cell template.</summary>
        /// <returns>The <see cref="T:System.Type"></see> of the <see cref="T:MControl.GridView.GridCell"></see> used as a template for this column. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public System.Type CellType
        {
            get
            {
                if (this.cellTemplate != null)
                {
                    return this.cellTemplate.GetType();
                }
                return null;
            }
        }

        /// <summary>Gets or sets the shortcut menu for the column.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> associated with the current <see cref="T:MControl.GridView.GridColumn"></see>. The default is null.</returns>
        [Description("Grid_ColumnContextMenuStrip"), Category("Behavior"), DefaultValue((string) null)]
        public override System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
            }
        }

        /// <summary>Gets or sets the name of the data source property or database column to which the <see cref="T:MControl.GridView.GridColumn"></see> is bound.</summary>
        /// <returns>The name of the property or database column associated with the <see cref="T:MControl.GridView.GridColumn"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultValue(""), Category("Data"), Description("Grid_ColumnDataPropertyName"), Browsable(true), Editor("System.Windows.Forms.Design.GridColumnDataPropertyNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string DataPropertyName
        {
            get
            {
                return this.dataPropertyName;
            }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                if (value != this.dataPropertyName)
                {
                    this.dataPropertyName = value;
                    if (base.Grid != null)
                    {
                        base.Grid.OnColumnDataPropertyNameChanged(this);
                    }
                }
            }
        }

        /// <summary>Gets or sets the column's default cell style.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the default style of the cells in the column.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Category("Appearance"), Description("Grid_ColumnDefaultCellStyle"), Browsable(true)]
        public override GridCellStyle DefaultCellStyle
        {
            get
            {
                return base.DefaultCellStyle;
            }
            set
            {
                base.DefaultCellStyle = value;
            }
        }

        internal int DesiredFillWidth
        {
            get
            {
                return this.desiredFillWidth;
            }
            set
            {
                this.desiredFillWidth = value;
            }
        }

        internal int DesiredMinimumWidth
        {
            get
            {
                return this.desiredMinimumWidth;
            }
            set
            {
                this.desiredMinimumWidth = value;
            }
        }

        /// <summary>Gets or sets the display order of the column relative to the currently displayed columns.</summary>
        /// <returns>The zero-based position of the column as it is displayed in the associated <see cref="T:MControl.GridView.Grid"></see>, or -1 if the band is not contained within a control. </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><see cref="P:MControl.GridView.GridElement.Grid"></see> is not null and the specified value when setting this property is less than 0 or greater than or equal to the number of columns in the control.-or-<see cref="P:MControl.GridView.GridElement.Grid"></see> is null and the specified value when setting this property is less than -1.-or-The specified value when setting this property is equal to <see cref="F:System.Int32.MaxValue"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int DisplayIndex
        {
            get
            {
                return this.displayIndex;
            }
            set
            {
                if (this.displayIndex != value)
                {
                    if (value == 0x7fffffff)
                    {
                        object[] args = new object[] { 0x7fffffff.ToString(CultureInfo.CurrentCulture) };
                        throw new ArgumentOutOfRangeException("DisplayIndex", value, MControl.GridView.RM.GetString("GridColumn_DisplayIndexTooLarge", args));
                    }
                    if (base.Grid != null)
                    {
                        if (value < 0)
                        {
                            throw new ArgumentOutOfRangeException("DisplayIndex", value, MControl.GridView.RM.GetString("GridColumn_DisplayIndexNegative"));
                        }
                        if (value >= base.Grid.Columns.Count)
                        {
                            throw new ArgumentOutOfRangeException("DisplayIndex", value, MControl.GridView.RM.GetString("GridColumn_DisplayIndexExceedsColumnCount"));
                        }
                        base.Grid.OnColumnDisplayIndexChanging(this, value);
                        this.displayIndex = value;
                        try
                        {
                            base.Grid.InDisplayIndexAdjustments = true;
                            base.Grid.OnColumnDisplayIndexChanged_PreNotification();
                            base.Grid.OnColumnDisplayIndexChanged(this);
                            base.Grid.OnColumnDisplayIndexChanged_PostNotification();
                            return;
                        }
                        finally
                        {
                            base.Grid.InDisplayIndexAdjustments = false;
                        }
                    }
                    if (value < -1)
                    {
                        throw new ArgumentOutOfRangeException("DisplayIndex", value, MControl.GridView.RM.GetString("GridColumn_DisplayIndexTooNegative"));
                    }
                    this.displayIndex = value;
                }
            }
        }

        internal bool DisplayIndexHasChanged
        {
            get
            {
                return ((this.flags & 0x10) != 0);
            }
            set
            {
                if (value)
                {
                    this.flags = (byte) (this.flags | 0x10);
                }
                else
                {
                    this.flags = (byte) (this.flags & -17);
                }
            }
        }

        internal int DisplayIndexInternal
        {
            set
            {
                this.displayIndex = value;
            }
        }

        /// <summary>Gets or sets the width, in pixels, of the column divider.</summary>
        /// <returns>The thickness, in pixels, of the divider (the column's right margin). </returns>
        [Category("Layout"), Description("Grid_ColumnDividerWidth"), DefaultValue(0)]
        public int DividerWidth
        {
            get
            {
                return base.DividerThickness;
            }
            set
            {
                base.DividerThickness = value;
            }
        }

        /// <summary>Gets or sets a value that represents the width of the column when it is in fill mode relative to the widths of other fill-mode columns in the control.</summary>
        /// <returns>A <see cref="T:System.Single"></see> representing the width of the column when it is in fill mode relative to the widths of other fill-mode columns. The default is 100.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than or equal to 0. </exception>
        [DefaultValue((float) 100f), Category("Layout"), Description("GridColumn_FillWeight")]
        public float FillWeight
        {
            get
            {
                return this.fillWeight;
            }
            set
            {
                if (value <= 0f)
                {
                    object[] args = new object[] { "FillWeight", value.ToString(CultureInfo.CurrentCulture), 0.ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentOutOfRangeException("FillWeight", MControl.GridView.RM.GetString("InvalidLowBoundArgument", args));
                }
                if (value > 65535f)
                {
                    object[] objArray2 = new object[] { "FillWeight", value.ToString(CultureInfo.CurrentCulture), ((ushort) 0xffff).ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentOutOfRangeException("FillWeight", MControl.GridView.RM.GetString("InvalidHighBoundArgumentEx", objArray2));
                }
                if (base.Grid != null)
                {
                    base.Grid.OnColumnFillWeightChanging(this, value);
                    this.fillWeight = value;
                    base.Grid.OnColumnFillWeightChanged(this);
                }
                else
                {
                    this.fillWeight = value;
                }
            }
        }

        internal float FillWeightInternal
        {
            set
            {
                this.fillWeight = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether a column will move when a user scrolls the <see cref="T:MControl.GridView.Grid"></see> control horizontally.</summary>
        /// <returns>true to freeze the column; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [RefreshProperties(RefreshProperties.All), Category("Layout"), DefaultValue(false), Description("Grid_ColumnFrozen")]
        public override bool Frozen
        {
            get
            {
                return base.Frozen;
            }
            set
            {
                base.Frozen = value;
            }
        }

        /// <summary>Gets or sets the <see cref="T:MControl.GridView.GridColumnHeaderCell"></see> that represents the column header.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridColumnHeaderCell"></see> that represents the header cell for the column.</returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public GridColumnHeaderCell HeaderCell
        {
            get
            {
                return (GridColumnHeaderCell) base.HeaderCellCore;
            }
            set
            {
                base.HeaderCellCore = value;
            }
        }

        /// <summary>Gets or sets the caption text on the column's header cell.</summary>
        /// <returns>A <see cref="T:System.String"></see> with the desired text. The default is an empty string ("").</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Category("Appearance"), Description("Grid_ColumnHeaderText"), Localizable(true)]
        public string HeaderText
        {
            get
            {
                if (base.HasHeaderCell)
                {
                    string str = this.HeaderCell.Value as string;
                    if (str != null)
                    {
                        return str;
                    }
                }
                return string.Empty;
            }
            set
            {
                if (((value != null) || base.HasHeaderCell) && ((this.HeaderCell.ValueType != null) && this.HeaderCell.ValueType.IsAssignableFrom(typeof(string))))
                {
                    this.HeaderCell.Value = value;
                }
            }
        }

        /// <summary>Gets the sizing mode in effect for the column.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridAutoSizeColumnMode"></see> value in effect for the column.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public GridAutoSizeColumnMode InheritedAutoSizeMode
        {
            get
            {
                return this.GetInheritedAutoSizeMode(base.Grid);
            }
        }

        /// <summary>Gets the cell style currently applied to the column.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the cell style used to display the column.</returns>
        [Browsable(false)]
        public override GridCellStyle InheritedStyle
        {
            get
            {
                GridCellStyle style = null;
                if (base.HasDefaultCellStyle)
                {
                    style = this.DefaultCellStyle;
                }
                if (base.Grid == null)
                {
                    return style;
                }
                GridCellStyle style2 = new GridCellStyle();
                GridCellStyle defaultCellStyle = base.Grid.DefaultCellStyle;
                if ((style != null) && !style.BackColor.IsEmpty)
                {
                    style2.BackColor = style.BackColor;
                }
                else
                {
                    style2.BackColor = defaultCellStyle.BackColor;
                }
                if ((style != null) && !style.ForeColor.IsEmpty)
                {
                    style2.ForeColor = style.ForeColor;
                }
                else
                {
                    style2.ForeColor = defaultCellStyle.ForeColor;
                }
                if ((style != null) && !style.SelectionBackColor.IsEmpty)
                {
                    style2.SelectionBackColor = style.SelectionBackColor;
                }
                else
                {
                    style2.SelectionBackColor = defaultCellStyle.SelectionBackColor;
                }
                if ((style != null) && !style.SelectionForeColor.IsEmpty)
                {
                    style2.SelectionForeColor = style.SelectionForeColor;
                }
                else
                {
                    style2.SelectionForeColor = defaultCellStyle.SelectionForeColor;
                }
                if ((style != null) && (style.Font != null))
                {
                    style2.Font = style.Font;
                }
                else
                {
                    style2.Font = defaultCellStyle.Font;
                }
                if ((style != null) && !style.IsNullValueDefault)
                {
                    style2.NullValue = style.NullValue;
                }
                else
                {
                    style2.NullValue = defaultCellStyle.NullValue;
                }
                if ((style != null) && !style.IsDataSourceNullValueDefault)
                {
                    style2.DataSourceNullValue = style.DataSourceNullValue;
                }
                else
                {
                    style2.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
                }
                if ((style != null) && (style.Format.Length != 0))
                {
                    style2.Format = style.Format;
                }
                else
                {
                    style2.Format = defaultCellStyle.Format;
                }
                if ((style != null) && !style.IsFormatProviderDefault)
                {
                    style2.FormatProvider = style.FormatProvider;
                }
                else
                {
                    style2.FormatProvider = defaultCellStyle.FormatProvider;
                }
                if ((style != null) && (style.Alignment != GridContentAlignment.NotSet))
                {
                    style2.AlignmentInternal = style.Alignment;
                }
                else
                {
                    style2.AlignmentInternal = defaultCellStyle.Alignment;
                }
                if ((style != null) && (style.WrapMode != GridTriState.NotSet))
                {
                    style2.WrapModeInternal = style.WrapMode;
                }
                else
                {
                    style2.WrapModeInternal = defaultCellStyle.WrapMode;
                }
                if ((style != null) && (style.Tag != null))
                {
                    style2.Tag = style.Tag;
                }
                else
                {
                    style2.Tag = defaultCellStyle.Tag;
                }
                if ((style != null) && (style.Padding != Padding.Empty))
                {
                    style2.PaddingInternal = style.Padding;
                    return style2;
                }
                style2.PaddingInternal = defaultCellStyle.Padding;
                return style2;
            }
        }

        internal bool IsBrowsableInternal
        {
            get
            {
                return ((this.flags & 8) != 0);
            }
            set
            {
                if (value)
                {
                    this.flags = (byte) (this.flags | 8);
                }
                else
                {
                    this.flags = (byte) (this.flags & -9);
                }
            }
        }

        /// <summary>Gets a value indicating whether the column is bound to a data source.</summary>
        /// <returns>true if the column is connected to a data source; otherwise, false.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsDataBound
        {
            get
            {
                return this.IsDataBoundInternal;
            }
        }

        internal bool IsDataBoundInternal
        {
            get
            {
                return ((this.flags & 4) != 0);
            }
            set
            {
                if (value)
                {
                    this.flags = (byte) (this.flags | 4);
                }
                else
                {
                    this.flags = (byte) (this.flags & -5);
                }
            }
        }

        /// <summary>Gets or sets the minimum width, in pixels, of the column.</summary>
        /// <returns>The number of pixels, from 2 to <see cref="F:System.Int32.MaxValue"></see>, that specifies the minimum width of the column. The default is 5.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 2 or greater than <see cref="F:System.Int32.MaxValue"></see>.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DefaultValue(5), Description("Grid_ColumnMinimumWidth"), Localizable(true), Category("Layout"), RefreshProperties(RefreshProperties.Repaint)]
        public int MinimumWidth
        {
            get
            {
                return base.MinimumThickness;
            }
            set
            {
                base.MinimumThickness = value;
            }
        }

        /// <summary>Gets or sets the name of the column.</summary>
        /// <returns>A <see cref="T:System.String"></see> that contains the name of the column. The default is an empty string ("").</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public string Name
        {
            get
            {
                if ((this.Site != null) && !string.IsNullOrEmpty(this.Site.Name))
                {
                    this.name = this.Site.Name;
                }
                return this.name;
            }
            set
            {
                string name = this.name;
                if (string.IsNullOrEmpty(value))
                {
                    this.name = string.Empty;
                }
                else
                {
                    this.name = value;
                }
                if ((base.Grid != null) && !string.Equals(this.name, name, StringComparison.Ordinal))
                {
                    base.Grid.OnColumnNameChanged(this);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the user can edit the column's cells.</summary>
        /// <returns>true if the user cannot edit the column's cells; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">This property is set to false for a column that is bound to a read-only data source. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Description("Grid_ColumnReadOnly"), Category("Behavior")]
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                if (((this.IsDataBound && (base.Grid != null)) && ((base.Grid.DataConnection != null) && (this.boundColumnIndex != -1))) && (base.Grid.DataConnection.DataFieldIsReadOnly(this.boundColumnIndex) && !value))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_ColumnBoundToAReadOnlyFieldMustRemainReadOnly"));
                }
                base.ReadOnly = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the column is resizable.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridTriState"></see> values. The default is <see cref="F:MControl.GridView.GridTriState.True"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Category("Behavior"), Description("Grid_ColumnResizable")]
        public override GridTriState Resizable
        {
            get
            {
                return base.Resizable;
            }
            set
            {
                base.Resizable = value;
            }
        }

        /// <summary>Gets or sets the site of the column.</summary>
        /// <returns>The <see cref="T:System.ComponentModel.ISite"></see> associated with the column, if any.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public ISite Site
        {
            get
            {
                return this.site;
            }
            set
            {
                this.site = value;
            }
        }

        /// <summary>Gets or sets the sort mode for the column.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridColumnSortMode"></see> that specifies the criteria used to order the rows based on the cell values in a column.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value assigned to the property conflicts with <see cref="P:MControl.GridView.Grid.SelectionMode"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Description("Grid_ColumnSortMode"), DefaultValue(0), Category("Behavior")]
        public GridColumnSortMode SortMode
        {
            get
            {
                if ((this.flags & 1) != 0)
                {
                    return GridColumnSortMode.Automatic;
                }
                if ((this.flags & 2) != 0)
                {
                    return GridColumnSortMode.Programmatic;
                }
                return GridColumnSortMode.NotSortable;
            }
            set
            {
                if (value != this.SortMode)
                {
                    if (value != GridColumnSortMode.NotSortable)
                    {
                        if ((((base.Grid != null) && !base.Grid.InInitialization) && (value == GridColumnSortMode.Automatic)) && ((base.Grid.SelectionMode == GridSelectionMode.FullColumnSelect) || (base.Grid.SelectionMode == GridSelectionMode.ColumnHeaderSelect)))
                        {
                            throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_SortModeAndSelectionModeClash", new object[] { value.ToString(), base.Grid.SelectionMode.ToString() }));
                        }
                        if (value == GridColumnSortMode.Automatic)
                        {
                            this.flags = (byte) (this.flags & -3);
                            this.flags = (byte) (this.flags | 1);
                        }
                        else
                        {
                            this.flags = (byte) (this.flags & -2);
                            this.flags = (byte) (this.flags | 2);
                        }
                    }
                    else
                    {
                        this.flags = (byte) (this.flags & -2);
                        this.flags = (byte) (this.flags & -3);
                    }
                    if (base.Grid != null)
                    {
                        base.Grid.OnColumnSortModeChanged(this);
                    }
                }
            }
        }

        /// <summary>Gets or sets the text used for ToolTips.</summary>
        /// <returns>The text to display as a ToolTip for the column.</returns>
        /// <filterpriority>1</filterpriority>
        [Localizable(true), Category("Appearance"), Description("Grid_ColumnToolTipText"), DefaultValue("")]
        public string ToolTipText
        {
            get
            {
                return this.HeaderCell.ToolTipText;
            }
            set
            {
                if (string.Compare(this.ToolTipText, value, false, CultureInfo.InvariantCulture) != 0)
                {
                    this.HeaderCell.ToolTipText = value;
                    if (base.Grid != null)
                    {
                        base.Grid.OnColumnToolTipTextChanged(this);
                    }
                }
            }
        }

        internal float UsedFillWeight
        {
            get
            {
                return this.usedFillWeight;
            }
            set
            {
                this.usedFillWeight = value;
            }
        }

        /// <summary>Gets or sets the data type of the values in the column's cells.</summary>
        /// <returns>A <see cref="T:System.Type"></see> that describes the run-time class of the values stored in the column's cells.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue((string) null), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Type ValueType
        {
            get
            {
                return (System.Type) base.Properties.GetObject(PropGridColumnValueType);
            }
            set
            {
                base.Properties.SetObject(PropGridColumnValueType, value);
            }
        }

        /// <summary>Gets or sets a value indicating whether the column is visible.</summary>
        /// <returns>true if the column is visible; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Description("Grid_ColumnVisible"), Localizable(true), Category("Appearance"), DefaultValue(true)]
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        /// <summary>Gets or sets the current width of the column.</summary>
        /// <returns>The width, in pixels, of the column. The default is 100.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is greater than 65536.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Description("Grid_ColumnWidth"), Localizable(true), Category("Layout"), RefreshProperties(RefreshProperties.Repaint)]
        public int Width
        {
            get
            {
                return base.Thickness;
            }
            set
            {
                base.Thickness = value;
            }
        }
    }
}

