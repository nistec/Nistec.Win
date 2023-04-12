namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.RowErrorTextNeeded"></see> event of a <see cref="T:MControl.GridView.Grid"></see> control. </summary>
    public class GridRowErrorTextNeededEventArgs : EventArgs
    {
        private string errorText;
        private int rowIndex;

        internal GridRowErrorTextNeededEventArgs(int rowIndex, string errorText)
        {
            this.rowIndex = rowIndex;
            this.errorText = errorText;
        }

        /// <summary>Gets or sets the error text for the row.</summary>
        /// <returns>A string that represents the error text for the row.</returns>
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

        /// <summary>Gets the row that raised the <see cref="E:MControl.GridView.Grid.RowErrorTextNeeded"></see> event.</summary>
        /// <returns>The zero based row index for the row.</returns>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}

