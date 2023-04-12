using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using Nistec.Printing.Sections;
using Nistec.Printing.Drawing;
using Nistec.Win;
//=using Nistec.Data;

namespace Nistec.Printing
{
	/// <summary>
	/// This class assists with the building of a McPrintDocument
	/// </summary>
//	/// <remarks>
//	/// <para>
//	/// ReportBuilder assists with the building of a report.
//	/// When it is constructed, you must provide the 
//	/// <see cref="Nistec.Printing.McPrintDocument"/> to be built.
//	/// </para>
//    /// <para>
//    /// Some summaries of important objects:
//    /// </para>
//    /// <para>
//    /// In page header / footer text, the following strings have special meanings.
//    /// <list type="table">
//    /// <listheader><term>String</term><description>Description</description></listheader>
//    /// <item><term>%p</term><description>Page Number</description></item>
//    /// </list>
//    /// </para>
//	/// </remarks>
	public partial class ReportBuilder
	{

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="printDocument">The document to be built</param>
        public ReportBuilder (McPrintDocument printDocument)
        {
            this.currentDocument = printDocument;
            this.containers = new Stack();
            this.formatStrings = new Hashtable();
            AddFormatExpression(typeof(DateTime), DefaultDateTimeFormatString);
        }
        public ReportBuilder()
        {
            this.currentDocument = new McPrintDocument();
            this.containers = new Stack();
            this.formatStrings = new Hashtable();
            AddFormatExpression(typeof(DateTime), DefaultDateTimeFormatString);
        }

        const string DefaultDateTimeFormatString = "d";

        Stack containers;

        readonly Hashtable formatStrings;
        McPrintDocument currentDocument;
        ReportSection currentSection;
        ReportDataColumn currentColumn;
        float maxDetailRowHeight = 8F;
        float minDetailRowHeight = 0F;
        float maxHeaderRowHeight = 8F;
        float minHeaderRowHeight = 0F;
		bool documentMode;
        Pen defaultTablePen;
        float horizLineMargins = 0.1f;


        #region "Settings, Properties, etc"


        /// <summary>
        /// The current document being built
        /// </summary>
        public McPrintDocument CurrentDocument
        {
            get { return this.currentDocument; }
        }

        /// <summary>
        /// Gets the current SectionContainer
        /// </summary>
        public SectionContainer CurrentContainer
        {
            get 
            {
                if (this.containers.Count == 0) 
                {
                    return null;
                }
                return this.containers.Peek() as SectionContainer; 
            }
        }

        /// <summary>
        /// The last section added throught AddSection()
        /// </summary>
        public ReportSection CurrentSection
        {
            get { return this.currentSection; }
        }

