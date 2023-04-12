namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Security.Permissions;
    using System.Windows.Forms.ButtonInternal;
    using System.Windows.Forms.Internal;
    using System.Windows.Forms.VisualStyles;
    using System.Windows.Forms;

    /// <summary>Displays a button-like user interface (UI) for use in a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridButtonCell : GridCell
    {
        private static readonly VisualStyleElement ButtonElement = VisualStyleElement.Button.PushButton.Normal;
        private static System.Type cellType = typeof(GridButtonCell);
        private const byte GRIDBUTTONCELL_horizontalTextMargin = 2;
        private const byte GRIDBUTTONCELL_textPadding = 5;
        private const byte GRIDBUTTONCELL_themeMargin = 100;
        private const byte GRIDBUTTONCELL_verticalTextMargin = 1;
        private static System.Type defaultFormattedValueType = typeof(string);
        private static System.Type defaultValueType = typeof(object);
        private static bool mouseInContentBounds = false;
        private static readonly int PropButtonCellFlatStyle = PropertyStore.CreateKey();
        private static readonly int PropButtonCellState = PropertyStore.CreateKey();
        private static readonly int PropButtonCellUseColumnTextForButtonValue = PropertyStore.CreateKey();
        private static Rectangle rectThemeMargins = new Rectangle(-1, -1, 0, 0);

        /// <summary>Creates an exact copy of this cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridButtonCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridButtonCell cell;
            System.Type type = base.GetType();
            if (type == cellType)
            {
                cell = new GridButtonCell();
            }
            else
            {
                cell = (GridButtonCell) Activator.CreateInstance(type);
            }
            base.CloneInternal(cell);
            cell.FlatStyleInternal = this.FlatStyle;
            cell.UseColumnTextForButtonValueInternal = this.UseColumnTextForButtonValue;
            return cell;
        }

        /// <summary>Creates a new accessible object for the <see cref="T:MControl.GridView.GridButtonCell"></see>. </summary>
        /// <returns>A new <see cref="T:MControl.GridView.GridButtonCell.GridButtonCellAccessibleObject"></see> for the <see cref="T:MControl.GridView.GridButtonCell"></see>. </returns>
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new GridButtonCellAccessibleObject(this);
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
            int num3;
            int num4;
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
            if (base.Grid.ApplyVisualStylesToInnerCells)
            {
                Rectangle themeMargins = GetThemeMargins(graphics);
                num3 = themeMargins.X + themeMargins.Width;
                num4 = themeMargins.Y + themeMargins.Height;
            }
            else
            {
                num3 = num4 = 5;
            }
            switch (freeDimensionFromConstraint)
            {
                case GridFreeDimension.Height:
                    if (((cellStyle.WrapMode != GridTriState.True) || (str.Length <= 1)) || ((((constraintSize.Width - num) - num3) - 4) <= 0))
                    {
                        size = new Size(0, GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags).Height);
                        break;
                    }
                    size = new Size(0, GridCell.MeasureTextHeight(graphics, str, cellStyle.Font, ((constraintSize.Width - num) - num3) - 4, flags));
                    break;

                case GridFreeDimension.Width:
                    if (((cellStyle.WrapMode != GridTriState.True) || (str.Length <= 1)) || ((((constraintSize.Height - num2) - num4) - 2) <= 0))
                    {
                        size = new Size(GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags).Width, 0);
                        break;
                    }
                    size = new Size(GridCell.MeasureTextWidth(graphics, str, cellStyle.Font, ((constraintSize.Height - num2) - num4) - 2, flags), 0);
                    break;

                default:
                    if ((cellStyle.WrapMode == GridTriState.True) && (str.Length > 1))
                    {
                        size = GridCell.MeasureTextPreferredSize(graphics, str, cellStyle.Font, 5f, flags);
                    }
                    else
                    {
                        size = GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags);
                    }
                    break;
            }
            if (freeDimensionFromConstraint != GridFreeDimension.Height)
            {
                size.Width += (num + num3) + 4;
                if (base.Grid.ShowCellErrors)
                {
                    size.Width = Math.Max(size.Width, (num + 8) + 12);
                }
            }
            if (freeDimensionFromConstraint != GridFreeDimension.Width)
            {
                size.Height += (num2 + num4) + 2;
                if (base.Grid.ShowCellErrors)
                {
                    size.Height = Math.Max(size.Height, (num2 + 8) + 11);
                }
            }
            return size;
        }

        private static Rectangle GetThemeMargins(Graphics g)
        {
            if (rectThemeMargins.X == -1)
            {
                Rectangle bounds = new Rectangle(0, 0, 100, 100);
                Rectangle backgroundContentRectangle = GridButtonCellRenderer.GridButtonRenderer.GetBackgroundContentRectangle(g, bounds);
                rectThemeMargins.X = backgroundContentRectangle.X;
                rectThemeMargins.Y = backgroundContentRectangle.Y;
                rectThemeMargins.Width = 100 - backgroundContentRectangle.Right;
                rectThemeMargins.Height = 100 - backgroundContentRectangle.Bottom;
            }
            return rectThemeMargins;
        }

        /// <summary>Retrieves the text associated with the button.</summary>
        /// <returns>The value of the <see cref="T:MControl.GridView.GridButtonCell"></see> or the <see cref="P:MControl.GridView.GridButtonColumn.Text"></see> value of the owning column if <see cref="P:MControl.GridView.GridButtonCell.UseColumnTextForButtonValue"></see> is true. </returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        protected override object GetValue(int rowIndex)
        {
            if (((this.UseColumnTextForButtonValue && (base.Grid != null)) && ((base.Grid.NewRowIndex != rowIndex) && (base.OwningColumn != null))) && (base.OwningColumn is GridButtonColumn))
            {
                return ((GridButtonColumn) base.OwningColumn).Text;
            }
            return base.GetValue(rowIndex);
        }

        /// <summary>Indicates whether a row is unshared if a key is pressed while the focus is on a cell in the row.</summary>
        /// <returns>true if the user pressed the SPACE key without modifier keys; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return ((((e.KeyCode == Keys.Space) && !e.Alt) && !e.Control) && !e.Shift);
        }

        /// <summary>Indicates whether a row is unshared when a key is released while the focus is on a cell in the row.</summary>
        /// <returns>true if the user released the SPACE key; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return (e.KeyCode == Keys.Space);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse button is held down while the pointer is on a cell in the row.</summary>
        /// <returns>true if the user pressed the left mouse button; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data.</param>
        protected override bool MouseDownUnsharesRow(GridCellMouseEventArgs e)
        {
            return (e.Button == MouseButtons.Left);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
        /// <returns>true if the cell was the last cell receiving a mouse click; otherwise, false.</returns>
        /// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
        protected override bool MouseEnterUnsharesRow(int rowIndex)
        {
            return ((base.ColumnIndex == base.Grid.MouseDownCellAddress.X) && (rowIndex == base.Grid.MouseDownCellAddress.Y));
        }

        /// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
        /// <returns>true if the button displayed by the cell is in the pressed state; otherwise, false.</returns>
        /// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
        protected override bool MouseLeaveUnsharesRow(int rowIndex)
        {
            return ((this.ButtonState & System.Windows.Forms.ButtonState.Pushed) != System.Windows.Forms.ButtonState.Normal);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse button is released while the pointer is on a cell in the row. </summary>
        /// <returns>true if the mouse up was caused by the release of the left mouse button; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data.</param>
        protected override bool MouseUpUnsharesRow(GridCellMouseEventArgs e)
        {
            return (e.Button == MouseButtons.Left);
        }

        /// <summary>Called when a character key is pressed while the focus is on the cell.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data.</param>
        /// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            if ((base.Grid != null) && (((e.KeyCode == Keys.Space) && !e.Alt) && (!e.Control && !e.Shift)))
            {
                this.UpdateButtonState(this.ButtonState | System.Windows.Forms.ButtonState.Checked, rowIndex);
                e.Handled = true;
            }
        }

        /// <summary>Called when a character key is released while the focus is on the cell.</summary>
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

        /// <summary>Called when the focus moves from the cell.</summary>
        /// <param name="throughMouseClick">true if focus left the cell as a result of user mouse click; false if focus left due to a programmatic cell change.</param>
        /// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
        protected override void OnLeave(int rowIndex, bool throughMouseClick)
        {
            if ((base.Grid != null) && (this.ButtonState != System.Windows.Forms.ButtonState.Normal))
            {
                this.UpdateButtonState(System.Windows.Forms.ButtonState.Normal, rowIndex);
            }
        }

        /// <summary>Called when the mouse button is held down while the pointer is on the cell.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseDown(GridCellMouseEventArgs e)
        {
            if ((base.Grid != null) && ((e.Button == MouseButtons.Left) && mouseInContentBounds))
            {
                this.UpdateButtonState(this.ButtonState | System.Windows.Forms.ButtonState.Pushed, e.RowIndex);
            }
        }

        /// <summary>Called when the mouse pointer moves out of the cell.</summary>
        /// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
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

        /// <summary>Called when the mouse pointer moves while it is over the cell. </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseMove(GridCellMouseEventArgs e)
        {
            if (base.Grid != null)
            {
                bool mouseInContentBounds = GridButtonCell.mouseInContentBounds;
                GridButtonCell.mouseInContentBounds = base.GetContentBounds(e.RowIndex).Contains(e.X, e.Y);
                if (mouseInContentBounds != GridButtonCell.mouseInContentBounds)
                {
                    if ((base.Grid.ApplyVisualStylesToInnerCells || (this.FlatStyle == System.Windows.Forms.FlatStyle.Flat)) || (this.FlatStyle == System.Windows.Forms.FlatStyle.Popup))
                    {
                        base.Grid.InvalidateCell(base.ColumnIndex, e.RowIndex);
                    }
                    if (((e.ColumnIndex == base.Grid.MouseDownCellAddress.X) && (e.RowIndex == base.Grid.MouseDownCellAddress.Y)) && (Control.MouseButtons == MouseButtons.Left))
                    {
                        if ((((this.ButtonState & System.Windows.Forms.ButtonState.Pushed) == System.Windows.Forms.ButtonState.Normal) && GridButtonCell.mouseInContentBounds) && base.Grid.CellMouseDownInContentBounds)
                        {
                            this.UpdateButtonState(this.ButtonState | System.Windows.Forms.ButtonState.Pushed, e.RowIndex);
                        }
                        else if (((this.ButtonState & System.Windows.Forms.ButtonState.Pushed) != System.Windows.Forms.ButtonState.Normal) && !GridButtonCell.mouseInContentBounds)
                        {
                            this.UpdateButtonState(this.ButtonState & ~System.Windows.Forms.ButtonState.Pushed, e.RowIndex);
                        }
                    }
                }
                base.OnMouseMove(e);
            }
        }

        /// <summary>Called when the mouse button is released while the pointer is on the cell. </summary>
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
            Rectangle empty;
            Point currentCellAddress = base.Grid.CurrentCellAddress;
            bool flag = (elementState & GridElementStates.Selected) != GridElementStates.None;
            bool flag2 = (currentCellAddress.X == base.ColumnIndex) && (currentCellAddress.Y == rowIndex);
            string text = formattedValue as string;
            SolidBrush cachedBrush = base.Grid.GetCachedBrush((GridCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
            SolidBrush brush2 = base.Grid.GetCachedBrush(flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
            if (paint && GridCell.PaintBorder(paintParts))
            {
                this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            Rectangle rect = cellBounds;
            Rectangle rectangle3 = this.BorderWidths(advancedBorderStyle);
            rect.Offset(rectangle3.X, rectangle3.Y);
            rect.Width -= rectangle3.Right;
            rect.Height -= rectangle3.Bottom;
            if ((rect.Height <= 0) || (rect.Width <= 0))
            {
                return Rectangle.Empty;
            }
            if ((paint && GridCell.PaintBackground(paintParts)) && (cachedBrush.Color.A == 0xff))
            {
                g.FillRectangle(cachedBrush, rect);
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
            Rectangle cellValueBounds = rect;
            if (((rect.Height <= 0) || (rect.Width <= 0)) || (!paint && !computeContentBounds))
            {
                if (computeErrorIconBounds)
                {
                    if (!string.IsNullOrEmpty(errorText))
                    {
                        empty = base.ComputeErrorIconBounds(cellValueBounds);
                    }
                    else
                    {
                        empty = Rectangle.Empty;
                    }
                }
                else
                {
                    empty = Rectangle.Empty;
                }
                goto Label_06AD;
            }
            if ((this.FlatStyle == System.Windows.Forms.FlatStyle.Standard) || (this.FlatStyle == System.Windows.Forms.FlatStyle.System))
            {
                if (base.Grid.ApplyVisualStylesToInnerCells)
                {
                    if (paint && GridCell.PaintContentBackground(paintParts))
                    {
                        PushButtonState normal = PushButtonState.Normal;
                        if ((this.ButtonState & (System.Windows.Forms.ButtonState.Checked | System.Windows.Forms.ButtonState.Pushed)) != System.Windows.Forms.ButtonState.Normal)
                        {
                            normal = PushButtonState.Pressed;
                        }
                        else if (((base.Grid.MouseEnteredCellAddress.Y == rowIndex) && (base.Grid.MouseEnteredCellAddress.X == base.ColumnIndex)) && mouseInContentBounds)
                        {
                            normal = PushButtonState.Hot;
                        }
                        if ((GridCell.PaintFocus(paintParts) && flag2) && (base.Grid.ShowFocusCues && base.Grid.Focused))
                        {
                            normal |= PushButtonState.Default;
                        }
                        GridButtonCellRenderer.DrawButton(g, rect, (int) normal);
                    }
                    empty = rect;
                    rect = GridButtonCellRenderer.GridButtonRenderer.GetBackgroundContentRectangle(g, rect);
                }
                else
                {
                    if (paint && GridCell.PaintContentBackground(paintParts))
                    {
                        ControlPaint.DrawBorder(g, rect, SystemColors.Control, (this.ButtonState == System.Windows.Forms.ButtonState.Normal) ? ButtonBorderStyle.Outset : ButtonBorderStyle.Inset);
                    }
                    empty = rect;
                    rect.Inflate(-SystemInformation.Border3DSize.Width, -SystemInformation.Border3DSize.Height);
                }
                goto Label_06AD;
            }
            if (this.FlatStyle != System.Windows.Forms.FlatStyle.Flat)
            {
                rect.Inflate(-1, -1);
                if (paint && GridCell.PaintContentBackground(paintParts))
                {
                    if ((this.ButtonState & (System.Windows.Forms.ButtonState.Checked | System.Windows.Forms.ButtonState.Pushed)) != System.Windows.Forms.ButtonState.Normal)
                    {
                        ButtonBaseAdapter.ColorData data2 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.Grid.Enabled).Calculate();
                        ButtonBaseAdapter.DrawDefaultBorder(g, rect, data2.options.highContrast ? data2.windowText : data2.windowFrame, true);
                        ControlPaint.DrawBorder(g, rect, data2.options.highContrast ? data2.windowText : data2.buttonShadow, ButtonBorderStyle.Solid);
                    }
                    else if (((base.Grid.MouseEnteredCellAddress.Y == rowIndex) && (base.Grid.MouseEnteredCellAddress.X == base.ColumnIndex)) && mouseInContentBounds)
                    {
                        ButtonBaseAdapter.ColorData colors = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.Grid.Enabled).Calculate();
                        ButtonBaseAdapter.DrawDefaultBorder(g, rect, colors.options.highContrast ? colors.windowText : colors.buttonShadow, false);
                        ButtonBaseAdapter.Draw3DLiteBorder(g, rect, colors, true);
                    }
                    else
                    {
                        ButtonBaseAdapter.ColorData data4 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.Grid.Enabled).Calculate();
                        ButtonBaseAdapter.DrawDefaultBorder(g, rect, data4.options.highContrast ? data4.windowText : data4.buttonShadow, false);
                        ButtonBaseAdapter.DrawFlatBorder(g, rect, data4.options.highContrast ? data4.windowText : data4.buttonShadow);
                    }
                }
                empty = rect;
                goto Label_06AD;
            }
            rect.Inflate(-1, -1);
            if (paint && GridCell.PaintContentBackground(paintParts))
            {
                ButtonBaseAdapter.DrawDefaultBorder(g, rect, brush2.Color, true);
                if (cachedBrush.Color.A == 0xff)
                {
                    if ((this.ButtonState & (System.Windows.Forms.ButtonState.Checked | System.Windows.Forms.ButtonState.Pushed)) != System.Windows.Forms.ButtonState.Normal)
                    {
                        ButtonBaseAdapter.ColorData data = ButtonBaseAdapter.PaintFlatRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.Grid.Enabled).Calculate();
                        IntPtr hdc = g.GetHdc();
                        try
                        {
                            using (WindowsGraphics graphics = WindowsGraphics.FromHdc(hdc))
                            {
                                WindowsBrush brush3;
                                if (data.options.highContrast)
                                {
                                    brush3 = new WindowsSolidBrush(graphics.DeviceContext, data.buttonShadow);
                                }
                                else
                                {
                                    brush3 = new WindowsSolidBrush(graphics.DeviceContext, data.lowHighlight);
                                }
                                try
                                {
                                    ButtonBaseAdapter.PaintButtonBackground(graphics, rect, brush3);
                                }
                                finally
                                {
                                    brush3.Dispose();
                                }
                            }
                            goto Label_04CF;
                        }
                        finally
                        {
                            g.ReleaseHdc();
                        }
                    }
                    if (((base.Grid.MouseEnteredCellAddress.Y == rowIndex) && (base.Grid.MouseEnteredCellAddress.X == base.ColumnIndex)) && mouseInContentBounds)
                    {
                        IntPtr hDc = g.GetHdc();
                        try
                        {
                            using (WindowsGraphics graphics2 = WindowsGraphics.FromHdc(hDc))
                            {
                                Color controlDark = SystemColors.ControlDark;
                                using (WindowsBrush brush4 = new WindowsSolidBrush(graphics2.DeviceContext, controlDark))
                                {
                                    ButtonBaseAdapter.PaintButtonBackground(graphics2, rect, brush4);
                                }
                            }
                        }
                        finally
                        {
                            g.ReleaseHdc();
                        }
                    }
                }
            }
        Label_04CF:
            empty = rect;
        Label_06AD:
            if (((paint && GridCell.PaintFocus(paintParts)) && (flag2 && base.Grid.ShowFocusCues)) && ((base.Grid.Focused && (rect.Width > ((2 * SystemInformation.Border3DSize.Width) + 1))) && (rect.Height > ((2 * SystemInformation.Border3DSize.Height) + 1))))
            {
                if ((this.FlatStyle == System.Windows.Forms.FlatStyle.System) || (this.FlatStyle == System.Windows.Forms.FlatStyle.Standard))
                {
                    ControlPaint.DrawFocusRectangle(g, Rectangle.Inflate(rect, -1, -1), Color.Empty, SystemColors.Control);
                }
                else if (this.FlatStyle == System.Windows.Forms.FlatStyle.Flat)
                {
                    if (((this.ButtonState & (System.Windows.Forms.ButtonState.Checked | System.Windows.Forms.ButtonState.Pushed)) != System.Windows.Forms.ButtonState.Normal) || ((base.Grid.CurrentCellAddress.Y == rowIndex) && (base.Grid.CurrentCellAddress.X == base.ColumnIndex)))
                    {
                        ButtonBaseAdapter.ColorData data5 = ButtonBaseAdapter.PaintFlatRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.Grid.Enabled).Calculate();
                        string str2 = (text != null) ? text : string.Empty;
                        ButtonBaseAdapter.LayoutOptions options = ButtonFlatAdapter.PaintFlatLayout(g, true, SystemInformation.HighContrast, 1, rect, Padding.Empty, false, cellStyle.Font, str2, base.Grid.Enabled, GridUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.Grid.RightToLeft);
                        options.everettButtonCompat = false;
                        ButtonBaseAdapter.LayoutData data6 = options.Layout();
                        ButtonBaseAdapter.DrawFlatFocus(g, data6.focus, data5.options.highContrast ? data5.windowText : data5.constrastButtonShadow);
                    }
                }
                else if (((this.ButtonState & (System.Windows.Forms.ButtonState.Checked | System.Windows.Forms.ButtonState.Pushed)) != System.Windows.Forms.ButtonState.Normal) || ((base.Grid.CurrentCellAddress.Y == rowIndex) && (base.Grid.CurrentCellAddress.X == base.ColumnIndex)))
                {
                    bool up = this.ButtonState == System.Windows.Forms.ButtonState.Normal;
                    string str3 = (text != null) ? text : string.Empty;
                    ButtonBaseAdapter.LayoutOptions options2 = ButtonPopupAdapter.PaintPopupLayout(g, up, SystemInformation.HighContrast ? 2 : 1, rect, Padding.Empty, false, cellStyle.Font, str3, base.Grid.Enabled, GridUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.Grid.RightToLeft);
                    options2.everettButtonCompat = false;
                    ButtonBaseAdapter.LayoutData data7 = options2.Layout();
                    ControlPaint.DrawFocusRectangle(g, data7.focus, cellStyle.ForeColor, cellStyle.BackColor);
                }
            }
            if (((text != null) && paint) && GridCell.PaintContentForeground(paintParts))
            {
                rect.Offset(2, 1);
                rect.Width -= 4;
                rect.Height -= 2;
                if ((((this.ButtonState & (System.Windows.Forms.ButtonState.Checked | System.Windows.Forms.ButtonState.Pushed)) != System.Windows.Forms.ButtonState.Normal) && (this.FlatStyle != System.Windows.Forms.FlatStyle.Flat)) && (this.FlatStyle != System.Windows.Forms.FlatStyle.Popup))
                {
                    rect.Offset(1, 1);
                    rect.Width--;
                    rect.Height--;
                }
                if ((rect.Width > 0) && (rect.Height > 0))
                {
                    Color color;
                    if (base.Grid.ApplyVisualStylesToInnerCells && ((this.FlatStyle == System.Windows.Forms.FlatStyle.System) || (this.FlatStyle == System.Windows.Forms.FlatStyle.Standard)))
                    {
                        color = GridButtonCellRenderer.GridButtonRenderer.GetColor(ColorProperty.TextColor);
                    }
                    else
                    {
                        color = brush2.Color;
                    }
                    TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
                    TextRenderer.DrawText(g, text, cellStyle.Font, rect, color, flags);
                }
            }
            if ((base.Grid.ShowCellErrors && paint) && GridCell.PaintErrorIcon(paintParts))
            {
                base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
            }
            return empty;
        }

        /// <summary>Returns the string representation of the cell.</summary>
        /// <returns>A <see cref="T:System.String"></see> that represents the current cell.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridButtonCell { ColumnIndex=" + base.ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + base.RowIndex.ToString(CultureInfo.CurrentCulture) + " }");
        }

        private void UpdateButtonState(System.Windows.Forms.ButtonState newButtonState, int rowIndex)
        {
            if (this.ButtonState != newButtonState)
            {
                this.ButtonState = newButtonState;
                base.Grid.InvalidateCell(base.ColumnIndex, rowIndex);
            }
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

        /// <summary>Gets or sets the style determining the button's appearance.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle"></see> values.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.FlatStyle"></see> value. </exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(2)]
        public System.Windows.Forms.FlatStyle FlatStyle
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropButtonCellFlatStyle, out flag);
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
                    base.Properties.SetInteger(PropButtonCellFlatStyle, (int) value);
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
                    base.Properties.SetInteger(PropButtonCellFlatStyle, (int) value);
                }
            }
        }

        /// <filterpriority>1</filterpriority>
        public override System.Type FormattedValueType
        {
            get
            {
                return defaultFormattedValueType;
            }
        }

        /// <summary>Gets or sets a value indicating whether the owning column's text will appear on the button displayed by the cell.</summary>
        /// <returns>true if the value of the <see cref="P:MControl.GridView.GridCell.Value"></see> property will automatically match the value of the <see cref="P:MControl.GridView.GridButtonColumn.Text"></see> property of the owning column; otherwise, false. The default is false.</returns>
        [DefaultValue(false)]
        public bool UseColumnTextForButtonValue
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropButtonCellUseColumnTextForButtonValue, out flag);
                if (!flag)
                {
                    return false;
                }
                return (integer != 0);
            }
            set
            {
                if (value != this.UseColumnTextForButtonValue)
                {
                    base.Properties.SetInteger(PropButtonCellUseColumnTextForButtonValue, value ? 1 : 0);
                    base.OnCommonChange();
                }
            }
        }

        internal bool UseColumnTextForButtonValueInternal
        {
            set
            {
                if (value != this.UseColumnTextForButtonValue)
                {
                    base.Properties.SetInteger(PropButtonCellUseColumnTextForButtonValue, value ? 1 : 0);
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

        /// <summary>Provides information about a <see cref="T:MControl.GridView.GridButtonCell"></see> to accessibility client applications.</summary>
        protected class GridButtonCellAccessibleObject : GridCell.GridCellAccessibleObject
        {
            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridButtonCell.GridButtonCellAccessibleObject"></see> class. </summary>
            /// <param name="owner">The <see cref="T:MControl.GridView.GridCell"></see> that owns the <see cref="T:MControl.GridView.GridButtonCell.GridButtonCellAccessibleObject"></see>.</param>
            public GridButtonCellAccessibleObject(GridCell owner) : base(owner)
            {
            }

            /// <summary>Performs the default action of the <see cref="T:MControl.GridView.GridButtonCell.GridButtonCellAccessibleObject"></see></summary>
            /// <exception cref="T:System.InvalidOperationException">The <see cref="T:MControl.GridView.GridButtonCell"></see> returned by the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property does not belong to a <see cref="T:MControl.GridView.Grid"></see> control.-or-The <see cref="T:MControl.GridView.GridButtonCell"></see> returned by the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property belongs to a shared row.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void DoDefaultAction()
            {
                GridButtonCell owner = (GridButtonCell) base.Owner;
                Grid grid = owner.Grid;
                if ((grid != null) && (owner.RowIndex == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedCell"));
                }
                if ((owner.OwningColumn != null) && (owner.OwningRow != null))
                {
                    grid.OnCellClickInternal(new GridCellEventArgs(owner.ColumnIndex, owner.RowIndex));
                    grid.OnCellContentClickInternal(new GridCellEventArgs(owner.ColumnIndex, owner.RowIndex));
                }
            }

            /// <summary>Gets the number of child accessible objects that belong to the <see cref="T:MControl.GridView.GridButtonCell.GridButtonCellAccessibleObject"></see>.</summary>
            /// <returns>The value –1.</returns>
            public override int GetChildCount()
            {
                return 0;
            }

            /// <summary>Gets a <see cref="T:System.String"></see> that represents the default action of the <see cref="T:MControl.GridView.GridButtonCell.GridButtonCellAccessibleObject"></see>.</summary>
            /// <returns>The <see cref="T:System.String"></see> "Press" if the <see cref="P:MControl.GridView.GridCell.ReadOnly"></see> property is set to false; otherwise, an empty <see cref="T:System.String"></see> ("").</returns>
            public override string DefaultAction
            {
                get
                {
                    return MControl.GridView.RM.GetString("Grid_AccButtonCellDefaultAction");
                }
            }
        }

        private class GridButtonCellRenderer
        {
            private static VisualStyleRenderer visualStyleRenderer;

            private GridButtonCellRenderer()
            {
            }

            public static void DrawButton(Graphics g, Rectangle bounds, int buttonState)
            {
                GridButtonRenderer.SetParameters(GridButtonCell.ButtonElement.ClassName, GridButtonCell.ButtonElement.Part, buttonState);
                GridButtonRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
            }

            public static VisualStyleRenderer GridButtonRenderer
            {
                get
                {
                    if (visualStyleRenderer == null)
                    {
                        visualStyleRenderer = new VisualStyleRenderer(GridButtonCell.ButtonElement);
                    }
                    return visualStyleRenderer;
                }
            }
        }
    }
}

