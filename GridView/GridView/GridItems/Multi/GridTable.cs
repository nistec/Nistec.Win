using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using mControl.Util;
using mControl.WinCtl.Controls;

namespace mControl.GridStyle
{

	[DesignTimeVisible(false), ToolboxItem(false)]
	public class GridTableStyle : Component, IGridEditingService
	{

		#region Fields

		private const GridLineStyle defaultGridLineStyle = GridLineStyle.Solid;
		private const int defaultPreferredColumnWidth = 0x4b;
		private const int defaultRowHeaderWidth = 0x23;
		private const bool defaultAllowSorting = true;

		private bool allowSorting;
		private int focusedRelation;
		private int focusedTextWidth;
		private GridColumnCollection gridColumns;
		private bool isDefaultTableStyle;
		private string mappingName;
		private string tableName;
		private bool readOnly;
		
		private int relationshipHeight;
		private Rectangle relationshipRect;
		internal const int relationshipSpacing = 1;
		private ArrayList relationsList;

		internal Grid dataGrid;

		internal static readonly Font defaultFont;
		internal static readonly int defaultFontHeight;
		//public static GridTableStyle DefaultTableStyle;


		//		private SolidBrush alternatingBackBrush;
		//		private SolidBrush backBrush;
		//		private bool columnHeadersVisible;
		//		private static readonly object EventAllowSorting;
		//		private static readonly object EventAlternatingBackColor;
		//		private static readonly object EventBackColor;
		//		private static readonly object EventColumnHeadersVisible;
		//		private static readonly object EventForeColor;
		//		private static readonly object EventGridLineColor;
		//		private static readonly object EventGridLineStyle;
		//		private static readonly object EventHeaderBackColor;
		//		private static readonly object EventHeaderFont;
		//		private static readonly object EventHeaderForeColor;
		//		private static readonly object EventLinkColor;
		//		private static readonly object EventLinkHoverColor;
		//		private static readonly object EventMappingName;
		//		private static readonly object EventPreferredColumnWidth;
		//		private static readonly object EventPreferredRowHeight;
		//		private static readonly object EventReadOnly;
		//		private static readonly object EventRowHeadersVisible;
		//		private static readonly object EventRowHeaderWidth;
		//		private static readonly object EventSelectionBackColor;
		//		private static readonly object EventSelectionForeColor;
		//		private SolidBrush foreBrush;
		//		private SolidBrush gridLineBrush;
		//		private GridLineStyle gridLineStyle;
		//		internal SolidBrush headerBackBrush;
		//		internal Font headerFont;
		//		internal SolidBrush headerForeBrush;
		//		internal Pen headerForePen;
		//		private SolidBrush linkBrush;
		//		internal int preferredColumnWidth;
		//		private int prefferedRowHeight;
		//		private bool rowHeadersVisible;
		//		private int rowHeaderWidth;
		//		private SolidBrush selectionBackBrush;
		//		private SolidBrush selectionForeBrush;
		#endregion

		#region  Events
		public event EventHandler AllowSortingChanged;
		public event EventHandler MappingNameChanged;
		public event EventHandler ReadOnlyChanged;

//		public event EventHandler AlternatingBackColorChanged;
//		public event EventHandler BackColorChanged;
//		public event EventHandler ColumnHeadersVisibleChanged;
//		public event EventHandler ForeColorChanged;
//		public event EventHandler GridLineColorChanged;
//		public event EventHandler GridLineStyleChanged;
//		public event EventHandler HeaderBackColorChanged;
//		public event EventHandler HeaderFontChanged;
//		public event EventHandler HeaderForeColorChanged;
//		public event EventHandler LinkColorChanged;
//		public event EventHandler LinkHoverColorChanged;
//		public event EventHandler PreferredColumnWidthChanged;
//		public event EventHandler PreferredRowHeightChanged;
//		public event EventHandler RowHeadersVisibleChanged;
//		public event EventHandler RowHeaderWidthChanged;
//		public event EventHandler SelectionBackColorChanged;
//		public event EventHandler SelectionForeColorChanged;

		#endregion

		#region Ctor

