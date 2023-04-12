using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions ; 
using System.Collections;

using mControl.GridStyle.Columns;
using mControl.Util;

namespace mControl.GridStyle.Controls
{


	public enum StatusBarMode
	{
		Hide=0,
		Show=1,
		ShowPanels=2
	}

	/// <summary>
	/// Summary description for GridLinkLabel.
	/// </summary>
	[Designer(typeof(GridStatusBarDesigner))]
	[System.ComponentModel.ToolboxItem(false)]
	public class GridStatusBar : mControl.WinCtl.Controls.CtlStatusBar   
	{
		#region Members

		// Fields
		private StatusBarMode m_StatusBarMode; 
		private GridStyle.Grid grid;
		private int panelsCount;

		#endregion

		#region Constructor

		public GridStatusBar()
		{
			m_StatusBarMode=StatusBarMode.Hide;
		}

		public GridStatusBar(GridStyle.Grid ctl)
		{
			m_StatusBarMode=StatusBarMode.Hide;
			grid=ctl;
		}

		#endregion

		#region internal methods

		public void SetDataGrid(GridStyle.Grid parentGrid)
		{
			this.grid = (GridStyle.Grid)parentGrid;
		}

		#endregion

		#region status Methods

		public  void ShowPanelsInternal(DataGridTableStyle  tbl)
		{
			ShowPanelColumns(tbl,false);
		}

		public void ShowPanelColumns(DataGridTableStyle  tbl,bool RunSumPanels)
		{
			this.StatusBarMode=mControl.GridStyle.Controls.StatusBarMode.ShowPanels;
			int cnt=tbl.GridColumnStyles.Count;  
			this.Panels.Clear ();
			StatusBarPanel pnlx=null;
			pnlx=this.Panels.Add("");   
			if(tbl.RowHeadersVisible )
			{
				pnlx.Width =tbl.RowHeaderWidth -1;   
				//pnlx.Icon=ResourceUtils.LoadIcon ("properties.ico");      
				pnlx.Text ="?";
	
			}
			else
			{
				pnlx.Width =0;   
			}
			pnlx.BorderStyle  =System.Windows.Forms.StatusBarPanelBorderStyle.None ;    
			//Alignment to right becouse its a number
			pnlx.Alignment   =System.Windows.Forms.HorizontalAlignment.Center ;    
		
			bool runSum= RunSumPanels && tbl.GridColumnStyles.Count>0;
		
			for(int i=0;i<cnt;i++)
			{
				pnlx=this.Panels.Add(tbl.GridColumnStyles[i].MappingName);   
				pnlx.Width =tbl.GridColumnStyles[i].Width ;

				pnlx.Text ="";//i.ToString();
				pnlx.BorderStyle =System.Windows.Forms.StatusBarPanelBorderStyle.None   ;    
				//Alignment to right becouse its a number
				pnlx.Alignment   =System.Windows.Forms.HorizontalAlignment.Right ;
	
				if(runSum)
				{
					if(tbl.GridColumnStyles[i] !=null)
					{
						if(((GridColumnStyle)tbl.GridColumnStyles[i]).ShowSum)
							SumGridColumn(i);
					}
				}

			}
			panelsCount=cnt;
		}

		private void ShowPanelColumns(DataGridTableStyle  tbl,bool RunSumPanels,int cnt)
		{
			StatusBarPanel pnlx=null;
	
			for(int i=0;i<cnt;i++)
			{
				pnlx=this.Panels.Add(tbl.GridColumnStyles[i].MappingName);   
				pnlx.Width =tbl.GridColumnStyles[i].Width ;

				pnlx.Text ="";//i.ToString();
				pnlx.BorderStyle =System.Windows.Forms.StatusBarPanelBorderStyle.None   ;    
				//Alignment to right becouse its a number
				pnlx.Alignment   =System.Windows.Forms.HorizontalAlignment.Right ;

				if(RunSumPanels && tbl.GridColumnStyles.Count>0)
				{
					if(tbl.GridColumnStyles[i] !=null)
					{
						if(((GridColumnStyle)tbl.GridColumnStyles[i]).ShowSum)
							SumGridColumn(i);
					}
				}

			}
			panelsCount=cnt;
		}

