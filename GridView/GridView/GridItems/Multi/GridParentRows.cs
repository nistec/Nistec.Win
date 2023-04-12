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
using System.ComponentModel.Design;

using System.Drawing.Imaging;

using mControl.Util;
using mControl.WinCtl.Controls;


namespace mControl.GridView
{

	internal class GridParentRows
	{
		#region Methods
		static GridParentRows()
		{
			GridParentRows.rightArrow = null;
			GridParentRows.leftArrow = null;
		}

//		public Object()
//		{
//		}

		internal GridParentRows(Grid dataGrid)
		{
			this.backBrush = Grid.DefaultParentRowsBackBrush;
			this.foreBrush = Grid.DefaultParentRowsForeBrush;
			this.borderWidth = 1;
			this.borderBrush = new SolidBrush(SystemColors.WindowFrame);
			this.colorMap = new ColorMap[] { new ColorMap() };
			this.gridLinePen = SystemPens.Control;
			this.totalHeight = 0;
			this.textRegionHeight = 0;
			this.layout = new GridParentRows.Layout();
			this.downLeftArrow = false;
			this.downRightArrow = false;
			this.horizOffset = 0;
			this.parents = new ArrayList();
			this.parentsCount = 0;
			this.rowHeights = new ArrayList();
			this.colorMap[0].OldColor = Color.Black;
			this.dataGrid = dataGrid;
		}

		internal void AddParent(GridState dgs)
		{
			//CurrencyManager manager1 = (CurrencyManager) this.dataGrid.BindingContext[dgs.DataSource, dgs.DataMember];
			BindManager manager1 = (BindManager) this.dataGrid.BindContext[dgs.DataSource, dgs.DataMember];
		
			this.parents.Add(dgs);
			this.SetParentCount(this.parentsCount + 1);
		}

		private int CellCount()
		{
			int num1 = 0;
			num1 = this.ColsCount();
			if ((this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.TableName) || (this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.Both))
			{
				num1++;
			}
			return num1;
		}

 
		internal void CheckNull(object value, string propName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(propName);
			}
		}

 
		internal void Clear()
		{
			for (int num1 = 0; num1 < this.parents.Count; num1++)
			{
				(this.parents[num1] as GridState).RemoveChangeNotification();
			}
			this.parents.Clear();
			this.rowHeights.Clear();
			this.totalHeight = 0;
			this.SetParentCount(0);
		}

		private int ColsCount()
		{
			int num1 = 0;
			for (int num2 = 0; num2 < this.parentsCount; num2++)
			{
				GridState state1 = (GridState) this.parents[num2];
				num1 = Math.Max(num1, state1.GridColumnStyles.Count);
			}
			return num1;
		}

 
		private void ComputeLayout(Rectangle bounds, int tableNameBoxWidth, int[] colsNameWidths, int[] colsDataWidths)
		{
			int num1 = this.TotalWidth(tableNameBoxWidth, colsNameWidths, colsDataWidths);
			if (num1 > bounds.Width)
			{
				this.layout.leftArrow = new Rectangle(bounds.X, bounds.Y, 15, bounds.Height);
				this.layout.data = new Rectangle(this.layout.leftArrow.Right, bounds.Y, bounds.Width - 30, bounds.Height);
				this.layout.rightArrow = new Rectangle(this.layout.data.Right, bounds.Y, 15, bounds.Height);
			}
			else
			{
				this.layout.data = bounds;
				this.layout.leftArrow = Rectangle.Empty;
				this.layout.rightArrow = Rectangle.Empty;
			}
		}

		internal void Dispose()
		{
			this.gridLinePen.Dispose();
		}

 
	
		private Bitmap GetBitmap(string bitmapName, Color transparentColor)
		{
			Bitmap bitmap1 = null;
			try
			{
				bitmap1 = new Bitmap(typeof(GridParentRows),"Images." + bitmapName);
				bitmap1.MakeTransparent(transparentColor);
			}
			catch (Exception)
			{
			}
			return bitmap1;
		}

