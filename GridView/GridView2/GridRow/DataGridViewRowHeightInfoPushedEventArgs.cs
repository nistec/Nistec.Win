namespace MControl.GridView
{
    using System;
    using System.ComponentModel;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.RowHeightInfoPushed"></see> event of a <see cref="T:MControl.GridView.Grid"></see>. </summary>
    public class GridRowHeightInfoPushedEventArgs : HandledEventArgs
    {
        private int height;
        private int minimumHeight;
        private int rowIndex;

        internal GridRowHeightInfoPushedEventArgs(int rowIndex, int height, int minimumHeight) : base(false)
        {
            this.rowIndex = rowIndex;
            this.height = height;
            this.minimumHeight = minimumHeight;
        }

        /// <summary>Gets the height of the row the event occurred for.</summary>
        /// <returns>The row height, in pixels.</returns>
        public int Height
        {
            get
            {
                return this.height;
            }
        }

        /// <summary>Gets the minimum height of the row the event occurred for.</summary>
        /// <returns>The minimum row height, in pixels.</returns>
        public int MinimumHeight
        {
            get
            {
                return this.minimumHeight;
            }
        }

        /// <summary>Gets the index of the row the event occurred for.</summary>
        /// <returns>The zero-based index of the row.</returns>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}

