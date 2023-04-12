using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

using Nistec.WinForms;
using System.Collections;
using Nistec.Win;

namespace Nistec.GridView  
{
    /// <summary>
    /// Displays the values of a data source or GridFields in a table where each row represents a key and value. This Grid allows you to select, sort, and edit these items.
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(VGrid), "Images.VGrid.bmp")]
    [Designer("Nistec.GridView.Design.VGridDesigner"), DefaultProperty("DataSource")]//, DefaultEvent("Navigate")]
    public class VGrid : Nistec.GridView.Grid    
	{
		#region Members 

        internal const string DefaultSourceName = "GridField";
        internal const string DefaultKeyName = "Key";
        internal const string DefaultValueName = "Value";
        internal const string DefaultFieldTypeName = "FieldType";
        //internal const string DefaultCaptionName = "Caption";

        private GridField[] _VGridSource;
        private DataTable _DataTable;

        private string _KeyHeader;
        private string _ValueHeader;
        private int _KeyColumnWidth;
        private string _KeyColumnName;
        private string _ValueColumnName;
        private string _FieldColumnName;
        Color _ColumnKeyBackColor;
        Color _ColumnKeyForeColor;

        private bool initilaizedColumns=false;
        private GridFieldCollection _fields;
        private bool initilized;
        /// <summary>
        /// Field Changed event
        /// </summary>
        public event FieldChangedEventHandler FieldChanged;

    	#endregion

		#region Constructors
        /// <summary>
        /// Initilaized VGrid
        /// </summary>
        public VGrid()
        {
            initilized = false;
            _KeyHeader="Key";
            _ValueHeader="Value";
            _KeyColumnWidth=50;
            _KeyColumnName="Key";
            _ValueColumnName="Value";
            _FieldColumnName="FieldType";
            _ColumnKeyBackColor= Color.White;
            _ColumnKeyForeColor=Color.Black;


            base.ControlLayout = ControlLayout.Visual;
            base.AllowColumnContextMenu = false;
            base.AllowGridContextMenu = false;
            base.AllowAdd = false;
            base.AutoAdjust = true;
            //CreateColumns();
        }
         
	
        /// <summary>
        /// UserControl overrides dispose to clean up the component list.
        /// </summary>
        /// <param name="disposing"></param>
		protected override void Dispose(bool disposing) 
		{
			if (disposing) 
			{
				//if (!(components == null)) 
				//{
				//	components.Dispose();
				//}
			}
			base.Dispose(disposing);
		}

        /// <summary>
        /// OnHandleCreated
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
            {
                if (this.DataSource == null && _fields != null && _fields.Count > 0)
                {
                    this.SetDataBinding(_fields, VGrid.DefaultFieldTypeName);
                }
            }
            base.AdjustColumns();
        }

        internal void OnFieldChanged(string field, object value)
        {
            if (FieldChanged != null)
                OnFieldChanged(new FieldChangedEventArgs(field, value));
        }
        /// <summary>
        /// Raise Field Changed event 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFieldChanged(FieldChangedEventArgs e)
        {
            if (FieldChanged != null)
                FieldChanged(this, e);
        }


		#endregion

		#region  Property
       
        //private void SetColumnsProperties()
        //{
        //    if (DesignMode)
        //        return;
        //    this.Columns[2].MappingName = ColumnFieldTypeName;
        //    this.ColumnKey.MappingName = ColumnKeyName;
        //    this.ColumnValue.MappingName = ColumnValueName;
        //    this.ColumnKey.HeaderText = ColumnKeyHeader;
        //    this.ColumnValue.HeaderText = ColumnValueHeader;
        //    this.ColumnKey.BackColor = ColumnKeyBackColor;
        //    this.ColumnKey.ForeColor = ColumnKeyForeColor;

        //    int colKeyWidth = ColumnKeyWidth;
        //    this.ColumnKey.Width = colKeyWidth;
        //    this.ColumnValue.Width = base.Width - colKeyWidth;
        //    AdjustVColumns();
        //    this.Invalidate();
        //    columnsPropertiesCreated = true;
        //}

        /// <summary>
        /// Get or Set Column Key Name
        /// </summary>
        [Category("VGrid"),DefaultValue("Key")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ColumnKeyName
        {
            get { return this._KeyColumnName; }
            set 
            {
                if (value == null || value == "")
                    return;
                if (this._KeyColumnName != value)
                {
                    this._KeyColumnName = value;
                    if(!DesignMode)
                    this.ColumnKey.MappingName = value;
                }
            }
        }
        /// <summary>
        /// Get or Set Column Value Name
        /// </summary>
        [Category("VGrid"), DefaultValue("Value")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ColumnValueName
        {
            get { return this._ValueColumnName; }
            set 
            {
                if (value == null || value == "")
                    return;
                if (this._ValueColumnName != value)
                {
                    this._ValueColumnName = value;
                    if (!DesignMode)
                        this.ColumnValue.MappingName = value;
                }
                
            }
        }
        /// <summary>
        /// Get or Set Column Field Type Name
        /// </summary>
        [Category("VGrid"), DefaultValue("FieldType")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ColumnFieldTypeName
        {
            get { return this._FieldColumnName; }
            set 
            {
                if (value == null || value == "")
                    return;
                if (this._FieldColumnName != value)
                {
                    this._FieldColumnName = value; 
                    if(!DesignMode)
                    this.Columns[2].MappingName = value;
                }
            }
        }
        /// <summary>
        /// Get or Set Column Key Header
        /// </summary>
        [Category("VGrid"), DefaultValue("Key")]
        public string ColumnKeyHeader
        {
            get { return this._KeyHeader; }
            set 
            {
                if (value == null || value == "")
                    return;
                if (this._KeyHeader != value)
                {
                    this._KeyHeader = value;
                    if (!DesignMode)
                        this.ColumnKey.HeaderText = value;
                }
            }
        }
        /// <summary>
        /// Get or set Column Value Header
        /// </summary>
        [Category("VGrid"), DefaultValue("Value")]
        public string ColumnValueHeader
        {
            get { return this._ValueHeader; }
            set 
            {
                if (value == null || value == "")
                    return;
                if (this._ValueHeader != value)
                {
                    this._ValueHeader = value;
                    if (!DesignMode)
                        this.ColumnValue.HeaderText = value;
                }
                
            }
        }
        /// <summary>
        /// Get or Set Column Key Width
        /// </summary>
        [Category("VGrid"), DefaultValue(50), Description("Key Column width in percent")]
        public int ColumnKeyWidth
        {
            get { return this._KeyColumnWidth; }
            set 
            {
                if (value <0 || value > 100)
                    value=50;
                if (this._KeyColumnWidth != value)
                {
                    this._KeyColumnWidth = value;
                    if (!DesignMode)
                    {
                        this.ColumnKey.Width = value;
                        this.ColumnValue.Width = base.Width - value;
                        AdjustVColumns();
                        this.Invalidate();
                    }
                }
            }
        }
        /// <summary>
        /// Get or Set Column Key BackColor
        /// </summary>
        [Category("VGrid"), DefaultValue(typeof(Color), "White")]
        public Color ColumnKeyBackColor
        {
            get
            {
                return _ColumnKeyBackColor;
                //if(!DesignMode)
                //    return this.ColumnKey.BackColor; 
                //return Color.White;
            }
            set 
            {
                _ColumnKeyBackColor = value;
                if (!DesignMode)
                    this.ColumnKey.BackColor = value; 
            }
        }
        /// <summary>
        /// Get or Set Column Key Fore Color
        /// </summary>
        [Category("VGrid"), DefaultValue(typeof(Color), "Black")]
        public Color ColumnKeyForeColor
        {
            get {
                return _ColumnKeyForeColor;
                //if (!DesignMode)
                //    return this.ColumnKey.ForeColor; 
                //return Color.Black; 
            }
            set
            {
                _ColumnKeyForeColor = value;
                if (!DesignMode)
                    this.ColumnKey.ForeColor = value;
            }
        }
        /// <summary>
        /// Get Vertical Source as array of GridField
        /// </summary>
        [Browsable(false), Category("VGrid")]
        public GridField[] VerticalSource 
		{
			get 
			{
                return _VGridSource;
			}
		}
        /// <summary>
        /// Get Column Key
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public GridLabelColumn ColumnKey
        {
            get 
            {
                if (!this.initilaizedColumns)
                {
                    CreateColumns();
                }
                return (GridLabelColumn)this.Columns[0]; 
            }
        }
        /// <summary>
        /// Get Column Value
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public GridMultiColumn ColumnValue
        {
            get 
            {
                if (!this.initilaizedColumns)
                {
                    CreateColumns();
                }

                return (GridMultiColumn)this.Columns[1]; 
            }
        }
        /// <summary>
        /// Get or set data source
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new object DataSource
        {
            get { return base.DataSource; }
            set 
            { 
               // base.DataSource = value; 
                SetDataBinding(value, DefaultSourceName);
            }
        }
        /// <summary>
        /// Get or Set data member
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string DataMember
        {
            get { return base.DataMember; }
            set { base.DataMember = value; }
        }
        /// <summary>
        /// Get or Set mapping name
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string MappingName
        {
            get { return base.MappingName; }
            set { base.MappingName = value; }
        }
        /// <summary>
        /// Get or Set Auto Adjust
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AutoAdjust
        {
            get { return base.AutoAdjust; }
            set { base.AutoAdjust = value; }
        }

        /// <summary>
        /// Get or Set Selection Type
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new SelectionType SelectionType
        {
            get { return base.SelectionType; }
            set { base.SelectionType = value; }
        }
        /// <summary>
        /// Get or Set Control Layout
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ControlLayout ControlLayout
        {
            get { return base.ControlLayout; }
            set { base.ControlLayout = value; }
        }
        /// <summary>
        /// Get or Set Preferred Column Width
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int PreferredColumnWidth
        {
            get { return base.PreferredColumnWidth; }
            set { base.PreferredColumnWidth = value; }
        }
        /// <summary>
        /// Get or set Preferred Row Height
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int PreferredRowHeight
        {
            get { return base.PreferredRowHeight; }
            set { base.PreferredRowHeight = value; }
        }
        /// <summary>
        /// Get or Set Allow Column ContextMenu
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AllowColumnContextMenu
        {
            get { return base.AllowColumnContextMenu; }
            set { base.AllowColumnContextMenu = value; }
        }
        /// <summary>
        /// Get or Set Allow Grid Context Menu
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AllowGridContextMenu
        {
            get { return base.AllowGridContextMenu; }
            set { base.AllowGridContextMenu = value; }
        }
        /// <summary>
        /// Get or Set Allow Navigation
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AllowNavigation
        {
            get { return base.AllowNavigation; }
            set { base.AllowNavigation = value; }
        }
        /// <summary>
        /// Get or set Allow Add
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AllowAdd
        {
            get { return base.AllowAdd; }
            set { base.AllowAdd = false; }

        }
        /// <summary>
        /// Get or Set Item
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Browsable(false)]
        public new GridField this[string key]
        {
            get 
            {
                return Fields[key];
                //return ItemCell(this.CurrentRowIndex, colName); 
            }
            set 
            {
                GridField field = Fields[key];

                if (field != null)
                {
                   field.Value = value;
                }
                //ItemCell(this.CurrentRowIndex, colName, value); 
            }
        }
        /// <summary>
        /// Get or Set item
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        [Browsable(false),EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new object this[int col]
        {
            get { return base[col]; }
            //set
            //{
            //    base[col] = value;
            //}
        }

 
        /// <summary>
        /// Get or Set item
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new object this[int row, string colName]
        {
            get { return base[row, colName]; }
            set { base[row, colName]= value; }
        }
        /// <summary>
        /// Get or Set item
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new object this[int rowIndex, int columnIndex]
        {
            get
            {
                return base[rowIndex, columnIndex];
            }
            set
            {
                base[rowIndex, columnIndex] = value;
            }
        }
        /// <summary>
        /// Get or Set item
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new object this[GridCell cell]
        {
            //[System.Security.Permissions.UIPermission( System.Security.Permissions.SecurityAction.Demand)]
            get
            {
                return base[cell];
            }
            //set
            //{
            //    base[cell] = value;
            //}
        }
		#endregion

        #region  override
        /// <summary>
        /// Get Grid ColumnCollection
        /// </summary>
        [Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
        public override GridColumnCollection Columns
        {
            get { return base.Columns; }
        }

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    if (this.initilaizedColumns)
        //    {
        //        AdjustVColumns();
        //        this.Invalidate();
        //    }
        //}

        /// <summary>
        /// End Init Internal
        /// </summary>
        protected override void EndInitInternal()
        {
            if (DesignMode || initilized)
                return;
            if (this.Fields.Count > 0)
            {
                this.SetDataBinding(this.Fields, DefaultSourceName);

                //_VGridSource = new GridField[this.Fields.Count];
                //this.Fields.CopyTo(_VGridSource);
                //_DataTable = VGridConverter.ConvertToVerticalSource(this.Fields, DefaultSourceName);
            }
        }

        /// <summary>
        /// PropertyItemChanged
        /// </summary>
        public event PropertyItemChangedEventHandler PropertyItemChanged;
        /// <summary>
        /// OnCellValidated
        /// </summary>
        /// <param name="sender"></param>
        internal override void OnCellValidated(GridColumnStyle sender)
        {
            base.OnCellValidated(sender);

            GridField field=GetCurrentField();

            OnPropertyItemChanged(new PropertyItemChangedEventArgs(field.Key, field.Value));

        }
        /// <summary>
        /// OnPropertyItemChanged
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPropertyItemChanged(PropertyItemChangedEventArgs e)
        {
            if (PropertyItemChanged != null)
                PropertyItemChanged(this, e);
        }

        #endregion

		#region Methods
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="mappingName"></param>
        public void Init(string mappingName)
        {
            SetDataBinding(this._fields, mappingName);
        }
  
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="source"></param>
        /// <param name="mappingName"></param>
        public void Init(GridField[] source, string mappingName)
        {
            this.SetDataBinding(source, mappingName);
        }
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <param name="mappingName"></param>
        /// <param name="columnsMappingName"></param>
        /// <param name="columnsHeaderNames"></param>
        public new void Init(object dataSource, string dataMember, string mappingName, string[] columnsMappingName, string[] columnsHeaderNames)
        {
            //CreateColumns(columnsMappingName, columnsHeaderNames);
            this.Init(dataSource, dataMember, mappingName);
        }
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <param name="mappingName"></param>
        public new void Init(object dataSource, string dataMember, string mappingName)
        {
            if (mappingName == null || mappingName == "")
            {
                throw new ArgumentException("Invalid MappingName");
            }
            Type type = dataSource.GetType();
            if (!(type == typeof(DataTable)))
            {
                throw new ArgumentException("DataSource not supported");
            }
            //((DataTable)dataSource ).TableName= mappingName;
            SetDataBinding((DataTable)dataSource, mappingName, DefaultKeyName, DefaultValueName);
        }
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceName"></param>
        /// <param name="keyColumnName"></param>
        /// <param name="valueColumnName"></param>
        public void SetDataBinding(object source, string sourceName, string keyColumnName, string valueColumnName)
        {
            ColumnKeyName = keyColumnName;
            ColumnValueName = valueColumnName;
            SetDataBinding(source, sourceName);
        }
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="dt"></param>
        public void SetDataBinding(DataTable dt)
        {
            SetDataBinding(dt, dt.TableName);
        }
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <param name="sourceName"></param>
        public void SetDataBinding(string[] keys, object[] values, string sourceName)
        {
            GridField[] source = VGridConverter.ConvertToGridField(keys, values);
            SetDataBinding(source, sourceName);
        }
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sourceName"></param>
        public void SetDataBinding(Hashtable list, string sourceName)
        {
            GridField[] source = VGridConverter.ConvertToGridField(list);
            SetDataBinding(source, sourceName);
        }

        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        public new void SetDataBinding(object source, string name)
        {
            Type type = source.GetType();
            if (type == typeof(DataTable))
            {
                //_DataTable = (DataTable)source;
                //_DataTable.TableName = name;
                if (_FieldColumnName != DefaultFieldTypeName)
                    _VGridSource = VGridConverter.ConvertToGridField((DataTable)source, this.ColumnKeyName, this.ColumnValueName, this.ColumnFieldTypeName);
                else
                    _VGridSource = VGridConverter.ConvertToGridField((DataTable)source, this.ColumnKeyName, this.ColumnValueName);//, this.ColumnFieldTypeName);
            }
            else if (type == typeof(DataView))
            {
                _VGridSource = VGridConverter.ConvertToGridField((DataView)source, this.ColumnKeyName, this.ColumnValueName);
            }
            else if (type == typeof(DataRow))
            {
                _VGridSource = VGridConverter.ConvertToGridField((DataRow)source);//, this.ColumnKeyName, this.ColumnValueName);
            }
            else if (type == typeof(DataRowView))
            {
                _VGridSource = VGridConverter.ConvertToGridField(((DataRowView)source).Row);//, this.ColumnKeyName, this.ColumnValueName);
            }
            else if (type == typeof(GridField[]))
            {
                _VGridSource = (GridField[])source;
            }
            else if (type == typeof(GridFieldCollection))
            {
                _VGridSource = new GridField[((GridFieldCollection)source).Count];
                ((GridFieldCollection)source).CopyTo(_VGridSource);
            }
            else
            {
                _VGridSource = VGridConverter.ConvertToGridField(source);
            }

            if (_VGridSource != null)
            {
                _DataTable = VGridConverter.ConvertToDataTable(_VGridSource, name);
            }

            if (_DataTable == null || _DataTable.Rows.Count == 0)
            {
                MsgBox.ShowWarning("InvalidDataSource");
                return;
            }
            VGridSourceToFildsCollection();
            CreateColumns();
            SetColumnsSource();
            initilized = true;
            base.Init(_DataTable, "", name);
            base.AdjustColumns();//.PerformAdjustColumns();
            //base.OnResizeAdjustColumns();
        }

        internal void SetColumnsSource()
        {
            if (DesignMode)
                return;

            if (ColumnKeyName == null || ColumnValueName == null || ColumnFieldTypeName == null)
            {
                throw new ArgumentException("Invalid KeyColumnName or  ValueColumnName or FieldColumnName ");
            }
            if (ColumnKeyName == "" || ColumnValueName == "" || ColumnFieldTypeName == "")
            {
                throw new ArgumentException("Invalid KeyColumnName or  ValueColumnName or FieldColumnName ");
            }
            //base.Columns.Clear();
            if (!this.initilaizedColumns)
            {
                CreateColumns();
            }

            this.Columns[0].MappingName = ColumnKeyName;
            this.Columns[0].HeaderText = ColumnKeyHeader;
            this.Columns[0].Width = Grid.DefaultColumnWidth;
  
            this.Columns[1].MappingName = ColumnValueName;
            this.Columns[1].HeaderText = ColumnValueHeader;
            this.Columns[1].Width = Grid.DefaultColumnWidth;
 
            this.Columns[2].MappingName = ColumnFieldTypeName;
        }

        internal void CreateColumns()
        {
            if (this.initilaizedColumns)
                return;
            this.Columns.Clear();

            GridLabelColumn gl = new GridLabelColumn();
            gl.MappingName = ColumnKeyName;
            gl.HeaderText = ColumnKeyHeader;
            gl.Width = Grid.DefaultColumnWidth;
            gl.DrawLabel = true;
            gl.AutoAdjust = true;
            this.Columns.Add(gl);

            GridMultiColumn gm = new GridMultiColumn();
            gm.MappingName = ColumnValueName;
            gm.HeaderText = ColumnValueHeader;
            gm.Width = Grid.DefaultColumnWidth;
            gm.AutoAdjust = true;
            //gm.CellValidated += new EventHandler(gm_CellValidated);
            this.Columns.Add(gm);

            GridEnumColumn ge = new GridEnumColumn();
            ge.MappingName = ColumnFieldTypeName;
            this.Columns.Add(ge);

            gm.enumCol = ge;
            gm.SetVgrid(this);

           
            //this.Columns[2].MappingName = ColumnFieldTypeName;
            //this.ColumnKey.MappingName = ColumnKeyName;
            //this.ColumnValue.MappingName = ColumnValueName;
            //this.ColumnKey.HeaderText = ColumnKeyHeader;
            //this.ColumnValue.HeaderText = ColumnValueHeader;
            
            //this.ColumnKey.BackColor = ColumnKeyBackColor;
            //this.ColumnKey.ForeColor = ColumnKeyForeColor;

            //int colKeyWidth = ColumnKeyWidth;
            //this.ColumnKey.Width = colKeyWidth;
            //this.ColumnValue.Width = base.Width - colKeyWidth;
            //AdjustVColumns();
            //this.Invalidate();
            ////columnsPropertiesCreated = true;

            this.initilaizedColumns = true;

        }

 
        //void gm_CellValidated(object sender, EventArgs e)
        //{
        //    string key = base[0].ToString();
        //    Fields[key].Value= base[1];
        //}

        private int CalcVRowsHeight()
        {
            int rows = 0;
            if (this._DataTable != null)
            {
                rows = this._DataTable.Rows.Count;
            }
            int rowHeight = PreferredRowHeight;
            int rowsAdd = this.AllowAdd ? 1 : 0;
            int headerAdd = ColumnHeadersVisible ? this.layout.ColumnHeaders.Height : 0;//.DefaultColumnHeaderHeight:0 ;
            return ((rowsAdd + rows) * rowHeight) + headerAdd;
        }
        /// <summary>
        /// Adjust VColumns
        /// </summary>
        internal protected void AdjustVColumns()
        {
            try
            {
                int scrallWidth = 0;
                int prc = this.ColumnKeyWidth;

                if (!this.DesignMode && this._DataTable != null)
                {
                    int gridHeight = HeightInternal;
                    int calcRowsHeight = CalcVRowsHeight() - 3;
                    if (calcRowsHeight > gridHeight)
                    {
                        scrallWidth = this.VertScrollBar.Width;
                    }
                }
                int gridWidth = this.Width -5- scrallWidth;
                this.ColumnKey.width = gridWidth * prc / 100;
                this.ColumnValue.width = gridWidth - this.ColumnKey.width;
            }
            catch//(Exception ex)
            {
                MessageBox.Show("Adjust VGrid Columns Failed");//ex.Message);
            }
        }
        /// <summary>
        /// Get Value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetValue(string key)
        {
            if (base.DataList.Sort == "")
            {
                base.DataList.Sort = this.ColumnKeyName;
            }
            int indx = base.DataList.Find(key);
            if (indx > -1)
            {
                return base.DataList[indx][ColumnValueName];
            }
            return null;
        }
        /// <summary>
        /// Get Field
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GridField GetField(string key)
        {
            foreach (GridField f in _VGridSource)
            {
                if (f.Key.Equals(key))
                {
                    return f;
                }
            }
            return null;
        }
        /// <summary>
        /// Get Field Type
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FieldType GetFieldType(string key)
        {
            GridField f =GetField(key);
                if (f!=null)
                {
                    return f.FieldType;
                }

                return FieldType.Text;
        }
		#endregion


  


        /// <summary>
        /// Get VGrid Field Collection
        /// </summary>
        [Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        public GridFieldCollection Fields
        {
            get
            {
                if (_fields == null)
                {
                    _fields = new GridFieldCollection(this);
                }
                 return _fields;
            }
        }

        private void VGridSourceToFildsCollection()
        {
            Fields.Clear();
            Fields.AddRange(_VGridSource);
        }
        /// <summary>
        /// Get Current Field
        /// </summary>
        /// <returns></returns>
        public GridField GetCurrentField()
        {

            object key = base[this.ColumnKeyName];
            if (key == null)
                return null;
            return Fields[key.ToString()];

        }
        /// <summary>
        /// Set Column Styles To Fields
        /// </summary>
        /// <param name="cols"></param>
        public void SetColumnStylesToFields(GridColumnCollection cols)
        {
            foreach (GridColumnStyle col in cols)
            {
                if(col.IsBound )
                {
                   SetColumnStyleToField(col);
                }
            }
        }

        private void SetColumnStyleToField(GridColumnStyle col)
        {

            switch (col.ColumnType)
            {
                case  GridColumnType.ComboColumn:
                    GridField field = GetField(col.MappingName);
                    if (field != null)
                    {
                        SetComboField(field, col);
                    }
                   break;
            }

        }

        private void SetComboField(GridField field,GridColumnStyle col)
        {
            field.MultiType = Nistec.WinForms.MultiType.Combo;
            if(((GridComboColumn) col).DataSource !=null)
            {
                    
            }
            else //if(((GridComboColumn) col).Items)
            {
                field.Items.AddRange(((GridComboColumn)col).Items);
            }
         }

 

        //public void SetFieldsKey(string[] keys)
        //{
        //    if (keys.Length != this.RowCount)
        //    {
        //        RM.ShowWarning("keys length not equal to grid rows count");
        //    }

        //    for (int i = 0; i < keys.Length; i++)
        //    {
        //        _DataTable.Rows[i][0] = keys[i];
        //    }
        //    //this.DataSource = _DataTable;
        //}


    }
}