		internal Rectangle GetBoundsForGridStateAccesibility(GridState dgs)
		{
			Rectangle rectangle1 = Rectangle.Empty;
			int num1 = 0;
			for (int num2 = 0; num2 < this.parentsCount; num2++)
			{
				int num3 = (int) this.rowHeights[num2];
				if (this.parents[num2] == dgs)
				{
					rectangle1.X = this.layout.leftArrow.IsEmpty ? this.layout.data.X : this.layout.leftArrow.Right;
					rectangle1.Height = num3;
					rectangle1.Y = num1;
					rectangle1.Width = this.layout.data.Width;
					return rectangle1;
				}
				num1 += num3;
			}
			return rectangle1;
		}

 
		private int GetColBoxWidth(Graphics g, Font font, int colNum)
		{
			int num1 = 0;
			for (int num2 = 0; num2 < this.parentsCount; num2++)
			{
				GridState state1 = (GridState) this.parents[num2];
				GridColumnCollection collection1 = state1.GridColumnStyles;
				if (colNum < collection1.Count)
				{
					string text1 = collection1[colNum].HeaderText + " :";
					SizeF ef1 = g.MeasureString(text1, font);
					int num3 = (int) ef1.Width;
					num1 = Math.Max(num3, num1);
				}
			}
			return num1;
		}

		private int GetColDataBoxWidth(Graphics g, int colNum)
		{
			int num1 = 0;
			for (int num2 = 0; num2 < this.parentsCount; num2++)
			{
				GridState state1 = (GridState) this.parents[num2];
				GridColumnCollection collection1 = state1.GridColumnStyles;
				if (colNum < collection1.Count)
				{
					//-BindManagerBase
					BindManagerBase bm=this.dataGrid.BindContext[state1.DataSource, state1.DataMember];
					//BindManager bm=this.dataGrid.GetBindManager(state1.DataSource, state1.DataMember);
					object obj1 = collection1[colNum].GetColumnValueAtRow((BindManager)bm, state1.LinkingRow.RowNumber);
					//object obj1 = collection1[colNum].GetColumnValueAtRow((BindManager) this.dataGrid.BindingContext[state1.DataSource, state1.DataMember], state1.LinkingRow.RowNumber);
					Size size1 = collection1[colNum].GetPreferredSize(g, obj1);
					num1 = Math.Max(size1.Width, num1);
				}
			}
			return num1;
		}
 
		private Bitmap GetLeftArrowBitmap()
		{
			if (GridParentRows.leftArrow == null)
			{
				GridParentRows.leftArrow = this.GetBitmap("GridParentRows.LeftArrow.bmp", Color.White);
			}
			return GridParentRows.leftArrow;
		}


		private Bitmap GetRightArrowBitmap()
		{
			if (GridParentRows.rightArrow == null)
			{
				GridParentRows.rightArrow = this.GetBitmap("GridParentRows.RightArrow.bmp", Color.White);
			}
			return GridParentRows.rightArrow;
		}

		private int GetTableBoxWidth(Graphics g, Font font)
		{
			Font font1 = font;
			try
			{
				font1 = new Font(font, FontStyle.Bold);
			}
			catch (Exception)
			{
			}
			int num1 = 0;
			for (int num2 = 0; num2 < this.parentsCount; num2++)
			{
				GridState state1 = (GridState) this.parents[num2];
				string text1 = state1.ListManager.GetListName() + " :";
				SizeF ef1 = g.MeasureString(text1, font1);
				int num3 = (int) ef1.Width;
				num1 = Math.Max(num3, num1);
			}
			return num1;
		}

 
		internal GridState GetTopParent()
		{
			if (this.parentsCount < 1)
			{
				return null;
			}
			return (GridState) ((ICloneable) this.parents[this.parentsCount - 1]).Clone();
		}

		internal void Invalidate()
		{
			if (this.dataGrid != null)
			{
				this.dataGrid.InvalidateParentRows();
			}
		}