		static GridTableStyle()
		{
//			GridTableStyle.EventAllowSorting = new object();
//			GridTableStyle.EventGridLineColor = new object();
//			GridTableStyle.EventGridLineStyle = new object();
//			GridTableStyle.EventHeaderBackColor = new object();
//			GridTableStyle.EventHeaderForeColor = new object();
//			GridTableStyle.EventHeaderFont = new object();
//			GridTableStyle.EventLinkColor = new object();
//			GridTableStyle.EventLinkHoverColor = new object();
//			GridTableStyle.EventPreferredColumnWidth = new object();
//			GridTableStyle.EventPreferredRowHeight = new object();
//			GridTableStyle.EventColumnHeadersVisible = new object();
//			GridTableStyle.EventRowHeaderWidth = new object();
//			GridTableStyle.EventSelectionBackColor = new object();
//			GridTableStyle.EventSelectionForeColor = new object();
//			GridTableStyle.EventMappingName = new object();
//			GridTableStyle.EventAlternatingBackColor = new object();
//			GridTableStyle.EventBackColor = new object();
//			GridTableStyle.EventForeColor = new object();
//			GridTableStyle.EventReadOnly = new object();
//			GridTableStyle.EventRowHeadersVisible = new object();

			GridTableStyle.defaultFont = Control.DefaultFont;
			GridTableStyle.defaultFontHeight = GridTableStyle.defaultFont.Height;
			//GridTableStyle.DefaultTableStyle = new GridTableStyle(true);
		}

		public GridTableStyle() : this(null,false)
		{

		}
		public GridTableStyle(bool isDefaultTableStyle) : this(null,isDefaultTableStyle)
		{
		}

		public GridTableStyle(Grid grid) : this(grid,false)
		{
		}

		public GridTableStyle(Grid grid,bool isDefaultTableStyle)
		{
			this.dataGrid =grid;// null;
			this.relationshipHeight = 0;
			this.relationshipRect = Rectangle.Empty;
			this.focusedRelation = -1;
			this.relationsList = new ArrayList(2);
			this.mappingName = "";
			this.gridColumns = null;
			this.readOnly = false;
			this.isDefaultTableStyle = false;
			this.allowSorting = true;
			this.tableName="";
			
			//TableStyle:this.alternatingBackBrush = GridTableStyle.DefaultAlternatingBackBrush;
			//TableStyle:this.backBrush = GridTableStyle.DefaultBackBrush;
			//TableStyle:this.foreBrush = GridTableStyle.DefaultForeBrush;
			//TableStyle:this.gridLineBrush = GridTableStyle.DefaultGridLineBrush;
			//TableStyle:this.gridLineStyle = GridLineStyle.Solid;
			//TableStyle:this.headerBackBrush = GridTableStyle.DefaultHeaderBackBrush;
			//TableStyle:this.headerFont = null;
			//TableStyle:this.headerForeBrush = GridTableStyle.DefaultHeaderForeBrush;
			//TableStyle:this.headerForePen = GridTableStyle.DefaultHeaderForePen;
			//TableStyle:this.linkBrush = GridTableStyle.DefaultLinkBrush;
			//TableStyle:this.preferredColumnWidth = 0x4b;
			//TableStyle:this.prefferedRowHeight = GridTableStyle.defaultFontHeight + 3;
			//TableStyle:this.selectionBackBrush = GridTableStyle.DefaultSelectionBackBrush;
			//TableStyle:this.selectionForeBrush = GridTableStyle.DefaultSelectionForeBrush;
			//TableStyle:this.rowHeaderWidth = 0x23;
			//TableStyle:this.rowHeadersVisible = true;
			//TableStyle:this.columnHeadersVisible = true;
			this.gridColumns = new GridColumnCollection(this, isDefaultTableStyle);
			this.gridColumns.CollectionChanged += new CollectionChangeEventHandler(this.OnColumnCollectionChanged);
			this.isDefaultTableStyle = isDefaultTableStyle;
		}

		public GridTableStyle(Grid grid,BindManager listManager) : this(grid)
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

