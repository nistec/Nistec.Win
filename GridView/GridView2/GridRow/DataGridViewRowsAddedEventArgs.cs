namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.RowsAdded"></see> event. </summary>
    public class GridRowsAddedEventArgs : EventArgs
    {
        private int rowCount;
        private int rowIndex;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowsAddedEventArgs"></see> class. </summary>
        /// <param name="rowCount">The number of rows that have been added.</param>
        /// <param name="rowIndex">The index of the first added row.</param>
        public GridRowsAddedEventArgs(int rowIndex, int rowCount)
        {
            this.rowIndex = rowIndex;
            this.rowCount = rowCount;
        }

        /// <summary>Gets the number of rows that have been added.</summary>
        /// <returns>The number of rows that have been added.</returns>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        /// <summary>Gets the index of the first added row.</summary>
        /// <returns>The index of the first added row.</returns>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}

