using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Drawing.Design;

using Nistec.WinForms;
using Nistec.Data;
using Nistec.Data.Advanced;
using Nistec.Win;

namespace Nistec.GridView
{
    /// <summary>
    /// Represents the table drawn by the Grid control at run time
    /// </summary>
	[DesignTimeVisible(false), ToolboxItem(false)]
	public class GridTableStyle : Component, IGridEditingService
	{

		#region Fields

		private const GridLineStyle defaultGridLineStyle = GridLineStyle.Solid;
		private const int defaultPreferredColumnWidth = 0x4b;
		private const int defaultRowHeaderWidth = 0x23;
		private const bool defaultAllowSorting = true;

		private bool allowSorting;
		private GridColumnCollection gridColumns;
		private bool isDefaultTableStyle;
		private string mappingName;
		private string tableName;
		private bool readOnly;
		
		internal Grid dataGrid;

        internal static readonly Font defaultFont;
        internal static readonly int defaultFontHeight;
        /// <summary>
        /// DefaultTableStyle member
        /// </summary>
		public static GridTableStyle DefaultTableStyle;

		#endregion

		#region  Events
        /// <summary>
        /// Allow Sorting Changed event
        /// </summary>
		public event System.EventHandler AllowSortingChanged;
        /// <summary>
        /// Mapping Name Changed event
        /// </summary>
        public event System.EventHandler MappingNameChanged;
        /// <summary>
        /// Read Only Changed event
        /// </summary>
        public event System.EventHandler ReadOnlyChanged;


		#endregion

		#region Ctor

		static GridTableStyle()
		{
            GridTableStyle.defaultFont = Control.DefaultFont;
            GridTableStyle.defaultFontHeight = GridTableStyle.defaultFont.Height;
            GridTableStyle.DefaultTableStyle = new GridTableStyle(null,true);
		}
        /// <summary>
        /// GridTableStyle ctor
        /// </summary>
        public GridTableStyle()
            : this(null, false)
        {

        }
        /// <summary>
        /// GridTableStyle ctor
        /// </summary>
        /// <param name="isDefaultTableStyle"></param>
        public GridTableStyle(bool isDefaultTableStyle)
            : this(null, isDefaultTableStyle)
        {
        }
        /// <summary>
        /// GridTableStyle ctor
        /// </summary>
        /// <param name="grid"></param>
        public GridTableStyle(Grid grid)
            : this(grid, false)
        {
        }
        /// <summary>
        /// GridTableStyle ctor
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="isDefaultTableStyle"></param>
		public GridTableStyle(Grid grid, bool isDefaultTableStyle)
		{
            this.dataGrid = grid;// null;
			this.gridColumns = null;
			this.readOnly = false;
			this.isDefaultTableStyle = false;
			this.allowSorting = true;
			this.tableName="";
            this.mappingName = "";
            this.relationsList = new List<string>(2);

			this.gridColumns = new GridColumnCollection(this, isDefaultTableStyle);
			this.gridColumns.CollectionChanged += new CollectionChangeEventHandler(this.OnColumnCollectionChanged);
			this.isDefaultTableStyle = isDefaultTableStyle;
		}

        void dataGrid_DataSourceChanged(object sender, EventArgs e)
        {
            if (relationsList != null && relationsList.Count > 0)
            {
                //int row = dataGrid.currentRow;
                //if (row == -1)
                //    return;
                //GridRow gridRow= dataGrid.GridRows[row];
                //if (gridRow == null)
                //    return;
                if(hashRels==null)
                    return;
                //CloseRelationList(gridRow);

                if (hashRels.Count > 0)
                {
                    foreach (DictionaryEntry d in hashRels)
                    {
                        gridCtl = (GridControl)d.Value;
                        gridCtl.ClosePopUp();
                        //gridCtl.Dispose();
                        //hashRels.Remove(row.number);

                    }
                }
            }
        }

        /// <summary>
        /// GridTableStyle ctor
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="listManager"></param>
        public GridTableStyle(Grid grid, BindManager listManager)
            : this(grid)
		{
			//this.MappingName = this.GetListName();
			this.MappingName = listManager.GetListName();
			this.SetGridColumnStylesCollection(listManager);
		}

//		public GridTableStyle(BindManager listManager) : this()
//		{
//			//this.MappingName = this.GetListName();
//			this.MappingName = listManager.GetListName();
//			this.SetGridColumnStylesCollection(listManager);
//		}

