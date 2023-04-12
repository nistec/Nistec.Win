using System;
using System.Windows.Forms;
//using Nistec.GridView.Columns;  

namespace Nistec.GridView 
{

	#region delegates
    /// <summary>
    /// Cell Validating Event Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	public delegate void CellValidatingEventHandler(object sender,CellValidatingEventArgs e);
    /// <summary>
    /// Button Click Event Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	public delegate void ButtonClickEventHandler(object sender,ButtonClickEventArgs e);
    /// <summary>
    /// Column Resize Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ColumnResizeHandler(object sender, ColumnResizeEventArgs e);

    /// <summary>
    /// Field Changed Event Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void FieldChangedEventHandler(object sender, FieldChangedEventArgs e);

    //public delegate void CellClickEventHandler(object sender,CellClickEventArgs e);
    //public delegate void GridRowStateEventHandler(object sender, GridRowStateEventArgs e);

	#endregion

	#region Enums
    /// <summary>
    /// UpdateState
    /// </summary>
	public enum UpdateState
	{
        /// <summary>
        /// Commit
        /// </summary>
		Commit,
        /// <summary>
        /// Rollback
        /// </summary>
		Rollback,
        /// <summary>
        /// Cancel
        /// </summary>
		Cancel
	}
	#endregion

	#region CellValidating
    /// <summary>
    /// Cell Validating Event class
    /// </summary>
	public class CellValidatingEventArgs : System.ComponentModel.CancelEventArgs 
	{
        
		private string mColName;
        private int mRow;
        //private bool mCancel;
        private object mValue;
        
        /// <summary>
        /// Initilaized Cell Validating Event class
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="colName"></param>
        /// <param name="value"></param>
		public CellValidatingEventArgs(int rowNum, string colName, object value) 
		{
			mRow = rowNum;
			mColName = colName;
			//mCancel = false;
			mValue = value;
		}
        /// <summary>
        /// Get the cell row index
        /// </summary>
		public int Row 
		{
			get{return mRow;}
		}
        /// <summary>
        /// get the cell value
        /// </summary>
		public object Value 
		{
			get{return mValue;}
		}
        /// <summary>
        /// Get the cell column name
        /// </summary>
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

    //public class LinkClickEvent : EventArgs 
    //{
        
    //    private int m_rowNum;
    //    private int m_columnNum;
    //    private GridTableStyle m_TblStyle;
    //    private System.Windows.Forms.LinkLabel mLink=new LinkLabel ();  
      
    //    // New 
    //    public LinkClickEvent(GridTableStyle tbl, int rowNum, int columnNum) 
    //    {
    //        m_TblStyle=tbl;
    //        m_rowNum = rowNum;
    //        m_columnNum = columnNum;
    //        mLink=new LinkLabel ();  
    //    }

    //    public int Column 
    //    {
    //        get{return m_columnNum;}
    //    }
        
    //    public int Row 
    //    {
    //        get{return m_rowNum;}
    //    }

    //    public string CellValue 
    //    {
    //        get{return m_TblStyle.Grid[m_rowNum,m_columnNum].ToString () ;}
    //    }

    //    public string MappingName 
    //    {
    //        get{return m_TblStyle.GridColumnStyles[m_columnNum].MappingName ;}
    //    }

    //    public string HeaderText 
    //    {
    //        get{return m_TblStyle.GridColumnStyles[m_columnNum].HeaderText  ;}
    //    }
		
    //    public void Link()
    //    {
    //        // Determine which link was clicked within the McLinkLabel.
    //        //this.mLink.Links[mLink.Links.IndexOf(e.Link)].Visited = true;
    //        try
    //        {
    //            string target = m_TblStyle.Grid [m_rowNum,m_columnNum]as string; 

    //            if(null != target)// && target.StartsWith("www"))
    //            {
    //                System.Diagnostics.Process.Start(target);
    //            }
    //        }
	
    //        catch(Exception ex)
    //        {
    //            MessageBox.Show (ex.Message.ToString ()); 
    //        }
    //    }

    //}
	#endregion

	#region CellClickEvent

    //public class CellClickEventArgs : EventArgs 
    //{
        