		internal void InvalidateRect(Rectangle rect)
		{
			if (this.dataGrid != null)
			{
				Rectangle rectangle1 = new Rectangle(rect.X, rect.Y, rect.Width + this.borderWidth, rect.Height + this.borderWidth);
				this.dataGrid.InvalidateParentRowsRect(rectangle1);
			}
		}

		internal bool IsEmpty()
		{
			return (this.parentsCount == 0);
		}

		private void LeftArrowClick(int cellCount)
		{
			if (this.horizOffset > 0)
			{
				this.ResetMouseInfo();
				this.horizOffset--;
				this.Invalidate();
			}
			else
			{
				this.ResetMouseInfo();
				this.InvalidateRect(this.layout.leftArrow);
			}
		}

		private int MirrorRect(Rectangle surroundingRect, Rectangle containedRect, bool alignToRight)
		{
			if (alignToRight)
			{
				return ((surroundingRect.Right - containedRect.Right) + surroundingRect.X);
			}
			return containedRect.X;
		}
 
		internal void OnLayout()
		{
			if (this.parentsCount != this.rowHeights.Count)
			{
				int num1 = 0;
				if (this.totalHeight == 0)
				{
					this.totalHeight += 2 * this.borderWidth;
				}
				this.textRegionHeight = this.dataGrid.Font.Height + 2;
				if (this.parentsCount > this.rowHeights.Count)
				{
					for (int num3 = this.rowHeights.Count; num3 < this.parentsCount; num3++)
					{
						GridState state1 = (GridState) this.parents[num3];
						GridColumnCollection collection1 = state1.GridColumnStyles;
						int num4 = 0;
						for (int num5 = 0; num5 < collection1.Count; num5++)
						{
							num4 = Math.Max(num4, collection1[num5].GetMinimumHeight());
						}
						num1 = Math.Max(num4, this.textRegionHeight);
						num1++;
						this.rowHeights.Add(num1);
						this.totalHeight += num1;
					}
				}
				else
				{
					if (this.parentsCount == 0)
					{
						this.totalHeight = 0;
					}
					else
					{
						this.totalHeight -= (int) this.rowHeights[this.rowHeights.Count - 1];
					}
					this.rowHeights.RemoveAt(this.rowHeights.Count - 1);
				}
			}
		}

 
		internal void OnMouseDown(int x, int y, bool alignToRight)
		{
			if (!this.layout.rightArrow.IsEmpty)
			{
				int num1 = this.CellCount();
				if (this.layout.rightArrow.Contains(x, y))
				{
					this.downRightArrow = true;
					if (alignToRight)
					{
						this.LeftArrowClick(num1);
					}
					else
					{
						this.RightArrowClick(num1);
					}
				}
				else if (this.layout.leftArrow.Contains(x, y))
				{
					this.downLeftArrow = true;
					if (alignToRight)
					{
						this.RightArrowClick(num1);
					}
					else
					{
						this.LeftArrowClick(num1);
					}
				}
				else
				{
					if (this.downLeftArrow)
					{
						this.downLeftArrow = false;
						this.InvalidateRect(this.layout.leftArrow);
					}
					if (this.downRightArrow)
					{
						this.downRightArrow = false;
						this.InvalidateRect(this.layout.rightArrow);
					}
				}
			}
		}

		internal void OnMouseLeave()
		{
			if (this.downLeftArrow)
			{
				this.downLeftArrow = false;
				this.InvalidateRect(this.layout.leftArrow);
			}
			if (this.downRightArrow)
			{
				this.downRightArrow = false;
				this.InvalidateRect(this.layout.rightArrow);
			}
		}

		internal void OnMouseMove(int x, int y)
		{
			if (this.downLeftArrow)
			{
				this.downLeftArrow = false;
				this.InvalidateRect(this.layout.leftArrow);
			}
			if (this.downRightArrow)
			{
				this.downRightArrow = false;
				this.InvalidateRect(this.layout.rightArrow);
			}
		}

