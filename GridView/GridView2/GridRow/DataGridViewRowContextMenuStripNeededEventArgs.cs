namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.RowContextMenuStripNeeded"></see> event. </summary>
    public class GridRowContextMenuStripNeededEventArgs : EventArgs
    {
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private int rowIndex;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowContextMenuStripNeededEventArgs"></see> class. </summary>
        /// <param name="rowIndex">The index of the row.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than -1.</exception>
        public GridRowContextMenuStripNeededEventArgs(int rowIndex)
        {
            if (rowIndex < -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            this.rowIndex = rowIndex;
        }

        internal GridRowContextMenuStripNeededEventArgs(int rowIndex, System.Windows.Forms.ContextMenuStrip contextMenuStrip) : this(rowIndex)
        {
            this.contextMenuStrip = contextMenuStrip;
        }

        /// <summary>Gets or sets the shortcut menu for the row that raised the <see cref="E:MControl.GridView.Grid.RowContextMenuStripNeeded"></see> event.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> in use.</returns>
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

        /// <summary>Gets the index of the row that is requesting a shortcut menu.</summary>
        /// <returns>The zero-based index of the row that is requesting a shortcut menu.</returns>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}

