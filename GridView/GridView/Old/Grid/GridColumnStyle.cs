using System;
using System.ComponentModel ;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection ;
using mControl.WinCtl.Controls;
using mControl.Util;

namespace mControl.GridStyle 
{

	#region Enum
	public enum ColumnTypes
	{
		None=0,
		TextColumn = 1,
		ComboColumn = 2,
		DateTimeColumn = 3,
		LabelColumn = 4,
		LinkColumn = 5,
		ButtonColumn = 6,
		ProgressColumn = 7,
		BoolColumn = 8,
		IconColumn = 9,
		MultiColumn = 10,
		EnumColumn = 11,
		MenuColumn = 12,
		GridColumn = 13,
	}

	#endregion

	[System.ComponentModel.ToolboxItem(false)]
	public abstract class GridColumnStyle : DataGridColumnStyle ,IGridColumnStyle
	{
	
		#region Memmbers

		protected RightToLeft			m_RightToLeft;  
		protected DataTypes				m_DataType; 
		protected bool					m_AllowNull;
		protected bool					m_Visible;

		internal ColumnTypes			m_ColumnType;

		protected bool					m_RowEdit;
		protected int					m_PrevRow;
		//protected bool					m_IsSum;
		protected AggregateMode			m_AggregateMode;

		protected string m_Format;
		protected IFormatProvider m_formatInfo;
		protected MethodInfo m_parseMethod;
		protected Win32.RECT m_rect;
		protected TypeConverter m_typeConverter;

		protected string oldValue;
		protected int editRow;
		protected int xMargin;
		protected int yMargin;
		protected Control hostedCtl;
		//protected GridTableStyle gridTableStyle;
		internal Grid grid;

		//private GridStyle.GridColumn m_GridDataColumn;
		public event CellValidatingEventHandler CellValidating;
		public event EventHandler CellValidated;

		internal protected virtual bool OnCellValidating(int rowNum,object value)
		{
			if(CellValidating!=null)
			{
				CellValidatingEventArgs   evnt = new CellValidatingEventArgs  (rowNum, this.MappingName ,value);
				CellValidating(this,evnt);
				return !(evnt.Cancel) ;
			}
			return true;
		}

		internal protected virtual void OnCellValidated()
		{
			if(CellValidated!=null)
			{
				CellValidated(this,EventArgs.Empty);
			}
			grid.OnDirty();
		}

		#endregion

		#region Constructor
		public GridColumnStyle() : base() 
		{
			InitManagerColumn();
		}

		internal GridColumnStyle(PropertyDescriptor prop) : base(prop)
		{
			InitManagerColumn();
			this.HeaderText = prop.Name;
			this.MappingName = prop.Name;
		}

		internal GridColumnStyle(PropertyDescriptor prop, bool isDefault) : base(prop)//,isDefault)
		{
			InitManagerColumn();
			//base.isDefault = isDefault;
			if (isDefault)
			{
				this.HeaderText = prop.Name;
				this.MappingName = prop.Name;
			}
		}

		private void InitManagerColumn()
		{
			this.xMargin =0;// 2;
			this.yMargin =0;// 1;
			this.m_Format = null;
			this.m_formatInfo = null;
			this.oldValue = null;
			this.editRow = -1;

			base.Alignment =System.Windows.Forms.HorizontalAlignment.Left ;    
			base.Width =80;
			m_Visible=true;
			m_RightToLeft=RightToLeft.Inherit;  
			m_DataType=DataTypes.Text ; 
			m_AllowNull = true;
			//m_IsSum=false;
			m_AggregateMode=AggregateMode.None;
			m_ColumnType   = ColumnTypes.None;
		}

		#endregion

		#region Internal Proprty

		[Browsable(false)]
		public virtual DataTypes DataType 
		{
			get	{return m_DataType;}
			//set{mDataType=value;} 
		}

		[Browsable(false)]
		public virtual AggregateMode SumMode 
		{
			get{return m_AggregateMode;}
//			set
//			{
//              m_AggregateMode=AggregateMode.None;
//			}
		}

		[Browsable(false)]
		internal virtual bool ShowSum 
		{
			get{return m_AggregateMode!=AggregateMode.None;}
		}

		[Browsable(false),DefaultValue(true)]
		public virtual bool AllowNull 
		{
			get{return m_AllowNull;}
			set{m_AllowNull = value;}
		}

		public  ColumnTypes ColumnType 
		{
			get{return m_ColumnType;}
		}

