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
    /// Grid Item Collection
    /// </summary>
	public class GridItemCollection : ICollection, IEnumerable
	{
		// Methods
		static GridItemCollection()
		{
			GridItemCollection.Empty = new GridItemCollection(new GridItem[0]);
		}

		internal GridItemCollection(GridItem[] entries)
		{
			if (entries == null)
			{
				this.entries = new GridItem[0];
			}
			else
			{
				this.entries = entries;
			}
		}
        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
		public IEnumerator GetEnumerator()
		{
			return this.entries.GetEnumerator();
		}

 
		void ICollection.CopyTo(Array dest, int index)
		{
			if (this.entries.Length > 0)
			{
				Array.Copy(this.entries, 0, dest, index, this.entries.Length);
			}
		}
        /// <summary>
        /// Get value indicating if IsSynchronized
        /// </summary>
		public bool IsSynchronized 
		{
			get{return false;}
		}

 
		object ICollection.SyncRoot
		{
			get{return this;}
		}

		/// <summary>
		/// Get items count
		/// </summary>
		public int Count
		{
			get
			{
				return this.entries.Length;
			}
		}
 
        /// <summary>
        /// Get Grid item
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
		public GridItem this[string label]
		{
			get
			{
				foreach (GridItem item1 in this.entries)
				{
					if (item1.Label == label)
					{
						return item1;
					}
				}
				return null;
			}
		}
        /// <summary>
        /// Get Grid item
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public GridItem this[int index]
		{
			get
			{
				return this.entries[index];
			}
		}
 

		/// <summary>
        /// Empty GridItemCollection
		/// </summary>
		public static GridItemCollection Empty;
		internal GridItem[] entries;
	}

}
