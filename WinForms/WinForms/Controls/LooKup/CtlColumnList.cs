using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.ComponentModel;

using System.Collections;
using System.Security;
using System.Security.Permissions;
using System.Globalization;
using System.Data;
using System.Reflection;


using Nistec.Win32;
using Nistec.Data;

using Nistec.Printing;
using Nistec.Printing.Data;
using Nistec.Win;
  
namespace Nistec.WinForms
{

	public enum LookupFilterMode
	{
		None,
		BoundControl,
		Manual
	}

	public interface IMcLookUp
	{
		void DataBind(DataView value, bool forceAutoFill);
		void DataBind(DataView value,string valueMember,string displayMember, bool forceAutoFill);
		//void DataBind(DataView value,int columnValue,int columnView, bool forceAutoFill);
		DataView DataSource{get;set;}
		bool AutoFill{get;set;}
		LookupFilterMode LookupFilterMode{get;set;}
		string RowFilter{get;set;}
		string DisplayMember{get;set;}
		string ValueMember{get;set;}
		//int ColumnView{get;set;}
		//int ColumnValue{get;set;}
		string ColumnDisplay{get;set;}
		McColumnCollection Columns{get;}
	}

	[ToolboxItem(false),Designer(typeof(Design.McDesigner))]
	public class McColumnList: McListBox ,IMcLookUp//,ILayout,IMcList,IBind
	{	

		#region Members

		private McColumnCollection m_Cols;
		private int m_ColumnSpacing=3;
		private int m_ColumnImage=-1;

		private DataView m_dv = null;
		//private int m_ColumnView = 0;
		//private int m_ColumnValue = 0;
		private bool m_Alternating=true;
		
		private int[] m_ColumnsWidth;
		internal int[] m_SortOrders;

		//disply
		private string columnDisplay="";
		private int[] m_ColWidths =null;
		private string[]m_ColCaption=null;

		private object m_SelectedValue;

		//autoFill
		private int performColumnWidth=80;
		private bool autoFill=true;
		private bool allowSort=true;
		internal int activeColumnSort=-1;


		public event EventHandler CollectionChanged;
		#endregion

		#region Constructor

		public McColumnList(): base()
		{
			ColumnViewSetting();
		}

		internal void ColumnViewSetting()
		{
			base.isColumnView=true;
			base.DrawMode=System.Windows.Forms.DrawMode.OwnerDrawFixed;
			base.MultiColumn=false;
			base.DrawItemStyle=DrawItemStyle.Default;
		}

		private void InitColumnCollection ()
		{
			if(m_Cols!=null)
			{
				m_Cols.Clear();
			}
			m_Cols=new McColumnCollection(this);
			m_Cols.CollectionChanged+=new EventHandler(m_Cols_CollectionChanged);
		}

		private void m_Cols_CollectionChanged(object sender, EventArgs e)
		{
			OnCollectionChanged(e);
		}

		#endregion

		#region override

		protected virtual void OnCollectionChanged(EventArgs e)
		{
			if(CollectionChanged!=null)
				CollectionChanged(this,e);
		}

		#endregion

		#region Data

		public void DataBind(DataView value, bool forceAutoFill)
		{
			DataBind(value,this.ValueMember,this.DisplayMember,forceAutoFill);
		}

		public void DataBind(DataView value,string valueMember,string displayMember, bool forceAutoFill)
		{
			try
			{
				this.m_dv=value;
				base.DataSource=m_dv;
				if(m_dv==null)
				{
					this.ClearInternal();
					return;
				}
				if(valueMember==null || valueMember=="" )
				{
					//throw new ArgumentException("Value member can not be null or empty ");
					valueMember=m_dv.Table.Columns[0].ColumnName;
				}
				else if(!m_dv.Table.Columns.Contains(valueMember))
				{
					throw new ArgumentException("Data source not Contains Column ", valueMember);
				}
				if(displayMember==null || displayMember=="" )
				{
					//throw new ArgumentException("Display member can not be null or empty ");
					displayMember=m_dv.Table.Columns.Count>1? m_dv.Table.Columns[1].ColumnName:m_dv.Table.Columns[0].ColumnName;
				}
				else if(!m_dv.Table.Columns.Contains(displayMember))
				{
					throw new ArgumentException("Data source not Contains Column ", displayMember);
				}

				this.ValueMember=valueMember;
				this.DisplayMember=displayMember;

				if(!DesignMode && this.m_dv!=null )
				{
					if(autoFill || forceAutoFill)
						CreateDynamicColumns();
					else if(this.Columns.Count==0 )
					{
						autoFill=true;
						CreateDynamicColumns();
					}
					else
					{
						SetColumnsIndex();
						SetColumnsWidth();
					}
	
				}
				OnDataSourceChanged(EventArgs.Empty);
				this.Invalidate();
			}
			catch (Exception ex)
			{
				throw ex;
			}		
		}