		[Browsable(false),DefaultValue(null)]
		public virtual Control HostedControl 
		{
			get{return hostedCtl;}
		}

		[DefaultValue(true)]//,RefreshProperties(RefreshProperties.All)]
		public virtual bool Visible 
		{
			get{return m_Visible;}
			set
			{
				m_Visible = value;
                this.Invalidate();
			}
		}


//		internal protected GridStyle.GridColumn GridColumn
//		{
//			get{return m_GridDataColumn;}
//			set{m_GridDataColumn=value;}
//	    }

		#endregion

		#region VirtualProperty

		[Browsable(false)]
		public virtual int MinimumWidth 
		{
			get { return 0;}//this.GetMinimumWidth();}//this.GetPreferredSize( null, null ).Width; }
		}

		[Browsable(false)]
		public virtual int MinimumHeight 
		{
			get { return GetMinimumHeight();}//mdHeader.MinRowHeight ;}//this.GetMinimumHeight(); }
		}

		#endregion

		#region DataGridColumnStyle methods

		internal void BeginEdit()
		{
          ((CtlGrid)this.DataGridTableStyle.DataGrid).BeginEdit () ;  
		}

		protected override object GetColumnValueAtRow(CurrencyManager source, int rowNum) 
		{ 
			
			object val=base.GetColumnValueAtRow (source,rowNum);
			if(val==null)
				val=this.NullText;
			return val;
		}

		#endregion 

		#region InternalMethods

		internal void SetDataType(DataTypes format) 
		{
			m_DataType=format;
		}

//		protected void SetRightToLeft(RightToLeft value)
//		{
//			if (value==RightToLeft.Yes)
//			{
//				this.TextAlignment =System.Windows.Forms.HorizontalAlignment.Right;
//			}
//			else if (value==RightToLeft.No)
//			{
//				this.TextAlignment  =System.Windows.Forms.HorizontalAlignment.Left ;
//			}
//			
//		}

		protected DateTime ParseToDate(object value) 
		{ 
			try
			{
				DateTime val = Convert.ToDateTime(value);
				return val;   
			}
			catch //(System.Exception Throw) 
			{
				throw new Exception("Error Invalid Date time format ");
			}

		}

		protected Decimal ParseToDecimal(object value) 
		{ 
			try
			{
				decimal val = Convert.ToDecimal (value);
				return val;   
			}
			catch //(System.Exception Throw) 
			{
				throw new Exception("Error : Invalid Number format ");
			}
		}

		protected string GetFormatText(string val) 
		{
			string res = String.Empty;
			try 
			{
				if (val != String.Empty ) 
				{
					if( m_Format != "")
					{
						switch (m_DataType) 
						{
							case DataTypes.Text :
								return string.Format(("{0:"+ (m_Format + "}")), val);
							case  DataTypes.Number  :
								if(Info.IsDouble(val))
								   return System.Convert.ToDecimal(val).ToString(m_Format);
								else
									return val;
							case  DataTypes.Date  :
								if(Info.IsDate(val))
									return  System.Convert.ToDateTime(val).ToString(m_Format);
								else
									return val;
							default:
								return string.Format(("{0:"+ (m_Format + "}")), val);
						}
					}
					else
						return val;
				}
				return res; 
			}
			catch  
			{
				try 
				{
					return val;
				}
				catch{return null;}
			}
			
		}

//		protected bool IsValid(object NewValue)
//		{
//			//string val =GetColumnValueAtRow(cm, rowNum).ToString ();
//			if(NewValue==null && !this.m_AllowNull )
//			{
//				NativeMethods.ErrMsg (mControl.Resources.CommonResexKeys.NullArgument);
//				return false;  
//			}
//			return true;
//		}
	
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
					NativeMethods.ErrMsg (RM.GetString(RM.NullArgument));
					return false;
				}
				//else if ((this.NullText != "")) 
				//{
				//	val = this.NullText;
				//}
			}
			switch (m_DataType) 
			{
				case DataTypes.Number   :
					isok = Regx.IsNumeric(value);
					if(!isok)
						NativeMethods.ErrMsg (RM.GetString(RM.ErrorConvertToNumber ));
					break;
				case DataTypes.Date:
					//isok =Regx.IsValidDate(this.m_FormatType,value);//  TypeUtils.IsDate(value);
					isok =Info.IsDateTime(value);//  TypeUtils.IsDate(value);
					if(!isok)
						NativeMethods.ErrMsg (RM.GetString(RM.ErrorConvertToDate));
					break;
				default:
					isok = true;
					break;
			}
			return isok;
		}



