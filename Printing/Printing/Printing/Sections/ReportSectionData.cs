
using System;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Nistec.Printing.Sections;
using Nistec.Printing.Data;
using Nistec.Printing.Drawing;
using Nistec.Win;

namespace Nistec.Printing.Sections
{
	/// <summary>
	/// A ReportSection that represents the printing of data
	/// from a DataView
	/// </summary>
//	/// <remarks>
//	/// <para>
//    /// A ReportSectionData contains a DataView and
//    /// zero or more ReportDataColumns that that correspond 
//    /// to data to be printed from the DataView.
//    /// </para>
//    /// <para>
//    /// A header row can be printed at the top of the table
//    /// (and every new page) based on the HeaderText for
//    /// each ReportDataColumn object.
//    /// </para>
//    /// <para>
//    /// Margins and HorizontalAlignment are specified the same
//    /// as for any other ReportSectin. <b>VerticalAlignment is
//    /// not implemented.</b>
//    /// </para>
//    /// </remarks>

	public class ReportSectionData : Nistec.Printing.Sections.ReportSection
	{
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSource">A DataView to use as the source of data</param>
		public ReportSectionData(DataView dataSource)
		{
            this.DataSource = dataSource;
            this.borderPens = new BorderPens();
		}


        const int HeaderRowNumber = -1;
        /// <summary>
        /// The current/next row to be printed.
        /// </summary>
        protected int rowIndex;

        /// <summary>
        /// The number of data rows that will fit on the next call to print.
        /// </summary>
        protected int dataRowsFit;

        /// <summary>
        /// Heights of each row that will be printed next...
        /// </summary>
        ArrayList rowHeights;


        bool showHeaderRow;
        DataView dataSource;
		int columnOrdinal;
        float minDetailRowHeight = 0F;
        float minHeaderRowHeight = 0F;
        float maxDetailRowHeight = 8F;
        float maxHeaderRowHeight = 8F;

        /// <summary>
        /// Size of header
        /// </summary>
        SizeF headerSize;
        bool headerSizeInit;


        /************
         * Pens used for lines
         */
        BorderPens borderPens;
        Pen innerPenHeaderBottom;
        Pen innerPenRow;

        #region "Properties and fields"

        /// <summary>
        /// A header row should be printed at the top of
        /// the table on every page.
        /// If false, the headerow is only printed on the first page.
        /// </summary>
        public bool ShowHeaderRow
        {
            get { return this.showHeaderRow; }
            set { this.showHeaderRow = value; }
        }


        /// <summary>
        /// The DataView which represents the data to be shown
        /// in this section.
        /// </summary>
        public DataView DataSource
        {
            get { return this.dataSource; }
            set { this.dataSource = value; }
        }

		/// <summary>
		/// Gets the number of Column index in the DataView 
		/// Equals -1 if there is no Column Index
		/// </summary>
		public int ColumnOrdinal
		{
			get
			{
				if (DataSource == null)
				{
					return -1;
				}
				else
				{
					return columnOrdinal;
				}
			}
			set{columnOrdinal=value;}
		}

//		private Nistec.Data.ExportColumnType[] columnsOrdinal;
//
//		public Nistec.Data.ExportColumnType[] ColumnsOrdinal
//		{
//			get{return columnsOrdinal;}
//			set{columnsOrdinal=value;}
//		}

        public AdoField[] GetExportColumnType()
		{
            AdoField[] cols = new AdoField[columns.Count];
			for(int i=0;i< this.columns.Count;i++)
			{
                cols[i] = new AdoField(((ReportDataColumn)columns[i]).Field, ((ReportDataColumn)columns[i]).HeaderText, ((ReportDataColumn)columns[i]).ColumnOrdinal);
			}
			return cols;
		}

