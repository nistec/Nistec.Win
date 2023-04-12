namespace MControl.GridView
{
    using System;
    using System.ComponentModel;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.SortCompare"></see> event.</summary>
    public class GridSortCompareEventArgs : HandledEventArgs
    {
        private object cellValue1;
        private object cellValue2;
        private GridColumn gridColumn;
        private int rowIndex1;
        private int rowIndex2;
        private int sortResult;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridSortCompareEventArgs"></see> class.</summary>
        /// <param name="cellValue2">The value of the second cell to compare.</param>
        /// <param name="gridColumn">The column to sort.</param>
        /// <param name="rowIndex2">The index of the row containing the second cell.</param>
        /// <param name="cellValue1">The value of the first cell to compare.</param>
        /// <param name="rowIndex1">The index of the row containing the first cell.</param>
        public GridSortCompareEventArgs(GridColumn gridColumn, object cellValue1, object cellValue2, int rowIndex1, int rowIndex2)
        {
            this.gridColumn = gridColumn;
            this.cellValue1 = cellValue1;
            this.cellValue2 = cellValue2;
            this.rowIndex1 = rowIndex1;
            this.rowIndex2 = rowIndex2;
        }

        /// <summary>Gets the value of the first cell to compare.</summary>
        /// <returns>The value of the first cell.</returns>
        public object CellValue1
        {
            get
            {
                return this.cellValue1;
            }
        }

        /// <summary>Gets the value of the second cell to compare.</summary>
        /// <returns>The value of the second cell.</returns>
        public object CellValue2
        {
            get
            {
                return this.cellValue2;
            }
        }

        /// <summary>Gets the column being sorted. </summary>
        /// <returns>The <see cref="T:MControl.GridView.GridColumn"></see> to sort.</returns>
        public GridColumn Column
        {
            get
            {
                return this.gridColumn;
            }
        }

        /// <summary>Gets the index of the row containing the first cell to compare.</summary>
        /// <returns>The index of the row containing the second cell.</returns>
        public int RowIndex1
        {
            get
            {
                return this.rowIndex1;
            }
        }

        /// <summary>Gets the index of the row containing the second cell to compare.</summary>
        /// <returns>The index of the row containing the second cell.</returns>
        public int RowIndex2
        {
            get
            {
                return this.rowIndex2;
            }
        }

        /// <summary>Gets or sets a value indicating the order in which the compared cells will be sorted.</summary>
        /// <returns>Less than zero if the first cell will be sorted before the second cell; zero if the first cell and second cell have equivalent values; greater than zero if the second cell will be sorted before the first cell.</returns>
        public int SortResult
        {
            get
            {
                return this.sortResult;
            }
            set
            {
                this.sortResult = value;
            }
        }
    }
}

