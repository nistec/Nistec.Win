namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.AutoSizeColumnModeChanged"></see> event. </summary>
    public class GridAutoSizeColumnModeEventArgs : EventArgs
    {
        private GridColumn gridColumn;
        private GridAutoSizeColumnMode previousMode;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridAutoSizeColumnModeEventArgs"></see> class. </summary>
        /// <param name="previousMode">The previous <see cref="T:MControl.GridView.GridAutoSizeColumnMode"></see> value of the column's <see cref="P:MControl.GridView.GridColumn.AutoSizeMode"></see> property. </param>
        /// <param name="gridColumn">The column with the <see cref="P:MControl.GridView.GridColumn.AutoSizeMode"></see> property that changed.</param>
        public GridAutoSizeColumnModeEventArgs(GridColumn gridColumn, GridAutoSizeColumnMode previousMode)
        {
            this.gridColumn = gridColumn;
            this.previousMode = previousMode;
        }

        /// <summary>Gets the column with the <see cref="P:MControl.GridView.GridColumn.AutoSizeMode"></see> property that changed.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridColumn"></see> with the <see cref="P:MControl.GridView.GridColumn.AutoSizeMode"></see> property that changed.</returns>
        public GridColumn Column
        {
            get
            {
                return this.gridColumn;
            }
        }

        /// <summary>Gets the previous value of the <see cref="P:MControl.GridView.GridColumn.AutoSizeMode"></see> property of the column.</summary>
        /// <returns>An <see cref="T:MControl.GridView.GridAutoSizeColumnMode"></see> value representing the previous value of the <see cref="P:MControl.GridView.GridColumn.AutoSizeMode"></see> property of the <see cref="P:MControl.GridView.GridAutoSizeColumnModeEventArgs.Column"></see>.</returns>
        public GridAutoSizeColumnMode PreviousMode
        {
            get
            {
                return this.previousMode;
            }
        }
    }
}

