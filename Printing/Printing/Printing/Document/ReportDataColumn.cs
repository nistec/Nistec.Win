using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Nistec.Printing.Sections;
using Nistec.Printing.Drawing;

namespace Nistec.Printing
{

    /// <summary>
    /// ReportDataColumn provides the necessary information for
    /// formatting data for a column of a report.
    /// </summary>
//    /// <remarks>
//    /// <para>
//    /// For every column to be presented within a section of data,
//    /// a new ReportDataColumn object is instantiated and added
//    /// to the <see cref="Nistec.Printing.ReportSection"/>.
//    /// At a minimum, each column describes a
//    /// source field (column) from the DataSource,
//    /// and a maximum width for the column.
//    /// </para>
//    /// <para>
//    /// The ReportDataColumn can also be setup with its own unique
//    /// <see cref="Nistec.Printing.TextStyle"/> for header and normal rows.
//    /// Therefore, each column's data could be formatted differently.
//    /// Use the TextStyle to setup font, color, and alignment (left, center, right)
//    /// </para>
//    /// <para>
//    /// Things to add: background color, borders...
//    /// </para>
//    /// </remarks>
    public class ReportDataColumn
    {
        string field;
        float width;
        float maxWidth;
        string formatExpression;
        string headerRowText;
		int columnOrdinal;
        
        TextStyle headerTextStyle;
        TextStyle detailRowTextStyle;

        bool sizeWidthToHeader;
        bool sizeWidthToContents;

        Pen rightPen;
		private System.Windows.Forms.HorizontalAlignment alignment=HorizontalAlignment.Left;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">The name of the field in the DataSource to be used in this column</param>
        /// <param name="maxWidth">The maxWidth for this column.</param>
        public ReportDataColumn (string field,int colOrdinal, float maxWidth)
        {
            Field = field;
            MaximumWidth = maxWidth;
            HeaderText = field;
			columnOrdinal=colOrdinal;
        }

        /// <summary>
        /// An event fired for every format of a cell in every row of the report.
        /// This event is fired at least twice for every cell printed,
        /// so make sure the code is "lean".
        /// </summary>
        public event FormatColumnHandler FormatColumn;


        #region "Public properties"
        /// <summary>
        /// The source field from the DataSource.  That is, this is
        /// the DataTable column name. 
        /// </summary>
        public string Field
        {
            get { return this.field; }
            set {this.field = value; }
        }

		/// <summary>
		/// The ColumnIndex in DataSource. 
		/// </summary>
		public int ColumnOrdinal
		{
			get { return this.columnOrdinal; }
			set {this.columnOrdinal= value; }
		}

        /// <summary>
        /// The width for the column.
        /// </summary>
        public float Width
        {
            get 
            { 
                return this.width; 
            }
            set 
            {
                this.width = value; 
            }
        }

        /// <summary>
        /// The maximum width for the column.
        /// </summary>
        public float MaximumWidth
        {
            get { return this.maxWidth; }
            set {this.maxWidth = value; }
        }

        /// <summary>
        /// The <see cref="System.String"/> to display in the header row.
        /// The default value is the field name.
        /// </summary>
        public string HeaderText
        {
            get { return this.headerRowText; }
            set { this.headerRowText = value; }
        }

        string prefix = "{0:";
        string suffix = "}";
        /// <summary>
        /// A format expression to use for output formatting.
        /// <seealso cref="System.String.Format"/>
        /// </summary>
        public string FormatExpression
        {
            get 
            { 
                return this.formatExpression; 
            }
            set 
            { 
                if (value.StartsWith(prefix))
                {
                    Debug.WriteLine ("Deprecated use of FormatExpression.  In the future, omit the {0:}");
                    this.formatExpression = value;
                }
                else
                {
                    this.formatExpression = prefix + value + suffix;
                }
            }
        }

        /// <summary>
        /// The text style to use for text
        /// Defaults to TableHeader
        /// </summary>
        public TextStyle HeaderTextStyle
        {
            get
            {
                if (this.headerTextStyle == null)
                {
                    return TextStyle.TableHeader;
                }
                else
                {
                    return this.headerTextStyle;
                }
            }
            set { this.headerTextStyle = value; }
        }

