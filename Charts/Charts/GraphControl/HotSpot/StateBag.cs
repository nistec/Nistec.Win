namespace System.Web.UI
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Security.Permissions;
    using System.Web;
    using System.Web.Util;

    /// <summary>Manages the view state of ASP.NET server controls, including pages. This class cannot be inherited.</summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    public sealed class StateBag : IStateManager, IDictionary, ICollection, IEnumerable
    {
        private IDictionary bag;
        private bool ignoreCase;
        private bool marked;

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateBag"></see> class. This is the default constructor for this class.</summary>
        public StateBag() : this(false)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateBag"></see> class that allows stored state values to be case-insensitive.</summary>
        /// <param name="ignoreCase">true to ignore case; otherwise, false. </param>
        public StateBag(bool ignoreCase)
        {
            this.marked = false;
            this.ignoreCase = ignoreCase;
            this.bag = this.CreateBag();
        }

        /// <summary>Adds a new <see cref="T:System.Web.UI.StateItem"></see> object to the <see cref="T:System.Web.UI.StateBag"></see> object. If the item already exists in the <see cref="T:System.Web.UI.StateBag"></see> object, this method updates the value of the item.</summary>
        /// <returns>Returns a <see cref="T:System.Web.UI.StateItem"></see> that represents the object added to view state.</returns>
        /// <param name="value">The value of the item to add to the <see cref="T:System.Web.UI.StateBag"></see>. </param>
        /// <param name="key">The attribute name for the <see cref="T:System.Web.UI.StateItem"></see>. </param>
        /// <exception cref="T:System.ArgumentException">key is null.- or -The number of characters in key is 0. </exception>
        public StateItem Add(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw ExceptionUtil.ParameterNullOrEmpty("key");
            }
            StateItem item = this.bag[key] as StateItem;
            if (item == null)
            {
                if ((value != null) || this.marked)
                {
                    item = new StateItem(value);
                    this.bag.Add(key, item);
                }
            }
            else if ((value == null) && !this.marked)
            {
                this.bag.Remove(key);
            }
            else
            {
                item.Value = value;
            }
            if ((item != null) && this.marked)
            {
                item.IsDirty = true;
            }
            return item;
        }

        /// <summary>Removes all items from the current <see cref="T:System.Web.UI.StateBag"></see> object.</summary>
        public void Clear()
        {
            this.bag.Clear();
        }

        private IDictionary CreateBag()
        {
            return new HybridDictionary(this.ignoreCase);
        }

        /// <summary>Returns an enumerator that iterates over all the key/value pairs of the <see cref="T:System.Web.UI.StateItem"></see> objects stored in the <see cref="T:System.Web.UI.StateBag"></see> object.</summary>
        /// <returns>The enumerator to iterate through the state bag.</returns>
        public IDictionaryEnumerator GetEnumerator()
        {
            return this.bag.GetEnumerator();
        }

        /// <summary>Checks a <see cref="T:System.Web.UI.StateItem"></see> object stored in the <see cref="T:System.Web.UI.StateBag"></see> object to evaluate whether it has been modified since the call to <see cref="M:System.Web.UI.Control.TrackViewState"></see>.</summary>
        /// <returns>true if the item has been modified; otherwise, false.</returns>
        /// <param name="key">The key of the item to check. </param>
        public bool IsItemDirty(string key)
        {
            StateItem item = this.bag[key] as StateItem;
            return ((item != null) && item.IsDirty);
        }

        internal void LoadViewState(object state)
        {
            if (state != null)
            {
                ArrayList list = (ArrayList) state;
                for (int i = 0; i < list.Count; i += 2)
                {
                    string key = ((IndexedString) list[i]).Value;
                    object obj2 = list[i + 1];
                    this.Add(key, obj2);
                }
            }
        }

        /// <summary>Removes the specified key/value pair from the <see cref="T:System.Web.UI.StateBag"></see> object.</summary>
        /// <param name="key">The item to remove. </param>
        public void Remove(string key)
        {
            this.bag.Remove(key);
        }

        internal object SaveViewState()
        {
            ArrayList list = null;
            if (this.bag.Count != 0)
            {
                IDictionaryEnumerator enumerator = this.bag.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    StateItem item = (StateItem) enumerator.Value;
                    if (item.IsDirty)
                    {
                        if (list == null)
                        {
                            list = new ArrayList();
                        }
                        list.Add(new IndexedString((string) enumerator.Key));
                        list.Add(item.Value);
                    }
                }
            }
            return list;
        }

        /// <summary>Sets the state of the <see cref="T:System.Web.UI.StateBag"></see> object as well as the <see cref="P:System.Web.SessionState.ISessionStateItemCollection.Dirty"></see> property of each of the <see cref="T:System.Web.UI.StateItem"></see> objects contained by it.</summary>
        /// <param name="dirty">true to mark the state of the collection and its items as modified; otherwise, false.</param>
        public void SetDirty(bool dirty)
        {
            if (this.bag.Count != 0)
            {
                foreach (StateItem item in this.bag.Values)
                {
                    item.IsDirty = dirty;
                }
            }
        }

        /// <summary>Sets the <see cref="P:System.Web.SessionState.ISessionStateItemCollection.Dirty"></see> property for the specified <see cref="T:System.Web.UI.StateItem"></see> object in the <see cref="T:System.Web.UI.StateBag"></see> object.</summary>
        /// <param name="dirty">true to mark the state of the item as modified; otherwise, false.</param>
        /// <param name="key">The key that identifies which <see cref="T:System.Web.UI.StateItem"></see> in the <see cref="T:System.Web.UI.StateBag"></see> to set. </param>
        public void SetItemDirty(string key, bool dirty)
        {
            StateItem item = this.bag[key] as StateItem;
            if (item != null)
            {
                item.IsDirty = dirty;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.Values.CopyTo(array, index);
        }

        void IDictionary.Add(object key, object value)
        {
            this.Add((string) key, value);
        }

        bool IDictionary.Contains(object key)
        {
            return this.bag.Contains((string) key);
        }

        void IDictionary.Remove(object key)
        {
            this.Remove((string) key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        void IStateManager.LoadViewState(object state)
        {
            this.LoadViewState(state);
        }

        object IStateManager.SaveViewState()
        {
            return this.SaveViewState();
        }

        void IStateManager.TrackViewState()
        {
            this.TrackViewState();
        }

        internal void TrackViewState()
        {
            this.marked = true;
        }

        /// <summary>Gets the number of <see cref="T:System.Web.UI.StateItem"></see> objects in the <see cref="T:System.Web.UI.StateBag"></see> object.</summary>
        /// <returns>The number of items in the <see cref="T:System.Web.UI.StateBag"></see>.</returns>
        public int Count
        {
            get
            {
                return this.bag.Count;
            }
        }

        internal bool IsTrackingViewState
        {
            get
            {
                return this.marked;
            }
        }

        /// <summary>Gets or sets the value of an item stored in the <see cref="T:System.Web.UI.StateBag"></see> object.</summary>
        /// <returns>The specified item in the <see cref="T:System.Web.UI.StateBag"></see> object.</returns>
        /// <param name="key">The key for the item. </param>
        public object this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw ExceptionUtil.ParameterNullOrEmpty("key");
                }
                StateItem item = this.bag[key] as StateItem;
                if (item != null)
                {
                    return item.Value;
                }
                return null;
            }
            set
            {
                this.Add(key, value);
            }
        }

        /// <summary>Gets a collection of keys representing the items in the <see cref="T:System.Web.UI.StateBag"></see> object.</summary>
        /// <returns>The collection of keys.</returns>
        public ICollection Keys
        {
            get
            {
                return this.bag.Keys;
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

        bool IDictionary.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool IDictionary.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        object IDictionary.this[object key]
        {
            get
            {
                return this[(string) key];
            }
            set
            {
                this[(string) key] = value;
            }
        }

        bool IStateManager.IsTrackingViewState
        {
            get
            {
                return this.IsTrackingViewState;
            }
        }

        /// <summary>Gets a collection of the view-state values stored in the <see cref="T:System.Web.UI.StateBag"></see> object.</summary>
        /// <returns>The collection of view-state values.</returns>
        public ICollection Values
        {
            get
            {
                return this.bag.Values;
            }
        }
    }
}

