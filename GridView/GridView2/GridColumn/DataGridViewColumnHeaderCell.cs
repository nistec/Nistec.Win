namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Security.Permissions;
    using System.Text;
    using System.Windows.Forms.VisualStyles;
    using System.Windows.Forms;

    /// <summary>Represents a column header in a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridColumnHeaderCell : GridHeaderCell
    {
        private static System.Type cellType = typeof(GridColumnHeaderCell);
        private const byte GRIDCOLUMNHEADERCELL_horizontalTextMarginLeft = 2;
        private const byte GRIDCOLUMNHEADERCELL_horizontalTextMarginRight = 2;
        private const byte GRIDCOLUMNHEADERCELL_sortGlyphHeight = 7;
        private const byte GRIDCOLUMNHEADERCELL_sortGlyphHorizontalMargin = 4;
        private const byte GRIDCOLUMNHEADERCELL_sortGlyphSeparatorWidth = 2;
        private const byte GRIDCOLUMNHEADERCELL_sortGlyphWidth = 9;
        private const byte GRIDCOLUMNHEADERCELL_verticalMargin = 1;
        private static readonly VisualStyleElement HeaderElement = VisualStyleElement.Header.Item.Normal;
        private SortOrder sortGlyphDirection = SortOrder.None;

        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridColumnHeaderCell cell;
            System.Type type = base.GetType();
            if (type == cellType)
            {
                cell = new GridColumnHeaderCell();
            }
            else
            {
                cell = (GridColumnHeaderCell) Activator.CreateInstance(type);
            }
            base.CloneInternal(cell);
            cell.Value = base.Value;
            return cell;
        }

        /// <summary>Creates a new accessible object for the <see cref="T:MControl.GridView.GridColumnHeaderCell"></see>. </summary>
        /// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject"></see> for the <see cref="T:MControl.GridView.GridColumnHeaderCell"></see>. </returns>
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new GridColumnHeaderCellAccessibleObject(this);
        }

        /// <summary>Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard"></see>.</summary>
        /// <returns>A <see cref="T:System.Object"></see> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard"></see>.</returns>
        /// <param name="inLastRow">true to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="inFirstRow">true to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="lastCell">true to indicate that the cell is the last column of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="format">The current format string of the cell.</param>
        /// <param name="firstCell">true to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="rowIndex">The zero-based index of the row containing the cell.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is not -1.</exception>
        protected override object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
        {
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if (base.Grid == null)
            {
                return null;
            }
            object obj2 = this.GetValue(rowIndex);
            StringBuilder sb = new StringBuilder(0x40);
            if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
            {
                if (firstCell)
                {
                    sb.Append("<TABLE>");
                    sb.Append("<THEAD>");
                }
                sb.Append("<TH>");
                if (obj2 != null)
                {
                    GridCell.FormatPlainTextAsHtml(obj2.ToString(), new StringWriter(sb, CultureInfo.CurrentCulture));
                }
                else
                {
                    sb.Append("&nbsp;");
                }
                sb.Append("</TH>");
                if (lastCell)
                {
                    sb.Append("</THEAD>");
                    if (inLastRow)
                    {
                        sb.Append("</TABLE>");
                    }
                }
                return sb.ToString();
            }
            bool csv = string.Equals(format, DataFormats.CommaSeparatedValue, StringComparison.OrdinalIgnoreCase);
            if ((!csv && !string.Equals(format, DataFormats.Text, StringComparison.OrdinalIgnoreCase)) && !string.Equals(format, DataFormats.UnicodeText, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            if (obj2 != null)
            {
                bool escapeApplied = false;
                int length = sb.Length;
                GridCell.FormatPlainText(obj2.ToString(), csv, new StringWriter(sb, CultureInfo.CurrentCulture), ref escapeApplied);
                if (escapeApplied)
                {
                    sb.Insert(length, '"');
                }
            }
            if (lastCell)
            {
                if (!inLastRow)
                {
                    sb.Append('\r');
                    sb.Append('\n');
                }
            }
            else
            {
                sb.Append(csv ? ',' : '\t');
            }
            return sb.ToString();
        }

        /// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics"></see> object and cell style.</summary>
        /// <returns>The <see cref="T:System.Drawing.Rectangle"></see> that bounds the cell's contents.</returns>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> to be applied to the cell.</param>
        /// <param name="graphics">The graphics context for the cell.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is not -1.</exception>
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
            if ((base.Grid == null) || (base.OwningColumn == null))
            {
                return Rectangle.Empty;
            }
            object formattedValue = this.GetValue(rowIndex);
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, formattedValue, cellStyle, style, GridPaintParts.ContentForeground, false);
        }

        /// <summary>Retrieves the inherited shortcut menu for the specified row.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> of the column headers if one exists; otherwise, the <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> inherited from <see cref="T:MControl.GridView.Grid"></see>.</returns>
        /// <param name="rowIndex">The index of the row to get the <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> of. The index must be -1 to indicate the row of column headers.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is not -1.</exception>
        public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
        {
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            ContextMenuStrip contextMenuStrip = base.GetContextMenuStrip(-1);
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

        /// <summary>Gets the style applied to the cell. </summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that includes the style settings of the cell inherited from the cell's parent row, column, and <see cref="T:MControl.GridView.Grid"></see>.</returns>
        /// <param name="inheritedCellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> to be populated with the inherited cell style. </param>
        /// <param name="includeColors">true to include inherited colors in the returned cell style; otherwise, false. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is not -1.</exception>
        public override GridCellStyle GetInheritedStyle(GridCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
        {
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            GridCellStyle style = (inheritedCellStyle == null) ? new GridCellStyle() : inheritedCellStyle;
            GridCellStyle style2 = null;
            if (base.HasStyle)
            {
                style2 = base.Style;
            }
            GridCellStyle columnHeadersDefaultCellStyle = base.Grid.ColumnHeadersDefaultCellStyle;
            GridCellStyle defaultCellStyle = base.Grid.DefaultCellStyle;
            if (includeColors)
            {
                if ((style2 != null) && !style2.BackColor.IsEmpty)
                {
                    style.BackColor = style2.BackColor;
                }
                else if (!columnHeadersDefaultCellStyle.BackColor.IsEmpty)
                {
                    style.BackColor = columnHeadersDefaultCellStyle.BackColor;
                }
                else
                {
                    style.BackColor = defaultCellStyle.BackColor;
                }
                if ((style2 != null) && !style2.ForeColor.IsEmpty)
                {
                    style.ForeColor = style2.ForeColor;
                }
                else if (!columnHeadersDefaultCellStyle.ForeColor.IsEmpty)
                {
                    style.ForeColor = columnHeadersDefaultCellStyle.ForeColor;
                }
                else
                {
                    style.ForeColor = defaultCellStyle.ForeColor;
                }
                if ((style2 != null) && !style2.SelectionBackColor.IsEmpty)
                {
                    style.SelectionBackColor = style2.SelectionBackColor;
                }
                else if (!columnHeadersDefaultCellStyle.SelectionBackColor.IsEmpty)
                {
                    style.SelectionBackColor = columnHeadersDefaultCellStyle.SelectionBackColor;
                }
                else
                {
                    style.SelectionBackColor = defaultCellStyle.SelectionBackColor;
                }
                if ((style2 != null) && !style2.SelectionForeColor.IsEmpty)
                {
                    style.SelectionForeColor = style2.SelectionForeColor;
                }
                else if (!columnHeadersDefaultCellStyle.SelectionForeColor.IsEmpty)
                {
                    style.SelectionForeColor = columnHeadersDefaultCellStyle.SelectionForeColor;
                }
                else
                {
                    style.SelectionForeColor = defaultCellStyle.SelectionForeColor;
                }
            }
            if ((style2 != null) && (style2.Font != null))
            {
                style.Font = style2.Font;
            }
            else if (columnHeadersDefaultCellStyle.Font != null)
            {
                style.Font = columnHeadersDefaultCellStyle.Font;
            }
            else
            {
                style.Font = defaultCellStyle.Font;
            }
            if ((style2 != null) && !style2.IsNullValueDefault)
            {
                style.NullValue = style2.NullValue;
            }
            else if (!columnHeadersDefaultCellStyle.IsNullValueDefault)
            {
                style.NullValue = columnHeadersDefaultCellStyle.NullValue;
            }
            else
            {
                style.NullValue = defaultCellStyle.NullValue;
            }
            if ((style2 != null) && !style2.IsDataSourceNullValueDefault)
            {
                style.DataSourceNullValue = style2.DataSourceNullValue;
            }
            else if (!columnHeadersDefaultCellStyle.IsDataSourceNullValueDefault)
            {
                style.DataSourceNullValue = columnHeadersDefaultCellStyle.DataSourceNullValue;
            }
            else
            {
                style.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
            }
            if ((style2 != null) && (style2.Format.Length != 0))
            {
                style.Format = style2.Format;
            }
            else if (columnHeadersDefaultCellStyle.Format.Length != 0)
            {
                style.Format = columnHeadersDefaultCellStyle.Format;
            }
            else
            {
                style.Format = defaultCellStyle.Format;
            }
            if ((style2 != null) && !style2.IsFormatProviderDefault)
            {
                style.FormatProvider = style2.FormatProvider;
            }
            else if (!columnHeadersDefaultCellStyle.IsFormatProviderDefault)
            {
                style.FormatProvider = columnHeadersDefaultCellStyle.FormatProvider;
            }
            else
            {
                style.FormatProvider = defaultCellStyle.FormatProvider;
            }
            if ((style2 != null) && (style2.Alignment != GridContentAlignment.NotSet))
            {
                style.AlignmentInternal = style2.Alignment;
            }
            else if (columnHeadersDefaultCellStyle.Alignment != GridContentAlignment.NotSet)
            {
                style.AlignmentInternal = columnHeadersDefaultCellStyle.Alignment;
            }
            else
            {
                style.AlignmentInternal = defaultCellStyle.Alignment;
            }
            if ((style2 != null) && (style2.WrapMode != GridTriState.NotSet))
            {
                style.WrapModeInternal = style2.WrapMode;
            }
            else if (columnHeadersDefaultCellStyle.WrapMode != GridTriState.NotSet)
            {
                style.WrapModeInternal = columnHeadersDefaultCellStyle.WrapMode;
            }
            else
            {
                style.WrapModeInternal = defaultCellStyle.WrapMode;
            }
            if ((style2 != null) && (style2.Tag != null))
            {
                style.Tag = style2.Tag;
            }
            else if (columnHeadersDefaultCellStyle.Tag != null)
            {
                style.Tag = columnHeadersDefaultCellStyle.Tag;
            }
            else
            {
                style.Tag = defaultCellStyle.Tag;
            }
            if ((style2 != null) && (style2.Padding != Padding.Empty))
            {
                style.PaddingInternal = style2.Padding;
                return style;
            }
            if (columnHeadersDefaultCellStyle.Padding != Padding.Empty)
            {
                style.PaddingInternal = columnHeadersDefaultCellStyle.Padding;
                return style;
            }
            style.PaddingInternal = defaultCellStyle.Padding;
            return style;
        }

        /// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> that represents the preferred size, in pixels, of the cell.</returns>
        /// <param name="cellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the style of the cell.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to draw the cell.</param>
        /// <param name="constraintSize">The cell's maximum allowable size.</param>
        /// <param name="rowIndex">The zero-based row index of the cell.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is not -1.</exception>
        protected override Size GetPreferredSize(Graphics graphics, GridCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            Size size;
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
            GridFreeDimension freeDimensionFromConstraint = GridCell.GetFreeDimensionFromConstraint(constraintSize);
            GridAdvancedBorderStyle gridAdvancedBorderStylePlaceholder = new GridAdvancedBorderStyle();
            GridAdvancedBorderStyle advancedBorderStyle = base.Grid.AdjustColumnHeaderBorderStyle(base.Grid.AdvancedColumnHeadersBorderStyle, gridAdvancedBorderStylePlaceholder, false, false);
            Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
            int num = (rectangle.Left + rectangle.Width) + cellStyle.Padding.Horizontal;
            int num2 = (rectangle.Top + rectangle.Height) + cellStyle.Padding.Vertical;
            TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
            string str = this.GetValue(rowIndex) as string;
            switch (freeDimensionFromConstraint)
            {
                case GridFreeDimension.Height:
                {
                    Size empty;
                    int num3 = constraintSize.Width - num;
                    size = new Size(0, 0);
                    if (((num3 < 0x11) || (base.OwningColumn == null)) || (base.OwningColumn.SortMode == GridColumnSortMode.NotSortable))
                    {
                        empty = Size.Empty;
                    }
                    else
                    {
                        empty = new Size(0x11, 7);
                    }
                    if ((((num3 - 2) - 2) > 0) && !string.IsNullOrEmpty(str))
                    {
                        if (cellStyle.WrapMode == GridTriState.True)
                        {
                            if ((empty.Width > 0) && (((((num3 - 2) - 2) - 2) - empty.Width) > 0))
                            {
                                size = new Size(0, GridCell.MeasureTextHeight(graphics, str, cellStyle.Font, (((num3 - 2) - 2) - 2) - empty.Width, flags));
                            }
                            else
                            {
                                size = new Size(0, GridCell.MeasureTextHeight(graphics, str, cellStyle.Font, (num3 - 2) - 2, flags));
                            }
                        }
                        else
                        {
                            size = new Size(0, GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags).Height);
                        }
                    }
                    size.Height = Math.Max(size.Height, empty.Height);
                    size.Height = Math.Max(size.Height, 1);
                    goto Label_0391;
                }
                case GridFreeDimension.Width:
                    size = new Size(0, 0);
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (cellStyle.WrapMode != GridTriState.True)
                        {
                            size = new Size(GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags).Width, 0);
                            break;
                        }
                        size = new Size(GridCell.MeasureTextWidth(graphics, str, cellStyle.Font, Math.Max(1, (constraintSize.Height - num2) - 2), flags), 0);
                    }
                    break;

                default:
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (cellStyle.WrapMode == GridTriState.True)
                        {
                            size = GridCell.MeasureTextPreferredSize(graphics, str, cellStyle.Font, 5f, flags);
                        }
                        else
                        {
                            size = GridCell.MeasureTextSize(graphics, str, cellStyle.Font, flags);
                        }
                    }
                    else
                    {
                        size = new Size(0, 0);
                    }
                    if ((base.OwningColumn != null) && (base.OwningColumn.SortMode != GridColumnSortMode.NotSortable))
                    {
                        size.Width += 0x11;
                        if (!string.IsNullOrEmpty(str))
                        {
                            size.Width += 2;
                        }
                        size.Height = Math.Max(size.Height, 7);
                    }
                    size.Width = Math.Max(size.Width, 1);
                    size.Height = Math.Max(size.Height, 1);
                    goto Label_0391;
            }
            if (((((constraintSize.Height - num2) - 2) > 7) && (base.OwningColumn != null)) && (base.OwningColumn.SortMode != GridColumnSortMode.NotSortable))
            {
                size.Width += 0x11;
                if (!string.IsNullOrEmpty(str))
                {
                    size.Width += 2;
                }
            }
            size.Width = Math.Max(size.Width, 1);
        Label_0391:
            if (freeDimensionFromConstraint != GridFreeDimension.Height)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    size.Width += 4;
                }
                size.Width += num;
            }
            if (freeDimensionFromConstraint != GridFreeDimension.Width)
            {
                size.Height += 2 + num2;
            }
            if (base.Grid.ApplyVisualStylesToHeaderCells)
            {
                Rectangle themeMargins = GridHeaderCell.GetThemeMargins(graphics);
                if (freeDimensionFromConstraint != GridFreeDimension.Height)
                {
                    size.Width += themeMargins.X + themeMargins.Width;
                }
                if (freeDimensionFromConstraint != GridFreeDimension.Width)
                {
                    size.Height += themeMargins.Y + themeMargins.Height;
                }
            }
            return size;
        }

        /// <summary>Gets the value of the cell. </summary>
        /// <returns>The value contained in the <see cref="T:MControl.GridView.GridColumnHeaderCell"></see>.</returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is not -1.</exception>
        protected override object GetValue(int rowIndex)
        {
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if (this.ContainsLocalValue)
            {
                return base.Properties.GetObject(GridCell.PropCellValue);
            }
            if (base.OwningColumn != null)
            {
                return base.OwningColumn.Name;
            }
            return null;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates gridElementState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, gridElementState, formattedValue, cellStyle, advancedBorderStyle, paintParts, true);
        }

        private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates gridElementState, object formattedValue, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts, bool paint)
        {
            Pen pen;
            int num5;
            Rectangle empty = Rectangle.Empty;
            if (paint && GridCell.PaintBorder(paintParts))
            {
                this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            Rectangle bounds = cellBounds;
            Rectangle rectangle3 = this.BorderWidths(advancedBorderStyle);
            bounds.Offset(rectangle3.X, rectangle3.Y);
            bounds.Width -= rectangle3.Right;
            bounds.Height -= rectangle3.Bottom;
            Rectangle destRect = bounds;
            bool flag = (gridElementState & GridElementStates.Selected) != GridElementStates.None;
            if (base.Grid.ApplyVisualStylesToHeaderCells)
            {
                if ((cellStyle.Padding != Padding.Empty) && (cellStyle.Padding != Padding.Empty))
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
                if ((paint && GridCell.PaintBackground(paintParts)) && ((destRect.Width > 0) && (destRect.Height > 0)))
                {
                    int headerState = 1;
                    if (((base.OwningColumn != null) && (base.OwningColumn.SortMode != GridColumnSortMode.NotSortable)) || ((base.Grid.SelectionMode == GridSelectionMode.FullColumnSelect) || (base.Grid.SelectionMode == GridSelectionMode.ColumnHeaderSelect)))
                    {
                        if (base.ButtonState != ButtonState.Normal)
                        {
                            headerState = 3;
                        }
                        else if ((base.Grid.MouseEnteredCellAddress.Y == rowIndex) && (base.Grid.MouseEnteredCellAddress.X == base.ColumnIndex))
                        {
                            headerState = 2;
                        }
                        else if (flag)
                        {
                            headerState = 3;
                        }
                    }
                    if (base.Grid.RightToLeftInternal)
                    {
                        Bitmap flipXPThemesBitmap = base.FlipXPThemesBitmap;
                        if (((flipXPThemesBitmap == null) || (flipXPThemesBitmap.Width < destRect.Width)) || (((flipXPThemesBitmap.Width > (2 * destRect.Width)) || (flipXPThemesBitmap.Height < destRect.Height)) || (flipXPThemesBitmap.Height > (2 * destRect.Height))))
                        {
                            flipXPThemesBitmap = base.FlipXPThemesBitmap = new Bitmap(destRect.Width, destRect.Height);
                        }
                        GridColumnHeaderCellRenderer.DrawHeader(Graphics.FromImage(flipXPThemesBitmap), new Rectangle(0, 0, destRect.Width, destRect.Height), headerState);
                        flipXPThemesBitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        g.DrawImage(flipXPThemesBitmap, destRect, new Rectangle(flipXPThemesBitmap.Width - destRect.Width, 0, destRect.Width, destRect.Height), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        GridColumnHeaderCellRenderer.DrawHeader(g, destRect, headerState);
                    }
                }
                Rectangle themeMargins = GridHeaderCell.GetThemeMargins(g);
                bounds.Y += themeMargins.Y;
                bounds.Height -= themeMargins.Y + themeMargins.Height;
                if (base.Grid.RightToLeftInternal)
                {
                    bounds.X += themeMargins.Width;
                    bounds.Width -= themeMargins.X + themeMargins.Width;
                }
                else
                {
                    bounds.X += themeMargins.X;
                    bounds.Width -= themeMargins.X + themeMargins.Width;
                }
            }
            else
            {
                if ((paint && GridCell.PaintBackground(paintParts)) && ((destRect.Width > 0) && (destRect.Height > 0)))
                {
                    SolidBrush cachedBrush = base.Grid.GetCachedBrush((GridCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
                    if (cachedBrush.Color.A == 0xff)
                    {
                        g.FillRectangle(cachedBrush, destRect);
                    }
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
            }
            bool flag2 = false;
            Point point = new Point(0, 0);
            string str = formattedValue as string;
            bounds.Y++;
            bounds.Height -= 2;
            if (((((bounds.Width - 2) - 2) > 0) && (bounds.Height > 0)) && !string.IsNullOrEmpty(str))
            {
                Color color;
                bounds.Offset(2, 0);
                bounds.Width -= 4;
                if (base.Grid.ApplyVisualStylesToHeaderCells)
                {
                    color = GridColumnHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
                }
                else
                {
                    color = flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor;
                }
                if ((base.OwningColumn != null) && (base.OwningColumn.SortMode != GridColumnSortMode.NotSortable))
                {
                    bool flag3;
                    int maxWidth = ((bounds.Width - 2) - 9) - 8;
                    if (((maxWidth > 0) && (GridCell.GetPreferredTextHeight(g, base.Grid.RightToLeftInternal, str, cellStyle, maxWidth, out flag3) <= bounds.Height)) && !flag3)
                    {
                        flag2 = this.SortGlyphDirection != SortOrder.None;
                        bounds.Width -= 0x13;
                        if (base.Grid.RightToLeftInternal)
                        {
                            bounds.X += 0x13;
                            point = new Point((((bounds.Left - 2) - 2) - 4) - 9, bounds.Top + ((bounds.Height - 7) / 2));
                        }
                        else
                        {
                            point = new Point(((bounds.Right + 2) + 2) + 4, bounds.Top + ((bounds.Height - 7) / 2));
                        }
                    }
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
                        TextRenderer.DrawText(g, str, cellStyle.Font, bounds, color, flags);
                    }
                }
                else
                {
                    empty = GridUtilities.GetTextBounds(bounds, str, flags, cellStyle);
                }
            }
            else if ((paint && (this.SortGlyphDirection != SortOrder.None)) && ((bounds.Width >= 0x11) && (bounds.Height >= 7)))
            {
                flag2 = true;
                point = new Point(bounds.Left + ((bounds.Width - 9) / 2), bounds.Top + ((bounds.Height - 7) / 2));
            }
            if ((paint && flag2) && GridCell.PaintContentBackground(paintParts))
            {
                pen = null;
                Pen lightPen = null;
                base.GetContrastedPens(cellStyle.BackColor, ref pen, ref lightPen);
                if (this.SortGlyphDirection != SortOrder.Ascending)
                {
                    switch (advancedBorderStyle.Right)
                    {
                        case GridAdvancedCellBorderStyle.Inset:
                            g.DrawLine(lightPen, point.X, point.Y + 1, (point.X + 4) - 1, (point.Y + 7) - 1);
                            g.DrawLine(lightPen, (int) (point.X + 1), (int) (point.Y + 1), (int) ((point.X + 4) - 1), (int) ((point.Y + 7) - 1));
                            g.DrawLine(pen, (int) (point.X + 4), (int) ((point.Y + 7) - 1), (int) ((point.X + 9) - 2), (int) (point.Y + 1));
                            g.DrawLine(pen, (int) (point.X + 4), (int) ((point.Y + 7) - 1), (int) ((point.X + 9) - 3), (int) (point.Y + 1));
                            g.DrawLine(pen, point.X, point.Y, (point.X + 9) - 2, point.Y);
                            return empty;

                        case GridAdvancedCellBorderStyle.InsetDouble:
                            goto Label_0BD2;

                        case GridAdvancedCellBorderStyle.Outset:
                        case GridAdvancedCellBorderStyle.OutsetDouble:
                        case GridAdvancedCellBorderStyle.OutsetPartial:
                            g.DrawLine(pen, point.X, point.Y + 1, (point.X + 4) - 1, (point.Y + 7) - 1);
                            g.DrawLine(pen, (int) (point.X + 1), (int) (point.Y + 1), (int) ((point.X + 4) - 1), (int) ((point.Y + 7) - 1));
                            g.DrawLine(lightPen, (int) (point.X + 4), (int) ((point.Y + 7) - 1), (int) ((point.X + 9) - 2), (int) (point.Y + 1));
                            g.DrawLine(lightPen, (int) (point.X + 4), (int) ((point.Y + 7) - 1), (int) ((point.X + 9) - 3), (int) (point.Y + 1));
                            g.DrawLine(lightPen, point.X, point.Y, (point.X + 9) - 2, point.Y);
                            return empty;
                    }
                    goto Label_0BD2;
                }
                switch (advancedBorderStyle.Right)
                {
                    case GridAdvancedCellBorderStyle.Inset:
                        g.DrawLine(lightPen, point.X, (point.Y + 7) - 2, (point.X + 4) - 1, point.Y);
                        g.DrawLine(lightPen, point.X + 1, (point.Y + 7) - 2, (point.X + 4) - 1, point.Y);
                        g.DrawLine(pen, point.X + 4, point.Y, (point.X + 9) - 2, (point.Y + 7) - 2);
                        g.DrawLine(pen, point.X + 4, point.Y, (point.X + 9) - 3, (point.Y + 7) - 2);
                        g.DrawLine(pen, point.X, (point.Y + 7) - 1, (point.X + 9) - 2, (point.Y + 7) - 1);
                        return empty;

                    case GridAdvancedCellBorderStyle.Outset:
                    case GridAdvancedCellBorderStyle.OutsetDouble:
                    case GridAdvancedCellBorderStyle.OutsetPartial:
                        g.DrawLine(pen, point.X, (point.Y + 7) - 2, (point.X + 4) - 1, point.Y);
                        g.DrawLine(pen, point.X + 1, (point.Y + 7) - 2, (point.X + 4) - 1, point.Y);
                        g.DrawLine(lightPen, point.X + 4, point.Y, (point.X + 9) - 2, (point.Y + 7) - 2);
                        g.DrawLine(lightPen, point.X + 4, point.Y, (point.X + 9) - 3, (point.Y + 7) - 2);
                        g.DrawLine(lightPen, point.X, (point.Y + 7) - 1, (point.X + 9) - 2, (point.Y + 7) - 1);
                        return empty;
                }
                for (int i = 0; i < 4; i++)
                {
                    g.DrawLine(pen, (int) (point.X + i), (int) (((point.Y + 7) - i) - 1), (int) (((point.X + 9) - i) - 1), (int) (((point.Y + 7) - i) - 1));
                }
                g.DrawLine(pen, (int) (point.X + 4), (int) (((point.Y + 7) - 4) - 1), (int) (point.X + 4), (int) ((point.Y + 7) - 4));
            }
            return empty;
        Label_0BD2:
            num5 = 0;
            while (num5 < 4)
            {
                g.DrawLine(pen, (int) (point.X + num5), (int) ((point.Y + num5) + 2), (int) (((point.X + 9) - num5) - 1), (int) ((point.Y + num5) + 2));
                num5++;
            }
            g.DrawLine(pen, (int) (point.X + 4), (int) ((point.Y + 4) + 1), (int) (point.X + 4), (int) ((point.Y + 4) + 2));
            return empty;
        }

        /// <summary>Sets the value of the cell. </summary>
        /// <returns>true if the value has been set; otherwise false.</returns>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        /// <param name="value">The cell value to set. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is not -1.</exception>
        protected override bool SetValue(int rowIndex, object value)
        {
            if (rowIndex != -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            object obj2 = this.GetValue(rowIndex);
            base.Properties.SetObject(GridCell.PropCellValue, value);
            if ((base.Grid != null) && (obj2 != value))
            {
                base.RaiseCellValueChanged(new GridCellEventArgs(base.ColumnIndex, -1));
            }
            return true;
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridColumnHeaderCell { ColumnIndex=" + base.ColumnIndex.ToString(CultureInfo.CurrentCulture) + " }");
        }

        internal bool ContainsLocalValue
        {
            get
            {
                return base.Properties.ContainsObject(GridCell.PropCellValue);
            }
        }

        /// <summary>Gets or sets a value indicating which sort glyph is displayed.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.SortOrder"></see> value representing the current glyph. The default is <see cref="F:System.Windows.Forms.SortOrder.None"></see>. </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.SortOrder"></see> value.</exception>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the value of either the <see cref="P:MControl.GridView.GridCell.OwningColumn"></see> property or the <see cref="P:MControl.GridView.GridElement.Grid"></see> property of the cell is null.-or-When changing the value of this property, the specified value is not <see cref="F:System.Windows.Forms.SortOrder.None"></see> and the value of the <see cref="P:MControl.GridView.GridColumn.SortMode"></see> property of the owning column is <see cref="F:MControl.GridView.GridColumnSortMode.NotSortable"></see>.</exception>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SortOrder SortGlyphDirection
        {
            get
            {
                return this.sortGlyphDirection;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 2))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(SortOrder));
                }
                if ((base.OwningColumn == null) || (base.Grid == null))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_CellDoesNotYetBelongToGrid"));
                }
                if (value != this.sortGlyphDirection)
                {
                    if ((base.OwningColumn.SortMode == GridColumnSortMode.NotSortable) && (value != SortOrder.None))
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumnHeaderCell_SortModeAndSortGlyphDirectionClash", new object[] { value.ToString() }));
                    }
                    this.sortGlyphDirection = value;
                    base.Grid.OnSortGlyphDirectionChanged(this);
                }
            }
        }

        internal SortOrder SortGlyphDirectionInternal
        {
            set
            {
                this.sortGlyphDirection = value;
            }
        }

        /// <summary>Provides information about a <see cref="T:MControl.GridView.GridColumnHeaderCell"></see> to accessibility client applications.</summary>
        protected class GridColumnHeaderCellAccessibleObject : GridCell.GridCellAccessibleObject
        {
            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see> class.</summary>
            /// <param name="owner">The <see cref="T:MControl.GridView.GridColumnHeaderCell"></see> that owns the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see>.</param>
            public GridColumnHeaderCellAccessibleObject(GridColumnHeaderCell owner) : base(owner)
            {
            }

            /// <summary>Performs the default action associated with the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see>.</summary>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void DoDefaultAction()
            {
                GridColumnHeaderCell owner = (GridColumnHeaderCell) base.Owner;
                Grid grid = owner.Grid;
                if (owner.OwningColumn != null)
                {
                    if (owner.OwningColumn.SortMode == GridColumnSortMode.Automatic)
                    {
                        ListSortDirection ascending = ListSortDirection.Ascending;
                        if ((grid.SortedColumn == owner.OwningColumn) && (grid.SortOrder == SortOrder.Ascending))
                        {
                            ascending = ListSortDirection.Descending;
                        }
                        grid.Sort(owner.OwningColumn, ascending);
                    }
                    else if ((grid.SelectionMode == GridSelectionMode.FullColumnSelect) || (grid.SelectionMode == GridSelectionMode.ColumnHeaderSelect))
                    {
                        owner.OwningColumn.Selected = true;
                    }
                }
            }

            /// <summary>Navigates to another accessible object.</summary>
            /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject"></see> that represents an object in the specified direction.</returns>
            /// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation"></see> values.</param>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
            {
                if (base.Owner.OwningColumn != null)
                {
                    switch (navigationDirection)
                    {
                        case AccessibleNavigation.Left:
                            if (base.Owner.Grid.RightToLeft != RightToLeft.No)
                            {
                                return this.NavigateForward();
                            }
                            return this.NavigateBackward();

                        case AccessibleNavigation.Right:
                            if (base.Owner.Grid.RightToLeft != RightToLeft.No)
                            {
                                return this.NavigateBackward();
                            }
                            return this.NavigateForward();

                        case AccessibleNavigation.Next:
                            return this.NavigateForward();

                        case AccessibleNavigation.Previous:
                            return this.NavigateBackward();

                        case AccessibleNavigation.FirstChild:
                            return base.Owner.Grid.AccessibilityObject.GetChild(0).GetChild(0);

                        case AccessibleNavigation.LastChild:
                        {
                            AccessibleObject child = base.Owner.Grid.AccessibilityObject.GetChild(0);
                            return child.GetChild(child.GetChildCount() - 1);
                        }
                    }
                }
                return null;
            }

            private AccessibleObject NavigateBackward()
            {
                if (base.Owner.OwningColumn == base.Owner.Grid.Columns.GetFirstColumn(GridElementStates.Visible))
                {
                    if (base.Owner.Grid.RowHeadersVisible)
                    {
                        return this.Parent.GetChild(0);
                    }
                    return null;
                }
                int index = base.Owner.Grid.Columns.GetPreviousColumn(base.Owner.OwningColumn, GridElementStates.Visible, GridElementStates.None).Index;
                int num2 = base.Owner.Grid.Columns.ColumnIndexToActualDisplayIndex(index, GridElementStates.Visible);
                if (base.Owner.Grid.RowHeadersVisible)
                {
                    return this.Parent.GetChild(num2 + 1);
                }
                return this.Parent.GetChild(num2);
            }

            private AccessibleObject NavigateForward()
            {
                if (base.Owner.OwningColumn == base.Owner.Grid.Columns.GetLastColumn(GridElementStates.Visible, GridElementStates.None))
                {
                    return null;
                }
                int index = base.Owner.Grid.Columns.GetNextColumn(base.Owner.OwningColumn, GridElementStates.Visible, GridElementStates.None).Index;
                int num2 = base.Owner.Grid.Columns.ColumnIndexToActualDisplayIndex(index, GridElementStates.Visible);
                if (base.Owner.Grid.RowHeadersVisible)
                {
                    return this.Parent.GetChild(num2 + 1);
                }
                return this.Parent.GetChild(num2);
            }

            /// <summary>Modifies the column selection depending on the selection mode.</summary>
            /// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection"></see> values. </param>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void Select(AccessibleSelection flags)
            {
                if (base.Owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                }
                GridColumnHeaderCell owner = (GridColumnHeaderCell) base.Owner;
                Grid grid = owner.Grid;
                if (grid != null)
                {
                    if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
                    {
                        grid.FocusInternal();
                    }
                    if ((owner.OwningColumn != null) && ((grid.SelectionMode == GridSelectionMode.FullColumnSelect) || (grid.SelectionMode == GridSelectionMode.ColumnHeaderSelect)))
                    {
                        if ((flags & (AccessibleSelection.AddSelection | AccessibleSelection.TakeSelection)) != AccessibleSelection.None)
                        {
                            owner.OwningColumn.Selected = true;
                        }
                        else if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection)
                        {
                            owner.OwningColumn.Selected = false;
                        }
                    }
                }
            }

            public override Rectangle Bounds
            {
                get
                {
                    return base.GetAccessibleObjectBounds(this.ParentPrivate);
                }
            }

            /// <summary>Gets a string that describes the default action of the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>A string that describes the default action of the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see></returns>
            public override string DefaultAction
            {
                get
                {
                    if (base.Owner.OwningColumn == null)
                    {
                        return string.Empty;
                    }
                    if (base.Owner.OwningColumn.SortMode == GridColumnSortMode.Automatic)
                    {
                        return MControl.GridView.RM.GetString("Grid_AccColumnHeaderCellDefaultAction");
                    }
                    if ((base.Owner.Grid.SelectionMode != GridSelectionMode.FullColumnSelect) && (base.Owner.Grid.SelectionMode != GridSelectionMode.ColumnHeaderSelect))
                    {
                        return string.Empty;
                    }
                    return MControl.GridView.RM.GetString("Grid_AccColumnHeaderCellSelectDefaultAction");
                }
            }

            /// <summary>Gets the name of the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>The name of the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see>.</returns>
            public override string Name
            {
                get
                {
                    if (base.Owner.OwningColumn != null)
                    {
                        return base.Owner.OwningColumn.HeaderText;
                    }
                    return string.Empty;
                }
            }

            public override AccessibleObject Parent
            {
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    return this.ParentPrivate;
                }
            }

            private AccessibleObject ParentPrivate
            {
                get
                {
                    return base.Owner.Grid.AccessibilityObject.GetChild(0);
                }
            }

            /// <summary>Gets the role of the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.RowHeader"></see> value.</returns>
            public override AccessibleRole Role
            {
                get
                {
                    return AccessibleRole.ColumnHeader;
                }
            }

            /// <summary>Gets the state of the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates"></see> values. The default is <see cref="F:System.Windows.Forms.AccessibleStates.Selectable"></see>.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            public override AccessibleStates State
            {
                get
                {
                    AccessibleStates selectable = AccessibleStates.Selectable;
                    if ((base.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen)
                    {
                        selectable |= AccessibleStates.Offscreen;
                    }
                    if (((base.Owner.Grid.SelectionMode == GridSelectionMode.FullColumnSelect) || (base.Owner.Grid.SelectionMode == GridSelectionMode.ColumnHeaderSelect)) && ((base.Owner.OwningColumn != null) && base.Owner.OwningColumn.Selected))
                    {
                        selectable |= AccessibleStates.Selected;
                    }
                    return selectable;
                }
            }

            /// <summary>Gets the value of the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>The value of the <see cref="T:MControl.GridView.GridColumnHeaderCell.GridColumnHeaderCellAccessibleObject"></see>.</returns>
            public override string Value
            {
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    return this.Name;
                }
            }
        }

        private class GridColumnHeaderCellRenderer
        {
            private static System.Windows.Forms.VisualStyles.VisualStyleRenderer visualStyleRenderer;

            private GridColumnHeaderCellRenderer()
            {
            }

            public static void DrawHeader(Graphics g, Rectangle bounds, int headerState)
            {
                Rectangle rect = Rectangle.Truncate(g.ClipBounds);
                if (2 == headerState)
                {
                    VisualStyleRenderer.SetParameters(GridColumnHeaderCell.HeaderElement);
                    Rectangle clipRectangle = new Rectangle(bounds.Left, bounds.Bottom - 2, 2, 2);
                    clipRectangle.Intersect(rect);
                    VisualStyleRenderer.DrawBackground(g, bounds, clipRectangle);
                    clipRectangle = new Rectangle(bounds.Right - 2, bounds.Bottom - 2, 2, 2);
                    clipRectangle.Intersect(rect);
                    VisualStyleRenderer.DrawBackground(g, bounds, clipRectangle);
                }
                VisualStyleRenderer.SetParameters(GridColumnHeaderCell.HeaderElement.ClassName, GridColumnHeaderCell.HeaderElement.Part, headerState);
                VisualStyleRenderer.DrawBackground(g, bounds, rect);
            }

            public static System.Windows.Forms.VisualStyles.VisualStyleRenderer VisualStyleRenderer
            {
                get
                {
                    if (visualStyleRenderer == null)
                    {
                        visualStyleRenderer = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(GridColumnHeaderCell.HeaderElement);
                    }
                    return visualStyleRenderer;
                }
            }
        }
    }
}

