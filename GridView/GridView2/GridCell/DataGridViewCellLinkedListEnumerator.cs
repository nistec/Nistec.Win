namespace MControl.GridView
{
    using System;
    using System.Collections;

    internal class GridCellLinkedListEnumerator : IEnumerator
    {
        private GridCellLinkedListElement current;
        private GridCellLinkedListElement headElement;
        private bool reset;

        public GridCellLinkedListEnumerator(GridCellLinkedListElement headElement)
        {
            this.headElement = headElement;
            this.reset = true;
        }

        bool IEnumerator.MoveNext()
        {
            if (this.reset)
            {
                this.current = this.headElement;
                this.reset = false;
            }
            else
            {
                this.current = this.current.Next;
            }
            return (this.current != null);
        }

        void IEnumerator.Reset()
        {
            this.reset = true;
            this.current = null;
        }

        object IEnumerator.Current
        {
            get
            {
                return this.current.GridCell;
            }
        }
    }
}