		internal void ClearInternal()
		{
			this.Items.Clear();
			SelectedIndex = -1;
		}


//		public void DataBind(DataView value, bool forceAutoFill)
//		{
//		  DataBind(value,this.ColumnValue,this.ColumnView,forceAutoFill);
//		}
//
//		public void DataBind(DataView value,int columnValue,int columnView, bool forceAutoFill)
//		{
//			try
//			{
//				this.m_dv=value;
//				base.DataSource=value;
//				this.ColumnValue=columnValue;
//				this.ColumnView=columnView;
//
//				if(!DesignMode && this.m_dv!=null )
//				{
//					if(autoFill || forceAutoFill)
//						CreateDynamicColumns();
//					else if(this.Columns.Count==0 )
//					{
//						autoFill=true;
//						CreateDynamicColumns();
//					}
//					else
//					{
//						SetColumnsIndex();
//						SetColumnsWidth();
//					}
//					base.ValueMember=this.m_Cols[m_ColumnValue].ColumnName;
//				}
//				OnDataSourceChanged(EventArgs.Empty);
//				this.Invalidate();
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}
//		}


		[DefaultValue(null), Category("Data"), Description("ListControlDataSource"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
		public new DataView DataSource
		{
			get{ return this.m_dv; }
			set
			{ 

				//bool forceAutoFill=m_dv!=null && this.Columns.Count>0 && !autoFill;
				
				if (m_dv != value)
				{
					DataBind(value, false);
				}
			}
		}

		#endregion

		#region Auto Fill

		[Category("Columns"),DefaultValue(true) ]
		public bool AutoFill
		{
			get{return this.autoFill;}
			set{this.autoFill=value;}
		}

		[Category("Columns"),DefaultValue(true) ]
		public bool AllowSort
		{
			get{return this.allowSort;}
			set{this.allowSort=value;}
		}


		[Category("Columns"),DefaultValue(80) ]
		public int PerformColumnWidth
		{
			get{return this.performColumnWidth;}
			set
			{
				if(value >0 && value <=this.Width)
				{
					this.performColumnWidth=value;
				}
			}
		}

		private void  CreateDynamicColumns()
		{
			if(this.m_dv!=null)
			{
				InitColumnCollection ();
				DataTable dt=this.m_dv.Table;
				//this.Columns.Clear();	
				int colCount=dt.Columns.Count;
				int scroallWidth=0;
				if(this.ScrollAlwaysVisible)
				{
					scroallWidth=20;
				}
				else if((dt.Rows.Count*ItemHeight)>this.Height)
				{
					scroallWidth=20;
				}
				int colWidth=performColumnWidth;
				int sumColWidth= (colCount*performColumnWidth)-scroallWidth;
				
				
				int orderDisplyType=0;
				LookupColumnDisply lcd=new LookupColumnDisply();
				if(this.columnDisplay!="")
				{
					orderDisplyType=lcd.GetColumnsCaptionWidth(this.columnDisplay,true);
				}
				if(orderDisplyType>0)
				{
					this.m_ColWidths=lcd.m_ColWidths;
					this.m_ColCaption=lcd.m_ColCaption;
	
					for(int i=0;i<m_ColWidths.Length;i++)
					{
						McColumn col=new McColumn();
						col.ColumnName=dt.Columns[i].ColumnName;
                        col.FieldType = GetDataType(dt.Columns[i].DataType);
						if(orderDisplyType==1)
						{
							col.Caption=dt.Columns[i].Caption!=""?dt.Columns[i].Caption:col.ColumnName;
						}
						else
						{
							col.Caption=m_ColCaption[i]!=""?m_ColCaption[i]:col.ColumnName;
						}
						col.Width=m_ColWidths[i];
						col.Display=m_ColWidths[i]>0;
						m_Cols.Add(col);	
					}
				}
				else if(this.m_ColWidths!=null)
				{

					for(int i=0;i<m_ColWidths.Length;i++)
					{
						McColumn col=new McColumn();
						col.ColumnName=dt.Columns[i].ColumnName;
                        col.FieldType = GetDataType(dt.Columns[i].DataType);
						col.Caption=dt.Columns[i].Caption!=""?dt.Columns[i].Caption:col.ColumnName;
						col.Width=m_ColWidths[i];
						col.Display=m_ColWidths[i]>0;
						m_Cols.Add(col);	
					}
				}
				else
				{
				
					if(this.Width<sumColWidth && colCount >0)
					{
						colWidth=sumColWidth/colCount;
					}
				
					foreach(DataColumn c in dt.Columns)
					{
						McColumn col=new McColumn();
						col.ColumnName=c.ColumnName;
                        col.FieldType = GetDataType(c.DataType);
						col.Caption=c.Caption!=""?c.Caption:c.ColumnName;
						col.Width=colWidth;
						col.Display=true;
						this.Columns.Add(col);	
					}
				}
				SetColumnsIndex();
				SetColumnsWidth();
			}
		}

		private FieldType GetDataType(System.Type type )
		{
			if(type.Equals(Type.GetType("System.Boolean")))
			{
				return FieldType.Bool;
			}
			return FieldType.Text;
		}

		private void SetColumnsIndex()
		{
			int i=0;
			foreach(DataColumn c in m_dv.Table.Columns)
			{
				McColumn col=FindColumn(c.ColumnName);
				if(col!=null)
					col.Ordinal=i;
				i++;
			}
		}

		private McColumn FindColumn(string name)
		{
			foreach(McColumn c in m_Cols)
			{
				if(c.ColumnName==null)
				{
					throw new Exception("Invalid Column name in " + c.ToString());
				}
				if(c.ColumnName.Equals(name))
					return c;
			}
			return null;
		}

		private void SetColumnsWidth()
		{
			int cnt=this.Columns.Count;
			if(cnt>0)
			{
				m_ColumnsWidth=null;
				m_ColumnsWidth =new int[cnt];
				for(int i=0 ;i< Columns.Count;i++)
				{
					m_ColumnsWidth.SetValue(Columns[i].Width,i);
				}
				m_SortOrders=new int[cnt];
				m_SortOrders.Initialize();
			}
		}

		public int GetActiveColumnSort()
		{
			return this.activeColumnSort;
		}
		#endregion

		#region DrawItem
	
		//Draw multiple columns
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			try
			{
				if(m_Cols==null) 
				{
					return;
					//throw new ArgumentException("Invalid Columns");
				}
				int iIndex = e.Index;
				int colCount=m_Cols.Count;
				bool rtl=(this.RightToLeft == RightToLeft.Yes);
		
				if(iIndex > -1 && colCount>0)
				{
					int iXPos = 0;
					int iYPos = 0;
					int widthAdd=0;
					bool drawImage=false;
					Rectangle rectImage = Rectangle.Empty;

					DataRow dr =  ((DataView)this.DataSource)[iIndex].Row;//  m_dv[iIndex].Row;
					//DataRow dr =  m_DataView[iIndex].Row;
					int imageIndex=-1;
			
					if(ColumnImage>-1 && ColumnImage < colCount)
					{
						imageIndex=(int) Types.ToInt(dr[ColumnImage],-1);
					}

					e.DrawBackground();

					Rectangle rectAlter=new Rectangle(e.Bounds.X,e.Bounds.Y,e.Bounds.Width,ItemHeight);
			
					if ( (e.State & DrawItemState.Selected) == DrawItemState.Selected )
					{
						using(Brush slc=LayoutManager.GetBrushCaption())
						e.Graphics.FillRectangle(slc,(RectangleF)rectAlter);
						//e.DrawFocusRectangle();
					}
					else if(iIndex%2==0 && m_Alternating)
					{
						using(Brush bck=LayoutManager.GetBrushFlat())
						{
								//e.Graphics.DrawRectangle(new Pen(Brushes.Gray),rectAlter);
								e.Graphics.FillRectangle(bck,(RectangleF)rectAlter);
						}
					}
				
					if(this.ImageList!=null && imageIndex > -1)
					{
		
						Rectangle bounds=e.Bounds;
						if ((imageIndex >= 0) && (imageIndex < ImageList.Images.Count))
						{
							//Rectangle rectImage;
							if (rtl)
								rectImage = new Rectangle(bounds.Width-ImageList.ImageSize.Width-1, bounds.Y + ((bounds.Height - ImageList.ImageSize.Height) / 2), ImageList.ImageSize.Width, ImageList.ImageSize.Height);
							else
								rectImage = new Rectangle(bounds.X , bounds.Y + ((bounds.Height - ImageList.ImageSize.Height) / 2), ImageList.ImageSize.Width, ImageList.ImageSize.Height);
		
							drawImage=true;
						}
					}

					int colWidth=0;
					Rectangle rectStr=Rectangle.Empty;
			
					for(int index = 0; index < colCount; index++) //Loop for drawing each column
					{
						colWidth=m_Cols[index].Width;
						int colIndex=m_Cols[index].Ordinal;
                        if ((colIndex < 0) || m_Cols[index].Display == false || colWidth <= ColumnSpacing)
                                continue;

						if(drawImage)
						{
							widthAdd=ImageList.ImageSize.Width + 1;
							if(colWidth > ColumnSpacing+widthAdd)
							{
								ImageList.Draw(e.Graphics, rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height, imageIndex);
							}
							else
							{
								widthAdd=0;
							}
							drawImage=false;
						}
						else
						{
							widthAdd=0;
						}
                        if (m_Cols[index].FieldType == FieldType.Bool)
						{
							bool bRes=false;
							//TODO :check parse
							bRes=bool.Parse(dr[colIndex].ToString());

							Rectangle boolRect= new Rectangle(iXPos + ((colWidth-9)/2),e.Bounds.Y+((ItemHeight-9)/2),9,9);
							
							e.Graphics.DrawRectangle(new Pen(Brushes.Gray),boolRect);
							if(bRes)
							{
								using(Brush bCheck=LayoutManager.GetBrushHot(),bSlct=LayoutManager.GetBrushSelected())
								{
									//if ( (e.State & DrawItemState.Selected) == DrawItemState.Selected )
									//	e.Graphics.FillRectangle(bSlct,boolRect.X+2,boolRect.Y+2,6,6);
									//else
										e.Graphics.FillRectangle(bCheck,boolRect.X+2,boolRect.Y+2,6,6);
								}
							}

						}
						else
						{
							using(StringFormat sf=GetStringFormat(true,rtl))
							{
								if(rtl)
									rectStr=new Rectangle(e.Bounds.Width-(iXPos+widthAdd+colWidth-2), e.Bounds.Y, colWidth-widthAdd-ColumnSpacing, ItemHeight);
								else
									rectStr=new Rectangle (iXPos+widthAdd, e.Bounds.Y, colWidth-widthAdd-ColumnSpacing, ItemHeight);
				

								if(m_Cols[index].Alignment==HorizontalAlignment.Right)
									sf.Alignment=StringAlignment.Far;
								e.Graphics.DrawString(dr[colIndex].ToString(), Font, new SolidBrush(e.ForeColor),(RectangleF)rectStr,sf);
							}
						}
							//e.Graphics.DrawString(dr[index].ToString(), Font, new SolidBrush(e.ForeColor), new RectangleF(iXPos+widthAdd, e.Bounds.Y, colWidth-widthAdd-ColumnSpacing, ItemHeight));
							iXPos += colWidth;
					}
					iXPos = 0;
					iYPos += ItemHeight;
					//e.DrawFocusRectangle();

					//base.OnDrawItem(e);
				}
			}			
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "\r\nIn McColumnList.OnDrawItem(DrawItemEventArgs).");
			}
		}

		private StringFormat GetStringFormat(bool wordWrap,bool rtl)
		{
			StringFormat format1 = new StringFormat();
			format1.Trimming = StringTrimming.EllipsisCharacter;
			format1.FormatFlags = 0;
			if (!wordWrap)
			{
				format1.FormatFlags = StringFormatFlags.NoWrap;
			}
			//format1.HotkeyPrefix = HotkeyPrefix.Show;
			if (rtl)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				format1.Alignment = StringAlignment.Near;
			}
			else
			{
				format1.Alignment = StringAlignment.Near;
			}
			return format1;
		}



		#endregion

		#region Filter

		private LookupFilterMode filterMode;
		private string filter="";
		private string controlBoundName="";
		private Control controlFilterBound=null;

		[Category("Filter"),DefaultValue(LookupFilterMode.None)]
		public LookupFilterMode LookupFilterMode
		{
			get {return this.filterMode;}
			set{this.filterMode=value;}
		}

		[Category("Filter"),DefaultValue("")]
		public string RowFilter
		{
			get {return this.filter;}
			set{this.filter=value;}
		}

		[Category("Filter"),DefaultValue("")]
		public string ControlBoundName
		{
			get {return this.controlBoundName;}
			set{this.controlBoundName=value;}
		}

		[Category("Filter"),DefaultValue(null)]
		public Control ControlFilterBound
		{
			get {return this.controlFilterBound;}
			set{this.controlFilterBound=value;}
		}


		public void SetFilterBound()
		{
			string strFilter="";
			try
			{
				if(filter.Length>0)
				{
					strFilter=this.filter;
				}
				else
				{
					if(ControlFilterBound==null)
					{
						MsgBox.ShowWarning("Invalid ControlFilterBound");
						return;
					}
					if(controlBoundName=="")
					{
						MsgBox.ShowWarning("Invalid ControlBoundName");
						return;
					}
					string val=ControlFilterBound.Text;
                    if (WinHelp.IsNumber(val))
					{
						strFilter=controlBoundName + "=" + val ;
					}
					else
					{
						strFilter=controlBoundName + "='" + val + "'";
					}
				}
				DataView dv=this.DataSource;
				dv.RowFilter=strFilter;
				this.DataSource=dv;
			}
			catch(Exception ex)
			{
				MsgBox.ShowWarning(ex.Message);
			}
		}

		#endregion

		#region ColumnDisply

		public string ColumnDisplay
		{
			get{return columnDisplay;}
			set
			{
				if(value==null || value=="")
				{
					columnDisplay="";
					return;
				}
				LookupColumnDisply lcd=new LookupColumnDisply();
				if(lcd.GetColumnsCaptionWidth(value,false)>0)//if(ParseColumnsWidth(value)!=null)
				{
					columnDisplay=value;
				}
			}
		}

