using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;

using mControl.Collections;
using mControl.WinCtl.Controls;
using mControl.GridStyle.Columns;  

namespace mControl.GridStyle 
{
	public enum SelectionTypes
	{
       Cell=0,
       FullRow=1 
	}

	public enum AggregateMode
	{
		None=0,
		Sum=1,
		Avg=2,
		Max=3,
		Min=4
	}


//	public struct ColumnHeader
//	{
//		public string MappingName;
//		public string HeaderText;
//		public Type   DataType;
//		public int	  Width;	
//	}

	//[Designer("System.Windows.Forms.Design.DataGridDesigner, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultProperty("DataSource"), DefaultEvent("Navigate")]

	//[Designer(typeof(TableDesigner))]
	[ToolboxItem(false)]
	internal class CtlGrid : DataGrid ,IEditableObject 

	{

		#region Members for cursor bound
		private const int MinColWidth = 10;
		private const int MinRowHeight = 10;
	
		private bool m_columnResize = false;
		private bool m_rowResize = false;
		private int m_columnLeft = 0;
		private int m_rowTop = 0;
		private int m_columnIndex = -1;
		private int m_rowIndex = -1;
		private bool m_ColumnCell = false;
		private bool m_ColumnHeader=false;

		//protected mControl.WinCtl.Controls.Styles m_style;
		//protected mControl.WinCtl.Controls.StyleLayout  m_LayoutColors;

		#endregion

		#region Members

		private Grid mGrid;
		//private PopUpContext m_PopUpContext;
        private SelectionTypes m_SelectionType=SelectionTypes.Cell;

		internal static Rectangle m_FocusBounds;
        internal bool Initilaize;  

		#region Events
		//public delegate void ColumnClickHandler(object sender, ClickEvent e);
		//public delegate void ButtonClickHandler(object sender, ButtonClickEvent e);
		//public delegate void LinkClickHandler(object sender, LinkClickEvent  e);
		//public delegate void CellClickHandler(object sender, CellClickEvent  e);

		//public event ColumnClickHandler CellClick;
		//public event CellClickHandler CellClick;
		//public event ButtonClickHandler ButtonClick;
		//public event LinkClickHandler LinkClick;
		public event ColumnResizeHandler ColumnResize;
		public event RowBeforeUpdateHandler RowBeforeUpdate;
		
		#endregion

		#endregion

		#region Contructor

		public CtlGrid() : base() 
		{
			// put a stop to the flickering nonsense
			this.SetStyle( ControlStyles.DoubleBuffer, true );
			this.SetStyle( ControlStyles.ResizeRedraw , true );
		
			//m_PopUpContext = new PopUpContext();
			base.PreferredRowHeight =16;
			base.PreferredColumnWidth =75;
			base.BorderStyle=BorderStyle.None;
			base.CaptionVisible=false;
			base.ParentRowsVisible=false;
			base.ParentRowsLabelStyle=DataGridParentRowsLabelStyle.None;

			Initilaize=false;

		}

		public CtlGrid(Grid grid) : base() 
		{
            mGrid=grid;
			// put a stop to the flickering nonsense
			this.SetStyle( ControlStyles.DoubleBuffer, true );
			this.SetStyle( ControlStyles.ResizeRedraw , true );
			
			//m_PopUpContext = new PopUpContext();
			//mdHeader.InitManagerProperty();
			base.PreferredRowHeight =16;
			base.PreferredColumnWidth =75;
			Initilaize=false;
			//m_LayoutColors=new mControl.WinCtl.Controls.StyleLayout ();
            //m_LayoutColors.SetStyleColors (mControl.WinCtl.Controls.Styles.SteelBlue);

		}

		#endregion

		#region Property

		[Browsable(false)]
		internal IStyleGrid GridLayout
		{
			get
			{
				return mGrid.GridLayout as IStyleGrid;
			}
		}
		
		internal IGrid  interanlGrid 
		{
			get{return mGrid as IGrid;}
			set{mGrid = value as Grid;}
		}
		
		
		public SelectionTypes  SelectionType 
		{
			get{return m_SelectionType;}
			set{m_SelectionType = value;}
		}