		internal void OnMouseUp(int x, int y)
		{
			this.ResetMouseInfo();
			if (!this.layout.rightArrow.IsEmpty && this.layout.rightArrow.Contains(x, y))
			{
				this.InvalidateRect(this.layout.rightArrow);
			}
			else if (!this.layout.leftArrow.IsEmpty && this.layout.leftArrow.Contains(x, y))
			{
				this.InvalidateRect(this.layout.leftArrow);
			}
		}

		internal void OnResize(Rectangle oldBounds)
		{
			this.Invalidate();
		}

 
		internal void Paint(Graphics g, Rectangle visualbounds, bool alignRight)
		{
			Rectangle rectangle1 = visualbounds;
			if (this.borderWidth > 0)
			{
				this.PaintBorder(g, rectangle1);
				rectangle1.Inflate(-this.borderWidth, -this.borderWidth);
			}
			this.PaintParentRows(g, rectangle1, alignRight);
		}

 
		private void PaintBitmap(Graphics g, Bitmap b, Rectangle bounds)
		{
			int num1 = bounds.X + ((bounds.Width - b.Width) / 2);
			int num2 = bounds.Y + ((bounds.Height - b.Height) / 2);
			Rectangle rectangle1 = new Rectangle(num1, num2, b.Width, b.Height);
			g.FillRectangle(this.BackBrush, rectangle1);
			ImageAttributes attributes1 = new ImageAttributes();
			this.colorMap[0].NewColor = this.ForeColor;
			attributes1.SetRemapTable(this.colorMap, ColorAdjustType.Bitmap);
			g.DrawImage(b, rectangle1, 0, 0, rectangle1.Width, rectangle1.Height, GraphicsUnit.Pixel, attributes1);
			attributes1.Dispose();
		}

 
		private void PaintBorder(Graphics g, Rectangle bounds)
		{
			Rectangle rectangle1 = bounds;
			rectangle1.Height = this.borderWidth;
			g.FillRectangle(this.borderBrush, rectangle1);
			rectangle1.Y = bounds.Bottom - this.borderWidth;
			g.FillRectangle(this.borderBrush, rectangle1);
			rectangle1 = new Rectangle(bounds.X, bounds.Y + this.borderWidth, this.borderWidth, bounds.Height - (2 * this.borderWidth));
			g.FillRectangle(this.borderBrush, rectangle1);
			rectangle1.X = bounds.Right - this.borderWidth;
			g.FillRectangle(this.borderBrush, rectangle1);
		}

 
		private int PaintColumns(Graphics g, Rectangle bounds, GridState dgs, Font font, bool alignToRight, int[] colsNameWidths, int[] colsDataWidths, int skippedCells)
		{
			Rectangle rectangle1 = bounds;
			GridColumnCollection collection1 = dgs.GridColumnStyles;
			int num1 = 0;
			for (int num2 = 0; num2 < collection1.Count; num2++)
			{
				if (num1 >= bounds.Width)
				{
					return num1;
				}
				if (((this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.ColumnName) || (this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.Both)) && (skippedCells >= this.horizOffset))
				{
					rectangle1.X = bounds.X + num1;
					rectangle1.Width = Math.Min(bounds.Width - num1, colsNameWidths[num2]);
					rectangle1.X = this.MirrorRect(bounds, rectangle1, alignToRight);
					string text1 = collection1[num2].HeaderText + ": ";
					this.PaintText(g, rectangle1, text1, font, false, alignToRight);
					num1 += rectangle1.Width;
				}
				if (num1 >= bounds.Width)
				{
					return num1;
				}
				if (skippedCells < this.horizOffset)
				{
					skippedCells++;
				}
				else
				{
					rectangle1.X = bounds.X + num1;
					rectangle1.Width = Math.Min(bounds.Width - num1, colsDataWidths[num2]);
					rectangle1.X = this.MirrorRect(bounds, rectangle1, alignToRight);
					BindManager bm = (BindManager) this.dataGrid.BindContext[dgs.DataSource, dgs.DataMember] ;
					//BindManager bm = (BindManager) this.dataGrid.GetBindManager(dgs.DataSource, dgs.DataMember) ;
					collection1[num2].Paint(g, rectangle1, (BindManager) bm, bm.Position, this.BackBrush, this.ForeBrush, alignToRight);
				
					//collection1[num2].Paint(g, rectangle1, (CurrencyManager) this.dataGrid.BindingContext[dgs.DataSource, dgs.DataMember], this.dataGrid.BindingContext[dgs.DataSource, dgs.DataMember].Position, this.BackBrush, this.ForeBrush, alignToRight);
					num1 += rectangle1.Width;
					g.DrawLine(new Pen(SystemColors.ControlDark), alignToRight ? rectangle1.X : rectangle1.Right, rectangle1.Y, alignToRight ? rectangle1.X : rectangle1.Right, rectangle1.Bottom);
					num1++;
					if (num2 < (collection1.Count - 1))
					{
						rectangle1.X = bounds.X + num1;
						rectangle1.Width = Math.Min(bounds.Width - num1, 3);
						rectangle1.X = this.MirrorRect(bounds, rectangle1, alignToRight);
						g.FillRectangle(this.BackBrush, rectangle1);
						num1 += 3;
					}
				}
			}
			return num1;
		}

 
		private void PaintDownButton(Graphics g, Rectangle bounds)
		{
			g.DrawLine(Pens.Black, bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
			g.DrawLine(Pens.White, bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
			g.DrawLine(Pens.White, bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
			g.DrawLine(Pens.Black, bounds.X, bounds.Y + bounds.Height, bounds.X, bounds.Y);
		}

		private void PaintLeftArrow(Graphics g, Rectangle bounds, bool alignToRight)
		{
			Bitmap bitmap2;
			Bitmap bitmap1 = this.GetLeftArrowBitmap();
			if (this.downLeftArrow)
			{
				this.PaintDownButton(g, bounds);
				this.layout.leftArrow.Inflate(-1, -1);
				lock ((bitmap2 = bitmap1))
				{
					this.PaintBitmap(g, bitmap1, bounds);
				}
				this.layout.leftArrow.Inflate(1, 1);
			}
			else
			{
				lock ((bitmap2 = bitmap1))
				{
					this.PaintBitmap(g, bitmap1, bounds);
				}
			}
		}

		private void PaintParentRows(Graphics g, Rectangle bounds, bool alignToRight)
		{
			int num1 = 0;
			int num2 = this.ColsCount();
			int[] numArray1 = new int[num2];
			int[] numArray2 = new int[num2];
			if ((this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.TableName) || (this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.Both))
			{
				num1 = this.GetTableBoxWidth(g, this.dataGrid.Font);
			}
			for (int num3 = 0; num3 < num2; num3++)
			{
				if ((this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.ColumnName) || (this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.Both))
				{
					numArray1[num3] = this.GetColBoxWidth(g, this.dataGrid.Font, num3);
				}
				else
				{
					numArray1[num3] = 0;
				}
				numArray2[num3] = this.GetColDataBoxWidth(g, num3);
			}
			this.ComputeLayout(bounds, num1, numArray1, numArray2);
			if (!this.layout.leftArrow.IsEmpty)
			{
				g.FillRectangle(this.BackBrush, this.layout.leftArrow);
				this.PaintLeftArrow(g, this.layout.leftArrow, alignToRight);
			}
			Rectangle rectangle1 = this.layout.data;
			for (int num4 = 0; num4 < this.parentsCount; num4++)
			{
				rectangle1.Height = (int) this.rowHeights[num4];
				if (rectangle1.Y > bounds.Bottom)
				{
					break;
				}
				int num5 = this.PaintRow(g, rectangle1, num4, this.dataGrid.Font, alignToRight, num1, numArray1, numArray2);
				if (num4 == (this.parentsCount - 1))
				{
					break;
				}
				g.DrawLine(this.gridLinePen, rectangle1.X, rectangle1.Bottom, rectangle1.X + num5, rectangle1.Bottom);
				rectangle1.Y += rectangle1.Height;
			}
			if (!this.layout.rightArrow.IsEmpty)
			{
				g.FillRectangle(this.BackBrush, this.layout.rightArrow);
				this.PaintRightArrow(g, this.layout.rightArrow, alignToRight);
			}
		}

 
		private void PaintRightArrow(Graphics g, Rectangle bounds, bool alignToRight)
		{
			Bitmap bitmap2;
			Bitmap bitmap1 = this.GetRightArrowBitmap();
			if (this.downRightArrow)
			{
				this.PaintDownButton(g, bounds);
				this.layout.rightArrow.Inflate(-1, -1);
				lock ((bitmap2 = bitmap1))
				{
					this.PaintBitmap(g, bitmap1, bounds);
				}
				this.layout.rightArrow.Inflate(1, 1);
			}
			else
			{
				lock ((bitmap2 = bitmap1))
				{
					this.PaintBitmap(g, bitmap1, bounds);
				}
			}
		}

		private int PaintRow(Graphics g, Rectangle bounds, int row, Font font, bool alignToRight, int tableNameBoxWidth, int[] colsNameWidths, int[] colsDataWidths)
		{
			GridState state1 = (GridState) this.parents[row];
			Rectangle rectangle1 = bounds;
			Rectangle rectangle2 = bounds;
			rectangle1.Height = (int) this.rowHeights[row];
			rectangle2.Height = (int) this.rowHeights[row];
			int num1 = 0;
			int num2 = 0;
			if ((this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.TableName) || (this.dataGrid.ParentRowsLabelStyle == GridParentRowsLabelStyle.Both))
			{
				if (num2 < this.horizOffset)
				{
					num2++;
				}
				else
				{
					rectangle1.Width = Math.Min(rectangle1.Width, tableNameBoxWidth);
					rectangle1.X = this.MirrorRect(bounds, rectangle1, alignToRight);
					string text1 = state1.ListManager.GetListName() + ": ";
					this.PaintText(g, rectangle1, text1, font, true, alignToRight);
					num1 += rectangle1.Width;
				}
			}
			if (num1 >= bounds.Width)
			{
				return bounds.Width;
			}
			rectangle2.Width -= num1;
			rectangle2.X += alignToRight ? 0 : num1;
			num1 += this.PaintColumns(g, rectangle2, state1, font, alignToRight, colsNameWidths, colsDataWidths, num2);
			if (num1 < bounds.Width)
			{
				rectangle1.X = bounds.X + num1;
				rectangle1.Width = bounds.Width - num1;
				rectangle1.X = this.MirrorRect(bounds, rectangle1, alignToRight);
				g.FillRectangle(this.BackBrush, rectangle1);
			}
			return num1;
		}

		private int PaintText(Graphics g, Rectangle textBounds, string text, Font font, bool bold, bool alignToRight)
		{
			Font font1 = font;
			if (bold)
			{
				try
				{
					font1 = new Font(font, FontStyle.Bold);
				}
				catch
				{
				}
			}
			else
			{
				font1 = font;
			}
			g.FillRectangle(this.BackBrush, textBounds);
			StringFormat format1 = new StringFormat();
			if (alignToRight)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				format1.Alignment = StringAlignment.Far;
			}
			format1.FormatFlags |= StringFormatFlags.NoWrap;
			textBounds.Offset(0, 2);
			textBounds.Height -= 2;
			g.DrawString(text, font1, this.ForeBrush, (RectangleF) textBounds, format1);
			format1.Dispose();
			return textBounds.Width;
		}

		internal GridState PopTop()
		{
			if (this.parentsCount < 1)
			{
				return null;
			}
			this.SetParentCount(this.parentsCount - 1);
			GridState state1 = (GridState) this.parents[this.parentsCount];
			state1.RemoveChangeNotification();
			this.parents.RemoveAt(this.parentsCount);
			return state1;
		}

 
		private void ResetMouseInfo()
		{
			this.downLeftArrow = false;
			this.downRightArrow = false;
		}

		private void RightArrowClick(int cellCount)
		{
			if (this.horizOffset < (cellCount - 1))
			{
				this.ResetMouseInfo();
				this.horizOffset++;
				this.Invalidate();
			}
			else
			{
				this.ResetMouseInfo();
				this.InvalidateRect(this.layout.rightArrow);
			}
		}

		internal void SetParentCount(int count)
		{
			this.parentsCount = count;
			//this.dataGrid.Caption.BackButtonVisible = (this.parentsCount > 0) && this.dataGrid.AllowNavigation;
		}

 
		private int TotalWidth(int tableNameBoxWidth, int[] colsNameWidths, int[] colsDataWidths)
		{
			int num1 = 0;
			num1 += tableNameBoxWidth;
			for (int num2 = 0; num2 < colsNameWidths.Length; num2++)
			{
				num1 += colsNameWidths[num2];
				num1 += colsDataWidths[num2];
			}
			return (num1 + (3 * (colsNameWidths.Length - 1)));
		}


		#endregion

		#region Properties
		public AccessibleObject AccessibleObject
		{
			get
			{
				if (this.accessibleObject == null)
				{
					this.accessibleObject = new GridParentRows.GridParentRowsAccessibleObject(this);
				}
				return this.accessibleObject;
			}
		}
 
		internal SolidBrush BackBrush
		{
			get
			{
				return this.backBrush;
			}
			set
			{
				if (value != this.backBrush)
				{
					this.CheckNull(value, "BackBrush");
					this.backBrush = value;
					this.Invalidate();
				}
			}
		}
 
		internal Color BackColor
		{
			get
			{
				return this.backBrush.Color;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException("GridEmptyColor", "Parent Rows BackColor" );
				}
				if (value != this.backBrush.Color)
				{
					this.backBrush = new SolidBrush(value);
					this.Invalidate();
				}
			}
		}
 
		internal Brush BorderBrush
		{
			get
			{
				return this.borderBrush;
			}
			set
			{
				if (value != this.borderBrush)
				{
					this.borderBrush = value;
					this.Invalidate();
				}
			}
		}
		internal SolidBrush ForeBrush
		{
			get
			{
				return this.foreBrush;
			}
			set
			{
				if (value != this.foreBrush)
				{
					this.CheckNull(value, "BackBrush");
					this.foreBrush = value;
					this.Invalidate();
				}
			}
		}
		internal Color ForeColor
		{
			get
			{
				return this.foreBrush.Color;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException("GridEmptyColor",  "Parent Rows ForeColor" );
				}
				if (value != this.foreBrush.Color)
				{
					this.foreBrush = new SolidBrush(value);
					this.Invalidate();
				}
			}
		}
 
