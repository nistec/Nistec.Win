namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using MControl.Util;

    /// <summary>Represents a linear collection of elements in a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridBand : GridElement, ICloneable, IDisposable
    {
        private int bandIndex = -1;
        internal bool bandIsRow;
        private int cachedThickness;
        internal const int maxBandThickness = 0x10000;
        internal const int minBandThickness = 2;
        private int minimumThickness;
        private static readonly int PropContextMenuStrip = PropertyStore.CreateKey();
        private static readonly int PropDefaultCellStyle = PropertyStore.CreateKey();
        private static readonly int PropDefaultHeaderCellType = PropertyStore.CreateKey();
        private static readonly int PropDividerThickness = PropertyStore.CreateKey();
        private PropertyStore propertyStore = new PropertyStore();
        private static readonly int PropHeaderCell = PropertyStore.CreateKey();
        private static readonly int PropUserData = PropertyStore.CreateKey();
        private int thickness;

        internal GridBand()
        {
        }

        /// <summary>Creates an exact copy of this band.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridBand"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual object Clone()
        {
            GridBand gridBand = (GridBand) Activator.CreateInstance(base.GetType());
            if (gridBand != null)
            {
                this.CloneInternal(gridBand);
            }
            return gridBand;
        }

        internal void CloneInternal(GridBand gridBand)
        {
            gridBand.propertyStore = new PropertyStore();
            gridBand.bandIndex = -1;
            gridBand.bandIsRow = this.bandIsRow;
            if ((!this.bandIsRow || (this.bandIndex >= 0)) || (base.Grid == null))
            {
                gridBand.StateInternal = this.State & ~(GridElementStates.Selected | GridElementStates.Displayed);
            }
            gridBand.thickness = this.Thickness;
            gridBand.MinimumThickness = this.MinimumThickness;
            gridBand.cachedThickness = this.CachedThickness;
            gridBand.DividerThickness = this.DividerThickness;
            gridBand.Tag = this.Tag;
            if (this.HasDefaultCellStyle)
            {
                gridBand.DefaultCellStyle = new GridCellStyle(this.DefaultCellStyle);
            }
            if (this.HasDefaultHeaderCellType)
            {
                gridBand.DefaultHeaderCellType = this.DefaultHeaderCellType;
            }
            if (this.ContextMenuStripInternal != null)
            {
                gridBand.ContextMenuStrip = this.ContextMenuStripInternal.Clone();
            }
        }

        private void DetachContextMenuStrip(object sender, EventArgs e)
        {
            this.ContextMenuStripInternal = null;
        }

        /// <summary>Releases all resources used by the <see cref="T:MControl.GridView.GridBand"></see>.  </summary>
        /// <filterpriority>1</filterpriority>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases the unmanaged resources used by the <see cref="T:MControl.GridView.GridBand"></see> and optionally releases the managed resources.  </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                System.Windows.Forms.ContextMenuStrip contextMenuStripInternal = this.ContextMenuStripInternal;
                if (contextMenuStripInternal != null)
                {
                    contextMenuStripInternal.Disposed -= new EventHandler(this.DetachContextMenuStrip);
                }
            }
        }

        /// <summary>Releases the resources associated with the band.</summary>
        ~GridBand()
        {
            this.Dispose(false);
        }

        internal void GetHeightInfo(int rowIndex, out int height, out int minimumHeight)
        {
            if (((base.Grid != null) && (base.Grid.VirtualMode || (base.Grid.DataSource != null))) && (base.Grid.AutoSizeRowsMode == GridAutoSizeRowsMode.None))
            {
                GridRowHeightInfoNeededEventArgs args = base.Grid.OnRowHeightInfoNeeded(rowIndex, this.thickness, this.minimumThickness);
                height = args.Height;
                minimumHeight = args.MinimumHeight;
            }
            else
            {
                height = this.thickness;
                minimumHeight = this.minimumThickness;
            }
        }

        /// <summary>Called when the band is associated with a different <see cref="T:MControl.GridView.Grid"></see>.</summary>
        protected override void OnGridChanged()
        {
            if (this.HasDefaultCellStyle)
            {
                if (base.Grid == null)
                {
                    this.DefaultCellStyle.RemoveScope(this.bandIsRow ? GridCellStyleScopes.Row : GridCellStyleScopes.Column);
                }
                else
                {
                    this.DefaultCellStyle.AddScope(base.Grid, this.bandIsRow ? GridCellStyleScopes.Row : GridCellStyleScopes.Column);
                }
            }
            base.OnGridChanged();
        }

        internal void OnStateChanged(GridElementStates elementState)
        {
            if (base.Grid != null)
            {
                if (this.bandIsRow)
                {
                    base.Grid.Rows.InvalidateCachedRowCount(elementState);
                    base.Grid.Rows.InvalidateCachedRowsHeight(elementState);
                    if (this.bandIndex != -1)
                    {
                        base.Grid.OnGridElementStateChanged(this, -1, elementState);
                    }
                }
                else
                {
                    base.Grid.Columns.InvalidateCachedColumnCount(elementState);
                    base.Grid.Columns.InvalidateCachedColumnsWidth(elementState);
                    base.Grid.OnGridElementStateChanged(this, -1, elementState);
                }
            }
        }

        private void OnStateChanging(GridElementStates elementState)
        {
            if (base.Grid != null)
            {
                if (this.bandIsRow)
                {
                    if (this.bandIndex != -1)
                    {
                        base.Grid.OnGridElementStateChanging(this, -1, elementState);
                    }
                }
                else
                {
                    base.Grid.OnGridElementStateChanging(this, -1, elementState);
                }
            }
        }

        private bool ShouldSerializeDefaultHeaderCellType()
        {
            System.Type type = (System.Type) this.Properties.GetObject(PropDefaultHeaderCellType);
            return (type != null);
        }

        internal bool ShouldSerializeResizable()
        {
            return ((this.State & GridElementStates.ResizableSet) != GridElementStates.None);
        }

        /// <summary>Returns a string that represents the current band.</summary>
        /// <returns>A <see cref="T:System.String"></see> that represents the current <see cref="T:MControl.GridView.GridBand"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x24);
            builder.Append("GridBand { Index=");
            builder.Append(this.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        internal int CachedThickness
        {
            get
            {
                return this.cachedThickness;
            }
            set
            {
                this.cachedThickness = value;
            }
        }

        /// <summary>Gets or sets the shortcut menu for the band.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> associated with the current <see cref="T:MControl.GridView.GridBand"></see>. The default is null.</returns>
        [DefaultValue((string) null)]
        public virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get
            {
                if (this.bandIsRow)
                {
                    return ((GridRow) this).GetContextMenuStrip(this.Index);
                }
                return this.ContextMenuStripInternal;
            }
            set
            {
                this.ContextMenuStripInternal = value;
            }
        }

        internal System.Windows.Forms.ContextMenuStrip ContextMenuStripInternal
        {
            get
            {
                return (System.Windows.Forms.ContextMenuStrip) this.Properties.GetObject(PropContextMenuStrip);
            }
            set
            {
                System.Windows.Forms.ContextMenuStrip strip = (System.Windows.Forms.ContextMenuStrip) this.Properties.GetObject(PropContextMenuStrip);
                if (strip != value)
                {
                    EventHandler handler = new EventHandler(this.DetachContextMenuStrip);
                    if (strip != null)
                    {
                        strip.Disposed -= handler;
                    }
                    this.Properties.SetObject(PropContextMenuStrip, value);
                    if (value != null)
                    {
                        value.Disposed += handler;
                    }
                    if (base.Grid != null)
                    {
                        base.Grid.OnBandContextMenuStripChanged(this);
                    }
                }
            }
        }

        /// <summary>Gets or sets the default cell style of the band.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCellStyle"></see> associated with the <see cref="T:MControl.GridView.GridBand"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public virtual GridCellStyle DefaultCellStyle
        {
            get
            {
                GridCellStyle style = (GridCellStyle) this.Properties.GetObject(PropDefaultCellStyle);
                if (style == null)
                {
                    style = new GridCellStyle();
                    style.AddScope(base.Grid, this.bandIsRow ? GridCellStyleScopes.Row : GridCellStyleScopes.Column);
                    this.Properties.SetObject(PropDefaultCellStyle, style);
                }
                return style;
            }
            set
            {
                GridCellStyle defaultCellStyle = null;
                if (this.HasDefaultCellStyle)
                {
                    defaultCellStyle = this.DefaultCellStyle;
                    defaultCellStyle.RemoveScope(this.bandIsRow ? GridCellStyleScopes.Row : GridCellStyleScopes.Column);
                }
                if ((value != null) || this.Properties.ContainsObject(PropDefaultCellStyle))
                {
                    if (value != null)
                    {
                        value.AddScope(base.Grid, this.bandIsRow ? GridCellStyleScopes.Row : GridCellStyleScopes.Column);
                    }
                    this.Properties.SetObject(PropDefaultCellStyle, value);
                }
                if (((((defaultCellStyle != null) && (value == null)) || ((defaultCellStyle == null) && (value != null))) || (((defaultCellStyle != null) && (value != null)) && !defaultCellStyle.Equals(this.DefaultCellStyle))) && (base.Grid != null))
                {
                    base.Grid.OnBandDefaultCellStyleChanged(this);
                }
            }
        }

        /// <summary>Gets or sets the run-time type of the default header cell.</summary>
        /// <returns>A <see cref="T:System.Type"></see> that describes the run-time class of the object used as the default header cell.</returns>
        /// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:System.Type"></see> representing <see cref="T:MControl.GridView.GridHeaderCell"></see> or a derived type. </exception>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public System.Type DefaultHeaderCellType
        {
            get
            {
                System.Type type = (System.Type) this.Properties.GetObject(PropDefaultHeaderCellType);
                if (type != null)
                {
                    return type;
                }
                if (this.bandIsRow)
                {
                    return typeof(GridRowHeaderCell);
                }
                return typeof(GridColumnHeaderCell);
            }
            set
            {
                if ((value != null) || this.Properties.ContainsObject(PropDefaultHeaderCellType))
                {
                    if (!System.Type.GetType("MControl.GridView.GridHeaderCell").IsAssignableFrom(value))
                    {
                        throw new ArgumentException(MControl.GridView.RM.GetString("Grid_WrongType", new object[] { "DefaultHeaderCellType", "MControl.GridView.GridHeaderCell" }));
                    }
                    this.Properties.SetObject(PropDefaultHeaderCellType, value);
                }
            }
        }

        /// <summary>Gets a value indicating whether the band is currently displayed onscreen. </summary>
        /// <returns>true if the band is currently onscreen; otherwise, false.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool Displayed
        {
            get
            {
                return ((this.State & GridElementStates.Displayed) != GridElementStates.None);
            }
        }

        internal bool DisplayedInternal
        {
            set
            {
                if (value)
                {
                    base.StateInternal = this.State | GridElementStates.Displayed;
                }
                else
                {
                    base.StateInternal = this.State & ~GridElementStates.Displayed;
                }
                if (base.Grid != null)
                {
                    this.OnStateChanged(GridElementStates.Displayed);
                }
            }
        }

        internal int DividerThickness
        {
            get
            {
                bool flag;
                int integer = this.Properties.GetInteger(PropDividerThickness, out flag);
                if (!flag)
                {
                    return 0;
                }
                return integer;
            }
            set
            {
                if (value < 0)
                {
                    if (this.bandIsRow)
                    {
                        object[] objArray = new object[] { "DividerHeight", value.ToString(CultureInfo.CurrentCulture), 0.ToString(CultureInfo.CurrentCulture) };
                        throw new ArgumentOutOfRangeException("DividerHeight", MControl.GridView.RM.GetString("InvalidLowBoundArgumentEx", objArray));
                    }
                    object[] args = new object[] { "DividerWidth", value.ToString(CultureInfo.CurrentCulture), 0.ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentOutOfRangeException("DividerWidth", MControl.GridView.RM.GetString("InvalidLowBoundArgumentEx", args));
                }
                if (value > 0x10000)
                {
                    if (this.bandIsRow)
                    {
                        object[] objArray3 = new object[] { "DividerHeight", value.ToString(CultureInfo.CurrentCulture), 0x10000.ToString(CultureInfo.CurrentCulture) };
                        throw new ArgumentOutOfRangeException("DividerHeight", MControl.GridView.RM.GetString("InvalidHighBoundArgumentEx", objArray3));
                    }
                    object[] objArray4 = new object[] { "DividerWidth", value.ToString(CultureInfo.CurrentCulture), 0x10000.ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentOutOfRangeException("DividerWidth", MControl.GridView.RM.GetString("InvalidHighBoundArgumentEx", objArray4));
                }
                if (value != this.DividerThickness)
                {
                    this.Properties.SetInteger(PropDividerThickness, value);
                    if (base.Grid != null)
                    {
                        base.Grid.OnBandDividerThicknessChanged(this);
                    }
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the band will move when a user scrolls through the <see cref="T:MControl.GridView.Grid"></see>.</summary>
        /// <returns>true if the band cannot be scrolled from view; otherwise, false. The default is false.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DefaultValue(false)]
        public virtual bool Frozen
        {
            get
            {
                return ((this.State & GridElementStates.Frozen) != GridElementStates.None);
            }
            set
            {
                if (((this.State & GridElementStates.Frozen) != GridElementStates.None) != value)
                {
                    this.OnStateChanging(GridElementStates.Frozen);
                    if (value)
                    {
                        base.StateInternal = this.State | GridElementStates.Frozen;
                    }
                    else
                    {
                        base.StateInternal = this.State & ~GridElementStates.Frozen;
                    }
                    this.OnStateChanged(GridElementStates.Frozen);
                }
            }
        }

        /// <summary>Gets a value indicating whether the <see cref="P:MControl.GridView.GridBand.DefaultCellStyle"></see> property has been set. </summary>
        /// <returns>true if the <see cref="P:MControl.GridView.GridBand.DefaultCellStyle"></see> property has been set; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool HasDefaultCellStyle
        {
            get
            {
                return (this.Properties.ContainsObject(PropDefaultCellStyle) && (this.Properties.GetObject(PropDefaultCellStyle) != null));
            }
        }

        internal bool HasDefaultHeaderCellType
        {
            get
            {
                return (this.Properties.ContainsObject(PropDefaultHeaderCellType) && (this.Properties.GetObject(PropDefaultHeaderCellType) != null));
            }
        }

        internal bool HasHeaderCell
        {
            get
            {
                return (this.Properties.ContainsObject(PropHeaderCell) && (this.Properties.GetObject(PropHeaderCell) != null));
            }
        }

        /// <summary>Gets or sets the header cell of the <see cref="T:MControl.GridView.GridBand"></see>.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridHeaderCell"></see> representing the header cell of the <see cref="T:MControl.GridView.GridBand"></see>.</returns>
        /// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:MControl.GridView.GridRowHeaderCell"></see> and this <see cref="T:MControl.GridView.GridBand"></see> instance is of type <see cref="T:MControl.GridView.GridRow"></see>.-or-The specified value when setting this property is not a <see cref="T:MControl.GridView.GridColumnHeaderCell"></see> and this <see cref="T:MControl.GridView.GridBand"></see> instance is of type <see cref="T:MControl.GridView.GridColumn"></see>.</exception>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected GridHeaderCell HeaderCellCore
        {
            get
            {
                GridHeaderCell cell = (GridHeaderCell) this.Properties.GetObject(PropHeaderCell);
                if (cell == null)
                {
                    cell = (GridHeaderCell) System.Windows.Forms.SecurityUtils.SecureCreateInstance(this.DefaultHeaderCellType);
                    cell.GridInternal = base.Grid;
                    if (this.bandIsRow)
                    {
                        cell.OwningRowInternal = (GridRow) this;
                        this.Properties.SetObject(PropHeaderCell, cell);
                        return cell;
                    }
                    GridColumn column = this as GridColumn;
                    cell.OwningColumnInternal = column;
                    this.Properties.SetObject(PropHeaderCell, cell);
                    if ((base.Grid != null) && (base.Grid.SortedColumn == column))
                    {
                        GridColumnHeaderCell cell2 = cell as GridColumnHeaderCell;
                        cell2.SortGlyphDirection = base.Grid.SortOrder;
                    }
                }
                return cell;
            }
            set
            {
                GridHeaderCell cell = (GridHeaderCell) this.Properties.GetObject(PropHeaderCell);
                if ((value != null) || this.Properties.ContainsObject(PropHeaderCell))
                {
                    if (cell != null)
                    {
                        cell.GridInternal = null;
                        if (this.bandIsRow)
                        {
                            cell.OwningRowInternal = null;
                        }
                        else
                        {
                            cell.OwningColumnInternal = null;
                            ((GridColumnHeaderCell) cell).SortGlyphDirectionInternal = SortOrder.None;
                        }
                    }
                    if (value != null)
                    {
                        if (this.bandIsRow)
                        {
                            if (!(value is GridRowHeaderCell))
                            {
                                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_WrongType", new object[] { "HeaderCell", "MControl.GridView.GridRowHeaderCell" }));
                            }
                            if (value.OwningRow != null)
                            {
                                value.OwningRow.HeaderCell = null;
                            }
                            value.OwningRowInternal = (GridRow) this;
                        }
                        else
                        {
                            if (!(value is GridColumnHeaderCell))
                            {
                                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_WrongType", new object[] { "HeaderCell", "MControl.GridView.GridColumnHeaderCell" }));
                            }
                            if (value.OwningColumn != null)
                            {
                                value.OwningColumn.HeaderCell = null;
                            }
                            value.OwningColumnInternal = (GridColumn) this;
                        }
                        value.GridInternal = base.Grid;
                    }
                    this.Properties.SetObject(PropHeaderCell, value);
                }
                if (((((value == null) && (cell != null)) || ((value != null) && (cell == null))) || (((value != null) && (cell != null)) && !cell.Equals(value))) && (base.Grid != null))
                {
                    base.Grid.OnBandHeaderCellChanged(this);
                }
            }
        }

        /// <summary>Gets the relative position of the band within the <see cref="T:MControl.GridView.Grid"></see> control.</summary>
        /// <returns>The zero-based position of the band in the <see cref="T:MControl.GridView.GridRowCollection"></see> or <see cref="T:MControl.GridView.GridColumnCollection"></see> that it is contained within. The default is -1, indicating that there is no associated <see cref="T:MControl.GridView.Grid"></see> control.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Index
        {
            get
            {
                return this.bandIndex;
            }
        }

        internal int IndexInternal
        {
            set
            {
                this.bandIndex = value;
            }
        }

        /// <summary>Gets the cell style in effect for the current band, taking into account style inheritance.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> associated with the <see cref="T:MControl.GridView.GridBand"></see>. The default is null.</returns>
        [Browsable(false)]
        public virtual GridCellStyle InheritedStyle
        {
            get
            {
                return null;
            }
        }

        /// <summary>Gets a value indicating whether the band represents a row.</summary>
        /// <returns>true if the band represents a <see cref="T:MControl.GridView.GridRow"></see>; otherwise, false.</returns>
        protected bool IsRow
        {
            get
            {
                return this.bandIsRow;
            }
        }

        internal int MinimumThickness
        {
            get
            {
                if (this.bandIsRow && (this.bandIndex > -1))
                {
                    int num;
                    int num2;
                    this.GetHeightInfo(this.bandIndex, out num, out num2);
                    return num2;
                }
                return this.minimumThickness;
            }
            set
            {
                if (this.minimumThickness != value)
                {
                    if (value < 2)
                    {
                        if (this.bandIsRow)
                        {
                            object[] objArray = new object[] { 2.ToString(CultureInfo.CurrentCulture) };
                            throw new ArgumentOutOfRangeException("MinimumHeight", value, MControl.GridView.RM.GetString("GridBand_MinimumHeightSmallerThanOne", objArray));
                        }
                        object[] args = new object[] { 2.ToString(CultureInfo.CurrentCulture) };
                        throw new ArgumentOutOfRangeException("MinimumWidth", value, MControl.GridView.RM.GetString("GridBand_MinimumWidthSmallerThanOne", args));
                    }
                    if (this.Thickness < value)
                    {
                        if ((base.Grid != null) && !this.bandIsRow)
                        {
                            base.Grid.OnColumnMinimumWidthChanging((GridColumn) this, value);
                        }
                        this.Thickness = value;
                    }
                    this.minimumThickness = value;
                    if (base.Grid != null)
                    {
                        base.Grid.OnBandMinimumThicknessChanged(this);
                    }
                }
            }
        }

        internal PropertyStore Properties
        {
            get
            {
                return this.propertyStore;
            }
        }

        /// <summary>Gets or sets a value indicating whether the user can edit the band's cells.</summary>
        /// <returns>true if the user cannot edit the band's cells; otherwise, false. The default is false.</returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, this <see cref="T:MControl.GridView.GridBand"></see> instance is a shared <see cref="T:MControl.GridView.GridRow"></see>.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DefaultValue(false)]
        public virtual bool ReadOnly
        {
            get
            {
                return (((this.State & GridElementStates.ReadOnly) != GridElementStates.None) || ((base.Grid != null) && base.Grid.ReadOnly));
            }
            set
            {
                if (base.Grid != null)
                {
                    if (!base.Grid.ReadOnly)
                    {
                        if (this.bandIsRow)
                        {
                            if (this.bandIndex == -1)
                            {
                                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertySetOnSharedRow", new object[] { "ReadOnly" }));
                            }
                            this.OnStateChanging(GridElementStates.ReadOnly);
                            base.Grid.SetReadOnlyRowCore(this.bandIndex, value);
                        }
                        else
                        {
                            this.OnStateChanging(GridElementStates.ReadOnly);
                            base.Grid.SetReadOnlyColumnCore(this.bandIndex, value);
                        }
                    }
                }
                else if (((this.State & GridElementStates.ReadOnly) != GridElementStates.None) != value)
                {
                    if (value)
                    {
                        if (this.bandIsRow)
                        {
                            foreach (GridCell cell in ((GridRow) this).Cells)
                            {
                                if (cell.ReadOnly)
                                {
                                    cell.ReadOnlyInternal = false;
                                }
                            }
                        }
                        base.StateInternal = this.State | GridElementStates.ReadOnly;
                    }
                    else
                    {
                        base.StateInternal = this.State & ~GridElementStates.ReadOnly;
                    }
                }
            }
        }

        internal bool ReadOnlyInternal
        {
            set
            {
                if (value)
                {
                    base.StateInternal = this.State | GridElementStates.ReadOnly;
                }
                else
                {
                    base.StateInternal = this.State & ~GridElementStates.ReadOnly;
                }
                this.OnStateChanged(GridElementStates.ReadOnly);
            }
        }

        /// <summary>Gets or sets a value indicating whether the band can be resized in the user interface (UI).</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridTriState"></see> values. The default is <see cref="F:MControl.GridView.GridTriState.True"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(true)]
        public virtual GridTriState Resizable
        {
            get
            {
                if ((this.State & GridElementStates.ResizableSet) != GridElementStates.None)
                {
                    if ((this.State & GridElementStates.Resizable) == GridElementStates.None)
                    {
                        return GridTriState.False;
                    }
                    return GridTriState.True;
                }
                if (base.Grid == null)
                {
                    return GridTriState.NotSet;
                }
                if (!base.Grid.AllowUserToResizeColumns)
                {
                    return GridTriState.False;
                }
                return GridTriState.True;
            }
            set
            {
                GridTriState resizable = this.Resizable;
                if (value == GridTriState.NotSet)
                {
                    base.StateInternal = this.State & ~GridElementStates.ResizableSet;
                }
                else
                {
                    base.StateInternal = this.State | GridElementStates.ResizableSet;
                    if (((this.State & GridElementStates.Resizable) != GridElementStates.None) != (value == GridTriState.True))
                    {
                        if (value == GridTriState.True)
                        {
                            base.StateInternal = this.State | GridElementStates.Resizable;
                        }
                        else
                        {
                            base.StateInternal = this.State & ~GridElementStates.Resizable;
                        }
                    }
                }
                if (resizable != this.Resizable)
                {
                    this.OnStateChanged(GridElementStates.Resizable);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the band is in a selected user interface (UI) state.</summary>
        /// <returns>true if the band is selected; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is true, but the band has not been added to a <see cref="T:MControl.GridView.Grid"></see> control. -or-This property is being set on a shared <see cref="T:MControl.GridView.GridRow"></see>.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual bool Selected
        {
            get
            {
                return ((this.State & GridElementStates.Selected) != GridElementStates.None);
            }
            set
            {
                if (base.Grid != null)
                {
                    if (!this.bandIsRow)
                    {
                        if ((base.Grid.SelectionMode == GridSelectionMode.FullColumnSelect) || (base.Grid.SelectionMode == GridSelectionMode.ColumnHeaderSelect))
                        {
                            base.Grid.SetSelectedColumnCoreInternal(this.bandIndex, value);
                        }
                    }
                    else
                    {
                        if (this.bandIndex == -1)
                        {
                            throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertySetOnSharedRow", new object[] { "Selected" }));
                        }
                        if ((base.Grid.SelectionMode == GridSelectionMode.FullRowSelect) || (base.Grid.SelectionMode == GridSelectionMode.RowHeaderSelect))
                        {
                            base.Grid.SetSelectedRowCoreInternal(this.bandIndex, value);
                        }
                    }
                }
                else if (value)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridBand_CannotSelect"));
                }
            }
        }

        internal bool SelectedInternal
        {
            set
            {
                if (value)
                {
                    base.StateInternal = this.State | GridElementStates.Selected;
                }
                else
                {
                    base.StateInternal = this.State & ~GridElementStates.Selected;
                }
                if (base.Grid != null)
                {
                    this.OnStateChanged(GridElementStates.Selected);
                }
            }
        }

        /// <summary>Gets or sets the object that contains data to associate with the band.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that contains information associated with the band. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object Tag
        {
            get
            {
                return this.Properties.GetObject(PropUserData);
            }
            set
            {
                if ((value != null) || this.Properties.ContainsObject(PropUserData))
                {
                    this.Properties.SetObject(PropUserData, value);
                }
            }
        }

        internal int Thickness
        {
            get
            {
                if (this.bandIsRow && (this.bandIndex > -1))
                {
                    int num;
                    int num2;
                    this.GetHeightInfo(this.bandIndex, out num, out num2);
                    return num;
                }
                return this.thickness;
            }
            set
            {
                int minimumThickness = this.MinimumThickness;
                if (value < minimumThickness)
                {
                    value = minimumThickness;
                }
                if (value > 0x10000)
                {
                    if (this.bandIsRow)
                    {
                        object[] objArray = new object[] { "Height", value.ToString(CultureInfo.CurrentCulture), 0x10000.ToString(CultureInfo.CurrentCulture) };
                        throw new ArgumentOutOfRangeException("Height", MControl.GridView.RM.GetString("InvalidHighBoundArgumentEx", objArray));
                    }
                    object[] args = new object[] { "Width", value.ToString(CultureInfo.CurrentCulture), 0x10000.ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentOutOfRangeException("Width", MControl.GridView.RM.GetString("InvalidHighBoundArgumentEx", args));
                }
                bool flag = true;
                if (this.bandIsRow)
                {
                    if ((base.Grid != null) && (base.Grid.AutoSizeRowsMode != GridAutoSizeRowsMode.None))
                    {
                        this.cachedThickness = value;
                        flag = false;
                    }
                }
                else
                {
                    GridColumn gridColumn = (GridColumn) this;
                    GridAutoSizeColumnMode inheritedAutoSizeMode = gridColumn.InheritedAutoSizeMode;
                    if (((inheritedAutoSizeMode != GridAutoSizeColumnMode.Fill) && (inheritedAutoSizeMode != GridAutoSizeColumnMode.None)) && (inheritedAutoSizeMode != GridAutoSizeColumnMode.NotSet))
                    {
                        this.cachedThickness = value;
                        flag = false;
                    }
                    else if (((inheritedAutoSizeMode == GridAutoSizeColumnMode.Fill) && (base.Grid != null)) && gridColumn.Visible)
                    {
                        IntPtr handle = base.Grid.Handle;
                        base.Grid.AdjustFillingColumn(gridColumn, value);
                        flag = false;
                    }
                }
                if (flag && (this.thickness != value))
                {
                    if (base.Grid != null)
                    {
                        base.Grid.OnBandThicknessChanging();
                    }
                    this.ThicknessInternal = value;
                }
            }
        }

        internal int ThicknessInternal
        {
            get
            {
                return this.thickness;
            }
            set
            {
                this.thickness = value;
                if (base.Grid != null)
                {
                    base.Grid.OnBandThicknessChanged(this);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the band is visible to the user.</summary>
        /// <returns>true if the band is visible; otherwise, false. The default is true.</returns>
        /// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is false and the band is the row for new records.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DefaultValue(true)]
        public virtual bool Visible
        {
            get
            {
                return ((this.State & GridElementStates.Visible) != GridElementStates.None);
            }
            set
            {
                if (((this.State & GridElementStates.Visible) != GridElementStates.None) != value)
                {
                    if ((((base.Grid != null) && this.bandIsRow) && ((base.Grid.NewRowIndex != -1) && (base.Grid.NewRowIndex == this.bandIndex))) && !value)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridBand_NewRowCannotBeInvisible"));
                    }
                    this.OnStateChanging(GridElementStates.Visible);
                    if (value)
                    {
                        base.StateInternal = this.State | GridElementStates.Visible;
                    }
                    else
                    {
                        base.StateInternal = this.State & ~GridElementStates.Visible;
                    }
                    this.OnStateChanged(GridElementStates.Visible);
                }
            }
        }
    }
}

