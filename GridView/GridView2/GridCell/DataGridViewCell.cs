namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Text;
    using System.Windows.Forms;
    using MControl.Util;

    /// <summary>Represents an individual cell in a <see cref="T:MControl.GridView.Grid"></see> control. </summary>
    /// <filterpriority>2</filterpriority>
    [TypeConverter(typeof(GridCellConverter))]
    public abstract class GridCell : GridElement, ICloneable, IDisposable
    {
        private const int GRIDCELL_constrastThreshold = 0x3e8;
        private const byte GRIDCELL_flagAreaNotSet = 0;
        private const byte GRIDCELL_flagDataArea = 1;
        private const byte GRIDCELL_flagErrorArea = 2;
        private const int GRIDCELL_highConstrastThreshold = 0x7d0;
        internal const byte GRIDCELL_iconMarginHeight = 4;
        internal const byte GRIDCELL_iconMarginWidth = 4;
        internal const byte GRIDCELL_iconsHeight = 11;
        internal const byte GRIDCELL_iconsWidth = 12;
        private const int GRIDCELL_maxToolTipCutOff = 0x100;
        private const int GRIDCELL_maxToolTipLength = 0x120;
        private const string GRIDCELL_toolTipEllipsis = "...";
        private const int GRIDCELL_toolTipEllipsisLength = 3;
        private static Bitmap errorBmp = null;
        private byte flags;
        private GridColumn owningColumn;
        private GridRow owningRow;
        private static readonly int PropCellAccessibilityObject = PropertyStore.CreateKey();
        private static readonly int PropCellContextMenuStrip = PropertyStore.CreateKey();
        private static readonly int PropCellErrorText = PropertyStore.CreateKey();
        private static readonly int PropCellStyle = PropertyStore.CreateKey();
        private static readonly int PropCellTag = PropertyStore.CreateKey();
        private static readonly int PropCellToolTipText = PropertyStore.CreateKey();
        internal static readonly int PropCellValue = PropertyStore.CreateKey();
        private static readonly int PropCellValueType = PropertyStore.CreateKey();
        private PropertyStore propertyStore = new PropertyStore();
        private static System.Type stringType = typeof(string);
        private const TextFormatFlags textFormatSupportedFlags = (TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak);

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCell"></see> class. </summary>
        protected GridCell()
        {
            base.StateInternal = GridElementStates.None;
        }

        /// <summary>Modifies the input cell border style according to the specified criteria. </summary>
        /// <returns>The modified <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see>.</returns>
        /// <param name="gridAdvancedBorderStylePlaceholder">A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that is used to store intermediate changes to the cell border style. </param>
        /// <param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false. </param>
        /// <param name="gridAdvancedBorderStyleInput">A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that represents the cell border style to modify.</param>
        /// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false. </param>
        /// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false. </param>
        /// <param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false. </param>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual GridAdvancedBorderStyle AdjustCellBorderStyle(GridAdvancedBorderStyle gridAdvancedBorderStyleInput, GridAdvancedBorderStyle gridAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            switch (gridAdvancedBorderStyleInput.All)
            {
                case GridAdvancedCellBorderStyle.NotSet:
                    if ((base.Grid != null) && (base.Grid.AdvancedCellBorderStyle == gridAdvancedBorderStyleInput))
                    {
                        switch (base.Grid.CellBorderStyle)
                        {
                            case GridCellBorderStyle.SingleVertical:
                                if (base.Grid.RightToLeftInternal)
                                {
                                    gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Single;
                                    gridAdvancedBorderStylePlaceholder.RightInternal = (isFirstDisplayedColumn && singleVerticalBorderAdded) ? GridAdvancedCellBorderStyle.Single : GridAdvancedCellBorderStyle.None;
                                }
                                else
                                {
                                    gridAdvancedBorderStylePlaceholder.LeftInternal = (isFirstDisplayedColumn && singleVerticalBorderAdded) ? GridAdvancedCellBorderStyle.Single : GridAdvancedCellBorderStyle.None;
                                    gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Single;
                                }
                                gridAdvancedBorderStylePlaceholder.TopInternal = GridAdvancedCellBorderStyle.None;
                                gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.None;
                                return gridAdvancedBorderStylePlaceholder;

                            case GridCellBorderStyle.SingleHorizontal:
                                gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.None;
                                gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.None;
                                gridAdvancedBorderStylePlaceholder.TopInternal = (isFirstDisplayedRow && singleHorizontalBorderAdded) ? GridAdvancedCellBorderStyle.Single : GridAdvancedCellBorderStyle.None;
                                gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.Single;
                                return gridAdvancedBorderStylePlaceholder;
                        }
                    }
                    return gridAdvancedBorderStyleInput;

                case GridAdvancedCellBorderStyle.None:
                    return gridAdvancedBorderStyleInput;

                case GridAdvancedCellBorderStyle.Single:
                    if ((base.Grid == null) || !base.Grid.RightToLeftInternal)
                    {
                        gridAdvancedBorderStylePlaceholder.LeftInternal = (isFirstDisplayedColumn && singleVerticalBorderAdded) ? GridAdvancedCellBorderStyle.Single : GridAdvancedCellBorderStyle.None;
                        gridAdvancedBorderStylePlaceholder.RightInternal = GridAdvancedCellBorderStyle.Single;
                        break;
                    }
                    gridAdvancedBorderStylePlaceholder.LeftInternal = GridAdvancedCellBorderStyle.Single;
                    gridAdvancedBorderStylePlaceholder.RightInternal = (isFirstDisplayedColumn && singleVerticalBorderAdded) ? GridAdvancedCellBorderStyle.Single : GridAdvancedCellBorderStyle.None;
                    break;

                case GridAdvancedCellBorderStyle.OutsetPartial:
                    return gridAdvancedBorderStyleInput;

                default:
                    return gridAdvancedBorderStyleInput;
            }
            gridAdvancedBorderStylePlaceholder.TopInternal = (isFirstDisplayedRow && singleHorizontalBorderAdded) ? GridAdvancedCellBorderStyle.Single : GridAdvancedCellBorderStyle.None;
            gridAdvancedBorderStylePlaceholder.BottomInternal = GridAdvancedCellBorderStyle.Single;
            return gridAdvancedBorderStylePlaceholder;
        }

        /// <summary>Returns a <see cref="T:System.Drawing.Rectangle"></see> that represents the widths of all the cell margins. </summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the widths of all the cell margins.</returns>
        /// <param name="advancedBorderStyle">A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that the margins are to be calculated for. </param>
        protected virtual Rectangle BorderWidths(GridAdvancedBorderStyle advancedBorderStyle)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.X = (advancedBorderStyle.Left == GridAdvancedCellBorderStyle.None) ? 0 : 1;
            if ((advancedBorderStyle.Left == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Left == GridAdvancedCellBorderStyle.InsetDouble))
            {
                rectangle.X++;
            }
            rectangle.Y = (advancedBorderStyle.Top == GridAdvancedCellBorderStyle.None) ? 0 : 1;
            if ((advancedBorderStyle.Top == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Top == GridAdvancedCellBorderStyle.InsetDouble))
            {
                rectangle.Y++;
            }
            rectangle.Width = (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.None) ? 0 : 1;
            if ((advancedBorderStyle.Right == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.InsetDouble))
            {
                rectangle.Width++;
            }
            rectangle.Height = (advancedBorderStyle.Bottom == GridAdvancedCellBorderStyle.None) ? 0 : 1;
            if ((advancedBorderStyle.Bottom == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Bottom == GridAdvancedCellBorderStyle.InsetDouble))
            {
                rectangle.Height++;
            }
            if (this.owningColumn != null)
            {
                if ((base.Grid != null) && base.Grid.RightToLeftInternal)
                {
                    rectangle.X += this.owningColumn.DividerWidth;
                }
                else
                {
                    rectangle.Width += this.owningColumn.DividerWidth;
                }
            }
            if (this.owningRow != null)
            {
                rectangle.Height += this.owningRow.DividerHeight;
            }
            return rectangle;
        }

        internal virtual void CacheEditingControl()
        {
        }

        internal GridElementStates CellStateFromColumnRowStates(GridElementStates rowState)
        {
            GridElementStates states = GridElementStates.Selected | GridElementStates.Resizable | GridElementStates.ReadOnly;
            GridElementStates states2 = GridElementStates.Visible | GridElementStates.Frozen | GridElementStates.Displayed;
            GridElementStates states3 = this.owningColumn.State & states;
            states3 |= rowState & states;
            return (states3 | ((this.owningColumn.State & states2) & (rowState & states2)));
        }

        /// <summary>Indicates whether the cell's row will be unshared when the cell is clicked.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">The <see cref="T:MControl.GridView.GridCellEventArgs"></see> containing the data passed to the <see cref="M:MControl.GridView.GridCell.OnClick(MControl.GridView.GridCellEventArgs)"></see> method.</param>
        protected virtual bool ClickUnsharesRow(GridCellEventArgs e)
        {
            return false;
        }

        internal bool ClickUnsharesRowInternal(GridCellEventArgs e)
        {
            return this.ClickUnsharesRow(e);
        }

        /// <summary>Creates an exact copy of this cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual object Clone()
        {
            GridCell gridCell = (GridCell) Activator.CreateInstance(base.GetType());
            this.CloneInternal(gridCell);
            return gridCell;
        }

        internal void CloneInternal(GridCell gridCell)
        {
            if (this.HasValueType)
            {
                gridCell.ValueType = this.ValueType;
            }
            if (this.HasStyle)
            {
                gridCell.Style = new GridCellStyle(this.Style);
            }
            if (this.HasErrorText)
            {
                gridCell.ErrorText = this.ErrorTextInternal;
            }
            if (this.HasToolTipText)
            {
                gridCell.ToolTipText = this.ToolTipTextInternal;
            }
            if (this.ContextMenuStripInternal != null)
            {
                gridCell.ContextMenuStrip = this.ContextMenuStripInternal.Clone();
            }
            gridCell.StateInternal = this.State & ~GridElementStates.Selected;
            gridCell.Tag = this.Tag;
        }

        internal static int ColorDistance(Color color1, Color color2)
        {
            int num = color1.R - color2.R;
            int num2 = color1.G - color2.G;
            int num3 = color1.B - color2.B;
            return (((num * num) + (num2 * num2)) + (num3 * num3));
        }

        internal void ComputeBorderStyleCellStateAndCellBounds(int rowIndex, out GridAdvancedBorderStyle dgvabsEffective, out GridElementStates cellState, out Rectangle cellBounds)
        {
            bool singleVerticalBorderAdded = !base.Grid.RowHeadersVisible && (base.Grid.AdvancedCellBorderStyle.All == GridAdvancedCellBorderStyle.Single);
            bool singleHorizontalBorderAdded = !base.Grid.ColumnHeadersVisible && (base.Grid.AdvancedCellBorderStyle.All == GridAdvancedCellBorderStyle.Single);
            GridAdvancedBorderStyle gridAdvancedBorderStylePlaceholder = new GridAdvancedBorderStyle();
            if ((rowIndex > -1) && (this.OwningColumn != null))
            {
                dgvabsEffective = this.AdjustCellBorderStyle(base.Grid.AdvancedCellBorderStyle, gridAdvancedBorderStylePlaceholder, singleVerticalBorderAdded, singleHorizontalBorderAdded, rowIndex == base.Grid.FirstDisplayedRowIndex, this.ColumnIndex == base.Grid.FirstDisplayedColumnIndex);
                GridElementStates rowState = base.Grid.Rows.GetRowState(rowIndex);
                cellState = this.CellStateFromColumnRowStates(rowState);
                cellState |= this.State;
            }
            else if (this.OwningColumn != null)
            {
                GridColumn lastColumn = base.Grid.Columns.GetLastColumn(GridElementStates.Visible, GridElementStates.None);
                bool isLastVisibleColumn = (lastColumn != null) && (lastColumn.Index == this.ColumnIndex);
                dgvabsEffective = base.Grid.AdjustColumnHeaderBorderStyle(base.Grid.AdvancedColumnHeadersBorderStyle, gridAdvancedBorderStylePlaceholder, this.ColumnIndex == base.Grid.FirstDisplayedColumnIndex, isLastVisibleColumn);
                cellState = this.OwningColumn.State | this.State;
            }
            else if (this.OwningRow != null)
            {
                dgvabsEffective = this.OwningRow.AdjustRowHeaderBorderStyle(base.Grid.AdvancedRowHeadersBorderStyle, gridAdvancedBorderStylePlaceholder, singleVerticalBorderAdded, singleHorizontalBorderAdded, rowIndex == base.Grid.FirstDisplayedRowIndex, rowIndex == base.Grid.Rows.GetLastRow(GridElementStates.Visible));
                cellState = this.OwningRow.GetState(rowIndex) | this.State;
            }
            else
            {
                dgvabsEffective = base.Grid.AdjustedTopLeftHeaderBorderStyle;
                cellState = this.State;
            }
            cellBounds = new Rectangle(new Point(0, 0), this.GetSize(rowIndex));
        }

        internal Rectangle ComputeErrorIconBounds(Rectangle cellValueBounds)
        {
            if ((cellValueBounds.Width >= 20) && (cellValueBounds.Height >= 0x13))
            {
                return new Rectangle(base.Grid.RightToLeftInternal ? (cellValueBounds.Left + 4) : ((cellValueBounds.Right - 4) - 12), cellValueBounds.Y + ((cellValueBounds.Height - 11) / 2), 12, 11);
            }
            return Rectangle.Empty;
        }

        /// <summary>Indicates whether the cell's row will be unshared when the cell's content is clicked.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">The <see cref="T:MControl.GridView.GridCellEventArgs"></see> containing the data passed to the <see cref="M:MControl.GridView.GridCell.OnContentClick(MControl.GridView.GridCellEventArgs)"></see> method.</param>
        protected virtual bool ContentClickUnsharesRow(GridCellEventArgs e)
        {
            return false;
        }

        internal bool ContentClickUnsharesRowInternal(GridCellEventArgs e)
        {
            return this.ContentClickUnsharesRow(e);
        }

        /// <summary>Indicates whether the cell's row will be unshared when the cell's content is double-clicked.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">The <see cref="T:MControl.GridView.GridCellEventArgs"></see> containing the data passed to the <see cref="M:MControl.GridView.GridCell.OnContentDoubleClick(MControl.GridView.GridCellEventArgs)"></see> method.</param>
        protected virtual bool ContentDoubleClickUnsharesRow(GridCellEventArgs e)
        {
            return false;
        }

        internal bool ContentDoubleClickUnsharesRowInternal(GridCellEventArgs e)
        {
            return this.ContentDoubleClickUnsharesRow(e);
        }

        /// <summary>Creates a new accessible object for the <see cref="T:MControl.GridView.GridCell"></see>. </summary>
        /// <returns>A new <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> for the <see cref="T:MControl.GridView.GridCell"></see>. </returns>
        protected virtual AccessibleObject CreateAccessibilityInstance()
        {
            return new GridCellAccessibleObject(this);
        }

        private void DetachContextMenuStrip(object sender, EventArgs e)
        {
            this.ContextMenuStripInternal = null;
        }

        /// <summary>Removes the cell's editing control from the <see cref="T:MControl.GridView.Grid"></see>.</summary>
        /// <exception cref="T:System.InvalidOperationException">This cell is not associated with a <see cref="T:MControl.GridView.Grid"></see>.-or-The <see cref="P:MControl.GridView.Grid.EditingControl"></see> property of the associated <see cref="T:MControl.GridView.Grid"></see> has a value of null. This is the case, for example, when the control is not in edit mode.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void DetachEditingControl()
        {
            Grid grid = base.Grid;
            if ((grid == null) || (grid.EditingControl == null))
            {
                throw new InvalidOperationException();
            }
            if (grid.EditingControl.ParentInternal != null)
            {
                if (grid.EditingControl.ContainsFocus)
                {
                    ContainerControl containerControlInternal = grid.GetContainerControlInternal() as ContainerControl;
                    if ((containerControlInternal != null) && ((grid.EditingControl == containerControlInternal.ActiveControl) || grid.EditingControl.Contains(containerControlInternal.ActiveControl)))
                    {
                        grid.FocusInternal();
                    }
                    else
                    {
                        System.Windows.Forms.UnsafeNativeMethods.SetFocus(new HandleRef(null, IntPtr.Zero));
                    }
                }
                grid.EditingPanel.Controls.Remove(grid.EditingControl);
            }
            if (grid.EditingPanel.ParentInternal != null)
            {
                ((Grid.GridControlCollection) grid.Controls).RemoveInternal(grid.EditingPanel);
            }
            this.CurrentMouseLocation = 0;
        }

        /// <summary>Releases all resources used by the <see cref="T:MControl.GridView.GridCell"></see>. </summary>
        /// <filterpriority>1</filterpriority>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases the unmanaged resources used by the <see cref="T:MControl.GridView.GridCell"></see> and optionally releases the managed resources. </summary>
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

        /// <summary>Indicates whether the cell's row will be unshared when the cell is double-clicked.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">The <see cref="T:MControl.GridView.GridCellEventArgs"></see> containing the data passed to the <see cref="M:MControl.GridView.GridCell.OnDoubleClick(MControl.GridView.GridCellEventArgs)"></see> method.</param>
        protected virtual bool DoubleClickUnsharesRow(GridCellEventArgs e)
        {
            return false;
        }

        internal bool DoubleClickUnsharesRowInternal(GridCellEventArgs e)
        {
            return this.DoubleClickUnsharesRow(e);
        }

        /// <summary>Indicates whether the parent row will be unshared when the focus moves to the cell.</summary>
        /// <returns>true if the row will be unshared; otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        protected virtual bool EnterUnsharesRow(int rowIndex, bool throughMouseClick)
        {
            return false;
        }

        internal bool EnterUnsharesRowInternal(int rowIndex, bool throughMouseClick)
        {
            return this.EnterUnsharesRow(rowIndex, throughMouseClick);
        }

        /// <summary>Releases the unmanaged resources and performs other cleanup operations before the <see cref="T:MControl.GridView.GridCell"></see> is reclaimed by garbage collection.</summary>
        ~GridCell()
        {
            this.Dispose(false);
        }

        internal static void FormatPlainText(string s, bool csv, TextWriter output, ref bool escapeApplied)
        {
            if (s != null)
            {
                int length = s.Length;
                for (int i = 0; i < length; i++)
                {
                    char ch = s[i];
                    switch (ch)
                    {
                        case '\t':
                            if (!csv)
                            {
                                output.Write(' ');
                            }
                            else
                            {
                                output.Write('\t');
                            }
                            break;

                        case '"':
                            if (csv)
                            {
                                output.Write("\"\"");
                                escapeApplied = true;
                            }
                            else
                            {
                                output.Write('"');
                            }
                            break;

                        case ',':
                            if (csv)
                            {
                                escapeApplied = true;
                            }
                            output.Write(',');
                            break;

                        default:
                            output.Write(ch);
                            break;
                    }
                }
                if (escapeApplied)
                {
                    output.Write('"');
                }
            }
        }

        internal static void FormatPlainTextAsHtml(string s, TextWriter output)
        {
            if (s != null)
            {
                int length = s.Length;
                char ch = '\0';
                for (int i = 0; i < length; i++)
                {
                    char ch2 = s[i];
                    switch (ch2)
                    {
                        case '\n':
                            output.Write("<br>");
                            goto Label_0113;

                        case '\r':
                            goto Label_0113;

                        case ' ':
                            if (ch != ' ')
                            {
                                break;
                            }
                            output.Write("&nbsp;");
                            goto Label_0113;

                        case '"':
                            output.Write("&quot;");
                            goto Label_0113;

                        case '&':
                            output.Write("&amp;");
                            goto Label_0113;

                        case '<':
                            output.Write("&lt;");
                            goto Label_0113;

                        case '>':
                            output.Write("&gt;");
                            goto Label_0113;

                        default:
                            if ((ch2 >= '\x00a0') && (ch2 < 'Ā'))
                            {
                                output.Write("&#");
                                output.Write(((int) ch2).ToString(NumberFormatInfo.InvariantInfo));
                                output.Write(';');
                            }
                            else
                            {
                                output.Write(ch2);
                            }
                            goto Label_0113;
                    }
                    output.Write(ch2);
                Label_0113:
                    ch = ch2;
                }
            }
        }

        private static Bitmap GetBitmap(string bitmapName)
        {
            Bitmap bitmap = new Bitmap(typeof(GridCell), bitmapName);
            bitmap.MakeTransparent();
            return bitmap;
        }

        /// <summary>Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard"></see>.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard"></see>.</returns>
        /// <param name="inLastRow">true to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="inFirstRow">true to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="lastCell">true to indicate that the cell is the last column of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="format">The current format string of the cell.</param>
        /// <param name="firstCell">true to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, false.</param>
        /// <param name="rowIndex">The zero-based index of the row containing the cell.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than 0 or greater than or equal to the number of rows in the control.</exception>
        /// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:MControl.GridView.Grid.DataError"></see> event of the <see cref="T:MControl.GridView.Grid"></see> control or the handler set the <see cref="P:MControl.GridView.GridDataErrorEventArgs.ThrowException"></see> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"></see>.</exception>
        /// <exception cref="T:System.InvalidOperationException">The value of the cell's <see cref="P:MControl.GridView.GridElement.Grid"></see> property is null.-or-<see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        protected virtual object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
        {
            if (base.Grid == null)
            {
                return null;
            }
            if ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            GridCellStyle gridCellStyle = this.GetInheritedStyle(null, rowIndex, false);
            object obj2 = null;
            if (base.Grid.IsSharedCellSelected(this, rowIndex))
            {
                obj2 = this.GetEditedFormattedValue(this.GetValue(rowIndex), rowIndex, ref gridCellStyle, GridDataErrorContexts.ClipboardContent | GridDataErrorContexts.Formatting);
            }
            StringBuilder sb = new StringBuilder(0x40);
            if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
            {
                if (firstCell)
                {
                    if (inFirstRow)
                    {
                        sb.Append("<TABLE>");
                    }
                    sb.Append("<TR>");
                }
                sb.Append("<TD>");
                if (obj2 != null)
                {
                    FormatPlainTextAsHtml(obj2.ToString(), new StringWriter(sb, CultureInfo.CurrentCulture));
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
                if ((firstCell && lastCell) && (inFirstRow && inLastRow))
                {
                    sb.Append(obj2.ToString());
                }
                else
                {
                    bool escapeApplied = false;
                    int length = sb.Length;
                    FormatPlainText(obj2.ToString(), csv, new StringWriter(sb, CultureInfo.CurrentCulture), ref escapeApplied);
                    if (escapeApplied)
                    {
                        sb.Insert(length, '"');
                    }
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

        internal object GetClipboardContentInternal(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
        {
            return this.GetClipboardContent(rowIndex, firstCell, lastCell, inFirstRow, inLastRow, format);
        }

        /// <summary>Returns the bounding rectangle that encloses the cell's content area using a default <see cref="T:System.Drawing.Graphics"></see> and cell style currently in effect for the cell.</summary>
        /// <returns>The <see cref="T:System.Drawing.Rectangle"></see> that bounds the cell's contents.</returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified rowIndex is less than 0 or greater than the number of rows in the control minus 1. </exception>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        public Rectangle GetContentBounds(int rowIndex)
        {
            if (base.Grid == null)
            {
                return Rectangle.Empty;
            }
            GridCellStyle cellStyle = this.GetInheritedStyle(null, rowIndex, false);
            return this.GetContentBounds(base.Grid.CachedGraphics, cellStyle, rowIndex);
        }

        /// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics"></see> and cell style.</summary>
        /// <returns>The <see cref="T:System.Drawing.Rectangle"></see> that bounds the cell's contents.</returns>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> to be applied to the cell.</param>
        /// <param name="graphics">The graphics context for the cell.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        protected virtual Rectangle GetContentBounds(Graphics graphics, GridCellStyle cellStyle, int rowIndex)
        {
            return Rectangle.Empty;
        }

        internal System.Windows.Forms.ContextMenuStrip GetContextMenuStrip(int rowIndex)
        {
            System.Windows.Forms.ContextMenuStrip contextMenuStripInternal = this.ContextMenuStripInternal;
            if ((base.Grid == null) || (!base.Grid.VirtualMode && (base.Grid.DataSource == null)))
            {
                return contextMenuStripInternal;
            }
            return base.Grid.OnCellContextMenuStripNeeded(this.ColumnIndex, rowIndex, contextMenuStripInternal);
        }

        internal void GetContrastedPens(Color baseline, ref Pen darkPen, ref Pen lightPen)
        {
            int num = ColorDistance(baseline, SystemColors.ControlDark);
            int num2 = ColorDistance(baseline, SystemColors.ControlLightLight);
            if (SystemInformation.HighContrast)
            {
                if (num < 0x7d0)
                {
                    darkPen = base.Grid.GetCachedPen(ControlPaint.DarkDark(baseline));
                }
                else
                {
                    darkPen = base.Grid.GetCachedPen(SystemColors.ControlDark);
                }
                if (num2 < 0x7d0)
                {
                    lightPen = base.Grid.GetCachedPen(ControlPaint.LightLight(baseline));
                }
                else
                {
                    lightPen = base.Grid.GetCachedPen(SystemColors.ControlLightLight);
                }
            }
            else
            {
                if (num < 0x3e8)
                {
                    darkPen = base.Grid.GetCachedPen(ControlPaint.Dark(baseline));
                }
                else
                {
                    darkPen = base.Grid.GetCachedPen(SystemColors.ControlDark);
                }
                if (num2 < 0x3e8)
                {
                    lightPen = base.Grid.GetCachedPen(ControlPaint.Light(baseline));
                }
                else
                {
                    lightPen = base.Grid.GetCachedPen(SystemColors.ControlLightLight);
                }
            }
        }

        /// <summary>Returns the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed.</summary>
        /// <returns>The current, formatted value of the <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <param name="context">A bitwise combination of <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values that specifies the data error context.</param>
        /// <param name="rowIndex">The row index of the cell.</param>
        /// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:MControl.GridView.Grid.DataError"></see> event of the <see cref="T:MControl.GridView.Grid"></see> control or the handler set the <see cref="P:MControl.GridView.GridDataErrorEventArgs.ThrowException"></see> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"></see>.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified rowIndex is less than 0 or greater than the number of rows in the control minus 1. </exception>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        public object GetEditedFormattedValue(int rowIndex, GridDataErrorContexts context)
        {
            if (base.Grid == null)
            {
                return null;
            }
            GridCellStyle gridCellStyle = this.GetInheritedStyle(null, rowIndex, false);
            return this.GetEditedFormattedValue(this.GetValue(rowIndex), rowIndex, ref gridCellStyle, context);
        }

        internal object GetEditedFormattedValue(object value, int rowIndex, ref GridCellStyle gridCellStyle, GridDataErrorContexts context)
        {
            Point currentCellAddress = base.Grid.CurrentCellAddress;
            if ((this.ColumnIndex == currentCellAddress.X) && (rowIndex == currentCellAddress.Y))
            {
                IGridEditingControl editingControl = (IGridEditingControl) base.Grid.EditingControl;
                if (editingControl != null)
                {
                    return editingControl.GetEditingControlFormattedValue(context);
                }
                IGridEditingCell cell = this as IGridEditingCell;
                if ((cell != null) && base.Grid.IsCurrentCellInEditMode)
                {
                    return cell.GetEditingCellFormattedValue(context);
                }
            }
            return this.GetFormattedValue(value, rowIndex, ref gridCellStyle, null, null, context);
        }

        internal Rectangle GetErrorIconBounds(int rowIndex)
        {
            GridCellStyle cellStyle = this.GetInheritedStyle(null, rowIndex, false);
            return this.GetErrorIconBounds(base.Grid.CachedGraphics, cellStyle, rowIndex);
        }

        /// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
        /// <returns>The <see cref="T:System.Drawing.Rectangle"></see> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty"></see>.</returns>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> to be applied to the cell.</param>
        /// <param name="graphics">The graphics context for the cell.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        protected virtual Rectangle GetErrorIconBounds(Graphics graphics, GridCellStyle cellStyle, int rowIndex)
        {
            return Rectangle.Empty;
        }

        /// <summary>Returns a string that represents the error for the cell.</summary>
        /// <returns>A string that describes the error for the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <param name="rowIndex">The row index of the cell.</param>
        protected internal virtual string GetErrorText(int rowIndex)
        {
            string errorText = string.Empty;
            object obj2 = this.Properties.GetObject(PropCellErrorText);
            if (obj2 != null)
            {
                errorText = (string) obj2;
            }
            else if ((((base.Grid != null) && (rowIndex != -1)) && ((rowIndex != base.Grid.NewRowIndex) && (this.OwningColumn != null))) && (this.OwningColumn.IsDataBound && (base.Grid.DataConnection != null)))
            {
                errorText = base.Grid.DataConnection.GetError(this.OwningColumn.BoundColumnIndex, this.ColumnIndex, rowIndex);
            }
            if (((base.Grid != null) && (base.Grid.VirtualMode || (base.Grid.DataSource != null))) && ((this.ColumnIndex >= 0) && (rowIndex >= 0)))
            {
                errorText = base.Grid.OnCellErrorTextNeeded(this.ColumnIndex, rowIndex, errorText);
            }
            return errorText;
        }

        internal object GetFormattedValue(int rowIndex, ref GridCellStyle cellStyle, GridDataErrorContexts context)
        {
            if (base.Grid == null)
            {
                return null;
            }
            return this.GetFormattedValue(this.GetValue(rowIndex), rowIndex, ref cellStyle, null, null, context);
        }

        /// <summary>Gets the value of the cell as formatted for display. </summary>
        /// <returns>The formatted value of the cell or null if the cell does not belong to a <see cref="T:MControl.GridView.Grid"></see> control.</returns>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> in effect for the cell.</param>
        /// <param name="context">A bitwise combination of <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values describing the context in which the formatted value is needed.</param>
        /// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        /// <param name="value">The value to be formatted. </param>
        /// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
        /// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:MControl.GridView.Grid.DataError"></see> event of the <see cref="T:MControl.GridView.Grid"></see> control or the handler set the <see cref="P:MControl.GridView.GridDataErrorEventArgs.ThrowException"></see> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"></see>.</exception>
        protected virtual object GetFormattedValue(object value, int rowIndex, ref GridCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, GridDataErrorContexts context)
        {
            if (base.Grid == null)
            {
                return null;
            }
            GridCellFormattingEventArgs args = base.Grid.OnCellFormatting(this.ColumnIndex, rowIndex, value, this.FormattedValueType, cellStyle);
            cellStyle = args.CellStyle;
            bool formattingApplied = args.FormattingApplied;
            object obj2 = args.Value;
            bool flag2 = true;
            if ((!formattingApplied && (this.FormattedValueType != null)) && ((obj2 == null) || !this.FormattedValueType.IsAssignableFrom(obj2.GetType())))
            {
                try
                {
                    obj2 = Formatter.FormatObject(obj2, this.FormattedValueType, (valueTypeConverter == null) ? this.ValueTypeConverter : valueTypeConverter, (formattedValueTypeConverter == null) ? this.FormattedValueTypeConverter : formattedValueTypeConverter, cellStyle.Format, cellStyle.FormatProvider, cellStyle.NullValue, cellStyle.DataSourceNullValue);
                }
                catch (Exception exception)
                {
                    if (System.Windows.Forms.ClientUtils.IsCriticalException(exception))
                    {
                        throw;
                    }
                    GridDataErrorEventArgs e = new GridDataErrorEventArgs(exception, this.ColumnIndex, rowIndex, context);
                    base.RaiseDataError(e);
                    if (e.ThrowException)
                    {
                        throw e.Exception;
                    }
                    flag2 = false;
                }
            }
            if (flag2 && (((obj2 == null) || (this.FormattedValueType == null)) || !this.FormattedValueType.IsAssignableFrom(obj2.GetType())))
            {
                if (((obj2 == null) && (cellStyle.NullValue == null)) && ((this.FormattedValueType != null) && !typeof(System.ValueType).IsAssignableFrom(this.FormattedValueType)))
                {
                    return null;
                }
                Exception exception2 = null;
                if (this.FormattedValueType == null)
                {
                    exception2 = new FormatException(MControl.GridView.RM.GetString("GridCell_FormattedValueTypeNull"));
                }
                else
                {
                    exception2 = new FormatException(MControl.GridView.RM.GetString("GridCell_FormattedValueHasWrongType"));
                }
                GridDataErrorEventArgs args3 = new GridDataErrorEventArgs(exception2, this.ColumnIndex, rowIndex, context);
                base.RaiseDataError(args3);
                if (args3.ThrowException)
                {
                    throw args3.Exception;
                }
            }
            return obj2;
        }

        internal static GridFreeDimension GetFreeDimensionFromConstraint(System.Drawing.Size constraintSize)
        {
            if ((constraintSize.Width < 0) || (constraintSize.Height < 0))
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("InvalidArgument", new object[] { "constraintSize", constraintSize.ToString() }));
            }
            if (constraintSize.Width == 0)
            {
                if (constraintSize.Height == 0)
                {
                    return GridFreeDimension.Both;
                }
                return GridFreeDimension.Width;
            }
            if (constraintSize.Height != 0)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("InvalidArgument", new object[] { "constraintSize", constraintSize.ToString() }));
            }
            return GridFreeDimension.Height;
        }

        internal int GetHeight(int rowIndex)
        {
            if (base.Grid == null)
            {
                return -1;
            }
            return this.owningRow.GetHeight(rowIndex);
        }

        /// <summary>Gets the inherited shortcut menu for the current cell.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> if the parent <see cref="T:MControl.GridView.Grid"></see>, <see cref="T:MControl.GridView.GridRow"></see>, or <see cref="T:MControl.GridView.GridColumn"></see> has a <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> assigned; otherwise, null.</returns>
        /// <param name="rowIndex">The row index of the current cell.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:MControl.GridView.GridElement.Grid"></see> property of the cell is not null and the specified rowIndex is less than 0 or greater than the number of rows in the control minus 1. </exception>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        public virtual System.Windows.Forms.ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
        {
            if (base.Grid != null)
            {
                if ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count))
                {
                    throw new ArgumentOutOfRangeException("rowIndex");
                }
                if (this.ColumnIndex < 0)
                {
                    throw new InvalidOperationException();
                }
            }
            System.Windows.Forms.ContextMenuStrip contextMenuStrip = this.GetContextMenuStrip(rowIndex);
            if (contextMenuStrip != null)
            {
                return contextMenuStrip;
            }
            if (this.owningRow != null)
            {
                contextMenuStrip = this.owningRow.GetContextMenuStrip(rowIndex);
                if (contextMenuStrip != null)
                {
                    return contextMenuStrip;
                }
            }
            if (this.owningColumn != null)
            {
                contextMenuStrip = this.owningColumn.ContextMenuStrip;
                if (contextMenuStrip != null)
                {
                    return contextMenuStrip;
                }
            }
            if (base.Grid != null)
            {
                return base.Grid.ContextMenuStrip;
            }
            return null;
        }

        /// <summary>Returns a value indicating the current state of the cell as inherited from the state of its row and column.</summary>
        /// <returns>A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values representing the current state of the cell.</returns>
        /// <param name="rowIndex">The index of the row containing the cell.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:MControl.GridView.Grid"></see> control and rowIndex is outside the valid range of 0 to the number of rows in the control minus 1.</exception>
        /// <exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:MControl.GridView.Grid"></see> control and rowIndex is not -1.-or-rowIndex is not the index of the row containing this cell.</exception>
        public virtual GridElementStates GetInheritedState(int rowIndex)
        {
            GridElementStates states = this.State | GridElementStates.ResizableSet;
            if (base.Grid == null)
            {
                if (rowIndex != -1)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("InvalidArgument", new object[] { "rowIndex", rowIndex.ToString(CultureInfo.CurrentCulture) }));
                }
                if (this.owningRow != null)
                {
                    states |= this.owningRow.GetState(-1) & (GridElementStates.Visible | GridElementStates.Selected | GridElementStates.ReadOnly | GridElementStates.Frozen);
                    if (this.owningRow.GetResizable(rowIndex) == GridTriState.True)
                    {
                        states |= GridElementStates.Resizable;
                    }
                }
                return states;
            }
            if ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if (base.Grid.Rows.SharedRow(rowIndex) != this.owningRow)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("InvalidArgument", new object[] { "rowIndex", rowIndex.ToString(CultureInfo.CurrentCulture) }));
            }
            GridElementStates rowState = base.Grid.Rows.GetRowState(rowIndex);
            states |= rowState & (GridElementStates.Selected | GridElementStates.ReadOnly);
            states |= this.owningColumn.State & (GridElementStates.Selected | GridElementStates.ReadOnly);
            if ((this.owningRow.GetResizable(rowIndex) == GridTriState.True) || (this.owningColumn.Resizable == GridTriState.True))
            {
                states |= GridElementStates.Resizable;
            }
            if (this.owningColumn.Visible && this.owningRow.GetVisible(rowIndex))
            {
                states |= GridElementStates.Visible;
                if (this.owningColumn.Displayed && this.owningRow.GetDisplayed(rowIndex))
                {
                    states |= GridElementStates.Displayed;
                }
            }
            if (this.owningColumn.Frozen && this.owningRow.GetFrozen(rowIndex))
            {
                states |= GridElementStates.Frozen;
            }
            return states;
        }

        /// <summary>Gets the style applied to the cell. </summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that includes the style settings of the cell inherited from the cell's parent row, column, and <see cref="T:MControl.GridView.Grid"></see>.</returns>
        /// <param name="inheritedCellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> to be populated with the inherited cell style. </param>
        /// <param name="includeColors">true to include inherited colors in the returned cell style; otherwise, false. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than 0, or greater than or equal to the number of rows in the parent <see cref="T:MControl.GridView.Grid"></see>.</exception>
        /// <exception cref="T:System.InvalidOperationException">The cell has no associated <see cref="T:MControl.GridView.Grid"></see>.-or-<see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        public virtual GridCellStyle GetInheritedStyle(GridCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
        {
            GridCellStyle placeholderCellStyle;
            if (base.Grid == null)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_CellNeedsGridForInheritedStyle"));
            }
            if ((rowIndex < 0) || (rowIndex >= base.Grid.Rows.Count))
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if (this.ColumnIndex < 0)
            {
                throw new InvalidOperationException();
            }
            if (inheritedCellStyle == null)
            {
                placeholderCellStyle = base.Grid.PlaceholderCellStyle;
                if (!includeColors)
                {
                    placeholderCellStyle.BackColor = Color.Empty;
                    placeholderCellStyle.ForeColor = Color.Empty;
                    placeholderCellStyle.SelectionBackColor = Color.Empty;
                    placeholderCellStyle.SelectionForeColor = Color.Empty;
                }
            }
            else
            {
                placeholderCellStyle = inheritedCellStyle;
            }
            GridCellStyle style = null;
            if (this.HasStyle)
            {
                style = this.Style;
            }
            GridCellStyle style3 = null;
            if (base.Grid.Rows.SharedRow(rowIndex).HasDefaultCellStyle)
            {
                style3 = base.Grid.Rows.SharedRow(rowIndex).DefaultCellStyle;
            }
            GridCellStyle style4 = null;
            if (this.owningColumn.HasDefaultCellStyle)
            {
                style4 = this.owningColumn.DefaultCellStyle;
            }
            GridCellStyle defaultCellStyle = base.Grid.DefaultCellStyle;
            if (includeColors)
            {
                if ((style != null) && !style.BackColor.IsEmpty)
                {
                    placeholderCellStyle.BackColor = style.BackColor;
                }
                else if ((style3 != null) && !style3.BackColor.IsEmpty)
                {
                    placeholderCellStyle.BackColor = style3.BackColor;
                }
                else if (!base.Grid.RowsDefaultCellStyle.BackColor.IsEmpty && (((rowIndex % 2) == 0) || base.Grid.AlternatingRowsDefaultCellStyle.BackColor.IsEmpty))
                {
                    placeholderCellStyle.BackColor = base.Grid.RowsDefaultCellStyle.BackColor;
                }
                else if (((rowIndex % 2) == 1) && !base.Grid.AlternatingRowsDefaultCellStyle.BackColor.IsEmpty)
                {
                    placeholderCellStyle.BackColor = base.Grid.AlternatingRowsDefaultCellStyle.BackColor;
                }
                else if ((style4 != null) && !style4.BackColor.IsEmpty)
                {
                    placeholderCellStyle.BackColor = style4.BackColor;
                }
                else
                {
                    placeholderCellStyle.BackColor = defaultCellStyle.BackColor;
                }
                if ((style != null) && !style.ForeColor.IsEmpty)
                {
                    placeholderCellStyle.ForeColor = style.ForeColor;
                }
                else if ((style3 != null) && !style3.ForeColor.IsEmpty)
                {
                    placeholderCellStyle.ForeColor = style3.ForeColor;
                }
                else if (!base.Grid.RowsDefaultCellStyle.ForeColor.IsEmpty && (((rowIndex % 2) == 0) || base.Grid.AlternatingRowsDefaultCellStyle.ForeColor.IsEmpty))
                {
                    placeholderCellStyle.ForeColor = base.Grid.RowsDefaultCellStyle.ForeColor;
                }
                else if (((rowIndex % 2) == 1) && !base.Grid.AlternatingRowsDefaultCellStyle.ForeColor.IsEmpty)
                {
                    placeholderCellStyle.ForeColor = base.Grid.AlternatingRowsDefaultCellStyle.ForeColor;
                }
                else if ((style4 != null) && !style4.ForeColor.IsEmpty)
                {
                    placeholderCellStyle.ForeColor = style4.ForeColor;
                }
                else
                {
                    placeholderCellStyle.ForeColor = defaultCellStyle.ForeColor;
                }
                if ((style != null) && !style.SelectionBackColor.IsEmpty)
                {
                    placeholderCellStyle.SelectionBackColor = style.SelectionBackColor;
                }
                else if ((style3 != null) && !style3.SelectionBackColor.IsEmpty)
                {
                    placeholderCellStyle.SelectionBackColor = style3.SelectionBackColor;
                }
                else if (!base.Grid.RowsDefaultCellStyle.SelectionBackColor.IsEmpty && (((rowIndex % 2) == 0) || base.Grid.AlternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty))
                {
                    placeholderCellStyle.SelectionBackColor = base.Grid.RowsDefaultCellStyle.SelectionBackColor;
                }
                else if (((rowIndex % 2) == 1) && !base.Grid.AlternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty)
                {
                    placeholderCellStyle.SelectionBackColor = base.Grid.AlternatingRowsDefaultCellStyle.SelectionBackColor;
                }
                else if ((style4 != null) && !style4.SelectionBackColor.IsEmpty)
                {
                    placeholderCellStyle.SelectionBackColor = style4.SelectionBackColor;
                }
                else
                {
                    placeholderCellStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
                }
                if ((style != null) && !style.SelectionForeColor.IsEmpty)
                {
                    placeholderCellStyle.SelectionForeColor = style.SelectionForeColor;
                }
                else if ((style3 != null) && !style3.SelectionForeColor.IsEmpty)
                {
                    placeholderCellStyle.SelectionForeColor = style3.SelectionForeColor;
                }
                else if (!base.Grid.RowsDefaultCellStyle.SelectionForeColor.IsEmpty && (((rowIndex % 2) == 0) || base.Grid.AlternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty))
                {
                    placeholderCellStyle.SelectionForeColor = base.Grid.RowsDefaultCellStyle.SelectionForeColor;
                }
                else if (((rowIndex % 2) == 1) && !base.Grid.AlternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty)
                {
                    placeholderCellStyle.SelectionForeColor = base.Grid.AlternatingRowsDefaultCellStyle.SelectionForeColor;
                }
                else if ((style4 != null) && !style4.SelectionForeColor.IsEmpty)
                {
                    placeholderCellStyle.SelectionForeColor = style4.SelectionForeColor;
                }
                else
                {
                    placeholderCellStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
                }
            }
            if ((style != null) && (style.Font != null))
            {
                placeholderCellStyle.Font = style.Font;
            }
            else if ((style3 != null) && (style3.Font != null))
            {
                placeholderCellStyle.Font = style3.Font;
            }
            else if ((base.Grid.RowsDefaultCellStyle.Font != null) && (((rowIndex % 2) == 0) || (base.Grid.AlternatingRowsDefaultCellStyle.Font == null)))
            {
                placeholderCellStyle.Font = base.Grid.RowsDefaultCellStyle.Font;
            }
            else if (((rowIndex % 2) == 1) && (base.Grid.AlternatingRowsDefaultCellStyle.Font != null))
            {
                placeholderCellStyle.Font = base.Grid.AlternatingRowsDefaultCellStyle.Font;
            }
            else if ((style4 != null) && (style4.Font != null))
            {
                placeholderCellStyle.Font = style4.Font;
            }
            else
            {
                placeholderCellStyle.Font = defaultCellStyle.Font;
            }
            if ((style != null) && !style.IsNullValueDefault)
            {
                placeholderCellStyle.NullValue = style.NullValue;
            }
            else if ((style3 != null) && !style3.IsNullValueDefault)
            {
                placeholderCellStyle.NullValue = style3.NullValue;
            }
            else if (!base.Grid.RowsDefaultCellStyle.IsNullValueDefault && (((rowIndex % 2) == 0) || base.Grid.AlternatingRowsDefaultCellStyle.IsNullValueDefault))
            {
                placeholderCellStyle.NullValue = base.Grid.RowsDefaultCellStyle.NullValue;
            }
            else if (((rowIndex % 2) == 1) && !base.Grid.AlternatingRowsDefaultCellStyle.IsNullValueDefault)
            {
                placeholderCellStyle.NullValue = base.Grid.AlternatingRowsDefaultCellStyle.NullValue;
            }
            else if ((style4 != null) && !style4.IsNullValueDefault)
            {
                placeholderCellStyle.NullValue = style4.NullValue;
            }
            else
            {
                placeholderCellStyle.NullValue = defaultCellStyle.NullValue;
            }
            if ((style != null) && !style.IsDataSourceNullValueDefault)
            {
                placeholderCellStyle.DataSourceNullValue = style.DataSourceNullValue;
            }
            else if ((style3 != null) && !style3.IsDataSourceNullValueDefault)
            {
                placeholderCellStyle.DataSourceNullValue = style3.DataSourceNullValue;
            }
            else if (!base.Grid.RowsDefaultCellStyle.IsDataSourceNullValueDefault && (((rowIndex % 2) == 0) || base.Grid.AlternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault))
            {
                placeholderCellStyle.DataSourceNullValue = base.Grid.RowsDefaultCellStyle.DataSourceNullValue;
            }
            else if (((rowIndex % 2) == 1) && !base.Grid.AlternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault)
            {
                placeholderCellStyle.DataSourceNullValue = base.Grid.AlternatingRowsDefaultCellStyle.DataSourceNullValue;
            }
            else if ((style4 != null) && !style4.IsDataSourceNullValueDefault)
            {
                placeholderCellStyle.DataSourceNullValue = style4.DataSourceNullValue;
            }
            else
            {
                placeholderCellStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
            }
            if ((style != null) && (style.Format.Length != 0))
            {
                placeholderCellStyle.Format = style.Format;
            }
            else if ((style3 != null) && (style3.Format.Length != 0))
            {
                placeholderCellStyle.Format = style3.Format;
            }
            else if ((base.Grid.RowsDefaultCellStyle.Format.Length != 0) && (((rowIndex % 2) == 0) || (base.Grid.AlternatingRowsDefaultCellStyle.Format.Length == 0)))
            {
                placeholderCellStyle.Format = base.Grid.RowsDefaultCellStyle.Format;
            }
            else if (((rowIndex % 2) == 1) && (base.Grid.AlternatingRowsDefaultCellStyle.Format.Length != 0))
            {
                placeholderCellStyle.Format = base.Grid.AlternatingRowsDefaultCellStyle.Format;
            }
            else if ((style4 != null) && (style4.Format.Length != 0))
            {
                placeholderCellStyle.Format = style4.Format;
            }
            else
            {
                placeholderCellStyle.Format = defaultCellStyle.Format;
            }
            if ((style != null) && !style.IsFormatProviderDefault)
            {
                placeholderCellStyle.FormatProvider = style.FormatProvider;
            }
            else if ((style3 != null) && !style3.IsFormatProviderDefault)
            {
                placeholderCellStyle.FormatProvider = style3.FormatProvider;
            }
            else if (!base.Grid.RowsDefaultCellStyle.IsFormatProviderDefault && (((rowIndex % 2) == 0) || base.Grid.AlternatingRowsDefaultCellStyle.IsFormatProviderDefault))
            {
                placeholderCellStyle.FormatProvider = base.Grid.RowsDefaultCellStyle.FormatProvider;
            }
            else if (((rowIndex % 2) == 1) && !base.Grid.AlternatingRowsDefaultCellStyle.IsFormatProviderDefault)
            {
                placeholderCellStyle.FormatProvider = base.Grid.AlternatingRowsDefaultCellStyle.FormatProvider;
            }
            else if ((style4 != null) && !style4.IsFormatProviderDefault)
            {
                placeholderCellStyle.FormatProvider = style4.FormatProvider;
            }
            else
            {
                placeholderCellStyle.FormatProvider = defaultCellStyle.FormatProvider;
            }
            if ((style != null) && (style.Alignment != GridContentAlignment.NotSet))
            {
                placeholderCellStyle.AlignmentInternal = style.Alignment;
            }
            else if ((style3 != null) && (style3.Alignment != GridContentAlignment.NotSet))
            {
                placeholderCellStyle.AlignmentInternal = style3.Alignment;
            }
            else if ((base.Grid.RowsDefaultCellStyle.Alignment != GridContentAlignment.NotSet) && (((rowIndex % 2) == 0) || (base.Grid.AlternatingRowsDefaultCellStyle.Alignment == GridContentAlignment.NotSet)))
            {
                placeholderCellStyle.AlignmentInternal = base.Grid.RowsDefaultCellStyle.Alignment;
            }
            else if (((rowIndex % 2) == 1) && (base.Grid.AlternatingRowsDefaultCellStyle.Alignment != GridContentAlignment.NotSet))
            {
                placeholderCellStyle.AlignmentInternal = base.Grid.AlternatingRowsDefaultCellStyle.Alignment;
            }
            else if ((style4 != null) && (style4.Alignment != GridContentAlignment.NotSet))
            {
                placeholderCellStyle.AlignmentInternal = style4.Alignment;
            }
            else
            {
                placeholderCellStyle.AlignmentInternal = defaultCellStyle.Alignment;
            }
            if ((style != null) && (style.WrapMode != GridTriState.NotSet))
            {
                placeholderCellStyle.WrapModeInternal = style.WrapMode;
            }
            else if ((style3 != null) && (style3.WrapMode != GridTriState.NotSet))
            {
                placeholderCellStyle.WrapModeInternal = style3.WrapMode;
            }
            else if ((base.Grid.RowsDefaultCellStyle.WrapMode != GridTriState.NotSet) && (((rowIndex % 2) == 0) || (base.Grid.AlternatingRowsDefaultCellStyle.WrapMode == GridTriState.NotSet)))
            {
                placeholderCellStyle.WrapModeInternal = base.Grid.RowsDefaultCellStyle.WrapMode;
            }
            else if (((rowIndex % 2) == 1) && (base.Grid.AlternatingRowsDefaultCellStyle.WrapMode != GridTriState.NotSet))
            {
                placeholderCellStyle.WrapModeInternal = base.Grid.AlternatingRowsDefaultCellStyle.WrapMode;
            }
            else if ((style4 != null) && (style4.WrapMode != GridTriState.NotSet))
            {
                placeholderCellStyle.WrapModeInternal = style4.WrapMode;
            }
            else
            {
                placeholderCellStyle.WrapModeInternal = defaultCellStyle.WrapMode;
            }
            if ((style != null) && (style.Tag != null))
            {
                placeholderCellStyle.Tag = style.Tag;
            }
            else if ((style3 != null) && (style3.Tag != null))
            {
                placeholderCellStyle.Tag = style3.Tag;
            }
            else if ((base.Grid.RowsDefaultCellStyle.Tag != null) && (((rowIndex % 2) == 0) || (base.Grid.AlternatingRowsDefaultCellStyle.Tag == null)))
            {
                placeholderCellStyle.Tag = base.Grid.RowsDefaultCellStyle.Tag;
            }
            else if (((rowIndex % 2) == 1) && (base.Grid.AlternatingRowsDefaultCellStyle.Tag != null))
            {
                placeholderCellStyle.Tag = base.Grid.AlternatingRowsDefaultCellStyle.Tag;
            }
            else if ((style4 != null) && (style4.Tag != null))
            {
                placeholderCellStyle.Tag = style4.Tag;
            }
            else
            {
                placeholderCellStyle.Tag = defaultCellStyle.Tag;
            }
            if ((style != null) && (style.Padding != Padding.Empty))
            {
                placeholderCellStyle.PaddingInternal = style.Padding;
                return placeholderCellStyle;
            }
            if ((style3 != null) && (style3.Padding != Padding.Empty))
            {
                placeholderCellStyle.PaddingInternal = style3.Padding;
                return placeholderCellStyle;
            }
            if ((base.Grid.RowsDefaultCellStyle.Padding != Padding.Empty) && (((rowIndex % 2) == 0) || (base.Grid.AlternatingRowsDefaultCellStyle.Padding == Padding.Empty)))
            {
                placeholderCellStyle.PaddingInternal = base.Grid.RowsDefaultCellStyle.Padding;
                return placeholderCellStyle;
            }
            if (((rowIndex % 2) == 1) && (base.Grid.AlternatingRowsDefaultCellStyle.Padding != Padding.Empty))
            {
                placeholderCellStyle.PaddingInternal = base.Grid.AlternatingRowsDefaultCellStyle.Padding;
                return placeholderCellStyle;
            }
            if ((style4 != null) && (style4.Padding != Padding.Empty))
            {
                placeholderCellStyle.PaddingInternal = style4.Padding;
                return placeholderCellStyle;
            }
            placeholderCellStyle.PaddingInternal = defaultCellStyle.Padding;
            return placeholderCellStyle;
        }

        internal GridCellStyle GetInheritedStyleInternal(int rowIndex)
        {
            return this.GetInheritedStyle(null, rowIndex, true);
        }

        internal int GetPreferredHeight(int rowIndex, int width)
        {
            if (base.Grid == null)
            {
                return -1;
            }
            GridCellStyle cellStyle = this.GetInheritedStyle(null, rowIndex, false);
            return this.GetPreferredSize(base.Grid.CachedGraphics, cellStyle, rowIndex, new System.Drawing.Size(width, 0)).Height;
        }

        internal System.Drawing.Size GetPreferredSize(int rowIndex)
        {
            if (base.Grid == null)
            {
                return new System.Drawing.Size(-1, -1);
            }
            GridCellStyle cellStyle = this.GetInheritedStyle(null, rowIndex, false);
            return this.GetPreferredSize(base.Grid.CachedGraphics, cellStyle, rowIndex, System.Drawing.Size.Empty);
        }

        /// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> that represents the preferred size, in pixels, of the cell.</returns>
        /// <param name="cellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the style of the cell.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to draw the cell.</param>
        /// <param name="constraintSize">The cell's maximum allowable size.</param>
        /// <param name="rowIndex">The zero-based row index of the cell.</param>
        protected virtual System.Drawing.Size GetPreferredSize(Graphics graphics, GridCellStyle cellStyle, int rowIndex, System.Drawing.Size constraintSize)
        {
            return new System.Drawing.Size(-1, -1);
        }

        internal static int GetPreferredTextHeight(Graphics g, bool rightToLeft, string text, GridCellStyle cellStyle, int maxWidth, out bool widthTruncated)
        {
            TextFormatFlags flags = GridUtilities.ComputeTextFormatFlagsForCellStyleAlignment(rightToLeft, cellStyle.Alignment, cellStyle.WrapMode);
            if (cellStyle.WrapMode == GridTriState.True)
            {
                return MeasureTextHeight(g, text, cellStyle.Font, maxWidth, flags, out widthTruncated);
            }
            System.Drawing.Size size = MeasureTextSize(g, text, cellStyle.Font, flags);
            widthTruncated = size.Width > maxWidth;
            return size.Height;
        }

        internal int GetPreferredWidth(int rowIndex, int height)
        {
            if (base.Grid == null)
            {
                return -1;
            }
            GridCellStyle cellStyle = this.GetInheritedStyle(null, rowIndex, false);
            return this.GetPreferredSize(base.Grid.CachedGraphics, cellStyle, rowIndex, new System.Drawing.Size(0, height)).Width;
        }

        /// <summary>Gets the size of the cell.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> representing the cell's dimensions.</returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <exception cref="T:System.InvalidOperationException">rowIndex is -1</exception>
        protected virtual System.Drawing.Size GetSize(int rowIndex)
        {
            if (base.Grid == null)
            {
                return new System.Drawing.Size(-1, -1);
            }
            if (rowIndex == -1)
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidPropertyGetOnSharedCell", new object[] { "Size" }));
            }
            return new System.Drawing.Size(this.owningColumn.Thickness, this.owningRow.GetHeight(rowIndex));
        }

        private string GetToolTipText(int rowIndex)
        {
            string toolTipTextInternal = this.ToolTipTextInternal;
            if ((base.Grid == null) || (!base.Grid.VirtualMode && (base.Grid.DataSource == null)))
            {
                return toolTipTextInternal;
            }
            return base.Grid.OnCellToolTipTextNeeded(this.ColumnIndex, rowIndex, toolTipTextInternal);
        }

        /// <summary>Gets the value of the cell. </summary>
        /// <returns>The value contained in the <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:MControl.GridView.GridElement.Grid"></see> property of the cell is not null and rowIndex is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:MControl.GridView.Grid"></see>.</exception>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridElement.Grid"></see> property of the cell is not null and the value of the <see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> property is less than 0, indicating that the cell is a row header cell.</exception>
        protected virtual object GetValue(int rowIndex)
        {
            Grid grid = base.Grid;
            if (grid != null)
            {
                if ((rowIndex < 0) || (rowIndex >= grid.Rows.Count))
                {
                    throw new ArgumentOutOfRangeException("rowIndex");
                }
                if (this.ColumnIndex < 0)
                {
                    throw new InvalidOperationException();
                }
            }
            if ((((grid == null) || ((grid.AllowUserToAddRowsInternal && (rowIndex > -1)) && ((rowIndex == grid.NewRowIndex) && (rowIndex != grid.CurrentCellAddress.Y)))) || ((!grid.VirtualMode && (this.OwningColumn != null)) && !this.OwningColumn.IsDataBound)) || ((rowIndex == -1) || (this.ColumnIndex == -1)))
            {
                return this.Properties.GetObject(PropCellValue);
            }
            if ((this.OwningColumn == null) || !this.OwningColumn.IsDataBound)
            {
                return grid.OnCellValueNeeded(this.ColumnIndex, rowIndex);
            }
            Grid.GridDataConnection dataConnection = grid.DataConnection;
            if (dataConnection == null)
            {
                return null;
            }
            if (dataConnection.CurrencyManager.Count <= rowIndex)
            {
                return this.Properties.GetObject(PropCellValue);
            }
            return dataConnection.GetValue(this.OwningColumn.BoundColumnIndex, this.ColumnIndex, rowIndex);
        }

        internal object GetValueInternal(int rowIndex)
        {
            return this.GetValue(rowIndex);
        }

        /// <summary>Initializes the control used to edit the cell.</summary>
        /// <param name="initialFormattedValue">An <see cref="T:System.Object"></see> that represents the value displayed by the cell when editing is started.</param>
        /// <param name="rowIndex">The zero-based row index of the cell's location.</param>
        /// <param name="gridCellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the style of the cell.</param>
        /// <exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:MControl.GridView.Grid"></see> or if one is present, it does not have an associated editing control. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void InitializeEditingControl(int rowIndex, object initialFormattedValue, GridCellStyle gridCellStyle)
        {
            Grid grid = base.Grid;
            if ((grid == null) || (grid.EditingControl == null))
            {
                throw new InvalidOperationException();
            }
            if (grid.EditingControl.ParentInternal == null)
            {
                grid.EditingControl.CausesValidation = grid.CausesValidation;
                grid.EditingPanel.CausesValidation = grid.CausesValidation;
                grid.EditingControl.Visible = true;
                grid.EditingPanel.Visible = false;
                grid.Controls.Add(grid.EditingPanel);
                grid.EditingPanel.Controls.Add(grid.EditingControl);
            }
        }

        /// <summary>Indicates whether the parent row is unshared if the user presses a key while the focus is on the cell.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return false;
        }

        internal bool KeyDownUnsharesRowInternal(KeyEventArgs e, int rowIndex)
        {
            return this.KeyDownUnsharesRow(e, rowIndex);
        }

        /// <summary>Determines if edit mode should be started based on the given key.</summary>
        /// <returns>true if edit mode should be started; otherwise, false. The default is false.</returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that represents the key that was pressed.</param>
        /// <filterpriority>1</filterpriority>
        public virtual bool KeyEntersEditMode(KeyEventArgs e)
        {
            return false;
        }

        /// <summary>Indicates whether a row will be unshared if a key is pressed while a cell in the row has focus.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs"></see> that contains the event data. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual bool KeyPressUnsharesRow(KeyPressEventArgs e, int rowIndex)
        {
            return false;
        }

        internal bool KeyPressUnsharesRowInternal(KeyPressEventArgs e, int rowIndex)
        {
            return this.KeyPressUnsharesRow(e, rowIndex);
        }

        /// <summary>Indicates whether the parent row is unshared when the user releases a key while the focus is on the cell.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return false;
        }

        internal bool KeyUpUnsharesRowInternal(KeyEventArgs e, int rowIndex)
        {
            return this.KeyUpUnsharesRow(e, rowIndex);
        }

        /// <summary>Indicates whether a row will be unshared when the focus leaves a cell in the row.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        protected virtual bool LeaveUnsharesRow(int rowIndex, bool throughMouseClick)
        {
            return false;
        }

        internal bool LeaveUnsharesRowInternal(int rowIndex, bool throughMouseClick)
        {
            return this.LeaveUnsharesRow(rowIndex, throughMouseClick);
        }

        /// <summary>Gets the height, in pixels, of the specified text, given the specified characteristics.</summary>
        /// <returns>The height, in pixels, of the text.</returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to render the text.</param>
        /// <param name="font">The <see cref="T:System.Drawing.Font"></see> applied to the text.</param>
        /// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values to apply to the text.</param>
        /// <param name="maxWidth">The maximum width of the text.</param>
        /// <param name="text">The text to measure.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">maxWidth is less than 1.</exception>
        /// <exception cref="T:System.ArgumentNullException">graphics is null.-or-font is null.</exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">flags is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags)
        {
            bool flag;
            return MeasureTextHeight(graphics, text, font, maxWidth, flags, out flag);
        }

        /// <summary>Gets the height, in pixels, of the specified text, given the specified characteristics. Also indicates whether the required width is greater than the specified maximum width.</summary>
        /// <returns>The height, in pixels, of the text.</returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to render the text.</param>
        /// <param name="font">The <see cref="T:System.Drawing.Font"></see> applied to the text.</param>
        /// <param name="widthTruncated">Set to true if the required width of the text is greater than maxWidth.</param>
        /// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values to apply to the text.</param>
        /// <param name="maxWidth">The maximum width of the text.</param>
        /// <param name="text">The text to measure.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">maxWidth is less than 1.</exception>
        /// <exception cref="T:System.ArgumentNullException">graphics is null.-or-font is null.</exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">flags is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags, out bool widthTruncated)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            if (maxWidth <= 0)
            {
                object[] args = new object[] { "maxWidth", maxWidth.ToString(CultureInfo.CurrentCulture), 0.ToString(CultureInfo.CurrentCulture) };
                throw new ArgumentOutOfRangeException("maxWidth", MControl.GridView.RM.GetString("InvalidLowBoundArgument", args));
            }
            if (!GridUtilities.ValidTextFormatFlags(flags))
            {
                throw new InvalidEnumArgumentException("flags", (int) flags, typeof(TextFormatFlags));
            }
            flags &= TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
            System.Drawing.Size size = TextRenderer.MeasureText(text, font, new System.Drawing.Size(maxWidth, 0x7fffffff), flags);
            widthTruncated = size.Width > maxWidth;
            return size.Height;
        }

        /// <summary>Gets the ideal height and width of the specified text given the specified characteristics.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> representing the preferred height and width of the text.</returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to render the text.</param>
        /// <param name="font">The <see cref="T:System.Drawing.Font"></see> applied to the text.</param>
        /// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values to apply to the text.</param>
        /// <param name="maxRatio">The maximum width-to-height ratio of the block of text.</param>
        /// <param name="text">The text to measure.</param>
        /// <exception cref="T:System.ArgumentNullException">graphics is null.-or-font is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">maxRatio is less than or equal to 0.</exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">flags is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Drawing.Size MeasureTextPreferredSize(Graphics graphics, string text, Font font, float maxRatio, TextFormatFlags flags)
        {
            System.Drawing.Size size2;
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            if (maxRatio <= 0f)
            {
                throw new ArgumentOutOfRangeException("maxRatio", MControl.GridView.RM.GetString("InvalidLowBoundArgument", new object[] { "maxRatio", maxRatio.ToString(CultureInfo.CurrentCulture), "0.0" }));
            }
            if (!GridUtilities.ValidTextFormatFlags(flags))
            {
                throw new InvalidEnumArgumentException("flags", (int) flags, typeof(TextFormatFlags));
            }
            if (string.IsNullOrEmpty(text))
            {
                return new System.Drawing.Size(0, 0);
            }
            System.Drawing.Size size = MeasureTextSize(graphics, text, font, flags);
            if ((size.Width / size.Height) <= maxRatio)
            {
                return size;
            }
            flags &= TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
            float num = ((((float) (size.Width * size.Width)) / ((float) size.Height)) / maxRatio) * 1.1f;
            do
            {
                size2 = TextRenderer.MeasureText(text, font, new System.Drawing.Size((int) num, 0x7fffffff), flags);
                if (((size2.Width / size2.Height) <= maxRatio) || (size2.Width > ((int) num)))
                {
                    return size2;
                }
                num = size2.Width * 0.9f;
            }
            while (num > 1f);
            return size2;
        }

        /// <summary>Gets the height and width of the specified text given the specified characteristics.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> representing the height and width of the text.</returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to render the text.</param>
        /// <param name="font">The <see cref="T:System.Drawing.Font"></see> applied to the text.</param>
        /// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values to apply to the text.</param>
        /// <param name="text">The text to measure.</param>
        /// <exception cref="T:System.ArgumentNullException">graphics is null.-or-font is null.</exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">flags is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Drawing.Size MeasureTextSize(Graphics graphics, string text, Font font, TextFormatFlags flags)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            if (!GridUtilities.ValidTextFormatFlags(flags))
            {
                throw new InvalidEnumArgumentException("flags", (int) flags, typeof(TextFormatFlags));
            }
            flags &= TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
            return TextRenderer.MeasureText(text, font, new System.Drawing.Size(0x7fffffff, 0x7fffffff), flags);
        }

        /// <summary>Gets the width, in pixels, of the specified text given the specified characteristics.</summary>
        /// <returns>The width, in pixels, of the text.</returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to render the text.</param>
        /// <param name="font">The <see cref="T:System.Drawing.Font"></see> applied to the text.</param>
        /// <param name="maxHeight">The maximum height of the text.</param>
        /// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values to apply to the text.</param>
        /// <param name="text">The text to measure.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">maxHeight is less than 1.</exception>
        /// <exception cref="T:System.ArgumentNullException">graphics is null.-or-font is null.</exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">flags is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"></see>  values.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static int MeasureTextWidth(Graphics graphics, string text, Font font, int maxHeight, TextFormatFlags flags)
        {
            if (maxHeight <= 0)
            {
                object[] args = new object[] { "maxHeight", maxHeight.ToString(CultureInfo.CurrentCulture), 0.ToString(CultureInfo.CurrentCulture) };
                throw new ArgumentOutOfRangeException("maxHeight", MControl.GridView.RM.GetString("InvalidLowBoundArgument", args));
            }
            System.Drawing.Size size = MeasureTextSize(graphics, text, font, flags);
            if ((size.Height >= maxHeight) || ((flags & TextFormatFlags.SingleLine) != TextFormatFlags.GlyphOverhangPadding))
            {
                return size.Width;
            }
            flags &= TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
            int width = size.Width;
            float num2 = width * 0.9f;
            do
            {
                System.Drawing.Size size2 = TextRenderer.MeasureText(text, font, new System.Drawing.Size((int) num2, maxHeight), flags);
                if ((size2.Height > maxHeight) || (size2.Width > ((int) num2)))
                {
                    return width;
                }
                width = (int) num2;
                num2 = size2.Width * 0.9f;
            }
            while (num2 > 1f);
            return width;
        }

        /// <summary>Indicates whether a row will be unshared if the user clicks a mouse button while the pointer is on a cell in the row.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data. </param>
        protected virtual bool MouseClickUnsharesRow(GridCellMouseEventArgs e)
        {
            return false;
        }

        internal bool MouseClickUnsharesRowInternal(GridCellMouseEventArgs e)
        {
            return this.MouseClickUnsharesRow(e);
        }

        /// <summary>Indicates whether a row will be unshared if the user double-clicks a cell in the row.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data.</param>
        protected virtual bool MouseDoubleClickUnsharesRow(GridCellMouseEventArgs e)
        {
            return false;
        }

        internal bool MouseDoubleClickUnsharesRowInternal(GridCellMouseEventArgs e)
        {
            return this.MouseDoubleClickUnsharesRow(e);
        }

        /// <summary>Indicates whether a row will be unshared when the user holds down a mouse button while the pointer is on a cell in the row.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data. </param>
        protected virtual bool MouseDownUnsharesRow(GridCellMouseEventArgs e)
        {
            return false;
        }

        internal bool MouseDownUnsharesRowInternal(GridCellMouseEventArgs e)
        {
            return this.MouseDownUnsharesRow(e);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual bool MouseEnterUnsharesRow(int rowIndex)
        {
            return false;
        }

        internal bool MouseEnterUnsharesRowInternal(int rowIndex)
        {
            return this.MouseEnterUnsharesRow(rowIndex);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual bool MouseLeaveUnsharesRow(int rowIndex)
        {
            return false;
        }

        internal bool MouseLeaveUnsharesRowInternal(int rowIndex)
        {
            return this.MouseLeaveUnsharesRow(rowIndex);
        }

        /// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data. </param>
        protected virtual bool MouseMoveUnsharesRow(GridCellMouseEventArgs e)
        {
            return false;
        }

        internal bool MouseMoveUnsharesRowInternal(GridCellMouseEventArgs e)
        {
            return this.MouseMoveUnsharesRow(e);
        }

        /// <summary>Indicates whether a row will be unshared when the user releases a mouse button while the pointer is on a cell in the row.</summary>
        /// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:MControl.GridView.GridCell"></see> class always returns false.</returns>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data. </param>
        protected virtual bool MouseUpUnsharesRow(GridCellMouseEventArgs e)
        {
            return false;
        }

        internal bool MouseUpUnsharesRowInternal(GridCellMouseEventArgs e)
        {
            return this.MouseUpUnsharesRow(e);
        }

        private void OnCellDataAreaMouseEnterInternal(int rowIndex)
        {
            if (base.Grid.ShowCellToolTips)
            {
                Point currentCellAddress = base.Grid.CurrentCellAddress;
                if (((currentCellAddress.X == -1) || (currentCellAddress.X != this.ColumnIndex)) || ((currentCellAddress.Y != rowIndex) || (base.Grid.EditingControl == null)))
                {
                    string toolTipText = this.GetToolTipText(rowIndex);
                    if (string.IsNullOrEmpty(toolTipText))
                    {
                        if (this.FormattedValueType == stringType)
                        {
                            if ((rowIndex != -1) && (this.OwningColumn != null))
                            {
                                int preferredWidth = this.GetPreferredWidth(rowIndex, this.OwningRow.Height);
                                int preferredHeight = this.GetPreferredHeight(rowIndex, this.OwningColumn.Width);
                                if ((this.OwningColumn.Width < preferredWidth) || (this.OwningRow.Height < preferredHeight))
                                {
                                    GridCellStyle gridCellStyle = this.GetInheritedStyle(null, rowIndex, false);
                                    string str2 = this.GetEditedFormattedValue(this.GetValue(rowIndex), rowIndex, ref gridCellStyle, GridDataErrorContexts.Display) as string;
                                    if (!string.IsNullOrEmpty(str2))
                                    {
                                        toolTipText = TruncateToolTipText(str2);
                                    }
                                }
                            }
                            else if ((((rowIndex != -1) && (this.OwningRow != null)) && ((base.Grid.RowHeadersVisible && (base.Grid.RowHeadersWidth > 0)) && (this.OwningColumn == null))) || (rowIndex == -1))
                            {
                                string str3 = this.GetValue(rowIndex) as string;
                                if (!string.IsNullOrEmpty(str3))
                                {
                                    GridCellStyle cellStyle = this.GetInheritedStyle(null, rowIndex, false);
                                    Rectangle rectangle = this.GetContentBounds(base.Grid.CachedGraphics, cellStyle, rowIndex);
                                    bool widthTruncated = false;
                                    int num3 = 0;
                                    if (rectangle.Width > 0)
                                    {
                                        num3 = GetPreferredTextHeight(base.Grid.CachedGraphics, base.Grid.RightToLeftInternal, str3, cellStyle, rectangle.Width, out widthTruncated);
                                    }
                                    else
                                    {
                                        widthTruncated = true;
                                    }
                                    if ((num3 > rectangle.Height) || widthTruncated)
                                    {
                                        toolTipText = TruncateToolTipText(str3);
                                    }
                                }
                            }
                        }
                    }
                    else if (base.Grid.IsRestricted)
                    {
                        toolTipText = TruncateToolTipText(toolTipText);
                    }
                    if (!string.IsNullOrEmpty(toolTipText))
                    {
                        base.Grid.ActivateToolTip(true, toolTipText, this.ColumnIndex, rowIndex);
                    }
                }
            }
        }

        private void OnCellDataAreaMouseLeaveInternal()
        {
            base.Grid.ActivateToolTip(false, string.Empty, -1, -1);
        }

        private void OnCellErrorAreaMouseEnterInternal(int rowIndex)
        {
            string errorText = this.GetErrorText(rowIndex);
            base.Grid.ActivateToolTip(true, errorText, this.ColumnIndex, rowIndex);
        }

        private void OnCellErrorAreaMouseLeaveInternal()
        {
            base.Grid.ActivateToolTip(false, string.Empty, -1, -1);
        }

        /// <summary>Called when the cell is clicked.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains the event data. </param>
        protected virtual void OnClick(GridCellEventArgs e)
        {
        }

        internal void OnClickInternal(GridCellEventArgs e)
        {
            this.OnClick(e);
        }

        internal void OnCommonChange()
        {
            if (((base.Grid != null) && !base.Grid.IsDisposed) && !base.Grid.Disposing)
            {
                if (this.RowIndex == -1)
                {
                    base.Grid.OnColumnCommonChange(this.ColumnIndex);
                }
                else
                {
                    base.Grid.OnCellCommonChange(this.ColumnIndex, this.RowIndex);
                }
            }
        }

        /// <summary>Called when the cell's contents are clicked.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains the event data. </param>
        protected virtual void OnContentClick(GridCellEventArgs e)
        {
        }

        internal void OnContentClickInternal(GridCellEventArgs e)
        {
            this.OnContentClick(e);
        }

        /// <summary>Called when the cell's contents are double-clicked.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains the event data. </param>
        protected virtual void OnContentDoubleClick(GridCellEventArgs e)
        {
        }

        internal void OnContentDoubleClickInternal(GridCellEventArgs e)
        {
            this.OnContentDoubleClick(e);
        }

        /// <summary>Called when the <see cref="P:MControl.GridView.GridElement.Grid"></see> property of the cell changes.</summary>
        protected override void OnGridChanged()
        {
            if (this.HasStyle)
            {
                if (base.Grid == null)
                {
                    this.Style.RemoveScope(GridCellStyleScopes.Cell);
                }
                else
                {
                    this.Style.AddScope(base.Grid, GridCellStyleScopes.Cell);
                }
            }
            base.OnGridChanged();
        }

        /// <summary>Called when the cell is double-clicked.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains the event data. </param>
        protected virtual void OnDoubleClick(GridCellEventArgs e)
        {
        }

        internal void OnDoubleClickInternal(GridCellEventArgs e)
        {
            this.OnDoubleClick(e);
        }

        /// <summary>Called when the focus moves to a cell.</summary>
        /// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual void OnEnter(int rowIndex, bool throughMouseClick)
        {
        }

        internal void OnEnterInternal(int rowIndex, bool throughMouseClick)
        {
            this.OnEnter(rowIndex, throughMouseClick);
        }

        /// <summary>Called when a character key is pressed while the focus is on a cell.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
        }

        internal void OnKeyDownInternal(KeyEventArgs e, int rowIndex)
        {
            this.OnKeyDown(e, rowIndex);
        }

        /// <summary>Called when a key is pressed while the focus is on a cell.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs"></see> that contains the event data. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual void OnKeyPress(KeyPressEventArgs e, int rowIndex)
        {
        }

        internal void OnKeyPressInternal(KeyPressEventArgs e, int rowIndex)
        {
            this.OnKeyPress(e, rowIndex);
        }

        /// <summary>Called when a character key is released while the focus is on a cell.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data. </param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual void OnKeyUp(KeyEventArgs e, int rowIndex)
        {
        }

        internal void OnKeyUpInternal(KeyEventArgs e, int rowIndex)
        {
            this.OnKeyUp(e, rowIndex);
        }

        /// <summary>Called when the focus moves from a cell.</summary>
        /// <param name="throughMouseClick">true if a user action moved focus from the cell; false if a programmatic operation moved focus from the cell.</param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual void OnLeave(int rowIndex, bool throughMouseClick)
        {
        }

        internal void OnLeaveInternal(int rowIndex, bool throughMouseClick)
        {
            this.OnLeave(rowIndex, throughMouseClick);
        }

        /// <summary>Called when the user clicks a mouse button while the pointer is on a cell.  </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data. </param>
        protected virtual void OnMouseClick(GridCellMouseEventArgs e)
        {
        }

        internal void OnMouseClickInternal(GridCellMouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        /// <summary>Called when the user double-clicks a mouse button while the pointer is on a cell.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data. </param>
        protected virtual void OnMouseDoubleClick(GridCellMouseEventArgs e)
        {
        }

        internal void OnMouseDoubleClickInternal(GridCellMouseEventArgs e)
        {
            this.OnMouseDoubleClick(e);
        }

        /// <summary>Called when the user holds down a mouse button while the pointer is on a cell.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data. </param>
        protected virtual void OnMouseDown(GridCellMouseEventArgs e)
        {
        }

        internal void OnMouseDownInternal(GridCellMouseEventArgs e)
        {
            base.Grid.CellMouseDownInContentBounds = this.GetContentBounds(e.RowIndex).Contains(e.X, e.Y);
            if ((((this.ColumnIndex < 0) || (e.RowIndex < 0)) && base.Grid.ApplyVisualStylesToHeaderCells) || (((this.ColumnIndex >= 0) && (e.RowIndex >= 0)) && base.Grid.ApplyVisualStylesToInnerCells))
            {
                base.Grid.InvalidateCell(this.ColumnIndex, e.RowIndex);
            }
            this.OnMouseDown(e);
        }

        /// <summary>Called when the mouse pointer moves over a cell.</summary>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual void OnMouseEnter(int rowIndex)
        {
        }

        internal void OnMouseEnterInternal(int rowIndex)
        {
            this.OnMouseEnter(rowIndex);
        }

        /// <summary>Called when the mouse pointer leaves the cell.</summary>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        protected virtual void OnMouseLeave(int rowIndex)
        {
        }

        internal void OnMouseLeaveInternal(int rowIndex)
        {
            switch (this.CurrentMouseLocation)
            {
                case 1:
                    this.OnCellDataAreaMouseLeaveInternal();
                    break;

                case 2:
                    this.OnCellErrorAreaMouseLeaveInternal();
                    break;
            }
            this.CurrentMouseLocation = 0;
            this.OnMouseLeave(rowIndex);
        }

        /// <summary>Called when the mouse pointer moves within a cell.</summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data. </param>
        protected virtual void OnMouseMove(GridCellMouseEventArgs e)
        {
        }

        internal void OnMouseMoveInternal(GridCellMouseEventArgs e)
        {
            byte currentMouseLocation = this.CurrentMouseLocation;
            this.UpdateCurrentMouseLocation(e);
            switch (currentMouseLocation)
            {
                case 0:
                    if (this.CurrentMouseLocation != 1)
                    {
                        this.OnCellErrorAreaMouseEnterInternal(e.RowIndex);
                        break;
                    }
                    this.OnCellDataAreaMouseEnterInternal(e.RowIndex);
                    break;

                case 1:
                    if (this.CurrentMouseLocation == 2)
                    {
                        this.OnCellDataAreaMouseLeaveInternal();
                        this.OnCellErrorAreaMouseEnterInternal(e.RowIndex);
                    }
                    break;

                case 2:
                    if (this.CurrentMouseLocation == 1)
                    {
                        this.OnCellErrorAreaMouseLeaveInternal();
                        this.OnCellDataAreaMouseEnterInternal(e.RowIndex);
                    }
                    break;
            }
            this.OnMouseMove(e);
        }

        /// <summary>Called when the user releases a mouse button while the pointer is on a cell. </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> that contains the event data. </param>
        protected virtual void OnMouseUp(GridCellMouseEventArgs e)
        {
        }

        internal void OnMouseUpInternal(GridCellMouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            if ((((this.ColumnIndex < 0) || (e.RowIndex < 0)) && base.Grid.ApplyVisualStylesToHeaderCells) || (((this.ColumnIndex >= 0) && (e.RowIndex >= 0)) && base.Grid.ApplyVisualStylesToInnerCells))
            {
                base.Grid.InvalidateCell(this.ColumnIndex, e.RowIndex);
            }
            if ((e.Button == MouseButtons.Left) && this.GetContentBounds(e.RowIndex).Contains(x, y))
            {
                base.Grid.OnCommonCellContentClick(e.ColumnIndex, e.RowIndex, e.Clicks > 1);
            }
            if (((base.Grid != null) && (e.ColumnIndex < base.Grid.Columns.Count)) && (e.RowIndex < base.Grid.Rows.Count))
            {
                this.OnMouseUp(e);
            }
        }

        /// <summary>Paints the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <param name="formattedValue">The formatted data of the <see cref="T:MControl.GridView.GridCell"></see> that is being painted.</param>
        /// <param name="paintParts">A bitwise combination of the <see cref="T:MControl.GridView.GridPaintParts"></see> values that specifies which parts of the cell need to be painted.</param>
        /// <param name="advancedBorderStyle">A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that contains border styles for the cell that is being painted.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to paint the <see cref="T:MControl.GridView.GridCell"></see>.</param>
        /// <param name="errorText">An error message that is associated with the cell.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be repainted.</param>
        /// <param name="cellState">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values that specifies the state of the cell.</param>
        /// <param name="value">The data of the <see cref="T:MControl.GridView.GridCell"></see> that is being painted.</param>
        /// <param name="cellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that contains formatting and style information about the cell.</param>
        /// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle"></see> that contains the bounds of the <see cref="T:MControl.GridView.GridCell"></see> that is being painted.</param>
        protected virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
        }

        internal static bool PaintBackground(GridPaintParts paintParts)
        {
            return ((paintParts & GridPaintParts.Background) != GridPaintParts.None);
        }

        internal static bool PaintBorder(GridPaintParts paintParts)
        {
            return ((paintParts & GridPaintParts.Border) != GridPaintParts.None);
        }

        /// <summary>Paints the border of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <param name="cellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that contains formatting and style information about the current cell.</param>
        /// <param name="bounds">A <see cref="T:System.Drawing.Rectangle"></see> that contains the area of the border that is being painted.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to paint the border.</param>
        /// <param name="advancedBorderStyle">A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that contains border styles of the border that is being painted.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be repainted.</param>
        protected virtual void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle)
        {
            int num;
            int num2;
            int x;
            int num5;
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if (base.Grid == null)
            {
                return;
            }
            Pen darkPen = null;
            Pen lightPen = null;
            Pen cachedPen = base.Grid.GetCachedPen(cellStyle.BackColor);
            Pen gridPen = base.Grid.GridPen;
            this.GetContrastedPens(cellStyle.BackColor, ref darkPen, ref lightPen);
            int width = (this.owningColumn == null) ? 0 : this.owningColumn.DividerWidth;
            if (width != 0)
            {
                Color controlLightLight;
                if (width > bounds.Width)
                {
                    width = bounds.Width;
                }
                switch (advancedBorderStyle.Right)
                {
                    case GridAdvancedCellBorderStyle.Single:
                        controlLightLight = base.Grid.GridPen.Color;
                        break;

                    case GridAdvancedCellBorderStyle.Inset:
                        controlLightLight = SystemColors.ControlLightLight;
                        break;

                    default:
                        controlLightLight = SystemColors.ControlDark;
                        break;
                }
                graphics.FillRectangle(base.Grid.GetCachedBrush(controlLightLight), base.Grid.RightToLeftInternal ? bounds.X : (bounds.Right - width), bounds.Y, width, bounds.Height);
                if (base.Grid.RightToLeftInternal)
                {
                    bounds.X += width;
                }
                bounds.Width -= width;
                if (bounds.Width <= 0)
                {
                    return;
                }
            }
            width = (this.owningRow == null) ? 0 : this.owningRow.DividerHeight;
            if (width != 0)
            {
                Color color;
                if (width > bounds.Height)
                {
                    width = bounds.Height;
                }
                switch (advancedBorderStyle.Bottom)
                {
                    case GridAdvancedCellBorderStyle.Single:
                        color = base.Grid.GridPen.Color;
                        break;

                    case GridAdvancedCellBorderStyle.Inset:
                        color = SystemColors.ControlLightLight;
                        break;

                    default:
                        color = SystemColors.ControlDark;
                        break;
                }
                graphics.FillRectangle(base.Grid.GetCachedBrush(color), bounds.X, bounds.Bottom - width, bounds.Width, width);
                bounds.Height -= width;
                if (bounds.Height <= 0)
                {
                    return;
                }
            }
            if (advancedBorderStyle.All == GridAdvancedCellBorderStyle.None)
            {
                return;
            }
            switch (advancedBorderStyle.Left)
            {
                case GridAdvancedCellBorderStyle.Single:
                    graphics.DrawLine(gridPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
                    goto Label_0432;

                case GridAdvancedCellBorderStyle.Inset:
                    graphics.DrawLine(darkPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
                    goto Label_0432;

                case GridAdvancedCellBorderStyle.InsetDouble:
                    num = bounds.Y + 1;
                    num2 = bounds.Bottom - 1;
                    if ((advancedBorderStyle.Top == GridAdvancedCellBorderStyle.OutsetPartial) || (advancedBorderStyle.Top == GridAdvancedCellBorderStyle.None))
                    {
                        num--;
                    }
                    if (advancedBorderStyle.Bottom == GridAdvancedCellBorderStyle.OutsetPartial)
                    {
                        num2++;
                    }
                    graphics.DrawLine(lightPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
                    graphics.DrawLine(darkPen, bounds.X + 1, num, bounds.X + 1, num2);
                    goto Label_0432;

                case GridAdvancedCellBorderStyle.Outset:
                    graphics.DrawLine(lightPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
                    goto Label_0432;

                case GridAdvancedCellBorderStyle.OutsetDouble:
                    num = bounds.Y + 1;
                    num2 = bounds.Bottom - 1;
                    if ((advancedBorderStyle.Top == GridAdvancedCellBorderStyle.OutsetPartial) || (advancedBorderStyle.Top == GridAdvancedCellBorderStyle.None))
                    {
                        num--;
                    }
                    if (advancedBorderStyle.Bottom == GridAdvancedCellBorderStyle.OutsetPartial)
                    {
                        num2++;
                    }
                    graphics.DrawLine(darkPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
                    graphics.DrawLine(lightPen, bounds.X + 1, num, bounds.X + 1, num2);
                    goto Label_0432;

                case GridAdvancedCellBorderStyle.OutsetPartial:
                    num = bounds.Y + 2;
                    num2 = bounds.Bottom - 3;
                    if ((advancedBorderStyle.Top != GridAdvancedCellBorderStyle.OutsetDouble) && (advancedBorderStyle.Top != GridAdvancedCellBorderStyle.InsetDouble))
                    {
                        if (advancedBorderStyle.Top == GridAdvancedCellBorderStyle.None)
                        {
                            num--;
                        }
                        break;
                    }
                    num++;
                    break;

                default:
                    goto Label_0432;
            }
            graphics.DrawLine(cachedPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
            graphics.DrawLine(lightPen, bounds.X, num, bounds.X, num2);
        Label_0432:
            switch (advancedBorderStyle.Right)
            {
                case GridAdvancedCellBorderStyle.Single:
                    graphics.DrawLine(gridPen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
                    goto Label_067D;

                case GridAdvancedCellBorderStyle.Inset:
                    graphics.DrawLine(lightPen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
                    goto Label_067D;

                case GridAdvancedCellBorderStyle.InsetDouble:
                    num = bounds.Y + 1;
                    num2 = bounds.Bottom - 1;
                    if ((advancedBorderStyle.Top == GridAdvancedCellBorderStyle.OutsetPartial) || (advancedBorderStyle.Top == GridAdvancedCellBorderStyle.None))
                    {
                        num--;
                    }
                    if ((advancedBorderStyle.Bottom == GridAdvancedCellBorderStyle.OutsetPartial) || (advancedBorderStyle.Bottom == GridAdvancedCellBorderStyle.Inset))
                    {
                        num2++;
                    }
                    graphics.DrawLine(lightPen, bounds.Right - 2, bounds.Y, bounds.Right - 2, bounds.Bottom - 1);
                    graphics.DrawLine(darkPen, bounds.Right - 1, num, bounds.Right - 1, num2);
                    goto Label_067D;

                case GridAdvancedCellBorderStyle.Outset:
                    graphics.DrawLine(darkPen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
                    goto Label_067D;

                case GridAdvancedCellBorderStyle.OutsetDouble:
                    num = bounds.Y + 1;
                    num2 = bounds.Bottom - 1;
                    if ((advancedBorderStyle.Top == GridAdvancedCellBorderStyle.OutsetPartial) || (advancedBorderStyle.Top == GridAdvancedCellBorderStyle.None))
                    {
                        num--;
                    }
                    if (advancedBorderStyle.Bottom == GridAdvancedCellBorderStyle.OutsetPartial)
                    {
                        num2++;
                    }
                    graphics.DrawLine(darkPen, bounds.Right - 2, bounds.Y, bounds.Right - 2, bounds.Bottom - 1);
                    graphics.DrawLine(lightPen, bounds.Right - 1, num, bounds.Right - 1, num2);
                    goto Label_067D;

                case GridAdvancedCellBorderStyle.OutsetPartial:
                    num = bounds.Y + 2;
                    num2 = bounds.Bottom - 3;
                    if ((advancedBorderStyle.Top != GridAdvancedCellBorderStyle.OutsetDouble) && (advancedBorderStyle.Top != GridAdvancedCellBorderStyle.InsetDouble))
                    {
                        if (advancedBorderStyle.Top == GridAdvancedCellBorderStyle.None)
                        {
                            num--;
                        }
                        break;
                    }
                    num++;
                    break;

                default:
                    goto Label_067D;
            }
            graphics.DrawLine(cachedPen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
            graphics.DrawLine(darkPen, bounds.Right - 1, num, bounds.Right - 1, num2);
        Label_067D:
            switch (advancedBorderStyle.Top)
            {
                case GridAdvancedCellBorderStyle.Single:
                    graphics.DrawLine(gridPen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
                    break;

                case GridAdvancedCellBorderStyle.Inset:
                    x = bounds.X;
                    num5 = bounds.Right - 1;
                    if ((advancedBorderStyle.Left == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Left == GridAdvancedCellBorderStyle.InsetDouble))
                    {
                        x++;
                    }
                    if ((advancedBorderStyle.Right == GridAdvancedCellBorderStyle.Inset) || (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.Outset))
                    {
                        num5--;
                    }
                    graphics.DrawLine(darkPen, x, bounds.Y, num5, bounds.Y);
                    break;

                case GridAdvancedCellBorderStyle.InsetDouble:
                    x = bounds.X;
                    if ((advancedBorderStyle.Left != GridAdvancedCellBorderStyle.OutsetPartial) && (advancedBorderStyle.Left != GridAdvancedCellBorderStyle.None))
                    {
                        x++;
                    }
                    num5 = bounds.Right - 2;
                    if ((advancedBorderStyle.Right == GridAdvancedCellBorderStyle.OutsetPartial) || (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.None))
                    {
                        num5++;
                    }
                    graphics.DrawLine(lightPen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
                    graphics.DrawLine(darkPen, x, bounds.Y + 1, num5, bounds.Y + 1);
                    break;

                case GridAdvancedCellBorderStyle.Outset:
                    x = bounds.X;
                    num5 = bounds.Right - 1;
                    if ((advancedBorderStyle.Left == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Left == GridAdvancedCellBorderStyle.InsetDouble))
                    {
                        x++;
                    }
                    if ((advancedBorderStyle.Right == GridAdvancedCellBorderStyle.Inset) || (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.Outset))
                    {
                        num5--;
                    }
                    graphics.DrawLine(lightPen, x, bounds.Y, num5, bounds.Y);
                    break;

                case GridAdvancedCellBorderStyle.OutsetDouble:
                    x = bounds.X;
                    if ((advancedBorderStyle.Left != GridAdvancedCellBorderStyle.OutsetPartial) && (advancedBorderStyle.Left != GridAdvancedCellBorderStyle.None))
                    {
                        x++;
                    }
                    num5 = bounds.Right - 2;
                    if ((advancedBorderStyle.Right == GridAdvancedCellBorderStyle.OutsetPartial) || (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.None))
                    {
                        num5++;
                    }
                    graphics.DrawLine(darkPen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
                    graphics.DrawLine(lightPen, x, bounds.Y + 1, num5, bounds.Y + 1);
                    break;

                case GridAdvancedCellBorderStyle.OutsetPartial:
                    x = bounds.X;
                    num5 = bounds.Right - 1;
                    if (advancedBorderStyle.Left != GridAdvancedCellBorderStyle.None)
                    {
                        x++;
                        if ((advancedBorderStyle.Left == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Left == GridAdvancedCellBorderStyle.InsetDouble))
                        {
                            x++;
                        }
                    }
                    if (advancedBorderStyle.Right != GridAdvancedCellBorderStyle.None)
                    {
                        num5--;
                        if ((advancedBorderStyle.Right == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.InsetDouble))
                        {
                            num5--;
                        }
                    }
                    graphics.DrawLine(cachedPen, x, bounds.Y, num5, bounds.Y);
                    graphics.DrawLine(lightPen, x + 1, bounds.Y, num5 - 1, bounds.Y);
                    break;
            }
            switch (advancedBorderStyle.Bottom)
            {
                case GridAdvancedCellBorderStyle.Single:
                    graphics.DrawLine(gridPen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
                    return;

                case GridAdvancedCellBorderStyle.Inset:
                    num5 = bounds.Right - 1;
                    if (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.InsetDouble)
                    {
                        num5--;
                    }
                    graphics.DrawLine(lightPen, bounds.X, bounds.Bottom - 1, num5, bounds.Bottom - 1);
                    return;

                case GridAdvancedCellBorderStyle.InsetDouble:
                case GridAdvancedCellBorderStyle.OutsetDouble:
                    break;

                case GridAdvancedCellBorderStyle.Outset:
                    x = bounds.X;
                    num5 = bounds.Right - 1;
                    if ((advancedBorderStyle.Right == GridAdvancedCellBorderStyle.InsetDouble) || (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.OutsetDouble))
                    {
                        num5--;
                    }
                    graphics.DrawLine(darkPen, x, bounds.Bottom - 1, num5, bounds.Bottom - 1);
                    return;

                case GridAdvancedCellBorderStyle.OutsetPartial:
                    x = bounds.X;
                    num5 = bounds.Right - 1;
                    if (advancedBorderStyle.Left != GridAdvancedCellBorderStyle.None)
                    {
                        x++;
                        if ((advancedBorderStyle.Left == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Left == GridAdvancedCellBorderStyle.InsetDouble))
                        {
                            x++;
                        }
                    }
                    if (advancedBorderStyle.Right != GridAdvancedCellBorderStyle.None)
                    {
                        num5--;
                        if ((advancedBorderStyle.Right == GridAdvancedCellBorderStyle.OutsetDouble) || (advancedBorderStyle.Right == GridAdvancedCellBorderStyle.InsetDouble))
                        {
                            num5--;
                        }
                    }
                    graphics.DrawLine(cachedPen, x, bounds.Bottom - 1, num5, bounds.Bottom - 1);
                    graphics.DrawLine(darkPen, (int) (x + 1), (int) (bounds.Bottom - 1), (int) (num5 - 1), (int) (bounds.Bottom - 1));
                    break;

                default:
                    return;
            }
        }

        internal static bool PaintContentBackground(GridPaintParts paintParts)
        {
            return ((paintParts & GridPaintParts.ContentBackground) != GridPaintParts.None);
        }

        internal static bool PaintContentForeground(GridPaintParts paintParts)
        {
            return ((paintParts & GridPaintParts.ContentForeground) != GridPaintParts.None);
        }

        internal static bool PaintErrorIcon(GridPaintParts paintParts)
        {
            return ((paintParts & GridPaintParts.ErrorIcon) != GridPaintParts.None);
        }

        private static void PaintErrorIcon(Graphics graphics, Rectangle iconBounds)
        {
            Bitmap errorBitmap = ErrorBitmap;
            if (errorBitmap != null)
            {
                lock (errorBitmap)
                {
                    graphics.DrawImage(errorBitmap, iconBounds, 0, 0, 12, 11, GraphicsUnit.Pixel);
                }
            }
        }

        /// <summary>Paints the error icon of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to paint the border.</param>
        /// <param name="cellValueBounds">The bounding <see cref="T:System.Drawing.Rectangle"></see> that encloses the cell's content area.</param>
        /// <param name="errorText">An error message that is associated with the cell.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be repainted.</param>
        protected virtual void PaintErrorIcon(Graphics graphics, Rectangle clipBounds, Rectangle cellValueBounds, string errorText)
        {
            if ((!string.IsNullOrEmpty(errorText) && (cellValueBounds.Width >= 20)) && (cellValueBounds.Height >= 0x13))
            {
                PaintErrorIcon(graphics, this.ComputeErrorIconBounds(cellValueBounds));
            }
        }

        internal void PaintErrorIcon(Graphics graphics, GridCellStyle cellStyle, int rowIndex, Rectangle cellBounds, Rectangle cellValueBounds, string errorText)
        {
            if ((!string.IsNullOrEmpty(errorText) && (cellValueBounds.Width >= 20)) && (cellValueBounds.Height >= 0x13))
            {
                Rectangle iconBounds = this.GetErrorIconBounds(graphics, cellStyle, rowIndex);
                if ((iconBounds.Width >= 4) && (iconBounds.Height >= 11))
                {
                    iconBounds.X += cellBounds.X;
                    iconBounds.Y += cellBounds.Y;
                    PaintErrorIcon(graphics, iconBounds);
                }
            }
        }

        internal static bool PaintFocus(GridPaintParts paintParts)
        {
            return ((paintParts & GridPaintParts.Focus) != GridPaintParts.None);
        }

        internal void PaintInternal(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            this.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }

        internal static void PaintPadding(Graphics graphics, Rectangle bounds, GridCellStyle cellStyle, Brush br, bool rightToLeft)
        {
            Rectangle rectangle;
            if (rightToLeft)
            {
                rectangle = new Rectangle(bounds.X, bounds.Y, cellStyle.Padding.Right, bounds.Height);
                graphics.FillRectangle(br, rectangle);
                rectangle.X = bounds.Right - cellStyle.Padding.Left;
                rectangle.Width = cellStyle.Padding.Left;
                graphics.FillRectangle(br, rectangle);
                rectangle.X = bounds.Left + cellStyle.Padding.Right;
            }
            else
            {
                rectangle = new Rectangle(bounds.X, bounds.Y, cellStyle.Padding.Left, bounds.Height);
                graphics.FillRectangle(br, rectangle);
                rectangle.X = bounds.Right - cellStyle.Padding.Right;
                rectangle.Width = cellStyle.Padding.Right;
                graphics.FillRectangle(br, rectangle);
                rectangle.X = bounds.Left + cellStyle.Padding.Left;
            }
            rectangle.Y = bounds.Y;
            rectangle.Width = bounds.Width - cellStyle.Padding.Horizontal;
            rectangle.Height = cellStyle.Padding.Top;
            graphics.FillRectangle(br, rectangle);
            rectangle.Y = bounds.Bottom - cellStyle.Padding.Bottom;
            rectangle.Height = cellStyle.Padding.Bottom;
            graphics.FillRectangle(br, rectangle);
        }

        internal static bool PaintSelectionBackground(GridPaintParts paintParts)
        {
            return ((paintParts & GridPaintParts.SelectionBackground) != GridPaintParts.None);
        }

        internal void PaintWork(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates cellState, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            object obj2;
            Grid grid = base.Grid;
            int columnIndex = this.ColumnIndex;
            object obj3 = this.GetValue(rowIndex);
            string errorText = this.GetErrorText(rowIndex);
            if ((columnIndex > -1) && (rowIndex > -1))
            {
                obj2 = this.GetEditedFormattedValue(obj3, rowIndex, ref cellStyle, GridDataErrorContexts.Display | GridDataErrorContexts.Formatting);
            }
            else
            {
                obj2 = obj3;
            }
            GridCellPaintingEventArgs cellPaintingEventArgs = grid.CellPaintingEventArgs;
            cellPaintingEventArgs.SetProperties(graphics, clipBounds, cellBounds, rowIndex, columnIndex, cellState, obj3, obj2, errorText, cellStyle, advancedBorderStyle, paintParts);
            grid.OnCellPainting(cellPaintingEventArgs);
            if (!cellPaintingEventArgs.Handled)
            {
                this.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, obj3, obj2, errorText, cellStyle, advancedBorderStyle, paintParts);
            }
        }

        /// <summary>Converts a value formatted for display to an actual cell value.</summary>
        /// <returns>The cell value.</returns>
        /// <param name="formattedValue">The display value of the cell.</param>
        /// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> for the display value type, or null to use the default converter.</param>
        /// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> for the cell value type, or null to use the default converter.</param>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> in effect for the cell.</param>
        /// <exception cref="T:System.FormatException">The <see cref="P:MControl.GridView.GridCell.FormattedValueType"></see> property value is null.-or-The <see cref="P:MControl.GridView.GridCell.ValueType"></see> property value is null.-or-formattedValue cannot be converted.</exception>
        /// <exception cref="T:System.ArgumentNullException">cellStyle is null.</exception>
        /// <exception cref="T:System.ArgumentException">formattedValue is null.-or-The type of formattedValue does not match the type indicated by the <see cref="P:MControl.GridView.GridCell.FormattedValueType"></see> property. </exception>
        public virtual object ParseFormattedValue(object formattedValue, GridCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            return this.ParseFormattedValueInternal(this.ValueType, formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
        }

        internal object ParseFormattedValueInternal(System.Type valueType, object formattedValue, GridCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if (this.FormattedValueType == null)
            {
                throw new FormatException(MControl.GridView.RM.GetString("GridCell_FormattedValueTypeNull"));
            }
            if (valueType == null)
            {
                throw new FormatException(MControl.GridView.RM.GetString("GridCell_ValueTypeNull"));
            }
            if ((formattedValue == null) || !this.FormattedValueType.IsAssignableFrom(formattedValue.GetType()))
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("GridCell_FormattedValueHasWrongType"), "formattedValue");
            }
            return Formatter.ParseObject(formattedValue, valueType, this.FormattedValueType, (valueTypeConverter == null) ? this.ValueTypeConverter : valueTypeConverter, (formattedValueTypeConverter == null) ? this.FormattedValueTypeConverter : formattedValueTypeConverter, cellStyle.FormatProvider, cellStyle.NullValue, cellStyle.IsDataSourceNullValueDefault ? Formatter.GetDefaultDataSourceNullValue(valueType) : cellStyle.DataSourceNullValue);
        }

        /// <summary>Sets the location and size of the editing control hosted by a cell in the <see cref="T:MControl.GridView.Grid"></see> control. </summary>
        /// <param name="cellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the style of the cell being edited.</param>
        /// <param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false.</param>
        /// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param>
        /// <param name="setSize">true to specify the size; false to allow the control to size itself. </param>
        /// <param name="setLocation">true to have the control placed as specified by the other arguments; false to allow the control to place itself.</param>
        /// <param name="cellClip">The area that will be used to paint the editing control.</param>
        /// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param>
        /// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle"></see> that defines the cell bounds. </param>
        /// <param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false.</param>
        /// <exception cref="T:System.InvalidOperationException">The cell is not contained within a <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, GridCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            Rectangle rectangle = this.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
            if (setLocation)
            {
                base.Grid.EditingControl.Location = new Point(rectangle.X, rectangle.Y);
            }
            if (setSize)
            {
                base.Grid.EditingControl.Size = new System.Drawing.Size(rectangle.Width, rectangle.Height);
            }
        }

        /// <summary>Sets the location and size of the editing panel hosted by the cell, and returns the normal bounds of the editing control within the editing panel.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the normal bounds of the editing control within the editing panel.</returns>
        /// <param name="cellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the style of the cell being edited.</param>
        /// <param name="isFirstDisplayedColumn">true if the cell is in the first column currently displayed in the control; otherwise, false.</param>
        /// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param>
        /// <param name="cellClip">The area that will be used to paint the editing panel.</param>
        /// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param>
        /// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle"></see> that defines the cell bounds. </param>
        /// <param name="isFirstDisplayedRow">true if the cell is in the first row currently displayed in the control; otherwise, false.</param>
        /// <exception cref="T:System.InvalidOperationException">The cell has not been added to a <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, GridCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            int num;
            int num5;
            if (base.Grid == null)
            {
                throw new InvalidOperationException();
            }
            GridAdvancedBorderStyle gridAdvancedBorderStylePlaceholder = new GridAdvancedBorderStyle();
            GridAdvancedBorderStyle advancedBorderStyle = this.AdjustCellBorderStyle(base.Grid.AdvancedCellBorderStyle, gridAdvancedBorderStylePlaceholder, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
            Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
            rectangle.X += cellStyle.Padding.Left;
            rectangle.Y += cellStyle.Padding.Top;
            rectangle.Width += cellStyle.Padding.Right;
            rectangle.Height += cellStyle.Padding.Bottom;
            int width = cellBounds.Width;
            int height = cellBounds.Height;
            if ((cellClip.X - cellBounds.X) >= rectangle.X)
            {
                num = cellClip.X;
                width -= cellClip.X - cellBounds.X;
            }
            else
            {
                num = cellBounds.X + rectangle.X;
                width -= rectangle.X;
            }
            if (cellClip.Right <= (cellBounds.Right - rectangle.Width))
            {
                width -= cellBounds.Right - cellClip.Right;
            }
            else
            {
                width -= rectangle.Width;
            }
            int x = cellBounds.X - cellClip.X;
            int num4 = (cellBounds.Width - rectangle.X) - rectangle.Width;
            if ((cellClip.Y - cellBounds.Y) >= rectangle.Y)
            {
                num5 = cellClip.Y;
                height -= cellClip.Y - cellBounds.Y;
            }
            else
            {
                num5 = cellBounds.Y + rectangle.Y;
                height -= rectangle.Y;
            }
            if (cellClip.Bottom <= (cellBounds.Bottom - rectangle.Height))
            {
                height -= cellBounds.Bottom - cellClip.Bottom;
            }
            else
            {
                height -= rectangle.Height;
            }
            int y = cellBounds.Y - cellClip.Y;
            int num8 = (cellBounds.Height - rectangle.Y) - rectangle.Height;
            base.Grid.EditingPanel.Location = new Point(num, num5);
            base.Grid.EditingPanel.Size = new System.Drawing.Size(width, height);
            return new Rectangle(x, y, num4, num8);
        }

        /// <summary>Sets the value of the cell. </summary>
        /// <returns>true if the value has been set; otherwise, false.</returns>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        /// <param name="value">The cell value to set. </param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:MControl.GridView.Grid"></see>.</exception>
        protected virtual bool SetValue(int rowIndex, object value)
        {
            object obj2 = null;
            Grid grid = base.Grid;
            if ((grid != null) && !grid.InSortOperation)
            {
                obj2 = this.GetValue(rowIndex);
            }
            if (((grid != null) && (this.OwningColumn != null)) && this.OwningColumn.IsDataBound)
            {
                Grid.GridDataConnection dataConnection = grid.DataConnection;
                if (dataConnection == null)
                {
                    return false;
                }
                if (dataConnection.CurrencyManager.Count <= rowIndex)
                {
                    if ((value != null) || this.Properties.ContainsObject(PropCellValue))
                    {
                        this.Properties.SetObject(PropCellValue, value);
                    }
                }
                else
                {
                    if (!dataConnection.PushValue(this.OwningColumn.BoundColumnIndex, this.ColumnIndex, rowIndex, value))
                    {
                        return false;
                    }
                    if (((base.Grid == null) || (this.OwningRow == null)) || (this.OwningRow.Grid == null))
                    {
                        return true;
                    }
                    if (this.OwningRow.Index == base.Grid.CurrentCellAddress.Y)
                    {
                        base.Grid.IsCurrentRowDirtyInternal = true;
                    }
                }
            }
            else if (((grid == null) || !grid.VirtualMode) || ((rowIndex == -1) || (this.ColumnIndex == -1)))
            {
                if ((value != null) || this.Properties.ContainsObject(PropCellValue))
                {
                    this.Properties.SetObject(PropCellValue, value);
                }
            }
            else
            {
                grid.OnCellValuePushed(this.ColumnIndex, rowIndex, value);
            }
            if (((grid != null) && !grid.InSortOperation) && ((((obj2 == null) && (value != null)) || ((obj2 != null) && (value == null))) || ((obj2 != null) && !value.Equals(obj2))))
            {
                base.RaiseCellValueChanged(new GridCellEventArgs(this.ColumnIndex, rowIndex));
            }
            return true;
        }

        internal bool SetValueInternal(int rowIndex, object value)
        {
            return this.SetValue(rowIndex, value);
        }

        internal static bool TextFitsInBounds(Graphics graphics, string text, Font font, System.Drawing.Size maxBounds, TextFormatFlags flags)
        {
            bool flag;
            return ((MeasureTextHeight(graphics, text, font, maxBounds.Width, flags, out flag) <= maxBounds.Height) && !flag);
        }

        /// <summary>Returns a string that describes the current object. </summary>
        /// <returns>A string that represents the current object.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridCell { ColumnIndex=" + this.ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + this.RowIndex.ToString(CultureInfo.CurrentCulture) + " }");
        }

        private static string TruncateToolTipText(string toolTipText)
        {
            if (toolTipText.Length > 0x120)
            {
                StringBuilder builder = new StringBuilder(toolTipText.Substring(0, 0x100), 0x103);
                builder.Append("...");
                return builder.ToString();
            }
            return toolTipText;
        }

        private void UpdateCurrentMouseLocation(GridCellMouseEventArgs e)
        {
            if (this.GetErrorIconBounds(e.RowIndex).Contains(e.X, e.Y))
            {
                this.CurrentMouseLocation = 2;
            }
            else
            {
                this.CurrentMouseLocation = 1;
            }
        }

        /// <summary>Gets the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> assigned to the <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> assigned to the <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public AccessibleObject AccessibilityObject
        {
            get
            {
                AccessibleObject obj2 = (AccessibleObject) this.Properties.GetObject(PropCellAccessibilityObject);
                if (obj2 == null)
                {
                    obj2 = this.CreateAccessibilityInstance();
                    this.Properties.SetObject(PropCellAccessibilityObject, obj2);
                }
                return obj2;
            }
        }

        /// <summary>Gets the column index for this cell. </summary>
        /// <returns>The index of the column that contains the cell; -1 if the cell is not contained within a column.</returns>
        /// <filterpriority>1</filterpriority>
        public int ColumnIndex
        {
            get
            {
                if (this.owningColumn == null)
                {
                    return -1;
                }
                return this.owningColumn.Index;
            }
        }

        /// <summary>Gets the bounding rectangle that encloses the cell's content area.</summary>
        /// <returns>The <see cref="T:System.Drawing.Rectangle"></see> that bounds the cell's contents.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> property is less than 0, indicating that the cell is a row header cell.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public Rectangle ContentBounds
        {
            get
            {
                return this.GetContentBounds(this.RowIndex);
            }
        }

        /// <summary>Gets or sets the shortcut menu associated with the cell. </summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> associated with the cell.</returns>
        [DefaultValue((string) null)]
        public virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return this.GetContextMenuStrip(this.RowIndex);
            }
            set
            {
                this.ContextMenuStripInternal = value;
            }
        }

        private System.Windows.Forms.ContextMenuStrip ContextMenuStripInternal
        {
            get
            {
                return (System.Windows.Forms.ContextMenuStrip) this.Properties.GetObject(PropCellContextMenuStrip);
            }
            set
            {
                System.Windows.Forms.ContextMenuStrip strip = (System.Windows.Forms.ContextMenuStrip) this.Properties.GetObject(PropCellContextMenuStrip);
                if (strip != value)
                {
                    EventHandler handler = new EventHandler(this.DetachContextMenuStrip);
                    if (strip != null)
                    {
                        strip.Disposed -= handler;
                    }
                    this.Properties.SetObject(PropCellContextMenuStrip, value);
                    if (value != null)
                    {
                        value.Disposed += handler;
                    }
                    if (base.Grid != null)
                    {
                        base.Grid.OnCellContextMenuStripChanged(this);
                    }
                }
            }
        }

        private byte CurrentMouseLocation
        {
            get
            {
                return (byte) (this.flags & 3);
            }
            set
            {
                this.flags = (byte) (this.flags & -4);
                this.flags = (byte) (this.flags | value);
            }
        }

        /// <summary>Gets the default value for a cell in the row for new records.</summary>
        /// <returns>An <see cref="T:System.Object"></see> representing the default value.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public virtual object DefaultNewRowValue
        {
            get
            {
                return null;
            }
        }

        /// <summary>Gets a value that indicates whether the cell is currently displayed on-screen. </summary>
        /// <returns>true if the cell is on-screen or partially on-screen; otherwise, false.</returns>
        [Browsable(false)]
        public virtual bool Displayed
        {
            get
            {
                if (base.Grid == null)
                {
                    return false;
                }
                if (((base.Grid == null) || (this.RowIndex < 0)) || (this.ColumnIndex < 0))
                {
                    return false;
                }
                return (this.owningColumn.Displayed && this.owningRow.Displayed);
            }
        }

        /// <summary>Gets the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed. </summary>
        /// <returns>The current, formatted value of the <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        /// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:MControl.GridView.Grid.DataError"></see> event of the <see cref="T:MControl.GridView.Grid"></see> control or the handler set the <see cref="P:MControl.GridView.GridDataErrorEventArgs.ThrowException"></see> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"></see>.</exception>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell. </exception>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public object EditedFormattedValue
        {
            get
            {
                if (base.Grid == null)
                {
                    return null;
                }
                GridCellStyle gridCellStyle = this.GetInheritedStyle(null, this.RowIndex, false);
                return this.GetEditedFormattedValue(this.GetValue(this.RowIndex), this.RowIndex, ref gridCellStyle, GridDataErrorContexts.Formatting);
            }
        }

        /// <summary>Gets the type of the cell's hosted editing control. </summary>
        /// <returns>A <see cref="T:System.Type"></see> representing the <see cref="T:MControl.GridView.GridTextBoxEditingControl"></see> type.</returns>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public virtual System.Type EditType
        {
            get
            {
                return typeof(GridTextBoxEditingControl);
            }
        }

        private static Bitmap ErrorBitmap
        {
            get
            {
                if (errorBmp == null)
                {
                    errorBmp = GetBitmap("GridRow.error.bmp");
                }
                return errorBmp;
            }
        }

        /// <summary>Gets the bounds of the error icon for the cell.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the bounds of the error icon for the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        /// <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:MControl.GridView.Grid"></see> control.-or- <see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public Rectangle ErrorIconBounds
        {
            get
            {
                return this.GetErrorIconBounds(this.RowIndex);
            }
        }

        /// <summary>Gets or sets the text describing an error condition associated with the cell. </summary>
        /// <returns>The text that describes an error condition associated with the cell.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public string ErrorText
        {
            get
            {
                return this.GetErrorText(this.RowIndex);
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
                object obj2 = this.Properties.GetObject(PropCellErrorText);
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                string errorTextInternal = this.ErrorTextInternal;
                if (!string.IsNullOrEmpty(value) || this.Properties.ContainsObject(PropCellErrorText))
                {
                    this.Properties.SetObject(PropCellErrorText, value);
                }
                if ((base.Grid != null) && !errorTextInternal.Equals(this.ErrorTextInternal))
                {
                    base.Grid.OnCellErrorTextChanged(this);
                }
            }
        }

        /// <summary>Gets the value of the cell as formatted for display.</summary>
        /// <returns>The formatted value of the cell or null if the cell does not belong to a <see cref="T:MControl.GridView.Grid"></see> control.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        /// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:MControl.GridView.Grid.DataError"></see> event of the <see cref="T:MControl.GridView.Grid"></see> control or the handler set the <see cref="P:MControl.GridView.GridDataErrorEventArgs.ThrowException"></see> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"></see>.</exception>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public object FormattedValue
        {
            get
            {
                if (base.Grid == null)
                {
                    return null;
                }
                GridCellStyle cellStyle = this.GetInheritedStyle(null, this.RowIndex, false);
                return this.GetFormattedValue(this.RowIndex, ref cellStyle, GridDataErrorContexts.Formatting);
            }
        }

        /// <summary>Gets the type of the formatted value associated with the cell. </summary>
        /// <returns>A <see cref="T:System.Type"></see> representing the type of the cell's formatted value.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public virtual System.Type FormattedValueType
        {
            get
            {
                return this.ValueType;
            }
        }

        private TypeConverter FormattedValueTypeConverter
        {
            get
            {
                if (this.FormattedValueType == null)
                {
                    return null;
                }
                if (base.Grid != null)
                {
                    return base.Grid.GetCachedTypeConverter(this.FormattedValueType);
                }
                return TypeDescriptor.GetConverter(this.FormattedValueType);
            }
        }

        /// <summary>Gets a value indicating whether the cell is frozen. </summary>
        /// <returns>true if the cell is frozen; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public virtual bool Frozen
        {
            get
            {
                if (((base.Grid != null) && (this.RowIndex >= 0)) && (this.ColumnIndex >= 0))
                {
                    return (this.owningColumn.Frozen && this.owningRow.Frozen);
                }
                if ((this.owningRow == null) || ((this.owningRow.Grid != null) && (this.RowIndex < 0)))
                {
                    return false;
                }
                return this.owningRow.Frozen;
            }
        }

        internal bool HasErrorText
        {
            get
            {
                return (this.Properties.ContainsObject(PropCellErrorText) && (this.Properties.GetObject(PropCellErrorText) != null));
            }
        }

        /// <summary>Gets a value indicating whether the <see cref="P:MControl.GridView.GridCell.Style"></see> property has been set.</summary>
        /// <returns>true if the <see cref="P:MControl.GridView.GridCell.Style"></see> property has been set; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool HasStyle
        {
            get
            {
                return (this.Properties.ContainsObject(PropCellStyle) && (this.Properties.GetObject(PropCellStyle) != null));
            }
        }

        internal bool HasToolTipText
        {
            get
            {
                return (this.Properties.ContainsObject(PropCellToolTipText) && (this.Properties.GetObject(PropCellToolTipText) != null));
            }
        }

        internal bool HasValue
        {
            get
            {
                return (this.Properties.ContainsObject(PropCellValue) && (this.Properties.GetObject(PropCellValue) != null));
            }
        }

        internal virtual bool HasValueType
        {
            get
            {
                return (this.Properties.ContainsObject(PropCellValueType) && (this.Properties.GetObject(PropCellValueType) != null));
            }
        }

        /// <summary>Gets the current state of the cell as inherited from the state of its row and column.</summary>
        /// <returns>A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values representing the current state of the cell.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:MControl.GridView.Grid"></see> control and the value of its <see cref="P:MControl.GridView.GridCell.RowIndex"></see> property is -1.</exception>
        /// <exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:MControl.GridView.Grid"></see> control and the value of its <see cref="P:MControl.GridView.GridCell.RowIndex"></see> property is not -1.</exception>
        [Browsable(false)]
        public GridElementStates InheritedState
        {
            get
            {
                return this.GetInheritedState(this.RowIndex);
            }
        }

        /// <summary>Gets the style currently applied to the cell. </summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCellStyle"></see> currently applied to the cell.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        /// <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:MControl.GridView.Grid"></see> control.-or- <see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        [Browsable(false)]
        public GridCellStyle InheritedStyle
        {
            get
            {
                return this.GetInheritedStyleInternal(this.RowIndex);
            }
        }

        /// <summary>Gets a value indicating whether this cell is currently being edited.</summary>
        /// <returns>true if the cell is in edit mode; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.</exception>
        [Browsable(false)]
        public bool IsInEditMode
        {
            get
            {
                if (base.Grid == null)
                {
                    return false;
                }
                if (this.RowIndex == -1)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedCell"));
                }
                Point currentCellAddress = base.Grid.CurrentCellAddress;
                return ((((currentCellAddress.X != -1) && (currentCellAddress.X == this.ColumnIndex)) && (currentCellAddress.Y == this.RowIndex)) && base.Grid.IsCurrentCellInEditMode);
            }
        }

        /// <summary>Gets the column that contains this cell.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridColumn"></see> that contains the cell, or null if the cell is not in a column.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public GridColumn OwningColumn
        {
            get
            {
                return this.owningColumn;
            }
        }

        internal GridColumn OwningColumnInternal
        {
            set
            {
                this.owningColumn = value;
            }
        }

        /// <summary>Gets the row that contains this cell. </summary>
        /// <returns>The <see cref="T:MControl.GridView.GridRow"></see> that contains the cell, or null if the cell is not in a row.</returns>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public GridRow OwningRow
        {
            get
            {
                return this.owningRow;
            }
        }

        internal GridRow OwningRowInternal
        {
            set
            {
                this.owningRow = value;
            }
        }

        /// <summary>Gets the size, in pixels, of a rectangular area into which the cell can fit. </summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> containing the height and width, in pixels.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public System.Drawing.Size PreferredSize
        {
            get
            {
                return this.GetPreferredSize(this.RowIndex);
            }
        }

        internal PropertyStore Properties
        {
            get
            {
                return this.propertyStore;
            }
        }

        /// <summary>Gets or sets a value indicating whether the cell's data can be edited. </summary>
        /// <returns>true if the cell's data can be edited; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">There is no owning row when setting this property. -or-The owning row is shared when setting this property.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual bool ReadOnly
        {
            get
            {
                return (((this.State & GridElementStates.ReadOnly) != GridElementStates.None) || ((((this.owningRow != null) && ((this.owningRow.Grid == null) || (this.RowIndex >= 0))) && this.owningRow.ReadOnly) || ((((base.Grid != null) && (this.RowIndex >= 0)) && (this.ColumnIndex >= 0)) && this.owningColumn.ReadOnly)));
            }
            set
            {
                if (base.Grid != null)
                {
                    if (this.RowIndex == -1)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedCell"));
                    }
                    if ((value != this.ReadOnly) && !base.Grid.ReadOnly)
                    {
                        base.Grid.OnGridElementStateChanging(this, -1, GridElementStates.ReadOnly);
                        base.Grid.SetReadOnlyCellCore(this.ColumnIndex, this.RowIndex, value);
                    }
                }
                else if (this.owningRow == null)
                {
                    if (value != this.ReadOnly)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCell_CannotSetReadOnlyState"));
                    }
                }
                else
                {
                    this.owningRow.SetReadOnlyCellCore(this, value);
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
                if (base.Grid != null)
                {
                    base.Grid.OnGridElementStateChanged(this, -1, GridElementStates.ReadOnly);
                }
            }
        }

        /// <summary>Gets a value indicating whether the cell can be resized. </summary>
        /// <returns>true if the cell can be resized; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public virtual bool Resizable
        {
            get
            {
                return ((((this.owningRow != null) && ((this.owningRow.Grid == null) || (this.RowIndex >= 0))) && (this.owningRow.Resizable == GridTriState.True)) || ((((base.Grid != null) && (this.RowIndex >= 0)) && (this.ColumnIndex >= 0)) && (this.owningColumn.Resizable == GridTriState.True)));
            }
        }

        /// <summary>Gets the index of the cell's parent row. </summary>
        /// <returns>The index of the row that contains the cell; -1 if there is no owning row.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int RowIndex
        {
            get
            {
                if (this.owningRow == null)
                {
                    return -1;
                }
                return this.owningRow.Index;
            }
        }

        /// <summary>Gets or sets a value indicating whether the cell has been selected. </summary>
        /// <returns>true if the cell has been selected; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:MControl.GridView.Grid"></see> when setting this property. -or-The owning row is shared when setting this property.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool Selected
        {
            get
            {
                return (((this.State & GridElementStates.Selected) != GridElementStates.None) || ((((this.owningRow != null) && ((this.owningRow.Grid == null) || (this.RowIndex >= 0))) && this.owningRow.Selected) || ((((base.Grid != null) && (this.RowIndex >= 0)) && (this.ColumnIndex >= 0)) && this.owningColumn.Selected)));
            }
            set
            {
                if (base.Grid != null)
                {
                    if (this.RowIndex == -1)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedCell"));
                    }
                    base.Grid.SetSelectedCellCoreInternal(this.ColumnIndex, this.RowIndex, value);
                }
                else if (value)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCell_CannotSetSelectedState"));
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
                    base.Grid.OnGridElementStateChanged(this, -1, GridElementStates.Selected);
                }
            }
        }

        /// <summary>Gets the size of the cell.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size"></see> set to the owning row's height and the owning column's width. </returns>
        /// <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public System.Drawing.Size Size
        {
            get
            {
                return this.GetSize(this.RowIndex);
            }
        }

        internal Rectangle StdBorderWidths
        {
            get
            {
                if (base.Grid != null)
                {
                    GridAdvancedBorderStyle gridAdvancedBorderStylePlaceholder = new GridAdvancedBorderStyle();
                    GridAdvancedBorderStyle advancedBorderStyle = this.AdjustCellBorderStyle(base.Grid.AdvancedCellBorderStyle, gridAdvancedBorderStylePlaceholder, false, false, false, false);
                    return this.BorderWidths(advancedBorderStyle);
                }
                return Rectangle.Empty;
            }
        }

        /// <summary>Gets or sets the style for the cell. </summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(true)]
        public GridCellStyle Style
        {
            get
            {
                GridCellStyle style = (GridCellStyle) this.Properties.GetObject(PropCellStyle);
                if (style == null)
                {
                    style = new GridCellStyle();
                    style.AddScope(base.Grid, GridCellStyleScopes.Cell);
                    this.Properties.SetObject(PropCellStyle, style);
                }
                return style;
            }
            set
            {
                GridCellStyle style = null;
                if (this.HasStyle)
                {
                    style = this.Style;
                    style.RemoveScope(GridCellStyleScopes.Cell);
                }
                if ((value != null) || this.Properties.ContainsObject(PropCellStyle))
                {
                    if (value != null)
                    {
                        value.AddScope(base.Grid, GridCellStyleScopes.Cell);
                    }
                    this.Properties.SetObject(PropCellStyle, value);
                }
                if (((((style != null) && (value == null)) || ((style == null) && (value != null))) || (((style != null) && (value != null)) && !style.Equals(this.Style))) && (base.Grid != null))
                {
                    base.Grid.OnCellStyleChanged(this);
                }
            }
        }

        /// <summary>Gets or sets the object that contains supplemental data about the cell. </summary>
        /// <returns>An <see cref="T:System.Object"></see> that contains data about the cell. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [Category("Data"), TypeConverter(typeof(StringConverter)), Localizable(false), Bindable(true), Description("ControlTag"), DefaultValue((string) null)]
        public object Tag
        {
            get
            {
                return this.Properties.GetObject(PropCellTag);
            }
            set
            {
                if ((value != null) || this.Properties.ContainsObject(PropCellTag))
                {
                    this.Properties.SetObject(PropCellTag, value);
                }
            }
        }

        /// <summary>Gets or sets the ToolTip text associated with this cell.</summary>
        /// <returns>The ToolTip text associated with the cell. The default is <see cref="F:System.String.Empty"></see>. </returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string ToolTipText
        {
            get
            {
                return this.GetToolTipText(this.RowIndex);
            }
            set
            {
                this.ToolTipTextInternal = value;
            }
        }

        private string ToolTipTextInternal
        {
            get
            {
                object obj2 = this.Properties.GetObject(PropCellToolTipText);
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                string toolTipTextInternal = this.ToolTipTextInternal;
                if (!string.IsNullOrEmpty(value) || this.Properties.ContainsObject(PropCellToolTipText))
                {
                    this.Properties.SetObject(PropCellToolTipText, value);
                }
                if ((base.Grid != null) && !toolTipTextInternal.Equals(this.ToolTipTextInternal))
                {
                    base.Grid.OnCellToolTipTextChanged(this);
                }
            }
        }

        /// <summary>Gets or sets the value associated with this cell. </summary>
        /// <returns>Gets or sets the data to be displayed by the cell. The default is null.</returns>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCell.ColumnIndex"></see> is less than 0, indicating that the cell is a row header cell.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><see cref="P:MControl.GridView.GridCell.RowIndex"></see> is outside the valid range of 0 to the number of rows in the control minus 1.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public object Value
        {
            get
            {
                return this.GetValue(this.RowIndex);
            }
            set
            {
                this.SetValue(this.RowIndex, value);
            }
        }

        /// <summary>Gets or sets the data type of the values in the cell. </summary>
        /// <returns>A <see cref="T:System.Type"></see> representing the data type of the value in the cell.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public virtual System.Type ValueType
        {
            get
            {
                System.Type valueType = (System.Type) this.Properties.GetObject(PropCellValueType);
                if ((valueType == null) && (this.OwningColumn != null))
                {
                    valueType = this.OwningColumn.ValueType;
                }
                return valueType;
            }
            set
            {
                if ((value != null) || this.Properties.ContainsObject(PropCellValueType))
                {
                    this.Properties.SetObject(PropCellValueType, value);
                }
            }
        }

        private TypeConverter ValueTypeConverter
        {
            get
            {
                TypeConverter boundColumnConverter = null;
                if (this.OwningColumn != null)
                {
                    boundColumnConverter = this.OwningColumn.BoundColumnConverter;
                }
                if ((boundColumnConverter != null) || (this.ValueType == null))
                {
                    return boundColumnConverter;
                }
                if (base.Grid != null)
                {
                    return base.Grid.GetCachedTypeConverter(this.ValueType);
                }
                return TypeDescriptor.GetConverter(this.ValueType);
            }
        }

        /// <summary>Gets a value indicating whether the cell is in a row or column that has been hidden. </summary>
        /// <returns>true if the cell is visible; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public virtual bool Visible
        {
            get
            {
                if (((base.Grid != null) && (this.RowIndex >= 0)) && (this.ColumnIndex >= 0))
                {
                    return (this.owningColumn.Visible && this.owningRow.Visible);
                }
                if ((this.owningRow == null) || ((this.owningRow.Grid != null) && (this.RowIndex < 0)))
                {
                    return false;
                }
                return this.owningRow.Visible;
            }
        }

        /// <summary>Provides information about a <see cref="T:MControl.GridView.GridCell"></see> to accessibility client applications.</summary>
        [ComVisible(true)]
        protected class GridCellAccessibleObject : AccessibleObject
        {
            private GridCell owner;

            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> class without initializing the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property.</summary>
            public GridCellAccessibleObject()
            {
            }

            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> class, setting the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property to the specified <see cref="T:MControl.GridView.GridCell"></see>.</summary>
            /// <param name="owner">The <see cref="T:MControl.GridView.GridCell"></see> that owns the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</param>
            public GridCellAccessibleObject(GridCell owner)
            {
                this.owner = owner;
            }

            /// <summary>Performs the default action associated with the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</summary>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.-or-The value of the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> property is not null and the <see cref="P:MControl.GridView.GridCell.RowIndex"></see> property of the <see cref="T:MControl.GridView.GridCell"></see> returned by the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is equal to -1.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void DoDefaultAction()
            {
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                }
                GridCell owner = this.Owner;
                Grid grid = owner.Grid;
                if (!(owner is GridHeaderCell))
                {
                    if ((grid != null) && (owner.RowIndex == -1))
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("Grid_InvalidOperationOnSharedCell"));
                    }
                    this.Select(AccessibleSelection.TakeSelection | AccessibleSelection.TakeFocus);
                    if ((!owner.ReadOnly && (owner.EditType != null)) && (!grid.InBeginEdit && !grid.InEndEdit))
                    {
                        if (grid.IsCurrentCellInEditMode)
                        {
                            grid.EndEdit();
                        }
                        else if (grid.EditMode != GridEditMode.EditProgrammatically)
                        {
                            grid.BeginEdit(true);
                        }
                    }
                }
            }

            internal Rectangle GetAccessibleObjectBounds(AccessibleObject parentAccObject)
            {
                Rectangle columnDisplayRectangle;
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                }
                if (this.owner.OwningColumn == null)
                {
                    return Rectangle.Empty;
                }
                Rectangle bounds = parentAccObject.Bounds;
                int num = this.owner.Grid.Columns.ColumnIndexToActualDisplayIndex(this.owner.Grid.FirstDisplayedScrollingColumnIndex, GridElementStates.Visible);
                int num2 = this.owner.Grid.Columns.ColumnIndexToActualDisplayIndex(this.owner.ColumnIndex, GridElementStates.Visible);
                bool rowHeadersVisible = this.owner.Grid.RowHeadersVisible;
                if (num2 < num)
                {
                    columnDisplayRectangle = parentAccObject.GetChild((num2 + 1) + (rowHeadersVisible ? 1 : 0)).Bounds;
                    if (this.Owner.Grid.RightToLeft == RightToLeft.No)
                    {
                        columnDisplayRectangle.X -= this.owner.OwningColumn.Width;
                    }
                    else
                    {
                        columnDisplayRectangle.X = columnDisplayRectangle.Right;
                    }
                    columnDisplayRectangle.Width = this.owner.OwningColumn.Width;
                }
                else if (num2 == num)
                {
                    columnDisplayRectangle = this.owner.Grid.GetColumnDisplayRectangle(this.owner.ColumnIndex, false);
                    int firstDisplayedScrollingColumnHiddenWidth = this.owner.Grid.FirstDisplayedScrollingColumnHiddenWidth;
                    if (firstDisplayedScrollingColumnHiddenWidth != 0)
                    {
                        if (this.owner.Grid.RightToLeft == RightToLeft.No)
                        {
                            columnDisplayRectangle.X -= firstDisplayedScrollingColumnHiddenWidth;
                        }
                        columnDisplayRectangle.Width += firstDisplayedScrollingColumnHiddenWidth;
                    }
                    columnDisplayRectangle = this.owner.Grid.RectangleToScreen(columnDisplayRectangle);
                }
                else
                {
                    columnDisplayRectangle = parentAccObject.GetChild((num2 - 1) + (rowHeadersVisible ? 1 : 0)).Bounds;
                    if (this.owner.Grid.RightToLeft == RightToLeft.No)
                    {
                        columnDisplayRectangle.X = columnDisplayRectangle.Right;
                    }
                    else
                    {
                        columnDisplayRectangle.X -= this.owner.OwningColumn.Width;
                    }
                    columnDisplayRectangle.Width = this.owner.OwningColumn.Width;
                }
                bounds.X = columnDisplayRectangle.X;
                bounds.Width = columnDisplayRectangle.Width;
                return bounds;
            }

            private AccessibleObject GetAccessibleObjectParent()
            {
                if ((((this.owner is GridButtonCell) || (this.owner is GridCheckBoxCell)) || ((this.owner is GridComboBoxCell) || (this.owner is GridImageCell))) || ((this.owner is GridLinkCell) || (this.owner is GridTextBoxCell)))
                {
                    return this.ParentPrivate;
                }
                return this.Parent;
            }

            /// <summary>Returns the accessible object corresponding to the specified index.</summary>
            /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject"></see> that represents the accessible child corresponding to the specified index.</returns>
            /// <param name="index">The zero-based index of the child accessible object.</param>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            public override AccessibleObject GetChild(int index)
            {
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                }
                if (((this.owner.Grid.EditingControl != null) && this.owner.Grid.IsCurrentCellInEditMode) && ((this.owner.Grid.CurrentCell == this.owner) && (index == 0)))
                {
                    return this.owner.Grid.EditingControl.AccessibilityObject;
                }
                return null;
            }

            /// <summary>Returns the number of children that belong to the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</summary>
            /// <returns>The value 1 if the <see cref="T:MControl.GridView.GridCell"></see> that owns <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> is being edited; otherwise, –1.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            public override int GetChildCount()
            {
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                }
                if (((this.owner.Grid.EditingControl != null) && this.owner.Grid.IsCurrentCellInEditMode) && (this.owner.Grid.CurrentCell == this.owner))
                {
                    return 1;
                }
                return 0;
            }

            /// <summary>Returns the child accessible object that has keyboard focus.</summary>
            /// <returns>null in all cases.</returns>
            public override AccessibleObject GetFocused()
            {
                return null;
            }

            /// <summary>Returns the child accessible object that is currently selected.</summary>
            /// <returns>null in all cases.</returns>
            public override AccessibleObject GetSelected()
            {
                return null;
            }

            /// <summary>Navigates to another accessible object.</summary>
            /// <returns>A <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see> that represents the <see cref="T:MControl.GridView.GridCell"></see> at the specified <see cref="T:System.Windows.Forms.AccessibleNavigation"></see> value.</returns>
            /// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation"></see> values.</param>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
            {
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                }
                if ((this.owner.OwningColumn != null) && (this.owner.OwningRow != null))
                {
                    switch (navigationDirection)
                    {
                        case AccessibleNavigation.Up:
                            if (this.owner.OwningRow.Index != this.owner.Grid.Rows.GetFirstRow(GridElementStates.Visible))
                            {
                                int previousRow = this.owner.Grid.Rows.GetPreviousRow(this.owner.OwningRow.Index, GridElementStates.Visible);
                                return this.owner.Grid.Rows[previousRow].Cells[this.owner.OwningColumn.Index].AccessibilityObject;
                            }
                            if (!this.owner.Grid.ColumnHeadersVisible)
                            {
                                return null;
                            }
                            return this.owner.OwningColumn.HeaderCell.AccessibilityObject;

                        case AccessibleNavigation.Down:
                            if (this.owner.OwningRow.Index != this.owner.Grid.Rows.GetLastRow(GridElementStates.Visible))
                            {
                                int nextRow = this.owner.Grid.Rows.GetNextRow(this.owner.OwningRow.Index, GridElementStates.Visible);
                                return this.owner.Grid.Rows[nextRow].Cells[this.owner.OwningColumn.Index].AccessibilityObject;
                            }
                            return null;

                        case AccessibleNavigation.Left:
                            if (this.owner.Grid.RightToLeft != RightToLeft.No)
                            {
                                return this.NavigateForward(true);
                            }
                            return this.NavigateBackward(true);

                        case AccessibleNavigation.Right:
                            if (this.owner.Grid.RightToLeft != RightToLeft.No)
                            {
                                return this.NavigateBackward(true);
                            }
                            return this.NavigateForward(true);

                        case AccessibleNavigation.Next:
                            return this.NavigateForward(false);

                        case AccessibleNavigation.Previous:
                            return this.NavigateBackward(false);
                    }
                }
                return null;
            }

            private AccessibleObject NavigateBackward(bool wrapAround)
            {
                if (this.owner.OwningColumn == this.owner.Grid.Columns.GetFirstColumn(GridElementStates.Visible))
                {
                    if (wrapAround)
                    {
                        AccessibleObject obj2 = this.Owner.OwningRow.AccessibilityObject.Navigate(AccessibleNavigation.Previous);
                        if ((obj2 != null) && (obj2.GetChildCount() > 0))
                        {
                            return obj2.GetChild(obj2.GetChildCount() - 1);
                        }
                        return null;
                    }
                    if (this.owner.Grid.RowHeadersVisible)
                    {
                        return this.owner.OwningRow.AccessibilityObject.GetChild(0);
                    }
                    return null;
                }
                int index = this.owner.Grid.Columns.GetPreviousColumn(this.owner.OwningColumn, GridElementStates.Visible, GridElementStates.None).Index;
                return this.owner.OwningRow.Cells[index].AccessibilityObject;
            }

            private AccessibleObject NavigateForward(bool wrapAround)
            {
                if (this.owner.OwningColumn == this.owner.Grid.Columns.GetLastColumn(GridElementStates.Visible, GridElementStates.None))
                {
                    if (!wrapAround)
                    {
                        return null;
                    }
                    AccessibleObject obj2 = this.Owner.OwningRow.AccessibilityObject.Navigate(AccessibleNavigation.Next);
                    if ((obj2 == null) || (obj2.GetChildCount() <= 0))
                    {
                        return null;
                    }
                    if (this.Owner.Grid.RowHeadersVisible)
                    {
                        return obj2.GetChild(1);
                    }
                    return obj2.GetChild(0);
                }
                int index = this.owner.Grid.Columns.GetNextColumn(this.owner.OwningColumn, GridElementStates.Visible, GridElementStates.None).Index;
                return this.owner.OwningRow.Cells[index].AccessibilityObject;
            }

            /// <summary>Modifies the selection or moves the keyboard focus of the accessible object.</summary>
            /// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection"></see> values.</param>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void Select(AccessibleSelection flags)
            {
                if (this.owner == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                }
                if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
                {
                    this.owner.Grid.FocusInternal();
                }
                if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
                {
                    this.owner.Selected = true;
                    this.owner.Grid.CurrentCell = this.owner;
                }
                if ((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection)
                {
                    this.owner.Selected = true;
                }
                if (((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection) && ((flags & (AccessibleSelection.AddSelection | AccessibleSelection.TakeSelection)) == AccessibleSelection.None))
                {
                    this.owner.Selected = false;
                }
            }

            /// <summary>Gets the location and size of the accessible object.</summary>
            /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the bounds of the accessible object.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            public override Rectangle Bounds
            {
                get
                {
                    return this.GetAccessibleObjectBounds(this.GetAccessibleObjectParent());
                }
            }

            /// <summary>Gets a string that describes the default action of the <see cref="T:MControl.GridView.GridCell"></see>.</summary>
            /// <returns>The string "Edit".</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            public override string DefaultAction
            {
                get
                {
                    if (this.Owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                    }
                    if (!this.Owner.ReadOnly)
                    {
                        return MControl.GridView.RM.GetString("Grid_AccCellDefaultAction");
                    }
                    return string.Empty;
                }
            }

            /// <summary>Gets the names of the owning cell's type and base type.</summary>
            /// <returns>The names of the owning cell's type and base type.</returns>
            public override string Help
            {
                get
                {
                    return (this.owner.GetType().Name + "(" + this.owner.GetType().BaseType.Name + ")");
                }
            }

            /// <summary>Gets the name of the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</summary>
            /// <returns>The name of the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            public override string Name
            {
                get
                {
                    if (this.owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                    }
                    if (this.owner.OwningColumn != null)
                    {
                        return MControl.GridView.RM.GetString("Grid_AccGridCellName", new object[] { this.owner.OwningColumn.HeaderText, this.owner.OwningRow.Index });
                    }
                    return string.Empty;
                }
            }

            /// <summary>Gets or sets the cell that owns the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</summary>
            /// <returns>The <see cref="T:MControl.GridView.GridCell"></see> that owns the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</returns>
            /// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property has already been set.</exception>
            public GridCell Owner
            {
                get
                {
                    return this.owner;
                }
                set
                {
                    if (this.owner != null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerAlreadySet"));
                    }
                    this.owner = value;
                }
            }

            /// <summary>Gets the parent of the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</summary>
            /// <returns>The parent of the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
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
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                    }
                    if (this.owner.OwningRow == null)
                    {
                        return null;
                    }
                    return this.owner.OwningRow.AccessibilityObject;
                }
            }

            /// <summary>Gets the role of the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</summary>
            /// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Cell"></see> value.</returns>
            public override AccessibleRole Role
            {
                get
                {
                    return AccessibleRole.Cell;
                }
            }

            /// <summary>Gets the state of the <see cref="T:MControl.GridView.GridCell.GridCellAccessibleObject"></see>.</summary>
            /// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates"></see> values. </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            public override AccessibleStates State
            {
                get
                {
                    Rectangle rectangle;
                    if (this.owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                    }
                    AccessibleStates states = AccessibleStates.Selectable | AccessibleStates.Focusable;
                    if (this.owner == this.owner.Grid.CurrentCell)
                    {
                        states |= AccessibleStates.Focused;
                    }
                    if (this.owner.Selected)
                    {
                        states |= AccessibleStates.Selected;
                    }
                    if ((this.owner.OwningColumn != null) && (this.owner.OwningRow != null))
                    {
                        rectangle = this.owner.Grid.GetCellDisplayRectangle(this.owner.OwningColumn.Index, this.owner.OwningRow.Index, false);
                    }
                    else if (this.owner.OwningRow != null)
                    {
                        rectangle = this.owner.Grid.GetCellDisplayRectangle(-1, this.owner.OwningRow.Index, false);
                    }
                    else if (this.owner.OwningColumn != null)
                    {
                        rectangle = this.owner.Grid.GetCellDisplayRectangle(this.owner.OwningColumn.Index, -1, false);
                    }
                    else
                    {
                        rectangle = this.owner.Grid.GetCellDisplayRectangle(-1, -1, false);
                    }
                    if (!rectangle.IntersectsWith(this.owner.Grid.ClientRectangle))
                    {
                        states |= AccessibleStates.Offscreen;
                    }
                    return states;
                }
            }

            /// <summary>Gets or sets a string representing the formatted value of the owning cell. </summary>
            /// <returns>A <see cref="T:System.String"></see> representation of the cell value.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            public override string Value
            {
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    if (this.owner == null)
                    {
                        throw new InvalidOperationException(MControl.GridView.RM.GetString("GridCellAccessibleObject_OwnerNotSet"));
                    }
                    object formattedValue = this.owner.FormattedValue;
                    string str = formattedValue as string;
                    if ((formattedValue == null) || ((str != null) && string.IsNullOrEmpty(str)))
                    {
                        return MControl.GridView.RM.GetString("Grid_AccNullValue");
                    }
                    if (str != null)
                    {
                        return str;
                    }
                    if (this.owner.OwningColumn == null)
                    {
                        return string.Empty;
                    }
                    TypeConverter formattedValueTypeConverter = this.owner.FormattedValueTypeConverter;
                    if ((formattedValueTypeConverter != null) && formattedValueTypeConverter.CanConvertTo(typeof(string)))
                    {
                        return formattedValueTypeConverter.ConvertToString(formattedValue);
                    }
                    return formattedValue.ToString();
                }
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                set
                {
                    if ((!(this.owner is GridHeaderCell) && !this.owner.ReadOnly) && (this.owner.OwningRow != null))
                    {
                        if (this.owner.Grid.IsCurrentCellInEditMode)
                        {
                            this.owner.Grid.EndEdit();
                        }
                        GridCellStyle inheritedStyle = this.owner.InheritedStyle;
                        object formattedValue = this.owner.GetFormattedValue(value, this.owner.OwningRow.Index, ref inheritedStyle, null, null, GridDataErrorContexts.Formatting);
                        this.owner.Value = this.owner.ParseFormattedValue(formattedValue, inheritedStyle, null, null);
                    }
                }
            }
        }
    }
}