		internal int Height
		{
			get
			{
				return this.totalHeight;
			}
		}
		internal bool Visible
		{
			get
			{
				return this.dataGrid.ParentRowsVisible;
			}
			set
			{
				this.dataGrid.ParentRowsVisible = value;
			}
		}

		#endregion

		#region Fields
		private AccessibleObject accessibleObject;
		private SolidBrush backBrush;
		private Brush borderBrush;
		private int borderWidth;
		private ColorMap[] colorMap;
		private Grid dataGrid;
		private bool downLeftArrow;
		private bool downRightArrow;
		private SolidBrush foreBrush;
		private Pen gridLinePen;
		private int horizOffset;
		private Layout layout;
		private static Bitmap leftArrow;
		private ArrayList parents;
		private int parentsCount;
		private static Bitmap rightArrow;
		private ArrayList rowHeights;
		private int textRegionHeight;
		private int totalHeight;
		#endregion

		#region Nested Types
		[ComVisible(true)]
		internal protected class GridParentRowsAccessibleObject : AccessibleObject
		{
			// Methods
			public GridParentRowsAccessibleObject(GridParentRows owner)
			{
				this.owner = null;
				this.owner = owner;
			}

			public override void DoDefaultAction()
			{
				this.owner.dataGrid.NavigateBack();
			}