    //    private int m_rowNum;
    //    private int m_columnNum;
    //    private GridTableStyle m_TblStyle;
        
    //    // New 
    //    public CellClickEventArgs(GridTableStyle tbl, int rowNum, int columnNum) 
    //    {
    //        m_TblStyle=tbl;
    //        m_rowNum = rowNum;
    //        m_columnNum = columnNum;
    //    }
        
    //    public int Column 
    //    {
    //        get{return m_columnNum;}
    //    }
        
    //    public int Row 
    //    {
    //        get{return m_rowNum;}
    //    }

    //    public string CellValue 
    //    {
    //        get{return m_TblStyle.Grid[m_rowNum,m_columnNum].ToString () ;}
    //    }

    //    public string MappingName 
    //    {
    //        get{return m_TblStyle.GridColumnStyles[m_columnNum].MappingName ;}
    //    }

    //    public string HeaderText 
    //    {
    //        get{return m_TblStyle.GridColumnStyles[m_columnNum].HeaderText  ;}
    //    }
		
    //}
	
	#endregion

	#region ButtonClickEventArgs

    /// <summary>
    /// Button Click EventArgs class
    /// </summary>
	public class ButtonClickEventArgs : EventArgs 
	{
		
		//private int m_rowNum;
		private string m_MappingName;
		private object m_CellValue;
        /// <summary>
        /// Get the button MappingName
        /// </summary>
        public string MappingName
        {
            get { return m_MappingName; }
        }

        //public int Row
        //{
        //    get { return m_rowNum; }
        //}
        /// <summary>
        /// Get the button Value
        /// </summary>
		public object Value 
		{
			get { return m_CellValue; }
		}

        //public ButtonClickEventArgs(object cellValue ) : base() 
        //{
        //    m_CellValue = cellValue;
        //}

        /// <summary>
        /// Initilaized Button ClickEventArgs
        /// </summary>
        /// <param name="mappingName"></param>
        /// <param name="cellValue"></param>
        public ButtonClickEventArgs(string mappingName, object cellValue)
            : base()
        {

            //m_rowNum = rowNum;
            m_MappingName = mappingName;
            m_CellValue = cellValue;

        }

	}

	#endregion

    #region LinkClickEvent

    //public class GridRowStateEventArgs : EventArgs
    //{
    //    public readonly GridRowChangeState State = GridRowChangeState.None;

    //    public GridRowStateEventArgs(GridRowChangeState state)
    //    {
    //        State = state;
    //    }
    //}
    #endregion

    #region ColumnResizeEventArgs

    /// <summary>
    /// Column Resize EventArgs class
    /// </summary>
    public class ColumnResizeEventArgs : EventArgs
    {

        private int m_NewSize;
        //private int m_OldSize;
        private int m_columnNum;
        /// <summary>
        /// Get the column
        /// </summary>
        public int Column
        {
            get { return m_columnNum; }
        }
        /// <summary>
        /// Get the new size
        /// </summary>
        public int NewSize
        {
            get { return m_NewSize; }
        }

        /*public int OldSize 
        {
            get { return m_OldSize; }
        }*/
        /// <summary>
        /// Initilaized Column Resize EventArgs
        /// </summary>
        /// <param name="newSize"></param>
        /// <param name="columnNum"></param>
        public ColumnResizeEventArgs(int newSize, int columnNum)
            : base()
        {
            m_NewSize = newSize;
            //m_OldSize = oldSize;
            m_columnNum = columnNum;
        }

    }

    #endregion

    #region VFieldValidating

    /// <summary>
    /// Represent a Field Changed EventArgs class
    /// </summary>
    public class FieldChangedEventArgs : EventArgs
    {

        private string _Key;
        private object _Value;

        /// <summary>
        /// Initilaizing Field Changed EventArgs
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public FieldChangedEventArgs(string key, object value)
        {
            _Key = key;
            _Value = value;
        }
        /// <summary>
        /// Get the field value
        /// </summary>
        public object Value
        {
            get { return _Value; }
        }
        /// <summary>
        /// Get the field key
        /// </summary>
        public string Field
        {
            get { return _Key; }
        }

     }
    #endregion


}