        /// <summary>
        /// The last section added throught AddSection()
        /// </summary>
        public ReportDataColumn CurrentColumn
        {
            get { return this.currentColumn; }
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
        /// Adds a default format expression for a specific Type.
        /// </summary>
        /// <param name="type">The type which should use the given format string.</param>
        /// <param name="formatString">A formatString to use for the given type. 
        /// See the Format method of <see cref="System.String"/> </param>
        /// <remarks>
        /// These default format expressions are copied into a column's
        /// FormatExpression when the column is created if the type matches.
        /// </remarks>
        /// <example>
        /// The following entry is added by default for DateTime objects.
        /// Unless a new string is added for that Type (or the default is
        /// removed) then all columns of DateTime type will be formated
        /// with the string "{0:d}"
        /// <code>
        /// string DefaultDateTimeFormatString = "{0:d}";
        /// AddFormatExpression(typeof(DateTime), DefaultDateTimeFormatString);
        /// </code>
        /// </example>
        public void AddFormatExpression (Type type, string formatString)
        {
            this.formatStrings[type] = formatString;
        }

        /// <summary>
        /// Removes the default format expression for a given type
        /// </summary>
        /// <param name="type">The Type whose's default format expression should be removed</param>
        public void ClearFormatExpression (Type type)
        {
            if (this.formatStrings.ContainsKey(type))
            {
                this.formatStrings.Remove(type);
            }
        }

        /// <summary>
        /// Gets or sets the default top and bottom margins used
        /// for horizontal lines
        /// </summary>
        public float HorizLineMargins
        {
            get { return this.horizLineMargins; }
            set { this.horizLineMargins = value; }
        }

        #endregion


        #region "Containers"

		/// <summary>
		/// This adds the provided SectionContainer to the McPrintDocument
		/// and makes it the default container for new sections added to the report.
		/// </summary>
		/// <param name="container">The SectionContainter to add</param>
		/// <returns>The SectionContainter started</returns>
        public SectionContainer StartContainer (
            SectionContainer container)
        {
            if (this.CurrentDocument.Body == null)
            {
                this.CurrentDocument.Body = container;
            }
            else if (this.CurrentContainer != null)
            {
                this.CurrentContainer.AddSection(container);
            }
            //container.sectionIndex = 0;
            this.containers.Push(container);
            return container;
        }

		/// <summary>
		/// Finish and close the last container opened
		/// </summary>
		/// <returns>The containter finished</returns>
        public SectionContainer FinishContainer ()
        {
            return this.containers.Pop() as SectionContainer;
        }

		/// <summary>
		/// Start a linear layout (that is, create a container
		/// from LinearSections, and add future sections to this
		/// container).
		/// </summary>
		/// <param name="direction">The Direction that the sub-sections
		/// of this linear section are laid out.  (Generally vertical)</param>
		/// <returns>The new LinearSections object started by this method</returns>
        public LinearSections StartLinearLayout (Direction direction)
        {
            LinearSections container = new LinearSections();
            container.Direction =direction;
            StartContainer(container);
            return container;
        }

		/// <summary>
		/// Finish and close the last container opened, hopefully it
		/// is a container started with StartLinearLayout 
		/// </summary>
		/// <returns>The containter closed (as a LinearSections object)</returns>
		public LinearSections FinishLinearLayout ()
        {
            return FinishContainer() as LinearSections;
        }

        /// <summary>
        /// Start a layered layout (that is, create a container
        /// from LayeredSections, and add future sections to this
        /// container) that expands to use the full width and 
        /// height of the parent section
        /// </summary>
        /// <returns>The new LayeredSections object started by this method</returns>
        public LayeredSections StartLayeredLayout ()
        {
            return StartLayeredLayout (true, true);
        }

        /// <summary>
		/// Start a layered layout (that is, create a container
		/// from LayeredSections, and add future sections to this
		/// container).
		/// </summary>
        /// <param name="useFullWidth">Indicates that this layered layout will expand
        /// to full width within the parent layout</param>
        /// <param name="useFullHeight">Indicates that this layered layout will expand
        /// to full height within the parent layout</param>
        /// <returns>The new LayeredSections object started by this method</returns>
        public LayeredSections StartLayeredLayout (bool useFullWidth, bool useFullHeight)
        {
            LayeredSections container = new LayeredSections(useFullWidth, useFullHeight);
            StartContainer(container);
            return container;
        }

		/// <summary>
		/// Finish and close the last container opened, hopefully it
		/// is a container started with StartLinearLayout 
		/// </summary>
		/// <returns>The containter closed (as a LinearSections object)</returns>
        public LayeredSections FinishLayeredLayout ()
        {
            return FinishContainer() as LayeredSections;
        }

		/// <summary>
		/// This is an unsupported function.  Avoid using it...
		/// Starts a linear horizontal layout within a parent vertical layout,
		/// which causes sections to first be layed out across a page as rows, then 
		/// down the page at the end of each row.
		/// </summary>
		/// <remarks>
		/// This DocumentLayout should be avoided.  The simple LinearSection
		/// with Vertical layout can handle most scenarios.  However, some more advanced
		/// setups may require this.
		/// </remarks>
		public void StartDocumentLayout()
		{
            Debug.WriteLine ("Avoid using the document layout - unsupported...");
			Debug.Assert(!this.documentMode, "Already in document layout.  May not nest these.");
            LinearRepeatableSections repSections = new LinearRepeatableSections (Direction.Vertical);
			this.StartContainer (repSections);
            this.StartLinearLayout(Direction.Horizontal);
			this.documentMode = true;
		}

		/// <summary>
		/// Finishes a document layout (really, it just finishes two
		/// LinearSections containers).
		/// </summary>
		public void FinishDocumentLayout()
		{
			Debug.Assert(this.documentMode, "Not in document layout.  This could present problems trying to FinishDocumentLayout()");
			this.FinishContainer();
			this.FinishContainer();
			this.documentMode = false;
		}


        /// <summary>
        /// Starts a layout of columns.
        /// </summary>
        /// <param name="columnWidth">The width of each column, in inches</param>
        /// <param name="spaceBetween">The distance between columns, in inches</param>
        /// <param name="lineDivider">Pen to use as a line divider</param>
        public void StartColumnLayout (float columnWidth, float spaceBetween,  Pen lineDivider)
        {
            LinearRepeatableSections colSections = 
                new LinearRepeatableSections(Direction.Horizontal);
            this.StartContainer (colSections);
            if (lineDivider != null)
            {
                SectionLine line = new SectionLine (Direction.Vertical, lineDivider);
                line.MarginLeft = (spaceBetween - lineDivider.Width) / 2;
                line.MarginRight = (spaceBetween - lineDivider.Width) / 2;
                colSections.Divider = line;
            }
            else
            {
                colSections.SkipAmount = spaceBetween;
            }
            
            LinearSections sections;
            sections = this.StartLinearLayout(Direction.Vertical);
            sections.MaxWidth = columnWidth;
            sections.UseFullWidth = true;
        }

        /// <summary>
        /// Start a layout of columns by specifying the number of columns.
        /// </summary>
        /// <param name="numberOfColumns">Number of columns to fit on the page.</param>
        /// <param name="spaceBetween">The distance between columns, in inches</param>
        /// <param name="lineDivider">A pen to use as a line divider</param>
        public void StartColumnLayout (int numberOfColumns, float spaceBetween, Pen lineDivider)
        {
            float width = this.CurrentDocument.DefaultPageSettings.Bounds.Width;
            width -= this.CurrentDocument.DefaultPageSettings.Margins.Left;
            width -= this.CurrentDocument.DefaultPageSettings.Margins.Right;
            width /= 100f;
            width -= (numberOfColumns - 1) * spaceBetween;
            Debug.Assert(width > 0, "Too many columns, too wide of margins, or some other problem.  Columns will not print.");
            width /= numberOfColumns;
            StartColumnLayout (width, spaceBetween, lineDivider);
        }

        /// <summary>
        /// Start a layout of columns by specifying the number of columns.
        /// </summary>
        /// <param name="numberOfColumns">Number of columns to fit on the page.</param>
        /// <param name="spaceBetween">The distance between columns, in inches</param>
        public void StartColumnLayout (int numberOfColumns, float spaceBetween)
        {
            StartColumnLayout (numberOfColumns, spaceBetween, null);
        }

        /// <summary>
        /// Ends the layout in columns.
        /// </summary>
        public void FinishColumnLayout()
        {
            this.FinishContainer(); // the vertical layout
            this.FinishContainer(); // the horizontal layout
        }

        #endregion


        #region "Basic Sections"

        /// <summary>
        /// Adds any ReportSection to the document.
        /// </summary>
        /// <param name="section">ReportSection to add</param>
        /// <returns>The ReportSection added</returns>
        public ReportSection AddSection (ReportSection section)
        {
            if (this.CurrentContainer == null)
            {
                throw new ReportBuilderException("No SectionContainer defined to add section: " + section.ToString());
            }
            this.CurrentContainer.AddSection(section);
            this.currentSection = section;
            if (this.documentMode)
            {
                section.VerticalAlignment = VerticalAlignment.Bottom;
            }
            return section;
        }

        /// <summary>
        /// Adds a section to the McPrintDocument
        /// consisting of a text field with a given TextStyle
        /// </summary>
        /// <param name="text">Text for this section</param>
        /// <param name="textStyle">Text style to use for this section</param>
        /// <returns>ReportSection just created</returns>
        public ReportSectionText AddTextSection (string text, TextStyle textStyle)
        {
            ReportSectionText section = new ReportSectionText(text, textStyle);
			if (this.documentMode)
			{
				section.SingleLineMode = true;
			}

            AddSection(section);
            return section;
        }

		/// <summary>
		/// Adds a section to the McPrintDocument
		/// consisting of a text field.
		/// </summary>
		/// <param name="text">Text for this section</param>
		/// <returns>ReportSection just created</returns>
		public ReportSectionText AddTextSection (string text)
		{
			return AddTextSection (text, TextStyle.Normal);
		}


        /// <summary>
        /// Adds a page break at this point in the report
        /// </summary>
        /// <returns>SectionBreak added</returns>
        public SectionBreak AddPageBreak ()
        {
            return (SectionBreak) AddSection (new SectionBreak ());
        }

        /// <summary>
        /// Adds a column break at this point in the report
        /// </summary>
        /// <returns>SectionBreak added</returns>
        public SectionBreak AddColumnBreak ()
        {
            return (SectionBreak) AddSection (new SectionBreak (false));
        }

        /// <summary>
        /// Adds a line break at this point in the report
        /// </summary>
        /// <returns>SectionBreak added</returns>
        public SectionBreak AddLineBreak ()
        {
            return (SectionBreak) AddSection (new SectionBreak (false));
        }


        /// <summary>
        /// Adds a horizontal line
        /// </summary>
        /// <param name="pen">Pen to use for the line</param>
        /// <returns>SectionLine added</returns>
        public SectionLine AddHorizontalLine (Pen pen)
        {
            SectionLine sectionLine = new SectionLine (Direction.Horizontal, pen);
            sectionLine.MarginTop = HorizLineMargins;
            sectionLine.MarginBottom = HorizLineMargins;
            AddSection (sectionLine);
            return sectionLine;
        }

        /// <summary>
        /// Adds a horizontal line using the document's NormalPen
        /// </summary>
        /// <returns>SectionLine added</returns>
        public SectionLine AddHorizontalLine ()
        {
            return AddHorizontalLine (CurrentDocument.NormalPen);
        }


        #endregion


        #region "Data Section & Columns"

        /// <summary>
        /// Gets or sets the default pen used for table grid lines
        /// </summary>
        public Pen DefaultTablePen
        {
            get { return this.defaultTablePen; }
            set { this.defaultTablePen = value; }
        }


        /// <summary>
        /// Adds a data section with no default columns.
        /// Use the AddColumn() method to add those.
        /// </summary>
        /// <param name="dataSource">DataView for the source of data</param>
        /// <param name="showHeaderRow">Indicates a header row should be printed</param>
        /// <returns>ReportSection just created</returns>
        public ReportSectionData AddDataSection (
            DataView dataSource, bool showHeaderRow
            )
        {
            ReportSectionData section = new ReportSectionData(dataSource);
            section.ShowHeaderRow = showHeaderRow;
            section.MaxDetailRowHeight = this.MaxDetailRowHeight;
            section.MinDetailRowHeight = this.MinDetailRowHeight;
            section.MaxHeaderRowHeight = this.MaxHeaderRowHeight;
            section.MinHeaderRowHeight = this.MinHeaderRowHeight;
            section.InnerPenHeaderBottom = this.DefaultTablePen;
            section.InnerPenRow = this.DefaultTablePen;
            section.OuterPens = this.DefaultTablePen;

            AddSection(section);
            return section;
        }

        /// <summary>
        /// Adds a data section with all columns.
        /// </summary>
        /// <param name="dataSource">DataView for the source of data</param>
        /// <param name="showHeaderRow">Indicates a header row should be printed</param>
        /// <returns>ReportSection just created</returns>
        /// <param name="maxColumnWidth">Maximum width of columns</param>
        /// <param name="sizeWidthToHeader">Auto-Size the columns based on the header</param>
        /// <param name="sizeWidthToContents">Auto-Size the columns based on the data contents</param>
        public ReportSectionData AddDataSection (
            DataView dataSource, bool showHeaderRow,
            float maxColumnWidth, bool sizeWidthToHeader, bool sizeWidthToContents
            )
        {
            ReportSectionData section = AddDataSection(dataSource, showHeaderRow);
            AddAllColumns (maxColumnWidth, sizeWidthToHeader, sizeWidthToContents);
            return section;
        }

        /// <summary>
        /// Adds all columns from the DataSource to the current section
        /// </summary>
        /// <param name="maxWidth">Maximum width for the columns</param>
        /// <param name="sizeWidthToHeader">Size the columns based on the header</param>
        /// <param name="sizeWidthToContents">Size the columns based on the data contents</param>
        public void AddAllColumns(float maxWidth,
            bool sizeWidthToHeader, bool sizeWidthToContents)
        {
            ReportSectionData dataSection = this.currentSection as ReportSectionData;
            if (dataSection != null)
            {
                foreach (DataColumn col in dataSection.DataSource.Table.Columns)
                {
                    AddColumn (col, col.ColumnName,maxWidth,
                        sizeWidthToHeader, sizeWidthToContents);
                    
                }
            }
        }

        /*****************
         * Function that actually creates the column
         */

        /// <summary>
        /// Adds a single column to the current section (last section added).
        /// </summary>
        /// <param name="col">Column in the data source</param>
        /// <param name="headerText">Text to display in the header row(s)</param>
        /// <param name="maxWidth">Maximum width of the column</param>
        /// <param name="sizeWidthToHeader">Size the column width based on the header</param>
        /// <param name="sizeWidthToContents">Size the column width based on the data contents</param>
        /// <param name="horizontalAlignment">Specifies the horizontal alignment of the
        /// contents of this column</param>
        /// <returns>ReportDataColumn just added</returns>
        public ReportDataColumn AddColumn (DataColumn col, string headerText,
            float maxWidth, bool sizeWidthToHeader, bool sizeWidthToContents,
            HorizontalAlignment horizontalAlignment)
        {
            ReportSectionData dataSection = this.currentSection as ReportSectionData;
            this.currentColumn = null;
            if (dataSection != null)
            {
                this.currentColumn = new ReportDataColumn(col.ColumnName,col.Ordinal, maxWidth);
                this.currentColumn.HeaderText = headerText;
                this.currentColumn.SizeWidthToHeader = sizeWidthToHeader;
                this.currentColumn.SizeWidthToContents = sizeWidthToContents;

                // check for default format string and add it
                if (this.formatStrings.ContainsKey(col.DataType))
                {
                    this.currentColumn.FormatExpression = (string) this.formatStrings[col.DataType];
                }
                this.currentColumn.RightPen = this.DefaultTablePen;

                switch (horizontalAlignment)
                {
                    case HorizontalAlignment.Center:
                        this.currentColumn.HeaderTextStyle = TextStyle.TableHeaderCentered;
                        this.currentColumn.DetailRowTextStyle = TextStyle.TableRowCentered;
                        break;
                    case HorizontalAlignment.Right:
                        this.currentColumn.HeaderTextStyle = TextStyle.TableHeaderRight;
                        this.currentColumn.DetailRowTextStyle = TextStyle.TableRowRight;
                        break;
                }

                dataSection.AddColumn(this.currentColumn);
            }
            return this.currentColumn;
        }

        /********************
         * Overloads
         */

        /// <summary>
        /// Adds a single column to the current section (last section added).
        /// </summary>
        /// <param name="columnName">Column name in the data source</param>
        /// <param name="headerText">Text to display in the header row(s)</param>
        /// <param name="maxWidth">Maximum width of the column</param>
        /// <param name="sizeWidthToHeader">Size the column width based on the header</param>
        /// <param name="sizeWidthToContents">Size the column width based on the data contents</param>
        /// <param name="horizontalAlignment">Specifies the horizontal alignment of the
        /// contents of this column</param>
        /// <returns>ReportDataColumn just added</returns>
        public ReportDataColumn AddColumn (string columnName, string headerText,
            float maxWidth, bool sizeWidthToHeader, bool sizeWidthToContents,
            HorizontalAlignment horizontalAlignment)
        {
            ReportSectionData dataSection = this.currentSection as ReportSectionData;
            this.currentColumn = null;
            if (dataSection != null)
            {
                DataColumn col = dataSection.DataSource.Table.Columns[columnName];
                if (col == null)
                {
                    throw new ReportBuilderException ("There is no column named '" + columnName +
                        "' in table '" + dataSection.DataSource.Table.TableName + "'");
                }
                return AddColumn (col, headerText, maxWidth, 
                    sizeWidthToHeader, sizeWidthToContents, horizontalAlignment);
            }
            return null;
        }

        /// <summary>
        /// Adds a single column to the current section (last section added).
        /// </summary>
        /// <param name="col">Column in the data source</param>
        /// <param name="headerText">Text to display in the header row(s)</param>
        /// <param name="maxWidth">Maximum width of the column</param>
        /// <param name="sizeWidthToHeader">Size the column width based on the header</param>
        /// <param name="sizeWidthToContents">Size the column width based on the data contents</param>
        /// <returns>ReportDataColumn just added</returns>
        public ReportDataColumn AddColumn (DataColumn col, string headerText,
            float maxWidth, bool sizeWidthToHeader, bool sizeWidthToContents)
        {
            return AddColumn (col, headerText, maxWidth, sizeWidthToHeader, 
                sizeWidthToContents, HorizontalAlignment.Left);
        }

        /// <summary>
        /// Adds a single column to the current section (last section added).
        /// </summary>
        /// <param name="col">Column in the data source</param>
        /// <param name="headerText">Text to display in the header row(s)</param>
        /// <param name="width">Maximum width of the column</param>
        /// <returns>ReportDataColumn just added</returns>
        public ReportDataColumn AddColumn (DataColumn col, string headerText,
            float width)
        {
            return AddColumn (col, headerText, width, false, false,
                HorizontalAlignment.Left);
        }

        /// <summary>
        /// Adds a single column to the current section (last section added).
        /// </summary>
        /// <param name="columnName">Column name in the data source</param>
        /// <param name="headerText">Text to display in the header row(s)</param>
        /// <param name="width">Maximum width of the column</param>
        /// <param name="sizeWidthToHeader">Size the column width based on the header</param>
        /// <param name="sizeWidthToContents">Size the column width based on the data contents</param>
        /// <returns>ReportDataColumn just added</returns>
        public ReportDataColumn AddColumn (string columnName, string headerText,
            float width, bool sizeWidthToHeader, bool sizeWidthToContents)
        {
            return AddColumn (columnName, headerText, width, sizeWidthToHeader, sizeWidthToContents,
                HorizontalAlignment.Left);
        }

  
        /// <summary>
        /// Adds a single column to the current section (last section added).
        /// </summary>
        /// <param name="columnName">Column name in the data source</param>
        /// <param name="headerText">Text to display in the header row(s)</param>
        /// <param name="width">Width of the column</param>
        /// <returns>ReportDataColumn just added</returns>
        public ReportDataColumn AddColumn (string columnName, string headerText,
            float width)
        {
            return AddColumn (columnName, headerText, width, false, false,
                HorizontalAlignment.Left);
        }


        #endregion


        #region "Header and Footer sections"

        //public const string Default = null;

        /// <summary>
        /// Gets a LayeredSections container for the document's PageHeader.
        /// If McPrintDocument's PageHeader is a valid LayeredSections
        /// object, then using this property returns that object.
        /// If McPrintDocument's PageHeader is null or any other
        /// object, then using this property creates a new LayeredSection
        /// and replaces McPrintDocument's PageHeader with the new section.
        /// </summary>
        public LayeredSections PageHeader
        {
            get
            {
                LayeredSections section = this.CurrentDocument.PageHeader as LayeredSections;
                if (section == null)
                {
                    section = new LayeredSections (true, false);
                    this.CurrentDocument.PageHeader = section;
                }
                return section;
            }
        }

        /// <summary>
        /// Gets a LayeredSections container for the document's PageFooter.
        /// If McPrintDocument's PageFooter is a valid LayeredSections
        /// object, then using this property returns that object.
        /// If McPrintDocument's PageFooter is null or any other
        /// object, then using this property creates a new LayeredSection
        /// and replaces McPrintDocument's PageFooter with the new section.
        /// </summary>
        public LayeredSections PageFooter
        {
            get
            {
                LayeredSections section = this.CurrentDocument.PageFooter as LayeredSections;
                if (section == null)
                {
                    section = new LayeredSections (true, false);
                    section.VerticalAlignment = VerticalAlignment.Bottom;
                    this.CurrentDocument.PageFooter = section;
                }
                return section;
            }
        }

        /// <summary>
        /// Gets a RepeatableTextSection with the given parameters.
        /// </summary>
        /// <param name="firstPageText">A string to use for the first page</param>
        /// <param name="evenPageText">A string to use for even pages</param>
        /// <param name="oddPageText">A string to use for odd pages</param>
        /// <param name="textStyle">The <see cref="Nistec.Printing.TextStyle"/> for the text.</param>
        /// <param name="hAlign">The <see cref="Nistec.Printing.HorizontalAlignment"/>
        /// for the text.</param>
        /// <returns>A RepeatableTextSection</returns>
        /// <remarks>
        /// <para>
        /// Specifing null for the firstPageText will result in oddPageText
        /// being used instead.  The blank string "" will suppress printing
        /// of a header.
        /// </para>
        /// This method is often used with the PageHeader and PageFooter
        /// properties as follows:
        /// <code>
        ///    builder.PageHeader.AddSection(builder.GetRepeatable(
        ///        "Text for first page",
        ///        "Text for odd pages",
        ///        "Text for even pages",
        ///        TextStyle.PageHeader,
        ///        StringAlignment.Center));
        /// </code>
        /// But it can be used anywhere you need to get a RepeateableTextSection.
        /// </remarks>
        public RepeatableTextSection GetRepeatable (string firstPageText, 
            string evenPageText, string oddPageText, 
            TextStyle textStyle, HorizontalAlignment hAlign)
        {
            RepeatableTextSection section = new RepeatableTextSection (
                oddPageText, textStyle);
            
            section.TextFirstPage = firstPageText;
            section.TextEvenPage = evenPageText;
            section.TextOddPage = oddPageText; 
            section.UseFullWidth = true;
            section.HorizontalAlignment = hAlign;
            return section;
        }


        /// <summary>
        /// Adds a page header to the report document with three sections of text
        /// and optionally a separate first page text
        /// </summary>
        /// <param name="leftText">
        /// Text to be displayed on the left side of the header
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerText">
        /// Text to be displayed in the center of the header
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightText">
        /// Text to be displayed on the right side of the header
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        public void AddPageHeader (
            string leftText,
            string centerText,
            string rightText
            )
        {
            AddPageHeader(leftText, centerText, rightText,
                leftText, centerText, rightText);
        }

        /// <summary>
        /// Adds a page header to the report document with three sections of text
        /// and optionally a separate first page text
        /// </summary>
        /// <param name="leftTextFirstPage">
        /// Text to be displayed on the left side of the header on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerTextFirstPage">
        /// Text to be displayed in the center of the header on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightTextFirstPage">
        /// Text to be displayed on the right side of the header on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="leftText">
        /// Text to be displayed on the left side of the header
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerText">
        /// Text to be displayed in the center of the header
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightText">
        /// Text to be displayed on the right side of the header
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        public void AddPageHeader (
            string leftTextFirstPage,
            string centerTextFirstPage,
            string rightTextFirstPage,
            string leftText,
            string centerText,
            string rightText
            )
        {
            AddPageHeader(
                leftTextFirstPage, centerTextFirstPage, rightTextFirstPage,
                leftText, centerText, rightText,
                leftText, centerText, rightText);
        }

        /// <summary>
        /// Adds a page header to the report document with three sections of text
        /// and optionally a separate first page text
        /// </summary>
        /// <param name="leftTextFirstPage">
        /// Text to be displayed on the left side of the header on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerTextFirstPage">
        /// Text to be displayed in the center of the header on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightTextFirstPage">
        /// Text to be displayed on the right side of the header on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="leftTextEvenPages">
        /// Text to be displayed on the left side of the header on even pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerTextEvenPages">
        /// Text to be displayed in the center of the header on even pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightTextEvenPages">
        /// Text to be displayed on the right side of the header on even pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="leftTextOddPages">
        /// Text to be displayed on the left side of the header on odd pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerTextOddPages">
        /// Text to be displayed in the center of the header on odd pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightTextOddPages">
        /// Text to be displayed on the right side of the header on odd pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        public void AddPageHeader (
            string leftTextFirstPage,
            string centerTextFirstPage,
            string rightTextFirstPage,
            string leftTextEvenPages,
            string centerTextEvenPages,
            string rightTextEvenPages,
            string leftTextOddPages,
            string centerTextOddPages,
            string rightTextOddPages
            )
        {
            AddPageHeader (leftTextFirstPage, leftTextEvenPages, leftTextOddPages, 
                 HorizontalAlignment.Left);
            AddPageHeader (centerTextFirstPage, centerTextEvenPages, centerTextOddPages, 
                 HorizontalAlignment.Center);
            AddPageHeader (rightTextFirstPage, rightTextEvenPages, rightTextOddPages, 
                 HorizontalAlignment.Right);
        }


