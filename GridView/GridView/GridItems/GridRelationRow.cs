using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;

using Nistec.WinForms;

namespace Nistec.GridView
{

	internal class GridRelationshipRow : GridRow
	{
		#region Ctor

		public GridRelationshipRow(Grid dataGrid, GridTableStyle dgTable, int rowNumber) : base(dataGrid, dgTable, rowNumber)
		{
            this.expanded = false;
        }
        #endregion 

        #region Methods

        private Rectangle GetRelationshipRectWithMirroring()
		{
			Rectangle rectangle1 =Rectangle.Empty;// this.GetRelationshipRect();
			if (base.Grid.RowHeadersVisible)//base.dgTable.IsDefault ? base.Grid.RowHeadersVisible : base.dgTable.RowHeadersVisible)
			{
				int num1 =base.Grid.RowHeaderWidth;// base.dgTable.IsDefault ? base.Grid.RowHeaderWidth : base.dgTable.RowHeaderWidth;
				Rectangle rectangle2 = base.Grid.GetRowHeaderRect();
				rectangle1.X += rectangle2.X + num1;
			}
			rectangle1.X = this.MirrorRelationshipRectangle(rectangle1, base.Grid.GetRowHeaderRect(), base.Grid.RightToLeft == RightToLeft.Yes);
			return rectangle1;
		}

		internal override void LoseChildFocus(Rectangle rowHeaders, bool alignToRight)
		{
			Rectangle rectangle1 =Rectangle.Empty;// this.GetRelationshipRect();
			rectangle1.X += rowHeaders.X + base.dgTable.dataGrid.RowHeaderWidth;
			rectangle1.X = this.MirrorRelationshipRectangle(rectangle1, rowHeaders, alignToRight);
			this.InvalidateRowRect(rectangle1);
		}

 
		protected internal override int MinimumRowHeight(GridTableStyle dgTable)
		{
			return (base.MinimumRowHeight(dgTable));// + (this.expanded ? this.GetRelationshipRect().Height : 0));
		}

		protected internal override int MinimumRowHeight(GridColumnCollection cols)
		{
			return (base.MinimumRowHeight(cols));// + (this.expanded ? this.GetRelationshipRect().Height : 0));
		}

		private int MirrorRectangle(int x, int width, Rectangle rect, bool alignToRight)
		{
			if (alignToRight)
			{
				return (((rect.Right + rect.X) - width) - x);
			}
			return x;
		}

		private int MirrorRelationshipRectangle(Rectangle relRect, Rectangle rowHeader, bool alignToRight)
		{
			if (alignToRight)
			{
				return (rowHeader.X - relRect.Width);
			}
			return relRect.X;
		}

		public override bool OnKeyPress(Keys keyData)
		{
			if (((keyData & ~Keys.KeyCode) == Keys.Shift) && ((keyData & Keys.KeyCode) != Keys.Tab))
			{
				return false;
			}
			switch ((keyData & Keys.KeyCode))
			{
				case Keys.Tab:
					return false;

				case Keys.Return:
					return false;

				case Keys.F5:
					if (((base.dgTable == null) || (base.dgTable.Grid == null)) || (!base.dgTable.Grid.AllowNavigation))
					{
						return false;
					}
					return true;

				case Keys.NumLock:
					return base.OnKeyPress(keyData);
			}
			return base.OnKeyPress(keyData);
		}

        //public override bool OnMouseDown(int x, int y, Rectangle rowHeaders, bool alignToRight)
        //{

        //    Rectangle rectangle1 = this.GetRelationshipRectWithMirroring();
        //    if (!rectangle1.Contains(x, y))
        //    {
        //        return base.OnMouseDown(x, y, rowHeaders, alignToRight);
        //    }
        //    return true;
        //}