		public bool BeginEdit(GridColumnStyle gridColumn, int rowNumber)
		{
			Grid grid1 = this.Grid;
			if (grid1 == null)
			{
				return false;
			}
			return grid1.BeginEdit(gridColumn, rowNumber);
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
					SizeF ef1 = graphics1.MeasureString((string) this.RelationsList[num2], this.Grid.LinkFont);
					int num3 = (int) Math.Ceiling((double) ef1.Width);
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

		protected internal virtual GridColumnStyle CreateGridColumn(PropertyDescriptor prop)
		{
			return this.CreateGridColumn(prop, false);
		}

		protected internal virtual GridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault)
		{
			Type type1 = prop.PropertyType;
			if (type1.Equals(typeof(bool)))
			{
				return  new GridBoolColumn(prop, isDefault);
			}
			if (!type1.Equals(typeof(string)))
			{
				if (type1.Equals(typeof(DateTime)))
				{
					return new GridDateColumn(prop, "d", isDefault);
				}
				if (((type1.Equals(typeof(short)) || type1.Equals(typeof(int))) || (type1.Equals(typeof(long)) || type1.Equals(typeof(ushort)))) || (((type1.Equals(typeof(uint)) || type1.Equals(typeof(ulong))) || (type1.Equals(typeof(decimal)) || type1.Equals(typeof(double)))) || ((type1.Equals(typeof(float)) || type1.Equals(typeof(byte))) || type1.Equals(typeof(sbyte)))))
				{
					return new GridTextColumn(prop, "G", isDefault);
				}
			}
			return new GridTextColumn(prop, isDefault);
		}

 
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				GridColumnCollection collection1 = this.GridColumnStyles;
				if (collection1 != null)
				{
					for (int num1 = 0; num1 < collection1.Count; num1++)
					{
						collection1[num1].Dispose();
					}
				}
			}
			base.Dispose(disposing);
		}

 
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
				return !typeof(Array).IsAssignableFrom(prop.PropertyType);
			}
			return false;
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

		protected virtual void OnMappingNameChanged(EventArgs e)
		{
			if(this.MappingNameChanged!=null)
				this.MappingNameChanged(this,e);

			//			EventHandler handler1 = base.Events[GridTableStyle.EventMappingName] as EventHandler;
			//			if (handler1 != null)
			//			{
			//				handler1(this, e);
			//			}
		}

		protected virtual void OnAllowSortingChanged(EventArgs e)
		{
			if(this.AllowSortingChanged!=null)
				this.AllowSortingChanged(this,e);
//						EventHandler handler1 = base.Events[GridTableStyle.EventAllowSorting] as EventHandler;
//						if (handler1 != null)
//						{
//							handler1(this, e);
//						}
		}

		protected virtual void OnReadOnlyChanged(EventArgs e)
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
		public bool AllowSorting
		{
			get
			{
				return this.allowSorting;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException("GridDefaultTableSet",  "AllowSorting" );
				}
				if (this.allowSorting != value)
				{
					this.allowSorting = value;
					this.OnAllowSortingChanged(EventArgs.Empty);
				}
			}
		}
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
						SizeF ef1 = graphics1.MeasureString((string) this.RelationsList[this.focusedRelation], this.Grid.LinkFont);
						this.focusedTextWidth = (int) Math.Ceiling((double) ef1.Width);
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
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public virtual GridColumnCollection GridColumnStyles
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
 
		public string TableName
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
 

		[Editor("mControl.GridStyle.Design.GridTableMappingNameEditor", typeof(UITypeEditor))]
		public string MappingName
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
					try
					{
						if (this.Grid != null)
						{
							this.Grid.TableStyles.CheckForMappingNameDuplicates(this);
						}
					}
					catch
					{
						this.mappingName = text1;
						throw;
					}
					this.OnMappingNameChanged(EventArgs.Empty);
				}
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
 
		internal ArrayList RelationsList
		{
			get
			{
				return this.relationsList;
			}
		}
 
		[DefaultValue(false)]
		public virtual bool ReadOnly
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
					this.OnReadOnlyChanged(EventArgs.Empty);
				}
			}
		}

		#endregion


		#region Un used properties

		//		internal Brush LinkBrush
		//		{
		//			get
		//			{
		//				return this.linkBrush;
		//			}
		//		}
		//		[Category("Colors"), Description("GridLinkColor")]
		//		public Color LinkColor
		//		{
		//			get
		//			{
		//				return this.linkBrush.Color;
		//			}
		//			set
		//			{
		//				if (this.isDefaultTableStyle)
		//				{
		//					throw new ArgumentException("GridDefaultTableSet", "LinkColor" );
		//				}
		//				if (value.IsEmpty)
		//				{
		//					throw new ArgumentException("GridEmptyColor",  "LinkColor" );
		//				}
		//				if (!this.linkBrush.Color.Equals(value))
		//				{
		//					this.linkBrush = new SolidBrush(value);
		//					this.OnLinkColorChanged(EventArgs.Empty);
		//				}
		//			}
		//		}
 
		//		[EditorBrowsable(EditorBrowsableState.Never), ComVisible(false), Description("GridLinkHoverColor"), Category("Colors"), Browsable(false)]
		//		public Color LinkHoverColor
		//		{
		//			get
		//			{
		//				return this.LinkColor;
		//			}
		//			set
		//			{
		//			}
		//		}

		//		[Localizable(true), TypeConverter(typeof(GridPreferredColumnWidthTypeConverter)), Category("Layout"), DefaultValue(0x4b), Description("GridPreferredColumnWidth")]
		//		public int PreferredColumnWidth
		//		{
		//			get
		//			{
		//				return this.preferredColumnWidth;
		//			}
		//			set
		//			{
		//				if (this.isDefaultTableStyle)
		//				{
		//					throw new ArgumentException("GridDefaultTableSet",  "PreferredColumnWidth" );
		//				}
		//				if (value < 0)
		//				{
		//					throw new ArgumentException("GridColumnWidth", "PreferredColumnWidth");
		//				}
		//				if (this.preferredColumnWidth != value)
		//				{
		//					this.preferredColumnWidth = value;
		//					this.OnPreferredColumnWidthChanged(EventArgs.Empty);
		//				}
		//			}
		//		}
 
		//		[Description("GridPreferredRowHeight"), Localizable(true), Category("Layout")]
		//		public int PreferredRowHeight
		//		{
		//			get
		//			{
		//				return this.prefferedRowHeight;
		//			}
		//			set
		//			{
		//				if (this.isDefaultTableStyle)
		//				{
		//					throw new ArgumentException("GridDefaultTableSet",  "PrefferedRowHeight" );
		//				}
		//				if (value < 0)
		//				{
		//					throw new ArgumentException("GridRowRowHeight");
		//				}
		//				this.prefferedRowHeight = value;
		//				this.OnPreferredRowHeightChanged(EventArgs.Empty);
		//			}
		//		}

		//		[Category("Display"), DefaultValue(true), Description("GridRowHeadersVisible")]
		//		public bool RowHeadersVisible
		//		{
		//			get
		//			{
		//				return this.rowHeadersVisible;
		//			}
		//			set
		//			{
		//				if (this.rowHeadersVisible != value)
		//				{
		//					this.rowHeadersVisible = value;
		//					this.OnRowHeadersVisibleChanged(EventArgs.Empty);
		//				}
		//			}
		//		}
		// 
		//		[Description("GridRowHeaderWidth"), Localizable(true), Category("Layout"), DefaultValue(0x23)]
		//		public int RowHeaderWidth
		//		{
		//			get
		//			{
		//				return this.rowHeaderWidth;
		//			}
		//			set
		//			{
		//				if (this.Grid != null)
		//				{
		//					value = Math.Max(this.Grid.MinimumRowHeaderWidth(), value);
		//				}
		//				if (this.rowHeaderWidth != value)
		//				{
		//					this.rowHeaderWidth = value;
		//					this.OnRowHeaderWidthChanged(EventArgs.Empty);
		//				}
		//			}
		//		}

		#endregion

		#region Un used virtual event
 
 
