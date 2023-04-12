namespace MControl.GridView
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class GridCellLinkedList : IEnumerable
    {
        private int count;
        private GridCellLinkedListElement headElement;
        private GridCellLinkedListElement lastAccessedElement;
        private int lastAccessedIndex = -1;

        public void Add(GridCell gridCell)
        {
            GridCellLinkedListElement element = new GridCellLinkedListElement(gridCell);
            if (this.headElement != null)
            {
                element.Next = this.headElement;
            }
            this.headElement = element;
            this.count++;
            this.lastAccessedElement = null;
            this.lastAccessedIndex = -1;
        }

        public void Clear()
        {
            this.lastAccessedElement = null;
            this.lastAccessedIndex = -1;
            this.headElement = null;
            this.count = 0;
        }

        public bool Contains(GridCell gridCell)
        {
            int num = 0;
            GridCellLinkedListElement headElement = this.headElement;
            while (headElement != null)
            {
                if (headElement.GridCell == gridCell)
                {
                    this.lastAccessedElement = headElement;
                    this.lastAccessedIndex = num;
                    return true;
                }
                headElement = headElement.Next;
                num++;
            }
            return false;
        }

        public bool Remove(GridCell gridCell)
        {
            GridCellLinkedListElement element = null;
            GridCellLinkedListElement headElement = this.headElement;
            while (headElement != null)
            {
                if (headElement.GridCell == gridCell)
                {
                    break;
                }
                element = headElement;
                headElement = headElement.Next;
            }
            if (headElement.GridCell != gridCell)
            {
                return false;
            }
            GridCellLinkedListElement next = headElement.Next;
            if (element == null)
            {
                this.headElement = next;
            }
            else
            {
                element.Next = next;
            }
            this.count--;
            this.lastAccessedElement = null;
            this.lastAccessedIndex = -1;
            return true;
        }

        public int RemoveAllCellsAtBand(bool column, int bandIndex)
        {
            int num = 0;
            GridCellLinkedListElement element = null;
            GridCellLinkedListElement headElement = this.headElement;
            while (headElement != null)
            {
                if ((column && (headElement.GridCell.ColumnIndex == bandIndex)) || (!column && (headElement.GridCell.RowIndex == bandIndex)))
                {
                    GridCellLinkedListElement next = headElement.Next;
                    if (element == null)
                    {
                        this.headElement = next;
                    }
                    else
                    {
                        element.Next = next;
                    }
                    headElement = next;
                    this.count--;
                    this.lastAccessedElement = null;
                    this.lastAccessedIndex = -1;
                    num++;
                }
                else
                {
                    element = headElement;
                    headElement = headElement.Next;
                }
            }
            return num;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new GridCellLinkedListEnumerator(this.headElement);
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public GridCell HeadCell
        {
            get
            {
                return this.headElement.GridCell;
            }
        }

        public GridCell this[int index]
        {
            get
            {
                if ((this.lastAccessedIndex != -1) && (index >= this.lastAccessedIndex))
                {
                    while (this.lastAccessedIndex < index)
                    {
                        this.lastAccessedElement = this.lastAccessedElement.Next;
                        this.lastAccessedIndex++;
                    }
                    return this.lastAccessedElement.GridCell;
                }
                GridCellLinkedListElement headElement = this.headElement;
                for (int i = index; i > 0; i--)
                {
                    headElement = headElement.Next;
                }
                this.lastAccessedElement = headElement;
                this.lastAccessedIndex = index;
                return headElement.GridCell;
            }
        }
    }
}