        /// <summary>
        /// Adds a header to the report document with one section of text.
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="hAlign">Alignment of the text within the header</param>
        public void AddPageHeader (
            string text,
            HorizontalAlignment hAlign
            )
        {
            PageHeader.AddSection (GetRepeatable(text, text, text, 
                TextStyle.PageHeader, hAlign));
        }

        /// <summary>
        /// Adds a header to the report document with one section of text.
        /// </summary>
        /// <param name="textFirstPage">Text to be displayed on the first page.</param>
        /// <param name="textEvenPages">Text to be displayed on even pages.</param>
        /// <param name="textOddPages">Text to be dsiplayed on odd pages.</param>
        /// <param name="hAlign">Alignment of the text within the header</param>
        public void AddPageHeader (
            string textFirstPage,
            string textEvenPages,
            string textOddPages,
            HorizontalAlignment hAlign
            )
        {
            PageHeader.AddSection (GetRepeatable(textFirstPage, textEvenPages, textOddPages, 
                 TextStyle.PageHeader, hAlign));
        }



        /// <summary>
        /// Adds a page footer to the report document with three sections of text
        /// and optionally a separate first page text
        /// </summary>
        /// <param name="leftText">
        /// Text to be displayed on the left side of the footer
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerText">
        /// Text to be displayed in the center of the footer
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightText">
        /// Text to be displayed on the right side of the footer
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        public void AddPageFooter (
            string leftText,
            string centerText,
            string rightText
            )
        {
            AddPageFooter(
                leftText, centerText, rightText,
                leftText, centerText, rightText,
                leftText, centerText, rightText);
        }

