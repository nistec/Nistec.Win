namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellErrorTextNeeded"></see> event of a <see cref="T:MControl.GridView.Grid"></see> control. </summary>
    public class GridCellErrorTextNeededEventArgs : GridCellEventArgs
    {
        private string errorText;

        internal GridCellErrorTextNeededEventArgs(int columnIndex, int rowIndex, string errorText) : base(columnIndex, rowIndex)
        {
            this.errorText = errorText;
        }

        /// <summary>Gets or sets the message that is displayed when the cell is selected.</summary>
        /// <returns>The error message.</returns>
        public string ErrorText
        {
            get
            {
                return this.errorText;
            }
            set
            {
                this.errorText = value;
            }
        }
    }
}