		public void ExportDocument()
		{
			if(dataSource==null)
			{
				MsgBox.ShowWarning("Invalid data source");
			}
            AdoField[] cols = GetExportColumnType();
			if(cols!=null)
				AdoExport.Export(dataSource.Table,false,cols);
			else
				AdoExport.Export(dataSource.Table,false);
		}

        /// <summary>
        /// Gets the number of detail rows (rows in the DataView object)
        /// Equals 0 if there is no DataSource
        /// </summary>
        public int TotalRows
        {
            get
            {
                if (DataSource == null)
                {
                    return 0;
                }
                else
                {
                    return DataSource.Count;
                }
            }
        }

        
        /// <summary>
        /// Used to determine the maximum size of a detail row
        /// any larger and it will be clipped at this size, possibly losing information
        /// </summary>
        public float MaxDetailRowHeight
        {
            get { return this.maxDetailRowHeight; }
            set { this.maxDetailRowHeight = value; }
        }

        /// <summary>
        /// Used to determine the minimum size of a detail row
        /// Even if the row is smaller, it will add empty space before
        /// the next row is printed
        /// </summary>
        public float MinDetailRowHeight
        {
            get { return this.minDetailRowHeight; }
            set { this.minDetailRowHeight = value; }
        }

        /// <summary>
        /// Used to determine the maximum size of a header row
        /// any larger and it will be clipped at this size, possibly losing information
        /// </summary>
        public float MaxHeaderRowHeight
        {
            get { return this.maxHeaderRowHeight; }
            set { this.maxHeaderRowHeight = value; }
        }

        /// <summary>
        /// Used to determine the minimum size of a header row
        /// Even if the row is smaller, it will add empty space before
        /// the next row is printed
        /// </summary>
        public float MinHeaderRowHeight
        {
            get { return this.minHeaderRowHeight; }
            set { this.minHeaderRowHeight = value; }
        }


        /// <summary>
        /// Gets or sets the pen used to draw the line at the top of the table
        /// </summary>
        public Pen OuterPenTop
        {
            get { return borderPens.Top; }
            set { borderPens.Top = value; }
        }
        /// <summary>
        /// Gets or sets the pen used to draw the line on the right side of the table
        /// </summary>
        public Pen OuterPenRight
        {
            get { return borderPens.Right; }
            set { borderPens.Right = value; }
        }
        /// <summary>
        /// Gets or sets the pen used to draw the line at the bottom of the table
        /// </summary>
        public Pen OuterPenBottom
        {
            get { return borderPens.Bottom; }
            set { borderPens.Bottom = value; }
        }
        /// <summary>
        /// Gets or sets the pen used to draw the line on the left side of the table
        /// </summary>
        public Pen OuterPenLeft
        {
            get { return borderPens.Left; }
            set { borderPens.Left = value; }
        }

        /// <summary>
        /// Sets the pens used on all outer sides (top, right, bottom, left)
        /// </summary>
        public Pen OuterPens
        {
            set
            {
                this.borderPens.Top = value;
                this.borderPens.Right = value;
                this.borderPens.Bottom = value;
                this.borderPens.Left = value;
            }
        }

        /// <summary>
        /// Gets or sets the pen used to draw the line under the header row
        /// </summary>
        public Pen InnerPenHeaderBottom
        {
            get { return innerPenHeaderBottom; }
            set { innerPenHeaderBottom = value; }
        }
        /// <summary>
        /// Gets or sets the pen used to draw the line under all other rows
        /// </summary>
        public Pen InnerPenRow
        {
            get { return innerPenRow; }
            set { innerPenRow = value; }
        }


        #endregion

        #region "Columns"
        /// <summary>
        /// The collection of columns used to format this ReportSectionData
        /// </summary>
        ArrayList columns = new ArrayList();

        /// <summary>
        /// Add a column object to the list of columns
        /// Each column object should be a new instance of ReportDataColumn
        /// You can pass a single instance into AddColumn more than once, but
        /// you will just get the same column printed out multiple times.
        /// </summary>
        /// <param name="rc">The column info to add</param>
        /// <returns>The number of columns</returns>
        public virtual int AddColumn(ReportDataColumn rc)
        {
            // Add a reference to this (as the parent).
            rc.parentSection = this;
            return this.columns.Add(rc);
        }