//		private int SetColumnsCaptionWidth(string value,bool setArrays)
//		{
//			try
//			{
//				string[] strlist=value.Split(';');
//				int cnt=strlist.Length;
//				if(cnt==0)
//				{
//					return 0;
//				}
//				int[] results=new int[2];
//				int orderType=-1;
//				if(cnt>=2)
//				{
//					results[0]=Types.StringToInt(strlist[0],int.MinValue);
//					results[1]=Types.StringToInt(strlist[1],int.MinValue);
//
//					if(results[0]==int.MinValue && results[1]==int.MinValue)
//					{
//						throw new InvalidCastException("Unrecognized Format");
//					}
//					else if(results[0]==int.MinValue )
//					{
//						orderType=2;//Caption;Width
//					}
//					else if(results[1]==int.MinValue )
//					{
//						orderType=3;//Width;Caption
//					}
//					else
//					{
//						orderType=1;//Width;//Width
//					}
//				}
//				else
//				{
//					return -1;
//				}
//
//	
//				string[] strWidth=null;
//				string[] strCaption=null;
//				int[] intWidth=null;
//		
//				if(orderType==2)//Caption;Width
//				{
//					strCaption=GetSplitArray(value,0,1);
//					strWidth=GetSplitArray(value,1,1);
//					intWidth=ConvertArrayToInt(strWidth);
//					//SetColumns(strCaption,strCaption,intWidth);
//					if(setArrays)
//						SetColumnsInternal(strCaption,intWidth);
//				}
//		
//				else if(orderType==3)//Width;Caption
//				{
//					strWidth=GetSplitArray(value,0,1);
//					strCaption=GetSplitArray(value,1,1);
//					intWidth=ConvertArrayToInt(strWidth);
//					//SetColumns(strCaption,strCaption,intWidth);
//					if(setArrays)
//						SetColumnsInternal(strCaption,intWidth);
//				}
//				else //orderType=0;//Width;//Width
//				{
//					strWidth=GetSplitArray(value,0,0);
//					intWidth=ConvertArrayToInt(strWidth);
//					//SetColumns(intWidth);
//					if(setArrays)
//						this.m_ColWidths=intWidth;
//				}
//				return orderType;
//			}
//			catch(Exception ex)
//			{
//				MsgBox.ShowError(ex.Message);
//				return -1;
//			}
//		}
//
//		private string[] GetSplitArray(string value,int mode,int interval)
//		{
//			string[] strlist=value.Split(';');
//			int cnt=strlist.Length;
//			if(cnt==0)
//			{
//				return null;
//			}
//			string[] strRes=new string[cnt/(interval+1)];
//			int j=0;
//
//			for(int i=mode;i<cnt;i++)
//			{
//				strRes[j]=strlist[i];
//				j++;
//				i+=interval;
//			}
//			return strRes;
//		}
//
//		private int[] ConvertArrayToInt(string[] value)
//		{
//			int[] intWidths=new int[value.Length];
//			for(int i=0;i< value.Length;i++)
//			{
//				int res=int.Parse(value[i]);
//				if(res<0 || res > 1000)
//				{
//					throw new InvalidCastException("Value must be between 0 and 1000");
//				}
//				intWidths[i]=res;
//			}
//			return intWidths;
//		}
//
//		private int[] ParseColumnsWidth(string value)
//		{
//			string[] strWidths=value.Split(';');
//			int cnt=strWidths.Length;
//			int[] intWidths=new int[cnt];
//			try
//			{
//				for(int i=0;i< strWidths.Length;i++)
//				{
//					//intWidths[i]=Types.StringToInt(strWidths[i],-1);
//					int res=int.Parse(strWidths[i]);
//					if(res<0 || res > 1000)
//					{
//						throw new InvalidCastException("Value must be between 0 and 1000");
//					}
//					intWidths[i]=res;
//				}
//				return intWidths;
//			}
//			catch(Exception ex)
//			{
//				MsgBox.ShowError(ex.Message);
//				return null;
//			}
//		}
//
//		private void SetColumnsInternal(string[]Caption,int[] ColumnWidth )
//		{
//			this.m_ColWidths=ColumnWidth;
//			this.m_ColCaption=Caption;
//		}

		public void SetColumns(int[] ColumnWidth )
		{
			m_ColWidths=ColumnWidth;//new m_ColWidths[ColumnWidth.Length];
		}

		public void SetColumns(string[]ColumnNames,int[] ColumnWidth )
		{
			SetColumns(ColumnNames,ColumnNames,ColumnWidth );
		}

		public void SetColumns(string[]ColumnNames,string[] Captions,int[] ColumnWidth )
		{
			if(!(ColumnNames.Length.Equals(Captions.Length)).Equals(ColumnWidth.Length))
			{
				MsgBox.ShowError("All the Arrays must be equals");
				return;
			}
			int cnt=ColumnNames.Length;
			InitColumnCollection();
			for(int i=0;i<cnt;i++)
			{
				m_Cols.Add(new McColumn(ColumnNames[i],Captions[i],ColumnWidth[i]));
			}
		}

		#endregion

		#region Properties

        public void SortBy(McColumn Col)
        {
            if (m_dv == null) return;
            string expression = Col.GetSortExpression();
            m_dv.Sort = expression;
            base.Invalidate(true);
            //isInitItems = true;
        }

		//Convenient for resorting the ComboBox based on a column.
		public void SortBy(string Col, SortDiraction so)
		{
            if (m_dv == null) return;
            //if (so == SortOrder.None)
            //    so = SortOrder.Ascending;
            string expression = McColumn.GetSortExpression(Col, so);
            m_dv.Sort = expression;
			base.Invalidate(true);
			//isInitItems = true;
		}

		[Category("Columns"),DefaultValue(3)]
		public int ColumnSpacing
		{
			get
			{
				return m_ColumnSpacing;
			}
			set
			{
				if(value < 0 || value > 10)
				{
					throw new Exception("ColumnSpacing must be between 0 and 10");
				}
				m_ColumnSpacing = value;
				this.Invalidate();
			}
		}

