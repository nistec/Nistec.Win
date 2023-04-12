using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Security.Permissions;
using System.Diagnostics;
using System.Drawing.Design;

using Nistec.WinForms;
using System.Collections.Generic;
using Nistec.Win;


namespace Nistec.GridView
{
 

    /// <summary>
    /// Specifies the appearance, text formatting, and behavior of a Grid control column. This class is abstract.
    /// </summary>
	[DesignTimeVisible(false), DefaultProperty("Header"), ToolboxItem(false)]
    public abstract class GridColumnStyle : Component, IGridColumnEditingNotifyService, IGridColumn
	{

		#region Fields
		private HorizontalAlignment alignment;
		private GridTableStyle gridTableStyle;
		//		private static readonly object EventAlignment;
		//		private static readonly object EventFont;
		//		private static readonly object EventHeaderText;
		//		private static readonly object EventMappingName;
		//		private static readonly object EventNullText;
		//		private static readonly object EventPropertyDescriptor;
		//		private static readonly object EventReadOnly;
		//		private static readonly object EventWidth;
		private Font font;
		internal int fontHeight;
		//private AccessibleObject headerAccessibleObject;
		private string headerName;
		private bool invalid;
		private bool isDefault;
		private string mappingName;
		private string nullText;
		private PropertyDescriptor propertyDescriptor;
		private bool readOnly;
		private bool updating;
		internal int width;
		private int bckWidth;
        private bool autoAdjust;

        /// <summary>
        /// isBound memeber
        /// </summary>
        internal protected bool isBound;/*bound*/
        /// <summary>
        /// Current column text member
        /// </summary>
        protected string m_Text;

        internal GridColumnType m_ColumnType;
        /// <summary>
        /// Current data type member
        /// </summary>
		internal protected FieldType	 m_DataType; 
        /// <summary>
        /// Is Current column Allow null
        /// </summary>
		protected bool		 m_AllowNull;
        /// <summary>
        /// Is Current column Visible
        /// </summary>
		protected bool		 m_Visible;
        /// <summary>
        /// Is Current column allow Un bound
        /// </summary>
        protected bool       m_AllowUnBound;
        /// <summary>
        /// xMargin member
        /// </summary>
		protected int xMargin=2;
        /// <summary>
        /// yMargin member
        /// </summary>
		protected int yMargin=1;
        /// <summary>
        /// bMargin member
        /// </summary>
        protected int bMargin=5;
        /// <summary>
        /// isSelected member
        /// </summary>
        protected bool isSelected;
        /// <summary>
        /// Is Enabled member
        /// </summary>
        protected bool m_Enabled;
        /// <summary>
        /// aggregateMode member
        /// </summary>
        protected AggregateMode aggregateMode;
        internal int editRow;
        internal bool isMaped = false;
        internal Rectangle DesignRect = Rectangle.Empty;
        internal Control HostControl;
        internal Rectangle cellBounds = Rectangle.Empty;
        //internal object defaultValue;

        //internal bool alignToRight = false;

        //protected bool HostedControlVisible;
      
        //internal List<object> cellBoundList;
        //private int cellBoundCount;

        //internal void CellBoundAdd()
        //{
        //    cellBoundList.Add(string.Empty);
        //}
        //internal void CellBoundRemove(int row)
        //{
        //    cellBoundList.RemoveAt(row);
        //}

        //internal void CellBoundInit(int count)
        //{
        //    if (this.cellBoundList != null)
        //    {
        //        this.cellBoundList.Clear();
        //        this.cellBoundList = null;
        //    }
        //    this.cellBoundList = new System.Collections.Generic.List<object>(count);// this.GridTableStyle.dataGrid.RowCount);
        //    if (m_ColumnType== GridColumnType.BoolColumn)
        //    {
        //        //bool[] bools = new bool[count];
        //        //bools.Initialize();
        //        //cellBoundList.AddRange(bools);
        //        for (int i = 0; i < count; i++)
        //        {
        //            cellBoundList.Add(false);
        //        }
        //    }
        //    else if (m_ColumnType == GridColumnType.DateTimeColumn)
        //    {
        //        //DateTime[] dates = new DateTime[count];
        //        //dates.Initialize();
        //        //cellBoundList.AddRange(dates);
        //        for (int i = 0; i < count; i++)
        //        {
        //            cellBoundList.Add(DateTime.Now);
        //        }
        //    }
        //    else
        //    {
        //        //string[] strings=new string[count];
        //        //strings.Initialize();
        //        //cellBoundList.AddRange(strings);
        //        for (int i = 0; i < count; i++)
        //        {
        //            cellBoundList.Add(string.Empty);
        //        }
        //    }
            