        /// <summary>
        /// Removes a column from the section
        /// </summary>
        /// <param name="index">Index of the column to remove</param>
        public virtual void RemoveColumn(int index)
        {
            this.columns.RemoveAt(index);
        }

        /// <summary>
        /// Gets the column at the specified index
        /// </summary>
        /// <param name="index">Index of a column</param>
        /// <returns>A ReportDataColumn object</returns>
        public virtual ReportDataColumn GetColumn(int index)
        {
            return (ReportDataColumn) this.columns[index];
        }

        /// <summary>
        /// The number of columns in this section.
        /// </summary>
        public int ColumnCount
        {
            get { return this.columns.Count; }
        }

        /// <summary>
        /// Clears all the columns from this section.
        /// Printing a section with 0 columns is just fine,
        /// a little weird, but technically fine.
        /// </summary>
        public virtual void ClearColumns()
        {
            this.columns.Clear();
        }
        #endregion


        #region "Overrides from ReportSection"

        /// <summary>
        /// Setup for printing
        /// </summary>
        /// <param name="g">Graphics object</param>
        protected override void DoBeginPrint(Graphics g)
        {
            if (this.ShowHeaderRow)
            {
                this.rowIndex = 0;
            }
            else
            {
                this.rowIndex = HeaderRowNumber;
            }
            // setup column widths
            foreach (ReportDataColumn rCol in this.columns)
            {
                rCol.SizeColumn(g, this.DataSource);
            }

            // get rid of the pen on the last column
            ReportDataColumn lastCol = (ReportDataColumn) 
                this.columns[this.columns.Count - 1];
            lastCol.RightPen = null;
        }


        /// <summary>
        /// Called to calculate the size that this section requires on
        /// the next call to Print.  This method will be called exactly once
        /// prior to each call to Print.  It must update the values Size and
        /// Continued of the ReportSection base class.
        /// </summary>
        /// <param name="printDocument">The parent McPrintDocument that is printing.</param>
        /// <param name="g">Graphics object to print on.</param>
        /// <param name="bounds">Bounds of the area to print within.</param>
        /// <returns>SectionSizeValues of requiredSize, fits, and continues.</returns>
        protected override SectionSizeValues DoCalcSize (
            McPrintDocument printDocument,
            Graphics g,
            Bounds bounds
            )
        {
//            Debug.WriteLine("ReportSectionData DoCalcSize");
//            Debug.WriteLine("   Bounds: " + bounds.ToString());
//            Debug.WriteLine("   RowIndex: " + this.rowIndex + ", Total Rows: " + this.TotalRows);

            // Default values
            SectionSizeValues retvals = new SectionSizeValues();

            Bounds insideBorder = this.borderPens.GetInnerBounds (bounds);
            CalcHeaderSize (g, insideBorder);
            Bounds tableBounds = GetTableBounds (insideBorder);

            PointF originalPosition = tableBounds.Position;
            if (SizePrintHeader (g, ref tableBounds, true))
            {
                this.dataRowsFit = FindDataRowsFit (g, ref tableBounds);
                if (this.TotalRows == 0)
                {
                    retvals.Fits = true;
                }
                else if (this.dataRowsFit > 0)
                {
                    retvals.Fits = true;
                    if (this.rowIndex + this.dataRowsFit < this.TotalRows)
                    {
                        retvals.Continued = true;
                    }
                }
            }

            retvals.RequiredSize = this.borderPens.AddBorderSize (
                new SizeF (this.headerSize.Width,
                tableBounds.Position.Y - originalPosition.Y));
            
            return retvals;
        }