//		protected virtual void OnAlternatingBackColorChanged(EventArgs e)
//		{
//			if(this.AlternatingBackColorChanged!=null)
//				this.AlternatingBackColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventAlternatingBackColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}
//
//		protected virtual void OnBackColorChanged(EventArgs e)
//		{
//			if(this.BackColorChanged!=null)
//				this.BackColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventForeColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}


//		protected virtual void OnColumnHeadersVisibleChanged(EventArgs e)
//		{
//			if(this.ColumnHeadersVisibleChanged!=null)
//				this.ColumnHeadersVisibleChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventColumnHeadersVisible] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}

//		protected virtual void OnForeColorChanged(EventArgs e)
//		{
//			if(this.ForeColorChanged!=null)
//				this.ForeColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventBackColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}

//		protected virtual void OnGridLineColorChanged(EventArgs e)
//		{
//			if(this.GridLineColorChanged!=null)
//				this.GridLineColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventGridLineColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}

 
//		protected virtual void OnGridLineStyleChanged(EventArgs e)
//		{
//			if(this.GridLineStyleChanged!=null)
//				this.GridLineStyleChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventGridLineStyle] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}

//		protected virtual void OnHeaderBackColorChanged(EventArgs e)
//		{
//			if(this.HeaderBackColorChanged!=null)
//				this.HeaderBackColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventHeaderBackColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}

 
//		protected virtual void OnHeaderFontChanged(EventArgs e)
//		{
//			if(this.HeaderFontChanged!=null)
//				this.HeaderFontChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventHeaderFont] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}
//
 
