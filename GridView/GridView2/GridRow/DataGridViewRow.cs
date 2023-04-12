namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>Represents a row in a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    [TypeConverter(typeof(GridRowConverter))]
    public class GridRow : GridBand
    {
        internal const int defaultMinRowThickness = 3;
        private const GridAutoSizeRowCriteriaInternal invalidGridAutoSizeRowCriteriaInternalMask = ~(GridAutoSizeRowCriteriaInternal.AllColumns | GridAutoSizeRowCriteriaInternal.Header);
        private static readonly int PropRowAccessibilityObject = PropertyStore.CreateKey();
        private static readonly int PropRowErrorText = PropertyStore.CreateKey();
        private GridCellCollection rowCells;
        private static System.Type rowType = typeof(GridRow);

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRow"></see> class without using a template.</summary>
        public GridRow()
        {
            base.bandIsRow = true;
            base.MinimumThickness = 3;
            base.Thickness = Control.DefaultFont.Height + 9;
        }

        /// <summary>Modifies an input row header border style according to the specified criteria.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that represents the new border style used.</returns>
        /// <param name="gridAdvancedBorderStylePlaceholder">A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that is used to store intermediate changes to the row header border style.</param>
        /// <param name="gridAdvancedBorderStyleInput">A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that represents the row header border style to modify. </param>
        /// <param name="singleVerticalBorderAdded">true to add a single vertical border to the result; otherwise, false. </param>
        /// <param name="isLastVisibleRow">true if the row is the last row in the <see cref="T:MControl.GridView.Grid"></see> that has its <see cref="P:MControl.GridView.GridRow.Visible"></see> property set to true; otherwise, false. </param>
        /// <param name="singleHorizontalBorderAdded">true to add a single horizontal border to the result; otherwise, false. </param>
        /// <param name="isFirstDisplayedRow">true if the row is the first row displayed in the <see cref="T:MControl.GridView.Grid"></see>; otherwise, false. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual GridAdvancedBorderStyle AdjustRowHeaderBorderStyle(GridAdvancedBorderStyle gridAdvancedBorderStyleInput, GridAdvancedBorderStyle gridAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedRow, bool isLastVisibleRow)
        {
            if ((base.Grid == null) || !base.Grid.ApplyVisualStylesToHeaderCells)
            {
                switch (gridAdvancedBorderStyleInput.All)
                {
                    case GridAdvancedCellBorderStyle.Single:
                        if (isFirstDisplayedRow && !base.Grid.ColumnHeadersVisible)
                        {
                            return gridAdvancedBorderStyleInput;
                        }
                        gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Single;
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.None;
                        gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.Single;
                        gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Single;
                        return gridAdvancedBorderStylePlaceholder;

                    case GridAdvancedCellBorderStyle.Inset:
                        if (!isFirstDisplayedRow || !singleHorizontalBorderAdded)
                        {
                            return gridAdvancedBorderStyleInput;
                        }
                        gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Inset;
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.InsetDouble;
                        gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.Inset;
                        gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Inset;
                        return gridAdvancedBorderStylePlaceholder;

                    case GridAdvancedCellBorderStyle.InsetDouble:
                        if ((base.Grid == null) || !base.Grid.RightToLeftInternal)
                        {
                            gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.InsetDouble;
                            gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Inset;
                        }
                        else
                        {
                            gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Inset;
                            gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.InsetDouble;
                        }
                        if (isFirstDisplayedRow)
                        {
                            gridAdvancedBorderStylePlaceholder.TopInternal = base.Grid.ColumnHeadersVisible ? GridAdvancedCellBorderStyle.Inset : GridAdvancedCellBorderStyle.InsetDouble;
                        }
                        else
                        {
                            gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.Inset;
                        }
                        gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.Inset;
                        return gridAdvancedBorderStylePlaceholder;

                    case GridAdvancedCellBorderStyle.Outset:
                        if (!isFirstDisplayedRow || !singleHorizontalBorderAdded)
                        {
                            return gridAdvancedBorderStyleInput;
                        }
                        gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Outset;
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.OutsetDouble;
                        gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.Outset;
                        gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Outset;
                        return gridAdvancedBorderStylePlaceholder;

                    case GridAdvancedCellBorderStyle.OutsetDouble:
                        if ((base.Grid == null) || !base.Grid.RightToLeftInternal)
                        {
                            gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.OutsetDouble;
                            gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Outset;
                        }
                        else
                        {
                            gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Outset;
                            gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.OutsetDouble;
                        }
                        if (isFirstDisplayedRow)
                        {
                            gridAdvancedBorderStylePlaceholder.TopInternal = base.Grid.ColumnHeadersVisible ? GridAdvancedCellBorderStyle.Outset : GridAdvancedCellBorderStyle.OutsetDouble;
                        }
                        else
                        {
                            gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.Outset;
                        }
                        gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.Outset;
                        return gridAdvancedBorderStylePlaceholder;

                    case GridAdvancedCellBorderStyle.OutsetPartial:
                        if ((base.Grid == null) || !base.Grid.RightToLeftInternal)
                        {
                            gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.OutsetDouble;
                            gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Outset;
                        }
                        else
                        {
                            gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Outset;
                            gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.OutsetDouble;
                        }
                        if (isFirstDisplayedRow)
                        {
                            gridAdvancedBorderStylePlaceholder.TopInternal = base.Grid.ColumnHeadersVisible ? GridAdvancedCellBorderStyle.Outset : GridAdvancedCellBorderStyle.OutsetDouble;
                        }
                        else
                        {
                            gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.OutsetPartial;
                        }
                        gridAdvancedBorderStylePlaceholder.BottomInternal = isLastVisibleRow ? GridAdvancedCellBorderStyle.Outset : GridAdvancedCellBorderStyle.OutsetPartial;
                        return gridAdvancedBorderStylePlaceholder;
                }
                return gridAdvancedBorderStyleInput;
            }
            switch (gridAdvancedBorderStyleInput.All)
            {
                case GridAdvancedCellBorderStyle.Single:
                    if (!isFirstDisplayedRow || base.Grid.ColumnHeadersVisible)
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.None;
                    }
                    else
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.Single;
                    }
                    gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Single;
                    gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Single;
                    gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.None;
                    return gridAdvancedBorderStylePlaceholder;

                case GridAdvancedCellBorderStyle.Inset:
                    if (!isFirstDisplayedRow || base.Grid.ColumnHeadersVisible)
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.None;
                        break;
                    }
                    gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.Inset;
                    break;

                case GridAdvancedCellBorderStyle.InsetDouble:
                    if (!isFirstDisplayedRow || base.Grid.ColumnHeadersVisible)
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.None;
                    }
                    else
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.InsetDouble;
                    }
                    if ((base.Grid != null) && base.Grid.RightToLeftInternal)
                    {
                        gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Inset;
                    }
                    else
                    {
                        gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.InsetDouble;
                    }
                    gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Inset;
                    gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.None;
                    return gridAdvancedBorderStylePlaceholder;

                case GridAdvancedCellBorderStyle.Outset:
                    if (!isFirstDisplayedRow || base.Grid.ColumnHeadersVisible)
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.None;
                    }
                    else
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.Outset;
                    }
                    gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Outset;
                    gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Outset;
                    gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.None;
                    return gridAdvancedBorderStylePlaceholder;

                case GridAdvancedCellBorderStyle.OutsetDouble:
                    if (!isFirstDisplayedRow || base.Grid.ColumnHeadersVisible)
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.None;
                    }
                    else
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.OutsetDouble;
                    }
                    if ((base.Grid != null) && base.Grid.RightToLeftInternal)
                    {
                        gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Outset;
                    }
                    else
                    {
                        gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.OutsetDouble;
                    }
                    gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Outset;
                    gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.None;
                    return gridAdvancedBorderStylePlaceholder;

                case GridAdvancedCellBorderStyle.OutsetPartial:
                    if (!isFirstDisplayedRow || base.Grid.ColumnHeadersVisible)
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.None;
                    }
                    else
                    {
                        gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.OutsetDouble;
                    }
                    if ((base.Grid != null) && base.Grid.RightToLeftInternal)
                    {
                        gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Outset;
                    }
                    else
                    {
                        gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.OutsetDouble;
                    }
                    gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Outset;
                    gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.None;
                    return gridAdvancedBorderStylePlaceholder;

                default:
                    return gridAdvancedBorderStyleInput;
            }
            gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Inset;
            gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Inset;
            gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.None;
            return gridAdvancedBorderStylePlaceholder;
        }

        private void BuildInheritedRowHeaderCellStyle(GridCellStyle inheritedCellStyle)
        {
            GridCellStyle style = null;
            if (this.HeaderCell.HasStyle)
            {
                style = this.HeaderCell.Style;
            }
            GridCellStyle rowHeadersDefaultCellStyle = base.Grid.RowHeadersDefaultCellStyle;
            GridCellStyle defaultCellStyle = base.Grid.DefaultCellStyle;
            if ((style != null) && !style.BackColor.IsEmpty)
            {
                inheritedCellStyle.BackColor = style.BackColor;
            }
            else if (!rowHeadersDefaultCellStyle.BackColor.IsEmpty)
            {
                inheritedCellStyle.BackColor = rowHeadersDefaultCellStyle.BackColor;
            }
            else
            {
                inheritedCellStyle.BackColor = defaultCellStyle.BackColor;
            }
            if ((style != null) && !style.ForeColor.IsEmpty)
            {
                inheritedCellStyle.ForeColor = style.ForeColor;
            }
            else if (!rowHeadersDefaultCellStyle.ForeColor.IsEmpty)
            {
                inheritedCellStyle.ForeColor = rowHeadersDefaultCellStyle.ForeColor;
            }
            else
            {
                inheritedCellStyle.ForeColor = defaultCellStyle.ForeColor;
            }
            if ((style != null) && !style.SelectionBackColor.IsEmpty)
            {
                inheritedCellStyle.SelectionBackColor = style.SelectionBackColor;
            }
            else if (!rowHeadersDefaultCellStyle.SelectionBackColor.IsEmpty)
            {
                inheritedCellStyle.SelectionBackColor = rowHeadersDefaultCellStyle.SelectionBackColor;
            }
            else
            {
                inheritedCellStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
            }
            if ((style != null) && !style.SelectionForeColor.IsEmpty)
            {
                inheritedCellStyle.SelectionForeColor = style.SelectionForeColor;
            }
            else if (!rowHeadersDefaultCellStyle.SelectionForeColor.IsEmpty)
            {
                inheritedCellStyle.SelectionForeColor = rowHeadersDefaultCellStyle.SelectionForeColor;
            }
            else
            {
                inheritedCellStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
            }
            if ((style != null) && (style.Font != null))
            {
                inheritedCellStyle.Font = style.Font;
            }
            else if (rowHeadersDefaultCellStyle.Font != null)
            {
                inheritedCellStyle.Font = rowHeadersDefaultCellStyle.Font;
            }
            else
            {
                inheritedCellStyle.Font = defaultCellStyle.Font;
            }
            if ((style != null) && !style.IsNullValueDefault)
            {
                inheritedCellStyle.NullValue = style.NullValue;
            }
            else if (!rowHeadersDefaultCellStyle.IsNullValueDefault)
            {
                inheritedCellStyle.NullValue = rowHeadersDefaultCellStyle.NullValue;
            }
            else
            {
                inheritedCellStyle.NullValue = defaultCellStyle.NullValue;
            }
            if ((style != null) && !style.IsDataSourceNullValueDefault)
            {
                inheritedCellStyle.DataSourceNullValue = style.DataSourceNullValue;
            }
            else if (!rowHeadersDefaultCellStyle.IsDataSourceNullValueDefault)
            {
                inheritedCellStyle.DataSourceNullValue = rowHeadersDefaultCellStyle.DataSourceNullValue;
            }
            else
            {
                inheritedCellStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
            }
            if ((style != null) && (style.Format.Length != 0))
            {
                inheritedCellStyle.Format = style.Format;
            }
            else if (rowHeadersDefaultCellStyle.Format.Length != 0)
            {
                inheritedCellStyle.Format = rowHeadersDefaultCellStyle.Format;
            }
            else
            {
                inheritedCellStyle.Format = defaultCellStyle.Format;
            }
            if ((style != null) && !style.IsFormatProviderDefault)
            {
                inheritedCellStyle.FormatProvider = style.FormatProvider;
            }
            else if (!rowHeadersDefaultCellStyle.IsFormatProviderDefault)
            {
                inheritedCellStyle.FormatProvider = rowHeadersDefaultCellStyle.FormatProvider;
            }
            else
            {
                inheritedCellStyle.FormatProvider = defaultCellStyle.FormatProvider;
            }
            if ((style != null) && (style.Alignment != GridContentAlignment.NotSet))
            {
                inheritedCellStyle.AlignmentInternal = style.Alignment;
            }
            else if ((rowHeadersDefaultCellStyle != null) && (rowHeadersDefaultCellStyle.Alignment != GridContentAlignment.NotSet))
            {
                inheritedCellStyle.AlignmentInternal = rowHeadersDefaultCellStyle.Alignment;
            }
            else
            {
                inheritedCellStyle.AlignmentInternal = defaultCellStyle.Alignment;
            }
            if ((style != null) && (style.WrapMode != GridTriState.NotSet))
            {
                inheritedCellStyle.WrapModeInternal = style.WrapMode;
            }
            else if ((rowHeadersDefaultCellStyle != null) && (rowHeadersDefaultCellStyle.WrapMode != GridTriState.NotSet))
            {
                inheritedCellStyle.WrapModeInternal = rowHeadersDefaultCellStyle.WrapMode;
            }
            else
            {
                inheritedCellStyle.WrapModeInternal = defaultCellStyle.WrapMode;
            }
            if ((style != null) && (style.Tag != null))
            {
                inheritedCellStyle.Tag = style.Tag;
            }
            else if (rowHeadersDefaultCellStyle.Tag != null)
            {
                inheritedCellStyle.Tag = rowHeadersDefaultCellStyle.Tag;
            }
            else
            {
                inheritedCellStyle.Tag = defaultCellStyle.Tag;
            }
            if ((style != null) && (style.Padding != Padding.Empty))
            {
                inheritedCellStyle.PaddingInternal = style.Padding;
            }
            else if (rowHeadersDefaultCellStyle.Padding != Padding.Empty)
            {
                inheritedCellStyle.PaddingInternal = rowHeadersDefaultCellStyle.Padding;
            }
            else
            {
                inheritedCellStyle.PaddingInternal = defaultCellStyle.Padding;
            }
        }

        private void BuildInheritedRowStyle(int rowIndex, GridCellStyle inheritedRowStyle)
        {
            GridCellStyle style = null;
            if (base.HasDefaultCellStyle)
            {
                style = this.DefaultCellStyle;
            }
            GridCellStyle defaultCellStyle = base.Grid.DefaultCellStyle;
            GridCellStyle rowsDefaultCellStyle = base.Grid.RowsDefaultCellStyle;
            GridCellStyle alternatingRowsDefaultCellStyle = base.Grid.AlternatingRowsDefaultCellStyle;
            if ((style != null) && !style.BackColor.IsEmpty)
            {
                inheritedRowStyle.BackColor = style.BackColor;
            }
            else if (!rowsDefaultCellStyle.BackColor.IsEmpty && (((rowIndex % 2) == 0) || alternatingRowsDefaultCellStyle.BackColor.IsEmpty))
            {
                inheritedRowStyle.BackColor = rowsDefaultCellStyle.BackColor;
            }
            else if (((rowIndex % 2) == 1) && !alternatingRowsDefaultCellStyle.BackColor.IsEmpty)
            {
                inheritedRowStyle.BackColor = alternatingRowsDefaultCellStyle.BackColor;
            }
            else
            {
                inheritedRowStyle.BackColor = defaultCellStyle.BackColor;
            }
            if ((style != null) && !style.ForeColor.IsEmpty)
            {
                inheritedRowStyle.ForeColor = style.ForeColor;
            }
            else if (!rowsDefaultCellStyle.ForeColor.IsEmpty && (((rowIndex % 2) == 0) || alternatingRowsDefaultCellStyle.ForeColor.IsEmpty))
            {
                inheritedRowStyle.ForeColor = rowsDefaultCellStyle.ForeColor;
            }
            else if (((rowIndex % 2) == 1) && !alternatingRowsDefaultCellStyle.ForeColor.IsEmpty)
            {
                inheritedRowStyle.ForeColor = alternatingRowsDefaultCellStyle.ForeColor;
            }
            else
            {
                inheritedRowStyle.ForeColor = defaultCellStyle.ForeColor;
            }
            if ((style != null) && !style.SelectionBackColor.IsEmpty)
            {
                inheritedRowStyle.SelectionBackColor = style.SelectionBackColor;
            }
            else if (!rowsDefaultCellStyle.SelectionBackColor.IsEmpty && (((rowIndex % 2) == 0) || alternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty))
            {
                inheritedRowStyle.SelectionBackColor = rowsDefaultCellStyle.SelectionBackColor;
            }
            else if (((rowIndex % 2) == 1) && !alternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty)
            {
                inheritedRowStyle.SelectionBackColor = alternatingRowsDefaultCellStyle.SelectionBackColor;
            }
            else
            {
                inheritedRowStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
            }
            if ((style != null) && !style.SelectionForeColor.IsEmpty)
            {
                inheritedRowStyle.SelectionForeColor = style.SelectionForeColor;
            }
            else if (!rowsDefaultCellStyle.SelectionForeColor.IsEmpty && (((rowIndex % 2) == 0) || alternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty))
            {
                inheritedRowStyle.SelectionForeColor = rowsDefaultCellStyle.SelectionForeColor;
            }
            else if (((rowIndex % 2) == 1) && !alternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty)
            {
                inheritedRowStyle.SelectionForeColor = alternatingRowsDefaultCellStyle.SelectionForeColor;
            }
            else
            {
                inheritedRowStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
            }
            if ((style != null) && (style.Font != null))
            {
                inheritedRowStyle.Font = style.Font;
            }
            else if ((rowsDefaultCellStyle.Font != null) && (((rowIndex % 2) == 0) || (alternatingRowsDefaultCellStyle.Font == null)))
            {
                inheritedRowStyle.Font = rowsDefaultCellStyle.Font;
            }
            else if (((rowIndex % 2) == 1) && (alternatingRowsDefaultCellStyle.Font != null))
            {
                inheritedRowStyle.Font = alternatingRowsDefaultCellStyle.Font;
            }
            else
            {
                inheritedRowStyle.Font = defaultCellStyle.Font;
            }
            if ((style != null) && !style.IsNullValueDefault)
            {
                inheritedRowStyle.NullValue = style.NullValue;
            }
            else if (!rowsDefaultCellStyle.IsNullValueDefault && (((rowIndex % 2) == 0) || alternatingRowsDefaultCellStyle.IsNullValueDefault))
            {
                inheritedRowStyle.NullValue = rowsDefaultCellStyle.NullValue;
            }
            else if (((rowIndex % 2) == 1) && !alternatingRowsDefaultCellStyle.IsNullValueDefault)
            {
                inheritedRowStyle.NullValue = alternatingRowsDefaultCellStyle.NullValue;
            }
            else
            {
                inheritedRowStyle.NullValue = defaultCellStyle.NullValue;
            }
            if ((style != null) && !style.IsDataSourceNullValueDefault)
            {
                inheritedRowStyle.DataSourceNullValue = style.DataSourceNullValue;
            }
            else if (!rowsDefaultCellStyle.IsDataSourceNullValueDefault && (((rowIndex % 2) == 0) || alternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault))
            {
                inheritedRowStyle.DataSourceNullValue = rowsDefaultCellStyle.DataSourceNullValue;
            }
            else if (((rowIndex % 2) == 1) && !alternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault)
            {
                inheritedRowStyle.DataSourceNullValue = alternatingRowsDefaultCellStyle.DataSourceNullValue;
            }
            else
            {
                inheritedRowStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
            }
            if ((style != null) && (style.Format.Length != 0))
            {
                inheritedRowStyle.Format = style.Format;
            }
            else if ((rowsDefaultCellStyle.Format.Length != 0) && (((rowIndex % 2) == 0) || (alternatingRowsDefaultCellStyle.Format.Length == 0)))
            {
                inheritedRowStyle.Format = rowsDefaultCellStyle.Format;
            }
            else if (((rowIndex % 2) == 1) && (alternatingRowsDefaultCellStyle.Format.Length != 0))
            {
                inheritedRowStyle.Format = alternatingRowsDefaultCellStyle.Format;
            }
            else
            {
                inheritedRowStyle.Format = defaultCellStyle.Format;
            }
            if ((style != null) && !style.IsFormatProviderDefault)
            {
                inheritedRowStyle.FormatProvider = style.FormatProvider;
            }
            else if (!rowsDefaultCellStyle.IsFormatProviderDefault && (((rowIndex % 2) == 0) || alternatingRowsDefaultCellStyle.IsFormatProviderDefault))
            {
                inheritedRowStyle.FormatProvider = rowsDefaultCellStyle.FormatProvider;
            }
            else if (((rowIndex % 2) == 1) && !alternatingRowsDefaultCellStyle.IsFormatProviderDefault)
            {
                inheritedRowStyle.FormatProvider = alternatingRowsDefaultCellStyle.FormatProvider;
            }
            else
            {
                inheritedRowStyle.FormatProvider = defaultCellStyle.FormatProvider;
            }
            if ((style != null) && (style.Alignment != GridContentAlignment.NotSet))
            {
                inheritedRowStyle.AlignmentInternal = style.Alignment;
            }
            else if ((rowsDefaultCellStyle.Alignment != GridContentAlignment.NotSet) && (((rowIndex % 2) == 0) || (alternatingRowsDefaultCellStyle.Alignment == GridContentAlignment.NotSet)))
            {
                inheritedRowStyle.AlignmentInternal = rowsDefaultCellStyle.Alignment;
            }
            else if (((rowIndex % 2) == 1) && (alternatingRowsDefaultCellStyle.Alignment != GridContentAlignment.NotSet))
            {
                inheritedRowStyle.AlignmentInternal = alternatingRowsDefaultCellStyle.Alignment;
            }
            else
            {
                inheritedRowStyle.AlignmentInternal = defaultCellStyle.Alignment;
            }
            if ((style != null) && (style.WrapMode != GridTriState.NotSet))
            {
                inheritedRowStyle.WrapModeInternal = style.WrapMode;
            }
            else if ((rowsDefaultCellStyle.WrapMode != GridTriState.NotSet) && (((rowIndex % 2) == 0) || (alternatingRowsDefaultCellStyle.WrapMode == GridTriState.NotSet)))
            {
                inheritedRowStyle.WrapModeInternal = rowsDefaultCellStyle.WrapMode;
            }
            else if (((rowIndex % 2) == 1) && (alternatingRowsDefaultCellStyle.WrapMode != GridTriState.NotSet))
            {
                inheritedRowStyle.WrapModeInternal = alternatingRowsDefaultCellStyle.WrapMode;
            }
            else
            {
                inheritedRowStyle.WrapModeInternal = defaultCellStyle.WrapMode;
            }
            if ((style != null) && (style.Tag != null))
            {
                inheritedRowStyle.Tag = style.Tag;
            }
            else if ((rowsDefaultCellStyle.Tag != null) && (((rowIndex % 2) == 0) || (alternatingRowsDefaultCellStyle.Tag == null)))
            {
                inheritedRowStyle.Tag = rowsDefaultCellStyle.Tag;
            }
            else if (((rowIndex % 2) == 1) && (alternatingRowsDefaultCellStyle.Tag != null))
            {
                inheritedRowStyle.Tag = alternatingRowsDefaultCellStyle.Tag;
            }
            else
            {
                inheritedRowStyle.Tag = defaultCellStyle.Tag;
            }
            if ((style != null) && (style.Padding != Padding.Empty))
            {
                inheritedRowStyle.PaddingInternal = style.Padding;
            }
            else if ((rowsDefaultCellStyle.Padding != Padding.Empty) && (((rowIndex % 2) == 0) || (alternatingRowsDefaultCellStyle.Padding == Padding.Empty)))
            {
                inheritedRowStyle.PaddingInternal = rowsDefaultCellStyle.Padding;
            }
            else if (((rowIndex % 2) == 1) && (alternatingRowsDefaultCellStyle.Padding != Padding.Empty))
            {
                inheritedRowStyle.PaddingInternal = alternatingRowsDefaultCellStyle.Padding;
            }
            else
            {
                inheritedRowStyle.PaddingInternal = defaultCellStyle.Padding;
            }
        }

        /// <summary>Creates an exact copy of this row.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridRow"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridRow row;
            System.Type type = base.GetType();
            if (type == rowType)
            {
                row = new GridRow();
            }
            else
            {
                row = (GridRow) Activator.CreateInstance(type);
            }
            if (row != null)
            {
                base.CloneInternal(row);
                if (this.HasErrorText)
                {
                    row.ErrorText = this.ErrorTextInternal;
                }
                if (base.HasHeaderCell)
                {
                    row.HeaderCell = (GridRowHeaderCell) this.HeaderCell.Clone();
                }
                row.CloneCells(this);
            }
            return row;
        }

        private void CloneCells(GridRow rowTemplate)
        {
            int count = rowTemplate.Cells.Count;
            if (count > 0)
            {
                GridCell[] gridCells = new GridCell[count];
                for (int i = 0; i < count; i++)
                {
                    GridCell cell = rowTemplate.Cells[i];
                    gridCells[i] = (GridCell) cell.Clone();
                }
                this.Cells.AddRange(gridCells);
            }
        }

        /// <summary>Creates a new accessible object for the <see cref="T:MControl.GridView.GridRow"></see>. </summary>
        /// <returns>A new <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see> for the <see cref="T:MControl.GridView.GridRow"></see>. </returns>
        protected virtual AccessibleObject CreateAccessibilityInstance()
        {
            return new GridRowAccessibleObject(this);
        }

        /// <summary>Clears the existing cells and sets their template according to the supplied <see cref="T:MControl.GridView.Grid"></see> template.</summary>
        /// <param name="grid">A <see cref="T:MControl.GridView.Grid"></see> that acts as a template for cell styles. </param>
        /// <exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:MControl.GridView.Grid"></see> was added. -or-A column that has no cell template was added.</exception>
        /// <exception cref="T:System.ArgumentNullException">grid is null. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public void CreateCells(Grid grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }
            if (base.Grid != null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_RowAlreadyBelongsToGrid"));
            }
            GridCellCollection cells = this.Cells;
            cells.Clear();
            foreach (GridColumn column in grid.Columns)
            {
                if (column.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_AColumnHasNoCellTemplate"));
                }
                GridCell gridCell = (GridCell) column.CellTemplate.Clone();
                cells.Add(gridCell);
            }
        }

        /// <summary>Clears the existing cells and sets their template and values.</summary>
        /// <param name="grid">A <see cref="T:MControl.GridView.Grid"></see> that acts as a template for cell styles. </param>
        /// <param name="values">An array of objects that initialize the reset cells. </param>
        /// <exception cref="T:System.ArgumentNullException">Either of the parameters is null. </exception>
        /// <exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:MControl.GridView.Grid"></see> was added. -or-A column that has no cell template was added.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public void CreateCells(Grid grid, params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            this.CreateCells(grid);
            this.SetValuesInternal(values);
        }

        /// <summary>Constructs a new collection of cells based on this row.</summary>
        /// <returns>The newly created <see cref="T:MControl.GridView.GridCellCollection"></see>.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual GridCellCollection CreateCellsInstance()
        {
            return new GridCellCollection(this);
        }

        internal void DetachFromGrid()
        {
            if (base.Grid != null)
            {
                base.GridInternal = null;
                base.IndexInternal = -1;
                if (base.HasHeaderCell)
                {
                    this.HeaderCell.GridInternal = null;
                }
                foreach (GridCell cell in this.Cells)
                {
                    cell.GridInternal = null;
                    if (cell.Selected)
                    {
                        cell.SelectedInternal = false;
                    }
                }
                if (this.Selected)
                {
                    base.SelectedInternal = false;
                }
            }
        }

        /// <summary>Draws a focus rectangle around the specified bounds.</summary>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> used to paint the focus rectangle.</param>
        /// <param name="rowState">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values that specifies the state of the row.</param>
        /// <param name="bounds">A <see cref="T:System.Drawing.Rectangle"></see> that contains the bounds of the <see cref="T:MControl.GridView.GridRow"></see> that is being painted.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to paint the <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be painted.</param>
        /// <param name="cellsPaintSelectionBackground">true to use the <see cref="P:MControl.GridView.GridCellStyle.SelectionBackColor"></see> property of cellStyle as the color of the focus rectangle; false to use the <see cref="P:MControl.GridView.GridCellStyle.BackColor"></see> property of cellStyle as the color of the focus rectangle.</param>
        /// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        /// <exception cref="T:System.ArgumentNullException">graphics is null.-or-cellStyle is null.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual void DrawFocus(Graphics graphics, Rectangle clipBounds, Rectangle bounds, int rowIndex, GridElementStates rowState, GridCellStyle cellStyle, bool cellsPaintSelectionBackground)
        {
            Color selectionBackColor;
            if (base.Grid == null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_RowDoesNotYetBelongToGrid"));
            }
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if (cellsPaintSelectionBackground && ((rowState & GridElementStates.Selected) != GridElementStates.None))
            {
                selectionBackColor = cellStyle.SelectionBackColor;
            }
            else
            {
                selectionBackColor = cellStyle.BackColor;
            }
            ControlPaint.DrawFocusRectangle(graphics, bounds, Color.Empty, selectionBackColor);
        }

        /// <summary>Gets the shortcut menu for the row.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> that belongs to the <see cref="T:MControl.GridView.GridRow"></see> at the specified index.</returns>
        /// <param name="rowIndex">The index of the current row.</param>
        /// <exception cref="T:System.InvalidOperationException">rowIndex is -1.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than zero or greater than or equal to the number of rows in the control minus one.</exception>
        public System.Windows.Forms.ContextMenuStrip GetContextMenuStrip(int rowIndex)
        {
            System.Windows.Forms.ContextMenuStrip contextMenuStripInternal = base.ContextMenuStripInternal;
            if (base.Grid == null)
            {
                return contextMenuStripInternal;
            }
            if (rowIndex == -1)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedRow"));
            }
            if ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if (!base.Grid.VirtualMode && (base.Grid.DataSource == null))
            {
                return contextMenuStripInternal;
            }
            return base.Grid.OnRowContextMenuStripNeeded(rowIndex, contextMenuStripInternal);
        }

        internal bool GetDisplayed(int rowIndex)
        {
            return ((this.GetState(rowIndex) & GridElementStates.Displayed) != GridElementStates.None);
        }

        /// <summary>Gets the error text for the row at the specified index.</summary>
        /// <returns>A string that describes the error of the row at the specified index.</returns>
        /// <param name="rowIndex">The index of the row that contains the error.</param>
        /// <exception cref="T:System.InvalidOperationException">The row belongs to a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The row belongs to a <see cref="T:MControl.GridView.Grid"></see> control and rowIndex is less than zero or greater than the number of rows in the control minus one. </exception>
        public string GetErrorText(int rowIndex)
        {
            string errorTextInternal = this.ErrorTextInternal;
            if (base.Grid == null)
            {
                return errorTextInternal;
            }
            if (rowIndex == -1)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedRow"));
            }
            if ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if ((string.IsNullOrEmpty(errorTextInternal) && (base.Grid.DataSource != null)) && (rowIndex != base.Grid.NewRowIndex))
            {
                errorTextInternal = base.Grid.DataConnection.GetError(rowIndex);
            }
            if ((base.Grid.DataSource == null) && !base.Grid.VirtualMode)
            {
                return errorTextInternal;
            }
            return base.Grid.OnRowErrorTextNeeded(rowIndex, errorTextInternal);
        }

        internal bool GetFrozen(int rowIndex)
        {
            return ((this.GetState(rowIndex) & GridElementStates.Frozen) != GridElementStates.None);
        }

        internal int GetHeight(int rowIndex)
        {
            int num;
            int num2;
            base.GetHeightInfo(rowIndex, out num, out num2);
            return num;
        }

        internal int GetMinimumHeight(int rowIndex)
        {
            int num;
            int num2;
            base.GetHeightInfo(rowIndex, out num, out num2);
            return num2;
        }

        /// <summary>Calculates the ideal height of the specified row based on the specified criteria.</summary>
        /// <returns>The ideal height of the row, in pixels.</returns>
        /// <param name="autoSizeRowMode">A <see cref="T:MControl.GridView.GridAutoSizeRowMode"></see> that specifies an automatic sizing mode.</param>
        /// <param name="rowIndex">The index of the row whose preferred height is calculated.</param>
        /// <param name="fixedWidth">true to calculate the preferred height for a fixed cell width; otherwise, false.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The rowIndex is not in the valid range of 0 to the number of rows in the control minus 1. </exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">autoSizeRowMode is not a valid <see cref="T:MControl.GridView.GridAutoSizeRowMode"></see> value. </exception>
        public virtual int GetPreferredHeight(int rowIndex, GridAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
        {
            if ((autoSizeRowMode & ~GridAutoSizeRowMode.AllCells) != ((GridAutoSizeRowMode) 0))
            {
                throw new InvalidEnumArgumentException("autoSizeRowMode", (int) autoSizeRowMode, typeof(GridAutoSizeRowMode));
            }
            if ((base.Grid != null) && ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count)))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if (base.Grid == null)
            {
                return -1;
            }
            int num = 0;
            if (base.Grid.RowHeadersVisible && ((autoSizeRowMode & GridAutoSizeRowMode.RowHeader) != ((GridAutoSizeRowMode) 0)))
            {
                if ((fixedWidth || (base.Grid.RowHeadersWidthSizeMode == GridRowHeadersWidthSizeMode.EnableResizing)) || (base.Grid.RowHeadersWidthSizeMode == GridRowHeadersWidthSizeMode.DisableResizing))
                {
                    num = Math.Max(num, this.HeaderCell.GetPreferredHeight(rowIndex, base.Grid.RowHeadersWidth));
                }
                else
                {
                    num = Math.Max(num, this.HeaderCell.GetPreferredSize(rowIndex).Height);
                }
            }
            if ((autoSizeRowMode & GridAutoSizeRowMode.AllCellsExceptHeader) != ((GridAutoSizeRowMode) 0))
            {
                foreach (GridCell cell in this.Cells)
                {
                    GridColumn column = base.Grid.Columns[cell.ColumnIndex];
                    if (column.Visible)
                    {
                        int preferredHeight;
                        if (fixedWidth || ((column.InheritedAutoSizeMode & (GridAutoSizeColumnMode.DisplayedCellsExceptHeader | GridAutoSizeColumnMode.AllCellsExceptHeader)) == GridAutoSizeColumnMode.NotSet))
                        {
                            preferredHeight = cell.GetPreferredHeight(rowIndex, column.Width);
                        }
                        else
                        {
                            preferredHeight = cell.GetPreferredSize(rowIndex).Height;
                        }
                        if (num < preferredHeight)
                        {
                            num = preferredHeight;
                        }
                    }
                }
            }
            return num;
        }

        internal bool GetReadOnly(int rowIndex)
        {
            return (((this.GetState(rowIndex) & GridElementStates.ReadOnly) != GridElementStates.None) || ((base.Grid != null) && base.Grid.ReadOnly));
        }

        internal GridTriState GetResizable(int rowIndex)
        {
            if ((this.GetState(rowIndex) & GridElementStates.ResizableSet) != GridElementStates.None)
            {
                if ((this.GetState(rowIndex) & GridElementStates.Resizable) == GridElementStates.None)
                {
                    return GridTriState.False;
                }
                return GridTriState.True;
            }
            if (base.Grid == null)
            {
                return GridTriState.NotSet;
            }
            if (!base.Grid.AllowUserToResizeRows)
            {
                return GridTriState.False;
            }
            return GridTriState.True;
        }

        internal bool GetSelected(int rowIndex)
        {
            return ((this.GetState(rowIndex) & GridElementStates.Selected) != GridElementStates.None);
        }

        /// <summary>Returns a value indicating the current state of the row.</summary>
        /// <returns>A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values indicating the row state.</returns>
        /// <param name="rowIndex">The index of the row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The row has been added to a <see cref="T:MControl.GridView.Grid"></see> control, but the rowIndex value is not in the valid range of 0 to the number of rows in the control minus 1.</exception>
        /// <exception cref="T:System.ArgumentException">The row is not a shared row, but the rowIndex value does not match the row's <see cref="P:MControl.GridView.GridBand.Index"></see> property value.-or-The row has not been added to a <see cref="T:MControl.GridView.Grid"></see> control, but the rowIndex value does not match the row's <see cref="P:MControl.GridView.GridBand.Index"></see> property value.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual GridElementStates GetState(int rowIndex)
        {
            if ((base.Grid != null) && ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count)))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if ((base.Grid != null) && (base.Grid.Rows.SharedRow(rowIndex).Index == -1))
            {
                return base.Grid.Rows.GetRowState(rowIndex);
            }
            if (rowIndex != base.Index)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("InvalidArgument", new object[] { "rowIndex", rowIndex.ToString(CultureInfo.CurrentCulture) }));
            }
            return base.State;
        }

        internal bool GetVisible(int rowIndex)
        {
            return ((this.GetState(rowIndex) & GridElementStates.Visible) != GridElementStates.None);
        }

        internal void OnSharedStateChanged(int sharedRowIndex, GridElementStates elementState)
        {
            base.Grid.Rows.InvalidateCachedRowCount(elementState);
            base.Grid.Rows.InvalidateCachedRowsHeight(elementState);
            base.Grid.OnGridElementStateChanged(this, sharedRowIndex, elementState);
        }

        internal void OnSharedStateChanging(int sharedRowIndex, GridElementStates elementState)
        {
            base.Grid.OnGridElementStateChanging(this, sharedRowIndex, elementState);
        }

        /// <summary>Paints the current row.</summary>
        /// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle"></see> that contains the bounds of the <see cref="T:MControl.GridView.GridRow"></see> that is being painted.</param>
        /// <param name="rowState">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values that specifies the state of the row.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to paint the <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:MControl.GridView.Grid"></see> that has the <see cref="P:MControl.GridView.GridRow.Visible"></see> property set to true; otherwise, false.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be painted.</param>
        /// <param name="isFirstDisplayedRow">true to indicate whether the current row is the first row displayed in the <see cref="T:MControl.GridView.Grid"></see>; otherwise, false.</param>
        /// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:MControl.GridView.Grid"></see> control.-or-The row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The row is in a <see cref="T:MControl.GridView.Grid"></see> control and rowIndex is less than zero or greater than the number of rows in the control minus one.</exception>
        protected internal virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, GridElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow)
        {
            if (base.Grid == null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_RowDoesNotYetBelongToGrid"));
            }
            Grid grid = base.Grid;
            Rectangle rectangle = clipBounds;
            GridRow row = grid.Rows.SharedRow(rowIndex);
            GridCellStyle inheritedRowStyle = new GridCellStyle();
            this.BuildInheritedRowStyle(rowIndex, inheritedRowStyle);
            GridRowPrePaintEventArgs rowPrePaintEventArgs = grid.RowPrePaintEventArgs;
            rowPrePaintEventArgs.SetProperties(graphics, clipBounds, rowBounds, rowIndex, rowState, row.GetErrorText(rowIndex), inheritedRowStyle, isFirstDisplayedRow, isLastVisibleRow);
            grid.OnRowPrePaint(rowPrePaintEventArgs);
            if (!rowPrePaintEventArgs.Handled)
            {
                GridPaintParts paintParts = rowPrePaintEventArgs.PaintParts;
                rectangle = rowPrePaintEventArgs.ClipBounds;
                this.PaintHeader(graphics, rectangle, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
                this.PaintCells(graphics, rectangle, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
                row = grid.Rows.SharedRow(rowIndex);
                this.BuildInheritedRowStyle(rowIndex, inheritedRowStyle);
                GridRowPostPaintEventArgs rowPostPaintEventArgs = grid.RowPostPaintEventArgs;
                rowPostPaintEventArgs.SetProperties(graphics, rectangle, rowBounds, rowIndex, rowState, row.GetErrorText(rowIndex), inheritedRowStyle, isFirstDisplayedRow, isLastVisibleRow);
                grid.OnRowPostPaint(rowPostPaintEventArgs);
            }
        }

        /// <summary>Paints the cells in the current row.</summary>
        /// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle"></see> that contains the bounds of the <see cref="T:MControl.GridView.GridRow"></see> that is being painted.</param>
        /// <param name="rowState">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values that specifies the state of the row.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to paint the <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:MControl.GridView.Grid"></see> that has the <see cref="P:MControl.GridView.GridRow.Visible"></see> property set to true; otherwise, false.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be painted.</param>
        /// <param name="paintParts">A bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values indicating the parts of the cells to paint.</param>
        /// <param name="isFirstDisplayedRow">true to indicate whether the current row is the first row displayed in the <see cref="T:MControl.GridView.Grid"></see>; otherwise, false.</param>
        /// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        /// <exception cref="T:System.ArgumentException">paintParts in not a valid bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual void PaintCells(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, GridElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, GridPaintParts paintParts)
        {
            GridCell cell;
            GridAdvancedBorderStyle style3;
            if (base.Grid == null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_RowDoesNotYetBelongToGrid"));
            }
            if ((paintParts < GridPaintParts.None) || (paintParts > GridPaintParts.All))
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridPaintPartsCombination", new object[] { "paintParts" }));
            }
            Grid grid = base.Grid;
            Rectangle rect = rowBounds;
            int num = grid.RowHeadersVisible ? grid.RowHeadersWidth : 0;
            bool isFirstDisplayedColumn = true;
            GridElementStates none = GridElementStates.None;
            GridCellStyle inheritedCellStyle = new GridCellStyle();
            GridColumn column = null;
            GridAdvancedBorderStyle gridAdvancedBorderStylePlaceholder = new GridAdvancedBorderStyle();
            GridColumn firstColumn = grid.Columns.GetFirstColumn(GridElementStates.Visible | GridElementStates.Frozen);
            while (firstColumn != null)
            {
                cell = this.Cells[firstColumn.Index];
                rect.Width = firstColumn.Thickness;
                if (grid.SingleVerticalBorderAdded && isFirstDisplayedColumn)
                {
                    rect.Width++;
                }
                if (grid.RightToLeftInternal)
                {
                    rect.X = (rowBounds.Right - num) - rect.Width;
                }
                else
                {
                    rect.X = rowBounds.X + num;
                }
                column = grid.Columns.GetNextColumn(firstColumn, GridElementStates.Visible | GridElementStates.Frozen, GridElementStates.None);
                if (clipBounds.IntersectsWith(rect))
                {
                    none = cell.CellStateFromColumnRowStates(rowState);
                    if (base.Index != -1)
                    {
                        none |= cell.State;
                    }
                    cell.GetInheritedStyle(inheritedCellStyle, rowIndex, true);
                    style3 = cell.AdjustCellBorderStyle(grid.AdvancedCellBorderStyle, gridAdvancedBorderStylePlaceholder, grid.SingleVerticalBorderAdded, grid.SingleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
                    cell.PaintWork(graphics, clipBounds, rect, rowIndex, none, inheritedCellStyle, style3, paintParts);
                }
                num += rect.Width;
                if (num >= rowBounds.Width)
                {
                    break;
                }
                firstColumn = column;
                isFirstDisplayedColumn = false;
            }
            Rectangle rectangle2 = rowBounds;
            if ((num < rectangle2.Width) && (grid.FirstDisplayedScrollingColumnIndex >= 0))
            {
                if (!grid.RightToLeftInternal)
                {
                    rectangle2.X -= grid.FirstDisplayedScrollingColumnHiddenWidth;
                }
                rectangle2.Width += grid.FirstDisplayedScrollingColumnHiddenWidth;
                Region clip = null;
                if (grid.FirstDisplayedScrollingColumnHiddenWidth > 0)
                {
                    clip = graphics.Clip;
                    Rectangle rectangle3 = rowBounds;
                    if (!grid.RightToLeftInternal)
                    {
                        rectangle3.X += num;
                    }
                    rectangle3.Width -= num;
                    graphics.SetClip(rectangle3);
                }
                firstColumn = grid.Columns[grid.FirstDisplayedScrollingColumnIndex];
                while (firstColumn != null)
                {
                    cell = this.Cells[firstColumn.Index];
                    rect.Width = firstColumn.Thickness;
                    if (grid.SingleVerticalBorderAdded && isFirstDisplayedColumn)
                    {
                        rect.Width++;
                    }
                    if (grid.RightToLeftInternal)
                    {
                        rect.X = (rectangle2.Right - num) - rect.Width;
                    }
                    else
                    {
                        rect.X = rectangle2.X + num;
                    }
                    column = grid.Columns.GetNextColumn(firstColumn, GridElementStates.Visible, GridElementStates.None);
                    if (clipBounds.IntersectsWith(rect))
                    {
                        none = cell.CellStateFromColumnRowStates(rowState);
                        if (base.Index != -1)
                        {
                            none |= cell.State;
                        }
                        cell.GetInheritedStyle(inheritedCellStyle, rowIndex, true);
                        style3 = cell.AdjustCellBorderStyle(grid.AdvancedCellBorderStyle, gridAdvancedBorderStylePlaceholder, grid.SingleVerticalBorderAdded, grid.SingleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
                        cell.PaintWork(graphics, clipBounds, rect, rowIndex, none, inheritedCellStyle, style3, paintParts);
                    }
                    num += rect.Width;
                    if (num >= rectangle2.Width)
                    {
                        break;
                    }
                    firstColumn = column;
                    isFirstDisplayedColumn = false;
                }
                if (clip != null)
                {
                    graphics.Clip = clip;
                    clip.Dispose();
                }
            }
        }

        /// <summary>Paints the header cell of the current row.</summary>
        /// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle"></see> that contains the bounds of the <see cref="T:MControl.GridView.GridRow"></see> that is being painted.</param>
        /// <param name="rowState">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values that specifies the state of the row.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to paint the <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <param name="isLastVisibleRow">true to indicate that the current row is the last row in the <see cref="T:MControl.GridView.Grid"></see> that has the <see cref="P:MControl.GridView.GridRow.Visible"></see> property set to true; otherwise, false.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be painted.</param>
        /// <param name="paintParts">A bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values indicating the parts of the cells to paint.</param>
        /// <param name="isFirstDisplayedRow">true to indicate that the current row is the first row displayed in the <see cref="T:MControl.GridView.Grid"></see>; otherwise, false.</param>
        /// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">paintParts in not a valid bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual void PaintHeader(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, GridElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, GridPaintParts paintParts)
        {
            if (base.Grid == null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_RowDoesNotYetBelongToGrid"));
            }
            if ((paintParts < GridPaintParts.None) || (paintParts > GridPaintParts.All))
            {
                throw new InvalidEnumArgumentException("paintParts", (int) paintParts, typeof(GridPaintParts));
            }
            Grid grid = base.Grid;
            if (grid.RowHeadersVisible)
            {
                Rectangle rect = rowBounds;
                rect.Width = grid.RowHeadersWidth;
                if (grid.RightToLeftInternal)
                {
                    rect.X = rowBounds.Right - rect.Width;
                }
                if (clipBounds.IntersectsWith(rect))
                {
                    GridCellStyle inheritedCellStyle = new GridCellStyle();
                    GridAdvancedBorderStyle gridAdvancedBorderStylePlaceholder = new GridAdvancedBorderStyle();
                    this.BuildInheritedRowHeaderCellStyle(inheritedCellStyle);
                    GridAdvancedBorderStyle advancedBorderStyle = this.AdjustRowHeaderBorderStyle(grid.AdvancedRowHeadersBorderStyle, gridAdvancedBorderStylePlaceholder, grid.SingleVerticalBorderAdded, grid.SingleHorizontalBorderAdded, isFirstDisplayedRow, isLastVisibleRow);
                    this.HeaderCell.PaintWork(graphics, clipBounds, rect, rowIndex, rowState, inheritedCellStyle, advancedBorderStyle, paintParts);
                }
            }
        }

        internal void SetReadOnlyCellCore(GridCell gridCell, bool readOnly)
        {
            if (this.ReadOnly && !readOnly)
            {
                foreach (GridCell cell in this.Cells)
                {
                    cell.ReadOnlyInternal = true;
                }
                gridCell.ReadOnlyInternal = false;
                this.ReadOnly = false;
            }
            else if (!this.ReadOnly && readOnly)
            {
                gridCell.ReadOnlyInternal = true;
            }
        }

        /// <summary>Sets the values of the row's cells.</summary>
        /// <returns>true if all values have been set; otherwise, false.</returns>
        /// <param name="values">One or more objects that represent the cell values in the row.-or-An <see cref="T:System.Array"></see> of <see cref="T:System.Object"></see> values. </param>
        /// <exception cref="T:System.ArgumentNullException">values is null. </exception>
        /// <exception cref="T:System.InvalidOperationException">This method is called when the associated <see cref="T:MControl.GridView.Grid"></see> is operating in virtual mode. -or-This row is a shared row.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public bool SetValues(params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (base.Grid != null)
            {
                if (base.Grid.VirtualMode)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationInVirtualMode"));
                }
                if (base.Index == -1)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedRow"));
                }
            }
            return this.SetValuesInternal(values);
        }

        internal bool SetValuesInternal(params object[] values)
        {
            bool flag = true;
            GridCellCollection cells = this.Cells;
            int count = cells.Count;
            for (int i = 0; i < cells.Count; i++)
            {
                if (i == values.Length)
                {
                    break;
                }
                if (!cells[i].SetValueInternal(base.Index, values[i]))
                {
                    flag = false;
                }
            }
            return (flag && (values.Length <= count));
        }

        /// <summary>Gets a human-readable string that describes the row.</summary>
        /// <returns>A <see cref="T:System.String"></see> that describes this row.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x24);
            builder.Append("GridRow { Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>Gets the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see> assigned to the <see cref="T:MControl.GridView.GridRow"></see>.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see> assigned to the <see cref="T:MControl.GridView.GridRow"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public AccessibleObject AccessibilityObject
        {
            get
            {
                AccessibleObject obj2 = (AccessibleObject) base.Properties.GetObject(PropRowAccessibilityObject);
                if (obj2 == null)
                {
                    obj2 = this.CreateAccessibilityInstance();
                    base.Properties.SetObject(PropRowAccessibilityObject, obj2);
                }
                return obj2;
            }
        }

        /// <summary>Gets the collection of cells that populate the row.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellCollection"></see> that contains all of the cells in the row.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridCellCollection Cells
        {
            get
            {
                if (this.rowCells == null)
                {
                    this.rowCells = this.CreateCellsInstance();
                }
                return this.rowCells;
            }
        }

        /// <summary>Gets or sets the shortcut menu for the row.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> associated with the current <see cref="T:MControl.GridView.GridRow"></see>. The default is null.</returns>
        /// <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        [Category("Behavior"), Description("Grid_RowContextMenuStrip"), DefaultValue((string) null)]
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

        /// <summary>Gets the data-bound object that populated the row.</summary>
        /// <returns>The data-bound <see cref="T:System.Object"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public object DataBoundItem
        {
            get
            {
                if (((base.Grid != null) && (base.Grid.DataConnection != null)) && ((base.Index > -1) && (base.Index != base.Grid.NewRowIndex)))
                {
                    return base.Grid.DataConnection.CurrencyManager[base.Index];
                }
                return null;
            }
        }

        /// <summary>Gets or sets the default styles for the row, which are used to render cells in the row unless the styles are overridden. </summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCellStyle"></see> to be applied as the default style.</returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(true), Description("Grid_RowDefaultCellStyle"), NotifyParentProperty(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance")]
        public override GridCellStyle DefaultCellStyle
        {
            get
            {
                return base.DefaultCellStyle;
            }
            set
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertySetOnSharedRow", new object[] { "DefaultCellStyle" }));
                }
                base.DefaultCellStyle = value;
            }
        }

        /// <summary>Gets a value indicating whether this row is displayed on the screen.</summary>
        /// <returns>true if the row is currently displayed on the screen; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        [Browsable(false)]
        public override bool Displayed
        {
            get
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertyGetOnSharedRow", new object[] { "Displayed" }));
                }
                return this.GetDisplayed(base.Index);
            }
        }

        /// <summary>Gets or sets the height, in pixels, of the row divider.</summary>
        /// <returns>The height, in pixels, of the divider (the row's bottom margin). </returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        [NotifyParentProperty(true), Category("Appearance"), Description("Grid_RowDividerHeight"), DefaultValue(0)]
        public int DividerHeight
        {
            get
            {
                return base.DividerThickness;
            }
            set
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertySetOnSharedRow", new object[] { "DividerHeight" }));
                }
                base.DividerThickness = value;
            }
        }

        /// <summary>Gets or sets the error message text for row-level errors.</summary>
        /// <returns>A <see cref="T:System.String"></see> containing the error message.</returns>
        /// <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is a shared row in a <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        /// <filterpriority>1</filterpriority>
        [NotifyParentProperty(true), Category("Appearance"), DefaultValue(""), Description("Grid_RowErrorText")]
        public string ErrorText
        {
            get
            {
                return this.GetErrorText(base.Index);
            }
            set
            {
                this.ErrorTextInternal = value;
            }
        }

        private string ErrorTextInternal
        {
            get
            {
                object obj2 = base.Properties.GetObject(PropRowErrorText);
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                string errorTextInternal = this.ErrorTextInternal;
                if (!string.IsNullOrEmpty(value) || base.Properties.ContainsObject(PropRowErrorText))
                {
                    base.Properties.SetObject(PropRowErrorText, value);
                }
                if ((base.Grid != null) && !errorTextInternal.Equals(this.ErrorTextInternal))
                {
                    base.Grid.OnRowErrorTextChanged(this);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the row is frozen. </summary>
        /// <returns>true if the row is frozen; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public override bool Frozen
        {
            get
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertyGetOnSharedRow", new object[] { "Frozen" }));
                }
                return this.GetFrozen(base.Index);
            }
            set
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertySetOnSharedRow", new object[] { "Frozen" }));
                }
                base.Frozen = value;
            }
        }

        internal bool HasErrorText
        {
            get
            {
                return (base.Properties.ContainsObject(PropRowErrorText) && (base.Properties.GetObject(PropRowErrorText) != null));
            }
        }

        /// <summary>Gets or sets the row's header cell.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridRowHeaderCell"></see> that represents the header cell of row.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridRowHeaderCell HeaderCell
        {
            get
            {
                return (GridRowHeaderCell) base.HeaderCellCore;
            }
            set
            {
                base.HeaderCellCore = value;
            }
        }

        /// <summary>Gets or sets the current height of the row.</summary>
        /// <returns>The height, in pixels, of the row. The default is the height of the default font plus 9 pixels.</returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Category("Appearance"), NotifyParentProperty(true), Description("Grid_RowHeight"), DefaultValue(0x16)]
        public int Height
        {
            get
            {
                return base.Thickness;
            }
            set
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertySetOnSharedRow", new object[] { "Height" }));
                }
                base.Thickness = value;
            }
        }

        /// <summary>Gets the cell style in effect for the row.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that specifies the formatting and style information for the cells in the row.</returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        public override GridCellStyle InheritedStyle
        {
            get
            {
                if (base.Index == -1)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertyGetOnSharedRow", new object[] { "InheritedStyle" }));
                }
                GridCellStyle inheritedRowStyle = new GridCellStyle();
                this.BuildInheritedRowStyle(base.Index, inheritedRowStyle);
                return inheritedRowStyle;
            }
        }

        /// <summary>Gets a value indicating whether the row is the row for new records.</summary>
        /// <returns>true if the row is the last row in the <see cref="T:MControl.GridView.Grid"></see>, which is used for the entry of a new row of data; otherwise, false.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNewRow
        {
            get
            {
                return ((base.Grid != null) && (base.Grid.NewRowIndex == base.Index));
            }
        }

        /// <summary>Gets or sets the minimum height of the row.</summary>
        /// <returns>The minimum row height in pixels, ranging from 2 to <see cref="F:System.Int32.MaxValue"></see>. The default is 3.</returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 2.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int MinimumHeight
        {
            get
            {
                return base.MinimumThickness;
            }
            set
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertySetOnSharedRow", new object[] { "MinimumHeight" }));
                }
                base.MinimumThickness = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the row is read-only.</summary>
        /// <returns>true if the row is read-only; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [NotifyParentProperty(true), Description("Grid_RowReadOnly"), DefaultValue(false), Browsable(true), Category("Behavior")]
        public override bool ReadOnly
        {
            get
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertyGetOnSharedRow", new object[] { "ReadOnly" }));
                }
                return this.GetReadOnly(base.Index);
            }
            set
            {
                base.ReadOnly = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether users can resize the row or indicating that the behavior is inherited from the <see cref="P:MControl.GridView.Grid.AllowUserToResizeRows"></see> property.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridTriState"></see> value that indicates whether the row can be resized or whether it can be resized only when the <see cref="P:MControl.GridView.Grid.AllowUserToResizeRows"></see> property is set to true.</returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        [Description("Grid_RowResizable"), Category("Behavior"), NotifyParentProperty(true)]
        public override GridTriState Resizable
        {
            get
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertyGetOnSharedRow", new object[] { "Resizable" }));
                }
                return this.GetResizable(base.Index);
            }
            set
            {
                base.Resizable = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the row is selected. </summary>
        /// <returns>true if the row is selected; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        public override bool Selected
        {
            get
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertyGetOnSharedRow", new object[] { "Selected" }));
                }
                return this.GetSelected(base.Index);
            }
            set
            {
                base.Selected = value;
            }
        }

        /// <summary>Gets the current state of the row.</summary>
        /// <returns>A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values indicating the row state.</returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        public override GridElementStates State
        {
            get
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertyGetOnSharedRow", new object[] { "State" }));
                }
                return this.GetState(base.Index);
            }
        }

        /// <summary>Gets or sets a value indicating whether the row is visible. </summary>
        /// <returns>true if the row is visible; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:MControl.GridView.Grid"></see> control and is a shared row.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public override bool Visible
        {
            get
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertyGetOnSharedRow", new object[] { "Visible" }));
                }
                return this.GetVisible(base.Index);
            }
            set
            {
                if ((base.Grid != null) && (base.Index == -1))
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertySetOnSharedRow", new object[] { "Visible" }));
                }
                base.Visible = value;
            }
        }

        /// <summary>Provides information about a <see cref="T:MControl.GridView.GridRow"></see> to accessibility client applications.</summary>
        [ComVisible(true)]
        protected class GridRowAccessibleObject : AccessibleObject
        {
            private GridRow owner;
            private GridRow.GridSelectedRowCellsAccessibleObject selectedCellsAccessibilityObject;

            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see> class without setting the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property.</summary>
            public GridRowAccessibleObject()
            {
            }

            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see> class, setting the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property to the specified <see cref="T:MControl.GridView.GridRow"></see>.</summary>
            /// <param name="owner">The <see cref="T:MControl.GridView.GridRow"></see> that owns the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see></param>
            public GridRowAccessibleObject(GridRow owner)
            {
                this.owner = owner;
            }

            /// <summary>Returns the accessible child corresponding to the specified index.</summary>
            /// <returns>A <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> that represents the <see cref="T:MControl.GridView.GridCell"></see> corresponding to the specified index.</returns>
            /// <param name="index">The zero-based index of the accessible child.</param>
            /// <exception cref="T:System.InvalidOperationException">index is less than 0.-or-The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            public override AccessibleObject GetChild(int index)
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                }
                if ((index == 0) && this.owner.Grid.RowHeadersVisible)
                {
                    return this.owner.HeaderCell.AccessibilityObject;
                }
                if (this.owner.Grid.RowHeadersVisible)
                {
                    index--;
                }
                int num = this.owner.Grid.Columns.ActualDisplayIndexToColumnIndex(index, GridElementStates.Visible);
                return this.owner.Cells[num].AccessibilityObject;
            }

            /// <summary>Returns the number of children belonging to the accessible object.</summary>
            /// <returns>The number of child accessible objects that belong to the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see> corresponds to the number of visible columns in the <see cref="T:MControl.GridView.Grid"></see>. If the <see cref="P:MControl.GridView.Grid.RowHeadersVisible"></see> property is true, the <see cref="M:MControl.GridView.GridRow.GridRowAccessibleObject.GetChildCount"></see> method includes the <see cref="T:MControl.GridView.GridRowHeaderCell"></see> in the count of child accessible objects.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            public override int GetChildCount()
            {
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                }
                int columnCount = this.owner.Grid.Columns.GetColumnCount(GridElementStates.Visible);
                if (this.owner.Grid.RowHeadersVisible)
                {
                    columnCount++;
                }
                return columnCount;
            }

            /// <summary>Returns the accessible object that has keyboard focus.</summary>
            /// <returns>A <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> if the cell indicated by the <see cref="P:MControl.GridView.Grid.CurrentCell"></see> property has keyboard focus and is in the current <see cref="T:MControl.GridView.GridRow"></see>; otherwise, null.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            public override AccessibleObject GetFocused()
            {
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                }
                if ((this.owner.Grid.Focused && (this.owner.Grid.CurrentCell != null)) && (this.owner.Grid.CurrentCell.RowIndex == this.owner.Index))
                {
                    return this.owner.Grid.CurrentCell.AccessibilityObject;
                }
                return null;
            }

            /// <summary>Gets an accessible object that represents the currently selected <see cref="T:MControl.GridView.GridCell"></see> objects.</summary>
            /// <returns>An accessible object that represents the currently selected <see cref="T:MControl.GridView.GridCell"></see> objects in the <see cref="T:MControl.GridView.GridRow"></see>.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            public override AccessibleObject GetSelected()
            {
                return this.SelectedCellsAccessibilityObject;
            }

            /// <summary>Navigates to another accessible object.</summary>
            /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject"></see> that represents an object in the specified direction.</returns>
            /// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation"></see> values.</param>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
            {
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                }
                switch (navigationDirection)
                {
                    case AccessibleNavigation.Up:
                    case AccessibleNavigation.Previous:
                    {
                        if (this.owner.Index == this.owner.Grid.Rows.GetFirstRow(GridElementStates.Visible))
                        {
                            if (this.owner.Grid.ColumnHeadersVisible)
                            {
                                return this.ParentPrivate.GetChild(0);
                            }
                            return null;
                        }
                        int previousRow = this.owner.Grid.Rows.GetPreviousRow(this.owner.Index, GridElementStates.Visible);
                        int index = this.owner.Grid.Rows.GetRowCount(GridElementStates.Visible, 0, previousRow);
                        if (!this.owner.Grid.ColumnHeadersVisible)
                        {
                            return this.owner.Grid.AccessibilityObject.GetChild(index);
                        }
                        return this.owner.Grid.AccessibilityObject.GetChild(index + 1);
                    }
                    case AccessibleNavigation.Down:
                    case AccessibleNavigation.Next:
                    {
                        if (this.owner.Index == this.owner.Grid.Rows.GetLastRow(GridElementStates.Visible))
                        {
                            return null;
                        }
                        int nextRow = this.owner.Grid.Rows.GetNextRow(this.owner.Index, GridElementStates.Visible);
                        int num2 = this.owner.Grid.Rows.GetRowCount(GridElementStates.Visible, 0, nextRow);
                        if (!this.owner.Grid.ColumnHeadersVisible)
                        {
                            return this.owner.Grid.AccessibilityObject.GetChild(num2);
                        }
                        return this.owner.Grid.AccessibilityObject.GetChild(num2 + 1);
                    }
                    case AccessibleNavigation.FirstChild:
                        if (this.GetChildCount() != 0)
                        {
                            return this.GetChild(0);
                        }
                        return null;

                    case AccessibleNavigation.LastChild:
                    {
                        int childCount = this.GetChildCount();
                        if (childCount != 0)
                        {
                            return this.GetChild(childCount - 1);
                        }
                        return null;
                    }
                }
                return null;
            }

            /// <summary>Modifies the selection or moves the keyboard focus of the accessible object.</summary>
            /// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection"></see> values.</param>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void Select(AccessibleSelection flags)
            {
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                }
                Grid grid = this.owner.Grid;
                if (grid != null)
                {
                    if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
                    {
                        grid.FocusInternal();
                    }
                    if (((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection) && (this.owner.Cells.Count > 0))
                    {
                        if ((grid.CurrentCell != null) && (grid.CurrentCell.OwningColumn != null))
                        {
                            grid.CurrentCell = this.owner.Cells[grid.CurrentCell.OwningColumn.Index];
                        }
                        else
                        {
                            int index = grid.Columns.GetFirstColumn(GridElementStates.Visible).Index;
                            if (index > -1)
                            {
                                grid.CurrentCell = this.owner.Cells[index];
                            }
                        }
                    }
                    if ((((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection) && ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.None)) && ((grid.SelectionMode == GridSelectionMode.FullRowSelect) || (grid.SelectionMode == GridSelectionMode.RowHeaderSelect)))
                    {
                        this.owner.Selected = true;
                    }
                    if (((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection) && ((flags & (AccessibleSelection.AddSelection | AccessibleSelection.TakeSelection)) == AccessibleSelection.None))
                    {
                        this.owner.Selected = false;
                    }
                }
            }

            /// <summary>Gets the location and size of the accessible object.</summary>
            /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the bounds of the accessible object.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            public override Rectangle Bounds
            {
                get
                {
                    Rectangle bounds;
                    if (this.owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                    }
                    if (this.owner.Index < this.owner.Grid.FirstDisplayedScrollingRowIndex)
                    {
                        int num = this.owner.Grid.Rows.GetRowCount(GridElementStates.Visible, 0, this.owner.Index);
                        bounds = this.ParentPrivate.GetChild((num + 1) + 1).Bounds;
                        bounds.Y -= this.owner.Height;
                        bounds.Height = this.owner.Height;
                        return bounds;
                    }
                    if ((this.owner.Index >= this.owner.Grid.FirstDisplayedScrollingRowIndex) && (this.owner.Index < (this.owner.Grid.FirstDisplayedScrollingRowIndex + this.owner.Grid.DisplayedRowCount(true))))
                    {
                        bounds = this.owner.Grid.GetRowDisplayRectangle(this.owner.Index, false);
                        return this.owner.Grid.RectangleToScreen(bounds);
                    }
                    int index = this.owner.Grid.Rows.GetRowCount(GridElementStates.Visible, 0, this.owner.Index);
                    bounds = this.ParentPrivate.GetChild(index).Bounds;
                    bounds.Y += bounds.Height;
                    bounds.Height = this.owner.Height;
                    return bounds;
                }
            }

            /// <summary>Gets the name of the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see>.</summary>
            /// <returns>The name of the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see>.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            public override string Name
            {
                get
                {
                    if (this.owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                    }
                    return MControl.GridView.RM.GetString("Grid_AccRowName", new object[] { this.owner.Index.ToString(CultureInfo.CurrentCulture) });
                }
            }

            /// <summary>Gets or sets the <see cref="T:MControl.GridView.GridRow"></see> to which this <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see> applies.</summary>
            /// <returns>The <see cref="T:MControl.GridView.GridRow"></see> that owns this <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see>.</returns>
            /// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property has already been set.</exception>
            public GridRow Owner
            {
                get
                {
                    return this.owner;
                }
                set
                {
                    if (this.owner != null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerAlreadySet"));
                    }
                    this.owner = value;
                }
            }

            /// <summary>Gets the parent of the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see>.</summary>
            /// <returns>The <see cref="T:MControl.GridView.Grid.GridAccessibleObject"></see> that belongs to the <see cref="T:MControl.GridView.Grid"></see>.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
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
                    if (this.owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                    }
                    return this.owner.Grid.AccessibilityObject;
                }
            }

            /// <summary>Gets the role of the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see>.</summary>
            /// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Row"></see> value.</returns>
            public override AccessibleRole Role
            {
                get
                {
                    return AccessibleRole.Row;
                }
            }

            private AccessibleObject SelectedCellsAccessibilityObject
            {
                get
                {
                    if (this.owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                    }
                    if (this.selectedCellsAccessibilityObject == null)
                    {
                        this.selectedCellsAccessibilityObject = new GridRow.GridSelectedRowCellsAccessibleObject(this.owner);
                    }
                    return this.selectedCellsAccessibilityObject;
                }
            }

            /// <summary>Gets the state of the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see>.</summary>
            /// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates"></see> values. The default is the bitwise combination of the <see cref="F:System.Windows.Forms.AccessibleStates.Selectable"></see> and <see cref="F:System.Windows.Forms.AccessibleStates.Focusable"></see> values.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            public override AccessibleStates State
            {
                get
                {
                    if (this.owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                    }
                    AccessibleStates selectable = AccessibleStates.Selectable;
                    bool flag = true;
                    if (this.owner.Selected)
                    {
                        flag = true;
                    }
                    else
                    {
                        for (int i = 0; i < this.owner.Cells.Count; i++)
                        {
                            if (!this.owner.Cells[i].Selected)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        selectable |= AccessibleStates.Selected;
                    }
                    if (!this.owner.Grid.GetRowDisplayRectangle(this.owner.Index, true).IntersectsWith(this.owner.Grid.ClientRectangle))
                    {
                        selectable |= AccessibleStates.Offscreen;
                    }
                    return selectable;
                }
            }

            /// <summary>Gets the value of the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see>.</summary>
            /// <returns>The value of the <see cref="T:MControl.GridView.GridRow.GridRowAccessibleObject"></see>.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridRow.GridRowAccessibleObject.Owner"></see> property is null.</exception>
            public override string Value
            {
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    if (this.owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridRowAccessibleObject_OwnerNotSet"));
                    }
                    if (this.owner.Grid.AllowUserToAddRows && (this.owner.Index == this.owner.Grid.NewRowIndex))
                    {
                        return MControl.GridView.RM.GetString("Grid_AccRowCreateNew");
                    }
                    StringBuilder builder = new StringBuilder(0x400);
                    int childCount = this.GetChildCount();
                    int num2 = this.owner.Grid.RowHeadersVisible ? 1 : 0;
                    for (int i = num2; i < childCount; i++)
                    {
                        AccessibleObject child = this.GetChild(i);
                        if (child != null)
                        {
                            builder.Append(child.Value);
                        }
                        if (i != (childCount - 1))
                        {
                            builder.Append(";");
                        }
                    }
                    return builder.ToString();
                }
            }
        }

        private class GridSelectedRowCellsAccessibleObject : AccessibleObject
        {
            private GridRow owner;

            internal GridSelectedRowCellsAccessibleObject(GridRow owner)
            {
                this.owner = owner;
            }

            public override AccessibleObject GetChild(int index)
            {
                if (index < this.GetChildCount())
                {
                    int num = -1;
                    for (int i = 1; i < this.owner.AccessibilityObject.GetChildCount(); i++)
                    {
                        if ((this.owner.AccessibilityObject.GetChild(i).State & AccessibleStates.Selected) == AccessibleStates.Selected)
                        {
                            num++;
                        }
                        if (num == index)
                        {
                            return this.owner.AccessibilityObject.GetChild(i);
                        }
                    }
                }
                return null;
            }

            public override int GetChildCount()
            {
                int num = 0;
                for (int i = 1; i < this.owner.AccessibilityObject.GetChildCount(); i++)
                {
                    if ((this.owner.AccessibilityObject.GetChild(i).State & AccessibleStates.Selected) == AccessibleStates.Selected)
                    {
                        num++;
                    }
                }
                return num;
            }

            public override AccessibleObject GetFocused()
            {
                if ((this.owner.Grid.CurrentCell != null) && this.owner.Grid.CurrentCell.Selected)
                {
                    return this.owner.Grid.CurrentCell.AccessibilityObject;
                }
                return null;
            }

            public override AccessibleObject GetSelected()
            {
                return this;
            }

            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
            {
                switch (navigationDirection)
                {
                    case AccessibleNavigation.FirstChild:
                        if (this.GetChildCount() <= 0)
                        {
                            return null;
                        }
                        return this.GetChild(0);

                    case AccessibleNavigation.LastChild:
                        if (this.GetChildCount() <= 0)
                        {
                            return null;
                        }
                        return this.GetChild(this.GetChildCount() - 1);
                }
                return null;
            }

            public override string Name
            {
                get
                {
                    return MControl.GridView.RM.GetString("Grid_AccSelectedRowCellsName");
                }
            }

            public override AccessibleObject Parent
            {
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    return this.owner.AccessibilityObject;
                }
            }

            public override AccessibleRole Role
            {
                get
                {
                    return AccessibleRole.Grouping;
                }
            }

            public override AccessibleStates State
            {
                get
                {
                    return (AccessibleStates.Selectable | AccessibleStates.Selected);
                }
            }

            public override string Value
            {
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    return this.Name;
                }
            }
        }
    }
}