        /// <summary>
        /// Adds a page footer to the report document with three sections of text
        /// and optionally a separate first page text
        /// </summary>
        /// <param name="leftTextFirstPage">
        /// Text to be displayed on the left side of the footer on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerTextFirstPage">
        /// Text to be displayed in the center of the footer on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightTextFirstPage">
        /// Text to be displayed on the right side of the footer on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="leftText">
        /// Text to be displayed on the left side of the footer
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerText">
        /// Text to be displayed in the center of the footer
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightText">
        /// Text to be displayed on the right side of the footer
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        public void AddPageFooter (
            string leftTextFirstPage,
            string centerTextFirstPage,
            string rightTextFirstPage,
            string leftText,
            string centerText,
            string rightText
            )
        {
            AddPageFooter(
                leftTextFirstPage, centerTextFirstPage, rightTextFirstPage,
                leftText, centerText, rightText, 
                leftText, centerText, rightText);
        }

        /// <summary>
        /// Adds a page footer to the report document with three sections of text
        /// and optionally a separate first page text
        /// </summary>
        /// <param name="leftTextFirstPage">
        /// Text to be displayed on the left side of the footer on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerTextFirstPage">
        /// Text to be displayed in the center of the footer on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightTextFirstPage">
        /// Text to be displayed on the right side of the footer on the first page.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="leftTextEvenPages">
        /// Text to be displayed on the left side of the footer on even pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerTextEvenPages">
        /// Text to be displayed in the center of the footer on even pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightTextEvenPages">
        /// Text to be displayed on the right side of the footer on even pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="leftTextOddPages">
        /// Text to be displayed on the left side of the footer on odd pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="centerTextOddPages">
        /// Text to be displayed in the center of the footer on odd pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        /// <param name="rightTextOddPages">
        /// Text to be displayed on the right side of the footer on odd pages.
        /// Specifying String.Empty will leave the section blank.
        /// </param>
        public void AddPageFooter (
            string leftTextFirstPage,
            string centerTextFirstPage,
            string rightTextFirstPage,
            string leftTextEvenPages,
            string centerTextEvenPages,
            string rightTextEvenPages,
            string leftTextOddPages,
            string centerTextOddPages,
            string rightTextOddPages
            )
        {
            AddPageFooter (leftTextFirstPage, leftTextEvenPages, leftTextOddPages, 
                HorizontalAlignment.Left);
            AddPageFooter (centerTextFirstPage, centerTextEvenPages, centerTextOddPages, 
                HorizontalAlignment.Center);
            AddPageFooter (rightTextFirstPage, rightTextEvenPages, rightTextOddPages, 
                HorizontalAlignment.Right);
        }