		internal int  ActiveColumnIndex 
		{
			get{return m_columnIndex;}
		}

		internal Size VScrollBar()
		{
          return this.VertScrollBar.Size;
		}
		internal Size HScrollBar()
		{
			return this.HorizScrollBar.Size;
		}


//		public new bool CaptionVisible
//		{
//			get{return mGrid.CaptionVisible;}
//			set{mGrid.CaptionVisible=value;}
//		}
//
//		public new string CaptionText
//		{
//			get{return mGrid.CaptionText;}
//			set{mGrid.CaptionText=value;}
//		}


		//protected TableStyleCollection mTableStyles=new TableStyleCollection();	
		/*protected GridStyle.TableCollection mTableStyles=new GridStyle.TableCollection();	

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new  GridStyle.TableCollection TableStyles
		{
			get { return mTableStyles; }
		}*/

       
		[Browsable(false)] 
		public DataGridTableStyle TableStyle 
		{
			get 
			{
				// use reflection to figure out which table style is being used.
				FieldInfo myGridTableInfo = this.GetType().GetField( "myGridTable", BindingFlags.NonPublic | BindingFlags.Instance );
				return (DataGridTableStyle) myGridTableInfo.GetValue( this );
			}
		}
        
//		public PopUpContext PopUpMenu 
//		{
//			get{
//				//if ((m_PopUpContext == null)) 
//				//	m_PopUpContext = new PopUpContext();
//				return m_PopUpContext;
//			   }
//		}
        
		//public enValidType ValidType 
		//{
		//	get{return mValidType;}
		//	set{mValidType = value;}
		//}
        

//		[Category("Style")]
//		public  mControl.WinCtl.Controls.Styles Style
//		{
//			get {return m_style;}
//			set
//			{
//				if( m_style !=value)
//				{
//					m_style =value;
//					m_LayoutColors.SetStyleColors (value); 
//					this.Invalidate(); 
//				}
//			} 
//		}
//
//		[Category("Style")]
//		public  mControl.WinCtl.Controls.StyleLayout  LayoutColors
//		{
//			get	{return m_LayoutColors;}
//		}
//
//		public void SetStyleLayout(mControl.WinCtl.Controls.StyleLayout value)
//		{
//			this.m_LayoutColors =value;
//			this.m_style=value.Style;
//			this.Invalidate(); 
//		}

		#endregion

		#region Mouse Events

		protected override void OnClick(EventArgs e)
		{
		    if(!Initilaize)
				return;

			base.OnClick (e);
			if(m_SelectionType==SelectionTypes.FullRow)
				this.Select (CurrentCell.RowNumber);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if(!Initilaize)
				return;
			base.OnKeyPress (e);
			((GridColumnStyle)this.TableStyle.GridColumnStyles[this.CurrentCell.ColumnNumber]).KeyPress (this.CurrentRowIndex , (System.Windows.Forms.Keys )e.KeyChar );  
		}

		protected override void OnMouseMove(MouseEventArgs e) 
		{
			if(!Initilaize)
				return;

			base.OnMouseMove( e );

			HitTestInfo hti = this.HitTest( e.X, e.Y );

			/*if ( hti.Column != -1 && m_columnResize ) 
			{
				
				if ( e.X >= ( m_columnLeft + m_preferredWidth ) ) 
				{
					base.OnMouseMove( e );
				}

			} 
			else if ( hti.Row != -1 && m_rowResize ) 
			{
				
				if ( e.Y >= ( m_rowTop + m_preferredHeight ) ) 
				{
					base.OnMouseMove( e );
				}

			} 
			else 
			{
				base.OnMouseMove( e );
			}*/

			if (((hti.Column != -1) 
				&& (hti.Type == DataGrid.HitTestType.Cell))) 
			{
				if ((this.TableStyle.GridColumnStyles[hti.Column] is GridLinkColumn)) 
				{
					Cursor.Current = System.Windows.Forms.Cursors.Hand;
				}
			}
 

		}