        //    cellBoundCount = count;
        //}

 
        #endregion

		#region Events
        /// <summary>
        /// Occurs when the Alignment property value changes
        /// </summary>
		public event EventHandler AlignmentChanged;
        /// <summary>
        /// Occurs when the Font property value changes
        /// </summary>
		public event EventHandler FontChanged;
        /// <summary>
        /// Occurs when the HeaderText property value changes
        /// </summary>
		public event EventHandler HeaderTextChanged;
        /// <summary>
        /// Occurs when the MappingName property value changes
        /// </summary>
		public event EventHandler MappingNameChanged;
        /// <summary>
        /// Occurs when the NullText property value changes
        /// </summary>
		public event EventHandler NullTextChanged;
        /// <summary>
        /// Occurs when the Property Descriptor value changes
        /// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
		public event EventHandler PropertyDescriptorChanged;
        /// <summary>
        /// Occurs when the ReadOnly property value changes
        /// </summary>
		public event EventHandler ReadOnlyChanged;
        /// <summary>
        /// Occurs when the Width property value changes
        /// </summary>
		public event EventHandler WidthChanged;
        /// <summary>
        /// Occurs when the Cell start Editing
        /// </summary>
        public event EventHandler CellEdit;
        /// <summary>
        /// Occurs when the Active Cell KeyPress
        /// </summary>
        public event KeyEventHandler CellKeyPress;
        /// <summary>
        /// Occurs when the Active Cell Leave
        /// </summary>
        public event EventHandler CellLeave;
        /// <summary>
        /// Occurs when the Cell is Validating and befor is commited
        /// </summary>
        public event CellValidatingEventHandler CellValidating;
        /// <summary>
        /// Occurs after the Cell is Validated and changes commited
        /// </summary>
        public event EventHandler CellValidated;
        /// <summary>
        /// Raise the Cell start Editing
        /// </summary>
        internal protected virtual void OnCellEdit()
        {
            if (CellEdit != null)
            {
                CellEdit(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Raise the Cell Leave event
        /// </summary>
        internal protected virtual void OnCellLeave()
        {
            if (CellLeave != null)
            {
                CellLeave(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Raise the Cell KeyPress event
        /// </summary>
        /// <param name="keyData"></param>
        internal protected virtual void OnCellKeyPress(Keys keyData)
        {
            if(CellKeyPress!=null)
            {
                CellKeyPress(this, new KeyEventArgs(keyData));
            }
        }
        /// <summary>
        /// Raise the Cell Validating event
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal protected virtual bool OnCellValidating(int rowNum, object value)
        {
            if (CellValidating != null)
            {
                CellValidatingEventArgs evnt = new CellValidatingEventArgs(rowNum, this.MappingName, value);
                CellValidating(this, evnt);
                return !(evnt.Cancel);
            }
            return gridTableStyle.Grid.OnCellValidating(this, rowNum, value);
        }
        /// <summary>
        /// Raise the Cell Validating event
        /// </summary>
        internal protected virtual void OnCellValidated()
        {
            OnCellValidated(false);
        }
        /// <summary>
        /// Raise the Cell Validated event
        /// </summary>
        /// <param name="dirty"></param>
        internal protected virtual void OnCellValidated(bool dirty)
        {
            if (CellValidated != null)
            {
                CellValidated(this, EventArgs.Empty);
            }
            this.GridTableStyle.dataGrid.OnCellValidated(this);
            if (dirty)
            {
                this.GridTableStyle.dataGrid.OnDirty(Grid.GridDirtyRowState.Editing);
            }
        }
		#endregion
		
		#region Ctor
		static GridColumnStyle()
		{
//			GridColumnStyle.EventAlignment = new object();
//			GridColumnStyle.EventPropertyDescriptor = new object();
//			GridColumnStyle.EventFont = new object();
//			GridColumnStyle.EventHeaderText = new object();
//			GridColumnStyle.EventMappingName = new object();
//			GridColumnStyle.EventNullText = new object();
//			GridColumnStyle.EventReadOnly = new object();
//			GridColumnStyle.EventWidth = new object();
		}

 
        /// <summary>
        /// Initializes a new instance of the GridColumnStyle class
        /// </summary>
		public GridColumnStyle()
		{
            aggregateMode = AggregateMode.None;
			this.alignment = HorizontalAlignment.Left;
			this.propertyDescriptor = null;
			this.gridTableStyle = null;
			this.font = null;
			this.fontHeight = -1;
			this.mappingName = "";
			this.headerName = "";
			this.invalid = false;
            this.nullText = "";// "(null)";// "GridNullText";
			this.readOnly = false;
			this.updating = false;
			this.width = -1;
			this.isDefault = false;
            this.autoAdjust = false;
            this.isBound = true;/*bound*/
            this.m_Text = "";
            this.m_Enabled = true;
            this.editRow = -1;
			this.m_ColumnType=GridColumnType.TextColumn;
		    this.m_DataType=FieldType.Text; 
			this.m_Visible=true;
			this.m_AllowNull=true;
			this.isSelected=false;
			//this.headerAccessibleObject = null;
		}

        /// <summary>
        /// Initializes a new instance of the GridColumnStyle class
        /// </summary>
        /// <param name="prop"></param>
		public GridColumnStyle(PropertyDescriptor prop) : this()
		{
			this.PropertyDescriptor = prop;
			if (prop != null)
			{
				this.readOnly = prop.IsReadOnly;
			}
		}


        /// <summary>
        /// Initializes a new instance of the GridColumnStyle class
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
		internal GridColumnStyle(PropertyDescriptor prop, bool isDefault) : this(prop)
		{
			this.isDefault = isDefault;
			if (isDefault)
			{
				this.headerName = prop.Name;
				this.mappingName = prop.Name;
			}
		}
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                headerName = null;
                mappingName = null;
                m_Text = null;
                nullText = null;

                if (font != null)
                {
                    font.Dispose();
                    font = null;
                }
                if (HostControl != null)
                {
                    HostControl.Dispose();
                    HostControl = null;
                }
            }
            base.Dispose(disposing);
        }
		#endregion

		#region virtual
        /// <summary>
        /// Commits changes in the current cell ,When overridden in a derived class, initiates a request to complete an editing procedure. 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
		protected internal abstract bool Commit(BindManager dataSource, int rowNum);
        /// <summary>
        /// Notifies a column that it must relinquish the focus to the control it is hosting.
        /// </summary>
		protected internal virtual void ConcedeFocus()
		{
		}
        /// <summary>
        /// Overloaded. Prepares the cell for editing a value.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="bounds"></param>
        /// <param name="readOnly"></param>
        /// <param name="instantText"></param>
        /// <param name="cellIsVisible"></param>
		protected internal abstract void Edit(BindManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible);
        /// <summary>
        /// Enters a DBNull.Value into the column
        /// </summary>
		protected internal virtual void EnterNullValue()
		{
		}
        /// <summary>
        /// When overridden in a derived class, gets the minimum height of a row
        /// </summary>
        /// <returns></returns>
		protected internal abstract int GetMinimumHeight();
        /// <summary>
        /// When overridden in a derived class, gets the height used for automatically resizing columns.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
		protected internal abstract int GetPreferredHeight(Graphics g, object value);
        /// <summary>
        /// When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to GridTable using the GridColumnStyle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
		protected internal abstract Size GetPreferredSize(Graphics g, object value);
        /// <summary>
        /// When overridden in a derived class, initiates a request to interrupt an edit procedure.
        /// </summary>
        /// <param name="rowNum"></param>
		protected internal abstract void Abort(int rowNum);
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
		protected internal abstract void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum);
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="alignToRight"></param>
		protected internal abstract void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum, bool alignToRight);
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="backBrush"></param>
        /// <param name="foreBrush"></param>
        /// <param name="alignToRight"></param>
		protected internal virtual void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum, alignToRight);
		}