//		protected virtual void OnHeaderForeColorChanged(EventArgs e)
//		{
//			if(this.HeaderForeColorChanged!=null)
//				this.HeaderForeColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventHeaderForeColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}
//
//		protected virtual void OnLinkColorChanged(EventArgs e)
//		{
//			if(this.LinkColorChanged!=null)
//				this.LinkColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventLinkColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}
//
//		protected virtual void OnLinkHoverColorChanged(EventArgs e)
//		{
//			if(this.LinkHoverColorChanged!=null)
//				this.LinkHoverColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventLinkHoverColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}

 

//		protected virtual void OnPreferredColumnWidthChanged(EventArgs e)
//		{
//			if(this.PreferredColumnWidthChanged!=null)
//				this.PreferredColumnWidthChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventPreferredColumnWidth] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}
//
//		protected virtual void OnPreferredRowHeightChanged(EventArgs e)
//		{
//			if(this.PreferredRowHeightChanged!=null)
//				this.PreferredRowHeightChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventPreferredRowHeight] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}
//
// 
//		protected virtual void OnRowHeadersVisibleChanged(EventArgs e)
//		{
//			if(this.RowHeadersVisibleChanged!=null)
//				this.RowHeadersVisibleChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventRowHeadersVisible] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}
//
//		protected virtual void OnRowHeaderWidthChanged(EventArgs e)
//		{
//			if(this.RowHeaderWidthChanged!=null)
//				this.RowHeaderWidthChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventRowHeaderWidth] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}
//
//		protected virtual void OnSelectionBackColorChanged(EventArgs e)
//		{
//			if(this.SelectionBackColorChanged!=null)
//				this.SelectionBackColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventSelectionBackColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}
//
// 
//		protected virtual void OnSelectionForeColorChanged(EventArgs e)
//		{
//			if(this.SelectionForeColorChanged!=null)
//				this.SelectionForeColorChanged(this,e);
//
////			EventHandler handler1 = base.Events[GridTableStyle.EventSelectionForeColor] as EventHandler;
////			if (handler1 != null)
////			{
////				handler1(this, e);
////			}
//		}

		#endregion

		#region Un used TableStyle:

		//		public void ResetAlternatingBackColor()
		//		{
		//			if (this.ShouldSerializeAlternatingBackColor())
		//			{
		//				this.AlternatingBackColor = GridTableStyle.DefaultAlternatingBackBrush.Color;
		//				this.InvalidateInside();
		//			}
		//		}
		//
		// 
		//		public void ResetBackColor()
		//		{
		//			if (!this.backBrush.Equals(GridTableStyle.DefaultBackBrush))
		//			{
		//				this.BackColor = GridTableStyle.DefaultBackBrush.Color;
		//			}
		//		}
		//
		//		public void ResetForeColor()
		//		{
		//			if (!this.foreBrush.Equals(GridTableStyle.DefaultForeBrush))
		//			{
		//				this.ForeColor = GridTableStyle.DefaultForeBrush.Color;
		//			}
		//		}
		//
		// 
		//		public void ResetGridLineColor()
		//		{
		//			if (this.ShouldSerializeGridLineColor())
		//			{
		//				this.GridLineColor = GridTableStyle.DefaultGridLineBrush.Color;
		//			}
		//		}
		//
		//		public void ResetHeaderBackColor()
		//		{
		//			if (this.ShouldSerializeHeaderBackColor())
		//			{
		//				this.HeaderBackColor = GridTableStyle.DefaultHeaderBackBrush.Color;
		//			}
		//		}
		//
		//		public void ResetHeaderFont()
		//		{
		//			if (this.headerFont != null)
		//			{
		//				this.headerFont = null;
		//				this.OnHeaderFontChanged(EventArgs.Empty);
		//			}
		//		}
		//
		// 
		//		public void ResetHeaderForeColor()
		//		{
		//			if (this.ShouldSerializeHeaderForeColor())
		//			{
		//				this.HeaderForeColor = GridTableStyle.DefaultHeaderForeBrush.Color;
		//			}
		//		}
		//
		//		public void ResetLinkColor()
		//		{
		//			if (this.ShouldSerializeLinkColor())
		//			{
		//				this.LinkColor = GridTableStyle.DefaultLinkBrush.Color;
		//			}
		//		}
		//
		//		public void ResetLinkHoverColor()
		//		{
		//		}
		//
		//		public void ResetSelectionForeColor()
		//		{
		//			if (this.ShouldSerializeSelectionForeColor())
		//			{
		//				this.SelectionForeColor = GridTableStyle.DefaultSelectionForeBrush.Color;
		//			}
		//		}
		//
		//		protected virtual bool ShouldSerializeAlternatingBackColor()
		//		{
		//			return !this.AlternatingBackBrush.Equals(GridTableStyle.DefaultAlternatingBackBrush);
		//		}
		//
		// 
		//		protected bool ShouldSerializeBackColor()
		//		{
		//			return !GridTableStyle.DefaultBackBrush.Equals(this.backBrush);
		//		}
		//
		//		protected bool ShouldSerializeForeColor()
		//		{
		//			return !GridTableStyle.DefaultForeBrush.Equals(this.foreBrush);
		//		}
		//
		// 
		//		protected virtual bool ShouldSerializeGridLineColor()
		//		{
		//			return !this.GridLineBrush.Equals(GridTableStyle.DefaultGridLineBrush);
		//		}
		//
		//		protected virtual bool ShouldSerializeHeaderBackColor()
		//		{
		//			return !this.HeaderBackBrush.Equals(GridTableStyle.DefaultHeaderBackBrush);
		//		}
		//
		//		private bool ShouldSerializeHeaderFont()
		//		{
		//			return (this.headerFont != null);
		//		}
		//
		// 
		//		protected virtual bool ShouldSerializeHeaderForeColor()
		//		{
		//			return !this.HeaderForePen.Equals(GridTableStyle.DefaultHeaderForePen);
		//		}
		//
		//		protected virtual bool ShouldSerializeLinkColor()
		//		{
		//			return !this.LinkBrush.Equals(GridTableStyle.DefaultLinkBrush);
		//		}
		//
		//		protected virtual bool ShouldSerializeLinkHoverColor()
		//		{
		//			return false;
		//		}
		//
		// 
		//		protected bool ShouldSerializePreferredRowHeight()
		//		{
		//			return (this.prefferedRowHeight != (GridTableStyle.defaultFontHeight + 3));
		//		}
		//
		// 
		//		protected bool ShouldSerializeSelectionBackColor()
		//		{
		//			return !GridTableStyle.DefaultSelectionBackBrush.Equals(this.selectionBackBrush);
		//		}
		//
		// 
		//		protected virtual bool ShouldSerializeSelectionForeColor()
		//		{
		//			return !this.SelectionForeBrush.Equals(GridTableStyle.DefaultSelectionForeBrush);
		//		}

		//		public void ResetSelectionBackColor()
		//		{
		//			if (this.ShouldSerializeSelectionBackColor())
		//			{
		//				this.SelectionBackColor = GridTableStyle.DefaultSelectionBackBrush.Color;
		//			}
		//		}