//		public CurrencyManager CM()
//		{
//			return ((CurrencyManager)
//				(this.DataGridTableStyle.DataGrid.BindingContext
//				[this.DataGridTableStyle.DataGrid.DataSource, this.DataGridTableStyle.DataGrid.DataMember]));
//		}
//
//		public System.Data.DataView GetDataView()  
//		{
//			try
//			{
//				return (( System.Data.DataView)(CM().List));
//			}
//			catch ( Exception ex)
//			{
//				throw ex;
//			}
//						 
//		}
//
		internal object GetControlvalue()  
		{
			//CM()
			return this.GetColumnValueAtRow(grid.ListManager, CurrentRow());
		}
//
//		public System.Type  GetColumnDataType(int index)  
//		{
//			try
//			{
//				return (( System.Data.DataView)(CM().List)).Table.Columns[index].DataType ;
//			}
//			catch ( Exception ex){
//				throw ex;
//			}
//		}
//
//		public System.Data.DataColumn[] GetPrimaryKey()  
//		{
//			try
//			{
//				return (( System.Data.DataView)(CM().List)).Table.PrimaryKey;
//			}
//			catch ( Exception ex)
//			{
//				throw ex;
//			}
//		}

//		public bool IsPrimaryKey(System.Data.DataColumn col)  
//		{
//			try
//			{
//				foreach(System.Data.DataColumn c in  GetPrimaryKey())
//				{
//                    if(c==col)
//						return true;
//				}
//				return false;
//			}
//			catch ( Exception ex)
//			{
//				throw ex;
//			}
//		}

		internal int CurrentRow()
		{
			return this.DataGridTableStyle.DataGrid.CurrentRowIndex ;
		}

		internal int CurrentCol() 
		{
			return this.DataGridTableStyle.DataGrid.CurrentCell.ColumnNumber;
		}

		internal DataGridColumnStyle CurrentColType( int col )  
		{
			return this.DataGridTableStyle.GridColumnStyles[col];
		}

		internal DataTypes CurrentColDataType( int col )  
		{
			return ((GridColumnStyle)this.DataGridTableStyle.GridColumnStyles[col]).m_DataType ;
		}

//		public int GetColIndex(string colName)
//		{
//			try
//			{
//				return GetDataView().Table.Columns[colName].Ordinal;
//			}
//			catch (Exception ex)
//			{throw ex;}
//		
//		}


	
		#endregion

		#region Inernal ColumnStyle

//		protected void BeginUpdate()
//		{
//			this.updating = true;
//		}
//
// 
//		protected void EndUpdate()
//		{
//			this.updating = false;
//			if (this.invalid)
//			{
//				this.invalid = false;
//				this.Invalidate();
//			}
//		}

//		protected void CheckValidDataSource(CurrencyManager value)
//		{
//			if (value == null)
//			{
//			 	throw new ArgumentNullException("value", "DataGridColumnStyle.CheckValidDataSource(DataSource value), value == null");
//			}
//			if (this.PropertyDescriptor == null)
//			{
//				throw new InvalidOperationException(SR.GetString("DataGridColumnUnbound", new object[] { this.HeaderText }));
//			}
//		}

 
//		protected internal virtual void ColumnStartedEditing(Control editingControl)
//		{
//		  this.DataGridTableStyle.DataGrid.ColumnStartedEditing(editingControl);
//		}


//		protected internal virtual object GetColumnValueAtRow(CurrencyManager source, int rowNum)
//		{
//			this.CheckValidDataSource(source);
//			if (this.PropertyDescriptor == null)
//			{
//			 	throw new InvalidOperationException(SR.GetString("DataGridColumnNoPropertyDescriptor"));
//			}
//			return this.PropertyDescriptor.GetValue(source[rowNum]);
//		}

 
//		internal virtual string GetDisplayText(object value)
//		{
//			 return value.ToString();
//		}

 
//		protected virtual void Invalidate()
//		{
//			if (this.updating)
//			{
//				this.invalid = true;
//			}
//			else
//			{
//				DataGridTableStyle style1 = this.DataGridTableStyle;
//				if (style1 != null)
//				{
//					style1.InvalidateColumn(this);
//				}
//			}
//		}

 
		internal virtual bool KeyPress(int rowNum, Keys keyData)
		{
			if (this.ReadOnly || (((this.DataGridTableStyle != null) && (this.DataGridTableStyle.DataGrid != null)) && this.DataGridTableStyle.DataGrid.ReadOnly))
			{
				return false;
			}
			if ((keyData != (Keys.Control | Keys.NumPad0)) && (keyData != (Keys.Control | Keys.D0)))
			{
				return false;
			}
			this.EnterNullValue();
			return true;
		}

 
		internal virtual bool MouseDown(int rowNum, int x, int y)
		{
			return false;
		}

 
