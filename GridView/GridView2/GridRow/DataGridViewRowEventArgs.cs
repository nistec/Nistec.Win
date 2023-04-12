namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for row-related <see cref="T:MControl.GridView.Grid"></see> events. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridRowEventArgs : EventArgs
    {
        private GridRow gridRow;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowEventArgs"></see> class. </summary>
        /// <param name="gridRow">The <see cref="T:MControl.GridView.GridRow"></see> that the event occurred for.</param>
        /// <exception cref="T:System.ArgumentNullException">gridRow is null.</exception>
        public GridRowEventArgs(GridRow gridRow)
        {
            if (gridRow == null)
            {
                throw new ArgumentNullException("gridRow");
            }
            this.gridRow = gridRow;
        }

        /// <summary>Gets the <see cref="T:MControl.GridView.GridRow"></see> associated with the event.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridRow"></see> associated with the event.</returns>
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

