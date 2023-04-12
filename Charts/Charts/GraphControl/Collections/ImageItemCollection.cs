namespace MControl.Charts
{
    using System;
    using System.Collections;
    using MControl.Collections;

    /// <summary>Represents a collection of <see cref="T:MControl.Charts.ImageItem"></see> and <see cref="T:MControl.Charts.ImageItem"></see>-derived objects that are used by data source controls in advanced data-binding scenarios.</summary>
    //[Editor("System.Web.UI.Design.WebControls.ImageItemCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    public class ImageItemCollection : CollectionWithEvents
    {

        /// <summary>Occurs when one or more <see cref="T:MControl.Charts.ImageItem"></see> objects contained by the collection changes state.</summary>
        public event EventHandler CollectionChanged;

        /// <summary>Appends the specified <see cref="T:MControl.Charts.ImageItem"></see> object to the end of the collection.</summary>
        /// <returns>The index value of the added item.</returns>
        /// <param name="imageItem">The <see cref="T:MControl.Charts.ImageItem"></see> to append to the collection. </param>
        public int Add(ImageItem imageItem)
        {
            return ((IList) this).Add(imageItem);
        }

        /// <summary>Creates a <see cref="T:MControl.Charts.ImageItem"></see> object with the specified name and default value, and appends it to the end of the collection.</summary>
        /// <returns>The index value of the added item.</returns>
        /// <param name="name">The name of the imageItem. </param>
        public int Add(string name)
        {
            return ((IList) this).Add(new ImageItem(name));
        }

        internal void CallOnCollectionChanged()
        {
            this.OnCollectionChanged(EventArgs.Empty);
        }
        /// <summary>Raises the <see cref="E:MControl.Charts.ImageItemCollection.ImageItemsChanged"></see> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
        protected virtual void OnCollectionChanged(EventArgs e)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, e);
            }
        }

        /// <summary>Determines whether the <see cref="T:MControl.Charts.ImageItemCollection"></see> collection contains a specific value</summary>
        /// <returns>true if the object is found in the <see cref="T:MControl.Charts.ImageItemCollection"></see>; otherwise, false. If null is passed for the value imageItem, false is returned.</returns>
        /// <param name="imageItem">The <see cref="T:MControl.Charts.ImageItem"></see> to locate in the <see cref="T:MControl.Charts.ImageItemCollection"></see>.</param>
        public bool Contains(ImageItem imageItem)
        {
            return ((IList) this).Contains(imageItem);
        }

        public void CopyTo(ImageItem[] imageItems, int index)
        {
            base.List.CopyTo(imageItems, index);
        }

        private int GetIndex(string name)
        {
            for (int i = 0; i < base.Count; i++)
            {
                if (string.Equals(this[i].ImageUrl, name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>Determines the index of a specified <see cref="T:MControl.Charts.ImageItem"></see> object in the <see cref="T:MControl.Charts.ImageItemCollection"></see> collection.</summary>
        /// <returns>The index of imageItem, if it is found in the collection; otherwise, -1.</returns>
        /// <param name="imageItem">The <see cref="T:MControl.Charts.ImageItem"></see> to locate in the <see cref="T:MControl.Charts.ImageItemCollection"></see>.</param>
        public int IndexOf(ImageItem imageItem)
        {
            return ((IList) this).IndexOf(imageItem);
        }

        /// <summary>Inserts the specified <see cref="T:MControl.Charts.ImageItem"></see> object into the <see cref="T:MControl.Charts.ImageItemCollection"></see> collection at the specified index.</summary>
        /// <param name="imageItem">The <see cref="T:MControl.Charts.ImageItem"></see> to insert. </param>
        /// <param name="index">The zero-based index at which the <see cref="T:MControl.Charts.ImageItem"></see> is inserted. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.-or- index is greater than Count. </exception>
        public void Insert(int index, ImageItem imageItem)
        {
            ((IList) this).Insert(index, imageItem);
        }

        /// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
        protected override void OnClearComplete()
        {
            base.OnClearComplete();
            this.OnCollectionChanged(EventArgs.Empty);
        }

        /// <summary>Occurs before the <see cref="M:MControl.Charts.ImageItemCollection.Insert(System.Int32,MControl.Charts.ImageItem)"></see> method is called.</summary>
        /// <param name="value">The <see cref="T:MControl.Charts.ImageItem"></see> that is inserted into the <see cref="T:MControl.Charts.ImageItemCollection"></see>. </param>
        /// <param name="index">The index in the collection that the <see cref="T:MControl.Charts.ImageItem"></see> is inserted at. </param>
        protected override void OnInsert(int index, object value)
        {
            base.OnInsert(index, value);
           // ((ImageItem) value).SetOwner(this);
        }

        /// <summary>Occurs after the <see cref="M:MControl.Charts.ImageItemCollection.Insert(System.Int32,MControl.Charts.ImageItem)"></see> method completes.</summary>
        /// <param name="value">The <see cref="T:MControl.Charts.ImageItem"></see> that was inserted into the <see cref="T:MControl.Charts.ImageItemCollection"></see>. </param>
        /// <param name="index">The index in the collection that the <see cref="T:MControl.Charts.ImageItem"></see> was inserted at. </param>
        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
            this.OnCollectionChanged(EventArgs.Empty);
        }

 

        /// <summary>Occurs after the <see cref="M:MControl.Charts.ImageItemCollection.Remove(MControl.Charts.ImageItem)"></see> method completes.</summary>
        /// <param name="value">The <see cref="T:MControl.Charts.ImageItem"></see> that was removed from the <see cref="T:MControl.Charts.ImageItemCollection"></see>. </param>
        /// <param name="index">The index in the collection that the <see cref="T:MControl.Charts.ImageItem"></see> was removed from. </param>
        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            //((ImageItem) value).SetOwner(null);
            this.OnCollectionChanged(EventArgs.Empty);
        }

        /// <summary>Performs additional custom processes when validating a value.</summary>
        /// <param name="o">The object being validated. </param>
        /// <exception cref="T:System.ArgumentNullException">The object is null. </exception>
        /// <exception cref="T:System.ArgumentException">The object is not an instance of the <see cref="T:MControl.Charts.ImageItem"></see> class or one of its derived classes. </exception>
        protected override void OnValidate(object o)
        {
            base.OnValidate(o);
            if (!(o is ImageItem))
            {
                throw new ArgumentException("NotImageItem");
            }
        }

        /// <summary>Removes the specified <see cref="T:MControl.Charts.ImageItem"></see> object from the <see cref="T:MControl.Charts.ImageItemCollection"></see> collection.</summary>
        /// <param name="imageItem">The <see cref="T:MControl.Charts.ImageItem"></see> to remove from the <see cref="T:MControl.Charts.ImageItemCollection"></see>. </param>
        public void Remove(ImageItem imageItem)
        {
            ((IList) this).Remove(imageItem);
        }

        ///// <summary>Removes the <see cref="T:MControl.Charts.ImageItem"></see> object at the specified index from the <see cref="T:MControl.Charts.ImageItemCollection"></see> collection.</summary>
        ///// <param name="index">The index of the <see cref="T:MControl.Charts.ImageItem"></see> to remove. </param>
        //public void RemoveAt(int index)
        //{
        //    ((IList)this).RemoveAt(index);
        //}

 
        /// <summary>Gets or sets the <see cref="T:MControl.Charts.ImageItem"></see> object with the specified name in the collection.</summary>
        /// <returns>The <see cref="T:MControl.Charts.ImageItem"></see> with the specified name in the collection. If the <see cref="T:MControl.Charts.ImageItem"></see> is not found in the collection, the indexer returns null.</returns>
        /// <param name="name">The <see cref="P:MControl.Charts.ImageItem.Name"></see> of the <see cref="T:MControl.Charts.ImageItem"></see> to retrieve from the collection. </param>
        public ImageItem this[string name]
        {
            get
            {
                int imageItemIndex = this.GetIndex(name);
                if (imageItemIndex == -1)
                {
                    return null;
                }
                return this[imageItemIndex];
            }
            set
            {
                int imageItemIndex = this.GetIndex(name);
                if (imageItemIndex == -1)
                {
                    this.Add(value);
                }
                else
                {
                    this[imageItemIndex] = value;
                }
            }
        }

        /// <summary>Gets or sets the <see cref="T:MControl.Charts.ImageItem"></see> object at the specified index in the collection.</summary>
        /// <returns>The <see cref="T:MControl.Charts.ImageItem"></see> at the specified index in the collection. </returns>
        /// <param name="index">The index of the <see cref="T:MControl.Charts.ImageItem"></see> to retrieve from the collection. </param>
        public ImageItem this[int index]
        {
            get
            {
                return (ImageItem) this[index];
            }
            set
            {
                this[index] = value;
            }
        }
    }
}

