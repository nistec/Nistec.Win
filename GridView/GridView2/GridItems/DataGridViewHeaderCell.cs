namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms.VisualStyles;
    using System.Windows.Forms;

    /// <summary>Contains functionality common to row header cells and column header cells.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridHeaderCell : GridCell
    {
        private const string AEROTHEMEFILENAME = "Aero.msstyles";
        private static System.Type cellType = typeof(GridHeaderCell);
        private const byte GRIDHEADERCELL_themeMargin = 100;
        private static System.Type defaultFormattedValueType = typeof(string);
        private static System.Type defaultValueType = typeof(object);
        private static readonly int PropButtonState = PropertyStore.CreateKey();
        private static readonly int PropFlipXPThemesBitmap = PropertyStore.CreateKey();
        private static readonly int PropValueType = PropertyStore.CreateKey();
        private static Rectangle rectThemeMargins = new Rectangle(-1, -1, 0, 0);

        /// <summary>Creates an exact copy of this cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridHeaderCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridHeaderCell cell;
            System.Type type = base.GetType();
            if (type == cellType)
            {
                cell = new GridHeaderCell();
            }
            else
            {
                cell = (GridHeaderCell) Activator.CreateInstance(type);
            }
            base.CloneInternal(cell);
            cell.Value = base.Value;
            return cell;
        }

        /// <summary>Releases the unmanaged resources used by the <see cref="T:MControl.GridView.GridHeaderCell"></see> and optionally releases the managed resources. </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected override void Dispose(bool disposing)
        {
            if ((this.FlipXPThemesBitmap != null) && disposing)
            {
                this.FlipXPThemesBitmap.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>Gets the shortcut menu of the header cell.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> if the <see cref="T:MControl.GridView.GridHeaderCell"></see> or <see cref="T:MControl.GridView.Grid"></see> has a shortcut menu assigned; otherwise, null.</returns>
        /// <param name="rowIndex">Ignored by this implementation.</param>
        public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
        {
            ContextMenuStrip contextMenuStrip = base.GetContextMenuStrip(rowIndex);
            if (contextMenuStrip != null)
            {
                return contextMenuStrip;
            }
            if (base.Grid != null)
            {
                return base.Grid.ContextMenuStrip;
            }
            return null;
        }

        /// <summary>Returns a value indicating the current state of the cell as inherited from the state of its row or column.</summary>
        /// <returns>A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values representing the current state of the cell.</returns>
        /// <param name="rowIndex">The index of the row containing the cell or -1 if the cell is not a row header cell or is not contained within a <see cref="T:MControl.GridView.Grid"></see> control.</param>
        /// <exception cref="T:System.ArgumentException">The cell is a row header cell, the cell is not contained within a <see cref="T:MControl.GridView.Grid"></see> control, and rowIndex is not -1.- or -The cell is a row header cell, the cell is contained within a <see cref="T:MControl.GridView.Grid"></see> control, and rowIndex is outside the valid range of 0 to the number of rows in the control minus 1.- or -The cell is a row header cell and rowIndex is not the index of the row containing this cell.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The cell is a column header cell or the control's <see cref="P:MControl.GridView.Grid.TopLeftHeaderCell"></see>  and rowIndex is not -1.</exception>
        public override GridElementStates GetInheritedState(int rowIndex)
        {
            GridElementStates states = GridElementStates.ResizableSet | GridElementStates.ReadOnly;
            if (base.OwningRow != null)
            {
                if (((base.Grid == null) && (rowIndex != -1)) || ((base.Grid != null) && ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count))))
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("InvalidArgument", new object[] { "rowIndex", rowIndex.ToString(CultureInfo.CurrentCulture) }));
                }
                if ((base.Grid != null) && (base.Grid.Rows.SharedRow(rowIndex) != base.OwningRow))
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("InvalidArgument", new object[] { "rowIndex", rowIndex.ToString(CultureInfo.CurrentCulture) }));
                }
                states |= base.OwningRow.GetState(rowIndex) & GridElementStates.Frozen;
                if ((base.OwningRow.GetResizable(rowIndex) == GridTriState.True) || ((base.Grid != null) && (base.Grid.RowHeadersWidthSizeMode == GridRowHeadersWidthSizeMode.EnableResizing)))
                {
                    states |= GridElementStates.Resizable;
                }
                if (base.OwningRow.GetVisible(rowIndex) && ((base.Grid == null) || base.Grid.RowHeadersVisible))
                {
                    states |= GridElementStates.Visible;
                    if (base.OwningRow.GetDisplayed(rowIndex))
                    {
                        states |= GridElementStates.Displayed;
                    }
                }
                return states;
            }
            if (base.OwningColumn != null)
            {
                if (rowIndex != -1)
                {
                    throw new ArgumentOutOfRangeException("rowIndex");
                }
                states |= base.OwningColumn.State & GridElementStates.Frozen;
                if ((base.OwningColumn.Resizable == GridTriState.True) || ((base.Grid != null) && (base.Grid.ColumnHeadersHeightSizeMode == GridColumnHeadersHeightSizeMode.EnableResizing)))
                {
                    states |= GridElementStates.Resizable;
                }
                if (base.OwningColumn.Visible && ((base.Grid == null) || base.Grid.ColumnHeadersVisible))
                {
                    states |= GridElementStates.Visible;
                    if (base.OwningColumn.Displayed)
                    {
                        states |= GridElementStates.Displayed;
                    }
                }
                return states;
            }
            if (base.Grid != null)
            {
                if (rowIndex != -1)
                {
                    throw new ArgumentOutOfRangeException("rowIndex");
                }
                states |= GridElementStates.Frozen;
                if ((base.Grid.RowHeadersWidthSizeMode == GridRowHeadersWidthSizeMode.EnableResizing) || (base.Grid.ColumnHeadersHeightSizeMode == GridColumnHeadersHeightSizeMode.EnableResizing))
                {
                    states |= GridElementStates.Resizable;
                }
                if (base.Grid.RowHeadersVisible && base.Grid.ColumnHeadersVisible)
                {
                    states |= GridElementStates.Visible;
                    if (base.Grid.LayoutInfo.TopLeftHeader != Rectangle.Empty)
                    {
                        states |= GridElementStates.Displayed;
                    }
                }
            }
            return states;
        }

        /// <summary>Gets the size of the cell.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> that represents the size of the header cell.</returns>
        /// <param name="rowIndex">The row index of the header cell.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:MControl.GridView.GridElement.Grid"></see> property for this cell is null and rowIndex does not equal -1. -or-The value of the <see cref="P:MControl.GridView.GridCell.OwningColumn"></see> property for this cell is not null and rowIndex does not equal -1. -or-The value of the <see cref="P:MControl.GridView.GridCell.OwningRow"></see> property for this cell is not null and rowIndex is less than zero or greater than or equal to the number of rows in the control.-or-The values of the <see cref="P:MControl.GridView.GridCell.OwningColumn"></see> and <see cref="P:MControl.GridView.GridCell.OwningRow"></see> properties of this cell are both null and rowIndex does not equal -1.</exception>
        /// <exception cref="T:System.ArgumentException">The value of the <see cref="P:MControl.GridView.GridCell.OwningRow"></see> property for this cell is not null and rowIndex indicates a row other than the <see cref="P:MControl.GridView.GridCell.OwningRow"></see>.</exception>
        protected override Size GetSize(int rowIndex)
        {
            if (base.Grid == null)
            {
                if (rowIndex != -1)
                {
                    throw new ArgumentOutOfRangeException("rowIndex");
                }
                return new Size(-1, -1);
            }
            if (base.OwningColumn != null)
            {
                if (rowIndex != -1)
                {
                    throw new ArgumentOutOfRangeException("rowIndex");
                }
                return new Size(base.OwningColumn.Thickness, base.Grid.ColumnHeadersHeight);
            }
            if (base.OwningRow != null)
            {
                if ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count))
                {
                    throw new ArgumentOutOfRangeException("rowIndex");
                }
                if (base.Grid.Rows.SharedRow(rowIndex) != base.OwningRow)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("InvalidArgument", new object[] { "rowIndex", rowIndex.ToString(CultureInfo.CurrentCulture) }));
                }
                return new Size(base.Grid.RowHeadersWidth, base.OwningRow.GetHeight(rowIndex));
            }
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            return new Size(base.Grid.RowHeadersWidth, base.Grid.ColumnHeadersHeight);
        }

        internal static Rectangle GetThemeMargins(Graphics g)
        {
            if (rectThemeMargins.X == -1)
            {
                Rectangle bounds = new Rectangle(0, 0, 100, 100);
                Rectangle backgroundContentRectangle = GridHeaderCellRenderer.VisualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
                rectThemeMargins.X = backgroundContentRectangle.X;
                rectThemeMargins.Y = backgroundContentRectangle.Y;
                rectThemeMargins.Width = 100 - backgroundContentRectangle.Right;
                rectThemeMargins.Height = 100 - backgroundContentRectangle.Bottom;
                if ((rectThemeMargins.X == 3) && (((rectThemeMargins.Y + rectThemeMargins.Width) + rectThemeMargins.Height) == 0))
                {
                    rectThemeMargins = new Rectangle(0, 0, 2, 3);
                }
                else
                {
                    try
                    {
                        if (string.Equals(Path.GetFileName(VisualStyleInformation.ThemeFilename), "Aero.msstyles", StringComparison.OrdinalIgnoreCase))
                        {
                            rectThemeMargins = new Rectangle(2, 1, 0, 2);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return rectThemeMargins;
        }

        /// <summary>Gets the value of the cell. </summary>
        /// <returns>The value of the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is not -1.</exception>
        protected override object GetValue(int rowIndex)
        {
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            return base.Properties.GetObject(GridCell.PropCellValue);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse button is held down while the pointer is on a cell in the row.</summary>
        /// <returns>true if the user clicks with the left mouse button, visual styles are enabled, and the <see cref="P:MControl.GridView.Grid.EnableHeadersVisualStyles"></see> property is true; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains information about the mouse position.</param>
        protected override bool MouseDownUnsharesRow(GridCellMouseEventArgs e)
        {
            return ((e.Button == MouseButtons.Left) && base.Grid.ApplyVisualStylesToHeaderCells);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
        /// <returns>true if visual styles are enabled, and the <see cref="P:MControl.GridView.Grid.EnableHeadersVisualStyles"></see> property is true; otherwise, false.</returns>
        /// <param name="rowIndex">The index of the row that the mouse pointer entered.</param>
        protected override bool MouseEnterUnsharesRow(int rowIndex)
        {
            return (((base.ColumnIndex == base.Grid.MouseDownCellAddress.X) && (rowIndex == base.Grid.MouseDownCellAddress.Y)) && base.Grid.ApplyVisualStylesToHeaderCells);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
        /// <returns>true if the <see cref="P:MControl.GridView.GridHeaderCell.ButtonState"></see> property value is not <see cref="F:System.Windows.Forms.ButtonState.Normal"></see>, visual styles are enabled, and the <see cref="P:MControl.GridView.Grid.EnableHeadersVisualStyles"></see> property is true; otherwise, false.</returns>
        /// <param name="rowIndex">The index of the row that the mouse pointer left.</param>
        protected override bool MouseLeaveUnsharesRow(int rowIndex)
        {
            return ((this.ButtonState != System.Windows.Forms.ButtonState.Normal) && base.Grid.ApplyVisualStylesToHeaderCells);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse button is released while the pointer is on a cell in the row.</summary>
        /// <returns>true if the left mouse button was released, visual styles are enabled, and the <see cref="P:MControl.GridView.Grid.EnableHeadersVisualStyles"></see> property is true; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains information about the mouse position.</param>
        protected override bool MouseUpUnsharesRow(GridCellMouseEventArgs e)
        {
            return ((e.Button == MouseButtons.Left) && base.Grid.ApplyVisualStylesToHeaderCells);
        }

        /// <summary>Called when the mouse button is held down while the pointer is on a cell.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains information about the mouse position.</param>
        protected override void OnMouseDown(GridCellMouseEventArgs e)
        {
            if ((base.Grid != null) && (((e.Button == MouseButtons.Left) && base.Grid.ApplyVisualStylesToHeaderCells) && !base.Grid.ResizingOperationAboutToStart))
            {
                this.UpdateButtonState(System.Windows.Forms.ButtonState.Pushed, e.RowIndex);
            }
        }

        /// <summary>Called when the mouse pointer enters the cell.</summary>
        /// <param name="rowIndex">The index of the row containing the cell.</param>
        protected override void OnMouseEnter(int rowIndex)
        {
            if ((base.Grid != null) && base.Grid.ApplyVisualStylesToHeaderCells)
            {
                if ((((base.ColumnIndex == base.Grid.MouseDownCellAddress.X) && (rowIndex == base.Grid.MouseDownCellAddress.Y)) && ((this.ButtonState == System.Windows.Forms.ButtonState.Normal) && (Control.MouseButtons == MouseButtons.Left))) && !base.Grid.ResizingOperationAboutToStart)
                {
                    this.UpdateButtonState(System.Windows.Forms.ButtonState.Pushed, rowIndex);
                }
                base.Grid.InvalidateCell(base.ColumnIndex, rowIndex);
            }
        }

        /// <summary>Called when the mouse pointer leaves the cell.</summary>
        /// <param name="rowIndex">The index of the row containing the cell.</param>
        protected override void OnMouseLeave(int rowIndex)
        {
            if ((base.Grid != null) && base.Grid.ApplyVisualStylesToHeaderCells)
            {
                if (this.ButtonState != System.Windows.Forms.ButtonState.Normal)
                {
                    this.UpdateButtonState(System.Windows.Forms.ButtonState.Normal, rowIndex);
                }
                base.Grid.InvalidateCell(base.ColumnIndex, rowIndex);
            }
        }

        /// <summary>Called when the mouse button is released while the pointer is over the cell. </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains information about the mouse position.</param>
        protected override void OnMouseUp(GridCellMouseEventArgs e)
        {
            if ((base.Grid != null) && ((e.Button == MouseButtons.Left) && base.Grid.ApplyVisualStylesToHeaderCells))
            {
                this.UpdateButtonState(System.Windows.Forms.ButtonState.Normal, e.RowIndex);
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates gridElementState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if (GridCell.PaintBorder(paintParts))
            {
                this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            if (GridCell.PaintBackground(paintParts))
            {
                Rectangle rect = cellBounds;
                Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
                rect.Offset(rectangle2.X, rectangle2.Y);
                rect.Width -= rectangle2.Right;
                rect.Height -= rectangle2.Bottom;
                bool flag = (gridElementState & GridElementStates.Selected) != GridElementStates.None;
                SolidBrush cachedBrush = base.Grid.GetCachedBrush((GridCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
                if (cachedBrush.Color.A == 0xff)
                {
                    graphics.FillRectangle(cachedBrush, rect);
                }
            }
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridHeaderCell { ColumnIndex=" + base.ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + base.RowIndex.ToString(CultureInfo.CurrentCulture) + " }");
        }

        private void UpdateButtonState(System.Windows.Forms.ButtonState newButtonState, int rowIndex)
        {
            this.ButtonStatePrivate = newButtonState;
            base.Grid.InvalidateCell(base.ColumnIndex, rowIndex);
        }

        /// <summary>Gets the buttonlike visual state of the header cell.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.ButtonState"></see> values; the default is <see cref="F:System.Windows.Forms.ButtonState.Normal"></see>.</returns>
        protected System.Windows.Forms.ButtonState ButtonState
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropButtonState, out flag);
                if (flag)
                {
                    return (System.Windows.Forms.ButtonState) integer;
                }
                return System.Windows.Forms.ButtonState.Normal;
            }
        }

        private System.Windows.Forms.ButtonState ButtonStatePrivate
        {
            set
            {
                if (this.ButtonState != value)
                {
                    base.Properties.SetInteger(PropButtonState, (int) value);
                }
            }
        }

        [Browsable(false)]
        public override bool Displayed
        {
            get
            {
                if ((base.Grid == null) || !base.Grid.Visible)
                {
                    return false;
                }
                if (base.OwningRow != null)
                {
                    return (base.Grid.RowHeadersVisible && base.OwningRow.Displayed);
                }
                if (base.OwningColumn == null)
                {
                    return (base.Grid.LayoutInfo.TopLeftHeader != Rectangle.Empty);
                }
                return (base.Grid.ColumnHeadersVisible && base.OwningColumn.Displayed);
            }
        }

        internal Bitmap FlipXPThemesBitmap
        {
            get
            {
                return (Bitmap) base.Properties.GetObject(PropFlipXPThemesBitmap);
            }
            set
            {
                if ((value != null) || base.Properties.ContainsObject(PropFlipXPThemesBitmap))
                {
                    base.Properties.SetObject(PropFlipXPThemesBitmap, value);
                }
            }
        }

        /// <summary>Gets the type of the formatted value of the cell.</summary>
        /// <returns>A <see cref="T:System.Type"></see> object representing the <see cref="T:System.String"></see> type.</returns>
        public override System.Type FormattedValueType
        {
            get
            {
                return defaultFormattedValueType;
            }
        }

        /// <summary>Gets a value indicating whether the cell is frozen. </summary>
        /// <returns>true if the cell is frozen; otherwise, false. The default is false if the cell is detached from a <see cref="T:MControl.GridView.Grid"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public override bool Frozen
        {
            get
            {
                if (base.OwningRow != null)
                {
                    return base.OwningRow.Frozen;
                }
                if (base.OwningColumn != null)
                {
                    return base.OwningColumn.Frozen;
                }
                return (base.Grid != null);
            }
        }

        internal override bool HasValueType
        {
            get
            {
                return (base.Properties.ContainsObject(PropValueType) && (base.Properties.GetObject(PropValueType) != null));
            }
        }

        /// <summary>Gets a value indicating whether the header cell is read-only.</summary>
        /// <returns>true in all cases.</returns>
        /// <exception cref="T:System.InvalidOperationException">An operation tries to set this property.</exception>
        /// <filterpriority>1</filterpriority>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool ReadOnly
        {
            get
            {
                return true;
            }
            set
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_HeaderCellReadOnlyProperty", new object[] { "ReadOnly" }));
            }
        }

        /// <summary>Gets a value indicating whether the cell is resizable.</summary>
        /// <returns>true if this cell can be resized; otherwise, false. The default is false if the cell is not attached to a <see cref="T:MControl.GridView.Grid"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public override bool Resizable
        {
            get
            {
                if (base.OwningRow != null)
                {
                    return ((base.OwningRow.Resizable == GridTriState.True) || ((base.Grid != null) && (base.Grid.RowHeadersWidthSizeMode == GridRowHeadersWidthSizeMode.EnableResizing)));
                }
                if (base.OwningColumn != null)
                {
                    return ((base.OwningColumn.Resizable == GridTriState.True) || ((base.Grid != null) && (base.Grid.ColumnHeadersHeightSizeMode == GridColumnHeadersHeightSizeMode.EnableResizing)));
                }
                if (base.Grid == null)
                {
                    return false;
                }
                if (base.Grid.RowHeadersWidthSizeMode != GridRowHeadersWidthSizeMode.EnableResizing)
                {
                    return (base.Grid.ColumnHeadersHeightSizeMode == GridColumnHeadersHeightSizeMode.EnableResizing);
                }
                return true;
            }
        }

        /// <summary>Gets or sets a value indicating whether the cell is selected.</summary>
        /// <returns>false in all cases.</returns>
        /// <exception cref="T:System.InvalidOperationException">This property is being set.</exception>
        /// <filterpriority>1</filterpriority>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool Selected
        {
            get
            {
                return false;
            }
            set
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_HeaderCellReadOnlyProperty", new object[] { "Selected" }));
            }
        }

        /// <summary>Gets the type of the value stored in the cell.</summary>
        /// <returns>A <see cref="T:System.Type"></see> object representing the <see cref="T:System.String"></see> type.</returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type ValueType
        {
            get
            {
                System.Type type = (System.Type) base.Properties.GetObject(PropValueType);
                if (type != null)
                {
                    return type;
                }
                return defaultValueType;
            }
            set
            {
                if ((value != null) || base.Properties.ContainsObject(PropValueType))
                {
                    base.Properties.SetObject(PropValueType, value);
                }
            }
        }

        /// <summary>Gets a value indicating whether or not the cell is visible.</summary>
        /// <returns>true if the cell is visible; otherwise, false. The default is false if the cell is detached from a <see cref="T:MControl.GridView.Grid"></see></returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public override bool Visible
        {
            get
            {
                if (base.OwningRow != null)
                {
                    if (!base.OwningRow.Visible)
                    {
                        return false;
                    }
                    if (base.Grid != null)
                    {
                        return base.Grid.RowHeadersVisible;
                    }
                    return true;
                }
                if (base.OwningColumn != null)
                {
                    if (!base.OwningColumn.Visible)
                    {
                        return false;
                    }
                    if (base.Grid != null)
                    {
                        return base.Grid.ColumnHeadersVisible;
                    }
                    return true;
                }
                if (base.Grid == null)
                {
                    return false;
                }
                return (base.Grid.RowHeadersVisible && base.Grid.ColumnHeadersVisible);
            }
        }

        private class GridHeaderCellRenderer
        {
            private static System.Windows.Forms.VisualStyles.VisualStyleRenderer visualStyleRenderer;

            private GridHeaderCellRenderer()
            {
            }

            public static System.Windows.Forms.VisualStyles.VisualStyleRenderer VisualStyleRenderer
            {
                get
                {
                    if (visualStyleRenderer == null)
                    {
                        visualStyleRenderer = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(VisualStyleElement.Header.Item.Normal);
                    }
                    return visualStyleRenderer;
                }
            }
        }
    }
}

