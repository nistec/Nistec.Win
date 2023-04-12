namespace MControl.GridView
{
    using System;
    using System.ComponentModel;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.UserDeletingRow"></see> event of a <see cref="T:MControl.GridView.Grid"></see>. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridRowCancelEventArgs : CancelEventArgs
    {
        private GridRow gridRow;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowCancelEventArgs"></see> class. </summary>
        /// <param name="gridRow">The row the user is deleting.</param>
        public GridRowCancelEventArgs(GridRow gridRow)
        {
            this.gridRow = gridRow;
        }

        /// <summary>Gets the row that the user is deleting.</summary>
        /// <returns>The row that the user deleted.</returns>
        /// <filterpriority>1</filterpriority>
        public GridRow Row
        {
            get
            {
                return this.gridRow;
            }
        }
    }
}