        /// <summary>
        /// Allows the column to free resources when the control it hosts is not needed. 
        /// </summary>
		protected internal virtual void ReleaseHostedControl()
		{
		}
        /// <summary>
        /// Get or Set the current Cell Text
        /// </summary>
        public abstract string Text { get;set;}

		#endregion

		#region Methods

 

        internal string GetCurrentText()
        {
            if (IsBound)
            {
                if (this.gridTableStyle != null)
                {
                    object o = this.GridTableStyle.Grid[this.MappingName];
                    return o == null ? "" : o.ToString();
                }
            }
            return "";
        }
        internal void SetCurrentText(object value)
        {
            if (IsBound)
            {
                if (this.gridTableStyle != null)
                {
                    this.GridTableStyle.Grid[this.MappingName] = value;
                }
            }
        }
 
        /// <summary>
        /// Suspends the painting of the column until the EndUpdate method is called
        /// </summary>
 		protected void BeginUpdate()
		{
			this.updating = true;
		}

 
        /// <summary>
        /// Throws an exception if the Grid does not have a valid data source, or if this column is not mapped to a valid property in the data source.
        /// </summary>
        /// <param name="value"></param>
		protected void CheckValidDataSource(BindManager value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", "GridColumnStyle.CheckValidDataSource(DataSource value), value == null");
			}
			if (this.isBound && this.PropertyDescriptor == null)
			{
				throw new InvalidOperationException("GridColumnUnbound "+ this.HeaderText );
			}
		}
        /// <summary>
        /// Informs the Grid that the user has begun editing the column. 
        /// </summary>
        /// <param name="editingControl"></param>
		protected internal virtual void ColumnStartedEditing(Control editingControl)
		{
			this.GridTableStyle.Grid.ColumnStartedEditing(editingControl);
		}


