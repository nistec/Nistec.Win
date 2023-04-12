namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.AutoSizeColumnsModeChanged"></see> event. </summary>
    public class GridAutoSizeColumnsModeEventArgs : EventArgs
    {
        private GridAutoSizeColumnMode[] previousModes;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridAutoSizeColumnsModeEventArgs"></see> class. </summary>
        /// <param name="previousModes">An array of <see cref="T:MControl.GridView.GridAutoSizeColumnMode"></see> values representing the previous <see cref="P:MControl.GridView.GridColumn.AutoSizeMode"></see> property values of each column. </param>
        public GridAutoSizeColumnsModeEventArgs(GridAutoSizeColumnMode[] previousModes)
        {
            this.previousModes = previousModes;
        }

        /// <summary>Gets an array of the previous values of the column <see cref="P:MControl.GridView.GridColumn.AutoSizeMode"></see> properties.</summary>
        /// <returns>An array of <see cref="T:MControl.GridView.GridAutoSizeColumnMode"></see> values representing the previous values of the column <see cref="P:MControl.GridView.GridColumn.AutoSizeMode"></see> properties.</returns>
        public GridAutoSizeColumnMode[] PreviousModes
        {
            get
            {
                return this.previousModes;
            }
        }
    }
}

