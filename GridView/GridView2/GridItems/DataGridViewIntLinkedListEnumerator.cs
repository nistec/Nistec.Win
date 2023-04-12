namespace MControl.GridView
{
    using System;
    using System.Collections;

    internal class GridIntLinkedListEnumerator : IEnumerator
    {
        private GridIntLinkedListElement current;
        private GridIntLinkedListElement headElement;
        private bool reset;

        public GridIntLinkedListEnumerator(GridIntLinkedListElement headElement)
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
                return this.current.Int;
            }
        }
    }
}

