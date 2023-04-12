namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.RowStateChanged"></see> event of a <see cref="T:MControl.GridView.Grid"></see>.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridRowStateChangedEventArgs : EventArgs
    {
        private GridRow gridRow;
        private GridElementStates stateChanged;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowStateChangedEventArgs"></see> class. </summary>
        /// <param name="stateChanged">One of the <see cref="T:MControl.GridView.GridElementStates"></see> values indicating the state that has changed on the row.</param>
        /// <param name="gridRow">The <see cref="T:MControl.GridView.GridRow"></see> that has a changed state.</param>
        public GridRowStateChangedEventArgs(GridRow gridRow, GridElementStates stateChanged)
        {
            this.gridRow = gridRow;
            this.stateChanged = stateChanged;
        }

        /// <summary>Gets the <see cref="T:MControl.GridView.GridRow"></see> that has a changed state.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridRow"></see> that has a changed state.</returns>
        /// <filterpriority>1</filterpriority>
        public GridRow Row
        {
            get
            {
                return this.gridRow;
            }
        }

        /// <summary>Gets the state that has changed on the row.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridElementStates"></see> values indicating the state that has changed on the row.</returns>
        public GridElementStates StateChanged
        {
            get
            {
                return this.stateChanged;
            }
        }
    }
}

