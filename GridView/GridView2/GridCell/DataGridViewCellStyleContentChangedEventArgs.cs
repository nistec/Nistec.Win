namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellStyleContentChanged"></see> event. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellStyleContentChangedEventArgs : EventArgs
    {
        private bool changeAffectsPreferredSize;
        private GridCellStyle gridCellStyle;

        internal GridCellStyleContentChangedEventArgs(GridCellStyle gridCellStyle, bool changeAffectsPreferredSize)
        {
            this.gridCellStyle = gridCellStyle;
            this.changeAffectsPreferredSize = changeAffectsPreferredSize;
        }

        /// <summary>Gets the changed <see cref="T:MControl.GridView.GridCellStyle"></see>.</summary>
        /// <returns>The changed <see cref="T:MControl.GridView.GridCellStyle"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public GridCellStyle CellStyle
        {
            get
            {
                return this.gridCellStyle;
            }
        }

        /// <summary>Gets the scope that is affected by the changed cell style.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyleScopes"></see> that indicates which <see cref="T:MControl.GridView.Grid"></see> entity owns the cell style that changed.</returns>
        /// <filterpriority>1</filterpriority>
        public GridCellStyleScopes CellStyleScope
        {
            get
            {
                return this.gridCellStyle.Scope;
            }
        }

        internal bool ChangeAffectsPreferredSize
        {
            get
            {
                return this.changeAffectsPreferredSize;
            }
        }
    }
}