        /// <summary>
        /// The text style to use for text
        /// Defaults to normal.  
        /// </summary>
        public TextStyle DetailRowTextStyle
        {
            get
            {
                if (this.detailRowTextStyle == null)
                {
                    return TextStyle.TableRow;
                }
                else
                {
                    return this.detailRowTextStyle;
                }
            }
            set { this.detailRowTextStyle = value; }
        }

        /// <summary>
        /// The column's width is sized to the header text
        /// The Width property, if non zero,
        /// will set the maximum width. 
        /// </summary>
        public bool SizeWidthToHeader
        {
            get { return this.sizeWidthToHeader; }
            set { this.sizeWidthToHeader = value; }
        }

        /// <summary>
        /// The column's width is size to the contents of
        /// all cells (not including header text).  This adds
        /// a bit of processing time for long tables.
        /// The Width property, if non zero,
        /// will set the maximum width.
        /// </summary>
        public bool SizeWidthToContents
        {
            get { return this.sizeWidthToContents; }
            set { this.sizeWidthToContents = value; }
        }
              
        /// <summary>
        /// Gets or sets the pen used to draw the line for columns
        /// </summary>
        public Pen RightPen
        {
            get { return this.rightPen; }
            set { this.rightPen = value; }
        }

        /// <summary>
        /// Gets the width of the pen, or 0 if null
        /// </summary>
        float rightPenWidth
        {
            get
            {
                float width = 0;
                if (RightPen != null)
                {
                    width = RightPen.Width;
                }
                return width;
            }
        }

        #endregion



        /// <summary>
        /// This is the section to which this column belongs.
        /// Therefore, each column can only belong to one Section
        /// at a time.
        /// It is set by "add column" of the ReportSection class.
        /// </summary>
        internal protected ReportSectionData parentSection;


        /// <summary>
        /// Set the size of the column - must be called prior to printing
        /// or else width is never set.
        /// </summary>
        /// <param name="g">Graphics object</param>
        /// <param name="dataSource">DataSource for the cell contents</param>
        /// <returns>The width used for the column</returns>
        internal protected virtual float SizeColumn (
            Graphics g, DataView dataSource)
        {
            // HACK: I don't like where maxHeight is coming from here.
            float maxHeight = this.parentSection.MaxHeaderRowHeight;
            float headerWidth = 0f;
            if (SizeWidthToHeader)
            {
                // size the header
                SizeF headerSize = SizePaint( 
                    g, this.HeaderText, 0, 0, this.MaximumWidth, maxHeight, true, 
                    this.HeaderTextStyle);
                headerWidth = headerSize.Width;
            }

            maxHeight = this.parentSection.MaxDetailRowHeight;
            float contentWidth = 0f;
            if (SizeWidthToContents)
            {
                foreach (DataRowView drv in dataSource)
                {
                    object obj = drv[this.field];
                    string text = GetString(obj);

                    // size the header
                    SizeF cellSize = SizePaint( 
                        g, text, 0, 0, this.MaximumWidth, maxHeight, true, 
                        this.DetailRowTextStyle);
                    // use the new width if it is bigger
                    contentWidth = Math.Max(contentWidth, cellSize.Width);
                }
            }

            // find the maximum used width (if nonzero), and use it 
            // if it's less than the max
            float maxUsedWidth = Math.Max(headerWidth, contentWidth);
            if (maxUsedWidth > 0 && maxUsedWidth < this.maxWidth)
            {
                this.width = maxUsedWidth;
            }
            else
            {
                this.width = this.maxWidth;
            }
            return this.width;
        }

        /// <summary>
        /// The string used to represent null values in a cell.
        /// </summary>
        public string NullValueString = "<NULL>";


