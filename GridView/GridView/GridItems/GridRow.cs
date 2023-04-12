using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Security.Permissions;
using System.Drawing.Imaging;


namespace Nistec.GridView
{

	internal abstract class GridRow : MarshalByRefObject
	{

        // Fields
        //private AccessibleObject accessibleObject;
        private static ColorMap[] colorMap;
        protected GridTableStyle dgTable;
        private static Bitmap errorBmp;
        private int height;
        private static Bitmap leftArrow;
        protected internal int number;
        private static Bitmap pencilBmp;
        private static Bitmap pencilRowBmp;
        private static Bitmap pencilRightBmp;
        private static Bitmap pencilLeftBmp;
        private static Bitmap rightArrow;
        private bool selected;
        private static Bitmap starBmp;
        private string tooltip;
        private IntPtr tooltipID;
        protected const int xOffset = 3;
        protected const int yOffset = 2;

		#region  Methods
		static GridRow()
		{
			GridRow.colorMap = new ColorMap[] { new ColorMap() };
			GridRow.rightArrow = null;
			GridRow.leftArrow = null;
			GridRow.errorBmp = null;
			GridRow.pencilBmp = null;
			GridRow.starBmp = null;
   		}

		public GridRow(Grid dataGrid, GridTableStyle dgTable, int rowNumber)
		{
			this.tooltipID = new IntPtr(-1);
			this.tooltip = string.Empty;
			if ((dataGrid == null) || (dgTable.Grid == null))
			{
				throw new ArgumentNullException("dataGrid");
			}
			if (rowNumber < 0)
			{
				throw new ArgumentException("GridRowRowNumber", "rowNumber");
			}
			this.number = rowNumber;
			GridRow.colorMap[0].OldColor = Color.Black;
			GridRow.colorMap[0].NewColor = dgTable.dataGrid.HeaderForeColor;
			this.dgTable = dgTable;
            this.height = dataGrid.GetMinimumRowHeight();//-- this.MinimumRowHeight(dgTable);
		}

		protected Brush BackBrushForDataPaint(ref GridCell current, GridColumnStyle gridColumn, int column)
		{
			Brush brush1 = this.GetBackBrush();
			if (this.Selected)
			{
				brush1 = this.dgTable.IsDefault ? this.Grid.SelectionBackBrush : this.dgTable.dataGrid.SelectionBackBrush;
			}
			return brush1;
		}
 
        //protected virtual AccessibleObject CreateAccessibleObject()
        //{
        //    return new GridRow.GridRowAccessibleObject(this);
        //}

		protected Brush ForeBrushForDataPaint(ref GridCell current, GridColumnStyle gridColumn, int column)
		{
			Brush brush1 = this.dgTable.IsDefault ? this.Grid.ForeBrush : this.dgTable.dataGrid.ForeBrush;
			if (this.Selected)
			{
				brush1 = this.dgTable.IsDefault ? this.Grid.SelectionForeBrush : this.dgTable.dataGrid.SelectionForeBrush;
			}
			return brush1;
		}

		protected Brush GetBackBrush()
		{
			Brush brush1 = this.dgTable.IsDefault ? this.Grid.BackBrush : this.dgTable.dataGrid.BackBrush;
			if (this.Grid.LedgerStyle && ((this.RowNumber % 2) == 1))
			{
				brush1 =this.Grid.AlternatingBackBrush;//TableStyle: this.dgTable.IsDefault ? this.Grid.AlternatingBackBrush : this.dgTable.AlternatingBackBrush;
			}
			return brush1;
		}

		protected Bitmap GetBitmap(string bitmapName)
		{
			Bitmap bitmap1 = null;
			try
			{
				bitmap1 = new Bitmap(typeof(GridRow),"Images." + bitmapName);
				bitmap1.MakeTransparent();
			}
			catch (Exception exception1)
			{
				throw exception1;
			}
			return bitmap1;
		}

