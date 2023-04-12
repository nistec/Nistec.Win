namespace MControl.GridView
{
    using System;
    using System.Globalization;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.RowsRemoved"></see> event.</summary>
    public class GridRowsRemovedEventArgs : EventArgs
    {
        private int rowCount;
        private int rowIndex;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowsRemovedEventArgs"></see> class.</summary>
        /// <param name="rowCount">The number of rows that were deleted.</param>
        /// <param name="rowIndex">The zero-based index of the row that was deleted, or the first deleted row if multiple rows were deleted. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than 0.-or-rowCount is less than 1.</exception>
        public GridRowsRemovedEventArgs(int rowIndex, int rowCount)
        {
            if (rowIndex < 0)
            {
                object[] args = new object[] { "rowIndex", rowIndex.ToString(CultureInfo.CurrentCulture), 0.ToString(CultureInfo.CurrentCulture) };
                throw new ArgumentOutOfRangeException("rowIndex", MControl.GridView.RM.GetString("InvalidLowBoundArgumentEx", args));
            }
            if (rowCount < 1)
            {
                object[] objArray2 = new object[] { "rowCount", rowCount.ToString(CultureInfo.CurrentCulture), 1.ToString(CultureInfo.CurrentCulture) };
                throw new ArgumentOutOfRangeException("rowCount", MControl.GridView.RM.GetString("InvalidLowBoundArgumentEx", objArray2));
            }
            this.rowIndex = rowIndex;
            this.rowCount = rowCount;
        }

        /// <summary>Gets the number of rows that were deleted.</summary>
        /// <returns>The number of deleted rows.</returns>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        /// <summary>Gets the zero-based index of the row deleted, or the first deleted row if multiple rows were deleted.</summary>
        /// <returns>The zero-based index of the row that was deleted, or the first deleted row if multiple rows were deleted.</returns>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}

