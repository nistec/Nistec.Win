namespace MControl.GridView
{
    using System;

    internal class GridCellLinkedListElement
    {
        private MControl.GridView.GridCell gridCell;
        private GridCellLinkedListElement next;

        public GridCellLinkedListElement(MControl.GridView.GridCell gridCell)
        {
            this.gridCell = gridCell;
        }

        public MControl.GridView.GridCell GridCell
        {
            get
            {
                return this.gridCell;
            }
        }

        public GridCellLinkedListElement Next
        {
            get
            {
                return this.next;
            }
            set
            {
                this.next = value;
            }
        }
    }
}