			public override AccessibleObject GetChild(int index)
			{
				return ((GridState) this.owner.parents[index]).ParentRowAccessibleObject;
			}

 
			public override int GetChildCount()
			{
				return this.owner.parentsCount;
			}

			public override AccessibleObject GetFocused()
			{
				return null;
			}

			internal AccessibleObject GetNext(AccessibleObject child)
			{
				int num1 = this.GetChildCount();
				bool flag1 = false;
				for (int num2 = 0; num2 < num1; num2++)
				{
					if (flag1)
					{
						return this.GetChild(num2);
					}
					if (this.GetChild(num2) == child)
					{
						flag1 = true;
					}
				}
				return null;
			}

			internal AccessibleObject GetPrev(AccessibleObject child)
			{
				int num1 = this.GetChildCount();
				bool flag1 = false;
				for (int num2 = num1 - 1; num2 >= 0; num2--)
				{
					if (flag1)
					{
						return this.GetChild(num2);
					}
					if (this.GetChild(num2) == child)
					{
						flag1 = true;
					}
				}
				return null;
			}

			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
					case AccessibleNavigation.Up:
					case AccessibleNavigation.Left:
					case AccessibleNavigation.Previous:
						return this.Parent.GetChild(this.GetChildCount() - 1);

					case AccessibleNavigation.Down:
					case AccessibleNavigation.Right:
					case AccessibleNavigation.Next:
						return this.Parent.GetChild(1);

