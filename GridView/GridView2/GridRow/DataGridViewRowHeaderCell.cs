namespace MControl.GridView
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Security.Permissions;
    using System.Text;
    using System.Windows.Forms.VisualStyles;
    using System.Windows.Forms;

    /// <summary>Represents a row header of a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridRowHeaderCell : GridHeaderCell
    {
        private static System.Type cellType = typeof(GridRowHeaderCell);
        private static ColorMap[] colorMap = new ColorMap[] { new ColorMap() };
        private const byte GRIDROWHEADERCELL_contentMarginHeight = 3;
        private const byte GRIDROWHEADERCELL_contentMarginWidth = 3;
        private const byte GRIDROWHEADERCELL_horizontalTextMarginLeft = 1;
        private const byte GRIDROWHEADERCELL_horizontalTextMarginRight = 2;
        private const byte GRIDROWHEADERCELL_iconMarginHeight = 2;
        private const byte GRIDROWHEADERCELL_iconMarginWidth = 3;
        private const byte GRIDROWHEADERCELL_iconsHeight = 11;
        private const byte GRIDROWHEADERCELL_iconsWidth = 12;
        private const byte GRIDROWHEADERCELL_verticalTextMargin = 1;
        private static readonly VisualStyleElement HeaderElement = VisualStyleElement.Header.Item.Normal;
        private static Bitmap leftArrowBmp = null;
        private static Bitmap leftArrowStarBmp;
        private static Bitmap pencilLTRBmp = null;
        private static Bitmap pencilRTLBmp = null;
        private static Bitmap rightArrowBmp = null;
        private static Bitmap rightArrowStarBmp;
        private static Bitmap starBmp = null;

        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridRowHeaderCell cell;
            System.Type type = base.GetType();
            if (type == cellType)
            {
                cell = new GridRowHeaderCell();
            }
            else
            {
                cell = (GridRowHeaderCell) Activator.CreateInstance(type);
            }
            base.CloneInternal(cell);
            cell.Value = base.Value;
            return cell;
        }

        /// <summary>Creates a new accessible object for the <see cref="T:MControl.GridView.GridRowHeaderCell"></see>. </summary>
        /// <returns>A new <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see> for the <see cref="T:MControl.GridView.GridRowHeaderCell"></see>. </returns>
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new GridRowHeaderCellAccessibleObject(this);
        }

        private static Bitmap GetArrowBitmap(bool rightToLeft)
        {
            if (!rightToLeft)
            {
                return RightArrowBitmap;
            }
            return LeftArrowBitmap;
        }

        private static Bitmap GetArrowStarBitmap(bool rightToLeft)
        {
            if (!rightToLeft)
            {
                return RightArrowStarBitmap;
            }
            return LeftArrowStarBitmap;
        }

        private static Bitmap GetBitmap(string bitmapName)
        {
            Bitmap bitmap = new Bitmap(typeof(GridRowHeaderCell), bitmapName);
            bitmap.MakeTransparent();
            return bitmap;
        }

        /// <summary>Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard"></see>.</summary>
        /// <returns>A <see cref="T:System.Object"></see> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard"></see>.</returns>
        /// <param name="inLastRow">true to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="inFirstRow">true to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="lastCell">true to indicate that the cell is the last column of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="format">The current format string of the cell.</param>
        /// <param name="firstCell">true to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="rowIndex">The zero-based index of the row containing the cell.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than zero or greater than or equal to the number of rows in the control.</exception>
        protected override object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
        {
            if (base.Grid == null)
            {
                return null;
            }
            if ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            object obj2 = this.GetValue(rowIndex);
            StringBuilder sb = new StringBuilder(0x40);
            if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
            {
                if (inFirstRow)
                {
                    sb.Append("<TABLE>");
                }
                sb.Append("<TR>");
                sb.Append("<TD ALIGN=\"center\">");
                if (obj2 != null)
                {
                    sb.Append("<B>");
                    GridCell.FormatPlainTextAsHtml(obj2.ToString(), new StringWriter(sb, CultureInfo.CurrentCulture));
                    sb.Append("</B>");
                }
                else
                {
                    sb.Append("&nbsp;");
                }
                sb.Append("</TD>");
                if (lastCell)
                {
                    sb.Append("</TR>");
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

        protected override Rectangle GetContentBounds(Graphics graphics, GridCellStyle cellStyle, int rowIndex)
        {
            GridAdvancedBorderStyle style;
            GridElementStates states;
            Rectangle rectangle;
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if ((base.Grid == null) || (base.OwningRow == null))
            {
                return Rectangle.Empty;
            }
            object formattedValue = this.GetValue(rowIndex);
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
            if (((base.Grid == null) || (rowIndex < 0)) || (!base.Grid.ShowRowErrors || string.IsNullOrEmpty(this.GetErrorText(rowIndex))))
            {
                return Rectangle.Empty;
            }
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            object obj2 = this.GetValue(rowIndex);
            object formattedValue = this.GetFormattedValue(obj2, rowIndex, ref cellStyle, null, null, GridDataErrorContexts.Formatting);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, formattedValue, this.GetErrorText(rowIndex), cellStyle, style, GridPaintParts.ContentForeground, false, true, false);
        }

        protected internal override string GetErrorText(int rowIndex)
        {
            if (base.OwningRow == null)
            {
                return base.GetErrorText(rowIndex);
            }
            return base.OwningRow.GetErrorText(rowIndex);
        }

        /// <summary>Retrieves the inherited shortcut menu for the specified row.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> of the row if one exists; otherwise, the <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> inherited from <see cref="T:MControl.GridView.Grid"></see>.</returns>
        /// <param name="rowIndex">The index of the row to get the <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> of. The index must be -1 to indicate the row of column headers.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:MControl.GridView.GridElement.Grid"></see> property of the cell is not null and the specified rowIndex is less than 0 or greater than the number of rows in the control minus 1.</exception>
        public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
        {
            if ((base.Grid != null) && ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count)))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
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

        public override GridCellStyle GetInheritedStyle(GridCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
        {
            GridCellStyle style = (inheritedCellStyle == null) ? new GridCellStyle() : inheritedCellStyle;
            GridCellStyle style2 = null;
            if (base.HasStyle)
            {
                style2 = base.Style;
            }
            GridCellStyle rowHeadersDefaultCellStyle = base.Grid.RowHeadersDefaultCellStyle;
            GridCellStyle defaultCellStyle = base.Grid.DefaultCellStyle;
            if (includeColors)
            {
                if ((style2 != null) && !style2.BackColor.IsEmpty)
                {
                    style.BackColor = style2.BackColor;
                }
                else if (!rowHeadersDefaultCellStyle.BackColor.IsEmpty)
                {
                    style.BackColor = rowHeadersDefaultCellStyle.BackColor;
                }
                else
                {
                    style.BackColor = defaultCellStyle.BackColor;
                }
                if ((style2 != null) && !style2.ForeColor.IsEmpty)
                {
                    style.ForeColor = style2.ForeColor;
                }
                else if (!rowHeadersDefaultCellStyle.ForeColor.IsEmpty)
                {
                    style.ForeColor = rowHeadersDefaultCellStyle.ForeColor;
                }
                else
                {
                    style.ForeColor = defaultCellStyle.ForeColor;
                }
                if ((style2 != null) && !style2.SelectionBackColor.IsEmpty)
                {
                    style.SelectionBackColor = style2.SelectionBackColor;
                }
                else if (!rowHeadersDefaultCellStyle.SelectionBackColor.IsEmpty)
                {
                    style.SelectionBackColor = rowHeadersDefaultCellStyle.SelectionBackColor;
                }
                else
                {
                    style.SelectionBackColor = defaultCellStyle.SelectionBackColor;
                }
                if ((style2 != null) && !style2.SelectionForeColor.IsEmpty)
                {
                    style.SelectionForeColor = style2.SelectionForeColor;
                }
                else if (!rowHeadersDefaultCellStyle.SelectionForeColor.IsEmpty)
                {
                    style.SelectionForeColor = rowHeadersDefaultCellStyle.SelectionForeColor;
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
            else if (rowHeadersDefaultCellStyle.Font != null)
            {
                style.Font = rowHeadersDefaultCellStyle.Font;
            }
            else
            {
                style.Font = defaultCellStyle.Font;
            }
            if ((style2 != null) && !style2.IsNullValueDefault)
            {
                style.NullValue = style2.NullValue;
            }
            else if (!rowHeadersDefaultCellStyle.IsNullValueDefault)
            {
                style.NullValue = rowHeadersDefaultCellStyle.NullValue;
            }
            else
            {
                style.NullValue = defaultCellStyle.NullValue;
            }
            if ((style2 != null) && !style2.IsDataSourceNullValueDefault)
            {
                style.DataSourceNullValue = style2.DataSourceNullValue;
            }
            else if (!rowHeadersDefaultCellStyle.IsDataSourceNullValueDefault)
            {
                style.DataSourceNullValue = rowHeadersDefaultCellStyle.DataSourceNullValue;
            }
            else
            {
                style.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
            }
            if ((style2 != null) && (style2.Format.Length != 0))
            {
                style.Format = style2.Format;
            }
            else if (rowHeadersDefaultCellStyle.Format.Length != 0)
            {
                style.Format = rowHeadersDefaultCellStyle.Format;
            }
            else
            {
                style.Format = defaultCellStyle.Format;
            }
            if ((style2 != null) && !style2.IsFormatProviderDefault)
            {
                style.FormatProvider = style2.FormatProvider;
            }
            else if (!rowHeadersDefaultCellStyle.IsFormatProviderDefault)
            {
                style.FormatProvider = rowHeadersDefaultCellStyle.FormatProvider;
            }
            else
            {
                style.FormatProvider = defaultCellStyle.FormatProvider;
            }
            if ((style2 != null) && (style2.Alignment != GridContentAlignment.NotSet))
            {
                style.AlignmentInternal = style2.Alignment;
            }
            else if (rowHeadersDefaultCellStyle.Alignment != GridContentAlignment.NotSet)
            {
                style.AlignmentInternal = rowHeadersDefaultCellStyle.Alignment;
            }
            else
            {
                style.AlignmentInternal = defaultCellStyle.Alignment;
            }
            if ((style2 != null) && (style2.WrapMode != GridTriState.NotSet))
            {
                style.WrapModeInternal = style2.WrapMode;
            }
            else if (rowHeadersDefaultCellStyle.WrapMode != GridTriState.NotSet)
            {
                style.WrapModeInternal = rowHeadersDefaultCellStyle.WrapMode;
            }
            else
            {
                style.WrapModeInternal = defaultCellStyle.WrapMode;
            }
            if ((style2 != null) && (style2.Tag != null))
            {
                style.Tag = style2.Tag;
            }
            else if (rowHeadersDefaultCellStyle.Tag != null)
            {
                style.Tag = rowHeadersDefaultCellStyle.Tag;
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
            if (rowHeadersDefaultCellStyle.Padding != Padding.Empty)
            {
                style.PaddingInternal = rowHeadersDefaultCellStyle.Padding;
                return style;
            }
            style.PaddingInternal = defaultCellStyle.Padding;
            return style;
        }

        private static Bitmap GetPencilBitmap(bool rightToLeft)
        {
            if (!rightToLeft)
            {
                return PencilLTRBitmap;
            }
            return PencilRTLBitmap;
        }

        protected override Size GetPreferredSize(Graphics graphics, GridCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (base.Grid == null)
            {
                return new Size(-1, -1);
            }
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            GridAdvancedBorderStyle gridAdvancedBorderStylePlaceholder = new GridAdvancedBorderStyle();
            GridAdvancedBorderStyle advancedBorderStyle = base.OwningRow.AdjustRowHeaderBorderStyle(base.Grid.AdvancedRowHeadersBorderStyle, gridAdvancedBorderStylePlaceholder, false, false, false, false);
            Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
            int borderAndPaddingWidths = (rectangle.Left + rectangle.Width) + cellStyle.Padding.Horizontal;
            int borderAndPaddingHeights = (rectangle.Top + rectangle.Height) + cellStyle.Padding.Vertical;
            TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
            if (base.Grid.ApplyVisualStylesToHeaderCells)
            {
                Rectangle themeMargins = GridHeaderCell.GetThemeMargins(graphics);
                borderAndPaddingWidths += themeMargins.Y;
                borderAndPaddingWidths += themeMargins.Height;
                borderAndPaddingHeights += themeMargins.X;
                borderAndPaddingHeights += themeMargins.Width;
            }
            object obj2 = this.GetValue(rowIndex);
            if (!(obj2 is string))
            {
                obj2 = null;
            }
            return GridUtilities.GetPreferredRowHeaderSize(graphics, (string) obj2, cellStyle, borderAndPaddingWidths, borderAndPaddingHeights, base.Grid.ShowRowErrors, true, constraintSize, flags);
        }

        /// <summary>Gets the value of the cell. </summary>
        /// <returns>The value contained in the <see cref="T:MControl.GridView.GridRowHeaderCell"></see>.</returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:MControl.GridView.GridElement.Grid"></see> property of the cell is not null and rowIndex is less than -1 or greater than or equal to the number of rows in the parent <see cref="T:MControl.GridView.Grid"></see>.</exception>
        protected override object GetValue(int rowIndex)
        {
            if ((base.Grid != null) && ((rowIndex < -1) || (rowIndex >= base.Grid.Rows.Count)))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            return base.Properties.GetObject(GridCell.PropCellValue);
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
        }

        private void PaintIcon(Graphics g, Bitmap bmp, Rectangle bounds, Color foreColor)
        {
            Rectangle destRect = new Rectangle(base.Grid.RightToLeftInternal ? ((bounds.Right - 3) - 12) : (bounds.Left + 3), bounds.Y + ((bounds.Height - 11) / 2), 12, 11);
            colorMap[0].NewColor = foreColor;
            colorMap[0].OldColor = Color.Black;
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetRemapTable(colorMap, ColorAdjustType.Bitmap);
            g.DrawImage(bmp, destRect, 0, 0, 12, 11, GraphicsUnit.Pixel, imageAttr);
            imageAttr.Dispose();
        }

        private Rectangle PaintPrivate(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates gridElementState, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
        {
            Rectangle empty = Rectangle.Empty;
            if (paint && GridCell.PaintBorder(paintParts))
            {
                this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            Rectangle rect = cellBounds;
            Rectangle rectangle3 = this.BorderWidths(advancedBorderStyle);
            rect.Offset(rectangle3.X, rectangle3.Y);
            rect.Width -= rectangle3.Right;
            rect.Height -= rectangle3.Bottom;
            Rectangle destRect = rect;
            bool flag = (gridElementState & GridElementStates.Selected) != GridElementStates.None;
            if (base.Grid.ApplyVisualStylesToHeaderCells)
            {
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
                if ((destRect.Width > 0) && (destRect.Height > 0))
                {
                    if (paint && GridCell.PaintBackground(paintParts))
                    {
                        int headerState = 1;
                        if ((base.Grid.SelectionMode == GridSelectionMode.FullRowSelect) || (base.Grid.SelectionMode == GridSelectionMode.RowHeaderSelect))
                        {
                            if (base.ButtonState != ButtonState.Normal)
                            {
                                headerState = 3;
                            }
                            else if ((base.Grid.MouseEnteredCellAddress.Y == rowIndex) && (base.Grid.MouseEnteredCellAddress.X == -1))
                            {
                                headerState = 2;
                            }
                            else if (flag)
                            {
                                headerState = 3;
                            }
                        }
                        using (Bitmap bitmap = new Bitmap(destRect.Height, destRect.Width))
                        {
                            using (Graphics graphics2 = Graphics.FromImage(bitmap))
                            {
                                GridRowHeaderCellRenderer.DrawHeader(graphics2, new Rectangle(0, 0, destRect.Height, destRect.Width), headerState);
                                bitmap.RotateFlip(base.Grid.RightToLeftInternal ? RotateFlipType.Rotate90FlipNone : RotateFlipType.Rotate90FlipX);
                                graphics.DrawImage(bitmap, destRect, new Rectangle(0, 0, destRect.Width, destRect.Height), GraphicsUnit.Pixel);
                            }
                        }
                    }
                    Rectangle themeMargins = GridHeaderCell.GetThemeMargins(graphics);
                    if (base.Grid.RightToLeftInternal)
                    {
                        rect.X += themeMargins.Height;
                    }
                    else
                    {
                        rect.X += themeMargins.Y;
                    }
                    rect.Width -= themeMargins.Y + themeMargins.Height;
                    rect.Height -= themeMargins.X + themeMargins.Width;
                    rect.Y += themeMargins.X;
                }
            }
            else
            {
                if ((rect.Width > 0) && (rect.Height > 0))
                {
                    SolidBrush cachedBrush = base.Grid.GetCachedBrush((GridCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
                    if ((paint && GridCell.PaintBackground(paintParts)) && (cachedBrush.Color.A == 0xff))
                    {
                        graphics.FillRectangle(cachedBrush, rect);
                    }
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
            }
            Bitmap bmp = null;
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Rectangle cellValueBounds = rect;
                string str = formattedValue as string;
                if (!string.IsNullOrEmpty(str))
                {
                    if ((rect.Width >= 0x12) && (rect.Height >= 15))
                    {
                        if (paint && GridCell.PaintContentBackground(paintParts))
                        {
                            if (base.Grid.CurrentCellAddress.Y == rowIndex)
                            {
                                if (base.Grid.VirtualMode)
                                {
                                    if (base.Grid.IsCurrentRowDirty && base.Grid.ShowEditingIcon)
                                    {
                                        bmp = GetPencilBitmap(base.Grid.RightToLeftInternal);
                                    }
                                    else if (base.Grid.NewRowIndex == rowIndex)
                                    {
                                        bmp = GetArrowStarBitmap(base.Grid.RightToLeftInternal);
                                    }
                                    else
                                    {
                                        bmp = GetArrowBitmap(base.Grid.RightToLeftInternal);
                                    }
                                }
                                else if (base.Grid.IsCurrentCellDirty && base.Grid.ShowEditingIcon)
                                {
                                    bmp = GetPencilBitmap(base.Grid.RightToLeftInternal);
                                }
                                else if (base.Grid.NewRowIndex == rowIndex)
                                {
                                    bmp = GetArrowStarBitmap(base.Grid.RightToLeftInternal);
                                }
                                else
                                {
                                    bmp = GetArrowBitmap(base.Grid.RightToLeftInternal);
                                }
                            }
                            else if (base.Grid.NewRowIndex == rowIndex)
                            {
                                bmp = StarBitmap;
                            }
                            if (bmp != null)
                            {
                                Color color;
                                if (base.Grid.ApplyVisualStylesToHeaderCells)
                                {
                                    color = GridRowHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
                                }
                                else
                                {
                                    color = flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor;
                                }
                                lock (bmp)
                                {
                                    this.PaintIcon(graphics, bmp, rect, color);
                                }
                            }
                        }
                        if (!base.Grid.RightToLeftInternal)
                        {
                            rect.X += 0x12;
                        }
                        rect.Width -= 0x12;
                    }
                    rect.Offset(4, 1);
                    rect.Width -= 9;
                    rect.Height -= 2;
                    if ((rect.Width > 0) && (rect.Height > 0))
                    {
                        TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.Grid.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
                        if (base.Grid.ShowRowErrors && (rect.Width > 0x12))
                        {
                            Size maxBounds = new Size((rect.Width - 12) - 6, rect.Height);
                            if (GridCell.TextFitsInBounds(graphics, str, cellStyle.Font, maxBounds, flags))
                            {
                                if (base.Grid.RightToLeftInternal)
                                {
                                    rect.X += 0x12;
                                }
                                rect.Width -= 0x12;
                            }
                        }
                        if (GridCell.PaintContentForeground(paintParts))
                        {
                            if (paint)
                            {
                                Color color2;
                                if (base.Grid.ApplyVisualStylesToHeaderCells)
                                {
                                    color2 = GridRowHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
                                }
                                else
                                {
                                    color2 = flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor;
                                }
                                if ((flags & TextFormatFlags.SingleLine) != TextFormatFlags.GlyphOverhangPadding)
                                {
                                    flags |= TextFormatFlags.EndEllipsis;
                                }
                                TextRenderer.DrawText(graphics, str, cellStyle.Font, rect, color2, flags);
                            }
                            else if (computeContentBounds)
                            {
                                empty = GridUtilities.GetTextBounds(rect, str, flags, cellStyle);
                            }
                        }
                    }
                    if (cellValueBounds.Width >= 0x21)
                    {
                        if ((paint && base.Grid.ShowRowErrors) && GridCell.PaintErrorIcon(paintParts))
                        {
                            this.PaintErrorIcon(graphics, clipBounds, cellValueBounds, errorText);
                            return empty;
                        }
                        if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
                        {
                            empty = base.ComputeErrorIconBounds(cellValueBounds);
                        }
                    }
                    return empty;
                }
                if (((rect.Width >= 0x12) && (rect.Height >= 15)) && (paint && GridCell.PaintContentBackground(paintParts)))
                {
                    if (base.Grid.CurrentCellAddress.Y == rowIndex)
                    {
                        if (base.Grid.VirtualMode)
                        {
                            if (base.Grid.IsCurrentRowDirty && base.Grid.ShowEditingIcon)
                            {
                                bmp = GetPencilBitmap(base.Grid.RightToLeftInternal);
                            }
                            else if (base.Grid.NewRowIndex == rowIndex)
                            {
                                bmp = GetArrowStarBitmap(base.Grid.RightToLeftInternal);
                            }
                            else
                            {
                                bmp = GetArrowBitmap(base.Grid.RightToLeftInternal);
                            }
                        }
                        else if (base.Grid.IsCurrentCellDirty && base.Grid.ShowEditingIcon)
                        {
                            bmp = GetPencilBitmap(base.Grid.RightToLeftInternal);
                        }
                        else if (base.Grid.NewRowIndex == rowIndex)
                        {
                            bmp = GetArrowStarBitmap(base.Grid.RightToLeftInternal);
                        }
                        else
                        {
                            bmp = GetArrowBitmap(base.Grid.RightToLeftInternal);
                        }
                    }
                    else if (base.Grid.NewRowIndex == rowIndex)
                    {
                        bmp = StarBitmap;
                    }
                    if (bmp != null)
                    {
                        lock (bmp)
                        {
                            Color color3;
                            if (base.Grid.ApplyVisualStylesToHeaderCells)
                            {
                                color3 = GridRowHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
                            }
                            else
                            {
                                color3 = flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor;
                            }
                            this.PaintIcon(graphics, bmp, rect, color3);
                        }
                    }
                }
                if (cellValueBounds.Width >= 0x21)
                {
                    if ((paint && base.Grid.ShowRowErrors) && GridCell.PaintErrorIcon(paintParts))
                    {
                        base.PaintErrorIcon(graphics, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
                        return empty;
                    }
                    if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
                    {
                        empty = base.ComputeErrorIconBounds(cellValueBounds);
                    }
                }
            }
            return empty;
        }

        protected override bool SetValue(int rowIndex, object value)
        {
            object obj2 = this.GetValue(rowIndex);
            if ((value != null) || base.Properties.ContainsObject(GridCell.PropCellValue))
            {
                base.Properties.SetObject(GridCell.PropCellValue, value);
            }
            if ((base.Grid != null) && (obj2 != value))
            {
                base.RaiseCellValueChanged(new GridCellEventArgs(-1, rowIndex));
            }
            return true;
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridRowHeaderCell { RowIndex=" + base.RowIndex.ToString(CultureInfo.CurrentCulture) + " }");
        }

        private static Bitmap LeftArrowBitmap
        {
            get
            {
                if (leftArrowBmp == null)
                {
                    leftArrowBmp = GetBitmap("GridRow.left.bmp");
                }
                return leftArrowBmp;
            }
        }

        private static Bitmap LeftArrowStarBitmap
        {
            get
            {
                if (leftArrowStarBmp == null)
                {
                    leftArrowStarBmp = GetBitmap("GridRow.leftstar.bmp");
                }
                return leftArrowStarBmp;
            }
        }

        private static Bitmap PencilLTRBitmap
        {
            get
            {
                if (pencilLTRBmp == null)
                {
                    pencilLTRBmp = GetBitmap("GridRow.pencil_ltr.bmp");
                }
                return pencilLTRBmp;
            }
        }

        private static Bitmap PencilRTLBitmap
        {
            get
            {
                if (pencilRTLBmp == null)
                {
                    pencilRTLBmp = GetBitmap("GridRow.pencil_rtl.bmp");
                }
                return pencilRTLBmp;
            }
        }

        private static Bitmap RightArrowBitmap
        {
            get
            {
                if (rightArrowBmp == null)
                {
                    rightArrowBmp = GetBitmap("GridRow.right.bmp");
                }
                return rightArrowBmp;
            }
        }

        private static Bitmap RightArrowStarBitmap
        {
            get
            {
                if (rightArrowStarBmp == null)
                {
                    rightArrowStarBmp = GetBitmap("GridRow.rightstar.bmp");
                }
                return rightArrowStarBmp;
            }
        }

        private static Bitmap StarBitmap
        {
            get
            {
                if (starBmp == null)
                {
                    starBmp = GetBitmap("GridRow.star.bmp");
                }
                return starBmp;
            }
        }

        /// <summary>Provides information about a <see cref="T:MControl.GridView.GridRowHeaderCell"></see> to accessibility client applications.</summary>
        protected class GridRowHeaderCellAccessibleObject : GridCell.GridCellAccessibleObject
        {
            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see> class. </summary>
            /// <param name="owner">The <see cref="T:MControl.GridView.GridRowHeaderCell"></see> that owns this accessible object.</param>
            public GridRowHeaderCellAccessibleObject(GridRowHeaderCell owner) : base(owner)
            {
            }

            /// <summary>Performs the default action of the <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see>.</summary>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void DoDefaultAction()
            {
                if (((base.Owner.Grid.SelectionMode == GridSelectionMode.FullRowSelect) || (base.Owner.Grid.SelectionMode == GridSelectionMode.RowHeaderSelect)) && (base.Owner.OwningRow != null))
                {
                    base.Owner.OwningRow.Selected = true;
                }
            }

            /// <summary>Navigates to another accessible object.</summary>
            /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject"></see> that represents an object in the specified direction.</returns>
            /// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation"></see> values.</param>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
            {
                switch (navigationDirection)
                {
                    case AccessibleNavigation.Up:
                        if (base.Owner.OwningRow != null)
                        {
                            if (base.Owner.OwningRow.Index == base.Owner.Grid.Rows.GetFirstRow(GridElementStates.Visible))
                            {
                                if (base.Owner.Grid.ColumnHeadersVisible)
                                {
                                    return base.Owner.Grid.AccessibilityObject.GetChild(0).GetChild(0);
                                }
                                return null;
                            }
                            int previousRow = base.Owner.Grid.Rows.GetPreviousRow(base.Owner.OwningRow.Index, GridElementStates.Visible);
                            int index = base.Owner.Grid.Rows.GetRowCount(GridElementStates.Visible, 0, previousRow);
                            if (base.Owner.Grid.ColumnHeadersVisible)
                            {
                                return base.Owner.Grid.AccessibilityObject.GetChild(index + 1).GetChild(0);
                            }
                            return base.Owner.Grid.AccessibilityObject.GetChild(index).GetChild(0);
                        }
                        return null;

                    case AccessibleNavigation.Down:
                        if (base.Owner.OwningRow != null)
                        {
                            if (base.Owner.OwningRow.Index == base.Owner.Grid.Rows.GetLastRow(GridElementStates.Visible))
                            {
                                return null;
                            }
                            int nextRow = base.Owner.Grid.Rows.GetNextRow(base.Owner.OwningRow.Index, GridElementStates.Visible);
                            int num2 = base.Owner.Grid.Rows.GetRowCount(GridElementStates.Visible, 0, nextRow);
                            if (base.Owner.Grid.ColumnHeadersVisible)
                            {
                                return base.Owner.Grid.AccessibilityObject.GetChild(1 + num2).GetChild(0);
                            }
                            return base.Owner.Grid.AccessibilityObject.GetChild(num2).GetChild(0);
                        }
                        return null;

                    case AccessibleNavigation.Next:
                        if ((base.Owner.OwningRow == null) || (base.Owner.Grid.Columns.GetColumnCount(GridElementStates.Visible) <= 0))
                        {
                            return null;
                        }
                        return this.ParentPrivate.GetChild(1);

                    case AccessibleNavigation.Previous:
                        return null;
                }
                return null;
            }

            /// <summary>Modifies the row selection depending on the selection mode.</summary>
            /// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection"></see> values.</param>
            /// <exception cref="T:System.InvalidOperationException">The <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property value is null.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void Select(AccessibleSelection flags)
            {
                if (base.Owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                }
                GridRowHeaderCell owner = (GridRowHeaderCell) base.Owner;
                Grid grid = owner.Grid;
                if (grid != null)
                {
                    if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
                    {
                        grid.FocusInternal();
                    }
                    if ((owner.OwningRow != null) && ((grid.SelectionMode == GridSelectionMode.FullRowSelect) || (grid.SelectionMode == GridSelectionMode.RowHeaderSelect)))
                    {
                        if ((flags & (AccessibleSelection.AddSelection | AccessibleSelection.TakeSelection)) != AccessibleSelection.None)
                        {
                            owner.OwningRow.Selected = true;
                        }
                        else if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection)
                        {
                            owner.OwningRow.Selected = false;
                        }
                    }
                }
            }

            public override Rectangle Bounds
            {
                get
                {
                    if (base.Owner.OwningRow == null)
                    {
                        return Rectangle.Empty;
                    }
                    Rectangle bounds = this.ParentPrivate.Bounds;
                    bounds.Width = base.Owner.Grid.RowHeadersWidth;
                    return bounds;
                }
            }

            /// <summary>Gets a description of the default action of the <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>An empty string ("").</returns>
            public override string DefaultAction
            {
                get
                {
                    if ((base.Owner.Grid.SelectionMode != GridSelectionMode.FullRowSelect) && (base.Owner.Grid.SelectionMode != GridSelectionMode.RowHeaderSelect))
                    {
                        return string.Empty;
                    }
                    return MControl.GridView.RM.GetString("Grid_RowHeaderCellAccDefaultAction");
                }
            }

            /// <summary>Gets the name of the <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>The name of the <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see>.</returns>
            public override string Name
            {
                get
                {
                    if (this.ParentPrivate != null)
                    {
                        return this.ParentPrivate.Name;
                    }
                    return string.Empty;
                }
            }

            /// <summary>Gets the parent of the <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>The <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see> that belongs to the <see cref="T:MControl.GridView.GridRow"></see> of the current <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see>.</returns>
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
                    if (base.Owner.OwningRow == null)
                    {
                        return null;
                    }
                    return base.Owner.OwningRow.AccessibilityObject;
                }
            }

            /// <summary>Gets the role of the <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.RowHeader"></see> value.</returns>
            public override AccessibleRole Role
            {
                get
                {
                    return AccessibleRole.RowHeader;
                }
            }

            /// <summary>Gets the state of the <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates"></see> values. </returns>
            public override AccessibleStates State
            {
                get
                {
                    AccessibleStates selectable = AccessibleStates.Selectable;
                    if ((base.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen)
                    {
                        selectable |= AccessibleStates.Offscreen;
                    }
                    if (((base.Owner.Grid.SelectionMode == GridSelectionMode.FullRowSelect) || (base.Owner.Grid.SelectionMode == GridSelectionMode.RowHeaderSelect)) && ((base.Owner.OwningRow != null) && base.Owner.OwningRow.Selected))
                    {
                        selectable |= AccessibleStates.Selected;
                    }
                    return selectable;
                }
            }

            /// <summary>Gets the value of the <see cref="T:MControl.GridView.GridRowHeaderCell.GridRowHeaderCellAccessibleObject"></see>.</summary>
            /// <returns>An empty string ("").</returns>
            public override string Value
            {
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    return string.Empty;
                }
            }
        }

        private class GridRowHeaderCellRenderer
        {
            private static System.Windows.Forms.VisualStyles.VisualStyleRenderer visualStyleRenderer;

            private GridRowHeaderCellRenderer()
            {
            }

            public static void DrawHeader(Graphics g, Rectangle bounds, int headerState)
            {
                VisualStyleRenderer.SetParameters(GridRowHeaderCell.HeaderElement.ClassName, GridRowHeaderCell.HeaderElement.Part, headerState);
                VisualStyleRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
            }

            public static System.Windows.Forms.VisualStyles.VisualStyleRenderer VisualStyleRenderer
            {
                get
                {
                    if (visualStyleRenderer == null)
                    {
                        visualStyleRenderer = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(GridRowHeaderCell.HeaderElement);
                    }
                    return visualStyleRenderer;
                }
            }
        }
    }
}

