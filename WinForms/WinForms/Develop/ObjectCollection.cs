using System;
using System.Collections.Generic;
using System.Text;

namespace mControl.WinCtl.Develop
{
  [ListBindable(false)]
public class ObjectCollection : IList, ICollection, IEnumerable
{
    // Fields
    private IComparer comparer;
    private ArrayList innerList;
    private ComboBox owner;

    // Methods
    public ObjectCollection(ComboBox owner)
    {
        this.owner = owner;
    }

    public int Add(object item)
    {
        this.owner.CheckNoDataSource();
        int num = this.AddInternal(item);
        if (this.owner.UpdateNeeded() && (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems))
        {
            this.owner.SetAutoComplete(false, false);
        }
        return num;
    }

    private int AddInternal(object item)
    {
        if (item == null)
        {
            throw new ArgumentNullException("item");
        }
        int index = -1;
        if (!this.owner.sorted)
        {
            this.InnerList.Add(item);
        }
        else
        {
            index = this.InnerList.BinarySearch(item, this.Comparer);
            if (index < 0)
            {
                index = ~index;
            }
            this.InnerList.Insert(index, item);
        }
        bool flag = false;
        try
        {
            if (this.owner.sorted)
            {
                if (this.owner.IsHandleCreated)
                {
                    this.owner.NativeInsert(index, item);
                }
            }
            else
            {
                index = this.InnerList.Count - 1;
                if (this.owner.IsHandleCreated)
                {
                    this.owner.NativeAdd(item);
                }
            }
            flag = true;
        }
        finally
        {
            if (!flag)
            {
                this.InnerList.Remove(item);
            }
        }
        return index;
    }

    public void AddRange(object[] items)
    {
        this.owner.CheckNoDataSource();
        this.owner.BeginUpdate();
        try
        {
            this.AddRangeInternal(items);
        }
        finally
        {
            this.owner.EndUpdate();
        }
    }

    internal void AddRangeInternal(IList items)
    {
        if (items == null)
        {
            throw new ArgumentNullException("items");
        }
        foreach (object obj2 in items)
        {
            this.AddInternal(obj2);
        }
        if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
        {
            this.owner.SetAutoComplete(false, false);
        }
    }

    public void Clear()
    {
        this.owner.CheckNoDataSource();
        this.ClearInternal();
    }

    internal void ClearInternal()
    {
        if (this.owner.IsHandleCreated)
        {
            this.owner.NativeClear();
        }
        this.InnerList.Clear();
        this.owner.selectedIndex = -1;
        if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
        {
            this.owner.SetAutoComplete(false, true);
        }
    }

    public bool Contains(object value)
    {
        return (this.IndexOf(value) != -1);
    }

    public void CopyTo(object[] destination, int arrayIndex)
    {
        this.InnerList.CopyTo(destination, arrayIndex);
    }

    public IEnumerator GetEnumerator()
    {
        return this.InnerList.GetEnumerator();
    }

    public int IndexOf(object value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }
        return this.InnerList.IndexOf(value);
    }

    public void Insert(int index, object item)
    {
        this.owner.CheckNoDataSource();
        if (item == null)
        {
            throw new ArgumentNullException("item");
        }
        if ((index < 0) || (index > this.InnerList.Count))
        {
            throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
        }
        if (this.owner.sorted)
        {
            this.Add(item);
        }
        else
        {
            this.InnerList.Insert(index, item);
            if (this.owner.IsHandleCreated)
            {
                bool flag = false;
                try
                {
                    this.owner.NativeInsert(index, item);
                    flag = true;
                }
                finally
                {
                    if (flag)
                    {
                        if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
                        {
                            this.owner.SetAutoComplete(false, false);
                        }
                    }
                    else
                    {
                        this.InnerList.RemoveAt(index);
                    }
                }
            }
        }
    }

    public void Remove(object value)
    {
        int index = this.InnerList.IndexOf(value);
        if (index != -1)
        {
            this.RemoveAt(index);
        }
    }

    public void RemoveAt(int index)
    {
        this.owner.CheckNoDataSource();
        if ((index < 0) || (index >= this.InnerList.Count))
        {
            throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
        }
        if (this.owner.IsHandleCreated)
        {
            this.owner.NativeRemoveAt(index);
        }
        this.InnerList.RemoveAt(index);
        if (!this.owner.IsHandleCreated && (index < this.owner.selectedIndex))
        {
            this.owner.selectedIndex--;
        }
        if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
        {
            this.owner.SetAutoComplete(false, false);
        }
    }

    internal void SetItemInternal(int index, object value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }
        if ((index < 0) || (index >= this.InnerList.Count))
        {
            throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
        }
        this.InnerList[index] = value;
        if (this.owner.IsHandleCreated)
        {
            bool flag = index == this.owner.SelectedIndex;
            if (string.Compare(this.owner.GetItemText(value), this.owner.NativeGetItemText(index), true, CultureInfo.CurrentCulture) != 0)
            {
                this.owner.NativeRemoveAt(index);
                this.owner.NativeInsert(index, value);
                if (flag)
                {
                    this.owner.SelectedIndex = index;
                    this.owner.UpdateText();
                }
                if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
                {
                    this.owner.SetAutoComplete(false, false);
                }
            }
            else if (flag)
            {
                this.owner.OnSelectedItemChanged(EventArgs.Empty);
                this.owner.OnSelectedIndexChanged(EventArgs.Empty);
            }
        }
    }

    void ICollection.CopyTo(Array destination, int index)
    {
        this.InnerList.CopyTo(destination, index);
    }

    int IList.Add(object item)
    {
        return this.Add(item);
    }

    // Properties
    private IComparer Comparer
    {
        get
        {
            if (this.comparer == null)
            {
                this.comparer = new ComboBox.ItemComparer(this.owner);
            }
            return this.comparer;
        }
    }

    public int Count
    {
        get
        {
            return this.InnerList.Count;
        }
    }

    private ArrayList InnerList
    {
        get
        {
            if (this.innerList == null)
            {
                this.innerList = new ArrayList();
            }
            return this.innerList;
        }
    }

    public bool IsReadOnly
    {
        get
        {
            return false;
        }
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual object this[int index]
    {
        get
        {
            if ((index < 0) || (index >= this.InnerList.Count))
            {
                throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
            }
            return this.InnerList[index];
        }
        set
        {
            this.owner.CheckNoDataSource();
            this.SetItemInternal(index, value);
        }
    }

    bool ICollection.IsSynchronized
    {
        get
        {
            return false;
        }
    }

    object ICollection.SyncRoot
    {
        get
        {
            return this;
        }
    }

    bool IList.IsFixedSize
    {
        get
        {
            return false;
        }
    }
}


}