		public  void ResettingPanels(DataGridTableStyle  tbl,bool RunSumPanels)
		{
			int cnt=tbl.GridColumnStyles.Count; 
			int w=0;
	
			if(cnt !=panelsCount)
			{
               ShowPanelColumns(tbl,RunSumPanels);
				return;
			}

			StatusBarPanel pnlx=null;
			pnlx=this.Panels[0];   
			
			if(tbl.RowHeadersVisible )
			{
				pnlx.Width =tbl.RowHeaderWidth -1;   
			}
			else
			{
				pnlx.MinWidth  =0;   
				pnlx.Width =0;   
			}

			for(int i=0;i<cnt;i++)
			{
				pnlx=this.Panels[i+1];   
				if(tbl.GridColumnStyles[i].MappingName =="" )
					pnlx.Width =0 ;
				else 
				{
					w=tbl.GridColumnStyles[i].Width;
					if(w<pnlx.MinWidth)
						w=pnlx.MinWidth;
					pnlx.Width =w;
				}
			}
		}

		public string GetColumnPanel(int index)
		{
			if(index+1<1 || index+1 >this.Panels.Count)
			{
				throw new Exception("index out of range");
			}
            return this.Panels[index+1].Text ; 
		}

		public void SumGridColumn(int index)
		{

			if(this.StatusBarMode!=StatusBarMode.ShowPanels)
				return;

			if(index+1<1 || index+1 >this.Panels.Count)
			{
				throw new Exception("index out of range");
			}

			try
			{
				int cnt=this.grid.DataList.Count;//.Table.Rows.Count ;  
				decimal res=0; 
				res=RunAggregate(this.grid.Columns[index],cnt,index);

//				for(int i=0;i<cnt;i++)
//				{
//					res+= Regx.ParseDecimal(this.grid[i,index].ToString(),0);
//					//res+= System.Convert.ToDecimal ( this.grid[i,index]);
//				}

				this.Panels[index+1].Text =res.ToString ("N");   
				//}
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message.ToString (),"mControl"); 
			}
		}