//		[Category("Columns"),DefaultValue(0)]
//		public int ColumnView
//		{
//			get
//			{
//				return m_ColumnView;
//			}
//			set
//			{
//				if(value < 0)
//				{
//					throw new Exception("ColumnView must be greater than zero\r\n(set)McColumnList.ViewColumn");
//				}
//				m_ColumnView = value;
//			}
//		}
//
//		[Category("Columns"),DefaultValue(0)]
//		public int ColumnValue
//		{
//			get
//			{
//				return m_ColumnValue;
//			}
//			set
//			{
//				if(value < 0)
//				{
//					throw new Exception("ColumnValue must be greater than zero\r\n(set)McColumnList.ViewColumn");
//				}
//				m_ColumnValue = value;
//			}
//		}

		//Does nothing... yet
		//		public new bool Sorted
		//		{
		//			get
		//			{
		//				return false;
		//			}
		//		}

		//Indexer for retriving values based on the column string.
		//Will return the value of the given column at SelectedIndex row.
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object this[string ColName]
		{
			get
			{
				try
				{
					if(SelectedIndex < 0)
						return null;
					object o = m_dv.Table.Rows[SelectedIndex][ColName];
					return o;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn McColumnList[string](get).");
				}
			}
			set
			{
				try
				{
					m_dv.Table.Rows[SelectedIndex][ColName] = value;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn McColumnList[string](set).");
				}
			}
		}

		//Indexer for retriving values based on the column string.
		//Will return the value of the given column at SelectedIndex row.
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object this[int ColIndex]
		{
			get
			{
				try
				{
					if(SelectedIndex < 0)
						return null;
					object o = m_dv.Table.Rows[SelectedIndex][ColIndex];
					return o;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn McColumnList[string](get).");
				}
			}
			set
			{
				try
				{
					m_dv.Table.Rows[SelectedIndex][ColIndex] = value;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn McColumnList[string](set).");
				}
			}
		}

		//Indexer for retriving values based on the column string.
		//Will return the value of the given column at SelectedIndex row.
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object this[int Row ,int ColIndex]
		{
			get
			{
				try
				{
					if(Row < 0)
						return null;
					object o = m_dv.Table.Rows[Row][ColIndex];
					return o;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn McColumnList[string](get).");
				}
			}
			set
			{
				try
				{
					m_dv.Table.Rows[Row][ColIndex] = value;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn McColumnList[string](set).");
				}
			}
		}

		[Category("Columns"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public McColumnCollection Columns
		{
			get
			{
				if(m_Cols==null)
				{
					m_Cols = new McColumnCollection(this);
				}
				return m_Cols;
			}
		}

		[DefaultValue(-1), Category("Behavior")]
		public int ColumnImage
		{
			get
			{
				return m_ColumnImage;
			}
			set
			{
					if(value < -1 || value > Columns.Count)
					{
						throw new ArgumentException("Index is out of range");
					}
					m_ColumnImage = value;
					this.Invalidate();
			}
		}

		[DefaultValue(true), Category("Appearance")]
		public bool AlternatingDraw
		{
			get
			{
				return m_Alternating;
			}
			set
			{
				m_Alternating = value;
				this.Invalidate();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Description("ListControlSelectedValue"), Browsable(false), DefaultValue((string) null), Category("Data")]
		public new object SelectedValue
		{
			get
			{
				return m_SelectedValue;
			}
			set
			{
				if(value == System.DBNull.Value || value.Equals(null) || value.Equals(""))
				{
					SelectedIndex = -1;
					return;
				}

//				if (m_ColumnValue <0 || m_ColumnValue> m_Cols.Count)
//				{
//					throw new Exception("Incorrect ColumnValue");
//				}
				if(!this.Sorted)
				{
					this.m_dv.Sort=ValueMember;//  this.m_dv.Table.Columns[m_ValueMember].ColumnName;
				}
				int index =m_dv.Find(value);
				base.SelectedIndex = index;
				m_SelectedValue =value; 
			}
		}

		#endregion

		#region Hide Property

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public new string DisplayMember
		{
			get{ return base.DisplayMember; }
			set
			{
				base.DisplayMember=value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public new string ValueMember
		{
			get{ return base.ValueMember; }
			set
			{
				base.ValueMember=value;			
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public new ListBox.ObjectCollection Items
		{
			get {return base.Items;}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public override DrawMode DrawMode
		{
			get
			{
				return base.DrawMode;// m_DrawMode;
			}
			set
			{
				base.DrawMode=value;
			}
		}
 
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public new  bool IntegralHeight
		{
			get
			{
				return base.IntegralHeight;
			}
			set
			{
				base.IntegralHeight=value;
			}
		}
 	
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public new DrawItemStyle DrawItemStyle
		{
			get
			{
				return base.DrawItemStyle;
			}
			set
			{
				base.DrawItemStyle = DrawItemStyle.Default;
			}
		}

        //[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
        //public new bool UseFirstImage
        //{
        //    get
        //    {
        //        return base.UseFirstImage;
        //    }
        //    set
        //    {
        //        base.UseFirstImage = value;
        //    }
        //}

		#endregion

		#region public Methods

		public virtual  void PerformImport()
		{
			try
			{
                DataTable dt= AdoImport.Import();
				//DataSet ds=Nistec.Data.ImportUtil.ImportXml();
				if(dt==null)// || ds.Tables.Count==0)
					return;
				this.DataSource=dt.DefaultView;
			}
			catch(Exception ex)
			{
				MsgBox.ShowError(ex.Message);
			}
		}

        public virtual void PerformExport(string filter,string sort)
        {
            if (this.m_dv == null)
                return;
            string fileName = AdoExport.GetFileExportName();

            if (fileName != "")
            {
                System.Data.DataTable dtExport = Nistec.Data.DataUtil.GetFilteredData(this.m_dv, filter, sort);
                //PerformExport(dtExport, fileName);
                AdoExport.Export(dtExport.Copy(), false);
            }
        }

        public virtual void PerformExport()
        {

            if (this.m_dv == null)
                return;

            //string filter="XLS files (*.xls)|*.xls|CSV files (*.csv)|*.csv";
            //string fileName=Util.CommonDialog.SaveAs  (filter); 
            McColumn[] cols = m_Cols.GetVisibleColumns();

            AdoExport.Export(this.m_dv.Table.Copy(), false);

            //string fileName = AdoExport.GetFileExportName();

            //if (fileName != "")
            //{
            //    System.Data.DataTable dtExport = this.m_dv.Table.Copy();
            //    PerformExport(dtExport, fileName);
            //}

        }

        //private void PerformExport(DataTable dtExport, string fileName)
        //{
 

        //    Nistec.Data.ExportUtil ex = new Nistec.Data.ExportUtil();//Nistec.Data.AppType.Win);

        //    McColumn[] cols = m_Cols.GetVisibleColumns();
        //    int cnt = cols.Length;
        //    int[] columnList = new int[cnt];
        //    string[] Headers = new string[cnt];

        //    for (int i = 0; i < cnt; i++)
        //    {
        //        Headers[i] = cols[i].Caption;
        //        columnList[i] = cols[i].ColumnOrdinal;
        //    }

        //    if (fileName.ToLower().EndsWith("csv"))
        //        ex.Export(dtExport, /*columnList,*/ Headers,  Nistec.Data.ExportFormat.CSV, fileName);
        //    else if (fileName.ToLower().EndsWith("xls"))
        //        ex.Export(dtExport, /*columnList, */ Headers, Nistec.Data.ExportFormat.Excel, fileName);
        //    else if (fileName.ToLower().EndsWith("htm"))
        //        ex.Export(dtExport, /*columnList, */ Headers, Nistec.Data.ExportFormat.Html, fileName);
        //    else if (fileName.ToLower().EndsWith("xml"))
        //        ex.Export(dtExport, /*columnList, */ Headers, Nistec.Data.ExportFormat.Xml, fileName);    

        //}

		public virtual void PerformPrint()
		{
            ReportBuilder.PrintDataView(this.m_dv, this.m_dv.Table.TableName);
            //DataViewDocument rptList=new Nistec.Printing.DataViewDocument(this.m_dv,this.m_dv.Table.TableName);
		  	
            //rptList.SetDefaultStyle();
            //rptList.CreateDocument(	DataViewDocument.ConvertRtlToAlignment(this.RightToLeft),
            //    (float)this.performColumnWidth,
            //    this.Columns.GetVisibleColumns());
            //rptList.Show();
		}

		public virtual void PerformFilter()
		{
			if(this.m_dv!=null)
				FilterDlg.Open(this, m_dv);
		}

		public virtual void PerformRemoveFilter()
		{
			if(this.m_dv!=null)
				m_dv.RowFilter="";
		}

        //public virtual void PerformGetDataDB()
        //{
        //    Nistec.WinForms.PrintDB pdb=new Nistec.WinForms.PrintDB();
        //    pdb.Owner=this.Parent.FindForm();
        //    pdb.SetStyleLayout(this.LayoutManager.Layout);
		
        //    pdb.Open(new object[]{this.Parent,"GetView"});
        //}


//		public virtual void PerformColumnsFilter()
//		{
//			if(this.Columns.Count>0) 
//				ColumnFilterDlg.ShowColumns(this);
//		}
		#endregion



	}
}
