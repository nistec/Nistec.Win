using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing; 
using System.Collections;
using System.Data; 

using mControl.Util;
using mControl.WinCtl.Controls;
using mControl.GridStyle;
using mControl.GridStyle.Columns; 

using System.Diagnostics;
      
namespace mControl.GridStyle 
{
  
	[System.ComponentModel.Designer (typeof(GridDesigner))]
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(System.Windows.Forms.DataGrid ))]
	public class Grid : System.Windows.Forms.UserControl,IEditableObject,ISupportInitialize,IGrid,IStyleCtl
	{

		#region NetReflectedFram
		internal bool m_netFram=false;

		public void NetReflectedFram(string pk)
		{
			try
			{
				// this is done because this method can be called explicitly from code.
				System.Reflection.MethodBase method = (System.Reflection.MethodBase) (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
				m_netFram=mControl.Util.Net.NetFramReg.NetReflected(method,pk);
			}
			catch{}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);

			if(m_TableStyle !=null)
			{
				m_TableStyle.DataGrid =(DataGrid)this.mGrid;
				//this.mGrid.SetDataGridInColumnInternal();
			}
			OnStylePainterChanged(EventArgs.Empty);

			m_Caption.OnMenuHandel();

			if(!DesignMode && !m_netFram)
			{
				mControl.Util.Net.netGrid.NetFram(this.Name);
			}
		}

		#endregion

		#region Static Members

		//Defaults
		public const int DefaultScrollWidth=20;
		public const int DefaultColumnWidth=80;
		public const int DefaultRowHeight=16;
		public const int DefaultCaptionHeight=20;//19;
		public const int DefaultRowHeaderWidth=35;
		public const int DefaultColumnHeaderHeight=19;//20;
		public const int DefaultGridHeight=20;
		public const int DefaultGridWidth=20;

		//MinMax
		public const int MaxGridWidth=1280;
		public const int MinRowHeight=12;

		public static HorizontalAlignment Alignment = HorizontalAlignment.Left;
		public static System.Drawing.Color ColumnsForeColor=System.Drawing.Color.Black ;
		public static System.Drawing.Color ColumnsBackColor=System.Drawing.Color.White ;
	
    
		public static double DecMinValue=-9999999999;
		public static double DecMaxValue= 9999999999;
		public static System.DateTime  DateMinValue= new System.DateTime(1900,1,1);
		public static System.DateTime  DateMaxValue=new System.DateTime(2999,12,31);

		//public static int MinColWidth=8;
		//public static int MinRowHeight=6;
		public static System.Drawing.Size DefaultControlSize=new System.Drawing.Size(80,12);		

		#endregion
        
		#region Members

		private System.ComponentModel.IContainer components;
		private object										m_DataSource;
		private string										m_MappingName;
		private bool										colsCreated;
		private bool										tblsCreated;
		private bool										sourceCreated;
		private bool										sourceChanged;
		private int											gridColumnsCount;
		private bool										dirty;
		private bool										colsAutoCreated;
		private bool										captionVisible;


		private bool allowAddNew=true;
		//private bool allowEdit=true;
		private bool allowDelete=true;

		protected mControl.GridStyle.Controls.GridStatusBar m_StatusBar;
		protected System.Drawing.Color						m_BorderColor;
		protected System.Drawing.Color						m_BorderHotColor;
		protected bool										m_AutoAdjust;
		protected DataGridTableStyle						m_TableStyle ;
		protected System.Windows.Forms.BorderStyle			m_BorderStyle;
		protected int										m_MaxWidth;
		private GridColumnsCollection cols;
		private GridColumnStyle[] gridColumns;
		private mControl.GridStyle.CtlGrid mGrid;
		private mControl.GridStyle.Controls.GridCaption m_Caption;

		public event  EventHandler ColumnResize;

		[Description("DataGridOnBorderStyleChangedDescr"), Category("PropertyChanged")]
		public event EventHandler BorderStyleChanged;
		[Category("PropertyChanged"), Description("DataGridOnCurrentCellChangedDescr")]
		public event EventHandler CurrentCellChanged;
		[Category("PropertyChanged"), Description("DataGridOnReadOnlyChangedDescr")]
		public event EventHandler ReadOnlyChanged;
		[Description("DataGridNavigateEventDescr"), Category("Action")]
		public event NavigateEventHandler Navigate;
		[Description("DataGridOnDataSourceChangedDescr"), Category("PropertyChanged")]
		public event EventHandler DataSourceChanged;
		[Category("PropertyChanged"), Description("DataGridOnFlatModeChangedDescr")]
		public event EventHandler FlatModeChanged;
		[Category("PropertyChanged"), Description("DataSetChanged")]
		public event EventHandler DataSelectedChanged;
		[Category("PropertyChanged"), Description("DataChanged")]
		public event EventHandler DirtyChanged;

		//		[Category("CellAction"), Description("Cell Validation event")]
		//		public event CellValidatingEventHandler CellValidating;
		//		[Category("CellAction"), Description("Cell Validated event")]
		//		public event EventHandler CellValidated;
		//		[Category("CellAction"), Description("Cell Click event")]
		//		public event CellClickEventHandler CellClicked;
		[Category("CellAction"), Description("Button Click event")]
		public event CellClickEventHandler ButtonClicked;


		internal virtual void OnButtonClicked(object sender, DataGridTableStyle tbl, int rowNum, int columnNum)
		{
			if(ButtonClicked!=null)
				ButtonClicked(sender,new CellClickEventArgs (tbl, rowNum, columnNum)); 
		}

		//		internal virtual void OnCellValidating(object sender, int rowNum, string colName, object value)
		//		{
		//			if(CellValidating!=null)
		//			{
		//				CellValidatingEventArgs   evnt = new CellValidatingEventArgs  (rowNum, colName ,value);
		//				CellValidating(this,evnt);
		//				return !(evnt.Cancel) ;
		//			}
		//			return true;
		//
		//			//if(CellValidating!=null)
		//			//	CellValidating(sender,new CellValidatingEventArgs (rowNum, colName,value)); 
		//		}
		//
		//		internal virtual void OnCellValidated(object sender)
		//		{
		//			if(CellValidated!=null)
		//				CellValidated(sender,EventArgs.Empty); 
		//		}

		#endregion

		#region Constructor

//		static Grid()
//		{
//			Grid.layout=new GridStyleLayout();// mControl.WinCtl.Controls.StyleControl.Layout;
//		}

		public Grid() 
		{
			gridLayout=new GridStyleLayout();
			//m_GridTablesMode=GridTablesMode.Single;
			m_BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			m_MaxWidth=Grid.MaxGridWidth;
			m_BorderColor=System.Drawing.Color.DarkGray ;
			captionVisible=true;
			m_AutoAdjust=false;
			//isCreated=false;
			colsCreated=false;
			tblsCreated=false;
			sourceCreated=false;
			sourceChanged=false;
			colsAutoCreated=false;
			dirty=false;
			this.tblSelected=-1;
			tblCount=0;
			//-createMenu=false;
	        m_MappingName="";
			this.allowMultiTables=false;
 
			InitializeComponent();
			this.cols=new GridStyle.GridColumnsCollection (this);
			this.cols.CollectionChanged+=new CollectionChangeEventHandler(cols_CollectionChanged);
			this.m_Caption.owner = this;


			this.mGrid.interanlGrid =this;
			this.m_TableStyle.DataGrid=this.mGrid;
//			if(netFramGrid.NetFram())
//			{
//				netFramwork.NetReflectedFram();
//			}
			//InitMenu();
			//SetPopUpStatus();
			//tbls.Cleared += new mControl.Collections.CollectionClear(tbls_Cleared);
			//tbls.Inserted +=new mControl.Collections.CollectionChange(tbls_Inserted);
			//tbls.Removed +=new mControl.Collections.CollectionChange(tbls_Removed);

		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed (e);
			if(PopUpMenu!=null)
			{
				PopUpMenu.SelectedValueChanged-=new EventHandler(PopUpMenu_SelectedValueChanged); 
			}
		}


		// UserControl overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing) 
		{
			if (disposing) 
			{
				if (!(components == null)) 
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
        
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent() 
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Grid));
			this.m_TableStyle = new System.Windows.Forms.DataGridTableStyle();
			this.m_StatusBar = new mControl.GridStyle.Controls.GridStatusBar(this);
			this.m_Caption = new mControl.GridStyle.Controls.GridCaption();
			this.mGrid = new mControl.GridStyle.CtlGrid();
			//this.GridImageList = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.mGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// m_TableStyle
			// 
			this.m_TableStyle.DataGrid = null;
			this.m_TableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.m_TableStyle.MappingName = "";
			// 
			// m_StatusBar
			// 
			this.m_StatusBar.Location = new System.Drawing.Point(1, 139);
			this.m_StatusBar.Name = "m_StatusBar";
			this.m_StatusBar.Size = new System.Drawing.Size(190, 20);
			this.m_StatusBar.SizingGrip = false;
			this.m_StatusBar.TabIndex = 3;
			this.m_StatusBar.Visible = false;
			// 
			// m_Caption
			// 
			this.m_Caption.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.m_Caption.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
			this.m_Caption.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_Caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.m_Caption.LeftMargin = 0;
			this.m_Caption.Location = new System.Drawing.Point(1, 1);
			this.m_Caption.Name = "m_Caption";
			this.m_Caption.Size = new System.Drawing.Size(214, 20);
			this.m_Caption.TabIndex = 0;
			this.m_Caption.TabStop = false;
			this.m_Caption.TopMargin = 0;
			// 
			// mGrid
			// 
			this.mGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.mGrid.CaptionVisible = false;
			this.mGrid.DataMember = "";
			this.mGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.mGrid.Location = new System.Drawing.Point(1, 21);
			this.mGrid.Name = "mGrid";
			this.mGrid.ParentRowsVisible = false;
			this.mGrid.SelectionType = mControl.GridStyle.SelectionTypes.Cell;
			this.mGrid.Size = new System.Drawing.Size(214, 154);
			this.mGrid.TabIndex = 4;
			this.mGrid.ReadOnlyChanged += new System.EventHandler(this.mGrid_ReadOnlyChanged);
			this.mGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mGrid_KeyDown);
			this.mGrid.FlatModeChanged += new System.EventHandler(this.mGrid_FlatModeChanged);
			this.mGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mGrid_MouseDown);
			this.mGrid.ColumnResize += new mControl.GridStyle.ColumnResizeHandler(this.mGrid_ColumnResize);
			this.mGrid.SizeChanged += new System.EventHandler(this.mGrid_SizeChanged);
			this.mGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mGrid_KeyPress);
			this.mGrid.DataSourceChanged += new System.EventHandler(this.mGrid_DataSourceChanged);
			this.mGrid.BorderStyleChanged += new System.EventHandler(this.mGrid_BorderStyleChanged);
			this.mGrid.Navigate += new System.Windows.Forms.NavigateEventHandler(this.mGrid_Navigate);
			this.mGrid.DoubleClick += new System.EventHandler(this.mGrid_DoubleClick);
			this.mGrid.MouseEnter += new System.EventHandler(this.mGrid_MouseEnter);
			this.mGrid.Click += new System.EventHandler(this.mGrid_Click);
			this.mGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mGrid_MouseUp);
			this.mGrid.Leave += new System.EventHandler(this.mGrid_Leave);
			this.mGrid.MouseLeave += new System.EventHandler(this.mGrid_MouseLeave);
			this.mGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mGrid_KeyUp);
			this.mGrid.CurrentCellChanged += new System.EventHandler(this.mGrid_CurrentCellChanged);
			this.mGrid.Enter += new System.EventHandler(this.mGrid_Enter);
			// 
			// GridImageList
			// 
