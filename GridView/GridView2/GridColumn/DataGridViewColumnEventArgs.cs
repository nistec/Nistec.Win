namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for column-related events of a <see cref="T:MControl.GridView.Grid"></see>. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridColumnEventArgs : EventArgs
    {
        private GridColumn gridColumn;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridColumnEventArgs"></see> class. </summary>
        /// <param name="gridColumn">The column that the event occurs for.</param>
        /// <exception cref="T:System.ArgumentNullException">gridColumn is null.</exception>
        public GridColumnEventArgs(GridColumn gridColumn)
        {
            if (gridColumn == null)
            {
                throw new ArgumentNullException("gridColumn");
            }
            this.gridColumn = gridColumn;
        }

        /// <summary>Gets the column that the event occurs for.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridColumn"></see> that the event occurs for.</returns>
        /// <filterpriority>1</filterpriority>
        public GridColumn Column
        {
            get
            {
                return this.gridColumn;
            }
        }
    }
}

