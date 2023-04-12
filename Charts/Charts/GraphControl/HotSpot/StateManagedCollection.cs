namespace Nistec.Charts
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Security.Permissions;
    using System.Web;
    using System.Web.UI;

    /// <summary>Provides a base class for all strongly typed collections that manage <see cref="T:System.Web.UI.IStateManager"></see> objects.</summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    [Serializable]
    public abstract class ChartStateManagedCollection : IList, ICollection, IEnumerable, IStateManager
    {
        private ArrayList _collectionItems = new ArrayList();
        private bool _saveAll;
        private bool _tracking;

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateManagedCollection"></see> class. </summary>
        protected ChartStateManagedCollection()
        {
        }

        public abstract int Add(ChartHotSpot spot);

        public abstract void RemoveAt(int index);

        public abstract void Insert(int index, ChartHotSpot spot);


        public int IndexOf(object value)
        {
            if (value == null)
            {
                return -1;
            }
            this.OnValidate(value);
            return this._collectionItems.IndexOf(value);

        }
 


        /// <summary>Removes all items from the <see cref="T:System.Web.UI.StateManagedCollection"></see> collection.</summary>
        public void Clear()
        {
            this.OnClear();
            this._collectionItems.Clear();
            this.OnClearComplete();
            if (this._tracking)
            {
                this._saveAll = true;
            }
        }

        /// <summary>Copies the elements of the <see cref="T:System.Web.UI.StateManagedCollection"></see> collection to an array, starting at a particular array index.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from the <see cref="T:System.Web.UI.StateManagedCollection"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.</exception>
        /// <exception cref="T:System.ArgumentException">array is multidimensional.- or -index is greater than or equal to the length of array.- or -The number of elements in the source <see cref="T:System.Web.UI.StateManagedCollection"></see> is greater than the available space from the index to the end of the destination array.</exception>
        /// <exception cref="T:System.ArgumentNullException">array is null.</exception>
        public void CopyTo(Array array, int index)
        {
            this._collectionItems.CopyTo(array, index);
        }

        /// <summary>When overridden in a derived class, creates an instance of a class that implements <see cref="T:System.Web.UI.IStateManager"></see>. The type of object created is based on the specified member of the collection returned by the <see cref="M:System.Web.UI.StateManagedCollection.GetKnownTypes"></see> method.</summary>
        /// <returns>An instance of a class derived from <see cref="T:System.Web.UI.IStateManager"></see>, according to the index provided.</returns>
        /// <param name="index">The index, from the ordered list of types returned by <see cref="M:System.Web.UI.StateManagedCollection.GetKnownTypes"></see>, of the type of <see cref="T:System.Web.UI.IStateManager"></see> to create.</param>
        protected virtual object CreateKnownType(int index)
        {
            throw new InvalidOperationException("StateManagedCollection_NoKnownTypes");
        }

        /// <summary>Returns an iterator that iterates through the <see cref="T:System.Web.UI.StateManagedCollection"></see> collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"></see> that can be used to iterate through the <see cref="T:System.Web.UI.StateManagedCollection"></see>.</returns>
        public IEnumerator GetEnumerator()
        {
            return this._collectionItems.GetEnumerator();
        }

        private int GetKnownTypeCount()
        {
            Type[] knownTypes = this.GetKnownTypes();
            if (knownTypes == null)
            {
                return 0;
            }
            return knownTypes.Length;
        }

        /// <summary>When overridden in a derived class, gets an array of <see cref="T:System.Web.UI.IStateManager"></see> types that the <see cref="T:System.Web.UI.StateManagedCollection"></see> collection can contain.</summary>
        /// <returns>An ordered array of <see cref="T:System.Type"></see> objects that identify the types of <see cref="T:System.Web.UI.IStateManager"></see> objects the collection can contain. The default implementation returns null.</returns>
        protected virtual Type[] GetKnownTypes()
        {
            return null;
        }

        private void InsertInternal(int index, object o)
        {
            int num;
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }
            if (((IStateManager) this).IsTrackingViewState)
            {
                ((IStateManager) o).TrackViewState();
                this.SetDirtyObject(o);
            }
            this.OnInsert(index, o);
            if (index == -1)
            {
                num = this._collectionItems.Add(o);
            }
            else
            {
                num = index;
                this._collectionItems.Insert(index, o);
            }
            try
            {
                this.OnInsertComplete(index, o);
            }
            catch
            {
                this._collectionItems.RemoveAt(num);
                throw;
            }
        }

        private void LoadAllItemsFromViewState(object savedState)
        {
            Pair pair = (Pair) savedState;
            if (pair.Second is Pair)
            {
                Pair second = (Pair) pair.Second;
                object[] first = (object[]) pair.First;
                int[] numArray = (int[]) second.First;
                ArrayList list = (ArrayList) second.Second;
                this.Clear();
                for (int i = 0; i < first.Length; i++)
                {
                    object obj2;
                    if (numArray == null)
                    {
                        obj2 = this.CreateKnownType(0);
                    }
                    else
                    {
                        int index = numArray[i];
                        if (index < this.GetKnownTypeCount())
                        {
                            obj2 = this.CreateKnownType(index);
                        }
                        else
                        {
                            string typeName = (string) list[index - this.GetKnownTypeCount()];
                            obj2 = Activator.CreateInstance(Type.GetType(typeName));
                        }
                    }
                    ((IStateManager) obj2).TrackViewState();
                    ((IStateManager) obj2).LoadViewState(first[i]);
                    this.Add(obj2 as ChartHotSpot);
                }
            }
            else
            {
                object[] objArray2 = (object[]) pair.First;
                int[] numArray2 = (int[]) pair.Second;
                this.Clear();
                for (int j = 0; j < objArray2.Length; j++)
                {
                    int num4 = 0;
                    if (numArray2 != null)
                    {
                        num4 = numArray2[j];
                    }
                    object obj3 = this.CreateKnownType(num4);
                    ((IStateManager) obj3).TrackViewState();
                    ((IStateManager) obj3).LoadViewState(objArray2[j]);
                    this.Add(obj3 as ChartHotSpot);
                }
            }
        }

        private void LoadChangedItemsFromViewState(object savedState)
        {
            Triplet triplet = (Triplet) savedState;
            if (triplet.Third is Pair)
            {
                Pair third = (Pair) triplet.Third;
                ArrayList first = (ArrayList) triplet.First;
                ArrayList second = (ArrayList) triplet.Second;
                ArrayList list3 = (ArrayList) third.First;
                ArrayList list4 = (ArrayList) third.Second;
                for (int i = 0; i < first.Count; i++)
                {
                    int num2 = (int) first[i];
                    if (num2 < this.Count)
                    {
                        ((IStateManager) ((IList) this)[num2]).LoadViewState(second[i]);
                    }
                    else
                    {
                        object obj2;
                        if (list3 == null)
                        {
                            obj2 = this.CreateKnownType(0);
                        }
                        else
                        {
                            int index = (int) list3[i];
                            if (index < this.GetKnownTypeCount())
                            {
                                obj2 = this.CreateKnownType(index);
                            }
                            else
                            {
                                string typeName = (string) list4[index - this.GetKnownTypeCount()];
                                obj2 = Activator.CreateInstance(Type.GetType(typeName));
                            }
                        }
                        ((IStateManager) obj2).TrackViewState();
                        ((IStateManager) obj2).LoadViewState(second[i]);
                        this.Add(obj2 as ChartHotSpot);
                    }
                }
            }
            else
            {
                ArrayList list5 = (ArrayList) triplet.First;
                ArrayList list6 = (ArrayList) triplet.Second;
                ArrayList list7 = (ArrayList) triplet.Third;
                for (int j = 0; j < list5.Count; j++)
                {
                    int num5 = (int) list5[j];
                    if (num5 < this.Count)
                    {
                        ((IStateManager) ((IList) this)[num5]).LoadViewState(list6[j]);
                    }
                    else
                    {
                        int num6 = 0;
                        if (list7 != null)
                        {
                            num6 = (int) list7[j];
                        }
                        object obj3 = this.CreateKnownType(num6);
                        ((IStateManager) obj3).TrackViewState();
                        ((IStateManager) obj3).LoadViewState(list6[j]);
                        this.Add(obj3 as ChartHotSpot);
                    }
                }
            }
        }

        /// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.Clear"></see> method removes all items from the collection.</summary>
        protected virtual void OnClear()
        {
        }

        /// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.Clear"></see> method finishes removing all items from the collection.</summary>
        protected virtual void OnClearComplete()
        {
        }

        /// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)"></see> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Add(System.Object)"></see> method adds an item to the collection.</summary>
        /// <param name="value">The object to insert into the <see cref="T:System.Web.UI.StateManagedCollection"></see>.</param>
        /// <param name="index">The zero-based index at which value should be inserted by the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)"></see> method.</param>
        protected virtual void OnInsert(int index, object value)
        {
        }

        /// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)"></see> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Add(System.Object)"></see> method adds an item to the collection.</summary>
        /// <param name="value">The object inserted into the <see cref="T:System.Web.UI.StateManagedCollection"></see>.</param>
        /// <param name="index">The zero-based index at which value is inserted by the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)"></see> method.</param>
        protected virtual void OnInsertComplete(int index, object value)
        {
        }

        /// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)"></see> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)"></see> method removes the specified item from the collection.</summary>
        /// <param name="value">The object to remove from the <see cref="T:System.Web.UI.StateManagedCollection"></see>, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)"></see> is called.</param>
        /// <param name="index">The zero-based index of the item to remove, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)"></see> is called.</param>
        protected virtual void OnRemove(int index, object value)
        {
        }

        /// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)"></see> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)"></see> method removes the specified item from the collection.</summary>
        /// <param name="value">The object removed from the <see cref="T:System.Web.UI.StateManagedCollection"></see>, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)"></see> is called.</param>
        /// <param name="index">The zero-based index of the item to remove, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)"></see> is called.</param>
        protected virtual void OnRemoveComplete(int index, object value)
        {
        }

        /// <summary>When overridden in a derived class, validates an element of the <see cref="T:System.Web.UI.StateManagedCollection"></see> collection.</summary>
        /// <param name="value">The <see cref="T:System.Web.UI.IStateManager"></see> to validate.</param>
        /// <exception cref="T:System.ArgumentNullException">value is null.</exception>
        protected virtual void OnValidate(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
        }

        private object SaveAllItemsToViewState()
        {
            bool flag = false;
            int count = this._collectionItems.Count;
            int[] x = new int[count];
            object[] objArray = new object[count];
            ArrayList y = null;
            IDictionary dictionary = null;
            int knownTypeCount = this.GetKnownTypeCount();
            for (int i = 0; i < count; i++)
            {
                object o = this._collectionItems[i];
                this.SetDirtyObject(o);
                objArray[i] = ((IStateManager) o).SaveViewState();
                if (objArray[i] != null)
                {
                    flag = true;
                }
                Type type = o.GetType();
                int index = -1;
                if (knownTypeCount != 0)
                {
                    index = ((IList)this.GetKnownTypes()).IndexOf(type);
                }
                if (index != -1)
                {
                    x[i] = index;
                }
                else
                {
                    if (y == null)
                    {
                        y = new ArrayList();
                        dictionary = new HybridDictionary();
                    }
                    object obj3 = dictionary[type];
                    if (obj3 == null)
                    {
                        y.Add(type.AssemblyQualifiedName);
                        obj3 = (y.Count + knownTypeCount) - 1;
                        dictionary[type] = obj3;
                    }
                    x[i] = (int) obj3;
                }
            }
            if (!flag)
            {
                return null;
            }
            if (y != null)
            {
                return new Pair(objArray, new Pair(x, y));
            }
            if (knownTypeCount == 1)
            {
                x = null;
            }
            return new Pair(objArray, x);
        }

        private object SaveChangedItemsToViewState()
        {
            bool flag = false;
            int count = this._collectionItems.Count;
            ArrayList x = new ArrayList();
            ArrayList y = new ArrayList();
            ArrayList list3 = new ArrayList();
            ArrayList list4 = null;
            IDictionary dictionary = null;
            int knownTypeCount = this.GetKnownTypeCount();
            for (int i = 0; i < count; i++)
            {
                object obj2 = this._collectionItems[i];
                object obj3 = ((IStateManager) obj2).SaveViewState();
                if (obj3 != null)
                {
                    flag = true;
                    x.Add(i);
                    y.Add(obj3);
                    Type type = obj2.GetType();
                    int index = -1;
                    if (knownTypeCount != 0)
                    {
                        index = ((IList) this.GetKnownTypes()).IndexOf(type);
                    }
                    if (index != -1)
                    {
                        list3.Add(index);
                    }
                    else
                    {
                        if (list4 == null)
                        {
                            list4 = new ArrayList();
                            dictionary = new HybridDictionary();
                        }
                        object obj4 = dictionary[type];
                        if (obj4 == null)
                        {
                            list4.Add(type.AssemblyQualifiedName);
                            obj4 = (list4.Count + knownTypeCount) - 1;
                            dictionary[type] = obj4;
                        }
                        list3.Add(obj4);
                    }
                }
            }
            if (!flag)
            {
                return null;
            }
            if (list4 != null)
            {
                return new Triplet(x, y, new Pair(list3, list4));
            }
            if (knownTypeCount == 1)
            {
                list3 = null;
            }
            return new Triplet(x, y, list3);
        }

        /// <summary>Forces the entire <see cref="T:System.Web.UI.StateManagedCollection"></see> collection to be serialized into view state. </summary>
        public void SetDirty()
        {
            this._saveAll = true;
        }

        /// <summary>When overridden in a derived class, instructs an object contained by the collection to record its entire state to view state, rather than recording only change information.</summary>
        /// <param name="o">The <see cref="T:System.Web.UI.IStateManager"></see> that should serialize itself completely.</param>
        protected abstract void SetDirtyObject(object o);
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        int IList.Add(object value)
        {
            this.OnValidate(value);
            this.InsertInternal(-1, value);
            return (this._collectionItems.Count - 1);
        }

        void IList.Clear()
        {
            this.Clear();
        }

        bool IList.Contains(object value)
        {
            if (value == null)
            {
                return false;
            }
            this.OnValidate(value);
            return this._collectionItems.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            if (value == null)
            {
                return -1;
            }
            this.OnValidate(value);
            return this._collectionItems.IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if ((index < 0) || (index > this.Count))
            {
                throw new ArgumentOutOfRangeException("index", "StateManagedCollection_InvalidIndex");
            }
            this.OnValidate(value);
            this.InsertInternal(index, value);
            if (this._tracking)
            {
                this._saveAll = true;
            }
        }

        void IList.Remove(object value)
        {
            if (value != null)
            {
                this.OnValidate(value);
                this.RemoveAt(this.IndexOf(value));
            }
        }

        void IList.RemoveAt(int index)
        {
            object obj2 = this._collectionItems[index];
            this.OnRemove(index, obj2);
            this._collectionItems.RemoveAt(index);
            try
            {
                this.OnRemoveComplete(index, obj2);
            }
            catch
            {
                this._collectionItems.Insert(index, obj2);
                throw;
            }
            if (this._tracking)
            {
                this._saveAll = true;
            }
        }

        void IStateManager.LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                if (savedState is Triplet)
                {
                    this.LoadChangedItemsFromViewState(savedState);
                }
                else
                {
                    this.LoadAllItemsFromViewState(savedState);
                }
            }
        }

        object IStateManager.SaveViewState()
        {
            if (this._saveAll)
            {
                return this.SaveAllItemsToViewState();
            }
            return this.SaveChangedItemsToViewState();
        }

        void IStateManager.TrackViewState()
        {
            this._tracking = true;
            foreach (IStateManager manager in this._collectionItems)
            {
                manager.TrackViewState();
            }
        }

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection"></see> collection.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection"></see>.</returns>
        public int Count
        {
            get
            {
                return this._collectionItems.Count;
            }
        }

        int ICollection.Count
        {
            get
            {
                return this.Count;
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
                return null;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool IList.IsReadOnly
        {
            get
            {
                return this._collectionItems.IsReadOnly;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this._collectionItems[index];
            }
            set
            {
                if ((index < 0) || (index >= this.Count))
                {
                    throw new ArgumentOutOfRangeException("index", "StateManagedCollection_InvalidIndex");
                }
                this.RemoveAt(index);
                this.Insert(index, value as ChartHotSpot);
            }
        }

        bool IStateManager.IsTrackingViewState
        {
            get
            {
                return this._tracking;
            }
        }
    }
}