//		internal SolidBrush AlternatingBackBrush
//		{
//			get
//			{
//				return this.alternatingBackBrush;
//			}
//		}
// 
//		[Category("Colors"), Description("GridAlternatingBackColor")]
//		public Color AlternatingBackColor
//		{
//			get
//			{
//				return this.alternatingBackBrush.Color;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet",  "AlternatingBackColor" );
//				}
//				if (Grid.IsTransparentColor(value))
//				{
//					throw new ArgumentException("GridTableStyleTransparentAlternatingBackColorNotAllowed");
//				}
//				if (value.IsEmpty)
//				{
//					throw new ArgumentException("GridEmptyColor",  "AlternatingBackColor" );
//				}
//				if (!this.alternatingBackBrush.Color.Equals(value))
//				{
//					this.alternatingBackBrush = new SolidBrush(value);
//					this.InvalidateInside();
//					this.OnAlternatingBackColorChanged(EventArgs.Empty);
//				}
//			}
//		}
// 
//		internal SolidBrush BackBrush
//		{
//			get
//			{
//				return this.backBrush;
//			}
//		}
// 
//		[Category("Colors"), Description("ControlBackColor")]
//		public Color BackColor
//		{
//			get
//			{
//				return this.backBrush.Color;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet",  "BackColor" );
//				}
//				if (Grid.IsTransparentColor(value))
//				{
//					throw new ArgumentException("GridTableStyleTransparentBackColorNotAllowed");
//				}
//				if (value.IsEmpty)
//				{
//					throw new ArgumentException("GridEmptyColor",  "BackColor" );
//				}
//				if (!this.backBrush.Color.Equals(value))
//				{
//					this.backBrush = new SolidBrush(value);
//					this.InvalidateInside();
//					this.OnBackColorChanged(EventArgs.Empty);
//				}
//			}
//		}
// 
//		internal int BorderWidth
//		{
//			get
//			{
//				GridLineStyle style1;
//				int num1;
//				if (this.Grid == null)
//				{
//					return 0;
//				}
//				if (this.IsDefault)
//				{
//					style1 = this.Grid.GridLineStyle;
//					num1 = this.Grid.GridLineWidth;
//				}
//				else
//				{
//					style1 = this.GridLineStyle;
//					num1 = this.GridLineWidth;
//				}
//				if (style1 == GridLineStyle.None)
//				{
//					return 0;
//				}
//				return num1;
//			}
//		}
// 
//		[DefaultValue(true), Description("GridColumnHeadersVisible"), Category("Display")]
//		public bool ColumnHeadersVisible
//		{
//			get
//			{
//				return this.columnHeadersVisible;
//			}
//			set
//			{
//				if (this.columnHeadersVisible != value)
//				{
//					this.columnHeadersVisible = value;
//					this.OnColumnHeadersVisibleChanged(EventArgs.Empty);
//				}
//			}
//		}

