namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.ColumnStateChanged"></see> event. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridColumnStateChangedEventArgs : EventArgs
    {
        private GridColumn gridColumn;
        private GridElementStates stateChanged;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridColumnStateChangedEventArgs"></see> class. </summary>
        /// <param name="stateChanged">One of the <see cref="T:MControl.GridView.GridElementStates"></see> values.</param>
        /// <param name="gridColumn">The <see cref="T:MControl.GridView.GridColumn"></see> whose state has changed.</param>
        public GridColumnStateChangedEventArgs(GridColumn gridColumn, GridElementStates stateChanged)
        {
            this.gridColumn = gridColumn;
            this.stateChanged = stateChanged;
        }

        /// <summary>Gets the column whose state changed.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridColumn"></see> whose state changed.</returns>
        /// <filterpriority>1</filterpriority>
        public GridColumn Column
        {
            get
            {
                return this.gridColumn;
            }
        }

        /// <summary>Gets the new column state.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridElementStates"></see> values.</returns>
        public GridElementStates StateChanged
        {
            get
            {
                return this.stateChanged;
            }
        }
    }
}

