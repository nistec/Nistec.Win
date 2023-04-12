namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Windows.Forms.ButtonInternal;
    using System.Windows.Forms.VisualStyles;
    using System.Windows.Forms;

    /// <summary>Displays a check box user interface (UI) to use in a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridCheckBoxCell : GridCell, IGridEditingCell
    {
        private static readonly GridContentAlignment anyBottom = (GridContentAlignment.BottomRight | GridContentAlignment.BottomCenter | GridContentAlignment.BottomLeft);
        private static readonly GridContentAlignment anyCenter = (GridContentAlignment.BottomCenter | GridContentAlignment.MiddleCenter | GridContentAlignment.TopCenter);
        private static readonly GridContentAlignment anyLeft = (GridContentAlignment.BottomLeft | GridContentAlignment.MiddleLeft | GridContentAlignment.TopLeft);
        private static readonly GridContentAlignment anyMiddle = (GridContentAlignment.MiddleRight | GridContentAlignment.MiddleCenter | GridContentAlignment.MiddleLeft);
        private static readonly GridContentAlignment anyRight = (GridContentAlignment.BottomRight | GridContentAlignment.MiddleRight | GridContentAlignment.TopRight);
        private static System.Type cellType = typeof(GridCheckBoxCell);
        private static readonly VisualStyleElement CheckBoxElement = VisualStyleElement.Button.CheckBox.UncheckedNormal;
        private static Bitmap checkImage = null;
        private const byte GRIDCHECKBOXCELL_checked = 0x10;
        private const byte GRIDCHECKBOXCELL_indeterminate = 0x20;
        private const byte GRIDCHECKBOXCELL_margin = 2;
        private const byte GRIDCHECKBOXCELL_threeState = 1;
        private const byte GRIDCHECKBOXCELL_valueChanged = 2;
        private static System.Type defaultBooleanType = typeof(bool);
        private static System.Type defaultCheckStateType = typeof(CheckState);
        private byte flags;
        private static bool mouseInContentBounds = false;
        private static readonly int PropButtonCellState = PropertyStore.CreateKey();
        private static readonly int PropFalseValue = PropertyStore.CreateKey();
        private static readonly int PropFlatStyle = PropertyStore.CreateKey();
        private static readonly int PropIndeterminateValue = PropertyStore.CreateKey();
        private static readonly int PropTrueValue = PropertyStore.CreateKey();

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCheckBoxCell"></see> class to its default state.</summary>
        public GridCheckBoxCell() : this(false)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCheckBoxCell"></see> class, enabling binary or ternary state.</summary>
        /// <param name="threeState">true to enable ternary state; false to enable binary state.</param>
        public GridCheckBoxCell(bool threeState)
        {
            if (threeState)
            {
                this.flags = 1;
            }
        }

        /// <summary>Creates an exact copy of this cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridCheckBoxCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridCheckBoxCell cell;
            System.Type type = base.GetType();
            if (type == cellType)
            {
                cell = new GridCheckBoxCell();
            }
            else
            {
                cell = (GridCheckBoxCell) Activator.CreateInstance(type);
            }
            base.CloneInternal(cell);
            cell.ThreeStateInternal = this.ThreeState;
            cell.TrueValueInternal = this.TrueValue;
            cell.FalseValueInternal = this.FalseValue;
            cell.IndeterminateValueInternal = this.IndeterminateValue;
            cell.FlatStyleInternal = this.FlatStyle;
            return cell;
        }

        private bool CommonContentClickUnsharesRow(GridCellEventArgs e)
        {
            Point currentCellAddress = base.Grid.CurrentCellAddress;
            return (((currentCellAddress.X == base.ColumnIndex) && (currentCellAddress.Y == e.RowIndex)) && base.Grid.IsCurrentCellInEditMode);
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the cell content is clicked.</summary>
        /// <returns>true if the cell is in edit mode; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains data about the mouse click.</param>
        protected override bool ContentClickUnsharesRow(GridCellEventArgs e)
        {
            return this.CommonContentClickUnsharesRow(e);
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the cell content is double-clicked.</summary>
        /// <returns>true if the cell is in edit mode; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains data about the double-click.</param>
        protected override bool ContentDoubleClickUnsharesRow(GridCellEventArgs e)
        {
            return this.CommonContentClickUnsharesRow(e);
        }

        /// <summary>Creates a new accessible object for the <see cref="T:MControl.GridView.GridCheckBoxCell"></see>. </summary>
        /// <returns>A new <see cref="T:MControl.GridView.GridCheckBoxCell.GridCheckBoxCellAccessibleObject"></see> for the <see cref="T:MControl.GridView.GridCheckBoxCell"></see>. </returns>
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new GridCheckBoxCellAccessibleObject(this);
        }

        protected override Rectangle GetContentBounds(Graphics graphics, GridCellStyle cellStyle, int rowIndex)
        {
            GridAdvancedBorderStyle style;
            GridElementStates states;
            Rectangle rectangle;
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if (((base.Grid == null) || (rowIndex < 0)) || (base.OwningColumn == null))
            {
                return Rectangle.Empty;
            }
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, null, null, cellStyle, style, GridPaintParts.ContentForeground, true, false, false);
        }

        /// <summary>Gets the formatted value of the cell while it is in edit mode.</summary>
        /// <returns>An <see cref="T:System.Object"></see> representing the formatted value of the editing cell. </returns>
        /// <param name="context">A bitwise combination of <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values that describes the context in which any formatting error occurs. </param>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="P:MControl.GridView.GridCheckBoxCell.FormattedValueType"></see> property value is null.</exception>
        public virtual object GetEditingCellFormattedValue(GridDataErrorContexts context)
        {
            if (this.FormattedValueType == null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCell_FormattedValueTypeNull"));
            }
            if (this.FormattedValueType.IsAssignableFrom(defaultCheckStateType))
            {
                if ((this.flags & 0x10) != 0)
                {
                    if ((context & GridDataErrorContexts.ClipboardContent) != 0)
                    {
                        return MControl.GridView.RM.GetString("GridCheckBoxCell_ClipboardChecked");
                    }
                    return CheckState.Checked;
                }
                if ((this.flags & 0x20) != 0)
                {
                    if ((context & GridDataErrorContexts.ClipboardContent) != 0)
                    {
                        return MControl.GridView.RM.GetString("GridCheckBoxCell_ClipboardIndeterminate");
                    }
                    return CheckState.Indeterminate;
                }
                if ((context & GridDataErrorContexts.ClipboardContent) != 0)
                {
                    return MControl.GridView.RM.GetString("GridCheckBoxCell_ClipboardUnchecked");
                }
                return CheckState.Unchecked;
            }
            if (!this.FormattedValueType.IsAssignableFrom(defaultBooleanType))
            {
                return null;
            }
            bool flag = (this.flags & 0x10) != 0;
            if ((context & GridDataErrorContexts.ClipboardContent) != 0)
            {
                return MControl.GridView.RM.GetString(flag ? "GridCheckBoxCell_ClipboardTrue" : "GridCheckBoxCell_ClipboardFalse");
            }
            return flag;
        }

        protected override Rectangle GetErrorIconBounds(Graphics graphics, GridCellStyle cellStyle, int rowIndex)
        {
            GridAdvancedBorderStyle style;
            GridElementStates states;
            Rectangle rectangle;
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if (((base.Grid == null) || (rowIndex < 0)) || (((base.OwningColumn == null) || !base.Grid.ShowCellErrors) || string.IsNullOrEmpty(this.GetErrorText(rowIndex))))
            {
                return Rectangle.Empty;
            }
            Point currentCellAddress = base.Grid.CurrentCellAddress;
            if (((currentCellAddress.X == base.ColumnIndex) && (currentCellAddress.Y == rowIndex)) && base.Grid.IsCurrentCellInEditMode)
            {
                return Rectangle.Empty;
            }
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, null, this.GetErrorText(rowIndex), cellStyle, style, GridPaintParts.ContentForeground, false, true, false);
        }

        /// <summary>Gets the formatted value of the cell's data. </summary>
        /// <returns>The value of the cell's data after formatting has been applied or null if the cell is not part of a <see cref="T:MControl.GridView.Grid"></see> control.</returns>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> in effect for the cell.</param>
        /// <param name="context">A bitwise combination of <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values describing the context in which the formatted value is needed.</param>
        /// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        /// <param name="value">The value to be formatted. </param>
        /// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
        protected override object GetFormattedValue(object value, int rowIndex, ref GridCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, GridDataErrorContexts context)
        {
            if (value != null)
            {
                if (this.ThreeState)
                {
                    if (value.Equals(this.TrueValue) || ((value is int) && (((int) value) == 1)))
                    {
                        value = CheckState.Checked;
                    }
                    else if (value.Equals(this.FalseValue) || ((value is int) && (((int) value) == 0)))
                    {
                        value = CheckState.Unchecked;
                    }
                    else if (value.Equals(this.IndeterminateValue) || ((value is int) && (((int) value) == 2)))
                    {
                        value = CheckState.Indeterminate;
                    }
                }
                else if (value.Equals(this.TrueValue) || ((value is int) && (((int) value) != 0)))
                {
                    value = true;
                }
                else if (value.Equals(this.FalseValue) || ((value is int) && (((int) value) == 0)))
                {
                    value = false;
                }
            }
            object obj2 = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
            if ((obj2 == null) || ((context & GridDataErrorContexts.ClipboardContent) == 0))
            {
                return obj2;
            }
            if (obj2 is bool)
            {
                if ((bool) obj2)
                {
                    return MControl.GridView.RM.GetString(this.ThreeState ? "GridCheckBoxCell_ClipboardChecked" : "GridCheckBoxCell_ClipboardTrue");
                }
                return MControl.GridView.RM.GetString(this.ThreeState ? "GridCheckBoxCell_ClipboardUnchecked" : "GridCheckBoxCell_ClipboardFalse");
            }
            if (!(obj2 is CheckState))
            {
                return obj2;
            }
            switch (((CheckState) obj2))
            {
                case CheckState.Checked:
                    return MControl.GridView.RM.GetString(this.ThreeState ? "GridCheckBoxCell_ClipboardChecked" : "GridCheckBoxCell_ClipboardTrue");

                case CheckState.Unchecked:
                    return MControl.GridView.RM.GetString(this.ThreeState ? "GridCheckBoxCell_ClipboardUnchecked" : "GridCheckBoxCell_ClipboardFalse");
            }
            return MControl.GridView.RM.GetString("GridCheckBoxCell_ClipboardIndeterminate");
        }

        protected override Size GetPreferredSize(Graphics graphics, GridCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            Size size;
            int num3;
            if (base.Grid == null)
            {
                return new Size(-1, -1);
            }
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            GridFreeDimension freeDimensionFromConstraint = GridCell.GetFreeDimensionFromConstraint(constraintSize);
            Rectangle stdBorderWidths = base.StdBorderWidths;
            int num = (stdBorderWidths.Left + stdBorderWidths.Width) + cellStyle.Padding.Horizontal;
            int num2 = (stdBorderWidths.Top + stdBorderWidths.Height) + cellStyle.Padding.Vertical;
            if (!base.Grid.ApplyVisualStylesToInnerCells)
            {
                switch (this.FlatStyle)
                {
                    case System.Windows.Forms.FlatStyle.Flat:
                        num3 = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal).Width - 3;
                        goto Label_01A9;

                    case System.Windows.Forms.FlatStyle.Popup:
                        num3 = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal).Width - 2;
                        goto Label_01A9;
                }
                num3 = ((SystemInformation.Border3DSize.Width * 2) + 9) + 4;
            }
            else
            {
                Size glyphSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);
                switch (this.FlatStyle)
                {
                    case System.Windows.Forms.FlatStyle.Flat:
                        glyphSize.Width -= 3;
                        glyphSize.Height -= 3;
                        break;

                    case System.Windows.Forms.FlatStyle.Popup:
                        glyphSize.Width -= 2;
                        glyphSize.Height -= 2;
                        break;
                }
                switch (freeDimensionFromConstraint)
                {
                    case GridFreeDimension.Height:
                        size = new Size(0, (glyphSize.Height + num2) + 4);
                        goto Label_01EA;

                    case GridFreeDimension.Width:
                        size = new Size((glyphSize.Width + num) + 4, 0);
                        goto Label_01EA;

                    default:
                        size = new Size((glyphSize.Width + num) + 4, (glyphSize.Height + num2) + 4);
                        goto Label_01EA;
                }
            }
        Label_01A9:
            switch (freeDimensionFromConstraint)
            {
                case GridFreeDimension.Height:
                    size = new Size(0, num3 + num2);
                    goto Label_01EA;

                case GridFreeDimension.Width:
                    size = new Size(num3 + num, 0);
                    goto Label_01EA;

                default:
                    size = new Size(num3 + num, num3 + num2);
                    goto Label_01EA;
            }
        Label_01EA:
            if (base.Grid.ShowCellErrors)
            {
                if (freeDimensionFromConstraint != GridFreeDimension.Height)
                {
                    size.Width = Math.Max(size.Width, (num + 8) + 12);
                }
                if (freeDimensionFromConstraint != GridFreeDimension.Width)
                {
                    size.Height = Math.Max(size.Height, (num2 + 8) + 11);
                }
            }
            return size;
        }

        /// <summary>Indicates whether the row containing the cell is unshared when a key is pressed while the cell has focus.</summary>
        /// <returns>true if the SPACE key is pressed and the CTRL, ALT, and SHIFT keys are all not pressed; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains data about the key press. </param>
        /// <param name="rowIndex">The index of the row containing the cell. </param>
        protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return ((((e.KeyCode == Keys.Space) && !e.Alt) && !e.Control) && !e.Shift);
        }

        /// <summary>Indicates whether the row containing the cell is unshared when a key is released while the cell has focus.</summary>
        /// <returns>true if the SPACE key is released; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains data about the key press. </param>
        /// <param name="rowIndex">The index of the row containing the cell. </param>
        protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return (e.KeyCode == Keys.Space);
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is pressed while the pointer is over the cell.</summary>
        /// <returns>Always true.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains data about the mouse click.</param>
        protected override bool MouseDownUnsharesRow(GridCellMouseEventArgs e)
        {
            return (e.Button == MouseButtons.Left);
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer moves over the cell.</summary>
        /// <returns>true if the cell was the last cell receiving a mouse click; otherwise, false.</returns>
        /// <param name="rowIndex">The index of the row containing the cell.</param>
        protected override bool MouseEnterUnsharesRow(int rowIndex)
        {
            return ((base.ColumnIndex == base.Grid.MouseDownCellAddress.X) && (rowIndex == base.Grid.MouseDownCellAddress.Y));
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer leaves the cell.</summary>
        /// <returns>true if the button is not in the normal state; false if the button is in the pressed state.</returns>
        /// <param name="rowIndex">The index of the row containing the cell.</param>
        protected override bool MouseLeaveUnsharesRow(int rowIndex)
        {
            return ((this.ButtonState & System.Windows.Forms.ButtonState.Pushed) != System.Windows.Forms.ButtonState.Normal);
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is released while the pointer is over the cell.</summary>
        /// <returns>Always true.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains data about the mouse click.</param>
        protected override bool MouseUpUnsharesRow(GridCellMouseEventArgs e)
        {
            return (e.Button == MouseButtons.Left);
        }

        private void NotifyGridOfValueChange()
        {
            this.flags = (byte) (this.flags | 2);
            base.Grid.NotifyCurrentCellDirty(true);
        }

        private void OnCommonContentClick(GridCellEventArgs e)
        {
            if (base.Grid != null)
            {
                Point currentCellAddress = base.Grid.CurrentCellAddress;
                if (((currentCellAddress.X == base.ColumnIndex) && (currentCellAddress.Y == e.RowIndex)) && (base.Grid.IsCurrentCellInEditMode && this.SwitchFormattedValue()))
                {
                    this.NotifyGridOfValueChange();
                }
            }
        }

        protected override void OnContentClick(GridCellEventArgs e)
        {
            this.OnCommonContentClick(e);
        }

        protected override void OnContentDoubleClick(GridCellEventArgs e)
        {
            this.OnCommonContentClick(e);
        }

        /// <summary>Called when a character key is pressed while the focus is on a cell.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data</param>
        /// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            if ((base.Grid != null) && (((e.KeyCode == Keys.Space) && !e.Alt) && (!e.Control && !e.Shift)))
            {
                this.UpdateButtonState(this.ButtonState | System.Windows.Forms.ButtonState.Checked, rowIndex);
                e.Handled = true;
            }
        }

        /// <summary>Called when a character key is released while the focus is on a cell.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data</param>
        /// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
        protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
        {
            if ((base.Grid != null) && (e.KeyCode == Keys.Space))
            {
                this.UpdateButtonState(this.ButtonState & ~System.Windows.Forms.ButtonState.Checked, rowIndex);
                if ((!e.Alt && !e.Control) && !e.Shift)
                {
                    base.RaiseCellClick(new GridCellEventArgs(base.ColumnIndex, rowIndex));
                    if (((base.Grid != null) && (base.ColumnIndex < base.Grid.Columns.Count)) && (rowIndex < base.Grid.Rows.Count))
                    {
                        base.RaiseCellContentClick(new GridCellEventArgs(base.ColumnIndex, rowIndex));
                    }
                    e.Handled = true;
                }
            }
        }

        /// <summary>Called when the focus moves from a cell.</summary>
        /// <param name="throughMouseClick">true if the cell was left as a result of user mouse click rather than a programmatic cell change; otherwise, false.</param>
        /// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
        protected override void OnLeave(int rowIndex, bool throughMouseClick)
        {
            if ((base.Grid != null) && (this.ButtonState != System.Windows.Forms.ButtonState.Normal))
            {
                this.UpdateButtonState(System.Windows.Forms.ButtonState.Normal, rowIndex);
            }
        }

        /// <summary>Called when the mouse button is held down while the pointer is on a cell.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseDown(GridCellMouseEventArgs e)
        {
            if ((base.Grid != null) && ((e.Button == MouseButtons.Left) && mouseInContentBounds))
            {
                this.UpdateButtonState(this.ButtonState | System.Windows.Forms.ButtonState.Pushed, e.RowIndex);
            }
        }

        /// <summary>Called when the mouse pointer moves from a cell.</summary>
        /// <param name="rowIndex">The row index of the current cell or -1 if the cell is not owned by a row.</param>
        protected override void OnMouseLeave(int rowIndex)
        {
            if (base.Grid != null)
            {
                if (mouseInContentBounds)
                {
                    mouseInContentBounds = false;
                    if (((base.ColumnIndex >= 0) && (rowIndex >= 0)) && ((base.Grid.ApplyVisualStylesToInnerCells || (this.FlatStyle == System.Windows.Forms.FlatStyle.Flat)) || (this.FlatStyle == System.Windows.Forms.FlatStyle.Popup)))
                    {
                        base.Grid.InvalidateCell(base.ColumnIndex, rowIndex);
                    }
                }
                if ((((this.ButtonState & System.Windows.Forms.ButtonState.Pushed) != System.Windows.Forms.ButtonState.Normal) && (base.ColumnIndex == base.Grid.MouseDownCellAddress.X)) && (rowIndex == base.Grid.MouseDownCellAddress.Y))
                {
                    this.UpdateButtonState(this.ButtonState & ~System.Windows.Forms.ButtonState.Pushed, rowIndex);
                }
            }
        }

        /// <summary>Called when the mouse pointer moves within a cell.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseMove(GridCellMouseEventArgs e)
        {
            if (base.Grid != null)
            {
                bool mouseInContentBounds = GridCheckBoxCell.mouseInContentBounds;
                GridCheckBoxCell.mouseInContentBounds = base.GetContentBounds(e.RowIndex).Contains(e.X, e.Y);
                if (mouseInContentBounds != GridCheckBoxCell.mouseInContentBounds)
                {
                    if ((base.Grid.ApplyVisualStylesToInnerCells || (this.FlatStyle == System.Windows.Forms.FlatStyle.Flat)) || (this.FlatStyle == System.Windows.Forms.FlatStyle.Popup))
                    {
                        base.Grid.InvalidateCell(base.ColumnIndex, e.RowIndex);
                    }
                    if (((e.ColumnIndex == base.Grid.MouseDownCellAddress.X) && (e.RowIndex == base.Grid.MouseDownCellAddress.Y)) && (Control.MouseButtons == MouseButtons.Left))
                    {
                        if ((((this.ButtonState & System.Windows.Forms.ButtonState.Pushed) == System.Windows.Forms.ButtonState.Normal) && GridCheckBoxCell.mouseInContentBounds) && base.Grid.CellMouseDownInContentBounds)
                        {
                            this.UpdateButtonState(this.ButtonState | System.Windows.Forms.ButtonState.Pushed, e.RowIndex);
                        }
                        else if (((this.ButtonState & System.Windows.Forms.ButtonState.Pushed) != System.Windows.Forms.ButtonState.Normal) && !GridCheckBoxCell.mouseInContentBounds)
                        {
                            this.UpdateButtonState(this.ButtonState & ~System.Windows.Forms.ButtonState.Pushed, e.RowIndex);
                        }
                    }
                }
                base.OnMouseMove(e);
            }
        }

        /// <summary>Called when the mouse button is released while the pointer is on a cell. </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseUp(GridCellMouseEventArgs e)
        {
            if ((base.Grid != null) && (e.Button == MouseButtons.Left))
            {
                this.UpdateButtonState(this.ButtonState & ~System.Windows.Forms.ButtonState.Pushed, e.RowIndex);
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates elementState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, elementState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
        }

        private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates elementState, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
        {
            Rectangle checkBounds;
            CheckState @unchecked;
            System.Windows.Forms.ButtonState normal;
            Size glyphSize;
            if (paint && GridCell.PaintBorder(paintParts))
            {
                this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            Rectangle rectangle2 = cellBounds;
            Rectangle rectangle3 = this.BorderWidths(advancedBorderStyle);
            rectangle2.Offset(rectangle3.X, rectangle3.Y);
            rectangle2.Width -= rectangle3.Right;
            rectangle2.Height -= rectangle3.Bottom;
            bool flag = (elementState & GridElementStates.Selected) != GridElementStates.None;
            bool isMixed = false;
            bool flag3 = true;
            Point currentCellAddress = base.Grid.CurrentCellAddress;
            if (((currentCellAddress.X == base.ColumnIndex) && (currentCellAddress.Y == rowIndex)) && base.Grid.IsCurrentCellInEditMode)
            {
                flag3 = false;
            }
            if ((formattedValue != null) && (formattedValue is CheckState))
            {
                @unchecked = (CheckState) formattedValue;
                normal = (@unchecked == CheckState.Unchecked) ? System.Windows.Forms.ButtonState.Normal : System.Windows.Forms.ButtonState.Checked;
                isMixed = @unchecked == CheckState.Indeterminate;
            }
            else if ((formattedValue != null) && (formattedValue is bool))
            {
                if ((bool) formattedValue)
                {
                    @unchecked = CheckState.Checked;
                    normal = System.Windows.Forms.ButtonState.Checked;
                }
                else
                {
                    @unchecked = CheckState.Unchecked;
                    normal = System.Windows.Forms.ButtonState.Normal;
                }
            }
            else
            {
                normal = System.Windows.Forms.ButtonState.Normal;
                @unchecked = CheckState.Unchecked;
            }
            if ((this.ButtonState & (System.Windows.Forms.ButtonState.Checked | System.Windows.Forms.ButtonState.Pushed)) != System.Windows.Forms.ButtonState.Normal)
            {
                normal |= System.Windows.Forms.ButtonState.Pushed;
            }
            SolidBrush cachedBrush = base.Grid.GetCachedBrush((GridCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
            if ((paint && GridCell.PaintBackground(paintParts)) && (cachedBrush.Color.A == 0xff))
            {
                g.FillRectangle(cachedBrush, rectangle2);
            }
            if (cellStyle.Padding != Padding.Empty)
            {
                if (base.Grid.RightToLeftInternal)
                {
                    rectangle2.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
                }
                else
                {
                    rectangle2.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
                }
                rectangle2.Width -= cellStyle.Padding.Horizontal;
                rectangle2.Height -= cellStyle.Padding.Vertical;
            }
            if (((paint && GridCell.PaintFocus(paintParts)) && (base.Grid.ShowFocusCues && base.Grid.Focused)) && ((currentCellAddress.X == base.ColumnIndex) && (currentCellAddress.Y == rowIndex)))
            {
                ControlPaint.DrawFocusRectangle(g, rectangle2, Color.Empty, cachedBrush.Color);
            }
            Rectangle cellValueBounds = rectangle2;
            rectangle2.Inflate(-2, -2);
            CheckBoxState uncheckedNormal = CheckBoxState.UncheckedNormal;
            if (base.Grid.ApplyVisualStylesToInnerCells)
            {
                uncheckedNormal = CheckBoxRenderer.ConvertFromButtonState(normal, isMixed, ((base.Grid.MouseEnteredCellAddress.Y == rowIndex) && (base.Grid.MouseEnteredCellAddress.X == base.ColumnIndex)) && mouseInContentBounds);
                glyphSize = CheckBoxRenderer.GetGlyphSize(g, uncheckedNormal);
                switch (this.FlatStyle)
                {
                    case System.Windows.Forms.FlatStyle.Flat:
                        glyphSize.Width -= 3;
                        glyphSize.Height -= 3;
                        goto Label_03EF;

                    case System.Windows.Forms.FlatStyle.Popup:
                        glyphSize.Width -= 2;
                        glyphSize.Height -= 2;
                        goto Label_03EF;

                    case System.Windows.Forms.FlatStyle.Standard:
                    case System.Windows.Forms.FlatStyle.System:
                        goto Label_03EF;
                }
            }
            else
            {
                switch (this.FlatStyle)
                {
                    case System.Windows.Forms.FlatStyle.Flat:
                        glyphSize = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal);
                        glyphSize.Width -= 3;
                        glyphSize.Height -= 3;
                        goto Label_03EF;

                    case System.Windows.Forms.FlatStyle.Popup:
                        glyphSize = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal);
                        glyphSize.Width -= 2;
                        glyphSize.Height -= 2;
                        goto Label_03EF;
                }
                glyphSize = new Size((SystemInformation.Border3DSize.Width * 2) + 9, (SystemInformation.Border3DSize.Width * 2) + 9);
            }
        Label_03EF:
            if (((rectangle2.Width >= glyphSize.Width) && (rectangle2.Height >= glyphSize.Height)) && (paint || computeContentBounds))
            {
                int x = 0;
                int y = 0;
                if ((!base.Grid.RightToLeftInternal && ((cellStyle.Alignment & anyRight) != GridContentAlignment.NotSet)) || (base.Grid.RightToLeftInternal && ((cellStyle.Alignment & anyLeft) != GridContentAlignment.NotSet)))
                {
                    x = rectangle2.Right - glyphSize.Width;
                }
                else if ((cellStyle.Alignment & anyCenter) != GridContentAlignment.NotSet)
                {
                    x = rectangle2.Left + ((rectangle2.Width - glyphSize.Width) / 2);
                }
                else
                {
                    x = rectangle2.Left;
                }
                if ((cellStyle.Alignment & anyBottom) != GridContentAlignment.NotSet)
                {
                    y = rectangle2.Bottom - glyphSize.Height;
                }
                else if ((cellStyle.Alignment & anyMiddle) != GridContentAlignment.NotSet)
                {
                    y = rectangle2.Top + ((rectangle2.Height - glyphSize.Height) / 2);
                }
                else
                {
                    y = rectangle2.Top;
                }
                if ((base.Grid.ApplyVisualStylesToInnerCells && (this.FlatStyle != System.Windows.Forms.FlatStyle.Flat)) && (this.FlatStyle != System.Windows.Forms.FlatStyle.Popup))
                {
                    if (paint && GridCell.PaintContentForeground(paintParts))
                    {
                        GridCheckBoxCellRenderer.DrawCheckBox(g, new Rectangle(x, y, glyphSize.Width, glyphSize.Height), (int) uncheckedNormal);
                    }
                    checkBounds = new Rectangle(x, y, glyphSize.Width, glyphSize.Height);
                }
                else if ((this.FlatStyle == System.Windows.Forms.FlatStyle.System) || (this.FlatStyle == System.Windows.Forms.FlatStyle.Standard))
                {
                    if (paint && GridCell.PaintContentForeground(paintParts))
                    {
                        if (isMixed)
                        {
                            ControlPaint.DrawMixedCheckBox(g, x, y, glyphSize.Width, glyphSize.Height, normal);
                        }
                        else
                        {
                            ControlPaint.DrawCheckBox(g, x, y, glyphSize.Width, glyphSize.Height, normal);
                        }
                    }
                    checkBounds = new Rectangle(x, y, glyphSize.Width, glyphSize.Height);
                }
                else if (this.FlatStyle == System.Windows.Forms.FlatStyle.Flat)
                {
                    Rectangle bounds = new Rectangle(x, y, glyphSize.Width, glyphSize.Height);
                    SolidBrush brush2 = null;
                    SolidBrush brush3 = null;
                    Color empty = Color.Empty;
                    if (paint && GridCell.PaintContentForeground(paintParts))
                    {
                        brush2 = base.Grid.GetCachedBrush(flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
                        brush3 = base.Grid.GetCachedBrush((GridCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
                        empty = ControlPaint.LightLight(brush3.Color);
                        if (((base.Grid.MouseEnteredCellAddress.Y == rowIndex) && (base.Grid.MouseEnteredCellAddress.X == base.ColumnIndex)) && mouseInContentBounds)
                        {
                            float percentage = 0.9f;
                            if (empty.GetBrightness() < 0.5)
                            {
                                percentage = 1.2f;
                            }
                            empty = Color.FromArgb(ButtonBaseAdapter.ColorOptions.Adjust255(percentage, empty.R), ButtonBaseAdapter.ColorOptions.Adjust255(percentage, empty.G), ButtonBaseAdapter.ColorOptions.Adjust255(percentage, empty.B));
                        }
                        empty = g.GetNearestColor(empty);
                        using (Pen pen = new Pen(brush2.Color))
                        {
                            g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);
                            g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1);
                        }
                    }
                    bounds.Inflate(-1, -1);
                    bounds.Width++;
                    bounds.Height++;
                    if (paint && GridCell.PaintContentForeground(paintParts))
                    {
                        if (@unchecked == CheckState.Indeterminate)
                        {
                            ButtonBaseAdapter.DrawDitheredFill(g, brush3.Color, empty, bounds);
                        }
                        else
                        {
                            using (SolidBrush brush4 = new SolidBrush(empty))
                            {
                                g.FillRectangle(brush4, bounds);
                            }
                        }
                        if (@unchecked != CheckState.Unchecked)
                        {
                            Rectangle destination = new Rectangle(x - 1, y - 1, glyphSize.Width + 3, glyphSize.Height + 3);
                            destination.Width++;
                            destination.Height++;
                            if (((checkImage == null) || (checkImage.Width != destination.Width)) || (checkImage.Height != destination.Height))
                            {
                                if (checkImage != null)
                                {
                                    checkImage.Dispose();
                                    checkImage = null;
                                }
                                MControl.Util.NativeMethods.RECT rect = MControl.Util.NativeMethods.RECT.FromXYWH(0, 0, destination.Width, destination.Height);
                                Bitmap image = new Bitmap(destination.Width, destination.Height);
                                using (Graphics graphics = Graphics.FromImage(image))
                                {
                                    graphics.Clear(Color.Transparent);
                                    IntPtr hdc = graphics.GetHdc();
                                    try
                                    {
                                        System.Windows.Forms.SafeNativeMethods.DrawFrameControl(new HandleRef(graphics, hdc), ref rect, 2, 1);
                                    }
                                    finally
                                    {
                                        graphics.ReleaseHdcInternal(hdc);
                                    }
                                }
                                image.MakeTransparent();
                                checkImage = image;
                            }
                            destination.Y--;
                            ControlPaint.DrawImageColorized(g, checkImage, destination, (@unchecked == CheckState.Indeterminate) ? ControlPaint.LightLight(brush2.Color) : brush2.Color);
                        }
                    }
                    checkBounds = bounds;
                }
                else
                {
                    Rectangle clientRectangle = new Rectangle(x, y, glyphSize.Width - 1, glyphSize.Height - 1);
                    clientRectangle.Y -= 3;
                    if ((this.ButtonState & (System.Windows.Forms.ButtonState.Checked | System.Windows.Forms.ButtonState.Pushed)) != System.Windows.Forms.ButtonState.Normal)
                    {
                        ButtonBaseAdapter.LayoutOptions options = CheckBoxPopupAdapter.PaintPopupLayout(g, true, glyphSize.Width, clientRectangle, Padding.Empty, false, cellStyle.Font, string.Empty, base.Grid.Enabled, GridUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.Grid.RightToLeft);
                        options.everettButtonCompat = false;
                        ButtonBaseAdapter.LayoutData layout = options.Layout();
                        if (paint && GridCell.PaintContentForeground(paintParts))
                        {
                            ButtonBaseAdapter.ColorData colors = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.Grid.Enabled).Calculate();
                            CheckBoxBaseAdapter.DrawCheckBackground(base.Grid.Enabled, @unchecked, g, layout.checkBounds, colors.windowText, colors.buttonFace, true, colors);
                            CheckBoxBaseAdapter.DrawPopupBorder(g, layout.checkBounds, colors);
                            CheckBoxBaseAdapter.DrawCheckOnly(glyphSize.Width, (@unchecked == CheckState.Checked) || (@unchecked == CheckState.Indeterminate), base.Grid.Enabled, @unchecked, g, layout, colors, colors.windowText, colors.buttonFace, true);
                        }
                        checkBounds = layout.checkBounds;
                    }
                    else if (((base.Grid.MouseEnteredCellAddress.Y == rowIndex) && (base.Grid.MouseEnteredCellAddress.X == base.ColumnIndex)) && mouseInContentBounds)
                    {
                        ButtonBaseAdapter.LayoutOptions options2 = CheckBoxPopupAdapter.PaintPopupLayout(g, true, glyphSize.Width, clientRectangle, Padding.Empty, false, cellStyle.Font, string.Empty, base.Grid.Enabled, GridUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.Grid.RightToLeft);
                        options2.everettButtonCompat = false;
                        ButtonBaseAdapter.LayoutData data3 = options2.Layout();
                        if (paint && GridCell.PaintContentForeground(paintParts))
                        {
                            ButtonBaseAdapter.ColorData data4 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.Grid.Enabled).Calculate();
                            CheckBoxBaseAdapter.DrawCheckBackground(base.Grid.Enabled, @unchecked, g, data3.checkBounds, data4.windowText, data4.options.highContrast ? data4.buttonFace : data4.highlight, true, data4);
                            CheckBoxBaseAdapter.DrawPopupBorder(g, data3.checkBounds, data4);
                            CheckBoxBaseAdapter.DrawCheckOnly(glyphSize.Width, (@unchecked == CheckState.Checked) || (@unchecked == CheckState.Indeterminate), base.Grid.Enabled, @unchecked, g, data3, data4, data4.windowText, data4.highlight, true);
                        }
                        checkBounds = data3.checkBounds;
                    }
                    else
                    {
                        ButtonBaseAdapter.LayoutOptions options3 = CheckBoxPopupAdapter.PaintPopupLayout(g, false, glyphSize.Width, clientRectangle, Padding.Empty, false, cellStyle.Font, string.Empty, base.Grid.Enabled, GridUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.Grid.RightToLeft);
                        options3.everettButtonCompat = false;
                        ButtonBaseAdapter.LayoutData data5 = options3.Layout();
                        if (paint && GridCell.PaintContentForeground(paintParts))
                        {
                            ButtonBaseAdapter.ColorData data6 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.Grid.Enabled).Calculate();
                            CheckBoxBaseAdapter.DrawCheckBackground(base.Grid.Enabled, @unchecked, g, data5.checkBounds, data6.windowText, data6.options.highContrast ? data6.buttonFace : data6.highlight, true, data6);
                            ButtonBaseAdapter.DrawFlatBorder(g, data5.checkBounds, data6.buttonShadow);
                            CheckBoxBaseAdapter.DrawCheckOnly(glyphSize.Width, (@unchecked == CheckState.Checked) || (@unchecked == CheckState.Indeterminate), base.Grid.Enabled, @unchecked, g, data5, data6, data6.windowText, data6.highlight, true);
                        }
                        checkBounds = data5.checkBounds;
                    }
                }
            }
            else if (computeErrorIconBounds)
            {
                if (!string.IsNullOrEmpty(errorText))
                {
                    checkBounds = base.ComputeErrorIconBounds(cellValueBounds);
                }
                else
                {
                    checkBounds = Rectangle.Empty;
                }
            }
            else
            {
                checkBounds = Rectangle.Empty;
            }
            if ((paint && GridCell.PaintErrorIcon(paintParts)) && (flag3 && base.Grid.ShowCellErrors))
            {
                base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
            }
            return checkBounds;
        }

        /// <summary>Converts a value formatted for display to an actual cell value.</summary>
        /// <returns>The cell value.</returns>
        /// <param name="formattedValue">The display value of the cell.</param>
        /// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> for the display value type, or null to use the default converter.</param>
        /// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> for the cell value type, or null to use the default converter.</param>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> in effect for the cell.</param>
        /// <exception cref="T:System.FormatException">The <see cref="P:MControl.GridView.GridCell.FormattedValueType"></see> property value is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">cellStyle is null.</exception>
        /// <exception cref="T:System.ArgumentException">formattedValue is null.- or -The type of formattedValue does not match the type indicated by the <see cref="P:MControl.GridView.GridCell.FormattedValueType"></see> property. </exception>
        public override object ParseFormattedValue(object formattedValue, GridCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            if (formattedValue != null)
            {
                if (formattedValue is bool)
                {
                    if (!((bool) formattedValue))
                    {
                        if (this.FalseValue != null)
                        {
                            return this.FalseValue;
                        }
                        if ((this.ValueType != null) && this.ValueType.IsAssignableFrom(defaultBooleanType))
                        {
                            return false;
                        }
                        if ((this.ValueType != null) && this.ValueType.IsAssignableFrom(defaultCheckStateType))
                        {
                            return CheckState.Unchecked;
                        }
                    }
                    else
                    {
                        if (this.TrueValue != null)
                        {
                            return this.TrueValue;
                        }
                        if ((this.ValueType != null) && this.ValueType.IsAssignableFrom(defaultBooleanType))
                        {
                            return true;
                        }
                        if ((this.ValueType != null) && this.ValueType.IsAssignableFrom(defaultCheckStateType))
                        {
                            return CheckState.Checked;
                        }
                    }
                }
                else if (formattedValue is CheckState)
                {
                    switch (((CheckState) formattedValue))
                    {
                        case CheckState.Unchecked:
                            if (this.FalseValue == null)
                            {
                                if ((this.ValueType != null) && this.ValueType.IsAssignableFrom(defaultBooleanType))
                                {
                                    return false;
                                }
                                if ((this.ValueType != null) && this.ValueType.IsAssignableFrom(defaultCheckStateType))
                                {
                                    return CheckState.Unchecked;
                                }
                                goto Label_01C8;
                            }
                            return this.FalseValue;

                        case CheckState.Checked:
                            if (this.TrueValue == null)
                            {
                                if ((this.ValueType != null) && this.ValueType.IsAssignableFrom(defaultBooleanType))
                                {
                                    return true;
                                }
                                if ((this.ValueType != null) && this.ValueType.IsAssignableFrom(defaultCheckStateType))
                                {
                                    return CheckState.Checked;
                                }
                                goto Label_01C8;
                            }
                            return this.TrueValue;

                        case CheckState.Indeterminate:
                            if (this.IndeterminateValue == null)
                            {
                                if ((this.ValueType != null) && this.ValueType.IsAssignableFrom(defaultCheckStateType))
                                {
                                    return CheckState.Indeterminate;
                                }
                                goto Label_01C8;
                            }
                            return this.IndeterminateValue;
                    }
                }
            }
        Label_01C8:
            return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
        }

        /// <summary>This method is not meaningful for this type.</summary>
        /// <param name="selectAll">This parameter is ignored.</param>
        public virtual void PrepareEditingCellForEdit(bool selectAll)
        {
        }

        private bool SwitchFormattedValue()
        {
            if (this.FormattedValueType == null)
            {
                return false;
            }
            IGridEditingCell cell = this;
            if (this.FormattedValueType.IsAssignableFrom(typeof(CheckState)))
            {
                if ((this.flags & 0x10) != 0)
                {
                    cell.EditingCellFormattedValue = CheckState.Indeterminate;
                }
                else if ((this.flags & 0x20) != 0)
                {
                    cell.EditingCellFormattedValue = CheckState.Unchecked;
                }
                else
                {
                    cell.EditingCellFormattedValue = CheckState.Checked;
                }
            }
            else if (this.FormattedValueType.IsAssignableFrom(defaultBooleanType))
            {
                cell.EditingCellFormattedValue = !((bool) cell.GetEditingCellFormattedValue(GridDataErrorContexts.Formatting));
            }
            return true;
        }

        /// <summary>Returns the string representation of the cell.</summary>
        /// <returns>A <see cref="T:System.String"></see> that represents the current cell.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridCheckBoxCell { ColumnIndex=" + base.ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + base.RowIndex.ToString(CultureInfo.CurrentCulture) + " }");
        }

        private void UpdateButtonState(System.Windows.Forms.ButtonState newButtonState, int rowIndex)
        {
            this.ButtonState = newButtonState;
            base.Grid.InvalidateCell(base.ColumnIndex, rowIndex);
        }

        private System.Windows.Forms.ButtonState ButtonState
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropButtonCellState, out flag);
                if (flag)
                {
                    return (System.Windows.Forms.ButtonState) integer;
                }
                return System.Windows.Forms.ButtonState.Normal;
            }
            set
            {
                if (this.ButtonState != value)
                {
                    base.Properties.SetInteger(PropButtonCellState, (int) value);
                }
            }
        }

        /// <summary>Gets or sets the formatted value of the control hosted by the cell when it is in edit mode.</summary>
        /// <returns>An <see cref="T:System.Object"></see> representing the cell's value.</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="P:MControl.GridView.GridCheckBoxCell.FormattedValueType"></see> property value is null.</exception>
        /// <exception cref="T:System.ArgumentException">The <see cref="P:MControl.GridView.GridCheckBoxCell.FormattedValueType"></see> property value is null.-or-The assigned value is null or is not of the type indicated by the <see cref="P:MControl.GridView.GridCheckBoxCell.FormattedValueType"></see> property.-or- The assigned value is not of type <see cref="T:System.Boolean"></see> nor of type <see cref="T:System.Windows.Forms.CheckState"></see>.</exception>
        public virtual object EditingCellFormattedValue
        {
            get
            {
                return this.GetEditingCellFormattedValue(GridDataErrorContexts.Formatting);
            }
            set
            {
                if (this.FormattedValueType == null)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("GridCell_FormattedValueTypeNull"));
                }
                if ((value == null) || !this.FormattedValueType.IsAssignableFrom(value.GetType()))
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("GridCheckBoxCell_InvalidValueType"));
                }
                if (value is CheckState)
                {
                    if (((CheckState) value) == CheckState.Checked)
                    {
                        this.flags = (byte) (this.flags | 0x10);
                        this.flags = (byte) (this.flags & -33);
                    }
                    else if (((CheckState) value) == CheckState.Indeterminate)
                    {
                        this.flags = (byte) (this.flags | 0x20);
                        this.flags = (byte) (this.flags & -17);
                    }
                    else
                    {
                        this.flags = (byte) (this.flags & -17);
                        this.flags = (byte) (this.flags & -33);
                    }
                }
                else
                {
                    if (!(value is bool))
                    {
                        throw new ArgumentException(MControl.GridView.RM.GetString("GridCheckBoxCell_InvalidValueType"));
                    }
                    if ((bool) value)
                    {
                        this.flags = (byte) (this.flags | 0x10);
                    }
                    else
                    {
                        this.flags = (byte) (this.flags & -17);
                    }
                    this.flags = (byte) (this.flags & -33);
                }
            }
        }

        /// <summary>Gets or sets a flag indicating that the value has been changed for this cell.</summary>
        /// <returns>true if the cell's value has changed; otherwise, false.</returns>
        public virtual bool EditingCellValueChanged
        {
            get
            {
                return ((this.flags & 2) != 0);
            }
            set
            {
                if (value)
                {
                    this.flags = (byte) (this.flags | 2);
                }
                else
                {
                    this.flags = (byte) (this.flags & -3);
                }
            }
        }

        /// <summary>Gets the type of the cell's hosted editing control.</summary>
        /// <returns>The <see cref="T:System.Type"></see> of the underlying editing control.</returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type EditType
        {
            get
            {
                return null;
            }
        }

        /// <summary>Gets or sets the underlying value corresponding to a cell value of false.</summary>
        /// <returns>An <see cref="T:System.Object"></see> corresponding to a cell value of false. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue((string) null)]
        public object FalseValue
        {
            get
            {
                return base.Properties.GetObject(PropFalseValue);
            }
            set
            {
                if ((value != null) || base.Properties.ContainsObject(PropFalseValue))
                {
                    base.Properties.SetObject(PropFalseValue, value);
                    if (base.Grid != null)
                    {
                        if (base.RowIndex != -1)
                        {
                            base.Grid.InvalidateCell(this);
                        }
                        else
                        {
                            base.Grid.InvalidateColumnInternal(base.ColumnIndex);
                        }
                    }
                }
            }
        }

        internal object FalseValueInternal
        {
            set
            {
                if ((value != null) || base.Properties.ContainsObject(PropFalseValue))
                {
                    base.Properties.SetObject(PropFalseValue, value);
                }
            }
        }

        /// <summary>Gets or sets the flat style appearance of the check box user interface (UI).</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle"></see> values. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard"></see>.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.FlatStyle"></see> value.</exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(2)]
        public System.Windows.Forms.FlatStyle FlatStyle
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropFlatStyle, out flag);
                if (flag)
                {
                    return (System.Windows.Forms.FlatStyle) integer;
                }
                return System.Windows.Forms.FlatStyle.Standard;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 3))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(System.Windows.Forms.FlatStyle));
                }
                if (value != this.FlatStyle)
                {
                    base.Properties.SetInteger(PropFlatStyle, (int) value);
                    base.OnCommonChange();
                }
            }
        }

        internal System.Windows.Forms.FlatStyle FlatStyleInternal
        {
            set
            {
                if (value != this.FlatStyle)
                {
                    base.Properties.SetInteger(PropFlatStyle, (int) value);
                }
            }
        }

        /// <summary>Gets the type of the cell display value. </summary>
        /// <returns>A <see cref="T:System.Type"></see> representing the display type of the cell.</returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type FormattedValueType
        {
            get
            {
                if (this.ThreeState)
                {
                    return defaultCheckStateType;
                }
                return defaultBooleanType;
            }
        }

        /// <summary>Gets or sets the underlying value corresponding to an indeterminate or null cell value.</summary>
        /// <returns>An <see cref="T:System.Object"></see> corresponding to an indeterminate or null cell value. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue((string) null)]
        public object IndeterminateValue
        {
            get
            {
                return base.Properties.GetObject(PropIndeterminateValue);
            }
            set
            {
                if ((value != null) || base.Properties.ContainsObject(PropIndeterminateValue))
                {
                    base.Properties.SetObject(PropIndeterminateValue, value);
                    if (base.Grid != null)
                    {
                        if (base.RowIndex != -1)
                        {
                            base.Grid.InvalidateCell(this);
                        }
                        else
                        {
                            base.Grid.InvalidateColumnInternal(base.ColumnIndex);
                        }
                    }
                }
            }
        }

        internal object IndeterminateValueInternal
        {
            set
            {
                if ((value != null) || base.Properties.ContainsObject(PropIndeterminateValue))
                {
                    base.Properties.SetObject(PropIndeterminateValue, value);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether ternary mode has been enabled for the hosted check box control.</summary>
        /// <returns>true if ternary mode is enabled; false if binary mode is enabled. The default is false.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(false)]
        public bool ThreeState
        {
            get
            {
                return ((this.flags & 1) != 0);
            }
            set
            {
                if (this.ThreeState != value)
                {
                    this.ThreeStateInternal = value;
                    if (base.Grid != null)
                    {
                        if (base.RowIndex != -1)
                        {
                            base.Grid.InvalidateCell(this);
                        }
                        else
                        {
                            base.Grid.InvalidateColumnInternal(base.ColumnIndex);
                        }
                    }
                }
            }
        }

        internal bool ThreeStateInternal
        {
            set
            {
                if (this.ThreeState != value)
                {
                    if (value)
                    {
                        this.flags = (byte) (this.flags | 1);
                    }
                    else
                    {
                        this.flags = (byte) (this.flags & -2);
                    }
                }
            }
        }

        /// <summary>Gets or sets the underlying value corresponding to a cell value of true.</summary>
        /// <returns>An <see cref="T:System.Object"></see> corresponding to a cell value of true. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue((string) null)]
        public object TrueValue
        {
            get
            {
                return base.Properties.GetObject(PropTrueValue);
            }
            set
            {
                if ((value != null) || base.Properties.ContainsObject(PropTrueValue))
                {
                    base.Properties.SetObject(PropTrueValue, value);
                    if (base.Grid != null)
                    {
                        if (base.RowIndex != -1)
                        {
                            base.Grid.InvalidateCell(this);
                        }
                        else
                        {
                            base.Grid.InvalidateColumnInternal(base.ColumnIndex);
                        }
                    }
                }
            }
        }

        internal object TrueValueInternal
        {
            set
            {
                if ((value != null) || base.Properties.ContainsObject(PropTrueValue))
                {
                    base.Properties.SetObject(PropTrueValue, value);
                }
            }
        }

        /// <summary>Gets the data type of the values in the cell.</summary>
        /// <returns>The <see cref="T:System.Type"></see> of the underlying value of the cell.</returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type ValueType
        {
            get
            {
                System.Type valueType = base.ValueType;
                if (valueType != null)
                {
                    return valueType;
                }
                if (this.ThreeState)
                {
                    return defaultCheckStateType;
                }
                return defaultBooleanType;
            }
            set
            {
                base.ValueType = value;
                this.ThreeState = (value != null) && defaultCheckStateType.IsAssignableFrom(value);
            }
        }

        /// <summary>Provides information about a <see cref="T:MControl.GridView.GridCheckBoxCell"></see> to accessibility client applications.</summary>
        protected class GridCheckBoxCellAccessibleObject : GridCell.GridCellAccessibleObject
        {
            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCheckBoxCell.GridCheckBoxCellAccessibleObject"></see> class. </summary>
            /// <param name="owner">The <see cref="T:MControl.GridView.GridCell"></see> that owns the <see cref="T:MControl.GridView.GridCheckBoxCell.GridCheckBoxCellAccessibleObject"></see>.</param>
            public GridCheckBoxCellAccessibleObject(GridCell owner) : base(owner)
            {
            }

            /// <summary>Performs the default action of the <see cref="T:MControl.GridView.GridCheckBoxCell.GridCheckBoxCellAccessibleObject"></see>.</summary>
            /// <exception cref="T:System.InvalidOperationException">The <see cref="T:MControl.GridView.GridCheckBoxCell"></see> returned by the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property does not belong to a Grid control.-or-The <see cref="T:MControl.GridView.GridCheckBoxCell"></see> returned by the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property belongs to a shared row.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void DoDefaultAction()
            {
                GridCheckBoxCell owner = (GridCheckBoxCell) base.Owner;
                Grid grid = owner.Grid;
                if ((grid != null) && (owner.RowIndex == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedCell"));
                }
                if ((!owner.ReadOnly && (owner.OwningColumn != null)) && (owner.OwningRow != null))
                {
                    grid.CurrentCell = owner;
                    bool flag = false;
                    if (!grid.IsCurrentCellInEditMode)
                    {
                        flag = true;
                        grid.BeginEdit(false);
                    }
                    if (grid.IsCurrentCellInEditMode)
                    {
                        if (owner.SwitchFormattedValue())
                        {
                            owner.NotifyGridOfValueChange();
                            grid.InvalidateCell(owner.ColumnIndex, owner.RowIndex);
                            int num = grid.Rows.GetRowCount(GridElementStates.Visible, 0, owner.RowIndex);
                            int num2 = grid.Columns.ColumnIndexToActualDisplayIndex(owner.ColumnIndex, GridElementStates.Visible);
                            bool columnHeadersVisible = grid.ColumnHeadersVisible;
                            bool rowHeadersVisible = grid.RowHeadersVisible;
                            int objectID = (num + (columnHeadersVisible ? 1 : 0)) + 1;
                            int childID = num2 + (rowHeadersVisible ? 1 : 0);
                            ((Control.ControlAccessibleObject) grid.AccessibilityObject).NotifyClients(AccessibleEvents.DefaultActionChange, objectID, childID);
                        }
                        if (flag)
                        {
                            grid.EndEdit();
                        }
                    }
                }
            }

            /// <summary>Gets the number of child accessible objects that belong to the <see cref="T:MControl.GridView.GridCheckBoxCell.GridCheckBoxCellAccessibleObject"></see>.</summary>
            /// <returns>The value –1.</returns>
            public override int GetChildCount()
            {
                return 0;
            }

            /// <summary>Gets a string that represents the default action of the <see cref="T:MControl.GridView.GridCheckBoxCell.GridCheckBoxCellAccessibleObject"></see>.</summary>
            /// <returns>A description of the default action.</returns>
            public override string DefaultAction
            {
                get
                {
                    if (base.Owner.ReadOnly)
                    {
                        return string.Empty;
                    }
                    bool flag = true;
                    object formattedValue = base.Owner.FormattedValue;
                    if (formattedValue is CheckState)
                    {
                        flag = ((CheckState) formattedValue) == CheckState.Unchecked;
                    }
                    else if (formattedValue is bool)
                    {
                        flag = !((bool) formattedValue);
                    }
                    if (flag)
                    {
                        return MControl.GridView.RM.GetString("Grid_AccCheckBoxCellDefaultActionCheck");
                    }
                    return MControl.GridView.RM.GetString("Grid_AccCheckBoxCellDefaultActionUncheck");
                }
            }
        }

        private class GridCheckBoxCellRenderer
        {
            private static VisualStyleRenderer visualStyleRenderer;

            private GridCheckBoxCellRenderer()
            {
            }

            public static void DrawCheckBox(Graphics g, Rectangle bounds, int state)
            {
                CheckBoxRenderer.SetParameters(GridCheckBoxCell.CheckBoxElement.ClassName, GridCheckBoxCell.CheckBoxElement.Part, state);
                CheckBoxRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
            }

            public static VisualStyleRenderer CheckBoxRenderer
            {
                get
                {
                    if (visualStyleRenderer == null)
                    {
                        visualStyleRenderer = new VisualStyleRenderer(GridCheckBoxCell.CheckBoxElement);
                    }
                    return visualStyleRenderer;
                }
            }
        }
    }
}