//		protected internal virtual void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
//		{
//			this.CheckValidDataSource(source);
//			if (source.Position != rowNum)
//			{
//				throw new ArgumentException(SR.GetString("DataGridColumnListManagerPosition"), "rowNum");
//			}
//			if (source[rowNum] is IEditableObject)
//			{
//				((IEditableObject) source[rowNum]).BeginEdit();
//			}
//			this.PropertyDescriptor.SetValue(source[rowNum], value);
//		}

//		protected virtual void SetDataGrid(DataGrid value)
//		{
//			this.SetDataGridInColumn(value);
//		}
//
// 
//		protected virtual void SetDataGridInColumn(DataGrid value)
//		{
//			if ((this.PropertyDescriptor == null) && (value != null))
//			{
//				CurrencyManager manager1 = value.ListManager;
//				if (manager1 != null)
//				{
//					PropertyDescriptorCollection collection1 = manager1.GetItemProperties();
//					int num2 = collection1.Count;
//					for (int num1 = 0; num1 < collection1.Count; num1++)
//					{
//						PropertyDescriptor descriptor1 = collection1[num1];
//						if (!typeof(IList).IsAssignableFrom(descriptor1.PropertyType) && descriptor1.Name.Equals(this.HeaderText))
//						{
//							this.PropertyDescriptor = descriptor1;
//							return;
//						}
//					}
//				}
//			}
//		}

 
//		internal void SetDataGridInternalInColumn(DataGrid value)
//		{
//			if ((value != null) && !value.Initializing)
//			{
//				this.SetDataGridInColumn(value);
//			}
//		}

 
//		internal void SetDataGridTableInColumn(DataGridTableStyle value, bool force)
//		{
//			if (((this.dataGridTableStyle == null) || !this.dataGridTableStyle.Equals(value)) || force)
//			{
//				if (((value != null) && (value.DataGrid != null)) && !value.DataGrid.Initializing)
//				{
//				  this.SetDataGridInColumn(value.DataGrid);
//				}
//				this.dataGridTableStyle = value;
//			}
//		}

//		[Browsable(false)]
//		public virtual DataGridTableStyle DataGridTableStyle
//		{
//			get
//			{
//				return this.dataGridTableStyle;
//			}
//		}
//
//		[Editor("System.Windows.Forms.Design.DataGridColumnStyleMappingNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true)]
//		public string MappingName
//		{
//			get
//			{
//				return this.mappingName;
//			}
//			set
//			{
//				if (value == null)
//				{
//					value = "";
//				}
//				if (!this.mappingName.Equals(value))
//				{
//					string text1 = this.mappingName;
//					this.mappingName = value;
//					try
//					{
//						if (this.dataGridTableStyle != null)
//						{
//							this.dataGridTableStyle.GridColumnStyles.CheckForMappingNameDuplicates(this);
//						}
//					}
//					catch
//					{
//						this.mappingName = text1;
//						throw;
//					}
//					this.OnMappingNameChanged(EventArgs.Empty);
//				}
//			}
//		}
 
//		[Category("Display"), Localizable(true)]
//		public virtual string NullText
//		{
//			get
//			{
//				return this.nullText;
//			}
//			set
//			{
//				if ((this.nullText == null) || !this.nullText.Equals(value))
//				{
//					this.nullText = value;
//					this.OnNullTextChanged(EventArgs.Empty);
//					this.Invalidate();
//				}
//			}
//		}
 
//		[EditorBrowsable(EditorBrowsableState.Advanced), DefaultValue((string) null), Browsable(false)]
//		public virtual PropertyDescriptor PropertyDescriptor
//		{
//			get
//			{
//				return this.propertyDescriptor;
//			}
//			set
//			{
//				if (this.propertyDescriptor != value)
//				{
//					this.propertyDescriptor = value;
//					this.OnPropertyDescriptorChanged(EventArgs.Empty);
//				}
//			}
//		}
 