		internal void SumGridColumn(int index,decimal oldValue,decimal newValue)
		{
			if(this.StatusBarMode!=StatusBarMode.ShowPanels)
				return;
			if(index+1<1 || index+1 >this.Panels.Count)
			{
				throw new Exception("index out of range");
			}
		
			try
			{
				decimal res= Regx.ParseDecimal(this.Panels[index+1].Text,0);
				//decimal res= System.Convert.ToDecimal ( this.Panels[index+1].Text); 
				AggregateMode mode=grid.Columns[index].SumMode;

				switch(mode)
				{
					case AggregateMode.Sum:
						res+= (newValue-oldValue);
						break;
					case AggregateMode.Avg:
						int cnt=this.grid.Rows.Count;
						if(cnt>0)
						res = (res+newValue-oldValue)/cnt;
						break;
					case AggregateMode.Max:
						res= Math.Max( res,newValue);
						break;
					case AggregateMode.Min:
						res= Math.Min( res,newValue);
						break;
					default:
						break;
				}
				//res+= diff;
				this.Panels[index+1].Text =res.ToString ("N");   
				this.Panels[index+1].ToolTipText=mode.ToString();
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message.ToString (),"mControl"); 
			}
		}


		public void RunSum(DataGridTableStyle  tbl)
		{
			if(this.StatusBarMode!=mControl.GridStyle.Controls.StatusBarMode.ShowPanels)
				return;
			try
			{
				int cnt=this.grid.DataList.Count;//.Table.Rows.Count ;  
				decimal res=0; 
				int index=0;
	
				foreach(DataGridColumnStyle c in tbl.GridColumnStyles)
				{
					if(((GridColumnStyle)c).ShowSum )
					{
						res=RunAggregate((GridColumnStyle)c,cnt,index);
						this.Panels[index+1].Text =res.ToString ("N");   
					}
					index++;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message.ToString (),"mControl"); 
			}
	
		}

		public void RunSum()
		{
			if(this.StatusBarMode!=mControl.GridStyle.Controls.StatusBarMode.ShowPanels)
				return;
			int cnt=this.grid.DataList.Count;//.Table.Rows.Count ;  
			decimal res=0; 
			int index=0;
	
			foreach(GridColumnStyle c in this.grid.Columns)
			{
				if(c.ShowSum )
				{
					res=RunAggregate(c,cnt,index);
					this.Panels[index+1].Text =res.ToString ("N");   
				}
				index++;
			}
		}

		internal decimal SumGridColumn(int colIndex,AggregateMode mode)
		{
			if(colIndex<0 || colIndex > this.grid.Columns.Count-1)
			{
				throw new Exception("index out of range");
			}

			int count=this.grid.DataList.Count;  
			GridColumnStyle c = this.grid.Columns[colIndex];
			if(c.ShowSum )
			{
				switch(mode)
				{
					case AggregateMode.Sum:
						return SetSum(count,colIndex);
					case AggregateMode.Avg:
						return SetAvg(count,colIndex);
					case AggregateMode.Max:
						return SetMax(count,colIndex);
					case AggregateMode.Min:
						return SetMin(count,colIndex);
					default:
						return 0;
				}
			}
			return 0;
		}

		private decimal RunAggregate(GridColumnStyle col, int count,int colIndex)
		{

			AggregateMode mode=col.SumMode;
			this.Panels[colIndex+1].ToolTipText =mode.ToString();   

			switch(mode)
			{
				case AggregateMode.Sum:
					return SetSum(count,colIndex);
				case AggregateMode.Avg:
					return SetAvg(count,colIndex);
				case AggregateMode.Max:
					return SetMax(count,colIndex);
				case AggregateMode.Min:
					return SetMin(count,colIndex);
				default:
					return 0;
			}
		}

		private decimal SetSum(int count,int colIndex)
		{
			decimal res=0; 
			for(int i=0;i<count;i++)
			{
				res+= Regx.ParseDecimal(this.grid[i,colIndex].ToString(),(decimal)0);
			}
			return res;
		}
		private decimal SetAvg(int count,int colIndex)
		{
			if(count<=0)
				return 0;
			decimal res=0; 
			for(int i=0;i<count;i++)
			{
				res+= Regx.ParseDecimal(this.grid[i,colIndex].ToString(),(decimal)0);
			}
			return res/count;
		}
		private decimal SetMax(int count,int colIndex)
		{
			decimal res=0; 
			for(int i=0;i<count;i++)
			{
				res= Math.Max(res, Regx.ParseDecimal(this.grid[i,colIndex].ToString(),(decimal)0));
			}
			return res;
		}
		private decimal SetMin(int count,int colIndex)
		{
			decimal res=0; 
			for(int i=0;i<count;i++)
			{
				res= Math.Min(res, Regx.ParseDecimal(this.grid[i,colIndex].ToString(),(decimal)0));
			}
			return res;
		}

		
		protected void mGrid_ColumnResize(object sender,GridStyle.ColumnResizeEventArgs e)
		{
			if(this.StatusBarMode==mControl.GridStyle.Controls.StatusBarMode.ShowPanels)
			{
				this.Panels [e.Column+1].Width =e.NewSize;   
			}
		}

		#endregion

		#region statusPopUp

//		private void SetPopUpStatus()
//		{
//			m_StatusContext=new mControl.WinCtl.Controls.CtlContextMenu ();
//			MenuItem itmFilter=m_StatusContext.MenuItems.Add ("Filter");
//			MenuItem itmHide=m_StatusContext.MenuItems.Add ("Hide");
//			MenuItem itmSum=m_StatusContext.MenuItems.Add ("SumPanels");
//			MenuItem itmExport=m_StatusContext.MenuItems.Add ("Export");
//			itmFilter.Click +=new EventHandler(itmFilter_Click);
//			itmHide.Click +=new EventHandler(itmHide_Click);
//			itmSum.Click +=new EventHandler(itmSum_Click);
//			itmExport.Click +=new EventHandler(itmExport_Click);
//			this.ContextMenu =m_StatusContext;
//		}
//
//		protected void itmFilter_Click( object sender,EventArgs e)
//		{
//			GridStyle.FilterDlg.Open(this.grid );
//		}
//
//		protected void itmHide_Click( object sender,EventArgs e)
//		{
//			this.ShowStatusBar =false;
//		}
//		protected void itmSum_Click( object sender,EventArgs e)
//		{
//			this.RunSum (((GridStyle.GridTableStyle) this.grid.DataGrid.TableStyle) );
//		}

		private void InitializeComponent()
		{
			// 
			// GridStatusBar
			// 
			//this.StyleCtl.BorderColor = System.Drawing.Color.SteelBlue;
			//this.StyleCtl.FlatColor = System.Drawing.Color.AliceBlue;
			//this.StyleCtl.PanelsBackColor = System.Drawing.Color.Gray;
			//this.StyleCtl.PanelsBorderColor = System.Drawing.Color.LightGray;
			//this.StyleCtl.StylePlan = mControl.WinCtl.Controls.Styles.SteelBlue;

		}
		
