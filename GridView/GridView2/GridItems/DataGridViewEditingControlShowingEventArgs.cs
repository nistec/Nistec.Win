namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.EditingControlShowing"></see> event.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridEditingControlShowingEventArgs : EventArgs
    {
        private GridCellStyle cellStyle;
        private System.Windows.Forms.Control control;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridEditingControlShowingEventArgs"></see> class.</summary>
        /// <param name="cellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> representing the style of the cell being edited.</param>
        /// <param name="control">A <see cref="T:System.Windows.Forms.Control"></see> in which the user will edit the selected cell's contents.</param>
        public GridEditingControlShowingEventArgs(System.Windows.Forms.Control control, GridCellStyle cellStyle)
        {
            this.control = control;
            this.cellStyle = cellStyle;
        }

        /// <summary>Gets or sets the cell style of the edited cell.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> representing the style of the cell being edited.</returns>
        /// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
        public GridCellStyle CellStyle
        {
            get
            {
                return this.cellStyle;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.cellStyle = value;
            }
        }

        /// <summary>The control shown to the user for editing the selected cell's value.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Control"></see> that displays an area for the user to enter or change the selected cell's value.</returns>
        /// <filterpriority>1</filterpriority>
        public System.Windows.Forms.Control Control
        {
            get
            {
                return this.control;
            }
        }
    }
}