//		[DefaultValue(false)]
//		public virtual bool ReadOnly
//		{
//			get
//			{
//				return this.readOnly;
//			}
//			set
//			{
//				if (this.readOnly != value)
//				{
//					this.readOnly = value;
//					this.OnReadOnlyChanged(EventArgs.Empty);
//				}
//			}
//		}
//		internal virtual bool WantArrows
//		{
//			get
//			{
//				 return false;
//			}
//		}
//		[Localizable(true), DefaultValue(100), Category("Layout")]
//		public virtual int Width
//		{
//			get
//			{
//				return this.width;
//			}
//			set
//			{
//				if (this.width != value)
//				{
//					this.width = value;
//					DataGrid grid1 = (this.DataGridTableStyle == null) ? null : this.DataGridTableStyle.DataGrid;
//					if (grid1 != null)
//					{
//						grid1.PerformLayout();
//						grid1.InvalidateInside();
//					}
//					this.OnWidthChanged(EventArgs.Empty);
//				}
//			}
//		}


 


		#endregion

		#region abstract methods

		protected override void Abort(int rowNum) 
		{
			// no implementation 
		}

		protected override bool Commit(CurrencyManager dataSource, int rowNum) 
		{ 
			return true; 
		}

		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible) 
		{ 
			// no implementation 
		}


		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			 this.Paint(g, bounds, source, rowNum, false);
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			string text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			this.PaintText(g, bounds, text1, alignToRight);
		}
 
		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			string text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			this.PaintText(g, bounds, text1, backBrush, foreBrush, alignToRight);
		}

 
		protected virtual void PaintText(Graphics g, Rectangle bounds, string text, bool alignToRight)
		{
			//this.PaintText(g, bounds, text, (SolidBrush) SystemBrushes.Window, (SolidBrush) SystemBrushes.WindowText , alignToRight);
			this.PaintText(g, bounds, text, (SolidBrush)new SolidBrush (this.DataGridTableStyle.BackColor) , (SolidBrush)new SolidBrush (this.DataGridTableStyle.ForeColor), alignToRight);
			//this.PaintText(g, bounds, text, this.DataGridTableStyle.BackBrush, this.DataGridTableStyle.ForeBrush, alignToRight);
		}

		protected virtual void PaintText(Graphics g, Rectangle bounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			//
		}

		#endregion

		#region override

		protected override int GetMinimumHeight()
		{
			return ((base.FontHeight + this.yMargin) + 3);
		}
 
		protected override int GetPreferredHeight(Graphics g, object value)
		{
			int num1 = 0;
			int num2 = 0;
			string text1 = this.GetText(value);
			while ((num1 != -1) && (num1 < text1.Length))
			{
				num1 = text1.IndexOf("\r\n", num1 + 1);
				num2++;
			}
			return ((base.FontHeight * num2) + this.yMargin);
		}

 
		protected override Size GetPreferredSize(Graphics g, object value)
		{
			Size size1 = Size.Ceiling(g.MeasureString(this.GetText(value), this.DataGridTableStyle.DataGrid.Font));
			size1.Width += (this.xMargin * 2) + 1;//7this.DataGridTableStyle.GridLineWidth;
			size1.Height += this.yMargin;
			return size1;
		}

 
		protected string GetText(object value)
		{
			if (value is DBNull)
			{
				return this.NullText;
			}
			if (((this.m_Format != null) && (this.m_Format.Length != 0)) && (value is IFormattable))
			{
				try
				{
					return ((IFormattable) value).ToString(this.m_Format, this.m_formatInfo);
				}
				catch (Exception ex)
				{
					MessageBox.Show (ex.Message );
					goto Label_0084;
				}
			}
			if ((this.m_typeConverter != null) && this.m_typeConverter.CanConvertTo(typeof(string)))
			{
				return (string) this.m_typeConverter.ConvertTo(value, typeof(string));
			}
			Label_0084:
				if (value == null)
				{
					return "";
				}
			return value.ToString();
		}


		#endregion

		#region Virtual

		internal protected virtual void ReleaseHostedControlInternal()
		{
			base.ReleaseHostedControl();
		}

		internal protected virtual void SetDataGridInColumnInternal(DataGrid value,bool forces)
		{
			base.SetDataGridInColumn(value);
		}

		internal void SetGridInColumnInternal(Grid value)
		{
			this.grid=value;
		}