		#endregion

		#region methods
        /// <summary>
        /// Requests an edit operation. 
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
		public bool BeginEdit(GridColumnStyle gridColumn, int rowNumber)
		{
			Grid grid1 = this.Grid;
			if (grid1 == null)
			{
				return false;
			}
			return grid1.BeginEdit(gridColumn, rowNumber);
		}
        /// <summary>
        /// Overloaded. Creates a GridColumnStyle. 
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
		protected internal virtual GridColumnStyle CreateGridColumn(PropertyDescriptor prop)
		{
			return this.CreateGridColumn(prop, false);
		}
        /// <summary>
        /// Overloaded. Creates a GridColumnStyle.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
		protected internal virtual GridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault)
		{
            System.Type type1 = prop.PropertyType;
            if (type1.Equals(typeof(bool)))
            {
                return new GridBoolColumn(prop, isDefault);
            }
			if (!type1.Equals(typeof(string)))
			{
                if (type1.Equals(typeof(System.DateTime)))
				{
					return new GridDateColumn(prop, "G", isDefault);
				}
				if (((type1.Equals(typeof(short)) || type1.Equals(typeof(int))) || (type1.Equals(typeof(long)) || type1.Equals(typeof(ushort)))) || (((type1.Equals(typeof(uint)) || type1.Equals(typeof(ulong))) || (type1.Equals(typeof(decimal)) || type1.Equals(typeof(double)))) || ((type1.Equals(typeof(float)) || type1.Equals(typeof(byte))) || type1.Equals(typeof(sbyte)))))
				{
                    GridTextColumn col = new GridTextColumn(prop, "G", isDefault);
                    col.FormatType = Formats.GeneralNumber;
                    return col;//new GridTextColumn(prop, "G", isDefault);
				}
			}
            return new GridMemoColumn(prop, isDefault);
            //return new GridTextColumn(prop, isDefault);
		}

        //public new void Dispose()
        //{
        //    this.Dispose(true);
        //}
 
        /// <summary>
        /// Overloaded. Overridden. Releases the resources used by the GridTable
        /// </summary>
        /// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
                if(this.ctlPopUp!=null)
                    this.ctlPopUp.SelectedItemClick -= new SelectedPopUpItemEventHandler(ctlPopUp_SelectedItemClick);

