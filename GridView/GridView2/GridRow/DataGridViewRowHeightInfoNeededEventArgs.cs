namespace MControl.GridView
{
    using System;
    using System.Globalization;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.RowHeightInfoNeeded"></see> event of a <see cref="T:MControl.GridView.Grid"></see>. </summary>
    public class GridRowHeightInfoNeededEventArgs : EventArgs
    {
        private int height = -1;
        private int minimumHeight = -1;
        private int rowIndex = -1;

        internal GridRowHeightInfoNeededEventArgs()
        {
        }

        internal void SetProperties(int rowIndex, int height, int minimumHeight)
        {
            this.rowIndex = rowIndex;
            this.height = height;
            this.minimumHeight = minimumHeight;
        }

        /// <summary>Gets or sets the height of the row the event occurred for.</summary>
        /// <returns>The row height. </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is greater than 65,536. </exception>
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                if (value < this.minimumHeight)
                {
                    value = this.minimumHeight;
                }
                if (value > 0x10000)
                {
                    object[] args = new object[] { "Height", value.ToString(CultureInfo.CurrentCulture), 0x10000.ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentOutOfRangeException("Height", MControl.GridView.RM.GetString("InvalidHighBoundArgumentEx", args));
                }
                this.height = value;
            }
        }

        /// <summary>Gets or sets the minimum height of the row the event occurred for. </summary>
        /// <returns>The minimum row height.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 2.</exception>
        public int MinimumHeight
        {
            get
            {
                return this.minimumHeight;
            }
            set
            {
                if (value < 2)
                {
                    object[] args = new object[] { 2.ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentOutOfRangeException("MinimumHeight", value, MControl.GridView.RM.GetString("GridBand_MinimumHeightSmallerThanOne", args));
                }
                if (this.height < value)
                {
                    this.height = value;
                }
                this.minimumHeight = value;
            }
        }

        /// <summary>Gets the index of the row associated with this <see cref="T:MControl.GridView.GridRowHeightInfoNeededEventArgs"></see>.</summary>
        /// <returns>The zero-based index of the row the event occurred for.</returns>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}

