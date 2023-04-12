namespace MControl.GridView
{
    using System;

    internal class GridIntLinkedListElement
    {
        private int integer;
        private GridIntLinkedListElement next;

        public GridIntLinkedListElement(int integer)
        {
            this.integer = integer;
        }

        public int Int
        {
            get
            {
                return this.integer;
            }
            set
            {
                this.integer = value;
            }
        }

        public GridIntLinkedListElement Next
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

