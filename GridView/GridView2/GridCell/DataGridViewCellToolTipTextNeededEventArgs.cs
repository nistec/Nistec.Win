namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellToolTipTextNeeded"></see> event. </summary>
    public class GridCellToolTipTextNeededEventArgs : GridCellEventArgs
    {
        private string toolTipText;

        internal GridCellToolTipTextNeededEventArgs(int columnIndex, int rowIndex, string toolTipText) : base(columnIndex, rowIndex)
        {
            this.toolTipText = toolTipText;
        }

        /// <summary>Gets or sets the ToolTip text.</summary>
        /// <returns>The current ToolTip text.</returns>
        public string ToolTipText
        {
            get
            {
                return this.toolTipText;
            }
            set
            {
                this.toolTipText = value;
            }
        }
    }
}