		protected override void OnMouseDown(MouseEventArgs e) 
		{
			if(!Initilaize)
				return;
			base.OnMouseDown (e);
			
			try
			{
	
				//DataGridTableStyle ts = this.TableStyle;
				HitTestInfo hti = this.HitTest( e.X, e.Y );

				if (e.Button == MouseButtons.Right) 
				{
					m_ColumnCell=false;
					m_rowResize = false;
					m_columnResize = false;
					m_FocusBounds=Rectangle.Empty;
					m_columnIndex = hti.Column;
                    
					if ( hti.Type == HitTestType.ColumnHeader && hti.Column != -1 )
					{
						m_ColumnHeader=true;
						m_columnLeft =GetColumnLeft(m_columnIndex);
					}
					else
					{
						m_ColumnHeader=false;
					}
					//if(!(m_PopUpContext == null)) 
					// m_PopUpContext.ShowPopUp(this, new Point(e.X, e.Y),this.TableStyle );
					return; 
				}
				m_ColumnHeader=false;
				
				if (((hti.Column != -1) 
					&& (hti.Type == DataGrid.HitTestType.Cell))) 
				{
					m_ColumnCell=true;
					m_rowResize = false;
					m_columnResize = false;
					m_columnIndex = hti.Column;
					m_rowIndex = hti.Row;
					 
					Rectangle cursorRect = new Rectangle(e.X, e.Y, 1, 1);
					Rectangle cellBounds = this.GetCurrentCellBounds();
					if (cursorRect.IntersectsWith(cellBounds)) 
					{
						m_FocusBounds = cellBounds;
					}
					//DataGridTableStyle ts = this.TableStyle;
					//((GridColumnStyle)ts.GridColumnStyles[m_columnIndex]).MouseDown (this.CurrentRowIndex , e.X, e.Y);  
					((GridColumnStyle)this.TableStyle.GridColumnStyles[m_columnIndex]).MouseDown (this.CurrentRowIndex , e.X, e.Y);  
		
				}
				else if ( hti.Type == HitTestType.ColumnResize && hti.Column != -1 ) 
				{
				
					m_ColumnCell=false;
					m_rowResize = false;
					m_columnResize = true;
					m_columnIndex = hti.Column;
					//m_columnLeft = GetColumnLeft( hti.Column );
					m_columnLeft =e.X-this.TableStyle.GridColumnStyles[ m_columnIndex ].Width;//  GetColumnLeft( hti.Column );
					//m_preferredWidth = ( ( GridColumnStyle )this.TableStyle.GridColumnStyles[ hti.Column ] ).MinimumWidth;
					m_FocusBounds=Rectangle.Empty; 
				} 
				else if ( hti.Type == HitTestType.RowResize && hti.Row != -1 ) 
				{
				
					m_ColumnCell=false;
					m_columnResize = false;
					m_rowResize = true;
					m_rowTop = GetRowTop( hti.Row );
					m_rowIndex = hti.Row;
					//m_preferredHeight = CalculatePreferredHeight();
					m_FocusBounds=Rectangle.Empty; 

				} 
				else 
				{
				
					m_ColumnCell=false;
					m_columnResize = false;
					m_rowResize = false;
					m_FocusBounds=Rectangle.Empty; 

				}
						
			}
			catch(Exception ex)
			{
              Console.WriteLine(ex.Message); 
			}
		}