        //protected virtual AccessibleObject CreateHeaderAccessibleObject()
        //{
        //    return new GridColumnStyle.GridColumnHeaderAccessibleObject(this);
        //}
        /// <summary>
        /// Overloaded. Prepares the cell for editing a value.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="bounds"></param>
        /// <param name="readOnly"></param>
		protected internal virtual void Edit(BindManager source, int rowNum, Rectangle bounds, bool readOnly)
		{
			this.Edit(source, rowNum, bounds, readOnly, null, true);
		}
        /// <summary>
        /// Overloaded. Prepares the cell for editing a value.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="bounds"></param>
        /// <param name="readOnly"></param>
        /// <param name="instantText"></param>
		protected internal virtual void Edit(BindManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText)
		{
            this.Edit(source, rowNum, bounds, readOnly, instantText, true);
		}

        /// <summary>
        /// Resumes the painting of columns suspended by calling the BeginUpdate method.
        /// </summary>
		protected void EndUpdate()
		{
			this.updating = false;
			if (this.invalid)
			{
				this.invalid = false;
				this.Invalidate();
			}
		}
        /// <summary>
        /// Gets the value in the specified row from the specified BindManager
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
 		protected internal virtual object GetColumnValueAtRow(BindManager source, int rowNum)
		{
            if (!isBound)
            {
                //if (cellBoundCount > 0)
                //    return cellBoundList[rowNum];
                //else
                    return m_Text;
            }
			//this.CheckValidDataSource(source);
			if (this.PropertyDescriptor == null)
			{
                return null;
				//throw new InvalidOperationException("GridColumnNoPropertyDescriptor");
			}
			return this.PropertyDescriptor.GetValue(source[rowNum]);
		}

        internal virtual string GetValueText(object value)
        {
            if (value == null)
                return this.nullText;
            return value.ToString();
        }

		internal virtual string GetDisplayText(object value)
		{
			return value.ToString();
		}

        /// <summary>
        /// Redraws the grid and causes a paint message to be sent to the control
        /// </summary>
        /// <param name="gridFocus"></param>
        protected void InvalidateGrid(bool gridFocus)
        {
            Grid grid = null;
            if (this.GridTableStyle != null)
                grid = this.GridTableStyle.dataGrid;
            if (grid == null)
            {
                this.Invalidate();
                return;
            }
            if (gridFocus && grid.CanFocus)
            {
                grid.Focus();
            }
            grid.Invalidate();
        }

        /// <summary>
        /// Redraws the column and causes a paint message to be sent to the control
        /// </summary>
		protected virtual void Invalidate()
		{
			if (this.updating)
			{
				this.invalid = true;
			}
			else
			{
				GridTableStyle style1 = this.GridTableStyle;
				if (style1 != null)
				{
					style1.InvalidateColumn(this);
				}
			}
		}
 
		internal virtual bool KeyPress(int rowNum, Keys keyData)
		{
			if (this.ReadOnly || (((this.GridTableStyle != null) && (this.GridTableStyle.Grid != null)) && this.GridTableStyle.Grid.ReadOnly))
			{
				return false;
			}
			if ((keyData != (Keys.Control | Keys.NumPad0)) && (keyData != (Keys.Control | Keys.D0)))
			{
                OnCellKeyPress(keyData);
				return false;
			}
			this.EnterNullValue();
            return true;
		}

 
		internal virtual bool MouseDown(int rowNum, int x, int y)
		{
			return false;
		}

		internal virtual void MouseUp(int rowNum,MouseEventArgs e)// int x, int y)
		{
              //
        }

        internal virtual void MouseMove(int rowNum, int x, int y)
        {
            //
        }

		#region virtual event

