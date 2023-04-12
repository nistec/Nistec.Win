namespace MControl.GridView
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>Represents a collection of <see cref="T:MControl.GridView.GridColumn"></see> objects that are selected in a <see cref="T:MControl.GridView.Grid"></see>.</summary>
    /// <filterpriority>2</filterpriority>
    [ListBindable(false)]
    public class GridSelectedColumnCollection : BaseCollection, IList, ICollection, IEnumerable
    {
        private ArrayList items = new ArrayList();

        internal GridSelectedColumnCollection()
        {
        }

        internal int Add(GridColumn gridColumn)
        {
            return this.items.Add(gridColumn);
        }

        /// <summary>Clears the collection.</summary>
        /// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Clear()
        {
            throw new NotSupportedException(MControl.GridView.RM.GetString("Grid_ReadOnlyCollection"));
        }

        /// <summary>Determines whether the specified column is contained in the collection.</summary>
        /// <returns>true if the gridColumn parameter is in the collection; otherwise, false.</returns>
        /// <param name="gridColumn">A <see cref="T:MControl.GridView.GridColumn"></see> to locate in the <see cref="T:MControl.GridView.GridSelectedColumnCollection"></see>.</param>
        /// <filterpriority>1</filterpriority>
        public bool Contains(GridColumn gridColumn)
        {
            return (this.items.IndexOf(gridColumn) != -1);
        }

        /// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in the array at which copying begins.</param>
        /// <exception cref="T:System.InvalidCastException">The <see cref="T:MControl.GridView.GridColumnCollection"></see> cannot be cast automatically to the type of array.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.</exception>
        /// <exception cref="T:System.ArgumentException">array is multidimensional.-or-index is equal to or greater than the length of array.-or-The number of elements in the <see cref="T:MControl.GridView.GridCellCollection"></see> is greater than the available space from index to the end of array.</exception>
        /// <exception cref="T:System.ArgumentNullException">array is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void CopyTo(GridColumn[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        /// <summary>Inserts a column into the collection at the specified position.</summary>
        /// <param name="gridColumn">The <see cref="T:MControl.GridView.GridColumn"></see> to insert into the <see cref="T:MControl.GridView.GridSelectedColumnCollection"></see>.</param>
        /// <param name="index">The zero-based index at which the column should be inserted. </param>
        /// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Insert(int index, GridColumn gridColumn)
        {
            throw new NotSupportedException(MControl.GridView.RM.GetString("Grid_ReadOnlyCollection"));
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.items.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        int IList.Add(object value)
        {
            throw new NotSupportedException(MControl.GridView.RM.GetString("Grid_ReadOnlyCollection"));
        }

        void IList.Clear()
        {
            throw new NotSupportedException(MControl.GridView.RM.GetString("Grid_ReadOnlyCollection"));
        }

        bool IList.Contains(object value)
        {
            return this.items.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return this.items.IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException(MControl.GridView.RM.GetString("Grid_ReadOnlyCollection"));
        }

        void IList.Remove(object value)
        {
            throw new NotSupportedException(MControl.GridView.RM.GetString("Grid_ReadOnlyCollection"));
        }

        void IList.RemoveAt(int index)
        {
            throw new NotSupportedException(MControl.GridView.RM.GetString("Grid_ReadOnlyCollection"));
        }

        /// <summary>Gets the column at the specified index.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridColumn"></see> at the specified index.</returns>
        /// <param name="index">The index of the <see cref="T:MControl.GridView.GridColumn"></see> to get from the <see cref="T:MControl.GridView.GridSelectedColumnCollection"></see>.</param>
        /// <filterpriority>1</filterpriority>
        public GridColumn this[int index]
        {
            get
            {
                return (GridColumn) this.items[index];
            }
        }

        protected override ArrayList List
        {
            get
            {
                return this.items;
            }
        }

        int ICollection.Count
        {
            get
            {
                return this.items.Count;
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
                return true;
            }
        }

        bool IList.IsReadOnly
        {
            get
            {
                return true;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                throw new NotSupportedException(MControl.GridView.RM.GetString("Grid_ReadOnlyCollection"));
            }
        }
    }
}