        /// <summary>
        /// Adds a footer to the report document with one section of text.
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="hAlign">Alignment of the text within the footer</param>
        public void AddPageFooter (
            string text,
            HorizontalAlignment hAlign
            )
        {
            PageFooter.AddSection (GetRepeatable(text, text, text,
                TextStyle.PageFooter, hAlign));
        }

        /// <summary>
        /// Adds a footer to the report document with one section of text.
        /// </summary>
        /// <param name="textFirstPage">Text to be displayed on the first page.</param>
        /// <param name="textEvenPages">Text to by displayed on even pages</param>
        /// <param name="textOddPages">Text to by displayed on odd pages</param>
        /// <param name="hAlign">Alignment of the text within the footer</param>
        public void AddPageFooter (
            string textFirstPage,
            string textEvenPages,
            string textOddPages,
            HorizontalAlignment hAlign
            )
        {
            PageFooter.AddSection (GetRepeatable(textFirstPage, textEvenPages, textOddPages, 
                 TextStyle.PageFooter, hAlign));
        }


        /// <summary>
        /// Adds a horizontal line to the bottom of the page header,
        /// to separate the header from the body.
        /// </summary>
        /// <returns>The SectionLine added.  Update the returned
        /// value to set pen, length, magins, etc.</returns>
        public SectionLine AddPageHeaderLine ()
        {
            SectionLine line = new SectionLine (
                Direction.Horizontal, CurrentDocument.NormalPen);
            line.VerticalAlignment = VerticalAlignment.Bottom;
            line.MarginBottom = 0.1f;
            PageHeader.AddSection (line);
            return line;
        }

