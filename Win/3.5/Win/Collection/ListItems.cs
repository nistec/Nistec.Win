using System;
using System.Windows.Forms;   
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace MControl.Collections
{
    public delegate void ItemsCollectionChange(int index, ListItem value);

 
    public class ItemsCollection : ListItems
    {
        public event EventHandler ItemsCleared;

        public event ItemsCollectionChange ItemAdded;
        public event ItemsCollectionChange ItemRemoved;

        protected virtual void OnItemAdded(int index, ListItem item)
        {
            if (ItemAdded != null)
                ItemAdded(index, item);
        }

        protected virtual void OnItemRemoved(int index, ListItem item)
        {
            if (ItemRemoved != null)
                ItemRemoved(index, item);
        }

        protected virtual void OnItemsCleared(EventArgs e)
        {
            if (ItemsCleared != null)
                ItemsCleared(this, e);
        }

        public ItemsCollection()
        {
        }

        public new void Add(ListItem item)
        {
            base.Add(item);
            OnItemAdded(base.Count-1, item);
        }

        public override void AddRange(ListItem[] items)
        {
            foreach (ListItem item in items)
            {
                Add(item);
            }
            //base.AddRange(items);
            //OnItemAdded(EventArgs.Empty);
        }
        public override void AddRange(ListItems items)
        {
            foreach (ListItem item in items)
            {
                Add(item);
            }
            //base.AddRange(items);
            //OnItemAdded(EventArgs.Empty);
        }
        public override void Add(string s, int imageIndex)
        {
            Add(new ListItem(s,imageIndex));
        }

        public override void Add(object value, string s, int imageIndex)
        {
            Add(new ListItem(value,s, imageIndex));
        }

        public override void AddRange(string[] list, int imageIndex)
        {
            foreach (string s in list)
            {
                Add(s, imageIndex);
                //OnItemAdded(EventArgs.Empty);
            }
        }

        public override void AddRange(string[] list, int imageIndex, string startsWith)
        {
            foreach (string s in list)
            {
                if (s.StartsWith(startsWith))
                {
                    Add(s, imageIndex);
                    //OnItemAdded(EventArgs.Empty);
                }
            }
        }

        public override void AddRange(string[] list)
        {
            foreach (string s in list)
            {
                Add(s, -1);
                //OnItemAdded(EventArgs.Empty);
            }
        }

        public new void InsertRange(int index, IEnumerable<ListItem> items)
        {
            int i = index;
            foreach (ListItem item in items)
            {
                Insert(i,item);
                i++;
            }
        }
        public new void Insert(int index, ListItem item)
        {
            base.Insert(index, item);
            OnItemAdded(index, item);
        }

        public new void Clear()
        {
            base.Clear();
            OnItemsCleared(EventArgs.Empty);
        }
        public new void Remove(ListItem item)
        {
            int index = IndexOf(item);
            base.Remove(item);
            OnItemRemoved(index, item);
        }
        public new void RemoveAt(int index)
        {
            ListItem item=this[index];
            base.RemoveAt(index);
            OnItemRemoved(index, item);
        }

        [EditorBrowsable( EditorBrowsableState.Never)]
        public new void RemoveAll(Predicate<ListItem> match)
        {
            base.RemoveAll(match);
            //OnItemRemoved(EventArgs.Empty);
        }

        public new void RemoveRange(int index, int count)
        {
            for (int i = 0; i < count;i++ )
            {
                RemoveAt(i + index);
            }
            //base.RemoveRange(index, count);
            //OnItemRemoved(EventArgs.Empty);
        }

  
    }

    
    public class ListItems : List<ListItem>
    {
        private int maxWidth;
        WordComparer compare;
 
        public int MaxWidth
        {
            get { return maxWidth; }
        }

        public ListItems()
        {
            maxWidth = 0;
            compare = new WordComparer();
        }

        public new void Add(ListItem item)
        {
            base.Add(item);
            maxWidth = Math.Max(maxWidth, item.Width);
        }

        public virtual void AddRange(ListItem[] items)
        {
            base.AddRange(items);
            ComputeWidth();
        }
        public virtual void AddRange(ListItems items)
        {
            base.AddRange(items);
            maxWidth = Math.Max(maxWidth, items.MaxWidth);
        }
        public virtual void Add(string s, int imageIndex)
        {
            ListItem item = new ListItem(s, imageIndex);
            base.Add(item);
            maxWidth = Math.Max(maxWidth, item.Width);
        }
        public virtual void Add(object value,string s, int imageIndex)
        {
            ListItem item = new ListItem(value,s, imageIndex);
            base.Add(item);
            maxWidth = Math.Max(maxWidth, item.Width);
        }
        public virtual void AddRange(string[] list, int imageIndex)
        {
            foreach (string s in list)
            {
                ListItem item = new ListItem(s, imageIndex);
                base.Add(item);
                maxWidth = Math.Max(maxWidth, item.Width);
            }
        }

        public virtual void AddRange(string[] list, int imageIndex, string startsWith)
        {
            foreach (string s in list)
            {
                if (s.StartsWith(startsWith))
                {
                    ListItem item = new ListItem(s, imageIndex);
                    base.Add(item);
                    maxWidth = Math.Max(maxWidth, item.Width);
                }
            }
        }

        public virtual void AddRange(string[] list)
        {
            foreach (string s in list)
            {
                ListItem item = new ListItem(s, 0);
                base.Add(item);
                maxWidth = Math.Max(maxWidth, item.Width);
            }
        }

        public string[] ToStringArray()
        {
            string[] list=new string[this.Count];
            int i = 0;
            foreach (ListItem o in this)
            {
                list[i]=o.Text;
                i++;
            }
            return list;
        }

        public ListItems GetItemsStartsWith(string word)
        {
            ListItems items = new ListItems();
            int maxW = 0;
            foreach (ListItem o in this)
            {
                if (o.Text.ToLower().StartsWith(word.ToLower()))
                {
                    items.Add(o);
                    maxW = Math.Max(maxW, o.Width);
                }
            }
            items.maxWidth = maxW;
            return items;
        }
        public new void Clear()
        {
            base.Clear();
            this.maxWidth = 0;
        }
        public new void Sort()
        {
            base.Sort(compare);
        }

        public int ComputeWidth()
        {
            int maxW = 0;
            foreach (ListItem o in this)
            {
                maxW = Math.Max(maxW, o.Width);
            }
            maxWidth = maxW;
            return maxWidth;
        }

        public class WordComparer : IComparer<ListItem>
        {

            public int Compare(ListItem wx, ListItem wy)
            {
                string x = wx.Text;
                string y = wy.Text;

                if (x == null)
                {
                    if (y == null)
                    {
                        // If x is null and y is null, they're
                        // equal. 
                        return 0;
                    }
                    else
                    {
                        // If x is null and y is not null, y
                        // is greater. 
                        return -1;
                    }
                }
                else
                {
                    // If x is not null...
                    //
                    if (y == null)
                    // ...and y is null, x is greater.
                    {
                        return 1;
                    }
                    else
                    {
                        return x.CompareTo(y);
                    }
                }
            }

        }

    }


    public struct ListItem
    {
        public const int charWidth = 6;
        public const int iconSpace = 40;

        public readonly string Text;
        public readonly int Length;
        public readonly int ImageIndex;
        public readonly int Width;
        public readonly object Value;

  
        public ListItem(string text)
            : this(text, 0)
        {
        }

        public ListItem(string text, int imageIndex)
        {
            Value = text;
            Text = text;
            ImageIndex = imageIndex;
            Length = text.Length;
            Width = (Length * charWidth) + iconSpace;
        }

        public ListItem( object value,string text, int imageIndex)
        {
            Value = value;
            Text = text;
            ImageIndex = imageIndex;
            Length = text.Length;
            Width = (Length * charWidth) + iconSpace;
        }

        public override string ToString()
        {
            return Text;
        }
    }



}