        public override bool OnMouseDown(int x, int y, Rectangle rowHeaders, bool alignToRight)
        {
            if ((base.dgTable.IsDefault ? base.Grid.RowHeadersVisible : base.dgTable.dataGrid.RowHeadersVisible) && this.PointOverPlusMinusGlyph(x, y, rowHeaders, alignToRight))
            {
                if (base.dgTable.RelationsList.Count == 0)
                {
                    return false;
                }
                if (this.expanded)
                {
                    this.Collapse();
                    base.Grid.TableStyle.CloseRelationList(this);
                }
                else if (base.Grid.TableStyle.RelationsList.Count > 1)
                {
                    base.Grid.TableStyle.RelationExpanded += new EventHandler(TableStyle_RelationExpanded);
                    base.Grid.TableStyle.ShowRelationList(this,new Point(x,y));
                    return true;
                }
                else
                {
                    this.Expand();
                    base.Grid.TableStyle.ShowRelationList(this,new Point(x,y));
                    //base.Grid.TableStyle.ShowRelationList(this,GetRelationshipRect());
                }
 
                base.Grid.OnNodeClick(EventArgs.Empty);
                return true;
            }
            if (!this.expanded)
            {
                return base.OnMouseDown(x, y, rowHeaders, alignToRight);
            }
            Rectangle rectangle1 = this.GetRelationshipRectWithMirroring();
            if (!rectangle1.Contains(x, y))
            {
                return base.OnMouseDown(x, y, rowHeaders, alignToRight);
            }
            int num1 = this.RelationFromY(y);
            if (num1 != -1)
            {
                 this.FocusedRelation = -1;
                //- base.Grid.NavigateTo((string)base.dgTable.RelationsList[num1], this, true);
                base.Grid.TableStyle.ShowRelationList(this, new Point(x, y));
            }
            return true;
        }

        void TableStyle_RelationExpanded(object sender, EventArgs e)
        {
            this.Expand();
            base.Grid.OnNodeClick(EventArgs.Empty);
        }


		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int numVisibleColumns)
		{
			return  this.Paint(g, bounds, trueRowBounds, firstVisibleColumn, numVisibleColumns, false);
		}