        /// <summary>
        /// Adds a horizontal line to the top of the page footer,
        /// to separate the footer from the body.
        /// </summary>
        /// <returns>The SectionLine added.  Update the returned
        /// value to set pen, length, magins, etc.</returns>
        public SectionLine AddPageFooterLine ()
        {
            SectionLine line = new SectionLine (
                Direction.Horizontal, CurrentDocument.NormalPen);
            line.VerticalAlignment = VerticalAlignment.Top;
            line.MarginTop = 0.1f;
            PageFooter.AddSection (line);
            return line;
        }

        #endregion


        //public static HorizontalAlignment ConvertRtlToAlignment(System.Windows.Forms.RightToLeft rtl)
        //{
        //    return rtl == RightToLeft.Yes ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        //}

        //public static void PrintDataView(DataView dv, string header)
        //{
        //    PrintDataView(dv, header, true);
        //}

        //public static void PrintDataView(DataView dv, string header, bool preview)
        //{
        //    McPrintDocument rpt = new McPrintDocument();
        //    ReportBuilder rb = new ReportBuilder(rpt);
        //    rb.CreateDocument(dv);
        //    rb.CreateHeaderAndFooter(header);

        //    if (preview)
        //    {
        //        McPrintPreviewDialog dlg = new McPrintPreviewDialog();
        //        dlg.Document = rpt;
        //        dlg.Show();
        //    }
        //    else
        //    {
        //        rpt.Print();
        //    }
        //}

