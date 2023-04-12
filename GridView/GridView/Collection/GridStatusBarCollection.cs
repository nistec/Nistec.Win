using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.Drawing.Design;
using System.Security;
using System.Globalization;


namespace Nistec.GridView
{
    /// <summary>
    /// Grid Status Panel Collection
    /// </summary>
    public class GridStatusPanelCollection : BaseCollection, IList, ICollection, IEnumerable
	{
        internal GridStatusBar gridStatusBar;
        /// <summary>
        /// Collection Change Event 
        /// </summary>
        public event CollectionChangeEventHandler CollectionChanged;

        /// <summary>
        /// Raise Collection Changed
        /// </summary>
        /// <param name="e"></param>
        protected void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);

        }
        /// <summary>
        /// Initilaized Grid Status Panel Collection
        /// </summary>
        /// <param name="statusBar"></param>
        public GridStatusPanelCollection(GridStatusBar statusBar)
        {
            gridStatusBar = statusBar;
            list = new ArrayList();
            
        }

        //public override int Add(object column)
        //{
        //    return Add(column as GridStatusPanel);
        //}
        /// <summary>
        /// Add GridStatusPanel to collection
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public int Add(GridStatusPanel column)
        {
            //if (!base.Contains(column))
            //{
                //column.StatusPanel= gridStatusBar.Panels.Add("");
                column.gridStatusBar = this.gridStatusBar;
                list.Add(column);
                OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add,column));
                return list.Count-1;
            //}
            //return -1;
        }

        //public void AddRange(object[] columns)
        //{
        //    foreach (object o in columns)
        //     Add(o as GridStatusPanel);
        //}

        /// <summary>
        /// Add GridStatusPanel array to collection
        /// </summary>
        /// <param name="columns"></param>
        public void AddRange(GridStatusPanel[] columns)
        {
            foreach (GridStatusPanel o in columns)
                Add(o);
        }
        //public override void Remove(object column)
        //{
        //    Remove(column as GridStatusPanel);
        //}
 
        /// <summary>
        /// Remove item from collection
        /// </summary>
        /// <param name="column"></param>
        public void Remove(GridStatusPanel column)
        {
            if (list.Contains(column))
            {
                list.Remove(column);
                OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
            }
        }
        /// <summary>
        /// Remove item from collection
        /// </summary>
        /// <param name="index"></param>
        public void Remove(int index)
        {
            if (index >=0 && index < Count)
            {
                Remove(this[index]);
            }
        }
        /// <summary>
        /// Clear collection
        /// </summary>
        public void Clear()
        {
            list.Clear();
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
        }

        /// <summary>
        /// Get index of item
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public int IndexOf(GridStatusPanel column)
        {
            for (int i = 0; i < Count; i++)
            {
                if (list[i] == column)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// Get Enumerator
        /// </summary>
        /// <returns></returns>
        public new IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }


        void ICollection.CopyTo(Array dest, int index)
        {
            if (list.Count > 0)
            {
                list.CopyTo(0, dest, index, list.Count);
            }
        }
        /// <summary>
        /// Get IsSynchronized
        /// </summary>
        public new bool IsSynchronized
        {
            get { return false; }
        }


        object ICollection.SyncRoot
        {
            get { return this; }
        }

        /// <summary>
        /// Get item count
        /// </summary>
        public new int Count
        {
            get
            {
                return this.list.Count;
            }
        }

        int IList.Add(object value)
        {
            return this.Add((GridStatusPanel)value);
        }


        void IList.Clear()
        {
            this.Clear();
        }


        bool IList.Contains(object value)
        {
            return this.list.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return this.list.IndexOf(value);
        }


        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        void IList.Remove(object value)
        {
            list.Remove((GridStatusPanel)value);
        }

        void IList.RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        bool IList.IsFixedSize
        {
           get{return false;}
        }

        bool IList.IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this.list[index]; }
            set { this.list[index] = value; }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
		public GridStatusPanel this[string label]
		{
			get
			{
                foreach (GridStatusPanel item1 in list)
				{
					if (item1.MappingName == label)
					{
						return item1;
					}
				}
				return null;
			}
		}
        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public  GridStatusPanel this[int index]
		{
			get
			{
                return (GridStatusPanel)list[index];
			}
		}
 

		// Fields
        //public static GridStatusPanelCollection Empty;
        private ArrayList list;
	}

}
