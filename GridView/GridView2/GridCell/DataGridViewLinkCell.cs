namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Security.Permissions;
    using System.Windows.Forms;

    /// <summary>Represents a cell that contains a link. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridLinkCell : GridCell
    {
        private static readonly GridContentAlignment anyBottom = (GridContentAlignment.BottomRight | GridContentAlignment.BottomCenter | GridContentAlignment.BottomLeft);
        private static readonly GridContentAlignment anyLeft = (GridContentAlignment.BottomLeft | GridContentAlignment.MiddleLeft | GridContentAlignment.TopLeft);
        private static readonly GridContentAlignment anyRight = (GridContentAlignment.BottomRight | GridContentAlignment.MiddleRight | GridContentAlignment.TopRight);
        private static System.Type cellType = typeof(GridLinkCell);
        private static Cursor gridCursor = null;
        private const byte GRIDLINKCELL_horizontalTextMarginLeft = 1;
        private const byte GRIDLINKCELL_horizontalTextMarginRight = 2;
        private const byte GRIDLINKCELL_verticalTextMarginBottom = 1;
        private const byte GRIDLINKCELL_verticalTextMarginTop = 1;
        private static System.Type defaultFormattedValueType = typeof(string);
        private static System.Type defaultValueType = typeof(object);
        private bool linkVisited;
        private bool linkVisitedSet;
        private static readonly int PropLinkCellActiveLinkColor = PropertyStore.CreateKey();
        private static readonly int PropLinkCellLinkBehavior = PropertyStore.CreateKey();
        private static readonly int PropLinkCellLinkColor = PropertyStore.CreateKey();
        private static readonly int PropLinkCellLinkState = PropertyStore.CreateKey();
        private static readonly int PropLinkCellTrackVisitedState = PropertyStore.CreateKey();
        private static readonly int PropLinkCellUseColumnTextForLinkValue = PropertyStore.CreateKey();
        private static readonly int PropLinkCellVisitedLinkColor = PropertyStore.CreateKey();

        /// <summary>Creates an exact copy of this cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridLinkCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridLinkCell cell;
            System.Type type = base.GetType();
            if (type == cellType)
            {
                cell = new GridLinkCell();
            }
            else
            {
                cell = (GridLinkCell) Activator.CreateInstance(type);
            }
            base.CloneInternal(cell);
            if (base.Properties.ContainsObject(PropLinkCellActiveLinkColor))
            {
                cell.ActiveLinkColorInternal = this.ActiveLinkColor;
            }
            if (base.Properties.ContainsInteger(PropLinkCellUseColumnTextForLinkValue))
            {
                cell.UseColumnTextForLinkValueInternal = this.UseColumnTextForLinkValue;
            }
            if (base.Properties.ContainsInteger(PropLinkCellLinkBehavior))
            {
                cell.LinkBehaviorInternal = this.LinkBehavior;
            }
            if (base.Properties.ContainsObject(PropLinkCellLinkColor))
            {
                cell.LinkColorInternal = this.LinkColor;
            }
            if (base.Properties.ContainsInteger(PropLinkCellTrackVisitedState))
            {
                cell.TrackVisitedStateInternal = this.TrackVisitedState;
            }
            if (base.Properties.ContainsObject(PropLinkCellVisitedLinkColor))
            {
                cell.VisitedLinkColorInternal = this.VisitedLinkColor;
            }
            if (this.linkVisitedSet)
            {
                cell.LinkVisited = this.LinkVisited;
            }
            return cell;
        }

        /// <summary>Creates a new accessible object for the <see cref="T:MControl.GridView.GridLinkCell"></see>. </summary>
        /// <returns>A new <see cref="T:MControl.GridView.GridLinkCell.GridLinkCellAccessibleObject"></see> for the <see cref="T:MControl.GridView.GridLinkCell"></see>. </returns>
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new GridLinkCellAccessibleObject(this);
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
            object obj2 = this.GetValue(rowIndex);
            object formattedValue = this.GetFormattedValue(obj2, rowIndex, ref cellStyle, null, null, GridDataErrorContexts.Formatting);
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, formattedValue, this.GetErrorText(rowIndex), cellStyle, style, GridPaintParts.ContentForeground, false, true, false);
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
                        size = new Size(0, GridCell.MeasureTextHeight(graphics, str, cellStyle.Font, Math.Max(1, ((constraintSize.Width - num) - 1) - 2), flags));
                        goto Label_01DF;

                    case GridFreeDimension.Width:
                    {
                        int num3 = ((constraintSize.Height - num2) - 1) - 1;
                        if ((cellStyle.Alignment & anyBottom) != GridContentAlignment.NotSet)
                        {
                            num3--;
                        }
                        size = new Size(GridCell.MeasureTextWidth(graphics, str, cellStyle.Font, Math.Max(1, num3), flags), 0);
                        goto Label_01DF;
                    }
                }
                size = GridCell.MeasureTextPreferredSize(graphics, str, cellStyle.Font, 5f, flags);
            }
            else
            {
                switch (freeDimensionFromConstraint)
                {
                    case GridFreeDimension.Height:
                        size = new Size(0, GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags).Height);
                        goto Label_01DF;

                    case GridFreeDimension.Width:
                        size = new Size(GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags).Width, 0);
                        goto Label_01DF;
                }
                size = GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags);
            }
        Label_01DF:
            if (freeDimensionFromConstraint != GridFreeDimension.Height)
            {
                size.Width += 3 + num;
                if (base.Grid.ShowCellErrors)
                {
                    size.Width = Math.Max(size.Width, (num + 8) + 12);
                }
            }
            if (freeDimensionFromConstraint != GridFreeDimension.Width)
            {
                size.Height += 2 + num2;
                if ((cellStyle.Alignment & anyBottom) != GridContentAlignment.NotSet)
                {
                    size.Height++;
                }
                if (base.Grid.ShowCellErrors)
                {
                    size.Height = Math.Max(size.Height, (num2 + 8) + 11);
                }
            }
            return size;
        }

        protected override object GetValue(int rowIndex)
        {
            if (((this.UseColumnTextForLinkValue && (base.Grid != null)) && ((base.Grid.NewRowIndex != rowIndex) && (base.OwningColumn != null))) && (base.OwningColumn is GridLinkColumn))
            {
                return ((GridLinkColumn) base.OwningColumn).Text;
            }
            return base.GetValue(rowIndex);
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when a key is released and the cell has focus.</summary>
        /// <returns>true if the SPACE key was released, the <see cref="P:MControl.GridView.GridLinkCell.TrackVisitedState"></see> property is true, the <see cref="P:MControl.GridView.GridLinkCell.LinkVisited"></see> property is false, and the CTRL, ALT, and SHIFT keys are not pressed; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains data about the key press.</param>
        /// <param name="rowIndex">The index of the row containing the cell.</param>
        protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return ((((e.KeyCode != Keys.Space) || e.Alt) || (e.Control || e.Shift)) || (this.TrackVisitedState && !this.LinkVisited));
        }

        private bool LinkBoundsContainPoint(int x, int y, int rowIndex)
        {
            return base.GetContentBounds(rowIndex).Contains(x, y);
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is pressed while the pointer is over the cell.</summary>
        /// <returns>true if the mouse pointer is over the link; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains data about the mouse click.</param>
        protected override bool MouseDownUnsharesRow(GridCellMouseEventArgs e)
        {
            return this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex);
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer leaves the cell.</summary>
        /// <returns>true if the link displayed by the cell is not in the normal state; otherwise, false.</returns>
        /// <param name="rowIndex">The index of the row containing the cell.</param>
        protected override bool MouseLeaveUnsharesRow(int rowIndex)
        {
            return (this.LinkState != System.Windows.Forms.LinkState.Normal);
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer moves over the cell.</summary>
        /// <returns>true if the mouse pointer is over the link and the link is has not yet changed color to reflect the hover state; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains data about the mouse click.</param>
        protected override bool MouseMoveUnsharesRow(GridCellMouseEventArgs e)
        {
            if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex))
            {
                if ((this.LinkState & System.Windows.Forms.LinkState.Hover) == System.Windows.Forms.LinkState.Normal)
                {
                    return true;
                }
            }
            else if ((this.LinkState & System.Windows.Forms.LinkState.Hover) != System.Windows.Forms.LinkState.Normal)
            {
                return true;
            }
            return false;
        }

        /// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is released while the pointer is over the cell. </summary>
        /// <returns>true if the mouse pointer is over the link; otherwise, false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains data about the mouse click.</param>
        protected override bool MouseUpUnsharesRow(GridCellMouseEventArgs e)
        {
            return (this.TrackVisitedState && this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex));
        }

        protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
        {
            if ((base.Grid != null) && (((e.KeyCode == Keys.Space) && !e.Alt) && (!e.Control && !e.Shift)))
            {
                base.RaiseCellClick(new GridCellEventArgs(base.ColumnIndex, rowIndex));
                if (((base.Grid != null) && (base.ColumnIndex < base.Grid.Columns.Count)) && (rowIndex < base.Grid.Rows.Count))
                {
                    base.RaiseCellContentClick(new GridCellEventArgs(base.ColumnIndex, rowIndex));
                    if (this.TrackVisitedState)
                    {
                        this.LinkVisited = true;
                    }
                }
                e.Handled = true;
            }
        }

        protected override void OnMouseDown(GridCellMouseEventArgs e)
        {
            if (base.Grid != null)
            {
                if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex))
                {
                    this.LinkState |= System.Windows.Forms.LinkState.Active;
                    base.Grid.InvalidateCell(base.ColumnIndex, e.RowIndex);
                }
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseLeave(int rowIndex)
        {
            if (base.Grid != null)
            {
                if (gridCursor != null)
                {
                    base.Grid.Cursor = gridCursor;
                    gridCursor = null;
                }
                if (this.LinkState != System.Windows.Forms.LinkState.Normal)
                {
                    this.LinkState = System.Windows.Forms.LinkState.Normal;
                    base.Grid.InvalidateCell(base.ColumnIndex, rowIndex);
                }
                base.OnMouseLeave(rowIndex);
            }
        }

        protected override void OnMouseMove(GridCellMouseEventArgs e)
        {
            if (base.Grid != null)
            {
                if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex))
                {
                    if ((this.LinkState & System.Windows.Forms.LinkState.Hover) == System.Windows.Forms.LinkState.Normal)
                    {
                        this.LinkState |= System.Windows.Forms.LinkState.Hover;
                        base.Grid.InvalidateCell(base.ColumnIndex, e.RowIndex);
                    }
                    if (gridCursor == null)
                    {
                        gridCursor = base.Grid.UserSetCursor;
                    }
                    if (base.Grid.Cursor != Cursors.Hand)
                    {
                        base.Grid.Cursor = Cursors.Hand;
                    }
                }
                else if ((this.LinkState & System.Windows.Forms.LinkState.Hover) != System.Windows.Forms.LinkState.Normal)
                {
                    this.LinkState &= ~System.Windows.Forms.LinkState.Hover;
                    base.Grid.Cursor = gridCursor;
                    base.Grid.InvalidateCell(base.ColumnIndex, e.RowIndex);
                }
                base.OnMouseMove(e);
            }
        }

        protected override void OnMouseUp(GridCellMouseEventArgs e)
        {
            if ((base.Grid != null) && (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex) && this.TrackVisitedState))
            {
                this.LinkVisited = true;
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
        }

        private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
        {
            if (paint && GridCell.PaintBorder(paintParts))
            {
                this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            Rectangle empty = Rectangle.Empty;
            Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
            Rectangle rect = cellBounds;
            rect.Offset(rectangle2.X, rectangle2.Y);
            rect.Width -= rectangle2.Right;
            rect.Height -= rectangle2.Bottom;
            Point currentCellAddress = base.Grid.CurrentCellAddress;
            bool flag = (currentCellAddress.X == base.ColumnIndex) && (currentCellAddress.Y == rowIndex);
            bool flag2 = (cellState & GridElementStates.Selected) != GridElementStates.None;
            SolidBrush cachedBrush = base.Grid.GetCachedBrush((GridCell.PaintSelectionBackground(paintParts) && flag2) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
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
            Rectangle rectangle = rect;
            string text = formattedValue as string;
            if ((text != null) && (paint || computeContentBounds))
            {
                rect.Offset(1, 1);
                rect.Width -= 3;
                rect.Height -= 2;
                if ((cellStyle.Alignment & anyBottom) != GridContentAlignment.NotSet)
                {
                    rect.Height--;
                }
                Font linkFont = null;
                Font hoverLinkFont = null;
                LinkUtilities.EnsureLinkFonts(cellStyle.Font, this.LinkBehavior, ref linkFont, ref hoverLinkFont);
                TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
                if (paint)
                {
                    if ((rect.Width > 0) && (rect.Height > 0))
                    {
                        Color activeLinkColor;
                        if ((flag && base.Grid.ShowFocusCues) && (base.Grid.Focused && GridCell.PaintFocus(paintParts)))
                        {
                            Rectangle rectangle5 = GridUtilities.GetTextBounds(rect, text, flags, cellStyle, (this.LinkState == System.Windows.Forms.LinkState.Hover) ? hoverLinkFont : linkFont);
                            if ((cellStyle.Alignment & anyLeft) != GridContentAlignment.NotSet)
                            {
                                rectangle5.X--;
                                rectangle5.Width++;
                            }
                            else if ((cellStyle.Alignment & anyRight) != GridContentAlignment.NotSet)
                            {
                                rectangle5.X++;
                                rectangle5.Width++;
                            }
                            rectangle5.Height += 2;
                            ControlPaint.DrawFocusRectangle(g, rectangle5, Color.Empty, cachedBrush.Color);
                        }
                        if ((this.LinkState & System.Windows.Forms.LinkState.Active) == System.Windows.Forms.LinkState.Active)
                        {
                            activeLinkColor = this.ActiveLinkColor;
                        }
                        else if (this.LinkVisited)
                        {
                            activeLinkColor = this.VisitedLinkColor;
                        }
                        else
                        {
                            activeLinkColor = this.LinkColor;
                        }
                        if (GridCell.PaintContentForeground(paintParts))
                        {
                            if ((flags & TextFormatFlags.SingleLine) != TextFormatFlags.GlyphOverhangPadding)
                            {
                                flags |= TextFormatFlags.EndEllipsis;
                            }
                            TextRenderer.DrawText(g, text, (this.LinkState == System.Windows.Forms.LinkState.Hover) ? hoverLinkFont : linkFont, rect, activeLinkColor, flags);
                        }
                    }
                    else if (((flag && base.Grid.ShowFocusCues) && (base.Grid.Focused && GridCell.PaintFocus(paintParts))) && ((rectangle.Width > 0) && (rectangle.Height > 0)))
                    {
                        ControlPaint.DrawFocusRectangle(g, rectangle, Color.Empty, cachedBrush.Color);
                    }
                }
                else
                {
                    empty = GridUtilities.GetTextBounds(rect, text, flags, cellStyle, (this.LinkState == System.Windows.Forms.LinkState.Hover) ? hoverLinkFont : linkFont);
                }
                linkFont.Dispose();
                hoverLinkFont.Dispose();
            }
            else if (paint || computeContentBounds)
            {
                if (((flag && base.Grid.ShowFocusCues) && (base.Grid.Focused && GridCell.PaintFocus(paintParts))) && ((paint && (rect.Width > 0)) && (rect.Height > 0)))
                {
                    ControlPaint.DrawFocusRectangle(g, rect, Color.Empty, cachedBrush.Color);
                }
            }
            else if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
            {
                empty = base.ComputeErrorIconBounds(rectangle);
            }
            if ((base.Grid.ShowCellErrors && paint) && GridCell.PaintErrorIcon(paintParts))
            {
                base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, rectangle, errorText);
            }
            return empty;
        }

        private bool ShouldSerializeActiveLinkColor()
        {
            return !this.ActiveLinkColor.Equals(LinkUtilities.IEActiveLinkColor);
        }

        private bool ShouldSerializeLinkColor()
        {
            return !this.LinkColor.Equals(LinkUtilities.IELinkColor);
        }

        private bool ShouldSerializeLinkVisited()
        {
            return (this.linkVisitedSet = true);
        }

        private bool ShouldSerializeVisitedLinkColor()
        {
            return !this.VisitedLinkColor.Equals(LinkUtilities.IEVisitedLinkColor);
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridLinkCell { ColumnIndex=" + base.ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + base.RowIndex.ToString(CultureInfo.CurrentCulture) + " }");
        }

        /// <summary>Gets or sets the color used to display an active link.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the color used to display a link that is being selected. The default value is the user's Internet Explorer setting for the color of links in the hover state. </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        public Color ActiveLinkColor
        {
            get
            {
                if (base.Properties.ContainsObject(PropLinkCellActiveLinkColor))
                {
                    return (Color) base.Properties.GetObject(PropLinkCellActiveLinkColor);
                }
                return LinkUtilities.IEActiveLinkColor;
            }
            set
            {
                if (!value.Equals(this.ActiveLinkColor))
                {
                    base.Properties.SetObject(PropLinkCellActiveLinkColor, value);
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

        internal Color ActiveLinkColorInternal
        {
            set
            {
                if (!value.Equals(this.ActiveLinkColor))
                {
                    base.Properties.SetObject(PropLinkCellActiveLinkColor, value);
                }
            }
        }

        /// <summary>Gets the type of the cell's hosted editing control.</summary>
        /// <returns>Always null. </returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type EditType
        {
            get
            {
                return null;
            }
        }

        /// <summary>Gets the display <see cref="T:System.Type"></see> of the cell value.</summary>
        /// <returns>Always <see cref="T:System.String"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type FormattedValueType
        {
            get
            {
                return defaultFormattedValueType;
            }
        }

        /// <summary>Gets or sets a value that represents the behavior of a link.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior"></see> values. The default is <see cref="F:System.Windows.Forms.LinkBehavior.SystemDefault"></see>.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.LinkBehavior"></see> value.</exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(0)]
        public System.Windows.Forms.LinkBehavior LinkBehavior
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropLinkCellLinkBehavior, out flag);
                if (flag)
                {
                    return (System.Windows.Forms.LinkBehavior) integer;
                }
                return System.Windows.Forms.LinkBehavior.SystemDefault;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 3))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(System.Windows.Forms.LinkBehavior));
                }
                if (value != this.LinkBehavior)
                {
                    base.Properties.SetInteger(PropLinkCellLinkBehavior, (int) value);
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

        internal System.Windows.Forms.LinkBehavior LinkBehaviorInternal
        {
            set
            {
                if (value != this.LinkBehavior)
                {
                    base.Properties.SetInteger(PropLinkCellLinkBehavior, (int) value);
                }
            }
        }

        /// <summary>Gets or sets the color used to display an inactive and unvisited link.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the color used to initially display a link. The default value is the user's Internet Explorer setting for the link color.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        public Color LinkColor
        {
            get
            {
                if (base.Properties.ContainsObject(PropLinkCellLinkColor))
                {
                    return (Color) base.Properties.GetObject(PropLinkCellLinkColor);
                }
                return LinkUtilities.IELinkColor;
            }
            set
            {
                if (!value.Equals(this.LinkColor))
                {
                    base.Properties.SetObject(PropLinkCellLinkColor, value);
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

        internal Color LinkColorInternal
        {
            set
            {
                if (!value.Equals(this.LinkColor))
                {
                    base.Properties.SetObject(PropLinkCellLinkColor, value);
                }
            }
        }

        private System.Windows.Forms.LinkState LinkState
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropLinkCellLinkState, out flag);
                if (flag)
                {
                    return (System.Windows.Forms.LinkState) integer;
                }
                return System.Windows.Forms.LinkState.Normal;
            }
            set
            {
                if (this.LinkState != value)
                {
                    base.Properties.SetInteger(PropLinkCellLinkState, (int) value);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the link was visited.</summary>
        /// <returns>true if the link has been visited; otherwise, false. The default is false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool LinkVisited
        {
            get
            {
                return (this.linkVisitedSet && this.linkVisited);
            }
            set
            {
                this.linkVisitedSet = true;
                if (value != this.LinkVisited)
                {
                    this.linkVisited = value;
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

        /// <summary>Gets or sets a value indicating whether the link changes color when it is visited.</summary>
        /// <returns>true if the link changes color when it is selected; otherwise, false. The default is true.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(true)]
        public bool TrackVisitedState
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropLinkCellTrackVisitedState, out flag);
                return (!flag || (integer != 0));
            }
            set
            {
                if (value != this.TrackVisitedState)
                {
                    base.Properties.SetInteger(PropLinkCellTrackVisitedState, value ? 1 : 0);
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

        internal bool TrackVisitedStateInternal
        {
            set
            {
                if (value != this.TrackVisitedState)
                {
                    base.Properties.SetInteger(PropLinkCellTrackVisitedState, value ? 1 : 0);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the column <see cref="P:MControl.GridView.GridLinkColumn.Text"></see> property value is displayed as the link text.</summary>
        /// <returns>true if the column <see cref="P:MControl.GridView.GridLinkColumn.Text"></see> property value is displayed as the link text; false if the cell <see cref="P:MControl.GridView.GridCell.FormattedValue"></see> property value is displayed as the link text. The default is false.</returns>
        [DefaultValue(false)]
        public bool UseColumnTextForLinkValue
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropLinkCellUseColumnTextForLinkValue, out flag);
                if (!flag)
                {
                    return false;
                }
                return (integer != 0);
            }
            set
            {
                if (value != this.UseColumnTextForLinkValue)
                {
                    base.Properties.SetInteger(PropLinkCellUseColumnTextForLinkValue, value ? 1 : 0);
                    base.OnCommonChange();
                }
            }
        }

        internal bool UseColumnTextForLinkValueInternal
        {
            set
            {
                if (value != this.UseColumnTextForLinkValue)
                {
                    base.Properties.SetInteger(PropLinkCellUseColumnTextForLinkValue, value ? 1 : 0);
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

        /// <summary>Gets or sets the color used to display a link that has been previously visited.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the color used to display a link that has been visited. The default value is the user's Internet Explorer setting for the visited link color.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        public Color VisitedLinkColor
        {
            get
            {
                if (base.Properties.ContainsObject(PropLinkCellVisitedLinkColor))
                {
                    return (Color) base.Properties.GetObject(PropLinkCellVisitedLinkColor);
                }
                return LinkUtilities.IEVisitedLinkColor;
            }
            set
            {
                if (!value.Equals(this.VisitedLinkColor))
                {
                    base.Properties.SetObject(PropLinkCellVisitedLinkColor, value);
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

        internal Color VisitedLinkColorInternal
        {
            set
            {
                if (!value.Equals(this.VisitedLinkColor))
                {
                    base.Properties.SetObject(PropLinkCellVisitedLinkColor, value);
                }
            }
        }

        /// <summary>Provides information about a <see cref="T:MControl.GridView.GridLinkCell"></see> control to accessibility client applications.</summary>
        protected class GridLinkCellAccessibleObject : GridCell.GridCellAccessibleObject
        {
            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridLinkCell.GridLinkCellAccessibleObject"></see> class. </summary>
            /// <param name="owner">The <see cref="T:MControl.GridView.GridCell"></see> that owns the <see cref="T:MControl.GridView.GridLinkCell.GridLinkCellAccessibleObject"></see>.</param>
            public GridLinkCellAccessibleObject(GridCell owner) : base(owner)
            {
            }

            /// <summary>Performs the default action of the <see cref="T:MControl.GridView.GridLinkCell.GridLinkCellAccessibleObject"></see>.</summary>
            /// <exception cref="T:System.InvalidOperationException">The cell returned by the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property has a <see cref="P:MControl.GridView.GridElement.Grid"></see> property value that is not null and a <see cref="P:MControl.GridView.GridCell.RowIndex"></see> property value of -1, indicating that the cell is in a shared row.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void DoDefaultAction()
            {
                GridLinkCell owner = (GridLinkCell) base.Owner;
                Grid grid = owner.Grid;
                if ((grid != null) && (owner.RowIndex == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedCell"));
                }
                if ((owner.OwningColumn != null) && (owner.OwningRow != null))
                {
                    grid.OnCellContentClickInternal(new GridCellEventArgs(owner.ColumnIndex, owner.RowIndex));
                }
            }

            /// <summary>Gets the number of child accessible objects that belong to the <see cref="T:MControl.GridView.GridLinkCell.GridLinkCellAccessibleObject"></see>.</summary>
            /// <returns>The value –1.</returns>
            public override int GetChildCount()
            {
                return 0;
            }

            /// <summary>Gets a string that represents the default action of the <see cref="T:MControl.GridView.GridLinkCell.GridLinkCellAccessibleObject"></see>.</summary>
            /// <returns>The string "Click".</returns>
            public override string DefaultAction
            {
                get
                {
                    return MControl.GridView.RM.GetString("Grid_AccLinkCellDefaultAction");
                }
            }
        }
    }
}