        /// <summary>
        /// Called to actually print this section.  
        /// The DoCalcSize method will be called exactly once prior to each
        /// call of DoPrint.
        /// It should obey the value or Size and Continued as set by
        /// DoCalcSize().
        /// </summary>
        /// <param name="printDocument">The parent McPrintDocument that is printing.</param>
        /// <param name="g">Graphics object to print on.</param>
        /// <param name="bounds">Bounds of the area to print within.</param>
        protected override void DoPrint (
            McPrintDocument printDocument,
            Graphics g,
            Bounds bounds
            )
        {
//            Debug.WriteLine("ReportSectionData DoPrint");
//            Debug.WriteLine("   Bounds: " + bounds.ToString());
//            Debug.WriteLine("   RowIndex: " + this.rowIndex + ", Total Rows: " + this.TotalRows);

            Bounds tableBounds = GetTableBounds (bounds, this.RequiredSize);
            this.borderPens.DrawBorder (g, tableBounds);
            tableBounds = borderPens.GetInnerBounds (tableBounds);
            
            SizePrintHeader (g, ref tableBounds, false);
            PrintRows (g, ref tableBounds);
        }

        #endregion

        #region "Private sizing and printing functions"

        /// <summary>
        /// Calculates the size of the header and put it in headerSize
        /// </summary>
        SizeF CalcHeaderSize (Graphics g, Bounds bounds)
        {
            if (!headerSizeInit)
            {
                headerSize = SizePrintRow(g, HeaderRowNumber, 
                    bounds.Position.X, bounds.Position.Y, 
                    bounds.Width, this.MaxHeaderRowHeight, true);
                headerSizeInit = true;
            }
            return headerSize;
        }

        /// <summary>
        /// Gets the bounds used for the actual table, that is
        /// position is the top left corner of the table (after
        /// applying margins and alignments).  The width is based on
        /// this.headerSize and the border size.  The height is the full 
        /// height of the bounds.
        /// </summary>
        /// <param name="bounds">Maximum bounds allowed for the table</param>
        /// <returns>The bounds that the table is printed into.</returns>
        Bounds GetTableBounds (Bounds bounds)
        {
            // Find the correct x to center by getting a rectangle that holds the header row.
            SizeF size = this.borderPens.AddBorderSize (this.headerSize);
            RectangleF rect = bounds.GetRectangleF (size, 
                this.HorizontalAlignment, this.VerticalAlignment);
            
            // Create a new bounds with no margins at the correct location 
            // (centered left to right).
            return new Bounds (rect.Left, bounds.Position.Y, 
                rect.Right, bounds.Limit.Y);
        }

