namespace Nistec.Printing.View
{
    using System;
    using System.Collections;

    public class PagesCollection : CollectionBase
    {
        public void AddPage(Page page)
        {
            base.List.Add(page);
        }

        public void AddPage(int index, Page page)
        {
            base.List.Insert(index, page);
        }

        public Page GetPage(int index)
        {
            return (Page) base.List[index];
        }

        public int IndexOf(Page value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, Page value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(Page value)
        {
            base.List.Remove(value);
        }

        public void Remove(int index)
        {
            base.List.RemoveAt(index);
        }

        public int MaxPages
        {
            get
            {
                return base.Count;
            }
        }
    }
}

