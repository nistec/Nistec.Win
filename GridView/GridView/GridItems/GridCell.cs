using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Runtime.InteropServices;


namespace Nistec.GridView
{
    /// <summary>
    /// Identifies a cell in the grid.
    /// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct GridCell
	{
		private int rowNumber;
		private int columnNumber;
        /// <summary>
        /// Initializes a new instance of the DataGridCell class
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
		public GridCell(int r, int c)
		{
			this.rowNumber = r;
			this.columnNumber = c;
		}
        /// <summary>
        /// Overloaded. Determines whether two Object instances are equal
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
		public override bool Equals(object o)
		{
			if (o is GridCell)
			{
				GridCell cell1 = (GridCell) o;
				if (cell1.RowNumber == this.RowNumber)
				{
					return (cell1.ColumnNumber == this.ColumnNumber);
				}
			}
			return false;
		}
        /// <summary>
        /// Overridden. Gets a hash value that can be added to a Hashtable
        /// </summary>
        /// <returns></returns>
		public override int GetHashCode()
		{
			return (((~this.rowNumber * (this.columnNumber + 1)) & 0xffff00) >> 8);
		}
        /// <summary>
        /// Overridden. Gets the row number and column number of the cell
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			return ("GridCell {RowNumber = " + this.RowNumber.ToString() + ", ColumnNumber = " + this.ColumnNumber.ToString() + "}");
		}
        /// <summary>
        /// Gets or sets the number of a column in the Grid control.
        /// </summary>
		public int ColumnNumber
		{
			get
			{
				return this.columnNumber;
			}
			set
			{
				this.columnNumber = value;
			}
		}
        /// <summary>
        /// Gets or sets the number of a row in the Grid control
        /// </summary>
		public int RowNumber
		{
			get
			{
				return this.rowNumber;
			}
			set
			{
				this.rowNumber = value;
			}
		}

	}
 
}