        public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int numVisibleColumns, bool alignToRight)
        {

            //bool flag1 = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
            int num1 = base.dgTable.dataGrid.BorderWidth;
            Rectangle rectangle1 = bounds;
            rectangle1.Height = base.Height - num1;
            int num2 = this.PaintData(g, rectangle1, firstVisibleColumn, numVisibleColumns, alignToRight);
            int num3 = (num2 + bounds.X) - trueRowBounds.X;
            rectangle1.Offset(0, num1);
            if (num1 > 0)
            {
                this.PaintBottomBorder(g, rectangle1, num2, num1, alignToRight);
            }
            if (this.expanded && (base.dgTable.RelationsList.Count > 0))
            {
                Rectangle rectangle2 = new Rectangle(trueRowBounds.X, rectangle1.Bottom, trueRowBounds.Width, (trueRowBounds.Height - rectangle1.Height) - (2 * num1));
                this.PaintRelations(g, rectangle2, trueRowBounds, num3, firstVisibleColumn, numVisibleColumns, alignToRight);
                rectangle2.Height++;
                if (num1 > 0)
                {
                    this.PaintBottomBorder(g, rectangle2, num3, num1, alignToRight);
                }
            }
           
            return num2;
        }

        //public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int numVisibleColumns, bool alignToRight)
        //{
			
        //    //bool flag1 = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
        //    int num1 = base.dgTable.dataGrid.BorderWidth;
        //    Rectangle rectangle1 = bounds;
        //    rectangle1.Height = base.Height - num1;
        //    int num2 = this.PaintData(g, rectangle1, firstVisibleColumn, numVisibleColumns, alignToRight);
        //    int num3 = (num2 + bounds.X) - trueRowBounds.X;
        //    rectangle1.Offset(0, num1);
        //    if (num1 > 0)
        //    {
        //        this.PaintBottomBorder(g, rectangle1, num2, num1, alignToRight);
        //    }
        //    return num2;
        //}

		protected override void PaintCellContents(Graphics g, Rectangle cellBounds, GridColumnStyle column, Brush backBr, Brush foreBrush, bool alignToRight)
		{
            Rectangle rectangle1 = cellBounds;
            BindManager manager1 = base.Grid.ListManager;

            if (!column.IsBound)/*bound*/
                goto Label_01;

			string text1 = string.Empty;
			object obj1 = base.Grid.ListManager[base.number];
			if (obj1 is IDataErrorInfo)
			{
				text1 = ((IDataErrorInfo) obj1)[column.PropertyDescriptor.Name];
			}
			if ((text1 != null) && !text1.Equals(string.Empty))
			{
				Rectangle rectangle2;
				int num1;
				Bitmap bitmap1 = base.GetErrorBitmap();
				lock (bitmap1)
				{
					rectangle2 = base.PaintIcon(g, rectangle1, true, alignToRight, bitmap1);//, backBr);
				}
				if (alignToRight)
				{
					rectangle1.Width -= rectangle2.Width + 3;
				}
				else
				{
					rectangle1.X += rectangle2.Width + 3;
				}
				Grid grid1 = base.Grid;
				grid1.ToolTipId = (num1 = grid1.ToolTipId) + 1;
				base.Grid.ToolTipProvider.AddToolTip(text1, (IntPtr) num1, rectangle2);
			}
            Label_01:
			column.Paint(g, rectangle1, manager1, base.RowNumber, backBr, foreBrush, alignToRight);
		}


        public override void PaintHeader(Graphics g, Rectangle bounds, bool alignToRight, bool isCellDirty, bool isRowDirty)
        {
            Grid grid1 = base.Grid;
            Rectangle rectangle1 = bounds;
            if (!grid1.FlatMode)
            {
                ControlPaint.DrawBorder3D(g, rectangle1, Border3DStyle.RaisedInner);
                rectangle1.Inflate(-1, -1);
            }
            //if (base.dgTable.IsDefault)
            //{
            //    this.PaintHeaderInside(g, rectangle1, base.Grid.HeaderBackBrush, alignToRight, isCellDirty,isRowDirty);
            //}
            //else
            //{
            //    this.PaintHeaderInside(g, rectangle1, base.dgTable.dataGrid.HeaderBackBrush, alignToRight, isCellDirty,isRowDirty);
            //}
            /*ControlLayout*/
            this.PaintHeaderInside(g, rectangle1, base.Grid.GetHeaderBackBrush(false, rectangle1), alignToRight, isCellDirty, isRowDirty);
        }

 
		public void PaintHeaderInside(Graphics g, Rectangle bounds, Brush backBr, bool alignToRight)
		{
			this.PaintHeaderInside(g, bounds, backBr, alignToRight, false,false);
		}

        public void PaintHeaderInside(Graphics g, Rectangle bounds, Brush backBr, bool alignToRight, bool isCellDirty, bool isRowDirty)
		{
            bool flag1 = (base.dgTable.RelationsList.Count > 0 && base.dgTable.Grid.AllowNavigation);
            int num1 = this.MirrorRectangle(bounds.X, bounds.Width - (flag1 ? 14 : 0), bounds, alignToRight);
            Rectangle rectangle1 = new Rectangle(num1, bounds.Y, bounds.Width - (flag1 ? 14 : 0), bounds.Height);
            base.PaintHeader(g, rectangle1, alignToRight, isCellDirty, isRowDirty);
            int num2 = this.MirrorRectangle(bounds.X + rectangle1.Width, 14, bounds, alignToRight);
            Rectangle rectangle2 = new Rectangle(num2, bounds.Y, 14, bounds.Height);
            if (flag1)
            {
                this.PaintPlusMinusGlyph(g, rectangle2, backBr, alignToRight);
            }

		
            //int num1 = this.MirrorRectangle(bounds.X, bounds.Width , bounds, alignToRight);
            //Rectangle rectangle1 = new Rectangle(num1, bounds.Y, bounds.Width , bounds.Height);

            //base.PaintHeader(g, rectangle1, alignToRight, isCellDirty,isRowDirty);
            //int num2 = this.MirrorRectangle(bounds.X + rectangle1.Width, 14, bounds, alignToRight);
            //Rectangle rectangle2 = new Rectangle(num2, bounds.Y, 14, bounds.Height);
		}


		internal override bool ProcessTabKey(Keys keyData, Rectangle rowHeaders, bool alignToRight)
		{
				return false;
		}

        private void PaintRelations(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int dataWidth, int firstCol, int nCols, bool alignToRight)
        {
            Rectangle rectangle1 = this.GetRelationshipRect();
            rectangle1.X = alignToRight ? (bounds.Right - rectangle1.Width) : bounds.X;
            rectangle1.Y = bounds.Y;
            int num1 = Math.Max(dataWidth, rectangle1.Width);
            Region region1 = g.Clip;
            g.ExcludeClip(rectangle1);
            g.FillRectangle(base.GetBackBrush(), alignToRight ? (bounds.Right - dataWidth) : bounds.X, bounds.Y, dataWidth, bounds.Height);
            g.SetClip(bounds);
            rectangle1.Height -= base.dgTable.dataGrid.BorderWidth;
            g.DrawRectangle(SystemPens.ControlText, rectangle1.X, rectangle1.Y, rectangle1.Width - 1, rectangle1.Height - 1);
            rectangle1.Inflate(-1, -1);
            int num2 = this.PaintRelationText(g, rectangle1, alignToRight);
            if (num2 < rectangle1.Height)
            {
                g.FillRectangle(base.GetBackBrush(), rectangle1.X, rectangle1.Y + num2, rectangle1.Width, rectangle1.Height - num2);
            }
            g.Clip = region1;
            if (num1 < bounds.Width)
            {
                int num3;
                if (base.dgTable.IsDefault)
                {
                    num3 = base.Grid.GridLineWidth;
                }
                else
                {
                    num3 = base.dgTable.dataGrid.GridLineWidth;
                }
                g.FillRectangle(base.Grid.BackgroundBrush, alignToRight ? bounds.X : (bounds.X + num1), bounds.Y, ((bounds.Width - num1) - num3) + 1, bounds.Height);
                if (num3 > 0)
                {
                    Brush brush1;
                    if (base.dgTable.IsDefault)
                    {
                        brush1 = base.Grid.GridLineBrush;
                    }
                    else
                    {
                        brush1 = base.dgTable.dataGrid.GridLineBrush;
                    }
                    g.FillRectangle(brush1, alignToRight ? ((bounds.Right - num3) - num1) : ((bounds.X + num1) - num3), bounds.Y, num3, bounds.Height);
                }
            }
        }


        private int PaintRelationText(Graphics g, Rectangle bounds, bool alignToRight)
        {
            g.FillRectangle(base.GetBackBrush(), bounds.X, bounds.Y, bounds.Width, 1);
            int num1 = base.dgTable.RelationshipHeight;
            Rectangle rectangle1 = new Rectangle(bounds.X, bounds.Y + 1, bounds.Width, num1);
            int num2 = 1;
            for (int num3 = 0; num3 < base.dgTable.RelationsList.Count; num3++)
            {
                if (num2 > bounds.Height)
                {
                    return num2;
                }
                Brush brush1 = base.Grid.LinkBrush;// base.dgTable.IsDefault ? base.Grid.LinkBrush : base.dgTable.LinkBrush;
                Font font1 = base.Grid.Font;
                brush1 = base.Grid.LinkBrush;// base.dgTable.IsDefault ? base.Grid.LinkBrush : base.dgTable.LinkBrush;
                font1 = base.Grid.LinkFont;
                g.FillRectangle(base.GetBackBrush(), rectangle1);
                StringFormat format1 = new StringFormat();
                if (alignToRight)
                {
                    format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                    format1.Alignment = StringAlignment.Far;
                }
                g.DrawString((string)base.dgTable.RelationsList[num3], font1, brush1, (RectangleF)rectangle1, format1);
                if ((num3 == this.FocusedRelation) && (base.number == base.Grid.CurrentCell.RowNumber))
                {
                    rectangle1.Width = base.dgTable.FocusedTextWidth;
                    ControlPaint.DrawFocusRectangle(g, rectangle1, ((SolidBrush)brush1).Color, ((SolidBrush)base.GetBackBrush()).Color);
                    rectangle1.Width = bounds.Width;
                }
                format1.Dispose();
                rectangle1.Y += num1;
                num2 += rectangle1.Height;
            }
            return num2;
        }


		#endregion

        #region properties

        // Properties
        public virtual bool Expanded
        {
            get
            {
                return this.expanded;
            }
            set
            {
                if (this.expanded != value)
                {
                    if (this.expanded)
                    {
                        this.Collapse();
                    }
                    else
                    {
                        this.Expand();
                    }
                }
            }
        }
        private int FocusedRelation
        {
            get
            {
                return base.dgTable.FocusedRelation;
            }
            set
            {
                base.dgTable.FocusedRelation = value;
            }
        }

        public override int Height
        {
            get
            {
                int h = base.Height;
                //if (this.expanded)
                //{
                //    Rectangle rectangle1 = this.GetRelationshipRect();
                //    return (h + rectangle1.Height);
                //}
                return h;
            }
            set
            {
                //if (this.expanded)
                //{
                //    Rectangle rectangle1 = this.GetRelationshipRect();
                //    base.Height = value - rectangle1.Height;
                //}
                //else
                //{
                    base.Height = value;
                //}
            }
        }

        // Fields
        private const bool defaultOpen = false;
        private bool expanded;
        private const int expandoBoxWidth = 14;
        private const int indentWidth = 20;
        private const int triangleSize = 5;


        #endregion

        #region multi
   
        private void Collapse()
        {
            if (this.expanded)
            {
                this.expanded = false;
                this.FocusedRelation = -1;
                base.Grid.OnRowHeightChanged(this);
                base.Grid.TableStyle.RelationExpanded -= new EventHandler(TableStyle_RelationExpanded);
            }
        }


        private void Expand()
        {
            if ((!this.expanded && (base.Grid != null)) && ((base.dgTable != null) && (base.dgTable.RelationsList.Count > 0)))
            {
                this.expanded = true;
                this.FocusedRelation = -1;
                base.Grid.OnRowHeightChanged(this);
                base.Grid.CurrentRowIndex = this.number;
            }
        }

        private void PaintPlusMinusGlyph(Graphics g, Rectangle bounds, Brush backBr, bool alignToRight)
        {
            //bool flag1 = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
            Rectangle rectangle1 = this.GetOutlineRect(bounds.X, bounds.Y);
            rectangle1 = Rectangle.Intersect(bounds, rectangle1);
            if (!rectangle1.IsEmpty)
            {
                g.FillRectangle(backBr, bounds);
                //bool flag2 = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
                Pen pen1 = base.Grid.HeaderForePen;// base.dgTable.IsDefault ? base.Grid.HeaderForePen : base.dgTable.HeaderForePen;
                g.DrawRectangle(pen1, rectangle1.X, rectangle1.Y, rectangle1.Width - 1, rectangle1.Height - 1);
                int num1 = 2;
                g.DrawLine(pen1, rectangle1.X + num1, rectangle1.Y + (rectangle1.Width / 2), (rectangle1.Right - num1) - 1, rectangle1.Y + (rectangle1.Width / 2));
                if (!this.expanded)
                {
                    g.DrawLine(pen1, rectangle1.X + (rectangle1.Height / 2), rectangle1.Y + num1, rectangle1.X + (rectangle1.Height / 2), (rectangle1.Bottom - num1) - 1);
                }
                //else
                //{
                    //Point[] pointArray1 = null;
                    //pointArray1 = new Point[] { new Point(rectangle1.X + (rectangle1.Height / 2), rectangle1.Bottom), new Point(pointArray1[0].X, (bounds.Y + (2 * num1)) + base.Height), new Point(alignToRight ? bounds.X : bounds.Right, pointArray1[1].Y) };
                    //g.DrawLines(pen1, pointArray1);
                //}
            }
        }
 
        private bool PointOverPlusMinusGlyph(int x, int y, Rectangle rowHeaders, bool alignToRight)
        {
            if ((base.dgTable == null) || (base.dgTable.Grid == null) || (!base.dgTable.Grid.AllowNavigation))
            {
                return false;
            }
            Rectangle rectangle1 = rowHeaders;
            if (!base.Grid.FlatMode)
            {
                rectangle1.Inflate(-1, -1);
            }
            Rectangle rectangle2 = this.GetOutlineRect(rectangle1.Right - 14, 0);
            rectangle2.X = this.MirrorRectangle(rectangle2.X, rectangle2.Width, rectangle1, alignToRight);
            return rectangle2.Contains(x, y);
        }

        public override Rectangle GetNonScrollableArea()
        {
            if (this.expanded)
            {
                return this.GetRelationshipRect();
            }
            return Rectangle.Empty;
        }

        private Rectangle GetOutlineRect(Point p)
        {
            return this.GetOutlineRect(p.X, p.Y);
        }

        private Rectangle GetOutlineRect(int xOrigin, int yOrigin)
        {
            return new Rectangle(xOrigin + 2, yOrigin + 2, 9, 9);
        }

        private Rectangle GetRelationshipRect()
        {
            Rectangle rectangle1 = base.dgTable.RelationshipRect;
            
            //rectangle1 = new Rectangle(0, 0, this.Grid.Width - 10, 80);

            rectangle1.Y = base.Height - base.dgTable.dataGrid.BorderWidth;
            
 
            return rectangle1;
        }

        private int RelationFromXY(int x, int y)
        {
            Rectangle rectangle1 = this.GetRelationshipRectWithMirroring();
            if (rectangle1.Contains(x, y))
            {
                return this.RelationFromY(y);
            }
            return -1;
        }

        private int RelationFromY(int y)
        {
            int num1 = -1;
            int num2 = base.dgTable.RelationshipHeight;
            Rectangle rectangle1 = this.GetRelationshipRect();
            int num3 = (base.Height - base.dgTable.dataGrid.BorderWidth) +1;
            while (num3 < rectangle1.Bottom)
            {
                if (num3 > y)
                {
                    break;
                }
                num3 += num2;
                num1++;
            }
            if (num1 >= base.dgTable.RelationsList.Count)
            {
                return -1;
            }
            return num1;
        }

        #endregion

        // Fields
        //private const bool defaultOpen = false;
        //private const int indentWidth = 20;
        //private const int triangleSize = 5;

	}

}