//		internal static SolidBrush DefaultAlternatingBackBrush
//		{
//			get
//			{
//				return (SolidBrush) SystemBrushes.Window;
//			}
//		}
//		internal static SolidBrush DefaultBackBrush
//		{
//			get
//			{
//				return (SolidBrush) SystemBrushes.Window;
//			}
//		}
// 
//		internal static SolidBrush DefaultForeBrush
//		{
//			get
//			{
//				return (SolidBrush) SystemBrushes.WindowText;
//			}
//		}
// 
//		private static SolidBrush DefaultGridLineBrush
//		{
//			get
//			{
//				return (SolidBrush) SystemBrushes.Control;
//			}
//		}
// 
//		private static SolidBrush DefaultHeaderBackBrush
//		{
//			get
//			{
//				return (SolidBrush) SystemBrushes.Control;
//			}
//		}
//		private static SolidBrush DefaultHeaderForeBrush
//		{
//			get
//			{
//				return (SolidBrush) SystemBrushes.ControlText;
//			}
//		}
//		private static Pen DefaultHeaderForePen
//		{
//			get
//			{
//				return new Pen(SystemColors.ControlText);
//			}
//		}
// 
//		private static SolidBrush DefaultLinkBrush
//		{
//			get
//			{
//				return (SolidBrush) SystemBrushes.HotTrack;
//			}
//		}
//		private static SolidBrush DefaultSelectionBackBrush
//		{
//			get
//			{
//				return (SolidBrush) SystemBrushes.ActiveCaption;
//			}
//		}
//		private static SolidBrush DefaultSelectionForeBrush
//		{
//			get
//			{
//				return (SolidBrush) SystemBrushes.ActiveCaptionText;
//			}
//		}

//		internal SolidBrush ForeBrush
//		{
//			get
//			{
//				return this.foreBrush;
//			}
//		}
// 
//		[Description("ControlForeColor"), Category("Colors")]
//		public Color ForeColor
//		{
//			get
//			{
//				return this.foreBrush.Color;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet", "ForeColor" );
//				}
//				if (value.IsEmpty)
//				{
//					throw new ArgumentException("GridEmptyColor",  "BackColor" );
//				}
//				if (!this.foreBrush.Color.Equals(value))
//				{
//					this.foreBrush = new SolidBrush(value);
//					this.InvalidateInside();
//					this.OnForeColorChanged(EventArgs.Empty);
//				}
//			}
//		}

