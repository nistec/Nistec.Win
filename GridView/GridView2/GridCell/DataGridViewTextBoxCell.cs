namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    /// <summary>Displays editable text information in a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridTextBoxCell : GridCell
    {
        private static System.Type cellType = typeof(GridTextBoxCell);
        private const byte GRIDTEXTBOXCELL_horizontalTextMarginLeft = 0;
        private const byte GRIDTEXTBOXCELL_horizontalTextMarginRight = 0;
        private const byte GRIDTEXTBOXCELL_horizontalTextOffsetLeft = 3;
        private const byte GRIDTEXTBOXCELL_horizontalTextOffsetRight = 4;
        private const byte GRIDTEXTBOXCELL_ignoreNextMouseClick = 1;
        private const int GRIDTEXTBOXCELL_maxInputLength = 0x7fff;
        private const byte GRIDTEXTBOXCELL_verticalTextMarginBottom = 1;
        private const byte GRIDTEXTBOXCELL_verticalTextMarginTopWithoutWrapping = 2;
        private const byte GRIDTEXTBOXCELL_verticalTextMarginTopWithWrapping = 1;
        private const byte GRIDTEXTBOXCELL_verticalTextOffsetBottom = 1;
        private const byte GRIDTEXTBOXCELL_verticalTextOffsetTop = 2;
        private static System.Type defaultFormattedValueType = typeof(string);
        private static System.Type defaultValueType = typeof(object);
        private byte flagsState;
        private static readonly int PropTextBoxCellEditingTextBox = PropertyStore.CreateKey();
        private static readonly int PropTextBoxCellMaxInputLength = PropertyStore.CreateKey();

        internal override void CacheEditingControl()
        {
            this.EditingTextBox = base.Grid.EditingControl as GridTextBoxEditingControl;
        }

        /// <summary>Creates an exact copy of this cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridTextBoxCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridTextBoxCell cell;
            System.Type type = base.GetType();
            if (type == cellType)
            {
                cell = new GridTextBoxCell();
            }
            else
            {
                cell = (GridTextBoxCell) Activator.CreateInstance(type);
            }
            base.CloneInternal(cell);
            cell.MaxInputLength = this.MaxInputLength;
            return cell;
        }

        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            Grid grid = base.Grid;
            if ((grid == null) || (grid.EditingControl == null))
            {
                throw new InvalidOperationException();
            }
            TextBox editingControl = grid.EditingControl as TextBox;
            if (editingControl != null)
            {
                editingControl.ClearUndo();
            }
            this.EditingTextBox = null;
            base.DetachEditingControl();
        }

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds, GridCellStyle cellStyle)
        {
            int height;
            TextBox editingControl = base.Grid.EditingControl as TextBox;
            int width = editingControlBounds.Width;
            if (editingControl != null)
            {
                switch (cellStyle.Alignment)
                {
                    case GridContentAlignment.TopLeft:
                    case GridContentAlignment.MiddleLeft:
                    case GridContentAlignment.BottomLeft:
                        if (base.Grid.RightToLeftInternal)
                        {
                            editingControlBounds.X++;
                            editingControlBounds.Width = Math.Max(0, (editingControlBounds.Width - 3) - 2);
                        }
                        else
                        {
                            editingControlBounds.X += 3;
                            editingControlBounds.Width = Math.Max(0, (editingControlBounds.Width - 3) - 1);
                        }
                        break;

                    case GridContentAlignment.TopCenter:
                    case GridContentAlignment.MiddleCenter:
                    case GridContentAlignment.BottomCenter:
                        editingControlBounds.X++;
                        editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 3);
                        break;

                    case GridContentAlignment.TopRight:
                    case GridContentAlignment.BottomRight:
                    case GridContentAlignment.MiddleRight:
                        if (base.Grid.RightToLeftInternal)
                        {
                            editingControlBounds.X += 3;
                            editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 4);
                        }
                        else
                        {
                            editingControlBounds.X++;
                            editingControlBounds.Width = Math.Max(0, (editingControlBounds.Width - 4) - 1);
                        }
                        break;
                }
                switch (cellStyle.Alignment)
                {
                    case GridContentAlignment.TopLeft:
                    case GridContentAlignment.TopCenter:
                    case GridContentAlignment.TopRight:
                        editingControlBounds.Y += 2;
                        editingControlBounds.Height = Math.Max(0, editingControlBounds.Height - 2);
                        break;

                    case GridContentAlignment.MiddleLeft:
                    case GridContentAlignment.MiddleCenter:
                    case GridContentAlignment.MiddleRight:
                        editingControlBounds.Height++;
                        break;

                    case GridContentAlignment.BottomCenter:
                    case GridContentAlignment.BottomRight:
                    case GridContentAlignment.BottomLeft:
                        editingControlBounds.Height = Math.Max(0, editingControlBounds.Height - 1);
                        break;
                }
                if (cellStyle.WrapMode == GridTriState.False)
                {
                    height = editingControl.PreferredSize.Height;
                }
                else
                {
                    string editingControlFormattedValue = (string) ((IGridEditingControl) editingControl).GetEditingControlFormattedValue(GridDataErrorContexts.Formatting);
                    if (string.IsNullOrEmpty(editingControlFormattedValue))
                    {
                        editingControlFormattedValue = " ";
                    }
                    TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
                    height = GridCell.MeasureTextHeight(base.Grid.CachedGraphics, editingControlFormattedValue, cellStyle.Font, width, flags);
                }
                if (height < editingControlBounds.Height)
                {
                    GridContentAlignment alignment = cellStyle.Alignment;
                    if (alignment <= GridContentAlignment.MiddleCenter)
                    {
                        switch (alignment)
                        {
                            case GridContentAlignment.TopLeft:
                            case GridContentAlignment.TopCenter:
                            case (GridContentAlignment.TopCenter | GridContentAlignment.TopLeft):
                            case GridContentAlignment.TopRight:
                                return editingControlBounds;

                            case GridContentAlignment.MiddleLeft:
                            case GridContentAlignment.MiddleCenter:
                                goto Label_0310;
                        }
                        return editingControlBounds;
                    }
                    if (alignment <= GridContentAlignment.BottomLeft)
                    {
                        switch (alignment)
                        {
                            case GridContentAlignment.MiddleRight:
                                goto Label_0310;

                            case GridContentAlignment.BottomLeft:
                                goto Label_032B;
                        }
                        return editingControlBounds;
                    }
                    switch (alignment)
                    {
                        case GridContentAlignment.BottomCenter:
                        case GridContentAlignment.BottomRight:
                            goto Label_032B;
                    }
                }
            }
            return editingControlBounds;
        Label_0310:
            editingControlBounds.Y += (editingControlBounds.Height - height) / 2;
            return editingControlBounds;
        Label_032B:
            editingControlBounds.Y += editingControlBounds.Height - height;
            return editingControlBounds;
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
            object obj2 = this.GetValue(rowIndex);
            object formattedValue = this.GetFormattedValue(obj2, rowIndex, ref cellStyle, null, null, GridDataErrorContexts.Formatting);
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, formattedValue, null, cellStyle, style, GridPaintParts.ContentForeground, true, false, false);
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
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, null, this.GetErrorText(rowIndex), cellStyle, style, GridPaintParts.ContentForeground, false, true, false);
        }

        protected override Size GetPreferredSize(Graphics graphics, GridCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            Size size;
            if (base.Grid == null)
            {
                return new Size(-1, -1);
            }
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            Rectangle stdBorderWidths = base.StdBorderWidths;
            int num = (stdBorderWidths.Left + stdBorderWidths.Width) + cellStyle.Padding.Horizontal;
            int num2 = (stdBorderWidths.Top + stdBorderWidths.Height) + cellStyle.Padding.Vertical;
            GridFreeDimension freeDimensionFromConstraint = GridCell.GetFreeDimensionFromConstraint(constraintSize);
            string str = base.GetFormattedValue(rowIndex, ref cellStyle, GridDataErrorContexts.PreferredSize | GridDataErrorContexts.Formatting) as string;
            if (string.IsNullOrEmpty(str))
            {
                str = " ";
            }
            TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
            if ((cellStyle.WrapMode == GridTriState.True) && (str.Length > 1))
            {
                switch (freeDimensionFromConstraint)
                {
                    case GridFreeDimension.Height:
                        size = new Size(0, GridCell.MeasureTextHeight(graphics, str, cellStyle.Font, Math.Max(1, constraintSize.Width - num), flags));
                        goto Label_01C3;

                    case GridFreeDimension.Width:
                        size = new Size(GridCell.MeasureTextWidth(graphics, str, cellStyle.Font, Math.Max(1, ((constraintSize.Height - num2) - 1) - 1), flags), 0);
                        goto Label_01C3;
                }
                size = GridCell.MeasureTextPreferredSize(graphics, str, cellStyle.Font, 5f, flags);
            }
            else
            {
                switch (freeDimensionFromConstraint)
                {
                    case GridFreeDimension.Height:
                        size = new Size(0, GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags).Height);
                        goto Label_01C3;

                    case GridFreeDimension.Width:
                        size = new Size(GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags).Width, 0);
                        goto Label_01C3;
                }
                size = GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags);
            }
        Label_01C3:
            if (freeDimensionFromConstraint != GridFreeDimension.Height)
            {
                size.Width += num;
                if (base.Grid.ShowCellErrors)
                {
                    size.Width = Math.Max(size.Width, (num + 8) + 12);
                }
            }
            if (freeDimensionFromConstraint != GridFreeDimension.Width)
            {
                int num3 = (cellStyle.WrapMode == GridTriState.True) ? 1 : 2;
                size.Height += (num3 + 1) + num2;
                if (base.Grid.ShowCellErrors)
                {
                    size.Height = Math.Max(size.Height, (num2 + 8) + 11);
                }
            }
            return size;
        }

        /// <summary>Attaches and initializes the hosted editing control.</summary>
        /// <param name="initialFormattedValue">The initial value to be displayed in the control.</param>
        /// <param name="rowIndex">The index of the row being edited.</param>
        /// <param name="gridCellStyle">A cell style that is used to determine the appearance of the hosted control.</param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, GridCellStyle gridCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, gridCellStyle);
            TextBox editingControl = base.Grid.EditingControl as TextBox;
            if (editingControl != null)
            {
                editingControl.BorderStyle = BorderStyle.None;
                editingControl.AcceptsReturn = editingControl.Multiline = gridCellStyle.WrapMode == GridTriState.True;
                editingControl.MaxLength = this.MaxInputLength;
                string str = initialFormattedValue as string;
                if (str == null)
                {
                    editingControl.Text = string.Empty;
                }
                else
                {
                    editingControl.Text = str;
                }
                this.EditingTextBox = base.Grid.EditingControl as GridTextBoxEditingControl;
            }
        }

        /// <summary>Determines if edit mode should be started based on the given key.</summary>
        /// <returns>true if edit mode should be started; otherwise, false. </returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that represents the key that was pressed.</param>
        /// <filterpriority>1</filterpriority>
        public override bool KeyEntersEditMode(KeyEventArgs e)
        {
            return (((((char.IsLetterOrDigit((char) ((ushort) e.KeyCode)) && ((e.KeyCode < Keys.F1) || (e.KeyCode > Keys.F24))) || ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.Divide))) || (((e.KeyCode >= Keys.OemSemicolon) && (e.KeyCode <= Keys.OemBackslash)) || ((e.KeyCode == Keys.Space) && !e.Shift))) && (!e.Alt && !e.Control)) || base.KeyEntersEditMode(e));
        }

        /// <summary>Called by <see cref="T:MControl.GridView.Grid"></see> when the selection cursor moves onto a cell.</summary>
        /// <param name="throughMouseClick">true if the cell was entered as a result of a mouse click; otherwise, false.</param>
        /// <param name="rowIndex">The index of the row entered by the mouse.</param>
        protected override void OnEnter(int rowIndex, bool throughMouseClick)
        {
            if ((base.Grid != null) && throughMouseClick)
            {
                this.flagsState = (byte) (this.flagsState | 1);
            }
        }

        /// <summary>Called by the <see cref="T:MControl.GridView.Grid"></see> when the mouse leaves a cell.</summary>
        /// <param name="throughMouseClick">true if the cell was left as a result of a mouse click; otherwise, false.</param>
        /// <param name="rowIndex">The index of the row the mouse has left.</param>
        protected override void OnLeave(int rowIndex, bool throughMouseClick)
        {
            if (base.Grid != null)
            {
                this.flagsState = (byte) (this.flagsState & -2);
            }
        }

        /// <summary>Called by <see cref="T:MControl.GridView.Grid"></see> when the mouse leaves a cell.</summary>
        /// <param name="e">An <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseClick(GridCellMouseEventArgs e)
        {
            if (base.Grid != null)
            {
                Point currentCellAddress = base.Grid.CurrentCellAddress;
                if (((currentCellAddress.X == e.ColumnIndex) && (currentCellAddress.Y == e.RowIndex)) && (e.Button == MouseButtons.Left))
                {
                    if ((this.flagsState & 1) != 0)
                    {
                        this.flagsState = (byte) (this.flagsState & -2);
                    }
                    else if (base.Grid.EditMode != GridEditMode.EditProgrammatically)
                    {
                        base.Grid.BeginEdit(true);
                    }
                }
            }
        }

        private bool OwnsEditingTextBox(int rowIndex)
        {
            return (((rowIndex != -1) && (this.EditingTextBox != null)) && (rowIndex == this.EditingTextBox.EditingControlRowIndex));
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
        }

        private Rectangle PaintPrivate(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
        {
            SolidBrush cachedBrush;
            Rectangle empty = Rectangle.Empty;
            if (paint && GridCell.PaintBorder(paintParts))
            {
                this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
            Rectangle rect = cellBounds;
            rect.Offset(rectangle2.X, rectangle2.Y);
            rect.Width -= rectangle2.Right;
            rect.Height -= rectangle2.Bottom;
            Point currentCellAddress = base.Grid.CurrentCellAddress;
            bool flag = (currentCellAddress.X == base.ColumnIndex) && (currentCellAddress.Y == rowIndex);
            bool flag2 = flag && (base.Grid.EditingControl != null);
            bool flag3 = (cellState & GridElementStates.Selected) != GridElementStates.None;
            if ((GridCell.PaintSelectionBackground(paintParts) && flag3) && !flag2)
            {
                cachedBrush = base.Grid.GetCachedBrush(cellStyle.SelectionBackColor);
            }
            else
            {
                cachedBrush = base.Grid.GetCachedBrush(cellStyle.BackColor);
            }
            if (((paint && GridCell.PaintBackground(paintParts)) && ((cachedBrush.Color.A == 0xff) && (rect.Width > 0))) && (rect.Height > 0))
            {
                graphics.FillRectangle(cachedBrush, rect);
            }
            if (cellStyle.Padding != Padding.Empty)
            {
                if (base.Grid.RightToLeftInternal)
                {
                    rect.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
                }
                else
                {
                    rect.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
                }
                rect.Width -= cellStyle.Padding.Horizontal;
                rect.Height -= cellStyle.Padding.Vertical;
            }
            if (((paint && flag) && (!flag2 && GridCell.PaintFocus(paintParts))) && ((base.Grid.ShowFocusCues && base.Grid.Focused) && ((rect.Width > 0) && (rect.Height > 0))))
            {
                ControlPaint.DrawFocusRectangle(graphics, rect, Color.Empty, cachedBrush.Color);
            }
            Rectangle cellValueBounds = rect;
            string text = formattedValue as string;
            if ((text != null) && ((paint && !flag2) || computeContentBounds))
            {
                int y = (cellStyle.WrapMode == GridTriState.True) ? 1 : 2;
                rect.Offset(0, y);
                rect.Width = rect.Width;
                rect.Height -= y + 1;
                if ((rect.Width > 0) && (rect.Height > 0))
                {
                    TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
                    if (paint)
                    {
                        if (GridCell.PaintContentForeground(paintParts))
                        {
                            if ((flags & TextFormatFlags.SingleLine) != TextFormatFlags.GlyphOverhangPadding)
                            {
                                flags |= TextFormatFlags.EndEllipsis;
                            }
                            TextRenderer.DrawText(graphics, text, cellStyle.Font, rect, flag3 ? cellStyle.SelectionForeColor : cellStyle.ForeColor, flags);
                        }
                    }
                    else
                    {
                        empty = GridUtilities.GetTextBounds(rect, text, flags, cellStyle);
                    }
                }
            }
            else if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
            {
                empty = base.ComputeErrorIconBounds(cellValueBounds);
            }
            if ((base.Grid.ShowCellErrors && paint) && GridCell.PaintErrorIcon(paintParts))
            {
                base.PaintErrorIcon(graphics, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
            }
            return empty;
        }

        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, GridCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = this.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
            editingControlBounds = this.GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            base.Grid.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            base.Grid.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridTextBoxCell { ColumnIndex=" + base.ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + base.RowIndex.ToString(CultureInfo.CurrentCulture) + " }");
        }

        private GridTextBoxEditingControl EditingTextBox
        {
            get
            {
                return (GridTextBoxEditingControl) base.Properties.GetObject(PropTextBoxCellEditingTextBox);
            }
            set
            {
                if ((value != null) || base.Properties.ContainsObject(PropTextBoxCellEditingTextBox))
                {
                    base.Properties.SetObject(PropTextBoxCellEditingTextBox, value);
                }
            }
        }

        /// <summary>Gets the type of the formatted value associated with the cell.</summary>
        /// <returns>A <see cref="T:System.Type"></see> representing the <see cref="T:System.String"></see> type in all cases.</returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type FormattedValueType
        {
            get
            {
                return defaultFormattedValueType;
            }
        }

        /// <summary>Gets or sets the maximum number of characters that can be entered into the text box.</summary>
        /// <returns>The maximum number of characters that can be entered into the text box; the default value is 32767.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0.</exception>
        [DefaultValue(0x7fff)]
        public virtual int MaxInputLength
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropTextBoxCellMaxInputLength, out flag);
                if (flag)
                {
                    return integer;
                }
                return 0x7fff;
            }
            set
            {
                if (value < 0)
                {
                    object[] args = new object[] { "MaxInputLength", value.ToString(CultureInfo.CurrentCulture), 0.ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentOutOfRangeException("MaxInputLength", MControl.GridView.RM.GetString("InvalidLowBoundArgumentEx", args));
                }
                base.Properties.SetInteger(PropTextBoxCellMaxInputLength, value);
                if (this.OwnsEditingTextBox(base.RowIndex))
                {
                    this.EditingTextBox.MaxLength = value;
                }
            }
        }

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
                return defaultValueType;
            }
        }
    }
}