		protected override void OnMouseUp(MouseEventArgs e) 
		{
			if(!Initilaize)
				return;
	
			base.OnMouseUp (e);
			
			try
			{

				if (e.Button == MouseButtons.Right &&  m_ColumnHeader && m_columnIndex != -1)
				{
					if(!mGrid.AllowChangeColumnMapping)
						return;
					
					int x =m_columnLeft<0?0:m_columnLeft;
					int y =Grid.DefaultColumnHeaderHeight;

					int width= TableStyle.GridColumnStyles[m_columnIndex].Width;
					if((m_columnLeft + width)>this.Width)
						width= width-((m_columnLeft + width)-this.Width);
                    else if(m_columnLeft<0)
						width= width+m_columnLeft;
					mGrid.ShowColumnMenu(new Point(x,y),width);
				
					//if(!(m_PopUpContext == null)) 
					// m_PopUpContext.ShowPopUp(this, new Point(e.X, e.Y),this.TableStyle );
					return; 
				}

				//DataGridTableStyle ts = this.TableStyle;
				//HitTestInfo hti = this.HitTest( e.X, e.Y );
			
				//if (((hti.Column != -1) 
				//	&& (hti.Type == DataGrid.HitTestType.Cell))) 
				if(m_ColumnCell &&	m_columnIndex != -1 && m_rowIndex != -1)
				{
					/*Rectangle cellBounds = this.GetCurrentCellBounds();
					if ((cellBounds.Equals(m_FocusBounds) 
						&& !m_FocusBounds.Equals(Rectangle.Empty))) 
					{
						Rectangle cursorRect = new Rectangle(e.X, e.Y, 1, 1);
						if (cursorRect.IntersectsWith(m_FocusBounds)) 
						{
							if(this.TableStyle.GridColumnStyles[hti.Column].ReadOnly )
							  return;
							//ClickColumnEvent = new ClickEvent(this.TableStyle, hti.Row, hti.Column);
							if((LinkClick!=null)||(ButtonClick!=null)||(CellClick!=null) )
								GridEvents(hti);
						}
					}*/
		
					//m_FocusBounds = Rectangle.Empty;
					this.Invalidate(this.GetCurrentCellBounds());
				}

				else if ( m_columnResize ) 
				{
					if ( e.X < ( m_columnLeft + MinColWidth))//m_preferredWidth ) ) 
					{
						this.TableStyle.GridColumnStyles[ m_columnIndex ].Width =MinColWidth;//  m_preferredWidth;
					}

					m_columnResize = false;
					m_FocusBounds = Rectangle.Empty;
					ColumnResize(this, new ColumnResizeEventArgs (this.TableStyle.GridColumnStyles[ m_columnIndex ].Width,m_columnIndex));
			
				} 
				else if ( m_rowResize ) 
				{

					if ( e.Y < ( m_rowTop + MinRowHeight))//  m_preferredHeight ) ) 
					{
						SetRowHeight( m_rowIndex,MinRowHeight );// m_preferredHeight );	
					}

					m_rowResize = false;
					m_FocusBounds = Rectangle.Empty;
				}
		
			}
			catch{}
		}

		/*private void GridEvents(HitTestInfo hti)
		{
			//ClickColumnEvent = new ClickEvent(this.TableStyle, hti.Row, hti.Column);
							
			if(this.TableStyle.GridColumnStyles[hti.Column] is GridLinkColumn )
			{
				if(LinkClick!=null) 
				{
					LinkClickEvent  LinkColumnEvent = new LinkClickEvent (this.TableStyle, hti.Row, hti.Column);
					LinkClick(this.TableStyle.GridColumnStyles[hti.Column], LinkColumnEvent);
				}
			}
			else if(this.TableStyle.GridColumnStyles[hti.Column] is  GridButtonColumn  )
			{
				if(ButtonClick!=null) 
				{
					ButtonClickEvent ButtonColumnEvent = new ButtonClickEvent (this.TableStyle, hti.Row, hti.Column);
					ButtonClick(this.TableStyle.GridColumnStyles[hti.Column], ButtonColumnEvent);
				}
			}
			else
			{
				if(CellClick!=null) 
				{
					CellClickEvent CellColumnEvent = new CellClickEvent (this.TableStyle, hti.Row, hti.Column);
					CellClick(this.TableStyle.GridColumnStyles[hti.Column], CellColumnEvent);
				}
			}

		}*/

		#endregion

		#region Fileds

//		internal void GetFileds()
//		{
//           //for(int i=0
//		}

		#endregion

		#region RowEvents

		private bool IsRowEdit=false;
		private object[] dataRowItems=null;
        private int rowPosition=0;
		private CurrencyManager cm;


		private void CM_PositionChanged(object sender, EventArgs e)
		{
			if(CM.Position != rowPosition && IsRowEdit)
			{
				if(RowBeforeUpdate!=null)
				{
					RowUpdateEvent ev=new RowUpdateEvent (CurrentRowIndex);
					RowBeforeUpdate(this,ev);
					if(ev.RowState == GridStyle.UpdateState.Rollback )
						this.CancelEdit ();
					else if(ev.RowState == GridStyle.UpdateState.Cancel )
						this.CM.Position=rowPosition;
					else
						this.EndEdit();
					return;
				}
			}
				rowPosition=CM.Position; 
		}