					case AccessibleNavigation.FirstChild:
						if (this.GetChildCount() > 0)
						{
							return this.GetChild(0);
						}
						break;

					case AccessibleNavigation.LastChild:
						if (this.GetChildCount() > 0)
						{
							return this.GetChild(this.GetChildCount() - 1);
						}
						break;
				}
				return null;
			}

 
			public override void Select(AccessibleSelection flags)
			{
			}


			// Properties
			public override Rectangle Bounds
			{
				get
				{
					return this.owner.dataGrid.RectangleToScreen(this.owner.dataGrid.ParentRowsBounds);
				}
			}
			private Grid Grid
			{
				get
				{
					return this.owner.dataGrid;
				}
			}
 
			public override string DefaultAction
			{
				get
				{
					return "AccDGNavigateBack";
				}
			}
			public override string Name
			{
				get
				{
					return "AccDGParentRows";
				}
			}
 
			internal GridParentRows Owner
			{
				get
				{
					return this.owner;
				}
			}
 
			public override AccessibleObject Parent
			{
				get
				{
					return this.owner.dataGrid.AccessibilityObject;
				}
			}
 
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.List;
				}
			}
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates states1 = AccessibleStates.ReadOnly;
					if (this.owner.parentsCount == 0)
					{
						states1 |= AccessibleStates.Invisible;
					}
					if (this.owner.dataGrid.ParentRowsVisible)
					{
						return (states1 | AccessibleStates.Expanded);
					}
					return (states1 | AccessibleStates.Collapsed);
				}
			}
			public override string Value
			{
				get
				{
					return null;
				}
			}

			// Fields
			private GridParentRows owner;
		}

		private class Layout
		{
			// Methods
			public Layout()
			{
				this.data = Rectangle.Empty;
				this.leftArrow = Rectangle.Empty;
				this.rightArrow = Rectangle.Empty;
			}
 
			public override string ToString()
			{
				StringBuilder builder1 = new StringBuilder(200);
				builder1.Append("ParentRows Layout: \n");
				builder1.Append("data = ");
				builder1.Append(this.data.ToString());
				builder1.Append("\n leftArrow = ");
				builder1.Append(this.leftArrow.ToString());
				builder1.Append("\n rightArrow = ");
				builder1.Append(this.rightArrow.ToString());
				builder1.Append("\n");
				return builder1.ToString();
			}

 

			// Fields
			public Rectangle data;
			public Rectangle leftArrow;
			public Rectangle rightArrow;
		}
		#endregion
	}
 

}
