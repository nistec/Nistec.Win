namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.DataError"></see> event.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridDataErrorEventArgs : GridCellCancelEventArgs
    {
        private GridDataErrorContexts context;
        private System.Exception exception;
        private bool throwException;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridDataErrorEventArgs"></see> class.</summary>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="columnIndex">The column index of the cell that raised the <see cref="E:MControl.GridView.Grid.DataError"></see>.</param>
        /// <param name="context">A bitwise combination of <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values indicating the context in which the error occurred. </param>
        /// <param name="rowIndex">The row index of the cell that raised the <see cref="E:MControl.GridView.Grid.DataError"></see>.</param>
        public GridDataErrorEventArgs(System.Exception exception, int columnIndex, int rowIndex, GridDataErrorContexts context) : base(columnIndex, rowIndex)
        {
            this.exception = exception;
            this.context = context;
        }

        /// <summary>Gets details about the state of the <see cref="T:MControl.GridView.Grid"></see> when the error occurred.</summary>
        /// <returns>A bitwise combination of the <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values that specifies the context in which the error occurred.</returns>
        /// <filterpriority>1</filterpriority>
        public GridDataErrorContexts Context
        {
            get
            {
                return this.context;
            }
        }

        /// <summary>Gets the exception that represents the error.</summary>
        /// <returns>An <see cref="T:System.Exception"></see> that represents the error.</returns>
        /// <filterpriority>1</filterpriority>
        public System.Exception Exception
        {
            get
            {
                return this.exception;
            }
        }

        /// <summary>Gets or sets a value indicating whether to throw the exception after the <see cref="T:MControl.GridView.GridDataErrorEventHandler"></see> delegate is finished with it.</summary>
        /// <returns>true if the exception should be thrown; otherwise, false. The default is false.</returns>
        /// <exception cref="T:System.ArgumentException">When setting this property to true, the <see cref="P:MControl.GridView.GridDataErrorEventArgs.Exception"></see> property value is null.</exception>
        /// <filterpriority>1</filterpriority>
        public bool ThrowException
        {
            get
            {
                return this.throwException;
            }
            set
            {
                if (value && (this.exception == null))
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("Grid_CannotThrowNullException"));
                }
                this.throwException = value;
            }
        }
    }
}

