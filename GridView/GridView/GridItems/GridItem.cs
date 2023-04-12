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
    /// Represents an item (row) in a Grid control
    /// </summary>
	public abstract class GridItem
	{
		/// <summary>
        /// Initializes a new instance of the GridItem class
		/// </summary>
		protected GridItem()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public abstract bool Select();

		/// <summary>
        /// Gets  a value indicating whether the control is Expand able
		/// </summary>
		public virtual bool Expandable
		{
			get
			{
				return false;
			}
		}
 
        /// <summary>
        /// Get a value indicating whether the control is Expanded
        /// </summary>
		public virtual bool Expanded
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException("GridItemNotExpandable");
			}
		}
        /// <summary>
        /// Get Grid Item Collection
        /// </summary>
		public abstract GridItemCollection GridItems { get; }
        /// <summary>
        /// Get the Grid Item Type
        /// </summary>
		public abstract GridItemType GridItemType { get; }
        /// <summary>
        /// Get Label grid item
        /// </summary>
		public abstract string Label { get; }
        /// <summary>
        /// Gets a reference to the parent control 
        /// </summary>
		public abstract GridItem Parent { get; }
        /// <summary>
        /// Get the Property Descriptor
        /// </summary>
		public abstract PropertyDescriptor PropertyDescriptor { get; }
        /// <summary>
        /// Ge item value
        /// </summary>
		public abstract object Value { get; }
        
	}
 

}
