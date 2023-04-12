namespace MControl.GridView
{
    using System;
    using System.Windows.Forms;

    /// <summary>Defines common functionality for a cell that allows the manipulation of its value.</summary>
    /// <filterpriority>2</filterpriority>
    public interface IGridEditingCell
    {
        /// <summary>Retrieves the formatted value of the cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the formatted version of the cell contents.</returns>
        /// <param name="context">A bitwise combination of <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values that specifies the context in which the data is needed.</param>
        object GetEditingCellFormattedValue(GridDataErrorContexts context);
        /// <summary>Prepares the currently selected cell for editing</summary>
        /// <param name="selectAll">true to select the cell contents; otherwise, false.</param>
        void PrepareEditingCellForEdit(bool selectAll);

        /// <summary>Gets or sets the formatted value of the cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that contains the cell's value.</returns>
        object EditingCellFormattedValue { get; set; }

        /// <summary>Gets or sets a value indicating whether the value of the cell has changed.</summary>
        /// <returns>true if the value of the cell has changed; otherwise, false.</returns>
        bool EditingCellValueChanged { get; set; }
    }
}

