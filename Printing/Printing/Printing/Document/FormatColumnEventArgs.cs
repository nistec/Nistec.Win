
using System;

namespace Nistec.Printing
{

    /// <summary>
    /// A delegate to handle column formatting.
    /// </summary>
    public delegate void FormatColumnHandler (object sender, FormatColumnEventArgs e);

    /// <summary>
    /// The Event Arguments used when a FormatColumn event is raised.
    /// </summary>
    public class FormatColumnEventArgs : EventArgs
    {
        object originalValue;
        string stringValue;
    
        /// <summary>
        /// The original value of the data to be printed in a column.
        /// </summary>
        public object OriginalValue
        {
            get { return this.originalValue; }
            set { this.originalValue = value; }
        }

        /// <summary>
        /// The formatted string that should be printed in place of the
        /// original column data.
        /// </summary>
        public string StringValue
        {
            get { return this.stringValue; }
            set { this.stringValue = value; }
        }

    } // class



}