				GridColumnCollection collection1 = this.GridColumnStyles;
				if (collection1 != null)
				{
					for (int num1 = 0; num1 < collection1.Count; num1++)
					{
						collection1[num1].Dispose();
					}
                    collection1.Clear();
				}
			}
			base.Dispose(disposing);
		}

 
        /// <summary>
        /// Requests an end to an edit operation.
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="rowNumber"></param>
        /// <param name="shouldAbort"></param>
        /// <returns></returns>
        public bool EndEdit(GridColumnStyle gridColumn, int rowNumber, bool shouldAbort)
        {
            Grid grid1 = this.Grid;
            if (grid1 == null)
            {
                return false;
            }
            return grid1.EndEdit(gridColumn, rowNumber, shouldAbort);
        }

 
		internal void InvalidateColumn(GridColumnStyle column)
		{
			int num1 = this.GridColumnStyles.IndexOf(column);
			if ((num1 >= 0) && (this.Grid != null))
			{
				this.Grid.InvalidateColumn(num1);
			}
		}

		private void InvalidateInside()
		{
			if (this.Grid != null)
			{
				this.Grid.InvalidateInside();
			}
		}

		private bool PropertyDescriptorIsARelation(PropertyDescriptor prop)
		{
			if (typeof(IList).IsAssignableFrom(prop.PropertyType))
			{
                return !typeof(System.Array).IsAssignableFrom(prop.PropertyType);
			}
			return false;
		}

        //internal void SetGridColumnStylesCollection(BindManager listManager)
        //{
        //    this.gridColumns.CollectionChanged -= new CollectionChangeEventHandler(this.OnColumnCollectionChanged);
        //    PropertyDescriptorCollection collection1 = listManager.GetItemProperties();
        //    if (this.relationsList.Count > 0)
        //    {
        //        this.relationsList.Clear();
        //    }
        //    int num1 = collection1.Count;
        //    for (int num2 = 0; num2 < num1; num2++)
        //    {
        //        PropertyDescriptor descriptor1 = collection1[num2];
        //        if (descriptor1.IsBrowsable)
        //        {
        //            GridColumnStyle style1 = this.CreateGridColumn(descriptor1, this.isDefaultTableStyle);
        //            if (this.isDefaultTableStyle)
        //            {
        //                this.gridColumns.AddDefaultColumn(style1);
        //            }
        //            else
        //            {
        //                style1.MappingName = descriptor1.Name;
        //                style1.HeaderText = descriptor1.Name;
        //                this.gridColumns.Add(style1);
        //            }
        //        }
        //    }
        //    this.gridColumns.CollectionChanged += new CollectionChangeEventHandler(this.OnColumnCollectionChanged);
        //}

 
		internal void SetInternalGrid(Grid dG, bool force)
		{
			if (((this.dataGrid == null) || !this.dataGrid.Equals(dG)) || force)
			{
				this.dataGrid = dG;
				if ((dG == null) || !dG.Initializing)
				{
					int num1 = this.gridColumns.Count;
					for (int num2 = 0; num2 < num1; num2++)
					{
						this.gridColumns[num2].SetGridInternalInColumn(dG);
					}
				}
			}
		}
        /// <summary>
        /// Get Bounds Columns
        /// </summary>
        /// <returns></returns>
        public GridColumnStyle[] GetBoundsColumns()
        {
            int cnt = GetBoundsColumnsCount();

            GridColumnStyle[] gcs = new GridColumnStyle[cnt];
            int i = 0;
            foreach (GridColumnStyle c in this.GridColumnStyles)
            {
                if (c.IsVisibleInternal && c.isBound)
                {
                    gcs[i] = c;
                    i++;
                }
            }
            return gcs;
        }
        /// <summary>
        /// Get Visible Columns
        /// </summary>
        /// <returns></returns>
        public GridColumnStyle[] GetVisibleColumns()
        {
            int cnt = GetVisibleColumnsCount();

            GridColumnStyle[] gcs = new GridColumnStyle[cnt];
            int i = 0;
            foreach (GridColumnStyle c in this.GridColumnStyles)
            {
                if (c.IsVisibleInternal /*bound*/)// c != null && c.Visible && c.Width > 0)
                {
                    gcs[i] = c;
                    i++;
                }
            }
            return gcs;
        }
        /// <summary>
        /// Get Visible Columns Count
        /// </summary>
        /// <returns></returns>
        public int GetVisibleColumnsCount()
        {
            int cnt = 0;
            foreach (GridColumnStyle c in this.GridColumnStyles)
            {
                if (c.IsVisibleInternal /*bound*/ /*c != null && c.Visible && c.Width > 0*/) cnt++;
            }
            return cnt;
        }
        /// <summary>
        /// Get Bounds Columns Count
        /// </summary>
        /// <returns></returns>
        public int GetBoundsColumnsCount()
        {
            int cnt = 0;
            foreach (GridColumnStyle c in this.GridColumnStyles)
            {
                if (c.IsVisibleInternal && c.isBound) cnt++;
            }
            return cnt;
        }

		#endregion

		#region virtual event

		private void OnColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			this.gridColumns.CollectionChanged -= new CollectionChangeEventHandler(this.OnColumnCollectionChanged);
			try
			{
				Grid grid1 = this.Grid;
				GridColumnStyle style1 = e.Element as GridColumnStyle;
				if (e.Action == CollectionChangeAction.Add)
				{
					if (style1 != null)
					{
						style1.SetGridInternalInColumn(grid1);
					}
				}
				else if (e.Action == CollectionChangeAction.Remove)
				{
					if (style1 != null)
					{
						style1.SetGridInternalInColumn(null);
					}
				}
				else if (e.Element != null)
				{
					for (int num1 = 0; num1 < this.gridColumns.Count; num1++)
					{
						this.gridColumns[num1].SetGridInternalInColumn(null);
					}
				}
				if (grid1 != null)
				{
					grid1.OnColumnCollectionChanged(this, e);
				}
			}
			finally
			{
				this.gridColumns.CollectionChanged += new CollectionChangeEventHandler(this.OnColumnCollectionChanged);
			}
		}
        /// <summary>
        /// On Mapping Name Changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMappingNameChanged(System.EventArgs e)
		{
			if(this.MappingNameChanged!=null)
				this.MappingNameChanged(this,e);

			//			EventHandler handler1 = base.Events[GridTableStyle.EventMappingName] as EventHandler;
			//			if (handler1 != null)
			//			{
			//				handler1(this, e);
			//			}
		}
        /// <summary>
        /// On Allow Sorting Changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAllowSortingChanged(System.EventArgs e)
		{
			if(this.AllowSortingChanged!=null)
				this.AllowSortingChanged(this,e);
//						EventHandler handler1 = base.Events[GridTableStyle.EventAllowSorting] as EventHandler;
//						if (handler1 != null)
//						{
//							handler1(this, e);
//						}
		}
        /// <summary>
        /// On ReadOnly Changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnReadOnlyChanged(System.EventArgs e)
		{
			if(this.ReadOnlyChanged!=null)
				this.ReadOnlyChanged(this,e);

//			EventHandler handler1 = base.Events[GridTableStyle.EventReadOnly] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

		#endregion

		#region Properties

		[DefaultValue(true), Description("GridAllowSorting"), Category("Behavior")]
		internal bool AllowSorting
		{
			get
			{
				return this.allowSorting;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
                    throw new System.ArgumentException("GridDefaultTableSet", "AllowSorting");
				}
				if (this.allowSorting != value)
				{
					this.allowSorting = value;
                    this.OnAllowSortingChanged(System.EventArgs.Empty);
				}
			}
		}
        /// <summary>
        /// Get the Grid parent
        /// </summary>
		[Browsable(false)]
        public virtual Grid Grid
		{
			get
			{
				return this.dataGrid;
			}
			set
			{
				this.SetInternalGrid(value, true);
			}
		}
 

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		internal virtual GridColumnCollection GridColumnStyles
		{
			get
			{
				return this.gridColumns;
			}
		}
 
		internal bool IsDefault
		{
			get
			{
				return this.isDefaultTableStyle;
			}
		}
 
		internal string TableName
		{
			get
			{
				return this.tableName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!value.Equals(this.tableName))
				{
					this.tableName = value;
				}
			}
		}
 

		[Editor("Nistec.GridView.Design.GridTableMappingNameEditor", typeof(UITypeEditor))]
		internal string MappingName
		{
			get
			{
				return this.mappingName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!value.Equals(this.mappingName))
				{
					string text1 = this.MappingName;
					this.mappingName = value;
//					try
//					{
//						if (this.Grid != null)
//						{
//							this.Grid.TableStyles.CheckForMappingNameDuplicates(this);
//						}
//					}
//					catch
//					{
//						this.mappingName = text1;
//						throw;
//					}
                    this.OnMappingNameChanged(System.EventArgs.Empty);
				}
			}
		}
 

		[DefaultValue(false)]
		internal virtual bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				if (this.readOnly != value)
				{
					this.readOnly = value;
                    this.OnReadOnlyChanged(System.EventArgs.Empty);
				}
			}
		}

		#endregion

        #region multi
        //private List<DataRelation> relations;
        private List<string> relationsList;
        private int relationshipHeight;
        private Rectangle relationshipRect;
        internal const int relationshipSpacing = 1;
        private int focusedRelation;
        private int focusedTextWidth;
        private IDataSource IParent;

        internal int FocusedRelation
        {
            get
            {
                return this.focusedRelation;
            }
            set
            {
                if (this.focusedRelation != value)
                {
                    this.focusedRelation = value;
                    if (this.focusedRelation == -1)
                    {
                        this.focusedTextWidth = 0;
                    }
                    else
                    {
                        Graphics graphics1 = this.Grid.CreateGraphicsInternal();
                        SizeF ef1 = graphics1.MeasureString((string)this.RelationsList[this.focusedRelation], this.Grid.LinkFont);
                        this.focusedTextWidth = (int)Math.Ceiling((double)ef1.Width);
                        graphics1.Dispose();
                    }
                }
            }
        }
        internal int FocusedTextWidth
        {
            get
            {
                return this.focusedTextWidth;
            }
        }

        internal List<string> RelationsList
        {
            get
            {
                return this.relationsList;
            }
        }

        internal Rectangle RelationshipRect
        {
            get
            {
                if (this.relationshipRect.IsEmpty)
                {
                    this.ComputeRelationshipRect();
                }
                return this.relationshipRect;
            }
            set
            {
                this.relationshipRect = value;
            }
        }

        internal int RelationshipHeight
        {
            get
            {
                return this.relationshipHeight;
            }
            set
            {
                this.relationshipHeight = value;
            }
        }
        internal void ResetRelationsList()
        {
            if (this.isDefaultTableStyle)
            {
                this.relationsList.Clear();
            }
        }

        internal void ResetRelationsUI()
        {
            this.relationshipRect = Rectangle.Empty;
            this.focusedRelation = -1;
            this.relationshipHeight = this.dataGrid.LinkFontHeight + 1;
        }

        internal void SetRelationsList(BindManager listManager)
        {
            PropertyDescriptorCollection collection1 = listManager.GetItemProperties();
            int num1 = collection1.Count;
            if (this.relationsList.Count > 0)
            {
                this.relationsList.Clear();
            }
            for (int num2 = 0; num2 < num1; num2++)
            {
                PropertyDescriptor descriptor1 = collection1[num2];
                if (this.PropertyDescriptorIsARelation(descriptor1))
                {
                    this.relationsList.Add(descriptor1.Name);
                }
            }
        }

        internal void SetRelationsList(List<DataRelation> list)
        {
            IParent = this.dataGrid;
            if (this.relationsList.Count > 0)
            {
                //this.dataGrid.DataSourceChanged -= new EventHandler(dataGrid_DataSourceChanged);
                this.relationsList.Clear();
            }
            foreach (DataRelation rel in list)
            {
                if (rel.ParentTable.TableName==this.MappingName)
                {
                    this.relationsList.Add(rel.RelationName);
                }
            }
            //this.dataGrid.DataSourceChanged += new EventHandler(dataGrid_DataSourceChanged);
        }
        
        internal void SetRelationsList(List<string> list,IDataSource parent)
        {
            IParent = parent;
            relationsList = list;
           // this.dataGrid.DataSourceChanged += new EventHandler(dataGrid_DataSourceChanged);

        }

        internal void PairTableStylesReset(BindManager lm)
        {
            if (lm == null)
                return;

            PropertyDescriptorCollection collection1 = lm.GetItemProperties();
            GridColumnCollection collection2 = this.GridColumnStyles;

            //for (int i = 0; i < collection2.Count; i++)
            //{
            //    collection2[i].PropertyDescriptor = null;
            //    collection2[i].isMaped = false;
            //}
            int colIndex = 0;
            for (int indx = 0; indx < collection1.Count && colIndex < collection2.Count; indx++)
            {
                //bool isDesc = false;
                GridColumnStyle style1 = collection2.MapColumnStyleToPropertyName(collection1[indx].Name, collection2[colIndex], colIndex);//, ref isDesc);
                if (style1 != null)
                {
                    style1.isMaped = true;

                    if (style1.IsBound)//isDesc)
                        style1.PropertyDescriptor = collection1[indx];
                    else
                    {
                        //style1.CellBoundInit(lm.Count);
                        indx--;
                    }
                    colIndex++;
                }
            }
            //this.SetRelationsList(lm);

        }
         private Rectangle ComputeRelationshipRect()
        {
            if (this.relationshipRect.IsEmpty && this.Grid.AllowNavigation)
            {
                Graphics graphics1 = this.Grid.CreateGraphicsInternal();
                this.relationshipRect = new Rectangle();
                this.relationshipRect.X = 0;
                int num1 = 0;
                for (int num2 = 0; num2 < this.RelationsList.Count; num2++)
                {
                    SizeF ef1 = graphics1.MeasureString((string)this.RelationsList[num2], this.Grid.LinkFont);
                    int num3 = (int)Math.Ceiling((double)ef1.Width);
                    if (num3 > num1)
                    {
                        num1 = num3;
                    }
                }
                graphics1.Dispose();
                this.relationshipRect.Width = num1 + 5;
                this.relationshipRect.Width += 2;
                this.relationshipRect.Height = this.dataGrid.BorderWidth + (this.relationshipHeight * this.RelationsList.Count);
                this.relationshipRect.Height += 2;
                if (this.RelationsList.Count > 0)
                {
                    this.relationshipRect.Height += 2;
                }
            }
            return this.relationshipRect;
        }


        internal void SetGridColumnStylesCollection(BindManager listManager)
        {
            this.gridColumns.CollectionChanged -= new CollectionChangeEventHandler(this.OnColumnCollectionChanged);
            PropertyDescriptorCollection collection1 = listManager.GetItemProperties();
            if (this.relationsList.Count > 0)
            {
                this.relationsList.Clear();
            }
            int num1 = collection1.Count;
            for (int num2 = 0; num2 < num1; num2++)
            {
                PropertyDescriptor descriptor1 = collection1[num2];
                if (descriptor1.IsBrowsable)
                {
                    if (this.PropertyDescriptorIsARelation(descriptor1))
                    {
                        this.relationsList.Add(descriptor1.Name);
                    }
                    else
                    {
                        GridColumnStyle style1 = this.CreateGridColumn(descriptor1, this.isDefaultTableStyle);
                        if (this.isDefaultTableStyle)
                        {
                            this.gridColumns.AddDefaultColumn(style1);
                        }
                        else
                        {
                            style1.MappingName = descriptor1.Name;
                            style1.HeaderText = descriptor1.Name;
                            this.gridColumns.Add(style1);
                        }
                    }
                }
            }
            this.gridColumns.CollectionChanged += new CollectionChangeEventHandler(this.OnColumnCollectionChanged);
        }
        #endregion

        #region Relations

        internal bool isRelationSet;
        private Nistec.WinForms.McPopUp ctlPopUp;
        private string currentRelation;
        private Rectangle currentRect;
        private int currentRow;
        private GridControl gridCtl;
        private Hashtable hashRels;
        internal event EventHandler RelationExpanded;
        /// <summary>
        /// Get the Relation PopUp
        /// </summary>
        public Nistec.WinForms.McPopUp RelationPopUp
        {
            get
            {
                if (this.ctlPopUp == null)
                {
                    this.ctlPopUp = new McPopUp(this.dataGrid);
                    this.ctlPopUp.UseOwnerWidth = false;
                    this.ctlPopUp.TextOnly = true;
                    this.ctlPopUp.SelectedItemClick += new SelectedPopUpItemEventHandler(ctlPopUp_SelectedItemClick);
                }
                return this.ctlPopUp;
            }
        }

        private void ctlPopUp_SelectedItemClick(object sender, SelectedPopUpItemEvent e)
        {
            if (dataGrid.DataList == null && hashRels.Count == 0)
            {
                SetDataMember(e.Item.Text);
                return;
            }
            this.currentRelation = e.Item.Text;
            ShowGridRelation(currentRect,currentRelation, RelationPopUp.Tag);
        }


        private void SetRelationList()
        {
            if (isRelationSet) return;
            isRelationSet = false;
            RelationPopUp.MenuItems.Clear();
            if (hashRels == null)
                hashRels = new  Hashtable();

            if (this.relationsList.Count == 0) return;

            foreach (string s in relationsList)
            {
                RelationPopUp.MenuItems.AddItem(s, s, -1);
            }
            isRelationSet = true;
        }

        internal void ShowRelationList(GridRow row, Point pt)
        {
            SetRelationList();
            if (this.relationsList.Count == 0) return;

            currentRect = this.dataGrid.GetRowBounds(row);
            currentRow = row.number;

            if (dataGrid.DataList == null && hashRels.Count == 0)
            {
                if(this.relationsList.Count > 0)//1
                    ShowPopUp(currentRect,pt,row.number);
                return;
            }

            gridCtl = new GridControl(row.number);
            if (!hashRels.Contains(row.number))
            {
                hashRels.Add(row.number, gridCtl);
            }

            if (this.relationsList.Count > 1)
            {
                ShowPopUp(currentRect,pt, row.number);
            }
            //else if (this.relationsList.Count > 1)
            //{
            //    ShowGridRelation(currentRect, relationsList[1].ToString(), row.number);
            //}
            else
            {
                ShowGridRelation(currentRect,relationsList[0].ToString(), row.number);
            }
        }
        //Nistec.GridView.Grid relationGrid;

        //internal void ShowRelationList(GridRow row, Rectangle rect)
        //{
        //    relationGrid = new Grid();
        //    relationGrid.Visible=false;
        //    relationGrid.CaptionVisible = false;
        //        //relationGrid.Size = rect.Size;
        //        //relationGrid.SetBounds(rect.X, rect.Y, rect.Width, rect.Height, BoundsSpecified.All);
        //        this.dataGrid.Controls.Add(relationGrid);
                      
        //    Rectangle currentRect = this.dataGrid.GetRowBounds(row);
        //    Rectangle rectBounds = new Rectangle(dataGrid.RowHeaderWidth, currentRect.Top + dataGrid.PreferredRowHeight, dataGrid.layout.Inside.Width-50, rect.Height);

        //    //Point p = this.dataGrid.PointToScreen(new Point(rect.X, rect.Y));

        //    relationGrid.Bounds = rectBounds;
        //        relationGrid.SetDataBinding(this.dataGrid.DataList.Table.Copy(), "");
        //        relationGrid.Visible = true;

        //}
        
        private void ShowPopUp(Rectangle rect,Point pt, object tag)
        {
            int listWidth = (int)RelationPopUp.CalcDropDownWidth();

            Point p = this.dataGrid.PointToScreen(new Point(pt.X, pt.Y + rect.Y));

            //Point p = this.dataGrid.Parent.PointToScreen(new Point(this.dataGrid.Right - listWidth - (this.downButtonRect.Width / 2), this.dataGrid.Top + rect.Top + this.downButtonRect.Height));
            //if (this.dataGrid.RightToLeft == RightToLeft.Yes)
            //    p = this.dataGrid.Parent.PointToScreen(new Point(this.dataGrid.Left + rect.X + (this.downButtonRect.Width / 2), this.dataGrid.Top + rect.Top + this.downButtonRect.Height));

            RelationPopUp.Tag = tag;
            RelationPopUp.ShowPopUp(p);
        }

        internal void CloseRelationList(GridRow row)
        {
            if (hashRels.Contains(row.number))
            {
                gridCtl=(GridControl)hashRels[row.number];
                gridCtl.ClosePopUp();
                gridCtl.Dispose();
                hashRels.Remove(row.number);
            }
            else if (gridCtl != null)
            {
                gridCtl.ClosePopUp();
            }
        }

        private void ShowGridRelation(Rectangle rowRect, string relationName, object key)
        {

            if (hashRels.Contains(key))
            {
                gridCtl = (GridControl)hashRels[key];
            }
            else
            {
                int row = dataGrid.currentRow;
                gridCtl = new GridControl(row);
            }
            if (!gridCtl.Initilaized)
            {
                gridCtl.SetGrid(this.dataGrid);
            }
            gridCtl.SetRelationRows(relationName);
            gridCtl.Bounds = rowRect;
            gridCtl.ShowPopUpInternal(new Point(rowRect.X /*+ gridPoint.X*/, /*gridPoint.Y +*/ rowRect.Bottom));
            if (RelationExpanded != null)
                RelationExpanded(this, EventArgs.Empty);
        }

        private Point GetGridPopUpLocation()
        {
            int x = this.dataGrid.Left;
            int y = this.dataGrid.Top;
            Control cur=this.dataGrid;
            do
            {
                cur=cur.Parent;
                x+=cur.Left;
                y+=cur.Top;

            }
            while (cur.Parent != null && !(cur.Parent is Form));
            return new Point(x, y);
        }

        private void SetDataMember(string dataMember)
        {
            this.dataGrid.forceDefaultTableStyle = true;
            this.dataGrid.DataMember = dataMember;// ((PopUpItem)e.Value).Tag.ToString();
            this.dataGrid.OnNavigate(new NavigateEventArgs(true));
        }

        #endregion

    }
 
}