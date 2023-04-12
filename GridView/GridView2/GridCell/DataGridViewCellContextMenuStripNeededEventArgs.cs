namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellContextMenuStripNeeded"></see> event. </summary>
    public class GridCellContextMenuStripNeededEventArgs : GridCellEventArgs
    {
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellContextMenuStripNeededEventArgs"></see> class. </summary>
        /// <param name="columnIndex">The column index of cell that the event occurred for.</param>
        /// <param name="rowIndex">The row index of the cell that the event occurred for.</param>
        public GridCellContextMenuStripNeededEventArgs(int columnIndex, int rowIndex) : base(columnIndex, rowIndex)
        {
        }

        internal GridCellContextMenuStripNeededEventArgs(int columnIndex, int rowIndex, System.Windows.Forms.ContextMenuStrip contextMenuStrip) : base(columnIndex, rowIndex)
        {
            this.contextMenuStrip = contextMenuStrip;
        }

        /// <summary>Gets or sets the shortcut menu for the cell that raised the <see cref="E:MControl.GridView.Grid.CellContextMenuStripNeeded"></see> event.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> for the cell. </returns>
        public System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return this.contextMenuStrip;
            }
            set
            {
                this.contextMenuStrip = value;
            }
        }
    }
}

