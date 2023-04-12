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
using System.Collections.Generic;

using Nistec.Data;
using Nistec.Win32;

using Nistec.WinForms;
using Nistec.Data.Factory;
using Nistec.Data.Advanced;
using Nistec.Win;


namespace Nistec.GridView
{
    /// <summary>
    /// Displays the values of a data source in a table where each column represents a field and each row represents a record. This Grid allows you to select, sort, and edit these items.
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(Grid), "Images.Grid.bmp")]
    [Designer("Nistec.GridView.Design.GridDesigner"), DefaultProperty("DataSource")]//, DefaultEvent("Navigate")]
    public class Grid : McBind, ISupportInitialize, IGridEditingService, ILayout, IDataSource 
    {

        #region NetReflectedFram
        //internal bool m_netFram = false;
        /// <summary>
        /// NetReflectedFram
        /// </summary>
        /// <param name="pk"></param>
        public void NetReflectedFram(string pk)
        {
            //try
            //{
            //    // this is done because this method can be called explicitly from code.
            //    System.Reflection.MethodBase method = (System.Reflection.MethodBase)(new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
            //    m_netFram = Nistec.Net.License.nf_1.nf_2(method, pk);
            //}
            //catch { }
        }


        #endregion

        #region static members

        private const int GRIDSTATE_allowSorting =          1;
        private const int GRIDSTATE_columnHeadersVisible =  2;
        private const int GRIDSTATE_rowHeadersVisible =     4;
        private const int GRIDSTATE_trackColResize =        8;
        private const int GRIDSTATE_trackRowResize =        0x10;
        private const int GRIDSTATE_isLedgerStyle =         0x20;
        private const int GRIDSTATE_isFlatMode =            0x40;
        private const int GRIDSTATE_listHasErrors =         0x80;
        private const int GRIDSTATE_dragging =              0x100;
        private const int GRIDSTATE_inListAddNew =          0x200;
        private const int GRIDSTATE_inDeleteRow =           0x400;
        private const int GRIDSTATE_canFocus =              0x800;
        private const int GRIDSTATE_readOnlyMode =          0x1000;
        private const int GRIDSTATE_allowNavigation =       0x2000;
        private const int GRIDSTATE_isNavigating =          0x4000;
        private const int GRIDSTATE_isEditing =             0x8000;
        private const int GRIDSTATE_editControlChanging =   0x10000;
        private const int GRIDSTATE_isScrolling =           0x20000;
        private const int GRIDSTATE_overCaption =           0x40000;
        private const int GRIDSTATE_childLinkFocused =      0x80000;
        private const int GRIDSTATE_inAddNewRow =           0x100000;
        private const int GRIDSTATE_inSetListManager =      0x200000;
        private const int GRIDSTATE_metaDataChanged =       0x400000;
        private const int GRIDSTATE_exceptionInPaint =      0x800000;
 
        private const BorderStyle defaultBorderStyle = BorderStyle.Fixed3D;
        private const bool defaultCaptionVisible = true;
        private const GridLineStyle defaultGridLineStyle = GridLineStyle.Solid;
        private const GridParentRowsLabelStyle defaultParentRowsLabelStyle = GridParentRowsLabelStyle.Both;
        private const bool defaultParentRowsVisible = true;
        private const int defaultPreferredColumnWidth = 0x4b;
        private const int defaultRowHeaderWidth = 0x23;

        private static int defaultFontHeight;
        private const int NumRowsForAutoResize = 10;

        /// <summary>
        /// DefaultScrollWidth
        /// </summary>
        public const int DefaultScrollWidth = 20;
        /// <summary>
        /// DefaultColumnWidth
        /// </summary>
        public const int DefaultColumnWidth = 80;
        /// <summary>
        /// DefaultRowHeight
        /// </summary>
        public const int DefaultRowHeight = 20;
        /// <summary>
        /// DefaultCaptionHeight
        /// </summary>
        public const int DefaultCaptionHeight = 19;
        /// <summary>
        /// DefaultRowHeaderWidth
        /// </summary>
        public const int DefaultRowHeaderWidth = 35;
        /// <summary>
        /// DefaultColumnHeaderHeight
        /// </summary>
        public const int DefaultColumnHeaderHeight =20;// 19;//20;
        /// <summary>
        /// DefaultStatusBarHeight
        /// </summary>
        public const int DefaultStatusBarHeight = 22;

        internal const int MaxGridWidth = 1280;
        internal const int MinRowHeight = 20;
        internal const int hMargin = 7;

        /// <summary>
        /// GridNewRowState
        /// </summary>
        public enum GridNewRowState 
        { 
            /// <summary>
            /// AddNew
            /// </summary>
            AddNew,
            /// <summary>
            /// Cancel
            /// </summary>
            Cancel,
            /// <summary>
            /// Completed
            /// </summary>
            Completed 
        }

        #endregion

        #region members
        // Fields
        private GridStatusBar statusBar;
        internal List<DataRelation> relations; 

        private GridAddNewRow addNewRow;
        private bool allowColumnResize;
        private bool allowRowResize;
        private SolidBrush alternatingBackBrush;
        private SolidBrush backBrush;
        private SolidBrush backgroundBrush;
        private SolidBrush foreBrush;
        private SolidBrush gridLineBrush;
        private SolidBrush headerBackBrush;
        private SolidBrush headerForeBrush;
        private SolidBrush linkBrush;
        private SolidBrush selectionBackBrush;
        private SolidBrush selectionForeBrush;

        private GridLineStyle gridLineStyle;
        private bool paintAlternating;
        private EventHandler currentChangedHandler;
        private EventHandler metaDataChangedHandler;
        private EventHandler positionChangedHandler;
        private BindItemChangedEventHandler itemChangedHandler;
        private MouseEventArgs lastSplitBar;

        private BorderStyle borderStyle;
        private WinMethods.RECT[] cachedScrollableRegion;
        private GridCaption caption;
        private int captionFontHeight;
        internal bool checkHierarchy;
        internal int currentCol;
        internal int currentRow;
        internal TraceSwitch GridAcc;
        private GridRow[] dataGridRows;
        private int dataGridRowsLength;
        private string dataMember;
        private object dataSource;
        private GridTableStyle defaultTableStyle;
        private GridTableStyle m_TableStyle;
        private GridColumnStyle editColumn;
        private GridRow editRow;
        private const int errorRowBitmapWidth = 15;
        internal int firstVisibleCol;
        internal int firstVisibleRow;
        private int fontHeight;
        private BitVector32 gridState;
        private Font headerFont;
        private int headerFontHeight;
        private Pen headerForePen;
        private int horizontalOffset;
        private ScrollBar horizScrollBar;
        internal bool inInit;
        private int lastRowSelected;
        private int lastTotallyVisibleCol;
        internal LayoutData layout;
        private Font linkFont;
        private int linkFontHeight;
        private BindManager listManager;
        private int minRowHeaderWidth;
        internal GridTableStyle myGridTable;
        internal int negOffset;
        private int numSelectedRows;
        private int numTotallyVisibleRows;
        private int numVisibleCols;
        private int numVisibleRows;
        private int oldRow;
        private GridState originalState;
        private Policy policy;
        private int preferredColumnWidth;
        private int prefferedRowHeight;
        private int rowHeaderWidth;
        private int toolTipId;
        private GridToolTip toolTipProvider;
        private int trackColAnchor;
        private int trackColumn;
        private PropertyDescriptor trackColumnHeader;
        private int trackRow;
        private int trackRowAnchor;
        private ScrollBar vertScrollBar;
        private int wheelDelta;

        private bool dirty = false;
        internal bool forceDefaultTableStyle;
        //added
        private bool rowDirty = false;
        private HorizontalAlignment columnsHeaderAlignment;
        private bool rowNew = false;
        private bool autoAdjust = false;
        private ControlLayout m_ControlLayout;
        private string rowFilter = "";
        private SelectionType m_SelectionType = SelectionType.Cell;
        private DataView _DataView;

        private bool allowGridContexMenu;
        private bool allowColumnContexMenu;

        internal GridContexMenu _GridContexMenu;
        internal GridColumnContexMenu _GridColumnContexMenu;
        private int minRowHeight=0;
        private int colHeaderHot=-1;

        /// <summary>
        /// GridDirtyRowState
        /// </summary>
        public enum GridDirtyRowState 
        { 
            /// <summary>
            /// Editing
            /// </summary>
            Editing, 
            /// <summary>
            /// Cancel
            /// </summary>
            Cancel, 
            /// <summary>
            /// Completed
            /// </summary>
            Completed 
        }


        #region multi
        //private SolidBrush borderBrush;
        //private EventHandler backButtonHandler;
        //private NavigateEventHandler onNavigate;
        //private EventHandler onRowHeaderClick;
        //private EventHandler downButtonHandler;
        //internal GridTableCollection dataGridTables;
        //private CollectionChangeEventHandler dataGridTableStylesCollectionChanged;
        //private GridParentRows parentRows;
        //internal GridParentRowsLabelStyle parentRowsLabels;

        #endregion
 
        #endregion

        #region events
        // Events

        #region multi
        /// <summary>
        /// Occurs when the AllowNavigation property has changed
        /// </summary>
        [Description("Occurs when the AllowNavigation property has changed"), Category("PropertyChanged")]
        public event EventHandler AllowNavigationChanged;
        /// <summary>
        /// Occurs when the user navigates to a new table. 
        /// </summary>
        [Description("Occurs when the user navigates to a new table. "), Category("Action")]
        public event NavigateEventHandler Navigate;
        //		[Category("PropertyChanged"), Description("GridOnParentRowsLabelStyleChanged")]
        //		public event EventHandler ParentRowsLabelStyleChanged;
        //		[Category("PropertyChanged"), Description("GridOnParentRowsVisibleChanged")]
        //		public event EventHandler ParentRowsVisibleChanged;
        //		[Description("GridDownButtonClick"), Category("Action")]
        //		public event EventHandler ShowParentDetailsButtonClick;
        //		[Category("Action"), Description("GridBackButtonClick")]
        //		public event EventHandler BackButtonClick;
        //      [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        //      public event EventHandler BackgroundImageChanged;

        #endregion

        /// <summary>
        /// Occurs when the BackgroundColor has changed
        /// </summary>
        [Description("Occurs when the BackgroundColor has changed"), Category("PropertyChanged")]
        public event EventHandler BackgroundColorChanged;
        /// <summary>
        /// Occurs when the BorderStyle has changed
        /// </summary>
        [Description("Occurs when the BorderStyle has changed"), Category("PropertyChanged")]
        public event EventHandler BorderStyleChanged;
        /// <summary>
        /// Occurs when the Caption Visible has changed
        /// </summary>
        [Description("Occurs when the Caption Visible has changed"), Category("PropertyChanged")]
        public event EventHandler CaptionVisibleChanged;
        /// <summary>
        /// Occurs when the Current Cell has changed
        /// </summary>
        [Category("PropertyChanged"), Description("Occurs when the Current Cell has changed")]
        public event EventHandler CurrentCellChanged;
        /// <summary>
        /// Occurs when the Current Row has changed
        /// </summary>
        [Category("PropertyChanged"), Description("Occurs when the Current Row has changed")]
        public event EventHandler CurrentRowChanged;
        /// <summary>
        /// Occurs when a CurrentRow is changing
        /// </summary>
        [Category("PropertyChanged"), Description("Occurs when a CurrentRow is changing")]
        public event CancelEventHandler CurrentRowChanging;
        /// <summary>
        /// Occurs when a New Row Added
        /// </summary>
        [Category("PropertyChanged"), Description("On New Row Added and before completed")]
        public event EventHandler NewRowAdded;
        /// <summary>
        /// After New Row completed
        /// </summary>
        [Category("PropertyChanged"), Description("After New Row completed")]
        public event EventHandler NewRowCompleted;
        /// <summary>
        /// Before Current Row Deleting
        /// </summary>
        [Category("PropertyChanged"), Description("Before Current Row Deleting")]
        public event CancelEventHandler RowDeleting;
        /// <summary>
        /// After Row Deleted
        /// </summary>
        [Category("PropertyChanged"), Description("After Row Deleted")]
        public event EventHandler RowDeleted;
        /// <summary>
        /// Cursor Changed 
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler CursorChanged;
        /// <summary>
        /// Occurs when the DataSource property value has changed
        /// </summary>
        [Description("Occurs when the DataSource property value has changed"), Category("PropertyChanged")]
        public event EventHandler DataSourceChanged;
        /// <summary>
        /// Occurs when the FlatMode has changed
        /// </summary>
        [Category("PropertyChanged"), Description("Occurs when the FlatMode has changed")]
        public event EventHandler FlatModeChanged;
        /// <summary>
        /// Occurs when GridNode Clicked
        /// </summary>
        [Description("Occurs when GridNode Clicked"), Category("Action")]
        internal event EventHandler NodeClick;
        /// <summary>
        /// Occurs when the ReadOnly property value changes
        /// </summary>
        [Category("PropertyChanged"), Description("Occurs when the ReadOnly property value changes")]
        public event EventHandler ReadOnlyChanged;
        /// <summary>
        /// Occurs when a row header is clicked
        /// </summary>
        [Category("PropertyChanged"), Description("Occurs when a row header is clicked")]
        protected event EventHandler RowHeaderClick;
        /// <summary>
        /// Occurs when the user scrolls the Grid control
        /// </summary>
        [Category("Action"), Description("Occurs when the user scrolls the Grid control")]
        public event EventHandler Scroll;
        /// <summary>
        /// Occurs when Text Changed
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler TextChanged;
        /// <summary>
        /// Occurs when the Filter has been Changed
        /// </summary>
        [Category("Action"), Description("Occurs when the Filter has been Changed")]
        public event EventHandler FilterChanged;
        /// <summary>
        /// Occurs when the Grid has been changed until the next AcceptChanges
        /// </summary>
        [Category("Action"), Description("Occurs when the Grid has been changed until the next AcceptChanges")]
        public event EventHandler DirtyChanged;
        /// <summary>
        /// Occurs when the Grid Row has been changed
        /// </summary>
        [Category("Action"), Description("Occurs when the Grid Row has been changed")]
        public event EventHandler DirtyRowChanged;
        /// <summary>
        /// Occurs when the GridButtomColumn Clicked
        /// </summary>
        [Category("Action"), Description("Occurs when the GridButtomColumn Clicked")]
        public event ButtonClickEventHandler ButtonClick;
        /// <summary>
        /// Summarize Change event
        /// </summary>
        [Category("StatusBar"), Description("Summarize Change event")]
        public event EventHandler SummarizeChange;
        /// <summary>
        /// Occurs when the Column Size has been changed
        /// </summary>
        [Category("Action"), Description("Occurs when the Column Size has been changed")]
        public event ColumnResizeHandler ColumnResize;
        /// <summary>
        /// OnButtonClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal protected virtual void OnSummarizeChange(object sender, EventArgs e)
        {
            if (SummarizeChange != null)
                SummarizeChange(sender, e);
        }
        /// <summary>
        /// OnButtonClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal protected virtual void OnButtonClick(object sender, ButtonClickEventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(sender, e);
        }
        /// <summary>
        /// ErrorOcurredEventHandler ErrorOcurred
        /// </summary>
        public event ErrorOcurredEventHandler ErrorOcurred;

        /// <summary>
        /// OnErrorOccouerd
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnErrorOccouerd(ErrorOcurredEventArgs e)
        {
            if (ErrorOcurred != null)
                ErrorOcurred(this, e);
        
        }
        /// <summary>
        /// OnErrorOccouerd
        /// </summary>
        /// <param name="msg"></param>
        protected virtual void OnErrorOccouerd(string msg)
        {
            if (ErrorOcurred != null)
              OnErrorOccouerd(new ErrorOcurredEventArgs(msg));
        }

        #region validating
        /// <summary>
        /// Occurs when a Cell's Update , but befor the GridView control updates the Cell.
        /// </summary>
        [Category("PropertyChanged"), Description("GridOnCurrentCellValidating")]
        public event CellValidatingEventHandler CellValidating;

        /// <summary>
        /// Occurs when a Cell's Update , but after the GridView control updates the Cell.
        /// </summary>
        [Category("PropertyChanged"), Description("GridOnCurrentCellValidated")]
        public event EventHandler CellValidated;

        internal virtual bool OnCellValidating(GridColumnStyle sender, int rowNum, object value)
        {
            if (CellValidating != null)
            {
                CellValidatingEventArgs evnt = new CellValidatingEventArgs(rowNum, sender.MappingName, value);
                CellValidating(sender, evnt);
                return !(evnt.Cancel);
            }
            return true;
        }

        internal virtual void OnCellValidated(GridColumnStyle sender)
        {
            if (CellValidated != null)
            {
                CellValidated(sender, EventArgs.Empty);
            }
        }
        #endregion

        #endregion

        #region Ctor

        static Grid()
        {
            Grid.defaultFontHeight = Control.DefaultFont.Height;

        }

        /// <summary>
        /// Initializes a new instance of the Grid class.
        /// </summary>
        public Grid()
        {
            forceDefaultTableStyle = false;
            columnsHeaderAlignment = HorizontalAlignment.Left;

            this.GridAcc = null;
            this.defaultTableStyle = new GridTableStyle(this, true);
            this.m_TableStyle = new GridTableStyle(this);

            this.paintAlternating = false;
            m_ControlLayout = ControlLayout.Visual;
            //ResetStyleLayout();

            this.alternatingBackBrush = Grid.DefaultAlternatingBackBrush;
            this.gridLineBrush = Grid.DefaultGridLineBrush;
            this.gridLineStyle = GridLineStyle.Solid;
            this.headerBackBrush = Grid.DefaultHeaderBackBrush;
            this.selectionBackBrush = Grid.DefaultSelectionBackBrush;
            this.selectionForeBrush = Grid.DefaultSelectionForeBrush;
            this.backBrush = Grid.DefaultBackBrush;
            this.foreBrush = Grid.DefaultForeBrush;
            this.backgroundBrush = Grid.DefaultBackgroundBrush;
            this.headerForeBrush = Grid.DefaultHeaderForeBrush;
            this.headerForePen = Grid.DefaultHeaderForePen;
            this.linkBrush = Grid.DefaultLinkBrush;

            this.BackColor = Grid.DefaultBackBrush.Color;
            this.ForeColor = Grid.DefaultForeBrush.Color;

            ResetStyleLayout();

            this.prefferedRowHeight = Grid.defaultFontHeight + 3;

            this.headerFont = null;
            this.preferredColumnWidth = 0x4b;
            this.rowHeaderWidth = 0x23;
            this.originalState = null;
            this.dataGridRows = new GridRow[0];
            this.dataGridRowsLength = 0;
            this.toolTipId = 0;
            this.toolTipProvider = null;
            this.addNewRow = null;
            this.layout = new Grid.LayoutData();
            this.cachedScrollableRegion = null;
            this.allowColumnResize = true;
            this.allowRowResize = true;
            this.trackColAnchor = 0;
            this.trackColumn = 0;
            this.trackRowAnchor = 0;
            this.trackRow = 0;
            this.trackColumnHeader = null;
            this.lastSplitBar = null;
            this.linkFont = null;
            this.fontHeight = -1;
            this.linkFontHeight = -1;
            this.captionFontHeight = -1;
            this.headerFontHeight = -1;
            this.dataSource = null;
            this.dataMember = "";
            this.listManager = null;
            this.myGridTable = null;
            this.checkHierarchy = true;
            this.inInit = false;
            this.currentRow = 0;
            this.currentCol = 0;
            this.numSelectedRows = 0;
            this.lastRowSelected = -1;
            this.policy = new Grid.Policy();
            this.editColumn = null;
            this.editRow = null;
            this.horizScrollBar = new HScrollBar();
            this.vertScrollBar = new VScrollBar();
            this.horizontalOffset = 0;
            this.negOffset = 0;
            this.wheelDelta = 0;
            this.firstVisibleRow = 0;
            this.firstVisibleCol = 0;
            this.numVisibleRows = 0;
            this.numVisibleCols = 0;
            this.numTotallyVisibleRows = 0;
            this.lastTotallyVisibleCol = 0;
            this.oldRow = -1;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.Opaque, false);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            base.SetStyle(ControlStyles.UserMouse, true);
 
            this.gridState = new BitVector32(0x42827);
            this.layout = this.CreateInitialLayoutState();
            this.horizScrollBar.Top = base.ClientRectangle.Height - this.horizScrollBar.Height;
            this.horizScrollBar.Left = 0;
            this.horizScrollBar.Visible = false;
            this.horizScrollBar.Scroll += new ScrollEventHandler(this.GridHScrolled);
            base.Controls.Add(this.horizScrollBar);
            this.vertScrollBar.Top = 0;
            this.vertScrollBar.Left = base.ClientRectangle.Width - this.vertScrollBar.Width;
            this.vertScrollBar.Visible = false;
            this.vertScrollBar.Scroll += new ScrollEventHandler(this.GridVScrolled);
            base.Controls.Add(this.vertScrollBar);
            this.BorderStyle = BorderStyle.Fixed3D;
            this.currentChangedHandler = new EventHandler(this.DataSource_RowChanged);
            this.positionChangedHandler = new EventHandler(this.DataSource_PositionChanged);
            this.itemChangedHandler = new BindItemChangedEventHandler(this.DataSource_ItemChanged);
            this.metaDataChangedHandler = new EventHandler(this.DataSource_MetaDataChanged);
            this.SetGridTable(this.m_TableStyle, true, true);
            //this.SetGridTable(this.defaultTableStyle, true, true);
            this.caption = new GridCaption(this);
            this.allowGridContexMenu=true;
            this.allowColumnContexMenu=true;

            PerformGridContextMenu();

            #region multi
            //this.parentRows = null;
            //this.parentRowsLabels = GridParentRowsLabelStyle.Both;
            //this.dataGridTables = null;
            //this.dataGridTables = new GridTableCollection(this);
            //this.parentRows = new GridParentRows(this);
            //this.dataGridTableStylesCollectionChanged = new CollectionChangeEventHandler(this.TableStylesCollectionChanged);
            //this.dataGridTables.CollectionChanged += this.dataGridTableStylesCollectionChanged;
            //this.backButtonHandler = new EventHandler(this.OnBackButtonClicked);
            //this.downButtonHandler = new EventHandler(this.OnShowParentDetailsButtonClicked);
            //this.caption.BackwardClicked += this.backButtonHandler;
            //this.caption.DownClicked += this.downButtonHandler;
            #endregion

            this.RecalculateFonts();
            base.Size = new Size(130, 80);
            base.Invalidate();
            base.PerformLayout();
        }

        #endregion

        #region Advandes

        //public VGridField[] SummarizeColumns()
        //{
        //    if (RowCount == 0)
        //        return null;
        //    VGridFieldCollection vcols = new VGridFieldCollection();
        //    GridColumnCollection gridCols= this.GridColumns;
        //    foreach (GridColumnStyle c in gridCols)
        //    {
        //        if (c.DataType == FieldType.Number)
        //        {
        //            object o=PerformCompute( AggregateMode.Sum,c.MappingName,this.RowFilter);
        //            VGridField field=new VGridField(c.MappingName,o);
        //            vcols.Add(field);
        //        }
        //    }
        //    if (vcols.Count == 0)
        //        return null;
        //   return vcols.GetFieldsArray();
        //}

        //public void CreateColumns(string[] mappingNames)
        //{
        //    CreateColumns(mappingNames, mappingNames);
        //}

        //public void CreateColumns(string[] mappingNames, string[] HeaderNames)
        //{
        //    if (mappingNames == null || HeaderNames == null)
        //    {
        //        throw new ArgumentException("Invalid mappingNames or  HeaderNames ");
        //    }
        //    if (mappingNames.Length == 0 || HeaderNames.Length == 0)
        //    {
        //        throw new ArgumentException("Invalid mappingNames or  HeaderNames ");
        //    }
        //    this.Columns.Clear();
        //    GridTextColumn gc = null;

        //    for (int i = 0; i < mappingNames.Length; i++)
        //    {
        //        gc = new GridTextColumn();
        //        gc.MappingName = mappingNames[i];
        //        gc.HeaderText = HeaderNames[i];
        //        gc.Width = Grid.DefaultColumnWidth;
        //        this.Columns.Add(gc);
        //    }
        //}

        /// <summary>
        /// Init Grid data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <param name="mappingName"></param>
        /// <param name="columnsMappingName"></param>
        /// <param name="columnsHeaderNames"></param>
        public void Init(object dataSource, string dataMember, string mappingName, string[] columnsMappingName, string[] columnsHeaderNames)
        {
            GridPerform.CreateColumns(this, columnsMappingName, columnsHeaderNames);
            Init(dataSource, dataMember, mappingName);
        }
        /// <summary>
        /// Init Grid data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <param name="mappingName"></param>
        public void Init(object dataSource, string dataMember, string mappingName)
        {
            //if (mappingName == null || mappingName == "")
            //{
            //    throw new ArgumentException("Invalid MappingName");
            //}

            BeginInit();
            this.DataMember = dataMember;
            this.MappingName = mappingName;
            this.DataSource = dataSource;
            EndInit();
        }

        /// <summary>
        /// Gets the internal height of the grid control.
        /// </summary>
        [Browsable(false)]
        public int HeightInternal
        {
            get
            {
                int captionHeight = CaptionVisible ? this.layout.Caption.Height : 0;
                return this.Height - captionHeight - 12;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the grid has been marked as changed. 
        /// </summary>
        [Browsable(false)]
        public bool Dirty
        {
            get { return dirty; }
        }
        /// <summary>
        /// Gets a value indicating whether the grid current row has been marked as changed. 
        /// </summary>
        [Browsable(false)]
        public bool DirtyRow
        {
            get { return this.rowDirty; }
        }


        internal void OnDirty(GridDirtyRowState state)
        {
            bool isDirty = state == GridDirtyRowState.Editing;
            
            if (dirty != isDirty)
            {
                dirty = isDirty;
                if (this.DirtyChanged != null)
                {
                    this.DirtyChanged(this, EventArgs.Empty);
                }
            }
            if (!isDirty && this.rowDirty)
            {
                OnDirtyRow(state);// (false);
            }

        }

        /// <summary>
        /// Occurs when grid value has been changed
        /// </summary>
        /// <param name="state"></param>
        protected virtual void OnDirtyRow(GridDirtyRowState state)//bool isDirty)
        {
            bool isDirty = state == GridDirtyRowState.Editing;

            if (this.rowDirty != isDirty)
            {
                this.rowDirty = isDirty;
                if (this.DirtyRowChanged != null)
                {
                    this.DirtyRowChanged(this, EventArgs.Empty);
                }
                if (rowNew && state == GridDirtyRowState.Completed)// !isDirty)
                {
                    OnNewRowChanged(GridNewRowState.Completed);
                }
                if (isDirty && !this.dirty)
                {
                    OnDirty(state);//(true);
                }
            }
        }


        //		[Localizable(true), Description("GridGridTables"), Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //		public GridTableCollection TableStyles
        //		{
        //			get
        //			{
        //				return this.dataGridTables;
        //			}
        //		}

        /// <summary>
        /// Gets or sets the name of the data member to map the data source to. 
        /// </summary>
        [DefaultValue(""), Category("Data"), Description("DataSource Mapping name ")]//[Browsable(false)]//[Editor("Nistec.GridView.Design.GridTableMappingNameEditor", typeof(UITypeEditor))]
        public string MappingName
        {
            get
            {
                return m_TableStyle.MappingName;
            }
            set
            {

                m_TableStyle.MappingName = value;

            }
        }
        /// <summary>
        /// Gets a collection of DataField objects that represent the column fields in a Grid control at design time.
        /// </summary>
        [Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        public virtual GridColumnCollection Columns
        {
            get
            {
                //if (DesignMode || myGridTable == null)
                //{
                   // return m_TableStyle.GridColumnStyles;
                //}
                return this.myGridTable.GridColumnStyles;
            }
        }

        [Browsable(false)]
        internal GridTableStyle TableStyle
        {
            get
            {
                return myGridTable;
            }
        }

        /// <summary>
        /// Gets a collection of DataField objects that represent the column fields in a Grid control at run time.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal GridColumnCollection GridColumns
        {
            get
            {
                return this.myGridTable.GridColumnStyles;
            }
        }

        ///// <summary>
        ///// Gets a collection of DataField objects that represent the column fields in a Grid control at run time.
        ///// </summary>
        //public GridColumnCollection GetGridColumnStyles()
        //{
        //    if (myGridTable == null)
        //        return null;
        //    return this.myGridTable.GridColumnStyles;
        //}
        ///// <summary>
        ///// Gets a GridColumn of DataField objects that represent the column field in a GridColumns collection by column index.
        ///// </summary>
        ///// <param name="index"></param>
        ///// <returns></returns>
        // public GridColumnStyle GetGridColumnStyles(int index)
        //{
        //    GridColumnCollection cols = GridColumns;
        //    if (cols == null)
        //        return null;
        //    return cols[index];
        //}
        ///// <summary>
        ///// Gets a GridColumn of DataField objects that represent the column field in a GridColumns collection by column Name.
        ///// </summary>
        ///// <param name="columnName"></param>
        ///// <returns></returns>
        //public GridColumnStyle GetGridColumnStyles(string columnName)
        //{
        //    GridColumnCollection cols = GridColumns;
        //    if (cols == null)
        //        return null;
        //    return cols[columnName];
        //}

        /// <summary>
        /// Gets the DataView that the Grid is displaying data for.
        /// </summary>
        [Browsable(false)]
        public System.Data.DataView DataList
        {
            get
            {
                if (_DataView == null)
                {
                    CreateDataView();
                }
                return _DataView;
           }
        }

        /// <summary>
        /// Overloaded. Gets or sets the cell located at the intersection of the specified column index in current row. 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        [Browsable(false)]
        public object this[int col]
        {
            get { return this[this.CurrentRowIndex, col]; }
            set 
            { 
                this[this.CurrentRowIndex, col] = value; 
            }
        }

        /// <summary>
        /// Overloaded. Gets or sets the cell located at the intersection of the specified column name in current row. 
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        [Browsable(false)]
        public object this[string colName]
        {
            get { return ItemCell(this.CurrentRowIndex, colName); }
            set { ItemCell(this.CurrentRowIndex, colName, value); }
        }
        /// <summary>
        /// Overloaded. Gets or sets the cell located at the intersection of the specified row and column . 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        [Browsable(false)]
        public object this[int row, string colName]
        {
            get { return ItemCell(row, colName); }
            set { ItemCell(row, colName, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating how the cells of the Grid can be selected.
        /// </summary>
        [Category("Behavior"), DefaultValue(SelectionType.Cell)]
        public SelectionType SelectionType
        {
            get { return this.m_SelectionType; }
            set
            {
                this.m_SelectionType = value;
                //Updated
                if (value == SelectionType.LabelRow)
                {
                    this.ReadOnly = true;
                }

            }
        }

     
        //internal void SetCurrentCell(int row, int col)
        //{
        //    GridCell curCell = new GridCell(row, col);
        //    this.CurrentCell = curCell;
        //}

        [Description("Get Item Cell In Grid")]
        private object ItemCell(int row, string colName)
        {
            if (row < 0) return null;
            int col = this.myGridTable.GridColumnStyles.IndexOf(colName);
            if (col == -1) return null;
            return this[row, col];
        }

        private void ItemCell(int row, string colName, object value)
        {
            try
            {
                if (row < 0) return;
                int col = this.myGridTable.GridColumnStyles.IndexOf(colName);
                if (col == -1) return;
                this[row, col] = value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int GetColumnIndex(string colName)
        {
            try
            {
                return this.myGridTable.GridColumnStyles.IndexOf(colName);
            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// Get Cell Display Text of the specified row and column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public string GetCellText(int row,int col)
        {
            try
            {
                object cellContent = this[row, col];
                if (cellContent== null || cellContent == DBNull.Value)
                {
                    return "";
                }
                GridColumnStyle cs = this.myGridTable.GridColumnStyles[col];

                if (cs.ColumnType == GridColumnType.ComboColumn)
                {
                    cellContent = ((GridComboColumn)cs).GetDisplyValue(cellContent);
                }
                return cellContent.ToString();
            }
            catch
            {
                return null;
            }
        }

        internal Control GetCurrentColumnControl(bool allowReadOnly)
        {
            if (currentCol < 0)
                return null;
            GridColumnStyle col = GridColumns[currentCol];
            if (!allowReadOnly && col.ReadOnly)
                return null;
            Control c = col.HostControl;
            if (c == null || !c.ContainsFocus)
                return null;
            return c;
        }
        internal void SetCurrentColumnValue(bool allowReadOnly,string value)
        {
            if (currentCol < 0)
                return ;
            GridColumnStyle col = GridColumns[currentCol];
            if (!allowReadOnly && col.ReadOnly)
                return ;
            //Control c = col.HostControl;
            //if (c == null || !c.ContainsFocus)
            //    return ;
            col.Text = value;
            //col.SetCurrentText(value);
            this.ColumnStartedEditing(col.Bounds);

        }

        internal bool IsDesignMode
        {
            get { return this.DesignMode; }
        }

        internal Rectangle CaptionRect()
        {
            return this.layout.Caption;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetVisibleColumnsCount()
        {
            return this.myGridTable.GetVisibleColumnsCount();
        }
        /// <summary>
        /// GetVisibleColumns
        /// </summary>
        /// <returns></returns>
        public GridColumnStyle[] GetVisibleColumns()
        {
            return this.myGridTable.GetVisibleColumns();
        }
        /// <summary>
        /// GetBoundsColumns
        /// </summary>
        /// <returns></returns>
        public GridColumnStyle[] GetBoundsColumns()
        {
            return this.myGridTable.GetBoundsColumns();
        }

        internal int CalcRowsHeight(int visibleRows)
        {
             int rows =0;
             if (this.listManager != null)
             {
                 rows = this.listManager.Count;

                 if (visibleRows > 0 && visibleRows < rows)
                 {
                     rows = visibleRows;
                 }
             }
            int rowHeight = PreferredRowHeight;
            int rowsAdd = this.AllowAdd ? 1 : 0;
            int headerAdd = ColumnHeadersVisible ? this.layout.ColumnHeaders.Height : 0;//.DefaultColumnHeaderHeight:0 ;
            //int CaptionHeight=CaptionVisible  ? Grid.DefaultCaptionHeight :0;
            return ((rowsAdd + rows) * rowHeight) + headerAdd;
        }

        /// <summary>
        /// AdjustColumns
        /// </summary>
        /// <param name="showScroll"></param>
        /// <param name="calcHeight"></param>
        internal protected void AdjustColumns(bool showScroll, bool calcHeight)
        {
            try
            {
                int gridHeight = 0;
                int calcRowsHeight = 0;

                bool showVerticalScroll = showScroll;
                GridTableStyle ts = null;

                if (this.DesignMode)
                {
                    if (this.Columns.Count == 0)
                    {
                        MessageBox.Show("No ColumnStyles Define");
                        return;
                    }
                    ts = this.m_TableStyle;
                    gridHeight = this.Height - Grid.DefaultCaptionHeight;
                }
                else
                {
                    ts = this.myGridTable;
                    if (calcHeight)
                    {
                        int w = this.Height;
                        gridHeight = HeightInternal;
                        calcRowsHeight = CalcRowsHeight(0);// -3;
                        showVerticalScroll = (calcRowsHeight > gridHeight);
                    }
                }


                int scrollWidth = this.vertScrollBar.Width;
                if (!showVerticalScroll)
                    scrollWidth = 0;

                int rowHeader = 0;
                if (this.RowHeadersVisible)
                    rowHeader = this.RowHeaderWidth;

                int colCount = ts.GridColumnStyles.Count;
                int gridWidth = this.Width - scrollWidth - rowHeader - 4;
                ArrayList list = new ArrayList();

                int flag1 = 0;
                bool match = false;
                while (!match && flag1 <= 1)
                {
                    int visibleCols = 0;
                    int colsWidth = 0;
                    int colsW = 0;

                    for (int i = 0; i < colCount; i++)
                    {

                        colsW = 0;
                        colsW = ts.GridColumnStyles[i].Width;

                        if (colsW < 0)
                            colsW = Grid.DefaultColumnWidth;

                        if (colsW > 0)
                        {
                            list.Add(ts.GridColumnStyles[i]);
                            colsWidth += colsW;
                            visibleCols++;
                        }
                    }

                    if (visibleCols == 0)
                    {
                        MessageBox.Show("No ColumnStyles Define");
                        return;
                    }

                    if (MaxGridWidth < colsWidth)
                    {
                        gridWidth = MaxGridWidth - Grid.DefaultScrollWidth;

                    }

                    int sumColsW = 0;
                    int defaultWidth = this.PreferredColumnWidth;

                    if (colsWidth == gridWidth)
                        return;

                    float ColDiff = (float)((gridWidth - colsWidth) / visibleCols);
                    colsW = 0;

                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        colsW = ((GridColumnStyle)list[i]).Width;
                        colsW += ((int)ColDiff);
                        if (colsW < 0)
                            colsW = defaultWidth;

                        ((GridColumnStyle)list[i]).Width = colsW;
                        sumColsW += colsW;
                    }
                    int diff = gridWidth - sumColsW;
                    if (diff < 0)
                        diff = defaultWidth;

                    ((GridColumnStyle)list[list.Count - 1]).Width = diff;
                    match = (sumColsW + diff) == gridWidth;
                    flag1++;
                    list.Clear();
                }

            }
            catch//(Exception ex)
            {
                MessageBox.Show("Adjust Columns Failed");//ex.Message);
            }
        }
        /// <summary>
        /// Adjust Columns width
        /// </summary>
        public void AdjustColumns()
        {
            if (this.DesignMode)
                OnDesignAdjustColumns(true);
            else
                OnResizeAdjustColumns();
        }

        /// <summary>
        /// Adjust Columns Occurs at design time
        /// </summary>
        /// <param name="showScroll"></param>
        internal protected void OnDesignAdjustColumns(bool showScroll)
        {
            if (this.Columns.Count == 0)
            {
                return;
            }

            ColumnsVisibility cv = new ColumnsVisibility(this, true, showScroll,false);
            if (cv.IsEmpty)
                return;

            int diff = cv.VisibleDiff;
            foreach (GridColumnStyle gc in cv.columns)
            {
                if (cv.ShouldResizeDesign(gc))
                {
                    gc.Width = Math.Max(0/*DefaultColumnWidth*/, gc.Width + diff);
                }
            }
        }
        /// <summary>
        /// Adjust Columns Occurs at run time
        /// </summary>
        internal protected void OnResizeAdjustColumns()
        {
            if (this.Initializing || this.DesignMode)
                return;
 
            ColumnsVisibility cv = new ColumnsVisibility(this,false);
            if (cv.IsEmpty)
                return;

            int diff = cv.VisibleDiff;
            foreach (GridColumnStyle gc in cv.columns)
            {
                if (cv.ShouldResize(gc))
                {
                    gc.Width = Math.Max(0/*DefaultColumnWidth*/, gc.Width + diff);
                }
            }
        }

        /// <summary>
        /// Gets or sets the expression used to filter which rows are viewed in the DataView.
        /// </summary>
        [Category("Data"),DefaultValue("")]
        public string RowFilter
        {
            get 
            {
                if (_DataView != null)
                    return _DataView.RowFilter;
                return rowFilter; 
            }
            set
            {
                if (value == null)
                    value = "";
                if (rowFilter != value)
                {
                    rowFilter = value;
                    OnFilterChanged();
                }
            }
        }

        private void OnFilterChanged()
        {
            if (this.DataList != null )
            {
                if (this.DataList.RowFilter != rowFilter)
                {
                    try
                    {
                        this.DataList.RowFilter = rowFilter;
                        OnFilterChanged(EventArgs.Empty);
                        //this.SetDataBinding(this.DataList, "");
                    }
                    catch
                    {
                        OnErrorOccouerd("Filter");
                    }
                }
            }
        }
        /// <summary>
        /// Occurs when RowFilter has been changed
        /// </summary>
        /// <param name="e"></param>
         protected virtual void OnFilterChanged(EventArgs e)
        {
            if (FilterChanged != null)
            {
                FilterChanged(this, e);
            }
        }

        /// <summary>
        /// Set Row Filter
        /// </summary>
        /// <param name="filter"></param>
        public void SetFilter(string filter)
        {
            RowFilter = filter;
        }

        //public void SetFilter(string filter)
        //{
        //    System.Data.DataView dtv = GetDataList();
        //    if (dtv == null) return;

        //    //dtv.RowFilter = "";
        //    //string msgNoFound = "No records were found that match the filter criteria.";

        //    try
        //    {
        //        if ((filter.Trim().Length == 0))
        //        {
        //            //MessageBox.Show(msgNoFound, "Nistec", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return;
        //        }
        //        dtv.RowFilter = filter;
        //        rowFilter = filter;
        //        this.SetDataBinding(dtv, "");
        //        //this.DataSource = dtv;
        //        // //Display the number of rows in the view

        //        //base.Text= (STATUS_MESSAGE + dtv.Count.ToString());
        //        //LblFilter.Text = strFilter;
        //        if ((dtv.Count == 0))
        //        {
        //            //MessageBox.Show(msgNoFound, "Nistec", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Nistec");
        //    }
        //}

        /// <summary>
        /// Remove the current row filter
        /// </summary>
        public void RemoveFilter()
        {
            //System.Data.DataView dtv = GetDataList();
            //if (dtv == null) return;
            RowFilter = "";
        }

        /// <summary>
        /// Gets a value indicating whether the Data source has changes, including new, deleted, or modified rows
        /// </summary>
        /// <returns></returns>
        public bool HasChanges()
        {
             if (_DataView == null) return false;
            return _DataView.Table.DataSet.HasChanges();
        }

        /// <summary>
        /// Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the Data source by MappingName property
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="provider"></param>
        /// <param name="dbTableName"></param>
        /// <returns></returns>
        public int UpdateChanges(string connectionString, Data.DBProvider provider, string dbTableName)
        {

            if (!this.dirty) return 0;
            try
            {
                //DataView dv = GetDataList();
                if (DataList == null) return -1;
                this.EndEdit();

                IDbCmd cmd = DbFactory.Create(connectionString, provider);
                int res = cmd.Adapter.UpdateChanges(_DataView.Table, dbTableName);

                if (res > 0)
                {
                    _DataView.Table.AcceptChanges();
                }
                //dirty=false;
                this.OnDirty(GridDirtyRowState.Completed);//(false);
                return res;
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the Data source by MappingName property
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dbTableName"></param>
        /// <returns></returns>
        public int UpdateChanges(System.Data.IDbConnection conn, string dbTableName)
        {
            if (!this.dirty) return 0;
            try
            {
               
                //DataView dv = GetDataList();
                if (DataList == null) return -1;
                this.EndEdit();

                IDbCmd cmd = DbFactory.Create(conn);
                int res = cmd.Adapter.UpdateChanges(_DataView.Table, dbTableName);

                if (res > 0)
                {
                    _DataView.Table.AcceptChanges();
                }
                //dirty=false;
                this.OnDirty(GridDirtyRowState.Completed);// (false);
                return res;
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Accept Changes local only
        /// </summary>
        public void AcceptChanges()
        {
            OnDirtyRow(GridDirtyRowState.Completed);
            this.listManager.EndCurrentEdit();
            if (this.DataList != null)
            {
                this.DataList.Table.AcceptChanges();
            }
            this.OnDirty(GridDirtyRowState.Completed);
        }

        /// <summary>
        /// Gets a copy of the DataTable containing all changes made to it since it was last loaded, or since AcceptChanges was called.
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable GetChanges()
        {
            //DataView dv = GetDataList();
            if (DataList == null) return null;
            return _DataView.Table.GetChanges();
        }

        /// <summary>
        /// Rejects all changes made to the row since AcceptChanges was last called. 
        /// </summary>
        public void RejectChanges()
        {
            OnDirtyRow(GridDirtyRowState.Cancel);
            this.listManager.CancelCurrentEdit();
            if (this.DataList != null)
            {
                this.DataList.Table.RejectChanges();
            }
            this.OnDirty(GridDirtyRowState.Cancel);
        }
        /// <summary>
        /// Forces a repopulation of the data-bound list. 
        /// </summary>
        public void RefreshList()
        {
            if (listManager != null)
                this.listManager.Refresh();
        }

        /// <summary>
        /// Gets the total number of GridRow objects in this collection.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Description("GridRows")]
        public int RowCount
        {
            get
            {
                return listManager == null ? 0 : listManager.Count;
            }
        }
        /// <summary>
        /// Get DataRow by row index
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public System.Data.DataRow GetDataRow(int indx)
        {
            try
            {
                //DataView dv = GetDataList();
                if (DataList == null) return null;

                return _DataView.Table.Rows[indx];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Current DataRow
        /// </summary>
        /// <returns></returns>
        public System.Data.DataRowView GetCurrentDataRow()
        {
            try
            {
                if(this.ListManager==null)
                    return null;
                return (DataRowView)this.ListManager.Current;
            }
            catch //(Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Represents a collection of rows for a Grid DataView.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Description("GridRows")]
        public System.Collections.IList Rows
        {
            get
            {
                return listManager == null ? null : listManager.List;
            }
        }


        //		internal bool SetMaxWidth(int value)
        //		{
        ////			if(value<Grid.DefaultGridWidth || value>Grid.MaxGridWidth)
        ////				return false;
        ////			m_MaxWidth=value; 
        //			return true;
        //		}

        #endregion

        #region Perform

        //public virtual void PerformColumnsFilter()
        //{
        //    if (this.RowCount > 0)
        //        GridColumnFilterDlg.ShowColumns(this);
        //}

        //public virtual void PerformPrint()
        //{
        //    if (this.RowCount > 0)
        //        PrintGridDataView.Print(this);
        //}

        //public virtual void PerformFind()
        //{
        //    if (this.RowCount > 0)
        //        FindDlg.Open(this);
        //}

        //public virtual void PerformCurrentRow()
        //{
        //    DataRowView drv = this.GetCurrentDataRow();
        //    if (drv != null)
        //    {
        //        //VGridDlg.VGridShow(drv, this.MappingName);
        //        VGridDlg gd = new VGridDlg(drv, this.MappingName);
        //        gd.VGrid.SetColumnStylesToFields(this.GetGridColumnStyles());
        //        gd.VGrid.SetStyleLayout(this.LayoutManager.Layout);
        //        gd.VGrid.ControlLayout = this.ControlLayout;
        //        gd.Show();
        //    }
        //}

        //public virtual void PerformChart()
        //{
        //    if (this.RowCount > 0)
        //        GridChartDlg.Open(this, this.caption.Text);
        //}

        //public virtual void PerformSummarize()
        //{
        //    VGridField[] fields = this.SummarizeColumns();
        //    if (fields == null || fields.Length == 0)
        //    {
        //        return;
        //    }
        //    VGridDlg gd = new VGridDlg();
        //    gd.CaptionText = "Summarize Grid";
        //    gd.SelectObject(fields, this.MappingName);
        //    gd.VGrid.SetStyleLayout(this.LayoutManager.Layout);
        //    gd.VGrid.ControlLayout = this.ControlLayout;
        //    gd.VGrid.ReadOnly = true;
        //    gd.Show();
        //}

        //public virtual void PerformFilter()
        //{
        //    if (listManager == null) return;
        //    GridFilterDlg.ShowFilter(this);
        //}

        //public  decimal PerformCompute(AggregateMode mode, string column, decimal defaultValue)
        //{
        //    object o = PerformCompute(mode.ToString() + "(" + column + ")", RowFilter);
        //    return Nistec.Win.Types.ToDecimal(o, defaultValue);
        //}
        //public int PerformCompute(AggregateMode mode, string column, int defaultValue)
        //{
        //    object o = PerformCompute(mode.ToString() + "(" + column + ")", RowFilter);
        //    return Nistec.Win.Types.ToInt(o, defaultValue);
        //}

        //public virtual object PerformCompute(AggregateMode mode, string column)
        //{
        //    return PerformCompute(mode.ToString() + "(" + column + ")", RowFilter);
        //}

        //public virtual object PerformCompute(AggregateMode mode, string column, string filter)
        //{
        //    return PerformCompute(mode.ToString() + "(" + column + ")", filter);
        //}

        //public virtual object PerformCompute(string expression, string filter)
        //{
        //    try
        //    {
        //        if (_DataView == null) return null;
        //        return _DataView.Table.Compute(expression, filter);
        //    }
        //    catch(Exception ex)
        //    {
        //        OnErrorOccouerd("Compute: " + ex.Message);
        //        return "";
        //    }
        //}

        //public virtual void PerformAdjustColumns()
        //{

        //    AdjustColumns();
        //    this.Invalidate();
        //    /*bound*/
        //    //if (!DesignMode && this.myGridTable.GridColumnStyles.Count > 0)
        //    //{
        //    //    AdjustColumns(false, true);
        //    //    this.Invalidate();
        //    //}
        //}

        //public virtual void PerformExport()
        //{

        //    if (_DataView == null) return;

        //    System.Data.DataTable dtExport = _DataView.Table.Copy();
        //    try
        //    {
        //        Nistec.Data.ExportColumnType[] exColumns = PrintGridDataView.CreateExportColumns(this);
        //        Nistec.Data.Export.Export(dtExport, exColumns);
        //    }
        //    catch
        //    {
        //        Nistec.Data.Export.Export(dtExport);
        //    }
        //}
        //public virtual void PerformExport(string filter, string sort, DataViewRowState state)
        //{
        //     if (_DataView == null) return;

        //     System.Data.DataTable dtExport = Nistec.Data.DataUtil.GetFilteredData(_DataView, filter, sort, state);
        //    try
        //    {
        //        Nistec.Data.ExportColumnType[] exColumns = PrintGridDataView.CreateExportColumns(this);
        //        Nistec.Data.Export.Export(dtExport, exColumns);
        //    }
        //    catch
        //    {
        //        Nistec.Data.Export.Export(dtExport);
        //    }
        //}
        //public virtual void PerformExportFiltred()
        //{
        //    if (_DataView == null) return;

        //    System.Data.DataTable dtExport = Nistec.Data.DataUtil.GetFilteredData(_DataView.Table, _DataView.RowFilter, _DataView.Sort, _DataView.RowStateFilter);
        //    try
        //    {
        //        Nistec.Data.ExportColumnType[] exColumns = PrintGridDataView.CreateExportColumns(this);
        //        Nistec.Data.Export.Export(dtExport, exColumns);
        //    }
        //    catch
        //    {
        //        Nistec.Data.Export.Export(dtExport);
        //    }
        //}
        //public virtual void PerformImport()
        //{
        //    try
        //    {
        //        DataSet ds = Nistec.Data.Export.ImportXml();
        //        if (ds == null || ds.Tables.Count == 0)
        //            return;
        //        if (ds.Tables.Count == 1)
        //        {
        //            this.DataSource = ds.Tables[0];
        //        }
        //        else
        //        {
        //            this.DataSource = ds;
        //            this.DataMember = ds.Tables[0].TableName;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Util.MsgBox.ShowError(ex.Message);
        //    }
        //}

        internal virtual void PerformGridContextMenu()
        {
            if (_GridContexMenu == null)
            {
                _GridContexMenu = new GridContexMenu(this);
            }
            this.ContextMenuStrip = _GridContexMenu;
        }

        internal virtual void PerformColumnContextMenu()
        {
            if (_GridColumnContexMenu == null)
            {
                _GridColumnContexMenu = new GridColumnContexMenu(this);
            }
            foreach (GridColumnStyle c in GridColumns)
            {
                //if (c.ColumnType == GridColumnType.TextColumn)
                //{
                //   ((GridTextColumn)c).EditBox.ContextMenuStrip=_GridColumnContexMenu;
                //}
                //else if (c.ColumnType == GridColumnType.ComboColumn)
                //{
                //    ((GridComboColumn)c).EditBox.ContextMenuStrip = _GridColumnContexMenu;
                //}

                if (c.HostControl != null)
                    c.HostControl.ContextMenuStrip = _GridColumnContexMenu;
            }
        }

        //internal void PerformChartAdd(GridChart ctl)
        //{
        //    this.Controls.Add(ctl);
        //    //_GridContexMenu.Items["cmChart"].Text = "Remove Pie Chart";
        //}

        //internal void PerformChartRemove()
        //{
        //    foreach (Control c in this.Controls)
        //    {
        //        if (c is GridChart)
        //        {
        //            this.Controls.Remove(c); 
        //            break;
        //        }
        //    }
        //   // _GridContexMenu.Items["cmChart"].Text = "Grid Pie Chart";
        //}


        #endregion

        #region Staus bar
        /// <summary>
        /// Get Grid StatusBar
        /// </summary>
        [Browsable (false),EditorBrowsable(EditorBrowsableState.Advanced)]
        public GridStatusBar StatusBar
        {
            get
            {
                if (statusBar == null)
                {
                    statusBar = new GridStatusBar();
                    statusBar.Grid = this;
                    statusBar.SizingGrip = false;
                    this.Controls.Add(statusBar);
                    //base.PerformLayout();
                    //base.Invalidate();
                }
                return statusBar;
            }

        }
        /// <summary>
        /// Show StatusBar Columns
        /// </summary>
        /// <param name="visible"></param>
        public void StatusBarInitilaize(bool visible)
        {
            StatusBar.InitilaizeColumns = visible;

        }
        /// <summary>
        /// InitilaizeColumns and Summarize All Columns
        /// </summary>
        public void SummarizeColumns()
        {
            StatusBar.InitilaizeColumns = true;
            StatusBar.SummarizeColumns();
        }
        ///// <summary>
        ///// Summarize Column by column index
        ///// </summary>
        ///// <param name="columnIndex"></param>
        //public void SummarizeColumns(int columnIndex)
        //{
        //    StatusBar.InitilaizeColumns = true;
        //    StatusBar.SummarizeColumns(columnIndex);
        //}
        /// <summary>
        /// InitilaizeColumns and Summarize Columns by array of columns mappingName
        /// </summary>
        /// <param name="mappingNames"></param>
        public void SummarizeColumns(params string[] mappingNames)
        {
            StatusBar.InitilaizeColumns = true;
            if (mappingNames != null)
            {
                StatusBar.AddColumnToSum(mappingNames);
            }
            StatusBar.SummarizeColumns();
        }

        ///// <summary>
        ///// Get Grid Summarize Columns collection
        ///// </summary>
        //public GridStatusPanelCollection GridStatusColumns
        //{
        //    get
        //    {
        //        return StatusBar.GridColumns;
        //    }
        //}

        #endregion

        #region InvokeDataSource

        private delegate DataTable AsyncDataSourceHandler(IDataReader reader, DataTable tblSchema, int fastFirstRows, int maxRows);
        private delegate void SetDataSourceCallBack(object source, bool isBatch, bool isEnd);
        //private bool allowAddLoading;
        //private bool stopLoading;
        private int loadCounter;
        //private bool disableOnLoading;
        //private bool loading;
        private BitArray loadingParam;
        const int loadingRowBatch=100;
        //private DataTable asyncDataSource;

        private BitArray LoadingParam
        {
            get
            {
                if (loadingParam == null)
                {
                    loadingParam = new BitArray(10);
                }
                return loadingParam;
            }
        }

        /// <summary>
        /// Occurs when LoadingDataSource in process
        /// </summary>
        public event EventHandler LoadingDataSource;
        /// <summary>
        /// Occurs when LoadDataSource finshed
        /// </summary>
        public event EventHandler LoadDataSourceEnd;
        /// <summary>
        /// Get Load Counter index
        /// </summary>
        [Browsable(false)]
        public int LoadCounter
        {
            get { return loadCounter; }
        }
        /// <summary>
        /// Get or set value indicating wether the grid will disable on async loading 
        /// </summary>
        public bool DisableOnLoading
        {
            get { return LoadingParam[2]; }
            set { LoadingParam[2] = value; }
        }

        /// <summary>
        /// Stop Async Data Loading
        /// </summary>
        public void StopLoading()
        {
            LoadingParam[1] = true;
        }

        private bool IsLoading()
        {
            return LoadingParam[0] || !this.Enabled;
        }

        private void SetLoading(bool start)
        {

            if (start)
            {
                LoadingParam[0] = true;//loading
                LoadingParam[1] = false;//stopLoading
                //LoadingParam[2] = true;//disableOnLoading
                LoadingParam[3] = this.AllowSorting;
                LoadingParam[4] = this.AllowRemove;
                LoadingParam[5] = this.AllowGridContextMenu;
                LoadingParam[6] = this.AllowColumnContextMenu;
                LoadingParam[7] = this.AllowAdd;

                this.AllowSorting = false;
                this.AllowRemove = false;
                this.AllowGridContextMenu = false;
                this.AllowColumnContextMenu = false;
                this.AllowAdd = false;
            }
            else
            {
                LoadingParam[0] = false;//loading
                //LoadingParam[1] = false;//stopLoading
                //LoadingParam[2] = false;//disableOnLoading
                this.AllowSorting = LoadingParam[3];
                this.AllowRemove = LoadingParam[4];
                this.allowGridContexMenu = LoadingParam[5];
                this.allowColumnContexMenu = LoadingParam[6];
                this.AllowAdd = LoadingParam[7];

                if (LoadingParam[2])
                    this.Enabled = true;

            }
        }

        /// <summary>
        /// Invoke async DataSource
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="provider"></param>
        /// <param name="sql"></param>
        /// <param name="mappingName"></param>
        /// <param name="fastFirstRows"></param>
        /// <param name="maxRows"></param>
        public void InvokeDataSource(string connection,DBProvider provider, string sql, string mappingName, int fastFirstRows, int maxRows)
        {
            if (IsLoading()) return;

            IDbCmd cmd = DbFactory.Create(connection,provider);
            DataTable dtSchema = cmd.Adapter.GetSchemaTable(sql, SchemaType.Source);
            IDataReader reader = cmd.ExecuteReader(sql, CommandBehavior.CloseConnection);
            InvokeDataSource(reader, dtSchema, mappingName, fastFirstRows, maxRows);
        }

        /// <summary>
        /// Invoke async DataSource
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="mappingName"></param>
        /// <param name="fastFirstRows"></param>
        /// <param name="maxRows"></param>
        public void InvokeDataSource(IDbConnection connection, string sql, string mappingName, int fastFirstRows, int maxRows)
        {
            if (IsLoading()) return;
            IDbCmd cmd = DbFactory.Create(connection);
            DataTable dtSchema = cmd.Adapter.GetSchemaTable(sql, SchemaType.Source);
            IDataReader reader = cmd.ExecuteReader(sql, CommandBehavior.Default);
            InvokeDataSource(reader, dtSchema, mappingName, fastFirstRows, maxRows);
        }

        /// <summary>
        /// Invoke async DataSource by IDataReader and DataTable schema
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="tblSchema"></param>
        /// <param name="mappingName"></param>
        /// <param name="fastFirstRows"></param>
        /// <param name="maxRows"></param>
        public void InvokeDataSource(IDataReader reader, DataTable tblSchema, string mappingName, int fastFirstRows, int maxRows)
        {
            if (IsLoading()) return;
            SetLoading(true);
            this.MappingName = mappingName;
            this.dataSource = null;
            //if (this.dataSource != null)
            //{
            //    SetDataSource(null, false);
            //}
            if(DisableOnLoading) Enabled = false;
            AsyncDataSourceHandler handler = new AsyncDataSourceHandler(RunAsyncCall);
            AsyncCallback cb = new AsyncCallback(RunAsyncCallback);
            handler.BeginInvoke(reader, tblSchema, fastFirstRows, maxRows, cb, null);
        }

        /// <summary>
        /// Invoke async DataSource by IDataReader and Grid Columns
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="mappingName"></param>
        /// <param name="fastFirstRows"></param>
        /// <param name="maxRows"></param>
        public void InvokeDataSource(IDataReader reader, string mappingName, int fastFirstRows, int maxRows)
        {

            if (IsLoading()) return;
            if (this.Columns.Count == 0)
            {
                throw new ArgumentException("Invalid Columns ");
            }
            DataTable tblSchema = GetSechemaFromColumns();

            SetLoading(true);
            this.MappingName = mappingName;
            this.dataSource = null;
            //if (this.dataSource != null)
            //{
            //    SetDataSource(null, false);
            //}
            if (DisableOnLoading) Enabled = false;
            AsyncDataSourceHandler handler = new AsyncDataSourceHandler(RunAsyncCall);
            AsyncCallback cb = new AsyncCallback(RunAsyncCallback);
            handler.BeginInvoke(reader, tblSchema, fastFirstRows, maxRows, cb, null);
        }

        /// <summary>
        /// Invoke sync DataSource by IDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="mappingName"></param>
        public void InvokeDataSource(IDataReader reader, string mappingName)
        {
            if (IsLoading()) return;
            DataTable tblSchema = new DataTable();
            SetLoading(true);
            this.MappingName = mappingName;
            this.dataSource = null;
            //if (this.dataSource != null)
            //{
            //    SetDataSource(null, false);
            //}
            if (DisableOnLoading) Enabled = false;
            AsyncDataSourceHandler handler = new AsyncDataSourceHandler(RunAsyncCall);
            AsyncCallback cb = new AsyncCallback(RunAsyncCallback);
            handler.BeginInvoke(reader, tblSchema, 0, 0, cb, null);
        }

        private DataTable GetSechemaFromColumns()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < Columns.Count; i++)
            {
                if (Columns[i].IsBound)
                {
                    dt.Columns.Add(Columns[i].MappingName);
                }
            }
            return dt;
        }

        private void RunAsyncCallback(IAsyncResult ar)
        {
            try
            {
                System.Threading.Thread th = System.Threading.Thread.CurrentThread;
                AsyncDataSourceHandler handler = (AsyncDataSourceHandler)((System.Runtime.Remoting.Messaging.AsyncResult)ar).AsyncDelegate;
                //SetDataSource(handler.EndInvoke(ar), true);
                ////this.AllowAdd = allowAddLoading;
                //if (LoadDataSourceEnd != null)
                //    LoadDataSourceEnd(this, EventArgs.Empty);

                DataTable asyncDataSource = handler.EndInvoke(ar);

                OnAsyncDataSourceEnd(asyncDataSource, EventArgs.Empty);
            }
            catch (System.TimeoutException ex)
            {
                MsgBox.ShowError(ex.Message);
            }
            finally
            {
                LoadingParam[0] = false;//loading
            }
        }

        private DataTable RunAsyncCall(IDataReader reader, DataTable tbl, int fastFirstRows, int maxRows)
        {
            bool hasTableEndLoading = false;
            LoadingParam[1] = false;//stopLoading
            LoadingParam[0] = true;//loading

            try
            {
                tbl.TableName = this.MappingName;


                if (fastFirstRows == 0)
                {
                    tbl.Load(reader, LoadOption.Upsert);
                }
                else
                {
                    loadCounter = 0;
                    hasTableEndLoading = true;
                    int fieldsCount = reader.FieldCount;
                    object[] values = new object[fieldsCount];
                    tbl.BeginLoadData();
                    while (!LoadingParam[1] && reader.Read() && ((loadCounter < maxRows) || (maxRows == 0)))
                    {
                        values.Initialize();
                        for (int i = 0; i < fieldsCount; i++)
                        {
                            values[i] =(object) reader[i];
                        }

                        tbl.LoadDataRow(values, LoadOption.Upsert);
                        if (loadCounter == fastFirstRows)
                        {
                            SetDataSource(tbl,false,false);
                        }
                        if (loadCounter > fastFirstRows && loadCounter % loadingRowBatch == 0)
                        {
                            SetDataSource(tbl,true, false);
                            OnLoadingDataSource(EventArgs.Empty);
                        }
                        loadCounter++;
                    }

                }
                //hasTableEndLoading = true;
            }
            catch (OperationCanceledException ex)
            {
                OnErrorOccouerd(ex.Message);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                OnErrorOccouerd(ex.Message);
            }
            finally
            {

                try
                {
                    if (hasTableEndLoading)
                    {
                        tbl.EndLoadData();
                    }
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                catch (Exception e)
                {
                    OnErrorOccouerd(e.Message);
                }
                LoadingParam[0] = false;//loading

            }
            return tbl;
        }

        /// <summary>
        /// OnLoadingDataSource
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLoadingDataSource(EventArgs e)
        {
            if (LoadingDataSource != null)
                LoadingDataSource(this, e);
        }

        /// <summary>
        /// Invoke sync DataSource
        /// </summary>
        /// <param name="source"></param>
        /// <param name="isEnd"></param>
        public void InvokeDataSource(object source, bool isEnd)
        {
            if (IsLoading()) return;
            SetLoading(true);
            SetDataSource(source,false, isEnd);
        }
        /// <summary>
        /// Invoke sync DataSource
        /// </summary>
        /// <param name="source"></param>
        public void InvokeDataSource(object source)
        {
            if (IsLoading()) return;
            SetLoading(true);
            SetDataSource(source, false, false);
        }
        private void SetDataSource(object source,bool isBatch, bool isEnd)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceCallBack(SetDataSource), source,isBatch, isEnd);
            }
            else
            {
                if(isBatch)
                    this.ReBinding(source);
                else
                    this.SetDataBinding(source, this.DataMember);

                if (isEnd)
                {
                    SetLoading(false);
                }
            }
        }
        /// <summary>
        /// OnAsyncDataSourceEnd
        /// </summary>
        /// <param name="asyncDataSource"></param>
        /// <param name="e"></param>
        protected virtual void OnAsyncDataSourceEnd(object asyncDataSource,EventArgs e)
        {
            SetDataSource(asyncDataSource,false, true);
            //this.AllowAdd = allowAddLoading;
            if (LoadDataSourceEnd != null)
                LoadDataSourceEnd(this, EventArgs.Empty);

        }

       #endregion

        #region Methods

        private void AbortEdit()
        {
            this.gridState[0x10000] = true;
            this.editColumn.Abort(this.editRow.RowNumber);
            this.gridState[0x10000] = false;
            this.gridState[0x8000] = false;
            this.editRow = null;
            this.editColumn = null;
            this.OnDirtyRow(GridDirtyRowState.Cancel);
            OnNewRowChanged(GridNewRowState.Cancel);
         }

        //private void CellBoundAdd()
        //{
        //    for (int num1 = 0; num1 < this.myGridTable.GridColumnStyles.Count; num1++)
        //    {
        //        GridColumnStyle style1 = this.myGridTable.GridColumnStyles[num1];
        //        if (!style1.isBound)
        //            style1.CellBoundAdd();
        //    }
        //}
        //private void CellBoundRemove(int row)
        //{
        //    for (int num1 = 0; num1 < this.myGridTable.GridColumnStyles.Count; num1++)
        //    {
        //        GridColumnStyle style1 = this.myGridTable.GridColumnStyles[num1];
        //        if (!style1.isBound)
        //            style1.CellBoundRemove(row);
        //    }
        //}

         //private System.Collections.Generic.List<GridRow> GridRowsList;

  
        //public void BeginLoad()
        //{
        //    GridRowsList = new System.Collections.Generic.List<GridRow>();
        //    this.EnsureBound();
        //    this.ResetSelection();
        //    this.UpdateListManager();
        //    this.gridState[0x200] = true;
        //    this.gridState[0x100000] = true;

        //}

        //public void EndLoad()
        //{
        //    this.gridState[0x200] = false;
        //    GridRowsList = null;
        //}

        //public void LoadRowBinding(IDataRecord reader)
        //{
        //    BindManager manager1 = this.ListManager;
        //    GridTableStyle style1 = this.myGridTable;
        //    //this.InitializeColumnWidths();
        //    if (manager1 == null)
        //    {
        //        //this.SetGridRows(new GridRow[0], 0);
        //        return;
        //    }
        //    else
        //    {
        //        int count = manager1.Count;
        //        if (this.policy.AllowAdd)
        //        {
        //            count++;
        //        }
        //        GridRow[] rowArray1 = new GridRow[count];
        //        for (int num2 = 0; num2 < manager1.Count; num2++)
        //        {
        //            rowArray1[num2] = new GridRelationshipRow(this, style1, num2);
        //        }
        //        if (this.policy.AllowAdd)
        //        {
        //            this.addNewRow = new GridAddNewRow(this, style1, count - 1);
        //            rowArray1[count - 1] = this.addNewRow;
        //        }
        //        else
        //        {
        //            this.addNewRow = null;
        //        }
        //        //this.SetGridRows(rowArray1, num1);
        //        GridRowsList.Add(addNewRow);

        //        //this.SetGridRows(rowArray1, num1);
        //        this.dataGridRows = GridRowsList.ToArray();// newRows;
        //        this.dataGridRowsLength = count;
        //        this.vertScrollBar.Maximum = Math.Max(0, this.GridRowsLength - 1);

        //        int rowIndex = count - 1;
        //        this.ListManager.AddNew();

        //        //this.ListManager.Position = rowIndex;
        //        for (int columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++)
        //        {
        //            this.myGridTable.GridColumnStyles[columnIndex].SetColumnValue(manager1, rowIndex, reader[columnIndex]);
        //        }
        //        this.ListManager.EndCurrentEdit();
        //        //this.SetGridRows(GridRowsList.ToArray(),count);

        //        this.dataGridRows = GridRowsList.ToArray();
        //        this.dataGridRowsLength = count;
        //        this.vertScrollBar.Maximum = Math.Max(0, this.GridRowsLength - 1);
        //        //if (this.firstVisibleRow > newRowsLength)
        //        //{
        //          //  this.vertScrollBar.Value = 0;
        //           // this.firstVisibleRow = 0;
        //        //}

        //        this.gridState[0x80000] = false;
        //        this.ResetSelection();
        //        //this.ResetMouseState();
        //        //base.PerformLayout();
               
        //        base.Invalidate();
   
        //    }

        //}

        //public void LoadRowBinding1(IDataRecord reader)
        //{
        //    if (!this.policy.AllowAdd)
        //        return;

        //    try
        //    {

        //        BindManager manager1 = this.ListManager;
        //        int count = manager1.Count;
        //        count++;
        //        this.ListManager.AddNew();

        //        //AddNewRow();

        //        //GridRow[] newRows = new GridRow[count];
        //        //for (int i = 0; i < manager1.Count; i++)
        //        //{
        //        //    newRows[i] = new GridRelationshipRow(this, style1, i);
        //        //}

        //        this.addNewRow = new GridAddNewRow(this, myGridTable, count - 1);
        //        //newRows[count - 1] = this.addNewRow;
        //        GridRowsList.Add(addNewRow);

        //        //this.SetGridRows(rowArray1, num1);
        //        this.dataGridRows = GridRowsList.ToArray();// newRows;
        //        this.dataGridRowsLength = count;
        //        this.vertScrollBar.Maximum = Math.Max(0, this.GridRowsLength - 1);
                
        //        int rowIndex = count - 1;
        //        for (int columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++)
        //        {
        //            this.myGridTable.GridColumnStyles[columnIndex].SetColumnValue(manager1, rowIndex, reader[columnIndex]);
        //        }

        //        this.ListManager.EndCurrentEdit();
  
        //                  this.EnsureBound();
        //    this.ResetSelection();
        //    this.UpdateListManager();
        //    this.gridState[0x200] = true;
        //    this.gridState[0x100000] = true;
        //    this.ListManager.Position = rowIndex;
           
        //        this.ListManager.AddNew();
  
                
               
        //        //this.ListManager.EndCurrentEdit();
        //        //    this.AddNewRow();
        //         //this.ListManager.Position = rowIndex;

        //        //this.InvalidateRow(rowIndex);
                        
        //        //CommitEdit();
        //        //this.ListManager.Refresh();
        //        //RefreshCurrent(rowIndex);
        //        //GoToCell(count, 0);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        internal void AddNewRow()
        {
            this.EnsureBound();
            this.ResetSelection();
            this.UpdateListManager();
            this.gridState[0x200] = true;
            this.gridState[0x100000] = true;
            try
            {
                //CellBoundAdd();
                this.ListManager.AddNew();
                //OnNewRowAdded(EventArgs.Empty);
                //rowNew = true;
            }
            catch (Exception exception1)
            {
                this.gridState[0x200] = false;
                this.gridState[0x100000] = false;
                base.PerformLayout();
                this.InvalidateInside();
                throw exception1;
            }
            this.gridState[0x200] = false;
        }

        private void AllowSortingChanged(object sender, EventArgs e)
        {
            if (!this.myGridTable.AllowSorting && (this.listManager != null))
            {
                IList list1 = this.ListManager.List;
                if (list1 is IBindingList)
                {
                    ((IBindingList)list1).RemoveSort();
                }
            }
        }
        /// <summary>
        /// Attempts to put the grid into a state where editing is allowed
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public bool BeginEdit(GridColumnStyle gridColumn, int rowNumber)
        {
            if ((this.DataSource == null) || (this.myGridTable == null))
            {
                return false;
            }
            if (this.gridState[0x8000])
            {
                return false;
            }
            int num1 = -1;
            num1 = this.myGridTable.GridColumnStyles.IndexOf(gridColumn);
            if (num1 < 0)
            {
                return false;
            }
            this.CurrentCell = new GridCell(rowNumber, num1);
            this.ResetSelection();
            this.Edit();
            return true;
        }


        private Rectangle CalcColResizeFeedbackRect(MouseEventArgs e)
        {
            Rectangle rectangle1 = this.layout.Data;
            Rectangle rectangle2 = new Rectangle(e.X, rectangle1.Y, 3, rectangle1.Height);
            rectangle2.X = Math.Min(rectangle1.Right - 3, rectangle2.X);
            rectangle2.X = Math.Max(rectangle2.X, 0);
            return rectangle2;
        }


        private Rectangle CalcRowResizeFeedbackRect(MouseEventArgs e)
        {
            Rectangle rectangle1 = this.layout.Data;
            Rectangle rectangle2 = new Rectangle(rectangle1.X, e.Y, rectangle1.Width, 3);
            rectangle2.Y = Math.Min(rectangle1.Bottom - 3, rectangle2.Y);
            rectangle2.Y = Math.Max(rectangle2.Y, 0);
            return rectangle2;
        }


        private void CancelCursorUpdate()
        {
            if (this.listManager != null)
            {
                this.EndEdit();
                this.listManager.CancelCurrentEdit();
            }
        }
        /// <summary>
        /// Cancels the current edit operation and rolls back all changes. 
        /// </summary>
        protected virtual void CancelEditing()
        {
            OnNewRowChanged(GridNewRowState.Cancel);
            OnDirtyRow(GridDirtyRowState.Cancel);// this.rowDirty = false;//OnDirtyRow(false);
            this.CancelCursorUpdate();
            if (this.gridState[0x100000])
            {
                this.gridState[0x100000] = false;
                GridRow[] rowArray1 = this.GridRows;
                rowArray1[this.GridRowsLength - 1] = new GridAddNewRow(this, this.myGridTable, this.GridRowsLength - 1);
                this.SetGridRows(rowArray1, this.GridRowsLength);
            }
        }

        private void CheckHierarchyState()
        {
            if (((this.checkHierarchy && (this.ListManager != null)) && (this.myGridTable != null)) && (this.myGridTable != null))
            {
                for (int num1 = 0; num1 < this.myGridTable.GridColumnStyles.Count; num1++)
                {
                    GridColumnStyle style1 = this.myGridTable.GridColumnStyles[num1];
                }
                this.checkHierarchy = false;
            }
        }

        private void ClearRegionCache()
        {
            this.cachedScrollableRegion = null;
        }


        private void ColAutoResize(int col)
        {
            this.EndEdit();
            BindManager manager1 = this.ListManager;
            if (manager1 != null)
            {
                Graphics graphics1 = this.CreateGraphicsInternal();
                try
                {
                    Font font1;
                    GridColumnStyle style1 = this.myGridTable.GridColumnStyles[col];
                    string text1 = style1.HeaderText;
                    if (this.myGridTable.IsDefault)
                    {
                        font1 = this.HeaderFont;
                    }
                    else
                    {
                        font1 = this.myGridTable.dataGrid.HeaderFont;
                    }
                    SizeF ef1 = graphics1.MeasureString(text1, font1);
                    int num1 = (((int)ef1.Width) + this.layout.ColumnHeaders.Height) + 1;
                    int num2 = manager1.Count;
                    for (int num3 = 0; num3 < num2; num3++)
                    {
                        object obj1 = style1.GetColumnValueAtRow(manager1, num3);
                        Size size1 = style1.GetPreferredSize(graphics1, obj1);
                        int num4 = size1.Width;
                        if (num4 > num1)
                        {
                            num1 = num4;
                        }
                    }
                    if (style1.Width != num1)
                    {
                        style1.width = num1;
                        base.PerformLayout();
                        Rectangle rectangle1 = this.layout.Data;
                        if (this.layout.ColumnHeadersVisible)
                        {
                            rectangle1 = Rectangle.Union(rectangle1, this.layout.ColumnHeaders);
                        }
                        int num5 = this.GetColBeg(col);
                        rectangle1.Width -= rectangle1.X - num5;
                        rectangle1.X = num5;
                        base.Invalidate(rectangle1);
                    }
                }
                finally
                {
                    graphics1.Dispose();
                }
            }
        }
        /// <summary>
        /// Collapses child relations, if any exist for all rows, or for a specified row.
        /// </summary>
        /// <param name="row"></param>
        public void Collapse(int row)
        {
            this.SetRowExpansionState(row, false);
        }


        private void ColResizeBegin(MouseEventArgs e, int col)
        {
            int num1 = e.X;
            this.EndEdit();
            Rectangle rectangle1 = Rectangle.Union(this.layout.ColumnHeaders, this.layout.Data);
            if (this.isRightToLeft())
            {
                rectangle1.Width = (this.GetColBeg(col) - this.layout.Data.X) - 2;
            }
            else
            {
                int num2 = this.GetColBeg(col);
                rectangle1.X = num2 + 3;
                rectangle1.Width = ((this.layout.Data.X + this.layout.Data.Width) - num2) - 2;
            }
            IntSecurity.AdjustCursorClip.Assert();
            try
            {
                this.CaptureInternal = true;
                Cursor.Clip = base.RectangleToScreen(rectangle1);
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
            this.gridState[8] = true;
            this.trackColAnchor = num1;
            this.trackColumn = col;
            this.DrawColSplitBar(e);
            this.lastSplitBar = e;
        }


        private void ColResizeEnd(MouseEventArgs e)
        {
            try
            {
                //				if (this.lastSplitBar != null)
                //				{
                //					this.DrawColSplitBar(this.lastSplitBar);
                //					this.lastSplitBar = null;
                //				}
                bool flag1 = this.isRightToLeft();
                int num1 = flag1 ? Math.Max(e.X, this.layout.Data.X) : Math.Min(e.X, this.layout.Data.Right + 1);
                int num2 = num1 - this.GetColEnd(this.trackColumn);
                if (flag1)
                {
                    num2 = -num2;
                }
                if ((this.trackColAnchor != num1) && (num2 != 0))
                {
                    GridColumnStyle style1 = this.myGridTable.GridColumnStyles[this.trackColumn];
                    int width = style1.Width + num2;
                    width = Math.Max(width, 3);
                    style1.Width = width;
                    base.PerformLayout();
                    Rectangle rectangle1 = Rectangle.Union(this.layout.ColumnHeaders, this.layout.Data);
                    int num4 = this.GetColBeg(this.trackColumn);
                    rectangle1.Width -= flag1 ? (rectangle1.Right - num4) : (num4 - rectangle1.X);
                    rectangle1.X = flag1 ? this.layout.Data.X : num4;
                    base.Invalidate(rectangle1);
                    if(ColumnResize!=null)
                        ColumnResize(this,new ColumnResizeEventArgs(width,this.trackColumn));
                }
               
            }
            finally
            {
                Cursor.Clip = Rectangle.Empty;
                this.CaptureInternal = false;
                if (this.autoAdjust)
                {
                    OnResizeAdjustColumns();
                }
            }
        }

        private void ColResizeMove(MouseEventArgs e)
        {
            if (this.lastSplitBar != null)
            {
                this.DrawColSplitBar(this.lastSplitBar);
                this.lastSplitBar = e;
            }
            this.DrawColSplitBar(e);
        }
        /// <summary>
        /// Sort Grid by specified column
        /// </summary>
        /// <param name="columnIndex"></param>
        public void SortGrid(int columnIndex)
        {

            if (columnIndex < 0 || columnIndex >= this.myGridTable.GridColumnStyles.Count)
            {
                throw new ArgumentOutOfRangeException("Column Index is out of range ", columnIndex.ToString());
            }
            if (this.CommitEdit())
            {
                bool flag1;
                PropertyDescriptor prop = this.myGridTable.GridColumnStyles[columnIndex].PropertyDescriptor;
                if (this.myGridTable.IsDefault)
                {
                    flag1 = this.AllowSorting;
                }
                else
                {
                    flag1 = this.myGridTable.AllowSorting;
                }
                if (flag1)
                {
                    if (RowCount > 100000)
                    {
                        if (MsgDlg.ShowDialog(this.StylePainter, "Sorting may take a long time, Continue ? ", "Grid View", Buttons.YesNo) == DialogResult.No)
                            return;
                    }

                    ListSortDirection direction1 = this.ListManager.GetSortDirection();
                    PropertyDescriptor descriptor1 = this.ListManager.GetSortProperty();
                    if ((descriptor1 != null) && descriptor1.Equals(prop))
                    {
                        direction1 = (direction1 == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    }
                    else
                    {
                        direction1 = ListSortDirection.Ascending;
                    }
                    if (this.listManager.Count != 0 && prop != null /*bound*/)
                    {
                        if (RowCount > 10000)
                        {
                            this.BeginInvoke(new SortCallBack(AsyncSort), prop, direction1);
                        }
                        else
                        {
                            this.ListManager.SetSort(prop, direction1);
                            this.ResetSelection();
                            this.InvalidateInside();
                        }
                    }
                }
            }
        }

        private delegate void SortCallBack (PropertyDescriptor prop, ListSortDirection dir);

        private void AsyncSort(PropertyDescriptor prop, ListSortDirection dir)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SortCallBack(AsyncSort), prop, dir);
            }
            else
            {
                this.ListManager.SetSort(prop, dir);
                this.ResetSelection();
                this.InvalidateInside();
            }
        }


        private void ColumnHeaderClicked(PropertyDescriptor prop)
        {
            if (this.CommitEdit())
            {
                bool flag1;
                if (this.myGridTable.IsDefault)
                {
                    flag1 = this.AllowSorting;
                }
                else
                {
                    flag1 = this.myGridTable.AllowSorting;
                }
                if (flag1)
                {
                    if (RowCount > 100000)
                    {
                        if (MsgDlg.ShowDialog(this.StylePainter, "Sorting may take a long time, Continue ? ", "Grid View", Buttons.YesNo) == DialogResult.No)
                            return;
                    }
                    ListSortDirection direction1 = this.ListManager.GetSortDirection();
                    PropertyDescriptor descriptor1 = this.ListManager.GetSortProperty();
                    if ((descriptor1 != null) && descriptor1.Equals(prop))
                    {
                        direction1 = (direction1 == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    }
                    else
                    {
                        direction1 = ListSortDirection.Ascending;
                    }
                    if (this.listManager.Count != 0 && prop != null /*bound*/)
                    {
                        if (RowCount > 10000)
                        {
                            this.BeginInvoke(new SortCallBack(AsyncSort), prop, direction1);
                        }
                        else
                        {
                            this.ListManager.SetSort(prop, direction1);
                            this.ResetSelection();
                            this.InvalidateInside();
                        }
                    }
                }
            }
        }


        private void ColumnHeadersVisibleChanged(object sender, EventArgs e)
        {
            this.layout.ColumnHeadersVisible = (this.myGridTable != null) && this.myGridTable.dataGrid.ColumnHeadersVisible;
            base.PerformLayout();
            this.InvalidateInside();
        }
        /// <summary>
        /// Informs the Grid control that the user has begun editing a column
        /// </summary>
        /// <param name="bounds"></param>
        protected internal virtual void ColumnStartedEditing(Rectangle bounds)
        {
            this.OnDirtyRow(GridDirtyRowState.Editing);
            GridRow[] rowArray1 = this.GridRows;
            if (this.gridState[0x100000])
            {
                int num1 = this.GridRowsLength;
                GridRow[] rowArray2 = new GridRow[num1 + 1];
                for (int num2 = 0; num2 < num1; num2++)
                {
                    rowArray2[num2] = rowArray1[num2];
                }
                rowArray2[num1] = new GridAddNewRow(this, this.myGridTable, num1);
                this.SetGridRows(rowArray2, num1 + 1);
                this.Edit();
                this.gridState[0x100000] = false;
                this.gridState[0x8000] = true;
                this.gridState[0x4000] = false;
            }
            else
            {
                this.gridState[0x8000] = true;
                this.gridState[0x4000] = false;
                this.InvalidateRowHeader(this.currentRow);
                rowArray1[this.currentRow].LoseChildFocus(this.layout.RowHeaders, this.isRightToLeft());
            }
        }

        /// <summary>
        /// Informs the Grid control that the user has begun editing a column
        /// </summary>
        /// <param name="editingControl"></param>
        protected internal virtual void ColumnStartedEditing(Control editingControl)
        {
            this.ColumnStartedEditing(editingControl.Bounds);
        }


        private bool CommitEdit()
        {
            if ((!this.gridState[0x8000] && !this.gridState[0x4000]) || this.gridState[0x10000])
            {
                return true;
            }
            this.gridState[0x10000] = true;
            if (this.editColumn.ReadOnly || this.gridState[0x100000])
            {
                if (this.gridState[0x800])
                {
                    this.FocusInternal();
                }
                this.editColumn.ConcedeFocus();
                if ((this.gridState[0x800] && base.CanFocus) && !this.Focused)
                {
                    this.FocusInternal();
                }
                this.gridState[0x10000] = false;
                return true;
            }
            bool flag1 = this.editColumn.Commit(this.ListManager, this.currentRow);
            this.gridState[0x10000] = false;
            if (flag1)
            {
                this.gridState[0x8000] = false;
            }
            return flag1;
        }


        private int ComputeColumnDelta(int from, int to)
        {
            int num1 = from;
            int num2 = to;
            int num3 = -1;
            if (num1 > num2)
            {
                num1 = to;
                num2 = from;
                num3 = 1;
            }
            int num4 = 0;
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            for (int num5 = num1; num5 < num2; num5++)
            {
                if (collection1[num5].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                {
                    num4 += collection1[num5].Width;
                }
            }
            return (num3 * num4);
        }

        private int ComputeDeltaRows(int targetRow)
        {
            if (this.firstVisibleRow == targetRow)
            {
                return 0;
            }
            int num2 = -1;
            int num3 = -1;
            int num4 = this.GridRowsLength;
            int num5 = 0;
            GridRow[] rowArray1 = this.GridRows;
            for (int num6 = 0; num6 < num4; num6++)
            {
                if (num6 == this.firstVisibleRow)
                {
                    num2 = num5;
                }
                if (num6 == targetRow)
                {
                    num3 = num5;
                }
                if ((num3 != -1) && (num2 != -1))
                {
                    break;
                }
                num5 += rowArray1[num6].Height;
            }
            int num7 = num3 + rowArray1[targetRow].Height;
            int num8 = this.layout.Data.Height + num2;
            if (num7 > num8)
            {
                int num9 = num7 - num8;
                num2 += num9;
            }
            else
            {
                if (num2 < num3)
                {
                    return 0;
                }
                int num10 = num2 - num3;
                num2 -= num10;
            }
            int num11 = this.ComputeFirstVisibleRow(num2);
            return (num11 - this.firstVisibleRow);
        }


        private int ComputeFirstVisibleColumn()
        {
            int num1 = 0;
            if (this.horizontalOffset == 0)
            {
                this.negOffset = 0;
                return 0;
            }
            if (((this.myGridTable != null) && (this.myGridTable.GridColumnStyles != null)) && (this.myGridTable.GridColumnStyles.Count != 0))
            {
                GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
                int num2 = 0;
                int num3 = collection1.Count;
                if (collection1[0].Width == -1)
                {
                    this.negOffset = 0;
                    return 0;
                }
                num1 = 0;
                while (num1 < num3)
                {
                    if (collection1[num1].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                    {
                        num2 += collection1[num1].Width;
                    }
                    if (num2 > this.horizontalOffset)
                    {
                        break;
                    }
                    num1++;
                }
                if (num1 == num3)
                {
                    this.negOffset = 0;
                    return 0;
                }
                this.negOffset = collection1[num1].Width - (num2 - this.horizontalOffset);
            }
            return num1;
        }

        private int ComputeFirstVisibleRow(int firstVisibleRowLogicalTop)
        {
            int num2 = this.GridRowsLength;
            int num3 = 0;
            GridRow[] rowArray1 = this.GridRows;
            int num1 = 0;
            while (num1 < num2)
            {
                if (num3 >= firstVisibleRowLogicalTop)
                {
                    return num1;
                }
                num3 += rowArray1[num1].Height;
                num1++;
            }
            return num1;
        }


        private void ComputeLayout()
        {
            bool ltr = !this.isRightToLeft();
            Rectangle rectangle1 = this.layout.ResizeBoxRect;
            this.EndEdit();
            this.ClearRegionCache();
            Grid.LayoutData data1 = new Grid.LayoutData(this.layout);
            data1.Inside = base.ClientRectangle;
            Rectangle rectInside = data1.Inside;
            int borderW = this.BorderWidth;
            rectInside.Inflate(-borderW, -borderW);
            Rectangle rectData = rectInside;
            if (this.layout.CaptionVisible)
            {
                int num2 = this.captionFontHeight + 6;
                Rectangle rectCaption = data1.Caption;
                rectCaption = rectData;
                rectCaption.Height = num2;
                rectData.Y += num2;
                rectData.Height -= num2;
                data1.Caption = rectCaption;
            }
            else
            {
                data1.Caption = Rectangle.Empty;
            }
            if (this.layout.StatusBarVisible)
            {
                //int statusHeight = DefaultStatusBarHeight;
                //Rectangle rectStatus = data1.StatusBar;
                //rectStatus = rectData;
                //rectStatus.Height = statusHeight;
                //rectStatus.Y = rectData.Height-statusHeight;
                rectData.Height -= DefaultStatusBarHeight;
                //data1.StatusBar = rectStatus;
  
            }
            else
            {
                //data1.StatusBar = Rectangle.Empty;
            }
            //			if (this.layout.ParentRowsVisible)
            //			{
            //				Rectangle rectangle5 = data1.ParentRows;
            //				int num3 = this.parentRows.Height;
            //				rectangle5 = rectData;
            //				rectangle5.Height = num3;
            //				rectData.Y += num3;
            //				rectData.Height -= num3;
            //				data1.ParentRows = rectangle5;
            //			}
            //			else
            //			{
            //				data1.ParentRows = Rectangle.Empty;
            //			}
            int num4 = this.headerFontHeight + hMargin;//6;
            if (this.layout.ColumnHeadersVisible)
            {
                Rectangle colHrect = data1.ColumnHeaders;
                colHrect = rectData;
                colHrect.Height = num4;
                rectData.Y += num4;
                rectData.Height -= num4;
                data1.ColumnHeaders = colHrect;
            }
            else
            {
                data1.ColumnHeaders = Rectangle.Empty;
            }
            bool rowHvisible = this.RowHeadersVisible;// this.myGridTable.IsDefault ? this.RowHeadersVisible : this.myGridTable.RowHeadersVisible;
            int rowHwidth = this.RowHeaderWidth;// this.myGridTable.IsDefault ? this.RowHeaderWidth : this.myGridTable.RowHeaderWidth;
            data1.RowHeadersVisible = rowHvisible;
            if ((this.myGridTable != null) && rowHvisible)
            {
                Rectangle rowHrect = data1.RowHeaders;
                if (ltr)
                {
                    rowHrect = rectData;
                    rowHrect.Width = rowHwidth;
                    rectData.X += rowHwidth;
                    rectData.Width -= rowHwidth;
                }
                else
                {
                    rowHrect = rectData;
                    rowHrect.Width = rowHwidth;
                    rowHrect.X = rectData.Right - rowHwidth;
                    rectData.Width -= rowHwidth;
                }
                data1.RowHeaders = rowHrect;
                if (this.layout.ColumnHeadersVisible)
                {
                    Rectangle rectangle8 = data1.TopLeftHeader;
                    Rectangle rectangle9 = data1.ColumnHeaders;
                    if (ltr)
                    {
                        rectangle8 = rectangle9;
                        rectangle8.Width = rowHwidth;
                        rectangle9.Width -= rowHwidth;
                        rectangle9.X += rowHwidth;
                    }
                    else
                    {
                        rectangle8 = rectangle9;
                        rectangle8.Width = rowHwidth;
                        rectangle8.X = rectangle9.Right - rowHwidth;
                        rectangle9.Width -= rowHwidth;
                    }
                    data1.TopLeftHeader = rectangle8;
                    data1.ColumnHeaders = rectangle9;
                }
                else
                {
                    data1.TopLeftHeader = Rectangle.Empty;
                }
            }
            else
            {
                data1.RowHeaders = Rectangle.Empty;
                data1.TopLeftHeader = Rectangle.Empty;
            }
            data1.Data = rectData;
            data1.Inside = rectInside;
            this.layout = data1;
            this.LayoutScrollBars();
            if (!rectangle1.Equals(this.layout.ResizeBoxRect) && !this.layout.ResizeBoxRect.IsEmpty)
            {
                base.Invalidate(this.layout.ResizeBoxRect);
            }
            this.layout.dirty = false;
        }

        internal int GetMinimumRowHeight()
        {
            if (minRowHeight == 0)
            {
                ComuteMinimumRowHeight();
            }
            return minRowHeight;
        }

        private void ComuteMinimumRowHeight()
        {
            //this.minRowHeight = Grid.DefaultRowHeight;
            int num1 = this.PreferredRowHeight ;
            try
            {
                if (this.DataSource == null)
                {
                    minRowHeight = num1;
                }
                GridColumnCollection columns = myGridTable.GridColumnStyles;
                int num2 =columns.Count;
                for (int num3 = 0; num3 < num2; num3++)
                {
                    if (columns[num3].PropertyDescriptor != null)
                    {
                        num1 = Math.Max(num1, columns[num3].GetMinimumHeight());
                    }
                }
                minRowHeight = num1;
            }
            catch 
            {
                minRowHeight = this.PreferredRowHeight;
            }
            //return minRowHeight;

        }

        internal void ComputeMinimumRowHeaderWidth()
        {
            this.minRowHeaderWidth = 0;
            this.minRowHeaderWidth = 15;
            if (this.ListHasErrors)
            {
                this.minRowHeaderWidth += 15;
            }
            //			if ((this.myGridTable != null) && (this.myGridTable.RelationsList.Count != 0))
            //			{
            //				this.minRowHeaderWidth += 15;
            //			}
        }

        private int ComputeRowDelta(int from, int to)
        {
            int num1 = from;
            int num2 = to;
            int num3 = -1;
            if (num1 > num2)
            {
                num1 = to;
                num2 = from;
                num3 = 1;
            }
            GridRow[] rowArray1 = this.GridRows;
            int num4 = 0;
            for (int num5 = num1; num5 < num2; num5++)
            {
                num4 += rowArray1[num5].Height;
            }
            return (num3 * num4);
        }

        private void ComputeVisibleColumns()
        {
            this.EnsureBound();
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            int num1 = collection1.Count;
            int num2 = -this.negOffset;
            int num3 = 0;
            int num4 = this.layout.Data.Width;
            int num5 = this.firstVisibleCol;
            if ((num4 >= 0) && (collection1.Count != 0))
            {
                while ((num2 < num4) && (num5 < num1))
                {
                    if (collection1[num5].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                    {
                        num2 += collection1[num5].Width;
                    }
                    num5++;
                    num3++;
                }
                this.numVisibleCols = num3;
                if (num2 >= num4)
                {
                    goto Label_0137;
                }
                for (int num6 = this.firstVisibleCol - 1; num6 > 0; num6--)
                {
                    if ((num2 + collection1[num6].Width) > num4)
                    {
                        break;
                    }
                    if (collection1[num6].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                    {
                        num2 += collection1[num6].Width;
                    }
                    num3++;
                    this.firstVisibleCol--;
                }
            }
            else
            {
                this.numVisibleCols = this.firstVisibleCol = this.lastTotallyVisibleCol = 0;
                return;
            }
            if (this.numVisibleCols != num3)
            {
                base.Invalidate(this.layout.Data);
                base.Invalidate(this.layout.ColumnHeaders);
                this.numVisibleCols = num3;
            }
        Label_0137:
            this.lastTotallyVisibleCol = (this.firstVisibleCol + this.numVisibleCols) - 1;
            if (num2 > this.layout.Data.Width)
            {
                this.lastTotallyVisibleCol--;
            }
        }

        private void ComputeVisibleRows()
        {
            this.EnsureBound();
            int num1 = this.layout.Data.Height;
            int num2 = 0;
            int num3 = 0;
            GridRow[] rowArray1 = this.GridRows;
            int num4 = this.GridRowsLength;
            if (num1 < 0)
            {
                this.numVisibleRows = this.numTotallyVisibleRows = 0;
            }
            else
            {
                for (int num5 = this.firstVisibleRow; num5 < num4; num5++)
                {
                    if (num2 > num1)
                    {
                        break;
                    }
                    num2 += rowArray1[num5].Height;
                    num3++;
                }
                if (num2 < num1)
                {
                    for (int num6 = this.firstVisibleRow - 1; num6 >= 0; num6--)
                    {
                        int num7 = rowArray1[num6].Height;
                        if ((num2 + num7) > num1)
                        {
                            break;
                        }
                        num2 += num7;
                        this.firstVisibleRow--;
                        num3++;
                    }
                }
                this.numVisibleRows = this.numTotallyVisibleRows = num3;
                if (num2 > num1)
                {
                    this.numTotallyVisibleRows--;
                }
            }
        }

        //		protected override AccessibleObject CreateAccessibilityInstance()
        //		{
        //			base.AccessibleName = "Grid";
        //			base.AccessibleRole = AccessibleRole.Table;
        //			return new Grid.GridAccessibleObject(this);
        //		}


        private GridState CreateChildState(string relationName, GridRow source)
        {
            string text1;
            GridState state1 = new GridState();
            if ((this.DataMember == null) || this.DataMember.Equals(string.Empty))
            {
                text1 = relationName;
            }
            else
            {
                text1 = this.DataMember + "." + relationName;
            }

            //BindingManagerBase bm = (BindingManagerBase) this.BindingContext[this.DataSource, text1];
            //BindManager manager1 = (BindManager.GetBindManager(this.dataSource,text1, bm)) ;
            //BindManager manager1 =base.GetBindManager(this.dataSource,text1) ;

            BindManager manager1 = (BindManager)this.BindContext[this.DataSource, text1];
            state1.DataSource = this.DataSource;
            state1.DataMember = text1;
            state1.ListManager = manager1;
            state1.GridRows = null;
            state1.GridRowsLength = manager1.Count + (this.policy.AllowAdd ? 1 : 0);
            return state1;
        }

        private void CreateGridRows()
        {
            BindManager manager1 = this.ListManager;
            GridTableStyle style1 = this.myGridTable;
            this.InitializeColumnWidths();
            if (manager1 == null)
            {
                this.SetGridRows(new GridRow[0], 0);
            }
            else
            {
                int num1 = manager1.Count;
                if (this.policy.AllowAdd)
                {
                    num1++;
                }
                GridRow[] rowArray1 = new GridRow[num1];
                for (int num2 = 0; num2 < manager1.Count; num2++)
                {
                    rowArray1[num2] = new GridRelationshipRow(this, style1, num2);
                }
                if (this.policy.AllowAdd)
                {
                    this.addNewRow = new GridAddNewRow(this, style1, num1 - 1);
                    rowArray1[num1 - 1] = this.addNewRow;
                }
                else
                {
                    this.addNewRow = null;
                }
                this.SetGridRows(rowArray1, num1);
            }
        }
        /// <summary>
        /// Creates a new GridColumn with the specified PropertyDescriptor.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        protected virtual GridColumnStyle CreateGridColumn(PropertyDescriptor prop)
        {
            if (this.myGridTable != null)
            {
                return this.myGridTable.CreateGridColumn(prop);
            }
            return null;
        }
        /// <summary>
        /// Creates a GridColumn using the specified PropertyDescriptor
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        protected virtual GridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault)
        {
            if (this.myGridTable != null)
            {
                return this.myGridTable.CreateGridColumn(prop, isDefault);
            }
            return null;
        }

         private Grid.LayoutData CreateInitialLayoutState()
        {
            Grid.LayoutData data1 = new Grid.LayoutData();
            data1.Inside = new Rectangle();
            data1.TopLeftHeader = new Rectangle();
            data1.ColumnHeaders = new Rectangle();
            data1.RowHeaders = new Rectangle();
            data1.Data = new Rectangle();
            data1.Caption = new Rectangle();
            data1.ParentRows = new Rectangle();
            data1.ResizeBoxRect = new Rectangle();
            data1.ColumnHeadersVisible = true;
            data1.RowHeadersVisible = true;
            data1.CaptionVisible = true;
            data1.StatusBarVisible = false;
            //data1.StatusBar = new Rectangle();
            data1.ParentRowsVisible = true;
            data1.ClientRectangle = base.ClientRectangle;
 
            return data1;
        }

        private WinMethods.RECT[] CreateScrollableRegion(Rectangle scroll)
        {
            if (this.cachedScrollableRegion == null)
            {
                bool flag1 = this.isRightToLeft();
                Region region1 = new Region(scroll);
                int num1 = this.numVisibleRows;
                int num2 = this.layout.Data.Y;
                int num3 = this.layout.Data.X;
                GridRow[] rowArray1 = this.GridRows;
                for (int num4 = this.firstVisibleRow; num4 < num1; num4++)
                {
                    int num5 = rowArray1[num4].Height;
                    Rectangle rectangle1 = rowArray1[num4].GetNonScrollableArea();
                    rectangle1.X += num3;
                    rectangle1.X = this.MirrorRectangle(rectangle1, this.layout.Data, flag1);
                    if (!rectangle1.IsEmpty)
                    {
                        region1.Exclude(new Rectangle(rectangle1.X, rectangle1.Y + num2, rectangle1.Width, rectangle1.Height));
                    }
                    num2 += num5;
                }
                Graphics graphics1 = this.CreateGraphicsInternal();
                IntPtr ptr1 = region1.GetHrgn(graphics1);
                graphics1.Dispose();
                this.cachedScrollableRegion = Grid.RegionCracker.CrackRegionData(ptr1);
                WinMethods.DeleteObject(new HandleRef(this, ptr1));
            }
            return this.cachedScrollableRegion;
        }


        private bool GridSourceHasErrors()
        {
            if (this.ListManager != null)
            {
                for (int num1 = 0; num1 < this.listManager.Count; num1++)
                {
                    object obj1 = this.listManager[num1];
                    if (obj1 is IDataErrorInfo)
                    {
                        string text1 = ((IDataErrorInfo)obj1).Error;
                        if ((text1 != null) && (text1.Length != 0))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        private void DataSource_Changed(object sender, EventArgs ea)
        {
            this.policy.UpdatePolicy(this.ListManager, this.ReadOnly);
            if (this.gridState[0x200])
            {
                GridRow[] rowArray1 = this.GridRows;
                int num1 = this.GridRowsLength;
                rowArray1[num1 - 1] = new GridRelationshipRow(this, this.myGridTable, num1 - 1);
                this.SetGridRows(rowArray1, num1);
                OnNewRowChanged(GridNewRowState.AddNew);
            }
            else if (this.gridState[0x100000] && !this.gridState[0x400])
            {
                this.listManager.CancelCurrentEdit();
                this.gridState[0x100000] = false;
                this.RecreateGridRows(false);
                OnNewRowChanged(GridNewRowState.Cancel);
            }
            else if (!this.gridState[0x400])
            {

                this.RecreateGridRows(false);
                this.currentRow = Math.Min(this.currentRow, this.listManager.Count);
   
            }
            bool flag1 = this.ListHasErrors;
            this.ListHasErrors = this.GridSourceHasErrors();
            if (flag1 == this.ListHasErrors)
            {
                this.InvalidateInside();
            }
        }

        private void DataSource_ItemChanged(object sender, BindItemChangedEventArgs ea)
        {
            if (ea.Index == -1)
            {
                this.DataSource_Changed(sender, EventArgs.Empty);
            }
            else
            {
                object obj1 = this.listManager[ea.Index];
                bool flag1 = this.ListHasErrors;
                if (obj1 is IDataErrorInfo)
                {
                    if (((IDataErrorInfo)obj1).Error.Length != 0)
                    {
                        this.ListHasErrors = true;
                    }
                    else if (this.ListHasErrors)
                    {
                        this.ListHasErrors = this.GridSourceHasErrors();
                    }
                }
                if (flag1 == this.ListHasErrors)
                {
                    this.InvalidateRow(ea.Index);
                }
                if ((this.editColumn != null) && (ea.Index == this.currentRow))
                {
                    this.editColumn.UpdateUI(this.ListManager, ea.Index, null);
                }
            }
        }


        internal void DataSource_MetaDataChanged(object sender, EventArgs e)
        {
            this.MetaDataChanged();
        }

        private void DataSource_PositionChanged(object sender, EventArgs ea)
        {

            if ((this.GridRowsLength > (this.listManager.Count + (this.policy.AllowAdd ? 1 : 0))) && !this.gridState[0x400])
            {
                this.RecreateGridRows(false);
            }
            if (this.ListManager.Position != this.currentRow)
            {
                this.CurrentCell = new GridCell(this.listManager.Position, this.currentCol);
            }
            this.OnDirtyRow(GridDirtyRowState.Completed);
        }

        private void DataSource_RowChanged(object sender, EventArgs ea)
        {
            this.InvalidateRow(this.currentRow);
            this.OnCurrentRowChanged(ea);
         }
        /// <summary>
         /// Deletes the record at the specified index from the data source.
        /// </summary>
        /// <param name="rowIndex"></param>
        public void DeleteRow(int rowIndex)
        {
            GridRow[] rowArray1 = this.GridRows;
            if (rowIndex >= 0 && rowIndex < rowArray1.Length)
            {
                this.numSelectedRows = 0;
                this.Select(rowIndex);
                this.gridState[0x80000] = false;
                if (!this.policy.AllowRemove || (this.numSelectedRows <= 0))
                {
                    return;// false;
                }
                this.gridState[0x400] = true;
                this.DeleteRows(rowArray1);
                this.currentRow = (this.listManager.Count == 0) ? 0 : this.listManager.Position;
                this.numSelectedRows = 0;

                //CellBoundRemove(rowIndex);
            }
        }

        private void DeleteGridRows(int deletedRows)
        {
            if (deletedRows != 0)
            {
                int num1 = this.GridRowsLength;
                int num2 = (num1 - deletedRows) + (this.gridState[0x100000] ? 1 : 0);
                GridRow[] rowArray1 = new GridRow[num2];
                GridRow[] rowArray2 = this.GridRows;
                int num3 = 0;
                for (int num4 = 0; num4 < num1; num4++)
                {
                    if (rowArray2[num4].Selected)
                    {
                        num3++;
                    }
                    else
                    {
                        rowArray1[num4 - num3] = rowArray2[num4];
                        rowArray1[num4 - num3].number = num4 - num3;
                    }
                }
                if (this.gridState[0x100000])
                {
                    rowArray1[num1 - num3] = new GridAddNewRow(this, this.myGridTable, num1 - num3);
                    this.gridState[0x100000] = false;
                }
                this.SetGridRows(rowArray1, num2);
                this.OnDirty(GridDirtyRowState.Editing);// (true);
                OnRowDeleted(EventArgs.Empty);
            }
        }

        private void DeleteRows(GridRow[] localGridRows)
        {
            int num1 = 0;
            this.BeginUpdateInternal();
            try
            {
                if (this.ListManager != null)
                {
                    for (int num2 = 0; num2 < this.GridRowsLength; num2++)
                    {
                        if (localGridRows[num2].Selected)
                        {
                            if (localGridRows[num2] is GridAddNewRow)
                            {
                                localGridRows[num2].Selected = false;
                            }
                            else
                            {

                                CancelEventArgs e = new CancelEventArgs();
                                OnRowDeleting(e);
                                if (!e.Cancel)
                                {
                                    this.ListManager.RemoveAt(num2 - num1);
                                    num1++;
                                }

                             }
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                this.RecreateGridRows(false);
                this.gridState[0x400] = false;
                this.EndUpdateInternal();
                throw exception1;
            }
            this.DeleteGridRows(num1);
            this.gridState[0x400] = false;
            this.EndUpdateInternal();
        }
        /// <summary>
        /// Releases all resources used by this Grid.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.vertScrollBar != null)
                {
                    this.vertScrollBar.Dispose();
                    //this.vertScrollBar=null;
                }
                if (this.horizScrollBar != null)
                {
                    this.horizScrollBar.Dispose();
                    //this.horizScrollBar=null;
                }
                if (this.myGridTable != null)
                {
                    this.myGridTable.Dispose();
                    this.myGridTable=null;
                }
                if (this.statusBar != null)
                {
                    this.statusBar.Dispose();
                    this.statusBar = null;
                }

                if (this.listManager != null)
                {
                    this.UnWireDataSource();
                    this.listManager = null;
                }
                if (_DataView != null)
                {
                    _DataView.ListChanged -= new ListChangedEventHandler(_DataView_ListChanged);
                    _DataView = null;
                }

                if (dataSource != null)
                {
                    if (dataSource is IDisposable)
                    {
                        ((IDisposable)dataSource).Dispose();
                    }
                    dataSource = null;
                }

                DisposeGraphics();
                //GridTableCollection collection1 = this.TableStyles;
                //if (collection1 != null)
                //{
                //    for (int num1 = 0; num1 < collection1.Count; num1++)
                //    {
                //        collection1[num1].Dispose();
                //    }
                //}
            }
            base.Dispose(disposing);
        }

        private void DisposeGraphics()
        {
            if (alternatingBackBrush != null)
            {
                alternatingBackBrush.Dispose();
                alternatingBackBrush = null;
            }
            if (backBrush != null)
            {
                backBrush.Dispose();
                backBrush = null;
            }
            //if (backgroundBrush != null)
            //{
            //    backgroundBrush.Dispose();
            //    backgroundBrush = null;
            //}
            if (foreBrush != null)
            {
                foreBrush.Dispose();
                foreBrush = null;
            }
            //if (gridLineBrush != null)
            //{
            //    gridLineBrush.Dispose();
            //    gridLineBrush = null;
            //}
            if (headerBackBrush != null)
            {
                headerBackBrush.Dispose();
                headerBackBrush = null;
            }
            if (headerForeBrush != null)
            {
                headerForeBrush.Dispose();
                headerForeBrush = null;
            }
            //if (linkBrush != null)
            //{
            //    linkBrush.Dispose();
            //    linkBrush = null;
            //}
            if (selectionBackBrush != null)
            {
                selectionBackBrush.Dispose();
                selectionBackBrush = null;
            }
            if (selectionForeBrush != null)
            {
                selectionForeBrush.Dispose();
                selectionForeBrush = null;
            }
        }

        private void DrawColSplitBar(MouseEventArgs e)
        {
            Rectangle rectangle1 = this.CalcColResizeFeedbackRect(e);
            this.DrawSplitBar(rectangle1);
        }


        private void DrawRowSplitBar(MouseEventArgs e)
        {
            Rectangle rectangle1 = this.CalcRowResizeFeedbackRect(e);
            this.DrawSplitBar(rectangle1);
        }

        private void DrawSplitBar(Rectangle r)
        {
            IntPtr ptr1 = base.Handle;
            IntPtr ptr2 = WinMethods.GetDCEx(new HandleRef(this, ptr1), WinMethods.NullHandleRef, 0x402);
            IntPtr ptr3 = WinMethods.CreateHalftoneHBRUSH();
            IntPtr ptr4 = WinMethods.SelectObject(new HandleRef(this, ptr2), new HandleRef(null, ptr3));
            WinMethods.PatBlt(new HandleRef(this, ptr2), r.X, r.Y, r.Width, r.Height, 0x5a0049);
            WinMethods.SelectObject(new HandleRef(this, ptr2), new HandleRef(null, ptr4));
            WinMethods.DeleteObject(new HandleRef(null, ptr3));
            WinMethods.ReleaseDC(new HandleRef(this, ptr1), new HandleRef(this, ptr2));
        }

        internal void Edit(int row)
        {
            this.CurrentRow = row;
            if (this.currentRow == row)
            {
                this.Edit(null);
            }
        }

        private void Edit()
        {
            this.Edit(null);
        }


        private void Edit(string instantText)
        {
            this.EnsureBound();
            bool flag1 = true;
            this.EndEdit();
            GridRow[] rowArray1 = this.GridRows;
            if (this.GridRowsLength != 0)
            {
                rowArray1[this.currentRow].OnEdit();
                this.editRow = rowArray1[this.currentRow];
                if (this.myGridTable.GridColumnStyles.Count != 0)
                {
                    this.editColumn = this.myGridTable.GridColumnStyles[this.currentCol];
                    if (this.editColumn.IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                    {
                        Rectangle rectangle1 = Rectangle.Empty;
                        if (((this.currentRow < this.firstVisibleRow) || (this.currentRow > (this.firstVisibleRow + this.numVisibleRows))) || (((this.currentCol < this.firstVisibleCol) || (this.currentCol > ((this.firstVisibleCol + this.numVisibleCols) - 1))) || ((this.currentCol == this.firstVisibleCol) && (this.negOffset != 0))))
                        {
                            flag1 = false;
                        }
                        else
                        {
                            rectangle1 = this.GetCellBounds(this.currentRow, this.currentCol);
                        }
                        this.gridState[0x4000] = true;
                        this.gridState[0x8000] = false;
                        this.gridState[0x10000] = true;
                        if (m_SelectionType != SelectionType.LabelRow)
                        {
                            this.editColumn.Edit(this.ListManager, this.currentRow, rectangle1, (this.myGridTable.ReadOnly || this.ReadOnly) || !this.policy.AllowEdit, instantText, flag1);
                            this.gridState[0x10000] = false;
                        }
                        //this.gridState[0x10000] = false;
                    }
                }
            }
        }

        internal void EndEdit()
        {
            if ((this.gridState[0x8000] || this.gridState[0x4000]) && !this.CommitEdit())
            {
                if (this.rowDirty)
                    return;
                this.AbortEdit();
            }
        }
        /// <summary>
        /// Applies pending changes to the underlying data source
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="rowNumber"></param>
        /// <param name="shouldAbort"></param>
        /// <returns></returns>
        public bool EndEdit(GridColumnStyle gridColumn, int rowNumber, bool shouldAbort)
        {
            bool flag1 = false;
            if (!this.gridState[0x8000])
            {
                return flag1;
            }
            GridColumnStyle style1 = this.editColumn;
            int num1 = this.editRow.RowNumber;
            if (shouldAbort)
            {
                this.AbortEdit();
                return true;
            }
            return this.CommitEdit();
        }
        /// <summary>
        /// Begins the initialization of this grid control.
        /// </summary>
        public void BeginInit()
        {
            if (this.inInit)
            {
                throw new InvalidOperationException("GridBeginInit");
            }
            this.inInit = true;
        }
        /// <summary>
        /// Ends the initialization of an grid control.
        /// </summary>
        public  void EndInit()
        {
            this.inInit = false;
            //			if ((this.myGridTable == null) && (this.ListManager != null))
            //			{
            //				//this.SetGridTable(this.TableStyles[this.GetListName()], true);
            //				this.SetGridTable(this.TableStyles[this.ListManager.GetListName()], true);
            //			}
            if (this.myGridTable != null)
            {
                this.myGridTable.Grid = this;
            }
            //ResetStyleLayout();
            EndInitInternal();
        }
        /// <summary>
        /// Ends the internal initialization of an grid control.
        /// </summary>
        protected virtual void EndInitInternal()
        {
            // used by vgrid
        }

        private void EnforceValidDataMember(object value)
        {
            if (!"".Equals(this.DataMember) && (this.BindingContext != null))
            {
                try
                {
                    //-BindingManagerBase
                    //BindingManagerBase base1 = this.BindingContext[value, this.dataMember];
                    BindManagerBase base1 = this.BindContext[value, this.dataMember];
                }
                catch (Exception)
                {
                    this.dataMember = "";
                }
            }
        }

        private void EnsureBound()
        {
            if (!this.Bound)
            {
                throw new InvalidOperationException("GridUnbound");
            }
        }


        private void EnsureVisible(int row, int col)
        {
            bool flag1 = false;
            int num1 = this.firstVisibleRow;
            int num2 = this.firstVisibleCol;
            if ((row < this.firstVisibleRow) || (row >= (this.firstVisibleRow + this.numTotallyVisibleRows)))
            {
                flag1 = true;
                num1 = row;
            }
            if (((col < this.firstVisibleCol) || ((col == this.firstVisibleCol) && (this.negOffset != 0))) || ((col > this.firstVisibleCol) && (col > this.lastTotallyVisibleCol)))
            {
                flag1 = true;
                num2 = col;
            }
            if (flag1)
            {
                this.ScrollTo(num1, num2);
            }
        }
        /// <summary>
        /// Displays child relations, if any exist, for all rows or a specific row
        /// </summary>
        /// <param name="row"></param>
        public void Expand(int row)
        {
            this.SetRowExpansionState(row, true);
        }
        /// <summary>
        /// Gets the Rectangle that specifies the four corners of a cell.
        /// </summary>
        /// <param name="gc"></param>
        /// <returns></returns>
        public Rectangle GetCellBounds(GridCell gc)
        {
            return this.GetCellBounds(gc.RowNumber, gc.ColumnNumber);
        }
        /// <summary>
        /// Gets the Rectangle that specifies the four corners of a cell.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public Rectangle GetCellBounds(int row, int col)
        {
            Rectangle rectangle1 = this.GridRows[row].GetCellBounds(col);
            rectangle1.Y += this.GetRowTop(row);
            rectangle1.X += this.layout.Data.X - this.negOffset;
            rectangle1.X = this.MirrorRectangle(rectangle1, this.layout.Data, this.isRightToLeft());
            return rectangle1;
        }

        internal int GetColBeg(int col)
        {
            int num1 = this.layout.Data.X - this.negOffset;
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            int num2 = Math.Min(col, collection1.Count);
            for (int num3 = this.firstVisibleCol; num3 < num2; num3++)
            {
                if (collection1[num3].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                {
                    num1 += collection1[num3].Width;
                }
            }
            return this.MirrorPoint(num1, this.layout.Data, this.isRightToLeft());
        }

        internal int GetColEnd(int col)
        {
            int num1 = this.GetColBeg(col);
            int num2 = this.myGridTable.GridColumnStyles[col].Width;
            if (!this.isRightToLeft())
            {
                return (num1 + num2);
            }
            return (num1 - num2);
        }

        private int GetColFromX(int x)
        {
            if (this.myGridTable != null)
            {
                Rectangle rectangle1 = this.layout.Data;
                x = this.MirrorPoint(x, rectangle1, this.isRightToLeft());
                GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
                int num1 = collection1.Count;
                int num2 = rectangle1.X - this.negOffset;
                for (int num3 = this.firstVisibleCol; (num2 < (rectangle1.Width + rectangle1.X)) && (num3 < num1); num3++)
                {
                    if (collection1[num3].IsVisibleInternal)//*bound*/collection1[num3].PropertyDescriptor != null)
                    {
                        num2 += collection1[num3].Width;
                    }
                    if (num2 > x)
                    {
                        return num3;
                    }
                }
            }
            return -1;
        }

        internal Rectangle GetColumnHeadersRect()
        {
            return this.layout.ColumnHeaders;
        }

        private int GetColumnWidthSum()
        {
            int num1 = 0;
            if ((this.myGridTable != null) && (this.myGridTable.GridColumnStyles != null))
            {
                GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
                int num2 = collection1.Count;
                for (int num3 = 0; num3 < num2; num3++)
                {
                    if (collection1[num3].IsVisibleInternal)//*bound*/collection1[num3].PropertyDescriptor != null)
                    {
                        num1 += collection1[num3].Width;
                    }
                }
            }
            return num1;
        }

        /// <summary>
        /// Gets a Rectangle that specifies the four corners of the selected cell.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetCurrentCellBounds()
        {
            GridCell cell1 = this.CurrentCell;
            return this.GetCellBounds(cell1.RowNumber, cell1.ColumnNumber);
        }

 
        //multi
        private GridRelationshipRow[] GetExpandableRows()
        {
            int num1 = this.GridRowsLength;
            GridRow[] rowArray1 = this.GridRows;
            if (this.policy.AllowAdd)
            {
                num1 = Math.Max(num1 - 1, 0);
            }
            GridRelationshipRow[] rowArray2 = new GridRelationshipRow[num1];
            for (int num2 = 0; num2 < num1; num2++)
            {
                rowArray2[num2] = (GridRelationshipRow)rowArray1[num2];
            }
            return rowArray2;
        }
        /// <summary>
        /// GetOutputTextDelimiter
        /// </summary>
        /// <returns></returns>
        protected virtual string GetOutputTextDelimiter()
        {
            return "\t";
        }

        private int GetRowBottom(int row)
        {
            GridRow[] rowArray1 = this.GridRows;
            return (this.GetRowTop(row) + rowArray1[row].Height);
        }

        internal Rectangle GetRowBounds(GridRow row)
        {
            Rectangle rectangle1 = new Rectangle();
            rectangle1.Y = this.GetRowTop(row.RowNumber);
            rectangle1.X = this.layout.Data.X;
            rectangle1.Height = row.Height;
            rectangle1.Width = this.layout.Data.Width;
            return rectangle1;
        }

        private int GetRowFromY(int y)
        {
            Rectangle rectangle1 = this.layout.Data;
            int num1 = rectangle1.Y;
            int num2 = this.firstVisibleRow;
            int num3 = this.GridRowsLength;
            GridRow[] rowArray1 = this.GridRows;
            int num4 = rectangle1.Bottom;
            while ((num1 < num4) && (num2 < num3))
            {
                num1 += rowArray1[num2].Height;
                if (num1 > y)
                {
                    return num2;
                }
                num2++;
            }
            return -1;
        }

        internal Rectangle GetRowHeaderRect()
        {
            return this.layout.RowHeaders;
        }

        internal Rectangle GetRowRect(int rowNumber)
        {
            Rectangle rectangle1 = this.layout.Data;
            int num1 = rectangle1.Y;
            GridRow[] rowArray1 = this.GridRows;
            for (int num2 = this.firstVisibleRow; num2 <= rowNumber; num2++)
            {
                if (num1 > rectangle1.Bottom)
                {
                    break;
                }
                if (num2 == rowNumber)
                {
                    Rectangle rectangle2 = new Rectangle(rectangle1.X, num1, rectangle1.Width, rowArray1[num2].Height);
                    if (this.layout.RowHeadersVisible)
                    {
                        rectangle2.Width += this.layout.RowHeaders.Width;
                        rectangle2.X -= this.isRightToLeft() ? 0 : this.layout.RowHeaders.Width;
                    }
                    return rectangle2;
                }
                num1 += rowArray1[num2].Height;
            }
            return Rectangle.Empty;
        }

        private int GetRowTop(int row)
        {
            GridRow[] rowArray1 = this.GridRows;
            int num1 = this.layout.Data.Y;
            int num2 = Math.Min(row, this.GridRowsLength);
            for (int num3 = this.firstVisibleRow; num3 < num2; num3++)
            {
                num1 += rowArray1[num3].Height;
            }
            for (int num4 = this.firstVisibleRow; num4 > num2; num4--)
            {
                num1 -= rowArray1[num4].Height;
            }
            return num1;
        }
        /// <summary>
        /// Listens for the scroll event of the horizontal scroll bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="se"></param>
        protected virtual void GridHScrolled(object sender, ScrollEventArgs se)
        {
            if (base.Enabled && (this.DataSource != null))
            {
                this.gridState[0x20000] = true;
                if ((se.Type == ScrollEventType.SmallIncrement) || (se.Type == ScrollEventType.SmallDecrement))
                {
                    int num1 = (se.Type == ScrollEventType.SmallIncrement) ? 1 : -1;
                    this.ScrollRight(num1);
                    se.NewValue = this.HorizontalOffset;
                }
                else if (se.Type != ScrollEventType.EndScroll)
                {
                    this.HorizontalOffset = se.NewValue;
                }
            }
        }

        private void GridLineColorChanged(object sender, EventArgs e)
        {
            base.Invalidate(this.layout.Data);
        }

        private void GridLineStyleChanged(object sender, EventArgs e)
        {
            //this.myGridTable.ResetRelationsUI();
            base.Invalidate(this.layout.Data);
        }
        /// <summary>
        /// Listens for the scroll event of the vertical scroll bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="se"></param>
        protected virtual void GridVScrolled(object sender, ScrollEventArgs se)
        {
            if (base.Enabled && (this.DataSource != null))
            {
                this.gridState[0x20000] = true;
                se.NewValue = Math.Min(se.NewValue, this.GridRowsLength - this.numTotallyVisibleRows);
                int num1 = se.NewValue - this.firstVisibleRow;
                this.ScrollDown(num1);
            }
        }

        private void HandleEndCurrentEdit()
        {
            if (rowNew)
            {
               return;
            }

            int num1 = this.currentRow;
            int num2 = this.currentCol;
            try
            {
                this.listManager.EndCurrentEdit();
            }
            catch (Exception exception1)
            {
                DialogResult result1 = MsgBox.ShowQuestion("You are trying to push incorrect value into column , Error is:'" + exception1.Message + "' , Would you like to correct this problem ?");
                //DialogResult result1 = MessageBox.Show("GridPushedIncorrectValueIntoColumn " + exception1.Message, "GridErrorMessageBoxCaption", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    this.currentRow = num1;
                    this.currentCol = num2;
                    this.InvalidateRowHeader(this.currentRow);
                    this.Edit();
                    return;
                }
                this.listManager.PositionChanged -= this.positionChangedHandler;
                this.listManager.CancelCurrentEdit();
                this.listManager.Position = this.currentRow;
                this.listManager.PositionChanged += this.positionChangedHandler;
            }
        }

        private void HeaderBackColorChanged(object sender, EventArgs e)
        {
            if (this.layout.RowHeadersVisible)
            {
                base.Invalidate(this.layout.RowHeaders);
            }
            if (this.layout.ColumnHeadersVisible)
            {
                base.Invalidate(this.layout.ColumnHeaders);
            }
            base.Invalidate(this.layout.TopLeftHeader);
        }

        private void HeaderFontChanged(object sender, EventArgs e)
        {
            this.RecalculateFonts();
            base.PerformLayout();
            base.Invalidate(this.layout.Inside);
        }

        private void HeaderForeColorChanged(object sender, EventArgs e)
        {
            if (this.layout.RowHeadersVisible)
            {
                base.Invalidate(this.layout.RowHeaders);
            }
            if (this.layout.ColumnHeadersVisible)
            {
                base.Invalidate(this.layout.ColumnHeaders);
            }
            base.Invalidate(this.layout.TopLeftHeader);
        }
        /// <summary>
        /// Retrieves the child object at the specified screen coordinates
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Grid.HitTestInfo HitTest(Point position)
        {
            return this.HitTest(position.X, position.Y);
        }

        /// <summary>
        /// Retrieves the child object at the specified screen coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Grid.HitTestInfo HitTest(int x, int y)
        {
            int num3 = this.layout.Data.Y;
            Grid.HitTestInfo info1 = new Grid.HitTestInfo();
            if (this.layout.CaptionVisible && this.layout.Caption.Contains(x, y))
            {
                info1.type = Grid.HitTestType.Caption;
                return info1;
            }
            if (this.layout.ParentRowsVisible && this.layout.ParentRows.Contains(x, y))
            {
                info1.type = Grid.HitTestType.ParentRows;
                return info1;
            }
            if (!this.layout.Inside.Contains(x, y))
            {
                return info1;
            }
            if (this.layout.TopLeftHeader.Contains(x, y))
            {
                return info1;
            }
            if (this.layout.ColumnHeaders.Contains(x, y))
            {
                info1.type = Grid.HitTestType.ColumnHeader;
                info1.col = this.GetColFromX(x);
                if (info1.col < 0)
                {
                    return Grid.HitTestInfo.Nowhere;
                }
                int num1 = this.GetColBeg(info1.col + 1);
                bool flag1 = this.isRightToLeft();
                if ((flag1 && ((x - num1) < 8)) || (!flag1 && ((num1 - x) < 8)))
                {
                    info1.type = Grid.HitTestType.ColumnResize;
                }
                if (!this.allowColumnResize)
                {
                    return Grid.HitTestInfo.Nowhere;
                }
                return info1;
            }
            if (this.layout.RowHeaders.Contains(x, y))
            {
                info1.type = Grid.HitTestType.RowHeader;
                info1.row = this.GetRowFromY(y);
                if (info1.row < 0)
                {
                    return Grid.HitTestInfo.Nowhere;
                }
                GridRow[] rowArray1 = this.GridRows;
                int num2 = this.GetRowTop(info1.row) + rowArray1[info1.row].Height;
                if ((((num2 - y) - this.BorderWidth) < 2) && !(rowArray1[info1.row] is GridAddNewRow))
                {
                    info1.type = Grid.HitTestType.RowResize;
                }
                if (!this.allowRowResize)
                {
                    return Grid.HitTestInfo.Nowhere;
                }
                return info1;
            }
            if (!this.layout.Data.Contains(x, y))
            {
                return info1;
            }
            info1.type = Grid.HitTestType.Cell;
            info1.col = this.GetColFromX(x);
            info1.row = this.GetRowFromY(y);
            if ((info1.col >= 0) && (info1.row >= 0))
            {
                return info1;
            }
            return Grid.HitTestInfo.Nowhere;
        }

        private void InitializeColumnWidths()
        {
            if (this.myGridTable != null)
            {
                GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
                int num1 = collection1.Count;
                int num2 = this.myGridTable.IsDefault ? this.PreferredColumnWidth : this.myGridTable.dataGrid.PreferredColumnWidth;
                for (int num3 = 0; num3 < num1; num3++)
                {
                    if (collection1[num3].width == -1)
                    {
                        collection1[num3].width = num2;
                    }
                }
            }
        }

        internal void InvalidateCaption()
        {
            if (this.layout.CaptionVisible)
            {
                base.Invalidate(this.layout.Caption);
            }
        }

        internal void InvalidateCaptionRect(Rectangle r)
        {
            if (this.layout.CaptionVisible)
            {
                base.Invalidate(r);
            }
        }

        internal void InvalidateColumn(int column)
        {
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            if ((((column >= 0) && (collection1 != null)) && (collection1.Count > column)) && ((column >= this.firstVisibleCol) && (column <= ((this.firstVisibleCol + this.numVisibleCols) - 1))))
            {
                Rectangle rectangle1 = new Rectangle();
                rectangle1.Height = this.layout.Data.Height;
                rectangle1.Width = collection1[column].Width;
                rectangle1.Y = this.layout.Data.Y;
                int num1 = this.layout.Data.X - this.negOffset;
                int num2 = collection1.Count;
                for (int num3 = this.firstVisibleCol; num3 < num2; num3++)
                {
                    if (num3 == column)
                    {
                        break;
                    }
                    num1 += collection1[num3].Width;
                }
                rectangle1.X = num1;
                rectangle1.X = this.MirrorRectangle(rectangle1, this.layout.Data, this.isRightToLeft());
                base.Invalidate(rectangle1);
            }
        }

        internal void InvalidateInside()
        {
            base.Invalidate(this.layout.Inside);
        }

        internal void InvalidateParentRows()
        {
            if (this.layout.ParentRowsVisible)
            {
                base.Invalidate(this.layout.ParentRows);
            }
        }

        internal void InvalidateParentRowsRect(Rectangle r)
        {
            Rectangle rectangle1 = this.layout.ParentRows;
            base.Invalidate(r);
            bool flag1 = rectangle1.IsEmpty;
        }

        internal void InvalidateRow(int rowNumber)
        {
            Rectangle rectangle1 = this.GetRowRect(rowNumber);
            if (!rectangle1.IsEmpty)
            {
                base.Invalidate(rectangle1);

            }
        }

        private void InvalidateRowHeader(int rowNumber)
        {
            if (((rowNumber >= this.firstVisibleRow) && (rowNumber < (this.firstVisibleRow + this.numVisibleRows))) && this.layout.RowHeadersVisible)
            {
                Rectangle rectangle1 = new Rectangle();
                rectangle1.Y = this.GetRowTop(rowNumber);
                rectangle1.X = this.layout.RowHeaders.X;
                rectangle1.Width = this.layout.RowHeaders.Width;
                rectangle1.Height = this.GridRows[rowNumber].Height;
                base.Invalidate(rectangle1);
            }
        }

        internal void InvalidateRowRect(int rowNumber, Rectangle r)
        {
            Rectangle rectangle1 = this.GetRowRect(rowNumber);
            if (!rectangle1.IsEmpty)
            {
                Rectangle rectangle2 = new Rectangle(rectangle1.X + r.X, rectangle1.Y + r.Y, r.Width, r.Height);
                if (this.vertScrollBar.Visible && this.isRightToLeft())
                {
                    rectangle2.X -= this.vertScrollBar.Width;
                }
                base.Invalidate(rectangle2);
            }
        }
   


        //--		public bool IsExpanded(int rowNumber)
        //		{
        //			if ((rowNumber < 0) || (rowNumber > this.GridRowsLength))
        //			{
        //				throw new ArgumentOutOfRangeException("rowNumber");
        //			}
        //			GridRow row1 = this.GridRows[rowNumber];
        //			if (row1 is GridRelationshipRow)
        //			{
        //				GridRelationshipRow row2 = (GridRelationshipRow) row1;
        //				return row2.Expanded;
        //			}
        //			return false;
        //		}

        private bool isRightToLeft()
        {
            return (this.RightToLeft == RightToLeft.Yes);
        }

        /// <summary>
        /// Gets a value indicating whether a specified row is selected
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool IsSelected(int row)
        {
            return this.GridRows[row].Selected;
        }

        internal static bool IsTransparentColor(Color color)
        {
            return (color.A < 0xff);
        }

        /// <summary>
        /// OnEnabledChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            LayoutScrollBars();
        }

        private void LayoutScrollBars()
        {
            if ((this.listManager == null) || (this.myGridTable == null))
            {
                this.horizScrollBar.Visible = false;
                this.vertScrollBar.Visible = false;
            }
            else
            {
                bool flag1 = false;
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = this.isRightToLeft();
                int num1 = this.myGridTable.GridColumnStyles.Count;
                GridRow[] rowArray1 = this.GridRows;
                int sumColWidth = this.GetColumnWidthSum();
                if ((sumColWidth > this.layout.Data.Width) && !flag1)
                {
                    int num3 = this.horizScrollBar.Height;
                    this.layout.Data.Height -= num3;
                    if (this.layout.RowHeadersVisible)
                    {
                        this.layout.RowHeaders.Height -= num3;
                    }
                    flag1 = true;
                }
                int num4 = this.firstVisibleRow;
                this.ComputeVisibleRows();
                if ((this.numTotallyVisibleRows != this.GridRowsLength) && !flag2)
                {
                    int num5 = this.vertScrollBar.Width;
                    this.layout.Data.Width -= num5;
                    if (this.layout.ColumnHeadersVisible)
                    {
                        if (flag4)
                        {
                            this.layout.ColumnHeaders.X += num5;
                        }
                        this.layout.ColumnHeaders.Width -= num5;
                    }
                    flag2 = true;
                }
                this.firstVisibleCol = this.ComputeFirstVisibleColumn();
                this.ComputeVisibleColumns();
                if ((flag2 && (sumColWidth > this.layout.Data.Width)) && !flag1)
                {
                    this.firstVisibleRow = num4;
                    int num6 = this.horizScrollBar.Height;
                    this.layout.Data.Height -= num6;
                    if (this.layout.RowHeadersVisible)
                    {
                        this.layout.RowHeaders.Height -= num6;
                    }
                    flag1 = true;
                    flag3 = true;
                }
                if (flag3)
                {
                    this.ComputeVisibleRows();
                    if ((this.numTotallyVisibleRows != this.GridRowsLength) && !flag2)
                    {
                        int num7 = this.vertScrollBar.Width;
                        this.layout.Data.Width -= num7;
                        if (this.layout.ColumnHeadersVisible)
                        {
                            if (flag4)
                            {
                                this.layout.ColumnHeaders.X += num7;
                            }
                            this.layout.ColumnHeaders.Width -= num7;
                        }
                        flag2 = true;
                    }
                }
                this.layout.ResizeBoxRect = new Rectangle();
                if (flag2 && flag1)
                {
                    Rectangle rectangle1 = this.layout.Data;
                    this.layout.ResizeBoxRect = new Rectangle(flag4 ? rectangle1.X : rectangle1.Right, rectangle1.Bottom, this.vertScrollBar.Width, this.horizScrollBar.Height);
                }
                if (flag1 && (num1 > 0))
                {
                    int num8 = sumColWidth - this.layout.Data.Width;
                    int bottom = StatusBarVisible ? this.layout.Data.Bottom + 2 : this.layout.Data.Bottom;
                    this.horizScrollBar.Minimum = 0;
                    this.horizScrollBar.Maximum = sumColWidth;
                    this.horizScrollBar.SmallChange = 1;
                    this.horizScrollBar.LargeChange = Math.Max(sumColWidth - num8, 0);
                    this.horizScrollBar.Enabled = base.Enabled;
                    this.horizScrollBar.RightToLeft = this.RightToLeft;
                    this.horizScrollBar.Bounds = new Rectangle(flag4 ? (this.layout.Inside.X + this.layout.ResizeBoxRect.Width) : this.layout.Inside.X,bottom /*this.layout.Data.Bottom*/, this.layout.Inside.Width - this.layout.ResizeBoxRect.Width, this.horizScrollBar.Height);
                    this.horizScrollBar.Visible = true;
                }
                else if (this.horizScrollBar.Visible)
                {
                    this.HorizontalOffset = 0;
                    this.horizScrollBar.Visible = false;
                }
                if (flag2)
                {
                    int num9 = this.layout.Data.Y;
                    if (this.layout.ColumnHeadersVisible)
                    {
                        num9 = this.layout.ColumnHeaders.Y;
                    }
                    this.vertScrollBar.LargeChange = (this.numTotallyVisibleRows != 0) ? this.numTotallyVisibleRows : 1;
                    this.vertScrollBar.Bounds = new Rectangle(flag4 ? this.layout.Data.X : this.layout.Data.Right, num9, this.vertScrollBar.Width, this.layout.Data.Height + this.layout.ColumnHeaders.Height);
                    this.vertScrollBar.Enabled = base.Enabled;
                    this.vertScrollBar.Visible = true;
                    if (flag4)
                    {
                        this.layout.Data.X += this.vertScrollBar.Width;
                    }
                }
                else if (this.vertScrollBar.Visible)
                {
                    this.vertScrollBar.Visible = false;
                }
            }
        }

        private void LinkColorChanged(object sender, EventArgs e)
        {
            base.Invalidate(this.layout.Data);
        }

        private void LinkHoverColorChanged(object sender, EventArgs e)
        {
            base.Invalidate(this.layout.Data);
        }

        private void MetaDataChanged()
        {
            //this.parentRows.Clear();
            //this.caption.BackButtonActive = this.caption.DownButtonActive = this.caption.BackButtonVisible = false;
            this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
            this.gridState[0x400000] = true;
            try
            {
                if (this.originalState != null)
                {
                    this.Set_ListManager(this.originalState.DataSource, this.originalState.DataMember, true);
                    this.originalState = null;
                }
                else
                {
                    this.Set_ListManager(this.DataSource, this.DataMember, true);
                }
            }
            finally
            {
                this.gridState[0x400000] = false;
            }
        }

        internal int MinimumRowHeaderWidth()
        {
            return this.minRowHeaderWidth;
        }


        private int MirrorPoint(int x, Rectangle rect, bool rightToLeft)
        {
            if (rightToLeft)
            {
                return ((rect.Right + rect.X) - x);
            }
            return x;
        }

        private int MirrorRectangle(Rectangle R1, Rectangle rect, bool rightToLeft)
        {
            if (rightToLeft)
            {
                return ((rect.Right + rect.X) - R1.Right);
            }
            return R1.X;
        }

        private int MoveLeftRight(GridColumnCollection cols, int startCol, bool goRight)
        {
            int num1;
            if (goRight)
            {
                num1 = startCol + 1;
                while (num1 < cols.Count)
                {
                    if (cols[num1].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                    {
                        return num1;
                    }
                    num1++;
                }
                return num1;
            }
            num1 = startCol - 1;
            while (num1 >= 0)
            {
                if (cols[num1].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                {
                    return num1;
                }
                num1--;
            }
            return num1;
        }

        #region navigate

        //public void NavigateBack()
        //{
        //    if (this.CommitEdit() && !this.parentRows.IsEmpty())
        //    {
        //        if (this.gridState[0x100000])
        //        {
        //            this.gridState[0x100000] = false;
        //            try
        //            {
        //                this.listManager.CancelCurrentEdit();
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        else
        //        {
        //            this.UpdateListManager();
        //        }
        //        GridState state1 = this.parentRows.PopTop();
        //        this.ResetMouseState();
        //        state1.PullState(this, false);
        //        if (this.parentRows.GetTopParent() == null)
        //        {
        //            this.originalState = null;
        //        }
        //        GridRow[] rowArray1 = this.GridRows;
        //        if ((this.ReadOnly || !this.policy.AllowAdd) == (rowArray1[this.GridRowsLength - 1] is GridAddNewRow))
        //        {
        //            int num1 = (this.ReadOnly || !this.policy.AllowAdd) ? (this.GridRowsLength - 1) : (this.GridRowsLength + 1);
        //            GridRow[] rowArray2 = new GridRow[num1];
        //            for (int num2 = 0; num2 < Math.Min(num1, this.GridRowsLength); num2++)
        //            {
        //                rowArray2[num2] = this.GridRows[num2];
        //            }
        //            if (!this.ReadOnly && this.policy.AllowAdd)
        //            {
        //                rowArray2[num1 - 1] = new GridAddNewRow(this, this.myGridTable, num1 - 1);
        //            }
        //            this.SetGridRows(rowArray2, num1);
        //        }
        //        rowArray1 = this.GridRows;
        //        if (((rowArray1 != null) && (rowArray1.Length != 0)) && (rowArray1[0].GridTableStyle != this.myGridTable))
        //        {
        //            for (int num3 = 0; num3 < rowArray1.Length; num3++)
        //            {
        //                rowArray1[num3].GridTableStyle = this.myGridTable;
        //            }
        //        }
        //        if ((this.myGridTable.GridColumnStyles.Count > 0) && (this.myGridTable.GridColumnStyles[0].Width == -1))
        //        {
        //            this.InitializeColumnWidths();
        //        }
        //        this.currentRow = (this.ListManager.Position == -1) ? 0 : this.ListManager.Position;
        //        if (!this.AllowNavigation)
        //        {
        //            this.RecreateGridRows();
        //        }
        //        //this.caption.BackButtonActive = (this.parentRows.GetTopParent() != null) && this.AllowNavigation;
        //        //this.caption.BackButtonVisible = this.caption.BackButtonActive;
        //        this.caption.DownButtonActive = this.parentRows.GetTopParent() != null;
        //        base.PerformLayout();
        //        base.Invalidate();
        //        if (this.vertScrollBar.Visible)
        //        {
        //            this.vertScrollBar.Value = this.firstVisibleRow;
        //        }
        //        if (this.horizScrollBar.Visible)
        //        {
        //            this.horizScrollBar.Value = this.HorizontalOffset + this.negOffset;
        //        }
        //        this.Edit();
        //        this.OnNavigate(new NavigateEventArgs(false));
        //    }
        //}

        //private void NavigateTo(GridState childState)
        //{
        //    this.EndEdit();
        //    this.gridState[0x4000] = false;
        //    this.ResetMouseState();
        //    childState.PullState(this, true);
        //    if (this.listManager.Position != this.currentRow)
        //    {
        //        this.currentRow = (this.listManager.Position == -1) ? 0 : this.listManager.Position;
        //    }
        //    if (this.parentRows.GetTopParent() != null)
        //    {
        //        //this.caption.BackButtonActive = this.AllowNavigation;
        //        //this.caption.BackButtonVisible = this.caption.BackButtonActive;
        //        this.caption.DownButtonActive = true;
        //    }
        //    this.HorizontalOffset = 0;
        //    base.PerformLayout();
        //    base.Invalidate();
        //}

        //public void NavigateTo(int rowNumber, string relationName)
        //{
        //    if (this.AllowNavigation)
        //    {
        //        GridRow[] rowArray1 = this.GridRows;
        //        if ((rowNumber < 0) || (rowNumber > (this.GridRowsLength - (this.policy.AllowAdd ? 2 : 1))))
        //        {
        //            throw new ArgumentOutOfRangeException("rowNumber");
        //        }
        //        this.EnsureBound();
        //        GridRow row1 = rowArray1[rowNumber];
        //        this.NavigateTo(relationName, row1, false);
        //    }
        //}

        //internal void NavigateTo(string relationName, GridRow source, bool fromRow)
        //{
        //    if (this.AllowNavigation && this.CommitEdit())
        //    {
        //        GridState state1;
        //        try
        //        {
        //            state1 = this.CreateChildState(relationName, source);
        //        }
        //        catch (Exception)
        //        {
        //            this.NavigateBack();
        //            return;
        //        }
        //        try
        //        {
        //            this.listManager.EndCurrentEdit();
        //        }
        //        catch (Exception)
        //        {
        //            return;
        //        }
        //        GridState state2 = new GridState(this);
        //        state2.LinkingRow = source;
        //        if (source.RowNumber != this.CurrentRow)
        //        {
        //            this.listManager.Position = source.RowNumber;
        //        }
        //        if (this.parentRows.GetTopParent() == null)
        //        {
        //            this.originalState = state2;
        //        }
        //        this.parentRows.AddParent(state2);
        //        this.NavigateTo(state1);
        //        this.OnNavigate(new NavigateEventArgs(true));
        //    }
        //}

        #endregion

        private Point NormalizeToRow(int x, int y, int row)
        {
            Point point1 = new Point(0, this.layout.Data.Y);
            GridRow[] rowArray1 = this.GridRows;
            for (int num1 = this.firstVisibleRow; num1 < row; num1++)
            {
                point1.Y += rowArray1[num1].Height;
            }
            return new Point(x, y - point1.Y);
        }

        private void ObjectSiteChange(IContainer container, IComponent component, bool site)
        {
            if (site)
            {
                if (component.Site == null)
                {
                    container.Add(component);
                }
            }
            else if ((component.Site != null) && (component.Site.Container == container))
            {
                container.Remove(component);
            }
        }

        #endregion

        #region virtual events
        /// <summary>
        /// Raises the TextChanged event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (this.TextChanged != null)
                this.TextChanged(this, e);
        }
        /// <summary>
        /// Raises the AllowNavigationChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAllowNavigationChanged(EventArgs e)
        {
            if (this.AllowNavigationChanged != null)
                this.AllowNavigationChanged(this, e);
            //			EventHandler handler1 = base.Events[Grid.EVENT_ALLOWNAVIGATIONCHANGED] as EventHandler;
            //			if (handler1 != null)
            //			{
            //				handler1(this, e);
            //			}
        }
        /// <summary>
        /// Raises the CursorChanged event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCursorChanged(EventArgs e)
        {
            if (this.CursorChanged != null)
                this.CursorChanged(this, e);
        }

        //		protected virtual void OnAllowNavigationChanged(EventArgs e)
        //		{
        //			if(this.AllowNavigationChanged!=null)
        //				this.AllowNavigationChanged(this,e);
        //
        //		}
        /// <summary>
        /// Overrides Control.OnBackColorChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            this.backBrush = new SolidBrush(this.BackColor);
            base.Invalidate();
            base.OnBackColorChanged(e);
        }
        /// <summary>
        /// Overrides Control.OnBackgroundColorChanged
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBackgroundColorChanged(EventArgs e)
        {
            if (this.BackgroundColorChanged != null)
                this.BackgroundColorChanged(this, e);
        }
        /// <summary>
        /// Overrides Control.OnBindingContextChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBindingContextChanged(EventArgs e)
        {
            //if ((this.DataSource != null) && !this.gridState[0x200000])
            //{
            //    try
            //    {
            //        //this.Set_ListManager(this.DataSource, this.DataMember, true, false);
            //        this.Set_ListManager(this.DataSource, this.DataMember, false, false);
            //        //this.Set_ListManager(this.DataSource, this.DataMember, true);
            //    }
            //    catch
            //    {
            //        if ((this.Site == null) || !this.Site.DesignMode)
            //        {
            //            /*site*/
            //            return;//throw;
            //        }
            //        //MessageBox.Show("GridExceptionInPaint");
            //        this.BeginUpdateInternal();
            //        //this.parentRows.Clear();
            //        //this.caption.BackButtonActive = this.caption.DownButtonActive = this.caption.BackButtonVisible = false;
            //        this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
            //        this.originalState = null;
            //        this.Set_ListManager(null, string.Empty, true);
            //        this.EndUpdateInternal();
            //    }
            //}
            base.OnBindingContextChanged(e);
        }
        /// <summary>
        /// Overrides OnBorderStyleChanged
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBorderStyleChanged(EventArgs e)
        {
            if (this.BorderStyleChanged != null)
                this.BorderStyleChanged(this, e);

            //			EventHandler handler1 = base.Events[Grid.EVENT_BORDERSTYLECHANGED] as EventHandler;
            //			if (handler1 != null)
            //			{
            //				handler1(this, e);
            //			}
        }
        /// <summary>
        /// Overrides OnCaptionVisibleChanged
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCaptionVisibleChanged(EventArgs e)
        {
            if (this.CaptionVisibleChanged != null)
                this.CaptionVisibleChanged(this, e);
            //			EventHandler handler1 = base.Events[Grid.EVENT_CAPTIONVISIBLECHANGED] as EventHandler;
            //			if (handler1 != null)
            //			{
            //				handler1(this, e);
            //			}
        }

        internal void OnColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            GridTableStyle style1 = (GridTableStyle)sender;
            if (style1.Equals(this.myGridTable))
            {
                if (!this.myGridTable.IsDefault && ((e.Action != CollectionChangeAction.Refresh) || (e.Element == null)))
                {
                    this.PairTableStylesAndGridColumns(this.listManager, this.myGridTable, false);
                }
                base.Invalidate();
                base.PerformLayout();
            }
        }

        /// <summary>
        /// Overrides OnNewRowCompleted
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnNewRowCompleted(EventArgs e)
        {
            rowNew = false;
            if (this.NewRowCompleted != null)
            {
                NewRowCompleted(this, e);
            }
        }
        /// <summary>
        /// Overrides OnNewRowAdded
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnNewRowAdded(EventArgs e)
        {
            if (this.NewRowAdded != null)
            {
                NewRowAdded(this, e);
            }
        }

        
        private void OnNewRowChanged(GridNewRowState state)//bool isNew)
        {
            bool isNew = state == GridNewRowState.AddNew;

            if (this.rowNew != isNew)
            {
                if (state == GridNewRowState.Completed)
                {
                     OnNewRowCompleted(EventArgs.Empty);
                }
                else
                {
                    this.rowNew = isNew;
                    OnNewRowAdded(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Raises the RowDeleting event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRowDeleting(CancelEventArgs e)
        {
            if (this.RowDeleting != null)
            {
                RowDeleting(this, e);
            }
        }
        /// <summary>
        /// Raises the RowDeleted event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRowDeleted(EventArgs e)
        {
            if (this.RowDeleted != null)
            {
                RowDeleted(this, e);
            }
        }

        private bool OnCurrentRowChanging()
        {
            if (CurrentRowChanging != null)
            {
                //OnCurrentRowChanging();
                CancelEventArgs e = new CancelEventArgs();
                this.CurrentRowChanging(this, e);
                return !(e.Cancel);
            }
            return true;
        }

        //protected virtual void OnCurrentRowChanging(CancelEventArgs e)
        //{
        //    if (this.CurrentRowChanging != null)
        //    {
        //        CurrentRowChanging(this, e);
        //    }
        //}

        /// <summary>
        /// Raises the CurrentRowChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCurrentRowChanged(EventArgs e)
        {
            if (this.CurrentRowChanged != null)
            {
                CurrentRowChanged(this, e);
            }

        }
        /// <summary>
        /// Raises the CurrentCellChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCurrentCellChanged(EventArgs e)
        {
            if (m_SelectionType == SelectionType.FullRow || m_SelectionType == SelectionType.FullRow)
                this.Select(CurrentCell.RowNumber);

            if (this.CurrentCellChanged != null)
                this.CurrentCellChanged(this, e);
            //			EventHandler handler1 = base.Events[Grid.EVENT_CURRENTCELLCHANGED] as EventHandler;
            //			if (handler1 != null)
            //			{
            //				handler1(this, e);
            //			}
        }
        /// <summary>
        /// Raises the DataSourceChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDataSourceChanged(EventArgs e)
        {
            OnFilterChanged();

            if (this.DataSourceChanged != null)
            {
                try
                {
                    this.DataSourceChanged(this, e);
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }
            }
            //EventHandler handler1 = base.Events[Grid.EVENT_DATASOURCECHANGED] as EventHandler;
            //if (handler1 != null)
            //{
            //    handler1(this, e);
            //}
        }
        /// <summary>
        /// Raises the Enter event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            if (this.gridState[0x800] && !this.gridState[0x10000])
            {
                if (this.Bound)
                {
                    this.Edit();
                }
                base.OnEnter(e);
            }
        }
        /// <summary>
        /// Raises the FlatModeChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFlatModeChanged(EventArgs e)
        {
            if (this.FlatModeChanged != null)
                this.FlatModeChanged(this, e);
        }

        /// <summary>
        /// Raises the FontChanged event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            this.Caption.OnGridFontChanged();
            this.RecalculateFonts();
            this.RecreateGridRows(true);
            ResetHostingControls();
            //			if (this.originalState != null)
            //			{
            //				Stack stack1 = new Stack();
            //				while (!this.parentRows.IsEmpty())
            //				{
            //					GridState state1 = this.parentRows.PopTop();
            //					int num1 = state1.GridRowsLength;
            //					for (int num2 = 0; num2 < num1; num2++)
            //					{
            //						state1.GridRows[num2].Height = state1.GridRows[num2].MinimumRowHeight(state1.GridColumnStyles);
            //					}
            //					stack1.Push(state1);
            //				}
            //				while (stack1.Count != 0)
            //				{
            //					this.parentRows.AddParent((GridState) stack1.Pop());
            //				}
            //			}
            base.OnFontChanged(e);
        }
        /// <summary>
        /// Raises the ForeColorChanged event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnForeColorChanged(EventArgs e)
        {
            this.foreBrush = new SolidBrush(this.ForeColor);
            ResetHostingControls();
            base.Invalidate();
            base.OnForeColorChanged(e);
        }

        /// <summary>
        /// Raises the HandleCreated event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.toolTipProvider = new GridToolTip(this);
            this.toolTipProvider.CreateToolTipHandle();
            this.toolTipId = 0;
            base.PerformLayout();
            if (AutoAdjust)
            {
                OnResizeAdjustColumns();
            }

            //if (StatusBarVisible)
            //{
            //    StatusBarInitilaize(true);
            //    SummarizeColumns();
            //}
            //m_Caption.OnMenuHandel();

            //if (!DesignMode)// && !m_netFram)
            //{
            //   // Nistec.Win.Net.netGrid.NetFram(this.Name, "Mc");
            //    if(this.AllowColumnContextMenu)
            //    PerformColumnContextMenu();
            //}

        }
        /// <summary>
        /// Raises the HandleDestroyed event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            this.toolTipProvider.Destroy();
            this.toolTipProvider = null;
            this.toolTipId = 0;
        }
        /// <summary>
        /// Raises the KeyDown event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            this.ProcessGridKey(e);
        }
        /// <summary>
        /// Raises the KeyPress event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            if ((((collection1 != null) && (this.currentCol > 0)) && ((this.currentCol < collection1.Count) && !collection1[this.currentCol].ReadOnly)) && (e.KeyChar > ' '))
            {
                this.Edit(new string(new char[] { e.KeyChar }));
            }
        }
        /// <summary>
        /// Raises the Layout event
        /// </summary>
        /// <param name="levent"></param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!this.gridState[0x10000])
            {
                base.OnLayout(levent);
                this.gridState[0x800] = false;
                try
                {
                    if (base.IsHandleCreated)
                    {
                        //						if (this.layout.ParentRowsVisible)
                        //						{
                        //							this.parentRows.OnLayout();
                        //						}
                        if (this.ToolTipProvider != null)
                        {
                            this.ResetToolTip();
                        }
                        this.ComputeLayout();
                    }
                }
                finally
                {
                    this.gridState[0x800] = true;
                }
            }
        }
        /// <summary>
        /// Raises the Leave event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            if ((base.Disposing || base.IsDisposed) || !base.IsHandleCreated)
            {
                this.OnLeave_Grid();
            }
            else if (!this.gridState[0x10000] && base.IsHandleCreated)
            {
                base.BeginInvoke(new MethodInvoker(this.OnLeave_Grid));
            }
            base.OnLeave(e);
        }
        private void OnLeave_Grid()
        {
            this.gridState[0x800] = false;
            try
            {
                this.EndEdit();
                if ((this.listManager != null) && !this.gridState[0x10000])
                {
                    if (this.gridState[0x100000])
                    {
                        this.listManager.CancelCurrentEdit();
                        GridRow[] rowArray1 = this.GridRows;
                        rowArray1[this.GridRowsLength - 1] = new GridAddNewRow(this, this.myGridTable, this.GridRowsLength - 1);
                        this.SetGridRows(rowArray1, this.GridRowsLength);
                    }
                    else
                    {
                        this.HandleEndCurrentEdit();
                    }
                }
            }
            finally
            {
                this.gridState[0x800] = true;
                if (!this.gridState[0x10000])
                {
                    this.gridState[0x100000] = false;
                }
            }
        }
        /// <summary>
        /// Raises the MouseDown event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Grid.HitTestInfo info1;
            base.OnMouseDown(e);
            this.gridState[0x80000] = false;
            this.gridState[0x100] = false;
            if (this.listManager != null)
            {
                info1 = this.HitTest(e.X, e.Y);
                Keys keys1 = Control.ModifierKeys;
                bool flag1 = ((keys1 & Keys.Control) == Keys.Control) && ((keys1 & Keys.Alt) == Keys.None);
                bool flag2 = (keys1 & Keys.Shift) == Keys.Shift;
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }
                if (info1.type == Grid.HitTestType.ColumnResize)
                {
                    if (!this.myGridTable.GridColumnStyles[info1.col].Visible)
                    {
                        return;
                    }
                    if (e.Clicks > 1)
                    {
                        this.ColAutoResize(info1.col);
                        return;
                    }
                    this.ColResizeBegin(e, info1.col);
                    return;
                }
                if (info1.type == Grid.HitTestType.RowResize)
                {
                    if (e.Clicks > 1)
                    {
                        this.RowAutoResize(info1.row);
                        return;
                    }
                    this.RowResizeBegin(e, info1.row);
                    return;
                }
                if (info1.type == Grid.HitTestType.ColumnHeader)
                {
                    this.trackColumnHeader = this.myGridTable.GridColumnStyles[info1.col].PropertyDescriptor;
                    return;
                }
                if (info1.type == Grid.HitTestType.Caption)
                {
                    Rectangle rectangle1 = this.layout.Caption;
                    this.caption.MouseDown(e.X - rectangle1.X, e.Y - rectangle1.Y);
                    return;
                }
                if (this.layout.Data.Contains(e.X, e.Y) || this.layout.RowHeaders.Contains(e.X, e.Y))
                {
                    int num1 = this.GetRowFromY(e.Y);
                    if (num1 > -1)
                    {
                        Point point1 = this.NormalizeToRow(e.X, e.Y, num1);
                        if (this.GridRows[num1].OnMouseDown(point1.X, point1.Y, this.layout.RowHeaders, this.isRightToLeft()))
                        {
                            this.CommitEdit();
                            GridRow[] rowArray1 = this.GridRows;
                            //multi
                            if (((num1 < this.GridRowsLength) && (rowArray1[num1] is GridRelationshipRow)) && ((GridRelationshipRow)rowArray1[num1]).Expanded)
                            {
                                this.EnsureVisible(num1, 0);
                            }
                            this.Edit();
                            return;
                        }
                        else if (num1 == GridRows.Length-1)
                        {
                            this.CurrentRow = GridRows.Length-1;
                        }
                    }
                }
                if (info1.type != Grid.HitTestType.RowHeader)
                {
                    //					if (info1.type == Grid.HitTestType.ParentRows)
                    //					{
                    //						this.EndEdit();
                    //						this.parentRows.OnMouseDown(e.X, e.Y, this.isRightToLeft());
                    //					}
                    if ((info1.type == Grid.HitTestType.Cell) && !this.myGridTable.GridColumnStyles[info1.col].MouseDown(info1.row, e.X, e.Y))
                    {
                        GridCell cell1 = new GridCell(info1.row, info1.col);
                        if (this.policy.AllowEdit && this.CurrentCell.Equals(cell1))
                        {
                            this.ResetSelection();
                            this.EnsureVisible(this.currentRow, this.currentCol);
                            this.Edit();
                        }
                        else
                        {
                            this.ResetSelection();
                            this.CurrentCell = cell1;
                        }
                    }
                    return;
                }
                this.EndEdit();
                if (!(this.GridRows[info1.row] is GridAddNewRow))
                {
                    this.CurrentCell = new GridCell(info1.row, this.currentCol);
                }
                if (flag1)
                {
                    if (this.IsSelected(info1.row))
                    {
                        this.UnSelect(info1.row);
                        goto Label_0333;
                    }
                    this.Select(info1.row);
                    goto Label_0333;
                }
                if ((this.lastRowSelected == -1) || !flag2)
                {
                    this.ResetSelection();
                    this.Select(info1.row);
                    goto Label_0333;
                }
                int num2 = Math.Min(this.lastRowSelected, info1.row);
                int num3 = Math.Max(this.lastRowSelected, info1.row);
                int num4 = this.lastRowSelected;
                this.ResetSelection();
                this.lastRowSelected = num4;
                GridRow[] rowArray2 = this.GridRows;
                for (int num5 = num2; num5 <= num3; num5++)
                {
                    rowArray2[num5].Selected = true;
                    this.numSelectedRows++;
                }
                this.EndEdit();
            }
            return;
        Label_0333:
            this.lastRowSelected = info1.row;
        }
        /// <summary>
        /// Raises the MouseLeave event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (this.oldRow != -1)
            {
                this.GridRows[this.oldRow].OnMouseLeft(this.layout.RowHeaders, this.isRightToLeft());
            }
            if (this.gridState[0x40000])
            {
                this.caption.MouseLeft();
            }
            if (colHeaderHot > -1)
            {
                colHeaderHot = -1;
                this.Invalidate(this.layout.ColumnHeaders);
            }

            this.Cursor = null;
        }
        /// <summary>
        /// Raises the MouseMove event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.listManager != null)
            {
                Grid.HitTestInfo info1 = this.HitTest(e.X, e.Y);
                bool flag1 = this.isRightToLeft();
                if (this.gridState[8])
                {
                    this.ColResizeMove(e);
                }
                if (this.gridState[0x10])
                {
                    this.RowResizeMove(e);
                }
                if (this.gridState[8] || (info1.type == Grid.HitTestType.ColumnResize))
                {
                    this.Cursor = System.Windows.Forms.Cursors.SizeWE;
                }
                else if (this.gridState[0x10] || (info1.type == Grid.HitTestType.RowResize))
                {
                    this.Cursor = System.Windows.Forms.Cursors.SizeNS;
                }
                else
                {

                this.Cursor = null;
                if (info1.type == Grid.HitTestType.ColumnHeader)
                {
                    if (colHeaderHot != info1.col)
                    {
                        colHeaderHot = info1.col;
                        this.Invalidate(this.layout.ColumnHeaders);
                    }
                    return;
                }
                if (colHeaderHot > -1)
                {
                    colHeaderHot = -1;
                    this.Invalidate(this.layout.ColumnHeaders);
                }
                if (this.layout.Data.Contains(e.X, e.Y) || (this.layout.RowHeadersVisible && this.layout.RowHeaders.Contains(e.X, e.Y)))
                    {
                        GridRow[] rowArray1 = this.GridRows;
                        int num1 = this.GetRowFromY(e.Y);
                        if ((this.lastRowSelected != -1) && !this.gridState[0x100])
                        {
                            int num2 = this.GetRowTop(this.lastRowSelected);
                            int num3 = num2 + rowArray1[this.lastRowSelected].Height;
                            int num4 = SystemInformation.DragSize.Height;
                            this.gridState[0x100] = (((e.Y - num2) < num4) && ((num2 - e.Y) < num4)) || (((e.Y - num3) < num4) && ((num3 - e.Y) < num4));
                        }
                        if (num1 > -1)
                        {
                            Point point1 = this.NormalizeToRow(e.X, e.Y, num1);
                            if (!rowArray1[num1].OnMouseMove(point1.X, point1.Y, this.layout.RowHeaders, flag1) && this.gridState[0x100])
                            {
                                MouseButtons buttons1 = Control.MouseButtons;
                                if (((this.lastRowSelected != -1) && ((buttons1 & MouseButtons.Left) == MouseButtons.Left)) && (((Control.ModifierKeys & Keys.Control) != Keys.Control) || ((Control.ModifierKeys & Keys.Alt) != Keys.None)))
                                {
                                    int num5 = this.lastRowSelected;
                                    this.ResetSelection();
                                    this.lastRowSelected = num5;
                                    int num6 = Math.Min(this.lastRowSelected, num1);
                                    int num7 = Math.Max(this.lastRowSelected, num1);
                                    GridRow[] rowArray2 = this.GridRows;
                                    for (int num8 = num6; num8 <= num7; num8++)
                                    {
                                        rowArray2[num8].Selected = true;
                                        this.numSelectedRows++;
                                    }
                                }
                            }
                        }
                        if ((this.oldRow != num1) && (this.oldRow != -1))
                        {
                            rowArray1[this.oldRow].OnMouseLeft(this.layout.RowHeaders, flag1);
                        }
                        this.oldRow = num1;
                    }
                    //					if ((info1.type == Grid.HitTestType.ParentRows) && (this.parentRows != null))
                    //					{
                    //						this.parentRows.OnMouseMove(e.X, e.Y);
                    //					}
                    if (info1.type == Grid.HitTestType.Caption)
                    {
                        this.gridState[0x40000] = true;
                        Rectangle rectangle1 = this.layout.Caption;
                        this.caption.MouseOver(e.X - rectangle1.X, e.Y - rectangle1.Y);
                    }
                    else if (this.gridState[0x40000])
                    {
                        this.gridState[0x40000] = false;
                        this.caption.MouseLeft();
                    }
                    else if (info1.type == Grid.HitTestType.Cell)
                    {
                        this.myGridTable.GridColumnStyles[info1.col].MouseMove(info1.row, e.X, e.Y);
                    }
                }
            }
        }
        /// <summary>
        /// Raises the MouseUp event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.gridState[0x100] = false;
            if ((this.listManager != null) && (this.myGridTable != null))
            {
                if (this.gridState[8])
                {
                    this.ColResizeEnd(e);
                }
                if (this.gridState[0x10])
                {
                    this.RowResizeEnd(e);
                }
                this.gridState[8] = false;
                this.gridState[0x10] = false;
                Grid.HitTestInfo info1 = this.HitTest(e.X, e.Y);
                if ((info1.type & Grid.HitTestType.Caption) == Grid.HitTestType.Caption)
                {
                    this.caption.MouseUp(e.X, e.Y);
                }
                if ((info1.type == Grid.HitTestType.ColumnHeader) && (this.myGridTable.GridColumnStyles[info1.col].PropertyDescriptor == this.trackColumnHeader))
                {
                    this.ColumnHeaderClicked(this.trackColumnHeader);
                }
                this.trackColumnHeader = null;
                if (info1.type == Grid.HitTestType.Cell)
                {
                    this.myGridTable.GridColumnStyles[info1.col].MouseUp(info1.row,e);// e.X, e.Y);
                }
                else if (this.layout.TopLeftHeader.Contains(e.X, e.Y) && (this.layout.RowHeadersVisible && this.layout.ColumnHeadersVisible))
                {
                    this.SelectAll();
                }
                //else if (/*this.layout.Data.Contains(e.X, e.Y) ||*/ this.layout.RowHeaders.Contains(e.X, e.Y))
                //{
                //    int num1 = this.GetRowFromY(e.Y);
                //    if (num1 > -1)
                //    {
                //        Point point1 = this.NormalizeToRow(e.X, e.Y, num1);
                //        this.GridRows[num1].OnMouseUp(point1.X, point1.Y, this.layout.RowHeaders, this.isRightToLeft());
                //    }
                //}

            }
        }
        /// <summary>
        /// Raises the MouseWheel event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            bool flag1 = true;
            if ((Control.ModifierKeys & Keys.Control) != Keys.None)
            {
                flag1 = false;
            }
            if ((this.listManager != null) && (this.myGridTable != null))
            {
                ScrollBar bar1 = flag1 ? this.vertScrollBar : this.horizScrollBar;
                if (bar1.Visible)
                {
                    this.gridState[0x20000] = true;
                    this.wheelDelta += e.Delta;
                    float single1 = ((float)this.wheelDelta) / 120f;
                    int num1 = (int)(SystemInformation.MouseWheelScrollLines * single1);
                    if (num1 != 0)
                    {
                        this.wheelDelta = 0;
                        if (flag1)
                        {
                            int num2 = this.firstVisibleRow - num1;
                            num2 = Math.Max(0, Math.Min(num2, this.GridRowsLength - this.numTotallyVisibleRows));
                            this.ScrollDown(num2 - this.firstVisibleRow);
                        }
                        else
                        {
                            int num3 = this.horizScrollBar.Value + (((num1 < 0) ? 1 : -1) * this.horizScrollBar.LargeChange);
                            this.HorizontalOffset = num3;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Raises the Navigate event
        /// </summary>
        /// <param name="e"></param>
        internal protected void OnNavigate(NavigateEventArgs e)
        {
            if (this.Navigate != null)
            {
                this.Navigate(this, e);
            }
        }
        /// <summary>
        /// Raises the NodeClick event
        /// </summary>
        /// <param name="e"></param>
        internal void OnNodeClick(EventArgs e)
        {
            base.PerformLayout();
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            if (((this.firstVisibleCol > -1) && (this.firstVisibleCol < collection1.Count)) && (collection1[this.firstVisibleCol] == this.editColumn))
            {
                this.Edit();
            }
            if (this.NodeClick != null)
                this.NodeClick(this, e);
            //			EventHandler handler1 = (EventHandler) base.Events[Grid.EVENT_NODECLICKED];
            //			if (handler1 != null)
            //			{
            //				handler1(this, e);
            //			}
        }
        /// <summary>
        /// Raises the Paint event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            string step = "1";
            try
            {
                this.CheckHierarchyState();
                if (this.layout.dirty)
                {
                    this.ComputeLayout();
                }
                step = "2";
                Graphics graphics1 = e.Graphics;
                Region region1 = graphics1.Clip;
                if (this.layout.CaptionVisible)
                {
                    this.caption.Paint(graphics1, this.layout.Caption, this.isRightToLeft());
                }
                step = "3"; 
                Rectangle rectangle1 = this.layout.Data;
                if (this.layout.RowHeadersVisible)
                {
                    rectangle1 = Rectangle.Union(rectangle1, this.layout.RowHeaders);
                }
                if (this.layout.ColumnHeadersVisible)
                {
                    rectangle1 = Rectangle.Union(rectangle1, this.layout.ColumnHeaders);
                }
                step = "4";
                graphics1.SetClip(rectangle1);
                this.PaintGrid(graphics1, rectangle1);
                graphics1.Clip = region1;
                region1.Dispose();
                step = "5";
                this.PaintBorder(graphics1, this.layout.ClientRectangle);
                graphics1.FillRectangle(Grid.DefaultHeaderBackBrush, this.layout.ResizeBoxRect);
                step = "6";
                base.OnPaint(e);
            }
            catch (Exception ex)
            {
                if ((this.Site == null) || !this.Site.DesignMode)
                {
                    OnErrorOccouerd("Paint : Step:" + step + " Error " + ex.Message);
                    /*site*/
                    return;//throw;
                }
                this.gridState[0x800000] = true;
                try
                {
                    //MessageBox.Show("GridExceptionInPaint");
                    this.BeginUpdateInternal();
                    //this.parentRows.Clear();
                    //this.caption.BackButtonActive = this.caption.DownButtonActive = this.caption.BackButtonVisible = false;
                    this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
                    this.originalState = null;
                    this.Set_ListManager(null, string.Empty, true);
                    return;
                }
                finally
                {
                    this.gridState[0x800000] = false;
                    this.EndUpdateInternal();
                }
            }
        }
        /// <summary>
        /// Raises the PaintBackground event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        //		protected virtual void OnParentRowsLabelStyleChanged(EventArgs e)
        //		{
        //			if(ParentRowsLabelStyleChanged!=null)
        //					ParentRowsLabelStyleChanged(this,e);
        //
        //		}
        //
        //		protected virtual void OnParentRowsVisibleChanged(EventArgs e)
        //		{
        //				if(this.ParentRowsVisibleChanged!=null)
        //					this.ParentRowsVisibleChanged(this,e);
        //
        //		}

        /// <summary>
        /// Raises the ReadOnlyChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnReadOnlyChanged(EventArgs e)
        {
            if (this.ReadOnlyChanged != null)
                this.ReadOnlyChanged(this, e);

        }
        /// <summary>
        /// Raises the RightToLeftChanged event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            ResetHostingControls();
            base.OnRightToLeftChanged(e);
        }
        /// <summary>
        /// Raises the Resize event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            if (this.layout.CaptionVisible)
            {
                base.Invalidate(this.layout.Caption);
            }
            int num1 = this.BorderWidth;
            Rectangle rectangle3 = this.layout.ClientRectangle;
            Rectangle rectangle1 = new Rectangle((rectangle3.X + rectangle3.Width) - num1, rectangle3.Y, num1, rectangle3.Height);
            Rectangle rectangle2 = new Rectangle(rectangle3.X, (rectangle3.Y + rectangle3.Height) - num1, rectangle3.Width, num1);
            Rectangle rectangle4 = base.ClientRectangle;
            if (rectangle4.Width != rectangle3.Width)
            {
                base.Invalidate(rectangle1);
                rectangle1 = new Rectangle((rectangle4.X + rectangle4.Width) - num1, rectangle4.Y, num1, rectangle4.Height);
                base.Invalidate(rectangle1);
            }
            if (rectangle4.Height != rectangle3.Height)
            {
                base.Invalidate(rectangle2);
                rectangle2 = new Rectangle(rectangle4.X, (rectangle4.Y + rectangle4.Height) - num1, rectangle4.Width, num1);
                base.Invalidate(rectangle2);
            }
            if (!this.layout.ResizeBoxRect.IsEmpty)
            {
                base.Invalidate(this.layout.ResizeBoxRect);
            }
            this.layout.ClientRectangle = rectangle4;
            int num2 = this.firstVisibleRow;
            base.OnResize(e);
            if (this.isRightToLeft() || (num2 != this.firstVisibleRow))
            {
                base.Invalidate();
            }
            if (AutoAdjust)
            {
                //AdjustColumns();
                OnResizeAdjustColumns();
            }
        }
        /// <summary>
        /// Raises the RowHeaderClick event
        /// </summary>
        /// <param name="e"></param>
         protected void OnRowHeaderClick(EventArgs e)
        {
            if (this.RowHeaderClick != null)
            {
                this.RowHeaderClick(this, e);
            }
        }

        internal void OnRowHeightChanged(GridRow row)
        {
            this.ClearRegionCache();
            int num1 = this.GetRowTop(row.RowNumber);
            if (num1 > 0)
            {
                Rectangle rectangle1 = new Rectangle();
                rectangle1.Y = num1;
                rectangle1.X = this.layout.Inside.X;
                rectangle1.Width = this.layout.Inside.Width;
                rectangle1.Height = this.layout.Inside.Bottom - num1;
                base.Invalidate(rectangle1);
            }
        }

        /// <summary>
        /// Raises the Scroll event
        /// </summary>
        /// <param name="e"></param>
        protected void OnScroll(EventArgs e)
        {
            if (this.ToolTipProvider != null)
            {
                this.ResetToolTip();
            }

            if (this.Scroll != null)
                this.Scroll(this, e);

        }

        //		protected void OnShowParentDetailsButtonClicked(object sender, EventArgs e)
        //		{
        //			if(ShowParentDetailsButtonClick!=null)
        //				this.ShowParentDetailsButtonClick(this,e);
        //			this.ParentRowsVisible = !this.caption.ToggleDownButtonDirection();
        //		}

        #endregion

        #region Paint

        private void PaintBorder(Graphics g, Rectangle bounds)
        {
            if (this.BorderStyle != BorderStyle.None)
            {
                if (this.BorderStyle == BorderStyle.Fixed3D)
                {
                    Border3DStyle style1 = Border3DStyle.Sunken;
                    ControlPaint.DrawBorder3D(g, bounds, style1);
                }
                else if (this.BorderStyle == BorderStyle.FixedSingle)
                {
                    using (Pen pn = this.LayoutManager.GetPenBorder())
                    {
                        bounds.Width--;
                        bounds.Height--;
                        g.DrawRectangle(pn, bounds);

                    }
                }
                else
                {
                    Pen pen1 = SystemPens.WindowFrame;
                    bounds.Width--;
                    bounds.Height--;
                    g.DrawRectangle(pen1, bounds);
                }
            }
        }

        private void PaintColumnHeaders(Graphics g)
        {
            bool flag1 = this.isRightToLeft();
            Rectangle rectangle1 = this.layout.ColumnHeaders;
            if (!flag1)
            {
                rectangle1.X -= this.negOffset;
            }
            rectangle1.Width += this.negOffset;
            int num1 = this.PaintColumnHeaderText(g, rectangle1);
            if (flag1)
            {
                rectangle1.X = rectangle1.Right - num1;
            }
            rectangle1.Width = num1;
            if (!this.FlatMode)
            {
                ControlPaint.DrawBorder3D(g, rectangle1, Border3DStyle.RaisedInner);
                rectangle1.Inflate(-1, -1);
                rectangle1.Width--;
                rectangle1.Height--;
                g.DrawRectangle(/*new Pen(Brushes.LightGray, 2)*/SystemPens.Control, rectangle1);
            }
        }

        private int PaintColumnHeaderText(Graphics g, Rectangle boundingRect)
        {
            int num1 = 0;
            Rectangle rectangle1 = boundingRect;
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            bool rtl = this.isRightToLeft();
            int num2 = collection1.Count;
            PropertyDescriptor descriptor1 = null;
            descriptor1 = this.ListManager.GetSortProperty();
            for (int num3 = this.firstVisibleCol; num3 < num2; num3++)
            {
                if (collection1[num3].IsVisibleInternal)/*bound*///.PropertyDescriptor != null)
                {
                    Brush brush1;
                    if (num1 > boundingRect.Width)
                    {
                        break;
                    }
                    bool flag2 = false;
                    TriangleDirection direction1 = TriangleDirection.Up;
                    /*bound*/
                    if (collection1[num3].IsBound)
                    {
                        flag2 = (descriptor1 != null) && descriptor1.Equals(collection1[num3].PropertyDescriptor);
                        //TriangleDirection direction1 = TriangleDirection.Up;
                        if (flag2)
                        {
                            ListSortDirection direction2 = this.ListManager.GetSortDirection();
                            if (direction2 == ListSortDirection.Descending)
                            {
                                direction1 = TriangleDirection.Down;
                            }
                        }
                    }
                    if (rtl)
                    {
                        rectangle1.Width = collection1[num3].Width - (flag2 ? rectangle1.Height : 0);
                        rectangle1.X = (boundingRect.Right - num1) - rectangle1.Width;
                    }
                    else
                    {
                        rectangle1.X = boundingRect.X + num1;
                        rectangle1.Width = collection1[num3].Width - (flag2 ? rectangle1.Height : 0);
                    }
                    //if (this.myGridTable.IsDefault)
                    //{
                    //    brush1 = this.HeaderBackBrush;
                    //}
                    //else
                    //{
                    //    brush1 = this.headerBackBrush;//TableStyle: this.myGridTable.HeaderBackBrush;
                    //}
                    /*ControlLayout*/
                    brush1 = GetHeaderBackBrush(num3==colHeaderHot, rectangle1);
                    g.FillRectangle(brush1, rectangle1);
                    if (rtl)
                    {
                        rectangle1.X -= 2;
                        rectangle1.Y += 2;
                    }
                    else
                    {
                        rectangle1.X += 2;
                        rectangle1.Y += 2;
                    }

                    StringFormat format1 = new StringFormat();
                    //HorizontalAlignment alignment1 = collection1[num3].Alignment;
                    HorizontalAlignment alignment1 = this.columnsHeaderAlignment;// rtl ? HorizontalAlignment.Right : HorizontalAlignment.Left;
                    format1.Alignment = (alignment1 == HorizontalAlignment.Right) ? StringAlignment.Far : ((alignment1 == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Near);
                    format1.FormatFlags |= StringFormatFlags.NoWrap;
                    if (rtl)
                    {
                        format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                        format1.Alignment = StringAlignment.Near;
                    }
                    g.DrawString(collection1[num3].HeaderText, this.myGridTable.IsDefault ? this.HeaderFont : this.myGridTable.dataGrid.HeaderFont, this.myGridTable.IsDefault ? this.HeaderForeBrush : this.myGridTable.dataGrid.HeaderForeBrush, (RectangleF)rectangle1, format1);
                    format1.Dispose();
                    if (rtl)
                    {
                        rectangle1.X += 2;
                        rectangle1.Y -= 2;
                    }
                    else
                    {
                        rectangle1.X -= 2;
                        rectangle1.Y -= 2;
                    }
                    if (flag2)
                    {
                        Rectangle rectangle2 = new Rectangle(rtl ? (rectangle1.X - rectangle1.Height) : rectangle1.Right, rectangle1.Y, rectangle1.Height, rectangle1.Height);
                        g.FillRectangle(brush1, rectangle2);
                        int num4 = Math.Max(0, (rectangle1.Height - 5) / 2);
                        rectangle2.Inflate(-num4, -num4);
                        Pen pen1 = LayoutManager.GetPenBorder();// new Pen(this.BackgroundBrush);
                        Pen pen2 = pen1;// new Pen(this.myGridTable.dataGrid.BackBrush);
                        Triangle.Paint(g, rectangle2, direction1, brush1, pen1, pen2, pen1, true);
                        pen1.Dispose();
                        pen2.Dispose();
                    }
                    int num5 = rectangle1.Width + (flag2 ? rectangle1.Height : 0);
                    if (!this.FlatMode)
                    {
                        if (rtl && flag2)
                        {
                            rectangle1.X -= rectangle1.Height;
                        }
                        rectangle1.Width = num5;
                        ControlPaint.DrawBorder3D(g, rectangle1, Border3DStyle.RaisedInner);
                    }
                    num1 += num5;
                }
            }
            if (num1 < boundingRect.Width)
            {
                rectangle1 = boundingRect;
                if (!rtl)
                {
                    rectangle1.X += num1;
                }
                rectangle1.Width -= num1;
                g.FillRectangle(this.backgroundBrush, rectangle1);
            }
            return num1;
        }


        private void PaintGrid(Graphics g, Rectangle gridBounds)
        {
            Rectangle rectangle1 = gridBounds;
            if (this.listManager != null)
            {
                if (this.layout.ColumnHeadersVisible)
                {
                    Region region1 = g.Clip;
                    g.SetClip(this.layout.ColumnHeaders);
                    this.PaintColumnHeaders(g);
                    g.Clip = region1;
                    region1.Dispose();
                    int num1 = this.layout.ColumnHeaders.Height;
                    rectangle1.Y += num1;
                    rectangle1.Height -= num1;
                }
                if (this.layout.TopLeftHeader.Width > 0)
                {
                    /*ControlLayout*/
                    Rectangle rct = this.layout.TopLeftHeader;
                     g.FillRectangle(GetHeaderBackBrush(false,rct), rct);
                    //if (this.myGridTable.IsDefault)
                    //{
                    //    g.FillRectangle(this.HeaderBackBrush, this.layout.TopLeftHeader);
                    //}
                    //else
                    //{
                    //    g.FillRectangle(this.myGridTable.dataGrid.HeaderBackBrush, this.layout.TopLeftHeader);
                    //}
                    if (!this.FlatMode)
                    {
                        ControlPaint.DrawBorder3D(g, this.layout.TopLeftHeader, Border3DStyle.RaisedInner);
                    }
                }
                this.PaintRows(g, ref rectangle1);
            }
            if (rectangle1.Height > 0)
            {
                g.FillRectangle(this.backgroundBrush, rectangle1);
            }
        }

        private void PaintRows(Graphics g, ref Rectangle boundingRect)
        {
            int num1 = 0;
            bool flag1 = this.isRightToLeft();
            Rectangle rectangle1 = boundingRect;
            Rectangle rectangle2 = Rectangle.Empty;
            bool flag2 = this.layout.RowHeadersVisible;
            Rectangle rectangle3 = Rectangle.Empty;
            int rowsCnt = this.GridRowsLength;
            GridRow[] rowArray1 = this.GridRows;
            int num3 = this.myGridTable.GridColumnStyles.Count - this.firstVisibleCol;
            for (int rowIndx = this.firstVisibleRow; rowIndx < rowsCnt; rowIndx++)
            {
                if (num1 > boundingRect.Height)
                {
                    break;
                }
                rectangle1 = boundingRect;
                rectangle1.Height = rowArray1[rowIndx].Height;
                rectangle1.Y = boundingRect.Y + num1;
                if (flag2)
                {
                    rectangle3 = rectangle1;
                    rectangle3.Width = this.layout.RowHeaders.Width;
                    if (flag1)
                    {
                        rectangle3.X = (rectangle1.Right) - rectangle3.Width;
                    }
                    if (g.IsVisible(rectangle3))
                    {
                        rowArray1[rowIndx].PaintHeader(g, rectangle3, flag1, this.gridState[0x8000], this.rowDirty);
                        g.ExcludeClip(rectangle3);
                    }
                    if (!flag1)
                    {
                        rectangle1.X += rectangle3.Width;
                    }
                    rectangle1.Width -= rectangle3.Width;
                }
                if (g.IsVisible(rectangle1))
                {
                    rectangle2 = rectangle1;
                    if (!flag1)
                    {
                        rectangle2.X -= this.negOffset;
                    }
                    rectangle2.Width += this.negOffset;
                    rowArray1[rowIndx].Paint(g, rectangle2, rectangle1, this.firstVisibleCol, num3, flag1);
                }
                num1 += rectangle1.Height;
            }
            boundingRect.Y += num1;
            boundingRect.Height -= num1;
        }

        #endregion

        #region Process

      
        private void PairTableStylesAndGridColumns(BindManager lm, GridTableStyle gridTable, bool forceColumnCreation)
        {
            if (lm == null)
                return;
            PropertyDescriptorCollection collection1 = lm.GetItemProperties();
            GridColumnCollection collection2 = gridTable.GridColumnStyles;
            //if (!gridTable.IsDefault && (string.Compare(GetListName(), gridTable.MappingName, true) == 0))
            if (!gridTable.IsDefault && (string.Compare(lm.GetListName(), gridTable.MappingName, true) == 0))
            {
                if ((gridTable.GridColumnStyles.Count == 0) && !base.DesignMode)
                {
                    if (forceColumnCreation)
                    {
                        gridTable.SetGridColumnStylesCollection(lm);
                    }
                }
                else
                {
                    for (int i = 0; i < collection2.Count; i++)
                    {
                        collection2[i].PropertyDescriptor = null;
                        collection2[i].isMaped = false;
                    }
                    int colIndex = 0;
                    for (int indx = 0; indx < collection1.Count && colIndex < collection2.Count; indx++)
                    {
                        //bool isDesc = false;
                        GridColumnStyle style1 = collection2.MapColumnStyleToPropertyName(collection1[indx].Name, collection2[colIndex], colIndex);//, ref isDesc);
                        if (style1 != null)
                        {
                            style1.isMaped = true;

                            if (style1.IsBound)//isDesc)
                            {
                                style1.PropertyDescriptor = collection1[indx];
                            }
                            else
                            {
                                //style1.CellBoundInit(lm.Count);
                                indx--;
                            }
                            colIndex++;
                        }
                    }
                    if (relations != null)
                    {
                        gridTable.SetRelationsList(relations);// (lm);
                    }
                }
            }
            else
            {
                gridTable.SetGridColumnStylesCollection(lm);
                if ((gridTable.GridColumnStyles.Count > 0) && (gridTable.GridColumnStyles[0].Width == -1))
                {
                    this.InitializeColumnWidths();
                }
            }
        }

        private void PreferredColumnWidthChanged(object sender, EventArgs e)
        {
            this.SetGridRows(null, this.GridRowsLength);
            base.PerformLayout();
            base.Invalidate();
        }
        /// <summary>
        /// Processes a dialog key
        /// </summary>
        /// <param name="keyData"></param>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            GridRow[] rowArray1 = this.GridRows;
            if (((this.listManager != null) && (this.GridRowsLength > 0)) && rowArray1[this.currentRow].OnKeyPress(keyData))
            {
                return true;
            }
            switch ((keyData & Keys.KeyCode))
            {
                case Keys.Add:
                case Keys.Subtract:
                case Keys.Oemplus:
                case Keys.OemMinus:
                case Keys.A:
                case Keys.F:
                case Keys.Delete:
                case Keys.Escape:
                case Keys.Space:
                case Keys.Prior:
                case Keys.Next:
                case Keys.Left:
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                case Keys.Return:
                case Keys.Tab:
                    {
                        KeyEventArgs args1 = new KeyEventArgs(keyData);
                        if (this.ProcessGridKey(args1))
                        {
                            return true;
                        }
                        break;
                    }
                case Keys.C:
                    if ((((keyData & Keys.Control) != Keys.None) && ((keyData & Keys.Alt) == Keys.None)) && this.Bound)
                    {
                        SetDataToClipboard(true);
                        //return true;
                        //if (this.numSelectedRows == 0)
                        //{
                        //    if (this.currentRow < this.ListManager.Count)
                        //    {
                        //        bool copyDisplayText = ((keyData & Keys.KeyCode) == Keys.C) ? true : false;

                        //        GridColumnStyle style1 = this.myGridTable.GridColumnStyles[this.currentCol];
                        //        string text1 = copyDisplayText ? style1.GetDisplayText(style1.GetColumnValueAtRow(this.ListManager, this.currentRow)) : style1.GetDisplayText(style1.GetColumnValueAtRow(this.ListManager, this.currentRow));
                        //        Clipboard.SetDataObject(text1);
                        //        return true;
                        //    }
                        //}
                        //else
                        //{
                        //    int num1 = 0;
                        //    string text2 = "";
                        //    for (int num2 = 0; num2 < this.GridRowsLength; num2++)
                        //    {
                        //        if (rowArray1[num2].Selected)
                        //        {
                        //            GridColumnCollection collection2 = this.myGridTable.GridColumnStyles;
                        //            int num3 = collection2.Count;
                        //            for (int num4 = 0; num4 < num3; num4++)
                        //            {
                        //                GridColumnStyle style2 = collection2[num4];
                        //                text2 = text2 + style2.GetValueText(style2.GetColumnValueAtRow(this.ListManager, num2));
                        //                if (num4 < (num3 - 1))
                        //                {
                        //                    text2 = text2 + this.GetOutputTextDelimiter();
                        //                }
                        //            }
                        //            if (num1 < (this.numSelectedRows - 1))
                        //            {
                        //                text2 = text2 + "\r\n";
                        //            }
                        //            num1++;
                        //        }
                        //    }
                        //    Clipboard.SetDataObject(text2);
                        //    return true;
                        //}
                    }
                    break;
                case Keys.V:
                    if ((((keyData & Keys.Control) != Keys.None) && ((keyData & Keys.Alt) == Keys.None)) && this.Bound)
                    {
                        SetDataFromClipboard();
                        //return true;
                    }
                    break;
                case Keys.Insert:
                    //copy
                    if ((((keyData & Keys.Control) != Keys.None) && ((keyData & Keys.Alt) == Keys.None)) && this.Bound)
                    {
                        //bool copyDisplayText = ((keyData & Keys.KeyCode) == Keys.C) ? true : false;
                        SetDataToClipboard(false);
                        //return true;
                    }
                    //paste
                    else if ((((keyData & Keys.Shift) != Keys.None) && ((keyData & Keys.Alt) == Keys.None)) && this.Bound)
                    {
                        SetDataFromClipboard();
                        //return true;
                    }
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Set Data To Clipboard
        /// </summary>
        /// <param name="copyDisplayText">if true copy the display text from cells else copy the cells value</param>
        /// <returns></returns>
        public void SetDataToClipboard(bool copyDisplayText)
        {
            
            //bool copyDisplayText = ((keyData & Keys.KeyCode) == Keys.C) ? true : false;
            if (this.numSelectedRows == 0)
            {
                Control c = GetCurrentColumnControl(true);
                if (c != null)
                {
                    Clipboard.SetText(c.Text);
                    return;
                }

                if (this.currentRow < this.ListManager.Count)
                {
                    GridColumnStyle style1 = this.myGridTable.GridColumnStyles[this.currentCol];
                    string text1 = copyDisplayText ? style1.GetDisplayText(style1.GetColumnValueAtRow(this.ListManager, this.currentRow)) : style1.GetValueText(style1.GetColumnValueAtRow(this.ListManager, this.currentRow));
                    Clipboard.SetDataObject(text1);
                    //return true;
                }
            }
            else
            {
                int num1 = 0;
                //string text2 = "";
                string text = "";
                object value = null;
                StringBuilder sb = new StringBuilder();
                GridRow[] rowArray1 = this.GridRows;

                for (int row = 0; row < this.GridRowsLength; row++)
                {
                    if (rowArray1[row].Selected)
                    {
                        GridColumnCollection collection2 = this.myGridTable.GridColumnStyles;
                        int colCount = collection2.Count;
                        for (int col = 0; col < colCount; col++)
                        {
                            GridColumnStyle colStyle = collection2[col];
                            value = colStyle.GetColumnValueAtRow(this.ListManager, row);
                            text = copyDisplayText ? colStyle.GetDisplayText(value) : colStyle.GetValueText(value); 
                            //text2 = text2 + colStyle.GetValueText(colStyle.GetColumnValueAtRow(this.ListManager, row));
                            sb.Append(text);
                            if (col < (colCount - 1))
                            {
                                //text2 = text2 + this.GetOutputTextDelimiter();
                                sb.Append(this.GetOutputTextDelimiter());
                            }
                        }
                        if (num1 < (this.numSelectedRows - 1))
                        {
                            //text2 = text2 + "\r\n";
                            sb.Append("\r\n");
                        }
                        num1++;
                    }
                }

                Clipboard.SetDataObject(sb.ToString());//text2);
                //return true;
            }

        }
        /// <summary>
        /// Set Data from Clipboard
        /// </summary>
        public bool SetDataFromClipboard()
        {
            if (this.ReadOnly)
            {
                return true;
            }
            //_Message = "";
            try
            {

                if (this.numSelectedRows == 0)
                {

                    Control c = GetCurrentColumnControl(false);
                    if (c != null)
                    {
                        SetCurrentColumnValue(false, Clipboard.GetText());

                        //c.Text = Clipboard.GetText();
                        return true;
                    }
                }

                //Read the copied data from the Clipboard
                IDataObject objExcel = Clipboard.GetDataObject();

                //Proceed if some copied data is present
                if (objExcel == null)
                    return false;

                //Define a DataTable to hold the copied data for binding to the DataGrid
                int cols = this.GridColumns.Count;

                int rowsToPaste = this.numSelectedRows;
                bool shouldAddNew = false;
                int currentRow = this.CurrentRowIndex;
                int rowCopyed = 0;

                if (currentRow == this.RowCount - 1)
                {
                    if (this.AllowAdd)
                    {
                        rowsToPaste = int.MaxValue;
                        shouldAddNew = true;
                    }
                    else
                    {
                        rowsToPaste = 1;
                    }
                }

                //Next proceed only of the copied data is in the CSV format indicating Excel content
                if (objExcel.GetDataPresent(DataFormats.CommaSeparatedValue))
                {
                    //Cast the copied data in the CommaSeparatedValue format & hold in a StreamReader Object
                    using (System.IO.StreamReader srReadExcel = new System.IO.StreamReader((System.IO.Stream)objExcel.GetData(DataFormats.CommaSeparatedValue), Encoding.Default))
                    {
                        string sFormattedData = "";
                        //string s = srReadExcel.ReadToEnd();

                        //Set the delimiter character for use in splitting the copied data
                        char[] charDelimiter = new char[] { ',' };

                        //Loop till no further data is available
                        while (srReadExcel.Peek() > 0)
                        {

                            if (rowCopyed >= rowsToPaste)
                            {
                                break;
                            }
                            //Array to hold the split data for each row
                            System.Array arrSplitData = null;

                            int i = 0;

                            //Read a line of data from the StreamReader object
                            sFormattedData = srReadExcel.ReadLine();
                            //char[] ary = sFormattedData.ToCharArray();

                            //Split the string contents into an array
                            arrSplitData = sFormattedData.Split(charDelimiter, StringSplitOptions.None);// (charDelimiterArray);

                            //this.CurrentRow=this.RowCount;

                            int colSplit = arrSplitData.Length;//.GetUpperBound(0);
                            if (colSplit != cols)
                            {
                                //throw new ArgumentException("Error split DataFormats , Try format cells to general format or remove Comma Separated from cells;");
                                return false;
                            }

                            else //if (colSplit == cols)
                            {
                                for (i = 0; i < cols; i++)
                                {
                                    try
                                    {
                                        this[i] = arrSplitData.GetValue(i);//.ToString().TrimStart(new char[]{'"'}).TrimEnd(new char[]{'"'});
                                    }
                                    catch { }
                                }
                            }
                            rowCopyed++;
                            i = 0;
                            if (shouldAddNew)
                            {
                                AddNewRow();
                            }
                            else
                            {
                                currentRow++;
                                this.CurrentRowIndex = currentRow;
                            }
                        }

                        //Close the StreamReader object
                        srReadExcel.Close();
                    }
                    return rowCopyed>0;
                    //Bind the data to the DataGrid
                    //dgrExcelContents.DataSource = tbl.DefaultView();
                }
                //Next proceed only of the copied data is in the CSV format indicating Excel content
                else if (objExcel.GetDataPresent(DataFormats.UnicodeText) || objExcel.GetDataPresent(DataFormats.Text))
                {
                    //Cast the copied data in the CommaSeparatedValue format & hold in a StreamReader Object
                    using (System.IO.StringReader srReadExcel = new System.IO.StringReader((string)objExcel.GetData(DataFormats.UnicodeText,true)))//, Encoding.Default))
                    {
                        string sFormattedData = "";

                        //Set the delimiter character for use in splitting the copied data
                        string[] charDelimiter = new string[] { "\t" };

                        //Loop till no further data is available
                        while (srReadExcel.Peek() > 0)
                        {
                            if (rowCopyed >= rowsToPaste)
                            {
                                break;
                            }
                            //Array to hold the split data for each row
                            System.Array arrSplitData = null;

                            int i = 0;

                            //Read a line of data from the StreamReader object
                            sFormattedData = srReadExcel.ReadLine();
                            //char[] ary = sFormattedData.ToCharArray();

                            //Split the string contents into an array
                            arrSplitData = sFormattedData.Split(charDelimiter, StringSplitOptions.None);// (charDelimiterArray);

                            //this.CurrentRow=this.RowCount;

                            int colSplit = arrSplitData.Length;//.GetUpperBound(0);
                            if (colSplit != cols)
                            {
                                //throw new ArgumentException("Error split DataFormats , Try format cells to general format or remove Comma Separated from cells;");
                                return false;
                            }

                            else //if (colSplit == cols)
                            {
                                //if(this.s
                                for (i = 0; i < cols; i++)
                                {
                                    try
                                    {
                                        this[i] = arrSplitData.GetValue(i);//.ToString().TrimStart(new char[]{'"'}).TrimEnd(new char[]{'"'});
                                    }
                                    catch (Exception ex) { OnErrorOccouerd(ex.Message); }
                                }
                            }

                            rowCopyed++;
                            i = 0;
                            if (shouldAddNew)
                            {
                                AddNewRow();
                            }
                            else
                            {
                                currentRow++;
                                this.CurrentRowIndex = currentRow;
                            }
                        }

                        //Close the StreamReader object
                        srReadExcel.Close();
                    }
                    return rowCopyed>0;
                    //Bind the data to the DataGrid
                    //dgrExcelContents.DataSource = tbl.DefaultView();
                }
                else
                {
                    return false;
                    // _Message = exp.Message;
                    //MsgBox.ShowWarning("Clipboard data does not seem to be copied from Excel!");
                }
            }
            catch (Exception exp)
            {
                //throw exp;
                MsgBox.ShowError(exp.Message);
                return false;
            }

        }


   

        /// <summary>
        /// Processes a Grid key
        /// </summary>
        /// <param name="ke"></param>
        [SecurityPermission(SecurityAction.LinkDemand)]
        protected bool ProcessGridKey(KeyEventArgs ke)
        {
            if ((this.listManager == null) || (this.myGridTable == null))
            {
                return false;
            }
            GridRow[] rowArray1 = this.GridRows;
            KeyEventArgs keyArgs = ke;
            if (this.isRightToLeft())
            {
                switch (ke.KeyCode)
                {
                    case Keys.Left:
                        keyArgs = new KeyEventArgs(Keys.Right | ke.Modifiers);
                        break;

                    case Keys.Right:
                        keyArgs = new KeyEventArgs(Keys.Left | ke.Modifiers);
                        break;
                }
            }
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            int num1 = 0;
            int colCnt = collection1.Count;
            for (int num3 = 0; num3 < collection1.Count; num3++)
            {
                if (collection1[num3].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                {
                    num1 = num3;
                    break;
                }
            }
            for (int num4 = collection1.Count - 1; num4 >= 0; num4--)
            {
                if (collection1[num4].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                {
                    colCnt = num4;
                    break;
                }
            }
            switch (keyArgs.KeyCode)
            {
 
                case Keys.Oemplus:
                case Keys.Add:
                    this.gridState[0x80000] = false;
                    if (keyArgs.Control)
                    {
                        //multi
                        this.SetRowExpansionState(-1, true);
                        this.EndEdit();
                        return true;
                    }
                    return false;

                case Keys.Oemcomma:
                case Keys.Separator:
                case Keys.IMEConvert:
                case Keys.IMENonconvert:
                case Keys.IMEAceept:
                case Keys.IMEModeChange:
                case Keys.Select:
                case Keys.Print:
                case Keys.Execute:
                case Keys.Snapshot:
                case Keys.Insert:
                    goto Label_0D3A;

                case Keys.OemMinus:
                case Keys.Subtract:
                    this.gridState[0x80000] = false;
                    if (keyArgs.Control && !keyArgs.Alt)
                    {
                        //multi
                        this.SetRowExpansionState(-1, false);
                        return true;
                    }
                    return false;

                case Keys.F2:
                    this.gridState[0x80000] = false;
                    this.ResetSelection();
                    this.Edit();
                    goto Label_0D3A;

                case Keys.A:
                    {
                        this.gridState[0x80000] = false;
                        if (!keyArgs.Control || keyArgs.Alt)
                        {
                            return false;
                        }
                        SelectAll();
                        //GridRow[] rowArray11 = this.GridRows;
                        //for (int num20 = 0; num20 < this.GridRowsLength; num20++)
                        //{
                        //    if (rowArray11[num20] is GridRelationshipRow)
                        //    {
                        //        rowArray11[num20].Selected = true;
                        //    }
                        //}
                        //this.numSelectedRows = this.GridRowsLength - (this.policy.AllowAdd ? 1 : 0);
                        //this.EndEdit();
                        return true;
                    }
                case Keys.F:
                    {
                        if (!keyArgs.Control)
                        {
                            return false;
                        }
                        this.gridState[0x80000] = false;
                        this.ResetSelection();
                        this.EndEdit();
                        GridPerform.Find(this);// this.PerformFind();
                        goto Label_0D3A;
                    }
                case Keys.Escape:
                    this.gridState[0x80000] = false;
                    this.ResetSelection();
                    if (this.gridState[0x8000])
                    {
                        this.AbortEdit();
                        if (this.layout.RowHeadersVisible && (this.currentRow > -1))
                        {
                            Rectangle rectangle1 = this.GetRowRect(this.currentRow);
                            rectangle1.Width = this.layout.RowHeaders.Width;
                            base.Invalidate(rectangle1);
                        }
                        this.Edit();
                    }
                    else
                    {
                        this.CancelEditing();
                        this.Edit();
                        return false;
                    }
                    goto Label_0D3A;

                case Keys.Space:
                    this.gridState[0x80000] = false;
                    if (keyArgs.Shift)
                    {
                        this.ResetSelection();
                        this.EndEdit();
                        this.GridRows[this.currentRow].Selected = true;
                        this.numSelectedRows = 1;
                        return true;
                    }
                    return false;

                case Keys.Prior:
                    {
                        this.gridState[0x80000] = false;
                        if (!keyArgs.Shift)
                        {
                            if (keyArgs.Control && !keyArgs.Alt)
                            {
                                //this.ParentRowsVisible = false;
                                goto Label_0D3A;
                            }
                            this.ResetSelection();
                            this.CurrentRow = Math.Max(0, this.CurrentRow - this.numTotallyVisibleRows);
                            goto Label_0D3A;
                        }
                        int num11 = this.currentRow;
                        this.CurrentRow = Math.Max(0, this.CurrentRow - this.numTotallyVisibleRows);
                        GridRow[] rowArray8 = this.GridRows;
                        for (int num12 = num11; num12 >= this.currentRow; num12--)
                        {
                            if (!rowArray8[num12].Selected)
                            {
                                rowArray8[num12].Selected = true;
                                this.numSelectedRows++;
                            }
                        }
                        this.EndEdit();
                        goto Label_0D3A;
                    }
                case Keys.Next:
                    {
                        this.gridState[0x80000] = false;
                        if (!keyArgs.Shift)
                        {
                            if (keyArgs.Control && !keyArgs.Alt)
                            {
                                //this.ParentRowsVisible = true;
                                goto Label_0D3A;
                            }
                            this.ResetSelection();
                            this.CurrentRow = Math.Min((int)(this.GridRowsLength - (this.policy.AllowAdd ? 2 : 1)), (int)(this.CurrentRow + this.numTotallyVisibleRows));
                            goto Label_0D3A;
                        }
                        int num9 = this.currentRow;
                        this.CurrentRow = Math.Min((int)(this.GridRowsLength - (this.policy.AllowAdd ? 2 : 1)), (int)(this.currentRow + this.numTotallyVisibleRows));
                        GridRow[] rowArray7 = this.GridRows;
                        for (int num10 = num9; num10 <= this.currentRow; num10++)
                        {
                            if (!rowArray7[num10].Selected)
                            {
                                rowArray7[num10].Selected = true;
                                this.numSelectedRows++;
                            }
                        }
                        this.EndEdit();
                        goto Label_0D3A;
                    }
                case Keys.End:
                    this.gridState[0x80000] = false;
                    this.ResetSelection();
                    this.CurrentColumn = colCnt;
                    if (keyArgs.Control && !keyArgs.Alt)
                    {
                        int num18 = this.currentRow;
                        this.CurrentRow = Math.Max(0, this.GridRowsLength - (this.policy.AllowAdd ? 2 : 1));
                        if (keyArgs.Shift)
                        {
                            GridRow[] rowArray10 = this.GridRows;
                            for (int num19 = num18; num19 <= this.currentRow; num19++)
                            {
                                rowArray10[num19].Selected = true;
                            }
                            this.numSelectedRows = (this.currentRow - num18) + 1;
                            this.EndEdit();
                        }
                        return true;
                    }
                    goto Label_0D3A;

                case Keys.Home:
                    {
                        this.gridState[0x80000] = false;
                        this.ResetSelection();
                        this.CurrentColumn = 0;
                        if (!keyArgs.Control || keyArgs.Alt)
                        {
                            goto Label_0D3A;
                        }
                        int num16 = this.currentRow;
                        this.CurrentRow = 0;
                        if (keyArgs.Shift)
                        {
                            GridRow[] rowArray9 = this.GridRows;
                            for (int num17 = 0; num17 <= num16; num17++)
                            {
                                rowArray9[num17].Selected = true;
                                this.numSelectedRows++;
                            }
                            this.EndEdit();
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        this.gridState[0x80000] = false;
                        this.ResetSelection();
                        if ((keyArgs.Modifiers & ~Keys.KeyCode) == Keys.Alt)
                        {
                            return true;
                        }
                        if ((keyArgs.Modifiers & Keys.Control) == Keys.Control)
                        {
                            this.CurrentColumn = num1;
                            goto Label_0D3A;
                        }
                        if ((this.currentCol == num1) && (this.currentRow != 0))
                        {
                            this.CurrentRow--;
                            int num13 = this.MoveLeftRight(this.myGridTable.GridColumnStyles, this.myGridTable.GridColumnStyles.Count, false);
                            this.CurrentColumn = num13;
                            goto Label_0D3A;
                        }
                        int num14 = this.MoveLeftRight(this.myGridTable.GridColumnStyles, this.currentCol, false);
                        if (num14 == -1)
                        {
                            if (this.currentRow == 0)
                            {
                                return true;
                            }
                            this.CurrentRow--;
                            this.CurrentColumn = colCnt;
                            goto Label_0D3A;
                        }
                        if (this.SelectionType == SelectionType.Tab && !this.gridState[0x10000])
                        {
                            return false;
                        }
                        this.CurrentColumn = num14;
                        goto Label_0D3A;
                    }
                case Keys.Up:
                    {
                        this.gridState[0x80000] = false;
                        if (!keyArgs.Control || keyArgs.Alt)
                        {
                            if (keyArgs.Shift)
                            {
                                GridRow[] rowArray3 = this.GridRows;
                                if (rowArray3[this.currentRow].Selected)
                                {
                                    if (this.currentRow >= 1)
                                    {
                                        if (rowArray3[this.currentRow - 1].Selected)
                                        {
                                            if ((this.currentRow >= (this.GridRowsLength - 1)) || !rowArray3[this.currentRow + 1].Selected)
                                            {
                                                this.numSelectedRows--;
                                                rowArray3[this.currentRow].Selected = false;
                                            }
                                        }
                                        else
                                        {
                                            this.numSelectedRows += rowArray3[this.currentRow - 1].Selected ? 0 : 1;
                                            rowArray3[this.currentRow - 1].Selected = true;
                                        }
                                        this.CurrentRow--;
                                    }
                                }
                                else
                                {
                                    this.numSelectedRows++;
                                    rowArray3[this.currentRow].Selected = true;
                                    if (this.currentRow >= 1)
                                    {
                                        this.numSelectedRows += rowArray3[this.currentRow - 1].Selected ? 0 : 1;
                                        rowArray3[this.currentRow - 1].Selected = true;
                                        this.CurrentRow--;
                                    }
                                }
                                this.EndEdit();
                                return true;
                            }
                            if (keyArgs.Alt)
                            {
                                //multi
                                this.SetRowExpansionState(-1, false);
                                return true;
                            }
                            this.ResetSelection();
                            this.CurrentRow--;
                            goto Label_0D3A;
                        }
                        if (!keyArgs.Shift)
                        {
                            this.ResetSelection();
                            this.CurrentRow = 0;
                            return true;
                        }
                        GridRow[] rowArray2 = this.GridRows;
                        int num5 = this.currentRow;
                        this.CurrentRow = 0;
                        this.ResetSelection();
                        for (int num6 = 0; num6 <= num5; num6++)
                        {
                            rowArray2[num6].Selected = true;
                        }
                        this.numSelectedRows = num5 + 1;
                        this.EndEdit();
                        return true;
                    }
                case Keys.Right:

                    this.gridState[0x80000] = false;
                    this.ResetSelection();
                    if (((keyArgs.Modifiers & Keys.Control) != Keys.Control) || keyArgs.Alt)
                    {
                        if ((this.currentCol == colCnt) && (this.currentRow != (this.GridRowsLength - 1)))
                        {
                            this.CurrentRow++;
                            this.CurrentColumn = num1;
                            goto Label_0D3A;
                        }
                        int num15 = this.MoveLeftRight(this.myGridTable.GridColumnStyles, this.currentCol, true);
                        if (num15 == (collection1.Count + 1))
                        {
                            this.CurrentColumn = num1;
                            this.CurrentRow++;
                            goto Label_0D3A;
                        }
                        if (this.SelectionType == SelectionType.Tab && !this.gridState[0x10000])
                        {
                            return false;
                        }
                        this.CurrentColumn = num15;
                        goto Label_0D3A;
                    }
                    if (this.SelectionType == SelectionType.Tab && !this.gridState[0x10000])
                    {
                        return false;
                    }
                    this.CurrentColumn = colCnt;
                    goto Label_0D3A;

                case Keys.Down:
                    {
                        this.gridState[0x80000] = false;
                        if (!keyArgs.Control || keyArgs.Alt)
                        {
                            if (keyArgs.Shift)
                            {
                                GridRow[] rowArray5 = this.GridRows;
                                if (rowArray5[this.currentRow].Selected)
                                {
                                    if (this.currentRow < ((this.GridRowsLength - (this.policy.AllowAdd ? 1 : 0)) - 1))
                                    {
                                        if (rowArray5[this.currentRow + 1].Selected)
                                        {
                                            if ((this.currentRow == 0) || !rowArray5[this.currentRow - 1].Selected)
                                            {
                                                this.numSelectedRows--;
                                                rowArray5[this.currentRow].Selected = false;
                                            }
                                        }
                                        else
                                        {
                                            this.numSelectedRows += rowArray5[this.currentRow + 1].Selected ? 0 : 1;
                                            rowArray5[this.currentRow + 1].Selected = true;
                                        }
                                        this.CurrentRow++;
                                    }
                                }
                                else
                                {
                                    this.numSelectedRows++;
                                    rowArray5[this.currentRow].Selected = true;
                                    if (this.currentRow < ((this.GridRowsLength - (this.policy.AllowAdd ? 1 : 0)) - 1))
                                    {
                                        this.CurrentRow++;
                                        this.numSelectedRows += rowArray5[this.currentRow].Selected ? 0 : 1;
                                        rowArray5[this.currentRow].Selected = true;
                                    }
                                }
                                this.EndEdit();
                                return true;
                            }
                            if (keyArgs.Alt)
                            {
                                //multi
                                this.SetRowExpansionState(-1, true);
                                return true;
                            }
                            this.ResetSelection();
                            this.CurrentRow++;
                            goto Label_0D3A;
                        }
                        if (!keyArgs.Shift)
                        {
                            this.ResetSelection();
                            this.CurrentRow = Math.Max(0, this.GridRowsLength - (this.policy.AllowAdd ? 2 : 1));
                            return true;
                        }
                        int num7 = this.currentRow;
                        this.CurrentRow = Math.Max(0, this.GridRowsLength - (this.policy.AllowAdd ? 2 : 1));
                        GridRow[] rowArray4 = this.GridRows;
                        this.ResetSelection();
                        for (int num8 = num7; num8 <= this.currentRow; num8++)
                        {
                            rowArray4[num8].Selected = true;
                        }
                        this.numSelectedRows = (this.currentRow - num7) + 1;
                        this.EndEdit();
                        return true;
                    }
                case Keys.Delete:
                    this.gridState[0x80000] = false;
                    if (!this.policy.AllowRemove || (this.numSelectedRows <= 0))
                    {
                        return false;
                    }
                    this.gridState[0x400] = true;
                    this.DeleteRows(rowArray1);
                    this.currentRow = (this.listManager.Count == 0) ? 0 : this.listManager.Position;
                    this.numSelectedRows = 0;
                    goto Label_0D3A;

                case Keys.Return:
                    this.gridState[0x80000] = false;
                    this.ResetSelection();
                    if (!this.gridState[0x8000])
                    {
                        return false;
                    }
                    if (((keyArgs.Modifiers & Keys.Control) != Keys.None) && !keyArgs.Alt)
                    {
                        this.EndEdit();
                        this.HandleEndCurrentEdit();
                        this.Edit();
                        goto Label_0D3A;
                    }
                    this.CurrentRow = this.currentRow + 1;
                    goto Label_0D3A;

                case Keys.Tab:
                    return this.ProcessTabKey(keyArgs.KeyData);

                default:
                    goto Label_0D3A;
            }
            return true;
        Label_0D3A:
            return true;
        }
        
        /// <summary>
        /// Processes a Key Preview
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand)]
        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x100)
            {
                KeyEventArgs args1 = new KeyEventArgs(((Keys)((int)m.WParam)) | Control.ModifierKeys);
                switch (args1.KeyCode)
                {
                    case Keys.Oemplus:
                    case Keys.OemMinus:
                    case Keys.F2:
                    case Keys.Add:
                    case Keys.Subtract:
                    case Keys.A:
                    case Keys.Escape:
                    case Keys.Space:
                    case Keys.Prior:
                    case Keys.Next:
                    case Keys.End:
                    case Keys.Home:
                    case Keys.Left:
                    case Keys.Up:
                    case Keys.Right:
                    case Keys.Down:
                    case Keys.Delete:
                    case Keys.Return:
                    case Keys.Tab:
                        return this.ProcessGridKey(args1);

                    case Keys.Oemcomma:
                    case Keys.Separator:
                    case Keys.IMEConvert:
                    case Keys.IMENonconvert:
                    case Keys.IMEAceept:
                    case Keys.IMEModeChange:
                    case Keys.Select:
                    case Keys.Print:
                    case Keys.Execute:
                    case Keys.Snapshot:
                    case Keys.Insert:
                        goto Label_011C;
                }
            }
        //else if (m.Msg == 0x101)
        //{
        //    KeyEventArgs args2 = new KeyEventArgs(((Keys) ((int) m.WParam)) | Control.ModifierKeys);
        //    if (args2.KeyCode == Keys.Tab)
        //    {
        //        return this.ProcessGridKey(args2);
        //    }
        //}
        Label_011C:
            return base.ProcessKeyPreview(ref m);
        }
        /// <summary>
        /// Process Tab Key
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        [PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Window=\"AllWindows\"/>\r\n</PermissionSet>\r\n")]
        protected bool ProcessTabKey(Keys keyData)
        {
            if ((this.ListManager == null) || (this.myGridTable == null))
            {
                return false;
            }
            bool flag1 = false;
            int num7 = this.myGridTable.GridColumnStyles.Count;
            this.isRightToLeft();
            this.ResetSelection();
            if (this.gridState[0x8000])
            {
                flag1 = true;
                if (!this.CommitEdit())
                {
                    this.Edit();
                    return true;
                }
            }
            if ((keyData & Keys.Control) == Keys.Control)
            {
                if ((keyData & Keys.Alt) == Keys.Alt)
                {
                    return true;
                }
                Keys keys1 = keyData & ~Keys.Control;
                this.EndEdit();
                bool flag2 = false;
                IntSecurity.ModifyFocus.Assert();
                try
                {
                    flag2 = base.ProcessDialogKey(keys1);
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                return flag2;
            }
            GridRow[] rowArray1 = this.GridRows;
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            int num1 = 0;
            int num2 = collection1.Count - 1;
            if (rowArray1.Length == 0)
            {
                this.EndEdit();
                bool flag3 = false;
                IntSecurity.ModifyFocus.Assert();
                try
                {
                    flag3 = base.ProcessDialogKey(keyData);
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
                return flag3;
            }
            for (int num3 = 0; num3 < collection1.Count; num3++)
            {
                if (collection1[num3].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                {
                    num2 = num3;
                    break;
                }
            }
            for (int num4 = collection1.Count - 1; num4 >= 0; num4--)
            {
                if (collection1[num4].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                {
                    num1 = num4;
                    break;
                }
            }
            if (this.CurrentColumn == num1)
            {
                if ((this.gridState[0x80000] || (!this.gridState[0x80000] && ((keyData & Keys.Shift) != Keys.Shift))) && rowArray1[this.CurrentRow].ProcessTabKey(keyData, this.layout.RowHeaders, this.isRightToLeft()))
                {
                    if (collection1.Count > 0)
                    {
                        collection1[this.CurrentColumn].ConcedeFocus();
                    }
                    this.gridState[0x80000] = true;
                    if ((this.gridState[0x800] && base.CanFocus) && !this.Focused)
                    {
                        this.FocusInternal();
                    }
                    return true;
                }
                if ((this.currentRow == (this.GridRowsLength - 1)) && ((keyData & Keys.Shift) == Keys.None))
                {
                    this.EndEdit();
                    bool flag4 = false;
                    IntSecurity.ModifyFocus.Assert();
                    try
                    {
                        flag4 = base.ProcessDialogKey(keyData);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                    return flag4;
                }
            }
            if (this.CurrentColumn == num2)
            {
                if (!this.gridState[0x80000])
                {
                    if (((this.CurrentRow != 0) && ((keyData & Keys.Shift) == Keys.Shift)) && rowArray1[this.CurrentRow - 1].ProcessTabKey(keyData, this.layout.RowHeaders, this.isRightToLeft()))
                    {
                        this.CurrentRow--;
                        if (collection1.Count > 0)
                        {
                            collection1[this.CurrentColumn].ConcedeFocus();
                        }
                        this.gridState[0x80000] = true;
                        if ((this.gridState[0x800] && base.CanFocus) && !this.Focused)
                        {
                            this.FocusInternal();
                        }
                        return true;
                    }
                }
                else
                {
                    if (!rowArray1[this.CurrentRow].ProcessTabKey(keyData, this.layout.RowHeaders, this.isRightToLeft()))
                    {
                        this.gridState[0x80000] = false;
                        this.CurrentColumn = num1;
                    }
                    return true;
                }
                if ((this.currentRow == 0) && ((keyData & Keys.Shift) == Keys.Shift))
                {
                    this.EndEdit();
                    bool flag5 = false;
                    IntSecurity.ModifyFocus.Assert();
                    try
                    {
                        flag5 = base.ProcessDialogKey(keyData);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                    return flag5;
                }
            }
            if ((keyData & Keys.Shift) != Keys.Shift)
            {
                if (this.CurrentColumn == num1)
                {
                    if (this.CurrentRow != (this.GridRowsLength - 1))
                    {
                        this.CurrentColumn = num2;
                    }
                    this.CurrentRow++;
                }
                else
                {
                    int num5 = this.MoveLeftRight(collection1, this.currentCol, true);
                    this.CurrentColumn = num5;
                }
            }
            else if (this.CurrentColumn == num2)
            {
                if (this.CurrentRow != 0)
                {
                    this.CurrentColumn = num1;
                }
                if (!this.gridState[0x80000])
                {
                    this.CurrentRow--;
                }
            }
            else if (this.gridState[0x80000] && (this.CurrentColumn == num1))
            {
                this.InvalidateRow(this.currentRow);
                this.Edit();
            }
            else
            {
                int num6 = this.MoveLeftRight(collection1, this.currentCol, false);
                this.CurrentColumn = num6;
            }
            this.gridState[0x80000] = false;
            if (flag1)
            {
                this.ResetSelection();
                this.Edit();
            }
            return true;
        }

        internal void RecalculateFonts()
        {
            try
            {
                this.linkFont = new Font(this.Font, FontStyle.Underline);
            }
            catch (Exception)
            {
            }
            this.fontHeight = this.Font.Height;
            this.linkFontHeight = this.LinkFont.Height;
            this.captionFontHeight = this.CaptionFont.Height;
            if ((this.myGridTable == null) || this.myGridTable.IsDefault)
            {
                this.headerFontHeight = this.HeaderFont.Height;
            }
            else
            {
                this.headerFontHeight = this.myGridTable.dataGrid.HeaderFont.Height;
            }
        }

        private void RecreateGridRows(bool resetRowHeight)
        {
            if (resetRowHeight)
            {
                ComuteMinimumRowHeight();
            }
            int num1 = 0;
            BindManager manager1 = this.ListManager;
            if (manager1 != null)
            {
                num1 = manager1.Count;
                if (this.policy.AllowAdd)
                {
                    num1++;
                }
            }
            this.SetGridRows(null, num1);
        }

        /// <summary>
        /// Reset Alternating BackColor
        /// </summary>
        public void ResetAlternatingBackColor()
        {
            if (this.ShouldSerializeAlternatingBackColor())
            {
                this.AlternatingBackColor = Grid.DefaultAlternatingBackBrush.Color;
                this.InvalidateInside();
            }
        }
        /// <summary>
        /// Reset BackColor
        /// </summary>
        public override void ResetBackColor()
        {
            if (!this.BackColor.Equals(Grid.DefaultBackBrush.Color))
            {
                this.BackColor = Grid.DefaultBackBrush.Color;
            }
        }

        private void ResetCaptionBackColor()
        {
            this.Caption.ResetBackColor();
        }

        private void ResetCaptionFont()
        {
            this.Caption.ResetFont();
        }
        /// <summary>
        /// Reset ForeColor
        /// </summary>
        public override void ResetForeColor()
        {
            if (!this.ForeColor.Equals(Grid.DefaultForeBrush.Color))
            {
                this.ForeColor = Grid.DefaultForeBrush.Color;
            }
        }
        /// <summary>
        /// Reset GridLineColor
        /// </summary>
        public void ResetGridLineColor()
        {
            if (this.ShouldSerializeGridLineColor())
            {
                this.GridLineColor = Grid.DefaultGridLineBrush.Color;
            }
        }
        /// <summary>
        /// Reset HeaderBackColor
        /// </summary>
        public void ResetHeaderBackColor()
        {
            if (this.ShouldSerializeHeaderBackColor())
            {
                this.HeaderBackColor = Grid.DefaultHeaderBackBrush.Color;
            }
        }
        /// <summary>
        /// Reset HeaderFont
        /// </summary>
        public void ResetHeaderFont()
        {
            if (this.headerFont != null)
            {
                this.headerFont = null;
                this.RecalculateFonts();
                base.PerformLayout();
                base.Invalidate(this.layout.Inside);
            }
        }
        /// <summary>
        /// Reset HeaderForeColor
        /// </summary>
        public void ResetHeaderForeColor()
        {
            if (this.ShouldSerializeHeaderForeColor())
            {
                this.HeaderForeColor = Grid.DefaultHeaderForeBrush.Color;
            }
        }
        private void ResetHorizontalOffset()
        {
            this.horizontalOffset = 0;
            this.negOffset = 0;
            this.firstVisibleCol = 0;
            this.numVisibleCols = 0;
            this.lastTotallyVisibleCol = 0;
        }
        /// <summary>
        /// Reset LinkColor
        /// </summary>
        public void ResetLinkColor()
        {
            if (this.ShouldSerializeLinkColor())
            {
                this.LinkColor = Grid.DefaultLinkBrush.Color;
            }
        }
        /// <summary>
        /// Reset LinkHoverColor
        /// </summary>
        public void ResetLinkHoverColor()
        {
        }

        private void ResetMouseState()
        {
            this.oldRow = -1;
            this.gridState[0x40000] = true;
        }
        /// <summary>
        /// Reset Selection
        /// </summary>
        protected void ResetSelection()
        {
            if (this.numSelectedRows > 0)
            {
                GridRow[] rowArray1 = this.GridRows;
                for (int num1 = 0; num1 < this.GridRowsLength; num1++)
                {
                    if (rowArray1[num1].Selected)
                    {
                        rowArray1[num1].Selected = false;
                    }
                }
            }
            this.numSelectedRows = 0;
            this.lastRowSelected = -1;
        }
        /// <summary>
        /// Reset SelectionBackColor
        /// </summary>
        public void ResetSelectionBackColor()
        {
            if (this.ShouldSerializeSelectionBackColor())
            {
                this.SelectionBackColor = Grid.DefaultSelectionBackBrush.Color;
            }
        }
        /// <summary>
        /// Reset SelectionForeColor
        /// </summary>
        public void ResetSelectionForeColor()
        {
            if (this.ShouldSerializeSelectionForeColor())
            {
                this.SelectionForeColor = Grid.DefaultSelectionForeBrush.Color;
            }
        }

        private void ResetToolTip()
        {
            this.ToolTipProvider.Destroy();
            this.ToolTipProvider.CreateToolTipHandle();

            this.ToolTipId = 0;
        }

        private void ResetUIState()
        {
            this.gridState[0x80000] = false;
            this.ResetSelection();
            this.ResetMouseState();
            base.PerformLayout();
            base.Invalidate();
            if (this.horizScrollBar.Visible)
            {
                this.horizScrollBar.Invalidate();
            }
            if (this.vertScrollBar.Visible)
            {
                this.vertScrollBar.Invalidate();
            }
        }

        private void RowAutoResize(int row)
        {
            this.EndEdit();
            BindManager manager1 = this.ListManager;
            if (manager1 != null)
            {
                Graphics graphics1 = this.CreateGraphicsInternal();
                try
                {
                    GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
                    GridRow row1 = this.GridRows[row];
                    int num5 = manager1.Count;
                    int num1 = 0;
                    int num2 = collection1.Count;
                    for (int num3 = 0; num3 < num2; num3++)
                    {
                        object obj1 = collection1[num3].GetColumnValueAtRow(manager1, row);
                        num1 = Math.Max(num1, collection1[num3].GetPreferredHeight(graphics1, obj1));
                    }
                    if (row1.Height != num1)
                    {
                        row1.Height = num1;
                        base.PerformLayout();
                        Rectangle rectangle1 = this.layout.Data;
                        if (this.layout.RowHeadersVisible)
                        {
                            rectangle1 = Rectangle.Union(rectangle1, this.layout.RowHeaders);
                        }
                        int num4 = this.GetRowTop(row);
                        rectangle1.Height -= rectangle1.Y - num4;
                        rectangle1.Y = num4;
                        base.Invalidate(rectangle1);
                    }
                }
                finally
                {
                    graphics1.Dispose();
                }
            }
        }


        private void RowHeadersVisibleChanged(object sender, EventArgs e)
        {
            this.layout.RowHeadersVisible = (this.myGridTable != null) && this.myGridTable.dataGrid.RowHeadersVisible;
            base.PerformLayout();
            this.InvalidateInside();
        }


        private void RowHeaderWidthChanged(object sender, EventArgs e)
        {
            if (this.layout.RowHeadersVisible)
            {
                base.PerformLayout();
                this.InvalidateInside();
            }
        }

        private void RowResizeBegin(MouseEventArgs e, int row)
        {
            int num1 = e.Y;
            this.EndEdit();
            Rectangle rectangle1 = Rectangle.Union(this.layout.RowHeaders, this.layout.Data);
            int num2 = this.GetRowTop(row);
            rectangle1.Y = num2 + 3;
            rectangle1.Height = ((this.layout.Data.Y + this.layout.Data.Height) - num2) - 2;
            IntSecurity.AdjustCursorClip.Assert();
            try
            {
                this.CaptureInternal = true;
                Cursor.Clip = base.RectangleToScreen(rectangle1);
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
            this.gridState[0x10] = true;
            this.trackRowAnchor = num1;
            this.trackRow = row;
            this.DrawRowSplitBar(e);
            this.lastSplitBar = e;
        }


        private void RowResizeEnd(MouseEventArgs e)
        {
            try
            {
                if (this.lastSplitBar != null)
                {
                    this.DrawRowSplitBar(this.lastSplitBar);
                    this.lastSplitBar = null;
                }
                int num1 = Math.Min(e.Y, (this.layout.Data.Y + this.layout.Data.Height) + 1);
                int num2 = num1 - this.GetRowBottom(this.trackRow);
                if ((this.trackRowAnchor != num1) && (num2 != 0))
                {
                    GridRow row1 = this.GridRows[this.trackRow];
                    int num3 = row1.Height + num2;
                    num3 = Math.Max(num3, 3);
                    row1.Height = num3;
                    base.PerformLayout();
                    Rectangle rectangle1 = Rectangle.Union(this.layout.RowHeaders, this.layout.Data);
                    int num4 = this.GetRowTop(this.trackRow);
                    rectangle1.Height -= rectangle1.Y - num4;
                    rectangle1.Y = num4;
                    base.Invalidate(rectangle1);
                }
            }
            finally
            {
                Cursor.Clip = Rectangle.Empty;
                this.CaptureInternal = false;
            }
        }

        private void RowResizeMove(MouseEventArgs e)
        {
            if (this.lastSplitBar != null)
            {
                this.DrawRowSplitBar(this.lastSplitBar);
                this.lastSplitBar = e;
            }
            this.DrawRowSplitBar(e);
        }
        /// <summary>
        /// Selects all the cells in the grid
        /// </summary>
        public void SelectAll()
        {
            int numOfRows = this.GridRowsLength - (this.policy.AllowAdd ? 1 : 0);
            //bool selected = numSelectedRows != numOfRows;
            if (numOfRows <= 0)
                return;
            //if (!selected)
            //{
            //    ResetSelection();
            //    return;
            //}
            this.gridState[0x80000] = false;
            GridRow[] rowArray11 = this.GridRows;
            for (int i = 0; i < this.GridRowsLength; i++)
            {
                if (rowArray11[i] is GridRelationshipRow)
                {
                    rowArray11[i].Selected = true;
                }
            }
            this.numSelectedRows = this.GridRowsLength - (this.policy.AllowAdd ? 1 : 0);
            this.EndEdit();
        }

        private void ScrollDown(int rows)
        {
            if (rows != 0)
            {
                this.ClearRegionCache();
                int num1 = Math.Max(0, Math.Min((int)(this.firstVisibleRow + rows), (int)(this.GridRowsLength - 1)));
                int num2 = this.firstVisibleRow;
                this.firstVisibleRow = num1;
                this.vertScrollBar.Value = num1;
                bool flag1 = this.gridState[0x8000];
                this.ComputeVisibleRows();
                if (this.gridState[0x20000])
                {
                    this.EndEdit();//--this.Edit();
                    this.gridState[0x20000] = false;
                }
                else
                {
                    this.EndEdit();
                }
                int num3 = this.ComputeRowDelta(num2, num1);
                Rectangle rectangle1 = this.layout.Data;
                if (this.layout.RowHeadersVisible)
                {
                    rectangle1 = Rectangle.Union(rectangle1, this.layout.RowHeaders);
                }
                WinMethods.RECT rect1 = WinMethods.RECT.FromXYWH(rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height);
                WinMethods.ScrollWindow(new HandleRef(this, base.Handle), 0, num3, ref rect1, ref rect1);
                this.OnScroll(EventArgs.Empty);
                if (flag1)
                {
                    this.InvalidateRowHeader(this.currentRow);
                }
            }
        }


        private void ScrollRectangles(WinMethods.RECT[] rects, int change)
        {
            if (this.isRightToLeft())
            {
                change = -change;
            }
            for (int num1 = 0; num1 < rects.Length; num1++)
            {
                WinMethods.RECT rect1 = rects[num1];
                WinMethods.ScrollWindow(new HandleRef(this, base.Handle), change, 0, ref rect1, ref rect1);
            }
        }

        private void ScrollRight(int columns)
        {
            int num1 = this.firstVisibleCol + columns;
            GridColumnCollection collection1 = this.myGridTable.GridColumnStyles;
            int num2 = 0;
            int num3 = collection1.Count;
            int num4 = 0;
            if (this.myGridTable.IsDefault)
            {
                num4 = num3;
            }
            else
            {
                for (int num5 = 0; num5 < num3; num5++)
                {
                    if (collection1[num5].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                    {
                        num4++;
                    }
                }
            }
            if (((this.lastTotallyVisibleCol != (num4 - 1)) || (columns <= 0)) && (((this.firstVisibleCol != 0) || (columns >= 0)) || (this.negOffset != 0)))
            {
                num1 = Math.Min(num1, num3 - 1);
                for (int num6 = 0; num6 < num1; num6++)
                {
                    if (collection1[num6].IsVisibleInternal)//*bound*/.PropertyDescriptor != null)
                    {
                        num2 += collection1[num6].Width;
                    }
                }
                this.HorizontalOffset = num2;
            }
        }

        private void ScrollTo(int targetRow, int targetCol)
        {
            int num1 = this.ComputeDeltaRows(targetRow);
            this.ScrollDown(num1);
            int num2 = targetCol - this.firstVisibleCol;
            if (targetCol > this.lastTotallyVisibleCol)
            {
                num2 = targetCol - this.lastTotallyVisibleCol;
            }
            if ((num2 != 0) || (this.negOffset != 0))
            {
                this.ScrollRight(num2);
            }
        }

        /// <summary>
        /// Select a specific row
        /// </summary>
        /// <param name="row"></param>
        public void Select(int row)
        {
            GridRow[] rowArray1 = this.GridRows;
            if (!rowArray1[row].Selected)
            {
                rowArray1[row].Selected = true;
                this.numSelectedRows++;
            }
            this.EndEdit();
        }


        internal void Set_ListManager(object newDataSource, string newDataMember, bool force)
        {
            //bool forceColumnCreation = Columns.Count == 0;
            this.Set_ListManager(newDataSource, newDataMember, force, true);
        }


        internal void Set_ListManager(object newDataSource, string newDataMember, bool force, bool forceColumnCreation)
        {
            bool flag1 = this.DataSource != newDataSource;
            bool flag2 = this.DataMember != newDataMember;
            if ((force || flag1) || (flag2 || !this.gridState[0x200000]))
            {
                this.gridState[0x200000] = true;
                bool flag3 = true;
                try
                {
                    this.UpdateListManager();
                    if (this.listManager != null)
                    {
                        this.UnWireDataSource();
                    }
                    BindManager manager1 = this.listManager;
                    bool flag4 = false;
                    if (((newDataSource != null) && (this.BindContext != null)) && (newDataSource != Convert.DBNull))
                    {
                        this.listManager = (BindManager)this.BindContext[newDataSource, newDataMember];
                    }
                    else
                    {
                        this.listManager = null;
                    }
                    if (_DataView != null)
                    {
                        _DataView.ListChanged -= new ListChangedEventHandler(_DataView_ListChanged);
                        _DataView = null;
                    }

                    this.dataSource = newDataSource;
                    this.dataMember = (newDataMember == null) ? "" : newDataMember;
                    flag4 = this.listManager != manager1;
                    if (this.listManager != null)
                    {
                        this.WireDataSource();
                        this.policy.UpdatePolicy(this.listManager, this.ReadOnly);
                    }
                    if (this.dataSource == null)
                    {
                        if (flag4 || this.gridState[0x400000])
                        {
                            this.RecreateGridRows(true);
                            this.EndUpdateInternal();
                            flag3 = false;

                            this.OnDataSourceChanged(EventArgs.Empty);
                        }
                        return;
                    }
                    if (!this.Initializing && (this.listManager == null))
                    {
                        this.SetGridRows(null, 0);
                        this.SetGridTable(this.defaultTableStyle, forceColumnCreation,true);
                    }
                    if (force || flag4 || this.gridState[0x400000])
                    {
                        this.BeginUpdateInternal();
                        if (this.listManager != null)
                        {
                            if (forceColumnCreation)
                            {
                                this.defaultTableStyle.GridColumnStyles.ResetDefaultColumnCollection();
                                //GridTableStyle style1 = this.dataGridTables[this.GetListName()];
                                //GridTableStyle style1 = this.myGridTable;
                            }
                            _DataView = (DataView)this.listManager.DataList(this.DataMember);
                            if (_DataView != null)
                               _DataView.ListChanged += new ListChangedEventHandler(_DataView_ListChanged);

                           CreateRelations();

                            if (!forceDefaultTableStyle && Columns.Count > 0)
                            {
                                this.SetMappingName();
                                if (m_TableStyle.GridColumnStyles.Count == 0)
                                    forceColumnCreation = true;
                                this.SetGridTable(m_TableStyle, forceColumnCreation,false);
                            }
                            else
                            {
                                this.SetGridTable(this.defaultTableStyle, forceColumnCreation,false);
                            }

                            if (string.IsNullOrEmpty(this.MappingName))
                            {
                                this.SetMappingName();
                            }
                            this.currentRow = (this.listManager.Position == -1) ? 0 : this.listManager.Position;
                        }
                        this.RecreateGridRows(true);
                        this.EndUpdateInternal();
                        flag3 = false;
                        this.ComputeMinimumRowHeaderWidth();
                        this.RowHeaderWidth = Math.Max(this.minRowHeaderWidth, this.RowHeaderWidth);
           
                        this.ListHasErrors = this.GridSourceHasErrors();
                        this.ResetUIState();

                        if (dataSource is DataView)
                        {
                            rowFilter = ((DataView)dataSource).RowFilter;
                        }
                        else
                        {
                            rowFilter = "";
                        }
                         this.OnDataSourceChanged(EventArgs.Empty);
                         
                    }
                }
                finally
                {
                    this.gridState[0x200000] = false;
                    if (flag3)
                    {
                        this.EndUpdateInternal();
                    }
                }
            }
        }

          /// <summary>
        /// ReBinding a new data source in the same schema as the current data source at run time
        /// </summary>
        /// <param name="newDataSource"></param>
        public void ReBinding(object newDataSource)
        {

            if (newDataSource == null || this.dataSource == null || this.listManager == null || this.gridState[0x200000])
            {
                return;
            }

            this.gridState[0x200000] = true;
            bool flag3 = true;
            try
            {
                BindManager manager1 = this.listManager;
                if (((newDataSource != null) && (this.BindContext != null)) && (newDataSource != Convert.DBNull))
                {
                    this.listManager = (BindManager)this.BindContext[newDataSource, DataMember];
                }
                else
                {
                    this.listManager = null;
                }
                this.dataSource = newDataSource;

                if (this.dataSource == null || this.listManager == null)
                {
                    this.SetGridRows(null, 0);
                    //this.SetGridTable(this.defaultTableStyle, forceColumnCreation, true);
                    myGridTable.PairTableStylesReset(this.listManager);

                    //this.RecreateGridRows(true);
                    this.EndUpdateInternal();
                    flag3 = false;
                    return;
                }

                this.BeginUpdateInternal();
                if (this.listManager != null)
                {
                    //if (forceColumnCreation)
                    //this.defaultTableStyle.GridColumnStyles.ResetDefaultColumnCollection();

                    _DataView = (DataView)this.listManager.DataList(this.DataMember);

                    myGridTable.PairTableStylesReset(this.listManager);


                    //if (!forceDefaultTableStyle && Columns.Count > 0)
                    //{
                    //    this.SetGridTable(m_TableStyle, true, false);
                    //}
                    //else
                    //{
                    //    this.SetGridTable(this.defaultTableStyle, true, false);
                    //}

                    if (string.IsNullOrEmpty(this.MappingName))
                    {
                        this.SetMappingName();
                    }
                    //this.currentRow = (this.listManager.Position == -1) ? 0 : this.listManager.Position;
                }
                this.RecreateGridRows(true);
                this.EndUpdateInternal();
                flag3 = false;
                //base.Invalidate();
                if (this.horizScrollBar.Visible)
                {
                    this.horizScrollBar.Invalidate();
                }
                if (this.vertScrollBar.Visible)
                {
                    this.vertScrollBar.Invalidate();
                }

            }
            finally
            {
                this.gridState[0x200000] = false;
                if (flag3)
                {
                    this.EndUpdateInternal();
                }
            }
        }
 
        private void CreateRelations()
        {

            if(relations != null)
            {
                //if (this.relations.Count > 0 && currentRow > -1)
                //{
                //    this.Collapse(this.currentRow);
                //}

                relations.Clear();
                relations = null;
            }
            if (!(dataSource is DataSet))
            {
                this.caption.DownButtonVisible = false;
                return;
            }
            this.caption.DownButtonVisible = true;
            DataSet ds = (DataSet)this.dataSource;
            relations = new List<DataRelation>();
            foreach (DataRelation rel in ds.Relations)
            {
                relations.Add(rel);
            }
        }

        private void CreateDataView()
        {
            if (_DataView != null)
                return;
            if (this.listManager != null)
            {
                _DataView = (DataView)this.listManager.DataList(this.DataMember);
                if (_DataView != null)
                    _DataView.ListChanged += new ListChangedEventHandler(_DataView_ListChanged);
            }
        }
        ///// <summary>
        ///// ReBinding a new data source in the same schema as the current data source at run time
        ///// </summary>
        ///// <param name="newDataSource"></param>
        //public void ReBinding(object newDataSource)
        //{

        //    if (newDataSource == null || newDataSource == Convert.DBNull || this.gridState[0x200000])
        //        return;

        //    this.originalState = null;

        //    this.gridState[0x200000] = true;
        //    bool flag3 = true;
        //    try
        //    {
        //        this.UpdateListManager();

        //        if (this.listManager != null)
        //        {
        //            this.UnWireDataSource();
        //        }
        //        BindManager manager1 = this.listManager;
        //        bool flag4 = false;
        //        this.listManager = (BindManager)this.BindContext[newDataSource, DataMember];

        //        this.dataSource = newDataSource;
        //        flag4 = this.listManager != manager1;
        //        if (this.listManager != null)
        //        {
        //            this.WireDataSource();
        //            this.policy.UpdatePolicy(this.listManager, this.ReadOnly);
        //        }
        //        if (flag4 || this.gridState[0x400000])
        //        {
        //            this.BeginUpdateInternal();
        //            if (this.listManager != null)
        //            {
        //                //this.defaultTableStyle.GridColumnStyles.ResetDefaultColumnCollection();

        //                if (_DataView != null)
        //                {
        //                    _DataView.ListChanged -= new ListChangedEventHandler(_DataView_ListChanged);
        //                    _DataView = null;
        //                }

        //                _DataView = (DataView)this.listManager.DataList(this.DataMember);
        //                if (_DataView != null)
        //                    _DataView.ListChanged += new ListChangedEventHandler(_DataView_ListChanged);

                       
        //                myGridTable.PairTableStylesReset(this.listManager);
                        
        //                this.gridState[0x4000] = false;
        //                this.horizScrollBar.Value = horizScrollBar.Minimum;// 0; //fix_scroll
        //                this.firstVisibleRow = 0;
        //                this.currentCol = 0;
        //                this.currentRow = (this.listManager.Position == -1) ? 0 : this.listManager.Position;
        //                this.ResetHorizontalOffset();
        //                this.negOffset = 0;
        //                //this.ResetUIState();
        //                this.checkHierarchy = true;
     
        //            }
        //             this.RecreateGridRows(false);
        //            this.EndUpdateInternal();
        //            flag3 = false;
        //            //this.ComputeMinimumRowHeaderWidth();
        //            //this.RowHeaderWidth = Math.Max(this.minRowHeaderWidth, this.RowHeaderWidth);

        //            this.ListHasErrors = this.GridSourceHasErrors();
        //            this.ResetUIState();
        //            this.OnDataSourceChanged(EventArgs.Empty);

        //        }
        //    }
        //    finally
        //    {
        //        this.gridState[0x200000] = false;
        //        if (flag3)
        //        {
        //            this.EndUpdateInternal();
        //        }
        //    }

        //}

        //public void SetDataBinding(object dataSource)
        //{
        //    //this.parentRows.Clear();
        //    this.originalState = null;
        //    //this.caption.BackButtonActive = this.caption.DownButtonActive = this.caption.BackButtonVisible = false;
        //    this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
        //    this.Set_ListManager(dataSource, DataMember, false, false);
        //}

        /// <summary>
        /// Sets the DataSource and DataMember properties at run time
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        public void SetDataBinding(object dataSource, string dataMember)
        {
            //this.parentRows.Clear();
            this.originalState = null;
            //this.caption.BackButtonActive = this.caption.DownButtonActive = this.caption.BackButtonVisible = false;
            this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
            this.Set_ListManager(dataSource, dataMember, false);
        }
  
        void _DataView_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnFilterChanged(EventArgs.Empty);
        }

        delegate void SetGridRowsCallback(GridRow[] newRows, int newRowsLength);

        internal void SetGridRows(GridRow[] newRows, int newRowsLength)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetGridRowsCallback(SetGridRows), newRows, newRowsLength);
            }
            else
            {
                this.dataGridRows = newRows;
                this.dataGridRowsLength = newRowsLength;
                this.vertScrollBar.Maximum = Math.Max(0,this.GridRowsLength - 1);
                if (this.firstVisibleRow > newRowsLength)
                {
                    this.vertScrollBar.Value = 0;
                    this.firstVisibleRow = 0;
                }
                this.ResetUIState();
            }
        }

        internal void SetGridTable(GridTableStyle newTable, bool forceColumnCreation,bool resetUI)
        {

            if (this.myGridTable != null)
            {
                this.UnWireTableStylePropChanged(this.myGridTable);
                if (this.myGridTable.IsDefault)
                {
                    this.myGridTable.GridColumnStyles.ResetPropertyDescriptors();
                    this.myGridTable.ResetRelationsList();
                }
            }
            this.myGridTable = newTable;
            this.WireTableStylePropChanged(this.myGridTable);
            this.layout.RowHeadersVisible = newTable.IsDefault ? this.RowHeadersVisible : newTable.dataGrid.RowHeadersVisible;
            if (newTable != null)
            {
                newTable.Grid = this;
            }
            if (this.ListManager != null &&  forceColumnCreation)//this.Columns.Count==0)
            {
                this.PairTableStylesAndGridColumns(this.ListManager, this.myGridTable, forceColumnCreation);
            }
            if (newTable != null)
            {
                //newTable.ResetRelationsUI();
            }
            this.gridState[0x4000] = false;
            this.horizScrollBar.Value = horizScrollBar.Minimum;// 0; //fix_scroll
            this.firstVisibleRow = 0;
            this.currentCol = 0;
            if (this.listManager == null)
            {
                this.currentRow = 0;
            }
            else
            {
                this.currentRow = (this.listManager.Position == -1) ? 0 : this.listManager.Position;
            }
            this.ResetHorizontalOffset();
            this.negOffset = 0;
        if(resetUI)
            this.ResetUIState();
            this.checkHierarchy = true;

            if (this.AllowColumnContextMenu)
                    PerformColumnContextMenu();
        }

        private void SetMappingName()
        {   //Updated
            if (this.DataList != null && (this.myGridTable.MappingName == null || this.myGridTable.MappingName == ""))
            {
                this.MappingName = this.DataList.Table.TableName;
            }
            //added
            if (this.DataMember == "" && (this.MappingName == null || this.MappingName == ""))
            {
                if (!(this.DataSource is DataSet && this.DataMember == ""))
                {
                    //throw new InvalidDataException("Invalid mapping name");
                }
            }

        }

        internal void SetParentRowsVisibility(bool visible)
        {
            Rectangle rectangle1 = this.layout.ParentRows;
            Rectangle rectangle2 = this.layout.Data;
            if (this.layout.RowHeadersVisible)
            {
                rectangle2.X -= this.isRightToLeft() ? 0 : this.layout.RowHeaders.Width;
                rectangle2.Width += this.layout.RowHeaders.Width;
            }
            if (this.layout.ColumnHeadersVisible)
            {
                rectangle2.Y -= this.layout.ColumnHeaders.Height;
                rectangle2.Height += this.layout.ColumnHeaders.Height;
            }
            this.EndEdit();
            if (visible)
            {
                this.layout.ParentRowsVisible = true;
                base.PerformLayout();
                base.Invalidate();
            }
            else
            {
                WinMethods.RECT rect1 = WinMethods.RECT.FromXYWH(rectangle2.X, rectangle2.Y - this.layout.ParentRows.Height, rectangle2.Width, rectangle2.Height + this.layout.ParentRows.Height);
                WinMethods.ScrollWindow(new HandleRef(this, base.Handle), 0, -rectangle1.Height, ref rect1, ref rect1);
                if (this.vertScrollBar.Visible)
                {
                    Rectangle rectangle3 = this.vertScrollBar.Bounds;
                    rectangle3.Y -= rectangle1.Height;
                    rectangle3.Height += rectangle1.Height;
                    base.Invalidate(rectangle3);
                }
                this.layout.ParentRowsVisible = false;
                base.PerformLayout();
            }
        }

        private void SetRowExpansionState(int row, bool expanded)
        {
            if ((row < -1) || (row > (this.GridRowsLength - (this.policy.AllowAdd ? 2 : 1))))
            {
                throw new ArgumentOutOfRangeException("row");
            }
            GridRow[] rowArray1 = this.GridRows;
            if (row == -1)
            {
                GridRelationshipRow[] rowArray2 = this.GetExpandableRows();
                bool flag1 = false;
                for (int num1 = 0; num1 < rowArray2.Length; num1++)
                {
                    if (rowArray2[num1].Expanded != expanded)
                    {
                        rowArray2[num1].Expanded = expanded;
                        flag1 = true;
                    }
                }
                if (flag1 && (this.gridState[0x4000] || this.gridState[0x8000]))
                {
                    this.ResetSelection();
                    this.Edit();
                }
            }
            else if (rowArray1[row] is GridRelationshipRow)
            {
                GridRelationshipRow row1 = (GridRelationshipRow)rowArray1[row];
                if (row1.Expanded != expanded)
                {
                    if (this.gridState[0x4000] || this.gridState[0x8000])
                    {
                        this.ResetSelection();
                        this.Edit();
                    }
                    row1.Expanded = expanded;
                }
            }
        }

        /// <summary>
        /// Indicates whether the AlternatingBackColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeAlternatingBackColor()
        {
            return !this.AlternatingBackBrush.Equals(Grid.DefaultAlternatingBackBrush);
        }

        internal bool ShouldSerializeBackColor()
        {
            return !Grid.DefaultBackBrush.Color.Equals(this.BackColor);
        }

        /// <summary>
        /// Indicates whether the BackgroundColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeBackgroundColor()
        {
            return !this.BackgroundBrush.Equals(Grid.DefaultBackgroundBrush);
        }
        /// <summary>
        /// Indicates whether the CaptionBackColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeCaptionBackColor()
        {
            return this.Caption.ShouldSerializeBackColor();
        }


        private bool ShouldSerializeCaptionFont()
        {
            return this.Caption.ShouldSerializeFont();
        }

        /// <summary>
        /// Indicates whether the CaptionForeColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeCaptionForeColor()
        {
            return this.Caption.ShouldSerializeForeColor();
        }

        internal bool ShouldSerializeForeColor()
        {
            return !Grid.DefaultForeBrush.Color.Equals(this.ForeColor);
        }
        /// <summary>
        /// Indicates whether the GridLineColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeGridLineColor()
        {
            return !this.GridLineBrush.Equals(Grid.DefaultGridLineBrush);
        }

        /// <summary>
        /// Indicates whether the HeaderBackColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeHeaderBackColor()
        {
            return !this.HeaderBackBrush.Equals(Grid.DefaultHeaderBackBrush);
        }
        /// <summary>
        /// Indicates whether the HeaderFont property should be persisted
        /// </summary>
        /// <returns></returns>
        protected bool ShouldSerializeHeaderFont()
        {
            return (this.headerFont != null);
        }

        /// <summary>
        /// Indicates whether the HeaderForeColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeHeaderForeColor()
        {
            return !this.HeaderForePen.Equals(Grid.DefaultHeaderForePen);
        }


        internal virtual bool ShouldSerializeLinkColor()
        {
            return !this.LinkBrush.Equals(Grid.DefaultLinkBrush);
        }

        /// <summary>
        /// Indicates whether the LinkHoverColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeLinkHoverColor()
        {
            return false;
        }
        /// <summary>
        /// Indicates whether the PreferredRowHeight property should be persisted
        /// </summary>
        /// <returns></returns>
        protected bool ShouldSerializePreferredRowHeight()
        {
            return (this.prefferedRowHeight != (Grid.defaultFontHeight + 3));
        }
        /// <summary>
        /// Indicates whether the SelectionBackColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected bool ShouldSerializeSelectionBackColor()
        {
            return !Grid.DefaultSelectionBackBrush.Equals(this.selectionBackBrush);
        }
        /// <summary>
        /// Indicates whether the SelectionForeColor property should be persisted
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeSelectionForeColor()
        {
            return !this.SelectionForeBrush.Equals(Grid.DefaultSelectionForeBrush);
        }
        
        /// <summary>
        /// Adds or removes the GridTable objects from the container that is associated with the Grid
        /// </summary>
        /// <param name="site"></param>
        public void SubObjectsSiteChange(bool site)
        {
            Grid grid1 = this;
            if (grid1.DesignMode && (grid1.Site != null))
            {
                IDesignerHost host1 = (IDesignerHost)grid1.Site.GetService(typeof(IDesignerHost));
                if (host1 != null)
                {
                    DesignerTransaction transaction1 = host1.CreateTransaction();
                    try
                    {
                        IContainer container1 = grid1.Site.Container;
                        GridTableStyle style1 = this.myGridTable;// styleArray1[num1];
                        /*site*/ //this.ObjectSiteChange(container1, style1, site);
                        GridColumnStyle[] styleArray2 = new GridColumnStyle[style1.GridColumnStyles.Count];
                        style1.GridColumnStyles.CopyTo(styleArray2, 0);
                        for (int num2 = 0; num2 < styleArray2.Length; num2++)
                        {
                            GridColumnStyle style2 = styleArray2[num2];
                            this.ObjectSiteChange(container1, style2, site);
                        }
                    }
                    finally
                    {
                        transaction1.Commit();
                    }
                }
            }
        }

        internal void TextBoxOnMouseWheel(MouseEventArgs e)
        {
            this.OnMouseWheel(e);
        }

        /// <summary>
        /// Unselects a specified row
        /// </summary>
        /// <param name="row"></param>
        public void UnSelect(int row)
        {
            GridRow[] rowArray1 = this.GridRows;
            if (rowArray1[row].Selected)
            {
                rowArray1[row].Selected = false;
                this.numSelectedRows--;
            }
        }



        private void UpdateListManager()
        {
            try
            {
                if (this.listManager != null)
                {
                    this.EndEdit();
                    this.listManager.EndCurrentEdit();
                }
            }
            catch (Exception)
            {
            }
        }

        private void UnWireDataSource()
        {
            this.listManager.CurrentChanged -= this.currentChangedHandler;
            this.listManager.PositionChanged -= this.positionChangedHandler;
            this.listManager.ItemChanged -= this.itemChangedHandler;
            this.listManager.MetaDataChanged -= this.metaDataChangedHandler;

        }


        private void UnWireTableStylePropChanged(GridTableStyle gridTable)
        {
            gridTable.AllowSortingChanged -= new EventHandler(this.AllowSortingChanged);
        }
        private void WireDataSource()
        {
            this.listManager.CurrentChanged += this.currentChangedHandler;
            this.listManager.PositionChanged += this.positionChangedHandler;
            this.listManager.ItemChanged += this.itemChangedHandler;
            this.listManager.MetaDataChanged += this.metaDataChangedHandler;

        }



        private void WireTableStylePropChanged(GridTableStyle gridTable)
        {
            gridTable.AllowSortingChanged += new EventHandler(this.AllowSortingChanged);

        }


        #endregion

        #region Color properties

        internal Brush AlternatingBackBrush
        {
            get
            {
                return this.alternatingBackBrush;
            }
        }

        [Category("Colors"), Description("GridAlternatingBackColor")]
        internal Color AlternatingBackColor
        {
            get
            {
                return this.alternatingBackBrush.Color;
            }
            set
            {
                if (value.IsEmpty)
                {
                    throw new ArgumentException("GridEmptyColor", "AlternatingBackColor");
                }
                if (Grid.IsTransparentColor(value))
                {
                    throw new ArgumentException("GridTransparentAlternatingBackColorNotAllowed");
                }
                if (!this.alternatingBackBrush.Color.Equals(value))
                {
                    this.alternatingBackBrush = new SolidBrush(value);
                    this.InvalidateInside();
                }
            }
        }

        internal SolidBrush BackBrush
        {
            get
            {
                return this.backBrush;
            }
        }

        /// <summary>
        /// Gets or sets the back color color of an object
        /// </summary>
        [Browsable(false), Description("ControlBackColor"), Category("Colors")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (Grid.IsTransparentColor(value))
                {
                    throw new ArgumentException("GridTransparentBackColorNotAllowed");
                }
                base.BackColor = value;
            }
        }

        internal SolidBrush BackgroundBrush
        {
            get
            {
                return this.backgroundBrush;
            }
        }

        /// <summary>
        /// Gets or sets the background color of the control
        /// </summary>
        [Category("Colors"), Description("GridBackgroundColor")]
        public Color BackgroundColor
        {
            get
            {
                return this.backgroundBrush.Color;
            }
            set
            {
                if (value.IsEmpty)
                {
                    throw new ArgumentException("GridEmptyColor", "BackgroundColor");
                }
                if (!value.Equals(this.backgroundBrush.Color))
                {
                    this.backgroundBrush = new SolidBrush(value);
                    base.Invalidate(this.layout.Inside);
                    this.OnBackgroundColorChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the background image of the control
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }
        /// <summary>
        /// Gets or sets the Caption Back Color of the control
        /// </summary>
        [Description("GridCaptionBackColor"), Category("Colors")]
        internal Color CaptionBackColor
        {
            get
            {
                return this.Caption.BackColor;
            }
            set
            {
                if (Grid.IsTransparentColor(value))
                {
                    throw new ArgumentException("GridTransparentCaptionBackColorNotAllowed");
                }
                this.Caption.BackColor = value;
            }
        }
        /// <summary>
        /// Gets or sets the Caption Fore Color of the control
        /// </summary>
        [Description("GridCaptionForeColor"), Category("Colors")]
        internal Color CaptionForeColor
        {
            get
            {
                return this.Caption.ForeColor;
            }
            set
            {
                this.Caption.ForeColor = value;
            }
        }

        internal SolidBrush HeaderBackBrush
        {
            get
            {
                return this.headerBackBrush;
            }
        }

        [Description("GridHeaderBackColor"), Category("Colors")]
        internal Color HeaderBackColor
        {
            get
            {
                return this.headerBackBrush.Color;
            }
            set
            {
                if (value.IsEmpty)
                {
                    throw new ArgumentException("GridEmptyColor", "HeaderBackColor");
                }
                if (Grid.IsTransparentColor(value))
                {
                    throw new ArgumentException("GridTransparentHeaderBackColorNotAllowed");
                }
                if (!value.Equals(this.headerBackBrush.Color))
                {
                    this.headerBackBrush = new SolidBrush(value);
                    if (this.layout.RowHeadersVisible)
                    {
                        base.Invalidate(this.layout.RowHeaders);
                    }
                    if (this.layout.ColumnHeadersVisible)
                    {
                        base.Invalidate(this.layout.ColumnHeaders);
                    }
                    base.Invalidate(this.layout.TopLeftHeader);
                }
            }
        }

        internal SolidBrush HeaderForeBrush
        {
            get
            {
                return this.headerForeBrush;
            }
        }

        [Category("Colors"), Description("GridHeaderForeColor")]
        internal Color HeaderForeColor
        {
            get
            {
                return this.headerForePen.Color;
            }
            set
            {
                if (value.IsEmpty)
                {
                    throw new ArgumentException("GridEmptyColor", "HeaderForeColor");
                }
                if (!value.Equals(this.headerForePen.Color))
                {
                    this.headerForePen = new Pen(value);
                    this.headerForeBrush = new SolidBrush(value);
                    if (this.layout.RowHeadersVisible)
                    {
                        base.Invalidate(this.layout.RowHeaders);
                    }
                    if (this.layout.ColumnHeadersVisible)
                    {
                        base.Invalidate(this.layout.ColumnHeaders);
                    }
                    base.Invalidate(this.layout.TopLeftHeader);
                }
            }
        }

        internal Pen HeaderForePen
        {
            get
            {
                return this.headerForePen;
            }
        }

        internal SolidBrush ForeBrush
        {
            get
            {
                return this.foreBrush;
            }
        }
        /// <summary>
        /// Gets or sets the Fore Color of the control
        /// </summary>
        [Browsable(false), Description("ControlForeColor"), Category("Colors")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        internal Brush LinkBrush
        {
            get
            {
                return this.linkBrush;
            }
        }
        /// <summary>
        /// Gets or sets the Link Color of the control
        /// </summary>
        [Description("GridLinkColor"), Category("Colors")]
        public Color LinkColor
        {
            get
            {
                return this.linkBrush.Color;
            }
            set
            {
                if (value.IsEmpty)
                {
                    throw new ArgumentException("GridEmptyColor", "LinkColor");
                }
                if (!this.linkBrush.Color.Equals(value))
                {
                    this.linkBrush = new SolidBrush(value);
                    base.Invalidate(this.layout.Data);
                }
            }
        }
        /// <summary>
        /// Gets or sets the value indicating if should Paint Alternating rows
        /// </summary>
        [DefaultValue(false), Localizable(true),  Description("PaintAlternatingRows"), Category("Colors")]//,RefreshProperties( RefreshProperties.Repaint),DesignerSerializationVisibility( DesignerSerializationVisibility.Visible)]
        public bool PaintAlternating
        {
            get
            {
                return this.paintAlternating;
            }
            set
            {
                if (this.paintAlternating != value)
                {
                    this.paintAlternating = value;
                    ResetStyleLayout();
                    this.InvalidateInside();
                }
            }
        }


        internal SolidBrush SelectionBackBrush
        {
            get
            {
                return this.selectionBackBrush;
            }
        }
        [Description("GridSelectionBackColor"), Category("Colors")]
        internal Color SelectionBackColor
        {
            get
            {
                return this.selectionBackBrush.Color;
            }
            set
            {
                if (value.IsEmpty)
                {
                    throw new ArgumentException("GridEmptyColor", "SelectionBackColor");
                }
                if (Grid.IsTransparentColor(value))
                {
                    throw new ArgumentException("GridTransparentSelectionBackColorNotAllowed");
                }
                if (!value.Equals(this.selectionBackBrush.Color))
                {
                    this.selectionBackBrush = new SolidBrush(value);
                    this.InvalidateInside();
                }
            }
        }

        internal SolidBrush SelectionForeBrush
        {
            get
            {
                return this.selectionForeBrush;
            }
        }

        [Description("GridSelectionForeColor"), Category("Colors")]
        internal Color SelectionForeColor
        {
            get
            {
                return this.selectionForeBrush.Color;
            }
            set
            {
                if (value.IsEmpty)
                {
                    throw new ArgumentException("GridEmptyColor", "SelectionForeColor");
                }
                if (!value.Equals(this.selectionForeBrush.Color))
                {
                    this.selectionForeBrush = new SolidBrush(value);
                    this.InvalidateInside();
                }
            }
        }

        #endregion

        #region properties
        /// <summary>
        /// Get or Set value indicating whether Grid ContextMenu is allowed
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Allow Grid Context Menu")]
        public bool AllowGridContextMenu
        {
            get{   return this.allowGridContexMenu;}
            set
            {
                if (this.allowGridContexMenu != value)
                {
                    this.allowGridContexMenu = value;
                    if (!value && _GridContexMenu != null)
                    {
                        _GridContexMenu.Dispose();
                        _GridContexMenu = null;
                    }
                    else
                    {
                        PerformGridContextMenu();
                    }
                }
            }
        }
        /// <summary>
        /// Get or Set value indicating whether Column ContextMenu is allowed
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Allow Grid Columns Context Menu")]
        public bool AllowColumnContextMenu
        {
            get { return this.allowColumnContexMenu; }
            set
            {
                if (this.allowColumnContexMenu != value)
                {
                    this.allowColumnContexMenu = value;
                    if (!value && _GridColumnContexMenu != null)
                    {
                        _GridColumnContexMenu.Dispose();
                        _GridColumnContexMenu = null;
                    }
                    else
                    {
                        PerformColumnContextMenu();
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether navigation is allowed
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("GridNavigationMode")]
        public bool AllowNavigation
        {
            get
            {
                return this.gridState[0x2000];
            }
            set
            {
                if (this.AllowNavigation != value)
                {
                    this.gridState[0x2000] = value;
                    //this.Caption.BackButtonActive = !this.parentRows.IsEmpty() && value;
                    //this.Caption.BackButtonVisible = this.Caption.BackButtonActive;
                    //this.RecreateGridRows();
                    this.OnAllowNavigationChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Get or Set value indicating whether Grid AutoAdjust is allowed
        /// </summary>
        [Description("auto adjust on resize"), DefaultValue(false), Category("Behavior")]
        public bool AutoAdjust
        {
            get
            {
                return this.autoAdjust;
            }
            set
            {
                if (this.autoAdjust != value)
                {
                    this.autoAdjust = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Get the current grid row state
        /// </summary>
        [Browsable(false)]//Added
        public GridRowState GridRowState
        {
            get
            {
               // bool ok = (this.GridRows[this.currentRow] is GridAddNewRow);
                //GridRow gr = this.editRow;
                //try
                if (this.rowNew && (this.currentRow == (this.GridRowsLength - 1)))// && (this.GridRows[this.currentRow] is GridAddNewRow))
                    return GridRowState.New;
                else if (this.rowDirty)
                    return GridRowState.Edit;
                else
                    return GridRowState.Default;
            }
        }
        /// <summary>
        /// Get indicating whether the grid is binding
        /// </summary>
        [Browsable(false)]//Added
        public bool IsBinding
        {
            get
            {
                if (this.listManager == null)
                    return false;
                return this.listManager.IsBinding;
            }
        }

        /// <summary>
        /// Get or Set the Columns Header Alignment
        /// </summary>
        [Description("GridColumns Header Alignmnet"), DefaultValue(HorizontalAlignment.Left), Category("Appearance")]
        public HorizontalAlignment ColumnsHeaderAlignment
        {
            get
            {
                return this.columnsHeaderAlignment;
            }
            set
            {
                if (this.columnsHeaderAlignment != value)
                {
                    this.columnsHeaderAlignment = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Get or Set the value indicating whether the grid allow sorting
        /// </summary>
        [Description("GridAllowSorting"), DefaultValue(true), Category("Behavior")]
        public bool AllowSorting
        {
            get
            {
                return this.gridState[1];
            }
            set
            {
                if (this.AllowSorting != value)
                {
                    this.gridState[1] = value;
                    if (!value && (this.listManager != null))
                    {
                        IList list1 = this.listManager.List;
                        if (list1 is IBindingList)
                        {
                            ((IBindingList)list1).RemoveSort();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get or Set the grid Border Style
        /// </summary>
        [Description("GridBorderStyle"), DispId(-504), DefaultValue(2), Category("Appearance")]
        public BorderStyle BorderStyle
        {
            get
            {
                return this.borderStyle;
            }
            set
            {
                if (!Enum.IsDefined(typeof(BorderStyle), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
                }
                if (this.borderStyle != value)
                {
                    this.borderStyle = value;
                    base.PerformLayout();
                    base.Invalidate();
                    this.OnBorderStyleChanged(EventArgs.Empty);
                }
            }
        }

        internal int BorderWidth
        {
            get
            {
                if (this.BorderStyle == BorderStyle.Fixed3D)
                {
                    return SystemInformation.Border3DSize.Width;
                }
                if (this.BorderStyle == BorderStyle.FixedSingle)
                {
                    return 1;//2;
                }
                return 0;
            }
        }

        private bool Bound
        {
            get
            {
                if (this.listManager != null)
                {
                    return (this.myGridTable != null);
                }
                return false;
            }
        }

        internal GridCaption Caption
        {
            get
            {
                return this.caption;
            }
        }
        /// <summary>
        /// Get or Set the grid Caption Font
        /// </summary>
        [Localizable(true), Category("Appearance"), AmbientValue((string)null), Description("GridCaptionFont")]
        public Font CaptionFont
        {
            get
            {
                return this.Caption.Font;
            }
            set
            {
                this.Caption.Font = value;
            }
        }
        /// <summary>
        /// Get or Set the grid Caption Text
        /// </summary>
        [Description("GridCaptionText"), Localizable(true), Category("Appearance"), DefaultValue("")]
        public string CaptionText
        {
            get
            {
                return this.Caption.Text;
            }
            set
            {
                this.Caption.Text = value;
            }
        }
        /// <summary>
        /// Get or Set the grid Caption visibility
        /// </summary>
        [DefaultValue(true), Description("GridCaptionVisible"), Category("Display")]
        public bool CaptionVisible
        {
            get
            {
                return this.layout.CaptionVisible;
            }
            set
            {
                if (this.layout.CaptionVisible != value)
                {
                    this.layout.CaptionVisible = value;
                    base.PerformLayout();
                    base.Invalidate();
                    this.OnCaptionVisibleChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Get or Set the grid StatusBar visibility
        /// </summary>
        [DefaultValue(false), Description("GridStatusBarVisible"), Category("Display")]
        public bool StatusBarVisible
        {
            get
            {
                return this.layout.StatusBarVisible;
            }
            set
            {
                if (this.layout.StatusBarVisible != value)
                {
                    this.layout.StatusBarVisible = value;
                    if (value)
                    {
                        if (statusBar == null)
                        {
                            statusBar = new GridStatusBar();
                            statusBar.Grid = this;
                            statusBar.SizingGrip = false;
                            this.Controls.Add(statusBar);
                            //ComputeLayout();
                            //statusBar.Bounds = this.layout.StatusBar;
                        }
                    }
                    else
                    {
                        this.Controls.Remove(statusBar);
                        statusBar.Dispose();
                        statusBar = null;
                    }
                    base.PerformLayout();
                    base.Invalidate();
                    //this.OnCaptionVisibleChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Get or Set the grid Column Headers visibility
        /// </summary>
        [Description("GridColumnHeadersVisible"), DefaultValue(true), Category("Display")]
        public bool ColumnHeadersVisible
        {
            get
            {
                return this.gridState[2];
            }
            set
            {
                if (this.ColumnHeadersVisible != value)
                {
                    this.gridState[2] = value;
                    this.layout.ColumnHeadersVisible = value;
                    base.PerformLayout();
                    this.InvalidateInside();
                }
            }
        }
        /// <summary>
        /// Gets or sets which cell has the focus. Not available at design time
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("GridCurrentCell")]
        public GridCell CurrentCell
        {
            get
            {
                return new GridCell(this.currentRow, this.currentCol);
            }
            set
            {
                if (this.layout.dirty)
                {
                    throw new Exception("GridSettingCurrentCellNotGood");
                }
                if (((value.RowNumber != this.currentRow) || (value.ColumnNumber != this.currentCol)) && (((this.GridRowsLength != 0) && (this.myGridTable.GridColumnStyles != null)) && (this.myGridTable.GridColumnStyles.Count != 0)))
                {
                    this.EnsureBound();
                    int curRow = this.currentRow;
                    int curCol = this.currentCol;
                    bool flag1 = this.gridState[0x8000];
                    bool flag2 = false;
                    bool flag3 = false;
                    int colNum = value.ColumnNumber;
                    int rowNum = value.RowNumber;
                    try
                    {
                        int colCount = this.myGridTable.GridColumnStyles.Count;
                        if (colNum < 0)
                        {
                            colNum = 0;
                        }
                        if (colNum >= colCount)
                        {
                            colNum = colCount - 1;
                        }
                        int rowsLength = this.GridRowsLength;
                        GridRow[] rowArray1 = this.GridRows;
                        if (rowNum < 0)
                        {
                            rowNum = 0;
                        }
                        if (rowNum >= rowsLength)
                        {
                            rowNum = rowsLength - 1;
                        }
                        bool rowChanging = this.currentRow != rowNum;
                        if (rowChanging)
                        {
                            if (!OnCurrentRowChanging())
                            {
                                return;
                            }
                        }
                        if (this.currentCol != colNum)
                        {
                            flag2 = true;
                            this.EndEdit();
                            this.currentCol = colNum;
                            this.InvalidateRow(this.currentRow);
                        }
                        if (rowChanging)
                        {
  
                            flag2 = true;
                            this.EndEdit();
                            if (this.currentRow < rowsLength)
                            {
                                rowArray1[this.currentRow].OnRowLeave();
                            }
                            rowArray1[rowNum].OnRowEnter();
                            this.currentRow = rowNum;
                            if (curRow < rowsLength)
                            {
                                this.InvalidateRow(curRow);
                            }
                            this.InvalidateRow(this.currentRow);
                            if (curRow != this.listManager.Position)
                            {
                                flag3 = true;
                                if (this.gridState[0x8000])
                                {
                                    this.AbortEdit();
                                }
                            }
                            else if (this.gridState[0x100000])
                            {

                                this.ListManager.PositionChanged -= this.positionChangedHandler;
                                this.ListManager.CancelCurrentEdit();
                                this.ListManager.Position = this.currentRow;
                                this.ListManager.PositionChanged += this.positionChangedHandler;
                                rowArray1[this.GridRowsLength - 1] = new GridAddNewRow(this, this.myGridTable, this.GridRowsLength - 1);
                                this.SetGridRows(rowArray1, this.GridRowsLength);
                                this.gridState[0x100000] = false;
                                OnNewRowChanged(GridNewRowState.Cancel);
                            }
                            else
                            {
 
                                this.ListManager.EndCurrentEdit();
                                //OnNewRowChanged(GridNewRowState.Cancel);
                                if (rowsLength != this.GridRowsLength)
                                {
                                    this.currentRow = (this.currentRow == (rowsLength - 1)) ? (this.GridRowsLength - 1) : this.currentRow;
                                }
                                if ((this.currentRow == (this.dataGridRowsLength - 1)) && this.policy.AllowAdd)
                                {
                                    this.AddNewRow();
                                }
                                else
                                {
                                     this.ListManager.Position = this.currentRow;
                                }
                            }
                        }
                    }
                    catch (Exception exception1)
                    {
                        DialogResult result1 = MsgBox.ShowQuestion("You are trying to push incorrect value into column , Error is:'" + exception1.Message + "' , Would you like to correct this problem ?");
                        //DialogResult result1 = MessageBox.Show("GridPushedIncorrectValueIntoColumn " + exception1.Message, "GridErrorMessageBoxCaption", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {
                            this.currentRow = curRow;
                            this.currentCol = curCol;
                            this.InvalidateRowHeader(rowNum);
                            this.InvalidateRowHeader(this.currentRow);
                            if (flag1)
                            {
                                this.Edit();
                            }
                        }
                        else
                        {
                            if (((this.currentRow == (this.GridRowsLength - 1)) && (curRow == (this.GridRowsLength - 2))) && (this.GridRows[this.currentRow] is GridAddNewRow))
                            {
                                rowNum = curRow;
                            }
                            this.currentRow = rowNum;
                            this.listManager.PositionChanged -= this.positionChangedHandler;
                            this.listManager.CancelCurrentEdit();
                            this.listManager.Position = rowNum;
                            this.listManager.PositionChanged += this.positionChangedHandler;
                            this.currentRow = rowNum;
                            this.currentCol = colNum;
                            if (flag1)
                            {
                                this.Edit();
                            }
                        }
                    }
                    if (flag2)
                    {
                        this.EnsureVisible(this.currentRow, this.currentCol);
                        this.OnCurrentCellChanged(EventArgs.Empty);
                        if (!flag3)
                        {
                            this.Edit();
                        }
                        else
                        {
                            base.AccessibilityNotifyClients(AccessibleEvents.Focus, this.myGridTable.GridColumnStyles.Count + this.currentRow);
                            base.AccessibilityNotifyClients(AccessibleEvents.Selection, this.myGridTable.GridColumnStyles.Count + this.currentRow);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Go To specified Cell by row and column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void GoToCell(int row, int col)
        {
            if (row < 0 || row >= RowCount)
                return;
            if (col < 0 || col >= myGridTable.GridColumnStyles.Count)
                return;

            this.CurrentCell = new GridCell(row, col);
        }
        /// <summary>
        /// Refresh Current 
        /// </summary>
        public void RefreshCurrent()
        {
            if (this.layout.dirty)
            {
                throw new Exception("GridSettingCurrentCellNotGood");
            }
            if (!((((this.GridRowsLength != 0) && (this.myGridTable.GridColumnStyles != null)) && (this.myGridTable.GridColumnStyles.Count != 0))))
            {
                return;
            }
            this.EnsureBound();

            RefreshCurrent(this.currentRow);
        }

        private void RefreshCurrent(int rowNum)
        {
            int curRow = this.currentRow;
            bool flag1 = this.gridState[0x8000];
            bool flag2 = false;
            bool flag3 = false;

            try
            {
                bool rowChanging = this.currentRow != rowNum;
                if (rowChanging)
                {
                    if (!OnCurrentRowChanging())
                    {
                        return;
                    }
                }

                int rowsLength = this.GridRowsLength;
                GridRow[] rowArray1 = this.GridRows;

                flag2 = true;
                this.EndEdit();
                if (this.currentRow < rowsLength)
                {
                    rowArray1[this.currentRow].OnRowLeave();
                }
                rowArray1[rowNum].OnRowEnter();
                this.currentRow = rowNum;
                if (curRow < rowsLength)
                {
                    this.InvalidateRow(curRow);
                }
                this.InvalidateRow(this.currentRow);
                if (curRow != this.listManager.Position)
                {
                    flag3 = true;
                    if (this.gridState[0x8000])
                    {
                        this.AbortEdit();
                    }
                }
                else if (this.gridState[0x100000])
                {

                    this.ListManager.PositionChanged -= this.positionChangedHandler;
                    this.ListManager.CancelCurrentEdit();
                    this.ListManager.Position = this.currentRow;
                    this.ListManager.PositionChanged += this.positionChangedHandler;
                    rowArray1[this.GridRowsLength - 1] = new GridAddNewRow(this, this.myGridTable, this.GridRowsLength - 1);
                    this.SetGridRows(rowArray1, this.GridRowsLength);
                    this.gridState[0x100000] = false;
                    OnNewRowChanged(GridNewRowState.Cancel);
                }
                else
                {

                    this.ListManager.EndCurrentEdit();
                    //OnNewRowChanged(GridNewRowState.Cancel);
                    if (rowsLength != this.GridRowsLength)
                    {
                        this.currentRow = (this.currentRow == (rowsLength - 1)) ? (this.GridRowsLength - 1) : this.currentRow;
                    }
                    if ((this.currentRow == (this.dataGridRowsLength - 1)) && this.policy.AllowAdd)
                    {
                        this.AddNewRow();
                    }
                    else
                    {
                        this.ListManager.Position = this.currentRow;
                    }
                    this.OnDirtyRow(GridDirtyRowState.Completed);
                }

            }
            catch (Exception exception1)
            {
                DialogResult result1 = MsgBox.ShowQuestion("You are trying to push incorrect value into column , Error is:'" + exception1.Message + "' , Would you like to correct this problem ?");
                //DialogResult result1 = MessageBox.Show("GridPushedIncorrectValueIntoColumn " + exception1.Message, "GridErrorMessageBoxCaption", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    this.currentRow = curRow;
                    //this.currentCol = curCol;
                    this.InvalidateRowHeader(rowNum);
                    this.InvalidateRowHeader(this.currentRow);
                    if (flag1)
                    {
                        this.Edit();
                    }
                }
                else
                {
                    if (((this.currentRow == (this.GridRowsLength - 1)) && (curRow == (this.GridRowsLength - 2))) && (this.GridRows[this.currentRow] is GridAddNewRow))
                    {
                        rowNum = curRow;
                    }
                    this.currentRow = rowNum;
                    this.listManager.PositionChanged -= this.positionChangedHandler;
                    this.listManager.CancelCurrentEdit();
                    this.listManager.Position = rowNum;
                    this.listManager.PositionChanged += this.positionChangedHandler;
                    this.currentRow = rowNum;
                    //this.currentCol = colNum;
                    if (flag1)
                    {
                        this.Edit();
                    }
                }
            }
            if (flag2)
            {
                this.EnsureVisible(this.currentRow, this.currentCol);
                this.OnCurrentCellChanged(EventArgs.Empty);
                if (!flag3)
                {
                    this.Edit();
                }
                else
                {
                    base.AccessibilityNotifyClients(AccessibleEvents.Focus, this.myGridTable.GridColumnStyles.Count + this.currentRow);
                    base.AccessibilityNotifyClients(AccessibleEvents.Selection, this.myGridTable.GridColumnStyles.Count + this.currentRow);
                }
            }
        }
     
      

        private int CurrentColumn
        {
            get
            {
                return this.CurrentCell.ColumnNumber;
            }
            set
            {
                this.CurrentCell = new GridCell(this.currentRow, value);
            }
        }
        private int CurrentRow
        {
            get
            {
                return this.CurrentCell.RowNumber;
            }
            set
            {
                this.CurrentCell = new GridCell(value, this.currentCol);
            }
        }
        /// <summary>
        /// Gets or sets index of the row that currently has focus
        /// </summary>
        [Browsable(false), Description("GridSelectedIndex"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentRowIndex
        {
            get
            {
                if (this.originalState == null)
                {
                    if (this.ListManager != null)
                    {
                        return this.ListManager.Position;
                    }
                    return -1;
                }
                if (this.BindingContext == null)
                {
                    return -1;
                }
                //BindManager manager1 =base.GetBindManager(this.originalState.DataSource,this.originalState.DataMember) ;
                BindManager manager1 = (BindManager)this.BindContext[this.originalState.DataSource, this.originalState.DataMember];

                //BindingManagerBase bm = (BindingManagerBase) this.BindingContext[this.originalState.DataSource, this.originalState.DataMember];
                //BindManager manager1 = (BindManager.GetBindManager(this.dataSource,this.originalState.DataMember, bm)) ;
                return manager1.Position;
            }
            set
            {
                if (this.ListManager == null)
                {
                    throw new InvalidOperationException("GridSetSelectIndex");
                }
                if (this.originalState == null)
                {
                    this.ListManager.Position = value;
                    this.currentRow = value;
                }
                else
                {
                    //BindManager manager1 =base.GetBindManager(this.originalState.DataSource,this.originalState.DataMember) ;

                    //BindingManagerBase bm = (BindingManagerBase) this.BindingContext[this.originalState.DataSource, this.originalState.DataMember];
                    //BindManager manager1 = (BindManager.GetBindManager(this.dataSource,this.originalState.DataMember, bm)) ;
                    BindManager manager1 = (BindManager)this.BindContext[this.originalState.DataSource, this.originalState.DataMember];
                    manager1.Position = value;
                    this.originalState.LinkingRow = this.originalState.GridRows[value];
                    base.Invalidate();
                }
            }
        }
        /// <summary>
        /// Gets or sets the ambient cursor of an object
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }

        internal GridRow[] GridRows
        {
            get
            {
                if (this.dataGridRows == null)
                {
                    this.CreateGridRows();
                }
                return this.dataGridRows;
            }
        }
        internal int GridRowsLength
        {
            get
            {
                return this.dataGridRowsLength;
            }
        }
        /// <summary>
        /// Gets or sets the name of the list or table in the data source for which the Grid is displaying data
        /// </summary>
        [DefaultValue((string)null), Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Description("GridDataMember"), Category("Data")]
        public string DataMember
        {
            get
            {
                return this.dataMember;
            }
            set
            {
                if ((this.dataMember == null) || !this.dataMember.Equals(value))
                {
                    //this.parentRows.Clear();
                    this.originalState = null;
                    //this.caption.BackButtonActive = this.caption.DownButtonActive = this.caption.BackButtonVisible = false;
                    this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
                    this.Set_ListManager(this.DataSource, value, false);
                }
            }
        }
        /// <summary>
        /// Gets or sets the data source that the Grid is displaying data for
        /// </summary>
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Description("GridDataSource"), DefaultValue((string)null), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if (((value != null) && !(value is IList)) && !(value is IListSource))
                {
                    throw new Exception("BadDataSourceForComplexBinding");
                }
                if ((this.dataSource == null) || !this.dataSource.Equals(value))
                {
                    if (((value == null) || (value == Convert.DBNull)) && !"".Equals(this.DataMember))
                    {
                        this.dataSource = null;
                        this.DataMember = "";
                    }
                    else
                    {
                        if (value != null)
                        {
                            this.EnforceValidDataMember(value);
                        }
                        //this.parentRows.Clear();
                        this.originalState = null;
                        //this.caption.BackButtonActive = this.caption.DownButtonActive = this.caption.BackButtonVisible = false;
                        this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
                        this.Set_ListManager(value, this.DataMember, false);
                    }
                }
            }
        }

        internal static SolidBrush DefaultAlternatingBackBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.Window;
            }
        }
        internal static SolidBrush DefaultBackBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.Window;
            }
        }

        private static SolidBrush DefaultBackgroundBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.AppWorkspace;
            }
        }
        internal static SolidBrush DefaultForeBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.WindowText;
            }
        }

        private static SolidBrush DefaultGridLineBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.Control;
            }
        }
        private static SolidBrush DefaultHeaderBackBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.Control;
            }
        }

        private static SolidBrush DefaultHeaderForeBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.ControlText;
            }
        }
        private static Pen DefaultHeaderForePen
        {
            get
            {
                return new Pen(SystemColors.ControlText);
            }
        }
        private static SolidBrush DefaultLinkBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.HotTrack;
            }
        }
        internal static SolidBrush DefaultParentRowsBackBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.Control;
            }
        }
        internal static SolidBrush DefaultParentRowsForeBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.WindowText;
            }
        }
        private static SolidBrush DefaultSelectionBackBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.ActiveCaption;
            }
        }

        private static SolidBrush DefaultSelectionForeBrush
        {
            get
            {
                return (SolidBrush)SystemBrushes.ActiveCaptionText;
            }
        }
        /// <summary>
        /// Get the defult size of grid
        /// </summary>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(130, 80);
            }
        }
        /// <summary>
        /// Gets the index of the first visible column in a grid
        /// </summary>
        [Description("GridFirstVisibleColumn"), Browsable(false)]
        public int FirstVisibleColumn
        {
            get
            {
                return this.firstVisibleCol;
            }
        }

        [Category("Appearance"), Description("GridFlatMode"), DefaultValue(false)]
        internal bool FlatMode
        {
            get
            {
                return this.gridState[0x40];
            }
            set
            {
                if (value != this.FlatMode)
                {
                    this.gridState[0x40] = value;
                    base.Invalidate(this.layout.Inside);
                    this.OnFlatModeChanged(EventArgs.Empty);
                }
            }
        }

        internal new int FontHeight
        {
            get
            {
                return this.fontHeight;
            }
        }

        internal SolidBrush GridLineBrush
        {
            get
            {
                return this.gridLineBrush;
            }
        }
        /// <summary>
        /// Gets or sets the color of the grid lines.
        /// </summary>
        [Category("Colors"), Description("GridGridLineColor")]
        public Color GridLineColor
        {
            get
            {
                return this.gridLineBrush.Color;
            }
            set
            {
                if (this.gridLineBrush.Color != value)
                {
                    if (value.IsEmpty)
                    {
                        throw new ArgumentException("GridEmptyColor", "GridLineColor");
                    }
                    this.gridLineBrush = new SolidBrush(value);
                    base.Invalidate(this.layout.Data);
                }
            }
        }
        /// <summary>
        /// Gets or sets the line style of the grid
        /// </summary>
        [Description("GridGridLineStyle"), Category("Appearance"), DefaultValue(GridLineStyle.Solid)]
        public GridLineStyle GridLineStyle
        {
            get
            {
                return this.gridLineStyle;
            }
            set
            {
                if (!Enum.IsDefined(typeof(GridLineStyle), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(GridLineStyle));
                }
                if (this.gridLineStyle != value)
                {
                    this.gridLineStyle = value;
                    //this.myGridTable.ResetRelationsUI();
                    base.Invalidate(this.layout.Data);
                }
            }
        }
        internal int GridLineWidth
        {
            get
            {
                if (this.GridLineStyle != GridLineStyle.Solid)
                {
                    return 0;
                }
                return 1;
            }
        }
        /// <summary>
        /// Gets or sets the font used for column headers
        /// </summary>
        [Description("GridHeaderFont"), Category("Appearance")]
        public Font HeaderFont
        {
            get
            {
                if (this.headerFont != null)
                {
                    return this.headerFont;
                }
                return this.Font;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("HeaderFont");
                }
                if (!value.Equals(this.headerFont))
                {
                    this.headerFont = value;
                    this.RecalculateFonts();
                    base.PerformLayout();
                    base.Invalidate(this.layout.Inside);
                }
            }
        }
        internal int HorizontalOffset
        {
            get
            {
                return this.horizontalOffset;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                int num1 = this.GetColumnWidthSum();
                int num2 = num1 - this.layout.Data.Width;
                if ((value > num2) && (num2 > 0))
                {
                    value = num2;
                }
                if (value != this.horizontalOffset)
                {
                    int num3 = this.horizontalOffset - value;
                    this.horizScrollBar.Value = value;
                    Rectangle rectangle1 = this.layout.Data;
                    if (this.layout.ColumnHeadersVisible)
                    {
                        rectangle1 = Rectangle.Union(rectangle1, this.layout.ColumnHeaders);
                    }
                    this.horizontalOffset = value;
                    this.firstVisibleCol = this.ComputeFirstVisibleColumn();
                    this.ComputeVisibleColumns();
                    if (this.gridState[0x20000])
                    {
                        if (((this.currentCol >= this.firstVisibleCol) && (this.currentCol < ((this.firstVisibleCol + this.numVisibleCols) - 1))) && (this.gridState[0x8000] || this.gridState[0x4000]))
                        {
                            this.Edit();
                        }
                        else
                        {
                            this.EndEdit();
                        }
                        this.gridState[0x20000] = false;
                    }
                    else
                    {
                        this.EndEdit();
                    }
                    WinMethods.RECT[] rectArray1 = this.CreateScrollableRegion(rectangle1);
                    this.ScrollRectangles(rectArray1, num3);
                    this.OnScroll(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets the horizontal scroll bar for the grid
        /// </summary>
        [Description("GridHorizScrollBar")]
        protected ScrollBar HorizScrollBar
        {
            get
            {
                return this.horizScrollBar;
            }
        }
        internal bool Initializing
        {
            get
            {
                return this.inInit;
            }
        }
        /// <summary>
        /// Overloaded. Gets or sets the cell located at the intersection of the specified row and column
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        [Browsable(false)]
        public object this[int rowIndex, int columnIndex]
        {
            get
            {
                this.EnsureBound();
                if ((rowIndex < 0) || (rowIndex >= this.GridRowsLength))
                {
                    throw new ArgumentOutOfRangeException("rowIndex");
                }
                if ((columnIndex < 0) || (columnIndex >= this.myGridTable.GridColumnStyles.Count))
                {
                    throw new ArgumentOutOfRangeException("columnIndex");
                }
                BindManager manager1 = this.ListManager;
                GridColumnStyle style1 = this.myGridTable.GridColumnStyles[columnIndex];
                return style1.GetColumnValueAtRow(manager1, rowIndex);
            }
            set
            {
                this.EnsureBound();
                if ((rowIndex < 0) || (rowIndex >= this.GridRowsLength))
                {
                    throw new ArgumentOutOfRangeException("rowIndex");
                }
                if ((columnIndex < 0) || (columnIndex >= this.myGridTable.GridColumnStyles.Count))
                {
                    throw new ArgumentOutOfRangeException("columnIndex");
                }
                BindManager manager1 = this.ListManager;
                if (manager1.Position != rowIndex)
                {
                    manager1.Position = rowIndex;
                }
                this.myGridTable.GridColumnStyles[columnIndex].SetColumnValueAtRow(manager1, rowIndex, value,true);
                //this.ColumnStartedEditing(this.GetCurrentCellBounds());
                //this.OnDirty(GridDirtyRowState.Editing);
                
                if (((columnIndex >= this.firstVisibleCol) && (columnIndex <= ((this.firstVisibleCol + this.numVisibleCols) - 1))) && ((rowIndex >= this.firstVisibleRow) && (rowIndex <= (this.firstVisibleRow + this.numVisibleRows))))
                {
                    Rectangle rectangle1 = this.GetCellBounds(rowIndex, columnIndex);
                    base.Invalidate(rectangle1);
                }
            }
        }
        /// <summary>
        /// Overloaded. Gets or sets the cell located at the intersection of the specified cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        [Browsable(false)]
        public object this[GridCell cell]
        {
            get
            {
                return this[cell.RowNumber, cell.ColumnNumber];
            }
            set
            {
                this[cell.RowNumber, cell.ColumnNumber] = value;
            }
        }

        internal bool LedgerStyle
        {
            get
            {
                return this.gridState[0x20];
            }
        }


        internal Font LinkFont
        {
            get
            {
                return this.linkFont;
            }
        }

        internal int LinkFontHeight
        {
            get
            {
                return this.linkFontHeight;
            }
        }
        /// <summary>
        /// Gets or sets the color displayed when hovering over link text
        /// </summary>
        [ComVisible(false), Description("GridLinkHoverColor"), Category("Colors"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Color LinkHoverColor
        {
            get
            {
                return this.LinkColor;
            }
            set
            {
            }
        }

        private bool ListHasErrors
        {
            get
            {
                return this.gridState[0x80];
            }
            set
            {
                if (this.ListHasErrors != value)
                {
                    this.gridState[0x80] = value;
                    this.ComputeMinimumRowHeaderWidth();
                    if (this.layout.RowHeadersVisible)
                    {
                        if (value)
                        {
                            this.RowHeaderWidth += 15;
                        }
                        else
                        {
                            this.RowHeaderWidth -= 15;
                        }
                    }
                }
            }
        }

        
        /// <summary>
        /// Get the Bind Manager
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Description("GridListManager")]
        protected internal BindManager ListManager
        {
            get
            {
                if (((this.listManager == null) && (this.BindingContext != null)) && (this.DataSource != null))
                {
                    //return base.GetBindManager(this.dataSource, this.DataMember) ;
                    //BindingManagerBase bm = (BindingManagerBase) this.BindingContext[this.DataSource, this.DataMember];
                    //return (BindManager.GetBindManager(this.dataSource, this.DataMember,bm)) ;

                    return (BindManager)this.BindContext[this.DataSource, this.DataMember];
                }
                return this.listManager;
            }
            set
            {
                throw new NotSupportedException("GridSetListManager");
            }
        }
        //		internal Rectangle ParentRowsBounds
        //		{
        //			get
        //			{
        //				return this.layout.ParentRows;
        //			}
        //		}
        //
        //		[Description("GridParentRowsLabelStyle"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(3), Category("Display")]
        //		public GridParentRowsLabelStyle ParentRowsLabelStyle
        //		{
        //			get
        //			{
        //				return this.parentRowsLabels;
        //			}
        //			set
        //			{
        //				if (!Enum.IsDefined(typeof(GridParentRowsLabelStyle), value))
        //				{
        //					throw new InvalidEnumArgumentException("value", (int) value, typeof(GridParentRowsLabelStyle));
        //				}
        //				if (this.parentRowsLabels != value)
        //				{
        //					this.parentRowsLabels = value;
        //					base.Invalidate(this.layout.ParentRows);
        //					this.OnParentRowsLabelStyleChanged(EventArgs.Empty);
        //				}
        //			}
        //		}
        //		[DefaultValue(true), Description("GridParentRowsVisible"), Category("Display")]
        //		public bool ParentRowsVisible
        //		{
        //			get
        //			{
        //				return this.layout.ParentRowsVisible;
        //			}
        //			set
        //			{
        //				if (this.layout.ParentRowsVisible != value)
        //				{
        //					this.SetParentRowsVisibility(value);
        //					this.caption.SetDownButtonDirection(!value);
        //					this.OnParentRowsVisibleChanged(EventArgs.Empty);
        //				}
        //			}
        //		}
        /// <summary>
        /// Gets or sets the default width of the grid columns in pixels
        /// </summary>
        [Description("GridPreferredColumnWidth"), TypeConverter(typeof(GridPreferredColumnWidthTypeConverter)), Category("Layout"), DefaultValue(0x4b)]
        public int PreferredColumnWidth
        {
            get
            {
                return this.preferredColumnWidth;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("GridColumnWidth", "PreferredColumnWidth");
                }
                if (this.preferredColumnWidth != value)
                {
                    this.preferredColumnWidth = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the preferred row height for the Grid control
        /// </summary>
        [Category("Layout"), Description("GridPreferredRowHeight")]
        public int PreferredRowHeight
        {
            get
            {
                return this.prefferedRowHeight;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("GridRowRowHeight");
                }
                this.prefferedRowHeight = value;
            }
        }
        /// <summary>
        /// Gets or sets a value that indicates whether the items in the BoundColumn can be edited
        /// </summary>
        [DefaultValue(false), Category("Behavior"), Description("GridReadOnly")]
        public bool ReadOnly
        {
            get
            {
                return this.gridState[0x1000];
            }
            set
            {
                if (this.ReadOnly != value)
                {
                    //Updated
                    if (!value && m_SelectionType == SelectionType.LabelRow)
                    {
                        throw new ArgumentException("Current Selection Type not allow edit mode");
                        //return;
                    }

                    bool flag1 = false;
                    if (value)
                    {
                        flag1 = this.policy.AllowAdd;
                        this.policy.AllowRemove = false;
                        this.policy.AllowEdit = false;
                        this.policy.AllowAdd = false;
                    }
                    else
                    {
                        flag1 |= this.policy.UpdatePolicy(this.ListManager, value);
                    }
                    this.gridState[0x1000] = value;
                    GridRow[] rowArray1 = this.GridRows;
                    if (flag1)
                    {
                        this.RecreateGridRows(false);
                        GridRow[] rowArray2 = this.GridRows;
                        int num1 = Math.Min(rowArray2.Length, rowArray1.Length);
                        for (int num2 = 0; num2 < num1; num2++)
                        {
                            if (rowArray1[num2].Selected)
                            {
                                rowArray2[num2].Selected = true;
                            }
                        }
                    }
                    base.PerformLayout();
                    this.InvalidateInside();
                    this.OnReadOnlyChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets a value that indicates whether the new rows can be added to grid
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("GridAllowAdd")]
        public bool AllowAdd
        {
            get
            {
                return this.policy.AllowAdd;
            }
            set
            {
                if (this.AllowAdd != value)
                {
                    if (this.ReadOnly)
                    {
                        this.policy.AllowAdd = false;
                        return;
                    }
                    bool flag1 = false;
                    flag1 |= this.policy.UpdatePolicyAllowAdd(this.ListManager, value);

                    //this.policy.AllowAdd = value;
                    GridRow[] rowArray1 = this.GridRows;
                    if (flag1)
                    {
                        this.RecreateGridRows(false);
                        GridRow[] rowArray2 = this.GridRows;
                        int num1 = Math.Min(rowArray2.Length, rowArray1.Length);
                        for (int num2 = 0; num2 < num1; num2++)
                        {
                            if (rowArray1[num2].Selected)
                            {
                                rowArray2[num2].Selected = true;
                            }
                        }
                    }
                    base.PerformLayout();
                    this.InvalidateInside();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the deletes rows are allowed
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("GridAllowRemove")]
        public bool AllowRemove
        {
            get
            {
                return this.policy.AllowRemove;
            }
            set
            {
                if (this.ReadOnly)
                    return;
                if (this.AllowRemove != value)
                {
                    this.policy.AllowRemove = value;
                    base.PerformLayout();
                    this.InvalidateInside();
                    //this.OnReadOnlyChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the user can drag column headers to reorder columns in the control
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("AllowColumnResize")]
        public bool AllowColumnResize
        {
            get
            {
                return this.allowColumnResize;
            }
            set
            {
                if (this.allowColumnResize != value)
                {
                    this.allowColumnResize = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the user can drag rows headers to reorder rows in the control
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("AllowRowResize")]
        public bool AllowRowResize
        {
            get
            {
                return this.allowRowResize;
            }
            set
            {
                if (this.allowRowResize != value)
                {
                    this.allowRowResize = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether row headers are visible
        /// </summary>
        [Category("Display"), DefaultValue(true), Description("GridRowHeadersVisible")]
        public bool RowHeadersVisible
        {
            get
            {
                return this.gridState[4];
            }
            set
            {
                if (this.RowHeadersVisible != value)
                {
                    this.gridState[4] = value;
                    base.PerformLayout();
                    this.InvalidateInside();
                }
            }
        }
        /// <summary>
        /// Gets or sets the width of row headers.
        /// </summary>
        [DefaultValue(0x23), Description("GridRowHeaderWidth"), Category("Layout")]
        public int RowHeaderWidth
        {
            get
            {
                return this.rowHeaderWidth;
            }
            set
            {
                value = Math.Max(this.minRowHeaderWidth, value);
                if (this.rowHeaderWidth != value)
                {
                    this.rowHeaderWidth = value;
                    if (this.layout.RowHeadersVisible)
                    {
                        base.PerformLayout();
                        this.InvalidateInside();
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets the ISite of the Component
        /// </summary>
        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                ISite site1 = this.Site;
                base.Site = value;
                if (value != site1)
                {
                    this.SubObjectsSiteChange(false);
                    this.SubObjectsSiteChange(true);
                }
            }
        }
        /// <summary>
        /// Overridden. Gets or sets the text associated with the control
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), Bindable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        internal int ToolTipId
        {
            get
            {
                return this.toolTipId;
            }
            set
            {
                this.toolTipId = value;
            }
        }

        internal GridToolTip ToolTipProvider
        {
            get
            {
                return this.toolTipProvider;
            }
        }
        /// <summary>
        /// Gets the vertical scroll bar of the control
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced), Description("GridVertScrollBar"), Browsable(false)]
        internal protected ScrollBar VertScrollBar
        {
            get
            {
                return this.vertScrollBar;
            }
        }

        /// <summary>
        /// Get value indicating whether if current row is a new row
        /// </summary>
        [Browsable(false), Description("IsGridNewRow")]
        public bool IsNewRow
        {
            get
            {
                return rowNew;

                //bool stat = gridState[GRIDSTATE_inAddNewRow];
                //stat = gridState[GRIDSTATE_inDeleteRow];
                //stat = gridState[GRIDSTATE_inListAddNew];
                //stat = gridState[GRIDSTATE_inSetListManager];
                //stat = gridState[GRIDSTATE_isEditing];

                //DataRowView drv = this.GetCurrentDataRow();
                //if (drv == null)
                //    return false;
                //return drv.IsNew;
            }
        }

        /// <summary>
        /// Get value of Visible Vertical Scroll Bar Width
        /// </summary>
        [Browsable(false), Description("GridVisibleVGerticalScrollBar")]
        public int VisibleVerticalScrollBarWidth
        {
            get
            {
                return this.vertScrollBar.Visible ? this.vertScrollBar.Width : 0;
            }
        }
        /// <summary>
        /// Gets the number of visible columns
        /// </summary>
        [Browsable(false), Description("GridVisibleColumnCount")]
        public int VisibleColumnCount
        {
            get
            {
                return Math.Min(this.numVisibleCols, (this.myGridTable == null) ? 0 : this.myGridTable.GridColumnStyles.Count);
            }
        }
        /// <summary>
        /// Gets the number of rows visible
        /// </summary>
        [Browsable(false), Description("GridVisibleRowCount")]
        public int VisibleRowCount
        {
            get
            {
                return this.numVisibleRows;
            }
        }
        #endregion

        #region ILayout
        /// <summary>
        /// member of Style Painter
        /// </summary>
        protected IStyle m_StylePainter;
        /// <summary>
        /// Get the Painter Type
        /// </summary>
        [Browsable(false)]
        public PainterTypes PainterType
        {
            get { return PainterTypes.Grid; }
        }
        /// <summary>
        /// Get or Set the Style Painter
        /// </summary>
        [Category("Colors"), DefaultValue(null), RefreshProperties(RefreshProperties.All)]
        public IStyle StylePainter
        {
            get { return m_StylePainter; }
            set
            {
                if (m_StylePainter != value)
                {
                    if (this.m_StylePainter != null)
                        this.m_StylePainter.PropertyChanged -= new PropertyChangedEventHandler(m_Style_PropertyChanged);
                    m_StylePainter = value;
                    if (this.m_StylePainter != null)
                        this.m_StylePainter.PropertyChanged += new PropertyChangedEventHandler(m_Style_PropertyChanged);
                    OnStylePainterChanged(EventArgs.Empty);
                    this.Invalidate();
                }
            }
        }

        private IStyleGrid Painter
        {
            get { return m_StylePainter as IStyleGrid; }
        }
        /// <summary>
        /// Get the Current LayoutManager
        /// </summary>
        [Browsable(false)]
        public IStyleLayout LayoutManager
        {
            get
            {
                if (m_StylePainter != null)
                    return m_StylePainter.Layout as IStyleLayout;
                else
                    return StyleLayout.DefaultLayout as IStyleLayout;
            }
        }
        /// <summary>
        /// Reset Style Layout
        /// </summary>
        public virtual void ResetStyleLayout()
        {
            SetStyleLayout(LayoutManager.Layout);

            this.gridLineBrush = Grid.DefaultGridLineBrush;
            this.gridLineStyle = GridLineStyle.Solid;
            this.headerForePen = Grid.DefaultHeaderForePen;
            this.linkBrush = Grid.DefaultLinkBrush;

        }

        /// <summary>
        /// Set Style Layout
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetStyleLayout(StyleLayout value)
        {
            //this.borderBrush=new SolidBrush(value.ge;
            if (this.paintAlternating)
                this.alternatingBackBrush = (SolidBrush)value.GetBrushAlternating();
            else
                this.alternatingBackBrush = (SolidBrush)value.GetBrushBack();

            this.backBrush = (SolidBrush)value.GetBrushBack();
            this.foreBrush = (SolidBrush)value.GetBrushText();
            this.headerBackBrush = (SolidBrush)value.GetBrushHeader();//.GetBrushFlat(FlatLayout.Dark);
            this.headerForeBrush = (SolidBrush)value.GetBrushHeaderText();//.GetBrushText();
            this.selectionBackBrush = (SolidBrush)value.GetBrushCaption();
            this.selectionForeBrush = (SolidBrush)value.GetBrushSelectedText();
            //this.backgroundBrush = (SolidBrush)value.GetBrushFlat();


            //			this.gridLineBrush = Grid.DefaultGridLineBrush;
            //			this.gridLineStyle = GridLineStyle.Solid;
            //			this.headerForePen = Grid.DefaultHeaderForePen;
            //			this.linkBrush = Grid.DefaultLinkBrush;

            this.BackColor = this.backBrush.Color;
            this.ForeColor = this.foreBrush.Color;
            this.Font = value.TextFont;
            //this.HeaderFont = value.CaptionFont;
            this.Invalidate();
        }

        /// <summary>
        /// Set Style Layout
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetStyleLayout(Styles value)
        {
            LayoutManager.Layout.StylePlan = value;
            SetStyleLayout(LayoutManager.Layout);

        }
        /// <summary>
        /// Occurs when Style Painter Changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            SetStyleLayout(LayoutManager.Layout);
            this.Invalidate(true);
        }
        /// <summary>
        /// Occurs when Style Property Changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "StylePlan":
                    SetStyleLayout(LayoutManager.StylePlan);
                    this.Invalidate(true);
                    break;
                    //this.selectionBackBrush = (SolidBrush)value.GetBrushCaption();
                    //this.selectionForeBrush = (SolidBrush)value.GetBrushCaptionText();//.GetBrushSelectedText();
                    //this.backgroundBrush = (SolidBrush)value.GetBrushFlat();

                case "HeaderBackColor":
                    this.headerBackBrush = (SolidBrush)LayoutManager.GetBrushHeader();
                    break;
                case "HeaderForeColor":
                    this.headerForeBrush = (SolidBrush)LayoutManager.GetBrushHeaderText();
                    break;
                case "BorderHotColor":
                    break;
                case "BorderColor":
                    break;
                case "BackColor":
                    this.backBrush = (SolidBrush)LayoutManager.GetBrushBack();
                    break;
                case "ForeColor":
                    this.foreBrush = (SolidBrush)LayoutManager.GetBrushText();
                    break;
                case "CaptionBackColor":
                    this.CaptionBackColor = LayoutManager.Layout.CaptionColor; 
                    break;
                case "CaptionForeColor":
                    this.CaptionForeColor = LayoutManager.Layout.CaptionTextColor;
                    break;
                case "TextFont":
                    this.Font = LayoutManager.TextFont;
                    this.HeaderFont = LayoutManager.TextFont;
                    break;
                case "CaptionFont":
                    this.CaptionFont = LayoutManager.CaptionFont; 
                    break;
                case "AlternatingColor":
                    if (this.paintAlternating)
                        this.alternatingBackBrush = (SolidBrush)LayoutManager.GetBrushAlternating();
                    else
                        this.alternatingBackBrush = (SolidBrush)LayoutManager.GetBrushBack();
                    break;
            }
            if (this.DesignMode)
            {
                this.Invalidate(true);
            }
        }

        private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            OnStylePropertyChanged(e);
        }

        internal Brush GetHeaderBackBrush(bool hot, Rectangle rect)
        {
            if (hot)
            {
                return LayoutManager.GetBrushSelected();
            }
            

           switch (m_ControlLayout)
            {
                case ControlLayout.System:
                case ControlLayout.Flat:
                    return this.headerBackBrush;
                case ControlLayout.Visual:
                    return LayoutManager.GetBrushGradient(rect, 270f);
                case ControlLayout.XpLayout:
                    return LayoutManager.GetBrushGradient(rect, 270f);
                case ControlLayout.VistaLayout:
                    return LayoutManager.GetBrushGradientDark(rect, 270f);
                default:
                    return this.headerBackBrush;
            }
        }
        /// <summary>
        /// Get or Set Control Layout property
        /// </summary>
        [DefaultValue(ControlLayout.Visual), Category("Behavior")]
        public ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                if (m_ControlLayout != value)
                {
                    m_ControlLayout = value;
                    this.FlatMode = (value == ControlLayout.Flat);
                    InvalidateCaption();
                    InvalidateInside();
                }
            }
        }

        private void ResetHostingControls()
        {
            if (DesignMode || !IsHandleCreated)
                return;

            foreach(GridColumnStyle column in this.myGridTable.GridColumnStyles)
            {
                column.ResetHostControl();
            }
         }


        #endregion

        #region nestaed  class
        /// <summary>
        /// Nasted class HitTestInfo
        /// </summary>
        public sealed class HitTestInfo
        {
            // Methods
            static HitTestInfo()
            {
                Grid.HitTestInfo.Nowhere = new Grid.HitTestInfo();
            }

            internal HitTestInfo()
            {
                this.type = Grid.HitTestType.None;
                this.type = Grid.HitTestType.None;
                this.row = this.col = -1;
            }

            internal HitTestInfo(Grid.HitTestType type)
            {
                this.type = Grid.HitTestType.None;
                this.type = type;
                this.row = this.col = -1;
            }
            /// <summary>
            /// Determines whether two Object instances are equal
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public override bool Equals(object value)
            {
                if (value is Grid.HitTestInfo)
                {
                    Grid.HitTestInfo info1 = (Grid.HitTestInfo)value;
                    if ((this.type == info1.type) && (this.row == info1.row))
                    {
                        return (this.col == info1.col);
                    }
                }
                return false;
            }
            /// <summary>
            /// Retrieves a value that indicates the hash code value for the object
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return ((((int)this.type) + (this.row << 8)) + (this.col << 0x10));
            }
            /// <summary>
            /// Retrieves a string that indicates the current object
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return ("{ " + this.type.ToString() + "," + this.row.ToString() + "," + this.col.ToString() + "}");
            }



            /// <summary>
            /// Get current Column
            /// </summary>
            public int Column
            {
                get
                {
                    return this.col;
                }
            }
            /// <summary>
            /// Get current row
            /// </summary>
            public int Row
            {
                get
                {
                    return this.row;
                }
            }
            /// <summary>
            /// Get Hit test type
            /// </summary>
            public Grid.HitTestType Type
            {
                get
                {
                    return this.type;
                }
            }


            // Fields
            internal int col;
            /// <summary>
            /// static HitTestInfo
            /// </summary>
            public static readonly Grid.HitTestInfo Nowhere;
            internal int row;
            internal Grid.HitTestType type;
        }

        /// <summary>
        /// HitTestType
        /// </summary>
        [Flags]
        public enum HitTestType
        {
            /// <summary>
            /// Caption
            /// </summary>
            Caption = 0x20,
            /// <summary>
            /// Cell
            /// </summary>
            Cell = 1,
            /// <summary>
            /// ColumnHeader
            /// </summary>
            ColumnHeader = 2,
            /// <summary>
            /// ColumnResize
            /// </summary>
            ColumnResize = 8,
            /// <summary>
            /// None
            /// </summary>
            None = 0,
            /// <summary>
            /// ParentRows
            /// </summary>
            ParentRows = 0x40,
            /// <summary>
            /// RowHeader
            /// </summary>
            RowHeader = 4,
            /// <summary>
            /// RowResize
            /// </summary>
            RowResize = 0x10
        }

        internal class LayoutData
        {
            // Methods
            public LayoutData()
            {
                this.dirty = true;
                this.Inside = Rectangle.Empty;
                this.RowHeaders = Rectangle.Empty;
                this.TopLeftHeader = Rectangle.Empty;
                this.ColumnHeaders = Rectangle.Empty;
                this.Data = Rectangle.Empty;
                this.Caption = Rectangle.Empty;
                this.ParentRows = Rectangle.Empty;
                this.ResizeBoxRect = Rectangle.Empty;
                this.ClientRectangle = Rectangle.Empty;
                //this.StatusBar = Rectangle.Empty;
            }

            public LayoutData(Grid.LayoutData src)
            {
                this.dirty = true;
                this.Inside = Rectangle.Empty;
                this.RowHeaders = Rectangle.Empty;
                this.TopLeftHeader = Rectangle.Empty;
                this.ColumnHeaders = Rectangle.Empty;
                this.Data = Rectangle.Empty;
                this.Caption = Rectangle.Empty;
                this.ParentRows = Rectangle.Empty;
                this.ResizeBoxRect = Rectangle.Empty;
                this.ClientRectangle = Rectangle.Empty;
                //this.StatusBar = Rectangle.Empty;
                this.GrabLayout(src);
            }

            private void GrabLayout(Grid.LayoutData src)
            {
                this.Inside = src.Inside;
                this.TopLeftHeader = src.TopLeftHeader;
                this.ColumnHeaders = src.ColumnHeaders;
                this.RowHeaders = src.RowHeaders;
                this.Data = src.Data;
                this.Caption = src.Caption;
                this.ParentRows = src.ParentRows;
                this.ResizeBoxRect = src.ResizeBoxRect;
                this.ColumnHeadersVisible = src.ColumnHeadersVisible;
                this.RowHeadersVisible = src.RowHeadersVisible;
                this.CaptionVisible = src.CaptionVisible;
                this.ParentRowsVisible = src.ParentRowsVisible;
                this.ClientRectangle = src.ClientRectangle;
                //this.StatusBar = src.StatusBar;
                this.StatusBarVisible = src.StatusBarVisible;
            }

            public override string ToString()
            {
                StringBuilder builder1 = new StringBuilder(200);
                builder1.Append(base.ToString());
                builder1.Append(" { \n");
                builder1.Append("Inside = ");
                builder1.Append(this.Inside.ToString());
                builder1.Append('\n');
                builder1.Append("TopLeftHeader = ");
                builder1.Append(this.TopLeftHeader.ToString());
                builder1.Append('\n');
                builder1.Append("ColumnHeaders = ");
                builder1.Append(this.ColumnHeaders.ToString());
                builder1.Append('\n');
                builder1.Append("RowHeaders = ");
                builder1.Append(this.RowHeaders.ToString());
                builder1.Append('\n');
                builder1.Append("Data = ");
                builder1.Append(this.Data.ToString());
                builder1.Append('\n');
                builder1.Append("Caption = ");
                builder1.Append(this.Caption.ToString());
                builder1.Append('\n');
                builder1.Append("ParentRows = ");
                builder1.Append(this.ParentRows.ToString());
                builder1.Append('\n');
                builder1.Append("ResizeBoxRect = ");
                builder1.Append(this.ResizeBoxRect.ToString());
                builder1.Append('\n');
                builder1.Append("ColumnHeadersVisible = ");
                builder1.Append(this.ColumnHeadersVisible.ToString());
                builder1.Append('\n');
                builder1.Append("RowHeadersVisible = ");
                builder1.Append(this.RowHeadersVisible.ToString());
                builder1.Append('\n');
                builder1.Append("CaptionVisible = ");
                builder1.Append(this.CaptionVisible.ToString());
                builder1.Append('\n');
                builder1.Append("ParentRowsVisible = ");
                builder1.Append(this.ParentRowsVisible.ToString());
                builder1.Append('\n');
                builder1.Append("ClientRectangle = ");
                builder1.Append(this.ClientRectangle.ToString());
                builder1.Append(" } ");
                return builder1.ToString();
            }


            // Fields
            public bool StatusBarVisible;
            //public Rectangle StatusBar;
            public Rectangle Caption;
            public bool CaptionVisible;
            public Rectangle ClientRectangle;
            public Rectangle ColumnHeaders;
            public bool ColumnHeadersVisible;
            public Rectangle Data;
            internal bool dirty;
            public Rectangle Inside;
            public Rectangle ParentRows;
            public bool ParentRowsVisible;
            public Rectangle ResizeBoxRect;
            public Rectangle RowHeaders;
            public bool RowHeadersVisible;
            public Rectangle TopLeftHeader;
        }

        private class Policy
        {
            // Methods
            public Policy()
            {
                this.allowAdd = true;
                this.allowEdit = true;
                this.allowRemove = true;
            }

            public bool UpdatePolicy(BindManager listManager, bool gridReadOnly)
            {
                bool flag1 = false;
                IBindingList list1 = (listManager == null) ? null : (listManager.List as IBindingList);
                if (listManager == null)
                {
                    if (!this.allowAdd)
                    {
                        flag1 = true;
                    }
                    this.allowAdd = this.allowEdit = this.allowRemove = true;
                    return flag1;
                }
                if ((this.AllowAdd != listManager.AllowAdd) && !gridReadOnly)
                {
                    flag1 = true;
                }
                if (!listManager.AllowAdd || gridReadOnly)
                {
                    this.AllowAdd = false;
                }

                //this.AllowAdd = ((listManager.AllowAdd && !gridReadOnly) && (list1 != null)) && list1.SupportsChangeNotification;
                this.AllowEdit = listManager.AllowEdit && !gridReadOnly;
                this.AllowRemove = ((listManager.AllowRemove && !gridReadOnly) && (list1 != null)) && list1.SupportsChangeNotification;
                return flag1;
            }

            public bool UpdatePolicyAllowAdd(BindManager listManager, bool gridAllowAdd)
            {
                bool flag1 = false;
                IBindingList list1 = (listManager == null) ? null : (listManager.List as IBindingList);
                if (listManager == null)
                {
                    if (!this.allowAdd)
                    {
                        flag1 = true;
                    }
                    this.allowAdd = gridAllowAdd;
                    return flag1;
                }
                if (!listManager.AllowAdd && gridAllowAdd)
                {
                    flag1 = this.AllowAdd;
                    this.allowAdd = false;
                    return flag1;
                }
                if ((this.AllowAdd != listManager.AllowAdd) && gridAllowAdd)
                {
                    flag1 = true;
                }

                this.AllowAdd = ((listManager.AllowAdd && gridAllowAdd) && (list1 != null)) && list1.SupportsChangeNotification;
                return flag1;
            }

            // Properties
            public bool AllowAdd
            {
                get
                {
                    return this.allowAdd;
                }
                set
                {
                    if (this.allowAdd != value)
                    {
                        this.allowAdd = value;
                    }
                }
            }

            public bool AllowEdit
            {
                get
                {
                    return this.allowEdit;
                }
                set
                {
                    if (this.allowEdit != value)
                    {
                        this.allowEdit = value;
                    }
                }
            }

            public bool AllowRemove
            {
                get
                {
                    return this.allowRemove;
                }
                set
                {
                    if (this.allowRemove != value)
                    {
                        this.allowRemove = value;
                    }
                }
            }


            // Fields
            private bool allowAdd;
            private bool allowEdit;
            private bool allowRemove;
        }

        private class RegionCracker
        {
            // Methods
            public RegionCracker()
            {
            }

            public static WinMethods.RECT[] CrackRegionData(IntPtr hRgn)
            {
                WinMethods.RECT[] rectArray1 = new WinMethods.RECT[0];
                try
                {
                    int num1 = WinMethods.GetRegionData(new HandleRef(null, hRgn), 0, null);
                    byte[] buffer1 = new byte[num1];
                    if (num1 != WinMethods.GetRegionData(new HandleRef(null, hRgn), num1, buffer1))
                    {
                        throw new InvalidOperationException("GridFailedToGetRegionInfo");
                    }
                    int num2 = Grid.RegionCracker.ToInt(buffer1, 0);
                    int num3 = Grid.RegionCracker.ToInt(buffer1, 8);
                    rectArray1 = Grid.RegionCracker.GetRects(buffer1, num2, num3);
                }
                catch (Exception)
                {
                    throw new Win32Exception();
                }
                return rectArray1;
            }

            private static WinMethods.RECT[] GetRects(byte[] buffer, int cbHeader, int nCount)
            {
                WinMethods.RECT[] rectArray1 = new WinMethods.RECT[nCount];
                int num1 = cbHeader;
                int num2 = 0;
                while (num2 < nCount)
                {
                    int num3 = Grid.RegionCracker.ToInt(buffer, num1);
                    int num4 = Grid.RegionCracker.ToInt(buffer, num1 + 4);
                    int num5 = Grid.RegionCracker.ToInt(buffer, num1 + 8);
                    int num6 = Grid.RegionCracker.ToInt(buffer, num1 + 12);
                    rectArray1[num2] = new WinMethods.RECT(num3, num4, num5, num6);
                    num2++;
                    num1 += 0x10;
                }
                return rectArray1;
            }


            private static int ToInt(byte[] buffer, int offset)
            {
                int num1 = buffer[offset];
                num1 |= buffer[offset + 1] << 8;
                num1 |= buffer[offset + 2] << 0x10;
                return (num1 | (buffer[offset + 3] << 0x18));
            }

        }
        #endregion

    }
}