        //public void CreateHeaderAndFooter(string header)
        //{
        //    if (!string.IsNullOrEmpty(header))
        //    {
        //        this.AddPageHeader(header, HorizontalAlignment.Center);
        //    }
        //    this.AddPageFooter("Page %p", "Page %p", "Page %p", HorizontalAlignment.Center);

        //}
        //public virtual void CreateDocument(DataView dv)
        //{
        //    CreateDocument(dv, HorizontalAlignment.Left, 80, null);
        //}

        //public virtual void CreateDocument(DataView dv, HorizontalAlignment alignment)
        //{
        //    CreateDocument(dv,alignment, 80,null);
        //}

        //public void CreateDocument(DataView dv, HorizontalAlignment alignment, float maxColumnWidth, McColumn[] cols)
        //{

        //    //ResetStyles();

        //    // Following line sets up the pen used for lins for tables
        //    this.DefaultTablePen = this.currentDocument.ThinPen;

        //    TextStyle.Heading1.StringAlignment = alignment == HorizontalAlignment.Left ? StringAlignment.Near : alignment == HorizontalAlignment.Center ? StringAlignment.Center : StringAlignment.Far;

        //    //SetDefaultStyle();

        //    this.currentDocument.SectionData = this.AddDataSection(dv, true);

        //    this.CurrentSection.HorizontalAlignment = alignment;// HorizontalAlignment.Left;
        //    this.CurrentSection.UseFullWidth = true;
        //    this.CurrentSection.UseFullHeight = true;

        //    //if (isColumnCreated) goto Label_01;
        //    this.currentDocument.SectionData.ClearColumns();

        //    int docwidth = this.CurrentDocument.DefaultPageSettings.PaperSize.Width;
        //    int colTotalWidth = 0;

        //    if (cols != null)
        //    {
        //        colTotalWidth = GetColumnsTotalWidth(cols);
        //        if (docwidth < colTotalWidth)
        //        {
        //            this.CurrentDocument.DefaultPageSettings.Landscape = true;
        //        }