//		protected void itmExport_Click( object sender,EventArgs e)
//		{
//			string filter="XLS files (*.xls)|*.xls|CSV files (*.csv)|*.csv";
//			string fileName=CommonDialog.SaveAs  (filter); 
// 
//			if(fileName!="")
//			{
//	
//				//int cnt=this.dataGrid.TableStyle.GridColumnStyles.Count;
//				//int[] columnList=new int [cnt];
//				//string[] Headers=new string[cnt]; 
//				
//				//for(int i=0;i<cnt;i++)
//				//{
//				//	Headers.SetValue (this.dataGrid.TableStyle.GridColumnStyles[i].HeaderText,i); 
//				//}
//
//				mControl.Data.Export ex=new mControl.Data.Export (mControl .Data.AppType.Win);
//				System.Data.DataTable dtExport = this.grid.DataList.Table.Copy();
//				//for(int i=0;i<cnt;i++)
//				//{
//				//	Headers.SetValue (this.dataGrid.TableStyle.GridColumnStyles[i].HeaderText,i); 
//				//}
//
//				if(fileName.EndsWith ("csv"))
//					ex.ExportDetails (dtExport ,mControl.Data.ExportFormat.CSV,fileName);    
//				else if(fileName.EndsWith ("xls"))
//					ex.ExportDetails (dtExport ,mControl.Data.ExportFormat.Excel,fileName);    
//	
//			}
//		}

		#endregion

		#region Properties

		[Browsable(false)]   
		private new bool TabStop 
		{
			get{return base.TabStop;}
			set{base.TabStop =value;}
		}

		[Category("Behavior"),DefaultValue(StatusBarMode.Hide)]
		public StatusBarMode StatusBarMode 
		{
			get{return m_StatusBarMode;}
			set
			{
				if(m_StatusBarMode !=value)
				{
					m_StatusBarMode =value;
					this.Visible= value !=StatusBarMode.Hide;
					this.ShowPanels=value==StatusBarMode.ShowPanels;
					this.Invalidate();
				}
			}
		}

		public override mControl.WinCtl.Controls.IStyleLayout CtlStyleLayout
		{
			get
			{
				if(grid!=null)
				   return grid.CtlStyleLayout;
				return base.CtlStyleLayout;
			}
		}

		#endregion

	}

	#region GridStatusBar Desiner

	//[Designer(typeof(CtlBaseDesigner))]

	internal class GridStatusBarDesigner : System.Windows.Forms.Design.ControlDesigner
	{
		public GridStatusBarDesigner(){}

		protected override void PreFilterProperties(IDictionary properties)
		{
			//base.PreFilterProperties (properties);
			properties.Remove("TabStop");
		}

		protected override void PreFilterAttributes(IDictionary attributes)
		{
			base.PreFilterAttributes (attributes);
	
		}

		protected override void PostFilterProperties(IDictionary Properties)
		{
			Properties.Remove("TabStop");
			Properties.Remove("ForeColor");
			Properties.Remove("BackColor");
			Properties.Remove("AutoScroll");
			Properties.Remove("AutoScrollMargin");
			Properties.Remove("AutoScrollMinSize");
			Properties.Remove("BackgroundImage");
			Properties.Remove("Image");
			Properties.Remove("ImageAlign");
			Properties.Remove("ImageIndex");
			Properties.Remove("ImageList");
			Properties.Remove("AllowDrop");
			Properties.Remove("ContextMenu");
			Properties.Remove("FlatStyle");
			Properties.Remove("Text");
			Properties.Remove("TextAlign");

		}
	}
	#endregion

}