//			this.GridImageList.ImageSize = new System.Drawing.Size(16, 16);
//			this.GridImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GridImageList.ImageStream")));
//			this.GridImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// Grid
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.mGrid);
			this.Controls.Add(this.m_Caption);
			this.Controls.Add(this.m_StatusBar);
			this.DockPadding.All = 1;
			this.Name = "Grid";
			this.Size = new System.Drawing.Size(216, 176);
			((System.ComponentModel.ISupportInitialize)(this.mGrid)).EndInit();
			this.ResumeLayout(false);

		}
        

		#endregion

		#region Fileds

		private System.Collections.Hashtable ActiveFields;
		private System.Collections.Hashtable Fields;
		private System.Collections.Hashtable FieldsList;
	    private bool allowChangeColumnMapping;
		private mControl.WinCtl.Controls.CtlPopUp PopUpMenu;
		//optional private CtlContextMenu FieldsMenu;


		public bool AllowChangeColumnMapping
		{
			get{return this.allowChangeColumnMapping;}
			set
			{
               this.allowChangeColumnMapping=value;
			}
		}

		internal void CreateActiveFields()
		{
			if(cols.Count==0 )
				return;
			int cnt=Columns.Count;
			if(ActiveFields!=null)
				ActiveFields.Clear();
			else
				ActiveFields=new Hashtable();

			string colName="";
			string colCaption="";
 
			for (int i=0;i<cnt;i++)
			{
				colName=Columns[i].MappingName;
				if(colName!="")
				{
					colCaption=Columns[i].HeaderText;
					if(!ActiveFields.ContainsKey(colName))
						ActiveFields.Add(colName,colCaption);
				}
			}
		}

		internal void CreateFields()
		{
          CreateFields(this.DataList.Table);
		}

		internal void CreateFields(System.Data.DataTable dt)
		{
			if(dt.Columns.Count==0 )
				return;
			int cnt=dt.Columns.Count;
			if(Fields!=null)
				Fields.Clear();
			else
				Fields=new Hashtable();

			if(FieldsList!=null)
				FieldsList.Clear();
			else
				FieldsList=new Hashtable();

			string colName="";
			string colCaption="";
 
			for (int i=0;i<cnt;i++)
			{
				colName=dt.Columns[i].ColumnName;
				if(colName!="")
				{
					colCaption=dt.Columns[i].Caption;
					Fields.Add(colName,colCaption);
					if(!ActiveFields.ContainsKey(colName))
					{
						FieldsList.Add(colName,colCaption);
					}
				}

			}
		}

		internal bool CreateFieldsMenu()
		{
			if(FieldsList==null)
				return false;
			if(FieldsList.Count==0 )
				return false;

            //optional
			//if(FieldsMenu!=null)
			//	FieldsMenu.MenuItems.Clear();
            //else
			//    FieldsMenu=new CtlContextMenu();

			if(PopUpMenu!=null)
			{
				PopUpMenu.MenuItems.Clear();
			}
			else
			{
				PopUpMenu=new  CtlPopUp(this);
				PopUpMenu.SelectedValueChanged+=new EventHandler(PopUpMenu_SelectedValueChanged); 
			}

            //PopUpMenu.BackColor=this.HeaderBackColor; 
			//PopUpMenu.ImageList=this.GridImageList; 

			int cnt=FieldsList.Count;
	
			string colName="";
			string colCaption="";
			int colIndex=mGrid.ActiveColumnIndex;
            System.Data.DataTable dt=DataList.Table; 
			IDictionaryEnumerator list = FieldsList.GetEnumerator();
			
			while (list.MoveNext() )
			{
                colName=list.Key.ToString();
				if((colName!="") && IsColumnTypeOK(Columns[colIndex],dt.Columns[colName].DataType))
				{
					colCaption=list.Value.ToString();
					PopUpMenu.MenuItems.AddItem(colCaption,colName,-1);
					//optional
					//MenuItem itm= FieldsMenu.AddItem(colCaption,colName,-1,new EventHandler(mnColumn_Click));  
					//FieldsMenu.SetDraw(itm,true);
					//itm.Visible=true;
				}
			}
			return PopUpMenu.MenuItems.Count>0;// FieldsMenu.ItemsCount>0;
		}

		private void PopUpMenu_SelectedValueChanged(object sender, EventArgs e)
		{
			CtlPopUp itm= (CtlPopUp)sender;
			if(itm!=null)
			{
				string member= itm.SelectedItem.Text;
				int colIndex=mGrid.ActiveColumnIndex;
				string oldName=Columns[colIndex].MappingName;
				if(!oldName.Equals(member))
					ChangeColumn(colIndex,oldName,member);
			}
		}

		internal void ChangeColumn(string colName, string oldCol,string newCol)
		{
           int indx=cols.IndexOf(cols[colName]); 
			ChangeColumn(indx,oldCol,newCol);
		}

		internal void ChangeColumn(int indx, string oldCol,string newCol)
		{
			GridColumnStyle c=cols[indx];
			c.MappingName=newCol;
			object newColName=Fields[newCol];
			object OldColName=Fields[oldCol];
			c.HeaderText=newColName.ToString();
			ActiveFields.Remove(oldCol);
			ActiveFields.Add(newCol,newColName);
			FieldsList.Remove(newCol);
			FieldsList.Add(oldCol,OldColName);
		}

		private void mnColumn_Click(object sender, System.EventArgs e)
		{
			//optional
			MenuItem itm= (MenuItem)sender;
			string member= itm.Text;
			int colIndex=mGrid.ActiveColumnIndex;
			string oldName=Columns[colIndex].MappingName;
			if(!oldName.Equals(member))
			ChangeColumn(colIndex,oldName,member);
		} 

		public void ShowColumnMenu(Point pos,int width)
		{
			bool ok=CreateFieldsMenu();
			if(ok)
			{
				PopUpMenu.BackColor=this.HeaderBackColor;
				PopUpMenu.BorderStyle=Border3DStyle.Etched;
				PopUpMenu.DropDownWidth=width;// mGrid.ActiveColumnWidth;// Columns[mGrid.ActiveColumnIndex].Width;
				PopUpMenu.ShowPopUp(this.mGrid.PointToScreen(pos));
				//optional
				//FieldsMenu.Show(this.mGrid,pos);
			}
		}

		public bool IsColumnTypeOK(GridColumnStyle col,Type dataType)
		{
			if(col.ColumnType==ColumnTypes.BoolColumn)
			{
				return dataType.Equals(typeof(bool));
			}
			if(col.ColumnType==ColumnTypes.DateTimeColumn)
			{
				return dataType.Equals(typeof(DateTime));
			}
			if(col.ColumnType==ColumnTypes.TextColumn || col.ColumnType==ColumnTypes.LabelColumn)
			{
				if(col.DataType==DataTypes.Number)
				{
					return (((dataType.Equals(typeof(short)) || dataType.Equals(typeof(int))) || (dataType.Equals(typeof(long)) || dataType.Equals(typeof(ushort)))) || (((dataType.Equals(typeof(uint)) || dataType.Equals(typeof(ulong))) || (dataType.Equals(typeof(decimal)) || dataType.Equals(typeof(double)))) || ((dataType.Equals(typeof(float)) || dataType.Equals(typeof(byte))) || dataType.Equals(typeof(sbyte)))));
				}
				else
					return true;
			}
			return false;
		}
		#endregion

		#region Tables

		private TableCollection tbls;
		private int tblCount;
	    private bool allowMultiTables;
		private int tblSelected;


		[Category("Tables"),DefaultValue(false)]
		public bool AllowMultiTables
		{
			get{return this.allowMultiTables;}
			set{this.allowMultiTables=value;}
		}

		[Category("Tables"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public TableCollection  Tables 
		{
			get
			{
				if(tbls==null)
				{
                  tbls=new TableCollection(this);
				  //allowMultiTables=true;
				}
				return tbls;
			}
		}

		private void SetTables(System.Data.DataSet ds)
		{
			if(!(ds.Tables.Count>1 && allowMultiTables))
			{
				this.tblCount=0;
				return ;
			}
				if(this.tbls==null)
				{
					tbls=new TableCollection(this);
					foreach(System.Data.DataTable t in ds.Tables)
					{

						GridTableStyle tbl=new GridTableStyle(this);
						tbl.MappingName=t.TableName;
						tbls.Add(tbl);
					}
				}
				MenuItem itmSpace= m_Caption.GridContextMenu.AddItem("-",-1,null); 
				m_Caption.GridContextMenu.SetDraw(itmSpace,true);
	
				foreach(GridTableStyle t in tbls)
				{
					MenuItem itm= m_Caption.GridContextMenu.AddItem(t.MappingName,-1,new EventHandler(mnTable_Click));  
					if(t.MappingName==m_MappingName)
					{
						itm.Checked=true;
					}
					m_Caption.GridContextMenu.SetDraw(itm,true);
				}
			
			this.tblCount=tbls.Count;
		}

		public virtual void SetTableStyle(int index)
		{
			if(tbls==null)
				return;

			if(index > -1 && index < Tables.Count)
			{
				string mapName=Tables[index].MappingName; 
				if(mapName!=null)
				{
                  SetTableStyle(mapName);
				}
			}
		}

		public virtual void SetTableStyle(string mapName)
		{
			if(tbls==null)
				return;

			GridTableStyle ts=tbls.GetTable(mapName);
	
			if(ts==null)
			{
				MsgBox.ShowWarning("Invalid TableStyle with mappingName " + mapName);
				return;
			}
			if(mapName!=m_MappingName)
			{
				int oldItem=-1; 
				int newItem=-1; 
				int flag=0;
				foreach(MenuItem mn in m_Caption.GridContextMenu.MenuItems)
				{
					if(mn.Text.Equals(m_MappingName))
					{
						oldItem= mn.Index;
						flag++;
					}
					if(mn.Text.Equals(mapName))
					{
						newItem= mn.Index;
						flag++;
					}
					if(flag==2)
						break;
				}

				//this.selectedIndex=tbls.IndexOf(ts);
				DataMember=mapName;
                m_Caption.GridContextMenu.MenuItems[oldItem].Checked=false; 
				m_Caption.GridContextMenu.MenuItems[newItem].Checked=true; 
				OnDataSelectedChanged(EventArgs.Empty);
			}
		}

		private void mnTable_Click(object sender, System.EventArgs e)
		{
			MenuItem itm= (MenuItem)sender;
			string member= itm.Text;
			SetTableStyle(member);

//			if(member!=m_MappingName)
//			{
//				foreach(MenuItem mn in this.GridContextMenu.MenuItems)
//				{
//					if(mn.Text.Equals(m_MappingName))
//					{
//						mn.Checked=false;
//						break;
//					}
//				}
//			}
//			DataMember=member;
//			itm.Checked=true; 
		}

		protected virtual void OnDataSelectedChanged(EventArgs e)
		{
			if(DataSelectedChanged!=null)
			{
              DataSelectedChanged(this,e);  
			}
		}

		#endregion

		#region Columns

		protected virtual void OnColumnCollectionChange(CollectionChangeEventArgs e)
		{
			if (((e.Action != CollectionChangeAction.Refresh) || (e.Element == null)))
			{
				if (e.Action == CollectionChangeAction.Add)
				{
					//this.colsCreated=false;
					//cols.Add((GridColumnStyle)e.Element);
				}
				else if (e.Action == CollectionChangeAction.Remove)
				{
					//this.colsCreated=false;
					//cols.Remove((GridColumnStyle)sender);
				}
			}
			else
			{
				base.Invalidate();
				base.PerformLayout();
			}
		}

		private void cols_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			OnColumnCollectionChange(e);
			if(this.DesignMode)
			{
				this.Refresh();
			}
		}

		protected internal virtual GridColumnStyle CreateNewColumn(PropertyDescriptor prop, bool isDefault)
		{
			//GridColumnStyle style1 = null;
			Type type1 = prop.PropertyType;
			bool readOnly=this.ReadOnly;
			if (type1.Equals(typeof(bool)))
			{
				return new GridBoolColumn(prop, isDefault);
			}
			if (!type1.Equals(typeof(string)))
			{
				if (type1.Equals(typeof(DateTime)))
				{
					if(readOnly)
					   return new GridLabelColumn(prop, "d", isDefault);
					else
						return new GridDateColumn(prop, "d", isDefault);
		
				}
				if (((type1.Equals(typeof(short)) || type1.Equals(typeof(int))) || (type1.Equals(typeof(long)) || type1.Equals(typeof(ushort)))) || (((type1.Equals(typeof(uint)) || type1.Equals(typeof(ulong))) || (type1.Equals(typeof(decimal)) || type1.Equals(typeof(double)))) || ((type1.Equals(typeof(float)) || type1.Equals(typeof(byte))) || type1.Equals(typeof(sbyte)))))
				{
					if(readOnly)
						return new GridLabelColumn(prop, "G", isDefault);
					else
					{
                        GridTextColumn col=new GridTextColumn(prop, "G", isDefault);
						col.FormatType= Formats.GeneralNumber;
						return col;//  new GridTextColumn(prop, "G", isDefault);
					}
				}
			}
			if(readOnly)
				return new GridLabelColumn(prop);
			else
				return new GridTextColumn(prop,isDefault);
		}

		//GridColumnStyle []
		protected internal void CreateGridColumns()
		{
			if(colsCreated)
			{
				return ;//this.gridColumns;
			}

			//GridColumnStyle [] arry=null;   
		
			if(tblCount>0)
			{
				GridTableStyle ts= tbls.GetTable(m_MappingName); 
				if(ts ==null)
				{
					throw new Exception("Invalid Mapping name in TableStyle");
				}
				if(ts.Columns.Count>0)
				{
					CreateColumnsVisible(ts.Columns);
					//arry=new GridColumnStyle[ts.Columns.Count];   
					//ts.Columns.CopyTo(arry,0);
					goto Label_01;
				}
				
			}
			else if(cols.Count>0)// && tblCount==0)
			{
	            CheckColumnMappingName(cols);
				CreateColumnsVisible(cols);
				//arry=new GridColumnStyle[cols.Count];   
				//cols.CopyTo(arry,0);
			}
			else if(this.DataSource !=null)
			{
				Columns.Clear();
				CurrencyManager listManager=mGrid.CM;
				PropertyDescriptorCollection collectionProp = listManager.GetItemProperties();
				int cnt = collectionProp.Count;
				//arry=new GridColumnStyle[cnt];   

				for (int i = 0; i < cnt; i++)
				{
					PropertyDescriptor prop = collectionProp[i];
					GridColumnStyle col=CreateNewColumn(prop,false);
					col.MappingName=prop.Name;
					col.HeaderText =prop.Name;
					col.grid=this;
					col.Visible=true;
					this.cols.Add(col);
					//arry.SetValue ((GridColumnStyle)col,i);
				}
				colsAutoCreated=true;
				CreateColumnsVisible(cols);
			}
			Label_01:
			//this.gridColumns=(GridColumnStyle[])arry;
			this.colsCreated=true;
			//return arry;
		}

		private bool CheckColumnMappingName(GridColumnsCollection colls)
		{
			foreach(GridColumnStyle c in colls)
			{
				if(c.MappingName=="")
				{
					string ex= "GridColumnStyle " + c.HeaderText + " has no mapping name";
					MsgBox.ShowError(ex);
					//throw new Exception("GridColumnStyle " + c.HeaderText + " has no mapping name");
					return false;
				}
			}
			return true;
		}

		internal void SetColumnsVisible()
		{
          CreateColumnsVisible(this.cols);
		}

		private void CreateColumnsVisible(GridColumnsCollection colls)
		{
			int length=GetColumnsVisibleCount(colls);
            ArrayList array=new ArrayList();
			int cnt=0;

			foreach(GridColumnStyle c in colls)
			{
				if(c.Visible)
				{
					array.Add(c);
					cnt++;
				}
			}
			this.gridColumns=new GridColumnStyle[cnt];
			array.CopyTo(this.gridColumns,0);
			gridColumnsCount=cnt;
		}

		private int GetColumnsVisibleCount(GridColumnsCollection colls)
		{
			int cnt=0;
			foreach(GridColumnStyle c in colls)
			{
				if(c.Visible)
					cnt++;
			}
			return cnt;
		}
		
		internal GridColumnStyle[] GridColumns
		{
			get{return this.gridColumns;} 
		}

		[Category("Columns"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public GridColumnsCollection  Columns 
		{
			get
			{
				if(this.cols==null)
				{
					this.cols=new GridStyle.GridColumnsCollection (this);
				}
				return cols;
			}
		}

		public GridColumnStyle[] GetVisibleColumns()
		{
			int cnt=0;
			foreach(GridColumnStyle c in Columns)
			{
				if(c!=null && c.Visible && c.Width>0) cnt++;
			}
			GridColumnStyle[] gcs=new GridColumnStyle[cnt];
			int i=0;
			foreach(GridColumnStyle c in Columns)
			{
				if(c!=null && c.Visible && c.Width>0) 
				{
					gcs[i]=c;
					i++;
				}
			}
			return gcs;
		}

		public int GetWidthInternal()
		{
			int width=0;
			int rowHeader=this.RowHeadersVisible ? this.RowHeaderWidth :0;
			if(Columns.Count>0)
			{
				foreach(GridColumnStyle c in Columns)
				{
					width+=c.Width; 
				}
				
				width+=mGrid.VScrollBar().Width+2;
			}
			else if(m_DataSource!=null)
			{
				int i=DataList.Table.Columns.Count;
				width=i*PreferredColumnWidth;
			}
			else
			{
				width=this.Width;	
			}
            
			width += rowHeader; 
			if(width> m_MaxWidth)
				width= m_MaxWidth; 
			return width;
		}
		
		public int GetHeightInternal(int visibleRows)
		{
			int rows=mGrid.CM.Count;//DataList.Count;
			if(visibleRows > 0 && visibleRows < rows)
			{
				rows=visibleRows; 
			}
			int rowHeight=PreferredRowHeight;
			int rowsAdd=DataList.AllowNew ? 1:0 ;
			//int gridHeight=HeightInternal;//.DataGrid.Height;
			int HeaderAdd=ColumnHeadersVisible  ? Grid.DefaultColumnHeaderHeight:0 ;
			//int CaptionHeight=CaptionVisible  ? Grid.DefaultCaptionHeight :0;
			return ((rowsAdd+rows)*rowHeight)+HeaderAdd;//+ CaptionHeight; 
		}

		internal protected void AdjustColumns()
		{
			try
			{

				int gridHeight=HeightInternal;
				int CalcRowsHeight=GetHeightInternal(0);
				
				bool showVerticalScroll= (CalcRowsHeight > gridHeight);
				int GridWidth=this.Width;
				
				DataGridTableStyle ts=mGrid.TableStyle;

				 
				int ScrollWidth=mGrid.VScrollBar().Width;
				if(!showVerticalScroll)
					ScrollWidth=0;//16;
		              
				int RowHeader=0;
				if(ts.RowHeadersVisible)
					RowHeader=ts.RowHeaderWidth;
				int colCount= ts.GridColumnStyles.Count;
				int colsWidth=0; 
				int colsW=0;
				int tCols=0;  
				int gridWidth=GridWidth-ScrollWidth-RowHeader-2; 
				ArrayList list=new ArrayList();
		   
				for(int i=0;i<colCount;i++)
				{

					colsW=0;
					colsW= ts.GridColumnStyles[i].Width;
		        	
					if(colsW<0)
						colsW=Grid.DefaultColumnWidth;
				    
					if(colsW>0)
					{
						list.Add(ts.GridColumnStyles[i]);
						colsWidth+=colsW;
						tCols++;  
					}
				}

				if(tCols==0)
				{
					MessageBox.Show("No ColumnStyles Define");
					return;
				}

				if(m_MaxWidth<colsWidth)
				{
					gridWidth=m_MaxWidth-Grid.DefaultScrollWidth;

				}

				int sumColsW=0;
				int defaultWidth=this.PreferredColumnWidth;
				if(colsWidth != gridWidth)
				{
					float ColDiff=(float)((gridWidth-colsWidth)/tCols);
					int colWidth;

					for(int i=0;i<list.Count-1;i++)
					{
						colWidth=((DataGridColumnStyle)list[i]).Width;
						colWidth+=((int)ColDiff);
						if(colWidth<0)
							colWidth= defaultWidth;
                        
						((DataGridColumnStyle)list[i]).Width=colWidth;
						sumColsW+=colWidth;
					}

//					for(int i=0;i<colCount-1;i++)
//					{
//						colWidth= ts.GridColumnStyles[i].Width;//this.Columns[i].Width;
//						if(colWidth>0) 
//							colWidth+=((int)ColDiff);
//						if(colWidth<0)
//							colWidth= defaultWidth;
//						//if(colsWidth>0)
//						//{
//						ts.GridColumnStyles[i].Width=colWidth;//+((int)ColDiff); 
//						//this.Columns[i].Width=colsWidth+((int)ColDiff); 
//						sumColsW+=ts.GridColumnStyles[i].Width;//this.Columns[i].Width;// 
//						//}
//					}
					int diff=gridWidth-sumColsW;
					//base.GridColumnStyles[cols-1].Width=base.GridColumnStyles[cols-1].Width+diff; 
					if(diff<0)
                       diff=defaultWidth;
 
					((DataGridColumnStyle)list[list.Count-1]).Width=diff;
					//if(ts.GridColumnStyles[colCount-1].Width!=0) 
					  // ts.GridColumnStyles[colCount-1].Width=diff; 
				}
			
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message);
			}
		}

		#endregion

		#region GridProperty

		internal System.Windows.Forms.DataGridTableStyle  TableStyle 
		{
			get { return m_TableStyle; }
		}

		[Browsable(false)] 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
		internal  GridStyle.CtlGrid   DataGrid 
		{
			get{return mGrid;}
		}
        
		[Browsable(false)] 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
		internal protected System.Windows.Forms.GridTableStylesCollection  TableStyles 
		{
			get { return mGrid.TableStyles; }
		}
	
		[Browsable(false)]
		public System.Windows.Forms.DataGridCell CurrentCell 
		{
			get{return mGrid.CurrentCell;}
			set{mGrid.CurrentCell = value;}
		}

//		[Browsable(false)]
//		public object CurrentCellValue 
//		{
//			get{return mGrid[mGrid.CurrentCell];}
//			set{mGrid[mGrid.CurrentCell] = value;}
//		}

		[Browsable(false),DefaultValue(-1)]
		public int CurrentRowIndex 
		{
			get{return mGrid.CurrentRowIndex;}
			set
			{
				if(mGrid.CM !=null ) 
				{mGrid.CurrentRowIndex = value;}
			}
		}

		[Browsable(false)]
		public object this[int col] 
		{
			get{return mGrid[mGrid.CurrentRowIndex, col];}
			set{mGrid[mGrid.CurrentRowIndex, col] = value;}
		}

		[Browsable(false)]
		public object this[string colName] 
		{
			get{return ItemCell(mGrid.CurrentRowIndex, colName);}
			set{SetItemCell(mGrid.CurrentRowIndex, colName,value);}
		}

		[Browsable(false)]
		public object this[int row, int col] 
		{
			get{return mGrid[row, col];}
			set{mGrid[row, col] = value;}
		}
        
		[Browsable(false)]
		public object this[int row, string colName] 
		{
			get{return ItemCell(row, colName);}
			set{SetItemCell(row, colName,value);}
		}

		[Category("Behavior"),DefaultValue(GridStyle.SelectionTypes.Cell )]
		public GridStyle.SelectionTypes  SelectionType 
		{
			get{return mGrid.SelectionType;}
			set{mGrid.SelectionType = value;}
		}

//		[Browsable(false),Category("Style")]
//		public  GridTablesMode GridTablesMode
//		{
//			get {return m_GridTablesMode;}
//		}

		[Category("Style"),DefaultValue(false)]
		public  bool AutoAdjust 
		{
			get{return m_AutoAdjust;}
			set
			{
				m_AutoAdjust=value;
				//if(value && IsHandleCreated && isCreated)
				//{
				//	//SetAutoAdjust();
				//}
			}
		}

		[Browsable(false)] 
		public int HeightInternal
		{
			get{return mGrid.Height;}
		}

		[Browsable(false)] 
		public bool Dirty
		{
			get{return dirty;}
		}
		#endregion

		#region GridTaleStyle Property

		[Category("Behavior"),DefaultValue(true)]   
		public bool RowHeadersVisible 
		{
			get{return m_TableStyle.RowHeadersVisible;}
			set
			{
				mGrid.RowHeadersVisible = value;
				m_TableStyle.RowHeadersVisible=value;
				//this.Invalidate();
			}
		}
		
		[Category("Behavior"),DefaultValue(true)]   
		public bool ColumnHeadersVisible 
		{
			get{return m_TableStyle.ColumnHeadersVisible;}
			set
			{
				mGrid.ColumnHeadersVisible = value;
				m_TableStyle.ColumnHeadersVisible=value;
				//this.Invalidate();
			}
		}

		[Category("Behavior"),DefaultValue(35)]   
		public int RowHeaderWidth 
		{
			get{return m_TableStyle.RowHeaderWidth;}
			set
			{
				mGrid.RowHeaderWidth = value;
				m_TableStyle.RowHeaderWidth=value;
				//this.Invalidate();
			}
		}

		[Category("Behavior"),DefaultValue(true)]   
		public bool AllowSorting 
		{
			get{return m_TableStyle.AllowSorting;}
			set
			{
				mGrid.AllowSorting = value;
				m_TableStyle.AllowSorting=value;
				//this.Invalidate();
			}
		}

		[Category("Style"),DefaultValue(typeof(Color),"System.Drawing.SystemColors.HotTrack")]   
		public System.Drawing.Color LinkColor
		{
			get{return m_TableStyle.LinkColor;}
			set
			{
				mGrid.LinkColor = value;
				m_TableStyle.LinkColor=value;
				//this.Invalidate();
			}
		}

		[DefaultValue(typeof(Font),"Microsoft Sans Serif , 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177))")]   
		public System.Drawing.Font HeaderFont 
		{
			get{return m_TableStyle.HeaderFont;}
			set
			{
				mGrid.HeaderFont = value;
				m_TableStyle.HeaderFont=value;
				//this.Invalidate();
			}
		}
        
		[Category("Style"),DefaultValue(System.Windows.Forms.DataGridLineStyle.Solid)]   
		public System.Windows.Forms.DataGridLineStyle GridLineStyle 
		{
			get{return m_TableStyle.GridLineStyle;}
			set
			{
				mGrid.GridLineStyle = value;
				m_TableStyle.GridLineStyle=value;
				//this.Invalidate();
			}
		}
        
		[DefaultValue(typeof(Color),"Control")]   
		internal System.Drawing.Color GridBackColor 
		{
			get{return m_TableStyle.BackColor;}
			set
			{
				//mGrid.BackColor = value;
				m_TableStyle.BackColor=value;
				gridLayout.BackColor=value;
				//this.Invalidate();
			}
		}
        
		[DefaultValue(typeof(Color),"WindowText")]   
		internal System.Drawing.Color GridForeColor 
		{
			get{return m_TableStyle.ForeColor;}
			set
			{
				//mGrid.ForeColor = value;
				m_TableStyle.ForeColor=value;
				gridLayout.ForeColor=value;
				//this.Invalidate ();
			}
		}

		[DefaultValue(typeof(Color),"Window")]   
		internal System.Drawing.Color AlternatingBackColor 
		{
			get{return m_TableStyle.AlternatingBackColor;}
			set
			{
				//mGrid.AlternatingBackColor = value;
				m_TableStyle.AlternatingBackColor=value;
				gridLayout.AlternatingColor=value;
				//this.Invalidate();
			}
		}

		[DefaultValue(typeof(Color),"ActiveCaption")]   
		internal System.Drawing.Color SelectionBackColor 
		{
			get{return m_TableStyle.SelectionBackColor;}
			set
			{
				//mGrid.SelectionBackColor = value;
				m_TableStyle.SelectionBackColor=value;
				//this.Invalidate();
			}
		}
	
		[DefaultValue(typeof(Color),"ActiveCaptionText")]   
		internal System.Drawing.Color SelectionForeColor 
		{
			get{return m_TableStyle.SelectionForeColor;}
			set
			{
				//mGrid.SelectionForeColor = value;
				m_TableStyle.SelectionForeColor=value;
				//this.Invalidate();
			}
		}
     
		[DefaultValue(typeof(Color),"Control")]   
		internal System.Drawing.Color HeaderBackColor 
		{
			get{return m_TableStyle.HeaderBackColor;}
			set
			{
				//mGrid.HeaderBackColor = value;
				m_TableStyle.HeaderBackColor=value;
				//this.Invalidate();
			}
		}
        
		[DefaultValue(typeof(Color),"ControlText")]   
		internal System.Drawing.Color HeaderForeColor 
		{
			get{return m_TableStyle.HeaderForeColor;}
			set
			{
				//mGrid.HeaderForeColor = value;
				m_TableStyle.HeaderForeColor=value;
				//this.Invalidate();
			}
		}

		[Category("Style"),DefaultValue(typeof(Color),"Control")]   
		public System.Drawing.Color GridLineColor 
		{
			get{return m_TableStyle.GridLineColor;}
			set
			{
				mGrid.GridLineColor = value;
				m_TableStyle.GridLineColor=value;
				//this.Invalidate();
			}
		}

		[Category("Behavior"),DefaultValue(75)]   
		public int PreferredColumnWidth 
		{
			get{return m_TableStyle.PreferredColumnWidth;}
			set
			{
				mGrid.PreferredColumnWidth = value;
				m_TableStyle.PreferredColumnWidth=value;
				//this.Invalidate();
			}
		}

		[Category("Behavior"),DefaultValue(16)]   
		public int PreferredRowHeight 
		{
			get{return m_TableStyle.PreferredRowHeight;}
			set
			{
				mGrid.PreferredRowHeight = value;
				m_TableStyle.PreferredRowHeight=value;
				this.Invalidate();
			}
		}

		[Category("Behavior"),Browsable(true),DefaultValue(false)]   
		public bool  ReadOnly
		{
			get{ return mGrid.ReadOnly ; }
			set
			{
				mGrid.ReadOnly =value; 
				m_TableStyle.ReadOnly=value;
				if(value)
				{
					this.allowAddNew=false;
					this.allowDelete=false;
				}
			}
		}

		#endregion

		#region StyleProperty

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new System.Windows.Forms.ScrollableControl.DockPaddingEdges DockPadding
		{
			get{return base.DockPadding;}
		}

//		[DefaultValue(typeof(Color),"DarkGray")]   
//		internal System.Drawing.Color BorderColor 
//		{
//			get { return m_BorderColor; }
//			set 
//			{
//				m_BorderColor = value;
//				gridLayout.BorderColor=value;
//				this.Invalidate ();
//			}
//		}
//
//		[DefaultValue(typeof(Color),"DarkGray")]   
//		internal System.Drawing.Color BorderHotColor 
//		{
//			get { return m_BorderHotColor; }
//			set 
//			{
//				m_BorderHotColor = value;
//				gridLayout.BorderHotColor=value;
//				//this.Invalidate ();
//			}
//		}

		[Category("Style"),DefaultValue(BorderStyle.FixedSingle)] 
		public new System.Windows.Forms.BorderStyle BorderStyle
		{
			get{ return m_BorderStyle;}
			set
			{ 
				if( m_BorderStyle != value)
				{
					m_BorderStyle= value; 
					this.Invalidate();
					OnBorderStyleChanged(EventArgs.Empty);
				}
			}
		}
	
		[Category("Style"),Browsable(true),	RefreshProperties(RefreshProperties.Repaint),DefaultValue(false)]   
		public bool  FlatMode
		{
			get{ return mGrid.FlatMode ; }
			set{mGrid.FlatMode = value; }
		}


		[DefaultValue(typeof(Color),"AppWorkspace")]   
		public System.Drawing.Color BackgroundColor 
		{
			get{return mGrid.BackgroundColor;}
			set
			{
				this.BackColor=value;
				mGrid.BackgroundColor = value;
			}
		}
     
		[Category("Caption"),DefaultValue(typeof(Font),"Microsoft Sans Serif , 8.25F, System.Drawing.FontStyle.Bold")]   
		public System.Drawing.Font CaptionFont 
		{
			get{return m_Caption.Font;}
			set{m_Caption.Font=value;}
		}
        
		[Category("Caption"),DefaultValue("")]   
		public string CaptionText 
		{
			get{return m_Caption.Text;}
			set{m_Caption.Text=value;}
		}
        
		[Category("Caption"),DefaultValue(true)]   
		public bool CaptionVisible 
		{
			get{return captionVisible;}//m_Caption.Visible;}// mGrid.CaptionVisible;}
			set
			{
				this.captionVisible=value;
				m_Caption.Visible=value;
			}//mGrid.CaptionVisible = value;}
		}
        
        
		[DefaultValue(typeof(Color),"Control")]   
		internal System.Drawing.Color ParentRowsBackColor 
		{
			get{return mGrid.ParentRowsBackColor;}
			set{mGrid.ParentRowsBackColor = value;}
		}
        
		[DefaultValue(typeof(Color),"WindowText")]   
		internal System.Drawing.Color ParentRowsForeColor 
		{
			get{return mGrid.ParentRowsForeColor;}
			set{mGrid.ParentRowsForeColor = value;}
		}
        
		[DefaultValue(true)]   
		internal bool ParentRowsVisible 
		{
			get{return mGrid.ParentRowsVisible;}
			set{mGrid.ParentRowsVisible = value;}
		}
        

		#endregion

		#region statusBar

		[Category("Behavior"),DefaultValue(mControl.GridStyle.Controls.StatusBarMode.Hide)]
		public mControl.GridStyle.Controls.StatusBarMode StatusBarMode 
		{
			get
			  { 
				if(m_StatusBar==null)
					return GridStyle.Controls.StatusBarMode.Hide;
				return this.m_StatusBar.StatusBarMode;
			   }
			set
			{
				if(value!=GridStyle.Controls.StatusBarMode.Hide)
					StatusBar.StatusBarMode=value;
                else if(m_StatusBar!=null)
                    StatusBar.StatusBarMode=value;
				//this.m_StatusBar.StatusBarMode =value;
				this.Invalidate();
			}
		}
		 
		[Browsable(false)]
		public mControl.GridStyle.Controls.GridStatusBar   StatusBar 
		{
			get{return this.m_StatusBar; }
		}
       
		[Browsable(false),DefaultValue(false)]   
		public bool IsStatusBarVisible 
		{
			get{
				if(m_StatusBar==null)
					return false;
				return this.m_StatusBar.Visible;
			   }
		}

		internal void SumPanel(int index,AggregateMode mode, decimal oldValue,decimal newValue )
		{

//			if( this.StatusBarMode!=mControl.GridStyle.Controls.StatusBarMode.ShowPanels)// .ShowStatusBar)
//				return;
		
			try
			{
				decimal res= System.Convert.ToDecimal ( this.m_StatusBar.Panels[index+1].Text);   
				switch(mode)
				{
					case AggregateMode.Sum:
						res+= (newValue-oldValue);
						break;
					case AggregateMode.Avg:
						int cnt=this.Rows.Count;
						if(cnt>0)
						 res+= (res+newValue-oldValue)/cnt;
						break;
					case AggregateMode.Max:
						res=Math.Max(res,newValue);
						break;
					case AggregateMode.Min:
						res=Math.Min(res,newValue);
						break;
					default:
						break;
				}

				//res+= diff;
				this.m_StatusBar.Panels[index+1].Text =res.ToString ();   
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message.ToString (),"mControl"); 
			}
		}

		public void RunSum()
		{
			if( this.StatusBarMode!=mControl.GridStyle.Controls.StatusBarMode.ShowPanels)// .ShowStatusBar)
				return;
			StatusBar.RunSum(m_TableStyle);
		}

		public decimal SumGridColumn(int colIndex,AggregateMode mode)
		{
			return StatusBar.SumGridColumn(colIndex,mode);
		}
		
		#endregion

		#region CtlContextMenu

		//private bool showPopUpMenu=false;
		public bool ShowPopUpMenu
		{
			get{return this.m_Caption.ShowPopUpMenu;}
			set{this.m_Caption.ShowPopUpMenu=value;}
		}

		public virtual void PerformPrint()
		{
			if(this.RowCount>0)
			  PrintGridDataView.Print(this);
			//ReportDocument rpt=new mControl.WinCtl.Controls.ReportDocument(this);
			//rpt.HAlignment=HorizontalAlignment.Center;
			//rpt.PageNumbering=true;
			//rpt.Print();
			
		}

		public virtual void PerformFilter()
		{
			if(this.DataList!=null)
                  GridStyle.GridFilterDlg.ShowFilter(this);
				//GridStyle.FilterDlg.Open(this);
		}
		public virtual void PerformSum()
		{
			if(this.IsStatusBarVisible && this.DataList!=null) 
				m_StatusBar.RunSum (m_TableStyle);
		}
		public virtual void PerformColumnsFilter()
		{
			if(this.Columns.Count>0) 
			   ColumnFilterDlg.ShowColumns(this);
		}
		public virtual void PerformAdjustColumns()
		{
			if(this.mGrid.TableStyle.GridColumnStyles.Count >0)//|| this.Columns.Count>0 && this.DataList!=null)
			{
				AdjustColumns ();
				if(this.IsStatusBarVisible)//  ShowStatusBar )
					this.m_StatusBar.ResettingPanels(m_TableStyle,true);
				this.Invalidate();
			}
		}

		public virtual void PerformShowStatusBar()
		{
			this.StatusBar.Visible=!this.StatusBar.Visible;
		}

		public virtual  void PerformExport()
		{
			
			if(this.DataList==null)
				return;

			System.Data.DataTable dtExport = this.DataGrid.DataList.Table.Copy();
			try
			{
				mControl.Data.ExportColumnType[] exColumns=PrintGridDataView.CreateExportColumns(this);
				mControl.Data.Export.WinExport(dtExport,exColumns);
			}
			catch
			{
				mControl.Data.Export.WinExport(dtExport);
			}

			//string filter="XLS files (*.xls)|*.xls|CSV files (*.csv)|*.csv";
			//string fileName=Util.CommonDialog.SaveAs  (filter); 
 
//			string fileName=mControl.Data.Export.GetFileExportName();
// 
//			if(fileName!="")
//			{
//	
//				mControl.Data.Export ex=new mControl.Data.Export (mControl .Data.AppType.Win);
//				System.Data.DataTable dtExport = this.DataGrid.DataList.Table.Copy();
//				//for(int i=0;i<cnt;i++)
//				//{
//				//	Headers.SetValue (this.DataGrid.TableStyle.GridColumnStyles[i].HeaderText,i); 
//				//}
//
//				if(fileName.EndsWith ("csv"))
//					ex.ExportData (dtExport ,mControl.Data.ExportFormat.CSV,fileName);    
//				else if(fileName.EndsWith ("xls"))
//					ex.ExportData (dtExport ,mControl.Data.ExportFormat.Excel,fileName);    
//	
//			}

		}

		public virtual  void PerformImport()
		{
			try
			{
				DataSet ds=mControl.Data.Export.ImportXml();
				if(ds==null || ds.Tables.Count==0)
					return;
				if(ds.Tables.Count==1)
				{
					this.DataSource=ds.Tables[0];
				}
				else
				{
					SetTables(ds);
					SetTableStyle(0);
					//this.DataSource=ds.Tables[0];
				}
			}
			catch(Exception ex)
			{
				Util.MsgBox.ShowError(ex.Message);
			}
		}

		#endregion

		#region DataProperties

		public void Select(int row)
		{
			mGrid.Select(row);
		}

		[Browsable(false)]
		public System.Data.DataView DataList 
		{
			get{ return mGrid.DataList;}
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Description("DataGridListManager")]
		internal CurrencyManager ListManager
		{
			get{ return mGrid.CM;}
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Description("DataGridRows")]
		public System.Collections.IList Rows
		{
			get
			{ 
				if(ListManager==null)
					return null;
				return mGrid.CM.List;
			}
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Description("DataGridRows")]
		public int RowCount
		{
			get
			{ 
				if(ListManager==null)
					return 0;
				return mGrid.CM.List.Count;
			}
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Description("DataGridVisibleColumnCount")]
		public int VisibleColumnCount
		{
			get{return mGrid.VisibleColumnCount;}
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Description("DataGridVisibleRowCount")]
		public int VisibleRowCount
		{
			get{return mGrid.VisibleRowCount;}
		}

		public System.Data.DataRow GetDataRow(int indx) 
		{
			try 
			{
				return ((System.Data.DataRow)(DataList.Table.Rows[indx]));
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}


		[Category("Data"),DefaultValue(true)]
		public bool AllowAddNew
		{
			get{return this.allowAddNew;}
			set
			{
				allowAddNew=value;
				if(this.DataList!=null)
				{
					this.DataList.AllowNew=value;
				}
			}
		}

//		public bool AllowEdit
//		{
//			get{return this.allowEdit;}
//			set
//			{
//				allowEdit=value;
//				if(!value)
//					AllowAddNew=false;
//				if(this.DataList!=null)
//				{
//					this.DataList.AllowEdit=value;
//				}
//			}
//		}

		[Category("Data"),DefaultValue(true)]
		public bool AllowDelete
		{
			get{return this.allowDelete;}
			set
			{
				allowDelete=value;
				if(this.DataList!=null)
				{
					this.DataList.AllowDelete=value;
				}
			}
		}

		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Description("DataGridDataSourceDescr"), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get
			{
				return m_DataSource;
			}
			set
			{
				if (((value != null) && !(value is System.Collections.IList)) && !(value is IListSource))
				{
					throw new Exception("BadDataSourceForComplexBinding");
				}
				if(this.initialising)
				{
					m_DataSource=value;
				}
				else
				{
					SetDataConnection(value,DataMember);
				}

//				if (((value != null) && !(value is System.Collections.IList)) && !(value is IListSource))
//				{
//					throw new Exception("BadDataSourceForComplexBinding");
//				}
//				if ((mGrid.DataSource == null) || !mGrid.DataSource.Equals(value))
//				{
//					sourceCreated=false;
//					if (((value == null) || (value == Convert.DBNull)) && !"".Equals(this.DataMember))
//					{
//						mGrid.DataSource = null;
//						mGrid.DataMember = "";
//						sourceChanged=true;
//						return;
//					}
//					else
//					{
//						if (value != null)
//						{
//							m_DataSource=value;
//							EnforceValidDataMember(value);
//							//mGrid.DataSource=value;
//							sourceChanged=true;
//						}
//					}
//				}
//				if(IsHandleCreated )
//				{
//					if(mGrid.DataSource !=null) 
//					{
//						SetTableStyles(false);
//					}
//					if(this.StatusBarMode==mControl.GridStyle.Controls.StatusBarMode.ShowPanels)
//					{
//						m_StatusBar.ShowPanelColumns(m_TableStyle,true);
//					}
//				}
			}
		}
 
		[DefaultValue((string) ""), Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Description("DataGridDataMemberDescr"), Category("Data")]
		public string DataMember
		{
			get
			{
				//				if(mGrid.DataMember.Length==0 && m_DataSource!=null)
				//				{
				//					return mGrid.DataList.Table.TableName;
				//				}
				return mGrid.DataMember;//this.dataMember;
			}
			set
			{
				if (mGrid.DataMember !=value)
				{
					mGrid.DataMember =value;  
					//sourceCreated=false;
					//sourceChanged=true;
					if(value!="")
					{
						if((m_DataSource!=null) && !(m_DataSource is System.Data.DataViewManager))
						{
							m_MappingName=value;
						}
						sourceChanged=true;
						if(IsHandleCreated)
						{
							if(tbls!=null)
							{
								this.tblSelected=tbls.IndexOf(m_MappingName);
							}
							this.SetTableStyles(true); 
						}
					}
				}

			}
		}

		[Category("Data"),DefaultValue(""), Editor("System.Windows.Forms.Design.GridColumnStyleMappingNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Localizable(true)]
		public string MappingName
		{
			get
			{
				return this.m_MappingName;
			}
			set
			{
				if(value ==null)// || value=="")
				{
					value="";//throw new ArgumentException("Invalid MappingName");
				}

				if (!this.m_MappingName.Equals(value))
				{
					this.m_MappingName = value;
				}
			}
		}
		#endregion

		#region DataManager

		public void SetDataBinding(object dataSource,string dataMember)
		{
			this.m_DataSource=dataSource;
			//this.m_MappingName=dataMember;
			this.mGrid.SetDataBinding(dataSource,dataMember);
		}

//		public void ReBind(object dataSource,string dataMember,string mappingName)
//		{
//			if(this.DesignMode) return;
//
//			if(mappingName ==null || mappingName=="")
//			{
//				throw new ArgumentException("Invalid MappingName");
//			}
//			this.MappingName=mappingName;
//			this.SetDataConnection(dataSource,dataMember);
//			//this.DataMember=dataMember;
//			//this.DataSource=dataSource;
//		}
//
//		public virtual void ReLoad()
//		{
//			//this.sourceCreated=false;
//			//this.DataSource=m_DataSource;
//			if(IsHandleCreated && m_DataSource != null)
//			{
//				EnforceValidDataMember(m_DataSource);
//				sourceChanged=true;
//				if(mGrid.DataSource !=null) 
//				{
//					SetTableStyles(true);
//				}
//				if(this.StatusBarMode==mControl.GridStyle.Controls.StatusBarMode.ShowPanels)
//				{
//					m_StatusBar.ShowPanelColumns(m_TableStyle,true);
//				}
//			}
//		}

		private void EnforceValidDataMember(object value)
		{
			string member=null;

			if(value is System.Data.DataSet)// && (this.DataMember==null || this.DataMember==""))
			{
				System.Data.DataSet ds=(System.Data.DataSet)value;
				member=this.DataMember;
		
				if(!(this.DataMember==null || this.DataMember==""))
				{
					if(!ds.Tables.Contains(this.DataMember))
					{
						throw new Exception("DataMember Not Exists in DataSource");
						//member=ds.Tables[0].TableName;
					}
				}
				else
				{
					throw new Exception("Invalid DataMember");
					//member=ds.Tables[0].TableName;
				}
				//mGrid.DataMember =member;
				m_MappingName=member;
				mGrid.DataSource =value;
				sourceCreated=true;
				if(IsHandleCreated && ds.Tables.Count>1 )
				{
					SetTables(ds);
				}
			}
			else if(value is System.Data.DataViewManager)
			{
				System.Data.DataViewManager dvm=(System.Data.DataViewManager)value;

				if(this.m_MappingName.Equals(""))
				{
					throw new Exception("Invalid MappingName");
				}

				if(!(this.DataMember==null || this.DataMember==""))
				{
					if(!dvm.DataSet.Tables.Contains(this.m_MappingName))
					{
						throw new Exception("MappingName Not Exists in DataSource");
					}
				}
				else
				{
					throw new Exception("Invalid DataMember");
				}

				mGrid.ParentRowsVisible=false;
				mGrid.ParentRowsLabelStyle=DataGridParentRowsLabelStyle.None;
				mGrid.DataSource =value;
				sourceCreated=true;
				goto Label_01;
			}
			else if(value is System.Data.DataView)
			{
				this.DataMember="";

				if(this.m_MappingName.Equals(""))
				{
					throw new Exception("Invalid MappingName");
				}
				member = ((System.Data.DataView)value).Table.TableName;
		
				if(m_MappingName != member)
				{
					((System.Data.DataView)value).Table.TableName=m_MappingName;
				}
				mGrid.DataSource =value;
				sourceCreated=true;
				goto Label_01;

			}
			else if(value is System.Data.DataTable)
			{
				this.DataMember="";

				if(this.m_MappingName.Equals(""))
				{
					throw new Exception("Invalid MappingName");
				}
				member = ((System.Data.DataTable)value).TableName;
		
				if(m_MappingName != member)
				{
					((System.Data.DataTable)value).TableName=m_MappingName;
				}
				mGrid.DataSource =value;
				sourceCreated=true;
				goto Label_01;

			}
			else
			{
				//Not Supported
				sourceCreated=false;
				return;
			}
			Label_01:

			if(this.DataList!=null && !this.ReadOnly)
			{
				this.DataList.AllowNew=this.allowAddNew;
				//this.DataList.AllowEdit=this.AllowEdit;
				this.DataList.AllowDelete=this.allowDelete;
			}
		}

	
		internal void SetDataConnection(object dataSource,string dataMember)
		{

			if (((dataSource != null) && !(dataSource is System.Collections.IList)) && !(dataSource is IListSource))
			{
				throw new Exception("BadDataSourceForComplexBinding");
			}

			if (dataSource == null) //|| (dataSource == Convert.DBNull) && !"".Equals(this.DataMember))
			{
				this.m_DataSource=null;
				mGrid.DataSource = null;
				mGrid.DataMember = "";
				sourceCreated=false;
				sourceChanged=true;
				return;
			}
			bool forcesStyle=this.cols.Count==0 || this.colsAutoCreated;

			if (!sourceCreated || m_DataSource!=dataSource)//!mGrid.DataSource.Equals(dataSource))
			{
				sourceChanged=sourceCreated;
				//forcesStyle=mGrid.DataSource!=null;
				m_DataSource=dataSource;
				EnforceValidDataMember(dataSource);
				//mGrid.DataSource=value;
				//sourceChanged=isSourcChanged;
			}
			//if(IsHandleCreated )
			//{
				if(mGrid.DataSource !=null) 
				{
					SetTableStyles(forcesStyle);
				}
				if(this.StatusBarMode==mControl.GridStyle.Controls.StatusBarMode.ShowPanels)
				{
					m_StatusBar.ShowPanelColumns(m_TableStyle,true);
				}
			//}
		}

		#endregion

		#region public Methods

		public void SetCurrentCell(int row,int col)
		{
			DataGridCell curCell=new DataGridCell(row,col);
			this.mGrid.CurrentCell=curCell;
		}

		[Description("Get Item Cell In DataGrid")]
		private object ItemCell (int row,string colName)
		{
			if(DataList==null)return null;
			return DataList[row][colName];// Table.Rows[row][colName];
			//return mGrid[row][this.GetColIndex (colName)];
		}
		
		private void SetItemCell (int row,string colName,object Value)
		{
			try 
			{
				System.Data.DataView dv = DataList;
				dv.Table.Rows[row][colName] = Value;
				mGrid.Update();
				mGrid.Invalidate(mGrid.GetCellBounds(row, dv.Table.Columns[colName].Ordinal));
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

//		public void ClearFilter()
//		{
//			((System.Data.DataView)mGrid.DataList).RowFilter ="";
//			//mGrid.DataSource =this.DataSource;
//		}
//     
//		public void OpenFilter(object sender, Grid grid) 
//		{
//			GridStyle.FilterDlg.Open(grid);
//		}
        
		public void SetAutoAdjust ()
		{
			AdjustColumns (); 
			if(this.IsStatusBarVisible)
				this.m_StatusBar.ResettingPanels(m_TableStyle,true);
		}

		public void SetFilter(string filter)
		{
		    System.Data.DataView dtv=this.DataList;
			if(dtv==null)return ;

			dtv.RowFilter ="";
			string msgNoFound= "No records were found that match the filter criteria.";
		
			try
			{
				if ((filter.Trim().Length == 0)) 
				{
					MessageBox.Show (msgNoFound,"mControl",MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
					return;
				}
				dtv.RowFilter = filter;
				this.SetDataBinding(dtv,"");
				//this.DataSource = dtv;
				// //Display the number of rows in the view
				
				//base.Text= (STATUS_MESSAGE + dtv.Count.ToString());
				//LblFilter.Text = strFilter;
				if ((dtv.Count == 0)) 
				{
					MessageBox.Show (msgNoFound,"mControl",MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message ,"mControl");
			}
		}

		
		public void RemoveFilter()
		{
			if(this.DataList!=null)
				this.DataList.RowFilter="";
		}

		internal void OnDirty()
		{
			if(!dirty)
			{
				dirty=true;
				if(this.DirtyChanged!=null)
				{
                  this.DirtyChanged(this,EventArgs.Empty);
				}
			}

		}

		public bool HasChanges()
		{
			if(DataList==null)return false;
			return DataList.Table.DataSet.HasChanges();
		}

	
		public int UpdateChanges(System.Data.IDbConnection conn,string dbTableName)
		{

			try
			{
				DataView dv=DataList;
				if(dv==null)return -1;
				this.EndEdit();
				
				Data.IDBCmd cmd=mControl.Data.DBUtil.Create(conn);

				int res=cmd.UpdateChanges(dv.Table,dbTableName);
				//int res=mControl.Data.DBUtil.UpdateChanges(conn,DataList.Table,dbTableName);
				if(res>0)
				{
					dv.Table.AcceptChanges();
				}
				dirty=false;
				return res;
			}
			catch(Exception ex)
			{
				MsgBox.ShowError(ex.Message);
				return -1;
			}
		}

		public int UpdateChanges(System.Data.IDbConnection conn)
		{
			return UpdateChanges(conn,this.MappingName);
		}

		/// <summary>
		/// Accept Changes local only
		/// </summary>
		public void AcceptChanges()
		{
			if(this.DataList==null)return;
			DataList.Table.AcceptChanges();
			dirty=false;
		}

		public System.Data.DataTable GetChanges()
		{
			if(this.DataList==null)return null;
          return this.DataList.Table.GetChanges();
		}

		public void RejectChanges()
		{
			if(this.DataList==null)return ;
			DataList.Table.RejectChanges();
			dirty=false;
		}
		public void RejectChanges(string mappingName)
		{
			if(DataSource is System.Data.DataSet)
			{
				System.Data.DataSet ds=(System.Data.DataSet)DataSource;
				ds.Tables[mappingName].RejectChanges();
			}
			else
			{
				if(this.DataList==null)return ;
		
				if(DataList.Table.TableName.Equals(mappingName))
				{
					DataList.Table.RejectChanges();
				}
				else
				{
					MsgBox.ShowError("Invalid MappingName");
				}
			}
			dirty=false;
		}
		public void RejectAllChanges()
		{
			if(DataSource is System.Data.DataSet)
			{
				System.Data.DataSet ds=(System.Data.DataSet)DataSource;
				ds.RejectChanges();
			}
			else
			{
				if(this.DataList==null)return ;
				DataList.Table.RejectChanges();
			}
			dirty=false;
		}

		public new void Update()
		{
		   this.mGrid.Update();
           base.Update(); 
		}

		#endregion

		#region Override

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			if(this.DesignMode)
			{
              this.Invalidate();
			}
		}


		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e  )
		{ 
			base.OnPaint (e) ;

			if(m_BorderStyle==BorderStyle.FixedSingle)
			{
				using(System.Drawing.Pen p = CtlStyleLayout.GetPenBorder())// new System.Drawing.Pen(m_BorderColor,1))
				{
					e.Graphics.DrawRectangle (p,this.ClientRectangle.X ,ClientRectangle.Y   ,ClientRectangle.Width-1 ,ClientRectangle.Height-1 );    
				}
			}
			else if(m_BorderStyle==BorderStyle.Fixed3D )
			{
				ControlPaint.DrawBorder3D(e.Graphics,this.ClientRectangle,Border3DStyle.Sunken);
			}
		}

		protected virtual void OnColumnResize(GridStyle.ColumnResizeEventArgs e)
		{
			if( this.StatusBarMode==mControl.GridStyle.Controls.StatusBarMode.ShowPanels)
			{
				this.m_StatusBar.Panels [e.Column+1].Width =e.NewSize;   
			}
			//if(this.AutoAdjust && isCreated)
			//{
			//	SetAutoAdjust(); 
			//}

			if(ColumnResize!=null)
				ColumnResize(this,e); 
		}
		protected virtual void OnCurrentCellChanged(EventArgs e)
		{
			if(CurrentCellChanged!=null)
				CurrentCellChanged(this,e);
		}
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			if(BorderStyleChanged!=null)
				BorderStyleChanged(this,e);
		}
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			if(ReadOnlyChanged!=null)
				ReadOnlyChanged(this,e);
		}
		protected virtual void OnNavigate(NavigateEventArgs e)
		{
			if(Navigate!=null)
				Navigate(this,e);
		}
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			if(DataSourceChanged!=null)
				DataSourceChanged(this,e);
		}
		protected virtual void OnFlatModeChanged(EventArgs e)
		{
			if(FlatModeChanged!=null)
				FlatModeChanged(this,e);
		}

		#endregion

		#region internal TableStyle

		internal protected void SetTableStyles(bool force)
		{
//			if((!force && this.tblsCreated) || !sourceCreated)
//				return;

			if(this.m_TableStyle==null)
			{
				m_TableStyle=new DataGridTableStyle();
			}

			if(this.DataList !=null && (m_MappingName==null || m_MappingName.Equals("")))
			{
				this.MappingName=this.DataList.Table.TableName;
				if(m_MappingName==null || m_MappingName.Equals(""))
				{
					throw new Exception("Invalid Mapping name");
				}
			}

//			if(sourceChanged)
//			{
//				m_TableStyle.GridColumnStyles.Clear();
//				m_TableStyle.MappingName =m_MappingName;
//				sourceChanged=false;
//				tblsCreated=false;
//			}
			if(force)
			{
              cols.Clear();
			  colsCreated=false;
			}
			if(cols.Count ==0 || !colsCreated) 
			{
				CreateGridColumns();
				tblsCreated=false;
			}
			if(this.allowChangeColumnMapping && Columns.Count>0)
			{
				CreateActiveFields(); 
				CreateFields();
			}
			if(!tblsCreated)
			{
				m_TableStyle.MappingName=this.MappingName;
				m_TableStyle.GridColumnStyles.Clear();
				m_TableStyle.GridColumnStyles.AddRange (this.gridColumns); 
				if(this.tblSelected > -1 && tblCount>0)
				{
					this.cols.Clear();
					this.cols.AddRange(this.gridColumns);
                  	m_TableStyle.AllowSorting=tbls[tblSelected].AllowSorting;
					m_TableStyle.ColumnHeadersVisible=tbls[tblSelected].ColumnHeadersVisible;
					m_TableStyle.RowHeadersVisible=tbls[tblSelected].RowHeadersVisible;
					m_TableStyle.ReadOnly=tbls[tblSelected].ReadOnly;
				}

				this.mGrid.TableStyles.Clear();
				this.mGrid.TableStyles.Add (m_TableStyle) ;
				this.mGrid.Initilaize=true;
				tblsCreated=true;
				if(this.AutoAdjust)
				    AdjustColumns();
			}
		}

		internal protected void ResetColumn(bool forces)
		{
			if(this.cols.Count==0)
			{
				MsgBox.ShowWarning("GridColumns not found ");
				return;
			}
			if(forces)
			{
			  CreateColumnsVisible(this.cols);
			}
			if(this.gridColumnsCount==0)
			{
              throw new Exception("Invalid GridColumns ");
			}

			m_TableStyle.GridColumnStyles.Clear();
			m_TableStyle.GridColumnStyles.AddRange (this.gridColumns); 

			this.mGrid.TableStyles.Clear();
			this.mGrid.TableStyles.Add (m_TableStyle) ;
			this.mGrid.Initilaize=true;
			if(this.AutoAdjust)
				AdjustColumns();

		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		internal protected DataGridTableStyle GridTableStyle 
		{
			get
			{
				//if(this.tbls==null)
				//	this.tbls=new TableCollection ();
				if(m_TableStyle ==null)
				{
					m_TableStyle=new DataGridTableStyle();
					//m_GridTableStyle=new GridTableStyle (); 
					//m_GridTableStyle.DataGrid =(DataGrid)this.mGrid; 
				}
				//if(IsHandleCreated)
				//{
				//	if(this.tbls.Count ==0 )
				//		this.tbls.Add (m_GridTableStyle); 
				//}
				return m_TableStyle;
			}
		}


		internal bool SetMaxWidth(int value)
		{
			if(value<Grid.DefaultGridWidth || value>Grid.MaxGridWidth)
				return false;
			m_MaxWidth=value; 
			return true;
		}
		#endregion

		#region IStyleCtl

		protected IStyle							m_StylePainter;
		internal GridStyleLayout					gridLayout;
		//private  mControl.WinCtl.Controls.Styles	m_StylePlan;

		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Grid;}
		}

		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
		public IStyle  StylePainter
		{
			get {return m_StylePainter;}
			set 
			{
				if(m_StylePainter!=value)
				{
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					m_StylePainter = value;
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					OnStylePainterChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		private IStyleGrid Painter
		{
			get{return m_StylePainter as IStyleGrid;}
		}

		[Browsable(false)]
		public IStyleGrid GridLayout
		{
			get
			{
				return gridLayout as IStyleGrid;
			}
		}

		[Browsable(false)]
		public IStyleLayout CtlStyleLayout
		{
			get
			{
//				if(m_StylePainter!=null)
//					return m_StylePainter.Layout as IStyleLayout;
//				else
//					return StyleControl.Layout as IStyleLayout;

				return gridLayout as IStyleLayout;
			}
		}

		public virtual void SetStyleLayout(StyleLayout value)
		{
			//this.GridContextMenu.SetStyleLayout(value);
			gridLayout.SetStyleLayout(value);
	  		//-this.BorderColor=gridLayout.BorderColor;
			//-this.BorderHotColor=gridLayout.BorderHotColor;
			//this.CaptionBackColor=gridLayout.CaptionBackColor;
			//this.CaptionForeColor=gridLayout.ColorBrush2;
			this.AlternatingBackColor=gridLayout.AlternatingColor ;
			this.GridBackColor=gridLayout.BackColor;
			this.GridForeColor=gridLayout.ForeColor;
			this.HeaderBackColor =gridLayout.HeaderBackColor;//.BorderColor;
			this.HeaderForeColor=gridLayout.HeaderForeColor;
			this.SelectionBackColor=gridLayout.SelectionBackColor;
			this.SelectionForeColor=gridLayout.SelectionForeColor;
		}

		public virtual void SetStyleLayout(Styles value)
		{
			//m_StylePlan=value;
			gridLayout.StylePlan=value;
			SetStyleLayout(gridLayout);
			if(this.IsStatusBarVisible)
			  this.m_StatusBar.Invalidate();
			this.m_Caption.Invalidate();
           //GridLayout.StylePlan=value;
			SetStyleLayout(GridLayout.Layout);
		}

		protected virtual void OnStylePainterChanged(EventArgs e)
		{
			if(m_StylePainter!=null)
				SetStyleLayout(m_StylePainter.Layout);
			else
				SetStyleLayout(StyleControl.Layout);
			if(m_Caption.GridContextMenu!=null)
			  m_Caption.GridContextMenu.StylePainter=m_StylePainter;
			this.m_Caption.StylePainter=m_StylePainter;
			if(this.IsStatusBarVisible)
				this.m_StatusBar.StylePainter=m_StylePainter;
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			switch(e.PropertyName)
			{
				case"StylePlan":
					this.SetStyleLayout(m_StylePainter.StylePlan);
					break;
				case"StyleLayout":
					this.SetStyleLayout(m_StylePainter.Layout);
					break;
				case "BorderColor":
					//-this.BorderColor=Painter.BorderColor;
					this.HeaderBackColor =Painter.HeaderBackColor;//.BorderColor;
					break;
				case"BorderHotColor":
					//-this.BorderHotColor=Painter.BorderHotColor;
					break;
//				case"CaptionColor":
//				case"CaptionBackColor":
//					this.CaptionBackColor=Painter.CaptionBackColor;
//					break;
//				case"ColorBrush2":
//				case"CaptionForeColor":
//					this.CaptionForeColor=Painter.ColorBrush2;
//					break;
				case"ForeColor":
					this.ForeColor=Painter.ForeColor;
					break;
				case"BackColor":
					this.BackColor=Painter.BackColor;
					this.HeaderForeColor=Painter.BackColor;
					break;
				case "AlternatingColor":
					this.AlternatingBackColor=Painter.AlternatingColor;
					break;
			}
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}



		#endregion

		#region IEditableObject Members

		public void EndEdit()
		{
			mGrid.EndEdit();
		}

		public void CancelEdit()
		{
			mGrid.CancelEdit();
		}

		public void BeginEdit()
		{
			mGrid.BeginEdit();
		}

		#endregion

		#region Private GridEvents

		private void mGrid_SizeChanged(object sender, EventArgs e)
		{
			OnSizeChanged(e);
		}

		private void mGrid_CurrentCellChanged(object sender, EventArgs e)
		{
			OnCurrentCellChanged(e);
		}

		protected void mGrid_ColumnResize(object sender,GridStyle.ColumnResizeEventArgs e)
		{
			OnColumnResize(e);
		}
		private void mGrid_ReadOnlyChanged(object sender, EventArgs e)
		{
			OnReadOnlyChanged(e);
		}

		private void mGrid_DataSourceChanged(object sender, EventArgs e)
		{
			OnDataSourceChanged(e);
		}

		private void mGrid_FlatModeChanged(object sender, EventArgs e)
		{
			OnFlatModeChanged(e);
		}

		private void mGrid_Navigate(object sender, NavigateEventArgs ne)
		{
			OnNavigate(ne);
		}

		private void mGrid_BorderStyleChanged(object sender, EventArgs e)
		{
			OnBorderStyleChanged(e);
		}

		private void mGrid_DoubleClick(object sender, System.EventArgs e)
		{
			this.OnDoubleClick(e);
		}

		private void mGrid_Click(object sender, System.EventArgs e)
		{
			this.OnClick(e);
		}

		private void mGrid_Enter(object sender, System.EventArgs e)
		{
			this.OnEnter(e);
		}

		private void mGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.OnMouseDown(e);
		}

		private void mGrid_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.OnMouseUp(e);
		}

		private void mGrid_Leave(object sender, System.EventArgs e)
		{
			this.OnLeave(e);
		}

		private void mGrid_MouseEnter(object sender, System.EventArgs e)
		{
			this.OnMouseEnter(e);
		}

		private void mGrid_MouseLeave(object sender, System.EventArgs e)
		{
			this.OnMouseLeave(e);
		}

		private void mGrid_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.OnKeyUp(e);
		}

		private void mGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.OnKeyPress(e);
		}

		private void mGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.OnKeyDown(e);
		}


		#endregion

		#region ISupportInitialize Members

		private bool initialising=true;

		public void BeginInit()
		{
			this.initialising = true;
			this.mGrid.BeginInit();
		}

		public void EndInit()
		{
			this.initialising = false;
			SetDataConnection(DataSource,DataMember);
			this.mGrid.EndInit();
		}

		public void Init(object dataSource,string dataMember,string mappingName)
		{
			if(mappingName ==null || mappingName=="")
			{
				throw new ArgumentException("Invalid MappingName");
			}

			BeginInit();
            this.DataSource=dataSource;
			this.DataMember=dataMember;
			this.MappingName=mappingName;
			EndInit();
		}


//		public void EndInit()
//		{
//			this.inInit = false;
//			if ((this.myGridTable == null) && (this.ListManager != null))
//			{
//				this.SetDataGridTable(this.TableStyles[this.ListManager.GetListName()], true);
//			}
//			if (this.myGridTable != null)
//			{
//				this.myGridTable.DataGrid = this;
//			}
//
//		}
		#endregion
	}
}

