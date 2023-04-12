namespace MControl.GridView
{
    using System;

    internal class GridCellStyleChangedEventArgs : EventArgs
    {
        private bool changeAffectsPreferredSize;

        internal GridCellStyleChangedEventArgs()
        {
        }

        internal bool ChangeAffectsPreferredSize
        {
            get
            {
                return this.changeAffectsPreferredSize;
            }
            set
            {
                this.changeAffectsPreferredSize = value;
            }
        }
    }
}