		public virtual Rectangle GetCellBounds(int col)
		{
			int num1 = this.dgTable.Grid.FirstVisibleColumn;
			int num2 = 0;
			Rectangle rectangle1 = new Rectangle();
			GridColumnCollection collection1 = this.dgTable.GridColumnStyles;
			if (collection1 == null)
			{
				return rectangle1;
			}
			for (int num3 = num1; num3 < col; num3++)
			{
                if (collection1[num3].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
				{
					num2 += collection1[num3].Width;
				}
			}
			int num4 = this.dgTable.dataGrid.GridLineWidth;
			return new Rectangle(num2, 0, collection1[col].Width - num4, this.Height - num4);
		}

		protected Bitmap GetErrorBitmap()
		{
			if (GridRow.errorBmp == null)
			{
				GridRow.errorBmp = this.GetBitmap("GridRow.error.bmp");
			}
			GridRow.errorBmp.MakeTransparent();
			return GridRow.errorBmp;
		}

		protected Bitmap GetLeftArrowBitmap()
		{
			if (GridRow.leftArrow == null)
			{
				GridRow.leftArrow = this.GetBitmap("GridRow.left.bmp");
			}
			return GridRow.leftArrow;
		}

		public virtual Rectangle GetNonScrollableArea()
		{
			return Rectangle.Empty;
		}

		protected Bitmap GetPencilBitmap()
		{
			if (GridRow.pencilBmp == null)
			{
				GridRow.pencilBmp = this.GetBitmap("GridRow.pencil.bmp");
			}
			return GridRow.pencilBmp;
		}

        protected Bitmap GetPencilRowBitmap()
        {
            if (GridRow.pencilRowBmp == null)
            {
                GridRow.pencilRowBmp = this.GetBitmap("GridRow.pencilRow.bmp");
            }
            return GridRow.pencilRowBmp;
        }

        protected Bitmap GetPencilRightBitmap()
        {
            if (GridRow.pencilRightBmp == null)
            {
                GridRow.pencilRightBmp = this.GetBitmap("GridRow.pencilRight.bmp");
            }
            return GridRow.pencilRightBmp;
        }
       
        protected Bitmap GetPencilLeftBitmap()
        {
            if (GridRow.pencilLeftBmp == null)
            {
                GridRow.pencilLeftBmp = this.GetBitmap("GridRow.pencilLeft.bmp");
            }
            return GridRow.pencilLeftBmp;
        }

		protected Bitmap GetRightArrowBitmap()
		{
			if (GridRow.rightArrow == null)
			{
				GridRow.rightArrow = this.GetBitmap("GridRow.right.bmp");
			}
			return GridRow.rightArrow;
		}

 
		protected Bitmap GetStarBitmap()
		{
			if (GridRow.starBmp == null)
			{
				GridRow.starBmp = this.GetBitmap("GridRow.star.bmp");
			}
			return GridRow.starBmp;
		}

 
		public virtual void InvalidateRow()
		{
			this.dgTable.Grid.InvalidateRow(this.number);
		}

		public virtual void InvalidateRowRect(Rectangle r)
		{
			this.dgTable.Grid.InvalidateRowRect(this.number, r);
		}

 
		internal abstract void LoseChildFocus(Rectangle rowHeaders, bool alignToRight);
		
        protected internal virtual int MinimumRowHeight(GridTableStyle dgTable)
		{
			return this.MinimumRowHeight(dgTable.GridColumnStyles);
		}

 
		protected internal virtual int MinimumRowHeight(GridColumnCollection columns)
		{
			int num1 = this.dgTable.IsDefault ? this.Grid.PreferredRowHeight : this.dgTable.dataGrid.PreferredRowHeight;
			try
			{
				if (this.dgTable.Grid.DataSource == null)
				{
					return num1;
				}
				int num2 = columns.Count;
				for (int num3 = 0; num3 < num2; num3++)
				{
					if (columns[num3].PropertyDescriptor != null)
					{
						num1 = Math.Max(num1, columns[num3].GetMinimumHeight());
					}
				}
			}
			catch (Exception)
			{
			}
			return num1;
		}

 
		public virtual void OnEdit()
		{
		}

 
		public virtual bool OnKeyPress(Keys keyData)
		{
			int num1 = this.dgTable.Grid.CurrentCell.ColumnNumber;
			GridColumnCollection collection1 = this.dgTable.GridColumnStyles;
			if (((collection1 != null) && (num1 >= 0)) && (num1 < collection1.Count))
			{
				GridColumnStyle style1 = collection1[num1];
				if (style1.KeyPress(this.RowNumber, keyData))
				{
					return true;
				}
			}
			return false;
		}

		public virtual bool OnMouseDown(int x, int y, Rectangle rowHeaders)
		{
			return this.OnMouseDown(x, y, rowHeaders, false);
		}

 
		public virtual bool OnMouseDown(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			this.LoseChildFocus(rowHeaders, alignToRight);
			return false;
		}

        //public virtual void OnMouseUp(int x, int y, Rectangle rowHeaders, bool alignToRight)
        //{
        //}

		public virtual void OnMouseLeft()
		{
		}

		public virtual void OnMouseLeft(Rectangle rowHeaders, bool alignToRight)
		{
		}

		public virtual bool OnMouseMove(int x, int y, Rectangle rowHeaders)
		{
			return false;
		}
 		public virtual bool OnMouseMove(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			return false;
		}
 		public virtual void OnRowEnter()
		{
            //int i = this.RowNumber;

		}

 		public virtual void OnRowLeave()
		{
            //int i=this.RowNumber;
		}
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dataBounds"></param>
        /// <param name="rowBounds"></param>
        /// <param name="firstVisibleColumn"></param>
        /// <param name="numVisibleColumns"></param>
        /// <returns></returns>
		public abstract int Paint(Graphics g, Rectangle dataBounds, Rectangle rowBounds, int firstVisibleColumn, int numVisibleColumns);
         ///<summary>
         ///Overloaded. When overridden in a derived class, paints the column in a Grid control. 
         ///</summary>
        /// <param name="g"></param>
        /// <param name="dataBounds"></param>
        /// <param name="rowBounds"></param>
        /// <param name="firstVisibleColumn"></param>
        /// <param name="numVisibleColumns"></param>
        /// <param name="alignToRight"></param>
        /// <returns></returns>
        public abstract int Paint(Graphics g, Rectangle dataBounds, Rectangle rowBounds, int firstVisibleColumn, int numVisibleColumns, bool alignToRight);
		protected virtual void PaintBottomBorder(Graphics g, Rectangle bounds, int dataWidth)
		{
			this.PaintBottomBorder(g, bounds, dataWidth, this.dgTable.dataGrid.GridLineWidth, false);
		}

		protected virtual void PaintBottomBorder(Graphics g, Rectangle bounds, int dataWidth, int borderWidth, bool alignToRight)
		{
            Rectangle rectangle1 = new Rectangle(alignToRight ? (bounds.Right - dataWidth) : bounds.X, bounds.Bottom - borderWidth, dataWidth, borderWidth);
            g.FillRectangle(this.dgTable.IsDefault ? this.Grid.GridLineBrush : this.dgTable.dataGrid.GridLineBrush, rectangle1);
            if (dataWidth < bounds.Width)
            {
                g.FillRectangle(this.dgTable.Grid.BackgroundBrush, alignToRight ? bounds.X : rectangle1.Right, rectangle1.Y, bounds.Width - rectangle1.Width, borderWidth);
            }
		}

		protected virtual void PaintCellContents(Graphics g, Rectangle cellBounds, GridColumnStyle column, Brush backBr, Brush foreBrush)
		{
			this.PaintCellContents(g, cellBounds, column, backBr, foreBrush, false);
		}

		protected virtual void PaintCellContents(Graphics g, Rectangle cellBounds, GridColumnStyle column, Brush backBr, Brush foreBrush, bool alignToRight)
		{
			g.FillRectangle(backBr, cellBounds);
		}

 
		public virtual int PaintData(Graphics g, Rectangle bounds, int firstVisibleColumn, int columnCount)
		{
			return this.PaintData(g, bounds, firstVisibleColumn, columnCount, false);
		}

 
		public virtual int PaintData(Graphics g, Rectangle bounds, int firstVisibleColumn, int columnCount, bool alignToRight)
		{
			Rectangle rectangle1 = bounds;
			int num1 = this.dgTable.IsDefault ? this.Grid.GridLineWidth : this.dgTable.dataGrid.GridLineWidth;
			int num2 = 0;
			GridCell cell1 = this.dgTable.Grid.CurrentCell;
			GridColumnCollection collection1 = this.dgTable.GridColumnStyles;
			int num3 = collection1.Count;
			for (int num4 = firstVisibleColumn; num4 < num3; num4++)
			{
				if (num2 > bounds.Width)
				{
					break;
				}
                if (collection1[num4].IsVisibleInternal)/*bound*///(collection1[num4].PropertyDescriptor != null) && (collection1[num4].Width > 0))
				{
					rectangle1.Width = collection1[num4].Width - num1;
					if (alignToRight)
					{
						rectangle1.X = (bounds.Right - num2) - rectangle1.Width;
					}
					else
					{
						rectangle1.X = bounds.X + num2;
					}
					Brush brush1 = this.BackBrushForDataPaint(ref cell1, collection1[num4], num4);
					Brush brush2 = this.ForeBrushForDataPaint(ref cell1, collection1[num4], num4);
					this.PaintCellContents(g, rectangle1, collection1[num4], brush1, brush2, alignToRight);
					if (num1 > 0)
					{
						g.FillRectangle(this.dgTable.IsDefault ? this.Grid.GridLineBrush : this.dgTable.dataGrid.GridLineBrush, alignToRight ? (rectangle1.X - num1) : rectangle1.Right, rectangle1.Y, num1, rectangle1.Height);
					}
					num2 += rectangle1.Width + num1;
				}
			}
			if (num2 < bounds.Width)
			{
				g.FillRectangle(this.dgTable.Grid.BackgroundBrush, alignToRight ? bounds.X : (bounds.X + num2), bounds.Y, bounds.Width - num2, bounds.Height);
			}
			return num2;
		}

 
		public virtual void PaintHeader(Graphics g, Rectangle visualBounds)
		{
			this.PaintHeader(g, visualBounds, false);
		}

 
		public virtual void PaintHeader(Graphics g, Rectangle visualBounds, bool alignToRight)
		{
			this.PaintHeader(g, visualBounds, alignToRight, false,false);
		}

         public virtual void PaintHeader(Graphics g, Rectangle visualBounds, bool alignToRight, bool isCellDirty,bool isRowDirty)
		{
			Bitmap bitmap1;
			object obj1;
			Bitmap bitmap2;
			Rectangle rectangle3;
			Rectangle rectangle1 = visualBounds;
			if (this is GridAddNewRow)
			{
				bitmap1 = this.GetStarBitmap();
				lock ((bitmap2 = bitmap1))
				{
					rectangle3 = this.PaintIcon(g, rectangle1, true, alignToRight, bitmap1);
					rectangle1.X += rectangle3.Width + 3;
					//return;
				}
				return;
			}

            if (isCellDirty)//(rowIsDirty)
			{
				bitmap1 = this.GetPencilBitmap();
				lock ((bitmap2 = bitmap1))
				{
					rectangle3 = this.PaintIcon(g, rectangle1, this.RowNumber == this.Grid.CurrentCell.RowNumber, alignToRight, bitmap1);
					rectangle1.X += rectangle3.Width + 3;
					goto Label_010A;
				}
			}
            else if (isRowDirty)//(this.dgTable.dataGrid.DirtyRow)
            {
                //bitmap1 = this.GetPencilRowBitmap();
                bitmap1=alignToRight ? this.GetPencilLeftBitmap() : this.GetPencilRightBitmap();
                lock ((bitmap2 = bitmap1))
                {
                    rectangle3 = this.PaintIcon(g, rectangle1, this.RowNumber == this.Grid.CurrentCell.RowNumber, alignToRight, bitmap1);
                    rectangle1.X += rectangle3.Width + 3;
                    goto Label_010A;
                }

            }
			bitmap1 = alignToRight ? this.GetLeftArrowBitmap() : this.GetRightArrowBitmap();
			lock ((bitmap2 = bitmap1))
			{
				rectangle3 = this.PaintIcon(g, rectangle1, this.RowNumber == this.Grid.CurrentCell.RowNumber, alignToRight, bitmap1);
				rectangle1.X += rectangle3.Width + 3;
			}
        Label_010A:
			obj1 = this.Grid.ListManager[this.number];
			if (obj1 is IDataErrorInfo)
			{
				string text1 = ((IDataErrorInfo) obj1).Error;
				if (text1 == null)
				{
					text1 = string.Empty;
				}
				if ((this.tooltip != text1) && (this.tooltip.Length > 0))
				{
					this.Grid.ToolTipProvider.RemoveToolTip(this.tooltipID);
					this.tooltip = string.Empty;
					this.tooltipID = new IntPtr(-1);
				}
				if (text1.Length != 0)
				{
					Rectangle rectangle2;
					int num1;
					bitmap1 = this.GetErrorBitmap();
					lock ((bitmap2 = bitmap1))
					{
						rectangle2 = this.PaintIcon(g, rectangle1, true, alignToRight, bitmap1);
					}
					rectangle1.X += rectangle2.Width + 3;
					this.tooltip = text1;
					Grid grid1 = this.Grid;
					grid1.ToolTipId = (num1 = grid1.ToolTipId) + 1;
					this.tooltipID = (IntPtr) num1;
					this.Grid.ToolTipProvider.AddToolTip(this.tooltip, this.tooltipID, rectangle2);
				}
			}
		}

        protected Rectangle PaintIcon(Graphics g, Rectangle visualBounds, bool paintIcon, bool alignToRight, Bitmap bmp)
        {
            /*ControlLayout*/
            return this.PaintIcon(g, visualBounds, paintIcon, alignToRight, bmp, this.Grid.GetHeaderBackBrush(false, visualBounds));
            //return this.PaintIcon(g, visualBounds, paintIcon, alignToRight, bmp, this.dgTable.IsDefault ? this.Grid.HeaderBackBrush : this.dgTable.dataGrid.HeaderBackBrush);
        }

 
		protected Rectangle PaintIcon(Graphics g, Rectangle visualBounds, bool paintIcon, bool alignToRight, Bitmap bmp, Brush backBrush)
		{
			Size size1 = bmp.Size;
			Rectangle rectangle1 = new Rectangle(alignToRight ? ((visualBounds.Right - 3) - size1.Width) : (visualBounds.X + 3), visualBounds.Y + 2, size1.Width, size1.Height);
			g.FillRectangle(backBrush, visualBounds);
            if (paintIcon)
            {
                GridRow.colorMap[0].NewColor = this.dgTable.IsDefault ? this.Grid.HeaderForeColor : this.dgTable.dataGrid.HeaderForeColor;
                GridRow.colorMap[0].OldColor = Color.Black;
                ImageAttributes attributes1 = new ImageAttributes();
                attributes1.SetRemapTable(GridRow.colorMap, ColorAdjustType.Bitmap);
                g.DrawImage(bmp, rectangle1, 0, 0, rectangle1.Width, rectangle1.Height, GraphicsUnit.Pixel, attributes1);
                attributes1.Dispose();
            }
			return rectangle1;
		}

		protected int PaintPositionArrow(Graphics g, Rectangle bounds, Brush backBrush, bool alignToRight)
		{
			Bitmap bitmap1 = alignToRight ? this.GetLeftArrowBitmap() : this.GetRightArrowBitmap();
			lock (bitmap1)
			{
				Size size1 = bitmap1.Size;
				Rectangle rectangle1 = new Rectangle(alignToRight ? ((bounds.Right - 3) - size1.Width) : (bounds.X + 3), bounds.Y + 2, size1.Width, size1.Height);
				if (this.dgTable.IsDefault)
				{
					g.FillRectangle(this.Grid.HeaderBackBrush, bounds);
				}
				else
				{
					g.FillRectangle(this.dgTable.dataGrid.HeaderBackBrush, bounds);
				}
				if (this.RowNumber == this.Grid.CurrentCell.RowNumber)
				{
					GridRow.colorMap[0].NewColor = this.dgTable.dataGrid.HeaderForeColor;
					ImageAttributes attributes1 = new ImageAttributes();
					attributes1.SetRemapTable(GridRow.colorMap, ColorAdjustType.Bitmap);
					g.DrawImage(bitmap1, rectangle1, rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height, GraphicsUnit.Pixel, attributes1);
					attributes1.Dispose();
				}
				return size1.Width;
			}
		}

 
		internal abstract bool ProcessTabKey(Keys keyData, Rectangle rowHeaders, bool alignToRight);
 


		#endregion

		#region Properties
        //public AccessibleObject AccessibleObject
        //{
        //    get
        //    {
        //        if (this.accessibleObject == null)
        //        {
        //            this.accessibleObject = this.CreateAccessibleObject();
        //        }
        //        return this.accessibleObject;
        //    }
        //}
		public Grid Grid
		{
			get
			{
				return this.dgTable.Grid;
			}
		}
 
		internal GridTableStyle GridTableStyle
		{
			get
			{
				return this.dgTable;
			}
			set
			{
				this.dgTable = value;
			}
		}
 
		public virtual int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = Math.Max(0, value);
				this.dgTable.Grid.OnRowHeightChanged(this);
			}
		}
		public int RowNumber
		{
			get
			{
				return this.number;
			}
		}
		public virtual bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
				this.InvalidateRow();
			}
		}
 

		#endregion


		#region accessible