        /// <summary>
        /// Gets a string for the information in this column based on
        /// the object passed.
        /// </summary>
        /// <param name="obj">An object whose value is to be returned as a string.</param>
        /// <returns>A string to print in the report</returns>
        internal protected virtual string GetString(object obj)
        {
            FormatColumnEventArgs e = new FormatColumnEventArgs();
            e.OriginalValue = obj;
            if (obj != null)
            {
                if (this.FormatExpression == null || this.FormatExpression == String.Empty)
                {
                    e.StringValue = obj.ToString();
                }
                else
                {
                    e.StringValue = String.Format(this.FormatExpression, obj);
                }
            }
            else
            {
                e.StringValue = this.NullValueString;
            }

            if (this.FormatColumn != null)
            {
                this.FormatColumn (this, e);
            }
            return e.StringValue;
        } // GetString


        /// <summary>
        /// Paints or measures the object passed in according 
        /// to the formatting rules of this column.
        /// </summary>
        /// <param name="g">the graphics to paint the value onto</param>
        /// <param name="text">the text to paint</param>
        /// <param name="x">the x coordinate to start the paint</param>
        /// <param name="y">the y coordinate to start the paint</param>
        /// <param name="width">the width of the cell</param>
        /// <param name="height">The max height of this cell (when in sizeOnly
        /// or else the actual height to use</param>
        /// <param name="sizeOnly">only calculate the sizes</param>
        /// <param name="textStyle">The text style to use, header or detail</param>
        /// <returns>A sizeF representing the measured size of the string + margins</returns>
        internal protected virtual SizeF SizePaint
            ( 
            Graphics g,
            string text,
            float x, float y,
            float width,
            float height,
            bool sizeOnly,
            TextStyle textStyle
            )
        {
            SizeF stringSize = new SizeF(width, height);
            Font font = textStyle.GetFont();
            StringFormat stringFormat = GetStringFormat();

            // take off the margins, border line is counted as margins
            // for simplicity
            float sideMargins = textStyle.MarginNear + textStyle.MarginFar + rightPenWidth;
            float topBottomMargins = textStyle.MarginTop + textStyle.MarginBottom;

            // Setup the legal drawing rectangle
            RectangleF textLayout = new RectangleF (
                x + textStyle.MarginNear, y + textStyle.MarginTop,
                width - sideMargins, height - topBottomMargins); 

            if (sizeOnly)
            {
                // Find the height of the actual string to be drawn
                stringSize = g.MeasureString(text, font, textLayout.Size, stringFormat);
                stringSize.Width += sideMargins;
                stringSize.Height += topBottomMargins;
                // Don't go bigger than maxHeight
                stringSize.Height = Math.Min(stringSize.Height, height);
            } 
            else
            {
                // draw background & text
                if (textStyle.BackgroundBrush != null)
                {
                    RectangleF cellLayout = new RectangleF (
                        x, y, width, height);
                    g.FillRectangle (textStyle.BackgroundBrush, cellLayout);
                }
                DrawRightLine (g, RightPen, x + width, y, height);
                g.DrawString(text, font, textStyle.Brush, textLayout, stringFormat);
            }
			stringFormat.Dispose();
            return stringSize;
        }


		public virtual StringFormat GetStringFormat()
		{
			// HACK: these constants should be set elsewhere?
			StringFormat stringFormat = new StringFormat();
			stringFormat.FormatFlags = StringFormatFlags.LineLimit |
				StringFormatFlags.NoClip;

			// TODO: Trimming can be a public property like alignment
			stringFormat.Trimming = StringTrimming.Word;
			//stringFormat.Trimming = StringTrimming.EllipsisWord;
			if(alignment==HorizontalAlignment.Left)
				stringFormat.Alignment = StringAlignment.Near;
			else if(alignment==HorizontalAlignment.Right)
				stringFormat.Alignment = StringAlignment.Far;
			else if(alignment==HorizontalAlignment.Center)
				stringFormat.Alignment = StringAlignment.Center;
			return (stringFormat);
		}

	
		public System.Windows.Forms.HorizontalAlignment ColumnAlignment
		{
			get{return this.alignment;}
			set{this.alignment=value;}
		}


        void DrawRightLine (Graphics g, Pen pen, float x, float y, float height)
        {
            if (pen != null)
            {
                // Draw to the inside of the rectangle
                x -= this.rightPenWidth / 2;
                g.DrawLine (pen, x, y, x, y + height);
            }
        }



    } // class ColumnInfo



}
