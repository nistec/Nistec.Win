using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Runtime.InteropServices;


namespace mControl.GridView
{
	[StructLayout(LayoutKind.Sequential)]
	public struct GridCellBound
	{
		private int rowNumber;
		private object value;

        public GridCellBound(int r, object v)
		{
			this.rowNumber = r;
			this.value = v;
		}
		public override bool Equals(object o)
		{
			if (o is GridCellBound)
			{
				GridCellBound cell1 = (GridCellBound) o;
				if (cell1.RowNumber == this.RowNumber)
				{
					return (cell1.Value == this.Value);
				}
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (((~this.rowNumber /* * (this.columnNumber + 1)*/) & 0xffff00) >> 8);
		}

		public override string ToString()
		{
			return ("GridCellBound {RowNumber = " + this.RowNumber.ToString() + ", Value = " + this.Value.ToString() + "}");
		}

		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}
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