//		// Nested Types
        //[ComVisible(true)]
        //protected class GridCellAccessibleObject : AccessibleObject
        //{
        //    // Methods
        //        public GridCellAccessibleObject(GridRow owner, int column)
        //        {
        //            this.owner = null;
        //            this.owner = owner;
        //            this.column = column;
        //        }

        //        public override void DoDefaultAction()
        //        {
        //            this.Select(AccessibleSelection.TakeSelection | AccessibleSelection.TakeFocus);
        //        }

 
        //        public override AccessibleObject GetFocused()
        //        {
        //            return this.Grid.AccessibilityObject.GetFocused();
        //        }

        //        public override AccessibleObject Navigate(AccessibleNavigation navdir)
        //        {
        //            switch (navdir)
        //            {
        //                case AccessibleNavigation.Up:
        //                    return this.Grid.AccessibilityObject.GetChild(((1 + this.owner.dgTable.GridColumnStyles.Count) + this.owner.RowNumber) - 1).Navigate(AccessibleNavigation.FirstChild);

        //                case AccessibleNavigation.Down:
        //                    return this.Grid.AccessibilityObject.GetChild(((1 + this.owner.dgTable.GridColumnStyles.Count) + this.owner.RowNumber) + 1).Navigate(AccessibleNavigation.FirstChild);

        //                case AccessibleNavigation.Left:
        //                case AccessibleNavigation.Previous:
        //                {
        //                    if (this.column > 0)
        //                    {
        //                        return this.owner.AccessibleObject.GetChild(this.column - 1);
        //                    }
        //                    AccessibleObject obj2 = this.Grid.AccessibilityObject.GetChild(((1 + this.owner.dgTable.GridColumnStyles.Count) + this.owner.RowNumber) - 1);
        //                    if (obj2 != null)
        //                    {
        //                        return obj2.Navigate(AccessibleNavigation.LastChild);
        //                    }
        //                    break;
        //                }
        //                case AccessibleNavigation.Right:
        //                case AccessibleNavigation.Next:
        //                {
        //                    if (this.column < (this.owner.AccessibleObject.GetChildCount() - 1))
        //                    {
        //                        return this.owner.AccessibleObject.GetChild(this.column + 1);
        //                    }
        //                    AccessibleObject obj1 = this.Grid.AccessibilityObject.GetChild(((1 + this.owner.dgTable.GridColumnStyles.Count) + this.owner.RowNumber) + 1);
        //                    if (obj1 == null)
        //                    {
        //                        break;
        //                    }
        //                    return obj1.Navigate(AccessibleNavigation.FirstChild);
        //                }
        //            }
        //            return null;
        //        }

        //        public override void Select(AccessibleSelection flags)
        //        {
        //            if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
        //            {
        //                this.Grid.Focus();
        //            }
        //            if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
        //            {
        //                this.Grid.CurrentCell = new GridCell(this.owner.RowNumber, this.column);
        //            }
        //        }

 
        //        public override Rectangle Bounds
        //        {
        //            get
        //            {
        //                return this.Grid.RectangleToScreen(this.Grid.GetCellBounds(new GridCell(this.owner.RowNumber, this.column)));
        //            }
        //        }
 
        //        protected Grid Grid
        //        {
        //            get
        //            {
        //                return this.owner.Grid;
        //            }
        //        }
 
        //        public override string DefaultAction
        //        {
        //            get
        //            {
        //                return "AccDGEdit";
        //            }
        //        }
 
        //        public override string Name
        //        {
        //            get
        //            {
        //                return this.Grid.myGridTable.GridColumnStyles[this.column].HeaderText;
        //            }
        //        }
        //        protected GridRow Owner
        //        {
        //            get
        //            {
        //                return this.owner;
        //            }
        //        }
 
        //        public override AccessibleObject Parent
        //        {
        //            get
        //            {
        //                return this.owner.AccessibleObject;
        //            }
        //        }
        //        public override AccessibleRole Role
        //        {
        //            get
        //            {
        //                return AccessibleRole.Cell;
        //            }
        //        }
        //        public override AccessibleStates State
        //        {
        //            get
        //            {
        //                AccessibleStates states1 = AccessibleStates.Selectable | AccessibleStates.Focusable;
        //                if ((this.Grid.CurrentCell.RowNumber != this.owner.RowNumber) || (this.Grid.CurrentCell.ColumnNumber != this.column))
        //                {
        //                    return states1;
        //                }
        //                if (this.Grid.Focused)
        //                {
        //                    states1 |= AccessibleStates.Focused;
        //                }
        //                return (states1 | AccessibleStates.Selected);
        //            }
        //        }
 
        //        public override string Value
        //        {
        //            get
        //            {
        //                if (this.owner is GridAddNewRow)
        //                {
        //                    return null;
        //                }
        //                return GridRow.GridRowAccessibleObject.CellToDisplayString(this.Grid, this.owner.RowNumber, this.column);
        //            }
        //            set
        //            {
        //                if (!(this.owner is GridAddNewRow))
        //                {
        //                    object obj1 = GridRow.GridRowAccessibleObject.DisplayStringToCell(this.Grid, this.owner.RowNumber, this.column, value);
        //                    this.Grid[this.owner.RowNumber, this.column] = obj1;
        //                }
        //            }
        //        }

        //    // Properties

        //    // Fields
        //    private int column;
        //    private GridRow owner;
        //}

        //[ComVisible(true)]
        //protected class GridRowAccessibleObject : AccessibleObject
        //{
        //    // Methods
        //    public GridRowAccessibleObject(GridRow owner)
        //    {
        //        this.owner = null;
        //        this.owner = owner;
        //        Grid grid1 = this.Grid;
        //        this.EnsureChildren();
        //    }

        //    protected virtual void AddChildAccessibleObjects(IList children)
        //    {
        //        int num1 = this.Grid.myGridTable.GridColumnStyles.Count;
        //        for (int num2 = 0; num2 < num1; num2++)
        //        {
        //            children.Add(this.CreateCellAccessibleObject(num2));
        //        }
        //    }

        //    internal static string CellToDisplayString(Grid grid, int row, int column)
        //    {
        //        if (column < grid.myGridTable.GridColumnStyles.Count)
        //        {
        //            return grid.myGridTable.GridColumnStyles[column].PropertyDescriptor.Converter.ConvertToString(grid[row, column]);
        //        }
        //        return "";
        //    }

 
        //    protected virtual AccessibleObject CreateCellAccessibleObject(int column)
        //    {
        //        return new GridRow.GridCellAccessibleObject(this.owner, column);
        //    }

 
        //    internal static object DisplayStringToCell(Grid grid, int row, int column, string value)
        //    {
        //        if (column < grid.myGridTable.GridColumnStyles.Count)
        //        {
        //            return grid.myGridTable.GridColumnStyles[column].PropertyDescriptor.Converter.ConvertFromString(value);
        //        }
        //        return null;
        //    }

        //    private void EnsureChildren()
        //    {
        //        if (this.cells == null)
        //        {
        //            this.cells = new ArrayList(this.Grid.myGridTable.GridColumnStyles.Count + 2);
        //            this.AddChildAccessibleObjects(this.cells);
        //        }
        //    }

 
        //    public override AccessibleObject GetChild(int index)
        //    {
        //        if (index < this.cells.Count)
        //        {
        //            return (AccessibleObject) this.cells[index];
        //        }
        //        return null;
        //    }

        //    public override int GetChildCount()
        //    {
        //        return this.cells.Count;
        //    }

        //    public override AccessibleObject GetFocused()
        //    {
        //        if (this.Grid.Focused)
        //        {
        //            GridCell cell1 = this.Grid.CurrentCell;
        //            if (cell1.RowNumber == this.owner.RowNumber)
        //            {
        //                return (AccessibleObject) this.cells[cell1.ColumnNumber];
        //            }
        //        }
        //        return null;
        //    }

 
        //    public override AccessibleObject Navigate(AccessibleNavigation navdir)
        //    {
        //        switch (navdir)
        //        {
        //            case AccessibleNavigation.Up:
        //            case AccessibleNavigation.Left:
        //            case AccessibleNavigation.Previous:
        //                return this.Grid.AccessibilityObject.GetChild(((1 + this.owner.dgTable.GridColumnStyles.Count) + this.owner.RowNumber) - 1);

        //            case AccessibleNavigation.Down:
        //            case AccessibleNavigation.Right:
        //            case AccessibleNavigation.Next:
        //                return this.Grid.AccessibilityObject.GetChild(((1 + this.owner.dgTable.GridColumnStyles.Count) + this.owner.RowNumber) + 1);

        //            case AccessibleNavigation.FirstChild:
        //                if (this.GetChildCount() > 0)
        //                {
        //                    return this.GetChild(0);
        //                }
        //                break;

        //            case AccessibleNavigation.LastChild:
        //                if (this.GetChildCount() > 0)
        //                {
        //                    return this.GetChild(this.GetChildCount() - 1);
        //                }
        //                break;
        //        }
        //        return null;
        //    }

 
        //    public override void Select(AccessibleSelection flags)
        //    {
        //        if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
        //        {
        //            this.Grid.Focus();
        //        }
        //        if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
        //        {
        //            this.Grid.CurrentRowIndex = this.owner.RowNumber;
        //        }
        //    }



        //    // Properties
        //    public override Rectangle Bounds
        //    {
        //        get
        //        {
        //            return this.Grid.RectangleToScreen(this.Grid.GetRowBounds(this.owner));
        //        }
        //    }
 
        //    private Grid Grid
        //    {
        //        get
        //        {
        //            return this.owner.Grid;
        //        }
        //    }
        //    public override string Name
        //    {
        //        get
        //        {
        //            if (this.owner is GridAddNewRow)
        //            {
        //                return "AccDGNewRow";
        //            }
        //            return GridRow.GridRowAccessibleObject.CellToDisplayString(this.Grid, this.owner.RowNumber, 0);
        //        }
        //    }
 
        //    protected GridRow Owner
        //    {
        //        get
        //        {
        //            return this.owner;
        //        }
        //    }
 
        //    public override AccessibleObject Parent
        //    {
        //        get
        //        {
        //            return this.Grid.AccessibilityObject;
        //        }
        //    }
        //    public override AccessibleRole Role
        //    {
        //        get
        //        {
        //            return AccessibleRole.Row;
        //        }
        //    }
 
        //    public override AccessibleStates State
        //    {
        //        get
        //        {
        //            AccessibleStates states1 = AccessibleStates.Selectable | AccessibleStates.Focusable;
        //            if (this.Grid.CurrentCell.RowNumber == this.owner.RowNumber)
        //            {
        //                states1 |= AccessibleStates.Focused;
        //            }
        //            if (this.Grid.CurrentRowIndex == this.owner.RowNumber)
        //            {
        //                states1 |= AccessibleStates.Selected;
        //            }
        //            return states1;
        //        }
        //    }
        //    public override string Value
        //    {
        //        get
        //        {
        //            return this.Name;
        //        }
        //    }

        //    // Fields
        //    private ArrayList cells;
        //    private GridRow owner;
        //}

		#endregion
	}
 
}