		protected override void OnCurrentCellChanged(EventArgs e) 
		{
			base.OnCurrentCellChanged (e);
			
			if(m_SelectionType==SelectionTypes.FullRow)
			   this.Select (CurrentCell.RowNumber);
            
		}
	
		
		public void InitBufer()
		{
			if(dataRowItems==null)
			{
				int lngth=this.DataList[CM.Position].Row.ItemArray.Length ;
				dataRowItems=new object[lngth];
			}
		}

		private void FillBufer()
		{
			object []d=this.DataList[CM.Position].Row.ItemArray;
			if(dataRowItems==null)
			   dataRowItems=new object[d.Length];
  
			for(int i =0;i< d.Length ;i++ )
			{
				dataRowItems.SetValue (d[i],i);
			}
		}

		#endregion

		#region internal Methods

		internal new void ColumnStartedEditing(Control editingControl)
		{
			base.ColumnStartedEditing(editingControl.Bounds);
		}

		internal new void ColumnStartedEditing(System.Drawing.Rectangle bounds)
		{
			base.ColumnStartedEditing(bounds);
		}

		internal void ControlOnMouseWheel(MouseEventArgs e)
		{
			this.OnMouseWheel(e);
		}

		internal protected void SetDataGridInColumnInternal()
		{
			foreach(DataGridColumnStyle c in this.TableStyle.GridColumnStyles )
			{
				if(c is IGridColumnStyle)
				((GridColumnStyle)c).SetDataGridInColumnInternal((DataGrid)this,true);
			}
		}

//		internal protected void SetGridColumnsInternal()
//		{
//			if(this.TableStyles 
//			foreach(GridColumnStyle c in this.TableStyle.GridColumnStyles )
//			{
//				c.SetDataGridInColumnInternal(this);
//			}
//		}

		#endregion

		#region IEditableObject Members

		public void EndEdit()
		{
			IsRowEdit=false;
			this.CM.PositionChanged -=new EventHandler(CM_PositionChanged);  
		}

		public void CancelEdit()
		{
			if(IsRowEdit)
			{
				for(int i =0;i< dataRowItems.Length ;i++ )
				{
					//this[CurrentCellRow,i]=dataRowItems[i];
					this.DataList.Table.Rows[rowPosition][i]=dataRowItems[i];
				
				}
			}
			IsRowEdit =false;  
		}

		public void BeginEdit()
		{
			if(!IsRowEdit)
			{
				rowPosition=CM.Position; 
				this.CM.PositionChanged +=new EventHandler(CM_PositionChanged);  
				FillBufer();
				IsRowEdit =true;  
			}
		}

		#endregion

		#region Methods

		// Since a row is composed of several different column styles, 
		// we need to enumerate all of the column styles in the collection 
		// and find the largest minimum height.
		private int CalculatePreferredHeight() 
		{
			
			DataGridTableStyle ts = this.TableStyle;
			int maxHeight = 0;

			foreach( GridColumnStyle cs in ts.GridColumnStyles ) 
			{
				maxHeight = Math.Max( maxHeight, cs.MinimumHeight );
			}

			return maxHeight;

		}

		private int GetRowTop( int rowNum ) 
		{
			
			DataGrid dg = new DataGrid();
			MethodInfo dgMethod = dg.GetType().GetMethod( "GetRowTop", BindingFlags.Instance | BindingFlags.NonPublic );//, BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static );
			return ( int ) dgMethod.Invoke( this, new object[] { rowNum } );

		}

		private void SetRowHeight( int rowIndex, int height ) 
		{

			DataGrid dg = new DataGrid();
			PropertyInfo dgRowsInfo = dg.GetType().GetProperty( "DataGridRows", BindingFlags.Instance | BindingFlags.NonPublic );//, BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static );
			object[] rows = ( object[] ) dgRowsInfo.GetValue( this, null );
			PropertyInfo dgRowHeightInfo = rows[ rowIndex ].GetType().GetProperty( "Height", BindingFlags.Instance | BindingFlags.Public );

			dgRowHeightInfo.SetValue( rows[rowIndex], height, null );

		}