		private void OnAlignmentChanged(EventArgs e)
		{
			if(this.AlignmentChanged!=null)
				this.AlignmentChanged(this,e);
//			EventHandler handler1 = base.Events[GridColumnStyle.EventAlignment] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

 
		private void OnFontChanged(EventArgs e)
		{
			if(this.FontChanged!=null)
				this.FontChanged(this,e);

//			EventHandler handler1 = base.Events[GridColumnStyle.EventFont] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

		private void OnHeaderTextChanged(EventArgs e)
		{
			if(this.HeaderTextChanged!=null)
				this.HeaderTextChanged(this,e);

//			EventHandler handler1 = base.Events[GridColumnStyle.EventHeaderText] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

		private void OnMappingNameChanged(EventArgs e)
		{
			if(this.MappingNameChanged!=null)
				this.MappingNameChanged(this,e);

//			EventHandler handler1 = base.Events[GridColumnStyle.EventMappingName] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

		private void OnNullTextChanged(EventArgs e)
		{
			if(this.NullTextChanged!=null)
				this.NullTextChanged(this,e);

//			EventHandler handler1 = base.Events[GridColumnStyle.EventNullText] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

 
		private void OnPropertyDescriptorChanged(EventArgs e)
		{
			if(this.PropertyDescriptorChanged!=null)
				this.PropertyDescriptorChanged(this,e);

//			EventHandler handler1 = base.Events[GridColumnStyle.EventPropertyDescriptor] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

 
		private void OnReadOnlyChanged(EventArgs e)
		{
			if(this.ReadOnlyChanged!=null)
				this.ReadOnlyChanged(this,e);

//			EventHandler handler1 = base.Events[GridColumnStyle.EventReadOnly] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

		private void OnWidthChanged(EventArgs e)
		{
			if(this.WidthChanged!=null)
				this.WidthChanged(this,e);

//			EventHandler handler1 = base.Events[GridColumnStyle.EventWidth] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

		#endregion

        /// <summary>
        /// Allows the column to free resources when the control it hosts is not needed.
        /// </summary>
        protected internal abstract void ResetHostControl();

		private void ResetFont()
		{
			if (this.font != null)
			{
				this.font = null;
				this.OnFontChanged(EventArgs.Empty);
				this.Invalidate();
			}
		}

 
        /// <summary>
        /// ResetHeaderText
        /// </summary>
		public void ResetHeaderText()
		{
			this.HeaderText = "";
		}
        /// <summary>
        /// Validating
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool Validating(int rowNum, object value)
        {
            if ((!IsValid(value)))
            {
                return false;
            }
            if (!OnCellValidating(rowNum, value))
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Sets the Grid for the column. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="value"></param>
        /// <param name="dirty"></param>
        protected internal virtual void SetColumnValueAtRow(BindManager source, int rowNum, object value, bool dirty)
        {
            if (Validating(rowNum, value))
            {
                SetColumnValueAtRow(source, rowNum, value);
                this.OnCellValidated(dirty);
            }
        }
        /// <summary>
        /// Sets the Grid for the column. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="value"></param>
		protected internal virtual void SetColumnValueAtRow(BindManager source, int rowNum, object value)
		{
            //if (!isBound)
            //{
            //    cellBoundList[rowNum] = value;
            //    return;
            //}
			this.CheckValidDataSource(source);
			if (source.Position != rowNum)
			{
				throw new ArgumentException("GridColumnListManagerPosition", "rowNum");
			}
			if (source[rowNum] is IEditableObject)
			{
				((IEditableObject) source[rowNum]).BeginEdit();
			}
            this.PropertyDescriptor.SetValue(source[rowNum], value);
         }

         internal virtual void SetColumnValue(BindManager source, int rowNum, object value)
        {
            this.CheckValidDataSource(source);
            if (source.Position != rowNum)
            {
                throw new ArgumentException("GridColumnListManagerPosition", "rowNum");
            }
            this.PropertyDescriptor.SetValue(source[rowNum], value);
        }
        /// <summary>
        /// Set the Grid parent of the control
        /// </summary>
        /// <param name="value"></param>
		protected virtual void SetGrid(Grid value)
		{
			this.SetGridInColumn(value);
		}

        /// <summary>
        /// Sets the Grid for the column. 
        /// </summary>
        /// <param name="value"></param>
		protected virtual void SetGridInColumn(Grid value)
		{
			if ((this.PropertyDescriptor == null) && (value != null))
			{
				BindManager manager1 = value.ListManager;
				if (manager1 != null)
				{
					PropertyDescriptorCollection collection1 = manager1.GetItemProperties();
					int num2 = collection1.Count;
					for (int num1 = 0; num1 < collection1.Count; num1++)
					{
						PropertyDescriptor descriptor1 = collection1[num1];
						if (!typeof(IList).IsAssignableFrom(descriptor1.PropertyType) && descriptor1.Name.Equals(this.HeaderText))
						{
							this.PropertyDescriptor = descriptor1;
							return;
						}
					}
				}
			}
		}

		internal void SetGridInternalInColumn(Grid value)
		{
			if ((value != null) && !value.Initializing)
			{
				this.SetGridInColumn(value);
			}
		}

 
		internal void SetGridTableInColumn(GridTableStyle value, bool force)
		{
			if (((this.gridTableStyle == null) || !this.gridTableStyle.Equals(value)) || force)
			{
				if (((value != null) && (value.Grid != null)) && !value.Grid.Initializing)
				{
					this.SetGridInColumn(value.Grid);
				}
				this.gridTableStyle = value;
			}
		}

		private bool ShouldSerializeFont()
		{
			return (this.font != null);
		}

		private bool ShouldSerializeHeaderText()
		{
			return (this.headerName.Length != 0);
		}

		private bool ShouldSerializeNullText()
		{
            return !"".Equals(this.nullText);
            //return !"(null)".Equals(this.nullText);
			//return !"GridNullText".Equals(this.nullText);
		}

        /// <summary>
        /// Informs the Grid that the user has begun editing the column. 
        /// </summary>
        /// <param name="editingControl"></param>
		void IGridColumnEditingNotifyService.ColumnStartedEditing(Control editingControl)
		{
			this.ColumnStartedEditing(editingControl);
		}

 
        /// <summary>
        /// Updates the value of a specified row with the given text.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="instantText"></param>
		protected internal virtual void UpdateUI(BindManager source, int rowNum, string instantText)
		{
		}
        /// <summary>
        /// Get if IsValid
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
		protected bool IsValid (object val)
		{
			bool isok = false;
			string value=val.ToString () ;

			if (this.ReadOnly) 
			{
				return true;
			}
			else if ((value.Length ==0)) 
			{
				if (!m_AllowNull) 
				{
					RM.ShowError(RM.NullArgument);
					return false;
				}
				//else if ((this.NullText != "")) 
				//{
				//	val = this.NullText;
				//}
			}
			switch (m_DataType) 
			{
				case FieldType.Number   :
                    isok = WinHelp.IsNumber(value);
					if(!isok)
						RM.ShowError(RM.ErrorConvertToNumber );
					break;
				case FieldType.Date:
					//isok =Regx.IsValidDate(this.m_FormatType,value);//  TypeUtils.IsDate(value);
					isok =WinHelp.IsDateTime(value);//  TypeUtils.IsDate(value);
					if(!isok)
						RM.ShowError(RM.ErrorConvertToDate);
					break;
				default:
					isok = true;
					break;
			}
			return isok;
		}
 
		internal object GetControlvalue()  
		{
			return this.GetColumnValueAtRow(this.GridTableStyle.dataGrid.ListManager, this.GridTableStyle.dataGrid.currentRow);
		}

		#endregion

		#region Properties
        /// <summary>
        /// Get the current cell bounds
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle Bounds
        {
            get { return this.cellBounds; }
        }
      
        /// <summary>
        /// Get Aggregate Mode
        /// </summary>
        [Browsable(false), DefaultValue(AggregateMode.None), EditorBrowsable(EditorBrowsableState.Never)]
        public AggregateMode AggregateMode
        {
            get { return aggregateMode; }
        }
        /// <summary>
        /// Get or Set indicating the column is enabled
        /// </summary>
        [Browsable(false), DefaultValue(true),EditorBrowsable(  EditorBrowsableState.Never),DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public virtual bool Enabled
        {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }

        [Browsable(false)]
        internal bool IsVisibleInternal/*bound*/
        {
            get
            {/*bound*/
                if (isDefault)
                    return this.propertyDescriptor != null && this.Visible && this.width > 0;
                if (!isMaped)
                    return false;
                if (!this.Visible || this.width == 0)
                    return false;
                if (IsBound && string.IsNullOrEmpty(mappingName))
                    return false;
                return true;
            }
        }

        [Browsable(false)]
        internal bool IsVisibleDesigner/*bound*/
        {
            get
            {/*bound*/
                if (!this.Visible || this.width == 0)
                    return false;
                if (IsBound && string.IsNullOrEmpty(mappingName))
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Get indicating the column is bound
        /// </summary>
        [DefaultValue(true)]
        public bool IsBound/*bound*/
        {
            get { return isBound; }
            //set { isBound = value; }
        }
        /// <summary>
        /// Get the Grid Column Type
        /// </summary>
        public GridColumnType ColumnType 
		{
			get{return m_ColumnType;}
		}
        /// <summary>
        /// Get the Grid Column DataType
        /// </summary>
		[Browsable(false)]
		public virtual FieldType DataType 
		{
			get	{return m_DataType;}
			//set{mDataType=value;} 
		}
        /// <summary>
        /// Get or Set indicating the column allow null value
        /// </summary>
		[Browsable(false),DefaultValue(true)]
		public virtual bool AllowNull 
		{
			get{return m_AllowNull;}
			set{m_AllowNull = value;}
		}
        /// <summary>
        /// Get or Set indicating whether the column is auto adjust
        /// </summary>
        [DefaultValue(false)]
        public virtual bool AutoAdjust
        {
            get { return autoAdjust; }
            set { autoAdjust = value;}
        }
        /// <summary>
        /// Gets or sets the alignment of text in a column.
        /// </summary>
		[Localizable(true), DefaultValue(0), Category("Display")]
		public virtual HorizontalAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!Enum.IsDefined(typeof(HorizontalAlignment), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(GridLineStyle));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					this.OnAlignmentChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}
		[Browsable(false)]
		internal virtual GridTableStyle GridTableStyle
		{
			get
			{
				return this.gridTableStyle;
			}
		}
 
        /// <summary>
        ///  Get the Font Height
        /// </summary>
		protected int FontHeight
		{
			get
			{
				if (this.fontHeight != -1)
				{
					return this.fontHeight;
				}
				if (this.GridTableStyle != null)
				{
					return this.GridTableStyle.Grid.FontHeight;
				}
				return GridTableStyle.defaultFontHeight;
			}
		}
 
        //[Browsable(false)]
        //public AccessibleObject HeaderAccessibleObject
        //{
        //    get
        //    {
        //        if (this.headerAccessibleObject == null)
        //        {
        //            this.headerAccessibleObject = this.CreateHeaderAccessibleObject();
        //        }
        //        return this.headerAccessibleObject;
        //    }
        //}
 
        /// <summary>
        /// Get or Set Column Header Text
        /// </summary>
		[Category("Display"), Localizable(true)]
		public virtual string HeaderText
		{
			get
			{
				return this.headerName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.headerName.Equals(value))
				{
					this.headerName = value;
					this.OnHeaderTextChanged(EventArgs.Empty);
					if (this.PropertyDescriptor != null)
					{
						this.Invalidate();
					}
				}
			}
		}
        /// <summary>
        /// Get or Set the column MappingName
        /// </summary>
		[Editor("System.Windows.Forms.Design.GridColumnMappingNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true)]
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
				if (!this.mappingName.Equals(value))
				{
					string text1 = this.mappingName;
					this.mappingName = value;
					try
					{
						if (this.gridTableStyle != null)
						{
							this.gridTableStyle.GridColumnStyles.CheckForMappingNameDuplicates(this);
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
        /// <summary>
        /// Get or Set Column Null Text Value
        /// </summary>
		[Category("Display"), Localizable(true)]
		public virtual string NullText
		{
			get
			{
				return this.nullText;
			}
			set
			{
				if ((this.nullText == null) || !this.nullText.Equals(value))
				{
					this.nullText = value;
					this.OnNullTextChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}
 
        /// <summary>
        /// Get or Set Column Property Descriptor
        /// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced), DefaultValue((string) null), Browsable(false)]
		public virtual PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propertyDescriptor;
			}
			set
			{
				if (this.propertyDescriptor != value)
				{
					this.propertyDescriptor = value;
					this.OnPropertyDescriptorChanged(EventArgs.Empty);
				}
			}
		}
 
        /// <summary>
        /// Get or Set indicating the column is Read only
        /// </summary>
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
 
		internal virtual bool WantArrows
		{
			get
			{
				return false;
			}
		}
 
        /// <summary>
        /// Get or Set indicating the column is visible
        /// </summary>
		[Localizable(true), DefaultValue(true), Category("Layout")]//,RefreshProperties(RefreshProperties.All)]
		public virtual bool Visible 
		{
			get{return m_Visible;}
			set
			{
				bool oldValue=m_Visible;
				if(m_Visible != value)
				{
					m_Visible = value;
					if(!m_Visible)
					{
						this.bckWidth=this.width;
						this.width=0;
					}
					else if(oldValue==false && value)
					{
						this.width=this.bckWidth;
					}
					this.Invalidate();
				}
			}
		}

        /// <summary>
        /// Get or Set the column width
        /// </summary>
		[Localizable(true), DefaultValue(100), Category("Layout")]
		public virtual int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (this.width != value && this.Visible)
				{
					this.width = value;
					Grid grid1 = (this.GridTableStyle == null) ? null : this.GridTableStyle.Grid;
					if (grid1 != null)
					{
						grid1.PerformLayout();
						grid1.InvalidateInside();
					}
					this.OnWidthChanged(EventArgs.Empty);
				}
			}
		}

        internal int WidthToLength()
        {
            int fsize = 8;
            if (this.font.Size > 0)
                fsize =(int) this.font.Size;
                return this.width / fsize;
           
        }

        /// <summary>
        /// Get the value if column is marked as read only from GridTable or itself
        /// </summary>
        /// <returns></returns>
		protected bool IsReadOnly()
		{
			bool flag1 = this.ReadOnly;
			if (this.GridTableStyle != null)
			{
				flag1 = flag1 || this.GridTableStyle.ReadOnly;
				if (this.GridTableStyle.dataGrid != null)
				{
					flag1 = flag1 || this.GridTableStyle.dataGrid.ReadOnly;
				}
			}
			return flag1;
		}

		#endregion

		#region Nested Types
        /// <summary>
        /// Nested CompModSwitches
        /// </summary>
		protected class CompModSwitches
		{
			/// <summary>
            /// CompModSwitches ctor
			/// </summary>
			public CompModSwitches()
			{
			}


			/// <summary>
            /// Get TraceSwitch
			/// </summary>
			public static TraceSwitch GridEditColumnEditing
			{
				get
				{
					if (GridColumnStyle.CompModSwitches.dgEditColumnEditing == null)
					{
						GridColumnStyle.CompModSwitches.dgEditColumnEditing = new TraceSwitch("DGEditColumnEditing", "Editing related tracing");
					}
					return GridColumnStyle.CompModSwitches.dgEditColumnEditing;
				}
			}
 

			// Fields
			private static TraceSwitch dgEditColumnEditing;
		}

        //[ComVisible(true)]
        //protected class GridColumnHeaderAccessibleObject : AccessibleObject
        //{
        //    // Methods
        //    public GridColumnHeaderAccessibleObject(GridColumnStyle owner)
        //    {
        //        this.owner = null;
        //        this.owner = owner;
        //    }

 
        //    public override AccessibleObject Navigate(AccessibleNavigation navdir)
        //    {
        //        switch (navdir)
        //        {
        //            case AccessibleNavigation.Up:
        //            case AccessibleNavigation.Left:
        //            case AccessibleNavigation.Previous:
        //                return this.Parent.GetChild((1 + this.Owner.gridTableStyle.GridColumnStyles.IndexOf(this.Owner)) - 1);

        //            case AccessibleNavigation.Down:
        //            case AccessibleNavigation.Right:
        //            case AccessibleNavigation.Next:
        //                return this.Parent.GetChild((1 + this.Owner.gridTableStyle.GridColumnStyles.IndexOf(this.Owner)) + 1);
        //        }
        //        return null;
        //    }

 
        //    public override Rectangle Bounds
        //    {
        //        get
        //        {
        //            if (this.owner.PropertyDescriptor == null)
        //            {
        //                return Rectangle.Empty;
        //            }
        //            Grid grid1 = this.Grid;
        //            if (grid1.GridRowsLength == 0)
        //            {
        //                return Rectangle.Empty;
        //            }
        //            GridColumnCollection collection1 = this.owner.gridTableStyle.GridColumnStyles;
        //            int num1 = -1;
        //            for (int num2 = 0; num2 < collection1.Count; num2++)
        //            {
        //                if (collection1[num2] == this.owner)
        //                {
        //                    num1 = num2;
        //                    break;
        //                }
        //            }
        //            Rectangle rectangle1 = grid1.GetCellBounds(0, num1);
        //            Rectangle rectangle2 = grid1.GetColumnHeadersRect();
        //            rectangle1.Y = rectangle2.Y;
        //            return grid1.RectangleToScreen(rectangle1);
        //        }
        //    }
        //    private Grid Grid
        //    {
        //        get
        //        {
        //            return this.owner.GridTableStyle.dataGrid;
        //        }
        //    }
        //    public override string Name
        //    {
        //        get
        //        {
        //            return this.Owner.headerName;
        //        }
        //    }
 
        //    protected GridColumnStyle Owner
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
        //            return AccessibleRole.ColumnHeader;
        //        }
        //    }

        //    // Properties
	
        //    // Fields
        //    private GridColumnStyle owner;
        //}
		#endregion
	}
 
}