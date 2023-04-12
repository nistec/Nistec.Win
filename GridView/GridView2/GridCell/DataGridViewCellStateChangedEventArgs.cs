namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellStateChanged"></see> event. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellStateChangedEventArgs : EventArgs
    {
        private GridCell gridCell;
        private GridElementStates stateChanged;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellStateChangedEventArgs"></see> class. </summary>
        /// <param name="stateChanged">One of the <see cref="T:MControl.GridView.GridElementStates"></see> values indicating the state that has changed on the cell.</param>
        /// <param name="gridCell">The <see cref="T:MControl.GridView.GridCell"></see> that has a changed state.</param>
        /// <exception cref="T:System.ArgumentNullException">gridCell is null.</exception>
        public GridCellStateChangedEventArgs(GridCell gridCell, GridElementStates stateChanged)
        {
            if (gridCell == null)
            {
                throw new ArgumentNullException("gridCell");
            }
            this.gridCell = gridCell;
            this.stateChanged = stateChanged;
        }

        /// <summary>Gets the <see cref="T:MControl.GridView.GridCell"></see> that has a changed state.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCell"></see> whose state has changed.</returns>
        /// <filterpriority>1</filterpriority>
        public GridCell Cell
        {
            get
            {
                return this.gridCell;
            }
        }

        /// <summary>Gets the state that has changed on the cell.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridElementStates"></see> values indicating the state that has changed on the cell.</returns>
        public GridElementStates StateChanged
        {
            get
            {
                return this.stateChanged;
            }
        }
    }
}

