namespace MControl.GridView
{
    using System;
    using System.Drawing;
    using System.Security.Permissions;
    using System.Windows.Forms.VisualStyles;
    using System.Windows.Forms;

    /// <summary>Represents the cell in the top left corner of the <see cref="T:MControl.GridView.Grid"></see> that sits above the row headers and to the left of the column headers.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridTopLeftHeaderCell : GridColumnHeaderCell
    {
        private const byte GRIDTOPLEFTHEADERCELL_horizontalTextMarginLeft = 1;
        private const byte GRIDTOPLEFTHEADERCELL_horizontalTextMarginRight = 2;
        private const byte GRIDTOPLEFTHEADERCELL_verticalTextMargin = 1;
        private static readonly VisualStyleElement HeaderElement = VisualStyleElement.Header.Item.Normal;

        /// <summary>Creates a new accessible object for the <see cref="T:MControl.GridView.GridTopLeftHeaderCell"></see>. </summary>
        /// <returns>A new <see cref="T:MControl.GridView.GridTopLeftHeaderCell.GridTopLeftHeaderCellAccessibleObject"></see> for the <see cref="T:MControl.GridView.GridTopLeftHeaderCell"></see>. </returns>
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new GridTopLeftHeaderCellAccessibleObject(this);
        }

        /// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics"></see> object and cell style.</summary>
        /// <returns>The <see cref="T:System.Drawing.Rectangle"></see> that bounds the cell's contents.</returns>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> to be applied to the cell.</param>
        /// <param name="graphics">The graphics context for the cell.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex does not equal -1.</exception>
        protected override Rectangle GetContentBounds(Graphics graphics, GridCellStyle cellStyle, int rowIndex)
        {
            GridAdvancedBorderStyle style;
            GridElementStates states;
            Rectangle rectangle;
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if (base.Grid == null)
            {
                return Rectangle.Empty;
            }
            object formattedValue = this.GetValue(rowIndex);
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, formattedValue, null, cellStyle, style, GridPaintParts.ContentForeground, true, false, false);
        }

        /// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
        /// <returns>The <see cref="T:System.Drawing.Rectangle"></see> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty"></see>.</returns>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> to be applied to the cell.</param>
        /// <param name="graphics">The graphics context for the cell.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex does not equal -1.</exception>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, GridCellStyle cellStyle, int rowIndex)
        {
            GridAdvancedBorderStyle style;
            GridElementStates states;
            Rectangle rectangle;
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if (base.Grid == null)
            {
                return Rectangle.Empty;
            }
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, null, this.GetErrorText(rowIndex), cellStyle, style, GridPaintParts.ContentForeground, false, true, false);
        }

        /// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> that represents the preferred size, in pixels, of the cell.</returns>
        /// <param name="cellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the style of the cell.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to draw the cell.</param>
        /// <param name="constraintSize">The cell's maximum allowable size.</param>
        /// <param name="rowIndex">The zero-based row index of the cell.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex does not equal -1.</exception>
        protected override Size GetPreferredSize(Graphics graphics, GridCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if (base.Grid == null)
            {
                return new Size(-1, -1);
            }
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            Rectangle rectangle = this.BorderWidths(base.Grid.AdjustedTopLeftHeaderBorderStyle);
            int borderAndPaddingWidths = (rectangle.Left + rectangle.Width) + cellStyle.Padding.Horizontal;
            int borderAndPaddingHeights = (rectangle.Top + rectangle.Height) + cellStyle.Padding.Vertical;
            TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
            object obj2 = this.GetValue(rowIndex);
            if (!(obj2 is string))
            {
                obj2 = null;
            }
            return GridUtilities.GetPreferredRowHeaderSize(graphics, (string) obj2, cellStyle, borderAndPaddingWidths, borderAndPaddingHeights, base.Grid.ShowCellErrors, false, constraintSize, flags);
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
        }

        protected override void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle)
        {
            if (base.Grid != null)
            {
                base.PaintBorder(graphics, clipBounds, bounds, cellStyle, advancedBorderStyle);
                if (!base.Grid.RightToLeftInternal && base.Grid.ApplyVisualStylesToHeaderCells)
                {
                    if (base.Grid.AdvancedColumnHeadersBorderStyle.All == GridAdvancedCellBorderStyle.Inset)
                    {
                        Pen darkPen = null;
                        Pen lightPen = null;
                        base.GetContrastedPens(cellStyle.BackColor, ref darkPen, ref lightPen);
                        graphics.DrawLine(darkPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
                        graphics.DrawLine(darkPen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
                    }
                    else if (base.Grid.AdvancedColumnHeadersBorderStyle.All == GridAdvancedCellBorderStyle.Outset)
                    {
                        Pen pen3 = null;
                        Pen pen4 = null;
                        base.GetContrastedPens(cellStyle.BackColor, ref pen3, ref pen4);
                        graphics.DrawLine(pen4, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
                        graphics.DrawLine(pen4, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
                    }
                    else if (base.Grid.AdvancedColumnHeadersBorderStyle.All == GridAdvancedCellBorderStyle.InsetDouble)
                    {
                        Pen pen5 = null;
                        Pen pen6 = null;
                        base.GetContrastedPens(cellStyle.BackColor, ref pen5, ref pen6);
                        graphics.DrawLine(pen5, (int) (bounds.X + 1), (int) (bounds.Y + 1), (int) (bounds.X + 1), (int) (bounds.Bottom - 1));
                        graphics.DrawLine(pen5, (int) (bounds.X + 1), (int) (bounds.Y + 1), (int) (bounds.Right - 1), (int) (bounds.Y + 1));
                    }
                }
            }
        }

        private Rectangle PaintPrivate(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
        {
            Rectangle empty = Rectangle.Empty;
            Rectangle bounds = cellBounds;
            Rectangle rectangle3 = this.BorderWidths(advancedBorderStyle);
            bounds.Offset(rectangle3.X, rectangle3.Y);
            bounds.Width -= rectangle3.Right;
            bounds.Height -= rectangle3.Bottom;
            bool flag = (cellState & GridElementStates.Selected) != GridElementStates.None;
            if (paint && GridCell.PaintBackground(paintParts))
            {
                if (base.Grid.ApplyVisualStylesToHeaderCells)
                {
                    int headerState = 1;
                    if (base.ButtonState != ButtonState.Normal)
                    {
                        headerState = 3;
                    }
                    else if ((base.Grid.MouseEnteredCellAddress.Y == rowIndex) && (base.Grid.MouseEnteredCellAddress.X == base.ColumnIndex))
                    {
                        headerState = 2;
                    }
                    bounds.Inflate(0x10, 0x10);
                    GridTopLeftHeaderCellRenderer.DrawHeader(graphics, bounds, headerState);
                    bounds.Inflate(-16, -16);
                }
                else
                {
                    SolidBrush cachedBrush = base.Grid.GetCachedBrush((GridCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
                    if (cachedBrush.Color.A == 0xff)
                    {
                        graphics.FillRectangle(cachedBrush, bounds);
                    }
                }
            }
            if (paint && GridCell.PaintBorder(paintParts))
            {
                this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            if (cellStyle.Padding != Padding.Empty)
            {
                if (base.Grid.RightToLeftInternal)
                {
                    bounds.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
                }
                else
                {
                    bounds.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
                }
                bounds.Width -= cellStyle.Padding.Horizontal;
                bounds.Height -= cellStyle.Padding.Vertical;
            }
            Rectangle cellValueBounds = bounds;
            string str = formattedValue as string;
            bounds.Offset(1, 1);
            bounds.Width -= 3;
            bounds.Height -= 2;
            if ((((bounds.Width > 0) && (bounds.Height > 0)) && !string.IsNullOrEmpty(str)) && (paint || computeContentBounds))
            {
                Color color;
                if (base.Grid.ApplyVisualStylesToHeaderCells)
                {
                    color = GridTopLeftHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
                }
                else
                {
                    color = flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor;
                }
                TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
                if (paint)
                {
                    if (GridCell.PaintContentForeground(paintParts))
                    {
                        if ((flags & TextFormatFlags.SingleLine) != TextFormatFlags.GlyphOverhangPadding)
                        {
                            flags |= TextFormatFlags.EndEllipsis;
                        }
                        TextRenderer.DrawText(graphics, str, cellStyle.Font, bounds, color, flags);
                    }
                }
                else
                {
                    empty = GridUtilities.GetTextBounds(bounds, str, flags, cellStyle);
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
        public override string ToString()
        {
            return "GridTopLeftHeaderCell";
        }

        /// <summary>Provides information about a <see cref="T:MControl.GridView.GridTopLeftHeaderCell"></see> to accessibility client applications.</summary>
        protected class GridTopLeftHeaderCellAccessibleObject : GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject
        {
            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridTopLeftHeaderCell.GridTopLeftHeaderCellAccessibleObject"></see> class. </summary>
            /// <param name="owner">The <see cref="T:MControl.GridView.GridTopLeftHeaderCell"></see> that owns the <see cref="T:MControl.GridView.GridTopLeftHeaderCell.GridTopLeftHeaderCellAccessibleObject"></see>.</param>
            public GridTopLeftHeaderCellAccessibleObject(GridTopLeftHeaderCell owner) : base(owner)
            {
            }

            /// <summary>Performs the default action of the <see cref="T:MControl.GridView.GridTopLeftHeaderCell"></see>.</summary>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void DoDefaultAction()
            {
                base.Owner.Grid.SelectAll();
            }

            /// <summary>Navigates to another accessible object.</summary>
            /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject"></see> that represents an object in the specified direction.</returns>
            /// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation"></see> values.</param>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
            {
                switch (navigationDirection)
                {
                    case AccessibleNavigation.Left:
                        if (base.Owner.Grid.RightToLeft != RightToLeft.No)
                        {
                            return this.NavigateForward();
                        }
                        return null;

                    case AccessibleNavigation.Right:
                        if (base.Owner.Grid.RightToLeft != RightToLeft.No)
                        {
                            return null;
                        }
                        return this.NavigateForward();

                    case AccessibleNavigation.Next:
                        return this.NavigateForward();

                    case AccessibleNavigation.Previous:
                        return null;
                }
                return null;
            }

            private AccessibleObject NavigateForward()
            {
                if (base.Owner.Grid.Columns.GetColumnCount(GridElementStates.Visible) == 0)
                {
                    return null;
                }
                return base.Owner.Grid.AccessibilityObject.GetChild(0).GetChild(1);
            }

            /// <summary>Modifies the selection in the <see cref="T:MControl.GridView.Grid"></see> control or sets input focus to the control. </summary>
            /// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection"></see> values. </param>
            /// <exception cref="T:System.InvalidOperationException">The <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property value is null.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void Select(AccessibleSelection flags)
            {
                if (base.Owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                }
                if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
                {
                    base.Owner.Grid.FocusInternal();
                    if ((base.Owner.Grid.Columns.GetColumnCount(GridElementStates.Visible) > 0) && (base.Owner.Grid.Rows.GetRowCount(GridElementStates.Visible) > 0))
                    {
                        GridRow row = base.Owner.Grid.Rows[base.Owner.Grid.Rows.GetFirstRow(GridElementStates.Visible)];
                        GridColumn firstColumn = base.Owner.Grid.Columns.GetFirstColumn(GridElementStates.Visible);
                        base.Owner.Grid.SetCurrentCellAddressCoreInternal(firstColumn.Index, row.Index, false, true, false);
                    }
                }
                if (((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection) && base.Owner.Grid.MultiSelect)
                {
                    base.Owner.Grid.SelectAll();
                }
                if (((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection) && ((flags & AccessibleSelection.AddSelection) == AccessibleSelection.None))
                {
                    base.Owner.Grid.ClearSelection();
                }
            }

            public override Rectangle Bounds
            {
                get
                {
                    Rectangle r = base.Owner.Grid.GetCellDisplayRectangle(-1, -1, false);
                    return base.Owner.Grid.RectangleToScreen(r);
                }
            }

            /// <summary>Gets a description of the default action of the <see cref="T:MControl.GridView.GridTopLeftHeaderCell.GridTopLeftHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>The string "Press to Select All" if the <see cref="P:MControl.GridView.Grid.MultiSelect"></see> property is true; otherwise, an empty string ("").</returns>
            public override string DefaultAction
            {
                get
                {
                    if (base.Owner.Grid.MultiSelect)
                    {
                        return MControl.GridView.RM.GetString("Grid_AccTopLeftColumnHeaderCellDefaultAction");
                    }
                    return string.Empty;
                }
            }

            /// <summary>Gets the name of the <see cref="T:MControl.GridView.GridTopLeftHeaderCell.GridTopLeftHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>The name of the <see cref="T:MControl.GridView.GridTopLeftHeaderCell.GridTopLeftHeaderCellAccessibleObject"></see>.</returns>
            public override string Name
            {
                get
                {
                    object obj2 = base.Owner.Value;
                    if ((obj2 != null) && !(obj2 is string))
                    {
                        return string.Empty;
                    }
                    string str = obj2 as string;
                    if (!string.IsNullOrEmpty(str))
                    {
                        return string.Empty;
                    }
                    if (base.Owner.Grid == null)
                    {
                        return string.Empty;
                    }
                    if (base.Owner.Grid.RightToLeft == RightToLeft.No)
                    {
                        return MControl.GridView.RM.GetString("Grid_AccTopLeftColumnHeaderCellName");
                    }
                    return MControl.GridView.RM.GetString("Grid_AccTopLeftColumnHeaderCellNameRTL");
                }
            }

            /// <summary>Gets the state of the <see cref="T:MControl.GridView.GridTopLeftHeaderCell.GridTopLeftHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates"></see> values. The default is <see cref="F:System.Windows.Forms.AccessibleStates.Selectable"></see>.</returns>
            public override AccessibleStates State
            {
                get
                {
                    AccessibleStates selectable = AccessibleStates.Selectable;
                    if ((base.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen)
                    {
                        selectable |= AccessibleStates.Offscreen;
                    }
                    if (base.Owner.Grid.AreAllCellsSelected(false))
                    {
                        selectable |= AccessibleStates.Selected;
                    }
                    return selectable;
                }
            }

            /// <summary>The value of the containing <see cref="T:MControl.GridView.GridTopLeftHeaderCell"></see>.</summary>
            /// <returns>Always returns <see cref="F:System.String.Empty"></see>.</returns>
            public override string Value
            {
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    return string.Empty;
                }
            }
        }

        private class GridTopLeftHeaderCellRenderer
        {
            private static System.Windows.Forms.VisualStyles.VisualStyleRenderer visualStyleRenderer;

            private GridTopLeftHeaderCellRenderer()
            {
            }

            public static void DrawHeader(Graphics g, Rectangle bounds, int headerState)
            {
                VisualStyleRenderer.SetParameters(GridTopLeftHeaderCell.HeaderElement.ClassName, GridTopLeftHeaderCell.HeaderElement.Part, headerState);
                VisualStyleRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
            }

            public static System.Windows.Forms.VisualStyles.VisualStyleRenderer VisualStyleRenderer
            {
                get
                {
                    if (visualStyleRenderer == null)
                    {
                        visualStyleRenderer = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(GridTopLeftHeaderCell.HeaderElement);
                    }
                    return visualStyleRenderer;
                }
            }
        }
    }
}