        //        foreach (McColumn c in cols)
        //        {
        //            if (c.Display)
        //            {
        //                this.AddColumn(c.ColumnName, c.Caption, (float)c.Width, true, true, c.Alignment);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        colTotalWidth = GetColumnsTotalWidth(maxColumnWidth);
        //        if (docwidth < colTotalWidth)
        //        {
        //            this.CurrentDocument.DefaultPageSettings.Landscape = true;
        //        }
        //        this.AddAllColumns(maxColumnWidth, true, true);
        //    }
        //}

        //public void ResetStyles()
        //{
        //    TextStyle.ResetStyles();
        //    //isResetStyle = true;
        //}

        //public void SetCellMargins(float top, float left, float right, float bottom)
        //{
        //    TextStyle.TableRow.MarginNear = left / 10;// 0.1f;
        //    TextStyle.TableRow.MarginFar = right / 10;//0.1f;
        //    TextStyle.TableRow.MarginTop = top / 10;// 0.05f;
        //    TextStyle.TableRow.MarginBottom = bottom / 10;//0.05f;
        //}

        //public void SetHeaderMargins(float top, float left, float right, float bottom)
        //{
        //    TextStyle.TableHeader.MarginNear = left / 10;// 0.1f;
        //    TextStyle.TableHeader.MarginFar = right / 10;//0.1f;
        //    TextStyle.TableHeader.MarginTop = top / 10;// 0.05f;
        //    TextStyle.TableHeader.MarginBottom = bottom / 10;//0.05f;
        //}

        //public void SetHeader(Brush headerBack, Brush headerFore, Brush background)
        //{
        //    TextStyle.Heading1.Size = 24;
        //    TextStyle.Heading1.Bold = false;
        //    TextStyle.TableHeader.BackgroundBrush = headerBack;//Brushes.Navy;
        //    TextStyle.TableHeader.Brush = headerFore;//Brushes.White;
        //    TextStyle.TableRow.BackgroundBrush = background;//Brushes.White;//
        //}

        //public void SetDefaultStyle()
        //{
        //    //if (!isResetStyle)
        //    //    ResetStyles();
        //    SetHeader(Brushes.Navy, Brushes.White, Brushes.Transparent);
        //    SetHeaderMargins(0.5f, 1f, 1f, 0.5f);
        //    SetHeaderMargins(0.1f, 0.1f, 0.1f, 0.1f);
        //}

        //public void SetOrientation(bool landscape)
        //{
        //    this.CurrentDocument.DefaultPageSettings.Landscape = landscape;
        //}

        //public virtual void CreateColumns(McColumn[] cols)
        //{
        //   this.currentDocument.SectionData.ClearColumns();

        //    int docwidth = this.CurrentDocument.DefaultPageSettings.PaperSize.Width;
        //    int colTotalWidth = 0;

        //    if (cols != null)
        //    {
        //        colTotalWidth = GetColumnsTotalWidth(cols);
        //        if (docwidth < colTotalWidth)
        //        {
        //            this.CurrentDocument.DefaultPageSettings.Landscape = true;
        //        }

        //        foreach (McColumn c in cols)
        //        {
        //            if (c.Display)
        //            {
        //                ReportDataColumn rdc = this.AddColumn(c.ColumnName, c.Caption, (float)c.Width, true, true);
        //                if (c.DataType == DataTypes.Number)
        //                    rdc.ColumnAlignment = HorizontalAlignment.Right;
        //                else
        //                    rdc.ColumnAlignment = c.Alignment;
        //            }
        //        }
        //        //isColumnCreated = true;
        //    }
        //}

        //public virtual ReportDataColumn AddColumn(McColumn col)
        //{
        //    if (col == null)
        //    {
        //        throw new ArgumentException("Invalid Column control");
        //    }
        //    ReportDataColumn rdc = this.AddColumn(col.ColumnName, col.Caption, (float)col.Width, true, true);
        //    rdc.ColumnAlignment = col.Alignment;
        //    return rdc;
        //}

        //public virtual ReportDataColumn AddColumn(string colName, string caption, int width)
        //{
        //    if (colName != null || caption == null || width < 0)
        //    {
        //        throw new ArgumentException("Incorrect parameters");
        //    }
        //    ReportDataColumn rdc = this.AddColumn(colName, caption, (float)width, true, true);
        //    return rdc;
        //}

        //public virtual ReportDataColumn AddColumn(string colName, string caption, int width, HorizontalAlignment alignment)
        //{
        //    if (colName != null || caption == null || width < 0)
        //    {
        //        throw new ArgumentException("Incorrect parameters");
        //    }
        //    ReportDataColumn rdc = this.AddColumn(colName, caption, (float)width, true, true);
        //    rdc.ColumnAlignment = alignment;
        //    return rdc;
        //}

        //private int GetColumnsTotalWidth(McColumn[] cols)
        //{
        //    int colTotalWidth = 0;
        //    if (cols != null)
        //    {
        //        foreach (McColumn c in cols)
        //        {
        //            if (c.Display)
        //            {
        //                colTotalWidth += c.Width;
        //            }
        //        }
        //    }
        //    return colTotalWidth;
        //}

        //private int GetColumnsTotalWidth( float maxColWidth)
        //{
        //    int colTotalWidth = 0;
        //    DataView dv = this.currentDocument.SectionData.DataSource;
        //    if (dv != null)
        //    {
        //        colTotalWidth = dv.Table.Columns.Count * (int)maxColWidth;
        //    }
        //    return colTotalWidth;
        //}


	} // end class


    /// <summary>
    /// Exception thrown by the builder class when it is used incorrectly
    /// </summary>
    public class ReportBuilderException : ApplicationException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">Message</param>
        public ReportBuilderException(string msg)
            : base(msg)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="innerException">An inner exception</param>
        public ReportBuilderException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
    }
}