//		internal SolidBrush GridLineBrush
//		{
//			get
//			{
//				return this.gridLineBrush;
//			}
//		}
//		[Description("GridGridLineColor"), Category("Colors")]
//		public Color GridLineColor
//		{
//			get
//			{
//				return this.gridLineBrush.Color;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet", "GridLineColor" );
//				}
//				if (this.gridLineBrush.Color != value)
//				{
//					if (value.IsEmpty)
//					{
//						throw new ArgumentException("GridEmptyColor",  "GridLineColor" );
//					}
//					this.gridLineBrush = new SolidBrush(value);
//					this.OnGridLineColorChanged(EventArgs.Empty);
//				}
//			}
//		}
//		[Description("GridGridLineStyle"), Category("Appearance"), DefaultValue(1)]
//		public GridLineStyle GridLineStyle
//		{
//			get
//			{
//				return this.gridLineStyle;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet", "GridLineStyle" );
//				}
//				if (!Enum.IsDefined(typeof(GridLineStyle), value))
//				{
//					throw new InvalidEnumArgumentException("value", (int) value, typeof(GridLineStyle));
//				}
//				if (this.gridLineStyle != value)
//				{
//					this.gridLineStyle = value;
//					this.OnGridLineStyleChanged(EventArgs.Empty);
//				}
//			}
//		}
// 
//		internal int GridLineWidth
//		{
//			get
//			{
//				if (this.GridLineStyle != GridLineStyle.Solid)
//				{
//					return 0;
//				}
//				return 1;
//			}
//		}
// 
//		internal SolidBrush HeaderBackBrush
//		{
//			get
//			{
//				return this.headerBackBrush;
//			}
//		}
// 
//		[Description("GridHeaderBackColor"), Category("Colors")]
//		public Color HeaderBackColor
//		{
//			get
//			{
//				return this.headerBackBrush.Color;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet", "HeaderBackColor" );
//				}
//				if (Grid.IsTransparentColor(value))
//				{
//					throw new ArgumentException("GridTableStyleTransparentHeaderBackColorNotAllowed");
//				}
//				if (value.IsEmpty)
//				{
//					throw new ArgumentException("GridEmptyColor",  "HeaderBackColor" );
//				}
//				if (!value.Equals(this.headerBackBrush.Color))
//				{
//					this.headerBackBrush = new SolidBrush(value);
//					this.OnHeaderBackColorChanged(EventArgs.Empty);
//				}
//			}
//		}
// 
//		[AmbientValue((string) null), Category("Appearance"), Description("GridHeaderFont"), Localizable(true)]
//		public Font HeaderFont
//		{
//			get
//			{
//				if (this.headerFont != null)
//				{
//					return this.headerFont;
//				}
//				if (this.Grid != null)
//				{
//					return this.Grid.Font;
//				}
//				return Control.DefaultFont;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet",  "HeaderFont" );
//				}
//				if (((value == null) && (this.headerFont != null)) || ((value != null) && !value.Equals(this.headerFont)))
//				{
//					this.headerFont = value;
//					this.OnHeaderFontChanged(EventArgs.Empty);
//				}
//			}
//		}
// 
//		internal SolidBrush HeaderForeBrush
//		{
//			get
//			{
//				return this.headerForeBrush;
//			}
//		}
//		[Category("Colors"), Description("GridHeaderForeColor")]
//		public Color HeaderForeColor
//		{
//			get
//			{
//				return this.headerForePen.Color;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet", "HeaderForeColor" );
//				}
//				if (value.IsEmpty)
//				{
//					throw new ArgumentException("GridEmptyColor", "HeaderForeColor" );
//				}
//				if (!value.Equals(this.headerForePen.Color))
//				{
//					this.headerForePen = new Pen(value);
//					this.headerForeBrush = new SolidBrush(value);
//					this.OnHeaderForeColorChanged(EventArgs.Empty);
//				}
//			}
//		}
// 
//		internal Pen HeaderForePen
//		{
//			get
//			{
//				return this.headerForePen;
//			}
//		}

//		internal SolidBrush SelectionBackBrush
//		{
//			get
//			{
//				return this.selectionBackBrush;
//			}
//		}
// 
//		[Description("GridSelectionBackColor"), Category("Colors")]
//		public Color SelectionBackColor
//		{
//			get
//			{
//				return this.selectionBackBrush.Color;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet", "SelectionBackColor" );
//				}
//				if (Grid.IsTransparentColor(value))
//				{
//					throw new ArgumentException("GridTableStyleTransparentSelectionBackColorNotAllowed");
//				}
//				if (value.IsEmpty)
//				{
//					throw new ArgumentException("GridEmptyColor",  "SelectionBackColor" );
//				}
//				if (!value.Equals(this.selectionBackBrush.Color))
//				{
//					this.selectionBackBrush = new SolidBrush(value);
//					this.InvalidateInside();
//					this.OnSelectionBackColorChanged(EventArgs.Empty);
//				}
//			}
//		}
//		internal SolidBrush SelectionForeBrush
//		{
//			get
//			{
//				return this.selectionForeBrush;
//			}
//		}
// 
//		[Description("GridSelectionForeColor"), Category("Colors")]
//		public Color SelectionForeColor
//		{
//			get
//			{
//				return this.selectionForeBrush.Color;
//			}
//			set
//			{
//				if (this.isDefaultTableStyle)
//				{
//					throw new ArgumentException("GridDefaultTableSet",  "SelectionForeColor" );
//				}
//				if (value.IsEmpty)
//				{
//					throw new ArgumentException("GridEmptyColor", "SelectionForeColor" );
//				}
//				if (!value.Equals(this.selectionForeBrush.Color))
//				{
//					this.selectionForeBrush = new SolidBrush(value);
//					this.InvalidateInside();
//					this.OnSelectionForeColorChanged(EventArgs.Empty);
//				}
//			}
//		}
 
		#endregion


		
	}
 
}