//		internal void SetDataGridTableInColumnInternal(DataGridTableStyle value, bool force)
//		{
//			if (((this.DataGridTableStyle == null) || !this.DataGridTableStyle.Equals(value)) || force)
//			{
//				if (((value != null) && (value.DataGrid != null)) && !value.DataGrid.IsHandleCreated)//.Initializing)
//				{
//					this.SetDataGridInColumn(value.DataGrid);
//				}
//				//base.DataGridTableStyle = value;
//			}
//		}
 

//		protected virtual void SetDataGridInColumn(DataGrid value)
//		{
//			if ((this.PropertyDescriptor == null) && (value != null))
//			{
//				CurrencyManager manager1 = value.ListManager;
//				if (manager1 != null)
//				{
//					PropertyDescriptorCollection collection1 = manager1.GetItemProperties();
//					int num2 = collection1.Count;
//					for (int num1 = 0; num1 < collection1.Count; num1++)
//					{
//						PropertyDescriptor descriptor1 = collection1[num1];
//						if (!typeof(IList).IsAssignableFrom(descriptor1.PropertyType) && descriptor1.Name.Equals(this.HeaderText))
//						{
//							this.PropertyDescriptor = descriptor1;
//							return;
//						}
//					}
//				}
//			}
//		}


		protected virtual Rectangle GetCellBounds( Rectangle cellBounds ) 
		{
			return new Rectangle( 
				cellBounds.X + xMargin , 
				cellBounds.Y + yMargin, 
				cellBounds.Width-(xMargin*2 ),
				cellBounds.Height-(yMargin*2 ) );
		}

		protected virtual void RunSum(object oldValue,object newValue)
		{
			if( m_DataType==DataTypes.Number)//m_IsSum &&
			{
				if(Regx.IsNumeric (oldValue.ToString ())&& Regx.IsNumeric (newValue.ToString ()) ) 
				{
					decimal decOld=System.Convert.ToDecimal (oldValue) ;
					decimal decNew=System.Convert.ToDecimal (newValue);
					//((CtlGrid)this.DataGridTableStyle.DataGrid).interanlGrid.SumPanel(this.CurrentCol(),decNew-decOld);    
					this.grid.SumPanel(this.CurrentCol(), m_AggregateMode,decOld,decNew);
				}
			}
		}

		protected virtual string GetDisplayText(object value)
		{
			return this.GetText(value);
		}

		protected virtual void DebugOut(string s)
		{
          
		}

  	  #endregion

		#region Properties

		[DefaultValue(""),Editor("System.Windows.Forms.Design.DataGridColumnStyleFormatEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		public virtual string Format
		{
			get
			{
				return this.m_Format;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if ((this.m_Format == null) || !this.m_Format.Equals(value))
				{
					this.m_Format = value;
					if (((this.m_Format.Length == 0) && (this.m_typeConverter != null)) && !this.m_typeConverter.CanConvertFrom(typeof(string)))
					{
						this.ReadOnly = true;
					}
					this.Invalidate();
				}
			}
		}

		[DefaultValue(null),Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.m_formatInfo;
			}
			set
			{
				if ((this.m_formatInfo == null) || !this.m_formatInfo.Equals(value))
				{
					this.m_formatInfo = value;
				}
			}
		}

		[DefaultValue((string) null), Description("FormatControlFormatDescr")]
		public override PropertyDescriptor PropertyDescriptor
		{
			set
			{
				base.PropertyDescriptor = value;
				if ((this.PropertyDescriptor != null) && (this.PropertyDescriptor.PropertyType != typeof(object)))
				{
					this.m_typeConverter = TypeDescriptor.GetConverter(this.PropertyDescriptor.PropertyType);
					this.m_parseMethod = this.PropertyDescriptor.PropertyType.GetMethod("Parse", new Type[] { typeof(string), typeof(IFormatProvider) });
				}
			}
		}
 
		[DefaultValue(false), Description("Read only mode")]
		public override bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				if ((value || !"".Equals(this.m_Format)) || ((this.m_typeConverter == null) || this.m_typeConverter.CanConvertFrom(typeof(string))))
				{
					base.ReadOnly = value;
				}
			}
		}
 
		#endregion

//		internal  Styles stylePlan;
//
//		[Category("Style")]
//		public  Styles StylePlan
//		{
//			get {return  stylePlan;}
//			//set{edit.StyleCtl.StylePlan =value;} 
//		}
//		public void SetStyleLayout(Styles value)
//		{
//			stylePlan=value;
////			if((this.hostedCtl!=null) && (this.hostedCtl is IStyleCtl))
////			{
////				((IStyleCtl)this.hostedCtl).SetStyleLayout(value);
////			}
//			
//		}

	}

}