using System;
using System.Windows.Forms;
using mControl.GridStyle.Columns;  

namespace mControl.GridStyle 
{

	#region delegates

	public delegate void CellValidatingEventHandler(object sender,CellValidatingEventArgs e);
	public delegate void CellClickEventHandler(object sender,CellClickEventArgs e);
	public delegate void ButtonClickEventHandler(object sender,ButtonClickEventArgs e);
	public delegate void ColumnResizeHandler(object sender, ColumnResizeEventArgs e);
	public delegate void RowBeforeUpdateHandler(object sender, RowUpdateEvent e);

	#endregion

	#region Enums
	public enum UpdateState
	{
		Commit,
		Rollback,
		Cancel
	}
	#endregion

	#region CellValidating

	public class CellValidatingEventArgs : System.ComponentModel.CancelEventArgs 
	{
        
		protected string mColName;
		protected int mRow;
		protected bool mCancel;
		protected object mValue;
        
		public CellValidatingEventArgs(int rowNum, string colName, object value) 
		{
			mRow = rowNum;
			mColName = colName;
			mCancel = false;
			mValue = value;
		}
        
		public int Row 
		{
			get{return mRow;}
		}
        
		public object Value 
		{
			get{return mValue;}
		}
        
		public string ColName 
		{
			get{return mColName;}
		}
        
//		public bool Cancel 
//		{
//			get{return base.Cancel;}
//			set{base.Cancel = value;}
//		}
	}
	#endregion

	#region LinkClickEvent

	public class LinkClickEvent : EventArgs 
	{
        
		private int m_rowNum;
		private int m_columnNum;
		private DataGridTableStyle m_TblStyle;
		private System.Windows.Forms.LinkLabel mLink=new LinkLabel ();  
      
		// New 
		public LinkClickEvent(DataGridTableStyle tbl, int rowNum, int columnNum) 
		{
			m_TblStyle=tbl;
			m_rowNum = rowNum;
			m_columnNum = columnNum;
			mLink=new LinkLabel ();  
		}

		public int Column 
		{
			get{return m_columnNum;}
		}
        
		public int Row 
		{
			get{return m_rowNum;}
		}

		public string CellValue 
		{
			get{return m_TblStyle.DataGrid[m_rowNum,m_columnNum].ToString () ;}
		}

		public string MappingName 
		{
			get{return m_TblStyle.GridColumnStyles[m_columnNum].MappingName ;}
		}

		public string HeaderText 
		{
			get{return m_TblStyle.GridColumnStyles[m_columnNum].HeaderText  ;}
		}
		
		public void Link()
		{
			// Determine which link was clicked within the CtlLinkLabel.
			//this.mLink.Links[mLink.Links.IndexOf(e.Link)].Visited = true;
			try
			{
				string target = m_TblStyle.DataGrid [m_rowNum,m_columnNum]as string; 

				if(null != target)// && target.StartsWith("www"))
				{
					System.Diagnostics.Process.Start(target);
				}
			}
	
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message.ToString ()); 
			}
		}

	}
	#endregion

	#region CellClickEvent

	public class CellClickEventArgs : EventArgs 
	{
        
		private int m_rowNum;
		private int m_columnNum;
		private DataGridTableStyle m_TblStyle;
        
		// New 
		public CellClickEventArgs(DataGridTableStyle tbl, int rowNum, int columnNum) 
		{
			m_TblStyle=tbl;
			m_rowNum = rowNum;
			m_columnNum = columnNum;
		}
        
		public int Column 
		{
			get{return m_columnNum;}
		}
        
		public int Row 
		{
			get{return m_rowNum;}
		}

		public string CellValue 
		{
			get{return m_TblStyle.DataGrid[m_rowNum,m_columnNum].ToString () ;}
		}

		public string MappingName 
		{
			get{return m_TblStyle.GridColumnStyles[m_columnNum].MappingName ;}
		}

		public string HeaderText 
		{
			get{return m_TblStyle.GridColumnStyles[m_columnNum].HeaderText  ;}
		}
		
	}
	
	#endregion

	#region ButtonClickEventArgs

	public class ButtonClickEventArgs : EventArgs 
	{
		
		private int m_rowNum;
		private string m_MappingName;
		private string m_CellValue;

		public string MappingName {
			get { return m_MappingName; }
		}

		public int Row {
			get { return m_rowNum; }
		}

		public string CellValue {
			get { return m_CellValue; }
		}

		public ButtonClickEventArgs( int rowNum, string mappingName, string cellValue ) : base() {
			
			m_rowNum = rowNum;
			m_MappingName = mappingName;
			m_CellValue = cellValue;

		}

	}

	#endregion

	#region ColumnResizeEventArgs

	public class ColumnResizeEventArgs : EventArgs 
	{
		
		private int m_NewSize;
		//private int m_OldSize;
		private int m_columnNum;
	
		public int Column 
		{
			get { return m_columnNum; }
		}

		public int NewSize 
		{
			get { return m_NewSize; }
		}

		/*public int OldSize 
		{
			get { return m_OldSize; }
		}*/
	
		public ColumnResizeEventArgs( int newSize, int columnNum ) : base() 
		{
			m_NewSize = newSize;
			//m_OldSize = oldSize;
			m_columnNum = columnNum;
		}

	}

	#endregion

	#region RowUpdateEvent

	public class RowUpdateEvent : EventArgs 
	{
        
		//protected CurrencyManager mCManger;
		protected int mRow;
		protected UpdateState mState;
		protected object mValue;
        
		public RowUpdateEvent(int rowNum) 
		{
			mRow = rowNum;
			//mCManger = cMngr;
			mState = UpdateState.Commit;
			//mValue = iValue;
		}
        
		public int Row 
		{
			get{return mRow;}
		}
        
		/*public object Value 
		{
			get{return mValue;}
		}*/
        
		public UpdateState RowState 
		{
			get{return mState;}
			set{mState = value;}
		}
	}
	#endregion


}