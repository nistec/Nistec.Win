
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;

using Nistec.WinForms;

namespace Nistec.GridView
{
	internal class GridAddNewRow : GridRow
	{
		// Methods
		public GridAddNewRow(Grid dGrid, GridTableStyle gridTable, int rowNum) : base(dGrid, gridTable, rowNum)
		{
			this.dataBound = false;
		}

 
		internal override void LoseChildFocus(Rectangle rowHeader, bool alignToRight)
		{
		}

		public override void OnEdit()
		{
			if (!this.DataBound)
			{
				base.Grid.AddNewRow();
			}
		}

		public override void OnRowLeave()
		{
			if (this.DataBound)
			{
				this.DataBound = false;
			}
		}

        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="trueRowBounds"></param>
        /// <param name="firstVisibleColumn"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int columnCount)
		{
			return this.Paint(g, bounds, trueRowBounds, firstVisibleColumn, columnCount, false);
		}
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="trueRowBounds"></param>
        /// <param name="firstVisibleColumn"></param>
        /// <param name="columnCount"></param>
        /// <param name="alignToRight"></param>
        /// <returns></returns>
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int columnCount, bool alignToRight)
		{
			GridLineStyle style1;
			Rectangle rectangle1 = bounds;
			if (base.dgTable.IsDefault)
			{
				style1 = base.Grid.GridLineStyle;
			}
			else
			{
				style1 = base.dgTable.dataGrid.GridLineStyle;
			}
			int num1 = (base.Grid == null) ? 0 : ((style1 == GridLineStyle.Solid) ? 1 : 0);
			rectangle1.Height -= num1;
			int num2 = base.PaintData(g, rectangle1, firstVisibleColumn, columnCount, alignToRight);
			if (num1 > 0)
			{
				this.PaintBottomBorder(g, bounds, num2, num1, alignToRight);
			}
			return num2;
		}

 
		protected override void PaintCellContents(Graphics g, Rectangle cellBounds, GridColumnStyle column, Brush backBr, Brush foreBrush, bool alignToRight)
		{
			if (this.DataBound)
			{
				BindManager manager1 = base.Grid.ListManager;
				column.Paint(g, cellBounds, manager1, base.RowNumber, alignToRight);
			}
			else
			{
				base.PaintCellContents(g, cellBounds, column, backBr, foreBrush, alignToRight);
			}
		}

 
		internal override bool ProcessTabKey(Keys keyData, Rectangle rowHeaders, bool alignToRight)
		{
			return false;
		}


		// Properties
		public bool DataBound
		{
			get
			{
				return this.dataBound;
			}
			set
			{
				this.dataBound = value;
			}
		}

		// Fields
		private bool dataBound;
	}
 
}