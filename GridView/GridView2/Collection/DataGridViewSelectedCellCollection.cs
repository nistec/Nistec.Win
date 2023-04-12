namespace MControl.GridView
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>Represents a collection of cells that are selected in a <see cref="T:MControl.GridView.Grid"></see>.</summary>
    /// <filterpriority>2</filterpriority>
    [ListBindable(false)]
    public class GridSelectedCellCollection : BaseCollection, IList, ICollection, IEnumerable
    {
        private ArrayList items = new ArrayList();

        internal GridSelectedCellCollection()
        {
        }

        internal int Add(GridCell gridCell)
        {
            return this.items.Add(gridCell);
        }

        internal void AddCellLinkedList(GridCellLinkedList gridCells)
        {
            foreach (GridCell cell in (IEnumerable) gridCells)
            {
                this.items.Add(cell);
            }
        }

        /// <summary>Clears the collection. </summary>
        /// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Clear()
        {
            throw new NotSupportedException(MControl.GridView.RM.GetString("Grid_ReadOnlyCollection"));
        }

        /// <summary>Determines whether the specified cell is contained in the collection.</summary>
        /// <returns>true if gridCell is in the <see cref="T:MControl.GridView.GridSelectedCellCollection"></see>; otherwise, false.</returns>
        /// <param name="gridCell">The <see cref="T:MControl.GridView.GridCell"></see> to locate in the <see cref="T:MControl.GridView.GridSelectedCellCollection"></see>.</param>
        /// <filterpriority>1</filterpriority>
        public bool Contains(GridCell gridCell)
        {
            return (this.items.IndexOf(gridCell) != -1);
        }

        /// <summary>Copies the elements of the collection to the specified <see cref="T:MControl.GridView.GridCell"></see> array, starting at the specified index.</summary>
        /// <param name="array">The one-dimensional array of type <see cref="T:MControl.GridView.GridCell"></see> that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.</exception>
        /// <exception cref="T:System.ArgumentException">array is multidimensional.-or-index is equal to or greater than the length of array.-or-The number of elements in the <see cref="T:MControl.GridView.GridCellCollection"></see> is greater than the available space from index to the end of array.</exception>
        /// <exception cref="T:System.InvalidCastException">The <see cref="T:MControl.GridView.GridCellCollection"></see> cannot be cast automatically to the type of array.</exception>
        /// <exception cref="T:System.ArgumentNullException">array is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void CopyTo(GridCell[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        /// <summary>Inserts a cell into the collection.</summary>
        /// <param name="gridCell">The object to be added to the <see cref="T:MControl.GridView.GridSelectedCellCollection"></see>.</param>
        /// <param name="index">The index at which gridCell should be inserted.</param>
        /// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
        /// <filterpriority>1</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Insert(int index, GridCell gridCell)
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

        /// <summary>Gets the cell at the specified index.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCell"></see> at the specified index.</returns>
        /// <param name="index">The index of the <see cref="T:MControl.GridView.GridCell"></see> to get from the <see cref="T:MControl.GridView.GridSelectedCellCollection"></see>.</param>
        /// <filterpriority>1</filterpriority>
        public GridCell this[int index]
        {
            get
            {
                return (GridCell) this.items[index];
            }
        }

        /// <summary>Gets a list of elements in the collection.</summary>
        /// <returns>An <see cref="T:System.Collections.ArrayList"></see> containing the elements of the collection.</returns>
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

