namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="T:MControl.GridView.Grid"></see><see cref="E:MControl.GridView.Grid.AutoSizeRowsModeChanged"></see> and <see cref="E:MControl.GridView.Grid.RowHeadersWidthSizeModeChanged"></see> events.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridAutoSizeModeEventArgs : EventArgs
    {
        private bool previousModeAutoSized;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridAutoSizeModeEventArgs"></see> class.</summary>
        /// <param name="previousModeAutoSized">true if the <see cref="P:MControl.GridView.Grid.AutoSizeRowsMode"></see> property was previously set to any <see cref="T:MControl.GridView.GridAutoSizeRowsMode"></see> value other than <see cref="F:MControl.GridView.GridAutoSizeRowsMode.None"></see> or the <see cref="P:MControl.GridView.Grid.RowHeadersWidthSizeMode"></see> property was previously set to any <see cref="T:MControl.GridView.GridRowHeadersWidthSizeMode"></see> value other than <see cref="F:MControl.GridView.GridRowHeadersWidthSizeMode.DisableResizing"></see> or <see cref="F:MControl.GridView.GridRowHeadersWidthSizeMode.EnableResizing"></see>; otherwise, false.</param>
        public GridAutoSizeModeEventArgs(bool previousModeAutoSized)
        {
            this.previousModeAutoSized = previousModeAutoSized;
        }

        /// <summary>Gets a value specifying whether the <see cref="T:MControl.GridView.Grid"></see> was previously set to automatically resize.</summary>
        /// <returns>true if the <see cref="P:MControl.GridView.Grid.AutoSizeRowsMode"></see> property was previously set to any <see cref="T:MControl.GridView.GridAutoSizeRowsMode"></see> value other than <see cref="F:MControl.GridView.GridAutoSizeRowsMode.None"></see> or the <see cref="P:MControl.GridView.Grid.RowHeadersWidthSizeMode"></see> property was previously set to any <see cref="T:MControl.GridView.GridRowHeadersWidthSizeMode"></see> value other than <see cref="F:MControl.GridView.GridRowHeadersWidthSizeMode.DisableResizing"></see> or <see cref="F:MControl.GridView.GridRowHeadersWidthSizeMode.EnableResizing"></see>; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool PreviousModeAutoSized
        {
            get
            {
                return this.previousModeAutoSized;
            }
        }
    }
}