        /// <summary>
        /// Gets the bounds used for the actual table, that is
        /// position is the top left corner of the table (after
        /// applying margins and alignments).
        /// The width is based on
        /// this.headerSize and the border size.  The height is the full 
        /// height of the bounds.
        /// </summary>
        /// <param name="bounds">Maximum bounds allowed for the table</param>
        /// <param name="size">Size required for the table</param>
        /// <returns>The bounds that the table is printed into.</returns>
        Bounds GetTableBounds (Bounds bounds, SizeF size)
        {
            // size = this.borderPens.AddBorderSize (size);
            // Find the correct x and y to center
            RectangleF rect = bounds.GetRectangleF (size, 
                this.HorizontalAlignment, this.VerticalAlignment);
            
            // Create a new bounds with no margins at the correct location 
            // (centered left to right and top to bottom).
            return new Bounds (rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        /// <summary>
        /// Sizes or prints the header, if it is enabled
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="sizeOnly"></param>
        /// <returns>True if it fits, false if it doesn't</returns>
        bool SizePrintHeader (Graphics g, ref Bounds bounds, bool sizeOnly)
        {
            bool headerFits = true;
            if (this.ShowHeaderRow)
            {
                // if it fits, print it
                if (bounds.SizeFits(this.headerSize))
                {
                    if (!sizeOnly)
                    {
                        SizePrintRow (g, HeaderRowNumber, bounds.Position.X, bounds.Position.Y,
                            this.headerSize.Width, this.headerSize.Height, false);
                    }
                    bounds.Position.Y += this.headerSize.Height;
                }
                else
                {
                    headerFits = false;
                }
            }
            return headerFits;
        }

        /// <summary>
        /// Sizes rows and figures out how many will fit in the given bounds.
        /// </summary>
        /// <param name="g">Graphics object used for measuring</param>
        /// <param name="bounds">Maximum bounds allowed for table</param>
        /// <returns>The number of data rows that will fit</returns>
        int FindDataRowsFit (Graphics g, ref Bounds bounds)
        {
            int rowsThatFit = 0;
            int index = this.rowIndex;
            rowHeights = new ArrayList();
            // now start printing rows as long as they fit on the page
            while (index < this.TotalRows)
            {
                SizeF rowSize = SizePrintRow (g, index, bounds.Position.X, 
                    bounds.Position.Y,  bounds.Width, this.MaxDetailRowHeight, true);
                if (bounds.SizeFits(rowSize))
                {
                    Debug.Assert (rowHeights.Count == rowsThatFit, "rowHeights.Count is not equal to index");
                    rowHeights.Add (rowSize.Height);
                    bounds.Position.Y += rowSize.Height;
                    index++;
                    rowsThatFit++;
                }
                else
                {
                    break;
                }
            }
            return rowsThatFit;
        }

 
        /// <summary>
        /// Prints rows of a table based on current rowIndex and dataRowsFit variables
        /// Also, bounds is incremented as each row is printed.
        /// </summary>
        /// <param name="g">Graphics object used for printing</param>
        /// <param name="bounds">Bounds allowed for table</param>
        /// <returns>True if at least one row fits</returns>
        void PrintRows (Graphics g, ref Bounds bounds)
        {
            // print all the rows that we already decided fit on the page...
            for (int rowCount = 0; rowCount < this.dataRowsFit; rowCount++, this.rowIndex++ )
            {
                float height = (float) rowHeights[rowCount];
                SizePrintRow (g, this.rowIndex, bounds.Position.X, 
                    bounds.Position.Y, bounds.Width, height, false);
                bounds.Position.Y += height;

                Debug.Assert (bounds.Position.Y - bounds.Limit.Y < 0.0001f, 
                    "Row doesn't really fit, but we thought it did");
            }
        }


		/// <summary>
		/// Finds the size of a row, given the row number and the
		/// maximum height/width for the row.
		/// </summary>
		/// <param name="g">Graphics object to use for sizing</param>
		/// <param name="rowIndex">Index of the row within the DataView.
		/// Use the constant HeaderRowNumber to size the header row.</param>
		/// <param name="x">X position for the origin of the row.</param>
		/// <param name="y">Y position for the origin of the row.</param>
		/// <param name="maxWidth">The maximum width the row may consume.</param>
		/// <param name="maxHeight">The maximum height the row may consume.</param>
		/// <param name="sizeOnly">Only size, don't actually print</param>
		/// <returns>A SizeF object containg the size required for the row.</returns>
        SizeF SizePrintRow (
            Graphics g, int rowIndex,
            float x, float y, float maxWidth, float maxHeight, bool sizeOnly)
        {
            bool isHeader = (rowIndex == HeaderRowNumber);
            float rowWidth = 0;
            float rowHeight = 0;
            float xPos = x;

            //calc row hight and col width
            float w = 0f;
            float prc = 1;
            float colw = 0f;
            foreach (ReportDataColumn col in this.columns)
            {
                w += col.Width;
            }
            if (w > maxWidth)
            {
                prc =1-( (w - maxWidth) / w);
                rowHeight = 0.2f / prc;
            }

            // get height and width of row by iterating through every column
            // adding all widths to get the rowWidth
            foreach (ReportDataColumn col in this.columns)
            {
                colw = col.Width * prc;
                // HACK: Small number comparison
                if ((rowWidth + colw) - maxWidth > 0.001f)
                {
                    // it won't fit
                    break;
                }
                string text = GetCellString(rowIndex, col);
                TextStyle textStyle = GetTextStyle (rowIndex, col);
                SizeF size = col.SizePaint(g, text, xPos, y, colw, maxHeight, sizeOnly, textStyle);
                rowHeight = Math.Max(rowHeight, GetValidHeight(size.Height, isHeader));
                rowWidth += colw;
                xPos += colw;
            }
            if (rowIndex != this.TotalRows - 1) // don't print a line for the last row...
            {
                rowHeight += RowLine (g, x, y + rowHeight, rowWidth, isHeader, sizeOnly);
            }
            return new SizeF (rowWidth, rowHeight);
        }

        /// <summary>
        /// Prints or sizes a line (under a row)
        /// </summary>
        /// <param name="g">Graphics object to draw on</param>
        /// <param name="x">X position of the line</param>
        /// <param name="y">Y position of the line</param>
        /// <param name="length">Length of the line</param>
        /// <param name="isHeader">The row is a header row</param>
        /// <param name="sizeOnly">SizeOnly - don't print if true</param>
        /// <returns>The width (height) of the line</returns>
        float RowLine (Graphics g, float x, float y, float length, bool isHeader, bool sizeOnly)
        {
            float height = 0;
            Pen pen;
            if (isHeader)
            {
                pen = this.innerPenHeaderBottom;
            }
            else
            {
                pen = this.innerPenRow;
            }

            if (pen != null)
            {
                if (!sizeOnly)
                {
                    y -= pen.Width / 2;
                    g.DrawLine (pen, x, y, x + length, y);
                }
                height = pen.Width;
            }
            return height;
        }

        #endregion

        #region "Protected virtual getters"

        /// <summary>
        /// Gets the text style for a cell based on the column
        /// and whether it is a header row or not
        /// </summary>
        /// <param name="rowIndex">Row number for the cell</param>
        /// <param name="col">ReportDataColumn for the cell</param>
        /// <returns>The TextStyle to use for text printed on given row of the given columns</returns>
        protected virtual TextStyle GetTextStyle (int rowIndex, ReportDataColumn col)
        {
            bool isHeader = (rowIndex == HeaderRowNumber);
            if (isHeader)
            {
                return col.HeaderTextStyle;
            }
            else
            {
                return col.DetailRowTextStyle;
            }
        }

        /// <summary>
        /// Gets the string to be printed for the row and column
        /// </summary>
        /// <param name="rowIndex">Row number for the string
        /// HeaderRowNumber will return the HeaderRowText</param>
        /// <param name="col">The column that the string goes into</param>
        /// <returns>a string to be printed</returns>
        protected virtual string GetCellString (int rowIndex, ReportDataColumn col)
        {
            bool isHeader = (rowIndex == HeaderRowNumber);
            if (isHeader)
            {
                return col.HeaderText;
            }
            else
            {
                Object obj = this.DataSource[rowIndex][col.Field];
                return col.GetString(obj);
            }
        }

        /// <summary>
        /// Checks that a height of a row is in the valid range.
        /// </summary>
        /// <param name="height">The desired height to use</param>
        /// <param name="isHeader">True if this is for a header row</param>
        /// <returns>height in the range min to max</returns>
        protected virtual float GetValidHeight (float height, bool isHeader)
        {
            float min, max;
            if (isHeader)
            {
                min = this.MinHeaderRowHeight;
                max = this.MaxHeaderRowHeight;
            }
            else
            {
                min = this.MinDetailRowHeight;
                max = this.MaxDetailRowHeight;
            }

            if (height < min)
            {
                return min;
            }
            else if (height > max)
            {
                return max;
            }
            else
            {
                return height;
            }
        }

        #endregion

	}
}