		private int GetColumnLeft( int columnNum ) 
		{
			
			DataGridTableStyle ts = TableStyle;
			int columnLeft = ( ts.RowHeadersVisible ) ? ts.RowHeaderWidth : 0;

			for ( int i=0; i < columnNum; i++ ) 
			{
				columnLeft += ts.GridColumnStyles[ i ].Width;
			}
			int scrallH=this.HorizScrollBar.Value;
			int res= columnLeft-scrallH;
			return res;//<0 ? 0:res;

		}

		protected override void OnDataSourceChanged(EventArgs e)
		{
			base.OnDataSourceChanged (e);
			this.cm = null;
		}


		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Description("DataGridListManager")]
		internal CurrencyManager CM
		{
			get
			{
				if (((this.cm == null) && (this.BindingContext != null)) && (this.DataSource != null))
				{
					cm= (CurrencyManager) this.BindingContext[this.DataSource, this.DataMember];
				}
				return this.cm;
			}
			set
			{
				throw new NotSupportedException("DataGridSetListManager");
			}
		}

		[Browsable(false)]
		internal System.Data.DataView DataList 
		{
			get
			{
				if(DataSource!=null)
				{
					try
					{
						if(CM==null)
							return null;
						return (System.Data.DataView)CM.List;
					}
					catch(Exception ex)
					{
						throw  ex;
						//throw new Exception ("Data Source type not supported");	
					}
				}
				else
					return null;
			}
      
		}

//		[Browsable(false)]
//		public CurrencyManager CM 
//		{
//			get
//			{
//				if(DataSource ==null)
//				{
//                  return null;
//				}
//				if(this.BindingContext ==null)
//						this.BindingContext=  new BindingContext ();
//				if(DataMember!=null)//DataSource
//				{
//					return ((CurrencyManager)(this.BindingContext[DataSource,DataMember]));
//				}
//				else
//					return ((CurrencyManager)(this.BindingContext[DataSource]));
//				    //return ((CurrencyManager)(this.DataSource));
//			}
//		 
//		}      

//		[Browsable(false)]
//		public System.Data.DataView DataList 
//		{
//			get
//			{
//				if(DataSource!=null)
//				{
//					try
//					{
//						//return (System.Data.DataView)((System.Data.DataSet)CM.List).Tables[this.DataMember].DefaultView;
//						if(DataSource is System.Data.DataSet)
//						{
//							if(this.DataMember.Length >0) 
//								return (System.Data.DataView)((System.Data.DataSet)DataSource).Tables[this.DataMember].DefaultView;
//							else
//								return (System.Data.DataView)((System.Data.DataSet)DataSource).Tables[this.TableStyle.MappingName].DefaultView;
//						}
//						if(DataSource is System.Data.DataViewManager)
//						{
//							string mapName=this.mGrid.MappingName;
//							return (System.Data.DataView)((System.Data.DataViewManager)DataSource).DataSet.Tables[mapName].DefaultView;
//						}
//						if(DataSource is System.Data.DataView)
//						{
//							return (System.Data.DataView) this.DataSource;
//							//return ((System.Data.DataView)(CM.List));
//						}
//						if(DataSource is System.Data.DataTable)
//						{
//							return (System.Data.DataView) ((System.Data.DataTable)this.DataSource).DefaultView;
//							//return (System.Data.DataView)((System.Data.DataTable)CM.List).DefaultView;
//						}
//						throw new Exception ("Data Source type not supported");	
//					}
//					catch(Exception ex)
//					{
//						throw  ex;
//					}
//				}
//				else
//					return null;
//			}
//		}

		public int GetColIndex(string colName) 
		{
			try 
			{
				return DataList.Table.Columns[colName].Ordinal;
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}
 
        
		#endregion

		#region Overrides

		protected override void OnPaint(PaintEventArgs e) 
		{
			base.OnPaint (e);
		}


		protected override void OnFontChanged(System.EventArgs e) 
		{
			base.OnFontChanged (e);

			//SetRowHeight( 0,this.FontHeight  );	
			//this.TableStyle.PreferredRowHeight=this.Font.Height;//  FontHeight+4;   
		}
		#endregion

	}


}